using System;
using System.Drawing.Printing;

namespace ImportExportClassLibrary
{
	// Token: 0x02000037 RID: 55
	public class TemplatesClassPrinterSettings
	{
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060001FB RID: 507 RVA: 0x00015CB3 File Offset: 0x00014CB3
		// (set) Token: 0x060001FC RID: 508 RVA: 0x00015CBB File Offset: 0x00014CBB
		public PrinterSettings PrinterSettings
		{
			get
			{
				return this.printerSettings;
			}
			set
			{
				this.printerSettings = value;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060001FD RID: 509 RVA: 0x00015CC4 File Offset: 0x00014CC4
		// (set) Token: 0x060001FE RID: 510 RVA: 0x00015CCC File Offset: 0x00014CCC
		public bool PrintPreview
		{
			get
			{
				return this.printPreview;
			}
			set
			{
				this.printPreview = value;
			}
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00015CD5 File Offset: 0x00014CD5
		public TemplatesClassPrinterSettings(PrinterSettings printerSettings)
		{
			this.printerSettings = printerSettings;
			this.printPreview = false;
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00015CEB File Offset: 0x00014CEB
		public TemplatesClassPrinterSettings()
		{
			this.printerSettings = null;
			this.printPreview = false;
		}

		// Token: 0x04000105 RID: 261
		private PrinterSettings printerSettings;

		// Token: 0x04000106 RID: 262
		private bool printPreview;
	}
}
