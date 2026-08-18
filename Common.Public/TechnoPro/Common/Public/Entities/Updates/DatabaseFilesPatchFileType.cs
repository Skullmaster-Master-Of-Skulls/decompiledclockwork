using System;

namespace TechnoPro.Common.Public.Entities.Updates
{
	// Token: 0x02000146 RID: 326
	public class DatabaseFilesPatchFileType : IUpdateFileType
	{
		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x060007C7 RID: 1991 RVA: 0x00010D5C File Offset: 0x0000EF5C
		public eUpdateFileTypes UpdateFileType
		{
			get
			{
				return eUpdateFileTypes.Database_files_patch;
			}
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x00010D70 File Offset: 0x0000EF70
		public string GetFilenamePattern(int addSize = 0)
		{
			return string.Format("{0}.*.{1}", eUpdateFileTypes.Database_files_patch.GetTitle(), eUpdateFileTypes.Database_files_patch.GetExtension());
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x00010D98 File Offset: 0x0000EF98
		public string GetFileTypeTitle(string fn)
		{
			string[] array = fn.Split(new char[]
			{
				'.'
			});
			return (array.Length == 0) ? string.Empty : array[0];
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x00010DCC File Offset: 0x0000EFCC
		public Version GetFileVersion(string fn)
		{
			Version result;
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
			return result;
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x00010E2C File Offset: 0x0000F02C
		public bool IsHotFix(string fn)
		{
			return fn.ToLower().Contains("hotfix");
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x00010E50 File Offset: 0x0000F050
		public int GetAddressSize(string fn)
		{
			return 0;
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x060007CD RID: 1997 RVA: 0x00010E64 File Offset: 0x0000F064
		public string Extension
		{
			get
			{
				return eUpdateFileTypes.Database_files_patch.GetExtension();
			}
		}
	}
}
