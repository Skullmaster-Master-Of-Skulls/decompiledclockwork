using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Runtime.Serialization;
using System.ServiceModel.Dispatcher;
using System.Web.Services.Description;
using System.Xml;
using System.Xml.Schema;

namespace System.ServiceModel.Description
{
	// Token: 0x0200040A RID: 1034
	internal abstract class MessageContractExporter
	{
		// Token: 0x0600274B RID: 10059 RVA: 0x000916C1 File Offset: 0x0008F8C1
		internal static void ExportMessageBinding(WsdlExporter exporter, WsdlEndpointConversionContext endpointContext, Type messageContractExporterType, OperationDescription operation)
		{
			new MessageContractExporter.MessageBindingExporter(exporter, endpointContext).ExportMessageBinding(operation, messageContractExporterType);
		}

		// Token: 0x0600274C RID: 10060
		protected abstract object OnExportMessageContract();

		// Token: 0x0600274D RID: 10061
		protected abstract void ExportHeaders(int messageIndex, object state);

		// Token: 0x0600274E RID: 10062
		protected abstract void ExportBody(int messageIndex, object state);

		// Token: 0x0600274F RID: 10063
		protected abstract void ExportKnownTypes();

		// Token: 0x06002750 RID: 10064
		protected abstract bool IsRpcStyle();

		// Token: 0x06002751 RID: 10065
		protected abstract bool IsEncoded();

		// Token: 0x06002752 RID: 10066
		protected abstract object GetExtensionData();

		// Token: 0x170009D8 RID: 2520
		// (get) Token: 0x06002753 RID: 10067 RVA: 0x000916D1 File Offset: 0x0008F8D1
		protected MessageContractExporter.MessageExportContext ExportedMessages
		{
			get
			{
				return MessageContractExporter.GetMessageExportContext(this.exporter);
			}
		}

		// Token: 0x06002754 RID: 10068 RVA: 0x000916E0 File Offset: 0x0008F8E0
		private void AddElementToSchema(XmlSchemaElement element, string elementNs, XmlSchemaSet schemaSet)
		{
			OperationDescription operationDescription = this.operation;
			if (operationDescription.OperationMethod != null)
			{
				XmlQualifiedName xmlQualifiedName = new XmlQualifiedName(element.Name, elementNs);
				MessageContractExporter.OperationElement operationElement;
				if (this.ExportedMessages.ElementTypes.TryGetValue(xmlQualifiedName, out operationElement))
				{
					if (operationElement.Operation.OperationMethod == operationDescription.OperationMethod)
					{
						return;
					}
					if (!SchemaHelper.IsMatch(element, operationElement.Element))
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("CannotHaveTwoOperationsWithTheSameElement5", new object[]
						{
							operationDescription.OperationMethod.DeclaringType,
							operationDescription.OperationMethod.Name,
							xmlQualifiedName,
							operationElement.Operation.OperationMethod.DeclaringType,
							operationElement.Operation.Name
						})));
					}
					return;
				}
				else
				{
					this.ExportedMessages.ElementTypes.Add(xmlQualifiedName, new MessageContractExporter.OperationElement(element, operationDescription));
				}
			}
			SchemaHelper.AddElementToSchema(element, SchemaHelper.GetSchema(elementNs, schemaSet), schemaSet);
		}

		// Token: 0x06002755 RID: 10069 RVA: 0x000917DC File Offset: 0x0008F9DC
		private static MessageContractExporter.MessageExportContext GetMessageExportContext(WsdlExporter exporter)
		{
			object obj;
			if (!exporter.State.TryGetValue(typeof(MessageContractExporter.MessageExportContext), out obj))
			{
				obj = new MessageContractExporter.MessageExportContext();
				exporter.State[typeof(MessageContractExporter.MessageExportContext)] = obj;
			}
			return (MessageContractExporter.MessageExportContext)obj;
		}

		// Token: 0x06002756 RID: 10070 RVA: 0x00091824 File Offset: 0x0008FA24
		protected MessageContractExporter(WsdlExporter exporter, WsdlContractConversionContext context, OperationDescription operation, IOperationBehavior extension)
		{
			this.exporter = exporter;
			this.contractContext = context;
			this.operation = operation;
			this.extension = extension;
		}

		// Token: 0x06002757 RID: 10071 RVA: 0x0009184C File Offset: 0x0008FA4C
		internal void ExportMessageContract()
		{
			if (this.extension == null)
			{
				return;
			}
			object state = this.OnExportMessageContract();
			OperationFormatter.Validate(this.operation, this.IsRpcStyle(), this.IsEncoded());
			this.ExportKnownTypes();
			for (int i = 0; i < this.operation.Messages.Count; i++)
			{
				this.ExportMessage(i, state);
			}
			if (!this.operation.IsOneWay)
			{
				this.ExportFaults(state);
			}
			foreach (object obj in this.exporter.GeneratedXmlSchemas.Schemas())
			{
				XmlSchema xmlSchema = (XmlSchema)obj;
				MessageContractExporter.EnsureXsdImport(xmlSchema.TargetNamespace, this.contractContext.WsdlPortType.ServiceDescription);
			}
		}

		// Token: 0x06002758 RID: 10072 RVA: 0x0009192C File Offset: 0x0008FB2C
		private void ExportMessage(int messageIndex, object state)
		{
			try
			{
				MessageDescription messageDescription = this.operation.Messages[messageIndex];
				Message message;
				if (this.CreateMessage(messageDescription, messageIndex, out message))
				{
					if (messageDescription.IsUntypedMessage)
					{
						this.ExportAnyMessage(message, messageDescription.Body.ReturnValue ?? messageDescription.Body.Parts[0]);
						return;
					}
					bool isRequest = messageIndex == 0;
					StreamFormatter streamFormatter = StreamFormatter.Create(messageDescription, this.operation.Name, isRequest);
					if (streamFormatter != null)
					{
						this.ExportStreamBody(message, streamFormatter.WrapperName, streamFormatter.WrapperNamespace, streamFormatter.PartName, streamFormatter.PartNamespace, this.IsRpcStyle(), false);
					}
					else
					{
						this.ExportBody(messageIndex, state);
					}
				}
				if (!messageDescription.IsUntypedMessage)
				{
					this.ExportHeaders(messageIndex, state);
				}
			}
			finally
			{
				this.Compile();
			}
		}

		// Token: 0x06002759 RID: 10073 RVA: 0x000919FC File Offset: 0x0008FBFC
		protected virtual void ExportFaults(object state)
		{
			foreach (FaultDescription fault in this.operation.Faults)
			{
				this.ExportFault(fault);
			}
		}

		// Token: 0x0600275A RID: 10074 RVA: 0x00091A50 File Offset: 0x0008FC50
		protected bool IsOperationInherited()
		{
			return this.operation.DeclaringContract != this.contractContext.Contract;
		}

		// Token: 0x0600275B RID: 10075 RVA: 0x00091A70 File Offset: 0x0008FC70
		private void ExportAnyMessage(Message message, MessagePartDescription part)
		{
			XmlSchemaSet generatedXmlSchemas = this.exporter.GeneratedXmlSchemas;
			XmlSchema schema = SchemaHelper.GetSchema(DataContractSerializerMessageContractImporter.GenericMessageTypeName.Namespace, generatedXmlSchemas);
			if (!schema.SchemaTypes.Contains(DataContractSerializerMessageContractImporter.GenericMessageTypeName))
			{
				XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
				xmlSchemaComplexType.Name = DataContractSerializerMessageContractImporter.GenericMessageTypeName.Name;
				XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
				xmlSchemaComplexType.Particle = xmlSchemaSequence;
				XmlSchemaAny xmlSchemaAny = new XmlSchemaAny();
				xmlSchemaAny.MinOccurs = 0m;
				xmlSchemaAny.MaxOccurs = decimal.MaxValue;
				xmlSchemaAny.Namespace = "##any";
				xmlSchemaSequence.Items.Add(xmlSchemaAny);
				SchemaHelper.AddTypeToSchema(xmlSchemaComplexType, schema, generatedXmlSchemas);
			}
			string partName = string.IsNullOrEmpty(part.UniquePartName) ? part.Name : part.UniquePartName;
			MessagePart messagePart = MessageContractExporter.AddMessagePart(message, partName, XmlQualifiedName.Empty, DataContractSerializerMessageContractImporter.GenericMessageTypeName);
			part.UniquePartName = messagePart.Name;
		}

		// Token: 0x0600275C RID: 10076 RVA: 0x00091B58 File Offset: 0x0008FD58
		protected void ExportStreamBody(Message message, string wrapperName, string wrapperNs, string partName, string partNs, bool isRpc, bool skipSchemaExport)
		{
			XmlSchemaSet generatedXmlSchemas = this.exporter.GeneratedXmlSchemas;
			XmlSchema schema = SchemaHelper.GetSchema(DataContractSerializerMessageContractImporter.StreamBodyTypeName.Namespace, generatedXmlSchemas);
			if (!schema.SchemaTypes.Contains(DataContractSerializerMessageContractImporter.StreamBodyTypeName))
			{
				SchemaHelper.AddTypeToSchema(new XmlSchemaSimpleType
				{
					Name = DataContractSerializerMessageContractImporter.StreamBodyTypeName.Name,
					Content = new XmlSchemaSimpleTypeRestriction
					{
						BaseTypeName = XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.Base64Binary).QualifiedName
					}
				}, schema, generatedXmlSchemas);
			}
			XmlSchemaSequence wrapperSequence = null;
			if (!isRpc && wrapperName != null)
			{
				wrapperSequence = this.ExportWrappedPart(message, wrapperName, wrapperNs, generatedXmlSchemas, skipSchemaExport);
			}
			MessagePartDescription part = new MessagePartDescription(partName, partNs);
			this.ExportMessagePart(message, part, DataContractSerializerMessageContractImporter.StreamBodyTypeName, null, false, false, skipSchemaExport, !isRpc, wrapperNs, wrapperSequence, generatedXmlSchemas);
		}

		// Token: 0x0600275D RID: 10077 RVA: 0x00091C14 File Offset: 0x0008FE14
		private void ExportFault(FaultDescription fault)
		{
			Message message = new Message();
			message.Name = this.GetFaultMessageName(fault.Name);
			XmlQualifiedName elementName = this.ExportFaultElement(fault);
			this.contractContext.WsdlPortType.ServiceDescription.Messages.Add(message);
			MessageContractExporter.AddMessagePart(message, "detail", elementName, null);
			OperationFault operationFault = this.contractContext.GetOperationFault(fault);
			WsdlExporter.WSAddressingHelper.AddActionAttribute(fault.Action, operationFault, this.exporter.PolicyVersion);
			operationFault.Message = new XmlQualifiedName(message.Name, message.ServiceDescription.TargetNamespace);
		}

		// Token: 0x0600275E RID: 10078 RVA: 0x00091CAC File Offset: 0x0008FEAC
		private XmlQualifiedName ExportFaultElement(FaultDescription fault)
		{
			XmlSchemaType xsdType;
			XmlQualifiedName typeName = this.ExportType(fault.DetailType, fault.Name, this.operation.Name, out xsdType);
			XmlQualifiedName xmlQualifiedName;
			if (XmlName.IsNullOrEmpty(fault.ElementName))
			{
				xmlQualifiedName = this.DataContractExporter.GetRootElementName(fault.DetailType);
				if (xmlQualifiedName == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxFaultTypeAnonymous", new object[]
					{
						this.operation.Name,
						fault.DetailType.FullName
					})));
				}
			}
			else
			{
				xmlQualifiedName = new XmlQualifiedName(fault.ElementName.EncodedName, fault.Namespace);
			}
			this.ExportGlobalElement(xmlQualifiedName.Name, xmlQualifiedName.Namespace, true, typeName, xsdType, this.exporter.GeneratedXmlSchemas);
			return xmlQualifiedName;
		}

		// Token: 0x170009D9 RID: 2521
		// (get) Token: 0x0600275F RID: 10079 RVA: 0x00091D78 File Offset: 0x0008FF78
		protected XsdDataContractExporter DataContractExporter
		{
			get
			{
				object obj;
				if (!this.exporter.State.TryGetValue(typeof(XsdDataContractExporter), out obj))
				{
					obj = new XsdDataContractExporter(this.exporter.GeneratedXmlSchemas);
					this.exporter.State.Add(typeof(XsdDataContractExporter), obj);
				}
				return (XsdDataContractExporter)obj;
			}
		}

		// Token: 0x06002760 RID: 10080 RVA: 0x00091DD8 File Offset: 0x0008FFD8
		protected XmlQualifiedName ExportType(Type type, string partName, string operationName, out XmlSchemaType xsdType)
		{
			xsdType = null;
			if (type == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxExportMustHaveType", new object[]
				{
					operationName,
					partName
				})));
			}
			if (type == typeof(void))
			{
				return null;
			}
			this.DataContractExporter.Export(type);
			XmlQualifiedName schemaTypeName = this.DataContractExporter.GetSchemaTypeName(type);
			if (MessageContractExporter.IsNullOrEmpty(schemaTypeName))
			{
				xsdType = this.DataContractExporter.GetSchemaType(type);
			}
			return schemaTypeName;
		}

		// Token: 0x170009DA RID: 2522
		// (get) Token: 0x06002761 RID: 10081 RVA: 0x00091E5F File Offset: 0x0009005F
		protected XmlSchemaSet SchemaSet
		{
			get
			{
				return this.exporter.GeneratedXmlSchemas;
			}
		}

		// Token: 0x06002762 RID: 10082 RVA: 0x00091E6C File Offset: 0x0009006C
		protected static MessagePart AddMessagePart(Message message, string partName, XmlQualifiedName elementName, XmlQualifiedName typeName)
		{
			if (message.Parts[partName] != null)
			{
				if (MessageContractExporter.IsNullOrEmpty(elementName))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("SFxPartNameMustBeUniqueInRpc", new object[]
					{
						partName
					})));
				}
				int num = 1;
				while (message.Parts[partName + num.ToString()] != null)
				{
					if (num == 2147483647)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("SFxTooManyPartsWithSameName", new object[]
						{
							partName
						})));
					}
					num++;
				}
				partName += num.ToString(CultureInfo.InvariantCulture);
			}
			MessagePart messagePart = new MessagePart();
			messagePart.Name = partName;
			messagePart.Element = elementName;
			messagePart.Type = typeName;
			message.Parts.Add(messagePart);
			MessageContractExporter.EnsureXsdImport(MessageContractExporter.IsNullOrEmpty(elementName) ? typeName.Namespace : elementName.Namespace, message.ServiceDescription);
			return messagePart;
		}

		// Token: 0x06002763 RID: 10083 RVA: 0x00091F64 File Offset: 0x00090164
		private static void EnsureXsdImport(string ns, ServiceDescription wsdl)
		{
			string text = wsdl.TargetNamespace;
			if (!text.EndsWith("/", StringComparison.Ordinal))
			{
				text += "/Imports";
			}
			else
			{
				text += "Imports";
			}
			if (text == ns)
			{
				text = wsdl.TargetNamespace;
			}
			XmlSchema xmlSchema = MessageContractExporter.GetContainedSchema(wsdl, text);
			if (xmlSchema != null)
			{
				using (XmlSchemaObjectEnumerator enumerator = xmlSchema.Includes.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						XmlSchemaImport xmlSchemaImport = obj as XmlSchemaImport;
						if (xmlSchemaImport != null && SchemaHelper.NamespacesEqual(xmlSchemaImport.Namespace, ns))
						{
							return;
						}
					}
					goto IL_BB;
				}
			}
			xmlSchema = new XmlSchema();
			xmlSchema.TargetNamespace = text;
			wsdl.Types.Schemas.Add(xmlSchema);
			IL_BB:
			XmlSchemaImport xmlSchemaImport2 = new XmlSchemaImport();
			if (ns != null && ns.Length > 0)
			{
				xmlSchemaImport2.Namespace = ns;
			}
			xmlSchema.Includes.Add(xmlSchemaImport2);
		}

		// Token: 0x06002764 RID: 10084 RVA: 0x00092064 File Offset: 0x00090264
		private static XmlSchema GetContainedSchema(ServiceDescription wsdl, string ns)
		{
			foreach (object obj in wsdl.Types.Schemas)
			{
				XmlSchema xmlSchema = (XmlSchema)obj;
				if (SchemaHelper.NamespacesEqual(xmlSchema.TargetNamespace, ns))
				{
					return xmlSchema;
				}
			}
			return null;
		}

		// Token: 0x06002765 RID: 10085 RVA: 0x000920D0 File Offset: 0x000902D0
		protected static bool IsNullOrEmpty(XmlQualifiedName qname)
		{
			return qname == null || qname.IsEmpty;
		}

		// Token: 0x06002766 RID: 10086 RVA: 0x000920E4 File Offset: 0x000902E4
		protected void ExportGlobalElement(string elementName, string elementNs, bool isNillable, XmlQualifiedName typeName, XmlSchemaType xsdType, XmlSchemaSet schemaSet)
		{
			XmlSchemaElement xmlSchemaElement = new XmlSchemaElement();
			xmlSchemaElement.Name = elementName;
			if (xsdType != null)
			{
				xmlSchemaElement.SchemaType = xsdType;
			}
			else
			{
				xmlSchemaElement.SchemaTypeName = typeName;
			}
			xmlSchemaElement.IsNillable = isNillable;
			this.AddElementToSchema(xmlSchemaElement, elementNs, schemaSet);
		}

		// Token: 0x06002767 RID: 10087 RVA: 0x00092128 File Offset: 0x00090328
		private void ExportLocalElement(string wrapperNs, string elementName, string elementNs, XmlQualifiedName typeName, XmlSchemaType xsdType, bool multiple, bool isOptional, bool isNillable, XmlSchemaSequence sequence, XmlSchemaSet schemaSet)
		{
			XmlSchema schema = SchemaHelper.GetSchema(wrapperNs, schemaSet);
			XmlSchemaElement xmlSchemaElement = new XmlSchemaElement();
			if (elementNs == wrapperNs)
			{
				xmlSchemaElement.Name = elementName;
				if (xsdType != null)
				{
					xmlSchemaElement.SchemaType = xsdType;
				}
				else
				{
					xmlSchemaElement.SchemaTypeName = typeName;
					SchemaHelper.AddImportToSchema(xmlSchemaElement.SchemaTypeName.Namespace, schema);
				}
				SchemaHelper.AddElementForm(xmlSchemaElement, schema);
				xmlSchemaElement.IsNillable = isNillable;
			}
			else
			{
				xmlSchemaElement.RefName = new XmlQualifiedName(elementName, elementNs);
				SchemaHelper.AddImportToSchema(elementNs, schema);
				this.ExportGlobalElement(elementName, elementNs, isNillable, typeName, xsdType, schemaSet);
			}
			if (multiple)
			{
				xmlSchemaElement.MaxOccurs = decimal.MaxValue;
			}
			if (isOptional)
			{
				xmlSchemaElement.MinOccurs = 0m;
			}
			sequence.Items.Add(xmlSchemaElement);
		}

		// Token: 0x06002768 RID: 10088 RVA: 0x000921E4 File Offset: 0x000903E4
		protected XmlSchemaSequence ExportWrappedPart(Message message, string elementName, string elementNs, XmlSchemaSet schemaSet, bool skipSchemaExport)
		{
			MessageContractExporter.AddMessagePart(message, "parameters", new XmlQualifiedName(elementName, elementNs), XmlQualifiedName.Empty);
			if (skipSchemaExport)
			{
				return MessageContractExporter.emptySequence;
			}
			XmlSchemaElement xmlSchemaElement = new XmlSchemaElement();
			xmlSchemaElement.Name = elementName;
			XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
			xmlSchemaElement.SchemaType = xmlSchemaComplexType;
			XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
			xmlSchemaComplexType.Particle = xmlSchemaSequence;
			this.AddElementToSchema(xmlSchemaElement, elementNs, schemaSet);
			return xmlSchemaSequence;
		}

		// Token: 0x06002769 RID: 10089 RVA: 0x00092248 File Offset: 0x00090448
		protected bool CreateMessage(MessageDescription message, int messageIndex, out Message wsdlMessage)
		{
			wsdlMessage = null;
			bool flag = true;
			if (this.ExportedMessages.WsdlMessages.ContainsKey(new MessageContractExporter.MessageDescriptionDictionaryKey(this.contractContext.Contract, message)))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("MultipleCallsToExportContractWithSameContract")));
			}
			MessageContractExporter.TypedMessageKey key = null;
			MessageContractExporter.OperationMessageKey operationMessageKey = null;
			if (message.IsTypedMessage)
			{
				key = new MessageContractExporter.TypedMessageKey(message.MessageType, this.operation.DeclaringContract.Namespace, this.GetExtensionData());
				if (this.ExportedMessages.TypedMessages.TryGetValue(key, out wsdlMessage))
				{
					flag = false;
				}
			}
			else if (this.operation.OperationMethod != null)
			{
				operationMessageKey = new MessageContractExporter.OperationMessageKey(this.operation, messageIndex);
				if (this.ExportedMessages.ParameterMessages.TryGetValue(operationMessageKey, out wsdlMessage))
				{
					flag = false;
				}
			}
			ServiceDescription serviceDescription = this.contractContext.WsdlPortType.ServiceDescription;
			if (flag)
			{
				wsdlMessage = new Message();
				wsdlMessage.Name = this.GetMessageName(message);
				serviceDescription.Messages.Add(wsdlMessage);
				if (message.IsTypedMessage)
				{
					this.ExportedMessages.TypedMessages.Add(key, wsdlMessage);
				}
				else if (operationMessageKey != null)
				{
					this.ExportedMessages.ParameterMessages.Add(operationMessageKey, wsdlMessage);
				}
			}
			OperationMessage operationMessage = this.contractContext.GetOperationMessage(message);
			operationMessage.Message = new XmlQualifiedName(wsdlMessage.Name, wsdlMessage.ServiceDescription.TargetNamespace);
			this.ExportedMessages.WsdlMessages.Add(new MessageContractExporter.MessageDescriptionDictionaryKey(this.contractContext.Contract, message), wsdlMessage);
			return flag;
		}

		// Token: 0x0600276A RID: 10090 RVA: 0x000923D0 File Offset: 0x000905D0
		protected bool CreateHeaderMessage(MessageDescription message, out Message wsdlMessage)
		{
			wsdlMessage = null;
			if (this.ExportedMessages.WsdlHeaderMessages.ContainsKey(new MessageContractExporter.MessageDescriptionDictionaryKey(this.contractContext.Contract, message)))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("MultipleCallsToExportContractWithSameContract")));
			}
			MessageContractExporter.TypedMessageKey key = null;
			if (message.IsTypedMessage)
			{
				key = new MessageContractExporter.TypedMessageKey(message.MessageType, this.operation.DeclaringContract.Namespace, this.GetExtensionData());
				if (this.ExportedMessages.TypedHeaderMessages.TryGetValue(key, out wsdlMessage))
				{
					this.ExportedMessages.WsdlHeaderMessages.Add(new MessageContractExporter.MessageDescriptionDictionaryKey(this.contractContext.Contract, message), wsdlMessage);
					return false;
				}
			}
			string headerMessageName = this.GetHeaderMessageName(message);
			wsdlMessage = new Message();
			wsdlMessage.Name = headerMessageName;
			this.contractContext.WsdlPortType.ServiceDescription.Messages.Add(wsdlMessage);
			if (message.IsTypedMessage)
			{
				this.ExportedMessages.TypedHeaderMessages.Add(key, wsdlMessage);
			}
			this.ExportedMessages.WsdlHeaderMessages.Add(new MessageContractExporter.MessageDescriptionDictionaryKey(this.contractContext.Contract, message), wsdlMessage);
			return true;
		}

		// Token: 0x0600276B RID: 10091 RVA: 0x000924F8 File Offset: 0x000906F8
		private string GetMessageName(MessageDescription messageDescription)
		{
			string text = XmlName.IsNullOrEmpty(messageDescription.MessageName) ? null : messageDescription.MessageName.EncodedName;
			if (string.IsNullOrEmpty(text))
			{
				string name = this.contractContext.WsdlPortType.Name;
				string name2 = this.contractContext.GetOperation(this.operation).Name;
				string text2 = this.operation.IsServerInitiated() ? "Callback" : string.Empty;
				if (messageDescription.Direction == MessageDirection.Input)
				{
					text = string.Format(CultureInfo.InvariantCulture, "{0}_{1}_Input{2}Message", new object[]
					{
						name,
						name2,
						text2
					});
				}
				else
				{
					text = string.Format(CultureInfo.InvariantCulture, "{0}_{1}_Output{2}Message", new object[]
					{
						name,
						name2,
						text2
					});
				}
			}
			ServiceDescription serviceDescription = this.contractContext.WsdlPortType.ServiceDescription;
			return this.GetUniqueMessageName(serviceDescription, text);
		}

		// Token: 0x0600276C RID: 10092 RVA: 0x000925D8 File Offset: 0x000907D8
		private string GetHeaderMessageName(MessageDescription messageDescription)
		{
			Message message = this.ExportedMessages.WsdlMessages[new MessageContractExporter.MessageDescriptionDictionaryKey(this.contractContext.Contract, messageDescription)];
			string messageNameBase = string.Format(CultureInfo.InvariantCulture, "{0}_Headers", new object[]
			{
				message.Name
			});
			ServiceDescription serviceDescription = this.contractContext.WsdlPortType.ServiceDescription;
			return this.GetUniqueMessageName(serviceDescription, messageNameBase);
		}

		// Token: 0x0600276D RID: 10093 RVA: 0x00092640 File Offset: 0x00090840
		protected string GetFaultMessageName(string faultName)
		{
			string name = this.contractContext.WsdlPortType.Name;
			string name2 = this.contractContext.GetOperation(this.operation).Name;
			string messageNameBase = string.Format(CultureInfo.InvariantCulture, "{0}_{1}_{2}_FaultMessage", new object[]
			{
				name,
				name2,
				faultName
			});
			ServiceDescription serviceDescription = this.contractContext.WsdlPortType.ServiceDescription;
			return this.GetUniqueMessageName(serviceDescription, messageNameBase);
		}

		// Token: 0x0600276E RID: 10094 RVA: 0x000926B0 File Offset: 0x000908B0
		private static bool DoesMessageNameExist(string messageName, object wsdlObject)
		{
			return ((ServiceDescription)wsdlObject).Messages[messageName] != null;
		}

		// Token: 0x0600276F RID: 10095 RVA: 0x000926C6 File Offset: 0x000908C6
		private string GetUniqueMessageName(ServiceDescription wsdl, string messageNameBase)
		{
			return NamingHelper.GetUniqueName(messageNameBase, new NamingHelper.DoesNameExist(MessageContractExporter.DoesMessageNameExist), wsdl);
		}

		// Token: 0x06002770 RID: 10096 RVA: 0x000926DC File Offset: 0x000908DC
		protected void ExportMessagePart(Message message, MessagePartDescription part, XmlQualifiedName typeName, XmlSchemaType xsdType, bool isOptional, bool isNillable, bool skipSchemaExport, bool generateElement, string wrapperNs, XmlSchemaSequence wrapperSequence, XmlSchemaSet schemaSet)
		{
			if (MessageContractExporter.IsNullOrEmpty(typeName) && xsdType == null)
			{
				return;
			}
			string name = part.Name;
			string text = string.IsNullOrEmpty(part.UniquePartName) ? name : part.UniquePartName;
			MessagePart messagePart = null;
			if (generateElement)
			{
				if (wrapperSequence != null)
				{
					if (!skipSchemaExport)
					{
						this.ExportLocalElement(wrapperNs, text, part.Namespace, typeName, xsdType, part.Multiple, isOptional, isNillable, wrapperSequence, schemaSet);
					}
				}
				else
				{
					if (!skipSchemaExport)
					{
						this.ExportGlobalElement(name, part.Namespace, isNillable, typeName, xsdType, schemaSet);
					}
					messagePart = MessageContractExporter.AddMessagePart(message, text, new XmlQualifiedName(name, part.Namespace), XmlQualifiedName.Empty);
				}
			}
			else
			{
				if (string.IsNullOrEmpty(typeName.Name))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxAnonymousTypeNotSupported", new object[]
					{
						message.Name,
						text
					})));
				}
				messagePart = MessageContractExporter.AddMessagePart(message, text, XmlQualifiedName.Empty, typeName);
			}
			if (messagePart != null)
			{
				part.UniquePartName = messagePart.Name;
			}
		}

		// Token: 0x06002771 RID: 10097 RVA: 0x000927D4 File Offset: 0x000909D4
		protected void AddParameterOrder(MessageDescription message)
		{
			if (this.operation == null)
			{
				return;
			}
			Operation operation = this.contractContext.GetOperation(this.operation);
			if (operation != null)
			{
				if (operation.ParameterOrder == null)
				{
					operation.ParameterOrder = new string[this.GetParameterCount()];
				}
				if (operation.ParameterOrder.Length == 0)
				{
					return;
				}
				foreach (MessagePartDescription messagePartDescription in message.Body.Parts)
				{
					ParameterInfo parameterInfo = messagePartDescription.AdditionalAttributesProvider as ParameterInfo;
					if (parameterInfo != null && parameterInfo.Position >= 0)
					{
						operation.ParameterOrder[parameterInfo.Position] = messagePartDescription.Name;
					}
				}
			}
		}

		// Token: 0x06002772 RID: 10098 RVA: 0x0009288C File Offset: 0x00090A8C
		private int GetParameterCount()
		{
			int num = -1;
			foreach (MessageDescription messageDescription in this.operation.Messages)
			{
				foreach (MessagePartDescription messagePartDescription in messageDescription.Body.Parts)
				{
					ParameterInfo parameterInfo = messagePartDescription.AdditionalAttributesProvider as ParameterInfo;
					if (parameterInfo == null)
					{
						return 0;
					}
					if (num < parameterInfo.Position)
					{
						num = parameterInfo.Position;
					}
				}
			}
			return num + 1;
		}

		// Token: 0x06002773 RID: 10099 RVA: 0x00092944 File Offset: 0x00090B44
		protected virtual void Compile()
		{
			foreach (object obj in this.SchemaSet.Schemas())
			{
				XmlSchema schema = (XmlSchema)obj;
				this.SchemaSet.Reprocess(schema);
			}
			SchemaHelper.Compile(this.SchemaSet, this.exporter.Errors);
		}

		// Token: 0x040021E9 RID: 8681
		protected readonly WsdlContractConversionContext contractContext;

		// Token: 0x040021EA RID: 8682
		protected readonly WsdlExporter exporter;

		// Token: 0x040021EB RID: 8683
		protected readonly OperationDescription operation;

		// Token: 0x040021EC RID: 8684
		protected readonly IOperationBehavior extension;

		// Token: 0x040021ED RID: 8685
		private static readonly XmlSchemaSequence emptySequence = new XmlSchemaSequence();

		// Token: 0x02000BBF RID: 3007
		private class MessageBindingExporter
		{
			// Token: 0x06007473 RID: 29811 RVA: 0x001B2BA0 File Offset: 0x001B0DA0
			internal MessageBindingExporter(WsdlExporter exporter, WsdlEndpointConversionContext endpointContext)
			{
				this.endpointContext = endpointContext;
				this.exportedMessages = (MessageContractExporter.MessageExportContext)exporter.State[typeof(MessageContractExporter.MessageExportContext)];
				this.soapVersion = SoapHelper.GetSoapVersion(endpointContext.WsdlBinding);
				this.exporter = exporter;
			}

			// Token: 0x06007474 RID: 29812 RVA: 0x001B2BF4 File Offset: 0x001B0DF4
			internal void ExportMessageBinding(OperationDescription operation, Type messageContractExporterType)
			{
				OperationBinding operationBinding = this.endpointContext.GetOperationBinding(operation);
				bool flag;
				bool isEncoded;
				if (!MessageContractExporter.MessageBindingExporter.GetStyleAndUse(operation, messageContractExporterType, out flag, out isEncoded))
				{
					return;
				}
				SoapOperationBinding orCreateSoapOperationBinding = SoapHelper.GetOrCreateSoapOperationBinding(this.endpointContext, operation, this.exporter);
				if (orCreateSoapOperationBinding == null)
				{
					return;
				}
				orCreateSoapOperationBinding.Style = (flag ? SoapBindingStyle.Rpc : SoapBindingStyle.Document);
				if (flag)
				{
					SoapBinding soapBinding = (SoapBinding)this.endpointContext.WsdlBinding.Extensions.Find(typeof(SoapBinding));
					soapBinding.Style = orCreateSoapOperationBinding.Style;
				}
				orCreateSoapOperationBinding.SoapAction = operation.Messages[0].Action;
				foreach (MessageDescription messageDescription in operation.Messages)
				{
					MessageBinding messageBinding = this.endpointContext.GetMessageBinding(messageDescription);
					Message message;
					if (this.exportedMessages.WsdlHeaderMessages.TryGetValue(new MessageContractExporter.MessageDescriptionDictionaryKey(this.endpointContext.Endpoint.Contract, messageDescription), out message))
					{
						XmlQualifiedName messageName = new XmlQualifiedName(message.Name, message.ServiceDescription.TargetNamespace);
						foreach (MessageHeaderDescription messageHeaderDescription in messageDescription.Headers)
						{
							if (!messageHeaderDescription.IsUnknownHeaderCollection)
							{
								this.ExportMessageHeaderBinding(messageHeaderDescription, messageName, isEncoded, messageBinding);
							}
						}
					}
					this.ExportMessageBodyBinding(messageDescription, flag, isEncoded, messageBinding);
				}
				foreach (FaultDescription fault in operation.Faults)
				{
					this.ExportFaultBinding(fault, isEncoded, operationBinding);
				}
			}

			// Token: 0x06007475 RID: 29813 RVA: 0x001B2DCC File Offset: 0x001B0FCC
			private void ExportFaultBinding(FaultDescription fault, bool isEncoded, OperationBinding operationBinding)
			{
				SoapHelper.CreateSoapFaultBinding(fault.Name, this.endpointContext, this.endpointContext.GetFaultBinding(fault), isEncoded);
			}

			// Token: 0x06007476 RID: 29814 RVA: 0x001B2DEC File Offset: 0x001B0FEC
			private void ExportMessageBodyBinding(MessageDescription messageDescription, bool isRpc, bool isEncoded, MessageBinding messageBinding)
			{
				SoapBodyBinding orCreateSoapBodyBinding = SoapHelper.GetOrCreateSoapBodyBinding(this.endpointContext, messageBinding, this.exporter);
				if (orCreateSoapBodyBinding == null)
				{
					return;
				}
				orCreateSoapBodyBinding.Use = (isEncoded ? SoapBindingUse.Encoded : SoapBindingUse.Literal);
				if (isRpc)
				{
					string wrapperNamespace;
					if (!this.ExportedMessages.WrapperNamespaces.TryGetValue(new MessageContractExporter.MessageDescriptionDictionaryKey(this.endpointContext.ContractConversionContext.Contract, messageDescription), out wrapperNamespace))
					{
						wrapperNamespace = messageDescription.Body.WrapperNamespace;
					}
					orCreateSoapBodyBinding.Namespace = wrapperNamespace;
				}
				if (isEncoded)
				{
					orCreateSoapBodyBinding.Encoding = XmlSerializerOperationFormatter.GetEncoding(this.soapVersion);
				}
			}

			// Token: 0x06007477 RID: 29815 RVA: 0x001B2E74 File Offset: 0x001B1074
			private void ExportMessageHeaderBinding(MessageHeaderDescription header, XmlQualifiedName messageName, bool isEncoded, MessageBinding messageBinding)
			{
				SoapHeaderBinding soapHeaderBinding = SoapHelper.CreateSoapHeaderBinding(this.endpointContext, messageBinding);
				soapHeaderBinding.Part = (string.IsNullOrEmpty(header.UniquePartName) ? header.Name : header.UniquePartName);
				soapHeaderBinding.Message = messageName;
				soapHeaderBinding.Use = (isEncoded ? SoapBindingUse.Encoded : SoapBindingUse.Literal);
				if (isEncoded)
				{
					soapHeaderBinding.Encoding = XmlSerializerOperationFormatter.GetEncoding(this.soapVersion);
				}
			}

			// Token: 0x06007478 RID: 29816 RVA: 0x001B2ED8 File Offset: 0x001B10D8
			private static bool GetStyleAndUse(OperationDescription operation, Type messageContractExporterType, out bool isRpc, out bool isEncoded)
			{
				isRpc = (isEncoded = false);
				if (messageContractExporterType == typeof(DataContractSerializerMessageContractExporter) || messageContractExporterType == null)
				{
					DataContractSerializerOperationBehavior dataContractSerializerOperationBehavior = operation.Behaviors.Find<DataContractSerializerOperationBehavior>();
					if (dataContractSerializerOperationBehavior != null)
					{
						isRpc = (dataContractSerializerOperationBehavior.DataContractFormatAttribute.Style == OperationFormatStyle.Rpc);
						isEncoded = false;
						return true;
					}
					if (messageContractExporterType == typeof(DataContractSerializerMessageContractExporter))
					{
						return false;
					}
				}
				if (!(messageContractExporterType == typeof(XmlSerializerMessageContractExporter)) && !(messageContractExporterType == null))
				{
					return false;
				}
				XmlSerializerOperationBehavior xmlSerializerOperationBehavior = operation.Behaviors.Find<XmlSerializerOperationBehavior>();
				if (xmlSerializerOperationBehavior != null)
				{
					isRpc = (xmlSerializerOperationBehavior.XmlSerializerFormatAttribute.Style == OperationFormatStyle.Rpc);
					isEncoded = xmlSerializerOperationBehavior.XmlSerializerFormatAttribute.IsEncoded;
					return true;
				}
				return false;
			}

			// Token: 0x17001AE7 RID: 6887
			// (get) Token: 0x06007479 RID: 29817 RVA: 0x001B2F8C File Offset: 0x001B118C
			private MessageContractExporter.MessageExportContext ExportedMessages
			{
				get
				{
					return MessageContractExporter.GetMessageExportContext(this.exporter);
				}
			}

			// Token: 0x040041EE RID: 16878
			private WsdlEndpointConversionContext endpointContext;

			// Token: 0x040041EF RID: 16879
			private MessageContractExporter.MessageExportContext exportedMessages;

			// Token: 0x040041F0 RID: 16880
			private EnvelopeVersion soapVersion;

			// Token: 0x040041F1 RID: 16881
			private WsdlExporter exporter;
		}

		// Token: 0x02000BC0 RID: 3008
		protected class MessageExportContext
		{
			// Token: 0x040041F2 RID: 16882
			internal readonly Dictionary<MessageContractExporter.MessageDescriptionDictionaryKey, Message> WsdlMessages = new Dictionary<MessageContractExporter.MessageDescriptionDictionaryKey, Message>();

			// Token: 0x040041F3 RID: 16883
			internal readonly Dictionary<MessageContractExporter.MessageDescriptionDictionaryKey, Message> WsdlHeaderMessages = new Dictionary<MessageContractExporter.MessageDescriptionDictionaryKey, Message>();

			// Token: 0x040041F4 RID: 16884
			internal readonly Dictionary<MessageContractExporter.MessageDescriptionDictionaryKey, string> WrapperNamespaces = new Dictionary<MessageContractExporter.MessageDescriptionDictionaryKey, string>();

			// Token: 0x040041F5 RID: 16885
			internal readonly Dictionary<MessageContractExporter.TypedMessageKey, Message> TypedMessages = new Dictionary<MessageContractExporter.TypedMessageKey, Message>();

			// Token: 0x040041F6 RID: 16886
			internal readonly Dictionary<MessageContractExporter.TypedMessageKey, Message> TypedHeaderMessages = new Dictionary<MessageContractExporter.TypedMessageKey, Message>();

			// Token: 0x040041F7 RID: 16887
			internal readonly Dictionary<MessageContractExporter.OperationMessageKey, Message> ParameterMessages = new Dictionary<MessageContractExporter.OperationMessageKey, Message>();

			// Token: 0x040041F8 RID: 16888
			internal readonly Dictionary<XmlQualifiedName, MessageContractExporter.OperationElement> ElementTypes = new Dictionary<XmlQualifiedName, MessageContractExporter.OperationElement>();
		}

		// Token: 0x02000BC1 RID: 3009
		protected sealed class MessageDescriptionDictionaryKey
		{
			// Token: 0x0600747B RID: 29819 RVA: 0x001B2FFC File Offset: 0x001B11FC
			public MessageDescriptionDictionaryKey(ContractDescription contract, MessageDescription MessageDescription)
			{
				this.Contract = contract;
				this.MessageDescription = MessageDescription;
			}

			// Token: 0x0600747C RID: 29820 RVA: 0x001B3014 File Offset: 0x001B1214
			public override bool Equals(object obj)
			{
				MessageContractExporter.MessageDescriptionDictionaryKey messageDescriptionDictionaryKey = obj as MessageContractExporter.MessageDescriptionDictionaryKey;
				return messageDescriptionDictionaryKey != null && messageDescriptionDictionaryKey.MessageDescription == this.MessageDescription && messageDescriptionDictionaryKey.Contract == this.Contract;
			}

			// Token: 0x0600747D RID: 29821 RVA: 0x001B304A File Offset: 0x001B124A
			public override int GetHashCode()
			{
				return this.Contract.GetHashCode() ^ this.MessageDescription.GetHashCode();
			}

			// Token: 0x040041F9 RID: 16889
			public readonly ContractDescription Contract;

			// Token: 0x040041FA RID: 16890
			public readonly MessageDescription MessageDescription;
		}

		// Token: 0x02000BC2 RID: 3010
		internal sealed class TypedMessageKey
		{
			// Token: 0x0600747E RID: 29822 RVA: 0x001B3063 File Offset: 0x001B1263
			public TypedMessageKey(Type type, string contractNS, object extensionData)
			{
				this.type = type;
				this.contractNS = contractNS;
				this.extensionData = extensionData;
			}

			// Token: 0x0600747F RID: 29823 RVA: 0x001B3080 File Offset: 0x001B1280
			public override bool Equals(object obj)
			{
				MessageContractExporter.TypedMessageKey typedMessageKey = obj as MessageContractExporter.TypedMessageKey;
				return typedMessageKey != null && typedMessageKey.type == this.type && typedMessageKey.contractNS == this.contractNS && typedMessageKey.extensionData.Equals(this.extensionData);
			}

			// Token: 0x06007480 RID: 29824 RVA: 0x001B30D3 File Offset: 0x001B12D3
			public override int GetHashCode()
			{
				return this.type.GetHashCode();
			}

			// Token: 0x040041FB RID: 16891
			private Type type;

			// Token: 0x040041FC RID: 16892
			private string contractNS;

			// Token: 0x040041FD RID: 16893
			private object extensionData;
		}

		// Token: 0x02000BC3 RID: 3011
		internal sealed class OperationMessageKey
		{
			// Token: 0x06007481 RID: 29825 RVA: 0x001B30E0 File Offset: 0x001B12E0
			public OperationMessageKey(OperationDescription operation, int messageIndex)
			{
				this.methodInfo = operation.OperationMethod;
				this.messageIndex = messageIndex;
				this.declaringContract = operation.DeclaringContract;
			}

			// Token: 0x06007482 RID: 29826 RVA: 0x001B3108 File Offset: 0x001B1308
			public override bool Equals(object obj)
			{
				MessageContractExporter.OperationMessageKey operationMessageKey = obj as MessageContractExporter.OperationMessageKey;
				return operationMessageKey != null && operationMessageKey.methodInfo == this.methodInfo && operationMessageKey.messageIndex == this.messageIndex && operationMessageKey.declaringContract.Name == this.declaringContract.Name && operationMessageKey.declaringContract.Namespace == this.declaringContract.Namespace;
			}

			// Token: 0x06007483 RID: 29827 RVA: 0x001B317D File Offset: 0x001B137D
			public override int GetHashCode()
			{
				return this.methodInfo.GetHashCode() ^ this.messageIndex;
			}

			// Token: 0x040041FE RID: 16894
			private MethodInfo methodInfo;

			// Token: 0x040041FF RID: 16895
			private int messageIndex;

			// Token: 0x04004200 RID: 16896
			private ContractDescription declaringContract;
		}

		// Token: 0x02000BC4 RID: 3012
		internal sealed class OperationElement
		{
			// Token: 0x06007484 RID: 29828 RVA: 0x001B3191 File Offset: 0x001B1391
			internal OperationElement(XmlSchemaElement element, OperationDescription operation)
			{
				this.element = element;
				this.operation = operation;
			}

			// Token: 0x17001AE8 RID: 6888
			// (get) Token: 0x06007485 RID: 29829 RVA: 0x001B31A7 File Offset: 0x001B13A7
			internal XmlSchemaElement Element
			{
				get
				{
					return this.element;
				}
			}

			// Token: 0x17001AE9 RID: 6889
			// (get) Token: 0x06007486 RID: 29830 RVA: 0x001B31AF File Offset: 0x001B13AF
			internal OperationDescription Operation
			{
				get
				{
					return this.operation;
				}
			}

			// Token: 0x04004201 RID: 16897
			private XmlSchemaElement element;

			// Token: 0x04004202 RID: 16898
			private OperationDescription operation;
		}
	}
}
