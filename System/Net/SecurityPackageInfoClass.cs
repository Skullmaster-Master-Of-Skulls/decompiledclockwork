using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x0200054A RID: 1354
	internal class SecurityPackageInfoClass
	{
		// Token: 0x06002927 RID: 10535 RVA: 0x000ABBA0 File Offset: 0x000AABA0
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
				if (ComNetOS.IsWin9x)
				{
					this.Name = Marshal.PtrToStringAnsi(intPtr);
				}
				else
				{
					this.Name = Marshal.PtrToStringUni(intPtr);
				}
			}
			intPtr = Marshal.ReadIntPtr(ptr, (int)Marshal.OffsetOf(typeof(SecurityPackageInfo), "Comment"));
			if (intPtr != IntPtr.Zero)
			{
				if (ComNetOS.IsWin9x)
				{
					this.Comment = Marshal.PtrToStringAnsi(intPtr);
					return;
				}
				this.Comment = Marshal.PtrToStringUni(intPtr);
			}
		}

		// Token: 0x06002928 RID: 10536 RVA: 0x000ABD00 File Offset: 0x000AAD00
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"Capabilities:",
				string.Format(CultureInfo.InvariantCulture, "0x{0:x}", new object[]
				{
					this.Capabilities
				}),
				" Version:",
				this.Version.ToString(NumberFormatInfo.InvariantInfo),
				" RPCID:",
				this.RPCID.ToString(NumberFormatInfo.InvariantInfo),
				" MaxToken:",
				this.MaxToken.ToString(NumberFormatInfo.InvariantInfo),
				" Name:",
				(this.Name == null) ? "(null)" : this.Name,
				" Comment:",
				(this.Comment == null) ? "(null)" : this.Comment
			});
		}

		// Token: 0x04002836 RID: 10294
		internal int Capabilities;

		// Token: 0x04002837 RID: 10295
		internal short Version;

		// Token: 0x04002838 RID: 10296
		internal short RPCID;

		// Token: 0x04002839 RID: 10297
		internal int MaxToken;

		// Token: 0x0400283A RID: 10298
		internal string Name;

		// Token: 0x0400283B RID: 10299
		internal string Comment;
	}
}
