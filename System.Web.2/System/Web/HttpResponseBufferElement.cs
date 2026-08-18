using System;
using System.Text;
using System.Web.Util;

namespace System.Web
{
	// Token: 0x020000C3 RID: 195
	internal sealed class HttpResponseBufferElement : HttpBaseMemoryResponseBufferElement, IHttpResponseElement
	{
		// Token: 0x06000D63 RID: 3427 RVA: 0x000255FF File Offset: 0x000237FF
		internal HttpResponseBufferElement(byte[] data, int size)
		{
			this._data = data;
			this._size = size;
			this._free = 0;
			this._recycle = false;
		}

		// Token: 0x06000D64 RID: 3428 RVA: 0x00025624 File Offset: 0x00023824
		internal override HttpResponseBufferElement Clone()
		{
			int num = this._size - this._free;
			byte[] array = new byte[num];
			Buffer.BlockCopy(this._data, 0, array, 0, num);
			return new HttpResponseBufferElement(array, num);
		}

		// Token: 0x06000D65 RID: 3429 RVA: 0x00006164 File Offset: 0x00004364
		internal override void Recycle()
		{
		}

		// Token: 0x06000D66 RID: 3430 RVA: 0x0002565C File Offset: 0x0002385C
		internal override int Append(byte[] data, int offset, int size)
		{
			if (this._free == 0 || size == 0)
			{
				return 0;
			}
			int num = (this._free >= size) ? size : this._free;
			Buffer.BlockCopy(data, offset, this._data, this._size - this._free, num);
			this._free -= num;
			return num;
		}

		// Token: 0x06000D67 RID: 3431 RVA: 0x000256B4 File Offset: 0x000238B4
		internal override int Append(IntPtr data, int offset, int size)
		{
			if (this._free == 0 || size == 0)
			{
				return 0;
			}
			int num = (this._free >= size) ? size : this._free;
			Misc.CopyMemory(data, offset, this._data, this._size - this._free, num);
			this._free -= num;
			return num;
		}

		// Token: 0x06000D68 RID: 3432 RVA: 0x0002570C File Offset: 0x0002390C
		internal override void AppendEncodedChars(char[] data, int offset, int size, Encoder encoder, bool flushEncoder)
		{
			int bytes = encoder.GetBytes(data, offset, size, this._data, this._size - this._free, flushEncoder);
			this._free -= bytes;
		}

		// Token: 0x06000D69 RID: 3433 RVA: 0x00025747 File Offset: 0x00023947
		long IHttpResponseElement.GetSize()
		{
			return (long)(this._size - this._free);
		}

		// Token: 0x06000D6A RID: 3434 RVA: 0x00025757 File Offset: 0x00023957
		byte[] IHttpResponseElement.GetBytes()
		{
			return this._data;
		}

		// Token: 0x06000D6B RID: 3435 RVA: 0x00025760 File Offset: 0x00023960
		void IHttpResponseElement.Send(HttpWorkerRequest wr)
		{
			int num = this._size - this._free;
			if (num > 0)
			{
				wr.SendResponseFromMemory(this._data, num);
			}
		}

		// Token: 0x040004F8 RID: 1272
		private byte[] _data;
	}
}
