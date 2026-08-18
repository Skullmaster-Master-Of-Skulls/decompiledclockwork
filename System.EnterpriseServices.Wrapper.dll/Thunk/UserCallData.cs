using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;

namespace System.EnterpriseServices.Thunk
{
	// Token: 0x02000057 RID: 87
	internal class UserCallData
	{
		// Token: 0x060000CB RID: 203 RVA: 0x000029FC File Offset: 0x00001DFC
		public static UserCallData Get(IntPtr pinned)
		{
			return (UserCallData)((GCHandle)pinned).Target;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00002A1C File Offset: 0x00001E1C
		public UserCallData(object otp, IMessage msg, IntPtr ctx, [MarshalAs(UnmanagedType.U1)] bool fIsAutoDone, MemberInfo mb)
		{
			this.otp = otp;
			this.msg = msg;
			this.pDestCtx = ctx.ToInt64();
			this.fIsAutoDone = fIsAutoDone;
			this.mb = mb;
			this.except = null;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00002A64 File Offset: 0x00001E64
		public IntPtr Pin()
		{
			return (IntPtr)GCHandle.Alloc(this, GCHandleType.Normal);
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00002A80 File Offset: 0x00001E80
		public void Unpin(IntPtr pinned)
		{
			((GCHandle)pinned).Free();
		}

		// Token: 0x04000121 RID: 289
		public object otp;

		// Token: 0x04000122 RID: 290
		public object except;

		// Token: 0x04000123 RID: 291
		public MemberInfo mb;

		// Token: 0x04000124 RID: 292
		public IMessage msg;

		// Token: 0x04000125 RID: 293
		public unsafe IUnknown* pDestCtx;

		// Token: 0x04000126 RID: 294
		public bool fIsAutoDone;
	}
}
