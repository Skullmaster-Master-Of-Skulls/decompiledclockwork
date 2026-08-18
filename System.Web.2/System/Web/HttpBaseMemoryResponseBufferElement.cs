using System;
using System.Text;

namespace System.Web
{
	// Token: 0x020000C2 RID: 194
	internal abstract class HttpBaseMemoryResponseBufferElement
	{
		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x06000D5B RID: 3419 RVA: 0x000255EE File Offset: 0x000237EE
		internal int FreeBytes
		{
			get
			{
				return this._free;
			}
		}

		// Token: 0x06000D5C RID: 3420 RVA: 0x000255F6 File Offset: 0x000237F6
		internal void DisableRecycling()
		{
			this._recycle = false;
		}

		// Token: 0x06000D5D RID: 3421
		internal abstract void Recycle();

		// Token: 0x06000D5E RID: 3422
		internal abstract HttpResponseBufferElement Clone();

		// Token: 0x06000D5F RID: 3423
		internal abstract int Append(byte[] data, int offset, int size);

		// Token: 0x06000D60 RID: 3424
		internal abstract int Append(IntPtr data, int offset, int size);

		// Token: 0x06000D61 RID: 3425
		internal abstract void AppendEncodedChars(char[] data, int offset, int size, Encoder encoder, bool flushEncoder);

		// Token: 0x040004F5 RID: 1269
		protected int _size;

		// Token: 0x040004F6 RID: 1270
		protected int _free;

		// Token: 0x040004F7 RID: 1271
		protected bool _recycle;
	}
}
