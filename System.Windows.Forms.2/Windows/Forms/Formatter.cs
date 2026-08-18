using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;

namespace System.Windows.Forms
{
	// Token: 0x0200025E RID: 606
	internal class Formatter
	{
		// Token: 0x06002770 RID: 10096 RVA: 0x000B863C File Offset: 0x000B683C
		public static object FormatObject(object value, Type targetType, TypeConverter sourceConverter, TypeConverter targetConverter, string formatString, IFormatProvider formatInfo, object formattedNullValue, object dataSourceNullValue)
		{
			if (Formatter.IsNullData(value, dataSourceNullValue))
			{
				value = DBNull.Value;
			}
			Type type = targetType;
			targetType = Formatter.NullableUnwrap(targetType);
			sourceConverter = Formatter.NullableUnwrap(sourceConverter);
			targetConverter = Formatter.NullableUnwrap(targetConverter);
			bool flag = targetType != type;
			object obj = Formatter.FormatObjectInternal(value, targetType, sourceConverter, targetConverter, formatString, formatInfo, formattedNullValue);
			if (type.IsValueType && obj == null && !flag)
			{
				throw new FormatException(Formatter.GetCantConvertMessage(value, targetType));
			}
			return obj;
		}

		// Token: 0x06002771 RID: 10097 RVA: 0x000B86A8 File Offset: 0x000B68A8
		private static object FormatObjectInternal(object value, Type targetType, TypeConverter sourceConverter, TypeConverter targetConverter, string formatString, IFormatProvider formatInfo, object formattedNullValue)
		{
			if (value == DBNull.Value || value == null)
			{
				if (formattedNullValue != null)
				{
					return formattedNullValue;
				}
				if (targetType == Formatter.stringType)
				{
					return string.Empty;
				}
				if (targetType == Formatter.checkStateType)
				{
					return CheckState.Indeterminate;
				}
				return null;
			}
			else
			{
				if (targetType == Formatter.stringType && value is IFormattable && !string.IsNullOrEmpty(formatString))
				{
					return (value as IFormattable).ToString(formatString, formatInfo);
				}
				Type type = value.GetType();
				TypeConverter converter = TypeDescriptor.GetConverter(type);
				if (sourceConverter != null && sourceConverter != converter && sourceConverter.CanConvertTo(targetType))
				{
					return sourceConverter.ConvertTo(null, Formatter.GetFormatterCulture(formatInfo), value, targetType);
				}
				TypeConverter converter2 = TypeDescriptor.GetConverter(targetType);
				if (targetConverter != null && targetConverter != converter2 && targetConverter.CanConvertFrom(type))
				{
					return targetConverter.ConvertFrom(null, Formatter.GetFormatterCulture(formatInfo), value);
				}
				if (targetType == Formatter.checkStateType)
				{
					if (type == Formatter.booleanType)
					{
						return ((bool)value) ? CheckState.Checked : CheckState.Unchecked;
					}
					if (sourceConverter == null)
					{
						sourceConverter = converter;
					}
					if (sourceConverter != null && sourceConverter.CanConvertTo(Formatter.booleanType))
					{
						return ((bool)sourceConverter.ConvertTo(null, Formatter.GetFormatterCulture(formatInfo), value, Formatter.booleanType)) ? CheckState.Checked : CheckState.Unchecked;
					}
				}
				if (targetType.IsAssignableFrom(type))
				{
					return value;
				}
				if (sourceConverter == null)
				{
					sourceConverter = converter;
				}
				if (targetConverter == null)
				{
					targetConverter = converter2;
				}
				if (sourceConverter != null && sourceConverter.CanConvertTo(targetType))
				{
					return sourceConverter.ConvertTo(null, Formatter.GetFormatterCulture(formatInfo), value, targetType);
				}
				if (targetConverter != null && targetConverter.CanConvertFrom(type))
				{
					return targetConverter.ConvertFrom(null, Formatter.GetFormatterCulture(formatInfo), value);
				}
				if (value is IConvertible)
				{
					return Formatter.ChangeType(value, targetType, formatInfo);
				}
				throw new FormatException(Formatter.GetCantConvertMessage(value, targetType));
			}
		}

		// Token: 0x06002772 RID: 10098 RVA: 0x000B884C File Offset: 0x000B6A4C
		public static object ParseObject(object value, Type targetType, Type sourceType, TypeConverter targetConverter, TypeConverter sourceConverter, IFormatProvider formatInfo, object formattedNullValue, object dataSourceNullValue)
		{
			Type type = targetType;
			sourceType = Formatter.NullableUnwrap(sourceType);
			targetType = Formatter.NullableUnwrap(targetType);
			sourceConverter = Formatter.NullableUnwrap(sourceConverter);
			targetConverter = Formatter.NullableUnwrap(targetConverter);
			bool flag = targetType != type;
			object obj = Formatter.ParseObjectInternal(value, targetType, sourceType, targetConverter, sourceConverter, formatInfo, formattedNullValue);
			if (obj == DBNull.Value)
			{
				return Formatter.NullData(type, dataSourceNullValue);
			}
			return obj;
		}

		// Token: 0x06002773 RID: 10099 RVA: 0x000B88A8 File Offset: 0x000B6AA8
		private static object ParseObjectInternal(object value, Type targetType, Type sourceType, TypeConverter targetConverter, TypeConverter sourceConverter, IFormatProvider formatInfo, object formattedNullValue)
		{
			if (Formatter.EqualsFormattedNullValue(value, formattedNullValue, formatInfo) || value == DBNull.Value)
			{
				return DBNull.Value;
			}
			TypeConverter converter = TypeDescriptor.GetConverter(targetType);
			if (targetConverter != null && converter != targetConverter && targetConverter.CanConvertFrom(sourceType))
			{
				return targetConverter.ConvertFrom(null, Formatter.GetFormatterCulture(formatInfo), value);
			}
			TypeConverter converter2 = TypeDescriptor.GetConverter(sourceType);
			if (sourceConverter != null && converter2 != sourceConverter && sourceConverter.CanConvertTo(targetType))
			{
				return sourceConverter.ConvertTo(null, Formatter.GetFormatterCulture(formatInfo), value, targetType);
			}
			if (value is string)
			{
				object obj = Formatter.InvokeStringParseMethod(value, targetType, formatInfo);
				if (obj != Formatter.parseMethodNotFound)
				{
					return obj;
				}
			}
			else if (value is CheckState)
			{
				CheckState checkState = (CheckState)value;
				if (checkState == CheckState.Indeterminate)
				{
					return DBNull.Value;
				}
				if (targetType == Formatter.booleanType)
				{
					return checkState == CheckState.Checked;
				}
				if (targetConverter == null)
				{
					targetConverter = converter;
				}
				if (targetConverter != null && targetConverter.CanConvertFrom(Formatter.booleanType))
				{
					return targetConverter.ConvertFrom(null, Formatter.GetFormatterCulture(formatInfo), checkState == CheckState.Checked);
				}
			}
			else if (value != null && targetType.IsAssignableFrom(value.GetType()))
			{
				return value;
			}
			if (targetConverter == null)
			{
				targetConverter = converter;
			}
			if (sourceConverter == null)
			{
				sourceConverter = converter2;
			}
			if (targetConverter != null && targetConverter.CanConvertFrom(sourceType))
			{
				return targetConverter.ConvertFrom(null, Formatter.GetFormatterCulture(formatInfo), value);
			}
			if (sourceConverter != null && sourceConverter.CanConvertTo(targetType))
			{
				return sourceConverter.ConvertTo(null, Formatter.GetFormatterCulture(formatInfo), value, targetType);
			}
			if (value is IConvertible)
			{
				return Formatter.ChangeType(value, targetType, formatInfo);
			}
			throw new FormatException(Formatter.GetCantConvertMessage(value, targetType));
		}

		// Token: 0x06002774 RID: 10100 RVA: 0x000B8A18 File Offset: 0x000B6C18
		private static object ChangeType(object value, Type type, IFormatProvider formatInfo)
		{
			object result;
			try
			{
				if (formatInfo == null)
				{
					formatInfo = CultureInfo.CurrentCulture;
				}
				result = Convert.ChangeType(value, type, formatInfo);
			}
			catch (InvalidCastException ex)
			{
				throw new FormatException(ex.Message, ex);
			}
			return result;
		}

		// Token: 0x06002775 RID: 10101 RVA: 0x000B8A5C File Offset: 0x000B6C5C
		private static bool EqualsFormattedNullValue(object value, object formattedNullValue, IFormatProvider formatInfo)
		{
			string text = formattedNullValue as string;
			string text2 = value as string;
			if (text != null && text2 != null)
			{
				return text.Length == text2.Length && string.Compare(text2, text, true, Formatter.GetFormatterCulture(formatInfo)) == 0;
			}
			return object.Equals(value, formattedNullValue);
		}

		// Token: 0x06002776 RID: 10102 RVA: 0x000B8AA8 File Offset: 0x000B6CA8
		private static string GetCantConvertMessage(object value, Type targetType)
		{
			string name = (value == null) ? "Formatter_CantConvertNull" : "Formatter_CantConvert";
			return string.Format(CultureInfo.CurrentCulture, SR.GetString(name), new object[]
			{
				value,
				targetType.Name
			});
		}

		// Token: 0x06002777 RID: 10103 RVA: 0x000B8AE8 File Offset: 0x000B6CE8
		private static CultureInfo GetFormatterCulture(IFormatProvider formatInfo)
		{
			if (formatInfo is CultureInfo)
			{
				return formatInfo as CultureInfo;
			}
			return CultureInfo.CurrentCulture;
		}

		// Token: 0x06002778 RID: 10104 RVA: 0x000B8B00 File Offset: 0x000B6D00
		public static object InvokeStringParseMethod(object value, Type targetType, IFormatProvider formatInfo)
		{
			object result;
			try
			{
				MethodInfo method = targetType.GetMethod("Parse", BindingFlags.Static | BindingFlags.Public, null, new Type[]
				{
					Formatter.stringType,
					typeof(NumberStyles),
					typeof(IFormatProvider)
				}, null);
				if (method != null)
				{
					result = method.Invoke(null, new object[]
					{
						(string)value,
						NumberStyles.Any,
						formatInfo
					});
				}
				else
				{
					method = targetType.GetMethod("Parse", BindingFlags.Static | BindingFlags.Public, null, new Type[]
					{
						Formatter.stringType,
						typeof(IFormatProvider)
					}, null);
					if (method != null)
					{
						result = method.Invoke(null, new object[]
						{
							(string)value,
							formatInfo
						});
					}
					else
					{
						method = targetType.GetMethod("Parse", BindingFlags.Static | BindingFlags.Public, null, new Type[]
						{
							Formatter.stringType
						}, null);
						if (method != null)
						{
							result = method.Invoke(null, new object[]
							{
								(string)value
							});
						}
						else
						{
							result = Formatter.parseMethodNotFound;
						}
					}
				}
			}
			catch (TargetInvocationException ex)
			{
				throw new FormatException(ex.InnerException.Message, ex.InnerException);
			}
			return result;
		}

		// Token: 0x06002779 RID: 10105 RVA: 0x000B8C4C File Offset: 0x000B6E4C
		public static bool IsNullData(object value, object dataSourceNullValue)
		{
			return value == null || value == DBNull.Value || object.Equals(value, Formatter.NullData(value.GetType(), dataSourceNullValue));
		}

		// Token: 0x0600277A RID: 10106 RVA: 0x000B8C6D File Offset: 0x000B6E6D
		public static object NullData(Type type, object dataSourceNullValue)
		{
			if (!type.IsGenericType || !(type.GetGenericTypeDefinition() == typeof(Nullable<>)))
			{
				return dataSourceNullValue;
			}
			if (dataSourceNullValue == null || dataSourceNullValue == DBNull.Value)
			{
				return null;
			}
			return dataSourceNullValue;
		}

		// Token: 0x0600277B RID: 10107 RVA: 0x000B8CA0 File Offset: 0x000B6EA0
		private static Type NullableUnwrap(Type type)
		{
			if (type == Formatter.stringType)
			{
				return Formatter.stringType;
			}
			Type underlyingType = Nullable.GetUnderlyingType(type);
			return underlyingType ?? type;
		}

		// Token: 0x0600277C RID: 10108 RVA: 0x000B8CD0 File Offset: 0x000B6ED0
		private static TypeConverter NullableUnwrap(TypeConverter typeConverter)
		{
			NullableConverter nullableConverter = typeConverter as NullableConverter;
			if (nullableConverter == null)
			{
				return typeConverter;
			}
			return nullableConverter.UnderlyingTypeConverter;
		}

		// Token: 0x0600277D RID: 10109 RVA: 0x000B8CEF File Offset: 0x000B6EEF
		public static object GetDefaultDataSourceNullValue(Type type)
		{
			if (!(type != null) || type.IsValueType)
			{
				return Formatter.defaultDataSourceNullValue;
			}
			return null;
		}

		// Token: 0x0400103A RID: 4154
		private static Type stringType = typeof(string);

		// Token: 0x0400103B RID: 4155
		private static Type booleanType = typeof(bool);

		// Token: 0x0400103C RID: 4156
		private static Type checkStateType = typeof(CheckState);

		// Token: 0x0400103D RID: 4157
		private static object parseMethodNotFound = new object();

		// Token: 0x0400103E RID: 4158
		private static object defaultDataSourceNullValue = DBNull.Value;
	}
}
