using System;
using System.ComponentModel;
using System.Design;
using System.Drawing.Design;
using System.Runtime.InteropServices;

namespace System.Windows.Forms.Design
{
	// Token: 0x020002E4 RID: 740
	public class FolderNameEditor : UITypeEditor
	{
		// Token: 0x06001D9D RID: 7581 RVA: 0x000B3847 File Offset: 0x000B1A47
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

		// Token: 0x06001D9E RID: 7582 RVA: 0x00009D4C File Offset: 0x00007F4C
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.Modal;
		}

		// Token: 0x06001D9F RID: 7583 RVA: 0x00003937 File Offset: 0x00001B37
		protected virtual void InitializeDialog(FolderNameEditor.FolderBrowser folderBrowser)
		{
		}

		// Token: 0x04001787 RID: 6023
		private FolderNameEditor.FolderBrowser folderBrowser;

		// Token: 0x02000571 RID: 1393
		protected sealed class FolderBrowser : Component
		{
			// Token: 0x170009AD RID: 2477
			// (get) Token: 0x060031E4 RID: 12772 RVA: 0x0010F7F9 File Offset: 0x0010D9F9
			// (set) Token: 0x060031E5 RID: 12773 RVA: 0x0010F801 File Offset: 0x0010DA01
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

			// Token: 0x170009AE RID: 2478
			// (get) Token: 0x060031E6 RID: 12774 RVA: 0x0010F80A File Offset: 0x0010DA0A
			public string DirectoryPath
			{
				get
				{
					return this.directoryPath;
				}
			}

			// Token: 0x170009AF RID: 2479
			// (get) Token: 0x060031E7 RID: 12775 RVA: 0x0010F812 File Offset: 0x0010DA12
			// (set) Token: 0x060031E8 RID: 12776 RVA: 0x0010F81A File Offset: 0x0010DA1A
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

			// Token: 0x170009B0 RID: 2480
			// (get) Token: 0x060031E9 RID: 12777 RVA: 0x0010F823 File Offset: 0x0010DA23
			// (set) Token: 0x060031EA RID: 12778 RVA: 0x0010F82B File Offset: 0x0010DA2B
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

			// Token: 0x060031EB RID: 12779 RVA: 0x0010F840 File Offset: 0x0010DA40
			private static UnsafeNativeMethods.IMalloc GetSHMalloc()
			{
				UnsafeNativeMethods.IMalloc[] array = new UnsafeNativeMethods.IMalloc[1];
				UnsafeNativeMethods.Shell32.SHGetMalloc(array);
				return array[0];
			}

			// Token: 0x060031EC RID: 12780 RVA: 0x0010F85E File Offset: 0x0010DA5E
			public DialogResult ShowDialog()
			{
				return this.ShowDialog(null);
			}

			// Token: 0x060031ED RID: 12781 RVA: 0x0010F868 File Offset: 0x0010DA68
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

			// Token: 0x04002167 RID: 8551
			private static readonly int MAX_PATH = 260;

			// Token: 0x04002168 RID: 8552
			private FolderNameEditor.FolderBrowserFolder startLocation;

			// Token: 0x04002169 RID: 8553
			private FolderNameEditor.FolderBrowserStyles publicOptions = FolderNameEditor.FolderBrowserStyles.RestrictToFilesystem;

			// Token: 0x0400216A RID: 8554
			private UnsafeNativeMethods.BrowseInfos privateOptions = UnsafeNativeMethods.BrowseInfos.NewDialogStyle;

			// Token: 0x0400216B RID: 8555
			private string descriptionText = string.Empty;

			// Token: 0x0400216C RID: 8556
			private string directoryPath = string.Empty;
		}

		// Token: 0x02000572 RID: 1394
		protected enum FolderBrowserFolder
		{
			// Token: 0x0400216E RID: 8558
			Desktop,
			// Token: 0x0400216F RID: 8559
			Favorites = 6,
			// Token: 0x04002170 RID: 8560
			MyComputer = 17,
			// Token: 0x04002171 RID: 8561
			MyDocuments = 5,
			// Token: 0x04002172 RID: 8562
			MyPictures = 39,
			// Token: 0x04002173 RID: 8563
			NetAndDialUpConnections = 49,
			// Token: 0x04002174 RID: 8564
			NetworkNeighborhood = 18,
			// Token: 0x04002175 RID: 8565
			Printers = 4,
			// Token: 0x04002176 RID: 8566
			Recent = 8,
			// Token: 0x04002177 RID: 8567
			SendTo,
			// Token: 0x04002178 RID: 8568
			StartMenu = 11,
			// Token: 0x04002179 RID: 8569
			Templates = 21
		}

		// Token: 0x02000573 RID: 1395
		[Flags]
		protected enum FolderBrowserStyles
		{
			// Token: 0x0400217B RID: 8571
			BrowseForComputer = 4096,
			// Token: 0x0400217C RID: 8572
			BrowseForEverything = 16384,
			// Token: 0x0400217D RID: 8573
			BrowseForPrinter = 8192,
			// Token: 0x0400217E RID: 8574
			RestrictToDomain = 2,
			// Token: 0x0400217F RID: 8575
			RestrictToFilesystem = 1,
			// Token: 0x04002180 RID: 8576
			RestrictToSubfolders = 8,
			// Token: 0x04002181 RID: 8577
			ShowTextBox = 16
		}
	}
}
