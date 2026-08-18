using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Transactions;

namespace System.ServiceModel
{
	// Token: 0x020000DF RID: 223
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class ServiceBehaviorAttribute : Attribute, IServiceBehavior
	{
		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000431 RID: 1073 RVA: 0x00015D7F File Offset: 0x00013F7F
		// (set) Token: 0x06000432 RID: 1074 RVA: 0x00015D87 File Offset: 0x00013F87
		[DefaultValue(null)]
		public string Name
		{
			get
			{
				return this.serviceName;
			}
			set
			{
				this.serviceName = value;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000433 RID: 1075 RVA: 0x00015D90 File Offset: 0x00013F90
		// (set) Token: 0x06000434 RID: 1076 RVA: 0x00015D98 File Offset: 0x00013F98
		[DefaultValue(null)]
		public string Namespace
		{
			get
			{
				return this.serviceNamespace;
			}
			set
			{
				this.serviceNamespace = value;
			}
		}

		// Token: 0x17000115 RID: 277
		// (set) Token: 0x06000435 RID: 1077 RVA: 0x00015DA1 File Offset: 0x00013FA1
		internal IInstanceProvider InstanceProvider
		{
			set
			{
				this.instanceProvider = value;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000436 RID: 1078 RVA: 0x00015DAA File Offset: 0x00013FAA
		// (set) Token: 0x06000437 RID: 1079 RVA: 0x00015DB2 File Offset: 0x00013FB2
		[DefaultValue(AddressFilterMode.Exact)]
		public AddressFilterMode AddressFilterMode
		{
			get
			{
				return this.addressFilterMode;
			}
			set
			{
				if (!AddressFilterModeHelper.IsDefined(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value"));
				}
				this.addressFilterMode = value;
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000438 RID: 1080 RVA: 0x00015DD8 File Offset: 0x00013FD8
		// (set) Token: 0x06000439 RID: 1081 RVA: 0x00015DE0 File Offset: 0x00013FE0
		[DefaultValue(true)]
		public bool AutomaticSessionShutdown
		{
			get
			{
				return this.automaticSessionShutdown;
			}
			set
			{
				this.automaticSessionShutdown = value;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x0600043A RID: 1082 RVA: 0x00015DE9 File Offset: 0x00013FE9
		// (set) Token: 0x0600043B RID: 1083 RVA: 0x00015DF4 File Offset: 0x00013FF4
		[DefaultValue(null)]
		public string ConfigurationName
		{
			get
			{
				return this.configurationName;
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				if (value == string.Empty)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", SR.GetString("SFxConfigurationNameCannotBeEmpty")));
				}
				this.configurationName = value;
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x0600043C RID: 1084 RVA: 0x00015E47 File Offset: 0x00014047
		// (set) Token: 0x0600043D RID: 1085 RVA: 0x00015E4F File Offset: 0x0001404F
		public IsolationLevel TransactionIsolationLevel
		{
			get
			{
				return this.transactionIsolationLevel;
			}
			set
			{
				if (value > IsolationLevel.Unspecified)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value"));
				}
				this.transactionIsolationLevel = value;
				this.isolationLevelSet = true;
			}
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x00015E78 File Offset: 0x00014078
		public bool ShouldSerializeTransactionIsolationLevel()
		{
			return this.IsolationLevelSet;
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x0600043F RID: 1087 RVA: 0x00015E80 File Offset: 0x00014080
		internal bool IsolationLevelSet
		{
			get
			{
				return this.isolationLevelSet;
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000440 RID: 1088 RVA: 0x00015E88 File Offset: 0x00014088
		// (set) Token: 0x06000441 RID: 1089 RVA: 0x00015E90 File Offset: 0x00014090
		[DefaultValue(false)]
		public bool IncludeExceptionDetailInFaults
		{
			get
			{
				return this.includeExceptionDetailInFaults;
			}
			set
			{
				this.includeExceptionDetailInFaults = value;
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000442 RID: 1090 RVA: 0x00015E99 File Offset: 0x00014099
		// (set) Token: 0x06000443 RID: 1091 RVA: 0x00015EA1 File Offset: 0x000140A1
		[DefaultValue(ConcurrencyMode.Single)]
		public ConcurrencyMode ConcurrencyMode
		{
			get
			{
				return this.concurrencyMode;
			}
			set
			{
				if (!ConcurrencyModeHelper.IsDefined(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value"));
				}
				this.concurrencyMode = value;
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000444 RID: 1092 RVA: 0x00015EC7 File Offset: 0x000140C7
		// (set) Token: 0x06000445 RID: 1093 RVA: 0x00015ECF File Offset: 0x000140CF
		[DefaultValue(false)]
		public bool EnsureOrderedDispatch
		{
			get
			{
				return this.ensureOrderedDispatch;
			}
			set
			{
				this.ensureOrderedDispatch = value;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000446 RID: 1094 RVA: 0x00015ED8 File Offset: 0x000140D8
		// (set) Token: 0x06000447 RID: 1095 RVA: 0x00015EE0 File Offset: 0x000140E0
		[DefaultValue(InstanceContextMode.PerSession)]
		public InstanceContextMode InstanceContextMode
		{
			get
			{
				return this.instanceMode;
			}
			set
			{
				if (!InstanceContextModeHelper.IsDefined(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value"));
				}
				this.instanceMode = value;
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000448 RID: 1096 RVA: 0x00015F06 File Offset: 0x00014106
		// (set) Token: 0x06000449 RID: 1097 RVA: 0x00015F0E File Offset: 0x0001410E
		public bool ReleaseServiceInstanceOnTransactionComplete
		{
			get
			{
				return this.releaseServiceInstanceOnTransactionComplete;
			}
			set
			{
				this.releaseServiceInstanceOnTransactionComplete = value;
				this.releaseServiceInstanceOnTransactionCompleteSet = true;
			}
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x00015F1E File Offset: 0x0001411E
		public bool ShouldSerializeConfigurationName()
		{
			return this.configurationName != null;
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x00015F29 File Offset: 0x00014129
		public bool ShouldSerializeReleaseServiceInstanceOnTransactionComplete()
		{
			return this.ReleaseServiceInstanceOnTransactionCompleteSet;
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x0600044C RID: 1100 RVA: 0x00015F31 File Offset: 0x00014131
		internal bool ReleaseServiceInstanceOnTransactionCompleteSet
		{
			get
			{
				return this.releaseServiceInstanceOnTransactionCompleteSet;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x0600044D RID: 1101 RVA: 0x00015F39 File Offset: 0x00014139
		// (set) Token: 0x0600044E RID: 1102 RVA: 0x00015F41 File Offset: 0x00014141
		public bool TransactionAutoCompleteOnSessionClose
		{
			get
			{
				return this.transactionAutoCompleteOnSessionClose;
			}
			set
			{
				this.transactionAutoCompleteOnSessionClose = value;
				this.transactionAutoCompleteOnSessionCloseSet = true;
			}
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x00015F51 File Offset: 0x00014151
		public bool ShouldSerializeTransactionAutoCompleteOnSessionClose()
		{
			return this.TransactionAutoCompleteOnSessionCloseSet;
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000450 RID: 1104 RVA: 0x00015F59 File Offset: 0x00014159
		internal bool TransactionAutoCompleteOnSessionCloseSet
		{
			get
			{
				return this.transactionAutoCompleteOnSessionCloseSet;
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000451 RID: 1105 RVA: 0x00015F61 File Offset: 0x00014161
		// (set) Token: 0x06000452 RID: 1106 RVA: 0x00015F6C File Offset: 0x0001416C
		public string TransactionTimeout
		{
			get
			{
				return this.transactionTimeoutString;
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("value"));
				}
				try
				{
					TimeSpan t = TimeSpan.Parse(value, CultureInfo.InvariantCulture);
					if (t < TimeSpan.Zero)
					{
						string @string = SR.GetString("SFxTimeoutOutOfRange0");
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value, @string));
					}
					this.transactionTimeout = t;
					this.transactionTimeoutString = value;
					this.transactionTimeoutSet = true;
				}
				catch (FormatException innerException)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("SFxTimeoutInvalidStringFormat"), "value", innerException));
				}
				catch (OverflowException)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value"));
				}
			}
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x00016038 File Offset: 0x00014238
		public bool ShouldSerializeTransactionTimeout()
		{
			return this.TransactionTimeoutSet;
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000454 RID: 1108 RVA: 0x00016040 File Offset: 0x00014240
		internal TimeSpan TransactionTimeoutTimespan
		{
			get
			{
				return this.transactionTimeout;
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000455 RID: 1109 RVA: 0x00016048 File Offset: 0x00014248
		internal bool TransactionTimeoutSet
		{
			get
			{
				return this.transactionTimeoutSet;
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000456 RID: 1110 RVA: 0x00016050 File Offset: 0x00014250
		// (set) Token: 0x06000457 RID: 1111 RVA: 0x00016058 File Offset: 0x00014258
		[DefaultValue(true)]
		public bool ValidateMustUnderstand
		{
			get
			{
				return this.validateMustUnderstand;
			}
			set
			{
				this.validateMustUnderstand = value;
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000458 RID: 1112 RVA: 0x00016061 File Offset: 0x00014261
		// (set) Token: 0x06000459 RID: 1113 RVA: 0x00016069 File Offset: 0x00014269
		[DefaultValue(false)]
		public bool IgnoreExtensionDataObject
		{
			get
			{
				return this.ignoreExtensionDataObject;
			}
			set
			{
				this.ignoreExtensionDataObject = value;
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x0600045A RID: 1114 RVA: 0x00016072 File Offset: 0x00014272
		// (set) Token: 0x0600045B RID: 1115 RVA: 0x0001607A File Offset: 0x0001427A
		[DefaultValue(2147483647)]
		public int MaxItemsInObjectGraph
		{
			get
			{
				return this.maxItemsInObjectGraph;
			}
			set
			{
				this.maxItemsInObjectGraph = value;
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x0600045C RID: 1116 RVA: 0x00016083 File Offset: 0x00014283
		// (set) Token: 0x0600045D RID: 1117 RVA: 0x0001608B File Offset: 0x0001428B
		[DefaultValue(true)]
		public bool UseSynchronizationContext
		{
			get
			{
				return this.useSynchronizationContext;
			}
			set
			{
				this.useSynchronizationContext = value;
			}
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x00016094 File Offset: 0x00014294
		public object GetWellKnownSingleton()
		{
			return this.wellKnownSingleton;
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x0001609C File Offset: 0x0001429C
		public void SetWellKnownSingleton(object value)
		{
			if (value == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
			}
			this.wellKnownSingleton = value;
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x000160B8 File Offset: 0x000142B8
		internal object GetHiddenSingleton()
		{
			return this.hiddenSingleton;
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x000160C0 File Offset: 0x000142C0
		internal void SetHiddenSingleton(object value)
		{
			if (value == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
			}
			this.hiddenSingleton = value;
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x000160DC File Offset: 0x000142DC
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SetIsolationLevel(ChannelDispatcher channelDispatcher)
		{
			if (channelDispatcher == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("channelDispatcher");
			}
			channelDispatcher.TransactionIsolationLevel = this.transactionIsolationLevel;
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x000160FD File Offset: 0x000142FD
		void IServiceBehavior.Validate(ServiceDescription description, ServiceHostBase serviceHostBase)
		{
			if (this.concurrencyMode != ConcurrencyMode.Single && this.ensureOrderedDispatch)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxNonConcurrentOrEnsureOrderedDispatch", new object[]
				{
					description.Name
				})));
			}
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x00016138 File Offset: 0x00014338
		void IServiceBehavior.AddBindingParameters(ServiceDescription description, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection parameters)
		{
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x0001613C File Offset: 0x0001433C
		void IServiceBehavior.ApplyDispatchBehavior(ServiceDescription description, ServiceHostBase serviceHostBase)
		{
			for (int i = 0; i < serviceHostBase.ChannelDispatchers.Count; i++)
			{
				ChannelDispatcher channelDispatcher = serviceHostBase.ChannelDispatchers[i] as ChannelDispatcher;
				if (channelDispatcher != null)
				{
					channelDispatcher.IncludeExceptionDetailInFaults = this.includeExceptionDetailInFaults;
					if (channelDispatcher.HasApplicationEndpoints())
					{
						channelDispatcher.TransactionTimeout = this.transactionTimeout;
						if (this.isolationLevelSet)
						{
							this.SetIsolationLevel(channelDispatcher);
						}
						foreach (EndpointDispatcher endpointDispatcher in channelDispatcher.Endpoints)
						{
							if (!endpointDispatcher.IsSystemEndpoint)
							{
								DispatchRuntime dispatchRuntime = endpointDispatcher.DispatchRuntime;
								dispatchRuntime.ConcurrencyMode = this.concurrencyMode;
								dispatchRuntime.EnsureOrderedDispatch = this.ensureOrderedDispatch;
								dispatchRuntime.ValidateMustUnderstand = this.validateMustUnderstand;
								dispatchRuntime.AutomaticInputSessionShutdown = this.automaticSessionShutdown;
								dispatchRuntime.TransactionAutoCompleteOnSessionClose = this.transactionAutoCompleteOnSessionClose;
								dispatchRuntime.ReleaseServiceInstanceOnTransactionComplete = this.releaseServiceInstanceOnTransactionComplete;
								if (!this.useSynchronizationContext)
								{
									dispatchRuntime.SynchronizationContext = null;
								}
								if (!endpointDispatcher.AddressFilterSetExplicit)
								{
									EndpointAddress originalAddress = endpointDispatcher.OriginalAddress;
									if (originalAddress == null || this.AddressFilterMode == AddressFilterMode.Any)
									{
										endpointDispatcher.AddressFilter = new MatchAllMessageFilter();
									}
									else if (this.AddressFilterMode == AddressFilterMode.Prefix)
									{
										endpointDispatcher.AddressFilter = new PrefixEndpointAddressMessageFilter(originalAddress);
									}
									else if (this.AddressFilterMode == AddressFilterMode.Exact)
									{
										endpointDispatcher.AddressFilter = new EndpointAddressMessageFilter(originalAddress);
									}
								}
							}
						}
					}
				}
			}
			DataContractSerializerServiceBehavior.ApplySerializationSettings(description, this.ignoreExtensionDataObject, this.maxItemsInObjectGraph);
			this.ApplyInstancing(description, serviceHostBase);
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x000162D8 File Offset: 0x000144D8
		private void ApplyInstancing(ServiceDescription description, ServiceHostBase serviceHostBase)
		{
			Type serviceType = description.ServiceType;
			InstanceContext instanceContext = null;
			for (int i = 0; i < serviceHostBase.ChannelDispatchers.Count; i++)
			{
				ChannelDispatcher channelDispatcher = serviceHostBase.ChannelDispatchers[i] as ChannelDispatcher;
				if (channelDispatcher != null)
				{
					foreach (EndpointDispatcher endpointDispatcher in channelDispatcher.Endpoints)
					{
						if (!endpointDispatcher.IsSystemEndpoint)
						{
							DispatchRuntime dispatchRuntime = endpointDispatcher.DispatchRuntime;
							if (dispatchRuntime.InstanceProvider == null)
							{
								if (this.instanceProvider == null)
								{
									if (serviceType == null && this.wellKnownSingleton == null)
									{
										throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("InstanceSettingsMustHaveTypeOrWellKnownObject0")));
									}
									if (this.instanceMode != InstanceContextMode.Single && this.wellKnownSingleton != null)
									{
										throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxWellKnownNonSingleton0")));
									}
								}
								else
								{
									dispatchRuntime.InstanceProvider = this.instanceProvider;
								}
							}
							dispatchRuntime.Type = serviceType;
							dispatchRuntime.InstanceContextProvider = InstanceContextProviderBase.GetProviderForMode(this.instanceMode, dispatchRuntime);
							if (this.instanceMode == InstanceContextMode.Single && dispatchRuntime.SingletonInstanceContext == null)
							{
								if (instanceContext == null)
								{
									if (this.wellKnownSingleton != null)
									{
										instanceContext = new InstanceContext(serviceHostBase, this.wellKnownSingleton, true, false);
									}
									else if (this.hiddenSingleton != null)
									{
										instanceContext = new InstanceContext(serviceHostBase, this.hiddenSingleton, false, false);
									}
									else
									{
										instanceContext = new InstanceContext(serviceHostBase, false);
									}
									instanceContext.AutoClose = false;
								}
								dispatchRuntime.SingletonInstanceContext = instanceContext;
							}
						}
					}
				}
			}
		}

		// Token: 0x040009DA RID: 2522
		internal static IsolationLevel DefaultIsolationLevel = IsolationLevel.Unspecified;

		// Token: 0x040009DB RID: 2523
		private ConcurrencyMode concurrencyMode;

		// Token: 0x040009DC RID: 2524
		private bool ensureOrderedDispatch;

		// Token: 0x040009DD RID: 2525
		private string configurationName;

		// Token: 0x040009DE RID: 2526
		private bool includeExceptionDetailInFaults;

		// Token: 0x040009DF RID: 2527
		private InstanceContextMode instanceMode;

		// Token: 0x040009E0 RID: 2528
		private bool releaseServiceInstanceOnTransactionComplete = true;

		// Token: 0x040009E1 RID: 2529
		private bool releaseServiceInstanceOnTransactionCompleteSet;

		// Token: 0x040009E2 RID: 2530
		private bool transactionAutoCompleteOnSessionClose;

		// Token: 0x040009E3 RID: 2531
		private bool transactionAutoCompleteOnSessionCloseSet;

		// Token: 0x040009E4 RID: 2532
		private object wellKnownSingleton;

		// Token: 0x040009E5 RID: 2533
		private object hiddenSingleton;

		// Token: 0x040009E6 RID: 2534
		private bool validateMustUnderstand = true;

		// Token: 0x040009E7 RID: 2535
		private bool ignoreExtensionDataObject;

		// Token: 0x040009E8 RID: 2536
		private int maxItemsInObjectGraph = int.MaxValue;

		// Token: 0x040009E9 RID: 2537
		private IsolationLevel transactionIsolationLevel = ServiceBehaviorAttribute.DefaultIsolationLevel;

		// Token: 0x040009EA RID: 2538
		private bool isolationLevelSet;

		// Token: 0x040009EB RID: 2539
		private bool automaticSessionShutdown = true;

		// Token: 0x040009EC RID: 2540
		private IInstanceProvider instanceProvider;

		// Token: 0x040009ED RID: 2541
		private TimeSpan transactionTimeout = TimeSpan.Zero;

		// Token: 0x040009EE RID: 2542
		private string transactionTimeoutString;

		// Token: 0x040009EF RID: 2543
		private bool transactionTimeoutSet;

		// Token: 0x040009F0 RID: 2544
		private bool useSynchronizationContext = true;

		// Token: 0x040009F1 RID: 2545
		private string serviceName;

		// Token: 0x040009F2 RID: 2546
		private string serviceNamespace;

		// Token: 0x040009F3 RID: 2547
		private AddressFilterMode addressFilterMode;
	}
}
