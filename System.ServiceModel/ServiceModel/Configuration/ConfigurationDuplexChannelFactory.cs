using System;
using System.Configuration;
using System.ServiceModel.Diagnostics;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020006F0 RID: 1776
	public sealed class ConfigurationDuplexChannelFactory<TChannel> : DuplexChannelFactory<TChannel>
	{
		// Token: 0x06004420 RID: 17440 RVA: 0x00101544 File Offset: 0x000FF744
		public ConfigurationDuplexChannelFactory(object callbackObject, string endpointConfigurationName, EndpointAddress remoteAddress, Configuration configuration) : base(typeof(TChannel))
		{
			using (ServiceModelActivity serviceModelActivity = DiagnosticUtility.ShouldUseActivity ? ServiceModelActivity.CreateBoundedActivity() : null)
			{
				if (DiagnosticUtility.ShouldUseActivity)
				{
					ServiceModelActivity.Start(serviceModelActivity, SR.GetString("ActivityConstructChannelFactory", new object[]
					{
						TraceUtility.CreateSourceString(this)
					}), ActivityType.Construct);
				}
				if (callbackObject == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("callbackObject");
				}
				if (endpointConfigurationName == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("endpointConfigurationName");
				}
				if (configuration == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("configuration");
				}
				base.CheckAndAssignCallbackInstance(callbackObject);
				base.InitializeEndpoint(endpointConfigurationName, remoteAddress, configuration);
			}
		}
	}
}
