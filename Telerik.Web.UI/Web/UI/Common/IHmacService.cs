using System;

namespace Telerik.Web.UI.Common
{
	// Token: 0x02000092 RID: 146
	internal interface IHmacService
	{
		// Token: 0x0600058A RID: 1418
		int GetHmacLength();

		// Token: 0x0600058B RID: 1419
		string HMAC256(string input);
	}
}
