using System;

namespace TechnoPro.Common.Public.Entities.Updates
{
	// Token: 0x02000148 RID: 328
	public class DatabasePatchFileType : IUpdateFileType
	{
		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x060007D7 RID: 2007 RVA: 0x00010F9C File Offset: 0x0000F19C
		public eUpdateFileTypes UpdateFileType
		{
			get
			{
				return eUpdateFileTypes.Database_patch;
			}
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x00010FB0 File Offset: 0x0000F1B0
		public string GetFilenamePattern(int addSize = 0)
		{
			return string.Format("{0}.*.{1}", eUpdateFileTypes.Database_patch.GetTitle(), eUpdateFileTypes.Database_patch.GetExtension());
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x00010FD8 File Offset: 0x0000F1D8
		public string GetFileTypeTitle(string fn)
		{
			string[] array = fn.Split(new char[]
			{
				'.'
			});
			return (array.Length == 0) ? string.Empty : array[0];
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x0001100C File Offset: 0x0000F20C
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

		// Token: 0x060007DB RID: 2011 RVA: 0x0001106C File Offset: 0x0000F26C
		public bool IsHotFix(string fn)
		{
			return fn.ToLower().Contains("hotfix");
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x00011090 File Offset: 0x0000F290
		public int GetAddressSize(string fn)
		{
			return 0;
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x060007DD RID: 2013 RVA: 0x000110A4 File Offset: 0x0000F2A4
		public string Extension
		{
			get
			{
				return eUpdateFileTypes.Database_patch.GetExtension();
			}
		}
	}
}
