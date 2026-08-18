using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace WCFExtras.Wsdl.Documentation
{
	// Token: 0x02000013 RID: 19
	public class XmlCommentsOpExtension : IOperationBehavior, IOperationContractGenerationExtension
	{
		// Token: 0x06000069 RID: 105 RVA: 0x00003E6A File Offset: 0x0000206A
		void IOperationBehavior.AddBindingParameters(OperationDescription operationDescription, BindingParameterCollection bindingParameters)
		{
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003E6D File Offset: 0x0000206D
		void IOperationBehavior.ApplyClientBehavior(OperationDescription operationDescription, ClientOperation clientOperation)
		{
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003E70 File Offset: 0x00002070
		void IOperationBehavior.ApplyDispatchBehavior(OperationDescription operationDescription, DispatchOperation dispatchOperation)
		{
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003E73 File Offset: 0x00002073
		void IOperationBehavior.Validate(OperationDescription operationDescription)
		{
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003E76 File Offset: 0x00002076
		public XmlCommentsOpExtension(XmlCommentsImporter importer, string documentation)
		{
			this.documentation = documentation;
			this.importer = importer;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003E8F File Offset: 0x0000208F
		void IOperationContractGenerationExtension.GenerateOperation(OperationContractGenerationContext context)
		{
			XmlCommentsImporter.AddXmlComment(context.SyncMethod, this.documentation, XmlCommentsImporter.options);
		}

		// Token: 0x04000016 RID: 22
		private string documentation;

		// Token: 0x04000017 RID: 23
		private XmlCommentsImporter importer;
	}
}
