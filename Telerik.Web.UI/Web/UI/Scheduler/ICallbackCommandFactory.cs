using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI.Scheduler
{
	// Token: 0x02000F87 RID: 3975
	internal interface ICallbackCommandFactory
	{
		// Token: 0x0600985A RID: 39002
		ICallbackCommand FromDictionary(IDictionary<string, object> dictionary, JavaScriptSerializer serializer);
	}
}
