using System;

namespace TechnoPro.Common.Public.Entities.Updates
{
	// Token: 0x02000144 RID: 324
	public class ClockWorkWebUpdateFileType : IUpdateFileType
	{
		// Token: 0x170002CF RID: 719
		// (get) Token: 0x060007B7 RID: 1975 RVA: 0x00010AB4 File Offset: 0x0000ECB4
		public eUpdateFileTypes UpdateFileType
		{
			get
			{
				return eUpdateFileTypes.ClockWorkWeb_update;
			}
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x00010AC8 File Offset: 0x0000ECC8
		public string GetFilenamePattern(int addSize = 0)
		{
			return string.Format("{0}.x{1}.*.{2}", eUpdateFileTypes.ClockWorkWeb_update.GetTitle(), addSize, eUpdateFileTypes.ClockWorkWeb_update.GetExtension());
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x00010AF8 File Offset: 0x0000ECF8
		public string GetFileTypeTitle(string fn)
		{
			string[] array = fn.Split(new char[]
			{
				'.'
			});
			return (array.Length == 0) ? string.Empty : array[0];
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x00010B2C File Offset: 0x0000ED2C
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

		// Token: 0x060007BB RID: 1979 RVA: 0x00010B8C File Offset: 0x0000ED8C
		public bool IsHotFix(string fn)
		{
			return fn.ToLower().Contains("hotfix");
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x00010BB0 File Offset: 0x0000EDB0
		public int GetAddressSize(string fn)
		{
			string[] array = fn.Split(new char[]
			{
				'.'
			});
			int num;
			return (array.Length > 1 && int.TryParse(array[1].Substring(1), out num)) ? num : 0;
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x060007BD RID: 1981 RVA: 0x00010BF0 File Offset: 0x0000EDF0
		public string Extension
		{
			get
			{
				return eUpdateFileTypes.ClockWorkWeb_update.GetExtension();
			}
		}
	}
}
