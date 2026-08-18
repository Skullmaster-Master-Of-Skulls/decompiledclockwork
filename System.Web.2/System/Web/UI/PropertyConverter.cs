using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;

namespace System.Web.UI
{
	// Token: 0x020002F2 RID: 754
	public static class PropertyConverter
	{
		// Token: 0x060022F8 RID: 8952 RVA: 0x00071E74 File Offset: 0x00070074
		public static object EnumFromString(Type enumType, string value)
		{
			object result;
			try
			{
				result = Enum.Parse(enumType, value, true);
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060022F9 RID: 8953 RVA: 0x00071EA4 File Offset: 0x000700A4
		public static string EnumToString(Type enumType, object enumValue)
		{
			string text = Enum.Format(enumType, enumValue, "G");
			return text.Replace('_', '-');
		}

		// Token: 0x060022FA RID: 8954 RVA: 0x00071EC8 File Offset: 0x000700C8
		public static object ObjectFromString(Type objType, MemberInfo propertyInfo, string value)
		{
			if (value == null)
			{
				return null;
			}
			if (objType.Equals(typeof(bool)) && value.Length == 0)
			{
				return null;
			}
			bool flag = true;
			object obj = null;
			try
			{
				if (objType.IsEnum)
				{
					flag = false;
					obj = PropertyConverter.EnumFromString(objType, value);
				}
				else if (objType.Equals(typeof(string)))
				{
					flag = false;
					obj = value;
				}
				else
				{
					PropertyDescriptor propertyDescriptor = null;
					if (propertyInfo != null)
					{
						propertyDescriptor = TypeDescriptor.GetProperties(propertyInfo.ReflectedType)[propertyInfo.Name];
					}
					if (propertyDescriptor != null)
					{
						TypeConverter converter = propertyDescriptor.Converter;
						if (converter != null && converter.CanConvertFrom(typeof(string)))
						{
							flag = false;
							obj = converter.ConvertFromInvariantString(value);
						}
					}
				}
			}
			catch
			{
			}
			if (flag)
			{
				MethodInfo method = objType.GetMethod("Parse", PropertyConverter.s_parseMethodTypesWithSOP);
				if (method != null)
				{
					object[] parameters = new object[]
					{
						value,
						CultureInfo.InvariantCulture
					};
					try
					{
						obj = Util.InvokeMethod(method, null, parameters);
						goto IL_11F;
					}
					catch
					{
						goto IL_11F;
					}
				}
				method = objType.GetMethod("Parse", PropertyConverter.s_parseMethodTypes);
				if (method != null)
				{
					object[] parameters2 = new object[]
					{
						value
					};
					try
					{
						obj = Util.InvokeMethod(method, null, parameters2);
					}
					catch
					{
					}
				}
			}
			IL_11F:
			if (obj == null)
			{
				throw new HttpException(SR.GetString("Type_not_creatable_from_string", new object[]
				{
					objType.FullName,
					value,
					propertyInfo.Name
				}));
			}
			return obj;
		}

		// Token: 0x04001C91 RID: 7313
		private static readonly Type[] s_parseMethodTypes = new Type[]
		{
			typeof(string)
		};

		// Token: 0x04001C92 RID: 7314
		private static readonly Type[] s_parseMethodTypesWithSOP = new Type[]
		{
			typeof(string),
			typeof(IServiceProvider)
		};
	}
}
