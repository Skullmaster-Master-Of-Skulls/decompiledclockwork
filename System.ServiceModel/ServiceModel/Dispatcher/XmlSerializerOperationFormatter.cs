using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Xml;
using System.Xml.Serialization;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020005AF RID: 1455
	internal class XmlSerializerOperationFormatter : OperationFormatter
	{
		// Token: 0x060038CB RID: 14539 RVA: 0x000DB518 File Offset: 0x000D9718
		public XmlSerializerOperationFormatter(OperationDescription description, XmlSerializerFormatAttribute xmlSerializerFormatAttribute, XmlSerializerOperationFormatter.MessageInfo requestMessageInfo, XmlSerializerOperationFormatter.MessageInfo replyMessageInfo) : base(description, xmlSerializerFormatAttribute.Style == OperationFormatStyle.Rpc, xmlSerializerFormatAttribute.IsEncoded)
		{
			if (xmlSerializerFormatAttribute.IsEncoded && xmlSerializerFormatAttribute.Style != OperationFormatStyle.Rpc)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxDocEncodedNotSupported", new object[]
				{
					description.Name
				})));
			}
			this.isEncoded = xmlSerializerFormatAttribute.IsEncoded;
			this.requestMessageInfo = requestMessageInfo;
			this.replyMessageInfo = replyMessageInfo;
		}

		// Token: 0x060038CC RID: 14540 RVA: 0x000DB590 File Offset: 0x000D9790
		protected override void AddHeadersToMessage(Message message, MessageDescription messageDescription, object[] parameters, bool isRequest)
		{
			try
			{
				XmlSerializer headerSerializer;
				OperationFormatter.MessageHeaderDescriptionTable headerDescriptionTable;
				MessageHeaderDescription unknownHeaderDescription;
				if (isRequest)
				{
					headerSerializer = this.requestMessageInfo.HeaderSerializer;
					headerDescriptionTable = this.requestMessageInfo.HeaderDescriptionTable;
					unknownHeaderDescription = this.requestMessageInfo.UnknownHeaderDescription;
				}
				else
				{
					headerSerializer = this.replyMessageInfo.HeaderSerializer;
					headerDescriptionTable = this.replyMessageInfo.HeaderDescriptionTable;
					unknownHeaderDescription = this.replyMessageInfo.UnknownHeaderDescription;
				}
				if (headerSerializer != null)
				{
					object[] array = new object[headerDescriptionTable.Count];
					XmlSerializerOperationFormatter.MessageHeaderOfTHelper messageHeaderOfTHelper = null;
					int num = 0;
					foreach (MessageHeaderDescription messageHeaderDescription in messageDescription.Headers)
					{
						object obj = parameters[messageHeaderDescription.Index];
						if (!messageHeaderDescription.IsUnknownHeaderCollection)
						{
							if (messageHeaderDescription.TypedHeader)
							{
								if (messageHeaderOfTHelper == null)
								{
									messageHeaderOfTHelper = new XmlSerializerOperationFormatter.MessageHeaderOfTHelper(parameters.Length);
								}
								array[num++] = messageHeaderOfTHelper.GetContentAndSaveHeaderAttributes(parameters[messageHeaderDescription.Index], messageHeaderDescription);
							}
							else
							{
								array[num++] = obj;
							}
						}
					}
					MemoryStream memoryStream = new MemoryStream();
					XmlDictionaryWriter xmlDictionaryWriter = XmlDictionaryWriter.CreateTextWriter(memoryStream);
					xmlDictionaryWriter.WriteStartElement("root");
					headerSerializer.Serialize(xmlDictionaryWriter, array, null, this.isEncoded ? XmlSerializerOperationFormatter.GetEncoding(message.Version.Envelope) : null);
					xmlDictionaryWriter.WriteEndElement();
					xmlDictionaryWriter.Flush();
					XmlDocument xmlDocument = new XmlDocument();
					memoryStream.Position = 0L;
					xmlDocument.Load(new XmlTextReader(memoryStream)
					{
						DtdProcessing = DtdProcessing.Prohibit
					});
					foreach (object obj2 in xmlDocument.DocumentElement.ChildNodes)
					{
						XmlElement xmlElement = (XmlElement)obj2;
						MessageHeaderDescription messageHeaderDescription2 = headerDescriptionTable.Get(xmlElement.LocalName, xmlElement.NamespaceURI);
						if (messageHeaderDescription2 == null)
						{
							message.Headers.Add(new OperationFormatter.XmlElementMessageHeader(this, message.Version, xmlElement.LocalName, xmlElement.NamespaceURI, false, null, false, xmlElement));
						}
						else
						{
							bool mustUnderstand;
							bool relay;
							string actor;
							if (messageHeaderDescription2.TypedHeader)
							{
								messageHeaderOfTHelper.GetHeaderAttributes(messageHeaderDescription2, out mustUnderstand, out relay, out actor);
							}
							else
							{
								mustUnderstand = messageHeaderDescription2.MustUnderstand;
								relay = messageHeaderDescription2.Relay;
								actor = messageHeaderDescription2.Actor;
							}
							message.Headers.Add(new OperationFormatter.XmlElementMessageHeader(this, message.Version, xmlElement.LocalName, xmlElement.NamespaceURI, mustUnderstand, actor, relay, xmlElement));
						}
					}
				}
				if (unknownHeaderDescription != null && parameters[unknownHeaderDescription.Index] != null)
				{
					foreach (object parameterValue in ((IEnumerable)parameters[unknownHeaderDescription.Index]))
					{
						bool mustUnderstand;
						bool relay;
						string actor;
						XmlElement xmlElement2 = (XmlElement)OperationFormatter.GetContentOfMessageHeaderOfT(unknownHeaderDescription, parameterValue, out mustUnderstand, out relay, out actor);
						if (xmlElement2 != null)
						{
							message.Headers.Add(new OperationFormatter.XmlElementMessageHeader(this, message.Version, xmlElement2.LocalName, xmlElement2.NamespaceURI, mustUnderstand, actor, relay, xmlElement2));
						}
					}
				}
			}
			catch (InvalidOperationException ex)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CommunicationException(SR.GetString("SFxErrorSerializingHeader", new object[]
				{
					messageDescription.MessageName,
					ex.Message
				}), ex));
			}
		}

		// Token: 0x060038CD RID: 14541 RVA: 0x000DB920 File Offset: 0x000D9B20
		protected override void GetHeadersFromMessage(Message message, MessageDescription messageDescription, object[] parameters, bool isRequest)
		{
			try
			{
				XmlSerializer headerSerializer;
				OperationFormatter.MessageHeaderDescriptionTable headerDescriptionTable;
				MessageHeaderDescription unknownHeaderDescription;
				if (isRequest)
				{
					headerSerializer = this.requestMessageInfo.HeaderSerializer;
					headerDescriptionTable = this.requestMessageInfo.HeaderDescriptionTable;
					unknownHeaderDescription = this.requestMessageInfo.UnknownHeaderDescription;
				}
				else
				{
					headerSerializer = this.replyMessageInfo.HeaderSerializer;
					headerDescriptionTable = this.replyMessageInfo.HeaderDescriptionTable;
					unknownHeaderDescription = this.replyMessageInfo.UnknownHeaderDescription;
				}
				MessageHeaders headers = message.Headers;
				ArrayList arrayList = null;
				XmlDocument xmlDoc = null;
				if (unknownHeaderDescription != null)
				{
					arrayList = new ArrayList();
					xmlDoc = new XmlDocument();
				}
				if (headerSerializer == null)
				{
					if (unknownHeaderDescription != null)
					{
						for (int i = 0; i < headers.Count; i++)
						{
							XmlSerializerOperationFormatter.AddUnknownHeader(unknownHeaderDescription, arrayList, xmlDoc, null, headers[i], headers.GetReaderAtHeader(i));
						}
						parameters[unknownHeaderDescription.Index] = arrayList.ToArray(unknownHeaderDescription.TypedHeader ? typeof(MessageHeader<XmlElement>) : typeof(XmlElement));
					}
				}
				else
				{
					MemoryStream memoryStream = new MemoryStream();
					XmlDictionaryWriter xmlDictionaryWriter = XmlDictionaryWriter.CreateTextWriter(memoryStream);
					message.WriteStartEnvelope(xmlDictionaryWriter);
					message.WriteStartHeaders(xmlDictionaryWriter);
					XmlSerializerOperationFormatter.MessageHeaderOfTHelper messageHeaderOfTHelper = null;
					for (int j = 0; j < headers.Count; j++)
					{
						MessageHeaderInfo messageHeaderInfo = headers[j];
						XmlDictionaryReader readerAtHeader = headers.GetReaderAtHeader(j);
						MessageHeaderDescription messageHeaderDescription = headerDescriptionTable.Get(messageHeaderInfo.Name, messageHeaderInfo.Namespace);
						if (messageHeaderDescription != null)
						{
							if (messageHeaderInfo.MustUnderstand)
							{
								headers.UnderstoodHeaders.Add(messageHeaderInfo);
							}
							if (messageHeaderDescription.TypedHeader)
							{
								if (messageHeaderOfTHelper == null)
								{
									messageHeaderOfTHelper = new XmlSerializerOperationFormatter.MessageHeaderOfTHelper(parameters.Length);
								}
								messageHeaderOfTHelper.SetHeaderAttributes(messageHeaderDescription, messageHeaderInfo.MustUnderstand, messageHeaderInfo.Relay, messageHeaderInfo.Actor);
							}
						}
						if (messageHeaderDescription == null && unknownHeaderDescription != null)
						{
							XmlSerializerOperationFormatter.AddUnknownHeader(unknownHeaderDescription, arrayList, xmlDoc, xmlDictionaryWriter, messageHeaderInfo, readerAtHeader);
						}
						else
						{
							xmlDictionaryWriter.WriteNode(readerAtHeader, false);
						}
						readerAtHeader.Close();
					}
					xmlDictionaryWriter.WriteEndElement();
					xmlDictionaryWriter.WriteEndElement();
					xmlDictionaryWriter.Flush();
					memoryStream.Position = 0L;
					XmlDictionaryReader xmlDictionaryReader = XmlDictionaryReader.CreateTextReader(memoryStream.GetBuffer(), 0, (int)memoryStream.Length, XmlDictionaryReaderQuotas.Max);
					xmlDictionaryReader.ReadStartElement();
					xmlDictionaryReader.MoveToContent();
					if (!xmlDictionaryReader.IsEmptyElement)
					{
						xmlDictionaryReader.ReadStartElement();
						object[] array = (object[])headerSerializer.Deserialize(xmlDictionaryReader, this.isEncoded ? XmlSerializerOperationFormatter.GetEncoding(message.Version.Envelope) : null);
						int num = 0;
						foreach (MessageHeaderDescription messageHeaderDescription2 in messageDescription.Headers)
						{
							if (!messageHeaderDescription2.IsUnknownHeaderCollection)
							{
								object obj = array[num++];
								if (messageHeaderDescription2.TypedHeader && obj != null)
								{
									obj = messageHeaderOfTHelper.CreateMessageHeader(messageHeaderDescription2, obj);
								}
								parameters[messageHeaderDescription2.Index] = obj;
							}
						}
						xmlDictionaryReader.Close();
					}
					if (unknownHeaderDescription != null)
					{
						parameters[unknownHeaderDescription.Index] = arrayList.ToArray(unknownHeaderDescription.TypedHeader ? typeof(MessageHeader<XmlElement>) : typeof(XmlElement));
					}
				}
			}
			catch (InvalidOperationException innerException)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CommunicationException(SR.GetString("SFxErrorDeserializingHeader", new object[]
				{
					messageDescription.MessageName
				}), innerException));
			}
		}

		// Token: 0x060038CE RID: 14542 RVA: 0x000DBC70 File Offset: 0x000D9E70
		private static void AddUnknownHeader(MessageHeaderDescription unknownHeaderDescription, ArrayList unknownHeaders, XmlDocument xmlDoc, XmlDictionaryWriter bufferWriter, MessageHeaderInfo header, XmlDictionaryReader headerReader)
		{
			object obj = xmlDoc.ReadNode(headerReader);
			if (bufferWriter != null)
			{
				((XmlElement)obj).WriteTo(bufferWriter);
			}
			if (obj != null && unknownHeaderDescription.TypedHeader)
			{
				obj = TypedHeaderManager.Create(unknownHeaderDescription.Type, obj, header.MustUnderstand, header.Relay, header.Actor);
			}
			unknownHeaders.Add(obj);
		}

		// Token: 0x060038CF RID: 14543 RVA: 0x000DBCCC File Offset: 0x000D9ECC
		protected override void WriteBodyAttributes(XmlDictionaryWriter writer, MessageVersion version)
		{
			if (this.isEncoded && version.Envelope == EnvelopeVersion.Soap11)
			{
				string encoding = XmlSerializerOperationFormatter.GetEncoding(version.Envelope);
				writer.WriteAttributeString("encodingStyle", version.Envelope.Namespace, encoding);
			}
			writer.WriteAttributeString("xmlns", "xsi", null, "http://www.w3.org/2001/XMLSchema-instance");
			writer.WriteAttributeString("xmlns", "xsd", null, "http://www.w3.org/2001/XMLSchema");
		}

		// Token: 0x060038D0 RID: 14544 RVA: 0x000DBD40 File Offset: 0x000D9F40
		protected override void SerializeBody(XmlDictionaryWriter writer, MessageVersion version, string action, MessageDescription messageDescription, object returnValue, object[] parameters, bool isRequest)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("writer"));
			}
			if (parameters == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("parameters"));
			}
			try
			{
				XmlSerializerOperationFormatter.MessageInfo messageInfo;
				if (isRequest)
				{
					messageInfo = this.requestMessageInfo;
				}
				else
				{
					messageInfo = this.replyMessageInfo;
				}
				if (messageInfo.RpcEncodedTypedMessageBodyParts == null)
				{
					this.SerializeBody(writer, version, messageInfo.BodySerializer, messageDescription.Body.ReturnValue, messageDescription.Body.Parts, returnValue, parameters);
				}
				else
				{
					object[] array = new object[messageInfo.RpcEncodedTypedMessageBodyParts.Count];
					object obj = parameters[messageDescription.Body.Parts[0].Index];
					if (obj == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxBodyCannotBeNull", new object[]
						{
							messageDescription.MessageName
						})));
					}
					int num = 0;
					foreach (MessagePartDescription messagePartDescription in messageInfo.RpcEncodedTypedMessageBodyParts)
					{
						MemberInfo memberInfo = messagePartDescription.MemberInfo;
						FieldInfo fieldInfo = memberInfo as FieldInfo;
						if (fieldInfo != null)
						{
							array[num++] = fieldInfo.GetValue(obj);
						}
						else
						{
							PropertyInfo propertyInfo = memberInfo as PropertyInfo;
							if (propertyInfo != null)
							{
								array[num++] = propertyInfo.GetValue(obj, null);
							}
						}
					}
					this.SerializeBody(writer, version, messageInfo.BodySerializer, null, messageInfo.RpcEncodedTypedMessageBodyParts, null, array);
				}
			}
			catch (InvalidOperationException ex)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CommunicationException(SR.GetString("SFxErrorSerializingBody", new object[]
				{
					messageDescription.MessageName,
					ex.Message
				}), ex));
			}
		}

		// Token: 0x060038D1 RID: 14545 RVA: 0x000DBF2C File Offset: 0x000DA12C
		private void SerializeBody(XmlDictionaryWriter writer, MessageVersion version, XmlSerializer serializer, MessagePartDescription returnPart, MessagePartDescriptionCollection bodyParts, object returnValue, object[] parameters)
		{
			if (serializer == null)
			{
				return;
			}
			bool flag = OperationFormatter.IsValidReturnValue(returnPart);
			object[] array = new object[bodyParts.Count + (flag ? 1 : 0)];
			int num = 0;
			if (flag)
			{
				array[num++] = returnValue;
			}
			for (int i = 0; i < bodyParts.Count; i++)
			{
				array[num++] = parameters[bodyParts[i].Index];
			}
			string encodingStyle = this.isEncoded ? XmlSerializerOperationFormatter.GetEncoding(version.Envelope) : null;
			serializer.Serialize(writer, array, null, encodingStyle);
		}

		// Token: 0x060038D2 RID: 14546 RVA: 0x000DBFB8 File Offset: 0x000DA1B8
		protected override object DeserializeBody(XmlDictionaryReader reader, MessageVersion version, string action, MessageDescription messageDescription, object[] parameters, bool isRequest)
		{
			XmlSerializerOperationFormatter.MessageInfo messageInfo;
			if (isRequest)
			{
				messageInfo = this.requestMessageInfo;
			}
			else
			{
				messageInfo = this.replyMessageInfo;
			}
			if (messageInfo.RpcEncodedTypedMessageBodyParts == null)
			{
				return this.DeserializeBody(reader, version, messageInfo.BodySerializer, messageDescription.Body.ReturnValue, messageDescription.Body.Parts, parameters, isRequest);
			}
			object[] array = new object[messageInfo.RpcEncodedTypedMessageBodyParts.Count];
			this.DeserializeBody(reader, version, messageInfo.BodySerializer, null, messageInfo.RpcEncodedTypedMessageBodyParts, array, isRequest);
			object obj = Activator.CreateInstance(messageDescription.Body.Parts[0].Type);
			int num = 0;
			foreach (MessagePartDescription messagePartDescription in messageInfo.RpcEncodedTypedMessageBodyParts)
			{
				MemberInfo memberInfo = messagePartDescription.MemberInfo;
				FieldInfo fieldInfo = memberInfo as FieldInfo;
				if (fieldInfo != null)
				{
					fieldInfo.SetValue(obj, array[num++]);
				}
				else
				{
					PropertyInfo propertyInfo = memberInfo as PropertyInfo;
					if (propertyInfo != null)
					{
						propertyInfo.SetValue(obj, array[num++], null);
					}
				}
			}
			parameters[messageDescription.Body.Parts[0].Index] = obj;
			return null;
		}

		// Token: 0x060038D3 RID: 14547 RVA: 0x000DC100 File Offset: 0x000DA300
		private object DeserializeBody(XmlDictionaryReader reader, MessageVersion version, XmlSerializer serializer, MessagePartDescription returnPart, MessagePartDescriptionCollection bodyParts, object[] parameters, bool isRequest)
		{
			object result;
			try
			{
				if (reader == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("reader"));
				}
				if (parameters == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("parameters"));
				}
				object obj = null;
				if (serializer == null)
				{
					result = null;
				}
				else if (reader.NodeType == XmlNodeType.EndElement)
				{
					result = null;
				}
				else
				{
					object[] array = (object[])serializer.Deserialize(reader, this.isEncoded ? XmlSerializerOperationFormatter.GetEncoding(version.Envelope) : null);
					int num = 0;
					if (OperationFormatter.IsValidReturnValue(returnPart))
					{
						obj = array[num++];
					}
					for (int i = 0; i < bodyParts.Count; i++)
					{
						parameters[bodyParts[i].Index] = array[num++];
					}
					result = obj;
				}
			}
			catch (InvalidOperationException innerException)
			{
				string name = isRequest ? "SFxErrorDeserializingRequestBody" : "SFxErrorDeserializingReplyBody";
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CommunicationException(SR.GetString(name, new object[]
				{
					base.OperationName
				}), innerException));
			}
			return result;
		}

		// Token: 0x060038D4 RID: 14548 RVA: 0x000DC210 File Offset: 0x000DA410
		internal static string GetEncoding(EnvelopeVersion version)
		{
			if (version == EnvelopeVersion.Soap11)
			{
				return "http://schemas.xmlsoap.org/soap/encoding/";
			}
			if (version == EnvelopeVersion.Soap12)
			{
				return "http://www.w3.org/2003/05/soap-encoding";
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("version", SR.GetString("EnvelopeVersionNotSupported", new object[]
			{
				version
			}));
		}

		// Token: 0x040029B4 RID: 10676
		private const string soap11Encoding = "http://schemas.xmlsoap.org/soap/encoding/";

		// Token: 0x040029B5 RID: 10677
		private const string soap12Encoding = "http://www.w3.org/2003/05/soap-encoding";

		// Token: 0x040029B6 RID: 10678
		private bool isEncoded;

		// Token: 0x040029B7 RID: 10679
		private XmlSerializerOperationFormatter.MessageInfo requestMessageInfo;

		// Token: 0x040029B8 RID: 10680
		private XmlSerializerOperationFormatter.MessageInfo replyMessageInfo;

		// Token: 0x02000CAE RID: 3246
		internal abstract class MessageInfo
		{
			// Token: 0x17001B8B RID: 7051
			// (get) Token: 0x06007946 RID: 31046
			internal abstract XmlSerializer BodySerializer { get; }

			// Token: 0x17001B8C RID: 7052
			// (get) Token: 0x06007947 RID: 31047
			internal abstract XmlSerializer HeaderSerializer { get; }

			// Token: 0x17001B8D RID: 7053
			// (get) Token: 0x06007948 RID: 31048
			internal abstract OperationFormatter.MessageHeaderDescriptionTable HeaderDescriptionTable { get; }

			// Token: 0x17001B8E RID: 7054
			// (get) Token: 0x06007949 RID: 31049
			internal abstract MessageHeaderDescription UnknownHeaderDescription { get; }

			// Token: 0x17001B8F RID: 7055
			// (get) Token: 0x0600794A RID: 31050
			internal abstract MessagePartDescriptionCollection RpcEncodedTypedMessageBodyParts { get; }
		}

		// Token: 0x02000CAF RID: 3247
		private class MessageHeaderOfTHelper
		{
			// Token: 0x0600794C RID: 31052 RVA: 0x001C4CAF File Offset: 0x001C2EAF
			internal MessageHeaderOfTHelper(int parameterCount)
			{
				this.attributes = new object[parameterCount];
			}

			// Token: 0x0600794D RID: 31053 RVA: 0x001C4CC4 File Offset: 0x001C2EC4
			internal object GetContentAndSaveHeaderAttributes(object parameterValue, MessageHeaderDescription headerDescription)
			{
				if (parameterValue == null)
				{
					return null;
				}
				bool mustUnderstand;
				bool relay;
				string actor;
				if (headerDescription.Multiple)
				{
					object[] array = (object[])parameterValue;
					MessageHeader<object>[] array2 = new MessageHeader<object>[array.Length];
					Array array3 = Array.CreateInstance(headerDescription.Type, array.Length);
					for (int i = 0; i < array3.Length; i++)
					{
						array3.SetValue(OperationFormatter.GetContentOfMessageHeaderOfT(headerDescription, array[i], out mustUnderstand, out relay, out actor), i);
						array2[i] = new MessageHeader<object>(null, mustUnderstand, actor, relay);
					}
					this.attributes[headerDescription.Index] = array2;
					return array3;
				}
				object contentOfMessageHeaderOfT = OperationFormatter.GetContentOfMessageHeaderOfT(headerDescription, parameterValue, out mustUnderstand, out relay, out actor);
				this.attributes[headerDescription.Index] = new MessageHeader<object>(null, mustUnderstand, actor, relay);
				return contentOfMessageHeaderOfT;
			}

			// Token: 0x0600794E RID: 31054 RVA: 0x001C4D78 File Offset: 0x001C2F78
			internal void GetHeaderAttributes(MessageHeaderDescription headerDescription, out bool mustUnderstand, out bool relay, out string actor)
			{
				MessageHeader<object> messageHeader = null;
				if (headerDescription.Multiple)
				{
					MessageHeader<object>[] array = (MessageHeader<object>[])this.attributes[headerDescription.Index];
					for (int i = 0; i < array.Length; i++)
					{
						if (array[i] != null)
						{
							messageHeader = array[i];
							array[i] = null;
							break;
						}
					}
				}
				else
				{
					messageHeader = (MessageHeader<object>)this.attributes[headerDescription.Index];
				}
				mustUnderstand = messageHeader.MustUnderstand;
				relay = messageHeader.Relay;
				actor = messageHeader.Actor;
			}

			// Token: 0x0600794F RID: 31055 RVA: 0x001C4DF0 File Offset: 0x001C2FF0
			internal void SetHeaderAttributes(MessageHeaderDescription headerDescription, bool mustUnderstand, bool relay, string actor)
			{
				if (headerDescription.Multiple)
				{
					if (this.attributes[headerDescription.Index] == null)
					{
						this.attributes[headerDescription.Index] = new List<MessageHeader<object>>();
					}
					((List<MessageHeader<object>>)this.attributes[headerDescription.Index]).Add(new MessageHeader<object>(null, mustUnderstand, actor, relay));
					return;
				}
				this.attributes[headerDescription.Index] = new MessageHeader<object>(null, mustUnderstand, actor, relay);
			}

			// Token: 0x06007950 RID: 31056 RVA: 0x001C4E60 File Offset: 0x001C3060
			internal object CreateMessageHeader(MessageHeaderDescription headerDescription, object headerValue)
			{
				if (headerDescription.Multiple)
				{
					IList<MessageHeader<object>> list = (IList<MessageHeader<object>>)this.attributes[headerDescription.Index];
					object[] array = (object[])Array.CreateInstance(TypedHeaderManager.GetMessageHeaderType(headerDescription.Type), list.Count);
					Array array2 = (Array)headerValue;
					for (int i = 0; i < array.Length; i++)
					{
						MessageHeader<object> messageHeader = list[i];
						array[i] = TypedHeaderManager.Create(headerDescription.Type, array2.GetValue(i), messageHeader.MustUnderstand, messageHeader.Relay, messageHeader.Actor);
					}
					return array;
				}
				MessageHeader<object> messageHeader2 = (MessageHeader<object>)this.attributes[headerDescription.Index];
				return TypedHeaderManager.Create(headerDescription.Type, headerValue, messageHeader2.MustUnderstand, messageHeader2.Relay, messageHeader2.Actor);
			}

			// Token: 0x04004529 RID: 17705
			private object[] attributes;
		}
	}
}
