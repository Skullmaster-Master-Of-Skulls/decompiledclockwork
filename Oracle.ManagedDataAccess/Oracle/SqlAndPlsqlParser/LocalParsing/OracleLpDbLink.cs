using System;
using System.Text;

namespace Oracle.SqlAndPlsqlParser.LocalParsing
{
	// Token: 0x020002AF RID: 687
	internal class OracleLpDbLink : IComparable<OracleLpDbLink>
	{
		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x060019B6 RID: 6582 RVA: 0x001097EC File Offset: 0x001079EC
		// (set) Token: 0x060019B7 RID: 6583 RVA: 0x001097F4 File Offset: 0x001079F4
		public OracleLpName Database
		{
			get
			{
				return this.m_vDatabase;
			}
			set
			{
				this.m_vDatabase = value;
			}
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x060019B8 RID: 6584 RVA: 0x00109800 File Offset: 0x00107A00
		// (set) Token: 0x060019B9 RID: 6585 RVA: 0x00109808 File Offset: 0x00107A08
		public OracleLpName Domain
		{
			get
			{
				return this.m_vDomain;
			}
			set
			{
				this.m_vDomain = value;
			}
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x060019BA RID: 6586 RVA: 0x00109814 File Offset: 0x00107A14
		// (set) Token: 0x060019BB RID: 6587 RVA: 0x0010981C File Offset: 0x00107A1C
		public OracleLpName ConnectionQualifier
		{
			get
			{
				return this.m_vConnectionQualifier;
			}
			set
			{
				this.m_vConnectionQualifier = value;
			}
		}

		// Token: 0x060019BD RID: 6589 RVA: 0x00109830 File Offset: 0x00107A30
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(128);
			stringBuilder.Append(this.m_vDatabase.DbName);
			if (this.m_vDomain != null && this.m_vDomain.DbName != null)
			{
				stringBuilder.Append('.');
				stringBuilder.Append(this.m_vDomain.DbName);
			}
			if (this.m_vConnectionQualifier != null && this.m_vConnectionQualifier.DbName != null)
			{
				stringBuilder.Append('@');
				stringBuilder.Append(this.m_vConnectionQualifier.DbName);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060019BE RID: 6590 RVA: 0x001098C0 File Offset: 0x00107AC0
		public int CompareTo(OracleLpDbLink other)
		{
			if (this == other)
			{
				return 0;
			}
			if (other == null)
			{
				return 1;
			}
			return this.ToString().CompareTo(other.ToString());
		}

		// Token: 0x04001C38 RID: 7224
		protected OracleLpName m_vDatabase;

		// Token: 0x04001C39 RID: 7225
		protected OracleLpName m_vDomain;

		// Token: 0x04001C3A RID: 7226
		protected OracleLpName m_vConnectionQualifier;
	}
}
