using System;

namespace Org.BouncyCastle.Asn1.Esf
{
	// Token: 0x02000409 RID: 1033
	public class SignaturePolicyIdentifier : Asn1Encodable, IAsn1Choice
	{
		// Token: 0x0600232E RID: 9006 RVA: 0x000D8CA4 File Offset: 0x000D7CA4
		public static SignaturePolicyIdentifier GetInstance(object obj)
		{
			if (obj == null || obj is SignaturePolicyIdentifier)
			{
				return (SignaturePolicyIdentifier)obj;
			}
			if (obj is SignaturePolicyId)
			{
				return new SignaturePolicyIdentifier((SignaturePolicyId)obj);
			}
			if (obj is Asn1Null)
			{
				return new SignaturePolicyIdentifier();
			}
			throw new ArgumentException("Unknown object in 'SignaturePolicyIdentifier' factory: " + obj.GetType().Name, "obj");
		}

		// Token: 0x0600232F RID: 9007 RVA: 0x000D8D04 File Offset: 0x000D7D04
		public SignaturePolicyIdentifier()
		{
			this.sigPolicy = null;
		}

		// Token: 0x06002330 RID: 9008 RVA: 0x000D8D13 File Offset: 0x000D7D13
		public SignaturePolicyIdentifier(SignaturePolicyId signaturePolicyId)
		{
			if (signaturePolicyId == null)
			{
				throw new ArgumentNullException("signaturePolicyId");
			}
			this.sigPolicy = signaturePolicyId;
		}

		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x06002331 RID: 9009 RVA: 0x000D8D30 File Offset: 0x000D7D30
		public SignaturePolicyId SignaturePolicyId
		{
			get
			{
				return this.sigPolicy;
			}
		}

		// Token: 0x06002332 RID: 9010 RVA: 0x000D8D38 File Offset: 0x000D7D38
		public override Asn1Object ToAsn1Object()
		{
			if (this.sigPolicy != null)
			{
				return this.sigPolicy.ToAsn1Object();
			}
			return DerNull.Instance;
		}

		// Token: 0x0400186B RID: 6251
		private readonly SignaturePolicyId sigPolicy;
	}
}
