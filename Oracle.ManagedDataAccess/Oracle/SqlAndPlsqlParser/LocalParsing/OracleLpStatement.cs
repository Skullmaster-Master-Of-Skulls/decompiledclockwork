using System;
using System.Collections.Generic;
using System.Text;
using OracleInternal.Common;

namespace Oracle.SqlAndPlsqlParser.LocalParsing
{
	// Token: 0x020001CA RID: 458
	public class OracleLpStatement : OracleLpStatementElement
	{
		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x0600118C RID: 4492 RVA: 0x000C3C64 File Offset: 0x000C1E64
		internal override OracleLpStatementElementType ElementType
		{
			get
			{
				return OracleLpStatementElementType.Statement;
			}
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x0600118D RID: 4493 RVA: 0x000C3C68 File Offset: 0x000C1E68
		public virtual OracleLpStatementType StatementType
		{
			get
			{
				return OracleLpStatementType.Unknown;
			}
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x0600118E RID: 4494 RVA: 0x000C3C6C File Offset: 0x000C1E6C
		public OracleLpTextFragment Text
		{
			get
			{
				return this.m_vText;
			}
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x0600118F RID: 4495 RVA: 0x000C3C74 File Offset: 0x000C1E74
		public List<OracleLpBindParameter> BindParameters
		{
			get
			{
				return this.m_vBindParameters;
			}
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06001190 RID: 4496 RVA: 0x000C3C7C File Offset: 0x000C1E7C
		public bool HasBindParameters
		{
			get
			{
				return this.m_vBindParameters != null && this.m_vBindParameters.Count != 0;
			}
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06001191 RID: 4497 RVA: 0x000C3C9C File Offset: 0x000C1E9C
		// (set) Token: 0x06001192 RID: 4498 RVA: 0x000C3CA4 File Offset: 0x000C1EA4
		public bool HasReturningClause
		{
			get
			{
				return this.m_vHasReturningClause;
			}
			internal set
			{
				this.m_vHasReturningClause = value;
			}
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06001193 RID: 4499 RVA: 0x000C3CB0 File Offset: 0x000C1EB0
		internal object ODPContext
		{
			get
			{
				return this.m_vODPContext;
			}
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06001194 RID: 4500 RVA: 0x000C3CB8 File Offset: 0x000C1EB8
		internal List<OracleLpQteNamedObject> NamedObjectsReferences
		{
			get
			{
				return this.m_vNamedObjectsReferences;
			}
		}

		// Token: 0x06001195 RID: 4501 RVA: 0x000C3CC0 File Offset: 0x000C1EC0
		internal OracleLpStatement(OracleLpTextFragment text, IOracleMetadata odpContext) : base(null)
		{
			this.m_vText = text;
			this.m_vODPContext = odpContext;
		}

		// Token: 0x06001196 RID: 4502 RVA: 0x000C3CD8 File Offset: 0x000C1ED8
		internal void AddParameter(OracleLpBindParameter param)
		{
			if (this.m_vBindParameters == null)
			{
				this.m_vBindParameters = new List<OracleLpBindParameter>();
			}
			this.m_vBindParameters.Add(param);
		}

		// Token: 0x06001197 RID: 4503 RVA: 0x000C3CFC File Offset: 0x000C1EFC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(256);
			this.ToString(stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x06001198 RID: 4504 RVA: 0x000C3D24 File Offset: 0x000C1F24
		internal override void ToString(StringBuilder sb)
		{
			sb.Append("Statement Type:  ");
			sb.Append(this.StatementType.ToString());
			sb.Append('\n');
			sb.Append("Text:  ");
			sb.Append(this.Text);
			sb.Append('\n');
			sb.Append("Bind parameters: ");
			if (this.m_vBindParameters == null)
			{
				sb.Append("None\n");
			}
			else
			{
				sb.Append(this.m_vBindParameters.Count);
				sb.Append('\n');
				this.m_vBindParameters.ForEach(delegate(OracleLpBindParameter bp)
				{
					sb.Append(bp.ToString());
				});
			}
			sb.Append("Has RETURNING clause:  ");
			sb.Append(this.m_vHasReturningClause);
			sb.Append("\n\n");
		}

		// Token: 0x040013F1 RID: 5105
		protected OracleLpTextFragment m_vText;

		// Token: 0x040013F2 RID: 5106
		protected List<OracleLpBindParameter> m_vBindParameters;

		// Token: 0x040013F3 RID: 5107
		protected bool m_vHasReturningClause;

		// Token: 0x040013F4 RID: 5108
		internal IOracleMetadata m_vODPContext;

		// Token: 0x040013F5 RID: 5109
		internal List<OracleLpQteNamedObject> m_vNamedObjectsReferences;
	}
}
