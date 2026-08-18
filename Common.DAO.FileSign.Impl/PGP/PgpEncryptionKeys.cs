using System;
using System.IO;
using System.Linq;
using System.Text;
using Org.BouncyCastle.Bcpg.OpenPgp;

namespace TechnoPro.Common.DAO.FileSign.Impl.PGP
{
	// Token: 0x02000007 RID: 7
	public class PgpEncryptionKeys
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002800 File Offset: 0x00000A00
		// (set) Token: 0x06000028 RID: 40 RVA: 0x00002808 File Offset: 0x00000A08
		public PgpPublicKey PublicKey { get; private set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002811 File Offset: 0x00000A11
		// (set) Token: 0x0600002A RID: 42 RVA: 0x00002819 File Offset: 0x00000A19
		public PgpPrivateKey PrivateKey { get; private set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002822 File Offset: 0x00000A22
		// (set) Token: 0x0600002C RID: 44 RVA: 0x0000282A File Offset: 0x00000A2A
		public PgpSecretKey SecretKey { get; private set; }

		// Token: 0x0600002D RID: 45 RVA: 0x00002834 File Offset: 0x00000A34
		public PgpEncryptionKeys(string publicKey, string privateKey, string passPhrase)
		{
			if (!string.IsNullOrEmpty(privateKey))
			{
				using (Stream stream = new MemoryStream(Encoding.UTF8.GetBytes(privateKey)))
				{
					this.SecretKey = this.readSecretKey(stream);
				}
			}
			if (!string.IsNullOrEmpty(publicKey))
			{
				using (Stream stream2 = new MemoryStream(Encoding.UTF8.GetBytes(publicKey)))
				{
					this.PublicKey = this.readPublicKey(stream2);
				}
			}
			this.PrivateKey = this.readPrivateKey(passPhrase);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000028D4 File Offset: 0x00000AD4
		public PgpEncryptionKeys()
		{
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000028DC File Offset: 0x00000ADC
		public void Init(string publicKeyPath, string privateKeyPath, string passPhrase)
		{
			if (!File.Exists(publicKeyPath))
			{
				throw new ArgumentException("Public key file not found.", "publicKeyPath");
			}
			if (!File.Exists(privateKeyPath))
			{
				throw new ArgumentException("Private key file not found.", "privateKeyPath");
			}
			if (string.IsNullOrEmpty(passPhrase))
			{
				throw new ArgumentException("passPhrase is null or empty.", "passPhrase");
			}
			this.PublicKey = this.readPublicKey(publicKeyPath);
			this.SecretKey = this.readSecretKey(privateKeyPath);
			this.PrivateKey = this.readPrivateKey(passPhrase);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002958 File Offset: 0x00000B58
		private PgpSecretKey readSecretKey(string privateKeyPath)
		{
			PgpSecretKey result;
			using (Stream stream = File.OpenRead(privateKeyPath))
			{
				result = this.readSecretKey(stream);
			}
			return result;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002994 File Offset: 0x00000B94
		private PgpSecretKey readSecretKey(Stream keyIn)
		{
			using (Stream decoderStream = PgpUtilities.GetDecoderStream(keyIn))
			{
				PgpSecretKeyRingBundle secretKeyRingBundle = new PgpSecretKeyRingBundle(decoderStream);
				PgpSecretKey firstSecretKey = this.getFirstSecretKey(secretKeyRingBundle);
				if (firstSecretKey != null)
				{
					return firstSecretKey;
				}
			}
			throw new ArgumentException("Can't find signing key in key ring.");
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000029E8 File Offset: 0x00000BE8
		private PgpSecretKey getFirstSecretKey(PgpSecretKeyRingBundle secretKeyRingBundle)
		{
			foreach (object obj in secretKeyRingBundle.GetKeyRings())
			{
				PgpSecretKey pgpSecretKey = (from PgpSecretKey k in ((PgpSecretKeyRing)obj).GetSecretKeys()
				where k.IsSigningKey
				select k).FirstOrDefault<PgpSecretKey>();
				if (pgpSecretKey != null)
				{
					return pgpSecretKey;
				}
			}
			return null;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002A78 File Offset: 0x00000C78
		private PgpPublicKey readPublicKey(string publicKeyPath)
		{
			PgpPublicKey result;
			using (Stream stream = File.OpenRead(publicKeyPath))
			{
				result = this.readPublicKey(stream);
			}
			return result;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002AB4 File Offset: 0x00000CB4
		private PgpPublicKey readPublicKey(Stream keyIn)
		{
			using (Stream decoderStream = PgpUtilities.GetDecoderStream(keyIn))
			{
				try
				{
					PgpPublicKeyRingBundle publicKeyRingBundle = new PgpPublicKeyRingBundle(decoderStream);
					PgpPublicKey firstPublicKey = this.getFirstPublicKey(publicKeyRingBundle);
					if (firstPublicKey != null)
					{
						return firstPublicKey;
					}
				}
				catch (Exception)
				{
					throw new ArgumentException("There was a problem with the public key ring.");
				}
			}
			throw new ArgumentException("No encryption key found in public key ring.");
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002B20 File Offset: 0x00000D20
		private PgpPublicKey getFirstPublicKey(PgpPublicKeyRingBundle publicKeyRingBundle)
		{
			foreach (object obj in publicKeyRingBundle.GetKeyRings())
			{
				PgpPublicKey pgpPublicKey = (from PgpPublicKey k in ((PgpPublicKeyRing)obj).GetPublicKeys()
				where k.IsEncryptionKey
				select k).FirstOrDefault<PgpPublicKey>();
				if (pgpPublicKey != null)
				{
					return pgpPublicKey;
				}
			}
			return null;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002BB0 File Offset: 0x00000DB0
		private PgpPrivateKey readPrivateKey(string passPhrase)
		{
			PgpPrivateKey pgpPrivateKey = this.SecretKey.ExtractPrivateKey(passPhrase.ToCharArray());
			if (pgpPrivateKey != null)
			{
				return pgpPrivateKey;
			}
			throw new ArgumentException("No private key found in secret key.");
		}
	}
}
