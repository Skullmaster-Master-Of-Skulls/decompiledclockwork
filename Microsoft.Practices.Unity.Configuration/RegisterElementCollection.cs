using System;
using System.Configuration;
using System.Xml;
using Microsoft.Practices.Unity.Configuration.ConfigurationHelpers;

namespace Microsoft.Practices.Unity.Configuration
{
	// Token: 0x02000037 RID: 55
	[ConfigurationCollection(typeof(RegisterElement), AddItemName = "register")]
	public class RegisterElementCollection : DeserializableConfigurationElementCollection<RegisterElement>
	{
		// Token: 0x060001C4 RID: 452 RVA: 0x00006484 File Offset: 0x00004684
		protected override bool OnDeserializeUnrecognizedElement(string elementName, XmlReader reader)
		{
			return RegisterElementCollection.UnknownElementHandlerMap.ProcessElement(this, elementName, reader) || base.OnDeserializeUnrecognizedElement(elementName, reader);
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x000064A0 File Offset: 0x000046A0
		protected override object GetElementKey(ConfigurationElement element)
		{
			RegisterElement registerElement = (RegisterElement)element;
			return registerElement.TypeName + ":" + registerElement.Name;
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x000064D8 File Offset: 0x000046D8
		// Note: this type is marked as 'beforefieldinit'.
		static RegisterElementCollection()
		{
			UnknownElementHandlerMap<RegisterElementCollection> unknownElementHandlerMap = new UnknownElementHandlerMap<RegisterElementCollection>();
			unknownElementHandlerMap.Add("type", delegate(RegisterElementCollection rec, XmlReader xr)
			{
				rec.ReadUnwrappedElement(xr, rec);
			});
			RegisterElementCollection.UnknownElementHandlerMap = unknownElementHandlerMap;
		}

		// Token: 0x04000065 RID: 101
		private static readonly UnknownElementHandlerMap<RegisterElementCollection> UnknownElementHandlerMap;
	}
}
