using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Telerik.Web.UI;

namespace Telerik.Charting.Styles
{
	// Token: 0x020017C0 RID: 6080
	internal class ShadowManager
	{
		// Token: 0x0600ECA2 RID: 60578 RVA: 0x0035F450 File Offset: 0x0035D650
		public static void DrawLineShadow(ChartGraphics graphics, Pen pen, PointF[] points, int lineType, int lineWidth, int pa_width, int pa_height, int shadowDistance, Color shadowColor, float shadowBlur, ShadowPosition shadowPosition)
		{
			GraphicsPath graphicsPath = new GraphicsPath();
			switch (lineType)
			{
			case 0:
				graphicsPath.AddLines(points);
				break;
			case 1:
				graphicsPath.AddBeziers(points);
				break;
			case 2:
				graphicsPath.AddCurve(points);
				break;
			}
			Image image = ShadowManager.DrawShadow(graphicsPath, new SolidBrush(shadowColor), pen, (float)(lineWidth + shadowDistance), shadowBlur, new Size(pa_width, pa_height), DrawType.Line);
			graphics.Graphics.DrawImage(image, ShadowManager.SetShadowPosition(shadowPosition, new PointF(0f, 0f), (float)shadowDistance));
			image.Dispose();
		}

		// Token: 0x0600ECA3 RID: 60579 RVA: 0x0035F4E0 File Offset: 0x0035D6E0
		public static void DrawLineShadow(ChartGraphics graphics, Pen pen, GraphicsPath path, int lineWidth, int pa_width, int pa_height, int shadowDistance, Color shadowColor, float shadowBlur, ShadowPosition shadowPosition)
		{
			Image image = ShadowManager.DrawShadow(path, new SolidBrush(shadowColor), pen, (float)(lineWidth + shadowDistance), shadowBlur, new Size(pa_width, pa_height), DrawType.Line);
			graphics.Graphics.DrawImage(image, ShadowManager.SetShadowPosition(shadowPosition, new PointF(0f, 0f), (float)shadowDistance));
			image.Dispose();
		}

		// Token: 0x0600ECA4 RID: 60580 RVA: 0x0035F538 File Offset: 0x0035D738
		public static PointF SetShadowPosition(ShadowPosition position, PointF point, float shadowDistance)
		{
			PointF result = default(PointF);
			float num = (float)((double)shadowDistance * Math.Sin(0.7853981633974483));
			switch (position)
			{
			case ShadowPosition.Right:
				result.X = point.X + shadowDistance;
				result.Y = point.Y;
				break;
			case ShadowPosition.Left:
				result.X = point.X - shadowDistance;
				result.Y = point.Y;
				break;
			case ShadowPosition.Top:
				result.X = point.X;
				result.Y = point.Y - shadowDistance;
				break;
			case ShadowPosition.Bottom:
				result.X = point.X;
				result.Y = point.Y + shadowDistance;
				break;
			case ShadowPosition.TopRight:
				result.X = point.X + num;
				result.Y = point.Y - num;
				break;
			case ShadowPosition.TopLeft:
				result.X = point.X - num;
				result.Y = point.Y - num;
				break;
			case ShadowPosition.BottomRight:
				result.X = point.X + num;
				result.Y = point.Y + num;
				break;
			case ShadowPosition.BottomLeft:
				result.X = point.X - num;
				result.Y = point.Y + num;
				break;
			case ShadowPosition.Behind:
				result = point;
				break;
			}
			return result;
		}

		// Token: 0x0600ECA5 RID: 60581 RVA: 0x0035F6AC File Offset: 0x0035D8AC
		public static void DrawPolygonShadow(ChartSeries chartSeries, GraphicsPath grPath, ChartGraphics graphics, int width, int height)
		{
			if (chartSeries.Appearance.Shadow.Distance != 0f)
			{
				Image image = ShadowManager.DrawShadow(grPath, new SolidBrush(chartSeries.Appearance.Shadow.Color), new Pen(chartSeries.Appearance.Shadow.Color), chartSeries.Appearance.Shadow.Distance, chartSeries.Appearance.Shadow.Blur, new Size(width, height), DrawType.Fill);
				graphics.Graphics.DrawImage(image, ShadowManager.SetShadowPosition(chartSeries.Appearance.Shadow.Position, new Point(0, 0), chartSeries.Appearance.Shadow.Distance));
				image.Dispose();
			}
		}

		// Token: 0x0600ECA6 RID: 60582 RVA: 0x0035F770 File Offset: 0x0035D970
		public static void DrawPolygonShadow(ChartSeries chartSeries, PointF[] points, ChartGraphics graphics, int width, int height)
		{
			if (chartSeries.Appearance.Shadow.Distance != 0f && points.Length > 0)
			{
				using (GraphicsPath graphicsPath = new GraphicsPath())
				{
					graphicsPath.AddPolygon(points);
					Image image = ShadowManager.DrawShadow(graphicsPath, new SolidBrush(chartSeries.Appearance.Shadow.Color), new Pen(chartSeries.Appearance.Shadow.Color), chartSeries.Appearance.Shadow.Distance, chartSeries.Appearance.Shadow.Blur, new Size(width, height), DrawType.Fill);
					graphics.Graphics.DrawImage(image, ShadowManager.SetShadowPosition(chartSeries.Appearance.Shadow.Position, new Point(0, 0), chartSeries.Appearance.Shadow.Distance));
					image.Dispose();
				}
			}
		}

		// Token: 0x0600ECA7 RID: 60583 RVA: 0x0035F868 File Offset: 0x0035DA68
		public static void DrawPolygonShadow(GraphicsPath grPath, ChartGraphics graphics, int width, int height, int shadowDistance, Color shadowColor, float shadowBlur, ShadowPosition shadowPosition)
		{
			if (shadowDistance != 0)
			{
				Image image = ShadowManager.DrawShadow(grPath, new SolidBrush(shadowColor), new Pen(shadowColor), (float)shadowDistance, shadowBlur, new Size(width, height), DrawType.Fill);
				graphics.Graphics.DrawImage(image, ShadowManager.SetShadowPosition(shadowPosition, new Point(0, 0), (float)shadowDistance));
				image.Dispose();
			}
		}

		// Token: 0x0600ECA8 RID: 60584 RVA: 0x0035F8C4 File Offset: 0x0035DAC4
		public static void DrawPolygonShadow(PointF[] points, ChartGraphics graphics, int width, int height, int shadowDistance, Color shadowColor, float shadowBlur, ShadowPosition shadowPosition)
		{
			if (shadowDistance != 0 && points.Length > 0)
			{
				using (GraphicsPath graphicsPath = new GraphicsPath())
				{
					graphicsPath.AddPolygon(points);
					Image image = ShadowManager.DrawShadow(graphicsPath, new SolidBrush(shadowColor), new Pen(shadowColor), (float)shadowDistance, shadowBlur, new Size(width, height), DrawType.Fill);
					graphics.Graphics.DrawImage(image, ShadowManager.SetShadowPosition(shadowPosition, new Point(0, 0), (float)shadowDistance));
					image.Dispose();
				}
			}
		}

		// Token: 0x0600ECA9 RID: 60585 RVA: 0x0035F950 File Offset: 0x0035DB50
		private static Image DrawShadow(GraphicsPath Path, Brush Brush, Pen Pen, float Distance, float BlurCoef, Size ShadowImageSize, DrawType DrawType)
		{
			Bitmap bitmap = new Bitmap(ShadowImageSize.Width, ShadowImageSize.Height, PixelFormat.Format32bppArgb);
			Graphics graphics = Graphics.FromImage(bitmap);
			graphics.CompositingMode = CompositingMode.SourceCopy;
			graphics.SmoothingMode = SmoothingMode.HighQuality;
			Pen.Width = Distance * 0.7f;
			Pen.Alignment = PenAlignment.Outset;
			Pen.LineJoin = LineJoin.Round;
			graphics.DrawPath(Pen, Path);
			graphics.SmoothingMode = SmoothingMode.None;
			Image result = bitmap;
			switch (DrawType)
			{
			case DrawType.Line:
				graphics.DrawPath(Pen, Path);
				break;
			case DrawType.Fill:
				graphics.FillPath(Brush, Path);
				break;
			case DrawType.LineAndFill:
				graphics.DrawPath(Pen, Path);
				graphics.FillPath(Brush, Path);
				break;
			}
			if (BlurCoef != 0f)
			{
				RectangleF bounds = Path.GetBounds(new Matrix(), Pen);
				int num = Math.Max((int)(bounds.Left - BlurCoef), 0);
				int num2 = Math.Max((int)(bounds.Top - BlurCoef), 0);
				int num3 = Math.Min((int)(bounds.Width + BlurCoef * 2f), ShadowImageSize.Width);
				int num4 = Math.Min((int)(bounds.Height + BlurCoef * 2f), ShadowImageSize.Height);
				num3 = ((num + num3 < ShadowImageSize.Width) ? num3 : (ShadowImageSize.Width - num));
				num4 = ((num2 + num4 < ShadowImageSize.Height) ? num4 : (ShadowImageSize.Height - num2));
				Rectangle rect = new Rectangle(num, num2, num3, num4);
				result = ShadowManager.Blur(bitmap, Convert.ToInt32(BlurCoef), rect);
			}
			return result;
		}

		// Token: 0x0600ECAA RID: 60586 RVA: 0x0035FACD File Offset: 0x0035DCCD
		private static BColor[][] GetArrayFromImageManaged(Bitmap source, int wi, int hi)
		{
			return BColor.GetMatrix(source, wi, hi);
		}

		// Token: 0x0600ECAB RID: 60587 RVA: 0x0035FAD8 File Offset: 0x0035DCD8
		private static BColor[][] GetArrayFromImageUnManaged(BitmapData bmpData, int wi, int hi)
		{
			IntPtr scan = bmpData.Scan0;
			int num = wi * hi * 4;
			byte[] array = new byte[num];
			Marshal.Copy(scan, array, 0, num);
			return BColor.GetMatrix(array, wi, hi);
		}

		// Token: 0x0600ECAC RID: 60588 RVA: 0x0035FB0A File Offset: 0x0035DD0A
		private static BColor[][] GetArrayFromImage(Bitmap source, BitmapData bmpData, int wi, int hi, bool isGranted)
		{
			if (isGranted)
			{
				return ShadowManager.GetArrayFromImageUnManaged(bmpData, wi, hi);
			}
			source.UnlockBits(bmpData);
			return ShadowManager.GetArrayFromImageManaged(source, wi, hi);
		}

		// Token: 0x0600ECAD RID: 60589 RVA: 0x0035FB28 File Offset: 0x0035DD28
		private static void UpdateImageFromArray(Bitmap source, BColor[][] src, int top, int height, int left, int width, BColor[] dst, int srcWidth, int srcHeight, BitmapData bmpData, bool isGranted)
		{
			if (isGranted)
			{
				ShadowManager.UpdateImageFromArrayUnManaged(source, src, top, height, left, width, dst, srcWidth, srcHeight, bmpData);
				return;
			}
			ShadowManager.UpdateImageFromArrayManaged(source, src, top, height, left, width, dst, srcWidth, srcHeight);
		}

		// Token: 0x0600ECAE RID: 60590 RVA: 0x0035FB64 File Offset: 0x0035DD64
		private static void UpdateImageFromArrayManaged(Bitmap source, BColor[][] src, int top, int height, int left, int width, BColor[] dst, int srcWidth, int srcHeight)
		{
			ShadowManager.SetMatrix(source, src, top, height, left, width, dst, srcWidth, srcHeight);
		}

		// Token: 0x0600ECAF RID: 60591 RVA: 0x0035FB84 File Offset: 0x0035DD84
		private static void UpdateImageFromArrayUnManaged(Bitmap source, BColor[][] src, int top, int height, int left, int width, BColor[] dst, int srcWidth, int srcHeight, BitmapData bmpData)
		{
			IntPtr scan = bmpData.Scan0;
			int length = srcWidth * srcHeight * 4;
			byte[] asLine = BColor.GetAsLine(src, top, height, left, width, dst, srcWidth, srcHeight);
			Marshal.Copy(asLine, 0, scan, length);
			source.UnlockBits(bmpData);
		}

		// Token: 0x0600ECB0 RID: 60592 RVA: 0x0035FBC8 File Offset: 0x0035DDC8
		private static Image Blur(Bitmap source, int blurCoefficient, Rectangle rect)
		{
			int[] array = ShadowManager.GBlurRow(blurCoefficient);
			int num = array.Length;
			int width = source.Width;
			int height = source.Height;
			int top = rect.Top;
			int bottom = rect.Bottom;
			int right = rect.Right;
			int left = rect.Left;
			int num2 = 0;
			int num3 = left - blurCoefficient;
			BColor bcolor = BColor.CreateInstance();
			bool isGranted = SecurityHelper.IsPermissionGranted(new SecurityPermission(SecurityPermissionFlag.UnmanagedCode));
			BitmapData bitmapData = source.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
			BColor[][] arrayFromImage = ShadowManager.GetArrayFromImage(source, bitmapData, width, height, isGranted);
			BColor[] rectAsLine = BColor.GetRectAsLine(arrayFromImage, top, rect.Height, left, rect.Width);
			if (rectAsLine.Length <= 0)
			{
				source.UnlockBits(bitmapData);
				return source;
			}
			for (int i = top; i < bottom; i++)
			{
				long[] array2 = new long[num];
				long[] array3 = new long[num];
				long[] array4 = new long[num];
				long[] array5 = new long[num];
				long[] array6 = new long[num];
				long[] array7 = new long[num];
				long num4 = 0L;
				long num5 = 0L;
				long num6 = 0L;
				long num7 = 0L;
				long num8 = 0L;
				long num9 = 0L;
				int num10 = num - 1;
				int num11 = i - blurCoefficient;
				for (int j = 0; j < num; j++)
				{
					int num12 = num3 + j;
					array2[j] = 0L;
					array3[j] = 0L;
					array4[j] = 0L;
					array5[j] = 0L;
					array6[j] = 0L;
					array7[j] = 0L;
					if (num12 >= 0 && num12 < width)
					{
						for (int k = 0; k < num10; k++)
						{
							int num13 = num11 + k;
							if (num13 >= 0 && num13 < height)
							{
								BColor bcolor2 = arrayFromImage[num12][num13];
								int num14 = array[k];
								array2[j] += (long)num14;
								num14 *= (int)bcolor2.A + (bcolor2.A >> 7);
								array3[j] += (long)num14;
								num14 >>= 8;
								array4[j] += (long)(num14 * (int)bcolor2.R);
								array5[j] += (long)(num14 * (int)bcolor2.G);
								array6[j] += (long)(num14 * (int)bcolor2.B);
								array7[j] += (long)(num14 * (int)bcolor2.A);
							}
							k++;
							num13 = num11 + k;
							if (num13 >= 0 && num13 < height)
							{
								BColor bcolor3 = arrayFromImage[num12][num13];
								int num15 = array[k];
								array2[j] += (long)num15;
								num15 *= (int)bcolor3.A + (bcolor3.A >> 7);
								array3[j] += (long)num15;
								num15 >>= 8;
								array4[j] += (long)(num15 * (int)bcolor3.R);
								array5[j] += (long)(num15 * (int)bcolor3.G);
								array6[j] += (long)(num15 * (int)bcolor3.B);
								array7[j] += (long)(num15 * (int)bcolor3.A);
							}
						}
						int num16 = array[j];
						num4 += (long)num16 * array2[j];
						num5 += (long)num16 * array3[j];
						num6 += (long)num16 * array4[j];
						num7 += (long)num16 * array5[j];
						num8 += (long)num16 * array6[j];
						num9 += (long)num16 * array7[j];
					}
				}
				num5 >>= 8;
				if (num4 == 0L || num5 == 0L)
				{
					bcolor.A = (bcolor.R = (bcolor.G = (bcolor.B = 0)));
					rectAsLine[num2] = bcolor;
				}
				else
				{
					bcolor.R = (byte)(num6 / num5);
					bcolor.G = (byte)(num7 / num5);
					bcolor.B = (byte)(num8 / num5);
					bcolor.A = (byte)(num9 / num4);
					rectAsLine[num2] = bcolor;
				}
				num2++;
				for (int l = left + 1; l < right; l++)
				{
					for (int m = 0; m < num10; m++)
					{
						int num17 = m + 1;
						array2[m] = array2[num17];
						array3[m] = array3[num17];
						array4[m] = array4[num17];
						array5[m] = array5[num17];
						array6[m] = array6[num17];
						array7[m] = array7[num17];
					}
					num4 = 0L;
					num5 = 0L;
					num6 = 0L;
					num7 = 0L;
					num8 = 0L;
					num9 = 0L;
					int n;
					for (n = 0; n < num10; n++)
					{
						long num18 = (long)array[n];
						num4 += num18 * array2[n];
						num5 += num18 * array3[n];
						num6 += num18 * array4[n];
						num7 += num18 * array5[n];
						num8 += num18 * array6[n];
						num9 += num18 * array7[n];
					}
					n = num - 1;
					array2[n] = 0L;
					array3[n] = 0L;
					array4[n] = 0L;
					array5[n] = 0L;
					array6[n] = 0L;
					array7[n] = 0L;
					int num19 = l + n - blurCoefficient;
					if (num19 >= 0 && num19 < width)
					{
						for (int num20 = 0; num20 < num; num20++)
						{
							int num21 = num11 + num20;
							if (num21 >= 0 && num21 < height)
							{
								BColor bcolor4 = arrayFromImage[num19][num21];
								int num22 = array[num20];
								array2[n] += (long)num22;
								num22 *= (int)bcolor4.A + (bcolor4.A >> 7);
								array3[n] += (long)num22;
								num22 >>= 8;
								array4[n] += (long)num22 * (long)((ulong)bcolor4.R);
								array5[n] += (long)num22 * (long)((ulong)bcolor4.G);
								array6[n] += (long)num22 * (long)((ulong)bcolor4.B);
								array7[n] += (long)num22 * (long)((ulong)bcolor4.A);
							}
						}
						int num23 = array[n];
						num4 += (long)num23 * array2[n];
						num5 += (long)num23 * array3[n];
						num6 += (long)num23 * array4[n];
						num7 += (long)num23 * array5[n];
						num8 += (long)num23 * array6[n];
						num9 += (long)num23 * array7[n];
					}
					num5 >>= 8;
					if (num4 == 0L || num5 == 0L)
					{
						bcolor.A = (bcolor.R = (bcolor.G = (bcolor.B = 0)));
						rectAsLine[num2] = bcolor;
					}
					else
					{
						bcolor.R = (byte)(num6 / num5);
						bcolor.G = (byte)(num7 / num5);
						bcolor.B = (byte)(num8 / num5);
						bcolor.A = (byte)(num9 / num4);
						rectAsLine[num2] = bcolor;
					}
					num2++;
				}
			}
			ShadowManager.UpdateImageFromArray(source, arrayFromImage, top, rect.Height, left, rect.Width, rectAsLine, width, height, bitmapData, isGranted);
			return source;
		}

		// Token: 0x0600ECB1 RID: 60593 RVA: 0x003603AC File Offset: 0x0035E5AC
		private static int[] GBlurRow(int count)
		{
			int num = 1 + count * 2;
			int[] array = new int[num];
			int num2 = num - 1;
			for (int i = 0; i <= count; i++)
			{
				array[i] = 16 * (i + 1);
				array[num2 - i] = array[i];
			}
			return array;
		}

		// Token: 0x0600ECB2 RID: 60594 RVA: 0x003603E8 File Offset: 0x0035E5E8
		private static void SetMatrix(Bitmap source, BColor[][] src, int top, int height, int left, int width, BColor[] dst, int srcWidth, int srcHeight)
		{
			int num = 0;
			int num2 = top + height;
			int num3 = left + width;
			for (int i = top; i < num2; i++)
			{
				for (int j = left; j < num3; j++)
				{
					src[j][i] = dst[num++];
				}
			}
			for (int k = 0; k < srcWidth; k++)
			{
				for (int l = 0; l < srcHeight; l++)
				{
					source.SetPixel(k, l, Color.FromArgb((int)src[k][l].A, (int)src[k][l].R, (int)src[k][l].G, (int)src[k][l].B));
				}
			}
		}
	}
}
