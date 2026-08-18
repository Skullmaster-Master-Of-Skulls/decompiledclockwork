using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Data.Entity.Core.Objects.ELinq
{
	// Token: 0x02000562 RID: 1378
	internal class LinqExpressionNormalizer : EntityExpressionVisitor
	{
		// Token: 0x06003540 RID: 13632 RVA: 0x000FB9C0 File Offset: 0x000F9BC0
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
			if (this._patterns.TryGetValue(b.Left, out pattern) && pattern.Kind == LinqExpressionNormalizer.PatternKind.Compare && LinqExpressionNormalizer.IsConstantZero(b.Right))
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

		// Token: 0x06003541 RID: 13633 RVA: 0x000FBA6C File Offset: 0x000F9C6C
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

		// Token: 0x06003542 RID: 13634 RVA: 0x000FBB0B File Offset: 0x000F9D0B
		private static bool IsConstantZero(Expression expression)
		{
			return expression.NodeType == ExpressionType.Constant && ((ConstantExpression)expression).Value.Equals(0);
		}

		// Token: 0x06003543 RID: 13635 RVA: 0x000FBB30 File Offset: 0x000F9D30
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
		internal override Expression VisitMethodCall(MethodCallExpression m)
		{
			m = (MethodCallExpression)base.VisitMethodCall(m);
			if (m.Method.IsStatic)
			{
				if (m.Method.Name.StartsWith("op_", StringComparison.Ordinal))
				{
					string name;
					if (m.Arguments.Count == 2 && (name = m.Method.Name) != null)
					{
						if (<PrivateImplementationDetails>{000F5452-2AD1-45BF-987B-3043022F9799}.$$method0x600319e-1 == null)
						{
							<PrivateImplementationDetails>{000F5452-2AD1-45BF-987B-3043022F9799}.$$method0x600319e-1 = new Dictionary<string, int>(14)
							{
								{
									"op_Equality",
									0
								},
								{
									"op_Inequality",
									1
								},
								{
									"op_GreaterThan",
									2
								},
								{
									"op_GreaterThanOrEqual",
									3
								},
								{
									"op_LessThan",
									4
								},
								{
									"op_LessThanOrEqual",
									5
								},
								{
									"op_Multiply",
									6
								},
								{
									"op_Subtraction",
									7
								},
								{
									"op_Addition",
									8
								},
								{
									"op_Division",
									9
								},
								{
									"op_Modulus",
									10
								},
								{
									"op_BitwiseAnd",
									11
								},
								{
									"op_BitwiseOr",
									12
								},
								{
									"op_ExclusiveOr",
									13
								}
							};
						}
						int num;
						if (<PrivateImplementationDetails>{000F5452-2AD1-45BF-987B-3043022F9799}.$$method0x600319e-1.TryGetValue(name, out num))
						{
							switch (num)
							{
							case 0:
								return Expression.Equal(m.Arguments[0], m.Arguments[1], false, m.Method);
							case 1:
								return Expression.NotEqual(m.Arguments[0], m.Arguments[1], false, m.Method);
							case 2:
								return Expression.GreaterThan(m.Arguments[0], m.Arguments[1], false, m.Method);
							case 3:
								return Expression.GreaterThanOrEqual(m.Arguments[0], m.Arguments[1], false, m.Method);
							case 4:
								return Expression.LessThan(m.Arguments[0], m.Arguments[1], false, m.Method);
							case 5:
								return Expression.LessThanOrEqual(m.Arguments[0], m.Arguments[1], false, m.Method);
							case 6:
								return Expression.Multiply(m.Arguments[0], m.Arguments[1], m.Method);
							case 7:
								return Expression.Subtract(m.Arguments[0], m.Arguments[1], m.Method);
							case 8:
								return Expression.Add(m.Arguments[0], m.Arguments[1], m.Method);
							case 9:
								return Expression.Divide(m.Arguments[0], m.Arguments[1], m.Method);
							case 10:
								return Expression.Modulo(m.Arguments[0], m.Arguments[1], m.Method);
							case 11:
								return Expression.And(m.Arguments[0], m.Arguments[1], m.Method);
							case 12:
								return Expression.Or(m.Arguments[0], m.Arguments[1], m.Method);
							case 13:
								return Expression.ExclusiveOr(m.Arguments[0], m.Arguments[1], m.Method);
							}
						}
					}
					string name2;
					if (m.Arguments.Count == 1 && (name2 = m.Method.Name) != null)
					{
						if (<PrivateImplementationDetails>{000F5452-2AD1-45BF-987B-3043022F9799}.$$method0x600319e-2 == null)
						{
							<PrivateImplementationDetails>{000F5452-2AD1-45BF-987B-3043022F9799}.$$method0x600319e-2 = new Dictionary<string, int>(6)
							{
								{
									"op_UnaryNegation",
									0
								},
								{
									"op_UnaryPlus",
									1
								},
								{
									"op_Explicit",
									2
								},
								{
									"op_Implicit",
									3
								},
								{
									"op_OnesComplement",
									4
								},
								{
									"op_False",
									5
								}
							};
						}
						int num2;
						if (<PrivateImplementationDetails>{000F5452-2AD1-45BF-987B-3043022F9799}.$$method0x600319e-2.TryGetValue(name2, out num2))
						{
							switch (num2)
							{
							case 0:
								return Expression.Negate(m.Arguments[0], m.Method);
							case 1:
								return Expression.UnaryPlus(m.Arguments[0], m.Method);
							case 2:
							case 3:
								return Expression.Convert(m.Arguments[0], m.Type, m.Method);
							case 4:
							case 5:
								return Expression.Not(m.Arguments[0], m.Method);
							}
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
					if (declaringType.IsGenericType() && declaringType.GetGenericTypeDefinition() == typeof(List<>) && ReflectionUtil.TryLookupMethod(SequenceMethod.Contains, out methodInfo))
					{
						MethodInfo method = methodInfo.MakeGenericMethod(declaringType.GetGenericArguments());
						return Expression.Call(method, m.Object, m.Arguments[0]);
					}
				}
			}
			return LinqExpressionNormalizer.NormalizePredicateArgument(m);
		}

		// Token: 0x06003544 RID: 13636 RVA: 0x000FC234 File Offset: 0x000FA434
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

		// Token: 0x06003545 RID: 13637 RVA: 0x000FC28C File Offset: 0x000FA48C
		private static bool HasPredicateArgument(MethodCallExpression callExpression, out int argumentOrdinal)
		{
			argumentOrdinal = 0;
			bool result = false;
			SequenceMethod sequenceMethod;
			if (2 <= callExpression.Arguments.Count && ReflectionUtil.TryIdentifySequenceMethod(callExpression.Method, out sequenceMethod))
			{
				SequenceMethod sequenceMethod2 = sequenceMethod;
				if (sequenceMethod2 <= SequenceMethod.SkipWhileOrdinal)
				{
					switch (sequenceMethod2)
					{
					case SequenceMethod.Where:
					case SequenceMethod.WhereOrdinal:
						break;
					default:
						switch (sequenceMethod2)
						{
						case SequenceMethod.TakeWhile:
						case SequenceMethod.TakeWhileOrdinal:
						case SequenceMethod.SkipWhile:
						case SequenceMethod.SkipWhileOrdinal:
							break;
						case SequenceMethod.Skip:
							return result;
						default:
							return result;
						}
						break;
					}
				}
				else
				{
					switch (sequenceMethod2)
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
						switch (sequenceMethod2)
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

		// Token: 0x06003546 RID: 13638 RVA: 0x000FC354 File Offset: 0x000FA554
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

		// Token: 0x06003547 RID: 13639 RVA: 0x000FC438 File Offset: 0x000FA638
		private static bool RelationalOperatorPlaceholder<TLeft, TRight>(TLeft left, TRight right)
		{
			return object.ReferenceEquals(left, right);
		}

		// Token: 0x06003548 RID: 13640 RVA: 0x000FC44C File Offset: 0x000FA64C
		private static BinaryExpression CreateRelationalOperator(ExpressionType op, Expression left, Expression right)
		{
			BinaryExpression result;
			LinqExpressionNormalizer.TryCreateRelationalOperator(op, left, right, out result);
			return result;
		}

		// Token: 0x06003549 RID: 13641 RVA: 0x000FC468 File Offset: 0x000FA668
		private static bool TryCreateRelationalOperator(ExpressionType op, Expression left, Expression right, out BinaryExpression result)
		{
			MethodInfo method = LinqExpressionNormalizer.RelationalOperatorPlaceholderMethod.MakeGenericMethod(new Type[]
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

		// Token: 0x0600354A RID: 13642 RVA: 0x000FC524 File Offset: 0x000FA724
		private Expression CreateCompareExpression(Expression left, Expression right)
		{
			Expression expression = Expression.Condition(LinqExpressionNormalizer.CreateRelationalOperator(ExpressionType.Equal, left, right), Expression.Constant(0), Expression.Condition(LinqExpressionNormalizer.CreateRelationalOperator(ExpressionType.GreaterThan, left, right), Expression.Constant(1), Expression.Constant(-1)));
			this._patterns[expression] = new LinqExpressionNormalizer.ComparePattern(left, right);
			return expression;
		}

		// Token: 0x040013F8 RID: 5112
		private const bool LiftToNull = false;

		// Token: 0x040013F9 RID: 5113
		private readonly Dictionary<Expression, LinqExpressionNormalizer.Pattern> _patterns = new Dictionary<Expression, LinqExpressionNormalizer.Pattern>();

		// Token: 0x040013FA RID: 5114
		internal static readonly MethodInfo RelationalOperatorPlaceholderMethod = typeof(LinqExpressionNormalizer).GetOnlyDeclaredMethod("RelationalOperatorPlaceholder");

		// Token: 0x02000563 RID: 1379
		private abstract class Pattern
		{
			// Token: 0x170007F5 RID: 2037
			// (get) Token: 0x0600354D RID: 13645
			internal abstract LinqExpressionNormalizer.PatternKind Kind { get; }
		}

		// Token: 0x02000564 RID: 1380
		private enum PatternKind
		{
			// Token: 0x040013FC RID: 5116
			Compare
		}

		// Token: 0x02000565 RID: 1381
		private sealed class ComparePattern : LinqExpressionNormalizer.Pattern
		{
			// Token: 0x0600354F RID: 13647 RVA: 0x000FC5B9 File Offset: 0x000FA7B9
			internal ComparePattern(Expression left, Expression right)
			{
				this.Left = left;
				this.Right = right;
			}

			// Token: 0x170007F6 RID: 2038
			// (get) Token: 0x06003550 RID: 13648 RVA: 0x000FC5CF File Offset: 0x000FA7CF
			internal override LinqExpressionNormalizer.PatternKind Kind
			{
				get
				{
					return LinqExpressionNormalizer.PatternKind.Compare;
				}
			}

			// Token: 0x040013FD RID: 5117
			internal readonly Expression Left;

			// Token: 0x040013FE RID: 5118
			internal readonly Expression Right;
		}
	}
}
