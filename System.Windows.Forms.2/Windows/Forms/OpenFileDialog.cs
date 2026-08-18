using System;
using System.ComponentModel;
using System.IO;
using System.Security;
using System.Security.Permissions;

namespace System.Windows.Forms
{
	// Token: 0x02000312 RID: 786
	[SRDescription("DescriptionOpenFileDialog")]
	public sealed class OpenFileDialog : FileDialog
	{
		// Token: 0x17000BB8 RID: 3000
		// (get) Token: 0x0600320C RID: 12812 RVA: 0x000E15FF File Offset: 0x000DF7FF
		// (set) Token: 0x0600320D RID: 12813 RVA: 0x000E1607 File Offset: 0x000DF807
		[DefaultValue(true)]
		[SRDescription("OFDcheckFileExistsDescr")]
		public override bool CheckFileExists
		{
			get
			{
				return base.CheckFileExists;
			}
			set
			{
				base.CheckFileExists = value;
			}
		}

		// Token: 0x17000BB9 RID: 3001
		// (get) Token: 0x0600320E RID: 12814 RVA: 0x000E1610 File Offset: 0x000DF810
		// (set) Token: 0x0600320F RID: 12815 RVA: 0x000E161D File Offset: 0x000DF81D
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("OFDmultiSelectDescr")]
		public bool Multiselect
		{
			get
			{
				return base.GetOption(512);
			}
			set
			{
				base.SetOption(512, value);
			}
		}

		// Token: 0x17000BBA RID: 3002
		// (get) Token: 0x06003210 RID: 12816 RVA: 0x000E162B File Offset: 0x000DF82B
		// (set) Token: 0x06003211 RID: 12817 RVA: 0x000E1634 File Offset: 0x000DF834
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("OFDreadOnlyCheckedDescr")]
		public bool ReadOnlyChecked
		{
			get
			{
				return base.GetOption(1);
			}
			set
			{
				base.SetOption(1, value);
			}
		}

		// Token: 0x17000BBB RID: 3003
		// (get) Token: 0x06003212 RID: 12818 RVA: 0x000E163E File Offset: 0x000DF83E
		// (set) Token: 0x06003213 RID: 12819 RVA: 0x000E164A File Offset: 0x000DF84A
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("OFDshowReadOnlyDescr")]
		public bool ShowReadOnly
		{
			get
			{
				return !base.GetOption(4);
			}
			set
			{
				base.SetOption(4, !value);
			}
		}

		// Token: 0x06003214 RID: 12820 RVA: 0x000E1658 File Offset: 0x000DF858
		public Stream OpenFile()
		{
			IntSecurity.FileDialogOpenFile.Demand();
			string text = base.FileNamesInternal[0];
			if (text == null || text.Length == 0)
			{
				throw new ArgumentNullException("FileName");
			}
			Stream result = null;
			new FileIOPermission(FileIOPermissionAccess.Read, IntSecurity.UnsafeGetFullPath(text)).Assert();
			try
			{
				result = new FileStream(text, FileMode.Open, FileAccess.Read, FileShare.Read);
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
			return result;
		}

		// Token: 0x06003215 RID: 12821 RVA: 0x000E16C4 File Offset: 0x000DF8C4
		public override void Reset()
		{
			base.Reset();
			base.SetOption(4096, true);
		}

		// Token: 0x06003216 RID: 12822 RVA: 0x000E16D8 File Offset: 0x000DF8D8
		internal override void EnsureFileDialogPermission()
		{
			IntSecurity.FileDialogOpenFile.Demand();
		}

		// Token: 0x06003217 RID: 12823 RVA: 0x000E16E4 File Offset: 0x000DF8E4
		internal override bool RunFileDialog(NativeMethods.OPENFILENAME_I ofn)
		{
			IntSecurity.FileDialogOpenFile.Demand();
			bool openFileName = UnsafeNativeMethods.GetOpenFileName(ofn);
			if (!openFileName)
			{
				switch (SafeNativeMethods.CommDlgExtendedError())
				{
				case 12289:
					throw new InvalidOperationException(SR.GetString("FileDialogSubLassFailure"));
				case 12290:
					throw new InvalidOperationException(SR.GetString("FileDialogInvalidFileName", new object[]
					{
						base.FileName
					}));
				case 12291:
					throw new InvalidOperationException(SR.GetString("FileDialogBufferTooSmall"));
				}
			}
			return openFileName;
		}

		// Token: 0x06003218 RID: 12824 RVA: 0x000E1768 File Offset: 0x000DF968
		internal override string[] ProcessVistaFiles(FileDialogNative.IFileDialog dialog)
		{
			FileDialogNative.IFileOpenDialog fileOpenDialog = (FileDialogNative.IFileOpenDialog)dialog;
			if (this.Multiselect)
			{
				FileDialogNative.IShellItemArray shellItemArray;
				fileOpenDialog.GetResults(out shellItemArray);
				uint num;
				shellItemArray.GetCount(out num);
				string[] array = new string[num];
				for (uint num2 = 0U; num2 < num; num2 += 1U)
				{
					FileDialogNative.IShellItem item;
					shellItemArray.GetItemAt(num2, out item);
					array[(int)num2] = FileDialog.GetFilePathFromShellItem(item);
				}
				return array;
			}
			FileDialogNative.IShellItem item2;
			fileOpenDialog.GetResult(out item2);
			return new string[]
			{
				FileDialog.GetFilePathFromShellItem(item2)
			};
		}

		// Token: 0x06003219 RID: 12825 RVA: 0x000E17DA File Offset: 0x000DF9DA
		internal override FileDialogNative.IFileDialog CreateVistaDialog()
		{
			return (FileDialogNative.NativeFileOpenDialog)new FileDialogNative.FileOpenDialogRCW();
		}

		// Token: 0x17000BBC RID: 3004
		// (get) Token: 0x0600321A RID: 12826 RVA: 0x000E17E8 File Offset: 0x000DF9E8
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string SafeFileName
		{
			get
			{
				new FileIOPermission(PermissionState.Unrestricted).Assert();
				string fileName = base.FileName;
				CodeAccessPermission.RevertAssert();
				if (string.IsNullOrEmpty(fileName))
				{
					return "";
				}
				return OpenFileDialog.RemoveSensitivePathInformation(fileName);
			}
		}

		// Token: 0x0600321B RID: 12827 RVA: 0x000E1822 File Offset: 0x000DFA22
		private static string RemoveSensitivePathInformation(string fullPath)
		{
			return Path.GetFileName(fullPath);
		}

		// Token: 0x17000BBD RID: 3005
		// (get) Token: 0x0600321C RID: 12828 RVA: 0x000E182C File Offset: 0x000DFA2C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string[] SafeFileNames
		{
			get
			{
				new FileIOPermission(PermissionState.Unrestricted).Assert();
				string[] fileNames = base.FileNames;
				CodeAccessPermission.RevertAssert();
				if (fileNames == null || fileNames.Length == 0)
				{
					return new string[0];
				}
				string[] array = new string[fileNames.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = OpenFileDialog.RemoveSensitivePathInformation(fileNames[i]);
				}
				return array;
			}
		}

		// Token: 0x17000BBE RID: 3006
		// (get) Token: 0x0600321D RID: 12829 RVA: 0x000E1881 File Offset: 0x000DFA81
		internal override bool SettingsSupportVistaDialog
		{
			get
			{
				return base.SettingsSupportVistaDialog && !this.ShowReadOnly;
			}
		}
	}
}
