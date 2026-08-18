using System;
using System.Security.Permissions;

namespace System.Diagnostics
{
	// Token: 0x020004AD RID: 1197
	public sealed class Trace
	{
		// Token: 0x06002C5D RID: 11357 RVA: 0x000C7F04 File Offset: 0x000C6104
		private Trace()
		{
		}

		// Token: 0x17000AC4 RID: 2756
		// (get) Token: 0x06002C5E RID: 11358 RVA: 0x000C7F0C File Offset: 0x000C610C
		public static TraceListenerCollection Listeners
		{
			[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
			get
			{
				new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
				return TraceInternal.Listeners;
			}
		}

		// Token: 0x17000AC5 RID: 2757
		// (get) Token: 0x06002C5F RID: 11359 RVA: 0x000C7F1E File Offset: 0x000C611E
		// (set) Token: 0x06002C60 RID: 11360 RVA: 0x000C7F25 File Offset: 0x000C6125
		public static bool AutoFlush
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				return TraceInternal.AutoFlush;
			}
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			set
			{
				TraceInternal.AutoFlush = value;
			}
		}

		// Token: 0x17000AC6 RID: 2758
		// (get) Token: 0x06002C61 RID: 11361 RVA: 0x000C7F2D File Offset: 0x000C612D
		// (set) Token: 0x06002C62 RID: 11362 RVA: 0x000C7F34 File Offset: 0x000C6134
		public static bool UseGlobalLock
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				return TraceInternal.UseGlobalLock;
			}
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			set
			{
				TraceInternal.UseGlobalLock = value;
			}
		}

		// Token: 0x17000AC7 RID: 2759
		// (get) Token: 0x06002C63 RID: 11363 RVA: 0x000C7F3C File Offset: 0x000C613C
		public static CorrelationManager CorrelationManager
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				if (Trace.correlationManager == null)
				{
					Trace.correlationManager = new CorrelationManager();
				}
				return Trace.correlationManager;
			}
		}

		// Token: 0x17000AC8 RID: 2760
		// (get) Token: 0x06002C64 RID: 11364 RVA: 0x000C7F5A File Offset: 0x000C615A
		// (set) Token: 0x06002C65 RID: 11365 RVA: 0x000C7F61 File Offset: 0x000C6161
		public static int IndentLevel
		{
			get
			{
				return TraceInternal.IndentLevel;
			}
			set
			{
				TraceInternal.IndentLevel = value;
			}
		}

		// Token: 0x17000AC9 RID: 2761
		// (get) Token: 0x06002C66 RID: 11366 RVA: 0x000C7F69 File Offset: 0x000C6169
		// (set) Token: 0x06002C67 RID: 11367 RVA: 0x000C7F70 File Offset: 0x000C6170
		public static int IndentSize
		{
			get
			{
				return TraceInternal.IndentSize;
			}
			set
			{
				TraceInternal.IndentSize = value;
			}
		}

		// Token: 0x06002C68 RID: 11368 RVA: 0x000C7F78 File Offset: 0x000C6178
		[Conditional("TRACE")]
		public static void Flush()
		{
			TraceInternal.Flush();
		}

		// Token: 0x06002C69 RID: 11369 RVA: 0x000C7F7F File Offset: 0x000C617F
		[Conditional("TRACE")]
		public static void Close()
		{
			new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
			TraceInternal.Close();
		}

		// Token: 0x06002C6A RID: 11370 RVA: 0x000C7F91 File Offset: 0x000C6191
		[Conditional("TRACE")]
		public static void Assert(bool condition)
		{
			TraceInternal.Assert(condition);
		}

		// Token: 0x06002C6B RID: 11371 RVA: 0x000C7F99 File Offset: 0x000C6199
		[Conditional("TRACE")]
		public static void Assert(bool condition, string message)
		{
			TraceInternal.Assert(condition, message);
		}

		// Token: 0x06002C6C RID: 11372 RVA: 0x000C7FA2 File Offset: 0x000C61A2
		[Conditional("TRACE")]
		public static void Assert(bool condition, string message, string detailMessage)
		{
			TraceInternal.Assert(condition, message, detailMessage);
		}

		// Token: 0x06002C6D RID: 11373 RVA: 0x000C7FAC File Offset: 0x000C61AC
		[Conditional("TRACE")]
		public static void Fail(string message)
		{
			TraceInternal.Fail(message);
		}

		// Token: 0x06002C6E RID: 11374 RVA: 0x000C7FB4 File Offset: 0x000C61B4
		[Conditional("TRACE")]
		public static void Fail(string message, string detailMessage)
		{
			TraceInternal.Fail(message, detailMessage);
		}

		// Token: 0x06002C6F RID: 11375 RVA: 0x000C7FBD File Offset: 0x000C61BD
		public static void Refresh()
		{
			DiagnosticsConfiguration.Refresh();
			Switch.RefreshAll();
			TraceSource.RefreshAll();
			TraceInternal.Refresh();
		}

		// Token: 0x06002C70 RID: 11376 RVA: 0x000C7FD3 File Offset: 0x000C61D3
		[Conditional("TRACE")]
		public static void TraceInformation(string message)
		{
			TraceInternal.TraceEvent(TraceEventType.Information, 0, message, null);
		}

		// Token: 0x06002C71 RID: 11377 RVA: 0x000C7FDE File Offset: 0x000C61DE
		[Conditional("TRACE")]
		public static void TraceInformation(string format, params object[] args)
		{
			TraceInternal.TraceEvent(TraceEventType.Information, 0, format, args);
		}

		// Token: 0x06002C72 RID: 11378 RVA: 0x000C7FE9 File Offset: 0x000C61E9
		[Conditional("TRACE")]
		public static void TraceWarning(string message)
		{
			TraceInternal.TraceEvent(TraceEventType.Warning, 0, message, null);
		}

		// Token: 0x06002C73 RID: 11379 RVA: 0x000C7FF4 File Offset: 0x000C61F4
		[Conditional("TRACE")]
		public static void TraceWarning(string format, params object[] args)
		{
			TraceInternal.TraceEvent(TraceEventType.Warning, 0, format, args);
		}

		// Token: 0x06002C74 RID: 11380 RVA: 0x000C7FFF File Offset: 0x000C61FF
		[Conditional("TRACE")]
		public static void TraceError(string message)
		{
			TraceInternal.TraceEvent(TraceEventType.Error, 0, message, null);
		}

		// Token: 0x06002C75 RID: 11381 RVA: 0x000C800A File Offset: 0x000C620A
		[Conditional("TRACE")]
		public static void TraceError(string format, params object[] args)
		{
			TraceInternal.TraceEvent(TraceEventType.Error, 0, format, args);
		}

		// Token: 0x06002C76 RID: 11382 RVA: 0x000C8015 File Offset: 0x000C6215
		[Conditional("TRACE")]
		public static void Write(string message)
		{
			TraceInternal.Write(message);
		}

		// Token: 0x06002C77 RID: 11383 RVA: 0x000C801D File Offset: 0x000C621D
		[Conditional("TRACE")]
		public static void Write(object value)
		{
			TraceInternal.Write(value);
		}

		// Token: 0x06002C78 RID: 11384 RVA: 0x000C8025 File Offset: 0x000C6225
		[Conditional("TRACE")]
		public static void Write(string message, string category)
		{
			TraceInternal.Write(message, category);
		}

		// Token: 0x06002C79 RID: 11385 RVA: 0x000C802E File Offset: 0x000C622E
		[Conditional("TRACE")]
		public static void Write(object value, string category)
		{
			TraceInternal.Write(value, category);
		}

		// Token: 0x06002C7A RID: 11386 RVA: 0x000C8037 File Offset: 0x000C6237
		[Conditional("TRACE")]
		public static void WriteLine(string message)
		{
			TraceInternal.WriteLine(message);
		}

		// Token: 0x06002C7B RID: 11387 RVA: 0x000C803F File Offset: 0x000C623F
		[Conditional("TRACE")]
		public static void WriteLine(object value)
		{
			TraceInternal.WriteLine(value);
		}

		// Token: 0x06002C7C RID: 11388 RVA: 0x000C8047 File Offset: 0x000C6247
		[Conditional("TRACE")]
		public static void WriteLine(string message, string category)
		{
			TraceInternal.WriteLine(message, category);
		}

		// Token: 0x06002C7D RID: 11389 RVA: 0x000C8050 File Offset: 0x000C6250
		[Conditional("TRACE")]
		public static void WriteLine(object value, string category)
		{
			TraceInternal.WriteLine(value, category);
		}

		// Token: 0x06002C7E RID: 11390 RVA: 0x000C8059 File Offset: 0x000C6259
		[Conditional("TRACE")]
		public static void WriteIf(bool condition, string message)
		{
			TraceInternal.WriteIf(condition, message);
		}

		// Token: 0x06002C7F RID: 11391 RVA: 0x000C8062 File Offset: 0x000C6262
		[Conditional("TRACE")]
		public static void WriteIf(bool condition, object value)
		{
			TraceInternal.WriteIf(condition, value);
		}

		// Token: 0x06002C80 RID: 11392 RVA: 0x000C806B File Offset: 0x000C626B
		[Conditional("TRACE")]
		public static void WriteIf(bool condition, string message, string category)
		{
			TraceInternal.WriteIf(condition, message, category);
		}

		// Token: 0x06002C81 RID: 11393 RVA: 0x000C8075 File Offset: 0x000C6275
		[Conditional("TRACE")]
		public static void WriteIf(bool condition, object value, string category)
		{
			TraceInternal.WriteIf(condition, value, category);
		}

		// Token: 0x06002C82 RID: 11394 RVA: 0x000C807F File Offset: 0x000C627F
		[Conditional("TRACE")]
		public static void WriteLineIf(bool condition, string message)
		{
			TraceInternal.WriteLineIf(condition, message);
		}

		// Token: 0x06002C83 RID: 11395 RVA: 0x000C8088 File Offset: 0x000C6288
		[Conditional("TRACE")]
		public static void WriteLineIf(bool condition, object value)
		{
			TraceInternal.WriteLineIf(condition, value);
		}

		// Token: 0x06002C84 RID: 11396 RVA: 0x000C8091 File Offset: 0x000C6291
		[Conditional("TRACE")]
		public static void WriteLineIf(bool condition, string message, string category)
		{
			TraceInternal.WriteLineIf(condition, message, category);
		}

		// Token: 0x06002C85 RID: 11397 RVA: 0x000C809B File Offset: 0x000C629B
		[Conditional("TRACE")]
		public static void WriteLineIf(bool condition, object value, string category)
		{
			TraceInternal.WriteLineIf(condition, value, category);
		}

		// Token: 0x06002C86 RID: 11398 RVA: 0x000C80A5 File Offset: 0x000C62A5
		[Conditional("TRACE")]
		public static void Indent()
		{
			TraceInternal.Indent();
		}

		// Token: 0x06002C87 RID: 11399 RVA: 0x000C80AC File Offset: 0x000C62AC
		[Conditional("TRACE")]
		public static void Unindent()
		{
			TraceInternal.Unindent();
		}

		// Token: 0x040026D0 RID: 9936
		private static volatile CorrelationManager correlationManager;
	}
}
