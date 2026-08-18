using System;
using Telerik.Web.UI.Common;

namespace Telerik.Web.UI
{
	// Token: 0x0200026B RID: 619
	internal class DialogHashService
	{
		// Token: 0x06001660 RID: 5728 RVA: 0x0004C074 File Offset: 0x0004A274
		public static HmacEnabledCryptoService GetService()
		{
			if (DialogHashService.service == null)
			{
				lock (DialogHashService.serviceLock)
				{
					if (DialogHashService.service == null)
					{
						DialogHashService.service = new HmacEnabledCryptoService(CryptoService.GetService("Telerik.Web.UI.DialogParametersEncryptionKey"), HmacService.GetService());
					}
				}
			}
			return DialogHashService.service;
		}

		// Token: 0x040005EC RID: 1516
		private static HmacEnabledCryptoService service = null;

		// Token: 0x040005ED RID: 1517
		private static readonly object serviceLock = new object();
	}
}
