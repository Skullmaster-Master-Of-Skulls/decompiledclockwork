using System;
using System.Data.Common.CommandTrees;
using System.Data.Common.CommandTrees.ExpressionBuilder;
using System.Data.Common.Utils;
using System.Data.Mapping.ViewGeneration.Structures;
using System.Text;

namespace System.Data.Mapping.ViewGeneration.CqlGeneration
{
	// Token: 0x02000272 RID: 626
	internal sealed class QualifiedSlot : ProjectedSlot
	{
		// Token: 0x0600262B RID: 9771 RVA: 0x00091A61 File Offset: 0x0008FC61
		internal QualifiedSlot(CqlBlock block, ProjectedSlot slot)
		{
			this.m_block = block;
			this.m_slot = slot;
		}

		// Token: 0x0600262C RID: 9772 RVA: 0x00091A78 File Offset: 0x0008FC78
		internal override ProjectedSlot DeepQualify(CqlBlock block)
		{
			return new QualifiedSlot(block, this.m_slot);
		}

		// Token: 0x0600262D RID: 9773 RVA: 0x00091A94 File Offset: 0x0008FC94
		internal override string GetCqlFieldAlias(MemberPath outputMember)
		{
			return this.GetOriginalSlot().GetCqlFieldAlias(outputMember);
		}

		// Token: 0x0600262E RID: 9774 RVA: 0x00091AB0 File Offset: 0x0008FCB0
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

		// Token: 0x0600262F RID: 9775 RVA: 0x00091AD8 File Offset: 0x0008FCD8
		internal string GetQualifiedCqlName(MemberPath outputMember)
		{
			return CqlWriter.GetQualifiedName(this.m_block.CqlAlias, this.GetCqlFieldAlias(outputMember));
		}

		// Token: 0x06002630 RID: 9776 RVA: 0x00091AF1 File Offset: 0x0008FCF1
		internal override StringBuilder AsEsql(StringBuilder builder, MemberPath outputMember, string blockAlias, int indentLevel)
		{
			builder.Append(this.GetQualifiedCqlName(outputMember));
			return builder;
		}

		// Token: 0x06002631 RID: 9777 RVA: 0x00091B02 File Offset: 0x0008FD02
		internal override DbExpression AsCqt(DbExpression row, MemberPath outputMember)
		{
			return this.m_block.GetInput(row).Property(this.GetCqlFieldAlias(outputMember));
		}

		// Token: 0x06002632 RID: 9778 RVA: 0x00091B1C File Offset: 0x0008FD1C
		internal override void ToCompactString(StringBuilder builder)
		{
			StringUtil.FormatStringBuilder(builder, "{0} ", new object[]
			{
				this.m_block.CqlAlias
			});
			this.m_slot.ToCompactString(builder);
		}

		// Token: 0x040011B6 RID: 4534
		private readonly CqlBlock m_block;

		// Token: 0x040011B7 RID: 4535
		private readonly ProjectedSlot m_slot;
	}
}
