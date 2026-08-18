using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace System.Runtime
{
	// Token: 0x02000035 RID: 53
	internal static class TypeHelper
	{
		// Token: 0x0600019D RID: 413 RVA: 0x00006E62 File Offset: 0x00005062
		public static bool AreTypesCompatible(object source, Type destinationType)
		{
			if (source == null)
			{
				return !destinationType.IsValueType || TypeHelper.IsNullableType(destinationType);
			}
			return TypeHelper.AreTypesCompatible(source.GetType(), destinationType);
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00006E84 File Offset: 0x00005084
		public static bool AreTypesCompatible(Type sourceType, Type destinationType)
		{
			return sourceType == destinationType || TypeHelper.IsImplicitNumericConversion(sourceType, destinationType) || TypeHelper.IsImplicitReferenceConversion(sourceType, destinationType) || TypeHelper.IsImplicitBoxingConversion(sourceType, destinationType) || TypeHelper.IsImplicitNullableConversion(sourceType, destinationType);
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00006EB0 File Offset: 0x000050B0
		public static bool AreReferenceTypesCompatible(Type sourceType, Type destinationType)
		{
			return sourceType == destinationType || TypeHelper.IsImplicitReferenceConversion(sourceType, destinationType);
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00006EBF File Offset: 0x000050BF
		public static IEnumerable<Type> GetCompatibleTypes(IEnumerable<Type> enumerable, Type targetType)
		{
			foreach (Type type in enumerable)
			{
				if (TypeHelper.AreTypesCompatible(type, targetType))
				{
					yield return type;
				}
			}
			IEnumerator<Type> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00006ED8 File Offset: 0x000050D8
		public static bool ContainsCompatibleType(IEnumerable<Type> enumerable, Type targetType)
		{
			foreach (Type sourceType in enumerable)
			{
				if (TypeHelper.AreTypesCompatible(sourceType, targetType))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00006F2C File Offset: 0x0000512C
		public static T Convert<T>(object source)
		{
			if (source is T)
			{
				return (T)((object)source);
			}
			if (source == null)
			{
				if (typeof(T).IsValueType && !TypeHelper.IsNullableType(typeof(T)))
				{
					throw Fx.Exception.AsError(new InvalidCastException(InternalSR.CannotConvertObject(source, typeof(T))));
				}
				return default(T);
			}
			else
			{
				T result;
				if (TypeHelper.TryNumericConversion<T>(source, out result))
				{
					return result;
				}
				throw Fx.Exception.AsError(new InvalidCastException(InternalSR.CannotConvertObject(source, typeof(T))));
			}
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00006FC4 File Offset: 0x000051C4
		public static IEnumerable<Type> GetImplementedTypes(Type type)
		{
			Dictionary<Type, object> dictionary = new Dictionary<Type, object>();
			TypeHelper.GetImplementedTypesHelper(type, dictionary);
			return dictionary.Keys;
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00006FE4 File Offset: 0x000051E4
		private static void GetImplementedTypesHelper(Type type, Dictionary<Type, object> typesEncountered)
		{
			if (typesEncountered.ContainsKey(type))
			{
				return;
			}
			typesEncountered.Add(type, type);
			Type[] interfaces = type.GetInterfaces();
			for (int i = 0; i < interfaces.Length; i++)
			{
				TypeHelper.GetImplementedTypesHelper(interfaces[i], typesEncountered);
			}
			Type baseType = type.BaseType;
			while (baseType != null && baseType != TypeHelper.ObjectType)
			{
				TypeHelper.GetImplementedTypesHelper(baseType, typesEncountered);
				baseType = baseType.BaseType;
			}
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00007050 File Offset: 0x00005250
		private static bool IsImplicitNumericConversion(Type source, Type destination)
		{
			TypeCode typeCode = Type.GetTypeCode(source);
			TypeCode typeCode2 = Type.GetTypeCode(destination);
			switch (typeCode)
			{
			case TypeCode.Char:
				return typeCode2 - TypeCode.UInt16 <= 7;
			case TypeCode.SByte:
				switch (typeCode2)
				{
				case TypeCode.Int16:
				case TypeCode.Int32:
				case TypeCode.Int64:
				case TypeCode.Single:
				case TypeCode.Double:
				case TypeCode.Decimal:
					return true;
				}
				return false;
			case TypeCode.Byte:
				return typeCode2 - TypeCode.Int16 <= 8;
			case TypeCode.Int16:
				switch (typeCode2)
				{
				case TypeCode.Int32:
				case TypeCode.Int64:
				case TypeCode.Single:
				case TypeCode.Double:
				case TypeCode.Decimal:
					return true;
				}
				return false;
			case TypeCode.UInt16:
				return typeCode2 - TypeCode.Int32 <= 6;
			case TypeCode.Int32:
				return typeCode2 == TypeCode.Int64 || typeCode2 - TypeCode.Single <= 2;
			case TypeCode.UInt32:
				return typeCode2 - TypeCode.UInt32 <= 5;
			case TypeCode.Int64:
			case TypeCode.UInt64:
				return typeCode2 - TypeCode.Single <= 2;
			case TypeCode.Single:
				return typeCode2 == TypeCode.Double;
			default:
				return false;
			}
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00007149 File Offset: 0x00005349
		private static bool IsImplicitReferenceConversion(Type sourceType, Type destinationType)
		{
			return destinationType.IsAssignableFrom(sourceType);
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00007154 File Offset: 0x00005354
		private static bool IsImplicitBoxingConversion(Type sourceType, Type destinationType)
		{
			return (sourceType.IsValueType && (destinationType == TypeHelper.ObjectType || destinationType == typeof(ValueType))) || (sourceType.IsEnum && destinationType == typeof(Enum));
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x000071A7 File Offset: 0x000053A7
		private static bool IsImplicitNullableConversion(Type sourceType, Type destinationType)
		{
			if (!TypeHelper.IsNullableType(destinationType))
			{
				return false;
			}
			destinationType = destinationType.GetGenericArguments()[0];
			if (TypeHelper.IsNullableType(sourceType))
			{
				sourceType = sourceType.GetGenericArguments()[0];
			}
			return TypeHelper.AreTypesCompatible(sourceType, destinationType);
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x000071D6 File Offset: 0x000053D6
		private static bool IsNullableType(Type type)
		{
			return type.IsGenericType && type.GetGenericTypeDefinition() == TypeHelper.NullableType;
		}

		// Token: 0x060001AA RID: 426 RVA: 0x000071F4 File Offset: 0x000053F4
		private static bool TryNumericConversion<T>(object source, out T result)
		{
			TypeCode typeCode = Type.GetTypeCode(source.GetType());
			TypeCode typeCode2 = Type.GetTypeCode(typeof(T));
			switch (typeCode)
			{
			case TypeCode.Char:
			{
				char c = (char)source;
				switch (typeCode2)
				{
				case TypeCode.UInt16:
					result = (T)((object)((ushort)c));
					return true;
				case TypeCode.Int32:
					result = (T)((object)((int)c));
					return true;
				case TypeCode.UInt32:
					result = (T)((object)((uint)c));
					return true;
				case TypeCode.Int64:
					result = (T)((object)((long)((ulong)c)));
					return true;
				case TypeCode.UInt64:
					result = (T)((object)((ulong)c));
					return true;
				case TypeCode.Single:
					result = (T)((object)((float)c));
					return true;
				case TypeCode.Double:
					result = (T)((object)((double)c));
					return true;
				case TypeCode.Decimal:
					result = (T)((object)c);
					return true;
				}
				break;
			}
			case TypeCode.SByte:
			{
				sbyte b = (sbyte)source;
				switch (typeCode2)
				{
				case TypeCode.Int16:
					result = (T)((object)((short)b));
					return true;
				case TypeCode.Int32:
					result = (T)((object)((int)b));
					return true;
				case TypeCode.Int64:
					result = (T)((object)((long)b));
					return true;
				case TypeCode.Single:
					result = (T)((object)((float)b));
					return true;
				case TypeCode.Double:
					result = (T)((object)((double)b));
					return true;
				case TypeCode.Decimal:
					result = (T)((object)b);
					return true;
				}
				break;
			}
			case TypeCode.Byte:
			{
				byte b2 = (byte)source;
				switch (typeCode2)
				{
				case TypeCode.Int16:
					result = (T)((object)((short)b2));
					return true;
				case TypeCode.UInt16:
					result = (T)((object)((ushort)b2));
					return true;
				case TypeCode.Int32:
					result = (T)((object)((int)b2));
					return true;
				case TypeCode.UInt32:
					result = (T)((object)((uint)b2));
					return true;
				case TypeCode.Int64:
					result = (T)((object)((long)((ulong)b2)));
					return true;
				case TypeCode.UInt64:
					result = (T)((object)((ulong)b2));
					return true;
				case TypeCode.Single:
					result = (T)((object)((float)b2));
					return true;
				case TypeCode.Double:
					result = (T)((object)((double)b2));
					return true;
				case TypeCode.Decimal:
					result = (T)((object)b2);
					return true;
				}
				break;
			}
			case TypeCode.Int16:
			{
				short num = (short)source;
				switch (typeCode2)
				{
				case TypeCode.Int32:
					result = (T)((object)((int)num));
					return true;
				case TypeCode.Int64:
					result = (T)((object)((long)num));
					return true;
				case TypeCode.Single:
					result = (T)((object)((float)num));
					return true;
				case TypeCode.Double:
					result = (T)((object)((double)num));
					return true;
				case TypeCode.Decimal:
					result = (T)((object)num);
					return true;
				}
				break;
			}
			case TypeCode.UInt16:
			{
				ushort num2 = (ushort)source;
				switch (typeCode2)
				{
				case TypeCode.Int32:
					result = (T)((object)((int)num2));
					return true;
				case TypeCode.UInt32:
					result = (T)((object)((uint)num2));
					return true;
				case TypeCode.Int64:
					result = (T)((object)((long)((ulong)num2)));
					return true;
				case TypeCode.UInt64:
					result = (T)((object)((ulong)num2));
					return true;
				case TypeCode.Single:
					result = (T)((object)((float)num2));
					return true;
				case TypeCode.Double:
					result = (T)((object)((double)num2));
					return true;
				case TypeCode.Decimal:
					result = (T)((object)num2);
					return true;
				}
				break;
			}
			case TypeCode.Int32:
			{
				int num3 = (int)source;
				switch (typeCode2)
				{
				case TypeCode.Int64:
					result = (T)((object)((long)num3));
					return true;
				case TypeCode.Single:
					result = (T)((object)((float)num3));
					return true;
				case TypeCode.Double:
					result = (T)((object)((double)num3));
					return true;
				case TypeCode.Decimal:
					result = (T)((object)num3);
					return true;
				}
				break;
			}
			case TypeCode.UInt32:
			{
				uint num4 = (uint)source;
				switch (typeCode2)
				{
				case TypeCode.UInt32:
					result = (T)((object)num4);
					return true;
				case TypeCode.Int64:
					result = (T)((object)((long)((ulong)num4)));
					return true;
				case TypeCode.UInt64:
					result = (T)((object)((ulong)num4));
					return true;
				case TypeCode.Single:
					result = (T)((object)num4);
					return true;
				case TypeCode.Double:
					result = (T)((object)num4);
					return true;
				case TypeCode.Decimal:
					result = (T)((object)num4);
					return true;
				}
				break;
			}
			case TypeCode.Int64:
			{
				long num5 = (long)source;
				switch (typeCode2)
				{
				case TypeCode.Single:
					result = (T)((object)((float)num5));
					return true;
				case TypeCode.Double:
					result = (T)((object)((double)num5));
					return true;
				case TypeCode.Decimal:
					result = (T)((object)num5);
					return true;
				}
				break;
			}
			case TypeCode.UInt64:
			{
				ulong num6 = (ulong)source;
				switch (typeCode2)
				{
				case TypeCode.Single:
					result = (T)((object)num6);
					return true;
				case TypeCode.Double:
					result = (T)((object)num6);
					return true;
				case TypeCode.Decimal:
					result = (T)((object)num6);
					return true;
				}
				break;
			}
			case TypeCode.Single:
				if (typeCode2 == TypeCode.Double)
				{
					result = (T)((object)((double)((float)source)));
					return true;
				}
				break;
			}
			result = default(T);
			return false;
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00007858 File Offset: 0x00005A58
		public static object GetDefaultValueForType(Type type)
		{
			if (!type.IsValueType)
			{
				return null;
			}
			if (type.IsEnum)
			{
				Array values = Enum.GetValues(type);
				if (values.Length > 0)
				{
					return values.GetValue(0);
				}
			}
			return Activator.CreateInstance(type);
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00007895 File Offset: 0x00005A95
		public static bool IsNullableValueType(Type type)
		{
			return type.IsValueType && TypeHelper.IsNullableType(type);
		}

		// Token: 0x060001AD RID: 429 RVA: 0x000078A7 File Offset: 0x00005AA7
		public static bool IsNonNullableValueType(Type type)
		{
			return type.IsValueType && !type.IsGenericType && type != TypeHelper.StringType;
		}

		// Token: 0x060001AE RID: 430 RVA: 0x000078C8 File Offset: 0x00005AC8
		public static bool ShouldFilterProperty(PropertyDescriptor property, Attribute[] attributes)
		{
			if (attributes == null || attributes.Length == 0)
			{
				return false;
			}
			foreach (Attribute attribute in attributes)
			{
				Attribute attribute2 = property.Attributes[attribute.GetType()];
				if (attribute2 == null)
				{
					if (!attribute.IsDefaultAttribute())
					{
						return true;
					}
				}
				else if (!attribute.Match(attribute2))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x040000CC RID: 204
		public static readonly Type ArrayType = typeof(Array);

		// Token: 0x040000CD RID: 205
		public static readonly Type BoolType = typeof(bool);

		// Token: 0x040000CE RID: 206
		public static readonly Type GenericCollectionType = typeof(ICollection<>);

		// Token: 0x040000CF RID: 207
		public static readonly Type ByteType = typeof(byte);

		// Token: 0x040000D0 RID: 208
		public static readonly Type SByteType = typeof(sbyte);

		// Token: 0x040000D1 RID: 209
		public static readonly Type CharType = typeof(char);

		// Token: 0x040000D2 RID: 210
		public static readonly Type ShortType = typeof(short);

		// Token: 0x040000D3 RID: 211
		public static readonly Type UShortType = typeof(ushort);

		// Token: 0x040000D4 RID: 212
		public static readonly Type IntType = typeof(int);

		// Token: 0x040000D5 RID: 213
		public static readonly Type UIntType = typeof(uint);

		// Token: 0x040000D6 RID: 214
		public static readonly Type LongType = typeof(long);

		// Token: 0x040000D7 RID: 215
		public static readonly Type ULongType = typeof(ulong);

		// Token: 0x040000D8 RID: 216
		public static readonly Type FloatType = typeof(float);

		// Token: 0x040000D9 RID: 217
		public static readonly Type DoubleType = typeof(double);

		// Token: 0x040000DA RID: 218
		public static readonly Type DecimalType = typeof(decimal);

		// Token: 0x040000DB RID: 219
		public static readonly Type ExceptionType = typeof(Exception);

		// Token: 0x040000DC RID: 220
		public static readonly Type NullableType = typeof(Nullable<>);

		// Token: 0x040000DD RID: 221
		public static readonly Type ObjectType = typeof(object);

		// Token: 0x040000DE RID: 222
		public static readonly Type StringType = typeof(string);

		// Token: 0x040000DF RID: 223
		public static readonly Type TypeType = typeof(Type);

		// Token: 0x040000E0 RID: 224
		public static readonly Type VoidType = typeof(void);
	}
}
