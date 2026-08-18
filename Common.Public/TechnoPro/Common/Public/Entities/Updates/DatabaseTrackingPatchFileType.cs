using System;

namespace TechnoPro.Common.Public.Entities.Updates
{
	// Token: 0x02000147 RID: 327
	public class DatabaseTrackingPatchFileType : IUpdateFileType
	{
		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x060007CF RID: 1999 RVA: 0x00010E7C File Offset: 0x0000F07C
		public eUpdateFileTypes UpdateFileType
		{
			get
			{
				return eUpdateFileTypes.Database_tracking_patch;
			}
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x00010E90 File Offset: 0x0000F090
		public string GetFilenamePattern(int addSize = 0)
		{
			return string.Format("{0}.*.{1}", eUpdateFileTypes.Database_tracking_patch.GetTitle(), eUpdateFileTypes.Database_tracking_patch.GetExtension());
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x00010EB8 File Offset: 0x0000F0B8
		public string GetFileTypeTitle(string fn)
		{
			string[] array = fn.Split(new char[]
			{
				'.'
			});
			return (array.Length == 0) ? string.Empty : array[0];
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x00010EEC File Offset: 0x0000F0EC
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

		// Token: 0x060007D3 RID: 2003 RVA: 0x00010F4C File Offset: 0x0000F14C
		public bool IsHotFix(string fn)
		{
			return fn.ToLower().Contains("hotfix");
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x00010F70 File Offset: 0x0000F170
		public int GetAddressSize(string fn)
		{
			return 0;
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x060007D5 RID: 2005 RVA: 0x00010F84 File Offset: 0x0000F184
		public string Extension
		{
			get
			{
				return eUpdateFileTypes.Database_tracking_patch.GetExtension();
			}
		}
	}
}
