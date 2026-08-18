using System;
using System.Collections.Specialized;

namespace System.Web.UI
{
	// Token: 0x020002B1 RID: 689
	public interface IPostBackDataHandler
	{
		// Token: 0x06001FB3 RID: 8115
		bool LoadPostData(string postDataKey, NameValueCollection postCollection);

		// Token: 0x06001FB4 RID: 8116
		void RaisePostDataChangedEvent();
	}
}
