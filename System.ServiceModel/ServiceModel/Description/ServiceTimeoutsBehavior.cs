using System;
using System.Collections.ObjectModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace System.ServiceModel.Description
{
	// Token: 0x0200043A RID: 1082
	internal class ServiceTimeoutsBehavior : IServiceBehavior
	{
		// Token: 0x06002A31 RID: 10801 RVA: 0x000A31E4 File Offset: 0x000A13E4
		internal ServiceTimeoutsBehavior(TimeSpan transactionTimeout)
		{
			this.transactionTimeout = transactionTimeout;
		}

		// Token: 0x17000A50 RID: 2640
		// (get) Token: 0x06002A32 RID: 10802 RVA: 0x000A31FE File Offset: 0x000A13FE
		// (set) Token: 0x06002A33 RID: 10803 RVA: 0x000A3206 File Offset: 0x000A1406
		internal TimeSpan TransactionTimeout
		{
			get
			{
				return this.transactionTimeout;
			}
			set
			{
				if (value < TimeSpan.Zero)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value, SR.GetString("ValueMustBeNonNegative")));
				}
				this.transactionTimeout = value;
			}
		}

		// Token: 0x06002A34 RID: 10804 RVA: 0x000A3241 File Offset: 0x000A1441
		void IServiceBehavior.Validate(ServiceDescription description, ServiceHostBase serviceHostBase)
		{
		}

		// Token: 0x06002A35 RID: 10805 RVA: 0x000A3243 File Offset: 0x000A1443
		void IServiceBehavior.AddBindingParameters(ServiceDescription description, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection parameters)
		{
		}

		// Token: 0x06002A36 RID: 10806 RVA: 0x000A3248 File Offset: 0x000A1448
		void IServiceBehavior.ApplyDispatchBehavior(ServiceDescription description, ServiceHostBase serviceHostBase)
		{
			if (this.transactionTimeout != TimeSpan.Zero)
			{
				for (int i = 0; i < serviceHostBase.ChannelDispatchers.Count; i++)
				{
					ChannelDispatcher channelDispatcher = serviceHostBase.ChannelDispatchers[i] as ChannelDispatcher;
					if (channelDispatcher != null && channelDispatcher.HasApplicationEndpoints() && (channelDispatcher.TransactionTimeout == TimeSpan.Zero || channelDispatcher.TransactionTimeout > this.transactionTimeout))
					{
						channelDispatcher.TransactionTimeout = this.transactionTimeout;
					}
				}
			}
		}

		// Token: 0x040022BE RID: 8894
		private TimeSpan transactionTimeout = TimeSpan.Zero;
	}
}
