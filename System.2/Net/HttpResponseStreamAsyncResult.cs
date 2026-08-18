using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Net
{
	// Token: 0x020001C0 RID: 448
	internal class HttpResponseStreamAsyncResult : LazyAsyncResult
	{
		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x060011AC RID: 4524 RVA: 0x0005FF64 File Offset: 0x0005E164
		internal ushort dataChunkCount
		{
			get
			{
				if (this.m_DataChunks == null)
				{
					return 0;
				}
				return (ushort)this.m_DataChunks.Length;
			}
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x060011AD RID: 4525 RVA: 0x0005FF79 File Offset: 0x0005E179
		internal unsafe UnsafeNclNativeMethods.HttpApi.HTTP_DATA_CHUNK* pDataChunks
		{
			get
			{
				if (this.m_DataChunks == null)
				{
					return null;
				}
				return (UnsafeNclNativeMethods.HttpApi.HTTP_DATA_CHUNK*)((void*)Marshal.UnsafeAddrOfPinnedArrayElement(this.m_DataChunks, 0));
			}
		}

		// Token: 0x060011AE RID: 4526 RVA: 0x0005FF97 File Offset: 0x0005E197
		internal HttpResponseStreamAsyncResult(object asyncObject, object userState, AsyncCallback callback) : base(asyncObject, userState, callback)
		{
		}

		// Token: 0x060011AF RID: 4527 RVA: 0x0005FFA4 File Offset: 0x0005E1A4
		internal unsafe HttpResponseStreamAsyncResult(object asyncObject, object userState, AsyncCallback callback, byte[] buffer, int offset, int size, bool chunked, bool sentHeaders) : base(asyncObject, userState, callback)
		{
			this.m_SentHeaders = sentHeaders;
			Overlapped overlapped = new Overlapped();
			overlapped.AsyncResult = this;
			if (size == 0)
			{
				this.m_DataChunks = null;
				this.m_pOverlapped = overlapped.Pack(HttpResponseStreamAsyncResult.s_IOCallback, null);
				return;
			}
			this.m_DataChunks = new UnsafeNclNativeMethods.HttpApi.HTTP_DATA_CHUNK[chunked ? 3 : 1];
			object[] array = new object[1 + this.m_DataChunks.Length];
			array[this.m_DataChunks.Length] = this.m_DataChunks;
			int num = 0;
			byte[] array2 = null;
			if (chunked)
			{
				array2 = ConnectStream.GetChunkHeader(size, out num);
				this.m_DataChunks[0] = default(UnsafeNclNativeMethods.HttpApi.HTTP_DATA_CHUNK);
				this.m_DataChunks[0].DataChunkType = UnsafeNclNativeMethods.HttpApi.HTTP_DATA_CHUNK_TYPE.HttpDataChunkFromMemory;
				this.m_DataChunks[0].BufferLength = (uint)(array2.Length - num);
				array[0] = array2;
				this.m_DataChunks[1] = default(UnsafeNclNativeMethods.HttpApi.HTTP_DATA_CHUNK);
				this.m_DataChunks[1].DataChunkType = UnsafeNclNativeMethods.HttpApi.HTTP_DATA_CHUNK_TYPE.HttpDataChunkFromMemory;
				this.m_DataChunks[1].BufferLength = (uint)size;
				array[1] = buffer;
				this.m_DataChunks[2] = default(UnsafeNclNativeMethods.HttpApi.HTTP_DATA_CHUNK);
				this.m_DataChunks[2].DataChunkType = UnsafeNclNativeMethods.HttpApi.HTTP_DATA_CHUNK_TYPE.HttpDataChunkFromMemory;
				this.m_DataChunks[2].BufferLength = (uint)NclConstants.CRLF.Length;
				array[2] = NclConstants.CRLF;
			}
			else
			{
				this.m_DataChunks[0] = default(UnsafeNclNativeMethods.HttpApi.HTTP_DATA_CHUNK);
				this.m_DataChunks[0].DataChunkType = UnsafeNclNativeMethods.HttpApi.HTTP_DATA_CHUNK_TYPE.HttpDataChunkFromMemory;
				this.m_DataChunks[0].BufferLength = (uint)size;
				array[0] = buffer;
			}
			this.m_pOverlapped = overlapped.Pack(HttpResponseStreamAsyncResult.s_IOCallback, array);
			if (chunked)
			{
				this.m_DataChunks[0].pBuffer = (byte*)((void*)Marshal.UnsafeAddrOfPinnedArrayElement(array2, num));
				this.m_DataChunks[1].pBuffer = (byte*)((void*)Marshal.UnsafeAddrOfPinnedArrayElement(buffer, offset));
				this.m_DataChunks[2].pBuffer = (byte*)((void*)Marshal.UnsafeAddrOfPinnedArrayElement(NclConstants.CRLF, 0));
				return;
			}
			this.m_DataChunks[0].pBuffer = (byte*)((void*)Marshal.UnsafeAddrOfPinnedArrayElement(buffer, offset));
		}

		// Token: 0x060011B0 RID: 4528 RVA: 0x000601C4 File Offset: 0x0005E3C4
		internal void IOCompleted(uint errorCode, uint numBytes)
		{
			HttpResponseStreamAsyncResult.IOCompleted(this, errorCode, numBytes);
		}

		// Token: 0x060011B1 RID: 4529 RVA: 0x000601D0 File Offset: 0x0005E3D0
		private unsafe static void IOCompleted(HttpResponseStreamAsyncResult asyncResult, uint errorCode, uint numBytes)
		{
			object result = null;
			try
			{
				if (errorCode != 0U && errorCode != 38U)
				{
					asyncResult.ErrorCode = (int)errorCode;
					result = new HttpListenerException((int)errorCode);
				}
				else if (asyncResult.m_DataChunks == null)
				{
					result = 0U;
					if (Logging.On)
					{
						Logging.Dump(Logging.HttpListener, asyncResult, "Callback", IntPtr.Zero, 0);
					}
				}
				else
				{
					result = ((asyncResult.m_DataChunks.Length == 1) ? asyncResult.m_DataChunks[0].BufferLength : 0U);
					if (Logging.On)
					{
						for (int i = 0; i < asyncResult.m_DataChunks.Length; i++)
						{
							Logging.Dump(Logging.HttpListener, asyncResult, "Callback", (IntPtr)((void*)asyncResult.m_DataChunks[0].pBuffer), (int)asyncResult.m_DataChunks[0].BufferLength);
						}
					}
				}
			}
			catch (Exception ex)
			{
				result = ex;
			}
			asyncResult.InvokeCallback(result);
		}

		// Token: 0x060011B2 RID: 4530 RVA: 0x000602C0 File Offset: 0x0005E4C0
		private unsafe static void Callback(uint errorCode, uint numBytes, NativeOverlapped* nativeOverlapped)
		{
			Overlapped overlapped = Overlapped.Unpack(nativeOverlapped);
			HttpResponseStreamAsyncResult asyncResult = overlapped.AsyncResult as HttpResponseStreamAsyncResult;
			HttpResponseStreamAsyncResult.IOCompleted(asyncResult, errorCode, numBytes);
		}

		// Token: 0x060011B3 RID: 4531 RVA: 0x000602E8 File Offset: 0x0005E4E8
		protected override void Cleanup()
		{
			base.Cleanup();
			if (this.m_pOverlapped != null)
			{
				Overlapped.Free(this.m_pOverlapped);
			}
		}

		// Token: 0x04001469 RID: 5225
		internal unsafe NativeOverlapped* m_pOverlapped;

		// Token: 0x0400146A RID: 5226
		private UnsafeNclNativeMethods.HttpApi.HTTP_DATA_CHUNK[] m_DataChunks;

		// Token: 0x0400146B RID: 5227
		internal bool m_SentHeaders;

		// Token: 0x0400146C RID: 5228
		private static readonly IOCompletionCallback s_IOCallback = new IOCompletionCallback(HttpResponseStreamAsyncResult.Callback);
	}
}
