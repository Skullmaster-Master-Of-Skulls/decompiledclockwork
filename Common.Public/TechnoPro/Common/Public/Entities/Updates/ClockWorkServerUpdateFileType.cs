using System;

namespace TechnoPro.Common.Public.Entities.Updates
{
	// Token: 0x02000145 RID: 325
	public class ClockWorkServerUpdateFileType : IUpdateFileType
	{
		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x060007BF RID: 1983 RVA: 0x00010C08 File Offset: 0x0000EE08
		public eUpdateFileTypes UpdateFileType
		{
			get
			{
				return eUpdateFileTypes.ClockWorkServer_update;
			}
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x00010C1C File Offset: 0x0000EE1C
		public string GetFilenamePattern(int addSize = 0)
		{
			return string.Format("{0}.x{1}.*.{2}", eUpdateFileTypes.ClockWorkServer_update.GetTitle(), addSize, eUpdateFileTypes.ClockWorkServer_update.GetExtension());
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x00010C4C File Offset: 0x0000EE4C
		public string GetFileTypeTitle(string fn)
		{
			string[] array = fn.Split(new char[]
			{
				'.'
			});
			return (array.Length == 0) ? string.Empty : array[0];
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x00010C80 File Offset: 0x0000EE80
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

		// Token: 0x060007C3 RID: 1987 RVA: 0x00010CE0 File Offset: 0x0000EEE0
		public bool IsHotFix(string fn)
		{
			return fn.ToLower().Contains("hotfix");
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x00010D04 File Offset: 0x0000EF04
		public int GetAddressSize(string fn)
		{
			string[] array = fn.Split(new char[]
			{
				'.'
			});
			int num;
			return (array.Length > 1 && int.TryParse(array[1].Substring(1), out num)) ? num : 0;
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x060007C5 RID: 1989 RVA: 0x00010D44 File Offset: 0x0000EF44
		public string Extension
		{
			get
			{
				return eUpdateFileTypes.ClockWorkServer_update.GetExtension();
			}
		}
	}
}
