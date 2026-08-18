using System;
using System.Drawing;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms
{
	// Token: 0x02000358 RID: 856
	public sealed class ScrollBarRenderer
	{
		// Token: 0x06003845 RID: 14405 RVA: 0x00002843 File Offset: 0x00000A43
		private ScrollBarRenderer()
		{
		}

		// Token: 0x17000D56 RID: 3414
		// (get) Token: 0x06003846 RID: 14406 RVA: 0x0002BD47 File Offset: 0x00029F47
		public static bool IsSupported
		{
			get
			{
				return VisualStyleRenderer.IsSupported;
			}
		}

		// Token: 0x06003847 RID: 14407 RVA: 0x000FA3C6 File Offset: 0x000F85C6
		public static void DrawArrowButton(Graphics g, Rectangle bounds, ScrollBarArrowButtonState state)
		{
			ScrollBarRenderer.InitializeRenderer(VisualStyleElement.ScrollBar.ArrowButton.LeftNormal, (int)state);
			ScrollBarRenderer.visualStyleRenderer.DrawBackground(g, bounds);
		}

		// Token: 0x06003848 RID: 14408 RVA: 0x000FA3DF File Offset: 0x000F85DF
		public static void DrawHorizontalThumb(Graphics g, Rectangle bounds, ScrollBarState state)
		{
			ScrollBarRenderer.InitializeRenderer(VisualStyleElement.ScrollBar.ThumbButtonHorizontal.Normal, (int)state);
			ScrollBarRenderer.visualStyleRenderer.DrawBackground(g, bounds);
		}

		// Token: 0x06003849 RID: 14409 RVA: 0x000FA3F8 File Offset: 0x000F85F8
		public static void DrawVerticalThumb(Graphics g, Rectangle bounds, ScrollBarState state)
		{
			ScrollBarRenderer.InitializeRenderer(VisualStyleElement.ScrollBar.ThumbButtonVertical.Normal, (int)state);
			ScrollBarRenderer.visualStyleRenderer.DrawBackground(g, bounds);
		}

		// Token: 0x0600384A RID: 14410 RVA: 0x000FA411 File Offset: 0x000F8611
		public static void DrawHorizontalThumbGrip(Graphics g, Rectangle bounds, ScrollBarState state)
		{
			ScrollBarRenderer.InitializeRenderer(VisualStyleElement.ScrollBar.GripperHorizontal.Normal, (int)state);
			ScrollBarRenderer.visualStyleRenderer.DrawBackground(g, bounds);
		}

		// Token: 0x0600384B RID: 14411 RVA: 0x000FA42A File Offset: 0x000F862A
		public static void DrawVerticalThumbGrip(Graphics g, Rectangle bounds, ScrollBarState state)
		{
			ScrollBarRenderer.InitializeRenderer(VisualStyleElement.ScrollBar.GripperVertical.Normal, (int)state);
			ScrollBarRenderer.visualStyleRenderer.DrawBackground(g, bounds);
		}

		// Token: 0x0600384C RID: 14412 RVA: 0x000FA443 File Offset: 0x000F8643
		public static void DrawRightHorizontalTrack(Graphics g, Rectangle bounds, ScrollBarState state)
		{
			ScrollBarRenderer.InitializeRenderer(VisualStyleElement.ScrollBar.RightTrackHorizontal.Normal, (int)state);
			ScrollBarRenderer.visualStyleRenderer.DrawBackground(g, bounds);
		}

		// Token: 0x0600384D RID: 14413 RVA: 0x000FA45C File Offset: 0x000F865C
		public static void DrawLeftHorizontalTrack(Graphics g, Rectangle bounds, ScrollBarState state)
		{
			ScrollBarRenderer.InitializeRenderer(VisualStyleElement.ScrollBar.LeftTrackHorizontal.Normal, (int)state);
			ScrollBarRenderer.visualStyleRenderer.DrawBackground(g, bounds);
		}

		// Token: 0x0600384E RID: 14414 RVA: 0x000FA475 File Offset: 0x000F8675
		public static void DrawUpperVerticalTrack(Graphics g, Rectangle bounds, ScrollBarState state)
		{
			ScrollBarRenderer.InitializeRenderer(VisualStyleElement.ScrollBar.UpperTrackVertical.Normal, (int)state);
			ScrollBarRenderer.visualStyleRenderer.DrawBackground(g, bounds);
		}

		// Token: 0x0600384F RID: 14415 RVA: 0x000FA48E File Offset: 0x000F868E
		public static void DrawLowerVerticalTrack(Graphics g, Rectangle bounds, ScrollBarState state)
		{
			ScrollBarRenderer.InitializeRenderer(VisualStyleElement.ScrollBar.LowerTrackVertical.Normal, (int)state);
			ScrollBarRenderer.visualStyleRenderer.DrawBackground(g, bounds);
		}

		// Token: 0x06003850 RID: 14416 RVA: 0x000FA4A7 File Offset: 0x000F86A7
		public static void DrawSizeBox(Graphics g, Rectangle bounds, ScrollBarSizeBoxState state)
		{
			ScrollBarRenderer.InitializeRenderer(VisualStyleElement.ScrollBar.SizeBox.LeftAlign, (int)state);
			ScrollBarRenderer.visualStyleRenderer.DrawBackground(g, bounds);
		}

		// Token: 0x06003851 RID: 14417 RVA: 0x000FA4C0 File Offset: 0x000F86C0
		public static Size GetThumbGripSize(Graphics g, ScrollBarState state)
		{
			ScrollBarRenderer.InitializeRenderer(VisualStyleElement.ScrollBar.GripperHorizontal.Normal, (int)state);
			return ScrollBarRenderer.visualStyleRenderer.GetPartSize(g, ThemeSizeType.True);
		}

		// Token: 0x06003852 RID: 14418 RVA: 0x000FA4D9 File Offset: 0x000F86D9
		public static Size GetSizeBoxSize(Graphics g, ScrollBarState state)
		{
			ScrollBarRenderer.InitializeRenderer(VisualStyleElement.ScrollBar.SizeBox.LeftAlign, (int)state);
			return ScrollBarRenderer.visualStyleRenderer.GetPartSize(g, ThemeSizeType.True);
		}

		// Token: 0x06003853 RID: 14419 RVA: 0x000FA4F2 File Offset: 0x000F86F2
		private static void InitializeRenderer(VisualStyleElement element, int state)
		{
			if (ScrollBarRenderer.visualStyleRenderer == null)
			{
				ScrollBarRenderer.visualStyleRenderer = new VisualStyleRenderer(element.ClassName, element.Part, state);
				return;
			}
			ScrollBarRenderer.visualStyleRenderer.SetParameters(element.ClassName, element.Part, state);
		}

		// Token: 0x04002191 RID: 8593
		[ThreadStatic]
		private static VisualStyleRenderer visualStyleRenderer;
	}
}
