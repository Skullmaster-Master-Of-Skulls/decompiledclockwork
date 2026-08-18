using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Security.Permissions;
using System.Threading;

namespace System.Diagnostics
{
	// Token: 0x02000764 RID: 1892
	[SRDescription("PerformanceCounterDesc")]
	[InstallerType("System.Diagnostics.PerformanceCounterInstaller,System.Configuration.Install, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, SharedState = true)]
	public sealed class PerformanceCounter : Component, ISupportInitialize
	{
		// Token: 0x06003A1C RID: 14876 RVA: 0x000F5AF0 File Offset: 0x000F4AF0
		public PerformanceCounter()
		{
			this.machineName = ".";
			this.categoryName = string.Empty;
			this.counterName = string.Empty;
			this.instanceName = string.Empty;
			this.isReadOnly = true;
			GC.SuppressFinalize(this);
		}

		// Token: 0x06003A1D RID: 14877 RVA: 0x000F5B50 File Offset: 0x000F4B50
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

		// Token: 0x06003A1E RID: 14878 RVA: 0x000F5BA8 File Offset: 0x000F4BA8
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

		// Token: 0x06003A1F RID: 14879 RVA: 0x000F5BFE File Offset: 0x000F4BFE
		public PerformanceCounter(string categoryName, string counterName, string instanceName) : this(categoryName, counterName, instanceName, true)
		{
		}

		// Token: 0x06003A20 RID: 14880 RVA: 0x000F5C0C File Offset: 0x000F4C0C
		public PerformanceCounter(string categoryName, string counterName, string instanceName, bool readOnly)
		{
			this.MachineName = ".";
			this.CategoryName = categoryName;
			this.CounterName = counterName;
			this.InstanceName = instanceName;
			this.isReadOnly = readOnly;
			this.Initialize();
			GC.SuppressFinalize(this);
		}

		// Token: 0x06003A21 RID: 14881 RVA: 0x000F5C65 File Offset: 0x000F4C65
		public PerformanceCounter(string categoryName, string counterName) : this(categoryName, counterName, true)
		{
		}

		// Token: 0x06003A22 RID: 14882 RVA: 0x000F5C70 File Offset: 0x000F4C70
		public PerformanceCounter(string categoryName, string counterName, bool readOnly) : this(categoryName, counterName, "", readOnly)
		{
		}

		// Token: 0x17000D96 RID: 3478
		// (get) Token: 0x06003A23 RID: 14883 RVA: 0x000F5C80 File Offset: 0x000F4C80
		// (set) Token: 0x06003A24 RID: 14884 RVA: 0x000F5C88 File Offset: 0x000F4C88
		[ReadOnly(true)]
		[SRDescription("PCCategoryName")]
		[RecommendedAsConfigurable(true)]
		[TypeConverter("System.Diagnostics.Design.CategoryValueConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
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

		// Token: 0x17000D97 RID: 3479
		// (get) Token: 0x06003A25 RID: 14885 RVA: 0x000F5CBC File Offset: 0x000F4CBC
		[MonitoringDescription("PC_CounterHelp")]
		[ReadOnly(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

		// Token: 0x17000D98 RID: 3480
		// (get) Token: 0x06003A26 RID: 14886 RVA: 0x000F5D0D File Offset: 0x000F4D0D
		// (set) Token: 0x06003A27 RID: 14887 RVA: 0x000F5D15 File Offset: 0x000F4D15
		[ReadOnly(true)]
		[SRDescription("PCCounterName")]
		[DefaultValue("")]
		[TypeConverter("System.Diagnostics.Design.CounterNameConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[RecommendedAsConfigurable(true)]
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

		// Token: 0x17000D99 RID: 3481
		// (get) Token: 0x06003A28 RID: 14888 RVA: 0x000F5D4C File Offset: 0x000F4D4C
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

		// Token: 0x17000D9A RID: 3482
		// (get) Token: 0x06003A29 RID: 14889 RVA: 0x000F5DAE File Offset: 0x000F4DAE
		// (set) Token: 0x06003A2A RID: 14890 RVA: 0x000F5DB6 File Offset: 0x000F4DB6
		[SRDescription("PCInstanceLifetime")]
		[DefaultValue(PerformanceCounterInstanceLifetime.Global)]
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

		// Token: 0x17000D9B RID: 3483
		// (get) Token: 0x06003A2B RID: 14891 RVA: 0x000F5DEA File Offset: 0x000F4DEA
		// (set) Token: 0x06003A2C RID: 14892 RVA: 0x000F5DF2 File Offset: 0x000F4DF2
		[SRDescription("PCInstanceName")]
		[RecommendedAsConfigurable(true)]
		[ReadOnly(true)]
		[DefaultValue("")]
		[TypeConverter("System.Diagnostics.Design.InstanceNameConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
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

		// Token: 0x17000D9C RID: 3484
		// (get) Token: 0x06003A2D RID: 14893 RVA: 0x000F5E32 File Offset: 0x000F4E32
		// (set) Token: 0x06003A2E RID: 14894 RVA: 0x000F5E3A File Offset: 0x000F4E3A
		[MonitoringDescription("PC_ReadOnly")]
		[DefaultValue(true)]
		[Browsable(false)]
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
					this.isReadOnly = value;
					this.Close();
				}
			}
		}

		// Token: 0x17000D9D RID: 3485
		// (get) Token: 0x06003A2F RID: 14895 RVA: 0x000F5E52 File Offset: 0x000F4E52
		// (set) Token: 0x06003A30 RID: 14896 RVA: 0x000F5E5C File Offset: 0x000F4E5C
		[SRDescription("PCMachineName")]
		[RecommendedAsConfigurable(true)]
		[DefaultValue(".")]
		[Browsable(false)]
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

		// Token: 0x17000D9E RID: 3486
		// (get) Token: 0x06003A31 RID: 14897 RVA: 0x000F5EB0 File Offset: 0x000F4EB0
		// (set) Token: 0x06003A32 RID: 14898 RVA: 0x000F5EE5 File Offset: 0x000F4EE5
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

		// Token: 0x06003A33 RID: 14899 RVA: 0x000F5F07 File Offset: 0x000F4F07
		public void BeginInit()
		{
			this.Close();
		}

		// Token: 0x06003A34 RID: 14900 RVA: 0x000F5F0F File Offset: 0x000F4F0F
		public void Close()
		{
			this.helpMsg = null;
			this.oldSample = CounterSample.Empty;
			this.sharedCounter = null;
			this.initialized = false;
			this.counterType = -1;
		}

		// Token: 0x06003A35 RID: 14901 RVA: 0x000F5F38 File Offset: 0x000F4F38
		public static void CloseSharedResources()
		{
			PerformanceCounterPermission performanceCounterPermission = new PerformanceCounterPermission(PerformanceCounterPermissionAccess.Browse, ".", "*");
			performanceCounterPermission.Demand();
			PerformanceCounterLib.CloseAllLibraries();
		}

		// Token: 0x06003A36 RID: 14902 RVA: 0x000F5F61 File Offset: 0x000F4F61
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Close();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06003A37 RID: 14903 RVA: 0x000F5F73 File Offset: 0x000F4F73
		public long Decrement()
		{
			if (this.ReadOnly)
			{
				this.ThrowReadOnly();
			}
			this.Initialize();
			return this.sharedCounter.Decrement();
		}

		// Token: 0x06003A38 RID: 14904 RVA: 0x000F5F94 File Offset: 0x000F4F94
		public void EndInit()
		{
			this.Initialize();
		}

		// Token: 0x06003A39 RID: 14905 RVA: 0x000F5F9C File Offset: 0x000F4F9C
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

		// Token: 0x06003A3A RID: 14906 RVA: 0x000F5FBE File Offset: 0x000F4FBE
		public long Increment()
		{
			if (this.isReadOnly)
			{
				this.ThrowReadOnly();
			}
			this.Initialize();
			return this.sharedCounter.Increment();
		}

		// Token: 0x06003A3B RID: 14907 RVA: 0x000F5FDF File Offset: 0x000F4FDF
		private void ThrowReadOnly()
		{
			throw new InvalidOperationException(SR.GetString("ReadOnlyCounter"));
		}

		// Token: 0x06003A3C RID: 14908 RVA: 0x000F5FF0 File Offset: 0x000F4FF0
		private void Initialize()
		{
			if (!this.initialized && !base.DesignMode)
			{
				bool flag = false;
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					RuntimeHelpers.PrepareConstrainedRegions();
					try
					{
					}
					finally
					{
						Monitor.Enter(this);
						flag = true;
					}
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
						Monitor.Exit(this);
					}
				}
			}
		}

		// Token: 0x06003A3D RID: 14909 RVA: 0x000F62D0 File Offset: 0x000F52D0
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

		// Token: 0x06003A3E RID: 14910 RVA: 0x000F63A0 File Offset: 0x000F53A0
		public float NextValue()
		{
			CounterSample nextCounterSample = this.NextSample();
			float result = CounterSample.Calculate(this.oldSample, nextCounterSample);
			this.oldSample = nextCounterSample;
			return result;
		}

		// Token: 0x06003A3F RID: 14911 RVA: 0x000F63D0 File Offset: 0x000F53D0
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

		// Token: 0x04003304 RID: 13060
		private string machineName;

		// Token: 0x04003305 RID: 13061
		private string categoryName;

		// Token: 0x04003306 RID: 13062
		private string counterName;

		// Token: 0x04003307 RID: 13063
		private string instanceName;

		// Token: 0x04003308 RID: 13064
		private PerformanceCounterInstanceLifetime instanceLifetime;

		// Token: 0x04003309 RID: 13065
		private bool isReadOnly;

		// Token: 0x0400330A RID: 13066
		private bool initialized;

		// Token: 0x0400330B RID: 13067
		private string helpMsg;

		// Token: 0x0400330C RID: 13068
		private int counterType = -1;

		// Token: 0x0400330D RID: 13069
		private CounterSample oldSample = CounterSample.Empty;

		// Token: 0x0400330E RID: 13070
		private SharedPerformanceCounter sharedCounter;

		// Token: 0x0400330F RID: 13071
		[Obsolete("This field has been deprecated and is not used.  Use machine.config or an application configuration file to set the size of the PerformanceCounter file mapping.")]
		public static int DefaultFileMappingSize = 524288;
	}
}
