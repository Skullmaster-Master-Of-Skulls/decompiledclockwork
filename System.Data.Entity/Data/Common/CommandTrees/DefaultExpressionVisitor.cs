using System;
using System.Collections.Generic;
using System.Data.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.Linq;

namespace System.Data.Common.CommandTrees
{
	// Token: 0x02000426 RID: 1062
	public class DefaultExpressionVisitor : DbExpressionVisitor<DbExpression>
	{
		// Token: 0x0600373F RID: 14143 RVA: 0x000D2B4A File Offset: 0x000D0D4A
		protected DefaultExpressionVisitor()
		{
		}

		// Token: 0x06003740 RID: 14144 RVA: 0x000089D0 File Offset: 0x00006BD0
		protected virtual void OnExpressionReplaced(DbExpression oldExpression, DbExpression newExpression)
		{
		}

		// Token: 0x06003741 RID: 14145 RVA: 0x000089D0 File Offset: 0x00006BD0
		protected virtual void OnVariableRebound(DbVariableReferenceExpression fromVarRef, DbVariableReferenceExpression toVarRef)
		{
		}

		// Token: 0x06003742 RID: 14146 RVA: 0x000089D0 File Offset: 0x00006BD0
		protected virtual void OnEnterScope(IEnumerable<DbVariableReferenceExpression> scopeVariables)
		{
		}

		// Token: 0x06003743 RID: 14147 RVA: 0x000089D0 File Offset: 0x00006BD0
		protected virtual void OnExitScope()
		{
		}

		// Token: 0x06003744 RID: 14148 RVA: 0x000D2B60 File Offset: 0x000D0D60
		protected virtual DbExpression VisitExpression(DbExpression expression)
		{
			DbExpression result = null;
			if (expression != null)
			{
				result = expression.Accept<DbExpression>(this);
			}
			return result;
		}

		// Token: 0x06003745 RID: 14149 RVA: 0x000D2B7B File Offset: 0x000D0D7B
		protected virtual IList<DbExpression> VisitExpressionList(IList<DbExpression> list)
		{
			return this.VisitList<DbExpression>(list, new Func<DbExpression, DbExpression>(this.VisitExpression));
		}

		// Token: 0x06003746 RID: 14150 RVA: 0x000D2B94 File Offset: 0x000D0D94
		protected virtual DbExpressionBinding VisitExpressionBinding(DbExpressionBinding binding)
		{
			DbExpressionBinding dbExpressionBinding = binding;
			if (binding != null)
			{
				DbExpression dbExpression = this.VisitExpression(binding.Expression);
				if (binding.Expression != dbExpression)
				{
					dbExpressionBinding = dbExpression.BindAs(binding.VariableName);
					this.RebindVariable(binding.Variable, dbExpressionBinding.Variable);
				}
			}
			return dbExpressionBinding;
		}

		// Token: 0x06003747 RID: 14151 RVA: 0x000D2BDC File Offset: 0x000D0DDC
		protected virtual IList<DbExpressionBinding> VisitExpressionBindingList(IList<DbExpressionBinding> list)
		{
			return this.VisitList<DbExpressionBinding>(list, new Func<DbExpressionBinding, DbExpressionBinding>(this.VisitExpressionBinding));
		}

		// Token: 0x06003748 RID: 14152 RVA: 0x000D2BF4 File Offset: 0x000D0DF4
		protected virtual DbGroupExpressionBinding VisitGroupExpressionBinding(DbGroupExpressionBinding binding)
		{
			DbGroupExpressionBinding dbGroupExpressionBinding = binding;
			if (binding != null)
			{
				DbExpression dbExpression = this.VisitExpression(binding.Expression);
				if (binding.Expression != dbExpression)
				{
					dbGroupExpressionBinding = dbExpression.GroupBindAs(binding.VariableName, binding.GroupVariableName);
					this.RebindVariable(binding.Variable, dbGroupExpressionBinding.Variable);
					this.RebindVariable(binding.GroupVariable, dbGroupExpressionBinding.GroupVariable);
				}
			}
			return dbGroupExpressionBinding;
		}

		// Token: 0x06003749 RID: 14153 RVA: 0x000D2C54 File Offset: 0x000D0E54
		protected virtual DbSortClause VisitSortClause(DbSortClause clause)
		{
			DbSortClause result = clause;
			if (clause != null)
			{
				DbExpression dbExpression = this.VisitExpression(clause.Expression);
				if (clause.Expression != dbExpression)
				{
					if (!string.IsNullOrEmpty(clause.Collation))
					{
						result = (clause.Ascending ? dbExpression.ToSortClause(clause.Collation) : dbExpression.ToSortClauseDescending(clause.Collation));
					}
					else
					{
						result = (clause.Ascending ? dbExpression.ToSortClause() : dbExpression.ToSortClauseDescending());
					}
				}
			}
			return result;
		}

		// Token: 0x0600374A RID: 14154 RVA: 0x000D2CC6 File Offset: 0x000D0EC6
		protected virtual IList<DbSortClause> VisitSortOrder(IList<DbSortClause> sortOrder)
		{
			return this.VisitList<DbSortClause>(sortOrder, new Func<DbSortClause, DbSortClause>(this.VisitSortClause));
		}

		// Token: 0x0600374B RID: 14155 RVA: 0x000D2CDC File Offset: 0x000D0EDC
		protected virtual DbAggregate VisitAggregate(DbAggregate aggregate)
		{
			DbFunctionAggregate dbFunctionAggregate = aggregate as DbFunctionAggregate;
			if (dbFunctionAggregate != null)
			{
				return this.VisitFunctionAggregate(dbFunctionAggregate);
			}
			DbGroupAggregate aggregate2 = (DbGroupAggregate)aggregate;
			return this.VisitGroupAggregate(aggregate2);
		}

		// Token: 0x0600374C RID: 14156 RVA: 0x000D2D0C File Offset: 0x000D0F0C
		protected virtual DbFunctionAggregate VisitFunctionAggregate(DbFunctionAggregate aggregate)
		{
			DbFunctionAggregate result = aggregate;
			if (aggregate != null)
			{
				EdmFunction edmFunction = this.VisitFunction(aggregate.Function);
				IList<DbExpression> list = this.VisitExpressionList(aggregate.Arguments);
				if (aggregate.Function != edmFunction || aggregate.Arguments != list)
				{
					if (aggregate.Distinct)
					{
						result = edmFunction.AggregateDistinct(list[0]);
					}
					else
					{
						result = edmFunction.Aggregate(list[0]);
					}
				}
			}
			return result;
		}

		// Token: 0x0600374D RID: 14157 RVA: 0x000D2D74 File Offset: 0x000D0F74
		protected virtual DbGroupAggregate VisitGroupAggregate(DbGroupAggregate aggregate)
		{
			DbGroupAggregate result = aggregate;
			if (aggregate != null)
			{
				IList<DbExpression> list = this.VisitExpressionList(aggregate.Arguments);
				if (aggregate.Arguments != list)
				{
					result = DbExpressionBuilder.GroupAggregate(list[0]);
				}
			}
			return result;
		}

		// Token: 0x0600374E RID: 14158 RVA: 0x000D2DAC File Offset: 0x000D0FAC
		protected virtual DbLambda VisitLambda(DbLambda lambda)
		{
			EntityUtil.CheckArgumentNull<DbLambda>(lambda, "lambda");
			DbLambda result = lambda;
			IList<DbVariableReferenceExpression> list = this.VisitList<DbVariableReferenceExpression>(lambda.Variables, delegate(DbVariableReferenceExpression varRef)
			{
				TypeUsage typeUsage = this.VisitTypeUsage(varRef.ResultType);
				if (varRef.ResultType != typeUsage)
				{
					return typeUsage.Variable(varRef.VariableName);
				}
				return varRef;
			});
			this.EnterScope(list.ToArray<DbVariableReferenceExpression>());
			DbExpression dbExpression = this.VisitExpression(lambda.Body);
			this.ExitScope();
			if (lambda.Variables != list || lambda.Body != dbExpression)
			{
				result = DbExpressionBuilder.Lambda(dbExpression, list);
			}
			return result;
		}

		// Token: 0x0600374F RID: 14159 RVA: 0x00002391 File Offset: 0x00000591
		protected virtual EdmType VisitType(EdmType type)
		{
			return type;
		}

		// Token: 0x06003750 RID: 14160 RVA: 0x00002391 File Offset: 0x00000591
		protected virtual TypeUsage VisitTypeUsage(TypeUsage type)
		{
			return type;
		}

		// Token: 0x06003751 RID: 14161 RVA: 0x00002391 File Offset: 0x00000591
		protected virtual EntitySetBase VisitEntitySet(EntitySetBase entitySet)
		{
			return entitySet;
		}

		// Token: 0x06003752 RID: 14162 RVA: 0x00002391 File Offset: 0x00000591
		protected virtual EdmFunction VisitFunction(EdmFunction functionMetadata)
		{
			return functionMetadata;
		}

		// Token: 0x06003753 RID: 14163 RVA: 0x000D2E1A File Offset: 0x000D101A
		private void NotifyIfChanged(DbExpression originalExpression, DbExpression newExpression)
		{
			if (originalExpression != newExpression)
			{
				this.OnExpressionReplaced(originalExpression, newExpression);
			}
		}

		// Token: 0x06003754 RID: 14164 RVA: 0x000D2E28 File Offset: 0x000D1028
		private IList<TElement> VisitList<TElement>(IList<TElement> list, Func<TElement, TElement> map)
		{
			IList<TElement> result = list;
			if (list != null)
			{
				List<TElement> list2 = null;
				for (int i = 0; i < list.Count; i++)
				{
					TElement telement = map(list[i]);
					if (list2 == null && list[i] != telement)
					{
						list2 = new List<TElement>(list);
						result = list2;
					}
					if (list2 != null)
					{
						list2[i] = telement;
					}
				}
			}
			return result;
		}

		// Token: 0x06003755 RID: 14165 RVA: 0x000D2E88 File Offset: 0x000D1088
		private DbExpression VisitUnary(DbUnaryExpression expression, Func<DbExpression, DbExpression> callback)
		{
			DbExpression dbExpression = expression;
			DbExpression dbExpression2 = this.VisitExpression(expression.Argument);
			if (expression.Argument != dbExpression2)
			{
				dbExpression = callback(dbExpression2);
			}
			this.NotifyIfChanged(expression, dbExpression);
			return dbExpression;
		}

		// Token: 0x06003756 RID: 14166 RVA: 0x000D2EC0 File Offset: 0x000D10C0
		private DbExpression VisitTypeUnary(DbUnaryExpression expression, TypeUsage type, Func<DbExpression, TypeUsage, DbExpression> callback)
		{
			DbExpression dbExpression = expression;
			DbExpression dbExpression2 = this.VisitExpression(expression.Argument);
			TypeUsage typeUsage = this.VisitTypeUsage(type);
			if (expression.Argument != dbExpression2 || type != typeUsage)
			{
				dbExpression = callback(dbExpression2, typeUsage);
			}
			this.NotifyIfChanged(expression, dbExpression);
			return dbExpression;
		}

		// Token: 0x06003757 RID: 14167 RVA: 0x000D2F04 File Offset: 0x000D1104
		private DbExpression VisitBinary(DbBinaryExpression expression, Func<DbExpression, DbExpression, DbExpression> callback)
		{
			DbExpression dbExpression = expression;
			DbExpression dbExpression2 = this.VisitExpression(expression.Left);
			DbExpression dbExpression3 = this.VisitExpression(expression.Right);
			if (expression.Left != dbExpression2 || expression.Right != dbExpression3)
			{
				dbExpression = callback(dbExpression2, dbExpression3);
			}
			this.NotifyIfChanged(expression, dbExpression);
			return dbExpression;
		}

		// Token: 0x06003758 RID: 14168 RVA: 0x000D2F54 File Offset: 0x000D1154
		private DbRelatedEntityRef VisitRelatedEntityRef(DbRelatedEntityRef entityRef)
		{
			RelationshipEndMember relationshipEndMember;
			RelationshipEndMember relationshipEndMember2;
			this.VisitRelationshipEnds(entityRef.SourceEnd, entityRef.TargetEnd, out relationshipEndMember, out relationshipEndMember2);
			DbExpression dbExpression = this.VisitExpression(entityRef.TargetEntityReference);
			if (entityRef.SourceEnd != relationshipEndMember || entityRef.TargetEnd != relationshipEndMember2 || entityRef.TargetEntityReference != dbExpression)
			{
				return DbExpressionBuilder.CreateRelatedEntityRef(relationshipEndMember, relationshipEndMember2, dbExpression);
			}
			return entityRef;
		}

		// Token: 0x06003759 RID: 14169 RVA: 0x000D2FAC File Offset: 0x000D11AC
		private void VisitRelationshipEnds(RelationshipEndMember source, RelationshipEndMember target, out RelationshipEndMember newSource, out RelationshipEndMember newTarget)
		{
			RelationshipType relationshipType = (RelationshipType)this.VisitType(target.DeclaringType);
			newSource = relationshipType.RelationshipEndMembers[source.Name];
			newTarget = relationshipType.RelationshipEndMembers[target.Name];
		}

		// Token: 0x0600375A RID: 14170 RVA: 0x000D2FF4 File Offset: 0x000D11F4
		private DbExpression VisitTerminal(DbExpression expression, Func<TypeUsage, DbExpression> reconstructor)
		{
			DbExpression dbExpression = expression;
			TypeUsage typeUsage = this.VisitTypeUsage(expression.ResultType);
			if (expression.ResultType != typeUsage)
			{
				dbExpression = reconstructor(typeUsage);
			}
			this.NotifyIfChanged(expression, dbExpression);
			return dbExpression;
		}

		// Token: 0x0600375B RID: 14171 RVA: 0x000D302C File Offset: 0x000D122C
		private void RebindVariable(DbVariableReferenceExpression from, DbVariableReferenceExpression to)
		{
			if (!from.VariableName.Equals(to.VariableName, StringComparison.Ordinal) || from.ResultType.EdmType != to.ResultType.EdmType || !from.ResultType.EdmEquals(to.ResultType))
			{
				this.varMappings[from] = to;
				this.OnVariableRebound(from, to);
			}
		}

		// Token: 0x0600375C RID: 14172 RVA: 0x000D3090 File Offset: 0x000D1290
		private DbExpressionBinding VisitExpressionBindingEnterScope(DbExpressionBinding binding)
		{
			DbExpressionBinding dbExpressionBinding = this.VisitExpressionBinding(binding);
			this.OnEnterScope(new DbVariableReferenceExpression[]
			{
				dbExpressionBinding.Variable
			});
			return dbExpressionBinding;
		}

		// Token: 0x0600375D RID: 14173 RVA: 0x000D30BB File Offset: 0x000D12BB
		private void EnterScope(params DbVariableReferenceExpression[] scopeVars)
		{
			this.OnEnterScope(scopeVars);
		}

		// Token: 0x0600375E RID: 14174 RVA: 0x000D30C4 File Offset: 0x000D12C4
		private void ExitScope()
		{
			this.OnExitScope();
		}

		// Token: 0x0600375F RID: 14175 RVA: 0x000D05D9 File Offset: 0x000CE7D9
		public override DbExpression Visit(DbExpression expression)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(expression, "expression");
			throw EntityUtil.NotSupported(Strings.Cqt_General_UnsupportedExpression(expression.GetType().FullName));
		}

		// Token: 0x06003760 RID: 14176 RVA: 0x000D30CC File Offset: 0x000D12CC
		public override DbExpression Visit(DbConstantExpression expression)
		{
			EntityUtil.CheckArgumentNull<DbConstantExpression>(expression, "expression");
			return this.VisitTerminal(expression, (TypeUsage newType) => newType.Constant(expression.GetValue()));
		}

		// Token: 0x06003761 RID: 14177 RVA: 0x000D310F File Offset: 0x000D130F
		public override DbExpression Visit(DbNullExpression expression)
		{
			EntityUtil.CheckArgumentNull<DbNullExpression>(expression, "expression");
			return this.VisitTerminal(expression, new Func<TypeUsage, DbExpression>(DbExpressionBuilder.Null));
		}

		// Token: 0x06003762 RID: 14178 RVA: 0x000D3130 File Offset: 0x000D1330
		public override DbExpression Visit(DbVariableReferenceExpression expression)
		{
			EntityUtil.CheckArgumentNull<DbVariableReferenceExpression>(expression, "expression");
			DbExpression dbExpression = expression;
			DbVariableReferenceExpression dbVariableReferenceExpression;
			if (this.varMappings.TryGetValue(expression, out dbVariableReferenceExpression))
			{
				dbExpression = dbVariableReferenceExpression;
			}
			this.NotifyIfChanged(expression, dbExpression);
			return dbExpression;
		}

		// Token: 0x06003763 RID: 14179 RVA: 0x000D3168 File Offset: 0x000D1368
		public override DbExpression Visit(DbParameterReferenceExpression expression)
		{
			EntityUtil.CheckArgumentNull<DbParameterReferenceExpression>(expression, "expression");
			return this.VisitTerminal(expression, (TypeUsage newType) => newType.Parameter(expression.ParameterName));
		}

		// Token: 0x06003764 RID: 14180 RVA: 0x000D31AC File Offset: 0x000D13AC
		public override DbExpression Visit(DbFunctionExpression expression)
		{
			EntityUtil.CheckArgumentNull<DbFunctionExpression>(expression, "expression");
			DbExpression dbExpression = expression;
			IList<DbExpression> list = this.VisitExpressionList(expression.Arguments);
			EdmFunction edmFunction = this.VisitFunction(expression.Function);
			if (expression.Arguments != list || expression.Function != edmFunction)
			{
				dbExpression = edmFunction.Invoke(list);
			}
			this.NotifyIfChanged(expression, dbExpression);
			return dbExpression;
		}

		// Token: 0x06003765 RID: 14181 RVA: 0x000D3204 File Offset: 0x000D1404
		public override DbExpression Visit(DbLambdaExpression expression)
		{
			EntityUtil.CheckArgumentNull<DbLambdaExpression>(expression, "expression");
			DbExpression dbExpression = expression;
			IList<DbExpression> list = this.VisitExpressionList(expression.Arguments);
			DbLambda dbLambda = this.VisitLambda(expression.Lambda);
			if (expression.Arguments != list || expression.Lambda != dbLambda)
			{
				dbExpression = dbLambda.Invoke(list);
			}
			this.NotifyIfChanged(expression, dbExpression);
			return dbExpression;
		}

		// Token: 0x06003766 RID: 14182 RVA: 0x000D325C File Offset: 0x000D145C
		public override DbExpression Visit(DbPropertyExpression expression)
		{
			EntityUtil.CheckArgumentNull<DbPropertyExpression>(expression, "expression");
			DbExpression dbExpression = expression;
			DbExpression dbExpression2 = this.VisitExpression(expression.Instance);
			if (expression.Instance != dbExpression2)
			{
				dbExpression = dbExpression2.Property(expression.Property.Name);
			}
			this.NotifyIfChanged(expression, dbExpression);
			return dbExpression;
		}

		// Token: 0x06003767 RID: 14183 RVA: 0x000D32A8 File Offset: 0x000D14A8
		public override DbExpression Visit(DbComparisonExpression expression)
		{
			EntityUtil.CheckArgumentNull<DbComparisonExpression>(expression, "expression");
			DbExpressionKind expressionKind = expression.ExpressionKind;
			if (expressionKind <= DbExpressionKind.GreaterThanOrEquals)
			{
				if (expressionKind == DbExpressionKind.Equals)
				{
					return this.VisitBinary(expression, new Func<DbExpression, DbExpression, DbExpression>(DbExpressionBuilder.Equal));
				}
				if (expressionKind == DbExpressionKind.GreaterThan)
				{
					return this.VisitBinary(expression, new Func<DbExpression, DbExpression, DbExpression>(DbExpressionBuilder.GreaterThan));
				}
				if (expressionKind == DbExpressionKind.GreaterThanOrEquals)
				{
					return this.VisitBinary(expression, new Func<DbExpression, DbExpression, DbExpression>(DbExpressionBuilder.GreaterThanOrEqual));
				}
			}
			else
			{
				if (expressionKind == DbExpressionKind.LessThan)
				{
					return this.VisitBinary(expression, new Func<DbExpression, DbExpression, DbExpression>(DbExpressionBuilder.LessThan));
				}
				if (expressionKind == DbExpressionKind.LessThanOrEquals)
				{
					return this.VisitBinary(expression, new Func<DbExpression, DbExpression, DbExpression>(DbExpressionBuilder.LessThanOrEqual));
				}
				if (expressionKind == DbExpressionKind.NotEquals)
				{
					return this.VisitBinary(expression, new Func<DbExpression, DbExpression, DbExpression>(DbExpressionBuilder.NotEqual));
				}
			}
			throw EntityUtil.NotSupported();
		}

		// Token: 0x06003768 RID: 14184 RVA: 0x000D3370 File Offset: 0x000D1570
		public override DbExpression Visit(DbLikeExpression expression)
		{
			EntityUtil.CheckArgumentNull<DbLikeExpression>(expression, "expression");
			DbExpression dbExpression = expression;
			DbExpression dbExpression2 = this.VisitExpression(expression.Argument);
			DbExpression dbExpression3 = this.VisitExpression(expression.Pattern);
			DbExpression dbExpression4 = this.VisitExpression(expression.Escape);
			if (expression.Argument != dbExpression2 || expression.Pattern != dbExpression3 || expression.Escape != dbExpression4)
			{
				dbExpression = dbExpression2.Like(dbExpression3, dbExpression4);
			}
			this.NotifyIfChanged(expression, dbExpression);
			return dbExpression;
		}

		// Token: 0x06003769 RID: 14185 RVA: 0x000D33E0 File Offset: 0x000D15E0
		public override DbExpression Visit(DbLimitExpression expression)
		{
			EntityUtil.CheckArgumentNull<DbLimitExpression>(expression, "expression");
			DbExpression dbExpression = expression;
			DbExpression dbExpression2 = this.VisitExpression(expression.Argument);
			DbExpression dbExpression3 = this.VisitExpression(expression.Limit);
			if (expression.Argument != dbExpression2 || expression.Limit != dbExpression3)
			{
				dbExpression = dbExpression2.Limit(dbExpression3);
			}
			this.NotifyIfChanged(expression, dbExpression);
			return dbExpression;
		}

		// Token: 0x0600376A RID: 14186 RVA: 0x000D3438 File Offset: 0x000D1638
		public override DbExpression Visit(DbIsNullExpression expression)
		{
			EntityUtil.CheckArgumentNull<DbIsNullExpression>(expression, "expression");
			return this.VisitUnary(expression, delegate(DbExpression exp)
			{
				if (TypeSemantics.IsRowType(exp.ResultType))
				{
					return DbExpressionBuilder.CreateIsNullExpressionAllowingRowTypeArgument(exp);
				}
				return exp.IsNull();
			});
		}

		// Token: 0x0600376B RID: 14187 RVA: 0x000D346C File Offset: 0x000D166C
		public override DbExpression Visit(DbArithmeticExpression expression)
		{
			EntityUtil.CheckArgumentNull<DbArithmeticExpression>(expression, "expression");
			DbExpression dbExpression = expression;
			IList<DbExpression> list = this.VisitExpressionList(expression.Arguments);
			if (expression.Arguments != list)
			{
				DbExpressionKind expressionKind = expression.ExpressionKind;
				if (expressionKind <= DbExpressionKind.Multiply)
				{
					if (expressionKind == DbExpressionKind.Divide)
					{
						dbExpression = list[0].Divide(list[1]);
						goto IL_E1;
					}
					switch (expressionKind)
					{
					case DbExpressionKind.Minus:
						dbExpression = list[0].Minus(list[1]);
						goto IL_E1;
					case DbExpressionKind.Modulo:
						dbExpression = list[0].Modulo(list[1]);
						goto IL_E1;
					case DbExpressionKind.Multiply:
						dbExpression = list[0].Multiply(list[1]);
						goto IL_E1;
					}
				}
				else
				{
					if (expressionKind == DbExpressionKind.Plus)
					{
						dbExpression = list[0].Plus(list[1]);
						goto IL_E1;
					}
					if (expressionKind == DbExpressionKind.UnaryMinus)
					{
						dbExpression = list[0].UnaryMinus();
						goto IL_E1;
					}
				}
				throw EntityUtil.NotSupported();
			}
			IL_E1:
			this.NotifyIfChanged(expression, dbExpression);
			return dbExpression;
		}

		// Token: 0x0600376C RID: 14188 RVA: 0x000D3563 File Offset: 0x000D1763
		public override DbExpression Visit(DbAndExpression expression)
		{
			EntityUtil.CheckArgumentNull<DbAndExpression>(expression, "expression");
			return this.VisitBinary(expression, new Func<DbExpression, DbExpression, DbExpression>(DbExpressionBuilder.And));
		}

		// Token: 0x0600376D RID: 14189 RVA: 0x000D3584 File Offset: 0x000D1784
		public override DbExpression Visit(DbOrExpression expression)
		{
			EntityUtil.CheckArgumentNull<DbOrExpression>(expression, "expression");
			return this.VisitBinary(expression, new Func<DbExpression, DbExpression, DbExpression>(DbExpressionBuilder.Or));
		}

		// Token: 0x0600376E RID: 14190 RVA: 0x000D35A5 File Offset: 0x000D17A5
		public override DbExpression Visit(DbNotExpression expression)
		{
			EntityUtil.CheckArgumentNull<DbNotExpression>(expression, "expression");
			return this.VisitUnary(expression, new Func<DbExpression, DbExpression>(DbExpressionBuilder.Not));
		}

		// Token: 0x0600376F RID: 14191 RVA: 0x000D35C6 File Offset: 0x000D17C6
		public override DbExpression Visit(DbDistinctExpression expression)
		{
			EntityUtil.CheckArgumentNull<DbDistinctExpression>(expression, "expression");
			return this.VisitUnary(expression, new Func<DbExpression, DbExpression>(DbExpressionBuilder.Distinct));
		}

		// Token: 0x06003770 RID: 14192 RVA: 0x000D35E8 File Offset: 0x000D17E8
		public override DbExpression Visit(DbElementExpression expression)
		{
			EntityUtil.CheckArgumentNull<DbElementExpression>(expression, "expression");
			Func<DbExpression, DbExpression> callback;
			if (expression.IsSinglePropertyUnwrapped)
			{
				callback = new Func<DbExpression, DbExpression>(DbExpressionBuilder.CreateElementExpressionUnwrapSingleProperty);
			}
			else
			{
				callback = new Func<DbExpression, DbExpression>(DbExpressionBuilder.Element);
			}
			return this.VisitUnary(expression, callback);
		}

		// Token: 0x06003771 RID: 14193 RVA: 0x000D362D File Offset: 0x000D182D
		public override DbExpression Visit(DbIsEmptyExpression expression)
		{
			EntityUtil.CheckArgumentNull<DbIsEmptyExpression>(expression, "expression");
			return this.VisitUnary(expression, new Func<DbExpression, DbExpression>(DbExpressionBuilder.IsEmpty));
		}

		// Token: 0x06003772 RID: 14194 RVA: 0x000D364E File Offset: 0x000D184E
		public override DbExpression Visit(DbUnionAllExpression expression)
		{
			EntityUtil.CheckArgumentNull<DbUnionAllExpression>(expression, "expression");
			return this.VisitBinary(expression, new Func<DbExpression, DbExpression, DbExpression>(DbExpressionBuilder.UnionAll));
		}

		// Token: 0x06003773 RID: 14195 RVA: 0x000D366F File Offset: 0x000D186F
		public override DbExpression Visit(DbIntersectExpression expression)
		{
			EntityUtil.CheckArgumentNull<DbIntersectExpression>(expression, "expression");
			return this.VisitBinary(expression, new Func<DbExpression, DbExpression, DbExpression>(DbExpressionBuilder.Intersect));
		}

		// Token: 0x06003774 RID: 14196 RVA: 0x000D3690 File Offset: 0x000D1890
		public override DbExpression Visit(DbExceptExpression expression)
		{
			EntityUtil.CheckArgumentNull<DbExceptExpression>(expression, "expression");
			return this.VisitBinary(expression, new Func<DbExpression, DbExpression, DbExpression>(DbExpressionBuilder.Except));
		}

		// Token: 0x06003775 RID: 14197 RVA: 0x000D36B1 File Offset: 0x000D18B1
		public override DbExpression Visit(DbTreatExpression expression)
		{
			EntityUtil.CheckArgumentNull<DbTreatExpression>(expression, "expression");
			return this.VisitTypeUnary(expression, expression.ResultType, new Func<DbExpression, TypeUsage, DbExpression>(DbExpressionBuilder.TreatAs));
		}

		// Token: 0x06003776 RID: 14198 RVA: 0x000D36D8 File Offset: 0x000D18D8
		public override DbExpression Visit(DbIsOfExpression expression)
		{
			EntityUtil.CheckArgumentNull<DbIsOfExpression>(expression, "expression");
			if (expression.ExpressionKind == DbExpressionKind.IsOfOnly)
			{
				return this.VisitTypeUnary(expression, expression.OfType, new Func<DbExpression, TypeUsage, DbExpression>(DbExpressionBuilder.IsOfOnly));
			}
			return this.VisitTypeUnary(expression, expression.OfType, new Func<DbExpression, TypeUsage, DbExpression>(DbExpressionBuilder.IsOf));
		}

		// Token: 0x06003777 RID: 14199 RVA: 0x000D372E File Offset: 0x000D192E
		public override DbExpression Visit(DbCastExpression expression)
		{
			EntityUtil.CheckArgumentNull<DbCastExpression>(expression, "expression");
			return this.VisitTypeUnary(expression, expression.ResultType, new Func<DbExpression, TypeUsage, DbExpression>(DbExpressionBuilder.CastTo));
		}

		// Token: 0x06003778 RID: 14200 RVA: 0x000D3758 File Offset: 0x000D1958
		public override DbExpression Visit(DbCaseExpression expression)
		{
			EntityUtil.CheckArgumentNull<DbCaseExpression>(expression, "expression");
			DbExpression dbExpression = expression;
			IList<DbExpression> list = this.VisitExpressionList(expression.When);
			IList<DbExpression> list2 = this.VisitExpressionList(expression.Then);
			DbExpression dbExpression2 = this.VisitExpression(expression.Else);
			if (expression.When != list || expression.Then != list2 || expression.Else != dbExpression2)
			{
				dbExpression = DbExpressionBuilder.Case(list, list2, dbExpression2);
			}
			this.NotifyIfChanged(expression, dbExpression);
			return dbExpression;
		}

		// Token: 0x06003779 RID: 14201 RVA: 0x000D37C8 File Offset: 0x000D19C8
		public override DbExpression Visit(DbOfTypeExpression expression)
		{
			EntityUtil.CheckArgumentNull<DbOfTypeExpression>(expression, "expression");
			if (expression.ExpressionKind == DbExpressionKind.OfTypeOnly)
			{
				return this.VisitTypeUnary(expression, expression.OfType, new Func<DbExpression, TypeUsage, DbExpression>(DbExpressionBuilder.OfTypeOnly));
			}
			return this.VisitTypeUnary(expression, expression.OfType, new Func<DbExpression, TypeUsage, DbExpression>(DbExpressionBuilder.OfType));
		}

		// Token: 0x0600377A RID: 14202 RVA: 0x000D3820 File Offset: 0x000D1A20
		public override DbExpression Visit(DbNewInstanceExpression expression)
		{
			EntityUtil.CheckArgumentNull<DbNewInstanceExpression>(expression, "expression");
			DbExpression dbExpression = expression;
			TypeUsage typeUsage = this.VisitTypeUsage(expression.ResultType);
			IList<DbExpression> list = this.VisitExpressionList(expression.Arguments);
			bool flag = expression.ResultType == typeUsage && expression.Arguments == list;
			if (expression.HasRelatedEntityReferences)
			{
				IList<DbRelatedEntityRef> list2 = this.VisitList<DbRelatedEntityRef>(expression.RelatedEntityReferences, new Func<DbRelatedEntityRef, DbRelatedEntityRef>(this.VisitRelatedEntityRef));
				if (!flag || expression.RelatedEntityReferences != list2)
				{
					dbExpression = DbExpressionBuilder.CreateNewEntityWithRelationshipsExpression((EntityType)typeUsage.EdmType, list, list2);
				}
			}
			else if (!flag)
			{
				dbExpression = typeUsage.New(list.ToArray<DbExpression>());
			}
			this.NotifyIfChanged(expression, dbExpression);
			return dbExpression;
		}

		// Token: 0x0600377B RID: 14203 RVA: 0x000D38CC File Offset: 0x000D1ACC
		public override DbExpression Visit(DbRefExpression expression)
		{
			EntityUtil.CheckArgumentNull<DbRefExpression>(expression, "expression");
			DbExpression dbExpression = expression;
			EntityType entityType = (EntityType)TypeHelpers.GetEdmType<RefType>(expression.ResultType).ElementType;
			DbExpression dbExpression2 = this.VisitExpression(expression.Argument);
			EntityType entityType2 = (EntityType)this.VisitType(entityType);
			EntitySet entitySet = (EntitySet)this.VisitEntitySet(expression.EntitySet);
			if (expression.Argument != dbExpression2 || entityType != entityType2 || expression.EntitySet != entitySet)
			{
				dbExpression = entitySet.RefFromKey(dbExpression2, entityType2);
			}
			this.NotifyIfChanged(expression, dbExpression);
			return dbExpression;
		}

		// Token: 0x0600377C RID: 14204 RVA: 0x000D3954 File Offset: 0x000D1B54
		public override DbExpression Visit(DbRelationshipNavigationExpression expression)
		{
			EntityUtil.CheckArgumentNull<DbRelationshipNavigationExpression>(expression, "expression");
			DbExpression dbExpression = expression;
			RelationshipEndMember relationshipEndMember;
			RelationshipEndMember relationshipEndMember2;
			this.VisitRelationshipEnds(expression.NavigateFrom, expression.NavigateTo, out relationshipEndMember, out relationshipEndMember2);
			DbExpression dbExpression2 = this.VisitExpression(expression.NavigationSource);
			if (expression.NavigateFrom != relationshipEndMember || expression.NavigateTo != relationshipEndMember2 || expression.NavigationSource != dbExpression2)
			{
				dbExpression = dbExpression2.Navigate(relationshipEndMember, relationshipEndMember2);
			}
			this.NotifyIfChanged(expression, dbExpression);
			return dbExpression;
		}

		// Token: 0x0600377D RID: 14205 RVA: 0x000D39BF File Offset: 0x000D1BBF
		public override DbExpression Visit(DbDerefExpression expression)
		{
			EntityUtil.CheckArgumentNull<DbDerefExpression>(expression, "expression");
			return this.VisitUnary(expression, new Func<DbExpression, DbExpression>(DbExpressionBuilder.Deref));
		}

		// Token: 0x0600377E RID: 14206 RVA: 0x000D39E0 File Offset: 0x000D1BE0
		public override DbExpression Visit(DbRefKeyExpression expression)
		{
			EntityUtil.CheckArgumentNull<DbRefKeyExpression>(expression, "expression");
			return this.VisitUnary(expression, new Func<DbExpression, DbExpression>(DbExpressionBuilder.GetRefKey));
		}

		// Token: 0x0600377F RID: 14207 RVA: 0x000D3A01 File Offset: 0x000D1C01
		public override DbExpression Visit(DbEntityRefExpression expression)
		{
			EntityUtil.CheckArgumentNull<DbEntityRefExpression>(expression, "expression");
			return this.VisitUnary(expression, new Func<DbExpression, DbExpression>(DbExpressionBuilder.GetEntityRef));
		}

		// Token: 0x06003780 RID: 14208 RVA: 0x000D3A24 File Offset: 0x000D1C24
		public override DbExpression Visit(DbScanExpression expression)
		{
			EntityUtil.CheckArgumentNull<DbScanExpression>(expression, "expression");
			DbExpression dbExpression = expression;
			EntitySetBase entitySetBase = this.VisitEntitySet(expression.Target);
			if (expression.Target != entitySetBase)
			{
				dbExpression = entitySetBase.Scan();
			}
			this.NotifyIfChanged(expression, dbExpression);
			return dbExpression;
		}

		// Token: 0x06003781 RID: 14209 RVA: 0x000D3A68 File Offset: 0x000D1C68
		public override DbExpression Visit(DbFilterExpression expression)
		{
			EntityUtil.CheckArgumentNull<DbFilterExpression>(expression, "expression");
			DbExpression dbExpression = expression;
			DbExpressionBinding dbExpressionBinding = this.VisitExpressionBindingEnterScope(expression.Input);
			DbExpression dbExpression2 = this.VisitExpression(expression.Predicate);
			this.ExitScope();
			if (expression.Input != dbExpressionBinding || expression.Predicate != dbExpression2)
			{
				dbExpression = dbExpressionBinding.Filter(dbExpression2);
			}
			this.NotifyIfChanged(expression, dbExpression);
			return dbExpression;
		}

		// Token: 0x06003782 RID: 14210 RVA: 0x000D3AC8 File Offset: 0x000D1CC8
		public override DbExpression Visit(DbProjectExpression expression)
		{
			EntityUtil.CheckArgumentNull<DbProjectExpression>(expression, "expression");
			DbExpression dbExpression = expression;
			DbExpressionBinding dbExpressionBinding = this.VisitExpressionBindingEnterScope(expression.Input);
			DbExpression dbExpression2 = this.VisitExpression(expression.Projection);
			this.ExitScope();
			if (expression.Input != dbExpressionBinding || expression.Projection != dbExpression2)
			{
				dbExpression = dbExpressionBinding.Project(dbExpression2);
			}
			this.NotifyIfChanged(expression, dbExpression);
			return dbExpression;
		}

		// Token: 0x06003783 RID: 14211 RVA: 0x000D3B28 File Offset: 0x000D1D28
		public override DbExpression Visit(DbCrossJoinExpression expression)
		{
			EntityUtil.CheckArgumentNull<DbCrossJoinExpression>(expression, "expression");
			DbExpression dbExpression = expression;
			IList<DbExpressionBinding> list = this.VisitExpressionBindingList(expression.Inputs);
			if (expression.Inputs != list)
			{
				dbExpression = DbExpressionBuilder.CrossJoin(list);
			}
			this.NotifyIfChanged(expression, dbExpression);
			return dbExpression;
		}

		// Token: 0x06003784 RID: 14212 RVA: 0x000D3B6C File Offset: 0x000D1D6C
		public override DbExpression Visit(DbJoinExpression expression)
		{
			EntityUtil.CheckArgumentNull<DbJoinExpression>(expression, "expression");
			DbExpression dbExpression = expression;
			DbExpressionBinding dbExpressionBinding = this.VisitExpressionBinding(expression.Left);
			DbExpressionBinding dbExpressionBinding2 = this.VisitExpressionBinding(expression.Right);
			this.EnterScope(new DbVariableReferenceExpression[]
			{
				dbExpressionBinding.Variable,
				dbExpressionBinding2.Variable
			});
			DbExpression dbExpression2 = this.VisitExpression(expression.JoinCondition);
			this.ExitScope();
			if (expression.Left != dbExpressionBinding || expression.Right != dbExpressionBinding2 || expression.JoinCondition != dbExpression2)
			{
				if (DbExpressionKind.InnerJoin == expression.ExpressionKind)
				{
					dbExpression = dbExpressionBinding.InnerJoin(dbExpressionBinding2, dbExpression2);
				}
				else if (DbExpressionKind.LeftOuterJoin == expression.ExpressionKind)
				{
					dbExpression = dbExpressionBinding.LeftOuterJoin(dbExpressionBinding2, dbExpression2);
				}
				else
				{
					dbExpression = dbExpressionBinding.FullOuterJoin(dbExpressionBinding2, dbExpression2);
				}
			}
			this.NotifyIfChanged(expression, dbExpression);
			return dbExpression;
		}

		// Token: 0x06003785 RID: 14213 RVA: 0x000D3C2C File Offset: 0x000D1E2C
		public override DbExpression Visit(DbApplyExpression expression)
		{
			EntityUtil.CheckArgumentNull<DbApplyExpression>(expression, "expression");
			DbExpression dbExpression = expression;
			DbExpressionBinding dbExpressionBinding = this.VisitExpressionBindingEnterScope(expression.Input);
			DbExpressionBinding dbExpressionBinding2 = this.VisitExpressionBinding(expression.Apply);
			this.ExitScope();
			if (expression.Input != dbExpressionBinding || expression.Apply != dbExpressionBinding2)
			{
				if (DbExpressionKind.CrossApply == expression.ExpressionKind)
				{
					dbExpression = dbExpressionBinding.CrossApply(dbExpressionBinding2);
				}
				else
				{
					dbExpression = dbExpressionBinding.OuterApply(dbExpressionBinding2);
				}
			}
			this.NotifyIfChanged(expression, dbExpression);
			return dbExpression;
		}

		// Token: 0x06003786 RID: 14214 RVA: 0x000D3CA0 File Offset: 0x000D1EA0
		public override DbExpression Visit(DbGroupByExpression expression)
		{
			EntityUtil.CheckArgumentNull<DbGroupByExpression>(expression, "expression");
			DbExpression dbExpression = expression;
			DbGroupExpressionBinding dbGroupExpressionBinding = this.VisitGroupExpressionBinding(expression.Input);
			this.EnterScope(new DbVariableReferenceExpression[]
			{
				dbGroupExpressionBinding.Variable
			});
			IList<DbExpression> list = this.VisitExpressionList(expression.Keys);
			this.ExitScope();
			this.EnterScope(new DbVariableReferenceExpression[]
			{
				dbGroupExpressionBinding.GroupVariable
			});
			IList<DbAggregate> list2 = this.VisitList<DbAggregate>(expression.Aggregates, new Func<DbAggregate, DbAggregate>(this.VisitAggregate));
			this.ExitScope();
			if (expression.Input != dbGroupExpressionBinding || expression.Keys != list || expression.Aggregates != list2)
			{
				RowType edmType = TypeHelpers.GetEdmType<RowType>(TypeHelpers.GetEdmType<CollectionType>(expression.ResultType).TypeUsage);
				List<KeyValuePair<string, DbExpression>> keys = (from p in edmType.Properties.Take(list.Count)
				select p.Name).Zip(list).ToList<KeyValuePair<string, DbExpression>>();
				List<KeyValuePair<string, DbAggregate>> aggregates = (from p in edmType.Properties.Skip(list.Count)
				select p.Name).Zip(list2).ToList<KeyValuePair<string, DbAggregate>>();
				dbExpression = dbGroupExpressionBinding.GroupBy(keys, aggregates);
			}
			this.NotifyIfChanged(expression, dbExpression);
			return dbExpression;
		}

		// Token: 0x06003787 RID: 14215 RVA: 0x000D3DF4 File Offset: 0x000D1FF4
		public override DbExpression Visit(DbSkipExpression expression)
		{
			EntityUtil.CheckArgumentNull<DbSkipExpression>(expression, "expression");
			DbExpression dbExpression = expression;
			DbExpressionBinding dbExpressionBinding = this.VisitExpressionBindingEnterScope(expression.Input);
			IList<DbSortClause> list = this.VisitSortOrder(expression.SortOrder);
			this.ExitScope();
			DbExpression dbExpression2 = this.VisitExpression(expression.Count);
			if (expression.Input != dbExpressionBinding || expression.SortOrder != list || expression.Count != dbExpression2)
			{
				dbExpression = dbExpressionBinding.Skip(list, dbExpression2);
			}
			this.NotifyIfChanged(expression, dbExpression);
			return dbExpression;
		}

		// Token: 0x06003788 RID: 14216 RVA: 0x000D3E6C File Offset: 0x000D206C
		public override DbExpression Visit(DbSortExpression expression)
		{
			EntityUtil.CheckArgumentNull<DbSortExpression>(expression, "expression");
			DbExpression dbExpression = expression;
			DbExpressionBinding dbExpressionBinding = this.VisitExpressionBindingEnterScope(expression.Input);
			IList<DbSortClause> list = this.VisitSortOrder(expression.SortOrder);
			this.ExitScope();
			if (expression.Input != dbExpressionBinding || expression.SortOrder != list)
			{
				dbExpression = dbExpressionBinding.Sort(list);
			}
			this.NotifyIfChanged(expression, dbExpression);
			return dbExpression;
		}

		// Token: 0x06003789 RID: 14217 RVA: 0x000D3ECC File Offset: 0x000D20CC
		public override DbExpression Visit(DbQuantifierExpression expression)
		{
			EntityUtil.CheckArgumentNull<DbQuantifierExpression>(expression, "expression");
			DbExpression dbExpression = expression;
			DbExpressionBinding dbExpressionBinding = this.VisitExpressionBindingEnterScope(expression.Input);
			DbExpression dbExpression2 = this.VisitExpression(expression.Predicate);
			this.ExitScope();
			if (expression.Input != dbExpressionBinding || expression.Predicate != dbExpression2)
			{
				if (expression.ExpressionKind == DbExpressionKind.All)
				{
					dbExpression = dbExpressionBinding.All(dbExpression2);
				}
				else
				{
					dbExpression = dbExpressionBinding.Any(dbExpression2);
				}
			}
			this.NotifyIfChanged(expression, dbExpression);
			return dbExpression;
		}

		// Token: 0x0400183B RID: 6203
		private readonly Dictionary<DbVariableReferenceExpression, DbVariableReferenceExpression> varMappings = new Dictionary<DbVariableReferenceExpression, DbVariableReferenceExpression>();
	}
}
