using System;
using System.IdentityModel.Selectors;
using System.ServiceModel.Channels;
using System.ServiceModel.Diagnostics;

namespace System.ServiceModel.Security
{
	// Token: 0x02000326 RID: 806
	internal abstract class CommunicationObjectSecurityTokenAuthenticator : SecurityTokenAuthenticator, ICommunicationObject, ISecurityCommunicationObject
	{
		// Token: 0x06001C74 RID: 7284 RVA: 0x0006A727 File Offset: 0x00068927
		protected CommunicationObjectSecurityTokenAuthenticator()
		{
			this.communicationObject = new WrapperSecurityCommunicationObject(this);
		}

		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x06001C75 RID: 7285 RVA: 0x0006A73B File Offset: 0x0006893B
		protected WrapperSecurityCommunicationObject CommunicationObject
		{
			get
			{
				return this.communicationObject;
			}
		}

		// Token: 0x14000018 RID: 24
		// (add) Token: 0x06001C76 RID: 7286 RVA: 0x0006A743 File Offset: 0x00068943
		// (remove) Token: 0x06001C77 RID: 7287 RVA: 0x0006A751 File Offset: 0x00068951
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

		// Token: 0x14000019 RID: 25
		// (add) Token: 0x06001C78 RID: 7288 RVA: 0x0006A75F File Offset: 0x0006895F
		// (remove) Token: 0x06001C79 RID: 7289 RVA: 0x0006A76D File Offset: 0x0006896D
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

		// Token: 0x1400001A RID: 26
		// (add) Token: 0x06001C7A RID: 7290 RVA: 0x0006A77B File Offset: 0x0006897B
		// (remove) Token: 0x06001C7B RID: 7291 RVA: 0x0006A789 File Offset: 0x00068989
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

		// Token: 0x1400001B RID: 27
		// (add) Token: 0x06001C7C RID: 7292 RVA: 0x0006A797 File Offset: 0x00068997
		// (remove) Token: 0x06001C7D RID: 7293 RVA: 0x0006A7A5 File Offset: 0x000689A5
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

		// Token: 0x1400001C RID: 28
		// (add) Token: 0x06001C7E RID: 7294 RVA: 0x0006A7B3 File Offset: 0x000689B3
		// (remove) Token: 0x06001C7F RID: 7295 RVA: 0x0006A7C1 File Offset: 0x000689C1
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

		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x06001C80 RID: 7296 RVA: 0x0006A7CF File Offset: 0x000689CF
		public CommunicationState State
		{
			get
			{
				return this.communicationObject.State;
			}
		}

		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x06001C81 RID: 7297 RVA: 0x0006A7DC File Offset: 0x000689DC
		public virtual TimeSpan DefaultOpenTimeout
		{
			get
			{
				return ServiceDefaults.OpenTimeout;
			}
		}

		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x06001C82 RID: 7298 RVA: 0x0006A7E3 File Offset: 0x000689E3
		public virtual TimeSpan DefaultCloseTimeout
		{
			get
			{
				return ServiceDefaults.CloseTimeout;
			}
		}

		// Token: 0x06001C83 RID: 7299 RVA: 0x0006A7EA File Offset: 0x000689EA
		public void Abort()
		{
			this.communicationObject.Abort();
		}

		// Token: 0x06001C84 RID: 7300 RVA: 0x0006A7F7 File Offset: 0x000689F7
		public void Close()
		{
			this.communicationObject.Close();
		}

		// Token: 0x06001C85 RID: 7301 RVA: 0x0006A804 File Offset: 0x00068A04
		public void Close(TimeSpan timeout)
		{
			this.communicationObject.Close(timeout);
		}

		// Token: 0x06001C86 RID: 7302 RVA: 0x0006A812 File Offset: 0x00068A12
		public IAsyncResult BeginClose(AsyncCallback callback, object state)
		{
			return this.communicationObject.BeginClose(callback, state);
		}

		// Token: 0x06001C87 RID: 7303 RVA: 0x0006A821 File Offset: 0x00068A21
		public IAsyncResult BeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.communicationObject.BeginClose(timeout, callback, state);
		}

		// Token: 0x06001C88 RID: 7304 RVA: 0x0006A831 File Offset: 0x00068A31
		public void EndClose(IAsyncResult result)
		{
			this.communicationObject.EndClose(result);
		}

		// Token: 0x06001C89 RID: 7305 RVA: 0x0006A83F File Offset: 0x00068A3F
		public void Open()
		{
			this.communicationObject.Open();
		}

		// Token: 0x06001C8A RID: 7306 RVA: 0x0006A84C File Offset: 0x00068A4C
		public void Open(TimeSpan timeout)
		{
			this.communicationObject.Open(timeout);
		}

		// Token: 0x06001C8B RID: 7307 RVA: 0x0006A85A File Offset: 0x00068A5A
		public IAsyncResult BeginOpen(AsyncCallback callback, object state)
		{
			return this.communicationObject.BeginOpen(callback, state);
		}

		// Token: 0x06001C8C RID: 7308 RVA: 0x0006A869 File Offset: 0x00068A69
		public IAsyncResult BeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.communicationObject.BeginOpen(timeout, callback, state);
		}

		// Token: 0x06001C8D RID: 7309 RVA: 0x0006A879 File Offset: 0x00068A79
		public void EndOpen(IAsyncResult result)
		{
			this.communicationObject.EndOpen(result);
		}

		// Token: 0x06001C8E RID: 7310 RVA: 0x0006A887 File Offset: 0x00068A87
		public void Dispose()
		{
			this.Close();
		}

		// Token: 0x06001C8F RID: 7311 RVA: 0x0006A88F File Offset: 0x00068A8F
		public virtual void OnAbort()
		{
		}

		// Token: 0x06001C90 RID: 7312 RVA: 0x0006A891 File Offset: 0x00068A91
		public IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new OperationWithTimeoutAsyncResult(new OperationWithTimeoutCallback(this.OnClose), timeout, callback, state);
		}

		// Token: 0x06001C91 RID: 7313 RVA: 0x0006A8A8 File Offset: 0x00068AA8
		public IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new OperationWithTimeoutAsyncResult(new OperationWithTimeoutCallback(this.OnOpen), timeout, callback, state);
		}

		// Token: 0x06001C92 RID: 7314 RVA: 0x0006A8BF File Offset: 0x00068ABF
		public virtual void OnClose(TimeSpan timeout)
		{
		}

		// Token: 0x06001C93 RID: 7315 RVA: 0x0006A8C1 File Offset: 0x00068AC1
		public virtual void OnClosed()
		{
			SecurityTraceRecordHelper.TraceTokenAuthenticatorClosed(this);
		}

		// Token: 0x06001C94 RID: 7316 RVA: 0x0006A8C9 File Offset: 0x00068AC9
		public virtual void OnClosing()
		{
		}

		// Token: 0x06001C95 RID: 7317 RVA: 0x0006A8CB File Offset: 0x00068ACB
		public void OnEndClose(IAsyncResult result)
		{
			OperationWithTimeoutAsyncResult.End(result);
		}

		// Token: 0x06001C96 RID: 7318 RVA: 0x0006A8D3 File Offset: 0x00068AD3
		public void OnEndOpen(IAsyncResult result)
		{
			OperationWithTimeoutAsyncResult.End(result);
		}

		// Token: 0x06001C97 RID: 7319 RVA: 0x0006A8DB File Offset: 0x00068ADB
		public virtual void OnFaulted()
		{
			this.OnAbort();
		}

		// Token: 0x06001C98 RID: 7320 RVA: 0x0006A8E3 File Offset: 0x00068AE3
		public virtual void OnOpen(TimeSpan timeout)
		{
		}

		// Token: 0x06001C99 RID: 7321 RVA: 0x0006A8E5 File Offset: 0x00068AE5
		public virtual void OnOpened()
		{
			SecurityTraceRecordHelper.TraceTokenAuthenticatorOpened(this);
		}

		// Token: 0x06001C9A RID: 7322 RVA: 0x0006A8ED File Offset: 0x00068AED
		public virtual void OnOpening()
		{
		}

		// Token: 0x04001DCD RID: 7629
		private WrapperSecurityCommunicationObject communicationObject;
	}
}
