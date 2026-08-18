using System;
using System.Data.Common.CommandTrees;

namespace System.Data.Common.EntitySql
{
	// Token: 0x0200034A RID: 842
	internal sealed class GroupKeyDefinitionScopeEntry : ScopeEntry, IGroupExpressionExtendedInfo, IGetAlternativeName
	{
		// Token: 0x0600317A RID: 12666 RVA: 0x000C2B86 File Offset: 0x000C0D86
		internal GroupKeyDefinitionScopeEntry(DbExpression varBasedExpression, DbExpression groupVarBasedExpression, DbExpression groupAggBasedExpression, string[] alternativeName) : base(ScopeEntryKind.GroupKeyDefinition)
		{
			this._varBasedExpression = varBasedExpression;
			this._groupVarBasedExpression = groupVarBasedExpression;
			this._groupAggBasedExpression = groupAggBasedExpression;
			this._alternativeName = alternativeName;
		}

		// Token: 0x0600317B RID: 12667 RVA: 0x000C2BAC File Offset: 0x000C0DAC
		internal override DbExpression GetExpression(string refName, ErrorContext errCtx)
		{
			return this._varBasedExpression;
		}

		// Token: 0x1700097D RID: 2429
		// (get) Token: 0x0600317C RID: 12668 RVA: 0x000C2BB4 File Offset: 0x000C0DB4
		DbExpression IGroupExpressionExtendedInfo.GroupVarBasedExpression
		{
			get
			{
				return this._groupVarBasedExpression;
			}
		}

		// Token: 0x1700097E RID: 2430
		// (get) Token: 0x0600317D RID: 12669 RVA: 0x000C2BBC File Offset: 0x000C0DBC
		DbExpression IGroupExpressionExtendedInfo.GroupAggBasedExpression
		{
			get
			{
				return this._groupAggBasedExpression;
			}
		}

		// Token: 0x1700097F RID: 2431
		// (get) Token: 0x0600317E RID: 12670 RVA: 0x000C2BC4 File Offset: 0x000C0DC4
		string[] IGetAlternativeName.AlternativeName
		{
			get
			{
				return this._alternativeName;
			}
		}

		// Token: 0x04001580 RID: 5504
		private readonly DbExpression _varBasedExpression;

		// Token: 0x04001581 RID: 5505
		private readonly DbExpression _groupVarBasedExpression;

		// Token: 0x04001582 RID: 5506
		private readonly DbExpression _groupAggBasedExpression;

		// Token: 0x04001583 RID: 5507
		private readonly string[] _alternativeName;
	}
}
