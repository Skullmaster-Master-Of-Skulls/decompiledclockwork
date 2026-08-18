using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime;
using System.Runtime.Diagnostics;
using System.Runtime.Serialization;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Diagnostics;
using System.Xml;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x0200058F RID: 1423
	internal abstract class OperationFormatter : IClientMessageFormatter, IDispatchMessageFormatter
	{
		// Token: 0x060036DA RID: 14042 RVA: 0x000D3644 File Offset: 0x000D1844
		public OperationFormatter(OperationDescription description, bool isRpc, bool isEncoded)
		{
			OperationFormatter.Validate(description, isRpc, isEncoded);
			this.requestDescription = description.Messages[0];
			if (description.Messages.Count == 2)
			{
				this.replyDescription = description.Messages[1];
			}
			int num = 3 + this.requestDescription.Body.Parts.Count;
			if (this.replyDescription != null)
			{
				num += 2 + this.replyDescription.Body.Parts.Count;
			}
			this.dictionary = new XmlDictionary(num * 2);
			OperationFormatter.GetActions(description, this.dictionary, out this.action, out this.replyAction);
			this.operationName = description.Name;
			this.requestStreamFormatter = StreamFormatter.Create(this.requestDescription, this.operationName, true);
			if (this.replyDescription != null)
			{
				this.replyStreamFormatter = StreamFormatter.Create(this.replyDescription, this.operationName, false);
			}
		}

		// Token: 0x060036DB RID: 14043
		protected abstract void AddHeadersToMessage(Message message, MessageDescription messageDescription, object[] parameters, bool isRequest);

		// Token: 0x060036DC RID: 14044
		protected abstract void SerializeBody(XmlDictionaryWriter writer, MessageVersion version, string action, MessageDescription messageDescription, object returnValue, object[] parameters, bool isRequest);

		// Token: 0x060036DD RID: 14045
		protected abstract void GetHeadersFromMessage(Message message, MessageDescription messageDescription, object[] parameters, bool isRequest);

		// Token: 0x060036DE RID: 14046
		protected abstract object DeserializeBody(XmlDictionaryReader reader, MessageVersion version, string action, MessageDescription messageDescription, object[] parameters, bool isRequest);

		// Token: 0x060036DF RID: 14047 RVA: 0x000D3735 File Offset: 0x000D1935
		protected virtual void WriteBodyAttributes(XmlDictionaryWriter writer, MessageVersion messageVersion)
		{
		}

		// Token: 0x17000D05 RID: 3333
		// (get) Token: 0x060036E0 RID: 14048 RVA: 0x000D3737 File Offset: 0x000D1937
		internal string RequestAction
		{
			get
			{
				if (this.action != null)
				{
					return this.action.Value;
				}
				return null;
			}
		}

		// Token: 0x17000D06 RID: 3334
		// (get) Token: 0x060036E1 RID: 14049 RVA: 0x000D374E File Offset: 0x000D194E
		internal string ReplyAction
		{
			get
			{
				if (this.replyAction != null)
				{
					return this.replyAction.Value;
				}
				return null;
			}
		}

		// Token: 0x17000D07 RID: 3335
		// (get) Token: 0x060036E2 RID: 14050 RVA: 0x000D3765 File Offset: 0x000D1965
		protected XmlDictionary Dictionary
		{
			get
			{
				return this.dictionary;
			}
		}

		// Token: 0x17000D08 RID: 3336
		// (get) Token: 0x060036E3 RID: 14051 RVA: 0x000D376D File Offset: 0x000D196D
		protected string OperationName
		{
			get
			{
				return this.operationName;
			}
		}

		// Token: 0x17000D09 RID: 3337
		// (get) Token: 0x060036E4 RID: 14052 RVA: 0x000D3775 File Offset: 0x000D1975
		protected MessageDescription ReplyDescription
		{
			get
			{
				return this.replyDescription;
			}
		}

		// Token: 0x17000D0A RID: 3338
		// (get) Token: 0x060036E5 RID: 14053 RVA: 0x000D377D File Offset: 0x000D197D
		protected MessageDescription RequestDescription
		{
			get
			{
				return this.requestDescription;
			}
		}

		// Token: 0x060036E6 RID: 14054 RVA: 0x000D3785 File Offset: 0x000D1985
		protected XmlDictionaryString AddToDictionary(string s)
		{
			return OperationFormatter.AddToDictionary(this.dictionary, s);
		}

		// Token: 0x060036E7 RID: 14055 RVA: 0x000D3794 File Offset: 0x000D1994
		public object DeserializeReply(Message message, object[] parameters)
		{
			if (message == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("message");
			}
			if (parameters == null)
			{
				throw TraceUtility.ThrowHelperError(new ArgumentNullException("parameters"), message);
			}
			object result;
			try
			{
				object obj2;
				if (this.replyDescription.IsTypedMessage)
				{
					object obj = OperationFormatter.CreateTypedMessageInstance(this.replyDescription.MessageType);
					OperationFormatter.TypedMessageParts typedMessageParts = new OperationFormatter.TypedMessageParts(obj, this.replyDescription);
					object[] array = new object[typedMessageParts.Count];
					this.GetPropertiesFromMessage(message, this.replyDescription, array);
					this.GetHeadersFromMessage(message, this.replyDescription, array, false);
					this.DeserializeBodyContents(message, array, false);
					typedMessageParts.SetTypedMessageParts(array);
					obj2 = obj;
				}
				else
				{
					this.GetPropertiesFromMessage(message, this.replyDescription, parameters);
					this.GetHeadersFromMessage(message, this.replyDescription, parameters, false);
					obj2 = this.DeserializeBodyContents(message, parameters, false);
				}
				result = obj2;
			}
			catch (XmlException ex)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CommunicationException(SR.GetString("SFxErrorDeserializingReplyBodyMore", new object[]
				{
					this.operationName,
					ex.Message
				}), ex));
			}
			catch (FormatException ex2)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CommunicationException(SR.GetString("SFxErrorDeserializingReplyBodyMore", new object[]
				{
					this.operationName,
					ex2.Message
				}), ex2));
			}
			catch (SerializationException ex3)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CommunicationException(SR.GetString("SFxErrorDeserializingReplyBodyMore", new object[]
				{
					this.operationName,
					ex3.Message
				}), ex3));
			}
			return result;
		}

		// Token: 0x060036E8 RID: 14056 RVA: 0x000D3934 File Offset: 0x000D1B34
		private static object CreateTypedMessageInstance(Type messageContractType)
		{
			BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance;
			object result;
			try
			{
				object obj = Activator.CreateInstance(messageContractType, bindingAttr, null, OperationFormatter.emptyObjectArray, null);
				result = obj;
			}
			catch (MissingMethodException innerException)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxMessageContractRequiresDefaultConstructor", new object[]
				{
					messageContractType.FullName
				}), innerException));
			}
			return result;
		}

		// Token: 0x060036E9 RID: 14057 RVA: 0x000D3998 File Offset: 0x000D1B98
		public void DeserializeRequest(Message message, object[] parameters)
		{
			if (message == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("message");
			}
			if (parameters == null)
			{
				throw TraceUtility.ThrowHelperError(new ArgumentNullException("parameters"), message);
			}
			try
			{
				if (this.requestDescription.IsTypedMessage)
				{
					object obj = OperationFormatter.CreateTypedMessageInstance(this.requestDescription.MessageType);
					OperationFormatter.TypedMessageParts typedMessageParts = new OperationFormatter.TypedMessageParts(obj, this.requestDescription);
					object[] array = new object[typedMessageParts.Count];
					this.GetPropertiesFromMessage(message, this.requestDescription, array);
					this.GetHeadersFromMessage(message, this.requestDescription, array, true);
					this.DeserializeBodyContents(message, array, true);
					typedMessageParts.SetTypedMessageParts(array);
					parameters[0] = obj;
				}
				else
				{
					this.GetPropertiesFromMessage(message, this.requestDescription, parameters);
					this.GetHeadersFromMessage(message, this.requestDescription, parameters, true);
					this.DeserializeBodyContents(message, parameters, true);
				}
			}
			catch (XmlException ex)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(OperationFormatter.CreateDeserializationFailedFault(SR.GetString("SFxErrorDeserializingRequestBodyMore", new object[]
				{
					this.operationName,
					ex.Message
				}), ex));
			}
			catch (FormatException ex2)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(OperationFormatter.CreateDeserializationFailedFault(SR.GetString("SFxErrorDeserializingRequestBodyMore", new object[]
				{
					this.operationName,
					ex2.Message
				}), ex2));
			}
			catch (SerializationException ex3)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CommunicationException(SR.GetString("SFxErrorDeserializingRequestBodyMore", new object[]
				{
					this.operationName,
					ex3.Message
				}), ex3));
			}
		}

		// Token: 0x060036EA RID: 14058 RVA: 0x000D3B30 File Offset: 0x000D1D30
		private object DeserializeBodyContents(Message message, object[] parameters, bool isRequest)
		{
			StreamFormatter streamFormatter;
			MessageDescription messageDescription;
			this.SetupStreamAndMessageDescription(isRequest, out streamFormatter, out messageDescription);
			if (streamFormatter != null)
			{
				object result = null;
				streamFormatter.Deserialize(parameters, ref result, message);
				return result;
			}
			if (message.IsEmpty)
			{
				return null;
			}
			XmlDictionaryReader readerAtBodyContents = message.GetReaderAtBodyContents();
			object result2;
			using (readerAtBodyContents)
			{
				object obj = this.DeserializeBody(readerAtBodyContents, message.Version, this.RequestAction, messageDescription, parameters, isRequest);
				message.ReadFromBodyContentsToEnd(readerAtBodyContents);
				result2 = obj;
			}
			return result2;
		}

		// Token: 0x060036EB RID: 14059 RVA: 0x000D3BB0 File Offset: 0x000D1DB0
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
			object[] array;
			if (this.requestDescription.IsTypedMessage)
			{
				OperationFormatter.TypedMessageParts typedMessageParts = new OperationFormatter.TypedMessageParts(parameters[0], this.requestDescription);
				array = new object[typedMessageParts.Count];
				typedMessageParts.GetTypedMessageParts(array);
			}
			else
			{
				array = parameters;
			}
			Message message = new OperationFormatter.OperationFormatterMessage(this, messageVersion, (this.action == null) ? null : ActionHeader.Create(this.action, messageVersion.Addressing), array, null, true);
			this.AddPropertiesToMessage(message, this.requestDescription, array);
			this.AddHeadersToMessage(message, this.requestDescription, array, true);
			return message;
		}

		// Token: 0x060036EC RID: 14060 RVA: 0x000D3C60 File Offset: 0x000D1E60
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
			object[] array;
			object returnValue;
			if (this.replyDescription.IsTypedMessage)
			{
				OperationFormatter.TypedMessageParts typedMessageParts = new OperationFormatter.TypedMessageParts(result, this.replyDescription);
				array = new object[typedMessageParts.Count];
				typedMessageParts.GetTypedMessageParts(array);
				returnValue = null;
			}
			else
			{
				array = parameters;
				returnValue = result;
			}
			Message message = new OperationFormatter.OperationFormatterMessage(this, messageVersion, (this.replyAction == null) ? null : ActionHeader.Create(this.replyAction, messageVersion.Addressing), array, returnValue, false);
			this.AddPropertiesToMessage(message, this.replyDescription, array);
			this.AddHeadersToMessage(message, this.replyDescription, array, false);
			return message;
		}

		// Token: 0x060036ED RID: 14061 RVA: 0x000D3D11 File Offset: 0x000D1F11
		private void SetupStreamAndMessageDescription(bool isRequest, out StreamFormatter streamFormatter, out MessageDescription messageDescription)
		{
			if (isRequest)
			{
				streamFormatter = this.requestStreamFormatter;
				messageDescription = this.requestDescription;
				return;
			}
			streamFormatter = this.replyStreamFormatter;
			messageDescription = this.replyDescription;
		}

		// Token: 0x060036EE RID: 14062 RVA: 0x000D3D38 File Offset: 0x000D1F38
		private void SerializeBodyContents(XmlDictionaryWriter writer, MessageVersion version, object[] parameters, object returnValue, bool isRequest)
		{
			StreamFormatter streamFormatter;
			MessageDescription messageDescription;
			this.SetupStreamAndMessageDescription(isRequest, out streamFormatter, out messageDescription);
			if (streamFormatter != null)
			{
				streamFormatter.Serialize(writer, parameters, returnValue);
				return;
			}
			this.SerializeBody(writer, version, this.RequestAction, messageDescription, returnValue, parameters, isRequest);
		}

		// Token: 0x060036EF RID: 14063 RVA: 0x000D3D73 File Offset: 0x000D1F73
		private IAsyncResult BeginSerializeBodyContents(XmlDictionaryWriter writer, MessageVersion version, object[] parameters, object returnValue, bool isRequest, AsyncCallback callback, object state)
		{
			return new OperationFormatter.SerializeBodyContentsAsyncResult(this, writer, version, parameters, returnValue, isRequest, callback, state);
		}

		// Token: 0x060036F0 RID: 14064 RVA: 0x000D3D86 File Offset: 0x000D1F86
		private void EndSerializeBodyContents(IAsyncResult result)
		{
			OperationFormatter.SerializeBodyContentsAsyncResult.End(result);
		}

		// Token: 0x060036F1 RID: 14065 RVA: 0x000D3D8E File Offset: 0x000D1F8E
		private void AddPropertiesToMessage(Message message, MessageDescription messageDescription, object[] parameters)
		{
			if (messageDescription.Properties.Count > 0)
			{
				this.AddPropertiesToMessageCore(message, messageDescription, parameters);
			}
		}

		// Token: 0x060036F2 RID: 14066 RVA: 0x000D3DA8 File Offset: 0x000D1FA8
		private void AddPropertiesToMessageCore(Message message, MessageDescription messageDescription, object[] parameters)
		{
			MessageProperties properties = message.Properties;
			MessagePropertyDescriptionCollection properties2 = messageDescription.Properties;
			for (int i = 0; i < properties2.Count; i++)
			{
				MessagePropertyDescription messagePropertyDescription = properties2[i];
				object obj = parameters[messagePropertyDescription.Index];
				if (obj != null)
				{
					properties.Add(messagePropertyDescription.Name, obj);
				}
			}
		}

		// Token: 0x060036F3 RID: 14067 RVA: 0x000D3DF8 File Offset: 0x000D1FF8
		private void GetPropertiesFromMessage(Message message, MessageDescription messageDescription, object[] parameters)
		{
			if (messageDescription.Properties.Count > 0)
			{
				this.GetPropertiesFromMessageCore(message, messageDescription, parameters);
			}
		}

		// Token: 0x060036F4 RID: 14068 RVA: 0x000D3E14 File Offset: 0x000D2014
		private void GetPropertiesFromMessageCore(Message message, MessageDescription messageDescription, object[] parameters)
		{
			MessageProperties properties = message.Properties;
			MessagePropertyDescriptionCollection properties2 = messageDescription.Properties;
			for (int i = 0; i < properties2.Count; i++)
			{
				MessagePropertyDescription messagePropertyDescription = properties2[i];
				if (properties.ContainsKey(messagePropertyDescription.Name))
				{
					parameters[messagePropertyDescription.Index] = properties[messagePropertyDescription.Name];
				}
			}
		}

		// Token: 0x060036F5 RID: 14069 RVA: 0x000D3E6A File Offset: 0x000D206A
		internal static object GetContentOfMessageHeaderOfT(MessageHeaderDescription headerDescription, object parameterValue, out bool mustUnderstand, out bool relay, out string actor)
		{
			actor = headerDescription.Actor;
			mustUnderstand = headerDescription.MustUnderstand;
			relay = headerDescription.Relay;
			if (headerDescription.TypedHeader && parameterValue != null)
			{
				parameterValue = TypedHeaderManager.GetContent(headerDescription.Type, parameterValue, out mustUnderstand, out relay, out actor);
			}
			return parameterValue;
		}

		// Token: 0x060036F6 RID: 14070 RVA: 0x000D3EA3 File Offset: 0x000D20A3
		internal static bool IsValidReturnValue(MessagePartDescription returnValue)
		{
			return returnValue != null && returnValue.Type != typeof(void);
		}

		// Token: 0x060036F7 RID: 14071 RVA: 0x000D3EC0 File Offset: 0x000D20C0
		internal static XmlDictionaryString AddToDictionary(XmlDictionary dictionary, string s)
		{
			XmlDictionaryString result;
			if (!dictionary.TryLookup(s, out result))
			{
				result = dictionary.Add(s);
			}
			return result;
		}

		// Token: 0x060036F8 RID: 14072 RVA: 0x000D3EE4 File Offset: 0x000D20E4
		internal static void Validate(OperationDescription operation, bool isRpc, bool isEncoded)
		{
			if (isEncoded && !isRpc)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxDocEncodedNotSupported", new object[]
				{
					operation.Name
				})));
			}
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			for (int i = 0; i < operation.Messages.Count; i++)
			{
				MessageDescription messageDescription = operation.Messages[i];
				if (messageDescription.IsTypedMessage || messageDescription.IsUntypedMessage)
				{
					if (isRpc && operation.IsValidateRpcWrapperName && !isEncoded)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxTypedMessageCannotBeRpcLiteral", new object[]
						{
							operation.Name
						})));
					}
					flag2 = true;
				}
				else if (messageDescription.IsVoid)
				{
					flag = true;
				}
				else
				{
					flag3 = true;
				}
			}
			if (flag3 && flag2)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxTypedOrUntypedMessageCannotBeMixedWithParameters", new object[]
				{
					operation.Name
				})));
			}
			if (isRpc && flag2 && flag)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxTypedOrUntypedMessageCannotBeMixedWithVoidInRpc", new object[]
				{
					operation.Name
				})));
			}
		}

		// Token: 0x060036F9 RID: 14073 RVA: 0x000D4004 File Offset: 0x000D2204
		internal static void GetActions(OperationDescription description, XmlDictionary dictionary, out XmlDictionaryString action, out XmlDictionaryString replyAction)
		{
			string text = description.Messages[0].Action;
			if (text == "*")
			{
				text = null;
			}
			string text2;
			if (!description.IsOneWay)
			{
				text2 = description.Messages[1].Action;
			}
			else
			{
				text2 = null;
			}
			if (text2 == "*")
			{
				text2 = null;
			}
			XmlDictionaryString xmlDictionaryString;
			replyAction = (xmlDictionaryString = null);
			action = xmlDictionaryString;
			if (text != null)
			{
				action = OperationFormatter.AddToDictionary(dictionary, text);
			}
			if (text2 != null)
			{
				replyAction = OperationFormatter.AddToDictionary(dictionary, text2);
			}
		}

		// Token: 0x060036FA RID: 14074 RVA: 0x000D4080 File Offset: 0x000D2280
		internal static NetDispatcherFaultException CreateDeserializationFailedFault(string reason, Exception innerException)
		{
			reason = SR.GetString("SFxDeserializationFailed1", new object[]
			{
				reason
			});
			FaultCode faultCode = new FaultCode("DeserializationFailed", "http://schemas.microsoft.com/net/2005/12/windowscommunicationfoundation/dispatcher");
			faultCode = FaultCode.CreateSenderFaultCode(faultCode);
			return new NetDispatcherFaultException(reason, faultCode, innerException);
		}

		// Token: 0x060036FB RID: 14075 RVA: 0x000D40C4 File Offset: 0x000D22C4
		internal static void TraceAndSkipElement(XmlReader xmlReader)
		{
			if (DiagnosticUtility.ShouldTraceVerbose)
			{
				TraceUtility.TraceEvent(TraceEventType.Verbose, 196615, SR.GetString("SFxTraceCodeElementIgnored"), new StringTraceRecord("Element", xmlReader.NamespaceURI + ":" + xmlReader.LocalName));
			}
			xmlReader.Skip();
		}

		// Token: 0x040028F7 RID: 10487
		private MessageDescription replyDescription;

		// Token: 0x040028F8 RID: 10488
		private MessageDescription requestDescription;

		// Token: 0x040028F9 RID: 10489
		private XmlDictionaryString action;

		// Token: 0x040028FA RID: 10490
		private XmlDictionaryString replyAction;

		// Token: 0x040028FB RID: 10491
		protected StreamFormatter requestStreamFormatter;

		// Token: 0x040028FC RID: 10492
		protected StreamFormatter replyStreamFormatter;

		// Token: 0x040028FD RID: 10493
		private XmlDictionary dictionary;

		// Token: 0x040028FE RID: 10494
		private string operationName;

		// Token: 0x040028FF RID: 10495
		private static object[] emptyObjectArray = new object[0];

		// Token: 0x02000C96 RID: 3222
		private class SerializeBodyContentsAsyncResult : AsyncResult
		{
			// Token: 0x060078EB RID: 30955 RVA: 0x001C3604 File Offset: 0x001C1804
			internal SerializeBodyContentsAsyncResult(OperationFormatter operationFormatter, XmlDictionaryWriter writer, MessageVersion version, object[] parameters, object returnValue, bool isRequest, AsyncCallback callback, object state) : base(callback, state)
			{
				StreamFormatter streamFormatter;
				MessageDescription messageDescription;
				operationFormatter.SetupStreamAndMessageDescription(isRequest, out streamFormatter, out messageDescription);
				bool flag;
				if (streamFormatter != null)
				{
					this.streamFormatter = streamFormatter;
					IAsyncResult result = streamFormatter.BeginSerialize(writer, parameters, returnValue, base.PrepareAsyncCompletion(OperationFormatter.SerializeBodyContentsAsyncResult.handleEndSerializeBodyContents), this);
					flag = base.SyncContinue(result);
				}
				else
				{
					operationFormatter.SerializeBody(writer, version, operationFormatter.RequestAction, messageDescription, returnValue, parameters, isRequest);
					flag = true;
				}
				if (flag)
				{
					base.Complete(true);
				}
			}

			// Token: 0x060078EC RID: 30956 RVA: 0x001C3678 File Offset: 0x001C1878
			private static bool HandleEndSerializeBodyContents(IAsyncResult result)
			{
				OperationFormatter.SerializeBodyContentsAsyncResult serializeBodyContentsAsyncResult = (OperationFormatter.SerializeBodyContentsAsyncResult)result.AsyncState;
				serializeBodyContentsAsyncResult.streamFormatter.EndSerialize(result);
				return true;
			}

			// Token: 0x060078ED RID: 30957 RVA: 0x001C369E File Offset: 0x001C189E
			public static void End(IAsyncResult result)
			{
				AsyncResult.End<OperationFormatter.SerializeBodyContentsAsyncResult>(result);
			}

			// Token: 0x040044DA RID: 17626
			private static AsyncResult.AsyncCompletion handleEndSerializeBodyContents = new AsyncResult.AsyncCompletion(OperationFormatter.SerializeBodyContentsAsyncResult.HandleEndSerializeBodyContents);

			// Token: 0x040044DB RID: 17627
			private StreamFormatter streamFormatter;
		}

		// Token: 0x02000C97 RID: 3223
		private class TypedMessageParts
		{
			// Token: 0x060078EF RID: 30959 RVA: 0x001C36BC File Offset: 0x001C18BC
			public TypedMessageParts(object instance, MessageDescription description)
			{
				if (description == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("description"));
				}
				if (instance == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException(SR.GetString("SFxTypedMessageCannotBeNull", new object[]
					{
						description.Action
					})));
				}
				this.members = new MemberInfo[description.Body.Parts.Count + description.Properties.Count + description.Headers.Count];
				foreach (MessagePartDescription messagePartDescription in description.Headers)
				{
					this.members[messagePartDescription.Index] = messagePartDescription.MemberInfo;
				}
				foreach (MessagePartDescription messagePartDescription2 in description.Properties)
				{
					this.members[messagePartDescription2.Index] = messagePartDescription2.MemberInfo;
				}
				foreach (MessagePartDescription messagePartDescription3 in description.Body.Parts)
				{
					this.members[messagePartDescription3.Index] = messagePartDescription3.MemberInfo;
				}
				this.instance = instance;
			}

			// Token: 0x060078F0 RID: 30960 RVA: 0x001C3838 File Offset: 0x001C1A38
			private object GetValue(int index)
			{
				MemberInfo memberInfo = this.members[index];
				if (memberInfo.MemberType == MemberTypes.Property)
				{
					return ((PropertyInfo)this.members[index]).GetValue(this.instance, null);
				}
				return ((FieldInfo)this.members[index]).GetValue(this.instance);
			}

			// Token: 0x060078F1 RID: 30961 RVA: 0x001C388C File Offset: 0x001C1A8C
			private void SetValue(object value, int index)
			{
				MemberInfo memberInfo = this.members[index];
				if (memberInfo.MemberType == MemberTypes.Property)
				{
					((PropertyInfo)this.members[index]).SetValue(this.instance, value, null);
					return;
				}
				((FieldInfo)this.members[index]).SetValue(this.instance, value);
			}

			// Token: 0x060078F2 RID: 30962 RVA: 0x001C38E0 File Offset: 0x001C1AE0
			internal void GetTypedMessageParts(object[] values)
			{
				for (int i = 0; i < this.members.Length; i++)
				{
					values[i] = this.GetValue(i);
				}
			}

			// Token: 0x060078F3 RID: 30963 RVA: 0x001C390C File Offset: 0x001C1B0C
			internal void SetTypedMessageParts(object[] values)
			{
				for (int i = 0; i < this.members.Length; i++)
				{
					this.SetValue(values[i], i);
				}
			}

			// Token: 0x17001B79 RID: 7033
			// (get) Token: 0x060078F4 RID: 30964 RVA: 0x001C3936 File Offset: 0x001C1B36
			internal int Count
			{
				get
				{
					return this.members.Length;
				}
			}

			// Token: 0x040044DC RID: 17628
			private object instance;

			// Token: 0x040044DD RID: 17629
			private MemberInfo[] members;
		}

		// Token: 0x02000C98 RID: 3224
		internal class OperationFormatterMessage : BodyWriterMessage
		{
			// Token: 0x060078F5 RID: 30965 RVA: 0x001C3940 File Offset: 0x001C1B40
			public OperationFormatterMessage(OperationFormatter operationFormatter, MessageVersion version, ActionHeader action, object[] parameters, object returnValue, bool isRequest) : base(version, action, new OperationFormatter.OperationFormatterMessage.OperationFormatterBodyWriter(operationFormatter, version, parameters, returnValue, isRequest))
			{
				this.operationFormatter = operationFormatter;
			}

			// Token: 0x060078F6 RID: 30966 RVA: 0x001C395E File Offset: 0x001C1B5E
			public OperationFormatterMessage(MessageVersion version, string action, BodyWriter bodyWriter) : base(version, action, bodyWriter)
			{
			}

			// Token: 0x060078F7 RID: 30967 RVA: 0x001C3969 File Offset: 0x001C1B69
			private OperationFormatterMessage(MessageHeaders headers, KeyValuePair<string, object>[] properties, OperationFormatter.OperationFormatterMessage.OperationFormatterBodyWriter bodyWriter) : base(headers, properties, bodyWriter)
			{
				this.operationFormatter = bodyWriter.OperationFormatter;
			}

			// Token: 0x060078F8 RID: 30968 RVA: 0x001C3980 File Offset: 0x001C1B80
			protected override void OnWriteStartBody(XmlDictionaryWriter writer)
			{
				base.OnWriteStartBody(writer);
				this.operationFormatter.WriteBodyAttributes(writer, this.Version);
			}

			// Token: 0x060078F9 RID: 30969 RVA: 0x001C399C File Offset: 0x001C1B9C
			protected override MessageBuffer OnCreateBufferedCopy(int maxBufferSize)
			{
				BodyWriter bodyWriter;
				if (base.BodyWriter.IsBuffered)
				{
					bodyWriter = base.BodyWriter;
				}
				else
				{
					bodyWriter = base.BodyWriter.CreateBufferedCopy(maxBufferSize);
				}
				KeyValuePair<string, object>[] array = new KeyValuePair<string, object>[base.Properties.Count];
				((ICollection<KeyValuePair<string, object>>)base.Properties).CopyTo(array, 0);
				return new OperationFormatter.OperationFormatterMessage.OperationFormatterMessageBuffer(base.Headers, array, bodyWriter);
			}

			// Token: 0x040044DE RID: 17630
			private OperationFormatter operationFormatter;

			// Token: 0x02000F3D RID: 3901
			private class OperationFormatterBodyWriter : BodyWriter
			{
				// Token: 0x0600869F RID: 34463 RVA: 0x001F2DBE File Offset: 0x001F0FBE
				public OperationFormatterBodyWriter(OperationFormatter operationFormatter, MessageVersion version, object[] parameters, object returnValue, bool isRequest) : base(OperationFormatter.OperationFormatterMessage.OperationFormatterBodyWriter.AreParametersBuffered(isRequest, operationFormatter))
				{
					this.parameters = parameters;
					this.returnValue = returnValue;
					this.isRequest = isRequest;
					this.operationFormatter = operationFormatter;
					this.version = version;
				}

				// Token: 0x17001D86 RID: 7558
				// (get) Token: 0x060086A0 RID: 34464 RVA: 0x001F2DF3 File Offset: 0x001F0FF3
				private object ThisLock
				{
					get
					{
						return this;
					}
				}

				// Token: 0x060086A1 RID: 34465 RVA: 0x001F2DF8 File Offset: 0x001F0FF8
				private static bool AreParametersBuffered(bool isRequest, OperationFormatter operationFormatter)
				{
					StreamFormatter streamFormatter = isRequest ? operationFormatter.requestStreamFormatter : operationFormatter.replyStreamFormatter;
					return streamFormatter == null;
				}

				// Token: 0x060086A2 RID: 34466 RVA: 0x001F2E1C File Offset: 0x001F101C
				protected override void OnWriteBodyContents(XmlDictionaryWriter writer)
				{
					object thisLock = this.ThisLock;
					lock (thisLock)
					{
						this.operationFormatter.SerializeBodyContents(writer, this.version, this.parameters, this.returnValue, this.isRequest);
					}
				}

				// Token: 0x060086A3 RID: 34467 RVA: 0x001F2E7C File Offset: 0x001F107C
				protected override IAsyncResult OnBeginWriteBodyContents(XmlDictionaryWriter writer, AsyncCallback callback, object state)
				{
					this.onBeginWriteBodyContentsCalled = true;
					return new OperationFormatter.OperationFormatterMessage.OperationFormatterBodyWriter.OnWriteBodyContentsAsyncResult(this, writer, callback, state);
				}

				// Token: 0x060086A4 RID: 34468 RVA: 0x001F2E8E File Offset: 0x001F108E
				protected override void OnEndWriteBodyContents(IAsyncResult result)
				{
					OperationFormatter.OperationFormatterMessage.OperationFormatterBodyWriter.OnWriteBodyContentsAsyncResult.End(result);
				}

				// Token: 0x17001D87 RID: 7559
				// (get) Token: 0x060086A5 RID: 34469 RVA: 0x001F2E96 File Offset: 0x001F1096
				internal OperationFormatter OperationFormatter
				{
					get
					{
						return this.operationFormatter;
					}
				}

				// Token: 0x04004E3C RID: 20028
				private bool isRequest;

				// Token: 0x04004E3D RID: 20029
				private OperationFormatter operationFormatter;

				// Token: 0x04004E3E RID: 20030
				private object[] parameters;

				// Token: 0x04004E3F RID: 20031
				private object returnValue;

				// Token: 0x04004E40 RID: 20032
				private MessageVersion version;

				// Token: 0x04004E41 RID: 20033
				private bool onBeginWriteBodyContentsCalled;

				// Token: 0x02000FC5 RID: 4037
				private class OnWriteBodyContentsAsyncResult : AsyncResult
				{
					// Token: 0x060088D9 RID: 35033 RVA: 0x001FDBB8 File Offset: 0x001FBDB8
					internal OnWriteBodyContentsAsyncResult(OperationFormatter.OperationFormatterMessage.OperationFormatterBodyWriter operationFormatterBodyWriter, XmlDictionaryWriter writer, AsyncCallback callback, object state) : base(callback, state)
					{
						this.operationFormatter = operationFormatterBodyWriter.OperationFormatter;
						IAsyncResult result = this.operationFormatter.BeginSerializeBodyContents(writer, operationFormatterBodyWriter.version, operationFormatterBodyWriter.parameters, operationFormatterBodyWriter.returnValue, operationFormatterBodyWriter.isRequest, base.PrepareAsyncCompletion(OperationFormatter.OperationFormatterMessage.OperationFormatterBodyWriter.OnWriteBodyContentsAsyncResult.handleEndOnWriteBodyContents), this);
						bool flag = base.SyncContinue(result);
						if (flag)
						{
							base.Complete(true);
						}
					}

					// Token: 0x060088DA RID: 35034 RVA: 0x001FDC20 File Offset: 0x001FBE20
					private static bool HandleEndOnWriteBodyContents(IAsyncResult result)
					{
						OperationFormatter.OperationFormatterMessage.OperationFormatterBodyWriter.OnWriteBodyContentsAsyncResult onWriteBodyContentsAsyncResult = (OperationFormatter.OperationFormatterMessage.OperationFormatterBodyWriter.OnWriteBodyContentsAsyncResult)result.AsyncState;
						onWriteBodyContentsAsyncResult.operationFormatter.EndSerializeBodyContents(result);
						return true;
					}

					// Token: 0x060088DB RID: 35035 RVA: 0x001FDC46 File Offset: 0x001FBE46
					public static void End(IAsyncResult result)
					{
						AsyncResult.End<OperationFormatter.OperationFormatterMessage.OperationFormatterBodyWriter.OnWriteBodyContentsAsyncResult>(result);
					}

					// Token: 0x04005075 RID: 20597
					private static AsyncResult.AsyncCompletion handleEndOnWriteBodyContents = new AsyncResult.AsyncCompletion(OperationFormatter.OperationFormatterMessage.OperationFormatterBodyWriter.OnWriteBodyContentsAsyncResult.HandleEndOnWriteBodyContents);

					// Token: 0x04005076 RID: 20598
					private OperationFormatter operationFormatter;
				}
			}

			// Token: 0x02000F3E RID: 3902
			private class OperationFormatterMessageBuffer : BodyWriterMessageBuffer
			{
				// Token: 0x060086A6 RID: 34470 RVA: 0x001F2E9E File Offset: 0x001F109E
				public OperationFormatterMessageBuffer(MessageHeaders headers, KeyValuePair<string, object>[] properties, BodyWriter bodyWriter) : base(headers, properties, bodyWriter)
				{
				}

				// Token: 0x060086A7 RID: 34471 RVA: 0x001F2EAC File Offset: 0x001F10AC
				public override Message CreateMessage()
				{
					OperationFormatter.OperationFormatterMessage.OperationFormatterBodyWriter operationFormatterBodyWriter = base.BodyWriter as OperationFormatter.OperationFormatterMessage.OperationFormatterBodyWriter;
					if (operationFormatterBodyWriter == null)
					{
						return base.CreateMessage();
					}
					object thisLock = base.ThisLock;
					Message result;
					lock (thisLock)
					{
						if (base.Closed)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(base.CreateBufferDisposedException());
						}
						result = new OperationFormatter.OperationFormatterMessage(base.Headers, base.Properties, operationFormatterBodyWriter);
					}
					return result;
				}
			}
		}

		// Token: 0x02000C99 RID: 3225
		internal abstract class OperationFormatterHeader : MessageHeader
		{
			// Token: 0x060078FA RID: 30970 RVA: 0x001C39F8 File Offset: 0x001C1BF8
			public OperationFormatterHeader(OperationFormatter operationFormatter, MessageVersion version, string name, string ns, bool mustUnderstand, string actor, bool relay)
			{
				this.operationFormatter = operationFormatter;
				this.version = version;
				if (actor != null)
				{
					this.innerHeader = MessageHeader.CreateHeader(name, ns, null, mustUnderstand, actor, relay);
					return;
				}
				this.innerHeader = MessageHeader.CreateHeader(name, ns, null, mustUnderstand, "", relay);
			}

			// Token: 0x060078FB RID: 30971 RVA: 0x001C3A4B File Offset: 0x001C1C4B
			public override bool IsMessageVersionSupported(MessageVersion messageVersion)
			{
				return this.innerHeader.IsMessageVersionSupported(messageVersion);
			}

			// Token: 0x17001B7A RID: 7034
			// (get) Token: 0x060078FC RID: 30972 RVA: 0x001C3A59 File Offset: 0x001C1C59
			public override string Name
			{
				get
				{
					return this.innerHeader.Name;
				}
			}

			// Token: 0x17001B7B RID: 7035
			// (get) Token: 0x060078FD RID: 30973 RVA: 0x001C3A66 File Offset: 0x001C1C66
			public override string Namespace
			{
				get
				{
					return this.innerHeader.Namespace;
				}
			}

			// Token: 0x17001B7C RID: 7036
			// (get) Token: 0x060078FE RID: 30974 RVA: 0x001C3A73 File Offset: 0x001C1C73
			public override bool MustUnderstand
			{
				get
				{
					return this.innerHeader.MustUnderstand;
				}
			}

			// Token: 0x17001B7D RID: 7037
			// (get) Token: 0x060078FF RID: 30975 RVA: 0x001C3A80 File Offset: 0x001C1C80
			public override bool Relay
			{
				get
				{
					return this.innerHeader.Relay;
				}
			}

			// Token: 0x17001B7E RID: 7038
			// (get) Token: 0x06007900 RID: 30976 RVA: 0x001C3A8D File Offset: 0x001C1C8D
			public override string Actor
			{
				get
				{
					return this.innerHeader.Actor;
				}
			}

			// Token: 0x06007901 RID: 30977 RVA: 0x001C3A9A File Offset: 0x001C1C9A
			protected override void OnWriteStartHeader(XmlDictionaryWriter writer, MessageVersion messageVersion)
			{
				writer.WriteStartElement((this.Namespace == null || this.Namespace.Length == 0) ? string.Empty : "h", this.Name, this.Namespace);
				this.OnWriteHeaderAttributes(writer, messageVersion);
			}

			// Token: 0x06007902 RID: 30978 RVA: 0x001C3AD7 File Offset: 0x001C1CD7
			protected virtual void OnWriteHeaderAttributes(XmlDictionaryWriter writer, MessageVersion messageVersion)
			{
				base.WriteHeaderAttributes(writer, messageVersion);
			}

			// Token: 0x040044DF RID: 17631
			protected MessageHeader innerHeader;

			// Token: 0x040044E0 RID: 17632
			protected OperationFormatter operationFormatter;

			// Token: 0x040044E1 RID: 17633
			protected MessageVersion version;
		}

		// Token: 0x02000C9A RID: 3226
		internal class XmlElementMessageHeader : OperationFormatter.OperationFormatterHeader
		{
			// Token: 0x06007903 RID: 30979 RVA: 0x001C3AE1 File Offset: 0x001C1CE1
			public XmlElementMessageHeader(OperationFormatter operationFormatter, MessageVersion version, string name, string ns, bool mustUnderstand, string actor, bool relay, XmlElement headerValue) : base(operationFormatter, version, name, ns, mustUnderstand, actor, relay)
			{
				this.headerValue = headerValue;
			}

			// Token: 0x06007904 RID: 30980 RVA: 0x001C3AFC File Offset: 0x001C1CFC
			protected override void OnWriteHeaderAttributes(XmlDictionaryWriter writer, MessageVersion messageVersion)
			{
				base.WriteHeaderAttributes(writer, messageVersion);
				XmlDictionaryReader xmlDictionaryReader = XmlDictionaryReader.CreateDictionaryReader(new XmlNodeReader(this.headerValue));
				xmlDictionaryReader.MoveToContent();
				writer.WriteAttributes(xmlDictionaryReader, false);
			}

			// Token: 0x06007905 RID: 30981 RVA: 0x001C3B31 File Offset: 0x001C1D31
			protected override void OnWriteHeaderContents(XmlDictionaryWriter writer, MessageVersion messageVersion)
			{
				this.headerValue.WriteContentTo(writer);
			}

			// Token: 0x040044E2 RID: 17634
			protected XmlElement headerValue;
		}

		// Token: 0x02000C9B RID: 3227
		internal struct QName
		{
			// Token: 0x06007906 RID: 30982 RVA: 0x001C3B3F File Offset: 0x001C1D3F
			internal QName(string name, string ns)
			{
				this.Name = name;
				this.Namespace = ns;
			}

			// Token: 0x040044E3 RID: 17635
			internal string Name;

			// Token: 0x040044E4 RID: 17636
			internal string Namespace;
		}

		// Token: 0x02000C9C RID: 3228
		internal class QNameComparer : IEqualityComparer<OperationFormatter.QName>
		{
			// Token: 0x06007907 RID: 30983 RVA: 0x001C3B4F File Offset: 0x001C1D4F
			private QNameComparer()
			{
			}

			// Token: 0x06007908 RID: 30984 RVA: 0x001C3B57 File Offset: 0x001C1D57
			public bool Equals(OperationFormatter.QName x, OperationFormatter.QName y)
			{
				return x.Name == y.Name && x.Namespace == y.Namespace;
			}

			// Token: 0x06007909 RID: 30985 RVA: 0x001C3B7F File Offset: 0x001C1D7F
			public int GetHashCode(OperationFormatter.QName obj)
			{
				return obj.Name.GetHashCode();
			}

			// Token: 0x040044E5 RID: 17637
			internal static OperationFormatter.QNameComparer Singleton = new OperationFormatter.QNameComparer();
		}

		// Token: 0x02000C9D RID: 3229
		internal class MessageHeaderDescriptionTable : Dictionary<OperationFormatter.QName, MessageHeaderDescription>
		{
			// Token: 0x0600790B RID: 30987 RVA: 0x001C3B98 File Offset: 0x001C1D98
			internal MessageHeaderDescriptionTable() : base(OperationFormatter.QNameComparer.Singleton)
			{
			}

			// Token: 0x0600790C RID: 30988 RVA: 0x001C3BA5 File Offset: 0x001C1DA5
			internal void Add(string name, string ns, MessageHeaderDescription message)
			{
				base.Add(new OperationFormatter.QName(name, ns), message);
			}

			// Token: 0x0600790D RID: 30989 RVA: 0x001C3BB8 File Offset: 0x001C1DB8
			internal MessageHeaderDescription Get(string name, string ns)
			{
				MessageHeaderDescription result;
				if (base.TryGetValue(new OperationFormatter.QName(name, ns), out result))
				{
					return result;
				}
				return null;
			}
		}
	}
}
