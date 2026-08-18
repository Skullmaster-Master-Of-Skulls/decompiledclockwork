using System;
using System.Text;

namespace Oracle.SqlAndPlsqlParser.LocalParsing
{
	// Token: 0x020002DA RID: 730
	public class OracleLpName
	{
		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x06001A91 RID: 6801 RVA: 0x0010B05C File Offset: 0x0010925C
		// (set) Token: 0x06001A92 RID: 6802 RVA: 0x0010B064 File Offset: 0x00109264
		public string DbName
		{
			get
			{
				return this.m_vDbName;
			}
			internal set
			{
				this.m_vDbName = value;
				this.m_vRawName = this.m_vDbName;
				if (this.m_vDbName != this.m_vDbName.ToUpperInvariant())
				{
					this.m_vCS = OracleLpCaseSensitivity.Sensitive;
				}
			}
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x06001A93 RID: 6803 RVA: 0x0010B098 File Offset: 0x00109298
		// (set) Token: 0x06001A94 RID: 6804 RVA: 0x0010B0A0 File Offset: 0x001092A0
		public string RawName
		{
			get
			{
				return this.m_vRawName;
			}
			internal set
			{
				this.m_vRawName = value;
				this.m_vDbName = OracleLpName.GetDbName(this.m_vRawName, out this.m_vCS);
			}
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06001A95 RID: 6805 RVA: 0x0010B0C0 File Offset: 0x001092C0
		public object CaseSensitivity
		{
			get
			{
				return this.m_vCS;
			}
		}

		// Token: 0x06001A96 RID: 6806 RVA: 0x0010B0D0 File Offset: 0x001092D0
		public OracleLpName()
		{
		}

		// Token: 0x06001A97 RID: 6807 RVA: 0x0010B0D8 File Offset: 0x001092D8
		public OracleLpName(string rawName)
		{
			this.RawName = rawName;
		}

		// Token: 0x06001A98 RID: 6808 RVA: 0x0010B0E8 File Offset: 0x001092E8
		public static string GetDbName(string name, out OracleLpCaseSensitivity cs)
		{
			if (string.IsNullOrEmpty(name))
			{
				cs = OracleLpCaseSensitivity.Unknown;
				return name;
			}
			int length = name.Length;
			if (length >= 2 && name[0] == '"' && name[length - 1] == '"')
			{
				cs = OracleLpCaseSensitivity.Sensitive;
				return name.Substring(1, length - 2);
			}
			return OracleLpName.GetDbText(name, out cs);
		}

		// Token: 0x06001A99 RID: 6809 RVA: 0x0010B13C File Offset: 0x0010933C
		public static string GetDbText(string text, out OracleLpCaseSensitivity cs)
		{
			int length = text.Length;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			StringBuilder stringBuilder = new StringBuilder(length);
			foreach (char c in text)
			{
				char c2 = c;
				if (c2 != '"')
				{
					if (c2 == '\'')
					{
						if (flag2)
						{
							flag2 = false;
						}
						else if (!flag)
						{
							flag2 = true;
						}
					}
				}
				else if (flag)
				{
					flag = false;
				}
				else if (!flag2)
				{
					flag = true;
				}
				if (flag)
				{
					flag3 = true;
					stringBuilder.Append(c);
				}
				else if (!char.IsWhiteSpace(c))
				{
					stringBuilder.Append(char.ToUpperInvariant(c));
				}
			}
			cs = (flag3 ? OracleLpCaseSensitivity.Sensitive : OracleLpCaseSensitivity.Insensitive);
			return stringBuilder.ToString();
		}

		// Token: 0x06001A9A RID: 6810 RVA: 0x0010B1E8 File Offset: 0x001093E8
		public override string ToString()
		{
			return this.m_vDbName;
		}

		// Token: 0x04001CA7 RID: 7335
		protected string m_vDbName;

		// Token: 0x04001CA8 RID: 7336
		protected string m_vRawName;

		// Token: 0x04001CA9 RID: 7337
		protected OracleLpCaseSensitivity m_vCS;
	}
}
