using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x02000217 RID: 535
	internal class SecurityPackageInfoClass
	{
		// Token: 0x060013C9 RID: 5065 RVA: 0x00068848 File Offset: 0x00066A48
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

		// Token: 0x060013CA RID: 5066 RVA: 0x00068980 File Offset: 0x00066B80
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

		// Token: 0x040015CB RID: 5579
		internal int Capabilities;

		// Token: 0x040015CC RID: 5580
		internal short Version;

		// Token: 0x040015CD RID: 5581
		internal short RPCID;

		// Token: 0x040015CE RID: 5582
		internal int MaxToken;

		// Token: 0x040015CF RID: 5583
		internal string Name;

		// Token: 0x040015D0 RID: 5584
		internal string Comment;
	}
}
