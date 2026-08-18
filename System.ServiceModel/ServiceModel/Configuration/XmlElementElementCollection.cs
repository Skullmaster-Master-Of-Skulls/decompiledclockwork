using System;
using System.Configuration;
using System.Xml;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020006EA RID: 1770
	[ConfigurationCollection(typeof(XmlElementElement), AddItemName = "xmlElement", CollectionType = ConfigurationElementCollectionType.BasicMap)]
	public sealed class XmlElementElementCollection : ServiceModelConfigurationElementCollection<XmlElementElement>
	{
		// Token: 0x0600440B RID: 17419 RVA: 0x00100ED3 File Offset: 0x000FF0D3
		public XmlElementElementCollection() : base(ConfigurationElementCollectionType.BasicMap, "xmlElement")
		{
		}

		// Token: 0x0600440C RID: 17420 RVA: 0x00100EE1 File Offset: 0x000FF0E1
		protected override object GetElementKey(ConfigurationElement element)
		{
			if (element == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("element");
			}
			return ((XmlElementElement)element).XmlElement.OuterXml;
		}

		// Token: 0x0600440D RID: 17421 RVA: 0x00100F08 File Offset: 0x000FF108
		protected override void Unmerge(ConfigurationElement sourceElement, ConfigurationElement parentElement, ConfigurationSaveMode saveMode)
		{
			if (sourceElement != null)
			{
				XmlElementElementCollection xmlElementElementCollection = (XmlElementElementCollection)sourceElement;
				XmlElementElementCollection xmlElementElementCollection2 = (XmlElementElementCollection)parentElement;
				for (int i = 0; i < xmlElementElementCollection.Count; i++)
				{
					XmlElementElement element = xmlElementElementCollection[i];
					if (xmlElementElementCollection2 == null || !xmlElementElementCollection2.ContainsKey(this.GetElementKey(element)))
					{
						XmlElementElement xmlElementElement = new XmlElementElement();
						xmlElementElement.ResetInternal(element);
						base.Add(xmlElementElement);
					}
				}
			}
		}

		// Token: 0x0600440E RID: 17422 RVA: 0x00100F68 File Offset: 0x000FF168
		protected override bool OnDeserializeUnrecognizedElement(string elementName, XmlReader reader)
		{
			XmlDocument xmlDocument = new XmlDocument();
			base.Add(new XmlElementElement((XmlElement)xmlDocument.ReadNode(reader)));
			return true;
		}
	}
}
