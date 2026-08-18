using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace System.Diagnostics
{
	// Token: 0x020004CB RID: 1227
	[DefaultEvent("EntryWritten")]
	[InstallerType("System.Diagnostics.EventLogInstaller, System.Configuration.Install, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[MonitoringDescription("EventLogDesc")]
	public class EventLog : Component, ISupportInitialize
	{
		// Token: 0x17000B0E RID: 2830
		// (get) Token: 0x06002DBE RID: 11710 RVA: 0x000CDB38 File Offset: 0x000CBD38
		private static bool SkipRegPatch
		{
			get
			{
				if (!EventLog.s_CheckedOsVersion)
				{
					OperatingSystem osversion = Environment.OSVersion;
					EventLog.s_SkipRegPatch = (osversion.Platform == PlatformID.Win32NT && osversion.Version.Major > 5);
					EventLog.s_CheckedOsVersion = true;
				}
				return EventLog.s_SkipRegPatch;
			}
		}

		// Token: 0x06002DBF RID: 11711 RVA: 0x000CDB84 File Offset: 0x000CBD84
		internal static PermissionSet _UnsafeGetAssertPermSet()
		{
			PermissionSet permissionSet = new PermissionSet(PermissionState.None);
			RegistryPermission perm = new RegistryPermission(PermissionState.Unrestricted);
			permissionSet.AddPermission(perm);
			EnvironmentPermission perm2 = new EnvironmentPermission(PermissionState.Unrestricted);
			permissionSet.AddPermission(perm2);
			SecurityPermission perm3 = new SecurityPermission(SecurityPermissionFlag.UnmanagedCode);
			permissionSet.AddPermission(perm3);
			return permissionSet;
		}

		// Token: 0x06002DC0 RID: 11712 RVA: 0x000CDBC6 File Offset: 0x000CBDC6
		public EventLog() : this("", ".", "")
		{
		}

		// Token: 0x06002DC1 RID: 11713 RVA: 0x000CDBDD File Offset: 0x000CBDDD
		public EventLog(string logName) : this(logName, ".", "")
		{
		}

		// Token: 0x06002DC2 RID: 11714 RVA: 0x000CDBF0 File Offset: 0x000CBDF0
		public EventLog(string logName, string machineName) : this(logName, machineName, "")
		{
		}

		// Token: 0x06002DC3 RID: 11715 RVA: 0x000CDBFF File Offset: 0x000CBDFF
		public EventLog(string logName, string machineName, string source)
		{
			this.m_underlyingEventLog = new EventLogInternal(logName, machineName, source, this);
		}

		// Token: 0x17000B0F RID: 2831
		// (get) Token: 0x06002DC4 RID: 11716 RVA: 0x000CDC16 File Offset: 0x000CBE16
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("LogEntries")]
		public EventLogEntryCollection Entries
		{
			get
			{
				return this.m_underlyingEventLog.Entries;
			}
		}

		// Token: 0x17000B10 RID: 2832
		// (get) Token: 0x06002DC5 RID: 11717 RVA: 0x000CDC23 File Offset: 0x000CBE23
		[Browsable(false)]
		public string LogDisplayName
		{
			get
			{
				return this.m_underlyingEventLog.LogDisplayName;
			}
		}

		// Token: 0x17000B11 RID: 2833
		// (get) Token: 0x06002DC6 RID: 11718 RVA: 0x000CDC30 File Offset: 0x000CBE30
		// (set) Token: 0x06002DC7 RID: 11719 RVA: 0x000CDC40 File Offset: 0x000CBE40
		[TypeConverter("System.Diagnostics.Design.LogConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[ReadOnly(true)]
		[MonitoringDescription("LogLog")]
		[DefaultValue("")]
		[SettingsBindable(true)]
		public string Log
		{
			get
			{
				return this.m_underlyingEventLog.Log;
			}
			set
			{
				EventLogInternal eventLogInternal = new EventLogInternal(value, this.m_underlyingEventLog.MachineName, this.m_underlyingEventLog.Source, this);
				EventLogInternal underlyingEventLog = this.m_underlyingEventLog;
				new EventLogPermission(EventLogPermissionAccess.Write, underlyingEventLog.machineName).Assert();
				if (underlyingEventLog.EnableRaisingEvents)
				{
					eventLogInternal.onEntryWrittenHandler = underlyingEventLog.onEntryWrittenHandler;
					eventLogInternal.EnableRaisingEvents = true;
				}
				this.m_underlyingEventLog = eventLogInternal;
				underlyingEventLog.Close();
			}
		}

		// Token: 0x17000B12 RID: 2834
		// (get) Token: 0x06002DC8 RID: 11720 RVA: 0x000CDCAC File Offset: 0x000CBEAC
		// (set) Token: 0x06002DC9 RID: 11721 RVA: 0x000CDCBC File Offset: 0x000CBEBC
		[ReadOnly(true)]
		[MonitoringDescription("LogMachineName")]
		[DefaultValue(".")]
		[SettingsBindable(true)]
		public string MachineName
		{
			get
			{
				return this.m_underlyingEventLog.MachineName;
			}
			set
			{
				EventLogInternal eventLogInternal = new EventLogInternal(this.m_underlyingEventLog.logName, value, this.m_underlyingEventLog.sourceName, this);
				EventLogInternal underlyingEventLog = this.m_underlyingEventLog;
				new EventLogPermission(EventLogPermissionAccess.Write, underlyingEventLog.machineName).Assert();
				if (underlyingEventLog.EnableRaisingEvents)
				{
					eventLogInternal.onEntryWrittenHandler = underlyingEventLog.onEntryWrittenHandler;
					eventLogInternal.EnableRaisingEvents = true;
				}
				this.m_underlyingEventLog = eventLogInternal;
				underlyingEventLog.Close();
			}
		}

		// Token: 0x17000B13 RID: 2835
		// (get) Token: 0x06002DCA RID: 11722 RVA: 0x000CDD28 File Offset: 0x000CBF28
		// (set) Token: 0x06002DCB RID: 11723 RVA: 0x000CDD35 File Offset: 0x000CBF35
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[ComVisible(false)]
		public long MaximumKilobytes
		{
			get
			{
				return this.m_underlyingEventLog.MaximumKilobytes;
			}
			set
			{
				this.m_underlyingEventLog.MaximumKilobytes = value;
			}
		}

		// Token: 0x17000B14 RID: 2836
		// (get) Token: 0x06002DCC RID: 11724 RVA: 0x000CDD43 File Offset: 0x000CBF43
		[Browsable(false)]
		[ComVisible(false)]
		public OverflowAction OverflowAction
		{
			get
			{
				return this.m_underlyingEventLog.OverflowAction;
			}
		}

		// Token: 0x17000B15 RID: 2837
		// (get) Token: 0x06002DCD RID: 11725 RVA: 0x000CDD50 File Offset: 0x000CBF50
		[Browsable(false)]
		[ComVisible(false)]
		public int MinimumRetentionDays
		{
			get
			{
				return this.m_underlyingEventLog.MinimumRetentionDays;
			}
		}

		// Token: 0x17000B16 RID: 2838
		// (get) Token: 0x06002DCE RID: 11726 RVA: 0x000CDD5D File Offset: 0x000CBF5D
		internal bool ComponentDesignMode
		{
			get
			{
				return base.DesignMode;
			}
		}

		// Token: 0x06002DCF RID: 11727 RVA: 0x000CDD65 File Offset: 0x000CBF65
		internal object ComponentGetService(Type service)
		{
			return this.GetService(service);
		}

		// Token: 0x17000B17 RID: 2839
		// (get) Token: 0x06002DD0 RID: 11728 RVA: 0x000CDD6E File Offset: 0x000CBF6E
		// (set) Token: 0x06002DD1 RID: 11729 RVA: 0x000CDD7B File Offset: 0x000CBF7B
		[Browsable(false)]
		[MonitoringDescription("LogMonitoring")]
		[DefaultValue(false)]
		public bool EnableRaisingEvents
		{
			get
			{
				return this.m_underlyingEventLog.EnableRaisingEvents;
			}
			set
			{
				this.m_underlyingEventLog.EnableRaisingEvents = value;
			}
		}

		// Token: 0x17000B18 RID: 2840
		// (get) Token: 0x06002DD2 RID: 11730 RVA: 0x000CDD89 File Offset: 0x000CBF89
		// (set) Token: 0x06002DD3 RID: 11731 RVA: 0x000CDD96 File Offset: 0x000CBF96
		[Browsable(false)]
		[DefaultValue(null)]
		[MonitoringDescription("LogSynchronizingObject")]
		public ISynchronizeInvoke SynchronizingObject
		{
			[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
			get
			{
				return this.m_underlyingEventLog.SynchronizingObject;
			}
			set
			{
				this.m_underlyingEventLog.SynchronizingObject = value;
			}
		}

		// Token: 0x17000B19 RID: 2841
		// (get) Token: 0x06002DD4 RID: 11732 RVA: 0x000CDDA4 File Offset: 0x000CBFA4
		// (set) Token: 0x06002DD5 RID: 11733 RVA: 0x000CDDB4 File Offset: 0x000CBFB4
		[ReadOnly(true)]
		[TypeConverter("System.Diagnostics.Design.StringValueConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[MonitoringDescription("LogSource")]
		[DefaultValue("")]
		[SettingsBindable(true)]
		public string Source
		{
			get
			{
				return this.m_underlyingEventLog.Source;
			}
			set
			{
				EventLogInternal eventLogInternal = new EventLogInternal(this.m_underlyingEventLog.Log, this.m_underlyingEventLog.MachineName, EventLog.CheckAndNormalizeSourceName(value), this);
				EventLogInternal underlyingEventLog = this.m_underlyingEventLog;
				new EventLogPermission(EventLogPermissionAccess.Write, underlyingEventLog.machineName).Assert();
				if (underlyingEventLog.EnableRaisingEvents)
				{
					eventLogInternal.onEntryWrittenHandler = underlyingEventLog.onEntryWrittenHandler;
					eventLogInternal.EnableRaisingEvents = true;
				}
				this.m_underlyingEventLog = eventLogInternal;
				underlyingEventLog.Close();
			}
		}

		// Token: 0x14000040 RID: 64
		// (add) Token: 0x06002DD6 RID: 11734 RVA: 0x000CDE25 File Offset: 0x000CC025
		// (remove) Token: 0x06002DD7 RID: 11735 RVA: 0x000CDE33 File Offset: 0x000CC033
		[MonitoringDescription("LogEntryWritten")]
		public event EntryWrittenEventHandler EntryWritten
		{
			add
			{
				this.m_underlyingEventLog.EntryWritten += value;
			}
			remove
			{
				this.m_underlyingEventLog.EntryWritten -= value;
			}
		}

		// Token: 0x06002DD8 RID: 11736 RVA: 0x000CDE41 File Offset: 0x000CC041
		public void BeginInit()
		{
			this.m_underlyingEventLog.BeginInit();
		}

		// Token: 0x06002DD9 RID: 11737 RVA: 0x000CDE4E File Offset: 0x000CC04E
		public void Clear()
		{
			this.m_underlyingEventLog.Clear();
		}

		// Token: 0x06002DDA RID: 11738 RVA: 0x000CDE5B File Offset: 0x000CC05B
		public void Close()
		{
			this.m_underlyingEventLog.Close();
		}

		// Token: 0x06002DDB RID: 11739 RVA: 0x000CDE68 File Offset: 0x000CC068
		public static void CreateEventSource(string source, string logName)
		{
			EventLog.CreateEventSource(new EventSourceCreationData(source, logName, "."));
		}

		// Token: 0x06002DDC RID: 11740 RVA: 0x000CDE7B File Offset: 0x000CC07B
		[Obsolete("This method has been deprecated.  Please use System.Diagnostics.EventLog.CreateEventSource(EventSourceCreationData sourceData) instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		public static void CreateEventSource(string source, string logName, string machineName)
		{
			EventLog.CreateEventSource(new EventSourceCreationData(source, logName, machineName));
		}

		// Token: 0x06002DDD RID: 11741 RVA: 0x000CDE8C File Offset: 0x000CC08C
		public static void CreateEventSource(EventSourceCreationData sourceData)
		{
			if (sourceData == null)
			{
				throw new ArgumentNullException("sourceData");
			}
			string text = sourceData.LogName;
			string source = sourceData.Source;
			string machineName = sourceData.MachineName;
			if (!SyntaxCheck.CheckMachineName(machineName))
			{
				throw new ArgumentException(SR.GetString("InvalidParameter", new object[]
				{
					"machineName",
					machineName
				}));
			}
			if (text == null || text.Length == 0)
			{
				text = "Application";
			}
			if (!EventLog.ValidLogName(text, false))
			{
				throw new ArgumentException(SR.GetString("BadLogName"));
			}
			if (source == null || source.Length == 0)
			{
				throw new ArgumentException(SR.GetString("MissingParameter", new object[]
				{
					"source"
				}));
			}
			if (source.Length + "SYSTEM\\CurrentControlSet\\Services\\EventLog".Length > 254)
			{
				throw new ArgumentException(SR.GetString("ParameterTooLong", new object[]
				{
					"source",
					254 - "SYSTEM\\CurrentControlSet\\Services\\EventLog".Length
				}));
			}
			EventLogPermission eventLogPermission = new EventLogPermission(EventLogPermissionAccess.Administer, machineName);
			eventLogPermission.Demand();
			Mutex mutex = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				SharedUtils.EnterMutex("netfxeventlog.1.0", ref mutex);
				if (EventLog.SourceExists(source, machineName, true))
				{
					if (".".Equals(machineName))
					{
						throw new ArgumentException(SR.GetString("LocalSourceAlreadyExists", new object[]
						{
							source
						}));
					}
					throw new ArgumentException(SR.GetString("SourceAlreadyExists", new object[]
					{
						source,
						machineName
					}));
				}
				else
				{
					PermissionSet permissionSet = EventLog._UnsafeGetAssertPermSet();
					permissionSet.Assert();
					RegistryKey registryKey = null;
					RegistryKey registryKey2 = null;
					RegistryKey registryKey3 = null;
					RegistryKey registryKey4 = null;
					RegistryKey registryKey5 = null;
					try
					{
						if (machineName == ".")
						{
							registryKey = Registry.LocalMachine;
						}
						else
						{
							registryKey = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, machineName);
						}
						registryKey2 = registryKey.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\EventLog", true);
						if (registryKey2 == null)
						{
							if (!".".Equals(machineName))
							{
								throw new InvalidOperationException(SR.GetString("RegKeyMissing", new object[]
								{
									"SYSTEM\\CurrentControlSet\\Services\\EventLog",
									text,
									source,
									machineName
								}));
							}
							throw new InvalidOperationException(SR.GetString("LocalRegKeyMissing", new object[]
							{
								"SYSTEM\\CurrentControlSet\\Services\\EventLog",
								text,
								source
							}));
						}
						else
						{
							registryKey3 = registryKey2.OpenSubKey(text, true);
							if (registryKey3 == null && text.Length >= 8)
							{
								string strA = text.Substring(0, 8);
								if (string.Compare(strA, "AppEvent", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(strA, "SecEvent", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(strA, "SysEvent", StringComparison.OrdinalIgnoreCase) == 0)
								{
									throw new ArgumentException(SR.GetString("InvalidCustomerLogName", new object[]
									{
										text
									}));
								}
								string text2 = EventLog.FindSame8FirstCharsLog(registryKey2, text);
								if (text2 != null)
								{
									throw new ArgumentException(SR.GetString("DuplicateLogName", new object[]
									{
										text,
										text2
									}));
								}
							}
							bool flag = registryKey3 == null;
							if (flag)
							{
								if (EventLog.SourceExists(text, machineName, true))
								{
									if (".".Equals(machineName))
									{
										throw new ArgumentException(SR.GetString("LocalLogAlreadyExistsAsSource", new object[]
										{
											text
										}));
									}
									throw new ArgumentException(SR.GetString("LogAlreadyExistsAsSource", new object[]
									{
										text,
										machineName
									}));
								}
								else
								{
									registryKey3 = registryKey2.CreateSubKey(text);
									if (!EventLog.SkipRegPatch)
									{
										registryKey3.SetValue("Sources", new string[]
										{
											text,
											source
										}, RegistryValueKind.MultiString);
									}
									EventLog.SetSpecialLogRegValues(registryKey3, text);
									registryKey4 = registryKey3.CreateSubKey(text);
									EventLog.SetSpecialSourceRegValues(registryKey4, sourceData);
								}
							}
							if (text != source)
							{
								if (!flag)
								{
									EventLog.SetSpecialLogRegValues(registryKey3, text);
									if (!EventLog.SkipRegPatch)
									{
										string[] array = registryKey3.GetValue("Sources") as string[];
										if (array == null)
										{
											registryKey3.SetValue("Sources", new string[]
											{
												text,
												source
											}, RegistryValueKind.MultiString);
										}
										else if (Array.IndexOf<string>(array, source) == -1)
										{
											string[] array2 = new string[array.Length + 1];
											Array.Copy(array, array2, array.Length);
											array2[array.Length] = source;
											registryKey3.SetValue("Sources", array2, RegistryValueKind.MultiString);
										}
									}
								}
								registryKey5 = registryKey3.CreateSubKey(source);
								EventLog.SetSpecialSourceRegValues(registryKey5, sourceData);
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
						if (registryKey3 != null)
						{
							registryKey3.Flush();
							registryKey3.Close();
						}
						if (registryKey4 != null)
						{
							registryKey4.Flush();
							registryKey4.Close();
						}
						if (registryKey5 != null)
						{
							registryKey5.Flush();
							registryKey5.Close();
						}
						CodeAccessPermission.RevertAssert();
					}
				}
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

		// Token: 0x06002DDE RID: 11742 RVA: 0x000CE324 File Offset: 0x000CC524
		public static void Delete(string logName)
		{
			EventLog.Delete(logName, ".");
		}

		// Token: 0x06002DDF RID: 11743 RVA: 0x000CE334 File Offset: 0x000CC534
		public static void Delete(string logName, string machineName)
		{
			if (!SyntaxCheck.CheckMachineName(machineName))
			{
				throw new ArgumentException(SR.GetString("InvalidParameterFormat", new object[]
				{
					"machineName"
				}));
			}
			if (logName == null || logName.Length == 0)
			{
				throw new ArgumentException(SR.GetString("NoLogName"));
			}
			if (!EventLog.ValidLogName(logName, false))
			{
				throw new InvalidOperationException(SR.GetString("BadLogName"));
			}
			EventLogPermission eventLogPermission = new EventLogPermission(EventLogPermissionAccess.Administer, machineName);
			eventLogPermission.Demand();
			SharedUtils.CheckEnvironment();
			PermissionSet permissionSet = EventLog._UnsafeGetAssertPermSet();
			permissionSet.Assert();
			RegistryKey registryKey = null;
			Mutex mutex = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				SharedUtils.EnterMutex("netfxeventlog.1.0", ref mutex);
				try
				{
					registryKey = EventLog.GetEventLogRegKey(machineName, true);
					if (registryKey == null)
					{
						throw new InvalidOperationException(SR.GetString("RegKeyNoAccess", new object[]
						{
							"HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Services\\EventLog",
							machineName
						}));
					}
					using (RegistryKey registryKey2 = registryKey.OpenSubKey(logName))
					{
						if (registryKey2 == null)
						{
							throw new InvalidOperationException(SR.GetString("MissingLog", new object[]
							{
								logName,
								machineName
							}));
						}
						EventLog eventLog = new EventLog(logName, machineName);
						try
						{
							eventLog.Clear();
						}
						finally
						{
							eventLog.Close();
						}
						string text = null;
						try
						{
							text = (string)registryKey2.GetValue("File");
						}
						catch
						{
						}
						if (text != null)
						{
							try
							{
								File.Delete(text);
							}
							catch
							{
							}
						}
					}
					registryKey.DeleteSubKeyTree(logName);
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
			finally
			{
				if (mutex != null)
				{
					mutex.ReleaseMutex();
				}
			}
		}

		// Token: 0x06002DE0 RID: 11744 RVA: 0x000CE4EC File Offset: 0x000CC6EC
		public static void DeleteEventSource(string source)
		{
			EventLog.DeleteEventSource(source, ".");
		}

		// Token: 0x06002DE1 RID: 11745 RVA: 0x000CE4FC File Offset: 0x000CC6FC
		public static void DeleteEventSource(string source, string machineName)
		{
			if (!SyntaxCheck.CheckMachineName(machineName))
			{
				throw new ArgumentException(SR.GetString("InvalidParameter", new object[]
				{
					"machineName",
					machineName
				}));
			}
			EventLogPermission eventLogPermission = new EventLogPermission(EventLogPermissionAccess.Administer, machineName);
			eventLogPermission.Demand();
			SharedUtils.CheckEnvironment();
			PermissionSet permissionSet = EventLog._UnsafeGetAssertPermSet();
			permissionSet.Assert();
			Mutex mutex = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				SharedUtils.EnterMutex("netfxeventlog.1.0", ref mutex);
				RegistryKey registryKey = null;
				RegistryKey registryKey2;
				registryKey = (registryKey2 = EventLog.FindSourceRegistration(source, machineName, true));
				try
				{
					if (registryKey == null)
					{
						if (machineName == null)
						{
							throw new ArgumentException(SR.GetString("LocalSourceNotRegistered", new object[]
							{
								source
							}));
						}
						throw new ArgumentException(SR.GetString("SourceNotRegistered", new object[]
						{
							source,
							machineName,
							"HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Services\\EventLog"
						}));
					}
					else
					{
						string name = registryKey.Name;
						int num = name.LastIndexOf('\\');
						if (string.Compare(name, num + 1, source, 0, name.Length - num, StringComparison.Ordinal) == 0)
						{
							throw new InvalidOperationException(SR.GetString("CannotDeleteEqualSource", new object[]
							{
								source
							}));
						}
					}
				}
				finally
				{
					if (registryKey2 != null)
					{
						((IDisposable)registryKey2).Dispose();
					}
				}
				try
				{
					registryKey = EventLog.FindSourceRegistration(source, machineName, false);
					registryKey.DeleteSubKeyTree(source);
					if (!EventLog.SkipRegPatch)
					{
						string[] array = (string[])registryKey.GetValue("Sources");
						ArrayList arrayList = new ArrayList(array.Length - 1);
						for (int i = 0; i < array.Length; i++)
						{
							if (array[i] != source)
							{
								arrayList.Add(array[i]);
							}
						}
						string[] array2 = new string[arrayList.Count];
						arrayList.CopyTo(array2);
						registryKey.SetValue("Sources", array2, RegistryValueKind.MultiString);
					}
				}
				finally
				{
					if (registryKey != null)
					{
						registryKey.Flush();
						registryKey.Close();
					}
					CodeAccessPermission.RevertAssert();
				}
			}
			finally
			{
				if (mutex != null)
				{
					mutex.ReleaseMutex();
				}
			}
		}

		// Token: 0x06002DE2 RID: 11746 RVA: 0x000CE708 File Offset: 0x000CC908
		protected override void Dispose(bool disposing)
		{
			if (this.m_underlyingEventLog != null)
			{
				this.m_underlyingEventLog.Dispose(disposing);
			}
			base.Dispose(disposing);
		}

		// Token: 0x06002DE3 RID: 11747 RVA: 0x000CE725 File Offset: 0x000CC925
		public void EndInit()
		{
			this.m_underlyingEventLog.EndInit();
		}

		// Token: 0x06002DE4 RID: 11748 RVA: 0x000CE732 File Offset: 0x000CC932
		public static bool Exists(string logName)
		{
			return EventLog.Exists(logName, ".");
		}

		// Token: 0x06002DE5 RID: 11749 RVA: 0x000CE740 File Offset: 0x000CC940
		public static bool Exists(string logName, string machineName)
		{
			if (!SyntaxCheck.CheckMachineName(machineName))
			{
				throw new ArgumentException(SR.GetString("InvalidParameterFormat", new object[]
				{
					"machineName"
				}));
			}
			EventLogPermission eventLogPermission = new EventLogPermission(EventLogPermissionAccess.Administer, machineName);
			eventLogPermission.Demand();
			if (logName == null || logName.Length == 0)
			{
				return false;
			}
			SharedUtils.CheckEnvironment();
			PermissionSet permissionSet = EventLog._UnsafeGetAssertPermSet();
			permissionSet.Assert();
			RegistryKey registryKey = null;
			RegistryKey registryKey2 = null;
			bool result;
			try
			{
				registryKey = EventLog.GetEventLogRegKey(machineName, false);
				if (registryKey == null)
				{
					result = false;
				}
				else
				{
					registryKey2 = registryKey.OpenSubKey(logName, false);
					result = (registryKey2 != null);
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
				CodeAccessPermission.RevertAssert();
			}
			return result;
		}

		// Token: 0x06002DE6 RID: 11750 RVA: 0x000CE7F0 File Offset: 0x000CC9F0
		private static string FindSame8FirstCharsLog(RegistryKey keyParent, string logName)
		{
			string strB = logName.Substring(0, 8);
			foreach (string text in keyParent.GetSubKeyNames())
			{
				if (text.Length >= 8 && string.Compare(text.Substring(0, 8), strB, StringComparison.OrdinalIgnoreCase) == 0)
				{
					return text;
				}
			}
			return null;
		}

		// Token: 0x06002DE7 RID: 11751 RVA: 0x000CE83C File Offset: 0x000CCA3C
		private static RegistryKey FindSourceRegistration(string source, string machineName, bool readOnly)
		{
			return EventLog.FindSourceRegistration(source, machineName, readOnly, false);
		}

		// Token: 0x06002DE8 RID: 11752 RVA: 0x000CE848 File Offset: 0x000CCA48
		private static RegistryKey FindSourceRegistration(string source, string machineName, bool readOnly, bool wantToCreate)
		{
			if (source != null && source.Length != 0)
			{
				SharedUtils.CheckEnvironment();
				PermissionSet permissionSet = EventLog._UnsafeGetAssertPermSet();
				permissionSet.Assert();
				RegistryKey registryKey = null;
				try
				{
					registryKey = EventLog.GetEventLogRegKey(machineName, !readOnly);
					if (registryKey == null)
					{
						return null;
					}
					StringBuilder stringBuilder = null;
					string[] subKeyNames = registryKey.GetSubKeyNames();
					for (int i = 0; i < subKeyNames.Length; i++)
					{
						RegistryKey registryKey2 = null;
						try
						{
							RegistryKey registryKey3 = registryKey.OpenSubKey(subKeyNames[i], !readOnly);
							if (registryKey3 != null)
							{
								registryKey2 = registryKey3.OpenSubKey(source, !readOnly);
								if (registryKey2 != null)
								{
									return registryKey3;
								}
								registryKey3.Close();
							}
						}
						catch (UnauthorizedAccessException)
						{
							if (stringBuilder == null)
							{
								stringBuilder = new StringBuilder(subKeyNames[i]);
							}
							else
							{
								stringBuilder.Append(", ");
								stringBuilder.Append(subKeyNames[i]);
							}
						}
						catch (SecurityException)
						{
							if (stringBuilder == null)
							{
								stringBuilder = new StringBuilder(subKeyNames[i]);
							}
							else
							{
								stringBuilder.Append(", ");
								stringBuilder.Append(subKeyNames[i]);
							}
						}
						finally
						{
							if (registryKey2 != null)
							{
								registryKey2.Close();
							}
						}
					}
					if (stringBuilder != null)
					{
						throw new SecurityException(SR.GetString(wantToCreate ? "SomeLogsInaccessibleToCreate" : "SomeLogsInaccessible", new object[]
						{
							stringBuilder.ToString()
						}));
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
			return null;
		}

		// Token: 0x06002DE9 RID: 11753 RVA: 0x000CE9C0 File Offset: 0x000CCBC0
		public static EventLog[] GetEventLogs()
		{
			return EventLog.GetEventLogs(".");
		}

		// Token: 0x06002DEA RID: 11754 RVA: 0x000CE9CC File Offset: 0x000CCBCC
		public static EventLog[] GetEventLogs(string machineName)
		{
			if (!SyntaxCheck.CheckMachineName(machineName))
			{
				throw new ArgumentException(SR.GetString("InvalidParameter", new object[]
				{
					"machineName",
					machineName
				}));
			}
			EventLogPermission eventLogPermission = new EventLogPermission(EventLogPermissionAccess.Administer, machineName);
			eventLogPermission.Demand();
			SharedUtils.CheckEnvironment();
			string[] array = new string[0];
			PermissionSet permissionSet = EventLog._UnsafeGetAssertPermSet();
			permissionSet.Assert();
			RegistryKey registryKey = null;
			try
			{
				registryKey = EventLog.GetEventLogRegKey(machineName, false);
				if (registryKey == null)
				{
					throw new InvalidOperationException(SR.GetString("RegKeyMissingShort", new object[]
					{
						"SYSTEM\\CurrentControlSet\\Services\\EventLog",
						machineName
					}));
				}
				array = registryKey.GetSubKeyNames();
			}
			finally
			{
				if (registryKey != null)
				{
					registryKey.Close();
				}
				CodeAccessPermission.RevertAssert();
			}
			if (EventLog.s_dontFilterRegKeys || machineName != ".")
			{
				EventLog[] array2 = new EventLog[array.Length];
				for (int i = 0; i < array.Length; i++)
				{
					EventLog eventLog = new EventLog(array[i], machineName);
					array2[i] = eventLog;
				}
				return array2;
			}
			List<EventLog> list = new List<EventLog>(array.Length);
			for (int j = 0; j < array.Length; j++)
			{
				EventLog item = new EventLog(array[j], machineName);
				SafeEventLogReadHandle safeEventLogReadHandle = SafeEventLogReadHandle.OpenEventLog(machineName, array[j]);
				if (!safeEventLogReadHandle.IsInvalid)
				{
					safeEventLogReadHandle.Close();
					list.Add(item);
				}
				else if (Marshal.GetLastWin32Error() != 87)
				{
					list.Add(item);
				}
			}
			return list.ToArray();
		}

		// Token: 0x06002DEB RID: 11755 RVA: 0x000CEB30 File Offset: 0x000CCD30
		private static bool IsWindowsRS5OrUp()
		{
			new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Assert();
			NativeMethods.RTL_OSVERSIONINFOEX rtl_OSVERSIONINFOEX = default(NativeMethods.RTL_OSVERSIONINFOEX);
			rtl_OSVERSIONINFOEX.dwOSVersionInfoSize = (uint)Marshal.SizeOf(rtl_OSVERSIONINFOEX);
			return NativeMethods.RtlGetVersion(out rtl_OSVERSIONINFOEX) == 0 && rtl_OSVERSIONINFOEX.dwPlatformId == 2U && (rtl_OSVERSIONINFOEX.dwMajorVersion > 10U || (rtl_OSVERSIONINFOEX.dwMajorVersion == 10U && (rtl_OSVERSIONINFOEX.dwMinorVersion > 0U || (rtl_OSVERSIONINFOEX.dwMinorVersion == 0U && rtl_OSVERSIONINFOEX.dwBuildNumber >= 17763U))));
		}

		// Token: 0x06002DEC RID: 11756 RVA: 0x000CEBB8 File Offset: 0x000CCDB8
		internal static RegistryKey GetEventLogRegKey(string machine, bool writable)
		{
			RegistryKey registryKey = null;
			try
			{
				if (machine.Equals("."))
				{
					registryKey = Registry.LocalMachine;
				}
				else
				{
					registryKey = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, machine);
				}
				if (registryKey != null)
				{
					return registryKey.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\EventLog", writable);
				}
			}
			finally
			{
				if (registryKey != null)
				{
					registryKey.Close();
				}
			}
			return null;
		}

		// Token: 0x06002DED RID: 11757 RVA: 0x000CEC1C File Offset: 0x000CCE1C
		internal static string GetDllPath(string machineName)
		{
			return Path.Combine(SharedUtils.GetLatestBuildDllDirectory(machineName), "EventLogMessages.dll");
		}

		// Token: 0x06002DEE RID: 11758 RVA: 0x000CEC2E File Offset: 0x000CCE2E
		public static bool SourceExists(string source)
		{
			return EventLog.SourceExists(source, ".");
		}

		// Token: 0x06002DEF RID: 11759 RVA: 0x000CEC3B File Offset: 0x000CCE3B
		public static bool SourceExists(string source, string machineName)
		{
			return EventLog.SourceExists(source, machineName, false);
		}

		// Token: 0x06002DF0 RID: 11760 RVA: 0x000CEC48 File Offset: 0x000CCE48
		internal static bool SourceExists(string source, string machineName, bool wantToCreate)
		{
			if (!SyntaxCheck.CheckMachineName(machineName))
			{
				throw new ArgumentException(SR.GetString("InvalidParameter", new object[]
				{
					"machineName",
					machineName
				}));
			}
			EventLogPermission eventLogPermission = new EventLogPermission(EventLogPermissionAccess.Write, machineName);
			eventLogPermission.Demand();
			bool result;
			using (RegistryKey registryKey = EventLog.FindSourceRegistration(source, machineName, true, wantToCreate))
			{
				result = (registryKey != null);
			}
			return result;
		}

		// Token: 0x06002DF1 RID: 11761 RVA: 0x000CECBC File Offset: 0x000CCEBC
		public static string LogNameFromSourceName(string source, string machineName)
		{
			EventLogPermission eventLogPermission = new EventLogPermission(EventLogPermissionAccess.Administer, machineName);
			eventLogPermission.Demand();
			return EventLog._InternalLogNameFromSourceName(source, machineName);
		}

		// Token: 0x06002DF2 RID: 11762 RVA: 0x000CECE0 File Offset: 0x000CCEE0
		internal static string _InternalLogNameFromSourceName(string source, string machineName)
		{
			string result;
			using (RegistryKey registryKey = EventLog.FindSourceRegistration(source, machineName, true))
			{
				if (registryKey == null)
				{
					result = "";
				}
				else
				{
					string name = registryKey.Name;
					int num = name.LastIndexOf('\\');
					result = name.Substring(num + 1);
				}
			}
			return result;
		}

		// Token: 0x06002DF3 RID: 11763 RVA: 0x000CED38 File Offset: 0x000CCF38
		[ComVisible(false)]
		public void ModifyOverflowPolicy(OverflowAction action, int retentionDays)
		{
			this.m_underlyingEventLog.ModifyOverflowPolicy(action, retentionDays);
		}

		// Token: 0x06002DF4 RID: 11764 RVA: 0x000CED47 File Offset: 0x000CCF47
		[ComVisible(false)]
		public void RegisterDisplayName(string resourceFile, long resourceId)
		{
			this.m_underlyingEventLog.RegisterDisplayName(resourceFile, resourceId);
		}

		// Token: 0x06002DF5 RID: 11765 RVA: 0x000CED58 File Offset: 0x000CCF58
		private static void SetSpecialLogRegValues(RegistryKey logKey, string logName)
		{
			if (logKey.GetValue("MaxSize") == null)
			{
				logKey.SetValue("MaxSize", 524288, RegistryValueKind.DWord);
			}
			if (logKey.GetValue("AutoBackupLogFiles") == null)
			{
				logKey.SetValue("AutoBackupLogFiles", 0, RegistryValueKind.DWord);
			}
			if (!EventLog.SkipRegPatch)
			{
				if (logKey.GetValue("Retention") == null)
				{
					logKey.SetValue("Retention", 604800, RegistryValueKind.DWord);
				}
				if (logKey.GetValue("File") == null)
				{
					string value;
					if (logName.Length > 8)
					{
						value = "%SystemRoot%\\System32\\config\\" + logName.Substring(0, 8) + ".evt";
					}
					else
					{
						value = "%SystemRoot%\\System32\\config\\" + logName + ".evt";
					}
					logKey.SetValue("File", value, RegistryValueKind.ExpandString);
				}
			}
		}

		// Token: 0x06002DF6 RID: 11766 RVA: 0x000CEE20 File Offset: 0x000CD020
		private static void SetSpecialSourceRegValues(RegistryKey sourceLogKey, EventSourceCreationData sourceData)
		{
			if (string.IsNullOrEmpty(sourceData.MessageResourceFile))
			{
				sourceLogKey.SetValue("EventMessageFile", EventLog.GetDllPath(sourceData.MachineName), RegistryValueKind.ExpandString);
			}
			else
			{
				sourceLogKey.SetValue("EventMessageFile", EventLog.FixupPath(sourceData.MessageResourceFile), RegistryValueKind.ExpandString);
			}
			if (!string.IsNullOrEmpty(sourceData.ParameterResourceFile))
			{
				sourceLogKey.SetValue("ParameterMessageFile", EventLog.FixupPath(sourceData.ParameterResourceFile), RegistryValueKind.ExpandString);
			}
			if (!string.IsNullOrEmpty(sourceData.CategoryResourceFile))
			{
				sourceLogKey.SetValue("CategoryMessageFile", EventLog.FixupPath(sourceData.CategoryResourceFile), RegistryValueKind.ExpandString);
				sourceLogKey.SetValue("CategoryCount", sourceData.CategoryCount, RegistryValueKind.DWord);
			}
		}

		// Token: 0x06002DF7 RID: 11767 RVA: 0x000CEEC9 File Offset: 0x000CD0C9
		private static string FixupPath(string path)
		{
			if (path[0] == '%')
			{
				return path;
			}
			return Path.GetFullPath(path);
		}

		// Token: 0x06002DF8 RID: 11768 RVA: 0x000CEEE0 File Offset: 0x000CD0E0
		internal static string TryFormatMessage(SafeLibraryHandle hModule, uint messageNum, string[] insertionStrings)
		{
			if (insertionStrings.Length == 0)
			{
				return EventLog.UnsafeTryFormatMessage(hModule, messageNum, insertionStrings);
			}
			string text = EventLog.UnsafeTryFormatMessage(hModule, messageNum, new string[0]);
			if (text == null)
			{
				return null;
			}
			int num = 0;
			for (int i = 0; i < text.Length; i++)
			{
				if (text[i] == '%' && text.Length > i + 1)
				{
					StringBuilder stringBuilder = new StringBuilder();
					while (i + 1 < text.Length && char.IsDigit(text[i + 1]))
					{
						stringBuilder.Append(text[i + 1]);
						i++;
					}
					i++;
					if (stringBuilder.Length > 0)
					{
						int val = -1;
						if (int.TryParse(stringBuilder.ToString(), NumberStyles.None, CultureInfo.InvariantCulture, out val))
						{
							num = Math.Max(num, val);
						}
					}
				}
			}
			if (num > insertionStrings.Length)
			{
				string[] array = new string[num];
				Array.Copy(insertionStrings, array, insertionStrings.Length);
				for (int j = insertionStrings.Length; j < array.Length; j++)
				{
					array[j] = "%" + (j + 1).ToString();
				}
				insertionStrings = array;
			}
			return EventLog.UnsafeTryFormatMessage(hModule, messageNum, insertionStrings);
		}

		// Token: 0x06002DF9 RID: 11769 RVA: 0x000CEFF4 File Offset: 0x000CD1F4
		internal static string UnsafeTryFormatMessage(SafeLibraryHandle hModule, uint messageNum, string[] insertionStrings)
		{
			string text = null;
			int num = 0;
			StringBuilder stringBuilder = new StringBuilder(1024);
			int num2 = 10240;
			IntPtr[] array = new IntPtr[insertionStrings.Length];
			GCHandle[] array2 = new GCHandle[insertionStrings.Length];
			GCHandle gchandle = GCHandle.Alloc(array, GCHandleType.Pinned);
			if (insertionStrings.Length == 0)
			{
				num2 |= 512;
			}
			try
			{
				for (int i = 0; i < array2.Length; i++)
				{
					array2[i] = GCHandle.Alloc(insertionStrings[i], GCHandleType.Pinned);
					array[i] = array2[i].AddrOfPinnedObject();
				}
				int num3 = 122;
				while (num == 0 && num3 == 122)
				{
					num = SafeNativeMethods.FormatMessage(num2, hModule, messageNum, 0, stringBuilder, stringBuilder.Capacity, array);
					if (num == 0)
					{
						num3 = Marshal.GetLastWin32Error();
						if (num3 == 122)
						{
							stringBuilder.Capacity *= 2;
						}
					}
				}
			}
			catch
			{
				num = 0;
			}
			finally
			{
				for (int j = 0; j < array2.Length; j++)
				{
					if (array2[j].IsAllocated)
					{
						array2[j].Free();
					}
				}
				gchandle.Free();
			}
			if (num > 0)
			{
				text = stringBuilder.ToString();
				if (text.Length > 1 && text[text.Length - 1] == '\n')
				{
					text = text.Substring(0, text.Length - 2);
				}
			}
			return text;
		}

		// Token: 0x06002DFA RID: 11770 RVA: 0x000CF150 File Offset: 0x000CD350
		private static bool CharIsPrintable(char c)
		{
			UnicodeCategory unicodeCategory = char.GetUnicodeCategory(c);
			return unicodeCategory != UnicodeCategory.Control || unicodeCategory == UnicodeCategory.Format || unicodeCategory == UnicodeCategory.LineSeparator || unicodeCategory == UnicodeCategory.ParagraphSeparator || unicodeCategory == UnicodeCategory.OtherNotAssigned;
		}

		// Token: 0x06002DFB RID: 11771 RVA: 0x000CF180 File Offset: 0x000CD380
		internal static bool ValidLogName(string logName, bool ignoreEmpty)
		{
			if (logName.Length == 0 && !ignoreEmpty)
			{
				return false;
			}
			foreach (char c in logName)
			{
				if (!EventLog.CharIsPrintable(c) || c == '\\' || c == '*' || c == '?')
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002DFC RID: 11772 RVA: 0x000CF1CF File Offset: 0x000CD3CF
		public void WriteEntry(string message)
		{
			this.WriteEntry(message, EventLogEntryType.Information, 0, 0, null);
		}

		// Token: 0x06002DFD RID: 11773 RVA: 0x000CF1DC File Offset: 0x000CD3DC
		public static void WriteEntry(string source, string message)
		{
			EventLog.WriteEntry(source, message, EventLogEntryType.Information, 0, 0, null);
		}

		// Token: 0x06002DFE RID: 11774 RVA: 0x000CF1E9 File Offset: 0x000CD3E9
		public void WriteEntry(string message, EventLogEntryType type)
		{
			this.WriteEntry(message, type, 0, 0, null);
		}

		// Token: 0x06002DFF RID: 11775 RVA: 0x000CF1F6 File Offset: 0x000CD3F6
		public static void WriteEntry(string source, string message, EventLogEntryType type)
		{
			EventLog.WriteEntry(source, message, type, 0, 0, null);
		}

		// Token: 0x06002E00 RID: 11776 RVA: 0x000CF203 File Offset: 0x000CD403
		public void WriteEntry(string message, EventLogEntryType type, int eventID)
		{
			this.WriteEntry(message, type, eventID, 0, null);
		}

		// Token: 0x06002E01 RID: 11777 RVA: 0x000CF210 File Offset: 0x000CD410
		public static void WriteEntry(string source, string message, EventLogEntryType type, int eventID)
		{
			EventLog.WriteEntry(source, message, type, eventID, 0, null);
		}

		// Token: 0x06002E02 RID: 11778 RVA: 0x000CF21D File Offset: 0x000CD41D
		public void WriteEntry(string message, EventLogEntryType type, int eventID, short category)
		{
			this.WriteEntry(message, type, eventID, category, null);
		}

		// Token: 0x06002E03 RID: 11779 RVA: 0x000CF22B File Offset: 0x000CD42B
		public static void WriteEntry(string source, string message, EventLogEntryType type, int eventID, short category)
		{
			EventLog.WriteEntry(source, message, type, eventID, category, null);
		}

		// Token: 0x06002E04 RID: 11780 RVA: 0x000CF23C File Offset: 0x000CD43C
		public static void WriteEntry(string source, string message, EventLogEntryType type, int eventID, short category, byte[] rawData)
		{
			using (EventLogInternal eventLogInternal = new EventLogInternal("", ".", EventLog.CheckAndNormalizeSourceName(source)))
			{
				eventLogInternal.WriteEntry(message, type, eventID, category, rawData);
			}
		}

		// Token: 0x06002E05 RID: 11781 RVA: 0x000CF288 File Offset: 0x000CD488
		public void WriteEntry(string message, EventLogEntryType type, int eventID, short category, byte[] rawData)
		{
			this.m_underlyingEventLog.WriteEntry(message, type, eventID, category, rawData);
		}

		// Token: 0x06002E06 RID: 11782 RVA: 0x000CF29C File Offset: 0x000CD49C
		[ComVisible(false)]
		public void WriteEvent(EventInstance instance, params object[] values)
		{
			this.WriteEvent(instance, null, values);
		}

		// Token: 0x06002E07 RID: 11783 RVA: 0x000CF2A7 File Offset: 0x000CD4A7
		[ComVisible(false)]
		public void WriteEvent(EventInstance instance, byte[] data, params object[] values)
		{
			this.m_underlyingEventLog.WriteEvent(instance, data, values);
		}

		// Token: 0x06002E08 RID: 11784 RVA: 0x000CF2B8 File Offset: 0x000CD4B8
		public static void WriteEvent(string source, EventInstance instance, params object[] values)
		{
			using (EventLogInternal eventLogInternal = new EventLogInternal("", ".", EventLog.CheckAndNormalizeSourceName(source)))
			{
				eventLogInternal.WriteEvent(instance, null, values);
			}
		}

		// Token: 0x06002E09 RID: 11785 RVA: 0x000CF300 File Offset: 0x000CD500
		public static void WriteEvent(string source, EventInstance instance, byte[] data, params object[] values)
		{
			using (EventLogInternal eventLogInternal = new EventLogInternal("", ".", EventLog.CheckAndNormalizeSourceName(source)))
			{
				eventLogInternal.WriteEvent(instance, data, values);
			}
		}

		// Token: 0x06002E0A RID: 11786 RVA: 0x000CF348 File Offset: 0x000CD548
		private static string CheckAndNormalizeSourceName(string source)
		{
			if (source == null)
			{
				source = string.Empty;
			}
			if (source.Length + "SYSTEM\\CurrentControlSet\\Services\\EventLog".Length > 254)
			{
				throw new ArgumentException(SR.GetString("ParameterTooLong", new object[]
				{
					"source",
					254 - "SYSTEM\\CurrentControlSet\\Services\\EventLog".Length
				}));
			}
			return source;
		}

		// Token: 0x0400273B RID: 10043
		private const string EventLogKey = "SYSTEM\\CurrentControlSet\\Services\\EventLog";

		// Token: 0x0400273C RID: 10044
		internal const string DllName = "EventLogMessages.dll";

		// Token: 0x0400273D RID: 10045
		private const string eventLogMutexName = "netfxeventlog.1.0";

		// Token: 0x0400273E RID: 10046
		private const int DefaultMaxSize = 524288;

		// Token: 0x0400273F RID: 10047
		private const int DefaultRetention = 604800;

		// Token: 0x04002740 RID: 10048
		private const int SecondsPerDay = 86400;

		// Token: 0x04002741 RID: 10049
		private EventLogInternal m_underlyingEventLog;

		// Token: 0x04002742 RID: 10050
		private static volatile bool s_CheckedOsVersion;

		// Token: 0x04002743 RID: 10051
		private static volatile bool s_SkipRegPatch;

		// Token: 0x04002744 RID: 10052
		private static readonly bool s_dontFilterRegKeys = !EventLog.IsWindowsRS5OrUp() || LocalAppContextSwitches.DisableEventLogRegistryKeysFiltering;
	}
}
