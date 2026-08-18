using System;

namespace System.IdentityModel
{
	// Token: 0x020000E4 RID: 228
	internal static class XmlEncryptionConstants
	{
		// Token: 0x04000794 RID: 1940
		public const string Namespace = "http://www.w3.org/2001/04/xmlenc#";

		// Token: 0x04000795 RID: 1941
		public const string Prefix = "xenc";

		// Token: 0x02000251 RID: 593
		public static class Attributes
		{
			// Token: 0x04000FB0 RID: 4016
			public const string Algorithm = "Algorithm";

			// Token: 0x04000FB1 RID: 4017
			public const string Encoding = "Encoding";

			// Token: 0x04000FB2 RID: 4018
			public const string Id = "Id";

			// Token: 0x04000FB3 RID: 4019
			public const string MimeType = "MimeType";

			// Token: 0x04000FB4 RID: 4020
			public const string Recipient = "Recipient";

			// Token: 0x04000FB5 RID: 4021
			public const string Type = "Type";

			// Token: 0x04000FB6 RID: 4022
			public const string Uri = "URI";
		}

		// Token: 0x02000252 RID: 594
		public static class Elements
		{
			// Token: 0x04000FB7 RID: 4023
			public const string CarriedKeyName = "CarriedKeyName";

			// Token: 0x04000FB8 RID: 4024
			public const string CipherData = "CipherData";

			// Token: 0x04000FB9 RID: 4025
			public const string CipherReference = "CiperReference";

			// Token: 0x04000FBA RID: 4026
			public const string CipherValue = "CipherValue";

			// Token: 0x04000FBB RID: 4027
			public const string DataReference = "DataReference";

			// Token: 0x04000FBC RID: 4028
			public const string EncryptedData = "EncryptedData";

			// Token: 0x04000FBD RID: 4029
			public const string EncryptedKey = "EncryptedKey";

			// Token: 0x04000FBE RID: 4030
			public const string EncryptionMethod = "EncryptionMethod";

			// Token: 0x04000FBF RID: 4031
			public const string EncryptionProperties = "EncryptionProperties";

			// Token: 0x04000FC0 RID: 4032
			public const string KeyReference = "KeyReference";

			// Token: 0x04000FC1 RID: 4033
			public const string KeySize = "KeySize";

			// Token: 0x04000FC2 RID: 4034
			public const string OaepParams = "OAEPparams";

			// Token: 0x04000FC3 RID: 4035
			public const string Recipient = "Recipient";

			// Token: 0x04000FC4 RID: 4036
			public const string ReferenceList = "ReferenceList";
		}

		// Token: 0x02000253 RID: 595
		public static class EncryptedDataTypes
		{
			// Token: 0x04000FC5 RID: 4037
			public const string Element = "http://www.w3.org/2001/04/xmlenc#Element";

			// Token: 0x04000FC6 RID: 4038
			public const string Content = "http://www.w3.org/2001/04/xmlenc#Content";
		}
	}
}
