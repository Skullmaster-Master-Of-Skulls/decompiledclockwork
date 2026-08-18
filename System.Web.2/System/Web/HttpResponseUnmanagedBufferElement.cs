using System;
using System.Text;
using System.Threading;
using System.Web.Hosting;
using System.Web.Util;

namespace System.Web
{
	// Token: 0x020000C4 RID: 196
	internal sealed class HttpResponseUnmanagedBufferElement : HttpBaseMemoryResponseBufferElement, IHttpResponseElement
	{
		// Token: 0x06000D6C RID: 3436 RVA: 0x0002578C File Offset: 0x0002398C
		static HttpResponseUnmanagedBufferElement()
		{
			if (HttpRuntime.UseIntegratedPipeline)
			{
				HttpResponseUnmanagedBufferElement.s_Pool = UnsafeIISMethods.MgdGetBufferPool(BufferingParams.INTEGRATED_MODE_BUFFER_SIZE);
				return;
			}
			HttpResponseUnmanagedBufferElement.s_Pool = UnsafeNativeMethods.BufferPoolGetPool(31744, 64);
		}

		// Token: 0x06000D6D RID: 3437 RVA: 0x000257B8 File Offset: 0x000239B8
		internal HttpResponseUnmanagedBufferElement()
		{
			if (HttpRuntime.UseIntegratedPipeline)
			{
				this._data = UnsafeIISMethods.MgdGetBuffer(HttpResponseUnmanagedBufferElement.s_Pool);
				this._size = BufferingParams.INTEGRATED_MODE_BUFFER_SIZE;
			}
			else
			{
				this._data = UnsafeNativeMethods.BufferPoolGetBuffer(HttpResponseUnmanagedBufferElement.s_Pool);
				this._size = 31744;
			}
			if (this._data == IntPtr.Zero)
			{
				throw new OutOfMemoryException();
			}
			this._free = this._size;
			this._recycle = true;
		}

		// Token: 0x06000D6E RID: 3438 RVA: 0x00025838 File Offset: 0x00023A38
		protected override void Finalize()
		{
			try
			{
				IntPtr intPtr = Interlocked.Exchange(ref this._data, IntPtr.Zero);
				if (intPtr != IntPtr.Zero)
				{
					if (HttpRuntime.UseIntegratedPipeline)
					{
						UnsafeIISMethods.MgdReturnBuffer(intPtr);
					}
					else
					{
						UnsafeNativeMethods.BufferPoolReleaseBuffer(intPtr);
					}
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x06000D6F RID: 3439 RVA: 0x00025894 File Offset: 0x00023A94
		internal override HttpResponseBufferElement Clone()
		{
			int num = this._size - this._free;
			byte[] array = new byte[num];
			Misc.CopyMemory(this._data, 0, array, 0, num);
			return new HttpResponseBufferElement(array, num);
		}

		// Token: 0x06000D70 RID: 3440 RVA: 0x000258CC File Offset: 0x00023ACC
		internal override void Recycle()
		{
			if (this._recycle)
			{
				this.ForceRecycle();
			}
		}

		// Token: 0x06000D71 RID: 3441 RVA: 0x000258DC File Offset: 0x00023ADC
		private void ForceRecycle()
		{
			IntPtr intPtr = Interlocked.Exchange(ref this._data, IntPtr.Zero);
			if (intPtr != IntPtr.Zero)
			{
				this._free = 0;
				this._recycle = false;
				if (HttpRuntime.UseIntegratedPipeline)
				{
					UnsafeIISMethods.MgdReturnBuffer(intPtr);
				}
				else
				{
					UnsafeNativeMethods.BufferPoolReleaseBuffer(intPtr);
				}
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x06000D72 RID: 3442 RVA: 0x00025934 File Offset: 0x00023B34
		internal override int Append(byte[] data, int offset, int size)
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

		// Token: 0x06000D73 RID: 3443 RVA: 0x0002598C File Offset: 0x00023B8C
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

		// Token: 0x06000D74 RID: 3444 RVA: 0x000259E3 File Offset: 0x00023BE3
		internal void AdjustSize(int size)
		{
			this._free -= size;
		}

		// Token: 0x06000D75 RID: 3445 RVA: 0x000259F4 File Offset: 0x00023BF4
		internal override void AppendEncodedChars(char[] data, int offset, int size, Encoder encoder, bool flushEncoder)
		{
			int num = HttpResponseUnmanagedBufferElement.UnsafeAppendEncodedChars(data, offset, size, this._data, this._size - this._free, this._free, encoder, flushEncoder);
			this._free -= num;
		}

		// Token: 0x06000D76 RID: 3446 RVA: 0x00025A38 File Offset: 0x00023C38
		private unsafe static int UnsafeAppendEncodedChars(char[] src, int srcOffset, int srcSize, IntPtr dest, int destOffset, int destSize, Encoder encoder, bool flushEncoder)
		{
			byte* bytes = (byte*)((void*)dest) + destOffset;
			int bytes2;
			fixed (char[] array = src)
			{
				char* ptr;
				if (src == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				bytes2 = encoder.GetBytes(ptr + srcOffset, srcSize, bytes, destSize, flushEncoder);
			}
			return bytes2;
		}

		// Token: 0x06000D77 RID: 3447 RVA: 0x00025747 File Offset: 0x00023947
		long IHttpResponseElement.GetSize()
		{
			return (long)(this._size - this._free);
		}

		// Token: 0x06000D78 RID: 3448 RVA: 0x00025A80 File Offset: 0x00023C80
		byte[] IHttpResponseElement.GetBytes()
		{
			int num = this._size - this._free;
			if (num > 0)
			{
				byte[] array = new byte[num];
				Misc.CopyMemory(this._data, 0, array, 0, num);
				return array;
			}
			return null;
		}

		// Token: 0x06000D79 RID: 3449 RVA: 0x00025AB8 File Offset: 0x00023CB8
		void IHttpResponseElement.Send(HttpWorkerRequest wr)
		{
			int num = this._size - this._free;
			if (num > 0)
			{
				wr.SendResponseFromMemory(this._data, num, true);
			}
		}

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x06000D7A RID: 3450 RVA: 0x00025AE8 File Offset: 0x00023CE8
		internal unsafe IntPtr FreeLocation
		{
			get
			{
				int num = this._size - this._free;
				byte* ptr = (byte*)this._data.ToPointer();
				ptr += num;
				return new IntPtr((void*)ptr);
			}
		}

		// Token: 0x040004F9 RID: 1273
		private IntPtr _data;

		// Token: 0x040004FA RID: 1274
		private static IntPtr s_Pool;
	}
}
