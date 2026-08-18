using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Security.Permissions;

namespace System.Web.UI
{
	// Token: 0x020003CF RID: 975
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class DataBinder
	{
		// Token: 0x06002F9B RID: 12187 RVA: 0x000D3CB4 File Offset: 0x000D2CB4
		public static object Eval(object container, string expression)
		{
			if (expression == null)
			{
				throw new ArgumentNullException("expression");
			}
			expression = expression.Trim();
			if (expression.Length == 0)
			{
				throw new ArgumentNullException("expression");
			}
			if (container == null)
			{
				return null;
			}
			string[] expressionParts = expression.Split(DataBinder.expressionPartSeparator);
			return DataBinder.Eval(container, expressionParts);
		}

		// Token: 0x06002F9C RID: 12188 RVA: 0x000D3D04 File Offset: 0x000D2D04
		private static object Eval(object container, string[] expressionParts)
		{
			object obj = container;
			int num = 0;
			while (num < expressionParts.Length && obj != null)
			{
				string text = expressionParts[num];
				if (text.IndexOfAny(DataBinder.indexExprStartChars) < 0)
				{
					obj = DataBinder.GetPropertyValue(obj, text);
				}
				else
				{
					obj = DataBinder.GetIndexedPropertyValue(obj, text);
				}
				num++;
			}
			return obj;
		}

		// Token: 0x06002F9D RID: 12189 RVA: 0x000D3D50 File Offset: 0x000D2D50
		public static string Eval(object container, string expression, string format)
		{
			object obj = DataBinder.Eval(container, expression);
			if (obj == null || obj == DBNull.Value)
			{
				return string.Empty;
			}
			if (string.IsNullOrEmpty(format))
			{
				return obj.ToString();
			}
			return string.Format(format, obj);
		}

		// Token: 0x06002F9E RID: 12190 RVA: 0x000D3D8C File Offset: 0x000D2D8C
		public static object GetPropertyValue(object container, string propName)
		{
			if (container == null)
			{
				throw new ArgumentNullException("container");
			}
			if (string.IsNullOrEmpty(propName))
			{
				throw new ArgumentNullException("propName");
			}
			PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(container).Find(propName, true);
			if (propertyDescriptor != null)
			{
				return propertyDescriptor.GetValue(container);
			}
			throw new HttpException(SR.GetString("DataBinder_Prop_Not_Found", new object[]
			{
				container.GetType().FullName,
				propName
			}));
		}

		// Token: 0x06002F9F RID: 12191 RVA: 0x000D3E04 File Offset: 0x000D2E04
		public static string GetPropertyValue(object container, string propName, string format)
		{
			object propertyValue = DataBinder.GetPropertyValue(container, propName);
			if (propertyValue == null || propertyValue == DBNull.Value)
			{
				return string.Empty;
			}
			if (string.IsNullOrEmpty(format))
			{
				return propertyValue.ToString();
			}
			return string.Format(format, propertyValue);
		}

		// Token: 0x06002FA0 RID: 12192 RVA: 0x000D3E40 File Offset: 0x000D2E40
		public static object GetIndexedPropertyValue(object container, string expr)
		{
			if (container == null)
			{
				throw new ArgumentNullException("container");
			}
			if (string.IsNullOrEmpty(expr))
			{
				throw new ArgumentNullException("expr");
			}
			object result = null;
			bool flag = false;
			int num = expr.IndexOfAny(DataBinder.indexExprStartChars);
			int num2 = expr.IndexOfAny(DataBinder.indexExprEndChars, num + 1);
			if (num < 0 || num2 < 0 || num2 == num + 1)
			{
				throw new ArgumentException(SR.GetString("DataBinder_Invalid_Indexed_Expr", new object[]
				{
					expr
				}));
			}
			string text = null;
			object obj = null;
			string text2 = expr.Substring(num + 1, num2 - num - 1).Trim();
			if (num != 0)
			{
				text = expr.Substring(0, num);
			}
			if (text2.Length != 0)
			{
				if ((text2[0] == '"' && text2[text2.Length - 1] == '"') || (text2[0] == '\'' && text2[text2.Length - 1] == '\''))
				{
					obj = text2.Substring(1, text2.Length - 2);
				}
				else if (char.IsDigit(text2[0]))
				{
					int num3;
					flag = int.TryParse(text2, NumberStyles.Integer, CultureInfo.InvariantCulture, out num3);
					if (flag)
					{
						obj = num3;
					}
					else
					{
						obj = text2;
					}
				}
				else
				{
					obj = text2;
				}
			}
			if (obj == null)
			{
				throw new ArgumentException(SR.GetString("DataBinder_Invalid_Indexed_Expr", new object[]
				{
					expr
				}));
			}
			object obj2;
			if (text != null && text.Length != 0)
			{
				obj2 = DataBinder.GetPropertyValue(container, text);
			}
			else
			{
				obj2 = container;
			}
			if (obj2 != null)
			{
				Array array = obj2 as Array;
				if (array != null && flag)
				{
					result = array.GetValue((int)obj);
				}
				else if (obj2 is IList && flag)
				{
					result = ((IList)obj2)[(int)obj];
				}
				else
				{
					PropertyInfo property = obj2.GetType().GetProperty("Item", BindingFlags.Instance | BindingFlags.Public, null, null, new Type[]
					{
						obj.GetType()
					}, null);
					if (property == null)
					{
						throw new ArgumentException(SR.GetString("DataBinder_No_Indexed_Accessor", new object[]
						{
							obj2.GetType().FullName
						}));
					}
					result = property.GetValue(obj2, new object[]
					{
						obj
					});
				}
			}
			return result;
		}

		// Token: 0x06002FA1 RID: 12193 RVA: 0x000D4084 File Offset: 0x000D3084
		public static string GetIndexedPropertyValue(object container, string propName, string format)
		{
			object indexedPropertyValue = DataBinder.GetIndexedPropertyValue(container, propName);
			if (indexedPropertyValue == null || indexedPropertyValue == DBNull.Value)
			{
				return string.Empty;
			}
			if (string.IsNullOrEmpty(format))
			{
				return indexedPropertyValue.ToString();
			}
			return string.Format(format, indexedPropertyValue);
		}

		// Token: 0x06002FA2 RID: 12194 RVA: 0x000D40C0 File Offset: 0x000D30C0
		public static object GetDataItem(object container)
		{
			bool flag;
			return DataBinder.GetDataItem(container, out flag);
		}

		// Token: 0x06002FA3 RID: 12195 RVA: 0x000D40D8 File Offset: 0x000D30D8
		public static object GetDataItem(object container, out bool foundDataItem)
		{
			if (container == null)
			{
				foundDataItem = false;
				return null;
			}
			IDataItemContainer dataItemContainer = container as IDataItemContainer;
			if (dataItemContainer != null)
			{
				foundDataItem = true;
				return dataItemContainer.DataItem;
			}
			string name = "DataItem";
			PropertyInfo property = container.GetType().GetProperty(name, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);
			if (property == null)
			{
				foundDataItem = false;
				return null;
			}
			foundDataItem = true;
			return property.GetValue(container, null);
		}

		// Token: 0x06002FA4 RID: 12196 RVA: 0x000D4129 File Offset: 0x000D3129
		internal static bool IsNull(object value)
		{
			return value == null || Convert.IsDBNull(value);
		}

		// Token: 0x040021E9 RID: 8681
		private static readonly char[] expressionPartSeparator = new char[]
		{
			'.'
		};

		// Token: 0x040021EA RID: 8682
		private static readonly char[] indexExprStartChars = new char[]
		{
			'[',
			'('
		};

		// Token: 0x040021EB RID: 8683
		private static readonly char[] indexExprEndChars = new char[]
		{
			']',
			')'
		};
	}
}
