using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Internal;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Windows.Forms.Internal;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms
{
	// Token: 0x02000170 RID: 368
	public sealed class ControlPaint
	{
		// Token: 0x06001323 RID: 4899 RVA: 0x00002843 File Offset: 0x00000A43
		private ControlPaint()
		{
		}

		// Token: 0x06001324 RID: 4900 RVA: 0x0003D394 File Offset: 0x0003B594
		internal static Rectangle CalculateBackgroundImageRectangle(Rectangle bounds, Image backgroundImage, ImageLayout imageLayout)
		{
			Rectangle result = bounds;
			if (backgroundImage != null)
			{
				switch (imageLayout)
				{
				case ImageLayout.None:
					result.Size = backgroundImage.Size;
					break;
				case ImageLayout.Center:
				{
					result.Size = backgroundImage.Size;
					Size size = bounds.Size;
					if (size.Width > result.Width)
					{
						result.X = (size.Width - result.Width) / 2;
					}
					if (size.Height > result.Height)
					{
						result.Y = (size.Height - result.Height) / 2;
					}
					break;
				}
				case ImageLayout.Stretch:
					result.Size = bounds.Size;
					break;
				case ImageLayout.Zoom:
				{
					Size size2 = backgroundImage.Size;
					float num = (float)bounds.Width / (float)size2.Width;
					float num2 = (float)bounds.Height / (float)size2.Height;
					if (num < num2)
					{
						result.Width = bounds.Width;
						result.Height = (int)((double)((float)size2.Height * num) + 0.5);
						if (bounds.Y >= 0)
						{
							result.Y = (bounds.Height - result.Height) / 2;
						}
					}
					else
					{
						result.Height = bounds.Height;
						result.Width = (int)((double)((float)size2.Width * num2) + 0.5);
						if (bounds.X >= 0)
						{
							result.X = (bounds.Width - result.Width) / 2;
						}
					}
					break;
				}
				}
			}
			return result;
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06001325 RID: 4901 RVA: 0x0003D529 File Offset: 0x0003B729
		public static Color ContrastControlDark
		{
			get
			{
				if (!SystemInformation.HighContrast)
				{
					return SystemColors.ControlDark;
				}
				return SystemColors.WindowFrame;
			}
		}

		// Token: 0x06001326 RID: 4902 RVA: 0x0003D540 File Offset: 0x0003B740
		private static IntPtr CreateBitmapInfo(Bitmap bitmap, IntPtr hdcS)
		{
			NativeMethods.BITMAPINFOHEADER bitmapinfoheader = new NativeMethods.BITMAPINFOHEADER();
			bitmapinfoheader.biSize = Marshal.SizeOf(bitmapinfoheader);
			bitmapinfoheader.biWidth = bitmap.Width;
			bitmapinfoheader.biHeight = bitmap.Height;
			bitmapinfoheader.biPlanes = 1;
			bitmapinfoheader.biBitCount = 16;
			bitmapinfoheader.biCompression = 0;
			int num = 0;
			IntPtr handle = SafeNativeMethods.CreateHalftonePalette(new HandleRef(null, hdcS));
			UnsafeNativeMethods.GetObject(new HandleRef(null, handle), 2, ref num);
			int[] array = new int[num];
			SafeNativeMethods.GetPaletteEntries(new HandleRef(null, handle), 0, num, array);
			int[] array2 = new int[num];
			for (int i = 0; i < num; i++)
			{
				int num2 = array[i];
				array2[i] = (num2 & -16777216) >> 6 + (num2 & 16711680) >> 4 + (num2 & 65280) >> 2;
			}
			SafeNativeMethods.DeleteObject(new HandleRef(null, handle));
			IntPtr intPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(bitmapinfoheader) + num * 4);
			Marshal.StructureToPtr(bitmapinfoheader, intPtr, false);
			Marshal.Copy(array2, 0, (IntPtr)((long)intPtr + (long)Marshal.SizeOf(bitmapinfoheader)), num);
			return intPtr;
		}

		// Token: 0x06001327 RID: 4903 RVA: 0x0003D654 File Offset: 0x0003B854
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public static IntPtr CreateHBitmap16Bit(Bitmap bitmap, Color background)
		{
			Size size = bitmap.Size;
			IntPtr intPtr2;
			using (DeviceContext screenDC = DeviceContext.ScreenDC)
			{
				IntPtr hdc = screenDC.Hdc;
				using (DeviceContext deviceContext = DeviceContext.FromCompatibleDC(hdc))
				{
					IntPtr hdc2 = deviceContext.Hdc;
					byte[] ppvBits = new byte[bitmap.Width * bitmap.Height];
					IntPtr intPtr = ControlPaint.CreateBitmapInfo(bitmap, hdc);
					intPtr2 = SafeNativeMethods.CreateDIBSection(new HandleRef(null, hdc), new HandleRef(null, intPtr), 0, ppvBits, IntPtr.Zero, 0);
					Marshal.FreeCoTaskMem(intPtr);
					if (intPtr2 == IntPtr.Zero)
					{
						throw new Win32Exception();
					}
					try
					{
						IntPtr intPtr3 = SafeNativeMethods.SelectObject(new HandleRef(null, hdc2), new HandleRef(null, intPtr2));
						if (intPtr3 == IntPtr.Zero)
						{
							throw new Win32Exception();
						}
						SafeNativeMethods.DeleteObject(new HandleRef(null, intPtr3));
						using (Graphics graphics = Graphics.FromHdcInternal(hdc2))
						{
							using (Brush brush = new SolidBrush(background))
							{
								graphics.FillRectangle(brush, 0, 0, size.Width, size.Height);
							}
							graphics.DrawImage(bitmap, 0, 0, size.Width, size.Height);
						}
					}
					catch
					{
						SafeNativeMethods.DeleteObject(new HandleRef(null, intPtr2));
						throw;
					}
				}
			}
			return intPtr2;
		}

		// Token: 0x06001328 RID: 4904 RVA: 0x0003D818 File Offset: 0x0003BA18
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public static IntPtr CreateHBitmapTransparencyMask(Bitmap bitmap)
		{
			if (bitmap == null)
			{
				throw new ArgumentNullException("bitmap");
			}
			Size size = bitmap.Size;
			int width = bitmap.Width;
			int height = bitmap.Height;
			int num = width / 8;
			if (width % 8 != 0)
			{
				num++;
			}
			if (num % 2 != 0)
			{
				num++;
			}
			byte[] array = new byte[num * height];
			BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
			for (int i = 0; i < height; i++)
			{
				IntPtr ptr = (IntPtr)((long)bitmapData.Scan0 + (long)(i * bitmapData.Stride));
				for (int j = 0; j < width; j++)
				{
					int num2 = Marshal.ReadInt32(ptr, j * 4);
					if (num2 >> 24 == 0)
					{
						int num3 = num * i + j / 8;
						byte[] array2 = array;
						int num4 = num3;
						array2[num4] |= (byte)(128 >> j % 8);
					}
				}
			}
			bitmap.UnlockBits(bitmapData);
			return SafeNativeMethods.CreateBitmap(size.Width, size.Height, 1, 1, array);
		}

		// Token: 0x06001329 RID: 4905 RVA: 0x0003D91C File Offset: 0x0003BB1C
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public static IntPtr CreateHBitmapColorMask(Bitmap bitmap, IntPtr monochromeMask)
		{
			Size size = bitmap.Size;
			IntPtr hbitmap = bitmap.GetHbitmap();
			IntPtr dc = UnsafeNativeMethods.GetDC(NativeMethods.NullHandleRef);
			IntPtr handle = UnsafeNativeMethods.CreateCompatibleDC(new HandleRef(null, dc));
			IntPtr handle2 = UnsafeNativeMethods.CreateCompatibleDC(new HandleRef(null, dc));
			UnsafeNativeMethods.ReleaseDC(NativeMethods.NullHandleRef, new HandleRef(null, dc));
			IntPtr handle3 = SafeNativeMethods.SelectObject(new HandleRef(null, handle), new HandleRef(null, monochromeMask));
			IntPtr handle4 = SafeNativeMethods.SelectObject(new HandleRef(null, handle2), new HandleRef(null, hbitmap));
			SafeNativeMethods.SetBkColor(new HandleRef(null, handle2), 16777215);
			SafeNativeMethods.SetTextColor(new HandleRef(null, handle2), 0);
			SafeNativeMethods.BitBlt(new HandleRef(null, handle2), 0, 0, size.Width, size.Height, new HandleRef(null, handle), 0, 0, 2229030);
			SafeNativeMethods.SelectObject(new HandleRef(null, handle), new HandleRef(null, handle3));
			SafeNativeMethods.SelectObject(new HandleRef(null, handle2), new HandleRef(null, handle4));
			UnsafeNativeMethods.DeleteCompatibleDC(new HandleRef(null, handle));
			UnsafeNativeMethods.DeleteCompatibleDC(new HandleRef(null, handle2));
			return System.Internal.HandleCollector.Add(hbitmap, NativeMethods.CommonHandles.GDI);
		}

		// Token: 0x0600132A RID: 4906 RVA: 0x0003DA3C File Offset: 0x0003BC3C
		internal static IntPtr CreateHalftoneHBRUSH()
		{
			short[] array = new short[8];
			for (int i = 0; i < 8; i++)
			{
				array[i] = (short)(21845 << (i & 1));
			}
			IntPtr intPtr = SafeNativeMethods.CreateBitmap(8, 8, 1, 1, array);
			IntPtr result = SafeNativeMethods.CreateBrushIndirect(new NativeMethods.LOGBRUSH
			{
				lbColor = ColorTranslator.ToWin32(Color.Black),
				lbStyle = 3,
				lbHatch = intPtr
			});
			SafeNativeMethods.DeleteObject(new HandleRef(null, intPtr));
			return result;
		}

		// Token: 0x0600132B RID: 4907 RVA: 0x0003DAB8 File Offset: 0x0003BCB8
		internal static void CopyPixels(IntPtr sourceHwnd, IDeviceContext targetDC, Point sourceLocation, Point destinationLocation, Size blockRegionSize, CopyPixelOperation copyPixelOperation)
		{
			int width = blockRegionSize.Width;
			int height = blockRegionSize.Height;
			DeviceContext deviceContext = DeviceContext.FromHwnd(sourceHwnd);
			HandleRef hDC = new HandleRef(null, targetDC.GetHdc());
			HandleRef hSrcDC = new HandleRef(null, deviceContext.Hdc);
			try
			{
				if (!SafeNativeMethods.BitBlt(hDC, destinationLocation.X, destinationLocation.Y, width, height, hSrcDC, sourceLocation.X, sourceLocation.Y, (int)copyPixelOperation))
				{
					throw new Win32Exception();
				}
			}
			finally
			{
				targetDC.ReleaseHdc();
				deviceContext.Dispose();
			}
		}

		// Token: 0x0600132C RID: 4908 RVA: 0x0003DB4C File Offset: 0x0003BD4C
		private static DashStyle BorderStyleToDashStyle(ButtonBorderStyle borderStyle)
		{
			switch (borderStyle)
			{
			case ButtonBorderStyle.Dotted:
				return DashStyle.Dot;
			case ButtonBorderStyle.Dashed:
				return DashStyle.Dash;
			case ButtonBorderStyle.Solid:
				return DashStyle.Solid;
			default:
				return DashStyle.Solid;
			}
		}

		// Token: 0x0600132D RID: 4909 RVA: 0x0003DB6C File Offset: 0x0003BD6C
		public static Color Dark(Color baseColor, float percOfDarkDark)
		{
			return new ControlPaint.HLSColor(baseColor).Darker(percOfDarkDark);
		}

		// Token: 0x0600132E RID: 4910 RVA: 0x0003DB88 File Offset: 0x0003BD88
		public static Color Dark(Color baseColor)
		{
			return new ControlPaint.HLSColor(baseColor).Darker(0.5f);
		}

		// Token: 0x0600132F RID: 4911 RVA: 0x0003DBA8 File Offset: 0x0003BDA8
		public static Color DarkDark(Color baseColor)
		{
			return new ControlPaint.HLSColor(baseColor).Darker(1f);
		}

		// Token: 0x06001330 RID: 4912 RVA: 0x0003DBC8 File Offset: 0x0003BDC8
		internal static bool IsDarker(Color c1, Color c2)
		{
			ControlPaint.HLSColor hlscolor = new ControlPaint.HLSColor(c1);
			ControlPaint.HLSColor hlscolor2 = new ControlPaint.HLSColor(c2);
			return hlscolor.Luminosity < hlscolor2.Luminosity;
		}

		// Token: 0x06001331 RID: 4913 RVA: 0x0003DBF5 File Offset: 0x0003BDF5
		internal static void PrintBorder(Graphics graphics, Rectangle bounds, BorderStyle style, Border3DStyle b3dStyle)
		{
			if (graphics == null)
			{
				throw new ArgumentNullException("graphics");
			}
			switch (style)
			{
			case BorderStyle.None:
				break;
			case BorderStyle.FixedSingle:
				ControlPaint.DrawBorder(graphics, bounds, Color.FromKnownColor(KnownColor.WindowFrame), ButtonBorderStyle.Solid);
				return;
			case BorderStyle.Fixed3D:
				ControlPaint.DrawBorder3D(graphics, bounds, b3dStyle);
				break;
			default:
				return;
			}
		}

		// Token: 0x06001332 RID: 4914 RVA: 0x0003DC30 File Offset: 0x0003BE30
		internal static void DrawBackgroundImage(Graphics g, Image backgroundImage, Color backColor, ImageLayout backgroundImageLayout, Rectangle bounds, Rectangle clipRect)
		{
			ControlPaint.DrawBackgroundImage(g, backgroundImage, backColor, backgroundImageLayout, bounds, clipRect, Point.Empty, RightToLeft.No);
		}

		// Token: 0x06001333 RID: 4915 RVA: 0x0003DC45 File Offset: 0x0003BE45
		internal static void DrawBackgroundImage(Graphics g, Image backgroundImage, Color backColor, ImageLayout backgroundImageLayout, Rectangle bounds, Rectangle clipRect, Point scrollOffset)
		{
			ControlPaint.DrawBackgroundImage(g, backgroundImage, backColor, backgroundImageLayout, bounds, clipRect, scrollOffset, RightToLeft.No);
		}

		// Token: 0x06001334 RID: 4916 RVA: 0x0003DC58 File Offset: 0x0003BE58
		internal static void DrawBackgroundImage(Graphics g, Image backgroundImage, Color backColor, ImageLayout backgroundImageLayout, Rectangle bounds, Rectangle clipRect, Point scrollOffset, RightToLeft rightToLeft)
		{
			if (g == null)
			{
				throw new ArgumentNullException("g");
			}
			if (backgroundImageLayout == ImageLayout.Tile)
			{
				using (TextureBrush textureBrush = new TextureBrush(backgroundImage, WrapMode.Tile))
				{
					if (scrollOffset != Point.Empty)
					{
						Matrix transform = textureBrush.Transform;
						transform.Translate((float)scrollOffset.X, (float)scrollOffset.Y);
						textureBrush.Transform = transform;
					}
					g.FillRectangle(textureBrush, clipRect);
					return;
				}
			}
			Rectangle rectangle = ControlPaint.CalculateBackgroundImageRectangle(bounds, backgroundImage, backgroundImageLayout);
			if (rightToLeft == RightToLeft.Yes && backgroundImageLayout == ImageLayout.None)
			{
				rectangle.X += clipRect.Width - rectangle.Width;
			}
			using (SolidBrush solidBrush = new SolidBrush(backColor))
			{
				g.FillRectangle(solidBrush, clipRect);
			}
			if (!clipRect.Contains(rectangle))
			{
				if (backgroundImageLayout == ImageLayout.Stretch || backgroundImageLayout == ImageLayout.Zoom)
				{
					rectangle.Intersect(clipRect);
					g.DrawImage(backgroundImage, rectangle);
					return;
				}
				if (backgroundImageLayout == ImageLayout.None)
				{
					rectangle.Offset(clipRect.Location);
					Rectangle destRect = rectangle;
					destRect.Intersect(clipRect);
					Rectangle rectangle2 = new Rectangle(Point.Empty, destRect.Size);
					g.DrawImage(backgroundImage, destRect, rectangle2.X, rectangle2.Y, rectangle2.Width, rectangle2.Height, GraphicsUnit.Pixel);
					return;
				}
				Rectangle destRect2 = rectangle;
				destRect2.Intersect(clipRect);
				Rectangle rectangle3 = new Rectangle(new Point(destRect2.X - rectangle.X, destRect2.Y - rectangle.Y), destRect2.Size);
				g.DrawImage(backgroundImage, destRect2, rectangle3.X, rectangle3.Y, rectangle3.Width, rectangle3.Height, GraphicsUnit.Pixel);
				return;
			}
			else
			{
				ImageAttributes imageAttributes = new ImageAttributes();
				imageAttributes.SetWrapMode(WrapMode.TileFlipXY);
				g.DrawImage(backgroundImage, rectangle, 0, 0, backgroundImage.Width, backgroundImage.Height, GraphicsUnit.Pixel, imageAttributes);
				imageAttributes.Dispose();
			}
		}

		// Token: 0x06001335 RID: 4917 RVA: 0x0003DE44 File Offset: 0x0003C044
		public static void DrawBorder(Graphics graphics, Rectangle bounds, Color color, ButtonBorderStyle style)
		{
			switch (style)
			{
			case ButtonBorderStyle.None:
				break;
			case ButtonBorderStyle.Dotted:
			case ButtonBorderStyle.Dashed:
			case ButtonBorderStyle.Solid:
				ControlPaint.DrawBorderSimple(graphics, bounds, color, style);
				return;
			case ButtonBorderStyle.Inset:
			case ButtonBorderStyle.Outset:
				ControlPaint.DrawBorderComplex(graphics, bounds, color, style);
				break;
			default:
				return;
			}
		}

		// Token: 0x06001336 RID: 4918 RVA: 0x0003DE78 File Offset: 0x0003C078
		public static void DrawBorder(Graphics graphics, Rectangle bounds, Color leftColor, int leftWidth, ButtonBorderStyle leftStyle, Color topColor, int topWidth, ButtonBorderStyle topStyle, Color rightColor, int rightWidth, ButtonBorderStyle rightStyle, Color bottomColor, int bottomWidth, ButtonBorderStyle bottomStyle)
		{
			if (graphics == null)
			{
				throw new ArgumentNullException("graphics");
			}
			int[] array = new int[topWidth];
			int[] array2 = new int[topWidth];
			int[] array3 = new int[leftWidth];
			int[] array4 = new int[leftWidth];
			int[] array5 = new int[bottomWidth];
			int[] array6 = new int[bottomWidth];
			int[] array7 = new int[rightWidth];
			int[] array8 = new int[rightWidth];
			float num = 0f;
			float num2 = 0f;
			if (leftWidth > 0)
			{
				num = (float)topWidth / (float)leftWidth;
				num2 = (float)bottomWidth / (float)leftWidth;
			}
			float num3 = 0f;
			float num4 = 0f;
			if (rightWidth > 0)
			{
				num3 = (float)topWidth / (float)rightWidth;
				num4 = (float)bottomWidth / (float)rightWidth;
			}
			ControlPaint.HLSColor hlscolor = new ControlPaint.HLSColor(topColor);
			ControlPaint.HLSColor hlscolor2 = new ControlPaint.HLSColor(leftColor);
			ControlPaint.HLSColor hlscolor3 = new ControlPaint.HLSColor(bottomColor);
			ControlPaint.HLSColor hlscolor4 = new ControlPaint.HLSColor(rightColor);
			if (topWidth > 0)
			{
				int i;
				for (i = 0; i < topWidth; i++)
				{
					int num5 = 0;
					if (num > 0f)
					{
						num5 = (int)((float)i / num);
					}
					int num6 = 0;
					if (num3 > 0f)
					{
						num6 = (int)((float)i / num3);
					}
					array[i] = bounds.X + num5;
					array2[i] = bounds.X + bounds.Width - num6 - 1;
					if (leftWidth > 0)
					{
						array3[num5] = bounds.Y + i + 1;
					}
					if (rightWidth > 0)
					{
						array7[num6] = bounds.Y + i;
					}
				}
				for (int j = i; j < leftWidth; j++)
				{
					array3[j] = bounds.Y + i + 1;
				}
				for (int k = i; k < rightWidth; k++)
				{
					array7[k] = bounds.Y + i;
				}
			}
			else
			{
				for (int l = 0; l < leftWidth; l++)
				{
					array3[l] = bounds.Y;
				}
				for (int m = 0; m < rightWidth; m++)
				{
					array7[m] = bounds.Y;
				}
			}
			if (bottomWidth > 0)
			{
				int n;
				for (n = 0; n < bottomWidth; n++)
				{
					int num7 = 0;
					if (num2 > 0f)
					{
						num7 = (int)((float)n / num2);
					}
					int num8 = 0;
					if (num4 > 0f)
					{
						num8 = (int)((float)n / num4);
					}
					array5[n] = bounds.X + num7;
					array6[n] = bounds.X + bounds.Width - num8 - 1;
					if (leftWidth > 0)
					{
						array4[num7] = bounds.Y + bounds.Height - n - 1;
					}
					if (rightWidth > 0)
					{
						array8[num8] = bounds.Y + bounds.Height - n - 1;
					}
				}
				for (int num9 = n; num9 < leftWidth; num9++)
				{
					array4[num9] = bounds.Y + bounds.Height - n - 1;
				}
				for (int num10 = n; num10 < rightWidth; num10++)
				{
					array8[num10] = bounds.Y + bounds.Height - n - 1;
				}
			}
			else
			{
				for (int num11 = 0; num11 < leftWidth; num11++)
				{
					array4[num11] = bounds.Y + bounds.Height - 1;
				}
				for (int num12 = 0; num12 < rightWidth; num12++)
				{
					array8[num12] = bounds.Y + bounds.Height - 1;
				}
			}
			switch (topStyle)
			{
			case ButtonBorderStyle.Dotted:
			{
				Pen pen = new Pen(topColor);
				pen.DashStyle = DashStyle.Dot;
				for (int num13 = 0; num13 < topWidth; num13++)
				{
					graphics.DrawLine(pen, array[num13], bounds.Y + num13, array2[num13], bounds.Y + num13);
				}
				pen.Dispose();
				break;
			}
			case ButtonBorderStyle.Dashed:
			{
				Pen pen = new Pen(topColor);
				pen.DashStyle = DashStyle.Dash;
				for (int num14 = 0; num14 < topWidth; num14++)
				{
					graphics.DrawLine(pen, array[num14], bounds.Y + num14, array2[num14], bounds.Y + num14);
				}
				pen.Dispose();
				break;
			}
			case ButtonBorderStyle.Solid:
			{
				Pen pen = new Pen(topColor);
				pen.DashStyle = DashStyle.Solid;
				for (int num15 = 0; num15 < topWidth; num15++)
				{
					graphics.DrawLine(pen, array[num15], bounds.Y + num15, array2[num15], bounds.Y + num15);
				}
				pen.Dispose();
				break;
			}
			case ButtonBorderStyle.Inset:
			{
				float num16 = ControlPaint.InfinityToOne(1f / (float)(topWidth - 1));
				for (int num17 = 0; num17 < topWidth; num17++)
				{
					Pen pen = new Pen(hlscolor.Darker(1f - (float)num17 * num16));
					pen.DashStyle = DashStyle.Solid;
					graphics.DrawLine(pen, array[num17], bounds.Y + num17, array2[num17], bounds.Y + num17);
					pen.Dispose();
				}
				break;
			}
			case ButtonBorderStyle.Outset:
			{
				float num18 = ControlPaint.InfinityToOne(1f / (float)(topWidth - 1));
				for (int num19 = 0; num19 < topWidth; num19++)
				{
					Pen pen = new Pen(hlscolor.Lighter(1f - (float)num19 * num18));
					pen.DashStyle = DashStyle.Solid;
					graphics.DrawLine(pen, array[num19], bounds.Y + num19, array2[num19], bounds.Y + num19);
					pen.Dispose();
				}
				break;
			}
			}
			switch (leftStyle)
			{
			case ButtonBorderStyle.Dotted:
			{
				Pen pen = new Pen(leftColor);
				pen.DashStyle = DashStyle.Dot;
				for (int num20 = 0; num20 < leftWidth; num20++)
				{
					graphics.DrawLine(pen, bounds.X + num20, array3[num20], bounds.X + num20, array4[num20]);
				}
				pen.Dispose();
				break;
			}
			case ButtonBorderStyle.Dashed:
			{
				Pen pen = new Pen(leftColor);
				pen.DashStyle = DashStyle.Dash;
				for (int num21 = 0; num21 < leftWidth; num21++)
				{
					graphics.DrawLine(pen, bounds.X + num21, array3[num21], bounds.X + num21, array4[num21]);
				}
				pen.Dispose();
				break;
			}
			case ButtonBorderStyle.Solid:
			{
				Pen pen = new Pen(leftColor);
				pen.DashStyle = DashStyle.Solid;
				for (int num22 = 0; num22 < leftWidth; num22++)
				{
					graphics.DrawLine(pen, bounds.X + num22, array3[num22], bounds.X + num22, array4[num22]);
				}
				pen.Dispose();
				break;
			}
			case ButtonBorderStyle.Inset:
			{
				float num23 = ControlPaint.InfinityToOne(1f / (float)(leftWidth - 1));
				for (int num24 = 0; num24 < leftWidth; num24++)
				{
					Pen pen = new Pen(hlscolor2.Darker(1f - (float)num24 * num23));
					pen.DashStyle = DashStyle.Solid;
					graphics.DrawLine(pen, bounds.X + num24, array3[num24], bounds.X + num24, array4[num24]);
					pen.Dispose();
				}
				break;
			}
			case ButtonBorderStyle.Outset:
			{
				float num25 = ControlPaint.InfinityToOne(1f / (float)(leftWidth - 1));
				for (int num26 = 0; num26 < leftWidth; num26++)
				{
					Pen pen = new Pen(hlscolor2.Lighter(1f - (float)num26 * num25));
					pen.DashStyle = DashStyle.Solid;
					graphics.DrawLine(pen, bounds.X + num26, array3[num26], bounds.X + num26, array4[num26]);
					pen.Dispose();
				}
				break;
			}
			}
			switch (bottomStyle)
			{
			case ButtonBorderStyle.Dotted:
			{
				Pen pen = new Pen(bottomColor);
				pen.DashStyle = DashStyle.Dot;
				for (int num27 = 0; num27 < bottomWidth; num27++)
				{
					graphics.DrawLine(pen, array5[num27], bounds.Y + bounds.Height - 1 - num27, array6[num27], bounds.Y + bounds.Height - 1 - num27);
				}
				pen.Dispose();
				break;
			}
			case ButtonBorderStyle.Dashed:
			{
				Pen pen = new Pen(bottomColor);
				pen.DashStyle = DashStyle.Dash;
				for (int num28 = 0; num28 < bottomWidth; num28++)
				{
					graphics.DrawLine(pen, array5[num28], bounds.Y + bounds.Height - 1 - num28, array6[num28], bounds.Y + bounds.Height - 1 - num28);
				}
				pen.Dispose();
				break;
			}
			case ButtonBorderStyle.Solid:
			{
				Pen pen = new Pen(bottomColor);
				pen.DashStyle = DashStyle.Solid;
				for (int num29 = 0; num29 < bottomWidth; num29++)
				{
					graphics.DrawLine(pen, array5[num29], bounds.Y + bounds.Height - 1 - num29, array6[num29], bounds.Y + bounds.Height - 1 - num29);
				}
				pen.Dispose();
				break;
			}
			case ButtonBorderStyle.Inset:
			{
				float num30 = ControlPaint.InfinityToOne(1f / (float)(bottomWidth - 1));
				for (int num31 = 0; num31 < bottomWidth; num31++)
				{
					Pen pen = new Pen(hlscolor3.Lighter(1f - (float)num31 * num30));
					pen.DashStyle = DashStyle.Solid;
					graphics.DrawLine(pen, array5[num31], bounds.Y + bounds.Height - 1 - num31, array6[num31], bounds.Y + bounds.Height - 1 - num31);
					pen.Dispose();
				}
				break;
			}
			case ButtonBorderStyle.Outset:
			{
				float num32 = ControlPaint.InfinityToOne(1f / (float)(bottomWidth - 1));
				for (int num33 = 0; num33 < bottomWidth; num33++)
				{
					Pen pen = new Pen(hlscolor3.Darker(1f - (float)num33 * num32));
					pen.DashStyle = DashStyle.Solid;
					graphics.DrawLine(pen, array5[num33], bounds.Y + bounds.Height - 1 - num33, array6[num33], bounds.Y + bounds.Height - 1 - num33);
					pen.Dispose();
				}
				break;
			}
			}
			switch (rightStyle)
			{
			case ButtonBorderStyle.None:
				break;
			case ButtonBorderStyle.Dotted:
			{
				Pen pen = new Pen(rightColor);
				pen.DashStyle = DashStyle.Dot;
				for (int num34 = 0; num34 < rightWidth; num34++)
				{
					graphics.DrawLine(pen, bounds.X + bounds.Width - 1 - num34, array7[num34], bounds.X + bounds.Width - 1 - num34, array8[num34]);
				}
				pen.Dispose();
				return;
			}
			case ButtonBorderStyle.Dashed:
			{
				Pen pen = new Pen(rightColor);
				pen.DashStyle = DashStyle.Dash;
				for (int num35 = 0; num35 < rightWidth; num35++)
				{
					graphics.DrawLine(pen, bounds.X + bounds.Width - 1 - num35, array7[num35], bounds.X + bounds.Width - 1 - num35, array8[num35]);
				}
				pen.Dispose();
				return;
			}
			case ButtonBorderStyle.Solid:
			{
				Pen pen = new Pen(rightColor);
				pen.DashStyle = DashStyle.Solid;
				for (int num36 = 0; num36 < rightWidth; num36++)
				{
					graphics.DrawLine(pen, bounds.X + bounds.Width - 1 - num36, array7[num36], bounds.X + bounds.Width - 1 - num36, array8[num36]);
				}
				pen.Dispose();
				return;
			}
			case ButtonBorderStyle.Inset:
			{
				float num37 = ControlPaint.InfinityToOne(1f / (float)(rightWidth - 1));
				for (int num38 = 0; num38 < rightWidth; num38++)
				{
					Pen pen = new Pen(hlscolor4.Lighter(1f - (float)num38 * num37));
					pen.DashStyle = DashStyle.Solid;
					graphics.DrawLine(pen, bounds.X + bounds.Width - 1 - num38, array7[num38], bounds.X + bounds.Width - 1 - num38, array8[num38]);
					pen.Dispose();
				}
				return;
			}
			case ButtonBorderStyle.Outset:
			{
				float num39 = ControlPaint.InfinityToOne(1f / (float)(rightWidth - 1));
				for (int num40 = 0; num40 < rightWidth; num40++)
				{
					Pen pen = new Pen(hlscolor4.Darker(1f - (float)num40 * num39));
					pen.DashStyle = DashStyle.Solid;
					graphics.DrawLine(pen, bounds.X + bounds.Width - 1 - num40, array7[num40], bounds.X + bounds.Width - 1 - num40, array8[num40]);
					pen.Dispose();
				}
				break;
			}
			default:
				return;
			}
		}

		// Token: 0x06001337 RID: 4919 RVA: 0x0003EA59 File Offset: 0x0003CC59
		public static void DrawBorder3D(Graphics graphics, Rectangle rectangle)
		{
			ControlPaint.DrawBorder3D(graphics, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, Border3DStyle.Etched, Border3DSide.Left | Border3DSide.Top | Border3DSide.Right | Border3DSide.Bottom);
		}

		// Token: 0x06001338 RID: 4920 RVA: 0x0003EA80 File Offset: 0x0003CC80
		public static void DrawBorder3D(Graphics graphics, Rectangle rectangle, Border3DStyle style)
		{
			ControlPaint.DrawBorder3D(graphics, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, style, Border3DSide.Left | Border3DSide.Top | Border3DSide.Right | Border3DSide.Bottom);
		}

		// Token: 0x06001339 RID: 4921 RVA: 0x0003EAA7 File Offset: 0x0003CCA7
		public static void DrawBorder3D(Graphics graphics, Rectangle rectangle, Border3DStyle style, Border3DSide sides)
		{
			ControlPaint.DrawBorder3D(graphics, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, style, sides);
		}

		// Token: 0x0600133A RID: 4922 RVA: 0x0003EACD File Offset: 0x0003CCCD
		public static void DrawBorder3D(Graphics graphics, int x, int y, int width, int height)
		{
			ControlPaint.DrawBorder3D(graphics, x, y, width, height, Border3DStyle.Etched, Border3DSide.Left | Border3DSide.Top | Border3DSide.Right | Border3DSide.Bottom);
		}

		// Token: 0x0600133B RID: 4923 RVA: 0x0003EADD File Offset: 0x0003CCDD
		public static void DrawBorder3D(Graphics graphics, int x, int y, int width, int height, Border3DStyle style)
		{
			ControlPaint.DrawBorder3D(graphics, x, y, width, height, style, Border3DSide.Left | Border3DSide.Top | Border3DSide.Right | Border3DSide.Bottom);
		}

		// Token: 0x0600133C RID: 4924 RVA: 0x0003EAF0 File Offset: 0x0003CCF0
		public static void DrawBorder3D(Graphics graphics, int x, int y, int width, int height, Border3DStyle style, Border3DSide sides)
		{
			if (graphics == null)
			{
				throw new ArgumentNullException("graphics");
			}
			int edge = (int)(style & (Border3DStyle)15);
			int num = (int)(sides | (Border3DSide)(style & (Border3DStyle)(-16)));
			NativeMethods.RECT rect = NativeMethods.RECT.FromXYWH(x, y, width, height);
			if ((num & 8192) == 8192)
			{
				Size border3DSize = SystemInformation.Border3DSize;
				rect.left -= border3DSize.Width;
				rect.right += border3DSize.Width;
				rect.top -= border3DSize.Height;
				rect.bottom += border3DSize.Height;
				num &= -8193;
			}
			using (WindowsGraphics windowsGraphics = WindowsGraphics.FromGraphics(graphics))
			{
				SafeNativeMethods.DrawEdge(new HandleRef(windowsGraphics, windowsGraphics.DeviceContext.Hdc), ref rect, edge, num);
			}
		}

		// Token: 0x0600133D RID: 4925 RVA: 0x0003EBCC File Offset: 0x0003CDCC
		private static void DrawBorderComplex(Graphics graphics, Rectangle bounds, Color color, ButtonBorderStyle style)
		{
			if (graphics == null)
			{
				throw new ArgumentNullException("graphics");
			}
			if (style == ButtonBorderStyle.Inset)
			{
				ControlPaint.HLSColor hlscolor = new ControlPaint.HLSColor(color);
				Pen pen = new Pen(hlscolor.Darker(1f));
				graphics.DrawLine(pen, bounds.X, bounds.Y, bounds.X + bounds.Width - 1, bounds.Y);
				graphics.DrawLine(pen, bounds.X, bounds.Y, bounds.X, bounds.Y + bounds.Height - 1);
				pen.Color = hlscolor.Lighter(1f);
				graphics.DrawLine(pen, bounds.X, bounds.Y + bounds.Height - 1, bounds.X + bounds.Width - 1, bounds.Y + bounds.Height - 1);
				graphics.DrawLine(pen, bounds.X + bounds.Width - 1, bounds.Y, bounds.X + bounds.Width - 1, bounds.Y + bounds.Height - 1);
				pen.Color = hlscolor.Lighter(0.5f);
				graphics.DrawLine(pen, bounds.X + 1, bounds.Y + 1, bounds.X + bounds.Width - 2, bounds.Y + 1);
				graphics.DrawLine(pen, bounds.X + 1, bounds.Y + 1, bounds.X + 1, bounds.Y + bounds.Height - 2);
				if (color.ToKnownColor() == SystemColors.Control.ToKnownColor())
				{
					pen.Color = SystemColors.ControlLight;
					graphics.DrawLine(pen, bounds.X + 1, bounds.Y + bounds.Height - 2, bounds.X + bounds.Width - 2, bounds.Y + bounds.Height - 2);
					graphics.DrawLine(pen, bounds.X + bounds.Width - 2, bounds.Y + 1, bounds.X + bounds.Width - 2, bounds.Y + bounds.Height - 2);
				}
				pen.Dispose();
				return;
			}
			bool flag = color.ToKnownColor() == SystemColors.Control.ToKnownColor();
			ControlPaint.HLSColor hlscolor2 = new ControlPaint.HLSColor(color);
			Pen pen2 = flag ? SystemPens.ControlLightLight : new Pen(hlscolor2.Lighter(1f));
			graphics.DrawLine(pen2, bounds.X, bounds.Y, bounds.X + bounds.Width - 1, bounds.Y);
			graphics.DrawLine(pen2, bounds.X, bounds.Y, bounds.X, bounds.Y + bounds.Height - 1);
			if (flag)
			{
				pen2 = SystemPens.ControlDarkDark;
			}
			else
			{
				pen2.Color = hlscolor2.Darker(1f);
			}
			graphics.DrawLine(pen2, bounds.X, bounds.Y + bounds.Height - 1, bounds.X + bounds.Width - 1, bounds.Y + bounds.Height - 1);
			graphics.DrawLine(pen2, bounds.X + bounds.Width - 1, bounds.Y, bounds.X + bounds.Width - 1, bounds.Y + bounds.Height - 1);
			if (flag)
			{
				if (SystemInformation.HighContrast)
				{
					pen2 = SystemPens.ControlLight;
				}
				else
				{
					pen2 = SystemPens.Control;
				}
			}
			else
			{
				pen2.Color = color;
			}
			graphics.DrawLine(pen2, bounds.X + 1, bounds.Y + 1, bounds.X + bounds.Width - 2, bounds.Y + 1);
			graphics.DrawLine(pen2, bounds.X + 1, bounds.Y + 1, bounds.X + 1, bounds.Y + bounds.Height - 2);
			if (flag)
			{
				pen2 = SystemPens.ControlDark;
			}
			else
			{
				pen2.Color = hlscolor2.Darker(0.5f);
			}
			graphics.DrawLine(pen2, bounds.X + 1, bounds.Y + bounds.Height - 2, bounds.X + bounds.Width - 2, bounds.Y + bounds.Height - 2);
			graphics.DrawLine(pen2, bounds.X + bounds.Width - 2, bounds.Y + 1, bounds.X + bounds.Width - 2, bounds.Y + bounds.Height - 2);
			if (!flag)
			{
				pen2.Dispose();
			}
		}

		// Token: 0x0600133E RID: 4926 RVA: 0x0003F098 File Offset: 0x0003D298
		internal static void DrawBorderSimple(Graphics graphics, Rectangle bounds, Color color, ButtonBorderStyle style)
		{
			if (graphics == null)
			{
				throw new ArgumentNullException("graphics");
			}
			bool flag = style == ButtonBorderStyle.Solid && color.IsSystemColor;
			Pen pen;
			if (flag)
			{
				pen = SystemPens.FromSystemColor(color);
			}
			else
			{
				pen = new Pen(color);
				if (style != ButtonBorderStyle.Solid)
				{
					pen.DashStyle = ControlPaint.BorderStyleToDashStyle(style);
				}
			}
			graphics.DrawRectangle(pen, bounds.X, bounds.Y, bounds.Width - 1, bounds.Height - 1);
			if (!flag)
			{
				pen.Dispose();
			}
		}

		// Token: 0x0600133F RID: 4927 RVA: 0x0003F115 File Offset: 0x0003D315
		public static void DrawButton(Graphics graphics, Rectangle rectangle, ButtonState state)
		{
			ControlPaint.DrawButton(graphics, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, state);
		}

		// Token: 0x06001340 RID: 4928 RVA: 0x0003F13C File Offset: 0x0003D33C
		public static void DrawButton(Graphics graphics, int x, int y, int width, int height, ButtonState state)
		{
			ControlPaint.DrawFrameControl(graphics, x, y, width, height, 4, (int)((ButtonState)16 | state), Color.Empty, Color.Empty);
		}

		// Token: 0x06001341 RID: 4929 RVA: 0x0003F164 File Offset: 0x0003D364
		public static void DrawCaptionButton(Graphics graphics, Rectangle rectangle, CaptionButton button, ButtonState state)
		{
			ControlPaint.DrawCaptionButton(graphics, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, button, state);
		}

		// Token: 0x06001342 RID: 4930 RVA: 0x0003F18C File Offset: 0x0003D38C
		public static void DrawCaptionButton(Graphics graphics, int x, int y, int width, int height, CaptionButton button, ButtonState state)
		{
			ControlPaint.DrawFrameControl(graphics, x, y, width, height, 1, (int)(button | (CaptionButton)state), Color.Empty, Color.Empty);
		}

		// Token: 0x06001343 RID: 4931 RVA: 0x0003F1B4 File Offset: 0x0003D3B4
		public static void DrawCheckBox(Graphics graphics, Rectangle rectangle, ButtonState state)
		{
			ControlPaint.DrawCheckBox(graphics, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, state);
		}

		// Token: 0x06001344 RID: 4932 RVA: 0x0003F1DC File Offset: 0x0003D3DC
		public static void DrawCheckBox(Graphics graphics, int x, int y, int width, int height, ButtonState state)
		{
			if ((state & ButtonState.Flat) == ButtonState.Flat)
			{
				ControlPaint.DrawFlatCheckBox(graphics, new Rectangle(x, y, width, height), state);
				return;
			}
			ControlPaint.DrawFrameControl(graphics, x, y, width, height, 4, (int)(ButtonState.Normal | state), Color.Empty, Color.Empty);
		}

		// Token: 0x06001345 RID: 4933 RVA: 0x0003F225 File Offset: 0x0003D425
		public static void DrawComboButton(Graphics graphics, Rectangle rectangle, ButtonState state)
		{
			ControlPaint.DrawComboButton(graphics, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, state);
		}

		// Token: 0x06001346 RID: 4934 RVA: 0x0003F24C File Offset: 0x0003D44C
		public static void DrawComboButton(Graphics graphics, int x, int y, int width, int height, ButtonState state)
		{
			ControlPaint.DrawFrameControl(graphics, x, y, width, height, 3, (int)((ButtonState)5 | state), Color.Empty, Color.Empty);
		}

		// Token: 0x06001347 RID: 4935 RVA: 0x0003F274 File Offset: 0x0003D474
		public static void DrawContainerGrabHandle(Graphics graphics, Rectangle bounds)
		{
			if (graphics == null)
			{
				throw new ArgumentNullException("graphics");
			}
			Brush white = Brushes.White;
			Pen black = Pens.Black;
			graphics.FillRectangle(white, bounds.Left + 1, bounds.Top + 1, bounds.Width - 2, bounds.Height - 2);
			graphics.DrawLine(black, bounds.X + 1, bounds.Y, bounds.Right - 2, bounds.Y);
			graphics.DrawLine(black, bounds.X + 1, bounds.Bottom - 1, bounds.Right - 2, bounds.Bottom - 1);
			graphics.DrawLine(black, bounds.X, bounds.Y + 1, bounds.X, bounds.Bottom - 2);
			graphics.DrawLine(black, bounds.Right - 1, bounds.Y + 1, bounds.Right - 1, bounds.Bottom - 2);
			int num = bounds.X + bounds.Width / 2;
			int num2 = bounds.Y + bounds.Height / 2;
			graphics.DrawLine(black, num, bounds.Y, num, bounds.Bottom - 2);
			graphics.DrawLine(black, bounds.X, num2, bounds.Right - 2, num2);
			graphics.DrawLine(black, num - 1, bounds.Y + 2, num + 1, bounds.Y + 2);
			graphics.DrawLine(black, num - 2, bounds.Y + 3, num + 2, bounds.Y + 3);
			graphics.DrawLine(black, bounds.X + 2, num2 - 1, bounds.X + 2, num2 + 1);
			graphics.DrawLine(black, bounds.X + 3, num2 - 2, bounds.X + 3, num2 + 2);
			graphics.DrawLine(black, bounds.Right - 3, num2 - 1, bounds.Right - 3, num2 + 1);
			graphics.DrawLine(black, bounds.Right - 4, num2 - 2, bounds.Right - 4, num2 + 2);
			graphics.DrawLine(black, num - 1, bounds.Bottom - 3, num + 1, bounds.Bottom - 3);
			graphics.DrawLine(black, num - 2, bounds.Bottom - 4, num + 2, bounds.Bottom - 4);
		}

		// Token: 0x06001348 RID: 4936 RVA: 0x0003F4B8 File Offset: 0x0003D6B8
		private static void DrawFlatCheckBox(Graphics graphics, Rectangle rectangle, ButtonState state)
		{
			if (graphics == null)
			{
				throw new ArgumentNullException("graphics");
			}
			Brush background = ((state & ButtonState.Inactive) == ButtonState.Inactive) ? SystemBrushes.Control : SystemBrushes.Window;
			Color foreground = ((state & ButtonState.Inactive) == ButtonState.Inactive) ? ((SystemInformation.HighContrast && AccessibilityImprovements.Level1) ? SystemColors.GrayText : SystemColors.ControlDark) : SystemColors.ControlText;
			ControlPaint.DrawFlatCheckBox(graphics, rectangle, foreground, background, state);
		}

		// Token: 0x06001349 RID: 4937 RVA: 0x0003F528 File Offset: 0x0003D728
		private static void DrawFlatCheckBox(Graphics graphics, Rectangle rectangle, Color foreground, Brush background, ButtonState state)
		{
			if (graphics == null)
			{
				throw new ArgumentNullException("graphics");
			}
			if (rectangle.Width < 0 || rectangle.Height < 0)
			{
				throw new ArgumentOutOfRangeException("rectangle");
			}
			Rectangle rect = new Rectangle(rectangle.X + 1, rectangle.Y + 1, rectangle.Width - 2, rectangle.Height - 2);
			graphics.FillRectangle(background, rect);
			if ((state & ButtonState.Checked) == ButtonState.Checked)
			{
				if (ControlPaint.checkImage == null || ControlPaint.checkImage.Width != rectangle.Width || ControlPaint.checkImage.Height != rectangle.Height)
				{
					if (ControlPaint.checkImage != null)
					{
						ControlPaint.checkImage.Dispose();
						ControlPaint.checkImage = null;
					}
					NativeMethods.RECT rect2 = NativeMethods.RECT.FromXYWH(0, 0, rectangle.Width, rectangle.Height);
					Bitmap bitmap = new Bitmap(rectangle.Width, rectangle.Height);
					using (Graphics graphics2 = Graphics.FromImage(bitmap))
					{
						graphics2.Clear(Color.Transparent);
						IntPtr hdc = graphics2.GetHdc();
						try
						{
							SafeNativeMethods.DrawFrameControl(new HandleRef(null, hdc), ref rect2, 2, 1);
						}
						finally
						{
							graphics2.ReleaseHdcInternal(hdc);
						}
					}
					bitmap.MakeTransparent();
					ControlPaint.checkImage = bitmap;
				}
				rectangle.X++;
				ControlPaint.DrawImageColorized(graphics, ControlPaint.checkImage, rectangle, foreground);
				rectangle.X--;
			}
			Pen controlDark = SystemPens.ControlDark;
			graphics.DrawRectangle(controlDark, rect.X, rect.Y, rect.Width - 1, rect.Height - 1);
		}

		// Token: 0x0600134A RID: 4938 RVA: 0x0003F6E0 File Offset: 0x0003D8E0
		public static void DrawFocusRectangle(Graphics graphics, Rectangle rectangle)
		{
			ControlPaint.DrawFocusRectangle(graphics, rectangle, SystemColors.ControlText, SystemColors.Control);
		}

		// Token: 0x0600134B RID: 4939 RVA: 0x0003F6F3 File Offset: 0x0003D8F3
		public static void DrawFocusRectangle(Graphics graphics, Rectangle rectangle, Color foreColor, Color backColor)
		{
			ControlPaint.DrawFocusRectangle(graphics, rectangle, backColor, false);
		}

		// Token: 0x0600134C RID: 4940 RVA: 0x0003F6FE File Offset: 0x0003D8FE
		internal static void DrawHighContrastFocusRectangle(Graphics graphics, Rectangle rectangle, Color color)
		{
			ControlPaint.DrawFocusRectangle(graphics, rectangle, color, true);
		}

		// Token: 0x0600134D RID: 4941 RVA: 0x0003F70C File Offset: 0x0003D90C
		private static void DrawFocusRectangle(Graphics graphics, Rectangle rectangle, Color color, bool highContrast)
		{
			if (graphics == null)
			{
				throw new ArgumentNullException("graphics");
			}
			int num = rectangle.Width;
			rectangle.Width = num - 1;
			num = rectangle.Height;
			rectangle.Height = num - 1;
			graphics.DrawRectangle(ControlPaint.GetFocusPen(color, (rectangle.X + rectangle.Y) % 2 == 1, highContrast), rectangle);
		}

		// Token: 0x0600134E RID: 4942 RVA: 0x0003F76C File Offset: 0x0003D96C
		private static void DrawFrameControl(Graphics graphics, int x, int y, int width, int height, int kind, int state, Color foreColor, Color backColor)
		{
			if (graphics == null)
			{
				throw new ArgumentNullException("graphics");
			}
			if (width < 0)
			{
				throw new ArgumentOutOfRangeException("width");
			}
			if (height < 0)
			{
				throw new ArgumentOutOfRangeException("height");
			}
			NativeMethods.RECT rect = NativeMethods.RECT.FromXYWH(0, 0, width, height);
			using (Bitmap bitmap = new Bitmap(width, height))
			{
				using (Graphics graphics2 = Graphics.FromImage(bitmap))
				{
					graphics2.Clear(Color.Transparent);
					using (WindowsGraphics windowsGraphics = WindowsGraphics.FromGraphics(graphics2))
					{
						SafeNativeMethods.DrawFrameControl(new HandleRef(windowsGraphics, windowsGraphics.DeviceContext.Hdc), ref rect, kind, state);
					}
					if (foreColor == Color.Empty || backColor == Color.Empty)
					{
						graphics.DrawImage(bitmap, x, y);
					}
					else
					{
						ImageAttributes imageAttributes = new ImageAttributes();
						imageAttributes.SetRemapTable(new ColorMap[]
						{
							new ColorMap
							{
								OldColor = Color.Black,
								NewColor = foreColor
							},
							new ColorMap
							{
								OldColor = Color.White,
								NewColor = backColor
							}
						}, ColorAdjustType.Bitmap);
						graphics.DrawImage(bitmap, new Rectangle(x, y, width, height), 0, 0, width, height, GraphicsUnit.Pixel, imageAttributes, null, IntPtr.Zero);
					}
				}
			}
		}

		// Token: 0x0600134F RID: 4943 RVA: 0x0003F8DC File Offset: 0x0003DADC
		public static void DrawGrabHandle(Graphics graphics, Rectangle rectangle, bool primary, bool enabled)
		{
			if (graphics == null)
			{
				throw new ArgumentNullException("graphics");
			}
			Pen pen;
			Brush control;
			if (primary)
			{
				if (ControlPaint.grabPenPrimary == null)
				{
					ControlPaint.grabPenPrimary = Pens.Black;
				}
				pen = ControlPaint.grabPenPrimary;
				if (enabled)
				{
					if (ControlPaint.grabBrushPrimary == null)
					{
						ControlPaint.grabBrushPrimary = Brushes.White;
					}
					control = ControlPaint.grabBrushPrimary;
				}
				else
				{
					control = SystemBrushes.Control;
				}
			}
			else
			{
				if (ControlPaint.grabPenSecondary == null)
				{
					ControlPaint.grabPenSecondary = Pens.White;
				}
				pen = ControlPaint.grabPenSecondary;
				if (enabled)
				{
					if (ControlPaint.grabBrushSecondary == null)
					{
						ControlPaint.grabBrushSecondary = Brushes.Black;
					}
					control = ControlPaint.grabBrushSecondary;
				}
				else
				{
					control = SystemBrushes.Control;
				}
			}
			Rectangle rect = new Rectangle(rectangle.X + 1, rectangle.Y + 1, rectangle.Width - 1, rectangle.Height - 1);
			graphics.FillRectangle(control, rect);
			int num = rectangle.Width;
			rectangle.Width = num - 1;
			num = rectangle.Height;
			rectangle.Height = num - 1;
			graphics.DrawRectangle(pen, rectangle);
		}

		// Token: 0x06001350 RID: 4944 RVA: 0x0003F9CC File Offset: 0x0003DBCC
		public static void DrawGrid(Graphics graphics, Rectangle area, Size pixelsBetweenDots, Color backColor)
		{
			if (graphics == null)
			{
				throw new ArgumentNullException("graphics");
			}
			if (pixelsBetweenDots.Width <= 0 || pixelsBetweenDots.Height <= 0)
			{
				throw new ArgumentOutOfRangeException("pixelsBetweenDots");
			}
			float brightness = backColor.GetBrightness();
			bool flag = (double)brightness < 0.5;
			if (ControlPaint.gridBrush == null || ControlPaint.gridSize.Width != pixelsBetweenDots.Width || ControlPaint.gridSize.Height != pixelsBetweenDots.Height || flag != ControlPaint.gridInvert)
			{
				if (ControlPaint.gridBrush != null)
				{
					ControlPaint.gridBrush.Dispose();
					ControlPaint.gridBrush = null;
				}
				ControlPaint.gridSize = pixelsBetweenDots;
				int num = 16;
				ControlPaint.gridInvert = flag;
				Color color = ControlPaint.gridInvert ? Color.White : Color.Black;
				int num2 = (num / pixelsBetweenDots.Width + 1) * pixelsBetweenDots.Width;
				int num3 = (num / pixelsBetweenDots.Height + 1) * pixelsBetweenDots.Height;
				Bitmap bitmap = new Bitmap(num2, num3);
				for (int i = 0; i < num2; i += pixelsBetweenDots.Width)
				{
					for (int j = 0; j < num3; j += pixelsBetweenDots.Height)
					{
						bitmap.SetPixel(i, j, color);
					}
				}
				ControlPaint.gridBrush = new TextureBrush(bitmap);
				bitmap.Dispose();
			}
			graphics.FillRectangle(ControlPaint.gridBrush, area);
		}

		// Token: 0x06001351 RID: 4945 RVA: 0x0003FB1C File Offset: 0x0003DD1C
		internal static void DrawImageColorized(Graphics graphics, Image image, Rectangle destination, Color replaceBlack)
		{
			ControlPaint.DrawImageColorized(graphics, image, destination, ControlPaint.RemapBlackAndWhitePreserveTransparentMatrix(replaceBlack, Color.White));
		}

		// Token: 0x06001352 RID: 4946 RVA: 0x0003FB31 File Offset: 0x0003DD31
		internal static bool IsImageTransparent(Image backgroundImage)
		{
			return backgroundImage != null && (backgroundImage.Flags & 2) > 0;
		}

		// Token: 0x06001353 RID: 4947 RVA: 0x0003FB44 File Offset: 0x0003DD44
		internal static void DrawImageReplaceColor(Graphics g, Image image, Rectangle dest, Color oldColor, Color newColor)
		{
			ImageAttributes imageAttributes = new ImageAttributes();
			imageAttributes.SetRemapTable(new ColorMap[]
			{
				new ColorMap
				{
					OldColor = oldColor,
					NewColor = newColor
				}
			}, ColorAdjustType.Bitmap);
			g.DrawImage(image, dest, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, imageAttributes, null, IntPtr.Zero);
			imageAttributes.Dispose();
		}

		// Token: 0x06001354 RID: 4948 RVA: 0x0003FBA4 File Offset: 0x0003DDA4
		private static void DrawImageColorized(Graphics graphics, Image image, Rectangle destination, ColorMatrix matrix)
		{
			if (graphics == null)
			{
				throw new ArgumentNullException("graphics");
			}
			ImageAttributes imageAttributes = new ImageAttributes();
			imageAttributes.SetColorMatrix(matrix);
			graphics.DrawImage(image, destination, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, imageAttributes, null, IntPtr.Zero);
			imageAttributes.Dispose();
		}

		// Token: 0x06001355 RID: 4949 RVA: 0x0003FBF0 File Offset: 0x0003DDF0
		public static void DrawImageDisabled(Graphics graphics, Image image, int x, int y, Color background)
		{
			ControlPaint.DrawImageDisabled(graphics, image, new Rectangle(x, y, image.Width, image.Height), background, false);
		}

		// Token: 0x06001356 RID: 4950 RVA: 0x0003FC10 File Offset: 0x0003DE10
		internal static void DrawImageDisabled(Graphics graphics, Image image, Rectangle imageBounds, Color background, bool unscaledImage)
		{
			if (graphics == null)
			{
				throw new ArgumentNullException("graphics");
			}
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			Size size = image.Size;
			if (ControlPaint.disabledImageAttr == null)
			{
				float[][] array = new float[5][];
				array[0] = new float[]
				{
					0.2125f,
					0.2125f,
					0.2125f,
					0f,
					0f
				};
				array[1] = new float[]
				{
					0.2577f,
					0.2577f,
					0.2577f,
					0f,
					0f
				};
				array[2] = new float[]
				{
					0.0361f,
					0.0361f,
					0.0361f,
					0f,
					0f
				};
				float[][] array2 = array;
				int num = 3;
				float[] array3 = new float[5];
				array3[3] = 1f;
				array2[num] = array3;
				array[4] = new float[]
				{
					0.38f,
					0.38f,
					0.38f,
					0f,
					1f
				};
				ColorMatrix colorMatrix = new ColorMatrix(array);
				ControlPaint.disabledImageAttr = new ImageAttributes();
				ControlPaint.disabledImageAttr.ClearColorKey();
				ControlPaint.disabledImageAttr.SetColorMatrix(colorMatrix);
			}
			if (unscaledImage)
			{
				using (Bitmap bitmap = new Bitmap(image.Width, image.Height))
				{
					using (Graphics graphics2 = Graphics.FromImage(bitmap))
					{
						graphics2.DrawImage(image, new Rectangle(0, 0, size.Width, size.Height), 0, 0, size.Width, size.Height, GraphicsUnit.Pixel, ControlPaint.disabledImageAttr);
					}
					graphics.DrawImageUnscaled(bitmap, imageBounds);
					return;
				}
			}
			graphics.DrawImage(image, imageBounds, 0, 0, size.Width, size.Height, GraphicsUnit.Pixel, ControlPaint.disabledImageAttr);
		}

		// Token: 0x06001357 RID: 4951 RVA: 0x0003FD88 File Offset: 0x0003DF88
		public static void DrawLockedFrame(Graphics graphics, Rectangle rectangle, bool primary)
		{
			if (graphics == null)
			{
				throw new ArgumentNullException("graphics");
			}
			Pen pen;
			if (primary)
			{
				pen = Pens.White;
			}
			else
			{
				pen = Pens.Black;
			}
			graphics.DrawRectangle(pen, rectangle.X, rectangle.Y, rectangle.Width - 1, rectangle.Height - 1);
			rectangle.Inflate(-1, -1);
			graphics.DrawRectangle(pen, rectangle.X, rectangle.Y, rectangle.Width - 1, rectangle.Height - 1);
			if (primary)
			{
				pen = Pens.Black;
			}
			else
			{
				pen = Pens.White;
			}
			rectangle.Inflate(-1, -1);
			graphics.DrawRectangle(pen, rectangle.X, rectangle.Y, rectangle.Width - 1, rectangle.Height - 1);
		}

		// Token: 0x06001358 RID: 4952 RVA: 0x0003FE4C File Offset: 0x0003E04C
		public static void DrawMenuGlyph(Graphics graphics, Rectangle rectangle, MenuGlyph glyph)
		{
			ControlPaint.DrawMenuGlyph(graphics, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, glyph);
		}

		// Token: 0x06001359 RID: 4953 RVA: 0x0003FE71 File Offset: 0x0003E071
		public static void DrawMenuGlyph(Graphics graphics, Rectangle rectangle, MenuGlyph glyph, Color foreColor, Color backColor)
		{
			ControlPaint.DrawMenuGlyph(graphics, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, glyph, foreColor, backColor);
		}

		// Token: 0x0600135A RID: 4954 RVA: 0x0003FE9C File Offset: 0x0003E09C
		public static void DrawMenuGlyph(Graphics graphics, int x, int y, int width, int height, MenuGlyph glyph)
		{
			ControlPaint.DrawFrameControl(graphics, x, y, width, height, 2, (int)glyph, Color.Empty, Color.Empty);
		}

		// Token: 0x0600135B RID: 4955 RVA: 0x0003FEC4 File Offset: 0x0003E0C4
		public static void DrawMenuGlyph(Graphics graphics, int x, int y, int width, int height, MenuGlyph glyph, Color foreColor, Color backColor)
		{
			ControlPaint.DrawFrameControl(graphics, x, y, width, height, 2, (int)glyph, foreColor, backColor);
		}

		// Token: 0x0600135C RID: 4956 RVA: 0x0003FEE3 File Offset: 0x0003E0E3
		public static void DrawMixedCheckBox(Graphics graphics, Rectangle rectangle, ButtonState state)
		{
			ControlPaint.DrawMixedCheckBox(graphics, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, state);
		}

		// Token: 0x0600135D RID: 4957 RVA: 0x0003FF08 File Offset: 0x0003E108
		public static void DrawMixedCheckBox(Graphics graphics, int x, int y, int width, int height, ButtonState state)
		{
			ControlPaint.DrawFrameControl(graphics, x, y, width, height, 4, (int)((ButtonState)8 | state), Color.Empty, Color.Empty);
		}

		// Token: 0x0600135E RID: 4958 RVA: 0x0003FF2F File Offset: 0x0003E12F
		public static void DrawRadioButton(Graphics graphics, Rectangle rectangle, ButtonState state)
		{
			ControlPaint.DrawRadioButton(graphics, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, state);
		}

		// Token: 0x0600135F RID: 4959 RVA: 0x0003FF54 File Offset: 0x0003E154
		public static void DrawRadioButton(Graphics graphics, int x, int y, int width, int height, ButtonState state)
		{
			ControlPaint.DrawFrameControl(graphics, x, y, width, height, 4, (int)((ButtonState)4 | state), Color.Empty, Color.Empty);
		}

		// Token: 0x06001360 RID: 4960 RVA: 0x0003FF7C File Offset: 0x0003E17C
		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		public static void DrawReversibleFrame(Rectangle rectangle, Color backColor, FrameStyle style)
		{
			int nDrawMode;
			Color c;
			if ((double)backColor.GetBrightness() < 0.5)
			{
				nDrawMode = 10;
				c = Color.White;
			}
			else
			{
				nDrawMode = 7;
				c = Color.Black;
			}
			IntPtr dcex = UnsafeNativeMethods.GetDCEx(new HandleRef(null, UnsafeNativeMethods.GetDesktopWindow()), NativeMethods.NullHandleRef, 1027);
			IntPtr intPtr;
			if (style != FrameStyle.Dashed)
			{
				if (style != FrameStyle.Thick)
				{
				}
				intPtr = SafeNativeMethods.CreatePen(0, 2, ColorTranslator.ToWin32(backColor));
			}
			else
			{
				intPtr = SafeNativeMethods.CreatePen(2, 1, ColorTranslator.ToWin32(backColor));
			}
			int nDrawMode2 = SafeNativeMethods.SetROP2(new HandleRef(null, dcex), nDrawMode);
			IntPtr handle = SafeNativeMethods.SelectObject(new HandleRef(null, dcex), new HandleRef(null, UnsafeNativeMethods.GetStockObject(5)));
			IntPtr handle2 = SafeNativeMethods.SelectObject(new HandleRef(null, dcex), new HandleRef(null, intPtr));
			SafeNativeMethods.SetBkColor(new HandleRef(null, dcex), ColorTranslator.ToWin32(c));
			SafeNativeMethods.Rectangle(new HandleRef(null, dcex), rectangle.X, rectangle.Y, rectangle.Right, rectangle.Bottom);
			SafeNativeMethods.SetROP2(new HandleRef(null, dcex), nDrawMode2);
			SafeNativeMethods.SelectObject(new HandleRef(null, dcex), new HandleRef(null, handle));
			SafeNativeMethods.SelectObject(new HandleRef(null, dcex), new HandleRef(null, handle2));
			if (intPtr != IntPtr.Zero)
			{
				SafeNativeMethods.DeleteObject(new HandleRef(null, intPtr));
			}
			UnsafeNativeMethods.ReleaseDC(NativeMethods.NullHandleRef, new HandleRef(null, dcex));
		}

		// Token: 0x06001361 RID: 4961 RVA: 0x000400D0 File Offset: 0x0003E2D0
		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		public static void DrawReversibleLine(Point start, Point end, Color backColor)
		{
			int colorRop = ControlPaint.GetColorRop(backColor, 10, 7);
			IntPtr dcex = UnsafeNativeMethods.GetDCEx(new HandleRef(null, UnsafeNativeMethods.GetDesktopWindow()), NativeMethods.NullHandleRef, 1027);
			IntPtr handle = SafeNativeMethods.CreatePen(0, 1, ColorTranslator.ToWin32(backColor));
			int nDrawMode = SafeNativeMethods.SetROP2(new HandleRef(null, dcex), colorRop);
			IntPtr handle2 = SafeNativeMethods.SelectObject(new HandleRef(null, dcex), new HandleRef(null, UnsafeNativeMethods.GetStockObject(5)));
			IntPtr handle3 = SafeNativeMethods.SelectObject(new HandleRef(null, dcex), new HandleRef(null, handle));
			SafeNativeMethods.MoveToEx(new HandleRef(null, dcex), start.X, start.Y, null);
			SafeNativeMethods.LineTo(new HandleRef(null, dcex), end.X, end.Y);
			SafeNativeMethods.SetROP2(new HandleRef(null, dcex), nDrawMode);
			SafeNativeMethods.SelectObject(new HandleRef(null, dcex), new HandleRef(null, handle2));
			SafeNativeMethods.SelectObject(new HandleRef(null, dcex), new HandleRef(null, handle3));
			SafeNativeMethods.DeleteObject(new HandleRef(null, handle));
			UnsafeNativeMethods.ReleaseDC(NativeMethods.NullHandleRef, new HandleRef(null, dcex));
		}

		// Token: 0x06001362 RID: 4962 RVA: 0x000401DB File Offset: 0x0003E3DB
		public static void DrawScrollButton(Graphics graphics, Rectangle rectangle, ScrollButton button, ButtonState state)
		{
			ControlPaint.DrawScrollButton(graphics, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, button, state);
		}

		// Token: 0x06001363 RID: 4963 RVA: 0x00040204 File Offset: 0x0003E404
		public static void DrawScrollButton(Graphics graphics, int x, int y, int width, int height, ScrollButton button, ButtonState state)
		{
			ControlPaint.DrawFrameControl(graphics, x, y, width, height, 3, (int)(button | (ScrollButton)state), Color.Empty, Color.Empty);
		}

		// Token: 0x06001364 RID: 4964 RVA: 0x0004022C File Offset: 0x0003E42C
		public static void DrawSelectionFrame(Graphics graphics, bool active, Rectangle outsideRect, Rectangle insideRect, Color backColor)
		{
			if (graphics == null)
			{
				throw new ArgumentNullException("graphics");
			}
			Brush brush;
			if (active)
			{
				brush = ControlPaint.GetActiveBrush(backColor);
			}
			else
			{
				brush = ControlPaint.GetSelectedBrush(backColor);
			}
			Region clip = graphics.Clip;
			graphics.ExcludeClip(insideRect);
			graphics.FillRectangle(brush, outsideRect);
			graphics.Clip = clip;
		}

		// Token: 0x06001365 RID: 4965 RVA: 0x00040279 File Offset: 0x0003E479
		public static void DrawSizeGrip(Graphics graphics, Color backColor, Rectangle bounds)
		{
			ControlPaint.DrawSizeGrip(graphics, backColor, bounds.X, bounds.Y, bounds.Width, bounds.Height);
		}

		// Token: 0x06001366 RID: 4966 RVA: 0x000402A0 File Offset: 0x0003E4A0
		public static void DrawSizeGrip(Graphics graphics, Color backColor, int x, int y, int width, int height)
		{
			if (graphics == null)
			{
				throw new ArgumentNullException("graphics");
			}
			using (Pen pen = new Pen(ControlPaint.LightLight(backColor)))
			{
				using (Pen pen2 = new Pen(ControlPaint.Dark(backColor)))
				{
					int num = Math.Min(width, height);
					int num2 = x + width - 1;
					int num3 = y + height - 2;
					for (int i = 0; i < num - 4; i += 4)
					{
						graphics.DrawLine(pen2, num2 - (i + 1) - 2, num3, num2, num3 - (i + 1) - 2);
						graphics.DrawLine(pen2, num2 - (i + 2) - 2, num3, num2, num3 - (i + 2) - 2);
						graphics.DrawLine(pen, num2 - (i + 3) - 2, num3, num2, num3 - (i + 3) - 2);
					}
				}
			}
		}

		// Token: 0x06001367 RID: 4967 RVA: 0x00040384 File Offset: 0x0003E584
		public static void DrawStringDisabled(Graphics graphics, string s, Font font, Color color, RectangleF layoutRectangle, StringFormat format)
		{
			if (graphics == null)
			{
				throw new ArgumentNullException("graphics");
			}
			if (SystemInformation.HighContrast && AccessibilityImprovements.Level1)
			{
				graphics.DrawString(s, font, SystemBrushes.GrayText, layoutRectangle, format);
				return;
			}
			layoutRectangle.Offset(1f, 1f);
			using (SolidBrush solidBrush = new SolidBrush(ControlPaint.LightLight(color)))
			{
				graphics.DrawString(s, font, solidBrush, layoutRectangle, format);
				layoutRectangle.Offset(-1f, -1f);
				color = ControlPaint.Dark(color);
				solidBrush.Color = color;
				graphics.DrawString(s, font, solidBrush, layoutRectangle, format);
			}
		}

		// Token: 0x06001368 RID: 4968 RVA: 0x00040434 File Offset: 0x0003E634
		public static void DrawStringDisabled(IDeviceContext dc, string s, Font font, Color color, Rectangle layoutRectangle, TextFormatFlags format)
		{
			if (dc == null)
			{
				throw new ArgumentNullException("dc");
			}
			if (SystemInformation.HighContrast && AccessibilityImprovements.Level1)
			{
				TextRenderer.DrawText(dc, s, font, layoutRectangle, SystemColors.GrayText, format);
				return;
			}
			layoutRectangle.Offset(1, 1);
			Color foreColor = ControlPaint.LightLight(color);
			TextRenderer.DrawText(dc, s, font, layoutRectangle, foreColor, format);
			layoutRectangle.Offset(-1, -1);
			foreColor = ControlPaint.Dark(color);
			TextRenderer.DrawText(dc, s, font, layoutRectangle, foreColor, format);
		}

		// Token: 0x06001369 RID: 4969 RVA: 0x000404AC File Offset: 0x0003E6AC
		public static void DrawVisualStyleBorder(Graphics graphics, Rectangle bounds)
		{
			if (graphics == null)
			{
				throw new ArgumentNullException("graphics");
			}
			using (Pen pen = new Pen(VisualStyleInformation.TextControlBorder))
			{
				graphics.DrawRectangle(pen, bounds);
			}
		}

		// Token: 0x0600136A RID: 4970 RVA: 0x000404F8 File Offset: 0x0003E6F8
		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		public static void FillReversibleRectangle(Rectangle rectangle, Color backColor)
		{
			int colorRop = ControlPaint.GetColorRop(backColor, 10813541, 5898313);
			int colorRop2 = ControlPaint.GetColorRop(backColor, 6, 6);
			IntPtr dcex = UnsafeNativeMethods.GetDCEx(new HandleRef(null, UnsafeNativeMethods.GetDesktopWindow()), NativeMethods.NullHandleRef, 1027);
			IntPtr handle = SafeNativeMethods.CreateSolidBrush(ColorTranslator.ToWin32(backColor));
			int nDrawMode = SafeNativeMethods.SetROP2(new HandleRef(null, dcex), colorRop2);
			IntPtr handle2 = SafeNativeMethods.SelectObject(new HandleRef(null, dcex), new HandleRef(null, handle));
			SafeNativeMethods.PatBlt(new HandleRef(null, dcex), rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, colorRop);
			SafeNativeMethods.SetROP2(new HandleRef(null, dcex), nDrawMode);
			SafeNativeMethods.SelectObject(new HandleRef(null, dcex), new HandleRef(null, handle2));
			SafeNativeMethods.DeleteObject(new HandleRef(null, handle));
			UnsafeNativeMethods.ReleaseDC(NativeMethods.NullHandleRef, new HandleRef(null, dcex));
		}

		// Token: 0x0600136B RID: 4971 RVA: 0x000405D7 File Offset: 0x0003E7D7
		internal static Font FontInPoints(Font font)
		{
			return new Font(font.FontFamily, font.SizeInPoints, font.Style, GraphicsUnit.Point, font.GdiCharSet, font.GdiVerticalFont);
		}

		// Token: 0x0600136C RID: 4972 RVA: 0x00040600 File Offset: 0x0003E800
		internal static bool FontToIFont(Font source, UnsafeNativeMethods.IFont target)
		{
			bool result = false;
			string name = target.GetName();
			if (!source.Name.Equals(name))
			{
				target.SetName(source.Name);
				result = true;
			}
			float num = (float)target.GetSize() / 10000f;
			float sizeInPoints = source.SizeInPoints;
			if (sizeInPoints != num)
			{
				target.SetSize((long)(sizeInPoints * 10000f));
				result = true;
			}
			NativeMethods.LOGFONT logfont = new NativeMethods.LOGFONT();
			IntSecurity.ObjectFromWin32Handle.Assert();
			try
			{
				source.ToLogFont(logfont);
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
			short weight = target.GetWeight();
			if ((int)weight != logfont.lfWeight)
			{
				target.SetWeight((short)logfont.lfWeight);
				result = true;
			}
			bool bold = target.GetBold();
			if (bold != logfont.lfWeight >= 700)
			{
				target.SetBold(logfont.lfWeight >= 700);
				result = true;
			}
			bool italic = target.GetItalic();
			if (italic != logfont.lfItalic > 0)
			{
				target.SetItalic(logfont.lfItalic > 0);
				result = true;
			}
			bool underline = target.GetUnderline();
			if (underline != logfont.lfUnderline > 0)
			{
				target.SetUnderline(logfont.lfUnderline > 0);
				result = true;
			}
			bool strikethrough = target.GetStrikethrough();
			if (strikethrough != logfont.lfStrikeOut > 0)
			{
				target.SetStrikethrough(logfont.lfStrikeOut > 0);
				result = true;
			}
			short charset = target.GetCharset();
			if (charset != (short)logfont.lfCharSet)
			{
				target.SetCharset((short)logfont.lfCharSet);
				result = true;
			}
			return result;
		}

		// Token: 0x0600136D RID: 4973 RVA: 0x00040780 File Offset: 0x0003E980
		private static int GetColorRop(Color color, int darkROP, int lightROP)
		{
			if ((double)color.GetBrightness() < 0.5)
			{
				return darkROP;
			}
			return lightROP;
		}

		// Token: 0x0600136E RID: 4974 RVA: 0x00040798 File Offset: 0x0003E998
		private static Brush GetActiveBrush(Color backColor)
		{
			Color color;
			if ((double)backColor.GetBrightness() <= 0.5)
			{
				color = SystemColors.ControlLight;
			}
			else
			{
				color = SystemColors.ControlDark;
			}
			if (ControlPaint.frameBrushActive == null || !ControlPaint.frameColorActive.Equals(color))
			{
				if (ControlPaint.frameBrushActive != null)
				{
					ControlPaint.frameBrushActive.Dispose();
					ControlPaint.frameBrushActive = null;
				}
				ControlPaint.frameColorActive = color;
				int num = 8;
				Bitmap bitmap = new Bitmap(num, num);
				for (int i = 0; i < num; i++)
				{
					for (int j = 0; j < num; j++)
					{
						bitmap.SetPixel(i, j, Color.Transparent);
					}
				}
				for (int k = 0; k < num; k++)
				{
					for (int l = -k; l < num; l += 4)
					{
						if (l >= 0)
						{
							bitmap.SetPixel(l, k, color);
						}
					}
				}
				ControlPaint.frameBrushActive = new TextureBrush(bitmap);
				bitmap.Dispose();
			}
			return ControlPaint.frameBrushActive;
		}

		// Token: 0x0600136F RID: 4975 RVA: 0x00040884 File Offset: 0x0003EA84
		private static Pen GetFocusPen(Color baseColor, bool odds, bool highContrast)
		{
			if (ControlPaint.focusPen == null || (!highContrast && (double)ControlPaint.focusPenColor.GetBrightness() <= 0.5 && (double)baseColor.GetBrightness() <= 0.5) || ControlPaint.focusPenColor.ToArgb() != baseColor.ToArgb() || ControlPaint.hcFocusPen != highContrast)
			{
				if (ControlPaint.focusPen != null)
				{
					ControlPaint.focusPen.Dispose();
					ControlPaint.focusPen = null;
					ControlPaint.focusPenInvert.Dispose();
					ControlPaint.focusPenInvert = null;
				}
				ControlPaint.focusPenColor = baseColor;
				ControlPaint.hcFocusPen = highContrast;
				Bitmap bitmap = new Bitmap(2, 2);
				Color color = Color.Transparent;
				Color color2;
				if (highContrast)
				{
					color2 = baseColor;
				}
				else
				{
					color2 = Color.Black;
					if ((double)baseColor.GetBrightness() <= 0.5)
					{
						color = color2;
						color2 = ControlPaint.InvertColor(baseColor);
					}
					else if (baseColor == Color.Transparent)
					{
						color = Color.White;
					}
				}
				bitmap.SetPixel(1, 0, color2);
				bitmap.SetPixel(0, 1, color2);
				bitmap.SetPixel(0, 0, color);
				bitmap.SetPixel(1, 1, color);
				Brush brush = new TextureBrush(bitmap);
				ControlPaint.focusPen = new Pen(brush, 1f);
				brush.Dispose();
				bitmap.SetPixel(1, 0, color);
				bitmap.SetPixel(0, 1, color);
				bitmap.SetPixel(0, 0, color2);
				bitmap.SetPixel(1, 1, color2);
				brush = new TextureBrush(bitmap);
				ControlPaint.focusPenInvert = new Pen(brush, 1f);
				brush.Dispose();
				bitmap.Dispose();
			}
			if (!odds)
			{
				return ControlPaint.focusPenInvert;
			}
			return ControlPaint.focusPen;
		}

		// Token: 0x06001370 RID: 4976 RVA: 0x000409F8 File Offset: 0x0003EBF8
		private static Brush GetSelectedBrush(Color backColor)
		{
			Color color;
			if ((double)backColor.GetBrightness() <= 0.5)
			{
				color = SystemColors.ControlLight;
			}
			else
			{
				color = SystemColors.ControlDark;
			}
			if (ControlPaint.frameBrushSelected == null || !ControlPaint.frameColorSelected.Equals(color))
			{
				if (ControlPaint.frameBrushSelected != null)
				{
					ControlPaint.frameBrushSelected.Dispose();
					ControlPaint.frameBrushSelected = null;
				}
				ControlPaint.frameColorSelected = color;
				int num = 8;
				Bitmap bitmap = new Bitmap(num, num);
				for (int i = 0; i < num; i++)
				{
					for (int j = 0; j < num; j++)
					{
						bitmap.SetPixel(i, j, Color.Transparent);
					}
				}
				int num2 = 0;
				for (int k = 0; k < num; k += 2)
				{
					for (int l = num2; l < num; l += 2)
					{
						bitmap.SetPixel(k, l, color);
					}
					num2 ^= 1;
				}
				ControlPaint.frameBrushSelected = new TextureBrush(bitmap);
				bitmap.Dispose();
			}
			return ControlPaint.frameBrushSelected;
		}

		// Token: 0x06001371 RID: 4977 RVA: 0x00040AE5 File Offset: 0x0003ECE5
		private static float InfinityToOne(float value)
		{
			if (value == float.NegativeInfinity || value == float.PositiveInfinity)
			{
				return 1f;
			}
			return value;
		}

		// Token: 0x06001372 RID: 4978 RVA: 0x00040AFE File Offset: 0x0003ECFE
		private static Color InvertColor(Color color)
		{
			return Color.FromArgb((int)color.A, (int)(~color.R), (int)(~color.G), (int)(~color.B));
		}

		// Token: 0x06001373 RID: 4979 RVA: 0x00040B28 File Offset: 0x0003ED28
		public static Color Light(Color baseColor, float percOfLightLight)
		{
			return new ControlPaint.HLSColor(baseColor).Lighter(percOfLightLight);
		}

		// Token: 0x06001374 RID: 4980 RVA: 0x00040B44 File Offset: 0x0003ED44
		public static Color Light(Color baseColor)
		{
			return new ControlPaint.HLSColor(baseColor).Lighter(0.5f);
		}

		// Token: 0x06001375 RID: 4981 RVA: 0x00040B64 File Offset: 0x0003ED64
		public static Color LightLight(Color baseColor)
		{
			return new ControlPaint.HLSColor(baseColor).Lighter(1f);
		}

		// Token: 0x06001376 RID: 4982 RVA: 0x00040B84 File Offset: 0x0003ED84
		internal static ColorMatrix MultiplyColorMatrix(float[][] matrix1, float[][] matrix2)
		{
			int num = 5;
			float[][] array = new float[num][];
			for (int i = 0; i < num; i++)
			{
				array[i] = new float[num];
			}
			float[] array2 = new float[num];
			for (int j = 0; j < num; j++)
			{
				for (int k = 0; k < num; k++)
				{
					array2[k] = matrix1[k][j];
				}
				for (int l = 0; l < num; l++)
				{
					float[] array3 = matrix2[l];
					float num2 = 0f;
					for (int m = 0; m < num; m++)
					{
						num2 += array3[m] * array2[m];
					}
					array[l][j] = num2;
				}
			}
			return new ColorMatrix(array);
		}

		// Token: 0x06001377 RID: 4983 RVA: 0x00040C2C File Offset: 0x0003EE2C
		internal static void PaintTableControlBorder(TableLayoutPanelCellBorderStyle borderStyle, Graphics g, Rectangle bound)
		{
			int x = bound.X;
			int y = bound.Y;
			int right = bound.Right;
			int bottom = bound.Bottom;
			switch (borderStyle)
			{
			case TableLayoutPanelCellBorderStyle.None:
			case TableLayoutPanelCellBorderStyle.Single:
				return;
			case TableLayoutPanelCellBorderStyle.Inset:
			case TableLayoutPanelCellBorderStyle.InsetDouble:
				g.DrawLine(SystemPens.ControlDark, x, y, right - 1, y);
				g.DrawLine(SystemPens.ControlDark, x, y, x, bottom - 1);
				using (Pen pen = new Pen(SystemColors.Window))
				{
					g.DrawLine(pen, right - 1, y, right - 1, bottom - 1);
					g.DrawLine(pen, x, bottom - 1, right - 1, bottom - 1);
					return;
				}
				break;
			case TableLayoutPanelCellBorderStyle.Outset:
			case TableLayoutPanelCellBorderStyle.OutsetDouble:
			case TableLayoutPanelCellBorderStyle.OutsetPartial:
				break;
			default:
				return;
			}
			using (Pen pen2 = new Pen(SystemColors.Window))
			{
				g.DrawLine(pen2, x, y, right - 1, y);
				g.DrawLine(pen2, x, y, x, bottom - 1);
			}
			g.DrawLine(SystemPens.ControlDark, right - 1, y, right - 1, bottom - 1);
			g.DrawLine(SystemPens.ControlDark, x, bottom - 1, right - 1, bottom - 1);
		}

		// Token: 0x06001378 RID: 4984 RVA: 0x00040D58 File Offset: 0x0003EF58
		internal static void PaintTableCellBorder(TableLayoutPanelCellBorderStyle borderStyle, Graphics g, Rectangle bound)
		{
			switch (borderStyle)
			{
			case TableLayoutPanelCellBorderStyle.None:
				return;
			case TableLayoutPanelCellBorderStyle.Single:
				g.DrawRectangle(SystemPens.ControlDark, bound);
				return;
			case TableLayoutPanelCellBorderStyle.Inset:
				using (Pen pen = new Pen(SystemColors.Window))
				{
					g.DrawLine(pen, bound.X, bound.Y, bound.X + bound.Width - 1, bound.Y);
					g.DrawLine(pen, bound.X, bound.Y, bound.X, bound.Y + bound.Height - 1);
				}
				g.DrawLine(SystemPens.ControlDark, bound.X + bound.Width - 1, bound.Y, bound.X + bound.Width - 1, bound.Y + bound.Height - 1);
				g.DrawLine(SystemPens.ControlDark, bound.X, bound.Y + bound.Height - 1, bound.X + bound.Width - 1, bound.Y + bound.Height - 1);
				return;
			case TableLayoutPanelCellBorderStyle.InsetDouble:
				g.DrawRectangle(SystemPens.Control, bound);
				bound = new Rectangle(bound.X + 1, bound.Y + 1, bound.Width - 1, bound.Height - 1);
				using (Pen pen2 = new Pen(SystemColors.Window))
				{
					g.DrawLine(pen2, bound.X, bound.Y, bound.X + bound.Width - 1, bound.Y);
					g.DrawLine(pen2, bound.X, bound.Y, bound.X, bound.Y + bound.Height - 1);
				}
				g.DrawLine(SystemPens.ControlDark, bound.X + bound.Width - 1, bound.Y, bound.X + bound.Width - 1, bound.Y + bound.Height - 1);
				g.DrawLine(SystemPens.ControlDark, bound.X, bound.Y + bound.Height - 1, bound.X + bound.Width - 1, bound.Y + bound.Height - 1);
				return;
			case TableLayoutPanelCellBorderStyle.Outset:
				g.DrawLine(SystemPens.ControlDark, bound.X, bound.Y, bound.X + bound.Width - 1, bound.Y);
				g.DrawLine(SystemPens.ControlDark, bound.X, bound.Y, bound.X, bound.Y + bound.Height - 1);
				using (Pen pen3 = new Pen(SystemColors.Window))
				{
					g.DrawLine(pen3, bound.X + bound.Width - 1, bound.Y, bound.X + bound.Width - 1, bound.Y + bound.Height - 1);
					g.DrawLine(pen3, bound.X, bound.Y + bound.Height - 1, bound.X + bound.Width - 1, bound.Y + bound.Height - 1);
					return;
				}
				break;
			case TableLayoutPanelCellBorderStyle.OutsetDouble:
			case TableLayoutPanelCellBorderStyle.OutsetPartial:
				break;
			default:
				return;
			}
			g.DrawRectangle(SystemPens.Control, bound);
			bound = new Rectangle(bound.X + 1, bound.Y + 1, bound.Width - 1, bound.Height - 1);
			g.DrawLine(SystemPens.ControlDark, bound.X, bound.Y, bound.X + bound.Width - 1, bound.Y);
			g.DrawLine(SystemPens.ControlDark, bound.X, bound.Y, bound.X, bound.Y + bound.Height - 1);
			using (Pen pen4 = new Pen(SystemColors.Window))
			{
				g.DrawLine(pen4, bound.X + bound.Width - 1, bound.Y, bound.X + bound.Width - 1, bound.Y + bound.Height - 1);
				g.DrawLine(pen4, bound.X, bound.Y + bound.Height - 1, bound.X + bound.Width - 1, bound.Y + bound.Height - 1);
			}
		}

		// Token: 0x06001379 RID: 4985 RVA: 0x00041228 File Offset: 0x0003F428
		private static ColorMatrix RemapBlackAndWhitePreserveTransparentMatrix(Color replaceBlack, Color replaceWhite)
		{
			float num = (float)replaceBlack.R / 255f;
			float num2 = (float)replaceBlack.G / 255f;
			float num3 = (float)replaceBlack.B / 255f;
			float num4 = (float)replaceBlack.A / 255f;
			float matrix = (float)replaceWhite.R / 255f;
			float matrix2 = (float)replaceWhite.G / 255f;
			float matrix3 = (float)replaceWhite.B / 255f;
			float num5 = (float)replaceWhite.A / 255f;
			return new ColorMatrix
			{
				Matrix00 = -num,
				Matrix01 = -num2,
				Matrix02 = -num3,
				Matrix10 = matrix,
				Matrix11 = matrix2,
				Matrix12 = matrix3,
				Matrix33 = 1f,
				Matrix40 = num,
				Matrix41 = num2,
				Matrix42 = num3,
				Matrix44 = 1f
			};
		}

		// Token: 0x0600137A RID: 4986 RVA: 0x00041320 File Offset: 0x0003F520
		internal static TextFormatFlags TextFormatFlagsForAlignmentGDI(System.Drawing.ContentAlignment align)
		{
			TextFormatFlags textFormatFlags = TextFormatFlags.Default;
			textFormatFlags |= ControlPaint.TranslateAlignmentForGDI(align);
			return textFormatFlags | ControlPaint.TranslateLineAlignmentForGDI(align);
		}

		// Token: 0x0600137B RID: 4987 RVA: 0x00041344 File Offset: 0x0003F544
		internal static StringAlignment TranslateAlignment(System.Drawing.ContentAlignment align)
		{
			StringAlignment result;
			if ((align & ControlPaint.anyRight) != (System.Drawing.ContentAlignment)0)
			{
				result = StringAlignment.Far;
			}
			else if ((align & ControlPaint.anyCenter) != (System.Drawing.ContentAlignment)0)
			{
				result = StringAlignment.Center;
			}
			else
			{
				result = StringAlignment.Near;
			}
			return result;
		}

		// Token: 0x0600137C RID: 4988 RVA: 0x00041370 File Offset: 0x0003F570
		internal static TextFormatFlags TranslateAlignmentForGDI(System.Drawing.ContentAlignment align)
		{
			TextFormatFlags result;
			if ((align & ControlPaint.anyBottom) != (System.Drawing.ContentAlignment)0)
			{
				result = TextFormatFlags.Bottom;
			}
			else if ((align & ControlPaint.anyMiddle) != (System.Drawing.ContentAlignment)0)
			{
				result = TextFormatFlags.VerticalCenter;
			}
			else
			{
				result = TextFormatFlags.Default;
			}
			return result;
		}

		// Token: 0x0600137D RID: 4989 RVA: 0x0004139C File Offset: 0x0003F59C
		internal static StringAlignment TranslateLineAlignment(System.Drawing.ContentAlignment align)
		{
			StringAlignment result;
			if ((align & ControlPaint.anyBottom) != (System.Drawing.ContentAlignment)0)
			{
				result = StringAlignment.Far;
			}
			else if ((align & ControlPaint.anyMiddle) != (System.Drawing.ContentAlignment)0)
			{
				result = StringAlignment.Center;
			}
			else
			{
				result = StringAlignment.Near;
			}
			return result;
		}

		// Token: 0x0600137E RID: 4990 RVA: 0x000413C8 File Offset: 0x0003F5C8
		internal static TextFormatFlags TranslateLineAlignmentForGDI(System.Drawing.ContentAlignment align)
		{
			TextFormatFlags result;
			if ((align & ControlPaint.anyRight) != (System.Drawing.ContentAlignment)0)
			{
				result = TextFormatFlags.Right;
			}
			else if ((align & ControlPaint.anyCenter) != (System.Drawing.ContentAlignment)0)
			{
				result = TextFormatFlags.HorizontalCenter;
			}
			else
			{
				result = TextFormatFlags.Default;
			}
			return result;
		}

		// Token: 0x0600137F RID: 4991 RVA: 0x000413F4 File Offset: 0x0003F5F4
		internal static StringFormat StringFormatForAlignment(System.Drawing.ContentAlignment align)
		{
			return new StringFormat
			{
				Alignment = ControlPaint.TranslateAlignment(align),
				LineAlignment = ControlPaint.TranslateLineAlignment(align)
			};
		}

		// Token: 0x06001380 RID: 4992 RVA: 0x00041420 File Offset: 0x0003F620
		internal static StringFormat CreateStringFormat(Control ctl, System.Drawing.ContentAlignment textAlign, bool showEllipsis, bool useMnemonic)
		{
			StringFormat stringFormat = ControlPaint.StringFormatForAlignment(textAlign);
			if (ctl.RightToLeft == RightToLeft.Yes)
			{
				stringFormat.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
			}
			if (showEllipsis)
			{
				stringFormat.Trimming = StringTrimming.EllipsisCharacter;
				stringFormat.FormatFlags |= StringFormatFlags.LineLimit;
			}
			if (!useMnemonic)
			{
				stringFormat.HotkeyPrefix = HotkeyPrefix.None;
			}
			else if (ctl.ShowKeyboardCues)
			{
				stringFormat.HotkeyPrefix = HotkeyPrefix.Show;
			}
			else
			{
				stringFormat.HotkeyPrefix = HotkeyPrefix.Hide;
			}
			if (ctl.AutoSize)
			{
				stringFormat.FormatFlags |= StringFormatFlags.MeasureTrailingSpaces;
			}
			return stringFormat;
		}

		// Token: 0x06001381 RID: 4993 RVA: 0x000414A8 File Offset: 0x0003F6A8
		internal static TextFormatFlags CreateTextFormatFlags(Control ctl, System.Drawing.ContentAlignment textAlign, bool showEllipsis, bool useMnemonic)
		{
			textAlign = ctl.RtlTranslateContent(textAlign);
			TextFormatFlags textFormatFlags = ControlPaint.TextFormatFlagsForAlignmentGDI(textAlign);
			textFormatFlags |= (TextFormatFlags.TextBoxControl | TextFormatFlags.WordBreak);
			if (showEllipsis)
			{
				textFormatFlags |= TextFormatFlags.EndEllipsis;
			}
			if (ctl.RightToLeft == RightToLeft.Yes)
			{
				textFormatFlags |= TextFormatFlags.RightToLeft;
			}
			if (!useMnemonic)
			{
				textFormatFlags |= TextFormatFlags.NoPrefix;
			}
			else if (!ctl.ShowKeyboardCues)
			{
				textFormatFlags |= TextFormatFlags.HidePrefix;
			}
			return textFormatFlags;
		}

		// Token: 0x04000911 RID: 2321
		[ThreadStatic]
		private static Bitmap checkImage;

		// Token: 0x04000912 RID: 2322
		[ThreadStatic]
		private static Pen focusPen;

		// Token: 0x04000913 RID: 2323
		[ThreadStatic]
		private static Pen focusPenInvert;

		// Token: 0x04000914 RID: 2324
		[ThreadStatic]
		private static Color focusPenColor;

		// Token: 0x04000915 RID: 2325
		[ThreadStatic]
		private static bool hcFocusPen;

		// Token: 0x04000916 RID: 2326
		private static Pen grabPenPrimary;

		// Token: 0x04000917 RID: 2327
		private static Pen grabPenSecondary;

		// Token: 0x04000918 RID: 2328
		private static Brush grabBrushPrimary;

		// Token: 0x04000919 RID: 2329
		private static Brush grabBrushSecondary;

		// Token: 0x0400091A RID: 2330
		[ThreadStatic]
		private static Brush frameBrushActive;

		// Token: 0x0400091B RID: 2331
		private static Color frameColorActive;

		// Token: 0x0400091C RID: 2332
		[ThreadStatic]
		private static Brush frameBrushSelected;

		// Token: 0x0400091D RID: 2333
		private static Color frameColorSelected;

		// Token: 0x0400091E RID: 2334
		[ThreadStatic]
		private static Brush gridBrush;

		// Token: 0x0400091F RID: 2335
		private static Size gridSize;

		// Token: 0x04000920 RID: 2336
		private static bool gridInvert;

		// Token: 0x04000921 RID: 2337
		[ThreadStatic]
		private static ImageAttributes disabledImageAttr;

		// Token: 0x04000922 RID: 2338
		private static readonly System.Drawing.ContentAlignment anyRight = (System.Drawing.ContentAlignment)1092;

		// Token: 0x04000923 RID: 2339
		private static readonly System.Drawing.ContentAlignment anyBottom = (System.Drawing.ContentAlignment)1792;

		// Token: 0x04000924 RID: 2340
		private static readonly System.Drawing.ContentAlignment anyCenter = (System.Drawing.ContentAlignment)546;

		// Token: 0x04000925 RID: 2341
		private static readonly System.Drawing.ContentAlignment anyMiddle = (System.Drawing.ContentAlignment)112;

		// Token: 0x02000644 RID: 1604
		private struct HLSColor
		{
			// Token: 0x060064B2 RID: 25778 RVA: 0x00176B3C File Offset: 0x00174D3C
			public HLSColor(Color color)
			{
				this.isSystemColors_Control = (color.ToKnownColor() == SystemColors.Control.ToKnownColor());
				int r = (int)color.R;
				int g = (int)color.G;
				int b = (int)color.B;
				int num = Math.Max(Math.Max(r, g), b);
				int num2 = Math.Min(Math.Min(r, g), b);
				int num3 = num + num2;
				this.luminosity = (num3 * 240 + 255) / 510;
				int num4 = num - num2;
				if (num4 == 0)
				{
					this.saturation = 0;
					this.hue = 160;
					return;
				}
				if (this.luminosity <= 120)
				{
					this.saturation = (num4 * 240 + num3 / 2) / num3;
				}
				else
				{
					this.saturation = (num4 * 240 + (510 - num3) / 2) / (510 - num3);
				}
				int num5 = ((num - r) * 40 + num4 / 2) / num4;
				int num6 = ((num - g) * 40 + num4 / 2) / num4;
				int num7 = ((num - b) * 40 + num4 / 2) / num4;
				if (r == num)
				{
					this.hue = num7 - num6;
				}
				else if (g == num)
				{
					this.hue = 80 + num5 - num7;
				}
				else
				{
					this.hue = 160 + num6 - num5;
				}
				if (this.hue < 0)
				{
					this.hue += 240;
				}
				if (this.hue > 240)
				{
					this.hue -= 240;
				}
			}

			// Token: 0x17001594 RID: 5524
			// (get) Token: 0x060064B3 RID: 25779 RVA: 0x00176CBC File Offset: 0x00174EBC
			public int Luminosity
			{
				get
				{
					return this.luminosity;
				}
			}

			// Token: 0x060064B4 RID: 25780 RVA: 0x00176CC4 File Offset: 0x00174EC4
			public Color Darker(float percDarker)
			{
				if (!this.isSystemColors_Control)
				{
					int num = 0;
					int num2 = this.NewLuma(-333, true);
					return this.ColorFromHLS(this.hue, num2 - (int)((float)(num2 - num) * percDarker), this.saturation);
				}
				if (percDarker == 0f)
				{
					return SystemColors.ControlDark;
				}
				if (percDarker == 1f)
				{
					return SystemColors.ControlDarkDark;
				}
				Color controlDark = SystemColors.ControlDark;
				Color controlDarkDark = SystemColors.ControlDarkDark;
				int num3 = (int)(controlDark.R - controlDarkDark.R);
				int num4 = (int)(controlDark.G - controlDarkDark.G);
				int num5 = (int)(controlDark.B - controlDarkDark.B);
				return Color.FromArgb((int)(controlDark.R - (byte)((float)num3 * percDarker)), (int)(controlDark.G - (byte)((float)num4 * percDarker)), (int)(controlDark.B - (byte)((float)num5 * percDarker)));
			}

			// Token: 0x060064B5 RID: 25781 RVA: 0x00176D98 File Offset: 0x00174F98
			public override bool Equals(object o)
			{
				if (!(o is ControlPaint.HLSColor))
				{
					return false;
				}
				ControlPaint.HLSColor hlscolor = (ControlPaint.HLSColor)o;
				return this.hue == hlscolor.hue && this.saturation == hlscolor.saturation && this.luminosity == hlscolor.luminosity && this.isSystemColors_Control == hlscolor.isSystemColors_Control;
			}

			// Token: 0x060064B6 RID: 25782 RVA: 0x00176DF0 File Offset: 0x00174FF0
			public static bool operator ==(ControlPaint.HLSColor a, ControlPaint.HLSColor b)
			{
				return a.Equals(b);
			}

			// Token: 0x060064B7 RID: 25783 RVA: 0x00176E05 File Offset: 0x00175005
			public static bool operator !=(ControlPaint.HLSColor a, ControlPaint.HLSColor b)
			{
				return !a.Equals(b);
			}

			// Token: 0x060064B8 RID: 25784 RVA: 0x00176E1D File Offset: 0x0017501D
			public override int GetHashCode()
			{
				return this.hue << 6 | this.saturation << 2 | this.luminosity;
			}

			// Token: 0x060064B9 RID: 25785 RVA: 0x00176E38 File Offset: 0x00175038
			public Color Lighter(float percLighter)
			{
				if (!this.isSystemColors_Control)
				{
					int num = this.luminosity;
					int num2 = this.NewLuma(500, true);
					return this.ColorFromHLS(this.hue, num + (int)((float)(num2 - num) * percLighter), this.saturation);
				}
				if (percLighter == 0f)
				{
					return SystemColors.ControlLight;
				}
				if (percLighter == 1f)
				{
					return SystemColors.ControlLightLight;
				}
				Color controlLight = SystemColors.ControlLight;
				Color controlLightLight = SystemColors.ControlLightLight;
				int num3 = (int)(controlLight.R - controlLightLight.R);
				int num4 = (int)(controlLight.G - controlLightLight.G);
				int num5 = (int)(controlLight.B - controlLightLight.B);
				return Color.FromArgb((int)(controlLight.R - (byte)((float)num3 * percLighter)), (int)(controlLight.G - (byte)((float)num4 * percLighter)), (int)(controlLight.B - (byte)((float)num5 * percLighter)));
			}

			// Token: 0x060064BA RID: 25786 RVA: 0x00176F0E File Offset: 0x0017510E
			private int NewLuma(int n, bool scale)
			{
				return this.NewLuma(this.luminosity, n, scale);
			}

			// Token: 0x060064BB RID: 25787 RVA: 0x00176F20 File Offset: 0x00175120
			private int NewLuma(int luminosity, int n, bool scale)
			{
				if (n == 0)
				{
					return luminosity;
				}
				if (!scale)
				{
					int num = luminosity + (int)((long)n * 240L / 1000L);
					if (num < 0)
					{
						num = 0;
					}
					if (num > 240)
					{
						num = 240;
					}
					return num;
				}
				if (n > 0)
				{
					return (int)(((long)(luminosity * (1000 - n)) + 241L * (long)n) / 1000L);
				}
				return luminosity * (n + 1000) / 1000;
			}

			// Token: 0x060064BC RID: 25788 RVA: 0x00176F94 File Offset: 0x00175194
			private Color ColorFromHLS(int hue, int luminosity, int saturation)
			{
				byte blue;
				byte red;
				byte green;
				if (saturation == 0)
				{
					green = (red = (blue = (byte)(luminosity * 255 / 240)));
					if (hue != 160)
					{
					}
				}
				else
				{
					int num;
					if (luminosity <= 120)
					{
						num = (luminosity * (240 + saturation) + 120) / 240;
					}
					else
					{
						num = luminosity + saturation - (luminosity * saturation + 120) / 240;
					}
					int n = 2 * luminosity - num;
					red = (byte)((this.HueToRGB(n, num, hue + 80) * 255 + 120) / 240);
					green = (byte)((this.HueToRGB(n, num, hue) * 255 + 120) / 240);
					blue = (byte)((this.HueToRGB(n, num, hue - 80) * 255 + 120) / 240);
				}
				return Color.FromArgb((int)red, (int)green, (int)blue);
			}

			// Token: 0x060064BD RID: 25789 RVA: 0x0017705C File Offset: 0x0017525C
			private int HueToRGB(int n1, int n2, int hue)
			{
				if (hue < 0)
				{
					hue += 240;
				}
				if (hue > 240)
				{
					hue -= 240;
				}
				if (hue < 40)
				{
					return n1 + ((n2 - n1) * hue + 20) / 40;
				}
				if (hue < 120)
				{
					return n2;
				}
				if (hue < 160)
				{
					return n1 + ((n2 - n1) * (160 - hue) + 20) / 40;
				}
				return n1;
			}

			// Token: 0x040039A8 RID: 14760
			private const int ShadowAdj = -333;

			// Token: 0x040039A9 RID: 14761
			private const int HilightAdj = 500;

			// Token: 0x040039AA RID: 14762
			private const int WatermarkAdj = -50;

			// Token: 0x040039AB RID: 14763
			private const int Range = 240;

			// Token: 0x040039AC RID: 14764
			private const int HLSMax = 240;

			// Token: 0x040039AD RID: 14765
			private const int RGBMax = 255;

			// Token: 0x040039AE RID: 14766
			private const int Undefined = 160;

			// Token: 0x040039AF RID: 14767
			private int hue;

			// Token: 0x040039B0 RID: 14768
			private int saturation;

			// Token: 0x040039B1 RID: 14769
			private int luminosity;

			// Token: 0x040039B2 RID: 14770
			private bool isSystemColors_Control;
		}
	}
}
