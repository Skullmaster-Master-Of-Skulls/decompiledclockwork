using System;
using System.ComponentModel;
using System.Configuration;
using System.Web.Util;

namespace System.Web.Configuration
{
	// Token: 0x0200072F RID: 1839
	public sealed class ProcessModelSection : ConfigurationSection
	{
		// Token: 0x06005872 RID: 22642 RVA: 0x00135B9C File Offset: 0x00133D9C
		static ProcessModelSection()
		{
			ProcessModelSection._properties = new ConfigurationPropertyCollection();
			ProcessModelSection._properties.Add(ProcessModelSection._propEnable);
			ProcessModelSection._properties.Add(ProcessModelSection._propTimeout);
			ProcessModelSection._properties.Add(ProcessModelSection._propIdleTimeout);
			ProcessModelSection._properties.Add(ProcessModelSection._propShutdownTimeout);
			ProcessModelSection._properties.Add(ProcessModelSection._propRequestLimit);
			ProcessModelSection._properties.Add(ProcessModelSection._propRequestQueueLimit);
			ProcessModelSection._properties.Add(ProcessModelSection._propRestartQueueLimit);
			ProcessModelSection._properties.Add(ProcessModelSection._propMemoryLimit);
			ProcessModelSection._properties.Add(ProcessModelSection._propWebGarden);
			ProcessModelSection._properties.Add(ProcessModelSection._propCpuMask);
			ProcessModelSection._properties.Add(ProcessModelSection._propUserName);
			ProcessModelSection._properties.Add(ProcessModelSection._propPassword);
			ProcessModelSection._properties.Add(ProcessModelSection._propLogLevel);
			ProcessModelSection._properties.Add(ProcessModelSection._propClientConnectedCheck);
			ProcessModelSection._properties.Add(ProcessModelSection._propComAuthenticationLevel);
			ProcessModelSection._properties.Add(ProcessModelSection._propComImpersonationLevel);
			ProcessModelSection._properties.Add(ProcessModelSection._propResponseDeadlockInterval);
			ProcessModelSection._properties.Add(ProcessModelSection._propResponseRestartDeadlockInterval);
			ProcessModelSection._properties.Add(ProcessModelSection._propAutoConfig);
			ProcessModelSection._properties.Add(ProcessModelSection._propMaxWorkerThreads);
			ProcessModelSection._properties.Add(ProcessModelSection._propMaxIOThreads);
			ProcessModelSection._properties.Add(ProcessModelSection._propMinWorkerThreads);
			ProcessModelSection._properties.Add(ProcessModelSection._propMinIOThreads);
			ProcessModelSection._properties.Add(ProcessModelSection._propServerErrorMessageFile);
			ProcessModelSection._properties.Add(ProcessModelSection._propPingFrequency);
			ProcessModelSection._properties.Add(ProcessModelSection._propPingTimeout);
			ProcessModelSection._properties.Add(ProcessModelSection._propMaxAppDomains);
			ProcessModelSection.cpuCount = SystemInfo.GetNumProcessCPUs();
		}

		// Token: 0x1700198F RID: 6543
		// (get) Token: 0x06005874 RID: 22644 RVA: 0x001361BD File Offset: 0x001343BD
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ProcessModelSection._properties;
			}
		}

		// Token: 0x17001990 RID: 6544
		// (get) Token: 0x06005875 RID: 22645 RVA: 0x001361C4 File Offset: 0x001343C4
		// (set) Token: 0x06005876 RID: 22646 RVA: 0x001361D6 File Offset: 0x001343D6
		[ConfigurationProperty("enable", DefaultValue = true)]
		public bool Enable
		{
			get
			{
				return (bool)base[ProcessModelSection._propEnable];
			}
			set
			{
				base[ProcessModelSection._propEnable] = value;
			}
		}

		// Token: 0x17001991 RID: 6545
		// (get) Token: 0x06005877 RID: 22647 RVA: 0x001361E9 File Offset: 0x001343E9
		// (set) Token: 0x06005878 RID: 22648 RVA: 0x001361FB File Offset: 0x001343FB
		[ConfigurationProperty("timeout", DefaultValue = "10675199.02:48:05.4775807")]
		[TypeConverter(typeof(InfiniteTimeSpanConverter))]
		public TimeSpan Timeout
		{
			get
			{
				return (TimeSpan)base[ProcessModelSection._propTimeout];
			}
			set
			{
				base[ProcessModelSection._propTimeout] = value;
			}
		}

		// Token: 0x17001992 RID: 6546
		// (get) Token: 0x06005879 RID: 22649 RVA: 0x0013620E File Offset: 0x0013440E
		// (set) Token: 0x0600587A RID: 22650 RVA: 0x00136220 File Offset: 0x00134420
		[ConfigurationProperty("idleTimeout", DefaultValue = "10675199.02:48:05.4775807")]
		[TypeConverter(typeof(InfiniteTimeSpanConverter))]
		public TimeSpan IdleTimeout
		{
			get
			{
				return (TimeSpan)base[ProcessModelSection._propIdleTimeout];
			}
			set
			{
				base[ProcessModelSection._propIdleTimeout] = value;
			}
		}

		// Token: 0x17001993 RID: 6547
		// (get) Token: 0x0600587B RID: 22651 RVA: 0x00136233 File Offset: 0x00134433
		// (set) Token: 0x0600587C RID: 22652 RVA: 0x00136245 File Offset: 0x00134445
		[ConfigurationProperty("shutdownTimeout", DefaultValue = "00:00:05")]
		[TypeConverter(typeof(InfiniteTimeSpanConverter))]
		[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "10675199.02:48:05.4775807")]
		public TimeSpan ShutdownTimeout
		{
			get
			{
				return (TimeSpan)base[ProcessModelSection._propShutdownTimeout];
			}
			set
			{
				base[ProcessModelSection._propShutdownTimeout] = value;
			}
		}

		// Token: 0x17001994 RID: 6548
		// (get) Token: 0x0600587D RID: 22653 RVA: 0x00136258 File Offset: 0x00134458
		// (set) Token: 0x0600587E RID: 22654 RVA: 0x0013626A File Offset: 0x0013446A
		[ConfigurationProperty("requestLimit", DefaultValue = 2147483647)]
		[TypeConverter(typeof(InfiniteIntConverter))]
		[IntegerValidator(MinValue = 0)]
		public int RequestLimit
		{
			get
			{
				return (int)base[ProcessModelSection._propRequestLimit];
			}
			set
			{
				base[ProcessModelSection._propRequestLimit] = value;
			}
		}

		// Token: 0x17001995 RID: 6549
		// (get) Token: 0x0600587F RID: 22655 RVA: 0x0013627D File Offset: 0x0013447D
		// (set) Token: 0x06005880 RID: 22656 RVA: 0x0013628F File Offset: 0x0013448F
		[ConfigurationProperty("requestQueueLimit", DefaultValue = 5000)]
		[TypeConverter(typeof(InfiniteIntConverter))]
		[IntegerValidator(MinValue = 0)]
		public int RequestQueueLimit
		{
			get
			{
				return (int)base[ProcessModelSection._propRequestQueueLimit];
			}
			set
			{
				base[ProcessModelSection._propRequestQueueLimit] = value;
			}
		}

		// Token: 0x17001996 RID: 6550
		// (get) Token: 0x06005881 RID: 22657 RVA: 0x001362A2 File Offset: 0x001344A2
		// (set) Token: 0x06005882 RID: 22658 RVA: 0x001362B4 File Offset: 0x001344B4
		[ConfigurationProperty("restartQueueLimit", DefaultValue = 10)]
		[TypeConverter(typeof(InfiniteIntConverter))]
		[IntegerValidator(MinValue = 0)]
		public int RestartQueueLimit
		{
			get
			{
				return (int)base[ProcessModelSection._propRestartQueueLimit];
			}
			set
			{
				base[ProcessModelSection._propRestartQueueLimit] = value;
			}
		}

		// Token: 0x17001997 RID: 6551
		// (get) Token: 0x06005883 RID: 22659 RVA: 0x001362C7 File Offset: 0x001344C7
		// (set) Token: 0x06005884 RID: 22660 RVA: 0x001362D9 File Offset: 0x001344D9
		[ConfigurationProperty("memoryLimit", DefaultValue = 60)]
		public int MemoryLimit
		{
			get
			{
				return (int)base[ProcessModelSection._propMemoryLimit];
			}
			set
			{
				base[ProcessModelSection._propMemoryLimit] = value;
			}
		}

		// Token: 0x17001998 RID: 6552
		// (get) Token: 0x06005885 RID: 22661 RVA: 0x001362EC File Offset: 0x001344EC
		// (set) Token: 0x06005886 RID: 22662 RVA: 0x001362FE File Offset: 0x001344FE
		[ConfigurationProperty("webGarden", DefaultValue = false)]
		public bool WebGarden
		{
			get
			{
				return (bool)base[ProcessModelSection._propWebGarden];
			}
			set
			{
				base[ProcessModelSection._propWebGarden] = value;
			}
		}

		// Token: 0x17001999 RID: 6553
		// (get) Token: 0x06005887 RID: 22663 RVA: 0x00136311 File Offset: 0x00134511
		// (set) Token: 0x06005888 RID: 22664 RVA: 0x0013632A File Offset: 0x0013452A
		[ConfigurationProperty("cpuMask", DefaultValue = "0xffffffff")]
		public int CpuMask
		{
			get
			{
				return Convert.ToInt32((string)base[ProcessModelSection._propCpuMask], 16);
			}
			set
			{
				base[ProcessModelSection._propCpuMask] = "0x" + Convert.ToString(value, 16);
			}
		}

		// Token: 0x1700199A RID: 6554
		// (get) Token: 0x06005889 RID: 22665 RVA: 0x00136349 File Offset: 0x00134549
		// (set) Token: 0x0600588A RID: 22666 RVA: 0x0013635B File Offset: 0x0013455B
		[ConfigurationProperty("userName", DefaultValue = "machine")]
		public string UserName
		{
			get
			{
				return (string)base[ProcessModelSection._propUserName];
			}
			set
			{
				base[ProcessModelSection._propUserName] = value;
			}
		}

		// Token: 0x1700199B RID: 6555
		// (get) Token: 0x0600588B RID: 22667 RVA: 0x00136369 File Offset: 0x00134569
		// (set) Token: 0x0600588C RID: 22668 RVA: 0x0013637B File Offset: 0x0013457B
		[ConfigurationProperty("password", DefaultValue = "AutoGenerate")]
		public string Password
		{
			get
			{
				return (string)base[ProcessModelSection._propPassword];
			}
			set
			{
				base[ProcessModelSection._propPassword] = value;
			}
		}

		// Token: 0x1700199C RID: 6556
		// (get) Token: 0x0600588D RID: 22669 RVA: 0x00136389 File Offset: 0x00134589
		// (set) Token: 0x0600588E RID: 22670 RVA: 0x0013639B File Offset: 0x0013459B
		[ConfigurationProperty("logLevel", DefaultValue = ProcessModelLogLevel.Errors)]
		public ProcessModelLogLevel LogLevel
		{
			get
			{
				return (ProcessModelLogLevel)base[ProcessModelSection._propLogLevel];
			}
			set
			{
				base[ProcessModelSection._propLogLevel] = value;
			}
		}

		// Token: 0x1700199D RID: 6557
		// (get) Token: 0x0600588F RID: 22671 RVA: 0x001363AE File Offset: 0x001345AE
		// (set) Token: 0x06005890 RID: 22672 RVA: 0x001363C0 File Offset: 0x001345C0
		[ConfigurationProperty("clientConnectedCheck", DefaultValue = "00:00:05")]
		[TypeConverter(typeof(InfiniteTimeSpanConverter))]
		public TimeSpan ClientConnectedCheck
		{
			get
			{
				return (TimeSpan)base[ProcessModelSection._propClientConnectedCheck];
			}
			set
			{
				base[ProcessModelSection._propClientConnectedCheck] = value;
			}
		}

		// Token: 0x1700199E RID: 6558
		// (get) Token: 0x06005891 RID: 22673 RVA: 0x001363D3 File Offset: 0x001345D3
		// (set) Token: 0x06005892 RID: 22674 RVA: 0x001363E5 File Offset: 0x001345E5
		[ConfigurationProperty("comAuthenticationLevel", DefaultValue = ProcessModelComAuthenticationLevel.Connect)]
		public ProcessModelComAuthenticationLevel ComAuthenticationLevel
		{
			get
			{
				return (ProcessModelComAuthenticationLevel)base[ProcessModelSection._propComAuthenticationLevel];
			}
			set
			{
				base[ProcessModelSection._propComAuthenticationLevel] = value;
			}
		}

		// Token: 0x1700199F RID: 6559
		// (get) Token: 0x06005893 RID: 22675 RVA: 0x001363F8 File Offset: 0x001345F8
		// (set) Token: 0x06005894 RID: 22676 RVA: 0x0013640A File Offset: 0x0013460A
		[ConfigurationProperty("comImpersonationLevel", DefaultValue = ProcessModelComImpersonationLevel.Impersonate)]
		public ProcessModelComImpersonationLevel ComImpersonationLevel
		{
			get
			{
				return (ProcessModelComImpersonationLevel)base[ProcessModelSection._propComImpersonationLevel];
			}
			set
			{
				base[ProcessModelSection._propComImpersonationLevel] = value;
			}
		}

		// Token: 0x170019A0 RID: 6560
		// (get) Token: 0x06005895 RID: 22677 RVA: 0x0013641D File Offset: 0x0013461D
		// (set) Token: 0x06005896 RID: 22678 RVA: 0x0013642F File Offset: 0x0013462F
		[ConfigurationProperty("responseDeadlockInterval", DefaultValue = "00:03:00")]
		[TypeConverter(typeof(InfiniteTimeSpanConverter))]
		[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "10675199.02:48:05.4775807")]
		public TimeSpan ResponseDeadlockInterval
		{
			get
			{
				return (TimeSpan)base[ProcessModelSection._propResponseDeadlockInterval];
			}
			set
			{
				base[ProcessModelSection._propResponseDeadlockInterval] = value;
			}
		}

		// Token: 0x170019A1 RID: 6561
		// (get) Token: 0x06005897 RID: 22679 RVA: 0x00136442 File Offset: 0x00134642
		// (set) Token: 0x06005898 RID: 22680 RVA: 0x00136454 File Offset: 0x00134654
		[ConfigurationProperty("responseRestartDeadlockInterval", DefaultValue = "00:03:00")]
		[TypeConverter(typeof(InfiniteTimeSpanConverter))]
		public TimeSpan ResponseRestartDeadlockInterval
		{
			get
			{
				return (TimeSpan)base[ProcessModelSection._propResponseRestartDeadlockInterval];
			}
			set
			{
				base[ProcessModelSection._propResponseRestartDeadlockInterval] = value;
			}
		}

		// Token: 0x170019A2 RID: 6562
		// (get) Token: 0x06005899 RID: 22681 RVA: 0x00136467 File Offset: 0x00134667
		// (set) Token: 0x0600589A RID: 22682 RVA: 0x00136479 File Offset: 0x00134679
		[ConfigurationProperty("autoConfig", DefaultValue = false)]
		public bool AutoConfig
		{
			get
			{
				return (bool)base[ProcessModelSection._propAutoConfig];
			}
			set
			{
				base[ProcessModelSection._propAutoConfig] = value;
			}
		}

		// Token: 0x170019A3 RID: 6563
		// (get) Token: 0x0600589B RID: 22683 RVA: 0x0013648C File Offset: 0x0013468C
		// (set) Token: 0x0600589C RID: 22684 RVA: 0x0013649E File Offset: 0x0013469E
		[ConfigurationProperty("maxWorkerThreads", DefaultValue = 20)]
		[IntegerValidator(MinValue = 1, MaxValue = 2147483646)]
		public int MaxWorkerThreads
		{
			get
			{
				return (int)base[ProcessModelSection._propMaxWorkerThreads];
			}
			set
			{
				base[ProcessModelSection._propMaxWorkerThreads] = value;
			}
		}

		// Token: 0x170019A4 RID: 6564
		// (get) Token: 0x0600589D RID: 22685 RVA: 0x001364B1 File Offset: 0x001346B1
		// (set) Token: 0x0600589E RID: 22686 RVA: 0x001364C3 File Offset: 0x001346C3
		[ConfigurationProperty("maxIoThreads", DefaultValue = 20)]
		[IntegerValidator(MinValue = 1, MaxValue = 2147483646)]
		public int MaxIOThreads
		{
			get
			{
				return (int)base[ProcessModelSection._propMaxIOThreads];
			}
			set
			{
				base[ProcessModelSection._propMaxIOThreads] = value;
			}
		}

		// Token: 0x170019A5 RID: 6565
		// (get) Token: 0x0600589F RID: 22687 RVA: 0x001364D6 File Offset: 0x001346D6
		// (set) Token: 0x060058A0 RID: 22688 RVA: 0x001364E8 File Offset: 0x001346E8
		[ConfigurationProperty("minWorkerThreads", DefaultValue = 1)]
		[IntegerValidator(MinValue = 1, MaxValue = 2147483646)]
		public int MinWorkerThreads
		{
			get
			{
				return (int)base[ProcessModelSection._propMinWorkerThreads];
			}
			set
			{
				base[ProcessModelSection._propMinWorkerThreads] = value;
			}
		}

		// Token: 0x170019A6 RID: 6566
		// (get) Token: 0x060058A1 RID: 22689 RVA: 0x001364FB File Offset: 0x001346FB
		// (set) Token: 0x060058A2 RID: 22690 RVA: 0x0013650D File Offset: 0x0013470D
		[ConfigurationProperty("minIoThreads", DefaultValue = 1)]
		[IntegerValidator(MinValue = 1, MaxValue = 2147483646)]
		public int MinIOThreads
		{
			get
			{
				return (int)base[ProcessModelSection._propMinIOThreads];
			}
			set
			{
				base[ProcessModelSection._propMinIOThreads] = value;
			}
		}

		// Token: 0x170019A7 RID: 6567
		// (get) Token: 0x060058A3 RID: 22691 RVA: 0x00136520 File Offset: 0x00134720
		// (set) Token: 0x060058A4 RID: 22692 RVA: 0x00136532 File Offset: 0x00134732
		[ConfigurationProperty("serverErrorMessageFile", DefaultValue = "")]
		public string ServerErrorMessageFile
		{
			get
			{
				return (string)base[ProcessModelSection._propServerErrorMessageFile];
			}
			set
			{
				base[ProcessModelSection._propServerErrorMessageFile] = value;
			}
		}

		// Token: 0x170019A8 RID: 6568
		// (get) Token: 0x060058A5 RID: 22693 RVA: 0x00136540 File Offset: 0x00134740
		// (set) Token: 0x060058A6 RID: 22694 RVA: 0x00136552 File Offset: 0x00134752
		[ConfigurationProperty("pingFrequency", DefaultValue = "10675199.02:48:05.4775807")]
		[TypeConverter(typeof(InfiniteTimeSpanConverter))]
		public TimeSpan PingFrequency
		{
			get
			{
				return (TimeSpan)base[ProcessModelSection._propPingFrequency];
			}
			set
			{
				base[ProcessModelSection._propPingFrequency] = value;
			}
		}

		// Token: 0x170019A9 RID: 6569
		// (get) Token: 0x060058A7 RID: 22695 RVA: 0x00136565 File Offset: 0x00134765
		// (set) Token: 0x060058A8 RID: 22696 RVA: 0x00136577 File Offset: 0x00134777
		[ConfigurationProperty("pingTimeout", DefaultValue = "10675199.02:48:05.4775807")]
		[TypeConverter(typeof(InfiniteTimeSpanConverter))]
		public TimeSpan PingTimeout
		{
			get
			{
				return (TimeSpan)base[ProcessModelSection._propPingTimeout];
			}
			set
			{
				base[ProcessModelSection._propPingTimeout] = value;
			}
		}

		// Token: 0x170019AA RID: 6570
		// (get) Token: 0x060058A9 RID: 22697 RVA: 0x0013658A File Offset: 0x0013478A
		// (set) Token: 0x060058AA RID: 22698 RVA: 0x0013659C File Offset: 0x0013479C
		[ConfigurationProperty("maxAppDomains", DefaultValue = 2000)]
		[IntegerValidator(MinValue = 1, MaxValue = 2147483646)]
		public int MaxAppDomains
		{
			get
			{
				return (int)base[ProcessModelSection._propMaxAppDomains];
			}
			set
			{
				base[ProcessModelSection._propMaxAppDomains] = value;
			}
		}

		// Token: 0x170019AB RID: 6571
		// (get) Token: 0x060058AB RID: 22699 RVA: 0x001365AF File Offset: 0x001347AF
		internal int CpuCount
		{
			get
			{
				return ProcessModelSection.cpuCount;
			}
		}

		// Token: 0x170019AC RID: 6572
		// (get) Token: 0x060058AC RID: 22700 RVA: 0x001365B6 File Offset: 0x001347B6
		internal int DefaultMaxWorkerThreadsForAutoConfig
		{
			get
			{
				return 100 * ProcessModelSection.cpuCount;
			}
		}

		// Token: 0x170019AD RID: 6573
		// (get) Token: 0x060058AD RID: 22701 RVA: 0x001365B6 File Offset: 0x001347B6
		internal int DefaultMaxIoThreadsForAutoConfig
		{
			get
			{
				return 100 * ProcessModelSection.cpuCount;
			}
		}

		// Token: 0x170019AE RID: 6574
		// (get) Token: 0x060058AE RID: 22702 RVA: 0x001365C0 File Offset: 0x001347C0
		internal int MaxWorkerThreadsTimesCpuCount
		{
			get
			{
				return this.MaxWorkerThreads * ProcessModelSection.cpuCount;
			}
		}

		// Token: 0x170019AF RID: 6575
		// (get) Token: 0x060058AF RID: 22703 RVA: 0x001365CE File Offset: 0x001347CE
		internal int MaxIoThreadsTimesCpuCount
		{
			get
			{
				return this.MaxIOThreads * ProcessModelSection.cpuCount;
			}
		}

		// Token: 0x170019B0 RID: 6576
		// (get) Token: 0x060058B0 RID: 22704 RVA: 0x001365DC File Offset: 0x001347DC
		internal int MinWorkerThreadsTimesCpuCount
		{
			get
			{
				return this.MinWorkerThreads * ProcessModelSection.cpuCount;
			}
		}

		// Token: 0x170019B1 RID: 6577
		// (get) Token: 0x060058B1 RID: 22705 RVA: 0x001365EA File Offset: 0x001347EA
		internal int MinIoThreadsTimesCpuCount
		{
			get
			{
				return this.MinIOThreads * ProcessModelSection.cpuCount;
			}
		}

		// Token: 0x170019B2 RID: 6578
		// (get) Token: 0x060058B2 RID: 22706 RVA: 0x001365F8 File Offset: 0x001347F8
		protected override ConfigurationElementProperty ElementProperty
		{
			get
			{
				return ProcessModelSection.s_elemProperty;
			}
		}

		// Token: 0x060058B3 RID: 22707 RVA: 0x00136600 File Offset: 0x00134800
		private static void Validate(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			ProcessModelSection processModelSection = (ProcessModelSection)value;
			int num = -1;
			try
			{
				num = processModelSection.CpuMask;
			}
			catch
			{
				throw new ConfigurationErrorsException(SR.GetString("Invalid_non_zero_hexadecimal_attribute", new object[]
				{
					"cpuMask"
				}), processModelSection.ElementInformation.Properties["cpuMask"].Source, processModelSection.ElementInformation.Properties["cpuMask"].LineNumber);
			}
			if (num == 0)
			{
				throw new ConfigurationErrorsException(SR.GetString("Invalid_non_zero_hexadecimal_attribute", new object[]
				{
					"cpuMask"
				}), processModelSection.ElementInformation.Properties["cpuMask"].Source, processModelSection.ElementInformation.Properties["cpuMask"].LineNumber);
			}
		}

		// Token: 0x04002F0B RID: 12043
		private const int DefaultMaxThreadsPerCPU = 100;

		// Token: 0x04002F0C RID: 12044
		private static readonly ConfigurationElementProperty s_elemProperty = new ConfigurationElementProperty(new CallbackValidator(typeof(ProcessModelSection), new ValidatorCallback(ProcessModelSection.Validate)));

		// Token: 0x04002F0D RID: 12045
		internal static TimeSpan DefaultClientConnectedCheck = new TimeSpan(0, 0, 5);

		// Token: 0x04002F0E RID: 12046
		private static ConfigurationPropertyCollection _properties;

		// Token: 0x04002F0F RID: 12047
		private static readonly ConfigurationProperty _propEnable = new ConfigurationProperty("enable", typeof(bool), true, ConfigurationPropertyOptions.None);

		// Token: 0x04002F10 RID: 12048
		private static readonly ConfigurationProperty _propTimeout = new ConfigurationProperty("timeout", typeof(TimeSpan), TimeSpan.MaxValue, StdValidatorsAndConverters.InfiniteTimeSpanConverter, null, ConfigurationPropertyOptions.None);

		// Token: 0x04002F11 RID: 12049
		private static readonly ConfigurationProperty _propIdleTimeout = new ConfigurationProperty("idleTimeout", typeof(TimeSpan), TimeSpan.MaxValue, StdValidatorsAndConverters.InfiniteTimeSpanConverter, null, ConfigurationPropertyOptions.None);

		// Token: 0x04002F12 RID: 12050
		private static readonly ConfigurationProperty _propShutdownTimeout = new ConfigurationProperty("shutdownTimeout", typeof(TimeSpan), TimeSpan.FromSeconds(5.0), StdValidatorsAndConverters.InfiniteTimeSpanConverter, StdValidatorsAndConverters.PositiveTimeSpanValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002F13 RID: 12051
		private static readonly ConfigurationProperty _propRequestLimit = new ConfigurationProperty("requestLimit", typeof(int), int.MaxValue, new InfiniteIntConverter(), StdValidatorsAndConverters.PositiveIntegerValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002F14 RID: 12052
		private static readonly ConfigurationProperty _propRequestQueueLimit = new ConfigurationProperty("requestQueueLimit", typeof(int), 5000, new InfiniteIntConverter(), StdValidatorsAndConverters.PositiveIntegerValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002F15 RID: 12053
		private static readonly ConfigurationProperty _propRestartQueueLimit = new ConfigurationProperty("restartQueueLimit", typeof(int), 10, new InfiniteIntConverter(), StdValidatorsAndConverters.PositiveIntegerValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002F16 RID: 12054
		private static readonly ConfigurationProperty _propMemoryLimit = new ConfigurationProperty("memoryLimit", typeof(int), 60, ConfigurationPropertyOptions.None);

		// Token: 0x04002F17 RID: 12055
		private static readonly ConfigurationProperty _propWebGarden = new ConfigurationProperty("webGarden", typeof(bool), false, ConfigurationPropertyOptions.None);

		// Token: 0x04002F18 RID: 12056
		private static readonly ConfigurationProperty _propCpuMask = new ConfigurationProperty("cpuMask", typeof(string), "0xffffffff", ConfigurationPropertyOptions.None);

		// Token: 0x04002F19 RID: 12057
		private static readonly ConfigurationProperty _propUserName = new ConfigurationProperty("userName", typeof(string), "machine", ConfigurationPropertyOptions.None);

		// Token: 0x04002F1A RID: 12058
		private static readonly ConfigurationProperty _propPassword = new ConfigurationProperty("password", typeof(string), "AutoGenerate", ConfigurationPropertyOptions.None);

		// Token: 0x04002F1B RID: 12059
		private static readonly ConfigurationProperty _propLogLevel = new ConfigurationProperty("logLevel", typeof(ProcessModelLogLevel), ProcessModelLogLevel.Errors, ConfigurationPropertyOptions.None);

		// Token: 0x04002F1C RID: 12060
		private static readonly ConfigurationProperty _propClientConnectedCheck = new ConfigurationProperty("clientConnectedCheck", typeof(TimeSpan), ProcessModelSection.DefaultClientConnectedCheck, StdValidatorsAndConverters.InfiniteTimeSpanConverter, null, ConfigurationPropertyOptions.None);

		// Token: 0x04002F1D RID: 12061
		private static readonly ConfigurationProperty _propComAuthenticationLevel = new ConfigurationProperty("comAuthenticationLevel", typeof(ProcessModelComAuthenticationLevel), ProcessModelComAuthenticationLevel.Connect, ConfigurationPropertyOptions.None);

		// Token: 0x04002F1E RID: 12062
		private static readonly ConfigurationProperty _propComImpersonationLevel = new ConfigurationProperty("comImpersonationLevel", typeof(ProcessModelComImpersonationLevel), ProcessModelComImpersonationLevel.Impersonate, ConfigurationPropertyOptions.None);

		// Token: 0x04002F1F RID: 12063
		private static readonly ConfigurationProperty _propResponseDeadlockInterval = new ConfigurationProperty("responseDeadlockInterval", typeof(TimeSpan), TimeSpan.FromMinutes(3.0), StdValidatorsAndConverters.InfiniteTimeSpanConverter, StdValidatorsAndConverters.PositiveTimeSpanValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002F20 RID: 12064
		private static readonly ConfigurationProperty _propResponseRestartDeadlockInterval = new ConfigurationProperty("responseRestartDeadlockInterval", typeof(TimeSpan), TimeSpan.FromMinutes(3.0), StdValidatorsAndConverters.InfiniteTimeSpanConverter, null, ConfigurationPropertyOptions.None);

		// Token: 0x04002F21 RID: 12065
		private static readonly ConfigurationProperty _propAutoConfig = new ConfigurationProperty("autoConfig", typeof(bool), false, ConfigurationPropertyOptions.None);

		// Token: 0x04002F22 RID: 12066
		private static readonly ConfigurationProperty _propMaxWorkerThreads = new ConfigurationProperty("maxWorkerThreads", typeof(int), 100, null, new IntegerValidator(1, 2147483646), ConfigurationPropertyOptions.None);

		// Token: 0x04002F23 RID: 12067
		private static readonly ConfigurationProperty _propMaxIOThreads = new ConfigurationProperty("maxIoThreads", typeof(int), 100, null, new IntegerValidator(1, 2147483646), ConfigurationPropertyOptions.None);

		// Token: 0x04002F24 RID: 12068
		private static readonly ConfigurationProperty _propMinWorkerThreads = new ConfigurationProperty("minWorkerThreads", typeof(int), 1, null, new IntegerValidator(1, 2147483646), ConfigurationPropertyOptions.None);

		// Token: 0x04002F25 RID: 12069
		private static readonly ConfigurationProperty _propMinIOThreads = new ConfigurationProperty("minIoThreads", typeof(int), 1, null, new IntegerValidator(1, 2147483646), ConfigurationPropertyOptions.None);

		// Token: 0x04002F26 RID: 12070
		private static readonly ConfigurationProperty _propServerErrorMessageFile = new ConfigurationProperty("serverErrorMessageFile", typeof(string), string.Empty, ConfigurationPropertyOptions.None);

		// Token: 0x04002F27 RID: 12071
		private static readonly ConfigurationProperty _propPingFrequency = new ConfigurationProperty("pingFrequency", typeof(TimeSpan), TimeSpan.MaxValue, StdValidatorsAndConverters.InfiniteTimeSpanConverter, null, ConfigurationPropertyOptions.None);

		// Token: 0x04002F28 RID: 12072
		private static readonly ConfigurationProperty _propPingTimeout = new ConfigurationProperty("pingTimeout", typeof(TimeSpan), TimeSpan.MaxValue, StdValidatorsAndConverters.InfiniteTimeSpanConverter, null, ConfigurationPropertyOptions.None);

		// Token: 0x04002F29 RID: 12073
		private static readonly ConfigurationProperty _propMaxAppDomains = new ConfigurationProperty("maxAppDomains", typeof(int), 2000, null, new IntegerValidator(1, 2147483646), ConfigurationPropertyOptions.None);

		// Token: 0x04002F2A RID: 12074
		private static int cpuCount;

		// Token: 0x04002F2B RID: 12075
		internal const string sectionName = "system.web/processModel";
	}
}
