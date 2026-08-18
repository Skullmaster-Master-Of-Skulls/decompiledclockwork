using System;
using System.Runtime;

namespace System.ServiceModel.Channels
{
	// Token: 0x020007E6 RID: 2022
	internal class DoneReceivingAsyncResult : CompletedAsyncResult
	{
		// Token: 0x06004C82 RID: 19586 RVA: 0x00117337 File Offset: 0x00115537
		internal DoneReceivingAsyncResult(AsyncCallback callback, object state) : base(callback, state)
		{
		}

		// Token: 0x06004C83 RID: 19587 RVA: 0x00117341 File Offset: 0x00115541
		internal static bool End(DoneReceivingAsyncResult result, out Message message)
		{
			message = null;
			return true;
		}

		// Token: 0x06004C84 RID: 19588 RVA: 0x00117347 File Offset: 0x00115547
		internal static bool End(DoneReceivingAsyncResult result, out RequestContext requestContext)
		{
			requestContext = null;
			return true;
		}

		// Token: 0x06004C85 RID: 19589 RVA: 0x0011734D File Offset: 0x0011554D
		internal static bool End(DoneReceivingAsyncResult result)
		{
			return true;
		}
	}
}
