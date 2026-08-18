using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using WCFExtrasPlus.Utils;

namespace WCFExtrasPlus.Soap
{
	// Token: 0x02000004 RID: 4
	internal class SoapHeaderOpExtension : IOperationBehavior, IOperationContractGenerationExtension
	{
		// Token: 0x06000009 RID: 9 RVA: 0x0000233C File Offset: 0x0000053C
		public SoapHeaderOpExtension(Dictionary<MessageHeaderDescription, SoapHeaderDirection> headers)
		{
			this.headers = headers;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000234B File Offset: 0x0000054B
		void IOperationBehavior.AddBindingParameters(OperationDescription operationDescription, BindingParameterCollection bindingParameters)
		{
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000234D File Offset: 0x0000054D
		void IOperationBehavior.ApplyClientBehavior(OperationDescription operationDescription, ClientOperation clientOperation)
		{
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000234F File Offset: 0x0000054F
		void IOperationBehavior.ApplyDispatchBehavior(OperationDescription operationDescription, DispatchOperation dispatchOperation)
		{
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002351 File Offset: 0x00000551
		void IOperationBehavior.Validate(OperationDescription operationDescription)
		{
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002354 File Offset: 0x00000554
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
				CodeMemberMethod codeMemberMethod = context.SyncMethod ?? context.BeginMethod;
				if (codeMemberMethod == null)
				{
					codeMemberMethod = context.TaskMethod;
				}
				codeMemberMethod.CustomAttributes.Add(value);
			}
		}

		// Token: 0x04000001 RID: 1
		private Dictionary<MessageHeaderDescription, SoapHeaderDirection> headers;
	}
}
