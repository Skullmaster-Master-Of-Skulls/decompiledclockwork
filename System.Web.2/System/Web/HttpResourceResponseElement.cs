using System;
using System.Web.Util;

namespace System.Web
{
	// Token: 0x020000C5 RID: 197
	internal sealed class HttpResourceResponseElement : IHttpResponseElement
	{
		// Token: 0x06000D7B RID: 3451 RVA: 0x00025B19 File Offset: 0x00023D19
		internal HttpResourceResponseElement(IntPtr data, int offset, int size)
		{
			this._data = data;
			this._offset = offset;
			this._size = size;
		}

		// Token: 0x06000D7C RID: 3452 RVA: 0x00025B36 File Offset: 0x00023D36
		long IHttpResponseElement.GetSize()
		{
			return (long)this._size;
		}

		// Token: 0x06000D7D RID: 3453 RVA: 0x00025B40 File Offset: 0x00023D40
		byte[] IHttpResponseElement.GetBytes()
		{
			if (this._size > 0)
			{
				byte[] array = new byte[this._size];
				Misc.CopyMemory(this._data, this._offset, array, 0, this._size);
				return array;
			}
			return null;
		}

		// Token: 0x06000D7E RID: 3454 RVA: 0x00025B7E File Offset: 0x00023D7E
		void IHttpResponseElement.Send(HttpWorkerRequest wr)
		{
			if (this._size > 0)
			{
				wr.SendResponseFromMemory(new IntPtr(this._data.ToInt64() + (long)this._offset), this._size, false);
			}
		}

		// Token: 0x040004FB RID: 1275
		private IntPtr _data;

		// Token: 0x040004FC RID: 1276
		private int _offset;

		// Token: 0x040004FD RID: 1277
		private int _size;
	}
}
