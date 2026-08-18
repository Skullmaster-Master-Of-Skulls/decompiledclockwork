using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using WCFExtras.Utils;

namespace WCFExtras.Soap
{
	// Token: 0x0200000E RID: 14
	internal class SoapHeaderOpExtension : IOperationBehavior, IOperationContractGenerationExtension
	{
		// Token: 0x06000040 RID: 64 RVA: 0x00003427 File Offset: 0x00001627
		public SoapHeaderOpExtension(Dictionary<MessageHeaderDescription, SoapHeaderDirection> headers)
		{
			this.headers = headers;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00003439 File Offset: 0x00001639
		void IOperationBehavior.AddBindingParameters(OperationDescription operationDescription, BindingParameterCollection bindingParameters)
		{
		}

		// Token: 0x06000042 RID: 66 RVA: 0x0000343C File Offset: 0x0000163C
		void IOperationBehavior.ApplyClientBehavior(OperationDescription operationDescription, ClientOperation clientOperation)
		{
		}

		// Token: 0x06000043 RID: 67 RVA: 0x0000343F File Offset: 0x0000163F
		void IOperationBehavior.ApplyDispatchBehavior(OperationDescription operationDescription, DispatchOperation dispatchOperation)
		{
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00003442 File Offset: 0x00001642
		void IOperationBehavior.Validate(OperationDescription operationDescription)
		{
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00003448 File Offset: 0x00001648
		void IOperationContractGenerationExtension.GenerateOperation(OperationContractGenerationContext context)
		{
			foreach (KeyValuePair<MessageHeaderDescription, SoapHeaderDirection> keyValuePair in this.headers)
			{
				MessageHeaderDescription key = keyValuePair.Key;
				string type = (string)ReflectionUtils.GetValue(key, "BaseType");
				CodeAttributeArgument codeAttributeArgument = new CodeAttributeArgument(new CodePrimitiveExpression(key.Name));
				CodeAttributeArgument codeAttributeArgument2 = new CodeAttributeArgument(new CodeTypeOfExpression(type));
				CodeAttributeArgument codeAttributeArgument3 = new CodeAttributeArgument("Direction", new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(typeof(SoapHeaderDirection)), keyValuePair.Value.ToString()));
				CodeAttributeDeclaration value = new CodeAttributeDeclaration(new CodeTypeReference(typeof(SoapHeaderAttribute)), new CodeAttributeArgument[]
				{
					codeAttributeArgument,
					codeAttributeArgument2,
					codeAttributeArgument3
				});
				context.SyncMethod.CustomAttributes.Add(value);
			}
		}

		// Token: 0x0400000E RID: 14
		private Dictionary<MessageHeaderDescription, SoapHeaderDirection> headers;
	}
}
