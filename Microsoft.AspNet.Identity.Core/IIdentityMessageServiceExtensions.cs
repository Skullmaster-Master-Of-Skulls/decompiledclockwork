using System;

namespace Microsoft.AspNet.Identity
{
	// Token: 0x02000002 RID: 2
	public static class IIdentityMessageServiceExtensions
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020EC File Offset: 0x000002EC
		public static void Send(this IIdentityMessageService service, IdentityMessage message)
		{
			if (service == null)
			{
				throw new ArgumentNullException("service");
			}
			AsyncHelper.RunSync(() => service.SendAsync(message));
		}
	}
}
