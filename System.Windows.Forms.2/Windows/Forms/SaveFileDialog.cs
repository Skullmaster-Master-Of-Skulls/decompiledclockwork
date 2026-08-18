using System;
using System.ComponentModel;
using System.IO;
using System.Security;
using System.Security.Permissions;

namespace System.Windows.Forms
{
	// Token: 0x02000353 RID: 851
	[Designer("System.Windows.Forms.Design.SaveFileDialogDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[SRDescription("DescriptionSaveFileDialog")]
	public sealed class SaveFileDialog : FileDialog
	{
		// Token: 0x17000D2E RID: 3374
		// (get) Token: 0x0600378C RID: 14220 RVA: 0x000F7AE0 File Offset: 0x000F5CE0
		// (set) Token: 0x0600378D RID: 14221 RVA: 0x000F7AED File Offset: 0x000F5CED
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("SaveFileDialogCreatePrompt")]
		public bool CreatePrompt
		{
			get
			{
				return base.GetOption(8192);
			}
			set
			{
				IntSecurity.FileDialogCustomization.Demand();
				base.SetOption(8192, value);
			}
		}

		// Token: 0x17000D2F RID: 3375
		// (get) Token: 0x0600378E RID: 14222 RVA: 0x000F7B05 File Offset: 0x000F5D05
		// (set) Token: 0x0600378F RID: 14223 RVA: 0x000F7B0E File Offset: 0x000F5D0E
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[SRDescription("SaveFileDialogOverWritePrompt")]
		public bool OverwritePrompt
		{
			get
			{
				return base.GetOption(2);
			}
			set
			{
				IntSecurity.FileDialogCustomization.Demand();
				base.SetOption(2, value);
			}
		}

		// Token: 0x06003790 RID: 14224 RVA: 0x000F7B24 File Offset: 0x000F5D24
		public Stream OpenFile()
		{
			IntSecurity.FileDialogSaveFile.Demand();
			string text = base.FileNamesInternal[0];
			if (string.IsNullOrEmpty(text))
			{
				throw new ArgumentNullException("FileName");
			}
			Stream result = null;
			new FileIOPermission(FileIOPermissionAccess.AllAccess, IntSecurity.UnsafeGetFullPath(text)).Assert();
			try
			{
				result = new FileStream(text, FileMode.Create, FileAccess.ReadWrite);
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
			return result;
		}

		// Token: 0x06003791 RID: 14225 RVA: 0x000F7B90 File Offset: 0x000F5D90
		private bool PromptFileCreate(string fileName)
		{
			return base.MessageBoxWithFocusRestore(SR.GetString("FileDialogCreatePrompt", new object[]
			{
				fileName
			}), base.DialogCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
		}

		// Token: 0x06003792 RID: 14226 RVA: 0x000F7BB5 File Offset: 0x000F5DB5
		private bool PromptFileOverwrite(string fileName)
		{
			return base.MessageBoxWithFocusRestore(SR.GetString("FileDialogOverwritePrompt", new object[]
			{
				fileName
			}), base.DialogCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
		}

		// Token: 0x06003793 RID: 14227 RVA: 0x000F7BDC File Offset: 0x000F5DDC
		internal override bool PromptUserIfAppropriate(string fileName)
		{
			return base.PromptUserIfAppropriate(fileName) && ((this.options & 2) == 0 || !FileDialog.FileExists(fileName) || base.UseVistaDialogInternal || this.PromptFileOverwrite(fileName)) && ((this.options & 8192) == 0 || FileDialog.FileExists(fileName) || this.PromptFileCreate(fileName));
		}

		// Token: 0x06003794 RID: 14228 RVA: 0x000F7C3B File Offset: 0x000F5E3B
		public override void Reset()
		{
			base.Reset();
			base.SetOption(2, true);
		}

		// Token: 0x06003795 RID: 14229 RVA: 0x000F7C4B File Offset: 0x000F5E4B
		internal override void EnsureFileDialogPermission()
		{
			IntSecurity.FileDialogSaveFile.Demand();
		}

		// Token: 0x06003796 RID: 14230 RVA: 0x000F7C58 File Offset: 0x000F5E58
		internal override bool RunFileDialog(NativeMethods.OPENFILENAME_I ofn)
		{
			IntSecurity.FileDialogSaveFile.Demand();
			bool saveFileName = UnsafeNativeMethods.GetSaveFileName(ofn);
			if (!saveFileName)
			{
				int num = SafeNativeMethods.CommDlgExtendedError();
				if (num == 12290)
				{
					throw new InvalidOperationException(SR.GetString("FileDialogInvalidFileName", new object[]
					{
						base.FileName
					}));
				}
			}
			return saveFileName;
		}

		// Token: 0x06003797 RID: 14231 RVA: 0x000F7CA8 File Offset: 0x000F5EA8
		internal override string[] ProcessVistaFiles(FileDialogNative.IFileDialog dialog)
		{
			FileDialogNative.IFileSaveDialog fileSaveDialog = (FileDialogNative.IFileSaveDialog)dialog;
			FileDialogNative.IShellItem item;
			dialog.GetResult(out item);
			return new string[]
			{
				FileDialog.GetFilePathFromShellItem(item)
			};
		}

		// Token: 0x06003798 RID: 14232 RVA: 0x000F7CD3 File Offset: 0x000F5ED3
		internal override FileDialogNative.IFileDialog CreateVistaDialog()
		{
			return (FileDialogNative.NativeFileSaveDialog)new FileDialogNative.FileSaveDialogRCW();
		}
	}
}
