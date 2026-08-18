using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x02000134 RID: 308
	[SecurityCritical]
	internal struct PinAndClear : IDisposable
	{
		// Token: 0x06000A09 RID: 2569 RVA: 0x0002465C File Offset: 0x0002285C
		[SecurityCritical]
		internal static PinAndClear Track(byte[] data)
		{
			return new PinAndClear
			{
				_gcHandle = GCHandle.Alloc(data, GCHandleType.Pinned),
				_data = data
			};
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x00024688 File Offset: 0x00022888
		[SecurityCritical]
		public void Dispose()
		{
			Array.Clear(this._data, 0, this._data.Length);
			this._gcHandle.Free();
		}

		// Token: 0x0400075A RID: 1882
		private byte[] _data;

		// Token: 0x0400075B RID: 1883
		private GCHandle _gcHandle;
	}
}
