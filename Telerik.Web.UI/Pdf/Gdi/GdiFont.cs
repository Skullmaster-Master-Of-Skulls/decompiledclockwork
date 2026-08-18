using System;

namespace Telerik.Pdf.Gdi
{
	// Token: 0x0200162B RID: 5675
	public class GdiFont
	{
		// Token: 0x0600DCA5 RID: 56485 RVA: 0x0030372F File Offset: 0x0030192F
		public GdiFont(IntPtr hFont, string faceName, int height)
		{
			this.hFont = hFont;
			this.faceName = faceName;
			this.height = height;
		}

		// Token: 0x0600DCA6 RID: 56486 RVA: 0x0030374C File Offset: 0x0030194C
		~GdiFont()
		{
			this.Dispose(false);
		}

		// Token: 0x0600DCA7 RID: 56487 RVA: 0x0030377C File Offset: 0x0030197C
		public virtual void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600DCA8 RID: 56488 RVA: 0x0030378B File Offset: 0x0030198B
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this.hFont != IntPtr.Zero)
			{
				NativeMethods.DeleteObject(this.hFont);
				this.hFont = IntPtr.Zero;
			}
		}

		// Token: 0x0600DCA9 RID: 56489 RVA: 0x003037BC File Offset: 0x003019BC
		public static GdiFont CreateFont(string faceName, int height, bool bold, bool italic)
		{
			return new GdiFont(NativeMethods.CreateFontIndirect(new LogFont
			{
				lfCharSet = 1,
				lfFaceName = faceName,
				lfHeight = height,
				lfWeight = (bold ? 700 : 0),
				lfItalic = Convert.ToByte(italic)
			}), faceName, height);
		}

		// Token: 0x0600DCAA RID: 56490 RVA: 0x00303810 File Offset: 0x00301A10
		public static GdiFont CreateDesignFont(string faceName, bool bold, bool italic, GdiDeviceContent dc)
		{
			GdiFont gdiFont = GdiFont.CreateFont(faceName, 2048, bold, italic);
			dc.SelectFont(gdiFont);
			GdiFontMetrics metrics = gdiFont.GetMetrics(dc);
			gdiFont.Dispose();
			return GdiFont.CreateFont(faceName, -Math.Abs(metrics.EmSquare), bold, italic);
		}

		// Token: 0x0600DCAB RID: 56491 RVA: 0x00303855 File Offset: 0x00301A55
		public GdiFontMetrics GetMetrics(GdiDeviceContent dc)
		{
			return new GdiFontMetrics(dc, this);
		}

		// Token: 0x1700438F RID: 17295
		// (get) Token: 0x0600DCAC RID: 56492 RVA: 0x0030385E File Offset: 0x00301A5E
		public string FaceName
		{
			get
			{
				return this.faceName;
			}
		}

		// Token: 0x17004390 RID: 17296
		// (get) Token: 0x0600DCAD RID: 56493 RVA: 0x00303866 File Offset: 0x00301A66
		public int Height
		{
			get
			{
				return this.height;
			}
		}

		// Token: 0x17004391 RID: 17297
		// (get) Token: 0x0600DCAE RID: 56494 RVA: 0x0030386E File Offset: 0x00301A6E
		public IntPtr Handle
		{
			get
			{
				return this.hFont;
			}
		}

		// Token: 0x04003E40 RID: 15936
		private IntPtr hFont;

		// Token: 0x04003E41 RID: 15937
		private string faceName;

		// Token: 0x04003E42 RID: 15938
		private int height;
	}
}
