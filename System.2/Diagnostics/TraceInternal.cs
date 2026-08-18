using System;
using System.Collections;
using System.IO;
using System.Security.Permissions;

namespace System.Diagnostics
{
	// Token: 0x020004B1 RID: 1201
	internal static class TraceInternal
	{
		// Token: 0x17000AD1 RID: 2769
		// (get) Token: 0x06002C99 RID: 11417 RVA: 0x000C8264 File Offset: 0x000C6464
		public static TraceListenerCollection Listeners
		{
			get
			{
				TraceInternal.InitializeSettings();
				if (TraceInternal.listeners == null)
				{
					object obj = TraceInternal.critSec;
					lock (obj)
					{
						if (TraceInternal.listeners == null)
						{
							SystemDiagnosticsSection systemDiagnosticsSection = DiagnosticsConfiguration.SystemDiagnosticsSection;
							if (systemDiagnosticsSection != null)
							{
								TraceInternal.listeners = systemDiagnosticsSection.Trace.Listeners.GetRuntimeObject();
							}
							else
							{
								TraceInternal.listeners = new TraceListenerCollection();
								TraceListener traceListener = new DefaultTraceListener();
								traceListener.IndentLevel = TraceInternal.indentLevel;
								traceListener.IndentSize = TraceInternal.indentSize;
								TraceInternal.listeners.Add(traceListener);
							}
						}
					}
				}
				return TraceInternal.listeners;
			}
		}

		// Token: 0x17000AD2 RID: 2770
		// (get) Token: 0x06002C9A RID: 11418 RVA: 0x000C8318 File Offset: 0x000C6518
		internal static string AppName
		{
			get
			{
				if (TraceInternal.appName == null)
				{
					new EnvironmentPermission(EnvironmentPermissionAccess.Read, "Path").Assert();
					TraceInternal.appName = Path.GetFileName(Environment.GetCommandLineArgs()[0]);
				}
				return TraceInternal.appName;
			}
		}

		// Token: 0x17000AD3 RID: 2771
		// (get) Token: 0x06002C9B RID: 11419 RVA: 0x000C834D File Offset: 0x000C654D
		// (set) Token: 0x06002C9C RID: 11420 RVA: 0x000C835B File Offset: 0x000C655B
		public static bool AutoFlush
		{
			get
			{
				TraceInternal.InitializeSettings();
				return TraceInternal.autoFlush;
			}
			set
			{
				TraceInternal.InitializeSettings();
				TraceInternal.autoFlush = value;
			}
		}

		// Token: 0x17000AD4 RID: 2772
		// (get) Token: 0x06002C9D RID: 11421 RVA: 0x000C836A File Offset: 0x000C656A
		// (set) Token: 0x06002C9E RID: 11422 RVA: 0x000C8378 File Offset: 0x000C6578
		public static bool UseGlobalLock
		{
			get
			{
				TraceInternal.InitializeSettings();
				return TraceInternal.useGlobalLock;
			}
			set
			{
				TraceInternal.InitializeSettings();
				TraceInternal.useGlobalLock = value;
			}
		}

		// Token: 0x17000AD5 RID: 2773
		// (get) Token: 0x06002C9F RID: 11423 RVA: 0x000C8387 File Offset: 0x000C6587
		// (set) Token: 0x06002CA0 RID: 11424 RVA: 0x000C8390 File Offset: 0x000C6590
		public static int IndentLevel
		{
			get
			{
				return TraceInternal.indentLevel;
			}
			set
			{
				object obj = TraceInternal.critSec;
				lock (obj)
				{
					if (value < 0)
					{
						value = 0;
					}
					TraceInternal.indentLevel = value;
					if (TraceInternal.listeners != null)
					{
						foreach (object obj2 in TraceInternal.Listeners)
						{
							TraceListener traceListener = (TraceListener)obj2;
							traceListener.IndentLevel = TraceInternal.indentLevel;
						}
					}
				}
			}
		}

		// Token: 0x17000AD6 RID: 2774
		// (get) Token: 0x06002CA1 RID: 11425 RVA: 0x000C8430 File Offset: 0x000C6630
		// (set) Token: 0x06002CA2 RID: 11426 RVA: 0x000C843E File Offset: 0x000C663E
		public static int IndentSize
		{
			get
			{
				TraceInternal.InitializeSettings();
				return TraceInternal.indentSize;
			}
			set
			{
				TraceInternal.InitializeSettings();
				TraceInternal.SetIndentSize(value);
			}
		}

		// Token: 0x06002CA3 RID: 11427 RVA: 0x000C844C File Offset: 0x000C664C
		private static void SetIndentSize(int value)
		{
			object obj = TraceInternal.critSec;
			lock (obj)
			{
				if (value < 0)
				{
					value = 0;
				}
				TraceInternal.indentSize = value;
				if (TraceInternal.listeners != null)
				{
					foreach (object obj2 in TraceInternal.Listeners)
					{
						TraceListener traceListener = (TraceListener)obj2;
						traceListener.IndentSize = TraceInternal.indentSize;
					}
				}
			}
		}

		// Token: 0x06002CA4 RID: 11428 RVA: 0x000C84F0 File Offset: 0x000C66F0
		public static void Indent()
		{
			object obj = TraceInternal.critSec;
			lock (obj)
			{
				TraceInternal.InitializeSettings();
				if (TraceInternal.indentLevel < 2147483647)
				{
					TraceInternal.indentLevel++;
				}
				foreach (object obj2 in TraceInternal.Listeners)
				{
					TraceListener traceListener = (TraceListener)obj2;
					traceListener.IndentLevel = TraceInternal.indentLevel;
				}
			}
		}

		// Token: 0x06002CA5 RID: 11429 RVA: 0x000C8594 File Offset: 0x000C6794
		public static void Unindent()
		{
			object obj = TraceInternal.critSec;
			lock (obj)
			{
				TraceInternal.InitializeSettings();
				if (TraceInternal.indentLevel > 0)
				{
					TraceInternal.indentLevel--;
				}
				foreach (object obj2 in TraceInternal.Listeners)
				{
					TraceListener traceListener = (TraceListener)obj2;
					traceListener.IndentLevel = TraceInternal.indentLevel;
				}
			}
		}

		// Token: 0x06002CA6 RID: 11430 RVA: 0x000C8634 File Offset: 0x000C6834
		public static void Flush()
		{
			if (TraceInternal.listeners != null)
			{
				if (TraceInternal.UseGlobalLock)
				{
					object obj = TraceInternal.critSec;
					lock (obj)
					{
						using (IEnumerator enumerator = TraceInternal.Listeners.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								object obj2 = enumerator.Current;
								TraceListener traceListener = (TraceListener)obj2;
								traceListener.Flush();
							}
							return;
						}
					}
				}
				foreach (object obj3 in TraceInternal.Listeners)
				{
					TraceListener traceListener2 = (TraceListener)obj3;
					if (!traceListener2.IsThreadSafe)
					{
						TraceListener obj4 = traceListener2;
						lock (obj4)
						{
							traceListener2.Flush();
							continue;
						}
					}
					traceListener2.Flush();
				}
			}
		}

		// Token: 0x06002CA7 RID: 11431 RVA: 0x000C8754 File Offset: 0x000C6954
		public static void Close()
		{
			if (TraceInternal.listeners != null)
			{
				object obj = TraceInternal.critSec;
				lock (obj)
				{
					foreach (object obj2 in TraceInternal.Listeners)
					{
						TraceListener traceListener = (TraceListener)obj2;
						traceListener.Close();
					}
				}
			}
		}

		// Token: 0x06002CA8 RID: 11432 RVA: 0x000C87E0 File Offset: 0x000C69E0
		public static void Assert(bool condition)
		{
			if (condition)
			{
				return;
			}
			TraceInternal.Fail(string.Empty);
		}

		// Token: 0x06002CA9 RID: 11433 RVA: 0x000C87F0 File Offset: 0x000C69F0
		public static void Assert(bool condition, string message)
		{
			if (condition)
			{
				return;
			}
			TraceInternal.Fail(message);
		}

		// Token: 0x06002CAA RID: 11434 RVA: 0x000C87FC File Offset: 0x000C69FC
		public static void Assert(bool condition, string message, string detailMessage)
		{
			if (condition)
			{
				return;
			}
			TraceInternal.Fail(message, detailMessage);
		}

		// Token: 0x06002CAB RID: 11435 RVA: 0x000C880C File Offset: 0x000C6A0C
		public static void Fail(string message)
		{
			if (TraceInternal.UseGlobalLock)
			{
				object obj = TraceInternal.critSec;
				lock (obj)
				{
					using (IEnumerator enumerator = TraceInternal.Listeners.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj2 = enumerator.Current;
							TraceListener traceListener = (TraceListener)obj2;
							traceListener.Fail(message);
							if (TraceInternal.AutoFlush)
							{
								traceListener.Flush();
							}
						}
						return;
					}
				}
			}
			foreach (object obj3 in TraceInternal.Listeners)
			{
				TraceListener traceListener2 = (TraceListener)obj3;
				if (!traceListener2.IsThreadSafe)
				{
					TraceListener obj4 = traceListener2;
					lock (obj4)
					{
						traceListener2.Fail(message);
						if (TraceInternal.AutoFlush)
						{
							traceListener2.Flush();
						}
						continue;
					}
				}
				traceListener2.Fail(message);
				if (TraceInternal.AutoFlush)
				{
					traceListener2.Flush();
				}
			}
		}

		// Token: 0x06002CAC RID: 11436 RVA: 0x000C894C File Offset: 0x000C6B4C
		public static void Fail(string message, string detailMessage)
		{
			if (TraceInternal.UseGlobalLock)
			{
				object obj = TraceInternal.critSec;
				lock (obj)
				{
					using (IEnumerator enumerator = TraceInternal.Listeners.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj2 = enumerator.Current;
							TraceListener traceListener = (TraceListener)obj2;
							traceListener.Fail(message, detailMessage);
							if (TraceInternal.AutoFlush)
							{
								traceListener.Flush();
							}
						}
						return;
					}
				}
			}
			foreach (object obj3 in TraceInternal.Listeners)
			{
				TraceListener traceListener2 = (TraceListener)obj3;
				if (!traceListener2.IsThreadSafe)
				{
					TraceListener obj4 = traceListener2;
					lock (obj4)
					{
						traceListener2.Fail(message, detailMessage);
						if (TraceInternal.AutoFlush)
						{
							traceListener2.Flush();
						}
						continue;
					}
				}
				traceListener2.Fail(message, detailMessage);
				if (TraceInternal.AutoFlush)
				{
					traceListener2.Flush();
				}
			}
		}

		// Token: 0x06002CAD RID: 11437 RVA: 0x000C8A90 File Offset: 0x000C6C90
		private static void InitializeSettings()
		{
			if (!TraceInternal.settingsInitialized || (TraceInternal.defaultInitialized && DiagnosticsConfiguration.IsInitialized()))
			{
				object obj = TraceInternal.critSec;
				lock (obj)
				{
					if (!TraceInternal.settingsInitialized || (TraceInternal.defaultInitialized && DiagnosticsConfiguration.IsInitialized()))
					{
						TraceInternal.defaultInitialized = DiagnosticsConfiguration.IsInitializing();
						TraceInternal.SetIndentSize(DiagnosticsConfiguration.IndentSize);
						TraceInternal.autoFlush = DiagnosticsConfiguration.AutoFlush;
						TraceInternal.useGlobalLock = DiagnosticsConfiguration.UseGlobalLock;
						TraceInternal.settingsInitialized = true;
					}
				}
			}
		}

		// Token: 0x06002CAE RID: 11438 RVA: 0x000C8B34 File Offset: 0x000C6D34
		internal static void Refresh()
		{
			object obj = TraceInternal.critSec;
			lock (obj)
			{
				TraceInternal.settingsInitialized = false;
				TraceInternal.listeners = null;
			}
			TraceInternal.InitializeSettings();
		}

		// Token: 0x06002CAF RID: 11439 RVA: 0x000C8B84 File Offset: 0x000C6D84
		public static void TraceEvent(TraceEventType eventType, int id, string format, params object[] args)
		{
			TraceEventCache eventCache = new TraceEventCache();
			if (TraceInternal.UseGlobalLock)
			{
				object obj = TraceInternal.critSec;
				lock (obj)
				{
					if (args == null)
					{
						using (IEnumerator enumerator = TraceInternal.Listeners.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								object obj2 = enumerator.Current;
								TraceListener traceListener = (TraceListener)obj2;
								traceListener.TraceEvent(eventCache, TraceInternal.AppName, eventType, id, format);
								if (TraceInternal.AutoFlush)
								{
									traceListener.Flush();
								}
							}
							return;
						}
					}
					using (IEnumerator enumerator2 = TraceInternal.Listeners.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							object obj3 = enumerator2.Current;
							TraceListener traceListener2 = (TraceListener)obj3;
							traceListener2.TraceEvent(eventCache, TraceInternal.AppName, eventType, id, format, args);
							if (TraceInternal.AutoFlush)
							{
								traceListener2.Flush();
							}
						}
						return;
					}
				}
			}
			if (args == null)
			{
				using (IEnumerator enumerator3 = TraceInternal.Listeners.GetEnumerator())
				{
					while (enumerator3.MoveNext())
					{
						object obj4 = enumerator3.Current;
						TraceListener traceListener3 = (TraceListener)obj4;
						if (!traceListener3.IsThreadSafe)
						{
							TraceListener obj5 = traceListener3;
							lock (obj5)
							{
								traceListener3.TraceEvent(eventCache, TraceInternal.AppName, eventType, id, format);
								if (TraceInternal.AutoFlush)
								{
									traceListener3.Flush();
								}
								continue;
							}
						}
						traceListener3.TraceEvent(eventCache, TraceInternal.AppName, eventType, id, format);
						if (TraceInternal.AutoFlush)
						{
							traceListener3.Flush();
						}
					}
					return;
				}
			}
			foreach (object obj6 in TraceInternal.Listeners)
			{
				TraceListener traceListener4 = (TraceListener)obj6;
				if (!traceListener4.IsThreadSafe)
				{
					TraceListener obj7 = traceListener4;
					lock (obj7)
					{
						traceListener4.TraceEvent(eventCache, TraceInternal.AppName, eventType, id, format, args);
						if (TraceInternal.AutoFlush)
						{
							traceListener4.Flush();
						}
						continue;
					}
				}
				traceListener4.TraceEvent(eventCache, TraceInternal.AppName, eventType, id, format, args);
				if (TraceInternal.AutoFlush)
				{
					traceListener4.Flush();
				}
			}
		}

		// Token: 0x06002CB0 RID: 11440 RVA: 0x000C8E18 File Offset: 0x000C7018
		public static void Write(string message)
		{
			if (TraceInternal.UseGlobalLock)
			{
				object obj = TraceInternal.critSec;
				lock (obj)
				{
					using (IEnumerator enumerator = TraceInternal.Listeners.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj2 = enumerator.Current;
							TraceListener traceListener = (TraceListener)obj2;
							traceListener.Write(message);
							if (TraceInternal.AutoFlush)
							{
								traceListener.Flush();
							}
						}
						return;
					}
				}
			}
			foreach (object obj3 in TraceInternal.Listeners)
			{
				TraceListener traceListener2 = (TraceListener)obj3;
				if (!traceListener2.IsThreadSafe)
				{
					TraceListener obj4 = traceListener2;
					lock (obj4)
					{
						traceListener2.Write(message);
						if (TraceInternal.AutoFlush)
						{
							traceListener2.Flush();
						}
						continue;
					}
				}
				traceListener2.Write(message);
				if (TraceInternal.AutoFlush)
				{
					traceListener2.Flush();
				}
			}
		}

		// Token: 0x06002CB1 RID: 11441 RVA: 0x000C8F58 File Offset: 0x000C7158
		public static void Write(object value)
		{
			if (TraceInternal.UseGlobalLock)
			{
				object obj = TraceInternal.critSec;
				lock (obj)
				{
					using (IEnumerator enumerator = TraceInternal.Listeners.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj2 = enumerator.Current;
							TraceListener traceListener = (TraceListener)obj2;
							traceListener.Write(value);
							if (TraceInternal.AutoFlush)
							{
								traceListener.Flush();
							}
						}
						return;
					}
				}
			}
			foreach (object obj3 in TraceInternal.Listeners)
			{
				TraceListener traceListener2 = (TraceListener)obj3;
				if (!traceListener2.IsThreadSafe)
				{
					TraceListener obj4 = traceListener2;
					lock (obj4)
					{
						traceListener2.Write(value);
						if (TraceInternal.AutoFlush)
						{
							traceListener2.Flush();
						}
						continue;
					}
				}
				traceListener2.Write(value);
				if (TraceInternal.AutoFlush)
				{
					traceListener2.Flush();
				}
			}
		}

		// Token: 0x06002CB2 RID: 11442 RVA: 0x000C9098 File Offset: 0x000C7298
		public static void Write(string message, string category)
		{
			if (TraceInternal.UseGlobalLock)
			{
				object obj = TraceInternal.critSec;
				lock (obj)
				{
					using (IEnumerator enumerator = TraceInternal.Listeners.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj2 = enumerator.Current;
							TraceListener traceListener = (TraceListener)obj2;
							traceListener.Write(message, category);
							if (TraceInternal.AutoFlush)
							{
								traceListener.Flush();
							}
						}
						return;
					}
				}
			}
			foreach (object obj3 in TraceInternal.Listeners)
			{
				TraceListener traceListener2 = (TraceListener)obj3;
				if (!traceListener2.IsThreadSafe)
				{
					TraceListener obj4 = traceListener2;
					lock (obj4)
					{
						traceListener2.Write(message, category);
						if (TraceInternal.AutoFlush)
						{
							traceListener2.Flush();
						}
						continue;
					}
				}
				traceListener2.Write(message, category);
				if (TraceInternal.AutoFlush)
				{
					traceListener2.Flush();
				}
			}
		}

		// Token: 0x06002CB3 RID: 11443 RVA: 0x000C91DC File Offset: 0x000C73DC
		public static void Write(object value, string category)
		{
			if (TraceInternal.UseGlobalLock)
			{
				object obj = TraceInternal.critSec;
				lock (obj)
				{
					using (IEnumerator enumerator = TraceInternal.Listeners.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj2 = enumerator.Current;
							TraceListener traceListener = (TraceListener)obj2;
							traceListener.Write(value, category);
							if (TraceInternal.AutoFlush)
							{
								traceListener.Flush();
							}
						}
						return;
					}
				}
			}
			foreach (object obj3 in TraceInternal.Listeners)
			{
				TraceListener traceListener2 = (TraceListener)obj3;
				if (!traceListener2.IsThreadSafe)
				{
					TraceListener obj4 = traceListener2;
					lock (obj4)
					{
						traceListener2.Write(value, category);
						if (TraceInternal.AutoFlush)
						{
							traceListener2.Flush();
						}
						continue;
					}
				}
				traceListener2.Write(value, category);
				if (TraceInternal.AutoFlush)
				{
					traceListener2.Flush();
				}
			}
		}

		// Token: 0x06002CB4 RID: 11444 RVA: 0x000C9320 File Offset: 0x000C7520
		public static void WriteLine(string message)
		{
			if (TraceInternal.UseGlobalLock)
			{
				object obj = TraceInternal.critSec;
				lock (obj)
				{
					using (IEnumerator enumerator = TraceInternal.Listeners.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj2 = enumerator.Current;
							TraceListener traceListener = (TraceListener)obj2;
							traceListener.WriteLine(message);
							if (TraceInternal.AutoFlush)
							{
								traceListener.Flush();
							}
						}
						return;
					}
				}
			}
			foreach (object obj3 in TraceInternal.Listeners)
			{
				TraceListener traceListener2 = (TraceListener)obj3;
				if (!traceListener2.IsThreadSafe)
				{
					TraceListener obj4 = traceListener2;
					lock (obj4)
					{
						traceListener2.WriteLine(message);
						if (TraceInternal.AutoFlush)
						{
							traceListener2.Flush();
						}
						continue;
					}
				}
				traceListener2.WriteLine(message);
				if (TraceInternal.AutoFlush)
				{
					traceListener2.Flush();
				}
			}
		}

		// Token: 0x06002CB5 RID: 11445 RVA: 0x000C9460 File Offset: 0x000C7660
		public static void WriteLine(object value)
		{
			if (TraceInternal.UseGlobalLock)
			{
				object obj = TraceInternal.critSec;
				lock (obj)
				{
					using (IEnumerator enumerator = TraceInternal.Listeners.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj2 = enumerator.Current;
							TraceListener traceListener = (TraceListener)obj2;
							traceListener.WriteLine(value);
							if (TraceInternal.AutoFlush)
							{
								traceListener.Flush();
							}
						}
						return;
					}
				}
			}
			foreach (object obj3 in TraceInternal.Listeners)
			{
				TraceListener traceListener2 = (TraceListener)obj3;
				if (!traceListener2.IsThreadSafe)
				{
					TraceListener obj4 = traceListener2;
					lock (obj4)
					{
						traceListener2.WriteLine(value);
						if (TraceInternal.AutoFlush)
						{
							traceListener2.Flush();
						}
						continue;
					}
				}
				traceListener2.WriteLine(value);
				if (TraceInternal.AutoFlush)
				{
					traceListener2.Flush();
				}
			}
		}

		// Token: 0x06002CB6 RID: 11446 RVA: 0x000C95A0 File Offset: 0x000C77A0
		public static void WriteLine(string message, string category)
		{
			if (TraceInternal.UseGlobalLock)
			{
				object obj = TraceInternal.critSec;
				lock (obj)
				{
					using (IEnumerator enumerator = TraceInternal.Listeners.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj2 = enumerator.Current;
							TraceListener traceListener = (TraceListener)obj2;
							traceListener.WriteLine(message, category);
							if (TraceInternal.AutoFlush)
							{
								traceListener.Flush();
							}
						}
						return;
					}
				}
			}
			foreach (object obj3 in TraceInternal.Listeners)
			{
				TraceListener traceListener2 = (TraceListener)obj3;
				if (!traceListener2.IsThreadSafe)
				{
					TraceListener obj4 = traceListener2;
					lock (obj4)
					{
						traceListener2.WriteLine(message, category);
						if (TraceInternal.AutoFlush)
						{
							traceListener2.Flush();
						}
						continue;
					}
				}
				traceListener2.WriteLine(message, category);
				if (TraceInternal.AutoFlush)
				{
					traceListener2.Flush();
				}
			}
		}

		// Token: 0x06002CB7 RID: 11447 RVA: 0x000C96E4 File Offset: 0x000C78E4
		public static void WriteLine(object value, string category)
		{
			if (TraceInternal.UseGlobalLock)
			{
				object obj = TraceInternal.critSec;
				lock (obj)
				{
					using (IEnumerator enumerator = TraceInternal.Listeners.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj2 = enumerator.Current;
							TraceListener traceListener = (TraceListener)obj2;
							traceListener.WriteLine(value, category);
							if (TraceInternal.AutoFlush)
							{
								traceListener.Flush();
							}
						}
						return;
					}
				}
			}
			foreach (object obj3 in TraceInternal.Listeners)
			{
				TraceListener traceListener2 = (TraceListener)obj3;
				if (!traceListener2.IsThreadSafe)
				{
					TraceListener obj4 = traceListener2;
					lock (obj4)
					{
						traceListener2.WriteLine(value, category);
						if (TraceInternal.AutoFlush)
						{
							traceListener2.Flush();
						}
						continue;
					}
				}
				traceListener2.WriteLine(value, category);
				if (TraceInternal.AutoFlush)
				{
					traceListener2.Flush();
				}
			}
		}

		// Token: 0x06002CB8 RID: 11448 RVA: 0x000C9828 File Offset: 0x000C7A28
		public static void WriteIf(bool condition, string message)
		{
			if (condition)
			{
				TraceInternal.Write(message);
			}
		}

		// Token: 0x06002CB9 RID: 11449 RVA: 0x000C9833 File Offset: 0x000C7A33
		public static void WriteIf(bool condition, object value)
		{
			if (condition)
			{
				TraceInternal.Write(value);
			}
		}

		// Token: 0x06002CBA RID: 11450 RVA: 0x000C983E File Offset: 0x000C7A3E
		public static void WriteIf(bool condition, string message, string category)
		{
			if (condition)
			{
				TraceInternal.Write(message, category);
			}
		}

		// Token: 0x06002CBB RID: 11451 RVA: 0x000C984A File Offset: 0x000C7A4A
		public static void WriteIf(bool condition, object value, string category)
		{
			if (condition)
			{
				TraceInternal.Write(value, category);
			}
		}

		// Token: 0x06002CBC RID: 11452 RVA: 0x000C9856 File Offset: 0x000C7A56
		public static void WriteLineIf(bool condition, string message)
		{
			if (condition)
			{
				TraceInternal.WriteLine(message);
			}
		}

		// Token: 0x06002CBD RID: 11453 RVA: 0x000C9861 File Offset: 0x000C7A61
		public static void WriteLineIf(bool condition, object value)
		{
			if (condition)
			{
				TraceInternal.WriteLine(value);
			}
		}

		// Token: 0x06002CBE RID: 11454 RVA: 0x000C986C File Offset: 0x000C7A6C
		public static void WriteLineIf(bool condition, string message, string category)
		{
			if (condition)
			{
				TraceInternal.WriteLine(message, category);
			}
		}

		// Token: 0x06002CBF RID: 11455 RVA: 0x000C9878 File Offset: 0x000C7A78
		public static void WriteLineIf(bool condition, object value, string category)
		{
			if (condition)
			{
				TraceInternal.WriteLine(value, category);
			}
		}

		// Token: 0x040026E2 RID: 9954
		private static volatile string appName = null;

		// Token: 0x040026E3 RID: 9955
		private static volatile TraceListenerCollection listeners;

		// Token: 0x040026E4 RID: 9956
		private static volatile bool autoFlush;

		// Token: 0x040026E5 RID: 9957
		private static volatile bool useGlobalLock;

		// Token: 0x040026E6 RID: 9958
		[ThreadStatic]
		private static int indentLevel;

		// Token: 0x040026E7 RID: 9959
		private static volatile int indentSize;

		// Token: 0x040026E8 RID: 9960
		private static volatile bool settingsInitialized;

		// Token: 0x040026E9 RID: 9961
		private static volatile bool defaultInitialized;

		// Token: 0x040026EA RID: 9962
		internal static readonly object critSec = new object();
	}
}
