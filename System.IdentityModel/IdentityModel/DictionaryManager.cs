using System;
using System.Xml;

namespace System.IdentityModel
{
	// Token: 0x02000037 RID: 55
	internal class DictionaryManager
	{
		// Token: 0x060001E9 RID: 489 RVA: 0x0000871C File Offset: 0x0000691C
		public DictionaryManager()
		{
			this.samlDictionary = XD.SamlDictionary;
			this.sigantureDictionary = XD.XmlSignatureDictionary;
			this.utilityDictionary = XD.UtilityDictionary;
			this.exclusiveC14NDictionary = XD.ExclusiveC14NDictionary;
			this.securityAlgorithmDictionary = XD.SecurityAlgorithmDictionary;
			this.parentDictionary = XD.Dictionary;
			this.securityJan2004Dictionary = XD.SecurityJan2004Dictionary;
			this.securityJanXXX2005Dictionary = XD.SecurityXXX2005Dictionary;
			this.secureConversationFeb2005Dictionary = XD.SecureConversationFeb2005Dictionary;
			this.trustFeb2005Dictionary = XD.TrustFeb2005Dictionary;
			this.xmlEncryptionDictionary = XD.XmlEncryptionDictionary;
			this.secureConversationDec2005Dictionary = XD.SecureConversationDec2005Dictionary;
			this.securityAlgorithmDec2005Dictionary = XD.SecurityAlgorithmDec2005Dictionary;
			this.trustDec2005Dictionary = XD.TrustDec2005Dictionary;
		}

		// Token: 0x060001EA RID: 490 RVA: 0x000087CC File Offset: 0x000069CC
		public DictionaryManager(IXmlDictionary parentDictionary)
		{
			this.samlDictionary = new SamlDictionary(parentDictionary);
			this.sigantureDictionary = new XmlSignatureDictionary(parentDictionary);
			this.utilityDictionary = new UtilityDictionary(parentDictionary);
			this.exclusiveC14NDictionary = new ExclusiveC14NDictionary(parentDictionary);
			this.securityAlgorithmDictionary = new SecurityAlgorithmDictionary(parentDictionary);
			this.securityJan2004Dictionary = new SecurityJan2004Dictionary(parentDictionary);
			this.securityJanXXX2005Dictionary = new SecurityXXX2005Dictionary(parentDictionary);
			this.secureConversationFeb2005Dictionary = new SecureConversationFeb2005Dictionary(parentDictionary);
			this.trustFeb2005Dictionary = new TrustFeb2005Dictionary(parentDictionary);
			this.xmlEncryptionDictionary = new XmlEncryptionDictionary(parentDictionary);
			this.parentDictionary = parentDictionary;
			this.secureConversationDec2005Dictionary = XD.SecureConversationDec2005Dictionary;
			this.securityAlgorithmDec2005Dictionary = XD.SecurityAlgorithmDec2005Dictionary;
			this.trustDec2005Dictionary = XD.TrustDec2005Dictionary;
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001EB RID: 491 RVA: 0x0000887F File Offset: 0x00006A7F
		// (set) Token: 0x060001EC RID: 492 RVA: 0x00008887 File Offset: 0x00006A87
		public SamlDictionary SamlDictionary
		{
			get
			{
				return this.samlDictionary;
			}
			set
			{
				this.samlDictionary = value;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001ED RID: 493 RVA: 0x00008890 File Offset: 0x00006A90
		// (set) Token: 0x060001EE RID: 494 RVA: 0x00008898 File Offset: 0x00006A98
		public XmlSignatureDictionary XmlSignatureDictionary
		{
			get
			{
				return this.sigantureDictionary;
			}
			set
			{
				this.sigantureDictionary = value;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001EF RID: 495 RVA: 0x000088A1 File Offset: 0x00006AA1
		// (set) Token: 0x060001F0 RID: 496 RVA: 0x000088A9 File Offset: 0x00006AA9
		public UtilityDictionary UtilityDictionary
		{
			get
			{
				return this.utilityDictionary;
			}
			set
			{
				this.utilityDictionary = value;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x000088B2 File Offset: 0x00006AB2
		// (set) Token: 0x060001F2 RID: 498 RVA: 0x000088BA File Offset: 0x00006ABA
		public ExclusiveC14NDictionary ExclusiveC14NDictionary
		{
			get
			{
				return this.exclusiveC14NDictionary;
			}
			set
			{
				this.exclusiveC14NDictionary = value;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x000088C3 File Offset: 0x00006AC3
		// (set) Token: 0x060001F4 RID: 500 RVA: 0x000088CB File Offset: 0x00006ACB
		public SecurityAlgorithmDec2005Dictionary SecurityAlgorithmDec2005Dictionary
		{
			get
			{
				return this.securityAlgorithmDec2005Dictionary;
			}
			set
			{
				this.securityAlgorithmDec2005Dictionary = value;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x000088D4 File Offset: 0x00006AD4
		// (set) Token: 0x060001F6 RID: 502 RVA: 0x000088DC File Offset: 0x00006ADC
		public SecurityAlgorithmDictionary SecurityAlgorithmDictionary
		{
			get
			{
				return this.securityAlgorithmDictionary;
			}
			set
			{
				this.securityAlgorithmDictionary = value;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x000088E5 File Offset: 0x00006AE5
		// (set) Token: 0x060001F8 RID: 504 RVA: 0x000088ED File Offset: 0x00006AED
		public SecurityJan2004Dictionary SecurityJan2004Dictionary
		{
			get
			{
				return this.securityJan2004Dictionary;
			}
			set
			{
				this.securityJan2004Dictionary = value;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x000088F6 File Offset: 0x00006AF6
		// (set) Token: 0x060001FA RID: 506 RVA: 0x000088FE File Offset: 0x00006AFE
		public SecurityXXX2005Dictionary SecurityJanXXX2005Dictionary
		{
			get
			{
				return this.securityJanXXX2005Dictionary;
			}
			set
			{
				this.securityJanXXX2005Dictionary = value;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001FB RID: 507 RVA: 0x00008907 File Offset: 0x00006B07
		// (set) Token: 0x060001FC RID: 508 RVA: 0x0000890F File Offset: 0x00006B0F
		public SecureConversationDec2005Dictionary SecureConversationDec2005Dictionary
		{
			get
			{
				return this.secureConversationDec2005Dictionary;
			}
			set
			{
				this.secureConversationDec2005Dictionary = value;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001FD RID: 509 RVA: 0x00008918 File Offset: 0x00006B18
		// (set) Token: 0x060001FE RID: 510 RVA: 0x00008920 File Offset: 0x00006B20
		public SecureConversationFeb2005Dictionary SecureConversationFeb2005Dictionary
		{
			get
			{
				return this.secureConversationFeb2005Dictionary;
			}
			set
			{
				this.secureConversationFeb2005Dictionary = value;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060001FF RID: 511 RVA: 0x00008929 File Offset: 0x00006B29
		// (set) Token: 0x06000200 RID: 512 RVA: 0x00008931 File Offset: 0x00006B31
		public TrustDec2005Dictionary TrustDec2005Dictionary
		{
			get
			{
				return this.trustDec2005Dictionary;
			}
			set
			{
				this.trustDec2005Dictionary = value;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000201 RID: 513 RVA: 0x0000893A File Offset: 0x00006B3A
		// (set) Token: 0x06000202 RID: 514 RVA: 0x00008942 File Offset: 0x00006B42
		public TrustFeb2005Dictionary TrustFeb2005Dictionary
		{
			get
			{
				return this.trustFeb2005Dictionary;
			}
			set
			{
				this.trustFeb2005Dictionary = value;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000203 RID: 515 RVA: 0x0000894B File Offset: 0x00006B4B
		// (set) Token: 0x06000204 RID: 516 RVA: 0x00008953 File Offset: 0x00006B53
		public XmlEncryptionDictionary XmlEncryptionDictionary
		{
			get
			{
				return this.xmlEncryptionDictionary;
			}
			set
			{
				this.xmlEncryptionDictionary = value;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000205 RID: 517 RVA: 0x0000895C File Offset: 0x00006B5C
		// (set) Token: 0x06000206 RID: 518 RVA: 0x00008964 File Offset: 0x00006B64
		public IXmlDictionary ParentDictionary
		{
			get
			{
				return this.parentDictionary;
			}
			set
			{
				this.parentDictionary = value;
			}
		}

		// Token: 0x04000132 RID: 306
		private SamlDictionary samlDictionary;

		// Token: 0x04000133 RID: 307
		private XmlSignatureDictionary sigantureDictionary;

		// Token: 0x04000134 RID: 308
		private UtilityDictionary utilityDictionary;

		// Token: 0x04000135 RID: 309
		private ExclusiveC14NDictionary exclusiveC14NDictionary;

		// Token: 0x04000136 RID: 310
		private SecurityAlgorithmDec2005Dictionary securityAlgorithmDec2005Dictionary;

		// Token: 0x04000137 RID: 311
		private SecurityAlgorithmDictionary securityAlgorithmDictionary;

		// Token: 0x04000138 RID: 312
		private SecurityJan2004Dictionary securityJan2004Dictionary;

		// Token: 0x04000139 RID: 313
		private SecurityXXX2005Dictionary securityJanXXX2005Dictionary;

		// Token: 0x0400013A RID: 314
		private SecureConversationDec2005Dictionary secureConversationDec2005Dictionary;

		// Token: 0x0400013B RID: 315
		private SecureConversationFeb2005Dictionary secureConversationFeb2005Dictionary;

		// Token: 0x0400013C RID: 316
		private TrustFeb2005Dictionary trustFeb2005Dictionary;

		// Token: 0x0400013D RID: 317
		private TrustDec2005Dictionary trustDec2005Dictionary;

		// Token: 0x0400013E RID: 318
		private XmlEncryptionDictionary xmlEncryptionDictionary;

		// Token: 0x0400013F RID: 319
		private IXmlDictionary parentDictionary;
	}
}
