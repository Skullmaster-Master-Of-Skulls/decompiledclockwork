using System;
using System.Configuration;
using System.Text;
using System.Xml;

namespace System.IdentityModel.Configuration
{
	// Token: 0x020001C1 RID: 449
	public class ConfigurationElementInterceptor : ConfigurationElement
	{
		// Token: 0x06000E7F RID: 3711 RVA: 0x00041D94 File Offset: 0x0003FF94
		protected override void DeserializeElement(XmlReader reader, bool serializeCollectionKey)
		{
			this.elementXml = new XmlDocument();
			this.elementXml.LoadXml(reader.ReadOuterXml());
			using (XmlReader xmlReader = XmlDictionaryReader.CreateTextReader(Encoding.UTF8.GetBytes(this.elementXml.DocumentElement.OuterXml), XmlDictionaryReaderQuotas.Max))
			{
				xmlReader.Read();
				base.DeserializeElement(xmlReader, serializeCollectionKey);
			}
		}

		// Token: 0x06000E80 RID: 3712 RVA: 0x00002434 File Offset: 0x00000634
		protected override bool OnDeserializeUnrecognizedAttribute(string name, string value)
		{
			return true;
		}

		// Token: 0x06000E81 RID: 3713 RVA: 0x00002434 File Offset: 0x00000634
		protected override bool OnDeserializeUnrecognizedElement(string elementName, XmlReader reader)
		{
			return true;
		}

		// Token: 0x06000E82 RID: 3714 RVA: 0x00041E10 File Offset: 0x00040010
		protected override void Reset(ConfigurationElement parentElement)
		{
			base.Reset(parentElement);
			this.Reset((ConfigurationElementInterceptor)parentElement);
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06000E83 RID: 3715 RVA: 0x00041E25 File Offset: 0x00040025
		public XmlElement ElementAsXml
		{
			get
			{
				if (this.elementXml != null)
				{
					return this.elementXml.DocumentElement;
				}
				return null;
			}
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06000E84 RID: 3716 RVA: 0x00041E3C File Offset: 0x0004003C
		public XmlNodeList ChildNodes
		{
			get
			{
				if (this.elementXml != null && this.ElementAsXml.ChildNodes.Count != 0)
				{
					return this.ElementAsXml.ChildNodes;
				}
				return null;
			}
		}

		// Token: 0x06000E85 RID: 3717 RVA: 0x00041E65 File Offset: 0x00040065
		private void Reset(ConfigurationElementInterceptor parentElement)
		{
			this.elementXml = parentElement.elementXml;
		}

		// Token: 0x04000D13 RID: 3347
		private XmlDocument elementXml;
	}
}
