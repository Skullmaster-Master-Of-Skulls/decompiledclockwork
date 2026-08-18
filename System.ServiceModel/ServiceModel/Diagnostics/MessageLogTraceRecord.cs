using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Runtime.Diagnostics;
using System.ServiceModel.Channels;
using System.ServiceModel.Security;
using System.Text;
using System.Xml;

namespace System.ServiceModel.Diagnostics
{
	// Token: 0x02000A82 RID: 2690
	internal sealed class MessageLogTraceRecord : TraceRecord
	{
		// Token: 0x06006A27 RID: 27175 RVA: 0x0018B9DD File Offset: 0x00189BDD
		private MessageLogTraceRecord(MessageLoggingSource source)
		{
			this.source = source;
			this.timestamp = DateTime.Now;
		}

		// Token: 0x06006A28 RID: 27176 RVA: 0x0018B9FE File Offset: 0x00189BFE
		internal MessageLogTraceRecord(ArraySegment<byte> buffer, MessageLoggingSource source) : this(source)
		{
			this.type = null;
			this.messageString = Encoding.UTF8.GetString(buffer.Array, buffer.Offset, buffer.Count);
		}

		// Token: 0x06006A29 RID: 27177 RVA: 0x0018BA33 File Offset: 0x00189C33
		internal MessageLogTraceRecord(string message, MessageLoggingSource source) : this(source)
		{
			this.type = null;
			this.messageString = message;
		}

		// Token: 0x06006A2A RID: 27178 RVA: 0x0018BA4C File Offset: 0x00189C4C
		internal MessageLogTraceRecord(Stream stream, MessageLoggingSource source) : this(source)
		{
			this.type = null;
			StringBuilder stringBuilder = new StringBuilder();
			StreamReader streamReader = new StreamReader(stream);
			int num = 4096;
			char[] array = DiagnosticUtility.Utility.AllocateCharArray(num);
			int i = MessageLogger.MaxMessageSize;
			if (-1 == i)
			{
				i = 4096;
			}
			while (i > 0)
			{
				int num2 = streamReader.Read(array, 0, num);
				if (num2 == 0)
				{
					break;
				}
				int charCount = (i < num2) ? i : num2;
				stringBuilder.Append(array, 0, charCount);
				i -= num2;
			}
			streamReader.Close();
			this.messageString = stringBuilder.ToString();
		}

		// Token: 0x06006A2B RID: 27179 RVA: 0x0018BAE4 File Offset: 0x00189CE4
		internal MessageLogTraceRecord(ref Message message, XmlReader reader, MessageLoggingSource source, bool logMessageBody) : this(source)
		{
			MessageBuffer messageBuffer = null;
			try
			{
				this.logMessageBody = logMessageBody;
				this.message = message;
				this.reader = reader;
				this.type = message.GetType();
			}
			finally
			{
				if (messageBuffer != null)
				{
					messageBuffer.Close();
				}
			}
		}

		// Token: 0x1700194C RID: 6476
		// (get) Token: 0x06006A2C RID: 27180 RVA: 0x0018BB3C File Offset: 0x00189D3C
		public Message Message
		{
			get
			{
				return this.message;
			}
		}

		// Token: 0x1700194D RID: 6477
		// (get) Token: 0x06006A2D RID: 27181 RVA: 0x0018BB44 File Offset: 0x00189D44
		public MessageLoggingSource MessageLoggingSource
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x06006A2E RID: 27182 RVA: 0x0018BB4C File Offset: 0x00189D4C
		internal override void WriteTo(XmlWriter writer)
		{
			writer.WriteStartElement("", "MessageLogTraceRecord", "http://schemas.microsoft.com/2004/06/ServiceModel/Management/MessageTrace");
			writer.WriteAttributeString("Time", this.timestamp.ToString("o", CultureInfo.InvariantCulture));
			writer.WriteAttributeString("Source", this.source.ToString());
			if (null != this.type)
			{
				XmlDictionaryWriter xmlDictionaryWriter = XmlDictionaryWriter.CreateDictionaryWriter(writer);
				xmlDictionaryWriter.WriteAttributeString("Type", this.type.ToString());
				this.WriteAddressingProperties(xmlDictionaryWriter);
				this.WriteHttpProperties(xmlDictionaryWriter);
				if (this.reader != null)
				{
					this.reader.MoveToContent();
				}
				if (this.logMessageBody)
				{
					if (this.reader != null)
					{
						xmlDictionaryWriter.WriteNode(this.reader, true);
					}
					else
					{
						bool flag = false;
						if (this.message is SecurityVerifiedMessage)
						{
							SecurityVerifiedMessage securityVerifiedMessage = this.message as SecurityVerifiedMessage;
							ReceiveSecurityHeader receivedSecurityHeader = securityVerifiedMessage.ReceivedSecurityHeader;
							flag = receivedSecurityHeader.HasAtLeastOneItemInsideSecurityHeaderEncrypted;
						}
						if (!flag)
						{
							this.message.ToString(xmlDictionaryWriter);
						}
						else
						{
							if (this.message.Version.Envelope != EnvelopeVersion.None)
							{
								xmlDictionaryWriter.WriteStartElement(XD.MessageDictionary.Prefix.Value, XD.MessageDictionary.Envelope, this.message.Version.Envelope.DictionaryNamespace);
								this.WriteHeader(xmlDictionaryWriter);
								this.message.WriteStartBody(writer);
							}
							this.message.BodyToString(xmlDictionaryWriter);
							if (this.message.Version.Envelope != EnvelopeVersion.None)
							{
								writer.WriteEndElement();
								xmlDictionaryWriter.WriteEndElement();
							}
						}
					}
				}
				else if (this.message.Version.Envelope != EnvelopeVersion.None)
				{
					if (this.reader != null)
					{
						xmlDictionaryWriter.WriteStartElement(this.reader.Prefix, this.reader.LocalName, this.reader.NamespaceURI);
						this.reader.Read();
						if (string.CompareOrdinal(this.reader.LocalName, "Header") == 0)
						{
							xmlDictionaryWriter.WriteNode(this.reader, true);
						}
						xmlDictionaryWriter.WriteEndElement();
					}
					else
					{
						xmlDictionaryWriter.WriteStartElement(XD.MessageDictionary.Prefix.Value, XD.MessageDictionary.Envelope, this.message.Version.Envelope.DictionaryNamespace);
						this.WriteHeader(xmlDictionaryWriter);
						xmlDictionaryWriter.WriteEndElement();
					}
				}
				if (this.reader != null)
				{
					this.reader.Close();
					this.reader = null;
				}
			}
			else
			{
				writer.WriteCData(this.messageString);
			}
			writer.WriteEndElement();
		}

		// Token: 0x06006A2F RID: 27183 RVA: 0x0018BDE4 File Offset: 0x00189FE4
		private void WriteHeader(XmlDictionaryWriter dictionaryWriter)
		{
			dictionaryWriter.WriteStartElement(XD.MessageDictionary.Prefix.Value, XD.MessageDictionary.Header, this.message.Version.Envelope.DictionaryNamespace);
			MessageHeaders headers = this.message.Headers;
			ReceiveSecurityHeader receiveSecurityHeader = null;
			if (this.message is SecurityVerifiedMessage)
			{
				SecurityVerifiedMessage securityVerifiedMessage = this.message as SecurityVerifiedMessage;
				receiveSecurityHeader = securityVerifiedMessage.ReceivedSecurityHeader;
			}
			for (int i = 0; i < headers.Count; i++)
			{
				if (receiveSecurityHeader != null && receiveSecurityHeader.HasAtLeastOneItemInsideSecurityHeaderEncrypted && receiveSecurityHeader.HeaderIndex == i)
				{
					receiveSecurityHeader.WriteStartHeader(dictionaryWriter, headers.MessageVersion);
					receiveSecurityHeader.WriteHeaderContents(dictionaryWriter, headers.MessageVersion);
					dictionaryWriter.WriteEndElement();
				}
				else
				{
					headers.WriteHeader(i, dictionaryWriter);
				}
			}
			dictionaryWriter.WriteEndElement();
		}

		// Token: 0x06006A30 RID: 27184 RVA: 0x0018BEA8 File Offset: 0x0018A0A8
		private void WriteAddressingProperties(XmlWriter dictionaryWriter)
		{
			object obj;
			if (this.message.Properties.TryGetValue(AddressingProperty.Name, out obj))
			{
				AddressingProperty addressingProperty = (AddressingProperty)obj;
				dictionaryWriter.WriteStartElement("Addressing");
				dictionaryWriter.WriteElementString("Action", addressingProperty.Action);
				if (null != addressingProperty.ReplyTo)
				{
					dictionaryWriter.WriteElementString("ReplyTo", addressingProperty.ReplyTo.ToString());
				}
				if (null != addressingProperty.To)
				{
					dictionaryWriter.WriteElementString("To", addressingProperty.To.AbsoluteUri);
				}
				if (null != addressingProperty.MessageId)
				{
					dictionaryWriter.WriteElementString("MessageID", addressingProperty.MessageId.ToString());
				}
				dictionaryWriter.WriteEndElement();
				this.message.Properties.Remove(AddressingProperty.Name);
			}
		}

		// Token: 0x06006A31 RID: 27185 RVA: 0x0018BF7C File Offset: 0x0018A17C
		private void WriteHttpProperties(XmlWriter dictionaryWriter)
		{
			object obj;
			if (this.message.Properties.TryGetValue(HttpResponseMessageProperty.Name, out obj))
			{
				HttpResponseMessageProperty httpResponseMessageProperty = (HttpResponseMessageProperty)obj;
				dictionaryWriter.WriteStartElement("HttpResponse");
				dictionaryWriter.WriteElementString("StatusCode", httpResponseMessageProperty.StatusCode.ToString());
				if (httpResponseMessageProperty.StatusDescription != null)
				{
					dictionaryWriter.WriteElementString("StatusDescription", httpResponseMessageProperty.StatusDescription);
				}
				dictionaryWriter.WriteStartElement("WebHeaders");
				WebHeaderCollection headers = httpResponseMessageProperty.Headers;
				for (int i = 0; i < headers.Count; i++)
				{
					string localName = headers.Keys[i];
					string value = headers[i];
					dictionaryWriter.WriteElementString(localName, value);
				}
				dictionaryWriter.WriteEndElement();
				dictionaryWriter.WriteEndElement();
			}
			if (this.message.Properties.TryGetValue(HttpRequestMessageProperty.Name, out obj))
			{
				HttpRequestMessageProperty httpRequestMessageProperty = (HttpRequestMessageProperty)obj;
				dictionaryWriter.WriteStartElement("HttpRequest");
				dictionaryWriter.WriteElementString("Method", httpRequestMessageProperty.Method);
				dictionaryWriter.WriteElementString("QueryString", httpRequestMessageProperty.QueryString);
				dictionaryWriter.WriteStartElement("WebHeaders");
				WebHeaderCollection headers2 = httpRequestMessageProperty.Headers;
				for (int j = 0; j < headers2.Count; j++)
				{
					string localName2 = headers2.Keys[j];
					string value2 = headers2[j];
					dictionaryWriter.WriteElementString(localName2, value2);
				}
				dictionaryWriter.WriteEndElement();
				dictionaryWriter.WriteEndElement();
			}
		}

		// Token: 0x04003C8C RID: 15500
		internal const string AddressingElementName = "Addressing";

		// Token: 0x04003C8D RID: 15501
		internal const string BodyElementName = "Body";

		// Token: 0x04003C8E RID: 15502
		internal const string HttpRequestMessagePropertyElementName = "HttpRequest";

		// Token: 0x04003C8F RID: 15503
		internal const string HttpResponseMessagePropertyElementName = "HttpResponse";

		// Token: 0x04003C90 RID: 15504
		internal const string NamespaceUri = "http://schemas.microsoft.com/2004/06/ServiceModel/Management/MessageTrace";

		// Token: 0x04003C91 RID: 15505
		internal const string NamespacePrefix = "";

		// Token: 0x04003C92 RID: 15506
		internal const string MessageHeaderElementName = "Header";

		// Token: 0x04003C93 RID: 15507
		internal const string MessageHeadersElementName = "MessageHeaders";

		// Token: 0x04003C94 RID: 15508
		internal const string MessageLogTraceRecordElementName = "MessageLogTraceRecord";

		// Token: 0x04003C95 RID: 15509
		internal const string MethodElementName = "Method";

		// Token: 0x04003C96 RID: 15510
		internal const string QueryStringElementName = "QueryString";

		// Token: 0x04003C97 RID: 15511
		internal const string StatusCodeElementName = "StatusCode";

		// Token: 0x04003C98 RID: 15512
		internal const string StatusDescriptionElementName = "StatusDescription";

		// Token: 0x04003C99 RID: 15513
		internal const string TraceTimeAttributeName = "Time";

		// Token: 0x04003C9A RID: 15514
		internal const string TypeElementName = "Type";

		// Token: 0x04003C9B RID: 15515
		internal const string WebHeadersElementName = "WebHeaders";

		// Token: 0x04003C9C RID: 15516
		private Message message;

		// Token: 0x04003C9D RID: 15517
		private XmlReader reader;

		// Token: 0x04003C9E RID: 15518
		private string messageString;

		// Token: 0x04003C9F RID: 15519
		private DateTime timestamp;

		// Token: 0x04003CA0 RID: 15520
		private bool logMessageBody = true;

		// Token: 0x04003CA1 RID: 15521
		private MessageLoggingSource source;

		// Token: 0x04003CA2 RID: 15522
		private Type type;
	}
}
