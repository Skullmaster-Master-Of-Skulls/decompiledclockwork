using System;
using System.Drawing;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms
{
	// Token: 0x0200040F RID: 1039
	public sealed class TrackBarRenderer
	{
		// Token: 0x06004847 RID: 18503 RVA: 0x00002843 File Offset: 0x00000A43
		private TrackBarRenderer()
		{
		}

		// Token: 0x170011B2 RID: 4530
		// (get) Token: 0x06004848 RID: 18504 RVA: 0x0002BD47 File Offset: 0x00029F47
		public static bool IsSupported
		{
			get
			{
				return VisualStyleRenderer.IsSupported;
			}
		}

		// Token: 0x06004849 RID: 18505 RVA: 0x00130490 File Offset: 0x0012E690
		public static void DrawHorizontalTrack(Graphics g, Rectangle bounds)
		{
			TrackBarRenderer.InitializeRenderer(VisualStyleElement.TrackBar.Track.Normal, 1);
			TrackBarRenderer.visualStyleRenderer.DrawBackground(g, bounds);
		}

		// Token: 0x0600484A RID: 18506 RVA: 0x001304A9 File Offset: 0x0012E6A9
		public static void DrawVerticalTrack(Graphics g, Rectangle bounds)
		{
			TrackBarRenderer.InitializeRenderer(VisualStyleElement.TrackBar.TrackVertical.Normal, 1);
			TrackBarRenderer.visualStyleRenderer.DrawBackground(g, bounds);
		}

		// Token: 0x0600484B RID: 18507 RVA: 0x001304C2 File Offset: 0x0012E6C2
		public static void DrawHorizontalThumb(Graphics g, Rectangle bounds, TrackBarThumbState state)
		{
			TrackBarRenderer.InitializeRenderer(VisualStyleElement.TrackBar.Thumb.Normal, (int)state);
			TrackBarRenderer.visualStyleRenderer.DrawBackground(g, bounds);
		}

		// Token: 0x0600484C RID: 18508 RVA: 0x001304DB File Offset: 0x0012E6DB
		public static void DrawVerticalThumb(Graphics g, Rectangle bounds, TrackBarThumbState state)
		{
			TrackBarRenderer.InitializeRenderer(VisualStyleElement.TrackBar.ThumbVertical.Normal, (int)state);
			TrackBarRenderer.visualStyleRenderer.DrawBackground(g, bounds);
		}

		// Token: 0x0600484D RID: 18509 RVA: 0x001304F4 File Offset: 0x0012E6F4
		public static void DrawLeftPointingThumb(Graphics g, Rectangle bounds, TrackBarThumbState state)
		{
			TrackBarRenderer.InitializeRenderer(VisualStyleElement.TrackBar.ThumbLeft.Normal, (int)state);
			TrackBarRenderer.visualStyleRenderer.DrawBackground(g, bounds);
		}

		// Token: 0x0600484E RID: 18510 RVA: 0x0013050D File Offset: 0x0012E70D
		public static void DrawRightPointingThumb(Graphics g, Rectangle bounds, TrackBarThumbState state)
		{
			TrackBarRenderer.InitializeRenderer(VisualStyleElement.TrackBar.ThumbRight.Normal, (int)state);
			TrackBarRenderer.visualStyleRenderer.DrawBackground(g, bounds);
		}

		// Token: 0x0600484F RID: 18511 RVA: 0x00130526 File Offset: 0x0012E726
		public static void DrawTopPointingThumb(Graphics g, Rectangle bounds, TrackBarThumbState state)
		{
			TrackBarRenderer.InitializeRenderer(VisualStyleElement.TrackBar.ThumbTop.Normal, (int)state);
			TrackBarRenderer.visualStyleRenderer.DrawBackground(g, bounds);
		}

		// Token: 0x06004850 RID: 18512 RVA: 0x0013053F File Offset: 0x0012E73F
		public static void DrawBottomPointingThumb(Graphics g, Rectangle bounds, TrackBarThumbState state)
		{
			TrackBarRenderer.InitializeRenderer(VisualStyleElement.TrackBar.ThumbBottom.Normal, (int)state);
			TrackBarRenderer.visualStyleRenderer.DrawBackground(g, bounds);
		}

		// Token: 0x06004851 RID: 18513 RVA: 0x00130558 File Offset: 0x0012E758
		public static void DrawHorizontalTicks(Graphics g, Rectangle bounds, int numTicks, EdgeStyle edgeStyle)
		{
			if (numTicks <= 0 || bounds.Height <= 0 || bounds.Width <= 0 || g == null)
			{
				return;
			}
			TrackBarRenderer.InitializeRenderer(VisualStyleElement.TrackBar.Ticks.Normal, 1);
			if (numTicks == 1)
			{
				TrackBarRenderer.visualStyleRenderer.DrawEdge(g, new Rectangle(bounds.X, bounds.Y, 2, bounds.Height), Edges.Left, edgeStyle, EdgeEffects.None);
				return;
			}
			float num = ((float)bounds.Width - 2f) / ((float)numTicks - 1f);
			while (numTicks > 0)
			{
				float num2 = (float)bounds.X + (float)(numTicks - 1) * num;
				TrackBarRenderer.visualStyleRenderer.DrawEdge(g, new Rectangle((int)Math.Round((double)num2), bounds.Y, 2, bounds.Height), Edges.Left, edgeStyle, EdgeEffects.None);
				numTicks--;
			}
		}

		// Token: 0x06004852 RID: 18514 RVA: 0x0013061C File Offset: 0x0012E81C
		public static void DrawVerticalTicks(Graphics g, Rectangle bounds, int numTicks, EdgeStyle edgeStyle)
		{
			if (numTicks <= 0 || bounds.Height <= 0 || bounds.Width <= 0 || g == null)
			{
				return;
			}
			TrackBarRenderer.InitializeRenderer(VisualStyleElement.TrackBar.TicksVertical.Normal, 1);
			if (numTicks == 1)
			{
				TrackBarRenderer.visualStyleRenderer.DrawEdge(g, new Rectangle(bounds.X, bounds.Y, bounds.Width, 2), Edges.Top, edgeStyle, EdgeEffects.None);
				return;
			}
			float num = ((float)bounds.Height - 2f) / ((float)numTicks - 1f);
			while (numTicks > 0)
			{
				float num2 = (float)bounds.Y + (float)(numTicks - 1) * num;
				TrackBarRenderer.visualStyleRenderer.DrawEdge(g, new Rectangle(bounds.X, (int)Math.Round((double)num2), bounds.Width, 2), Edges.Top, edgeStyle, EdgeEffects.None);
				numTicks--;
			}
		}

		// Token: 0x06004853 RID: 18515 RVA: 0x001306DD File Offset: 0x0012E8DD
		public static Size GetLeftPointingThumbSize(Graphics g, TrackBarThumbState state)
		{
			TrackBarRenderer.InitializeRenderer(VisualStyleElement.TrackBar.ThumbLeft.Normal, (int)state);
			return TrackBarRenderer.visualStyleRenderer.GetPartSize(g, ThemeSizeType.True);
		}

		// Token: 0x06004854 RID: 18516 RVA: 0x001306F6 File Offset: 0x0012E8F6
		public static Size GetRightPointingThumbSize(Graphics g, TrackBarThumbState state)
		{
			TrackBarRenderer.InitializeRenderer(VisualStyleElement.TrackBar.ThumbRight.Normal, (int)state);
			return TrackBarRenderer.visualStyleRenderer.GetPartSize(g, ThemeSizeType.True);
		}

		// Token: 0x06004855 RID: 18517 RVA: 0x0013070F File Offset: 0x0012E90F
		public static Size GetTopPointingThumbSize(Graphics g, TrackBarThumbState state)
		{
			TrackBarRenderer.InitializeRenderer(VisualStyleElement.TrackBar.ThumbTop.Normal, (int)state);
			return TrackBarRenderer.visualStyleRenderer.GetPartSize(g, ThemeSizeType.True);
		}

		// Token: 0x06004856 RID: 18518 RVA: 0x00130728 File Offset: 0x0012E928
		public static Size GetBottomPointingThumbSize(Graphics g, TrackBarThumbState state)
		{
			TrackBarRenderer.InitializeRenderer(VisualStyleElement.TrackBar.ThumbBottom.Normal, (int)state);
			return TrackBarRenderer.visualStyleRenderer.GetPartSize(g, ThemeSizeType.True);
		}

		// Token: 0x06004857 RID: 18519 RVA: 0x00130741 File Offset: 0x0012E941
		private static void InitializeRenderer(VisualStyleElement element, int state)
		{
			if (TrackBarRenderer.visualStyleRenderer == null)
			{
				TrackBarRenderer.visualStyleRenderer = new VisualStyleRenderer(element.ClassName, element.Part, state);
				return;
			}
			TrackBarRenderer.visualStyleRenderer.SetParameters(element.ClassName, element.Part, state);
		}

		// Token: 0x04002729 RID: 10025
		[ThreadStatic]
		private static VisualStyleRenderer visualStyleRenderer;

		// Token: 0x0400272A RID: 10026
		private const int lineWidth = 2;
	}
}
