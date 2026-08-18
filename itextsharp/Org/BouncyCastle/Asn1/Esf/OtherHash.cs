using System;
using Org.BouncyCastle.Asn1.Oiw;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Asn1.Esf
{
	// Token: 0x0200057F RID: 1407
	public class OtherHash : Asn1Encodable, IAsn1Choice
	{
		// Token: 0x06002FEA RID: 12266 RVA: 0x00127BBD File Offset: 0x00126BBD
		public static OtherHash GetInstance(object obj)
		{
			if (obj == null || obj is OtherHash)
			{
				return (OtherHash)obj;
			}
			if (obj is Asn1OctetString)
			{
				return new OtherHash((Asn1OctetString)obj);
			}
			return new OtherHash(OtherHashAlgAndValue.GetInstance(obj));
		}

		// Token: 0x06002FEB RID: 12267 RVA: 0x00127BF0 File Offset: 0x00126BF0
		public OtherHash(byte[] sha1Hash)
		{
			if (sha1Hash == null)
			{
				throw new ArgumentNullException("sha1Hash");
			}
			this.sha1Hash = new DerOctetString(sha1Hash);
		}

		// Token: 0x06002FEC RID: 12268 RVA: 0x00127C12 File Offset: 0x00126C12
		public OtherHash(Asn1OctetString sha1Hash)
		{
			if (sha1Hash == null)
			{
				throw new ArgumentNullException("sha1Hash");
			}
			this.sha1Hash = sha1Hash;
		}

		// Token: 0x06002FED RID: 12269 RVA: 0x00127C2F File Offset: 0x00126C2F
		public OtherHash(OtherHashAlgAndValue otherHash)
		{
			if (otherHash == null)
			{
				throw new ArgumentNullException("otherHash");
			}
			this.otherHash = otherHash;
		}

		// Token: 0x17000834 RID: 2100
		// (get) Token: 0x06002FEE RID: 12270 RVA: 0x00127C4C File Offset: 0x00126C4C
		public AlgorithmIdentifier HashAlgorithm
		{
			get
			{
				if (this.otherHash != null)
				{
					return this.otherHash.HashAlgorithm;
				}
				return new AlgorithmIdentifier(OiwObjectIdentifiers.IdSha1);
			}
		}

		// Token: 0x06002FEF RID: 12271 RVA: 0x00127C6C File Offset: 0x00126C6C
		public byte[] GetHashValue()
		{
			if (this.otherHash != null)
			{
				return this.otherHash.GetHashValue();
			}
			return this.sha1Hash.GetOctets();
		}

		// Token: 0x06002FF0 RID: 12272 RVA: 0x00127C8D File Offset: 0x00126C8D
		public override Asn1Object ToAsn1Object()
		{
			if (this.otherHash != null)
			{
				return this.otherHash.ToAsn1Object();
			}
			return this.sha1Hash;
		}

		// Token: 0x040020E5 RID: 8421
		private readonly Asn1OctetString sha1Hash;

		// Token: 0x040020E6 RID: 8422
		private readonly OtherHashAlgAndValue otherHash;
	}
}
