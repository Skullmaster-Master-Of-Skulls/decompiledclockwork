using System;
using System.Collections.Generic;
using System.Dynamic.Utils;
using System.Reflection;
using System.Reflection.Emit;

namespace System.Linq.Expressions.Compiler
{
	// Token: 0x0200027E RID: 638
	internal static class ILGen
	{
		// Token: 0x060016B1 RID: 5809 RVA: 0x0004B8B2 File Offset: 0x00049AB2
		internal static void Emit(this ILGenerator il, OpCode opcode, MethodBase methodBase)
		{
			if (methodBase.MemberType == MemberTypes.Constructor)
			{
				il.Emit(opcode, (ConstructorInfo)methodBase);
				return;
			}
			il.Emit(opcode, (MethodInfo)methodBase);
		}

		// Token: 0x060016B2 RID: 5810 RVA: 0x0004B8D8 File Offset: 0x00049AD8
		internal static void EmitLoadArg(this ILGenerator il, int index)
		{
			switch (index)
			{
			case 0:
				il.Emit(OpCodes.Ldarg_0);
				return;
			case 1:
				il.Emit(OpCodes.Ldarg_1);
				return;
			case 2:
				il.Emit(OpCodes.Ldarg_2);
				return;
			case 3:
				il.Emit(OpCodes.Ldarg_3);
				return;
			default:
				if (index <= 255)
				{
					il.Emit(OpCodes.Ldarg_S, (byte)index);
					return;
				}
				il.Emit(OpCodes.Ldarg, index);
				return;
			}
		}

		// Token: 0x060016B3 RID: 5811 RVA: 0x0004B94F File Offset: 0x00049B4F
		internal static void EmitLoadArgAddress(this ILGenerator il, int index)
		{
			if (index <= 255)
			{
				il.Emit(OpCodes.Ldarga_S, (byte)index);
				return;
			}
			il.Emit(OpCodes.Ldarga, index);
		}

		// Token: 0x060016B4 RID: 5812 RVA: 0x0004B973 File Offset: 0x00049B73
		internal static void EmitStoreArg(this ILGenerator il, int index)
		{
			if (index <= 255)
			{
				il.Emit(OpCodes.Starg_S, (byte)index);
				return;
			}
			il.Emit(OpCodes.Starg, index);
		}

		// Token: 0x060016B5 RID: 5813 RVA: 0x0004B998 File Offset: 0x00049B98
		internal static void EmitLoadValueIndirect(this ILGenerator il, Type type)
		{
			ContractUtils.RequiresNotNull(type, "type");
			if (!type.IsValueType)
			{
				il.Emit(OpCodes.Ldind_Ref);
				return;
			}
			if (type == typeof(int))
			{
				il.Emit(OpCodes.Ldind_I4);
				return;
			}
			if (type == typeof(uint))
			{
				il.Emit(OpCodes.Ldind_U4);
				return;
			}
			if (type == typeof(short))
			{
				il.Emit(OpCodes.Ldind_I2);
				return;
			}
			if (type == typeof(ushort))
			{
				il.Emit(OpCodes.Ldind_U2);
				return;
			}
			if (type == typeof(long) || type == typeof(ulong))
			{
				il.Emit(OpCodes.Ldind_I8);
				return;
			}
			if (type == typeof(char))
			{
				il.Emit(OpCodes.Ldind_I2);
				return;
			}
			if (type == typeof(bool))
			{
				il.Emit(OpCodes.Ldind_I1);
				return;
			}
			if (type == typeof(float))
			{
				il.Emit(OpCodes.Ldind_R4);
				return;
			}
			if (type == typeof(double))
			{
				il.Emit(OpCodes.Ldind_R8);
				return;
			}
			il.Emit(OpCodes.Ldobj, type);
		}

		// Token: 0x060016B6 RID: 5814 RVA: 0x0004BAF4 File Offset: 0x00049CF4
		internal static void EmitStoreValueIndirect(this ILGenerator il, Type type)
		{
			ContractUtils.RequiresNotNull(type, "type");
			if (!type.IsValueType)
			{
				il.Emit(OpCodes.Stind_Ref);
				return;
			}
			if (type == typeof(int))
			{
				il.Emit(OpCodes.Stind_I4);
				return;
			}
			if (type == typeof(short))
			{
				il.Emit(OpCodes.Stind_I2);
				return;
			}
			if (type == typeof(long) || type == typeof(ulong))
			{
				il.Emit(OpCodes.Stind_I8);
				return;
			}
			if (type == typeof(char))
			{
				il.Emit(OpCodes.Stind_I2);
				return;
			}
			if (type == typeof(bool))
			{
				il.Emit(OpCodes.Stind_I1);
				return;
			}
			if (type == typeof(float))
			{
				il.Emit(OpCodes.Stind_R4);
				return;
			}
			if (type == typeof(double))
			{
				il.Emit(OpCodes.Stind_R8);
				return;
			}
			il.Emit(OpCodes.Stobj, type);
		}

		// Token: 0x060016B7 RID: 5815 RVA: 0x0004BC14 File Offset: 0x00049E14
		internal static void EmitLoadElement(this ILGenerator il, Type type)
		{
			ContractUtils.RequiresNotNull(type, "type");
			if (!type.IsValueType)
			{
				il.Emit(OpCodes.Ldelem_Ref);
				return;
			}
			if (type.IsEnum)
			{
				il.Emit(OpCodes.Ldelem, type);
				return;
			}
			switch (Type.GetTypeCode(type))
			{
			case TypeCode.Boolean:
			case TypeCode.SByte:
				il.Emit(OpCodes.Ldelem_I1);
				return;
			case TypeCode.Char:
			case TypeCode.UInt16:
				il.Emit(OpCodes.Ldelem_U2);
				return;
			case TypeCode.Byte:
				il.Emit(OpCodes.Ldelem_U1);
				return;
			case TypeCode.Int16:
				il.Emit(OpCodes.Ldelem_I2);
				return;
			case TypeCode.Int32:
				il.Emit(OpCodes.Ldelem_I4);
				return;
			case TypeCode.UInt32:
				il.Emit(OpCodes.Ldelem_U4);
				return;
			case TypeCode.Int64:
			case TypeCode.UInt64:
				il.Emit(OpCodes.Ldelem_I8);
				return;
			case TypeCode.Single:
				il.Emit(OpCodes.Ldelem_R4);
				return;
			case TypeCode.Double:
				il.Emit(OpCodes.Ldelem_R8);
				return;
			default:
				il.Emit(OpCodes.Ldelem, type);
				return;
			}
		}

		// Token: 0x060016B8 RID: 5816 RVA: 0x0004BD10 File Offset: 0x00049F10
		internal static void EmitStoreElement(this ILGenerator il, Type type)
		{
			ContractUtils.RequiresNotNull(type, "type");
			if (type.IsEnum)
			{
				il.Emit(OpCodes.Stelem, type);
				return;
			}
			switch (Type.GetTypeCode(type))
			{
			case TypeCode.Boolean:
			case TypeCode.SByte:
			case TypeCode.Byte:
				il.Emit(OpCodes.Stelem_I1);
				return;
			case TypeCode.Char:
			case TypeCode.Int16:
			case TypeCode.UInt16:
				il.Emit(OpCodes.Stelem_I2);
				return;
			case TypeCode.Int32:
			case TypeCode.UInt32:
				il.Emit(OpCodes.Stelem_I4);
				return;
			case TypeCode.Int64:
			case TypeCode.UInt64:
				il.Emit(OpCodes.Stelem_I8);
				return;
			case TypeCode.Single:
				il.Emit(OpCodes.Stelem_R4);
				return;
			case TypeCode.Double:
				il.Emit(OpCodes.Stelem_R8);
				return;
			default:
				if (type.IsValueType)
				{
					il.Emit(OpCodes.Stelem, type);
					return;
				}
				il.Emit(OpCodes.Stelem_Ref);
				return;
			}
		}

		// Token: 0x060016B9 RID: 5817 RVA: 0x0004BDE6 File Offset: 0x00049FE6
		internal static void EmitType(this ILGenerator il, Type type)
		{
			ContractUtils.RequiresNotNull(type, "type");
			il.Emit(OpCodes.Ldtoken, type);
			il.Emit(OpCodes.Call, typeof(Type).GetMethod("GetTypeFromHandle"));
		}

		// Token: 0x060016BA RID: 5818 RVA: 0x0004BE1E File Offset: 0x0004A01E
		internal static void EmitFieldAddress(this ILGenerator il, FieldInfo fi)
		{
			ContractUtils.RequiresNotNull(fi, "fi");
			if (fi.IsStatic)
			{
				il.Emit(OpCodes.Ldsflda, fi);
				return;
			}
			il.Emit(OpCodes.Ldflda, fi);
		}

		// Token: 0x060016BB RID: 5819 RVA: 0x0004BE4C File Offset: 0x0004A04C
		internal static void EmitFieldGet(this ILGenerator il, FieldInfo fi)
		{
			ContractUtils.RequiresNotNull(fi, "fi");
			if (fi.IsStatic)
			{
				il.Emit(OpCodes.Ldsfld, fi);
				return;
			}
			il.Emit(OpCodes.Ldfld, fi);
		}

		// Token: 0x060016BC RID: 5820 RVA: 0x0004BE7A File Offset: 0x0004A07A
		internal static void EmitFieldSet(this ILGenerator il, FieldInfo fi)
		{
			ContractUtils.RequiresNotNull(fi, "fi");
			if (fi.IsStatic)
			{
				il.Emit(OpCodes.Stsfld, fi);
				return;
			}
			il.Emit(OpCodes.Stfld, fi);
		}

		// Token: 0x060016BD RID: 5821 RVA: 0x0004BEA8 File Offset: 0x0004A0A8
		internal static void EmitNew(this ILGenerator il, ConstructorInfo ci)
		{
			ContractUtils.RequiresNotNull(ci, "ci");
			if (ci.DeclaringType.ContainsGenericParameters)
			{
				throw Error.IllegalNewGenericParams(ci.DeclaringType);
			}
			il.Emit(OpCodes.Newobj, ci);
		}

		// Token: 0x060016BE RID: 5822 RVA: 0x0004BEDC File Offset: 0x0004A0DC
		internal static void EmitNew(this ILGenerator il, Type type, Type[] paramTypes)
		{
			ContractUtils.RequiresNotNull(type, "type");
			ContractUtils.RequiresNotNull(paramTypes, "paramTypes");
			ConstructorInfo constructor = type.GetConstructor(paramTypes);
			if (constructor == null)
			{
				throw Error.TypeDoesNotHaveConstructorForTheSignature();
			}
			il.EmitNew(constructor);
		}

		// Token: 0x060016BF RID: 5823 RVA: 0x0004BF1D File Offset: 0x0004A11D
		internal static void EmitNull(this ILGenerator il)
		{
			il.Emit(OpCodes.Ldnull);
		}

		// Token: 0x060016C0 RID: 5824 RVA: 0x0004BF2A File Offset: 0x0004A12A
		internal static void EmitString(this ILGenerator il, string value)
		{
			ContractUtils.RequiresNotNull(value, "value");
			il.Emit(OpCodes.Ldstr, value);
		}

		// Token: 0x060016C1 RID: 5825 RVA: 0x0004BF43 File Offset: 0x0004A143
		internal static void EmitBoolean(this ILGenerator il, bool value)
		{
			if (value)
			{
				il.Emit(OpCodes.Ldc_I4_1);
				return;
			}
			il.Emit(OpCodes.Ldc_I4_0);
		}

		// Token: 0x060016C2 RID: 5826 RVA: 0x0004BF5F File Offset: 0x0004A15F
		internal static void EmitChar(this ILGenerator il, char value)
		{
			il.EmitInt((int)value);
			il.Emit(OpCodes.Conv_U2);
		}

		// Token: 0x060016C3 RID: 5827 RVA: 0x0004BF73 File Offset: 0x0004A173
		internal static void EmitByte(this ILGenerator il, byte value)
		{
			il.EmitInt((int)value);
			il.Emit(OpCodes.Conv_U1);
		}

		// Token: 0x060016C4 RID: 5828 RVA: 0x0004BF87 File Offset: 0x0004A187
		internal static void EmitSByte(this ILGenerator il, sbyte value)
		{
			il.EmitInt((int)value);
			il.Emit(OpCodes.Conv_I1);
		}

		// Token: 0x060016C5 RID: 5829 RVA: 0x0004BF9B File Offset: 0x0004A19B
		internal static void EmitShort(this ILGenerator il, short value)
		{
			il.EmitInt((int)value);
			il.Emit(OpCodes.Conv_I2);
		}

		// Token: 0x060016C6 RID: 5830 RVA: 0x0004BFAF File Offset: 0x0004A1AF
		internal static void EmitUShort(this ILGenerator il, ushort value)
		{
			il.EmitInt((int)value);
			il.Emit(OpCodes.Conv_U2);
		}

		// Token: 0x060016C7 RID: 5831 RVA: 0x0004BFC4 File Offset: 0x0004A1C4
		internal static void EmitInt(this ILGenerator il, int value)
		{
			OpCode opcode;
			switch (value)
			{
			case -1:
				opcode = OpCodes.Ldc_I4_M1;
				break;
			case 0:
				opcode = OpCodes.Ldc_I4_0;
				break;
			case 1:
				opcode = OpCodes.Ldc_I4_1;
				break;
			case 2:
				opcode = OpCodes.Ldc_I4_2;
				break;
			case 3:
				opcode = OpCodes.Ldc_I4_3;
				break;
			case 4:
				opcode = OpCodes.Ldc_I4_4;
				break;
			case 5:
				opcode = OpCodes.Ldc_I4_5;
				break;
			case 6:
				opcode = OpCodes.Ldc_I4_6;
				break;
			case 7:
				opcode = OpCodes.Ldc_I4_7;
				break;
			case 8:
				opcode = OpCodes.Ldc_I4_8;
				break;
			default:
				if (value >= -128 && value <= 127)
				{
					il.Emit(OpCodes.Ldc_I4_S, (sbyte)value);
					return;
				}
				il.Emit(OpCodes.Ldc_I4, value);
				return;
			}
			il.Emit(opcode);
		}

		// Token: 0x060016C8 RID: 5832 RVA: 0x0004C07F File Offset: 0x0004A27F
		internal static void EmitUInt(this ILGenerator il, uint value)
		{
			il.EmitInt((int)value);
			il.Emit(OpCodes.Conv_U4);
		}

		// Token: 0x060016C9 RID: 5833 RVA: 0x0004C093 File Offset: 0x0004A293
		internal static void EmitLong(this ILGenerator il, long value)
		{
			il.Emit(OpCodes.Ldc_I8, value);
			il.Emit(OpCodes.Conv_I8);
		}

		// Token: 0x060016CA RID: 5834 RVA: 0x0004C0AC File Offset: 0x0004A2AC
		internal static void EmitULong(this ILGenerator il, ulong value)
		{
			il.Emit(OpCodes.Ldc_I8, (long)value);
			il.Emit(OpCodes.Conv_U8);
		}

		// Token: 0x060016CB RID: 5835 RVA: 0x0004C0C5 File Offset: 0x0004A2C5
		internal static void EmitDouble(this ILGenerator il, double value)
		{
			il.Emit(OpCodes.Ldc_R8, value);
		}

		// Token: 0x060016CC RID: 5836 RVA: 0x0004C0D3 File Offset: 0x0004A2D3
		internal static void EmitSingle(this ILGenerator il, float value)
		{
			il.Emit(OpCodes.Ldc_R4, value);
		}

		// Token: 0x060016CD RID: 5837 RVA: 0x0004C0E4 File Offset: 0x0004A2E4
		internal static bool CanEmitConstant(object value, Type type)
		{
			if (value == null || ILGen.CanEmitILConstant(type))
			{
				return true;
			}
			Type type2 = value as Type;
			if (type2 != null && ILGen.ShouldLdtoken(type2))
			{
				return true;
			}
			MethodBase methodBase = value as MethodBase;
			return methodBase != null && ILGen.ShouldLdtoken(methodBase);
		}

		// Token: 0x060016CE RID: 5838 RVA: 0x0004C134 File Offset: 0x0004A334
		private static bool CanEmitILConstant(Type type)
		{
			TypeCode typeCode = Type.GetTypeCode(type);
			return typeCode - TypeCode.Boolean <= 12 || typeCode == TypeCode.String;
		}

		// Token: 0x060016CF RID: 5839 RVA: 0x0004C157 File Offset: 0x0004A357
		internal static void EmitConstant(this ILGenerator il, object value)
		{
			il.EmitConstant(value, value.GetType());
		}

		// Token: 0x060016D0 RID: 5840 RVA: 0x0004C168 File Offset: 0x0004A368
		internal static void EmitConstant(this ILGenerator il, object value, Type type)
		{
			if (value == null)
			{
				il.EmitDefault(type);
				return;
			}
			if (il.TryEmitILConstant(value, type))
			{
				return;
			}
			Type type2 = value as Type;
			if (type2 != null && ILGen.ShouldLdtoken(type2))
			{
				il.EmitType(type2);
				if (type != typeof(Type))
				{
					il.Emit(OpCodes.Castclass, type);
				}
				return;
			}
			MethodBase methodBase = value as MethodBase;
			if (methodBase != null && ILGen.ShouldLdtoken(methodBase))
			{
				il.Emit(OpCodes.Ldtoken, methodBase);
				Type declaringType = methodBase.DeclaringType;
				if (declaringType != null && declaringType.IsGenericType)
				{
					il.Emit(OpCodes.Ldtoken, declaringType);
					il.Emit(OpCodes.Call, typeof(MethodBase).GetMethod("GetMethodFromHandle", new Type[]
					{
						typeof(RuntimeMethodHandle),
						typeof(RuntimeTypeHandle)
					}));
				}
				else
				{
					il.Emit(OpCodes.Call, typeof(MethodBase).GetMethod("GetMethodFromHandle", new Type[]
					{
						typeof(RuntimeMethodHandle)
					}));
				}
				if (type != typeof(MethodBase))
				{
					il.Emit(OpCodes.Castclass, type);
				}
				return;
			}
			throw ContractUtils.Unreachable;
		}

		// Token: 0x060016D1 RID: 5841 RVA: 0x0004C2AE File Offset: 0x0004A4AE
		internal static bool ShouldLdtoken(Type t)
		{
			return t is TypeBuilder || t.IsGenericParameter || t.IsVisible;
		}

		// Token: 0x060016D2 RID: 5842 RVA: 0x0004C2C8 File Offset: 0x0004A4C8
		internal static bool ShouldLdtoken(MethodBase mb)
		{
			if (mb is DynamicMethod)
			{
				return false;
			}
			Type declaringType = mb.DeclaringType;
			return declaringType == null || ILGen.ShouldLdtoken(declaringType);
		}

		// Token: 0x060016D3 RID: 5843 RVA: 0x0004C2F8 File Offset: 0x0004A4F8
		private static bool TryEmitILConstant(this ILGenerator il, object value, Type type)
		{
			switch (Type.GetTypeCode(type))
			{
			case TypeCode.Boolean:
				il.EmitBoolean((bool)value);
				return true;
			case TypeCode.Char:
				il.EmitChar((char)value);
				return true;
			case TypeCode.SByte:
				il.EmitSByte((sbyte)value);
				return true;
			case TypeCode.Byte:
				il.EmitByte((byte)value);
				return true;
			case TypeCode.Int16:
				il.EmitShort((short)value);
				return true;
			case TypeCode.UInt16:
				il.EmitUShort((ushort)value);
				return true;
			case TypeCode.Int32:
				il.EmitInt((int)value);
				return true;
			case TypeCode.UInt32:
				il.EmitUInt((uint)value);
				return true;
			case TypeCode.Int64:
				il.EmitLong((long)value);
				return true;
			case TypeCode.UInt64:
				il.EmitULong((ulong)value);
				return true;
			case TypeCode.Single:
				il.EmitSingle((float)value);
				return true;
			case TypeCode.Double:
				il.EmitDouble((double)value);
				return true;
			case TypeCode.Decimal:
				il.EmitDecimal((decimal)value);
				return true;
			case TypeCode.String:
				il.EmitString((string)value);
				return true;
			}
			return false;
		}

		// Token: 0x060016D4 RID: 5844 RVA: 0x0004C420 File Offset: 0x0004A620
		internal static void EmitConvertToType(this ILGenerator il, Type typeFrom, Type typeTo, bool isChecked)
		{
			if (TypeUtils.AreEquivalent(typeFrom, typeTo))
			{
				return;
			}
			if (typeFrom == typeof(void) || typeTo == typeof(void))
			{
				throw ContractUtils.Unreachable;
			}
			bool flag = typeFrom.IsNullableType();
			bool flag2 = typeTo.IsNullableType();
			Type nonNullableType = typeFrom.GetNonNullableType();
			Type nonNullableType2 = typeTo.GetNonNullableType();
			if (typeFrom.IsInterface || typeTo.IsInterface || typeFrom == typeof(object) || typeTo == typeof(object) || typeFrom == typeof(Enum) || typeFrom == typeof(ValueType) || TypeUtils.IsLegalExplicitVariantDelegateConversion(typeFrom, typeTo))
			{
				il.EmitCastToType(typeFrom, typeTo);
				return;
			}
			if (flag || flag2)
			{
				il.EmitNullableConversion(typeFrom, typeTo, isChecked);
				return;
			}
			if ((!TypeUtils.IsConvertible(typeFrom) || !TypeUtils.IsConvertible(typeTo)) && (nonNullableType.IsAssignableFrom(nonNullableType2) || nonNullableType2.IsAssignableFrom(nonNullableType)))
			{
				il.EmitCastToType(typeFrom, typeTo);
				return;
			}
			if (typeFrom.IsArray && typeTo.IsArray)
			{
				il.EmitCastToType(typeFrom, typeTo);
				return;
			}
			il.EmitNumericConversion(typeFrom, typeTo, isChecked);
		}

		// Token: 0x060016D5 RID: 5845 RVA: 0x0004C544 File Offset: 0x0004A744
		private static void EmitCastToType(this ILGenerator il, Type typeFrom, Type typeTo)
		{
			if (!typeFrom.IsValueType && typeTo.IsValueType)
			{
				il.Emit(OpCodes.Unbox_Any, typeTo);
				return;
			}
			if (typeFrom.IsValueType && !typeTo.IsValueType)
			{
				il.Emit(OpCodes.Box, typeFrom);
				if (typeTo != typeof(object))
				{
					il.Emit(OpCodes.Castclass, typeTo);
					return;
				}
				return;
			}
			else
			{
				if (!typeFrom.IsValueType && !typeTo.IsValueType)
				{
					il.Emit(OpCodes.Castclass, typeTo);
					return;
				}
				throw Error.InvalidCast(typeFrom, typeTo);
			}
		}

		// Token: 0x060016D6 RID: 5846 RVA: 0x0004C5D0 File Offset: 0x0004A7D0
		private static void EmitNumericConversion(this ILGenerator il, Type typeFrom, Type typeTo, bool isChecked)
		{
			bool flag = TypeUtils.IsUnsigned(typeFrom);
			bool flag2 = TypeUtils.IsFloatingPoint(typeFrom);
			if (typeTo == typeof(float))
			{
				if (flag)
				{
					il.Emit(OpCodes.Conv_R_Un);
				}
				il.Emit(OpCodes.Conv_R4);
				return;
			}
			if (typeTo == typeof(double))
			{
				if (flag)
				{
					il.Emit(OpCodes.Conv_R_Un);
				}
				il.Emit(OpCodes.Conv_R8);
				return;
			}
			TypeCode typeCode = Type.GetTypeCode(typeTo);
			if (isChecked)
			{
				if (flag)
				{
					switch (typeCode)
					{
					case TypeCode.Char:
					case TypeCode.UInt16:
						il.Emit(OpCodes.Conv_Ovf_U2_Un);
						return;
					case TypeCode.SByte:
						il.Emit(OpCodes.Conv_Ovf_I1_Un);
						return;
					case TypeCode.Byte:
						il.Emit(OpCodes.Conv_Ovf_U1_Un);
						return;
					case TypeCode.Int16:
						il.Emit(OpCodes.Conv_Ovf_I2_Un);
						return;
					case TypeCode.Int32:
						il.Emit(OpCodes.Conv_Ovf_I4_Un);
						return;
					case TypeCode.UInt32:
						il.Emit(OpCodes.Conv_Ovf_U4_Un);
						return;
					case TypeCode.Int64:
						il.Emit(OpCodes.Conv_Ovf_I8_Un);
						return;
					case TypeCode.UInt64:
						il.Emit(OpCodes.Conv_Ovf_U8_Un);
						return;
					default:
						throw Error.UnhandledConvert(typeTo);
					}
				}
				else
				{
					switch (typeCode)
					{
					case TypeCode.Char:
					case TypeCode.UInt16:
						il.Emit(OpCodes.Conv_Ovf_U2);
						return;
					case TypeCode.SByte:
						il.Emit(OpCodes.Conv_Ovf_I1);
						return;
					case TypeCode.Byte:
						il.Emit(OpCodes.Conv_Ovf_U1);
						return;
					case TypeCode.Int16:
						il.Emit(OpCodes.Conv_Ovf_I2);
						return;
					case TypeCode.Int32:
						il.Emit(OpCodes.Conv_Ovf_I4);
						return;
					case TypeCode.UInt32:
						il.Emit(OpCodes.Conv_Ovf_U4);
						return;
					case TypeCode.Int64:
						il.Emit(OpCodes.Conv_Ovf_I8);
						return;
					case TypeCode.UInt64:
						il.Emit(OpCodes.Conv_Ovf_U8);
						return;
					default:
						throw Error.UnhandledConvert(typeTo);
					}
				}
			}
			else
			{
				switch (typeCode)
				{
				case TypeCode.Char:
				case TypeCode.UInt16:
					il.Emit(OpCodes.Conv_U2);
					return;
				case TypeCode.SByte:
					il.Emit(OpCodes.Conv_I1);
					return;
				case TypeCode.Byte:
					il.Emit(OpCodes.Conv_U1);
					return;
				case TypeCode.Int16:
					il.Emit(OpCodes.Conv_I2);
					return;
				case TypeCode.Int32:
					il.Emit(OpCodes.Conv_I4);
					return;
				case TypeCode.UInt32:
					il.Emit(OpCodes.Conv_U4);
					return;
				case TypeCode.Int64:
					if (flag)
					{
						il.Emit(OpCodes.Conv_U8);
						return;
					}
					il.Emit(OpCodes.Conv_I8);
					return;
				case TypeCode.UInt64:
					if (flag || flag2)
					{
						il.Emit(OpCodes.Conv_U8);
						return;
					}
					il.Emit(OpCodes.Conv_I8);
					return;
				default:
					throw Error.UnhandledConvert(typeTo);
				}
			}
		}

		// Token: 0x060016D7 RID: 5847 RVA: 0x0004C838 File Offset: 0x0004AA38
		private static void EmitNullableToNullableConversion(this ILGenerator il, Type typeFrom, Type typeTo, bool isChecked)
		{
			Label label = default(Label);
			Label label2 = default(Label);
			LocalBuilder local = il.DeclareLocal(typeFrom);
			il.Emit(OpCodes.Stloc, local);
			LocalBuilder local2 = il.DeclareLocal(typeTo);
			il.Emit(OpCodes.Ldloca, local);
			il.EmitHasValue(typeFrom);
			label = il.DefineLabel();
			il.Emit(OpCodes.Brfalse_S, label);
			il.Emit(OpCodes.Ldloca, local);
			il.EmitGetValueOrDefault(typeFrom);
			Type nonNullableType = typeFrom.GetNonNullableType();
			Type nonNullableType2 = typeTo.GetNonNullableType();
			il.EmitConvertToType(nonNullableType, nonNullableType2, isChecked);
			ConstructorInfo constructor = typeTo.GetConstructor(new Type[]
			{
				nonNullableType2
			});
			il.Emit(OpCodes.Newobj, constructor);
			il.Emit(OpCodes.Stloc, local2);
			label2 = il.DefineLabel();
			il.Emit(OpCodes.Br_S, label2);
			il.MarkLabel(label);
			il.Emit(OpCodes.Ldloca, local2);
			il.Emit(OpCodes.Initobj, typeTo);
			il.MarkLabel(label2);
			il.Emit(OpCodes.Ldloc, local2);
		}

		// Token: 0x060016D8 RID: 5848 RVA: 0x0004C93C File Offset: 0x0004AB3C
		private static void EmitNonNullableToNullableConversion(this ILGenerator il, Type typeFrom, Type typeTo, bool isChecked)
		{
			LocalBuilder local = il.DeclareLocal(typeTo);
			Type nonNullableType = typeTo.GetNonNullableType();
			il.EmitConvertToType(typeFrom, nonNullableType, isChecked);
			ConstructorInfo constructor = typeTo.GetConstructor(new Type[]
			{
				nonNullableType
			});
			il.Emit(OpCodes.Newobj, constructor);
			il.Emit(OpCodes.Stloc, local);
			il.Emit(OpCodes.Ldloc, local);
		}

		// Token: 0x060016D9 RID: 5849 RVA: 0x0004C998 File Offset: 0x0004AB98
		private static void EmitNullableToNonNullableConversion(this ILGenerator il, Type typeFrom, Type typeTo, bool isChecked)
		{
			if (typeTo.IsValueType)
			{
				il.EmitNullableToNonNullableStructConversion(typeFrom, typeTo, isChecked);
				return;
			}
			il.EmitNullableToReferenceConversion(typeFrom);
		}

		// Token: 0x060016DA RID: 5850 RVA: 0x0004C9B4 File Offset: 0x0004ABB4
		private static void EmitNullableToNonNullableStructConversion(this ILGenerator il, Type typeFrom, Type typeTo, bool isChecked)
		{
			LocalBuilder local = il.DeclareLocal(typeFrom);
			il.Emit(OpCodes.Stloc, local);
			il.Emit(OpCodes.Ldloca, local);
			il.EmitGetValue(typeFrom);
			Type nonNullableType = typeFrom.GetNonNullableType();
			il.EmitConvertToType(nonNullableType, typeTo, isChecked);
		}

		// Token: 0x060016DB RID: 5851 RVA: 0x0004C9FA File Offset: 0x0004ABFA
		private static void EmitNullableToReferenceConversion(this ILGenerator il, Type typeFrom)
		{
			il.Emit(OpCodes.Box, typeFrom);
		}

		// Token: 0x060016DC RID: 5852 RVA: 0x0004CA08 File Offset: 0x0004AC08
		private static void EmitNullableConversion(this ILGenerator il, Type typeFrom, Type typeTo, bool isChecked)
		{
			bool flag = typeFrom.IsNullableType();
			bool flag2 = typeTo.IsNullableType();
			if (flag && flag2)
			{
				il.EmitNullableToNullableConversion(typeFrom, typeTo, isChecked);
				return;
			}
			if (flag)
			{
				il.EmitNullableToNonNullableConversion(typeFrom, typeTo, isChecked);
				return;
			}
			il.EmitNonNullableToNullableConversion(typeFrom, typeTo, isChecked);
		}

		// Token: 0x060016DD RID: 5853 RVA: 0x0004CA48 File Offset: 0x0004AC48
		internal static void EmitHasValue(this ILGenerator il, Type nullableType)
		{
			MethodInfo method = nullableType.GetMethod("get_HasValue", BindingFlags.Instance | BindingFlags.Public);
			il.Emit(OpCodes.Call, method);
		}

		// Token: 0x060016DE RID: 5854 RVA: 0x0004CA70 File Offset: 0x0004AC70
		internal static void EmitGetValue(this ILGenerator il, Type nullableType)
		{
			MethodInfo method = nullableType.GetMethod("get_Value", BindingFlags.Instance | BindingFlags.Public);
			il.Emit(OpCodes.Call, method);
		}

		// Token: 0x060016DF RID: 5855 RVA: 0x0004CA98 File Offset: 0x0004AC98
		internal static void EmitGetValueOrDefault(this ILGenerator il, Type nullableType)
		{
			MethodInfo method = nullableType.GetMethod("GetValueOrDefault", Type.EmptyTypes);
			il.Emit(OpCodes.Call, method);
		}

		// Token: 0x060016E0 RID: 5856 RVA: 0x0004CAC4 File Offset: 0x0004ACC4
		internal static void EmitArray<T>(this ILGenerator il, IList<T> items)
		{
			ContractUtils.RequiresNotNull(items, "items");
			il.EmitInt(items.Count);
			il.Emit(OpCodes.Newarr, typeof(T));
			for (int i = 0; i < items.Count; i++)
			{
				il.Emit(OpCodes.Dup);
				il.EmitInt(i);
				il.EmitConstant(items[i], typeof(T));
				il.EmitStoreElement(typeof(T));
			}
		}

		// Token: 0x060016E1 RID: 5857 RVA: 0x0004CB4C File Offset: 0x0004AD4C
		internal static void EmitArray(this ILGenerator il, Type elementType, int count, Action<int> emit)
		{
			ContractUtils.RequiresNotNull(elementType, "elementType");
			ContractUtils.RequiresNotNull(emit, "emit");
			if (count < 0)
			{
				throw Error.CountCannotBeNegative();
			}
			il.EmitInt(count);
			il.Emit(OpCodes.Newarr, elementType);
			for (int i = 0; i < count; i++)
			{
				il.Emit(OpCodes.Dup);
				il.EmitInt(i);
				emit(i);
				il.EmitStoreElement(elementType);
			}
		}

		// Token: 0x060016E2 RID: 5858 RVA: 0x0004CBB8 File Offset: 0x0004ADB8
		internal static void EmitArray(this ILGenerator il, Type arrayType)
		{
			ContractUtils.RequiresNotNull(arrayType, "arrayType");
			if (!arrayType.IsArray)
			{
				throw Error.ArrayTypeMustBeArray();
			}
			int arrayRank = arrayType.GetArrayRank();
			if (arrayRank == 1)
			{
				il.Emit(OpCodes.Newarr, arrayType.GetElementType());
				return;
			}
			Type[] array = new Type[arrayRank];
			for (int i = 0; i < arrayRank; i++)
			{
				array[i] = typeof(int);
			}
			il.EmitNew(arrayType, array);
		}

		// Token: 0x060016E3 RID: 5859 RVA: 0x0004CC24 File Offset: 0x0004AE24
		internal static void EmitDecimal(this ILGenerator il, decimal value)
		{
			if (!(decimal.Truncate(value) == value))
			{
				il.EmitDecimalBits(value);
				return;
			}
			if (-2147483648m <= value && value <= 2147483647m)
			{
				int value2 = decimal.ToInt32(value);
				il.EmitInt(value2);
				il.EmitNew(typeof(decimal).GetConstructor(new Type[]
				{
					typeof(int)
				}));
				return;
			}
			if (-9223372036854775808m <= value && value <= 9223372036854775807m)
			{
				long value3 = decimal.ToInt64(value);
				il.EmitLong(value3);
				il.EmitNew(typeof(decimal).GetConstructor(new Type[]
				{
					typeof(long)
				}));
				return;
			}
			il.EmitDecimalBits(value);
		}

		// Token: 0x060016E4 RID: 5860 RVA: 0x0004CD10 File Offset: 0x0004AF10
		private static void EmitDecimalBits(this ILGenerator il, decimal value)
		{
			int[] bits = decimal.GetBits(value);
			il.EmitInt(bits[0]);
			il.EmitInt(bits[1]);
			il.EmitInt(bits[2]);
			il.EmitBoolean(((long)bits[3] & (long)((ulong)int.MinValue)) != 0L);
			il.EmitByte((byte)(bits[3] >> 16));
			il.EmitNew(typeof(decimal).GetConstructor(new Type[]
			{
				typeof(int),
				typeof(int),
				typeof(int),
				typeof(bool),
				typeof(byte)
			}));
		}

		// Token: 0x060016E5 RID: 5861 RVA: 0x0004CDC0 File Offset: 0x0004AFC0
		internal static void EmitDefault(this ILGenerator il, Type type)
		{
			switch (Type.GetTypeCode(type))
			{
			case TypeCode.Empty:
			case TypeCode.DBNull:
			case TypeCode.String:
				il.Emit(OpCodes.Ldnull);
				return;
			case TypeCode.Object:
			case TypeCode.DateTime:
				if (type.IsValueType)
				{
					LocalBuilder local = il.DeclareLocal(type);
					il.Emit(OpCodes.Ldloca, local);
					il.Emit(OpCodes.Initobj, type);
					il.Emit(OpCodes.Ldloc, local);
					return;
				}
				il.Emit(OpCodes.Ldnull);
				return;
			case TypeCode.Boolean:
			case TypeCode.Char:
			case TypeCode.SByte:
			case TypeCode.Byte:
			case TypeCode.Int16:
			case TypeCode.UInt16:
			case TypeCode.Int32:
			case TypeCode.UInt32:
				il.Emit(OpCodes.Ldc_I4_0);
				return;
			case TypeCode.Int64:
			case TypeCode.UInt64:
				il.Emit(OpCodes.Ldc_I4_0);
				il.Emit(OpCodes.Conv_I8);
				return;
			case TypeCode.Single:
				il.Emit(OpCodes.Ldc_R4, 0f);
				return;
			case TypeCode.Double:
				il.Emit(OpCodes.Ldc_R8, 0.0);
				return;
			case TypeCode.Decimal:
				il.Emit(OpCodes.Ldc_I4_0);
				il.Emit(OpCodes.Newobj, typeof(decimal).GetConstructor(new Type[]
				{
					typeof(int)
				}));
				return;
			}
			throw ContractUtils.Unreachable;
		}
	}
}
