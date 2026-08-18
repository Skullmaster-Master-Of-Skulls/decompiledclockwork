using System;
using System.Threading;

namespace System.Net
{
	// Token: 0x020001BD RID: 445
	internal class ListenerAsyncResult : LazyAsyncResult
	{
		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06001174 RID: 4468 RVA: 0x0005EB65 File Offset: 0x0005CD65
		internal static IOCompletionCallback IOCallback
		{
			get
			{
				return ListenerAsyncResult.s_IOCallback;
			}
		}

		// Token: 0x06001175 RID: 4469 RVA: 0x0005EB6C File Offset: 0x0005CD6C
		internal ListenerAsyncResult(object asyncObject, object userState, AsyncCallback callback) : base(asyncObject, userState, callback)
		{
			this.m_RequestContext = new AsyncRequestContext(this);
		}

		// Token: 0x06001176 RID: 4470 RVA: 0x0005EB84 File Offset: 0x0005CD84
		private unsafe static void IOCompleted(ListenerAsyncResult asyncResult, uint errorCode, uint numBytes)
		{
			object obj = null;
			try
			{
				if (errorCode != 0U && errorCode != 234U)
				{
					asyncResult.ErrorCode = (int)errorCode;
					obj = new HttpListenerException((int)errorCode);
				}
				else
				{
					HttpListener httpListener = asyncResult.AsyncObject as HttpListener;
					if (errorCode == 0U)
					{
						bool flag = false;
						try
						{
							if (httpListener.ValidateRequest(asyncResult.m_RequestContext))
							{
								obj = httpListener.HandleAuthentication(asyncResult.m_RequestContext, out flag);
							}
							goto IL_92;
						}
						finally
						{
							if (flag)
							{
								asyncResult.m_RequestContext = ((obj == null) ? new AsyncRequestContext(asyncResult) : null);
							}
							else
							{
								asyncResult.m_RequestContext.Reset(0UL, 0U);
							}
						}
					}
					asyncResult.m_RequestContext.Reset(asyncResult.m_RequestContext.RequestBlob->RequestId, numBytes);
					IL_92:
					if (obj == null)
					{
						uint num = asyncResult.QueueBeginGetContext();
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
				if (Logging.On)
				{
					Logging.PrintError(Logging.HttpListener, ValidationHelper.HashString(asyncResult), "IOCompleted", ex.ToString());
				}
				obj = ex;
			}
			asyncResult.InvokeCallback(obj);
		}

		// Token: 0x06001177 RID: 4471 RVA: 0x0005ECA0 File Offset: 0x0005CEA0
		private unsafe static void WaitCallback(uint errorCode, uint numBytes, NativeOverlapped* nativeOverlapped)
		{
			Overlapped overlapped = Overlapped.Unpack(nativeOverlapped);
			ListenerAsyncResult asyncResult = (ListenerAsyncResult)overlapped.AsyncResult;
			ListenerAsyncResult.IOCompleted(asyncResult, errorCode, numBytes);
		}

		// Token: 0x06001178 RID: 4472 RVA: 0x0005ECC8 File Offset: 0x0005CEC8
		internal unsafe uint QueueBeginGetContext()
		{
			uint num;
			uint num2;
			for (;;)
			{
				(base.AsyncObject as HttpListener).EnsureBoundHandle();
				num = 0U;
				num2 = UnsafeNclNativeMethods.HttpApi.HttpReceiveHttpRequest((base.AsyncObject as HttpListener).RequestQueueHandle, this.m_RequestContext.RequestBlob->RequestId, 1U, this.m_RequestContext.RequestBlob, this.m_RequestContext.Size, &num, this.m_RequestContext.NativeOverlapped);
				if (num2 == 87U && this.m_RequestContext.RequestBlob->RequestId != 0UL)
				{
					this.m_RequestContext.RequestBlob->RequestId = 0UL;
				}
				else
				{
					if (num2 != 234U)
					{
						break;
					}
					this.m_RequestContext.Reset(this.m_RequestContext.RequestBlob->RequestId, num);
				}
			}
			if (num2 == 0U && HttpListener.SkipIOCPCallbackOnSuccess)
			{
				ListenerAsyncResult.IOCompleted(this, num2, num);
			}
			return num2;
		}

		// Token: 0x06001179 RID: 4473 RVA: 0x0005ED9E File Offset: 0x0005CF9E
		protected override void Cleanup()
		{
			if (this.m_RequestContext != null)
			{
				this.m_RequestContext.ReleasePins();
				this.m_RequestContext.Close();
			}
			base.Cleanup();
		}

		// Token: 0x0400145C RID: 5212
		private static readonly IOCompletionCallback s_IOCallback = new IOCompletionCallback(ListenerAsyncResult.WaitCallback);

		// Token: 0x0400145D RID: 5213
		private AsyncRequestContext m_RequestContext;
	}
}
