using System;

namespace DynamicScreens
{
	// Token: 0x02000075 RID: 117
	public class DynamicDataExtraInfo
	{
		// Token: 0x170001AE RID: 430
		// (get) Token: 0x060005CC RID: 1484 RVA: 0x00047E34 File Offset: 0x00046E34
		public char Code
		{
			get
			{
				return this.code;
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x060005CD RID: 1485 RVA: 0x00047E4C File Offset: 0x00046E4C
		public string CodeParams
		{
			get
			{
				return this.codeParams;
			}
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x00047E64 File Offset: 0x00046E64
		public DynamicDataExtraInfo(string s)
		{
			int num = s.IndexOf('.');
			if (num > 0)
			{
				this.code = s[0];
				this.codeParams = ((num < s.Length) ? s.Substring(num + 1) : "");
			}
			else if (s.Length > 1)
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

		// Token: 0x04000397 RID: 919
		private char code = ' ';

		// Token: 0x04000398 RID: 920
		private string codeParams = "";
	}
}
