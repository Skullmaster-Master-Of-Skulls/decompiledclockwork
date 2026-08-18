using System;

namespace System.Web.UI
{
	// Token: 0x0200029B RID: 667
	public interface ICallbackEventHandler
	{
		// Token: 0x06001F7B RID: 8059
		void RaiseCallbackEvent(string eventArgument);

		// Token: 0x06001F7C RID: 8060
		string GetCallbackResult();
	}
}
