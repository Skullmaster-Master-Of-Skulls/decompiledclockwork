using System;
using System.Linq;
using System.Threading;
using Renci.SshNet.Abstractions;
using Renci.SshNet.Common;
using Renci.SshNet.Messages;
using Renci.SshNet.Messages.Authentication;

namespace Renci.SshNet
{
	// Token: 0x02000018 RID: 24
	public class KeyboardInteractiveAuthenticationMethod : AuthenticationMethod, IDisposable
	{
		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00003E92 File Offset: 0x00002092
		public override string Name
		{
			get
			{
				return this._requestMessage.MethodName;
			}
		}

		// Token: 0x14000017 RID: 23
		// (add) Token: 0x0600010A RID: 266 RVA: 0x00003EA0 File Offset: 0x000020A0
		// (remove) Token: 0x0600010B RID: 267 RVA: 0x00003ED8 File Offset: 0x000020D8
		public event EventHandler<AuthenticationPromptEventArgs> AuthenticationPrompt;

		// Token: 0x0600010C RID: 268 RVA: 0x00003F0D File Offset: 0x0000210D
		public KeyboardInteractiveAuthenticationMethod(string username) : base(username)
		{
			this._requestMessage = new RequestMessageKeyboardInteractive(ServiceName.Connection, username);
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00003F38 File Offset: 0x00002138
		public override AuthenticationResult Authenticate(Session session)
		{
			this._session = session;
			session.UserAuthenticationSuccessReceived += this.Session_UserAuthenticationSuccessReceived;
			session.UserAuthenticationFailureReceived += this.Session_UserAuthenticationFailureReceived;
			session.MessageReceived += this.Session_MessageReceived;
			session.RegisterMessage("SSH_MSG_USERAUTH_INFO_REQUEST");
			try
			{
				session.SendMessage(this._requestMessage);
				session.WaitOnHandle(this._authenticationCompleted);
			}
			finally
			{
				session.UnRegisterMessage("SSH_MSG_USERAUTH_INFO_REQUEST");
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

		// Token: 0x0600010E RID: 270 RVA: 0x00004010 File Offset: 0x00002210
		private void Session_UserAuthenticationSuccessReceived(object sender, MessageEventArgs<SuccessMessage> e)
		{
			this._authenticationResult = AuthenticationResult.Success;
			this._authenticationCompleted.Set();
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00004025 File Offset: 0x00002225
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

		// Token: 0x06000110 RID: 272 RVA: 0x00004064 File Offset: 0x00002264
		private void Session_MessageReceived(object sender, MessageEventArgs<Message> e)
		{
			InformationRequestMessage informationRequestMessage = e.Message as InformationRequestMessage;
			if (informationRequestMessage != null)
			{
				AuthenticationPromptEventArgs eventArgs = new AuthenticationPromptEventArgs(base.Username, informationRequestMessage.Instruction, informationRequestMessage.Language, informationRequestMessage.Prompts);
				ThreadAbstraction.ExecuteThread(delegate
				{
					try
					{
						if (this.AuthenticationPrompt != null)
						{
							this.AuthenticationPrompt(this, eventArgs);
						}
						InformationResponseMessage informationResponseMessage = new InformationResponseMessage();
						foreach (string item in from r in eventArgs.Prompts
						orderby r.Id
						select r.Response)
						{
							informationResponseMessage.Responses.Add(item);
						}
						this._session.SendMessage(informationResponseMessage);
					}
					catch (Exception exception)
					{
						this._exception = exception;
						this._authenticationCompleted.Set();
					}
				});
			}
		}

		// Token: 0x06000111 RID: 273 RVA: 0x000040BF File Offset: 0x000022BF
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x000040D0 File Offset: 0x000022D0
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

		// Token: 0x06000113 RID: 275 RVA: 0x00004108 File Offset: 0x00002308
		~KeyboardInteractiveAuthenticationMethod()
		{
			this.Dispose(false);
		}

		// Token: 0x04000046 RID: 70
		private AuthenticationResult _authenticationResult = AuthenticationResult.Failure;

		// Token: 0x04000047 RID: 71
		private Session _session;

		// Token: 0x04000048 RID: 72
		private EventWaitHandle _authenticationCompleted = new AutoResetEvent(false);

		// Token: 0x04000049 RID: 73
		private Exception _exception;

		// Token: 0x0400004A RID: 74
		private readonly RequestMessage _requestMessage;

		// Token: 0x0400004C RID: 76
		private bool _isDisposed;
	}
}
