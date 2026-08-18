using System;
using System.Collections.Generic;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x0200010D RID: 269
	public abstract class BasicCommandTreeVisitor : BasicExpressionVisitor
	{
		// Token: 0x06000702 RID: 1794 RVA: 0x000269C8 File Offset: 0x00024BC8
		protected virtual void VisitSetClause(DbSetClause setClause)
		{
			Check.NotNull<DbSetClause>(setClause, "setClause");
			this.VisitExpression(setClause.Property);
			this.VisitExpression(setClause.Value);
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x000269EE File Offset: 0x00024BEE
		protected virtual void VisitModificationClause(DbModificationClause modificationClause)
		{
			Check.NotNull<DbModificationClause>(modificationClause, "modificationClause");
			this.VisitSetClause((DbSetClause)modificationClause);
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x00026A08 File Offset: 0x00024C08
		protected virtual void VisitModificationClauses(IList<DbModificationClause> modificationClauses)
		{
			Check.NotNull<IList<DbModificationClause>>(modificationClauses, "modificationClauses");
			for (int i = 0; i < modificationClauses.Count; i++)
			{
				this.VisitModificationClause(modificationClauses[i]);
			}
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x00026A40 File Offset: 0x00024C40
		public virtual void VisitCommandTree(DbCommandTree commandTree)
		{
			Check.NotNull<DbCommandTree>(commandTree, "commandTree");
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
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x00026AC2 File Offset: 0x00024CC2
		protected virtual void VisitDeleteCommandTree(DbDeleteCommandTree deleteTree)
		{
			Check.NotNull<DbDeleteCommandTree>(deleteTree, "deleteTree");
			this.VisitExpressionBindingPre(deleteTree.Target);
			this.VisitExpression(deleteTree.Predicate);
			this.VisitExpressionBindingPost(deleteTree.Target);
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x00026AF4 File Offset: 0x00024CF4
		protected virtual void VisitFunctionCommandTree(DbFunctionCommandTree functionTree)
		{
			Check.NotNull<DbFunctionCommandTree>(functionTree, "functionTree");
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x00026B04 File Offset: 0x00024D04
		protected virtual void VisitInsertCommandTree(DbInsertCommandTree insertTree)
		{
			Check.NotNull<DbInsertCommandTree>(insertTree, "insertTree");
			this.VisitExpressionBindingPre(insertTree.Target);
			this.VisitModificationClauses(insertTree.SetClauses);
			if (insertTree.Returning != null)
			{
				this.VisitExpression(insertTree.Returning);
			}
			this.VisitExpressionBindingPost(insertTree.Target);
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x00026B55 File Offset: 0x00024D55
		protected virtual void VisitQueryCommandTree(DbQueryCommandTree queryTree)
		{
			Check.NotNull<DbQueryCommandTree>(queryTree, "queryTree");
			this.VisitExpression(queryTree.Query);
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x00026B70 File Offset: 0x00024D70
		protected virtual void VisitUpdateCommandTree(DbUpdateCommandTree updateTree)
		{
			Check.NotNull<DbUpdateCommandTree>(updateTree, "updateTree");
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
