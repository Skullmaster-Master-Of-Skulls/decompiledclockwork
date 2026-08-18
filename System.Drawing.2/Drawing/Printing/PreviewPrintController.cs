using System;
using System.Collections;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Internal;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Drawing.Printing
{
	// Token: 0x0200005E RID: 94
	public class PreviewPrintController : PrintController
	{
		// Token: 0x06000763 RID: 1891 RVA: 0x0001E068 File Offset: 0x0001C268
		private void CheckSecurity()
		{
			IntSecurity.SafePrinting.Demand();
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000764 RID: 1892 RVA: 0x0000848C File Offset: 0x0000668C
		public override bool IsPreview
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x0001E074 File Offset: 0x0001C274
		public override void OnStartPrint(PrintDocument document, PrintEventArgs e)
		{
			this.CheckSecurity();
			base.OnStartPrint(document, e);
			try
			{
				if (!document.PrinterSettings.IsValid)
				{
					throw new InvalidPrinterException(document.PrinterSettings);
				}
				IntSecurity.AllPrintingAndUnmanagedCode.Assert();
				this.dc = document.PrinterSettings.CreateInformationContext(this.modeHandle);
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x0001E0E8 File Offset: 0x0001C2E8
		public override Graphics OnStartPage(PrintDocument document, PrintPageEventArgs e)
		{
			this.CheckSecurity();
			base.OnStartPage(document, e);
			try
			{
				IntSecurity.AllPrintingAndUnmanagedCode.Assert();
				if (e.CopySettingsToDevMode)
				{
					e.PageSettings.CopyToHdevmode(this.modeHandle);
				}
				Size size = e.PageBounds.Size;
				Size size2 = PrinterUnitConvert.Convert(size, PrinterUnit.Display, PrinterUnit.HundredthsOfAMillimeter);
				Metafile image = new Metafile(this.dc.Hdc, new Rectangle(0, 0, size2.Width, size2.Height), MetafileFrameUnit.GdiCompatible, EmfType.EmfPlusOnly);
				PreviewPageInfo value = new PreviewPageInfo(image, size);
				this.list.Add(value);
				PrintPreviewGraphics printingHelper = new PrintPreviewGraphics(document, e);
				this.graphics = Graphics.FromImage(image);
				if (this.graphics != null && document.OriginAtMargins)
				{
					int deviceCaps = UnsafeNativeMethods.GetDeviceCaps(new HandleRef(this.dc, this.dc.Hdc), 88);
					int deviceCaps2 = UnsafeNativeMethods.GetDeviceCaps(new HandleRef(this.dc, this.dc.Hdc), 90);
					int deviceCaps3 = UnsafeNativeMethods.GetDeviceCaps(new HandleRef(this.dc, this.dc.Hdc), 112);
					int deviceCaps4 = UnsafeNativeMethods.GetDeviceCaps(new HandleRef(this.dc, this.dc.Hdc), 113);
					float num = (float)(deviceCaps3 * 100 / deviceCaps);
					float num2 = (float)(deviceCaps4 * 100 / deviceCaps2);
					this.graphics.TranslateTransform(-num, -num2);
					this.graphics.TranslateTransform((float)document.DefaultPageSettings.Margins.Left, (float)document.DefaultPageSettings.Margins.Top);
				}
				this.graphics.PrintingHelper = printingHelper;
				if (this.antiAlias)
				{
					this.graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
					this.graphics.SmoothingMode = SmoothingMode.AntiAlias;
				}
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
			return this.graphics;
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x0001E2D4 File Offset: 0x0001C4D4
		public override void OnEndPage(PrintDocument document, PrintPageEventArgs e)
		{
			this.CheckSecurity();
			this.graphics.Dispose();
			this.graphics = null;
			base.OnEndPage(document, e);
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x0001E2F6 File Offset: 0x0001C4F6
		public override void OnEndPrint(PrintDocument document, PrintEventArgs e)
		{
			this.CheckSecurity();
			this.dc.Dispose();
			this.dc = null;
			base.OnEndPrint(document, e);
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x0001E318 File Offset: 0x0001C518
		public PreviewPageInfo[] GetPreviewPageInfo()
		{
			this.CheckSecurity();
			PreviewPageInfo[] array = new PreviewPageInfo[this.list.Count];
			this.list.CopyTo(array, 0);
			return array;
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x0600076A RID: 1898 RVA: 0x0001E34A File Offset: 0x0001C54A
		// (set) Token: 0x0600076B RID: 1899 RVA: 0x0001E352 File Offset: 0x0001C552
		public virtual bool UseAntiAlias
		{
			get
			{
				return this.antiAlias;
			}
			set
			{
				this.antiAlias = value;
			}
		}

		// Token: 0x040006B7 RID: 1719
		private IList list = new ArrayList();

		// Token: 0x040006B8 RID: 1720
		private Graphics graphics;

		// Token: 0x040006B9 RID: 1721
		private DeviceContext dc;

		// Token: 0x040006BA RID: 1722
		private bool antiAlias;
	}
}
