using System;
using System.Drawing;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms
{
	// Token: 0x0200039F RID: 927
	public sealed class TabRenderer
	{
		// Token: 0x06003CA8 RID: 15528 RVA: 0x00002843 File Offset: 0x00000A43
		private TabRenderer()
		{
		}

		// Token: 0x17000EC9 RID: 3785
		// (get) Token: 0x06003CA9 RID: 15529 RVA: 0x0002BD47 File Offset: 0x00029F47
		public static bool IsSupported
		{
			get
			{
				return VisualStyleRenderer.IsSupported;
			}
		}

		// Token: 0x06003CAA RID: 15530 RVA: 0x0010780F File Offset: 0x00105A0F
		public static void DrawTabItem(Graphics g, Rectangle bounds, TabItemState state)
		{
			TabRenderer.InitializeRenderer(VisualStyleElement.Tab.TabItem.Normal, (int)state);
			TabRenderer.visualStyleRenderer.DrawBackground(g, bounds);
		}

		// Token: 0x06003CAB RID: 15531 RVA: 0x00107828 File Offset: 0x00105A28
		public static void DrawTabItem(Graphics g, Rectangle bounds, bool focused, TabItemState state)
		{
			TabRenderer.InitializeRenderer(VisualStyleElement.Tab.TabItem.Normal, (int)state);
			TabRenderer.visualStyleRenderer.DrawBackground(g, bounds);
			Rectangle rectangle = Rectangle.Inflate(bounds, -3, -3);
			if (focused)
			{
				ControlPaint.DrawFocusRectangle(g, rectangle);
			}
		}

		// Token: 0x06003CAC RID: 15532 RVA: 0x00107861 File Offset: 0x00105A61
		public static void DrawTabItem(Graphics g, Rectangle bounds, string tabItemText, Font font, TabItemState state)
		{
			TabRenderer.DrawTabItem(g, bounds, tabItemText, font, false, state);
		}

		// Token: 0x06003CAD RID: 15533 RVA: 0x0010786F File Offset: 0x00105A6F
		public static void DrawTabItem(Graphics g, Rectangle bounds, string tabItemText, Font font, bool focused, TabItemState state)
		{
			TabRenderer.DrawTabItem(g, bounds, tabItemText, font, TextFormatFlags.HorizontalCenter | TextFormatFlags.SingleLine | TextFormatFlags.VerticalCenter, focused, state);
		}

		// Token: 0x06003CAE RID: 15534 RVA: 0x00107880 File Offset: 0x00105A80
		public static void DrawTabItem(Graphics g, Rectangle bounds, string tabItemText, Font font, TextFormatFlags flags, bool focused, TabItemState state)
		{
			TabRenderer.InitializeRenderer(VisualStyleElement.Tab.TabItem.Normal, (int)state);
			TabRenderer.visualStyleRenderer.DrawBackground(g, bounds);
			Rectangle rectangle = Rectangle.Inflate(bounds, -3, -3);
			Color color = TabRenderer.visualStyleRenderer.GetColor(ColorProperty.TextColor);
			TextRenderer.DrawText(g, tabItemText, font, rectangle, color, flags);
			if (focused)
			{
				ControlPaint.DrawFocusRectangle(g, rectangle);
			}
		}

		// Token: 0x06003CAF RID: 15535 RVA: 0x001078D8 File Offset: 0x00105AD8
		public static void DrawTabItem(Graphics g, Rectangle bounds, Image image, Rectangle imageRectangle, bool focused, TabItemState state)
		{
			TabRenderer.InitializeRenderer(VisualStyleElement.Tab.TabItem.Normal, (int)state);
			TabRenderer.visualStyleRenderer.DrawBackground(g, bounds);
			Rectangle rectangle = Rectangle.Inflate(bounds, -3, -3);
			TabRenderer.visualStyleRenderer.DrawImage(g, imageRectangle, image);
			if (focused)
			{
				ControlPaint.DrawFocusRectangle(g, rectangle);
			}
		}

		// Token: 0x06003CB0 RID: 15536 RVA: 0x00107920 File Offset: 0x00105B20
		public static void DrawTabItem(Graphics g, Rectangle bounds, string tabItemText, Font font, Image image, Rectangle imageRectangle, bool focused, TabItemState state)
		{
			TabRenderer.DrawTabItem(g, bounds, tabItemText, font, TextFormatFlags.HorizontalCenter | TextFormatFlags.SingleLine | TextFormatFlags.VerticalCenter, image, imageRectangle, focused, state);
		}

		// Token: 0x06003CB1 RID: 15537 RVA: 0x00107940 File Offset: 0x00105B40
		public static void DrawTabItem(Graphics g, Rectangle bounds, string tabItemText, Font font, TextFormatFlags flags, Image image, Rectangle imageRectangle, bool focused, TabItemState state)
		{
			TabRenderer.InitializeRenderer(VisualStyleElement.Tab.TabItem.Normal, (int)state);
			TabRenderer.visualStyleRenderer.DrawBackground(g, bounds);
			Rectangle rectangle = Rectangle.Inflate(bounds, -3, -3);
			TabRenderer.visualStyleRenderer.DrawImage(g, imageRectangle, image);
			Color color = TabRenderer.visualStyleRenderer.GetColor(ColorProperty.TextColor);
			TextRenderer.DrawText(g, tabItemText, font, rectangle, color, flags);
			if (focused)
			{
				ControlPaint.DrawFocusRectangle(g, rectangle);
			}
		}

		// Token: 0x06003CB2 RID: 15538 RVA: 0x001079A6 File Offset: 0x00105BA6
		public static void DrawTabPage(Graphics g, Rectangle bounds)
		{
			TabRenderer.InitializeRenderer(VisualStyleElement.Tab.Pane.Normal, 0);
			TabRenderer.visualStyleRenderer.DrawBackground(g, bounds);
		}

		// Token: 0x06003CB3 RID: 15539 RVA: 0x001079BF File Offset: 0x00105BBF
		private static void InitializeRenderer(VisualStyleElement element, int state)
		{
			if (TabRenderer.visualStyleRenderer == null)
			{
				TabRenderer.visualStyleRenderer = new VisualStyleRenderer(element.ClassName, element.Part, state);
				return;
			}
			TabRenderer.visualStyleRenderer.SetParameters(element.ClassName, element.Part, state);
		}

		// Token: 0x040023A9 RID: 9129
		[ThreadStatic]
		private static VisualStyleRenderer visualStyleRenderer;
	}
}
