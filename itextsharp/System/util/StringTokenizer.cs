using System;

namespace System.util
{
	// Token: 0x020004AD RID: 1197
	public class StringTokenizer
	{
		// Token: 0x0600286F RID: 10351 RVA: 0x000F6104 File Offset: 0x000F5104
		public StringTokenizer(string str) : this(str, " \t\n\r\f", false)
		{
		}

		// Token: 0x06002870 RID: 10352 RVA: 0x000F6113 File Offset: 0x000F5113
		public StringTokenizer(string str, string delim) : this(str, delim, false)
		{
		}

		// Token: 0x06002871 RID: 10353 RVA: 0x000F611E File Offset: 0x000F511E
		public StringTokenizer(string str, string delim, bool retDelims)
		{
			this.len = str.Length;
			this.str = str;
			this.delim = delim;
			this.retDelims = retDelims;
			this.pos = 0;
		}

		// Token: 0x06002872 RID: 10354 RVA: 0x000F6150 File Offset: 0x000F5150
		public bool HasMoreTokens()
		{
			if (!this.retDelims)
			{
				while (this.pos < this.len && this.delim.IndexOf(this.str[this.pos]) >= 0)
				{
					this.pos++;
				}
			}
			return this.pos < this.len;
		}

		// Token: 0x06002873 RID: 10355 RVA: 0x000F61B0 File Offset: 0x000F51B0
		public string NextToken(string delim)
		{
			this.delim = delim;
			return this.NextToken();
		}

		// Token: 0x06002874 RID: 10356 RVA: 0x000F61C0 File Offset: 0x000F51C0
		public string NextToken()
		{
			if (this.pos < this.len && this.delim.IndexOf(this.str[this.pos]) >= 0)
			{
				if (this.retDelims)
				{
					return this.str.Substring(this.pos++, 1);
				}
				while (++this.pos < this.len && this.delim.IndexOf(this.str[this.pos]) >= 0)
				{
				}
			}
			if (this.pos < this.len)
			{
				int num = this.pos;
				while (++this.pos < this.len && this.delim.IndexOf(this.str[this.pos]) < 0)
				{
				}
				return this.str.Substring(num, this.pos - num);
			}
			throw new IndexOutOfRangeException();
		}

		// Token: 0x06002875 RID: 10357 RVA: 0x000F62C0 File Offset: 0x000F52C0
		public int CountTokens()
		{
			int num = 0;
			int num2 = 0;
			bool flag = false;
			int i = this.pos;
			while (i < this.len)
			{
				if (this.delim.IndexOf(this.str[i++]) >= 0)
				{
					if (flag)
					{
						num++;
						flag = false;
					}
					num2++;
				}
				else
				{
					flag = true;
					while (i < this.len && this.delim.IndexOf(this.str[i]) < 0)
					{
						i++;
					}
				}
			}
			if (flag)
			{
				num++;
			}
			if (!this.retDelims)
			{
				return num;
			}
			return num + num2;
		}

		// Token: 0x04001CA9 RID: 7337
		private int pos;

		// Token: 0x04001CAA RID: 7338
		private string str;

		// Token: 0x04001CAB RID: 7339
		private int len;

		// Token: 0x04001CAC RID: 7340
		private string delim;

		// Token: 0x04001CAD RID: 7341
		private bool retDelims;
	}
}
