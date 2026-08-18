using System;

namespace System.IdentityModel
{
	// Token: 0x0200008E RID: 142
	internal struct SecurityBufferStruct
	{
		// Token: 0x0400042A RID: 1066
		public int count;

		// Token: 0x0400042B RID: 1067
		public BufferType type;

		// Token: 0x0400042C RID: 1068
		public IntPtr token;

		// Token: 0x0400042D RID: 1069
		public static readonly int Size = sizeof(SecurityBufferStruct);
	}
}
