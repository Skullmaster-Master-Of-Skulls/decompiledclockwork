using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;
using System.Globalization;
using System.IO;
using System.Linq.Expressions.Compiler;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace System.Linq.Expressions
{
	// Token: 0x02000216 RID: 534
	[__DynamicallyInvokable]
	public abstract class Expression
	{
		// Token: 0x0600121B RID: 4635 RVA: 0x0003C870 File Offset: 0x0003AA70
		[__DynamicallyInvokable]
		public static BinaryExpression Assign(Expression left, Expression right)
		{
			Expression.RequiresCanWrite(left, "left");
			Expression.RequiresCanRead(right, "right");
			TypeUtils.ValidateType(left.Type);
			TypeUtils.ValidateType(right.Type);
			if (!TypeUtils.AreReferenceAssignable(left.Type, right.Type))
			{
				throw Error.ExpressionTypeDoesNotMatchAssignment(right.Type, left.Type);
			}
			return new AssignBinaryExpression(left, right);
		}

		// Token: 0x0600121C RID: 4636 RVA: 0x0003C8D8 File Offset: 0x0003AAD8
		private static BinaryExpression GetUserDefinedBinaryOperator(ExpressionType binaryType, string name, Expression left, Expression right, bool liftToNull)
		{
			MethodInfo userDefinedBinaryOperator = Expression.GetUserDefinedBinaryOperator(binaryType, left.Type, right.Type, name);
			if (userDefinedBinaryOperator != null)
			{
				return new MethodBinaryExpression(binaryType, left, right, userDefinedBinaryOperator.ReturnType, userDefinedBinaryOperator);
			}
			if (left.Type.IsNullableType() && right.Type.IsNullableType())
			{
				Type nonNullableType = left.Type.GetNonNullableType();
				Type nonNullableType2 = right.Type.GetNonNullableType();
				userDefinedBinaryOperator = Expression.GetUserDefinedBinaryOperator(binaryType, nonNullableType, nonNullableType2, name);
				if (userDefinedBinaryOperator != null && userDefinedBinaryOperator.ReturnType.IsValueType && !userDefinedBinaryOperator.ReturnType.IsNullableType())
				{
					if (userDefinedBinaryOperator.ReturnType != typeof(bool) || liftToNull)
					{
						return new MethodBinaryExpression(binaryType, left, right, TypeUtils.GetNullableType(userDefinedBinaryOperator.ReturnType), userDefinedBinaryOperator);
					}
					return new MethodBinaryExpression(binaryType, left, right, typeof(bool), userDefinedBinaryOperator);
				}
			}
			return null;
		}

		// Token: 0x0600121D RID: 4637 RVA: 0x0003C9BC File Offset: 0x0003ABBC
		private static BinaryExpression GetMethodBasedBinaryOperator(ExpressionType binaryType, Expression left, Expression right, MethodInfo method, bool liftToNull)
		{
			Expression.ValidateOperator(method);
			ParameterInfo[] parametersCached = method.GetParametersCached();
			if (parametersCached.Length != 2)
			{
				throw Error.IncorrectNumberOfMethodCallArguments(method);
			}
			if (Expression.ParameterIsAssignable(parametersCached[0], left.Type) && Expression.ParameterIsAssignable(parametersCached[1], right.Type))
			{
				Expression.ValidateParamswithOperandsOrThrow(parametersCached[0].ParameterType, left.Type, binaryType, method.Name);
				Expression.ValidateParamswithOperandsOrThrow(parametersCached[1].ParameterType, right.Type, binaryType, method.Name);
				return new MethodBinaryExpression(binaryType, left, right, method.ReturnType, method);
			}
			if (!left.Type.IsNullableType() || !right.Type.IsNullableType() || !Expression.ParameterIsAssignable(parametersCached[0], left.Type.GetNonNullableType()) || !Expression.ParameterIsAssignable(parametersCached[1], right.Type.GetNonNullableType()) || !method.ReturnType.IsValueType || method.ReturnType.IsNullableType())
			{
				throw Error.OperandTypesDoNotMatchParameters(binaryType, method.Name);
			}
			if (method.ReturnType != typeof(bool) || liftToNull)
			{
				return new MethodBinaryExpression(binaryType, left, right, TypeUtils.GetNullableType(method.ReturnType), method);
			}
			return new MethodBinaryExpression(binaryType, left, right, typeof(bool), method);
		}

		// Token: 0x0600121E RID: 4638 RVA: 0x0003CB00 File Offset: 0x0003AD00
		private static BinaryExpression GetMethodBasedAssignOperator(ExpressionType binaryType, Expression left, Expression right, MethodInfo method, LambdaExpression conversion, bool liftToNull)
		{
			BinaryExpression binaryExpression = Expression.GetMethodBasedBinaryOperator(binaryType, left, right, method, liftToNull);
			if (conversion == null)
			{
				if (!TypeUtils.AreReferenceAssignable(left.Type, binaryExpression.Type))
				{
					throw Error.UserDefinedOpMustHaveValidReturnType(binaryType, binaryExpression.Method.Name);
				}
			}
			else
			{
				Expression.ValidateOpAssignConversionLambda(conversion, binaryExpression.Left, binaryExpression.Method, binaryExpression.NodeType);
				binaryExpression = new OpAssignMethodConversionBinaryExpression(binaryExpression.NodeType, binaryExpression.Left, binaryExpression.Right, binaryExpression.Left.Type, binaryExpression.Method, conversion);
			}
			return binaryExpression;
		}

		// Token: 0x0600121F RID: 4639 RVA: 0x0003CB8C File Offset: 0x0003AD8C
		private static BinaryExpression GetUserDefinedBinaryOperatorOrThrow(ExpressionType binaryType, string name, Expression left, Expression right, bool liftToNull)
		{
			BinaryExpression userDefinedBinaryOperator = Expression.GetUserDefinedBinaryOperator(binaryType, name, left, right, liftToNull);
			if (userDefinedBinaryOperator != null)
			{
				ParameterInfo[] parametersCached = userDefinedBinaryOperator.Method.GetParametersCached();
				Expression.ValidateParamswithOperandsOrThrow(parametersCached[0].ParameterType, left.Type, binaryType, name);
				Expression.ValidateParamswithOperandsOrThrow(parametersCached[1].ParameterType, right.Type, binaryType, name);
				return userDefinedBinaryOperator;
			}
			throw Error.BinaryOperatorNotDefined(binaryType, left.Type, right.Type);
		}

		// Token: 0x06001220 RID: 4640 RVA: 0x0003CBF8 File Offset: 0x0003ADF8
		private static BinaryExpression GetUserDefinedAssignOperatorOrThrow(ExpressionType binaryType, string name, Expression left, Expression right, LambdaExpression conversion, bool liftToNull)
		{
			BinaryExpression binaryExpression = Expression.GetUserDefinedBinaryOperatorOrThrow(binaryType, name, left, right, liftToNull);
			if (conversion == null)
			{
				if (!TypeUtils.AreReferenceAssignable(left.Type, binaryExpression.Type))
				{
					throw Error.UserDefinedOpMustHaveValidReturnType(binaryType, binaryExpression.Method.Name);
				}
			}
			else
			{
				Expression.ValidateOpAssignConversionLambda(conversion, binaryExpression.Left, binaryExpression.Method, binaryExpression.NodeType);
				binaryExpression = new OpAssignMethodConversionBinaryExpression(binaryExpression.NodeType, binaryExpression.Left, binaryExpression.Right, binaryExpression.Left.Type, binaryExpression.Method, conversion);
			}
			return binaryExpression;
		}

		// Token: 0x06001221 RID: 4641 RVA: 0x0003CC84 File Offset: 0x0003AE84
		private static MethodInfo GetUserDefinedBinaryOperator(ExpressionType binaryType, Type leftType, Type rightType, string name)
		{
			Type[] types = new Type[]
			{
				leftType,
				rightType
			};
			Type nonNullableType = leftType.GetNonNullableType();
			Type nonNullableType2 = rightType.GetNonNullableType();
			BindingFlags bindingAttr = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
			MethodInfo methodInfo = nonNullableType.GetMethodValidated(name, bindingAttr, null, types, null);
			if (methodInfo == null && !TypeUtils.AreEquivalent(leftType, rightType))
			{
				methodInfo = nonNullableType2.GetMethodValidated(name, bindingAttr, null, types, null);
			}
			if (Expression.IsLiftingConditionalLogicalOperator(leftType, rightType, methodInfo, binaryType))
			{
				methodInfo = Expression.GetUserDefinedBinaryOperator(binaryType, nonNullableType, nonNullableType2, name);
			}
			return methodInfo;
		}

		// Token: 0x06001222 RID: 4642 RVA: 0x0003CCF7 File Offset: 0x0003AEF7
		private static bool IsLiftingConditionalLogicalOperator(Type left, Type right, MethodInfo method, ExpressionType binaryType)
		{
			return right.IsNullableType() && left.IsNullableType() && method == null && (binaryType == ExpressionType.AndAlso || binaryType == ExpressionType.OrElse);
		}

		// Token: 0x06001223 RID: 4643 RVA: 0x0003CD20 File Offset: 0x0003AF20
		internal static bool ParameterIsAssignable(ParameterInfo pi, Type argType)
		{
			Type type = pi.ParameterType;
			if (type.IsByRef)
			{
				type = type.GetElementType();
			}
			return TypeUtils.AreReferenceAssignable(type, argType);
		}

		// Token: 0x06001224 RID: 4644 RVA: 0x0003CD4A File Offset: 0x0003AF4A
		private static void ValidateParamswithOperandsOrThrow(Type paramType, Type operandType, ExpressionType exprType, string name)
		{
			if (paramType.IsNullableType() && !operandType.IsNullableType())
			{
				throw Error.OperandTypesDoNotMatchParameters(exprType, name);
			}
		}

		// Token: 0x06001225 RID: 4645 RVA: 0x0003CD69 File Offset: 0x0003AF69
		private static void ValidateOperator(MethodInfo method)
		{
			Expression.ValidateMethodInfo(method);
			if (!method.IsStatic)
			{
				throw Error.UserDefinedOperatorMustBeStatic(method);
			}
			if (method.ReturnType == typeof(void))
			{
				throw Error.UserDefinedOperatorMustNotBeVoid(method);
			}
		}

		// Token: 0x06001226 RID: 4646 RVA: 0x0003CD9E File Offset: 0x0003AF9E
		private static void ValidateMethodInfo(MethodInfo method)
		{
			if (method.IsGenericMethodDefinition)
			{
				throw Error.MethodIsGeneric(method);
			}
			if (method.ContainsGenericParameters)
			{
				throw Error.MethodContainsGenericParameters(method);
			}
		}

		// Token: 0x06001227 RID: 4647 RVA: 0x0003CDC0 File Offset: 0x0003AFC0
		private static bool IsNullComparison(Expression left, Expression right)
		{
			return (Expression.IsNullConstant(left) && !Expression.IsNullConstant(right) && right.Type.IsNullableType()) || (Expression.IsNullConstant(right) && !Expression.IsNullConstant(left) && left.Type.IsNullableType());
		}

		// Token: 0x06001228 RID: 4648 RVA: 0x0003CE0C File Offset: 0x0003B00C
		private static bool IsNullConstant(Expression e)
		{
			ConstantExpression constantExpression = e as ConstantExpression;
			return constantExpression != null && constantExpression.Value == null;
		}

		// Token: 0x06001229 RID: 4649 RVA: 0x0003CE30 File Offset: 0x0003B030
		private static void ValidateUserDefinedConditionalLogicOperator(ExpressionType nodeType, Type left, Type right, MethodInfo method)
		{
			Expression.ValidateOperator(method);
			ParameterInfo[] parametersCached = method.GetParametersCached();
			if (parametersCached.Length != 2)
			{
				throw Error.IncorrectNumberOfMethodCallArguments(method);
			}
			if (!Expression.ParameterIsAssignable(parametersCached[0], left) && (!left.IsNullableType() || !Expression.ParameterIsAssignable(parametersCached[0], left.GetNonNullableType())))
			{
				throw Error.OperandTypesDoNotMatchParameters(nodeType, method.Name);
			}
			if (!Expression.ParameterIsAssignable(parametersCached[1], right) && (!right.IsNullableType() || !Expression.ParameterIsAssignable(parametersCached[1], right.GetNonNullableType())))
			{
				throw Error.OperandTypesDoNotMatchParameters(nodeType, method.Name);
			}
			if (parametersCached[0].ParameterType != parametersCached[1].ParameterType)
			{
				throw Error.UserDefinedOpMustHaveConsistentTypes(nodeType, method.Name);
			}
			if (method.ReturnType != parametersCached[0].ParameterType)
			{
				throw Error.UserDefinedOpMustHaveConsistentTypes(nodeType, method.Name);
			}
			if (Expression.IsValidLiftedConditionalLogicalOperator(left, right, parametersCached))
			{
				left = left.GetNonNullableType();
				right = left.GetNonNullableType();
			}
			MethodInfo booleanOperator = TypeUtils.GetBooleanOperator(method.DeclaringType, "op_True");
			MethodInfo booleanOperator2 = TypeUtils.GetBooleanOperator(method.DeclaringType, "op_False");
			if (booleanOperator == null || booleanOperator.ReturnType != typeof(bool) || booleanOperator2 == null || booleanOperator2.ReturnType != typeof(bool))
			{
				throw Error.LogicalOperatorMustHaveBooleanOperators(nodeType, method.Name);
			}
			Expression.VerifyOpTrueFalse(nodeType, left, booleanOperator2);
			Expression.VerifyOpTrueFalse(nodeType, left, booleanOperator);
		}

		// Token: 0x0600122A RID: 4650 RVA: 0x0003CFB0 File Offset: 0x0003B1B0
		private static void VerifyOpTrueFalse(ExpressionType nodeType, Type left, MethodInfo opTrue)
		{
			ParameterInfo[] parametersCached = opTrue.GetParametersCached();
			if (parametersCached.Length != 1)
			{
				throw Error.IncorrectNumberOfMethodCallArguments(opTrue);
			}
			if (!Expression.ParameterIsAssignable(parametersCached[0], left) && (!left.IsNullableType() || !Expression.ParameterIsAssignable(parametersCached[0], left.GetNonNullableType())))
			{
				throw Error.OperandTypesDoNotMatchParameters(nodeType, opTrue.Name);
			}
		}

		// Token: 0x0600122B RID: 4651 RVA: 0x0003D006 File Offset: 0x0003B206
		private static bool IsValidLiftedConditionalLogicalOperator(Type left, Type right, ParameterInfo[] pms)
		{
			return TypeUtils.AreEquivalent(left, right) && right.IsNullableType() && TypeUtils.AreEquivalent(pms[1].ParameterType, right.GetNonNullableType());
		}

		// Token: 0x0600122C RID: 4652 RVA: 0x0003D02E File Offset: 0x0003B22E
		[__DynamicallyInvokable]
		public static BinaryExpression MakeBinary(ExpressionType binaryType, Expression left, Expression right)
		{
			return Expression.MakeBinary(binaryType, left, right, false, null, null);
		}

		// Token: 0x0600122D RID: 4653 RVA: 0x0003D03B File Offset: 0x0003B23B
		[__DynamicallyInvokable]
		public static BinaryExpression MakeBinary(ExpressionType binaryType, Expression left, Expression right, bool liftToNull, MethodInfo method)
		{
			return Expression.MakeBinary(binaryType, left, right, liftToNull, method, null);
		}

		// Token: 0x0600122E RID: 4654 RVA: 0x0003D04C File Offset: 0x0003B24C
		[__DynamicallyInvokable]
		public static BinaryExpression MakeBinary(ExpressionType binaryType, Expression left, Expression right, bool liftToNull, MethodInfo method, LambdaExpression conversion)
		{
			switch (binaryType)
			{
			case ExpressionType.Add:
				return Expression.Add(left, right, method);
			case ExpressionType.AddChecked:
				return Expression.AddChecked(left, right, method);
			case ExpressionType.And:
				return Expression.And(left, right, method);
			case ExpressionType.AndAlso:
				return Expression.AndAlso(left, right, method);
			case ExpressionType.ArrayIndex:
				return Expression.ArrayIndex(left, right);
			case ExpressionType.Coalesce:
				return Expression.Coalesce(left, right, conversion);
			case ExpressionType.Divide:
				return Expression.Divide(left, right, method);
			case ExpressionType.Equal:
				return Expression.Equal(left, right, liftToNull, method);
			case ExpressionType.ExclusiveOr:
				return Expression.ExclusiveOr(left, right, method);
			case ExpressionType.GreaterThan:
				return Expression.GreaterThan(left, right, liftToNull, method);
			case ExpressionType.GreaterThanOrEqual:
				return Expression.GreaterThanOrEqual(left, right, liftToNull, method);
			case ExpressionType.LeftShift:
				return Expression.LeftShift(left, right, method);
			case ExpressionType.LessThan:
				return Expression.LessThan(left, right, liftToNull, method);
			case ExpressionType.LessThanOrEqual:
				return Expression.LessThanOrEqual(left, right, liftToNull, method);
			case ExpressionType.Modulo:
				return Expression.Modulo(left, right, method);
			case ExpressionType.Multiply:
				return Expression.Multiply(left, right, method);
			case ExpressionType.MultiplyChecked:
				return Expression.MultiplyChecked(left, right, method);
			case ExpressionType.NotEqual:
				return Expression.NotEqual(left, right, liftToNull, method);
			case ExpressionType.Or:
				return Expression.Or(left, right, method);
			case ExpressionType.OrElse:
				return Expression.OrElse(left, right, method);
			case ExpressionType.Power:
				return Expression.Power(left, right, method);
			case ExpressionType.RightShift:
				return Expression.RightShift(left, right, method);
			case ExpressionType.Subtract:
				return Expression.Subtract(left, right, method);
			case ExpressionType.SubtractChecked:
				return Expression.SubtractChecked(left, right, method);
			case ExpressionType.Assign:
				return Expression.Assign(left, right);
			case ExpressionType.AddAssign:
				return Expression.AddAssign(left, right, method, conversion);
			case ExpressionType.AndAssign:
				return Expression.AndAssign(left, right, method, conversion);
			case ExpressionType.DivideAssign:
				return Expression.DivideAssign(left, right, method, conversion);
			case ExpressionType.ExclusiveOrAssign:
				return Expression.ExclusiveOrAssign(left, right, method, conversion);
			case ExpressionType.LeftShiftAssign:
				return Expression.LeftShiftAssign(left, right, method, conversion);
			case ExpressionType.ModuloAssign:
				return Expression.ModuloAssign(left, right, method, conversion);
			case ExpressionType.MultiplyAssign:
				return Expression.MultiplyAssign(left, right, method, conversion);
			case ExpressionType.OrAssign:
				return Expression.OrAssign(left, right, method, conversion);
			case ExpressionType.PowerAssign:
				return Expression.PowerAssign(left, right, method, conversion);
			case ExpressionType.RightShiftAssign:
				return Expression.RightShiftAssign(left, right, method, conversion);
			case ExpressionType.SubtractAssign:
				return Expression.SubtractAssign(left, right, method, conversion);
			case ExpressionType.AddAssignChecked:
				return Expression.AddAssignChecked(left, right, method, conversion);
			case ExpressionType.MultiplyAssignChecked:
				return Expression.MultiplyAssignChecked(left, right, method, conversion);
			case ExpressionType.SubtractAssignChecked:
				return Expression.SubtractAssignChecked(left, right, method, conversion);
			}
			throw Error.UnhandledBinary(binaryType);
		}

		// Token: 0x0600122F RID: 4655 RVA: 0x0003D347 File Offset: 0x0003B547
		[__DynamicallyInvokable]
		public static BinaryExpression Equal(Expression left, Expression right)
		{
			return Expression.Equal(left, right, false, null);
		}

		// Token: 0x06001230 RID: 4656 RVA: 0x0003D352 File Offset: 0x0003B552
		[__DynamicallyInvokable]
		public static BinaryExpression Equal(Expression left, Expression right, bool liftToNull, MethodInfo method)
		{
			Expression.RequiresCanRead(left, "left");
			Expression.RequiresCanRead(right, "right");
			if (method == null)
			{
				return Expression.GetEqualityComparisonOperator(ExpressionType.Equal, "op_Equality", left, right, liftToNull);
			}
			return Expression.GetMethodBasedBinaryOperator(ExpressionType.Equal, left, right, method, liftToNull);
		}

		// Token: 0x06001231 RID: 4657 RVA: 0x0003D390 File Offset: 0x0003B590
		[__DynamicallyInvokable]
		public static BinaryExpression ReferenceEqual(Expression left, Expression right)
		{
			Expression.RequiresCanRead(left, "left");
			Expression.RequiresCanRead(right, "right");
			if (TypeUtils.HasReferenceEquality(left.Type, right.Type))
			{
				return new LogicalBinaryExpression(ExpressionType.Equal, left, right);
			}
			throw Error.ReferenceEqualityNotDefined(left.Type, right.Type);
		}

		// Token: 0x06001232 RID: 4658 RVA: 0x0003D3E1 File Offset: 0x0003B5E1
		[__DynamicallyInvokable]
		public static BinaryExpression NotEqual(Expression left, Expression right)
		{
			return Expression.NotEqual(left, right, false, null);
		}

		// Token: 0x06001233 RID: 4659 RVA: 0x0003D3EC File Offset: 0x0003B5EC
		[__DynamicallyInvokable]
		public static BinaryExpression NotEqual(Expression left, Expression right, bool liftToNull, MethodInfo method)
		{
			Expression.RequiresCanRead(left, "left");
			Expression.RequiresCanRead(right, "right");
			if (method == null)
			{
				return Expression.GetEqualityComparisonOperator(ExpressionType.NotEqual, "op_Inequality", left, right, liftToNull);
			}
			return Expression.GetMethodBasedBinaryOperator(ExpressionType.NotEqual, left, right, method, liftToNull);
		}

		// Token: 0x06001234 RID: 4660 RVA: 0x0003D428 File Offset: 0x0003B628
		[__DynamicallyInvokable]
		public static BinaryExpression ReferenceNotEqual(Expression left, Expression right)
		{
			Expression.RequiresCanRead(left, "left");
			Expression.RequiresCanRead(right, "right");
			if (TypeUtils.HasReferenceEquality(left.Type, right.Type))
			{
				return new LogicalBinaryExpression(ExpressionType.NotEqual, left, right);
			}
			throw Error.ReferenceEqualityNotDefined(left.Type, right.Type);
		}

		// Token: 0x06001235 RID: 4661 RVA: 0x0003D47C File Offset: 0x0003B67C
		private static BinaryExpression GetEqualityComparisonOperator(ExpressionType binaryType, string opName, Expression left, Expression right, bool liftToNull)
		{
			if (left.Type == right.Type && (TypeUtils.IsNumeric(left.Type) || left.Type == typeof(object) || TypeUtils.IsBool(left.Type) || left.Type.GetNonNullableType().IsEnum))
			{
				if (left.Type.IsNullableType() && liftToNull)
				{
					return new SimpleBinaryExpression(binaryType, left, right, typeof(bool?));
				}
				return new LogicalBinaryExpression(binaryType, left, right);
			}
			else
			{
				BinaryExpression userDefinedBinaryOperator = Expression.GetUserDefinedBinaryOperator(binaryType, opName, left, right, liftToNull);
				if (userDefinedBinaryOperator != null)
				{
					return userDefinedBinaryOperator;
				}
				if (!TypeUtils.HasBuiltInEqualityOperator(left.Type, right.Type) && !Expression.IsNullComparison(left, right))
				{
					throw Error.BinaryOperatorNotDefined(binaryType, left.Type, right.Type);
				}
				if (left.Type.IsNullableType() && liftToNull)
				{
					return new SimpleBinaryExpression(binaryType, left, right, typeof(bool?));
				}
				return new LogicalBinaryExpression(binaryType, left, right);
			}
		}

		// Token: 0x06001236 RID: 4662 RVA: 0x0003D57B File Offset: 0x0003B77B
		[__DynamicallyInvokable]
		public static BinaryExpression GreaterThan(Expression left, Expression right)
		{
			return Expression.GreaterThan(left, right, false, null);
		}

		// Token: 0x06001237 RID: 4663 RVA: 0x0003D586 File Offset: 0x0003B786
		[__DynamicallyInvokable]
		public static BinaryExpression GreaterThan(Expression left, Expression right, bool liftToNull, MethodInfo method)
		{
			Expression.RequiresCanRead(left, "left");
			Expression.RequiresCanRead(right, "right");
			if (method == null)
			{
				return Expression.GetComparisonOperator(ExpressionType.GreaterThan, "op_GreaterThan", left, right, liftToNull);
			}
			return Expression.GetMethodBasedBinaryOperator(ExpressionType.GreaterThan, left, right, method, liftToNull);
		}

		// Token: 0x06001238 RID: 4664 RVA: 0x0003D5C2 File Offset: 0x0003B7C2
		[__DynamicallyInvokable]
		public static BinaryExpression LessThan(Expression left, Expression right)
		{
			return Expression.LessThan(left, right, false, null);
		}

		// Token: 0x06001239 RID: 4665 RVA: 0x0003D5CD File Offset: 0x0003B7CD
		[__DynamicallyInvokable]
		public static BinaryExpression LessThan(Expression left, Expression right, bool liftToNull, MethodInfo method)
		{
			Expression.RequiresCanRead(left, "left");
			Expression.RequiresCanRead(right, "right");
			if (method == null)
			{
				return Expression.GetComparisonOperator(ExpressionType.LessThan, "op_LessThan", left, right, liftToNull);
			}
			return Expression.GetMethodBasedBinaryOperator(ExpressionType.LessThan, left, right, method, liftToNull);
		}

		// Token: 0x0600123A RID: 4666 RVA: 0x0003D609 File Offset: 0x0003B809
		[__DynamicallyInvokable]
		public static BinaryExpression GreaterThanOrEqual(Expression left, Expression right)
		{
			return Expression.GreaterThanOrEqual(left, right, false, null);
		}

		// Token: 0x0600123B RID: 4667 RVA: 0x0003D614 File Offset: 0x0003B814
		[__DynamicallyInvokable]
		public static BinaryExpression GreaterThanOrEqual(Expression left, Expression right, bool liftToNull, MethodInfo method)
		{
			Expression.RequiresCanRead(left, "left");
			Expression.RequiresCanRead(right, "right");
			if (method == null)
			{
				return Expression.GetComparisonOperator(ExpressionType.GreaterThanOrEqual, "op_GreaterThanOrEqual", left, right, liftToNull);
			}
			return Expression.GetMethodBasedBinaryOperator(ExpressionType.GreaterThanOrEqual, left, right, method, liftToNull);
		}

		// Token: 0x0600123C RID: 4668 RVA: 0x0003D650 File Offset: 0x0003B850
		[__DynamicallyInvokable]
		public static BinaryExpression LessThanOrEqual(Expression left, Expression right)
		{
			return Expression.LessThanOrEqual(left, right, false, null);
		}

		// Token: 0x0600123D RID: 4669 RVA: 0x0003D65B File Offset: 0x0003B85B
		[__DynamicallyInvokable]
		public static BinaryExpression LessThanOrEqual(Expression left, Expression right, bool liftToNull, MethodInfo method)
		{
			Expression.RequiresCanRead(left, "left");
			Expression.RequiresCanRead(right, "right");
			if (method == null)
			{
				return Expression.GetComparisonOperator(ExpressionType.LessThanOrEqual, "op_LessThanOrEqual", left, right, liftToNull);
			}
			return Expression.GetMethodBasedBinaryOperator(ExpressionType.LessThanOrEqual, left, right, method, liftToNull);
		}

		// Token: 0x0600123E RID: 4670 RVA: 0x0003D698 File Offset: 0x0003B898
		private static BinaryExpression GetComparisonOperator(ExpressionType binaryType, string opName, Expression left, Expression right, bool liftToNull)
		{
			if (!(left.Type == right.Type) || !TypeUtils.IsNumeric(left.Type))
			{
				return Expression.GetUserDefinedBinaryOperatorOrThrow(binaryType, opName, left, right, liftToNull);
			}
			if (left.Type.IsNullableType() && liftToNull)
			{
				return new SimpleBinaryExpression(binaryType, left, right, typeof(bool?));
			}
			return new LogicalBinaryExpression(binaryType, left, right);
		}

		// Token: 0x0600123F RID: 4671 RVA: 0x0003D6FC File Offset: 0x0003B8FC
		[__DynamicallyInvokable]
		public static BinaryExpression AndAlso(Expression left, Expression right)
		{
			return Expression.AndAlso(left, right, null);
		}

		// Token: 0x06001240 RID: 4672 RVA: 0x0003D708 File Offset: 0x0003B908
		[__DynamicallyInvokable]
		public static BinaryExpression AndAlso(Expression left, Expression right, MethodInfo method)
		{
			Expression.RequiresCanRead(left, "left");
			Expression.RequiresCanRead(right, "right");
			if (!(method == null))
			{
				Expression.ValidateUserDefinedConditionalLogicOperator(ExpressionType.AndAlso, left.Type, right.Type, method);
				Type type = (left.Type.IsNullableType() && TypeUtils.AreEquivalent(method.ReturnType, left.Type.GetNonNullableType())) ? left.Type : method.ReturnType;
				return new MethodBinaryExpression(ExpressionType.AndAlso, left, right, type, method);
			}
			if (left.Type == right.Type)
			{
				if (left.Type == typeof(bool))
				{
					return new LogicalBinaryExpression(ExpressionType.AndAlso, left, right);
				}
				if (left.Type == typeof(bool?))
				{
					return new SimpleBinaryExpression(ExpressionType.AndAlso, left, right, left.Type);
				}
			}
			method = Expression.GetUserDefinedBinaryOperator(ExpressionType.AndAlso, left.Type, right.Type, "op_BitwiseAnd");
			if (method != null)
			{
				Expression.ValidateUserDefinedConditionalLogicOperator(ExpressionType.AndAlso, left.Type, right.Type, method);
				Type type = (left.Type.IsNullableType() && TypeUtils.AreEquivalent(method.ReturnType, left.Type.GetNonNullableType())) ? left.Type : method.ReturnType;
				return new MethodBinaryExpression(ExpressionType.AndAlso, left, right, type, method);
			}
			throw Error.BinaryOperatorNotDefined(ExpressionType.AndAlso, left.Type, right.Type);
		}

		// Token: 0x06001241 RID: 4673 RVA: 0x0003D86D File Offset: 0x0003BA6D
		[__DynamicallyInvokable]
		public static BinaryExpression OrElse(Expression left, Expression right)
		{
			return Expression.OrElse(left, right, null);
		}

		// Token: 0x06001242 RID: 4674 RVA: 0x0003D878 File Offset: 0x0003BA78
		[__DynamicallyInvokable]
		public static BinaryExpression OrElse(Expression left, Expression right, MethodInfo method)
		{
			Expression.RequiresCanRead(left, "left");
			Expression.RequiresCanRead(right, "right");
			if (!(method == null))
			{
				Expression.ValidateUserDefinedConditionalLogicOperator(ExpressionType.OrElse, left.Type, right.Type, method);
				Type type = (left.Type.IsNullableType() && method.ReturnType == left.Type.GetNonNullableType()) ? left.Type : method.ReturnType;
				return new MethodBinaryExpression(ExpressionType.OrElse, left, right, type, method);
			}
			if (left.Type == right.Type)
			{
				if (left.Type == typeof(bool))
				{
					return new LogicalBinaryExpression(ExpressionType.OrElse, left, right);
				}
				if (left.Type == typeof(bool?))
				{
					return new SimpleBinaryExpression(ExpressionType.OrElse, left, right, left.Type);
				}
			}
			method = Expression.GetUserDefinedBinaryOperator(ExpressionType.OrElse, left.Type, right.Type, "op_BitwiseOr");
			if (method != null)
			{
				Expression.ValidateUserDefinedConditionalLogicOperator(ExpressionType.OrElse, left.Type, right.Type, method);
				Type type = (left.Type.IsNullableType() && method.ReturnType == left.Type.GetNonNullableType()) ? left.Type : method.ReturnType;
				return new MethodBinaryExpression(ExpressionType.OrElse, left, right, type, method);
			}
			throw Error.BinaryOperatorNotDefined(ExpressionType.OrElse, left.Type, right.Type);
		}

		// Token: 0x06001243 RID: 4675 RVA: 0x0003D9E5 File Offset: 0x0003BBE5
		[__DynamicallyInvokable]
		public static BinaryExpression Coalesce(Expression left, Expression right)
		{
			return Expression.Coalesce(left, right, null);
		}

		// Token: 0x06001244 RID: 4676 RVA: 0x0003D9F0 File Offset: 0x0003BBF0
		[__DynamicallyInvokable]
		public static BinaryExpression Coalesce(Expression left, Expression right, LambdaExpression conversion)
		{
			Expression.RequiresCanRead(left, "left");
			Expression.RequiresCanRead(right, "right");
			if (conversion == null)
			{
				Type type = Expression.ValidateCoalesceArgTypes(left.Type, right.Type);
				return new SimpleBinaryExpression(ExpressionType.Coalesce, left, right, type);
			}
			if (left.Type.IsValueType && !left.Type.IsNullableType())
			{
				throw Error.CoalesceUsedOnNonNullType();
			}
			Type type2 = conversion.Type;
			MethodInfo method = type2.GetMethod("Invoke");
			if (method.ReturnType == typeof(void))
			{
				throw Error.UserDefinedOperatorMustNotBeVoid(conversion);
			}
			ParameterInfo[] parametersCached = method.GetParametersCached();
			if (parametersCached.Length != 1)
			{
				throw Error.IncorrectNumberOfMethodCallArguments(conversion);
			}
			if (!TypeUtils.AreEquivalent(method.ReturnType, right.Type))
			{
				throw Error.OperandTypesDoNotMatchParameters(ExpressionType.Coalesce, conversion.ToString());
			}
			if (!Expression.ParameterIsAssignable(parametersCached[0], left.Type.GetNonNullableType()) && !Expression.ParameterIsAssignable(parametersCached[0], left.Type))
			{
				throw Error.OperandTypesDoNotMatchParameters(ExpressionType.Coalesce, conversion.ToString());
			}
			return new CoalesceConversionBinaryExpression(left, right, conversion);
		}

		// Token: 0x06001245 RID: 4677 RVA: 0x0003DAFC File Offset: 0x0003BCFC
		private static Type ValidateCoalesceArgTypes(Type left, Type right)
		{
			Type nonNullableType = left.GetNonNullableType();
			if (left.IsValueType && !left.IsNullableType())
			{
				throw Error.CoalesceUsedOnNonNullType();
			}
			if (left.IsNullableType() && TypeUtils.IsImplicitlyConvertible(right, nonNullableType))
			{
				return nonNullableType;
			}
			if (TypeUtils.IsImplicitlyConvertible(right, left))
			{
				return left;
			}
			if (TypeUtils.IsImplicitlyConvertible(nonNullableType, right))
			{
				return right;
			}
			throw Error.ArgumentTypesMustMatch();
		}

		// Token: 0x06001246 RID: 4678 RVA: 0x0003DB54 File Offset: 0x0003BD54
		[__DynamicallyInvokable]
		public static BinaryExpression Add(Expression left, Expression right)
		{
			return Expression.Add(left, right, null);
		}

		// Token: 0x06001247 RID: 4679 RVA: 0x0003DB60 File Offset: 0x0003BD60
		[__DynamicallyInvokable]
		public static BinaryExpression Add(Expression left, Expression right, MethodInfo method)
		{
			Expression.RequiresCanRead(left, "left");
			Expression.RequiresCanRead(right, "right");
			if (!(method == null))
			{
				return Expression.GetMethodBasedBinaryOperator(ExpressionType.Add, left, right, method, true);
			}
			if (left.Type == right.Type && TypeUtils.IsArithmetic(left.Type))
			{
				return new SimpleBinaryExpression(ExpressionType.Add, left, right, left.Type);
			}
			return Expression.GetUserDefinedBinaryOperatorOrThrow(ExpressionType.Add, "op_Addition", left, right, true);
		}

		// Token: 0x06001248 RID: 4680 RVA: 0x0003DBD4 File Offset: 0x0003BDD4
		[__DynamicallyInvokable]
		public static BinaryExpression AddAssign(Expression left, Expression right)
		{
			return Expression.AddAssign(left, right, null, null);
		}

		// Token: 0x06001249 RID: 4681 RVA: 0x0003DBDF File Offset: 0x0003BDDF
		[__DynamicallyInvokable]
		public static BinaryExpression AddAssign(Expression left, Expression right, MethodInfo method)
		{
			return Expression.AddAssign(left, right, method, null);
		}

		// Token: 0x0600124A RID: 4682 RVA: 0x0003DBEC File Offset: 0x0003BDEC
		[__DynamicallyInvokable]
		public static BinaryExpression AddAssign(Expression left, Expression right, MethodInfo method, LambdaExpression conversion)
		{
			Expression.RequiresCanRead(left, "left");
			Expression.RequiresCanWrite(left, "left");
			Expression.RequiresCanRead(right, "right");
			if (!(method == null))
			{
				return Expression.GetMethodBasedAssignOperator(ExpressionType.AddAssign, left, right, method, conversion, true);
			}
			if (!(left.Type == right.Type) || !TypeUtils.IsArithmetic(left.Type))
			{
				return Expression.GetUserDefinedAssignOperatorOrThrow(ExpressionType.AddAssign, "op_Addition", left, right, conversion, true);
			}
			if (conversion != null)
			{
				throw Error.ConversionIsNotSupportedForArithmeticTypes();
			}
			return new SimpleBinaryExpression(ExpressionType.AddAssign, left, right, left.Type);
		}

		// Token: 0x0600124B RID: 4683 RVA: 0x0003DC7C File Offset: 0x0003BE7C
		private static void ValidateOpAssignConversionLambda(LambdaExpression conversion, Expression left, MethodInfo method, ExpressionType nodeType)
		{
			Type type = conversion.Type;
			MethodInfo method2 = type.GetMethod("Invoke");
			ParameterInfo[] parametersCached = method2.GetParametersCached();
			if (parametersCached.Length != 1)
			{
				throw Error.IncorrectNumberOfMethodCallArguments(conversion);
			}
			if (!TypeUtils.AreEquivalent(method2.ReturnType, left.Type))
			{
				throw Error.OperandTypesDoNotMatchParameters(nodeType, conversion.ToString());
			}
			if (method != null && !TypeUtils.AreEquivalent(parametersCached[0].ParameterType, method.ReturnType))
			{
				throw Error.OverloadOperatorTypeDoesNotMatchConversionType(nodeType, conversion.ToString());
			}
		}

		// Token: 0x0600124C RID: 4684 RVA: 0x0003DD05 File Offset: 0x0003BF05
		[__DynamicallyInvokable]
		public static BinaryExpression AddAssignChecked(Expression left, Expression right)
		{
			return Expression.AddAssignChecked(left, right, null);
		}

		// Token: 0x0600124D RID: 4685 RVA: 0x0003DD0F File Offset: 0x0003BF0F
		[__DynamicallyInvokable]
		public static BinaryExpression AddAssignChecked(Expression left, Expression right, MethodInfo method)
		{
			return Expression.AddAssignChecked(left, right, method, null);
		}

		// Token: 0x0600124E RID: 4686 RVA: 0x0003DD1C File Offset: 0x0003BF1C
		[__DynamicallyInvokable]
		public static BinaryExpression AddAssignChecked(Expression left, Expression right, MethodInfo method, LambdaExpression conversion)
		{
			Expression.RequiresCanRead(left, "left");
			Expression.RequiresCanWrite(left, "left");
			Expression.RequiresCanRead(right, "right");
			if (!(method == null))
			{
				return Expression.GetMethodBasedAssignOperator(ExpressionType.AddAssignChecked, left, right, method, conversion, true);
			}
			if (!(left.Type == right.Type) || !TypeUtils.IsArithmetic(left.Type))
			{
				return Expression.GetUserDefinedAssignOperatorOrThrow(ExpressionType.AddAssignChecked, "op_Addition", left, right, conversion, true);
			}
			if (conversion != null)
			{
				throw Error.ConversionIsNotSupportedForArithmeticTypes();
			}
			return new SimpleBinaryExpression(ExpressionType.AddAssignChecked, left, right, left.Type);
		}

		// Token: 0x0600124F RID: 4687 RVA: 0x0003DDA9 File Offset: 0x0003BFA9
		[__DynamicallyInvokable]
		public static BinaryExpression AddChecked(Expression left, Expression right)
		{
			return Expression.AddChecked(left, right, null);
		}

		// Token: 0x06001250 RID: 4688 RVA: 0x0003DDB4 File Offset: 0x0003BFB4
		[__DynamicallyInvokable]
		public static BinaryExpression AddChecked(Expression left, Expression right, MethodInfo method)
		{
			Expression.RequiresCanRead(left, "left");
			Expression.RequiresCanRead(right, "right");
			if (!(method == null))
			{
				return Expression.GetMethodBasedBinaryOperator(ExpressionType.AddChecked, left, right, method, true);
			}
			if (left.Type == right.Type && TypeUtils.IsArithmetic(left.Type))
			{
				return new SimpleBinaryExpression(ExpressionType.AddChecked, left, right, left.Type);
			}
			return Expression.GetUserDefinedBinaryOperatorOrThrow(ExpressionType.AddChecked, "op_Addition", left, right, false);
		}

		// Token: 0x06001251 RID: 4689 RVA: 0x0003DE28 File Offset: 0x0003C028
		[__DynamicallyInvokable]
		public static BinaryExpression Subtract(Expression left, Expression right)
		{
			return Expression.Subtract(left, right, null);
		}

		// Token: 0x06001252 RID: 4690 RVA: 0x0003DE34 File Offset: 0x0003C034
		[__DynamicallyInvokable]
		public static BinaryExpression Subtract(Expression left, Expression right, MethodInfo method)
		{
			Expression.RequiresCanRead(left, "left");
			Expression.RequiresCanRead(right, "right");
			if (!(method == null))
			{
				return Expression.GetMethodBasedBinaryOperator(ExpressionType.Subtract, left, right, method, true);
			}
			if (left.Type == right.Type && TypeUtils.IsArithmetic(left.Type))
			{
				return new SimpleBinaryExpression(ExpressionType.Subtract, left, right, left.Type);
			}
			return Expression.GetUserDefinedBinaryOperatorOrThrow(ExpressionType.Subtract, "op_Subtraction", left, right, true);
		}

		// Token: 0x06001253 RID: 4691 RVA: 0x0003DEAB File Offset: 0x0003C0AB
		[__DynamicallyInvokable]
		public static BinaryExpression SubtractAssign(Expression left, Expression right)
		{
			return Expression.SubtractAssign(left, right, null, null);
		}

		// Token: 0x06001254 RID: 4692 RVA: 0x0003DEB6 File Offset: 0x0003C0B6
		[__DynamicallyInvokable]
		public static BinaryExpression SubtractAssign(Expression left, Expression right, MethodInfo method)
		{
			return Expression.SubtractAssign(left, right, method, null);
		}

		// Token: 0x06001255 RID: 4693 RVA: 0x0003DEC4 File Offset: 0x0003C0C4
		[__DynamicallyInvokable]
		public static BinaryExpression SubtractAssign(Expression left, Expression right, MethodInfo method, LambdaExpression conversion)
		{
			Expression.RequiresCanRead(left, "left");
			Expression.RequiresCanWrite(left, "left");
			Expression.RequiresCanRead(right, "right");
			if (!(method == null))
			{
				return Expression.GetMethodBasedAssignOperator(ExpressionType.SubtractAssign, left, right, method, conversion, true);
			}
			if (!(left.Type == right.Type) || !TypeUtils.IsArithmetic(left.Type))
			{
				return Expression.GetUserDefinedAssignOperatorOrThrow(ExpressionType.SubtractAssign, "op_Subtraction", left, right, conversion, true);
			}
			if (conversion != null)
			{
				throw Error.ConversionIsNotSupportedForArithmeticTypes();
			}
			return new SimpleBinaryExpression(ExpressionType.SubtractAssign, left, right, left.Type);
		}

		// Token: 0x06001256 RID: 4694 RVA: 0x0003DF51 File Offset: 0x0003C151
		[__DynamicallyInvokable]
		public static BinaryExpression SubtractAssignChecked(Expression left, Expression right)
		{
			return Expression.SubtractAssignChecked(left, right, null);
		}

		// Token: 0x06001257 RID: 4695 RVA: 0x0003DF5B File Offset: 0x0003C15B
		[__DynamicallyInvokable]
		public static BinaryExpression SubtractAssignChecked(Expression left, Expression right, MethodInfo method)
		{
			return Expression.SubtractAssignChecked(left, right, method, null);
		}

		// Token: 0x06001258 RID: 4696 RVA: 0x0003DF68 File Offset: 0x0003C168
		[__DynamicallyInvokable]
		public static BinaryExpression SubtractAssignChecked(Expression left, Expression right, MethodInfo method, LambdaExpression conversion)
		{
			Expression.RequiresCanRead(left, "left");
			Expression.RequiresCanWrite(left, "left");
			Expression.RequiresCanRead(right, "right");
			if (!(method == null))
			{
				return Expression.GetMethodBasedAssignOperator(ExpressionType.SubtractAssignChecked, left, right, method, conversion, true);
			}
			if (!(left.Type == right.Type) || !TypeUtils.IsArithmetic(left.Type))
			{
				return Expression.GetUserDefinedAssignOperatorOrThrow(ExpressionType.SubtractAssignChecked, "op_Subtraction", left, right, conversion, true);
			}
			if (conversion != null)
			{
				throw Error.ConversionIsNotSupportedForArithmeticTypes();
			}
			return new SimpleBinaryExpression(ExpressionType.SubtractAssignChecked, left, right, left.Type);
		}

		// Token: 0x06001259 RID: 4697 RVA: 0x0003DFF5 File Offset: 0x0003C1F5
		[__DynamicallyInvokable]
		public static BinaryExpression SubtractChecked(Expression left, Expression right)
		{
			return Expression.SubtractChecked(left, right, null);
		}

		// Token: 0x0600125A RID: 4698 RVA: 0x0003E000 File Offset: 0x0003C200
		[__DynamicallyInvokable]
		public static BinaryExpression SubtractChecked(Expression left, Expression right, MethodInfo method)
		{
			Expression.RequiresCanRead(left, "left");
			Expression.RequiresCanRead(right, "right");
			if (!(method == null))
			{
				return Expression.GetMethodBasedBinaryOperator(ExpressionType.SubtractChecked, left, right, method, true);
			}
			if (left.Type == right.Type && TypeUtils.IsArithmetic(left.Type))
			{
				return new SimpleBinaryExpression(ExpressionType.SubtractChecked, left, right, left.Type);
			}
			return Expression.GetUserDefinedBinaryOperatorOrThrow(ExpressionType.SubtractChecked, "op_Subtraction", left, right, true);
		}

		// Token: 0x0600125B RID: 4699 RVA: 0x0003E077 File Offset: 0x0003C277
		[__DynamicallyInvokable]
		public static BinaryExpression Divide(Expression left, Expression right)
		{
			return Expression.Divide(left, right, null);
		}

		// Token: 0x0600125C RID: 4700 RVA: 0x0003E084 File Offset: 0x0003C284
		[__DynamicallyInvokable]
		public static BinaryExpression Divide(Expression left, Expression right, MethodInfo method)
		{
			Expression.RequiresCanRead(left, "left");
			Expression.RequiresCanRead(right, "right");
			if (!(method == null))
			{
				return Expression.GetMethodBasedBinaryOperator(ExpressionType.Divide, left, right, method, true);
			}
			if (left.Type == right.Type && TypeUtils.IsArithmetic(left.Type))
			{
				return new SimpleBinaryExpression(ExpressionType.Divide, left, right, left.Type);
			}
			return Expression.GetUserDefinedBinaryOperatorOrThrow(ExpressionType.Divide, "op_Division", left, right, true);
		}

		// Token: 0x0600125D RID: 4701 RVA: 0x0003E0FB File Offset: 0x0003C2FB
		[__DynamicallyInvokable]
		public static BinaryExpression DivideAssign(Expression left, Expression right)
		{
			return Expression.DivideAssign(left, right, null, null);
		}

		// Token: 0x0600125E RID: 4702 RVA: 0x0003E106 File Offset: 0x0003C306
		[__DynamicallyInvokable]
		public static BinaryExpression DivideAssign(Expression left, Expression right, MethodInfo method)
		{
			return Expression.DivideAssign(left, right, method, null);
		}

		// Token: 0x0600125F RID: 4703 RVA: 0x0003E114 File Offset: 0x0003C314
		[__DynamicallyInvokable]
		public static BinaryExpression DivideAssign(Expression left, Expression right, MethodInfo method, LambdaExpression conversion)
		{
			Expression.RequiresCanRead(left, "left");
			Expression.RequiresCanWrite(left, "left");
			Expression.RequiresCanRead(right, "right");
			if (!(method == null))
			{
				return Expression.GetMethodBasedAssignOperator(ExpressionType.DivideAssign, left, right, method, conversion, true);
			}
			if (!(left.Type == right.Type) || !TypeUtils.IsArithmetic(left.Type))
			{
				return Expression.GetUserDefinedAssignOperatorOrThrow(ExpressionType.DivideAssign, "op_Division", left, right, conversion, true);
			}
			if (conversion != null)
			{
				throw Error.ConversionIsNotSupportedForArithmeticTypes();
			}
			return new SimpleBinaryExpression(ExpressionType.DivideAssign, left, right, left.Type);
		}

		// Token: 0x06001260 RID: 4704 RVA: 0x0003E1A1 File Offset: 0x0003C3A1
		[__DynamicallyInvokable]
		public static BinaryExpression Modulo(Expression left, Expression right)
		{
			return Expression.Modulo(left, right, null);
		}

		// Token: 0x06001261 RID: 4705 RVA: 0x0003E1AC File Offset: 0x0003C3AC
		[__DynamicallyInvokable]
		public static BinaryExpression Modulo(Expression left, Expression right, MethodInfo method)
		{
			Expression.RequiresCanRead(left, "left");
			Expression.RequiresCanRead(right, "right");
			if (!(method == null))
			{
				return Expression.GetMethodBasedBinaryOperator(ExpressionType.Modulo, left, right, method, true);
			}
			if (left.Type == right.Type && TypeUtils.IsArithmetic(left.Type))
			{
				return new SimpleBinaryExpression(ExpressionType.Modulo, left, right, left.Type);
			}
			return Expression.GetUserDefinedBinaryOperatorOrThrow(ExpressionType.Modulo, "op_Modulus", left, right, true);
		}

		// Token: 0x06001262 RID: 4706 RVA: 0x0003E223 File Offset: 0x0003C423
		[__DynamicallyInvokable]
		public static BinaryExpression ModuloAssign(Expression left, Expression right)
		{
			return Expression.ModuloAssign(left, right, null, null);
		}

		// Token: 0x06001263 RID: 4707 RVA: 0x0003E22E File Offset: 0x0003C42E
		[__DynamicallyInvokable]
		public static BinaryExpression ModuloAssign(Expression left, Expression right, MethodInfo method)
		{
			return Expression.ModuloAssign(left, right, method, null);
		}

		// Token: 0x06001264 RID: 4708 RVA: 0x0003E23C File Offset: 0x0003C43C
		[__DynamicallyInvokable]
		public static BinaryExpression ModuloAssign(Expression left, Expression right, MethodInfo method, LambdaExpression conversion)
		{
			Expression.RequiresCanRead(left, "left");
			Expression.RequiresCanWrite(left, "left");
			Expression.RequiresCanRead(right, "right");
			if (!(method == null))
			{
				return Expression.GetMethodBasedAssignOperator(ExpressionType.ModuloAssign, left, right, method, conversion, true);
			}
			if (!(left.Type == right.Type) || !TypeUtils.IsArithmetic(left.Type))
			{
				return Expression.GetUserDefinedAssignOperatorOrThrow(ExpressionType.ModuloAssign, "op_Modulus", left, right, conversion, true);
			}
			if (conversion != null)
			{
				throw Error.ConversionIsNotSupportedForArithmeticTypes();
			}
			return new SimpleBinaryExpression(ExpressionType.ModuloAssign, left, right, left.Type);
		}

		// Token: 0x06001265 RID: 4709 RVA: 0x0003E2C9 File Offset: 0x0003C4C9
		[__DynamicallyInvokable]
		public static BinaryExpression Multiply(Expression left, Expression right)
		{
			return Expression.Multiply(left, right, null);
		}

		// Token: 0x06001266 RID: 4710 RVA: 0x0003E2D4 File Offset: 0x0003C4D4
		[__DynamicallyInvokable]
		public static BinaryExpression Multiply(Expression left, Expression right, MethodInfo method)
		{
			Expression.RequiresCanRead(left, "left");
			Expression.RequiresCanRead(right, "right");
			if (!(method == null))
			{
				return Expression.GetMethodBasedBinaryOperator(ExpressionType.Multiply, left, right, method, true);
			}
			if (left.Type == right.Type && TypeUtils.IsArithmetic(left.Type))
			{
				return new SimpleBinaryExpression(ExpressionType.Multiply, left, right, left.Type);
			}
			return Expression.GetUserDefinedBinaryOperatorOrThrow(ExpressionType.Multiply, "op_Multiply", left, right, true);
		}

		// Token: 0x06001267 RID: 4711 RVA: 0x0003E34B File Offset: 0x0003C54B
		[__DynamicallyInvokable]
		public static BinaryExpression MultiplyAssign(Expression left, Expression right)
		{
			return Expression.MultiplyAssign(left, right, null, null);
		}

		// Token: 0x06001268 RID: 4712 RVA: 0x0003E356 File Offset: 0x0003C556
		[__DynamicallyInvokable]
		public static BinaryExpression MultiplyAssign(Expression left, Expression right, MethodInfo method)
		{
			return Expression.MultiplyAssign(left, right, method, null);
		}

		// Token: 0x06001269 RID: 4713 RVA: 0x0003E364 File Offset: 0x0003C564
		[__DynamicallyInvokable]
		public static BinaryExpression MultiplyAssign(Expression left, Expression right, MethodInfo method, LambdaExpression conversion)
		{
			Expression.RequiresCanRead(left, "left");
			Expression.RequiresCanWrite(left, "left");
			Expression.RequiresCanRead(right, "right");
			if (!(method == null))
			{
				return Expression.GetMethodBasedAssignOperator(ExpressionType.MultiplyAssign, left, right, method, conversion, true);
			}
			if (!(left.Type == right.Type) || !TypeUtils.IsArithmetic(left.Type))
			{
				return Expression.GetUserDefinedAssignOperatorOrThrow(ExpressionType.MultiplyAssign, "op_Multiply", left, right, conversion, true);
			}
			if (conversion != null)
			{
				throw Error.ConversionIsNotSupportedForArithmeticTypes();
			}
			return new SimpleBinaryExpression(ExpressionType.MultiplyAssign, left, right, left.Type);
		}

		// Token: 0x0600126A RID: 4714 RVA: 0x0003E3F1 File Offset: 0x0003C5F1
		[__DynamicallyInvokable]
		public static BinaryExpression MultiplyAssignChecked(Expression left, Expression right)
		{
			return Expression.MultiplyAssignChecked(left, right, null);
		}

		// Token: 0x0600126B RID: 4715 RVA: 0x0003E3FB File Offset: 0x0003C5FB
		[__DynamicallyInvokable]
		public static BinaryExpression MultiplyAssignChecked(Expression left, Expression right, MethodInfo method)
		{
			return Expression.MultiplyAssignChecked(left, right, method, null);
		}

		// Token: 0x0600126C RID: 4716 RVA: 0x0003E408 File Offset: 0x0003C608
		[__DynamicallyInvokable]
		public static BinaryExpression MultiplyAssignChecked(Expression left, Expression right, MethodInfo method, LambdaExpression conversion)
		{
			Expression.RequiresCanRead(left, "left");
			Expression.RequiresCanWrite(left, "left");
			Expression.RequiresCanRead(right, "right");
			if (!(method == null))
			{
				return Expression.GetMethodBasedAssignOperator(ExpressionType.MultiplyAssignChecked, left, right, method, conversion, true);
			}
			if (!(left.Type == right.Type) || !TypeUtils.IsArithmetic(left.Type))
			{
				return Expression.GetUserDefinedAssignOperatorOrThrow(ExpressionType.MultiplyAssignChecked, "op_Multiply", left, right, conversion, true);
			}
			if (conversion != null)
			{
				throw Error.ConversionIsNotSupportedForArithmeticTypes();
			}
			return new SimpleBinaryExpression(ExpressionType.MultiplyAssignChecked, left, right, left.Type);
		}

		// Token: 0x0600126D RID: 4717 RVA: 0x0003E495 File Offset: 0x0003C695
		[__DynamicallyInvokable]
		public static BinaryExpression MultiplyChecked(Expression left, Expression right)
		{
			return Expression.MultiplyChecked(left, right, null);
		}

		// Token: 0x0600126E RID: 4718 RVA: 0x0003E4A0 File Offset: 0x0003C6A0
		[__DynamicallyInvokable]
		public static BinaryExpression MultiplyChecked(Expression left, Expression right, MethodInfo method)
		{
			Expression.RequiresCanRead(left, "left");
			Expression.RequiresCanRead(right, "right");
			if (!(method == null))
			{
				return Expression.GetMethodBasedBinaryOperator(ExpressionType.MultiplyChecked, left, right, method, true);
			}
			if (left.Type == right.Type && TypeUtils.IsArithmetic(left.Type))
			{
				return new SimpleBinaryExpression(ExpressionType.MultiplyChecked, left, right, left.Type);
			}
			return Expression.GetUserDefinedBinaryOperatorOrThrow(ExpressionType.MultiplyChecked, "op_Multiply", left, right, true);
		}

		// Token: 0x0600126F RID: 4719 RVA: 0x0003E517 File Offset: 0x0003C717
		private static bool IsSimpleShift(Type left, Type right)
		{
			return TypeUtils.IsInteger(left) && right.GetNonNullableType() == typeof(int);
		}

		// Token: 0x06001270 RID: 4720 RVA: 0x0003E538 File Offset: 0x0003C738
		private static Type GetResultTypeOfShift(Type left, Type right)
		{
			if (!left.IsNullableType() && right.IsNullableType())
			{
				return typeof(Nullable<>).MakeGenericType(new Type[]
				{
					left
				});
			}
			return left;
		}

		// Token: 0x06001271 RID: 4721 RVA: 0x0003E565 File Offset: 0x0003C765
		[__DynamicallyInvokable]
		public static BinaryExpression LeftShift(Expression left, Expression right)
		{
			return Expression.LeftShift(left, right, null);
		}

		// Token: 0x06001272 RID: 4722 RVA: 0x0003E570 File Offset: 0x0003C770
		[__DynamicallyInvokable]
		public static BinaryExpression LeftShift(Expression left, Expression right, MethodInfo method)
		{
			Expression.RequiresCanRead(left, "left");
			Expression.RequiresCanRead(right, "right");
			if (!(method == null))
			{
				return Expression.GetMethodBasedBinaryOperator(ExpressionType.LeftShift, left, right, method, true);
			}
			if (Expression.IsSimpleShift(left.Type, right.Type))
			{
				Type resultTypeOfShift = Expression.GetResultTypeOfShift(left.Type, right.Type);
				return new SimpleBinaryExpression(ExpressionType.LeftShift, left, right, resultTypeOfShift);
			}
			return Expression.GetUserDefinedBinaryOperatorOrThrow(ExpressionType.LeftShift, "op_LeftShift", left, right, true);
		}

		// Token: 0x06001273 RID: 4723 RVA: 0x0003E5E7 File Offset: 0x0003C7E7
		[__DynamicallyInvokable]
		public static BinaryExpression LeftShiftAssign(Expression left, Expression right)
		{
			return Expression.LeftShiftAssign(left, right, null, null);
		}

		// Token: 0x06001274 RID: 4724 RVA: 0x0003E5F2 File Offset: 0x0003C7F2
		[__DynamicallyInvokable]
		public static BinaryExpression LeftShiftAssign(Expression left, Expression right, MethodInfo method)
		{
			return Expression.LeftShiftAssign(left, right, method, null);
		}

		// Token: 0x06001275 RID: 4725 RVA: 0x0003E600 File Offset: 0x0003C800
		[__DynamicallyInvokable]
		public static BinaryExpression LeftShiftAssign(Expression left, Expression right, MethodInfo method, LambdaExpression conversion)
		{
			Expression.RequiresCanRead(left, "left");
			Expression.RequiresCanWrite(left, "left");
			Expression.RequiresCanRead(right, "right");
			if (!(method == null))
			{
				return Expression.GetMethodBasedAssignOperator(ExpressionType.LeftShiftAssign, left, right, method, conversion, true);
			}
			if (!Expression.IsSimpleShift(left.Type, right.Type))
			{
				return Expression.GetUserDefinedAssignOperatorOrThrow(ExpressionType.LeftShiftAssign, "op_LeftShift", left, right, conversion, true);
			}
			if (conversion != null)
			{
				throw Error.ConversionIsNotSupportedForArithmeticTypes();
			}
			Type resultTypeOfShift = Expression.GetResultTypeOfShift(left.Type, right.Type);
			return new SimpleBinaryExpression(ExpressionType.LeftShiftAssign, left, right, resultTypeOfShift);
		}

		// Token: 0x06001276 RID: 4726 RVA: 0x0003E68D File Offset: 0x0003C88D
		[__DynamicallyInvokable]
		public static BinaryExpression RightShift(Expression left, Expression right)
		{
			return Expression.RightShift(left, right, null);
		}

		// Token: 0x06001277 RID: 4727 RVA: 0x0003E698 File Offset: 0x0003C898
		[__DynamicallyInvokable]
		public static BinaryExpression RightShift(Expression left, Expression right, MethodInfo method)
		{
			Expression.RequiresCanRead(left, "left");
			Expression.RequiresCanRead(right, "right");
			if (!(method == null))
			{
				return Expression.GetMethodBasedBinaryOperator(ExpressionType.RightShift, left, right, method, true);
			}
			if (Expression.IsSimpleShift(left.Type, right.Type))
			{
				Type resultTypeOfShift = Expression.GetResultTypeOfShift(left.Type, right.Type);
				return new SimpleBinaryExpression(ExpressionType.RightShift, left, right, resultTypeOfShift);
			}
			return Expression.GetUserDefinedBinaryOperatorOrThrow(ExpressionType.RightShift, "op_RightShift", left, right, true);
		}

		// Token: 0x06001278 RID: 4728 RVA: 0x0003E70F File Offset: 0x0003C90F
		[__DynamicallyInvokable]
		public static BinaryExpression RightShiftAssign(Expression left, Expression right)
		{
			return Expression.RightShiftAssign(left, right, null, null);
		}

		// Token: 0x06001279 RID: 4729 RVA: 0x0003E71A File Offset: 0x0003C91A
		[__DynamicallyInvokable]
		public static BinaryExpression RightShiftAssign(Expression left, Expression right, MethodInfo method)
		{
			return Expression.RightShiftAssign(left, right, method, null);
		}

		// Token: 0x0600127A RID: 4730 RVA: 0x0003E728 File Offset: 0x0003C928
		[__DynamicallyInvokable]
		public static BinaryExpression RightShiftAssign(Expression left, Expression right, MethodInfo method, LambdaExpression conversion)
		{
			Expression.RequiresCanRead(left, "left");
			Expression.RequiresCanWrite(left, "left");
			Expression.RequiresCanRead(right, "right");
			if (!(method == null))
			{
				return Expression.GetMethodBasedAssignOperator(ExpressionType.RightShiftAssign, left, right, method, conversion, true);
			}
			if (!Expression.IsSimpleShift(left.Type, right.Type))
			{
				return Expression.GetUserDefinedAssignOperatorOrThrow(ExpressionType.RightShiftAssign, "op_RightShift", left, right, conversion, true);
			}
			if (conversion != null)
			{
				throw Error.ConversionIsNotSupportedForArithmeticTypes();
			}
			Type resultTypeOfShift = Expression.GetResultTypeOfShift(left.Type, right.Type);
			return new SimpleBinaryExpression(ExpressionType.RightShiftAssign, left, right, resultTypeOfShift);
		}

		// Token: 0x0600127B RID: 4731 RVA: 0x0003E7B5 File Offset: 0x0003C9B5
		[__DynamicallyInvokable]
		public static BinaryExpression And(Expression left, Expression right)
		{
			return Expression.And(left, right, null);
		}

		// Token: 0x0600127C RID: 4732 RVA: 0x0003E7C0 File Offset: 0x0003C9C0
		[__DynamicallyInvokable]
		public static BinaryExpression And(Expression left, Expression right, MethodInfo method)
		{
			Expression.RequiresCanRead(left, "left");
			Expression.RequiresCanRead(right, "right");
			if (!(method == null))
			{
				return Expression.GetMethodBasedBinaryOperator(ExpressionType.And, left, right, method, true);
			}
			if (left.Type == right.Type && TypeUtils.IsIntegerOrBool(left.Type))
			{
				return new SimpleBinaryExpression(ExpressionType.And, left, right, left.Type);
			}
			return Expression.GetUserDefinedBinaryOperatorOrThrow(ExpressionType.And, "op_BitwiseAnd", left, right, true);
		}

		// Token: 0x0600127D RID: 4733 RVA: 0x0003E834 File Offset: 0x0003CA34
		[__DynamicallyInvokable]
		public static BinaryExpression AndAssign(Expression left, Expression right)
		{
			return Expression.AndAssign(left, right, null, null);
		}

		// Token: 0x0600127E RID: 4734 RVA: 0x0003E83F File Offset: 0x0003CA3F
		[__DynamicallyInvokable]
		public static BinaryExpression AndAssign(Expression left, Expression right, MethodInfo method)
		{
			return Expression.AndAssign(left, right, method, null);
		}

		// Token: 0x0600127F RID: 4735 RVA: 0x0003E84C File Offset: 0x0003CA4C
		[__DynamicallyInvokable]
		public static BinaryExpression AndAssign(Expression left, Expression right, MethodInfo method, LambdaExpression conversion)
		{
			Expression.RequiresCanRead(left, "left");
			Expression.RequiresCanWrite(left, "left");
			Expression.RequiresCanRead(right, "right");
			if (!(method == null))
			{
				return Expression.GetMethodBasedAssignOperator(ExpressionType.AndAssign, left, right, method, conversion, true);
			}
			if (!(left.Type == right.Type) || !TypeUtils.IsIntegerOrBool(left.Type))
			{
				return Expression.GetUserDefinedAssignOperatorOrThrow(ExpressionType.AndAssign, "op_BitwiseAnd", left, right, conversion, true);
			}
			if (conversion != null)
			{
				throw Error.ConversionIsNotSupportedForArithmeticTypes();
			}
			return new SimpleBinaryExpression(ExpressionType.AndAssign, left, right, left.Type);
		}

		// Token: 0x06001280 RID: 4736 RVA: 0x0003E8D9 File Offset: 0x0003CAD9
		[__DynamicallyInvokable]
		public static BinaryExpression Or(Expression left, Expression right)
		{
			return Expression.Or(left, right, null);
		}

		// Token: 0x06001281 RID: 4737 RVA: 0x0003E8E4 File Offset: 0x0003CAE4
		[__DynamicallyInvokable]
		public static BinaryExpression Or(Expression left, Expression right, MethodInfo method)
		{
			Expression.RequiresCanRead(left, "left");
			Expression.RequiresCanRead(right, "right");
			if (!(method == null))
			{
				return Expression.GetMethodBasedBinaryOperator(ExpressionType.Or, left, right, method, true);
			}
			if (left.Type == right.Type && TypeUtils.IsIntegerOrBool(left.Type))
			{
				return new SimpleBinaryExpression(ExpressionType.Or, left, right, left.Type);
			}
			return Expression.GetUserDefinedBinaryOperatorOrThrow(ExpressionType.Or, "op_BitwiseOr", left, right, true);
		}

		// Token: 0x06001282 RID: 4738 RVA: 0x0003E95B File Offset: 0x0003CB5B
		[__DynamicallyInvokable]
		public static BinaryExpression OrAssign(Expression left, Expression right)
		{
			return Expression.OrAssign(left, right, null, null);
		}

		// Token: 0x06001283 RID: 4739 RVA: 0x0003E966 File Offset: 0x0003CB66
		[__DynamicallyInvokable]
		public static BinaryExpression OrAssign(Expression left, Expression right, MethodInfo method)
		{
			return Expression.OrAssign(left, right, method, null);
		}

		// Token: 0x06001284 RID: 4740 RVA: 0x0003E974 File Offset: 0x0003CB74
		[__DynamicallyInvokable]
		public static BinaryExpression OrAssign(Expression left, Expression right, MethodInfo method, LambdaExpression conversion)
		{
			Expression.RequiresCanRead(left, "left");
			Expression.RequiresCanWrite(left, "left");
			Expression.RequiresCanRead(right, "right");
			if (!(method == null))
			{
				return Expression.GetMethodBasedAssignOperator(ExpressionType.OrAssign, left, right, method, conversion, true);
			}
			if (!(left.Type == right.Type) || !TypeUtils.IsIntegerOrBool(left.Type))
			{
				return Expression.GetUserDefinedAssignOperatorOrThrow(ExpressionType.OrAssign, "op_BitwiseOr", left, right, conversion, true);
			}
			if (conversion != null)
			{
				throw Error.ConversionIsNotSupportedForArithmeticTypes();
			}
			return new SimpleBinaryExpression(ExpressionType.OrAssign, left, right, left.Type);
		}

		// Token: 0x06001285 RID: 4741 RVA: 0x0003EA01 File Offset: 0x0003CC01
		[__DynamicallyInvokable]
		public static BinaryExpression ExclusiveOr(Expression left, Expression right)
		{
			return Expression.ExclusiveOr(left, right, null);
		}

		// Token: 0x06001286 RID: 4742 RVA: 0x0003EA0C File Offset: 0x0003CC0C
		[__DynamicallyInvokable]
		public static BinaryExpression ExclusiveOr(Expression left, Expression right, MethodInfo method)
		{
			Expression.RequiresCanRead(left, "left");
			Expression.RequiresCanRead(right, "right");
			if (!(method == null))
			{
				return Expression.GetMethodBasedBinaryOperator(ExpressionType.ExclusiveOr, left, right, method, true);
			}
			if (left.Type == right.Type && TypeUtils.IsIntegerOrBool(left.Type))
			{
				return new SimpleBinaryExpression(ExpressionType.ExclusiveOr, left, right, left.Type);
			}
			return Expression.GetUserDefinedBinaryOperatorOrThrow(ExpressionType.ExclusiveOr, "op_ExclusiveOr", left, right, true);
		}

		// Token: 0x06001287 RID: 4743 RVA: 0x0003EA83 File Offset: 0x0003CC83
		[__DynamicallyInvokable]
		public static BinaryExpression ExclusiveOrAssign(Expression left, Expression right)
		{
			return Expression.ExclusiveOrAssign(left, right, null, null);
		}

		// Token: 0x06001288 RID: 4744 RVA: 0x0003EA8E File Offset: 0x0003CC8E
		[__DynamicallyInvokable]
		public static BinaryExpression ExclusiveOrAssign(Expression left, Expression right, MethodInfo method)
		{
			return Expression.ExclusiveOrAssign(left, right, method, null);
		}

		// Token: 0x06001289 RID: 4745 RVA: 0x0003EA9C File Offset: 0x0003CC9C
		[__DynamicallyInvokable]
		public static BinaryExpression ExclusiveOrAssign(Expression left, Expression right, MethodInfo method, LambdaExpression conversion)
		{
			Expression.RequiresCanRead(left, "left");
			Expression.RequiresCanWrite(left, "left");
			Expression.RequiresCanRead(right, "right");
			if (!(method == null))
			{
				return Expression.GetMethodBasedAssignOperator(ExpressionType.ExclusiveOrAssign, left, right, method, conversion, true);
			}
			if (!(left.Type == right.Type) || !TypeUtils.IsIntegerOrBool(left.Type))
			{
				return Expression.GetUserDefinedAssignOperatorOrThrow(ExpressionType.ExclusiveOrAssign, "op_ExclusiveOr", left, right, conversion, true);
			}
			if (conversion != null)
			{
				throw Error.ConversionIsNotSupportedForArithmeticTypes();
			}
			return new SimpleBinaryExpression(ExpressionType.ExclusiveOrAssign, left, right, left.Type);
		}

		// Token: 0x0600128A RID: 4746 RVA: 0x0003EB29 File Offset: 0x0003CD29
		[__DynamicallyInvokable]
		public static BinaryExpression Power(Expression left, Expression right)
		{
			return Expression.Power(left, right, null);
		}

		// Token: 0x0600128B RID: 4747 RVA: 0x0003EB34 File Offset: 0x0003CD34
		[__DynamicallyInvokable]
		public static BinaryExpression Power(Expression left, Expression right, MethodInfo method)
		{
			Expression.RequiresCanRead(left, "left");
			Expression.RequiresCanRead(right, "right");
			if (method == null)
			{
				Type typeFromHandle = typeof(Math);
				method = typeFromHandle.GetMethod("Pow", BindingFlags.Static | BindingFlags.Public);
				if (method == null)
				{
					throw Error.BinaryOperatorNotDefined(ExpressionType.Power, left.Type, right.Type);
				}
			}
			return Expression.GetMethodBasedBinaryOperator(ExpressionType.Power, left, right, method, true);
		}

		// Token: 0x0600128C RID: 4748 RVA: 0x0003EBA7 File Offset: 0x0003CDA7
		[__DynamicallyInvokable]
		public static BinaryExpression PowerAssign(Expression left, Expression right)
		{
			return Expression.PowerAssign(left, right, null, null);
		}

		// Token: 0x0600128D RID: 4749 RVA: 0x0003EBB2 File Offset: 0x0003CDB2
		[__DynamicallyInvokable]
		public static BinaryExpression PowerAssign(Expression left, Expression right, MethodInfo method)
		{
			return Expression.PowerAssign(left, right, method, null);
		}

		// Token: 0x0600128E RID: 4750 RVA: 0x0003EBC0 File Offset: 0x0003CDC0
		[__DynamicallyInvokable]
		public static BinaryExpression PowerAssign(Expression left, Expression right, MethodInfo method, LambdaExpression conversion)
		{
			Expression.RequiresCanRead(left, "left");
			Expression.RequiresCanWrite(left, "left");
			Expression.RequiresCanRead(right, "right");
			if (method == null)
			{
				Type typeFromHandle = typeof(Math);
				method = typeFromHandle.GetMethod("Pow", BindingFlags.Static | BindingFlags.Public);
				if (method == null)
				{
					throw Error.BinaryOperatorNotDefined(ExpressionType.PowerAssign, left.Type, right.Type);
				}
			}
			return Expression.GetMethodBasedAssignOperator(ExpressionType.PowerAssign, left, right, method, conversion, true);
		}

		// Token: 0x0600128F RID: 4751 RVA: 0x0003EC40 File Offset: 0x0003CE40
		[__DynamicallyInvokable]
		public static BinaryExpression ArrayIndex(Expression array, Expression index)
		{
			Expression.RequiresCanRead(array, "array");
			Expression.RequiresCanRead(index, "index");
			if (index.Type != typeof(int))
			{
				throw Error.ArgumentMustBeArrayIndexType();
			}
			Type type = array.Type;
			if (!type.IsArray)
			{
				throw Error.ArgumentMustBeArray();
			}
			if (type.GetArrayRank() != 1)
			{
				throw Error.IncorrectNumberOfIndexes();
			}
			return new SimpleBinaryExpression(ExpressionType.ArrayIndex, array, index, type.GetElementType());
		}

		// Token: 0x06001290 RID: 4752 RVA: 0x0003ECB2 File Offset: 0x0003CEB2
		[__DynamicallyInvokable]
		public static BlockExpression Block(Expression arg0, Expression arg1)
		{
			Expression.RequiresCanRead(arg0, "arg0");
			Expression.RequiresCanRead(arg1, "arg1");
			return new Block2(arg0, arg1);
		}

		// Token: 0x06001291 RID: 4753 RVA: 0x0003ECD1 File Offset: 0x0003CED1
		[__DynamicallyInvokable]
		public static BlockExpression Block(Expression arg0, Expression arg1, Expression arg2)
		{
			Expression.RequiresCanRead(arg0, "arg0");
			Expression.RequiresCanRead(arg1, "arg1");
			Expression.RequiresCanRead(arg2, "arg2");
			return new Block3(arg0, arg1, arg2);
		}

		// Token: 0x06001292 RID: 4754 RVA: 0x0003ECFC File Offset: 0x0003CEFC
		[__DynamicallyInvokable]
		public static BlockExpression Block(Expression arg0, Expression arg1, Expression arg2, Expression arg3)
		{
			Expression.RequiresCanRead(arg0, "arg0");
			Expression.RequiresCanRead(arg1, "arg1");
			Expression.RequiresCanRead(arg2, "arg2");
			Expression.RequiresCanRead(arg3, "arg3");
			return new Block4(arg0, arg1, arg2, arg3);
		}

		// Token: 0x06001293 RID: 4755 RVA: 0x0003ED34 File Offset: 0x0003CF34
		[__DynamicallyInvokable]
		public static BlockExpression Block(Expression arg0, Expression arg1, Expression arg2, Expression arg3, Expression arg4)
		{
			Expression.RequiresCanRead(arg0, "arg0");
			Expression.RequiresCanRead(arg1, "arg1");
			Expression.RequiresCanRead(arg2, "arg2");
			Expression.RequiresCanRead(arg3, "arg3");
			Expression.RequiresCanRead(arg4, "arg4");
			return new Block5(arg0, arg1, arg2, arg3, arg4);
		}

		// Token: 0x06001294 RID: 4756 RVA: 0x0003ED84 File Offset: 0x0003CF84
		[__DynamicallyInvokable]
		public static BlockExpression Block(params Expression[] expressions)
		{
			ContractUtils.RequiresNotNull(expressions, "expressions");
			switch (expressions.Length)
			{
			case 2:
				return Expression.Block(expressions[0], expressions[1]);
			case 3:
				return Expression.Block(expressions[0], expressions[1], expressions[2]);
			case 4:
				return Expression.Block(expressions[0], expressions[1], expressions[2], expressions[3]);
			case 5:
				return Expression.Block(expressions[0], expressions[1], expressions[2], expressions[3], expressions[4]);
			default:
				ContractUtils.RequiresNotEmpty<Expression>(expressions, "expressions");
				Expression.RequiresCanRead(expressions, "expressions");
				return new BlockN(expressions.Copy<Expression>());
			}
		}

		// Token: 0x06001295 RID: 4757 RVA: 0x0003EE1D File Offset: 0x0003D01D
		[__DynamicallyInvokable]
		public static BlockExpression Block(IEnumerable<Expression> expressions)
		{
			return Expression.Block(EmptyReadOnlyCollection<ParameterExpression>.Instance, expressions);
		}

		// Token: 0x06001296 RID: 4758 RVA: 0x0003EE2A File Offset: 0x0003D02A
		[__DynamicallyInvokable]
		public static BlockExpression Block(Type type, params Expression[] expressions)
		{
			ContractUtils.RequiresNotNull(expressions, "expressions");
			return Expression.Block(type, expressions);
		}

		// Token: 0x06001297 RID: 4759 RVA: 0x0003EE3E File Offset: 0x0003D03E
		[__DynamicallyInvokable]
		public static BlockExpression Block(Type type, IEnumerable<Expression> expressions)
		{
			return Expression.Block(type, EmptyReadOnlyCollection<ParameterExpression>.Instance, expressions);
		}

		// Token: 0x06001298 RID: 4760 RVA: 0x0003EE4C File Offset: 0x0003D04C
		[__DynamicallyInvokable]
		public static BlockExpression Block(IEnumerable<ParameterExpression> variables, params Expression[] expressions)
		{
			return Expression.Block(variables, expressions);
		}

		// Token: 0x06001299 RID: 4761 RVA: 0x0003EE55 File Offset: 0x0003D055
		[__DynamicallyInvokable]
		public static BlockExpression Block(Type type, IEnumerable<ParameterExpression> variables, params Expression[] expressions)
		{
			return Expression.Block(type, variables, expressions);
		}

		// Token: 0x0600129A RID: 4762 RVA: 0x0003EE60 File Offset: 0x0003D060
		[__DynamicallyInvokable]
		public static BlockExpression Block(IEnumerable<ParameterExpression> variables, IEnumerable<Expression> expressions)
		{
			ContractUtils.RequiresNotNull(expressions, "expressions");
			ReadOnlyCollection<Expression> readOnlyCollection = expressions.ToReadOnly<Expression>();
			ContractUtils.RequiresNotEmpty<Expression>(readOnlyCollection, "expressions");
			Expression.RequiresCanRead(readOnlyCollection, "expressions");
			return Expression.Block(readOnlyCollection.Last<Expression>().Type, variables, readOnlyCollection);
		}

		// Token: 0x0600129B RID: 4763 RVA: 0x0003EEA8 File Offset: 0x0003D0A8
		[__DynamicallyInvokable]
		public static BlockExpression Block(Type type, IEnumerable<ParameterExpression> variables, IEnumerable<Expression> expressions)
		{
			ContractUtils.RequiresNotNull(type, "type");
			ContractUtils.RequiresNotNull(expressions, "expressions");
			ReadOnlyCollection<Expression> readOnlyCollection = expressions.ToReadOnly<Expression>();
			ReadOnlyCollection<ParameterExpression> readOnlyCollection2 = variables.ToReadOnly<ParameterExpression>();
			ContractUtils.RequiresNotEmpty<Expression>(readOnlyCollection, "expressions");
			Expression.RequiresCanRead(readOnlyCollection, "expressions");
			Expression.ValidateVariables(readOnlyCollection2, "variables");
			Expression expression = readOnlyCollection.Last<Expression>();
			if (type != typeof(void) && !TypeUtils.AreReferenceAssignable(type, expression.Type))
			{
				throw Error.ArgumentTypesMustMatch();
			}
			if (!TypeUtils.AreEquivalent(type, expression.Type))
			{
				return new ScopeWithType(readOnlyCollection2, readOnlyCollection, type);
			}
			if (readOnlyCollection.Count == 1)
			{
				return new Scope1(readOnlyCollection2, readOnlyCollection[0]);
			}
			return new ScopeN(readOnlyCollection2, readOnlyCollection);
		}

		// Token: 0x0600129C RID: 4764 RVA: 0x0003EF5C File Offset: 0x0003D15C
		internal static void ValidateVariables(ReadOnlyCollection<ParameterExpression> varList, string collectionName)
		{
			if (varList.Count == 0)
			{
				return;
			}
			int count = varList.Count;
			Set<ParameterExpression> set = new Set<ParameterExpression>(count);
			for (int i = 0; i < count; i++)
			{
				ParameterExpression parameterExpression = varList[i];
				if (parameterExpression == null)
				{
					throw new ArgumentNullException(string.Format(CultureInfo.CurrentCulture, "{0}[{1}]", new object[]
					{
						collectionName,
						set.Count
					}));
				}
				if (parameterExpression.IsByRef)
				{
					throw Error.VariableMustNotBeByRef(parameterExpression, parameterExpression.Type);
				}
				if (set.Contains(parameterExpression))
				{
					throw Error.DuplicateVariable(parameterExpression);
				}
				set.Add(parameterExpression);
			}
		}

		// Token: 0x0600129D RID: 4765 RVA: 0x0003EFF0 File Offset: 0x0003D1F0
		[__DynamicallyInvokable]
		public static CatchBlock Catch(Type type, Expression body)
		{
			return Expression.MakeCatchBlock(type, null, body, null);
		}

		// Token: 0x0600129E RID: 4766 RVA: 0x0003EFFB File Offset: 0x0003D1FB
		[__DynamicallyInvokable]
		public static CatchBlock Catch(ParameterExpression variable, Expression body)
		{
			ContractUtils.RequiresNotNull(variable, "variable");
			return Expression.MakeCatchBlock(variable.Type, variable, body, null);
		}

		// Token: 0x0600129F RID: 4767 RVA: 0x0003F016 File Offset: 0x0003D216
		[__DynamicallyInvokable]
		public static CatchBlock Catch(Type type, Expression body, Expression filter)
		{
			return Expression.MakeCatchBlock(type, null, body, filter);
		}

		// Token: 0x060012A0 RID: 4768 RVA: 0x0003F021 File Offset: 0x0003D221
		[__DynamicallyInvokable]
		public static CatchBlock Catch(ParameterExpression variable, Expression body, Expression filter)
		{
			ContractUtils.RequiresNotNull(variable, "variable");
			return Expression.MakeCatchBlock(variable.Type, variable, body, filter);
		}

		// Token: 0x060012A1 RID: 4769 RVA: 0x0003F03C File Offset: 0x0003D23C
		[__DynamicallyInvokable]
		public static CatchBlock MakeCatchBlock(Type type, ParameterExpression variable, Expression body, Expression filter)
		{
			ContractUtils.RequiresNotNull(type, "type");
			ContractUtils.Requires(variable == null || TypeUtils.AreEquivalent(variable.Type, type), "variable");
			if (variable != null && variable.IsByRef)
			{
				throw Error.VariableMustNotBeByRef(variable, variable.Type);
			}
			Expression.RequiresCanRead(body, "body");
			if (filter != null)
			{
				Expression.RequiresCanRead(filter, "filter");
				if (filter.Type != typeof(bool))
				{
					throw Error.ArgumentMustBeBoolean();
				}
			}
			return new CatchBlock(type, variable, body, filter);
		}

		// Token: 0x060012A2 RID: 4770 RVA: 0x0003F0C8 File Offset: 0x0003D2C8
		[__DynamicallyInvokable]
		public static ConditionalExpression Condition(Expression test, Expression ifTrue, Expression ifFalse)
		{
			Expression.RequiresCanRead(test, "test");
			Expression.RequiresCanRead(ifTrue, "ifTrue");
			Expression.RequiresCanRead(ifFalse, "ifFalse");
			if (test.Type != typeof(bool))
			{
				throw Error.ArgumentMustBeBoolean();
			}
			if (!TypeUtils.AreEquivalent(ifTrue.Type, ifFalse.Type))
			{
				throw Error.ArgumentTypesMustMatch();
			}
			return ConditionalExpression.Make(test, ifTrue, ifFalse, ifTrue.Type);
		}

		// Token: 0x060012A3 RID: 4771 RVA: 0x0003F13C File Offset: 0x0003D33C
		[__DynamicallyInvokable]
		public static ConditionalExpression Condition(Expression test, Expression ifTrue, Expression ifFalse, Type type)
		{
			Expression.RequiresCanRead(test, "test");
			Expression.RequiresCanRead(ifTrue, "ifTrue");
			Expression.RequiresCanRead(ifFalse, "ifFalse");
			ContractUtils.RequiresNotNull(type, "type");
			if (test.Type != typeof(bool))
			{
				throw Error.ArgumentMustBeBoolean();
			}
			if (type != typeof(void) && (!TypeUtils.AreReferenceAssignable(type, ifTrue.Type) || !TypeUtils.AreReferenceAssignable(type, ifFalse.Type)))
			{
				throw Error.ArgumentTypesMustMatch();
			}
			return ConditionalExpression.Make(test, ifTrue, ifFalse, type);
		}

		// Token: 0x060012A4 RID: 4772 RVA: 0x0003F1CF File Offset: 0x0003D3CF
		[__DynamicallyInvokable]
		public static ConditionalExpression IfThen(Expression test, Expression ifTrue)
		{
			return Expression.Condition(test, ifTrue, Expression.Empty(), typeof(void));
		}

		// Token: 0x060012A5 RID: 4773 RVA: 0x0003F1E7 File Offset: 0x0003D3E7
		[__DynamicallyInvokable]
		public static ConditionalExpression IfThenElse(Expression test, Expression ifTrue, Expression ifFalse)
		{
			return Expression.Condition(test, ifTrue, ifFalse, typeof(void));
		}

		// Token: 0x060012A6 RID: 4774 RVA: 0x0003F1FB File Offset: 0x0003D3FB
		[__DynamicallyInvokable]
		public static ConstantExpression Constant(object value)
		{
			return ConstantExpression.Make(value, (value == null) ? typeof(object) : value.GetType());
		}

		// Token: 0x060012A7 RID: 4775 RVA: 0x0003F218 File Offset: 0x0003D418
		[__DynamicallyInvokable]
		public static ConstantExpression Constant(object value, Type type)
		{
			ContractUtils.RequiresNotNull(type, "type");
			if (value == null && type.IsValueType && !type.IsNullableType())
			{
				throw Error.ArgumentTypesMustMatch();
			}
			if (value != null && !type.IsAssignableFrom(value.GetType()))
			{
				throw Error.ArgumentTypesMustMatch();
			}
			return ConstantExpression.Make(value, type);
		}

		// Token: 0x060012A8 RID: 4776 RVA: 0x0003F267 File Offset: 0x0003D467
		[__DynamicallyInvokable]
		public static DebugInfoExpression DebugInfo(SymbolDocumentInfo document, int startLine, int startColumn, int endLine, int endColumn)
		{
			ContractUtils.RequiresNotNull(document, "document");
			if (startLine == 16707566 && startColumn == 0 && endLine == 16707566 && endColumn == 0)
			{
				return new ClearDebugInfoExpression(document);
			}
			Expression.ValidateSpan(startLine, startColumn, endLine, endColumn);
			return new SpanDebugInfoExpression(document, startLine, startColumn, endLine, endColumn);
		}

		// Token: 0x060012A9 RID: 4777 RVA: 0x0003F2A7 File Offset: 0x0003D4A7
		[__DynamicallyInvokable]
		public static DebugInfoExpression ClearDebugInfo(SymbolDocumentInfo document)
		{
			ContractUtils.RequiresNotNull(document, "document");
			return new ClearDebugInfoExpression(document);
		}

		// Token: 0x060012AA RID: 4778 RVA: 0x0003F2BC File Offset: 0x0003D4BC
		private static void ValidateSpan(int startLine, int startColumn, int endLine, int endColumn)
		{
			if (startLine < 1)
			{
				throw Error.OutOfRange("startLine", 1);
			}
			if (startColumn < 1)
			{
				throw Error.OutOfRange("startColumn", 1);
			}
			if (endLine < 1)
			{
				throw Error.OutOfRange("endLine", 1);
			}
			if (endColumn < 1)
			{
				throw Error.OutOfRange("endColumn", 1);
			}
			if (startLine > endLine)
			{
				throw Error.StartEndMustBeOrdered();
			}
			if (startLine == endLine && startColumn > endColumn)
			{
				throw Error.StartEndMustBeOrdered();
			}
		}

		// Token: 0x060012AB RID: 4779 RVA: 0x0003F335 File Offset: 0x0003D535
		[__DynamicallyInvokable]
		public static DefaultExpression Empty()
		{
			return new DefaultExpression(typeof(void));
		}

		// Token: 0x060012AC RID: 4780 RVA: 0x0003F346 File Offset: 0x0003D546
		[__DynamicallyInvokable]
		public static DefaultExpression Default(Type type)
		{
			if (type == typeof(void))
			{
				return Expression.Empty();
			}
			return new DefaultExpression(type);
		}

		// Token: 0x060012AD RID: 4781 RVA: 0x0003F366 File Offset: 0x0003D566
		[__DynamicallyInvokable]
		public static DynamicExpression MakeDynamic(Type delegateType, CallSiteBinder binder, params Expression[] arguments)
		{
			return Expression.MakeDynamic(delegateType, binder, arguments);
		}

		// Token: 0x060012AE RID: 4782 RVA: 0x0003F370 File Offset: 0x0003D570
		[__DynamicallyInvokable]
		public static DynamicExpression MakeDynamic(Type delegateType, CallSiteBinder binder, IEnumerable<Expression> arguments)
		{
			ContractUtils.RequiresNotNull(delegateType, "delegateType");
			ContractUtils.RequiresNotNull(binder, "binder");
			if (!delegateType.IsSubclassOf(typeof(MulticastDelegate)))
			{
				throw Error.TypeMustBeDerivedFromSystemDelegate();
			}
			MethodInfo validMethodForDynamic = Expression.GetValidMethodForDynamic(delegateType);
			ReadOnlyCollection<Expression> arguments2 = arguments.ToReadOnly<Expression>();
			Expression.ValidateArgumentTypes(validMethodForDynamic, ExpressionType.Dynamic, ref arguments2);
			return DynamicExpression.Make(validMethodForDynamic.GetReturnType(), delegateType, binder, arguments2);
		}

		// Token: 0x060012AF RID: 4783 RVA: 0x0003F3D4 File Offset: 0x0003D5D4
		[__DynamicallyInvokable]
		public static DynamicExpression MakeDynamic(Type delegateType, CallSiteBinder binder, Expression arg0)
		{
			ContractUtils.RequiresNotNull(delegateType, "delegateType");
			ContractUtils.RequiresNotNull(binder, "binder");
			if (!delegateType.IsSubclassOf(typeof(MulticastDelegate)))
			{
				throw Error.TypeMustBeDerivedFromSystemDelegate();
			}
			MethodInfo validMethodForDynamic = Expression.GetValidMethodForDynamic(delegateType);
			ParameterInfo[] parametersCached = validMethodForDynamic.GetParametersCached();
			Expression.ValidateArgumentCount(validMethodForDynamic, ExpressionType.Dynamic, 2, parametersCached);
			Expression.ValidateDynamicArgument(arg0);
			Expression.ValidateOneArgument(validMethodForDynamic, ExpressionType.Dynamic, arg0, parametersCached[1]);
			return DynamicExpression.Make(validMethodForDynamic.GetReturnType(), delegateType, binder, arg0);
		}

		// Token: 0x060012B0 RID: 4784 RVA: 0x0003F448 File Offset: 0x0003D648
		[__DynamicallyInvokable]
		public static DynamicExpression MakeDynamic(Type delegateType, CallSiteBinder binder, Expression arg0, Expression arg1)
		{
			ContractUtils.RequiresNotNull(delegateType, "delegateType");
			ContractUtils.RequiresNotNull(binder, "binder");
			if (!delegateType.IsSubclassOf(typeof(MulticastDelegate)))
			{
				throw Error.TypeMustBeDerivedFromSystemDelegate();
			}
			MethodInfo validMethodForDynamic = Expression.GetValidMethodForDynamic(delegateType);
			ParameterInfo[] parametersCached = validMethodForDynamic.GetParametersCached();
			Expression.ValidateArgumentCount(validMethodForDynamic, ExpressionType.Dynamic, 3, parametersCached);
			Expression.ValidateDynamicArgument(arg0);
			Expression.ValidateOneArgument(validMethodForDynamic, ExpressionType.Dynamic, arg0, parametersCached[1]);
			Expression.ValidateDynamicArgument(arg1);
			Expression.ValidateOneArgument(validMethodForDynamic, ExpressionType.Dynamic, arg1, parametersCached[2]);
			return DynamicExpression.Make(validMethodForDynamic.GetReturnType(), delegateType, binder, arg0, arg1);
		}

		// Token: 0x060012B1 RID: 4785 RVA: 0x0003F4D0 File Offset: 0x0003D6D0
		[__DynamicallyInvokable]
		public static DynamicExpression MakeDynamic(Type delegateType, CallSiteBinder binder, Expression arg0, Expression arg1, Expression arg2)
		{
			ContractUtils.RequiresNotNull(delegateType, "delegateType");
			ContractUtils.RequiresNotNull(binder, "binder");
			if (!delegateType.IsSubclassOf(typeof(MulticastDelegate)))
			{
				throw Error.TypeMustBeDerivedFromSystemDelegate();
			}
			MethodInfo validMethodForDynamic = Expression.GetValidMethodForDynamic(delegateType);
			ParameterInfo[] parametersCached = validMethodForDynamic.GetParametersCached();
			Expression.ValidateArgumentCount(validMethodForDynamic, ExpressionType.Dynamic, 4, parametersCached);
			Expression.ValidateDynamicArgument(arg0);
			Expression.ValidateOneArgument(validMethodForDynamic, ExpressionType.Dynamic, arg0, parametersCached[1]);
			Expression.ValidateDynamicArgument(arg1);
			Expression.ValidateOneArgument(validMethodForDynamic, ExpressionType.Dynamic, arg1, parametersCached[2]);
			Expression.ValidateDynamicArgument(arg2);
			Expression.ValidateOneArgument(validMethodForDynamic, ExpressionType.Dynamic, arg2, parametersCached[3]);
			return DynamicExpression.Make(validMethodForDynamic.GetReturnType(), delegateType, binder, arg0, arg1, arg2);
		}

		// Token: 0x060012B2 RID: 4786 RVA: 0x0003F570 File Offset: 0x0003D770
		[__DynamicallyInvokable]
		public static DynamicExpression MakeDynamic(Type delegateType, CallSiteBinder binder, Expression arg0, Expression arg1, Expression arg2, Expression arg3)
		{
			ContractUtils.RequiresNotNull(delegateType, "delegateType");
			ContractUtils.RequiresNotNull(binder, "binder");
			if (!delegateType.IsSubclassOf(typeof(MulticastDelegate)))
			{
				throw Error.TypeMustBeDerivedFromSystemDelegate();
			}
			MethodInfo validMethodForDynamic = Expression.GetValidMethodForDynamic(delegateType);
			ParameterInfo[] parametersCached = validMethodForDynamic.GetParametersCached();
			Expression.ValidateArgumentCount(validMethodForDynamic, ExpressionType.Dynamic, 5, parametersCached);
			Expression.ValidateDynamicArgument(arg0);
			Expression.ValidateOneArgument(validMethodForDynamic, ExpressionType.Dynamic, arg0, parametersCached[1]);
			Expression.ValidateDynamicArgument(arg1);
			Expression.ValidateOneArgument(validMethodForDynamic, ExpressionType.Dynamic, arg1, parametersCached[2]);
			Expression.ValidateDynamicArgument(arg2);
			Expression.ValidateOneArgument(validMethodForDynamic, ExpressionType.Dynamic, arg2, parametersCached[3]);
			Expression.ValidateDynamicArgument(arg3);
			Expression.ValidateOneArgument(validMethodForDynamic, ExpressionType.Dynamic, arg3, parametersCached[4]);
			return DynamicExpression.Make(validMethodForDynamic.GetReturnType(), delegateType, binder, arg0, arg1, arg2, arg3);
		}

		// Token: 0x060012B3 RID: 4787 RVA: 0x0003F628 File Offset: 0x0003D828
		private static MethodInfo GetValidMethodForDynamic(Type delegateType)
		{
			MethodInfo method = delegateType.GetMethod("Invoke");
			ParameterInfo[] parametersCached = method.GetParametersCached();
			if (parametersCached.Length == 0 || parametersCached[0].ParameterType != typeof(CallSite))
			{
				throw Error.FirstArgumentMustBeCallSite();
			}
			return method;
		}

		// Token: 0x060012B4 RID: 4788 RVA: 0x0003F66C File Offset: 0x0003D86C
		[__DynamicallyInvokable]
		public static DynamicExpression Dynamic(CallSiteBinder binder, Type returnType, params Expression[] arguments)
		{
			return Expression.Dynamic(binder, returnType, arguments);
		}

		// Token: 0x060012B5 RID: 4789 RVA: 0x0003F678 File Offset: 0x0003D878
		[__DynamicallyInvokable]
		public static DynamicExpression Dynamic(CallSiteBinder binder, Type returnType, Expression arg0)
		{
			ContractUtils.RequiresNotNull(binder, "binder");
			Expression.ValidateDynamicArgument(arg0);
			DelegateHelpers.TypeInfo nextTypeInfo = DelegateHelpers.GetNextTypeInfo(returnType, DelegateHelpers.GetNextTypeInfo(arg0.Type, DelegateHelpers.NextTypeInfo(typeof(CallSite))));
			Type type = nextTypeInfo.DelegateType;
			if (type == null)
			{
				type = nextTypeInfo.MakeDelegateType(returnType, new Expression[]
				{
					arg0
				});
			}
			return DynamicExpression.Make(returnType, type, binder, arg0);
		}

		// Token: 0x060012B6 RID: 4790 RVA: 0x0003F6E4 File Offset: 0x0003D8E4
		[__DynamicallyInvokable]
		public static DynamicExpression Dynamic(CallSiteBinder binder, Type returnType, Expression arg0, Expression arg1)
		{
			ContractUtils.RequiresNotNull(binder, "binder");
			Expression.ValidateDynamicArgument(arg0);
			Expression.ValidateDynamicArgument(arg1);
			DelegateHelpers.TypeInfo nextTypeInfo = DelegateHelpers.GetNextTypeInfo(returnType, DelegateHelpers.GetNextTypeInfo(arg1.Type, DelegateHelpers.GetNextTypeInfo(arg0.Type, DelegateHelpers.NextTypeInfo(typeof(CallSite)))));
			Type type = nextTypeInfo.DelegateType;
			if (type == null)
			{
				type = nextTypeInfo.MakeDelegateType(returnType, new Expression[]
				{
					arg0,
					arg1
				});
			}
			return DynamicExpression.Make(returnType, type, binder, arg0, arg1);
		}

		// Token: 0x060012B7 RID: 4791 RVA: 0x0003F764 File Offset: 0x0003D964
		[__DynamicallyInvokable]
		public static DynamicExpression Dynamic(CallSiteBinder binder, Type returnType, Expression arg0, Expression arg1, Expression arg2)
		{
			ContractUtils.RequiresNotNull(binder, "binder");
			Expression.ValidateDynamicArgument(arg0);
			Expression.ValidateDynamicArgument(arg1);
			Expression.ValidateDynamicArgument(arg2);
			DelegateHelpers.TypeInfo nextTypeInfo = DelegateHelpers.GetNextTypeInfo(returnType, DelegateHelpers.GetNextTypeInfo(arg2.Type, DelegateHelpers.GetNextTypeInfo(arg1.Type, DelegateHelpers.GetNextTypeInfo(arg0.Type, DelegateHelpers.NextTypeInfo(typeof(CallSite))))));
			Type type = nextTypeInfo.DelegateType;
			if (type == null)
			{
				type = nextTypeInfo.MakeDelegateType(returnType, new Expression[]
				{
					arg0,
					arg1,
					arg2
				});
			}
			return DynamicExpression.Make(returnType, type, binder, arg0, arg1, arg2);
		}

		// Token: 0x060012B8 RID: 4792 RVA: 0x0003F800 File Offset: 0x0003DA00
		[__DynamicallyInvokable]
		public static DynamicExpression Dynamic(CallSiteBinder binder, Type returnType, Expression arg0, Expression arg1, Expression arg2, Expression arg3)
		{
			ContractUtils.RequiresNotNull(binder, "binder");
			Expression.ValidateDynamicArgument(arg0);
			Expression.ValidateDynamicArgument(arg1);
			Expression.ValidateDynamicArgument(arg2);
			Expression.ValidateDynamicArgument(arg3);
			DelegateHelpers.TypeInfo nextTypeInfo = DelegateHelpers.GetNextTypeInfo(returnType, DelegateHelpers.GetNextTypeInfo(arg3.Type, DelegateHelpers.GetNextTypeInfo(arg2.Type, DelegateHelpers.GetNextTypeInfo(arg1.Type, DelegateHelpers.GetNextTypeInfo(arg0.Type, DelegateHelpers.NextTypeInfo(typeof(CallSite)))))));
			Type type = nextTypeInfo.DelegateType;
			if (type == null)
			{
				type = nextTypeInfo.MakeDelegateType(returnType, new Expression[]
				{
					arg0,
					arg1,
					arg2,
					arg3
				});
			}
			return DynamicExpression.Make(returnType, type, binder, arg0, arg1, arg2, arg3);
		}

		// Token: 0x060012B9 RID: 4793 RVA: 0x0003F8B4 File Offset: 0x0003DAB4
		[__DynamicallyInvokable]
		public static DynamicExpression Dynamic(CallSiteBinder binder, Type returnType, IEnumerable<Expression> arguments)
		{
			ContractUtils.RequiresNotNull(arguments, "arguments");
			ContractUtils.RequiresNotNull(returnType, "returnType");
			ReadOnlyCollection<Expression> readOnlyCollection = arguments.ToReadOnly<Expression>();
			ContractUtils.RequiresNotEmpty<Expression>(readOnlyCollection, "args");
			return Expression.MakeDynamic(binder, returnType, readOnlyCollection);
		}

		// Token: 0x060012BA RID: 4794 RVA: 0x0003F8F4 File Offset: 0x0003DAF4
		private static DynamicExpression MakeDynamic(CallSiteBinder binder, Type returnType, ReadOnlyCollection<Expression> args)
		{
			ContractUtils.RequiresNotNull(binder, "binder");
			for (int i = 0; i < args.Count; i++)
			{
				Expression arg = args[i];
				Expression.ValidateDynamicArgument(arg);
			}
			Type delegateType = DelegateHelpers.MakeCallSiteDelegate(args, returnType);
			switch (args.Count)
			{
			case 1:
				return DynamicExpression.Make(returnType, delegateType, binder, args[0]);
			case 2:
				return DynamicExpression.Make(returnType, delegateType, binder, args[0], args[1]);
			case 3:
				return DynamicExpression.Make(returnType, delegateType, binder, args[0], args[1], args[2]);
			case 4:
				return DynamicExpression.Make(returnType, delegateType, binder, args[0], args[1], args[2], args[3]);
			default:
				return DynamicExpression.Make(returnType, delegateType, binder, args);
			}
		}

		// Token: 0x060012BB RID: 4795 RVA: 0x0003F9C8 File Offset: 0x0003DBC8
		private static void ValidateDynamicArgument(Expression arg)
		{
			Expression.RequiresCanRead(arg, "arguments");
			Type type = arg.Type;
			ContractUtils.RequiresNotNull(type, "type");
			TypeUtils.ValidateType(type);
			if (type == typeof(void))
			{
				throw Error.ArgumentTypeCannotBeVoid();
			}
		}

		// Token: 0x060012BC RID: 4796 RVA: 0x0003FA10 File Offset: 0x0003DC10
		[__DynamicallyInvokable]
		public static ElementInit ElementInit(MethodInfo addMethod, params Expression[] arguments)
		{
			return Expression.ElementInit(addMethod, arguments);
		}

		// Token: 0x060012BD RID: 4797 RVA: 0x0003FA1C File Offset: 0x0003DC1C
		[__DynamicallyInvokable]
		public static ElementInit ElementInit(MethodInfo addMethod, IEnumerable<Expression> arguments)
		{
			ContractUtils.RequiresNotNull(addMethod, "addMethod");
			ContractUtils.RequiresNotNull(arguments, "arguments");
			ReadOnlyCollection<Expression> readOnlyCollection = arguments.ToReadOnly<Expression>();
			Expression.RequiresCanRead(readOnlyCollection, "arguments");
			Expression.ValidateElementInitAddMethodInfo(addMethod);
			Expression.ValidateArgumentTypes(addMethod, ExpressionType.Call, ref readOnlyCollection);
			return new ElementInit(addMethod, readOnlyCollection);
		}

		// Token: 0x060012BE RID: 4798 RVA: 0x0003FA68 File Offset: 0x0003DC68
		private static void ValidateElementInitAddMethodInfo(MethodInfo addMethod)
		{
			Expression.ValidateMethodInfo(addMethod);
			ParameterInfo[] parametersCached = addMethod.GetParametersCached();
			if (parametersCached.Length == 0)
			{
				throw Error.ElementInitializerMethodWithZeroArgs();
			}
			if (!addMethod.Name.Equals("Add", StringComparison.OrdinalIgnoreCase))
			{
				throw Error.ElementInitializerMethodNotAdd();
			}
			if (addMethod.IsStatic)
			{
				throw Error.ElementInitializerMethodStatic();
			}
			foreach (ParameterInfo parameterInfo in parametersCached)
			{
				if (parameterInfo.ParameterType.IsByRef)
				{
					throw Error.ElementInitializerMethodNoRefOutParam(parameterInfo.Name, addMethod.Name);
				}
			}
		}

		// Token: 0x060012BF RID: 4799 RVA: 0x0003FAE6 File Offset: 0x0003DCE6
		[Obsolete("use a different constructor that does not take ExpressionType. Then override NodeType and Type properties to provide the values that would be specified to this constructor.")]
		protected Expression(ExpressionType nodeType, Type type)
		{
			if (Expression._legacyCtorSupportTable == null)
			{
				Interlocked.CompareExchange<ConditionalWeakTable<Expression, Expression.ExtensionInfo>>(ref Expression._legacyCtorSupportTable, new ConditionalWeakTable<Expression, Expression.ExtensionInfo>(), null);
			}
			Expression._legacyCtorSupportTable.Add(this, new Expression.ExtensionInfo(nodeType, type));
		}

		// Token: 0x060012C0 RID: 4800 RVA: 0x0003FB18 File Offset: 0x0003DD18
		[__DynamicallyInvokable]
		protected Expression()
		{
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x060012C1 RID: 4801 RVA: 0x0003FB20 File Offset: 0x0003DD20
		[__DynamicallyInvokable]
		public virtual ExpressionType NodeType
		{
			[__DynamicallyInvokable]
			get
			{
				Expression.ExtensionInfo extensionInfo;
				if (Expression._legacyCtorSupportTable != null && Expression._legacyCtorSupportTable.TryGetValue(this, out extensionInfo))
				{
					return extensionInfo.NodeType;
				}
				throw Error.ExtensionNodeMustOverrideProperty("Expression.NodeType");
			}
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x060012C2 RID: 4802 RVA: 0x0003FB54 File Offset: 0x0003DD54
		[__DynamicallyInvokable]
		public virtual Type Type
		{
			[__DynamicallyInvokable]
			get
			{
				Expression.ExtensionInfo extensionInfo;
				if (Expression._legacyCtorSupportTable != null && Expression._legacyCtorSupportTable.TryGetValue(this, out extensionInfo))
				{
					return extensionInfo.Type;
				}
				throw Error.ExtensionNodeMustOverrideProperty("Expression.Type");
			}
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x060012C3 RID: 4803 RVA: 0x0003FB88 File Offset: 0x0003DD88
		[__DynamicallyInvokable]
		public virtual bool CanReduce
		{
			[__DynamicallyInvokable]
			get
			{
				return false;
			}
		}

		// Token: 0x060012C4 RID: 4804 RVA: 0x0003FB8B File Offset: 0x0003DD8B
		[__DynamicallyInvokable]
		public virtual Expression Reduce()
		{
			if (this.CanReduce)
			{
				throw Error.ReducibleMustOverrideReduce();
			}
			return this;
		}

		// Token: 0x060012C5 RID: 4805 RVA: 0x0003FB9C File Offset: 0x0003DD9C
		[__DynamicallyInvokable]
		protected internal virtual Expression VisitChildren(ExpressionVisitor visitor)
		{
			if (!this.CanReduce)
			{
				throw Error.MustBeReducible();
			}
			return visitor.Visit(this.ReduceAndCheck());
		}

		// Token: 0x060012C6 RID: 4806 RVA: 0x0003FBB8 File Offset: 0x0003DDB8
		[__DynamicallyInvokable]
		protected internal virtual Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitExtension(this);
		}

		// Token: 0x060012C7 RID: 4807 RVA: 0x0003FBC4 File Offset: 0x0003DDC4
		[__DynamicallyInvokable]
		public Expression ReduceAndCheck()
		{
			if (!this.CanReduce)
			{
				throw Error.MustBeReducible();
			}
			Expression expression = this.Reduce();
			if (expression == null || expression == this)
			{
				throw Error.MustReduceToDifferent();
			}
			if (!TypeUtils.AreReferenceAssignable(this.Type, expression.Type))
			{
				throw Error.ReducedNotCompatible();
			}
			return expression;
		}

		// Token: 0x060012C8 RID: 4808 RVA: 0x0003FC10 File Offset: 0x0003DE10
		[__DynamicallyInvokable]
		public Expression ReduceExtensions()
		{
			Expression expression = this;
			while (expression.NodeType == ExpressionType.Extension)
			{
				expression = expression.ReduceAndCheck();
			}
			return expression;
		}

		// Token: 0x060012C9 RID: 4809 RVA: 0x0003FC33 File Offset: 0x0003DE33
		[__DynamicallyInvokable]
		public override string ToString()
		{
			return ExpressionStringBuilder.ExpressionToString(this);
		}

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x060012CA RID: 4810 RVA: 0x0003FC3C File Offset: 0x0003DE3C
		private string DebugView
		{
			get
			{
				string result;
				using (StringWriter stringWriter = new StringWriter(CultureInfo.CurrentCulture))
				{
					DebugViewWriter.WriteTo(this, stringWriter);
					result = stringWriter.ToString();
				}
				return result;
			}
		}

		// Token: 0x060012CB RID: 4811 RVA: 0x0003FC80 File Offset: 0x0003DE80
		internal static ReadOnlyCollection<T> ReturnReadOnly<T>(ref IList<T> collection)
		{
			IList<T> list = collection;
			ReadOnlyCollection<T> readOnlyCollection = list as ReadOnlyCollection<T>;
			if (readOnlyCollection != null)
			{
				return readOnlyCollection;
			}
			Interlocked.CompareExchange<IList<T>>(ref collection, list.ToReadOnly<T>(), list);
			return (ReadOnlyCollection<T>)collection;
		}

		// Token: 0x060012CC RID: 4812 RVA: 0x0003FCB4 File Offset: 0x0003DEB4
		internal static ReadOnlyCollection<Expression> ReturnReadOnly(IArgumentProvider provider, ref object collection)
		{
			Expression expression = collection as Expression;
			if (expression != null)
			{
				Interlocked.CompareExchange(ref collection, new ReadOnlyCollection<Expression>(new ListArgumentProvider(provider, expression)), expression);
			}
			return (ReadOnlyCollection<Expression>)collection;
		}

		// Token: 0x060012CD RID: 4813 RVA: 0x0003FCE8 File Offset: 0x0003DEE8
		internal static T ReturnObject<T>(object collectionOrT) where T : class
		{
			T t = collectionOrT as T;
			if (t != null)
			{
				return t;
			}
			return ((ReadOnlyCollection<T>)collectionOrT)[0];
		}

		// Token: 0x060012CE RID: 4814 RVA: 0x0003FD18 File Offset: 0x0003DF18
		private static void RequiresCanRead(Expression expression, string paramName)
		{
			if (expression == null)
			{
				throw new ArgumentNullException(paramName);
			}
			ExpressionType nodeType = expression.NodeType;
			if (nodeType != ExpressionType.MemberAccess)
			{
				if (nodeType == ExpressionType.Index)
				{
					IndexExpression indexExpression = (IndexExpression)expression;
					if (indexExpression.Indexer != null && !indexExpression.Indexer.CanRead)
					{
						throw new ArgumentException(Strings.ExpressionMustBeReadable, paramName);
					}
				}
			}
			else
			{
				MemberExpression memberExpression = (MemberExpression)expression;
				MemberInfo member = memberExpression.Member;
				if (member.MemberType == MemberTypes.Property)
				{
					PropertyInfo propertyInfo = (PropertyInfo)member;
					if (!propertyInfo.CanRead)
					{
						throw new ArgumentException(Strings.ExpressionMustBeReadable, paramName);
					}
				}
			}
		}

		// Token: 0x060012CF RID: 4815 RVA: 0x0003FDA4 File Offset: 0x0003DFA4
		private static void RequiresCanRead(IEnumerable<Expression> items, string paramName)
		{
			if (items != null)
			{
				IList<Expression> list = items as IList<Expression>;
				if (list != null)
				{
					for (int i = 0; i < list.Count; i++)
					{
						Expression.RequiresCanRead(list[i], paramName);
					}
					return;
				}
				foreach (Expression expression in items)
				{
					Expression.RequiresCanRead(expression, paramName);
				}
			}
		}

		// Token: 0x060012D0 RID: 4816 RVA: 0x0003FE18 File Offset: 0x0003E018
		private static void RequiresCanWrite(Expression expression, string paramName)
		{
			if (expression == null)
			{
				throw new ArgumentNullException(paramName);
			}
			bool flag = false;
			ExpressionType nodeType = expression.NodeType;
			if (nodeType != ExpressionType.MemberAccess)
			{
				if (nodeType != ExpressionType.Parameter)
				{
					if (nodeType == ExpressionType.Index)
					{
						IndexExpression indexExpression = (IndexExpression)expression;
						flag = (!(indexExpression.Indexer != null) || indexExpression.Indexer.CanWrite);
					}
				}
				else
				{
					flag = true;
				}
			}
			else
			{
				MemberExpression memberExpression = (MemberExpression)expression;
				MemberTypes memberType = memberExpression.Member.MemberType;
				if (memberType != MemberTypes.Field)
				{
					if (memberType == MemberTypes.Property)
					{
						PropertyInfo propertyInfo = (PropertyInfo)memberExpression.Member;
						flag = propertyInfo.CanWrite;
					}
				}
				else
				{
					FieldInfo fieldInfo = (FieldInfo)memberExpression.Member;
					flag = (!fieldInfo.IsInitOnly && !fieldInfo.IsLiteral);
				}
			}
			if (!flag)
			{
				throw new ArgumentException(Strings.ExpressionMustBeWriteable, paramName);
			}
		}

		// Token: 0x060012D1 RID: 4817 RVA: 0x0003FEE1 File Offset: 0x0003E0E1
		[__DynamicallyInvokable]
		public static GotoExpression Break(LabelTarget target)
		{
			return Expression.MakeGoto(GotoExpressionKind.Break, target, null, typeof(void));
		}

		// Token: 0x060012D2 RID: 4818 RVA: 0x0003FEF5 File Offset: 0x0003E0F5
		[__DynamicallyInvokable]
		public static GotoExpression Break(LabelTarget target, Expression value)
		{
			return Expression.MakeGoto(GotoExpressionKind.Break, target, value, typeof(void));
		}

		// Token: 0x060012D3 RID: 4819 RVA: 0x0003FF09 File Offset: 0x0003E109
		[__DynamicallyInvokable]
		public static GotoExpression Break(LabelTarget target, Type type)
		{
			return Expression.MakeGoto(GotoExpressionKind.Break, target, null, type);
		}

		// Token: 0x060012D4 RID: 4820 RVA: 0x0003FF14 File Offset: 0x0003E114
		[__DynamicallyInvokable]
		public static GotoExpression Break(LabelTarget target, Expression value, Type type)
		{
			return Expression.MakeGoto(GotoExpressionKind.Break, target, value, type);
		}

		// Token: 0x060012D5 RID: 4821 RVA: 0x0003FF1F File Offset: 0x0003E11F
		[__DynamicallyInvokable]
		public static GotoExpression Continue(LabelTarget target)
		{
			return Expression.MakeGoto(GotoExpressionKind.Continue, target, null, typeof(void));
		}

		// Token: 0x060012D6 RID: 4822 RVA: 0x0003FF33 File Offset: 0x0003E133
		[__DynamicallyInvokable]
		public static GotoExpression Continue(LabelTarget target, Type type)
		{
			return Expression.MakeGoto(GotoExpressionKind.Continue, target, null, type);
		}

		// Token: 0x060012D7 RID: 4823 RVA: 0x0003FF3E File Offset: 0x0003E13E
		[__DynamicallyInvokable]
		public static GotoExpression Return(LabelTarget target)
		{
			return Expression.MakeGoto(GotoExpressionKind.Return, target, null, typeof(void));
		}

		// Token: 0x060012D8 RID: 4824 RVA: 0x0003FF52 File Offset: 0x0003E152
		[__DynamicallyInvokable]
		public static GotoExpression Return(LabelTarget target, Type type)
		{
			return Expression.MakeGoto(GotoExpressionKind.Return, target, null, type);
		}

		// Token: 0x060012D9 RID: 4825 RVA: 0x0003FF5D File Offset: 0x0003E15D
		[__DynamicallyInvokable]
		public static GotoExpression Return(LabelTarget target, Expression value)
		{
			return Expression.MakeGoto(GotoExpressionKind.Return, target, value, typeof(void));
		}

		// Token: 0x060012DA RID: 4826 RVA: 0x0003FF71 File Offset: 0x0003E171
		[__DynamicallyInvokable]
		public static GotoExpression Return(LabelTarget target, Expression value, Type type)
		{
			return Expression.MakeGoto(GotoExpressionKind.Return, target, value, type);
		}

		// Token: 0x060012DB RID: 4827 RVA: 0x0003FF7C File Offset: 0x0003E17C
		[__DynamicallyInvokable]
		public static GotoExpression Goto(LabelTarget target)
		{
			return Expression.MakeGoto(GotoExpressionKind.Goto, target, null, typeof(void));
		}

		// Token: 0x060012DC RID: 4828 RVA: 0x0003FF90 File Offset: 0x0003E190
		[__DynamicallyInvokable]
		public static GotoExpression Goto(LabelTarget target, Type type)
		{
			return Expression.MakeGoto(GotoExpressionKind.Goto, target, null, type);
		}

		// Token: 0x060012DD RID: 4829 RVA: 0x0003FF9B File Offset: 0x0003E19B
		[__DynamicallyInvokable]
		public static GotoExpression Goto(LabelTarget target, Expression value)
		{
			return Expression.MakeGoto(GotoExpressionKind.Goto, target, value, typeof(void));
		}

		// Token: 0x060012DE RID: 4830 RVA: 0x0003FFAF File Offset: 0x0003E1AF
		[__DynamicallyInvokable]
		public static GotoExpression Goto(LabelTarget target, Expression value, Type type)
		{
			return Expression.MakeGoto(GotoExpressionKind.Goto, target, value, type);
		}

		// Token: 0x060012DF RID: 4831 RVA: 0x0003FFBA File Offset: 0x0003E1BA
		[__DynamicallyInvokable]
		public static GotoExpression MakeGoto(GotoExpressionKind kind, LabelTarget target, Expression value, Type type)
		{
			Expression.ValidateGoto(target, ref value, "target", "value");
			return new GotoExpression(kind, target, value, type);
		}

		// Token: 0x060012E0 RID: 4832 RVA: 0x0003FFD7 File Offset: 0x0003E1D7
		private static void ValidateGoto(LabelTarget target, ref Expression value, string targetParameter, string valueParameter)
		{
			ContractUtils.RequiresNotNull(target, targetParameter);
			if (value == null)
			{
				if (target.Type != typeof(void))
				{
					throw Error.LabelMustBeVoidOrHaveExpression();
				}
			}
			else
			{
				Expression.ValidateGotoType(target.Type, ref value, valueParameter);
			}
		}

		// Token: 0x060012E1 RID: 4833 RVA: 0x00040010 File Offset: 0x0003E210
		private static void ValidateGotoType(Type expectedType, ref Expression value, string paramName)
		{
			Expression.RequiresCanRead(value, paramName);
			if (expectedType != typeof(void) && !TypeUtils.AreReferenceAssignable(expectedType, value.Type) && !Expression.TryQuote(expectedType, ref value))
			{
				throw Error.ExpressionTypeDoesNotMatchLabel(value.Type, expectedType);
			}
		}

		// Token: 0x060012E2 RID: 4834 RVA: 0x0004005D File Offset: 0x0003E25D
		[__DynamicallyInvokable]
		public static IndexExpression MakeIndex(Expression instance, PropertyInfo indexer, IEnumerable<Expression> arguments)
		{
			if (indexer != null)
			{
				return Expression.Property(instance, indexer, arguments);
			}
			return Expression.ArrayAccess(instance, arguments);
		}

		// Token: 0x060012E3 RID: 4835 RVA: 0x00040078 File Offset: 0x0003E278
		[__DynamicallyInvokable]
		public static IndexExpression ArrayAccess(Expression array, params Expression[] indexes)
		{
			return Expression.ArrayAccess(array, indexes);
		}

		// Token: 0x060012E4 RID: 4836 RVA: 0x00040084 File Offset: 0x0003E284
		[__DynamicallyInvokable]
		public static IndexExpression ArrayAccess(Expression array, IEnumerable<Expression> indexes)
		{
			Expression.RequiresCanRead(array, "array");
			Type type = array.Type;
			if (!type.IsArray)
			{
				throw Error.ArgumentMustBeArray();
			}
			ReadOnlyCollection<Expression> readOnlyCollection = indexes.ToReadOnly<Expression>();
			if (type.GetArrayRank() != readOnlyCollection.Count)
			{
				throw Error.IncorrectNumberOfIndexes();
			}
			foreach (Expression expression in readOnlyCollection)
			{
				Expression.RequiresCanRead(expression, "indexes");
				if (expression.Type != typeof(int))
				{
					throw Error.ArgumentMustBeArrayIndexType();
				}
			}
			return new IndexExpression(array, null, readOnlyCollection);
		}

		// Token: 0x060012E5 RID: 4837 RVA: 0x00040130 File Offset: 0x0003E330
		[__DynamicallyInvokable]
		public static IndexExpression Property(Expression instance, string propertyName, params Expression[] arguments)
		{
			Expression.RequiresCanRead(instance, "instance");
			ContractUtils.RequiresNotNull(propertyName, "indexerName");
			PropertyInfo indexer = Expression.FindInstanceProperty(instance.Type, propertyName, arguments);
			return Expression.Property(instance, indexer, arguments);
		}

		// Token: 0x060012E6 RID: 4838 RVA: 0x0004016C File Offset: 0x0003E36C
		private static PropertyInfo FindInstanceProperty(Type type, string propertyName, Expression[] arguments)
		{
			BindingFlags flags = BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy;
			PropertyInfo propertyInfo = Expression.FindProperty(type, propertyName, arguments, flags);
			if (propertyInfo == null)
			{
				flags = (BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
				propertyInfo = Expression.FindProperty(type, propertyName, arguments, flags);
			}
			if (!(propertyInfo == null))
			{
				return propertyInfo;
			}
			if (arguments == null || arguments.Length == 0)
			{
				throw Error.InstancePropertyWithoutParameterNotDefinedForType(propertyName, type);
			}
			throw Error.InstancePropertyWithSpecifiedParametersNotDefinedForType(propertyName, Expression.GetArgTypesString(arguments), type);
		}

		// Token: 0x060012E7 RID: 4839 RVA: 0x000401C4 File Offset: 0x0003E3C4
		private static string GetArgTypesString(Expression[] arguments)
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = true;
			stringBuilder.Append("(");
			foreach (Type type in from arg in arguments
			select arg.Type)
			{
				if (!flag)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append(type.Name);
				flag = false;
			}
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}

		// Token: 0x060012E8 RID: 4840 RVA: 0x00040270 File Offset: 0x0003E470
		private static PropertyInfo FindProperty(Type type, string propertyName, Expression[] arguments, BindingFlags flags)
		{
			MemberInfo[] array = type.FindMembers(MemberTypes.Property, flags, Type.FilterNameIgnoreCase, propertyName);
			if (array == null || array.Length == 0)
			{
				return null;
			}
			PropertyInfo[] properties = array.Map((MemberInfo t) => (PropertyInfo)t);
			PropertyInfo result;
			int num = Expression.FindBestProperty(properties, arguments, out result);
			if (num == 0)
			{
				return null;
			}
			if (num > 1)
			{
				throw Error.PropertyWithMoreThanOneMatch(propertyName, type);
			}
			return result;
		}

		// Token: 0x060012E9 RID: 4841 RVA: 0x000402D8 File Offset: 0x0003E4D8
		private static int FindBestProperty(IEnumerable<PropertyInfo> properties, Expression[] args, out PropertyInfo property)
		{
			int num = 0;
			property = null;
			foreach (PropertyInfo propertyInfo in properties)
			{
				if (propertyInfo != null && Expression.IsCompatible(propertyInfo, args))
				{
					if (property == null)
					{
						property = propertyInfo;
						num = 1;
					}
					else
					{
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x060012EA RID: 4842 RVA: 0x00040348 File Offset: 0x0003E548
		private static bool IsCompatible(PropertyInfo pi, Expression[] args)
		{
			MethodInfo methodInfo = pi.GetGetMethod(true);
			ParameterInfo[] array;
			if (methodInfo != null)
			{
				array = methodInfo.GetParametersCached();
			}
			else
			{
				methodInfo = pi.GetSetMethod(true);
				array = methodInfo.GetParametersCached().RemoveLast<ParameterInfo>();
			}
			if (methodInfo == null)
			{
				return false;
			}
			if (args == null)
			{
				return array.Length == 0;
			}
			if (array.Length != args.Length)
			{
				return false;
			}
			for (int i = 0; i < args.Length; i++)
			{
				if (args[i] == null)
				{
					return false;
				}
				if (!TypeUtils.AreReferenceAssignable(array[i].ParameterType, args[i].Type))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060012EB RID: 4843 RVA: 0x000403D0 File Offset: 0x0003E5D0
		[__DynamicallyInvokable]
		public static IndexExpression Property(Expression instance, PropertyInfo indexer, params Expression[] arguments)
		{
			return Expression.Property(instance, indexer, arguments);
		}

		// Token: 0x060012EC RID: 4844 RVA: 0x000403DC File Offset: 0x0003E5DC
		[__DynamicallyInvokable]
		public static IndexExpression Property(Expression instance, PropertyInfo indexer, IEnumerable<Expression> arguments)
		{
			ReadOnlyCollection<Expression> arguments2 = arguments.ToReadOnly<Expression>();
			Expression.ValidateIndexedProperty(instance, indexer, ref arguments2);
			return new IndexExpression(instance, indexer, arguments2);
		}

		// Token: 0x060012ED RID: 4845 RVA: 0x00040404 File Offset: 0x0003E604
		private static void ValidateIndexedProperty(Expression instance, PropertyInfo property, ref ReadOnlyCollection<Expression> argList)
		{
			ContractUtils.RequiresNotNull(property, "property");
			if (property.PropertyType.IsByRef)
			{
				throw Error.PropertyCannotHaveRefType();
			}
			if (property.PropertyType == typeof(void))
			{
				throw Error.PropertyTypeCannotBeVoid();
			}
			ParameterInfo[] array = null;
			MethodInfo getMethod = property.GetGetMethod(true);
			if (getMethod != null)
			{
				array = getMethod.GetParametersCached();
				Expression.ValidateAccessor(instance, getMethod, array, ref argList);
			}
			MethodInfo setMethod = property.GetSetMethod(true);
			if (setMethod != null)
			{
				ParameterInfo[] parametersCached = setMethod.GetParametersCached();
				if (parametersCached.Length == 0)
				{
					throw Error.SetterHasNoParams();
				}
				Type parameterType = parametersCached[parametersCached.Length - 1].ParameterType;
				if (parameterType.IsByRef)
				{
					throw Error.PropertyCannotHaveRefType();
				}
				if (setMethod.ReturnType != typeof(void))
				{
					throw Error.SetterMustBeVoid();
				}
				if (property.PropertyType != parameterType)
				{
					throw Error.PropertyTyepMustMatchSetter();
				}
				if (getMethod != null)
				{
					if (getMethod.IsStatic ^ setMethod.IsStatic)
					{
						throw Error.BothAccessorsMustBeStatic();
					}
					if (array.Length != parametersCached.Length - 1)
					{
						throw Error.IndexesOfSetGetMustMatch();
					}
					for (int i = 0; i < array.Length; i++)
					{
						if (array[i].ParameterType != parametersCached[i].ParameterType)
						{
							throw Error.IndexesOfSetGetMustMatch();
						}
					}
				}
				else
				{
					Expression.ValidateAccessor(instance, setMethod, parametersCached.RemoveLast<ParameterInfo>(), ref argList);
				}
			}
			if (getMethod == null && setMethod == null)
			{
				throw Error.PropertyDoesNotHaveAccessor(property);
			}
		}

		// Token: 0x060012EE RID: 4846 RVA: 0x0004056C File Offset: 0x0003E76C
		private static void ValidateAccessor(Expression instance, MethodInfo method, ParameterInfo[] indexes, ref ReadOnlyCollection<Expression> arguments)
		{
			ContractUtils.RequiresNotNull(arguments, "arguments");
			Expression.ValidateMethodInfo(method);
			if ((method.CallingConvention & CallingConventions.VarArgs) != (CallingConventions)0)
			{
				throw Error.AccessorsCannotHaveVarArgs();
			}
			if (method.IsStatic)
			{
				if (instance != null)
				{
					throw Error.OnlyStaticMethodsHaveNullInstance();
				}
			}
			else
			{
				if (instance == null)
				{
					throw Error.OnlyStaticMethodsHaveNullInstance();
				}
				Expression.RequiresCanRead(instance, "instance");
				Expression.ValidateCallInstanceType(instance.Type, method);
			}
			Expression.ValidateAccessorArgumentTypes(method, indexes, ref arguments);
		}

		// Token: 0x060012EF RID: 4847 RVA: 0x000405D4 File Offset: 0x0003E7D4
		private static void ValidateAccessorArgumentTypes(MethodInfo method, ParameterInfo[] indexes, ref ReadOnlyCollection<Expression> arguments)
		{
			if (indexes.Length != 0)
			{
				if (indexes.Length != arguments.Count)
				{
					throw Error.IncorrectNumberOfMethodCallArguments(method);
				}
				Expression[] array = null;
				int i = 0;
				int num = indexes.Length;
				while (i < num)
				{
					Expression expression = arguments[i];
					ParameterInfo parameterInfo = indexes[i];
					Expression.RequiresCanRead(expression, "arguments");
					Type parameterType = parameterInfo.ParameterType;
					if (parameterType.IsByRef)
					{
						throw Error.AccessorsCannotHaveByRefArgs();
					}
					TypeUtils.ValidateType(parameterType);
					if (!TypeUtils.AreReferenceAssignable(parameterType, expression.Type) && !Expression.TryQuote(parameterType, ref expression))
					{
						throw Error.ExpressionTypeDoesNotMatchMethodParameter(expression.Type, parameterType, method);
					}
					if (array == null && expression != arguments[i])
					{
						array = new Expression[arguments.Count];
						for (int j = 0; j < i; j++)
						{
							array[j] = arguments[j];
						}
					}
					if (array != null)
					{
						array[i] = expression;
					}
					i++;
				}
				if (array != null)
				{
					arguments = new TrueReadOnlyCollection<Expression>(array);
					return;
				}
			}
			else if (arguments.Count > 0)
			{
				throw Error.IncorrectNumberOfMethodCallArguments(method);
			}
		}

		// Token: 0x060012F0 RID: 4848 RVA: 0x000406D0 File Offset: 0x0003E8D0
		[__DynamicallyInvokable]
		public static InvocationExpression Invoke(Expression expression, params Expression[] arguments)
		{
			return Expression.Invoke(expression, arguments);
		}

		// Token: 0x060012F1 RID: 4849 RVA: 0x000406DC File Offset: 0x0003E8DC
		[__DynamicallyInvokable]
		public static InvocationExpression Invoke(Expression expression, IEnumerable<Expression> arguments)
		{
			Expression.RequiresCanRead(expression, "expression");
			ReadOnlyCollection<Expression> arguments2 = arguments.ToReadOnly<Expression>();
			MethodInfo invokeMethod = Expression.GetInvokeMethod(expression);
			Expression.ValidateArgumentTypes(invokeMethod, ExpressionType.Invoke, ref arguments2);
			return new InvocationExpression(expression, arguments2, invokeMethod.ReturnType);
		}

		// Token: 0x060012F2 RID: 4850 RVA: 0x0004071C File Offset: 0x0003E91C
		internal static MethodInfo GetInvokeMethod(Expression expression)
		{
			Type type = expression.Type;
			if (!expression.Type.IsSubclassOf(typeof(MulticastDelegate)))
			{
				Type type2 = TypeUtils.FindGenericType(typeof(Expression<>), expression.Type);
				if (type2 == null)
				{
					throw Error.ExpressionTypeNotInvocable(expression.Type);
				}
				type = type2.GetGenericArguments()[0];
			}
			return type.GetMethod("Invoke");
		}

		// Token: 0x060012F3 RID: 4851 RVA: 0x00040786 File Offset: 0x0003E986
		[__DynamicallyInvokable]
		public static LabelExpression Label(LabelTarget target)
		{
			return Expression.Label(target, null);
		}

		// Token: 0x060012F4 RID: 4852 RVA: 0x0004078F File Offset: 0x0003E98F
		[__DynamicallyInvokable]
		public static LabelExpression Label(LabelTarget target, Expression defaultValue)
		{
			Expression.ValidateGoto(target, ref defaultValue, "label", "defaultValue");
			return new LabelExpression(target, defaultValue);
		}

		// Token: 0x060012F5 RID: 4853 RVA: 0x000407AA File Offset: 0x0003E9AA
		[__DynamicallyInvokable]
		public static LabelTarget Label()
		{
			return Expression.Label(typeof(void), null);
		}

		// Token: 0x060012F6 RID: 4854 RVA: 0x000407BC File Offset: 0x0003E9BC
		[__DynamicallyInvokable]
		public static LabelTarget Label(string name)
		{
			return Expression.Label(typeof(void), name);
		}

		// Token: 0x060012F7 RID: 4855 RVA: 0x000407CE File Offset: 0x0003E9CE
		[__DynamicallyInvokable]
		public static LabelTarget Label(Type type)
		{
			return Expression.Label(type, null);
		}

		// Token: 0x060012F8 RID: 4856 RVA: 0x000407D7 File Offset: 0x0003E9D7
		[__DynamicallyInvokable]
		public static LabelTarget Label(Type type, string name)
		{
			ContractUtils.RequiresNotNull(type, "type");
			TypeUtils.ValidateType(type);
			return new LabelTarget(type, name);
		}

		// Token: 0x060012F9 RID: 4857 RVA: 0x000407F4 File Offset: 0x0003E9F4
		internal static LambdaExpression CreateLambda(Type delegateType, Expression body, string name, bool tailCall, ReadOnlyCollection<ParameterExpression> parameters)
		{
			CacheDict<Type, Expression.LambdaFactory> cacheDict = Expression._LambdaFactories;
			if (cacheDict == null)
			{
				cacheDict = (Expression._LambdaFactories = new CacheDict<Type, Expression.LambdaFactory>(50));
			}
			MethodInfo methodInfo = null;
			Expression.LambdaFactory lambdaFactory;
			if (!cacheDict.TryGetValue(delegateType, out lambdaFactory))
			{
				methodInfo = typeof(Expression<>).MakeGenericType(new Type[]
				{
					delegateType
				}).GetMethod("Create", BindingFlags.Static | BindingFlags.NonPublic);
				if (delegateType.CanCache())
				{
					lambdaFactory = (cacheDict[delegateType] = (Expression.LambdaFactory)Delegate.CreateDelegate(typeof(Expression.LambdaFactory), methodInfo));
				}
			}
			if (lambdaFactory != null)
			{
				return lambdaFactory(body, name, tailCall, parameters);
			}
			return (LambdaExpression)methodInfo.Invoke(null, new object[]
			{
				body,
				name,
				tailCall,
				parameters
			});
		}

		// Token: 0x060012FA RID: 4858 RVA: 0x000408AC File Offset: 0x0003EAAC
		[__DynamicallyInvokable]
		public static Expression<TDelegate> Lambda<TDelegate>(Expression body, params ParameterExpression[] parameters)
		{
			return Expression.Lambda<TDelegate>(body, false, parameters);
		}

		// Token: 0x060012FB RID: 4859 RVA: 0x000408B6 File Offset: 0x0003EAB6
		[__DynamicallyInvokable]
		public static Expression<TDelegate> Lambda<TDelegate>(Expression body, bool tailCall, params ParameterExpression[] parameters)
		{
			return Expression.Lambda<TDelegate>(body, tailCall, parameters);
		}

		// Token: 0x060012FC RID: 4860 RVA: 0x000408C0 File Offset: 0x0003EAC0
		[__DynamicallyInvokable]
		public static Expression<TDelegate> Lambda<TDelegate>(Expression body, IEnumerable<ParameterExpression> parameters)
		{
			return Expression.Lambda<TDelegate>(body, null, false, parameters);
		}

		// Token: 0x060012FD RID: 4861 RVA: 0x000408CB File Offset: 0x0003EACB
		[__DynamicallyInvokable]
		public static Expression<TDelegate> Lambda<TDelegate>(Expression body, bool tailCall, IEnumerable<ParameterExpression> parameters)
		{
			return Expression.Lambda<TDelegate>(body, null, tailCall, parameters);
		}

		// Token: 0x060012FE RID: 4862 RVA: 0x000408D6 File Offset: 0x0003EAD6
		[__DynamicallyInvokable]
		public static Expression<TDelegate> Lambda<TDelegate>(Expression body, string name, IEnumerable<ParameterExpression> parameters)
		{
			return Expression.Lambda<TDelegate>(body, name, false, parameters);
		}

		// Token: 0x060012FF RID: 4863 RVA: 0x000408E4 File Offset: 0x0003EAE4
		[__DynamicallyInvokable]
		public static Expression<TDelegate> Lambda<TDelegate>(Expression body, string name, bool tailCall, IEnumerable<ParameterExpression> parameters)
		{
			ReadOnlyCollection<ParameterExpression> parameters2 = parameters.ToReadOnly<ParameterExpression>();
			Expression.ValidateLambdaArgs(typeof(TDelegate), ref body, parameters2);
			return new Expression<TDelegate>(body, name, tailCall, parameters2);
		}

		// Token: 0x06001300 RID: 4864 RVA: 0x00040913 File Offset: 0x0003EB13
		[__DynamicallyInvokable]
		public static LambdaExpression Lambda(Expression body, params ParameterExpression[] parameters)
		{
			return Expression.Lambda(body, false, parameters);
		}

		// Token: 0x06001301 RID: 4865 RVA: 0x0004091D File Offset: 0x0003EB1D
		[__DynamicallyInvokable]
		public static LambdaExpression Lambda(Expression body, bool tailCall, params ParameterExpression[] parameters)
		{
			return Expression.Lambda(body, tailCall, parameters);
		}

		// Token: 0x06001302 RID: 4866 RVA: 0x00040927 File Offset: 0x0003EB27
		[__DynamicallyInvokable]
		public static LambdaExpression Lambda(Expression body, IEnumerable<ParameterExpression> parameters)
		{
			return Expression.Lambda(body, null, false, parameters);
		}

		// Token: 0x06001303 RID: 4867 RVA: 0x00040932 File Offset: 0x0003EB32
		[__DynamicallyInvokable]
		public static LambdaExpression Lambda(Expression body, bool tailCall, IEnumerable<ParameterExpression> parameters)
		{
			return Expression.Lambda(body, null, tailCall, parameters);
		}

		// Token: 0x06001304 RID: 4868 RVA: 0x0004093D File Offset: 0x0003EB3D
		[__DynamicallyInvokable]
		public static LambdaExpression Lambda(Type delegateType, Expression body, params ParameterExpression[] parameters)
		{
			return Expression.Lambda(delegateType, body, null, false, parameters);
		}

		// Token: 0x06001305 RID: 4869 RVA: 0x00040949 File Offset: 0x0003EB49
		[__DynamicallyInvokable]
		public static LambdaExpression Lambda(Type delegateType, Expression body, bool tailCall, params ParameterExpression[] parameters)
		{
			return Expression.Lambda(delegateType, body, null, tailCall, parameters);
		}

		// Token: 0x06001306 RID: 4870 RVA: 0x00040955 File Offset: 0x0003EB55
		[__DynamicallyInvokable]
		public static LambdaExpression Lambda(Type delegateType, Expression body, IEnumerable<ParameterExpression> parameters)
		{
			return Expression.Lambda(delegateType, body, null, false, parameters);
		}

		// Token: 0x06001307 RID: 4871 RVA: 0x00040961 File Offset: 0x0003EB61
		[__DynamicallyInvokable]
		public static LambdaExpression Lambda(Type delegateType, Expression body, bool tailCall, IEnumerable<ParameterExpression> parameters)
		{
			return Expression.Lambda(delegateType, body, null, tailCall, parameters);
		}

		// Token: 0x06001308 RID: 4872 RVA: 0x0004096D File Offset: 0x0003EB6D
		[__DynamicallyInvokable]
		public static LambdaExpression Lambda(Expression body, string name, IEnumerable<ParameterExpression> parameters)
		{
			return Expression.Lambda(body, name, false, parameters);
		}

		// Token: 0x06001309 RID: 4873 RVA: 0x00040978 File Offset: 0x0003EB78
		[__DynamicallyInvokable]
		public static LambdaExpression Lambda(Expression body, string name, bool tailCall, IEnumerable<ParameterExpression> parameters)
		{
			ContractUtils.RequiresNotNull(body, "body");
			ReadOnlyCollection<ParameterExpression> readOnlyCollection = parameters.ToReadOnly<ParameterExpression>();
			int count = readOnlyCollection.Count;
			Type[] array = new Type[count + 1];
			if (count > 0)
			{
				Set<ParameterExpression> set = new Set<ParameterExpression>(readOnlyCollection.Count);
				for (int i = 0; i < count; i++)
				{
					ParameterExpression parameterExpression = readOnlyCollection[i];
					ContractUtils.RequiresNotNull(parameterExpression, "parameter");
					array[i] = (parameterExpression.IsByRef ? parameterExpression.Type.MakeByRefType() : parameterExpression.Type);
					if (set.Contains(parameterExpression))
					{
						throw Error.DuplicateVariable(parameterExpression);
					}
					set.Add(parameterExpression);
				}
			}
			array[count] = body.Type;
			Type delegateType = DelegateHelpers.MakeDelegateType(array);
			return Expression.CreateLambda(delegateType, body, name, tailCall, readOnlyCollection);
		}

		// Token: 0x0600130A RID: 4874 RVA: 0x00040A38 File Offset: 0x0003EC38
		[__DynamicallyInvokable]
		public static LambdaExpression Lambda(Type delegateType, Expression body, string name, IEnumerable<ParameterExpression> parameters)
		{
			ReadOnlyCollection<ParameterExpression> parameters2 = parameters.ToReadOnly<ParameterExpression>();
			Expression.ValidateLambdaArgs(delegateType, ref body, parameters2);
			return Expression.CreateLambda(delegateType, body, name, false, parameters2);
		}

		// Token: 0x0600130B RID: 4875 RVA: 0x00040A60 File Offset: 0x0003EC60
		[__DynamicallyInvokable]
		public static LambdaExpression Lambda(Type delegateType, Expression body, string name, bool tailCall, IEnumerable<ParameterExpression> parameters)
		{
			ReadOnlyCollection<ParameterExpression> parameters2 = parameters.ToReadOnly<ParameterExpression>();
			Expression.ValidateLambdaArgs(delegateType, ref body, parameters2);
			return Expression.CreateLambda(delegateType, body, name, tailCall, parameters2);
		}

		// Token: 0x0600130C RID: 4876 RVA: 0x00040A88 File Offset: 0x0003EC88
		private static void ValidateLambdaArgs(Type delegateType, ref Expression body, ReadOnlyCollection<ParameterExpression> parameters)
		{
			ContractUtils.RequiresNotNull(delegateType, "delegateType");
			Expression.RequiresCanRead(body, "body");
			if (!typeof(MulticastDelegate).IsAssignableFrom(delegateType) || delegateType == typeof(MulticastDelegate))
			{
				throw Error.LambdaTypeMustBeDerivedFromSystemDelegate();
			}
			CacheDict<Type, MethodInfo> lambdaDelegateCache = Expression._LambdaDelegateCache;
			MethodInfo method;
			if (!lambdaDelegateCache.TryGetValue(delegateType, out method))
			{
				method = delegateType.GetMethod("Invoke");
				if (delegateType.CanCache())
				{
					lambdaDelegateCache[delegateType] = method;
				}
			}
			ParameterInfo[] parametersCached = method.GetParametersCached();
			if (parametersCached.Length != 0)
			{
				if (parametersCached.Length != parameters.Count)
				{
					throw Error.IncorrectNumberOfLambdaDeclarationParameters();
				}
				Set<ParameterExpression> set = new Set<ParameterExpression>(parametersCached.Length);
				int i = 0;
				int num = parametersCached.Length;
				while (i < num)
				{
					ParameterExpression parameterExpression = parameters[i];
					ParameterInfo parameterInfo = parametersCached[i];
					Expression.RequiresCanRead(parameterExpression, "parameters");
					Type type = parameterInfo.ParameterType;
					if (parameterExpression.IsByRef)
					{
						if (!type.IsByRef)
						{
							throw Error.ParameterExpressionNotValidAsDelegate(parameterExpression.Type.MakeByRefType(), type);
						}
						type = type.GetElementType();
					}
					if (!TypeUtils.AreReferenceAssignable(parameterExpression.Type, type))
					{
						throw Error.ParameterExpressionNotValidAsDelegate(parameterExpression.Type, type);
					}
					if (set.Contains(parameterExpression))
					{
						throw Error.DuplicateVariable(parameterExpression);
					}
					set.Add(parameterExpression);
					i++;
				}
			}
			else if (parameters.Count > 0)
			{
				throw Error.IncorrectNumberOfLambdaDeclarationParameters();
			}
			if (method.ReturnType != typeof(void) && !TypeUtils.AreReferenceAssignable(method.ReturnType, body.Type) && !Expression.TryQuote(method.ReturnType, ref body))
			{
				throw Error.ExpressionTypeDoesNotMatchReturn(body.Type, method.ReturnType);
			}
		}

		// Token: 0x0600130D RID: 4877 RVA: 0x00040C34 File Offset: 0x0003EE34
		private static bool ValidateTryGetFuncActionArgs(Type[] typeArgs)
		{
			if (typeArgs == null)
			{
				throw new ArgumentNullException("typeArgs");
			}
			int i = 0;
			int num = typeArgs.Length;
			while (i < num)
			{
				Type type = typeArgs[i];
				if (type == null)
				{
					throw new ArgumentNullException("typeArgs");
				}
				if (type.IsByRef)
				{
					return false;
				}
				i++;
			}
			return true;
		}

		// Token: 0x0600130E RID: 4878 RVA: 0x00040C84 File Offset: 0x0003EE84
		[__DynamicallyInvokable]
		public static Type GetFuncType(params Type[] typeArgs)
		{
			if (!Expression.ValidateTryGetFuncActionArgs(typeArgs))
			{
				throw Error.TypeMustNotBeByRef();
			}
			Type funcType = DelegateHelpers.GetFuncType(typeArgs);
			if (funcType == null)
			{
				throw Error.IncorrectNumberOfTypeArgsForFunc();
			}
			return funcType;
		}

		// Token: 0x0600130F RID: 4879 RVA: 0x00040CB8 File Offset: 0x0003EEB8
		[__DynamicallyInvokable]
		public static bool TryGetFuncType(Type[] typeArgs, out Type funcType)
		{
			if (Expression.ValidateTryGetFuncActionArgs(typeArgs))
			{
				Type funcType2;
				funcType = (funcType2 = DelegateHelpers.GetFuncType(typeArgs));
				return funcType2 != null;
			}
			funcType = null;
			return false;
		}

		// Token: 0x06001310 RID: 4880 RVA: 0x00040CE4 File Offset: 0x0003EEE4
		[__DynamicallyInvokable]
		public static Type GetActionType(params Type[] typeArgs)
		{
			if (!Expression.ValidateTryGetFuncActionArgs(typeArgs))
			{
				throw Error.TypeMustNotBeByRef();
			}
			Type actionType = DelegateHelpers.GetActionType(typeArgs);
			if (actionType == null)
			{
				throw Error.IncorrectNumberOfTypeArgsForAction();
			}
			return actionType;
		}

		// Token: 0x06001311 RID: 4881 RVA: 0x00040D18 File Offset: 0x0003EF18
		[__DynamicallyInvokable]
		public static bool TryGetActionType(Type[] typeArgs, out Type actionType)
		{
			if (Expression.ValidateTryGetFuncActionArgs(typeArgs))
			{
				Type actionType2;
				actionType = (actionType2 = DelegateHelpers.GetActionType(typeArgs));
				return actionType2 != null;
			}
			actionType = null;
			return false;
		}

		// Token: 0x06001312 RID: 4882 RVA: 0x00040D43 File Offset: 0x0003EF43
		[__DynamicallyInvokable]
		public static Type GetDelegateType(params Type[] typeArgs)
		{
			ContractUtils.RequiresNotEmpty<Type>(typeArgs, "typeArgs");
			ContractUtils.RequiresNotNullItems<Type>(typeArgs, "typeArgs");
			return DelegateHelpers.MakeDelegateType(typeArgs);
		}

		// Token: 0x06001313 RID: 4883 RVA: 0x00040D61 File Offset: 0x0003EF61
		[__DynamicallyInvokable]
		public static ListInitExpression ListInit(NewExpression newExpression, params Expression[] initializers)
		{
			ContractUtils.RequiresNotNull(newExpression, "newExpression");
			ContractUtils.RequiresNotNull(initializers, "initializers");
			return Expression.ListInit(newExpression, initializers);
		}

		// Token: 0x06001314 RID: 4884 RVA: 0x00040D80 File Offset: 0x0003EF80
		[__DynamicallyInvokable]
		public static ListInitExpression ListInit(NewExpression newExpression, IEnumerable<Expression> initializers)
		{
			ContractUtils.RequiresNotNull(newExpression, "newExpression");
			ContractUtils.RequiresNotNull(initializers, "initializers");
			ReadOnlyCollection<Expression> readOnlyCollection = initializers.ToReadOnly<Expression>();
			if (readOnlyCollection.Count == 0)
			{
				throw Error.ListInitializerWithZeroMembers();
			}
			MethodInfo addMethod = Expression.FindMethod(newExpression.Type, "Add", null, new Expression[]
			{
				readOnlyCollection[0]
			}, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			return Expression.ListInit(newExpression, addMethod, initializers);
		}

		// Token: 0x06001315 RID: 4885 RVA: 0x00040DE4 File Offset: 0x0003EFE4
		[__DynamicallyInvokable]
		public static ListInitExpression ListInit(NewExpression newExpression, MethodInfo addMethod, params Expression[] initializers)
		{
			if (addMethod == null)
			{
				return Expression.ListInit(newExpression, initializers);
			}
			ContractUtils.RequiresNotNull(newExpression, "newExpression");
			ContractUtils.RequiresNotNull(initializers, "initializers");
			return Expression.ListInit(newExpression, addMethod, initializers);
		}

		// Token: 0x06001316 RID: 4886 RVA: 0x00040E18 File Offset: 0x0003F018
		[__DynamicallyInvokable]
		public static ListInitExpression ListInit(NewExpression newExpression, MethodInfo addMethod, IEnumerable<Expression> initializers)
		{
			if (addMethod == null)
			{
				return Expression.ListInit(newExpression, initializers);
			}
			ContractUtils.RequiresNotNull(newExpression, "newExpression");
			ContractUtils.RequiresNotNull(initializers, "initializers");
			ReadOnlyCollection<Expression> readOnlyCollection = initializers.ToReadOnly<Expression>();
			if (readOnlyCollection.Count == 0)
			{
				throw Error.ListInitializerWithZeroMembers();
			}
			ElementInit[] array = new ElementInit[readOnlyCollection.Count];
			for (int i = 0; i < readOnlyCollection.Count; i++)
			{
				array[i] = Expression.ElementInit(addMethod, new Expression[]
				{
					readOnlyCollection[i]
				});
			}
			return Expression.ListInit(newExpression, new TrueReadOnlyCollection<ElementInit>(array));
		}

		// Token: 0x06001317 RID: 4887 RVA: 0x00040EA3 File Offset: 0x0003F0A3
		[__DynamicallyInvokable]
		public static ListInitExpression ListInit(NewExpression newExpression, params ElementInit[] initializers)
		{
			return Expression.ListInit(newExpression, initializers);
		}

		// Token: 0x06001318 RID: 4888 RVA: 0x00040EAC File Offset: 0x0003F0AC
		[__DynamicallyInvokable]
		public static ListInitExpression ListInit(NewExpression newExpression, IEnumerable<ElementInit> initializers)
		{
			ContractUtils.RequiresNotNull(newExpression, "newExpression");
			ContractUtils.RequiresNotNull(initializers, "initializers");
			ReadOnlyCollection<ElementInit> readOnlyCollection = initializers.ToReadOnly<ElementInit>();
			if (readOnlyCollection.Count == 0)
			{
				throw Error.ListInitializerWithZeroMembers();
			}
			Expression.ValidateListInitArgs(newExpression.Type, readOnlyCollection);
			return new ListInitExpression(newExpression, readOnlyCollection);
		}

		// Token: 0x06001319 RID: 4889 RVA: 0x00040EF7 File Offset: 0x0003F0F7
		[__DynamicallyInvokable]
		public static LoopExpression Loop(Expression body)
		{
			return Expression.Loop(body, null);
		}

		// Token: 0x0600131A RID: 4890 RVA: 0x00040F00 File Offset: 0x0003F100
		[__DynamicallyInvokable]
		public static LoopExpression Loop(Expression body, LabelTarget @break)
		{
			return Expression.Loop(body, @break, null);
		}

		// Token: 0x0600131B RID: 4891 RVA: 0x00040F0A File Offset: 0x0003F10A
		[__DynamicallyInvokable]
		public static LoopExpression Loop(Expression body, LabelTarget @break, LabelTarget @continue)
		{
			Expression.RequiresCanRead(body, "body");
			if (@continue != null && @continue.Type != typeof(void))
			{
				throw Error.LabelTypeMustBeVoid();
			}
			return new LoopExpression(body, @break, @continue);
		}

		// Token: 0x0600131C RID: 4892 RVA: 0x00040F40 File Offset: 0x0003F140
		[__DynamicallyInvokable]
		public static MemberAssignment Bind(MemberInfo member, Expression expression)
		{
			ContractUtils.RequiresNotNull(member, "member");
			Expression.RequiresCanRead(expression, "expression");
			Type type;
			Expression.ValidateSettableFieldOrPropertyMember(member, out type);
			if (!type.IsAssignableFrom(expression.Type))
			{
				throw Error.ArgumentTypesMustMatch();
			}
			return new MemberAssignment(member, expression);
		}

		// Token: 0x0600131D RID: 4893 RVA: 0x00040F86 File Offset: 0x0003F186
		[__DynamicallyInvokable]
		public static MemberAssignment Bind(MethodInfo propertyAccessor, Expression expression)
		{
			ContractUtils.RequiresNotNull(propertyAccessor, "propertyAccessor");
			ContractUtils.RequiresNotNull(expression, "expression");
			Expression.ValidateMethodInfo(propertyAccessor);
			return Expression.Bind(Expression.GetProperty(propertyAccessor), expression);
		}

		// Token: 0x0600131E RID: 4894 RVA: 0x00040FB0 File Offset: 0x0003F1B0
		private static void ValidateSettableFieldOrPropertyMember(MemberInfo member, out Type memberType)
		{
			FieldInfo fieldInfo = member as FieldInfo;
			if (!(fieldInfo == null))
			{
				memberType = fieldInfo.FieldType;
				return;
			}
			PropertyInfo propertyInfo = member as PropertyInfo;
			if (propertyInfo == null)
			{
				throw Error.ArgumentMustBeFieldInfoOrPropertInfo();
			}
			if (!propertyInfo.CanWrite)
			{
				throw Error.PropertyDoesNotHaveSetter(propertyInfo);
			}
			memberType = propertyInfo.PropertyType;
		}

		// Token: 0x0600131F RID: 4895 RVA: 0x00041004 File Offset: 0x0003F204
		[__DynamicallyInvokable]
		public static MemberExpression Field(Expression expression, FieldInfo field)
		{
			ContractUtils.RequiresNotNull(field, "field");
			if (field.IsStatic)
			{
				if (expression != null)
				{
					throw new ArgumentException(Strings.OnlyStaticFieldsHaveNullInstance, "expression");
				}
			}
			else
			{
				if (expression == null)
				{
					throw new ArgumentException(Strings.OnlyStaticFieldsHaveNullInstance, "field");
				}
				Expression.RequiresCanRead(expression, "expression");
				if (!TypeUtils.AreReferenceAssignable(field.DeclaringType, expression.Type))
				{
					throw Error.FieldInfoNotDefinedForType(field.DeclaringType, field.Name, expression.Type);
				}
			}
			return MemberExpression.Make(expression, field);
		}

		// Token: 0x06001320 RID: 4896 RVA: 0x00041088 File Offset: 0x0003F288
		[__DynamicallyInvokable]
		public static MemberExpression Field(Expression expression, string fieldName)
		{
			Expression.RequiresCanRead(expression, "expression");
			FieldInfo field = expression.Type.GetField(fieldName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy);
			if (field == null)
			{
				field = expression.Type.GetField(fieldName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
			}
			if (field == null)
			{
				throw Error.InstanceFieldNotDefinedForType(fieldName, expression.Type);
			}
			return Expression.Field(expression, field);
		}

		// Token: 0x06001321 RID: 4897 RVA: 0x000410E4 File Offset: 0x0003F2E4
		[__DynamicallyInvokable]
		public static MemberExpression Field(Expression expression, Type type, string fieldName)
		{
			ContractUtils.RequiresNotNull(type, "type");
			FieldInfo field = type.GetField(fieldName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy);
			if (field == null)
			{
				field = type.GetField(fieldName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
			}
			if (field == null)
			{
				throw Error.FieldNotDefinedForType(fieldName, type);
			}
			return Expression.Field(expression, field);
		}

		// Token: 0x06001322 RID: 4898 RVA: 0x00041134 File Offset: 0x0003F334
		[__DynamicallyInvokable]
		public static MemberExpression Property(Expression expression, string propertyName)
		{
			Expression.RequiresCanRead(expression, "expression");
			ContractUtils.RequiresNotNull(propertyName, "propertyName");
			PropertyInfo property = expression.Type.GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy);
			if (property == null)
			{
				property = expression.Type.GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
			}
			if (property == null)
			{
				throw Error.InstancePropertyNotDefinedForType(propertyName, expression.Type);
			}
			return Expression.Property(expression, property);
		}

		// Token: 0x06001323 RID: 4899 RVA: 0x0004119C File Offset: 0x0003F39C
		[__DynamicallyInvokable]
		public static MemberExpression Property(Expression expression, Type type, string propertyName)
		{
			ContractUtils.RequiresNotNull(type, "type");
			ContractUtils.RequiresNotNull(propertyName, "propertyName");
			PropertyInfo property = type.GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy);
			if (property == null)
			{
				property = type.GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
			}
			if (property == null)
			{
				throw Error.PropertyNotDefinedForType(propertyName, type);
			}
			return Expression.Property(expression, property);
		}

		// Token: 0x06001324 RID: 4900 RVA: 0x000411F4 File Offset: 0x0003F3F4
		[__DynamicallyInvokable]
		public static MemberExpression Property(Expression expression, PropertyInfo property)
		{
			ContractUtils.RequiresNotNull(property, "property");
			MethodInfo methodInfo = property.GetGetMethod(true) ?? property.GetSetMethod(true);
			if (methodInfo == null)
			{
				throw Error.PropertyDoesNotHaveAccessor(property);
			}
			if (methodInfo.IsStatic)
			{
				if (expression != null)
				{
					throw new ArgumentException(Strings.OnlyStaticPropertiesHaveNullInstance, "expression");
				}
			}
			else
			{
				if (expression == null)
				{
					throw new ArgumentException(Strings.OnlyStaticPropertiesHaveNullInstance, "property");
				}
				Expression.RequiresCanRead(expression, "expression");
				if (!TypeUtils.IsValidInstanceType(property, expression.Type))
				{
					throw Error.PropertyNotDefinedForType(property, expression.Type);
				}
			}
			return MemberExpression.Make(expression, property);
		}

		// Token: 0x06001325 RID: 4901 RVA: 0x0004128A File Offset: 0x0003F48A
		[__DynamicallyInvokable]
		public static MemberExpression Property(Expression expression, MethodInfo propertyAccessor)
		{
			ContractUtils.RequiresNotNull(propertyAccessor, "propertyAccessor");
			Expression.ValidateMethodInfo(propertyAccessor);
			return Expression.Property(expression, Expression.GetProperty(propertyAccessor));
		}

		// Token: 0x06001326 RID: 4902 RVA: 0x000412AC File Offset: 0x0003F4AC
		private static PropertyInfo GetProperty(MethodInfo mi)
		{
			Type declaringType = mi.DeclaringType;
			BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic;
			bindingFlags |= (mi.IsStatic ? BindingFlags.Static : BindingFlags.Instance);
			PropertyInfo[] properties = declaringType.GetProperties(bindingFlags);
			foreach (PropertyInfo propertyInfo in properties)
			{
				if (propertyInfo.CanRead && Expression.CheckMethod(mi, propertyInfo.GetGetMethod(true)))
				{
					return propertyInfo;
				}
				if (propertyInfo.CanWrite && Expression.CheckMethod(mi, propertyInfo.GetSetMethod(true)))
				{
					return propertyInfo;
				}
			}
			throw Error.MethodNotPropertyAccessor(mi.DeclaringType, mi.Name);
		}

		// Token: 0x06001327 RID: 4903 RVA: 0x00041340 File Offset: 0x0003F540
		private static bool CheckMethod(MethodInfo method, MethodInfo propertyMethod)
		{
			if (method == propertyMethod)
			{
				return true;
			}
			Type declaringType = method.DeclaringType;
			return declaringType.IsInterface && method.Name == propertyMethod.Name && declaringType.GetMethod(method.Name) == propertyMethod;
		}

		// Token: 0x06001328 RID: 4904 RVA: 0x00041394 File Offset: 0x0003F594
		[__DynamicallyInvokable]
		public static MemberExpression PropertyOrField(Expression expression, string propertyOrFieldName)
		{
			Expression.RequiresCanRead(expression, "expression");
			PropertyInfo property = expression.Type.GetProperty(propertyOrFieldName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy);
			if (property != null)
			{
				return Expression.Property(expression, property);
			}
			FieldInfo field = expression.Type.GetField(propertyOrFieldName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy);
			if (field != null)
			{
				return Expression.Field(expression, field);
			}
			property = expression.Type.GetProperty(propertyOrFieldName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
			if (property != null)
			{
				return Expression.Property(expression, property);
			}
			field = expression.Type.GetField(propertyOrFieldName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
			if (field != null)
			{
				return Expression.Field(expression, field);
			}
			throw Error.NotAMemberOfType(propertyOrFieldName, expression.Type);
		}

		// Token: 0x06001329 RID: 4905 RVA: 0x00041438 File Offset: 0x0003F638
		[__DynamicallyInvokable]
		public static MemberExpression MakeMemberAccess(Expression expression, MemberInfo member)
		{
			ContractUtils.RequiresNotNull(member, "member");
			FieldInfo fieldInfo = member as FieldInfo;
			if (fieldInfo != null)
			{
				return Expression.Field(expression, fieldInfo);
			}
			PropertyInfo propertyInfo = member as PropertyInfo;
			if (propertyInfo != null)
			{
				return Expression.Property(expression, propertyInfo);
			}
			throw Error.MemberNotFieldOrProperty(member);
		}

		// Token: 0x0600132A RID: 4906 RVA: 0x00041486 File Offset: 0x0003F686
		[__DynamicallyInvokable]
		public static MemberInitExpression MemberInit(NewExpression newExpression, params MemberBinding[] bindings)
		{
			return Expression.MemberInit(newExpression, bindings);
		}

		// Token: 0x0600132B RID: 4907 RVA: 0x00041490 File Offset: 0x0003F690
		[__DynamicallyInvokable]
		public static MemberInitExpression MemberInit(NewExpression newExpression, IEnumerable<MemberBinding> bindings)
		{
			ContractUtils.RequiresNotNull(newExpression, "newExpression");
			ContractUtils.RequiresNotNull(bindings, "bindings");
			ReadOnlyCollection<MemberBinding> bindings2 = bindings.ToReadOnly<MemberBinding>();
			Expression.ValidateMemberInitArgs(newExpression.Type, bindings2);
			return new MemberInitExpression(newExpression, bindings2);
		}

		// Token: 0x0600132C RID: 4908 RVA: 0x000414CD File Offset: 0x0003F6CD
		[__DynamicallyInvokable]
		public static MemberListBinding ListBind(MemberInfo member, params ElementInit[] initializers)
		{
			ContractUtils.RequiresNotNull(member, "member");
			ContractUtils.RequiresNotNull(initializers, "initializers");
			return Expression.ListBind(member, initializers);
		}

		// Token: 0x0600132D RID: 4909 RVA: 0x000414EC File Offset: 0x0003F6EC
		[__DynamicallyInvokable]
		public static MemberListBinding ListBind(MemberInfo member, IEnumerable<ElementInit> initializers)
		{
			ContractUtils.RequiresNotNull(member, "member");
			ContractUtils.RequiresNotNull(initializers, "initializers");
			Type listType;
			Expression.ValidateGettableFieldOrPropertyMember(member, out listType);
			ReadOnlyCollection<ElementInit> initializers2 = initializers.ToReadOnly<ElementInit>();
			Expression.ValidateListInitArgs(listType, initializers2);
			return new MemberListBinding(member, initializers2);
		}

		// Token: 0x0600132E RID: 4910 RVA: 0x0004152C File Offset: 0x0003F72C
		[__DynamicallyInvokable]
		public static MemberListBinding ListBind(MethodInfo propertyAccessor, params ElementInit[] initializers)
		{
			ContractUtils.RequiresNotNull(propertyAccessor, "propertyAccessor");
			ContractUtils.RequiresNotNull(initializers, "initializers");
			return Expression.ListBind(propertyAccessor, initializers);
		}

		// Token: 0x0600132F RID: 4911 RVA: 0x0004154B File Offset: 0x0003F74B
		[__DynamicallyInvokable]
		public static MemberListBinding ListBind(MethodInfo propertyAccessor, IEnumerable<ElementInit> initializers)
		{
			ContractUtils.RequiresNotNull(propertyAccessor, "propertyAccessor");
			ContractUtils.RequiresNotNull(initializers, "initializers");
			return Expression.ListBind(Expression.GetProperty(propertyAccessor), initializers);
		}

		// Token: 0x06001330 RID: 4912 RVA: 0x00041570 File Offset: 0x0003F770
		private static void ValidateListInitArgs(Type listType, ReadOnlyCollection<ElementInit> initializers)
		{
			if (!typeof(IEnumerable).IsAssignableFrom(listType))
			{
				throw Error.TypeNotIEnumerable(listType);
			}
			int i = 0;
			int count = initializers.Count;
			while (i < count)
			{
				ElementInit elementInit = initializers[i];
				ContractUtils.RequiresNotNull(elementInit, "initializers");
				Expression.ValidateCallInstanceType(listType, elementInit.AddMethod);
				i++;
			}
		}

		// Token: 0x06001331 RID: 4913 RVA: 0x000415C8 File Offset: 0x0003F7C8
		[__DynamicallyInvokable]
		public static MemberMemberBinding MemberBind(MemberInfo member, params MemberBinding[] bindings)
		{
			ContractUtils.RequiresNotNull(member, "member");
			ContractUtils.RequiresNotNull(bindings, "bindings");
			return Expression.MemberBind(member, bindings);
		}

		// Token: 0x06001332 RID: 4914 RVA: 0x000415E8 File Offset: 0x0003F7E8
		[__DynamicallyInvokable]
		public static MemberMemberBinding MemberBind(MemberInfo member, IEnumerable<MemberBinding> bindings)
		{
			ContractUtils.RequiresNotNull(member, "member");
			ContractUtils.RequiresNotNull(bindings, "bindings");
			ReadOnlyCollection<MemberBinding> bindings2 = bindings.ToReadOnly<MemberBinding>();
			Type type;
			Expression.ValidateGettableFieldOrPropertyMember(member, out type);
			Expression.ValidateMemberInitArgs(type, bindings2);
			return new MemberMemberBinding(member, bindings2);
		}

		// Token: 0x06001333 RID: 4915 RVA: 0x00041628 File Offset: 0x0003F828
		[__DynamicallyInvokable]
		public static MemberMemberBinding MemberBind(MethodInfo propertyAccessor, params MemberBinding[] bindings)
		{
			ContractUtils.RequiresNotNull(propertyAccessor, "propertyAccessor");
			return Expression.MemberBind(Expression.GetProperty(propertyAccessor), bindings);
		}

		// Token: 0x06001334 RID: 4916 RVA: 0x00041641 File Offset: 0x0003F841
		[__DynamicallyInvokable]
		public static MemberMemberBinding MemberBind(MethodInfo propertyAccessor, IEnumerable<MemberBinding> bindings)
		{
			ContractUtils.RequiresNotNull(propertyAccessor, "propertyAccessor");
			return Expression.MemberBind(Expression.GetProperty(propertyAccessor), bindings);
		}

		// Token: 0x06001335 RID: 4917 RVA: 0x0004165C File Offset: 0x0003F85C
		private static void ValidateGettableFieldOrPropertyMember(MemberInfo member, out Type memberType)
		{
			FieldInfo fieldInfo = member as FieldInfo;
			if (!(fieldInfo == null))
			{
				memberType = fieldInfo.FieldType;
				return;
			}
			PropertyInfo propertyInfo = member as PropertyInfo;
			if (propertyInfo == null)
			{
				throw Error.ArgumentMustBeFieldInfoOrPropertInfo();
			}
			if (!propertyInfo.CanRead)
			{
				throw Error.PropertyDoesNotHaveGetter(propertyInfo);
			}
			memberType = propertyInfo.PropertyType;
		}

		// Token: 0x06001336 RID: 4918 RVA: 0x000416B0 File Offset: 0x0003F8B0
		private static void ValidateMemberInitArgs(Type type, ReadOnlyCollection<MemberBinding> bindings)
		{
			int i = 0;
			int count = bindings.Count;
			while (i < count)
			{
				MemberBinding memberBinding = bindings[i];
				ContractUtils.RequiresNotNull(memberBinding, "bindings");
				if (!memberBinding.Member.DeclaringType.IsAssignableFrom(type))
				{
					throw Error.NotAMemberOfType(memberBinding.Member.Name, type);
				}
				i++;
			}
		}

		// Token: 0x06001337 RID: 4919 RVA: 0x00041708 File Offset: 0x0003F908
		[__DynamicallyInvokable]
		public static MethodCallExpression Call(MethodInfo method, Expression arg0)
		{
			ContractUtils.RequiresNotNull(method, "method");
			ContractUtils.RequiresNotNull(arg0, "arg0");
			ParameterInfo[] array = Expression.ValidateMethodAndGetParameters(null, method);
			Expression.ValidateArgumentCount(method, ExpressionType.Call, 1, array);
			arg0 = Expression.ValidateOneArgument(method, ExpressionType.Call, arg0, array[0]);
			return new MethodCallExpression1(method, arg0);
		}

		// Token: 0x06001338 RID: 4920 RVA: 0x00041750 File Offset: 0x0003F950
		[__DynamicallyInvokable]
		public static MethodCallExpression Call(MethodInfo method, Expression arg0, Expression arg1)
		{
			ContractUtils.RequiresNotNull(method, "method");
			ContractUtils.RequiresNotNull(arg0, "arg0");
			ContractUtils.RequiresNotNull(arg1, "arg1");
			ParameterInfo[] array = Expression.ValidateMethodAndGetParameters(null, method);
			Expression.ValidateArgumentCount(method, ExpressionType.Call, 2, array);
			arg0 = Expression.ValidateOneArgument(method, ExpressionType.Call, arg0, array[0]);
			arg1 = Expression.ValidateOneArgument(method, ExpressionType.Call, arg1, array[1]);
			return new MethodCallExpression2(method, arg0, arg1);
		}

		// Token: 0x06001339 RID: 4921 RVA: 0x000417B4 File Offset: 0x0003F9B4
		[__DynamicallyInvokable]
		public static MethodCallExpression Call(MethodInfo method, Expression arg0, Expression arg1, Expression arg2)
		{
			ContractUtils.RequiresNotNull(method, "method");
			ContractUtils.RequiresNotNull(arg0, "arg0");
			ContractUtils.RequiresNotNull(arg1, "arg1");
			ContractUtils.RequiresNotNull(arg2, "arg2");
			ParameterInfo[] array = Expression.ValidateMethodAndGetParameters(null, method);
			Expression.ValidateArgumentCount(method, ExpressionType.Call, 3, array);
			arg0 = Expression.ValidateOneArgument(method, ExpressionType.Call, arg0, array[0]);
			arg1 = Expression.ValidateOneArgument(method, ExpressionType.Call, arg1, array[1]);
			arg2 = Expression.ValidateOneArgument(method, ExpressionType.Call, arg2, array[2]);
			return new MethodCallExpression3(method, arg0, arg1, arg2);
		}

		// Token: 0x0600133A RID: 4922 RVA: 0x00041830 File Offset: 0x0003FA30
		[__DynamicallyInvokable]
		public static MethodCallExpression Call(MethodInfo method, Expression arg0, Expression arg1, Expression arg2, Expression arg3)
		{
			ContractUtils.RequiresNotNull(method, "method");
			ContractUtils.RequiresNotNull(arg0, "arg0");
			ContractUtils.RequiresNotNull(arg1, "arg1");
			ContractUtils.RequiresNotNull(arg2, "arg2");
			ContractUtils.RequiresNotNull(arg3, "arg3");
			ParameterInfo[] array = Expression.ValidateMethodAndGetParameters(null, method);
			Expression.ValidateArgumentCount(method, ExpressionType.Call, 4, array);
			arg0 = Expression.ValidateOneArgument(method, ExpressionType.Call, arg0, array[0]);
			arg1 = Expression.ValidateOneArgument(method, ExpressionType.Call, arg1, array[1]);
			arg2 = Expression.ValidateOneArgument(method, ExpressionType.Call, arg2, array[2]);
			arg3 = Expression.ValidateOneArgument(method, ExpressionType.Call, arg3, array[3]);
			return new MethodCallExpression4(method, arg0, arg1, arg2, arg3);
		}

		// Token: 0x0600133B RID: 4923 RVA: 0x000418C8 File Offset: 0x0003FAC8
		[__DynamicallyInvokable]
		public static MethodCallExpression Call(MethodInfo method, Expression arg0, Expression arg1, Expression arg2, Expression arg3, Expression arg4)
		{
			ContractUtils.RequiresNotNull(method, "method");
			ContractUtils.RequiresNotNull(arg0, "arg0");
			ContractUtils.RequiresNotNull(arg1, "arg1");
			ContractUtils.RequiresNotNull(arg2, "arg2");
			ContractUtils.RequiresNotNull(arg3, "arg3");
			ContractUtils.RequiresNotNull(arg4, "arg4");
			ParameterInfo[] array = Expression.ValidateMethodAndGetParameters(null, method);
			Expression.ValidateArgumentCount(method, ExpressionType.Call, 5, array);
			arg0 = Expression.ValidateOneArgument(method, ExpressionType.Call, arg0, array[0]);
			arg1 = Expression.ValidateOneArgument(method, ExpressionType.Call, arg1, array[1]);
			arg2 = Expression.ValidateOneArgument(method, ExpressionType.Call, arg2, array[2]);
			arg3 = Expression.ValidateOneArgument(method, ExpressionType.Call, arg3, array[3]);
			arg4 = Expression.ValidateOneArgument(method, ExpressionType.Call, arg4, array[4]);
			return new MethodCallExpression5(method, arg0, arg1, arg2, arg3, arg4);
		}

		// Token: 0x0600133C RID: 4924 RVA: 0x0004197A File Offset: 0x0003FB7A
		[__DynamicallyInvokable]
		public static MethodCallExpression Call(MethodInfo method, params Expression[] arguments)
		{
			return Expression.Call(null, method, arguments);
		}

		// Token: 0x0600133D RID: 4925 RVA: 0x00041984 File Offset: 0x0003FB84
		[__DynamicallyInvokable]
		public static MethodCallExpression Call(MethodInfo method, IEnumerable<Expression> arguments)
		{
			return Expression.Call(null, method, arguments);
		}

		// Token: 0x0600133E RID: 4926 RVA: 0x0004198E File Offset: 0x0003FB8E
		[__DynamicallyInvokable]
		public static MethodCallExpression Call(Expression instance, MethodInfo method)
		{
			return Expression.Call(instance, method, EmptyReadOnlyCollection<Expression>.Instance);
		}

		// Token: 0x0600133F RID: 4927 RVA: 0x0004199C File Offset: 0x0003FB9C
		[__DynamicallyInvokable]
		public static MethodCallExpression Call(Expression instance, MethodInfo method, params Expression[] arguments)
		{
			return Expression.Call(instance, method, arguments);
		}

		// Token: 0x06001340 RID: 4928 RVA: 0x000419A8 File Offset: 0x0003FBA8
		[__DynamicallyInvokable]
		public static MethodCallExpression Call(Expression instance, MethodInfo method, Expression arg0, Expression arg1)
		{
			ContractUtils.RequiresNotNull(method, "method");
			ContractUtils.RequiresNotNull(arg0, "arg0");
			ContractUtils.RequiresNotNull(arg1, "arg1");
			ParameterInfo[] array = Expression.ValidateMethodAndGetParameters(instance, method);
			Expression.ValidateArgumentCount(method, ExpressionType.Call, 2, array);
			arg0 = Expression.ValidateOneArgument(method, ExpressionType.Call, arg0, array[0]);
			arg1 = Expression.ValidateOneArgument(method, ExpressionType.Call, arg1, array[1]);
			if (instance != null)
			{
				return new InstanceMethodCallExpression2(method, instance, arg0, arg1);
			}
			return new MethodCallExpression2(method, arg0, arg1);
		}

		// Token: 0x06001341 RID: 4929 RVA: 0x00041A18 File Offset: 0x0003FC18
		[__DynamicallyInvokable]
		public static MethodCallExpression Call(Expression instance, MethodInfo method, Expression arg0, Expression arg1, Expression arg2)
		{
			ContractUtils.RequiresNotNull(method, "method");
			ContractUtils.RequiresNotNull(arg0, "arg0");
			ContractUtils.RequiresNotNull(arg1, "arg1");
			ContractUtils.RequiresNotNull(arg2, "arg2");
			ParameterInfo[] array = Expression.ValidateMethodAndGetParameters(instance, method);
			Expression.ValidateArgumentCount(method, ExpressionType.Call, 3, array);
			arg0 = Expression.ValidateOneArgument(method, ExpressionType.Call, arg0, array[0]);
			arg1 = Expression.ValidateOneArgument(method, ExpressionType.Call, arg1, array[1]);
			arg2 = Expression.ValidateOneArgument(method, ExpressionType.Call, arg2, array[2]);
			if (instance != null)
			{
				return new InstanceMethodCallExpression3(method, instance, arg0, arg1, arg2);
			}
			return new MethodCallExpression3(method, arg0, arg1, arg2);
		}

		// Token: 0x06001342 RID: 4930 RVA: 0x00041AA4 File Offset: 0x0003FCA4
		[__DynamicallyInvokable]
		public static MethodCallExpression Call(Expression instance, string methodName, Type[] typeArguments, params Expression[] arguments)
		{
			ContractUtils.RequiresNotNull(instance, "instance");
			ContractUtils.RequiresNotNull(methodName, "methodName");
			if (arguments == null)
			{
				arguments = new Expression[0];
			}
			BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy;
			return Expression.Call(instance, Expression.FindMethod(instance.Type, methodName, typeArguments, arguments, flags), arguments);
		}

		// Token: 0x06001343 RID: 4931 RVA: 0x00041AEC File Offset: 0x0003FCEC
		[__DynamicallyInvokable]
		public static MethodCallExpression Call(Type type, string methodName, Type[] typeArguments, params Expression[] arguments)
		{
			ContractUtils.RequiresNotNull(type, "type");
			ContractUtils.RequiresNotNull(methodName, "methodName");
			if (arguments == null)
			{
				arguments = new Expression[0];
			}
			BindingFlags flags = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy;
			return Expression.Call(null, Expression.FindMethod(type, methodName, typeArguments, arguments, flags), arguments);
		}

		// Token: 0x06001344 RID: 4932 RVA: 0x00041B30 File Offset: 0x0003FD30
		[__DynamicallyInvokable]
		public static MethodCallExpression Call(Expression instance, MethodInfo method, IEnumerable<Expression> arguments)
		{
			ContractUtils.RequiresNotNull(method, "method");
			ReadOnlyCollection<Expression> args = arguments.ToReadOnly<Expression>();
			Expression.ValidateMethodInfo(method);
			Expression.ValidateStaticOrInstanceMethod(instance, method);
			Expression.ValidateArgumentTypes(method, ExpressionType.Call, ref args);
			if (instance == null)
			{
				return new MethodCallExpressionN(method, args);
			}
			return new InstanceMethodCallExpressionN(method, instance, args);
		}

		// Token: 0x06001345 RID: 4933 RVA: 0x00041B78 File Offset: 0x0003FD78
		private static ParameterInfo[] ValidateMethodAndGetParameters(Expression instance, MethodInfo method)
		{
			Expression.ValidateMethodInfo(method);
			Expression.ValidateStaticOrInstanceMethod(instance, method);
			return Expression.GetParametersForValidation(method, ExpressionType.Call);
		}

		// Token: 0x06001346 RID: 4934 RVA: 0x00041B90 File Offset: 0x0003FD90
		private static void ValidateStaticOrInstanceMethod(Expression instance, MethodInfo method)
		{
			if (method.IsStatic)
			{
				if (instance != null)
				{
					throw new ArgumentException(Strings.OnlyStaticMethodsHaveNullInstance, "instance");
				}
			}
			else
			{
				if (instance == null)
				{
					throw new ArgumentException(Strings.OnlyStaticMethodsHaveNullInstance, "method");
				}
				Expression.RequiresCanRead(instance, "instance");
				Expression.ValidateCallInstanceType(instance.Type, method);
			}
		}

		// Token: 0x06001347 RID: 4935 RVA: 0x00041BE2 File Offset: 0x0003FDE2
		private static void ValidateCallInstanceType(Type instanceType, MethodInfo method)
		{
			if (!TypeUtils.IsValidInstanceType(method, instanceType))
			{
				throw Error.InstanceAndMethodTypeMismatch(method, method.DeclaringType, instanceType);
			}
		}

		// Token: 0x06001348 RID: 4936 RVA: 0x00041BFC File Offset: 0x0003FDFC
		private static void ValidateArgumentTypes(MethodBase method, ExpressionType nodeKind, ref ReadOnlyCollection<Expression> arguments)
		{
			ParameterInfo[] parametersForValidation = Expression.GetParametersForValidation(method, nodeKind);
			Expression.ValidateArgumentCount(method, nodeKind, arguments.Count, parametersForValidation);
			Expression[] array = null;
			int i = 0;
			int num = parametersForValidation.Length;
			while (i < num)
			{
				Expression expression = arguments[i];
				ParameterInfo pi = parametersForValidation[i];
				expression = Expression.ValidateOneArgument(method, nodeKind, expression, pi);
				if (array == null && expression != arguments[i])
				{
					array = new Expression[arguments.Count];
					for (int j = 0; j < i; j++)
					{
						array[j] = arguments[j];
					}
				}
				if (array != null)
				{
					array[i] = expression;
				}
				i++;
			}
			if (array != null)
			{
				arguments = new TrueReadOnlyCollection<Expression>(array);
			}
		}

		// Token: 0x06001349 RID: 4937 RVA: 0x00041C9C File Offset: 0x0003FE9C
		private static ParameterInfo[] GetParametersForValidation(MethodBase method, ExpressionType nodeKind)
		{
			ParameterInfo[] array = method.GetParametersCached();
			if (nodeKind == ExpressionType.Dynamic)
			{
				array = array.RemoveFirst<ParameterInfo>();
			}
			return array;
		}

		// Token: 0x0600134A RID: 4938 RVA: 0x00041CBD File Offset: 0x0003FEBD
		private static void ValidateArgumentCount(MethodBase method, ExpressionType nodeKind, int count, ParameterInfo[] pis)
		{
			if (pis.Length != count)
			{
				if (nodeKind <= ExpressionType.Invoke)
				{
					if (nodeKind != ExpressionType.Call)
					{
						if (nodeKind != ExpressionType.Invoke)
						{
							goto IL_35;
						}
						throw Error.IncorrectNumberOfLambdaArguments();
					}
				}
				else
				{
					if (nodeKind == ExpressionType.New)
					{
						throw Error.IncorrectNumberOfConstructorArguments();
					}
					if (nodeKind != ExpressionType.Dynamic)
					{
						goto IL_35;
					}
				}
				throw Error.IncorrectNumberOfMethodCallArguments(method);
				IL_35:
				throw ContractUtils.Unreachable;
			}
		}

		// Token: 0x0600134B RID: 4939 RVA: 0x00041CFC File Offset: 0x0003FEFC
		private static Expression ValidateOneArgument(MethodBase method, ExpressionType nodeKind, Expression arg, ParameterInfo pi)
		{
			Expression.RequiresCanRead(arg, "arguments");
			Type type = pi.ParameterType;
			if (type.IsByRef)
			{
				type = type.GetElementType();
			}
			TypeUtils.ValidateType(type);
			if (!TypeUtils.AreReferenceAssignable(type, arg.Type) && !Expression.TryQuote(type, ref arg))
			{
				if (nodeKind <= ExpressionType.Invoke)
				{
					if (nodeKind != ExpressionType.Call)
					{
						if (nodeKind != ExpressionType.Invoke)
						{
							goto IL_83;
						}
						throw Error.ExpressionTypeDoesNotMatchParameter(arg.Type, type);
					}
				}
				else
				{
					if (nodeKind == ExpressionType.New)
					{
						throw Error.ExpressionTypeDoesNotMatchConstructorParameter(arg.Type, type);
					}
					if (nodeKind != ExpressionType.Dynamic)
					{
						goto IL_83;
					}
				}
				throw Error.ExpressionTypeDoesNotMatchMethodParameter(arg.Type, type, method);
				IL_83:
				throw ContractUtils.Unreachable;
			}
			return arg;
		}

		// Token: 0x0600134C RID: 4940 RVA: 0x00041D94 File Offset: 0x0003FF94
		private static bool TryQuote(Type parameterType, ref Expression argument)
		{
			Type typeFromHandle = typeof(LambdaExpression);
			if (TypeUtils.IsSameOrSubclass(typeFromHandle, parameterType) && parameterType.IsAssignableFrom(argument.GetType()))
			{
				argument = Expression.Quote(argument);
				return true;
			}
			return false;
		}

		// Token: 0x0600134D RID: 4941 RVA: 0x00041DD0 File Offset: 0x0003FFD0
		private static MethodInfo FindMethod(Type type, string methodName, Type[] typeArgs, Expression[] args, BindingFlags flags)
		{
			MemberInfo[] array = type.FindMembers(MemberTypes.Method, flags, Type.FilterNameIgnoreCase, methodName);
			if (array == null || array.Length == 0)
			{
				throw Error.MethodDoesNotExistOnType(methodName, type);
			}
			MethodInfo[] methods = array.Map((MemberInfo t) => (MethodInfo)t);
			MethodInfo result;
			int num = Expression.FindBestMethod(methods, typeArgs, args, out result);
			if (num == 0)
			{
				if (typeArgs != null && typeArgs.Length != 0)
				{
					throw Error.GenericMethodWithArgsDoesNotExistOnType(methodName, type);
				}
				throw Error.MethodWithArgsDoesNotExistOnType(methodName, type);
			}
			else
			{
				if (num > 1)
				{
					throw Error.MethodWithMoreThanOneMatch(methodName, type);
				}
				return result;
			}
		}

		// Token: 0x0600134E RID: 4942 RVA: 0x00041E54 File Offset: 0x00040054
		private static int FindBestMethod(IEnumerable<MethodInfo> methods, Type[] typeArgs, Expression[] args, out MethodInfo method)
		{
			int num = 0;
			method = null;
			foreach (MethodInfo m in methods)
			{
				MethodInfo methodInfo = Expression.ApplyTypeArgs(m, typeArgs);
				if (methodInfo != null && Expression.IsCompatible(methodInfo, args))
				{
					if (method == null || (!method.IsPublic && methodInfo.IsPublic))
					{
						method = methodInfo;
						num = 1;
					}
					else if (method.IsPublic == methodInfo.IsPublic)
					{
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x0600134F RID: 4943 RVA: 0x00041EEC File Offset: 0x000400EC
		private static bool IsCompatible(MethodBase m, Expression[] args)
		{
			ParameterInfo[] parametersCached = m.GetParametersCached();
			if (parametersCached.Length != args.Length)
			{
				return false;
			}
			for (int i = 0; i < args.Length; i++)
			{
				Expression expression = args[i];
				ContractUtils.RequiresNotNull(expression, "argument");
				Type type = expression.Type;
				Type type2 = parametersCached[i].ParameterType;
				if (type2.IsByRef)
				{
					type2 = type2.GetElementType();
				}
				if (!TypeUtils.AreReferenceAssignable(type2, type) && (!TypeUtils.IsSameOrSubclass(typeof(LambdaExpression), type2) || !type2.IsAssignableFrom(expression.GetType())))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001350 RID: 4944 RVA: 0x00041F79 File Offset: 0x00040179
		private static MethodInfo ApplyTypeArgs(MethodInfo m, Type[] typeArgs)
		{
			if (typeArgs == null || typeArgs.Length == 0)
			{
				if (!m.IsGenericMethodDefinition)
				{
					return m;
				}
			}
			else if (m.IsGenericMethodDefinition && m.GetGenericArguments().Length == typeArgs.Length)
			{
				return m.MakeGenericMethod(typeArgs);
			}
			return null;
		}

		// Token: 0x06001351 RID: 4945 RVA: 0x00041FAA File Offset: 0x000401AA
		[__DynamicallyInvokable]
		public static MethodCallExpression ArrayIndex(Expression array, params Expression[] indexes)
		{
			return Expression.ArrayIndex(array, indexes);
		}

		// Token: 0x06001352 RID: 4946 RVA: 0x00041FB4 File Offset: 0x000401B4
		[__DynamicallyInvokable]
		public static MethodCallExpression ArrayIndex(Expression array, IEnumerable<Expression> indexes)
		{
			Expression.RequiresCanRead(array, "array");
			ContractUtils.RequiresNotNull(indexes, "indexes");
			Type type = array.Type;
			if (!type.IsArray)
			{
				throw Error.ArgumentMustBeArray();
			}
			ReadOnlyCollection<Expression> readOnlyCollection = indexes.ToReadOnly<Expression>();
			if (type.GetArrayRank() != readOnlyCollection.Count)
			{
				throw Error.IncorrectNumberOfIndexes();
			}
			foreach (Expression expression in readOnlyCollection)
			{
				Expression.RequiresCanRead(expression, "indexes");
				if (expression.Type != typeof(int))
				{
					throw Error.ArgumentMustBeArrayIndexType();
				}
			}
			MethodInfo method = array.Type.GetMethod("Get", BindingFlags.Instance | BindingFlags.Public);
			return Expression.Call(array, method, readOnlyCollection);
		}

		// Token: 0x06001353 RID: 4947 RVA: 0x00042084 File Offset: 0x00040284
		[__DynamicallyInvokable]
		public static NewArrayExpression NewArrayInit(Type type, params Expression[] initializers)
		{
			return Expression.NewArrayInit(type, initializers);
		}

		// Token: 0x06001354 RID: 4948 RVA: 0x00042090 File Offset: 0x00040290
		[__DynamicallyInvokable]
		public static NewArrayExpression NewArrayInit(Type type, IEnumerable<Expression> initializers)
		{
			ContractUtils.RequiresNotNull(type, "type");
			ContractUtils.RequiresNotNull(initializers, "initializers");
			if (type.Equals(typeof(void)))
			{
				throw Error.ArgumentCannotBeOfTypeVoid();
			}
			ReadOnlyCollection<Expression> readOnlyCollection = initializers.ToReadOnly<Expression>();
			Expression[] array = null;
			int i = 0;
			int count = readOnlyCollection.Count;
			while (i < count)
			{
				Expression expression = readOnlyCollection[i];
				Expression.RequiresCanRead(expression, "initializers");
				if (!TypeUtils.AreReferenceAssignable(type, expression.Type))
				{
					if (!Expression.TryQuote(type, ref expression))
					{
						throw Error.ExpressionTypeCannotInitializeArrayType(expression.Type, type);
					}
					if (array == null)
					{
						array = new Expression[readOnlyCollection.Count];
						for (int j = 0; j < i; j++)
						{
							array[j] = readOnlyCollection[j];
						}
					}
				}
				if (array != null)
				{
					array[i] = expression;
				}
				i++;
			}
			if (array != null)
			{
				readOnlyCollection = new TrueReadOnlyCollection<Expression>(array);
			}
			return NewArrayExpression.Make(ExpressionType.NewArrayInit, type.MakeArrayType(), readOnlyCollection);
		}

		// Token: 0x06001355 RID: 4949 RVA: 0x0004216E File Offset: 0x0004036E
		[__DynamicallyInvokable]
		public static NewArrayExpression NewArrayBounds(Type type, params Expression[] bounds)
		{
			return Expression.NewArrayBounds(type, bounds);
		}

		// Token: 0x06001356 RID: 4950 RVA: 0x00042178 File Offset: 0x00040378
		[__DynamicallyInvokable]
		public static NewArrayExpression NewArrayBounds(Type type, IEnumerable<Expression> bounds)
		{
			ContractUtils.RequiresNotNull(type, "type");
			ContractUtils.RequiresNotNull(bounds, "bounds");
			if (type.Equals(typeof(void)))
			{
				throw Error.ArgumentCannotBeOfTypeVoid();
			}
			ReadOnlyCollection<Expression> readOnlyCollection = bounds.ToReadOnly<Expression>();
			int count = readOnlyCollection.Count;
			if (count <= 0)
			{
				throw Error.BoundsCannotBeLessThanOne();
			}
			for (int i = 0; i < count; i++)
			{
				Expression expression = readOnlyCollection[i];
				Expression.RequiresCanRead(expression, "bounds");
				if (!TypeUtils.IsInteger(expression.Type))
				{
					throw Error.ArgumentMustBeInteger();
				}
			}
			Type type2;
			if (count == 1)
			{
				type2 = type.MakeArrayType();
			}
			else
			{
				type2 = type.MakeArrayType(count);
			}
			return NewArrayExpression.Make(ExpressionType.NewArrayBounds, type2, bounds.ToReadOnly<Expression>());
		}

		// Token: 0x06001357 RID: 4951 RVA: 0x00042223 File Offset: 0x00040423
		[__DynamicallyInvokable]
		public static NewExpression New(ConstructorInfo constructor)
		{
			return Expression.New(constructor, null);
		}

		// Token: 0x06001358 RID: 4952 RVA: 0x0004222C File Offset: 0x0004042C
		[__DynamicallyInvokable]
		public static NewExpression New(ConstructorInfo constructor, params Expression[] arguments)
		{
			return Expression.New(constructor, arguments);
		}

		// Token: 0x06001359 RID: 4953 RVA: 0x00042238 File Offset: 0x00040438
		[__DynamicallyInvokable]
		public static NewExpression New(ConstructorInfo constructor, IEnumerable<Expression> arguments)
		{
			ContractUtils.RequiresNotNull(constructor, "constructor");
			ContractUtils.RequiresNotNull(constructor.DeclaringType, "constructor.DeclaringType");
			TypeUtils.ValidateType(constructor.DeclaringType);
			ReadOnlyCollection<Expression> arguments2 = arguments.ToReadOnly<Expression>();
			Expression.ValidateArgumentTypes(constructor, ExpressionType.New, ref arguments2);
			return new NewExpression(constructor, arguments2, null);
		}

		// Token: 0x0600135A RID: 4954 RVA: 0x00042284 File Offset: 0x00040484
		[__DynamicallyInvokable]
		public static NewExpression New(ConstructorInfo constructor, IEnumerable<Expression> arguments, IEnumerable<MemberInfo> members)
		{
			ContractUtils.RequiresNotNull(constructor, "constructor");
			ReadOnlyCollection<MemberInfo> members2 = members.ToReadOnly<MemberInfo>();
			ReadOnlyCollection<Expression> arguments2 = arguments.ToReadOnly<Expression>();
			Expression.ValidateNewArgs(constructor, ref arguments2, ref members2);
			return new NewExpression(constructor, arguments2, members2);
		}

		// Token: 0x0600135B RID: 4955 RVA: 0x000422BC File Offset: 0x000404BC
		[__DynamicallyInvokable]
		public static NewExpression New(ConstructorInfo constructor, IEnumerable<Expression> arguments, params MemberInfo[] members)
		{
			return Expression.New(constructor, arguments, members);
		}

		// Token: 0x0600135C RID: 4956 RVA: 0x000422C8 File Offset: 0x000404C8
		[__DynamicallyInvokable]
		public static NewExpression New(Type type)
		{
			ContractUtils.RequiresNotNull(type, "type");
			if (type == typeof(void))
			{
				throw Error.ArgumentCannotBeOfTypeVoid();
			}
			if (type.IsValueType)
			{
				return new NewValueTypeExpression(type, EmptyReadOnlyCollection<Expression>.Instance, null);
			}
			ConstructorInfo constructor = type.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Type.EmptyTypes, null);
			if (constructor == null)
			{
				throw Error.TypeMissingDefaultConstructor(type);
			}
			return Expression.New(constructor);
		}

		// Token: 0x0600135D RID: 4957 RVA: 0x00042338 File Offset: 0x00040538
		private static void ValidateNewArgs(ConstructorInfo constructor, ref ReadOnlyCollection<Expression> arguments, ref ReadOnlyCollection<MemberInfo> members)
		{
			ParameterInfo[] parametersCached;
			if ((parametersCached = constructor.GetParametersCached()).Length != 0)
			{
				if (arguments.Count != parametersCached.Length)
				{
					throw Error.IncorrectNumberOfConstructorArguments();
				}
				if (arguments.Count != members.Count)
				{
					throw Error.IncorrectNumberOfArgumentsForMembers();
				}
				Expression[] array = null;
				MemberInfo[] array2 = null;
				int i = 0;
				int count = arguments.Count;
				while (i < count)
				{
					Expression expression = arguments[i];
					Expression.RequiresCanRead(expression, "argument");
					MemberInfo memberInfo = members[i];
					ContractUtils.RequiresNotNull(memberInfo, "member");
					if (!TypeUtils.AreEquivalent(memberInfo.DeclaringType, constructor.DeclaringType))
					{
						throw Error.ArgumentMemberNotDeclOnType(memberInfo.Name, constructor.DeclaringType.Name);
					}
					Type type;
					Expression.ValidateAnonymousTypeMember(ref memberInfo, out type);
					if (!TypeUtils.AreReferenceAssignable(type, expression.Type) && !Expression.TryQuote(type, ref expression))
					{
						throw Error.ArgumentTypeDoesNotMatchMember(expression.Type, type);
					}
					ParameterInfo parameterInfo = parametersCached[i];
					Type type2 = parameterInfo.ParameterType;
					if (type2.IsByRef)
					{
						type2 = type2.GetElementType();
					}
					if (!TypeUtils.AreReferenceAssignable(type2, expression.Type) && !Expression.TryQuote(type2, ref expression))
					{
						throw Error.ExpressionTypeDoesNotMatchConstructorParameter(expression.Type, type2);
					}
					if (array == null && expression != arguments[i])
					{
						array = new Expression[arguments.Count];
						for (int j = 0; j < i; j++)
						{
							array[j] = arguments[j];
						}
					}
					if (array != null)
					{
						array[i] = expression;
					}
					if (array2 == null && memberInfo != members[i])
					{
						array2 = new MemberInfo[members.Count];
						for (int k = 0; k < i; k++)
						{
							array2[k] = members[k];
						}
					}
					if (array2 != null)
					{
						array2[i] = memberInfo;
					}
					i++;
				}
				if (array != null)
				{
					arguments = new TrueReadOnlyCollection<Expression>(array);
				}
				if (array2 != null)
				{
					members = new TrueReadOnlyCollection<MemberInfo>(array2);
					return;
				}
			}
			else
			{
				if (arguments != null && arguments.Count > 0)
				{
					throw Error.IncorrectNumberOfConstructorArguments();
				}
				if (members != null && members.Count > 0)
				{
					throw Error.IncorrectNumberOfMembersForGivenConstructor();
				}
			}
		}

		// Token: 0x0600135E RID: 4958 RVA: 0x00042538 File Offset: 0x00040738
		private static void ValidateAnonymousTypeMember(ref MemberInfo member, out Type memberType)
		{
			MemberTypes memberType2 = member.MemberType;
			if (memberType2 != MemberTypes.Field)
			{
				if (memberType2 != MemberTypes.Method)
				{
					if (memberType2 != MemberTypes.Property)
					{
						throw Error.ArgumentMustBeFieldInfoOrPropertInfoOrMethod();
					}
					PropertyInfo propertyInfo = member as PropertyInfo;
					if (!propertyInfo.CanRead)
					{
						throw Error.PropertyDoesNotHaveGetter(propertyInfo);
					}
					if (propertyInfo.GetGetMethod().IsStatic)
					{
						throw Error.ArgumentMustBeInstanceMember();
					}
					memberType = propertyInfo.PropertyType;
					return;
				}
				else
				{
					MethodInfo methodInfo = member as MethodInfo;
					if (methodInfo.IsStatic)
					{
						throw Error.ArgumentMustBeInstanceMember();
					}
					PropertyInfo property = Expression.GetProperty(methodInfo);
					member = property;
					memberType = property.PropertyType;
					return;
				}
			}
			else
			{
				FieldInfo fieldInfo = member as FieldInfo;
				if (fieldInfo.IsStatic)
				{
					throw Error.ArgumentMustBeInstanceMember();
				}
				memberType = fieldInfo.FieldType;
				return;
			}
		}

		// Token: 0x0600135F RID: 4959 RVA: 0x000425E0 File Offset: 0x000407E0
		[__DynamicallyInvokable]
		public static ParameterExpression Parameter(Type type)
		{
			return Expression.Parameter(type, null);
		}

		// Token: 0x06001360 RID: 4960 RVA: 0x000425E9 File Offset: 0x000407E9
		[__DynamicallyInvokable]
		public static ParameterExpression Variable(Type type)
		{
			return Expression.Variable(type, null);
		}

		// Token: 0x06001361 RID: 4961 RVA: 0x000425F4 File Offset: 0x000407F4
		[__DynamicallyInvokable]
		public static ParameterExpression Parameter(Type type, string name)
		{
			ContractUtils.RequiresNotNull(type, "type");
			if (type == typeof(void))
			{
				throw Error.ArgumentCannotBeOfTypeVoid();
			}
			bool isByRef = type.IsByRef;
			if (isByRef)
			{
				type = type.GetElementType();
			}
			return ParameterExpression.Make(type, name, isByRef);
		}

		// Token: 0x06001362 RID: 4962 RVA: 0x0004263E File Offset: 0x0004083E
		[__DynamicallyInvokable]
		public static ParameterExpression Variable(Type type, string name)
		{
			ContractUtils.RequiresNotNull(type, "type");
			if (type == typeof(void))
			{
				throw Error.ArgumentCannotBeOfTypeVoid();
			}
			if (type.IsByRef)
			{
				throw Error.TypeMustNotBeByRef();
			}
			return ParameterExpression.Make(type, name, false);
		}

		// Token: 0x06001363 RID: 4963 RVA: 0x00042679 File Offset: 0x00040879
		[__DynamicallyInvokable]
		public static RuntimeVariablesExpression RuntimeVariables(params ParameterExpression[] variables)
		{
			return Expression.RuntimeVariables(variables);
		}

		// Token: 0x06001364 RID: 4964 RVA: 0x00042684 File Offset: 0x00040884
		[__DynamicallyInvokable]
		public static RuntimeVariablesExpression RuntimeVariables(IEnumerable<ParameterExpression> variables)
		{
			ContractUtils.RequiresNotNull(variables, "variables");
			ReadOnlyCollection<ParameterExpression> readOnlyCollection = variables.ToReadOnly<ParameterExpression>();
			for (int i = 0; i < readOnlyCollection.Count; i++)
			{
				if (readOnlyCollection[i] == null)
				{
					throw new ArgumentNullException("variables[" + i.ToString() + "]");
				}
			}
			return new RuntimeVariablesExpression(readOnlyCollection);
		}

		// Token: 0x06001365 RID: 4965 RVA: 0x000426E1 File Offset: 0x000408E1
		[__DynamicallyInvokable]
		public static SwitchCase SwitchCase(Expression body, params Expression[] testValues)
		{
			return Expression.SwitchCase(body, testValues);
		}

		// Token: 0x06001366 RID: 4966 RVA: 0x000426EC File Offset: 0x000408EC
		[__DynamicallyInvokable]
		public static SwitchCase SwitchCase(Expression body, IEnumerable<Expression> testValues)
		{
			Expression.RequiresCanRead(body, "body");
			ReadOnlyCollection<Expression> readOnlyCollection = testValues.ToReadOnly<Expression>();
			Expression.RequiresCanRead(readOnlyCollection, "testValues");
			ContractUtils.RequiresNotEmpty<Expression>(readOnlyCollection, "testValues");
			return new SwitchCase(body, readOnlyCollection);
		}

		// Token: 0x06001367 RID: 4967 RVA: 0x00042728 File Offset: 0x00040928
		[__DynamicallyInvokable]
		public static SwitchExpression Switch(Expression switchValue, params SwitchCase[] cases)
		{
			return Expression.Switch(switchValue, null, null, cases);
		}

		// Token: 0x06001368 RID: 4968 RVA: 0x00042733 File Offset: 0x00040933
		[__DynamicallyInvokable]
		public static SwitchExpression Switch(Expression switchValue, Expression defaultBody, params SwitchCase[] cases)
		{
			return Expression.Switch(switchValue, defaultBody, null, cases);
		}

		// Token: 0x06001369 RID: 4969 RVA: 0x0004273E File Offset: 0x0004093E
		[__DynamicallyInvokable]
		public static SwitchExpression Switch(Expression switchValue, Expression defaultBody, MethodInfo comparison, params SwitchCase[] cases)
		{
			return Expression.Switch(switchValue, defaultBody, comparison, cases);
		}

		// Token: 0x0600136A RID: 4970 RVA: 0x00042749 File Offset: 0x00040949
		[__DynamicallyInvokable]
		public static SwitchExpression Switch(Type type, Expression switchValue, Expression defaultBody, MethodInfo comparison, params SwitchCase[] cases)
		{
			return Expression.Switch(type, switchValue, defaultBody, comparison, cases);
		}

		// Token: 0x0600136B RID: 4971 RVA: 0x00042756 File Offset: 0x00040956
		[__DynamicallyInvokable]
		public static SwitchExpression Switch(Expression switchValue, Expression defaultBody, MethodInfo comparison, IEnumerable<SwitchCase> cases)
		{
			return Expression.Switch(null, switchValue, defaultBody, comparison, cases);
		}

		// Token: 0x0600136C RID: 4972 RVA: 0x00042764 File Offset: 0x00040964
		[__DynamicallyInvokable]
		public static SwitchExpression Switch(Type type, Expression switchValue, Expression defaultBody, MethodInfo comparison, IEnumerable<SwitchCase> cases)
		{
			Expression.RequiresCanRead(switchValue, "switchValue");
			if (switchValue.Type == typeof(void))
			{
				throw Error.ArgumentCannotBeOfTypeVoid();
			}
			ReadOnlyCollection<SwitchCase> readOnlyCollection = cases.ToReadOnly<SwitchCase>();
			ContractUtils.RequiresNotEmpty<SwitchCase>(readOnlyCollection, "cases");
			ContractUtils.RequiresNotNullItems<SwitchCase>(readOnlyCollection, "cases");
			Type type2 = type ?? readOnlyCollection[0].Body.Type;
			bool customType = type != null;
			if (comparison != null)
			{
				ParameterInfo[] parametersCached = comparison.GetParametersCached();
				if (parametersCached.Length != 2)
				{
					throw Error.IncorrectNumberOfMethodCallArguments(comparison);
				}
				ParameterInfo parameterInfo = parametersCached[0];
				bool flag = false;
				if (!Expression.ParameterIsAssignable(parameterInfo, switchValue.Type))
				{
					flag = Expression.ParameterIsAssignable(parameterInfo, switchValue.Type.GetNonNullableType());
					if (!flag)
					{
						throw Error.SwitchValueTypeDoesNotMatchComparisonMethodParameter(switchValue.Type, parameterInfo.ParameterType);
					}
				}
				ParameterInfo parameterInfo2 = parametersCached[1];
				using (IEnumerator<SwitchCase> enumerator = readOnlyCollection.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						SwitchCase switchCase = enumerator.Current;
						ContractUtils.RequiresNotNull(switchCase, "cases");
						Expression.ValidateSwitchCaseType(switchCase.Body, customType, type2, "cases");
						for (int i = 0; i < switchCase.TestValues.Count; i++)
						{
							Type type3 = switchCase.TestValues[i].Type;
							if (flag)
							{
								if (!type3.IsNullableType())
								{
									throw Error.TestValueTypeDoesNotMatchComparisonMethodParameter(type3, parameterInfo2.ParameterType);
								}
								type3 = type3.GetNonNullableType();
							}
							if (!Expression.ParameterIsAssignable(parameterInfo2, type3))
							{
								throw Error.TestValueTypeDoesNotMatchComparisonMethodParameter(type3, parameterInfo2.ParameterType);
							}
						}
					}
					goto IL_24B;
				}
			}
			Expression expression = readOnlyCollection[0].TestValues[0];
			foreach (SwitchCase switchCase2 in readOnlyCollection)
			{
				ContractUtils.RequiresNotNull(switchCase2, "cases");
				Expression.ValidateSwitchCaseType(switchCase2.Body, customType, type2, "cases");
				for (int j = 0; j < switchCase2.TestValues.Count; j++)
				{
					if (!TypeUtils.AreEquivalent(expression.Type, switchCase2.TestValues[j].Type))
					{
						throw new ArgumentException(Strings.AllTestValuesMustHaveSameType, "cases");
					}
				}
			}
			BinaryExpression binaryExpression = Expression.Equal(switchValue, expression, false, comparison);
			comparison = binaryExpression.Method;
			IL_24B:
			if (defaultBody == null)
			{
				if (type2 != typeof(void))
				{
					throw Error.DefaultBodyMustBeSupplied();
				}
			}
			else
			{
				Expression.ValidateSwitchCaseType(defaultBody, customType, type2, "defaultBody");
			}
			if (comparison != null && comparison.ReturnType != typeof(bool))
			{
				throw Error.EqualityMustReturnBoolean(comparison);
			}
			return new SwitchExpression(type2, switchValue, defaultBody, comparison, readOnlyCollection);
		}

		// Token: 0x0600136D RID: 4973 RVA: 0x00042A34 File Offset: 0x00040C34
		private static void ValidateSwitchCaseType(Expression @case, bool customType, Type resultType, string parameterName)
		{
			if (customType)
			{
				if (resultType != typeof(void) && !TypeUtils.AreReferenceAssignable(resultType, @case.Type))
				{
					throw new ArgumentException(Strings.ArgumentTypesMustMatch, parameterName);
				}
			}
			else if (!TypeUtils.AreEquivalent(resultType, @case.Type))
			{
				throw new ArgumentException(Strings.AllCaseBodiesMustHaveSameType, parameterName);
			}
		}

		// Token: 0x0600136E RID: 4974 RVA: 0x00042A8A File Offset: 0x00040C8A
		[__DynamicallyInvokable]
		public static SymbolDocumentInfo SymbolDocument(string fileName)
		{
			return new SymbolDocumentInfo(fileName);
		}

		// Token: 0x0600136F RID: 4975 RVA: 0x00042A92 File Offset: 0x00040C92
		[__DynamicallyInvokable]
		public static SymbolDocumentInfo SymbolDocument(string fileName, Guid language)
		{
			return new SymbolDocumentWithGuids(fileName, ref language);
		}

		// Token: 0x06001370 RID: 4976 RVA: 0x00042A9C File Offset: 0x00040C9C
		[__DynamicallyInvokable]
		public static SymbolDocumentInfo SymbolDocument(string fileName, Guid language, Guid languageVendor)
		{
			return new SymbolDocumentWithGuids(fileName, ref language, ref languageVendor);
		}

		// Token: 0x06001371 RID: 4977 RVA: 0x00042AA8 File Offset: 0x00040CA8
		[__DynamicallyInvokable]
		public static SymbolDocumentInfo SymbolDocument(string fileName, Guid language, Guid languageVendor, Guid documentType)
		{
			return new SymbolDocumentWithGuids(fileName, ref language, ref languageVendor, ref documentType);
		}

		// Token: 0x06001372 RID: 4978 RVA: 0x00042AB6 File Offset: 0x00040CB6
		[__DynamicallyInvokable]
		public static TryExpression TryFault(Expression body, Expression fault)
		{
			return Expression.MakeTry(null, body, null, fault, null);
		}

		// Token: 0x06001373 RID: 4979 RVA: 0x00042AC2 File Offset: 0x00040CC2
		[__DynamicallyInvokable]
		public static TryExpression TryFinally(Expression body, Expression @finally)
		{
			return Expression.MakeTry(null, body, @finally, null, null);
		}

		// Token: 0x06001374 RID: 4980 RVA: 0x00042ACE File Offset: 0x00040CCE
		[__DynamicallyInvokable]
		public static TryExpression TryCatch(Expression body, params CatchBlock[] handlers)
		{
			return Expression.MakeTry(null, body, null, null, handlers);
		}

		// Token: 0x06001375 RID: 4981 RVA: 0x00042ADA File Offset: 0x00040CDA
		[__DynamicallyInvokable]
		public static TryExpression TryCatchFinally(Expression body, Expression @finally, params CatchBlock[] handlers)
		{
			return Expression.MakeTry(null, body, @finally, null, handlers);
		}

		// Token: 0x06001376 RID: 4982 RVA: 0x00042AE8 File Offset: 0x00040CE8
		[__DynamicallyInvokable]
		public static TryExpression MakeTry(Type type, Expression body, Expression @finally, Expression fault, IEnumerable<CatchBlock> handlers)
		{
			Expression.RequiresCanRead(body, "body");
			ReadOnlyCollection<CatchBlock> readOnlyCollection = handlers.ToReadOnly<CatchBlock>();
			ContractUtils.RequiresNotNullItems<CatchBlock>(readOnlyCollection, "handlers");
			Expression.ValidateTryAndCatchHaveSameType(type, body, readOnlyCollection);
			if (fault != null)
			{
				if (@finally != null || readOnlyCollection.Count > 0)
				{
					throw Error.FaultCannotHaveCatchOrFinally();
				}
				Expression.RequiresCanRead(fault, "fault");
			}
			else if (@finally != null)
			{
				Expression.RequiresCanRead(@finally, "finally");
			}
			else if (readOnlyCollection.Count == 0)
			{
				throw Error.TryMustHaveCatchFinallyOrFault();
			}
			return new TryExpression(type ?? body.Type, body, @finally, fault, readOnlyCollection);
		}

		// Token: 0x06001377 RID: 4983 RVA: 0x00042B70 File Offset: 0x00040D70
		private static void ValidateTryAndCatchHaveSameType(Type type, Expression tryBody, ReadOnlyCollection<CatchBlock> handlers)
		{
			if (type != null)
			{
				if (!(type != typeof(void)))
				{
					return;
				}
				if (!TypeUtils.AreReferenceAssignable(type, tryBody.Type))
				{
					throw Error.ArgumentTypesMustMatch();
				}
				using (IEnumerator<CatchBlock> enumerator = handlers.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						CatchBlock catchBlock = enumerator.Current;
						if (!TypeUtils.AreReferenceAssignable(type, catchBlock.Body.Type))
						{
							throw Error.ArgumentTypesMustMatch();
						}
					}
					return;
				}
			}
			if (tryBody == null || tryBody.Type == typeof(void))
			{
				using (IEnumerator<CatchBlock> enumerator2 = handlers.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						CatchBlock catchBlock2 = enumerator2.Current;
						if (catchBlock2.Body != null && catchBlock2.Body.Type != typeof(void))
						{
							throw Error.BodyOfCatchMustHaveSameTypeAsBodyOfTry();
						}
					}
					return;
				}
			}
			type = tryBody.Type;
			foreach (CatchBlock catchBlock3 in handlers)
			{
				if (catchBlock3.Body == null || !TypeUtils.AreEquivalent(catchBlock3.Body.Type, type))
				{
					throw Error.BodyOfCatchMustHaveSameTypeAsBodyOfTry();
				}
			}
		}

		// Token: 0x06001378 RID: 4984 RVA: 0x00042CD4 File Offset: 0x00040ED4
		[__DynamicallyInvokable]
		public static TypeBinaryExpression TypeIs(Expression expression, Type type)
		{
			Expression.RequiresCanRead(expression, "expression");
			ContractUtils.RequiresNotNull(type, "type");
			if (type.IsByRef)
			{
				throw Error.TypeMustNotBeByRef();
			}
			return new TypeBinaryExpression(expression, type, ExpressionType.TypeIs);
		}

		// Token: 0x06001379 RID: 4985 RVA: 0x00042D03 File Offset: 0x00040F03
		[__DynamicallyInvokable]
		public static TypeBinaryExpression TypeEqual(Expression expression, Type type)
		{
			Expression.RequiresCanRead(expression, "expression");
			ContractUtils.RequiresNotNull(type, "type");
			if (type.IsByRef)
			{
				throw Error.TypeMustNotBeByRef();
			}
			return new TypeBinaryExpression(expression, type, ExpressionType.TypeEqual);
		}

		// Token: 0x0600137A RID: 4986 RVA: 0x00042D32 File Offset: 0x00040F32
		[__DynamicallyInvokable]
		public static UnaryExpression MakeUnary(ExpressionType unaryType, Expression operand, Type type)
		{
			return Expression.MakeUnary(unaryType, operand, type, null);
		}

		// Token: 0x0600137B RID: 4987 RVA: 0x00042D40 File Offset: 0x00040F40
		[__DynamicallyInvokable]
		public static UnaryExpression MakeUnary(ExpressionType unaryType, Expression operand, Type type, MethodInfo method)
		{
			if (unaryType <= ExpressionType.Quote)
			{
				if (unaryType <= ExpressionType.Convert)
				{
					if (unaryType == ExpressionType.ArrayLength)
					{
						return Expression.ArrayLength(operand);
					}
					if (unaryType == ExpressionType.Convert)
					{
						return Expression.Convert(operand, type, method);
					}
				}
				else
				{
					if (unaryType == ExpressionType.ConvertChecked)
					{
						return Expression.ConvertChecked(operand, type, method);
					}
					switch (unaryType)
					{
					case ExpressionType.Negate:
						return Expression.Negate(operand, method);
					case ExpressionType.UnaryPlus:
						return Expression.UnaryPlus(operand, method);
					case ExpressionType.NegateChecked:
						return Expression.NegateChecked(operand, method);
					case ExpressionType.New:
					case ExpressionType.NewArrayInit:
					case ExpressionType.NewArrayBounds:
						break;
					case ExpressionType.Not:
						return Expression.Not(operand, method);
					default:
						if (unaryType == ExpressionType.Quote)
						{
							return Expression.Quote(operand);
						}
						break;
					}
				}
			}
			else if (unaryType <= ExpressionType.Increment)
			{
				if (unaryType == ExpressionType.TypeAs)
				{
					return Expression.TypeAs(operand, type);
				}
				if (unaryType == ExpressionType.Decrement)
				{
					return Expression.Decrement(operand, method);
				}
				if (unaryType == ExpressionType.Increment)
				{
					return Expression.Increment(operand, method);
				}
			}
			else
			{
				if (unaryType == ExpressionType.Throw)
				{
					return Expression.Throw(operand, type);
				}
				if (unaryType == ExpressionType.Unbox)
				{
					return Expression.Unbox(operand, type);
				}
				switch (unaryType)
				{
				case ExpressionType.PreIncrementAssign:
					return Expression.PreIncrementAssign(operand, method);
				case ExpressionType.PreDecrementAssign:
					return Expression.PreDecrementAssign(operand, method);
				case ExpressionType.PostIncrementAssign:
					return Expression.PostIncrementAssign(operand, method);
				case ExpressionType.PostDecrementAssign:
					return Expression.PostDecrementAssign(operand, method);
				case ExpressionType.OnesComplement:
					return Expression.OnesComplement(operand, method);
				case ExpressionType.IsTrue:
					return Expression.IsTrue(operand, method);
				case ExpressionType.IsFalse:
					return Expression.IsFalse(operand, method);
				}
			}
			throw Error.UnhandledUnary(unaryType);
		}

		// Token: 0x0600137C RID: 4988 RVA: 0x00042EB0 File Offset: 0x000410B0
		private static UnaryExpression GetUserDefinedUnaryOperatorOrThrow(ExpressionType unaryType, string name, Expression operand)
		{
			UnaryExpression userDefinedUnaryOperator = Expression.GetUserDefinedUnaryOperator(unaryType, name, operand);
			if (userDefinedUnaryOperator != null)
			{
				Expression.ValidateParamswithOperandsOrThrow(userDefinedUnaryOperator.Method.GetParametersCached()[0].ParameterType, operand.Type, unaryType, name);
				return userDefinedUnaryOperator;
			}
			throw Error.UnaryOperatorNotDefined(unaryType, operand.Type);
		}

		// Token: 0x0600137D RID: 4989 RVA: 0x00042EFC File Offset: 0x000410FC
		private static UnaryExpression GetUserDefinedUnaryOperator(ExpressionType unaryType, string name, Expression operand)
		{
			Type type = operand.Type;
			Type[] array = new Type[]
			{
				type
			};
			Type nonNullableType = type.GetNonNullableType();
			BindingFlags bindingAttr = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
			MethodInfo methodValidated = nonNullableType.GetMethodValidated(name, bindingAttr, null, array, null);
			if (methodValidated != null)
			{
				return new UnaryExpression(unaryType, operand, methodValidated.ReturnType, methodValidated);
			}
			if (type.IsNullableType())
			{
				array[0] = nonNullableType;
				methodValidated = nonNullableType.GetMethodValidated(name, bindingAttr, null, array, null);
				if (methodValidated != null && methodValidated.ReturnType.IsValueType && !methodValidated.ReturnType.IsNullableType())
				{
					return new UnaryExpression(unaryType, operand, TypeUtils.GetNullableType(methodValidated.ReturnType), methodValidated);
				}
			}
			return null;
		}

		// Token: 0x0600137E RID: 4990 RVA: 0x00042FA4 File Offset: 0x000411A4
		private static UnaryExpression GetMethodBasedUnaryOperator(ExpressionType unaryType, Expression operand, MethodInfo method)
		{
			Expression.ValidateOperator(method);
			ParameterInfo[] parametersCached = method.GetParametersCached();
			if (parametersCached.Length != 1)
			{
				throw Error.IncorrectNumberOfMethodCallArguments(method);
			}
			if (Expression.ParameterIsAssignable(parametersCached[0], operand.Type))
			{
				Expression.ValidateParamswithOperandsOrThrow(parametersCached[0].ParameterType, operand.Type, unaryType, method.Name);
				return new UnaryExpression(unaryType, operand, method.ReturnType, method);
			}
			if (operand.Type.IsNullableType() && Expression.ParameterIsAssignable(parametersCached[0], operand.Type.GetNonNullableType()) && method.ReturnType.IsValueType && !method.ReturnType.IsNullableType())
			{
				return new UnaryExpression(unaryType, operand, TypeUtils.GetNullableType(method.ReturnType), method);
			}
			throw Error.OperandTypesDoNotMatchParameters(unaryType, method.Name);
		}

		// Token: 0x0600137F RID: 4991 RVA: 0x00043068 File Offset: 0x00041268
		private static UnaryExpression GetUserDefinedCoercionOrThrow(ExpressionType coercionType, Expression expression, Type convertToType)
		{
			UnaryExpression userDefinedCoercion = Expression.GetUserDefinedCoercion(coercionType, expression, convertToType);
			if (userDefinedCoercion != null)
			{
				return userDefinedCoercion;
			}
			throw Error.CoercionOperatorNotDefined(expression.Type, convertToType);
		}

		// Token: 0x06001380 RID: 4992 RVA: 0x00043090 File Offset: 0x00041290
		private static UnaryExpression GetUserDefinedCoercion(ExpressionType coercionType, Expression expression, Type convertToType)
		{
			MethodInfo userDefinedCoercionMethod = TypeUtils.GetUserDefinedCoercionMethod(expression.Type, convertToType, false);
			if (userDefinedCoercionMethod != null)
			{
				return new UnaryExpression(coercionType, expression, convertToType, userDefinedCoercionMethod);
			}
			return null;
		}

		// Token: 0x06001381 RID: 4993 RVA: 0x000430C0 File Offset: 0x000412C0
		private static UnaryExpression GetMethodBasedCoercionOperator(ExpressionType unaryType, Expression operand, Type convertToType, MethodInfo method)
		{
			Expression.ValidateOperator(method);
			ParameterInfo[] parametersCached = method.GetParametersCached();
			if (parametersCached.Length != 1)
			{
				throw Error.IncorrectNumberOfMethodCallArguments(method);
			}
			if (Expression.ParameterIsAssignable(parametersCached[0], operand.Type) && TypeUtils.AreEquivalent(method.ReturnType, convertToType))
			{
				return new UnaryExpression(unaryType, operand, method.ReturnType, method);
			}
			if ((operand.Type.IsNullableType() || convertToType.IsNullableType()) && Expression.ParameterIsAssignable(parametersCached[0], operand.Type.GetNonNullableType()) && TypeUtils.AreEquivalent(method.ReturnType, convertToType.GetNonNullableType()))
			{
				return new UnaryExpression(unaryType, operand, convertToType, method);
			}
			throw Error.OperandTypesDoNotMatchParameters(unaryType, method.Name);
		}

		// Token: 0x06001382 RID: 4994 RVA: 0x0004316C File Offset: 0x0004136C
		[__DynamicallyInvokable]
		public static UnaryExpression Negate(Expression expression)
		{
			return Expression.Negate(expression, null);
		}

		// Token: 0x06001383 RID: 4995 RVA: 0x00043178 File Offset: 0x00041378
		[__DynamicallyInvokable]
		public static UnaryExpression Negate(Expression expression, MethodInfo method)
		{
			Expression.RequiresCanRead(expression, "expression");
			if (!(method == null))
			{
				return Expression.GetMethodBasedUnaryOperator(ExpressionType.Negate, expression, method);
			}
			if (TypeUtils.IsArithmetic(expression.Type) && !TypeUtils.IsUnsignedInt(expression.Type))
			{
				return new UnaryExpression(ExpressionType.Negate, expression, expression.Type, null);
			}
			return Expression.GetUserDefinedUnaryOperatorOrThrow(ExpressionType.Negate, "op_UnaryNegation", expression);
		}

		// Token: 0x06001384 RID: 4996 RVA: 0x000431DA File Offset: 0x000413DA
		[__DynamicallyInvokable]
		public static UnaryExpression UnaryPlus(Expression expression)
		{
			return Expression.UnaryPlus(expression, null);
		}

		// Token: 0x06001385 RID: 4997 RVA: 0x000431E4 File Offset: 0x000413E4
		[__DynamicallyInvokable]
		public static UnaryExpression UnaryPlus(Expression expression, MethodInfo method)
		{
			Expression.RequiresCanRead(expression, "expression");
			if (!(method == null))
			{
				return Expression.GetMethodBasedUnaryOperator(ExpressionType.UnaryPlus, expression, method);
			}
			if (TypeUtils.IsArithmetic(expression.Type))
			{
				return new UnaryExpression(ExpressionType.UnaryPlus, expression, expression.Type, null);
			}
			return Expression.GetUserDefinedUnaryOperatorOrThrow(ExpressionType.UnaryPlus, "op_UnaryPlus", expression);
		}

		// Token: 0x06001386 RID: 4998 RVA: 0x00043239 File Offset: 0x00041439
		[__DynamicallyInvokable]
		public static UnaryExpression NegateChecked(Expression expression)
		{
			return Expression.NegateChecked(expression, null);
		}

		// Token: 0x06001387 RID: 4999 RVA: 0x00043244 File Offset: 0x00041444
		[__DynamicallyInvokable]
		public static UnaryExpression NegateChecked(Expression expression, MethodInfo method)
		{
			Expression.RequiresCanRead(expression, "expression");
			if (!(method == null))
			{
				return Expression.GetMethodBasedUnaryOperator(ExpressionType.NegateChecked, expression, method);
			}
			if (TypeUtils.IsArithmetic(expression.Type) && !TypeUtils.IsUnsignedInt(expression.Type))
			{
				return new UnaryExpression(ExpressionType.NegateChecked, expression, expression.Type, null);
			}
			return Expression.GetUserDefinedUnaryOperatorOrThrow(ExpressionType.NegateChecked, "op_UnaryNegation", expression);
		}

		// Token: 0x06001388 RID: 5000 RVA: 0x000432A6 File Offset: 0x000414A6
		[__DynamicallyInvokable]
		public static UnaryExpression Not(Expression expression)
		{
			return Expression.Not(expression, null);
		}

		// Token: 0x06001389 RID: 5001 RVA: 0x000432B0 File Offset: 0x000414B0
		[__DynamicallyInvokable]
		public static UnaryExpression Not(Expression expression, MethodInfo method)
		{
			Expression.RequiresCanRead(expression, "expression");
			if (!(method == null))
			{
				return Expression.GetMethodBasedUnaryOperator(ExpressionType.Not, expression, method);
			}
			if (TypeUtils.IsIntegerOrBool(expression.Type))
			{
				return new UnaryExpression(ExpressionType.Not, expression, expression.Type, null);
			}
			UnaryExpression userDefinedUnaryOperator = Expression.GetUserDefinedUnaryOperator(ExpressionType.Not, "op_LogicalNot", expression);
			if (userDefinedUnaryOperator != null)
			{
				return userDefinedUnaryOperator;
			}
			return Expression.GetUserDefinedUnaryOperatorOrThrow(ExpressionType.Not, "op_OnesComplement", expression);
		}

		// Token: 0x0600138A RID: 5002 RVA: 0x00043318 File Offset: 0x00041518
		[__DynamicallyInvokable]
		public static UnaryExpression IsFalse(Expression expression)
		{
			return Expression.IsFalse(expression, null);
		}

		// Token: 0x0600138B RID: 5003 RVA: 0x00043324 File Offset: 0x00041524
		[__DynamicallyInvokable]
		public static UnaryExpression IsFalse(Expression expression, MethodInfo method)
		{
			Expression.RequiresCanRead(expression, "expression");
			if (!(method == null))
			{
				return Expression.GetMethodBasedUnaryOperator(ExpressionType.IsFalse, expression, method);
			}
			if (TypeUtils.IsBool(expression.Type))
			{
				return new UnaryExpression(ExpressionType.IsFalse, expression, expression.Type, null);
			}
			return Expression.GetUserDefinedUnaryOperatorOrThrow(ExpressionType.IsFalse, "op_False", expression);
		}

		// Token: 0x0600138C RID: 5004 RVA: 0x00043379 File Offset: 0x00041579
		[__DynamicallyInvokable]
		public static UnaryExpression IsTrue(Expression expression)
		{
			return Expression.IsTrue(expression, null);
		}

		// Token: 0x0600138D RID: 5005 RVA: 0x00043384 File Offset: 0x00041584
		[__DynamicallyInvokable]
		public static UnaryExpression IsTrue(Expression expression, MethodInfo method)
		{
			Expression.RequiresCanRead(expression, "expression");
			if (!(method == null))
			{
				return Expression.GetMethodBasedUnaryOperator(ExpressionType.IsTrue, expression, method);
			}
			if (TypeUtils.IsBool(expression.Type))
			{
				return new UnaryExpression(ExpressionType.IsTrue, expression, expression.Type, null);
			}
			return Expression.GetUserDefinedUnaryOperatorOrThrow(ExpressionType.IsTrue, "op_True", expression);
		}

		// Token: 0x0600138E RID: 5006 RVA: 0x000433D9 File Offset: 0x000415D9
		[__DynamicallyInvokable]
		public static UnaryExpression OnesComplement(Expression expression)
		{
			return Expression.OnesComplement(expression, null);
		}

		// Token: 0x0600138F RID: 5007 RVA: 0x000433E4 File Offset: 0x000415E4
		[__DynamicallyInvokable]
		public static UnaryExpression OnesComplement(Expression expression, MethodInfo method)
		{
			Expression.RequiresCanRead(expression, "expression");
			if (!(method == null))
			{
				return Expression.GetMethodBasedUnaryOperator(ExpressionType.OnesComplement, expression, method);
			}
			if (TypeUtils.IsInteger(expression.Type))
			{
				return new UnaryExpression(ExpressionType.OnesComplement, expression, expression.Type, null);
			}
			return Expression.GetUserDefinedUnaryOperatorOrThrow(ExpressionType.OnesComplement, "op_OnesComplement", expression);
		}

		// Token: 0x06001390 RID: 5008 RVA: 0x00043439 File Offset: 0x00041639
		[__DynamicallyInvokable]
		public static UnaryExpression TypeAs(Expression expression, Type type)
		{
			Expression.RequiresCanRead(expression, "expression");
			ContractUtils.RequiresNotNull(type, "type");
			TypeUtils.ValidateType(type);
			if (type.IsValueType && !type.IsNullableType())
			{
				throw Error.IncorrectTypeForTypeAs(type);
			}
			return new UnaryExpression(ExpressionType.TypeAs, expression, type, null);
		}

		// Token: 0x06001391 RID: 5009 RVA: 0x00043478 File Offset: 0x00041678
		[__DynamicallyInvokable]
		public static UnaryExpression Unbox(Expression expression, Type type)
		{
			Expression.RequiresCanRead(expression, "expression");
			ContractUtils.RequiresNotNull(type, "type");
			if (!expression.Type.IsInterface && expression.Type != typeof(object))
			{
				throw Error.InvalidUnboxType();
			}
			if (!type.IsValueType)
			{
				throw Error.InvalidUnboxType();
			}
			TypeUtils.ValidateType(type);
			return new UnaryExpression(ExpressionType.Unbox, expression, type, null);
		}

		// Token: 0x06001392 RID: 5010 RVA: 0x000434E3 File Offset: 0x000416E3
		[__DynamicallyInvokable]
		public static UnaryExpression Convert(Expression expression, Type type)
		{
			return Expression.Convert(expression, type, null);
		}

		// Token: 0x06001393 RID: 5011 RVA: 0x000434F0 File Offset: 0x000416F0
		[__DynamicallyInvokable]
		public static UnaryExpression Convert(Expression expression, Type type, MethodInfo method)
		{
			Expression.RequiresCanRead(expression, "expression");
			ContractUtils.RequiresNotNull(type, "type");
			TypeUtils.ValidateType(type);
			if (!(method == null))
			{
				return Expression.GetMethodBasedCoercionOperator(ExpressionType.Convert, expression, type, method);
			}
			if (TypeUtils.HasIdentityPrimitiveOrNullableConversion(expression.Type, type) || TypeUtils.HasReferenceConversion(expression.Type, type))
			{
				return new UnaryExpression(ExpressionType.Convert, expression, type, null);
			}
			return Expression.GetUserDefinedCoercionOrThrow(ExpressionType.Convert, expression, type);
		}

		// Token: 0x06001394 RID: 5012 RVA: 0x0004355D File Offset: 0x0004175D
		[__DynamicallyInvokable]
		public static UnaryExpression ConvertChecked(Expression expression, Type type)
		{
			return Expression.ConvertChecked(expression, type, null);
		}

		// Token: 0x06001395 RID: 5013 RVA: 0x00043568 File Offset: 0x00041768
		[__DynamicallyInvokable]
		public static UnaryExpression ConvertChecked(Expression expression, Type type, MethodInfo method)
		{
			Expression.RequiresCanRead(expression, "expression");
			ContractUtils.RequiresNotNull(type, "type");
			TypeUtils.ValidateType(type);
			if (!(method == null))
			{
				return Expression.GetMethodBasedCoercionOperator(ExpressionType.ConvertChecked, expression, type, method);
			}
			if (TypeUtils.HasIdentityPrimitiveOrNullableConversion(expression.Type, type))
			{
				return new UnaryExpression(ExpressionType.ConvertChecked, expression, type, null);
			}
			if (TypeUtils.HasReferenceConversion(expression.Type, type))
			{
				return new UnaryExpression(ExpressionType.Convert, expression, type, null);
			}
			return Expression.GetUserDefinedCoercionOrThrow(ExpressionType.ConvertChecked, expression, type);
		}

		// Token: 0x06001396 RID: 5014 RVA: 0x000435E0 File Offset: 0x000417E0
		[__DynamicallyInvokable]
		public static UnaryExpression ArrayLength(Expression array)
		{
			ContractUtils.RequiresNotNull(array, "array");
			if (!array.Type.IsArray || !typeof(Array).IsAssignableFrom(array.Type))
			{
				throw Error.ArgumentMustBeArray();
			}
			if (array.Type.GetArrayRank() != 1)
			{
				throw Error.ArgumentMustBeSingleDimensionalArrayType();
			}
			return new UnaryExpression(ExpressionType.ArrayLength, array, typeof(int), null);
		}

		// Token: 0x06001397 RID: 5015 RVA: 0x00043648 File Offset: 0x00041848
		[__DynamicallyInvokable]
		public static UnaryExpression Quote(Expression expression)
		{
			Expression.RequiresCanRead(expression, "expression");
			if (!(expression is LambdaExpression))
			{
				throw Error.QuotedExpressionMustBeLambda();
			}
			return new UnaryExpression(ExpressionType.Quote, expression, expression.GetType(), null);
		}

		// Token: 0x06001398 RID: 5016 RVA: 0x00043682 File Offset: 0x00041882
		[__DynamicallyInvokable]
		public static UnaryExpression Rethrow()
		{
			return Expression.Throw(null);
		}

		// Token: 0x06001399 RID: 5017 RVA: 0x0004368A File Offset: 0x0004188A
		[__DynamicallyInvokable]
		public static UnaryExpression Rethrow(Type type)
		{
			return Expression.Throw(null, type);
		}

		// Token: 0x0600139A RID: 5018 RVA: 0x00043693 File Offset: 0x00041893
		[__DynamicallyInvokable]
		public static UnaryExpression Throw(Expression value)
		{
			return Expression.Throw(value, typeof(void));
		}

		// Token: 0x0600139B RID: 5019 RVA: 0x000436A5 File Offset: 0x000418A5
		[__DynamicallyInvokable]
		public static UnaryExpression Throw(Expression value, Type type)
		{
			ContractUtils.RequiresNotNull(type, "type");
			TypeUtils.ValidateType(type);
			if (value != null)
			{
				Expression.RequiresCanRead(value, "value");
				if (value.Type.IsValueType)
				{
					throw Error.ArgumentMustNotHaveValueType();
				}
			}
			return new UnaryExpression(ExpressionType.Throw, value, type, null);
		}

		// Token: 0x0600139C RID: 5020 RVA: 0x000436E3 File Offset: 0x000418E3
		[__DynamicallyInvokable]
		public static UnaryExpression Increment(Expression expression)
		{
			return Expression.Increment(expression, null);
		}

		// Token: 0x0600139D RID: 5021 RVA: 0x000436EC File Offset: 0x000418EC
		[__DynamicallyInvokable]
		public static UnaryExpression Increment(Expression expression, MethodInfo method)
		{
			Expression.RequiresCanRead(expression, "expression");
			if (!(method == null))
			{
				return Expression.GetMethodBasedUnaryOperator(ExpressionType.Increment, expression, method);
			}
			if (TypeUtils.IsArithmetic(expression.Type))
			{
				return new UnaryExpression(ExpressionType.Increment, expression, expression.Type, null);
			}
			return Expression.GetUserDefinedUnaryOperatorOrThrow(ExpressionType.Increment, "op_Increment", expression);
		}

		// Token: 0x0600139E RID: 5022 RVA: 0x00043741 File Offset: 0x00041941
		[__DynamicallyInvokable]
		public static UnaryExpression Decrement(Expression expression)
		{
			return Expression.Decrement(expression, null);
		}

		// Token: 0x0600139F RID: 5023 RVA: 0x0004374C File Offset: 0x0004194C
		[__DynamicallyInvokable]
		public static UnaryExpression Decrement(Expression expression, MethodInfo method)
		{
			Expression.RequiresCanRead(expression, "expression");
			if (!(method == null))
			{
				return Expression.GetMethodBasedUnaryOperator(ExpressionType.Decrement, expression, method);
			}
			if (TypeUtils.IsArithmetic(expression.Type))
			{
				return new UnaryExpression(ExpressionType.Decrement, expression, expression.Type, null);
			}
			return Expression.GetUserDefinedUnaryOperatorOrThrow(ExpressionType.Decrement, "op_Decrement", expression);
		}

		// Token: 0x060013A0 RID: 5024 RVA: 0x000437A1 File Offset: 0x000419A1
		[__DynamicallyInvokable]
		public static UnaryExpression PreIncrementAssign(Expression expression)
		{
			return Expression.MakeOpAssignUnary(ExpressionType.PreIncrementAssign, expression, null);
		}

		// Token: 0x060013A1 RID: 5025 RVA: 0x000437AC File Offset: 0x000419AC
		[__DynamicallyInvokable]
		public static UnaryExpression PreIncrementAssign(Expression expression, MethodInfo method)
		{
			return Expression.MakeOpAssignUnary(ExpressionType.PreIncrementAssign, expression, method);
		}

		// Token: 0x060013A2 RID: 5026 RVA: 0x000437B7 File Offset: 0x000419B7
		[__DynamicallyInvokable]
		public static UnaryExpression PreDecrementAssign(Expression expression)
		{
			return Expression.MakeOpAssignUnary(ExpressionType.PreDecrementAssign, expression, null);
		}

		// Token: 0x060013A3 RID: 5027 RVA: 0x000437C2 File Offset: 0x000419C2
		[__DynamicallyInvokable]
		public static UnaryExpression PreDecrementAssign(Expression expression, MethodInfo method)
		{
			return Expression.MakeOpAssignUnary(ExpressionType.PreDecrementAssign, expression, method);
		}

		// Token: 0x060013A4 RID: 5028 RVA: 0x000437CD File Offset: 0x000419CD
		[__DynamicallyInvokable]
		public static UnaryExpression PostIncrementAssign(Expression expression)
		{
			return Expression.MakeOpAssignUnary(ExpressionType.PostIncrementAssign, expression, null);
		}

		// Token: 0x060013A5 RID: 5029 RVA: 0x000437D8 File Offset: 0x000419D8
		[__DynamicallyInvokable]
		public static UnaryExpression PostIncrementAssign(Expression expression, MethodInfo method)
		{
			return Expression.MakeOpAssignUnary(ExpressionType.PostIncrementAssign, expression, method);
		}

		// Token: 0x060013A6 RID: 5030 RVA: 0x000437E3 File Offset: 0x000419E3
		[__DynamicallyInvokable]
		public static UnaryExpression PostDecrementAssign(Expression expression)
		{
			return Expression.MakeOpAssignUnary(ExpressionType.PostDecrementAssign, expression, null);
		}

		// Token: 0x060013A7 RID: 5031 RVA: 0x000437EE File Offset: 0x000419EE
		[__DynamicallyInvokable]
		public static UnaryExpression PostDecrementAssign(Expression expression, MethodInfo method)
		{
			return Expression.MakeOpAssignUnary(ExpressionType.PostDecrementAssign, expression, method);
		}

		// Token: 0x060013A8 RID: 5032 RVA: 0x000437FC File Offset: 0x000419FC
		private static UnaryExpression MakeOpAssignUnary(ExpressionType kind, Expression expression, MethodInfo method)
		{
			Expression.RequiresCanRead(expression, "expression");
			Expression.RequiresCanWrite(expression, "expression");
			UnaryExpression unaryExpression;
			if (method == null)
			{
				if (TypeUtils.IsArithmetic(expression.Type))
				{
					return new UnaryExpression(kind, expression, expression.Type, null);
				}
				string name;
				if (kind == ExpressionType.PreIncrementAssign || kind == ExpressionType.PostIncrementAssign)
				{
					name = "op_Increment";
				}
				else
				{
					name = "op_Decrement";
				}
				unaryExpression = Expression.GetUserDefinedUnaryOperatorOrThrow(kind, name, expression);
			}
			else
			{
				unaryExpression = Expression.GetMethodBasedUnaryOperator(kind, expression, method);
			}
			if (!TypeUtils.AreReferenceAssignable(expression.Type, unaryExpression.Type))
			{
				throw Error.UserDefinedOpMustHaveValidReturnType(kind, method.Name);
			}
			return unaryExpression;
		}

		// Token: 0x0400095F RID: 2399
		private static readonly CacheDict<Type, MethodInfo> _LambdaDelegateCache = new CacheDict<Type, MethodInfo>(40);

		// Token: 0x04000960 RID: 2400
		private static volatile CacheDict<Type, Expression.LambdaFactory> _LambdaFactories;

		// Token: 0x04000961 RID: 2401
		private static ConditionalWeakTable<Expression, Expression.ExtensionInfo> _legacyCtorSupportTable;

		// Token: 0x02000422 RID: 1058
		// (Invoke) Token: 0x06001EB1 RID: 7857
		private delegate LambdaExpression LambdaFactory(Expression body, string name, bool tailCall, ReadOnlyCollection<ParameterExpression> parameters);

		// Token: 0x02000423 RID: 1059
		private class ExtensionInfo
		{
			// Token: 0x06001EB4 RID: 7860 RVA: 0x0006DEE9 File Offset: 0x0006C0E9
			public ExtensionInfo(ExpressionType nodeType, Type type)
			{
				this.NodeType = nodeType;
				this.Type = type;
			}

			// Token: 0x0400128A RID: 4746
			internal readonly ExpressionType NodeType;

			// Token: 0x0400128B RID: 4747
			internal readonly Type Type;
		}

		// Token: 0x02000424 RID: 1060
		internal class BinaryExpressionProxy
		{
			// Token: 0x06001EB5 RID: 7861 RVA: 0x0006DEFF File Offset: 0x0006C0FF
			public BinaryExpressionProxy(BinaryExpression node)
			{
				this._node = node;
			}

			// Token: 0x17000583 RID: 1411
			// (get) Token: 0x06001EB6 RID: 7862 RVA: 0x0006DF0E File Offset: 0x0006C10E
			public bool CanReduce
			{
				get
				{
					return this._node.CanReduce;
				}
			}

			// Token: 0x17000584 RID: 1412
			// (get) Token: 0x06001EB7 RID: 7863 RVA: 0x0006DF1B File Offset: 0x0006C11B
			public LambdaExpression Conversion
			{
				get
				{
					return this._node.Conversion;
				}
			}

			// Token: 0x17000585 RID: 1413
			// (get) Token: 0x06001EB8 RID: 7864 RVA: 0x0006DF28 File Offset: 0x0006C128
			public string DebugView
			{
				get
				{
					return this._node.DebugView;
				}
			}

			// Token: 0x17000586 RID: 1414
			// (get) Token: 0x06001EB9 RID: 7865 RVA: 0x0006DF35 File Offset: 0x0006C135
			public bool IsLifted
			{
				get
				{
					return this._node.IsLifted;
				}
			}

			// Token: 0x17000587 RID: 1415
			// (get) Token: 0x06001EBA RID: 7866 RVA: 0x0006DF42 File Offset: 0x0006C142
			public bool IsLiftedToNull
			{
				get
				{
					return this._node.IsLiftedToNull;
				}
			}

			// Token: 0x17000588 RID: 1416
			// (get) Token: 0x06001EBB RID: 7867 RVA: 0x0006DF4F File Offset: 0x0006C14F
			public Expression Left
			{
				get
				{
					return this._node.Left;
				}
			}

			// Token: 0x17000589 RID: 1417
			// (get) Token: 0x06001EBC RID: 7868 RVA: 0x0006DF5C File Offset: 0x0006C15C
			public MethodInfo Method
			{
				get
				{
					return this._node.Method;
				}
			}

			// Token: 0x1700058A RID: 1418
			// (get) Token: 0x06001EBD RID: 7869 RVA: 0x0006DF69 File Offset: 0x0006C169
			public ExpressionType NodeType
			{
				get
				{
					return this._node.NodeType;
				}
			}

			// Token: 0x1700058B RID: 1419
			// (get) Token: 0x06001EBE RID: 7870 RVA: 0x0006DF76 File Offset: 0x0006C176
			public Expression Right
			{
				get
				{
					return this._node.Right;
				}
			}

			// Token: 0x1700058C RID: 1420
			// (get) Token: 0x06001EBF RID: 7871 RVA: 0x0006DF83 File Offset: 0x0006C183
			public Type Type
			{
				get
				{
					return this._node.Type;
				}
			}

			// Token: 0x0400128C RID: 4748
			private readonly BinaryExpression _node;
		}

		// Token: 0x02000425 RID: 1061
		internal class BlockExpressionProxy
		{
			// Token: 0x06001EC0 RID: 7872 RVA: 0x0006DF90 File Offset: 0x0006C190
			public BlockExpressionProxy(BlockExpression node)
			{
				this._node = node;
			}

			// Token: 0x1700058D RID: 1421
			// (get) Token: 0x06001EC1 RID: 7873 RVA: 0x0006DF9F File Offset: 0x0006C19F
			public bool CanReduce
			{
				get
				{
					return this._node.CanReduce;
				}
			}

			// Token: 0x1700058E RID: 1422
			// (get) Token: 0x06001EC2 RID: 7874 RVA: 0x0006DFAC File Offset: 0x0006C1AC
			public string DebugView
			{
				get
				{
					return this._node.DebugView;
				}
			}

			// Token: 0x1700058F RID: 1423
			// (get) Token: 0x06001EC3 RID: 7875 RVA: 0x0006DFB9 File Offset: 0x0006C1B9
			public ReadOnlyCollection<Expression> Expressions
			{
				get
				{
					return this._node.Expressions;
				}
			}

			// Token: 0x17000590 RID: 1424
			// (get) Token: 0x06001EC4 RID: 7876 RVA: 0x0006DFC6 File Offset: 0x0006C1C6
			public ExpressionType NodeType
			{
				get
				{
					return this._node.NodeType;
				}
			}

			// Token: 0x17000591 RID: 1425
			// (get) Token: 0x06001EC5 RID: 7877 RVA: 0x0006DFD3 File Offset: 0x0006C1D3
			public Expression Result
			{
				get
				{
					return this._node.Result;
				}
			}

			// Token: 0x17000592 RID: 1426
			// (get) Token: 0x06001EC6 RID: 7878 RVA: 0x0006DFE0 File Offset: 0x0006C1E0
			public Type Type
			{
				get
				{
					return this._node.Type;
				}
			}

			// Token: 0x17000593 RID: 1427
			// (get) Token: 0x06001EC7 RID: 7879 RVA: 0x0006DFED File Offset: 0x0006C1ED
			public ReadOnlyCollection<ParameterExpression> Variables
			{
				get
				{
					return this._node.Variables;
				}
			}

			// Token: 0x0400128D RID: 4749
			private readonly BlockExpression _node;
		}

		// Token: 0x02000426 RID: 1062
		internal class CatchBlockProxy
		{
			// Token: 0x06001EC8 RID: 7880 RVA: 0x0006DFFA File Offset: 0x0006C1FA
			public CatchBlockProxy(CatchBlock node)
			{
				this._node = node;
			}

			// Token: 0x17000594 RID: 1428
			// (get) Token: 0x06001EC9 RID: 7881 RVA: 0x0006E009 File Offset: 0x0006C209
			public Expression Body
			{
				get
				{
					return this._node.Body;
				}
			}

			// Token: 0x17000595 RID: 1429
			// (get) Token: 0x06001ECA RID: 7882 RVA: 0x0006E016 File Offset: 0x0006C216
			public Expression Filter
			{
				get
				{
					return this._node.Filter;
				}
			}

			// Token: 0x17000596 RID: 1430
			// (get) Token: 0x06001ECB RID: 7883 RVA: 0x0006E023 File Offset: 0x0006C223
			public Type Test
			{
				get
				{
					return this._node.Test;
				}
			}

			// Token: 0x17000597 RID: 1431
			// (get) Token: 0x06001ECC RID: 7884 RVA: 0x0006E030 File Offset: 0x0006C230
			public ParameterExpression Variable
			{
				get
				{
					return this._node.Variable;
				}
			}

			// Token: 0x0400128E RID: 4750
			private readonly CatchBlock _node;
		}

		// Token: 0x02000427 RID: 1063
		internal class ConditionalExpressionProxy
		{
			// Token: 0x06001ECD RID: 7885 RVA: 0x0006E03D File Offset: 0x0006C23D
			public ConditionalExpressionProxy(ConditionalExpression node)
			{
				this._node = node;
			}

			// Token: 0x17000598 RID: 1432
			// (get) Token: 0x06001ECE RID: 7886 RVA: 0x0006E04C File Offset: 0x0006C24C
			public bool CanReduce
			{
				get
				{
					return this._node.CanReduce;
				}
			}

			// Token: 0x17000599 RID: 1433
			// (get) Token: 0x06001ECF RID: 7887 RVA: 0x0006E059 File Offset: 0x0006C259
			public string DebugView
			{
				get
				{
					return this._node.DebugView;
				}
			}

			// Token: 0x1700059A RID: 1434
			// (get) Token: 0x06001ED0 RID: 7888 RVA: 0x0006E066 File Offset: 0x0006C266
			public Expression IfFalse
			{
				get
				{
					return this._node.IfFalse;
				}
			}

			// Token: 0x1700059B RID: 1435
			// (get) Token: 0x06001ED1 RID: 7889 RVA: 0x0006E073 File Offset: 0x0006C273
			public Expression IfTrue
			{
				get
				{
					return this._node.IfTrue;
				}
			}

			// Token: 0x1700059C RID: 1436
			// (get) Token: 0x06001ED2 RID: 7890 RVA: 0x0006E080 File Offset: 0x0006C280
			public ExpressionType NodeType
			{
				get
				{
					return this._node.NodeType;
				}
			}

			// Token: 0x1700059D RID: 1437
			// (get) Token: 0x06001ED3 RID: 7891 RVA: 0x0006E08D File Offset: 0x0006C28D
			public Expression Test
			{
				get
				{
					return this._node.Test;
				}
			}

			// Token: 0x1700059E RID: 1438
			// (get) Token: 0x06001ED4 RID: 7892 RVA: 0x0006E09A File Offset: 0x0006C29A
			public Type Type
			{
				get
				{
					return this._node.Type;
				}
			}

			// Token: 0x0400128F RID: 4751
			private readonly ConditionalExpression _node;
		}

		// Token: 0x02000428 RID: 1064
		internal class ConstantExpressionProxy
		{
			// Token: 0x06001ED5 RID: 7893 RVA: 0x0006E0A7 File Offset: 0x0006C2A7
			public ConstantExpressionProxy(ConstantExpression node)
			{
				this._node = node;
			}

			// Token: 0x1700059F RID: 1439
			// (get) Token: 0x06001ED6 RID: 7894 RVA: 0x0006E0B6 File Offset: 0x0006C2B6
			public bool CanReduce
			{
				get
				{
					return this._node.CanReduce;
				}
			}

			// Token: 0x170005A0 RID: 1440
			// (get) Token: 0x06001ED7 RID: 7895 RVA: 0x0006E0C3 File Offset: 0x0006C2C3
			public string DebugView
			{
				get
				{
					return this._node.DebugView;
				}
			}

			// Token: 0x170005A1 RID: 1441
			// (get) Token: 0x06001ED8 RID: 7896 RVA: 0x0006E0D0 File Offset: 0x0006C2D0
			public ExpressionType NodeType
			{
				get
				{
					return this._node.NodeType;
				}
			}

			// Token: 0x170005A2 RID: 1442
			// (get) Token: 0x06001ED9 RID: 7897 RVA: 0x0006E0DD File Offset: 0x0006C2DD
			public Type Type
			{
				get
				{
					return this._node.Type;
				}
			}

			// Token: 0x170005A3 RID: 1443
			// (get) Token: 0x06001EDA RID: 7898 RVA: 0x0006E0EA File Offset: 0x0006C2EA
			public object Value
			{
				get
				{
					return this._node.Value;
				}
			}

			// Token: 0x04001290 RID: 4752
			private readonly ConstantExpression _node;
		}

		// Token: 0x02000429 RID: 1065
		internal class DebugInfoExpressionProxy
		{
			// Token: 0x06001EDB RID: 7899 RVA: 0x0006E0F7 File Offset: 0x0006C2F7
			public DebugInfoExpressionProxy(DebugInfoExpression node)
			{
				this._node = node;
			}

			// Token: 0x170005A4 RID: 1444
			// (get) Token: 0x06001EDC RID: 7900 RVA: 0x0006E106 File Offset: 0x0006C306
			public bool CanReduce
			{
				get
				{
					return this._node.CanReduce;
				}
			}

			// Token: 0x170005A5 RID: 1445
			// (get) Token: 0x06001EDD RID: 7901 RVA: 0x0006E113 File Offset: 0x0006C313
			public string DebugView
			{
				get
				{
					return this._node.DebugView;
				}
			}

			// Token: 0x170005A6 RID: 1446
			// (get) Token: 0x06001EDE RID: 7902 RVA: 0x0006E120 File Offset: 0x0006C320
			public SymbolDocumentInfo Document
			{
				get
				{
					return this._node.Document;
				}
			}

			// Token: 0x170005A7 RID: 1447
			// (get) Token: 0x06001EDF RID: 7903 RVA: 0x0006E12D File Offset: 0x0006C32D
			public int EndColumn
			{
				get
				{
					return this._node.EndColumn;
				}
			}

			// Token: 0x170005A8 RID: 1448
			// (get) Token: 0x06001EE0 RID: 7904 RVA: 0x0006E13A File Offset: 0x0006C33A
			public int EndLine
			{
				get
				{
					return this._node.EndLine;
				}
			}

			// Token: 0x170005A9 RID: 1449
			// (get) Token: 0x06001EE1 RID: 7905 RVA: 0x0006E147 File Offset: 0x0006C347
			public bool IsClear
			{
				get
				{
					return this._node.IsClear;
				}
			}

			// Token: 0x170005AA RID: 1450
			// (get) Token: 0x06001EE2 RID: 7906 RVA: 0x0006E154 File Offset: 0x0006C354
			public ExpressionType NodeType
			{
				get
				{
					return this._node.NodeType;
				}
			}

			// Token: 0x170005AB RID: 1451
			// (get) Token: 0x06001EE3 RID: 7907 RVA: 0x0006E161 File Offset: 0x0006C361
			public int StartColumn
			{
				get
				{
					return this._node.StartColumn;
				}
			}

			// Token: 0x170005AC RID: 1452
			// (get) Token: 0x06001EE4 RID: 7908 RVA: 0x0006E16E File Offset: 0x0006C36E
			public int StartLine
			{
				get
				{
					return this._node.StartLine;
				}
			}

			// Token: 0x170005AD RID: 1453
			// (get) Token: 0x06001EE5 RID: 7909 RVA: 0x0006E17B File Offset: 0x0006C37B
			public Type Type
			{
				get
				{
					return this._node.Type;
				}
			}

			// Token: 0x04001291 RID: 4753
			private readonly DebugInfoExpression _node;
		}

		// Token: 0x0200042A RID: 1066
		internal class DefaultExpressionProxy
		{
			// Token: 0x06001EE6 RID: 7910 RVA: 0x0006E188 File Offset: 0x0006C388
			public DefaultExpressionProxy(DefaultExpression node)
			{
				this._node = node;
			}

			// Token: 0x170005AE RID: 1454
			// (get) Token: 0x06001EE7 RID: 7911 RVA: 0x0006E197 File Offset: 0x0006C397
			public bool CanReduce
			{
				get
				{
					return this._node.CanReduce;
				}
			}

			// Token: 0x170005AF RID: 1455
			// (get) Token: 0x06001EE8 RID: 7912 RVA: 0x0006E1A4 File Offset: 0x0006C3A4
			public string DebugView
			{
				get
				{
					return this._node.DebugView;
				}
			}

			// Token: 0x170005B0 RID: 1456
			// (get) Token: 0x06001EE9 RID: 7913 RVA: 0x0006E1B1 File Offset: 0x0006C3B1
			public ExpressionType NodeType
			{
				get
				{
					return this._node.NodeType;
				}
			}

			// Token: 0x170005B1 RID: 1457
			// (get) Token: 0x06001EEA RID: 7914 RVA: 0x0006E1BE File Offset: 0x0006C3BE
			public Type Type
			{
				get
				{
					return this._node.Type;
				}
			}

			// Token: 0x04001292 RID: 4754
			private readonly DefaultExpression _node;
		}

		// Token: 0x0200042B RID: 1067
		internal class DynamicExpressionProxy
		{
			// Token: 0x06001EEB RID: 7915 RVA: 0x0006E1CB File Offset: 0x0006C3CB
			public DynamicExpressionProxy(DynamicExpression node)
			{
				this._node = node;
			}

			// Token: 0x170005B2 RID: 1458
			// (get) Token: 0x06001EEC RID: 7916 RVA: 0x0006E1DA File Offset: 0x0006C3DA
			public ReadOnlyCollection<Expression> Arguments
			{
				get
				{
					return this._node.Arguments;
				}
			}

			// Token: 0x170005B3 RID: 1459
			// (get) Token: 0x06001EED RID: 7917 RVA: 0x0006E1E7 File Offset: 0x0006C3E7
			public CallSiteBinder Binder
			{
				get
				{
					return this._node.Binder;
				}
			}

			// Token: 0x170005B4 RID: 1460
			// (get) Token: 0x06001EEE RID: 7918 RVA: 0x0006E1F4 File Offset: 0x0006C3F4
			public bool CanReduce
			{
				get
				{
					return this._node.CanReduce;
				}
			}

			// Token: 0x170005B5 RID: 1461
			// (get) Token: 0x06001EEF RID: 7919 RVA: 0x0006E201 File Offset: 0x0006C401
			public string DebugView
			{
				get
				{
					return this._node.DebugView;
				}
			}

			// Token: 0x170005B6 RID: 1462
			// (get) Token: 0x06001EF0 RID: 7920 RVA: 0x0006E20E File Offset: 0x0006C40E
			public Type DelegateType
			{
				get
				{
					return this._node.DelegateType;
				}
			}

			// Token: 0x170005B7 RID: 1463
			// (get) Token: 0x06001EF1 RID: 7921 RVA: 0x0006E21B File Offset: 0x0006C41B
			public ExpressionType NodeType
			{
				get
				{
					return this._node.NodeType;
				}
			}

			// Token: 0x170005B8 RID: 1464
			// (get) Token: 0x06001EF2 RID: 7922 RVA: 0x0006E228 File Offset: 0x0006C428
			public Type Type
			{
				get
				{
					return this._node.Type;
				}
			}

			// Token: 0x04001293 RID: 4755
			private readonly DynamicExpression _node;
		}

		// Token: 0x0200042C RID: 1068
		internal class GotoExpressionProxy
		{
			// Token: 0x06001EF3 RID: 7923 RVA: 0x0006E235 File Offset: 0x0006C435
			public GotoExpressionProxy(GotoExpression node)
			{
				this._node = node;
			}

			// Token: 0x170005B9 RID: 1465
			// (get) Token: 0x06001EF4 RID: 7924 RVA: 0x0006E244 File Offset: 0x0006C444
			public bool CanReduce
			{
				get
				{
					return this._node.CanReduce;
				}
			}

			// Token: 0x170005BA RID: 1466
			// (get) Token: 0x06001EF5 RID: 7925 RVA: 0x0006E251 File Offset: 0x0006C451
			public string DebugView
			{
				get
				{
					return this._node.DebugView;
				}
			}

			// Token: 0x170005BB RID: 1467
			// (get) Token: 0x06001EF6 RID: 7926 RVA: 0x0006E25E File Offset: 0x0006C45E
			public GotoExpressionKind Kind
			{
				get
				{
					return this._node.Kind;
				}
			}

			// Token: 0x170005BC RID: 1468
			// (get) Token: 0x06001EF7 RID: 7927 RVA: 0x0006E26B File Offset: 0x0006C46B
			public ExpressionType NodeType
			{
				get
				{
					return this._node.NodeType;
				}
			}

			// Token: 0x170005BD RID: 1469
			// (get) Token: 0x06001EF8 RID: 7928 RVA: 0x0006E278 File Offset: 0x0006C478
			public LabelTarget Target
			{
				get
				{
					return this._node.Target;
				}
			}

			// Token: 0x170005BE RID: 1470
			// (get) Token: 0x06001EF9 RID: 7929 RVA: 0x0006E285 File Offset: 0x0006C485
			public Type Type
			{
				get
				{
					return this._node.Type;
				}
			}

			// Token: 0x170005BF RID: 1471
			// (get) Token: 0x06001EFA RID: 7930 RVA: 0x0006E292 File Offset: 0x0006C492
			public Expression Value
			{
				get
				{
					return this._node.Value;
				}
			}

			// Token: 0x04001294 RID: 4756
			private readonly GotoExpression _node;
		}

		// Token: 0x0200042D RID: 1069
		internal class IndexExpressionProxy
		{
			// Token: 0x06001EFB RID: 7931 RVA: 0x0006E29F File Offset: 0x0006C49F
			public IndexExpressionProxy(IndexExpression node)
			{
				this._node = node;
			}

			// Token: 0x170005C0 RID: 1472
			// (get) Token: 0x06001EFC RID: 7932 RVA: 0x0006E2AE File Offset: 0x0006C4AE
			public ReadOnlyCollection<Expression> Arguments
			{
				get
				{
					return this._node.Arguments;
				}
			}

			// Token: 0x170005C1 RID: 1473
			// (get) Token: 0x06001EFD RID: 7933 RVA: 0x0006E2BB File Offset: 0x0006C4BB
			public bool CanReduce
			{
				get
				{
					return this._node.CanReduce;
				}
			}

			// Token: 0x170005C2 RID: 1474
			// (get) Token: 0x06001EFE RID: 7934 RVA: 0x0006E2C8 File Offset: 0x0006C4C8
			public string DebugView
			{
				get
				{
					return this._node.DebugView;
				}
			}

			// Token: 0x170005C3 RID: 1475
			// (get) Token: 0x06001EFF RID: 7935 RVA: 0x0006E2D5 File Offset: 0x0006C4D5
			public PropertyInfo Indexer
			{
				get
				{
					return this._node.Indexer;
				}
			}

			// Token: 0x170005C4 RID: 1476
			// (get) Token: 0x06001F00 RID: 7936 RVA: 0x0006E2E2 File Offset: 0x0006C4E2
			public ExpressionType NodeType
			{
				get
				{
					return this._node.NodeType;
				}
			}

			// Token: 0x170005C5 RID: 1477
			// (get) Token: 0x06001F01 RID: 7937 RVA: 0x0006E2EF File Offset: 0x0006C4EF
			public Expression Object
			{
				get
				{
					return this._node.Object;
				}
			}

			// Token: 0x170005C6 RID: 1478
			// (get) Token: 0x06001F02 RID: 7938 RVA: 0x0006E2FC File Offset: 0x0006C4FC
			public Type Type
			{
				get
				{
					return this._node.Type;
				}
			}

			// Token: 0x04001295 RID: 4757
			private readonly IndexExpression _node;
		}

		// Token: 0x0200042E RID: 1070
		internal class InvocationExpressionProxy
		{
			// Token: 0x06001F03 RID: 7939 RVA: 0x0006E309 File Offset: 0x0006C509
			public InvocationExpressionProxy(InvocationExpression node)
			{
				this._node = node;
			}

			// Token: 0x170005C7 RID: 1479
			// (get) Token: 0x06001F04 RID: 7940 RVA: 0x0006E318 File Offset: 0x0006C518
			public ReadOnlyCollection<Expression> Arguments
			{
				get
				{
					return this._node.Arguments;
				}
			}

			// Token: 0x170005C8 RID: 1480
			// (get) Token: 0x06001F05 RID: 7941 RVA: 0x0006E325 File Offset: 0x0006C525
			public bool CanReduce
			{
				get
				{
					return this._node.CanReduce;
				}
			}

			// Token: 0x170005C9 RID: 1481
			// (get) Token: 0x06001F06 RID: 7942 RVA: 0x0006E332 File Offset: 0x0006C532
			public string DebugView
			{
				get
				{
					return this._node.DebugView;
				}
			}

			// Token: 0x170005CA RID: 1482
			// (get) Token: 0x06001F07 RID: 7943 RVA: 0x0006E33F File Offset: 0x0006C53F
			public Expression Expression
			{
				get
				{
					return this._node.Expression;
				}
			}

			// Token: 0x170005CB RID: 1483
			// (get) Token: 0x06001F08 RID: 7944 RVA: 0x0006E34C File Offset: 0x0006C54C
			public ExpressionType NodeType
			{
				get
				{
					return this._node.NodeType;
				}
			}

			// Token: 0x170005CC RID: 1484
			// (get) Token: 0x06001F09 RID: 7945 RVA: 0x0006E359 File Offset: 0x0006C559
			public Type Type
			{
				get
				{
					return this._node.Type;
				}
			}

			// Token: 0x04001296 RID: 4758
			private readonly InvocationExpression _node;
		}

		// Token: 0x0200042F RID: 1071
		internal class LabelExpressionProxy
		{
			// Token: 0x06001F0A RID: 7946 RVA: 0x0006E366 File Offset: 0x0006C566
			public LabelExpressionProxy(LabelExpression node)
			{
				this._node = node;
			}

			// Token: 0x170005CD RID: 1485
			// (get) Token: 0x06001F0B RID: 7947 RVA: 0x0006E375 File Offset: 0x0006C575
			public bool CanReduce
			{
				get
				{
					return this._node.CanReduce;
				}
			}

			// Token: 0x170005CE RID: 1486
			// (get) Token: 0x06001F0C RID: 7948 RVA: 0x0006E382 File Offset: 0x0006C582
			public string DebugView
			{
				get
				{
					return this._node.DebugView;
				}
			}

			// Token: 0x170005CF RID: 1487
			// (get) Token: 0x06001F0D RID: 7949 RVA: 0x0006E38F File Offset: 0x0006C58F
			public Expression DefaultValue
			{
				get
				{
					return this._node.DefaultValue;
				}
			}

			// Token: 0x170005D0 RID: 1488
			// (get) Token: 0x06001F0E RID: 7950 RVA: 0x0006E39C File Offset: 0x0006C59C
			public ExpressionType NodeType
			{
				get
				{
					return this._node.NodeType;
				}
			}

			// Token: 0x170005D1 RID: 1489
			// (get) Token: 0x06001F0F RID: 7951 RVA: 0x0006E3A9 File Offset: 0x0006C5A9
			public LabelTarget Target
			{
				get
				{
					return this._node.Target;
				}
			}

			// Token: 0x170005D2 RID: 1490
			// (get) Token: 0x06001F10 RID: 7952 RVA: 0x0006E3B6 File Offset: 0x0006C5B6
			public Type Type
			{
				get
				{
					return this._node.Type;
				}
			}

			// Token: 0x04001297 RID: 4759
			private readonly LabelExpression _node;
		}

		// Token: 0x02000430 RID: 1072
		internal class LambdaExpressionProxy
		{
			// Token: 0x06001F11 RID: 7953 RVA: 0x0006E3C3 File Offset: 0x0006C5C3
			public LambdaExpressionProxy(LambdaExpression node)
			{
				this._node = node;
			}

			// Token: 0x170005D3 RID: 1491
			// (get) Token: 0x06001F12 RID: 7954 RVA: 0x0006E3D2 File Offset: 0x0006C5D2
			public Expression Body
			{
				get
				{
					return this._node.Body;
				}
			}

			// Token: 0x170005D4 RID: 1492
			// (get) Token: 0x06001F13 RID: 7955 RVA: 0x0006E3DF File Offset: 0x0006C5DF
			public bool CanReduce
			{
				get
				{
					return this._node.CanReduce;
				}
			}

			// Token: 0x170005D5 RID: 1493
			// (get) Token: 0x06001F14 RID: 7956 RVA: 0x0006E3EC File Offset: 0x0006C5EC
			public string DebugView
			{
				get
				{
					return this._node.DebugView;
				}
			}

			// Token: 0x170005D6 RID: 1494
			// (get) Token: 0x06001F15 RID: 7957 RVA: 0x0006E3F9 File Offset: 0x0006C5F9
			public string Name
			{
				get
				{
					return this._node.Name;
				}
			}

			// Token: 0x170005D7 RID: 1495
			// (get) Token: 0x06001F16 RID: 7958 RVA: 0x0006E406 File Offset: 0x0006C606
			public ExpressionType NodeType
			{
				get
				{
					return this._node.NodeType;
				}
			}

			// Token: 0x170005D8 RID: 1496
			// (get) Token: 0x06001F17 RID: 7959 RVA: 0x0006E413 File Offset: 0x0006C613
			public ReadOnlyCollection<ParameterExpression> Parameters
			{
				get
				{
					return this._node.Parameters;
				}
			}

			// Token: 0x170005D9 RID: 1497
			// (get) Token: 0x06001F18 RID: 7960 RVA: 0x0006E420 File Offset: 0x0006C620
			public Type ReturnType
			{
				get
				{
					return this._node.ReturnType;
				}
			}

			// Token: 0x170005DA RID: 1498
			// (get) Token: 0x06001F19 RID: 7961 RVA: 0x0006E42D File Offset: 0x0006C62D
			public bool TailCall
			{
				get
				{
					return this._node.TailCall;
				}
			}

			// Token: 0x170005DB RID: 1499
			// (get) Token: 0x06001F1A RID: 7962 RVA: 0x0006E43A File Offset: 0x0006C63A
			public Type Type
			{
				get
				{
					return this._node.Type;
				}
			}

			// Token: 0x04001298 RID: 4760
			private readonly LambdaExpression _node;
		}

		// Token: 0x02000431 RID: 1073
		internal class ListInitExpressionProxy
		{
			// Token: 0x06001F1B RID: 7963 RVA: 0x0006E447 File Offset: 0x0006C647
			public ListInitExpressionProxy(ListInitExpression node)
			{
				this._node = node;
			}

			// Token: 0x170005DC RID: 1500
			// (get) Token: 0x06001F1C RID: 7964 RVA: 0x0006E456 File Offset: 0x0006C656
			public bool CanReduce
			{
				get
				{
					return this._node.CanReduce;
				}
			}

			// Token: 0x170005DD RID: 1501
			// (get) Token: 0x06001F1D RID: 7965 RVA: 0x0006E463 File Offset: 0x0006C663
			public string DebugView
			{
				get
				{
					return this._node.DebugView;
				}
			}

			// Token: 0x170005DE RID: 1502
			// (get) Token: 0x06001F1E RID: 7966 RVA: 0x0006E470 File Offset: 0x0006C670
			public ReadOnlyCollection<ElementInit> Initializers
			{
				get
				{
					return this._node.Initializers;
				}
			}

			// Token: 0x170005DF RID: 1503
			// (get) Token: 0x06001F1F RID: 7967 RVA: 0x0006E47D File Offset: 0x0006C67D
			public NewExpression NewExpression
			{
				get
				{
					return this._node.NewExpression;
				}
			}

			// Token: 0x170005E0 RID: 1504
			// (get) Token: 0x06001F20 RID: 7968 RVA: 0x0006E48A File Offset: 0x0006C68A
			public ExpressionType NodeType
			{
				get
				{
					return this._node.NodeType;
				}
			}

			// Token: 0x170005E1 RID: 1505
			// (get) Token: 0x06001F21 RID: 7969 RVA: 0x0006E497 File Offset: 0x0006C697
			public Type Type
			{
				get
				{
					return this._node.Type;
				}
			}

			// Token: 0x04001299 RID: 4761
			private readonly ListInitExpression _node;
		}

		// Token: 0x02000432 RID: 1074
		internal class LoopExpressionProxy
		{
			// Token: 0x06001F22 RID: 7970 RVA: 0x0006E4A4 File Offset: 0x0006C6A4
			public LoopExpressionProxy(LoopExpression node)
			{
				this._node = node;
			}

			// Token: 0x170005E2 RID: 1506
			// (get) Token: 0x06001F23 RID: 7971 RVA: 0x0006E4B3 File Offset: 0x0006C6B3
			public Expression Body
			{
				get
				{
					return this._node.Body;
				}
			}

			// Token: 0x170005E3 RID: 1507
			// (get) Token: 0x06001F24 RID: 7972 RVA: 0x0006E4C0 File Offset: 0x0006C6C0
			public LabelTarget BreakLabel
			{
				get
				{
					return this._node.BreakLabel;
				}
			}

			// Token: 0x170005E4 RID: 1508
			// (get) Token: 0x06001F25 RID: 7973 RVA: 0x0006E4CD File Offset: 0x0006C6CD
			public bool CanReduce
			{
				get
				{
					return this._node.CanReduce;
				}
			}

			// Token: 0x170005E5 RID: 1509
			// (get) Token: 0x06001F26 RID: 7974 RVA: 0x0006E4DA File Offset: 0x0006C6DA
			public LabelTarget ContinueLabel
			{
				get
				{
					return this._node.ContinueLabel;
				}
			}

			// Token: 0x170005E6 RID: 1510
			// (get) Token: 0x06001F27 RID: 7975 RVA: 0x0006E4E7 File Offset: 0x0006C6E7
			public string DebugView
			{
				get
				{
					return this._node.DebugView;
				}
			}

			// Token: 0x170005E7 RID: 1511
			// (get) Token: 0x06001F28 RID: 7976 RVA: 0x0006E4F4 File Offset: 0x0006C6F4
			public ExpressionType NodeType
			{
				get
				{
					return this._node.NodeType;
				}
			}

			// Token: 0x170005E8 RID: 1512
			// (get) Token: 0x06001F29 RID: 7977 RVA: 0x0006E501 File Offset: 0x0006C701
			public Type Type
			{
				get
				{
					return this._node.Type;
				}
			}

			// Token: 0x0400129A RID: 4762
			private readonly LoopExpression _node;
		}

		// Token: 0x02000433 RID: 1075
		internal class MemberExpressionProxy
		{
			// Token: 0x06001F2A RID: 7978 RVA: 0x0006E50E File Offset: 0x0006C70E
			public MemberExpressionProxy(MemberExpression node)
			{
				this._node = node;
			}

			// Token: 0x170005E9 RID: 1513
			// (get) Token: 0x06001F2B RID: 7979 RVA: 0x0006E51D File Offset: 0x0006C71D
			public bool CanReduce
			{
				get
				{
					return this._node.CanReduce;
				}
			}

			// Token: 0x170005EA RID: 1514
			// (get) Token: 0x06001F2C RID: 7980 RVA: 0x0006E52A File Offset: 0x0006C72A
			public string DebugView
			{
				get
				{
					return this._node.DebugView;
				}
			}

			// Token: 0x170005EB RID: 1515
			// (get) Token: 0x06001F2D RID: 7981 RVA: 0x0006E537 File Offset: 0x0006C737
			public Expression Expression
			{
				get
				{
					return this._node.Expression;
				}
			}

			// Token: 0x170005EC RID: 1516
			// (get) Token: 0x06001F2E RID: 7982 RVA: 0x0006E544 File Offset: 0x0006C744
			public MemberInfo Member
			{
				get
				{
					return this._node.Member;
				}
			}

			// Token: 0x170005ED RID: 1517
			// (get) Token: 0x06001F2F RID: 7983 RVA: 0x0006E551 File Offset: 0x0006C751
			public ExpressionType NodeType
			{
				get
				{
					return this._node.NodeType;
				}
			}

			// Token: 0x170005EE RID: 1518
			// (get) Token: 0x06001F30 RID: 7984 RVA: 0x0006E55E File Offset: 0x0006C75E
			public Type Type
			{
				get
				{
					return this._node.Type;
				}
			}

			// Token: 0x0400129B RID: 4763
			private readonly MemberExpression _node;
		}

		// Token: 0x02000434 RID: 1076
		internal class MemberInitExpressionProxy
		{
			// Token: 0x06001F31 RID: 7985 RVA: 0x0006E56B File Offset: 0x0006C76B
			public MemberInitExpressionProxy(MemberInitExpression node)
			{
				this._node = node;
			}

			// Token: 0x170005EF RID: 1519
			// (get) Token: 0x06001F32 RID: 7986 RVA: 0x0006E57A File Offset: 0x0006C77A
			public ReadOnlyCollection<MemberBinding> Bindings
			{
				get
				{
					return this._node.Bindings;
				}
			}

			// Token: 0x170005F0 RID: 1520
			// (get) Token: 0x06001F33 RID: 7987 RVA: 0x0006E587 File Offset: 0x0006C787
			public bool CanReduce
			{
				get
				{
					return this._node.CanReduce;
				}
			}

			// Token: 0x170005F1 RID: 1521
			// (get) Token: 0x06001F34 RID: 7988 RVA: 0x0006E594 File Offset: 0x0006C794
			public string DebugView
			{
				get
				{
					return this._node.DebugView;
				}
			}

			// Token: 0x170005F2 RID: 1522
			// (get) Token: 0x06001F35 RID: 7989 RVA: 0x0006E5A1 File Offset: 0x0006C7A1
			public NewExpression NewExpression
			{
				get
				{
					return this._node.NewExpression;
				}
			}

			// Token: 0x170005F3 RID: 1523
			// (get) Token: 0x06001F36 RID: 7990 RVA: 0x0006E5AE File Offset: 0x0006C7AE
			public ExpressionType NodeType
			{
				get
				{
					return this._node.NodeType;
				}
			}

			// Token: 0x170005F4 RID: 1524
			// (get) Token: 0x06001F37 RID: 7991 RVA: 0x0006E5BB File Offset: 0x0006C7BB
			public Type Type
			{
				get
				{
					return this._node.Type;
				}
			}

			// Token: 0x0400129C RID: 4764
			private readonly MemberInitExpression _node;
		}

		// Token: 0x02000435 RID: 1077
		internal class MethodCallExpressionProxy
		{
			// Token: 0x06001F38 RID: 7992 RVA: 0x0006E5C8 File Offset: 0x0006C7C8
			public MethodCallExpressionProxy(MethodCallExpression node)
			{
				this._node = node;
			}

			// Token: 0x170005F5 RID: 1525
			// (get) Token: 0x06001F39 RID: 7993 RVA: 0x0006E5D7 File Offset: 0x0006C7D7
			public ReadOnlyCollection<Expression> Arguments
			{
				get
				{
					return this._node.Arguments;
				}
			}

			// Token: 0x170005F6 RID: 1526
			// (get) Token: 0x06001F3A RID: 7994 RVA: 0x0006E5E4 File Offset: 0x0006C7E4
			public bool CanReduce
			{
				get
				{
					return this._node.CanReduce;
				}
			}

			// Token: 0x170005F7 RID: 1527
			// (get) Token: 0x06001F3B RID: 7995 RVA: 0x0006E5F1 File Offset: 0x0006C7F1
			public string DebugView
			{
				get
				{
					return this._node.DebugView;
				}
			}

			// Token: 0x170005F8 RID: 1528
			// (get) Token: 0x06001F3C RID: 7996 RVA: 0x0006E5FE File Offset: 0x0006C7FE
			public MethodInfo Method
			{
				get
				{
					return this._node.Method;
				}
			}

			// Token: 0x170005F9 RID: 1529
			// (get) Token: 0x06001F3D RID: 7997 RVA: 0x0006E60B File Offset: 0x0006C80B
			public ExpressionType NodeType
			{
				get
				{
					return this._node.NodeType;
				}
			}

			// Token: 0x170005FA RID: 1530
			// (get) Token: 0x06001F3E RID: 7998 RVA: 0x0006E618 File Offset: 0x0006C818
			public Expression Object
			{
				get
				{
					return this._node.Object;
				}
			}

			// Token: 0x170005FB RID: 1531
			// (get) Token: 0x06001F3F RID: 7999 RVA: 0x0006E625 File Offset: 0x0006C825
			public Type Type
			{
				get
				{
					return this._node.Type;
				}
			}

			// Token: 0x0400129D RID: 4765
			private readonly MethodCallExpression _node;
		}

		// Token: 0x02000436 RID: 1078
		internal class NewArrayExpressionProxy
		{
			// Token: 0x06001F40 RID: 8000 RVA: 0x0006E632 File Offset: 0x0006C832
			public NewArrayExpressionProxy(NewArrayExpression node)
			{
				this._node = node;
			}

			// Token: 0x170005FC RID: 1532
			// (get) Token: 0x06001F41 RID: 8001 RVA: 0x0006E641 File Offset: 0x0006C841
			public bool CanReduce
			{
				get
				{
					return this._node.CanReduce;
				}
			}

			// Token: 0x170005FD RID: 1533
			// (get) Token: 0x06001F42 RID: 8002 RVA: 0x0006E64E File Offset: 0x0006C84E
			public string DebugView
			{
				get
				{
					return this._node.DebugView;
				}
			}

			// Token: 0x170005FE RID: 1534
			// (get) Token: 0x06001F43 RID: 8003 RVA: 0x0006E65B File Offset: 0x0006C85B
			public ReadOnlyCollection<Expression> Expressions
			{
				get
				{
					return this._node.Expressions;
				}
			}

			// Token: 0x170005FF RID: 1535
			// (get) Token: 0x06001F44 RID: 8004 RVA: 0x0006E668 File Offset: 0x0006C868
			public ExpressionType NodeType
			{
				get
				{
					return this._node.NodeType;
				}
			}

			// Token: 0x17000600 RID: 1536
			// (get) Token: 0x06001F45 RID: 8005 RVA: 0x0006E675 File Offset: 0x0006C875
			public Type Type
			{
				get
				{
					return this._node.Type;
				}
			}

			// Token: 0x0400129E RID: 4766
			private readonly NewArrayExpression _node;
		}

		// Token: 0x02000437 RID: 1079
		internal class NewExpressionProxy
		{
			// Token: 0x06001F46 RID: 8006 RVA: 0x0006E682 File Offset: 0x0006C882
			public NewExpressionProxy(NewExpression node)
			{
				this._node = node;
			}

			// Token: 0x17000601 RID: 1537
			// (get) Token: 0x06001F47 RID: 8007 RVA: 0x0006E691 File Offset: 0x0006C891
			public ReadOnlyCollection<Expression> Arguments
			{
				get
				{
					return this._node.Arguments;
				}
			}

			// Token: 0x17000602 RID: 1538
			// (get) Token: 0x06001F48 RID: 8008 RVA: 0x0006E69E File Offset: 0x0006C89E
			public bool CanReduce
			{
				get
				{
					return this._node.CanReduce;
				}
			}

			// Token: 0x17000603 RID: 1539
			// (get) Token: 0x06001F49 RID: 8009 RVA: 0x0006E6AB File Offset: 0x0006C8AB
			public ConstructorInfo Constructor
			{
				get
				{
					return this._node.Constructor;
				}
			}

			// Token: 0x17000604 RID: 1540
			// (get) Token: 0x06001F4A RID: 8010 RVA: 0x0006E6B8 File Offset: 0x0006C8B8
			public string DebugView
			{
				get
				{
					return this._node.DebugView;
				}
			}

			// Token: 0x17000605 RID: 1541
			// (get) Token: 0x06001F4B RID: 8011 RVA: 0x0006E6C5 File Offset: 0x0006C8C5
			public ReadOnlyCollection<MemberInfo> Members
			{
				get
				{
					return this._node.Members;
				}
			}

			// Token: 0x17000606 RID: 1542
			// (get) Token: 0x06001F4C RID: 8012 RVA: 0x0006E6D2 File Offset: 0x0006C8D2
			public ExpressionType NodeType
			{
				get
				{
					return this._node.NodeType;
				}
			}

			// Token: 0x17000607 RID: 1543
			// (get) Token: 0x06001F4D RID: 8013 RVA: 0x0006E6DF File Offset: 0x0006C8DF
			public Type Type
			{
				get
				{
					return this._node.Type;
				}
			}

			// Token: 0x0400129F RID: 4767
			private readonly NewExpression _node;
		}

		// Token: 0x02000438 RID: 1080
		internal class ParameterExpressionProxy
		{
			// Token: 0x06001F4E RID: 8014 RVA: 0x0006E6EC File Offset: 0x0006C8EC
			public ParameterExpressionProxy(ParameterExpression node)
			{
				this._node = node;
			}

			// Token: 0x17000608 RID: 1544
			// (get) Token: 0x06001F4F RID: 8015 RVA: 0x0006E6FB File Offset: 0x0006C8FB
			public bool CanReduce
			{
				get
				{
					return this._node.CanReduce;
				}
			}

			// Token: 0x17000609 RID: 1545
			// (get) Token: 0x06001F50 RID: 8016 RVA: 0x0006E708 File Offset: 0x0006C908
			public string DebugView
			{
				get
				{
					return this._node.DebugView;
				}
			}

			// Token: 0x1700060A RID: 1546
			// (get) Token: 0x06001F51 RID: 8017 RVA: 0x0006E715 File Offset: 0x0006C915
			public bool IsByRef
			{
				get
				{
					return this._node.IsByRef;
				}
			}

			// Token: 0x1700060B RID: 1547
			// (get) Token: 0x06001F52 RID: 8018 RVA: 0x0006E722 File Offset: 0x0006C922
			public string Name
			{
				get
				{
					return this._node.Name;
				}
			}

			// Token: 0x1700060C RID: 1548
			// (get) Token: 0x06001F53 RID: 8019 RVA: 0x0006E72F File Offset: 0x0006C92F
			public ExpressionType NodeType
			{
				get
				{
					return this._node.NodeType;
				}
			}

			// Token: 0x1700060D RID: 1549
			// (get) Token: 0x06001F54 RID: 8020 RVA: 0x0006E73C File Offset: 0x0006C93C
			public Type Type
			{
				get
				{
					return this._node.Type;
				}
			}

			// Token: 0x040012A0 RID: 4768
			private readonly ParameterExpression _node;
		}

		// Token: 0x02000439 RID: 1081
		internal class RuntimeVariablesExpressionProxy
		{
			// Token: 0x06001F55 RID: 8021 RVA: 0x0006E749 File Offset: 0x0006C949
			public RuntimeVariablesExpressionProxy(RuntimeVariablesExpression node)
			{
				this._node = node;
			}

			// Token: 0x1700060E RID: 1550
			// (get) Token: 0x06001F56 RID: 8022 RVA: 0x0006E758 File Offset: 0x0006C958
			public bool CanReduce
			{
				get
				{
					return this._node.CanReduce;
				}
			}

			// Token: 0x1700060F RID: 1551
			// (get) Token: 0x06001F57 RID: 8023 RVA: 0x0006E765 File Offset: 0x0006C965
			public string DebugView
			{
				get
				{
					return this._node.DebugView;
				}
			}

			// Token: 0x17000610 RID: 1552
			// (get) Token: 0x06001F58 RID: 8024 RVA: 0x0006E772 File Offset: 0x0006C972
			public ExpressionType NodeType
			{
				get
				{
					return this._node.NodeType;
				}
			}

			// Token: 0x17000611 RID: 1553
			// (get) Token: 0x06001F59 RID: 8025 RVA: 0x0006E77F File Offset: 0x0006C97F
			public Type Type
			{
				get
				{
					return this._node.Type;
				}
			}

			// Token: 0x17000612 RID: 1554
			// (get) Token: 0x06001F5A RID: 8026 RVA: 0x0006E78C File Offset: 0x0006C98C
			public ReadOnlyCollection<ParameterExpression> Variables
			{
				get
				{
					return this._node.Variables;
				}
			}

			// Token: 0x040012A1 RID: 4769
			private readonly RuntimeVariablesExpression _node;
		}

		// Token: 0x0200043A RID: 1082
		internal class SwitchCaseProxy
		{
			// Token: 0x06001F5B RID: 8027 RVA: 0x0006E799 File Offset: 0x0006C999
			public SwitchCaseProxy(SwitchCase node)
			{
				this._node = node;
			}

			// Token: 0x17000613 RID: 1555
			// (get) Token: 0x06001F5C RID: 8028 RVA: 0x0006E7A8 File Offset: 0x0006C9A8
			public Expression Body
			{
				get
				{
					return this._node.Body;
				}
			}

			// Token: 0x17000614 RID: 1556
			// (get) Token: 0x06001F5D RID: 8029 RVA: 0x0006E7B5 File Offset: 0x0006C9B5
			public ReadOnlyCollection<Expression> TestValues
			{
				get
				{
					return this._node.TestValues;
				}
			}

			// Token: 0x040012A2 RID: 4770
			private readonly SwitchCase _node;
		}

		// Token: 0x0200043B RID: 1083
		internal class SwitchExpressionProxy
		{
			// Token: 0x06001F5E RID: 8030 RVA: 0x0006E7C2 File Offset: 0x0006C9C2
			public SwitchExpressionProxy(SwitchExpression node)
			{
				this._node = node;
			}

			// Token: 0x17000615 RID: 1557
			// (get) Token: 0x06001F5F RID: 8031 RVA: 0x0006E7D1 File Offset: 0x0006C9D1
			public bool CanReduce
			{
				get
				{
					return this._node.CanReduce;
				}
			}

			// Token: 0x17000616 RID: 1558
			// (get) Token: 0x06001F60 RID: 8032 RVA: 0x0006E7DE File Offset: 0x0006C9DE
			public ReadOnlyCollection<SwitchCase> Cases
			{
				get
				{
					return this._node.Cases;
				}
			}

			// Token: 0x17000617 RID: 1559
			// (get) Token: 0x06001F61 RID: 8033 RVA: 0x0006E7EB File Offset: 0x0006C9EB
			public MethodInfo Comparison
			{
				get
				{
					return this._node.Comparison;
				}
			}

			// Token: 0x17000618 RID: 1560
			// (get) Token: 0x06001F62 RID: 8034 RVA: 0x0006E7F8 File Offset: 0x0006C9F8
			public string DebugView
			{
				get
				{
					return this._node.DebugView;
				}
			}

			// Token: 0x17000619 RID: 1561
			// (get) Token: 0x06001F63 RID: 8035 RVA: 0x0006E805 File Offset: 0x0006CA05
			public Expression DefaultBody
			{
				get
				{
					return this._node.DefaultBody;
				}
			}

			// Token: 0x1700061A RID: 1562
			// (get) Token: 0x06001F64 RID: 8036 RVA: 0x0006E812 File Offset: 0x0006CA12
			public ExpressionType NodeType
			{
				get
				{
					return this._node.NodeType;
				}
			}

			// Token: 0x1700061B RID: 1563
			// (get) Token: 0x06001F65 RID: 8037 RVA: 0x0006E81F File Offset: 0x0006CA1F
			public Expression SwitchValue
			{
				get
				{
					return this._node.SwitchValue;
				}
			}

			// Token: 0x1700061C RID: 1564
			// (get) Token: 0x06001F66 RID: 8038 RVA: 0x0006E82C File Offset: 0x0006CA2C
			public Type Type
			{
				get
				{
					return this._node.Type;
				}
			}

			// Token: 0x040012A3 RID: 4771
			private readonly SwitchExpression _node;
		}

		// Token: 0x0200043C RID: 1084
		internal class TryExpressionProxy
		{
			// Token: 0x06001F67 RID: 8039 RVA: 0x0006E839 File Offset: 0x0006CA39
			public TryExpressionProxy(TryExpression node)
			{
				this._node = node;
			}

			// Token: 0x1700061D RID: 1565
			// (get) Token: 0x06001F68 RID: 8040 RVA: 0x0006E848 File Offset: 0x0006CA48
			public Expression Body
			{
				get
				{
					return this._node.Body;
				}
			}

			// Token: 0x1700061E RID: 1566
			// (get) Token: 0x06001F69 RID: 8041 RVA: 0x0006E855 File Offset: 0x0006CA55
			public bool CanReduce
			{
				get
				{
					return this._node.CanReduce;
				}
			}

			// Token: 0x1700061F RID: 1567
			// (get) Token: 0x06001F6A RID: 8042 RVA: 0x0006E862 File Offset: 0x0006CA62
			public string DebugView
			{
				get
				{
					return this._node.DebugView;
				}
			}

			// Token: 0x17000620 RID: 1568
			// (get) Token: 0x06001F6B RID: 8043 RVA: 0x0006E86F File Offset: 0x0006CA6F
			public Expression Fault
			{
				get
				{
					return this._node.Fault;
				}
			}

			// Token: 0x17000621 RID: 1569
			// (get) Token: 0x06001F6C RID: 8044 RVA: 0x0006E87C File Offset: 0x0006CA7C
			public Expression Finally
			{
				get
				{
					return this._node.Finally;
				}
			}

			// Token: 0x17000622 RID: 1570
			// (get) Token: 0x06001F6D RID: 8045 RVA: 0x0006E889 File Offset: 0x0006CA89
			public ReadOnlyCollection<CatchBlock> Handlers
			{
				get
				{
					return this._node.Handlers;
				}
			}

			// Token: 0x17000623 RID: 1571
			// (get) Token: 0x06001F6E RID: 8046 RVA: 0x0006E896 File Offset: 0x0006CA96
			public ExpressionType NodeType
			{
				get
				{
					return this._node.NodeType;
				}
			}

			// Token: 0x17000624 RID: 1572
			// (get) Token: 0x06001F6F RID: 8047 RVA: 0x0006E8A3 File Offset: 0x0006CAA3
			public Type Type
			{
				get
				{
					return this._node.Type;
				}
			}

			// Token: 0x040012A4 RID: 4772
			private readonly TryExpression _node;
		}

		// Token: 0x0200043D RID: 1085
		internal class TypeBinaryExpressionProxy
		{
			// Token: 0x06001F70 RID: 8048 RVA: 0x0006E8B0 File Offset: 0x0006CAB0
			public TypeBinaryExpressionProxy(TypeBinaryExpression node)
			{
				this._node = node;
			}

			// Token: 0x17000625 RID: 1573
			// (get) Token: 0x06001F71 RID: 8049 RVA: 0x0006E8BF File Offset: 0x0006CABF
			public bool CanReduce
			{
				get
				{
					return this._node.CanReduce;
				}
			}

			// Token: 0x17000626 RID: 1574
			// (get) Token: 0x06001F72 RID: 8050 RVA: 0x0006E8CC File Offset: 0x0006CACC
			public string DebugView
			{
				get
				{
					return this._node.DebugView;
				}
			}

			// Token: 0x17000627 RID: 1575
			// (get) Token: 0x06001F73 RID: 8051 RVA: 0x0006E8D9 File Offset: 0x0006CAD9
			public Expression Expression
			{
				get
				{
					return this._node.Expression;
				}
			}

			// Token: 0x17000628 RID: 1576
			// (get) Token: 0x06001F74 RID: 8052 RVA: 0x0006E8E6 File Offset: 0x0006CAE6
			public ExpressionType NodeType
			{
				get
				{
					return this._node.NodeType;
				}
			}

			// Token: 0x17000629 RID: 1577
			// (get) Token: 0x06001F75 RID: 8053 RVA: 0x0006E8F3 File Offset: 0x0006CAF3
			public Type Type
			{
				get
				{
					return this._node.Type;
				}
			}

			// Token: 0x1700062A RID: 1578
			// (get) Token: 0x06001F76 RID: 8054 RVA: 0x0006E900 File Offset: 0x0006CB00
			public Type TypeOperand
			{
				get
				{
					return this._node.TypeOperand;
				}
			}

			// Token: 0x040012A5 RID: 4773
			private readonly TypeBinaryExpression _node;
		}

		// Token: 0x0200043E RID: 1086
		internal class UnaryExpressionProxy
		{
			// Token: 0x06001F77 RID: 8055 RVA: 0x0006E90D File Offset: 0x0006CB0D
			public UnaryExpressionProxy(UnaryExpression node)
			{
				this._node = node;
			}

			// Token: 0x1700062B RID: 1579
			// (get) Token: 0x06001F78 RID: 8056 RVA: 0x0006E91C File Offset: 0x0006CB1C
			public bool CanReduce
			{
				get
				{
					return this._node.CanReduce;
				}
			}

			// Token: 0x1700062C RID: 1580
			// (get) Token: 0x06001F79 RID: 8057 RVA: 0x0006E929 File Offset: 0x0006CB29
			public string DebugView
			{
				get
				{
					return this._node.DebugView;
				}
			}

			// Token: 0x1700062D RID: 1581
			// (get) Token: 0x06001F7A RID: 8058 RVA: 0x0006E936 File Offset: 0x0006CB36
			public bool IsLifted
			{
				get
				{
					return this._node.IsLifted;
				}
			}

			// Token: 0x1700062E RID: 1582
			// (get) Token: 0x06001F7B RID: 8059 RVA: 0x0006E943 File Offset: 0x0006CB43
			public bool IsLiftedToNull
			{
				get
				{
					return this._node.IsLiftedToNull;
				}
			}

			// Token: 0x1700062F RID: 1583
			// (get) Token: 0x06001F7C RID: 8060 RVA: 0x0006E950 File Offset: 0x0006CB50
			public MethodInfo Method
			{
				get
				{
					return this._node.Method;
				}
			}

			// Token: 0x17000630 RID: 1584
			// (get) Token: 0x06001F7D RID: 8061 RVA: 0x0006E95D File Offset: 0x0006CB5D
			public ExpressionType NodeType
			{
				get
				{
					return this._node.NodeType;
				}
			}

			// Token: 0x17000631 RID: 1585
			// (get) Token: 0x06001F7E RID: 8062 RVA: 0x0006E96A File Offset: 0x0006CB6A
			public Expression Operand
			{
				get
				{
					return this._node.Operand;
				}
			}

			// Token: 0x17000632 RID: 1586
			// (get) Token: 0x06001F7F RID: 8063 RVA: 0x0006E977 File Offset: 0x0006CB77
			public Type Type
			{
				get
				{
					return this._node.Type;
				}
			}

			// Token: 0x040012A6 RID: 4774
			private readonly UnaryExpression _node;
		}
	}
}
