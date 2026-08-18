using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Security;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Xml;
using System.Xml.Serialization;

namespace System.ServiceModel.Description
{
	// Token: 0x020003DF RID: 991
	public class XmlSerializerOperationBehavior : IOperationBehavior, IWsdlExportExtension
	{
		// Token: 0x06002551 RID: 9553 RVA: 0x000859E1 File Offset: 0x00083BE1
		public XmlSerializerOperationBehavior(OperationDescription operation) : this(operation, null)
		{
		}

		// Token: 0x06002552 RID: 9554 RVA: 0x000859EC File Offset: 0x00083BEC
		public XmlSerializerOperationBehavior(OperationDescription operation, XmlSerializerFormatAttribute attribute)
		{
			if (operation == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("operation");
			}
			XmlSerializerOperationBehavior.Reflector reflector = new XmlSerializerOperationBehavior.Reflector(operation.DeclaringContract.Namespace, operation.DeclaringContract.ContractType);
			this.reflector = reflector.ReflectOperation(operation, attribute ?? new XmlSerializerFormatAttribute());
		}

		// Token: 0x06002553 RID: 9555 RVA: 0x00085A45 File Offset: 0x00083C45
		internal XmlSerializerOperationBehavior(OperationDescription operation, XmlSerializerFormatAttribute attribute, XmlSerializerOperationBehavior.Reflector parentReflector) : this(operation, attribute)
		{
			this.reflector = parentReflector.ReflectOperation(operation, attribute ?? new XmlSerializerFormatAttribute());
		}

		// Token: 0x06002554 RID: 9556 RVA: 0x00085A66 File Offset: 0x00083C66
		private XmlSerializerOperationBehavior(XmlSerializerOperationBehavior.Reflector.OperationReflector reflector, bool builtInOperationBehavior)
		{
			this.reflector = reflector;
			this.builtInOperationBehavior = builtInOperationBehavior;
		}

		// Token: 0x1700097E RID: 2430
		// (get) Token: 0x06002555 RID: 9557 RVA: 0x00085A7C File Offset: 0x00083C7C
		internal XmlSerializerOperationBehavior.Reflector.OperationReflector OperationReflector
		{
			get
			{
				return this.reflector;
			}
		}

		// Token: 0x1700097F RID: 2431
		// (get) Token: 0x06002556 RID: 9558 RVA: 0x00085A84 File Offset: 0x00083C84
		internal bool IsBuiltInOperationBehavior
		{
			get
			{
				return this.builtInOperationBehavior;
			}
		}

		// Token: 0x17000980 RID: 2432
		// (get) Token: 0x06002557 RID: 9559 RVA: 0x00085A8C File Offset: 0x00083C8C
		public XmlSerializerFormatAttribute XmlSerializerFormatAttribute
		{
			get
			{
				return this.reflector.Attribute;
			}
		}

		// Token: 0x06002558 RID: 9560 RVA: 0x00085A99 File Offset: 0x00083C99
		internal static XmlSerializerOperationFormatter CreateOperationFormatter(OperationDescription operation)
		{
			return new XmlSerializerOperationBehavior(operation).CreateFormatter();
		}

		// Token: 0x06002559 RID: 9561 RVA: 0x00085AA6 File Offset: 0x00083CA6
		internal static XmlSerializerOperationFormatter CreateOperationFormatter(OperationDescription operation, XmlSerializerFormatAttribute attr)
		{
			return new XmlSerializerOperationBehavior(operation, attr).CreateFormatter();
		}

		// Token: 0x0600255A RID: 9562 RVA: 0x00085AB4 File Offset: 0x00083CB4
		internal static void AddBehaviors(ContractDescription contract)
		{
			XmlSerializerOperationBehavior.AddBehaviors(contract, false);
		}

		// Token: 0x0600255B RID: 9563 RVA: 0x00085ABD File Offset: 0x00083CBD
		internal static void AddBuiltInBehaviors(ContractDescription contract)
		{
			XmlSerializerOperationBehavior.AddBehaviors(contract, true);
		}

		// Token: 0x0600255C RID: 9564 RVA: 0x00085AC8 File Offset: 0x00083CC8
		private static void AddBehaviors(ContractDescription contract, bool builtInOperationBehavior)
		{
			XmlSerializerOperationBehavior.Reflector reflector = new XmlSerializerOperationBehavior.Reflector(contract.Namespace, contract.ContractType);
			foreach (OperationDescription operationDescription in contract.Operations)
			{
				XmlSerializerOperationBehavior.Reflector.OperationReflector operationReflector = reflector.ReflectOperation(operationDescription);
				if (operationReflector != null && operationDescription.DeclaringContract == contract)
				{
					operationDescription.Behaviors.Add(new XmlSerializerOperationBehavior(operationReflector, builtInOperationBehavior));
					operationDescription.Behaviors.Add(new XmlSerializerOperationGenerator(new XmlSerializerImportOptions()));
				}
			}
		}

		// Token: 0x0600255D RID: 9565 RVA: 0x00085B64 File Offset: 0x00083D64
		internal XmlSerializerOperationFormatter CreateFormatter()
		{
			return new XmlSerializerOperationFormatter(this.reflector.Operation, this.reflector.Attribute, this.reflector.Request, this.reflector.Reply);
		}

		// Token: 0x0600255E RID: 9566 RVA: 0x00085B97 File Offset: 0x00083D97
		private XmlSerializerFaultFormatter CreateFaultFormatter(SynchronizedCollection<FaultContractInfo> faultContractInfos)
		{
			return new XmlSerializerFaultFormatter(faultContractInfos, this.reflector.XmlSerializerFaultContractInfos);
		}

		// Token: 0x0600255F RID: 9567 RVA: 0x00085BAA File Offset: 0x00083DAA
		void IOperationBehavior.Validate(OperationDescription description)
		{
		}

		// Token: 0x06002560 RID: 9568 RVA: 0x00085BAC File Offset: 0x00083DAC
		void IOperationBehavior.AddBindingParameters(OperationDescription description, BindingParameterCollection parameters)
		{
		}

		// Token: 0x06002561 RID: 9569 RVA: 0x00085BB0 File Offset: 0x00083DB0
		void IOperationBehavior.ApplyDispatchBehavior(OperationDescription description, DispatchOperation dispatch)
		{
			if (description == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("description");
			}
			if (dispatch == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("dispatch");
			}
			if (dispatch.Formatter == null)
			{
				dispatch.Formatter = this.CreateFormatter();
				dispatch.DeserializeRequest = this.reflector.RequestRequiresSerialization;
				dispatch.SerializeReply = this.reflector.ReplyRequiresSerialization;
			}
			if (this.reflector.Attribute.SupportFaults)
			{
				if (!dispatch.IsFaultFormatterSetExplicit)
				{
					dispatch.FaultFormatter = this.CreateFaultFormatter(dispatch.FaultContractInfos);
					return;
				}
				IDispatchFaultFormatterWrapper dispatchFaultFormatterWrapper = dispatch.FaultFormatter as IDispatchFaultFormatterWrapper;
				if (dispatchFaultFormatterWrapper != null)
				{
					dispatchFaultFormatterWrapper.InnerFaultFormatter = this.CreateFaultFormatter(dispatch.FaultContractInfos);
				}
			}
		}

		// Token: 0x06002562 RID: 9570 RVA: 0x00085C68 File Offset: 0x00083E68
		void IOperationBehavior.ApplyClientBehavior(OperationDescription description, ClientOperation proxy)
		{
			if (description == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("description");
			}
			if (proxy == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("proxy");
			}
			if (proxy.Formatter == null)
			{
				proxy.Formatter = this.CreateFormatter();
				proxy.SerializeRequest = this.reflector.RequestRequiresSerialization;
				proxy.DeserializeReply = this.reflector.ReplyRequiresSerialization;
			}
			if (this.reflector.Attribute.SupportFaults && !proxy.IsFaultFormatterSetExplicit)
			{
				proxy.FaultFormatter = this.CreateFaultFormatter(proxy.FaultContractInfos);
			}
		}

		// Token: 0x06002563 RID: 9571 RVA: 0x00085D00 File Offset: 0x00083F00
		void IWsdlExportExtension.ExportEndpoint(WsdlExporter exporter, WsdlEndpointConversionContext endpointContext)
		{
			if (exporter == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("exporter");
			}
			if (endpointContext == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("endpointContext");
			}
			MessageContractExporter.ExportMessageBinding(exporter, endpointContext, typeof(XmlSerializerMessageContractExporter), this.reflector.Operation);
		}

		// Token: 0x06002564 RID: 9572 RVA: 0x00085D4F File Offset: 0x00083F4F
		void IWsdlExportExtension.ExportContract(WsdlExporter exporter, WsdlContractConversionContext contractContext)
		{
			if (exporter == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("exporter");
			}
			if (contractContext == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("contractContext");
			}
			new XmlSerializerMessageContractExporter(exporter, contractContext, this.reflector.Operation, this).ExportMessageContract();
		}

		// Token: 0x06002565 RID: 9573 RVA: 0x00085D90 File Offset: 0x00083F90
		public Collection<XmlMapping> GetXmlMappings()
		{
			Collection<XmlMapping> collection = new Collection<XmlMapping>();
			if (this.OperationReflector.Request != null && this.OperationReflector.Request.HeadersMapping != null)
			{
				collection.Add(this.OperationReflector.Request.HeadersMapping);
			}
			if (this.OperationReflector.Request != null && this.OperationReflector.Request.BodyMapping != null)
			{
				collection.Add(this.OperationReflector.Request.BodyMapping);
			}
			if (this.OperationReflector.Reply != null && this.OperationReflector.Reply.HeadersMapping != null)
			{
				collection.Add(this.OperationReflector.Reply.HeadersMapping);
			}
			if (this.OperationReflector.Reply != null && this.OperationReflector.Reply.BodyMapping != null)
			{
				collection.Add(this.OperationReflector.Reply.BodyMapping);
			}
			return collection;
		}

		// Token: 0x040020C4 RID: 8388
		private readonly XmlSerializerOperationBehavior.Reflector.OperationReflector reflector;

		// Token: 0x040020C5 RID: 8389
		private readonly bool builtInOperationBehavior;

		// Token: 0x02000B9F RID: 2975
		internal class Reflector
		{
			// Token: 0x0600739F RID: 29599 RVA: 0x001AF8DE File Offset: 0x001ADADE
			internal Reflector(string defaultNs, Type type)
			{
				this.importer = new XmlSerializerOperationBehavior.Reflector.XmlSerializerImporter(defaultNs);
				this.generation = new XmlSerializerOperationBehavior.Reflector.SerializerGenerationContext(type);
			}

			// Token: 0x060073A0 RID: 29600 RVA: 0x001AF914 File Offset: 0x001ADB14
			internal void EnsureMessageInfos()
			{
				object obj = this.thisLock;
				lock (obj)
				{
					foreach (XmlSerializerOperationBehavior.Reflector.OperationReflector operationReflector in this.operationReflectors)
					{
						operationReflector.EnsureMessageInfos();
					}
				}
			}

			// Token: 0x060073A1 RID: 29601 RVA: 0x001AF988 File Offset: 0x001ADB88
			private static XmlSerializerFormatAttribute FindAttribute(OperationDescription operation)
			{
				Type type = (operation.DeclaringContract != null) ? operation.DeclaringContract.ContractType : null;
				XmlSerializerFormatAttribute defaultFormatAttribute = (type != null) ? (TypeLoader.GetFormattingAttribute(type, null) as XmlSerializerFormatAttribute) : null;
				return TypeLoader.GetFormattingAttribute(operation.OperationMethod, defaultFormatAttribute) as XmlSerializerFormatAttribute;
			}

			// Token: 0x060073A2 RID: 29602 RVA: 0x001AF9D8 File Offset: 0x001ADBD8
			internal XmlSerializerOperationBehavior.Reflector.OperationReflector ReflectOperation(OperationDescription operation)
			{
				XmlSerializerFormatAttribute xmlSerializerFormatAttribute = XmlSerializerOperationBehavior.Reflector.FindAttribute(operation);
				if (xmlSerializerFormatAttribute == null)
				{
					return null;
				}
				return this.ReflectOperation(operation, xmlSerializerFormatAttribute);
			}

			// Token: 0x060073A3 RID: 29603 RVA: 0x001AF9FC File Offset: 0x001ADBFC
			internal XmlSerializerOperationBehavior.Reflector.OperationReflector ReflectOperation(OperationDescription operation, XmlSerializerFormatAttribute attrOverride)
			{
				XmlSerializerOperationBehavior.Reflector.OperationReflector operationReflector = new XmlSerializerOperationBehavior.Reflector.OperationReflector(this, operation, attrOverride, true);
				this.operationReflectors.Add(operationReflector);
				return operationReflector;
			}

			// Token: 0x0400417E RID: 16766
			private readonly XmlSerializerOperationBehavior.Reflector.XmlSerializerImporter importer;

			// Token: 0x0400417F RID: 16767
			private readonly XmlSerializerOperationBehavior.Reflector.SerializerGenerationContext generation;

			// Token: 0x04004180 RID: 16768
			private Collection<XmlSerializerOperationBehavior.Reflector.OperationReflector> operationReflectors = new Collection<XmlSerializerOperationBehavior.Reflector.OperationReflector>();

			// Token: 0x04004181 RID: 16769
			private object thisLock = new object();

			// Token: 0x02000EFE RID: 3838
			internal class OperationReflector
			{
				// Token: 0x06008575 RID: 34165 RVA: 0x001EDE74 File Offset: 0x001EC074
				internal OperationReflector(XmlSerializerOperationBehavior.Reflector parent, OperationDescription operation, XmlSerializerFormatAttribute attr, bool reflectOnDemand)
				{
					OperationFormatter.Validate(operation, attr.Style == OperationFormatStyle.Rpc, attr.IsEncoded);
					this.parent = parent;
					this.Operation = operation;
					this.Attribute = attr;
					this.IsEncoded = attr.IsEncoded;
					this.IsRpc = (attr.Style == OperationFormatStyle.Rpc);
					this.IsOneWay = (operation.Messages.Count == 1);
					this.RequestRequiresSerialization = !operation.Messages[0].IsUntypedMessage;
					this.ReplyRequiresSerialization = (!this.IsOneWay && !operation.Messages[1].IsUntypedMessage);
					MethodInfo operationMethod = operation.OperationMethod;
					if (operationMethod == null)
					{
						this.keyBase = string.Empty;
						if (operation.DeclaringContract != null)
						{
							this.keyBase = operation.DeclaringContract.Name + "," + operation.DeclaringContract.Namespace + ":";
						}
						this.keyBase += operation.Name;
					}
					else
					{
						this.keyBase = operationMethod.DeclaringType.FullName + ":" + operationMethod.ToString();
					}
					foreach (MessageDescription messageDescription in operation.Messages)
					{
						foreach (MessageHeaderDescription unknownHeaderInDescription in messageDescription.Headers)
						{
							this.SetUnknownHeaderInDescription(unknownHeaderInDescription);
						}
					}
					if (!reflectOnDemand)
					{
						this.EnsureMessageInfos();
					}
				}

				// Token: 0x06008576 RID: 34166 RVA: 0x001EE028 File Offset: 0x001EC228
				private void SetUnknownHeaderInDescription(MessageHeaderDescription header)
				{
					if (this.IsEncoded)
					{
						return;
					}
					if (header.AdditionalAttributesProvider != null)
					{
						XmlAttributes xmlAttributes = new XmlAttributes(header.AdditionalAttributesProvider);
						foreach (object obj in xmlAttributes.XmlAnyElements)
						{
							XmlAnyElementAttribute xmlAnyElementAttribute = (XmlAnyElementAttribute)obj;
							if (string.IsNullOrEmpty(xmlAnyElementAttribute.Name))
							{
								header.IsUnknownHeaderCollection = true;
							}
						}
					}
				}

				// Token: 0x17001D51 RID: 7505
				// (get) Token: 0x06008577 RID: 34167 RVA: 0x001EE0AC File Offset: 0x001EC2AC
				private string ContractName
				{
					get
					{
						return this.Operation.DeclaringContract.Name;
					}
				}

				// Token: 0x17001D52 RID: 7506
				// (get) Token: 0x06008578 RID: 34168 RVA: 0x001EE0BE File Offset: 0x001EC2BE
				private string ContractNamespace
				{
					get
					{
						return this.Operation.DeclaringContract.Namespace;
					}
				}

				// Token: 0x17001D53 RID: 7507
				// (get) Token: 0x06008579 RID: 34169 RVA: 0x001EE0D0 File Offset: 0x001EC2D0
				internal XmlSerializerOperationBehavior.Reflector.MessageInfo Request
				{
					get
					{
						this.parent.EnsureMessageInfos();
						return this.request;
					}
				}

				// Token: 0x17001D54 RID: 7508
				// (get) Token: 0x0600857A RID: 34170 RVA: 0x001EE0E3 File Offset: 0x001EC2E3
				internal XmlSerializerOperationBehavior.Reflector.MessageInfo Reply
				{
					get
					{
						this.parent.EnsureMessageInfos();
						return this.reply;
					}
				}

				// Token: 0x17001D55 RID: 7509
				// (get) Token: 0x0600857B RID: 34171 RVA: 0x001EE0F6 File Offset: 0x001EC2F6
				internal SynchronizedCollection<XmlSerializerOperationBehavior.Reflector.XmlSerializerFaultContractInfo> XmlSerializerFaultContractInfos
				{
					get
					{
						this.parent.EnsureMessageInfos();
						return this.xmlSerializerFaultContractInfos;
					}
				}

				// Token: 0x0600857C RID: 34172 RVA: 0x001EE10C File Offset: 0x001EC30C
				internal void EnsureMessageInfos()
				{
					if (this.request == null)
					{
						foreach (Type type in this.Operation.KnownTypes)
						{
							if (type == null)
							{
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxKnownTypeNull", new object[]
								{
									this.Operation.Name
								})));
							}
							this.parent.importer.IncludeType(type, this.IsEncoded);
						}
						this.request = this.CreateMessageInfo(this.Operation.Messages[0], ":Request");
						if (this.request != null && this.IsRpc && this.Operation.IsValidateRpcWrapperName && this.request.BodyMapping.XsdElementName != this.Operation.Name)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxRpcMessageBodyPartNameInvalid", new object[]
							{
								this.Operation.Name,
								this.Operation.Messages[0].MessageName,
								this.request.BodyMapping.XsdElementName,
								this.Operation.Name
							})));
						}
						if (!this.IsOneWay)
						{
							this.reply = this.CreateMessageInfo(this.Operation.Messages[1], ":Response");
							XmlName bodyWrapperResponseName = TypeLoader.GetBodyWrapperResponseName(this.Operation.Name);
							if (this.reply != null && this.IsRpc && this.Operation.IsValidateRpcWrapperName && this.reply.BodyMapping.XsdElementName != bodyWrapperResponseName.EncodedName)
							{
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxRpcMessageBodyPartNameInvalid", new object[]
								{
									this.Operation.Name,
									this.Operation.Messages[1].MessageName,
									this.reply.BodyMapping.XsdElementName,
									bodyWrapperResponseName.EncodedName
								})));
							}
						}
						if (this.Attribute.SupportFaults)
						{
							this.GenerateXmlSerializerFaultContractInfos();
						}
					}
				}

				// Token: 0x0600857D RID: 34173 RVA: 0x001EE37C File Offset: 0x001EC57C
				private void GenerateXmlSerializerFaultContractInfos()
				{
					SynchronizedCollection<XmlSerializerOperationBehavior.Reflector.XmlSerializerFaultContractInfo> synchronizedCollection = new SynchronizedCollection<XmlSerializerOperationBehavior.Reflector.XmlSerializerFaultContractInfo>();
					for (int i = 0; i < this.Operation.Faults.Count; i++)
					{
						FaultDescription faultDescription = this.Operation.Faults[i];
						FaultContractInfo faultContractInfo = new FaultContractInfo(faultDescription.Action, faultDescription.DetailType, faultDescription.ElementName, faultDescription.Namespace, this.Operation.KnownTypes);
						XmlQualifiedName faultContractElementName;
						XmlMembersMapping mapping = this.ImportFaultElement(faultDescription, out faultContractElementName);
						XmlSerializerOperationBehavior.Reflector.SerializerStub serializerStub = this.parent.generation.AddSerializer(mapping);
						synchronizedCollection.Add(new XmlSerializerOperationBehavior.Reflector.XmlSerializerFaultContractInfo(faultContractInfo, serializerStub, faultContractElementName));
					}
					this.xmlSerializerFaultContractInfos = synchronizedCollection;
				}

				// Token: 0x0600857E RID: 34174 RVA: 0x001EE420 File Offset: 0x001EC620
				private XmlSerializerOperationBehavior.Reflector.MessageInfo CreateMessageInfo(MessageDescription message, string key)
				{
					if (message.IsUntypedMessage)
					{
						return null;
					}
					XmlSerializerOperationBehavior.Reflector.MessageInfo messageInfo = new XmlSerializerOperationBehavior.Reflector.MessageInfo();
					if (message.IsTypedMessage)
					{
						key = string.Concat(new string[]
						{
							message.MessageType.FullName,
							":",
							this.IsEncoded.ToString(),
							":",
							this.IsRpc.ToString()
						});
					}
					XmlMembersMapping xmlMembersMapping = this.LoadHeadersMapping(message, key + ":Headers");
					messageInfo.SetHeaders(this.parent.generation.AddSerializer(xmlMembersMapping));
					MessagePartDescriptionCollection rpcEncodedTypedMessageBodyParts;
					messageInfo.SetBody(this.parent.generation.AddSerializer(this.LoadBodyMapping(message, key, out rpcEncodedTypedMessageBodyParts)), rpcEncodedTypedMessageBodyParts);
					this.CreateHeaderDescriptionTable(message, messageInfo, xmlMembersMapping);
					return messageInfo;
				}

				// Token: 0x0600857F RID: 34175 RVA: 0x001EE4E8 File Offset: 0x001EC6E8
				private void CreateHeaderDescriptionTable(MessageDescription message, XmlSerializerOperationBehavior.Reflector.MessageInfo info, XmlMembersMapping headersMapping)
				{
					int num = 0;
					OperationFormatter.MessageHeaderDescriptionTable messageHeaderDescriptionTable = new OperationFormatter.MessageHeaderDescriptionTable();
					info.SetHeaderDescriptionTable(messageHeaderDescriptionTable);
					foreach (MessageHeaderDescription messageHeaderDescription in message.Headers)
					{
						if (messageHeaderDescription.IsUnknownHeaderCollection)
						{
							info.SetUnknownHeaderDescription(messageHeaderDescription);
						}
						else if (headersMapping != null)
						{
							XmlMemberMapping xmlMemberMapping = headersMapping[num++];
							string text;
							string text2;
							if (this.IsEncoded)
							{
								text = xmlMemberMapping.TypeName;
								text2 = xmlMemberMapping.TypeNamespace;
							}
							else
							{
								text = xmlMemberMapping.XsdElementName;
								text2 = xmlMemberMapping.Namespace;
							}
							if (text != messageHeaderDescription.Name)
							{
								if (message.MessageType != null)
								{
									throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxHeaderNameMismatchInMessageContract", new object[]
									{
										message.MessageType,
										messageHeaderDescription.MemberInfo.Name,
										messageHeaderDescription.Name,
										text
									})));
								}
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxHeaderNameMismatchInOperation", new object[]
								{
									this.Operation.Name,
									this.Operation.DeclaringContract.Name,
									this.Operation.DeclaringContract.Namespace,
									messageHeaderDescription.Name,
									text
								})));
							}
							else if (text2 != messageHeaderDescription.Namespace)
							{
								if (message.MessageType != null)
								{
									throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxHeaderNamespaceMismatchInMessageContract", new object[]
									{
										message.MessageType,
										messageHeaderDescription.MemberInfo.Name,
										messageHeaderDescription.Namespace,
										text2
									})));
								}
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxHeaderNamespaceMismatchInOperation", new object[]
								{
									this.Operation.Name,
									this.Operation.DeclaringContract.Name,
									this.Operation.DeclaringContract.Namespace,
									messageHeaderDescription.Namespace,
									text2
								})));
							}
							else
							{
								messageHeaderDescriptionTable.Add(text, text2, messageHeaderDescription);
							}
						}
					}
				}

				// Token: 0x06008580 RID: 34176 RVA: 0x001EE740 File Offset: 0x001EC940
				private XmlMembersMapping LoadBodyMapping(MessageDescription message, string mappingKey, out MessagePartDescriptionCollection rpcEncodedTypedMessageBodyParts)
				{
					MessagePartDescription messagePartDescription;
					MessagePartDescriptionCollection messagePartDescriptionCollection;
					string text;
					string ns;
					if (this.IsEncoded && message.IsTypedMessage && message.Body.WrapperName == null)
					{
						MessagePartDescription wrapperPart = this.GetWrapperPart(message);
						messagePartDescription = null;
						rpcEncodedTypedMessageBodyParts = (messagePartDescriptionCollection = this.GetWrappedParts(wrapperPart));
						text = wrapperPart.Name;
						ns = wrapperPart.Namespace;
					}
					else
					{
						rpcEncodedTypedMessageBodyParts = null;
						messagePartDescription = (OperationFormatter.IsValidReturnValue(message.Body.ReturnValue) ? message.Body.ReturnValue : null);
						messagePartDescriptionCollection = message.Body.Parts;
						text = message.Body.WrapperName;
						ns = message.Body.WrapperNamespace;
					}
					bool flag = text != null;
					bool flag2 = messagePartDescription != null;
					int num = messagePartDescriptionCollection.Count + (flag2 ? 1 : 0);
					if (num == 0 && !flag)
					{
						return null;
					}
					XmlReflectionMember[] array = new XmlReflectionMember[num];
					int num2 = 0;
					if (flag2)
					{
						array[num2++] = XmlSerializerHelper.GetXmlReflectionMember(messagePartDescription, this.IsRpc, this.IsEncoded, flag);
					}
					for (int i = 0; i < messagePartDescriptionCollection.Count; i++)
					{
						array[num2++] = XmlSerializerHelper.GetXmlReflectionMember(messagePartDescriptionCollection[i], this.IsRpc, this.IsEncoded, flag);
					}
					if (!flag)
					{
						ns = this.ContractNamespace;
					}
					return this.ImportMembersMapping(text, ns, array, flag, this.IsRpc, mappingKey);
				}

				// Token: 0x06008581 RID: 34177 RVA: 0x001EE888 File Offset: 0x001ECA88
				private MessagePartDescription GetWrapperPart(MessageDescription message)
				{
					if (message.Body.Parts.Count != 1)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxRpcMessageMustHaveASingleBody", new object[]
						{
							this.Operation.Name,
							message.MessageName
						})));
					}
					MessagePartDescription messagePartDescription = message.Body.Parts[0];
					Type type = messagePartDescription.Type;
					if (type.BaseType != null && type.BaseType != typeof(object))
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxBodyObjectTypeCannotBeInherited", new object[]
						{
							type.FullName
						})));
					}
					if (typeof(IEnumerable).IsAssignableFrom(type))
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxBodyObjectTypeCannotBeInterface", new object[]
						{
							type.FullName,
							typeof(IEnumerable).FullName
						})));
					}
					if (typeof(IXmlSerializable).IsAssignableFrom(type))
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxBodyObjectTypeCannotBeInterface", new object[]
						{
							type.FullName,
							typeof(IXmlSerializable).FullName
						})));
					}
					return messagePartDescription;
				}

				// Token: 0x06008582 RID: 34178 RVA: 0x001EE9E4 File Offset: 0x001ECBE4
				private MessagePartDescriptionCollection GetWrappedParts(MessagePartDescription bodyPart)
				{
					Type type = bodyPart.Type;
					MessagePartDescriptionCollection messagePartDescriptionCollection = new MessagePartDescriptionCollection();
					foreach (MemberInfo memberInfo in type.GetMembers(BindingFlags.Instance | BindingFlags.Public))
					{
						if ((memberInfo.MemberType & (MemberTypes.Field | MemberTypes.Property)) != (MemberTypes)0 && !memberInfo.IsDefined(typeof(SoapIgnoreAttribute), false))
						{
							XmlName xmlName = new XmlName(memberInfo.Name);
							MessagePartDescription messagePartDescription = new MessagePartDescription(xmlName.EncodedName, string.Empty);
							messagePartDescription.AdditionalAttributesProvider = (messagePartDescription.MemberInfo = memberInfo);
							messagePartDescription.Index = (messagePartDescription.SerializationPosition = messagePartDescriptionCollection.Count);
							messagePartDescription.Type = ((memberInfo.MemberType == MemberTypes.Property) ? ((PropertyInfo)memberInfo).PropertyType : ((FieldInfo)memberInfo).FieldType);
							if (bodyPart.HasProtectionLevel)
							{
								messagePartDescription.ProtectionLevel = bodyPart.ProtectionLevel;
							}
							messagePartDescriptionCollection.Add(messagePartDescription);
						}
					}
					return messagePartDescriptionCollection;
				}

				// Token: 0x06008583 RID: 34179 RVA: 0x001EEAE0 File Offset: 0x001ECCE0
				private XmlMembersMapping LoadHeadersMapping(MessageDescription message, string mappingKey)
				{
					int count = message.Headers.Count;
					if (count == 0)
					{
						return null;
					}
					if (this.IsEncoded)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxHeadersAreNotSupportedInEncoded", new object[]
						{
							message.MessageName
						})));
					}
					int num = 0;
					int num2 = 0;
					XmlReflectionMember[] array = new XmlReflectionMember[count];
					for (int i = 0; i < count; i++)
					{
						MessageHeaderDescription messageHeaderDescription = message.Headers[i];
						if (!messageHeaderDescription.IsUnknownHeaderCollection)
						{
							array[num2++] = XmlSerializerHelper.GetXmlReflectionMember(messageHeaderDescription, false, this.IsEncoded, false);
						}
						else
						{
							num++;
						}
					}
					if (num == count)
					{
						return null;
					}
					if (num > 0)
					{
						XmlReflectionMember[] array2 = new XmlReflectionMember[count - num];
						Array.Copy(array, array2, array2.Length);
						array = array2;
					}
					return this.ImportMembersMapping(this.ContractName, this.ContractNamespace, array, false, false, mappingKey);
				}

				// Token: 0x06008584 RID: 34180 RVA: 0x001EEBB8 File Offset: 0x001ECDB8
				internal XmlMembersMapping ImportMembersMapping(string elementName, string ns, XmlReflectionMember[] members, bool hasWrapperElement, bool rpc, string mappingKey)
				{
					string mappingKey2 = mappingKey.StartsWith(":", StringComparison.Ordinal) ? (this.keyBase + mappingKey) : mappingKey;
					return this.parent.importer.ImportMembersMapping(new XmlName(elementName, true), ns, members, hasWrapperElement, rpc, this.IsEncoded, mappingKey2);
				}

				// Token: 0x06008585 RID: 34181 RVA: 0x001EEC0C File Offset: 0x001ECE0C
				internal XmlMembersMapping ImportFaultElement(FaultDescription fault, out XmlQualifiedName elementName)
				{
					XmlReflectionMember[] array = new XmlReflectionMember[1];
					XmlName xmlName = fault.ElementName;
					string @namespace = fault.Namespace;
					if (xmlName == null)
					{
						XmlTypeMapping xmlTypeMapping = this.parent.importer.ImportTypeMapping(fault.DetailType, this.IsEncoded);
						xmlName = new XmlName(xmlTypeMapping.ElementName, this.IsEncoded);
						@namespace = xmlTypeMapping.Namespace;
						if (xmlName == null)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxFaultTypeAnonymous", new object[]
							{
								this.Operation.Name,
								fault.DetailType.FullName
							})));
						}
					}
					elementName = new XmlQualifiedName(xmlName.DecodedName, @namespace);
					array[0] = XmlSerializerHelper.GetXmlReflectionMember(null, xmlName, @namespace, fault.DetailType, null, false, this.IsEncoded, false);
					string mappingKey = "fault:" + xmlName.DecodedName + ":" + @namespace;
					return this.ImportMembersMapping(xmlName.EncodedName, @namespace, array, false, this.IsRpc, mappingKey);
				}

				// Token: 0x04004D48 RID: 19784
				private readonly XmlSerializerOperationBehavior.Reflector parent;

				// Token: 0x04004D49 RID: 19785
				internal readonly OperationDescription Operation;

				// Token: 0x04004D4A RID: 19786
				internal readonly XmlSerializerFormatAttribute Attribute;

				// Token: 0x04004D4B RID: 19787
				internal readonly bool IsEncoded;

				// Token: 0x04004D4C RID: 19788
				internal readonly bool IsRpc;

				// Token: 0x04004D4D RID: 19789
				internal readonly bool IsOneWay;

				// Token: 0x04004D4E RID: 19790
				internal readonly bool RequestRequiresSerialization;

				// Token: 0x04004D4F RID: 19791
				internal readonly bool ReplyRequiresSerialization;

				// Token: 0x04004D50 RID: 19792
				private readonly string keyBase;

				// Token: 0x04004D51 RID: 19793
				private XmlSerializerOperationBehavior.Reflector.MessageInfo request;

				// Token: 0x04004D52 RID: 19794
				private XmlSerializerOperationBehavior.Reflector.MessageInfo reply;

				// Token: 0x04004D53 RID: 19795
				private SynchronizedCollection<XmlSerializerOperationBehavior.Reflector.XmlSerializerFaultContractInfo> xmlSerializerFaultContractInfos;
			}

			// Token: 0x02000EFF RID: 3839
			private class XmlSerializerImporter
			{
				// Token: 0x06008586 RID: 34182 RVA: 0x001EED0A File Offset: 0x001ECF0A
				internal XmlSerializerImporter(string defaultNs)
				{
					this.defaultNs = defaultNs;
					this.xmlImporter = null;
					this.soapImporter = null;
				}

				// Token: 0x17001D56 RID: 7510
				// (get) Token: 0x06008587 RID: 34183 RVA: 0x001EED27 File Offset: 0x001ECF27
				private SoapReflectionImporter SoapImporter
				{
					get
					{
						if (this.soapImporter == null)
						{
							this.soapImporter = new SoapReflectionImporter(NamingHelper.CombineUriStrings(this.defaultNs, "encoded"));
						}
						return this.soapImporter;
					}
				}

				// Token: 0x17001D57 RID: 7511
				// (get) Token: 0x06008588 RID: 34184 RVA: 0x001EED52 File Offset: 0x001ECF52
				private XmlReflectionImporter XmlImporter
				{
					get
					{
						if (this.xmlImporter == null)
						{
							this.xmlImporter = new XmlReflectionImporter(this.defaultNs);
						}
						return this.xmlImporter;
					}
				}

				// Token: 0x17001D58 RID: 7512
				// (get) Token: 0x06008589 RID: 34185 RVA: 0x001EED73 File Offset: 0x001ECF73
				private Dictionary<string, XmlMembersMapping> XmlMappings
				{
					get
					{
						if (this.xmlMappings == null)
						{
							this.xmlMappings = new Dictionary<string, XmlMembersMapping>();
						}
						return this.xmlMappings;
					}
				}

				// Token: 0x0600858A RID: 34186 RVA: 0x001EED90 File Offset: 0x001ECF90
				internal XmlMembersMapping ImportMembersMapping(XmlName elementName, string ns, XmlReflectionMember[] members, bool hasWrapperElement, bool rpc, bool isEncoded, string mappingKey)
				{
					string decodedName = elementName.DecodedName;
					XmlMembersMapping xmlMembersMapping;
					if (this.XmlMappings.TryGetValue(mappingKey, out xmlMembersMapping))
					{
						return xmlMembersMapping;
					}
					if (isEncoded)
					{
						xmlMembersMapping = this.SoapImporter.ImportMembersMapping(decodedName, ns, members, hasWrapperElement, rpc);
					}
					else
					{
						xmlMembersMapping = this.XmlImporter.ImportMembersMapping(decodedName, ns, members, hasWrapperElement, rpc);
					}
					xmlMembersMapping.SetKey(mappingKey);
					this.XmlMappings.Add(mappingKey, xmlMembersMapping);
					return xmlMembersMapping;
				}

				// Token: 0x0600858B RID: 34187 RVA: 0x001EEDFA File Offset: 0x001ECFFA
				internal XmlTypeMapping ImportTypeMapping(Type type, bool isEncoded)
				{
					if (isEncoded)
					{
						return this.SoapImporter.ImportTypeMapping(type);
					}
					return this.XmlImporter.ImportTypeMapping(type);
				}

				// Token: 0x0600858C RID: 34188 RVA: 0x001EEE18 File Offset: 0x001ED018
				internal void IncludeType(Type knownType, bool isEncoded)
				{
					if (isEncoded)
					{
						this.SoapImporter.IncludeType(knownType);
						return;
					}
					this.XmlImporter.IncludeType(knownType);
				}

				// Token: 0x04004D54 RID: 19796
				private readonly string defaultNs;

				// Token: 0x04004D55 RID: 19797
				private XmlReflectionImporter xmlImporter;

				// Token: 0x04004D56 RID: 19798
				private SoapReflectionImporter soapImporter;

				// Token: 0x04004D57 RID: 19799
				private Dictionary<string, XmlMembersMapping> xmlMappings;
			}

			// Token: 0x02000F00 RID: 3840
			internal class SerializerGenerationContext
			{
				// Token: 0x0600858D RID: 34189 RVA: 0x001EEE36 File Offset: 0x001ED036
				internal SerializerGenerationContext(Type type)
				{
					this.type = type;
				}

				// Token: 0x0600858E RID: 34190 RVA: 0x001EEE5C File Offset: 0x001ED05C
				internal XmlSerializerOperationBehavior.Reflector.SerializerStub AddSerializer(XmlMembersMapping mapping)
				{
					int handle = -1;
					if (mapping != null)
					{
						handle = ((IList)this.Mappings).Add(mapping);
					}
					return new XmlSerializerOperationBehavior.Reflector.SerializerStub(this, mapping, handle);
				}

				// Token: 0x0600858F RID: 34191 RVA: 0x001EEE84 File Offset: 0x001ED084
				internal XmlSerializer GetSerializer(int handle)
				{
					if (handle < 0)
					{
						return null;
					}
					if (this.serializers == null)
					{
						object obj = this.thisLock;
						lock (obj)
						{
							if (this.serializers == null)
							{
								this.serializers = this.GenerateSerializers();
							}
						}
					}
					return this.serializers[handle];
				}

				// Token: 0x06008590 RID: 34192 RVA: 0x001EEEE8 File Offset: 0x001ED0E8
				private XmlSerializer[] GenerateSerializers()
				{
					List<XmlMembersMapping> list = new List<XmlMembersMapping>();
					int[] array = new int[this.Mappings.Count];
					for (int i = 0; i < this.Mappings.Count; i++)
					{
						XmlMembersMapping item = this.Mappings[i];
						int num = list.IndexOf(item);
						if (num < 0)
						{
							list.Add(item);
							num = list.Count - 1;
						}
						array[i] = num;
					}
					XmlMapping[] mappings = list.ToArray();
					XmlSerializer[] array2 = this.CreateSerializersFromMappings(mappings, this.type);
					if (list.Count == this.Mappings.Count)
					{
						return array2;
					}
					XmlSerializer[] array3 = new XmlSerializer[this.Mappings.Count];
					for (int j = 0; j < this.Mappings.Count; j++)
					{
						array3[j] = array2[array[j]];
					}
					return array3;
				}

				// Token: 0x06008591 RID: 34193 RVA: 0x001EEFBE File Offset: 0x001ED1BE
				[SecuritySafeCritical]
				private XmlSerializer[] CreateSerializersFromMappings(XmlMapping[] mappings, Type type)
				{
					return XmlSerializer.FromMappings(mappings, type);
				}

				// Token: 0x04004D58 RID: 19800
				private List<XmlMembersMapping> Mappings = new List<XmlMembersMapping>();

				// Token: 0x04004D59 RID: 19801
				private XmlSerializer[] serializers;

				// Token: 0x04004D5A RID: 19802
				private Type type;

				// Token: 0x04004D5B RID: 19803
				private object thisLock = new object();
			}

			// Token: 0x02000F01 RID: 3841
			internal struct SerializerStub
			{
				// Token: 0x06008592 RID: 34194 RVA: 0x001EEFC7 File Offset: 0x001ED1C7
				internal SerializerStub(XmlSerializerOperationBehavior.Reflector.SerializerGenerationContext context, XmlMembersMapping mapping, int handle)
				{
					this.context = context;
					this.Mapping = mapping;
					this.Handle = handle;
				}

				// Token: 0x06008593 RID: 34195 RVA: 0x001EEFDE File Offset: 0x001ED1DE
				internal XmlSerializer GetSerializer()
				{
					return this.context.GetSerializer(this.Handle);
				}

				// Token: 0x04004D5C RID: 19804
				private readonly XmlSerializerOperationBehavior.Reflector.SerializerGenerationContext context;

				// Token: 0x04004D5D RID: 19805
				internal readonly XmlMembersMapping Mapping;

				// Token: 0x04004D5E RID: 19806
				internal readonly int Handle;
			}

			// Token: 0x02000F02 RID: 3842
			internal class XmlSerializerFaultContractInfo
			{
				// Token: 0x06008594 RID: 34196 RVA: 0x001EEFF4 File Offset: 0x001ED1F4
				internal XmlSerializerFaultContractInfo(FaultContractInfo faultContractInfo, XmlSerializerOperationBehavior.Reflector.SerializerStub serializerStub, XmlQualifiedName faultContractElementName)
				{
					if (faultContractInfo == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("faultContractInfo");
					}
					if (faultContractElementName == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("faultContractElementName");
					}
					this.faultContractInfo = faultContractInfo;
					this.serializerStub = serializerStub;
					this.faultContractElementName = faultContractElementName;
				}

				// Token: 0x17001D59 RID: 7513
				// (get) Token: 0x06008595 RID: 34197 RVA: 0x001EF048 File Offset: 0x001ED248
				internal FaultContractInfo FaultContractInfo
				{
					get
					{
						return this.faultContractInfo;
					}
				}

				// Token: 0x17001D5A RID: 7514
				// (get) Token: 0x06008596 RID: 34198 RVA: 0x001EF050 File Offset: 0x001ED250
				internal XmlQualifiedName FaultContractElementName
				{
					get
					{
						return this.faultContractElementName;
					}
				}

				// Token: 0x17001D5B RID: 7515
				// (get) Token: 0x06008597 RID: 34199 RVA: 0x001EF058 File Offset: 0x001ED258
				internal XmlSerializerObjectSerializer Serializer
				{
					get
					{
						if (this.serializer == null)
						{
							this.serializer = new XmlSerializerObjectSerializer(this.faultContractInfo.Detail, this.faultContractElementName, this.serializerStub.GetSerializer());
						}
						return this.serializer;
					}
				}

				// Token: 0x04004D5F RID: 19807
				private FaultContractInfo faultContractInfo;

				// Token: 0x04004D60 RID: 19808
				private XmlSerializerOperationBehavior.Reflector.SerializerStub serializerStub;

				// Token: 0x04004D61 RID: 19809
				private XmlQualifiedName faultContractElementName;

				// Token: 0x04004D62 RID: 19810
				private XmlSerializerObjectSerializer serializer;
			}

			// Token: 0x02000F03 RID: 3843
			internal class MessageInfo : XmlSerializerOperationFormatter.MessageInfo
			{
				// Token: 0x17001D5C RID: 7516
				// (get) Token: 0x06008598 RID: 34200 RVA: 0x001EF08F File Offset: 0x001ED28F
				internal XmlMembersMapping BodyMapping
				{
					get
					{
						return this.body.Mapping;
					}
				}

				// Token: 0x17001D5D RID: 7517
				// (get) Token: 0x06008599 RID: 34201 RVA: 0x001EF09C File Offset: 0x001ED29C
				internal override XmlSerializer BodySerializer
				{
					get
					{
						return this.body.GetSerializer();
					}
				}

				// Token: 0x17001D5E RID: 7518
				// (get) Token: 0x0600859A RID: 34202 RVA: 0x001EF0A9 File Offset: 0x001ED2A9
				internal XmlMembersMapping HeadersMapping
				{
					get
					{
						return this.headers.Mapping;
					}
				}

				// Token: 0x17001D5F RID: 7519
				// (get) Token: 0x0600859B RID: 34203 RVA: 0x001EF0B6 File Offset: 0x001ED2B6
				internal override XmlSerializer HeaderSerializer
				{
					get
					{
						return this.headers.GetSerializer();
					}
				}

				// Token: 0x17001D60 RID: 7520
				// (get) Token: 0x0600859C RID: 34204 RVA: 0x001EF0C3 File Offset: 0x001ED2C3
				internal override OperationFormatter.MessageHeaderDescriptionTable HeaderDescriptionTable
				{
					get
					{
						return this.headerDescriptionTable;
					}
				}

				// Token: 0x17001D61 RID: 7521
				// (get) Token: 0x0600859D RID: 34205 RVA: 0x001EF0CB File Offset: 0x001ED2CB
				internal override MessageHeaderDescription UnknownHeaderDescription
				{
					get
					{
						return this.unknownHeaderDescription;
					}
				}

				// Token: 0x17001D62 RID: 7522
				// (get) Token: 0x0600859E RID: 34206 RVA: 0x001EF0D3 File Offset: 0x001ED2D3
				internal override MessagePartDescriptionCollection RpcEncodedTypedMessageBodyParts
				{
					get
					{
						return this.rpcEncodedTypedMessageBodyParts;
					}
				}

				// Token: 0x0600859F RID: 34207 RVA: 0x001EF0DB File Offset: 0x001ED2DB
				internal void SetBody(XmlSerializerOperationBehavior.Reflector.SerializerStub body, MessagePartDescriptionCollection rpcEncodedTypedMessageBodyParts)
				{
					this.body = body;
					this.rpcEncodedTypedMessageBodyParts = rpcEncodedTypedMessageBodyParts;
				}

				// Token: 0x060085A0 RID: 34208 RVA: 0x001EF0EB File Offset: 0x001ED2EB
				internal void SetHeaders(XmlSerializerOperationBehavior.Reflector.SerializerStub headers)
				{
					this.headers = headers;
				}

				// Token: 0x060085A1 RID: 34209 RVA: 0x001EF0F4 File Offset: 0x001ED2F4
				internal void SetHeaderDescriptionTable(OperationFormatter.MessageHeaderDescriptionTable headerDescriptionTable)
				{
					this.headerDescriptionTable = headerDescriptionTable;
				}

				// Token: 0x060085A2 RID: 34210 RVA: 0x001EF0FD File Offset: 0x001ED2FD
				internal void SetUnknownHeaderDescription(MessageHeaderDescription unknownHeaderDescription)
				{
					this.unknownHeaderDescription = unknownHeaderDescription;
				}

				// Token: 0x04004D63 RID: 19811
				private XmlSerializerOperationBehavior.Reflector.SerializerStub headers;

				// Token: 0x04004D64 RID: 19812
				private XmlSerializerOperationBehavior.Reflector.SerializerStub body;

				// Token: 0x04004D65 RID: 19813
				private OperationFormatter.MessageHeaderDescriptionTable headerDescriptionTable;

				// Token: 0x04004D66 RID: 19814
				private MessageHeaderDescription unknownHeaderDescription;

				// Token: 0x04004D67 RID: 19815
				private MessagePartDescriptionCollection rpcEncodedTypedMessageBodyParts;
			}
		}
	}
}
