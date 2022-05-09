﻿using SpaceBaloons.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SpaceBaloons.Renderer
{
    internal class Display : FrameworkElement
    {
        Size area;
        GameModel model;
        static Random r = new Random();

        public void SetupSizes(Size area)
        {
            this.area = area;
            this.InvalidateVisual();
        }
        public void SetupModel(GameModel model)
        {
            this.model = model;
            this.model.Changed += (sender, eventargs) => this.InvalidateVisual();
        }
        public Brush WhiteLoonBrush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "fehér.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush BlueLoonBrush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "kék.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush YellowLoonBrush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "sárga.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush GreenLoonBrush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "zöld.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush RedLoonBrush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "prios.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush BlackLoonBrush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "fekete.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush SpaceBrush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "fekete_háttérsssd.jpg"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush DesertBrush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "homok2.jpg"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush PlanetBrush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "bolygossokadik.jpg"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush TurretBrush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "Untitled-3.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush BlueLaserBrush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "lézer_kék.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush RedLaserBrush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "lézer_prios.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush YellowLaserBrush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "lézer_sárga.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush GreenLaserBrush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "lézer_zöld.png"), UriKind.RelativeOrAbsolute)));
            }
        }


        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            if (area.Width > 0 && area.Height > 0 && model != null)
            {
                drawingContext.DrawRectangle(SpaceBrush, null, new Rect(0, 0, area.Width, area.Height));
                drawingContext.DrawRectangle(Brushes.Red, null, new Rect(0, area.Height / 10 * 9, area.Width, area.Height / 10));


                foreach (var item in model.Lasers)
                {
                    int rn = r.Next(0, 4);
                    if (rn == 0)
                    {
                        drawingContext.DrawEllipse(BlueLaserBrush, null, new Point(item.Pos.X, item.Pos.Y), 8, 12);
                    }
                    else if (rn == 1)
                    {
                        drawingContext.DrawEllipse(RedLaserBrush, null, new Point(item.Pos.X, item.Pos.Y), 8, 12);
                    }
                    else if (rn == 2)
                    {
                        drawingContext.DrawEllipse(YellowLaserBrush, null, new Point(item.Pos.X, item.Pos.Y), 8, 12);
                    }
                    else
                    {
                        drawingContext.DrawEllipse(GreenLaserBrush, null, new Point(item.Pos.X, item.Pos.Y), 8, 12);
                    }

                }
                foreach (var item in model.Baloons)
                {
                    if (item.Type == 1)
                    {
                        drawingContext.DrawEllipse(WhiteLoonBrush, null, new Point(item.Pos.X, item.Pos.Y), 40, 40);

                    }
                    else if (item.Type == 2)
                    {
                        drawingContext.DrawEllipse(BlueLoonBrush, null, new Point(item.Pos.X, item.Pos.Y), 40, 40);
                    }
                    else if (item.Type == 3)
                    {
                        drawingContext.DrawEllipse(YellowLoonBrush, null, new Point(item.Pos.X, item.Pos.Y), 40, 40);
                    }
                    else if (item.Type == 4)
                    {
                        drawingContext.DrawEllipse(GreenLoonBrush, null, new Point(item.Pos.X, item.Pos.Y), 40, 40);
                    }
                    else if (item.Type == 5)
                    {
                        drawingContext.DrawEllipse(RedLoonBrush, null, new Point(item.Pos.X, item.Pos.Y), 40, 40);
                    }
                    else
                    {
                        drawingContext.DrawEllipse(BlackLoonBrush, null, new Point(item.Pos.X, item.Pos.Y), 40, 40);
                    }
                }
                drawingContext.DrawRectangle(TurretBrush, null, new Rect(area.Width / 2 - 50, area.Height / 10 * 8, 100, 100));


            }
        }
    }
}
