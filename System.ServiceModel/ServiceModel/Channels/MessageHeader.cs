using System;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x020009CC RID: 2508
	[__DynamicallyInvokable]
	public abstract class MessageHeader : MessageHeaderInfo
	{
		// Token: 0x170017CA RID: 6090
		// (get) Token: 0x06006297 RID: 25239 RVA: 0x0016F2ED File Offset: 0x0016D4ED
		[__DynamicallyInvokable]
		public override string Actor
		{
			[__DynamicallyInvokable]
			get
			{
				return "";
			}
		}

		// Token: 0x170017CB RID: 6091
		// (get) Token: 0x06006298 RID: 25240 RVA: 0x0016F2F4 File Offset: 0x0016D4F4
		[__DynamicallyInvokable]
		public override bool IsReferenceParameter
		{
			[__DynamicallyInvokable]
			get
			{
				return false;
			}
		}

		// Token: 0x170017CC RID: 6092
		// (get) Token: 0x06006299 RID: 25241 RVA: 0x0016F2F7 File Offset: 0x0016D4F7
		[__DynamicallyInvokable]
		public override bool MustUnderstand
		{
			[__DynamicallyInvokable]
			get
			{
				return false;
			}
		}

		// Token: 0x170017CD RID: 6093
		// (get) Token: 0x0600629A RID: 25242 RVA: 0x0016F2FA File Offset: 0x0016D4FA
		[__DynamicallyInvokable]
		public override bool Relay
		{
			[__DynamicallyInvokable]
			get
			{
				return false;
			}
		}

		// Token: 0x0600629B RID: 25243 RVA: 0x0016F2FD File Offset: 0x0016D4FD
		[__DynamicallyInvokable]
		public virtual bool IsMessageVersionSupported(MessageVersion messageVersion)
		{
			if (messageVersion == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("messageVersion");
			}
			return true;
		}

		// Token: 0x0600629C RID: 25244 RVA: 0x0016F314 File Offset: 0x0016D514
		[__DynamicallyInvokable]
		public override string ToString()
		{
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			XmlDictionaryWriter xmlDictionaryWriter = XmlDictionaryWriter.CreateDictionaryWriter(new XmlTextWriter(stringWriter)
			{
				Formatting = Formatting.Indented
			});
			if (this.IsMessageVersionSupported(MessageVersion.Soap12WSAddressing10))
			{
				this.WriteHeader(xmlDictionaryWriter, MessageVersion.Soap12WSAddressing10);
			}
			else if (this.IsMessageVersionSupported(MessageVersion.Soap12WSAddressingAugust2004))
			{
				this.WriteHeader(xmlDictionaryWriter, MessageVersion.Soap12WSAddressingAugust2004);
			}
			else if (this.IsMessageVersionSupported(MessageVersion.Soap11WSAddressing10))
			{
				this.WriteHeader(xmlDictionaryWriter, MessageVersion.Soap11WSAddressing10);
			}
			else if (this.IsMessageVersionSupported(MessageVersion.Soap11WSAddressingAugust2004))
			{
				this.WriteHeader(xmlDictionaryWriter, MessageVersion.Soap11WSAddressingAugust2004);
			}
			else if (this.IsMessageVersionSupported(MessageVersion.Soap12))
			{
				this.WriteHeader(xmlDictionaryWriter, MessageVersion.Soap12);
			}
			else if (this.IsMessageVersionSupported(MessageVersion.Soap11))
			{
				this.WriteHeader(xmlDictionaryWriter, MessageVersion.Soap11);
			}
			else
			{
				this.WriteHeader(xmlDictionaryWriter, MessageVersion.None);
			}
			xmlDictionaryWriter.Flush();
			return stringWriter.ToString();
		}

		// Token: 0x0600629D RID: 25245 RVA: 0x0016F3FE File Offset: 0x0016D5FE
		[__DynamicallyInvokable]
		public void WriteHeader(XmlWriter writer, MessageVersion messageVersion)
		{
			this.WriteHeader(XmlDictionaryWriter.CreateDictionaryWriter(writer), messageVersion);
		}

		// Token: 0x0600629E RID: 25246 RVA: 0x0016F410 File Offset: 0x0016D610
		[__DynamicallyInvokable]
		public void WriteHeader(XmlDictionaryWriter writer, MessageVersion messageVersion)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("writer"));
			}
			if (messageVersion == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("messageVersion"));
			}
			this.OnWriteStartHeader(writer, messageVersion);
			this.OnWriteHeaderContents(writer, messageVersion);
			writer.WriteEndElement();
		}

		// Token: 0x0600629F RID: 25247 RVA: 0x0016F463 File Offset: 0x0016D663
		[__DynamicallyInvokable]
		public void WriteStartHeader(XmlDictionaryWriter writer, MessageVersion messageVersion)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("writer"));
			}
			if (messageVersion == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("messageVersion"));
			}
			this.OnWriteStartHeader(writer, messageVersion);
		}

		// Token: 0x060062A0 RID: 25248 RVA: 0x0016F49D File Offset: 0x0016D69D
		[__DynamicallyInvokable]
		protected virtual void OnWriteStartHeader(XmlDictionaryWriter writer, MessageVersion messageVersion)
		{
			writer.WriteStartElement(this.Name, this.Namespace);
			this.WriteHeaderAttributes(writer, messageVersion);
		}

		// Token: 0x060062A1 RID: 25249 RVA: 0x0016F4B9 File Offset: 0x0016D6B9
		[__DynamicallyInvokable]
		public void WriteHeaderContents(XmlDictionaryWriter writer, MessageVersion messageVersion)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("writer"));
			}
			if (messageVersion == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("messageVersion"));
			}
			this.OnWriteHeaderContents(writer, messageVersion);
		}

		// Token: 0x060062A2 RID: 25250
		[__DynamicallyInvokable]
		protected abstract void OnWriteHeaderContents(XmlDictionaryWriter writer, MessageVersion messageVersion);

		// Token: 0x060062A3 RID: 25251 RVA: 0x0016F4F4 File Offset: 0x0016D6F4
		[__DynamicallyInvokable]
		protected void WriteHeaderAttributes(XmlDictionaryWriter writer, MessageVersion messageVersion)
		{
			string actor = this.Actor;
			if (actor.Length > 0)
			{
				writer.WriteAttributeString(messageVersion.Envelope.DictionaryActor, messageVersion.Envelope.DictionaryNamespace, actor);
			}
			if (this.MustUnderstand)
			{
				writer.WriteAttributeString(XD.MessageDictionary.MustUnderstand, messageVersion.Envelope.DictionaryNamespace, "1");
			}
			if (this.Relay && messageVersion.Envelope == EnvelopeVersion.Soap12)
			{
				writer.WriteAttributeString(XD.Message12Dictionary.Relay, XD.Message12Dictionary.Namespace, "1");
			}
		}

		// Token: 0x060062A4 RID: 25252 RVA: 0x0016F58A File Offset: 0x0016D78A
		[__DynamicallyInvokable]
		public static MessageHeader CreateHeader(string name, string ns, object value)
		{
			return MessageHeader.CreateHeader(name, ns, value, false, "", false);
		}

		// Token: 0x060062A5 RID: 25253 RVA: 0x0016F59B File Offset: 0x0016D79B
		[__DynamicallyInvokable]
		public static MessageHeader CreateHeader(string name, string ns, object value, bool mustUnderstand)
		{
			return MessageHeader.CreateHeader(name, ns, value, mustUnderstand, "", false);
		}

		// Token: 0x060062A6 RID: 25254 RVA: 0x0016F5AC File Offset: 0x0016D7AC
		[__DynamicallyInvokable]
		public static MessageHeader CreateHeader(string name, string ns, object value, bool mustUnderstand, string actor)
		{
			return MessageHeader.CreateHeader(name, ns, value, mustUnderstand, actor, false);
		}

		// Token: 0x060062A7 RID: 25255 RVA: 0x0016F5BA File Offset: 0x0016D7BA
		[__DynamicallyInvokable]
		public static MessageHeader CreateHeader(string name, string ns, object value, bool mustUnderstand, string actor, bool relay)
		{
			return new XmlObjectSerializerHeader(name, ns, value, null, mustUnderstand, actor, relay);
		}

		// Token: 0x060062A8 RID: 25256 RVA: 0x0016F5CA File Offset: 0x0016D7CA
		[__DynamicallyInvokable]
		public static MessageHeader CreateHeader(string name, string ns, object value, XmlObjectSerializer serializer)
		{
			return MessageHeader.CreateHeader(name, ns, value, serializer, false, "", false);
		}

		// Token: 0x060062A9 RID: 25257 RVA: 0x0016F5DC File Offset: 0x0016D7DC
		[__DynamicallyInvokable]
		public static MessageHeader CreateHeader(string name, string ns, object value, XmlObjectSerializer serializer, bool mustUnderstand)
		{
			return MessageHeader.CreateHeader(name, ns, value, serializer, mustUnderstand, "", false);
		}

		// Token: 0x060062AA RID: 25258 RVA: 0x0016F5EF File Offset: 0x0016D7EF
		[__DynamicallyInvokable]
		public static MessageHeader CreateHeader(string name, string ns, object value, XmlObjectSerializer serializer, bool mustUnderstand, string actor)
		{
			return MessageHeader.CreateHeader(name, ns, value, serializer, mustUnderstand, actor, false);
		}

		// Token: 0x060062AB RID: 25259 RVA: 0x0016F5FF File Offset: 0x0016D7FF
		[__DynamicallyInvokable]
		public static MessageHeader CreateHeader(string name, string ns, object value, XmlObjectSerializer serializer, bool mustUnderstand, string actor, bool relay)
		{
			if (serializer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("serializer"));
			}
			return new XmlObjectSerializerHeader(name, ns, value, serializer, mustUnderstand, actor, relay);
		}

		// Token: 0x060062AC RID: 25260 RVA: 0x0016F628 File Offset: 0x0016D828
		internal static void GetHeaderAttributes(XmlDictionaryReader reader, MessageVersion version, out string actor, out bool mustUnderstand, out bool relay, out bool isReferenceParameter)
		{
			int attributeCount = reader.AttributeCount;
			if (attributeCount == 0)
			{
				mustUnderstand = false;
				actor = string.Empty;
				relay = false;
				isReferenceParameter = false;
				return;
			}
			string attribute = reader.GetAttribute(XD.MessageDictionary.MustUnderstand, version.Envelope.DictionaryNamespace);
			if (attribute != null && MessageHeader.ToBoolean(attribute))
			{
				mustUnderstand = true;
			}
			else
			{
				mustUnderstand = false;
			}
			if (mustUnderstand && attributeCount == 1)
			{
				actor = string.Empty;
				relay = false;
			}
			else
			{
				actor = reader.GetAttribute(version.Envelope.DictionaryActor, version.Envelope.DictionaryNamespace);
				if (actor == null)
				{
					actor = "";
				}
				if (version.Envelope == EnvelopeVersion.Soap12)
				{
					string attribute2 = reader.GetAttribute(XD.Message12Dictionary.Relay, version.Envelope.DictionaryNamespace);
					if (attribute2 != null && MessageHeader.ToBoolean(attribute2))
					{
						relay = true;
					}
					else
					{
						relay = false;
					}
				}
				else
				{
					relay = false;
				}
			}
			isReferenceParameter = false;
			if (version.Addressing == AddressingVersion.WSAddressing10)
			{
				string attribute3 = reader.GetAttribute(XD.AddressingDictionary.IsReferenceParameter, version.Addressing.DictionaryNamespace);
				if (attribute3 != null)
				{
					isReferenceParameter = MessageHeader.ToBoolean(attribute3);
				}
			}
		}

		// Token: 0x060062AD RID: 25261 RVA: 0x0016F73C File Offset: 0x0016D93C
		private static bool ToBoolean(string value)
		{
			if (value.Length == 1)
			{
				char c = value[0];
				if (c == '1')
				{
					return true;
				}
				if (c == '0')
				{
					return false;
				}
			}
			else
			{
				if (value == "true")
				{
					return true;
				}
				if (value == "false")
				{
					return false;
				}
			}
			bool result;
			try
			{
				result = XmlConvert.ToBoolean(value);
			}
			catch (FormatException ex)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(ex.Message, null));
			}
			return result;
		}

		// Token: 0x060062AE RID: 25262 RVA: 0x0016F7BC File Offset: 0x0016D9BC
		[__DynamicallyInvokable]
		protected MessageHeader()
		{
		}

		// Token: 0x04003927 RID: 14631
		private const bool DefaultRelayValue = false;

		// Token: 0x04003928 RID: 14632
		private const bool DefaultMustUnderstandValue = false;

		// Token: 0x04003929 RID: 14633
		private const string DefaultActorValue = "";
	}
}
