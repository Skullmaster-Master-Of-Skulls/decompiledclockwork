using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;
using System.Globalization;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Linq.Expressions.Compiler
{
	// Token: 0x02000283 RID: 643
	internal sealed class LambdaCompiler
	{
		// Token: 0x060016FF RID: 5887 RVA: 0x0004D525 File Offset: 0x0004B725
		private void EmitAddress(Expression node, Type type)
		{
			this.EmitAddress(node, type, LambdaCompiler.CompilationFlags.EmitExpressionStart);
		}

		// Token: 0x06001700 RID: 5888 RVA: 0x0004D530 File Offset: 0x0004B730
		private void EmitAddress(Expression node, Type type, LambdaCompiler.CompilationFlags flags)
		{
			bool flag = (flags & LambdaCompiler.CompilationFlags.EmitExpressionStartMask) == LambdaCompiler.CompilationFlags.EmitExpressionStart;
			LambdaCompiler.CompilationFlags flags2 = flag ? this.EmitExpressionStart(node) : LambdaCompiler.CompilationFlags.EmitNoExpressionStart;
			ExpressionType nodeType = node.NodeType;
			if (nodeType <= ExpressionType.MemberAccess)
			{
				if (nodeType == ExpressionType.ArrayIndex)
				{
					this.AddressOf((BinaryExpression)node, type);
					goto IL_A2;
				}
				if (nodeType == ExpressionType.Call)
				{
					this.AddressOf((MethodCallExpression)node, type);
					goto IL_A2;
				}
				if (nodeType == ExpressionType.MemberAccess)
				{
					this.AddressOf((MemberExpression)node, type);
					goto IL_A2;
				}
			}
			else
			{
				if (nodeType == ExpressionType.Parameter)
				{
					this.AddressOf((ParameterExpression)node, type);
					goto IL_A2;
				}
				if (nodeType == ExpressionType.Index)
				{
					this.AddressOf((IndexExpression)node, type);
					goto IL_A2;
				}
				if (nodeType == ExpressionType.Unbox)
				{
					this.AddressOf((UnaryExpression)node, type);
					goto IL_A2;
				}
			}
			this.EmitExpressionAddress(node, type);
			IL_A2:
			if (flag)
			{
				this.EmitExpressionEnd(flags2);
			}
		}

		// Token: 0x06001701 RID: 5889 RVA: 0x0004D5EC File Offset: 0x0004B7EC
		private void AddressOf(BinaryExpression node, Type type)
		{
			if (TypeUtils.AreEquivalent(type, node.Type))
			{
				this.EmitExpression(node.Left);
				this.EmitExpression(node.Right);
				Type type2 = node.Right.Type;
				if (type2.IsNullableType())
				{
					LocalBuilder local = this.GetLocal(type2);
					this._ilg.Emit(OpCodes.Stloc, local);
					this._ilg.Emit(OpCodes.Ldloca, local);
					this._ilg.EmitGetValue(type2);
					this.FreeLocal(local);
				}
				Type nonNullableType = type2.GetNonNullableType();
				if (nonNullableType != typeof(int))
				{
					this._ilg.EmitConvertToType(nonNullableType, typeof(int), true);
				}
				this._ilg.Emit(OpCodes.Ldelema, node.Type);
				return;
			}
			this.EmitExpressionAddress(node, type);
		}

		// Token: 0x06001702 RID: 5890 RVA: 0x0004D6C2 File Offset: 0x0004B8C2
		private void AddressOf(ParameterExpression node, Type type)
		{
			if (!TypeUtils.AreEquivalent(type, node.Type))
			{
				this.EmitExpressionAddress(node, type);
				return;
			}
			if (node.IsByRef)
			{
				this._scope.EmitGet(node);
				return;
			}
			this._scope.EmitAddressOf(node);
		}

		// Token: 0x06001703 RID: 5891 RVA: 0x0004D6FC File Offset: 0x0004B8FC
		private void AddressOf(MemberExpression node, Type type)
		{
			if (TypeUtils.AreEquivalent(type, node.Type))
			{
				Type objectType = null;
				if (node.Expression != null)
				{
					this.EmitInstance(node.Expression, objectType = node.Expression.Type);
				}
				this.EmitMemberAddress(node.Member, objectType);
				return;
			}
			this.EmitExpressionAddress(node, type);
		}

		// Token: 0x06001704 RID: 5892 RVA: 0x0004D750 File Offset: 0x0004B950
		private void EmitMemberAddress(MemberInfo member, Type objectType)
		{
			if (member.MemberType == MemberTypes.Field)
			{
				FieldInfo fieldInfo = (FieldInfo)member;
				if (!fieldInfo.IsLiteral && !fieldInfo.IsInitOnly)
				{
					this._ilg.EmitFieldAddress(fieldInfo);
					return;
				}
			}
			this.EmitMemberGet(member, objectType);
			LocalBuilder local = this.GetLocal(LambdaCompiler.GetMemberType(member));
			this._ilg.Emit(OpCodes.Stloc, local);
			this._ilg.Emit(OpCodes.Ldloca, local);
		}

		// Token: 0x06001705 RID: 5893 RVA: 0x0004D7C4 File Offset: 0x0004B9C4
		private void AddressOf(MethodCallExpression node, Type type)
		{
			if (!node.Method.IsStatic && node.Object.Type.IsArray && node.Method == node.Object.Type.GetMethod("Get", BindingFlags.Instance | BindingFlags.Public))
			{
				MethodInfo method = node.Object.Type.GetMethod("Address", BindingFlags.Instance | BindingFlags.Public);
				this.EmitMethodCall(node.Object, method, node);
				return;
			}
			this.EmitExpressionAddress(node, type);
		}

		// Token: 0x06001706 RID: 5894 RVA: 0x0004D844 File Offset: 0x0004BA44
		private void AddressOf(IndexExpression node, Type type)
		{
			if (!TypeUtils.AreEquivalent(type, node.Type) || node.Indexer != null)
			{
				this.EmitExpressionAddress(node, type);
				return;
			}
			if (node.Arguments.Count == 1)
			{
				this.EmitExpression(node.Object);
				this.EmitExpression(node.Arguments[0]);
				this._ilg.Emit(OpCodes.Ldelema, node.Type);
				return;
			}
			MethodInfo method = node.Object.Type.GetMethod("Address", BindingFlags.Instance | BindingFlags.Public);
			this.EmitMethodCall(node.Object, method, node);
		}

		// Token: 0x06001707 RID: 5895 RVA: 0x0004D8DF File Offset: 0x0004BADF
		private void AddressOf(UnaryExpression node, Type type)
		{
			this.EmitExpression(node.Operand);
			this._ilg.Emit(OpCodes.Unbox, type);
		}

		// Token: 0x06001708 RID: 5896 RVA: 0x0004D900 File Offset: 0x0004BB00
		private void EmitExpressionAddress(Expression node, Type type)
		{
			this.EmitExpression(node, LambdaCompiler.CompilationFlags.EmitNoExpressionStart | LambdaCompiler.CompilationFlags.EmitAsNoTail);
			LocalBuilder local = this.GetLocal(type);
			this._ilg.Emit(OpCodes.Stloc, local);
			this._ilg.Emit(OpCodes.Ldloca, local);
		}

		// Token: 0x06001709 RID: 5897 RVA: 0x0004D944 File Offset: 0x0004BB44
		private LambdaCompiler.WriteBack EmitAddressWriteBack(Expression node, Type type)
		{
			LambdaCompiler.CompilationFlags flags = this.EmitExpressionStart(node);
			LambdaCompiler.WriteBack writeBack = null;
			if (TypeUtils.AreEquivalent(type, node.Type))
			{
				ExpressionType nodeType = node.NodeType;
				if (nodeType != ExpressionType.MemberAccess)
				{
					if (nodeType == ExpressionType.Index)
					{
						writeBack = this.AddressOfWriteBack((IndexExpression)node);
					}
				}
				else
				{
					writeBack = this.AddressOfWriteBack((MemberExpression)node);
				}
			}
			if (writeBack == null)
			{
				this.EmitAddress(node, type, LambdaCompiler.CompilationFlags.EmitNoExpressionStart | LambdaCompiler.CompilationFlags.EmitAsNoTail);
			}
			this.EmitExpressionEnd(flags);
			return writeBack;
		}

		// Token: 0x0600170A RID: 5898 RVA: 0x0004D9B0 File Offset: 0x0004BBB0
		private LambdaCompiler.WriteBack AddressOfWriteBack(MemberExpression node)
		{
			if (node.Member.MemberType != MemberTypes.Property || !((PropertyInfo)node.Member).CanWrite)
			{
				return null;
			}
			LocalBuilder instanceLocal = null;
			Type instanceType = null;
			if (node.Expression != null)
			{
				this.EmitInstance(node.Expression, instanceType = node.Expression.Type);
				this._ilg.Emit(OpCodes.Dup);
				this._ilg.Emit(OpCodes.Stloc, instanceLocal = this.GetLocal(instanceType));
			}
			PropertyInfo pi = (PropertyInfo)node.Member;
			this.EmitCall(instanceType, pi.GetGetMethod(true));
			LocalBuilder valueLocal = this.GetLocal(node.Type);
			this._ilg.Emit(OpCodes.Stloc, valueLocal);
			this._ilg.Emit(OpCodes.Ldloca, valueLocal);
			return delegate()
			{
				if (instanceLocal != null)
				{
					this._ilg.Emit(OpCodes.Ldloc, instanceLocal);
					this.FreeLocal(instanceLocal);
				}
				this._ilg.Emit(OpCodes.Ldloc, valueLocal);
				this.FreeLocal(valueLocal);
				this.EmitCall(instanceType, pi.GetSetMethod(true));
			};
		}

		// Token: 0x0600170B RID: 5899 RVA: 0x0004DACC File Offset: 0x0004BCCC
		private LambdaCompiler.WriteBack AddressOfWriteBack(IndexExpression node)
		{
			if (node.Indexer == null || !node.Indexer.CanWrite)
			{
				return null;
			}
			LocalBuilder instanceLocal = null;
			Type instanceType = null;
			if (node.Object != null)
			{
				this.EmitInstance(node.Object, instanceType = node.Object.Type);
				this._ilg.Emit(OpCodes.Dup);
				this._ilg.Emit(OpCodes.Stloc, instanceLocal = this.GetLocal(instanceType));
			}
			List<LocalBuilder> args = new List<LocalBuilder>();
			foreach (Expression expression in node.Arguments)
			{
				this.EmitExpression(expression);
				LocalBuilder local = this.GetLocal(expression.Type);
				this._ilg.Emit(OpCodes.Dup);
				this._ilg.Emit(OpCodes.Stloc, local);
				args.Add(local);
			}
			this.EmitGetIndexCall(node, instanceType);
			LocalBuilder valueLocal = this.GetLocal(node.Type);
			this._ilg.Emit(OpCodes.Stloc, valueLocal);
			this._ilg.Emit(OpCodes.Ldloca, valueLocal);
			return delegate()
			{
				if (instanceLocal != null)
				{
					this._ilg.Emit(OpCodes.Ldloc, instanceLocal);
					this.FreeLocal(instanceLocal);
				}
				foreach (LocalBuilder local2 in args)
				{
					this._ilg.Emit(OpCodes.Ldloc, local2);
					this.FreeLocal(local2);
				}
				this._ilg.Emit(OpCodes.Ldloc, valueLocal);
				this.FreeLocal(valueLocal);
				this.EmitSetIndexCall(node, instanceType);
			};
		}

		// Token: 0x0600170C RID: 5900 RVA: 0x0004DC80 File Offset: 0x0004BE80
		private void EmitBinaryExpression(Expression expr)
		{
			this.EmitBinaryExpression(expr, LambdaCompiler.CompilationFlags.EmitAsNoTail);
		}

		// Token: 0x0600170D RID: 5901 RVA: 0x0004DC90 File Offset: 0x0004BE90
		private void EmitBinaryExpression(Expression expr, LambdaCompiler.CompilationFlags flags)
		{
			BinaryExpression binaryExpression = (BinaryExpression)expr;
			if (binaryExpression.Method != null)
			{
				this.EmitBinaryMethod(binaryExpression, flags);
				return;
			}
			if ((binaryExpression.NodeType == ExpressionType.Equal || binaryExpression.NodeType == ExpressionType.NotEqual) && (binaryExpression.Type == typeof(bool) || binaryExpression.Type == typeof(bool?)))
			{
				if (ConstantCheck.IsNull(binaryExpression.Left) && !ConstantCheck.IsNull(binaryExpression.Right) && binaryExpression.Right.Type.IsNullableType())
				{
					this.EmitNullEquality(binaryExpression.NodeType, binaryExpression.Right, binaryExpression.IsLiftedToNull);
					return;
				}
				if (ConstantCheck.IsNull(binaryExpression.Right) && !ConstantCheck.IsNull(binaryExpression.Left) && binaryExpression.Left.Type.IsNullableType())
				{
					this.EmitNullEquality(binaryExpression.NodeType, binaryExpression.Left, binaryExpression.IsLiftedToNull);
					return;
				}
				this.EmitExpression(LambdaCompiler.GetEqualityOperand(binaryExpression.Left));
				this.EmitExpression(LambdaCompiler.GetEqualityOperand(binaryExpression.Right));
			}
			else
			{
				this.EmitExpression(binaryExpression.Left);
				this.EmitExpression(binaryExpression.Right);
			}
			this.EmitBinaryOperator(binaryExpression.NodeType, binaryExpression.Left.Type, binaryExpression.Right.Type, binaryExpression.Type, binaryExpression.IsLiftedToNull);
		}

		// Token: 0x0600170E RID: 5902 RVA: 0x0004DDF8 File Offset: 0x0004BFF8
		private void EmitNullEquality(ExpressionType op, Expression e, bool isLiftedToNull)
		{
			if (isLiftedToNull)
			{
				this.EmitExpressionAsVoid(e);
				this._ilg.EmitDefault(typeof(bool?));
				return;
			}
			this.EmitAddress(e, e.Type);
			this._ilg.EmitHasValue(e.Type);
			if (op == ExpressionType.Equal)
			{
				this._ilg.Emit(OpCodes.Ldc_I4_0);
				this._ilg.Emit(OpCodes.Ceq);
			}
		}

		// Token: 0x0600170F RID: 5903 RVA: 0x0004DE68 File Offset: 0x0004C068
		private void EmitBinaryMethod(BinaryExpression b, LambdaCompiler.CompilationFlags flags)
		{
			if (b.IsLifted)
			{
				ParameterExpression parameterExpression = Expression.Variable(b.Left.Type.GetNonNullableType(), null);
				ParameterExpression parameterExpression2 = Expression.Variable(b.Right.Type.GetNonNullableType(), null);
				MethodCallExpression methodCallExpression = Expression.Call(null, b.Method, parameterExpression, parameterExpression2);
				Type resultType;
				if (b.IsLiftedToNull)
				{
					resultType = TypeUtils.GetNullableType(methodCallExpression.Type);
				}
				else
				{
					ExpressionType nodeType = b.NodeType;
					switch (nodeType)
					{
					case ExpressionType.Equal:
					case ExpressionType.GreaterThan:
					case ExpressionType.GreaterThanOrEqual:
					case ExpressionType.LessThan:
					case ExpressionType.LessThanOrEqual:
						break;
					case ExpressionType.ExclusiveOr:
					case ExpressionType.Invoke:
					case ExpressionType.Lambda:
					case ExpressionType.LeftShift:
						goto IL_C6;
					default:
						if (nodeType != ExpressionType.NotEqual)
						{
							goto IL_C6;
						}
						break;
					}
					if (methodCallExpression.Type != typeof(bool))
					{
						throw Error.ArgumentMustBeBoolean();
					}
					resultType = typeof(bool);
					goto IL_D2;
					IL_C6:
					resultType = TypeUtils.GetNullableType(methodCallExpression.Type);
				}
				IL_D2:
				ParameterExpression[] array = new ParameterExpression[]
				{
					parameterExpression,
					parameterExpression2
				};
				Expression[] array2 = new Expression[]
				{
					b.Left,
					b.Right
				};
				LambdaCompiler.ValidateLift(array, array2);
				this.EmitLift(b.NodeType, resultType, methodCallExpression, array, array2);
				return;
			}
			this.EmitMethodCallExpression(Expression.Call(null, b.Method, b.Left, b.Right), flags);
		}

		// Token: 0x06001710 RID: 5904 RVA: 0x0004DFAC File Offset: 0x0004C1AC
		private void EmitBinaryOperator(ExpressionType op, Type leftType, Type rightType, Type resultType, bool liftedToNull)
		{
			bool flag = leftType.IsNullableType();
			bool flag2 = rightType.IsNullableType();
			if (op != ExpressionType.ArrayIndex)
			{
				if (op == ExpressionType.Coalesce)
				{
					throw Error.UnexpectedCoalesceOperator();
				}
				if (flag || flag2)
				{
					this.EmitLiftedBinaryOp(op, leftType, rightType, resultType, liftedToNull);
					return;
				}
				this.EmitUnliftedBinaryOp(op, leftType, rightType);
				this.EmitConvertArithmeticResult(op, resultType);
				return;
			}
			else
			{
				if (rightType != typeof(int))
				{
					throw ContractUtils.Unreachable;
				}
				this._ilg.EmitLoadElement(leftType.GetElementType());
				return;
			}
		}

		// Token: 0x06001711 RID: 5905 RVA: 0x0004E028 File Offset: 0x0004C228
		private void EmitUnliftedBinaryOp(ExpressionType op, Type leftType, Type rightType)
		{
			if (op == ExpressionType.Equal || op == ExpressionType.NotEqual)
			{
				this.EmitUnliftedEquality(op, leftType);
				return;
			}
			if (!leftType.IsPrimitive)
			{
				throw Error.OperatorNotImplementedForType(op, leftType);
			}
			switch (op)
			{
			case ExpressionType.Add:
				this._ilg.Emit(OpCodes.Add);
				return;
			case ExpressionType.AddChecked:
				if (TypeUtils.IsFloatingPoint(leftType))
				{
					this._ilg.Emit(OpCodes.Add);
					return;
				}
				if (TypeUtils.IsUnsigned(leftType))
				{
					this._ilg.Emit(OpCodes.Add_Ovf_Un);
					return;
				}
				this._ilg.Emit(OpCodes.Add_Ovf);
				return;
			case ExpressionType.And:
			case ExpressionType.AndAlso:
				this._ilg.Emit(OpCodes.And);
				return;
			default:
				switch (op)
				{
				case ExpressionType.Divide:
					if (TypeUtils.IsUnsigned(leftType))
					{
						this._ilg.Emit(OpCodes.Div_Un);
						return;
					}
					this._ilg.Emit(OpCodes.Div);
					return;
				case ExpressionType.Equal:
				case ExpressionType.Invoke:
				case ExpressionType.Lambda:
				case ExpressionType.ListInit:
				case ExpressionType.MemberAccess:
				case ExpressionType.MemberInit:
					break;
				case ExpressionType.ExclusiveOr:
					this._ilg.Emit(OpCodes.Xor);
					return;
				case ExpressionType.GreaterThan:
					if (TypeUtils.IsUnsigned(leftType))
					{
						this._ilg.Emit(OpCodes.Cgt_Un);
						return;
					}
					this._ilg.Emit(OpCodes.Cgt);
					return;
				case ExpressionType.GreaterThanOrEqual:
				{
					Label label = this._ilg.DefineLabel();
					Label label2 = this._ilg.DefineLabel();
					if (TypeUtils.IsUnsigned(leftType))
					{
						this._ilg.Emit(OpCodes.Bge_Un_S, label);
					}
					else
					{
						this._ilg.Emit(OpCodes.Bge_S, label);
					}
					this._ilg.Emit(OpCodes.Ldc_I4_0);
					this._ilg.Emit(OpCodes.Br_S, label2);
					this._ilg.MarkLabel(label);
					this._ilg.Emit(OpCodes.Ldc_I4_1);
					this._ilg.MarkLabel(label2);
					return;
				}
				case ExpressionType.LeftShift:
					if (rightType != typeof(int))
					{
						throw ContractUtils.Unreachable;
					}
					this._ilg.Emit(OpCodes.Shl);
					return;
				case ExpressionType.LessThan:
					if (TypeUtils.IsUnsigned(leftType))
					{
						this._ilg.Emit(OpCodes.Clt_Un);
						return;
					}
					this._ilg.Emit(OpCodes.Clt);
					return;
				case ExpressionType.LessThanOrEqual:
				{
					Label label3 = this._ilg.DefineLabel();
					Label label4 = this._ilg.DefineLabel();
					if (TypeUtils.IsUnsigned(leftType))
					{
						this._ilg.Emit(OpCodes.Ble_Un_S, label3);
					}
					else
					{
						this._ilg.Emit(OpCodes.Ble_S, label3);
					}
					this._ilg.Emit(OpCodes.Ldc_I4_0);
					this._ilg.Emit(OpCodes.Br_S, label4);
					this._ilg.MarkLabel(label3);
					this._ilg.Emit(OpCodes.Ldc_I4_1);
					this._ilg.MarkLabel(label4);
					return;
				}
				case ExpressionType.Modulo:
					if (TypeUtils.IsUnsigned(leftType))
					{
						this._ilg.Emit(OpCodes.Rem_Un);
						return;
					}
					this._ilg.Emit(OpCodes.Rem);
					return;
				case ExpressionType.Multiply:
					this._ilg.Emit(OpCodes.Mul);
					return;
				case ExpressionType.MultiplyChecked:
					if (TypeUtils.IsFloatingPoint(leftType))
					{
						this._ilg.Emit(OpCodes.Mul);
						return;
					}
					if (TypeUtils.IsUnsigned(leftType))
					{
						this._ilg.Emit(OpCodes.Mul_Ovf_Un);
						return;
					}
					this._ilg.Emit(OpCodes.Mul_Ovf);
					return;
				default:
					switch (op)
					{
					case ExpressionType.Or:
					case ExpressionType.OrElse:
						this._ilg.Emit(OpCodes.Or);
						return;
					case ExpressionType.RightShift:
						if (rightType != typeof(int))
						{
							throw ContractUtils.Unreachable;
						}
						if (TypeUtils.IsUnsigned(leftType))
						{
							this._ilg.Emit(OpCodes.Shr_Un);
							return;
						}
						this._ilg.Emit(OpCodes.Shr);
						return;
					case ExpressionType.Subtract:
						this._ilg.Emit(OpCodes.Sub);
						return;
					case ExpressionType.SubtractChecked:
						if (TypeUtils.IsFloatingPoint(leftType))
						{
							this._ilg.Emit(OpCodes.Sub);
							return;
						}
						if (TypeUtils.IsUnsigned(leftType))
						{
							this._ilg.Emit(OpCodes.Sub_Ovf_Un);
							return;
						}
						this._ilg.Emit(OpCodes.Sub_Ovf);
						return;
					}
					break;
				}
				throw Error.UnhandledBinary(op);
			}
		}

		// Token: 0x06001712 RID: 5906 RVA: 0x0004E454 File Offset: 0x0004C654
		private void EmitConvertArithmeticResult(ExpressionType op, Type resultType)
		{
			switch (Type.GetTypeCode(resultType))
			{
			case TypeCode.SByte:
				this._ilg.Emit(LambdaCompiler.IsChecked(op) ? OpCodes.Conv_Ovf_I1 : OpCodes.Conv_I1);
				return;
			case TypeCode.Byte:
				this._ilg.Emit(LambdaCompiler.IsChecked(op) ? OpCodes.Conv_Ovf_U1 : OpCodes.Conv_U1);
				return;
			case TypeCode.Int16:
				this._ilg.Emit(LambdaCompiler.IsChecked(op) ? OpCodes.Conv_Ovf_I2 : OpCodes.Conv_I2);
				return;
			case TypeCode.UInt16:
				this._ilg.Emit(LambdaCompiler.IsChecked(op) ? OpCodes.Conv_Ovf_U2 : OpCodes.Conv_U2);
				return;
			default:
				return;
			}
		}

		// Token: 0x06001713 RID: 5907 RVA: 0x0004E500 File Offset: 0x0004C700
		private void EmitUnliftedEquality(ExpressionType op, Type type)
		{
			if (!type.IsPrimitive && type.IsValueType && !type.IsEnum)
			{
				throw Error.OperatorNotImplementedForType(op, type);
			}
			this._ilg.Emit(OpCodes.Ceq);
			if (op == ExpressionType.NotEqual)
			{
				this._ilg.Emit(OpCodes.Ldc_I4_0);
				this._ilg.Emit(OpCodes.Ceq);
			}
		}

		// Token: 0x06001714 RID: 5908 RVA: 0x0004E568 File Offset: 0x0004C768
		private void EmitLiftedBinaryOp(ExpressionType op, Type leftType, Type rightType, Type resultType, bool liftedToNull)
		{
			switch (op)
			{
			case ExpressionType.Add:
			case ExpressionType.AddChecked:
			case ExpressionType.Divide:
			case ExpressionType.ExclusiveOr:
			case ExpressionType.LeftShift:
			case ExpressionType.Modulo:
			case ExpressionType.Multiply:
			case ExpressionType.MultiplyChecked:
				break;
			case ExpressionType.And:
				if (leftType == typeof(bool?))
				{
					this.EmitLiftedBooleanAnd();
					return;
				}
				this.EmitLiftedBinaryArithmetic(op, leftType, rightType, resultType);
				return;
			case ExpressionType.AndAlso:
			case ExpressionType.ArrayLength:
			case ExpressionType.ArrayIndex:
			case ExpressionType.Call:
			case ExpressionType.Coalesce:
			case ExpressionType.Conditional:
			case ExpressionType.Constant:
			case ExpressionType.Convert:
			case ExpressionType.ConvertChecked:
			case ExpressionType.Invoke:
			case ExpressionType.Lambda:
			case ExpressionType.ListInit:
			case ExpressionType.MemberAccess:
			case ExpressionType.MemberInit:
				goto IL_109;
			case ExpressionType.Equal:
			case ExpressionType.GreaterThan:
			case ExpressionType.GreaterThanOrEqual:
			case ExpressionType.LessThan:
			case ExpressionType.LessThanOrEqual:
				goto IL_FB;
			default:
				switch (op)
				{
				case ExpressionType.NotEqual:
					goto IL_FB;
				case ExpressionType.Or:
					if (leftType == typeof(bool?))
					{
						this.EmitLiftedBooleanOr();
						return;
					}
					this.EmitLiftedBinaryArithmetic(op, leftType, rightType, resultType);
					return;
				case ExpressionType.OrElse:
				case ExpressionType.Parameter:
				case ExpressionType.Power:
				case ExpressionType.Quote:
					goto IL_109;
				case ExpressionType.RightShift:
				case ExpressionType.Subtract:
				case ExpressionType.SubtractChecked:
					break;
				default:
					goto IL_109;
				}
				break;
			}
			this.EmitLiftedBinaryArithmetic(op, leftType, rightType, resultType);
			return;
			IL_FB:
			this.EmitLiftedRelational(op, leftType, rightType, resultType, liftedToNull);
			return;
			IL_109:
			throw ContractUtils.Unreachable;
		}

		// Token: 0x06001715 RID: 5909 RVA: 0x0004E684 File Offset: 0x0004C884
		private void EmitLiftedRelational(ExpressionType op, Type leftType, Type rightType, Type resultType, bool liftedToNull)
		{
			Label label = this._ilg.DefineLabel();
			LocalBuilder local = this.GetLocal(leftType);
			LocalBuilder local2 = this.GetLocal(rightType);
			this._ilg.Emit(OpCodes.Stloc, local2);
			this._ilg.Emit(OpCodes.Stloc, local);
			if (op == ExpressionType.Equal)
			{
				this._ilg.Emit(OpCodes.Ldloca, local);
				this._ilg.EmitHasValue(leftType);
				this._ilg.Emit(OpCodes.Ldc_I4_0);
				this._ilg.Emit(OpCodes.Ceq);
				this._ilg.Emit(OpCodes.Ldloca, local2);
				this._ilg.EmitHasValue(rightType);
				this._ilg.Emit(OpCodes.Ldc_I4_0);
				this._ilg.Emit(OpCodes.Ceq);
				this._ilg.Emit(OpCodes.And);
				this._ilg.Emit(OpCodes.Dup);
				this._ilg.Emit(OpCodes.Brtrue_S, label);
				this._ilg.Emit(OpCodes.Pop);
				this._ilg.Emit(OpCodes.Ldloca, local);
				this._ilg.EmitHasValue(leftType);
				this._ilg.Emit(OpCodes.Ldloca, local2);
				this._ilg.EmitHasValue(rightType);
				this._ilg.Emit(OpCodes.And);
				this._ilg.Emit(OpCodes.Dup);
				this._ilg.Emit(OpCodes.Brfalse_S, label);
				this._ilg.Emit(OpCodes.Pop);
			}
			else if (op == ExpressionType.NotEqual)
			{
				this._ilg.Emit(OpCodes.Ldloca, local);
				this._ilg.EmitHasValue(leftType);
				this._ilg.Emit(OpCodes.Ldloca, local2);
				this._ilg.EmitHasValue(rightType);
				this._ilg.Emit(OpCodes.Or);
				this._ilg.Emit(OpCodes.Dup);
				this._ilg.Emit(OpCodes.Brfalse_S, label);
				this._ilg.Emit(OpCodes.Pop);
				this._ilg.Emit(OpCodes.Ldloca, local);
				this._ilg.EmitHasValue(leftType);
				this._ilg.Emit(OpCodes.Ldc_I4_0);
				this._ilg.Emit(OpCodes.Ceq);
				this._ilg.Emit(OpCodes.Ldloca, local2);
				this._ilg.EmitHasValue(rightType);
				this._ilg.Emit(OpCodes.Ldc_I4_0);
				this._ilg.Emit(OpCodes.Ceq);
				this._ilg.Emit(OpCodes.Or);
				this._ilg.Emit(OpCodes.Dup);
				this._ilg.Emit(OpCodes.Brtrue_S, label);
				this._ilg.Emit(OpCodes.Pop);
			}
			else
			{
				this._ilg.Emit(OpCodes.Ldloca, local);
				this._ilg.EmitHasValue(leftType);
				this._ilg.Emit(OpCodes.Ldloca, local2);
				this._ilg.EmitHasValue(rightType);
				this._ilg.Emit(OpCodes.And);
				this._ilg.Emit(OpCodes.Dup);
				this._ilg.Emit(OpCodes.Brfalse_S, label);
				this._ilg.Emit(OpCodes.Pop);
			}
			this._ilg.Emit(OpCodes.Ldloca, local);
			this._ilg.EmitGetValueOrDefault(leftType);
			this._ilg.Emit(OpCodes.Ldloca, local2);
			this._ilg.EmitGetValueOrDefault(rightType);
			this.FreeLocal(local);
			this.FreeLocal(local2);
			this.EmitBinaryOperator(op, leftType.GetNonNullableType(), rightType.GetNonNullableType(), resultType.GetNonNullableType(), false);
			if (!liftedToNull)
			{
				this._ilg.MarkLabel(label);
			}
			if (!TypeUtils.AreEquivalent(resultType, resultType.GetNonNullableType()))
			{
				this._ilg.EmitConvertToType(resultType.GetNonNullableType(), resultType, true);
			}
			if (liftedToNull)
			{
				Label label2 = this._ilg.DefineLabel();
				this._ilg.Emit(OpCodes.Br, label2);
				this._ilg.MarkLabel(label);
				this._ilg.Emit(OpCodes.Pop);
				this._ilg.Emit(OpCodes.Ldnull);
				this._ilg.Emit(OpCodes.Unbox_Any, resultType);
				this._ilg.MarkLabel(label2);
			}
		}

		// Token: 0x06001716 RID: 5910 RVA: 0x0004EAD0 File Offset: 0x0004CCD0
		private void EmitLiftedBinaryArithmetic(ExpressionType op, Type leftType, Type rightType, Type resultType)
		{
			bool flag = leftType.IsNullableType();
			bool flag2 = rightType.IsNullableType();
			Label label = this._ilg.DefineLabel();
			Label label2 = this._ilg.DefineLabel();
			LocalBuilder local = this.GetLocal(leftType);
			LocalBuilder local2 = this.GetLocal(rightType);
			LocalBuilder local3 = this.GetLocal(resultType);
			this._ilg.Emit(OpCodes.Stloc, local2);
			this._ilg.Emit(OpCodes.Stloc, local);
			if (flag)
			{
				this._ilg.Emit(OpCodes.Ldloca, local);
				this._ilg.EmitHasValue(leftType);
				this._ilg.Emit(OpCodes.Brfalse_S, label);
			}
			if (flag2)
			{
				this._ilg.Emit(OpCodes.Ldloca, local2);
				this._ilg.EmitHasValue(rightType);
				this._ilg.Emit(OpCodes.Brfalse_S, label);
			}
			if (flag)
			{
				this._ilg.Emit(OpCodes.Ldloca, local);
				this._ilg.EmitGetValueOrDefault(leftType);
			}
			else
			{
				this._ilg.Emit(OpCodes.Ldloc, local);
			}
			if (flag2)
			{
				this._ilg.Emit(OpCodes.Ldloca, local2);
				this._ilg.EmitGetValueOrDefault(rightType);
			}
			else
			{
				this._ilg.Emit(OpCodes.Ldloc, local2);
			}
			this.FreeLocal(local);
			this.FreeLocal(local2);
			this.EmitBinaryOperator(op, leftType.GetNonNullableType(), rightType.GetNonNullableType(), resultType.GetNonNullableType(), false);
			ConstructorInfo constructor = resultType.GetConstructor(new Type[]
			{
				resultType.GetNonNullableType()
			});
			this._ilg.Emit(OpCodes.Newobj, constructor);
			this._ilg.Emit(OpCodes.Stloc, local3);
			this._ilg.Emit(OpCodes.Br_S, label2);
			this._ilg.MarkLabel(label);
			this._ilg.Emit(OpCodes.Ldloca, local3);
			this._ilg.Emit(OpCodes.Initobj, resultType);
			this._ilg.MarkLabel(label2);
			this._ilg.Emit(OpCodes.Ldloc, local3);
			this.FreeLocal(local3);
		}

		// Token: 0x06001717 RID: 5911 RVA: 0x0004ECE0 File Offset: 0x0004CEE0
		private void EmitLiftedBooleanAnd()
		{
			Type typeFromHandle = typeof(bool?);
			Label label = this._ilg.DefineLabel();
			Label label2 = this._ilg.DefineLabel();
			Label label3 = this._ilg.DefineLabel();
			Label label4 = this._ilg.DefineLabel();
			Label label5 = this._ilg.DefineLabel();
			LocalBuilder local = this.GetLocal(typeFromHandle);
			LocalBuilder local2 = this.GetLocal(typeFromHandle);
			this._ilg.Emit(OpCodes.Stloc, local2);
			this._ilg.Emit(OpCodes.Stloc, local);
			this._ilg.Emit(OpCodes.Ldloca, local);
			this._ilg.EmitHasValue(typeFromHandle);
			this._ilg.Emit(OpCodes.Brfalse, label);
			this._ilg.Emit(OpCodes.Ldloca, local);
			this._ilg.EmitGetValueOrDefault(typeFromHandle);
			this._ilg.Emit(OpCodes.Ldc_I4_0);
			this._ilg.Emit(OpCodes.Ceq);
			this._ilg.Emit(OpCodes.Brtrue, label2);
			this._ilg.MarkLabel(label);
			this._ilg.Emit(OpCodes.Ldloca, local2);
			this._ilg.EmitHasValue(typeFromHandle);
			this._ilg.Emit(OpCodes.Brfalse_S, label3);
			this._ilg.Emit(OpCodes.Ldloca, local2);
			this.FreeLocal(local2);
			this._ilg.EmitGetValueOrDefault(typeFromHandle);
			this._ilg.Emit(OpCodes.Ldc_I4_0);
			this._ilg.Emit(OpCodes.Ceq);
			this._ilg.Emit(OpCodes.Brtrue_S, label2);
			this._ilg.Emit(OpCodes.Ldloca, local);
			this._ilg.EmitHasValue(typeFromHandle);
			this._ilg.Emit(OpCodes.Brfalse, label3);
			this._ilg.Emit(OpCodes.Ldc_I4_1);
			this._ilg.Emit(OpCodes.Br_S, label4);
			this._ilg.MarkLabel(label2);
			this._ilg.Emit(OpCodes.Ldc_I4_0);
			this._ilg.Emit(OpCodes.Br_S, label4);
			this._ilg.MarkLabel(label4);
			ConstructorInfo constructor = typeFromHandle.GetConstructor(new Type[]
			{
				typeof(bool)
			});
			this._ilg.Emit(OpCodes.Newobj, constructor);
			this._ilg.Emit(OpCodes.Stloc, local);
			this._ilg.Emit(OpCodes.Br, label5);
			this._ilg.MarkLabel(label3);
			this._ilg.Emit(OpCodes.Ldloca, local);
			this._ilg.Emit(OpCodes.Initobj, typeFromHandle);
			this._ilg.MarkLabel(label5);
			this._ilg.Emit(OpCodes.Ldloc, local);
			this.FreeLocal(local);
		}

		// Token: 0x06001718 RID: 5912 RVA: 0x0004EFB0 File Offset: 0x0004D1B0
		private void EmitLiftedBooleanOr()
		{
			Type typeFromHandle = typeof(bool?);
			Label label = this._ilg.DefineLabel();
			Label label2 = this._ilg.DefineLabel();
			Label label3 = this._ilg.DefineLabel();
			Label label4 = this._ilg.DefineLabel();
			Label label5 = this._ilg.DefineLabel();
			LocalBuilder local = this.GetLocal(typeFromHandle);
			LocalBuilder local2 = this.GetLocal(typeFromHandle);
			this._ilg.Emit(OpCodes.Stloc, local2);
			this._ilg.Emit(OpCodes.Stloc, local);
			this._ilg.Emit(OpCodes.Ldloca, local);
			this._ilg.EmitHasValue(typeFromHandle);
			this._ilg.Emit(OpCodes.Brfalse, label);
			this._ilg.Emit(OpCodes.Ldloca, local);
			this._ilg.EmitGetValueOrDefault(typeFromHandle);
			this._ilg.Emit(OpCodes.Ldc_I4_0);
			this._ilg.Emit(OpCodes.Ceq);
			this._ilg.Emit(OpCodes.Brfalse, label2);
			this._ilg.MarkLabel(label);
			this._ilg.Emit(OpCodes.Ldloca, local2);
			this._ilg.EmitHasValue(typeFromHandle);
			this._ilg.Emit(OpCodes.Brfalse_S, label3);
			this._ilg.Emit(OpCodes.Ldloca, local2);
			this.FreeLocal(local2);
			this._ilg.EmitGetValueOrDefault(typeFromHandle);
			this._ilg.Emit(OpCodes.Ldc_I4_0);
			this._ilg.Emit(OpCodes.Ceq);
			this._ilg.Emit(OpCodes.Brfalse_S, label2);
			this._ilg.Emit(OpCodes.Ldloca, local);
			this._ilg.EmitHasValue(typeFromHandle);
			this._ilg.Emit(OpCodes.Brfalse, label3);
			this._ilg.Emit(OpCodes.Ldc_I4_0);
			this._ilg.Emit(OpCodes.Br_S, label4);
			this._ilg.MarkLabel(label2);
			this._ilg.Emit(OpCodes.Ldc_I4_1);
			this._ilg.Emit(OpCodes.Br_S, label4);
			this._ilg.MarkLabel(label4);
			ConstructorInfo constructor = typeFromHandle.GetConstructor(new Type[]
			{
				typeof(bool)
			});
			this._ilg.Emit(OpCodes.Newobj, constructor);
			this._ilg.Emit(OpCodes.Stloc, local);
			this._ilg.Emit(OpCodes.Br, label5);
			this._ilg.MarkLabel(label3);
			this._ilg.Emit(OpCodes.Ldloca, local);
			this._ilg.Emit(OpCodes.Initobj, typeFromHandle);
			this._ilg.MarkLabel(label5);
			this._ilg.Emit(OpCodes.Ldloc, local);
			this.FreeLocal(local);
		}

		// Token: 0x06001719 RID: 5913 RVA: 0x0004F280 File Offset: 0x0004D480
		private LabelInfo EnsureLabel(LabelTarget node)
		{
			LabelInfo result;
			if (!this._labelInfo.TryGetValue(node, out result))
			{
				this._labelInfo.Add(node, result = new LabelInfo(this._ilg, node, false));
			}
			return result;
		}

		// Token: 0x0600171A RID: 5914 RVA: 0x0004F2BC File Offset: 0x0004D4BC
		private LabelInfo ReferenceLabel(LabelTarget node)
		{
			LabelInfo labelInfo = this.EnsureLabel(node);
			labelInfo.Reference(this._labelBlock);
			return labelInfo;
		}

		// Token: 0x0600171B RID: 5915 RVA: 0x0004F2E0 File Offset: 0x0004D4E0
		private LabelInfo DefineLabel(LabelTarget node)
		{
			if (node == null)
			{
				return new LabelInfo(this._ilg, null, false);
			}
			LabelInfo labelInfo = this.EnsureLabel(node);
			labelInfo.Define(this._labelBlock);
			return labelInfo;
		}

		// Token: 0x0600171C RID: 5916 RVA: 0x0004F313 File Offset: 0x0004D513
		private void PushLabelBlock(LabelScopeKind type)
		{
			this._labelBlock = new LabelScopeInfo(this._labelBlock, type);
		}

		// Token: 0x0600171D RID: 5917 RVA: 0x0004F327 File Offset: 0x0004D527
		private void PopLabelBlock(LabelScopeKind kind)
		{
			this._labelBlock = this._labelBlock.Parent;
		}

		// Token: 0x0600171E RID: 5918 RVA: 0x0004F33C File Offset: 0x0004D53C
		private void EmitLabelExpression(Expression expr, LambdaCompiler.CompilationFlags flags)
		{
			LabelExpression labelExpression = (LabelExpression)expr;
			LabelInfo labelInfo = null;
			if (this._labelBlock.Kind == LabelScopeKind.Block)
			{
				this._labelBlock.TryGetLabelInfo(labelExpression.Target, out labelInfo);
				if (labelInfo == null && this._labelBlock.Parent.Kind == LabelScopeKind.Switch)
				{
					this._labelBlock.Parent.TryGetLabelInfo(labelExpression.Target, out labelInfo);
				}
			}
			if (labelInfo == null)
			{
				labelInfo = this.DefineLabel(labelExpression.Target);
			}
			if (labelExpression.DefaultValue != null)
			{
				if (labelExpression.Target.Type == typeof(void))
				{
					this.EmitExpressionAsVoid(labelExpression.DefaultValue, flags);
				}
				else
				{
					flags = LambdaCompiler.UpdateEmitExpressionStartFlag(flags, LambdaCompiler.CompilationFlags.EmitExpressionStart);
					this.EmitExpression(labelExpression.DefaultValue, flags);
				}
			}
			labelInfo.Mark();
		}

		// Token: 0x0600171F RID: 5919 RVA: 0x0004F404 File Offset: 0x0004D604
		private void EmitGotoExpression(Expression expr, LambdaCompiler.CompilationFlags flags)
		{
			GotoExpression gotoExpression = (GotoExpression)expr;
			LabelInfo labelInfo = this.ReferenceLabel(gotoExpression.Target);
			LambdaCompiler.CompilationFlags compilationFlags = flags & LambdaCompiler.CompilationFlags.EmitAsTailCallMask;
			if (compilationFlags != LambdaCompiler.CompilationFlags.EmitAsNoTail)
			{
				compilationFlags = (labelInfo.CanReturn ? LambdaCompiler.CompilationFlags.EmitAsTail : LambdaCompiler.CompilationFlags.EmitAsNoTail);
				flags = LambdaCompiler.UpdateEmitAsTailCallFlag(flags, compilationFlags);
			}
			if (gotoExpression.Value != null)
			{
				if (gotoExpression.Target.Type == typeof(void))
				{
					this.EmitExpressionAsVoid(gotoExpression.Value, flags);
				}
				else
				{
					flags = LambdaCompiler.UpdateEmitExpressionStartFlag(flags, LambdaCompiler.CompilationFlags.EmitExpressionStart);
					this.EmitExpression(gotoExpression.Value, flags);
				}
			}
			labelInfo.EmitJump();
			this.EmitUnreachable(gotoExpression, flags);
		}

		// Token: 0x06001720 RID: 5920 RVA: 0x0004F4AA File Offset: 0x0004D6AA
		private void EmitUnreachable(Expression node, LambdaCompiler.CompilationFlags flags)
		{
			if (node.Type != typeof(void) && (flags & LambdaCompiler.CompilationFlags.EmitAsVoidType) == (LambdaCompiler.CompilationFlags)0)
			{
				this._ilg.EmitDefault(node.Type);
			}
		}

		// Token: 0x06001721 RID: 5921 RVA: 0x0004F4DC File Offset: 0x0004D6DC
		private bool TryPushLabelBlock(Expression node)
		{
			ExpressionType nodeType = node.NodeType;
			if (nodeType <= ExpressionType.Convert)
			{
				if (nodeType == ExpressionType.Conditional)
				{
					goto IL_15F;
				}
				if (nodeType == ExpressionType.Convert)
				{
					if (!(node.Type != typeof(void)))
					{
						this.PushLabelBlock(LabelScopeKind.Statement);
						return true;
					}
				}
			}
			else if (nodeType != ExpressionType.Block)
			{
				switch (nodeType)
				{
				case ExpressionType.Goto:
				case ExpressionType.Loop:
					goto IL_15F;
				case ExpressionType.Label:
					if (this._labelBlock.Kind == LabelScopeKind.Block)
					{
						LabelTarget target = ((LabelExpression)node).Target;
						if (this._labelBlock.ContainsTarget(target))
						{
							return false;
						}
						if (this._labelBlock.Parent.Kind == LabelScopeKind.Switch && this._labelBlock.Parent.ContainsTarget(target))
						{
							return false;
						}
					}
					this.PushLabelBlock(LabelScopeKind.Statement);
					return true;
				case ExpressionType.Switch:
				{
					this.PushLabelBlock(LabelScopeKind.Switch);
					SwitchExpression switchExpression = (SwitchExpression)node;
					foreach (SwitchCase switchCase in switchExpression.Cases)
					{
						this.DefineBlockLabels(switchCase.Body);
					}
					this.DefineBlockLabels(switchExpression.DefaultBody);
					return true;
				}
				}
			}
			else if (!(node is SpilledExpressionBlock))
			{
				this.PushLabelBlock(LabelScopeKind.Block);
				if (this._labelBlock.Parent.Kind != LabelScopeKind.Switch)
				{
					this.DefineBlockLabels(node);
				}
				return true;
			}
			if (this._labelBlock.Kind != LabelScopeKind.Expression)
			{
				this.PushLabelBlock(LabelScopeKind.Expression);
				return true;
			}
			return false;
			IL_15F:
			this.PushLabelBlock(LabelScopeKind.Statement);
			return true;
		}

		// Token: 0x06001722 RID: 5922 RVA: 0x0004F660 File Offset: 0x0004D860
		private void DefineBlockLabels(Expression node)
		{
			BlockExpression blockExpression = node as BlockExpression;
			if (blockExpression == null || blockExpression is SpilledExpressionBlock)
			{
				return;
			}
			int i = 0;
			int expressionCount = blockExpression.ExpressionCount;
			while (i < expressionCount)
			{
				Expression expression = blockExpression.GetExpression(i);
				LabelExpression labelExpression = expression as LabelExpression;
				if (labelExpression != null)
				{
					this.DefineLabel(labelExpression.Target);
				}
				i++;
			}
		}

		// Token: 0x06001723 RID: 5923 RVA: 0x0004F6B8 File Offset: 0x0004D8B8
		private void AddReturnLabel(LambdaExpression lambda)
		{
			Expression expression = lambda.Body;
			ExpressionType nodeType;
			for (;;)
			{
				nodeType = expression.NodeType;
				if (nodeType != ExpressionType.Block)
				{
					break;
				}
				BlockExpression blockExpression = (BlockExpression)expression;
				for (int i = blockExpression.ExpressionCount - 1; i >= 0; i--)
				{
					expression = blockExpression.GetExpression(i);
					if (LambdaCompiler.Significant(expression))
					{
						break;
					}
				}
			}
			if (nodeType != ExpressionType.Label)
			{
				return;
			}
			LabelTarget target = ((LabelExpression)expression).Target;
			this._labelInfo.Add(target, new LabelInfo(this._ilg, target, TypeUtils.AreReferenceAssignable(lambda.ReturnType, target.Type)));
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06001724 RID: 5924 RVA: 0x0004F744 File Offset: 0x0004D944
		private bool EmitDebugSymbols
		{
			get
			{
				return this._tree.DebugInfoGenerator != null;
			}
		}

		// Token: 0x06001725 RID: 5925 RVA: 0x0004F754 File Offset: 0x0004D954
		private LambdaCompiler(AnalyzedTree tree, LambdaExpression lambda)
		{
			Type[] parameterTypes = LambdaCompiler.GetParameterTypes(lambda).AddFirst(typeof(Closure));
			DynamicMethod dynamicMethod = new DynamicMethod(lambda.Name ?? "lambda_method", lambda.ReturnType, parameterTypes, true);
			this._tree = tree;
			this._lambda = lambda;
			this._method = dynamicMethod;
			dynamicMethod.ProfileAPICheck = true;
			this._ilg = dynamicMethod.GetILGenerator();
			this._hasClosureArgument = true;
			this._scope = tree.Scopes[lambda];
			this._boundConstants = tree.Constants[lambda];
			this.InitializeMethod();
		}

		// Token: 0x06001726 RID: 5926 RVA: 0x0004F824 File Offset: 0x0004DA24
		private LambdaCompiler(AnalyzedTree tree, LambdaExpression lambda, MethodBuilder method)
		{
			this._hasClosureArgument = tree.Scopes[lambda].NeedsClosure;
			Type[] array = LambdaCompiler.GetParameterTypes(lambda);
			if (this._hasClosureArgument)
			{
				array = array.AddFirst(typeof(Closure));
			}
			method.SetReturnType(lambda.ReturnType);
			method.SetParameters(array);
			string[] array2 = lambda.Parameters.Map((ParameterExpression p) => p.Name);
			int num = this._hasClosureArgument ? 2 : 1;
			for (int i = 0; i < array2.Length; i++)
			{
				method.DefineParameter(i + num, ParameterAttributes.None, array2[i]);
			}
			this._tree = tree;
			this._lambda = lambda;
			this._typeBuilder = (TypeBuilder)method.DeclaringType;
			this._method = method;
			this._ilg = method.GetILGenerator();
			this._scope = tree.Scopes[lambda];
			this._boundConstants = tree.Constants[lambda];
			this.InitializeMethod();
		}

		// Token: 0x06001727 RID: 5927 RVA: 0x0004F960 File Offset: 0x0004DB60
		private LambdaCompiler(LambdaCompiler parent, LambdaExpression lambda)
		{
			this._tree = parent._tree;
			this._lambda = lambda;
			this._method = parent._method;
			this._ilg = parent._ilg;
			this._hasClosureArgument = parent._hasClosureArgument;
			this._typeBuilder = parent._typeBuilder;
			this._scope = this._tree.Scopes[lambda];
			this._boundConstants = parent._boundConstants;
		}

		// Token: 0x06001728 RID: 5928 RVA: 0x0004FA07 File Offset: 0x0004DC07
		private void InitializeMethod()
		{
			this.AddReturnLabel(this._lambda);
			this._boundConstants.EmitCacheConstants(this);
		}

		// Token: 0x06001729 RID: 5929 RVA: 0x0004FA21 File Offset: 0x0004DC21
		public override string ToString()
		{
			return this._method.ToString();
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x0600172A RID: 5930 RVA: 0x0004FA2E File Offset: 0x0004DC2E
		internal ILGenerator IL
		{
			get
			{
				return this._ilg;
			}
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x0600172B RID: 5931 RVA: 0x0004FA36 File Offset: 0x0004DC36
		internal ReadOnlyCollection<ParameterExpression> Parameters
		{
			get
			{
				return this._lambda.Parameters;
			}
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x0600172C RID: 5932 RVA: 0x0004FA43 File Offset: 0x0004DC43
		internal bool CanEmitBoundConstants
		{
			get
			{
				return this._method is DynamicMethod;
			}
		}

		// Token: 0x0600172D RID: 5933 RVA: 0x0004FA54 File Offset: 0x0004DC54
		internal static Delegate Compile(LambdaExpression lambda, DebugInfoGenerator debugInfoGenerator)
		{
			AnalyzedTree analyzedTree = LambdaCompiler.AnalyzeLambda(ref lambda);
			analyzedTree.DebugInfoGenerator = debugInfoGenerator;
			LambdaCompiler lambdaCompiler = new LambdaCompiler(analyzedTree, lambda);
			lambdaCompiler.EmitLambdaBody();
			return lambdaCompiler.CreateDelegate();
		}

		// Token: 0x0600172E RID: 5934 RVA: 0x0004FA84 File Offset: 0x0004DC84
		internal static void Compile(LambdaExpression lambda, MethodBuilder method, DebugInfoGenerator debugInfoGenerator)
		{
			AnalyzedTree analyzedTree = LambdaCompiler.AnalyzeLambda(ref lambda);
			analyzedTree.DebugInfoGenerator = debugInfoGenerator;
			LambdaCompiler lambdaCompiler = new LambdaCompiler(analyzedTree, lambda, method);
			lambdaCompiler.EmitLambdaBody();
		}

		// Token: 0x0600172F RID: 5935 RVA: 0x0004FAAF File Offset: 0x0004DCAF
		private static AnalyzedTree AnalyzeLambda(ref LambdaExpression lambda)
		{
			lambda = StackSpiller.AnalyzeLambda(lambda);
			return VariableBinder.Bind(lambda);
		}

		// Token: 0x06001730 RID: 5936 RVA: 0x0004FAC4 File Offset: 0x0004DCC4
		internal LocalBuilder GetLocal(Type type)
		{
			LocalBuilder result;
			if (this._freeLocals.TryDequeue(type, out result))
			{
				return result;
			}
			return this._ilg.DeclareLocal(type);
		}

		// Token: 0x06001731 RID: 5937 RVA: 0x0004FAEF File Offset: 0x0004DCEF
		internal void FreeLocal(LocalBuilder local)
		{
			if (local != null)
			{
				this._freeLocals.Enqueue(local.LocalType, local);
			}
		}

		// Token: 0x06001732 RID: 5938 RVA: 0x0004FB08 File Offset: 0x0004DD08
		internal LocalBuilder GetNamedLocal(Type type, ParameterExpression variable)
		{
			LocalBuilder localBuilder = this._ilg.DeclareLocal(type);
			if (this.EmitDebugSymbols && variable.Name != null)
			{
				this._tree.DebugInfoGenerator.SetLocalName(localBuilder, variable.Name);
			}
			return localBuilder;
		}

		// Token: 0x06001733 RID: 5939 RVA: 0x0004FB4A File Offset: 0x0004DD4A
		internal int GetLambdaArgument(int index)
		{
			return index + (this._hasClosureArgument ? 1 : 0) + (this._method.IsStatic ? 0 : 1);
		}

		// Token: 0x06001734 RID: 5940 RVA: 0x0004FB6C File Offset: 0x0004DD6C
		internal void EmitLambdaArgument(int index)
		{
			this._ilg.EmitLoadArg(this.GetLambdaArgument(index));
		}

		// Token: 0x06001735 RID: 5941 RVA: 0x0004FB80 File Offset: 0x0004DD80
		internal void EmitClosureArgument()
		{
			this._ilg.EmitLoadArg(0);
		}

		// Token: 0x06001736 RID: 5942 RVA: 0x0004FB8E File Offset: 0x0004DD8E
		private Delegate CreateDelegate()
		{
			return this._method.CreateDelegate(this._lambda.Type, new Closure(this._boundConstants.ToArray(), null));
		}

		// Token: 0x06001737 RID: 5943 RVA: 0x0004FBB8 File Offset: 0x0004DDB8
		private FieldBuilder CreateStaticField(string name, Type type)
		{
			return this._typeBuilder.DefineField("<ExpressionCompilerImplementationDetails>{" + Interlocked.Increment(ref LambdaCompiler._Counter).ToString() + "}" + name, type, FieldAttributes.Private | FieldAttributes.Static);
		}

		// Token: 0x06001738 RID: 5944 RVA: 0x0004FBF8 File Offset: 0x0004DDF8
		private MemberExpression CreateLazyInitializedField<T>(string name)
		{
			if (this._method is DynamicMethod)
			{
				return Expression.Field(Expression.Constant(new StrongBox<T>(default(T))), "Value");
			}
			return Expression.Field(null, this.CreateStaticField(name, typeof(T)));
		}

		// Token: 0x06001739 RID: 5945 RVA: 0x0004FC48 File Offset: 0x0004DE48
		private static LambdaCompiler.CompilationFlags UpdateEmitAsTailCallFlag(LambdaCompiler.CompilationFlags flags, LambdaCompiler.CompilationFlags newValue)
		{
			LambdaCompiler.CompilationFlags compilationFlags = flags & LambdaCompiler.CompilationFlags.EmitAsTailCallMask;
			return (flags ^ compilationFlags) | newValue;
		}

		// Token: 0x0600173A RID: 5946 RVA: 0x0004FC64 File Offset: 0x0004DE64
		private static LambdaCompiler.CompilationFlags UpdateEmitExpressionStartFlag(LambdaCompiler.CompilationFlags flags, LambdaCompiler.CompilationFlags newValue)
		{
			LambdaCompiler.CompilationFlags compilationFlags = flags & LambdaCompiler.CompilationFlags.EmitExpressionStartMask;
			return (flags ^ compilationFlags) | newValue;
		}

		// Token: 0x0600173B RID: 5947 RVA: 0x0004FC7C File Offset: 0x0004DE7C
		private static LambdaCompiler.CompilationFlags UpdateEmitAsTypeFlag(LambdaCompiler.CompilationFlags flags, LambdaCompiler.CompilationFlags newValue)
		{
			LambdaCompiler.CompilationFlags compilationFlags = flags & LambdaCompiler.CompilationFlags.EmitAsTypeMask;
			return (flags ^ compilationFlags) | newValue;
		}

		// Token: 0x0600173C RID: 5948 RVA: 0x0004FC96 File Offset: 0x0004DE96
		internal void EmitExpression(Expression node)
		{
			this.EmitExpression(node, LambdaCompiler.CompilationFlags.EmitExpressionStart | LambdaCompiler.CompilationFlags.EmitAsNoTail);
		}

		// Token: 0x0600173D RID: 5949 RVA: 0x0004FCA4 File Offset: 0x0004DEA4
		private void EmitExpressionAsVoid(Expression node)
		{
			this.EmitExpressionAsVoid(node, LambdaCompiler.CompilationFlags.EmitAsNoTail);
		}

		// Token: 0x0600173E RID: 5950 RVA: 0x0004FCB4 File Offset: 0x0004DEB4
		private void EmitExpressionAsVoid(Expression node, LambdaCompiler.CompilationFlags flags)
		{
			LambdaCompiler.CompilationFlags flags2 = this.EmitExpressionStart(node);
			ExpressionType nodeType = node.NodeType;
			if (nodeType <= ExpressionType.Assign)
			{
				if (nodeType == ExpressionType.Constant || nodeType == ExpressionType.Parameter)
				{
					goto IL_D5;
				}
				if (nodeType == ExpressionType.Assign)
				{
					this.EmitAssign((BinaryExpression)node, LambdaCompiler.CompilationFlags.EmitAsVoidType);
					goto IL_D5;
				}
			}
			else if (nodeType <= ExpressionType.Default)
			{
				if (nodeType == ExpressionType.Block)
				{
					this.Emit((BlockExpression)node, LambdaCompiler.UpdateEmitAsTypeFlag(flags, LambdaCompiler.CompilationFlags.EmitAsVoidType));
					goto IL_D5;
				}
				if (nodeType == ExpressionType.Default)
				{
					goto IL_D5;
				}
			}
			else
			{
				if (nodeType == ExpressionType.Goto)
				{
					this.EmitGotoExpression(node, LambdaCompiler.UpdateEmitAsTypeFlag(flags, LambdaCompiler.CompilationFlags.EmitAsVoidType));
					goto IL_D5;
				}
				if (nodeType == ExpressionType.Throw)
				{
					this.EmitThrow((UnaryExpression)node, LambdaCompiler.CompilationFlags.EmitAsVoidType);
					goto IL_D5;
				}
			}
			if (node.Type == typeof(void))
			{
				this.EmitExpression(node, LambdaCompiler.UpdateEmitExpressionStartFlag(flags, LambdaCompiler.CompilationFlags.EmitNoExpressionStart));
			}
			else
			{
				this.EmitExpression(node, LambdaCompiler.CompilationFlags.EmitNoExpressionStart | LambdaCompiler.CompilationFlags.EmitAsNoTail);
				this._ilg.Emit(OpCodes.Pop);
			}
			IL_D5:
			this.EmitExpressionEnd(flags2);
		}

		// Token: 0x0600173F RID: 5951 RVA: 0x0004FDA0 File Offset: 0x0004DFA0
		private void EmitExpressionAsType(Expression node, Type type, LambdaCompiler.CompilationFlags flags)
		{
			if (type == typeof(void))
			{
				this.EmitExpressionAsVoid(node, flags);
				return;
			}
			if (!TypeUtils.AreEquivalent(node.Type, type))
			{
				this.EmitExpression(node);
				this._ilg.Emit(OpCodes.Castclass, type);
				return;
			}
			this.EmitExpression(node, LambdaCompiler.UpdateEmitExpressionStartFlag(flags, LambdaCompiler.CompilationFlags.EmitExpressionStart));
		}

		// Token: 0x06001740 RID: 5952 RVA: 0x0004FDFD File Offset: 0x0004DFFD
		private LambdaCompiler.CompilationFlags EmitExpressionStart(Expression node)
		{
			if (this.TryPushLabelBlock(node))
			{
				return LambdaCompiler.CompilationFlags.EmitExpressionStart;
			}
			return LambdaCompiler.CompilationFlags.EmitNoExpressionStart;
		}

		// Token: 0x06001741 RID: 5953 RVA: 0x0004FE0B File Offset: 0x0004E00B
		private void EmitExpressionEnd(LambdaCompiler.CompilationFlags flags)
		{
			if ((flags & LambdaCompiler.CompilationFlags.EmitExpressionStartMask) == LambdaCompiler.CompilationFlags.EmitExpressionStart)
			{
				this.PopLabelBlock(this._labelBlock.Kind);
			}
		}

		// Token: 0x06001742 RID: 5954 RVA: 0x0004FE28 File Offset: 0x0004E028
		private void EmitInvocationExpression(Expression expr, LambdaCompiler.CompilationFlags flags)
		{
			InvocationExpression invocationExpression = (InvocationExpression)expr;
			if (invocationExpression.LambdaOperand != null)
			{
				this.EmitInlinedInvoke(invocationExpression, flags);
				return;
			}
			expr = invocationExpression.Expression;
			if (typeof(LambdaExpression).IsAssignableFrom(expr.Type))
			{
				expr = Expression.Call(expr, expr.Type.GetMethod("Compile", new Type[0]));
			}
			expr = Expression.Call(expr, expr.Type.GetMethod("Invoke"), invocationExpression.Arguments);
			this.EmitExpression(expr);
		}

		// Token: 0x06001743 RID: 5955 RVA: 0x0004FEB0 File Offset: 0x0004E0B0
		private void EmitInlinedInvoke(InvocationExpression invoke, LambdaCompiler.CompilationFlags flags)
		{
			LambdaExpression lambdaOperand = invoke.LambdaOperand;
			List<LambdaCompiler.WriteBack> list = this.EmitArguments(lambdaOperand.Type.GetMethod("Invoke"), invoke);
			LambdaCompiler lambdaCompiler = new LambdaCompiler(this, lambdaOperand);
			if (list.Count != 0)
			{
				flags = LambdaCompiler.UpdateEmitAsTailCallFlag(flags, LambdaCompiler.CompilationFlags.EmitAsNoTail);
			}
			lambdaCompiler.EmitLambdaBody(this._scope, true, flags);
			LambdaCompiler.EmitWriteBack(list);
		}

		// Token: 0x06001744 RID: 5956 RVA: 0x0004FF10 File Offset: 0x0004E110
		private void EmitIndexExpression(Expression expr)
		{
			IndexExpression indexExpression = (IndexExpression)expr;
			Type objectType = null;
			if (indexExpression.Object != null)
			{
				this.EmitInstance(indexExpression.Object, objectType = indexExpression.Object.Type);
			}
			foreach (Expression node in indexExpression.Arguments)
			{
				this.EmitExpression(node);
			}
			this.EmitGetIndexCall(indexExpression, objectType);
		}

		// Token: 0x06001745 RID: 5957 RVA: 0x0004FF90 File Offset: 0x0004E190
		private void EmitIndexAssignment(BinaryExpression node, LambdaCompiler.CompilationFlags flags)
		{
			IndexExpression indexExpression = (IndexExpression)node.Left;
			LambdaCompiler.CompilationFlags compilationFlags = flags & LambdaCompiler.CompilationFlags.EmitAsTypeMask;
			Type objectType = null;
			if (indexExpression.Object != null)
			{
				this.EmitInstance(indexExpression.Object, objectType = indexExpression.Object.Type);
			}
			foreach (Expression node2 in indexExpression.Arguments)
			{
				this.EmitExpression(node2);
			}
			this.EmitExpression(node.Right);
			LocalBuilder local = null;
			if (compilationFlags != LambdaCompiler.CompilationFlags.EmitAsVoidType)
			{
				this._ilg.Emit(OpCodes.Dup);
				this._ilg.Emit(OpCodes.Stloc, local = this.GetLocal(node.Type));
			}
			this.EmitSetIndexCall(indexExpression, objectType);
			if (compilationFlags != LambdaCompiler.CompilationFlags.EmitAsVoidType)
			{
				this._ilg.Emit(OpCodes.Ldloc, local);
				this.FreeLocal(local);
			}
		}

		// Token: 0x06001746 RID: 5958 RVA: 0x00050084 File Offset: 0x0004E284
		private void EmitGetIndexCall(IndexExpression node, Type objectType)
		{
			if (node.Indexer != null)
			{
				MethodInfo getMethod = node.Indexer.GetGetMethod(true);
				this.EmitCall(objectType, getMethod);
				return;
			}
			if (node.Arguments.Count != 1)
			{
				this._ilg.Emit(OpCodes.Call, node.Object.Type.GetMethod("Get", BindingFlags.Instance | BindingFlags.Public));
				return;
			}
			this._ilg.EmitLoadElement(node.Type);
		}

		// Token: 0x06001747 RID: 5959 RVA: 0x000500FC File Offset: 0x0004E2FC
		private void EmitSetIndexCall(IndexExpression node, Type objectType)
		{
			if (node.Indexer != null)
			{
				MethodInfo setMethod = node.Indexer.GetSetMethod(true);
				this.EmitCall(objectType, setMethod);
				return;
			}
			if (node.Arguments.Count != 1)
			{
				this._ilg.Emit(OpCodes.Call, node.Object.Type.GetMethod("Set", BindingFlags.Instance | BindingFlags.Public));
				return;
			}
			this._ilg.EmitStoreElement(node.Type);
		}

		// Token: 0x06001748 RID: 5960 RVA: 0x00050174 File Offset: 0x0004E374
		private void EmitMethodCallExpression(Expression expr, LambdaCompiler.CompilationFlags flags)
		{
			MethodCallExpression methodCallExpression = (MethodCallExpression)expr;
			this.EmitMethodCall(methodCallExpression.Object, methodCallExpression.Method, methodCallExpression, flags);
		}

		// Token: 0x06001749 RID: 5961 RVA: 0x0005019C File Offset: 0x0004E39C
		private void EmitMethodCallExpression(Expression expr)
		{
			this.EmitMethodCallExpression(expr, LambdaCompiler.CompilationFlags.EmitAsNoTail);
		}

		// Token: 0x0600174A RID: 5962 RVA: 0x000501AA File Offset: 0x0004E3AA
		private void EmitMethodCall(Expression obj, MethodInfo method, IArgumentProvider methodCallExpr)
		{
			this.EmitMethodCall(obj, method, methodCallExpr, LambdaCompiler.CompilationFlags.EmitAsNoTail);
		}

		// Token: 0x0600174B RID: 5963 RVA: 0x000501BC File Offset: 0x0004E3BC
		private void EmitMethodCall(Expression obj, MethodInfo method, IArgumentProvider methodCallExpr, LambdaCompiler.CompilationFlags flags)
		{
			Type objectType = null;
			if (!method.IsStatic)
			{
				this.EmitInstance(obj, objectType = obj.Type);
			}
			if (obj != null && obj.Type.IsValueType)
			{
				this.EmitMethodCall(method, methodCallExpr, objectType);
				return;
			}
			this.EmitMethodCall(method, methodCallExpr, objectType, flags);
		}

		// Token: 0x0600174C RID: 5964 RVA: 0x00050207 File Offset: 0x0004E407
		private void EmitMethodCall(MethodInfo mi, IArgumentProvider args, Type objectType)
		{
			this.EmitMethodCall(mi, args, objectType, LambdaCompiler.CompilationFlags.EmitAsNoTail);
		}

		// Token: 0x0600174D RID: 5965 RVA: 0x00050218 File Offset: 0x0004E418
		private void EmitMethodCall(MethodInfo mi, IArgumentProvider args, Type objectType, LambdaCompiler.CompilationFlags flags)
		{
			List<LambdaCompiler.WriteBack> writeBacks = this.EmitArguments(mi, args);
			OpCode opCode = LambdaCompiler.UseVirtual(mi) ? OpCodes.Callvirt : OpCodes.Call;
			if (opCode == OpCodes.Callvirt && objectType.IsValueType)
			{
				this._ilg.Emit(OpCodes.Constrained, objectType);
			}
			if ((flags & LambdaCompiler.CompilationFlags.EmitAsTailCallMask) == LambdaCompiler.CompilationFlags.EmitAsTail && !LambdaCompiler.MethodHasByRefParameter(mi))
			{
				this._ilg.Emit(OpCodes.Tailcall);
			}
			if (mi.CallingConvention == CallingConventions.VarArgs)
			{
				this._ilg.EmitCall(opCode, mi, args.Map((Expression a) => a.Type));
			}
			else
			{
				this._ilg.Emit(opCode, mi);
			}
			LambdaCompiler.EmitWriteBack(writeBacks);
		}

		// Token: 0x0600174E RID: 5966 RVA: 0x000502E0 File Offset: 0x0004E4E0
		private static bool MethodHasByRefParameter(MethodInfo mi)
		{
			foreach (ParameterInfo pi in mi.GetParametersCached())
			{
				if (pi.IsByRefParameter())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600174F RID: 5967 RVA: 0x00050314 File Offset: 0x0004E514
		private void EmitCall(Type objectType, MethodInfo method)
		{
			if (method.CallingConvention == CallingConventions.VarArgs)
			{
				throw Error.UnexpectedVarArgsCall(method);
			}
			OpCode opCode = LambdaCompiler.UseVirtual(method) ? OpCodes.Callvirt : OpCodes.Call;
			if (opCode == OpCodes.Callvirt && objectType.IsValueType)
			{
				this._ilg.Emit(OpCodes.Constrained, objectType);
			}
			this._ilg.Emit(opCode, method);
		}

		// Token: 0x06001750 RID: 5968 RVA: 0x00050379 File Offset: 0x0004E579
		private static bool UseVirtual(MethodInfo mi)
		{
			return !mi.IsStatic && !mi.DeclaringType.IsValueType;
		}

		// Token: 0x06001751 RID: 5969 RVA: 0x00050395 File Offset: 0x0004E595
		private List<LambdaCompiler.WriteBack> EmitArguments(MethodBase method, IArgumentProvider args)
		{
			return this.EmitArguments(method, args, 0);
		}

		// Token: 0x06001752 RID: 5970 RVA: 0x000503A0 File Offset: 0x0004E5A0
		private List<LambdaCompiler.WriteBack> EmitArguments(MethodBase method, IArgumentProvider args, int skipParameters)
		{
			ParameterInfo[] parametersCached = method.GetParametersCached();
			List<LambdaCompiler.WriteBack> list = new List<LambdaCompiler.WriteBack>();
			int i = skipParameters;
			int num = parametersCached.Length;
			while (i < num)
			{
				ParameterInfo parameterInfo = parametersCached[i];
				Expression argument = args.GetArgument(i - skipParameters);
				Type type = parameterInfo.ParameterType;
				if (type.IsByRef)
				{
					type = type.GetElementType();
					LambdaCompiler.WriteBack writeBack = this.EmitAddressWriteBack(argument, type);
					if (writeBack != null)
					{
						list.Add(writeBack);
					}
				}
				else
				{
					this.EmitExpression(argument);
				}
				i++;
			}
			return list;
		}

		// Token: 0x06001753 RID: 5971 RVA: 0x00050418 File Offset: 0x0004E618
		private static void EmitWriteBack(IList<LambdaCompiler.WriteBack> writeBacks)
		{
			foreach (LambdaCompiler.WriteBack writeBack in writeBacks)
			{
				writeBack();
			}
		}

		// Token: 0x06001754 RID: 5972 RVA: 0x00050460 File Offset: 0x0004E660
		private void EmitConstantExpression(Expression expr)
		{
			ConstantExpression constantExpression = (ConstantExpression)expr;
			this.EmitConstant(constantExpression.Value, constantExpression.Type);
		}

		// Token: 0x06001755 RID: 5973 RVA: 0x00050486 File Offset: 0x0004E686
		private void EmitConstant(object value, Type type)
		{
			if (ILGen.CanEmitConstant(value, type))
			{
				this._ilg.EmitConstant(value, type);
				return;
			}
			this._boundConstants.EmitConstant(this, value, type);
		}

		// Token: 0x06001756 RID: 5974 RVA: 0x000504B0 File Offset: 0x0004E6B0
		private void EmitDynamicExpression(Expression expr)
		{
			if (!(this._method is DynamicMethod))
			{
				throw Error.CannotCompileDynamic();
			}
			DynamicExpression dynamicExpression = (DynamicExpression)expr;
			CallSite callSite = CallSite.Create(dynamicExpression.DelegateType, dynamicExpression.Binder);
			Type type = callSite.GetType();
			MethodInfo method = dynamicExpression.DelegateType.GetMethod("Invoke");
			this.EmitConstant(callSite, type);
			this._ilg.Emit(OpCodes.Dup);
			LocalBuilder local = this.GetLocal(typeof(CallSite));
			this._ilg.Emit(OpCodes.Stloc, local);
			this._ilg.Emit(OpCodes.Ldfld, type.GetField("Target"));
			this._ilg.Emit(OpCodes.Ldloc, local);
			this.FreeLocal(local);
			List<LambdaCompiler.WriteBack> writeBacks = this.EmitArguments(method, dynamicExpression, 1);
			this._ilg.Emit(OpCodes.Callvirt, method);
			LambdaCompiler.EmitWriteBack(writeBacks);
		}

		// Token: 0x06001757 RID: 5975 RVA: 0x00050598 File Offset: 0x0004E798
		private void EmitNewExpression(Expression expr)
		{
			NewExpression newExpression = (NewExpression)expr;
			if (newExpression.Constructor != null)
			{
				List<LambdaCompiler.WriteBack> writeBacks = this.EmitArguments(newExpression.Constructor, newExpression);
				this._ilg.Emit(OpCodes.Newobj, newExpression.Constructor);
				LambdaCompiler.EmitWriteBack(writeBacks);
				return;
			}
			LocalBuilder local = this.GetLocal(newExpression.Type);
			this._ilg.Emit(OpCodes.Ldloca, local);
			this._ilg.Emit(OpCodes.Initobj, newExpression.Type);
			this._ilg.Emit(OpCodes.Ldloc, local);
			this.FreeLocal(local);
		}

		// Token: 0x06001758 RID: 5976 RVA: 0x00050634 File Offset: 0x0004E834
		private void EmitTypeBinaryExpression(Expression expr)
		{
			TypeBinaryExpression typeBinaryExpression = (TypeBinaryExpression)expr;
			if (typeBinaryExpression.NodeType == ExpressionType.TypeEqual)
			{
				this.EmitExpression(typeBinaryExpression.ReduceTypeEqual());
				return;
			}
			Type type = typeBinaryExpression.Expression.Type;
			AnalyzeTypeIsResult analyzeTypeIsResult = ConstantCheck.AnalyzeTypeIs(typeBinaryExpression);
			if (analyzeTypeIsResult == AnalyzeTypeIsResult.KnownTrue || analyzeTypeIsResult == AnalyzeTypeIsResult.KnownFalse)
			{
				this.EmitExpressionAsVoid(typeBinaryExpression.Expression);
				this._ilg.EmitBoolean(analyzeTypeIsResult == AnalyzeTypeIsResult.KnownTrue);
				return;
			}
			if (analyzeTypeIsResult != AnalyzeTypeIsResult.KnownAssignable)
			{
				this.EmitExpression(typeBinaryExpression.Expression);
				if (type.IsValueType)
				{
					this._ilg.Emit(OpCodes.Box, type);
				}
				this._ilg.Emit(OpCodes.Isinst, typeBinaryExpression.TypeOperand);
				this._ilg.Emit(OpCodes.Ldnull);
				this._ilg.Emit(OpCodes.Cgt_Un);
				return;
			}
			if (type.IsNullableType())
			{
				this.EmitAddress(typeBinaryExpression.Expression, type);
				this._ilg.EmitHasValue(type);
				return;
			}
			this.EmitExpression(typeBinaryExpression.Expression);
			this._ilg.Emit(OpCodes.Ldnull);
			this._ilg.Emit(OpCodes.Ceq);
			this._ilg.Emit(OpCodes.Ldc_I4_0);
			this._ilg.Emit(OpCodes.Ceq);
		}

		// Token: 0x06001759 RID: 5977 RVA: 0x00050764 File Offset: 0x0004E964
		private void EmitVariableAssignment(BinaryExpression node, LambdaCompiler.CompilationFlags flags)
		{
			ParameterExpression parameterExpression = (ParameterExpression)node.Left;
			LambdaCompiler.CompilationFlags compilationFlags = flags & LambdaCompiler.CompilationFlags.EmitAsTypeMask;
			this.EmitExpression(node.Right);
			if (compilationFlags != LambdaCompiler.CompilationFlags.EmitAsVoidType)
			{
				this._ilg.Emit(OpCodes.Dup);
			}
			if (parameterExpression.IsByRef)
			{
				LocalBuilder local = this.GetLocal(parameterExpression.Type);
				this._ilg.Emit(OpCodes.Stloc, local);
				this._scope.EmitGet(parameterExpression);
				this._ilg.Emit(OpCodes.Ldloc, local);
				this.FreeLocal(local);
				this._ilg.EmitStoreValueIndirect(parameterExpression.Type);
				return;
			}
			this._scope.EmitSet(parameterExpression);
		}

		// Token: 0x0600175A RID: 5978 RVA: 0x0005080E File Offset: 0x0004EA0E
		private void EmitAssignBinaryExpression(Expression expr)
		{
			this.EmitAssign((BinaryExpression)expr, LambdaCompiler.CompilationFlags.EmitAsDefaultType);
		}

		// Token: 0x0600175B RID: 5979 RVA: 0x00050820 File Offset: 0x0004EA20
		private void EmitAssign(BinaryExpression node, LambdaCompiler.CompilationFlags emitAs)
		{
			ExpressionType nodeType = node.Left.NodeType;
			if (nodeType == ExpressionType.MemberAccess)
			{
				this.EmitMemberAssignment(node, emitAs);
				return;
			}
			if (nodeType == ExpressionType.Parameter)
			{
				this.EmitVariableAssignment(node, emitAs);
				return;
			}
			if (nodeType == ExpressionType.Index)
			{
				this.EmitIndexAssignment(node, emitAs);
				return;
			}
			throw Error.InvalidLvalue(node.Left.NodeType);
		}

		// Token: 0x0600175C RID: 5980 RVA: 0x00050878 File Offset: 0x0004EA78
		private void EmitParameterExpression(Expression expr)
		{
			ParameterExpression parameterExpression = (ParameterExpression)expr;
			this._scope.EmitGet(parameterExpression);
			if (parameterExpression.IsByRef)
			{
				this._ilg.EmitLoadValueIndirect(parameterExpression.Type);
			}
		}

		// Token: 0x0600175D RID: 5981 RVA: 0x000508B4 File Offset: 0x0004EAB4
		private void EmitLambdaExpression(Expression expr)
		{
			LambdaExpression lambda = (LambdaExpression)expr;
			this.EmitDelegateConstruction(lambda);
		}

		// Token: 0x0600175E RID: 5982 RVA: 0x000508D0 File Offset: 0x0004EAD0
		private void EmitRuntimeVariablesExpression(Expression expr)
		{
			RuntimeVariablesExpression runtimeVariablesExpression = (RuntimeVariablesExpression)expr;
			this._scope.EmitVariableAccess(this, runtimeVariablesExpression.Variables);
		}

		// Token: 0x0600175F RID: 5983 RVA: 0x000508F8 File Offset: 0x0004EAF8
		private void EmitMemberAssignment(BinaryExpression node, LambdaCompiler.CompilationFlags flags)
		{
			MemberExpression memberExpression = (MemberExpression)node.Left;
			MemberInfo member = memberExpression.Member;
			Type objectType = null;
			if (memberExpression.Expression != null)
			{
				this.EmitInstance(memberExpression.Expression, objectType = memberExpression.Expression.Type);
			}
			this.EmitExpression(node.Right);
			LocalBuilder local = null;
			LambdaCompiler.CompilationFlags compilationFlags = flags & LambdaCompiler.CompilationFlags.EmitAsTypeMask;
			if (compilationFlags != LambdaCompiler.CompilationFlags.EmitAsVoidType)
			{
				this._ilg.Emit(OpCodes.Dup);
				this._ilg.Emit(OpCodes.Stloc, local = this.GetLocal(node.Type));
			}
			MemberTypes memberType = member.MemberType;
			if (memberType != MemberTypes.Field)
			{
				if (memberType != MemberTypes.Property)
				{
					throw Error.InvalidMemberType(member.MemberType);
				}
				this.EmitCall(objectType, ((PropertyInfo)member).GetSetMethod(true));
			}
			else
			{
				this._ilg.EmitFieldSet((FieldInfo)member);
			}
			if (compilationFlags != LambdaCompiler.CompilationFlags.EmitAsVoidType)
			{
				this._ilg.Emit(OpCodes.Ldloc, local);
				this.FreeLocal(local);
			}
		}

		// Token: 0x06001760 RID: 5984 RVA: 0x000509F4 File Offset: 0x0004EBF4
		private void EmitMemberExpression(Expression expr)
		{
			MemberExpression memberExpression = (MemberExpression)expr;
			Type objectType = null;
			if (memberExpression.Expression != null)
			{
				this.EmitInstance(memberExpression.Expression, objectType = memberExpression.Expression.Type);
			}
			this.EmitMemberGet(memberExpression.Member, objectType);
		}

		// Token: 0x06001761 RID: 5985 RVA: 0x00050A38 File Offset: 0x0004EC38
		private void EmitMemberGet(MemberInfo member, Type objectType)
		{
			MemberTypes memberType = member.MemberType;
			if (memberType != MemberTypes.Field)
			{
				if (memberType != MemberTypes.Property)
				{
					throw ContractUtils.Unreachable;
				}
				this.EmitCall(objectType, ((PropertyInfo)member).GetGetMethod(true));
				return;
			}
			else
			{
				FieldInfo fieldInfo = (FieldInfo)member;
				if (fieldInfo.IsLiteral)
				{
					this.EmitConstant(fieldInfo.GetRawConstantValue(), fieldInfo.FieldType);
					return;
				}
				this._ilg.EmitFieldGet(fieldInfo);
				return;
			}
		}

		// Token: 0x06001762 RID: 5986 RVA: 0x00050A9F File Offset: 0x0004EC9F
		private void EmitInstance(Expression instance, Type type)
		{
			if (instance != null)
			{
				if (type.IsValueType)
				{
					this.EmitAddress(instance, type);
					return;
				}
				this.EmitExpression(instance);
			}
		}

		// Token: 0x06001763 RID: 5987 RVA: 0x00050ABC File Offset: 0x0004ECBC
		private void EmitNewArrayExpression(Expression expr)
		{
			NewArrayExpression node = (NewArrayExpression)expr;
			if (node.NodeType == ExpressionType.NewArrayInit)
			{
				this._ilg.EmitArray(node.Type.GetElementType(), node.Expressions.Count, delegate(int index)
				{
					this.EmitExpression(node.Expressions[index]);
				});
				return;
			}
			ReadOnlyCollection<Expression> expressions = node.Expressions;
			for (int i = 0; i < expressions.Count; i++)
			{
				Expression expression = expressions[i];
				this.EmitExpression(expression);
				this._ilg.EmitConvertToType(expression.Type, typeof(int), true);
			}
			this._ilg.EmitArray(node.Type);
		}

		// Token: 0x06001764 RID: 5988 RVA: 0x00050B88 File Offset: 0x0004ED88
		private void EmitDebugInfoExpression(Expression expr)
		{
			if (!this.EmitDebugSymbols)
			{
				return;
			}
			DebugInfoExpression debugInfoExpression = (DebugInfoExpression)expr;
			if (debugInfoExpression.IsClear && this._sequencePointCleared)
			{
				return;
			}
			this._tree.DebugInfoGenerator.MarkSequencePoint(this._lambda, this._method, this._ilg, debugInfoExpression);
			this._ilg.Emit(OpCodes.Nop);
			this._sequencePointCleared = debugInfoExpression.IsClear;
		}

		// Token: 0x06001765 RID: 5989 RVA: 0x00050BF5 File Offset: 0x0004EDF5
		private static void EmitExtensionExpression(Expression expr)
		{
			throw Error.ExtensionNotReduced();
		}

		// Token: 0x06001766 RID: 5990 RVA: 0x00050BFC File Offset: 0x0004EDFC
		private void EmitListInitExpression(Expression expr)
		{
			this.EmitListInit((ListInitExpression)expr);
		}

		// Token: 0x06001767 RID: 5991 RVA: 0x00050C0A File Offset: 0x0004EE0A
		private void EmitMemberInitExpression(Expression expr)
		{
			this.EmitMemberInit((MemberInitExpression)expr);
		}

		// Token: 0x06001768 RID: 5992 RVA: 0x00050C18 File Offset: 0x0004EE18
		private void EmitBinding(MemberBinding binding, Type objectType)
		{
			switch (binding.BindingType)
			{
			case MemberBindingType.Assignment:
				this.EmitMemberAssignment((MemberAssignment)binding, objectType);
				return;
			case MemberBindingType.MemberBinding:
				this.EmitMemberMemberBinding((MemberMemberBinding)binding);
				return;
			case MemberBindingType.ListBinding:
				this.EmitMemberListBinding((MemberListBinding)binding);
				return;
			default:
				throw Error.UnknownBindingType();
			}
		}

		// Token: 0x06001769 RID: 5993 RVA: 0x00050C70 File Offset: 0x0004EE70
		private void EmitMemberAssignment(MemberAssignment binding, Type objectType)
		{
			this.EmitExpression(binding.Expression);
			FieldInfo fieldInfo = binding.Member as FieldInfo;
			if (fieldInfo != null)
			{
				this._ilg.Emit(OpCodes.Stfld, fieldInfo);
				return;
			}
			PropertyInfo propertyInfo = binding.Member as PropertyInfo;
			if (propertyInfo != null)
			{
				this.EmitCall(objectType, propertyInfo.GetSetMethod(true));
				return;
			}
			throw Error.UnhandledBinding();
		}

		// Token: 0x0600176A RID: 5994 RVA: 0x00050CDC File Offset: 0x0004EEDC
		private void EmitMemberMemberBinding(MemberMemberBinding binding)
		{
			Type memberType = LambdaCompiler.GetMemberType(binding.Member);
			if (binding.Member is PropertyInfo && memberType.IsValueType)
			{
				throw Error.CannotAutoInitializeValueTypeMemberThroughProperty(binding.Member);
			}
			if (memberType.IsValueType)
			{
				this.EmitMemberAddress(binding.Member, binding.Member.DeclaringType);
			}
			else
			{
				this.EmitMemberGet(binding.Member, binding.Member.DeclaringType);
			}
			this.EmitMemberInit(binding.Bindings, false, memberType);
		}

		// Token: 0x0600176B RID: 5995 RVA: 0x00050D5C File Offset: 0x0004EF5C
		private void EmitMemberListBinding(MemberListBinding binding)
		{
			Type memberType = LambdaCompiler.GetMemberType(binding.Member);
			if (binding.Member is PropertyInfo && memberType.IsValueType)
			{
				throw Error.CannotAutoInitializeValueTypeElementThroughProperty(binding.Member);
			}
			if (memberType.IsValueType)
			{
				this.EmitMemberAddress(binding.Member, binding.Member.DeclaringType);
			}
			else
			{
				this.EmitMemberGet(binding.Member, binding.Member.DeclaringType);
			}
			this.EmitListInit(binding.Initializers, false, memberType);
		}

		// Token: 0x0600176C RID: 5996 RVA: 0x00050DDC File Offset: 0x0004EFDC
		private void EmitMemberInit(MemberInitExpression init)
		{
			this.EmitExpression(init.NewExpression);
			LocalBuilder localBuilder = null;
			if (init.NewExpression.Type.IsValueType && init.Bindings.Count > 0)
			{
				localBuilder = this._ilg.DeclareLocal(init.NewExpression.Type);
				this._ilg.Emit(OpCodes.Stloc, localBuilder);
				this._ilg.Emit(OpCodes.Ldloca, localBuilder);
			}
			this.EmitMemberInit(init.Bindings, localBuilder == null, init.NewExpression.Type);
			if (localBuilder != null)
			{
				this._ilg.Emit(OpCodes.Ldloc, localBuilder);
			}
		}

		// Token: 0x0600176D RID: 5997 RVA: 0x00050E80 File Offset: 0x0004F080
		private void EmitMemberInit(ReadOnlyCollection<MemberBinding> bindings, bool keepOnStack, Type objectType)
		{
			int count = bindings.Count;
			if (count == 0)
			{
				if (!keepOnStack)
				{
					this._ilg.Emit(OpCodes.Pop);
					return;
				}
			}
			else
			{
				for (int i = 0; i < count; i++)
				{
					if (keepOnStack || i < count - 1)
					{
						this._ilg.Emit(OpCodes.Dup);
					}
					this.EmitBinding(bindings[i], objectType);
				}
			}
		}

		// Token: 0x0600176E RID: 5998 RVA: 0x00050EE0 File Offset: 0x0004F0E0
		private void EmitListInit(ListInitExpression init)
		{
			this.EmitExpression(init.NewExpression);
			LocalBuilder localBuilder = null;
			if (init.NewExpression.Type.IsValueType)
			{
				localBuilder = this._ilg.DeclareLocal(init.NewExpression.Type);
				this._ilg.Emit(OpCodes.Stloc, localBuilder);
				this._ilg.Emit(OpCodes.Ldloca, localBuilder);
			}
			this.EmitListInit(init.Initializers, localBuilder == null, init.NewExpression.Type);
			if (localBuilder != null)
			{
				this._ilg.Emit(OpCodes.Ldloc, localBuilder);
			}
		}

		// Token: 0x0600176F RID: 5999 RVA: 0x00050F78 File Offset: 0x0004F178
		private void EmitListInit(ReadOnlyCollection<ElementInit> initializers, bool keepOnStack, Type objectType)
		{
			int count = initializers.Count;
			if (count == 0)
			{
				if (!keepOnStack)
				{
					this._ilg.Emit(OpCodes.Pop);
					return;
				}
			}
			else
			{
				for (int i = 0; i < count; i++)
				{
					if (keepOnStack || i < count - 1)
					{
						this._ilg.Emit(OpCodes.Dup);
					}
					this.EmitMethodCall(initializers[i].AddMethod, initializers[i], objectType);
					if (initializers[i].AddMethod.ReturnType != typeof(void))
					{
						this._ilg.Emit(OpCodes.Pop);
					}
				}
			}
		}

		// Token: 0x06001770 RID: 6000 RVA: 0x00051018 File Offset: 0x0004F218
		private static Type GetMemberType(MemberInfo member)
		{
			FieldInfo fieldInfo = member as FieldInfo;
			if (fieldInfo != null)
			{
				return fieldInfo.FieldType;
			}
			PropertyInfo propertyInfo = member as PropertyInfo;
			if (propertyInfo != null)
			{
				return propertyInfo.PropertyType;
			}
			throw Error.MemberNotFieldOrProperty(member);
		}

		// Token: 0x06001771 RID: 6001 RVA: 0x0005105C File Offset: 0x0004F25C
		internal static void ValidateLift(IList<ParameterExpression> variables, IList<Expression> arguments)
		{
			if (variables.Count != arguments.Count)
			{
				throw Error.IncorrectNumberOfIndexes();
			}
			int i = 0;
			int count = variables.Count;
			while (i < count)
			{
				if (!TypeUtils.AreReferenceAssignable(variables[i].Type, arguments[i].Type.GetNonNullableType()))
				{
					throw Error.ArgumentTypesMustMatch();
				}
				i++;
			}
		}

		// Token: 0x06001772 RID: 6002 RVA: 0x000510BC File Offset: 0x0004F2BC
		private void EmitLift(ExpressionType nodeType, Type resultType, MethodCallExpression mc, ParameterExpression[] paramList, Expression[] argList)
		{
			switch (nodeType)
			{
			case ExpressionType.Equal:
				goto IL_2CF;
			case ExpressionType.ExclusiveOr:
			case ExpressionType.GreaterThan:
			case ExpressionType.GreaterThanOrEqual:
			case ExpressionType.Invoke:
			case ExpressionType.Lambda:
			case ExpressionType.LeftShift:
			case ExpressionType.LessThan:
			case ExpressionType.LessThanOrEqual:
				break;
			default:
				if (nodeType == ExpressionType.NotEqual)
				{
					goto IL_2CF;
				}
				break;
			}
			IL_35:
			Label label = this._ilg.DefineLabel();
			Label label2 = this._ilg.DefineLabel();
			LocalBuilder local = this._ilg.DeclareLocal(typeof(bool));
			int i = 0;
			int num = paramList.Length;
			while (i < num)
			{
				ParameterExpression variable = paramList[i];
				Expression expression = argList[i];
				if (expression.Type.IsNullableType())
				{
					this._scope.AddLocal(this, variable);
					this.EmitAddress(expression, expression.Type);
					this._ilg.Emit(OpCodes.Dup);
					this._ilg.EmitHasValue(expression.Type);
					this._ilg.Emit(OpCodes.Ldc_I4_0);
					this._ilg.Emit(OpCodes.Ceq);
					this._ilg.Emit(OpCodes.Stloc, local);
					this._ilg.EmitGetValueOrDefault(expression.Type);
					this._scope.EmitSet(variable);
				}
				else
				{
					this._scope.AddLocal(this, variable);
					this.EmitExpression(expression);
					if (!expression.Type.IsValueType)
					{
						this._ilg.Emit(OpCodes.Dup);
						this._ilg.Emit(OpCodes.Ldnull);
						this._ilg.Emit(OpCodes.Ceq);
						this._ilg.Emit(OpCodes.Stloc, local);
					}
					this._scope.EmitSet(variable);
				}
				this._ilg.Emit(OpCodes.Ldloc, local);
				this._ilg.Emit(OpCodes.Brtrue, label2);
				i++;
			}
			this.EmitMethodCallExpression(mc);
			if (resultType.IsNullableType() && !TypeUtils.AreEquivalent(resultType, mc.Type))
			{
				ConstructorInfo constructor = resultType.GetConstructor(new Type[]
				{
					mc.Type
				});
				this._ilg.Emit(OpCodes.Newobj, constructor);
			}
			this._ilg.Emit(OpCodes.Br_S, label);
			this._ilg.MarkLabel(label2);
			if (TypeUtils.AreEquivalent(resultType, TypeUtils.GetNullableType(mc.Type)))
			{
				if (resultType.IsValueType)
				{
					LocalBuilder local2 = this.GetLocal(resultType);
					this._ilg.Emit(OpCodes.Ldloca, local2);
					this._ilg.Emit(OpCodes.Initobj, resultType);
					this._ilg.Emit(OpCodes.Ldloc, local2);
					this.FreeLocal(local2);
				}
				else
				{
					this._ilg.Emit(OpCodes.Ldnull);
				}
			}
			else
			{
				if (nodeType - ExpressionType.GreaterThan > 1 && nodeType - ExpressionType.LessThan > 1)
				{
					throw Error.UnknownLiftType(nodeType);
				}
				this._ilg.Emit(OpCodes.Ldc_I4_0);
			}
			this._ilg.MarkLabel(label);
			return;
			IL_2CF:
			if (!TypeUtils.AreEquivalent(resultType, TypeUtils.GetNullableType(mc.Type)))
			{
				Label label3 = this._ilg.DefineLabel();
				Label label4 = this._ilg.DefineLabel();
				Label label5 = this._ilg.DefineLabel();
				LocalBuilder local3 = this._ilg.DeclareLocal(typeof(bool));
				LocalBuilder local4 = this._ilg.DeclareLocal(typeof(bool));
				this._ilg.Emit(OpCodes.Ldc_I4_0);
				this._ilg.Emit(OpCodes.Stloc, local3);
				this._ilg.Emit(OpCodes.Ldc_I4_1);
				this._ilg.Emit(OpCodes.Stloc, local4);
				int j = 0;
				int num2 = paramList.Length;
				while (j < num2)
				{
					ParameterExpression variable2 = paramList[j];
					Expression expression2 = argList[j];
					this._scope.AddLocal(this, variable2);
					if (expression2.Type.IsNullableType())
					{
						this.EmitAddress(expression2, expression2.Type);
						this._ilg.Emit(OpCodes.Dup);
						this._ilg.EmitHasValue(expression2.Type);
						this._ilg.Emit(OpCodes.Ldc_I4_0);
						this._ilg.Emit(OpCodes.Ceq);
						this._ilg.Emit(OpCodes.Dup);
						this._ilg.Emit(OpCodes.Ldloc, local3);
						this._ilg.Emit(OpCodes.Or);
						this._ilg.Emit(OpCodes.Stloc, local3);
						this._ilg.Emit(OpCodes.Ldloc, local4);
						this._ilg.Emit(OpCodes.And);
						this._ilg.Emit(OpCodes.Stloc, local4);
						this._ilg.EmitGetValueOrDefault(expression2.Type);
					}
					else
					{
						this.EmitExpression(expression2);
						if (!expression2.Type.IsValueType)
						{
							this._ilg.Emit(OpCodes.Dup);
							this._ilg.Emit(OpCodes.Ldnull);
							this._ilg.Emit(OpCodes.Ceq);
							this._ilg.Emit(OpCodes.Dup);
							this._ilg.Emit(OpCodes.Ldloc, local3);
							this._ilg.Emit(OpCodes.Or);
							this._ilg.Emit(OpCodes.Stloc, local3);
							this._ilg.Emit(OpCodes.Ldloc, local4);
							this._ilg.Emit(OpCodes.And);
							this._ilg.Emit(OpCodes.Stloc, local4);
						}
						else
						{
							this._ilg.Emit(OpCodes.Ldc_I4_0);
							this._ilg.Emit(OpCodes.Stloc, local4);
						}
					}
					this._scope.EmitSet(variable2);
					j++;
				}
				this._ilg.Emit(OpCodes.Ldloc, local4);
				this._ilg.Emit(OpCodes.Brtrue, label4);
				this._ilg.Emit(OpCodes.Ldloc, local3);
				this._ilg.Emit(OpCodes.Brtrue, label5);
				this.EmitMethodCallExpression(mc);
				if (resultType.IsNullableType() && !TypeUtils.AreEquivalent(resultType, mc.Type))
				{
					ConstructorInfo constructor2 = resultType.GetConstructor(new Type[]
					{
						mc.Type
					});
					this._ilg.Emit(OpCodes.Newobj, constructor2);
				}
				this._ilg.Emit(OpCodes.Br_S, label3);
				this._ilg.MarkLabel(label4);
				this._ilg.EmitBoolean(nodeType == ExpressionType.Equal);
				this._ilg.Emit(OpCodes.Br_S, label3);
				this._ilg.MarkLabel(label5);
				this._ilg.EmitBoolean(nodeType == ExpressionType.NotEqual);
				this._ilg.MarkLabel(label3);
				return;
			}
			goto IL_35;
		}

		// Token: 0x06001773 RID: 6003 RVA: 0x0005175C File Offset: 0x0004F95C
		private void EmitExpression(Expression node, LambdaCompiler.CompilationFlags flags)
		{
			if (!this._guard.TryEnterOnCurrentStack())
			{
				this._guard.RunOnEmptyStack<LambdaCompiler, Expression, LambdaCompiler.CompilationFlags>(delegate(LambdaCompiler @this, Expression n, LambdaCompiler.CompilationFlags f)
				{
					@this.EmitExpression(n, f);
				}, this, node, flags);
				return;
			}
			bool flag = (flags & LambdaCompiler.CompilationFlags.EmitExpressionStartMask) == LambdaCompiler.CompilationFlags.EmitExpressionStart;
			LambdaCompiler.CompilationFlags flags2 = flag ? this.EmitExpressionStart(node) : LambdaCompiler.CompilationFlags.EmitNoExpressionStart;
			flags &= LambdaCompiler.CompilationFlags.EmitAsTailCallMask;
			switch (node.NodeType)
			{
			case ExpressionType.Add:
				this.EmitBinaryExpression(node, flags);
				goto IL_4ED;
			case ExpressionType.AddChecked:
				this.EmitBinaryExpression(node, flags);
				goto IL_4ED;
			case ExpressionType.And:
				this.EmitBinaryExpression(node, flags);
				goto IL_4ED;
			case ExpressionType.AndAlso:
				this.EmitAndAlsoBinaryExpression(node, flags);
				goto IL_4ED;
			case ExpressionType.ArrayLength:
				this.EmitUnaryExpression(node, flags);
				goto IL_4ED;
			case ExpressionType.ArrayIndex:
				this.EmitBinaryExpression(node, flags);
				goto IL_4ED;
			case ExpressionType.Call:
				this.EmitMethodCallExpression(node, flags);
				goto IL_4ED;
			case ExpressionType.Coalesce:
				this.EmitCoalesceBinaryExpression(node);
				goto IL_4ED;
			case ExpressionType.Conditional:
				this.EmitConditionalExpression(node, flags);
				goto IL_4ED;
			case ExpressionType.Constant:
				this.EmitConstantExpression(node);
				goto IL_4ED;
			case ExpressionType.Convert:
				this.EmitConvertUnaryExpression(node, flags);
				goto IL_4ED;
			case ExpressionType.ConvertChecked:
				this.EmitConvertUnaryExpression(node, flags);
				goto IL_4ED;
			case ExpressionType.Divide:
				this.EmitBinaryExpression(node, flags);
				goto IL_4ED;
			case ExpressionType.Equal:
				this.EmitBinaryExpression(node, flags);
				goto IL_4ED;
			case ExpressionType.ExclusiveOr:
				this.EmitBinaryExpression(node, flags);
				goto IL_4ED;
			case ExpressionType.GreaterThan:
				this.EmitBinaryExpression(node, flags);
				goto IL_4ED;
			case ExpressionType.GreaterThanOrEqual:
				this.EmitBinaryExpression(node, flags);
				goto IL_4ED;
			case ExpressionType.Invoke:
				this.EmitInvocationExpression(node, flags);
				goto IL_4ED;
			case ExpressionType.Lambda:
				this.EmitLambdaExpression(node);
				goto IL_4ED;
			case ExpressionType.LeftShift:
				this.EmitBinaryExpression(node, flags);
				goto IL_4ED;
			case ExpressionType.LessThan:
				this.EmitBinaryExpression(node, flags);
				goto IL_4ED;
			case ExpressionType.LessThanOrEqual:
				this.EmitBinaryExpression(node, flags);
				goto IL_4ED;
			case ExpressionType.ListInit:
				this.EmitListInitExpression(node);
				goto IL_4ED;
			case ExpressionType.MemberAccess:
				this.EmitMemberExpression(node);
				goto IL_4ED;
			case ExpressionType.MemberInit:
				this.EmitMemberInitExpression(node);
				goto IL_4ED;
			case ExpressionType.Modulo:
				this.EmitBinaryExpression(node, flags);
				goto IL_4ED;
			case ExpressionType.Multiply:
				this.EmitBinaryExpression(node, flags);
				goto IL_4ED;
			case ExpressionType.MultiplyChecked:
				this.EmitBinaryExpression(node, flags);
				goto IL_4ED;
			case ExpressionType.Negate:
				this.EmitUnaryExpression(node, flags);
				goto IL_4ED;
			case ExpressionType.UnaryPlus:
				this.EmitUnaryExpression(node, flags);
				goto IL_4ED;
			case ExpressionType.NegateChecked:
				this.EmitUnaryExpression(node, flags);
				goto IL_4ED;
			case ExpressionType.New:
				this.EmitNewExpression(node);
				goto IL_4ED;
			case ExpressionType.NewArrayInit:
				this.EmitNewArrayExpression(node);
				goto IL_4ED;
			case ExpressionType.NewArrayBounds:
				this.EmitNewArrayExpression(node);
				goto IL_4ED;
			case ExpressionType.Not:
				this.EmitUnaryExpression(node, flags);
				goto IL_4ED;
			case ExpressionType.NotEqual:
				this.EmitBinaryExpression(node, flags);
				goto IL_4ED;
			case ExpressionType.Or:
				this.EmitBinaryExpression(node, flags);
				goto IL_4ED;
			case ExpressionType.OrElse:
				this.EmitOrElseBinaryExpression(node, flags);
				goto IL_4ED;
			case ExpressionType.Parameter:
				this.EmitParameterExpression(node);
				goto IL_4ED;
			case ExpressionType.Power:
				this.EmitBinaryExpression(node, flags);
				goto IL_4ED;
			case ExpressionType.Quote:
				this.EmitQuoteUnaryExpression(node);
				goto IL_4ED;
			case ExpressionType.RightShift:
				this.EmitBinaryExpression(node, flags);
				goto IL_4ED;
			case ExpressionType.Subtract:
				this.EmitBinaryExpression(node, flags);
				goto IL_4ED;
			case ExpressionType.SubtractChecked:
				this.EmitBinaryExpression(node, flags);
				goto IL_4ED;
			case ExpressionType.TypeAs:
				this.EmitUnaryExpression(node, flags);
				goto IL_4ED;
			case ExpressionType.TypeIs:
				this.EmitTypeBinaryExpression(node);
				goto IL_4ED;
			case ExpressionType.Assign:
				this.EmitAssignBinaryExpression(node);
				goto IL_4ED;
			case ExpressionType.Block:
				this.EmitBlockExpression(node, flags);
				goto IL_4ED;
			case ExpressionType.DebugInfo:
				this.EmitDebugInfoExpression(node);
				goto IL_4ED;
			case ExpressionType.Decrement:
				this.EmitUnaryExpression(node, flags);
				goto IL_4ED;
			case ExpressionType.Dynamic:
				this.EmitDynamicExpression(node);
				goto IL_4ED;
			case ExpressionType.Default:
				this.EmitDefaultExpression(node);
				goto IL_4ED;
			case ExpressionType.Extension:
				LambdaCompiler.EmitExtensionExpression(node);
				goto IL_4ED;
			case ExpressionType.Goto:
				this.EmitGotoExpression(node, flags);
				goto IL_4ED;
			case ExpressionType.Increment:
				this.EmitUnaryExpression(node, flags);
				goto IL_4ED;
			case ExpressionType.Index:
				this.EmitIndexExpression(node);
				goto IL_4ED;
			case ExpressionType.Label:
				this.EmitLabelExpression(node, flags);
				goto IL_4ED;
			case ExpressionType.RuntimeVariables:
				this.EmitRuntimeVariablesExpression(node);
				goto IL_4ED;
			case ExpressionType.Loop:
				this.EmitLoopExpression(node);
				goto IL_4ED;
			case ExpressionType.Switch:
				this.EmitSwitchExpression(node, flags);
				goto IL_4ED;
			case ExpressionType.Throw:
				this.EmitThrowUnaryExpression(node);
				goto IL_4ED;
			case ExpressionType.Try:
				this.EmitTryExpression(node);
				goto IL_4ED;
			case ExpressionType.Unbox:
				this.EmitUnboxUnaryExpression(node);
				goto IL_4ED;
			case ExpressionType.TypeEqual:
				this.EmitTypeBinaryExpression(node);
				goto IL_4ED;
			case ExpressionType.OnesComplement:
				this.EmitUnaryExpression(node, flags);
				goto IL_4ED;
			case ExpressionType.IsTrue:
				this.EmitUnaryExpression(node, flags);
				goto IL_4ED;
			case ExpressionType.IsFalse:
				this.EmitUnaryExpression(node, flags);
				goto IL_4ED;
			}
			throw ContractUtils.Unreachable;
			IL_4ED:
			if (flag)
			{
				this.EmitExpressionEnd(flags2);
			}
		}

		// Token: 0x06001774 RID: 6004 RVA: 0x00051C60 File Offset: 0x0004FE60
		private static bool IsChecked(ExpressionType op)
		{
			if (op <= ExpressionType.MultiplyChecked)
			{
				if (op != ExpressionType.AddChecked && op != ExpressionType.ConvertChecked && op != ExpressionType.MultiplyChecked)
				{
					return false;
				}
			}
			else if (op != ExpressionType.NegateChecked && op != ExpressionType.SubtractChecked && op - ExpressionType.AddAssignChecked > 2)
			{
				return false;
			}
			return true;
		}

		// Token: 0x06001775 RID: 6005 RVA: 0x00051C8C File Offset: 0x0004FE8C
		internal void EmitConstantArray<T>(T[] array)
		{
			if (this._method is DynamicMethod)
			{
				this.EmitConstant(array, typeof(T[]));
				return;
			}
			if (this._typeBuilder != null)
			{
				FieldBuilder field = this.CreateStaticField("ConstantArray", typeof(T[]));
				Label label = this._ilg.DefineLabel();
				this._ilg.Emit(OpCodes.Ldsfld, field);
				this._ilg.Emit(OpCodes.Ldnull);
				this._ilg.Emit(OpCodes.Bne_Un, label);
				this._ilg.EmitArray(array);
				this._ilg.Emit(OpCodes.Stsfld, field);
				this._ilg.MarkLabel(label);
				this._ilg.Emit(OpCodes.Ldsfld, field);
				return;
			}
			this._ilg.EmitArray(array);
		}

		// Token: 0x06001776 RID: 6006 RVA: 0x00051D64 File Offset: 0x0004FF64
		private void EmitClosureCreation(LambdaCompiler inner)
		{
			bool needsClosure = inner._scope.NeedsClosure;
			bool flag = inner._boundConstants.Count > 0;
			if (!needsClosure && !flag)
			{
				this._ilg.EmitNull();
				return;
			}
			if (flag)
			{
				this._boundConstants.EmitConstant(this, inner._boundConstants.ToArray(), typeof(object[]));
			}
			else
			{
				this._ilg.EmitNull();
			}
			if (needsClosure)
			{
				this._scope.EmitGet(this._scope.NearestHoistedLocals.SelfVariable);
			}
			else
			{
				this._ilg.EmitNull();
			}
			this._ilg.EmitNew(typeof(Closure).GetConstructor(new Type[]
			{
				typeof(object[]),
				typeof(object[])
			}));
		}

		// Token: 0x06001777 RID: 6007 RVA: 0x00051E34 File Offset: 0x00050034
		private void EmitDelegateConstruction(LambdaCompiler inner)
		{
			Type type = inner._lambda.Type;
			DynamicMethod dynamicMethod = inner._method as DynamicMethod;
			if (dynamicMethod != null)
			{
				this._boundConstants.EmitConstant(this, dynamicMethod, typeof(MethodInfo));
				this._ilg.EmitType(type);
				this.EmitClosureCreation(inner);
				this._ilg.Emit(OpCodes.Callvirt, typeof(MethodInfo).GetMethod("CreateDelegate", new Type[]
				{
					typeof(Type),
					typeof(object)
				}));
				this._ilg.Emit(OpCodes.Castclass, type);
				return;
			}
			this.EmitClosureCreation(inner);
			this._ilg.Emit(OpCodes.Ldftn, inner._method);
			this._ilg.Emit(OpCodes.Newobj, (ConstructorInfo)type.GetMember(".ctor")[0]);
		}

		// Token: 0x06001778 RID: 6008 RVA: 0x00051F24 File Offset: 0x00050124
		private void EmitDelegateConstruction(LambdaExpression lambda)
		{
			LambdaCompiler lambdaCompiler;
			if (this._method is DynamicMethod)
			{
				lambdaCompiler = new LambdaCompiler(this._tree, lambda);
			}
			else
			{
				string name = string.IsNullOrEmpty(lambda.Name) ? LambdaCompiler.GetUniqueMethodName() : lambda.Name;
				MethodBuilder method = this._typeBuilder.DefineMethod(name, MethodAttributes.Private | MethodAttributes.Static);
				lambdaCompiler = new LambdaCompiler(this._tree, lambda, method);
			}
			lambdaCompiler.EmitLambdaBody(this._scope, false, LambdaCompiler.CompilationFlags.EmitAsNoTail);
			this.EmitDelegateConstruction(lambdaCompiler);
		}

		// Token: 0x06001779 RID: 6009 RVA: 0x00051F9E File Offset: 0x0005019E
		private static Type[] GetParameterTypes(LambdaExpression lambda)
		{
			return lambda.Parameters.Map(delegate(ParameterExpression p)
			{
				if (!p.IsByRef)
				{
					return p.Type;
				}
				return p.Type.MakeByRefType();
			});
		}

		// Token: 0x0600177A RID: 6010 RVA: 0x00051FCC File Offset: 0x000501CC
		private static string GetUniqueMethodName()
		{
			return "<ExpressionCompilerImplementationDetails>{" + Interlocked.Increment(ref LambdaCompiler._Counter).ToString() + "}lambda_method";
		}

		// Token: 0x0600177B RID: 6011 RVA: 0x00051FFC File Offset: 0x000501FC
		private void EmitLambdaBody()
		{
			LambdaCompiler.CompilationFlags flags = this._lambda.TailCall ? LambdaCompiler.CompilationFlags.EmitAsTail : LambdaCompiler.CompilationFlags.EmitAsNoTail;
			this.EmitLambdaBody(null, false, flags);
		}

		// Token: 0x0600177C RID: 6012 RVA: 0x0005202C File Offset: 0x0005022C
		private void EmitLambdaBody(CompilerScope parent, bool inlined, LambdaCompiler.CompilationFlags flags)
		{
			this._scope.Enter(this, parent);
			if (inlined)
			{
				for (int i = this._lambda.Parameters.Count - 1; i >= 0; i--)
				{
					this._scope.EmitSet(this._lambda.Parameters[i]);
				}
			}
			flags = LambdaCompiler.UpdateEmitExpressionStartFlag(flags, LambdaCompiler.CompilationFlags.EmitExpressionStart);
			if (this._lambda.ReturnType == typeof(void))
			{
				this.EmitExpressionAsVoid(this._lambda.Body, flags);
			}
			else
			{
				this.EmitExpression(this._lambda.Body, flags);
			}
			if (!inlined)
			{
				this._ilg.Emit(OpCodes.Ret);
			}
			this._scope.Exit();
			foreach (LabelInfo labelInfo in this._labelInfo.Values)
			{
				labelInfo.ValidateFinish();
			}
		}

		// Token: 0x0600177D RID: 6013 RVA: 0x00052138 File Offset: 0x00050338
		private void EmitConditionalExpression(Expression expr, LambdaCompiler.CompilationFlags flags)
		{
			ConditionalExpression conditionalExpression = (ConditionalExpression)expr;
			Label label = this._ilg.DefineLabel();
			this.EmitExpressionAndBranch(false, conditionalExpression.Test, label);
			this.EmitExpressionAsType(conditionalExpression.IfTrue, conditionalExpression.Type, flags);
			if (LambdaCompiler.NotEmpty(conditionalExpression.IfFalse))
			{
				Label label2 = this._ilg.DefineLabel();
				if ((flags & LambdaCompiler.CompilationFlags.EmitAsTailCallMask) == LambdaCompiler.CompilationFlags.EmitAsTail)
				{
					this._ilg.Emit(OpCodes.Ret);
				}
				else
				{
					this._ilg.Emit(OpCodes.Br, label2);
				}
				this._ilg.MarkLabel(label);
				this.EmitExpressionAsType(conditionalExpression.IfFalse, conditionalExpression.Type, flags);
				this._ilg.MarkLabel(label2);
				return;
			}
			this._ilg.MarkLabel(label);
		}

		// Token: 0x0600177E RID: 6014 RVA: 0x000521FC File Offset: 0x000503FC
		private static bool NotEmpty(Expression node)
		{
			DefaultExpression defaultExpression = node as DefaultExpression;
			return defaultExpression == null || defaultExpression.Type != typeof(void);
		}

		// Token: 0x0600177F RID: 6015 RVA: 0x00052230 File Offset: 0x00050430
		private static bool Significant(Expression node)
		{
			BlockExpression blockExpression = node as BlockExpression;
			if (blockExpression != null)
			{
				for (int i = 0; i < blockExpression.ExpressionCount; i++)
				{
					if (LambdaCompiler.Significant(blockExpression.GetExpression(i)))
					{
						return true;
					}
				}
				return false;
			}
			return LambdaCompiler.NotEmpty(node) && !(node is DebugInfoExpression);
		}

		// Token: 0x06001780 RID: 6016 RVA: 0x00052280 File Offset: 0x00050480
		private void EmitCoalesceBinaryExpression(Expression expr)
		{
			BinaryExpression binaryExpression = (BinaryExpression)expr;
			if (binaryExpression.Left.Type.IsNullableType())
			{
				this.EmitNullableCoalesce(binaryExpression);
				return;
			}
			if (binaryExpression.Left.Type.IsValueType)
			{
				throw Error.CoalesceUsedOnNonNullType();
			}
			if (binaryExpression.Conversion != null)
			{
				this.EmitLambdaReferenceCoalesce(binaryExpression);
				return;
			}
			this.EmitReferenceCoalesceWithoutConversion(binaryExpression);
		}

		// Token: 0x06001781 RID: 6017 RVA: 0x000522E0 File Offset: 0x000504E0
		private void EmitNullableCoalesce(BinaryExpression b)
		{
			LocalBuilder local = this.GetLocal(b.Left.Type);
			Label label = this._ilg.DefineLabel();
			Label label2 = this._ilg.DefineLabel();
			this.EmitExpression(b.Left);
			this._ilg.Emit(OpCodes.Stloc, local);
			this._ilg.Emit(OpCodes.Ldloca, local);
			this._ilg.EmitHasValue(b.Left.Type);
			this._ilg.Emit(OpCodes.Brfalse, label);
			Type nonNullableType = b.Left.Type.GetNonNullableType();
			if (b.Conversion != null)
			{
				ParameterExpression parameterExpression = b.Conversion.Parameters[0];
				this.EmitLambdaExpression(b.Conversion);
				if (!parameterExpression.Type.IsAssignableFrom(b.Left.Type))
				{
					this._ilg.Emit(OpCodes.Ldloca, local);
					this._ilg.EmitGetValueOrDefault(b.Left.Type);
				}
				else
				{
					this._ilg.Emit(OpCodes.Ldloc, local);
				}
				this._ilg.Emit(OpCodes.Callvirt, b.Conversion.Type.GetMethod("Invoke"));
			}
			else if (!TypeUtils.AreEquivalent(b.Type, nonNullableType))
			{
				this._ilg.Emit(OpCodes.Ldloca, local);
				this._ilg.EmitGetValueOrDefault(b.Left.Type);
				this._ilg.EmitConvertToType(nonNullableType, b.Type, true);
			}
			else
			{
				this._ilg.Emit(OpCodes.Ldloca, local);
				this._ilg.EmitGetValueOrDefault(b.Left.Type);
			}
			this.FreeLocal(local);
			this._ilg.Emit(OpCodes.Br, label2);
			this._ilg.MarkLabel(label);
			this.EmitExpression(b.Right);
			if (!TypeUtils.AreEquivalent(b.Right.Type, b.Type))
			{
				this._ilg.EmitConvertToType(b.Right.Type, b.Type, true);
			}
			this._ilg.MarkLabel(label2);
		}

		// Token: 0x06001782 RID: 6018 RVA: 0x00052504 File Offset: 0x00050704
		private void EmitLambdaReferenceCoalesce(BinaryExpression b)
		{
			LocalBuilder local = this.GetLocal(b.Left.Type);
			Label label = this._ilg.DefineLabel();
			Label label2 = this._ilg.DefineLabel();
			this.EmitExpression(b.Left);
			this._ilg.Emit(OpCodes.Dup);
			this._ilg.Emit(OpCodes.Stloc, local);
			this._ilg.Emit(OpCodes.Ldnull);
			this._ilg.Emit(OpCodes.Ceq);
			this._ilg.Emit(OpCodes.Brfalse, label2);
			this.EmitExpression(b.Right);
			this._ilg.Emit(OpCodes.Br, label);
			this._ilg.MarkLabel(label2);
			this.EmitLambdaExpression(b.Conversion);
			this._ilg.Emit(OpCodes.Ldloc, local);
			this.FreeLocal(local);
			this._ilg.Emit(OpCodes.Callvirt, b.Conversion.Type.GetMethod("Invoke"));
			this._ilg.MarkLabel(label);
		}

		// Token: 0x06001783 RID: 6019 RVA: 0x00052618 File Offset: 0x00050818
		private void EmitReferenceCoalesceWithoutConversion(BinaryExpression b)
		{
			Label label = this._ilg.DefineLabel();
			Label label2 = this._ilg.DefineLabel();
			this.EmitExpression(b.Left);
			this._ilg.Emit(OpCodes.Dup);
			this._ilg.Emit(OpCodes.Ldnull);
			this._ilg.Emit(OpCodes.Ceq);
			this._ilg.Emit(OpCodes.Brfalse, label2);
			this._ilg.Emit(OpCodes.Pop);
			this.EmitExpression(b.Right);
			if (!TypeUtils.AreEquivalent(b.Right.Type, b.Type))
			{
				if (b.Right.Type.IsValueType)
				{
					this._ilg.Emit(OpCodes.Box, b.Right.Type);
				}
				this._ilg.Emit(OpCodes.Castclass, b.Type);
			}
			this._ilg.Emit(OpCodes.Br_S, label);
			this._ilg.MarkLabel(label2);
			if (!TypeUtils.AreEquivalent(b.Left.Type, b.Type))
			{
				this._ilg.Emit(OpCodes.Castclass, b.Type);
			}
			this._ilg.MarkLabel(label);
		}

		// Token: 0x06001784 RID: 6020 RVA: 0x00052758 File Offset: 0x00050958
		private void EmitLiftedAndAlso(BinaryExpression b)
		{
			Type typeFromHandle = typeof(bool?);
			Label label = this._ilg.DefineLabel();
			Label label2 = this._ilg.DefineLabel();
			Label label3 = this._ilg.DefineLabel();
			Label label4 = this._ilg.DefineLabel();
			Label label5 = this._ilg.DefineLabel();
			LocalBuilder local = this.GetLocal(typeFromHandle);
			LocalBuilder local2 = this.GetLocal(typeFromHandle);
			this.EmitExpression(b.Left);
			this._ilg.Emit(OpCodes.Stloc, local);
			this._ilg.Emit(OpCodes.Ldloca, local);
			this._ilg.EmitHasValue(typeFromHandle);
			this._ilg.Emit(OpCodes.Brfalse, label);
			this._ilg.Emit(OpCodes.Ldloca, local);
			this._ilg.EmitGetValueOrDefault(typeFromHandle);
			this._ilg.Emit(OpCodes.Ldc_I4_0);
			this._ilg.Emit(OpCodes.Ceq);
			this._ilg.Emit(OpCodes.Brtrue, label2);
			this._ilg.MarkLabel(label);
			this.EmitExpression(b.Right);
			this._ilg.Emit(OpCodes.Stloc, local2);
			this._ilg.Emit(OpCodes.Ldloca, local2);
			this._ilg.EmitHasValue(typeFromHandle);
			this._ilg.Emit(OpCodes.Brfalse_S, label3);
			this._ilg.Emit(OpCodes.Ldloca, local2);
			this._ilg.EmitGetValueOrDefault(typeFromHandle);
			this._ilg.Emit(OpCodes.Ldc_I4_0);
			this._ilg.Emit(OpCodes.Ceq);
			this._ilg.Emit(OpCodes.Brtrue_S, label2);
			this._ilg.Emit(OpCodes.Ldloca, local);
			this._ilg.EmitHasValue(typeFromHandle);
			this._ilg.Emit(OpCodes.Brfalse, label3);
			this._ilg.Emit(OpCodes.Ldc_I4_1);
			this._ilg.Emit(OpCodes.Br_S, label4);
			this._ilg.MarkLabel(label2);
			this._ilg.Emit(OpCodes.Ldc_I4_0);
			this._ilg.Emit(OpCodes.Br_S, label4);
			this._ilg.MarkLabel(label4);
			ConstructorInfo constructor = typeFromHandle.GetConstructor(new Type[]
			{
				typeof(bool)
			});
			this._ilg.Emit(OpCodes.Newobj, constructor);
			this._ilg.Emit(OpCodes.Stloc, local);
			this._ilg.Emit(OpCodes.Br, label5);
			this._ilg.MarkLabel(label3);
			this._ilg.Emit(OpCodes.Ldloca, local);
			this._ilg.Emit(OpCodes.Initobj, typeFromHandle);
			this._ilg.MarkLabel(label5);
			this._ilg.Emit(OpCodes.Ldloc, local);
			this.FreeLocal(local);
			this.FreeLocal(local2);
		}

		// Token: 0x06001785 RID: 6021 RVA: 0x00052A40 File Offset: 0x00050C40
		private void EmitMethodAndAlso(BinaryExpression b, LambdaCompiler.CompilationFlags flags)
		{
			Label label = this._ilg.DefineLabel();
			this.EmitExpression(b.Left);
			this._ilg.Emit(OpCodes.Dup);
			MethodInfo booleanOperator = TypeUtils.GetBooleanOperator(b.Method.DeclaringType, "op_False");
			this._ilg.Emit(OpCodes.Call, booleanOperator);
			this._ilg.Emit(OpCodes.Brtrue, label);
			LocalBuilder local = this.GetLocal(b.Left.Type);
			this._ilg.Emit(OpCodes.Stloc, local);
			this.EmitExpression(b.Right);
			LocalBuilder local2 = this.GetLocal(b.Right.Type);
			this._ilg.Emit(OpCodes.Stloc, local2);
			this._ilg.Emit(OpCodes.Ldloc, local);
			this._ilg.Emit(OpCodes.Ldloc, local2);
			if ((flags & LambdaCompiler.CompilationFlags.EmitAsTailCallMask) == LambdaCompiler.CompilationFlags.EmitAsTail)
			{
				this._ilg.Emit(OpCodes.Tailcall);
			}
			this._ilg.Emit(OpCodes.Call, b.Method);
			this.FreeLocal(local);
			this.FreeLocal(local2);
			this._ilg.MarkLabel(label);
		}

		// Token: 0x06001786 RID: 6022 RVA: 0x00052B70 File Offset: 0x00050D70
		private void EmitUnliftedAndAlso(BinaryExpression b)
		{
			Label label = this._ilg.DefineLabel();
			Label label2 = this._ilg.DefineLabel();
			this.EmitExpressionAndBranch(false, b.Left, label);
			this.EmitExpression(b.Right);
			this._ilg.Emit(OpCodes.Br, label2);
			this._ilg.MarkLabel(label);
			this._ilg.Emit(OpCodes.Ldc_I4_0);
			this._ilg.MarkLabel(label2);
		}

		// Token: 0x06001787 RID: 6023 RVA: 0x00052BE8 File Offset: 0x00050DE8
		private void EmitAndAlsoBinaryExpression(Expression expr, LambdaCompiler.CompilationFlags flags)
		{
			BinaryExpression binaryExpression = (BinaryExpression)expr;
			if (binaryExpression.Method != null && !binaryExpression.IsLiftedLogical)
			{
				this.EmitMethodAndAlso(binaryExpression, flags);
				return;
			}
			if (binaryExpression.Left.Type == typeof(bool?))
			{
				this.EmitLiftedAndAlso(binaryExpression);
				return;
			}
			if (binaryExpression.IsLiftedLogical)
			{
				this.EmitExpression(binaryExpression.ReduceUserdefinedLifted());
				return;
			}
			this.EmitUnliftedAndAlso(binaryExpression);
		}

		// Token: 0x06001788 RID: 6024 RVA: 0x00052C5C File Offset: 0x00050E5C
		private void EmitLiftedOrElse(BinaryExpression b)
		{
			Type typeFromHandle = typeof(bool?);
			Label label = this._ilg.DefineLabel();
			Label label2 = this._ilg.DefineLabel();
			Label label3 = this._ilg.DefineLabel();
			Label label4 = this._ilg.DefineLabel();
			Label label5 = this._ilg.DefineLabel();
			LocalBuilder local = this.GetLocal(typeFromHandle);
			LocalBuilder local2 = this.GetLocal(typeFromHandle);
			this.EmitExpression(b.Left);
			this._ilg.Emit(OpCodes.Stloc, local);
			this._ilg.Emit(OpCodes.Ldloca, local);
			this._ilg.EmitHasValue(typeFromHandle);
			this._ilg.Emit(OpCodes.Brfalse, label);
			this._ilg.Emit(OpCodes.Ldloca, local);
			this._ilg.EmitGetValueOrDefault(typeFromHandle);
			this._ilg.Emit(OpCodes.Ldc_I4_0);
			this._ilg.Emit(OpCodes.Ceq);
			this._ilg.Emit(OpCodes.Brfalse, label2);
			this._ilg.MarkLabel(label);
			this.EmitExpression(b.Right);
			this._ilg.Emit(OpCodes.Stloc, local2);
			this._ilg.Emit(OpCodes.Ldloca, local2);
			this._ilg.EmitHasValue(typeFromHandle);
			this._ilg.Emit(OpCodes.Brfalse_S, label3);
			this._ilg.Emit(OpCodes.Ldloca, local2);
			this._ilg.EmitGetValueOrDefault(typeFromHandle);
			this._ilg.Emit(OpCodes.Ldc_I4_0);
			this._ilg.Emit(OpCodes.Ceq);
			this._ilg.Emit(OpCodes.Brfalse_S, label2);
			this._ilg.Emit(OpCodes.Ldloca, local);
			this._ilg.EmitHasValue(typeFromHandle);
			this._ilg.Emit(OpCodes.Brfalse, label3);
			this._ilg.Emit(OpCodes.Ldc_I4_0);
			this._ilg.Emit(OpCodes.Br_S, label4);
			this._ilg.MarkLabel(label2);
			this._ilg.Emit(OpCodes.Ldc_I4_1);
			this._ilg.Emit(OpCodes.Br_S, label4);
			this._ilg.MarkLabel(label4);
			ConstructorInfo constructor = typeFromHandle.GetConstructor(new Type[]
			{
				typeof(bool)
			});
			this._ilg.Emit(OpCodes.Newobj, constructor);
			this._ilg.Emit(OpCodes.Stloc, local);
			this._ilg.Emit(OpCodes.Br, label5);
			this._ilg.MarkLabel(label3);
			this._ilg.Emit(OpCodes.Ldloca, local);
			this._ilg.Emit(OpCodes.Initobj, typeFromHandle);
			this._ilg.MarkLabel(label5);
			this._ilg.Emit(OpCodes.Ldloc, local);
			this.FreeLocal(local);
			this.FreeLocal(local2);
		}

		// Token: 0x06001789 RID: 6025 RVA: 0x00052F44 File Offset: 0x00051144
		private void EmitUnliftedOrElse(BinaryExpression b)
		{
			Label label = this._ilg.DefineLabel();
			Label label2 = this._ilg.DefineLabel();
			this.EmitExpressionAndBranch(false, b.Left, label);
			this._ilg.Emit(OpCodes.Ldc_I4_1);
			this._ilg.Emit(OpCodes.Br, label2);
			this._ilg.MarkLabel(label);
			this.EmitExpression(b.Right);
			this._ilg.MarkLabel(label2);
		}

		// Token: 0x0600178A RID: 6026 RVA: 0x00052FBC File Offset: 0x000511BC
		private void EmitMethodOrElse(BinaryExpression b, LambdaCompiler.CompilationFlags flags)
		{
			Label label = this._ilg.DefineLabel();
			this.EmitExpression(b.Left);
			this._ilg.Emit(OpCodes.Dup);
			MethodInfo booleanOperator = TypeUtils.GetBooleanOperator(b.Method.DeclaringType, "op_True");
			this._ilg.Emit(OpCodes.Call, booleanOperator);
			this._ilg.Emit(OpCodes.Brtrue, label);
			LocalBuilder local = this.GetLocal(b.Left.Type);
			this._ilg.Emit(OpCodes.Stloc, local);
			this.EmitExpression(b.Right);
			LocalBuilder local2 = this.GetLocal(b.Right.Type);
			this._ilg.Emit(OpCodes.Stloc, local2);
			this._ilg.Emit(OpCodes.Ldloc, local);
			this._ilg.Emit(OpCodes.Ldloc, local2);
			if ((flags & LambdaCompiler.CompilationFlags.EmitAsTailCallMask) == LambdaCompiler.CompilationFlags.EmitAsTail)
			{
				this._ilg.Emit(OpCodes.Tailcall);
			}
			this._ilg.Emit(OpCodes.Call, b.Method);
			this.FreeLocal(local);
			this.FreeLocal(local2);
			this._ilg.MarkLabel(label);
		}

		// Token: 0x0600178B RID: 6027 RVA: 0x000530EC File Offset: 0x000512EC
		private void EmitOrElseBinaryExpression(Expression expr, LambdaCompiler.CompilationFlags flags)
		{
			BinaryExpression binaryExpression = (BinaryExpression)expr;
			if (binaryExpression.Method != null && !binaryExpression.IsLiftedLogical)
			{
				this.EmitMethodOrElse(binaryExpression, flags);
				return;
			}
			if (binaryExpression.Left.Type == typeof(bool?))
			{
				this.EmitLiftedOrElse(binaryExpression);
				return;
			}
			if (binaryExpression.IsLiftedLogical)
			{
				this.EmitExpression(binaryExpression.ReduceUserdefinedLifted());
				return;
			}
			this.EmitUnliftedOrElse(binaryExpression);
		}

		// Token: 0x0600178C RID: 6028 RVA: 0x00053160 File Offset: 0x00051360
		private void EmitExpressionAndBranch(bool branchValue, Expression node, Label label)
		{
			LambdaCompiler.CompilationFlags flags = this.EmitExpressionStart(node);
			try
			{
				if (node.Type == typeof(bool))
				{
					ExpressionType nodeType = node.NodeType;
					if (nodeType <= ExpressionType.Equal)
					{
						if (nodeType != ExpressionType.AndAlso)
						{
							if (nodeType != ExpressionType.Equal)
							{
								goto IL_96;
							}
							goto IL_86;
						}
					}
					else
					{
						switch (nodeType)
						{
						case ExpressionType.Not:
							this.EmitBranchNot(branchValue, (UnaryExpression)node, label);
							return;
						case ExpressionType.NotEqual:
							goto IL_86;
						case ExpressionType.Or:
							goto IL_96;
						case ExpressionType.OrElse:
							break;
						default:
							if (nodeType != ExpressionType.Block)
							{
								goto IL_96;
							}
							this.EmitBranchBlock(branchValue, (BlockExpression)node, label);
							return;
						}
					}
					this.EmitBranchLogical(branchValue, (BinaryExpression)node, label);
					return;
					IL_86:
					this.EmitBranchComparison(branchValue, (BinaryExpression)node, label);
					return;
				}
				IL_96:
				this.EmitExpression(node, LambdaCompiler.CompilationFlags.EmitNoExpressionStart | LambdaCompiler.CompilationFlags.EmitAsNoTail);
				this.EmitBranchOp(branchValue, label);
			}
			finally
			{
				this.EmitExpressionEnd(flags);
			}
		}

		// Token: 0x0600178D RID: 6029 RVA: 0x00053234 File Offset: 0x00051434
		private void EmitBranchOp(bool branch, Label label)
		{
			this._ilg.Emit(branch ? OpCodes.Brtrue : OpCodes.Brfalse, label);
		}

		// Token: 0x0600178E RID: 6030 RVA: 0x00053251 File Offset: 0x00051451
		private void EmitBranchNot(bool branch, UnaryExpression node, Label label)
		{
			if (node.Method != null)
			{
				this.EmitExpression(node, LambdaCompiler.CompilationFlags.EmitNoExpressionStart | LambdaCompiler.CompilationFlags.EmitAsNoTail);
				this.EmitBranchOp(branch, label);
				return;
			}
			this.EmitExpressionAndBranch(!branch, node.Operand, label);
		}

		// Token: 0x0600178F RID: 6031 RVA: 0x00053288 File Offset: 0x00051488
		private void EmitBranchComparison(bool branch, BinaryExpression node, Label label)
		{
			bool flag = branch == (node.NodeType == ExpressionType.Equal);
			if (node.Method != null)
			{
				this.EmitBinaryMethod(node, LambdaCompiler.CompilationFlags.EmitAsNoTail);
				this.EmitBranchOp(branch, label);
				return;
			}
			if (ConstantCheck.IsNull(node.Left))
			{
				if (node.Right.Type.IsNullableType())
				{
					this.EmitAddress(node.Right, node.Right.Type);
					this._ilg.EmitHasValue(node.Right.Type);
				}
				else
				{
					this.EmitExpression(LambdaCompiler.GetEqualityOperand(node.Right));
				}
				this.EmitBranchOp(!flag, label);
				return;
			}
			if (ConstantCheck.IsNull(node.Right))
			{
				if (node.Left.Type.IsNullableType())
				{
					this.EmitAddress(node.Left, node.Left.Type);
					this._ilg.EmitHasValue(node.Left.Type);
				}
				else
				{
					this.EmitExpression(LambdaCompiler.GetEqualityOperand(node.Left));
				}
				this.EmitBranchOp(!flag, label);
				return;
			}
			if (node.Left.Type.IsNullableType() || node.Right.Type.IsNullableType())
			{
				this.EmitBinaryExpression(node);
				this.EmitBranchOp(branch, label);
				return;
			}
			this.EmitExpression(LambdaCompiler.GetEqualityOperand(node.Left));
			this.EmitExpression(LambdaCompiler.GetEqualityOperand(node.Right));
			if (flag)
			{
				this._ilg.Emit(OpCodes.Beq, label);
				return;
			}
			this._ilg.Emit(OpCodes.Ceq);
			this._ilg.Emit(OpCodes.Brfalse, label);
		}

		// Token: 0x06001790 RID: 6032 RVA: 0x00053428 File Offset: 0x00051628
		private static Expression GetEqualityOperand(Expression expression)
		{
			if (expression.NodeType == ExpressionType.Convert)
			{
				UnaryExpression unaryExpression = (UnaryExpression)expression;
				if (TypeUtils.AreReferenceAssignable(unaryExpression.Type, unaryExpression.Operand.Type))
				{
					return unaryExpression.Operand;
				}
			}
			return expression;
		}

		// Token: 0x06001791 RID: 6033 RVA: 0x00053468 File Offset: 0x00051668
		private void EmitBranchLogical(bool branch, BinaryExpression node, Label label)
		{
			if (node.Method != null || node.IsLifted)
			{
				this.EmitExpression(node);
				this.EmitBranchOp(branch, label);
				return;
			}
			bool flag = node.NodeType == ExpressionType.AndAlso;
			if (branch == flag)
			{
				this.EmitBranchAnd(branch, node, label);
				return;
			}
			this.EmitBranchOr(branch, node, label);
		}

		// Token: 0x06001792 RID: 6034 RVA: 0x000534BC File Offset: 0x000516BC
		private void EmitBranchAnd(bool branch, BinaryExpression node, Label label)
		{
			Label label2 = this._ilg.DefineLabel();
			this.EmitExpressionAndBranch(!branch, node.Left, label2);
			this.EmitExpressionAndBranch(branch, node.Right, label);
			this._ilg.MarkLabel(label2);
		}

		// Token: 0x06001793 RID: 6035 RVA: 0x00053500 File Offset: 0x00051700
		private void EmitBranchOr(bool branch, BinaryExpression node, Label label)
		{
			this.EmitExpressionAndBranch(branch, node.Left, label);
			this.EmitExpressionAndBranch(branch, node.Right, label);
		}

		// Token: 0x06001794 RID: 6036 RVA: 0x00053520 File Offset: 0x00051720
		private void EmitBranchBlock(bool branch, BlockExpression node, Label label)
		{
			this.EnterScope(node);
			int expressionCount = node.ExpressionCount;
			for (int i = 0; i < expressionCount - 1; i++)
			{
				this.EmitExpressionAsVoid(node.GetExpression(i));
			}
			this.EmitExpressionAndBranch(branch, node.GetExpression(expressionCount - 1), label);
			this.ExitScope(node);
		}

		// Token: 0x06001795 RID: 6037 RVA: 0x0005356E File Offset: 0x0005176E
		private void EmitBlockExpression(Expression expr, LambdaCompiler.CompilationFlags flags)
		{
			this.Emit((BlockExpression)expr, LambdaCompiler.UpdateEmitAsTypeFlag(flags, LambdaCompiler.CompilationFlags.EmitAsDefaultType));
		}

		// Token: 0x06001796 RID: 6038 RVA: 0x00053584 File Offset: 0x00051784
		private void Emit(BlockExpression node, LambdaCompiler.CompilationFlags flags)
		{
			this.EnterScope(node);
			LambdaCompiler.CompilationFlags compilationFlags = flags & LambdaCompiler.CompilationFlags.EmitAsTypeMask;
			int expressionCount = node.ExpressionCount;
			LambdaCompiler.CompilationFlags compilationFlags2 = flags & LambdaCompiler.CompilationFlags.EmitAsTailCallMask;
			int i = 0;
			while (i < expressionCount - 1)
			{
				Expression expression = node.GetExpression(i);
				Expression expression2 = node.GetExpression(i + 1);
				if (!this.EmitDebugSymbols)
				{
					goto IL_60;
				}
				DebugInfoExpression debugInfoExpression = expression as DebugInfoExpression;
				if (debugInfoExpression == null || !debugInfoExpression.IsClear || !(expression2 is DebugInfoExpression))
				{
					goto IL_60;
				}
				IL_CC:
				i++;
				continue;
				IL_60:
				LambdaCompiler.CompilationFlags newValue;
				if (compilationFlags2 != LambdaCompiler.CompilationFlags.EmitAsNoTail)
				{
					GotoExpression gotoExpression = expression2 as GotoExpression;
					if (gotoExpression != null && (gotoExpression.Value == null || !LambdaCompiler.Significant(gotoExpression.Value)) && this.ReferenceLabel(gotoExpression.Target).CanReturn)
					{
						newValue = LambdaCompiler.CompilationFlags.EmitAsTail;
					}
					else
					{
						newValue = LambdaCompiler.CompilationFlags.EmitAsMiddle;
					}
				}
				else
				{
					newValue = LambdaCompiler.CompilationFlags.EmitAsNoTail;
				}
				flags = LambdaCompiler.UpdateEmitAsTailCallFlag(flags, newValue);
				this.EmitExpressionAsVoid(expression, flags);
				goto IL_CC;
			}
			if (compilationFlags == LambdaCompiler.CompilationFlags.EmitAsVoidType || node.Type == typeof(void))
			{
				this.EmitExpressionAsVoid(node.GetExpression(expressionCount - 1), compilationFlags2);
			}
			else
			{
				this.EmitExpressionAsType(node.GetExpression(expressionCount - 1), node.Type, compilationFlags2);
			}
			this.ExitScope(node);
		}

		// Token: 0x06001797 RID: 6039 RVA: 0x000536B8 File Offset: 0x000518B8
		private void EnterScope(object node)
		{
			if (LambdaCompiler.HasVariables(node) && (this._scope.MergedScopes == null || !this._scope.MergedScopes.Contains(node)))
			{
				CompilerScope compilerScope;
				if (!this._tree.Scopes.TryGetValue(node, out compilerScope))
				{
					compilerScope = new CompilerScope(node, false)
					{
						NeedsClosure = this._scope.NeedsClosure
					};
				}
				this._scope = compilerScope.Enter(this, this._scope);
			}
		}

		// Token: 0x06001798 RID: 6040 RVA: 0x00053730 File Offset: 0x00051930
		private static bool HasVariables(object node)
		{
			BlockExpression blockExpression = node as BlockExpression;
			if (blockExpression != null)
			{
				return blockExpression.Variables.Count > 0;
			}
			return ((CatchBlock)node).Variable != null;
		}

		// Token: 0x06001799 RID: 6041 RVA: 0x00053764 File Offset: 0x00051964
		private void ExitScope(object node)
		{
			if (this._scope.Node == node)
			{
				this._scope = this._scope.Exit();
			}
		}

		// Token: 0x0600179A RID: 6042 RVA: 0x00053788 File Offset: 0x00051988
		private void EmitDefaultExpression(Expression expr)
		{
			DefaultExpression defaultExpression = (DefaultExpression)expr;
			if (defaultExpression.Type != typeof(void))
			{
				this._ilg.EmitDefault(defaultExpression.Type);
			}
		}

		// Token: 0x0600179B RID: 6043 RVA: 0x000537C4 File Offset: 0x000519C4
		private void EmitLoopExpression(Expression expr)
		{
			LoopExpression loopExpression = (LoopExpression)expr;
			this.PushLabelBlock(LabelScopeKind.Statement);
			LabelInfo labelInfo = this.DefineLabel(loopExpression.BreakLabel);
			LabelInfo labelInfo2 = this.DefineLabel(loopExpression.ContinueLabel);
			labelInfo2.MarkWithEmptyStack();
			this.EmitExpressionAsVoid(loopExpression.Body);
			this._ilg.Emit(OpCodes.Br, labelInfo2.Label);
			this.PopLabelBlock(LabelScopeKind.Statement);
			labelInfo.MarkWithEmptyStack();
		}

		// Token: 0x0600179C RID: 6044 RVA: 0x00053830 File Offset: 0x00051A30
		private void EmitSwitchExpression(Expression expr, LambdaCompiler.CompilationFlags flags)
		{
			SwitchExpression switchExpression = (SwitchExpression)expr;
			if (this.TryEmitSwitchInstruction(switchExpression, flags))
			{
				return;
			}
			if (this.TryEmitHashtableSwitch(switchExpression, flags))
			{
				return;
			}
			ParameterExpression parameterExpression = Expression.Parameter(switchExpression.SwitchValue.Type, "switchValue");
			ParameterExpression parameterExpression2 = Expression.Parameter(LambdaCompiler.GetTestValueType(switchExpression), "testValue");
			this._scope.AddLocal(this, parameterExpression);
			this._scope.AddLocal(this, parameterExpression2);
			this.EmitExpression(switchExpression.SwitchValue);
			this._scope.EmitSet(parameterExpression);
			Label[] array = new Label[switchExpression.Cases.Count];
			bool[] array2 = new bool[switchExpression.Cases.Count];
			int i = 0;
			int count = switchExpression.Cases.Count;
			while (i < count)
			{
				this.DefineSwitchCaseLabel(switchExpression.Cases[i], out array[i], out array2[i]);
				foreach (Expression node in switchExpression.Cases[i].TestValues)
				{
					this.EmitExpression(node);
					this._scope.EmitSet(parameterExpression2);
					this.EmitExpressionAndBranch(true, Expression.Equal(parameterExpression, parameterExpression2, false, switchExpression.Comparison), array[i]);
				}
				i++;
			}
			Label label = this._ilg.DefineLabel();
			Label @default = (switchExpression.DefaultBody == null) ? label : this._ilg.DefineLabel();
			this.EmitSwitchCases(switchExpression, array, array2, @default, label, flags);
		}

		// Token: 0x0600179D RID: 6045 RVA: 0x000539D0 File Offset: 0x00051BD0
		private static Type GetTestValueType(SwitchExpression node)
		{
			if (node.Comparison == null)
			{
				return node.Cases[0].TestValues[0].Type;
			}
			Type type = node.Comparison.GetParametersCached()[1].ParameterType.GetNonRefType();
			if (node.IsLifted)
			{
				type = TypeUtils.GetNullableType(type);
			}
			return type;
		}

		// Token: 0x0600179E RID: 6046 RVA: 0x00053A30 File Offset: 0x00051C30
		private static bool FitsInBucket(List<LambdaCompiler.SwitchLabel> buckets, decimal key, int count)
		{
			decimal num = key - buckets[0].Key + 1m;
			return !(num > 2147483647m) && (buckets.Count + count) * 2 > num;
		}

		// Token: 0x0600179F RID: 6047 RVA: 0x00053A84 File Offset: 0x00051C84
		private static void MergeBuckets(List<List<LambdaCompiler.SwitchLabel>> buckets)
		{
			while (buckets.Count > 1)
			{
				List<LambdaCompiler.SwitchLabel> list = buckets[buckets.Count - 2];
				List<LambdaCompiler.SwitchLabel> list2 = buckets[buckets.Count - 1];
				if (!LambdaCompiler.FitsInBucket(list, list2[list2.Count - 1].Key, list2.Count))
				{
					return;
				}
				list.AddRange(list2);
				buckets.RemoveAt(buckets.Count - 1);
			}
		}

		// Token: 0x060017A0 RID: 6048 RVA: 0x00053AF4 File Offset: 0x00051CF4
		private static void AddToBuckets(List<List<LambdaCompiler.SwitchLabel>> buckets, LambdaCompiler.SwitchLabel key)
		{
			if (buckets.Count > 0)
			{
				List<LambdaCompiler.SwitchLabel> list = buckets[buckets.Count - 1];
				if (LambdaCompiler.FitsInBucket(list, key.Key, 1))
				{
					list.Add(key);
					LambdaCompiler.MergeBuckets(buckets);
					return;
				}
			}
			buckets.Add(new List<LambdaCompiler.SwitchLabel>
			{
				key
			});
		}

		// Token: 0x060017A1 RID: 6049 RVA: 0x00053B48 File Offset: 0x00051D48
		private static bool CanOptimizeSwitchType(Type valueType)
		{
			TypeCode typeCode = Type.GetTypeCode(valueType);
			return typeCode - TypeCode.Char <= 8;
		}

		// Token: 0x060017A2 RID: 6050 RVA: 0x00053B68 File Offset: 0x00051D68
		private bool TryEmitSwitchInstruction(SwitchExpression node, LambdaCompiler.CompilationFlags flags)
		{
			if (node.Comparison != null)
			{
				return false;
			}
			Type type = node.SwitchValue.Type;
			if (!LambdaCompiler.CanOptimizeSwitchType(type) || !TypeUtils.AreEquivalent(type, node.Cases[0].TestValues[0].Type))
			{
				return false;
			}
			if (!node.Cases.All((SwitchCase c) => c.TestValues.All((Expression t) => t is ConstantExpression)))
			{
				return false;
			}
			Label[] array = new Label[node.Cases.Count];
			bool[] array2 = new bool[node.Cases.Count];
			Set<decimal> set = new Set<decimal>();
			List<LambdaCompiler.SwitchLabel> list = new List<LambdaCompiler.SwitchLabel>();
			for (int i = 0; i < node.Cases.Count; i++)
			{
				this.DefineSwitchCaseLabel(node.Cases[i], out array[i], out array2[i]);
				foreach (Expression expression in node.Cases[i].TestValues)
				{
					ConstantExpression constantExpression = (ConstantExpression)expression;
					decimal num = LambdaCompiler.ConvertSwitchValue(constantExpression.Value);
					if (!set.Contains(num))
					{
						list.Add(new LambdaCompiler.SwitchLabel(num, constantExpression.Value, array[i]));
						set.Add(num);
					}
				}
			}
			list.Sort((LambdaCompiler.SwitchLabel x, LambdaCompiler.SwitchLabel y) => Math.Sign(x.Key - y.Key));
			List<List<LambdaCompiler.SwitchLabel>> list2 = new List<List<LambdaCompiler.SwitchLabel>>();
			foreach (LambdaCompiler.SwitchLabel key in list)
			{
				LambdaCompiler.AddToBuckets(list2, key);
			}
			LocalBuilder local = this.GetLocal(node.SwitchValue.Type);
			this.EmitExpression(node.SwitchValue);
			this._ilg.Emit(OpCodes.Stloc, local);
			Label label = this._ilg.DefineLabel();
			Label @default = (node.DefaultBody == null) ? label : this._ilg.DefineLabel();
			LambdaCompiler.SwitchInfo info = new LambdaCompiler.SwitchInfo(node, local, @default);
			this.EmitSwitchBuckets(info, list2, 0, list2.Count - 1);
			this.EmitSwitchCases(node, array, array2, @default, label, flags);
			this.FreeLocal(local);
			return true;
		}

		// Token: 0x060017A3 RID: 6051 RVA: 0x00053DEC File Offset: 0x00051FEC
		private static decimal ConvertSwitchValue(object value)
		{
			if (value is char)
			{
				return (int)((char)value);
			}
			return Convert.ToDecimal(value, CultureInfo.InvariantCulture);
		}

		// Token: 0x060017A4 RID: 6052 RVA: 0x00053E10 File Offset: 0x00052010
		private void DefineSwitchCaseLabel(SwitchCase @case, out Label label, out bool isGoto)
		{
			GotoExpression gotoExpression = @case.Body as GotoExpression;
			if (gotoExpression != null && gotoExpression.Value == null)
			{
				LabelInfo labelInfo = this.ReferenceLabel(gotoExpression.Target);
				if (labelInfo.CanBranch)
				{
					label = labelInfo.Label;
					isGoto = true;
					return;
				}
			}
			label = this._ilg.DefineLabel();
			isGoto = false;
		}

		// Token: 0x060017A5 RID: 6053 RVA: 0x00053E70 File Offset: 0x00052070
		private void EmitSwitchCases(SwitchExpression node, Label[] labels, bool[] isGoto, Label @default, Label end, LambdaCompiler.CompilationFlags flags)
		{
			this._ilg.Emit(OpCodes.Br, @default);
			int i = 0;
			int count = node.Cases.Count;
			while (i < count)
			{
				if (!isGoto[i])
				{
					this._ilg.MarkLabel(labels[i]);
					this.EmitExpressionAsType(node.Cases[i].Body, node.Type, flags);
					if (node.DefaultBody != null || i < count - 1)
					{
						if ((flags & LambdaCompiler.CompilationFlags.EmitAsTailCallMask) == LambdaCompiler.CompilationFlags.EmitAsTail)
						{
							this._ilg.Emit(OpCodes.Ret);
						}
						else
						{
							this._ilg.Emit(OpCodes.Br, end);
						}
					}
				}
				i++;
			}
			if (node.DefaultBody != null)
			{
				this._ilg.MarkLabel(@default);
				this.EmitExpressionAsType(node.DefaultBody, node.Type, flags);
			}
			this._ilg.MarkLabel(end);
		}

		// Token: 0x060017A6 RID: 6054 RVA: 0x00053F54 File Offset: 0x00052154
		private void EmitSwitchBuckets(LambdaCompiler.SwitchInfo info, List<List<LambdaCompiler.SwitchLabel>> buckets, int first, int last)
		{
			if (first == last)
			{
				this.EmitSwitchBucket(info, buckets[first]);
				return;
			}
			int num = (int)(((long)first + (long)last + 1L) / 2L);
			if (first == num - 1)
			{
				this.EmitSwitchBucket(info, buckets[first]);
			}
			else
			{
				Label label = this._ilg.DefineLabel();
				this._ilg.Emit(OpCodes.Ldloc, info.Value);
				this._ilg.EmitConstant(buckets[num - 1].Last<LambdaCompiler.SwitchLabel>().Constant);
				this._ilg.Emit(info.IsUnsigned ? OpCodes.Bgt_Un : OpCodes.Bgt, label);
				this.EmitSwitchBuckets(info, buckets, first, num - 1);
				this._ilg.MarkLabel(label);
			}
			this.EmitSwitchBuckets(info, buckets, num, last);
		}

		// Token: 0x060017A7 RID: 6055 RVA: 0x0005401C File Offset: 0x0005221C
		private void EmitSwitchBucket(LambdaCompiler.SwitchInfo info, List<LambdaCompiler.SwitchLabel> bucket)
		{
			if (bucket.Count == 1)
			{
				this._ilg.Emit(OpCodes.Ldloc, info.Value);
				this._ilg.EmitConstant(bucket[0].Constant);
				this._ilg.Emit(OpCodes.Beq, bucket[0].Label);
				return;
			}
			Label? label = null;
			if (info.Is64BitSwitch)
			{
				label = new Label?(this._ilg.DefineLabel());
				this._ilg.Emit(OpCodes.Ldloc, info.Value);
				this._ilg.EmitConstant(bucket.Last<LambdaCompiler.SwitchLabel>().Constant);
				this._ilg.Emit(info.IsUnsigned ? OpCodes.Bgt_Un : OpCodes.Bgt, label.Value);
				this._ilg.Emit(OpCodes.Ldloc, info.Value);
				this._ilg.EmitConstant(bucket[0].Constant);
				this._ilg.Emit(info.IsUnsigned ? OpCodes.Blt_Un : OpCodes.Blt, label.Value);
			}
			this._ilg.Emit(OpCodes.Ldloc, info.Value);
			decimal num = bucket[0].Key;
			if (num != 0m)
			{
				this._ilg.EmitConstant(bucket[0].Constant);
				this._ilg.Emit(OpCodes.Sub);
			}
			if (info.Is64BitSwitch)
			{
				this._ilg.Emit(OpCodes.Conv_I4);
			}
			int num2 = (int)(bucket[bucket.Count - 1].Key - bucket[0].Key + 1m);
			Label[] array = new Label[num2];
			int num3 = 0;
			foreach (LambdaCompiler.SwitchLabel switchLabel in bucket)
			{
				for (;;)
				{
					decimal num4 = num;
					num = ++num4;
					if (!(num4 != switchLabel.Key))
					{
						break;
					}
					array[num3++] = info.Default;
				}
				array[num3++] = switchLabel.Label;
			}
			this._ilg.Emit(OpCodes.Switch, array);
			if (info.Is64BitSwitch)
			{
				this._ilg.MarkLabel(label.Value);
			}
		}

		// Token: 0x060017A8 RID: 6056 RVA: 0x0005429C File Offset: 0x0005249C
		private bool TryEmitHashtableSwitch(SwitchExpression node, LambdaCompiler.CompilationFlags flags)
		{
			if (node.Comparison != typeof(string).GetMethod("op_Equality", BindingFlags.Static | BindingFlags.Public | BindingFlags.ExactBinding, null, new Type[]
			{
				typeof(string),
				typeof(string)
			}, null))
			{
				return false;
			}
			int num = 0;
			foreach (SwitchCase switchCase in node.Cases)
			{
				foreach (Expression expression in switchCase.TestValues)
				{
					if (!(expression is ConstantExpression))
					{
						return false;
					}
					num++;
				}
			}
			if (num < 7)
			{
				return false;
			}
			List<ElementInit> list = new List<ElementInit>(num);
			List<SwitchCase> list2 = new List<SwitchCase>(node.Cases.Count);
			int num2 = -1;
			MethodInfo method = typeof(Dictionary<string, int>).GetMethod("Add", new Type[]
			{
				typeof(string),
				typeof(int)
			});
			int i = 0;
			int count = node.Cases.Count;
			while (i < count)
			{
				foreach (Expression expression2 in node.Cases[i].TestValues)
				{
					ConstantExpression constantExpression = (ConstantExpression)expression2;
					if (constantExpression.Value != null)
					{
						list.Add(Expression.ElementInit(method, new Expression[]
						{
							constantExpression,
							Expression.Constant(i)
						}));
					}
					else
					{
						num2 = i;
					}
				}
				list2.Add(Expression.SwitchCase(node.Cases[i].Body, new Expression[]
				{
					Expression.Constant(i)
				}));
				i++;
			}
			MemberExpression memberExpression = this.CreateLazyInitializedField<Dictionary<string, int>>("dictionarySwitch");
			Expression instance = Expression.Condition(Expression.Equal(memberExpression, Expression.Constant(null, memberExpression.Type)), Expression.Assign(memberExpression, Expression.ListInit(Expression.New(typeof(Dictionary<string, int>).GetConstructor(new Type[]
			{
				typeof(int)
			}), new Expression[]
			{
				Expression.Constant(list.Count)
			}), list)), memberExpression);
			ParameterExpression parameterExpression = Expression.Variable(typeof(string), "switchValue");
			ParameterExpression parameterExpression2 = Expression.Variable(typeof(int), "switchIndex");
			BlockExpression node2 = Expression.Block(new ParameterExpression[]
			{
				parameterExpression2,
				parameterExpression
			}, new Expression[]
			{
				Expression.Assign(parameterExpression, node.SwitchValue),
				Expression.IfThenElse(Expression.Equal(parameterExpression, Expression.Constant(null, typeof(string))), Expression.Assign(parameterExpression2, Expression.Constant(num2)), Expression.IfThenElse(Expression.Call(instance, "TryGetValue", null, new Expression[]
				{
					parameterExpression,
					parameterExpression2
				}), Expression.Empty(), Expression.Assign(parameterExpression2, Expression.Constant(-1)))),
				Expression.Switch(node.Type, parameterExpression2, node.DefaultBody, null, list2)
			});
			this.EmitExpression(node2, flags);
			return true;
		}

		// Token: 0x060017A9 RID: 6057 RVA: 0x00054614 File Offset: 0x00052814
		private void CheckRethrow()
		{
			for (LabelScopeInfo labelScopeInfo = this._labelBlock; labelScopeInfo != null; labelScopeInfo = labelScopeInfo.Parent)
			{
				if (labelScopeInfo.Kind == LabelScopeKind.Catch)
				{
					return;
				}
				if (labelScopeInfo.Kind == LabelScopeKind.Finally)
				{
					break;
				}
			}
			throw Error.RethrowRequiresCatch();
		}

		// Token: 0x060017AA RID: 6058 RVA: 0x0005464C File Offset: 0x0005284C
		private void CheckTry()
		{
			for (LabelScopeInfo labelScopeInfo = this._labelBlock; labelScopeInfo != null; labelScopeInfo = labelScopeInfo.Parent)
			{
				if (labelScopeInfo.Kind == LabelScopeKind.Filter)
				{
					throw Error.TryNotAllowedInFilter();
				}
			}
		}

		// Token: 0x060017AB RID: 6059 RVA: 0x0005467B File Offset: 0x0005287B
		private void EmitSaveExceptionOrPop(CatchBlock cb)
		{
			if (cb.Variable != null)
			{
				this._scope.EmitSet(cb.Variable);
				return;
			}
			this._ilg.Emit(OpCodes.Pop);
		}

		// Token: 0x060017AC RID: 6060 RVA: 0x000546A8 File Offset: 0x000528A8
		private void EmitTryExpression(Expression expr)
		{
			TryExpression tryExpression = (TryExpression)expr;
			this.CheckTry();
			this.PushLabelBlock(LabelScopeKind.Try);
			this._ilg.BeginExceptionBlock();
			this.EmitExpression(tryExpression.Body);
			Type type = expr.Type;
			LocalBuilder local = null;
			if (type != typeof(void))
			{
				local = this.GetLocal(type);
				this._ilg.Emit(OpCodes.Stloc, local);
			}
			foreach (CatchBlock catchBlock in tryExpression.Handlers)
			{
				this.PushLabelBlock(LabelScopeKind.Catch);
				if (catchBlock.Filter == null)
				{
					this._ilg.BeginCatchBlock(catchBlock.Test);
				}
				else
				{
					this._ilg.BeginExceptFilterBlock();
				}
				this.EnterScope(catchBlock);
				this.EmitCatchStart(catchBlock);
				this.EmitExpression(catchBlock.Body);
				if (type != typeof(void))
				{
					this._ilg.Emit(OpCodes.Stloc, local);
				}
				this.ExitScope(catchBlock);
				this.PopLabelBlock(LabelScopeKind.Catch);
			}
			if (tryExpression.Finally != null || tryExpression.Fault != null)
			{
				this.PushLabelBlock(LabelScopeKind.Finally);
				if (tryExpression.Finally != null)
				{
					this._ilg.BeginFinallyBlock();
				}
				else
				{
					this._ilg.BeginFaultBlock();
				}
				this.EmitExpressionAsVoid(tryExpression.Finally ?? tryExpression.Fault);
				this._ilg.EndExceptionBlock();
				this.PopLabelBlock(LabelScopeKind.Finally);
			}
			else
			{
				this._ilg.EndExceptionBlock();
			}
			if (type != typeof(void))
			{
				this._ilg.Emit(OpCodes.Ldloc, local);
				this.FreeLocal(local);
			}
			this.PopLabelBlock(LabelScopeKind.Try);
		}

		// Token: 0x060017AD RID: 6061 RVA: 0x00054870 File Offset: 0x00052A70
		private void EmitCatchStart(CatchBlock cb)
		{
			if (cb.Filter == null)
			{
				this.EmitSaveExceptionOrPop(cb);
				return;
			}
			Label label = this._ilg.DefineLabel();
			Label label2 = this._ilg.DefineLabel();
			this._ilg.Emit(OpCodes.Isinst, cb.Test);
			this._ilg.Emit(OpCodes.Dup);
			this._ilg.Emit(OpCodes.Brtrue, label2);
			this._ilg.Emit(OpCodes.Pop);
			this._ilg.Emit(OpCodes.Ldc_I4_0);
			this._ilg.Emit(OpCodes.Br, label);
			this._ilg.MarkLabel(label2);
			this.EmitSaveExceptionOrPop(cb);
			this.PushLabelBlock(LabelScopeKind.Filter);
			this.EmitExpression(cb.Filter);
			this.PopLabelBlock(LabelScopeKind.Filter);
			this._ilg.MarkLabel(label);
			this._ilg.BeginCatchBlock(null);
			this._ilg.Emit(OpCodes.Pop);
		}

		// Token: 0x060017AE RID: 6062 RVA: 0x00054962 File Offset: 0x00052B62
		private void EmitQuoteUnaryExpression(Expression expr)
		{
			this.EmitQuote((UnaryExpression)expr);
		}

		// Token: 0x060017AF RID: 6063 RVA: 0x00054970 File Offset: 0x00052B70
		private void EmitQuote(UnaryExpression quote)
		{
			this.EmitConstant(quote.Operand, quote.Type);
			if (this._scope.NearestHoistedLocals != null)
			{
				this.EmitConstant(this._scope.NearestHoistedLocals, typeof(object));
				this._scope.EmitGet(this._scope.NearestHoistedLocals.SelfVariable);
				this._ilg.Emit(OpCodes.Call, typeof(RuntimeOps).GetMethod("Quote"));
				if (quote.Type != typeof(Expression))
				{
					this._ilg.Emit(OpCodes.Castclass, quote.Type);
				}
			}
		}

		// Token: 0x060017B0 RID: 6064 RVA: 0x00054A26 File Offset: 0x00052C26
		private void EmitThrowUnaryExpression(Expression expr)
		{
			this.EmitThrow((UnaryExpression)expr, LambdaCompiler.CompilationFlags.EmitAsDefaultType);
		}

		// Token: 0x060017B1 RID: 6065 RVA: 0x00054A38 File Offset: 0x00052C38
		private void EmitThrow(UnaryExpression expr, LambdaCompiler.CompilationFlags flags)
		{
			if (expr.Operand == null)
			{
				this.CheckRethrow();
				this._ilg.Emit(OpCodes.Rethrow);
			}
			else
			{
				this.EmitExpression(expr.Operand);
				this._ilg.Emit(OpCodes.Throw);
			}
			this.EmitUnreachable(expr, flags);
		}

		// Token: 0x060017B2 RID: 6066 RVA: 0x00054A89 File Offset: 0x00052C89
		private void EmitUnaryExpression(Expression expr, LambdaCompiler.CompilationFlags flags)
		{
			this.EmitUnary((UnaryExpression)expr, flags);
		}

		// Token: 0x060017B3 RID: 6067 RVA: 0x00054A98 File Offset: 0x00052C98
		private void EmitUnary(UnaryExpression node, LambdaCompiler.CompilationFlags flags)
		{
			if (node.Method != null)
			{
				this.EmitUnaryMethod(node, flags);
				return;
			}
			if (node.NodeType == ExpressionType.NegateChecked && TypeUtils.IsInteger(node.Operand.Type))
			{
				this.EmitExpression(node.Operand);
				LocalBuilder local = this.GetLocal(node.Operand.Type);
				this._ilg.Emit(OpCodes.Stloc, local);
				this._ilg.EmitInt(0);
				this._ilg.EmitConvertToType(typeof(int), node.Operand.Type, false);
				this._ilg.Emit(OpCodes.Ldloc, local);
				this.FreeLocal(local);
				this.EmitBinaryOperator(ExpressionType.SubtractChecked, node.Operand.Type, node.Operand.Type, node.Type, false);
				return;
			}
			this.EmitExpression(node.Operand);
			this.EmitUnaryOperator(node.NodeType, node.Operand.Type, node.Type);
		}

		// Token: 0x060017B4 RID: 6068 RVA: 0x00054BA4 File Offset: 0x00052DA4
		private void EmitUnaryOperator(ExpressionType op, Type operandType, Type resultType)
		{
			bool flag = operandType.IsNullableType();
			if (op == ExpressionType.ArrayLength)
			{
				this._ilg.Emit(OpCodes.Ldlen);
				return;
			}
			if (flag)
			{
				if (op <= ExpressionType.TypeAs)
				{
					if (op - ExpressionType.Negate > 2)
					{
						if (op != ExpressionType.Not)
						{
							if (op != ExpressionType.TypeAs)
							{
								goto IL_2D6;
							}
							this._ilg.Emit(OpCodes.Box, operandType);
							this._ilg.Emit(OpCodes.Isinst, resultType);
							if (resultType.IsNullableType())
							{
								this._ilg.Emit(OpCodes.Unbox_Any, resultType);
							}
							return;
						}
						else if (!(operandType != typeof(bool?)))
						{
							Label label = this._ilg.DefineLabel();
							LocalBuilder local = this.GetLocal(operandType);
							this._ilg.Emit(OpCodes.Stloc, local);
							this._ilg.Emit(OpCodes.Ldloca, local);
							this._ilg.EmitHasValue(operandType);
							this._ilg.Emit(OpCodes.Brfalse_S, label);
							this._ilg.Emit(OpCodes.Ldloca, local);
							this._ilg.EmitGetValueOrDefault(operandType);
							Type nonNullableType = operandType.GetNonNullableType();
							this.EmitUnaryOperator(op, nonNullableType, typeof(bool));
							ConstructorInfo constructor = resultType.GetConstructor(new Type[]
							{
								typeof(bool)
							});
							this._ilg.Emit(OpCodes.Newobj, constructor);
							this._ilg.Emit(OpCodes.Stloc, local);
							this._ilg.MarkLabel(label);
							this._ilg.Emit(OpCodes.Ldloc, local);
							this.FreeLocal(local);
							return;
						}
					}
				}
				else if (op != ExpressionType.Decrement && op != ExpressionType.Increment && op - ExpressionType.OnesComplement > 2)
				{
					goto IL_2D6;
				}
				Label label2 = this._ilg.DefineLabel();
				Label label3 = this._ilg.DefineLabel();
				LocalBuilder local2 = this.GetLocal(operandType);
				this._ilg.Emit(OpCodes.Stloc, local2);
				this._ilg.Emit(OpCodes.Ldloca, local2);
				this._ilg.EmitHasValue(operandType);
				this._ilg.Emit(OpCodes.Brfalse_S, label2);
				this._ilg.Emit(OpCodes.Ldloca, local2);
				this._ilg.EmitGetValueOrDefault(operandType);
				Type nonNullableType2 = resultType.GetNonNullableType();
				this.EmitUnaryOperator(op, nonNullableType2, nonNullableType2);
				ConstructorInfo constructor2 = resultType.GetConstructor(new Type[]
				{
					nonNullableType2
				});
				this._ilg.Emit(OpCodes.Newobj, constructor2);
				this._ilg.Emit(OpCodes.Stloc, local2);
				this._ilg.Emit(OpCodes.Br_S, label3);
				this._ilg.MarkLabel(label2);
				this._ilg.Emit(OpCodes.Ldloca, local2);
				this._ilg.Emit(OpCodes.Initobj, resultType);
				this._ilg.MarkLabel(label3);
				this._ilg.Emit(OpCodes.Ldloc, local2);
				this.FreeLocal(local2);
				return;
				IL_2D6:
				throw Error.UnhandledUnary(op);
			}
			if (op <= ExpressionType.TypeAs)
			{
				switch (op)
				{
				case ExpressionType.Negate:
				case ExpressionType.NegateChecked:
					this._ilg.Emit(OpCodes.Neg);
					goto IL_492;
				case ExpressionType.UnaryPlus:
					this._ilg.Emit(OpCodes.Nop);
					goto IL_492;
				case ExpressionType.New:
				case ExpressionType.NewArrayInit:
				case ExpressionType.NewArrayBounds:
					break;
				case ExpressionType.Not:
					if (operandType == typeof(bool))
					{
						this._ilg.Emit(OpCodes.Ldc_I4_0);
						this._ilg.Emit(OpCodes.Ceq);
						goto IL_492;
					}
					this._ilg.Emit(OpCodes.Not);
					goto IL_492;
				default:
					if (op == ExpressionType.TypeAs)
					{
						if (operandType.IsValueType)
						{
							this._ilg.Emit(OpCodes.Box, operandType);
						}
						this._ilg.Emit(OpCodes.Isinst, resultType);
						if (resultType.IsNullableType())
						{
							this._ilg.Emit(OpCodes.Unbox_Any, resultType);
						}
						return;
					}
					break;
				}
			}
			else
			{
				if (op == ExpressionType.Decrement)
				{
					this.EmitConstantOne(resultType);
					this._ilg.Emit(OpCodes.Sub);
					goto IL_492;
				}
				if (op == ExpressionType.Increment)
				{
					this.EmitConstantOne(resultType);
					this._ilg.Emit(OpCodes.Add);
					goto IL_492;
				}
				switch (op)
				{
				case ExpressionType.OnesComplement:
					this._ilg.Emit(OpCodes.Not);
					goto IL_492;
				case ExpressionType.IsTrue:
					this._ilg.Emit(OpCodes.Ldc_I4_1);
					this._ilg.Emit(OpCodes.Ceq);
					return;
				case ExpressionType.IsFalse:
					this._ilg.Emit(OpCodes.Ldc_I4_0);
					this._ilg.Emit(OpCodes.Ceq);
					return;
				}
			}
			throw Error.UnhandledUnary(op);
			IL_492:
			this.EmitConvertArithmeticResult(op, resultType);
		}

		// Token: 0x060017B5 RID: 6069 RVA: 0x0005504C File Offset: 0x0005324C
		private void EmitConstantOne(Type type)
		{
			switch (Type.GetTypeCode(type))
			{
			case TypeCode.Int16:
			case TypeCode.UInt16:
			case TypeCode.Int32:
			case TypeCode.UInt32:
				this._ilg.Emit(OpCodes.Ldc_I4_1);
				return;
			case TypeCode.Int64:
			case TypeCode.UInt64:
				this._ilg.Emit(OpCodes.Ldc_I8, 1L);
				return;
			case TypeCode.Single:
				this._ilg.Emit(OpCodes.Ldc_R4, 1f);
				return;
			case TypeCode.Double:
				this._ilg.Emit(OpCodes.Ldc_R8, 1.0);
				return;
			default:
				throw ContractUtils.Unreachable;
			}
		}

		// Token: 0x060017B6 RID: 6070 RVA: 0x000550E4 File Offset: 0x000532E4
		private void EmitUnboxUnaryExpression(Expression expr)
		{
			UnaryExpression unaryExpression = (UnaryExpression)expr;
			this.EmitExpression(unaryExpression.Operand);
			this._ilg.Emit(OpCodes.Unbox_Any, unaryExpression.Type);
		}

		// Token: 0x060017B7 RID: 6071 RVA: 0x0005511A File Offset: 0x0005331A
		private void EmitConvertUnaryExpression(Expression expr, LambdaCompiler.CompilationFlags flags)
		{
			this.EmitConvert((UnaryExpression)expr, flags);
		}

		// Token: 0x060017B8 RID: 6072 RVA: 0x0005512C File Offset: 0x0005332C
		private void EmitConvert(UnaryExpression node, LambdaCompiler.CompilationFlags flags)
		{
			if (node.Method != null)
			{
				if (node.IsLifted && (!node.Type.IsValueType || !node.Operand.Type.IsValueType))
				{
					ParameterInfo[] parametersCached = node.Method.GetParametersCached();
					Type type = parametersCached[0].ParameterType;
					if (type.IsByRef)
					{
						type = type.GetElementType();
					}
					UnaryExpression node2 = Expression.Convert(Expression.Call(node.Method, Expression.Convert(node.Operand, parametersCached[0].ParameterType)), node.Type);
					this.EmitConvert(node2, flags);
					return;
				}
				this.EmitUnaryMethod(node, flags);
				return;
			}
			else
			{
				if (node.Type == typeof(void))
				{
					this.EmitExpressionAsVoid(node.Operand, flags);
					return;
				}
				if (TypeUtils.AreEquivalent(node.Operand.Type, node.Type))
				{
					this.EmitExpression(node.Operand, flags);
					return;
				}
				this.EmitExpression(node.Operand);
				this._ilg.EmitConvertToType(node.Operand.Type, node.Type, node.NodeType == ExpressionType.ConvertChecked);
				return;
			}
		}

		// Token: 0x060017B9 RID: 6073 RVA: 0x00055250 File Offset: 0x00053450
		private void EmitUnaryMethod(UnaryExpression node, LambdaCompiler.CompilationFlags flags)
		{
			if (node.IsLifted)
			{
				ParameterExpression parameterExpression = Expression.Variable(node.Operand.Type.GetNonNullableType(), null);
				MethodCallExpression methodCallExpression = Expression.Call(node.Method, parameterExpression);
				Type nullableType = TypeUtils.GetNullableType(methodCallExpression.Type);
				this.EmitLift(node.NodeType, nullableType, methodCallExpression, new ParameterExpression[]
				{
					parameterExpression
				}, new Expression[]
				{
					node.Operand
				});
				this._ilg.EmitConvertToType(nullableType, node.Type, false);
				return;
			}
			this.EmitMethodCallExpression(Expression.Call(node.Method, node.Operand), flags);
		}

		// Token: 0x04000B63 RID: 2915
		private readonly AnalyzedTree _tree;

		// Token: 0x04000B64 RID: 2916
		private readonly ILGenerator _ilg;

		// Token: 0x04000B65 RID: 2917
		private readonly TypeBuilder _typeBuilder;

		// Token: 0x04000B66 RID: 2918
		private readonly MethodInfo _method;

		// Token: 0x04000B67 RID: 2919
		private LabelScopeInfo _labelBlock = new LabelScopeInfo(null, LabelScopeKind.Lambda);

		// Token: 0x04000B68 RID: 2920
		private readonly Dictionary<LabelTarget, LabelInfo> _labelInfo = new Dictionary<LabelTarget, LabelInfo>();

		// Token: 0x04000B69 RID: 2921
		private CompilerScope _scope;

		// Token: 0x04000B6A RID: 2922
		private readonly LambdaExpression _lambda;

		// Token: 0x04000B6B RID: 2923
		private readonly bool _hasClosureArgument;

		// Token: 0x04000B6C RID: 2924
		private readonly BoundConstants _boundConstants;

		// Token: 0x04000B6D RID: 2925
		private readonly KeyedQueue<Type, LocalBuilder> _freeLocals = new KeyedQueue<Type, LocalBuilder>();

		// Token: 0x04000B6E RID: 2926
		private bool _sequencePointCleared;

		// Token: 0x04000B6F RID: 2927
		private readonly StackGuard _guard = new StackGuard();

		// Token: 0x04000B70 RID: 2928
		private static int _Counter;

		// Token: 0x02000452 RID: 1106
		// (Invoke) Token: 0x06001FD1 RID: 8145
		private delegate void WriteBack();

		// Token: 0x02000453 RID: 1107
		[Flags]
		internal enum CompilationFlags
		{
			// Token: 0x040012DC RID: 4828
			EmitExpressionStart = 1,
			// Token: 0x040012DD RID: 4829
			EmitNoExpressionStart = 2,
			// Token: 0x040012DE RID: 4830
			EmitAsDefaultType = 16,
			// Token: 0x040012DF RID: 4831
			EmitAsVoidType = 32,
			// Token: 0x040012E0 RID: 4832
			EmitAsTail = 256,
			// Token: 0x040012E1 RID: 4833
			EmitAsMiddle = 512,
			// Token: 0x040012E2 RID: 4834
			EmitAsNoTail = 1024,
			// Token: 0x040012E3 RID: 4835
			EmitExpressionStartMask = 15,
			// Token: 0x040012E4 RID: 4836
			EmitAsTypeMask = 240,
			// Token: 0x040012E5 RID: 4837
			EmitAsTailCallMask = 3840
		}

		// Token: 0x02000454 RID: 1108
		private sealed class SwitchLabel
		{
			// Token: 0x06001FD4 RID: 8148 RVA: 0x0006F3A1 File Offset: 0x0006D5A1
			internal SwitchLabel(decimal key, object constant, Label label)
			{
				this.Key = key;
				this.Constant = constant;
				this.Label = label;
			}

			// Token: 0x040012E6 RID: 4838
			internal readonly decimal Key;

			// Token: 0x040012E7 RID: 4839
			internal readonly Label Label;

			// Token: 0x040012E8 RID: 4840
			internal readonly object Constant;
		}

		// Token: 0x02000455 RID: 1109
		private sealed class SwitchInfo
		{
			// Token: 0x06001FD5 RID: 8149 RVA: 0x0006F3C0 File Offset: 0x0006D5C0
			internal SwitchInfo(SwitchExpression node, LocalBuilder value, Label @default)
			{
				this.Node = node;
				this.Value = value;
				this.Default = @default;
				this.Type = this.Node.SwitchValue.Type;
				this.IsUnsigned = TypeUtils.IsUnsigned(this.Type);
				TypeCode typeCode = Type.GetTypeCode(this.Type);
				this.Is64BitSwitch = (typeCode == TypeCode.UInt64 || typeCode == TypeCode.Int64);
			}

			// Token: 0x040012E9 RID: 4841
			internal readonly SwitchExpression Node;

			// Token: 0x040012EA RID: 4842
			internal readonly LocalBuilder Value;

			// Token: 0x040012EB RID: 4843
			internal readonly Label Default;

			// Token: 0x040012EC RID: 4844
			internal readonly Type Type;

			// Token: 0x040012ED RID: 4845
			internal readonly bool IsUnsigned;

			// Token: 0x040012EE RID: 4846
			internal readonly bool Is64BitSwitch;
		}
	}
}
