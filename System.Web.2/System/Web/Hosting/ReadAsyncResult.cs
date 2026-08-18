using System;
using System.Web.Util;

namespace System.Web.Hosting
{
	// Token: 0x020007E7 RID: 2023
	internal class ReadAsyncResult : AsyncResultBase
	{
		// Token: 0x0600608A RID: 24714 RVA: 0x0014DAEC File Offset: 0x0014BCEC
		internal ReadAsyncResult(AsyncCallback cb, object state, byte[] buffer, int offset, int count, bool updatePerfCounter) : base(cb, state)
		{
			this._buffer = buffer;
			this._offset = offset;
			this._count = count;
			this._updatePerfCounter = updatePerfCounter;
		}

		// Token: 0x0600608B RID: 24715 RVA: 0x0014DB15 File Offset: 0x0014BD15
		internal override void Complete(int bytesRead, int hresult, IntPtr pbAsyncReceiveBuffer, bool synchronous)
		{
			if (this._updatePerfCounter && bytesRead > 0)
			{
				PerfCounters.IncrementCounterEx(AppPerfCounter.REQUEST_BYTES_IN, bytesRead);
			}
			if (bytesRead != 0)
			{
				this.CopyBytes(pbAsyncReceiveBuffer, bytesRead);
			}
			this._bytesRead = bytesRead;
			base.Complete(hresult, synchronous);
		}

		// Token: 0x0600608C RID: 24716 RVA: 0x0014DB48 File Offset: 0x0014BD48
		private unsafe void CopyBytes(IntPtr pbAsyncReceiveBuffer, int bytesRead)
		{
			byte* src = (byte*)((void*)pbAsyncReceiveBuffer);
			byte[] array;
			byte* ptr;
			if ((array = this._buffer) == null || array.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array[0];
			}
			StringUtil.memcpyimpl(src, ptr + this._offset, bytesRead);
			array = null;
		}

		// Token: 0x17001B87 RID: 7047
		// (get) Token: 0x0600608D RID: 24717 RVA: 0x0014DB8A File Offset: 0x0014BD8A
		// (set) Token: 0x0600608E RID: 24718 RVA: 0x0014DB92 File Offset: 0x0014BD92
		internal int BytesRead
		{
			get
			{
				return this._bytesRead;
			}
			set
			{
				this._bytesRead = value;
			}
		}

		// Token: 0x0400325A RID: 12890
		private int _bytesRead;

		// Token: 0x0400325B RID: 12891
		private byte[] _buffer;

		// Token: 0x0400325C RID: 12892
		private int _offset;

		// Token: 0x0400325D RID: 12893
		private int _count;

		// Token: 0x0400325E RID: 12894
		private bool _updatePerfCounter;
	}
}
