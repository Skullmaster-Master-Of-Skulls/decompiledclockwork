using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime;
using System.Runtime.Serialization;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Threading;

namespace System.ServiceModel.Description
{
	// Token: 0x02000437 RID: 1079
	[DataContract(Name = "ServiceHealth", Namespace = "http://schemas.microsoft.com/net/2018/08/health")]
	public class ServiceHealthModel
	{
		// Token: 0x06002A12 RID: 10770 RVA: 0x000A2E8C File Offset: 0x000A108C
		public ServiceHealthModel()
		{
		}

		// Token: 0x06002A13 RID: 10771 RVA: 0x000A2E94 File Offset: 0x000A1094
		public ServiceHealthModel(ServiceHostBase serviceHost)
		{
			if (serviceHost == null)
			{
				throw new ArgumentNullException("serviceHost");
			}
			this.Date = DateTimeOffset.Now;
			this.ServiceProperties = new ServiceHealthModel.ServicePropertiesModel(serviceHost);
			this.ProcessInformation = new ServiceHealthModel.ProcessInformationModel(serviceHost);
			this.ProcessThreads = new ServiceHealthModel.ProcessThreadsModel();
			this.ServiceEndpoints = ServiceHealthModel.GetServiceEndpoints(serviceHost);
			this.ChannelDispatchers = ServiceHealthModel.GetChannelDispatchers(serviceHost);
		}

		// Token: 0x06002A14 RID: 10772 RVA: 0x000A2EFB File Offset: 0x000A10FB
		public ServiceHealthModel(ServiceHostBase serviceHost, DateTimeOffset serviceStartTime) : this(serviceHost)
		{
			if (serviceHost == null)
			{
				throw new ArgumentNullException("serviceHost");
			}
			this.ProcessInformation.SetServiceStartDate(serviceStartTime);
		}

		// Token: 0x17000A47 RID: 2631
		// (get) Token: 0x06002A15 RID: 10773 RVA: 0x000A2F1E File Offset: 0x000A111E
		// (set) Token: 0x06002A16 RID: 10774 RVA: 0x000A2F26 File Offset: 0x000A1126
		[DataMember]
		public DateTimeOffset Date { get; private set; }

		// Token: 0x17000A48 RID: 2632
		// (get) Token: 0x06002A17 RID: 10775 RVA: 0x000A2F2F File Offset: 0x000A112F
		// (set) Token: 0x06002A18 RID: 10776 RVA: 0x000A2F37 File Offset: 0x000A1137
		[DataMember]
		public ServiceHealthModel.ServicePropertiesModel ServiceProperties { get; private set; }

		// Token: 0x17000A49 RID: 2633
		// (get) Token: 0x06002A19 RID: 10777 RVA: 0x000A2F40 File Offset: 0x000A1140
		// (set) Token: 0x06002A1A RID: 10778 RVA: 0x000A2F48 File Offset: 0x000A1148
		[DataMember]
		public ServiceHealthModel.ProcessInformationModel ProcessInformation { get; private set; }

		// Token: 0x17000A4A RID: 2634
		// (get) Token: 0x06002A1B RID: 10779 RVA: 0x000A2F51 File Offset: 0x000A1151
		// (set) Token: 0x06002A1C RID: 10780 RVA: 0x000A2F59 File Offset: 0x000A1159
		[DataMember]
		public ServiceHealthModel.ProcessThreadsModel ProcessThreads { get; private set; }

		// Token: 0x17000A4B RID: 2635
		// (get) Token: 0x06002A1D RID: 10781 RVA: 0x000A2F62 File Offset: 0x000A1162
		// (set) Token: 0x06002A1E RID: 10782 RVA: 0x000A2F6A File Offset: 0x000A116A
		[DataMember]
		public ServiceHealthModel.ServiceEndpointModel[] ServiceEndpoints { get; private set; }

		// Token: 0x17000A4C RID: 2636
		// (get) Token: 0x06002A1F RID: 10783 RVA: 0x000A2F73 File Offset: 0x000A1173
		// (set) Token: 0x06002A20 RID: 10784 RVA: 0x000A2F7B File Offset: 0x000A117B
		[DataMember]
		public ServiceHealthModel.ChannelDispatcherModel[] ChannelDispatchers { get; private set; }

		// Token: 0x06002A21 RID: 10785 RVA: 0x000A2F84 File Offset: 0x000A1184
		private static ServiceHealthModel.ServiceEndpointModel[] GetServiceEndpoints(ServiceHostBase serviceHost)
		{
			ServiceDescription description = serviceHost.Description;
			ServiceEndpointCollection serviceEndpointCollection = (description != null) ? description.Endpoints : null;
			List<ServiceHealthModel.ServiceEndpointModel> list = new List<ServiceHealthModel.ServiceEndpointModel>((serviceEndpointCollection != null) ? serviceEndpointCollection.Count : 0);
			if (serviceEndpointCollection != null && serviceEndpointCollection.Count > 0)
			{
				foreach (ServiceEndpoint endpoint in serviceEndpointCollection)
				{
					list.Add(new ServiceHealthModel.ServiceEndpointModel(endpoint));
				}
			}
			return list.ToArray();
		}

		// Token: 0x06002A22 RID: 10786 RVA: 0x000A3008 File Offset: 0x000A1208
		private static ServiceHealthModel.ChannelDispatcherModel[] GetChannelDispatchers(ServiceHostBase serviceHost)
		{
			ChannelDispatcherCollection channelDispatcherCollection = (serviceHost != null) ? serviceHost.ChannelDispatchers : null;
			List<ServiceHealthModel.ChannelDispatcherModel> list = new List<ServiceHealthModel.ChannelDispatcherModel>((channelDispatcherCollection != null) ? channelDispatcherCollection.Count : 0);
			if (channelDispatcherCollection != null && channelDispatcherCollection.Count > 0)
			{
				foreach (ChannelDispatcherBase channelDispatcher in channelDispatcherCollection)
				{
					list.Add(new ServiceHealthModel.ChannelDispatcherModel(channelDispatcher));
				}
			}
			return list.ToArray();
		}

		// Token: 0x040022B2 RID: 8882
		public const string Namespace = "http://schemas.microsoft.com/net/2018/08/health";

		// Token: 0x02000C13 RID: 3091
		[DataContract(Name = "ServiceProperties", Namespace = "http://schemas.microsoft.com/net/2018/08/health")]
		public class ServicePropertiesModel
		{
			// Token: 0x0600764B RID: 30283 RVA: 0x001BC59D File Offset: 0x001BA79D
			public ServicePropertiesModel()
			{
			}

			// Token: 0x0600764C RID: 30284 RVA: 0x001BC5A8 File Offset: 0x001BA7A8
			public ServicePropertiesModel(ServiceHostBase serviceHost)
			{
				if (serviceHost == null)
				{
					throw new ArgumentNullException("serviceHost");
				}
				this.Name = ServiceHealthBehavior.GetServiceName(serviceHost);
				this.State = serviceHost.State;
				ServiceDescription description = serviceHost.Description;
				string serviceTypeName;
				if (description == null)
				{
					serviceTypeName = null;
				}
				else
				{
					Type serviceType = description.ServiceType;
					serviceTypeName = ((serviceType != null) ? serviceType.FullName : null);
				}
				this.ServiceTypeName = serviceTypeName;
				ServiceDescription description2 = serviceHost.Description;
				ServiceBehaviorAttribute serviceBehaviorAttribute = (description2 != null) ? description2.Behaviors.Find<ServiceBehaviorAttribute>() : null;
				if (serviceBehaviorAttribute != null)
				{
					this.InstanceContextMode = new InstanceContextMode?(serviceBehaviorAttribute.InstanceContextMode);
					this.ConcurrencyMode = new ConcurrencyMode?(serviceBehaviorAttribute.ConcurrencyMode);
				}
				this.ServiceBehaviorNames = ServiceHealthModel.ServicePropertiesModel.GetServiceBehaviorNames(serviceHost);
				this.ServiceThrottle = new ServiceHealthModel.ServiceThrottleModel(serviceHost.ServiceThrottle);
				this.BaseAddresses = ServiceHealthModel.ServicePropertiesModel.GetBaseAddresses(serviceHost);
			}

			// Token: 0x17001B0C RID: 6924
			// (get) Token: 0x0600764D RID: 30285 RVA: 0x001BC66B File Offset: 0x001BA86B
			// (set) Token: 0x0600764E RID: 30286 RVA: 0x001BC673 File Offset: 0x001BA873
			[DataMember]
			public string Name { get; private set; }

			// Token: 0x17001B0D RID: 6925
			// (get) Token: 0x0600764F RID: 30287 RVA: 0x001BC67C File Offset: 0x001BA87C
			// (set) Token: 0x06007650 RID: 30288 RVA: 0x001BC684 File Offset: 0x001BA884
			[DataMember]
			public CommunicationState State { get; private set; }

			// Token: 0x17001B0E RID: 6926
			// (get) Token: 0x06007651 RID: 30289 RVA: 0x001BC68D File Offset: 0x001BA88D
			// (set) Token: 0x06007652 RID: 30290 RVA: 0x001BC695 File Offset: 0x001BA895
			[DataMember]
			public string ServiceTypeName { get; private set; }

			// Token: 0x17001B0F RID: 6927
			// (get) Token: 0x06007653 RID: 30291 RVA: 0x001BC69E File Offset: 0x001BA89E
			// (set) Token: 0x06007654 RID: 30292 RVA: 0x001BC6A6 File Offset: 0x001BA8A6
			[DataMember]
			public InstanceContextMode? InstanceContextMode { get; private set; }

			// Token: 0x17001B10 RID: 6928
			// (get) Token: 0x06007655 RID: 30293 RVA: 0x001BC6AF File Offset: 0x001BA8AF
			// (set) Token: 0x06007656 RID: 30294 RVA: 0x001BC6B7 File Offset: 0x001BA8B7
			[DataMember]
			public ConcurrencyMode? ConcurrencyMode { get; private set; }

			// Token: 0x17001B11 RID: 6929
			// (get) Token: 0x06007657 RID: 30295 RVA: 0x001BC6C0 File Offset: 0x001BA8C0
			// (set) Token: 0x06007658 RID: 30296 RVA: 0x001BC6C8 File Offset: 0x001BA8C8
			[DataMember]
			public ServiceHealthModel.ServiceThrottleModel ServiceThrottle { get; private set; }

			// Token: 0x17001B12 RID: 6930
			// (get) Token: 0x06007659 RID: 30297 RVA: 0x001BC6D1 File Offset: 0x001BA8D1
			// (set) Token: 0x0600765A RID: 30298 RVA: 0x001BC6D9 File Offset: 0x001BA8D9
			[DataMember]
			public string[] BaseAddresses { get; private set; }

			// Token: 0x17001B13 RID: 6931
			// (get) Token: 0x0600765B RID: 30299 RVA: 0x001BC6E2 File Offset: 0x001BA8E2
			// (set) Token: 0x0600765C RID: 30300 RVA: 0x001BC6EA File Offset: 0x001BA8EA
			[DataMember]
			public string[] ServiceBehaviorNames { get; private set; }

			// Token: 0x0600765D RID: 30301 RVA: 0x001BC6F4 File Offset: 0x001BA8F4
			private static string[] GetServiceBehaviorNames(ServiceHostBase serviceHost)
			{
				KeyedByTypeCollection<IServiceBehavior> keyedByTypeCollection;
				if (serviceHost == null)
				{
					keyedByTypeCollection = null;
				}
				else
				{
					ServiceDescription description = serviceHost.Description;
					keyedByTypeCollection = ((description != null) ? description.Behaviors : null);
				}
				KeyedByTypeCollection<IServiceBehavior> keyedByTypeCollection2 = keyedByTypeCollection;
				List<string> list = new List<string>((keyedByTypeCollection2 != null) ? keyedByTypeCollection2.Count : 0);
				if (keyedByTypeCollection2 != null)
				{
					foreach (IServiceBehavior serviceBehavior in keyedByTypeCollection2)
					{
						list.Add(serviceBehavior.GetType().FullName);
					}
				}
				return list.ToArray();
			}

			// Token: 0x0600765E RID: 30302 RVA: 0x001BC77C File Offset: 0x001BA97C
			private static string[] GetBaseAddresses(ServiceHostBase serviceHost)
			{
				if (((serviceHost != null) ? serviceHost.BaseAddresses : null) != null)
				{
					int count = serviceHost.BaseAddresses.Count;
					string[] array = new string[count];
					for (int i = 0; i < count; i++)
					{
						array[i] = serviceHost.BaseAddresses[i].ToString();
					}
					return array;
				}
				return null;
			}
		}

		// Token: 0x02000C14 RID: 3092
		[DataContract(Name = "ServiceThrottle", Namespace = "http://schemas.microsoft.com/net/2018/08/health")]
		public class ServiceThrottleModel
		{
			// Token: 0x0600765F RID: 30303 RVA: 0x001BC7CD File Offset: 0x001BA9CD
			public ServiceThrottleModel()
			{
			}

			// Token: 0x06007660 RID: 30304 RVA: 0x001BC7D8 File Offset: 0x001BA9D8
			public ServiceThrottleModel(ServiceThrottle serviceThrottle)
			{
				this.HasThrottle = (serviceThrottle != null);
				if (serviceThrottle == null)
				{
					return;
				}
				this.CallsCount = serviceThrottle.Calls.Count;
				this.CallsCapacity = serviceThrottle.Calls.Capacity;
				this.SessionsCount = serviceThrottle.Sessions.Count;
				this.SessionsCapacity = serviceThrottle.Sessions.Capacity;
				this.InstanceContextsCount = serviceThrottle.InstanceContexts.Count;
				this.InstanceContextsCapacity = serviceThrottle.InstanceContexts.Capacity;
			}

			// Token: 0x17001B14 RID: 6932
			// (get) Token: 0x06007661 RID: 30305 RVA: 0x001BC85F File Offset: 0x001BAA5F
			// (set) Token: 0x06007662 RID: 30306 RVA: 0x001BC867 File Offset: 0x001BAA67
			[DataMember]
			public bool HasThrottle { get; private set; }

			// Token: 0x17001B15 RID: 6933
			// (get) Token: 0x06007663 RID: 30307 RVA: 0x001BC870 File Offset: 0x001BAA70
			// (set) Token: 0x06007664 RID: 30308 RVA: 0x001BC878 File Offset: 0x001BAA78
			[DataMember]
			[DefaultValue(0)]
			public int CallsCount { get; private set; }

			// Token: 0x17001B16 RID: 6934
			// (get) Token: 0x06007665 RID: 30309 RVA: 0x001BC881 File Offset: 0x001BAA81
			// (set) Token: 0x06007666 RID: 30310 RVA: 0x001BC889 File Offset: 0x001BAA89
			[DataMember]
			[DefaultValue(0)]
			public int CallsCapacity { get; private set; }

			// Token: 0x17001B17 RID: 6935
			// (get) Token: 0x06007667 RID: 30311 RVA: 0x001BC892 File Offset: 0x001BAA92
			// (set) Token: 0x06007668 RID: 30312 RVA: 0x001BC89A File Offset: 0x001BAA9A
			[DataMember]
			[DefaultValue(0)]
			public int SessionsCount { get; private set; }

			// Token: 0x17001B18 RID: 6936
			// (get) Token: 0x06007669 RID: 30313 RVA: 0x001BC8A3 File Offset: 0x001BAAA3
			// (set) Token: 0x0600766A RID: 30314 RVA: 0x001BC8AB File Offset: 0x001BAAAB
			[DataMember]
			[DefaultValue(0)]
			public int SessionsCapacity { get; private set; }

			// Token: 0x17001B19 RID: 6937
			// (get) Token: 0x0600766B RID: 30315 RVA: 0x001BC8B4 File Offset: 0x001BAAB4
			// (set) Token: 0x0600766C RID: 30316 RVA: 0x001BC8BC File Offset: 0x001BAABC
			[DataMember]
			[DefaultValue(0)]
			public int InstanceContextsCount { get; private set; }

			// Token: 0x17001B1A RID: 6938
			// (get) Token: 0x0600766D RID: 30317 RVA: 0x001BC8C5 File Offset: 0x001BAAC5
			// (set) Token: 0x0600766E RID: 30318 RVA: 0x001BC8CD File Offset: 0x001BAACD
			[DataMember]
			[DefaultValue(0)]
			public int InstanceContextsCapacity { get; private set; }
		}

		// Token: 0x02000C15 RID: 3093
		[DataContract(Name = "ProcessInformation", Namespace = "http://schemas.microsoft.com/net/2018/08/health")]
		public class ProcessInformationModel
		{
			// Token: 0x0600766F RID: 30319 RVA: 0x001BC8D6 File Offset: 0x001BAAD6
			static ProcessInformationModel()
			{
				ServiceHealthModel.ProcessInformationModel.processName = ServiceHealthModel.ProcessInformationModel.GetProcessName();
				ServiceHealthModel.ProcessInformationModel.bitness = IntPtr.Size * 8;
			}

			// Token: 0x06007670 RID: 30320 RVA: 0x001BC906 File Offset: 0x001BAB06
			public ProcessInformationModel()
			{
			}

			// Token: 0x06007671 RID: 30321 RVA: 0x001BC910 File Offset: 0x001BAB10
			public ProcessInformationModel(ServiceHostBase serviceHost)
			{
				if (serviceHost == null)
				{
					throw new ArgumentNullException("serviceHost");
				}
				this.ProcessName = ServiceHealthModel.ProcessInformationModel.processName;
				this.GCMode = ServiceHealthModel.ProcessInformationModel.gcMode;
				this.ProcessStartDate = ServiceHealthModel.ProcessInformationModel.GetProcessStartDate();
				this.Threads = new ServiceHealthModel.ProcessThreadsModel();
				this.Bitness = ServiceHealthModel.ProcessInformationModel.bitness;
			}

			// Token: 0x17001B1B RID: 6939
			// (get) Token: 0x06007672 RID: 30322 RVA: 0x001BC968 File Offset: 0x001BAB68
			// (set) Token: 0x06007673 RID: 30323 RVA: 0x001BC970 File Offset: 0x001BAB70
			[DataMember]
			public string ProcessName { get; private set; }

			// Token: 0x17001B1C RID: 6940
			// (get) Token: 0x06007674 RID: 30324 RVA: 0x001BC979 File Offset: 0x001BAB79
			// (set) Token: 0x06007675 RID: 30325 RVA: 0x001BC981 File Offset: 0x001BAB81
			[DataMember]
			public int Bitness { get; private set; }

			// Token: 0x17001B1D RID: 6941
			// (get) Token: 0x06007676 RID: 30326 RVA: 0x001BC98A File Offset: 0x001BAB8A
			// (set) Token: 0x06007677 RID: 30327 RVA: 0x001BC992 File Offset: 0x001BAB92
			[DataMember]
			public DateTimeOffset ProcessStartDate { get; private set; }

			// Token: 0x17001B1E RID: 6942
			// (get) Token: 0x06007678 RID: 30328 RVA: 0x001BC99B File Offset: 0x001BAB9B
			// (set) Token: 0x06007679 RID: 30329 RVA: 0x001BC9A3 File Offset: 0x001BABA3
			[DataMember]
			public DateTimeOffset ServiceStartDate { get; private set; }

			// Token: 0x17001B1F RID: 6943
			// (get) Token: 0x0600767A RID: 30330 RVA: 0x001BC9AC File Offset: 0x001BABAC
			// (set) Token: 0x0600767B RID: 30331 RVA: 0x001BC9B4 File Offset: 0x001BABB4
			[DataMember]
			public TimeSpan Uptime { get; private set; }

			// Token: 0x17001B20 RID: 6944
			// (get) Token: 0x0600767C RID: 30332 RVA: 0x001BC9BD File Offset: 0x001BABBD
			// (set) Token: 0x0600767D RID: 30333 RVA: 0x001BC9C5 File Offset: 0x001BABC5
			[DataMember]
			public string GCMode { get; private set; }

			// Token: 0x17001B21 RID: 6945
			// (get) Token: 0x0600767E RID: 30334 RVA: 0x001BC9CE File Offset: 0x001BABCE
			// (set) Token: 0x0600767F RID: 30335 RVA: 0x001BC9D6 File Offset: 0x001BABD6
			[DataMember]
			public ServiceHealthModel.ProcessThreadsModel Threads { get; private set; }

			// Token: 0x06007680 RID: 30336 RVA: 0x001BC9DF File Offset: 0x001BABDF
			public void SetServiceStartDate(DateTimeOffset serviceStartTime)
			{
				this.ServiceStartDate = serviceStartTime;
				this.Uptime = DateTimeOffset.Now - serviceStartTime;
			}

			// Token: 0x06007681 RID: 30337 RVA: 0x001BC9FC File Offset: 0x001BABFC
			private static string GetProcessName()
			{
				string result;
				try
				{
					result = Path.GetFileName(Process.GetCurrentProcess().MainModule.FileName);
				}
				catch
				{
					result = string.Empty;
				}
				return result;
			}

			// Token: 0x06007682 RID: 30338 RVA: 0x001BCA3C File Offset: 0x001BAC3C
			private static DateTimeOffset GetProcessStartDate()
			{
				DateTimeOffset result;
				try
				{
					result = Process.GetCurrentProcess().StartTime;
				}
				catch
				{
					result = DateTimeOffset.MinValue;
				}
				return result;
			}

			// Token: 0x0400430B RID: 17163
			private static string processName;

			// Token: 0x0400430C RID: 17164
			private static string gcMode = GCSettings.IsServerGC ? "Server" : "Workstation";

			// Token: 0x0400430D RID: 17165
			private static int bitness;
		}

		// Token: 0x02000C16 RID: 3094
		[DataContract(Name = "ProcessThreads", Namespace = "http://schemas.microsoft.com/net/2018/08/health")]
		public class ProcessThreadsModel
		{
			// Token: 0x06007683 RID: 30339 RVA: 0x001BCA78 File Offset: 0x001BAC78
			public ProcessThreadsModel()
			{
				int nativeThreadCount = -1;
				int availableWorkerThreads;
				int availableCompletionPortThreads;
				ThreadPool.GetAvailableThreads(out availableWorkerThreads, out availableCompletionPortThreads);
				int minWorkerThreads;
				int minCompletionPortThreads;
				ThreadPool.GetMinThreads(out minWorkerThreads, out minCompletionPortThreads);
				int maxWorkerThreads;
				int maxCompletionPortThreads;
				ThreadPool.GetMaxThreads(out maxWorkerThreads, out maxCompletionPortThreads);
				try
				{
					nativeThreadCount = Process.GetCurrentProcess().Threads.Count;
				}
				catch
				{
				}
				this.AvailableWorkerThreads = availableWorkerThreads;
				this.AvailableCompletionPortThreads = availableCompletionPortThreads;
				this.MinWorkerThreads = minWorkerThreads;
				this.MinCompletionPortThreads = minCompletionPortThreads;
				this.MaxWorkerThreads = maxWorkerThreads;
				this.MaxCompletionPortThreads = maxCompletionPortThreads;
				this.NativeThreadCount = nativeThreadCount;
			}

			// Token: 0x17001B22 RID: 6946
			// (get) Token: 0x06007684 RID: 30340 RVA: 0x001BCB04 File Offset: 0x001BAD04
			// (set) Token: 0x06007685 RID: 30341 RVA: 0x001BCB0C File Offset: 0x001BAD0C
			[DataMember]
			public int AvailableWorkerThreads { get; private set; }

			// Token: 0x17001B23 RID: 6947
			// (get) Token: 0x06007686 RID: 30342 RVA: 0x001BCB15 File Offset: 0x001BAD15
			// (set) Token: 0x06007687 RID: 30343 RVA: 0x001BCB1D File Offset: 0x001BAD1D
			[DataMember]
			public int AvailableCompletionPortThreads { get; private set; }

			// Token: 0x17001B24 RID: 6948
			// (get) Token: 0x06007688 RID: 30344 RVA: 0x001BCB26 File Offset: 0x001BAD26
			// (set) Token: 0x06007689 RID: 30345 RVA: 0x001BCB2E File Offset: 0x001BAD2E
			[DataMember]
			public int MinWorkerThreads { get; private set; }

			// Token: 0x17001B25 RID: 6949
			// (get) Token: 0x0600768A RID: 30346 RVA: 0x001BCB37 File Offset: 0x001BAD37
			// (set) Token: 0x0600768B RID: 30347 RVA: 0x001BCB3F File Offset: 0x001BAD3F
			[DataMember]
			public int MinCompletionPortThreads { get; private set; }

			// Token: 0x17001B26 RID: 6950
			// (get) Token: 0x0600768C RID: 30348 RVA: 0x001BCB48 File Offset: 0x001BAD48
			// (set) Token: 0x0600768D RID: 30349 RVA: 0x001BCB50 File Offset: 0x001BAD50
			[DataMember]
			public int MaxWorkerThreads { get; private set; }

			// Token: 0x17001B27 RID: 6951
			// (get) Token: 0x0600768E RID: 30350 RVA: 0x001BCB59 File Offset: 0x001BAD59
			// (set) Token: 0x0600768F RID: 30351 RVA: 0x001BCB61 File Offset: 0x001BAD61
			[DataMember]
			public int MaxCompletionPortThreads { get; private set; }

			// Token: 0x17001B28 RID: 6952
			// (get) Token: 0x06007690 RID: 30352 RVA: 0x001BCB6A File Offset: 0x001BAD6A
			// (set) Token: 0x06007691 RID: 30353 RVA: 0x001BCB72 File Offset: 0x001BAD72
			[DataMember]
			public int NativeThreadCount { get; private set; }
		}

		// Token: 0x02000C17 RID: 3095
		[DataContract(Name = "ServiceEndpoint", Namespace = "http://schemas.microsoft.com/net/2018/08/health")]
		public class ServiceEndpointModel
		{
			// Token: 0x06007692 RID: 30354 RVA: 0x001BCB7B File Offset: 0x001BAD7B
			public ServiceEndpointModel()
			{
			}

			// Token: 0x06007693 RID: 30355 RVA: 0x001BCB84 File Offset: 0x001BAD84
			public ServiceEndpointModel(ServiceEndpoint endpoint)
			{
				if (endpoint == null)
				{
					return;
				}
				EndpointAddress address = endpoint.Address;
				string address2;
				if (address == null)
				{
					address2 = null;
				}
				else
				{
					Uri uri = address.Uri;
					address2 = ((uri != null) ? uri.ToString() : null);
				}
				this.Address = address2;
				Binding binding = endpoint.Binding;
				this.BindingName = ((binding != null) ? binding.Name : null);
				ContractDescription contract = endpoint.Contract;
				this.ContractName = ((contract != null) ? contract.ContractType.FullName : null);
				KeyedByTypeCollection<IEndpointBehavior> keyedByTypeCollection = (endpoint != null) ? endpoint.Behaviors : null;
				List<string> list = new List<string>();
				if (keyedByTypeCollection != null)
				{
					foreach (IEndpointBehavior endpointBehavior in keyedByTypeCollection)
					{
						list.Add(endpointBehavior.GetType().FullName);
					}
				}
			}

			// Token: 0x17001B29 RID: 6953
			// (get) Token: 0x06007694 RID: 30356 RVA: 0x001BCC50 File Offset: 0x001BAE50
			// (set) Token: 0x06007695 RID: 30357 RVA: 0x001BCC58 File Offset: 0x001BAE58
			[DataMember]
			public string Address { get; private set; }

			// Token: 0x17001B2A RID: 6954
			// (get) Token: 0x06007696 RID: 30358 RVA: 0x001BCC61 File Offset: 0x001BAE61
			// (set) Token: 0x06007697 RID: 30359 RVA: 0x001BCC69 File Offset: 0x001BAE69
			[DataMember]
			public string BindingName { get; private set; }

			// Token: 0x17001B2B RID: 6955
			// (get) Token: 0x06007698 RID: 30360 RVA: 0x001BCC72 File Offset: 0x001BAE72
			// (set) Token: 0x06007699 RID: 30361 RVA: 0x001BCC7A File Offset: 0x001BAE7A
			[DataMember]
			public string ContractName { get; private set; }

			// Token: 0x17001B2C RID: 6956
			// (get) Token: 0x0600769A RID: 30362 RVA: 0x001BCC83 File Offset: 0x001BAE83
			// (set) Token: 0x0600769B RID: 30363 RVA: 0x001BCC8B File Offset: 0x001BAE8B
			[DataMember]
			public string[] BehaviorNames { get; private set; }
		}

		// Token: 0x02000C18 RID: 3096
		[DataContract(Name = "ChannelDispatcher", Namespace = "http://schemas.microsoft.com/net/2018/08/health")]
		public class ChannelDispatcherModel
		{
			// Token: 0x0600769C RID: 30364 RVA: 0x001BCC94 File Offset: 0x001BAE94
			public ChannelDispatcherModel()
			{
			}

			// Token: 0x0600769D RID: 30365 RVA: 0x001BCC9C File Offset: 0x001BAE9C
			public ChannelDispatcherModel(ChannelDispatcherBase channelDispatcher)
			{
				if (channelDispatcher == null)
				{
					return;
				}
				IChannelListener channelListener = (channelDispatcher != null) ? channelDispatcher.Listener : null;
				if (channelListener != null)
				{
					Uri uri = channelListener.Uri;
					this.ListenerUri = ((uri != null) ? uri.ToString() : null);
					this.ListenerState = new CommunicationState?(channelListener.State);
					TransportChannelListener transportChannelListener = channelListener as TransportChannelListener;
					if (transportChannelListener != null)
					{
						string messageEncoder;
						if (transportChannelListener == null)
						{
							messageEncoder = null;
						}
						else
						{
							MessageEncoderFactory messageEncoderFactory = transportChannelListener.MessageEncoderFactory;
							messageEncoder = ((messageEncoderFactory != null) ? messageEncoderFactory.Encoder.GetType().FullName : null);
						}
						this.MessageEncoder = messageEncoder;
					}
				}
				ChannelDispatcher channelDispatcher2 = channelDispatcher as ChannelDispatcher;
				if (channelDispatcher2 != null)
				{
					this.State = new CommunicationState?(channelDispatcher2.State);
					this.BindingName = channelDispatcher2.BindingName;
					this.ServiceThrottle = new ServiceHealthModel.ServiceThrottleModel(channelDispatcher2.ServiceThrottle);
					this.CommunicationTimeouts = new ServiceHealthModel.CommunicationTimeoutsModel(channelDispatcher2.DefaultCommunicationTimeouts);
					if (channelDispatcher2.Endpoints != null && channelDispatcher2.Endpoints.Count > 0)
					{
						EndpointDispatcher endpointDispatcher = channelDispatcher2.Endpoints[0];
						if (endpointDispatcher != null)
						{
							this.ContractName = endpointDispatcher.ContractName;
							this.IsSystemEndpoint = endpointDispatcher.IsSystemEndpoint;
							this.MessageInspectors = ServiceHealthModel.ChannelDispatcherModel.GetMessageInspectors(endpointDispatcher);
						}
					}
				}
			}

			// Token: 0x17001B2D RID: 6957
			// (get) Token: 0x0600769E RID: 30366 RVA: 0x001BCDB4 File Offset: 0x001BAFB4
			// (set) Token: 0x0600769F RID: 30367 RVA: 0x001BCDBC File Offset: 0x001BAFBC
			[DataMember]
			public string ListenerUri { get; private set; }

			// Token: 0x17001B2E RID: 6958
			// (get) Token: 0x060076A0 RID: 30368 RVA: 0x001BCDC5 File Offset: 0x001BAFC5
			// (set) Token: 0x060076A1 RID: 30369 RVA: 0x001BCDCD File Offset: 0x001BAFCD
			[DataMember]
			public CommunicationState? ListenerState { get; private set; }

			// Token: 0x17001B2F RID: 6959
			// (get) Token: 0x060076A2 RID: 30370 RVA: 0x001BCDD6 File Offset: 0x001BAFD6
			// (set) Token: 0x060076A3 RID: 30371 RVA: 0x001BCDDE File Offset: 0x001BAFDE
			[DataMember]
			public string MessageEncoder { get; private set; }

			// Token: 0x17001B30 RID: 6960
			// (get) Token: 0x060076A4 RID: 30372 RVA: 0x001BCDE7 File Offset: 0x001BAFE7
			// (set) Token: 0x060076A5 RID: 30373 RVA: 0x001BCDEF File Offset: 0x001BAFEF
			[DataMember]
			public CommunicationState? State { get; private set; }

			// Token: 0x17001B31 RID: 6961
			// (get) Token: 0x060076A6 RID: 30374 RVA: 0x001BCDF8 File Offset: 0x001BAFF8
			// (set) Token: 0x060076A7 RID: 30375 RVA: 0x001BCE00 File Offset: 0x001BB000
			[DataMember]
			public string BindingName { get; private set; }

			// Token: 0x17001B32 RID: 6962
			// (get) Token: 0x060076A8 RID: 30376 RVA: 0x001BCE09 File Offset: 0x001BB009
			// (set) Token: 0x060076A9 RID: 30377 RVA: 0x001BCE11 File Offset: 0x001BB011
			[DataMember]
			public string ContractName { get; private set; }

			// Token: 0x17001B33 RID: 6963
			// (get) Token: 0x060076AA RID: 30378 RVA: 0x001BCE1A File Offset: 0x001BB01A
			// (set) Token: 0x060076AB RID: 30379 RVA: 0x001BCE22 File Offset: 0x001BB022
			[DataMember]
			public bool IsSystemEndpoint { get; private set; }

			// Token: 0x17001B34 RID: 6964
			// (get) Token: 0x060076AC RID: 30380 RVA: 0x001BCE2B File Offset: 0x001BB02B
			// (set) Token: 0x060076AD RID: 30381 RVA: 0x001BCE33 File Offset: 0x001BB033
			[DataMember]
			public string[] MessageInspectors { get; private set; }

			// Token: 0x17001B35 RID: 6965
			// (get) Token: 0x060076AE RID: 30382 RVA: 0x001BCE3C File Offset: 0x001BB03C
			// (set) Token: 0x060076AF RID: 30383 RVA: 0x001BCE44 File Offset: 0x001BB044
			[DataMember]
			public ServiceHealthModel.ServiceThrottleModel ServiceThrottle { get; private set; }

			// Token: 0x17001B36 RID: 6966
			// (get) Token: 0x060076B0 RID: 30384 RVA: 0x001BCE4D File Offset: 0x001BB04D
			// (set) Token: 0x060076B1 RID: 30385 RVA: 0x001BCE55 File Offset: 0x001BB055
			[DataMember]
			public ServiceHealthModel.CommunicationTimeoutsModel CommunicationTimeouts { get; private set; }

			// Token: 0x060076B2 RID: 30386 RVA: 0x001BCE60 File Offset: 0x001BB060
			private static string[] GetMessageInspectors(EndpointDispatcher endpointDispatcher)
			{
				SynchronizedCollection<IDispatchMessageInspector> synchronizedCollection;
				if (endpointDispatcher == null)
				{
					synchronizedCollection = null;
				}
				else
				{
					DispatchRuntime dispatchRuntime = endpointDispatcher.DispatchRuntime;
					synchronizedCollection = ((dispatchRuntime != null) ? dispatchRuntime.MessageInspectors : null);
				}
				SynchronizedCollection<IDispatchMessageInspector> synchronizedCollection2 = synchronizedCollection;
				List<string> list = new List<string>((synchronizedCollection2 != null) ? synchronizedCollection2.Count : 0);
				if (synchronizedCollection2 != null && synchronizedCollection2.Count > 0)
				{
					foreach (IDispatchMessageInspector dispatchMessageInspector in synchronizedCollection2)
					{
						list.Add(dispatchMessageInspector.GetType().FullName);
					}
				}
				return list.ToArray();
			}
		}

		// Token: 0x02000C19 RID: 3097
		[DataContract]
		public class CommunicationTimeoutsModel
		{
			// Token: 0x060076B3 RID: 30387 RVA: 0x001BCEF0 File Offset: 0x001BB0F0
			public CommunicationTimeoutsModel()
			{
			}

			// Token: 0x060076B4 RID: 30388 RVA: 0x001BCEF8 File Offset: 0x001BB0F8
			public CommunicationTimeoutsModel(IDefaultCommunicationTimeouts timeouts)
			{
				this.HasTimeouts = (timeouts != null);
				if (timeouts == null)
				{
					return;
				}
				this.CloseTimeout = timeouts.CloseTimeout;
				this.OpenTimeout = timeouts.OpenTimeout;
				this.ReceiveTimeout = timeouts.ReceiveTimeout;
				this.SendTimeout = timeouts.SendTimeout;
			}

			// Token: 0x17001B37 RID: 6967
			// (get) Token: 0x060076B5 RID: 30389 RVA: 0x001BCF49 File Offset: 0x001BB149
			// (set) Token: 0x060076B6 RID: 30390 RVA: 0x001BCF51 File Offset: 0x001BB151
			[DataMember]
			public bool HasTimeouts { get; private set; }

			// Token: 0x17001B38 RID: 6968
			// (get) Token: 0x060076B7 RID: 30391 RVA: 0x001BCF5A File Offset: 0x001BB15A
			// (set) Token: 0x060076B8 RID: 30392 RVA: 0x001BCF62 File Offset: 0x001BB162
			[DataMember]
			public TimeSpan CloseTimeout { get; private set; }

			// Token: 0x17001B39 RID: 6969
			// (get) Token: 0x060076B9 RID: 30393 RVA: 0x001BCF6B File Offset: 0x001BB16B
			// (set) Token: 0x060076BA RID: 30394 RVA: 0x001BCF73 File Offset: 0x001BB173
			[DataMember]
			public TimeSpan OpenTimeout { get; private set; }

			// Token: 0x17001B3A RID: 6970
			// (get) Token: 0x060076BB RID: 30395 RVA: 0x001BCF7C File Offset: 0x001BB17C
			// (set) Token: 0x060076BC RID: 30396 RVA: 0x001BCF84 File Offset: 0x001BB184
			[DataMember]
			public TimeSpan ReceiveTimeout { get; private set; }

			// Token: 0x17001B3B RID: 6971
			// (get) Token: 0x060076BD RID: 30397 RVA: 0x001BCF8D File Offset: 0x001BB18D
			// (set) Token: 0x060076BE RID: 30398 RVA: 0x001BCF95 File Offset: 0x001BB195
			[DataMember]
			public TimeSpan SendTimeout { get; private set; }
		}
	}
}
