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
	// Token: 0x02000220 RID: 544
	internal static class DesignerUtils
	{
		// Token: 0x1700034B RID: 843
		// (get) Token: 0x0600146A RID: 5226 RVA: 0x0006766C File Offset: 0x0006666C
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

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x0600146B RID: 5227 RVA: 0x00067704 File Offset: 0x00066704
		public static Brush HoverBrush
		{
			get
			{
				return DesignerUtils.hoverBrush;
			}
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x0600146C RID: 5228 RVA: 0x0006770C File Offset: 0x0006670C
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

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x0600146D RID: 5229 RVA: 0x00067778 File Offset: 0x00066778
		public static Point LastCursorPoint
		{
			get
			{
				int messagePos = SafeNativeMethods.GetMessagePos();
				return new Point(NativeMethods.Util.SignedLOWORD(messagePos), NativeMethods.Util.SignedHIWORD(messagePos));
			}
		}

		// Token: 0x0600146E RID: 5230 RVA: 0x0006779C File Offset: 0x0006679C
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

		// Token: 0x0600146F RID: 5231 RVA: 0x0006788C File Offset: 0x0006688C
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

		// Token: 0x06001470 RID: 5232 RVA: 0x0006795C File Offset: 0x0006695C
		public static void DrawResizeBorder(Graphics g, Region resizeBorder, Color backColor)
		{
			Brush brush = SystemBrushes.ControlDarkDark;
			if (backColor != Color.Empty && (double)backColor.GetBrightness() < 0.5)
			{
				brush = SystemBrushes.ControlLight;
			}
			g.FillRegion(brush, resizeBorder);
		}

		// Token: 0x06001471 RID: 5233 RVA: 0x000679A0 File Offset: 0x000669A0
		public static void DrawFrame(Graphics g, Region resizeBorder, FrameStyle style, Color backColor)
		{
			Color color = SystemColors.ControlDarkDark;
			if (backColor != Color.Empty && (double)backColor.GetBrightness() < 0.5)
			{
				color = SystemColors.ControlLight;
			}
			Brush brush;
			switch (style)
			{
			case FrameStyle.Dashed:
				brush = new HatchBrush(HatchStyle.Percent50, color, Color.Transparent);
				goto IL_55;
			}
			brush = new SolidBrush(color);
			IL_55:
			g.FillRegion(brush, resizeBorder);
		}

		// Token: 0x06001472 RID: 5234 RVA: 0x00067A0C File Offset: 0x00066A0C
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

		// Token: 0x06001473 RID: 5235 RVA: 0x00067AD4 File Offset: 0x00066AD4
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

		// Token: 0x06001474 RID: 5236 RVA: 0x00067B90 File Offset: 0x00066B90
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

		// Token: 0x06001475 RID: 5237 RVA: 0x00067CAC File Offset: 0x00066CAC
		public static void DrawSelectionBorder(Graphics graphics, Rectangle bounds)
		{
			graphics.FillRectangle(DesignerUtils.selectionBorderBrush, bounds);
		}

		// Token: 0x06001476 RID: 5238 RVA: 0x00067CBC File Offset: 0x00066CBC
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

		// Token: 0x06001477 RID: 5239 RVA: 0x00067D38 File Offset: 0x00066D38
		public static Size GetAdornmentDimensions(AdornmentType adornmentType)
		{
			switch (adornmentType)
			{
			case AdornmentType.GrabHandle:
				return new Size(DesignerUtils.HANDLESIZE, DesignerUtils.HANDLESIZE);
			case AdornmentType.ContainerSelector:
			case AdornmentType.Maximum:
				return new Size(DesignerUtils.CONTAINERGRABHANDLESIZE, DesignerUtils.CONTAINERGRABHANDLESIZE);
			default:
				return new Size(0, 0);
			}
		}

		// Token: 0x06001478 RID: 5240 RVA: 0x00067D84 File Offset: 0x00066D84
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

		// Token: 0x06001479 RID: 5241 RVA: 0x00067DE4 File Offset: 0x00066DE4
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

		// Token: 0x0600147A RID: 5242 RVA: 0x00067E58 File Offset: 0x00066E58
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

		// Token: 0x0600147B RID: 5243 RVA: 0x00067F14 File Offset: 0x00066F14
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

		// Token: 0x0600147C RID: 5244 RVA: 0x0006804C File Offset: 0x0006704C
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

		// Token: 0x0600147D RID: 5245 RVA: 0x0006811C File Offset: 0x0006711C
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

		// Token: 0x0600147E RID: 5246 RVA: 0x000681CF File Offset: 0x000671CF
		public static Rectangle GetBoundsForSelectionType(Rectangle originalBounds, SelectionBorderGlyphType type)
		{
			return DesignerUtils.GetBoundsForSelectionType(originalBounds, type, DesignerUtils.SELECTIONBORDERSIZE, DesignerUtils.SELECTIONBORDEROFFSET);
		}

		// Token: 0x0600147F RID: 5247 RVA: 0x000681E2 File Offset: 0x000671E2
		public static Rectangle GetBoundsForNoResizeSelectionType(Rectangle originalBounds, SelectionBorderGlyphType type)
		{
			return DesignerUtils.GetBoundsForSelectionType(originalBounds, type, DesignerUtils.SELECTIONBORDERSIZE, DesignerUtils.NORESIZEBORDEROFFSET);
		}

		// Token: 0x06001480 RID: 5248 RVA: 0x000681F8 File Offset: 0x000671F8
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

		// Token: 0x06001481 RID: 5249 RVA: 0x00068308 File Offset: 0x00067308
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

		// Token: 0x06001482 RID: 5250 RVA: 0x00068440 File Offset: 0x00067440
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

		// Token: 0x06001483 RID: 5251 RVA: 0x000684BC File Offset: 0x000674BC
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

		// Token: 0x06001484 RID: 5252 RVA: 0x00068574 File Offset: 0x00067574
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

		// Token: 0x06001485 RID: 5253 RVA: 0x000685EC File Offset: 0x000675EC
		public static IContainer CheckForNestedContainer(IContainer container)
		{
			NestedContainer nestedContainer = container as NestedContainer;
			if (nestedContainer != null)
			{
				return nestedContainer.Owner.Site.Container;
			}
			return container;
		}

		// Token: 0x06001486 RID: 5254 RVA: 0x00068618 File Offset: 0x00067618
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

		// Token: 0x06001487 RID: 5255 RVA: 0x000687C4 File Offset: 0x000677C4
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

		// Token: 0x06001488 RID: 5256 RVA: 0x00068830 File Offset: 0x00067830
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

		// Token: 0x04001208 RID: 4616
		private static Size minDragSize = Size.Empty;

		// Token: 0x04001209 RID: 4617
		private static SolidBrush hoverBrush = new SolidBrush(Color.FromArgb(50, SystemColors.Highlight));

		// Token: 0x0400120A RID: 4618
		private static HatchBrush selectionBorderBrush = new HatchBrush(HatchStyle.Percent50, SystemColors.ControlDarkDark, Color.Transparent);

		// Token: 0x0400120B RID: 4619
		private static IntPtr grabHandleFillBrushPrimary = SafeNativeMethods.CreateSolidBrush(ColorTranslator.ToWin32(SystemColors.Window));

		// Token: 0x0400120C RID: 4620
		private static IntPtr grabHandleFillBrush = SafeNativeMethods.CreateSolidBrush(ColorTranslator.ToWin32(SystemColors.ControlText));

		// Token: 0x0400120D RID: 4621
		private static IntPtr grabHandlePenPrimary = SafeNativeMethods.CreatePen(NativeMethods.PS_SOLID, 1, ColorTranslator.ToWin32(SystemColors.ControlText));

		// Token: 0x0400120E RID: 4622
		private static IntPtr grabHandlePen = SafeNativeMethods.CreatePen(NativeMethods.PS_SOLID, 1, ColorTranslator.ToWin32(SystemColors.Window));

		// Token: 0x0400120F RID: 4623
		private static Bitmap boxImage = null;

		// Token: 0x04001210 RID: 4624
		public static int BOXIMAGESIZE = 16;

		// Token: 0x04001211 RID: 4625
		public static int SELECTIONBORDERSIZE = 1;

		// Token: 0x04001212 RID: 4626
		public static int SELECTIONBORDERHITAREA = 3;

		// Token: 0x04001213 RID: 4627
		public static int HANDLESIZE = 7;

		// Token: 0x04001214 RID: 4628
		public static int HANDLEOVERLAP = 2;

		// Token: 0x04001215 RID: 4629
		public static int SELECTIONBORDEROFFSET = (DesignerUtils.HANDLESIZE - DesignerUtils.SELECTIONBORDERSIZE) / 2 - DesignerUtils.HANDLEOVERLAP;

		// Token: 0x04001216 RID: 4630
		public static int NORESIZEHANDLESIZE = 5;

		// Token: 0x04001217 RID: 4631
		public static int NORESIZEBORDEROFFSET = (DesignerUtils.NORESIZEHANDLESIZE - DesignerUtils.SELECTIONBORDERSIZE) / 2;

		// Token: 0x04001218 RID: 4632
		public static int LOCKHANDLEHEIGHT = 9;

		// Token: 0x04001219 RID: 4633
		public static int LOCKHANDLEWIDTH = 7;

		// Token: 0x0400121A RID: 4634
		public static int LOCKHANDLEOVERLAP = 2;

		// Token: 0x0400121B RID: 4635
		public static int LOCKEDSELECTIONBORDEROFFSET_Y = (DesignerUtils.LOCKHANDLEHEIGHT - DesignerUtils.SELECTIONBORDERSIZE) / 2 - DesignerUtils.LOCKHANDLEOVERLAP;

		// Token: 0x0400121C RID: 4636
		public static int LOCKEDSELECTIONBORDEROFFSET_X = (DesignerUtils.LOCKHANDLEWIDTH - DesignerUtils.SELECTIONBORDERSIZE) / 2 - DesignerUtils.LOCKHANDLEOVERLAP;

		// Token: 0x0400121D RID: 4637
		public static int LOCKHANDLESIZE_UPPER = 5;

		// Token: 0x0400121E RID: 4638
		public static int LOCKHANDLEHEIGHT_LOWER = 6;

		// Token: 0x0400121F RID: 4639
		public static int LOCKHANDLEWIDTH_LOWER = 7;

		// Token: 0x04001220 RID: 4640
		public static int LOCKHANDLEUPPER_OFFSET = (DesignerUtils.LOCKHANDLEWIDTH_LOWER - DesignerUtils.LOCKHANDLESIZE_UPPER) / 2;

		// Token: 0x04001221 RID: 4641
		public static int LOCKHANDLELOWER_OFFSET = DesignerUtils.LOCKHANDLEHEIGHT - DesignerUtils.LOCKHANDLEHEIGHT_LOWER;

		// Token: 0x04001222 RID: 4642
		public static int CONTAINERGRABHANDLESIZE = 15;

		// Token: 0x04001223 RID: 4643
		public static int SNAPELINEDELAY = 1000;

		// Token: 0x04001224 RID: 4644
		public static int MINIMUMSTYLESIZE = 20;

		// Token: 0x04001225 RID: 4645
		public static int MINIMUMSTYLEPERCENT = 50;

		// Token: 0x04001226 RID: 4646
		public static int MINCONTROLBITMAPSIZE = 1;

		// Token: 0x04001227 RID: 4647
		public static int MINUMUMSTYLESIZEDRAG = 8;

		// Token: 0x04001228 RID: 4648
		public static int DEFAULTROWCOUNT = 2;

		// Token: 0x04001229 RID: 4649
		public static int DEFAULTCOLUMNCOUNT = 2;

		// Token: 0x0400122A RID: 4650
		public static int RESIZEGLYPHSIZE = 4;

		// Token: 0x0400122B RID: 4651
		public static int DEFAULTFORMPADDING = 9;

		// Token: 0x0400122C RID: 4652
		public static readonly ContentAlignment anyTopAlignment = (ContentAlignment)7;

		// Token: 0x0400122D RID: 4653
		public static readonly ContentAlignment anyMiddleAlignment = (ContentAlignment)112;
	}
}
