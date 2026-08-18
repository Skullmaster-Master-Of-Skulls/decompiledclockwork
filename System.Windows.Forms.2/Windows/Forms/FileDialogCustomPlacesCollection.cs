using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Security.Permissions;

namespace System.Windows.Forms
{
	// Token: 0x02000251 RID: 593
	public class FileDialogCustomPlacesCollection : Collection<FileDialogCustomPlace>
	{
		// Token: 0x060025AA RID: 9642 RVA: 0x000AF8FC File Offset: 0x000ADAFC
		internal void Apply(FileDialogNative.IFileDialog dialog)
		{
			for (int i = base.Items.Count - 1; i >= 0; i--)
			{
				FileDialogCustomPlace fileDialogCustomPlace = base.Items[i];
				FileIOPermission fileIOPermission = new FileIOPermission(FileIOPermissionAccess.PathDiscovery, fileDialogCustomPlace.Path);
				fileIOPermission.Demand();
				try
				{
					FileDialogNative.IShellItem nativePath = fileDialogCustomPlace.GetNativePath();
					if (nativePath != null)
					{
						dialog.AddPlace(nativePath, 0);
					}
				}
				catch (FileNotFoundException)
				{
				}
			}
		}

		// Token: 0x060025AB RID: 9643 RVA: 0x000AF968 File Offset: 0x000ADB68
		public void Add(string path)
		{
			base.Add(new FileDialogCustomPlace(path));
		}

		// Token: 0x060025AC RID: 9644 RVA: 0x000AF976 File Offset: 0x000ADB76
		public void Add(Guid knownFolderGuid)
		{
			base.Add(new FileDialogCustomPlace(knownFolderGuid));
		}
	}
}
