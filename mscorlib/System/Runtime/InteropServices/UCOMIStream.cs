using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000549 RID: 1353
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.IStream instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Guid("0000000c-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface UCOMIStream
	{
		// Token: 0x06003391 RID: 13201
		void Read([MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] [Out] byte[] pv, int cb, IntPtr pcbRead);

		// Token: 0x06003392 RID: 13202
		void Write([MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] byte[] pv, int cb, IntPtr pcbWritten);

		// Token: 0x06003393 RID: 13203
		void Seek(long dlibMove, int dwOrigin, IntPtr plibNewPosition);

		// Token: 0x06003394 RID: 13204
		void SetSize(long libNewSize);

		// Token: 0x06003395 RID: 13205
		void CopyTo(UCOMIStream pstm, long cb, IntPtr pcbRead, IntPtr pcbWritten);

		// Token: 0x06003396 RID: 13206
		void Commit(int grfCommitFlags);

		// Token: 0x06003397 RID: 13207
		void Revert();

		// Token: 0x06003398 RID: 13208
		void LockRegion(long libOffset, long cb, int dwLockType);

		// Token: 0x06003399 RID: 13209
		void UnlockRegion(long libOffset, long cb, int dwLockType);

		// Token: 0x0600339A RID: 13210
		void Stat(out STATSTG pstatstg, int grfStatFlag);

		// Token: 0x0600339B RID: 13211
		void Clone(out UCOMIStream ppstm);
	}
}
