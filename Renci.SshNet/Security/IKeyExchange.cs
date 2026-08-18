using System;
using System.Security.Cryptography;
using Renci.SshNet.Common;
using Renci.SshNet.Compression;
using Renci.SshNet.Messages.Transport;
using Renci.SshNet.Security.Cryptography;

namespace Renci.SshNet.Security
{
	// Token: 0x02000066 RID: 102
	public interface IKeyExchange : IDisposable
	{
		// Token: 0x14000048 RID: 72
		// (add) Token: 0x06000631 RID: 1585
		// (remove) Token: 0x06000632 RID: 1586
		event EventHandler<HostKeyEventArgs> HostKeyReceived;

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000633 RID: 1587
		string Name { get; }

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000634 RID: 1588
		byte[] ExchangeHash { get; }

		// Token: 0x06000635 RID: 1589
		void Start(Session session, KeyExchangeInitMessage message);

		// Token: 0x06000636 RID: 1590
		void Finish();

		// Token: 0x06000637 RID: 1591
		Cipher CreateClientCipher();

		// Token: 0x06000638 RID: 1592
		Cipher CreateServerCipher();

		// Token: 0x06000639 RID: 1593
		HashAlgorithm CreateServerHash();

		// Token: 0x0600063A RID: 1594
		HashAlgorithm CreateClientHash();

		// Token: 0x0600063B RID: 1595
		Compressor CreateCompressor();

		// Token: 0x0600063C RID: 1596
		Compressor CreateDecompressor();
	}
}
