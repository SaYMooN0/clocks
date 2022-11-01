using System.Drawing;

namespace clocks
{
    public partial class Form1 : Form
    {
        int Lenght = 100;
        public Form1()
        {
            InitializeComponent();
            g = CreateGraphics();
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
        }
        Graphics g;
        Color bkColor = Control.DefaultBackColor;
        private void PaintBackGround()
        {
            g.FillRectangle(
                new SolidBrush(bkColor),
                new Rectangle(
                    new Point(
                        (int)(this.ClientRectangle.Width / 2 - Lenght),
                        (int)(this.ClientRectangle.Height / 2 - Lenght)),
                        new Size((int)Lenght * 2, (int)Lenght * 2)));
        }
        private void PaintCircle()
        {
            g.DrawEllipse(
                new Pen(
                    new SolidBrush(Color.Black),
                    2),
                new Rectangle(
                    new Point(
                        (int)(this.ClientRectangle.Width / 2 - Lenght),
                        (int)(this.ClientRectangle.Height / 2 - Lenght)),
                        new Size((int)Lenght * 2, (int)Lenght * 2)));
            //прорисовка линий, который указывают на деления часов
            Font font = new Font("System", 12, FontStyle.Regular);
            for (int i = 0; i < 12; i++)
            {
                g.DrawLine(new Pen(new SolidBrush(Color.Black), 1),
                    new Point((int)(ClientRectangle.Width / 2), (int)(ClientRectangle.Height / 2)),
                    new Point((int)(ClientRectangle.Width / 2 + Lenght * Math.Cos(Math.PI / 6 * i)), (int)(ClientRectangle.Height / 2 + Lenght * Math.Sin(Math.PI / 6 * i))));
                g.DrawString($"{i}", font, Brushes.Black, (int)(ClientRectangle.Width / 2.05 + 1.2 * Lenght * Math.Sin(Math.PI / 6 * i)), (int)(ClientRectangle.Height / 2.1 - 1.2 * Lenght * Math.Cos(Math.PI / 6 * i)));


            }
            //прорисовка круга, который закрывает внутреннюю часть линий чтобы остались только черточки
            g.FillEllipse(
                new SolidBrush(bkColor),
                new Rectangle(
                    new Point(
                        (int)(this.ClientRectangle.Width / 2 - Lenght+10),
                        (int)(this.ClientRectangle.Height / 2 - Lenght +10)),
                        new Size((int)(Lenght - 10) * 2, (int)(Lenght - 10) * 2)));

        }
        private void PaintArrows(DateTime dt)
        {
            //прорисовка секундной стрелки
            g.DrawLine(
                new Pen(new SolidBrush(Color.Black), 1),
                new Point((int)(ClientRectangle.Width / 2), (int)(ClientRectangle.Height / 2)),
                new Point((int)(ClientRectangle.Width / 2 + (Lenght - 4) * Math.Sin(2 * Math.PI / 60 * dt.Second)), (int)(ClientRectangle.Height / 2 - (Lenght - 4) * Math.Cos(2 * Math.PI / 60 * dt.Second))));
            //прорисовка минутной стрелки
            g.DrawLine(
                new Pen(new SolidBrush(Color.Black), 2),
                new Point((int)(ClientRectangle.Width / 2), (int)(ClientRectangle.Height / 2)),
                new Point((int)(ClientRectangle.Width / 2 + (Lenght - 4) * Math.Sin(2 * Math.PI / 60 * dt.Minute)), (int)(ClientRectangle.Height / 2 - (Lenght - 4) * Math.Cos(2 * Math.PI / 60 * dt.Minute))));
            //определения количества часов, прошедших после полудня или после полуночи
            //фактически перевод 23=>11 и так далее
            int hour;
            if (dt.Hour <= 12)
                hour = dt.Hour;
            else
                hour = dt.Hour - 12;
            //прорисовка часовой стрелки
            g.DrawLine(
                new Pen(new SolidBrush(Color.Black), 2),
                new Point((int)(ClientRectangle.Width / 2), (int)(ClientRectangle.Height / 2)),
                new Point((int)(ClientRectangle.Width / 2 + (Lenght - 10) * Math.Sin(2 * Math.PI / 12 * hour + 2 * Math.PI / (12 * 60) * dt.Minute)), (int)(ClientRectangle.Height / 2 - (Lenght - 10) * Math.Cos(2 * Math.PI / 12 * hour + 2 * Math.PI / (12 * 60) * dt.Minute))));
        }
        private void PaintClock(DateTime dtArg)
        {
            //фон
            PaintBackGround();
            //циферблат
            PaintCircle();
            //стрелки
            PaintArrows(dtArg);
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            PaintClock(DateTime.Now);
        }
    }
}