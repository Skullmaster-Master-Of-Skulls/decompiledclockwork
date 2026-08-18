using System;

namespace System.IdentityModel
{
	// Token: 0x020000E6 RID: 230
	internal static class XmlSignatureConstants
	{
		// Token: 0x04000796 RID: 1942
		public const string Namespace = "http://www.w3.org/2000/09/xmldsig#";

		// Token: 0x04000797 RID: 1943
		public const string Prefix = "ds";

		// Token: 0x02000254 RID: 596
		public static class Algorithms
		{
			// Token: 0x04000FC7 RID: 4039
			public const string ExcC14N = "http://www.w3.org/2001/10/xml-exc-c14n#";

			// Token: 0x04000FC8 RID: 4040
			public const string ExcC14NWithComments = "http://www.w3.org/2001/10/xml-exc-c14n#WithComments";

			// Token: 0x04000FC9 RID: 4041
			public const string Sha1 = "http://www.w3.org/2000/09/xmldsig#sha1";

			// Token: 0x04000FCA RID: 4042
			public const string EnvelopedSignature = "http://www.w3.org/2000/09/xmldsig#enveloped-signature";
		}

		// Token: 0x02000255 RID: 597
		public static class Attributes
		{
			// Token: 0x04000FCB RID: 4043
			public const string Algorithm = "Algorithm";

			// Token: 0x04000FCC RID: 4044
			public const string Id = "Id";

			// Token: 0x04000FCD RID: 4045
			public const string Type = "Type";

			// Token: 0x04000FCE RID: 4046
			public const string URI = "URI";
		}

		// Token: 0x02000256 RID: 598
		public static class Elements
		{
			// Token: 0x04000FCF RID: 4047
			public const string CanonicalizationMethod = "CanonicalizationMethod";

			// Token: 0x04000FD0 RID: 4048
			public const string DigestMethod = "DigestMethod";

			// Token: 0x04000FD1 RID: 4049
			public const string DigestValue = "DigestValue";

			// Token: 0x04000FD2 RID: 4050
			public const string Exponent = "Exponent";

			// Token: 0x04000FD3 RID: 4051
			public const string KeyInfo = "KeyInfo";

			// Token: 0x04000FD4 RID: 4052
			public const string KeyName = "KeyName";

			// Token: 0x04000FD5 RID: 4053
			public const string KeyValue = "KeyValue";

			// Token: 0x04000FD6 RID: 4054
			public const string Modulus = "Modulus";

			// Token: 0x04000FD7 RID: 4055
			public const string Object = "Object";

			// Token: 0x04000FD8 RID: 4056
			public const string Reference = "Reference";

			// Token: 0x04000FD9 RID: 4057
			public const string RetrievalMethod = "RetrievalMethod";

			// Token: 0x04000FDA RID: 4058
			public const string RsaKeyValue = "RsaKeyValue";

			// Token: 0x04000FDB RID: 4059
			public const string Signature = "Signature";

			// Token: 0x04000FDC RID: 4060
			public const string SignatureMethod = "SignatureMethod";

			// Token: 0x04000FDD RID: 4061
			public const string SignatureValue = "SignatureValue";

			// Token: 0x04000FDE RID: 4062
			public const string SignedInfo = "SignedInfo";

			// Token: 0x04000FDF RID: 4063
			public const string Transform = "Transform";

			// Token: 0x04000FE0 RID: 4064
			public const string Transforms = "Transforms";

			// Token: 0x04000FE1 RID: 4065
			public const string X509Data = "X509Data";

			// Token: 0x04000FE2 RID: 4066
			public const string X509IssuerName = "X509IssuerName";

			// Token: 0x04000FE3 RID: 4067
			public const string X509IssuerSerial = "X509IssuerSerial";

			// Token: 0x04000FE4 RID: 4068
			public const string X509SerialNumber = "X509SerialNumber";

			// Token: 0x04000FE5 RID: 4069
			public const string X509SubjectName = "X509SubjectName";

			// Token: 0x04000FE6 RID: 4070
			public const string X509Certificate = "X509Certificate";

			// Token: 0x04000FE7 RID: 4071
			public const string X509SKI = "X509SKI";
		}
	}
}
