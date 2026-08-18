using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using System.Threading;

namespace System.Diagnostics
{
	// Token: 0x020004DF RID: 1247
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, SharedState = true)]
	public sealed class PerformanceCounterCategory
	{
		// Token: 0x06002F24 RID: 12068 RVA: 0x000D38E4 File Offset: 0x000D1AE4
		public PerformanceCounterCategory()
		{
			this.machineName = ".";
		}

		// Token: 0x06002F25 RID: 12069 RVA: 0x000D38F7 File Offset: 0x000D1AF7
		public PerformanceCounterCategory(string categoryName) : this(categoryName, ".")
		{
		}

		// Token: 0x06002F26 RID: 12070 RVA: 0x000D3908 File Offset: 0x000D1B08
		public PerformanceCounterCategory(string categoryName, string machineName)
		{
			if (categoryName == null)
			{
				throw new ArgumentNullException("categoryName");
			}
			if (categoryName.Length == 0)
			{
				throw new ArgumentException(SR.GetString("InvalidParameter", new object[]
				{
					"categoryName",
					categoryName
				}));
			}
			if (!SyntaxCheck.CheckMachineName(machineName))
			{
				throw new ArgumentException(SR.GetString("InvalidParameter", new object[]
				{
					"machineName",
					machineName
				}));
			}
			PerformanceCounterPermission performanceCounterPermission = new PerformanceCounterPermission(PerformanceCounterPermissionAccess.Browse, machineName, categoryName);
			performanceCounterPermission.Demand();
			this.categoryName = categoryName;
			this.machineName = machineName;
		}

		// Token: 0x17000B7E RID: 2942
		// (get) Token: 0x06002F27 RID: 12071 RVA: 0x000D399A File Offset: 0x000D1B9A
		// (set) Token: 0x06002F28 RID: 12072 RVA: 0x000D39A4 File Offset: 0x000D1BA4
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
				if (value.Length == 0)
				{
					throw new ArgumentException(SR.GetString("InvalidProperty", new object[]
					{
						"CategoryName",
						value
					}));
				}
				lock (this)
				{
					PerformanceCounterPermission performanceCounterPermission = new PerformanceCounterPermission(PerformanceCounterPermissionAccess.Browse, this.machineName, value);
					performanceCounterPermission.Demand();
					this.categoryName = value;
				}
			}
		}

		// Token: 0x17000B7F RID: 2943
		// (get) Token: 0x06002F29 RID: 12073 RVA: 0x000D3A2C File Offset: 0x000D1C2C
		public string CategoryHelp
		{
			get
			{
				if (this.categoryName == null)
				{
					throw new InvalidOperationException(SR.GetString("CategoryNameNotSet"));
				}
				if (this.categoryHelp == null)
				{
					this.categoryHelp = PerformanceCounterLib.GetCategoryHelp(this.machineName, this.categoryName);
				}
				return this.categoryHelp;
			}
		}

		// Token: 0x17000B80 RID: 2944
		// (get) Token: 0x06002F2A RID: 12074 RVA: 0x000D3A6C File Offset: 0x000D1C6C
		public PerformanceCounterCategoryType CategoryType
		{
			get
			{
				CategorySample categorySample = PerformanceCounterLib.GetCategorySample(this.machineName, this.categoryName);
				if (categorySample.IsMultiInstance)
				{
					return PerformanceCounterCategoryType.MultiInstance;
				}
				if (PerformanceCounterLib.IsCustomCategory(".", this.categoryName))
				{
					return PerformanceCounterLib.GetCategoryType(".", this.categoryName);
				}
				return PerformanceCounterCategoryType.SingleInstance;
			}
		}

		// Token: 0x17000B81 RID: 2945
		// (get) Token: 0x06002F2B RID: 12075 RVA: 0x000D3AB9 File Offset: 0x000D1CB9
		// (set) Token: 0x06002F2C RID: 12076 RVA: 0x000D3AC4 File Offset: 0x000D1CC4
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
					throw new ArgumentException(SR.GetString("InvalidProperty", new object[]
					{
						"MachineName",
						value
					}));
				}
				lock (this)
				{
					if (this.categoryName != null)
					{
						PerformanceCounterPermission performanceCounterPermission = new PerformanceCounterPermission(PerformanceCounterPermissionAccess.Browse, value, this.categoryName);
						performanceCounterPermission.Demand();
					}
					this.machineName = value;
				}
			}
		}

		// Token: 0x06002F2D RID: 12077 RVA: 0x000D3B48 File Offset: 0x000D1D48
		public bool CounterExists(string counterName)
		{
			if (counterName == null)
			{
				throw new ArgumentNullException("counterName");
			}
			if (this.categoryName == null)
			{
				throw new InvalidOperationException(SR.GetString("CategoryNameNotSet"));
			}
			return PerformanceCounterLib.CounterExists(this.machineName, this.categoryName, counterName);
		}

		// Token: 0x06002F2E RID: 12078 RVA: 0x000D3B82 File Offset: 0x000D1D82
		public static bool CounterExists(string counterName, string categoryName)
		{
			return PerformanceCounterCategory.CounterExists(counterName, categoryName, ".");
		}

		// Token: 0x06002F2F RID: 12079 RVA: 0x000D3B90 File Offset: 0x000D1D90
		public static bool CounterExists(string counterName, string categoryName, string machineName)
		{
			if (counterName == null)
			{
				throw new ArgumentNullException("counterName");
			}
			if (categoryName == null)
			{
				throw new ArgumentNullException("categoryName");
			}
			if (categoryName.Length == 0)
			{
				throw new ArgumentException(SR.GetString("InvalidParameter", new object[]
				{
					"categoryName",
					categoryName
				}));
			}
			if (!SyntaxCheck.CheckMachineName(machineName))
			{
				throw new ArgumentException(SR.GetString("InvalidParameter", new object[]
				{
					"machineName",
					machineName
				}));
			}
			PerformanceCounterPermission performanceCounterPermission = new PerformanceCounterPermission(PerformanceCounterPermissionAccess.Browse, machineName, categoryName);
			performanceCounterPermission.Demand();
			return PerformanceCounterLib.CounterExists(machineName, categoryName, counterName);
		}

		// Token: 0x06002F30 RID: 12080 RVA: 0x000D3C24 File Offset: 0x000D1E24
		[Obsolete("This method has been deprecated.  Please use System.Diagnostics.PerformanceCounterCategory.Create(string categoryName, string categoryHelp, PerformanceCounterCategoryType categoryType, string counterName, string counterHelp) instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		public static PerformanceCounterCategory Create(string categoryName, string categoryHelp, string counterName, string counterHelp)
		{
			CounterCreationData counterCreationData = new CounterCreationData(counterName, counterHelp, PerformanceCounterType.NumberOfItems32);
			return PerformanceCounterCategory.Create(categoryName, categoryHelp, PerformanceCounterCategoryType.Unknown, new CounterCreationDataCollection(new CounterCreationData[]
			{
				counterCreationData
			}));
		}

		// Token: 0x06002F31 RID: 12081 RVA: 0x000D3C58 File Offset: 0x000D1E58
		public static PerformanceCounterCategory Create(string categoryName, string categoryHelp, PerformanceCounterCategoryType categoryType, string counterName, string counterHelp)
		{
			CounterCreationData counterCreationData = new CounterCreationData(counterName, counterHelp, PerformanceCounterType.NumberOfItems32);
			return PerformanceCounterCategory.Create(categoryName, categoryHelp, categoryType, new CounterCreationDataCollection(new CounterCreationData[]
			{
				counterCreationData
			}));
		}

		// Token: 0x06002F32 RID: 12082 RVA: 0x000D3C8A File Offset: 0x000D1E8A
		[Obsolete("This method has been deprecated.  Please use System.Diagnostics.PerformanceCounterCategory.Create(string categoryName, string categoryHelp, PerformanceCounterCategoryType categoryType, CounterCreationDataCollection counterData) instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		public static PerformanceCounterCategory Create(string categoryName, string categoryHelp, CounterCreationDataCollection counterData)
		{
			return PerformanceCounterCategory.Create(categoryName, categoryHelp, PerformanceCounterCategoryType.Unknown, counterData);
		}

		// Token: 0x06002F33 RID: 12083 RVA: 0x000D3C98 File Offset: 0x000D1E98
		public static PerformanceCounterCategory Create(string categoryName, string categoryHelp, PerformanceCounterCategoryType categoryType, CounterCreationDataCollection counterData)
		{
			if (categoryType < PerformanceCounterCategoryType.Unknown || categoryType > PerformanceCounterCategoryType.MultiInstance)
			{
				throw new ArgumentOutOfRangeException("categoryType");
			}
			if (counterData == null)
			{
				throw new ArgumentNullException("counterData");
			}
			PerformanceCounterCategory.CheckValidCategory(categoryName);
			if (categoryHelp != null)
			{
				PerformanceCounterCategory.CheckValidHelp(categoryHelp);
			}
			string machine = ".";
			PerformanceCounterPermission performanceCounterPermission = new PerformanceCounterPermission(PerformanceCounterPermissionAccess.Administer, machine, categoryName);
			performanceCounterPermission.Demand();
			SharedUtils.CheckNtEnvironment();
			Mutex mutex = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			PerformanceCounterCategory result;
			try
			{
				SharedUtils.EnterMutex("netfxperf.1.0", ref mutex);
				if (PerformanceCounterLib.IsCustomCategory(machine, categoryName) || PerformanceCounterLib.CategoryExists(machine, categoryName))
				{
					throw new InvalidOperationException(SR.GetString("PerformanceCategoryExists", new object[]
					{
						categoryName
					}));
				}
				PerformanceCounterCategory.CheckValidCounterLayout(counterData);
				PerformanceCounterLib.RegisterCategory(categoryName, categoryType, categoryHelp, counterData);
				result = new PerformanceCounterCategory(categoryName, machine);
			}
			finally
			{
				if (mutex != null)
				{
					mutex.ReleaseMutex();
					mutex.Close();
				}
			}
			return result;
		}

		// Token: 0x06002F34 RID: 12084 RVA: 0x000D3D68 File Offset: 0x000D1F68
		internal static void CheckValidCategory(string categoryName)
		{
			if (categoryName == null)
			{
				throw new ArgumentNullException("categoryName");
			}
			if (!PerformanceCounterCategory.CheckValidId(categoryName, 80))
			{
				throw new ArgumentException(SR.GetString("PerfInvalidCategoryName", new object[]
				{
					1,
					80
				}));
			}
			if (categoryName.Length > 1024 - "netfxcustomperfcounters.1.0".Length)
			{
				throw new ArgumentException(SR.GetString("CategoryNameTooLong"));
			}
		}

		// Token: 0x06002F35 RID: 12085 RVA: 0x000D3DE0 File Offset: 0x000D1FE0
		internal static void CheckValidCounter(string counterName)
		{
			if (counterName == null)
			{
				throw new ArgumentNullException("counterName");
			}
			if (!PerformanceCounterCategory.CheckValidId(counterName, 32767))
			{
				throw new ArgumentException(SR.GetString("PerfInvalidCounterName", new object[]
				{
					1,
					32767
				}));
			}
		}

		// Token: 0x06002F36 RID: 12086 RVA: 0x000D3E34 File Offset: 0x000D2034
		internal static bool CheckValidId(string id, int maxLength)
		{
			if (id.Length == 0 || id.Length > maxLength)
			{
				return false;
			}
			for (int i = 0; i < id.Length; i++)
			{
				char c = id[i];
				if ((i == 0 || i == id.Length - 1) && c == ' ')
				{
					return false;
				}
				if (c == '"')
				{
					return false;
				}
				if (char.IsControl(c))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002F37 RID: 12087 RVA: 0x000D3E94 File Offset: 0x000D2094
		internal static void CheckValidHelp(string help)
		{
			if (help == null)
			{
				throw new ArgumentNullException("help");
			}
			if (help.Length > 32767)
			{
				throw new ArgumentException(SR.GetString("PerfInvalidHelp", new object[]
				{
					0,
					32767
				}));
			}
		}

		// Token: 0x06002F38 RID: 12088 RVA: 0x000D3EE8 File Offset: 0x000D20E8
		internal static void CheckValidCounterLayout(CounterCreationDataCollection counterData)
		{
			Hashtable hashtable = new Hashtable();
			for (int i = 0; i < counterData.Count; i++)
			{
				if (counterData[i].CounterName == null || counterData[i].CounterName.Length == 0)
				{
					throw new ArgumentException(SR.GetString("InvalidCounterName"));
				}
				int counterType = (int)counterData[i].CounterType;
				if (counterType == 1073874176 || counterType == 575735040 || counterType == 592512256 || counterType == 574686464 || counterType == 591463680 || counterType == 537003008 || counterType == 549585920 || counterType == 805438464)
				{
					if (counterData.Count <= i + 1)
					{
						throw new InvalidOperationException(SR.GetString("CounterLayout"));
					}
					counterType = (int)counterData[i + 1].CounterType;
					if (!PerformanceCounterLib.IsBaseCounter(counterType))
					{
						throw new InvalidOperationException(SR.GetString("CounterLayout"));
					}
				}
				else if (PerformanceCounterLib.IsBaseCounter(counterType))
				{
					if (i == 0)
					{
						throw new InvalidOperationException(SR.GetString("CounterLayout"));
					}
					counterType = (int)counterData[i - 1].CounterType;
					if (counterType != 1073874176 && counterType != 575735040 && counterType != 592512256 && counterType != 574686464 && counterType != 591463680 && counterType != 537003008 && counterType != 549585920 && counterType != 805438464)
					{
						throw new InvalidOperationException(SR.GetString("CounterLayout"));
					}
				}
				if (hashtable.ContainsKey(counterData[i].CounterName))
				{
					throw new ArgumentException(SR.GetString("DuplicateCounterName", new object[]
					{
						counterData[i].CounterName
					}));
				}
				hashtable.Add(counterData[i].CounterName, string.Empty);
				if (counterData[i].CounterHelp == null || counterData[i].CounterHelp.Length == 0)
				{
					counterData[i].CounterHelp = counterData[i].CounterName;
				}
			}
		}

		// Token: 0x06002F39 RID: 12089 RVA: 0x000D40D8 File Offset: 0x000D22D8
		public static void Delete(string categoryName)
		{
			PerformanceCounterCategory.CheckValidCategory(categoryName);
			string machine = ".";
			PerformanceCounterPermission performanceCounterPermission = new PerformanceCounterPermission(PerformanceCounterPermissionAccess.Administer, machine, categoryName);
			performanceCounterPermission.Demand();
			SharedUtils.CheckNtEnvironment();
			categoryName = categoryName.ToLower(CultureInfo.InvariantCulture);
			Mutex mutex = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				SharedUtils.EnterMutex("netfxperf.1.0", ref mutex);
				if (!PerformanceCounterLib.IsCustomCategory(machine, categoryName))
				{
					throw new InvalidOperationException(SR.GetString("CantDeleteCategory"));
				}
				SharedPerformanceCounter.RemoveAllInstances(categoryName);
				PerformanceCounterLib.UnregisterCategory(categoryName);
				PerformanceCounterLib.CloseAllLibraries();
			}
			finally
			{
				if (mutex != null)
				{
					mutex.ReleaseMutex();
					mutex.Close();
				}
			}
		}

		// Token: 0x06002F3A RID: 12090 RVA: 0x000D4174 File Offset: 0x000D2374
		public static bool Exists(string categoryName)
		{
			return PerformanceCounterCategory.Exists(categoryName, ".");
		}

		// Token: 0x06002F3B RID: 12091 RVA: 0x000D4184 File Offset: 0x000D2384
		public static bool Exists(string categoryName, string machineName)
		{
			if (categoryName == null)
			{
				throw new ArgumentNullException("categoryName");
			}
			if (categoryName.Length == 0)
			{
				throw new ArgumentException(SR.GetString("InvalidParameter", new object[]
				{
					"categoryName",
					categoryName
				}));
			}
			if (!SyntaxCheck.CheckMachineName(machineName))
			{
				throw new ArgumentException(SR.GetString("InvalidParameter", new object[]
				{
					"machineName",
					machineName
				}));
			}
			PerformanceCounterPermission performanceCounterPermission = new PerformanceCounterPermission(PerformanceCounterPermissionAccess.Browse, machineName, categoryName);
			performanceCounterPermission.Demand();
			return PerformanceCounterLib.IsCustomCategory(machineName, categoryName) || PerformanceCounterLib.CategoryExists(machineName, categoryName);
		}

		// Token: 0x06002F3C RID: 12092 RVA: 0x000D4214 File Offset: 0x000D2414
		internal static string[] GetCounterInstances(string categoryName, string machineName)
		{
			PerformanceCounterPermission performanceCounterPermission = new PerformanceCounterPermission(PerformanceCounterPermissionAccess.Browse, machineName, categoryName);
			performanceCounterPermission.Demand();
			CategorySample categorySample = PerformanceCounterLib.GetCategorySample(machineName, categoryName);
			if (categorySample.InstanceNameTable.Count == 0)
			{
				return new string[0];
			}
			string[] array = new string[categorySample.InstanceNameTable.Count];
			categorySample.InstanceNameTable.Keys.CopyTo(array, 0);
			if (array.Length == 1 && array[0].CompareTo("systemdiagnosticsperfcounterlibsingleinstance") == 0)
			{
				return new string[0];
			}
			return array;
		}

		// Token: 0x06002F3D RID: 12093 RVA: 0x000D428C File Offset: 0x000D248C
		public PerformanceCounter[] GetCounters()
		{
			if (this.GetInstanceNames().Length != 0)
			{
				throw new ArgumentException(SR.GetString("InstanceNameRequired"));
			}
			return this.GetCounters("");
		}

		// Token: 0x06002F3E RID: 12094 RVA: 0x000D42B4 File Offset: 0x000D24B4
		public PerformanceCounter[] GetCounters(string instanceName)
		{
			if (instanceName == null)
			{
				throw new ArgumentNullException("instanceName");
			}
			if (this.categoryName == null)
			{
				throw new InvalidOperationException(SR.GetString("CategoryNameNotSet"));
			}
			if (instanceName.Length != 0 && !this.InstanceExists(instanceName))
			{
				throw new InvalidOperationException(SR.GetString("MissingInstance", new object[]
				{
					instanceName,
					this.categoryName
				}));
			}
			string[] counters = PerformanceCounterLib.GetCounters(this.machineName, this.categoryName);
			PerformanceCounter[] array = new PerformanceCounter[counters.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new PerformanceCounter(this.categoryName, counters[i], instanceName, this.machineName, true);
			}
			return array;
		}

		// Token: 0x06002F3F RID: 12095 RVA: 0x000D435E File Offset: 0x000D255E
		public static PerformanceCounterCategory[] GetCategories()
		{
			return PerformanceCounterCategory.GetCategories(".");
		}

		// Token: 0x06002F40 RID: 12096 RVA: 0x000D436C File Offset: 0x000D256C
		public static PerformanceCounterCategory[] GetCategories(string machineName)
		{
			if (!SyntaxCheck.CheckMachineName(machineName))
			{
				throw new ArgumentException(SR.GetString("InvalidParameter", new object[]
				{
					"machineName",
					machineName
				}));
			}
			PerformanceCounterPermission performanceCounterPermission = new PerformanceCounterPermission(PerformanceCounterPermissionAccess.Browse, machineName, "*");
			performanceCounterPermission.Demand();
			string[] categories = PerformanceCounterLib.GetCategories(machineName);
			PerformanceCounterCategory[] array = new PerformanceCounterCategory[categories.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new PerformanceCounterCategory(categories[i], machineName);
			}
			return array;
		}

		// Token: 0x06002F41 RID: 12097 RVA: 0x000D43E1 File Offset: 0x000D25E1
		public string[] GetInstanceNames()
		{
			if (this.categoryName == null)
			{
				throw new InvalidOperationException(SR.GetString("CategoryNameNotSet"));
			}
			return PerformanceCounterCategory.GetCounterInstances(this.categoryName, this.machineName);
		}

		// Token: 0x06002F42 RID: 12098 RVA: 0x000D440C File Offset: 0x000D260C
		public bool InstanceExists(string instanceName)
		{
			if (instanceName == null)
			{
				throw new ArgumentNullException("instanceName");
			}
			if (this.categoryName == null)
			{
				throw new InvalidOperationException(SR.GetString("CategoryNameNotSet"));
			}
			CategorySample categorySample = PerformanceCounterLib.GetCategorySample(this.machineName, this.categoryName);
			return categorySample.InstanceNameTable.ContainsKey(instanceName);
		}

		// Token: 0x06002F43 RID: 12099 RVA: 0x000D445D File Offset: 0x000D265D
		public static bool InstanceExists(string instanceName, string categoryName)
		{
			return PerformanceCounterCategory.InstanceExists(instanceName, categoryName, ".");
		}

		// Token: 0x06002F44 RID: 12100 RVA: 0x000D446C File Offset: 0x000D266C
		public static bool InstanceExists(string instanceName, string categoryName, string machineName)
		{
			if (instanceName == null)
			{
				throw new ArgumentNullException("instanceName");
			}
			if (categoryName == null)
			{
				throw new ArgumentNullException("categoryName");
			}
			if (categoryName.Length == 0)
			{
				throw new ArgumentException(SR.GetString("InvalidParameter", new object[]
				{
					"categoryName",
					categoryName
				}));
			}
			if (!SyntaxCheck.CheckMachineName(machineName))
			{
				throw new ArgumentException(SR.GetString("InvalidParameter", new object[]
				{
					"machineName",
					machineName
				}));
			}
			PerformanceCounterCategory performanceCounterCategory = new PerformanceCounterCategory(categoryName, machineName);
			return performanceCounterCategory.InstanceExists(instanceName);
		}

		// Token: 0x06002F45 RID: 12101 RVA: 0x000D44F8 File Offset: 0x000D26F8
		public InstanceDataCollectionCollection ReadCategory()
		{
			if (this.categoryName == null)
			{
				throw new InvalidOperationException(SR.GetString("CategoryNameNotSet"));
			}
			CategorySample categorySample = PerformanceCounterLib.GetCategorySample(this.machineName, this.categoryName);
			return categorySample.ReadCategory();
		}

		// Token: 0x040027B9 RID: 10169
		private string categoryName;

		// Token: 0x040027BA RID: 10170
		private string categoryHelp;

		// Token: 0x040027BB RID: 10171
		private string machineName;

		// Token: 0x040027BC RID: 10172
		internal const int MaxCategoryNameLength = 80;

		// Token: 0x040027BD RID: 10173
		internal const int MaxCounterNameLength = 32767;

		// Token: 0x040027BE RID: 10174
		internal const int MaxHelpLength = 32767;

		// Token: 0x040027BF RID: 10175
		private const string perfMutexName = "netfxperf.1.0";
	}
}
