using System;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.Structures;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.CqlGeneration
{
	// Token: 0x0200042F RID: 1071
	internal sealed class SlotInfo : InternalBase
	{
		// Token: 0x06002757 RID: 10071 RVA: 0x000BEB06 File Offset: 0x000BCD06
		internal SlotInfo(bool isRequiredByParent, bool isProjected, ProjectedSlot slotValue, MemberPath outputMember) : this(isRequiredByParent, isProjected, slotValue, outputMember, false)
		{
		}

		// Token: 0x06002758 RID: 10072 RVA: 0x000BEB14 File Offset: 0x000BCD14
		internal SlotInfo(bool isRequiredByParent, bool isProjected, ProjectedSlot slotValue, MemberPath outputMember, bool enforceNotNull)
		{
			this.m_isRequiredByParent = isRequiredByParent;
			this.m_isProjected = isProjected;
			this.m_slotValue = slotValue;
			this.m_outputMember = outputMember;
			this.m_enforceNotNull = enforceNotNull;
		}

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x06002759 RID: 10073 RVA: 0x000BEB41 File Offset: 0x000BCD41
		internal bool IsRequiredByParent
		{
			get
			{
				return this.m_isRequiredByParent;
			}
		}

		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x0600275A RID: 10074 RVA: 0x000BEB49 File Offset: 0x000BCD49
		internal bool IsProjected
		{
			get
			{
				return this.m_isProjected;
			}
		}

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x0600275B RID: 10075 RVA: 0x000BEB51 File Offset: 0x000BCD51
		internal MemberPath OutputMember
		{
			get
			{
				return this.m_outputMember;
			}
		}

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x0600275C RID: 10076 RVA: 0x000BEB59 File Offset: 0x000BCD59
		internal ProjectedSlot SlotValue
		{
			get
			{
				return this.m_slotValue;
			}
		}

		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x0600275D RID: 10077 RVA: 0x000BEB61 File Offset: 0x000BCD61
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

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x0600275E RID: 10078 RVA: 0x000BEB7E File Offset: 0x000BCD7E
		internal bool IsEnforcedNotNull
		{
			get
			{
				return this.m_enforceNotNull;
			}
		}

		// Token: 0x0600275F RID: 10079 RVA: 0x000BEB86 File Offset: 0x000BCD86
		internal void ResetIsRequiredByParent()
		{
			this.m_isRequiredByParent = false;
		}

		// Token: 0x06002760 RID: 10080 RVA: 0x000BEB90 File Offset: 0x000BCD90
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

		// Token: 0x06002761 RID: 10081 RVA: 0x000BEC08 File Offset: 0x000BCE08
		internal DbExpression AsCqt(DbExpression row)
		{
			DbExpression dbExpression = this.m_slotValue.AsCqt(row, this.m_outputMember);
			if (this.m_enforceNotNull)
			{
				dbExpression = dbExpression.And(dbExpression.IsNull().Not());
			}
			return dbExpression;
		}

		// Token: 0x06002762 RID: 10082 RVA: 0x000BEC43 File Offset: 0x000BCE43
		internal override void ToCompactString(StringBuilder builder)
		{
			if (this.m_slotValue != null)
			{
				builder.Append(this.CqlFieldAlias);
			}
		}

		// Token: 0x04000EC8 RID: 3784
		private bool m_isRequiredByParent;

		// Token: 0x04000EC9 RID: 3785
		private readonly bool m_isProjected;

		// Token: 0x04000ECA RID: 3786
		private readonly ProjectedSlot m_slotValue;

		// Token: 0x04000ECB RID: 3787
		private readonly MemberPath m_outputMember;

		// Token: 0x04000ECC RID: 3788
		private readonly bool m_enforceNotNull;
	}
}
