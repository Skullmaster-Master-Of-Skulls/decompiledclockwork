using System;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Security
{
	// Token: 0x02000324 RID: 804
	internal class WrapperSecurityCommunicationObject : CommunicationObject
	{
		// Token: 0x06001C3B RID: 7227 RVA: 0x0006A428 File Offset: 0x00068628
		public WrapperSecurityCommunicationObject(ISecurityCommunicationObject innerCommunicationObject)
		{
			if (innerCommunicationObject == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("innerCommunicationObject");
			}
			this.innerCommunicationObject = innerCommunicationObject;
		}

		// Token: 0x06001C3C RID: 7228 RVA: 0x0006A44A File Offset: 0x0006864A
		protected override Type GetCommunicationObjectType()
		{
			return this.innerCommunicationObject.GetType();
		}

		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x06001C3D RID: 7229 RVA: 0x0006A457 File Offset: 0x00068657
		protected override TimeSpan DefaultCloseTimeout
		{
			get
			{
				return this.innerCommunicationObject.DefaultCloseTimeout;
			}
		}

		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x06001C3E RID: 7230 RVA: 0x0006A464 File Offset: 0x00068664
		protected override TimeSpan DefaultOpenTimeout
		{
			get
			{
				return this.innerCommunicationObject.DefaultOpenTimeout;
			}
		}

		// Token: 0x06001C3F RID: 7231 RVA: 0x0006A471 File Offset: 0x00068671
		protected override void OnAbort()
		{
			this.innerCommunicationObject.OnAbort();
		}

		// Token: 0x06001C40 RID: 7232 RVA: 0x0006A47E File Offset: 0x0006867E
		protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.innerCommunicationObject.OnBeginClose(timeout, callback, state);
		}

		// Token: 0x06001C41 RID: 7233 RVA: 0x0006A48E File Offset: 0x0006868E
		protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.innerCommunicationObject.OnBeginOpen(timeout, callback, state);
		}

		// Token: 0x06001C42 RID: 7234 RVA: 0x0006A49E File Offset: 0x0006869E
		protected override void OnClose(TimeSpan timeout)
		{
			this.innerCommunicationObject.OnClose(timeout);
		}

		// Token: 0x06001C43 RID: 7235 RVA: 0x0006A4AC File Offset: 0x000686AC
		protected override void OnClosed()
		{
			this.innerCommunicationObject.OnClosed();
			base.OnClosed();
		}

		// Token: 0x06001C44 RID: 7236 RVA: 0x0006A4BF File Offset: 0x000686BF
		protected override void OnClosing()
		{
			this.innerCommunicationObject.OnClosing();
			base.OnClosing();
		}

		// Token: 0x06001C45 RID: 7237 RVA: 0x0006A4D2 File Offset: 0x000686D2
		protected override void OnEndClose(IAsyncResult result)
		{
			this.innerCommunicationObject.OnEndClose(result);
		}

		// Token: 0x06001C46 RID: 7238 RVA: 0x0006A4E0 File Offset: 0x000686E0
		protected override void OnEndOpen(IAsyncResult result)
		{
			this.innerCommunicationObject.OnEndOpen(result);
		}

		// Token: 0x06001C47 RID: 7239 RVA: 0x0006A4EE File Offset: 0x000686EE
		protected override void OnFaulted()
		{
			this.innerCommunicationObject.OnFaulted();
			base.OnFaulted();
		}

		// Token: 0x06001C48 RID: 7240 RVA: 0x0006A501 File Offset: 0x00068701
		protected override void OnOpen(TimeSpan timeout)
		{
			this.innerCommunicationObject.OnOpen(timeout);
		}

		// Token: 0x06001C49 RID: 7241 RVA: 0x0006A50F File Offset: 0x0006870F
		protected override void OnOpened()
		{
			this.innerCommunicationObject.OnOpened();
			base.OnOpened();
		}

		// Token: 0x06001C4A RID: 7242 RVA: 0x0006A522 File Offset: 0x00068722
		protected override void OnOpening()
		{
			this.innerCommunicationObject.OnOpening();
			base.OnOpening();
		}

		// Token: 0x06001C4B RID: 7243 RVA: 0x0006A535 File Offset: 0x00068735
		internal new void ThrowIfDisposedOrImmutable()
		{
			base.ThrowIfDisposedOrImmutable();
		}

		// Token: 0x04001DCA RID: 7626
		private ISecurityCommunicationObject innerCommunicationObject;
	}
}
