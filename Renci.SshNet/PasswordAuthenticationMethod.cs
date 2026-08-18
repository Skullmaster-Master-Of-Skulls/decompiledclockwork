using System;
using System.Text;
using System.Threading;
using Renci.SshNet.Abstractions;
using Renci.SshNet.Common;
using Renci.SshNet.Messages;
using Renci.SshNet.Messages.Authentication;

namespace Renci.SshNet
{
	// Token: 0x0200001B RID: 27
	public class PasswordAuthenticationMethod : AuthenticationMethod, IDisposable
	{
		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600012A RID: 298 RVA: 0x0000458C File Offset: 0x0000278C
		public override string Name
		{
			get
			{
				return this._requestMessage.MethodName;
			}
		}

		// Token: 0x14000019 RID: 25
		// (add) Token: 0x0600012B RID: 299 RVA: 0x0000459C File Offset: 0x0000279C
		// (remove) Token: 0x0600012C RID: 300 RVA: 0x000045D4 File Offset: 0x000027D4
		public event EventHandler<AuthenticationPasswordChangeEventArgs> PasswordExpired;

		// Token: 0x0600012D RID: 301 RVA: 0x00004609 File Offset: 0x00002809
		public PasswordAuthenticationMethod(string username, string password) : this(username, Encoding.UTF8.GetBytes(password))
		{
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00004620 File Offset: 0x00002820
		public PasswordAuthenticationMethod(string username, byte[] password) : base(username)
		{
			if (password == null)
			{
				throw new ArgumentNullException("password");
			}
			this._password = password;
			this._requestMessage = new RequestMessagePassword(ServiceName.Connection, base.Username, this._password);
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00004674 File Offset: 0x00002874
		public override AuthenticationResult Authenticate(Session session)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			this._session = session;
			session.UserAuthenticationSuccessReceived += this.Session_UserAuthenticationSuccessReceived;
			session.UserAuthenticationFailureReceived += this.Session_UserAuthenticationFailureReceived;
			session.MessageReceived += this.Session_MessageReceived;
			try
			{
				session.RegisterMessage("SSH_MSG_USERAUTH_PASSWD_CHANGEREQ");
				session.SendMessage(this._requestMessage);
				session.WaitOnHandle(this._authenticationCompleted);
			}
			finally
			{
				session.UnRegisterMessage("SSH_MSG_USERAUTH_PASSWD_CHANGEREQ");
				session.UserAuthenticationSuccessReceived -= this.Session_UserAuthenticationSuccessReceived;
				session.UserAuthenticationFailureReceived -= this.Session_UserAuthenticationFailureReceived;
				session.MessageReceived -= this.Session_MessageReceived;
			}
			if (this._exception != null)
			{
				throw this._exception;
			}
			return this._authenticationResult;
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00004758 File Offset: 0x00002958
		private void Session_UserAuthenticationSuccessReceived(object sender, MessageEventArgs<SuccessMessage> e)
		{
			this._authenticationResult = AuthenticationResult.Success;
			this._authenticationCompleted.Set();
		}

		// Token: 0x06000131 RID: 305 RVA: 0x0000476D File Offset: 0x0000296D
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

		// Token: 0x06000132 RID: 306 RVA: 0x000047A9 File Offset: 0x000029A9
		private void Session_MessageReceived(object sender, MessageEventArgs<Message> e)
		{
			if (e.Message is PasswordChangeRequiredMessage)
			{
				this._session.UnRegisterMessage("SSH_MSG_USERAUTH_PASSWD_CHANGEREQ");
				ThreadAbstraction.ExecuteThread(delegate
				{
					try
					{
						AuthenticationPasswordChangeEventArgs authenticationPasswordChangeEventArgs = new AuthenticationPasswordChangeEventArgs(base.Username);
						if (this.PasswordExpired != null)
						{
							this.PasswordExpired(this, authenticationPasswordChangeEventArgs);
						}
						this._session.SendMessage(new RequestMessagePassword(ServiceName.Connection, base.Username, this._password, authenticationPasswordChangeEventArgs.NewPassword));
					}
					catch (Exception exception)
					{
						this._exception = exception;
						this._authenticationCompleted.Set();
					}
				});
			}
		}

		// Token: 0x06000133 RID: 307 RVA: 0x000047D9 File Offset: 0x000029D9
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000134 RID: 308 RVA: 0x000047E8 File Offset: 0x000029E8
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

		// Token: 0x06000135 RID: 309 RVA: 0x00004820 File Offset: 0x00002A20
		~PasswordAuthenticationMethod()
		{
			this.Dispose(false);
		}

		// Token: 0x04000052 RID: 82
		private AuthenticationResult _authenticationResult = AuthenticationResult.Failure;

		// Token: 0x04000053 RID: 83
		private Session _session;

		// Token: 0x04000054 RID: 84
		private EventWaitHandle _authenticationCompleted = new AutoResetEvent(false);

		// Token: 0x04000055 RID: 85
		private Exception _exception;

		// Token: 0x04000056 RID: 86
		private readonly RequestMessage _requestMessage;

		// Token: 0x04000057 RID: 87
		private readonly byte[] _password;

		// Token: 0x04000059 RID: 89
		private bool _isDisposed;
	}
}
