using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020005DD RID: 1501
	internal abstract class ConstantBaseOp : ScalarOp
	{
		// Token: 0x06003BD6 RID: 15318 RVA: 0x001187DB File Offset: 0x001169DB
		protected ConstantBaseOp(OpType opType, TypeUsage type, object value) : base(opType, type)
		{
			this.m_value = value;
		}

		// Token: 0x06003BD7 RID: 15319 RVA: 0x001187EC File Offset: 0x001169EC
		protected ConstantBaseOp(OpType opType) : base(opType)
		{
		}

		// Token: 0x17000927 RID: 2343
		// (get) Token: 0x06003BD8 RID: 15320 RVA: 0x001187F5 File Offset: 0x001169F5
		internal virtual object Value
		{
			get
			{
				return this.m_value;
			}
		}

		// Token: 0x17000928 RID: 2344
		// (get) Token: 0x06003BD9 RID: 15321 RVA: 0x001187FD File Offset: 0x001169FD
		internal override int Arity
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06003BDA RID: 15322 RVA: 0x00118800 File Offset: 0x00116A00
		internal override bool IsEquivalent(Op other)
		{
			ConstantBaseOp constantBaseOp = other as ConstantBaseOp;
			return constantBaseOp != null && base.OpType == other.OpType && constantBaseOp.Type.EdmEquals(this.Type) && ((constantBaseOp.Value == null && this.Value == null) || constantBaseOp.Value.Equals(this.Value));
		}

		// Token: 0x04001675 RID: 5749
		private readonly object m_value;
	}
}
