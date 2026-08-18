using System;
using System.Collections.Generic;
using System.Data.Common.CommandTrees;
using System.Data.Common.CommandTrees.ExpressionBuilder;

namespace System.Data.Common.EntitySql
{
	// Token: 0x02000348 RID: 840
	internal sealed class SourceScopeEntry : ScopeEntry, IGroupExpressionExtendedInfo, IGetAlternativeName
	{
		// Token: 0x0600316C RID: 12652 RVA: 0x000C29CB File Offset: 0x000C0BCB
		internal SourceScopeEntry(DbVariableReferenceExpression varRef) : this(varRef, null)
		{
		}

		// Token: 0x0600316D RID: 12653 RVA: 0x000C29D5 File Offset: 0x000C0BD5
		internal SourceScopeEntry(DbVariableReferenceExpression varRef, string[] alternativeName) : base(ScopeEntryKind.SourceVar)
		{
			this._varBasedExpression = varRef;
			this._alternativeName = alternativeName;
		}

		// Token: 0x0600316E RID: 12654 RVA: 0x000C29EC File Offset: 0x000C0BEC
		internal override DbExpression GetExpression(string refName, ErrorContext errCtx)
		{
			return this._varBasedExpression;
		}

		// Token: 0x17000979 RID: 2425
		// (get) Token: 0x0600316F RID: 12655 RVA: 0x000C29F4 File Offset: 0x000C0BF4
		DbExpression IGroupExpressionExtendedInfo.GroupVarBasedExpression
		{
			get
			{
				return this._groupVarBasedExpression;
			}
		}

		// Token: 0x1700097A RID: 2426
		// (get) Token: 0x06003170 RID: 12656 RVA: 0x000C29FC File Offset: 0x000C0BFC
		DbExpression IGroupExpressionExtendedInfo.GroupAggBasedExpression
		{
			get
			{
				return this._groupAggBasedExpression;
			}
		}

		// Token: 0x1700097B RID: 2427
		// (get) Token: 0x06003171 RID: 12657 RVA: 0x000C2A04 File Offset: 0x000C0C04
		// (set) Token: 0x06003172 RID: 12658 RVA: 0x000C2A0C File Offset: 0x000C0C0C
		internal bool IsJoinClauseLeftExpr
		{
			get
			{
				return this._joinClauseLeftExpr;
			}
			set
			{
				this._joinClauseLeftExpr = value;
			}
		}

		// Token: 0x1700097C RID: 2428
		// (get) Token: 0x06003173 RID: 12659 RVA: 0x000C2A15 File Offset: 0x000C0C15
		string[] IGetAlternativeName.AlternativeName
		{
			get
			{
				return this._alternativeName;
			}
		}

		// Token: 0x06003174 RID: 12660 RVA: 0x000C2A20 File Offset: 0x000C0C20
		internal SourceScopeEntry AddParentVar(DbVariableReferenceExpression parentVarRef)
		{
			if (this._propRefs == null)
			{
				this._propRefs = new List<string>(2);
				this._propRefs.Add(((DbVariableReferenceExpression)this._varBasedExpression).VariableName);
			}
			this._varBasedExpression = parentVarRef;
			for (int i = this._propRefs.Count - 1; i >= 0; i--)
			{
				this._varBasedExpression = this._varBasedExpression.Property(this._propRefs[i]);
			}
			this._propRefs.Add(parentVarRef.VariableName);
			return this;
		}

		// Token: 0x06003175 RID: 12661 RVA: 0x000C2AAA File Offset: 0x000C0CAA
		internal void ReplaceParentVar(DbVariableReferenceExpression parentVarRef)
		{
			if (this._propRefs == null)
			{
				this._varBasedExpression = parentVarRef;
				return;
			}
			this._propRefs.RemoveAt(this._propRefs.Count - 1);
			this.AddParentVar(parentVarRef);
		}

		// Token: 0x06003176 RID: 12662 RVA: 0x000C2ADC File Offset: 0x000C0CDC
		internal void AdjustToGroupVar(DbVariableReferenceExpression parentVarRef, DbVariableReferenceExpression parentGroupVarRef, DbVariableReferenceExpression groupAggRef)
		{
			this.ReplaceParentVar(parentVarRef);
			this._groupVarBasedExpression = parentGroupVarRef;
			this._groupAggBasedExpression = groupAggRef;
			if (this._propRefs != null)
			{
				for (int i = this._propRefs.Count - 2; i >= 0; i--)
				{
					this._groupVarBasedExpression = this._groupVarBasedExpression.Property(this._propRefs[i]);
					this._groupAggBasedExpression = this._groupAggBasedExpression.Property(this._propRefs[i]);
				}
			}
		}

		// Token: 0x06003177 RID: 12663 RVA: 0x000C2B58 File Offset: 0x000C0D58
		internal void RollbackAdjustmentToGroupVar(DbVariableReferenceExpression pregroupParentVarRef)
		{
			this._groupVarBasedExpression = null;
			this._groupAggBasedExpression = null;
			this.ReplaceParentVar(pregroupParentVarRef);
		}

		// Token: 0x0400157A RID: 5498
		private readonly string[] _alternativeName;

		// Token: 0x0400157B RID: 5499
		private List<string> _propRefs;

		// Token: 0x0400157C RID: 5500
		private DbExpression _varBasedExpression;

		// Token: 0x0400157D RID: 5501
		private DbExpression _groupVarBasedExpression;

		// Token: 0x0400157E RID: 5502
		private DbExpression _groupAggBasedExpression;

		// Token: 0x0400157F RID: 5503
		private bool _joinClauseLeftExpr;
	}
}
