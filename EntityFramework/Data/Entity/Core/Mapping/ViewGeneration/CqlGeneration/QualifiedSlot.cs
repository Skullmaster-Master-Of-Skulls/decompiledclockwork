using System;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.Structures;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.CqlGeneration
{
	// Token: 0x02000424 RID: 1060
	internal sealed class QualifiedSlot : ProjectedSlot
	{
		// Token: 0x06002713 RID: 10003 RVA: 0x000BDC1D File Offset: 0x000BBE1D
		internal QualifiedSlot(CqlBlock block, ProjectedSlot slot)
		{
			this.m_block = block;
			this.m_slot = slot;
		}

		// Token: 0x06002714 RID: 10004 RVA: 0x000BDC34 File Offset: 0x000BBE34
		internal override ProjectedSlot DeepQualify(CqlBlock block)
		{
			return new QualifiedSlot(block, this.m_slot);
		}

		// Token: 0x06002715 RID: 10005 RVA: 0x000BDC50 File Offset: 0x000BBE50
		internal override string GetCqlFieldAlias(MemberPath outputMember)
		{
			return this.GetOriginalSlot().GetCqlFieldAlias(outputMember);
		}

		// Token: 0x06002716 RID: 10006 RVA: 0x000BDC6C File Offset: 0x000BBE6C
		internal ProjectedSlot GetOriginalSlot()
		{
			ProjectedSlot slot = this.m_slot;
			for (;;)
			{
				QualifiedSlot qualifiedSlot = slot as QualifiedSlot;
				if (qualifiedSlot == null)
				{
					break;
				}
				slot = qualifiedSlot.m_slot;
			}
			return slot;
		}

		// Token: 0x06002717 RID: 10007 RVA: 0x000BDC94 File Offset: 0x000BBE94
		internal string GetQualifiedCqlName(MemberPath outputMember)
		{
			return CqlWriter.GetQualifiedName(this.m_block.CqlAlias, this.GetCqlFieldAlias(outputMember));
		}

		// Token: 0x06002718 RID: 10008 RVA: 0x000BDCAD File Offset: 0x000BBEAD
		internal override StringBuilder AsEsql(StringBuilder builder, MemberPath outputMember, string blockAlias, int indentLevel)
		{
			builder.Append(this.GetQualifiedCqlName(outputMember));
			return builder;
		}

		// Token: 0x06002719 RID: 10009 RVA: 0x000BDCBE File Offset: 0x000BBEBE
		internal override DbExpression AsCqt(DbExpression row, MemberPath outputMember)
		{
			return this.m_block.GetInput(row).Property(this.GetCqlFieldAlias(outputMember));
		}

		// Token: 0x0600271A RID: 10010 RVA: 0x000BDCD8 File Offset: 0x000BBED8
		internal override void ToCompactString(StringBuilder builder)
		{
			StringUtil.FormatStringBuilder(builder, "{0} ", new object[]
			{
				this.m_block.CqlAlias
			});
			this.m_slot.ToCompactString(builder);
		}

		// Token: 0x04000EAC RID: 3756
		private readonly CqlBlock m_block;

		// Token: 0x04000EAD RID: 3757
		private readonly ProjectedSlot m_slot;
	}
}
