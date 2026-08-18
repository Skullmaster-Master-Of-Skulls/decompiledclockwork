using System;
using System.Collections.Generic;
using System.Data.Common.CommandTrees;
using System.Data.Common.Utils;
using System.Data.Mapping.ViewGeneration.Structures;
using System.Text;

namespace System.Data.Mapping.ViewGeneration.Validation
{
	// Token: 0x02000287 RID: 647
	internal class ViewCellSlot : ProjectedSlot
	{
		// Token: 0x060026C4 RID: 9924 RVA: 0x00095BC8 File Offset: 0x00093DC8
		internal ViewCellSlot(int slotNum, MemberProjectedSlot cSlot, MemberProjectedSlot sSlot)
		{
			this.m_slotNum = slotNum;
			this.m_cSlot = cSlot;
			this.m_sSlot = sSlot;
		}

		// Token: 0x1700079A RID: 1946
		// (get) Token: 0x060026C5 RID: 9925 RVA: 0x00095BE5 File Offset: 0x00093DE5
		internal MemberProjectedSlot CSlot
		{
			get
			{
				return this.m_cSlot;
			}
		}

		// Token: 0x1700079B RID: 1947
		// (get) Token: 0x060026C6 RID: 9926 RVA: 0x00095BED File Offset: 0x00093DED
		internal MemberProjectedSlot SSlot
		{
			get
			{
				return this.m_sSlot;
			}
		}

		// Token: 0x060026C7 RID: 9927 RVA: 0x00095BF8 File Offset: 0x00093DF8
		protected override bool IsEqualTo(ProjectedSlot right)
		{
			ViewCellSlot viewCellSlot = right as ViewCellSlot;
			return viewCellSlot != null && (this.m_slotNum == viewCellSlot.m_slotNum && ProjectedSlot.EqualityComparer.Equals(this.m_cSlot, viewCellSlot.m_cSlot)) && ProjectedSlot.EqualityComparer.Equals(this.m_sSlot, viewCellSlot.m_sSlot);
		}

		// Token: 0x060026C8 RID: 9928 RVA: 0x00095C4F File Offset: 0x00093E4F
		protected override int GetHash()
		{
			return ProjectedSlot.EqualityComparer.GetHashCode(this.m_cSlot) ^ ProjectedSlot.EqualityComparer.GetHashCode(this.m_sSlot) ^ this.m_slotNum;
		}

		// Token: 0x060026C9 RID: 9929 RVA: 0x00095C7C File Offset: 0x00093E7C
		internal static string SlotsToUserString(IEnumerable<ViewCellSlot> slots, bool isFromCside)
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = true;
			foreach (ViewCellSlot slot in slots)
			{
				if (!flag)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append(ViewCellSlot.SlotToUserString(slot, isFromCside));
				flag = false;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060026CA RID: 9930 RVA: 0x00095CEC File Offset: 0x00093EEC
		internal static string SlotToUserString(ViewCellSlot slot, bool isFromCside)
		{
			MemberProjectedSlot memberProjectedSlot = isFromCside ? slot.CSlot : slot.SSlot;
			return StringUtil.FormatInvariant("{0}", new object[]
			{
				memberProjectedSlot
			});
		}

		// Token: 0x060026CB RID: 9931 RVA: 0x00006174 File Offset: 0x00004374
		internal override string GetCqlFieldAlias(MemberPath outputMember)
		{
			return null;
		}

		// Token: 0x060026CC RID: 9932 RVA: 0x00006174 File Offset: 0x00004374
		internal override StringBuilder AsEsql(StringBuilder builder, MemberPath outputMember, string blockAlias, int indentLevel)
		{
			return null;
		}

		// Token: 0x060026CD RID: 9933 RVA: 0x00006174 File Offset: 0x00004374
		internal override DbExpression AsCqt(DbExpression row, MemberPath outputMember)
		{
			return null;
		}

		// Token: 0x060026CE RID: 9934 RVA: 0x00095D24 File Offset: 0x00093F24
		internal override void ToCompactString(StringBuilder builder)
		{
			builder.Append('<');
			StringUtil.FormatStringBuilder(builder, "{0}", new object[]
			{
				this.m_slotNum
			});
			builder.Append(':');
			this.m_cSlot.ToCompactString(builder);
			builder.Append('-');
			this.m_sSlot.ToCompactString(builder);
			builder.Append('>');
		}

		// Token: 0x040011E5 RID: 4581
		private readonly int m_slotNum;

		// Token: 0x040011E6 RID: 4582
		private readonly MemberProjectedSlot m_cSlot;

		// Token: 0x040011E7 RID: 4583
		private readonly MemberProjectedSlot m_sSlot;
	}
}
