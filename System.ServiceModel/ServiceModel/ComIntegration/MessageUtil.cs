using System;
using System.EnterpriseServices;
using System.Security.Principal;
using System.ServiceModel.Channels;
using System.ServiceModel.Security;
using System.Transactions;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x02000234 RID: 564
	internal static class MessageUtil
	{
		// Token: 0x060010D5 RID: 4309 RVA: 0x0003D1D0 File Offset: 0x0003B3D0
		public static WindowsIdentity GetMessageIdentity(Message message)
		{
			WindowsIdentity windowsIdentity = null;
			SecurityMessageProperty security = message.Properties.Security;
			if (security != null)
			{
				ServiceSecurityContext serviceSecurityContext = security.ServiceSecurityContext;
				if (serviceSecurityContext != null)
				{
					if (serviceSecurityContext.WindowsIdentity == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(Error.RequiresWindowsSecurity());
					}
					windowsIdentity = serviceSecurityContext.WindowsIdentity;
				}
			}
			if (windowsIdentity == null || windowsIdentity.IsAnonymous)
			{
				windowsIdentity = SecurityUtils.GetAnonymousIdentity();
			}
			return windowsIdentity;
		}

		// Token: 0x060010D6 RID: 4310 RVA: 0x0003D22C File Offset: 0x0003B42C
		public static Transaction GetMessageTransaction(Message message)
		{
			ServiceDomain.Enter(new ServiceConfig
			{
				Transaction = TransactionOption.Disabled
			});
			Transaction result;
			try
			{
				result = TransactionMessageProperty.TryGetTransaction(message);
			}
			finally
			{
				ServiceDomain.Leave();
			}
			return result;
		}
	}
}
