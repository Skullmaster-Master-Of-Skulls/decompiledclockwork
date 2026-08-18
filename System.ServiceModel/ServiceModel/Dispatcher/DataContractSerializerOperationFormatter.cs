using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Xml;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020005AD RID: 1453
	internal class DataContractSerializerOperationFormatter : OperationFormatter
	{
		// Token: 0x060038AB RID: 14507 RVA: 0x000DA604 File Offset: 0x000D8804
		public DataContractSerializerOperationFormatter(OperationDescription description, DataContractFormatAttribute dataContractFormatAttribute, DataContractSerializerOperationBehavior serializerFactory) : base(description, dataContractFormatAttribute.Style == OperationFormatStyle.Rpc, false)
		{
			if (description == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("description");
			}
			this.serializerFactory = (serializerFactory ?? new DataContractSerializerOperationBehavior(description));
			foreach (Type type in description.KnownTypes)
			{
				if (this.knownTypes == null)
				{
					this.knownTypes = new List<Type>();
				}
				if (type == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxKnownTypeNull", new object[]
					{
						description.Name
					})));
				}
				this.ValidateDataContractType(type);
				this.knownTypes.Add(type);
			}
			this.requestMessageInfo = this.CreateMessageInfo(dataContractFormatAttribute, base.RequestDescription, this.serializerFactory);
			if (base.ReplyDescription != null)
			{
				this.replyMessageInfo = this.CreateMessageInfo(dataContractFormatAttribute, base.ReplyDescription, this.serializerFactory);
			}
		}

		// Token: 0x060038AC RID: 14508 RVA: 0x000DA714 File Offset: 0x000D8914
		private DataContractSerializerOperationFormatter.MessageInfo CreateMessageInfo(DataContractFormatAttribute dataContractFormatAttribute, MessageDescription messageDescription, DataContractSerializerOperationBehavior serializerFactory)
		{
			if (messageDescription.IsUntypedMessage)
			{
				return null;
			}
			DataContractSerializerOperationFormatter.MessageInfo messageInfo = new DataContractSerializerOperationFormatter.MessageInfo();
			MessageBodyDescription body = messageDescription.Body;
			if (body.WrapperName != null)
			{
				messageInfo.WrapperName = base.AddToDictionary(body.WrapperName);
				messageInfo.WrapperNamespace = base.AddToDictionary(body.WrapperNamespace);
			}
			MessagePartDescriptionCollection parts = body.Parts;
			messageInfo.BodyParts = new DataContractSerializerOperationFormatter.PartInfo[parts.Count];
			for (int i = 0; i < parts.Count; i++)
			{
				messageInfo.BodyParts[i] = this.CreatePartInfo(parts[i], dataContractFormatAttribute.Style, serializerFactory);
			}
			if (OperationFormatter.IsValidReturnValue(messageDescription.Body.ReturnValue))
			{
				messageInfo.ReturnPart = this.CreatePartInfo(messageDescription.Body.ReturnValue, dataContractFormatAttribute.Style, serializerFactory);
			}
			messageInfo.HeaderDescriptionTable = new OperationFormatter.MessageHeaderDescriptionTable();
			messageInfo.HeaderParts = new DataContractSerializerOperationFormatter.PartInfo[messageDescription.Headers.Count];
			for (int j = 0; j < messageDescription.Headers.Count; j++)
			{
				MessageHeaderDescription messageHeaderDescription = messageDescription.Headers[j];
				if (messageHeaderDescription.IsUnknownHeaderCollection)
				{
					messageInfo.UnknownHeaderDescription = messageHeaderDescription;
				}
				else
				{
					this.ValidateDataContractType(messageHeaderDescription.Type);
					messageInfo.HeaderDescriptionTable.Add(messageHeaderDescription.Name, messageHeaderDescription.Namespace, messageHeaderDescription);
				}
				messageInfo.HeaderParts[j] = this.CreatePartInfo(messageHeaderDescription, OperationFormatStyle.Document, serializerFactory);
			}
			messageInfo.AnyHeaders = (messageInfo.UnknownHeaderDescription != null || messageInfo.HeaderDescriptionTable.Count > 0);
			return messageInfo;
		}

		// Token: 0x060038AD RID: 14509 RVA: 0x000DA894 File Offset: 0x000D8A94
		private void ValidateDataContractType(Type type)
		{
			if (this.dataContractExporter == null)
			{
				this.dataContractExporter = new XsdDataContractExporter();
				if (this.serializerFactory != null && this.serializerFactory.DataContractSurrogate != null)
				{
					ExportOptions exportOptions = new ExportOptions();
					exportOptions.DataContractSurrogate = this.serializerFactory.DataContractSurrogate;
					this.dataContractExporter.Options = exportOptions;
				}
			}
			this.dataContractExporter.GetSchemaTypeName(type);
		}

		// Token: 0x060038AE RID: 14510 RVA: 0x000DA8FC File Offset: 0x000D8AFC
		private DataContractSerializerOperationFormatter.PartInfo CreatePartInfo(MessagePartDescription part, OperationFormatStyle style, DataContractSerializerOperationBehavior serializerFactory)
		{
			string s = (style == OperationFormatStyle.Rpc || part.Namespace == null) ? string.Empty : part.Namespace;
			DataContractSerializerOperationFormatter.PartInfo partInfo = new DataContractSerializerOperationFormatter.PartInfo(part, base.AddToDictionary(part.Name), base.AddToDictionary(s), this.knownTypes, serializerFactory);
			this.ValidateDataContractType(partInfo.ContractType);
			return partInfo;
		}

		// Token: 0x060038AF RID: 14511 RVA: 0x000DA954 File Offset: 0x000D8B54
		protected override void AddHeadersToMessage(Message message, MessageDescription messageDescription, object[] parameters, bool isRequest)
		{
			DataContractSerializerOperationFormatter.MessageInfo messageInfo = isRequest ? this.requestMessageInfo : this.replyMessageInfo;
			DataContractSerializerOperationFormatter.PartInfo[] headerParts = messageInfo.HeaderParts;
			if (headerParts == null || headerParts.Length == 0)
			{
				return;
			}
			MessageHeaders headers = message.Headers;
			int i = 0;
			while (i < headerParts.Length)
			{
				DataContractSerializerOperationFormatter.PartInfo partInfo = headerParts[i];
				MessageHeaderDescription messageHeaderDescription = (MessageHeaderDescription)partInfo.Description;
				object obj = parameters[messageHeaderDescription.Index];
				if (!messageHeaderDescription.Multiple)
				{
					goto IL_BF;
				}
				if (obj != null)
				{
					bool isXmlElement = messageHeaderDescription.Type == typeof(XmlElement);
					using (IEnumerator enumerator = ((IEnumerable)obj).GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object parameterValue = enumerator.Current;
							this.AddMessageHeaderForParameter(headers, partInfo, message.Version, parameterValue, isXmlElement);
						}
						goto IL_D1;
					}
					goto IL_BF;
				}
				IL_D1:
				i++;
				continue;
				IL_BF:
				this.AddMessageHeaderForParameter(headers, partInfo, message.Version, obj, false);
				goto IL_D1;
			}
		}

		// Token: 0x060038B0 RID: 14512 RVA: 0x000DAA50 File Offset: 0x000D8C50
		private void AddMessageHeaderForParameter(MessageHeaders headers, DataContractSerializerOperationFormatter.PartInfo headerPart, MessageVersion messageVersion, object parameterValue, bool isXmlElement)
		{
			MessageHeaderDescription headerDescription = (MessageHeaderDescription)headerPart.Description;
			bool mustUnderstand;
			bool relay;
			string actor;
			object contentOfMessageHeaderOfT = OperationFormatter.GetContentOfMessageHeaderOfT(headerDescription, parameterValue, out mustUnderstand, out relay, out actor);
			if (!isXmlElement)
			{
				headers.Add(new DataContractSerializerOperationFormatter.DataContractSerializerMessageHeader(headerPart, contentOfMessageHeaderOfT, mustUnderstand, actor, relay));
				return;
			}
			if (contentOfMessageHeaderOfT == null)
			{
				return;
			}
			XmlElement xmlElement = (XmlElement)contentOfMessageHeaderOfT;
			headers.Add(new OperationFormatter.XmlElementMessageHeader(this, messageVersion, xmlElement.LocalName, xmlElement.NamespaceURI, mustUnderstand, actor, relay, xmlElement));
		}

		// Token: 0x060038B1 RID: 14513 RVA: 0x000DAAC0 File Offset: 0x000D8CC0
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
			DataContractSerializerOperationFormatter.MessageInfo messageInfo;
			if (isRequest)
			{
				messageInfo = this.requestMessageInfo;
			}
			else
			{
				messageInfo = this.replyMessageInfo;
			}
			if (messageInfo.WrapperName != null)
			{
				writer.WriteStartElement(messageInfo.WrapperName, messageInfo.WrapperNamespace);
			}
			if (messageInfo.ReturnPart != null)
			{
				this.SerializeParameter(writer, messageInfo.ReturnPart, returnValue);
			}
			this.SerializeParameters(writer, messageInfo.BodyParts, parameters);
			if (messageInfo.WrapperName != null)
			{
				writer.WriteEndElement();
			}
		}

		// Token: 0x060038B2 RID: 14514 RVA: 0x000DAB60 File Offset: 0x000D8D60
		private void SerializeParameters(XmlDictionaryWriter writer, DataContractSerializerOperationFormatter.PartInfo[] parts, object[] parameters)
		{
			foreach (DataContractSerializerOperationFormatter.PartInfo partInfo in parts)
			{
				object graph = parameters[partInfo.Description.Index];
				this.SerializeParameter(writer, partInfo, graph);
			}
		}

		// Token: 0x060038B3 RID: 14515 RVA: 0x000DAB98 File Offset: 0x000D8D98
		private void SerializeParameter(XmlDictionaryWriter writer, DataContractSerializerOperationFormatter.PartInfo part, object graph)
		{
			if (part.Description.Multiple)
			{
				if (graph == null)
				{
					return;
				}
				using (IEnumerator enumerator = ((IEnumerable)graph).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object graph2 = enumerator.Current;
						this.SerializeParameterPart(writer, part, graph2);
					}
					return;
				}
			}
			this.SerializeParameterPart(writer, part, graph);
		}

		// Token: 0x060038B4 RID: 14516 RVA: 0x000DAC08 File Offset: 0x000D8E08
		private void SerializeParameterPart(XmlDictionaryWriter writer, DataContractSerializerOperationFormatter.PartInfo part, object graph)
		{
			try
			{
				part.Serializer.WriteObject(writer, graph);
			}
			catch (SerializationException ex)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CommunicationException(SR.GetString("SFxInvalidMessageBodyErrorSerializingParameter", new object[]
				{
					part.Description.Namespace,
					part.Description.Name,
					ex.Message
				}), ex));
			}
		}

		// Token: 0x060038B5 RID: 14517 RVA: 0x000DAC7C File Offset: 0x000D8E7C
		protected override void GetHeadersFromMessage(Message message, MessageDescription messageDescription, object[] parameters, bool isRequest)
		{
			DataContractSerializerOperationFormatter.MessageInfo messageInfo = isRequest ? this.requestMessageInfo : this.replyMessageInfo;
			if (!messageInfo.AnyHeaders)
			{
				return;
			}
			MessageHeaders headers = message.Headers;
			KeyValuePair<Type, ArrayList>[] array = null;
			ArrayList arrayList = null;
			if (messageInfo.UnknownHeaderDescription != null)
			{
				arrayList = new ArrayList();
			}
			for (int i = 0; i < headers.Count; i++)
			{
				MessageHeaderInfo messageHeaderInfo = headers[i];
				MessageHeaderDescription messageHeaderDescription = messageInfo.HeaderDescriptionTable.Get(messageHeaderInfo.Name, messageHeaderInfo.Namespace);
				if (messageHeaderDescription != null)
				{
					if (messageHeaderInfo.MustUnderstand)
					{
						headers.UnderstoodHeaders.Add(messageHeaderInfo);
					}
					object obj = null;
					XmlDictionaryReader readerAtHeader = headers.GetReaderAtHeader(i);
					try
					{
						object obj2 = this.DeserializeHeaderContents(readerAtHeader, messageDescription, messageHeaderDescription);
						if (messageHeaderDescription.TypedHeader)
						{
							obj = TypedHeaderManager.Create(messageHeaderDescription.Type, obj2, headers[i].MustUnderstand, headers[i].Relay, headers[i].Actor);
						}
						else
						{
							obj = obj2;
						}
					}
					finally
					{
						readerAtHeader.Close();
					}
					if (messageHeaderDescription.Multiple)
					{
						if (array == null)
						{
							array = new KeyValuePair<Type, ArrayList>[parameters.Length];
						}
						if (array[messageHeaderDescription.Index].Key == null)
						{
							array[messageHeaderDescription.Index] = new KeyValuePair<Type, ArrayList>(messageHeaderDescription.TypedHeader ? TypedHeaderManager.GetMessageHeaderType(messageHeaderDescription.Type) : messageHeaderDescription.Type, new ArrayList());
						}
						array[messageHeaderDescription.Index].Value.Add(obj);
					}
					else
					{
						parameters[messageHeaderDescription.Index] = obj;
					}
				}
				else if (messageInfo.UnknownHeaderDescription != null)
				{
					MessageHeaderDescription unknownHeaderDescription = messageInfo.UnknownHeaderDescription;
					XmlDictionaryReader readerAtHeader2 = headers.GetReaderAtHeader(i);
					try
					{
						XmlDocument xmlDocument = new XmlDocument();
						object obj3 = xmlDocument.ReadNode(readerAtHeader2);
						if (obj3 != null && unknownHeaderDescription.TypedHeader)
						{
							obj3 = TypedHeaderManager.Create(unknownHeaderDescription.Type, obj3, headers[i].MustUnderstand, headers[i].Relay, headers[i].Actor);
						}
						arrayList.Add(obj3);
					}
					finally
					{
						readerAtHeader2.Close();
					}
				}
			}
			if (array != null)
			{
				for (int j = 0; j < parameters.Length; j++)
				{
					if (array[j].Key != null)
					{
						parameters[j] = array[j].Value.ToArray(array[j].Key);
					}
				}
			}
			if (messageInfo.UnknownHeaderDescription != null)
			{
				parameters[messageInfo.UnknownHeaderDescription.Index] = arrayList.ToArray(messageInfo.UnknownHeaderDescription.TypedHeader ? typeof(MessageHeader<XmlElement>) : typeof(XmlElement));
			}
		}

		// Token: 0x060038B6 RID: 14518 RVA: 0x000DAF44 File Offset: 0x000D9144
		private object DeserializeHeaderContents(XmlDictionaryReader reader, MessageDescription messageDescription, MessageHeaderDescription headerDescription)
		{
			bool flag;
			Type substituteDataContractType = DataContractSerializerOperationFormatter.GetSubstituteDataContractType(headerDescription.Type, out flag);
			XmlObjectSerializer xmlObjectSerializer = this.serializerFactory.CreateSerializer(substituteDataContractType, headerDescription.Name, headerDescription.Namespace, this.knownTypes);
			object obj = xmlObjectSerializer.ReadObject(reader);
			if (flag && obj != null)
			{
				return ((IEnumerable)obj).AsQueryable();
			}
			return obj;
		}

		// Token: 0x060038B7 RID: 14519 RVA: 0x000DAF9C File Offset: 0x000D919C
		protected override object DeserializeBody(XmlDictionaryReader reader, MessageVersion version, string action, MessageDescription messageDescription, object[] parameters, bool isRequest)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("reader"));
			}
			if (parameters == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("parameters"));
			}
			DataContractSerializerOperationFormatter.MessageInfo messageInfo;
			if (isRequest)
			{
				messageInfo = this.requestMessageInfo;
			}
			else
			{
				messageInfo = this.replyMessageInfo;
			}
			if (messageInfo.WrapperName != null)
			{
				if (!reader.IsStartElement(messageInfo.WrapperName, messageInfo.WrapperNamespace))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SerializationException(SR.GetString("SFxInvalidMessageBody", new object[]
					{
						messageInfo.WrapperName,
						messageInfo.WrapperNamespace,
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
			if (messageInfo.ReturnPart != null)
			{
				DataContractSerializerOperationFormatter.PartInfo returnPart;
				for (;;)
				{
					returnPart = messageInfo.ReturnPart;
					if (returnPart.Serializer.IsStartObject(reader))
					{
						break;
					}
					if (!reader.IsStartElement())
					{
						goto IL_102;
					}
					OperationFormatter.TraceAndSkipElement(reader);
				}
				result = this.DeserializeParameter(reader, returnPart, isRequest);
			}
			IL_102:
			this.DeserializeParameters(reader, messageInfo.BodyParts, parameters, isRequest);
			if (messageInfo.WrapperName != null)
			{
				reader.ReadEndElement();
			}
			return result;
		}

		// Token: 0x060038B8 RID: 14520 RVA: 0x000DB0CC File Offset: 0x000D92CC
		private void DeserializeParameters(XmlDictionaryReader reader, DataContractSerializerOperationFormatter.PartInfo[] parts, object[] parameters, bool isRequest)
		{
			int num = 0;
			while (reader.IsStartElement())
			{
				for (int i = num; i < parts.Length; i++)
				{
					DataContractSerializerOperationFormatter.PartInfo partInfo = parts[i];
					if (partInfo.Serializer.IsStartObject(reader))
					{
						object obj = this.DeserializeParameter(reader, partInfo, isRequest);
						parameters[partInfo.Description.Index] = obj;
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

		// Token: 0x060038B9 RID: 14521 RVA: 0x000DB140 File Offset: 0x000D9340
		private object DeserializeParameter(XmlDictionaryReader reader, DataContractSerializerOperationFormatter.PartInfo part, bool isRequest)
		{
			if (part.Description.Multiple)
			{
				ArrayList arrayList = new ArrayList();
				while (part.Serializer.IsStartObject(reader))
				{
					arrayList.Add(this.DeserializeParameterPart(reader, part, isRequest));
				}
				return arrayList.ToArray(part.Description.Type);
			}
			return this.DeserializeParameterPart(reader, part, isRequest);
		}

		// Token: 0x060038BA RID: 14522 RVA: 0x000DB19C File Offset: 0x000D939C
		private object DeserializeParameterPart(XmlDictionaryReader reader, DataContractSerializerOperationFormatter.PartInfo part, bool isRequest)
		{
			object result;
			try
			{
				result = part.ReadObject(reader);
			}
			catch (InvalidOperationException innerException)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxInvalidMessageBodyErrorDeserializingParameter", new object[]
				{
					part.Description.Namespace,
					part.Description.Name
				}), innerException));
			}
			catch (InvalidDataContractException innerException2)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataContractException(SR.GetString("SFxInvalidMessageBodyErrorDeserializingParameter", new object[]
				{
					part.Description.Namespace,
					part.Description.Name
				}), innerException2));
			}
			catch (FormatException ex)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(OperationFormatter.CreateDeserializationFailedFault(SR.GetString("SFxInvalidMessageBodyErrorDeserializingParameterMore", new object[]
				{
					part.Description.Namespace,
					part.Description.Name,
					ex.Message
				}), ex));
			}
			catch (SerializationException ex2)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(OperationFormatter.CreateDeserializationFailedFault(SR.GetString("SFxInvalidMessageBodyErrorDeserializingParameterMore", new object[]
				{
					part.Description.Namespace,
					part.Description.Name,
					ex2.Message
				}), ex2));
			}
			return result;
		}

		// Token: 0x060038BB RID: 14523 RVA: 0x000DB2F8 File Offset: 0x000D94F8
		internal static Type GetSubstituteDataContractType(Type type, out bool isQueryable)
		{
			if (type == DataContractSerializerOperationFormatter.typeOfIQueryable)
			{
				isQueryable = true;
				return DataContractSerializerOperationFormatter.typeOfIEnumerable;
			}
			if (type.IsGenericType && type.GetGenericTypeDefinition() == DataContractSerializerOperationFormatter.typeOfIQueryableGeneric)
			{
				isQueryable = true;
				return DataContractSerializerOperationFormatter.typeOfIEnumerableGeneric.MakeGenericType(type.GetGenericArguments());
			}
			isQueryable = false;
			return type;
		}

		// Token: 0x040029A9 RID: 10665
		private static Type typeOfIQueryable = typeof(IQueryable);

		// Token: 0x040029AA RID: 10666
		private static Type typeOfIQueryableGeneric = typeof(IQueryable<>);

		// Token: 0x040029AB RID: 10667
		private static Type typeOfIEnumerable = typeof(IEnumerable);

		// Token: 0x040029AC RID: 10668
		private static Type typeOfIEnumerableGeneric = typeof(IEnumerable<>);

		// Token: 0x040029AD RID: 10669
		protected DataContractSerializerOperationFormatter.MessageInfo requestMessageInfo;

		// Token: 0x040029AE RID: 10670
		protected DataContractSerializerOperationFormatter.MessageInfo replyMessageInfo;

		// Token: 0x040029AF RID: 10671
		private IList<Type> knownTypes;

		// Token: 0x040029B0 RID: 10672
		private XsdDataContractExporter dataContractExporter;

		// Token: 0x040029B1 RID: 10673
		private DataContractSerializerOperationBehavior serializerFactory;

		// Token: 0x02000CAB RID: 3243
		private class DataContractSerializerMessageHeader : XmlObjectSerializerHeader
		{
			// Token: 0x0600793B RID: 31035 RVA: 0x001C4B23 File Offset: 0x001C2D23
			public DataContractSerializerMessageHeader(DataContractSerializerOperationFormatter.PartInfo headerPart, object headerValue, bool mustUnderstand, string actor, bool relay) : base(headerPart.DictionaryName.Value, headerPart.DictionaryNamespace.Value, headerValue, headerPart.Serializer, mustUnderstand, actor ?? string.Empty, relay)
			{
				this.headerPart = headerPart;
			}

			// Token: 0x0600793C RID: 31036 RVA: 0x001C4B60 File Offset: 0x001C2D60
			protected override void OnWriteStartHeader(XmlDictionaryWriter writer, MessageVersion messageVersion)
			{
				string prefix = (this.Namespace == null || this.Namespace.Length == 0) ? string.Empty : "h";
				writer.WriteStartElement(prefix, this.headerPart.DictionaryName, this.headerPart.DictionaryNamespace);
				base.WriteHeaderAttributes(writer, messageVersion);
			}

			// Token: 0x04004518 RID: 17688
			private DataContractSerializerOperationFormatter.PartInfo headerPart;
		}

		// Token: 0x02000CAC RID: 3244
		protected class MessageInfo
		{
			// Token: 0x04004519 RID: 17689
			internal DataContractSerializerOperationFormatter.PartInfo[] HeaderParts;

			// Token: 0x0400451A RID: 17690
			internal XmlDictionaryString WrapperName;

			// Token: 0x0400451B RID: 17691
			internal XmlDictionaryString WrapperNamespace;

			// Token: 0x0400451C RID: 17692
			internal DataContractSerializerOperationFormatter.PartInfo[] BodyParts;

			// Token: 0x0400451D RID: 17693
			internal DataContractSerializerOperationFormatter.PartInfo ReturnPart;

			// Token: 0x0400451E RID: 17694
			internal OperationFormatter.MessageHeaderDescriptionTable HeaderDescriptionTable;

			// Token: 0x0400451F RID: 17695
			internal MessageHeaderDescription UnknownHeaderDescription;

			// Token: 0x04004520 RID: 17696
			internal bool AnyHeaders;
		}

		// Token: 0x02000CAD RID: 3245
		protected class PartInfo
		{
			// Token: 0x0600793E RID: 31038 RVA: 0x001C4BBC File Offset: 0x001C2DBC
			public PartInfo(MessagePartDescription description, XmlDictionaryString dictionaryName, XmlDictionaryString dictionaryNamespace, IList<Type> knownTypes, DataContractSerializerOperationBehavior behavior)
			{
				this.dictionaryName = dictionaryName;
				this.dictionaryNamespace = dictionaryNamespace;
				this.description = description;
				this.knownTypes = knownTypes;
				this.serializerFactory = behavior;
				this.contractType = DataContractSerializerOperationFormatter.GetSubstituteDataContractType(description.Type, out this.isQueryable);
			}

			// Token: 0x17001B86 RID: 7046
			// (get) Token: 0x0600793F RID: 31039 RVA: 0x001C4C0B File Offset: 0x001C2E0B
			public Type ContractType
			{
				get
				{
					return this.contractType;
				}
			}

			// Token: 0x17001B87 RID: 7047
			// (get) Token: 0x06007940 RID: 31040 RVA: 0x001C4C13 File Offset: 0x001C2E13
			public MessagePartDescription Description
			{
				get
				{
					return this.description;
				}
			}

			// Token: 0x17001B88 RID: 7048
			// (get) Token: 0x06007941 RID: 31041 RVA: 0x001C4C1B File Offset: 0x001C2E1B
			public XmlDictionaryString DictionaryName
			{
				get
				{
					return this.dictionaryName;
				}
			}

			// Token: 0x17001B89 RID: 7049
			// (get) Token: 0x06007942 RID: 31042 RVA: 0x001C4C23 File Offset: 0x001C2E23
			public XmlDictionaryString DictionaryNamespace
			{
				get
				{
					return this.dictionaryNamespace;
				}
			}

			// Token: 0x17001B8A RID: 7050
			// (get) Token: 0x06007943 RID: 31043 RVA: 0x001C4C2B File Offset: 0x001C2E2B
			public XmlObjectSerializer Serializer
			{
				get
				{
					if (this.serializer == null)
					{
						this.serializer = this.serializerFactory.CreateSerializer(this.contractType, this.DictionaryName, this.DictionaryNamespace, this.knownTypes);
					}
					return this.serializer;
				}
			}

			// Token: 0x06007944 RID: 31044 RVA: 0x001C4C64 File Offset: 0x001C2E64
			public object ReadObject(XmlDictionaryReader reader)
			{
				return this.ReadObject(reader, this.Serializer);
			}

			// Token: 0x06007945 RID: 31045 RVA: 0x001C4C74 File Offset: 0x001C2E74
			public object ReadObject(XmlDictionaryReader reader, XmlObjectSerializer serializer)
			{
				object obj = this.serializer.ReadObject(reader, false);
				if (this.isQueryable && obj != null)
				{
					return ((IEnumerable)obj).AsQueryable();
				}
				return obj;
			}

			// Token: 0x04004521 RID: 17697
			private XmlDictionaryString dictionaryName;

			// Token: 0x04004522 RID: 17698
			private XmlDictionaryString dictionaryNamespace;

			// Token: 0x04004523 RID: 17699
			private MessagePartDescription description;

			// Token: 0x04004524 RID: 17700
			private XmlObjectSerializer serializer;

			// Token: 0x04004525 RID: 17701
			private IList<Type> knownTypes;

			// Token: 0x04004526 RID: 17702
			private DataContractSerializerOperationBehavior serializerFactory;

			// Token: 0x04004527 RID: 17703
			private Type contractType;

			// Token: 0x04004528 RID: 17704
			private bool isQueryable;
		}
	}
}
