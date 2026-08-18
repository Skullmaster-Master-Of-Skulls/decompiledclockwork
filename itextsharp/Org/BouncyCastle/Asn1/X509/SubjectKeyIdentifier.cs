using System;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Digests;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000426 RID: 1062
	public class SubjectKeyIdentifier : Asn1Encodable
	{
		// Token: 0x06002429 RID: 9257 RVA: 0x000DC688 File Offset: 0x000DB688
		public static SubjectKeyIdentifier GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return SubjectKeyIdentifier.GetInstance(Asn1OctetString.GetInstance(obj, explicitly));
		}

		// Token: 0x0600242A RID: 9258 RVA: 0x000DC698 File Offset: 0x000DB698
		public static SubjectKeyIdentifier GetInstance(object obj)
		{
			if (obj is SubjectKeyIdentifier)
			{
				return (SubjectKeyIdentifier)obj;
			}
			if (obj is SubjectPublicKeyInfo)
			{
				return new SubjectKeyIdentifier((SubjectPublicKeyInfo)obj);
			}
			if (obj is Asn1OctetString)
			{
				return new SubjectKeyIdentifier((Asn1OctetString)obj);
			}
			if (obj is X509Extension)
			{
				return SubjectKeyIdentifier.GetInstance(X509Extension.ConvertValueToObject((X509Extension)obj));
			}
			throw new ArgumentException("Invalid SubjectKeyIdentifier: " + obj.GetType().Name);
		}

		// Token: 0x0600242B RID: 9259 RVA: 0x000DC70F File Offset: 0x000DB70F
		public SubjectKeyIdentifier(byte[] keyID)
		{
			if (keyID == null)
			{
				throw new ArgumentNullException("keyID");
			}
			this.keyIdentifier = keyID;
		}

		// Token: 0x0600242C RID: 9260 RVA: 0x000DC72C File Offset: 0x000DB72C
		public SubjectKeyIdentifier(Asn1OctetString keyID)
		{
			this.keyIdentifier = keyID.GetOctets();
		}

		// Token: 0x0600242D RID: 9261 RVA: 0x000DC740 File Offset: 0x000DB740
		public SubjectKeyIdentifier(SubjectPublicKeyInfo spki)
		{
			this.keyIdentifier = SubjectKeyIdentifier.GetDigest(spki);
		}

		// Token: 0x0600242E RID: 9262 RVA: 0x000DC754 File Offset: 0x000DB754
		public byte[] GetKeyIdentifier()
		{
			return this.keyIdentifier;
		}

		// Token: 0x0600242F RID: 9263 RVA: 0x000DC75C File Offset: 0x000DB75C
		public override Asn1Object ToAsn1Object()
		{
			return new DerOctetString(this.keyIdentifier);
		}

		// Token: 0x06002430 RID: 9264 RVA: 0x000DC769 File Offset: 0x000DB769
		public static SubjectKeyIdentifier CreateSha1KeyIdentifier(SubjectPublicKeyInfo keyInfo)
		{
			return new SubjectKeyIdentifier(keyInfo);
		}

		// Token: 0x06002431 RID: 9265 RVA: 0x000DC774 File Offset: 0x000DB774
		public static SubjectKeyIdentifier CreateTruncatedSha1KeyIdentifier(SubjectPublicKeyInfo keyInfo)
		{
			byte[] digest = SubjectKeyIdentifier.GetDigest(keyInfo);
			byte[] array = new byte[8];
			Array.Copy(digest, digest.Length - 8, array, 0, array.Length);
			byte[] array2 = array;
			int num = 0;
			array2[num] &= 15;
			byte[] array3 = array;
			int num2 = 0;
			array3[num2] |= 64;
			return new SubjectKeyIdentifier(array);
		}

		// Token: 0x06002432 RID: 9266 RVA: 0x000DC7D4 File Offset: 0x000DB7D4
		private static byte[] GetDigest(SubjectPublicKeyInfo spki)
		{
			IDigest digest = new Sha1Digest();
			byte[] array = new byte[digest.GetDigestSize()];
			byte[] bytes = spki.PublicKeyData.GetBytes();
			digest.BlockUpdate(bytes, 0, bytes.Length);
			digest.DoFinal(array, 0);
			return array;
		}

		// Token: 0x04001919 RID: 6425
		private readonly byte[] keyIdentifier;
	}
}
