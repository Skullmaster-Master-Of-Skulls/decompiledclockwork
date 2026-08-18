using System;
using System.Threading;

namespace System.IO
{
	// Token: 0x0200000A RID: 10
	internal class ByteBufferPool : IByteBufferPool
	{
		// Token: 0x06000021 RID: 33 RVA: 0x00002588 File Offset: 0x00001588
		public ByteBufferPool(int maxBuffers, int bufferSize)
		{
			this._max = maxBuffers;
			this._bufferPool = new byte[this._max][];
			this._bufferSize = bufferSize;
			this._current = -1;
			this._last = -1;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000025C8 File Offset: 0x000015C8
		public byte[] GetBuffer()
		{
			object obj = null;
			byte[] result;
			try
			{
				obj = Interlocked.Exchange(ref this._controlCookie, null);
				if (obj != null)
				{
					if (this._current == -1)
					{
						this._controlCookie = obj;
						result = new byte[this._bufferSize];
					}
					else
					{
						byte[] array = this._bufferPool[this._current];
						this._bufferPool[this._current] = null;
						if (this._current == this._last)
						{
							this._current = -1;
						}
						else
						{
							this._current = (this._current + 1) % this._max;
						}
						this._controlCookie = obj;
						result = array;
					}
				}
				else
				{
					result = new byte[this._bufferSize];
				}
			}
			catch (ThreadAbortException)
			{
				if (obj != null)
				{
					this._current = -1;
					this._last = -1;
					this._controlCookie = obj;
				}
				throw;
			}
			return result;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002694 File Offset: 0x00001694
		public void ReturnBuffer(byte[] buffer)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			object obj = null;
			try
			{
				obj = Interlocked.Exchange(ref this._controlCookie, null);
				if (obj != null)
				{
					if (this._current == -1)
					{
						this._bufferPool[0] = buffer;
						this._current = 0;
						this._last = 0;
					}
					else
					{
						int num = (this._last + 1) % this._max;
						if (num != this._current)
						{
							this._last = num;
							this._bufferPool[this._last] = buffer;
						}
					}
					this._controlCookie = obj;
				}
			}
			catch (ThreadAbortException)
			{
				if (obj != null)
				{
					this._current = -1;
					this._last = -1;
					this._controlCookie = obj;
				}
				throw;
			}
		}

		// Token: 0x04000041 RID: 65
		private byte[][] _bufferPool;

		// Token: 0x04000042 RID: 66
		private int _current;

		// Token: 0x04000043 RID: 67
		private int _last;

		// Token: 0x04000044 RID: 68
		private int _max;

		// Token: 0x04000045 RID: 69
		private int _bufferSize;

		// Token: 0x04000046 RID: 70
		private object _controlCookie = "cookie object";
	}
}
