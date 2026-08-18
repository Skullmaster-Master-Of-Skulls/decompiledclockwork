using System;
using System.Net.WebSockets;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading.Tasks;
using System.Web.Util;

namespace System.Web.WebSockets
{
	// Token: 0x020001BE RID: 446
	[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
	internal sealed class WebSocketPipe : IWebSocketPipe
	{
		// Token: 0x06001703 RID: 5891 RVA: 0x000484EF File Offset: 0x000466EF
		internal WebSocketPipe(IUnmanagedWebSocketContext context, IPerfCounters perfCounters)
		{
			this._context = context;
			this._perfCounters = perfCounters;
		}

		// Token: 0x06001704 RID: 5892 RVA: 0x00048508 File Offset: 0x00046708
		public Task WriteFragmentAsync(ArraySegment<byte> buffer, bool isUtf8Encoded, bool isFinalFragment)
		{
			TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();
			PinnedArraySegment<byte> pinnedBuffer = new PinnedArraySegment<byte>(buffer);
			WebSocketPipe.CompletionCallback obj = delegate(int hrError, int cbIO, bool fUtf8Encoded, bool fFinalFragment, bool fClose)
			{
				try
				{
					WebSocketPipe.ThrowExceptionForHR(hrError);
					tcs.TrySetResult(null);
				}
				catch (Exception exception)
				{
					tcs.TrySetException(exception);
				}
				finally
				{
					pinnedBuffer.Dispose();
				}
			};
			IntPtr pvCompletionContext = GCUtil.RootObject(obj);
			this._perfCounters.IncrementCounter(AppPerfCounter.REQUEST_BYTES_OUT_WEBSOCKETS, pinnedBuffer.Count);
			int count = pinnedBuffer.Count;
			bool flag;
			int hrError2 = this._context.WriteFragment(pinnedBuffer.Pointer, ref count, true, isUtf8Encoded, isFinalFragment, WebSocketPipe._asyncThunkAddress, pvCompletionContext, out flag);
			if (!flag)
			{
				WebSocketPipe.AsyncCallbackThunk(hrError2, pvCompletionContext, count, isUtf8Encoded, isFinalFragment, false);
			}
			return tcs.Task;
		}

		// Token: 0x06001705 RID: 5893 RVA: 0x000485AC File Offset: 0x000467AC
		public Task WriteCloseFragmentAsync(WebSocketCloseStatus closeStatus, string statusDescription)
		{
			TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();
			WebSocketPipe.CompletionCallback obj = delegate(int hrError, int cbIO, bool fUtf8Encoded, bool fFinalFragment, bool fClose)
			{
				try
				{
					WebSocketPipe.ThrowExceptionForHR(hrError);
					tcs.TrySetResult(null);
				}
				catch (Exception exception)
				{
					tcs.TrySetException(exception);
				}
			};
			IntPtr pvCompletionContext = GCUtil.RootObject(obj);
			bool flag;
			int hrError2 = this._context.SendConnectionClose(true, (ushort)closeStatus, statusDescription, WebSocketPipe._asyncThunkAddress, pvCompletionContext, out flag);
			if (!flag)
			{
				WebSocketPipe.AsyncCallbackThunk(hrError2, pvCompletionContext, 0, true, true, false);
			}
			return tcs.Task;
		}

		// Token: 0x06001706 RID: 5894 RVA: 0x00048614 File Offset: 0x00046814
		public Task<WebSocketReceiveResult> ReadFragmentAsync(ArraySegment<byte> buffer)
		{
			TaskCompletionSource<WebSocketReceiveResult> tcs = new TaskCompletionSource<WebSocketReceiveResult>();
			PinnedArraySegment<byte> pinnedBuffer = new PinnedArraySegment<byte>(buffer);
			WebSocketPipe.CompletionCallback obj = delegate(int hrError, int cbIO, bool fUtf8Encoded, bool fFinalFragment, bool fClose)
			{
				try
				{
					WebSocketPipe.ThrowExceptionForHR(hrError);
					WebSocketCloseStatus? closeStatus = null;
					string closeStatusDescription = null;
					WebSocketMessageType messageType = fUtf8Encoded ? WebSocketMessageType.Text : WebSocketMessageType.Binary;
					if (fClose)
					{
						messageType = WebSocketMessageType.Close;
						WebSocketCloseStatus value;
						this.GetCloseStatus(out value, out closeStatusDescription);
						closeStatus = new WebSocketCloseStatus?(value);
					}
					else
					{
						this._perfCounters.IncrementCounter(AppPerfCounter.REQUEST_BYTES_IN_WEBSOCKETS, cbIO);
					}
					tcs.TrySetResult(new WebSocketReceiveResult(cbIO, messageType, fFinalFragment, closeStatus, closeStatusDescription));
				}
				catch (Exception exception)
				{
					tcs.TrySetException(exception);
				}
				finally
				{
					pinnedBuffer.Dispose();
				}
			};
			IntPtr pvCompletionContext = GCUtil.RootObject(obj);
			int count = pinnedBuffer.Count;
			bool fUtf8Encoded2;
			bool fFinalFragment2;
			bool fClose2;
			bool flag;
			int hrError2 = this._context.ReadFragment(pinnedBuffer.Pointer, ref count, true, out fUtf8Encoded2, out fFinalFragment2, out fClose2, WebSocketPipe._asyncThunkAddress, pvCompletionContext, out flag);
			if (!flag)
			{
				WebSocketPipe.AsyncCallbackThunk(hrError2, pvCompletionContext, count, fUtf8Encoded2, fFinalFragment2, fClose2);
			}
			return tcs.Task;
		}

		// Token: 0x06001707 RID: 5895 RVA: 0x000486AC File Offset: 0x000468AC
		private unsafe void GetCloseStatus(out WebSocketCloseStatus closeStatus, out string closeStatusDescription)
		{
			ushort num;
			IntPtr zero;
			ushort length;
			int closeStatus2 = this._context.GetCloseStatus(out num, out zero, out length);
			if (closeStatus2 == -2147023728)
			{
				num = 0;
				zero = IntPtr.Zero;
			}
			else
			{
				WebSocketPipe.ThrowExceptionForHR(closeStatus2);
			}
			closeStatus = (WebSocketCloseStatus)num;
			if (zero != IntPtr.Zero)
			{
				closeStatusDescription = new string((char*)((void*)zero), 0, (int)length);
				return;
			}
			closeStatusDescription = null;
		}

		// Token: 0x06001708 RID: 5896 RVA: 0x00048706 File Offset: 0x00046906
		public void CloseTcpConnection()
		{
			this._context.CloseTcpConnection();
		}

		// Token: 0x06001709 RID: 5897 RVA: 0x00048714 File Offset: 0x00046914
		private static void AsyncCallbackThunk(int hrError, IntPtr pvCompletionContext, int cbIO, bool fUtf8Encoded, bool fFinalFragment, bool fClose)
		{
			WebSocketPipe.CompletionCallback completionCallback = (WebSocketPipe.CompletionCallback)GCUtil.UnrootObject(pvCompletionContext);
			completionCallback(hrError, cbIO, fUtf8Encoded, fFinalFragment, fClose);
		}

		// Token: 0x0600170A RID: 5898 RVA: 0x0004873A File Offset: 0x0004693A
		private static void ThrowExceptionForHR(int hrError)
		{
			if (hrError < 0)
			{
				throw new WebSocketException(hrError);
			}
		}

		// Token: 0x040016C3 RID: 5827
		private static readonly WebSocketPipe.CompletionCallbackThunk _asyncThunk = new WebSocketPipe.CompletionCallbackThunk(WebSocketPipe.AsyncCallbackThunk);

		// Token: 0x040016C4 RID: 5828
		private static readonly IntPtr _asyncThunkAddress = Marshal.GetFunctionPointerForDelegate(WebSocketPipe._asyncThunk);

		// Token: 0x040016C5 RID: 5829
		private readonly IUnmanagedWebSocketContext _context;

		// Token: 0x040016C6 RID: 5830
		private readonly IPerfCounters _perfCounters;

		// Token: 0x0200091E RID: 2334
		// (Invoke) Token: 0x06006918 RID: 26904
		private delegate void CompletionCallback(int hrError, int cbIO, bool fUtf8Encoded, bool fFinalFragment, bool fClose);

		// Token: 0x0200091F RID: 2335
		// (Invoke) Token: 0x0600691C RID: 26908
		private delegate void CompletionCallbackThunk(int hrError, IntPtr pvCompletionContext, int cbIO, bool fUtf8Encoded, bool fFinalFragment, bool fClose);
	}
}
