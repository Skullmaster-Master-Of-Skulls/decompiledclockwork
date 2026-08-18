using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;

namespace System.Data.Entity.Core.Common.EntitySql
{
	// Token: 0x02000271 RID: 625
	internal sealed class SourceScopeEntry : ScopeEntry, IGroupExpressionExtendedInfo, IGetAlternativeName
	{
		// Token: 0x060015FA RID: 5626 RVA: 0x0006AEB2 File Offset: 0x000690B2
		internal SourceScopeEntry(DbVariableReferenceExpression varRef) : this(varRef, null)
		{
		}

		// Token: 0x060015FB RID: 5627 RVA: 0x0006AEBC File Offset: 0x000690BC
		internal SourceScopeEntry(DbVariableReferenceExpression varRef, string[] alternativeName) : base(ScopeEntryKind.SourceVar)
		{
			this._varBasedExpression = varRef;
			this._alternativeName = alternativeName;
		}

		// Token: 0x060015FC RID: 5628 RVA: 0x0006AED3 File Offset: 0x000690D3
		internal override DbExpression GetExpression(string refName, ErrorContext errCtx)
		{
			return this._varBasedExpression;
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x060015FD RID: 5629 RVA: 0x0006AEDB File Offset: 0x000690DB
		DbExpression IGroupExpressionExtendedInfo.GroupVarBasedExpression
		{
			get
			{
				return this._groupVarBasedExpression;
			}
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x060015FE RID: 5630 RVA: 0x0006AEE3 File Offset: 0x000690E3
		DbExpression IGroupExpressionExtendedInfo.GroupAggBasedExpression
		{
			get
			{
				return this._groupAggBasedExpression;
			}
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x060015FF RID: 5631 RVA: 0x0006AEEB File Offset: 0x000690EB
		// (set) Token: 0x06001600 RID: 5632 RVA: 0x0006AEF3 File Offset: 0x000690F3
		internal bool IsJoinClauseLeftExpr { get; set; }

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06001601 RID: 5633 RVA: 0x0006AEFC File Offset: 0x000690FC
		string[] IGetAlternativeName.AlternativeName
		{
			get
			{
				return this._alternativeName;
			}
		}

		// Token: 0x06001602 RID: 5634 RVA: 0x0006AF04 File Offset: 0x00069104
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

		// Token: 0x06001603 RID: 5635 RVA: 0x0006AF8E File Offset: 0x0006918E
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

		// Token: 0x06001604 RID: 5636 RVA: 0x0006AFC0 File Offset: 0x000691C0
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

		// Token: 0x06001605 RID: 5637 RVA: 0x0006B03C File Offset: 0x0006923C
		internal void RollbackAdjustmentToGroupVar(DbVariableReferenceExpression pregroupParentVarRef)
		{
			this._groupVarBasedExpression = null;
			this._groupAggBasedExpression = null;
			this.ReplaceParentVar(pregroupParentVarRef);
		}

		// Token: 0x040007B3 RID: 1971
		private readonly string[] _alternativeName;

		// Token: 0x040007B4 RID: 1972
		private List<string> _propRefs;

		// Token: 0x040007B5 RID: 1973
		private DbExpression _varBasedExpression;

		// Token: 0x040007B6 RID: 1974
		private DbExpression _groupVarBasedExpression;

		// Token: 0x040007B7 RID: 1975
		private DbExpression _groupAggBasedExpression;
	}
}
