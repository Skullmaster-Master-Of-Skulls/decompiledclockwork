using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;
using System.Xml;
using System.Xml.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02001AFA RID: 6906
	internal static class XmlPersister
	{
		// Token: 0x06010B48 RID: 68424 RVA: 0x003B83DC File Offset: 0x003B65DC
		public static void SerializePropertiesAsAttributes(object target, XmlWriter writer)
		{
			XmlPersister.SerializePropertiesAsAttributes(target, writer, new string[0]);
		}

		// Token: 0x06010B49 RID: 68425 RVA: 0x003B83EC File Offset: 0x003B65EC
		public static void SerializePropertiesAsAttributes(object target, XmlWriter writer, string[] propertiesToIgnore)
		{
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(target);
			foreach (object obj in properties)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
				if (XmlPersister.IsValidProperty(propertyDescriptor) && Array.IndexOf<string>(propertiesToIgnore, propertyDescriptor.Name) <= -1)
				{
					string text = propertyDescriptor.Converter.ConvertToString(propertyDescriptor.GetValue(target));
					string defaultValueAsString = XmlPersister.GetDefaultValueAsString(propertyDescriptor);
					if (!(defaultValueAsString == text))
					{
						writer.WriteAttributeString(propertyDescriptor.Name, text);
					}
				}
			}
		}

		// Token: 0x06010B4A RID: 68426 RVA: 0x003B8490 File Offset: 0x003B6690
		private static string GetDefaultValueAsString(PropertyDescriptor property)
		{
			return property.Converter.ConvertToString(XmlPersister.GetDefaultValue(property));
		}

		// Token: 0x06010B4B RID: 68427 RVA: 0x003B84A4 File Offset: 0x003B66A4
		private static object GetDefaultValue(PropertyDescriptor property)
		{
			DefaultValueAttribute defaultValueAttribute = (DefaultValueAttribute)property.Attributes[typeof(DefaultValueAttribute)];
			return defaultValueAttribute.Value;
		}

		// Token: 0x06010B4C RID: 68428 RVA: 0x003B84D4 File Offset: 0x003B66D4
		public static void SerializeAttributeCollectionAsAttributes(System.Web.UI.AttributeCollection attributes, XmlWriter writer)
		{
			foreach (object obj in attributes.Keys)
			{
				string text = (string)obj;
				writer.WriteAttributeString(text, attributes[text]);
			}
		}

		// Token: 0x06010B4D RID: 68429 RVA: 0x003B8534 File Offset: 0x003B6734
		private static bool IsValidProperty(PropertyDescriptor property)
		{
			return property != null && property.IsBrowsable && !property.IsReadOnly && property.Converter.CanConvertFrom(typeof(string)) && property.Attributes[typeof(DefaultValueAttribute)] != null && property.Attributes[typeof(XmlIgnoreAttribute)] == null;
		}

		// Token: 0x06010B4E RID: 68430 RVA: 0x003B85A8 File Offset: 0x003B67A8
		public static void MergeObjects(object source, object destination)
		{
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(source);
			foreach (object obj in properties)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
				if (XmlPersister.IsValidProperty(propertyDescriptor))
				{
					object defaultValue = XmlPersister.GetDefaultValue(propertyDescriptor);
					object value = propertyDescriptor.GetValue(source);
					if (!defaultValue.Equals(value))
					{
						propertyDescriptor.SetValue(destination, value);
					}
				}
			}
		}

		// Token: 0x06010B4F RID: 68431 RVA: 0x003B862C File Offset: 0x003B682C
		public static void Deserialize(object target, System.Web.UI.AttributeCollection attributes, IDictionary<string, string> mappings, XmlReader reader, bool skipDefaultValues)
		{
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(target);
			while (reader.MoveToNextAttribute())
			{
				string text = reader.Name;
				string value = reader.Value;
				if (mappings != null && mappings.ContainsKey(text))
				{
					text = mappings[text];
				}
				PropertyDescriptor propertyDescriptor = properties.Find(text, true);
				if (propertyDescriptor == null && attributes != null)
				{
					attributes.Add(text, value);
				}
				else if (XmlPersister.IsValidProperty(propertyDescriptor))
				{
					string defaultValueAsString = XmlPersister.GetDefaultValueAsString(propertyDescriptor);
					if (!(defaultValueAsString == value) || !skipDefaultValues)
					{
						propertyDescriptor.SetValue(target, propertyDescriptor.Converter.ConvertFromString(value));
					}
				}
			}
		}

		// Token: 0x06010B50 RID: 68432 RVA: 0x003B86B6 File Offset: 0x003B68B6
		public static void Deserialize(object target, System.Web.UI.AttributeCollection attributes, IDictionary<string, string> mappings, XmlReader reader)
		{
			XmlPersister.Deserialize(target, attributes, mappings, reader, true);
		}
	}
}
