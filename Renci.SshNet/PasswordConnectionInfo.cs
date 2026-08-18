using System;
using System.Linq;
using System.Text;
using Renci.SshNet.Common;

namespace Renci.SshNet
{
	// Token: 0x0200001F RID: 31
	public class PasswordConnectionInfo : ConnectionInfo, IDisposable
	{
		// Token: 0x1400001A RID: 26
		// (add) Token: 0x06000178 RID: 376 RVA: 0x00005844 File Offset: 0x00003A44
		// (remove) Token: 0x06000179 RID: 377 RVA: 0x0000587C File Offset: 0x00003A7C
		public event EventHandler<AuthenticationPasswordChangeEventArgs> PasswordExpired;

		// Token: 0x0600017A RID: 378 RVA: 0x000058B1 File Offset: 0x00003AB1
		public PasswordConnectionInfo(string host, string username, string password) : this(host, ConnectionInfo.DefaultPort, username, Encoding.UTF8.GetBytes(password))
		{
		}

		// Token: 0x0600017B RID: 379 RVA: 0x000058CC File Offset: 0x00003ACC
		public PasswordConnectionInfo(string host, int port, string username, string password) : this(host, port, username, Encoding.UTF8.GetBytes(password), ProxyTypes.None, string.Empty, 0, string.Empty, string.Empty)
		{
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00005900 File Offset: 0x00003B00
		public PasswordConnectionInfo(string host, int port, string username, string password, ProxyTypes proxyType, string proxyHost, int proxyPort) : this(host, port, username, Encoding.UTF8.GetBytes(password), proxyType, proxyHost, proxyPort, string.Empty, string.Empty)
		{
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00005934 File Offset: 0x00003B34
		public PasswordConnectionInfo(string host, int port, string username, string password, ProxyTypes proxyType, string proxyHost, int proxyPort, string proxyUsername) : this(host, port, username, Encoding.UTF8.GetBytes(password), proxyType, proxyHost, proxyPort, proxyUsername, string.Empty)
		{
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00005964 File Offset: 0x00003B64
		public PasswordConnectionInfo(string host, string username, string password, ProxyTypes proxyType, string proxyHost, int proxyPort) : this(host, ConnectionInfo.DefaultPort, username, Encoding.UTF8.GetBytes(password), proxyType, proxyHost, proxyPort, string.Empty, string.Empty)
		{
		}

		// Token: 0x0600017F RID: 383 RVA: 0x0000599C File Offset: 0x00003B9C
		public PasswordConnectionInfo(string host, string username, string password, ProxyTypes proxyType, string proxyHost, int proxyPort, string proxyUsername) : this(host, ConnectionInfo.DefaultPort, username, Encoding.UTF8.GetBytes(password), proxyType, proxyHost, proxyPort, proxyUsername, string.Empty)
		{
		}

		// Token: 0x06000180 RID: 384 RVA: 0x000059D0 File Offset: 0x00003BD0
		public PasswordConnectionInfo(string host, string username, string password, ProxyTypes proxyType, string proxyHost, int proxyPort, string proxyUsername, string proxyPassword) : this(host, ConnectionInfo.DefaultPort, username, Encoding.UTF8.GetBytes(password), proxyType, proxyHost, proxyPort, proxyUsername, proxyPassword)
		{
		}

		// Token: 0x06000181 RID: 385 RVA: 0x000059FF File Offset: 0x00003BFF
		public PasswordConnectionInfo(string host, string username, byte[] password) : this(host, ConnectionInfo.DefaultPort, username, password)
		{
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00005A10 File Offset: 0x00003C10
		public PasswordConnectionInfo(string host, int port, string username, byte[] password) : this(host, port, username, password, ProxyTypes.None, string.Empty, 0, string.Empty, string.Empty)
		{
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00005A3C File Offset: 0x00003C3C
		public PasswordConnectionInfo(string host, int port, string username, byte[] password, ProxyTypes proxyType, string proxyHost, int proxyPort) : this(host, port, username, password, proxyType, proxyHost, proxyPort, string.Empty, string.Empty)
		{
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00005A64 File Offset: 0x00003C64
		public PasswordConnectionInfo(string host, int port, string username, byte[] password, ProxyTypes proxyType, string proxyHost, int proxyPort, string proxyUsername) : this(host, port, username, password, proxyType, proxyHost, proxyPort, proxyUsername, string.Empty)
		{
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00005A8C File Offset: 0x00003C8C
		public PasswordConnectionInfo(string host, string username, byte[] password, ProxyTypes proxyType, string proxyHost, int proxyPort) : this(host, ConnectionInfo.DefaultPort, username, password, proxyType, proxyHost, proxyPort, string.Empty, string.Empty)
		{
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00005AB8 File Offset: 0x00003CB8
		public PasswordConnectionInfo(string host, string username, byte[] password, ProxyTypes proxyType, string proxyHost, int proxyPort, string proxyUsername) : this(host, ConnectionInfo.DefaultPort, username, password, proxyType, proxyHost, proxyPort, proxyUsername, string.Empty)
		{
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00005AE0 File Offset: 0x00003CE0
		public PasswordConnectionInfo(string host, string username, byte[] password, ProxyTypes proxyType, string proxyHost, int proxyPort, string proxyUsername, string proxyPassword) : this(host, ConnectionInfo.DefaultPort, username, password, proxyType, proxyHost, proxyPort, proxyUsername, proxyPassword)
		{
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00005B08 File Offset: 0x00003D08
		public PasswordConnectionInfo(string host, int port, string username, byte[] password, ProxyTypes proxyType, string proxyHost, int proxyPort, string proxyUsername, string proxyPassword) : base(host, port, username, proxyType, proxyHost, proxyPort, proxyUsername, proxyPassword, new AuthenticationMethod[]
		{
			new PasswordAuthenticationMethod(username, password)
		})
		{
			foreach (PasswordAuthenticationMethod passwordAuthenticationMethod in base.AuthenticationMethods.OfType<PasswordAuthenticationMethod>())
			{
				passwordAuthenticationMethod.PasswordExpired += this.AuthenticationMethod_PasswordExpired;
			}
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00005B88 File Offset: 0x00003D88
		private void AuthenticationMethod_PasswordExpired(object sender, AuthenticationPasswordChangeEventArgs e)
		{
			if (this.PasswordExpired != null)
			{
				this.PasswordExpired(sender, e);
			}
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00005B9F File Offset: 0x00003D9F
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00005BB0 File Offset: 0x00003DB0
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

		// Token: 0x0600018C RID: 396 RVA: 0x00005C1C File Offset: 0x00003E1C
		~PasswordConnectionInfo()
		{
			this.Dispose(false);
		}

		// Token: 0x0400006B RID: 107
		private bool _isDisposed;
	}
}
