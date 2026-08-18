using System;
using System.Collections.Generic;
using System.Data.Spatial;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Data.Objects.ELinq
{
	// Token: 0x020001A8 RID: 424
	internal class LinqExpressionNormalizer : EntityExpressionVisitor
	{
		// Token: 0x06001E90 RID: 7824 RVA: 0x0006ABD4 File Offset: 0x00068DD4
		internal override Expression VisitBinary(BinaryExpression b)
		{
			b = (BinaryExpression)base.VisitBinary(b);
			if (b.NodeType == ExpressionType.Equal)
			{
				Expression expression = LinqExpressionNormalizer.UnwrapObjectConvert(b.Left);
				Expression expression2 = LinqExpressionNormalizer.UnwrapObjectConvert(b.Right);
				if (expression != b.Left || expression2 != b.Right)
				{
					b = LinqExpressionNormalizer.CreateRelationalOperator(ExpressionType.Equal, expression, expression2);
				}
			}
			LinqExpressionNormalizer.Pattern pattern;
			if (this._patterns.TryGetValue(b.Left, out pattern) && pattern.Kind == LinqExpressionNormalizer.PatternKind.Compare && this.IsConstantZero(b.Right))
			{
				LinqExpressionNormalizer.ComparePattern comparePattern = (LinqExpressionNormalizer.ComparePattern)pattern;
				BinaryExpression binaryExpression;
				if (LinqExpressionNormalizer.TryCreateRelationalOperator(b.NodeType, comparePattern.Left, comparePattern.Right, out binaryExpression))
				{
					b = binaryExpression;
				}
			}
			return b;
		}

		// Token: 0x06001E91 RID: 7825 RVA: 0x0006AC80 File Offset: 0x00068E80
		private static Expression UnwrapObjectConvert(Expression input)
		{
			if (input.NodeType == ExpressionType.Constant && input.Type == typeof(object))
			{
				ConstantExpression constantExpression = (ConstantExpression)input;
				if (constantExpression.Value != null && constantExpression.Value.GetType() != typeof(object))
				{
					return Expression.Constant(constantExpression.Value, constantExpression.Value.GetType());
				}
			}
			while (ExpressionType.Convert == input.NodeType && typeof(object) == input.Type)
			{
				input = ((UnaryExpression)input).Operand;
			}
			return input;
		}

		// Token: 0x06001E92 RID: 7826 RVA: 0x0006AD1F File Offset: 0x00068F1F
		private bool IsConstantZero(Expression expression)
		{
			return expression.NodeType == ExpressionType.Constant && ((ConstantExpression)expression).Value.Equals(0);
		}

		// Token: 0x06001E93 RID: 7827 RVA: 0x0006AD44 File Offset: 0x00068F44
		internal override Expression VisitMethodCall(MethodCallExpression m)
		{
			m = (MethodCallExpression)base.VisitMethodCall(m);
			if (m.Method.IsStatic)
			{
				if (m.Method.Name.StartsWith("op_", StringComparison.Ordinal))
				{
					if (m.Arguments.Count == 2)
					{
						string name = m.Method.Name;
						uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
						if (num <= 1915672496U)
						{
							if (num <= 1195761148U)
							{
								if (num != 835846267U)
								{
									if (num != 906583475U)
									{
										if (num == 1195761148U)
										{
											if (name == "op_GreaterThan")
											{
												return Expression.GreaterThan(m.Arguments[0], m.Arguments[1], false, m.Method);
											}
										}
									}
									else if (name == "op_Addition")
									{
										return Expression.Add(m.Arguments[0], m.Arguments[1], m.Method);
									}
								}
								else if (name == "op_BitwiseAnd")
								{
									return Expression.And(m.Arguments[0], m.Arguments[1], m.Method);
								}
							}
							else if (num <= 1258540185U)
							{
								if (num != 1234170120U)
								{
									if (num == 1258540185U)
									{
										if (name == "op_LessThan")
										{
											return Expression.LessThan(m.Arguments[0], m.Arguments[1], false, m.Method);
										}
									}
								}
								else if (name == "op_LessThanOrEqual")
								{
									return Expression.LessThanOrEqual(m.Arguments[0], m.Arguments[1], false, m.Method);
								}
							}
							else if (num != 1516143579U)
							{
								if (num == 1915672496U)
								{
									if (name == "op_Division")
									{
										return Expression.Divide(m.Arguments[0], m.Arguments[1], m.Method);
									}
								}
							}
							else if (name == "op_Equality")
							{
								return Expression.Equal(m.Arguments[0], m.Arguments[1], false, m.Method);
							}
						}
						else if (num <= 2459852411U)
						{
							if (num != 2366795836U)
							{
								if (num != 2429678952U)
								{
									if (num == 2459852411U)
									{
										if (name == "op_GreaterThanOrEqual")
										{
											return Expression.GreaterThanOrEqual(m.Arguments[0], m.Arguments[1], false, m.Method);
										}
									}
								}
								else if (name == "op_Modulus")
								{
									return Expression.Modulo(m.Arguments[0], m.Arguments[1], m.Method);
								}
							}
							else if (name == "op_ExclusiveOr")
							{
								return Expression.ExclusiveOr(m.Arguments[0], m.Arguments[1], m.Method);
							}
						}
						else if (num <= 3279419199U)
						{
							if (num != 2958252495U)
							{
								if (num == 3279419199U)
								{
									if (name == "op_Subtraction")
									{
										return Expression.Subtract(m.Arguments[0], m.Arguments[1], m.Method);
									}
								}
							}
							else if (name == "op_Multiply")
							{
								return Expression.Multiply(m.Arguments[0], m.Arguments[1], m.Method);
							}
						}
						else if (num != 3492550567U)
						{
							if (num == 3794317784U)
							{
								if (name == "op_Inequality")
								{
									return Expression.NotEqual(m.Arguments[0], m.Arguments[1], false, m.Method);
								}
							}
						}
						else if (name == "op_BitwiseOr")
						{
							return Expression.Or(m.Arguments[0], m.Arguments[1], m.Method);
						}
					}
					if (m.Arguments.Count == 1)
					{
						string name2 = m.Method.Name;
						if (name2 == "op_UnaryNegation")
						{
							return Expression.Negate(m.Arguments[0], m.Method);
						}
						if (name2 == "op_UnaryPlus")
						{
							return Expression.UnaryPlus(m.Arguments[0], m.Method);
						}
						if (name2 == "op_Explicit" || name2 == "op_Implicit")
						{
							return Expression.Convert(m.Arguments[0], m.Type, m.Method);
						}
						if (name2 == "op_OnesComplement" || name2 == "op_False")
						{
							return Expression.Not(m.Arguments[0], m.Method);
						}
					}
				}
				if (m.Method.Name == "Equals" && m.Arguments.Count > 1)
				{
					return Expression.Equal(m.Arguments[0], m.Arguments[1], false, m.Method);
				}
				if (m.Method.Name == "CompareString" && m.Method.DeclaringType.FullName == "Microsoft.VisualBasic.CompilerServices.Operators")
				{
					return this.CreateCompareExpression(m.Arguments[0], m.Arguments[1]);
				}
				if (m.Method.Name == "Compare" && m.Arguments.Count > 1 && m.Method.ReturnType == typeof(int))
				{
					return this.CreateCompareExpression(m.Arguments[0], m.Arguments[1]);
				}
			}
			else
			{
				if (m.Method.Name == "Equals" && m.Arguments.Count > 0)
				{
					Type parameterType = m.Method.GetParameters()[0].ParameterType;
					if (parameterType != typeof(DbGeography) && parameterType != typeof(DbGeometry))
					{
						return LinqExpressionNormalizer.CreateRelationalOperator(ExpressionType.Equal, m.Object, m.Arguments[0]);
					}
				}
				if (m.Method.Name == "CompareTo" && m.Arguments.Count == 1 && m.Method.ReturnType == typeof(int))
				{
					return this.CreateCompareExpression(m.Object, m.Arguments[0]);
				}
				if (m.Method.Name == "Contains" && m.Arguments.Count == 1)
				{
					Type declaringType = m.Method.DeclaringType;
					MethodInfo methodInfo;
					if (declaringType.IsGenericType && declaringType.GetGenericTypeDefinition() == typeof(List<>) && ReflectionUtil.TryLookupMethod(SequenceMethod.Contains, out methodInfo))
					{
						MethodInfo method = methodInfo.MakeGenericMethod(declaringType.GetGenericArguments());
						return Expression.Call(method, m.Object, m.Arguments[0]);
					}
				}
			}
			return LinqExpressionNormalizer.NormalizePredicateArgument(m);
		}

		// Token: 0x06001E94 RID: 7828 RVA: 0x0006B4E0 File Offset: 0x000696E0
		private static MethodCallExpression NormalizePredicateArgument(MethodCallExpression callExpression)
		{
			int index;
			Expression value;
			MethodCallExpression result;
			if (LinqExpressionNormalizer.HasPredicateArgument(callExpression, out index) && LinqExpressionNormalizer.TryMatchCoalescePattern(callExpression.Arguments[index], out value))
			{
				List<Expression> list = new List<Expression>(callExpression.Arguments);
				list[index] = value;
				result = Expression.Call(callExpression.Object, callExpression.Method, list);
			}
			else
			{
				result = callExpression;
			}
			return result;
		}

		// Token: 0x06001E95 RID: 7829 RVA: 0x0006B538 File Offset: 0x00069738
		private static bool HasPredicateArgument(MethodCallExpression callExpression, out int argumentOrdinal)
		{
			argumentOrdinal = 0;
			bool result = false;
			SequenceMethod sequenceMethod;
			if (2 <= callExpression.Arguments.Count && ReflectionUtil.TryIdentifySequenceMethod(callExpression.Method, out sequenceMethod))
			{
				if (sequenceMethod <= SequenceMethod.TakeWhileOrdinal)
				{
					if (sequenceMethod > SequenceMethod.WhereOrdinal && sequenceMethod - SequenceMethod.TakeWhile > 1)
					{
						return result;
					}
				}
				else if (sequenceMethod - SequenceMethod.SkipWhile > 1)
				{
					switch (sequenceMethod)
					{
					case SequenceMethod.FirstPredicate:
					case SequenceMethod.FirstOrDefaultPredicate:
					case SequenceMethod.LastPredicate:
					case SequenceMethod.LastOrDefaultPredicate:
					case SequenceMethod.SinglePredicate:
					case SequenceMethod.SingleOrDefaultPredicate:
						break;
					case SequenceMethod.FirstOrDefault:
					case SequenceMethod.Last:
					case SequenceMethod.LastOrDefault:
					case SequenceMethod.Single:
					case SequenceMethod.SingleOrDefault:
						return result;
					default:
						switch (sequenceMethod)
						{
						case SequenceMethod.AnyPredicate:
						case SequenceMethod.All:
						case SequenceMethod.CountPredicate:
						case SequenceMethod.LongCountPredicate:
							break;
						case SequenceMethod.Count:
						case SequenceMethod.LongCount:
							return result;
						default:
							return result;
						}
						break;
					}
				}
				argumentOrdinal = 1;
				result = true;
			}
			return result;
		}

		// Token: 0x06001E96 RID: 7830 RVA: 0x0006B5E4 File Offset: 0x000697E4
		private static bool TryMatchCoalescePattern(Expression expression, out Expression normalized)
		{
			normalized = null;
			bool result = false;
			if (expression.NodeType == ExpressionType.Quote)
			{
				UnaryExpression unaryExpression = (UnaryExpression)expression;
				if (LinqExpressionNormalizer.TryMatchCoalescePattern(unaryExpression.Operand, out normalized))
				{
					result = true;
					normalized = Expression.Quote(normalized);
				}
			}
			else if (expression.NodeType == ExpressionType.Lambda)
			{
				LambdaExpression lambdaExpression = (LambdaExpression)expression;
				if (lambdaExpression.Body.NodeType == ExpressionType.Coalesce && lambdaExpression.Body.Type == typeof(bool))
				{
					BinaryExpression binaryExpression = (BinaryExpression)lambdaExpression.Body;
					if (binaryExpression.Right.NodeType == ExpressionType.Constant && false.Equals(((ConstantExpression)binaryExpression.Right).Value))
					{
						normalized = Expression.Lambda(lambdaExpression.Type, Expression.Convert(binaryExpression.Left, typeof(bool)), lambdaExpression.Parameters);
						result = true;
					}
				}
			}
			return result;
		}

		// Token: 0x06001E97 RID: 7831 RVA: 0x0006B6C8 File Offset: 0x000698C8
		private static bool RelationalOperatorPlaceholder<TLeft, TRight>(TLeft left, TRight right)
		{
			return left == right;
		}

		// Token: 0x06001E98 RID: 7832 RVA: 0x0006B6D8 File Offset: 0x000698D8
		private static BinaryExpression CreateRelationalOperator(ExpressionType op, Expression left, Expression right)
		{
			BinaryExpression result;
			LinqExpressionNormalizer.TryCreateRelationalOperator(op, left, right, out result);
			return result;
		}

		// Token: 0x06001E99 RID: 7833 RVA: 0x0006B6F4 File Offset: 0x000698F4
		private static bool TryCreateRelationalOperator(ExpressionType op, Expression left, Expression right, out BinaryExpression result)
		{
			MethodInfo method = LinqExpressionNormalizer.s_relationalOperatorPlaceholderMethod.MakeGenericMethod(new Type[]
			{
				left.Type,
				right.Type
			});
			switch (op)
			{
			case ExpressionType.Equal:
				result = Expression.Equal(left, right, false, method);
				return true;
			case ExpressionType.ExclusiveOr:
			case ExpressionType.Invoke:
			case ExpressionType.Lambda:
			case ExpressionType.LeftShift:
				break;
			case ExpressionType.GreaterThan:
				result = Expression.GreaterThan(left, right, false, method);
				return true;
			case ExpressionType.GreaterThanOrEqual:
				result = Expression.GreaterThanOrEqual(left, right, false, method);
				return true;
			case ExpressionType.LessThan:
				result = Expression.LessThan(left, right, false, method);
				return true;
			case ExpressionType.LessThanOrEqual:
				result = Expression.LessThanOrEqual(left, right, false, method);
				return true;
			default:
				if (op == ExpressionType.NotEqual)
				{
					result = Expression.NotEqual(left, right, false, method);
					return true;
				}
				break;
			}
			result = null;
			return false;
		}

		// Token: 0x06001E9A RID: 7834 RVA: 0x0006B7AC File Offset: 0x000699AC
		private Expression CreateCompareExpression(Expression left, Expression right)
		{
			Expression expression = Expression.Condition(LinqExpressionNormalizer.CreateRelationalOperator(ExpressionType.Equal, left, right), Expression.Constant(0), Expression.Condition(LinqExpressionNormalizer.CreateRelationalOperator(ExpressionType.GreaterThan, left, right), Expression.Constant(1), Expression.Constant(-1)));
			this._patterns[expression] = new LinqExpressionNormalizer.ComparePattern(left, right);
			return expression;
		}

		// Token: 0x04000CD9 RID: 3289
		private const bool LiftToNull = false;

		// Token: 0x04000CDA RID: 3290
		private readonly Dictionary<Expression, LinqExpressionNormalizer.Pattern> _patterns = new Dictionary<Expression, LinqExpressionNormalizer.Pattern>();

		// Token: 0x04000CDB RID: 3291
		private static readonly MethodInfo s_relationalOperatorPlaceholderMethod = typeof(LinqExpressionNormalizer).GetMethod("RelationalOperatorPlaceholder", BindingFlags.Static | BindingFlags.NonPublic);

		// Token: 0x02000510 RID: 1296
		private abstract class Pattern
		{
			// Token: 0x17000B0E RID: 2830
			// (get) Token: 0x06003DCA RID: 15818
			internal abstract LinqExpressionNormalizer.PatternKind Kind { get; }
		}

		// Token: 0x02000511 RID: 1297
		private enum PatternKind
		{
			// Token: 0x04001B11 RID: 6929
			Compare
		}

		// Token: 0x02000512 RID: 1298
		private sealed class ComparePattern : LinqExpressionNormalizer.Pattern
		{
			// Token: 0x06003DCC RID: 15820 RVA: 0x000E7329 File Offset: 0x000E5529
			internal ComparePattern(Expression left, Expression right)
			{
				this.Left = left;
				this.Right = right;
			}

			// Token: 0x17000B0F RID: 2831
			// (get) Token: 0x06003DCD RID: 15821 RVA: 0x000173E2 File Offset: 0x000155E2
			internal override LinqExpressionNormalizer.PatternKind Kind
			{
				get
				{
					return LinqExpressionNormalizer.PatternKind.Compare;
				}
			}

			// Token: 0x04001B12 RID: 6930
			internal readonly Expression Left;

			// Token: 0x04001B13 RID: 6931
			internal readonly Expression Right;
		}
	}
}
