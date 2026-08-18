using System;
using System.Collections.Generic;

namespace System.Data.Common.CommandTrees
{
	// Token: 0x02000424 RID: 1060
	internal abstract class BasicCommandTreeVisitor : BasicExpressionVisitor
	{
		// Token: 0x0600371F RID: 14111 RVA: 0x000D1A06 File Offset: 0x000CFC06
		protected virtual void VisitSetClause(DbSetClause setClause)
		{
			EntityUtil.CheckArgumentNull<DbSetClause>(setClause, "setClause");
			this.VisitExpression(setClause.Property);
			this.VisitExpression(setClause.Value);
		}

		// Token: 0x06003720 RID: 14112 RVA: 0x000D1A2C File Offset: 0x000CFC2C
		protected virtual void VisitModificationClause(DbModificationClause modificationClause)
		{
			EntityUtil.CheckArgumentNull<DbModificationClause>(modificationClause, "modificationClause");
			this.VisitSetClause((DbSetClause)modificationClause);
		}

		// Token: 0x06003721 RID: 14113 RVA: 0x000D1A48 File Offset: 0x000CFC48
		protected virtual void VisitModificationClauses(IList<DbModificationClause> modificationClauses)
		{
			EntityUtil.CheckArgumentNull<IList<DbModificationClause>>(modificationClauses, "modificationClauses");
			for (int i = 0; i < modificationClauses.Count; i++)
			{
				this.VisitModificationClause(modificationClauses[i]);
			}
		}

		// Token: 0x06003722 RID: 14114 RVA: 0x000D1A80 File Offset: 0x000CFC80
		public virtual void VisitCommandTree(DbCommandTree commandTree)
		{
			EntityUtil.CheckArgumentNull<DbCommandTree>(commandTree, "commandTree");
			switch (commandTree.CommandTreeKind)
			{
			case DbCommandTreeKind.Query:
				this.VisitQueryCommandTree((DbQueryCommandTree)commandTree);
				return;
			case DbCommandTreeKind.Update:
				this.VisitUpdateCommandTree((DbUpdateCommandTree)commandTree);
				return;
			case DbCommandTreeKind.Insert:
				this.VisitInsertCommandTree((DbInsertCommandTree)commandTree);
				return;
			case DbCommandTreeKind.Delete:
				this.VisitDeleteCommandTree((DbDeleteCommandTree)commandTree);
				return;
			case DbCommandTreeKind.Function:
				this.VisitFunctionCommandTree((DbFunctionCommandTree)commandTree);
				return;
			default:
				throw EntityUtil.NotSupported();
			}
		}

		// Token: 0x06003723 RID: 14115 RVA: 0x000D1B02 File Offset: 0x000CFD02
		protected virtual void VisitDeleteCommandTree(DbDeleteCommandTree deleteTree)
		{
			EntityUtil.CheckArgumentNull<DbDeleteCommandTree>(deleteTree, "deleteTree");
			this.VisitExpressionBindingPre(deleteTree.Target);
			this.VisitExpression(deleteTree.Predicate);
			this.VisitExpressionBindingPost(deleteTree.Target);
		}

		// Token: 0x06003724 RID: 14116 RVA: 0x000D1B34 File Offset: 0x000CFD34
		protected virtual void VisitFunctionCommandTree(DbFunctionCommandTree functionTree)
		{
			EntityUtil.CheckArgumentNull<DbFunctionCommandTree>(functionTree, "functionTree");
		}

		// Token: 0x06003725 RID: 14117 RVA: 0x000D1B44 File Offset: 0x000CFD44
		protected virtual void VisitInsertCommandTree(DbInsertCommandTree insertTree)
		{
			EntityUtil.CheckArgumentNull<DbInsertCommandTree>(insertTree, "insertTree");
			this.VisitExpressionBindingPre(insertTree.Target);
			this.VisitModificationClauses(insertTree.SetClauses);
			if (insertTree.Returning != null)
			{
				this.VisitExpression(insertTree.Returning);
			}
			this.VisitExpressionBindingPost(insertTree.Target);
		}

		// Token: 0x06003726 RID: 14118 RVA: 0x000D1B95 File Offset: 0x000CFD95
		protected virtual void VisitQueryCommandTree(DbQueryCommandTree queryTree)
		{
			EntityUtil.CheckArgumentNull<DbQueryCommandTree>(queryTree, "queryTree");
			this.VisitExpression(queryTree.Query);
		}

		// Token: 0x06003727 RID: 14119 RVA: 0x000D1BB0 File Offset: 0x000CFDB0
		protected virtual void VisitUpdateCommandTree(DbUpdateCommandTree updateTree)
		{
			EntityUtil.CheckArgumentNull<DbUpdateCommandTree>(updateTree, "updateTree");
			this.VisitExpressionBindingPre(updateTree.Target);
			this.VisitModificationClauses(updateTree.SetClauses);
			this.VisitExpression(updateTree.Predicate);
			if (updateTree.Returning != null)
			{
				this.VisitExpression(updateTree.Returning);
			}
			this.VisitExpressionBindingPost(updateTree.Target);
		}
	}
}
