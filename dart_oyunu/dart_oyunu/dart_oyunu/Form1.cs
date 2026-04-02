using System.Drawing.Drawing2D;

namespace dart_oyunu
{
    public partial class Form1 : Form
    {
        private const int MaxShots = 10;
        private const int BoardDiameter = 400;
        private const int BullseyeRadius = 20;
        private const int InnerRadius = 45;
        private const int MiddleRadius = 80;
        private const int OuterRadius = 130;
        private const int FarthestRadius = 190;
        private const int BalloonBonus = 20;

        private readonly List<Point> dartMarks = new();
        private readonly List<MovingBalloon> balloons = new();

        private Image? idleImage;
        private Image? shootImage;
        private Image? balloon1Image;
        private Image? balloon2Image;
        private Image? balloon3Image;
        private Image? explosionImage;

        private int totalScore;
        private int shotCount;

        private Rectangle BoardRect => boardPanel.ClientRectangle;
        private Point BoardCenter => new(BoardRect.Width / 2, BoardRect.Height / 2);

        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
            LoadImages();
            SetupBalloons();
            StartNewGame();
        }

        private void LoadImages()
        {
            idleImage = LoadImage("idle.png");
            shootImage = LoadImage("shoot.png");
            balloon1Image = LoadImage("balloon_1.gif");
            balloon2Image = LoadImage("balloon_2.gif");
            balloon3Image = LoadImage("balloon_3.gif");
            explosionImage = LoadImage("Explosion.png");

            archerPictureBox.Image = idleImage;
            archerPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private Image? LoadImage(string fileName)
        {
            var startupPath = Path.Combine(Application.StartupPath, fileName);
            if (File.Exists(startupPath))
            {
                return Image.FromFile(startupPath);
            }

            var parentPath = Path.GetFullPath(Path.Combine(Application.StartupPath, "..", "..", "..", "..", fileName));
            if (File.Exists(parentPath))
            {
                return Image.FromFile(parentPath);
            }

            return null;
        }

        private void SetupBalloons()
        {
            balloons.Clear();
            balloons.Add(new MovingBalloon(balloon1Image, new Rectangle(35, 160, 70, 90), 2));
            balloons.Add(new MovingBalloon(balloon2Image, new Rectangle(120, 240, 75, 95), 3));
            balloons.Add(new MovingBalloon(balloon3Image, new Rectangle(55, 340, 80, 100), 4));
        }

        private void StartNewGame()
        {
            totalScore = 0;
            shotCount = 0;
            dartMarks.Clear();

            foreach (var balloon in balloons)
            {
                balloon.Reset();
            }

            UpdateScoreUI();
            archerPictureBox.Image = idleImage;
            shootTimer.Stop();
            balloonTimer.Start();
            boardPanel.Invalidate();
        }

        private void UpdateScoreUI()
        {
            scoreLabel.Text = $"Puan: {totalScore}";
            shotLabel.Text = $"Atış: {shotCount}/{MaxShots}";
        }

        private void boardPanel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            DrawDartBoard(e.Graphics);
            DrawDartMarks(e.Graphics);
            DrawBalloons(e.Graphics);
        }

        private void DrawDartBoard(Graphics g)
        {
            var center = BoardCenter;

            DrawRing(g, Color.Black, FarthestRadius, center);
            DrawRing(g, Color.Goldenrod, OuterRadius, center);
            DrawRing(g, Color.Green, MiddleRadius, center);
            DrawRing(g, Color.Yellow, InnerRadius, center);
            DrawRing(g, Color.Red, BullseyeRadius, center);

            using var borderPen = new Pen(Color.White, 2);
            g.DrawEllipse(borderPen, center.X - FarthestRadius, center.Y - FarthestRadius, FarthestRadius * 2, FarthestRadius * 2);
        }

        private static void DrawRing(Graphics g, Color color, int radius, Point center)
        {
            using var brush = new SolidBrush(color);
            g.FillEllipse(brush, center.X - radius, center.Y - radius, radius * 2, radius * 2);
        }

        private void DrawDartMarks(Graphics g)
        {
            using var brush = new SolidBrush(Color.DarkSlateBlue);
            using var pen = new Pen(Color.White, 1);

            foreach (var mark in dartMarks)
            {
                var markerRect = new Rectangle(mark.X - 5, mark.Y - 5, 10, 10);
                g.FillEllipse(brush, markerRect);
                g.DrawLine(pen, mark.X, mark.Y, mark.X + 14, mark.Y - 14);
                g.DrawLine(pen, mark.X + 10, mark.Y - 16, mark.X + 14, mark.Y - 14);
                g.DrawLine(pen, mark.X + 12, mark.Y - 10, mark.X + 14, mark.Y - 14);
            }
        }

        private void DrawBalloons(Graphics g)
        {
            foreach (var balloon in balloons)
            {
                if (!balloon.Visible)
                {
                    continue;
                }

                if (balloon.IsExploding && explosionImage != null)
                {
                    g.DrawImage(explosionImage, balloon.Bounds);
                }
                else if (balloon.Image != null)
                {
                    g.DrawImage(balloon.Image, balloon.Bounds);
                }
                else
                {
                    using var fallbackBrush = new SolidBrush(Color.LightPink);
                    g.FillEllipse(fallbackBrush, balloon.Bounds);
                }
            }
        }

        private void boardPanel_MouseClick(object sender, MouseEventArgs e)
        {
            if (shotCount >= MaxShots)
            {
                return;
            }

            if (TryHitBalloon(e.Location))
            {
                UpdateScoreUI();
                boardPanel.Invalidate();
                return;
            }

            shotCount++;
            dartMarks.Add(e.Location);

            totalScore += CalculateScore(e.Location);
            archerPictureBox.Image = shootImage ?? idleImage;
            shootTimer.Stop();
            shootTimer.Start();

            UpdateScoreUI();
            boardPanel.Invalidate();

            if (shotCount >= MaxShots)
            {
                EndGame();
            }
        }

        private bool TryHitBalloon(Point clickPoint)
        {
            foreach (var balloon in balloons)
            {
                if (!balloon.Visible || balloon.IsExploding)
                {
                    continue;
                }

                if (!balloon.Bounds.Contains(clickPoint))
                {
                    continue;
                }

                balloon.StartExplosion();
                totalScore += BalloonBonus;
                return true;
            }

            return false;
        }

        private int CalculateScore(Point clickPoint)
        {
            var center = BoardCenter;
            var dx = clickPoint.X - center.X;
            var dy = clickPoint.Y - center.Y;
            var distance = Math.Sqrt(dx * dx + dy * dy);

            if (distance <= BullseyeRadius) return 50;
            if (distance <= InnerRadius) return 25;
            if (distance <= MiddleRadius) return 15;
            if (distance <= OuterRadius) return 10;
            if (distance <= FarthestRadius) return 5;
            return 0;
        }

        private void EndGame()
        {
            balloonTimer.Stop();
            var result = MessageBox.Show(
                $"Oyun bitti! Toplam puanınız: {totalScore}\n\nYeniden oynamak ister misiniz?",
                "Oyun Sonu",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                StartNewGame();
            }
        }

        private void shootTimer_Tick(object sender, EventArgs e)
        {
            shootTimer.Stop();
            archerPictureBox.Image = idleImage;
        }

        private void balloonTimer_Tick(object sender, EventArgs e)
        {
            foreach (var balloon in balloons)
            {
                balloon.Update(BoardRect);
            }

            boardPanel.Invalidate();
        }

        private void newGameButton_Click(object sender, EventArgs e)
        {
            StartNewGame();
        }

        private sealed class MovingBalloon
        {
            private readonly Rectangle initialBounds;
            private int explosionTicks;

            public MovingBalloon(Image? image, Rectangle startBounds, int speed)
            {
                Image = image;
                initialBounds = startBounds;
                Bounds = startBounds;
                Speed = speed;
            }

            public Image? Image { get; }
            public Rectangle Bounds { get; private set; }
            public int Speed { get; }
            public bool Visible { get; private set; } = true;
            public bool IsExploding { get; private set; }

            public void StartExplosion()
            {
                IsExploding = true;
                explosionTicks = 6;
            }

            public void Update(Rectangle movementBounds)
            {
                if (!Visible)
                {
                    return;
                }

                if (IsExploding)
                {
                    explosionTicks--;
                    if (explosionTicks <= 0)
                    {
                        Visible = false;
                    }

                    return;
                }

                var nextY = Bounds.Y - Speed;
                if (nextY + Bounds.Height < 0)
                {
                    nextY = movementBounds.Height + Random.Shared.Next(20, 120);
                    var nextX = Random.Shared.Next(20, Math.Max(21, movementBounds.Width - Bounds.Width - 20));
                    Bounds = new Rectangle(nextX, nextY, Bounds.Width, Bounds.Height);
                    return;
                }

                Bounds = new Rectangle(Bounds.X, nextY, Bounds.Width, Bounds.Height);
            }

            public void Reset()
            {
                Bounds = initialBounds;
                IsExploding = false;
                Visible = true;
                explosionTicks = 0;
            }
        }
    }
}
