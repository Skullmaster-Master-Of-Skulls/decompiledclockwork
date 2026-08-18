using System;
using System.Xml;

namespace System.IdentityModel
{
	// Token: 0x020000D1 RID: 209
	internal class XmlEncryptionDictionary
	{
		// Token: 0x06000621 RID: 1569 RVA: 0x00018E20 File Offset: 0x00017020
		public XmlEncryptionDictionary(IdentityModelDictionary dictionary)
		{
			this.Namespace = dictionary.CreateString("http://www.w3.org/2001/04/xmlenc#", 156);
			this.DataReference = dictionary.CreateString("DataReference", 157);
			this.EncryptedData = dictionary.CreateString("EncryptedData", 158);
			this.EncryptionMethod = dictionary.CreateString("EncryptionMethod", 159);
			this.CipherData = dictionary.CreateString("CipherData", 160);
			this.CipherValue = dictionary.CreateString("CipherValue", 161);
			this.ReferenceList = dictionary.CreateString("ReferenceList", 162);
			this.Encoding = dictionary.CreateString("Encoding", 163);
			this.MimeType = dictionary.CreateString("MimeType", 164);
			this.Type = dictionary.CreateString("Type", 83);
			this.Id = dictionary.CreateString("Id", 3);
			this.CarriedKeyName = dictionary.CreateString("CarriedKeyName", 165);
			this.Recipient = dictionary.CreateString("Recipient", 166);
			this.EncryptedKey = dictionary.CreateString("EncryptedKey", 167);
			this.URI = dictionary.CreateString("URI", 1);
			this.KeyReference = dictionary.CreateString("KeyReference", 168);
			this.Prefix = dictionary.CreateString("e", 169);
			this.ElementType = dictionary.CreateString("http://www.w3.org/2001/04/xmlenc#Element", 170);
			this.ContentType = dictionary.CreateString("http://www.w3.org/2001/04/xmlenc#Content", 171);
			this.AlgorithmAttribute = dictionary.CreateString("Algorithm", 0);
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x00018FDC File Offset: 0x000171DC
		public XmlEncryptionDictionary(IXmlDictionary dictionary)
		{
			this.Namespace = this.LookupDictionaryString(dictionary, "http://www.w3.org/2001/04/xmlenc#");
			this.DataReference = this.LookupDictionaryString(dictionary, "DataReference");
			this.EncryptedData = this.LookupDictionaryString(dictionary, "EncryptedData");
			this.EncryptionMethod = this.LookupDictionaryString(dictionary, "EncryptionMethod");
			this.CipherData = this.LookupDictionaryString(dictionary, "CipherData");
			this.CipherValue = this.LookupDictionaryString(dictionary, "CipherValue");
			this.ReferenceList = this.LookupDictionaryString(dictionary, "ReferenceList");
			this.Encoding = this.LookupDictionaryString(dictionary, "Encoding");
			this.MimeType = this.LookupDictionaryString(dictionary, "MimeType");
			this.Type = this.LookupDictionaryString(dictionary, "Type");
			this.Id = this.LookupDictionaryString(dictionary, "Id");
			this.CarriedKeyName = this.LookupDictionaryString(dictionary, "CarriedKeyName");
			this.Recipient = this.LookupDictionaryString(dictionary, "Recipient");
			this.EncryptedKey = this.LookupDictionaryString(dictionary, "EncryptedKey");
			this.URI = this.LookupDictionaryString(dictionary, "URI");
			this.KeyReference = this.LookupDictionaryString(dictionary, "KeyReference");
			this.Prefix = this.LookupDictionaryString(dictionary, "e");
			this.ElementType = this.LookupDictionaryString(dictionary, "http://www.w3.org/2001/04/xmlenc#Element");
			this.ContentType = this.LookupDictionaryString(dictionary, "http://www.w3.org/2001/04/xmlenc#Content");
			this.AlgorithmAttribute = this.LookupDictionaryString(dictionary, "Algorithm");
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x00019158 File Offset: 0x00017358
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

		// Token: 0x040005ED RID: 1517
		public XmlDictionaryString Namespace;

		// Token: 0x040005EE RID: 1518
		public XmlDictionaryString DataReference;

		// Token: 0x040005EF RID: 1519
		public XmlDictionaryString EncryptedData;

		// Token: 0x040005F0 RID: 1520
		public XmlDictionaryString EncryptionMethod;

		// Token: 0x040005F1 RID: 1521
		public XmlDictionaryString CipherData;

		// Token: 0x040005F2 RID: 1522
		public XmlDictionaryString CipherValue;

		// Token: 0x040005F3 RID: 1523
		public XmlDictionaryString ReferenceList;

		// Token: 0x040005F4 RID: 1524
		public XmlDictionaryString Encoding;

		// Token: 0x040005F5 RID: 1525
		public XmlDictionaryString MimeType;

		// Token: 0x040005F6 RID: 1526
		public XmlDictionaryString Type;

		// Token: 0x040005F7 RID: 1527
		public XmlDictionaryString Id;

		// Token: 0x040005F8 RID: 1528
		public XmlDictionaryString CarriedKeyName;

		// Token: 0x040005F9 RID: 1529
		public XmlDictionaryString Recipient;

		// Token: 0x040005FA RID: 1530
		public XmlDictionaryString EncryptedKey;

		// Token: 0x040005FB RID: 1531
		public XmlDictionaryString URI;

		// Token: 0x040005FC RID: 1532
		public XmlDictionaryString KeyReference;

		// Token: 0x040005FD RID: 1533
		public XmlDictionaryString Prefix;

		// Token: 0x040005FE RID: 1534
		public XmlDictionaryString ElementType;

		// Token: 0x040005FF RID: 1535
		public XmlDictionaryString ContentType;

		// Token: 0x04000600 RID: 1536
		public XmlDictionaryString AlgorithmAttribute;
	}
}
