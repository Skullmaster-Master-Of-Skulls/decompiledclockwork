using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x0200049D RID: 1181
	internal interface IFunctionLibrary
	{
		// Token: 0x06002D41 RID: 11585
		QueryFunction Bind(string functionName, string functionNamespace, XPathExprList args);
	}
}
