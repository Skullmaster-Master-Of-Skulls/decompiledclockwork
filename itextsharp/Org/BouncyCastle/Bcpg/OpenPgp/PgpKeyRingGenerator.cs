using System;
using System.Collections;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Bcpg.OpenPgp
{
	// Token: 0x0200000C RID: 12
	public class PgpKeyRingGenerator
	{
		// Token: 0x0600004B RID: 75 RVA: 0x00004254 File Offset: 0x00003254
		public PgpKeyRingGenerator(int certificationLevel, PgpKeyPair masterKey, string id, SymmetricKeyAlgorithmTag encAlgorithm, char[] passPhrase, PgpSignatureSubpacketVector hashedPackets, PgpSignatureSubpacketVector unhashedPackets, SecureRandom rand) : this(certificationLevel, masterKey, id, encAlgorithm, passPhrase, false, hashedPackets, unhashedPackets, rand)
		{
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00004278 File Offset: 0x00003278
		public PgpKeyRingGenerator(int certificationLevel, PgpKeyPair masterKey, string id, SymmetricKeyAlgorithmTag encAlgorithm, char[] passPhrase, bool useSha1, PgpSignatureSubpacketVector hashedPackets, PgpSignatureSubpacketVector unhashedPackets, SecureRandom rand)
		{
			this.certificationLevel = certificationLevel;
			this.masterKey = masterKey;
			this.id = id;
			this.encAlgorithm = encAlgorithm;
			this.passPhrase = passPhrase;
			this.useSha1 = useSha1;
			this.hashedPacketVector = hashedPackets;
			this.unhashedPacketVector = unhashedPackets;
			this.rand = rand;
			this.keys.Add(new PgpSecretKey(certificationLevel, masterKey, id, encAlgorithm, passPhrase, useSha1, hashedPackets, unhashedPackets, rand));
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000042FB File Offset: 0x000032FB
		public void AddSubKey(PgpKeyPair keyPair)
		{
			this.AddSubKey(keyPair, this.hashedPacketVector, this.unhashedPacketVector);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00004310 File Offset: 0x00003310
		public void AddSubKey(PgpKeyPair keyPair, PgpSignatureSubpacketVector hashedPackets, PgpSignatureSubpacketVector unhashedPackets)
		{
			try
			{
				PgpSignatureGenerator pgpSignatureGenerator = new PgpSignatureGenerator(this.masterKey.PublicKey.Algorithm, HashAlgorithmTag.Sha1);
				pgpSignatureGenerator.InitSign(24, this.masterKey.PrivateKey);
				pgpSignatureGenerator.SetHashedSubpackets(hashedPackets);
				pgpSignatureGenerator.SetUnhashedSubpackets(unhashedPackets);
				ArrayList arrayList = new ArrayList();
				arrayList.Add(pgpSignatureGenerator.GenerateCertification(this.masterKey.PublicKey, keyPair.PublicKey));
				this.keys.Add(new PgpSecretKey(keyPair.PrivateKey, new PgpPublicKey(keyPair.PublicKey, null, arrayList), this.encAlgorithm, this.passPhrase, this.useSha1, this.rand));
			}
			catch (PgpException ex)
			{
				throw ex;
			}
			catch (Exception exception)
			{
				throw new PgpException("exception adding subkey: ", exception);
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000043E4 File Offset: 0x000033E4
		public PgpSecretKeyRing GenerateSecretKeyRing()
		{
			return new PgpSecretKeyRing(this.keys);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000043F4 File Offset: 0x000033F4
		public PgpPublicKeyRing GeneratePublicKeyRing()
		{
			ArrayList arrayList = new ArrayList();
			IEnumerator enumerator = this.keys.GetEnumerator();
			enumerator.MoveNext();
			PgpSecretKey pgpSecretKey = (PgpSecretKey)enumerator.Current;
			arrayList.Add(pgpSecretKey.PublicKey);
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				pgpSecretKey = (PgpSecretKey)obj;
				PgpPublicKey pgpPublicKey = new PgpPublicKey(pgpSecretKey.PublicKey);
				pgpPublicKey.publicPk = new PublicSubkeyPacket(pgpPublicKey.Algorithm, pgpPublicKey.CreationTime, pgpPublicKey.publicPk.Key);
				arrayList.Add(pgpPublicKey);
			}
			return new PgpPublicKeyRing(arrayList);
		}

		// Token: 0x0400000F RID: 15
		private ArrayList keys = new ArrayList();

		// Token: 0x04000010 RID: 16
		private string id;

		// Token: 0x04000011 RID: 17
		private SymmetricKeyAlgorithmTag encAlgorithm;

		// Token: 0x04000012 RID: 18
		private int certificationLevel;

		// Token: 0x04000013 RID: 19
		private char[] passPhrase;

		// Token: 0x04000014 RID: 20
		private bool useSha1;

		// Token: 0x04000015 RID: 21
		private PgpKeyPair masterKey;

		// Token: 0x04000016 RID: 22
		private PgpSignatureSubpacketVector hashedPacketVector;

		// Token: 0x04000017 RID: 23
		private PgpSignatureSubpacketVector unhashedPacketVector;

		// Token: 0x04000018 RID: 24
		private SecureRandom rand;
	}
}
