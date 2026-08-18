using System;
using System.Threading;

namespace System.Net
{
	// Token: 0x020004E5 RID: 1253
	internal class ListenerAsyncResult : LazyAsyncResult
	{
		// Token: 0x17000815 RID: 2069
		// (get) Token: 0x060026F8 RID: 9976 RVA: 0x000A0E0B File Offset: 0x0009FE0B
		internal static IOCompletionCallback IOCallback
		{
			get
			{
				return ListenerAsyncResult.s_IOCallback;
			}
		}

		// Token: 0x060026F9 RID: 9977 RVA: 0x000A0E12 File Offset: 0x0009FE12
		internal ListenerAsyncResult(object asyncObject, object userState, AsyncCallback callback) : base(asyncObject, userState, callback)
		{
			this.m_RequestContext = new AsyncRequestContext(this);
		}

		// Token: 0x060026FA RID: 9978 RVA: 0x000A0E2C File Offset: 0x0009FE2C
		private unsafe static void WaitCallback(uint errorCode, uint numBytes, NativeOverlapped* nativeOverlapped)
		{
			Overlapped overlapped = Overlapped.Unpack(nativeOverlapped);
			ListenerAsyncResult listenerAsyncResult = (ListenerAsyncResult)overlapped.AsyncResult;
			object obj = null;
			try
			{
				if (errorCode != 0U && errorCode != 234U)
				{
					listenerAsyncResult.ErrorCode = (int)errorCode;
					obj = new HttpListenerException((int)errorCode);
				}
				else
				{
					HttpListener httpListener = listenerAsyncResult.AsyncObject as HttpListener;
					if (errorCode == 0U)
					{
						bool flag = false;
						try
						{
							obj = httpListener.HandleAuthentication(listenerAsyncResult.m_RequestContext, out flag);
							goto IL_99;
						}
						finally
						{
							if (flag)
							{
								listenerAsyncResult.m_RequestContext = ((obj == null) ? new AsyncRequestContext(listenerAsyncResult) : null);
							}
							else
							{
								listenerAsyncResult.m_RequestContext.Reset(0UL, 0U);
							}
						}
					}
					listenerAsyncResult.m_RequestContext.Reset(listenerAsyncResult.m_RequestContext.RequestBlob->RequestId, numBytes);
					IL_99:
					if (obj == null)
					{
						uint num = listenerAsyncResult.QueueBeginGetContext();
						if (num != 0U && num != 997U)
						{
							obj = new HttpListenerException((int)num);
						}
					}
					if (obj == null)
					{
						return;
					}
				}
			}
			catch (Exception ex)
			{
				if (NclUtilities.IsFatal(ex))
				{
					throw;
				}
				obj = ex;
			}
			listenerAsyncResult.InvokeCallback(obj);
		}

		// Token: 0x060026FB RID: 9979 RVA: 0x000A0F30 File Offset: 0x0009FF30
		internal unsafe uint QueueBeginGetContext()
		{
			uint num;
			for (;;)
			{
				(base.AsyncObject as HttpListener).EnsureBoundHandle();
				uint size = 0U;
				num = UnsafeNclNativeMethods.HttpApi.HttpReceiveHttpRequest((base.AsyncObject as HttpListener).RequestQueueHandle, this.m_RequestContext.RequestBlob->RequestId, 1U, this.m_RequestContext.RequestBlob, this.m_RequestContext.Size, &size, this.m_RequestContext.NativeOverlapped);
				if (num == 87U && this.m_RequestContext.RequestBlob->RequestId != 0UL)
				{
					this.m_RequestContext.RequestBlob->RequestId = 0UL;
				}
				else
				{
					if (num != 234U)
					{
						break;
					}
					this.m_RequestContext.Reset(this.m_RequestContext.RequestBlob->RequestId, size);
				}
			}
			return num;
		}

		// Token: 0x060026FC RID: 9980 RVA: 0x000A0FF6 File Offset: 0x0009FFF6
		protected override void Cleanup()
		{
			if (this.m_RequestContext != null)
			{
				this.m_RequestContext.ReleasePins();
				this.m_RequestContext.Close();
			}
			base.Cleanup();
		}

		// Token: 0x0400269E RID: 9886
		private static readonly IOCompletionCallback s_IOCallback = new IOCompletionCallback(ListenerAsyncResult.WaitCallback);

		// Token: 0x0400269F RID: 9887
		private AsyncRequestContext m_RequestContext;
	}
}
