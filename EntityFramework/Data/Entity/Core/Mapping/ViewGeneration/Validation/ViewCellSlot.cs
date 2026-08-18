using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.Structures;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Validation
{
	// Token: 0x0200049B RID: 1179
	internal class ViewCellSlot : ProjectedSlot
	{
		// Token: 0x06002B7E RID: 11134 RVA: 0x000D36D8 File Offset: 0x000D18D8
		internal ViewCellSlot(int slotNum, MemberProjectedSlot cSlot, MemberProjectedSlot sSlot)
		{
			this.m_slotNum = slotNum;
			this.m_cSlot = cSlot;
			this.m_sSlot = sSlot;
		}

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x06002B7F RID: 11135 RVA: 0x000D36F5 File Offset: 0x000D18F5
		internal MemberProjectedSlot CSlot
		{
			get
			{
				return this.m_cSlot;
			}
		}

		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x06002B80 RID: 11136 RVA: 0x000D36FD File Offset: 0x000D18FD
		internal MemberProjectedSlot SSlot
		{
			get
			{
				return this.m_sSlot;
			}
		}

		// Token: 0x06002B81 RID: 11137 RVA: 0x000D3708 File Offset: 0x000D1908
		protected override bool IsEqualTo(ProjectedSlot right)
		{
			ViewCellSlot viewCellSlot = right as ViewCellSlot;
			return viewCellSlot != null && (this.m_slotNum == viewCellSlot.m_slotNum && ProjectedSlot.EqualityComparer.Equals(this.m_cSlot, viewCellSlot.m_cSlot)) && ProjectedSlot.EqualityComparer.Equals(this.m_sSlot, viewCellSlot.m_sSlot);
		}

		// Token: 0x06002B82 RID: 11138 RVA: 0x000D375F File Offset: 0x000D195F
		protected override int GetHash()
		{
			return ProjectedSlot.EqualityComparer.GetHashCode(this.m_cSlot) ^ ProjectedSlot.EqualityComparer.GetHashCode(this.m_sSlot) ^ this.m_slotNum;
		}

		// Token: 0x06002B83 RID: 11139 RVA: 0x000D378C File Offset: 0x000D198C
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

		// Token: 0x06002B84 RID: 11140 RVA: 0x000D37FC File Offset: 0x000D19FC
		internal static string SlotToUserString(ViewCellSlot slot, bool isFromCside)
		{
			MemberProjectedSlot memberProjectedSlot = isFromCside ? slot.CSlot : slot.SSlot;
			return StringUtil.FormatInvariant("{0}", new object[]
			{
				memberProjectedSlot
			});
		}

		// Token: 0x06002B85 RID: 11141 RVA: 0x000D3833 File Offset: 0x000D1A33
		internal override string GetCqlFieldAlias(MemberPath outputMember)
		{
			return null;
		}

		// Token: 0x06002B86 RID: 11142 RVA: 0x000D3836 File Offset: 0x000D1A36
		internal override StringBuilder AsEsql(StringBuilder builder, MemberPath outputMember, string blockAlias, int indentLevel)
		{
			return null;
		}

		// Token: 0x06002B87 RID: 11143 RVA: 0x000D3839 File Offset: 0x000D1A39
		internal override DbExpression AsCqt(DbExpression row, MemberPath outputMember)
		{
			return null;
		}

		// Token: 0x06002B88 RID: 11144 RVA: 0x000D383C File Offset: 0x000D1A3C
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

		// Token: 0x0400100E RID: 4110
		private readonly int m_slotNum;

		// Token: 0x0400100F RID: 4111
		private readonly MemberProjectedSlot m_cSlot;

		// Token: 0x04001010 RID: 4112
		private readonly MemberProjectedSlot m_sSlot;
	}
}
