using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Common.EntitySql.AST;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Linq;

namespace System.Data.Entity.Core.Common.EntitySql
{
	// Token: 0x0200024E RID: 590
	internal abstract class GroupAggregateInfo
	{
		// Token: 0x060014AD RID: 5293 RVA: 0x0006267E File Offset: 0x0006087E
		protected GroupAggregateInfo(GroupAggregateKind aggregateKind, GroupAggregateExpr astNode, ErrorContext errCtx, GroupAggregateInfo containingAggregate, ScopeRegion definingScopeRegion)
		{
			this.AggregateKind = aggregateKind;
			this.AstNode = astNode;
			this.ErrCtx = errCtx;
			this.DefiningScopeRegion = definingScopeRegion;
			this.SetContainingAggregate(containingAggregate);
		}

		// Token: 0x060014AE RID: 5294 RVA: 0x000626AB File Offset: 0x000608AB
		protected void AttachToAstNode(string aggregateName, TypeUsage resultType)
		{
			this.AggregateName = aggregateName;
			this.AggregateStubExpression = resultType.Null();
			this.AstNode.AggregateInfo = this;
		}

		// Token: 0x060014AF RID: 5295 RVA: 0x000626CC File Offset: 0x000608CC
		internal void DetachFromAstNode()
		{
			this.AstNode.AggregateInfo = null;
		}

		// Token: 0x060014B0 RID: 5296 RVA: 0x000626DC File Offset: 0x000608DC
		internal void UpdateScopeIndex(int referencedScopeIndex, SemanticResolver sr)
		{
			ScopeRegion definingScopeRegion = sr.GetDefiningScopeRegion(referencedScopeIndex);
			if (this._innermostReferencedScopeRegion == null || this._innermostReferencedScopeRegion.ScopeRegionIndex < definingScopeRegion.ScopeRegionIndex)
			{
				this._innermostReferencedScopeRegion = definingScopeRegion;
			}
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x060014B1 RID: 5297 RVA: 0x00062713 File Offset: 0x00060913
		// (set) Token: 0x060014B2 RID: 5298 RVA: 0x0006271B File Offset: 0x0006091B
		internal ScopeRegion InnermostReferencedScopeRegion
		{
			get
			{
				return this._innermostReferencedScopeRegion;
			}
			set
			{
				this._innermostReferencedScopeRegion = value;
			}
		}

		// Token: 0x060014B3 RID: 5299 RVA: 0x00062724 File Offset: 0x00060924
		internal void ValidateAndComputeEvaluatingScopeRegion(SemanticResolver sr)
		{
			this._evaluatingScopeRegion = (this._innermostReferencedScopeRegion ?? this.DefiningScopeRegion);
			if (!this._evaluatingScopeRegion.IsAggregating)
			{
				int scopeRegionIndex = this._evaluatingScopeRegion.ScopeRegionIndex;
				this._evaluatingScopeRegion = null;
				foreach (ScopeRegion scopeRegion in sr.ScopeRegions.Skip(scopeRegionIndex))
				{
					if (scopeRegion.IsAggregating)
					{
						this._evaluatingScopeRegion = scopeRegion;
						break;
					}
				}
				if (this._evaluatingScopeRegion == null)
				{
					string groupVarNotFoundInScope = Strings.GroupVarNotFoundInScope;
					throw new EntitySqlException(groupVarNotFoundInScope);
				}
			}
			this.ValidateContainedAggregates(this._evaluatingScopeRegion.ScopeRegionIndex, this.DefiningScopeRegion.ScopeRegionIndex);
		}

		// Token: 0x060014B4 RID: 5300 RVA: 0x000627E8 File Offset: 0x000609E8
		private void ValidateContainedAggregates(int outerBoundaryScopeRegionIndex, int innerBoundaryScopeRegionIndex)
		{
			if (this._containedAggregates != null)
			{
				foreach (GroupAggregateInfo groupAggregateInfo in this._containedAggregates)
				{
					if (groupAggregateInfo.EvaluatingScopeRegion.ScopeRegionIndex >= outerBoundaryScopeRegionIndex && groupAggregateInfo.EvaluatingScopeRegion.ScopeRegionIndex <= innerBoundaryScopeRegionIndex)
					{
						int num;
						int num2;
						string p = EntitySqlException.FormatErrorContext(this.ErrCtx.CommandText, this.ErrCtx.InputPosition, this.ErrCtx.ErrorContextInfo, this.ErrCtx.UseContextInfoAsResourceIdentifier, out num, out num2);
						string p2 = EntitySqlException.FormatErrorContext(groupAggregateInfo.ErrCtx.CommandText, groupAggregateInfo.ErrCtx.InputPosition, groupAggregateInfo.ErrCtx.ErrorContextInfo, groupAggregateInfo.ErrCtx.UseContextInfoAsResourceIdentifier, out num, out num2);
						string message = Strings.NestedAggregateCannotBeUsedInAggregate(p2, p);
						throw new EntitySqlException(message);
					}
					groupAggregateInfo.ValidateContainedAggregates(outerBoundaryScopeRegionIndex, innerBoundaryScopeRegionIndex);
				}
			}
		}

		// Token: 0x060014B5 RID: 5301 RVA: 0x000628EC File Offset: 0x00060AEC
		internal void SetContainingAggregate(GroupAggregateInfo containingAggregate)
		{
			if (this._containingAggregate != null)
			{
				this._containingAggregate.RemoveContainedAggregate(this);
			}
			this._containingAggregate = containingAggregate;
			if (this._containingAggregate != null)
			{
				this._containingAggregate.AddContainedAggregate(this);
			}
		}

		// Token: 0x060014B6 RID: 5302 RVA: 0x0006291D File Offset: 0x00060B1D
		private void AddContainedAggregate(GroupAggregateInfo containedAggregate)
		{
			if (this._containedAggregates == null)
			{
				this._containedAggregates = new List<GroupAggregateInfo>();
			}
			this._containedAggregates.Add(containedAggregate);
		}

		// Token: 0x060014B7 RID: 5303 RVA: 0x0006293E File Offset: 0x00060B3E
		private void RemoveContainedAggregate(GroupAggregateInfo containedAggregate)
		{
			this._containedAggregates.Remove(containedAggregate);
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x060014B8 RID: 5304 RVA: 0x0006294D File Offset: 0x00060B4D
		internal ScopeRegion EvaluatingScopeRegion
		{
			get
			{
				return this._evaluatingScopeRegion;
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x060014B9 RID: 5305 RVA: 0x00062955 File Offset: 0x00060B55
		internal GroupAggregateInfo ContainingAggregate
		{
			get
			{
				return this._containingAggregate;
			}
		}

		// Token: 0x04000710 RID: 1808
		private ScopeRegion _innermostReferencedScopeRegion;

		// Token: 0x04000711 RID: 1809
		private List<GroupAggregateInfo> _containedAggregates;

		// Token: 0x04000712 RID: 1810
		internal readonly GroupAggregateKind AggregateKind;

		// Token: 0x04000713 RID: 1811
		internal readonly GroupAggregateExpr AstNode;

		// Token: 0x04000714 RID: 1812
		internal readonly ErrorContext ErrCtx;

		// Token: 0x04000715 RID: 1813
		internal readonly ScopeRegion DefiningScopeRegion;

		// Token: 0x04000716 RID: 1814
		private ScopeRegion _evaluatingScopeRegion;

		// Token: 0x04000717 RID: 1815
		private GroupAggregateInfo _containingAggregate;

		// Token: 0x04000718 RID: 1816
		internal string AggregateName;

		// Token: 0x04000719 RID: 1817
		internal DbNullExpression AggregateStubExpression;
	}
}
