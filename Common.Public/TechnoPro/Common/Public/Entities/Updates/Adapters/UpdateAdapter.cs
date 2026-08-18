using System;

namespace TechnoPro.Common.Public.Entities.Updates.Adapters
{
	// Token: 0x02000154 RID: 340
	public static class UpdateAdapter
	{
		// Token: 0x0600081F RID: 2079 RVA: 0x00011690 File Offset: 0x0000F890
		public static string GetVersion(this string fn)
		{
			Version versionObject = fn.GetVersionObject();
			return (versionObject != null) ? versionObject.ToString() : string.Empty;
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x000116C0 File Offset: 0x0000F8C0
		public static Version GetVersionObject(this string fn)
		{
			string fileTypeTitle = fn.GetFileTypeTitle();
			IUpdateFileType updateFileType = UpdateFileTypeFactory.GetUpdateFileType(fileTypeTitle);
			bool flag = updateFileType != null;
			Version result;
			if (flag)
			{
				result = updateFileType.GetFileVersion(fn);
			}
			else
			{
				try
				{
					string[] array = fn.Split(new char[]
					{
						'.'
					});
					result = ((array.Length > 2) ? new Version(array[array.Length - 2].Trim().Replace('-', '.')) : null);
				}
				catch
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x00011744 File Offset: 0x0000F944
		public static string GetFileTypeTitle(this string filename)
		{
			string[] array = filename.Split(new char[]
			{
				'.'
			});
			return (array.Length == 0) ? string.Empty : array[0];
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x00011778 File Offset: 0x0000F978
		public static bool IsHotFix(this string fn)
		{
			string fileTypeTitle = fn.GetFileTypeTitle();
			IUpdateFileType updateFileType = UpdateFileTypeFactory.GetUpdateFileType(fileTypeTitle);
			return updateFileType != null && updateFileType.IsHotFix(fn);
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x000117A8 File Offset: 0x0000F9A8
		public static int GetAddressSize(this string fn)
		{
			string fileTypeTitle = fn.GetFileTypeTitle();
			IUpdateFileType updateFileType = UpdateFileTypeFactory.GetUpdateFileType(fileTypeTitle);
			bool flag = updateFileType != null;
			int result;
			if (flag)
			{
				result = updateFileType.GetAddressSize(fn);
			}
			else
			{
				string[] array = fn.Split(new char[]
				{
					'.'
				});
				int num;
				result = ((array.Length > 1 && int.TryParse(array[1].Substring(1), out num)) ? num : 0);
			}
			return result;
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x00011810 File Offset: 0x0000FA10
		public static string GetUpdateType(this ExecuteUpdatesResp executeUpdatesResp)
		{
			return (executeUpdatesResp.Filenames != null && executeUpdatesResp.Filenames.Count > 0) ? executeUpdatesResp.Filenames[0].GetFileTypeTitle() : string.Empty;
		}
	}
}
