using System;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Pkcs
{
	// Token: 0x02000500 RID: 1280
	public sealed class EncryptedPrivateKeyInfoFactory
	{
		// Token: 0x06002BBE RID: 11198 RVA: 0x00108C13 File Offset: 0x00107C13
		private EncryptedPrivateKeyInfoFactory()
		{
		}

		// Token: 0x06002BBF RID: 11199 RVA: 0x00108C1B File Offset: 0x00107C1B
		public static EncryptedPrivateKeyInfo CreateEncryptedPrivateKeyInfo(DerObjectIdentifier algorithm, char[] passPhrase, byte[] salt, int iterationCount, AsymmetricKeyParameter key)
		{
			return EncryptedPrivateKeyInfoFactory.CreateEncryptedPrivateKeyInfo(algorithm.Id, passPhrase, salt, iterationCount, PrivateKeyInfoFactory.CreatePrivateKeyInfo(key));
		}

		// Token: 0x06002BC0 RID: 11200 RVA: 0x00108C32 File Offset: 0x00107C32
		public static EncryptedPrivateKeyInfo CreateEncryptedPrivateKeyInfo(string algorithm, char[] passPhrase, byte[] salt, int iterationCount, AsymmetricKeyParameter key)
		{
			return EncryptedPrivateKeyInfoFactory.CreateEncryptedPrivateKeyInfo(algorithm, passPhrase, salt, iterationCount, PrivateKeyInfoFactory.CreatePrivateKeyInfo(key));
		}

		// Token: 0x06002BC1 RID: 11201 RVA: 0x00108C44 File Offset: 0x00107C44
		public static EncryptedPrivateKeyInfo CreateEncryptedPrivateKeyInfo(string algorithm, char[] passPhrase, byte[] salt, int iterationCount, PrivateKeyInfo keyInfo)
		{
			if (!PbeUtilities.IsPbeAlgorithm(algorithm))
			{
				throw new ArgumentException("attempt to use non-PBE algorithm with PBE EncryptedPrivateKeyInfo generation");
			}
			IBufferedCipher bufferedCipher = PbeUtilities.CreateEngine(algorithm) as IBufferedCipher;
			Asn1Encodable asn1Encodable = PbeUtilities.GenerateAlgorithmParameters(algorithm, salt, iterationCount);
			ICipherParameters parameters = PbeUtilities.GenerateCipherParameters(algorithm, passPhrase, asn1Encodable);
			bufferedCipher.Init(true, parameters);
			byte[] encoded = keyInfo.GetEncoded();
			byte[] encoding = bufferedCipher.DoFinal(encoded);
			DerObjectIdentifier objectIdentifier = PbeUtilities.GetObjectIdentifier(algorithm);
			AlgorithmIdentifier algId = new AlgorithmIdentifier(objectIdentifier, asn1Encodable);
			return new EncryptedPrivateKeyInfo(algId, encoding);
		}
	}
}
