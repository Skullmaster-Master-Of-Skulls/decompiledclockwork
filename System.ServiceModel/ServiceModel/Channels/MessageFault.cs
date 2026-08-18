using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Dispatcher;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x020009C9 RID: 2505
	[__DynamicallyInvokable]
	public abstract class MessageFault
	{
		// Token: 0x0600625F RID: 25183 RVA: 0x0016E2A8 File Offset: 0x0016C4A8
		public static MessageFault CreateFault(FaultCode code, string reason)
		{
			return MessageFault.CreateFault(code, new FaultReason(reason));
		}

		// Token: 0x06006260 RID: 25184 RVA: 0x0016E2B6 File Offset: 0x0016C4B6
		public static MessageFault CreateFault(FaultCode code, FaultReason reason)
		{
			return MessageFault.CreateFault(code, reason, null, null, "", "");
		}

		// Token: 0x06006261 RID: 25185 RVA: 0x0016E2CB File Offset: 0x0016C4CB
		public static MessageFault CreateFault(FaultCode code, FaultReason reason, object detail)
		{
			return MessageFault.CreateFault(code, reason, detail, DataContractSerializerDefaults.CreateSerializer((detail == null) ? typeof(object) : detail.GetType(), int.MaxValue), "", "");
		}

		// Token: 0x06006262 RID: 25186 RVA: 0x0016E2FE File Offset: 0x0016C4FE
		public static MessageFault CreateFault(FaultCode code, FaultReason reason, object detail, XmlObjectSerializer serializer)
		{
			return MessageFault.CreateFault(code, reason, detail, serializer, "", "");
		}

		// Token: 0x06006263 RID: 25187 RVA: 0x0016E313 File Offset: 0x0016C513
		public static MessageFault CreateFault(FaultCode code, FaultReason reason, object detail, XmlObjectSerializer serializer, string actor)
		{
			if (serializer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("serializer"));
			}
			return MessageFault.CreateFault(code, reason, detail, serializer, actor, actor);
		}

		// Token: 0x06006264 RID: 25188 RVA: 0x0016E33C File Offset: 0x0016C53C
		public static MessageFault CreateFault(FaultCode code, FaultReason reason, object detail, XmlObjectSerializer serializer, string actor, string node)
		{
			if (code == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("code"));
			}
			if (reason == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("reason"));
			}
			if (actor == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("actor"));
			}
			if (node == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("node"));
			}
			return new XmlObjectSerializerFault(code, reason, detail, serializer, actor, node);
		}

		// Token: 0x06006265 RID: 25189 RVA: 0x0016E3B8 File Offset: 0x0016C5B8
		[__DynamicallyInvokable]
		public static MessageFault CreateFault(Message message, int maxBufferSize)
		{
			if (message == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("message"));
			}
			XmlDictionaryReader readerAtBodyContents = message.GetReaderAtBodyContents();
			MessageFault result;
			using (readerAtBodyContents)
			{
				try
				{
					EnvelopeVersion envelope = message.Version.Envelope;
					MessageFault messageFault;
					if (envelope == EnvelopeVersion.Soap12)
					{
						messageFault = ReceivedFault.CreateFault12(readerAtBodyContents, maxBufferSize);
					}
					else if (envelope == EnvelopeVersion.Soap11)
					{
						messageFault = ReceivedFault.CreateFault11(readerAtBodyContents, maxBufferSize);
					}
					else
					{
						if (envelope != EnvelopeVersion.None)
						{
							throw TraceUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("EnvelopeVersionUnknown", new object[]
							{
								envelope.ToString()
							})), message);
						}
						messageFault = ReceivedFault.CreateFaultNone(readerAtBodyContents, maxBufferSize);
					}
					message.ReadFromBodyContentsToEnd(readerAtBodyContents);
					result = messageFault;
				}
				catch (InvalidOperationException innerException)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CommunicationException(SR.GetString("SFxErrorDeserializingFault"), innerException));
				}
				catch (FormatException innerException2)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CommunicationException(SR.GetString("SFxErrorDeserializingFault"), innerException2));
				}
				catch (XmlException innerException3)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CommunicationException(SR.GetString("SFxErrorDeserializingFault"), innerException3));
				}
			}
			return result;
		}

		// Token: 0x170017B8 RID: 6072
		// (get) Token: 0x06006266 RID: 25190 RVA: 0x0016E4F4 File Offset: 0x0016C6F4
		internal static MessageFault Default
		{
			get
			{
				if (MessageFault.defaultMessageFault == null)
				{
					MessageFault.defaultMessageFault = MessageFault.CreateFault(new FaultCode("Default"), new FaultReason("", CultureInfo.CurrentCulture));
				}
				return MessageFault.defaultMessageFault;
			}
		}

		// Token: 0x170017B9 RID: 6073
		// (get) Token: 0x06006267 RID: 25191 RVA: 0x0016E525 File Offset: 0x0016C725
		[__DynamicallyInvokable]
		public virtual string Actor
		{
			[__DynamicallyInvokable]
			get
			{
				return "";
			}
		}

		// Token: 0x170017BA RID: 6074
		// (get) Token: 0x06006268 RID: 25192
		[__DynamicallyInvokable]
		public abstract FaultCode Code { [__DynamicallyInvokable] get; }

		// Token: 0x170017BB RID: 6075
		// (get) Token: 0x06006269 RID: 25193 RVA: 0x0016E52C File Offset: 0x0016C72C
		public bool IsMustUnderstandFault
		{
			get
			{
				FaultCode code = this.Code;
				return string.Compare(code.Name, "MustUnderstand", StringComparison.Ordinal) == 0 && (string.Compare(code.Namespace, EnvelopeVersion.Soap11.Namespace, StringComparison.Ordinal) == 0 || string.Compare(code.Namespace, EnvelopeVersion.Soap12.Namespace, StringComparison.Ordinal) == 0);
			}
		}

		// Token: 0x170017BC RID: 6076
		// (get) Token: 0x0600626A RID: 25194 RVA: 0x0016E588 File Offset: 0x0016C788
		[__DynamicallyInvokable]
		public virtual string Node
		{
			[__DynamicallyInvokable]
			get
			{
				return "";
			}
		}

		// Token: 0x170017BD RID: 6077
		// (get) Token: 0x0600626B RID: 25195
		[__DynamicallyInvokable]
		public abstract bool HasDetail { [__DynamicallyInvokable] get; }

		// Token: 0x170017BE RID: 6078
		// (get) Token: 0x0600626C RID: 25196
		[__DynamicallyInvokable]
		public abstract FaultReason Reason { [__DynamicallyInvokable] get; }

		// Token: 0x0600626D RID: 25197 RVA: 0x0016E58F File Offset: 0x0016C78F
		[__DynamicallyInvokable]
		public T GetDetail<T>()
		{
			return this.GetDetail<T>(DataContractSerializerDefaults.CreateSerializer(typeof(T), int.MaxValue));
		}

		// Token: 0x0600626E RID: 25198 RVA: 0x0016E5AC File Offset: 0x0016C7AC
		[__DynamicallyInvokable]
		public T GetDetail<T>(XmlObjectSerializer serializer)
		{
			if (serializer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("serializer"));
			}
			XmlDictionaryReader readerAtDetailContents = this.GetReaderAtDetailContents();
			T result = (T)((object)serializer.ReadObject(readerAtDetailContents));
			if (!readerAtDetailContents.EOF)
			{
				readerAtDetailContents.MoveToContent();
				if (readerAtDetailContents.NodeType != XmlNodeType.EndElement && !readerAtDetailContents.EOF)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new FormatException(SR.GetString("ExtraContentIsPresentInFaultDetail")));
				}
			}
			return result;
		}

		// Token: 0x0600626F RID: 25199 RVA: 0x0016E621 File Offset: 0x0016C821
		[__DynamicallyInvokable]
		public XmlDictionaryReader GetReaderAtDetailContents()
		{
			if (!this.HasDetail)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("FaultDoesNotHaveAnyDetail")));
			}
			return this.OnGetReaderAtDetailContents();
		}

		// Token: 0x06006270 RID: 25200 RVA: 0x0016E64B File Offset: 0x0016C84B
		[__DynamicallyInvokable]
		protected virtual void OnWriteDetail(XmlDictionaryWriter writer, EnvelopeVersion version)
		{
			this.OnWriteStartDetail(writer, version);
			this.OnWriteDetailContents(writer);
			writer.WriteEndElement();
		}

		// Token: 0x06006271 RID: 25201 RVA: 0x0016E664 File Offset: 0x0016C864
		[__DynamicallyInvokable]
		protected virtual void OnWriteStartDetail(XmlDictionaryWriter writer, EnvelopeVersion version)
		{
			if (version == EnvelopeVersion.Soap12)
			{
				writer.WriteStartElement(XD.Message12Dictionary.FaultDetail, XD.Message12Dictionary.Namespace);
				return;
			}
			if (version == EnvelopeVersion.Soap11)
			{
				writer.WriteStartElement(XD.Message11Dictionary.FaultDetail, XD.Message11Dictionary.FaultNamespace);
				return;
			}
			writer.WriteStartElement(XD.Message12Dictionary.FaultDetail, XD.MessageDictionary.Namespace);
		}

		// Token: 0x06006272 RID: 25202
		[__DynamicallyInvokable]
		protected abstract void OnWriteDetailContents(XmlDictionaryWriter writer);

		// Token: 0x06006273 RID: 25203 RVA: 0x0016E6D4 File Offset: 0x0016C8D4
		[__DynamicallyInvokable]
		protected virtual XmlDictionaryReader OnGetReaderAtDetailContents()
		{
			XmlBuffer xmlBuffer = new XmlBuffer(int.MaxValue);
			XmlDictionaryWriter writer = xmlBuffer.OpenSection(XmlDictionaryReaderQuotas.Max);
			this.OnWriteDetail(writer, EnvelopeVersion.Soap12);
			xmlBuffer.CloseSection();
			xmlBuffer.Close();
			XmlDictionaryReader reader = xmlBuffer.GetReader(0);
			reader.Read();
			return reader;
		}

		// Token: 0x06006274 RID: 25204 RVA: 0x0016E720 File Offset: 0x0016C920
		public static bool WasHeaderNotUnderstood(MessageHeaders headers, string name, string ns)
		{
			if (headers == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("headers");
			}
			for (int i = 0; i < headers.Count; i++)
			{
				MessageHeaderInfo messageHeaderInfo = headers[i];
				if (string.Compare(messageHeaderInfo.Name, "NotUnderstood", StringComparison.Ordinal) == 0 && string.Compare(messageHeaderInfo.Namespace, "http://www.w3.org/2003/05/soap-envelope", StringComparison.Ordinal) == 0)
				{
					using (XmlDictionaryReader readerAtHeader = headers.GetReaderAtHeader(i))
					{
						readerAtHeader.MoveToAttribute("qname", "http://www.w3.org/2003/05/soap-envelope");
						string text;
						string text2;
						readerAtHeader.ReadContentAsQualifiedName(out text, out text2);
						if (text != null && text2 != null && string.Compare(name, text, StringComparison.Ordinal) == 0 && string.Compare(ns, text2, StringComparison.Ordinal) == 0)
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06006275 RID: 25205 RVA: 0x0016E7EC File Offset: 0x0016C9EC
		public void WriteTo(XmlWriter writer, EnvelopeVersion version)
		{
			this.WriteTo(XmlDictionaryWriter.CreateDictionaryWriter(writer), version);
		}

		// Token: 0x06006276 RID: 25206 RVA: 0x0016E7FC File Offset: 0x0016C9FC
		public void WriteTo(XmlDictionaryWriter writer, EnvelopeVersion version)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (version == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("version");
			}
			if (version == EnvelopeVersion.Soap12)
			{
				this.WriteTo12(writer);
				return;
			}
			if (version == EnvelopeVersion.Soap11)
			{
				this.WriteTo11(writer);
				return;
			}
			if (version == EnvelopeVersion.None)
			{
				this.WriteToNone(writer);
				return;
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("EnvelopeVersionUnknown", new object[]
			{
				version.ToString()
			})));
		}

		// Token: 0x06006277 RID: 25207 RVA: 0x0016E887 File Offset: 0x0016CA87
		private void WriteToNone(XmlDictionaryWriter writer)
		{
			this.WriteTo12Driver(writer, EnvelopeVersion.None);
		}

		// Token: 0x06006278 RID: 25208 RVA: 0x0016E898 File Offset: 0x0016CA98
		private void WriteTo12Driver(XmlDictionaryWriter writer, EnvelopeVersion version)
		{
			writer.WriteStartElement(XD.MessageDictionary.Fault, version.DictionaryNamespace);
			writer.WriteStartElement(XD.Message12Dictionary.FaultCode, version.DictionaryNamespace);
			this.WriteFaultCode12Driver(writer, this.Code, version);
			writer.WriteEndElement();
			writer.WriteStartElement(XD.Message12Dictionary.FaultReason, version.DictionaryNamespace);
			FaultReason reason = this.Reason;
			for (int i = 0; i < reason.Translations.Count; i++)
			{
				FaultReasonText faultReasonText = reason.Translations[i];
				writer.WriteStartElement(XD.Message12Dictionary.FaultText, version.DictionaryNamespace);
				writer.WriteAttributeString("xml", "lang", "http://www.w3.org/XML/1998/namespace", faultReasonText.XmlLang);
				writer.WriteString(faultReasonText.Text);
				writer.WriteEndElement();
			}
			writer.WriteEndElement();
			if (this.Node.Length > 0)
			{
				writer.WriteElementString(XD.Message12Dictionary.FaultNode, version.DictionaryNamespace, this.Node);
			}
			if (this.Actor.Length > 0)
			{
				writer.WriteElementString(XD.Message12Dictionary.FaultRole, version.DictionaryNamespace, this.Actor);
			}
			if (this.HasDetail)
			{
				this.OnWriteDetail(writer, version);
			}
			writer.WriteEndElement();
		}

		// Token: 0x06006279 RID: 25209 RVA: 0x0016E9D8 File Offset: 0x0016CBD8
		private void WriteFaultCode12Driver(XmlDictionaryWriter writer, FaultCode faultCode, EnvelopeVersion version)
		{
			writer.WriteStartElement(XD.Message12Dictionary.FaultValue, version.DictionaryNamespace);
			string localName;
			if (faultCode.IsSenderFault)
			{
				localName = version.SenderFaultName;
			}
			else if (faultCode.IsReceiverFault)
			{
				localName = version.ReceiverFaultName;
			}
			else
			{
				localName = faultCode.Name;
			}
			string @namespace;
			if (faultCode.IsPredefinedFault)
			{
				@namespace = version.Namespace;
			}
			else
			{
				@namespace = faultCode.Namespace;
			}
			if (writer.LookupPrefix(@namespace) == null)
			{
				writer.WriteAttributeString("xmlns", "a", "http://www.w3.org/2000/xmlns/", @namespace);
			}
			writer.WriteQualifiedName(localName, @namespace);
			writer.WriteEndElement();
			if (faultCode.SubCode != null)
			{
				writer.WriteStartElement(XD.Message12Dictionary.FaultSubcode, version.DictionaryNamespace);
				this.WriteFaultCode12Driver(writer, faultCode.SubCode, version);
				writer.WriteEndElement();
			}
		}

		// Token: 0x0600627A RID: 25210 RVA: 0x0016EA9D File Offset: 0x0016CC9D
		private void WriteTo12(XmlDictionaryWriter writer)
		{
			this.WriteTo12Driver(writer, EnvelopeVersion.Soap12);
		}

		// Token: 0x0600627B RID: 25211 RVA: 0x0016EAAC File Offset: 0x0016CCAC
		private void WriteTo11(XmlDictionaryWriter writer)
		{
			writer.WriteStartElement(XD.MessageDictionary.Fault, XD.Message11Dictionary.Namespace);
			writer.WriteStartElement(XD.Message11Dictionary.FaultCode, XD.Message11Dictionary.FaultNamespace);
			FaultCode faultCode = this.Code;
			if (faultCode.SubCode != null)
			{
				faultCode = faultCode.SubCode;
			}
			string localName;
			if (faultCode.IsSenderFault)
			{
				localName = "Client";
			}
			else if (faultCode.IsReceiverFault)
			{
				localName = "Server";
			}
			else
			{
				localName = faultCode.Name;
			}
			string text;
			if (faultCode.IsPredefinedFault)
			{
				text = "http://schemas.xmlsoap.org/soap/envelope/";
			}
			else
			{
				text = faultCode.Namespace;
			}
			if (writer.LookupPrefix(text) == null)
			{
				writer.WriteAttributeString("xmlns", "a", "http://www.w3.org/2000/xmlns/", text);
			}
			writer.WriteQualifiedName(localName, text);
			writer.WriteEndElement();
			FaultReasonText faultReasonText = this.Reason.Translations[0];
			writer.WriteStartElement(XD.Message11Dictionary.FaultString, XD.Message11Dictionary.FaultNamespace);
			if (faultReasonText.XmlLang.Length > 0)
			{
				writer.WriteAttributeString("xml", "lang", "http://www.w3.org/XML/1998/namespace", faultReasonText.XmlLang);
			}
			writer.WriteString(faultReasonText.Text);
			writer.WriteEndElement();
			if (this.Actor.Length > 0)
			{
				writer.WriteElementString(XD.Message11Dictionary.FaultActor, XD.Message11Dictionary.FaultNamespace, this.Actor);
			}
			if (this.HasDetail)
			{
				this.OnWriteDetail(writer, EnvelopeVersion.Soap11);
			}
			writer.WriteEndElement();
		}

		// Token: 0x0600627C RID: 25212 RVA: 0x0016EC23 File Offset: 0x0016CE23
		[__DynamicallyInvokable]
		protected MessageFault()
		{
		}

		// Token: 0x04003919 RID: 14617
		private static MessageFault defaultMessageFault;
	}
}
