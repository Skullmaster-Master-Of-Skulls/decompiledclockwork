using System;

namespace System.IdentityModel
{
	// Token: 0x020000C3 RID: 195
	internal static class XD
	{
		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060005EA RID: 1514 RVA: 0x00015AB1 File Offset: 0x00013CB1
		public static IdentityModelDictionary Dictionary
		{
			get
			{
				return IdentityModelDictionary.CurrentVersion;
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060005EB RID: 1515 RVA: 0x00015AB8 File Offset: 0x00013CB8
		public static ExclusiveC14NDictionary ExclusiveC14NDictionary
		{
			get
			{
				if (XD.exclusiveC14NDictionary == null)
				{
					XD.exclusiveC14NDictionary = new ExclusiveC14NDictionary(XD.Dictionary);
				}
				return XD.exclusiveC14NDictionary;
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060005EC RID: 1516 RVA: 0x00015AD5 File Offset: 0x00013CD5
		public static SamlDictionary SamlDictionary
		{
			get
			{
				if (XD.samlDictionary == null)
				{
					XD.samlDictionary = new SamlDictionary(XD.Dictionary);
				}
				return XD.samlDictionary;
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060005ED RID: 1517 RVA: 0x00015AF2 File Offset: 0x00013CF2
		public static SecureConversationDec2005Dictionary SecureConversationDec2005Dictionary
		{
			get
			{
				if (XD.secureConversationDec2005Dictionary == null)
				{
					XD.secureConversationDec2005Dictionary = new SecureConversationDec2005Dictionary(XD.Dictionary);
				}
				return XD.secureConversationDec2005Dictionary;
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060005EE RID: 1518 RVA: 0x00015B0F File Offset: 0x00013D0F
		public static SecureConversationFeb2005Dictionary SecureConversationFeb2005Dictionary
		{
			get
			{
				if (XD.secureConversationFeb2005Dictionary == null)
				{
					XD.secureConversationFeb2005Dictionary = new SecureConversationFeb2005Dictionary(XD.Dictionary);
				}
				return XD.secureConversationFeb2005Dictionary;
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060005EF RID: 1519 RVA: 0x00015B2C File Offset: 0x00013D2C
		public static SecurityAlgorithmDictionary SecurityAlgorithmDictionary
		{
			get
			{
				if (XD.securityAlgorithmDictionary == null)
				{
					XD.securityAlgorithmDictionary = new SecurityAlgorithmDictionary(XD.Dictionary);
				}
				return XD.securityAlgorithmDictionary;
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060005F0 RID: 1520 RVA: 0x00015B49 File Offset: 0x00013D49
		public static SecurityAlgorithmDec2005Dictionary SecurityAlgorithmDec2005Dictionary
		{
			get
			{
				if (XD.securityAlgorithmDec2005Dictionary == null)
				{
					XD.securityAlgorithmDec2005Dictionary = new SecurityAlgorithmDec2005Dictionary(XD.Dictionary);
				}
				return XD.securityAlgorithmDec2005Dictionary;
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060005F1 RID: 1521 RVA: 0x00015B66 File Offset: 0x00013D66
		public static SecurityJan2004Dictionary SecurityJan2004Dictionary
		{
			get
			{
				if (XD.securityJan2004Dictionary == null)
				{
					XD.securityJan2004Dictionary = new SecurityJan2004Dictionary(XD.Dictionary);
				}
				return XD.securityJan2004Dictionary;
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060005F2 RID: 1522 RVA: 0x00015B83 File Offset: 0x00013D83
		public static SecurityXXX2005Dictionary SecurityXXX2005Dictionary
		{
			get
			{
				if (XD.securityXXX2005Dictionary == null)
				{
					XD.securityXXX2005Dictionary = new SecurityXXX2005Dictionary(XD.Dictionary);
				}
				return XD.securityXXX2005Dictionary;
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060005F3 RID: 1523 RVA: 0x00015BA0 File Offset: 0x00013DA0
		public static TrustDec2005Dictionary TrustDec2005Dictionary
		{
			get
			{
				if (XD.trustDec2005Dictionary == null)
				{
					XD.trustDec2005Dictionary = new TrustDec2005Dictionary(XD.Dictionary);
				}
				return XD.trustDec2005Dictionary;
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060005F4 RID: 1524 RVA: 0x00015BBD File Offset: 0x00013DBD
		public static TrustFeb2005Dictionary TrustFeb2005Dictionary
		{
			get
			{
				if (XD.trustFeb2005Dictionary == null)
				{
					XD.trustFeb2005Dictionary = new TrustFeb2005Dictionary(XD.Dictionary);
				}
				return XD.trustFeb2005Dictionary;
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060005F5 RID: 1525 RVA: 0x00015BDA File Offset: 0x00013DDA
		public static UtilityDictionary UtilityDictionary
		{
			get
			{
				if (XD.utilityDictionary == null)
				{
					XD.utilityDictionary = new UtilityDictionary(XD.Dictionary);
				}
				return XD.utilityDictionary;
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060005F6 RID: 1526 RVA: 0x00015BF7 File Offset: 0x00013DF7
		public static XmlEncryptionDictionary XmlEncryptionDictionary
		{
			get
			{
				if (XD.xmlEncryptionDictionary == null)
				{
					XD.xmlEncryptionDictionary = new XmlEncryptionDictionary(XD.Dictionary);
				}
				return XD.xmlEncryptionDictionary;
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060005F7 RID: 1527 RVA: 0x00015C14 File Offset: 0x00013E14
		public static XmlSignatureDictionary XmlSignatureDictionary
		{
			get
			{
				if (XD.xmlSignatureDictionary == null)
				{
					XD.xmlSignatureDictionary = new XmlSignatureDictionary(XD.Dictionary);
				}
				return XD.xmlSignatureDictionary;
			}
		}

		// Token: 0x04000500 RID: 1280
		private static ExclusiveC14NDictionary exclusiveC14NDictionary;

		// Token: 0x04000501 RID: 1281
		private static SamlDictionary samlDictionary;

		// Token: 0x04000502 RID: 1282
		private static SecureConversationDec2005Dictionary secureConversationDec2005Dictionary;

		// Token: 0x04000503 RID: 1283
		private static SecureConversationFeb2005Dictionary secureConversationFeb2005Dictionary;

		// Token: 0x04000504 RID: 1284
		private static SecurityAlgorithmDictionary securityAlgorithmDictionary;

		// Token: 0x04000505 RID: 1285
		private static SecurityAlgorithmDec2005Dictionary securityAlgorithmDec2005Dictionary;

		// Token: 0x04000506 RID: 1286
		private static SecurityJan2004Dictionary securityJan2004Dictionary;

		// Token: 0x04000507 RID: 1287
		private static SecurityXXX2005Dictionary securityXXX2005Dictionary;

		// Token: 0x04000508 RID: 1288
		private static TrustDec2005Dictionary trustDec2005Dictionary;

		// Token: 0x04000509 RID: 1289
		private static TrustFeb2005Dictionary trustFeb2005Dictionary;

		// Token: 0x0400050A RID: 1290
		private static UtilityDictionary utilityDictionary;

		// Token: 0x0400050B RID: 1291
		private static XmlEncryptionDictionary xmlEncryptionDictionary;

		// Token: 0x0400050C RID: 1292
		private static XmlSignatureDictionary xmlSignatureDictionary;
	}
}
