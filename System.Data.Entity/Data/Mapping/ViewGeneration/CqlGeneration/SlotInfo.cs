using System;
using System.Data.Common.CommandTrees;
using System.Data.Common.CommandTrees.ExpressionBuilder;
using System.Data.Common.Utils;
using System.Data.Mapping.ViewGeneration.Structures;
using System.Text;

namespace System.Data.Mapping.ViewGeneration.CqlGeneration
{
	// Token: 0x02000279 RID: 633
	internal sealed class SlotInfo : InternalBase
	{
		// Token: 0x06002659 RID: 9817 RVA: 0x00092514 File Offset: 0x00090714
		internal SlotInfo(bool isRequiredByParent, bool isProjected, ProjectedSlot slotValue, MemberPath outputMember) : this(isRequiredByParent, isProjected, slotValue, outputMember, false)
		{
		}

		// Token: 0x0600265A RID: 9818 RVA: 0x00092522 File Offset: 0x00090722
		internal SlotInfo(bool isRequiredByParent, bool isProjected, ProjectedSlot slotValue, MemberPath outputMember, bool enforceNotNull)
		{
			this.m_isRequiredByParent = isRequiredByParent;
			this.m_isProjected = isProjected;
			this.m_slotValue = slotValue;
			this.m_outputMember = outputMember;
			this.m_enforceNotNull = enforceNotNull;
		}

		// Token: 0x1700078A RID: 1930
		// (get) Token: 0x0600265B RID: 9819 RVA: 0x0009254F File Offset: 0x0009074F
		internal bool IsRequiredByParent
		{
			get
			{
				return this.m_isRequiredByParent;
			}
		}

		// Token: 0x1700078B RID: 1931
		// (get) Token: 0x0600265C RID: 9820 RVA: 0x00092557 File Offset: 0x00090757
		internal bool IsProjected
		{
			get
			{
				return this.m_isProjected;
			}
		}

		// Token: 0x1700078C RID: 1932
		// (get) Token: 0x0600265D RID: 9821 RVA: 0x0009255F File Offset: 0x0009075F
		internal MemberPath OutputMember
		{
			get
			{
				return this.m_outputMember;
			}
		}

		// Token: 0x1700078D RID: 1933
		// (get) Token: 0x0600265E RID: 9822 RVA: 0x00092567 File Offset: 0x00090767
		internal ProjectedSlot SlotValue
		{
			get
			{
				return this.m_slotValue;
			}
		}

		// Token: 0x1700078E RID: 1934
		// (get) Token: 0x0600265F RID: 9823 RVA: 0x0009256F File Offset: 0x0009076F
		internal string CqlFieldAlias
		{
			get
			{
				if (this.m_slotValue == null)
				{
					return null;
				}
				return this.m_slotValue.GetCqlFieldAlias(this.m_outputMember);
			}
		}

		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x06002660 RID: 9824 RVA: 0x0009258C File Offset: 0x0009078C
		internal bool IsEnforcedNotNull
		{
			get
			{
				return this.m_enforceNotNull;
			}
		}

		// Token: 0x06002661 RID: 9825 RVA: 0x00092594 File Offset: 0x00090794
		internal void ResetIsRequiredByParent()
		{
			this.m_isRequiredByParent = false;
		}

		// Token: 0x06002662 RID: 9826 RVA: 0x000925A0 File Offset: 0x000907A0
		internal StringBuilder AsEsql(StringBuilder builder, string blockAlias, int indentLevel)
		{
			if (this.m_enforceNotNull)
			{
				builder.Append('(');
				this.m_slotValue.AsEsql(builder, this.m_outputMember, blockAlias, indentLevel);
				builder.Append(" AND ");
				this.m_slotValue.AsEsql(builder, this.m_outputMember, blockAlias, indentLevel);
				builder.Append(" IS NOT NULL)");
			}
			else
			{
				this.m_slotValue.AsEsql(builder, this.m_outputMember, blockAlias, indentLevel);
			}
			return builder;
		}

		// Token: 0x06002663 RID: 9827 RVA: 0x00092618 File Offset: 0x00090818
		internal DbExpression AsCqt(DbExpression row)
		{
			DbExpression dbExpression = this.m_slotValue.AsCqt(row, this.m_outputMember);
			if (this.m_enforceNotNull)
			{
				dbExpression = dbExpression.And(dbExpression.IsNull().Not());
			}
			return dbExpression;
		}

		// Token: 0x06002664 RID: 9828 RVA: 0x00092653 File Offset: 0x00090853
		internal override void ToCompactString(StringBuilder builder)
		{
			if (this.m_slotValue != null)
			{
				builder.Append(this.CqlFieldAlias);
			}
		}

		// Token: 0x040011C7 RID: 4551
		private bool m_isRequiredByParent;

		// Token: 0x040011C8 RID: 4552
		private readonly bool m_isProjected;

		// Token: 0x040011C9 RID: 4553
		private readonly ProjectedSlot m_slotValue;

		// Token: 0x040011CA RID: 4554
		private readonly MemberPath m_outputMember;

		// Token: 0x040011CB RID: 4555
		private readonly bool m_enforceNotNull;
	}
}
