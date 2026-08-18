using System;
using System.Drawing;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms
{
	// Token: 0x02000146 RID: 326
	public sealed class ButtonRenderer
	{
		// Token: 0x06000CC7 RID: 3271 RVA: 0x00002843 File Offset: 0x00000A43
		private ButtonRenderer()
		{
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06000CC8 RID: 3272 RVA: 0x00024B6C File Offset: 0x00022D6C
		// (set) Token: 0x06000CC9 RID: 3273 RVA: 0x00024B73 File Offset: 0x00022D73
		public static bool RenderMatchingApplicationState
		{
			get
			{
				return ButtonRenderer.renderMatchingApplicationState;
			}
			set
			{
				ButtonRenderer.renderMatchingApplicationState = value;
			}
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000CCA RID: 3274 RVA: 0x00024B7B File Offset: 0x00022D7B
		private static bool RenderWithVisualStyles
		{
			get
			{
				return !ButtonRenderer.renderMatchingApplicationState || Application.RenderWithVisualStyles;
			}
		}

		// Token: 0x06000CCB RID: 3275 RVA: 0x00024B8B File Offset: 0x00022D8B
		public static bool IsBackgroundPartiallyTransparent(PushButtonState state)
		{
			if (ButtonRenderer.RenderWithVisualStyles)
			{
				ButtonRenderer.InitializeRenderer((int)state);
				return ButtonRenderer.visualStyleRenderer.IsBackgroundPartiallyTransparent();
			}
			return false;
		}

		// Token: 0x06000CCC RID: 3276 RVA: 0x00024BA6 File Offset: 0x00022DA6
		public static void DrawParentBackground(Graphics g, Rectangle bounds, Control childControl)
		{
			if (ButtonRenderer.RenderWithVisualStyles)
			{
				ButtonRenderer.InitializeRenderer(0);
				ButtonRenderer.visualStyleRenderer.DrawParentBackground(g, bounds, childControl);
			}
		}

		// Token: 0x06000CCD RID: 3277 RVA: 0x00024BC2 File Offset: 0x00022DC2
		public static void DrawButton(Graphics g, Rectangle bounds, PushButtonState state)
		{
			if (ButtonRenderer.RenderWithVisualStyles)
			{
				ButtonRenderer.InitializeRenderer((int)state);
				ButtonRenderer.visualStyleRenderer.DrawBackground(g, bounds);
				return;
			}
			ControlPaint.DrawButton(g, bounds, ButtonRenderer.ConvertToButtonState(state));
		}

		// Token: 0x06000CCE RID: 3278 RVA: 0x00024BEC File Offset: 0x00022DEC
		internal static void DrawButtonForHandle(Graphics g, Rectangle bounds, bool focused, PushButtonState state, IntPtr handle)
		{
			Rectangle rectangle;
			if (ButtonRenderer.RenderWithVisualStyles)
			{
				ButtonRenderer.InitializeRenderer((int)state);
				ButtonRenderer.visualStyleRenderer.DrawBackground(g, bounds, handle);
				rectangle = ButtonRenderer.visualStyleRenderer.GetBackgroundContentRectangle(g, bounds);
			}
			else
			{
				ControlPaint.DrawButton(g, bounds, ButtonRenderer.ConvertToButtonState(state));
				rectangle = Rectangle.Inflate(bounds, -3, -3);
			}
			if (focused)
			{
				ControlPaint.DrawFocusRectangle(g, rectangle);
			}
		}

		// Token: 0x06000CCF RID: 3279 RVA: 0x00024C45 File Offset: 0x00022E45
		public static void DrawButton(Graphics g, Rectangle bounds, bool focused, PushButtonState state)
		{
			ButtonRenderer.DrawButtonForHandle(g, bounds, focused, state, IntPtr.Zero);
		}

		// Token: 0x06000CD0 RID: 3280 RVA: 0x00024C55 File Offset: 0x00022E55
		public static void DrawButton(Graphics g, Rectangle bounds, string buttonText, Font font, bool focused, PushButtonState state)
		{
			ButtonRenderer.DrawButton(g, bounds, buttonText, font, TextFormatFlags.HorizontalCenter | TextFormatFlags.SingleLine | TextFormatFlags.VerticalCenter, focused, state);
		}

		// Token: 0x06000CD1 RID: 3281 RVA: 0x00024C68 File Offset: 0x00022E68
		public static void DrawButton(Graphics g, Rectangle bounds, string buttonText, Font font, TextFormatFlags flags, bool focused, PushButtonState state)
		{
			Rectangle rectangle;
			Color foreColor;
			if (ButtonRenderer.RenderWithVisualStyles)
			{
				ButtonRenderer.InitializeRenderer((int)state);
				ButtonRenderer.visualStyleRenderer.DrawBackground(g, bounds);
				rectangle = ButtonRenderer.visualStyleRenderer.GetBackgroundContentRectangle(g, bounds);
				foreColor = ButtonRenderer.visualStyleRenderer.GetColor(ColorProperty.TextColor);
			}
			else
			{
				ControlPaint.DrawButton(g, bounds, ButtonRenderer.ConvertToButtonState(state));
				rectangle = Rectangle.Inflate(bounds, -3, -3);
				foreColor = SystemColors.ControlText;
			}
			TextRenderer.DrawText(g, buttonText, font, rectangle, foreColor, flags);
			if (focused)
			{
				ControlPaint.DrawFocusRectangle(g, rectangle);
			}
		}

		// Token: 0x06000CD2 RID: 3282 RVA: 0x00024CE4 File Offset: 0x00022EE4
		public static void DrawButton(Graphics g, Rectangle bounds, Image image, Rectangle imageBounds, bool focused, PushButtonState state)
		{
			Rectangle rectangle;
			if (ButtonRenderer.RenderWithVisualStyles)
			{
				ButtonRenderer.InitializeRenderer((int)state);
				ButtonRenderer.visualStyleRenderer.DrawBackground(g, bounds);
				ButtonRenderer.visualStyleRenderer.DrawImage(g, imageBounds, image);
				rectangle = ButtonRenderer.visualStyleRenderer.GetBackgroundContentRectangle(g, bounds);
			}
			else
			{
				ControlPaint.DrawButton(g, bounds, ButtonRenderer.ConvertToButtonState(state));
				g.DrawImage(image, imageBounds);
				rectangle = Rectangle.Inflate(bounds, -3, -3);
			}
			if (focused)
			{
				ControlPaint.DrawFocusRectangle(g, rectangle);
			}
		}

		// Token: 0x06000CD3 RID: 3283 RVA: 0x00024D54 File Offset: 0x00022F54
		public static void DrawButton(Graphics g, Rectangle bounds, string buttonText, Font font, Image image, Rectangle imageBounds, bool focused, PushButtonState state)
		{
			ButtonRenderer.DrawButton(g, bounds, buttonText, font, TextFormatFlags.HorizontalCenter | TextFormatFlags.SingleLine | TextFormatFlags.VerticalCenter, image, imageBounds, focused, state);
		}

		// Token: 0x06000CD4 RID: 3284 RVA: 0x00024D74 File Offset: 0x00022F74
		public static void DrawButton(Graphics g, Rectangle bounds, string buttonText, Font font, TextFormatFlags flags, Image image, Rectangle imageBounds, bool focused, PushButtonState state)
		{
			Rectangle rectangle;
			Color foreColor;
			if (ButtonRenderer.RenderWithVisualStyles)
			{
				ButtonRenderer.InitializeRenderer((int)state);
				ButtonRenderer.visualStyleRenderer.DrawBackground(g, bounds);
				ButtonRenderer.visualStyleRenderer.DrawImage(g, imageBounds, image);
				rectangle = ButtonRenderer.visualStyleRenderer.GetBackgroundContentRectangle(g, bounds);
				foreColor = ButtonRenderer.visualStyleRenderer.GetColor(ColorProperty.TextColor);
			}
			else
			{
				ControlPaint.DrawButton(g, bounds, ButtonRenderer.ConvertToButtonState(state));
				g.DrawImage(image, imageBounds);
				rectangle = Rectangle.Inflate(bounds, -3, -3);
				foreColor = SystemColors.ControlText;
			}
			TextRenderer.DrawText(g, buttonText, font, rectangle, foreColor, flags);
			if (focused)
			{
				ControlPaint.DrawFocusRectangle(g, rectangle);
			}
		}

		// Token: 0x06000CD5 RID: 3285 RVA: 0x00024E09 File Offset: 0x00023009
		internal static ButtonState ConvertToButtonState(PushButtonState state)
		{
			if (state == PushButtonState.Pressed)
			{
				return ButtonState.Pushed;
			}
			if (state != PushButtonState.Disabled)
			{
				return ButtonState.Normal;
			}
			return ButtonState.Inactive;
		}

		// Token: 0x06000CD6 RID: 3286 RVA: 0x00024E24 File Offset: 0x00023024
		private static void InitializeRenderer(int state)
		{
			if (ButtonRenderer.visualStyleRenderer == null)
			{
				ButtonRenderer.visualStyleRenderer = new VisualStyleRenderer(ButtonRenderer.ButtonElement.ClassName, ButtonRenderer.ButtonElement.Part, state);
				return;
			}
			ButtonRenderer.visualStyleRenderer.SetParameters(ButtonRenderer.ButtonElement.ClassName, ButtonRenderer.ButtonElement.Part, state);
		}

		// Token: 0x04000750 RID: 1872
		[ThreadStatic]
		private static VisualStyleRenderer visualStyleRenderer = null;

		// Token: 0x04000751 RID: 1873
		private static readonly VisualStyleElement ButtonElement = VisualStyleElement.Button.PushButton.Normal;

		// Token: 0x04000752 RID: 1874
		private static bool renderMatchingApplicationState = true;
	}
}
