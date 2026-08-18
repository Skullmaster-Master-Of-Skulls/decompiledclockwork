using System;
using System.IdentityModel.Selectors;
using System.Runtime.Diagnostics;
using System.ServiceModel.Channels;
using System.ServiceModel.Diagnostics;

namespace System.ServiceModel.Security
{
	// Token: 0x02000325 RID: 805
	internal abstract class CommunicationObjectSecurityTokenProvider : SecurityTokenProvider, ICommunicationObject, ISecurityCommunicationObject
	{
		// Token: 0x06001C4C RID: 7244 RVA: 0x0006A53D File Offset: 0x0006873D
		protected CommunicationObjectSecurityTokenProvider()
		{
			this.communicationObject = new WrapperSecurityCommunicationObject(this);
		}

		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x06001C4D RID: 7245 RVA: 0x0006A551 File Offset: 0x00068751
		internal EventTraceActivity EventTraceActivity
		{
			get
			{
				if (this.eventTraceActivity == null)
				{
					this.eventTraceActivity = EventTraceActivity.GetFromThreadOrCreate(false);
				}
				return this.eventTraceActivity;
			}
		}

		// Token: 0x17000719 RID: 1817
		// (get) Token: 0x06001C4E RID: 7246 RVA: 0x0006A56D File Offset: 0x0006876D
		protected WrapperSecurityCommunicationObject CommunicationObject
		{
			get
			{
				return this.communicationObject;
			}
		}

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x06001C4F RID: 7247 RVA: 0x0006A575 File Offset: 0x00068775
		// (remove) Token: 0x06001C50 RID: 7248 RVA: 0x0006A583 File Offset: 0x00068783
		public event EventHandler Closed
		{
			add
			{
				this.communicationObject.Closed += value;
			}
			remove
			{
				this.communicationObject.Closed -= value;
			}
		}

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x06001C51 RID: 7249 RVA: 0x0006A591 File Offset: 0x00068791
		// (remove) Token: 0x06001C52 RID: 7250 RVA: 0x0006A59F File Offset: 0x0006879F
		public event EventHandler Closing
		{
			add
			{
				this.communicationObject.Closing += value;
			}
			remove
			{
				this.communicationObject.Closing -= value;
			}
		}

		// Token: 0x14000015 RID: 21
		// (add) Token: 0x06001C53 RID: 7251 RVA: 0x0006A5AD File Offset: 0x000687AD
		// (remove) Token: 0x06001C54 RID: 7252 RVA: 0x0006A5BB File Offset: 0x000687BB
		public event EventHandler Faulted
		{
			add
			{
				this.communicationObject.Faulted += value;
			}
			remove
			{
				this.communicationObject.Faulted -= value;
			}
		}

		// Token: 0x14000016 RID: 22
		// (add) Token: 0x06001C55 RID: 7253 RVA: 0x0006A5C9 File Offset: 0x000687C9
		// (remove) Token: 0x06001C56 RID: 7254 RVA: 0x0006A5D7 File Offset: 0x000687D7
		public event EventHandler Opened
		{
			add
			{
				this.communicationObject.Opened += value;
			}
			remove
			{
				this.communicationObject.Opened -= value;
			}
		}

		// Token: 0x14000017 RID: 23
		// (add) Token: 0x06001C57 RID: 7255 RVA: 0x0006A5E5 File Offset: 0x000687E5
		// (remove) Token: 0x06001C58 RID: 7256 RVA: 0x0006A5F3 File Offset: 0x000687F3
		public event EventHandler Opening
		{
			add
			{
				this.communicationObject.Opening += value;
			}
			remove
			{
				this.communicationObject.Opening -= value;
			}
		}

		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x06001C59 RID: 7257 RVA: 0x0006A601 File Offset: 0x00068801
		public CommunicationState State
		{
			get
			{
				return this.communicationObject.State;
			}
		}

		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x06001C5A RID: 7258 RVA: 0x0006A60E File Offset: 0x0006880E
		public virtual TimeSpan DefaultOpenTimeout
		{
			get
			{
				return ServiceDefaults.OpenTimeout;
			}
		}

		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x06001C5B RID: 7259 RVA: 0x0006A615 File Offset: 0x00068815
		public virtual TimeSpan DefaultCloseTimeout
		{
			get
			{
				return ServiceDefaults.CloseTimeout;
			}
		}

		// Token: 0x06001C5C RID: 7260 RVA: 0x0006A61C File Offset: 0x0006881C
		public void Abort()
		{
			this.communicationObject.Abort();
		}

		// Token: 0x06001C5D RID: 7261 RVA: 0x0006A629 File Offset: 0x00068829
		public void Close()
		{
			this.communicationObject.Close();
		}

		// Token: 0x06001C5E RID: 7262 RVA: 0x0006A636 File Offset: 0x00068836
		public void Close(TimeSpan timeout)
		{
			this.communicationObject.Close(timeout);
		}

		// Token: 0x06001C5F RID: 7263 RVA: 0x0006A644 File Offset: 0x00068844
		public IAsyncResult BeginClose(AsyncCallback callback, object state)
		{
			return this.communicationObject.BeginClose(callback, state);
		}

		// Token: 0x06001C60 RID: 7264 RVA: 0x0006A653 File Offset: 0x00068853
		public IAsyncResult BeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.communicationObject.BeginClose(timeout, callback, state);
		}

		// Token: 0x06001C61 RID: 7265 RVA: 0x0006A663 File Offset: 0x00068863
		public void EndClose(IAsyncResult result)
		{
			this.communicationObject.EndClose(result);
		}

		// Token: 0x06001C62 RID: 7266 RVA: 0x0006A671 File Offset: 0x00068871
		public void Open()
		{
			this.communicationObject.Open();
		}

		// Token: 0x06001C63 RID: 7267 RVA: 0x0006A67E File Offset: 0x0006887E
		public void Open(TimeSpan timeout)
		{
			this.communicationObject.Open(timeout);
		}

		// Token: 0x06001C64 RID: 7268 RVA: 0x0006A68C File Offset: 0x0006888C
		public IAsyncResult BeginOpen(AsyncCallback callback, object state)
		{
			return this.communicationObject.BeginOpen(callback, state);
		}

		// Token: 0x06001C65 RID: 7269 RVA: 0x0006A69B File Offset: 0x0006889B
		public IAsyncResult BeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.communicationObject.BeginOpen(timeout, callback, state);
		}

		// Token: 0x06001C66 RID: 7270 RVA: 0x0006A6AB File Offset: 0x000688AB
		public void EndOpen(IAsyncResult result)
		{
			this.communicationObject.EndOpen(result);
		}

		// Token: 0x06001C67 RID: 7271 RVA: 0x0006A6B9 File Offset: 0x000688B9
		public void Dispose()
		{
			this.Close();
		}

		// Token: 0x06001C68 RID: 7272 RVA: 0x0006A6C1 File Offset: 0x000688C1
		public virtual void OnAbort()
		{
		}

		// Token: 0x06001C69 RID: 7273 RVA: 0x0006A6C3 File Offset: 0x000688C3
		public IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new OperationWithTimeoutAsyncResult(new OperationWithTimeoutCallback(this.OnClose), timeout, callback, state);
		}

		// Token: 0x06001C6A RID: 7274 RVA: 0x0006A6DA File Offset: 0x000688DA
		public IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new OperationWithTimeoutAsyncResult(new OperationWithTimeoutCallback(this.OnOpen), timeout, callback, state);
		}

		// Token: 0x06001C6B RID: 7275 RVA: 0x0006A6F1 File Offset: 0x000688F1
		public virtual void OnClose(TimeSpan timeout)
		{
		}

		// Token: 0x06001C6C RID: 7276 RVA: 0x0006A6F3 File Offset: 0x000688F3
		public virtual void OnClosed()
		{
			SecurityTraceRecordHelper.TraceTokenProviderClosed(this);
		}

		// Token: 0x06001C6D RID: 7277 RVA: 0x0006A6FB File Offset: 0x000688FB
		public virtual void OnClosing()
		{
		}

		// Token: 0x06001C6E RID: 7278 RVA: 0x0006A6FD File Offset: 0x000688FD
		public void OnEndClose(IAsyncResult result)
		{
			OperationWithTimeoutAsyncResult.End(result);
		}

		// Token: 0x06001C6F RID: 7279 RVA: 0x0006A705 File Offset: 0x00068905
		public void OnEndOpen(IAsyncResult result)
		{
			OperationWithTimeoutAsyncResult.End(result);
		}

		// Token: 0x06001C70 RID: 7280 RVA: 0x0006A70D File Offset: 0x0006890D
		public virtual void OnFaulted()
		{
			this.OnAbort();
		}

		// Token: 0x06001C71 RID: 7281 RVA: 0x0006A715 File Offset: 0x00068915
		public virtual void OnOpen(TimeSpan timeout)
		{
		}

		// Token: 0x06001C72 RID: 7282 RVA: 0x0006A717 File Offset: 0x00068917
		public virtual void OnOpened()
		{
			SecurityTraceRecordHelper.TraceTokenProviderOpened(this.EventTraceActivity, this);
		}

		// Token: 0x06001C73 RID: 7283 RVA: 0x0006A725 File Offset: 0x00068925
		public virtual void OnOpening()
		{
		}

		// Token: 0x04001DCB RID: 7627
		private EventTraceActivity eventTraceActivity;

		// Token: 0x04001DCC RID: 7628
		private WrapperSecurityCommunicationObject communicationObject;
	}
}
