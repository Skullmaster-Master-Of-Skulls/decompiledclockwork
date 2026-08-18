using System;
using System.Drawing;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms
{
	// Token: 0x020003A3 RID: 931
	public sealed class TextBoxRenderer
	{
		// Token: 0x06003CE8 RID: 15592 RVA: 0x00002843 File Offset: 0x00000A43
		private TextBoxRenderer()
		{
		}

		// Token: 0x17000ED7 RID: 3799
		// (get) Token: 0x06003CE9 RID: 15593 RVA: 0x0002BD47 File Offset: 0x00029F47
		public static bool IsSupported
		{
			get
			{
				return VisualStyleRenderer.IsSupported;
			}
		}

		// Token: 0x06003CEA RID: 15594 RVA: 0x00108588 File Offset: 0x00106788
		private static void DrawBackground(Graphics g, Rectangle bounds, TextBoxState state)
		{
			TextBoxRenderer.visualStyleRenderer.DrawBackground(g, bounds);
			if (state != TextBoxState.Disabled)
			{
				Color color = TextBoxRenderer.visualStyleRenderer.GetColor(ColorProperty.FillColor);
				if (color != SystemColors.Window)
				{
					Rectangle backgroundContentRectangle = TextBoxRenderer.visualStyleRenderer.GetBackgroundContentRectangle(g, bounds);
					using (SolidBrush solidBrush = new SolidBrush(SystemColors.Window))
					{
						g.FillRectangle(solidBrush, backgroundContentRectangle);
					}
				}
			}
		}

		// Token: 0x06003CEB RID: 15595 RVA: 0x00108600 File Offset: 0x00106800
		public static void DrawTextBox(Graphics g, Rectangle bounds, TextBoxState state)
		{
			TextBoxRenderer.InitializeRenderer((int)state);
			TextBoxRenderer.DrawBackground(g, bounds, state);
		}

		// Token: 0x06003CEC RID: 15596 RVA: 0x00108610 File Offset: 0x00106810
		public static void DrawTextBox(Graphics g, Rectangle bounds, string textBoxText, Font font, TextBoxState state)
		{
			TextBoxRenderer.DrawTextBox(g, bounds, textBoxText, font, TextFormatFlags.TextBoxControl, state);
		}

		// Token: 0x06003CED RID: 15597 RVA: 0x00108622 File Offset: 0x00106822
		public static void DrawTextBox(Graphics g, Rectangle bounds, string textBoxText, Font font, Rectangle textBounds, TextBoxState state)
		{
			TextBoxRenderer.DrawTextBox(g, bounds, textBoxText, font, textBounds, TextFormatFlags.TextBoxControl, state);
		}

		// Token: 0x06003CEE RID: 15598 RVA: 0x00108638 File Offset: 0x00106838
		public static void DrawTextBox(Graphics g, Rectangle bounds, string textBoxText, Font font, TextFormatFlags flags, TextBoxState state)
		{
			TextBoxRenderer.InitializeRenderer((int)state);
			Rectangle backgroundContentRectangle = TextBoxRenderer.visualStyleRenderer.GetBackgroundContentRectangle(g, bounds);
			backgroundContentRectangle.Inflate(-2, -2);
			TextBoxRenderer.DrawTextBox(g, bounds, textBoxText, font, backgroundContentRectangle, flags, state);
		}

		// Token: 0x06003CEF RID: 15599 RVA: 0x00108674 File Offset: 0x00106874
		public static void DrawTextBox(Graphics g, Rectangle bounds, string textBoxText, Font font, Rectangle textBounds, TextFormatFlags flags, TextBoxState state)
		{
			TextBoxRenderer.InitializeRenderer((int)state);
			TextBoxRenderer.DrawBackground(g, bounds, state);
			Color color = TextBoxRenderer.visualStyleRenderer.GetColor(ColorProperty.TextColor);
			TextRenderer.DrawText(g, textBoxText, font, textBounds, color, flags);
		}

		// Token: 0x06003CF0 RID: 15600 RVA: 0x001086B0 File Offset: 0x001068B0
		private static void InitializeRenderer(int state)
		{
			if (TextBoxRenderer.visualStyleRenderer == null)
			{
				TextBoxRenderer.visualStyleRenderer = new VisualStyleRenderer(TextBoxRenderer.TextBoxElement.ClassName, TextBoxRenderer.TextBoxElement.Part, state);
				return;
			}
			TextBoxRenderer.visualStyleRenderer.SetParameters(TextBoxRenderer.TextBoxElement.ClassName, TextBoxRenderer.TextBoxElement.Part, state);
		}

		// Token: 0x040023BB RID: 9147
		[ThreadStatic]
		private static VisualStyleRenderer visualStyleRenderer = null;

		// Token: 0x040023BC RID: 9148
		private static readonly VisualStyleElement TextBoxElement = VisualStyleElement.TextBox.TextEdit.Normal;
	}
}
