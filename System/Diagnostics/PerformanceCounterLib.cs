using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using Microsoft.Win32;

namespace System.Diagnostics
{
	// Token: 0x02000769 RID: 1897
	internal class PerformanceCounterLib
	{
		// Token: 0x17000DA3 RID: 3491
		// (get) Token: 0x06003A63 RID: 14947 RVA: 0x000F7090 File Offset: 0x000F6090
		private static object InternalSyncObject
		{
			get
			{
				if (PerformanceCounterLib.s_InternalSyncObject == null)
				{
					object value = new object();
					Interlocked.CompareExchange(ref PerformanceCounterLib.s_InternalSyncObject, value, null);
				}
				return PerformanceCounterLib.s_InternalSyncObject;
			}
		}

		// Token: 0x06003A64 RID: 14948 RVA: 0x000F70BC File Offset: 0x000F60BC
		internal PerformanceCounterLib(string machineName, string lcid)
		{
			this.machineName = machineName;
			this.perfLcid = lcid;
		}

		// Token: 0x17000DA4 RID: 3492
		// (get) Token: 0x06003A65 RID: 14949 RVA: 0x000F70F4 File Offset: 0x000F60F4
		internal static string ComputerName
		{
			get
			{
				if (PerformanceCounterLib.computerName == null)
				{
					lock (PerformanceCounterLib.InternalSyncObject)
					{
						if (PerformanceCounterLib.computerName == null)
						{
							StringBuilder stringBuilder = new StringBuilder(256);
							SafeNativeMethods.GetComputerName(stringBuilder, new int[]
							{
								stringBuilder.Capacity
							});
							PerformanceCounterLib.computerName = stringBuilder.ToString();
						}
					}
				}
				return PerformanceCounterLib.computerName;
			}
		}

		// Token: 0x17000DA5 RID: 3493
		// (get) Token: 0x06003A66 RID: 14950 RVA: 0x000F7168 File Offset: 0x000F6168
		private unsafe Hashtable CategoryTable
		{
			get
			{
				if (this.categoryTable == null)
				{
					lock (this.CategoryTableLock)
					{
						if (this.categoryTable == null)
						{
							byte[] performanceData = this.GetPerformanceData("Global");
							fixed (byte* ptr = performanceData)
							{
								IntPtr intPtr = new IntPtr((void*)ptr);
								NativeMethods.PERF_DATA_BLOCK perf_DATA_BLOCK = new NativeMethods.PERF_DATA_BLOCK();
								Marshal.PtrToStructure(intPtr, perf_DATA_BLOCK);
								intPtr = (IntPtr)((long)intPtr + (long)perf_DATA_BLOCK.HeaderLength);
								int numObjectTypes = perf_DATA_BLOCK.NumObjectTypes;
								long num = ptr + (long)perf_DATA_BLOCK.TotalByteLength;
								Hashtable hashtable = new Hashtable(numObjectTypes, StringComparer.OrdinalIgnoreCase);
								int num2 = 0;
								while (num2 < numObjectTypes && (long)intPtr < num)
								{
									NativeMethods.PERF_OBJECT_TYPE perf_OBJECT_TYPE = new NativeMethods.PERF_OBJECT_TYPE();
									Marshal.PtrToStructure(intPtr, perf_OBJECT_TYPE);
									CategoryEntry categoryEntry = new CategoryEntry(perf_OBJECT_TYPE);
									IntPtr intPtr2 = (IntPtr)((long)intPtr + (long)perf_OBJECT_TYPE.TotalByteLength);
									intPtr = (IntPtr)((long)intPtr + (long)perf_OBJECT_TYPE.HeaderLength);
									int num3 = 0;
									int num4 = -1;
									for (int i = 0; i < categoryEntry.CounterIndexes.Length; i++)
									{
										NativeMethods.PERF_COUNTER_DEFINITION perf_COUNTER_DEFINITION = new NativeMethods.PERF_COUNTER_DEFINITION();
										Marshal.PtrToStructure(intPtr, perf_COUNTER_DEFINITION);
										if (perf_COUNTER_DEFINITION.CounterNameTitleIndex != num4)
										{
											categoryEntry.CounterIndexes[num3] = perf_COUNTER_DEFINITION.CounterNameTitleIndex;
											categoryEntry.HelpIndexes[num3] = perf_COUNTER_DEFINITION.CounterHelpTitleIndex;
											num4 = perf_COUNTER_DEFINITION.CounterNameTitleIndex;
											num3++;
										}
										intPtr = (IntPtr)((long)intPtr + (long)perf_COUNTER_DEFINITION.ByteLength);
									}
									if (num3 < categoryEntry.CounterIndexes.Length)
									{
										int[] array = new int[num3];
										int[] array2 = new int[num3];
										Array.Copy(categoryEntry.CounterIndexes, array, num3);
										Array.Copy(categoryEntry.HelpIndexes, array2, num3);
										categoryEntry.CounterIndexes = array;
										categoryEntry.HelpIndexes = array2;
									}
									string text = (string)this.NameTable[categoryEntry.NameIndex];
									if (text != null)
									{
										hashtable[text] = categoryEntry;
									}
									intPtr = intPtr2;
									num2++;
								}
								this.categoryTable = hashtable;
							}
						}
					}
				}
				return this.categoryTable;
			}
		}

		// Token: 0x17000DA6 RID: 3494
		// (get) Token: 0x06003A67 RID: 14951 RVA: 0x000F73A8 File Offset: 0x000F63A8
		internal Hashtable HelpTable
		{
			get
			{
				if (this.helpTable == null)
				{
					lock (this.HelpTableLock)
					{
						if (this.helpTable == null)
						{
							this.helpTable = this.GetStringTable(true);
						}
					}
				}
				return this.helpTable;
			}
		}

		// Token: 0x17000DA7 RID: 3495
		// (get) Token: 0x06003A68 RID: 14952 RVA: 0x000F7400 File Offset: 0x000F6400
		private static string IniFilePath
		{
			get
			{
				if (PerformanceCounterLib.iniFilePath == null)
				{
					lock (PerformanceCounterLib.InternalSyncObject)
					{
						if (PerformanceCounterLib.iniFilePath == null)
						{
							EnvironmentPermission environmentPermission = new EnvironmentPermission(PermissionState.Unrestricted);
							environmentPermission.Assert();
							try
							{
								PerformanceCounterLib.iniFilePath = Path.GetTempFileName();
							}
							finally
							{
								CodeAccessPermission.RevertAssert();
							}
						}
					}
				}
				return PerformanceCounterLib.iniFilePath;
			}
		}

		// Token: 0x17000DA8 RID: 3496
		// (get) Token: 0x06003A69 RID: 14953 RVA: 0x000F7470 File Offset: 0x000F6470
		internal Hashtable NameTable
		{
			get
			{
				if (this.nameTable == null)
				{
					lock (this.NameTableLock)
					{
						if (this.nameTable == null)
						{
							this.nameTable = this.GetStringTable(false);
						}
					}
				}
				return this.nameTable;
			}
		}

		// Token: 0x17000DA9 RID: 3497
		// (get) Token: 0x06003A6A RID: 14954 RVA: 0x000F74C8 File Offset: 0x000F64C8
		private static string SymbolFilePath
		{
			get
			{
				if (PerformanceCounterLib.symbolFilePath == null)
				{
					lock (PerformanceCounterLib.InternalSyncObject)
					{
						if (PerformanceCounterLib.symbolFilePath == null)
						{
							EnvironmentPermission environmentPermission = new EnvironmentPermission(PermissionState.Unrestricted);
							environmentPermission.Assert();
							string tempPath = Path.GetTempPath();
							CodeAccessPermission.RevertAssert();
							PermissionSet permissionSet = new PermissionSet(PermissionState.None);
							permissionSet.AddPermission(new EnvironmentPermission(PermissionState.Unrestricted));
							permissionSet.AddPermission(new FileIOPermission(FileIOPermissionAccess.Write, tempPath));
							permissionSet.Assert();
							try
							{
								PerformanceCounterLib.symbolFilePath = Path.GetTempFileName();
							}
							finally
							{
								PermissionSet.RevertAssert();
							}
						}
					}
				}
				return PerformanceCounterLib.symbolFilePath;
			}
		}

		// Token: 0x06003A6B RID: 14955 RVA: 0x000F756C File Offset: 0x000F656C
		internal static bool CategoryExists(string machine, string category)
		{
			PerformanceCounterLib performanceCounterLib;
			for (CultureInfo cultureInfo = CultureInfo.CurrentCulture; cultureInfo != CultureInfo.InvariantCulture; cultureInfo = cultureInfo.Parent)
			{
				performanceCounterLib = PerformanceCounterLib.GetPerformanceCounterLib(machine, cultureInfo);
				if (performanceCounterLib.CategoryExists(category))
				{
					return true;
				}
			}
			performanceCounterLib = PerformanceCounterLib.GetPerformanceCounterLib(machine, new CultureInfo(9));
			return performanceCounterLib.CategoryExists(category);
		}

		// Token: 0x06003A6C RID: 14956 RVA: 0x000F75BF File Offset: 0x000F65BF
		internal bool CategoryExists(string category)
		{
			return this.CategoryTable.ContainsKey(category);
		}

		// Token: 0x06003A6D RID: 14957 RVA: 0x000F75D0 File Offset: 0x000F65D0
		internal static void CloseAllLibraries()
		{
			if (PerformanceCounterLib.libraryTable != null)
			{
				foreach (object obj in PerformanceCounterLib.libraryTable.Values)
				{
					PerformanceCounterLib performanceCounterLib = (PerformanceCounterLib)obj;
					performanceCounterLib.Close();
				}
				PerformanceCounterLib.libraryTable = null;
			}
		}

		// Token: 0x06003A6E RID: 14958 RVA: 0x000F763C File Offset: 0x000F663C
		internal static void CloseAllTables()
		{
			if (PerformanceCounterLib.libraryTable != null)
			{
				foreach (object obj in PerformanceCounterLib.libraryTable.Values)
				{
					PerformanceCounterLib performanceCounterLib = (PerformanceCounterLib)obj;
					performanceCounterLib.CloseTables();
				}
			}
		}

		// Token: 0x06003A6F RID: 14959 RVA: 0x000F76A0 File Offset: 0x000F66A0
		internal void CloseTables()
		{
			this.nameTable = null;
			this.helpTable = null;
			this.categoryTable = null;
			this.customCategoryTable = null;
		}

		// Token: 0x06003A70 RID: 14960 RVA: 0x000F76BE File Offset: 0x000F66BE
		internal void Close()
		{
			if (this.performanceMonitor != null)
			{
				this.performanceMonitor.Close();
				this.performanceMonitor = null;
			}
			this.CloseTables();
		}

		// Token: 0x06003A71 RID: 14961 RVA: 0x000F76E0 File Offset: 0x000F66E0
		internal static bool CounterExists(string machine, string category, string counter)
		{
			bool flag = false;
			bool flag2 = false;
			for (CultureInfo cultureInfo = CultureInfo.CurrentCulture; cultureInfo != CultureInfo.InvariantCulture; cultureInfo = cultureInfo.Parent)
			{
				PerformanceCounterLib performanceCounterLib = PerformanceCounterLib.GetPerformanceCounterLib(machine, cultureInfo);
				flag2 = performanceCounterLib.CounterExists(category, counter, ref flag);
				if (flag2)
				{
					break;
				}
			}
			if (!flag2)
			{
				PerformanceCounterLib performanceCounterLib = PerformanceCounterLib.GetPerformanceCounterLib(machine, new CultureInfo(9));
				flag2 = performanceCounterLib.CounterExists(category, counter, ref flag);
			}
			if (!flag)
			{
				throw new InvalidOperationException(SR.GetString("MissingCategory"));
			}
			return flag2;
		}

		// Token: 0x06003A72 RID: 14962 RVA: 0x000F7750 File Offset: 0x000F6750
		private bool CounterExists(string category, string counter, ref bool categoryExists)
		{
			categoryExists = false;
			if (!this.CategoryTable.ContainsKey(category))
			{
				return false;
			}
			categoryExists = true;
			CategoryEntry categoryEntry = (CategoryEntry)this.CategoryTable[category];
			for (int i = 0; i < categoryEntry.CounterIndexes.Length; i++)
			{
				int num = categoryEntry.CounterIndexes[i];
				string text = (string)this.NameTable[num];
				if (text == null)
				{
					text = string.Empty;
				}
				if (string.Compare(text, counter, StringComparison.OrdinalIgnoreCase) == 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003A73 RID: 14963 RVA: 0x000F77D0 File Offset: 0x000F67D0
		private static void CreateIniFile(string categoryName, string categoryHelp, CounterCreationDataCollection creationData, string[] languageIds)
		{
			FileIOPermission fileIOPermission = new FileIOPermission(PermissionState.Unrestricted);
			fileIOPermission.Assert();
			try
			{
				StreamWriter streamWriter = new StreamWriter(PerformanceCounterLib.IniFilePath, false, Encoding.Unicode);
				try
				{
					streamWriter.WriteLine("");
					streamWriter.WriteLine("[info]");
					streamWriter.Write("drivername");
					streamWriter.Write("=");
					streamWriter.WriteLine(categoryName);
					streamWriter.Write("symbolfile");
					streamWriter.Write("=");
					streamWriter.WriteLine(Path.GetFileName(PerformanceCounterLib.SymbolFilePath));
					streamWriter.WriteLine("");
					streamWriter.WriteLine("[languages]");
					foreach (string value in languageIds)
					{
						streamWriter.Write(value);
						streamWriter.Write("=");
						streamWriter.Write("language");
						streamWriter.WriteLine(value);
					}
					streamWriter.WriteLine("");
					streamWriter.WriteLine("[objects]");
					foreach (string value2 in languageIds)
					{
						streamWriter.Write("OBJECT_");
						streamWriter.Write("1_");
						streamWriter.Write(value2);
						streamWriter.Write("_NAME");
						streamWriter.Write("=");
						streamWriter.WriteLine(categoryName);
					}
					streamWriter.WriteLine("");
					streamWriter.WriteLine("[text]");
					foreach (string value3 in languageIds)
					{
						streamWriter.Write("OBJECT_");
						streamWriter.Write("1_");
						streamWriter.Write(value3);
						streamWriter.Write("_NAME");
						streamWriter.Write("=");
						streamWriter.WriteLine(categoryName);
						streamWriter.Write("OBJECT_");
						streamWriter.Write("1_");
						streamWriter.Write(value3);
						streamWriter.Write("_HELP");
						streamWriter.Write("=");
						if (categoryHelp == null || categoryHelp == string.Empty)
						{
							streamWriter.WriteLine(SR.GetString("HelpNotAvailable"));
						}
						else
						{
							streamWriter.WriteLine(categoryHelp);
						}
						int num = 0;
						foreach (object obj in creationData)
						{
							CounterCreationData counterCreationData = (CounterCreationData)obj;
							num++;
							streamWriter.WriteLine("");
							streamWriter.Write("DEVICE_COUNTER_");
							streamWriter.Write(num.ToString(CultureInfo.InvariantCulture));
							streamWriter.Write("_");
							streamWriter.Write(value3);
							streamWriter.Write("_NAME");
							streamWriter.Write("=");
							streamWriter.WriteLine(counterCreationData.CounterName);
							streamWriter.Write("DEVICE_COUNTER_");
							streamWriter.Write(num.ToString(CultureInfo.InvariantCulture));
							streamWriter.Write("_");
							streamWriter.Write(value3);
							streamWriter.Write("_HELP");
							streamWriter.Write("=");
							streamWriter.WriteLine(counterCreationData.CounterHelp);
						}
					}
					streamWriter.WriteLine("");
				}
				finally
				{
					streamWriter.Close();
				}
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
		}

		// Token: 0x06003A74 RID: 14964 RVA: 0x000F7B48 File Offset: 0x000F6B48
		private static void CreateRegistryEntry(string categoryName, PerformanceCounterCategoryType categoryType, CounterCreationDataCollection creationData, ref bool iniRegistered)
		{
			RegistryKey registryKey = null;
			RegistryKey registryKey2 = null;
			RegistryKey registryKey3 = null;
			RegistryPermission registryPermission = new RegistryPermission(PermissionState.Unrestricted);
			registryPermission.Assert();
			try
			{
				registryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Services", true);
				registryKey2 = registryKey.OpenSubKey(categoryName + "\\Performance", true);
				if (registryKey2 == null)
				{
					registryKey2 = registryKey.CreateSubKey(categoryName + "\\Performance");
				}
				registryKey2.SetValue("Open", "OpenPerformanceData");
				registryKey2.SetValue("Collect", "CollectPerformanceData");
				registryKey2.SetValue("Close", "ClosePerformanceData");
				registryKey2.SetValue("Library", "netfxperf.dll");
				registryKey2.SetValue("IsMultiInstance", (int)categoryType, RegistryValueKind.DWord);
				registryKey2.SetValue("CategoryOptions", 3, RegistryValueKind.DWord);
				string[] array = new string[creationData.Count];
				string[] array2 = new string[creationData.Count];
				for (int i = 0; i < creationData.Count; i++)
				{
					array[i] = creationData[i].CounterName;
					array2[i] = ((int)creationData[i].CounterType).ToString(CultureInfo.InvariantCulture);
				}
				registryKey3 = registryKey.OpenSubKey(categoryName + "\\Linkage", true);
				if (registryKey3 == null)
				{
					registryKey3 = registryKey.CreateSubKey(categoryName + "\\Linkage");
				}
				registryKey3.SetValue("Export", new string[]
				{
					categoryName
				});
				registryKey2.SetValue("Counter Types", array2);
				registryKey2.SetValue("Counter Names", array);
				object value = registryKey2.GetValue("First Counter");
				if (value != null)
				{
					iniRegistered = true;
				}
				else
				{
					iniRegistered = false;
				}
			}
			finally
			{
				if (registryKey2 != null)
				{
					registryKey2.Close();
				}
				if (registryKey3 != null)
				{
					registryKey3.Close();
				}
				if (registryKey != null)
				{
					registryKey.Close();
				}
				CodeAccessPermission.RevertAssert();
			}
		}

		// Token: 0x06003A75 RID: 14965 RVA: 0x000F7D1C File Offset: 0x000F6D1C
		private static void CreateSymbolFile(CounterCreationDataCollection creationData)
		{
			FileIOPermission fileIOPermission = new FileIOPermission(PermissionState.Unrestricted);
			fileIOPermission.Assert();
			try
			{
				StreamWriter streamWriter = new StreamWriter(PerformanceCounterLib.SymbolFilePath);
				try
				{
					streamWriter.Write("#define");
					streamWriter.Write(" ");
					streamWriter.Write("OBJECT_");
					streamWriter.WriteLine("1 0;");
					for (int i = 1; i <= creationData.Count; i++)
					{
						streamWriter.Write("#define");
						streamWriter.Write(" ");
						streamWriter.Write("DEVICE_COUNTER_");
						streamWriter.Write(i.ToString(CultureInfo.InvariantCulture));
						streamWriter.Write(" ");
						streamWriter.Write((i * 2).ToString(CultureInfo.InvariantCulture));
						streamWriter.WriteLine(";");
					}
					streamWriter.WriteLine("");
				}
				finally
				{
					streamWriter.Close();
				}
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
		}

		// Token: 0x06003A76 RID: 14966 RVA: 0x000F7E18 File Offset: 0x000F6E18
		private static void DeleteRegistryEntry(string categoryName)
		{
			RegistryKey registryKey = null;
			RegistryPermission registryPermission = new RegistryPermission(PermissionState.Unrestricted);
			registryPermission.Assert();
			try
			{
				registryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Services", true);
				bool flag = false;
				using (RegistryKey registryKey2 = registryKey.OpenSubKey(categoryName, true))
				{
					if (registryKey2 != null)
					{
						if (registryKey2.GetValueNames().Length == 0)
						{
							flag = true;
						}
						else
						{
							registryKey2.DeleteSubKeyTree("Linkage");
							registryKey2.DeleteSubKeyTree("Performance");
						}
					}
				}
				if (flag)
				{
					registryKey.DeleteSubKeyTree(categoryName);
				}
			}
			finally
			{
				if (registryKey != null)
				{
					registryKey.Close();
				}
				CodeAccessPermission.RevertAssert();
			}
		}

		// Token: 0x06003A77 RID: 14967 RVA: 0x000F7EBC File Offset: 0x000F6EBC
		private static void DeleteTemporaryFiles()
		{
			try
			{
				File.Delete(PerformanceCounterLib.IniFilePath);
			}
			catch
			{
			}
			try
			{
				File.Delete(PerformanceCounterLib.SymbolFilePath);
			}
			catch
			{
			}
		}

		// Token: 0x06003A78 RID: 14968 RVA: 0x000F7F04 File Offset: 0x000F6F04
		internal bool FindCustomCategory(string category, out PerformanceCounterCategoryType categoryType)
		{
			RegistryKey registryKey = null;
			RegistryKey registryKey2 = null;
			categoryType = PerformanceCounterCategoryType.Unknown;
			if (this.customCategoryTable == null)
			{
				this.customCategoryTable = new Hashtable(StringComparer.OrdinalIgnoreCase);
			}
			if (this.customCategoryTable.ContainsKey(category))
			{
				categoryType = (PerformanceCounterCategoryType)this.customCategoryTable[category];
				return true;
			}
			PermissionSet permissionSet = new PermissionSet(PermissionState.None);
			permissionSet.AddPermission(new RegistryPermission(PermissionState.Unrestricted));
			permissionSet.AddPermission(new SecurityPermission(SecurityPermissionFlag.UnmanagedCode));
			permissionSet.Assert();
			try
			{
				string name = "SYSTEM\\CurrentControlSet\\Services\\" + category + "\\Performance";
				if (this.machineName == "." || string.Compare(this.machineName, PerformanceCounterLib.ComputerName, StringComparison.OrdinalIgnoreCase) == 0)
				{
					registryKey = Registry.LocalMachine.OpenSubKey(name);
				}
				else
				{
					registryKey2 = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, "\\\\" + this.machineName);
					if (registryKey2 != null)
					{
						try
						{
							registryKey = registryKey2.OpenSubKey(name);
						}
						catch (SecurityException)
						{
							categoryType = PerformanceCounterCategoryType.Unknown;
							this.customCategoryTable[category] = categoryType;
							return false;
						}
					}
				}
				if (registryKey != null)
				{
					object value = registryKey.GetValue("Library", null, RegistryValueOptions.DoNotExpandEnvironmentNames);
					if (value != null && value is string && (string.Compare((string)value, "netfxperf.dll", StringComparison.OrdinalIgnoreCase) == 0 || ((string)value).EndsWith("\\netfxperf.dll", StringComparison.OrdinalIgnoreCase)))
					{
						object value2 = registryKey.GetValue("IsMultiInstance");
						if (value2 != null)
						{
							categoryType = (PerformanceCounterCategoryType)value2;
							if (categoryType < PerformanceCounterCategoryType.Unknown || categoryType > PerformanceCounterCategoryType.MultiInstance)
							{
								categoryType = PerformanceCounterCategoryType.Unknown;
							}
						}
						else
						{
							categoryType = PerformanceCounterCategoryType.Unknown;
						}
						object value3 = registryKey.GetValue("First Counter");
						if (value3 != null)
						{
							int num = (int)value3;
							this.customCategoryTable[category] = categoryType;
							return true;
						}
					}
				}
			}
			finally
			{
				if (registryKey != null)
				{
					registryKey.Close();
				}
				if (registryKey2 != null)
				{
					registryKey2.Close();
				}
				PermissionSet.RevertAssert();
			}
			return false;
		}

		// Token: 0x06003A79 RID: 14969 RVA: 0x000F8104 File Offset: 0x000F7104
		internal static string[] GetCategories(string machineName)
		{
			PerformanceCounterLib performanceCounterLib;
			for (CultureInfo cultureInfo = CultureInfo.CurrentCulture; cultureInfo != CultureInfo.InvariantCulture; cultureInfo = cultureInfo.Parent)
			{
				performanceCounterLib = PerformanceCounterLib.GetPerformanceCounterLib(machineName, cultureInfo);
				string[] categories = performanceCounterLib.GetCategories();
				if (categories.Length != 0)
				{
					return categories;
				}
			}
			performanceCounterLib = PerformanceCounterLib.GetPerformanceCounterLib(machineName, new CultureInfo(9));
			return performanceCounterLib.GetCategories();
		}

		// Token: 0x06003A7A RID: 14970 RVA: 0x000F8154 File Offset: 0x000F7154
		internal string[] GetCategories()
		{
			ICollection keys = this.CategoryTable.Keys;
			string[] array = new string[keys.Count];
			keys.CopyTo(array, 0);
			return array;
		}

		// Token: 0x06003A7B RID: 14971 RVA: 0x000F8184 File Offset: 0x000F7184
		internal static string GetCategoryHelp(string machine, string category)
		{
			PerformanceCounterLib performanceCounterLib;
			string categoryHelp;
			if (CultureInfo.CurrentCulture.Parent.LCID != 9)
			{
				for (CultureInfo cultureInfo = CultureInfo.CurrentCulture; cultureInfo != CultureInfo.InvariantCulture; cultureInfo = cultureInfo.Parent)
				{
					performanceCounterLib = PerformanceCounterLib.GetPerformanceCounterLib(machine, cultureInfo);
					categoryHelp = performanceCounterLib.GetCategoryHelp(category);
					if (categoryHelp != null)
					{
						return categoryHelp;
					}
				}
			}
			performanceCounterLib = PerformanceCounterLib.GetPerformanceCounterLib(machine, new CultureInfo(9));
			categoryHelp = performanceCounterLib.GetCategoryHelp(category);
			performanceCounterLib = PerformanceCounterLib.GetPerformanceCounterLib(machine, new CultureInfo(9));
			categoryHelp = performanceCounterLib.GetCategoryHelp(category);
			if (categoryHelp == null)
			{
				throw new InvalidOperationException(SR.GetString("MissingCategory"));
			}
			return categoryHelp;
		}

		// Token: 0x06003A7C RID: 14972 RVA: 0x000F8210 File Offset: 0x000F7210
		private string GetCategoryHelp(string category)
		{
			CategoryEntry categoryEntry = (CategoryEntry)this.CategoryTable[category];
			if (categoryEntry == null)
			{
				return null;
			}
			return (string)this.HelpTable[categoryEntry.HelpIndex];
		}

		// Token: 0x06003A7D RID: 14973 RVA: 0x000F8250 File Offset: 0x000F7250
		internal static CategorySample GetCategorySample(string machine, string category)
		{
			PerformanceCounterLib performanceCounterLib;
			CategorySample categorySample;
			for (CultureInfo cultureInfo = CultureInfo.CurrentCulture; cultureInfo != CultureInfo.InvariantCulture; cultureInfo = cultureInfo.Parent)
			{
				performanceCounterLib = PerformanceCounterLib.GetPerformanceCounterLib(machine, cultureInfo);
				categorySample = performanceCounterLib.GetCategorySample(category);
				if (categorySample != null)
				{
					return categorySample;
				}
			}
			performanceCounterLib = PerformanceCounterLib.GetPerformanceCounterLib(machine, new CultureInfo(9));
			categorySample = performanceCounterLib.GetCategorySample(category);
			if (categorySample == null)
			{
				throw new InvalidOperationException(SR.GetString("MissingCategory"));
			}
			return categorySample;
		}

		// Token: 0x06003A7E RID: 14974 RVA: 0x000F82B4 File Offset: 0x000F72B4
		private CategorySample GetCategorySample(string category)
		{
			CategoryEntry categoryEntry = (CategoryEntry)this.CategoryTable[category];
			if (categoryEntry == null)
			{
				return null;
			}
			byte[] performanceData = this.GetPerformanceData(categoryEntry.NameIndex.ToString(CultureInfo.InvariantCulture));
			if (performanceData == null)
			{
				throw new InvalidOperationException(SR.GetString("CantReadCategory", new object[]
				{
					category
				}));
			}
			return new CategorySample(performanceData, categoryEntry, this);
		}

		// Token: 0x06003A7F RID: 14975 RVA: 0x000F831C File Offset: 0x000F731C
		internal static string[] GetCounters(string machine, string category)
		{
			bool flag = false;
			PerformanceCounterLib performanceCounterLib;
			string[] counters;
			for (CultureInfo cultureInfo = CultureInfo.CurrentCulture; cultureInfo != CultureInfo.InvariantCulture; cultureInfo = cultureInfo.Parent)
			{
				performanceCounterLib = PerformanceCounterLib.GetPerformanceCounterLib(machine, cultureInfo);
				counters = performanceCounterLib.GetCounters(category, ref flag);
				if (flag)
				{
					return counters;
				}
			}
			performanceCounterLib = PerformanceCounterLib.GetPerformanceCounterLib(machine, new CultureInfo(9));
			counters = performanceCounterLib.GetCounters(category, ref flag);
			if (!flag)
			{
				throw new InvalidOperationException(SR.GetString("MissingCategory"));
			}
			return counters;
		}

		// Token: 0x06003A80 RID: 14976 RVA: 0x000F8388 File Offset: 0x000F7388
		private string[] GetCounters(string category, ref bool categoryExists)
		{
			categoryExists = false;
			CategoryEntry categoryEntry = (CategoryEntry)this.CategoryTable[category];
			if (categoryEntry == null)
			{
				return null;
			}
			categoryExists = true;
			int num = 0;
			string[] array = new string[categoryEntry.CounterIndexes.Length];
			for (int i = 0; i < array.Length; i++)
			{
				int num2 = categoryEntry.CounterIndexes[i];
				string text = (string)this.NameTable[num2];
				if (text != null && text != string.Empty)
				{
					array[num] = text;
					num++;
				}
			}
			if (num < array.Length)
			{
				string[] array2 = new string[num];
				Array.Copy(array, array2, num);
				array = array2;
			}
			return array;
		}

		// Token: 0x06003A81 RID: 14977 RVA: 0x000F842C File Offset: 0x000F742C
		internal static string GetCounterHelp(string machine, string category, string counter)
		{
			bool flag = false;
			PerformanceCounterLib performanceCounterLib;
			string counterHelp;
			if (CultureInfo.CurrentCulture.Parent.LCID != 9)
			{
				for (CultureInfo cultureInfo = CultureInfo.CurrentCulture; cultureInfo != CultureInfo.InvariantCulture; cultureInfo = cultureInfo.Parent)
				{
					performanceCounterLib = PerformanceCounterLib.GetPerformanceCounterLib(machine, cultureInfo);
					counterHelp = performanceCounterLib.GetCounterHelp(category, counter, ref flag);
					if (flag)
					{
						return counterHelp;
					}
				}
			}
			performanceCounterLib = PerformanceCounterLib.GetPerformanceCounterLib(machine, new CultureInfo(9));
			counterHelp = performanceCounterLib.GetCounterHelp(category, counter, ref flag);
			if (!flag)
			{
				throw new InvalidOperationException(SR.GetString("MissingCategoryDetail", new object[]
				{
					category
				}));
			}
			return counterHelp;
		}

		// Token: 0x06003A82 RID: 14978 RVA: 0x000F84BC File Offset: 0x000F74BC
		private string GetCounterHelp(string category, string counter, ref bool categoryExists)
		{
			categoryExists = false;
			CategoryEntry categoryEntry = (CategoryEntry)this.CategoryTable[category];
			if (categoryEntry == null)
			{
				return null;
			}
			categoryExists = true;
			int num = -1;
			for (int i = 0; i < categoryEntry.CounterIndexes.Length; i++)
			{
				int num2 = categoryEntry.CounterIndexes[i];
				string text = (string)this.NameTable[num2];
				if (text == null)
				{
					text = string.Empty;
				}
				if (string.Compare(text, counter, StringComparison.OrdinalIgnoreCase) == 0)
				{
					num = categoryEntry.HelpIndexes[i];
					break;
				}
			}
			if (num == -1)
			{
				throw new InvalidOperationException(SR.GetString("MissingCounter", new object[]
				{
					counter
				}));
			}
			string text2 = (string)this.HelpTable[num];
			if (text2 == null)
			{
				return string.Empty;
			}
			return text2;
		}

		// Token: 0x06003A83 RID: 14979 RVA: 0x000F8584 File Offset: 0x000F7584
		internal string GetCounterName(int index)
		{
			if (this.NameTable.ContainsKey(index))
			{
				return (string)this.NameTable[index];
			}
			return "";
		}

		// Token: 0x06003A84 RID: 14980 RVA: 0x000F85B8 File Offset: 0x000F75B8
		private static string[] GetLanguageIds()
		{
			RegistryKey registryKey = null;
			string[] result = new string[0];
			new RegistryPermission(PermissionState.Unrestricted).Assert();
			try
			{
				registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Perflib");
				if (registryKey != null)
				{
					result = registryKey.GetSubKeyNames();
				}
			}
			finally
			{
				if (registryKey != null)
				{
					registryKey.Close();
				}
				CodeAccessPermission.RevertAssert();
			}
			return result;
		}

		// Token: 0x06003A85 RID: 14981 RVA: 0x000F8618 File Offset: 0x000F7618
		internal static PerformanceCounterLib GetPerformanceCounterLib(string machineName, CultureInfo culture)
		{
			SharedUtils.CheckEnvironment();
			string text;
			if ((culture.LCID & 65280) == 0)
			{
				text = culture.LCID.ToString("X3", CultureInfo.InvariantCulture);
			}
			else
			{
				text = culture.LCID.ToString("X4", CultureInfo.InvariantCulture);
			}
			if (machineName.CompareTo(".") == 0)
			{
				machineName = PerformanceCounterLib.ComputerName.ToLower(CultureInfo.InvariantCulture);
			}
			else
			{
				machineName = machineName.ToLower(CultureInfo.InvariantCulture);
			}
			if (PerformanceCounterLib.libraryTable == null)
			{
				PerformanceCounterLib.libraryTable = new Hashtable();
			}
			string key = machineName + ":" + text;
			if (PerformanceCounterLib.libraryTable.Contains(key))
			{
				return (PerformanceCounterLib)PerformanceCounterLib.libraryTable[key];
			}
			PerformanceCounterLib performanceCounterLib = new PerformanceCounterLib(machineName, text);
			PerformanceCounterLib.libraryTable[key] = performanceCounterLib;
			return performanceCounterLib;
		}

		// Token: 0x06003A86 RID: 14982 RVA: 0x000F86EC File Offset: 0x000F76EC
		internal byte[] GetPerformanceData(string item)
		{
			if (this.performanceMonitor == null)
			{
				lock (this)
				{
					if (this.performanceMonitor == null)
					{
						this.performanceMonitor = new PerformanceMonitor(this.machineName);
					}
				}
			}
			return this.performanceMonitor.GetData(item);
		}

		// Token: 0x06003A87 RID: 14983 RVA: 0x000F8748 File Offset: 0x000F7748
		private Hashtable GetStringTable(bool isHelp)
		{
			PermissionSet permissionSet = new PermissionSet(PermissionState.None);
			permissionSet.AddPermission(new RegistryPermission(PermissionState.Unrestricted));
			permissionSet.AddPermission(new SecurityPermission(SecurityPermissionFlag.UnmanagedCode));
			permissionSet.Assert();
			RegistryKey registryKey;
			if (string.Compare(this.machineName, PerformanceCounterLib.ComputerName, StringComparison.OrdinalIgnoreCase) == 0)
			{
				registryKey = Registry.PerformanceData;
			}
			else
			{
				registryKey = RegistryKey.OpenRemoteBaseKey(RegistryHive.PerformanceData, this.machineName);
			}
			Hashtable hashtable;
			try
			{
				string[] array = null;
				int i = 14;
				int num = 0;
				while (i > 0)
				{
					try
					{
						if (!isHelp)
						{
							array = (string[])registryKey.GetValue("Counter " + this.perfLcid);
						}
						else
						{
							array = (string[])registryKey.GetValue("Explain " + this.perfLcid);
						}
						if (array != null && array.Length != 0)
						{
							break;
						}
						i--;
						if (num == 0)
						{
							num = 10;
						}
						else
						{
							Thread.Sleep(num);
							num *= 2;
						}
					}
					catch (IOException)
					{
						array = null;
						break;
					}
				}
				if (array == null)
				{
					hashtable = new Hashtable();
				}
				else
				{
					hashtable = new Hashtable(array.Length / 2);
					for (int j = 0; j < array.Length / 2; j++)
					{
						string text = array[j * 2 + 1];
						if (text == null)
						{
							text = string.Empty;
						}
						hashtable[int.Parse(array[j * 2], CultureInfo.InvariantCulture)] = text;
					}
				}
			}
			finally
			{
				registryKey.Close();
			}
			return hashtable;
		}

		// Token: 0x06003A88 RID: 14984 RVA: 0x000F88A8 File Offset: 0x000F78A8
		internal static bool IsCustomCategory(string machine, string category)
		{
			PerformanceCounterLib performanceCounterLib;
			for (CultureInfo cultureInfo = CultureInfo.CurrentCulture; cultureInfo != CultureInfo.InvariantCulture; cultureInfo = cultureInfo.Parent)
			{
				performanceCounterLib = PerformanceCounterLib.GetPerformanceCounterLib(machine, cultureInfo);
				if (performanceCounterLib.IsCustomCategory(category))
				{
					return true;
				}
			}
			performanceCounterLib = PerformanceCounterLib.GetPerformanceCounterLib(machine, new CultureInfo(9));
			return performanceCounterLib.IsCustomCategory(category);
		}

		// Token: 0x06003A89 RID: 14985 RVA: 0x000F88F9 File Offset: 0x000F78F9
		internal static bool IsBaseCounter(int type)
		{
			return type == 1073939458 || type == 1107494144 || type == 1073939459 || type == 1073939712 || type == 1073939457;
		}

		// Token: 0x06003A8A RID: 14986 RVA: 0x000F8928 File Offset: 0x000F7928
		private bool IsCustomCategory(string category)
		{
			PerformanceCounterCategoryType performanceCounterCategoryType;
			return this.FindCustomCategory(category, out performanceCounterCategoryType);
		}

		// Token: 0x06003A8B RID: 14987 RVA: 0x000F8940 File Offset: 0x000F7940
		internal static PerformanceCounterCategoryType GetCategoryType(string machine, string category)
		{
			PerformanceCounterCategoryType result = PerformanceCounterCategoryType.Unknown;
			PerformanceCounterLib performanceCounterLib;
			for (CultureInfo cultureInfo = CultureInfo.CurrentCulture; cultureInfo != CultureInfo.InvariantCulture; cultureInfo = cultureInfo.Parent)
			{
				performanceCounterLib = PerformanceCounterLib.GetPerformanceCounterLib(machine, cultureInfo);
				if (performanceCounterLib.FindCustomCategory(category, out result))
				{
					return result;
				}
			}
			performanceCounterLib = PerformanceCounterLib.GetPerformanceCounterLib(machine, new CultureInfo(9));
			performanceCounterLib.FindCustomCategory(category, out result);
			return result;
		}

		// Token: 0x06003A8C RID: 14988 RVA: 0x000F8994 File Offset: 0x000F7994
		internal static void RegisterCategory(string categoryName, PerformanceCounterCategoryType categoryType, string categoryHelp, CounterCreationDataCollection creationData)
		{
			try
			{
				bool flag = false;
				PerformanceCounterLib.CreateRegistryEntry(categoryName, categoryType, creationData, ref flag);
				if (!flag)
				{
					string[] languageIds = PerformanceCounterLib.GetLanguageIds();
					PerformanceCounterLib.CreateIniFile(categoryName, categoryHelp, creationData, languageIds);
					PerformanceCounterLib.CreateSymbolFile(creationData);
					PerformanceCounterLib.RegisterFiles(PerformanceCounterLib.IniFilePath, false);
				}
				PerformanceCounterLib.CloseAllTables();
				PerformanceCounterLib.CloseAllLibraries();
			}
			finally
			{
				PerformanceCounterLib.DeleteTemporaryFiles();
			}
		}

		// Token: 0x06003A8D RID: 14989 RVA: 0x000F89F4 File Offset: 0x000F79F4
		private static void RegisterFiles(string arg0, bool unregister)
		{
			ProcessStartInfo processStartInfo = new ProcessStartInfo();
			processStartInfo.UseShellExecute = false;
			processStartInfo.CreateNoWindow = true;
			processStartInfo.ErrorDialog = false;
			processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
			processStartInfo.WorkingDirectory = Environment.SystemDirectory;
			if (unregister)
			{
				processStartInfo.FileName = Environment.SystemDirectory + "\\unlodctr.exe";
			}
			else
			{
				processStartInfo.FileName = Environment.SystemDirectory + "\\lodctr.exe";
			}
			int num = 0;
			new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Assert();
			try
			{
				processStartInfo.Arguments = "\"" + arg0 + "\"";
				Process process = Process.Start(processStartInfo);
				process.WaitForExit();
				num = process.ExitCode;
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
			if (unregister && num == 2)
			{
				num = 0;
			}
			if (num != 0)
			{
				throw SharedUtils.CreateSafeWin32Exception(num);
			}
		}

		// Token: 0x06003A8E RID: 14990 RVA: 0x000F8AC0 File Offset: 0x000F7AC0
		internal static void UnregisterCategory(string categoryName)
		{
			PerformanceCounterLib.RegisterFiles(categoryName, true);
			PerformanceCounterLib.DeleteRegistryEntry(categoryName);
			PerformanceCounterLib.CloseAllTables();
			PerformanceCounterLib.CloseAllLibraries();
		}

		// Token: 0x04003320 RID: 13088
		internal const string PerfShimName = "netfxperf.dll";

		// Token: 0x04003321 RID: 13089
		private const string PerfShimFullNameSuffix = "\\netfxperf.dll";

		// Token: 0x04003322 RID: 13090
		internal const string OpenEntryPoint = "OpenPerformanceData";

		// Token: 0x04003323 RID: 13091
		internal const string CollectEntryPoint = "CollectPerformanceData";

		// Token: 0x04003324 RID: 13092
		internal const string CloseEntryPoint = "ClosePerformanceData";

		// Token: 0x04003325 RID: 13093
		internal const string SingleInstanceName = "systemdiagnosticsperfcounterlibsingleinstance";

		// Token: 0x04003326 RID: 13094
		private const string PerflibPath = "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Perflib";

		// Token: 0x04003327 RID: 13095
		internal const string ServicePath = "SYSTEM\\CurrentControlSet\\Services";

		// Token: 0x04003328 RID: 13096
		private const string categorySymbolPrefix = "OBJECT_";

		// Token: 0x04003329 RID: 13097
		private const string conterSymbolPrefix = "DEVICE_COUNTER_";

		// Token: 0x0400332A RID: 13098
		private const string helpSufix = "_HELP";

		// Token: 0x0400332B RID: 13099
		private const string nameSufix = "_NAME";

		// Token: 0x0400332C RID: 13100
		private const string textDefinition = "[text]";

		// Token: 0x0400332D RID: 13101
		private const string infoDefinition = "[info]";

		// Token: 0x0400332E RID: 13102
		private const string languageDefinition = "[languages]";

		// Token: 0x0400332F RID: 13103
		private const string objectDefinition = "[objects]";

		// Token: 0x04003330 RID: 13104
		private const string driverNameKeyword = "drivername";

		// Token: 0x04003331 RID: 13105
		private const string symbolFileKeyword = "symbolfile";

		// Token: 0x04003332 RID: 13106
		private const string defineKeyword = "#define";

		// Token: 0x04003333 RID: 13107
		private const string languageKeyword = "language";

		// Token: 0x04003334 RID: 13108
		private const string DllName = "netfxperf.dll";

		// Token: 0x04003335 RID: 13109
		private static string computerName;

		// Token: 0x04003336 RID: 13110
		private static string iniFilePath;

		// Token: 0x04003337 RID: 13111
		private static string symbolFilePath;

		// Token: 0x04003338 RID: 13112
		private PerformanceMonitor performanceMonitor;

		// Token: 0x04003339 RID: 13113
		private string machineName;

		// Token: 0x0400333A RID: 13114
		private string perfLcid;

		// Token: 0x0400333B RID: 13115
		private Hashtable customCategoryTable;

		// Token: 0x0400333C RID: 13116
		private static Hashtable libraryTable;

		// Token: 0x0400333D RID: 13117
		private Hashtable categoryTable;

		// Token: 0x0400333E RID: 13118
		private Hashtable nameTable;

		// Token: 0x0400333F RID: 13119
		private Hashtable helpTable;

		// Token: 0x04003340 RID: 13120
		private readonly object CategoryTableLock = new object();

		// Token: 0x04003341 RID: 13121
		private readonly object NameTableLock = new object();

		// Token: 0x04003342 RID: 13122
		private readonly object HelpTableLock = new object();

		// Token: 0x04003343 RID: 13123
		private static object s_InternalSyncObject;
	}
}
