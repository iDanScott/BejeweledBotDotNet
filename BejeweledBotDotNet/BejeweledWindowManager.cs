﻿using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace DotNetBejewelledBot
{
    class BejeweledWindowManager
    {
        private Rectangle m_Window;
        private Bitmap m_ScreenShot;
        private Bitmap m_BejeweledImage;
        private Bitmap m_ColourGrid;
        private Color[,] m_ColorMatrix;

        public Bitmap ScreenShot
        {
            get
            {
                return (Bitmap)m_BejeweledImage;
            }
        }

        public Bitmap ColourGrid
        {
            get
            {
                return m_ColourGrid;
            }
        }

        public Rectangle PlayingWindow
        {
            get
            {
                return m_Window;
            }
        }

        public BejeweledWindowManager()
        {
            m_Window = new Rectangle(new Point(0, 0), new Size(320, 320));
            m_ColorMatrix = new Color[8, 8];
            GetScreenshot();   
            GetColourGrid();
        }

        public void GetScreenshot()
        {
            m_ScreenShot = new Bitmap(Screen.PrimaryScreen.Bounds.Width,
                                       Screen.PrimaryScreen.Bounds.Height,
                                       PixelFormat.Format32bppArgb);

            using (Graphics gfxScreenshot = Graphics.FromImage(m_ScreenShot))
            {

                gfxScreenshot.CopyFromScreen(Screen.PrimaryScreen.Bounds.X,
                                             Screen.PrimaryScreen.Bounds.Y,
                                             0,
                                             0,
                                             Screen.PrimaryScreen.Bounds.Size,
                                             CopyPixelOperation.SourceCopy);
            }

            m_BejeweledImage = m_ScreenShot.Clone(m_Window, PixelFormat.Format32bppArgb);
        }

        public void CalculateMoves()
        {
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    Thread.Sleep(1);
                    if (BejeweledColor.Collection.Contains(m_ColorMatrix[x, y]))
                    {
                        if (((x <= 4) && (m_ColorMatrix[x, y] == m_ColorMatrix[x + 2, y]) &&
                            (m_ColorMatrix[x, y] == m_ColorMatrix[x + 3, y])) ||
                            ((x <= 6) && (y >= 1) && (y <= 6) && (m_ColorMatrix[x + 1, y - 1] == m_ColorMatrix[x, y]) &&
                            (m_ColorMatrix[x + 1, y + 1] == m_ColorMatrix[x, y])) ||
                            ((y <= 5) && (x <= 6) && (m_ColorMatrix[x + 1, y + 1] == m_ColorMatrix[x, y]) &&
                            (m_ColorMatrix[x + 1, y + 2] == m_ColorMatrix[x, y])) ||
                            ((y >= 2) && (x <= 6) && (m_ColorMatrix[x + 1, y - 1] == m_ColorMatrix[x, y]) &&
                            (m_ColorMatrix[x + 1, y - 2] == m_ColorMatrix[x, y])))
                        {
                            // Move Gem Right
                            WinAPI.SetCursorPos(m_Window.Left + (x * 40) + 20, m_Window.Top + (y * 40) + 20);
                            WinAPI.DoMouseClick();
                            Thread.Sleep(5);
                            WinAPI.SetCursorPos(m_Window.Left + (x * 40) + 60, m_Window.Top + (y * 40) + 20);
                            WinAPI.DoMouseClick();
                        }
                        else if (((x >= 3) && (m_ColorMatrix[x, y] == m_ColorMatrix[x - 2, y]) &&
                                 (m_ColorMatrix[x, y] == m_ColorMatrix[x - 3, y])) ||
                                 ((x >= 1) && (y >= 1) && (m_ColorMatrix[x - 1, y - 1] == m_ColorMatrix[x, y]) &&
                                 (y <= 6) && (m_ColorMatrix[x - 1, y + 1] == m_ColorMatrix[x, y])) ||
                                 ((x >= 1) && (y <= 5) && (m_ColorMatrix[x - 1, y + 1] == m_ColorMatrix[x, y]) &&
                                 (m_ColorMatrix[x - 1, y + 2] == m_ColorMatrix[x, y])) ||
                                 ((x >= 1) && (y >= 2) && (m_ColorMatrix[x - 1, y - 1] == m_ColorMatrix[x, y]) &&
                                  (m_ColorMatrix[x - 1, y - 2] == m_ColorMatrix[x, y])))
                        {
                            // Move gem left
                            WinAPI.SetCursorPos(m_Window.Left + (x * 40) + 20, m_Window.Top + (y * 40) + 20);
                            WinAPI.DoMouseClick();
                            Thread.Sleep(5);
                            WinAPI.SetCursorPos(m_Window.Left + (x * 40) - 20, m_Window.Top + (y * 40) + 20);
                            WinAPI.DoMouseClick();
                        }
                        else if (((y >= 3) && (m_ColorMatrix[x, y] == m_ColorMatrix[x, y - 2]) &&
                                 (m_ColorMatrix[x, y] == m_ColorMatrix[x, y - 3])) ||

                                 ((y >= 1) && (((x >= 1) && (m_ColorMatrix[x - 1, y - 1] == m_ColorMatrix[x, y]) &&
                                  (x <= 6) && (m_ColorMatrix[x + 1, y - 1] == m_ColorMatrix[x, y])) ||

                                 ((y >= 1) && (x >= 2) && (m_ColorMatrix[x - 1, y - 1] == m_ColorMatrix[x, y]) &&
                                  (m_ColorMatrix[x - 2, y - 1] == m_ColorMatrix[x, y])) ||

                                 ((y >= 1) && (x <= 5) && (m_ColorMatrix[x + 1, y - 1] == m_ColorMatrix[x, y]) &&
                                  (m_ColorMatrix[x + 2, y - 1] == m_ColorMatrix[x, y])))))
                        {
                            // Move gem up
                            WinAPI.SetCursorPos(m_Window.Left + (x * 40) + 20, m_Window.Top + (y * 40) + 20);
                            WinAPI.DoMouseClick();
                            Thread.Sleep(5);
                            WinAPI.SetCursorPos(m_Window.Left + (x * 40) + 20, m_Window.Top + (y * 40) - 20);
                            WinAPI.DoMouseClick();
                        }
                        else if (((y <= 6) && (x <=5) && (m_ColorMatrix[x, y] == m_ColorMatrix[x + 1, y + 1] && 
                                 (m_ColorMatrix[x, y] == m_ColorMatrix[x + 2, y + 1]))) || 
                                 ((x >= 2) && (y <= 6) && (m_ColorMatrix[x, y] == m_ColorMatrix[x - 1, y + 1] &&
                                  (m_ColorMatrix[x, y] == m_ColorMatrix[x - 2, y + 1]))) ||
                                 ((y <= 4) && (m_ColorMatrix[x, y + 2] == m_ColorMatrix[x, y]) && 
                                   (m_ColorMatrix[x, y + 3] == m_ColorMatrix[x, y])) || 
                                 ((x >= 1) && (x <= 6) && (y <= 6) && (m_ColorMatrix[x, y] == m_ColorMatrix[x + 1, y + 1]) &&
                                   (m_ColorMatrix[x, y] == m_ColorMatrix[x - 1, y + 1])))
                        {
                            // Move gem down
                            WinAPI.SetCursorPos(m_Window.Left + (x * 40) + 20, m_Window.Top + (y * 40) + 20);
                            WinAPI.DoMouseClick();
                            Thread.Sleep(5);
                            WinAPI.SetCursorPos(m_Window.Left + (x * 40) + 20, m_Window.Top + (y * 40) + 60);
                            WinAPI.DoMouseClick();
                        }
                    }
                }
            }
        }

        public void GetColourGrid()
        {
            m_ColourGrid = new Bitmap(m_Window.Width,
                                      m_Window.Height,
                                      PixelFormat.Format32bppArgb);

            using (Graphics gfxColourgrid = Graphics.FromImage(m_ColourGrid))
            {

                for (int x = 0; x < m_BejeweledImage.Size.Width; x += 40)
                {
                    for (int y = 0; y < m_BejeweledImage.Size.Height; y += 40)
                    {
                        Color pixel = m_BejeweledImage.GetPixel(x + 20, y + 22);
                        if (pixel.R > 145 && pixel.G > 145 && pixel.B > 145)
                        {
                            m_ColorMatrix[x / 40, y / 40] = BejeweledColor.White;
                            gfxColourgrid.FillRectangle(new SolidBrush(BejeweledColor.White), new Rectangle(x, y, 40, 40));
                        }
                        else if (pixel.R > 100 && pixel.G > 100 && pixel.B < 100)
                        {
                            m_ColorMatrix[x / 40, y / 40] = BejeweledColor.Yellow;
                            gfxColourgrid.FillRectangle(new SolidBrush(BejeweledColor.Yellow), new Rectangle(x, y, 40, 40));
                        }
                        else if (pixel.R > 90 && pixel.G < 60 && pixel.B > 90)
                        {
                            m_ColorMatrix[x / 40, y / 40] = BejeweledColor.Purple;
                            gfxColourgrid.FillRectangle(new SolidBrush(BejeweledColor.Purple), new Rectangle(x, y, 40, 40));
                        }
                        else if (pixel.R > 100 && pixel.G > 50 && pixel.G < 170 && pixel.B < 110)
                        {
                            m_ColorMatrix[x / 40, y / 40] = BejeweledColor.Orange;
                            gfxColourgrid.FillRectangle(new SolidBrush(BejeweledColor.Orange), new Rectangle(x, y, 40, 40));
                        }
                        else if (pixel.R < 80 && pixel.R > 15 && pixel.G < 150 && pixel.G > 60 && pixel.B > 100)
                        {
                            m_ColorMatrix[x / 40, y / 40] = BejeweledColor.Blue;
                            gfxColourgrid.FillRectangle(new SolidBrush(BejeweledColor.Blue), new Rectangle(x, y, 40, 40));
                        }
                        else if (pixel.R > 200 && pixel.G < 60 && pixel.B < 60)
                        {
                            m_ColorMatrix[x / 40, y / 40] = BejeweledColor.Red;
                            gfxColourgrid.FillRectangle(new SolidBrush(BejeweledColor.Red), new Rectangle(x, y, 40, 40));
                        }
                        else
                        {
                            m_ColorMatrix[x / 40, y / 40] = pixel;
                            gfxColourgrid.FillRectangle(new SolidBrush(pixel), new Rectangle(x, y, 40, 40));
                        }
                    }
                }
            }
        }

        public bool Calibrate()
        {
            Point Location = new Point();

            for (int x = 0; x < m_ScreenShot.Size.Width; x++)
            {
                for (int y = 0; y < m_ScreenShot.Size.Height; y++)
                {
                    if (Location == Point.Empty)
                    {
                        if (m_ScreenShot.GetPixel(x, y) == Color.FromArgb(255, 39, 19, 5))
                        {
                            Location = new Point(x, y);
                            break;
                        }
                    }
                }
                if (Location != Point.Empty)
                    break;
            }

            if (Location != Point.Empty)
            {
                m_Window = new Rectangle(Location, m_Window.Size);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
