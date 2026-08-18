using System;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Diagnostics.Application;
using System.Threading;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020005A3 RID: 1443
	public sealed class ServiceThrottle
	{
		// Token: 0x060037F9 RID: 14329 RVA: 0x000D766C File Offset: 0x000D586C
		internal ServiceThrottle(ServiceHostBase host)
		{
			if (host == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("host");
			}
			this.host = host;
			this.MaxConcurrentCalls = ServiceThrottle.DefaultMaxConcurrentCallsCpuCount;
			this.MaxConcurrentSessions = ServiceThrottle.DefaultMaxConcurrentSessionsCpuCount;
			this.isActive = true;
		}

		// Token: 0x17000D4F RID: 3407
		// (get) Token: 0x060037FA RID: 14330 RVA: 0x000D76C4 File Offset: 0x000D58C4
		internal FlowThrottle Calls
		{
			get
			{
				if (this.calls == null)
				{
					object obj = this.ThisLock;
					lock (obj)
					{
						if (this.calls == null)
						{
							FlowThrottle flowThrottle = new FlowThrottle(new WaitCallback(this.GotCall), ServiceThrottle.DefaultMaxConcurrentCallsCpuCount, "MaxConcurrentCalls", "maxConcurrentCalls");
							flowThrottle.SetRatio(new Action<int>(this.RatioCallsToken));
							this.calls = flowThrottle;
						}
					}
				}
				return this.calls;
			}
		}

		// Token: 0x17000D50 RID: 3408
		// (get) Token: 0x060037FB RID: 14331 RVA: 0x000D7750 File Offset: 0x000D5950
		internal FlowThrottle Sessions
		{
			get
			{
				if (this.sessions == null)
				{
					object obj = this.ThisLock;
					lock (obj)
					{
						if (this.sessions == null)
						{
							FlowThrottle flowThrottle = new FlowThrottle(new WaitCallback(this.GotSession), ServiceThrottle.DefaultMaxConcurrentSessionsCpuCount, "MaxConcurrentSessions", "maxConcurrentSessions");
							flowThrottle.SetRatio(new Action<int>(this.RatioSessionsToken));
							this.sessions = flowThrottle;
						}
					}
				}
				return this.sessions;
			}
		}

		// Token: 0x17000D51 RID: 3409
		// (get) Token: 0x060037FC RID: 14332 RVA: 0x000D77DC File Offset: 0x000D59DC
		internal QuotaThrottle Dynamic
		{
			get
			{
				if (this.dynamic == null)
				{
					object obj = this.ThisLock;
					lock (obj)
					{
						if (this.dynamic == null)
						{
							this.dynamic = new QuotaThrottle(new WaitCallback(this.GotDynamic), new object())
							{
								Owner = "ServiceHost"
							};
						}
					}
				}
				this.UpdateIsActive();
				return this.dynamic;
			}
		}

		// Token: 0x17000D52 RID: 3410
		// (get) Token: 0x060037FD RID: 14333 RVA: 0x000D785C File Offset: 0x000D5A5C
		// (set) Token: 0x060037FE RID: 14334 RVA: 0x000D7869 File Offset: 0x000D5A69
		internal int ManualFlowControlLimit
		{
			get
			{
				return this.Dynamic.Limit;
			}
			set
			{
				this.Dynamic.SetLimit(value);
			}
		}

		// Token: 0x17000D53 RID: 3411
		// (get) Token: 0x060037FF RID: 14335 RVA: 0x000D7877 File Offset: 0x000D5A77
		// (set) Token: 0x06003800 RID: 14336 RVA: 0x000D7884 File Offset: 0x000D5A84
		public int MaxConcurrentCalls
		{
			get
			{
				return this.Calls.Capacity;
			}
			set
			{
				this.ThrowIfClosedOrOpened("MaxConcurrentCalls");
				this.Calls.Capacity = value;
				this.UpdateIsActive();
				if (this.servicePerformanceCounters != null)
				{
					this.servicePerformanceCounters.SetThrottleBase(34, (long)this.Calls.Capacity);
				}
			}
		}

		// Token: 0x17000D54 RID: 3412
		// (get) Token: 0x06003801 RID: 14337 RVA: 0x000D78C4 File Offset: 0x000D5AC4
		// (set) Token: 0x06003802 RID: 14338 RVA: 0x000D78D1 File Offset: 0x000D5AD1
		public int MaxConcurrentSessions
		{
			get
			{
				return this.Sessions.Capacity;
			}
			set
			{
				this.ThrowIfClosedOrOpened("MaxConcurrentSessions");
				this.Sessions.Capacity = value;
				this.UpdateIsActive();
				if (this.servicePerformanceCounters != null)
				{
					this.servicePerformanceCounters.SetThrottleBase(38, (long)this.Sessions.Capacity);
				}
			}
		}

		// Token: 0x17000D55 RID: 3413
		// (get) Token: 0x06003803 RID: 14339 RVA: 0x000D7911 File Offset: 0x000D5B11
		// (set) Token: 0x06003804 RID: 14340 RVA: 0x000D791E File Offset: 0x000D5B1E
		public int MaxConcurrentInstances
		{
			get
			{
				return this.InstanceContexts.Capacity;
			}
			set
			{
				this.ThrowIfClosedOrOpened("MaxConcurrentInstances");
				this.InstanceContexts.Capacity = value;
				this.UpdateIsActive();
				if (this.servicePerformanceCounters != null)
				{
					this.servicePerformanceCounters.SetThrottleBase(36, (long)this.InstanceContexts.Capacity);
				}
			}
		}

		// Token: 0x17000D56 RID: 3414
		// (get) Token: 0x06003805 RID: 14341 RVA: 0x000D7960 File Offset: 0x000D5B60
		internal FlowThrottle InstanceContexts
		{
			get
			{
				if (this.instanceContexts == null)
				{
					object obj = this.ThisLock;
					lock (obj)
					{
						if (this.instanceContexts == null)
						{
							FlowThrottle flowThrottle = new FlowThrottle(new WaitCallback(this.GotInstanceContext), int.MaxValue, "MaxConcurrentInstances", "maxConcurrentInstances");
							flowThrottle.SetRatio(new Action<int>(this.RatioInstancesToken));
							if (this.servicePerformanceCounters != null)
							{
								this.InitializeInstancePerfCounterSettings(flowThrottle);
							}
							this.instanceContexts = flowThrottle;
						}
					}
				}
				return this.instanceContexts;
			}
		}

		// Token: 0x17000D57 RID: 3415
		// (get) Token: 0x06003806 RID: 14342 RVA: 0x000D79FC File Offset: 0x000D5BFC
		internal bool IsActive
		{
			get
			{
				return this.isActive;
			}
		}

		// Token: 0x17000D58 RID: 3416
		// (get) Token: 0x06003807 RID: 14343 RVA: 0x000D7A04 File Offset: 0x000D5C04
		internal object ThisLock
		{
			get
			{
				return this.thisLock;
			}
		}

		// Token: 0x06003808 RID: 14344 RVA: 0x000D7A0C File Offset: 0x000D5C0C
		internal void SetServicePerformanceCounters(ServicePerformanceCountersBase counters)
		{
			this.servicePerformanceCounters = counters;
			if (this.instanceContexts != null)
			{
				this.InitializeInstancePerfCounterSettings(this.instanceContexts);
			}
			this.InitializeCallsPerfCounterSettings();
			this.InitializeSessionsPerfCounterSettings();
		}

		// Token: 0x06003809 RID: 14345 RVA: 0x000D7A38 File Offset: 0x000D5C38
		private void InitializeInstancePerfCounterSettings(FlowThrottle instanceContextsFt)
		{
			instanceContextsFt.SetAcquired(new Action(this.AcquiredInstancesToken));
			instanceContextsFt.SetReleased(new Action(this.ReleasedInstancesToken));
			instanceContextsFt.SetRatio(new Action<int>(this.RatioInstancesToken));
			this.servicePerformanceCounters.SetThrottleBase(36, (long)instanceContextsFt.Capacity);
		}

		// Token: 0x0600380A RID: 14346 RVA: 0x000D7A90 File Offset: 0x000D5C90
		private void InitializeCallsPerfCounterSettings()
		{
			this.calls.SetAcquired(new Action(this.AcquiredCallsToken));
			this.calls.SetReleased(new Action(this.ReleasedCallsToken));
			this.calls.SetRatio(new Action<int>(this.RatioCallsToken));
			this.servicePerformanceCounters.SetThrottleBase(34, (long)this.calls.Capacity);
		}

		// Token: 0x0600380B RID: 14347 RVA: 0x000D7AFC File Offset: 0x000D5CFC
		private void InitializeSessionsPerfCounterSettings()
		{
			this.sessions.SetAcquired(new Action(this.AcquiredSessionsToken));
			this.sessions.SetReleased(new Action(this.ReleasedSessionsToken));
			this.sessions.SetRatio(new Action<int>(this.RatioSessionsToken));
			this.servicePerformanceCounters.SetThrottleBase(38, (long)this.sessions.Capacity);
		}

		// Token: 0x0600380C RID: 14348 RVA: 0x000D7B67 File Offset: 0x000D5D67
		private bool PrivateAcquireCall(ChannelHandler channel)
		{
			return this.calls == null || this.calls.Acquire(channel);
		}

		// Token: 0x0600380D RID: 14349 RVA: 0x000D7B7F File Offset: 0x000D5D7F
		private bool PrivateAcquireSessionListenerHandler(ListenerHandler listener)
		{
			if (this.sessions != null && listener.Channel != null && listener.Channel.Throttle == null)
			{
				listener.Channel.Throttle = this;
				return this.sessions.Acquire(listener);
			}
			return true;
		}

		// Token: 0x0600380E RID: 14350 RVA: 0x000D7BB8 File Offset: 0x000D5DB8
		private bool PrivateAcquireSession(ISessionThrottleNotification source)
		{
			return this.sessions == null || this.sessions.Acquire(source);
		}

		// Token: 0x0600380F RID: 14351 RVA: 0x000D7BD0 File Offset: 0x000D5DD0
		private bool PrivateAcquireDynamic(ChannelHandler channel)
		{
			return this.dynamic == null || this.dynamic.Acquire(channel);
		}

		// Token: 0x06003810 RID: 14352 RVA: 0x000D7BE8 File Offset: 0x000D5DE8
		private bool PrivateAcquireInstanceContext(ChannelHandler channel)
		{
			if (this.instanceContexts != null && channel.InstanceContext == null)
			{
				channel.InstanceContextServiceThrottle = this;
				return this.instanceContexts.Acquire(channel);
			}
			return true;
		}

		// Token: 0x06003811 RID: 14353 RVA: 0x000D7C10 File Offset: 0x000D5E10
		internal bool AcquireCall(ChannelHandler channel)
		{
			object obj = this.ThisLock;
			bool result;
			lock (obj)
			{
				result = this.PrivateAcquireCall(channel);
			}
			return result;
		}

		// Token: 0x06003812 RID: 14354 RVA: 0x000D7C54 File Offset: 0x000D5E54
		internal bool AcquireInstanceContextAndDynamic(ChannelHandler channel, bool acquireInstanceContextThrottle)
		{
			object obj = this.ThisLock;
			bool result;
			lock (obj)
			{
				if (!acquireInstanceContextThrottle)
				{
					result = this.PrivateAcquireDynamic(channel);
				}
				else
				{
					result = (this.PrivateAcquireInstanceContext(channel) && this.PrivateAcquireDynamic(channel));
				}
			}
			return result;
		}

		// Token: 0x06003813 RID: 14355 RVA: 0x000D7CB0 File Offset: 0x000D5EB0
		internal bool AcquireSession(ISessionThrottleNotification source)
		{
			object obj = this.ThisLock;
			bool result;
			lock (obj)
			{
				result = this.PrivateAcquireSession(source);
			}
			return result;
		}

		// Token: 0x06003814 RID: 14356 RVA: 0x000D7CF4 File Offset: 0x000D5EF4
		internal bool AcquireSession(ListenerHandler listener)
		{
			object obj = this.ThisLock;
			bool result;
			lock (obj)
			{
				result = this.PrivateAcquireSessionListenerHandler(listener);
			}
			return result;
		}

		// Token: 0x06003815 RID: 14357 RVA: 0x000D7D38 File Offset: 0x000D5F38
		private void GotCall(object state)
		{
			ChannelHandler channelHandler = (ChannelHandler)state;
			object obj = this.ThisLock;
			lock (obj)
			{
				channelHandler.ThrottleAcquiredForCall();
			}
		}

		// Token: 0x06003816 RID: 14358 RVA: 0x000D7D80 File Offset: 0x000D5F80
		private void GotDynamic(object state)
		{
			((ChannelHandler)state).ThrottleAcquired();
		}

		// Token: 0x06003817 RID: 14359 RVA: 0x000D7D90 File Offset: 0x000D5F90
		private void GotInstanceContext(object state)
		{
			ChannelHandler channelHandler = (ChannelHandler)state;
			object obj = this.ThisLock;
			lock (obj)
			{
				if (this.PrivateAcquireDynamic(channelHandler))
				{
					channelHandler.ThrottleAcquired();
				}
			}
		}

		// Token: 0x06003818 RID: 14360 RVA: 0x000D7DE0 File Offset: 0x000D5FE0
		private void GotSession(object state)
		{
			((ISessionThrottleNotification)state).ThrottleAcquired();
		}

		// Token: 0x06003819 RID: 14361 RVA: 0x000D7DED File Offset: 0x000D5FED
		internal void DeactivateChannel()
		{
			if (this.isActive && this.sessions != null)
			{
				this.sessions.Release();
			}
		}

		// Token: 0x0600381A RID: 14362 RVA: 0x000D7E0A File Offset: 0x000D600A
		internal void DeactivateCall()
		{
			if (this.isActive && this.calls != null)
			{
				this.calls.Release();
			}
		}

		// Token: 0x0600381B RID: 14363 RVA: 0x000D7E27 File Offset: 0x000D6027
		internal void DeactivateInstanceContext()
		{
			if (this.isActive && this.instanceContexts != null)
			{
				this.instanceContexts.Release();
			}
		}

		// Token: 0x0600381C RID: 14364 RVA: 0x000D7E44 File Offset: 0x000D6044
		internal int IncrementManualFlowControlLimit(int incrementBy)
		{
			return this.Dynamic.IncrementLimit(incrementBy);
		}

		// Token: 0x0600381D RID: 14365 RVA: 0x000D7E52 File Offset: 0x000D6052
		private void ThrowIfClosedOrOpened(string memberName)
		{
			if (this.host.State == CommunicationState.Opened)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxImmutableThrottle1", new object[]
				{
					memberName
				})));
			}
			this.host.ThrowIfClosedOrOpened();
		}

		// Token: 0x0600381E RID: 14366 RVA: 0x000D7E94 File Offset: 0x000D6094
		private void UpdateIsActive()
		{
			this.isActive = (this.dynamic != null || (this.calls != null && this.calls.Capacity != int.MaxValue) || (this.sessions != null && this.sessions.Capacity != int.MaxValue) || (this.instanceContexts != null && this.instanceContexts.Capacity != int.MaxValue));
		}

		// Token: 0x0600381F RID: 14367 RVA: 0x000D7F06 File Offset: 0x000D6106
		internal void AcquiredCallsToken()
		{
			this.servicePerformanceCounters.IncrementThrottlePercent(33);
		}

		// Token: 0x06003820 RID: 14368 RVA: 0x000D7F15 File Offset: 0x000D6115
		internal void ReleasedCallsToken()
		{
			this.servicePerformanceCounters.DecrementThrottlePercent(33);
		}

		// Token: 0x06003821 RID: 14369 RVA: 0x000D7F24 File Offset: 0x000D6124
		internal void RatioCallsToken(int count)
		{
			if (TD.ConcurrentCallsRatioIsEnabled())
			{
				TD.ConcurrentCallsRatio(count, this.MaxConcurrentCalls);
			}
		}

		// Token: 0x06003822 RID: 14370 RVA: 0x000D7F39 File Offset: 0x000D6139
		internal void AcquiredInstancesToken()
		{
			this.servicePerformanceCounters.IncrementThrottlePercent(35);
		}

		// Token: 0x06003823 RID: 14371 RVA: 0x000D7F48 File Offset: 0x000D6148
		internal void ReleasedInstancesToken()
		{
			this.servicePerformanceCounters.DecrementThrottlePercent(35);
		}

		// Token: 0x06003824 RID: 14372 RVA: 0x000D7F57 File Offset: 0x000D6157
		internal void RatioInstancesToken(int count)
		{
			if (TD.ConcurrentInstancesRatioIsEnabled())
			{
				TD.ConcurrentInstancesRatio(count, this.MaxConcurrentInstances);
			}
		}

		// Token: 0x06003825 RID: 14373 RVA: 0x000D7F6C File Offset: 0x000D616C
		internal void AcquiredSessionsToken()
		{
			this.servicePerformanceCounters.IncrementThrottlePercent(37);
		}

		// Token: 0x06003826 RID: 14374 RVA: 0x000D7F7B File Offset: 0x000D617B
		internal void ReleasedSessionsToken()
		{
			this.servicePerformanceCounters.DecrementThrottlePercent(37);
		}

		// Token: 0x06003827 RID: 14375 RVA: 0x000D7F8A File Offset: 0x000D618A
		internal void RatioSessionsToken(int count)
		{
			if (TD.ConcurrentSessionsRatioIsEnabled())
			{
				TD.ConcurrentSessionsRatio(count, this.MaxConcurrentSessions);
			}
		}

		// Token: 0x04002971 RID: 10609
		internal const int DefaultMaxConcurrentCalls = 16;

		// Token: 0x04002972 RID: 10610
		internal const int DefaultMaxConcurrentSessions = 100;

		// Token: 0x04002973 RID: 10611
		internal static int DefaultMaxConcurrentCallsCpuCount = 16 * OSEnvironmentHelper.ProcessorCount;

		// Token: 0x04002974 RID: 10612
		internal static int DefaultMaxConcurrentSessionsCpuCount = 100 * OSEnvironmentHelper.ProcessorCount;

		// Token: 0x04002975 RID: 10613
		private FlowThrottle calls;

		// Token: 0x04002976 RID: 10614
		private FlowThrottle sessions;

		// Token: 0x04002977 RID: 10615
		private QuotaThrottle dynamic;

		// Token: 0x04002978 RID: 10616
		private FlowThrottle instanceContexts;

		// Token: 0x04002979 RID: 10617
		private ServiceHostBase host;

		// Token: 0x0400297A RID: 10618
		private ServicePerformanceCountersBase servicePerformanceCounters;

		// Token: 0x0400297B RID: 10619
		private bool isActive;

		// Token: 0x0400297C RID: 10620
		private object thisLock = new object();

		// Token: 0x0400297D RID: 10621
		private const string MaxConcurrentCallsPropertyName = "MaxConcurrentCalls";

		// Token: 0x0400297E RID: 10622
		private const string MaxConcurrentCallsConfigName = "maxConcurrentCalls";

		// Token: 0x0400297F RID: 10623
		private const string MaxConcurrentSessionsPropertyName = "MaxConcurrentSessions";

		// Token: 0x04002980 RID: 10624
		private const string MaxConcurrentSessionsConfigName = "maxConcurrentSessions";

		// Token: 0x04002981 RID: 10625
		private const string MaxConcurrentInstancesPropertyName = "MaxConcurrentInstances";

		// Token: 0x04002982 RID: 10626
		private const string MaxConcurrentInstancesConfigName = "maxConcurrentInstances";
	}
}
