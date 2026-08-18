using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;

namespace System.Windows.Forms
{
	// Token: 0x0200025B RID: 603
	[DefaultEvent("HelpRequest")]
	[DefaultProperty("SelectedPath")]
	[Designer("System.Windows.Forms.Design.FolderBrowserDialogDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[SRDescription("DescriptionFolderBrowserDialog")]
	public sealed class FolderBrowserDialog : CommonDialog
	{
		// Token: 0x060025CE RID: 9678 RVA: 0x000AFCFB File Offset: 0x000ADEFB
		public FolderBrowserDialog()
		{
			this.Reset();
		}

		// Token: 0x1400019B RID: 411
		// (add) Token: 0x060025CF RID: 9679 RVA: 0x000AFD09 File Offset: 0x000ADF09
		// (remove) Token: 0x060025D0 RID: 9680 RVA: 0x000AFD12 File Offset: 0x000ADF12
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler HelpRequest
		{
			add
			{
				base.HelpRequest += value;
			}
			remove
			{
				base.HelpRequest -= value;
			}
		}

		// Token: 0x170008BA RID: 2234
		// (get) Token: 0x060025D1 RID: 9681 RVA: 0x000AFD1B File Offset: 0x000ADF1B
		// (set) Token: 0x060025D2 RID: 9682 RVA: 0x000AFD23 File Offset: 0x000ADF23
		[Browsable(true)]
		[DefaultValue(true)]
		[Localizable(false)]
		[SRCategory("CatFolderBrowsing")]
		[SRDescription("FolderBrowserDialogShowNewFolderButton")]
		public bool ShowNewFolderButton
		{
			get
			{
				return this.showNewFolderButton;
			}
			set
			{
				this.showNewFolderButton = value;
			}
		}

		// Token: 0x170008BB RID: 2235
		// (get) Token: 0x060025D3 RID: 9683 RVA: 0x000AFD2C File Offset: 0x000ADF2C
		// (set) Token: 0x060025D4 RID: 9684 RVA: 0x000AFD69 File Offset: 0x000ADF69
		[Browsable(true)]
		[DefaultValue("")]
		[Editor("System.Windows.Forms.Design.SelectedPathEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[Localizable(true)]
		[SRCategory("CatFolderBrowsing")]
		[SRDescription("FolderBrowserDialogSelectedPath")]
		public string SelectedPath
		{
			get
			{
				if (this.selectedPath == null || this.selectedPath.Length == 0)
				{
					return this.selectedPath;
				}
				if (this.selectedPathNeedsCheck)
				{
					new FileIOPermission(FileIOPermissionAccess.PathDiscovery, this.selectedPath).Demand();
				}
				return this.selectedPath;
			}
			set
			{
				this.selectedPath = ((value == null) ? string.Empty : value);
				this.selectedPathNeedsCheck = false;
			}
		}

		// Token: 0x170008BC RID: 2236
		// (get) Token: 0x060025D5 RID: 9685 RVA: 0x000AFD83 File Offset: 0x000ADF83
		// (set) Token: 0x060025D6 RID: 9686 RVA: 0x000AFD8B File Offset: 0x000ADF8B
		[Browsable(true)]
		[DefaultValue(Environment.SpecialFolder.Desktop)]
		[Localizable(false)]
		[SRCategory("CatFolderBrowsing")]
		[SRDescription("FolderBrowserDialogRootFolder")]
		[TypeConverter(typeof(SpecialFolderEnumConverter))]
		public Environment.SpecialFolder RootFolder
		{
			get
			{
				return this.rootFolder;
			}
			set
			{
				if (!Enum.IsDefined(typeof(Environment.SpecialFolder), value))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(Environment.SpecialFolder));
				}
				this.rootFolder = value;
			}
		}

		// Token: 0x170008BD RID: 2237
		// (get) Token: 0x060025D7 RID: 9687 RVA: 0x000AFDC1 File Offset: 0x000ADFC1
		// (set) Token: 0x060025D8 RID: 9688 RVA: 0x000AFDC9 File Offset: 0x000ADFC9
		[Browsable(true)]
		[DefaultValue("")]
		[Localizable(true)]
		[SRCategory("CatFolderBrowsing")]
		[SRDescription("FolderBrowserDialogDescription")]
		public string Description
		{
			get
			{
				return this.descriptionText;
			}
			set
			{
				this.descriptionText = ((value == null) ? string.Empty : value);
			}
		}

		// Token: 0x060025D9 RID: 9689 RVA: 0x000AFDDC File Offset: 0x000ADFDC
		private static UnsafeNativeMethods.IMalloc GetSHMalloc()
		{
			UnsafeNativeMethods.IMalloc[] array = new UnsafeNativeMethods.IMalloc[1];
			UnsafeNativeMethods.Shell32.SHGetMalloc(array);
			return array[0];
		}

		// Token: 0x060025DA RID: 9690 RVA: 0x000AFDFA File Offset: 0x000ADFFA
		public override void Reset()
		{
			this.rootFolder = Environment.SpecialFolder.Desktop;
			this.descriptionText = string.Empty;
			this.selectedPath = string.Empty;
			this.selectedPathNeedsCheck = false;
			this.showNewFolderButton = true;
		}

		// Token: 0x060025DB RID: 9691 RVA: 0x000AFE28 File Offset: 0x000AE028
		protected override bool RunDialog(IntPtr hWndOwner)
		{
			IntPtr zero = IntPtr.Zero;
			bool result = false;
			UnsafeNativeMethods.Shell32.SHGetSpecialFolderLocation(hWndOwner, (int)this.rootFolder, ref zero);
			if (zero == IntPtr.Zero)
			{
				UnsafeNativeMethods.Shell32.SHGetSpecialFolderLocation(hWndOwner, 0, ref zero);
				if (zero == IntPtr.Zero)
				{
					throw new InvalidOperationException(SR.GetString("FolderBrowserDialogNoRootFolder"));
				}
			}
			int num = 64;
			if (!this.showNewFolderButton)
			{
				num += 512;
			}
			if (Control.CheckForIllegalCrossThreadCalls && Application.OleRequired() != ApartmentState.STA)
			{
				throw new ThreadStateException(SR.GetString("DebuggingExceptionOnly", new object[]
				{
					SR.GetString("ThreadMustBeSTA")
				}));
			}
			IntPtr intPtr = IntPtr.Zero;
			IntPtr intPtr2 = IntPtr.Zero;
			IntPtr intPtr3 = IntPtr.Zero;
			try
			{
				UnsafeNativeMethods.BROWSEINFO browseinfo = new UnsafeNativeMethods.BROWSEINFO();
				intPtr2 = Marshal.AllocHGlobal(260 * Marshal.SystemDefaultCharSize);
				intPtr3 = Marshal.AllocHGlobal(261 * Marshal.SystemDefaultCharSize);
				this.callback = new UnsafeNativeMethods.BrowseCallbackProc(this.FolderBrowserDialog_BrowseCallbackProc);
				browseinfo.pidlRoot = zero;
				browseinfo.hwndOwner = hWndOwner;
				browseinfo.pszDisplayName = intPtr2;
				browseinfo.lpszTitle = this.descriptionText;
				browseinfo.ulFlags = num;
				browseinfo.lpfn = this.callback;
				browseinfo.lParam = IntPtr.Zero;
				browseinfo.iImage = 0;
				intPtr = UnsafeNativeMethods.Shell32.SHBrowseForFolder(browseinfo);
				if (intPtr != IntPtr.Zero)
				{
					UnsafeNativeMethods.Shell32.SHGetPathFromIDListLongPath(intPtr, ref intPtr3);
					this.selectedPathNeedsCheck = true;
					this.selectedPath = Marshal.PtrToStringAuto(intPtr3);
					result = true;
				}
			}
			finally
			{
				UnsafeNativeMethods.CoTaskMemFree(zero);
				if (intPtr != IntPtr.Zero)
				{
					UnsafeNativeMethods.CoTaskMemFree(intPtr);
				}
				if (intPtr3 != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr3);
				}
				if (intPtr2 != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr2);
				}
				this.callback = null;
			}
			return result;
		}

		// Token: 0x060025DC RID: 9692 RVA: 0x000AFFF8 File Offset: 0x000AE1F8
		private int FolderBrowserDialog_BrowseCallbackProc(IntPtr hwnd, int msg, IntPtr lParam, IntPtr lpData)
		{
			if (msg != 1)
			{
				if (msg == 2)
				{
					if (lParam != IntPtr.Zero)
					{
						IntPtr hglobal = Marshal.AllocHGlobal(261 * Marshal.SystemDefaultCharSize);
						bool flag = UnsafeNativeMethods.Shell32.SHGetPathFromIDListLongPath(lParam, ref hglobal);
						Marshal.FreeHGlobal(hglobal);
						UnsafeNativeMethods.SendMessage(new HandleRef(null, hwnd), 1125, 0, flag ? 1 : 0);
					}
				}
			}
			else if (this.selectedPath.Length != 0)
			{
				UnsafeNativeMethods.SendMessage(new HandleRef(null, hwnd), NativeMethods.BFFM_SETSELECTION, 1, this.selectedPath);
			}
			return 0;
		}

		// Token: 0x04000FBB RID: 4027
		private Environment.SpecialFolder rootFolder;

		// Token: 0x04000FBC RID: 4028
		private string descriptionText;

		// Token: 0x04000FBD RID: 4029
		private string selectedPath;

		// Token: 0x04000FBE RID: 4030
		private bool showNewFolderButton;

		// Token: 0x04000FBF RID: 4031
		private bool selectedPathNeedsCheck;

		// Token: 0x04000FC0 RID: 4032
		private UnsafeNativeMethods.BrowseCallbackProc callback;
	}
}
