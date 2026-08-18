using System;

namespace Telerik.Web.UI
{
	// Token: 0x0200182E RID: 6190
	internal interface IRadCompressionLogger
	{
		// Token: 0x0600F0BF RID: 61631
		void Write(string message);

		// Token: 0x0600F0C0 RID: 61632
		void Write(TFunc<string> info);
	}
}
