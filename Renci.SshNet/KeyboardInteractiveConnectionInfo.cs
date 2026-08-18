using System;
using System.Linq;
using Renci.SshNet.Common;

namespace Renci.SshNet
{
	// Token: 0x02000019 RID: 25
	public class KeyboardInteractiveConnectionInfo : ConnectionInfo, IDisposable
	{
		// Token: 0x14000018 RID: 24
		// (add) Token: 0x06000114 RID: 276 RVA: 0x00004138 File Offset: 0x00002338
		// (remove) Token: 0x06000115 RID: 277 RVA: 0x00004170 File Offset: 0x00002370
		public event EventHandler<AuthenticationPromptEventArgs> AuthenticationPrompt;

		// Token: 0x06000116 RID: 278 RVA: 0x000041A8 File Offset: 0x000023A8
		public KeyboardInteractiveConnectionInfo(string host, string username) : this(host, ConnectionInfo.DefaultPort, username, ProxyTypes.None, string.Empty, 0, string.Empty, string.Empty)
		{
		}

		// Token: 0x06000117 RID: 279 RVA: 0x000041D4 File Offset: 0x000023D4
		public KeyboardInteractiveConnectionInfo(string host, int port, string username) : this(host, port, username, ProxyTypes.None, string.Empty, 0, string.Empty, string.Empty)
		{
		}

		// Token: 0x06000118 RID: 280 RVA: 0x000041FC File Offset: 0x000023FC
		public KeyboardInteractiveConnectionInfo(string host, int port, string username, ProxyTypes proxyType, string proxyHost, int proxyPort) : this(host, port, username, proxyType, proxyHost, proxyPort, string.Empty, string.Empty)
		{
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00004224 File Offset: 0x00002424
		public KeyboardInteractiveConnectionInfo(string host, int port, string username, ProxyTypes proxyType, string proxyHost, int proxyPort, string proxyUsername) : this(host, port, username, proxyType, proxyHost, proxyPort, proxyUsername, string.Empty)
		{
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00004248 File Offset: 0x00002448
		public KeyboardInteractiveConnectionInfo(string host, string username, ProxyTypes proxyType, string proxyHost, int proxyPort) : this(host, ConnectionInfo.DefaultPort, username, proxyType, proxyHost, proxyPort, string.Empty, string.Empty)
		{
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00004274 File Offset: 0x00002474
		public KeyboardInteractiveConnectionInfo(string host, string username, ProxyTypes proxyType, string proxyHost, int proxyPort, string proxyUsername) : this(host, ConnectionInfo.DefaultPort, username, proxyType, proxyHost, proxyPort, proxyUsername, string.Empty)
		{
		}

		// Token: 0x0600011C RID: 284 RVA: 0x0000429C File Offset: 0x0000249C
		public KeyboardInteractiveConnectionInfo(string host, string username, ProxyTypes proxyType, string proxyHost, int proxyPort, string proxyUsername, string proxyPassword) : this(host, ConnectionInfo.DefaultPort, username, proxyType, proxyHost, proxyPort, proxyUsername, proxyPassword)
		{
		}

		// Token: 0x0600011D RID: 285 RVA: 0x000042C0 File Offset: 0x000024C0
		public KeyboardInteractiveConnectionInfo(string host, int port, string username, ProxyTypes proxyType, string proxyHost, int proxyPort, string proxyUsername, string proxyPassword) : base(host, port, username, proxyType, proxyHost, proxyPort, proxyUsername, proxyPassword, new AuthenticationMethod[]
		{
			new KeyboardInteractiveAuthenticationMethod(username)
		})
		{
			foreach (KeyboardInteractiveAuthenticationMethod keyboardInteractiveAuthenticationMethod in base.AuthenticationMethods.OfType<KeyboardInteractiveAuthenticationMethod>())
			{
				keyboardInteractiveAuthenticationMethod.AuthenticationPrompt += this.AuthenticationMethod_AuthenticationPrompt;
			}
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00004340 File Offset: 0x00002540
		private void AuthenticationMethod_AuthenticationPrompt(object sender, AuthenticationPromptEventArgs e)
		{
			if (this.AuthenticationPrompt != null)
			{
				this.AuthenticationPrompt(sender, e);
			}
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00004357 File Offset: 0x00002557
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00004368 File Offset: 0x00002568
		protected virtual void Dispose(bool disposing)
		{
			if (this._isDisposed)
			{
				return;
			}
			if (disposing)
			{
				if (base.AuthenticationMethods != null)
				{
					foreach (IDisposable disposable in base.AuthenticationMethods.OfType<IDisposable>())
					{
						disposable.Dispose();
					}
				}
				this._isDisposed = true;
			}
		}

		// Token: 0x06000121 RID: 289 RVA: 0x000043D4 File Offset: 0x000025D4
		~KeyboardInteractiveConnectionInfo()
		{
			this.Dispose(false);
		}

		// Token: 0x0400004E RID: 78
		private bool _isDisposed;
	}
}
