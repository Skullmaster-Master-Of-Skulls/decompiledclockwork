using System;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000564 RID: 1380
	internal class XmlSerializerObjectSerializer : XmlObjectSerializer
	{
		// Token: 0x060035A2 RID: 13730 RVA: 0x000D0D24 File Offset: 0x000CEF24
		internal XmlSerializerObjectSerializer(Type type)
		{
			this.Initialize(type, null, null, null);
		}

		// Token: 0x060035A3 RID: 13731 RVA: 0x000D0D36 File Offset: 0x000CEF36
		internal XmlSerializerObjectSerializer(Type type, XmlQualifiedName qualifiedName, XmlSerializer xmlSerializer)
		{
			if (qualifiedName == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("qualifiedName");
			}
			this.Initialize(type, qualifiedName.Name, qualifiedName.Namespace, xmlSerializer);
		}

		// Token: 0x060035A4 RID: 13732 RVA: 0x000D0D6C File Offset: 0x000CEF6C
		private void Initialize(Type type, string rootName, string rootNamespace, XmlSerializer xmlSerializer)
		{
			if (type == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("type");
			}
			this.rootType = type;
			this.rootName = rootName;
			this.rootNamespace = ((rootNamespace == null) ? string.Empty : rootNamespace);
			this.serializer = xmlSerializer;
			if (this.serializer == null)
			{
				if (this.rootName == null)
				{
					this.serializer = new XmlSerializer(type);
				}
				else
				{
					this.serializer = new XmlSerializer(type, new XmlRootAttribute
					{
						ElementName = this.rootName,
						Namespace = this.rootNamespace
					});
				}
			}
			else
			{
				this.isSerializerSetExplicit = true;
			}
			if (this.rootName == null)
			{
				XmlTypeMapping xmlTypeMapping = new XmlReflectionImporter().ImportTypeMapping(this.rootType);
				this.rootName = xmlTypeMapping.ElementName;
				this.rootNamespace = xmlTypeMapping.Namespace;
			}
		}

		// Token: 0x060035A5 RID: 13733 RVA: 0x000D0E3C File Offset: 0x000CF03C
		public override void WriteObject(XmlDictionaryWriter writer, object graph)
		{
			if (this.isSerializerSetExplicit)
			{
				this.serializer.Serialize(writer, new object[]
				{
					graph
				});
				return;
			}
			this.serializer.Serialize(writer, graph);
		}

		// Token: 0x060035A6 RID: 13734 RVA: 0x000D0E6A File Offset: 0x000CF06A
		public override void WriteStartObject(XmlDictionaryWriter writer, object graph)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException());
		}

		// Token: 0x060035A7 RID: 13735 RVA: 0x000D0E7B File Offset: 0x000CF07B
		public override void WriteObjectContent(XmlDictionaryWriter writer, object graph)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException());
		}

		// Token: 0x060035A8 RID: 13736 RVA: 0x000D0E8C File Offset: 0x000CF08C
		public override void WriteEndObject(XmlDictionaryWriter writer)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException());
		}

		// Token: 0x060035A9 RID: 13737 RVA: 0x000D0EA0 File Offset: 0x000CF0A0
		public override object ReadObject(XmlDictionaryReader reader, bool verifyObjectName)
		{
			if (!this.isSerializerSetExplicit)
			{
				return this.serializer.Deserialize(reader);
			}
			object[] array = (object[])this.serializer.Deserialize(reader);
			if (array != null && array.Length != 0)
			{
				return array[0];
			}
			return null;
		}

		// Token: 0x060035AA RID: 13738 RVA: 0x000D0EE0 File Offset: 0x000CF0E0
		public override bool IsStartObject(XmlDictionaryReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("reader"));
			}
			reader.MoveToElement();
			if (this.rootName != null)
			{
				return reader.IsStartElement(this.rootName, this.rootNamespace);
			}
			return reader.IsStartElement();
		}

		// Token: 0x0400288B RID: 10379
		private XmlSerializer serializer;

		// Token: 0x0400288C RID: 10380
		private Type rootType;

		// Token: 0x0400288D RID: 10381
		private string rootName;

		// Token: 0x0400288E RID: 10382
		private string rootNamespace;

		// Token: 0x0400288F RID: 10383
		private bool isSerializerSetExplicit;
	}
}
