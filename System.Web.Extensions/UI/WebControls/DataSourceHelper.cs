using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Web.Compilation;
using System.Web.Resources;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000095 RID: 149
	internal static class DataSourceHelper
	{
		// Token: 0x06000692 RID: 1682 RVA: 0x0001C452 File Offset: 0x0001A652
		public static object SaveViewState(ParameterCollection parameters)
		{
			if (parameters != null)
			{
				return ((IStateManager)parameters).SaveViewState();
			}
			return null;
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x0001C45F File Offset: 0x0001A65F
		public static void TrackViewState(ParameterCollection parameters)
		{
			if (parameters != null)
			{
				((IStateManager)parameters).TrackViewState();
			}
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x0001C46A File Offset: 0x0001A66A
		public static IDictionary<string, object> ToDictionary(this ParameterCollection parameters, HttpContext context, Control control)
		{
			return parameters.GetValues(context, control).ToDictionary();
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x0001C47C File Offset: 0x0001A67C
		internal static IDictionary<string, object> ToDictionary(this IOrderedDictionary parameterValues)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>(parameterValues.Count, StringComparer.OrdinalIgnoreCase);
			foreach (object obj in parameterValues)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				dictionary[(string)dictionaryEntry.Key] = dictionaryEntry.Value;
			}
			return dictionary;
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x0001C4F4 File Offset: 0x0001A6F4
		public static IOrderedDictionary ToCaseInsensitiveDictionary(this IDictionary dictionary)
		{
			if (dictionary != null)
			{
				IOrderedDictionary orderedDictionary = new OrderedDictionary(dictionary.Count, StringComparer.OrdinalIgnoreCase);
				foreach (object obj in dictionary)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					orderedDictionary[dictionaryEntry.Key] = dictionaryEntry.Value;
				}
				return orderedDictionary;
			}
			return null;
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x0001C56C File Offset: 0x0001A76C
		internal static object CreateObjectInstance(Type type)
		{
			return HttpRuntime.FastCreatePublicInstance(type);
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x0001C574 File Offset: 0x0001A774
		public static bool MergeDictionaries(object dataObjectType, ParameterCollection referenceValues, IDictionary source, IDictionary destination, IDictionary<string, Exception> validationErrors)
		{
			return DataSourceHelper.MergeDictionaries(dataObjectType, referenceValues, source, destination, null, validationErrors);
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x0001C584 File Offset: 0x0001A784
		public static bool MergeDictionaries(object dataObjectType, ParameterCollection reference, IDictionary source, IDictionary destination, IDictionary destinationCopy, IDictionary<string, Exception> validationErrors)
		{
			if (source != null)
			{
				foreach (object obj in source)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					object value = dictionaryEntry.Value;
					Parameter parameter = null;
					string text = (string)dictionaryEntry.Key;
					foreach (object obj2 in reference)
					{
						Parameter parameter2 = (Parameter)obj2;
						if (string.Equals(parameter2.Name, text, StringComparison.OrdinalIgnoreCase))
						{
							parameter = parameter2;
							break;
						}
					}
					if (parameter != null)
					{
						try
						{
							value = parameter.GetValue(value, true);
						}
						catch (Exception value2)
						{
							validationErrors[parameter.Name] = value2;
						}
					}
					destination[text] = value;
					if (destinationCopy != null)
					{
						destinationCopy[text] = value;
					}
				}
			}
			return validationErrors.Count == 0;
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x0001C6A0 File Offset: 0x0001A8A0
		public static Type GetType(string typeName)
		{
			return BuildManager.GetType(typeName, true, true);
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x0001C6AC File Offset: 0x0001A8AC
		private static object ConvertType(object value, Type type, string paramName)
		{
			string text = value as string;
			if (text != null)
			{
				TypeConverter converter = TypeDescriptor.GetConverter(type);
				if (converter != null)
				{
					try
					{
						value = converter.ConvertFromString(text);
					}
					catch (NotSupportedException)
					{
						throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, AtlasWeb.LinqDataSourceView_CannotConvertType, new object[]
						{
							paramName,
							typeof(string).FullName,
							type.FullName
						}));
					}
					catch (FormatException)
					{
						throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, AtlasWeb.LinqDataSourceView_CannotConvertType, new object[]
						{
							paramName,
							typeof(string).FullName,
							type.FullName
						}));
					}
				}
			}
			return value;
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x0001C774 File Offset: 0x0001A974
		public static object BuildDataObject(Type dataObjectType, IDictionary inputParameters, IDictionary<string, Exception> validationErrors)
		{
			object obj = DataSourceHelper.CreateObjectInstance(dataObjectType);
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(obj);
			foreach (object obj2 in inputParameters)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj2;
				string text = (dictionaryEntry.Key == null) ? string.Empty : dictionaryEntry.Key.ToString();
				PropertyDescriptor propertyDescriptor = properties.Find(text, true);
				if (propertyDescriptor != null && !propertyDescriptor.IsReadOnly)
				{
					try
					{
						object value = DataSourceHelper.BuildObjectValue(dictionaryEntry.Value, propertyDescriptor.PropertyType, text);
						propertyDescriptor.SetValue(obj, value);
					}
					catch (Exception value2)
					{
						validationErrors[propertyDescriptor.Name] = value2;
					}
				}
			}
			if (validationErrors.Any<KeyValuePair<string, Exception>>())
			{
				return null;
			}
			return obj;
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x0001C858 File Offset: 0x0001AA58
		internal static object BuildObjectValue(object value, Type destinationType, string paramName)
		{
			if (value != null && !destinationType.IsInstanceOfType(value))
			{
				Type type = destinationType;
				bool flag = false;
				if (destinationType.IsGenericType && destinationType.GetGenericTypeDefinition() == typeof(Nullable<>))
				{
					type = destinationType.GetGenericArguments()[0];
					flag = true;
				}
				else if (destinationType.IsByRef)
				{
					type = destinationType.GetElementType();
				}
				value = DataSourceHelper.ConvertType(value, type, paramName);
				if (flag)
				{
					Type type2 = value.GetType();
					if (type != type2)
					{
						throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, AtlasWeb.LinqDataSourceView_CannotConvertType, new object[]
						{
							paramName,
							type2.FullName,
							string.Format(CultureInfo.InvariantCulture, "Nullable<{0}>", new object[]
							{
								destinationType.GetGenericArguments()[0].FullName
							})
						}));
					}
				}
			}
			return value;
		}
	}
}
