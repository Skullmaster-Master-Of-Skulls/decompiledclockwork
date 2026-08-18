using System;
using System.Collections.Generic;
using System.Configuration;
using System.Xml;
using Microsoft.Practices.Unity.Configuration.ConfigurationHelpers;
using Microsoft.Practices.Unity.Configuration.Properties;

namespace Microsoft.Practices.Unity.Configuration
{
	// Token: 0x02000027 RID: 39
	[ConfigurationCollection(typeof(InjectionMemberElement))]
	public class InjectionMemberElementCollection : DeserializableConfigurationElementCollectionBase<InjectionMemberElement>
	{
		// Token: 0x1700002C RID: 44
		public InjectionMemberElement this[string key]
		{
			get
			{
				return (InjectionMemberElement)base.BaseGet(key);
			}
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00005375 File Offset: 0x00003575
		protected override bool OnDeserializeUnrecognizedElement(string elementName, XmlReader reader)
		{
			return this.DeserializeRegisteredElement(elementName, reader) || base.OnDeserializeUnrecognizedElement(elementName, reader);
		}

		// Token: 0x0600012E RID: 302 RVA: 0x0000538B File Offset: 0x0000358B
		protected override ConfigurationElement CreateNewElement()
		{
			throw new InvalidOperationException(Resources.CannotCreateInjectionMemberElement);
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00005397 File Offset: 0x00003597
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((InjectionMemberElement)element).Key;
		}

		// Token: 0x06000130 RID: 304 RVA: 0x000053A4 File Offset: 0x000035A4
		private Type GetKnownElementType(string elementName)
		{
			return this.elementTypeMap.GetOrNull(elementName);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x000053B2 File Offset: 0x000035B2
		private static Type GetExtensionElementType(string elementName)
		{
			return ExtensionElementMap.GetInjectionMemberElementType(elementName);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x000053BC File Offset: 0x000035BC
		private bool DeserializeRegisteredElement(string elementName, XmlReader reader)
		{
			Type type = this.GetKnownElementType(elementName) ?? InjectionMemberElementCollection.GetExtensionElementType(elementName);
			if (type == null)
			{
				return false;
			}
			this.ReadElementByType(reader, type, this);
			return true;
		}

		// Token: 0x04000046 RID: 70
		private readonly Dictionary<string, Type> elementTypeMap = new Dictionary<string, Type>
		{
			{
				"constructor",
				typeof(ConstructorElement)
			},
			{
				"property",
				typeof(PropertyElement)
			},
			{
				"method",
				typeof(MethodElement)
			}
		};
	}
}
