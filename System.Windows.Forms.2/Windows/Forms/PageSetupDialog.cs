using System;
using System.ComponentModel;
using System.Drawing.Printing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace System.Windows.Forms
{
	// Token: 0x02000449 RID: 1097
	[DefaultProperty("Document")]
	[SRDescription("DescriptionPageSetupDialog")]
	public sealed class PageSetupDialog : CommonDialog
	{
		// Token: 0x06004C0D RID: 19469 RVA: 0x000AFCFB File Offset: 0x000ADEFB
		public PageSetupDialog()
		{
			this.Reset();
		}

		// Token: 0x17001298 RID: 4760
		// (get) Token: 0x06004C0E RID: 19470 RVA: 0x0013BC70 File Offset: 0x00139E70
		// (set) Token: 0x06004C0F RID: 19471 RVA: 0x0013BC78 File Offset: 0x00139E78
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[SRDescription("PSDallowMarginsDescr")]
		public bool AllowMargins
		{
			get
			{
				return this.allowMargins;
			}
			set
			{
				this.allowMargins = value;
			}
		}

		// Token: 0x17001299 RID: 4761
		// (get) Token: 0x06004C10 RID: 19472 RVA: 0x0013BC81 File Offset: 0x00139E81
		// (set) Token: 0x06004C11 RID: 19473 RVA: 0x0013BC89 File Offset: 0x00139E89
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[SRDescription("PSDallowOrientationDescr")]
		public bool AllowOrientation
		{
			get
			{
				return this.allowOrientation;
			}
			set
			{
				this.allowOrientation = value;
			}
		}

		// Token: 0x1700129A RID: 4762
		// (get) Token: 0x06004C12 RID: 19474 RVA: 0x0013BC92 File Offset: 0x00139E92
		// (set) Token: 0x06004C13 RID: 19475 RVA: 0x0013BC9A File Offset: 0x00139E9A
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[SRDescription("PSDallowPaperDescr")]
		public bool AllowPaper
		{
			get
			{
				return this.allowPaper;
			}
			set
			{
				this.allowPaper = value;
			}
		}

		// Token: 0x1700129B RID: 4763
		// (get) Token: 0x06004C14 RID: 19476 RVA: 0x0013BCA3 File Offset: 0x00139EA3
		// (set) Token: 0x06004C15 RID: 19477 RVA: 0x0013BCAB File Offset: 0x00139EAB
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[SRDescription("PSDallowPrinterDescr")]
		public bool AllowPrinter
		{
			get
			{
				return this.allowPrinter;
			}
			set
			{
				this.allowPrinter = value;
			}
		}

		// Token: 0x1700129C RID: 4764
		// (get) Token: 0x06004C16 RID: 19478 RVA: 0x0013BCB4 File Offset: 0x00139EB4
		// (set) Token: 0x06004C17 RID: 19479 RVA: 0x0013BCBC File Offset: 0x00139EBC
		[SRCategory("CatData")]
		[DefaultValue(null)]
		[SRDescription("PDdocumentDescr")]
		public PrintDocument Document
		{
			get
			{
				return this.printDocument;
			}
			set
			{
				this.printDocument = value;
				if (this.printDocument != null)
				{
					this.pageSettings = this.printDocument.DefaultPageSettings;
					this.printerSettings = this.printDocument.PrinterSettings;
				}
			}
		}

		// Token: 0x1700129D RID: 4765
		// (get) Token: 0x06004C18 RID: 19480 RVA: 0x0013BCEF File Offset: 0x00139EEF
		// (set) Token: 0x06004C19 RID: 19481 RVA: 0x0013BCF7 File Offset: 0x00139EF7
		[DefaultValue(false)]
		[SRDescription("PSDenableMetricDescr")]
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public bool EnableMetric
		{
			get
			{
				return this.enableMetric;
			}
			set
			{
				this.enableMetric = value;
			}
		}

		// Token: 0x1700129E RID: 4766
		// (get) Token: 0x06004C1A RID: 19482 RVA: 0x0013BD00 File Offset: 0x00139F00
		// (set) Token: 0x06004C1B RID: 19483 RVA: 0x0013BD08 File Offset: 0x00139F08
		[SRCategory("CatData")]
		[SRDescription("PSDminMarginsDescr")]
		public Margins MinMargins
		{
			get
			{
				return this.minMargins;
			}
			set
			{
				if (value == null)
				{
					value = new Margins(0, 0, 0, 0);
				}
				this.minMargins = value;
			}
		}

		// Token: 0x1700129F RID: 4767
		// (get) Token: 0x06004C1C RID: 19484 RVA: 0x0013BD25 File Offset: 0x00139F25
		// (set) Token: 0x06004C1D RID: 19485 RVA: 0x0013BD2D File Offset: 0x00139F2D
		[SRCategory("CatData")]
		[DefaultValue(null)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("PSDpageSettingsDescr")]
		public PageSettings PageSettings
		{
			get
			{
				return this.pageSettings;
			}
			set
			{
				this.pageSettings = value;
				this.printDocument = null;
			}
		}

		// Token: 0x170012A0 RID: 4768
		// (get) Token: 0x06004C1E RID: 19486 RVA: 0x0013BD3D File Offset: 0x00139F3D
		// (set) Token: 0x06004C1F RID: 19487 RVA: 0x0013BD45 File Offset: 0x00139F45
		[SRCategory("CatData")]
		[DefaultValue(null)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("PSDprinterSettingsDescr")]
		public PrinterSettings PrinterSettings
		{
			get
			{
				return this.printerSettings;
			}
			set
			{
				this.printerSettings = value;
				this.printDocument = null;
			}
		}

		// Token: 0x170012A1 RID: 4769
		// (get) Token: 0x06004C20 RID: 19488 RVA: 0x0013BD55 File Offset: 0x00139F55
		// (set) Token: 0x06004C21 RID: 19489 RVA: 0x0013BD5D File Offset: 0x00139F5D
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("PSDshowHelpDescr")]
		public bool ShowHelp
		{
			get
			{
				return this.showHelp;
			}
			set
			{
				this.showHelp = value;
			}
		}

		// Token: 0x170012A2 RID: 4770
		// (get) Token: 0x06004C22 RID: 19490 RVA: 0x0013BD66 File Offset: 0x00139F66
		// (set) Token: 0x06004C23 RID: 19491 RVA: 0x0013BD6E File Offset: 0x00139F6E
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[SRDescription("PSDshowNetworkDescr")]
		public bool ShowNetwork
		{
			get
			{
				return this.showNetwork;
			}
			set
			{
				this.showNetwork = value;
			}
		}

		// Token: 0x06004C24 RID: 19492 RVA: 0x0013BD78 File Offset: 0x00139F78
		private int GetFlags()
		{
			int num = 0;
			num |= 8192;
			if (!this.allowMargins)
			{
				num |= 16;
			}
			if (!this.allowOrientation)
			{
				num |= 256;
			}
			if (!this.allowPaper)
			{
				num |= 512;
			}
			if (!this.allowPrinter || this.printerSettings == null)
			{
				num |= 32;
			}
			if (this.showHelp)
			{
				num |= 2048;
			}
			if (!this.showNetwork)
			{
				num |= 2097152;
			}
			if (this.minMargins != null)
			{
				num |= 1;
			}
			if (this.pageSettings.Margins != null)
			{
				num |= 2;
			}
			return num;
		}

		// Token: 0x06004C25 RID: 19493 RVA: 0x0013BE1C File Offset: 0x0013A01C
		public override void Reset()
		{
			this.allowMargins = true;
			this.allowOrientation = true;
			this.allowPaper = true;
			this.allowPrinter = true;
			this.MinMargins = null;
			this.pageSettings = null;
			this.printDocument = null;
			this.printerSettings = null;
			this.showHelp = false;
			this.showNetwork = true;
		}

		// Token: 0x06004C26 RID: 19494 RVA: 0x0013BE6F File Offset: 0x0013A06F
		private void ResetMinMargins()
		{
			this.MinMargins = null;
		}

		// Token: 0x06004C27 RID: 19495 RVA: 0x0013BE78 File Offset: 0x0013A078
		private bool ShouldSerializeMinMargins()
		{
			return this.minMargins.Left != 0 || this.minMargins.Right != 0 || this.minMargins.Top != 0 || this.minMargins.Bottom != 0;
		}

		// Token: 0x06004C28 RID: 19496 RVA: 0x0013BEB4 File Offset: 0x0013A0B4
		private static void UpdateSettings(NativeMethods.PAGESETUPDLG data, PageSettings pageSettings, PrinterSettings printerSettings)
		{
			IntSecurity.AllPrintingAndUnmanagedCode.Assert();
			try
			{
				pageSettings.SetHdevmode(data.hDevMode);
				if (printerSettings != null)
				{
					printerSettings.SetHdevmode(data.hDevMode);
					printerSettings.SetHdevnames(data.hDevNames);
				}
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
			Margins margins = new Margins();
			margins.Left = data.marginLeft;
			margins.Top = data.marginTop;
			margins.Right = data.marginRight;
			margins.Bottom = data.marginBottom;
			PrinterUnit fromUnit = ((data.Flags & 8) != 0) ? PrinterUnit.HundredthsOfAMillimeter : PrinterUnit.ThousandthsOfAnInch;
			pageSettings.Margins = PrinterUnitConvert.Convert(margins, fromUnit, PrinterUnit.Display);
		}

		// Token: 0x06004C29 RID: 19497 RVA: 0x0013BF60 File Offset: 0x0013A160
		protected override bool RunDialog(IntPtr hwndOwner)
		{
			IntSecurity.SafePrinting.Demand();
			NativeMethods.WndProc lpfnPageSetupHook = new NativeMethods.WndProc(this.HookProc);
			if (this.pageSettings == null)
			{
				throw new ArgumentException(SR.GetString("PSDcantShowWithoutPage"));
			}
			NativeMethods.PAGESETUPDLG pagesetupdlg = new NativeMethods.PAGESETUPDLG();
			pagesetupdlg.lStructSize = Marshal.SizeOf(pagesetupdlg);
			pagesetupdlg.Flags = this.GetFlags();
			pagesetupdlg.hwndOwner = hwndOwner;
			pagesetupdlg.lpfnPageSetupHook = lpfnPageSetupHook;
			PrinterUnit toUnit = PrinterUnit.ThousandthsOfAnInch;
			if (this.EnableMetric)
			{
				StringBuilder stringBuilder = new StringBuilder(2);
				int localeInfo = UnsafeNativeMethods.GetLocaleInfo(NativeMethods.LOCALE_USER_DEFAULT, 13, stringBuilder, stringBuilder.Capacity);
				if (localeInfo > 0 && int.Parse(stringBuilder.ToString(), CultureInfo.InvariantCulture) == 0)
				{
					toUnit = PrinterUnit.HundredthsOfAMillimeter;
				}
			}
			if (this.MinMargins != null)
			{
				Margins margins = PrinterUnitConvert.Convert(this.MinMargins, PrinterUnit.Display, toUnit);
				pagesetupdlg.minMarginLeft = margins.Left;
				pagesetupdlg.minMarginTop = margins.Top;
				pagesetupdlg.minMarginRight = margins.Right;
				pagesetupdlg.minMarginBottom = margins.Bottom;
			}
			if (this.pageSettings.Margins != null)
			{
				Margins margins2 = PrinterUnitConvert.Convert(this.pageSettings.Margins, PrinterUnit.Display, toUnit);
				pagesetupdlg.marginLeft = margins2.Left;
				pagesetupdlg.marginTop = margins2.Top;
				pagesetupdlg.marginRight = margins2.Right;
				pagesetupdlg.marginBottom = margins2.Bottom;
			}
			pagesetupdlg.marginLeft = Math.Max(pagesetupdlg.marginLeft, pagesetupdlg.minMarginLeft);
			pagesetupdlg.marginTop = Math.Max(pagesetupdlg.marginTop, pagesetupdlg.minMarginTop);
			pagesetupdlg.marginRight = Math.Max(pagesetupdlg.marginRight, pagesetupdlg.minMarginRight);
			pagesetupdlg.marginBottom = Math.Max(pagesetupdlg.marginBottom, pagesetupdlg.minMarginBottom);
			PrinterSettings printerSettings = (this.printerSettings == null) ? this.pageSettings.PrinterSettings : this.printerSettings;
			IntSecurity.AllPrintingAndUnmanagedCode.Assert();
			try
			{
				pagesetupdlg.hDevMode = printerSettings.GetHdevmode(this.pageSettings);
				pagesetupdlg.hDevNames = printerSettings.GetHdevnames();
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
			bool result;
			try
			{
				if (!UnsafeNativeMethods.PageSetupDlg(pagesetupdlg))
				{
					result = false;
				}
				else
				{
					PageSetupDialog.UpdateSettings(pagesetupdlg, this.pageSettings, this.printerSettings);
					result = true;
				}
			}
			finally
			{
				UnsafeNativeMethods.GlobalFree(new HandleRef(pagesetupdlg, pagesetupdlg.hDevMode));
				UnsafeNativeMethods.GlobalFree(new HandleRef(pagesetupdlg, pagesetupdlg.hDevNames));
			}
			return result;
		}

		// Token: 0x04002872 RID: 10354
		private PrintDocument printDocument;

		// Token: 0x04002873 RID: 10355
		private PageSettings pageSettings;

		// Token: 0x04002874 RID: 10356
		private PrinterSettings printerSettings;

		// Token: 0x04002875 RID: 10357
		private bool allowMargins;

		// Token: 0x04002876 RID: 10358
		private bool allowOrientation;

		// Token: 0x04002877 RID: 10359
		private bool allowPaper;

		// Token: 0x04002878 RID: 10360
		private bool allowPrinter;

		// Token: 0x04002879 RID: 10361
		private Margins minMargins;

		// Token: 0x0400287A RID: 10362
		private bool showHelp;

		// Token: 0x0400287B RID: 10363
		private bool showNetwork;

		// Token: 0x0400287C RID: 10364
		private bool enableMetric;
	}
}
