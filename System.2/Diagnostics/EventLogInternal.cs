using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Threading;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace System.Diagnostics
{
	// Token: 0x020004CC RID: 1228
	internal class EventLogInternal : IDisposable, ISupportInitialize
	{
		// Token: 0x17000B1A RID: 2842
		// (get) Token: 0x06002E0C RID: 11788 RVA: 0x000CF3C4 File Offset: 0x000CD5C4
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

		// Token: 0x17000B1B RID: 2843
		// (get) Token: 0x06002E0D RID: 11789 RVA: 0x000CF3F4 File Offset: 0x000CD5F4
		private static object InternalSyncObject
		{
			get
			{
				if (EventLogInternal.s_InternalSyncObject == null)
				{
					object value = new object();
					Interlocked.CompareExchange(ref EventLogInternal.s_InternalSyncObject, value, null);
				}
				return EventLogInternal.s_InternalSyncObject;
			}
		}

		// Token: 0x06002E0E RID: 11790 RVA: 0x000CF420 File Offset: 0x000CD620
		public EventLogInternal() : this("", ".", "", null)
		{
		}

		// Token: 0x06002E0F RID: 11791 RVA: 0x000CF438 File Offset: 0x000CD638
		public EventLogInternal(string logName) : this(logName, ".", "", null)
		{
		}

		// Token: 0x06002E10 RID: 11792 RVA: 0x000CF44C File Offset: 0x000CD64C
		public EventLogInternal(string logName, string machineName) : this(logName, machineName, "", null)
		{
		}

		// Token: 0x06002E11 RID: 11793 RVA: 0x000CF45C File Offset: 0x000CD65C
		public EventLogInternal(string logName, string machineName, string source) : this(logName, machineName, source, null)
		{
		}

		// Token: 0x06002E12 RID: 11794 RVA: 0x000CF468 File Offset: 0x000CD668
		public EventLogInternal(string logName, string machineName, string source, EventLog parent)
		{
			if (logName == null)
			{
				throw new ArgumentNullException("logName");
			}
			if (!EventLogInternal.ValidLogName(logName, true))
			{
				throw new ArgumentException(SR.GetString("BadLogName"));
			}
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
			this.machineName = machineName;
			this.logName = logName;
			this.sourceName = source;
			this.readHandle = null;
			this.writeHandle = null;
			this.boolFlags[2] = true;
			this.parent = parent;
		}

		// Token: 0x17000B1C RID: 2844
		// (get) Token: 0x06002E13 RID: 11795 RVA: 0x000CF51C File Offset: 0x000CD71C
		public EventLogEntryCollection Entries
		{
			get
			{
				string text = this.machineName;
				EventLogPermission eventLogPermission = new EventLogPermission(EventLogPermissionAccess.Administer, text);
				eventLogPermission.Demand();
				if (this.entriesCollection == null)
				{
					this.entriesCollection = new EventLogEntryCollection(this);
				}
				return this.entriesCollection;
			}
		}

		// Token: 0x17000B1D RID: 2845
		// (get) Token: 0x06002E14 RID: 11796 RVA: 0x000CF55C File Offset: 0x000CD75C
		internal int EntryCount
		{
			get
			{
				if (!this.IsOpenForRead)
				{
					this.OpenForRead(this.machineName);
				}
				int result;
				if (!UnsafeNativeMethods.GetNumberOfEventLogRecords(this.readHandle, out result))
				{
					throw SharedUtils.CreateSafeWin32Exception();
				}
				return result;
			}
		}

		// Token: 0x17000B1E RID: 2846
		// (get) Token: 0x06002E15 RID: 11797 RVA: 0x000CF595 File Offset: 0x000CD795
		private bool IsOpen
		{
			get
			{
				return this.readHandle != null || this.writeHandle != null;
			}
		}

		// Token: 0x17000B1F RID: 2847
		// (get) Token: 0x06002E16 RID: 11798 RVA: 0x000CF5AA File Offset: 0x000CD7AA
		private bool IsOpenForRead
		{
			get
			{
				return this.readHandle != null;
			}
		}

		// Token: 0x17000B20 RID: 2848
		// (get) Token: 0x06002E17 RID: 11799 RVA: 0x000CF5B5 File Offset: 0x000CD7B5
		private bool IsOpenForWrite
		{
			get
			{
				return this.writeHandle != null;
			}
		}

		// Token: 0x17000B21 RID: 2849
		// (get) Token: 0x06002E18 RID: 11800 RVA: 0x000CF5C0 File Offset: 0x000CD7C0
		public string LogDisplayName
		{
			get
			{
				if (this.logDisplayName != null)
				{
					return this.logDisplayName;
				}
				string text = this.machineName;
				if (this.GetLogName(text) != null)
				{
					EventLogPermission eventLogPermission = new EventLogPermission(EventLogPermissionAccess.Administer, text);
					eventLogPermission.Demand();
					SharedUtils.CheckEnvironment();
					PermissionSet permissionSet = EventLog._UnsafeGetAssertPermSet();
					permissionSet.Assert();
					RegistryKey registryKey = null;
					try
					{
						registryKey = this.GetLogRegKey(text, false);
						if (registryKey == null)
						{
							throw new InvalidOperationException(SR.GetString("MissingLog", new object[]
							{
								this.GetLogName(text),
								text
							}));
						}
						string text2 = (string)registryKey.GetValue("DisplayNameFile");
						if (text2 == null)
						{
							this.logDisplayName = this.GetLogName(text);
						}
						else
						{
							int messageNum = (int)registryKey.GetValue("DisplayNameID");
							this.logDisplayName = this.FormatMessageWrapper(text2, (uint)messageNum, null);
							if (this.logDisplayName == null)
							{
								this.logDisplayName = this.GetLogName(text);
							}
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
				return this.logDisplayName;
			}
		}

		// Token: 0x17000B22 RID: 2850
		// (get) Token: 0x06002E19 RID: 11801 RVA: 0x000CF6C8 File Offset: 0x000CD8C8
		public string Log
		{
			get
			{
				string currentMachineName = this.machineName;
				if (this.logName == null || this.logName.Length == 0)
				{
					EventLogPermission eventLogPermission = new EventLogPermission(EventLogPermissionAccess.Administer, currentMachineName);
					eventLogPermission.Demand();
				}
				return this.GetLogName(currentMachineName);
			}
		}

		// Token: 0x06002E1A RID: 11802 RVA: 0x000CF708 File Offset: 0x000CD908
		private string GetLogName(string currentMachineName)
		{
			if ((this.logName == null || this.logName.Length == 0) && this.sourceName != null && this.sourceName.Length != 0)
			{
				EventLogPermission eventLogPermission = new EventLogPermission(EventLogPermissionAccess.Write, currentMachineName);
				eventLogPermission.Demand();
				this.logName = EventLog._InternalLogNameFromSourceName(this.sourceName, currentMachineName);
			}
			return this.logName;
		}

		// Token: 0x17000B23 RID: 2851
		// (get) Token: 0x06002E1B RID: 11803 RVA: 0x000CF768 File Offset: 0x000CD968
		public string MachineName
		{
			get
			{
				string result = this.machineName;
				EventLogPermission eventLogPermission = new EventLogPermission(EventLogPermissionAccess.Write, result);
				eventLogPermission.Demand();
				return result;
			}
		}

		// Token: 0x17000B24 RID: 2852
		// (get) Token: 0x06002E1C RID: 11804 RVA: 0x000CF78C File Offset: 0x000CD98C
		// (set) Token: 0x06002E1D RID: 11805 RVA: 0x000CF7D8 File Offset: 0x000CD9D8
		[ComVisible(false)]
		public long MaximumKilobytes
		{
			get
			{
				string currentMachineName = this.machineName;
				EventLogPermission eventLogPermission = new EventLogPermission(EventLogPermissionAccess.Administer, currentMachineName);
				eventLogPermission.Demand();
				object logRegValue = this.GetLogRegValue(currentMachineName, "MaxSize");
				if (logRegValue != null)
				{
					int num = (int)logRegValue;
					return (long)((ulong)(num / 1024));
				}
				return 512L;
			}
			set
			{
				string currentMachineName = this.machineName;
				EventLogPermission eventLogPermission = new EventLogPermission(EventLogPermissionAccess.Administer, currentMachineName);
				eventLogPermission.Demand();
				if (value < 64L || value > 4194240L || value % 64L != 0L)
				{
					throw new ArgumentOutOfRangeException("MaximumKilobytes", SR.GetString("MaximumKilobytesOutOfRange"));
				}
				PermissionSet permissionSet = EventLog._UnsafeGetAssertPermSet();
				permissionSet.Assert();
				long num = value * 1024L;
				int num2 = (int)num;
				using (RegistryKey logRegKey = this.GetLogRegKey(currentMachineName, true))
				{
					logRegKey.SetValue("MaxSize", num2, RegistryValueKind.DWord);
				}
			}
		}

		// Token: 0x17000B25 RID: 2853
		// (get) Token: 0x06002E1E RID: 11806 RVA: 0x000CF87C File Offset: 0x000CDA7C
		internal Hashtable MessageLibraries
		{
			get
			{
				if (this.messageLibraries == null)
				{
					this.messageLibraries = new Hashtable(StringComparer.OrdinalIgnoreCase);
				}
				return this.messageLibraries;
			}
		}

		// Token: 0x17000B26 RID: 2854
		// (get) Token: 0x06002E1F RID: 11807 RVA: 0x000CF89C File Offset: 0x000CDA9C
		[ComVisible(false)]
		public OverflowAction OverflowAction
		{
			get
			{
				string currentMachineName = this.machineName;
				EventLogPermission eventLogPermission = new EventLogPermission(EventLogPermissionAccess.Administer, currentMachineName);
				eventLogPermission.Demand();
				object logRegValue = this.GetLogRegValue(currentMachineName, "Retention");
				if (logRegValue == null)
				{
					return OverflowAction.OverwriteOlder;
				}
				int num = (int)logRegValue;
				if (num == 0)
				{
					return OverflowAction.OverwriteAsNeeded;
				}
				if (num == -1)
				{
					return OverflowAction.DoNotOverwrite;
				}
				return OverflowAction.OverwriteOlder;
			}
		}

		// Token: 0x17000B27 RID: 2855
		// (get) Token: 0x06002E20 RID: 11808 RVA: 0x000CF8E4 File Offset: 0x000CDAE4
		[ComVisible(false)]
		public int MinimumRetentionDays
		{
			get
			{
				string currentMachineName = this.machineName;
				EventLogPermission eventLogPermission = new EventLogPermission(EventLogPermissionAccess.Administer, currentMachineName);
				eventLogPermission.Demand();
				object logRegValue = this.GetLogRegValue(currentMachineName, "Retention");
				if (logRegValue == null)
				{
					return 7;
				}
				int num = (int)logRegValue;
				if (num == 0 || num == -1)
				{
					return num;
				}
				return (int)((double)num / 86400.0);
			}
		}

		// Token: 0x17000B28 RID: 2856
		// (get) Token: 0x06002E21 RID: 11809 RVA: 0x000CF938 File Offset: 0x000CDB38
		// (set) Token: 0x06002E22 RID: 11810 RVA: 0x000CF968 File Offset: 0x000CDB68
		public bool EnableRaisingEvents
		{
			get
			{
				string text = this.machineName;
				EventLogPermission eventLogPermission = new EventLogPermission(EventLogPermissionAccess.Write, text);
				eventLogPermission.Demand();
				return this.boolFlags[8];
			}
			set
			{
				string currentMachineName = this.machineName;
				EventLogPermission eventLogPermission = new EventLogPermission(EventLogPermissionAccess.Write, currentMachineName);
				eventLogPermission.Demand();
				if (this.parent.ComponentDesignMode)
				{
					this.boolFlags[8] = value;
					return;
				}
				if (value)
				{
					this.StartRaisingEvents(currentMachineName, this.GetLogName(currentMachineName));
					return;
				}
				this.StopRaisingEvents(this.GetLogName(currentMachineName));
			}
		}

		// Token: 0x17000B29 RID: 2857
		// (get) Token: 0x06002E23 RID: 11811 RVA: 0x000CF9C8 File Offset: 0x000CDBC8
		private int OldestEntryNumber
		{
			get
			{
				if (!this.IsOpenForRead)
				{
					this.OpenForRead(this.machineName);
				}
				int num;
				if (!UnsafeNativeMethods.GetOldestEventLogRecord(this.readHandle, out num))
				{
					throw SharedUtils.CreateSafeWin32Exception();
				}
				if (num == 0)
				{
					num = 1;
				}
				return num;
			}
		}

		// Token: 0x17000B2A RID: 2858
		// (get) Token: 0x06002E24 RID: 11812 RVA: 0x000CFA06 File Offset: 0x000CDC06
		internal SafeEventLogReadHandle ReadHandle
		{
			get
			{
				if (!this.IsOpenForRead)
				{
					this.OpenForRead(this.machineName);
				}
				return this.readHandle;
			}
		}

		// Token: 0x17000B2B RID: 2859
		// (get) Token: 0x06002E25 RID: 11813 RVA: 0x000CFA24 File Offset: 0x000CDC24
		// (set) Token: 0x06002E26 RID: 11814 RVA: 0x000CFA9E File Offset: 0x000CDC9E
		public ISynchronizeInvoke SynchronizingObject
		{
			[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
			get
			{
				string text = this.machineName;
				EventLogPermission eventLogPermission = new EventLogPermission(EventLogPermissionAccess.Write, text);
				eventLogPermission.Demand();
				if (this.synchronizingObject == null && this.parent.ComponentDesignMode)
				{
					IDesignerHost designerHost = (IDesignerHost)this.parent.ComponentGetService(typeof(IDesignerHost));
					if (designerHost != null)
					{
						object rootComponent = designerHost.RootComponent;
						if (rootComponent != null && rootComponent is ISynchronizeInvoke)
						{
							this.synchronizingObject = (ISynchronizeInvoke)rootComponent;
						}
					}
				}
				return this.synchronizingObject;
			}
			set
			{
				this.synchronizingObject = value;
			}
		}

		// Token: 0x17000B2C RID: 2860
		// (get) Token: 0x06002E27 RID: 11815 RVA: 0x000CFAA8 File Offset: 0x000CDCA8
		public string Source
		{
			get
			{
				string text = this.machineName;
				EventLogPermission eventLogPermission = new EventLogPermission(EventLogPermissionAccess.Write, text);
				eventLogPermission.Demand();
				return this.sourceName;
			}
		}

		// Token: 0x06002E28 RID: 11816 RVA: 0x000CFAD4 File Offset: 0x000CDCD4
		[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
		private static void AddListenerComponent(EventLogInternal component, string compMachineName, string compLogName)
		{
			object internalSyncObject = EventLogInternal.InternalSyncObject;
			lock (internalSyncObject)
			{
				EventLogInternal.LogListeningInfo logListeningInfo = (EventLogInternal.LogListeningInfo)EventLogInternal.listenerInfos[compLogName];
				if (logListeningInfo != null)
				{
					logListeningInfo.listeningComponents.Add(component);
				}
				else
				{
					logListeningInfo = new EventLogInternal.LogListeningInfo();
					logListeningInfo.listeningComponents.Add(component);
					logListeningInfo.handleOwner = new EventLogInternal(compLogName, compMachineName);
					logListeningInfo.waitHandle = new AutoResetEvent(false);
					if (!UnsafeNativeMethods.NotifyChangeEventLog(logListeningInfo.handleOwner.ReadHandle, logListeningInfo.waitHandle.SafeWaitHandle))
					{
						throw new InvalidOperationException(SR.GetString("CantMonitorEventLog"), SharedUtils.CreateSafeWin32Exception());
					}
					logListeningInfo.registeredWaitHandle = ThreadPool.RegisterWaitForSingleObject(logListeningInfo.waitHandle, new WaitOrTimerCallback(EventLogInternal.StaticCompletionCallback), logListeningInfo, -1, false);
					EventLogInternal.listenerInfos[compLogName] = logListeningInfo;
				}
			}
		}

		// Token: 0x14000041 RID: 65
		// (add) Token: 0x06002E29 RID: 11817 RVA: 0x000CFBC0 File Offset: 0x000CDDC0
		// (remove) Token: 0x06002E2A RID: 11818 RVA: 0x000CFBFC File Offset: 0x000CDDFC
		public event EntryWrittenEventHandler EntryWritten
		{
			add
			{
				string text = this.machineName;
				EventLogPermission eventLogPermission = new EventLogPermission(EventLogPermissionAccess.Administer, text);
				eventLogPermission.Demand();
				this.onEntryWrittenHandler = (EntryWrittenEventHandler)Delegate.Combine(this.onEntryWrittenHandler, value);
			}
			remove
			{
				string text = this.machineName;
				EventLogPermission eventLogPermission = new EventLogPermission(EventLogPermissionAccess.Administer, text);
				eventLogPermission.Demand();
				this.onEntryWrittenHandler = (EntryWrittenEventHandler)Delegate.Remove(this.onEntryWrittenHandler, value);
			}
		}

		// Token: 0x06002E2B RID: 11819 RVA: 0x000CFC38 File Offset: 0x000CDE38
		public void BeginInit()
		{
			string currentMachineName = this.machineName;
			EventLogPermission eventLogPermission = new EventLogPermission(EventLogPermissionAccess.Write, currentMachineName);
			eventLogPermission.Demand();
			if (this.boolFlags[4])
			{
				throw new InvalidOperationException(SR.GetString("InitTwice"));
			}
			this.boolFlags[4] = true;
			if (this.boolFlags[8])
			{
				this.StopListening(this.GetLogName(currentMachineName));
			}
		}

		// Token: 0x06002E2C RID: 11820 RVA: 0x000CFCA4 File Offset: 0x000CDEA4
		public void Clear()
		{
			string currentMachineName = this.machineName;
			EventLogPermission eventLogPermission = new EventLogPermission(EventLogPermissionAccess.Administer, currentMachineName);
			eventLogPermission.Demand();
			if (!this.IsOpenForRead)
			{
				this.OpenForRead(currentMachineName);
			}
			if (!UnsafeNativeMethods.ClearEventLog(this.readHandle, NativeMethods.NullHandleRef))
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				if (lastWin32Error != 2)
				{
					throw SharedUtils.CreateSafeWin32Exception();
				}
			}
			this.Reset(currentMachineName);
		}

		// Token: 0x06002E2D RID: 11821 RVA: 0x000CFD01 File Offset: 0x000CDF01
		public void Close()
		{
			this.Close(this.machineName);
		}

		// Token: 0x06002E2E RID: 11822 RVA: 0x000CFD10 File Offset: 0x000CDF10
		private void Close(string currentMachineName)
		{
			EventLogPermission eventLogPermission = new EventLogPermission(EventLogPermissionAccess.Write, currentMachineName);
			eventLogPermission.Demand();
			if (this.readHandle != null)
			{
				try
				{
					this.readHandle.Close();
				}
				catch (IOException)
				{
					throw SharedUtils.CreateSafeWin32Exception();
				}
				this.readHandle = null;
			}
			if (this.writeHandle != null)
			{
				try
				{
					this.writeHandle.Close();
				}
				catch (IOException)
				{
					throw SharedUtils.CreateSafeWin32Exception();
				}
				this.writeHandle = null;
			}
			if (this.boolFlags[8])
			{
				this.StopRaisingEvents(this.GetLogName(currentMachineName));
			}
			if (this.messageLibraries != null)
			{
				foreach (object obj in this.messageLibraries.Values)
				{
					SafeLibraryHandle safeLibraryHandle = (SafeLibraryHandle)obj;
					safeLibraryHandle.Close();
				}
				this.messageLibraries = null;
			}
			this.boolFlags[512] = false;
		}

		// Token: 0x06002E2F RID: 11823 RVA: 0x000CFE18 File Offset: 0x000CE018
		private void CompletionCallback(object context)
		{
			if (this.boolFlags[256])
			{
				return;
			}
			object instanceLockObject = this.InstanceLockObject;
			lock (instanceLockObject)
			{
				if (this.boolFlags[1])
				{
					return;
				}
				this.boolFlags[1] = true;
			}
			int i = this.lastSeenCount;
			try
			{
				int oldestEntryNumber = this.OldestEntryNumber;
				int num = this.EntryCount + oldestEntryNumber;
				if (this.lastSeenCount < oldestEntryNumber || this.lastSeenCount > num)
				{
					this.lastSeenCount = oldestEntryNumber;
					i = this.lastSeenCount;
				}
				while (i < num)
				{
					while (i < num)
					{
						EventLogEntry entryWithOldest = this.GetEntryWithOldest(i);
						if (this.SynchronizingObject != null && this.SynchronizingObject.InvokeRequired)
						{
							this.SynchronizingObject.BeginInvoke(this.onEntryWrittenHandler, new object[]
							{
								this,
								new EntryWrittenEventArgs(entryWithOldest)
							});
						}
						else
						{
							this.onEntryWrittenHandler(this, new EntryWrittenEventArgs(entryWithOldest));
						}
						i++;
					}
					oldestEntryNumber = this.OldestEntryNumber;
					num = this.EntryCount + oldestEntryNumber;
				}
			}
			catch (Exception ex)
			{
			}
			try
			{
				int num2 = this.EntryCount + this.OldestEntryNumber;
				if (i > num2)
				{
					this.lastSeenCount = num2;
				}
				else
				{
					this.lastSeenCount = i;
				}
			}
			catch (Win32Exception ex2)
			{
			}
			object instanceLockObject2 = this.InstanceLockObject;
			lock (instanceLockObject2)
			{
				this.boolFlags[1] = false;
			}
		}

		// Token: 0x06002E30 RID: 11824 RVA: 0x000CFFC0 File Offset: 0x000CE1C0
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002E31 RID: 11825 RVA: 0x000CFFD0 File Offset: 0x000CE1D0
		internal void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					if (this.IsOpen)
					{
						this.Close();
					}
					if (this.readHandle != null)
					{
						this.readHandle.Close();
						this.readHandle = null;
					}
					if (this.writeHandle != null)
					{
						this.writeHandle.Close();
						this.writeHandle = null;
					}
				}
			}
			finally
			{
				this.messageLibraries = null;
				this.boolFlags[256] = true;
			}
		}

		// Token: 0x06002E32 RID: 11826 RVA: 0x000D0050 File Offset: 0x000CE250
		public void EndInit()
		{
			string currentMachineName = this.machineName;
			EventLogPermission eventLogPermission = new EventLogPermission(EventLogPermissionAccess.Write, currentMachineName);
			eventLogPermission.Demand();
			this.boolFlags[4] = false;
			if (this.boolFlags[8])
			{
				this.StartListening(currentMachineName, this.GetLogName(currentMachineName));
			}
		}

		// Token: 0x06002E33 RID: 11827 RVA: 0x000D009C File Offset: 0x000CE29C
		internal string FormatMessageWrapper(string dllNameList, uint messageNum, string[] insertionStrings)
		{
			if (dllNameList == null)
			{
				return null;
			}
			if (insertionStrings == null)
			{
				insertionStrings = new string[0];
			}
			string[] array = dllNameList.Split(new char[]
			{
				';'
			});
			foreach (string text in array)
			{
				if (text != null && text.Length != 0)
				{
					SafeLibraryHandle safeLibraryHandle = null;
					if (this.IsOpen)
					{
						safeLibraryHandle = (this.MessageLibraries[text] as SafeLibraryHandle);
						if (safeLibraryHandle == null || safeLibraryHandle.IsInvalid)
						{
							safeLibraryHandle = SafeLibraryHandle.LoadLibraryEx(text, IntPtr.Zero, 2);
							this.MessageLibraries[text] = safeLibraryHandle;
						}
					}
					else
					{
						safeLibraryHandle = SafeLibraryHandle.LoadLibraryEx(text, IntPtr.Zero, 2);
					}
					if (!safeLibraryHandle.IsInvalid)
					{
						string text2 = null;
						try
						{
							text2 = EventLog.TryFormatMessage(safeLibraryHandle, messageNum, insertionStrings);
						}
						finally
						{
							if (!this.IsOpen)
							{
								safeLibraryHandle.Close();
							}
						}
						if (text2 != null)
						{
							return text2;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06002E34 RID: 11828 RVA: 0x000D0190 File Offset: 0x000CE390
		internal EventLogEntry[] GetAllEntries()
		{
			string currentMachineName = this.machineName;
			if (!this.IsOpenForRead)
			{
				this.OpenForRead(currentMachineName);
			}
			EventLogEntry[] array = new EventLogEntry[this.EntryCount];
			int i = 0;
			int oldestEntryNumber = this.OldestEntryNumber;
			int num = 0;
			while (i < array.Length)
			{
				byte[] array2 = new byte[40000];
				int num2;
				int num3;
				if (!UnsafeNativeMethods.ReadEventLog(this.readHandle, 6, oldestEntryNumber + i, array2, array2.Length, out num2, out num3))
				{
					num = Marshal.GetLastWin32Error();
					if (num != 122 && num != 1503)
					{
						break;
					}
					if (num == 1503)
					{
						this.Reset(currentMachineName);
					}
					else if (num3 > array2.Length)
					{
						array2 = new byte[num3];
					}
					bool flag = UnsafeNativeMethods.ReadEventLog(this.readHandle, 6, oldestEntryNumber + i, array2, array2.Length, out num2, out num3);
					if (!flag)
					{
						break;
					}
					num = 0;
				}
				array[i] = new EventLogEntry(array2, 0, this);
				int num4 = EventLogInternal.IntFrom(array2, 0);
				i++;
				while (num4 < num2 && i < array.Length)
				{
					array[i] = new EventLogEntry(array2, num4, this);
					num4 += EventLogInternal.IntFrom(array2, num4);
					i++;
				}
			}
			if (i == array.Length)
			{
				return array;
			}
			if (num != 0)
			{
				throw new InvalidOperationException(SR.GetString("CantRetrieveEntries"), SharedUtils.CreateSafeWin32Exception(num));
			}
			throw new InvalidOperationException(SR.GetString("CantRetrieveEntries"));
		}

		// Token: 0x06002E35 RID: 11829 RVA: 0x000D02DC File Offset: 0x000CE4DC
		private int GetCachedEntryPos(int entryIndex)
		{
			if (this.cache == null || (this.boolFlags[2] && entryIndex < this.firstCachedEntry) || (!this.boolFlags[2] && entryIndex > this.firstCachedEntry) || this.firstCachedEntry == -1)
			{
				return -1;
			}
			while (this.lastSeenEntry < entryIndex)
			{
				this.lastSeenEntry++;
				if (this.boolFlags[2])
				{
					this.lastSeenPos = this.GetNextEntryPos(this.lastSeenPos);
					if (this.lastSeenPos < this.bytesCached)
					{
						continue;
					}
				}
				else
				{
					this.lastSeenPos = this.GetPreviousEntryPos(this.lastSeenPos);
					if (this.lastSeenPos >= 0)
					{
						continue;
					}
				}
				IL_FE:
				while (this.lastSeenEntry > entryIndex)
				{
					this.lastSeenEntry--;
					if (this.boolFlags[2])
					{
						this.lastSeenPos = this.GetPreviousEntryPos(this.lastSeenPos);
						if (this.lastSeenPos < 0)
						{
							break;
						}
					}
					else
					{
						this.lastSeenPos = this.GetNextEntryPos(this.lastSeenPos);
						if (this.lastSeenPos >= this.bytesCached)
						{
							break;
						}
					}
				}
				if (this.lastSeenPos >= this.bytesCached)
				{
					this.lastSeenPos = this.GetPreviousEntryPos(this.lastSeenPos);
					if (this.boolFlags[2])
					{
						this.lastSeenEntry--;
					}
					else
					{
						this.lastSeenEntry++;
					}
					return -1;
				}
				if (this.lastSeenPos < 0)
				{
					this.lastSeenPos = 0;
					if (this.boolFlags[2])
					{
						this.lastSeenEntry++;
					}
					else
					{
						this.lastSeenEntry--;
					}
					return -1;
				}
				return this.lastSeenPos;
			}
			goto IL_FE;
		}

		// Token: 0x06002E36 RID: 11830 RVA: 0x000D0484 File Offset: 0x000CE684
		internal EventLogEntry GetEntryAt(int index)
		{
			EventLogEntry entryAtNoThrow = this.GetEntryAtNoThrow(index);
			if (entryAtNoThrow == null)
			{
				throw new ArgumentException(SR.GetString("IndexOutOfBounds", new object[]
				{
					index.ToString(CultureInfo.CurrentCulture)
				}));
			}
			return entryAtNoThrow;
		}

		// Token: 0x06002E37 RID: 11831 RVA: 0x000D04C4 File Offset: 0x000CE6C4
		internal EventLogEntry GetEntryAtNoThrow(int index)
		{
			if (!this.IsOpenForRead)
			{
				this.OpenForRead(this.machineName);
			}
			if (index < 0 || index >= this.EntryCount)
			{
				return null;
			}
			index += this.OldestEntryNumber;
			EventLogEntry result = null;
			try
			{
				result = this.GetEntryWithOldest(index);
			}
			catch (InvalidOperationException)
			{
			}
			return result;
		}

		// Token: 0x06002E38 RID: 11832 RVA: 0x000D0520 File Offset: 0x000CE720
		private EventLogEntry GetEntryWithOldest(int index)
		{
			int cachedEntryPos = this.GetCachedEntryPos(index);
			if (cachedEntryPos >= 0)
			{
				return new EventLogEntry(this.cache, cachedEntryPos, this);
			}
			string currentMachineName = this.machineName;
			int dwReadFlags;
			if (this.GetCachedEntryPos(index + 1) < 0)
			{
				dwReadFlags = 6;
				this.boolFlags[2] = true;
			}
			else
			{
				dwReadFlags = 10;
				this.boolFlags[2] = false;
			}
			this.cache = new byte[40000];
			int num;
			int num2;
			bool flag = UnsafeNativeMethods.ReadEventLog(this.readHandle, dwReadFlags, index, this.cache, this.cache.Length, out num, out num2);
			if (!flag)
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				if (lastWin32Error == 122 || lastWin32Error == 1503)
				{
					if (lastWin32Error == 1503)
					{
						byte[] array = this.cache;
						this.Reset(currentMachineName);
						this.cache = array;
					}
					else if (num2 > this.cache.Length)
					{
						this.cache = new byte[num2];
					}
					flag = UnsafeNativeMethods.ReadEventLog(this.readHandle, 6, index, this.cache, this.cache.Length, out num, out num2);
				}
				if (!flag)
				{
					throw new InvalidOperationException(SR.GetString("CantReadLogEntryAt", new object[]
					{
						index.ToString(CultureInfo.CurrentCulture)
					}), SharedUtils.CreateSafeWin32Exception());
				}
			}
			this.bytesCached = num;
			this.firstCachedEntry = index;
			this.lastSeenEntry = index;
			this.lastSeenPos = 0;
			return new EventLogEntry(this.cache, 0, this);
		}

		// Token: 0x06002E39 RID: 11833 RVA: 0x000D0684 File Offset: 0x000CE884
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

		// Token: 0x06002E3A RID: 11834 RVA: 0x000D06E8 File Offset: 0x000CE8E8
		private RegistryKey GetLogRegKey(string currentMachineName, bool writable)
		{
			string text = this.GetLogName(currentMachineName);
			if (!EventLogInternal.ValidLogName(text, false))
			{
				throw new InvalidOperationException(SR.GetString("BadLogName"));
			}
			RegistryKey registryKey = null;
			RegistryKey registryKey2 = null;
			try
			{
				registryKey = EventLogInternal.GetEventLogRegKey(currentMachineName, false);
				if (registryKey == null)
				{
					throw new InvalidOperationException(SR.GetString("RegKeyMissingShort", new object[]
					{
						"SYSTEM\\CurrentControlSet\\Services\\EventLog",
						currentMachineName
					}));
				}
				registryKey2 = registryKey.OpenSubKey(text, writable);
				if (registryKey2 == null)
				{
					throw new InvalidOperationException(SR.GetString("MissingLog", new object[]
					{
						text,
						currentMachineName
					}));
				}
			}
			finally
			{
				if (registryKey != null)
				{
					registryKey.Close();
				}
			}
			return registryKey2;
		}

		// Token: 0x06002E3B RID: 11835 RVA: 0x000D0790 File Offset: 0x000CE990
		private object GetLogRegValue(string currentMachineName, string valuename)
		{
			PermissionSet permissionSet = EventLog._UnsafeGetAssertPermSet();
			permissionSet.Assert();
			RegistryKey registryKey = null;
			object result;
			try
			{
				registryKey = this.GetLogRegKey(currentMachineName, false);
				if (registryKey == null)
				{
					throw new InvalidOperationException(SR.GetString("MissingLog", new object[]
					{
						this.GetLogName(currentMachineName),
						currentMachineName
					}));
				}
				object value = registryKey.GetValue(valuename);
				result = value;
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

		// Token: 0x06002E3C RID: 11836 RVA: 0x000D0808 File Offset: 0x000CEA08
		private int GetNextEntryPos(int pos)
		{
			return pos + EventLogInternal.IntFrom(this.cache, pos);
		}

		// Token: 0x06002E3D RID: 11837 RVA: 0x000D0818 File Offset: 0x000CEA18
		private int GetPreviousEntryPos(int pos)
		{
			return pos - EventLogInternal.IntFrom(this.cache, pos - 4);
		}

		// Token: 0x06002E3E RID: 11838 RVA: 0x000D082A File Offset: 0x000CEA2A
		internal static string GetDllPath(string machineName)
		{
			return Path.Combine(SharedUtils.GetLatestBuildDllDirectory(machineName), "EventLogMessages.dll");
		}

		// Token: 0x06002E3F RID: 11839 RVA: 0x000D083C File Offset: 0x000CEA3C
		private static int IntFrom(byte[] buf, int offset)
		{
			return (-16777216 & (int)buf[offset + 3] << 24) | (16711680 & (int)buf[offset + 2] << 16) | (65280 & (int)buf[offset + 1] << 8) | (int)(byte.MaxValue & buf[offset]);
		}

		// Token: 0x06002E40 RID: 11840 RVA: 0x000D0874 File Offset: 0x000CEA74
		[ComVisible(false)]
		public void ModifyOverflowPolicy(OverflowAction action, int retentionDays)
		{
			string currentMachineName = this.machineName;
			EventLogPermission eventLogPermission = new EventLogPermission(EventLogPermissionAccess.Administer, currentMachineName);
			eventLogPermission.Demand();
			if (action < OverflowAction.DoNotOverwrite || action > OverflowAction.OverwriteOlder)
			{
				throw new InvalidEnumArgumentException("action", (int)action, typeof(OverflowAction));
			}
			long num = (long)action;
			if (action == OverflowAction.OverwriteOlder)
			{
				if (retentionDays < 1 || retentionDays > 365)
				{
					throw new ArgumentOutOfRangeException(SR.GetString("RentionDaysOutOfRange"));
				}
				num = (long)retentionDays * 86400L;
			}
			PermissionSet permissionSet = EventLog._UnsafeGetAssertPermSet();
			permissionSet.Assert();
			using (RegistryKey logRegKey = this.GetLogRegKey(currentMachineName, true))
			{
				logRegKey.SetValue("Retention", num, RegistryValueKind.DWord);
			}
		}

		// Token: 0x06002E41 RID: 11841 RVA: 0x000D092C File Offset: 0x000CEB2C
		private void OpenForRead(string currentMachineName)
		{
			EventLogPermission eventLogPermission = new EventLogPermission(EventLogPermissionAccess.Administer, currentMachineName);
			eventLogPermission.Demand();
			if (this.boolFlags[256])
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
			string text = this.GetLogName(currentMachineName);
			if (text == null || text.Length == 0)
			{
				throw new ArgumentException(SR.GetString("MissingLogProperty"));
			}
			if (!EventLog.Exists(text, currentMachineName))
			{
				throw new InvalidOperationException(SR.GetString("LogDoesNotExists", new object[]
				{
					text,
					currentMachineName
				}));
			}
			SharedUtils.CheckEnvironment();
			this.lastSeenEntry = 0;
			this.lastSeenPos = 0;
			this.bytesCached = 0;
			this.firstCachedEntry = -1;
			SafeEventLogReadHandle safeEventLogReadHandle = SafeEventLogReadHandle.OpenEventLog(currentMachineName, text);
			if (safeEventLogReadHandle.IsInvalid)
			{
				Win32Exception innerException = null;
				if (Marshal.GetLastWin32Error() != 0)
				{
					innerException = SharedUtils.CreateSafeWin32Exception();
				}
				throw new InvalidOperationException(SR.GetString("CantOpenLog", new object[]
				{
					text.ToString(),
					currentMachineName
				}), innerException);
			}
			this.readHandle = safeEventLogReadHandle;
		}

		// Token: 0x06002E42 RID: 11842 RVA: 0x000D0A20 File Offset: 0x000CEC20
		private void OpenForWrite(string currentMachineName)
		{
			if (this.boolFlags[256])
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
			if (this.sourceName == null || this.sourceName.Length == 0)
			{
				throw new ArgumentException(SR.GetString("NeedSourceToOpen"));
			}
			SharedUtils.CheckEnvironment();
			SafeEventLogWriteHandle safeEventLogWriteHandle = SafeEventLogWriteHandle.RegisterEventSource(currentMachineName, this.sourceName);
			if (safeEventLogWriteHandle.IsInvalid)
			{
				Win32Exception innerException = null;
				if (Marshal.GetLastWin32Error() != 0)
				{
					innerException = SharedUtils.CreateSafeWin32Exception();
				}
				throw new InvalidOperationException(SR.GetString("CantOpenLogAccess", new object[]
				{
					this.sourceName
				}), innerException);
			}
			this.writeHandle = safeEventLogWriteHandle;
		}

		// Token: 0x06002E43 RID: 11843 RVA: 0x000D0AC8 File Offset: 0x000CECC8
		[ComVisible(false)]
		public void RegisterDisplayName(string resourceFile, long resourceId)
		{
			string currentMachineName = this.machineName;
			EventLogPermission eventLogPermission = new EventLogPermission(EventLogPermissionAccess.Administer, currentMachineName);
			eventLogPermission.Demand();
			PermissionSet permissionSet = EventLog._UnsafeGetAssertPermSet();
			permissionSet.Assert();
			using (RegistryKey logRegKey = this.GetLogRegKey(currentMachineName, true))
			{
				logRegKey.SetValue("DisplayNameFile", resourceFile, RegistryValueKind.ExpandString);
				logRegKey.SetValue("DisplayNameID", resourceId, RegistryValueKind.DWord);
			}
		}

		// Token: 0x06002E44 RID: 11844 RVA: 0x000D0B3C File Offset: 0x000CED3C
		private void Reset(string currentMachineName)
		{
			bool isOpenForRead = this.IsOpenForRead;
			bool isOpenForWrite = this.IsOpenForWrite;
			bool value = this.boolFlags[8];
			bool flag = this.boolFlags[16];
			this.Close(currentMachineName);
			this.cache = null;
			if (isOpenForRead)
			{
				this.OpenForRead(currentMachineName);
			}
			if (isOpenForWrite)
			{
				this.OpenForWrite(currentMachineName);
			}
			if (flag)
			{
				this.StartListening(currentMachineName, this.GetLogName(currentMachineName));
			}
			this.boolFlags[8] = value;
		}

		// Token: 0x06002E45 RID: 11845 RVA: 0x000D0BB4 File Offset: 0x000CEDB4
		[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
		private static void RemoveListenerComponent(EventLogInternal component, string compLogName)
		{
			object internalSyncObject = EventLogInternal.InternalSyncObject;
			lock (internalSyncObject)
			{
				EventLogInternal.LogListeningInfo logListeningInfo = (EventLogInternal.LogListeningInfo)EventLogInternal.listenerInfos[compLogName];
				logListeningInfo.listeningComponents.Remove(component);
				if (logListeningInfo.listeningComponents.Count == 0)
				{
					logListeningInfo.handleOwner.Dispose();
					logListeningInfo.registeredWaitHandle.Unregister(logListeningInfo.waitHandle);
					logListeningInfo.waitHandle.Close();
					EventLogInternal.listenerInfos[compLogName] = null;
				}
			}
		}

		// Token: 0x06002E46 RID: 11846 RVA: 0x000D0C50 File Offset: 0x000CEE50
		[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
		private void StartListening(string currentMachineName, string currentLogName)
		{
			this.lastSeenCount = this.EntryCount + this.OldestEntryNumber;
			EventLogInternal.AddListenerComponent(this, currentMachineName, currentLogName);
			this.boolFlags[16] = true;
		}

		// Token: 0x06002E47 RID: 11847 RVA: 0x000D0C7B File Offset: 0x000CEE7B
		private void StartRaisingEvents(string currentMachineName, string currentLogName)
		{
			if (!this.boolFlags[4] && !this.boolFlags[8] && !this.parent.ComponentDesignMode)
			{
				this.StartListening(currentMachineName, currentLogName);
			}
			this.boolFlags[8] = true;
		}

		// Token: 0x06002E48 RID: 11848 RVA: 0x000D0CBC File Offset: 0x000CEEBC
		private static void StaticCompletionCallback(object context, bool wasSignaled)
		{
			EventLogInternal.LogListeningInfo logListeningInfo = (EventLogInternal.LogListeningInfo)context;
			if (logListeningInfo == null)
			{
				return;
			}
			object internalSyncObject = EventLogInternal.InternalSyncObject;
			EventLogInternal[] array;
			lock (internalSyncObject)
			{
				array = (EventLogInternal[])logListeningInfo.listeningComponents.ToArray(typeof(EventLogInternal));
			}
			for (int i = 0; i < array.Length; i++)
			{
				try
				{
					if (array[i] != null)
					{
						array[i].CompletionCallback(null);
					}
				}
				catch (ObjectDisposedException)
				{
				}
			}
		}

		// Token: 0x06002E49 RID: 11849 RVA: 0x000D0D50 File Offset: 0x000CEF50
		[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
		private void StopListening(string currentLogName)
		{
			EventLogInternal.RemoveListenerComponent(this, currentLogName);
			this.boolFlags[16] = false;
		}

		// Token: 0x06002E4A RID: 11850 RVA: 0x000D0D67 File Offset: 0x000CEF67
		private void StopRaisingEvents(string currentLogName)
		{
			if (!this.boolFlags[4] && this.boolFlags[8] && !this.parent.ComponentDesignMode)
			{
				this.StopListening(currentLogName);
			}
			this.boolFlags[8] = false;
		}

		// Token: 0x06002E4B RID: 11851 RVA: 0x000D0DA8 File Offset: 0x000CEFA8
		private static bool CharIsPrintable(char c)
		{
			UnicodeCategory unicodeCategory = char.GetUnicodeCategory(c);
			return unicodeCategory != UnicodeCategory.Control || unicodeCategory == UnicodeCategory.Format || unicodeCategory == UnicodeCategory.LineSeparator || unicodeCategory == UnicodeCategory.ParagraphSeparator || unicodeCategory == UnicodeCategory.OtherNotAssigned;
		}

		// Token: 0x06002E4C RID: 11852 RVA: 0x000D0DD8 File Offset: 0x000CEFD8
		internal static bool ValidLogName(string logName, bool ignoreEmpty)
		{
			if (logName.Length == 0 && !ignoreEmpty)
			{
				return false;
			}
			foreach (char c in logName)
			{
				if (!EventLogInternal.CharIsPrintable(c) || c == '\\' || c == '*' || c == '?')
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002E4D RID: 11853 RVA: 0x000D0E28 File Offset: 0x000CF028
		private void VerifyAndCreateSource(string sourceName, string currentMachineName)
		{
			if (this.boolFlags[512])
			{
				return;
			}
			if (!EventLog.SourceExists(sourceName, currentMachineName, true))
			{
				Mutex mutex = null;
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					SharedUtils.EnterMutex("netfxeventlog.1.0", ref mutex);
					if (!EventLog.SourceExists(sourceName, currentMachineName, true))
					{
						if (this.GetLogName(currentMachineName) == null)
						{
							this.logName = "Application";
						}
						EventLog.CreateEventSource(new EventSourceCreationData(sourceName, this.GetLogName(currentMachineName), currentMachineName));
						this.Reset(currentMachineName);
						goto IL_131;
					}
					string text = EventLog.LogNameFromSourceName(sourceName, currentMachineName);
					string text2 = this.GetLogName(currentMachineName);
					if (text != null && text2 != null && string.Compare(text, text2, StringComparison.OrdinalIgnoreCase) != 0)
					{
						throw new ArgumentException(SR.GetString("LogSourceMismatch", new object[]
						{
							this.Source.ToString(),
							text2,
							text
						}));
					}
					goto IL_131;
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
			EventLogPermission eventLogPermission = new EventLogPermission(EventLogPermissionAccess.Write, currentMachineName);
			eventLogPermission.Demand();
			string text3 = EventLog._InternalLogNameFromSourceName(sourceName, currentMachineName);
			string text4 = this.GetLogName(currentMachineName);
			if (text3 != null && text4 != null && string.Compare(text3, text4, StringComparison.OrdinalIgnoreCase) != 0)
			{
				throw new ArgumentException(SR.GetString("LogSourceMismatch", new object[]
				{
					this.Source.ToString(),
					text4,
					text3
				}));
			}
			IL_131:
			this.boolFlags[512] = true;
		}

		// Token: 0x06002E4E RID: 11854 RVA: 0x000D0F88 File Offset: 0x000CF188
		public void WriteEntry(string message)
		{
			this.WriteEntry(message, EventLogEntryType.Information, 0, 0, null);
		}

		// Token: 0x06002E4F RID: 11855 RVA: 0x000D0F95 File Offset: 0x000CF195
		public void WriteEntry(string message, EventLogEntryType type)
		{
			this.WriteEntry(message, type, 0, 0, null);
		}

		// Token: 0x06002E50 RID: 11856 RVA: 0x000D0FA2 File Offset: 0x000CF1A2
		public void WriteEntry(string message, EventLogEntryType type, int eventID)
		{
			this.WriteEntry(message, type, eventID, 0, null);
		}

		// Token: 0x06002E51 RID: 11857 RVA: 0x000D0FAF File Offset: 0x000CF1AF
		public void WriteEntry(string message, EventLogEntryType type, int eventID, short category)
		{
			this.WriteEntry(message, type, eventID, category, null);
		}

		// Token: 0x06002E52 RID: 11858 RVA: 0x000D0FC0 File Offset: 0x000CF1C0
		public void WriteEntry(string message, EventLogEntryType type, int eventID, short category, byte[] rawData)
		{
			if (eventID < 0 || eventID > 65535)
			{
				throw new ArgumentException(SR.GetString("EventID", new object[]
				{
					eventID,
					0,
					65535
				}));
			}
			if (this.Source.Length == 0)
			{
				throw new ArgumentException(SR.GetString("NeedSourceToWrite"));
			}
			if (!Enum.IsDefined(typeof(EventLogEntryType), type))
			{
				throw new InvalidEnumArgumentException("type", (int)type, typeof(EventLogEntryType));
			}
			string currentMachineName = this.machineName;
			if (!this.boolFlags[32])
			{
				EventLogPermission eventLogPermission = new EventLogPermission(EventLogPermissionAccess.Write, currentMachineName);
				eventLogPermission.Demand();
				this.boolFlags[32] = true;
			}
			this.VerifyAndCreateSource(this.sourceName, currentMachineName);
			this.InternalWriteEvent((uint)eventID, (ushort)category, type, new string[]
			{
				message
			}, rawData, currentMachineName);
		}

		// Token: 0x06002E53 RID: 11859 RVA: 0x000D10B0 File Offset: 0x000CF2B0
		[ComVisible(false)]
		public void WriteEvent(EventInstance instance, params object[] values)
		{
			this.WriteEvent(instance, null, values);
		}

		// Token: 0x06002E54 RID: 11860 RVA: 0x000D10BC File Offset: 0x000CF2BC
		[ComVisible(false)]
		public void WriteEvent(EventInstance instance, byte[] data, params object[] values)
		{
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			if (this.Source.Length == 0)
			{
				throw new ArgumentException(SR.GetString("NeedSourceToWrite"));
			}
			string currentMachineName = this.machineName;
			if (!this.boolFlags[32])
			{
				EventLogPermission eventLogPermission = new EventLogPermission(EventLogPermissionAccess.Write, currentMachineName);
				eventLogPermission.Demand();
				this.boolFlags[32] = true;
			}
			this.VerifyAndCreateSource(this.Source, currentMachineName);
			string[] array = null;
			if (values != null)
			{
				array = new string[values.Length];
				for (int i = 0; i < values.Length; i++)
				{
					if (values[i] != null)
					{
						array[i] = values[i].ToString();
					}
					else
					{
						array[i] = string.Empty;
					}
				}
			}
			this.InternalWriteEvent((uint)instance.InstanceId, (ushort)instance.CategoryId, instance.EntryType, array, data, currentMachineName);
		}

		// Token: 0x06002E55 RID: 11861 RVA: 0x000D1188 File Offset: 0x000CF388
		private void InternalWriteEvent(uint eventID, ushort category, EventLogEntryType type, string[] strings, byte[] rawData, string currentMachineName)
		{
			if (strings == null)
			{
				strings = new string[0];
			}
			if (strings.Length >= 256)
			{
				throw new ArgumentException(SR.GetString("TooManyReplacementStrings"));
			}
			for (int i = 0; i < strings.Length; i++)
			{
				if (strings[i] == null)
				{
					strings[i] = string.Empty;
				}
				if (strings[i].Length > 32766)
				{
					throw new ArgumentException(SR.GetString("LogEntryTooLong"));
				}
			}
			if (rawData == null)
			{
				rawData = new byte[0];
			}
			if (this.Source.Length == 0)
			{
				throw new ArgumentException(SR.GetString("NeedSourceToWrite"));
			}
			if (!this.IsOpenForWrite)
			{
				this.OpenForWrite(currentMachineName);
			}
			IntPtr[] array = new IntPtr[strings.Length];
			GCHandle[] array2 = new GCHandle[strings.Length];
			GCHandle gchandle = GCHandle.Alloc(array, GCHandleType.Pinned);
			try
			{
				for (int j = 0; j < strings.Length; j++)
				{
					array2[j] = GCHandle.Alloc(strings[j], GCHandleType.Pinned);
					array[j] = array2[j].AddrOfPinnedObject();
				}
				byte[] userSID = null;
				if (!UnsafeNativeMethods.ReportEvent(this.writeHandle, (short)type, category, eventID, userSID, (short)strings.Length, rawData.Length, new HandleRef(this, gchandle.AddrOfPinnedObject()), rawData))
				{
					throw SharedUtils.CreateSafeWin32Exception();
				}
			}
			finally
			{
				for (int k = 0; k < strings.Length; k++)
				{
					if (array2[k].IsAllocated)
					{
						array2[k].Free();
					}
				}
				gchandle.Free();
			}
		}

		// Token: 0x04002745 RID: 10053
		private EventLogEntryCollection entriesCollection;

		// Token: 0x04002746 RID: 10054
		internal string logName;

		// Token: 0x04002747 RID: 10055
		private int lastSeenCount;

		// Token: 0x04002748 RID: 10056
		internal readonly string machineName;

		// Token: 0x04002749 RID: 10057
		internal EntryWrittenEventHandler onEntryWrittenHandler;

		// Token: 0x0400274A RID: 10058
		private SafeEventLogReadHandle readHandle;

		// Token: 0x0400274B RID: 10059
		internal readonly string sourceName;

		// Token: 0x0400274C RID: 10060
		private SafeEventLogWriteHandle writeHandle;

		// Token: 0x0400274D RID: 10061
		private string logDisplayName;

		// Token: 0x0400274E RID: 10062
		private const int BUF_SIZE = 40000;

		// Token: 0x0400274F RID: 10063
		private int bytesCached;

		// Token: 0x04002750 RID: 10064
		private byte[] cache;

		// Token: 0x04002751 RID: 10065
		private int firstCachedEntry = -1;

		// Token: 0x04002752 RID: 10066
		private int lastSeenEntry;

		// Token: 0x04002753 RID: 10067
		private int lastSeenPos;

		// Token: 0x04002754 RID: 10068
		private ISynchronizeInvoke synchronizingObject;

		// Token: 0x04002755 RID: 10069
		private readonly EventLog parent;

		// Token: 0x04002756 RID: 10070
		private const string EventLogKey = "SYSTEM\\CurrentControlSet\\Services\\EventLog";

		// Token: 0x04002757 RID: 10071
		internal const string DllName = "EventLogMessages.dll";

		// Token: 0x04002758 RID: 10072
		private const string eventLogMutexName = "netfxeventlog.1.0";

		// Token: 0x04002759 RID: 10073
		private const int SecondsPerDay = 86400;

		// Token: 0x0400275A RID: 10074
		private const int DefaultMaxSize = 524288;

		// Token: 0x0400275B RID: 10075
		private const int DefaultRetention = 604800;

		// Token: 0x0400275C RID: 10076
		private const int Flag_notifying = 1;

		// Token: 0x0400275D RID: 10077
		private const int Flag_forwards = 2;

		// Token: 0x0400275E RID: 10078
		private const int Flag_initializing = 4;

		// Token: 0x0400275F RID: 10079
		internal const int Flag_monitoring = 8;

		// Token: 0x04002760 RID: 10080
		private const int Flag_registeredAsListener = 16;

		// Token: 0x04002761 RID: 10081
		private const int Flag_writeGranted = 32;

		// Token: 0x04002762 RID: 10082
		private const int Flag_disposed = 256;

		// Token: 0x04002763 RID: 10083
		private const int Flag_sourceVerified = 512;

		// Token: 0x04002764 RID: 10084
		private BitVector32 boolFlags;

		// Token: 0x04002765 RID: 10085
		private Hashtable messageLibraries;

		// Token: 0x04002766 RID: 10086
		private static readonly Hashtable listenerInfos = new Hashtable(StringComparer.OrdinalIgnoreCase);

		// Token: 0x04002767 RID: 10087
		private object m_InstanceLockObject;

		// Token: 0x04002768 RID: 10088
		private static object s_InternalSyncObject;

		// Token: 0x0200087D RID: 2173
		private class LogListeningInfo
		{
			// Token: 0x04003730 RID: 14128
			public EventLogInternal handleOwner;

			// Token: 0x04003731 RID: 14129
			public RegisteredWaitHandle registeredWaitHandle;

			// Token: 0x04003732 RID: 14130
			public WaitHandle waitHandle;

			// Token: 0x04003733 RID: 14131
			public ArrayList listeningComponents = new ArrayList();
		}
	}
}
