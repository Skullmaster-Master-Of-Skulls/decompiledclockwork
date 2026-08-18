using System;
using System.Collections.Generic;

namespace Oracle.SqlAndPlsqlParser.LocalParsing
{
	// Token: 0x020002E3 RID: 739
	internal abstract class OracleLpSelectTerm : OracleLpStatementElement, IOracleLpColumnDescriptorContainer
	{
		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x06001AD4 RID: 6868 RVA: 0x0010C0D0 File Offset: 0x0010A2D0
		internal override OracleLpStatementElementType ElementType
		{
			get
			{
				return OracleLpStatementElementType.SelectTerm;
			}
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x06001AD5 RID: 6869 RVA: 0x0010C0D4 File Offset: 0x0010A2D4
		public OracleLpSelectTermType Type
		{
			get
			{
				return this.m_vType;
			}
		}

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06001AD6 RID: 6870 RVA: 0x0010C0DC File Offset: 0x0010A2DC
		public List<OracleLpColumnDescriptor> ColumnDescriptors
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

		// Token: 0x06001AD7 RID: 6871 RVA: 0x0010C0F4 File Offset: 0x0010A2F4
		public OracleLpSelectTerm(OracleLpSelectClause sc) : base(sc)
		{
		}

		// Token: 0x06001AD8 RID: 6872
		public abstract void Resolve();

		// Token: 0x04001CE4 RID: 7396
		protected OracleLpSelectTermType m_vType;

		// Token: 0x04001CE5 RID: 7397
		protected List<OracleLpColumnDescriptor> m_vColumnDescriptors;
	}
}
