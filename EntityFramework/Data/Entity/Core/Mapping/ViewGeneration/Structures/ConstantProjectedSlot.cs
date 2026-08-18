using System;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Mapping.ViewGeneration.CqlGeneration;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Structures
{
	// Token: 0x02000470 RID: 1136
	internal sealed class ConstantProjectedSlot : ProjectedSlot
	{
		// Token: 0x060029C0 RID: 10688 RVA: 0x000C9998 File Offset: 0x000C7B98
		internal ConstantProjectedSlot(Constant value)
		{
			this.m_constant = value;
		}

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x060029C1 RID: 10689 RVA: 0x000C99A7 File Offset: 0x000C7BA7
		internal Constant CellConstant
		{
			get
			{
				return this.m_constant;
			}
		}

		// Token: 0x060029C2 RID: 10690 RVA: 0x000C99AF File Offset: 0x000C7BAF
		internal override ProjectedSlot DeepQualify(CqlBlock block)
		{
			return this;
		}

		// Token: 0x060029C3 RID: 10691 RVA: 0x000C99B2 File Offset: 0x000C7BB2
		internal override StringBuilder AsEsql(StringBuilder builder, MemberPath outputMember, string blockAlias, int indentLevel)
		{
			return this.m_constant.AsEsql(builder, outputMember, blockAlias);
		}

		// Token: 0x060029C4 RID: 10692 RVA: 0x000C99C2 File Offset: 0x000C7BC2
		internal override DbExpression AsCqt(DbExpression row, MemberPath outputMember)
		{
			return this.m_constant.AsCqt(row, outputMember);
		}

		// Token: 0x060029C5 RID: 10693 RVA: 0x000C99D4 File Offset: 0x000C7BD4
		protected override bool IsEqualTo(ProjectedSlot right)
		{
			ConstantProjectedSlot constantProjectedSlot = right as ConstantProjectedSlot;
			return constantProjectedSlot != null && Constant.EqualityComparer.Equals(this.m_constant, constantProjectedSlot.m_constant);
		}

		// Token: 0x060029C6 RID: 10694 RVA: 0x000C9A03 File Offset: 0x000C7C03
		protected override int GetHash()
		{
			return Constant.EqualityComparer.GetHashCode(this.m_constant);
		}

		// Token: 0x060029C7 RID: 10695 RVA: 0x000C9A15 File Offset: 0x000C7C15
		internal override void ToCompactString(StringBuilder builder)
		{
			this.m_constant.ToCompactString(builder);
		}

		// Token: 0x04000F81 RID: 3969
		private readonly Constant m_constant;
	}
}
