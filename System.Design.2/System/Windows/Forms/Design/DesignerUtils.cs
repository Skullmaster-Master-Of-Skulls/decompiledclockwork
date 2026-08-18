using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Design;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Windows.Forms.Design.Behavior;

namespace System.Windows.Forms.Design
{
	// Token: 0x020002D9 RID: 729
	internal static class DesignerUtils
	{
		// Token: 0x06001CF6 RID: 7414 RVA: 0x000AE424 File Offset: 0x000AC624
		static DesignerUtils()
		{
			if (!DpiHelper.IsScalingRequired)
			{
				return;
			}
			DesignerUtils.BOXIMAGESIZE = DpiHelper.LogicalToDeviceUnitsX(DesignerUtils.BOXIMAGESIZE);
			DesignerUtils.SELECTIONBORDERSIZE = DpiHelper.LogicalToDeviceUnitsX(DesignerUtils.SELECTIONBORDERSIZE);
			DesignerUtils.SELECTIONBORDERHITAREA = DpiHelper.LogicalToDeviceUnitsX(DesignerUtils.SELECTIONBORDERHITAREA);
			DesignerUtils.HANDLESIZE = DpiHelper.LogicalToDeviceUnitsX(DesignerUtils.HANDLESIZE);
			DesignerUtils.HANDLEOVERLAP = DpiHelper.LogicalToDeviceUnitsX(DesignerUtils.HANDLEOVERLAP);
			DesignerUtils.NORESIZEHANDLESIZE = DpiHelper.LogicalToDeviceUnitsX(DesignerUtils.NORESIZEHANDLESIZE);
			DesignerUtils.LOCKHANDLEHEIGHT = DpiHelper.LogicalToDeviceUnitsY(DesignerUtils.LOCKHANDLEHEIGHT);
			DesignerUtils.LOCKHANDLEWIDTH = DpiHelper.LogicalToDeviceUnitsX(DesignerUtils.LOCKHANDLEWIDTH);
			DesignerUtils.LOCKHANDLEOVERLAP = DpiHelper.LogicalToDeviceUnitsX(DesignerUtils.LOCKHANDLEOVERLAP);
			DesignerUtils.LOCKHANDLESIZE_UPPER = DpiHelper.LogicalToDeviceUnitsX(DesignerUtils.LOCKHANDLESIZE_UPPER);
			DesignerUtils.LOCKHANDLEHEIGHT_LOWER = DpiHelper.LogicalToDeviceUnitsY(DesignerUtils.LOCKHANDLEHEIGHT_LOWER);
			DesignerUtils.LOCKHANDLEWIDTH_LOWER = DpiHelper.LogicalToDeviceUnitsX(DesignerUtils.LOCKHANDLEWIDTH_LOWER);
			DesignerUtils.CONTAINERGRABHANDLESIZE = DpiHelper.LogicalToDeviceUnitsX(DesignerUtils.CONTAINERGRABHANDLESIZE);
			DesignerUtils.RESIZEGLYPHSIZE = DpiHelper.LogicalToDeviceUnitsX(DesignerUtils.RESIZEGLYPHSIZE);
			DesignerUtils.SELECTIONBORDEROFFSET = (DesignerUtils.HANDLESIZE - DesignerUtils.SELECTIONBORDERSIZE) / 2 - DesignerUtils.HANDLEOVERLAP;
			DesignerUtils.NORESIZEBORDEROFFSET = (DesignerUtils.NORESIZEHANDLESIZE - DesignerUtils.SELECTIONBORDERSIZE) / 2;
			DesignerUtils.LOCKEDSELECTIONBORDEROFFSET_Y = (DesignerUtils.LOCKHANDLEHEIGHT - DesignerUtils.SELECTIONBORDERSIZE) / 2 - DesignerUtils.LOCKHANDLEOVERLAP;
			DesignerUtils.LOCKEDSELECTIONBORDEROFFSET_X = (DesignerUtils.LOCKHANDLEWIDTH - DesignerUtils.SELECTIONBORDERSIZE) / 2 - DesignerUtils.LOCKHANDLEOVERLAP;
			DesignerUtils.LOCKHANDLEUPPER_OFFSET = (DesignerUtils.LOCKHANDLEWIDTH_LOWER - DesignerUtils.LOCKHANDLESIZE_UPPER) / 2;
			DesignerUtils.LOCKHANDLELOWER_OFFSET = DesignerUtils.LOCKHANDLEHEIGHT - DesignerUtils.LOCKHANDLEHEIGHT_LOWER;
		}

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x06001CF7 RID: 7415 RVA: 0x000AE738 File Offset: 0x000AC938
		public static Image BoxImage
		{
			get
			{
				if (DesignerUtils.boxImage == null)
				{
					DesignerUtils.boxImage = new Bitmap(DesignerUtils.BOXIMAGESIZE, DesignerUtils.BOXIMAGESIZE, PixelFormat.Format32bppPArgb);
					using (Graphics graphics = Graphics.FromImage(DesignerUtils.boxImage))
					{
						graphics.FillRectangle(new SolidBrush(SystemColors.InactiveBorder), 0, 0, DesignerUtils.BOXIMAGESIZE, DesignerUtils.BOXIMAGESIZE);
						graphics.DrawRectangle(new Pen(SystemColors.ControlDarkDark), 0, 0, DesignerUtils.BOXIMAGESIZE - 1, DesignerUtils.BOXIMAGESIZE - 1);
					}
				}
				return DesignerUtils.boxImage;
			}
		}

		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x06001CF8 RID: 7416 RVA: 0x000AE7D0 File Offset: 0x000AC9D0
		public static Brush HoverBrush
		{
			get
			{
				return DesignerUtils.hoverBrush;
			}
		}

		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x06001CF9 RID: 7417 RVA: 0x000AE7D8 File Offset: 0x000AC9D8
		public static Size MinDragSize
		{
			get
			{
				if (DesignerUtils.minDragSize == Size.Empty)
				{
					Size dragSize = SystemInformation.DragSize;
					Size doubleClickSize = SystemInformation.DoubleClickSize;
					DesignerUtils.minDragSize.Width = Math.Max(dragSize.Width, doubleClickSize.Width);
					DesignerUtils.minDragSize.Height = Math.Max(dragSize.Height, doubleClickSize.Height);
				}
				return DesignerUtils.minDragSize;
			}
		}

		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x06001CFA RID: 7418 RVA: 0x000AE844 File Offset: 0x000ACA44
		public static Point LastCursorPoint
		{
			get
			{
				int messagePos = SafeNativeMethods.GetMessagePos();
				return new Point(NativeMethods.Util.SignedLOWORD(messagePos), NativeMethods.Util.SignedHIWORD(messagePos));
			}
		}

		// Token: 0x06001CFB RID: 7419 RVA: 0x000AE868 File Offset: 0x000ACA68
		public static void SyncBrushes()
		{
			DesignerUtils.hoverBrush.Dispose();
			DesignerUtils.hoverBrush = new SolidBrush(Color.FromArgb(50, SystemColors.Highlight));
			DesignerUtils.selectionBorderBrush.Dispose();
			DesignerUtils.selectionBorderBrush = new HatchBrush(HatchStyle.Percent50, SystemColors.ControlDarkDark, Color.Transparent);
			SafeNativeMethods.DeleteObject(new HandleRef(null, DesignerUtils.grabHandleFillBrushPrimary));
			DesignerUtils.grabHandleFillBrushPrimary = SafeNativeMethods.CreateSolidBrush(ColorTranslator.ToWin32(SystemColors.Window));
			SafeNativeMethods.DeleteObject(new HandleRef(null, DesignerUtils.grabHandleFillBrush));
			DesignerUtils.grabHandleFillBrush = SafeNativeMethods.CreateSolidBrush(ColorTranslator.ToWin32(SystemColors.ControlText));
			SafeNativeMethods.DeleteObject(new HandleRef(null, DesignerUtils.grabHandlePenPrimary));
			DesignerUtils.grabHandlePenPrimary = SafeNativeMethods.CreatePen(NativeMethods.PS_SOLID, 1, ColorTranslator.ToWin32(SystemColors.ControlText));
			SafeNativeMethods.DeleteObject(new HandleRef(null, DesignerUtils.grabHandlePen));
			DesignerUtils.grabHandlePen = SafeNativeMethods.CreatePen(NativeMethods.PS_SOLID, 1, ColorTranslator.ToWin32(SystemColors.Window));
		}

		// Token: 0x06001CFC RID: 7420 RVA: 0x000AE958 File Offset: 0x000ACB58
		private static void DrawDragBorder(Graphics g, Size imageSize, int borderSize, Color backColor)
		{
			Pen pen = SystemPens.ControlDarkDark;
			if (backColor != Color.Empty && (double)backColor.GetBrightness() < 0.5)
			{
				pen = SystemPens.ControlLight;
			}
			g.DrawLine(pen, 1, 0, imageSize.Width - 2, 0);
			g.DrawLine(pen, 1, imageSize.Height - 1, imageSize.Width - 2, imageSize.Height - 1);
			g.DrawLine(pen, 0, 1, 0, imageSize.Height - 2);
			g.DrawLine(pen, imageSize.Width - 1, 1, imageSize.Width - 1, imageSize.Height - 2);
			for (int i = 1; i < borderSize; i++)
			{
				g.DrawRectangle(pen, i, i, imageSize.Width - (2 + i), imageSize.Height - (2 + i));
			}
		}

		// Token: 0x06001CFD RID: 7421 RVA: 0x000AEA28 File Offset: 0x000ACC28
		public static void DrawResizeBorder(Graphics g, Region resizeBorder, Color backColor)
		{
			Brush brush = SystemBrushes.ControlDarkDark;
			if (backColor != Color.Empty && (double)backColor.GetBrightness() < 0.5)
			{
				brush = SystemBrushes.ControlLight;
			}
			g.FillRegion(brush, resizeBorder);
		}

		// Token: 0x06001CFE RID: 7422 RVA: 0x000AEA6C File Offset: 0x000ACC6C
		public static void DrawFrame(Graphics g, Region resizeBorder, FrameStyle style, Color backColor)
		{
			Color color = SystemColors.ControlDarkDark;
			if (backColor != Color.Empty && (double)backColor.GetBrightness() < 0.5)
			{
				color = SystemColors.ControlLight;
			}
			Brush brush;
			if (style != FrameStyle.Dashed)
			{
				if (style != FrameStyle.Thick)
				{
				}
				brush = new SolidBrush(color);
			}
			else
			{
				brush = new HatchBrush(HatchStyle.Percent50, color, Color.Transparent);
			}
			g.FillRegion(brush, resizeBorder);
			brush.Dispose();
		}

		// Token: 0x06001CFF RID: 7423 RVA: 0x000AEAD4 File Offset: 0x000ACCD4
		public static void DrawGrabHandle(Graphics graphics, Rectangle bounds, bool isPrimary, Glyph glyph)
		{
			IntPtr hdc = graphics.GetHdc();
			try
			{
				IntPtr handle = SafeNativeMethods.SelectObject(new HandleRef(glyph, hdc), new HandleRef(glyph, isPrimary ? DesignerUtils.grabHandleFillBrushPrimary : DesignerUtils.grabHandleFillBrush));
				IntPtr handle2 = SafeNativeMethods.SelectObject(new HandleRef(glyph, hdc), new HandleRef(glyph, isPrimary ? DesignerUtils.grabHandlePenPrimary : DesignerUtils.grabHandlePen));
				SafeNativeMethods.RoundRect(new HandleRef(glyph, hdc), bounds.Left, bounds.Top, bounds.Right, bounds.Bottom, 2, 2);
				SafeNativeMethods.SelectObject(new HandleRef(glyph, hdc), new HandleRef(glyph, handle));
				SafeNativeMethods.SelectObject(new HandleRef(glyph, hdc), new HandleRef(glyph, handle2));
			}
			finally
			{
				graphics.ReleaseHdcInternal(hdc);
			}
		}

		// Token: 0x06001D00 RID: 7424 RVA: 0x000AEB9C File Offset: 0x000ACD9C
		public static void DrawNoResizeHandle(Graphics graphics, Rectangle bounds, bool isPrimary, Glyph glyph)
		{
			IntPtr hdc = graphics.GetHdc();
			try
			{
				IntPtr handle = SafeNativeMethods.SelectObject(new HandleRef(glyph, hdc), new HandleRef(glyph, isPrimary ? DesignerUtils.grabHandleFillBrushPrimary : DesignerUtils.grabHandleFillBrush));
				IntPtr handle2 = SafeNativeMethods.SelectObject(new HandleRef(glyph, hdc), new HandleRef(glyph, DesignerUtils.grabHandlePenPrimary));
				SafeNativeMethods.Rectangle(new HandleRef(glyph, hdc), bounds.Left, bounds.Top, bounds.Right, bounds.Bottom);
				SafeNativeMethods.SelectObject(new HandleRef(glyph, hdc), new HandleRef(glyph, handle));
				SafeNativeMethods.SelectObject(new HandleRef(glyph, hdc), new HandleRef(glyph, handle2));
			}
			finally
			{
				graphics.ReleaseHdcInternal(hdc);
			}
		}

		// Token: 0x06001D01 RID: 7425 RVA: 0x000AEC58 File Offset: 0x000ACE58
		public static void DrawLockedHandle(Graphics graphics, Rectangle bounds, bool isPrimary, Glyph glyph)
		{
			IntPtr hdc = graphics.GetHdc();
			try
			{
				IntPtr handle = SafeNativeMethods.SelectObject(new HandleRef(glyph, hdc), new HandleRef(glyph, DesignerUtils.grabHandlePenPrimary));
				IntPtr handle2 = SafeNativeMethods.SelectObject(new HandleRef(glyph, hdc), new HandleRef(glyph, DesignerUtils.grabHandleFillBrushPrimary));
				SafeNativeMethods.RoundRect(new HandleRef(glyph, hdc), bounds.Left + DesignerUtils.LOCKHANDLEUPPER_OFFSET, bounds.Top, bounds.Left + DesignerUtils.LOCKHANDLEUPPER_OFFSET + DesignerUtils.LOCKHANDLESIZE_UPPER, bounds.Top + DesignerUtils.LOCKHANDLESIZE_UPPER, 2, 2);
				SafeNativeMethods.SelectObject(new HandleRef(glyph, hdc), new HandleRef(glyph, isPrimary ? DesignerUtils.grabHandleFillBrushPrimary : DesignerUtils.grabHandleFillBrush));
				SafeNativeMethods.Rectangle(new HandleRef(glyph, hdc), bounds.Left, bounds.Top + DesignerUtils.LOCKHANDLELOWER_OFFSET, bounds.Right, bounds.Bottom);
				SafeNativeMethods.SelectObject(new HandleRef(glyph, hdc), new HandleRef(glyph, handle2));
				SafeNativeMethods.SelectObject(new HandleRef(glyph, hdc), new HandleRef(glyph, handle));
			}
			finally
			{
				graphics.ReleaseHdcInternal(hdc);
			}
		}

		// Token: 0x06001D02 RID: 7426 RVA: 0x000AED74 File Offset: 0x000ACF74
		public static void DrawSelectionBorder(Graphics graphics, Rectangle bounds)
		{
			graphics.FillRectangle(DesignerUtils.selectionBorderBrush, bounds);
		}

		// Token: 0x06001D03 RID: 7427 RVA: 0x000AED84 File Offset: 0x000ACF84
		public static void GenerateSnapShot(Control control, ref Image image, int borderSize, double opacity, Color backColor)
		{
			if (!DesignerUtils.GenerateSnapShotWithWM_PRINT(control, ref image))
			{
				DesignerUtils.GenerateSnapShotWithBitBlt(control, ref image);
			}
			if (opacity < 1.0 && opacity > 0.0)
			{
				DesignerUtils.SetImageAlpha((Bitmap)image, opacity);
			}
			if (borderSize > 0)
			{
				using (Graphics graphics = Graphics.FromImage(image))
				{
					DesignerUtils.DrawDragBorder(graphics, image.Size, borderSize, backColor);
				}
			}
		}

		// Token: 0x06001D04 RID: 7428 RVA: 0x000AEE00 File Offset: 0x000AD000
		public static Size GetAdornmentDimensions(AdornmentType adornmentType)
		{
			if (adornmentType == AdornmentType.GrabHandle)
			{
				return new Size(DesignerUtils.HANDLESIZE, DesignerUtils.HANDLESIZE);
			}
			if (adornmentType - AdornmentType.ContainerSelector > 1)
			{
				return new Size(0, 0);
			}
			return new Size(DesignerUtils.CONTAINERGRABHANDLESIZE, DesignerUtils.CONTAINERGRABHANDLESIZE);
		}

		// Token: 0x06001D05 RID: 7429 RVA: 0x000AEE38 File Offset: 0x000AD038
		public static bool UseSnapLines(IServiceProvider provider)
		{
			bool result = true;
			object obj = null;
			DesignerOptionService designerOptionService = provider.GetService(typeof(DesignerOptionService)) as DesignerOptionService;
			if (designerOptionService != null)
			{
				PropertyDescriptor propertyDescriptor = designerOptionService.Options.Properties["UseSnapLines"];
				if (propertyDescriptor != null)
				{
					obj = propertyDescriptor.GetValue(null);
				}
			}
			if (obj != null && obj is bool)
			{
				result = (bool)obj;
			}
			return result;
		}

		// Token: 0x06001D06 RID: 7430 RVA: 0x000AEE98 File Offset: 0x000AD098
		public static object GetOptionValue(IServiceProvider provider, string name)
		{
			object result = null;
			if (provider != null)
			{
				DesignerOptionService designerOptionService = provider.GetService(typeof(DesignerOptionService)) as DesignerOptionService;
				if (designerOptionService != null)
				{
					PropertyDescriptor propertyDescriptor = designerOptionService.Options.Properties[name];
					if (propertyDescriptor != null)
					{
						result = propertyDescriptor.GetValue(null);
					}
				}
				else
				{
					IDesignerOptionService designerOptionService2 = provider.GetService(typeof(IDesignerOptionService)) as IDesignerOptionService;
					if (designerOptionService2 != null)
					{
						result = designerOptionService2.GetOptionValue("WindowsFormsDesigner\\General", name);
					}
				}
			}
			return result;
		}

		// Token: 0x06001D07 RID: 7431 RVA: 0x000AEF0C File Offset: 0x000AD10C
		public static void GenerateSnapShotWithBitBlt(Control control, ref Image image)
		{
			HandleRef hWnd = new HandleRef(control, control.Handle);
			IntPtr dc = UnsafeNativeMethods.GetDC(hWnd);
			image = new Bitmap(Math.Max(control.Width, DesignerUtils.MINCONTROLBITMAPSIZE), Math.Max(control.Height, DesignerUtils.MINCONTROLBITMAPSIZE), PixelFormat.Format32bppPArgb);
			using (Graphics graphics = Graphics.FromImage(image))
			{
				if (control.BackColor == Color.Transparent)
				{
					graphics.Clear(SystemColors.Control);
				}
				IntPtr hdc = graphics.GetHdc();
				SafeNativeMethods.BitBlt(hdc, 0, 0, image.Width, image.Height, dc, 0, 0, 13369376);
				graphics.ReleaseHdc(hdc);
			}
		}

		// Token: 0x06001D08 RID: 7432 RVA: 0x000AEFC8 File Offset: 0x000AD1C8
		public static bool GenerateSnapShotWithWM_PRINT(Control control, ref Image image)
		{
			IntPtr handle = control.Handle;
			image = new Bitmap(Math.Max(control.Width, DesignerUtils.MINCONTROLBITMAPSIZE), Math.Max(control.Height, DesignerUtils.MINCONTROLBITMAPSIZE), PixelFormat.Format32bppPArgb);
			if (control.BackColor == Color.Transparent)
			{
				using (Graphics graphics = Graphics.FromImage(image))
				{
					graphics.Clear(SystemColors.Control);
				}
			}
			Color color = Color.FromArgb(255, 252, 186, 238);
			((Bitmap)image).SetPixel(image.Width / 2, image.Height / 2, color);
			using (Graphics graphics2 = Graphics.FromImage(image))
			{
				IntPtr hdc = graphics2.GetHdc();
				NativeMethods.SendMessage(handle, 791, hdc, (IntPtr)30);
				graphics2.ReleaseHdc(hdc);
			}
			return !((Bitmap)image).GetPixel(image.Width / 2, image.Height / 2).Equals(color);
		}

		// Token: 0x06001D09 RID: 7433 RVA: 0x000AF100 File Offset: 0x000AD300
		public static Rectangle GetBoundsForSelectionType(Rectangle originalBounds, SelectionBorderGlyphType type, int borderSize)
		{
			Rectangle result = Rectangle.Empty;
			switch (type)
			{
			case SelectionBorderGlyphType.Top:
				result = new Rectangle(originalBounds.Left - borderSize, originalBounds.Top - borderSize, originalBounds.Width + 2 * borderSize, borderSize);
				break;
			case SelectionBorderGlyphType.Bottom:
				result = new Rectangle(originalBounds.Left - borderSize, originalBounds.Bottom, originalBounds.Width + 2 * borderSize, borderSize);
				break;
			case SelectionBorderGlyphType.Left:
				result = new Rectangle(originalBounds.Left - borderSize, originalBounds.Top - borderSize, borderSize, originalBounds.Height + 2 * borderSize);
				break;
			case SelectionBorderGlyphType.Right:
				result = new Rectangle(originalBounds.Right, originalBounds.Top - borderSize, borderSize, originalBounds.Height + 2 * borderSize);
				break;
			case SelectionBorderGlyphType.Body:
				result = originalBounds;
				break;
			}
			return result;
		}

		// Token: 0x06001D0A RID: 7434 RVA: 0x000AF1D0 File Offset: 0x000AD3D0
		private static Rectangle GetBoundsForSelectionType(Rectangle originalBounds, SelectionBorderGlyphType type, int bordersize, int offset)
		{
			Rectangle result = DesignerUtils.GetBoundsForSelectionType(originalBounds, type, bordersize);
			if (offset != 0)
			{
				switch (type)
				{
				case SelectionBorderGlyphType.Top:
					result.Offset(-offset, -offset);
					result.Width += 2 * offset;
					break;
				case SelectionBorderGlyphType.Bottom:
					result.Offset(-offset, offset);
					result.Width += 2 * offset;
					break;
				case SelectionBorderGlyphType.Left:
					result.Offset(-offset, -offset);
					result.Height += 2 * offset;
					break;
				case SelectionBorderGlyphType.Right:
					result.Offset(offset, -offset);
					result.Height += 2 * offset;
					break;
				case SelectionBorderGlyphType.Body:
					result = originalBounds;
					break;
				}
			}
			return result;
		}

		// Token: 0x06001D0B RID: 7435 RVA: 0x000AF281 File Offset: 0x000AD481
		public static Rectangle GetBoundsForSelectionType(Rectangle originalBounds, SelectionBorderGlyphType type)
		{
			return DesignerUtils.GetBoundsForSelectionType(originalBounds, type, DesignerUtils.SELECTIONBORDERSIZE, DesignerUtils.SELECTIONBORDEROFFSET);
		}

		// Token: 0x06001D0C RID: 7436 RVA: 0x000AF294 File Offset: 0x000AD494
		public static Rectangle GetBoundsForNoResizeSelectionType(Rectangle originalBounds, SelectionBorderGlyphType type)
		{
			return DesignerUtils.GetBoundsForSelectionType(originalBounds, type, DesignerUtils.SELECTIONBORDERSIZE, DesignerUtils.NORESIZEBORDEROFFSET);
		}

		// Token: 0x06001D0D RID: 7437 RVA: 0x000AF2A8 File Offset: 0x000AD4A8
		public static int GetTextBaseline(Control ctrl, ContentAlignment alignment)
		{
			Rectangle clientRectangle = ctrl.ClientRectangle;
			int num = 0;
			int num2 = 0;
			using (Graphics graphics = ctrl.CreateGraphics())
			{
				IntPtr hdc = graphics.GetHdc();
				IntPtr handle = ctrl.Font.ToHfont();
				try
				{
					IntPtr handle2 = SafeNativeMethods.SelectObject(new HandleRef(ctrl, hdc), new HandleRef(ctrl, handle));
					NativeMethods.TEXTMETRIC textmetric = new NativeMethods.TEXTMETRIC();
					SafeNativeMethods.GetTextMetrics(new HandleRef(ctrl, hdc), textmetric);
					num = textmetric.tmAscent + 1;
					num2 = textmetric.tmHeight;
					SafeNativeMethods.SelectObject(new HandleRef(ctrl, hdc), new HandleRef(ctrl, handle2));
				}
				finally
				{
					SafeNativeMethods.DeleteObject(new HandleRef(ctrl.Font, handle));
					graphics.ReleaseHdc(hdc);
				}
			}
			if ((alignment & DesignerUtils.anyTopAlignment) != (ContentAlignment)0)
			{
				return clientRectangle.Top + num;
			}
			if ((alignment & DesignerUtils.anyMiddleAlignment) != (ContentAlignment)0)
			{
				return clientRectangle.Top + clientRectangle.Height / 2 - num2 / 2 + num;
			}
			return clientRectangle.Bottom - num2 + num;
		}

		// Token: 0x06001D0E RID: 7438 RVA: 0x000AF3B8 File Offset: 0x000AD5B8
		public static Rectangle GetBoundsFromToolboxSnapDragDropInfo(ToolboxSnapDragDropEventArgs e, Rectangle originalBounds, bool isMirrored)
		{
			Rectangle result = originalBounds;
			if (e.Offset != Point.Empty)
			{
				if ((e.SnapDirections & ToolboxSnapDragDropEventArgs.SnapDirection.Top) != ToolboxSnapDragDropEventArgs.SnapDirection.None)
				{
					result.Y += e.Offset.Y;
				}
				else if ((e.SnapDirections & ToolboxSnapDragDropEventArgs.SnapDirection.Bottom) != ToolboxSnapDragDropEventArgs.SnapDirection.None)
				{
					result.Y = originalBounds.Y - originalBounds.Height + e.Offset.Y;
				}
				if (!isMirrored)
				{
					if ((e.SnapDirections & ToolboxSnapDragDropEventArgs.SnapDirection.Left) != ToolboxSnapDragDropEventArgs.SnapDirection.None)
					{
						result.X += e.Offset.X;
					}
					else if ((e.SnapDirections & ToolboxSnapDragDropEventArgs.SnapDirection.Right) != ToolboxSnapDragDropEventArgs.SnapDirection.None)
					{
						result.X = originalBounds.X - originalBounds.Width + e.Offset.X;
					}
				}
				else if ((e.SnapDirections & ToolboxSnapDragDropEventArgs.SnapDirection.Left) != ToolboxSnapDragDropEventArgs.SnapDirection.None)
				{
					result.X = originalBounds.X - originalBounds.Width - e.Offset.X;
				}
				else if ((e.SnapDirections & ToolboxSnapDragDropEventArgs.SnapDirection.Right) != ToolboxSnapDragDropEventArgs.SnapDirection.None)
				{
					result.X -= e.Offset.X;
				}
			}
			return result;
		}

		// Token: 0x06001D0F RID: 7439 RVA: 0x000AF4EC File Offset: 0x000AD6EC
		public static string GetUniqueSiteName(IDesignerHost host, string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				return null;
			}
			INameCreationService nameCreationService = (INameCreationService)host.GetService(typeof(INameCreationService));
			if (nameCreationService == null)
			{
				return null;
			}
			if (host.Container.Components[name] != null)
			{
				string text = name;
				int num = 1;
				while (!nameCreationService.IsValidName(text))
				{
					text = name + num.ToString(CultureInfo.InvariantCulture);
					num++;
				}
				return text;
			}
			if (!nameCreationService.IsValidName(name))
			{
				return null;
			}
			return name;
		}

		// Token: 0x06001D10 RID: 7440 RVA: 0x000AF568 File Offset: 0x000AD768
		private unsafe static void SetImageAlpha(Bitmap b, double opacity)
		{
			if (opacity == 1.0)
			{
				return;
			}
			byte[] array = new byte[256];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (byte)((double)i * opacity);
			}
			BitmapData bitmapData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
			try
			{
				int num = bitmapData.Height * bitmapData.Width;
				int* ptr = (int*)((void*)bitmapData.Scan0);
				byte* ptr2 = (byte*)(ptr + num);
				for (byte* ptr3 = (byte*)ptr + 3; ptr3 < ptr2; ptr3 += 4)
				{
					*ptr3 = array[(int)(*ptr3)];
				}
			}
			finally
			{
				b.UnlockBits(bitmapData);
			}
		}

		// Token: 0x06001D11 RID: 7441 RVA: 0x000AF620 File Offset: 0x000AD820
		public static ICollection FilterGenericTypes(ICollection types)
		{
			if (types == null || types.Count == 0)
			{
				return types;
			}
			ArrayList arrayList = new ArrayList(types.Count);
			foreach (object obj in types)
			{
				Type type = (Type)obj;
				if (!type.ContainsGenericParameters)
				{
					arrayList.Add(type);
				}
			}
			return arrayList;
		}

		// Token: 0x06001D12 RID: 7442 RVA: 0x000AF698 File Offset: 0x000AD898
		public static IContainer CheckForNestedContainer(IContainer container)
		{
			NestedContainer nestedContainer = container as NestedContainer;
			if (nestedContainer != null)
			{
				return nestedContainer.Owner.Site.Container;
			}
			return container;
		}

		// Token: 0x06001D13 RID: 7443 RVA: 0x000AF6C4 File Offset: 0x000AD8C4
		public static ICollection CopyDragObjects(ICollection objects, IServiceProvider svcProvider)
		{
			if (objects == null || svcProvider == null)
			{
				return null;
			}
			Cursor value = Cursor.Current;
			try
			{
				Cursor.Current = Cursors.WaitCursor;
				ComponentSerializationService componentSerializationService = svcProvider.GetService(typeof(ComponentSerializationService)) as ComponentSerializationService;
				IDesignerHost designerHost = svcProvider.GetService(typeof(IDesignerHost)) as IDesignerHost;
				if (componentSerializationService != null && designerHost != null)
				{
					SerializationStore serializationStore = null;
					serializationStore = componentSerializationService.CreateStore();
					ICollection collection = DesignerUtils.GetCopySelection(objects, designerHost);
					foreach (object obj in collection)
					{
						IComponent value2 = (IComponent)obj;
						componentSerializationService.Serialize(serializationStore, value2);
					}
					serializationStore.Close();
					collection = componentSerializationService.Deserialize(serializationStore);
					ArrayList arrayList = new ArrayList(objects.Count);
					foreach (object obj2 in collection)
					{
						IComponent component = (IComponent)obj2;
						Control control = component as Control;
						if (control != null && control.Parent == null)
						{
							arrayList.Add(component);
						}
						else if (control == null)
						{
							ToolStripItem toolStripItem = component as ToolStripItem;
							if (toolStripItem != null && toolStripItem.GetCurrentParent() == null)
							{
								arrayList.Add(component);
							}
						}
					}
					return arrayList;
				}
			}
			finally
			{
				Cursor.Current = value;
			}
			return null;
		}

		// Token: 0x06001D14 RID: 7444 RVA: 0x000AF870 File Offset: 0x000ADA70
		private static ICollection GetCopySelection(ICollection objects, IDesignerHost host)
		{
			if (objects == null || host == null)
			{
				return null;
			}
			ArrayList arrayList = new ArrayList();
			foreach (object obj in objects)
			{
				IComponent component = (IComponent)obj;
				arrayList.Add(component);
				DesignerUtils.GetAssociatedComponents(component, host, arrayList);
			}
			return arrayList;
		}

		// Token: 0x06001D15 RID: 7445 RVA: 0x000AF8DC File Offset: 0x000ADADC
		internal static void GetAssociatedComponents(IComponent component, IDesignerHost host, ArrayList list)
		{
			if (host == null)
			{
				return;
			}
			ComponentDesigner componentDesigner = host.GetDesigner(component) as ComponentDesigner;
			if (componentDesigner == null)
			{
				return;
			}
			foreach (object obj in componentDesigner.AssociatedComponents)
			{
				IComponent component2 = (IComponent)obj;
				if (component2.Site != null)
				{
					list.Add(component2);
					DesignerUtils.GetAssociatedComponents(component2, host, list);
				}
			}
		}

		// Token: 0x06001D16 RID: 7446 RVA: 0x000AF95C File Offset: 0x000ADB5C
		private static int TreeView_GetExtendedStyle(IntPtr handle)
		{
			return NativeMethods.SendMessage(handle, 4397, IntPtr.Zero, IntPtr.Zero).ToInt32();
		}

		// Token: 0x06001D17 RID: 7447 RVA: 0x000AF986 File Offset: 0x000ADB86
		private static void TreeView_SetExtendedStyle(IntPtr handle, int extendedStyle, int mask)
		{
			NativeMethods.SendMessage(handle, 4396, new IntPtr(mask), new IntPtr(extendedStyle));
		}

		// Token: 0x06001D18 RID: 7448 RVA: 0x000AF9A0 File Offset: 0x000ADBA0
		public static void ApplyTreeViewThemeStyles(TreeView treeView)
		{
			if (treeView == null)
			{
				throw new ArgumentNullException("treeView");
			}
			treeView.HotTracking = true;
			treeView.ShowLines = false;
			IntPtr handle = treeView.Handle;
			SafeNativeMethods.SetWindowTheme(handle, "Explorer", null);
			int num = DesignerUtils.TreeView_GetExtendedStyle(handle);
			num |= 68;
			DesignerUtils.TreeView_SetExtendedStyle(handle, num, 0);
		}

		// Token: 0x06001D19 RID: 7449 RVA: 0x000AF9F1 File Offset: 0x000ADBF1
		private static void ListView_SetExtendedListViewStyleEx(IntPtr handle, int mask, int extendedStyle)
		{
			NativeMethods.SendMessage(handle, 4150, new IntPtr(mask), new IntPtr(extendedStyle));
		}

		// Token: 0x06001D1A RID: 7450 RVA: 0x000AFA0C File Offset: 0x000ADC0C
		public static void ApplyListViewThemeStyles(ListView listView)
		{
			if (listView == null)
			{
				throw new ArgumentNullException("listView");
			}
			IntPtr handle = listView.Handle;
			SafeNativeMethods.SetWindowTheme(handle, "Explorer", null);
			DesignerUtils.ListView_SetExtendedListViewStyleEx(handle, 65536, 65536);
		}

		// Token: 0x04001728 RID: 5928
		private static Size minDragSize = Size.Empty;

		// Token: 0x04001729 RID: 5929
		private static SolidBrush hoverBrush = new SolidBrush(Color.FromArgb(50, SystemColors.Highlight));

		// Token: 0x0400172A RID: 5930
		private static HatchBrush selectionBorderBrush = new HatchBrush(HatchStyle.Percent50, SystemColors.ControlDarkDark, Color.Transparent);

		// Token: 0x0400172B RID: 5931
		private static IntPtr grabHandleFillBrushPrimary = SafeNativeMethods.CreateSolidBrush(ColorTranslator.ToWin32(SystemColors.Window));

		// Token: 0x0400172C RID: 5932
		private static IntPtr grabHandleFillBrush = SafeNativeMethods.CreateSolidBrush(ColorTranslator.ToWin32(SystemColors.ControlText));

		// Token: 0x0400172D RID: 5933
		private static IntPtr grabHandlePenPrimary = SafeNativeMethods.CreatePen(NativeMethods.PS_SOLID, 1, ColorTranslator.ToWin32(SystemColors.ControlText));

		// Token: 0x0400172E RID: 5934
		private static IntPtr grabHandlePen = SafeNativeMethods.CreatePen(NativeMethods.PS_SOLID, 1, ColorTranslator.ToWin32(SystemColors.Window));

		// Token: 0x0400172F RID: 5935
		private static Bitmap boxImage = null;

		// Token: 0x04001730 RID: 5936
		public static int BOXIMAGESIZE = 16;

		// Token: 0x04001731 RID: 5937
		public static int SELECTIONBORDERSIZE = 1;

		// Token: 0x04001732 RID: 5938
		public static int SELECTIONBORDERHITAREA = 3;

		// Token: 0x04001733 RID: 5939
		public static int HANDLESIZE = 7;

		// Token: 0x04001734 RID: 5940
		public static int HANDLEOVERLAP = 2;

		// Token: 0x04001735 RID: 5941
		public static int SELECTIONBORDEROFFSET = (DesignerUtils.HANDLESIZE - DesignerUtils.SELECTIONBORDERSIZE) / 2 - DesignerUtils.HANDLEOVERLAP;

		// Token: 0x04001736 RID: 5942
		public static int NORESIZEHANDLESIZE = 5;

		// Token: 0x04001737 RID: 5943
		public static int NORESIZEBORDEROFFSET = (DesignerUtils.NORESIZEHANDLESIZE - DesignerUtils.SELECTIONBORDERSIZE) / 2;

		// Token: 0x04001738 RID: 5944
		public static int LOCKHANDLEHEIGHT = 9;

		// Token: 0x04001739 RID: 5945
		public static int LOCKHANDLEWIDTH = 7;

		// Token: 0x0400173A RID: 5946
		public static int LOCKHANDLEOVERLAP = 2;

		// Token: 0x0400173B RID: 5947
		public static int LOCKEDSELECTIONBORDEROFFSET_Y = (DesignerUtils.LOCKHANDLEHEIGHT - DesignerUtils.SELECTIONBORDERSIZE) / 2 - DesignerUtils.LOCKHANDLEOVERLAP;

		// Token: 0x0400173C RID: 5948
		public static int LOCKEDSELECTIONBORDEROFFSET_X = (DesignerUtils.LOCKHANDLEWIDTH - DesignerUtils.SELECTIONBORDERSIZE) / 2 - DesignerUtils.LOCKHANDLEOVERLAP;

		// Token: 0x0400173D RID: 5949
		public static int LOCKHANDLESIZE_UPPER = 5;

		// Token: 0x0400173E RID: 5950
		public static int LOCKHANDLEHEIGHT_LOWER = 6;

		// Token: 0x0400173F RID: 5951
		public static int LOCKHANDLEWIDTH_LOWER = 7;

		// Token: 0x04001740 RID: 5952
		public static int LOCKHANDLEUPPER_OFFSET = (DesignerUtils.LOCKHANDLEWIDTH_LOWER - DesignerUtils.LOCKHANDLESIZE_UPPER) / 2;

		// Token: 0x04001741 RID: 5953
		public static int LOCKHANDLELOWER_OFFSET = DesignerUtils.LOCKHANDLEHEIGHT - DesignerUtils.LOCKHANDLEHEIGHT_LOWER;

		// Token: 0x04001742 RID: 5954
		public static int CONTAINERGRABHANDLESIZE = 15;

		// Token: 0x04001743 RID: 5955
		public static int SNAPELINEDELAY = 1000;

		// Token: 0x04001744 RID: 5956
		public static int MINIMUMSTYLESIZE = 20;

		// Token: 0x04001745 RID: 5957
		public static int MINIMUMSTYLEPERCENT = 50;

		// Token: 0x04001746 RID: 5958
		public static int MINCONTROLBITMAPSIZE = 1;

		// Token: 0x04001747 RID: 5959
		public static int MINUMUMSTYLESIZEDRAG = 8;

		// Token: 0x04001748 RID: 5960
		public static int DEFAULTROWCOUNT = 2;

		// Token: 0x04001749 RID: 5961
		public static int DEFAULTCOLUMNCOUNT = 2;

		// Token: 0x0400174A RID: 5962
		public static int RESIZEGLYPHSIZE = 4;

		// Token: 0x0400174B RID: 5963
		public static int DEFAULTFORMPADDING = 9;

		// Token: 0x0400174C RID: 5964
		public static readonly ContentAlignment anyTopAlignment = (ContentAlignment)7;

		// Token: 0x0400174D RID: 5965
		public static readonly ContentAlignment anyMiddleAlignment = (ContentAlignment)112;
	}
}
