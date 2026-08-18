using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Common.CommandTrees;
using System.Data.Common.CommandTrees.ExpressionBuilder;
using System.Data.Common.CommandTrees.Internal;
using System.Data.Metadata.Edm;
using System.Globalization;

namespace System.Data.SqlClient.SqlGen
{
	// Token: 0x02000030 RID: 48
	internal class Sql8ExpressionRewriter : DbExpressionRebinder
	{
		// Token: 0x06000442 RID: 1090 RVA: 0x00012324 File Offset: 0x00010524
		internal static DbQueryCommandTree Rewrite(DbQueryCommandTree originalTree)
		{
			Sql8ExpressionRewriter sql8ExpressionRewriter = new Sql8ExpressionRewriter(originalTree.MetadataWorkspace);
			DbExpression query = sql8ExpressionRewriter.VisitExpression(originalTree.Query);
			return DbQueryCommandTree.FromValidExpression(originalTree.MetadataWorkspace, originalTree.DataSpace, query);
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x0001235C File Offset: 0x0001055C
		private Sql8ExpressionRewriter(MetadataWorkspace metadata) : base(metadata)
		{
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x00012365 File Offset: 0x00010565
		public override DbExpression Visit(DbExceptExpression e)
		{
			return this.TransformIntersectOrExcept(this.VisitExpression(e.Left), this.VisitExpression(e.Right), DbExpressionKind.Except);
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x00012387 File Offset: 0x00010587
		public override DbExpression Visit(DbIntersectExpression e)
		{
			return this.TransformIntersectOrExcept(this.VisitExpression(e.Left), this.VisitExpression(e.Right), DbExpressionKind.Intersect);
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x000123AC File Offset: 0x000105AC
		public override DbExpression Visit(DbSkipExpression e)
		{
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

		// Token: 0x06000447 RID: 1095 RVA: 0x000124A8 File Offset: 0x000106A8
		private DbExpression TransformIntersectOrExcept(DbExpression left, DbExpression right, DbExpressionKind expressionKind)
		{
			return this.TransformIntersectOrExcept(left, right, expressionKind, null, null);
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x000124B8 File Offset: 0x000106B8
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
				dbExpressionBinding2 = this.CapWithProject(dbExpressionBinding2, list2);
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

		// Token: 0x06000449 RID: 1097 RVA: 0x000125F0 File Offset: 0x000107F0
		private void FlattenProperties(DbExpression input, IList<DbPropertyExpression> flattenedProperties)
		{
			IList<EdmProperty> properties = TypeHelpers.GetProperties(input.ResultType);
			for (int i = 0; i < properties.Count; i++)
			{
				DbPropertyExpression dbPropertyExpression = input.Property(properties[i]);
				if (TypeSemantics.IsPrimitiveType(properties[i].TypeUsage))
				{
					flattenedProperties.Add(dbPropertyExpression);
				}
				else
				{
					this.FlattenProperties(dbPropertyExpression, flattenedProperties);
				}
			}
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x00012650 File Offset: 0x00010850
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

		// Token: 0x0600044B RID: 1099 RVA: 0x00012698 File Offset: 0x00010898
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

		// Token: 0x0600044C RID: 1100 RVA: 0x000126D4 File Offset: 0x000108D4
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

		// Token: 0x0600044D RID: 1101 RVA: 0x00012780 File Offset: 0x00010980
		private DbExpressionBinding CapWithProject(DbExpressionBinding inputBinding, IList<DbPropertyExpression> flattenedProperties)
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
