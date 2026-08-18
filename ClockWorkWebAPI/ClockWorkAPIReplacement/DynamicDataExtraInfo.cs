using System;

namespace ClockWorkWebAPI.ClockWorkAPIReplacement
{
	// Token: 0x02000058 RID: 88
	public class DynamicDataExtraInfo
	{
		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060004A5 RID: 1189 RVA: 0x00020F60 File Offset: 0x0001F160
		public char Code
		{
			get
			{
				return this.code;
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060004A6 RID: 1190 RVA: 0x00020F78 File Offset: 0x0001F178
		public string CodeParams
		{
			get
			{
				return this.codeParams;
			}
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x00020F90 File Offset: 0x0001F190
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

		// Token: 0x0400025A RID: 602
		private char code = ' ';

		// Token: 0x0400025B RID: 603
		private string codeParams = "";
	}
}
