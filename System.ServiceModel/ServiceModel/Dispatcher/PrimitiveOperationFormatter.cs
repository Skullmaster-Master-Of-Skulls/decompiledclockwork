using System;
using System.Runtime.Serialization;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Diagnostics;
using System.Xml;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000593 RID: 1427
	internal class PrimitiveOperationFormatter : IClientMessageFormatter, IDispatchMessageFormatter
	{
		// Token: 0x0600371C RID: 14108 RVA: 0x000D42D0 File Offset: 0x000D24D0
		public PrimitiveOperationFormatter(OperationDescription description, bool isRpc)
		{
			if (description == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("description");
			}
			OperationFormatter.Validate(description, isRpc, false);
			this.operation = description;
			this.requestMessage = description.Messages[0];
			if (description.Messages.Count == 2)
			{
				this.responseMessage = description.Messages[1];
			}
			int num = 3 + this.requestMessage.Body.Parts.Count;
			if (this.responseMessage != null)
			{
				num += 2 + this.responseMessage.Body.Parts.Count;
			}
			XmlDictionary xmlDictionary = new XmlDictionary(num * 2);
			this.xsiNilLocalName = xmlDictionary.Add("nil");
			this.xsiNilNamespace = xmlDictionary.Add("http://www.w3.org/2001/XMLSchema-instance");
			OperationFormatter.GetActions(description, xmlDictionary, out this.action, out this.replyAction);
			if (this.requestMessage.Body.WrapperName != null)
			{
				this.requestWrapperName = PrimitiveOperationFormatter.AddToDictionary(xmlDictionary, this.requestMessage.Body.WrapperName);
				this.requestWrapperNamespace = PrimitiveOperationFormatter.AddToDictionary(xmlDictionary, this.requestMessage.Body.WrapperNamespace);
			}
			this.requestParts = PrimitiveOperationFormatter.AddToDictionary(xmlDictionary, this.requestMessage.Body.Parts, isRpc);
			if (this.responseMessage != null)
			{
				if (this.responseMessage.Body.WrapperName != null)
				{
					this.responseWrapperName = PrimitiveOperationFormatter.AddToDictionary(xmlDictionary, this.responseMessage.Body.WrapperName);
					this.responseWrapperNamespace = PrimitiveOperationFormatter.AddToDictionary(xmlDictionary, this.responseMessage.Body.WrapperNamespace);
				}
				this.responseParts = PrimitiveOperationFormatter.AddToDictionary(xmlDictionary, this.responseMessage.Body.Parts, isRpc);
				if (this.responseMessage.Body.ReturnValue != null && this.responseMessage.Body.ReturnValue.Type != typeof(void))
				{
					this.returnPart = PrimitiveOperationFormatter.AddToDictionary(xmlDictionary, this.responseMessage.Body.ReturnValue, isRpc);
				}
			}
		}

		// Token: 0x17000D10 RID: 3344
		// (get) Token: 0x0600371D RID: 14109 RVA: 0x000D44DD File Offset: 0x000D26DD
		private ActionHeader ActionHeaderNone
		{
			get
			{
				if (this.actionHeaderNone == null)
				{
					this.actionHeaderNone = ActionHeader.Create(this.action, AddressingVersion.None);
				}
				return this.actionHeaderNone;
			}
		}

		// Token: 0x17000D11 RID: 3345
		// (get) Token: 0x0600371E RID: 14110 RVA: 0x000D4503 File Offset: 0x000D2703
		private ActionHeader ActionHeader10
		{
			get
			{
				if (this.actionHeader10 == null)
				{
					this.actionHeader10 = ActionHeader.Create(this.action, AddressingVersion.WSAddressing10);
				}
				return this.actionHeader10;
			}
		}

		// Token: 0x17000D12 RID: 3346
		// (get) Token: 0x0600371F RID: 14111 RVA: 0x000D4529 File Offset: 0x000D2729
		private ActionHeader ActionHeaderAugust2004
		{
			get
			{
				if (this.actionHeaderAugust2004 == null)
				{
					this.actionHeaderAugust2004 = ActionHeader.Create(this.action, AddressingVersion.WSAddressingAugust2004);
				}
				return this.actionHeaderAugust2004;
			}
		}

		// Token: 0x17000D13 RID: 3347
		// (get) Token: 0x06003720 RID: 14112 RVA: 0x000D454F File Offset: 0x000D274F
		private ActionHeader ReplyActionHeaderNone
		{
			get
			{
				if (this.replyActionHeaderNone == null)
				{
					this.replyActionHeaderNone = ActionHeader.Create(this.replyAction, AddressingVersion.None);
				}
				return this.replyActionHeaderNone;
			}
		}

		// Token: 0x17000D14 RID: 3348
		// (get) Token: 0x06003721 RID: 14113 RVA: 0x000D4575 File Offset: 0x000D2775
		private ActionHeader ReplyActionHeader10
		{
			get
			{
				if (this.replyActionHeader10 == null)
				{
					this.replyActionHeader10 = ActionHeader.Create(this.replyAction, AddressingVersion.WSAddressing10);
				}
				return this.replyActionHeader10;
			}
		}

		// Token: 0x17000D15 RID: 3349
		// (get) Token: 0x06003722 RID: 14114 RVA: 0x000D459B File Offset: 0x000D279B
		private ActionHeader ReplyActionHeaderAugust2004
		{
			get
			{
				if (this.replyActionHeaderAugust2004 == null)
				{
					this.replyActionHeaderAugust2004 = ActionHeader.Create(this.replyAction, AddressingVersion.WSAddressingAugust2004);
				}
				return this.replyActionHeaderAugust2004;
			}
		}

		// Token: 0x06003723 RID: 14115 RVA: 0x000D45C4 File Offset: 0x000D27C4
		private static XmlDictionaryString AddToDictionary(XmlDictionary dictionary, string s)
		{
			XmlDictionaryString result;
			if (!dictionary.TryLookup(s, out result))
			{
				result = dictionary.Add(s);
			}
			return result;
		}

		// Token: 0x06003724 RID: 14116 RVA: 0x000D45E8 File Offset: 0x000D27E8
		private static PrimitiveOperationFormatter.PartInfo[] AddToDictionary(XmlDictionary dictionary, MessagePartDescriptionCollection parts, bool isRpc)
		{
			PrimitiveOperationFormatter.PartInfo[] array = new PrimitiveOperationFormatter.PartInfo[parts.Count];
			for (int i = 0; i < parts.Count; i++)
			{
				array[i] = PrimitiveOperationFormatter.AddToDictionary(dictionary, parts[i], isRpc);
			}
			return array;
		}

		// Token: 0x06003725 RID: 14117 RVA: 0x000D4624 File Offset: 0x000D2824
		private ActionHeader GetActionHeader(AddressingVersion addressing)
		{
			if (this.action == null)
			{
				return null;
			}
			if (addressing == AddressingVersion.WSAddressingAugust2004)
			{
				return this.ActionHeaderAugust2004;
			}
			if (addressing == AddressingVersion.WSAddressing10)
			{
				return this.ActionHeader10;
			}
			if (addressing == AddressingVersion.None)
			{
				return this.ActionHeaderNone;
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("AddressingVersionNotSupported", new object[]
			{
				addressing
			})));
		}

		// Token: 0x06003726 RID: 14118 RVA: 0x000D468C File Offset: 0x000D288C
		private ActionHeader GetReplyActionHeader(AddressingVersion addressing)
		{
			if (this.replyAction == null)
			{
				return null;
			}
			if (addressing == AddressingVersion.WSAddressingAugust2004)
			{
				return this.ReplyActionHeaderAugust2004;
			}
			if (addressing == AddressingVersion.WSAddressing10)
			{
				return this.ReplyActionHeader10;
			}
			if (addressing == AddressingVersion.None)
			{
				return this.ReplyActionHeaderNone;
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("AddressingVersionNotSupported", new object[]
			{
				addressing
			})));
		}

		// Token: 0x06003727 RID: 14119 RVA: 0x000D46F4 File Offset: 0x000D28F4
		private static string GetArrayItemName(Type type)
		{
			TypeCode typeCode = Type.GetTypeCode(type);
			if (typeCode != TypeCode.Boolean)
			{
				switch (typeCode)
				{
				case TypeCode.Int32:
					return "int";
				case TypeCode.Int64:
					return "long";
				case TypeCode.Single:
					return "float";
				case TypeCode.Double:
					return "double";
				case TypeCode.Decimal:
					return "decimal";
				case TypeCode.DateTime:
					return "dateTime";
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxInvalidUseOfPrimitiveOperationFormatter")));
			}
			return "boolean";
		}

		// Token: 0x06003728 RID: 14120 RVA: 0x000D477C File Offset: 0x000D297C
		private static PrimitiveOperationFormatter.PartInfo AddToDictionary(XmlDictionary dictionary, MessagePartDescription part, bool isRpc)
		{
			Type type = part.Type;
			XmlDictionaryString itemName = null;
			XmlDictionaryString itemNamespace = null;
			if (type.IsArray && type != typeof(byte[]))
			{
				string arrayItemName = PrimitiveOperationFormatter.GetArrayItemName(type.GetElementType());
				itemName = PrimitiveOperationFormatter.AddToDictionary(dictionary, arrayItemName);
				itemNamespace = PrimitiveOperationFormatter.AddToDictionary(dictionary, "http://schemas.microsoft.com/2003/10/Serialization/Arrays");
			}
			return new PrimitiveOperationFormatter.PartInfo(part, PrimitiveOperationFormatter.AddToDictionary(dictionary, part.Name), PrimitiveOperationFormatter.AddToDictionary(dictionary, isRpc ? string.Empty : part.Namespace), itemName, itemNamespace);
		}

		// Token: 0x06003729 RID: 14121 RVA: 0x000D47F8 File Offset: 0x000D29F8
		public static bool IsContractSupported(OperationDescription description)
		{
			if (description == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("description");
			}
			MessageDescription messageDescription = description.Messages[0];
			MessageDescription messageDescription2 = null;
			if (description.Messages.Count == 2)
			{
				messageDescription2 = description.Messages[1];
			}
			if (messageDescription.Headers.Count > 0)
			{
				return false;
			}
			if (messageDescription.Properties.Count > 0)
			{
				return false;
			}
			if (messageDescription.IsTypedMessage)
			{
				return false;
			}
			if (messageDescription2 != null)
			{
				if (messageDescription2.Headers.Count > 0)
				{
					return false;
				}
				if (messageDescription2.Properties.Count > 0)
				{
					return false;
				}
				if (messageDescription2.IsTypedMessage)
				{
					return false;
				}
			}
			if (!PrimitiveOperationFormatter.AreTypesSupported(messageDescription.Body.Parts))
			{
				return false;
			}
			if (messageDescription2 != null)
			{
				if (!PrimitiveOperationFormatter.AreTypesSupported(messageDescription2.Body.Parts))
				{
					return false;
				}
				if (messageDescription2.Body.ReturnValue != null && !PrimitiveOperationFormatter.IsTypeSupported(messageDescription2.Body.ReturnValue))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600372A RID: 14122 RVA: 0x000D48E8 File Offset: 0x000D2AE8
		private static bool AreTypesSupported(MessagePartDescriptionCollection bodyDescriptions)
		{
			for (int i = 0; i < bodyDescriptions.Count; i++)
			{
				if (!PrimitiveOperationFormatter.IsTypeSupported(bodyDescriptions[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600372B RID: 14123 RVA: 0x000D4918 File Offset: 0x000D2B18
		private static bool IsTypeSupported(MessagePartDescription bodyDescription)
		{
			Type type = bodyDescription.Type;
			if (type == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxMessagePartDescriptionMissingType", new object[]
				{
					bodyDescription.Name,
					bodyDescription.Namespace
				})));
			}
			if (bodyDescription.Multiple)
			{
				return false;
			}
			if (type == typeof(void))
			{
				return true;
			}
			if (type.IsEnum)
			{
				return false;
			}
			TypeCode typeCode = Type.GetTypeCode(type);
			if (typeCode != TypeCode.Object)
			{
				if (typeCode != TypeCode.Boolean)
				{
					switch (typeCode)
					{
					case TypeCode.Int32:
					case TypeCode.Int64:
					case TypeCode.Single:
					case TypeCode.Double:
					case TypeCode.Decimal:
					case TypeCode.DateTime:
					case TypeCode.String:
						break;
					case TypeCode.UInt32:
					case TypeCode.UInt64:
					case (TypeCode)17:
						return false;
					default:
						return false;
					}
				}
				return true;
			}
			if (type.IsArray && type.GetArrayRank() == 1 && PrimitiveOperationFormatter.IsArrayTypeSupported(type.GetElementType()))
			{
				return true;
			}
			return false;
		}

		// Token: 0x0600372C RID: 14124 RVA: 0x000D49F4 File Offset: 0x000D2BF4
		private static bool IsArrayTypeSupported(Type type)
		{
			if (type.IsEnum)
			{
				return false;
			}
			switch (Type.GetTypeCode(type))
			{
			case TypeCode.Boolean:
			case TypeCode.Byte:
			case TypeCode.Int32:
			case TypeCode.Int64:
			case TypeCode.Single:
			case TypeCode.Double:
			case TypeCode.Decimal:
			case TypeCode.DateTime:
				return true;
			}
			return false;
		}

		// Token: 0x0600372D RID: 14125 RVA: 0x000D4A58 File Offset: 0x000D2C58
		public Message SerializeRequest(MessageVersion messageVersion, object[] parameters)
		{
			if (messageVersion == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("messageVersion");
			}
			if (parameters == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("parameters");
			}
			return Message.CreateMessage(messageVersion, this.GetActionHeader(messageVersion.Addressing), new PrimitiveOperationFormatter.PrimitiveRequestBodyWriter(parameters, this));
		}

		// Token: 0x0600372E RID: 14126 RVA: 0x000D4AA4 File Offset: 0x000D2CA4
		public Message SerializeReply(MessageVersion messageVersion, object[] parameters, object result)
		{
			if (messageVersion == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("messageVersion");
			}
			if (parameters == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("parameters");
			}
			return Message.CreateMessage(messageVersion, this.GetReplyActionHeader(messageVersion.Addressing), new PrimitiveOperationFormatter.PrimitiveResponseBodyWriter(parameters, result, this));
		}

		// Token: 0x0600372F RID: 14127 RVA: 0x000D4AF4 File Offset: 0x000D2CF4
		public object DeserializeReply(Message message, object[] parameters)
		{
			if (message == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("message"));
			}
			if (parameters == null)
			{
				throw TraceUtility.ThrowHelperError(new ArgumentNullException("parameters"), message);
			}
			object result;
			try
			{
				if (message.IsEmpty)
				{
					if (this.responseWrapperName != null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SerializationException(SR.GetString("SFxInvalidMessageBodyEmptyMessage")));
					}
					result = null;
				}
				else
				{
					XmlDictionaryReader readerAtBodyContents = message.GetReaderAtBodyContents();
					using (readerAtBodyContents)
					{
						object obj = this.DeserializeResponse(readerAtBodyContents, parameters);
						message.ReadFromBodyContentsToEnd(readerAtBodyContents);
						result = obj;
					}
				}
			}
			catch (XmlException ex)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CommunicationException(SR.GetString("SFxErrorDeserializingReplyBodyMore", new object[]
				{
					this.operation.Name,
					ex.Message
				}), ex));
			}
			catch (FormatException ex2)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CommunicationException(SR.GetString("SFxErrorDeserializingReplyBodyMore", new object[]
				{
					this.operation.Name,
					ex2.Message
				}), ex2));
			}
			catch (SerializationException ex3)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CommunicationException(SR.GetString("SFxErrorDeserializingReplyBodyMore", new object[]
				{
					this.operation.Name,
					ex3.Message
				}), ex3));
			}
			return result;
		}

		// Token: 0x06003730 RID: 14128 RVA: 0x000D4C74 File Offset: 0x000D2E74
		public void DeserializeRequest(Message message, object[] parameters)
		{
			if (message == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("message"));
			}
			if (parameters == null)
			{
				throw TraceUtility.ThrowHelperError(new ArgumentNullException("parameters"), message);
			}
			try
			{
				if (message.IsEmpty)
				{
					if (this.requestWrapperName != null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SerializationException(SR.GetString("SFxInvalidMessageBodyEmptyMessage")));
					}
				}
				else
				{
					XmlDictionaryReader readerAtBodyContents = message.GetReaderAtBodyContents();
					using (readerAtBodyContents)
					{
						this.DeserializeRequest(readerAtBodyContents, parameters);
						message.ReadFromBodyContentsToEnd(readerAtBodyContents);
					}
				}
			}
			catch (XmlException ex)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(OperationFormatter.CreateDeserializationFailedFault(SR.GetString("SFxErrorDeserializingRequestBodyMore", new object[]
				{
					this.operation.Name,
					ex.Message
				}), ex));
			}
			catch (FormatException ex2)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(OperationFormatter.CreateDeserializationFailedFault(SR.GetString("SFxErrorDeserializingRequestBodyMore", new object[]
				{
					this.operation.Name,
					ex2.Message
				}), ex2));
			}
			catch (SerializationException ex3)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CommunicationException(SR.GetString("SFxErrorDeserializingRequestBodyMore", new object[]
				{
					this.operation.Name,
					ex3.Message
				}), ex3));
			}
		}

		// Token: 0x06003731 RID: 14129 RVA: 0x000D4DE8 File Offset: 0x000D2FE8
		private void DeserializeRequest(XmlDictionaryReader reader, object[] parameters)
		{
			if (this.requestWrapperName != null)
			{
				if (!reader.IsStartElement(this.requestWrapperName, this.requestWrapperNamespace))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SerializationException(SR.GetString("SFxInvalidMessageBody", new object[]
					{
						this.requestWrapperName,
						this.requestWrapperNamespace,
						reader.NodeType,
						reader.Name,
						reader.NamespaceURI
					})));
				}
				bool isEmptyElement = reader.IsEmptyElement;
				reader.Read();
				if (isEmptyElement)
				{
					return;
				}
			}
			this.DeserializeParameters(reader, this.requestParts, parameters);
			if (this.requestWrapperName != null)
			{
				reader.ReadEndElement();
			}
		}

		// Token: 0x06003732 RID: 14130 RVA: 0x000D4E94 File Offset: 0x000D3094
		private object DeserializeResponse(XmlDictionaryReader reader, object[] parameters)
		{
			if (this.responseWrapperName != null)
			{
				if (!reader.IsStartElement(this.responseWrapperName, this.responseWrapperNamespace))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SerializationException(SR.GetString("SFxInvalidMessageBody", new object[]
					{
						this.responseWrapperName,
						this.responseWrapperNamespace,
						reader.NodeType,
						reader.Name,
						reader.NamespaceURI
					})));
				}
				bool isEmptyElement = reader.IsEmptyElement;
				reader.Read();
				if (isEmptyElement)
				{
					return null;
				}
			}
			object result = null;
			if (this.returnPart != null)
			{
				while (!this.IsPartElement(reader, this.returnPart))
				{
					if (!reader.IsStartElement() || this.IsPartElements(reader, this.responseParts))
					{
						goto IL_C9;
					}
					OperationFormatter.TraceAndSkipElement(reader);
				}
				result = this.DeserializeParameter(reader, this.returnPart);
			}
			IL_C9:
			this.DeserializeParameters(reader, this.responseParts, parameters);
			if (this.responseWrapperName != null)
			{
				reader.ReadEndElement();
			}
			return result;
		}

		// Token: 0x06003733 RID: 14131 RVA: 0x000D4F88 File Offset: 0x000D3188
		private void DeserializeParameters(XmlDictionaryReader reader, PrimitiveOperationFormatter.PartInfo[] parts, object[] parameters)
		{
			if (parts.Length != parameters.Length)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("SFxParameterCountMismatch", new object[]
				{
					"parts",
					parts.Length,
					"parameters",
					parameters.Length
				}), "parameters"));
			}
			int num = 0;
			while (reader.IsStartElement())
			{
				for (int i = num; i < parts.Length; i++)
				{
					PrimitiveOperationFormatter.PartInfo partInfo = parts[i];
					if (this.IsPartElement(reader, partInfo))
					{
						parameters[partInfo.Description.Index] = this.DeserializeParameter(reader, parts[i]);
						num = i + 1;
					}
					else
					{
						parameters[partInfo.Description.Index] = null;
					}
				}
				if (reader.IsStartElement())
				{
					OperationFormatter.TraceAndSkipElement(reader);
				}
			}
		}

		// Token: 0x06003734 RID: 14132 RVA: 0x000D504C File Offset: 0x000D324C
		private bool IsPartElements(XmlDictionaryReader reader, PrimitiveOperationFormatter.PartInfo[] parts)
		{
			foreach (PrimitiveOperationFormatter.PartInfo part in parts)
			{
				if (this.IsPartElement(reader, part))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003735 RID: 14133 RVA: 0x000D507A File Offset: 0x000D327A
		private bool IsPartElement(XmlDictionaryReader reader, PrimitiveOperationFormatter.PartInfo part)
		{
			return reader.IsStartElement(part.DictionaryName, part.DictionaryNamespace);
		}

		// Token: 0x06003736 RID: 14134 RVA: 0x000D508E File Offset: 0x000D328E
		private object DeserializeParameter(XmlDictionaryReader reader, PrimitiveOperationFormatter.PartInfo part)
		{
			if (reader.AttributeCount > 0 && reader.MoveToAttribute(this.xsiNilLocalName.Value, this.xsiNilNamespace.Value) && reader.ReadContentAsBoolean())
			{
				reader.Skip();
				return null;
			}
			return part.ReadValue(reader);
		}

		// Token: 0x06003737 RID: 14135 RVA: 0x000D50D0 File Offset: 0x000D32D0
		private void SerializeParameter(XmlDictionaryWriter writer, PrimitiveOperationFormatter.PartInfo part, object graph)
		{
			writer.WriteStartElement(part.DictionaryName, part.DictionaryNamespace);
			if (graph == null)
			{
				writer.WriteStartAttribute(this.xsiNilLocalName, this.xsiNilNamespace);
				writer.WriteValue(true);
				writer.WriteEndAttribute();
			}
			else
			{
				part.WriteValue(writer, graph);
			}
			writer.WriteEndElement();
		}

		// Token: 0x06003738 RID: 14136 RVA: 0x000D5124 File Offset: 0x000D3324
		private void SerializeParameters(XmlDictionaryWriter writer, PrimitiveOperationFormatter.PartInfo[] parts, object[] parameters)
		{
			if (parts.Length != parameters.Length)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("SFxParameterCountMismatch", new object[]
				{
					"parts",
					parts.Length,
					"parameters",
					parameters.Length
				}), "parameters"));
			}
			foreach (PrimitiveOperationFormatter.PartInfo partInfo in parts)
			{
				this.SerializeParameter(writer, partInfo, parameters[partInfo.Description.Index]);
			}
		}

		// Token: 0x06003739 RID: 14137 RVA: 0x000D51AB File Offset: 0x000D33AB
		private void SerializeRequest(XmlDictionaryWriter writer, object[] parameters)
		{
			if (this.requestWrapperName != null)
			{
				writer.WriteStartElement(this.requestWrapperName, this.requestWrapperNamespace);
			}
			this.SerializeParameters(writer, this.requestParts, parameters);
			if (this.requestWrapperName != null)
			{
				writer.WriteEndElement();
			}
		}

		// Token: 0x0600373A RID: 14138 RVA: 0x000D51E4 File Offset: 0x000D33E4
		private void SerializeResponse(XmlDictionaryWriter writer, object returnValue, object[] parameters)
		{
			if (this.responseWrapperName != null)
			{
				writer.WriteStartElement(this.responseWrapperName, this.responseWrapperNamespace);
			}
			if (this.returnPart != null)
			{
				this.SerializeParameter(writer, this.returnPart, returnValue);
			}
			this.SerializeParameters(writer, this.responseParts, parameters);
			if (this.responseWrapperName != null)
			{
				writer.WriteEndElement();
			}
		}

		// Token: 0x04002901 RID: 10497
		private OperationDescription operation;

		// Token: 0x04002902 RID: 10498
		private MessageDescription responseMessage;

		// Token: 0x04002903 RID: 10499
		private MessageDescription requestMessage;

		// Token: 0x04002904 RID: 10500
		private XmlDictionaryString action;

		// Token: 0x04002905 RID: 10501
		private XmlDictionaryString replyAction;

		// Token: 0x04002906 RID: 10502
		private ActionHeader actionHeaderNone;

		// Token: 0x04002907 RID: 10503
		private ActionHeader actionHeader10;

		// Token: 0x04002908 RID: 10504
		private ActionHeader actionHeaderAugust2004;

		// Token: 0x04002909 RID: 10505
		private ActionHeader replyActionHeaderNone;

		// Token: 0x0400290A RID: 10506
		private ActionHeader replyActionHeader10;

		// Token: 0x0400290B RID: 10507
		private ActionHeader replyActionHeaderAugust2004;

		// Token: 0x0400290C RID: 10508
		private XmlDictionaryString requestWrapperName;

		// Token: 0x0400290D RID: 10509
		private XmlDictionaryString requestWrapperNamespace;

		// Token: 0x0400290E RID: 10510
		private XmlDictionaryString responseWrapperName;

		// Token: 0x0400290F RID: 10511
		private XmlDictionaryString responseWrapperNamespace;

		// Token: 0x04002910 RID: 10512
		private PrimitiveOperationFormatter.PartInfo[] requestParts;

		// Token: 0x04002911 RID: 10513
		private PrimitiveOperationFormatter.PartInfo[] responseParts;

		// Token: 0x04002912 RID: 10514
		private PrimitiveOperationFormatter.PartInfo returnPart;

		// Token: 0x04002913 RID: 10515
		private XmlDictionaryString xsiNilLocalName;

		// Token: 0x04002914 RID: 10516
		private XmlDictionaryString xsiNilNamespace;

		// Token: 0x02000C9E RID: 3230
		private class PartInfo
		{
			// Token: 0x0600790E RID: 30990 RVA: 0x001C3BDC File Offset: 0x001C1DDC
			public PartInfo(MessagePartDescription description, XmlDictionaryString dictionaryName, XmlDictionaryString dictionaryNamespace, XmlDictionaryString itemName, XmlDictionaryString itemNamespace)
			{
				this.dictionaryName = dictionaryName;
				this.dictionaryNamespace = dictionaryNamespace;
				this.itemName = itemName;
				this.itemNamespace = itemNamespace;
				this.description = description;
				if (description.Type.IsArray)
				{
					this.isArray = true;
					this.typeCode = Type.GetTypeCode(description.Type.GetElementType());
					return;
				}
				this.isArray = false;
				this.typeCode = Type.GetTypeCode(description.Type);
			}

			// Token: 0x17001B7F RID: 7039
			// (get) Token: 0x0600790F RID: 30991 RVA: 0x001C3C57 File Offset: 0x001C1E57
			public MessagePartDescription Description
			{
				get
				{
					return this.description;
				}
			}

			// Token: 0x17001B80 RID: 7040
			// (get) Token: 0x06007910 RID: 30992 RVA: 0x001C3C5F File Offset: 0x001C1E5F
			public XmlDictionaryString DictionaryName
			{
				get
				{
					return this.dictionaryName;
				}
			}

			// Token: 0x17001B81 RID: 7041
			// (get) Token: 0x06007911 RID: 30993 RVA: 0x001C3C67 File Offset: 0x001C1E67
			public XmlDictionaryString DictionaryNamespace
			{
				get
				{
					return this.dictionaryNamespace;
				}
			}

			// Token: 0x06007912 RID: 30994 RVA: 0x001C3C70 File Offset: 0x001C1E70
			public object ReadValue(XmlDictionaryReader reader)
			{
				object result;
				if (this.isArray)
				{
					switch (this.typeCode)
					{
					case TypeCode.Boolean:
						if (!reader.IsEmptyElement)
						{
							reader.ReadStartElement();
							result = reader.ReadBooleanArray(this.itemName, this.itemNamespace);
							reader.ReadEndElement();
							return result;
						}
						reader.Read();
						return new bool[0];
					case TypeCode.Byte:
						return reader.ReadElementContentAsBase64();
					case TypeCode.Int32:
						if (!reader.IsEmptyElement)
						{
							reader.ReadStartElement();
							result = reader.ReadInt32Array(this.itemName, this.itemNamespace);
							reader.ReadEndElement();
							return result;
						}
						reader.Read();
						return new int[0];
					case TypeCode.Int64:
						if (!reader.IsEmptyElement)
						{
							reader.ReadStartElement();
							result = reader.ReadInt64Array(this.itemName, this.itemNamespace);
							reader.ReadEndElement();
							return result;
						}
						reader.Read();
						return new long[0];
					case TypeCode.Single:
						if (!reader.IsEmptyElement)
						{
							reader.ReadStartElement();
							result = reader.ReadSingleArray(this.itemName, this.itemNamespace);
							reader.ReadEndElement();
							return result;
						}
						reader.Read();
						return new float[0];
					case TypeCode.Double:
						if (!reader.IsEmptyElement)
						{
							reader.ReadStartElement();
							result = reader.ReadDoubleArray(this.itemName, this.itemNamespace);
							reader.ReadEndElement();
							return result;
						}
						reader.Read();
						return new double[0];
					case TypeCode.Decimal:
						if (!reader.IsEmptyElement)
						{
							reader.ReadStartElement();
							result = reader.ReadDecimalArray(this.itemName, this.itemNamespace);
							reader.ReadEndElement();
							return result;
						}
						reader.Read();
						return new decimal[0];
					case TypeCode.DateTime:
						if (!reader.IsEmptyElement)
						{
							reader.ReadStartElement();
							result = reader.ReadDateTimeArray(this.itemName, this.itemNamespace);
							reader.ReadEndElement();
							return result;
						}
						reader.Read();
						return new DateTime[0];
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxInvalidUseOfPrimitiveOperationFormatter")));
				}
				TypeCode typeCode = this.typeCode;
				if (typeCode != TypeCode.Boolean)
				{
					switch (typeCode)
					{
					case TypeCode.Int32:
						return reader.ReadElementContentAsInt();
					case TypeCode.Int64:
						return reader.ReadElementContentAsLong();
					case TypeCode.Single:
						return reader.ReadElementContentAsFloat();
					case TypeCode.Double:
						return reader.ReadElementContentAsDouble();
					case TypeCode.Decimal:
						return reader.ReadElementContentAsDecimal();
					case TypeCode.DateTime:
						return reader.ReadElementContentAsDateTime();
					case TypeCode.String:
						return reader.ReadElementContentAsString();
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxInvalidUseOfPrimitiveOperationFormatter")));
				}
				result = reader.ReadElementContentAsBoolean();
				return result;
			}

			// Token: 0x06007913 RID: 30995 RVA: 0x001C3F78 File Offset: 0x001C2178
			public void WriteValue(XmlDictionaryWriter writer, object value)
			{
				if (this.isArray)
				{
					switch (this.typeCode)
					{
					case TypeCode.Boolean:
					{
						bool[] array = (bool[])value;
						writer.WriteArray(null, this.itemName, this.itemNamespace, array, 0, array.Length);
						return;
					}
					case TypeCode.Byte:
					{
						byte[] array2 = (byte[])value;
						writer.WriteBase64(array2, 0, array2.Length);
						return;
					}
					case TypeCode.Int32:
					{
						int[] array3 = (int[])value;
						writer.WriteArray(null, this.itemName, this.itemNamespace, array3, 0, array3.Length);
						return;
					}
					case TypeCode.Int64:
					{
						long[] array4 = (long[])value;
						writer.WriteArray(null, this.itemName, this.itemNamespace, array4, 0, array4.Length);
						return;
					}
					case TypeCode.Single:
					{
						float[] array5 = (float[])value;
						writer.WriteArray(null, this.itemName, this.itemNamespace, array5, 0, array5.Length);
						return;
					}
					case TypeCode.Double:
					{
						double[] array6 = (double[])value;
						writer.WriteArray(null, this.itemName, this.itemNamespace, array6, 0, array6.Length);
						return;
					}
					case TypeCode.Decimal:
					{
						decimal[] array7 = (decimal[])value;
						writer.WriteArray(null, this.itemName, this.itemNamespace, array7, 0, array7.Length);
						return;
					}
					case TypeCode.DateTime:
					{
						DateTime[] array8 = (DateTime[])value;
						writer.WriteArray(null, this.itemName, this.itemNamespace, array8, 0, array8.Length);
						return;
					}
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxInvalidUseOfPrimitiveOperationFormatter")));
				}
				TypeCode typeCode = this.typeCode;
				if (typeCode != TypeCode.Boolean)
				{
					switch (typeCode)
					{
					case TypeCode.Int32:
						writer.WriteValue((int)value);
						return;
					case TypeCode.Int64:
						writer.WriteValue((long)value);
						return;
					case TypeCode.Single:
						writer.WriteValue((float)value);
						return;
					case TypeCode.Double:
						writer.WriteValue((double)value);
						return;
					case TypeCode.Decimal:
						writer.WriteValue((decimal)value);
						return;
					case TypeCode.DateTime:
						writer.WriteValue((DateTime)value);
						return;
					case TypeCode.String:
						writer.WriteString((string)value);
						return;
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxInvalidUseOfPrimitiveOperationFormatter")));
				}
				writer.WriteValue((bool)value);
			}

			// Token: 0x040044E6 RID: 17638
			private XmlDictionaryString dictionaryName;

			// Token: 0x040044E7 RID: 17639
			private XmlDictionaryString dictionaryNamespace;

			// Token: 0x040044E8 RID: 17640
			private XmlDictionaryString itemName;

			// Token: 0x040044E9 RID: 17641
			private XmlDictionaryString itemNamespace;

			// Token: 0x040044EA RID: 17642
			private MessagePartDescription description;

			// Token: 0x040044EB RID: 17643
			private TypeCode typeCode;

			// Token: 0x040044EC RID: 17644
			private bool isArray;
		}

		// Token: 0x02000C9F RID: 3231
		private class PrimitiveRequestBodyWriter : BodyWriter
		{
			// Token: 0x06007914 RID: 30996 RVA: 0x001C41BA File Offset: 0x001C23BA
			public PrimitiveRequestBodyWriter(object[] parameters, PrimitiveOperationFormatter primitiveOperationFormatter) : base(true)
			{
				this.parameters = parameters;
				this.primitiveOperationFormatter = primitiveOperationFormatter;
			}

			// Token: 0x06007915 RID: 30997 RVA: 0x001C41D1 File Offset: 0x001C23D1
			protected override void OnWriteBodyContents(XmlDictionaryWriter writer)
			{
				this.primitiveOperationFormatter.SerializeRequest(writer, this.parameters);
			}

			// Token: 0x040044ED RID: 17645
			private object[] parameters;

			// Token: 0x040044EE RID: 17646
			private PrimitiveOperationFormatter primitiveOperationFormatter;
		}

		// Token: 0x02000CA0 RID: 3232
		private class PrimitiveResponseBodyWriter : BodyWriter
		{
			// Token: 0x06007916 RID: 30998 RVA: 0x001C41E5 File Offset: 0x001C23E5
			public PrimitiveResponseBodyWriter(object[] parameters, object returnValue, PrimitiveOperationFormatter primitiveOperationFormatter) : base(true)
			{
				this.parameters = parameters;
				this.returnValue = returnValue;
				this.primitiveOperationFormatter = primitiveOperationFormatter;
			}

			// Token: 0x06007917 RID: 30999 RVA: 0x001C4203 File Offset: 0x001C2403
			protected override void OnWriteBodyContents(XmlDictionaryWriter writer)
			{
				this.primitiveOperationFormatter.SerializeResponse(writer, this.returnValue, this.parameters);
			}

			// Token: 0x040044EF RID: 17647
			private object[] parameters;

			// Token: 0x040044F0 RID: 17648
			private object returnValue;

			// Token: 0x040044F1 RID: 17649
			private PrimitiveOperationFormatter primitiveOperationFormatter;
		}
	}
}
