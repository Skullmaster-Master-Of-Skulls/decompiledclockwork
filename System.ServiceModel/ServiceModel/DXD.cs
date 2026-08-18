using System;
using System.Xml;

namespace System.ServiceModel
{
	// Token: 0x0200003D RID: 61
	internal static class DXD
	{
		// Token: 0x060001EF RID: 495 RVA: 0x000096C4 File Offset: 0x000078C4
		static DXD()
		{
			XmlDictionary dictionary = new XmlDictionary(137);
			DXD.atomicTransactionExternal11Dictionary = new AtomicTransactionExternal11Dictionary(dictionary);
			DXD.coordinationExternal11Dictionary = new CoordinationExternal11Dictionary(dictionary);
			DXD.secureConversationDec2005Dictionary = new SecureConversationDec2005Dictionary(dictionary);
			DXD.secureConversationDec2005Dictionary.PopulateSecureConversationDec2005();
			DXD.securityAlgorithmDec2005Dictionary = new SecurityAlgorithmDec2005Dictionary(dictionary);
			DXD.securityAlgorithmDec2005Dictionary.PopulateSecurityAlgorithmDictionaryString();
			DXD.trustDec2005Dictionary = new TrustDec2005Dictionary(dictionary);
			DXD.trustDec2005Dictionary.PopulateDec2005DictionaryStrings();
			DXD.trustDec2005Dictionary.PopulateFeb2005DictionaryString();
			DXD.wsrm11Dictionary = new Wsrm11Dictionary(dictionary);
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x00009746 File Offset: 0x00007946
		public static AtomicTransactionExternal11Dictionary AtomicTransactionExternal11Dictionary
		{
			get
			{
				return DXD.atomicTransactionExternal11Dictionary;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x0000974D File Offset: 0x0000794D
		public static CoordinationExternal11Dictionary CoordinationExternal11Dictionary
		{
			get
			{
				return DXD.coordinationExternal11Dictionary;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x00009754 File Offset: 0x00007954
		public static SecureConversationDec2005Dictionary SecureConversationDec2005Dictionary
		{
			get
			{
				return DXD.secureConversationDec2005Dictionary;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x0000975B File Offset: 0x0000795B
		public static SecurityAlgorithmDec2005Dictionary SecurityAlgorithmDec2005Dictionary
		{
			get
			{
				return DXD.securityAlgorithmDec2005Dictionary;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x00009762 File Offset: 0x00007962
		public static TrustDec2005Dictionary TrustDec2005Dictionary
		{
			get
			{
				return DXD.trustDec2005Dictionary;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x00009769 File Offset: 0x00007969
		public static Wsrm11Dictionary Wsrm11Dictionary
		{
			get
			{
				return DXD.wsrm11Dictionary;
			}
		}

		// Token: 0x040001BB RID: 443
		private static AtomicTransactionExternal11Dictionary atomicTransactionExternal11Dictionary;

		// Token: 0x040001BC RID: 444
		private static CoordinationExternal11Dictionary coordinationExternal11Dictionary;

		// Token: 0x040001BD RID: 445
		private static SecureConversationDec2005Dictionary secureConversationDec2005Dictionary;

		// Token: 0x040001BE RID: 446
		private static SecurityAlgorithmDec2005Dictionary securityAlgorithmDec2005Dictionary;

		// Token: 0x040001BF RID: 447
		private static TrustDec2005Dictionary trustDec2005Dictionary;

		// Token: 0x040001C0 RID: 448
		private static Wsrm11Dictionary wsrm11Dictionary;
	}
}
