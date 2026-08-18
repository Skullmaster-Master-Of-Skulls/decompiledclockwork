using System;
using System.CodeDom;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace WCFExtrasPlus.Wsdl.Documentation
{
	// Token: 0x0200001B RID: 27
	public class XmlCommentsOpExtension : IOperationBehavior, IOperationContractGenerationExtension
	{
		// Token: 0x06000099 RID: 153 RVA: 0x000049F7 File Offset: 0x00002BF7
		void IOperationBehavior.AddBindingParameters(OperationDescription operationDescription, BindingParameterCollection bindingParameters)
		{
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000049F9 File Offset: 0x00002BF9
		void IOperationBehavior.ApplyClientBehavior(OperationDescription operationDescription, ClientOperation clientOperation)
		{
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000049FB File Offset: 0x00002BFB
		void IOperationBehavior.ApplyDispatchBehavior(OperationDescription operationDescription, DispatchOperation dispatchOperation)
		{
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000049FD File Offset: 0x00002BFD
		void IOperationBehavior.Validate(OperationDescription operationDescription)
		{
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000049FF File Offset: 0x00002BFF
		public XmlCommentsOpExtension(XmlCommentsImporter importer, string documentation)
		{
			this.documentation = documentation;
			this.importer = importer;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00004A18 File Offset: 0x00002C18
		void IOperationContractGenerationExtension.GenerateOperation(OperationContractGenerationContext context)
		{
			CodeMemberMethod codeMemberMethod = context.SyncMethod ?? context.BeginMethod;
			if (codeMemberMethod == null)
			{
				codeMemberMethod = context.TaskMethod;
			}
			XmlCommentsImporter.AddXmlComment(codeMemberMethod, this.documentation, XmlCommentsImporter.options);
		}

		// Token: 0x04000029 RID: 41
		private string documentation;

		// Token: 0x0400002A RID: 42
		private XmlCommentsImporter importer;
	}
}
