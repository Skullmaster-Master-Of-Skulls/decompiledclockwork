using System;
using System.Collections.ObjectModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace System.ServiceModel.Description
{
	// Token: 0x0200043C RID: 1084
	public class ServiceThrottlingBehavior : IServiceBehavior
	{
		// Token: 0x17000A5A RID: 2650
		// (get) Token: 0x06002A5E RID: 10846 RVA: 0x000A3E0E File Offset: 0x000A200E
		// (set) Token: 0x06002A5F RID: 10847 RVA: 0x000A3E16 File Offset: 0x000A2016
		public int MaxConcurrentCalls
		{
			get
			{
				return this.calls;
			}
			set
			{
				if (value <= 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxThrottleLimitMustBeGreaterThanZero0")));
				}
				this.calls = value;
			}
		}

		// Token: 0x17000A5B RID: 2651
		// (get) Token: 0x06002A60 RID: 10848 RVA: 0x000A3E3D File Offset: 0x000A203D
		// (set) Token: 0x06002A61 RID: 10849 RVA: 0x000A3E45 File Offset: 0x000A2045
		public int MaxConcurrentSessions
		{
			get
			{
				return this.sessions;
			}
			set
			{
				if (value <= 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxThrottleLimitMustBeGreaterThanZero0")));
				}
				this.sessions = value;
			}
		}

		// Token: 0x17000A5C RID: 2652
		// (get) Token: 0x06002A62 RID: 10850 RVA: 0x000A3E6C File Offset: 0x000A206C
		// (set) Token: 0x06002A63 RID: 10851 RVA: 0x000A3EAA File Offset: 0x000A20AA
		public int MaxConcurrentInstances
		{
			get
			{
				if (this.maxInstanceSetExplicitly)
				{
					return this.instances;
				}
				this.instances = this.calls + this.sessions;
				if (this.instances < 0)
				{
					this.instances = int.MaxValue;
				}
				return this.instances;
			}
			set
			{
				if (value <= 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxThrottleLimitMustBeGreaterThanZero0")));
				}
				this.instances = value;
				this.maxInstanceSetExplicitly = true;
			}
		}

		// Token: 0x06002A64 RID: 10852 RVA: 0x000A3ED8 File Offset: 0x000A20D8
		void IServiceBehavior.Validate(ServiceDescription description, ServiceHostBase serviceHostBase)
		{
		}

		// Token: 0x06002A65 RID: 10853 RVA: 0x000A3EDA File Offset: 0x000A20DA
		void IServiceBehavior.AddBindingParameters(ServiceDescription description, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection parameters)
		{
		}

		// Token: 0x06002A66 RID: 10854 RVA: 0x000A3EDC File Offset: 0x000A20DC
		void IServiceBehavior.ApplyDispatchBehavior(ServiceDescription description, ServiceHostBase serviceHostBase)
		{
			if (serviceHostBase == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("serviceHostBase"));
			}
			ServiceThrottle serviceThrottle = serviceHostBase.ServiceThrottle;
			serviceThrottle.MaxConcurrentCalls = this.calls;
			serviceThrottle.MaxConcurrentSessions = this.sessions;
			serviceThrottle.MaxConcurrentInstances = this.MaxConcurrentInstances;
			for (int i = 0; i < serviceHostBase.ChannelDispatchers.Count; i++)
			{
				ChannelDispatcher channelDispatcher = serviceHostBase.ChannelDispatchers[i] as ChannelDispatcher;
				if (channelDispatcher != null)
				{
					if (serviceThrottle != channelDispatcher.ServiceThrottle && channelDispatcher.IsServiceThrottleReplaced)
					{
						channelDispatcher.ServiceThrottle = new ServiceThrottle(serviceHostBase)
						{
							MaxConcurrentCalls = this.calls,
							MaxConcurrentSessions = this.sessions,
							MaxConcurrentInstances = this.MaxConcurrentInstances
						};
					}
					else
					{
						channelDispatcher.ServiceThrottle = serviceThrottle;
					}
				}
			}
		}

		// Token: 0x040022CC RID: 8908
		internal static int DefaultMaxConcurrentInstances = ServiceThrottle.DefaultMaxConcurrentCallsCpuCount + ServiceThrottle.DefaultMaxConcurrentSessionsCpuCount;

		// Token: 0x040022CD RID: 8909
		private int calls = ServiceThrottle.DefaultMaxConcurrentCallsCpuCount;

		// Token: 0x040022CE RID: 8910
		private int sessions = ServiceThrottle.DefaultMaxConcurrentSessionsCpuCount;

		// Token: 0x040022CF RID: 8911
		private int instances = int.MaxValue;

		// Token: 0x040022D0 RID: 8912
		private bool maxInstanceSetExplicitly;
	}
}
