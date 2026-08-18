using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices.Thunk
{
	// Token: 0x02000058 RID: 88
	[Serializable]
	internal class UserMarshalData
	{
		// Token: 0x060000CF RID: 207 RVA: 0x00002A9C File Offset: 0x00001E9C
		public static UserMarshalData Get(IntPtr pinned)
		{
			return (UserMarshalData)((GCHandle)pinned).Target;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00002ABC File Offset: 0x00001EBC
		public UserMarshalData(IntPtr pUnk)
		{
			this.pUnk = pUnk;
			this.buffer = null;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00002AE0 File Offset: 0x00001EE0
		public IntPtr Pin()
		{
			return (IntPtr)GCHandle.Alloc(this, GCHandleType.Normal);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00002AFC File Offset: 0x00001EFC
		public void Unpin(IntPtr pinned)
		{
			((GCHandle)pinned).Free();
		}

		// Token: 0x04000127 RID: 295
		public IntPtr pUnk;

		// Token: 0x04000128 RID: 296
		public byte[] buffer;
	}
}
