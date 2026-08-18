using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Renci.SshNet
{
	// Token: 0x02000021 RID: 33
	public class PrivateKeyConnectionInfo : ConnectionInfo, IDisposable
	{
		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000198 RID: 408 RVA: 0x00005F80 File Offset: 0x00004180
		// (set) Token: 0x06000199 RID: 409 RVA: 0x00005F88 File Offset: 0x00004188
		public ICollection<PrivateKeyFile> KeyFiles { get; private set; }

		// Token: 0x0600019A RID: 410 RVA: 0x00005F94 File Offset: 0x00004194
		public PrivateKeyConnectionInfo(string host, string username, params PrivateKeyFile[] keyFiles) : this(host, ConnectionInfo.DefaultPort, username, ProxyTypes.None, string.Empty, 0, string.Empty, string.Empty, keyFiles)
		{
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00005FC0 File Offset: 0x000041C0
		public PrivateKeyConnectionInfo(string host, int port, string username, params PrivateKeyFile[] keyFiles) : this(host, port, username, ProxyTypes.None, string.Empty, 0, string.Empty, string.Empty, keyFiles)
		{
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00005FEC File Offset: 0x000041EC
		public PrivateKeyConnectionInfo(string host, int port, string username, ProxyTypes proxyType, string proxyHost, int proxyPort, params PrivateKeyFile[] keyFiles) : this(host, port, username, proxyType, proxyHost, proxyPort, string.Empty, string.Empty, keyFiles)
		{
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00006014 File Offset: 0x00004214
		public PrivateKeyConnectionInfo(string host, int port, string username, ProxyTypes proxyType, string proxyHost, int proxyPort, string proxyUsername, params PrivateKeyFile[] keyFiles) : this(host, port, username, proxyType, proxyHost, proxyPort, proxyUsername, string.Empty, keyFiles)
		{
		}

		// Token: 0x0600019E RID: 414 RVA: 0x0000603C File Offset: 0x0000423C
		public PrivateKeyConnectionInfo(string host, string username, ProxyTypes proxyType, string proxyHost, int proxyPort, params PrivateKeyFile[] keyFiles) : this(host, ConnectionInfo.DefaultPort, username, proxyType, proxyHost, proxyPort, string.Empty, string.Empty, keyFiles)
		{
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00006068 File Offset: 0x00004268
		public PrivateKeyConnectionInfo(string host, string username, ProxyTypes proxyType, string proxyHost, int proxyPort, string proxyUsername, params PrivateKeyFile[] keyFiles) : this(host, ConnectionInfo.DefaultPort, username, proxyType, proxyHost, proxyPort, proxyUsername, string.Empty, keyFiles)
		{
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00006090 File Offset: 0x00004290
		public PrivateKeyConnectionInfo(string host, string username, ProxyTypes proxyType, string proxyHost, int proxyPort, string proxyUsername, string proxyPassword, params PrivateKeyFile[] keyFiles) : this(host, ConnectionInfo.DefaultPort, username, proxyType, proxyHost, proxyPort, proxyUsername, proxyPassword, keyFiles)
		{
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x000060B8 File Offset: 0x000042B8
		public PrivateKeyConnectionInfo(string host, int port, string username, ProxyTypes proxyType, string proxyHost, int proxyPort, string proxyUsername, string proxyPassword, params PrivateKeyFile[] keyFiles) : base(host, port, username, proxyType, proxyHost, proxyPort, proxyUsername, proxyPassword, new AuthenticationMethod[]
		{
			new PrivateKeyAuthenticationMethod(username, keyFiles)
		})
		{
			this.KeyFiles = new Collection<PrivateKeyFile>(keyFiles);
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x000060F6 File Offset: 0x000042F6
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00006108 File Offset: 0x00004308
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

		// Token: 0x060001A4 RID: 420 RVA: 0x00006174 File Offset: 0x00004374
		~PrivateKeyConnectionInfo()
		{
			this.Dispose(false);
		}

		// Token: 0x04000072 RID: 114
		private bool _isDisposed;
	}
}
