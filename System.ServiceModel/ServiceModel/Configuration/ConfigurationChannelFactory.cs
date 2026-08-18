using System;
using System.Configuration;
using System.ServiceModel.Diagnostics;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020006EF RID: 1775
	public sealed class ConfigurationChannelFactory<TChannel> : ChannelFactory<TChannel>
	{
		// Token: 0x0600441F RID: 17439 RVA: 0x0010149C File Offset: 0x000FF69C
		public ConfigurationChannelFactory(string endpointConfigurationName, Configuration configuration, EndpointAddress remoteAddress) : base(typeof(TChannel))
		{
			using (ServiceModelActivity serviceModelActivity = DiagnosticUtility.ShouldUseActivity ? ServiceModelActivity.CreateBoundedActivity() : null)
			{
				if (DiagnosticUtility.ShouldUseActivity)
				{
					ServiceModelActivity.Start(serviceModelActivity, SR.GetString("ActivityConstructChannelFactory", new object[]
					{
						typeof(TChannel).FullName
					}), ActivityType.Construct);
				}
				if (endpointConfigurationName == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("endpointConfigurationName");
				}
				if (configuration == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("configuration");
				}
				base.InitializeEndpoint(endpointConfigurationName, remoteAddress, configuration);
			}
		}
	}
}
