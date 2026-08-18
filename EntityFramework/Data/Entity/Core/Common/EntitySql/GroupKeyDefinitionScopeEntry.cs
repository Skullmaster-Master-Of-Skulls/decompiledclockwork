using System;
using System.Data.Entity.Core.Common.CommandTrees;

namespace System.Data.Entity.Core.Common.EntitySql
{
	// Token: 0x02000257 RID: 599
	internal sealed class GroupKeyDefinitionScopeEntry : ScopeEntry, IGroupExpressionExtendedInfo, IGetAlternativeName
	{
		// Token: 0x060014D7 RID: 5335 RVA: 0x00062EFC File Offset: 0x000610FC
		internal GroupKeyDefinitionScopeEntry(DbExpression varBasedExpression, DbExpression groupVarBasedExpression, DbExpression groupAggBasedExpression, string[] alternativeName) : base(ScopeEntryKind.GroupKeyDefinition)
		{
			this._varBasedExpression = varBasedExpression;
			this._groupVarBasedExpression = groupVarBasedExpression;
			this._groupAggBasedExpression = groupAggBasedExpression;
			this._alternativeName = alternativeName;
		}

		// Token: 0x060014D8 RID: 5336 RVA: 0x00062F22 File Offset: 0x00061122
		internal override DbExpression GetExpression(string refName, ErrorContext errCtx)
		{
			return this._varBasedExpression;
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x060014D9 RID: 5337 RVA: 0x00062F2A File Offset: 0x0006112A
		DbExpression IGroupExpressionExtendedInfo.GroupVarBasedExpression
		{
			get
			{
				return this._groupVarBasedExpression;
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x060014DA RID: 5338 RVA: 0x00062F32 File Offset: 0x00061132
		DbExpression IGroupExpressionExtendedInfo.GroupAggBasedExpression
		{
			get
			{
				return this._groupAggBasedExpression;
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x060014DB RID: 5339 RVA: 0x00062F3A File Offset: 0x0006113A
		string[] IGetAlternativeName.AlternativeName
		{
			get
			{
				return this._alternativeName;
			}
		}

		// Token: 0x0400072F RID: 1839
		private readonly DbExpression _varBasedExpression;

		// Token: 0x04000730 RID: 1840
		private readonly DbExpression _groupVarBasedExpression;

		// Token: 0x04000731 RID: 1841
		private readonly DbExpression _groupAggBasedExpression;

		// Token: 0x04000732 RID: 1842
		private readonly string[] _alternativeName;
	}
}
