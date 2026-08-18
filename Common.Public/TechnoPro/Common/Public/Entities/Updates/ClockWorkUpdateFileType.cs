using System;

namespace TechnoPro.Common.Public.Entities.Updates
{
	// Token: 0x02000143 RID: 323
	public class ClockWorkUpdateFileType : IUpdateFileType
	{
		// Token: 0x170002CD RID: 717
		// (get) Token: 0x060007AF RID: 1967 RVA: 0x00010960 File Offset: 0x0000EB60
		public eUpdateFileTypes UpdateFileType
		{
			get
			{
				return eUpdateFileTypes.ClockWork_update;
			}
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x00010974 File Offset: 0x0000EB74
		public string GetFilenamePattern(int addSize = 0)
		{
			return string.Format("{0}.x{1}.*.{2}", eUpdateFileTypes.ClockWork_update.GetTitle(), addSize, eUpdateFileTypes.ClockWork_update.GetExtension());
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x000109A4 File Offset: 0x0000EBA4
		public string GetFileTypeTitle(string fn)
		{
			string[] array = fn.Split(new char[]
			{
				'.'
			});
			return (array.Length == 0) ? string.Empty : array[0];
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x000109D8 File Offset: 0x0000EBD8
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

		// Token: 0x060007B3 RID: 1971 RVA: 0x00010A38 File Offset: 0x0000EC38
		public bool IsHotFix(string fn)
		{
			return fn.ToLower().Contains("hotfix");
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x00010A5C File Offset: 0x0000EC5C
		public int GetAddressSize(string fn)
		{
			string[] array = fn.Split(new char[]
			{
				'.'
			});
			int num;
			return (array.Length > 1 && int.TryParse(array[1].Substring(1), out num)) ? num : 0;
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x060007B5 RID: 1973 RVA: 0x00010A9C File Offset: 0x0000EC9C
		public string Extension
		{
			get
			{
				return eUpdateFileTypes.ClockWork_update.GetExtension();
			}
		}
	}
}
