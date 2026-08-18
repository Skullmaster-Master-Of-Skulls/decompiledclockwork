using System;
using System.Collections.Generic;
using System.Xml;

namespace System.ServiceModel.Syndication
{
	// Token: 0x0200018F RID: 399
	internal struct ExtensibleSyndicationObject : IExtensibleSyndicationObject
	{
		// Token: 0x06000C71 RID: 3185 RVA: 0x0002CD04 File Offset: 0x0002AF04
		private ExtensibleSyndicationObject(ExtensibleSyndicationObject source)
		{
			if (source.attributeExtensions != null)
			{
				this.attributeExtensions = new Dictionary<XmlQualifiedName, string>();
				using (Dictionary<XmlQualifiedName, string>.KeyCollection.Enumerator enumerator = source.attributeExtensions.Keys.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						XmlQualifiedName key = enumerator.Current;
						this.attributeExtensions.Add(key, source.attributeExtensions[key]);
					}
					goto IL_66;
				}
			}
			this.attributeExtensions = null;
			IL_66:
			if (source.elementExtensions != null)
			{
				this.elementExtensions = new SyndicationElementExtensionCollection(source.elementExtensions);
				return;
			}
			this.elementExtensions = null;
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000C72 RID: 3186 RVA: 0x0002CDA8 File Offset: 0x0002AFA8
		public Dictionary<XmlQualifiedName, string> AttributeExtensions
		{
			get
			{
				if (this.attributeExtensions == null)
				{
					this.attributeExtensions = new Dictionary<XmlQualifiedName, string>();
				}
				return this.attributeExtensions;
			}
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06000C73 RID: 3187 RVA: 0x0002CDC3 File Offset: 0x0002AFC3
		public SyndicationElementExtensionCollection ElementExtensions
		{
			get
			{
				if (this.elementExtensions == null)
				{
					this.elementExtensions = new SyndicationElementExtensionCollection();
				}
				return this.elementExtensions;
			}
		}

		// Token: 0x06000C74 RID: 3188 RVA: 0x0002CDE0 File Offset: 0x0002AFE0
		private static XmlBuffer CreateXmlBuffer(XmlDictionaryReader unparsedExtensionsReader, int maxExtensionSize)
		{
			XmlBuffer xmlBuffer = new XmlBuffer(maxExtensionSize);
			using (XmlDictionaryWriter xmlDictionaryWriter = xmlBuffer.OpenSection(unparsedExtensionsReader.Quotas))
			{
				xmlDictionaryWriter.WriteStartElement("extensionWrapper");
				while (unparsedExtensionsReader.IsStartElement())
				{
					xmlDictionaryWriter.WriteNode(unparsedExtensionsReader, false);
				}
				xmlDictionaryWriter.WriteEndElement();
			}
			xmlBuffer.CloseSection();
			xmlBuffer.Close();
			return xmlBuffer;
		}

		// Token: 0x06000C75 RID: 3189 RVA: 0x0002CE50 File Offset: 0x0002B050
		internal void LoadElementExtensions(XmlReader readerOverUnparsedExtensions, int maxExtensionSize)
		{
			if (readerOverUnparsedExtensions == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("readerOverUnparsedExtensions");
			}
			if (maxExtensionSize < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("maxExtensionSize"));
			}
			XmlDictionaryReader unparsedExtensionsReader = XmlDictionaryReader.CreateDictionaryReader(readerOverUnparsedExtensions);
			this.elementExtensions = new SyndicationElementExtensionCollection(ExtensibleSyndicationObject.CreateXmlBuffer(unparsedExtensionsReader, maxExtensionSize));
		}

		// Token: 0x06000C76 RID: 3190 RVA: 0x0002CEA2 File Offset: 0x0002B0A2
		internal void LoadElementExtensions(XmlBuffer buffer)
		{
			this.elementExtensions = new SyndicationElementExtensionCollection(buffer);
		}

		// Token: 0x06000C77 RID: 3191 RVA: 0x0002CEB0 File Offset: 0x0002B0B0
		internal void WriteAttributeExtensions(XmlWriter writer)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (this.attributeExtensions != null)
			{
				foreach (XmlQualifiedName xmlQualifiedName in this.attributeExtensions.Keys)
				{
					string value = this.attributeExtensions[xmlQualifiedName];
					writer.WriteAttributeString(xmlQualifiedName.Name, xmlQualifiedName.Namespace, value);
				}
			}
		}

		// Token: 0x06000C78 RID: 3192 RVA: 0x0002CF3C File Offset: 0x0002B13C
		internal void WriteElementExtensions(XmlWriter writer)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (this.elementExtensions != null)
			{
				this.elementExtensions.WriteTo(writer);
			}
		}

		// Token: 0x06000C79 RID: 3193 RVA: 0x0002CF65 File Offset: 0x0002B165
		public ExtensibleSyndicationObject Clone()
		{
			return new ExtensibleSyndicationObject(this);
		}

		// Token: 0x040016AB RID: 5803
		private Dictionary<XmlQualifiedName, string> attributeExtensions;

		// Token: 0x040016AC RID: 5804
		private SyndicationElementExtensionCollection elementExtensions;
	}
}
