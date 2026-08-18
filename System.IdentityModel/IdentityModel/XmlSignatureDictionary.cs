using System;
using System.Xml;

namespace System.IdentityModel
{
	// Token: 0x020000D2 RID: 210
	internal class XmlSignatureDictionary
	{
		// Token: 0x06000624 RID: 1572 RVA: 0x00019190 File Offset: 0x00017390
		public XmlSignatureDictionary(IdentityModelDictionary dictionary)
		{
			this.Algorithm = dictionary.CreateString("Algorithm", 0);
			this.URI = dictionary.CreateString("URI", 1);
			this.Reference = dictionary.CreateString("Reference", 2);
			this.Transforms = dictionary.CreateString("Transforms", 4);
			this.Transform = dictionary.CreateString("Transform", 5);
			this.DigestMethod = dictionary.CreateString("DigestMethod", 6);
			this.DigestValue = dictionary.CreateString("DigestValue", 7);
			this.Namespace = dictionary.CreateString("http://www.w3.org/2000/09/xmldsig#", 8);
			this.EnvelopedSignature = dictionary.CreateString("http://www.w3.org/2000/09/xmldsig#enveloped-signature", 9);
			this.KeyInfo = dictionary.CreateString("KeyInfo", 10);
			this.Signature = dictionary.CreateString("Signature", 11);
			this.SignedInfo = dictionary.CreateString("SignedInfo", 12);
			this.CanonicalizationMethod = dictionary.CreateString("CanonicalizationMethod", 13);
			this.SignatureMethod = dictionary.CreateString("SignatureMethod", 14);
			this.SignatureValue = dictionary.CreateString("SignatureValue", 15);
			this.KeyName = dictionary.CreateString("KeyName", 82);
			this.Type = dictionary.CreateString("Type", 83);
			this.MgmtData = dictionary.CreateString("MgmtData", 84);
			this.Prefix = dictionary.CreateString("", 85);
			this.KeyValue = dictionary.CreateString("KeyValue", 86);
			this.RsaKeyValue = dictionary.CreateString("RSAKeyValue", 87);
			this.Modulus = dictionary.CreateString("Modulus", 88);
			this.Exponent = dictionary.CreateString("Exponent", 89);
			this.X509Data = dictionary.CreateString("X509Data", 90);
			this.X509IssuerSerial = dictionary.CreateString("X509IssuerSerial", 91);
			this.X509IssuerName = dictionary.CreateString("X509IssuerName", 92);
			this.X509SerialNumber = dictionary.CreateString("X509SerialNumber", 93);
			this.X509Certificate = dictionary.CreateString("X509Certificate", 94);
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x000193B0 File Offset: 0x000175B0
		public XmlSignatureDictionary(IXmlDictionary dictionary)
		{
			this.Algorithm = this.LookupDictionaryString(dictionary, "Algorithm");
			this.URI = this.LookupDictionaryString(dictionary, "URI");
			this.Reference = this.LookupDictionaryString(dictionary, "Reference");
			this.Transforms = this.LookupDictionaryString(dictionary, "Transforms");
			this.Transform = this.LookupDictionaryString(dictionary, "Transform");
			this.DigestMethod = this.LookupDictionaryString(dictionary, "DigestMethod");
			this.DigestValue = this.LookupDictionaryString(dictionary, "DigestValue");
			this.Namespace = this.LookupDictionaryString(dictionary, "http://www.w3.org/2000/09/xmldsig#");
			this.EnvelopedSignature = this.LookupDictionaryString(dictionary, "http://www.w3.org/2000/09/xmldsig#enveloped-signature");
			this.KeyInfo = this.LookupDictionaryString(dictionary, "KeyInfo");
			this.Signature = this.LookupDictionaryString(dictionary, "Signature");
			this.SignedInfo = this.LookupDictionaryString(dictionary, "SignedInfo");
			this.CanonicalizationMethod = this.LookupDictionaryString(dictionary, "CanonicalizationMethod");
			this.SignatureMethod = this.LookupDictionaryString(dictionary, "SignatureMethod");
			this.SignatureValue = this.LookupDictionaryString(dictionary, "SignatureValue");
			this.KeyName = this.LookupDictionaryString(dictionary, "KeyName");
			this.Type = this.LookupDictionaryString(dictionary, "Type");
			this.MgmtData = this.LookupDictionaryString(dictionary, "MgmtData");
			this.Prefix = this.LookupDictionaryString(dictionary, "");
			this.KeyValue = this.LookupDictionaryString(dictionary, "KeyValue");
			this.RsaKeyValue = this.LookupDictionaryString(dictionary, "RSAKeyValue");
			this.Modulus = this.LookupDictionaryString(dictionary, "Modulus");
			this.Exponent = this.LookupDictionaryString(dictionary, "Exponent");
			this.X509Data = this.LookupDictionaryString(dictionary, "X509Data");
			this.X509IssuerSerial = this.LookupDictionaryString(dictionary, "X509IssuerSerial");
			this.X509IssuerName = this.LookupDictionaryString(dictionary, "X509IssuerName");
			this.X509SerialNumber = this.LookupDictionaryString(dictionary, "X509SerialNumber");
			this.X509Certificate = this.LookupDictionaryString(dictionary, "X509Certificate");
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x000195BC File Offset: 0x000177BC
		private XmlDictionaryString LookupDictionaryString(IXmlDictionary dictionary, string value)
		{
			XmlDictionaryString result;
			if (!dictionary.TryLookup(value, out result))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("XDCannotFindValueInDictionaryString", new object[]
				{
					value
				}));
			}
			return result;
		}

		// Token: 0x04000601 RID: 1537
		public XmlDictionaryString Algorithm;

		// Token: 0x04000602 RID: 1538
		public XmlDictionaryString URI;

		// Token: 0x04000603 RID: 1539
		public XmlDictionaryString Reference;

		// Token: 0x04000604 RID: 1540
		public XmlDictionaryString Transforms;

		// Token: 0x04000605 RID: 1541
		public XmlDictionaryString Transform;

		// Token: 0x04000606 RID: 1542
		public XmlDictionaryString DigestMethod;

		// Token: 0x04000607 RID: 1543
		public XmlDictionaryString DigestValue;

		// Token: 0x04000608 RID: 1544
		public XmlDictionaryString Namespace;

		// Token: 0x04000609 RID: 1545
		public XmlDictionaryString EnvelopedSignature;

		// Token: 0x0400060A RID: 1546
		public XmlDictionaryString KeyInfo;

		// Token: 0x0400060B RID: 1547
		public XmlDictionaryString Signature;

		// Token: 0x0400060C RID: 1548
		public XmlDictionaryString SignedInfo;

		// Token: 0x0400060D RID: 1549
		public XmlDictionaryString CanonicalizationMethod;

		// Token: 0x0400060E RID: 1550
		public XmlDictionaryString SignatureMethod;

		// Token: 0x0400060F RID: 1551
		public XmlDictionaryString SignatureValue;

		// Token: 0x04000610 RID: 1552
		public XmlDictionaryString KeyName;

		// Token: 0x04000611 RID: 1553
		public XmlDictionaryString Type;

		// Token: 0x04000612 RID: 1554
		public XmlDictionaryString MgmtData;

		// Token: 0x04000613 RID: 1555
		public XmlDictionaryString Prefix;

		// Token: 0x04000614 RID: 1556
		public XmlDictionaryString KeyValue;

		// Token: 0x04000615 RID: 1557
		public XmlDictionaryString RsaKeyValue;

		// Token: 0x04000616 RID: 1558
		public XmlDictionaryString Modulus;

		// Token: 0x04000617 RID: 1559
		public XmlDictionaryString Exponent;

		// Token: 0x04000618 RID: 1560
		public XmlDictionaryString X509Data;

		// Token: 0x04000619 RID: 1561
		public XmlDictionaryString X509IssuerSerial;

		// Token: 0x0400061A RID: 1562
		public XmlDictionaryString X509IssuerName;

		// Token: 0x0400061B RID: 1563
		public XmlDictionaryString X509SerialNumber;

		// Token: 0x0400061C RID: 1564
		public XmlDictionaryString X509Certificate;
	}
}
