using System;
using System.ComponentModel;
using System.Drawing.Printing;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Windows.Forms
{
	// Token: 0x0200044B RID: 1099
	[DefaultProperty("Document")]
	[SRDescription("DescriptionPrintDialog")]
	[Designer("System.Windows.Forms.Design.PrintDialogDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public sealed class PrintDialog : CommonDialog
	{
		// Token: 0x06004C31 RID: 19505 RVA: 0x000AFCFB File Offset: 0x000ADEFB
		public PrintDialog()
		{
			this.Reset();
		}

		// Token: 0x170012A4 RID: 4772
		// (get) Token: 0x06004C32 RID: 19506 RVA: 0x0013C395 File Offset: 0x0013A595
		// (set) Token: 0x06004C33 RID: 19507 RVA: 0x0013C39D File Offset: 0x0013A59D
		[DefaultValue(false)]
		[SRDescription("PDallowCurrentPageDescr")]
		public bool AllowCurrentPage
		{
			get
			{
				return this.allowCurrentPage;
			}
			set
			{
				this.allowCurrentPage = value;
			}
		}

		// Token: 0x170012A5 RID: 4773
		// (get) Token: 0x06004C34 RID: 19508 RVA: 0x0013C3A6 File Offset: 0x0013A5A6
		// (set) Token: 0x06004C35 RID: 19509 RVA: 0x0013C3AE File Offset: 0x0013A5AE
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("PDallowPagesDescr")]
		public bool AllowSomePages
		{
			get
			{
				return this.allowPages;
			}
			set
			{
				this.allowPages = value;
			}
		}

		// Token: 0x170012A6 RID: 4774
		// (get) Token: 0x06004C36 RID: 19510 RVA: 0x0013C3B7 File Offset: 0x0013A5B7
		// (set) Token: 0x06004C37 RID: 19511 RVA: 0x0013C3BF File Offset: 0x0013A5BF
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[SRDescription("PDallowPrintToFileDescr")]
		public bool AllowPrintToFile
		{
			get
			{
				return this.allowPrintToFile;
			}
			set
			{
				this.allowPrintToFile = value;
			}
		}

		// Token: 0x170012A7 RID: 4775
		// (get) Token: 0x06004C38 RID: 19512 RVA: 0x0013C3C8 File Offset: 0x0013A5C8
		// (set) Token: 0x06004C39 RID: 19513 RVA: 0x0013C3D0 File Offset: 0x0013A5D0
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("PDallowSelectionDescr")]
		public bool AllowSelection
		{
			get
			{
				return this.allowSelection;
			}
			set
			{
				this.allowSelection = value;
			}
		}

		// Token: 0x170012A8 RID: 4776
		// (get) Token: 0x06004C3A RID: 19514 RVA: 0x0013C3D9 File Offset: 0x0013A5D9
		// (set) Token: 0x06004C3B RID: 19515 RVA: 0x0013C3E1 File Offset: 0x0013A5E1
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
				if (this.printDocument == null)
				{
					this.settings = new PrinterSettings();
					return;
				}
				this.settings = this.printDocument.PrinterSettings;
			}
		}

		// Token: 0x170012A9 RID: 4777
		// (get) Token: 0x06004C3C RID: 19516 RVA: 0x0013C40F File Offset: 0x0013A60F
		private PageSettings PageSettings
		{
			get
			{
				if (this.Document == null)
				{
					return this.PrinterSettings.DefaultPageSettings;
				}
				return this.Document.DefaultPageSettings;
			}
		}

		// Token: 0x170012AA RID: 4778
		// (get) Token: 0x06004C3D RID: 19517 RVA: 0x0013C430 File Offset: 0x0013A630
		// (set) Token: 0x06004C3E RID: 19518 RVA: 0x0013C44B File Offset: 0x0013A64B
		[SRCategory("CatData")]
		[DefaultValue(null)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("PDprinterSettingsDescr")]
		public PrinterSettings PrinterSettings
		{
			get
			{
				if (this.settings == null)
				{
					this.settings = new PrinterSettings();
				}
				return this.settings;
			}
			set
			{
				if (value != this.PrinterSettings)
				{
					this.settings = value;
					this.printDocument = null;
				}
			}
		}

		// Token: 0x170012AB RID: 4779
		// (get) Token: 0x06004C3F RID: 19519 RVA: 0x0013C464 File Offset: 0x0013A664
		// (set) Token: 0x06004C40 RID: 19520 RVA: 0x0013C46C File Offset: 0x0013A66C
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("PDprintToFileDescr")]
		public bool PrintToFile
		{
			get
			{
				return this.printToFile;
			}
			set
			{
				this.printToFile = value;
			}
		}

		// Token: 0x170012AC RID: 4780
		// (get) Token: 0x06004C41 RID: 19521 RVA: 0x0013C475 File Offset: 0x0013A675
		// (set) Token: 0x06004C42 RID: 19522 RVA: 0x0013C47D File Offset: 0x0013A67D
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("PDshowHelpDescr")]
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

		// Token: 0x170012AD RID: 4781
		// (get) Token: 0x06004C43 RID: 19523 RVA: 0x0013C486 File Offset: 0x0013A686
		// (set) Token: 0x06004C44 RID: 19524 RVA: 0x0013C48E File Offset: 0x0013A68E
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[SRDescription("PDshowNetworkDescr")]
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

		// Token: 0x170012AE RID: 4782
		// (get) Token: 0x06004C45 RID: 19525 RVA: 0x0013C497 File Offset: 0x0013A697
		// (set) Token: 0x06004C46 RID: 19526 RVA: 0x0013C49F File Offset: 0x0013A69F
		[DefaultValue(false)]
		[SRDescription("PDuseEXDialog")]
		public bool UseEXDialog
		{
			get
			{
				return this.useEXDialog;
			}
			set
			{
				this.useEXDialog = value;
			}
		}

		// Token: 0x06004C47 RID: 19527 RVA: 0x0013C4A8 File Offset: 0x0013A6A8
		private int GetFlags()
		{
			int num = 0;
			if (!this.UseEXDialog || Environment.OSVersion.Platform != PlatformID.Win32NT || Environment.OSVersion.Version.Major < 5)
			{
				num |= 4096;
			}
			if (!this.allowCurrentPage)
			{
				num |= 8388608;
			}
			if (!this.allowPages)
			{
				num |= 8;
			}
			if (!this.allowPrintToFile)
			{
				num |= 524288;
			}
			if (!this.allowSelection)
			{
				num |= 4;
			}
			num |= (int)this.PrinterSettings.PrintRange;
			if (this.printToFile)
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
			if (this.PrinterSettings.Collate)
			{
				num |= 16;
			}
			return num;
		}

		// Token: 0x06004C48 RID: 19528 RVA: 0x0013C56C File Offset: 0x0013A76C
		public override void Reset()
		{
			this.allowCurrentPage = false;
			this.allowPages = false;
			this.allowPrintToFile = true;
			this.allowSelection = false;
			this.printDocument = null;
			this.printToFile = false;
			this.settings = null;
			this.showHelp = false;
			this.showNetwork = true;
		}

		// Token: 0x06004C49 RID: 19529 RVA: 0x0013C5B8 File Offset: 0x0013A7B8
		internal static NativeMethods.PRINTDLG CreatePRINTDLG()
		{
			NativeMethods.PRINTDLG printdlg;
			if (IntPtr.Size == 4)
			{
				printdlg = new NativeMethods.PRINTDLG_32();
			}
			else
			{
				printdlg = new NativeMethods.PRINTDLG_64();
			}
			printdlg.lStructSize = Marshal.SizeOf(printdlg);
			printdlg.hwndOwner = IntPtr.Zero;
			printdlg.hDevMode = IntPtr.Zero;
			printdlg.hDevNames = IntPtr.Zero;
			printdlg.Flags = 0;
			printdlg.hDC = IntPtr.Zero;
			printdlg.nFromPage = 1;
			printdlg.nToPage = 1;
			printdlg.nMinPage = 0;
			printdlg.nMaxPage = 9999;
			printdlg.nCopies = 1;
			printdlg.hInstance = IntPtr.Zero;
			printdlg.lCustData = IntPtr.Zero;
			printdlg.lpfnPrintHook = null;
			printdlg.lpfnSetupHook = null;
			printdlg.lpPrintTemplateName = null;
			printdlg.lpSetupTemplateName = null;
			printdlg.hPrintTemplate = IntPtr.Zero;
			printdlg.hSetupTemplate = IntPtr.Zero;
			return printdlg;
		}

		// Token: 0x06004C4A RID: 19530 RVA: 0x0013C68C File Offset: 0x0013A88C
		internal static NativeMethods.PRINTDLGEX CreatePRINTDLGEX()
		{
			NativeMethods.PRINTDLGEX printdlgex = new NativeMethods.PRINTDLGEX();
			printdlgex.lStructSize = Marshal.SizeOf(printdlgex);
			printdlgex.hwndOwner = IntPtr.Zero;
			printdlgex.hDevMode = IntPtr.Zero;
			printdlgex.hDevNames = IntPtr.Zero;
			printdlgex.hDC = IntPtr.Zero;
			printdlgex.Flags = 0;
			printdlgex.Flags2 = 0;
			printdlgex.ExclusionFlags = 0;
			printdlgex.nPageRanges = 0;
			printdlgex.nMaxPageRanges = 1;
			printdlgex.pageRanges = UnsafeNativeMethods.GlobalAlloc(64, printdlgex.nMaxPageRanges * Marshal.SizeOf(typeof(NativeMethods.PRINTPAGERANGE)));
			printdlgex.nMinPage = 0;
			printdlgex.nMaxPage = 9999;
			printdlgex.nCopies = 1;
			printdlgex.hInstance = IntPtr.Zero;
			printdlgex.lpPrintTemplateName = null;
			printdlgex.nPropertyPages = 0;
			printdlgex.lphPropertyPages = IntPtr.Zero;
			printdlgex.nStartPage = NativeMethods.START_PAGE_GENERAL;
			printdlgex.dwResultAction = 0;
			return printdlgex;
		}

		// Token: 0x06004C4B RID: 19531 RVA: 0x0013C770 File Offset: 0x0013A970
		protected override bool RunDialog(IntPtr hwndOwner)
		{
			IntSecurity.SafePrinting.Demand();
			NativeMethods.WndProc hookProcPtr = new NativeMethods.WndProc(this.HookProc);
			bool result;
			if (!this.UseEXDialog || Environment.OSVersion.Platform != PlatformID.Win32NT || Environment.OSVersion.Version.Major < 5)
			{
				NativeMethods.PRINTDLG data = PrintDialog.CreatePRINTDLG();
				result = this.ShowPrintDialog(hwndOwner, hookProcPtr, data);
			}
			else
			{
				NativeMethods.PRINTDLGEX data2 = PrintDialog.CreatePRINTDLGEX();
				result = this.ShowPrintDialog(hwndOwner, data2);
			}
			return result;
		}

		// Token: 0x06004C4C RID: 19532 RVA: 0x0013C7E0 File Offset: 0x0013A9E0
		private bool ShowPrintDialog(IntPtr hwndOwner, NativeMethods.WndProc hookProcPtr, NativeMethods.PRINTDLG data)
		{
			data.Flags = this.GetFlags();
			data.nCopies = this.PrinterSettings.Copies;
			data.hwndOwner = hwndOwner;
			data.lpfnPrintHook = hookProcPtr;
			IntSecurity.AllPrintingAndUnmanagedCode.Assert();
			try
			{
				if (this.PageSettings == null)
				{
					data.hDevMode = this.PrinterSettings.GetHdevmode();
				}
				else
				{
					data.hDevMode = this.PrinterSettings.GetHdevmode(this.PageSettings);
				}
				data.hDevNames = this.PrinterSettings.GetHdevnames();
			}
			catch (InvalidPrinterException)
			{
				data.hDevMode = IntPtr.Zero;
				data.hDevNames = IntPtr.Zero;
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
			bool result;
			try
			{
				if (this.AllowSomePages)
				{
					if (this.PrinterSettings.FromPage < this.PrinterSettings.MinimumPage || this.PrinterSettings.FromPage > this.PrinterSettings.MaximumPage)
					{
						throw new ArgumentException(SR.GetString("PDpageOutOfRange", new object[]
						{
							"FromPage"
						}));
					}
					if (this.PrinterSettings.ToPage < this.PrinterSettings.MinimumPage || this.PrinterSettings.ToPage > this.PrinterSettings.MaximumPage)
					{
						throw new ArgumentException(SR.GetString("PDpageOutOfRange", new object[]
						{
							"ToPage"
						}));
					}
					if (this.PrinterSettings.ToPage < this.PrinterSettings.FromPage)
					{
						throw new ArgumentException(SR.GetString("PDpageOutOfRange", new object[]
						{
							"FromPage"
						}));
					}
					data.nFromPage = (short)this.PrinterSettings.FromPage;
					data.nToPage = (short)this.PrinterSettings.ToPage;
					data.nMinPage = (short)this.PrinterSettings.MinimumPage;
					data.nMaxPage = (short)this.PrinterSettings.MaximumPage;
				}
				if (!UnsafeNativeMethods.PrintDlg(data))
				{
					result = false;
				}
				else
				{
					IntSecurity.AllPrintingAndUnmanagedCode.Assert();
					try
					{
						PrintDialog.UpdatePrinterSettings(data.hDevMode, data.hDevNames, data.nCopies, data.Flags, this.settings, this.PageSettings);
					}
					finally
					{
						CodeAccessPermission.RevertAssert();
					}
					this.PrintToFile = ((data.Flags & 32) != 0);
					this.PrinterSettings.PrintToFile = this.PrintToFile;
					if (this.AllowSomePages)
					{
						this.PrinterSettings.FromPage = (int)data.nFromPage;
						this.PrinterSettings.ToPage = (int)data.nToPage;
					}
					if ((data.Flags & 262144) == 0 && Environment.OSVersion.Version.Major >= 6)
					{
						this.PrinterSettings.Copies = data.nCopies;
						this.PrinterSettings.Collate = ((data.Flags & 16) == 16);
					}
					result = true;
				}
			}
			finally
			{
				UnsafeNativeMethods.GlobalFree(new HandleRef(data, data.hDevMode));
				UnsafeNativeMethods.GlobalFree(new HandleRef(data, data.hDevNames));
			}
			return result;
		}

		// Token: 0x06004C4D RID: 19533 RVA: 0x0013CB1C File Offset: 0x0013AD1C
		private unsafe bool ShowPrintDialog(IntPtr hwndOwner, NativeMethods.PRINTDLGEX data)
		{
			data.Flags = this.GetFlags();
			data.nCopies = (int)this.PrinterSettings.Copies;
			data.hwndOwner = hwndOwner;
			IntSecurity.AllPrintingAndUnmanagedCode.Assert();
			try
			{
				if (this.PageSettings == null)
				{
					data.hDevMode = this.PrinterSettings.GetHdevmode();
				}
				else
				{
					data.hDevMode = this.PrinterSettings.GetHdevmode(this.PageSettings);
				}
				data.hDevNames = this.PrinterSettings.GetHdevnames();
			}
			catch (InvalidPrinterException)
			{
				data.hDevMode = IntPtr.Zero;
				data.hDevNames = IntPtr.Zero;
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
			bool result;
			try
			{
				if (this.AllowSomePages)
				{
					if (this.PrinterSettings.FromPage < this.PrinterSettings.MinimumPage || this.PrinterSettings.FromPage > this.PrinterSettings.MaximumPage)
					{
						throw new ArgumentException(SR.GetString("PDpageOutOfRange", new object[]
						{
							"FromPage"
						}));
					}
					if (this.PrinterSettings.ToPage < this.PrinterSettings.MinimumPage || this.PrinterSettings.ToPage > this.PrinterSettings.MaximumPage)
					{
						throw new ArgumentException(SR.GetString("PDpageOutOfRange", new object[]
						{
							"ToPage"
						}));
					}
					if (this.PrinterSettings.ToPage < this.PrinterSettings.FromPage)
					{
						throw new ArgumentException(SR.GetString("PDpageOutOfRange", new object[]
						{
							"FromPage"
						}));
					}
					int* ptr = (int*)((void*)data.pageRanges);
					*ptr = this.PrinterSettings.FromPage;
					ptr++;
					*ptr = this.PrinterSettings.ToPage;
					data.nPageRanges = 1;
					data.nMinPage = this.PrinterSettings.MinimumPage;
					data.nMaxPage = this.PrinterSettings.MaximumPage;
				}
				data.Flags &= -2099201;
				int hr = UnsafeNativeMethods.PrintDlgEx(data);
				if (NativeMethods.Failed(hr) || data.dwResultAction == 0)
				{
					result = false;
				}
				else
				{
					IntSecurity.AllPrintingAndUnmanagedCode.Assert();
					try
					{
						PrintDialog.UpdatePrinterSettings(data.hDevMode, data.hDevNames, (short)data.nCopies, data.Flags, this.PrinterSettings, this.PageSettings);
					}
					finally
					{
						CodeAccessPermission.RevertAssert();
					}
					this.PrintToFile = ((data.Flags & 32) != 0);
					this.PrinterSettings.PrintToFile = this.PrintToFile;
					if (this.AllowSomePages)
					{
						int* ptr2 = (int*)((void*)data.pageRanges);
						this.PrinterSettings.FromPage = *ptr2;
						ptr2++;
						this.PrinterSettings.ToPage = *ptr2;
					}
					if ((data.Flags & 262144) == 0 && Environment.OSVersion.Version.Major >= 6)
					{
						this.PrinterSettings.Copies = (short)data.nCopies;
						this.PrinterSettings.Collate = ((data.Flags & 16) == 16);
					}
					result = (data.dwResultAction == 1);
				}
			}
			finally
			{
				if (data.hDevMode != IntPtr.Zero)
				{
					UnsafeNativeMethods.GlobalFree(new HandleRef(data, data.hDevMode));
				}
				if (data.hDevNames != IntPtr.Zero)
				{
					UnsafeNativeMethods.GlobalFree(new HandleRef(data, data.hDevNames));
				}
				if (data.pageRanges != IntPtr.Zero)
				{
					UnsafeNativeMethods.GlobalFree(new HandleRef(data, data.pageRanges));
				}
			}
			return result;
		}

		// Token: 0x06004C4E RID: 19534 RVA: 0x0013CED8 File Offset: 0x0013B0D8
		private static void UpdatePrinterSettings(IntPtr hDevMode, IntPtr hDevNames, short copies, int flags, PrinterSettings settings, PageSettings pageSettings)
		{
			settings.SetHdevmode(hDevMode);
			settings.SetHdevnames(hDevNames);
			if (pageSettings != null)
			{
				pageSettings.SetHdevmode(hDevMode);
			}
			if (settings.Copies == 1)
			{
				settings.Copies = copies;
			}
			settings.PrintRange = (PrintRange)(flags & 4194307);
		}

		// Token: 0x04002882 RID: 10370
		private const int printRangeMask = 4194307;

		// Token: 0x04002883 RID: 10371
		private PrinterSettings settings;

		// Token: 0x04002884 RID: 10372
		private PrintDocument printDocument;

		// Token: 0x04002885 RID: 10373
		private bool allowCurrentPage;

		// Token: 0x04002886 RID: 10374
		private bool allowPages;

		// Token: 0x04002887 RID: 10375
		private bool allowPrintToFile;

		// Token: 0x04002888 RID: 10376
		private bool allowSelection;

		// Token: 0x04002889 RID: 10377
		private bool printToFile;

		// Token: 0x0400288A RID: 10378
		private bool showHelp;

		// Token: 0x0400288B RID: 10379
		private bool showNetwork;

		// Token: 0x0400288C RID: 10380
		private bool useEXDialog;
	}
}
