using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions.Internal;

namespace System.Linq.Expressions
{
	// Token: 0x020002DC RID: 732
	internal abstract class EntityExpressionVisitor
	{
		// Token: 0x060019B2 RID: 6578 RVA: 0x0007FD7C File Offset: 0x0007DF7C
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
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
			case ExpressionType.ExclusiveOr:
			case ExpressionType.LeftShift:
			case ExpressionType.Modulo:
			case ExpressionType.Multiply:
			case ExpressionType.MultiplyChecked:
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
			case ExpressionType.Equal:
			case ExpressionType.GreaterThan:
			case ExpressionType.GreaterThanOrEqual:
			case ExpressionType.LessThan:
			case ExpressionType.LessThanOrEqual:
			case ExpressionType.NotEqual:
				return this.VisitComparison((BinaryExpression)exp);
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

		// Token: 0x060019B3 RID: 6579 RVA: 0x0007FF34 File Offset: 0x0007E134
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

		// Token: 0x060019B4 RID: 6580 RVA: 0x0007FF90 File Offset: 0x0007E190
		internal virtual ElementInit VisitElementInitializer(ElementInit initializer)
		{
			ReadOnlyCollection<Expression> readOnlyCollection = this.VisitExpressionList(initializer.Arguments);
			if (readOnlyCollection != initializer.Arguments)
			{
				return Expression.ElementInit(initializer.AddMethod, readOnlyCollection);
			}
			return initializer;
		}

		// Token: 0x060019B5 RID: 6581 RVA: 0x0007FFC4 File Offset: 0x0007E1C4
		internal virtual Expression VisitUnary(UnaryExpression u)
		{
			Expression expression = this.Visit(u.Operand);
			if (expression != u.Operand)
			{
				return Expression.MakeUnary(u.NodeType, expression, u.Type, u.Method);
			}
			return u;
		}

		// Token: 0x060019B6 RID: 6582 RVA: 0x00080004 File Offset: 0x0007E204
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

		// Token: 0x060019B7 RID: 6583 RVA: 0x0008008D File Offset: 0x0007E28D
		internal virtual Expression VisitComparison(BinaryExpression expression)
		{
			return this.VisitBinary(EntityExpressionVisitor.RemoveUnnecessaryConverts(expression));
		}

		// Token: 0x060019B8 RID: 6584 RVA: 0x0008009C File Offset: 0x0007E29C
		internal virtual Expression VisitTypeIs(TypeBinaryExpression b)
		{
			Expression expression = this.Visit(b.Expression);
			if (expression != b.Expression)
			{
				return Expression.TypeIs(expression, b.TypeOperand);
			}
			return b;
		}

		// Token: 0x060019B9 RID: 6585 RVA: 0x000800CD File Offset: 0x0007E2CD
		internal virtual Expression VisitConstant(ConstantExpression c)
		{
			return c;
		}

		// Token: 0x060019BA RID: 6586 RVA: 0x000800D0 File Offset: 0x0007E2D0
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

		// Token: 0x060019BB RID: 6587 RVA: 0x00080129 File Offset: 0x0007E329
		internal virtual Expression VisitParameter(ParameterExpression p)
		{
			return p;
		}

		// Token: 0x060019BC RID: 6588 RVA: 0x0008012C File Offset: 0x0007E32C
		internal virtual Expression VisitMemberAccess(MemberExpression m)
		{
			Expression expression = this.Visit(m.Expression);
			if (expression != m.Expression)
			{
				return Expression.MakeMemberAccess(expression, m.Member);
			}
			return m;
		}

		// Token: 0x060019BD RID: 6589 RVA: 0x00080160 File Offset: 0x0007E360
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

		// Token: 0x060019BE RID: 6590 RVA: 0x000801A8 File Offset: 0x0007E3A8
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

		// Token: 0x060019BF RID: 6591 RVA: 0x00080228 File Offset: 0x0007E428
		internal virtual MemberAssignment VisitMemberAssignment(MemberAssignment assignment)
		{
			Expression expression = this.Visit(assignment.Expression);
			if (expression != assignment.Expression)
			{
				return Expression.Bind(assignment.Member, expression);
			}
			return assignment;
		}

		// Token: 0x060019C0 RID: 6592 RVA: 0x0008025C File Offset: 0x0007E45C
		internal virtual MemberMemberBinding VisitMemberMemberBinding(MemberMemberBinding binding)
		{
			IEnumerable<MemberBinding> enumerable = this.VisitBindingList(binding.Bindings);
			if (enumerable != binding.Bindings)
			{
				return Expression.MemberBind(binding.Member, enumerable);
			}
			return binding;
		}

		// Token: 0x060019C1 RID: 6593 RVA: 0x00080290 File Offset: 0x0007E490
		internal virtual MemberListBinding VisitMemberListBinding(MemberListBinding binding)
		{
			IEnumerable<ElementInit> enumerable = this.VisitElementInitializerList(binding.Initializers);
			if (enumerable != binding.Initializers)
			{
				return Expression.ListBind(binding.Member, enumerable);
			}
			return binding;
		}

		// Token: 0x060019C2 RID: 6594 RVA: 0x000802C4 File Offset: 0x0007E4C4
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

		// Token: 0x060019C3 RID: 6595 RVA: 0x0008033C File Offset: 0x0007E53C
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

		// Token: 0x060019C4 RID: 6596 RVA: 0x000803B4 File Offset: 0x0007E5B4
		internal virtual Expression VisitLambda(LambdaExpression lambda)
		{
			Expression expression = this.Visit(lambda.Body);
			if (expression != lambda.Body)
			{
				return Expression.Lambda(lambda.Type, expression, lambda.Parameters);
			}
			return lambda;
		}

		// Token: 0x060019C5 RID: 6597 RVA: 0x000803EC File Offset: 0x0007E5EC
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

		// Token: 0x060019C6 RID: 6598 RVA: 0x00080438 File Offset: 0x0007E638
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

		// Token: 0x060019C7 RID: 6599 RVA: 0x0008047C File Offset: 0x0007E67C
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

		// Token: 0x060019C8 RID: 6600 RVA: 0x000804C0 File Offset: 0x0007E6C0
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

		// Token: 0x060019C9 RID: 6601 RVA: 0x00080514 File Offset: 0x0007E714
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

		// Token: 0x060019CA RID: 6602 RVA: 0x00080556 File Offset: 0x0007E756
		internal virtual Expression VisitExtension(Expression ext)
		{
			return ext;
		}

		// Token: 0x060019CB RID: 6603 RVA: 0x0008055C File Offset: 0x0007E75C
		internal static Expression Visit(Expression exp, Func<Expression, Func<Expression, Expression>, Expression> visit)
		{
			EntityExpressionVisitor.BasicExpressionVisitor basicExpressionVisitor = new EntityExpressionVisitor.BasicExpressionVisitor(visit);
			return basicExpressionVisitor.Visit(exp);
		}

		// Token: 0x060019CC RID: 6604 RVA: 0x00080578 File Offset: 0x0007E778
		private static BinaryExpression RemoveUnnecessaryConverts(BinaryExpression expression)
		{
			if (expression.Method != null || expression.Left.Type != expression.Right.Type)
			{
				return expression;
			}
			switch (expression.Left.NodeType)
			{
			case ExpressionType.Constant:
			{
				ConstantExpression left = (ConstantExpression)expression.Left;
				if (expression.Right.NodeType == ExpressionType.Convert)
				{
					UnaryExpression unaryExpression = (UnaryExpression)expression.Right;
					if (EntityExpressionVisitor.TryConvertConstant(ref left, unaryExpression.Operand.Type))
					{
						return EntityExpressionVisitor.MakeBinaryExpression(expression.NodeType, left, unaryExpression.Operand);
					}
				}
				break;
			}
			case ExpressionType.Convert:
			{
				UnaryExpression unaryExpression2 = (UnaryExpression)expression.Left;
				switch (expression.Right.NodeType)
				{
				case ExpressionType.Constant:
				{
					ConstantExpression right = (ConstantExpression)expression.Right;
					if (EntityExpressionVisitor.TryConvertConstant(ref right, unaryExpression2.Operand.Type))
					{
						return EntityExpressionVisitor.MakeBinaryExpression(expression.NodeType, unaryExpression2.Operand, right);
					}
					break;
				}
				case ExpressionType.Convert:
				{
					UnaryExpression unaryExpression3 = (UnaryExpression)expression.Right;
					if (EntityExpressionVisitor.CanRemoveConverts(unaryExpression2, unaryExpression3))
					{
						return EntityExpressionVisitor.MakeBinaryExpression(expression.NodeType, unaryExpression2.Operand, unaryExpression3.Operand);
					}
					break;
				}
				}
				break;
			}
			}
			return expression;
		}

		// Token: 0x060019CD RID: 6605 RVA: 0x000806BC File Offset: 0x0007E8BC
		private static bool CanRemoveConverts(UnaryExpression leftConvert, UnaryExpression rightConvert)
		{
			if (leftConvert.Method != null || rightConvert.Method != null)
			{
				return false;
			}
			if (Type.GetTypeCode(leftConvert.Type) != TypeCode.Int32)
			{
				return false;
			}
			switch (Type.GetTypeCode(leftConvert.Operand.Type))
			{
			case TypeCode.Byte:
			case TypeCode.Int16:
				return leftConvert.Operand.Type == rightConvert.Operand.Type;
			default:
				return false;
			}
		}

		// Token: 0x060019CE RID: 6606 RVA: 0x00080738 File Offset: 0x0007E938
		private static bool TryConvertConstant(ref ConstantExpression constant, Type type)
		{
			if (Type.GetTypeCode(constant.Type) != TypeCode.Int32)
			{
				return false;
			}
			int num = (int)constant.Value;
			switch (Type.GetTypeCode(type))
			{
			case TypeCode.Byte:
				if (num >= 0 && num <= 255)
				{
					constant = Expression.Constant((byte)num);
					return true;
				}
				break;
			case TypeCode.Int16:
				if (num >= -32768 && num <= 32767)
				{
					constant = Expression.Constant((short)num);
					return true;
				}
				break;
			}
			return false;
		}

		// Token: 0x060019CF RID: 6607 RVA: 0x000807BC File Offset: 0x0007E9BC
		private static BinaryExpression MakeBinaryExpression(ExpressionType expressionType, Expression left, Expression right)
		{
			if (left.Type.IsEnum)
			{
				left = Expression.Convert(left, left.Type.GetEnumUnderlyingType());
			}
			if (right.Type.IsEnum)
			{
				right = Expression.Convert(right, right.Type.GetEnumUnderlyingType());
			}
			return Expression.MakeBinary(expressionType, left, right);
		}

		// Token: 0x040008DE RID: 2270
		internal const ExpressionType CustomExpression = (ExpressionType)(-1);

		// Token: 0x020002DD RID: 733
		private sealed class BasicExpressionVisitor : EntityExpressionVisitor
		{
			// Token: 0x060019D1 RID: 6609 RVA: 0x00080822 File Offset: 0x0007EA22
			internal BasicExpressionVisitor(Func<Expression, Func<Expression, Expression>, Expression> visit)
			{
				Func<Expression, Func<Expression, Expression>, Expression> visit2 = visit;
				if (visit == null)
				{
					visit2 = ((Expression exp, Func<Expression, Expression> baseVisit) => baseVisit(exp));
				}
				this._visit = visit2;
			}

			// Token: 0x060019D2 RID: 6610 RVA: 0x00080852 File Offset: 0x0007EA52
			internal override Expression Visit(Expression exp)
			{
				return this._visit(exp, new Func<Expression, Expression>(base.Visit));
			}

			// Token: 0x040008DF RID: 2271
			private readonly Func<Expression, Func<Expression, Expression>, Expression> _visit;
		}
	}
}
