using System;
using System.Xml;

namespace System.ServiceModel
{
	// Token: 0x02000077 RID: 119
	internal class XmlSignatureDictionary
	{
		// Token: 0x06000278 RID: 632 RVA: 0x0000FAA0 File Offset: 0x0000DCA0
		public XmlSignatureDictionary(ServiceModelDictionary dictionary)
		{
			this.Algorithm = dictionary.CreateString("Algorithm", 8);
			this.URI = dictionary.CreateString("URI", 11);
			this.Reference = dictionary.CreateString("Reference", 12);
			this.Transforms = dictionary.CreateString("Transforms", 17);
			this.Transform = dictionary.CreateString("Transform", 18);
			this.DigestMethod = dictionary.CreateString("DigestMethod", 19);
			this.DigestValue = dictionary.CreateString("DigestValue", 20);
			this.Namespace = dictionary.CreateString("http://www.w3.org/2000/09/xmldsig#", 33);
			this.EnvelopedSignature = dictionary.CreateString("http://www.w3.org/2000/09/xmldsig#enveloped-signature", 34);
			this.KeyInfo = dictionary.CreateString("KeyInfo", 35);
			this.Signature = dictionary.CreateString("Signature", 41);
			this.SignedInfo = dictionary.CreateString("SignedInfo", 42);
			this.CanonicalizationMethod = dictionary.CreateString("CanonicalizationMethod", 43);
			this.SignatureMethod = dictionary.CreateString("SignatureMethod", 44);
			this.SignatureValue = dictionary.CreateString("SignatureValue", 45);
			this.KeyName = dictionary.CreateString("KeyName", 317);
			this.Type = dictionary.CreateString("Type", 59);
			this.MgmtData = dictionary.CreateString("MgmtData", 318);
			this.Prefix = dictionary.CreateString("", 81);
			this.KeyValue = dictionary.CreateString("KeyValue", 319);
			this.RsaKeyValue = dictionary.CreateString("RSAKeyValue", 320);
			this.Modulus = dictionary.CreateString("Modulus", 321);
			this.Exponent = dictionary.CreateString("Exponent", 322);
			this.X509Data = dictionary.CreateString("X509Data", 323);
			this.X509IssuerSerial = dictionary.CreateString("X509IssuerSerial", 324);
			this.X509IssuerName = dictionary.CreateString("X509IssuerName", 325);
			this.X509SerialNumber = dictionary.CreateString("X509SerialNumber", 326);
			this.X509Certificate = dictionary.CreateString("X509Certificate", 327);
		}

		// Token: 0x0400069C RID: 1692
		public XmlDictionaryString Algorithm;

		// Token: 0x0400069D RID: 1693
		public XmlDictionaryString URI;

		// Token: 0x0400069E RID: 1694
		public XmlDictionaryString Reference;

		// Token: 0x0400069F RID: 1695
		public XmlDictionaryString Transforms;

		// Token: 0x040006A0 RID: 1696
		public XmlDictionaryString Transform;

		// Token: 0x040006A1 RID: 1697
		public XmlDictionaryString DigestMethod;

		// Token: 0x040006A2 RID: 1698
		public XmlDictionaryString DigestValue;

		// Token: 0x040006A3 RID: 1699
		public XmlDictionaryString Namespace;

		// Token: 0x040006A4 RID: 1700
		public XmlDictionaryString EnvelopedSignature;

		// Token: 0x040006A5 RID: 1701
		public XmlDictionaryString KeyInfo;

		// Token: 0x040006A6 RID: 1702
		public XmlDictionaryString Signature;

		// Token: 0x040006A7 RID: 1703
		public XmlDictionaryString SignedInfo;

		// Token: 0x040006A8 RID: 1704
		public XmlDictionaryString CanonicalizationMethod;

		// Token: 0x040006A9 RID: 1705
		public XmlDictionaryString SignatureMethod;

		// Token: 0x040006AA RID: 1706
		public XmlDictionaryString SignatureValue;

		// Token: 0x040006AB RID: 1707
		public XmlDictionaryString KeyName;

		// Token: 0x040006AC RID: 1708
		public XmlDictionaryString Type;

		// Token: 0x040006AD RID: 1709
		public XmlDictionaryString MgmtData;

		// Token: 0x040006AE RID: 1710
		public XmlDictionaryString Prefix;

		// Token: 0x040006AF RID: 1711
		public XmlDictionaryString KeyValue;

		// Token: 0x040006B0 RID: 1712
		public XmlDictionaryString RsaKeyValue;

		// Token: 0x040006B1 RID: 1713
		public XmlDictionaryString Modulus;

		// Token: 0x040006B2 RID: 1714
		public XmlDictionaryString Exponent;

		// Token: 0x040006B3 RID: 1715
		public XmlDictionaryString X509Data;

		// Token: 0x040006B4 RID: 1716
		public XmlDictionaryString X509IssuerSerial;

		// Token: 0x040006B5 RID: 1717
		public XmlDictionaryString X509IssuerName;

		// Token: 0x040006B6 RID: 1718
		public XmlDictionaryString X509SerialNumber;

		// Token: 0x040006B7 RID: 1719
		public XmlDictionaryString X509Certificate;
	}
}
