using System;
using System.ServiceModel.Dispatcher;
using System.Web.Services.Description;
using System.Xml;
using System.Xml.Schema;

namespace System.ServiceModel.Description
{
	// Token: 0x0200040B RID: 1035
	internal class DataContractSerializerMessageContractExporter : MessageContractExporter
	{
		// Token: 0x06002775 RID: 10101 RVA: 0x000929CC File Offset: 0x00090BCC
		internal DataContractSerializerMessageContractExporter(WsdlExporter exporter, WsdlContractConversionContext context, OperationDescription operation, IOperationBehavior extension) : base(exporter, context, operation, extension)
		{
		}

		// Token: 0x06002776 RID: 10102 RVA: 0x000929DC File Offset: 0x00090BDC
		protected override void Compile()
		{
			XmlSchema schema = StockSchemas.CreateWsdl();
			XmlSchema schema2 = StockSchemas.CreateSoap();
			XmlSchema schema3 = StockSchemas.CreateSoapEncoding();
			XmlSchema schema4 = StockSchemas.CreateFakeXsdSchema();
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

		// Token: 0x06002777 RID: 10103 RVA: 0x00092A6F File Offset: 0x00090C6F
		protected override bool IsRpcStyle()
		{
			return ((DataContractSerializerOperationBehavior)this.extension).DataContractFormatAttribute.Style == OperationFormatStyle.Rpc;
		}

		// Token: 0x06002778 RID: 10104 RVA: 0x00092A89 File Offset: 0x00090C89
		protected override bool IsEncoded()
		{
			return false;
		}

		// Token: 0x06002779 RID: 10105 RVA: 0x00092A8C File Offset: 0x00090C8C
		protected override object OnExportMessageContract()
		{
			return null;
		}

		// Token: 0x0600277A RID: 10106 RVA: 0x00092A90 File Offset: 0x00090C90
		protected override void ExportHeaders(int messageIndex, object state)
		{
			MessageDescription messageDescription = this.operation.Messages[messageIndex];
			Message message;
			if (messageDescription.Headers.Count > 0 && base.CreateHeaderMessage(messageDescription, out message))
			{
				foreach (MessageHeaderDescription messageHeaderDescription in messageDescription.Headers)
				{
					if (!messageHeaderDescription.IsUnknownHeaderCollection)
					{
						bool flag;
						Type substituteDataContractType = DataContractSerializerOperationFormatter.GetSubstituteDataContractType(messageHeaderDescription.Type, out flag);
						XmlSchemaType xsdType;
						XmlQualifiedName typeName = base.ExportType(substituteDataContractType, messageHeaderDescription.Name, this.operation.Name, out xsdType);
						base.ExportMessagePart(message, messageHeaderDescription, typeName, xsdType, true, DataContractSerializerMessageContractExporter.IsTypeNullable(messageHeaderDescription.Type), false, true, null, null, base.SchemaSet);
					}
				}
			}
		}

		// Token: 0x0600277B RID: 10107 RVA: 0x00092B5C File Offset: 0x00090D5C
		internal static bool IsTypeNullable(Type type)
		{
			return !type.IsValueType || (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>));
		}

		// Token: 0x0600277C RID: 10108 RVA: 0x00092B88 File Offset: 0x00090D88
		protected override void ExportBody(int messageIndex, object state)
		{
			MessageDescription messageDescription = this.operation.Messages[messageIndex];
			Message message = base.ExportedMessages.WsdlMessages[new MessageContractExporter.MessageDescriptionDictionaryKey(this.contractContext.Contract, messageDescription)];
			DataContractFormatAttribute dataContractFormatAttribute = ((DataContractSerializerOperationBehavior)this.extension).DataContractFormatAttribute;
			XmlSchemaSequence wrapperSequence = null;
			bool flag = messageDescription.Body.WrapperName != null;
			if (dataContractFormatAttribute.Style == OperationFormatStyle.Document && flag)
			{
				wrapperSequence = base.ExportWrappedPart(message, messageDescription.Body.WrapperName, messageDescription.Body.WrapperNamespace, base.SchemaSet, false);
			}
			if (OperationFormatter.IsValidReturnValue(messageDescription.Body.ReturnValue))
			{
				bool flag2;
				Type substituteDataContractType = DataContractSerializerOperationFormatter.GetSubstituteDataContractType(messageDescription.Body.ReturnValue.Type, out flag2);
				XmlSchemaType xsdType;
				XmlQualifiedName typeName = base.ExportType(substituteDataContractType, messageDescription.Body.ReturnValue.Name, this.operation.Name, out xsdType);
				base.ExportMessagePart(message, messageDescription.Body.ReturnValue, typeName, xsdType, true, DataContractSerializerMessageContractExporter.IsTypeNullable(messageDescription.Body.ReturnValue.Type), false, dataContractFormatAttribute.Style != OperationFormatStyle.Rpc, messageDescription.Body.WrapperNamespace, wrapperSequence, base.SchemaSet);
			}
			foreach (MessagePartDescription messagePartDescription in messageDescription.Body.Parts)
			{
				bool flag3;
				Type substituteDataContractType2 = DataContractSerializerOperationFormatter.GetSubstituteDataContractType(messagePartDescription.Type, out flag3);
				XmlSchemaType xsdType;
				XmlQualifiedName typeName2 = base.ExportType(substituteDataContractType2, messagePartDescription.Name, this.operation.Name, out xsdType);
				base.ExportMessagePart(message, messagePartDescription, typeName2, xsdType, true, DataContractSerializerMessageContractExporter.IsTypeNullable(messagePartDescription.Type), false, dataContractFormatAttribute.Style != OperationFormatStyle.Rpc, messageDescription.Body.WrapperNamespace, wrapperSequence, base.SchemaSet);
			}
			if (dataContractFormatAttribute.Style == OperationFormatStyle.Rpc)
			{
				base.AddParameterOrder(messageDescription);
			}
		}

		// Token: 0x0600277D RID: 10109 RVA: 0x00092D7C File Offset: 0x00090F7C
		protected override void ExportKnownTypes()
		{
			foreach (Type type in this.operation.KnownTypes)
			{
				base.DataContractExporter.Export(type);
			}
		}

		// Token: 0x0600277E RID: 10110 RVA: 0x00092DD4 File Offset: 0x00090FD4
		protected override object GetExtensionData()
		{
			return new DataContractSerializerMessageContractExporter.ExtensionData(((DataContractSerializerOperationBehavior)this.extension).DataContractFormatAttribute);
		}

		// Token: 0x02000BC5 RID: 3013
		private class ExtensionData
		{
			// Token: 0x06007487 RID: 29831 RVA: 0x001B31B7 File Offset: 0x001B13B7
			internal ExtensionData(DataContractFormatAttribute dcFormatAttr)
			{
				this.dcFormatAttr = dcFormatAttr;
			}

			// Token: 0x06007488 RID: 29832 RVA: 0x001B31C8 File Offset: 0x001B13C8
			public override bool Equals(object obj)
			{
				if (this.dcFormatAttr == obj)
				{
					return true;
				}
				DataContractSerializerMessageContractExporter.ExtensionData extensionData = obj as DataContractSerializerMessageContractExporter.ExtensionData;
				return extensionData != null && this.dcFormatAttr.Style == extensionData.dcFormatAttr.Style;
			}

			// Token: 0x06007489 RID: 29833 RVA: 0x001B3204 File Offset: 0x001B1404
			public override int GetHashCode()
			{
				return 1;
			}

			// Token: 0x04004203 RID: 16899
			private DataContractFormatAttribute dcFormatAttr;
		}
	}
}
