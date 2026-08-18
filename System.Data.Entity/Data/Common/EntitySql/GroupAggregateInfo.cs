using System;
using System.Collections.Generic;
using System.Data.Common.CommandTrees;
using System.Data.Common.CommandTrees.ExpressionBuilder;
using System.Data.Common.EntitySql.AST;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.Linq;

namespace System.Data.Common.EntitySql
{
	// Token: 0x0200033C RID: 828
	internal abstract class GroupAggregateInfo
	{
		// Token: 0x06003137 RID: 12599 RVA: 0x000C23AF File Offset: 0x000C05AF
		protected GroupAggregateInfo(GroupAggregateKind aggregateKind, GroupAggregateExpr astNode, ErrorContext errCtx, GroupAggregateInfo containingAggregate, ScopeRegion definingScopeRegion)
		{
			this.AggregateKind = aggregateKind;
			this.AstNode = astNode;
			this.ErrCtx = errCtx;
			this.DefiningScopeRegion = definingScopeRegion;
			this.SetContainingAggregate(containingAggregate);
		}

		// Token: 0x06003138 RID: 12600 RVA: 0x000C23DC File Offset: 0x000C05DC
		protected void AttachToAstNode(string aggregateName, TypeUsage resultType)
		{
			this.AggregateName = aggregateName;
			this.AggregateStubExpression = resultType.Null();
			this.AstNode.AggregateInfo = this;
		}

		// Token: 0x06003139 RID: 12601 RVA: 0x000C23FD File Offset: 0x000C05FD
		internal void DetachFromAstNode()
		{
			this.AstNode.AggregateInfo = null;
		}

		// Token: 0x0600313A RID: 12602 RVA: 0x000C240C File Offset: 0x000C060C
		internal void UpdateScopeIndex(int referencedScopeIndex, SemanticResolver sr)
		{
			ScopeRegion definingScopeRegion = sr.GetDefiningScopeRegion(referencedScopeIndex);
			if (this._innermostReferencedScopeRegion == null || this._innermostReferencedScopeRegion.ScopeRegionIndex < definingScopeRegion.ScopeRegionIndex)
			{
				this._innermostReferencedScopeRegion = definingScopeRegion;
			}
		}

		// Token: 0x1700096C RID: 2412
		// (get) Token: 0x0600313B RID: 12603 RVA: 0x000C2443 File Offset: 0x000C0643
		// (set) Token: 0x0600313C RID: 12604 RVA: 0x000C244B File Offset: 0x000C064B
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

		// Token: 0x0600313D RID: 12605 RVA: 0x000C2454 File Offset: 0x000C0654
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
					throw EntityUtil.EntitySqlError(Strings.GroupVarNotFoundInScope);
				}
			}
			this.ValidateContainedAggregates(this._evaluatingScopeRegion.ScopeRegionIndex, this.DefiningScopeRegion.ScopeRegionIndex);
		}

		// Token: 0x0600313E RID: 12606 RVA: 0x000C2518 File Offset: 0x000C0718
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
						throw EntityUtil.EntitySqlError(Strings.NestedAggregateCannotBeUsedInAggregate(p2, p));
					}
					groupAggregateInfo.ValidateContainedAggregates(outerBoundaryScopeRegionIndex, innerBoundaryScopeRegionIndex);
				}
			}
		}

		// Token: 0x0600313F RID: 12607 RVA: 0x000C261C File Offset: 0x000C081C
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

		// Token: 0x06003140 RID: 12608 RVA: 0x000C264D File Offset: 0x000C084D
		private void AddContainedAggregate(GroupAggregateInfo containedAggregate)
		{
			if (this._containedAggregates == null)
			{
				this._containedAggregates = new List<GroupAggregateInfo>();
			}
			this._containedAggregates.Add(containedAggregate);
		}

		// Token: 0x06003141 RID: 12609 RVA: 0x000C266E File Offset: 0x000C086E
		private void RemoveContainedAggregate(GroupAggregateInfo containedAggregate)
		{
			this._containedAggregates.Remove(containedAggregate);
		}

		// Token: 0x1700096D RID: 2413
		// (get) Token: 0x06003142 RID: 12610 RVA: 0x000C267D File Offset: 0x000C087D
		internal ScopeRegion EvaluatingScopeRegion
		{
			get
			{
				return this._evaluatingScopeRegion;
			}
		}

		// Token: 0x1700096E RID: 2414
		// (get) Token: 0x06003143 RID: 12611 RVA: 0x000C2685 File Offset: 0x000C0885
		internal GroupAggregateInfo ContainingAggregate
		{
			get
			{
				return this._containingAggregate;
			}
		}

		// Token: 0x0400155B RID: 5467
		private ScopeRegion _innermostReferencedScopeRegion;

		// Token: 0x0400155C RID: 5468
		private List<GroupAggregateInfo> _containedAggregates;

		// Token: 0x0400155D RID: 5469
		internal readonly GroupAggregateKind AggregateKind;

		// Token: 0x0400155E RID: 5470
		internal readonly GroupAggregateExpr AstNode;

		// Token: 0x0400155F RID: 5471
		internal readonly ErrorContext ErrCtx;

		// Token: 0x04001560 RID: 5472
		internal readonly ScopeRegion DefiningScopeRegion;

		// Token: 0x04001561 RID: 5473
		private ScopeRegion _evaluatingScopeRegion;

		// Token: 0x04001562 RID: 5474
		private GroupAggregateInfo _containingAggregate;

		// Token: 0x04001563 RID: 5475
		internal string AggregateName;

		// Token: 0x04001564 RID: 5476
		internal DbNullExpression AggregateStubExpression;
	}
}
