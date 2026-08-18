using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Text;
using NLog.Common;
using NLog.Conditions;
using NLog.Config;
using NLog.Layouts;

namespace NLog.Internal
{
	// Token: 0x020000A9 RID: 169
	internal static class PropertyHelper
	{
		// Token: 0x06000546 RID: 1350 RVA: 0x0000B97C File Offset: 0x00009B7C
		internal static void SetPropertyFromString(object obj, string propertyName, string value, ConfigurationItemFactory configurationItemFactory)
		{
			InternalLogger.Debug("Setting '{0}.{1}' to '{2}'", new object[]
			{
				obj.GetType().Name,
				propertyName,
				value
			});
			PropertyInfo propertyInfo;
			if (!PropertyHelper.TryGetPropertyInfo(obj, propertyName, out propertyInfo))
			{
				throw new NotSupportedException("Parameter " + propertyName + " not supported on " + obj.GetType().Name);
			}
			try
			{
				if (propertyInfo.IsDefined(typeof(ArrayParameterAttribute), false))
				{
					throw new NotSupportedException(string.Concat(new string[]
					{
						"Parameter ",
						propertyName,
						" of ",
						obj.GetType().Name,
						" is an array and cannot be assigned a scalar value."
					}));
				}
				Type type = propertyInfo.PropertyType;
				type = (Nullable.GetUnderlyingType(type) ?? type);
				object value2;
				if (!PropertyHelper.TryNLogSpecificConversion(type, value, out value2, configurationItemFactory) && !PropertyHelper.TryGetEnumValue(type, value, out value2) && !PropertyHelper.TryImplicitConversion(type, value, out value2) && !PropertyHelper.TrySpecialConversion(type, value, out value2) && !PropertyHelper.TryTypeConverterConversion(type, value, out value2))
				{
					value2 = Convert.ChangeType(value, type, CultureInfo.InvariantCulture);
				}
				propertyInfo.SetValue(obj, value2, null);
			}
			catch (TargetInvocationException ex)
			{
				throw new NLogConfigurationException(string.Concat(new object[]
				{
					"Error when setting property '",
					propertyInfo.Name,
					"' on ",
					obj
				}), ex.InnerException);
			}
			catch (Exception ex2)
			{
				InternalLogger.Warn(ex2, "Error when setting property '{0}' on '{1}'", new object[]
				{
					propertyInfo.Name,
					obj
				});
				if (ex2.MustBeRethrownImmediately())
				{
					throw;
				}
				throw new NLogConfigurationException(string.Concat(new object[]
				{
					"Error when setting property '",
					propertyInfo.Name,
					"' on ",
					obj
				}), ex2);
			}
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x0000BB64 File Offset: 0x00009D64
		internal static bool IsArrayProperty(Type t, string propertyName)
		{
			PropertyInfo propertyInfo;
			if (!PropertyHelper.TryGetPropertyInfo(t, propertyName, out propertyInfo))
			{
				throw new NotSupportedException("Parameter " + propertyName + " not supported on " + t.Name);
			}
			return propertyInfo.IsDefined(typeof(ArrayParameterAttribute), false);
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x0000BBAC File Offset: 0x00009DAC
		internal static bool TryGetPropertyInfo(object obj, string propertyName, out PropertyInfo result)
		{
			PropertyInfo property = obj.GetType().GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);
			if (property != null)
			{
				result = property;
				return true;
			}
			bool result2;
			lock (PropertyHelper.parameterInfoCache)
			{
				Type type = obj.GetType();
				Dictionary<string, PropertyInfo> dictionary;
				if (!PropertyHelper.parameterInfoCache.TryGetValue(type, out dictionary))
				{
					dictionary = PropertyHelper.BuildPropertyInfoDictionary(type);
					PropertyHelper.parameterInfoCache[type] = dictionary;
				}
				result2 = dictionary.TryGetValue(propertyName, out result);
			}
			return result2;
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x0000BC3C File Offset: 0x00009E3C
		internal static Type GetArrayItemType(PropertyInfo propInfo)
		{
			ArrayParameterAttribute arrayParameterAttribute = (ArrayParameterAttribute)Attribute.GetCustomAttribute(propInfo, typeof(ArrayParameterAttribute));
			if (arrayParameterAttribute != null)
			{
				return arrayParameterAttribute.ItemType;
			}
			return null;
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x0000BC6A File Offset: 0x00009E6A
		internal static IEnumerable<PropertyInfo> GetAllReadableProperties(Type type)
		{
			return type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x0000BC74 File Offset: 0x00009E74
		internal static void CheckRequiredParameters(object o)
		{
			foreach (PropertyInfo propertyInfo in PropertyHelper.GetAllReadableProperties(o.GetType()))
			{
				if (propertyInfo.IsDefined(typeof(RequiredParameterAttribute), false) && propertyInfo.GetValue(o, null) == null)
				{
					throw new NLogConfigurationException(string.Concat(new object[]
					{
						"Required parameter '",
						propertyInfo.Name,
						"' on '",
						o,
						"' was not specified."
					}));
				}
			}
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x0000BD18 File Offset: 0x00009F18
		private static bool TryImplicitConversion(Type resultType, string value, out object result)
		{
			MethodInfo method = resultType.GetMethod("op_Implicit", BindingFlags.Static | BindingFlags.Public, null, new Type[]
			{
				typeof(string)
			}, null);
			if (method == null)
			{
				result = null;
				return false;
			}
			result = method.Invoke(null, new object[]
			{
				value
			});
			return true;
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x0000BD70 File Offset: 0x00009F70
		private static bool TryNLogSpecificConversion(Type propertyType, string value, out object newValue, ConfigurationItemFactory configurationItemFactory)
		{
			if (propertyType == typeof(Layout) || propertyType == typeof(SimpleLayout))
			{
				newValue = new SimpleLayout(value, configurationItemFactory);
				return true;
			}
			if (propertyType == typeof(ConditionExpression))
			{
				newValue = ConditionParser.ParseExpression(value, configurationItemFactory);
				return true;
			}
			newValue = null;
			return false;
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x0000BDD0 File Offset: 0x00009FD0
		private static bool TryGetEnumValue(Type resultType, string value, out object result)
		{
			if (!resultType.IsEnum)
			{
				result = null;
				return false;
			}
			if (resultType.IsDefined(typeof(FlagsAttribute), false))
			{
				ulong num = 0UL;
				foreach (string text in value.Split(new char[]
				{
					','
				}))
				{
					FieldInfo field = resultType.GetField(text.Trim(), BindingFlags.IgnoreCase | BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy);
					if (field == null)
					{
						throw new NLogConfigurationException("Invalid enumeration value '" + value + "'.");
					}
					num |= Convert.ToUInt64(field.GetValue(null), CultureInfo.InvariantCulture);
				}
				result = Convert.ChangeType(num, Enum.GetUnderlyingType(resultType), CultureInfo.InvariantCulture);
				result = Enum.ToObject(resultType, result);
				return true;
			}
			FieldInfo field2 = resultType.GetField(value, BindingFlags.IgnoreCase | BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy);
			if (field2 == null)
			{
				throw new NLogConfigurationException("Invalid enumeration value '" + value + "'.");
			}
			result = field2.GetValue(null);
			return true;
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x0000BED0 File Offset: 0x0000A0D0
		private static bool TrySpecialConversion(Type type, string value, out object newValue)
		{
			if (type == typeof(Encoding))
			{
				newValue = Encoding.GetEncoding(value);
				return true;
			}
			if (type == typeof(CultureInfo))
			{
				newValue = new CultureInfo(value);
				return true;
			}
			if (type == typeof(Type))
			{
				newValue = Type.GetType(value, true);
				return true;
			}
			newValue = null;
			return false;
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x0000BF38 File Offset: 0x0000A138
		private static bool TryTypeConverterConversion(Type type, string value, out object newValue)
		{
			TypeConverter converter = TypeDescriptor.GetConverter(type);
			if (converter.CanConvertFrom(typeof(string)))
			{
				newValue = converter.ConvertFromInvariantString(value);
				return true;
			}
			newValue = null;
			return false;
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x0000BF70 File Offset: 0x0000A170
		private static bool TryGetPropertyInfo(Type targetType, string propertyName, out PropertyInfo result)
		{
			if (!string.IsNullOrEmpty(propertyName))
			{
				PropertyInfo property = targetType.GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);
				if (property != null)
				{
					result = property;
					return true;
				}
			}
			bool result2;
			lock (PropertyHelper.parameterInfoCache)
			{
				Dictionary<string, PropertyInfo> dictionary;
				if (!PropertyHelper.parameterInfoCache.TryGetValue(targetType, out dictionary))
				{
					dictionary = PropertyHelper.BuildPropertyInfoDictionary(targetType);
					PropertyHelper.parameterInfoCache[targetType] = dictionary;
				}
				result2 = dictionary.TryGetValue(propertyName, out result);
			}
			return result2;
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x0000BFF8 File Offset: 0x0000A1F8
		private static Dictionary<string, PropertyInfo> BuildPropertyInfoDictionary(Type t)
		{
			Dictionary<string, PropertyInfo> dictionary = new Dictionary<string, PropertyInfo>(StringComparer.OrdinalIgnoreCase);
			foreach (PropertyInfo propertyInfo in PropertyHelper.GetAllReadableProperties(t))
			{
				ArrayParameterAttribute arrayParameterAttribute = (ArrayParameterAttribute)Attribute.GetCustomAttribute(propertyInfo, typeof(ArrayParameterAttribute));
				if (arrayParameterAttribute != null)
				{
					dictionary[arrayParameterAttribute.ElementName] = propertyInfo;
				}
				else
				{
					dictionary[propertyInfo.Name] = propertyInfo;
				}
				if (propertyInfo.IsDefined(typeof(DefaultParameterAttribute), false))
				{
					dictionary[string.Empty] = propertyInfo;
				}
			}
			return dictionary;
		}

		// Token: 0x04000116 RID: 278
		private static Dictionary<Type, Dictionary<string, PropertyInfo>> parameterInfoCache = new Dictionary<Type, Dictionary<string, PropertyInfo>>();
	}
}
