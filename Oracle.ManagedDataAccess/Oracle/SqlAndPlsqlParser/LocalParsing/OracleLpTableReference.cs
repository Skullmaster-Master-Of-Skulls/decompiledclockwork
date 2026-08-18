using System;
using System.Collections.Generic;
using System.Text;

namespace Oracle.SqlAndPlsqlParser.LocalParsing
{
	// Token: 0x020002FA RID: 762
	internal class OracleLpTableReference : OracleLpStatementDataContainer
	{
		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x06001B49 RID: 6985 RVA: 0x0010D2D8 File Offset: 0x0010B4D8
		internal override OracleLpStatementElementType ElementType
		{
			get
			{
				return OracleLpStatementElementType.TableReference;
			}
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x06001B4A RID: 6986 RVA: 0x0010D2DC File Offset: 0x0010B4DC
		// (set) Token: 0x06001B4B RID: 6987 RVA: 0x0010D2E4 File Offset: 0x0010B4E4
		public OracleLpQueryTableExpression QueryTableExpression
		{
			get
			{
				return this.m_vQueryTableExpression;
			}
			set
			{
				this.m_vQueryTableExpression = value;
				if (this.m_vQueryTableExpression != null)
				{
					this.m_vQueryTableExpression.Parent = this;
				}
			}
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x06001B4C RID: 6988 RVA: 0x0010D304 File Offset: 0x0010B504
		// (set) Token: 0x06001B4D RID: 6989 RVA: 0x0010D30C File Offset: 0x0010B50C
		public bool OnlyQTE
		{
			get
			{
				return this.m_vOnlyQTE;
			}
			set
			{
				this.m_vOnlyQTE = value;
			}
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x06001B4E RID: 6990 RVA: 0x0010D318 File Offset: 0x0010B518
		// (set) Token: 0x06001B4F RID: 6991 RVA: 0x0010D320 File Offset: 0x0010B520
		public OracleLpTableReferenceType TableReferenceType
		{
			get
			{
				return this.m_vTableReferenceType;
			}
			internal set
			{
				this.m_vTableReferenceType = value;
			}
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x06001B50 RID: 6992 RVA: 0x0010D32C File Offset: 0x0010B52C
		// (set) Token: 0x06001B51 RID: 6993 RVA: 0x0010D334 File Offset: 0x0010B534
		public OracleLpName Alias
		{
			get
			{
				return this.m_vAlias;
			}
			internal set
			{
				this.m_vAlias = value;
			}
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x06001B52 RID: 6994 RVA: 0x0010D340 File Offset: 0x0010B540
		// (set) Token: 0x06001B53 RID: 6995 RVA: 0x0010D348 File Offset: 0x0010B548
		public bool InNullableJoin
		{
			get
			{
				return this.m_vInNullableJoin;
			}
			set
			{
				this.m_vInNullableJoin = value;
			}
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x06001B54 RID: 6996 RVA: 0x0010D354 File Offset: 0x0010B554
		public override List<OracleLpColumnDescriptor> ColumnDescriptors
		{
			get
			{
				if (this.m_vQueryTableExpression != null)
				{
					return this.m_vQueryTableExpression.ColumnDescriptors;
				}
				return null;
			}
		}

		// Token: 0x06001B55 RID: 6997 RVA: 0x0010D36C File Offset: 0x0010B56C
		public OracleLpTableReference(OracleLpStatementElement se) : base(se)
		{
		}

		// Token: 0x06001B56 RID: 6998 RVA: 0x0010D378 File Offset: 0x0010B578
		public override void RetrieveNamedObjectReferences(OracleLpStatement statement)
		{
			if (this.m_vQueryTableExpression != null)
			{
				this.m_vQueryTableExpression.RetrieveNamedObjectReferences(statement);
			}
		}

		// Token: 0x06001B57 RID: 6999 RVA: 0x0010D390 File Offset: 0x0010B590
		internal override void ToString(StringBuilder sb)
		{
			string depthIndent = base.DepthIndent;
			sb.Append(depthIndent);
			sb.Append("Type:  ");
			sb.Append(this.m_vTableReferenceType);
			sb.Append("  Alias: ");
			sb.Append((this.m_vAlias == null) ? "none" : (this.m_vAlias.DbName ?? "none"));
			if (this.m_vQueryTableExpression != null)
			{
				this.m_vQueryTableExpression.ToString(sb);
			}
		}

		// Token: 0x04001D42 RID: 7490
		protected OracleLpQueryTableExpression m_vQueryTableExpression;

		// Token: 0x04001D43 RID: 7491
		protected bool m_vOnlyQTE;

		// Token: 0x04001D44 RID: 7492
		protected OracleLpTableReferenceType m_vTableReferenceType;

		// Token: 0x04001D45 RID: 7493
		protected OracleLpName m_vAlias;

		// Token: 0x04001D46 RID: 7494
		protected bool m_vInNullableJoin;
	}
}
