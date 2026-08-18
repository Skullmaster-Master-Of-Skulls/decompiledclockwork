using System;

namespace System.Diagnostics
{
	// Token: 0x02000002 RID: 2
	public static class StackFrameExtensions
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public static bool HasNativeImage(this StackFrame stackFrame)
		{
			return stackFrame.GetNativeImageBase() != IntPtr.Zero;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002062 File Offset: 0x00000262
		public static bool HasMethod(this StackFrame stackFrame)
		{
			return stackFrame.GetMethod() != null;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002070 File Offset: 0x00000270
		public static bool HasILOffset(this StackFrame stackFrame)
		{
			return stackFrame.GetILOffset() != -1;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x0000207E File Offset: 0x0000027E
		public static bool HasSource(this StackFrame stackFrame)
		{
			return stackFrame.GetFileName() != null;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002089 File Offset: 0x00000289
		public static IntPtr GetNativeIP(this StackFrame stackFrame)
		{
			return (IntPtr)0;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002091 File Offset: 0x00000291
		public static IntPtr GetNativeImageBase(this StackFrame stackFrame)
		{
			return (IntPtr)0;
		}
	}
}
