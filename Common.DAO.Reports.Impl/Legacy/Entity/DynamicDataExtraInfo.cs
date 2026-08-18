using System;

namespace TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity
{
	// Token: 0x02000019 RID: 25
	public class DynamicDataExtraInfo
	{
		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x00027B68 File Offset: 0x00025D68
		public char Code
		{
			get
			{
				return this.code;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x00027B80 File Offset: 0x00025D80
		public string CodeParams
		{
			get
			{
				return this.codeParams;
			}
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00027B98 File Offset: 0x00025D98
		public DynamicDataExtraInfo(string s)
		{
			int num = s.IndexOf('.');
			bool flag = num > 0;
			if (flag)
			{
				this.code = s[0];
				this.codeParams = ((num < s.Length) ? s.Substring(num + 1) : "");
			}
			else
			{
				bool flag2 = s.Length > 1;
				if (flag2)
				{
					this.code = s[0];
					this.codeParams = s.Substring(1);
				}
				else
				{
					this.code = ((s.Length == 1) ? s[0] : ' ');
					this.codeParams = "";
				}
			}
		}

		// Token: 0x040000C3 RID: 195
		private char code = ' ';

		// Token: 0x040000C4 RID: 196
		private string codeParams = "";
	}
}
