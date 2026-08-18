using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using System.Threading;

namespace System.Diagnostics
{
	// Token: 0x02000765 RID: 1893
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, SharedState = true)]
	public sealed class PerformanceCounterCategory
	{
		// Token: 0x06003A41 RID: 14913 RVA: 0x000F6428 File Offset: 0x000F5428
		public PerformanceCounterCategory()
		{
			this.machineName = ".";
		}

		// Token: 0x06003A42 RID: 14914 RVA: 0x000F643B File Offset: 0x000F543B
		public PerformanceCounterCategory(string categoryName) : this(categoryName, ".")
		{
		}

		// Token: 0x06003A43 RID: 14915 RVA: 0x000F644C File Offset: 0x000F544C
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

		// Token: 0x17000D9F RID: 3487
		// (get) Token: 0x06003A44 RID: 14916 RVA: 0x000F64E2 File Offset: 0x000F54E2
		// (set) Token: 0x06003A45 RID: 14917 RVA: 0x000F64EC File Offset: 0x000F54EC
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

		// Token: 0x17000DA0 RID: 3488
		// (get) Token: 0x06003A46 RID: 14918 RVA: 0x000F6570 File Offset: 0x000F5570
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

		// Token: 0x17000DA1 RID: 3489
		// (get) Token: 0x06003A47 RID: 14919 RVA: 0x000F65B0 File Offset: 0x000F55B0
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

		// Token: 0x17000DA2 RID: 3490
		// (get) Token: 0x06003A48 RID: 14920 RVA: 0x000F65FD File Offset: 0x000F55FD
		// (set) Token: 0x06003A49 RID: 14921 RVA: 0x000F6608 File Offset: 0x000F5608
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

		// Token: 0x06003A4A RID: 14922 RVA: 0x000F6688 File Offset: 0x000F5688
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

		// Token: 0x06003A4B RID: 14923 RVA: 0x000F66C2 File Offset: 0x000F56C2
		public static bool CounterExists(string counterName, string categoryName)
		{
			return PerformanceCounterCategory.CounterExists(counterName, categoryName, ".");
		}

		// Token: 0x06003A4C RID: 14924 RVA: 0x000F66D0 File Offset: 0x000F56D0
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

		// Token: 0x06003A4D RID: 14925 RVA: 0x000F6768 File Offset: 0x000F5768
		[Obsolete("This method has been deprecated.  Please use System.Diagnostics.PerformanceCounterCategory.Create(string categoryName, string categoryHelp, PerformanceCounterCategoryType categoryType, string counterName, string counterHelp) instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		public static PerformanceCounterCategory Create(string categoryName, string categoryHelp, string counterName, string counterHelp)
		{
			CounterCreationData counterCreationData = new CounterCreationData(counterName, counterHelp, PerformanceCounterType.NumberOfItems32);
			return PerformanceCounterCategory.Create(categoryName, categoryHelp, PerformanceCounterCategoryType.Unknown, new CounterCreationDataCollection(new CounterCreationData[]
			{
				counterCreationData
			}));
		}

		// Token: 0x06003A4E RID: 14926 RVA: 0x000F679C File Offset: 0x000F579C
		public static PerformanceCounterCategory Create(string categoryName, string categoryHelp, PerformanceCounterCategoryType categoryType, string counterName, string counterHelp)
		{
			CounterCreationData counterCreationData = new CounterCreationData(counterName, counterHelp, PerformanceCounterType.NumberOfItems32);
			return PerformanceCounterCategory.Create(categoryName, categoryHelp, categoryType, new CounterCreationDataCollection(new CounterCreationData[]
			{
				counterCreationData
			}));
		}

		// Token: 0x06003A4F RID: 14927 RVA: 0x000F67D0 File Offset: 0x000F57D0
		[Obsolete("This method has been deprecated.  Please use System.Diagnostics.PerformanceCounterCategory.Create(string categoryName, string categoryHelp, PerformanceCounterCategoryType categoryType, CounterCreationDataCollection counterData) instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		public static PerformanceCounterCategory Create(string categoryName, string categoryHelp, CounterCreationDataCollection counterData)
		{
			return PerformanceCounterCategory.Create(categoryName, categoryHelp, PerformanceCounterCategoryType.Unknown, counterData);
		}

		// Token: 0x06003A50 RID: 14928 RVA: 0x000F67DC File Offset: 0x000F57DC
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

		// Token: 0x06003A51 RID: 14929 RVA: 0x000F68A8 File Offset: 0x000F58A8
		internal static void CheckValidCategory(string categoryName)
		{
			if (categoryName == null)
			{
				throw new ArgumentNullException("categoryName");
			}
			if (!PerformanceCounterCategory.CheckValidId(categoryName))
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

		// Token: 0x06003A52 RID: 14930 RVA: 0x000F6920 File Offset: 0x000F5920
		internal static void CheckValidCounter(string counterName)
		{
			if (counterName == null)
			{
				throw new ArgumentNullException("counterName");
			}
			if (!PerformanceCounterCategory.CheckValidId(counterName))
			{
				throw new ArgumentException(SR.GetString("PerfInvalidCounterName", new object[]
				{
					1,
					80
				}));
			}
		}

		// Token: 0x06003A53 RID: 14931 RVA: 0x000F6970 File Offset: 0x000F5970
		internal static bool CheckValidId(string id)
		{
			if (id.Length == 0 || id.Length > 80)
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

		// Token: 0x06003A54 RID: 14932 RVA: 0x000F69D4 File Offset: 0x000F59D4
		internal static void CheckValidHelp(string help)
		{
			if (help == null)
			{
				throw new ArgumentNullException("help");
			}
			if (help.Length > 255)
			{
				throw new ArgumentException(SR.GetString("PerfInvalidHelp", new object[]
				{
					0,
					255
				}));
			}
		}

		// Token: 0x06003A55 RID: 14933 RVA: 0x000F6A2C File Offset: 0x000F5A2C
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

		// Token: 0x06003A56 RID: 14934 RVA: 0x000F6C20 File Offset: 0x000F5C20
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

		// Token: 0x06003A57 RID: 14935 RVA: 0x000F6CBC File Offset: 0x000F5CBC
		public static bool Exists(string categoryName)
		{
			return PerformanceCounterCategory.Exists(categoryName, ".");
		}

		// Token: 0x06003A58 RID: 14936 RVA: 0x000F6CCC File Offset: 0x000F5CCC
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

		// Token: 0x06003A59 RID: 14937 RVA: 0x000F6D60 File Offset: 0x000F5D60
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

		// Token: 0x06003A5A RID: 14938 RVA: 0x000F6DD8 File Offset: 0x000F5DD8
		public PerformanceCounter[] GetCounters()
		{
			if (this.GetInstanceNames().Length != 0)
			{
				throw new ArgumentException(SR.GetString("InstanceNameRequired"));
			}
			return this.GetCounters("");
		}

		// Token: 0x06003A5B RID: 14939 RVA: 0x000F6E00 File Offset: 0x000F5E00
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

		// Token: 0x06003A5C RID: 14940 RVA: 0x000F6EAC File Offset: 0x000F5EAC
		public static PerformanceCounterCategory[] GetCategories()
		{
			return PerformanceCounterCategory.GetCategories(".");
		}

		// Token: 0x06003A5D RID: 14941 RVA: 0x000F6EB8 File Offset: 0x000F5EB8
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

		// Token: 0x06003A5E RID: 14942 RVA: 0x000F6F33 File Offset: 0x000F5F33
		public string[] GetInstanceNames()
		{
			if (this.categoryName == null)
			{
				throw new InvalidOperationException(SR.GetString("CategoryNameNotSet"));
			}
			return PerformanceCounterCategory.GetCounterInstances(this.categoryName, this.machineName);
		}

		// Token: 0x06003A5F RID: 14943 RVA: 0x000F6F60 File Offset: 0x000F5F60
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

		// Token: 0x06003A60 RID: 14944 RVA: 0x000F6FB1 File Offset: 0x000F5FB1
		public static bool InstanceExists(string instanceName, string categoryName)
		{
			return PerformanceCounterCategory.InstanceExists(instanceName, categoryName, ".");
		}

		// Token: 0x06003A61 RID: 14945 RVA: 0x000F6FC0 File Offset: 0x000F5FC0
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

		// Token: 0x06003A62 RID: 14946 RVA: 0x000F7050 File Offset: 0x000F6050
		public InstanceDataCollectionCollection ReadCategory()
		{
			if (this.categoryName == null)
			{
				throw new InvalidOperationException(SR.GetString("CategoryNameNotSet"));
			}
			CategorySample categorySample = PerformanceCounterLib.GetCategorySample(this.machineName, this.categoryName);
			return categorySample.ReadCategory();
		}

		// Token: 0x04003310 RID: 13072
		internal const int MaxNameLength = 80;

		// Token: 0x04003311 RID: 13073
		internal const int MaxHelpLength = 255;

		// Token: 0x04003312 RID: 13074
		private const string perfMutexName = "netfxperf.1.0";

		// Token: 0x04003313 RID: 13075
		private string categoryName;

		// Token: 0x04003314 RID: 13076
		private string categoryHelp;

		// Token: 0x04003315 RID: 13077
		private string machineName;
	}
}
