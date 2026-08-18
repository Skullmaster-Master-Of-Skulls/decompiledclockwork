using System;

namespace System.Web.Helpers.AntiXsrf
{
	// Token: 0x0200002E RID: 46
	internal interface IAntiForgeryTokenSerializer
	{
		// Token: 0x06000146 RID: 326
		AntiForgeryToken Deserialize(string serializedToken);

		// Token: 0x06000147 RID: 327
		string Serialize(AntiForgeryToken token);
	}
}
