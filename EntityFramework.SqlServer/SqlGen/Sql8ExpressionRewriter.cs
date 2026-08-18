using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.SqlServer.Utilities;
using System.Globalization;

namespace System.Data.Entity.SqlServer.SqlGen
{
	// Token: 0x02000035 RID: 53
	internal class Sql8ExpressionRewriter : DbExpressionRebinder
	{
		// Token: 0x060002FE RID: 766 RVA: 0x0000C2F8 File Offset: 0x0000A4F8
		internal static DbQueryCommandTree Rewrite(DbQueryCommandTree originalTree)
		{
			Sql8ExpressionRewriter sql8ExpressionRewriter = new Sql8ExpressionRewriter(originalTree.MetadataWorkspace);
			DbExpression query = sql8ExpressionRewriter.VisitExpression(originalTree.Query);
			return new DbQueryCommandTree(originalTree.MetadataWorkspace, originalTree.DataSpace, query, false);
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0000C331 File Offset: 0x0000A531
		private Sql8ExpressionRewriter(MetadataWorkspace metadata) : base(metadata)
		{
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0000C33A File Offset: 0x0000A53A
		public override DbExpression Visit(DbExceptExpression e)
		{
			Check.NotNull<DbExceptExpression>(e, "e");
			return this.TransformIntersectOrExcept(this.VisitExpression(e.Left), this.VisitExpression(e.Right), DbExpressionKind.Except);
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0000C368 File Offset: 0x0000A568
		public override DbExpression Visit(DbIntersectExpression e)
		{
			Check.NotNull<DbIntersectExpression>(e, "e");
			return this.TransformIntersectOrExcept(this.VisitExpression(e.Left), this.VisitExpression(e.Right), DbExpressionKind.Intersect);
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0000C398 File Offset: 0x0000A598
		public override DbExpression Visit(DbSkipExpression e)
		{
			Check.NotNull<DbSkipExpression>(e, "e");
			DbExpression right = this.VisitExpressionBinding(e.Input).Sort(this.VisitSortOrder(e.SortOrder)).Limit(this.VisitExpression(e.Count));
			DbExpression left = this.VisitExpression(e.Input.Expression);
			IList<DbSortClause> list = this.VisitSortOrder(e.SortOrder);
			IList<DbPropertyExpression> list2 = new List<DbPropertyExpression>(e.SortOrder.Count);
			foreach (DbSortClause dbSortClause in list)
			{
				if (dbSortClause.Expression.ExpressionKind == DbExpressionKind.Property)
				{
					list2.Add((DbPropertyExpression)dbSortClause.Expression);
				}
			}
			DbExpression input = this.TransformIntersectOrExcept(left, right, DbExpressionKind.Skip, list2, e.Input.VariableName);
			return input.BindAs(e.Input.VariableName).Sort(list);
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0000C4A0 File Offset: 0x0000A6A0
		private DbExpression TransformIntersectOrExcept(DbExpression left, DbExpression right, DbExpressionKind expressionKind)
		{
			return this.TransformIntersectOrExcept(left, right, expressionKind, null, null);
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0000C4B0 File Offset: 0x0000A6B0
		private DbExpression TransformIntersectOrExcept(DbExpression left, DbExpression right, DbExpressionKind expressionKind, IList<DbPropertyExpression> sortExpressionsOverLeft, string sortExpressionsBindingVariableName)
		{
			bool flag = expressionKind == DbExpressionKind.Except || expressionKind == DbExpressionKind.Skip;
			bool flag2 = expressionKind == DbExpressionKind.Except || expressionKind == DbExpressionKind.Intersect;
			DbExpressionBinding dbExpressionBinding = left.Bind();
			DbExpressionBinding dbExpressionBinding2 = right.Bind();
			IList<DbPropertyExpression> list = new List<DbPropertyExpression>();
			IList<DbPropertyExpression> list2 = new List<DbPropertyExpression>();
			this.FlattenProperties(dbExpressionBinding.Variable, list);
			this.FlattenProperties(dbExpressionBinding2.Variable, list2);
			if (expressionKind == DbExpressionKind.Skip && Sql8ExpressionRewriter.RemoveNonSortProperties(list, list2, sortExpressionsOverLeft, dbExpressionBinding.VariableName, sortExpressionsBindingVariableName))
			{
				dbExpressionBinding2 = Sql8ExpressionRewriter.CapWithProject(dbExpressionBinding2, list2);
			}
			DbExpression dbExpression = null;
			for (int i = 0; i < list.Count; i++)
			{
				DbExpression left2 = list[i].Equal(list2[i]);
				DbExpression left3 = list[i].IsNull();
				DbExpression right2 = list2[i].IsNull();
				DbExpression right3 = left3.And(right2);
				DbExpression dbExpression2 = left2.Or(right3);
				if (i == 0)
				{
					dbExpression = dbExpression2;
				}
				else
				{
					dbExpression = dbExpression.And(dbExpression2);
				}
			}
			DbExpression dbExpression3 = dbExpressionBinding2.Any(dbExpression);
			DbExpression predicate;
			if (flag)
			{
				predicate = dbExpression3.Not();
			}
			else
			{
				predicate = dbExpression3;
			}
			DbExpression dbExpression4 = dbExpressionBinding.Filter(predicate);
			if (flag2)
			{
				dbExpression4 = dbExpression4.Distinct();
			}
			return dbExpression4;
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0000C5E8 File Offset: 0x0000A7E8
		private void FlattenProperties(DbExpression input, IList<DbPropertyExpression> flattenedProperties)
		{
			IEnumerable<EdmProperty> properties = input.ResultType.GetProperties();
			foreach (EdmProperty edmProperty in properties)
			{
				DbPropertyExpression dbPropertyExpression = input.Property(edmProperty);
				if (BuiltInTypeKind.PrimitiveType == edmProperty.TypeUsage.EdmType.BuiltInTypeKind)
				{
					flattenedProperties.Add(dbPropertyExpression);
				}
				else
				{
					this.FlattenProperties(dbPropertyExpression, flattenedProperties);
				}
			}
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0000C66C File Offset: 0x0000A86C
		private static bool RemoveNonSortProperties(IList<DbPropertyExpression> list1, IList<DbPropertyExpression> list2, IList<DbPropertyExpression> sortList, string list1BindingVariableName, string sortExpressionsBindingVariableName)
		{
			bool result = false;
			for (int i = list1.Count - 1; i >= 0; i--)
			{
				if (!Sql8ExpressionRewriter.HasMatchInList(list1[i], sortList, list1BindingVariableName, sortExpressionsBindingVariableName))
				{
					list1.RemoveAt(i);
					list2.RemoveAt(i);
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0000C6B4 File Offset: 0x0000A8B4
		private static bool HasMatchInList(DbPropertyExpression expr, IList<DbPropertyExpression> list, string exprBindingVariableName, string listExpressionsBindingVariableName)
		{
			for (int i = 0; i < list.Count; i++)
			{
				if (Sql8ExpressionRewriter.AreMatching(expr, list[i], exprBindingVariableName, listExpressionsBindingVariableName))
				{
					list.RemoveAt(i);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0000C6F0 File Offset: 0x0000A8F0
		private static bool AreMatching(DbPropertyExpression expr1, DbPropertyExpression expr2, string expr1BindingVariableName, string expr2BindingVariableName)
		{
			if (expr1.Property.Name != expr2.Property.Name)
			{
				return false;
			}
			if (expr1.Instance.ExpressionKind != expr2.Instance.ExpressionKind)
			{
				return false;
			}
			if (expr1.Instance.ExpressionKind == DbExpressionKind.Property)
			{
				return Sql8ExpressionRewriter.AreMatching((DbPropertyExpression)expr1.Instance, (DbPropertyExpression)expr2.Instance, expr1BindingVariableName, expr2BindingVariableName);
			}
			DbVariableReferenceExpression dbVariableReferenceExpression = (DbVariableReferenceExpression)expr1.Instance;
			DbVariableReferenceExpression dbVariableReferenceExpression2 = (DbVariableReferenceExpression)expr2.Instance;
			return string.Equals(dbVariableReferenceExpression.VariableName, expr1BindingVariableName, StringComparison.Ordinal) && string.Equals(dbVariableReferenceExpression2.VariableName, expr2BindingVariableName, StringComparison.Ordinal);
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0000C79C File Offset: 0x0000A99C
		private static DbExpressionBinding CapWithProject(DbExpressionBinding inputBinding, IList<DbPropertyExpression> flattenedProperties)
		{
			List<KeyValuePair<string, DbExpression>> list = new List<KeyValuePair<string, DbExpression>>(flattenedProperties.Count);
			Dictionary<string, int> dictionary = new Dictionary<string, int>(flattenedProperties.Count);
			foreach (DbPropertyExpression dbPropertyExpression in flattenedProperties)
			{
				string text = dbPropertyExpression.Property.Name;
				int num;
				if (dictionary.TryGetValue(text, out num))
				{
					string text2;
					do
					{
						num++;
						text2 = text + num.ToString(CultureInfo.InvariantCulture);
					}
					while (dictionary.ContainsKey(text2));
					dictionary[text] = num;
					text = text2;
				}
				dictionary[text] = 0;
				list.Add(new KeyValuePair<string, DbExpression>(text, dbPropertyExpression));
			}
			DbExpression dbExpression = DbExpressionBuilder.NewRow(list);
			DbProjectExpression input = inputBinding.Project(dbExpression);
			DbExpressionBinding dbExpressionBinding = input.Bind();
			flattenedProperties.Clear();
			RowType rowType = (RowType)dbExpression.ResultType.EdmType;
			foreach (KeyValuePair<string, DbExpression> keyValuePair in list)
			{
				EdmProperty propertyMetadata = rowType.Properties[keyValuePair.Key];
				flattenedProperties.Add(dbExpressionBinding.Variable.Property(propertyMetadata));
			}
			return dbExpressionBinding;
		}
	}
}
