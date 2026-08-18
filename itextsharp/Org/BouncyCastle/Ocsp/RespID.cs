using System;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;

namespace Org.BouncyCastle.Ocsp
{
	// Token: 0x02000502 RID: 1282
	public class RespID
	{
		// Token: 0x06002BC9 RID: 11209 RVA: 0x00108D2D File Offset: 0x00107D2D
		public RespID(ResponderID id)
		{
			this.id = id;
		}

		// Token: 0x06002BCA RID: 11210 RVA: 0x00108D3C File Offset: 0x00107D3C
		public RespID(X509Name name)
		{
			try
			{
				this.id = new ResponderID(name);
			}
			catch (Exception innerException)
			{
				throw new ArgumentException("can't decode name.", innerException);
			}
		}

		// Token: 0x06002BCB RID: 11211 RVA: 0x00108D7C File Offset: 0x00107D7C
		public RespID(AsymmetricKeyParameter publicKey)
		{
			try
			{
				SubjectPublicKeyInfo subjectPublicKeyInfo = SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(publicKey);
				byte[] str = DigestUtilities.CalculateDigest("SHA1", subjectPublicKeyInfo.PublicKeyData.GetBytes());
				this.id = new ResponderID(new DerOctetString(str));
			}
			catch (Exception ex)
			{
				throw new OcspException("problem creating ID: " + ex, ex);
			}
		}

		// Token: 0x06002BCC RID: 11212 RVA: 0x00108DE4 File Offset: 0x00107DE4
		public ResponderID ToAsn1Object()
		{
			return this.id;
		}

		// Token: 0x06002BCD RID: 11213 RVA: 0x00108DEC File Offset: 0x00107DEC
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			RespID respID = obj as RespID;
			return respID != null && this.id.Equals(respID.id);
		}

		// Token: 0x06002BCE RID: 11214 RVA: 0x00108E1C File Offset: 0x00107E1C
		public override int GetHashCode()
		{
			return this.id.GetHashCode();
		}

		// Token: 0x04001E40 RID: 7744
		internal readonly ResponderID id;
	}
}
