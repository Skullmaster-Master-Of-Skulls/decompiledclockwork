using System;
using System.Security.Cryptography;
using Renci.SshNet.Abstractions;

namespace Renci.SshNet.Security
{
	// Token: 0x02000074 RID: 116
	public class KeyExchangeDiffieHellmanGroupExchangeSha256 : KeyExchangeDiffieHellmanGroupExchangeShaBase
	{
		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x060006A5 RID: 1701 RVA: 0x00014E08 File Offset: 0x00013008
		public override string Name
		{
			get
			{
				return "diffie-hellman-group-exchange-sha256";
			}
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x00014E10 File Offset: 0x00013010
		protected override byte[] Hash(byte[] hashBytes)
		{
			byte[] result;
			using (SHA256 sha = CryptoAbstraction.CreateSHA256())
			{
				result = sha.ComputeHash(hashBytes);
			}
			return result;
		}
	}
}
