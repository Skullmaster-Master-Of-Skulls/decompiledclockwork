using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using Renci.SshNet.Common;
using Renci.SshNet.Messages;
using Renci.SshNet.Messages.Authentication;

namespace Renci.SshNet
{
	// Token: 0x02000020 RID: 32
	public class PrivateKeyAuthenticationMethod : AuthenticationMethod, IDisposable
	{
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600018D RID: 397 RVA: 0x00005C4C File Offset: 0x00003E4C
		public override string Name
		{
			get
			{
				return "publickey";
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600018E RID: 398 RVA: 0x00005C53 File Offset: 0x00003E53
		// (set) Token: 0x0600018F RID: 399 RVA: 0x00005C5B File Offset: 0x00003E5B
		public ICollection<PrivateKeyFile> KeyFiles { get; private set; }

		// Token: 0x06000190 RID: 400 RVA: 0x00005C64 File Offset: 0x00003E64
		public PrivateKeyAuthenticationMethod(string username, params PrivateKeyFile[] keyFiles) : base(username)
		{
			if (keyFiles == null)
			{
				throw new ArgumentNullException("keyFiles");
			}
			this.KeyFiles = new Collection<PrivateKeyFile>(keyFiles);
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00005C9C File Offset: 0x00003E9C
		public override AuthenticationResult Authenticate(Session session)
		{
			session.UserAuthenticationSuccessReceived += this.Session_UserAuthenticationSuccessReceived;
			session.UserAuthenticationFailureReceived += this.Session_UserAuthenticationFailureReceived;
			session.MessageReceived += this.Session_MessageReceived;
			session.RegisterMessage("SSH_MSG_USERAUTH_PK_OK");
			AuthenticationResult authenticationResult;
			try
			{
				foreach (PrivateKeyFile privateKeyFile in this.KeyFiles)
				{
					this._authenticationCompleted.Reset();
					this._isSignatureRequired = false;
					RequestMessagePublicKey requestMessagePublicKey = new RequestMessagePublicKey(ServiceName.Connection, base.Username, privateKeyFile.HostKey.Name, privateKeyFile.HostKey.Data);
					if (this.KeyFiles.Count < 2)
					{
						byte[] bytes = new PrivateKeyAuthenticationMethod.SignatureData(requestMessagePublicKey, session.SessionId).GetBytes();
						requestMessagePublicKey.Signature = privateKeyFile.HostKey.Sign(bytes);
					}
					session.SendMessage(requestMessagePublicKey);
					session.WaitOnHandle(this._authenticationCompleted);
					if (this._isSignatureRequired)
					{
						this._authenticationCompleted.Reset();
						RequestMessagePublicKey requestMessagePublicKey2 = new RequestMessagePublicKey(ServiceName.Connection, base.Username, privateKeyFile.HostKey.Name, privateKeyFile.HostKey.Data);
						byte[] bytes2 = new PrivateKeyAuthenticationMethod.SignatureData(requestMessagePublicKey, session.SessionId).GetBytes();
						requestMessagePublicKey2.Signature = privateKeyFile.HostKey.Sign(bytes2);
						session.SendMessage(requestMessagePublicKey2);
					}
					session.WaitOnHandle(this._authenticationCompleted);
					if (this._authenticationResult == AuthenticationResult.Success)
					{
						break;
					}
				}
				authenticationResult = this._authenticationResult;
			}
			finally
			{
				session.UserAuthenticationSuccessReceived -= this.Session_UserAuthenticationSuccessReceived;
				session.UserAuthenticationFailureReceived -= this.Session_UserAuthenticationFailureReceived;
				session.MessageReceived -= this.Session_MessageReceived;
				session.UnRegisterMessage("SSH_MSG_USERAUTH_PK_OK");
			}
			return authenticationResult;
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00005E94 File Offset: 0x00004094
		private void Session_UserAuthenticationSuccessReceived(object sender, MessageEventArgs<SuccessMessage> e)
		{
			this._authenticationResult = AuthenticationResult.Success;
			this._authenticationCompleted.Set();
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00005EA9 File Offset: 0x000040A9
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

		// Token: 0x06000194 RID: 404 RVA: 0x00005EE5 File Offset: 0x000040E5
		private void Session_MessageReceived(object sender, MessageEventArgs<Message> e)
		{
			if (e.Message is PublicKeyMessage)
			{
				this._isSignatureRequired = true;
				this._authenticationCompleted.Set();
			}
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00005F07 File Offset: 0x00004107
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00005F18 File Offset: 0x00004118
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
					this._authenticationCompleted = null;
					authenticationCompleted.Dispose();
				}
				this._isDisposed = true;
			}
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00005F50 File Offset: 0x00004150
		~PrivateKeyAuthenticationMethod()
		{
			this.Dispose(false);
		}

		// Token: 0x0400006C RID: 108
		private AuthenticationResult _authenticationResult = AuthenticationResult.Failure;

		// Token: 0x0400006D RID: 109
		private EventWaitHandle _authenticationCompleted = new ManualResetEvent(false);

		// Token: 0x0400006E RID: 110
		private bool _isSignatureRequired;

		// Token: 0x04000070 RID: 112
		private bool _isDisposed;

		// Token: 0x02000127 RID: 295
		private class SignatureData : SshData
		{
			// Token: 0x170002DC RID: 732
			// (get) Token: 0x06000C5F RID: 3167 RVA: 0x00027B68 File Offset: 0x00025D68
			protected override int BufferCapacity
			{
				get
				{
					return base.BufferCapacity + 4 + this._sessionId.Length + 1 + 4 + this._message.Username.Length + 4 + this._serviceName.Length + 4 + this._authenticationMethod.Length + 1 + 4 + this._message.PublicKeyAlgorithmName.Length + 4 + this._message.PublicKeyData.Length;
				}
			}

			// Token: 0x06000C60 RID: 3168 RVA: 0x00027BD0 File Offset: 0x00025DD0
			public SignatureData(RequestMessagePublicKey message, byte[] sessionId)
			{
				this._message = message;
				this._sessionId = sessionId;
				this._serviceName = ServiceName.Connection.ToArray();
				this._authenticationMethod = SshData.Ascii.GetBytes("publickey");
			}

			// Token: 0x06000C61 RID: 3169 RVA: 0x0000B8A3 File Offset: 0x00009AA3
			protected override void LoadData()
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000C62 RID: 3170 RVA: 0x00027C08 File Offset: 0x00025E08
			protected override void SaveData()
			{
				base.WriteBinaryString(this._sessionId);
				base.Write(50);
				base.WriteBinaryString(this._message.Username);
				base.WriteBinaryString(this._serviceName);
				base.WriteBinaryString(this._authenticationMethod);
				base.Write(1);
				base.WriteBinaryString(this._message.PublicKeyAlgorithmName);
				base.WriteBinaryString(this._message.PublicKeyData);
			}

			// Token: 0x040004DB RID: 1243
			private readonly RequestMessagePublicKey _message;

			// Token: 0x040004DC RID: 1244
			private readonly byte[] _sessionId;

			// Token: 0x040004DD RID: 1245
			private readonly byte[] _serviceName;

			// Token: 0x040004DE RID: 1246
			private readonly byte[] _authenticationMethod;
		}
	}
}
