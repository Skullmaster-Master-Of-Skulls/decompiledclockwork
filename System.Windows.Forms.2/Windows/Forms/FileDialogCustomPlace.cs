using System;
using System.Globalization;
using System.Text;

namespace System.Windows.Forms
{
	// Token: 0x02000250 RID: 592
	public class FileDialogCustomPlace
	{
		// Token: 0x060025A1 RID: 9633 RVA: 0x000AF7A4 File Offset: 0x000AD9A4
		public FileDialogCustomPlace(string path)
		{
			this.Path = path;
		}

		// Token: 0x060025A2 RID: 9634 RVA: 0x000AF7C9 File Offset: 0x000AD9C9
		public FileDialogCustomPlace(Guid knownFolderGuid)
		{
			this.KnownFolderGuid = knownFolderGuid;
		}

		// Token: 0x170008AD RID: 2221
		// (get) Token: 0x060025A3 RID: 9635 RVA: 0x000AF7EE File Offset: 0x000AD9EE
		// (set) Token: 0x060025A4 RID: 9636 RVA: 0x000AF809 File Offset: 0x000ADA09
		public string Path
		{
			get
			{
				if (string.IsNullOrEmpty(this._path))
				{
					return string.Empty;
				}
				return this._path;
			}
			set
			{
				this._path = (value ?? "");
				this._knownFolderGuid = Guid.Empty;
			}
		}

		// Token: 0x170008AE RID: 2222
		// (get) Token: 0x060025A5 RID: 9637 RVA: 0x000AF826 File Offset: 0x000ADA26
		// (set) Token: 0x060025A6 RID: 9638 RVA: 0x000AF82E File Offset: 0x000ADA2E
		public Guid KnownFolderGuid
		{
			get
			{
				return this._knownFolderGuid;
			}
			set
			{
				this._path = string.Empty;
				this._knownFolderGuid = value;
			}
		}

		// Token: 0x060025A7 RID: 9639 RVA: 0x000AF842 File Offset: 0x000ADA42
		public override string ToString()
		{
			return string.Format(CultureInfo.CurrentCulture, "{0} Path: {1} KnownFolderGuid: {2}", new object[]
			{
				base.ToString(),
				this.Path,
				this.KnownFolderGuid
			});
		}

		// Token: 0x060025A8 RID: 9640 RVA: 0x000AF87C File Offset: 0x000ADA7C
		internal FileDialogNative.IShellItem GetNativePath()
		{
			string text;
			if (!string.IsNullOrEmpty(this._path))
			{
				text = this._path;
			}
			else
			{
				text = FileDialogCustomPlace.GetFolderLocation(this._knownFolderGuid);
			}
			if (string.IsNullOrEmpty(text))
			{
				return null;
			}
			return FileDialog.GetShellItemForPath(text);
		}

		// Token: 0x060025A9 RID: 9641 RVA: 0x000AF8C4 File Offset: 0x000ADAC4
		private static string GetFolderLocation(Guid folderGuid)
		{
			if (!UnsafeNativeMethods.IsVista)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder();
			if (UnsafeNativeMethods.Shell32.SHGetFolderPathEx(ref folderGuid, 0U, IntPtr.Zero, stringBuilder) == 0)
			{
				return stringBuilder.ToString();
			}
			return null;
		}

		// Token: 0x04000FA4 RID: 4004
		private string _path = "";

		// Token: 0x04000FA5 RID: 4005
		private Guid _knownFolderGuid = Guid.Empty;
	}
}
