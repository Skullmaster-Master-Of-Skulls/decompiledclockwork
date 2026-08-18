using System;
using System.Runtime.InteropServices;

namespace System.IdentityModel
{
	// Token: 0x02000092 RID: 146
	[StructLayout(LayoutKind.Sequential)]
	internal class SecurityBufferDescriptor
	{
		// Token: 0x060004D6 RID: 1238 RVA: 0x00011F5C File Offset: 0x0001015C
		public SecurityBufferDescriptor(int count)
		{
			this.Version = 0;
			this.Count = count;
			this.UnmanagedPointer = null;
		}

		// Token: 0x0400044A RID: 1098
		public readonly int Version;

		// Token: 0x0400044B RID: 1099
		public readonly int Count;

		// Token: 0x0400044C RID: 1100
		public unsafe void* UnmanagedPointer;
	}
}
