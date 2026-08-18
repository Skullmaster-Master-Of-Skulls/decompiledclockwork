using System;
using System.Globalization;
using System.Security.Permissions;

namespace System.Diagnostics
{
	// Token: 0x02000496 RID: 1174
	[__DynamicallyInvokable]
	public static class Debug
	{
		// Token: 0x17000A82 RID: 2690
		// (get) Token: 0x06002B73 RID: 11123 RVA: 0x000C53F6 File Offset: 0x000C35F6
		public static TraceListenerCollection Listeners
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
			get
			{
				return TraceInternal.Listeners;
			}
		}

		// Token: 0x17000A83 RID: 2691
		// (get) Token: 0x06002B74 RID: 11124 RVA: 0x000C53FD File Offset: 0x000C35FD
		// (set) Token: 0x06002B75 RID: 11125 RVA: 0x000C5404 File Offset: 0x000C3604
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

		// Token: 0x17000A84 RID: 2692
		// (get) Token: 0x06002B76 RID: 11126 RVA: 0x000C540C File Offset: 0x000C360C
		// (set) Token: 0x06002B77 RID: 11127 RVA: 0x000C5413 File Offset: 0x000C3613
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

		// Token: 0x17000A85 RID: 2693
		// (get) Token: 0x06002B78 RID: 11128 RVA: 0x000C541B File Offset: 0x000C361B
		// (set) Token: 0x06002B79 RID: 11129 RVA: 0x000C5422 File Offset: 0x000C3622
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

		// Token: 0x06002B7A RID: 11130 RVA: 0x000C542A File Offset: 0x000C362A
		[Conditional("DEBUG")]
		public static void Flush()
		{
			TraceInternal.Flush();
		}

		// Token: 0x06002B7B RID: 11131 RVA: 0x000C5431 File Offset: 0x000C3631
		[Conditional("DEBUG")]
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public static void Close()
		{
			TraceInternal.Close();
		}

		// Token: 0x06002B7C RID: 11132 RVA: 0x000C5438 File Offset: 0x000C3638
		[Conditional("DEBUG")]
		[__DynamicallyInvokable]
		public static void Assert(bool condition)
		{
			TraceInternal.Assert(condition);
		}

		// Token: 0x06002B7D RID: 11133 RVA: 0x000C5440 File Offset: 0x000C3640
		[Conditional("DEBUG")]
		[__DynamicallyInvokable]
		public static void Assert(bool condition, string message)
		{
			TraceInternal.Assert(condition, message);
		}

		// Token: 0x06002B7E RID: 11134 RVA: 0x000C5449 File Offset: 0x000C3649
		[Conditional("DEBUG")]
		[__DynamicallyInvokable]
		public static void Assert(bool condition, string message, string detailMessage)
		{
			TraceInternal.Assert(condition, message, detailMessage);
		}

		// Token: 0x06002B7F RID: 11135 RVA: 0x000C5453 File Offset: 0x000C3653
		[Conditional("DEBUG")]
		[__DynamicallyInvokable]
		public static void Assert(bool condition, string message, string detailMessageFormat, params object[] args)
		{
			TraceInternal.Assert(condition, message, string.Format(CultureInfo.InvariantCulture, detailMessageFormat, args));
		}

		// Token: 0x06002B80 RID: 11136 RVA: 0x000C5468 File Offset: 0x000C3668
		[Conditional("DEBUG")]
		[__DynamicallyInvokable]
		public static void Fail(string message)
		{
			TraceInternal.Fail(message);
		}

		// Token: 0x06002B81 RID: 11137 RVA: 0x000C5470 File Offset: 0x000C3670
		[Conditional("DEBUG")]
		[__DynamicallyInvokable]
		public static void Fail(string message, string detailMessage)
		{
			TraceInternal.Fail(message, detailMessage);
		}

		// Token: 0x06002B82 RID: 11138 RVA: 0x000C5479 File Offset: 0x000C3679
		[Conditional("DEBUG")]
		public static void Print(string message)
		{
			TraceInternal.WriteLine(message);
		}

		// Token: 0x06002B83 RID: 11139 RVA: 0x000C5481 File Offset: 0x000C3681
		[Conditional("DEBUG")]
		public static void Print(string format, params object[] args)
		{
			TraceInternal.WriteLine(string.Format(CultureInfo.InvariantCulture, format, args));
		}

		// Token: 0x06002B84 RID: 11140 RVA: 0x000C5494 File Offset: 0x000C3694
		[Conditional("DEBUG")]
		[__DynamicallyInvokable]
		public static void Write(string message)
		{
			TraceInternal.Write(message);
		}

		// Token: 0x06002B85 RID: 11141 RVA: 0x000C549C File Offset: 0x000C369C
		[Conditional("DEBUG")]
		[__DynamicallyInvokable]
		public static void Write(object value)
		{
			TraceInternal.Write(value);
		}

		// Token: 0x06002B86 RID: 11142 RVA: 0x000C54A4 File Offset: 0x000C36A4
		[Conditional("DEBUG")]
		[__DynamicallyInvokable]
		public static void Write(string message, string category)
		{
			TraceInternal.Write(message, category);
		}

		// Token: 0x06002B87 RID: 11143 RVA: 0x000C54AD File Offset: 0x000C36AD
		[Conditional("DEBUG")]
		[__DynamicallyInvokable]
		public static void Write(object value, string category)
		{
			TraceInternal.Write(value, category);
		}

		// Token: 0x06002B88 RID: 11144 RVA: 0x000C54B6 File Offset: 0x000C36B6
		[Conditional("DEBUG")]
		[__DynamicallyInvokable]
		public static void WriteLine(string message)
		{
			TraceInternal.WriteLine(message);
		}

		// Token: 0x06002B89 RID: 11145 RVA: 0x000C54BE File Offset: 0x000C36BE
		[Conditional("DEBUG")]
		[__DynamicallyInvokable]
		public static void WriteLine(object value)
		{
			TraceInternal.WriteLine(value);
		}

		// Token: 0x06002B8A RID: 11146 RVA: 0x000C54C6 File Offset: 0x000C36C6
		[Conditional("DEBUG")]
		[__DynamicallyInvokable]
		public static void WriteLine(string message, string category)
		{
			TraceInternal.WriteLine(message, category);
		}

		// Token: 0x06002B8B RID: 11147 RVA: 0x000C54CF File Offset: 0x000C36CF
		[Conditional("DEBUG")]
		[__DynamicallyInvokable]
		public static void WriteLine(object value, string category)
		{
			TraceInternal.WriteLine(value, category);
		}

		// Token: 0x06002B8C RID: 11148 RVA: 0x000C54D8 File Offset: 0x000C36D8
		[Conditional("DEBUG")]
		[__DynamicallyInvokable]
		public static void WriteLine(string format, params object[] args)
		{
			TraceInternal.WriteLine(string.Format(CultureInfo.InvariantCulture, format, args));
		}

		// Token: 0x06002B8D RID: 11149 RVA: 0x000C54EB File Offset: 0x000C36EB
		[Conditional("DEBUG")]
		[__DynamicallyInvokable]
		public static void WriteIf(bool condition, string message)
		{
			TraceInternal.WriteIf(condition, message);
		}

		// Token: 0x06002B8E RID: 11150 RVA: 0x000C54F4 File Offset: 0x000C36F4
		[Conditional("DEBUG")]
		[__DynamicallyInvokable]
		public static void WriteIf(bool condition, object value)
		{
			TraceInternal.WriteIf(condition, value);
		}

		// Token: 0x06002B8F RID: 11151 RVA: 0x000C54FD File Offset: 0x000C36FD
		[Conditional("DEBUG")]
		[__DynamicallyInvokable]
		public static void WriteIf(bool condition, string message, string category)
		{
			TraceInternal.WriteIf(condition, message, category);
		}

		// Token: 0x06002B90 RID: 11152 RVA: 0x000C5507 File Offset: 0x000C3707
		[Conditional("DEBUG")]
		[__DynamicallyInvokable]
		public static void WriteIf(bool condition, object value, string category)
		{
			TraceInternal.WriteIf(condition, value, category);
		}

		// Token: 0x06002B91 RID: 11153 RVA: 0x000C5511 File Offset: 0x000C3711
		[Conditional("DEBUG")]
		[__DynamicallyInvokable]
		public static void WriteLineIf(bool condition, string message)
		{
			TraceInternal.WriteLineIf(condition, message);
		}

		// Token: 0x06002B92 RID: 11154 RVA: 0x000C551A File Offset: 0x000C371A
		[Conditional("DEBUG")]
		[__DynamicallyInvokable]
		public static void WriteLineIf(bool condition, object value)
		{
			TraceInternal.WriteLineIf(condition, value);
		}

		// Token: 0x06002B93 RID: 11155 RVA: 0x000C5523 File Offset: 0x000C3723
		[Conditional("DEBUG")]
		[__DynamicallyInvokable]
		public static void WriteLineIf(bool condition, string message, string category)
		{
			TraceInternal.WriteLineIf(condition, message, category);
		}

		// Token: 0x06002B94 RID: 11156 RVA: 0x000C552D File Offset: 0x000C372D
		[Conditional("DEBUG")]
		[__DynamicallyInvokable]
		public static void WriteLineIf(bool condition, object value, string category)
		{
			TraceInternal.WriteLineIf(condition, value, category);
		}

		// Token: 0x06002B95 RID: 11157 RVA: 0x000C5537 File Offset: 0x000C3737
		[Conditional("DEBUG")]
		public static void Indent()
		{
			TraceInternal.Indent();
		}

		// Token: 0x06002B96 RID: 11158 RVA: 0x000C553E File Offset: 0x000C373E
		[Conditional("DEBUG")]
		public static void Unindent()
		{
			TraceInternal.Unindent();
		}
	}
}
