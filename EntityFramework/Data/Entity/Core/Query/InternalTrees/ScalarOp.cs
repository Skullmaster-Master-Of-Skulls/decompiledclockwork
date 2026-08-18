using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020005C7 RID: 1479
	internal abstract class ScalarOp : Op
	{
		// Token: 0x06003B26 RID: 15142 RVA: 0x00117EFD File Offset: 0x001160FD
		internal ScalarOp(OpType opType, TypeUsage type) : this(opType)
		{
			this.m_type = type;
		}

		// Token: 0x06003B27 RID: 15143 RVA: 0x00117F0D File Offset: 0x0011610D
		protected ScalarOp(OpType opType) : base(opType)
		{
		}

		// Token: 0x17000903 RID: 2307
		// (get) Token: 0x06003B28 RID: 15144 RVA: 0x00117F16 File Offset: 0x00116116
		internal override bool IsScalarOp
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003B29 RID: 15145 RVA: 0x00117F19 File Offset: 0x00116119
		internal override bool IsEquivalent(Op other)
		{
			return other.OpType == base.OpType && TypeSemantics.IsStructurallyEqual(this.Type, other.Type);
		}

		// Token: 0x17000904 RID: 2308
		// (get) Token: 0x06003B2A RID: 15146 RVA: 0x00117F3C File Offset: 0x0011613C
		// (set) Token: 0x06003B2B RID: 15147 RVA: 0x00117F44 File Offset: 0x00116144
		internal override TypeUsage Type
		{
			get
			{
				return this.m_type;
			}
			set
			{
				this.m_type = value;
			}
		}

		// Token: 0x17000905 RID: 2309
		// (get) Token: 0x06003B2C RID: 15148 RVA: 0x00117F4D File Offset: 0x0011614D
		internal virtual bool IsAggregateOp
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04001651 RID: 5713
		private TypeUsage m_type;
	}
}
