using System;
using System.Data.Metadata.Edm;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000BC RID: 188
	internal abstract class ScalarOp : Op
	{
		// Token: 0x06000BF4 RID: 3060 RVA: 0x0003BD06 File Offset: 0x00039F06
		internal ScalarOp(OpType opType, TypeUsage type) : this(opType)
		{
			this.m_type = type;
		}

		// Token: 0x06000BF5 RID: 3061 RVA: 0x0003BD16 File Offset: 0x00039F16
		protected ScalarOp(OpType opType) : base(opType)
		{
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000BF6 RID: 3062 RVA: 0x00017938 File Offset: 0x00015B38
		internal override bool IsScalarOp
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000BF7 RID: 3063 RVA: 0x0003BD1F File Offset: 0x00039F1F
		internal override bool IsEquivalent(Op other)
		{
			return other.OpType == base.OpType && TypeSemantics.IsStructurallyEqual(this.Type, other.Type);
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000BF8 RID: 3064 RVA: 0x0003BD42 File Offset: 0x00039F42
		// (set) Token: 0x06000BF9 RID: 3065 RVA: 0x0003BD4A File Offset: 0x00039F4A
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

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000BFA RID: 3066 RVA: 0x000173E2 File Offset: 0x000155E2
		internal virtual bool IsAggregateOp
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0400094F RID: 2383
		private TypeUsage m_type;
	}
}
