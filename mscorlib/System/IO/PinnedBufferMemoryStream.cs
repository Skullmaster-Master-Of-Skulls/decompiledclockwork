using System;
using System.Runtime.InteropServices;

namespace System.IO
{
	// Token: 0x020005C3 RID: 1475
	internal sealed class PinnedBufferMemoryStream : UnmanagedMemoryStream
	{
		// Token: 0x060036C9 RID: 14025 RVA: 0x000B95A8 File Offset: 0x000B85A8
		internal unsafe PinnedBufferMemoryStream(byte[] array)
		{
			int num = array.Length;
			if (num == 0)
			{
				array = new byte[1];
				num = 0;
			}
			this._array = array;
			this._pinningHandle = new GCHandle(array, GCHandleType.Pinned);
			fixed (byte* array2 = this._array)
			{
				base.Initialize(array2, (long)num, (long)num, FileAccess.Read, true);
			}
		}

		// Token: 0x060036CA RID: 14026 RVA: 0x000B9610 File Offset: 0x000B8610
		~PinnedBufferMemoryStream()
		{
			this.Dispose(false);
		}

		// Token: 0x060036CB RID: 14027 RVA: 0x000B9640 File Offset: 0x000B8640
		protected override void Dispose(bool disposing)
		{
			if (this._isOpen)
			{
				this._pinningHandle.Free();
				this._isOpen = false;
			}
			base.Dispose(disposing);
		}

		// Token: 0x04001CA7 RID: 7335
		private byte[] _array;

		// Token: 0x04001CA8 RID: 7336
		private GCHandle _pinningHandle;
	}
}
