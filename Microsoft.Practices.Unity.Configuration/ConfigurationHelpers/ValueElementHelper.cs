using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Xml;
using Microsoft.Practices.Unity.Configuration.Properties;

namespace Microsoft.Practices.Unity.Configuration.ConfigurationHelpers
{
	// Token: 0x0200001A RID: 26
	public class ValueElementHelper
	{
		// Token: 0x060000C4 RID: 196 RVA: 0x00004570 File Offset: 0x00002770
		public ValueElementHelper(IValueProvidingElement parentElement)
		{
			this.parentElement = parentElement;
			UnknownElementHandlerMap unknownElementHandlerMap = new UnknownElementHandlerMap();
			unknownElementHandlerMap.Add("value", delegate(XmlReader xr)
			{
				this.SetValue<ValueElement>(xr);
			});
			unknownElementHandlerMap.Add("dependency", delegate(XmlReader xr)
			{
				this.SetValue<DependencyElement>(xr);
			});
			unknownElementHandlerMap.Add("array", delegate(XmlReader xr)
			{
				this.SetValue<ArrayElement>(xr);
			});
			unknownElementHandlerMap.Add("optional", delegate(XmlReader xr)
			{
				this.SetValue<OptionalElement>(xr);
			});
			this.unknownElementHandlerMap = unknownElementHandlerMap;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x0000461E File Offset: 0x0000281E
		public static ParameterValueElement GetValue(ParameterValueElement currentValue)
		{
			return currentValue ?? ValueElementHelper.DefaultDependency;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x0000462A File Offset: 0x0000282A
		public bool DeserializeUnrecognizedAttribute(string name, string value)
		{
			this.attributeMap[name] = value;
			return true;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x0000463A File Offset: 0x0000283A
		public bool DeserializeUnknownElement(string elementName, XmlReader reader)
		{
			return this.unknownElementHandlerMap.ProcessElement(elementName, reader) || this.DeserializeExtensionValueElement(elementName, reader);
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00004655 File Offset: 0x00002855
		public void CompleteValueElement(XmlReader reader)
		{
			if (this.ShouldConstructValueElementFromAttributes(reader))
			{
				this.ConstructValueElementFromAttributes();
			}
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00004668 File Offset: 0x00002868
		public static void SerializeParameterValueElement(XmlWriter writer, ParameterValueElement element, bool elementsOnly)
		{
			string tagForElement = ValueElementHelper.GetTagForElement(element);
			if (elementsOnly)
			{
				writer.WriteElement(tagForElement, new Action<XmlWriter>(element.SerializeContent));
				return;
			}
			IAttributeOnlyElement attributeOnlyElement = element as IAttributeOnlyElement;
			if (attributeOnlyElement != null)
			{
				attributeOnlyElement.SerializeContent(writer);
				return;
			}
			writer.WriteElement(tagForElement, new Action<XmlWriter>(element.SerializeContent));
		}

		// Token: 0x060000CA RID: 202 RVA: 0x000046BC File Offset: 0x000028BC
		private static string GetTagForElement(ConfigurationElement element)
		{
			Type type = element.GetType();
			return ValueElementHelper.KnownValueElementTags.GetOrNull(type) ?? ExtensionElementMap.GetTagForExtensionElement(element);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x000046E8 File Offset: 0x000028E8
		private void SetValue<TElement>(XmlReader reader) where TElement : ParameterValueElement, new()
		{
			if (this.parentElement.Value != null)
			{
				throw new ConfigurationErrorsException(string.Format(CultureInfo.CurrentCulture, Resources.DuplicateParameterValueElement, new object[]
				{
					this.parentElement.DestinationName
				}), reader);
			}
			TElement telement = Activator.CreateInstance<TElement>();
			telement.Deserialize(reader);
			this.parentElement.Value = telement;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00004754 File Offset: 0x00002954
		private bool ShouldConstructValueElementFromAttributes(XmlReader reader)
		{
			if (this.parentElement.Value == null)
			{
				return this.attributeMap.Count > 0;
			}
			if (this.attributeMap.Count > 0)
			{
				throw new ConfigurationErrorsException(string.Format(CultureInfo.CurrentCulture, Resources.ElementWithAttributesAndParameterValueElements, new object[]
				{
					this.parentElement.DestinationName
				}), reader);
			}
			return false;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000047B8 File Offset: 0x000029B8
		private void ConstructValueElementFromAttributes()
		{
			if (this.attributeMap.ContainsKey("value"))
			{
				this.parentElement.Value = new ValueElement(this.attributeMap);
				return;
			}
			if (this.attributeMap.ContainsKey("dependencyName") || this.attributeMap.ContainsKey("dependencyType"))
			{
				this.parentElement.Value = new DependencyElement(this.attributeMap);
				return;
			}
			throw new ConfigurationErrorsException(string.Format(CultureInfo.CurrentCulture, Resources.InvalidValueAttributes, new object[]
			{
				this.parentElement.DestinationName
			}));
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00004854 File Offset: 0x00002A54
		private bool DeserializeExtensionValueElement(string elementName, XmlReader reader)
		{
			Type parameterValueElementType = ExtensionElementMap.GetParameterValueElementType(elementName);
			if (parameterValueElementType != null)
			{
				ParameterValueElement parameterValueElement = (ParameterValueElement)Activator.CreateInstance(parameterValueElementType);
				parameterValueElement.Deserialize(reader);
				this.parentElement.Value = parameterValueElement;
			}
			return parameterValueElementType != null;
		}

		// Token: 0x0400002B RID: 43
		private readonly IValueProvidingElement parentElement;

		// Token: 0x0400002C RID: 44
		private static readonly DependencyElement DefaultDependency = new DependencyElement();

		// Token: 0x0400002D RID: 45
		private readonly UnknownElementHandlerMap unknownElementHandlerMap;

		// Token: 0x0400002E RID: 46
		private readonly Dictionary<string, string> attributeMap = new Dictionary<string, string>();

		// Token: 0x0400002F RID: 47
		private static readonly Dictionary<Type, string> KnownValueElementTags = new Dictionary<Type, string>
		{
			{
				typeof(DependencyElement),
				"dependency"
			},
			{
				typeof(ValueElement),
				"value"
			},
			{
				typeof(ArrayElement),
				"array"
			},
			{
				typeof(OptionalElement),
				"optional"
			}
		};
	}
}
