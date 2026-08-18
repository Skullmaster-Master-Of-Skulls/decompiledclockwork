using System;
using System.Threading;
using Renci.SshNet.Messages;
using Renci.SshNet.Messages.Authentication;

namespace Renci.SshNet
{
	// Token: 0x0200001A RID: 26
	public class NoneAuthenticationMethod : AuthenticationMethod, IDisposable
	{
		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000122 RID: 290 RVA: 0x00004404 File Offset: 0x00002604
		public override string Name
		{
			get
			{
				return "none";
			}
		}

		// Token: 0x06000123 RID: 291 RVA: 0x0000440B File Offset: 0x0000260B
		public NoneAuthenticationMethod(string username) : base(username)
		{
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00004428 File Offset: 0x00002628
		public override AuthenticationResult Authenticate(Session session)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			session.UserAuthenticationSuccessReceived += this.Session_UserAuthenticationSuccessReceived;
			session.UserAuthenticationFailureReceived += this.Session_UserAuthenticationFailureReceived;
			try
			{
				session.SendMessage(new RequestMessageNone(ServiceName.Connection, base.Username));
				session.WaitOnHandle(this._authenticationCompleted);
			}
			finally
			{
				session.UserAuthenticationSuccessReceived -= this.Session_UserAuthenticationSuccessReceived;
				session.UserAuthenticationFailureReceived -= this.Session_UserAuthenticationFailureReceived;
			}
			return this._authenticationResult;
		}

		// Token: 0x06000125 RID: 293 RVA: 0x000044C4 File Offset: 0x000026C4
		private void Session_UserAuthenticationSuccessReceived(object sender, MessageEventArgs<SuccessMessage> e)
		{
			this._authenticationResult = AuthenticationResult.Success;
			this._authenticationCompleted.Set();
		}

		// Token: 0x06000126 RID: 294 RVA: 0x000044D9 File Offset: 0x000026D9
		private void Session_UserAuthenticationFailureReceived(object sender, MessageEventArgs<FailureMessage> e)
		{
			if (e.Message.PartialSuccess)
			{
				this._authenticationResult = AuthenticationResult.PartialSuccess;
			}
			else
			{
				this._authenticationResult = AuthenticationResult.Failure;
			}
			base.AllowedAuthentications = e.Message.AllowedAuthentications;
			this._authenticationCompleted.Set();
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00004515 File Offset: 0x00002715
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00004524 File Offset: 0x00002724
		protected virtual void Dispose(bool disposing)
		{
			if (this._isDisposed)
			{
				return;
			}
			if (disposing)
			{
				EventWaitHandle authenticationCompleted = this._authenticationCompleted;
				if (authenticationCompleted != null)
				{
					authenticationCompleted.Dispose();
					this._authenticationCompleted = null;
				}
				this._isDisposed = true;
			}
		}

		// Token: 0x06000129 RID: 297 RVA: 0x0000455C File Offset: 0x0000275C
		~NoneAuthenticationMethod()
		{
			this.Dispose(false);
		}

		// Token: 0x0400004F RID: 79
		private AuthenticationResult _authenticationResult = AuthenticationResult.Failure;

		// Token: 0x04000050 RID: 80
		private EventWaitHandle _authenticationCompleted = new AutoResetEvent(false);

		// Token: 0x04000051 RID: 81
		private bool _isDisposed;
	}
}
