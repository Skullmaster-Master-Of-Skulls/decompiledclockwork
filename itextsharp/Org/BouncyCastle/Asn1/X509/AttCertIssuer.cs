using System;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x0200048A RID: 1162
	public class AttCertIssuer : Asn1Encodable, IAsn1Choice
	{
		// Token: 0x06002759 RID: 10073 RVA: 0x000ED9EC File Offset: 0x000EC9EC
		public static AttCertIssuer GetInstance(object obj)
		{
			if (obj is AttCertIssuer)
			{
				return (AttCertIssuer)obj;
			}
			if (obj is V2Form)
			{
				return new AttCertIssuer(V2Form.GetInstance(obj));
			}
			if (obj is GeneralNames)
			{
				return new AttCertIssuer((GeneralNames)obj);
			}
			if (obj is Asn1TaggedObject)
			{
				return new AttCertIssuer(V2Form.GetInstance((Asn1TaggedObject)obj, false));
			}
			if (obj is Asn1Sequence)
			{
				return new AttCertIssuer(GeneralNames.GetInstance(obj));
			}
			throw new ArgumentException("unknown object in factory: " + obj.GetType().Name, "obj");
		}

		// Token: 0x0600275A RID: 10074 RVA: 0x000EDA7D File Offset: 0x000ECA7D
		public static AttCertIssuer GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			return AttCertIssuer.GetInstance(obj.GetObject());
		}

		// Token: 0x0600275B RID: 10075 RVA: 0x000EDA8A File Offset: 0x000ECA8A
		public AttCertIssuer(GeneralNames names)
		{
			this.obj = names;
			this.choiceObj = this.obj.ToAsn1Object();
		}

		// Token: 0x0600275C RID: 10076 RVA: 0x000EDAAA File Offset: 0x000ECAAA
		public AttCertIssuer(V2Form v2Form)
		{
			this.obj = v2Form;
			this.choiceObj = new DerTaggedObject(false, 0, this.obj);
		}

		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x0600275D RID: 10077 RVA: 0x000EDACC File Offset: 0x000ECACC
		public Asn1Encodable Issuer
		{
			get
			{
				return this.obj;
			}
		}

		// Token: 0x0600275E RID: 10078 RVA: 0x000EDAD4 File Offset: 0x000ECAD4
		public override Asn1Object ToAsn1Object()
		{
			return this.choiceObj;
		}

		// Token: 0x04001B1F RID: 6943
		internal readonly Asn1Encodable obj;

		// Token: 0x04001B20 RID: 6944
		internal readonly Asn1Object choiceObj;
	}
}
