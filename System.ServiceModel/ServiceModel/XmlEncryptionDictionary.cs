using System;
using System.Xml;

namespace System.ServiceModel
{
	// Token: 0x02000076 RID: 118
	internal class XmlEncryptionDictionary
	{
		// Token: 0x06000277 RID: 631 RVA: 0x0000F8F4 File Offset: 0x0000DAF4
		public XmlEncryptionDictionary(ServiceModelDictionary dictionary)
		{
			this.Namespace = dictionary.CreateString("http://www.w3.org/2001/04/xmlenc#", 37);
			this.DataReference = dictionary.CreateString("DataReference", 46);
			this.EncryptedData = dictionary.CreateString("EncryptedData", 47);
			this.EncryptionMethod = dictionary.CreateString("EncryptionMethod", 48);
			this.CipherData = dictionary.CreateString("CipherData", 49);
			this.CipherValue = dictionary.CreateString("CipherValue", 50);
			this.ReferenceList = dictionary.CreateString("ReferenceList", 57);
			this.Encoding = dictionary.CreateString("Encoding", 308);
			this.MimeType = dictionary.CreateString("MimeType", 309);
			this.Type = dictionary.CreateString("Type", 59);
			this.Id = dictionary.CreateString("Id", 14);
			this.CarriedKeyName = dictionary.CreateString("CarriedKeyName", 310);
			this.Recipient = dictionary.CreateString("Recipient", 311);
			this.EncryptedKey = dictionary.CreateString("EncryptedKey", 312);
			this.URI = dictionary.CreateString("URI", 11);
			this.KeyReference = dictionary.CreateString("KeyReference", 313);
			this.Prefix = dictionary.CreateString("e", 314);
			this.ElementType = dictionary.CreateString("http://www.w3.org/2001/04/xmlenc#Element", 315);
			this.ContentType = dictionary.CreateString("http://www.w3.org/2001/04/xmlenc#Content", 316);
			this.AlgorithmAttribute = dictionary.CreateString("Algorithm", 8);
		}

		// Token: 0x04000688 RID: 1672
		public XmlDictionaryString Namespace;

		// Token: 0x04000689 RID: 1673
		public XmlDictionaryString DataReference;

		// Token: 0x0400068A RID: 1674
		public XmlDictionaryString EncryptedData;

		// Token: 0x0400068B RID: 1675
		public XmlDictionaryString EncryptionMethod;

		// Token: 0x0400068C RID: 1676
		public XmlDictionaryString CipherData;

		// Token: 0x0400068D RID: 1677
		public XmlDictionaryString CipherValue;

		// Token: 0x0400068E RID: 1678
		public XmlDictionaryString ReferenceList;

		// Token: 0x0400068F RID: 1679
		public XmlDictionaryString Encoding;

		// Token: 0x04000690 RID: 1680
		public XmlDictionaryString MimeType;

		// Token: 0x04000691 RID: 1681
		public XmlDictionaryString Type;

		// Token: 0x04000692 RID: 1682
		public XmlDictionaryString Id;

		// Token: 0x04000693 RID: 1683
		public XmlDictionaryString CarriedKeyName;

		// Token: 0x04000694 RID: 1684
		public XmlDictionaryString Recipient;

		// Token: 0x04000695 RID: 1685
		public XmlDictionaryString EncryptedKey;

		// Token: 0x04000696 RID: 1686
		public XmlDictionaryString URI;

		// Token: 0x04000697 RID: 1687
		public XmlDictionaryString KeyReference;

		// Token: 0x04000698 RID: 1688
		public XmlDictionaryString Prefix;

		// Token: 0x04000699 RID: 1689
		public XmlDictionaryString ElementType;

		// Token: 0x0400069A RID: 1690
		public XmlDictionaryString ContentType;

		// Token: 0x0400069B RID: 1691
		public XmlDictionaryString AlgorithmAttribute;
	}
}
