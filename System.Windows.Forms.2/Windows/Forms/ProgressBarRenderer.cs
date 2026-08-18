using System;
using System.Drawing;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms
{
	// Token: 0x0200032A RID: 810
	public sealed class ProgressBarRenderer
	{
		// Token: 0x060033FE RID: 13310 RVA: 0x00002843 File Offset: 0x00000A43
		private ProgressBarRenderer()
		{
		}

		// Token: 0x17000C91 RID: 3217
		// (get) Token: 0x060033FF RID: 13311 RVA: 0x0002BD47 File Offset: 0x00029F47
		public static bool IsSupported
		{
			get
			{
				return VisualStyleRenderer.IsSupported;
			}
		}

		// Token: 0x06003400 RID: 13312 RVA: 0x000EBBFD File Offset: 0x000E9DFD
		public static void DrawHorizontalBar(Graphics g, Rectangle bounds)
		{
			ProgressBarRenderer.InitializeRenderer(VisualStyleElement.ProgressBar.Bar.Normal);
			ProgressBarRenderer.visualStyleRenderer.DrawBackground(g, bounds);
		}

		// Token: 0x06003401 RID: 13313 RVA: 0x000EBC15 File Offset: 0x000E9E15
		public static void DrawVerticalBar(Graphics g, Rectangle bounds)
		{
			ProgressBarRenderer.InitializeRenderer(VisualStyleElement.ProgressBar.BarVertical.Normal);
			ProgressBarRenderer.visualStyleRenderer.DrawBackground(g, bounds);
		}

		// Token: 0x06003402 RID: 13314 RVA: 0x000EBC2D File Offset: 0x000E9E2D
		public static void DrawHorizontalChunks(Graphics g, Rectangle bounds)
		{
			ProgressBarRenderer.InitializeRenderer(VisualStyleElement.ProgressBar.Chunk.Normal);
			ProgressBarRenderer.visualStyleRenderer.DrawBackground(g, bounds);
		}

		// Token: 0x06003403 RID: 13315 RVA: 0x000EBC45 File Offset: 0x000E9E45
		public static void DrawVerticalChunks(Graphics g, Rectangle bounds)
		{
			ProgressBarRenderer.InitializeRenderer(VisualStyleElement.ProgressBar.ChunkVertical.Normal);
			ProgressBarRenderer.visualStyleRenderer.DrawBackground(g, bounds);
		}

		// Token: 0x17000C92 RID: 3218
		// (get) Token: 0x06003404 RID: 13316 RVA: 0x000EBC5D File Offset: 0x000E9E5D
		public static int ChunkThickness
		{
			get
			{
				ProgressBarRenderer.InitializeRenderer(VisualStyleElement.ProgressBar.Chunk.Normal);
				return ProgressBarRenderer.visualStyleRenderer.GetInteger(IntegerProperty.ProgressChunkSize);
			}
		}

		// Token: 0x17000C93 RID: 3219
		// (get) Token: 0x06003405 RID: 13317 RVA: 0x000EBC78 File Offset: 0x000E9E78
		public static int ChunkSpaceThickness
		{
			get
			{
				ProgressBarRenderer.InitializeRenderer(VisualStyleElement.ProgressBar.Chunk.Normal);
				return ProgressBarRenderer.visualStyleRenderer.GetInteger(IntegerProperty.ProgressSpaceSize);
			}
		}

		// Token: 0x06003406 RID: 13318 RVA: 0x000EBC93 File Offset: 0x000E9E93
		private static void InitializeRenderer(VisualStyleElement element)
		{
			if (ProgressBarRenderer.visualStyleRenderer == null)
			{
				ProgressBarRenderer.visualStyleRenderer = new VisualStyleRenderer(element);
				return;
			}
			ProgressBarRenderer.visualStyleRenderer.SetParameters(element);
		}

		// Token: 0x04001EDB RID: 7899
		[ThreadStatic]
		private static VisualStyleRenderer visualStyleRenderer;
	}
}
