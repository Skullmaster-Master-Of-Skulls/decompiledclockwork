using System;
using System.Drawing;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms
{
	// Token: 0x02000161 RID: 353
	public sealed class ComboBoxRenderer
	{
		// Token: 0x06000EA6 RID: 3750 RVA: 0x00002843 File Offset: 0x00000A43
		private ComboBoxRenderer()
		{
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06000EA7 RID: 3751 RVA: 0x0002BD47 File Offset: 0x00029F47
		public static bool IsSupported
		{
			get
			{
				return VisualStyleRenderer.IsSupported;
			}
		}

		// Token: 0x06000EA8 RID: 3752 RVA: 0x0002BD50 File Offset: 0x00029F50
		private static void DrawBackground(Graphics g, Rectangle bounds, ComboBoxState state)
		{
			ComboBoxRenderer.visualStyleRenderer.DrawBackground(g, bounds);
			if (state != ComboBoxState.Disabled)
			{
				Color color = ComboBoxRenderer.visualStyleRenderer.GetColor(ColorProperty.FillColor);
				if (color != SystemColors.Window)
				{
					Rectangle backgroundContentRectangle = ComboBoxRenderer.visualStyleRenderer.GetBackgroundContentRectangle(g, bounds);
					backgroundContentRectangle.Inflate(-2, -2);
					g.FillRectangle(SystemBrushes.Window, backgroundContentRectangle);
				}
			}
		}

		// Token: 0x06000EA9 RID: 3753 RVA: 0x0002BDB0 File Offset: 0x00029FB0
		public static void DrawTextBox(Graphics g, Rectangle bounds, ComboBoxState state)
		{
			if (ComboBoxRenderer.visualStyleRenderer == null)
			{
				ComboBoxRenderer.visualStyleRenderer = new VisualStyleRenderer(ComboBoxRenderer.TextBoxElement.ClassName, ComboBoxRenderer.TextBoxElement.Part, (int)state);
			}
			else
			{
				ComboBoxRenderer.visualStyleRenderer.SetParameters(ComboBoxRenderer.TextBoxElement.ClassName, ComboBoxRenderer.TextBoxElement.Part, (int)state);
			}
			ComboBoxRenderer.DrawBackground(g, bounds, state);
		}

		// Token: 0x06000EAA RID: 3754 RVA: 0x0002BE0C File Offset: 0x0002A00C
		public static void DrawTextBox(Graphics g, Rectangle bounds, string comboBoxText, Font font, ComboBoxState state)
		{
			ComboBoxRenderer.DrawTextBox(g, bounds, comboBoxText, font, TextFormatFlags.TextBoxControl, state);
		}

		// Token: 0x06000EAB RID: 3755 RVA: 0x0002BE1E File Offset: 0x0002A01E
		public static void DrawTextBox(Graphics g, Rectangle bounds, string comboBoxText, Font font, Rectangle textBounds, ComboBoxState state)
		{
			ComboBoxRenderer.DrawTextBox(g, bounds, comboBoxText, font, textBounds, TextFormatFlags.TextBoxControl, state);
		}

		// Token: 0x06000EAC RID: 3756 RVA: 0x0002BE34 File Offset: 0x0002A034
		public static void DrawTextBox(Graphics g, Rectangle bounds, string comboBoxText, Font font, TextFormatFlags flags, ComboBoxState state)
		{
			if (ComboBoxRenderer.visualStyleRenderer == null)
			{
				ComboBoxRenderer.visualStyleRenderer = new VisualStyleRenderer(ComboBoxRenderer.TextBoxElement.ClassName, ComboBoxRenderer.TextBoxElement.Part, (int)state);
			}
			else
			{
				ComboBoxRenderer.visualStyleRenderer.SetParameters(ComboBoxRenderer.TextBoxElement.ClassName, ComboBoxRenderer.TextBoxElement.Part, (int)state);
			}
			Rectangle backgroundContentRectangle = ComboBoxRenderer.visualStyleRenderer.GetBackgroundContentRectangle(g, bounds);
			backgroundContentRectangle.Inflate(-2, -2);
			ComboBoxRenderer.DrawTextBox(g, bounds, comboBoxText, font, backgroundContentRectangle, flags, state);
		}

		// Token: 0x06000EAD RID: 3757 RVA: 0x0002BEB0 File Offset: 0x0002A0B0
		public static void DrawTextBox(Graphics g, Rectangle bounds, string comboBoxText, Font font, Rectangle textBounds, TextFormatFlags flags, ComboBoxState state)
		{
			if (ComboBoxRenderer.visualStyleRenderer == null)
			{
				ComboBoxRenderer.visualStyleRenderer = new VisualStyleRenderer(ComboBoxRenderer.TextBoxElement.ClassName, ComboBoxRenderer.TextBoxElement.Part, (int)state);
			}
			else
			{
				ComboBoxRenderer.visualStyleRenderer.SetParameters(ComboBoxRenderer.TextBoxElement.ClassName, ComboBoxRenderer.TextBoxElement.Part, (int)state);
			}
			ComboBoxRenderer.DrawBackground(g, bounds, state);
			Color color = ComboBoxRenderer.visualStyleRenderer.GetColor(ColorProperty.TextColor);
			TextRenderer.DrawText(g, comboBoxText, font, textBounds, color, flags);
		}

		// Token: 0x06000EAE RID: 3758 RVA: 0x0002BF2C File Offset: 0x0002A12C
		public static void DrawDropDownButton(Graphics g, Rectangle bounds, ComboBoxState state)
		{
			ComboBoxRenderer.DrawDropDownButtonForHandle(g, bounds, state, IntPtr.Zero);
		}

		// Token: 0x06000EAF RID: 3759 RVA: 0x0002BF3C File Offset: 0x0002A13C
		internal static void DrawDropDownButtonForHandle(Graphics g, Rectangle bounds, ComboBoxState state, IntPtr handle)
		{
			if (ComboBoxRenderer.visualStyleRenderer == null)
			{
				ComboBoxRenderer.visualStyleRenderer = new VisualStyleRenderer(ComboBoxRenderer.ComboBoxElement.ClassName, ComboBoxRenderer.ComboBoxElement.Part, (int)state);
			}
			else
			{
				ComboBoxRenderer.visualStyleRenderer.SetParameters(ComboBoxRenderer.ComboBoxElement.ClassName, ComboBoxRenderer.ComboBoxElement.Part, (int)state);
			}
			ComboBoxRenderer.visualStyleRenderer.DrawBackground(g, bounds, handle);
		}

		// Token: 0x040007ED RID: 2029
		[ThreadStatic]
		private static VisualStyleRenderer visualStyleRenderer = null;

		// Token: 0x040007EE RID: 2030
		private static readonly VisualStyleElement ComboBoxElement = VisualStyleElement.ComboBox.DropDownButton.Normal;

		// Token: 0x040007EF RID: 2031
		private static readonly VisualStyleElement TextBoxElement = VisualStyleElement.TextBox.TextEdit.Normal;
	}
}
