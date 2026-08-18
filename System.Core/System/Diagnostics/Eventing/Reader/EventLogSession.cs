using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using Microsoft.Win32;

namespace System.Diagnostics.Eventing.Reader
{
	// Token: 0x020002BF RID: 703
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public class EventLogSession : IDisposable
	{
		// Token: 0x06001977 RID: 6519 RVA: 0x0005CB38 File Offset: 0x0005AD38
		[SecuritySafeCritical]
		internal void SetupSystemContext()
		{
			EventLogPermissionHolder.GetEventLogPermission().Demand();
			if (!this.renderContextHandleSystem.IsInvalid)
			{
				return;
			}
			object obj = this.syncObject;
			lock (obj)
			{
				if (this.renderContextHandleSystem.IsInvalid)
				{
					this.renderContextHandleSystem = NativeWrapper.EvtCreateRenderContext(0, null, UnsafeNativeMethods.EvtRenderContextFlags.EvtRenderContextSystem);
				}
			}
		}

		// Token: 0x06001978 RID: 6520 RVA: 0x0005CBA8 File Offset: 0x0005ADA8
		[SecuritySafeCritical]
		internal void SetupUserContext()
		{
			EventLogPermissionHolder.GetEventLogPermission().Demand();
			object obj = this.syncObject;
			lock (obj)
			{
				if (this.renderContextHandleUser.IsInvalid)
				{
					this.renderContextHandleUser = NativeWrapper.EvtCreateRenderContext(0, null, UnsafeNativeMethods.EvtRenderContextFlags.EvtRenderContextUser);
				}
			}
		}

		// Token: 0x06001979 RID: 6521 RVA: 0x0005CC08 File Offset: 0x0005AE08
		[SecurityCritical]
		public EventLogSession()
		{
			EventLogPermissionHolder.GetEventLogPermission().Demand();
			this.syncObject = new object();
		}

		// Token: 0x0600197A RID: 6522 RVA: 0x0005CC46 File Offset: 0x0005AE46
		public EventLogSession(string server) : this(server, null, null, null, SessionAuthentication.Default)
		{
		}

		// Token: 0x0600197B RID: 6523 RVA: 0x0005CC54 File Offset: 0x0005AE54
		[SecurityCritical]
		public EventLogSession(string server, string domain, string user, SecureString password, SessionAuthentication logOnType)
		{
			EventLogPermissionHolder.GetEventLogPermission().Demand();
			if (server == null)
			{
				server = "localhost";
			}
			this.syncObject = new object();
			this.server = server;
			this.domain = domain;
			this.user = user;
			this.logOnType = logOnType;
			UnsafeNativeMethods.EvtRpcLogin evtRpcLogin = default(UnsafeNativeMethods.EvtRpcLogin);
			evtRpcLogin.Server = this.server;
			evtRpcLogin.User = this.user;
			evtRpcLogin.Domain = this.domain;
			evtRpcLogin.Flags = (int)this.logOnType;
			evtRpcLogin.Password = CoTaskMemUnicodeSafeHandle.Zero;
			try
			{
				if (password != null)
				{
					evtRpcLogin.Password.SetMemory(Marshal.SecureStringToCoTaskMemUnicode(password));
				}
				this.handle = NativeWrapper.EvtOpenSession(UnsafeNativeMethods.EvtLoginClass.EvtRpcLogin, ref evtRpcLogin, 0, 0);
			}
			finally
			{
				evtRpcLogin.Password.Close();
			}
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x0600197C RID: 6524 RVA: 0x0005CD50 File Offset: 0x0005AF50
		internal EventLogHandle Handle
		{
			get
			{
				return this.handle;
			}
		}

		// Token: 0x0600197D RID: 6525 RVA: 0x0005CD58 File Offset: 0x0005AF58
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600197E RID: 6526 RVA: 0x0005CD68 File Offset: 0x0005AF68
		[SecuritySafeCritical]
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this == EventLogSession.globalSession)
				{
					throw new InvalidOperationException();
				}
				EventLogPermissionHolder.GetEventLogPermission().Demand();
			}
			if (this.renderContextHandleSystem != null && !this.renderContextHandleSystem.IsInvalid)
			{
				this.renderContextHandleSystem.Dispose();
			}
			if (this.renderContextHandleUser != null && !this.renderContextHandleUser.IsInvalid)
			{
				this.renderContextHandleUser.Dispose();
			}
			if (this.handle != null && !this.handle.IsInvalid)
			{
				this.handle.Dispose();
			}
		}

		// Token: 0x0600197F RID: 6527 RVA: 0x0005CDF0 File Offset: 0x0005AFF0
		public void CancelCurrentOperations()
		{
			NativeWrapper.EvtCancel(this.handle);
		}

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06001980 RID: 6528 RVA: 0x0005CDFD File Offset: 0x0005AFFD
		public static EventLogSession GlobalSession
		{
			get
			{
				return EventLogSession.globalSession;
			}
		}

		// Token: 0x06001981 RID: 6529 RVA: 0x0005CE04 File Offset: 0x0005B004
		[SecurityCritical]
		public IEnumerable<string> GetProviderNames()
		{
			EventLogPermissionHolder.GetEventLogPermission().Demand();
			List<string> list = new List<string>(100);
			IEnumerable<string> result;
			using (EventLogHandle eventLogHandle = NativeWrapper.EvtOpenProviderEnum(this.Handle, 0))
			{
				bool flag = false;
				do
				{
					string item = NativeWrapper.EvtNextPublisherId(eventLogHandle, ref flag);
					if (!flag)
					{
						list.Add(item);
					}
				}
				while (!flag);
				result = list;
			}
			return result;
		}

		// Token: 0x06001982 RID: 6530 RVA: 0x0005CE6C File Offset: 0x0005B06C
		[SecurityCritical]
		public IEnumerable<string> GetLogNames()
		{
			EventLogPermissionHolder.GetEventLogPermission().Demand();
			List<string> list = new List<string>(100);
			IEnumerable<string> result;
			using (EventLogHandle eventLogHandle = NativeWrapper.EvtOpenChannelEnum(this.Handle, 0))
			{
				bool flag = false;
				do
				{
					string item = NativeWrapper.EvtNextChannelPath(eventLogHandle, ref flag);
					if (!flag)
					{
						list.Add(item);
					}
				}
				while (!flag);
				result = list;
			}
			return result;
		}

		// Token: 0x06001983 RID: 6531 RVA: 0x0005CED4 File Offset: 0x0005B0D4
		public EventLogInformation GetLogInformation(string logName, PathType pathType)
		{
			if (logName == null)
			{
				throw new ArgumentNullException("logName");
			}
			return new EventLogInformation(this, logName, pathType);
		}

		// Token: 0x06001984 RID: 6532 RVA: 0x0005CEEC File Offset: 0x0005B0EC
		public void ExportLog(string path, PathType pathType, string query, string targetFilePath)
		{
			this.ExportLog(path, pathType, query, targetFilePath, false);
		}

		// Token: 0x06001985 RID: 6533 RVA: 0x0005CEFC File Offset: 0x0005B0FC
		public void ExportLog(string path, PathType pathType, string query, string targetFilePath, bool tolerateQueryErrors)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (targetFilePath == null)
			{
				throw new ArgumentNullException("targetFilePath");
			}
			UnsafeNativeMethods.EvtExportLogFlags evtExportLogFlags;
			if (pathType != PathType.LogName)
			{
				if (pathType != PathType.FilePath)
				{
					throw new ArgumentOutOfRangeException("pathType");
				}
				evtExportLogFlags = UnsafeNativeMethods.EvtExportLogFlags.EvtExportLogFilePath;
			}
			else
			{
				evtExportLogFlags = UnsafeNativeMethods.EvtExportLogFlags.EvtExportLogChannelPath;
			}
			if (!tolerateQueryErrors)
			{
				NativeWrapper.EvtExportLog(this.Handle, path, query, targetFilePath, (int)evtExportLogFlags);
				return;
			}
			NativeWrapper.EvtExportLog(this.Handle, path, query, targetFilePath, (int)(evtExportLogFlags | UnsafeNativeMethods.EvtExportLogFlags.EvtExportLogTolerateQueryErrors));
		}

		// Token: 0x06001986 RID: 6534 RVA: 0x0005CF6E File Offset: 0x0005B16E
		public void ExportLogAndMessages(string path, PathType pathType, string query, string targetFilePath)
		{
			this.ExportLogAndMessages(path, pathType, query, targetFilePath, false, CultureInfo.CurrentCulture);
		}

		// Token: 0x06001987 RID: 6535 RVA: 0x0005CF81 File Offset: 0x0005B181
		public void ExportLogAndMessages(string path, PathType pathType, string query, string targetFilePath, bool tolerateQueryErrors, CultureInfo targetCultureInfo)
		{
			if (targetCultureInfo == null)
			{
				targetCultureInfo = CultureInfo.CurrentCulture;
			}
			this.ExportLog(path, pathType, query, targetFilePath, tolerateQueryErrors);
			NativeWrapper.EvtArchiveExportedLog(this.Handle, targetFilePath, targetCultureInfo.LCID, 0);
		}

		// Token: 0x06001988 RID: 6536 RVA: 0x0005CFB0 File Offset: 0x0005B1B0
		public void ClearLog(string logName)
		{
			this.ClearLog(logName, null);
		}

		// Token: 0x06001989 RID: 6537 RVA: 0x0005CFBA File Offset: 0x0005B1BA
		public void ClearLog(string logName, string backupPath)
		{
			if (logName == null)
			{
				throw new ArgumentNullException("logName");
			}
			NativeWrapper.EvtClearLog(this.Handle, logName, backupPath, 0);
		}

		// Token: 0x04000C77 RID: 3191
		internal EventLogHandle renderContextHandleSystem = EventLogHandle.Zero;

		// Token: 0x04000C78 RID: 3192
		internal EventLogHandle renderContextHandleUser = EventLogHandle.Zero;

		// Token: 0x04000C79 RID: 3193
		private object syncObject;

		// Token: 0x04000C7A RID: 3194
		private string server;

		// Token: 0x04000C7B RID: 3195
		private string user;

		// Token: 0x04000C7C RID: 3196
		private string domain;

		// Token: 0x04000C7D RID: 3197
		private SessionAuthentication logOnType;

		// Token: 0x04000C7E RID: 3198
		private EventLogHandle handle = EventLogHandle.Zero;

		// Token: 0x04000C7F RID: 3199
		private static EventLogSession globalSession = new EventLogSession();
	}
}
