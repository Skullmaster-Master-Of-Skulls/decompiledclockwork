using System;
using System.Globalization;
using System.Web.Services.Description;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.ServiceModel.Description
{
	// Token: 0x0200040C RID: 1036
	internal class XmlSerializerMessageContractExporter : MessageContractExporter
	{
		// Token: 0x0600277F RID: 10111 RVA: 0x00092DEB File Offset: 0x00090FEB
		internal XmlSerializerMessageContractExporter(WsdlExporter exporter, WsdlContractConversionContext context, OperationDescription operation, IOperationBehavior extension) : base(exporter, context, operation, extension)
		{
		}

		// Token: 0x06002780 RID: 10112 RVA: 0x00092DF8 File Offset: 0x00090FF8
		protected override bool IsRpcStyle()
		{
			return ((XmlSerializerOperationBehavior)this.extension).XmlSerializerFormatAttribute.Style == OperationFormatStyle.Rpc;
		}

		// Token: 0x06002781 RID: 10113 RVA: 0x00092E12 File Offset: 0x00091012
		protected override bool IsEncoded()
		{
			return ((XmlSerializerOperationBehavior)this.extension).XmlSerializerFormatAttribute.IsEncoded;
		}

		// Token: 0x06002782 RID: 10114 RVA: 0x00092E2C File Offset: 0x0009102C
		protected override object OnExportMessageContract()
		{
			object obj = this.Reflector.ReflectOperation(this.operation);
			if (obj == null)
			{
				XmlSerializerOperationBehavior xmlSerializerOperationBehavior = this.extension as XmlSerializerOperationBehavior;
				if (xmlSerializerOperationBehavior != null)
				{
					obj = this.Reflector.ReflectOperation(this.operation, xmlSerializerOperationBehavior.XmlSerializerFormatAttribute);
				}
			}
			return obj;
		}

		// Token: 0x06002783 RID: 10115 RVA: 0x00092E78 File Offset: 0x00091078
		protected override void ExportHeaders(int messageIndex, object state)
		{
			string name = this.contractContext.WsdlPortType.Name;
			string targetNamespace = this.contractContext.WsdlPortType.ServiceDescription.TargetNamespace;
			MessageDescription messageDescription = this.operation.Messages[messageIndex];
			if (messageDescription.Headers.Count > 0)
			{
				XmlSerializerOperationBehavior.Reflector.OperationReflector operationReflector = (XmlSerializerOperationBehavior.Reflector.OperationReflector)state;
				XmlMembersMapping headersMapping;
				if (messageIndex == 0)
				{
					headersMapping = operationReflector.Request.HeadersMapping;
				}
				else
				{
					headersMapping = operationReflector.Reply.HeadersMapping;
				}
				Message message;
				if (headersMapping != null && base.CreateHeaderMessage(messageDescription, out message))
				{
					this.ExportMembersMapping(headersMapping, message, false, operationReflector.IsEncoded, false, false, true);
				}
			}
		}

		// Token: 0x06002784 RID: 10116 RVA: 0x00092F18 File Offset: 0x00091118
		protected override void ExportBody(int messageIndex, object state)
		{
			MessageDescription messageDescription = this.operation.Messages[messageIndex];
			string name = this.contractContext.WsdlPortType.Name;
			string targetNamespace = this.contractContext.WsdlPortType.ServiceDescription.TargetNamespace;
			MessageContractExporter.MessageDescriptionDictionaryKey key = new MessageContractExporter.MessageDescriptionDictionaryKey(this.contractContext.Contract, messageDescription);
			Message message = base.ExportedMessages.WsdlMessages[key];
			XmlSerializerOperationBehavior.Reflector.OperationReflector operationReflector = (XmlSerializerOperationBehavior.Reflector.OperationReflector)state;
			XmlMembersMapping bodyMapping;
			if (messageIndex == 0)
			{
				bodyMapping = operationReflector.Request.BodyMapping;
			}
			else
			{
				bodyMapping = operationReflector.Reply.BodyMapping;
			}
			if (bodyMapping != null)
			{
				bool isDocWrapped = !operationReflector.IsRpc && messageDescription.Body.WrapperName != null;
				this.ExportMembersMapping(bodyMapping, message, false, operationReflector.IsEncoded, operationReflector.IsRpc, isDocWrapped, false);
				if (operationReflector.IsRpc)
				{
					base.AddParameterOrder(this.operation.Messages[messageIndex]);
					base.ExportedMessages.WrapperNamespaces.Add(key, bodyMapping.Namespace);
				}
			}
		}

		// Token: 0x06002785 RID: 10117 RVA: 0x00093024 File Offset: 0x00091224
		protected override void ExportFaults(object state)
		{
			XmlSerializerOperationBehavior.Reflector.OperationReflector operationReflector = (XmlSerializerOperationBehavior.Reflector.OperationReflector)state;
			if (operationReflector.Attribute.SupportFaults)
			{
				foreach (FaultDescription fault in this.operation.Faults)
				{
					this.ExportFault(fault, operationReflector);
				}
				this.Compile();
				return;
			}
			base.ExportFaults(state);
		}

		// Token: 0x06002786 RID: 10118 RVA: 0x0009309C File Offset: 0x0009129C
		private void ExportFault(FaultDescription fault, XmlSerializerOperationBehavior.Reflector.OperationReflector operationReflector)
		{
			Message message = new Message();
			message.Name = base.GetFaultMessageName(fault.Name);
			XmlQualifiedName elementName = this.ExportFaultElement(fault, operationReflector);
			this.contractContext.WsdlPortType.ServiceDescription.Messages.Add(message);
			MessageContractExporter.AddMessagePart(message, "detail", elementName, null);
			OperationFault operationFault = this.contractContext.GetOperationFault(fault);
			WsdlExporter.WSAddressingHelper.AddActionAttribute(fault.Action, operationFault, this.exporter.PolicyVersion);
			operationFault.Message = new XmlQualifiedName(message.Name, message.ServiceDescription.TargetNamespace);
		}

		// Token: 0x06002787 RID: 10119 RVA: 0x00093134 File Offset: 0x00091334
		private XmlQualifiedName ExportFaultElement(FaultDescription fault, XmlSerializerOperationBehavior.Reflector.OperationReflector operationReflector)
		{
			XmlQualifiedName result;
			XmlMembersMapping xmlMembersMapping = operationReflector.ImportFaultElement(fault, out result);
			if (operationReflector.IsEncoded)
			{
				this.SoapExporter.ExportMembersMapping(xmlMembersMapping);
			}
			else
			{
				this.XmlExporter.ExportMembersMapping(xmlMembersMapping);
			}
			return result;
		}

		// Token: 0x06002788 RID: 10120 RVA: 0x0009316E File Offset: 0x0009136E
		protected override void ExportKnownTypes()
		{
		}

		// Token: 0x06002789 RID: 10121 RVA: 0x00093170 File Offset: 0x00091370
		protected override object GetExtensionData()
		{
			return new XmlSerializerMessageContractExporter.ExtensionData(((XmlSerializerOperationBehavior)this.extension).XmlSerializerFormatAttribute);
		}

		// Token: 0x0600278A RID: 10122 RVA: 0x00093188 File Offset: 0x00091388
		private void ExportMembersMapping(XmlMembersMapping membersMapping, Message message, bool skipSchemaExport, bool isEncoded, bool isRpc, bool isDocWrapped, bool isHeader)
		{
			if (!skipSchemaExport)
			{
				if (isEncoded)
				{
					this.SoapExporter.ExportMembersMapping(membersMapping);
				}
				else
				{
					this.XmlExporter.ExportMembersMapping(membersMapping, !isRpc);
				}
			}
			if (!isDocWrapped)
			{
				bool flag = !isRpc && !isEncoded;
				for (int i = 0; i < membersMapping.Count; i++)
				{
					XmlMemberMapping xmlMemberMapping = membersMapping[i];
					string text = (isHeader || flag) ? NamingHelper.XmlName(xmlMemberMapping.MemberName) : xmlMemberMapping.XsdElementName;
					if (flag)
					{
						MessageContractExporter.AddMessagePart(message, text, new XmlQualifiedName(xmlMemberMapping.XsdElementName, xmlMemberMapping.Namespace), XmlQualifiedName.Empty);
					}
					else
					{
						if (string.IsNullOrEmpty(xmlMemberMapping.TypeName))
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxAnonymousTypeNotSupported", new object[]
							{
								message.Name,
								text
							})));
						}
						MessageContractExporter.AddMessagePart(message, text, XmlQualifiedName.Empty, new XmlQualifiedName(xmlMemberMapping.TypeName, xmlMemberMapping.TypeNamespace));
					}
				}
				return;
			}
			if (isHeader)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Header cannot be Document Wrapped", new object[0])));
			}
			MessageContractExporter.AddMessagePart(message, "parameters", new XmlQualifiedName(membersMapping.XsdElementName, membersMapping.Namespace), XmlQualifiedName.Empty);
		}

		// Token: 0x170009DB RID: 2523
		// (get) Token: 0x0600278B RID: 10123 RVA: 0x000932CC File Offset: 0x000914CC
		private XmlSerializerOperationBehavior.Reflector Reflector
		{
			get
			{
				object obj;
				if (!this.exporter.State.TryGetValue(typeof(XmlSerializerOperationBehavior.Reflector), out obj))
				{
					obj = new XmlSerializerOperationBehavior.Reflector(this.contractContext.Contract.Namespace, this.contractContext.Contract.ContractType);
					this.exporter.State.Add(typeof(XmlSerializerOperationBehavior.Reflector), obj);
				}
				return (XmlSerializerOperationBehavior.Reflector)obj;
			}
		}

		// Token: 0x170009DC RID: 2524
		// (get) Token: 0x0600278C RID: 10124 RVA: 0x00093340 File Offset: 0x00091540
		private SoapSchemaExporter SoapExporter
		{
			get
			{
				object obj;
				if (!this.exporter.State.TryGetValue(typeof(SoapSchemaExporter), out obj))
				{
					obj = new SoapSchemaExporter(this.Schemas);
					this.exporter.State.Add(typeof(SoapSchemaExporter), obj);
				}
				return (SoapSchemaExporter)obj;
			}
		}

		// Token: 0x170009DD RID: 2525
		// (get) Token: 0x0600278D RID: 10125 RVA: 0x00093398 File Offset: 0x00091598
		private XmlSchemaExporter XmlExporter
		{
			get
			{
				object obj;
				if (!this.exporter.State.TryGetValue(typeof(XmlSchemaExporter), out obj))
				{
					obj = new XmlSchemaExporter(this.Schemas);
					this.exporter.State.Add(typeof(XmlSchemaExporter), obj);
				}
				return (XmlSchemaExporter)obj;
			}
		}

		// Token: 0x170009DE RID: 2526
		// (get) Token: 0x0600278E RID: 10126 RVA: 0x000933F0 File Offset: 0x000915F0
		private XmlSchemas Schemas
		{
			get
			{
				object obj;
				if (!this.exporter.State.TryGetValue(typeof(XmlSchemas), out obj))
				{
					obj = new XmlSchemas();
					foreach (object obj2 in base.SchemaSet.Schemas())
					{
						XmlSchema xmlSchema = (XmlSchema)obj2;
						if (!((XmlSchemas)obj).Contains(xmlSchema.TargetNamespace))
						{
							((XmlSchemas)obj).Add(xmlSchema);
						}
					}
					this.exporter.State.Add(typeof(XmlSchemas), obj);
				}
				return (XmlSchemas)obj;
			}
		}

		// Token: 0x0600278F RID: 10127 RVA: 0x000934AC File Offset: 0x000916AC
		protected override void Compile()
		{
			XmlSchema schema = StockSchemas.CreateWsdl();
			XmlSchema schema2 = StockSchemas.CreateSoap();
			XmlSchema schema3 = StockSchemas.CreateSoapEncoding();
			XmlSchema schema4 = StockSchemas.CreateFakeXsdSchema();
			this.MoveSchemas();
			base.SchemaSet.Add(schema);
			base.SchemaSet.Add(schema2);
			base.SchemaSet.Add(schema3);
			base.SchemaSet.Add(schema4);
			base.Compile();
			base.SchemaSet.Remove(schema);
			base.SchemaSet.Remove(schema2);
			base.SchemaSet.Remove(schema3);
			base.SchemaSet.Remove(schema4);
		}

		// Token: 0x06002790 RID: 10128 RVA: 0x00093548 File Offset: 0x00091748
		private void MoveSchemas()
		{
			XmlSchemas schemas = this.Schemas;
			XmlSchemaSet schemaSet = base.SchemaSet;
			if (schemas != null)
			{
				schemas.Compile(delegate(object sender, ValidationEventArgs args)
				{
					SchemaHelper.HandleSchemaValidationError(sender, args, this.exporter.Errors);
				}, false);
				foreach (object obj in schemas)
				{
					XmlSchema schema = (XmlSchema)obj;
					if (!schemaSet.Contains(schema))
					{
						schemaSet.Add(schema);
						schemaSet.Reprocess(schema);
					}
				}
			}
		}

		// Token: 0x02000BC6 RID: 3014
		private class ExtensionData
		{
			// Token: 0x0600748A RID: 29834 RVA: 0x001B3207 File Offset: 0x001B1407
			internal ExtensionData(XmlSerializerFormatAttribute xsFormatAttr)
			{
				this.xsFormatAttr = xsFormatAttr;
			}

			// Token: 0x0600748B RID: 29835 RVA: 0x001B3218 File Offset: 0x001B1418
			public override bool Equals(object obj)
			{
				if (this.xsFormatAttr == obj)
				{
					return true;
				}
				XmlSerializerMessageContractExporter.ExtensionData extensionData = obj as XmlSerializerMessageContractExporter.ExtensionData;
				return extensionData != null && this.xsFormatAttr.Style == extensionData.xsFormatAttr.Style && this.xsFormatAttr.Use == extensionData.xsFormatAttr.Use;
			}

			// Token: 0x0600748C RID: 29836 RVA: 0x001B326E File Offset: 0x001B146E
			public override int GetHashCode()
			{
				return 1;
			}

			// Token: 0x04004204 RID: 16900
			private XmlSerializerFormatAttribute xsFormatAttr;
		}
	}
}
