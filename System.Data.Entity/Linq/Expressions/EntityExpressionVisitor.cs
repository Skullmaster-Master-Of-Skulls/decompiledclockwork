using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions.Internal;

namespace System.Linq.Expressions
{
	// Token: 0x02000006 RID: 6
	internal abstract class EntityExpressionVisitor
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		internal EntityExpressionVisitor()
		{
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
		internal virtual Expression Visit(Expression exp)
		{
			if (exp == null)
			{
				return exp;
			}
			switch (exp.NodeType)
			{
			case (ExpressionType)(-1):
				return this.VisitExtension(exp);
			case ExpressionType.Add:
			case ExpressionType.AddChecked:
			case ExpressionType.And:
			case ExpressionType.AndAlso:
			case ExpressionType.ArrayIndex:
			case ExpressionType.Coalesce:
			case ExpressionType.Divide:
			case ExpressionType.Equal:
			case ExpressionType.ExclusiveOr:
			case ExpressionType.GreaterThan:
			case ExpressionType.GreaterThanOrEqual:
			case ExpressionType.LeftShift:
			case ExpressionType.LessThan:
			case ExpressionType.LessThanOrEqual:
			case ExpressionType.Modulo:
			case ExpressionType.Multiply:
			case ExpressionType.MultiplyChecked:
			case ExpressionType.NotEqual:
			case ExpressionType.Or:
			case ExpressionType.OrElse:
			case ExpressionType.Power:
			case ExpressionType.RightShift:
			case ExpressionType.Subtract:
			case ExpressionType.SubtractChecked:
				return this.VisitBinary((BinaryExpression)exp);
			case ExpressionType.ArrayLength:
			case ExpressionType.Convert:
			case ExpressionType.ConvertChecked:
			case ExpressionType.Negate:
			case ExpressionType.UnaryPlus:
			case ExpressionType.NegateChecked:
			case ExpressionType.Not:
			case ExpressionType.Quote:
			case ExpressionType.TypeAs:
				return this.VisitUnary((UnaryExpression)exp);
			case ExpressionType.Call:
				return this.VisitMethodCall((MethodCallExpression)exp);
			case ExpressionType.Conditional:
				return this.VisitConditional((ConditionalExpression)exp);
			case ExpressionType.Constant:
				return this.VisitConstant((ConstantExpression)exp);
			case ExpressionType.Invoke:
				return this.VisitInvocation((InvocationExpression)exp);
			case ExpressionType.Lambda:
				return this.VisitLambda((LambdaExpression)exp);
			case ExpressionType.ListInit:
				return this.VisitListInit((ListInitExpression)exp);
			case ExpressionType.MemberAccess:
				return this.VisitMemberAccess((MemberExpression)exp);
			case ExpressionType.MemberInit:
				return this.VisitMemberInit((MemberInitExpression)exp);
			case ExpressionType.New:
				return this.VisitNew((NewExpression)exp);
			case ExpressionType.NewArrayInit:
			case ExpressionType.NewArrayBounds:
				return this.VisitNewArray((NewArrayExpression)exp);
			case ExpressionType.Parameter:
				return this.VisitParameter((ParameterExpression)exp);
			case ExpressionType.TypeIs:
				return this.VisitTypeIs((TypeBinaryExpression)exp);
			default:
				throw Error.UnhandledExpressionType(exp.NodeType);
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002204 File Offset: 0x00000404
		internal virtual MemberBinding VisitBinding(MemberBinding binding)
		{
			switch (binding.BindingType)
			{
			case MemberBindingType.Assignment:
				return this.VisitMemberAssignment((MemberAssignment)binding);
			case MemberBindingType.MemberBinding:
				return this.VisitMemberMemberBinding((MemberMemberBinding)binding);
			case MemberBindingType.ListBinding:
				return this.VisitMemberListBinding((MemberListBinding)binding);
			default:
				throw Error.UnhandledBindingType(binding.BindingType);
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002260 File Offset: 0x00000460
		internal virtual ElementInit VisitElementInitializer(ElementInit initializer)
		{
			ReadOnlyCollection<Expression> readOnlyCollection = this.VisitExpressionList(initializer.Arguments);
			if (readOnlyCollection != initializer.Arguments)
			{
				return Expression.ElementInit(initializer.AddMethod, readOnlyCollection);
			}
			return initializer;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002294 File Offset: 0x00000494
		internal virtual Expression VisitUnary(UnaryExpression u)
		{
			Expression expression = this.Visit(u.Operand);
			if (expression != u.Operand)
			{
				return Expression.MakeUnary(u.NodeType, expression, u.Type, u.Method);
			}
			return u;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000022D4 File Offset: 0x000004D4
		internal virtual Expression VisitBinary(BinaryExpression b)
		{
			Expression expression = this.Visit(b.Left);
			Expression expression2 = this.Visit(b.Right);
			Expression expression3 = this.Visit(b.Conversion);
			if (expression == b.Left && expression2 == b.Right && expression3 == b.Conversion)
			{
				return b;
			}
			if (b.NodeType == ExpressionType.Coalesce && b.Conversion != null)
			{
				return Expression.Coalesce(expression, expression2, expression3 as LambdaExpression);
			}
			return Expression.MakeBinary(b.NodeType, expression, expression2, b.IsLiftedToNull, b.Method);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002360 File Offset: 0x00000560
		internal virtual Expression VisitTypeIs(TypeBinaryExpression b)
		{
			Expression expression = this.Visit(b.Expression);
			if (expression != b.Expression)
			{
				return Expression.TypeIs(expression, b.TypeOperand);
			}
			return b;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002391 File Offset: 0x00000591
		internal virtual Expression VisitConstant(ConstantExpression c)
		{
			return c;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002394 File Offset: 0x00000594
		internal virtual Expression VisitConditional(ConditionalExpression c)
		{
			Expression expression = this.Visit(c.Test);
			Expression expression2 = this.Visit(c.IfTrue);
			Expression expression3 = this.Visit(c.IfFalse);
			if (expression != c.Test || expression2 != c.IfTrue || expression3 != c.IfFalse)
			{
				return Expression.Condition(expression, expression2, expression3);
			}
			return c;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002391 File Offset: 0x00000591
		internal virtual Expression VisitParameter(ParameterExpression p)
		{
			return p;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000023F0 File Offset: 0x000005F0
		internal virtual Expression VisitMemberAccess(MemberExpression m)
		{
			Expression expression = this.Visit(m.Expression);
			if (expression != m.Expression)
			{
				return Expression.MakeMemberAccess(expression, m.Member);
			}
			return m;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002424 File Offset: 0x00000624
		internal virtual Expression VisitMethodCall(MethodCallExpression m)
		{
			Expression expression = this.Visit(m.Object);
			IEnumerable<Expression> enumerable = this.VisitExpressionList(m.Arguments);
			if (expression != m.Object || enumerable != m.Arguments)
			{
				return Expression.Call(expression, m.Method, enumerable);
			}
			return m;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000246C File Offset: 0x0000066C
		internal virtual ReadOnlyCollection<Expression> VisitExpressionList(ReadOnlyCollection<Expression> original)
		{
			List<Expression> list = null;
			int i = 0;
			int count = original.Count;
			while (i < count)
			{
				Expression expression = this.Visit(original[i]);
				if (list != null)
				{
					list.Add(expression);
				}
				else if (expression != original[i])
				{
					list = new List<Expression>(count);
					for (int j = 0; j < i; j++)
					{
						list.Add(original[j]);
					}
					list.Add(expression);
				}
				i++;
			}
			if (list != null)
			{
				return list.ToReadOnlyCollection<Expression>();
			}
			return original;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000024EC File Offset: 0x000006EC
		internal virtual MemberAssignment VisitMemberAssignment(MemberAssignment assignment)
		{
			Expression expression = this.Visit(assignment.Expression);
			if (expression != assignment.Expression)
			{
				return Expression.Bind(assignment.Member, expression);
			}
			return assignment;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002520 File Offset: 0x00000720
		internal virtual MemberMemberBinding VisitMemberMemberBinding(MemberMemberBinding binding)
		{
			IEnumerable<MemberBinding> enumerable = this.VisitBindingList(binding.Bindings);
			if (enumerable != binding.Bindings)
			{
				return Expression.MemberBind(binding.Member, enumerable);
			}
			return binding;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002554 File Offset: 0x00000754
		internal virtual MemberListBinding VisitMemberListBinding(MemberListBinding binding)
		{
			IEnumerable<ElementInit> enumerable = this.VisitElementInitializerList(binding.Initializers);
			if (enumerable != binding.Initializers)
			{
				return Expression.ListBind(binding.Member, enumerable);
			}
			return binding;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002588 File Offset: 0x00000788
		internal virtual IEnumerable<MemberBinding> VisitBindingList(ReadOnlyCollection<MemberBinding> original)
		{
			List<MemberBinding> list = null;
			int i = 0;
			int count = original.Count;
			while (i < count)
			{
				MemberBinding memberBinding = this.VisitBinding(original[i]);
				if (list != null)
				{
					list.Add(memberBinding);
				}
				else if (memberBinding != original[i])
				{
					list = new List<MemberBinding>(count);
					for (int j = 0; j < i; j++)
					{
						list.Add(original[j]);
					}
					list.Add(memberBinding);
				}
				i++;
			}
			if (list != null)
			{
				return list;
			}
			return original;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002600 File Offset: 0x00000800
		internal virtual IEnumerable<ElementInit> VisitElementInitializerList(ReadOnlyCollection<ElementInit> original)
		{
			List<ElementInit> list = null;
			int i = 0;
			int count = original.Count;
			while (i < count)
			{
				ElementInit elementInit = this.VisitElementInitializer(original[i]);
				if (list != null)
				{
					list.Add(elementInit);
				}
				else if (elementInit != original[i])
				{
					list = new List<ElementInit>(count);
					for (int j = 0; j < i; j++)
					{
						list.Add(original[j]);
					}
					list.Add(elementInit);
				}
				i++;
			}
			if (list != null)
			{
				return list;
			}
			return original;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002678 File Offset: 0x00000878
		internal virtual Expression VisitLambda(LambdaExpression lambda)
		{
			Expression expression = this.Visit(lambda.Body);
			if (expression != lambda.Body)
			{
				return Expression.Lambda(lambda.Type, expression, lambda.Parameters);
			}
			return lambda;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000026B0 File Offset: 0x000008B0
		internal virtual NewExpression VisitNew(NewExpression nex)
		{
			IEnumerable<Expression> enumerable = this.VisitExpressionList(nex.Arguments);
			if (enumerable == nex.Arguments)
			{
				return nex;
			}
			if (nex.Members != null)
			{
				return Expression.New(nex.Constructor, enumerable, nex.Members);
			}
			return Expression.New(nex.Constructor, enumerable);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000026FC File Offset: 0x000008FC
		internal virtual Expression VisitMemberInit(MemberInitExpression init)
		{
			NewExpression newExpression = this.VisitNew(init.NewExpression);
			IEnumerable<MemberBinding> enumerable = this.VisitBindingList(init.Bindings);
			if (newExpression != init.NewExpression || enumerable != init.Bindings)
			{
				return Expression.MemberInit(newExpression, enumerable);
			}
			return init;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002740 File Offset: 0x00000940
		internal virtual Expression VisitListInit(ListInitExpression init)
		{
			NewExpression newExpression = this.VisitNew(init.NewExpression);
			IEnumerable<ElementInit> enumerable = this.VisitElementInitializerList(init.Initializers);
			if (newExpression != init.NewExpression || enumerable != init.Initializers)
			{
				return Expression.ListInit(newExpression, enumerable);
			}
			return init;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002784 File Offset: 0x00000984
		internal virtual Expression VisitNewArray(NewArrayExpression na)
		{
			IEnumerable<Expression> enumerable = this.VisitExpressionList(na.Expressions);
			if (enumerable == na.Expressions)
			{
				return na;
			}
			if (na.NodeType == ExpressionType.NewArrayInit)
			{
				return Expression.NewArrayInit(na.Type.GetElementType(), enumerable);
			}
			return Expression.NewArrayBounds(na.Type.GetElementType(), enumerable);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000027D8 File Offset: 0x000009D8
		internal virtual Expression VisitInvocation(InvocationExpression iv)
		{
			IEnumerable<Expression> enumerable = this.VisitExpressionList(iv.Arguments);
			Expression expression = this.Visit(iv.Expression);
			if (enumerable != iv.Arguments || expression != iv.Expression)
			{
				return Expression.Invoke(expression, enumerable);
			}
			return iv;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002391 File Offset: 0x00000591
		internal virtual Expression VisitExtension(Expression ext)
		{
			return ext;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000281C File Offset: 0x00000A1C
		internal static Expression Visit(Expression exp, Func<Expression, Func<Expression, Expression>, Expression> visit)
		{
			EntityExpressionVisitor.BasicExpressionVisitor basicExpressionVisitor = new EntityExpressionVisitor.BasicExpressionVisitor(visit);
			return basicExpressionVisitor.Visit(exp);
		}

		// Token: 0x04000079 RID: 121
		internal const ExpressionType CustomExpression = (ExpressionType)(-1);

		// Token: 0x0200043C RID: 1084
		private sealed class BasicExpressionVisitor : EntityExpressionVisitor
		{
			// Token: 0x06003A23 RID: 14883 RVA: 0x000DDDA0 File Offset: 0x000DBFA0
			internal BasicExpressionVisitor(Func<Expression, Func<Expression, Expression>, Expression> visit)
			{
				Func<Expression, Func<Expression, Expression>, Expression> visit2 = visit;
				if (visit == null && (visit2 = EntityExpressionVisitor.BasicExpressionVisitor.<>c.<>9__1_0) == null)
				{
					visit2 = (EntityExpressionVisitor.BasicExpressionVisitor.<>c.<>9__1_0 = ((Expression exp, Func<Expression, Expression> baseVisit) => baseVisit(exp)));
				}
				this._visit = visit2;
			}

			// Token: 0x06003A24 RID: 14884 RVA: 0x000DDDD2 File Offset: 0x000DBFD2
			internal override Expression Visit(Expression exp)
			{
				return this._visit(exp, new Func<Expression, Expression>(base.Visit));
			}

			// Token: 0x04001898 RID: 6296
			private readonly Func<Expression, Func<Expression, Expression>, Expression> _visit;
		}
	}
}
