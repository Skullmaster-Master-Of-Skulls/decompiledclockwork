using System;
using System.Data.Common.CommandTrees;
using System.Data.Mapping.ViewGeneration.CqlGeneration;
using System.Text;

namespace System.Data.Mapping.ViewGeneration.Structures
{
	// Token: 0x020002A9 RID: 681
	internal sealed class ConstantProjectedSlot : ProjectedSlot
	{
		// Token: 0x06002883 RID: 10371 RVA: 0x0009CE5F File Offset: 0x0009B05F
		internal ConstantProjectedSlot(Constant value, MemberPath memberPath)
		{
			this.m_constant = value;
			this.m_memberPath = memberPath;
		}

		// Token: 0x170007EC RID: 2028
		// (get) Token: 0x06002884 RID: 10372 RVA: 0x0009CE75 File Offset: 0x0009B075
		internal Constant CellConstant
		{
			get
			{
				return this.m_constant;
			}
		}

		// Token: 0x06002885 RID: 10373 RVA: 0x00048AC0 File Offset: 0x00046CC0
		internal override ProjectedSlot DeepQualify(CqlBlock block)
		{
			return this;
		}

		// Token: 0x06002886 RID: 10374 RVA: 0x0009CE7D File Offset: 0x0009B07D
		internal override StringBuilder AsEsql(StringBuilder builder, MemberPath outputMember, string blockAlias, int indentLevel)
		{
			return this.m_constant.AsEsql(builder, outputMember, blockAlias);
		}

		// Token: 0x06002887 RID: 10375 RVA: 0x0009CE8D File Offset: 0x0009B08D
		internal override DbExpression AsCqt(DbExpression row, MemberPath outputMember)
		{
			return this.m_constant.AsCqt(row, outputMember);
		}

		// Token: 0x06002888 RID: 10376 RVA: 0x0009CE9C File Offset: 0x0009B09C
		protected override bool IsEqualTo(ProjectedSlot right)
		{
			ConstantProjectedSlot constantProjectedSlot = right as ConstantProjectedSlot;
			return constantProjectedSlot != null && Constant.EqualityComparer.Equals(this.m_constant, constantProjectedSlot.m_constant);
		}

		// Token: 0x06002889 RID: 10377 RVA: 0x0009CECB File Offset: 0x0009B0CB
		protected override int GetHash()
		{
			return Constant.EqualityComparer.GetHashCode(this.m_constant);
		}

		// Token: 0x0600288A RID: 10378 RVA: 0x0009CEDD File Offset: 0x0009B0DD
		internal override void ToCompactString(StringBuilder builder)
		{
			this.m_constant.ToCompactString(builder);
		}

		// Token: 0x0400125A RID: 4698
		private readonly Constant m_constant;

		// Token: 0x0400125B RID: 4699
		private readonly MemberPath m_memberPath;
	}
}
