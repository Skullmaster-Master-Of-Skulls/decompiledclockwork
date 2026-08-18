using System;
using System.ServiceModel.Activation;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x02000276 RID: 630
	public sealed class WasHostedComPlusFactory : ServiceHostFactoryBase
	{
		// Token: 0x060011FD RID: 4605 RVA: 0x00041FAC File Offset: 0x000401AC
		public override ServiceHostBase CreateServiceHost(string constructorString, Uri[] baseAddresses)
		{
			if (!AspNetEnvironment.Enabled)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("Hosting_ProcessNotExecutingUnderHostedContext", new object[]
				{
					"WasHostedComPlusFactory.CreateServiceHost"
				})));
			}
			if (string.IsNullOrEmpty(constructorString))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("Hosting_ServiceTypeNotProvided")));
			}
			return new WebHostedComPlusServiceHost(constructorString, baseAddresses);
		}
	}
}
