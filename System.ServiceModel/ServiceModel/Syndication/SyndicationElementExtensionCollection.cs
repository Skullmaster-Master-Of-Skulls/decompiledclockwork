using System;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace System.ServiceModel.Syndication
{
	// Token: 0x02000188 RID: 392
	[TypeForwardedFrom("System.ServiceModel.Web, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
	public sealed class SyndicationElementExtensionCollection : Collection<SyndicationElementExtension>
	{
		// Token: 0x06000BB7 RID: 2999 RVA: 0x0002B8C0 File Offset: 0x00029AC0
		internal SyndicationElementExtensionCollection() : this(null)
		{
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x0002B8C9 File Offset: 0x00029AC9
		internal SyndicationElementExtensionCollection(XmlBuffer buffer)
		{
			this.buffer = buffer;
			if (this.buffer != null)
			{
				this.PopulateElements();
			}
			this.initialized = true;
		}

		// Token: 0x06000BB9 RID: 3001 RVA: 0x0002B8F0 File Offset: 0x00029AF0
		internal SyndicationElementExtensionCollection(SyndicationElementExtensionCollection source)
		{
			this.buffer = source.buffer;
			for (int i = 0; i < source.Items.Count; i++)
			{
				base.Add(source.Items[i]);
			}
			this.initialized = true;
		}

		// Token: 0x06000BBA RID: 3002 RVA: 0x0002B93E File Offset: 0x00029B3E
		public void Add(object extension)
		{
			if (extension is SyndicationElementExtension)
			{
				base.Add((SyndicationElementExtension)extension);
				return;
			}
			this.Add(extension, null);
		}

		// Token: 0x06000BBB RID: 3003 RVA: 0x0002B95D File Offset: 0x00029B5D
		public void Add(string outerName, string outerNamespace, object dataContractExtension)
		{
			this.Add(outerName, outerNamespace, dataContractExtension, null);
		}

		// Token: 0x06000BBC RID: 3004 RVA: 0x0002B969 File Offset: 0x00029B69
		public void Add(object dataContractExtension, DataContractSerializer serializer)
		{
			this.Add(null, null, dataContractExtension, serializer);
		}

		// Token: 0x06000BBD RID: 3005 RVA: 0x0002B975 File Offset: 0x00029B75
		public void Add(string outerName, string outerNamespace, object dataContractExtension, XmlObjectSerializer dataContractSerializer)
		{
			if (dataContractExtension == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("dataContractExtension");
			}
			if (dataContractSerializer == null)
			{
				dataContractSerializer = new DataContractSerializer(dataContractExtension.GetType());
			}
			base.Add(new SyndicationElementExtension(outerName, outerNamespace, dataContractExtension, dataContractSerializer));
		}

		// Token: 0x06000BBE RID: 3006 RVA: 0x0002B9AB File Offset: 0x00029BAB
		public void Add(object xmlSerializerExtension, XmlSerializer serializer)
		{
			if (xmlSerializerExtension == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("xmlSerializerExtension");
			}
			if (serializer == null)
			{
				serializer = new XmlSerializer(xmlSerializerExtension.GetType());
			}
			base.Add(new SyndicationElementExtension(xmlSerializerExtension, serializer));
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x0002B9DD File Offset: 0x00029BDD
		public void Add(XmlReader xmlReader)
		{
			if (xmlReader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("xmlReader");
			}
			base.Add(new SyndicationElementExtension(xmlReader));
		}

		// Token: 0x06000BC0 RID: 3008 RVA: 0x0002BA00 File Offset: 0x00029C00
		public XmlReader GetReaderAtElementExtensions()
		{
			XmlBuffer orCreateBufferOverExtensions = this.GetOrCreateBufferOverExtensions();
			XmlReader reader = orCreateBufferOverExtensions.GetReader(0);
			reader.ReadStartElement();
			return reader;
		}

		// Token: 0x06000BC1 RID: 3009 RVA: 0x0002BA23 File Offset: 0x00029C23
		public Collection<TExtension> ReadElementExtensions<TExtension>(string extensionName, string extensionNamespace)
		{
			return this.ReadElementExtensions<TExtension>(extensionName, extensionNamespace, new DataContractSerializer(typeof(TExtension)));
		}

		// Token: 0x06000BC2 RID: 3010 RVA: 0x0002BA3C File Offset: 0x00029C3C
		public Collection<TExtension> ReadElementExtensions<TExtension>(string extensionName, string extensionNamespace, XmlObjectSerializer serializer)
		{
			if (serializer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("serializer");
			}
			return this.ReadExtensions<TExtension>(extensionName, extensionNamespace, serializer, null);
		}

		// Token: 0x06000BC3 RID: 3011 RVA: 0x0002BA5B File Offset: 0x00029C5B
		public Collection<TExtension> ReadElementExtensions<TExtension>(string extensionName, string extensionNamespace, XmlSerializer serializer)
		{
			if (serializer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("serializer");
			}
			return this.ReadExtensions<TExtension>(extensionName, extensionNamespace, null, serializer);
		}

		// Token: 0x06000BC4 RID: 3012 RVA: 0x0002BA7C File Offset: 0x00029C7C
		internal void WriteTo(XmlWriter writer)
		{
			if (this.buffer != null)
			{
				using (XmlDictionaryReader reader = this.buffer.GetReader(0))
				{
					reader.ReadStartElement();
					while (reader.IsStartElement())
					{
						writer.WriteNode(reader, false);
					}
					return;
				}
			}
			for (int i = 0; i < base.Items.Count; i++)
			{
				base.Items[i].WriteTo(writer);
			}
		}

		// Token: 0x06000BC5 RID: 3013 RVA: 0x0002BAFC File Offset: 0x00029CFC
		protected override void ClearItems()
		{
			base.ClearItems();
			if (this.initialized)
			{
				this.buffer = null;
			}
		}

		// Token: 0x06000BC6 RID: 3014 RVA: 0x0002BB13 File Offset: 0x00029D13
		protected override void InsertItem(int index, SyndicationElementExtension item)
		{
			if (item == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("item");
			}
			base.InsertItem(index, item);
			if (this.initialized)
			{
				this.buffer = null;
			}
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x0002BB3F File Offset: 0x00029D3F
		protected override void RemoveItem(int index)
		{
			base.RemoveItem(index);
			if (this.initialized)
			{
				this.buffer = null;
			}
		}

		// Token: 0x06000BC8 RID: 3016 RVA: 0x0002BB57 File Offset: 0x00029D57
		protected override void SetItem(int index, SyndicationElementExtension item)
		{
			if (item == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("item");
			}
			base.SetItem(index, item);
			if (this.initialized)
			{
				this.buffer = null;
			}
		}

		// Token: 0x06000BC9 RID: 3017 RVA: 0x0002BB84 File Offset: 0x00029D84
		private XmlBuffer GetOrCreateBufferOverExtensions()
		{
			if (this.buffer != null)
			{
				return this.buffer;
			}
			XmlBuffer xmlBuffer = new XmlBuffer(int.MaxValue);
			using (XmlWriter xmlWriter = xmlBuffer.OpenSection(XmlDictionaryReaderQuotas.Max))
			{
				xmlWriter.WriteStartElement("extensionWrapper");
				for (int i = 0; i < base.Count; i++)
				{
					base[i].WriteTo(xmlWriter);
				}
				xmlWriter.WriteEndElement();
			}
			xmlBuffer.CloseSection();
			xmlBuffer.Close();
			this.buffer = xmlBuffer;
			return xmlBuffer;
		}

		// Token: 0x06000BCA RID: 3018 RVA: 0x0002BC18 File Offset: 0x00029E18
		private void PopulateElements()
		{
			using (XmlDictionaryReader reader = this.buffer.GetReader(0))
			{
				reader.ReadStartElement();
				int num = 0;
				while (reader.IsStartElement())
				{
					base.Add(new SyndicationElementExtension(this.buffer, num, reader.LocalName, reader.NamespaceURI));
					reader.Skip();
					num++;
				}
			}
		}

		// Token: 0x06000BCB RID: 3019 RVA: 0x0002BC88 File Offset: 0x00029E88
		private Collection<TExtension> ReadExtensions<TExtension>(string extensionName, string extensionNamespace, XmlObjectSerializer dcSerializer, XmlSerializer xmlSerializer)
		{
			if (string.IsNullOrEmpty(extensionName))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("ExtensionNameNotSpecified"));
			}
			if (extensionNamespace == null)
			{
				extensionNamespace = string.Empty;
			}
			Collection<TExtension> collection = new Collection<TExtension>();
			for (int i = 0; i < base.Count; i++)
			{
				if (!(extensionName != base[i].OuterName) && !(extensionNamespace != base[i].OuterNamespace))
				{
					if (dcSerializer != null)
					{
						collection.Add(base[i].GetObject<TExtension>(dcSerializer));
					}
					else
					{
						collection.Add(base[i].GetObject<TExtension>(xmlSerializer));
					}
				}
			}
			return collection;
		}

		// Token: 0x04001696 RID: 5782
		private XmlBuffer buffer;

		// Token: 0x04001697 RID: 5783
		private bool initialized;
	}
}
