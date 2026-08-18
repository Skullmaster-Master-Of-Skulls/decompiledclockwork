using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Security;
using System.Security.Permissions;
using System.Xml;

namespace Telerik.Charting.Styles
{
	// Token: 0x020017BD RID: 6077
	internal class StyleSerializer
	{
		// Token: 0x17004790 RID: 18320
		// (get) Token: 0x0600EC74 RID: 60532 RVA: 0x0035E7F6 File Offset: 0x0035C9F6
		// (set) Token: 0x0600EC75 RID: 60533 RVA: 0x0035E811 File Offset: 0x0035CA11
		public XmlDocument XmlDoc
		{
			get
			{
				if (this.xmlDoc == null)
				{
					this.xmlDoc = new XmlDocument();
				}
				return this.xmlDoc;
			}
			set
			{
				this.xmlDoc = value;
			}
		}

		// Token: 0x17004791 RID: 18321
		// (get) Token: 0x0600EC76 RID: 60534 RVA: 0x0035E81A File Offset: 0x0035CA1A
		// (set) Token: 0x0600EC77 RID: 60535 RVA: 0x0035E822 File Offset: 0x0035CA22
		public bool ProcessAllProperties
		{
			get
			{
				return this.processAll;
			}
			set
			{
				this.processAll = value;
			}
		}

		// Token: 0x0600EC78 RID: 60536 RVA: 0x0035E82B File Offset: 0x0035CA2B
		public string SaveXMLString(object styleContainer)
		{
			return this.Serialize(styleContainer).OuterXml;
		}

		// Token: 0x0600EC79 RID: 60537 RVA: 0x0035E839 File Offset: 0x0035CA39
		private XmlElement Serialize(object styleContainer)
		{
			return this.Serialize(styleContainer, string.Empty);
		}

		// Token: 0x0600EC7A RID: 60538 RVA: 0x0035E848 File Offset: 0x0035CA48
		private XmlElement Serialize(object styleContainer, string elementName)
		{
			Type type = styleContainer.GetType();
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(styleContainer);
			XmlElement xmlElement;
			if (string.Empty != elementName)
			{
				xmlElement = this.XmlDoc.CreateElement(elementName);
			}
			else
			{
				xmlElement = this.XmlDoc.CreateElement(type.Name);
			}
			if (styleContainer is ICollection)
			{
				using (IEnumerator enumerator = (styleContainer as IEnumerable).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object styleContainer2 = enumerator.Current;
						XmlElement xmlElement2 = this.Serialize(styleContainer2);
						if (xmlElement2.HasAttributes || xmlElement2.HasChildNodes)
						{
							xmlElement.AppendChild(xmlElement2);
						}
					}
					return xmlElement;
				}
			}
			if (!(styleContainer is BindableLegendItem))
			{
				foreach (object obj in properties)
				{
					PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
					if ((this.processAll || propertyDescriptor.Attributes[typeof(SkinnablePropertyAttribute)] != null) && propertyDescriptor.IsBrowsable)
					{
						this.SerializeProperty(propertyDescriptor, xmlElement, styleContainer);
					}
				}
			}
			return xmlElement;
		}

		// Token: 0x0600EC7B RID: 60539 RVA: 0x0035E988 File Offset: 0x0035CB88
		private void SerializeProperty(PropertyDescriptor propDescriptor, XmlElement propElement, object styleContainer)
		{
			if (this.IsDefaultValue(propDescriptor, styleContainer))
			{
				return;
			}
			if (null != propDescriptor.PropertyType.GetInterface("ICollection"))
			{
				this.SerializeComplexObject(propDescriptor, propElement, styleContainer);
				return;
			}
			if (propDescriptor.Converter.GetType() != typeof(TypeConverter) && propDescriptor.Converter.GetType() != typeof(ExpandableObjectConverter))
			{
				if (propDescriptor.ShouldSerializeValue(styleContainer))
				{
					propElement.SetAttribute(propDescriptor.Name, propDescriptor.Converter.ConvertToInvariantString(propDescriptor.GetValue(styleContainer)));
					return;
				}
			}
			else
			{
				this.SerializeComplexObject(propDescriptor, propElement, styleContainer);
			}
		}

		// Token: 0x0600EC7C RID: 60540 RVA: 0x0035EA2C File Offset: 0x0035CC2C
		private void SerializeComplexObject(PropertyDescriptor propDescriptor, XmlElement propElement, object styleContainer)
		{
			object value = propDescriptor.GetValue(styleContainer);
			if (value != null)
			{
				XmlElement xmlElement = this.Serialize(value, propDescriptor.Name);
				if (xmlElement.HasAttributes || xmlElement.HasChildNodes)
				{
					propElement.AppendChild(xmlElement);
				}
			}
		}

		// Token: 0x0600EC7D RID: 60541 RVA: 0x0035EA6A File Offset: 0x0035CC6A
		public void LoadXMLString(string xmlString, object styleContainer)
		{
			this.XmlDoc.LoadXml(xmlString);
			this.Deserialize(this.XmlDoc.DocumentElement, styleContainer);
		}

		// Token: 0x0600EC7E RID: 60542 RVA: 0x0035EA8C File Offset: 0x0035CC8C
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		private void Deserialize(XmlElement rootElement, object styleContainer)
		{
			styleContainer.GetType();
			if (styleContainer is ICollection)
			{
				int num = 0;
				if (styleContainer is ColorBlend)
				{
					this.DeserializeColorBlend(rootElement, styleContainer as ColorBlend, num);
					return;
				}
				if (this.processAll && styleContainer is IDeserializableCollection)
				{
					(styleContainer as IDeserializableCollection).PopulateFromXml(rootElement);
				}
				using (IEnumerator enumerator = (styleContainer as IEnumerable).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object styleContainer2 = enumerator.Current;
						XmlElement xmlElement = rootElement.ChildNodes[num] as XmlElement;
						if (xmlElement != null)
						{
							this.Deserialize(xmlElement, styleContainer2);
						}
						num++;
					}
					return;
				}
			}
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(styleContainer);
			foreach (object obj in properties)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
				if (this.processAll || propertyDescriptor.Attributes[typeof(SkinnablePropertyAttribute)] != null)
				{
					this.DeserializeProperty(propertyDescriptor, rootElement, styleContainer);
				}
			}
		}

		// Token: 0x0600EC7F RID: 60543 RVA: 0x0035EBC0 File Offset: 0x0035CDC0
		private void DeserializeProperty(PropertyDescriptor propDescriptor, XmlElement propElement, object styleContainer)
		{
			if (propElement.Attributes[propDescriptor.Name] != null)
			{
				if (propDescriptor.Converter.CanConvertFrom(typeof(string)) && this.IsDefaultValue(propDescriptor, styleContainer))
				{
					propDescriptor.SetValue(styleContainer, propDescriptor.Converter.ConvertFromInvariantString(propElement.GetAttribute(propDescriptor.Name)));
					return;
				}
			}
			else if (propElement.HasChildNodes)
			{
				foreach (object obj in propElement.ChildNodes)
				{
					XmlElement xmlElement = (XmlElement)obj;
					if (propDescriptor.Name == xmlElement.Name)
					{
						this.Deserialize(xmlElement, this.GetPropertyValue(propDescriptor, styleContainer));
					}
				}
			}
		}

		// Token: 0x0600EC80 RID: 60544 RVA: 0x0035EC94 File Offset: 0x0035CE94
		private void DeserializeColorBlend(XmlElement rootElement, ColorBlend colorBlend, int index)
		{
			if (colorBlend != null)
			{
				foreach (object obj in rootElement.ChildNodes)
				{
					XmlElement rootElement2 = (XmlElement)obj;
					GradientElement gradientElement = null;
					if (index < colorBlend.Count)
					{
						gradientElement = colorBlend[index];
					}
					else if (index < rootElement.ChildNodes.Count)
					{
						gradientElement = new GradientElement();
						colorBlend.Add(gradientElement);
					}
					if (gradientElement != null)
					{
						this.Deserialize(rootElement2, gradientElement);
					}
					index++;
				}
			}
		}

		// Token: 0x0600EC81 RID: 60545 RVA: 0x0035ED2C File Offset: 0x0035CF2C
		private bool IsDefaultValue(PropertyDescriptor propDescriptor, object styleContainer)
		{
			if (!StyleSerializer.HasReflectionPermission())
			{
				return true;
			}
			object defaultPropertyValue = this.GetDefaultPropertyValue(propDescriptor);
			return defaultPropertyValue != null && defaultPropertyValue.Equals(this.GetPropertyValue(propDescriptor, styleContainer));
		}

		// Token: 0x0600EC82 RID: 60546 RVA: 0x0035ED60 File Offset: 0x0035CF60
		private object GetDefaultPropertyValue(PropertyDescriptor propDescriptor)
		{
			DefaultValueAttribute defaultValueAttribute = propDescriptor.Attributes[typeof(DefaultValueAttribute)] as DefaultValueAttribute;
			if (defaultValueAttribute != null)
			{
				return defaultValueAttribute.Value;
			}
			return null;
		}

		// Token: 0x0600EC83 RID: 60547 RVA: 0x0035ED93 File Offset: 0x0035CF93
		private object GetPropertyValue(PropertyDescriptor propDescriptor, object styleContainer)
		{
			return propDescriptor.GetValue(styleContainer);
		}

		// Token: 0x0600EC84 RID: 60548 RVA: 0x0035ED9C File Offset: 0x0035CF9C
		private static bool HasReflectionPermission()
		{
			bool result;
			try
			{
				ReflectionPermission reflectionPermission = new ReflectionPermission(ReflectionPermissionFlag.MemberAccess);
				reflectionPermission.Demand();
				result = true;
			}
			catch (SecurityException)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x04004430 RID: 17456
		private XmlDocument xmlDoc;

		// Token: 0x04004431 RID: 17457
		private bool processAll;
	}
}
