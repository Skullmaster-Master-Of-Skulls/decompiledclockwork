using System;
using System.Configuration;
using System.Xml;
using Microsoft.Practices.Unity.Configuration.ConfigurationHelpers;

namespace Microsoft.Practices.Unity.Configuration
{
	// Token: 0x02000006 RID: 6
	[ConfigurationCollection(typeof(AliasElement))]
	public class AliasElementCollection : DeserializableConfigurationElementCollection<AliasElement>
	{
		// Token: 0x17000004 RID: 4
		public string this[string alias]
		{
			get
			{
				AliasElement aliasElement = (AliasElement)base.BaseGet(alias);
				if (aliasElement != null)
				{
					return aliasElement.TypeName;
				}
				return null;
			}
			set
			{
				if (base.BaseGet(alias) != null)
				{
					base.BaseRemove(alias);
				}
				base.BaseAdd(new AliasElement
				{
					Alias = alias,
					TypeName = value
				}, true);
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002395 File Offset: 0x00000595
		protected override bool OnDeserializeUnrecognizedElement(string elementName, XmlReader reader)
		{
			return AliasElementCollection.UnknownElementHandlerMap.ProcessElement(this, elementName, reader) || base.OnDeserializeUnrecognizedElement(elementName, reader);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000023B0 File Offset: 0x000005B0
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((AliasElement)element).Alias;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000023C8 File Offset: 0x000005C8
		// Note: this type is marked as 'beforefieldinit'.
		static AliasElementCollection()
		{
			UnknownElementHandlerMap<AliasElementCollection> unknownElementHandlerMap = new UnknownElementHandlerMap<AliasElementCollection>();
			unknownElementHandlerMap.Add("typeAlias", delegate(AliasElementCollection aec, XmlReader xr)
			{
				aec.ReadUnwrappedElement(xr, aec);
			});
			AliasElementCollection.UnknownElementHandlerMap = unknownElementHandlerMap;
		}

		// Token: 0x04000003 RID: 3
		private static readonly UnknownElementHandlerMap<AliasElementCollection> UnknownElementHandlerMap;
	}
}
