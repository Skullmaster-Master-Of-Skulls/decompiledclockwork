using System;
using System.Collections;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Web.UI.WebControls;

namespace System.Web.UI
{
	// Token: 0x0200026E RID: 622
	public sealed class DataBinder
	{
		// Token: 0x17000854 RID: 2132
		// (get) Token: 0x06001D98 RID: 7576 RVA: 0x000600EA File Offset: 0x0005E2EA
		// (set) Token: 0x06001D99 RID: 7577 RVA: 0x000600F1 File Offset: 0x0005E2F1
		public static bool EnableCaching
		{
			get
			{
				return DataBinder.enableCaching;
			}
			set
			{
				DataBinder.enableCaching = value;
				if (!value)
				{
					DataBinder.propertyCache.Clear();
				}
			}
		}

		// Token: 0x06001D9B RID: 7579 RVA: 0x00060108 File Offset: 0x0005E308
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

		// Token: 0x06001D9C RID: 7580 RVA: 0x00060158 File Offset: 0x0005E358
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

		// Token: 0x06001D9D RID: 7581 RVA: 0x000601A4 File Offset: 0x0005E3A4
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

		// Token: 0x06001D9E RID: 7582 RVA: 0x000601E0 File Offset: 0x0005E3E0
		internal static PropertyDescriptorCollection GetPropertiesFromCache(object container)
		{
			if (DataBinder.EnableCaching && !(container is ICustomTypeDescriptor))
			{
				PropertyDescriptorCollection propertyDescriptorCollection = null;
				Type type = container.GetType();
				if (!DataBinder.propertyCache.TryGetValue(type, out propertyDescriptorCollection))
				{
					propertyDescriptorCollection = TypeDescriptor.GetProperties(type);
					DataBinder.propertyCache.TryAdd(type, propertyDescriptorCollection);
				}
				return propertyDescriptorCollection;
			}
			return TypeDescriptor.GetProperties(container);
		}

		// Token: 0x06001D9F RID: 7583 RVA: 0x00060230 File Offset: 0x0005E430
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
			PropertyDescriptor propertyDescriptor = DataBinder.GetPropertiesFromCache(container).Find(propName, true);
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

		// Token: 0x06001DA0 RID: 7584 RVA: 0x000602A4 File Offset: 0x0005E4A4
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

		// Token: 0x06001DA1 RID: 7585 RVA: 0x000602E0 File Offset: 0x0005E4E0
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
					if (!(property != null))
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

		// Token: 0x06001DA2 RID: 7586 RVA: 0x00060514 File Offset: 0x0005E714
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

		// Token: 0x06001DA3 RID: 7587 RVA: 0x00060550 File Offset: 0x0005E750
		public static object GetDataItem(object container)
		{
			bool flag;
			return DataBinder.GetDataItem(container, out flag);
		}

		// Token: 0x06001DA4 RID: 7588 RVA: 0x00060568 File Offset: 0x0005E768
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

		// Token: 0x06001DA5 RID: 7589 RVA: 0x000605BF File Offset: 0x0005E7BF
		public static bool IsBindableType(Type type)
		{
			return DataBoundControlHelper.IsBindableType(type, true);
		}

		// Token: 0x06001DA6 RID: 7590 RVA: 0x000605C8 File Offset: 0x0005E7C8
		internal static bool IsNull(object value)
		{
			return value == null || Convert.IsDBNull(value);
		}

		// Token: 0x0400195E RID: 6494
		private static readonly char[] expressionPartSeparator = new char[]
		{
			'.'
		};

		// Token: 0x0400195F RID: 6495
		private static readonly char[] indexExprStartChars = new char[]
		{
			'[',
			'('
		};

		// Token: 0x04001960 RID: 6496
		private static readonly char[] indexExprEndChars = new char[]
		{
			']',
			')'
		};

		// Token: 0x04001961 RID: 6497
		private static readonly ConcurrentDictionary<Type, PropertyDescriptorCollection> propertyCache = new ConcurrentDictionary<Type, PropertyDescriptorCollection>();

		// Token: 0x04001962 RID: 6498
		private static bool enableCaching = true;
	}
}
