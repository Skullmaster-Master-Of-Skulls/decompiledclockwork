using System;
using System.Runtime.InteropServices;

namespace System.IdentityModel
{
	// Token: 0x020000A4 RID: 164
	internal class SecurityPackageInfoClass
	{
		// Token: 0x06000523 RID: 1315 RVA: 0x00013294 File Offset: 0x00011494
		internal SecurityPackageInfoClass(SafeHandle safeHandle, int index)
		{
			if (safeHandle.IsInvalid)
			{
				return;
			}
			IntPtr ptr = IntPtrHelper.Add(safeHandle.DangerousGetHandle(), SecurityPackageInfo.Size * index);
			this.Capabilities = Marshal.ReadInt32(ptr, (int)Marshal.OffsetOf(typeof(SecurityPackageInfo), "Capabilities"));
			this.Version = Marshal.ReadInt16(ptr, (int)Marshal.OffsetOf(typeof(SecurityPackageInfo), "Version"));
			this.RPCID = Marshal.ReadInt16(ptr, (int)Marshal.OffsetOf(typeof(SecurityPackageInfo), "RPCID"));
			this.MaxToken = Marshal.ReadInt32(ptr, (int)Marshal.OffsetOf(typeof(SecurityPackageInfo), "MaxToken"));
			IntPtr intPtr = Marshal.ReadIntPtr(ptr, (int)Marshal.OffsetOf(typeof(SecurityPackageInfo), "Name"));
			if (intPtr != IntPtr.Zero)
			{
				this.Name = Marshal.PtrToStringUni(intPtr);
			}
			intPtr = Marshal.ReadIntPtr(ptr, (int)Marshal.OffsetOf(typeof(SecurityPackageInfo), "Comment"));
			if (intPtr != IntPtr.Zero)
			{
				this.Comment = Marshal.PtrToStringUni(intPtr);
			}
		}

		// Token: 0x040004A3 RID: 1187
		internal int Capabilities;

		// Token: 0x040004A4 RID: 1188
		internal short Version;

		// Token: 0x040004A5 RID: 1189
		internal short RPCID;

		// Token: 0x040004A6 RID: 1190
		internal int MaxToken;

		// Token: 0x040004A7 RID: 1191
		internal string Name;

		// Token: 0x040004A8 RID: 1192
		internal string Comment;
	}
}
