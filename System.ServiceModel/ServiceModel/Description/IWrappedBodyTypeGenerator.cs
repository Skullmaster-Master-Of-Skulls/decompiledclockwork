using System;
using System.CodeDom;

namespace System.ServiceModel.Description
{
	// Token: 0x02000415 RID: 1045
	internal interface IWrappedBodyTypeGenerator
	{
		// Token: 0x06002808 RID: 10248
		void ValidateForParameterMode(OperationDescription operationDescription);

		// Token: 0x06002809 RID: 10249
		void AddMemberAttributes(XmlName messageName, MessagePartDescription part, CodeAttributeDeclarationCollection attributesImported, CodeAttributeDeclarationCollection typeAttributes, CodeAttributeDeclarationCollection fieldAttributes);

		// Token: 0x0600280A RID: 10250
		void AddTypeAttributes(string messageName, string typeNS, CodeAttributeDeclarationCollection typeAttributes, bool isEncoded);
	}
}
