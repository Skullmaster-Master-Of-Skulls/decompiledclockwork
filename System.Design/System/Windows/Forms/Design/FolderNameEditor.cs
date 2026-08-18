using System;
using System.ComponentModel;
using System.Design;
using System.Drawing.Design;
using System.Runtime.InteropServices;

namespace System.Windows.Forms.Design
{
	// Token: 0x02000238 RID: 568
	public class FolderNameEditor : UITypeEditor
	{
		// Token: 0x0600158F RID: 5519 RVA: 0x00070ED7 File Offset: 0x0006FED7
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			if (this.folderBrowser == null)
			{
				this.folderBrowser = new FolderNameEditor.FolderBrowser();
				this.InitializeDialog(this.folderBrowser);
			}
			if (this.folderBrowser.ShowDialog() != DialogResult.OK)
			{
				return value;
			}
			return this.folderBrowser.DirectoryPath;
		}

		// Token: 0x06001590 RID: 5520 RVA: 0x00070F13 File Offset: 0x0006FF13
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.Modal;
		}

		// Token: 0x06001591 RID: 5521 RVA: 0x00070F16 File Offset: 0x0006FF16
		protected virtual void InitializeDialog(FolderNameEditor.FolderBrowser folderBrowser)
		{
		}

		// Token: 0x0400129C RID: 4764
		private FolderNameEditor.FolderBrowser folderBrowser;

		// Token: 0x02000239 RID: 569
		protected sealed class FolderBrowser : Component
		{
			// Token: 0x1700037E RID: 894
			// (get) Token: 0x06001593 RID: 5523 RVA: 0x00070F20 File Offset: 0x0006FF20
			// (set) Token: 0x06001594 RID: 5524 RVA: 0x00070F28 File Offset: 0x0006FF28
			public FolderNameEditor.FolderBrowserStyles Style
			{
				get
				{
					return this.publicOptions;
				}
				set
				{
					this.publicOptions = value;
				}
			}

			// Token: 0x1700037F RID: 895
			// (get) Token: 0x06001595 RID: 5525 RVA: 0x00070F31 File Offset: 0x0006FF31
			public string DirectoryPath
			{
				get
				{
					return this.directoryPath;
				}
			}

			// Token: 0x17000380 RID: 896
			// (get) Token: 0x06001596 RID: 5526 RVA: 0x00070F39 File Offset: 0x0006FF39
			// (set) Token: 0x06001597 RID: 5527 RVA: 0x00070F41 File Offset: 0x0006FF41
			public FolderNameEditor.FolderBrowserFolder StartLocation
			{
				get
				{
					return this.startLocation;
				}
				set
				{
					this.startLocation = value;
				}
			}

			// Token: 0x17000381 RID: 897
			// (get) Token: 0x06001598 RID: 5528 RVA: 0x00070F4A File Offset: 0x0006FF4A
			// (set) Token: 0x06001599 RID: 5529 RVA: 0x00070F52 File Offset: 0x0006FF52
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

			// Token: 0x0600159A RID: 5530 RVA: 0x00070F68 File Offset: 0x0006FF68
			private static UnsafeNativeMethods.IMalloc GetSHMalloc()
			{
				UnsafeNativeMethods.IMalloc[] array = new UnsafeNativeMethods.IMalloc[1];
				UnsafeNativeMethods.Shell32.SHGetMalloc(array);
				return array[0];
			}

			// Token: 0x0600159B RID: 5531 RVA: 0x00070F86 File Offset: 0x0006FF86
			public DialogResult ShowDialog()
			{
				return this.ShowDialog(null);
			}

			// Token: 0x0600159C RID: 5532 RVA: 0x00070F90 File Offset: 0x0006FF90
			public DialogResult ShowDialog(IWin32Window owner)
			{
				IntPtr zero = IntPtr.Zero;
				IntPtr intPtr;
				if (owner != null)
				{
					intPtr = owner.Handle;
				}
				else
				{
					intPtr = UnsafeNativeMethods.GetActiveWindow();
				}
				UnsafeNativeMethods.Shell32.SHGetSpecialFolderLocation(intPtr, (int)this.startLocation, ref zero);
				if (zero == IntPtr.Zero)
				{
					return DialogResult.Cancel;
				}
				int num = (int)(this.publicOptions | (FolderNameEditor.FolderBrowserStyles)this.privateOptions);
				if ((num & 64) != 0)
				{
					Application.OleRequired();
				}
				IntPtr intPtr2 = IntPtr.Zero;
				try
				{
					UnsafeNativeMethods.BROWSEINFO browseinfo = new UnsafeNativeMethods.BROWSEINFO();
					IntPtr intPtr3 = Marshal.AllocHGlobal(FolderNameEditor.FolderBrowser.MAX_PATH);
					browseinfo.pidlRoot = zero;
					browseinfo.hwndOwner = intPtr;
					browseinfo.pszDisplayName = intPtr3;
					browseinfo.lpszTitle = this.descriptionText;
					browseinfo.ulFlags = num;
					browseinfo.lpfn = IntPtr.Zero;
					browseinfo.lParam = IntPtr.Zero;
					browseinfo.iImage = 0;
					intPtr2 = UnsafeNativeMethods.Shell32.SHBrowseForFolder(browseinfo);
					if (intPtr2 == IntPtr.Zero)
					{
						return DialogResult.Cancel;
					}
					UnsafeNativeMethods.Shell32.SHGetPathFromIDList(intPtr2, intPtr3);
					this.directoryPath = Marshal.PtrToStringAuto(intPtr3);
					Marshal.FreeHGlobal(intPtr3);
				}
				finally
				{
					UnsafeNativeMethods.IMalloc shmalloc = FolderNameEditor.FolderBrowser.GetSHMalloc();
					shmalloc.Free(zero);
					if (intPtr2 != IntPtr.Zero)
					{
						shmalloc.Free(intPtr2);
					}
				}
				return DialogResult.OK;
			}

			// Token: 0x0400129D RID: 4765
			private static readonly int MAX_PATH = 260;

			// Token: 0x0400129E RID: 4766
			private FolderNameEditor.FolderBrowserFolder startLocation;

			// Token: 0x0400129F RID: 4767
			private FolderNameEditor.FolderBrowserStyles publicOptions = FolderNameEditor.FolderBrowserStyles.RestrictToFilesystem;

			// Token: 0x040012A0 RID: 4768
			private UnsafeNativeMethods.BrowseInfos privateOptions = UnsafeNativeMethods.BrowseInfos.NewDialogStyle;

			// Token: 0x040012A1 RID: 4769
			private string descriptionText = string.Empty;

			// Token: 0x040012A2 RID: 4770
			private string directoryPath = string.Empty;
		}

		// Token: 0x0200023A RID: 570
		protected enum FolderBrowserFolder
		{
			// Token: 0x040012A4 RID: 4772
			Desktop,
			// Token: 0x040012A5 RID: 4773
			Favorites = 6,
			// Token: 0x040012A6 RID: 4774
			MyComputer = 17,
			// Token: 0x040012A7 RID: 4775
			MyDocuments = 5,
			// Token: 0x040012A8 RID: 4776
			MyPictures = 39,
			// Token: 0x040012A9 RID: 4777
			NetAndDialUpConnections = 49,
			// Token: 0x040012AA RID: 4778
			NetworkNeighborhood = 18,
			// Token: 0x040012AB RID: 4779
			Printers = 4,
			// Token: 0x040012AC RID: 4780
			Recent = 8,
			// Token: 0x040012AD RID: 4781
			SendTo,
			// Token: 0x040012AE RID: 4782
			StartMenu = 11,
			// Token: 0x040012AF RID: 4783
			Templates = 21
		}

		// Token: 0x0200023B RID: 571
		[Flags]
		protected enum FolderBrowserStyles
		{
			// Token: 0x040012B1 RID: 4785
			BrowseForComputer = 4096,
			// Token: 0x040012B2 RID: 4786
			BrowseForEverything = 16384,
			// Token: 0x040012B3 RID: 4787
			BrowseForPrinter = 8192,
			// Token: 0x040012B4 RID: 4788
			RestrictToDomain = 2,
			// Token: 0x040012B5 RID: 4789
			RestrictToFilesystem = 1,
			// Token: 0x040012B6 RID: 4790
			RestrictToSubfolders = 8,
			// Token: 0x040012B7 RID: 4791
			ShowTextBox = 16
		}
	}
}
