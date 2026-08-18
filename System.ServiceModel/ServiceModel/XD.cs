using System;

namespace System.ServiceModel
{
	// Token: 0x02000055 RID: 85
	internal static class XD
	{
		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000233 RID: 563 RVA: 0x0000C96A File Offset: 0x0000AB6A
		public static ServiceModelDictionary Dictionary
		{
			get
			{
				return ServiceModelDictionary.CurrentVersion;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000234 RID: 564 RVA: 0x0000C971 File Offset: 0x0000AB71
		public static ActivityIdFlowDictionary ActivityIdFlowDictionary
		{
			get
			{
				if (XD.activityIdFlowDictionary == null)
				{
					XD.activityIdFlowDictionary = new ActivityIdFlowDictionary(XD.Dictionary);
				}
				return XD.activityIdFlowDictionary;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000235 RID: 565 RVA: 0x0000C98E File Offset: 0x0000AB8E
		public static AddressingDictionary AddressingDictionary
		{
			get
			{
				if (XD.addressingDictionary == null)
				{
					XD.addressingDictionary = new AddressingDictionary(XD.Dictionary);
				}
				return XD.addressingDictionary;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000236 RID: 566 RVA: 0x0000C9AB File Offset: 0x0000ABAB
		public static Addressing10Dictionary Addressing10Dictionary
		{
			get
			{
				if (XD.addressing10Dictionary == null)
				{
					XD.addressing10Dictionary = new Addressing10Dictionary(XD.Dictionary);
				}
				return XD.addressing10Dictionary;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000237 RID: 567 RVA: 0x0000C9C8 File Offset: 0x0000ABC8
		public static Addressing200408Dictionary Addressing200408Dictionary
		{
			get
			{
				if (XD.addressing200408Dictionary == null)
				{
					XD.addressing200408Dictionary = new Addressing200408Dictionary(XD.Dictionary);
				}
				return XD.addressing200408Dictionary;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000238 RID: 568 RVA: 0x0000C9E5 File Offset: 0x0000ABE5
		public static AddressingNoneDictionary AddressingNoneDictionary
		{
			get
			{
				if (XD.addressingNoneDictionary == null)
				{
					XD.addressingNoneDictionary = new AddressingNoneDictionary(XD.Dictionary);
				}
				return XD.addressingNoneDictionary;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000239 RID: 569 RVA: 0x0000CA02 File Offset: 0x0000AC02
		public static AtomicTransactionExternalDictionary AtomicTransactionExternalDictionary
		{
			get
			{
				if (XD.atomicTransactionExternalDictionary == null)
				{
					XD.atomicTransactionExternalDictionary = new AtomicTransactionExternalDictionary(XD.Dictionary);
				}
				return XD.atomicTransactionExternalDictionary;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600023A RID: 570 RVA: 0x0000CA1F File Offset: 0x0000AC1F
		public static AtomicTransactionExternal10Dictionary AtomicTransactionExternal10Dictionary
		{
			get
			{
				if (XD.atomicTransactionExternal10Dictionary == null)
				{
					XD.atomicTransactionExternal10Dictionary = new AtomicTransactionExternal10Dictionary(XD.Dictionary);
				}
				return XD.atomicTransactionExternal10Dictionary;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600023B RID: 571 RVA: 0x0000CA3C File Offset: 0x0000AC3C
		public static CoordinationExternalDictionary CoordinationExternalDictionary
		{
			get
			{
				if (XD.coordinationExternalDictionary == null)
				{
					XD.coordinationExternalDictionary = new CoordinationExternalDictionary(XD.Dictionary);
				}
				return XD.coordinationExternalDictionary;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600023C RID: 572 RVA: 0x0000CA59 File Offset: 0x0000AC59
		public static CoordinationExternal10Dictionary CoordinationExternal10Dictionary
		{
			get
			{
				if (XD.coordinationExternal10Dictionary == null)
				{
					XD.coordinationExternal10Dictionary = new CoordinationExternal10Dictionary(XD.Dictionary);
				}
				return XD.coordinationExternal10Dictionary;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600023D RID: 573 RVA: 0x0000CA76 File Offset: 0x0000AC76
		public static DotNetAddressingDictionary DotNetAddressingDictionary
		{
			get
			{
				if (XD.dotNetAddressingDictionary == null)
				{
					XD.dotNetAddressingDictionary = new DotNetAddressingDictionary(XD.Dictionary);
				}
				return XD.dotNetAddressingDictionary;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600023E RID: 574 RVA: 0x0000CA93 File Offset: 0x0000AC93
		public static DotNetAtomicTransactionExternalDictionary DotNetAtomicTransactionExternalDictionary
		{
			get
			{
				if (XD.dotNetAtomicTransactionExternalDictionary == null)
				{
					XD.dotNetAtomicTransactionExternalDictionary = new DotNetAtomicTransactionExternalDictionary(XD.Dictionary);
				}
				return XD.dotNetAtomicTransactionExternalDictionary;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600023F RID: 575 RVA: 0x0000CAB0 File Offset: 0x0000ACB0
		public static DotNetOneWayDictionary DotNetOneWayDictionary
		{
			get
			{
				if (XD.dotNetOneWayDictionary == null)
				{
					XD.dotNetOneWayDictionary = new DotNetOneWayDictionary(XD.Dictionary);
				}
				return XD.dotNetOneWayDictionary;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000240 RID: 576 RVA: 0x0000CACD File Offset: 0x0000ACCD
		public static DotNetSecurityDictionary DotNetSecurityDictionary
		{
			get
			{
				if (XD.dotNetSecurityDictionary == null)
				{
					XD.dotNetSecurityDictionary = new DotNetSecurityDictionary(XD.Dictionary);
				}
				return XD.dotNetSecurityDictionary;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000241 RID: 577 RVA: 0x0000CAEA File Offset: 0x0000ACEA
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

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000242 RID: 578 RVA: 0x0000CB07 File Offset: 0x0000AD07
		public static MessageDictionary MessageDictionary
		{
			get
			{
				if (XD.messageDictionary == null)
				{
					XD.messageDictionary = new MessageDictionary(XD.Dictionary);
				}
				return XD.messageDictionary;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000243 RID: 579 RVA: 0x0000CB24 File Offset: 0x0000AD24
		public static Message11Dictionary Message11Dictionary
		{
			get
			{
				if (XD.message11Dictionary == null)
				{
					XD.message11Dictionary = new Message11Dictionary(XD.Dictionary);
				}
				return XD.message11Dictionary;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000244 RID: 580 RVA: 0x0000CB41 File Offset: 0x0000AD41
		public static Message12Dictionary Message12Dictionary
		{
			get
			{
				if (XD.message12Dictionary == null)
				{
					XD.message12Dictionary = new Message12Dictionary(XD.Dictionary);
				}
				return XD.message12Dictionary;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000245 RID: 581 RVA: 0x0000CB5E File Offset: 0x0000AD5E
		public static OleTxTransactionExternalDictionary OleTxTransactionExternalDictionary
		{
			get
			{
				if (XD.oleTxTransactionExternalDictionary == null)
				{
					XD.oleTxTransactionExternalDictionary = new OleTxTransactionExternalDictionary(XD.Dictionary);
				}
				return XD.oleTxTransactionExternalDictionary;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000246 RID: 582 RVA: 0x0000CB7B File Offset: 0x0000AD7B
		public static PeerWireStringsDictionary PeerWireStringsDictionary
		{
			get
			{
				if (XD.peerWireStringsDictionary == null)
				{
					XD.peerWireStringsDictionary = new PeerWireStringsDictionary(XD.Dictionary);
				}
				return XD.peerWireStringsDictionary;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000247 RID: 583 RVA: 0x0000CB98 File Offset: 0x0000AD98
		public static PolicyDictionary PolicyDictionary
		{
			get
			{
				if (XD.policyDictionary == null)
				{
					XD.policyDictionary = new PolicyDictionary(XD.Dictionary);
				}
				return XD.policyDictionary;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000248 RID: 584 RVA: 0x0000CBB5 File Offset: 0x0000ADB5
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

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000249 RID: 585 RVA: 0x0000CBD2 File Offset: 0x0000ADD2
		public static SecureConversationApr2004Dictionary SecureConversationApr2004Dictionary
		{
			get
			{
				if (XD.secureConversationApr2004Dictionary == null)
				{
					XD.secureConversationApr2004Dictionary = new SecureConversationApr2004Dictionary(XD.Dictionary);
				}
				return XD.secureConversationApr2004Dictionary;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600024A RID: 586 RVA: 0x0000CBEF File Offset: 0x0000ADEF
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

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600024B RID: 587 RVA: 0x0000CC0C File Offset: 0x0000AE0C
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

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600024C RID: 588 RVA: 0x0000CC29 File Offset: 0x0000AE29
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

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600024D RID: 589 RVA: 0x0000CC46 File Offset: 0x0000AE46
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

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600024E RID: 590 RVA: 0x0000CC63 File Offset: 0x0000AE63
		public static SerializationDictionary SerializationDictionary
		{
			get
			{
				if (XD.serializationDictionary == null)
				{
					XD.serializationDictionary = new SerializationDictionary(XD.Dictionary);
				}
				return XD.serializationDictionary;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600024F RID: 591 RVA: 0x0000CC80 File Offset: 0x0000AE80
		public static TrustApr2004Dictionary TrustApr2004Dictionary
		{
			get
			{
				if (XD.trustApr2004Dictionary == null)
				{
					XD.trustApr2004Dictionary = new TrustApr2004Dictionary(XD.Dictionary);
				}
				return XD.trustApr2004Dictionary;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000250 RID: 592 RVA: 0x0000CC9D File Offset: 0x0000AE9D
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

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000251 RID: 593 RVA: 0x0000CCBA File Offset: 0x0000AEBA
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

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000252 RID: 594 RVA: 0x0000CCD7 File Offset: 0x0000AED7
		public static WsrmFeb2005Dictionary WsrmFeb2005Dictionary
		{
			get
			{
				if (XD.wsrmFeb2005Dictionary == null)
				{
					XD.wsrmFeb2005Dictionary = new WsrmFeb2005Dictionary(XD.Dictionary);
				}
				return XD.wsrmFeb2005Dictionary;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000253 RID: 595 RVA: 0x0000CCF4 File Offset: 0x0000AEF4
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

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000254 RID: 596 RVA: 0x0000CD11 File Offset: 0x0000AF11
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

		// Token: 0x040004B0 RID: 1200
		private static ActivityIdFlowDictionary activityIdFlowDictionary;

		// Token: 0x040004B1 RID: 1201
		private static AddressingDictionary addressingDictionary;

		// Token: 0x040004B2 RID: 1202
		private static Addressing10Dictionary addressing10Dictionary;

		// Token: 0x040004B3 RID: 1203
		private static Addressing200408Dictionary addressing200408Dictionary;

		// Token: 0x040004B4 RID: 1204
		private static AddressingNoneDictionary addressingNoneDictionary;

		// Token: 0x040004B5 RID: 1205
		private static AtomicTransactionExternalDictionary atomicTransactionExternalDictionary;

		// Token: 0x040004B6 RID: 1206
		private static AtomicTransactionExternal10Dictionary atomicTransactionExternal10Dictionary;

		// Token: 0x040004B7 RID: 1207
		private static CoordinationExternalDictionary coordinationExternalDictionary;

		// Token: 0x040004B8 RID: 1208
		private static CoordinationExternal10Dictionary coordinationExternal10Dictionary;

		// Token: 0x040004B9 RID: 1209
		private static DotNetAddressingDictionary dotNetAddressingDictionary;

		// Token: 0x040004BA RID: 1210
		private static DotNetAtomicTransactionExternalDictionary dotNetAtomicTransactionExternalDictionary;

		// Token: 0x040004BB RID: 1211
		private static DotNetOneWayDictionary dotNetOneWayDictionary;

		// Token: 0x040004BC RID: 1212
		private static DotNetSecurityDictionary dotNetSecurityDictionary;

		// Token: 0x040004BD RID: 1213
		private static ExclusiveC14NDictionary exclusiveC14NDictionary;

		// Token: 0x040004BE RID: 1214
		private static MessageDictionary messageDictionary;

		// Token: 0x040004BF RID: 1215
		private static Message11Dictionary message11Dictionary;

		// Token: 0x040004C0 RID: 1216
		private static Message12Dictionary message12Dictionary;

		// Token: 0x040004C1 RID: 1217
		private static OleTxTransactionExternalDictionary oleTxTransactionExternalDictionary;

		// Token: 0x040004C2 RID: 1218
		private static PeerWireStringsDictionary peerWireStringsDictionary;

		// Token: 0x040004C3 RID: 1219
		private static PolicyDictionary policyDictionary;

		// Token: 0x040004C4 RID: 1220
		private static SamlDictionary samlDictionary;

		// Token: 0x040004C5 RID: 1221
		private static SecureConversationApr2004Dictionary secureConversationApr2004Dictionary;

		// Token: 0x040004C6 RID: 1222
		private static SecureConversationFeb2005Dictionary secureConversationFeb2005Dictionary;

		// Token: 0x040004C7 RID: 1223
		private static SecurityAlgorithmDictionary securityAlgorithmDictionary;

		// Token: 0x040004C8 RID: 1224
		private static SecurityJan2004Dictionary securityJan2004Dictionary;

		// Token: 0x040004C9 RID: 1225
		private static SecurityXXX2005Dictionary securityXXX2005Dictionary;

		// Token: 0x040004CA RID: 1226
		private static SerializationDictionary serializationDictionary;

		// Token: 0x040004CB RID: 1227
		private static TrustApr2004Dictionary trustApr2004Dictionary;

		// Token: 0x040004CC RID: 1228
		private static TrustFeb2005Dictionary trustFeb2005Dictionary;

		// Token: 0x040004CD RID: 1229
		private static UtilityDictionary utilityDictionary;

		// Token: 0x040004CE RID: 1230
		private static WsrmFeb2005Dictionary wsrmFeb2005Dictionary;

		// Token: 0x040004CF RID: 1231
		private static XmlEncryptionDictionary xmlEncryptionDictionary;

		// Token: 0x040004D0 RID: 1232
		private static XmlSignatureDictionary xmlSignatureDictionary;
	}
}
