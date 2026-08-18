using System;
using System.Collections.Specialized;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.ServiceModel.Channels;
using System.Xml.Serialization;

namespace System.ServiceModel.MsmqIntegration
{
	// Token: 0x020003AF RID: 943
	internal sealed class MsmqIntegrationChannelFactory : MsmqChannelFactoryBase<IOutputChannel>
	{
		// Token: 0x06002353 RID: 9043 RVA: 0x000813B9 File Offset: 0x0007F5B9
		internal MsmqIntegrationChannelFactory(MsmqIntegrationBindingElement bindingElement, BindingContext context) : base(bindingElement, context, null)
		{
			this.serializationFormat = bindingElement.SerializationFormat;
		}

		// Token: 0x170008C6 RID: 2246
		// (get) Token: 0x06002354 RID: 9044 RVA: 0x000813D0 File Offset: 0x0007F5D0
		private BinaryFormatter BinaryFormatter
		{
			get
			{
				if (this.binaryFormatter == null)
				{
					object thisLock = base.ThisLock;
					lock (thisLock)
					{
						if (this.binaryFormatter == null)
						{
							this.binaryFormatter = new BinaryFormatter();
						}
					}
				}
				return this.binaryFormatter;
			}
		}

		// Token: 0x170008C7 RID: 2247
		// (get) Token: 0x06002355 RID: 9045 RVA: 0x0008142C File Offset: 0x0007F62C
		private ActiveXSerializer ActiveXSerializer
		{
			get
			{
				if (this.activeXSerializer == null)
				{
					object thisLock = base.ThisLock;
					lock (thisLock)
					{
						if (this.activeXSerializer == null)
						{
							this.activeXSerializer = new ActiveXSerializer();
						}
					}
				}
				return this.activeXSerializer;
			}
		}

		// Token: 0x06002356 RID: 9046 RVA: 0x00081488 File Offset: 0x0007F688
		private XmlSerializer GetXmlSerializerForType(Type serializedType)
		{
			if (this.xmlSerializerTable == null)
			{
				object thisLock = base.ThisLock;
				lock (thisLock)
				{
					if (this.xmlSerializerTable == null)
					{
						this.xmlSerializerTable = new HybridDictionary();
					}
				}
			}
			XmlSerializer xmlSerializer = (XmlSerializer)this.xmlSerializerTable[serializedType];
			if (xmlSerializer != null)
			{
				return xmlSerializer;
			}
			object thisLock2 = base.ThisLock;
			XmlSerializer result;
			lock (thisLock2)
			{
				if (this.xmlSerializerTable.Count >= 1024)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CommunicationException(SR.GetString("MsmqSerializationTableFull", new object[]
					{
						1024
					})));
				}
				xmlSerializer = (XmlSerializer)this.xmlSerializerTable[serializedType];
				if (xmlSerializer != null)
				{
					result = xmlSerializer;
				}
				else
				{
					xmlSerializer = new XmlSerializer(serializedType);
					this.xmlSerializerTable[serializedType] = xmlSerializer;
					result = xmlSerializer;
				}
			}
			return result;
		}

		// Token: 0x170008C8 RID: 2248
		// (get) Token: 0x06002357 RID: 9047 RVA: 0x00081594 File Offset: 0x0007F794
		public MsmqMessageSerializationFormat SerializationFormat
		{
			get
			{
				base.ThrowIfDisposed();
				return this.serializationFormat;
			}
		}

		// Token: 0x06002358 RID: 9048 RVA: 0x000815A2 File Offset: 0x0007F7A2
		protected override IOutputChannel OnCreateChannel(EndpointAddress to, Uri via)
		{
			base.ValidateScheme(via);
			return new MsmqIntegrationOutputChannel(this, to, via, base.ManualAddressing);
		}

		// Token: 0x06002359 RID: 9049 RVA: 0x000815BC File Offset: 0x0007F7BC
		internal Stream Serialize(MsmqIntegrationMessageProperty property)
		{
			switch (this.SerializationFormat)
			{
			case MsmqMessageSerializationFormat.Xml:
			{
				Stream stream = new MemoryStream();
				XmlSerializer xmlSerializerForType = this.GetXmlSerializerForType(property.Body.GetType());
				xmlSerializerForType.Serialize(stream, property.Body);
				return stream;
			}
			case MsmqMessageSerializationFormat.Binary:
			{
				Stream stream = new MemoryStream();
				this.BinaryFormatter.Serialize(stream, property.Body);
				property.BodyType = new int?(768);
				return stream;
			}
			case MsmqMessageSerializationFormat.ActiveX:
			{
				if (property.BodyType != null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("MsmqCannotUseBodyTypeWithActiveXSerialization")));
				}
				Stream stream = new MemoryStream();
				int value = 0;
				this.ActiveXSerializer.Serialize(stream as MemoryStream, property.Body, ref value);
				property.BodyType = new int?(value);
				return stream;
			}
			case MsmqMessageSerializationFormat.ByteArray:
			{
				byte[] array = property.Body as byte[];
				if (array == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SerializationException(SR.GetString("MsmqByteArrayBodyExpected")));
				}
				Stream stream = new MemoryStream();
				stream.Write(array, 0, array.Length);
				return stream;
			}
			case MsmqMessageSerializationFormat.Stream:
			{
				Stream stream2 = property.Body as Stream;
				if (stream2 == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SerializationException(SR.GetString("MsmqStreamBodyExpected")));
				}
				return stream2;
			}
			default:
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SerializationException(SR.GetString("MsmqUnsupportedSerializationFormat", new object[]
				{
					this.SerializationFormat
				})));
			}
		}

		// Token: 0x04001FE4 RID: 8164
		private ActiveXSerializer activeXSerializer;

		// Token: 0x04001FE5 RID: 8165
		private BinaryFormatter binaryFormatter;

		// Token: 0x04001FE6 RID: 8166
		private MsmqMessageSerializationFormat serializationFormat;

		// Token: 0x04001FE7 RID: 8167
		private HybridDictionary xmlSerializerTable;

		// Token: 0x04001FE8 RID: 8168
		private const int maxSerializerTableSize = 1024;
	}
}
