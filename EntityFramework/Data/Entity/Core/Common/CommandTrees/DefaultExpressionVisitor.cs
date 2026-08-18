using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x02000032 RID: 50
	[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
	public class DefaultExpressionVisitor : DbExpressionVisitor<DbExpression>
	{
		// Token: 0x06000287 RID: 647 RVA: 0x0000E580 File Offset: 0x0000C780
		protected DefaultExpressionVisitor()
		{
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000E593 File Offset: 0x0000C793
		protected virtual void OnExpressionReplaced(DbExpression oldExpression, DbExpression newExpression)
		{
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000E595 File Offset: 0x0000C795
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "toVar")]
		protected virtual void OnVariableRebound(DbVariableReferenceExpression fromVarRef, DbVariableReferenceExpression toVarRef)
		{
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000E597 File Offset: 0x0000C797
		protected virtual void OnEnterScope(IEnumerable<DbVariableReferenceExpression> scopeVariables)
		{
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000E599 File Offset: 0x0000C799
		protected virtual void OnExitScope()
		{
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000E59C File Offset: 0x0000C79C
		protected virtual DbExpression VisitExpression(DbExpression expression)
		{
			DbExpression result = null;
			if (expression != null)
			{
				result = expression.Accept<DbExpression>(this);
			}
			return result;
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000E5B7 File Offset: 0x0000C7B7
		protected virtual IList<DbExpression> VisitExpressionList(IList<DbExpression> list)
		{
			return DefaultExpressionVisitor.VisitList<DbExpression>(list, new Func<DbExpression, DbExpression>(this.VisitExpression));
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000E5CC File Offset: 0x0000C7CC
		protected virtual DbExpressionBinding VisitExpressionBinding(DbExpressionBinding binding)
		{
			DbExpressionBinding dbExpressionBinding = binding;
			if (binding != null)
			{
				DbExpression dbExpression = this.VisitExpression(binding.Expression);
				if (!object.ReferenceEquals(binding.Expression, dbExpression))
				{
					dbExpressionBinding = dbExpression.BindAs(binding.VariableName);
					this.RebindVariable(binding.Variable, dbExpressionBinding.Variable);
				}
			}
			return dbExpressionBinding;
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000E619 File Offset: 0x0000C819
		protected virtual IList<DbExpressionBinding> VisitExpressionBindingList(IList<DbExpressionBinding> list)
		{
			return DefaultExpressionVisitor.VisitList<DbExpressionBinding>(list, new Func<DbExpressionBinding, DbExpressionBinding>(this.VisitExpressionBinding));
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000E630 File Offset: 0x0000C830
		protected virtual DbGroupExpressionBinding VisitGroupExpressionBinding(DbGroupExpressionBinding binding)
		{
			DbGroupExpressionBinding dbGroupExpressionBinding = binding;
			if (binding != null)
			{
				DbExpression dbExpression = this.VisitExpression(binding.Expression);
				if (!object.ReferenceEquals(binding.Expression, dbExpression))
				{
					dbGroupExpressionBinding = dbExpression.GroupBindAs(binding.VariableName, binding.GroupVariableName);
					this.RebindVariable(binding.Variable, dbGroupExpressionBinding.Variable);
					this.RebindVariable(binding.GroupVariable, dbGroupExpressionBinding.GroupVariable);
				}
			}
			return dbGroupExpressionBinding;
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000E698 File Offset: 0x0000C898
		protected virtual DbSortClause VisitSortClause(DbSortClause clause)
		{
			DbSortClause result = clause;
			if (clause != null)
			{
				DbExpression dbExpression = this.VisitExpression(clause.Expression);
				if (!object.ReferenceEquals(clause.Expression, dbExpression))
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

		// Token: 0x06000292 RID: 658 RVA: 0x0000E70F File Offset: 0x0000C90F
		protected virtual IList<DbSortClause> VisitSortOrder(IList<DbSortClause> sortOrder)
		{
			return DefaultExpressionVisitor.VisitList<DbSortClause>(sortOrder, new Func<DbSortClause, DbSortClause>(this.VisitSortClause));
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000E724 File Offset: 0x0000C924
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

		// Token: 0x06000294 RID: 660 RVA: 0x0000E754 File Offset: 0x0000C954
		protected virtual DbFunctionAggregate VisitFunctionAggregate(DbFunctionAggregate aggregate)
		{
			DbFunctionAggregate result = aggregate;
			if (aggregate != null)
			{
				EdmFunction edmFunction = this.VisitFunction(aggregate.Function);
				IList<DbExpression> list = this.VisitExpressionList(aggregate.Arguments);
				if (!object.ReferenceEquals(aggregate.Function, edmFunction) || !object.ReferenceEquals(aggregate.Arguments, list))
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

		// Token: 0x06000295 RID: 661 RVA: 0x0000E7C4 File Offset: 0x0000C9C4
		protected virtual DbGroupAggregate VisitGroupAggregate(DbGroupAggregate aggregate)
		{
			DbGroupAggregate result = aggregate;
			if (aggregate != null)
			{
				IList<DbExpression> list = this.VisitExpressionList(aggregate.Arguments);
				if (!object.ReferenceEquals(aggregate.Arguments, list))
				{
					result = DbExpressionBuilder.GroupAggregate(list[0]);
				}
			}
			return result;
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0000E838 File Offset: 0x0000CA38
		protected virtual DbLambda VisitLambda(DbLambda lambda)
		{
			Check.NotNull<DbLambda>(lambda, "lambda");
			DbLambda result = lambda;
			IList<DbVariableReferenceExpression> list = DefaultExpressionVisitor.VisitList<DbVariableReferenceExpression>(lambda.Variables, delegate(DbVariableReferenceExpression varRef)
			{
				TypeUsage typeUsage = this.VisitTypeUsage(varRef.ResultType);
				if (!object.ReferenceEquals(varRef.ResultType, typeUsage))
				{
					return typeUsage.Variable(varRef.VariableName);
				}
				return varRef;
			});
			this.EnterScope(list.ToArray<DbVariableReferenceExpression>());
			DbExpression dbExpression = this.VisitExpression(lambda.Body);
			this.ExitScope();
			if (!object.ReferenceEquals(lambda.Variables, list) || !object.ReferenceEquals(lambda.Body, dbExpression))
			{
				result = DbExpressionBuilder.Lambda(dbExpression, list);
			}
			return result;
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0000E8AF File Offset: 0x0000CAAF
		protected virtual EdmType VisitType(EdmType type)
		{
			return type;
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0000E8B2 File Offset: 0x0000CAB2
		protected virtual TypeUsage VisitTypeUsage(TypeUsage type)
		{
			return type;
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0000E8B5 File Offset: 0x0000CAB5
		protected virtual EntitySetBase VisitEntitySet(EntitySetBase entitySet)
		{
			return entitySet;
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000E8B8 File Offset: 0x0000CAB8
		protected virtual EdmFunction VisitFunction(EdmFunction functionMetadata)
		{
			return functionMetadata;
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0000E8BB File Offset: 0x0000CABB
		private void NotifyIfChanged(DbExpression originalExpression, DbExpression newExpression)
		{
			if (!object.ReferenceEquals(originalExpression, newExpression))
			{
				this.OnExpressionReplaced(originalExpression, newExpression);
			}
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0000E8D0 File Offset: 0x0000CAD0
		private static IList<TElement> VisitList<TElement>(IList<TElement> list, Func<TElement, TElement> map)
		{
			IList<TElement> result = list;
			if (list != null)
			{
				List<TElement> list2 = null;
				for (int i = 0; i < list.Count; i++)
				{
					TElement telement = map(list[i]);
					if (list2 == null && !object.ReferenceEquals(list[i], telement))
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

		// Token: 0x0600029D RID: 669 RVA: 0x0000E934 File Offset: 0x0000CB34
		private DbExpression VisitUnary(DbUnaryExpression expression, Func<DbExpression, DbExpression> callback)
		{
			DbExpression dbExpression = expression;
			DbExpression dbExpression2 = this.VisitExpression(expression.Argument);
			if (!object.ReferenceEquals(expression.Argument, dbExpression2))
			{
				dbExpression = callback(dbExpression2);
			}
			this.NotifyIfChanged(expression, dbExpression);
			return dbExpression;
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000E970 File Offset: 0x0000CB70
		private DbExpression VisitTypeUnary(DbUnaryExpression expression, TypeUsage type, Func<DbExpression, TypeUsage, DbExpression> callback)
		{
			DbExpression dbExpression = expression;
			DbExpression dbExpression2 = this.VisitExpression(expression.Argument);
			TypeUsage typeUsage = this.VisitTypeUsage(type);
			if (!object.ReferenceEquals(expression.Argument, dbExpression2) || !object.ReferenceEquals(type, typeUsage))
			{
				dbExpression = callback(dbExpression2, typeUsage);
			}
			this.NotifyIfChanged(expression, dbExpression);
			return dbExpression;
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000E9C0 File Offset: 0x0000CBC0
		private DbExpression VisitBinary(DbBinaryExpression expression, Func<DbExpression, DbExpression, DbExpression> callback)
		{
			DbExpression dbExpression = expression;
			DbExpression dbExpression2 = this.VisitExpression(expression.Left);
			DbExpression dbExpression3 = this.VisitExpression(expression.Right);
			if (!object.ReferenceEquals(expression.Left, dbExpression2) || !object.ReferenceEquals(expression.Right, dbExpression3))
			{
				dbExpression = callback(dbExpression2, dbExpression3);
			}
			this.NotifyIfChanged(expression, dbExpression);
			return dbExpression;
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000EA18 File Offset: 0x0000CC18
		private DbRelatedEntityRef VisitRelatedEntityRef(DbRelatedEntityRef entityRef)
		{
			RelationshipEndMember relationshipEndMember;
			RelationshipEndMember relationshipEndMember2;
			this.VisitRelationshipEnds(entityRef.SourceEnd, entityRef.TargetEnd, out relationshipEndMember, out relationshipEndMember2);
			DbExpression dbExpression = this.VisitExpression(entityRef.TargetEntityReference);
			if (!object.ReferenceEquals(entityRef.SourceEnd, relationshipEndMember) || !object.ReferenceEquals(entityRef.TargetEnd, relationshipEndMember2) || !object.ReferenceEquals(entityRef.TargetEntityReference, dbExpression))
			{
				return DbExpressionBuilder.CreateRelatedEntityRef(relationshipEndMember, relationshipEndMember2, dbExpression);
			}
			return entityRef;
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000EA7C File Offset: 0x0000CC7C
		private void VisitRelationshipEnds(RelationshipEndMember source, RelationshipEndMember target, out RelationshipEndMember newSource, out RelationshipEndMember newTarget)
		{
			RelationshipType relationshipType = (RelationshipType)this.VisitType(target.DeclaringType);
			newSource = relationshipType.RelationshipEndMembers[source.Name];
			newTarget = relationshipType.RelationshipEndMembers[target.Name];
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0000EAC4 File Offset: 0x0000CCC4
		private DbExpression VisitTerminal(DbExpression expression, Func<TypeUsage, DbExpression> reconstructor)
		{
			DbExpression dbExpression = expression;
			TypeUsage typeUsage = this.VisitTypeUsage(expression.ResultType);
			if (!object.ReferenceEquals(expression.ResultType, typeUsage))
			{
				dbExpression = reconstructor(typeUsage);
			}
			this.NotifyIfChanged(expression, dbExpression);
			return dbExpression;
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000EB00 File Offset: 0x0000CD00
		private void RebindVariable(DbVariableReferenceExpression from, DbVariableReferenceExpression to)
		{
			if (!from.VariableName.Equals(to.VariableName, StringComparison.Ordinal) || !object.ReferenceEquals(from.ResultType.EdmType, to.ResultType.EdmType) || !from.ResultType.EdmEquals(to.ResultType))
			{
				this.varMappings[from] = to;
				this.OnVariableRebound(from, to);
			}
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000EB68 File Offset: 0x0000CD68
		private DbExpressionBinding VisitExpressionBindingEnterScope(DbExpressionBinding binding)
		{
			DbExpressionBinding dbExpressionBinding = this.VisitExpressionBinding(binding);
			this.OnEnterScope(new DbVariableReferenceExpression[]
			{
				dbExpressionBinding.Variable
			});
			return dbExpressionBinding;
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000EB95 File Offset: 0x0000CD95
		private void EnterScope(params DbVariableReferenceExpression[] scopeVars)
		{
			this.OnEnterScope(scopeVars);
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0000EB9E File Offset: 0x0000CD9E
		private void ExitScope()
		{
			this.OnExitScope();
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0000EBA6 File Offset: 0x0000CDA6
		public override DbExpression Visit(DbExpression expression)
		{
			Check.NotNull<DbExpression>(expression, "expression");
			throw new NotSupportedException(Strings.Cqt_General_UnsupportedExpression(expression.GetType().FullName));
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000EBE4 File Offset: 0x0000CDE4
		public override DbExpression Visit(DbConstantExpression expression)
		{
			Check.NotNull<DbConstantExpression>(expression, "expression");
			return this.VisitTerminal(expression, (TypeUsage newType) => newType.Constant(expression.GetValue()));
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000EC27 File Offset: 0x0000CE27
		public override DbExpression Visit(DbNullExpression expression)
		{
			Check.NotNull<DbNullExpression>(expression, "expression");
			return this.VisitTerminal(expression, new Func<TypeUsage, DbExpression>(DbExpressionBuilder.Null));
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0000EC48 File Offset: 0x0000CE48
		public override DbExpression Visit(DbVariableReferenceExpression expression)
		{
			Check.NotNull<DbVariableReferenceExpression>(expression, "expression");
			DbExpression dbExpression = expression;
			DbVariableReferenceExpression dbVariableReferenceExpression;
			if (this.varMappings.TryGetValue(expression, out dbVariableReferenceExpression))
			{
				dbExpression = dbVariableReferenceExpression;
			}
			this.NotifyIfChanged(expression, dbExpression);
			return dbExpression;
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0000EC9C File Offset: 0x0000CE9C
		public override DbExpression Visit(DbParameterReferenceExpression expression)
		{
			Check.NotNull<DbParameterReferenceExpression>(expression, "expression");
			return this.VisitTerminal(expression, (TypeUsage newType) => newType.Parameter(expression.ParameterName));
		}

		// Token: 0x060002AC RID: 684 RVA: 0x0000ECE0 File Offset: 0x0000CEE0
		public override DbExpression Visit(DbFunctionExpression expression)
		{
			Check.NotNull<DbFunctionExpression>(expression, "expression");
			DbExpression dbExpression = expression;
			IList<DbExpression> list = this.VisitExpressionList(expression.Arguments);
			EdmFunction edmFunction = this.VisitFunction(expression.Function);
			if (!object.ReferenceEquals(expression.Arguments, list) || !object.ReferenceEquals(expression.Function, edmFunction))
			{
				dbExpression = edmFunction.Invoke(list);
			}
			this.NotifyIfChanged(expression, dbExpression);
			return dbExpression;
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0000ED44 File Offset: 0x0000CF44
		public override DbExpression Visit(DbLambdaExpression expression)
		{
			Check.NotNull<DbLambdaExpression>(expression, "expression");
			DbExpression dbExpression = expression;
			IList<DbExpression> list = this.VisitExpressionList(expression.Arguments);
			DbLambda dbLambda = this.VisitLambda(expression.Lambda);
			if (!object.ReferenceEquals(expression.Arguments, list) || !object.ReferenceEquals(expression.Lambda, dbLambda))
			{
				dbExpression = dbLambda.Invoke(list);
			}
			this.NotifyIfChanged(expression, dbExpression);
			return dbExpression;
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0000EDA8 File Offset: 0x0000CFA8
		public override DbExpression Visit(DbPropertyExpression expression)
		{
			Check.NotNull<DbPropertyExpression>(expression, "expression");
			DbExpression dbExpression = expression;
			DbExpression dbExpression2 = this.VisitExpression(expression.Instance);
			if (!object.ReferenceEquals(expression.Instance, dbExpression2))
			{
				dbExpression = dbExpression2.Property(expression.Property.Name);
			}
			this.NotifyIfChanged(expression, dbExpression);
			return dbExpression;
		}

		// Token: 0x060002AF RID: 687 RVA: 0x0000EDFC File Offset: 0x0000CFFC
		public override DbExpression Visit(DbComparisonExpression expression)
		{
			Check.NotNull<DbComparisonExpression>(expression, "expression");
			DbExpressionKind expressionKind = expression.ExpressionKind;
			if (expressionKind <= DbExpressionKind.GreaterThanOrEquals)
			{
				if (expressionKind == DbExpressionKind.Equals)
				{
					return this.VisitBinary(expression, new Func<DbExpression, DbExpression, DbExpression>(DbExpressionBuilder.Equal));
				}
				switch (expressionKind)
				{
				case DbExpressionKind.GreaterThan:
					return this.VisitBinary(expression, new Func<DbExpression, DbExpression, DbExpression>(DbExpressionBuilder.GreaterThan));
				case DbExpressionKind.GreaterThanOrEquals:
					return this.VisitBinary(expression, new Func<DbExpression, DbExpression, DbExpression>(DbExpressionBuilder.GreaterThanOrEqual));
				}
			}
			else
			{
				switch (expressionKind)
				{
				case DbExpressionKind.LessThan:
					return this.VisitBinary(expression, new Func<DbExpression, DbExpression, DbExpression>(DbExpressionBuilder.LessThan));
				case DbExpressionKind.LessThanOrEquals:
					return this.VisitBinary(expression, new Func<DbExpression, DbExpression, DbExpression>(DbExpressionBuilder.LessThanOrEqual));
				default:
					if (expressionKind == DbExpressionKind.NotEquals)
					{
						return this.VisitBinary(expression, new Func<DbExpression, DbExpression, DbExpression>(DbExpressionBuilder.NotEqual));
					}
					break;
				}
			}
			throw new NotSupportedException();
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0000EED4 File Offset: 0x0000D0D4
		public override DbExpression Visit(DbLikeExpression expression)
		{
			Check.NotNull<DbLikeExpression>(expression, "expression");
			DbExpression dbExpression = expression;
			DbExpression dbExpression2 = this.VisitExpression(expression.Argument);
			DbExpression dbExpression3 = this.VisitExpression(expression.Pattern);
			DbExpression dbExpression4 = this.VisitExpression(expression.Escape);
			if (!object.ReferenceEquals(expression.Argument, dbExpression2) || !object.ReferenceEquals(expression.Pattern, dbExpression3) || !object.ReferenceEquals(expression.Escape, dbExpression4))
			{
				dbExpression = dbExpression2.Like(dbExpression3, dbExpression4);
			}
			this.NotifyIfChanged(expression, dbExpression);
			return dbExpression;
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x0000EF54 File Offset: 0x0000D154
		public override DbExpression Visit(DbLimitExpression expression)
		{
			Check.NotNull<DbLimitExpression>(expression, "expression");
			DbExpression dbExpression = expression;
			DbExpression dbExpression2 = this.VisitExpression(expression.Argument);
			DbExpression dbExpression3 = this.VisitExpression(expression.Limit);
			if (!object.ReferenceEquals(expression.Argument, dbExpression2) || !object.ReferenceEquals(expression.Limit, dbExpression3))
			{
				dbExpression = dbExpression2.Limit(dbExpression3);
			}
			this.NotifyIfChanged(expression, dbExpression);
			return dbExpression;
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0000EFB6 File Offset: 0x0000D1B6
		public override DbExpression Visit(DbIsNullExpression expression)
		{
			Check.NotNull<DbIsNullExpression>(expression, "expression");
			return this.VisitUnary(expression, new Func<DbExpression, DbExpression>(DbExpressionBuilder.IsNull));
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0000EFD8 File Offset: 0x0000D1D8
		public override DbExpression Visit(DbArithmeticExpression expression)
		{
			Check.NotNull<DbArithmeticExpression>(expression, "expression");
			DbExpression dbExpression = expression;
			IList<DbExpression> list = this.VisitExpressionList(expression.Arguments);
			if (!object.ReferenceEquals(expression.Arguments, list))
			{
				DbExpressionKind expressionKind = expression.ExpressionKind;
				if (expressionKind <= DbExpressionKind.Multiply)
				{
					if (expressionKind == DbExpressionKind.Divide)
					{
						dbExpression = list[0].Divide(list[1]);
						goto IL_E6;
					}
					switch (expressionKind)
					{
					case DbExpressionKind.Minus:
						dbExpression = list[0].Minus(list[1]);
						goto IL_E6;
					case DbExpressionKind.Modulo:
						dbExpression = list[0].Modulo(list[1]);
						goto IL_E6;
					case DbExpressionKind.Multiply:
						dbExpression = list[0].Multiply(list[1]);
						goto IL_E6;
					}
				}
				else
				{
					if (expressionKind == DbExpressionKind.Plus)
					{
						dbExpression = list[0].Plus(list[1]);
						goto IL_E6;
					}
					if (expressionKind == DbExpressionKind.UnaryMinus)
					{
						dbExpression = list[0].UnaryMinus();
						goto IL_E6;
					}
				}
				throw new NotSupportedException();
			}
			IL_E6:
			this.NotifyIfChanged(expression, dbExpression);
			return dbExpression;
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0000F0D4 File Offset: 0x0000D2D4
		public override DbExpression Visit(DbAndExpression expression)
		{
			Check.NotNull<DbAndExpression>(expression, "expression");
			return this.VisitBinary(expression, new Func<DbExpression, DbExpression, DbExpression>(DbExpressionBuilder.And));
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000F0F5 File Offset: 0x0000D2F5
		public override DbExpression Visit(DbOrExpression expression)
		{
			Check.NotNull<DbOrExpression>(expression, "expression");
			return this.VisitBinary(expression, new Func<DbExpression, DbExpression, DbExpression>(DbExpressionBuilder.Or));
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000F118 File Offset: 0x0000D318
		public override DbExpression Visit(DbInExpression expression)
		{
			Check.NotNull<DbInExpression>(expression, "expression");
			DbExpression dbExpression = expression;
			DbExpression dbExpression2 = this.VisitExpression(expression.Item);
			IList<DbExpression> list = this.VisitExpressionList(expression.List);
			if (!object.ReferenceEquals(expression.Item, dbExpression2) || !object.ReferenceEquals(expression.List, list))
			{
				dbExpression = DbExpressionBuilder.CreateInExpression(dbExpression2, list);
			}
			this.NotifyIfChanged(expression, dbExpression);
			return dbExpression;
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000F17A File Offset: 0x0000D37A
		public override DbExpression Visit(DbNotExpression expression)
		{
			Check.NotNull<DbNotExpression>(expression, "expression");
			return this.VisitUnary(expression, new Func<DbExpression, DbExpression>(DbExpressionBuilder.Not));
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0000F19B File Offset: 0x0000D39B
		public override DbExpression Visit(DbDistinctExpression expression)
		{
			Check.NotNull<DbDistinctExpression>(expression, "expression");
			return this.VisitUnary(expression, new Func<DbExpression, DbExpression>(DbExpressionBuilder.Distinct));
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0000F1BC File Offset: 0x0000D3BC
		public override DbExpression Visit(DbElementExpression expression)
		{
			Check.NotNull<DbElementExpression>(expression, "expression");
			return this.VisitUnary(expression, expression.IsSinglePropertyUnwrapped ? new Func<DbExpression, DbExpression>(DbExpressionBuilder.CreateElementExpressionUnwrapSingleProperty) : new Func<DbExpression, DbExpression>(DbExpressionBuilder.Element));
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0000F1F3 File Offset: 0x0000D3F3
		public override DbExpression Visit(DbIsEmptyExpression expression)
		{
			Check.NotNull<DbIsEmptyExpression>(expression, "expression");
			return this.VisitUnary(expression, new Func<DbExpression, DbExpression>(DbExpressionBuilder.IsEmpty));
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0000F214 File Offset: 0x0000D414
		public override DbExpression Visit(DbUnionAllExpression expression)
		{
			Check.NotNull<DbUnionAllExpression>(expression, "expression");
			return this.VisitBinary(expression, new Func<DbExpression, DbExpression, DbExpression>(DbExpressionBuilder.UnionAll));
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000F235 File Offset: 0x0000D435
		public override DbExpression Visit(DbIntersectExpression expression)
		{
			Check.NotNull<DbIntersectExpression>(expression, "expression");
			return this.VisitBinary(expression, new Func<DbExpression, DbExpression, DbExpression>(DbExpressionBuilder.Intersect));
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000F256 File Offset: 0x0000D456
		public override DbExpression Visit(DbExceptExpression expression)
		{
			Check.NotNull<DbExceptExpression>(expression, "expression");
			return this.VisitBinary(expression, new Func<DbExpression, DbExpression, DbExpression>(DbExpressionBuilder.Except));
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000F277 File Offset: 0x0000D477
		public override DbExpression Visit(DbTreatExpression expression)
		{
			Check.NotNull<DbTreatExpression>(expression, "expression");
			return this.VisitTypeUnary(expression, expression.ResultType, new Func<DbExpression, TypeUsage, DbExpression>(DbExpressionBuilder.TreatAs));
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0000F2A0 File Offset: 0x0000D4A0
		public override DbExpression Visit(DbIsOfExpression expression)
		{
			Check.NotNull<DbIsOfExpression>(expression, "expression");
			if (expression.ExpressionKind == DbExpressionKind.IsOfOnly)
			{
				return this.VisitTypeUnary(expression, expression.OfType, new Func<DbExpression, TypeUsage, DbExpression>(DbExpressionBuilder.IsOfOnly));
			}
			return this.VisitTypeUnary(expression, expression.OfType, new Func<DbExpression, TypeUsage, DbExpression>(DbExpressionBuilder.IsOf));
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000F2F6 File Offset: 0x0000D4F6
		public override DbExpression Visit(DbCastExpression expression)
		{
			Check.NotNull<DbCastExpression>(expression, "expression");
			return this.VisitTypeUnary(expression, expression.ResultType, new Func<DbExpression, TypeUsage, DbExpression>(DbExpressionBuilder.CastTo));
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0000F320 File Offset: 0x0000D520
		public override DbExpression Visit(DbCaseExpression expression)
		{
			Check.NotNull<DbCaseExpression>(expression, "expression");
			DbExpression dbExpression = expression;
			IList<DbExpression> list = this.VisitExpressionList(expression.When);
			IList<DbExpression> list2 = this.VisitExpressionList(expression.Then);
			DbExpression dbExpression2 = this.VisitExpression(expression.Else);
			if (!object.ReferenceEquals(expression.When, list) || !object.ReferenceEquals(expression.Then, list2) || !object.ReferenceEquals(expression.Else, dbExpression2))
			{
				dbExpression = DbExpressionBuilder.Case(list, list2, dbExpression2);
			}
			this.NotifyIfChanged(expression, dbExpression);
			return dbExpression;
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000F3A0 File Offset: 0x0000D5A0
		public override DbExpression Visit(DbOfTypeExpression expression)
		{
			Check.NotNull<DbOfTypeExpression>(expression, "expression");
			if (expression.ExpressionKind == DbExpressionKind.OfTypeOnly)
			{
				return this.VisitTypeUnary(expression, expression.OfType, new Func<DbExpression, TypeUsage, DbExpression>(DbExpressionBuilder.OfTypeOnly));
			}
			return this.VisitTypeUnary(expression, expression.OfType, new Func<DbExpression, TypeUsage, DbExpression>(DbExpressionBuilder.OfType));
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0000F3F8 File Offset: 0x0000D5F8
		public override DbExpression Visit(DbNewInstanceExpression expression)
		{
			Check.NotNull<DbNewInstanceExpression>(expression, "expression");
			DbExpression dbExpression = expression;
			TypeUsage typeUsage = this.VisitTypeUsage(expression.ResultType);
			IList<DbExpression> list = this.VisitExpressionList(expression.Arguments);
			bool flag = object.ReferenceEquals(expression.ResultType, typeUsage) && object.ReferenceEquals(expression.Arguments, list);
			if (expression.HasRelatedEntityReferences)
			{
				IList<DbRelatedEntityRef> list2 = DefaultExpressionVisitor.VisitList<DbRelatedEntityRef>(expression.RelatedEntityReferences, new Func<DbRelatedEntityRef, DbRelatedEntityRef>(this.VisitRelatedEntityRef));
				if (!flag || !object.ReferenceEquals(expression.RelatedEntityReferences, list2))
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

		// Token: 0x060002C4 RID: 708 RVA: 0x0000F4B0 File Offset: 0x0000D6B0
		public override DbExpression Visit(DbRefExpression expression)
		{
			Check.NotNull<DbRefExpression>(expression, "expression");
			DbExpression dbExpression = expression;
			EntityType entityType = (EntityType)TypeHelpers.GetEdmType<RefType>(expression.ResultType).ElementType;
			DbExpression dbExpression2 = this.VisitExpression(expression.Argument);
			EntityType entityType2 = (EntityType)this.VisitType(entityType);
			EntitySet entitySet = (EntitySet)this.VisitEntitySet(expression.EntitySet);
			if (!object.ReferenceEquals(expression.Argument, dbExpression2) || !object.ReferenceEquals(entityType, entityType2) || !object.ReferenceEquals(expression.EntitySet, entitySet))
			{
				dbExpression = entitySet.RefFromKey(dbExpression2, entityType2);
			}
			this.NotifyIfChanged(expression, dbExpression);
			return dbExpression;
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0000F548 File Offset: 0x0000D748
		public override DbExpression Visit(DbRelationshipNavigationExpression expression)
		{
			Check.NotNull<DbRelationshipNavigationExpression>(expression, "expression");
			DbExpression dbExpression = expression;
			RelationshipEndMember relationshipEndMember;
			RelationshipEndMember relationshipEndMember2;
			this.VisitRelationshipEnds(expression.NavigateFrom, expression.NavigateTo, out relationshipEndMember, out relationshipEndMember2);
			DbExpression dbExpression2 = this.VisitExpression(expression.NavigationSource);
			if (!object.ReferenceEquals(expression.NavigateFrom, relationshipEndMember) || !object.ReferenceEquals(expression.NavigateTo, relationshipEndMember2) || !object.ReferenceEquals(expression.NavigationSource, dbExpression2))
			{
				dbExpression = dbExpression2.Navigate(relationshipEndMember, relationshipEndMember2);
			}
			this.NotifyIfChanged(expression, dbExpression);
			return dbExpression;
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000F5C2 File Offset: 0x0000D7C2
		public override DbExpression Visit(DbDerefExpression expression)
		{
			Check.NotNull<DbDerefExpression>(expression, "expression");
			return this.VisitUnary(expression, new Func<DbExpression, DbExpression>(DbExpressionBuilder.Deref));
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000F5E3 File Offset: 0x0000D7E3
		public override DbExpression Visit(DbRefKeyExpression expression)
		{
			Check.NotNull<DbRefKeyExpression>(expression, "expression");
			return this.VisitUnary(expression, new Func<DbExpression, DbExpression>(DbExpressionBuilder.GetRefKey));
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000F604 File Offset: 0x0000D804
		public override DbExpression Visit(DbEntityRefExpression expression)
		{
			Check.NotNull<DbEntityRefExpression>(expression, "expression");
			return this.VisitUnary(expression, new Func<DbExpression, DbExpression>(DbExpressionBuilder.GetEntityRef));
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0000F628 File Offset: 0x0000D828
		public override DbExpression Visit(DbScanExpression expression)
		{
			Check.NotNull<DbScanExpression>(expression, "expression");
			DbExpression dbExpression = expression;
			EntitySetBase entitySetBase = this.VisitEntitySet(expression.Target);
			if (!object.ReferenceEquals(expression.Target, entitySetBase))
			{
				dbExpression = entitySetBase.Scan();
			}
			this.NotifyIfChanged(expression, dbExpression);
			return dbExpression;
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0000F670 File Offset: 0x0000D870
		public override DbExpression Visit(DbFilterExpression expression)
		{
			Check.NotNull<DbFilterExpression>(expression, "expression");
			DbExpression dbExpression = expression;
			DbExpressionBinding dbExpressionBinding = this.VisitExpressionBindingEnterScope(expression.Input);
			DbExpression dbExpression2 = this.VisitExpression(expression.Predicate);
			this.ExitScope();
			if (!object.ReferenceEquals(expression.Input, dbExpressionBinding) || !object.ReferenceEquals(expression.Predicate, dbExpression2))
			{
				dbExpression = dbExpressionBinding.Filter(dbExpression2);
			}
			this.NotifyIfChanged(expression, dbExpression);
			return dbExpression;
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0000F6D8 File Offset: 0x0000D8D8
		public override DbExpression Visit(DbProjectExpression expression)
		{
			Check.NotNull<DbProjectExpression>(expression, "expression");
			DbExpression dbExpression = expression;
			DbExpressionBinding dbExpressionBinding = this.VisitExpressionBindingEnterScope(expression.Input);
			DbExpression dbExpression2 = this.VisitExpression(expression.Projection);
			this.ExitScope();
			if (!object.ReferenceEquals(expression.Input, dbExpressionBinding) || !object.ReferenceEquals(expression.Projection, dbExpression2))
			{
				dbExpression = dbExpressionBinding.Project(dbExpression2);
			}
			this.NotifyIfChanged(expression, dbExpression);
			return dbExpression;
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0000F740 File Offset: 0x0000D940
		public override DbExpression Visit(DbCrossJoinExpression expression)
		{
			Check.NotNull<DbCrossJoinExpression>(expression, "expression");
			DbExpression dbExpression = expression;
			IList<DbExpressionBinding> list = this.VisitExpressionBindingList(expression.Inputs);
			if (!object.ReferenceEquals(expression.Inputs, list))
			{
				dbExpression = DbExpressionBuilder.CrossJoin(list);
			}
			this.NotifyIfChanged(expression, dbExpression);
			return dbExpression;
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0000F788 File Offset: 0x0000D988
		public override DbExpression Visit(DbJoinExpression expression)
		{
			Check.NotNull<DbJoinExpression>(expression, "expression");
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
			if (!object.ReferenceEquals(expression.Left, dbExpressionBinding) || !object.ReferenceEquals(expression.Right, dbExpressionBinding2) || !object.ReferenceEquals(expression.JoinCondition, dbExpression2))
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

		// Token: 0x060002CE RID: 718 RVA: 0x0000F85C File Offset: 0x0000DA5C
		public override DbExpression Visit(DbApplyExpression expression)
		{
			Check.NotNull<DbApplyExpression>(expression, "expression");
			DbExpression dbExpression = expression;
			DbExpressionBinding dbExpressionBinding = this.VisitExpressionBindingEnterScope(expression.Input);
			DbExpressionBinding dbExpressionBinding2 = this.VisitExpressionBinding(expression.Apply);
			this.ExitScope();
			if (!object.ReferenceEquals(expression.Input, dbExpressionBinding) || !object.ReferenceEquals(expression.Apply, dbExpressionBinding2))
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

		// Token: 0x060002CF RID: 719 RVA: 0x0000F8E8 File Offset: 0x0000DAE8
		public override DbExpression Visit(DbGroupByExpression expression)
		{
			Check.NotNull<DbGroupByExpression>(expression, "expression");
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
			IList<DbAggregate> list2 = DefaultExpressionVisitor.VisitList<DbAggregate>(expression.Aggregates, new Func<DbAggregate, DbAggregate>(this.VisitAggregate));
			this.ExitScope();
			if (!object.ReferenceEquals(expression.Input, dbGroupExpressionBinding) || !object.ReferenceEquals(expression.Keys, list) || !object.ReferenceEquals(expression.Aggregates, list2))
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

		// Token: 0x060002D0 RID: 720 RVA: 0x0000FA50 File Offset: 0x0000DC50
		public override DbExpression Visit(DbSkipExpression expression)
		{
			Check.NotNull<DbSkipExpression>(expression, "expression");
			DbExpression dbExpression = expression;
			DbExpressionBinding dbExpressionBinding = this.VisitExpressionBindingEnterScope(expression.Input);
			IList<DbSortClause> list = this.VisitSortOrder(expression.SortOrder);
			this.ExitScope();
			DbExpression dbExpression2 = this.VisitExpression(expression.Count);
			if (!object.ReferenceEquals(expression.Input, dbExpressionBinding) || !object.ReferenceEquals(expression.SortOrder, list) || !object.ReferenceEquals(expression.Count, dbExpression2))
			{
				dbExpression = dbExpressionBinding.Skip(list, dbExpression2);
			}
			this.NotifyIfChanged(expression, dbExpression);
			return dbExpression;
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0000FAD4 File Offset: 0x0000DCD4
		public override DbExpression Visit(DbSortExpression expression)
		{
			Check.NotNull<DbSortExpression>(expression, "expression");
			DbExpression dbExpression = expression;
			DbExpressionBinding dbExpressionBinding = this.VisitExpressionBindingEnterScope(expression.Input);
			IList<DbSortClause> list = this.VisitSortOrder(expression.SortOrder);
			this.ExitScope();
			if (!object.ReferenceEquals(expression.Input, dbExpressionBinding) || !object.ReferenceEquals(expression.SortOrder, list))
			{
				dbExpression = dbExpressionBinding.Sort(list);
			}
			this.NotifyIfChanged(expression, dbExpression);
			return dbExpression;
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0000FB3C File Offset: 0x0000DD3C
		public override DbExpression Visit(DbQuantifierExpression expression)
		{
			Check.NotNull<DbQuantifierExpression>(expression, "expression");
			DbExpression dbExpression = expression;
			DbExpressionBinding dbExpressionBinding = this.VisitExpressionBindingEnterScope(expression.Input);
			DbExpression dbExpression2 = this.VisitExpression(expression.Predicate);
			this.ExitScope();
			if (!object.ReferenceEquals(expression.Input, dbExpressionBinding) || !object.ReferenceEquals(expression.Predicate, dbExpression2))
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

		// Token: 0x0400011D RID: 285
		private readonly Dictionary<DbVariableReferenceExpression, DbVariableReferenceExpression> varMappings = new Dictionary<DbVariableReferenceExpression, DbVariableReferenceExpression>();
	}
}
