using System;
using System.Text;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x020003FF RID: 1023
	public class X509NameTokenizer
	{
		// Token: 0x060022F9 RID: 8953 RVA: 0x000D7A3A File Offset: 0x000D6A3A
		public X509NameTokenizer(string oid) : this(oid, ',')
		{
		}

		// Token: 0x060022FA RID: 8954 RVA: 0x000D7A45 File Offset: 0x000D6A45
		public X509NameTokenizer(string oid, char separator)
		{
			this.value = oid;
			this.index = -1;
			this.separator = separator;
		}

		// Token: 0x060022FB RID: 8955 RVA: 0x000D7A6D File Offset: 0x000D6A6D
		public bool HasMoreTokens()
		{
			return this.index != this.value.Length;
		}

		// Token: 0x060022FC RID: 8956 RVA: 0x000D7A88 File Offset: 0x000D6A88
		public string NextToken()
		{
			if (this.index == this.value.Length)
			{
				return null;
			}
			int num = this.index + 1;
			bool flag = false;
			bool flag2 = false;
			this.buffer.Remove(0, this.buffer.Length);
			while (num != this.value.Length)
			{
				char c = this.value[num];
				if (c == '"')
				{
					if (!flag2)
					{
						flag = !flag;
					}
					else
					{
						this.buffer.Append(c);
						flag2 = false;
					}
				}
				else if (flag2 || flag)
				{
					if (c == '#' && this.buffer[this.buffer.Length - 1] == '=')
					{
						this.buffer.Append('\\');
					}
					else if (c == '+' && this.separator != '+')
					{
						this.buffer.Append('\\');
					}
					this.buffer.Append(c);
					flag2 = false;
				}
				else if (c == '\\')
				{
					flag2 = true;
				}
				else
				{
					if (c == this.separator)
					{
						break;
					}
					this.buffer.Append(c);
				}
				num++;
			}
			this.index = num;
			return this.buffer.ToString().Trim();
		}

		// Token: 0x040017D2 RID: 6098
		private string value;

		// Token: 0x040017D3 RID: 6099
		private int index;

		// Token: 0x040017D4 RID: 6100
		private char separator;

		// Token: 0x040017D5 RID: 6101
		private StringBuilder buffer = new StringBuilder();
	}
}
