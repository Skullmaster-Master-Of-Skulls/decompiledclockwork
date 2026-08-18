using System;
using System.Collections.Generic;
using System.Text;

namespace Oracle.SqlAndPlsqlParser.LocalParsing
{
	// Token: 0x020002CA RID: 714
	internal class OracleLpFromListTerm : OracleLpStatementDataContainer
	{
		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06001A57 RID: 6743 RVA: 0x0010A6C4 File Offset: 0x001088C4
		internal override OracleLpStatementElementType ElementType
		{
			get
			{
				return OracleLpStatementElementType.FromListTerm;
			}
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06001A58 RID: 6744 RVA: 0x0010A6C8 File Offset: 0x001088C8
		public OracleLpFromListTermType Type
		{
			get
			{
				return this.m_vType;
			}
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06001A59 RID: 6745 RVA: 0x0010A6D0 File Offset: 0x001088D0
		// (set) Token: 0x06001A5A RID: 6746 RVA: 0x0010A6D8 File Offset: 0x001088D8
		public OracleLpTableReference TableReference
		{
			get
			{
				return this.m_vTableReference;
			}
			set
			{
				this.m_vTableReference = value;
				if (this.m_vTableReference != null)
				{
					this.m_vTableReference.Parent = this;
				}
			}
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06001A5B RID: 6747 RVA: 0x0010A6F8 File Offset: 0x001088F8
		public List<OracleLpSpecificJoinClause> JoinClauses
		{
			get
			{
				return this.m_vJoinClauses;
			}
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06001A5C RID: 6748 RVA: 0x0010A700 File Offset: 0x00108900
		public override List<OracleLpColumnDescriptor> ColumnDescriptors
		{
			get
			{
				if (this.m_vColumnDescriptors == null)
				{
					this.Resolve();
				}
				return this.m_vColumnDescriptors;
			}
		}

		// Token: 0x06001A5D RID: 6749 RVA: 0x0010A718 File Offset: 0x00108918
		public OracleLpFromListTerm(OracleLpFromListTermType type, OracleLpStatementElement fc) : base(fc)
		{
			this.m_vType = type;
			if (type == OracleLpFromListTermType.JoinClause)
			{
				this.m_vJoinClauses = new List<OracleLpSpecificJoinClause>();
			}
		}

		// Token: 0x06001A5E RID: 6750 RVA: 0x0010A738 File Offset: 0x00108938
		public override void Resolve()
		{
			if (this.m_vType == OracleLpFromListTermType.TableReference)
			{
				this.m_vColumnDescriptors = this.m_vTableReference.ColumnDescriptors;
				return;
			}
			this.m_vColumnDescriptors = new List<OracleLpColumnDescriptor>();
			this.m_vColumnDescriptors.AddRange(this.m_vTableReference.ColumnDescriptors);
			foreach (OracleLpSpecificJoinClause oracleLpSpecificJoinClause in this.m_vJoinClauses)
			{
				this.m_vColumnDescriptors.AddRange(oracleLpSpecificJoinClause.TableReference.ColumnDescriptors);
			}
		}

		// Token: 0x06001A5F RID: 6751 RVA: 0x0010A7D8 File Offset: 0x001089D8
		public override void RetrieveNamedObjectReferences(OracleLpStatement statement)
		{
			this.m_vTableReference.RetrieveNamedObjectReferences(statement);
			if (this.m_vType == OracleLpFromListTermType.JoinClause)
			{
				foreach (OracleLpSpecificJoinClause oracleLpSpecificJoinClause in this.m_vJoinClauses)
				{
					oracleLpSpecificJoinClause.RetrieveNamedObjectReferences(statement);
				}
			}
		}

		// Token: 0x06001A60 RID: 6752 RVA: 0x0010A840 File Offset: 0x00108A40
		internal override void ToString(StringBuilder sb)
		{
			string depthIndent = base.DepthIndent;
			sb.Append(depthIndent);
			sb.Append("Type: ");
			sb.Append(this.m_vType);
			sb.Append('\n');
			this.m_vTableReference.ToString(sb);
			if (this.m_vType == OracleLpFromListTermType.JoinClause)
			{
				this.m_vJoinClauses.ForEach(delegate(OracleLpSpecificJoinClause jc)
				{
					jc.ToString(sb);
				});
			}
		}

		// Token: 0x04001C7D RID: 7293
		protected OracleLpFromListTermType m_vType;

		// Token: 0x04001C7E RID: 7294
		protected OracleLpTableReference m_vTableReference;

		// Token: 0x04001C7F RID: 7295
		protected List<OracleLpSpecificJoinClause> m_vJoinClauses;

		// Token: 0x04001C80 RID: 7296
		protected List<OracleLpColumnDescriptor> m_vColumnDescriptors;
	}
}
