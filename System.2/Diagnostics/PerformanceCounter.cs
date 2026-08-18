using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Security.Permissions;
using System.Threading;

namespace System.Diagnostics
{
	// Token: 0x020004DE RID: 1246
	[InstallerType("System.Diagnostics.PerformanceCounterInstaller,System.Configuration.Install, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[SRDescription("PerformanceCounterDesc")]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, SharedState = true)]
	public sealed class PerformanceCounter : Component, ISupportInitialize
	{
		// Token: 0x17000B74 RID: 2932
		// (get) Token: 0x06002EFC RID: 12028 RVA: 0x000D2F7C File Offset: 0x000D117C
		private object InstanceLockObject
		{
			get
			{
				if (this.m_InstanceLockObject == null)
				{
					object value = new object();
					Interlocked.CompareExchange(ref this.m_InstanceLockObject, value, null);
				}
				return this.m_InstanceLockObject;
			}
		}

		// Token: 0x06002EFD RID: 12029 RVA: 0x000D2FAC File Offset: 0x000D11AC
		public PerformanceCounter()
		{
			this.machineName = ".";
			this.categoryName = string.Empty;
			this.counterName = string.Empty;
			this.instanceName = string.Empty;
			this.isReadOnly = true;
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002EFE RID: 12030 RVA: 0x000D300C File Offset: 0x000D120C
		public PerformanceCounter(string categoryName, string counterName, string instanceName, string machineName)
		{
			this.MachineName = machineName;
			this.CategoryName = categoryName;
			this.CounterName = counterName;
			this.InstanceName = instanceName;
			this.isReadOnly = true;
			this.Initialize();
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002EFF RID: 12031 RVA: 0x000D3064 File Offset: 0x000D1264
		internal PerformanceCounter(string categoryName, string counterName, string instanceName, string machineName, bool skipInit)
		{
			this.MachineName = machineName;
			this.CategoryName = categoryName;
			this.CounterName = counterName;
			this.InstanceName = instanceName;
			this.isReadOnly = true;
			this.initialized = true;
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002F00 RID: 12032 RVA: 0x000D30BA File Offset: 0x000D12BA
		public PerformanceCounter(string categoryName, string counterName, string instanceName) : this(categoryName, counterName, instanceName, true)
		{
		}

		// Token: 0x06002F01 RID: 12033 RVA: 0x000D30C8 File Offset: 0x000D12C8
		public PerformanceCounter(string categoryName, string counterName, string instanceName, bool readOnly)
		{
			if (!readOnly)
			{
				PerformanceCounter.VerifyWriteableCounterAllowed();
			}
			this.MachineName = ".";
			this.CategoryName = categoryName;
			this.CounterName = counterName;
			this.InstanceName = instanceName;
			this.isReadOnly = readOnly;
			this.Initialize();
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002F02 RID: 12034 RVA: 0x000D312A File Offset: 0x000D132A
		public PerformanceCounter(string categoryName, string counterName) : this(categoryName, counterName, true)
		{
		}

		// Token: 0x06002F03 RID: 12035 RVA: 0x000D3135 File Offset: 0x000D1335
		public PerformanceCounter(string categoryName, string counterName, bool readOnly) : this(categoryName, counterName, "", readOnly)
		{
		}

		// Token: 0x17000B75 RID: 2933
		// (get) Token: 0x06002F04 RID: 12036 RVA: 0x000D3145 File Offset: 0x000D1345
		// (set) Token: 0x06002F05 RID: 12037 RVA: 0x000D314D File Offset: 0x000D134D
		[ReadOnly(true)]
		[DefaultValue("")]
		[TypeConverter("System.Diagnostics.Design.CategoryValueConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[SRDescription("PCCategoryName")]
		[SettingsBindable(true)]
		public string CategoryName
		{
			get
			{
				return this.categoryName;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (this.categoryName == null || string.Compare(this.categoryName, value, StringComparison.OrdinalIgnoreCase) != 0)
				{
					this.categoryName = value;
					this.Close();
				}
			}
		}

		// Token: 0x17000B76 RID: 2934
		// (get) Token: 0x06002F06 RID: 12038 RVA: 0x000D3184 File Offset: 0x000D1384
		[ReadOnly(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("PC_CounterHelp")]
		public string CounterHelp
		{
			get
			{
				string category = this.categoryName;
				string machine = this.machineName;
				PerformanceCounterPermission performanceCounterPermission = new PerformanceCounterPermission(PerformanceCounterPermissionAccess.Browse, machine, category);
				performanceCounterPermission.Demand();
				this.Initialize();
				if (this.helpMsg == null)
				{
					this.helpMsg = PerformanceCounterLib.GetCounterHelp(machine, category, this.counterName);
				}
				return this.helpMsg;
			}
		}

		// Token: 0x17000B77 RID: 2935
		// (get) Token: 0x06002F07 RID: 12039 RVA: 0x000D31D5 File Offset: 0x000D13D5
		// (set) Token: 0x06002F08 RID: 12040 RVA: 0x000D31DD File Offset: 0x000D13DD
		[ReadOnly(true)]
		[DefaultValue("")]
		[TypeConverter("System.Diagnostics.Design.CounterNameConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[SRDescription("PCCounterName")]
		[SettingsBindable(true)]
		public string CounterName
		{
			get
			{
				return this.counterName;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (this.counterName == null || string.Compare(this.counterName, value, StringComparison.OrdinalIgnoreCase) != 0)
				{
					this.counterName = value;
					this.Close();
				}
			}
		}

		// Token: 0x17000B78 RID: 2936
		// (get) Token: 0x06002F09 RID: 12041 RVA: 0x000D3214 File Offset: 0x000D1414
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("PC_CounterType")]
		public PerformanceCounterType CounterType
		{
			get
			{
				if (this.counterType == -1)
				{
					string category = this.categoryName;
					string machine = this.machineName;
					PerformanceCounterPermission performanceCounterPermission = new PerformanceCounterPermission(PerformanceCounterPermissionAccess.Browse, machine, category);
					performanceCounterPermission.Demand();
					this.Initialize();
					CategorySample categorySample = PerformanceCounterLib.GetCategorySample(machine, category);
					CounterDefinitionSample counterDefinitionSample = categorySample.GetCounterDefinitionSample(this.counterName);
					this.counterType = counterDefinitionSample.CounterType;
				}
				return (PerformanceCounterType)this.counterType;
			}
		}

		// Token: 0x17000B79 RID: 2937
		// (get) Token: 0x06002F0A RID: 12042 RVA: 0x000D3276 File Offset: 0x000D1476
		// (set) Token: 0x06002F0B RID: 12043 RVA: 0x000D327E File Offset: 0x000D147E
		[DefaultValue(PerformanceCounterInstanceLifetime.Global)]
		[SRDescription("PCInstanceLifetime")]
		public PerformanceCounterInstanceLifetime InstanceLifetime
		{
			get
			{
				return this.instanceLifetime;
			}
			set
			{
				if (value > PerformanceCounterInstanceLifetime.Process || value < PerformanceCounterInstanceLifetime.Global)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				if (this.initialized)
				{
					throw new InvalidOperationException(SR.GetString("CantSetLifetimeAfterInitialized"));
				}
				this.instanceLifetime = value;
			}
		}

		// Token: 0x17000B7A RID: 2938
		// (get) Token: 0x06002F0C RID: 12044 RVA: 0x000D32B2 File Offset: 0x000D14B2
		// (set) Token: 0x06002F0D RID: 12045 RVA: 0x000D32BA File Offset: 0x000D14BA
		[ReadOnly(true)]
		[DefaultValue("")]
		[TypeConverter("System.Diagnostics.Design.InstanceNameConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[SRDescription("PCInstanceName")]
		[SettingsBindable(true)]
		public string InstanceName
		{
			get
			{
				return this.instanceName;
			}
			set
			{
				if (value == null && this.instanceName == null)
				{
					return;
				}
				if ((value == null && this.instanceName != null) || (value != null && this.instanceName == null) || string.Compare(this.instanceName, value, StringComparison.OrdinalIgnoreCase) != 0)
				{
					this.instanceName = value;
					this.Close();
				}
			}
		}

		// Token: 0x17000B7B RID: 2939
		// (get) Token: 0x06002F0E RID: 12046 RVA: 0x000D32FA File Offset: 0x000D14FA
		// (set) Token: 0x06002F0F RID: 12047 RVA: 0x000D3302 File Offset: 0x000D1502
		[Browsable(false)]
		[DefaultValue(true)]
		[MonitoringDescription("PC_ReadOnly")]
		public bool ReadOnly
		{
			get
			{
				return this.isReadOnly;
			}
			set
			{
				if (value != this.isReadOnly)
				{
					if (!value)
					{
						PerformanceCounter.VerifyWriteableCounterAllowed();
					}
					this.isReadOnly = value;
					this.Close();
				}
			}
		}

		// Token: 0x17000B7C RID: 2940
		// (get) Token: 0x06002F10 RID: 12048 RVA: 0x000D3322 File Offset: 0x000D1522
		// (set) Token: 0x06002F11 RID: 12049 RVA: 0x000D332C File Offset: 0x000D152C
		[Browsable(false)]
		[DefaultValue(".")]
		[SRDescription("PCMachineName")]
		[SettingsBindable(true)]
		public string MachineName
		{
			get
			{
				return this.machineName;
			}
			set
			{
				if (!SyntaxCheck.CheckMachineName(value))
				{
					throw new ArgumentException(SR.GetString("InvalidParameter", new object[]
					{
						"machineName",
						value
					}));
				}
				if (this.machineName != value)
				{
					this.machineName = value;
					this.Close();
				}
			}
		}

		// Token: 0x17000B7D RID: 2941
		// (get) Token: 0x06002F12 RID: 12050 RVA: 0x000D3380 File Offset: 0x000D1580
		// (set) Token: 0x06002F13 RID: 12051 RVA: 0x000D33B5 File Offset: 0x000D15B5
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("PC_RawValue")]
		public long RawValue
		{
			get
			{
				if (this.ReadOnly)
				{
					return this.NextSample().RawValue;
				}
				this.Initialize();
				return this.sharedCounter.Value;
			}
			set
			{
				if (this.ReadOnly)
				{
					this.ThrowReadOnly();
				}
				this.Initialize();
				this.sharedCounter.Value = value;
			}
		}

		// Token: 0x06002F14 RID: 12052 RVA: 0x000D33D7 File Offset: 0x000D15D7
		public void BeginInit()
		{
			this.Close();
		}

		// Token: 0x06002F15 RID: 12053 RVA: 0x000D33DF File Offset: 0x000D15DF
		public void Close()
		{
			this.helpMsg = null;
			this.oldSample = CounterSample.Empty;
			this.sharedCounter = null;
			this.initialized = false;
			this.counterType = -1;
		}

		// Token: 0x06002F16 RID: 12054 RVA: 0x000D3408 File Offset: 0x000D1608
		public static void CloseSharedResources()
		{
			PerformanceCounterPermission performanceCounterPermission = new PerformanceCounterPermission(PerformanceCounterPermissionAccess.Browse, ".", "*");
			performanceCounterPermission.Demand();
			PerformanceCounterLib.CloseAllLibraries();
		}

		// Token: 0x06002F17 RID: 12055 RVA: 0x000D3431 File Offset: 0x000D1631
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Close();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06002F18 RID: 12056 RVA: 0x000D3443 File Offset: 0x000D1643
		public long Decrement()
		{
			if (this.ReadOnly)
			{
				this.ThrowReadOnly();
			}
			this.Initialize();
			return this.sharedCounter.Decrement();
		}

		// Token: 0x06002F19 RID: 12057 RVA: 0x000D3464 File Offset: 0x000D1664
		public void EndInit()
		{
			this.Initialize();
		}

		// Token: 0x06002F1A RID: 12058 RVA: 0x000D346C File Offset: 0x000D166C
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public long IncrementBy(long value)
		{
			if (this.isReadOnly)
			{
				this.ThrowReadOnly();
			}
			this.Initialize();
			return this.sharedCounter.IncrementBy(value);
		}

		// Token: 0x06002F1B RID: 12059 RVA: 0x000D348E File Offset: 0x000D168E
		public long Increment()
		{
			if (this.isReadOnly)
			{
				this.ThrowReadOnly();
			}
			this.Initialize();
			return this.sharedCounter.Increment();
		}

		// Token: 0x06002F1C RID: 12060 RVA: 0x000D34AF File Offset: 0x000D16AF
		private void ThrowReadOnly()
		{
			throw new InvalidOperationException(SR.GetString("ReadOnlyCounter"));
		}

		// Token: 0x06002F1D RID: 12061 RVA: 0x000D34C0 File Offset: 0x000D16C0
		private static void VerifyWriteableCounterAllowed()
		{
			if (EnvironmentHelpers.IsAppContainerProcess)
			{
				throw new NotSupportedException(SR.GetString("PCNotSupportedUnderAppContainer"));
			}
		}

		// Token: 0x06002F1E RID: 12062 RVA: 0x000D34D9 File Offset: 0x000D16D9
		private void Initialize()
		{
			if (!this.initialized && !base.DesignMode)
			{
				this.InitializeImpl();
			}
		}

		// Token: 0x06002F1F RID: 12063 RVA: 0x000D34F4 File Offset: 0x000D16F4
		private void InitializeImpl()
		{
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				Monitor.Enter(this.InstanceLockObject, ref flag);
				if (!this.initialized)
				{
					string text = this.categoryName;
					string text2 = this.machineName;
					if (text == string.Empty)
					{
						throw new InvalidOperationException(SR.GetString("CategoryNameMissing"));
					}
					if (this.counterName == string.Empty)
					{
						throw new InvalidOperationException(SR.GetString("CounterNameMissing"));
					}
					if (this.ReadOnly)
					{
						PerformanceCounterPermission performanceCounterPermission = new PerformanceCounterPermission(PerformanceCounterPermissionAccess.Browse, text2, text);
						performanceCounterPermission.Demand();
						if (!PerformanceCounterLib.CounterExists(text2, text, this.counterName))
						{
							throw new InvalidOperationException(SR.GetString("CounterExists", new object[]
							{
								text,
								this.counterName
							}));
						}
						PerformanceCounterCategoryType categoryType = PerformanceCounterLib.GetCategoryType(text2, text);
						if (categoryType == PerformanceCounterCategoryType.MultiInstance)
						{
							if (string.IsNullOrEmpty(this.instanceName))
							{
								throw new InvalidOperationException(SR.GetString("MultiInstanceOnly", new object[]
								{
									text
								}));
							}
						}
						else if (categoryType == PerformanceCounterCategoryType.SingleInstance && !string.IsNullOrEmpty(this.instanceName))
						{
							throw new InvalidOperationException(SR.GetString("SingleInstanceOnly", new object[]
							{
								text
							}));
						}
						if (this.instanceLifetime != PerformanceCounterInstanceLifetime.Global)
						{
							throw new InvalidOperationException(SR.GetString("InstanceLifetimeProcessonReadOnly"));
						}
						this.initialized = true;
					}
					else
					{
						PerformanceCounterPermission performanceCounterPermission2 = new PerformanceCounterPermission(PerformanceCounterPermissionAccess.Write, text2, text);
						performanceCounterPermission2.Demand();
						if (text2 != "." && string.Compare(text2, PerformanceCounterLib.ComputerName, StringComparison.OrdinalIgnoreCase) != 0)
						{
							throw new InvalidOperationException(SR.GetString("RemoteWriting"));
						}
						SharedUtils.CheckNtEnvironment();
						if (!PerformanceCounterLib.IsCustomCategory(text2, text))
						{
							throw new InvalidOperationException(SR.GetString("NotCustomCounter"));
						}
						PerformanceCounterCategoryType categoryType2 = PerformanceCounterLib.GetCategoryType(text2, text);
						if (categoryType2 == PerformanceCounterCategoryType.MultiInstance)
						{
							if (string.IsNullOrEmpty(this.instanceName))
							{
								throw new InvalidOperationException(SR.GetString("MultiInstanceOnly", new object[]
								{
									text
								}));
							}
						}
						else if (categoryType2 == PerformanceCounterCategoryType.SingleInstance && !string.IsNullOrEmpty(this.instanceName))
						{
							throw new InvalidOperationException(SR.GetString("SingleInstanceOnly", new object[]
							{
								text
							}));
						}
						if (string.IsNullOrEmpty(this.instanceName) && this.InstanceLifetime == PerformanceCounterInstanceLifetime.Process)
						{
							throw new InvalidOperationException(SR.GetString("InstanceLifetimeProcessforSingleInstance"));
						}
						this.sharedCounter = new SharedPerformanceCounter(text.ToLower(CultureInfo.InvariantCulture), this.counterName.ToLower(CultureInfo.InvariantCulture), this.instanceName.ToLower(CultureInfo.InvariantCulture), this.instanceLifetime);
						this.initialized = true;
					}
				}
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(this.InstanceLockObject);
				}
			}
		}

		// Token: 0x06002F20 RID: 12064 RVA: 0x000D3790 File Offset: 0x000D1990
		public CounterSample NextSample()
		{
			string category = this.categoryName;
			string machine = this.machineName;
			PerformanceCounterPermission performanceCounterPermission = new PerformanceCounterPermission(PerformanceCounterPermissionAccess.Browse, machine, category);
			performanceCounterPermission.Demand();
			this.Initialize();
			CategorySample categorySample = PerformanceCounterLib.GetCategorySample(machine, category);
			CounterDefinitionSample counterDefinitionSample = categorySample.GetCounterDefinitionSample(this.counterName);
			this.counterType = counterDefinitionSample.CounterType;
			if (!categorySample.IsMultiInstance)
			{
				if (this.instanceName != null && this.instanceName.Length != 0)
				{
					throw new InvalidOperationException(SR.GetString("InstanceNameProhibited", new object[]
					{
						this.instanceName
					}));
				}
				return counterDefinitionSample.GetSingleValue();
			}
			else
			{
				if (this.instanceName == null || this.instanceName.Length == 0)
				{
					throw new InvalidOperationException(SR.GetString("InstanceNameRequired"));
				}
				return counterDefinitionSample.GetInstanceValue(this.instanceName);
			}
		}

		// Token: 0x06002F21 RID: 12065 RVA: 0x000D385C File Offset: 0x000D1A5C
		public float NextValue()
		{
			CounterSample nextCounterSample = this.NextSample();
			float result = CounterSample.Calculate(this.oldSample, nextCounterSample);
			this.oldSample = nextCounterSample;
			return result;
		}

		// Token: 0x06002F22 RID: 12066 RVA: 0x000D388C File Offset: 0x000D1A8C
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public void RemoveInstance()
		{
			if (this.isReadOnly)
			{
				throw new InvalidOperationException(SR.GetString("ReadOnlyRemoveInstance"));
			}
			this.Initialize();
			this.sharedCounter.RemoveInstance(this.instanceName.ToLower(CultureInfo.InvariantCulture), this.instanceLifetime);
		}

		// Token: 0x040027AC RID: 10156
		private string machineName;

		// Token: 0x040027AD RID: 10157
		private string categoryName;

		// Token: 0x040027AE RID: 10158
		private string counterName;

		// Token: 0x040027AF RID: 10159
		private string instanceName;

		// Token: 0x040027B0 RID: 10160
		private PerformanceCounterInstanceLifetime instanceLifetime;

		// Token: 0x040027B1 RID: 10161
		private bool isReadOnly;

		// Token: 0x040027B2 RID: 10162
		private bool initialized;

		// Token: 0x040027B3 RID: 10163
		private string helpMsg;

		// Token: 0x040027B4 RID: 10164
		private int counterType = -1;

		// Token: 0x040027B5 RID: 10165
		private CounterSample oldSample = CounterSample.Empty;

		// Token: 0x040027B6 RID: 10166
		private SharedPerformanceCounter sharedCounter;

		// Token: 0x040027B7 RID: 10167
		[Obsolete("This field has been deprecated and is not used.  Use machine.config or an application configuration file to set the size of the PerformanceCounter file mapping.")]
		public static int DefaultFileMappingSize = 524288;

		// Token: 0x040027B8 RID: 10168
		private object m_InstanceLockObject;
	}
}
