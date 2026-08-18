using System;
using System.Data.Metadata.Edm;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000EA RID: 234
	internal abstract class ConstantBaseOp : ScalarOp
	{
		// Token: 0x06000CCC RID: 3276 RVA: 0x0003C7B8 File Offset: 0x0003A9B8
		protected ConstantBaseOp(OpType opType, TypeUsage type, object value) : base(opType, type)
		{
			this.m_value = value;
		}

		// Token: 0x06000CCD RID: 3277 RVA: 0x0003C7C9 File Offset: 0x0003A9C9
		protected ConstantBaseOp(OpType opType) : base(opType)
		{
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000CCE RID: 3278 RVA: 0x0003C7D2 File Offset: 0x0003A9D2
		internal virtual object Value
		{
			get
			{
				return this.m_value;
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000CCF RID: 3279 RVA: 0x000173E2 File Offset: 0x000155E2
		internal override int Arity
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06000CD0 RID: 3280 RVA: 0x0003C7DC File Offset: 0x0003A9DC
		internal override bool IsEquivalent(Op other)
		{
			ConstantBaseOp constantBaseOp = other as ConstantBaseOp;
			return constantBaseOp != null && base.OpType == other.OpType && constantBaseOp.Type.EdmEquals(this.Type) && ((constantBaseOp.Value == null && this.Value == null) || constantBaseOp.Value.Equals(this.Value));
		}

		// Token: 0x04000999 RID: 2457
		private readonly object m_value;
	}
}
