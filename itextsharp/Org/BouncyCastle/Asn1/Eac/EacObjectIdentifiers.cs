using System;

namespace Org.BouncyCastle.Asn1.Eac
{
	// Token: 0x0200051F RID: 1311
	public abstract class EacObjectIdentifiers
	{
		// Token: 0x04001EB7 RID: 7863
		public static readonly DerObjectIdentifier bsi_de = new DerObjectIdentifier("0.4.0.127.0.7");

		// Token: 0x04001EB8 RID: 7864
		public static readonly DerObjectIdentifier id_PK = new DerObjectIdentifier(EacObjectIdentifiers.bsi_de + ".2.2.1");

		// Token: 0x04001EB9 RID: 7865
		public static readonly DerObjectIdentifier id_PK_DH = new DerObjectIdentifier(EacObjectIdentifiers.id_PK + ".1");

		// Token: 0x04001EBA RID: 7866
		public static readonly DerObjectIdentifier id_PK_ECDH = new DerObjectIdentifier(EacObjectIdentifiers.id_PK + ".2");

		// Token: 0x04001EBB RID: 7867
		public static readonly DerObjectIdentifier id_CA = new DerObjectIdentifier(EacObjectIdentifiers.bsi_de + ".2.2.3");

		// Token: 0x04001EBC RID: 7868
		public static readonly DerObjectIdentifier id_CA_DH = new DerObjectIdentifier(EacObjectIdentifiers.id_CA + ".1");

		// Token: 0x04001EBD RID: 7869
		public static readonly DerObjectIdentifier id_CA_DH_3DES_CBC_CBC = new DerObjectIdentifier(EacObjectIdentifiers.id_CA_DH + ".1");

		// Token: 0x04001EBE RID: 7870
		public static readonly DerObjectIdentifier id_CA_ECDH = new DerObjectIdentifier(EacObjectIdentifiers.id_CA + ".2");

		// Token: 0x04001EBF RID: 7871
		public static readonly DerObjectIdentifier id_CA_ECDH_3DES_CBC_CBC = new DerObjectIdentifier(EacObjectIdentifiers.id_CA_ECDH + ".1");

		// Token: 0x04001EC0 RID: 7872
		public static readonly DerObjectIdentifier id_TA = new DerObjectIdentifier(EacObjectIdentifiers.bsi_de + ".2.2.2");

		// Token: 0x04001EC1 RID: 7873
		public static readonly DerObjectIdentifier id_TA_RSA = new DerObjectIdentifier(EacObjectIdentifiers.id_TA + ".1");

		// Token: 0x04001EC2 RID: 7874
		public static readonly DerObjectIdentifier id_TA_RSA_v1_5_SHA_1 = new DerObjectIdentifier(EacObjectIdentifiers.id_TA_RSA + ".1");

		// Token: 0x04001EC3 RID: 7875
		public static readonly DerObjectIdentifier id_TA_RSA_v1_5_SHA_256 = new DerObjectIdentifier(EacObjectIdentifiers.id_TA_RSA + ".2");

		// Token: 0x04001EC4 RID: 7876
		public static readonly DerObjectIdentifier id_TA_RSA_PSS_SHA_1 = new DerObjectIdentifier(EacObjectIdentifiers.id_TA_RSA + ".3");

		// Token: 0x04001EC5 RID: 7877
		public static readonly DerObjectIdentifier id_TA_RSA_PSS_SHA_256 = new DerObjectIdentifier(EacObjectIdentifiers.id_TA_RSA + ".4");

		// Token: 0x04001EC6 RID: 7878
		public static readonly DerObjectIdentifier id_TA_ECDSA = new DerObjectIdentifier(EacObjectIdentifiers.id_TA + ".2");

		// Token: 0x04001EC7 RID: 7879
		public static readonly DerObjectIdentifier id_TA_ECDSA_SHA_1 = new DerObjectIdentifier(EacObjectIdentifiers.id_TA_ECDSA + ".1");

		// Token: 0x04001EC8 RID: 7880
		public static readonly DerObjectIdentifier id_TA_ECDSA_SHA_224 = new DerObjectIdentifier(EacObjectIdentifiers.id_TA_ECDSA + ".2");

		// Token: 0x04001EC9 RID: 7881
		public static readonly DerObjectIdentifier id_TA_ECDSA_SHA_256 = new DerObjectIdentifier(EacObjectIdentifiers.id_TA_ECDSA + ".3");

		// Token: 0x04001ECA RID: 7882
		public static readonly DerObjectIdentifier id_TA_ECDSA_SHA_384 = new DerObjectIdentifier(EacObjectIdentifiers.id_TA_ECDSA + ".4");

		// Token: 0x04001ECB RID: 7883
		public static readonly DerObjectIdentifier id_TA_ECDSA_SHA_512 = new DerObjectIdentifier(EacObjectIdentifiers.id_TA_ECDSA + ".5");
	}
}
