using System;
using System.ComponentModel;

namespace System.Diagnostics
{
	// Token: 0x020004BC RID: 1212
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class StackFrameExtensions
	{
		// Token: 0x06002D57 RID: 11607 RVA: 0x000CC484 File Offset: 0x000CA684
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static bool HasNativeImage(this StackFrame stackFrame)
		{
			return stackFrame.GetNativeImageBase() != IntPtr.Zero;
		}

		// Token: 0x06002D58 RID: 11608 RVA: 0x000CC496 File Offset: 0x000CA696
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static bool HasMethod(this StackFrame stackFrame)
		{
			return stackFrame.GetMethod() != null;
		}

		// Token: 0x06002D59 RID: 11609 RVA: 0x000CC4A4 File Offset: 0x000CA6A4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static bool HasILOffset(this StackFrame stackFrame)
		{
			return stackFrame.GetILOffset() != -1;
		}

		// Token: 0x06002D5A RID: 11610 RVA: 0x000CC4B2 File Offset: 0x000CA6B2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static bool HasSource(this StackFrame stackFrame)
		{
			return stackFrame.GetFileName() != null;
		}

		// Token: 0x06002D5B RID: 11611 RVA: 0x000CC4BD File Offset: 0x000CA6BD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static IntPtr GetNativeIP(this StackFrame stackFrame)
		{
			return IntPtr.Zero;
		}

		// Token: 0x06002D5C RID: 11612 RVA: 0x000CC4C4 File Offset: 0x000CA6C4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static IntPtr GetNativeImageBase(this StackFrame stackFrame)
		{
			return IntPtr.Zero;
		}
	}
}
