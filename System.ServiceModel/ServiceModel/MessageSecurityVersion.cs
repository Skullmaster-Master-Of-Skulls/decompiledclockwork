using System;
using System.IdentityModel.Selectors;
using System.ServiceModel.Security;

namespace System.ServiceModel
{
	// Token: 0x020000C0 RID: 192
	[__DynamicallyInvokable]
	public abstract class MessageSecurityVersion
	{
		// Token: 0x170000BE RID: 190
		// (get) Token: 0x0600034E RID: 846 RVA: 0x000132E3 File Offset: 0x000114E3
		[__DynamicallyInvokable]
		public static MessageSecurityVersion WSSecurity11WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11
		{
			[__DynamicallyInvokable]
			get
			{
				return MessageSecurityVersion.WSSecurity11WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11MessageSecurityVersion.Instance;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x0600034F RID: 847 RVA: 0x000132EA File Offset: 0x000114EA
		[__DynamicallyInvokable]
		public static MessageSecurityVersion WSSecurity10WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11BasicSecurityProfile10
		{
			[__DynamicallyInvokable]
			get
			{
				return MessageSecurityVersion.WSSecurity10WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11BasicSecurityProfile10MessageSecurityVersion.Instance;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000350 RID: 848 RVA: 0x000132F1 File Offset: 0x000114F1
		[__DynamicallyInvokable]
		public static MessageSecurityVersion WSSecurity11WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11BasicSecurityProfile10
		{
			[__DynamicallyInvokable]
			get
			{
				return MessageSecurityVersion.WSSecurity11WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11BasicSecurityProfile10MessageSecurityVersion.Instance;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000351 RID: 849 RVA: 0x000132F8 File Offset: 0x000114F8
		public static MessageSecurityVersion WSSecurity11WSTrust13WSSecureConversation13WSSecurityPolicy12
		{
			get
			{
				return MessageSecurityVersion.WSSecurity11WSTrust13WSSecureConversation13WSSecurityPolicy12MessageSecurityVersion.Instance;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000352 RID: 850 RVA: 0x000132FF File Offset: 0x000114FF
		public static MessageSecurityVersion WSSecurity10WSTrust13WSSecureConversation13WSSecurityPolicy12BasicSecurityProfile10
		{
			get
			{
				return MessageSecurityVersion.WSSecurity10WSTrust13WSSecureConversation13WSSecurityPolicy12BasicSecurityProfile10MessageSecurityVersion.Instance;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000353 RID: 851 RVA: 0x00013306 File Offset: 0x00011506
		public static MessageSecurityVersion WSSecurity11WSTrust13WSSecureConversation13WSSecurityPolicy12BasicSecurityProfile10
		{
			get
			{
				return MessageSecurityVersion.WSSecurity11WSTrust13WSSecureConversation13WSSecurityPolicy12BasicSecurityProfile10MessageSecurityVersion.Instance;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000354 RID: 852 RVA: 0x0001330D File Offset: 0x0001150D
		public static MessageSecurityVersion Default
		{
			get
			{
				return MessageSecurityVersion.WSSecurity11WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11MessageSecurityVersion.Instance;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000355 RID: 853 RVA: 0x00013314 File Offset: 0x00011514
		internal static MessageSecurityVersion WSSXDefault
		{
			get
			{
				return MessageSecurityVersion.WSSecurity11WSTrust13WSSecureConversation13WSSecurityPolicy12MessageSecurityVersion.Instance;
			}
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0001331B File Offset: 0x0001151B
		internal MessageSecurityVersion()
		{
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000357 RID: 855 RVA: 0x00013323 File Offset: 0x00011523
		[__DynamicallyInvokable]
		public SecurityVersion SecurityVersion
		{
			[__DynamicallyInvokable]
			get
			{
				return this.MessageSecurityTokenVersion.SecurityVersion;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000358 RID: 856 RVA: 0x00013330 File Offset: 0x00011530
		[__DynamicallyInvokable]
		public TrustVersion TrustVersion
		{
			[__DynamicallyInvokable]
			get
			{
				return this.MessageSecurityTokenVersion.TrustVersion;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000359 RID: 857 RVA: 0x0001333D File Offset: 0x0001153D
		[__DynamicallyInvokable]
		public SecureConversationVersion SecureConversationVersion
		{
			[__DynamicallyInvokable]
			get
			{
				return this.MessageSecurityTokenVersion.SecureConversationVersion;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x0600035A RID: 858 RVA: 0x0001334A File Offset: 0x0001154A
		public SecurityTokenVersion SecurityTokenVersion
		{
			get
			{
				return this.MessageSecurityTokenVersion;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x0600035B RID: 859
		[__DynamicallyInvokable]
		public abstract SecurityPolicyVersion SecurityPolicyVersion { [__DynamicallyInvokable] get; }

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x0600035C RID: 860
		[__DynamicallyInvokable]
		public abstract BasicSecurityProfileVersion BasicSecurityProfileVersion { [__DynamicallyInvokable] get; }

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600035D RID: 861
		internal abstract MessageSecurityTokenVersion MessageSecurityTokenVersion { get; }

		// Token: 0x02000ACE RID: 2766
		private class WSSecurity11WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11MessageSecurityVersion : MessageSecurityVersion
		{
			// Token: 0x170019B9 RID: 6585
			// (get) Token: 0x06006E48 RID: 28232 RVA: 0x0019BA7F File Offset: 0x00199C7F
			public static MessageSecurityVersion Instance
			{
				get
				{
					return MessageSecurityVersion.WSSecurity11WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11MessageSecurityVersion.instance;
				}
			}

			// Token: 0x170019BA RID: 6586
			// (get) Token: 0x06006E49 RID: 28233 RVA: 0x0019BA86 File Offset: 0x00199C86
			public override BasicSecurityProfileVersion BasicSecurityProfileVersion
			{
				get
				{
					return null;
				}
			}

			// Token: 0x170019BB RID: 6587
			// (get) Token: 0x06006E4A RID: 28234 RVA: 0x0019BA89 File Offset: 0x00199C89
			internal override MessageSecurityTokenVersion MessageSecurityTokenVersion
			{
				get
				{
					return MessageSecurityTokenVersion.WSSecurity11WSTrustFebruary2005WSSecureConversationFebruary2005;
				}
			}

			// Token: 0x170019BC RID: 6588
			// (get) Token: 0x06006E4B RID: 28235 RVA: 0x0019BA90 File Offset: 0x00199C90
			public override SecurityPolicyVersion SecurityPolicyVersion
			{
				get
				{
					return SecurityPolicyVersion.WSSecurityPolicy11;
				}
			}

			// Token: 0x06006E4C RID: 28236 RVA: 0x0019BA97 File Offset: 0x00199C97
			public override string ToString()
			{
				return "WSSecurity11WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11";
			}

			// Token: 0x04003F0A RID: 16138
			private static MessageSecurityVersion instance = new MessageSecurityVersion.WSSecurity11WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11MessageSecurityVersion();
		}

		// Token: 0x02000ACF RID: 2767
		private class WSSecurity10WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11BasicSecurityProfile10MessageSecurityVersion : MessageSecurityVersion
		{
			// Token: 0x170019BD RID: 6589
			// (get) Token: 0x06006E4F RID: 28239 RVA: 0x0019BAB2 File Offset: 0x00199CB2
			public static MessageSecurityVersion Instance
			{
				get
				{
					return MessageSecurityVersion.WSSecurity10WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11BasicSecurityProfile10MessageSecurityVersion.instance;
				}
			}

			// Token: 0x170019BE RID: 6590
			// (get) Token: 0x06006E50 RID: 28240 RVA: 0x0019BAB9 File Offset: 0x00199CB9
			public override BasicSecurityProfileVersion BasicSecurityProfileVersion
			{
				get
				{
					return BasicSecurityProfileVersion.BasicSecurityProfile10;
				}
			}

			// Token: 0x170019BF RID: 6591
			// (get) Token: 0x06006E51 RID: 28241 RVA: 0x0019BAC0 File Offset: 0x00199CC0
			internal override MessageSecurityTokenVersion MessageSecurityTokenVersion
			{
				get
				{
					return MessageSecurityTokenVersion.WSSecurity10WSTrustFebruary2005WSSecureConversationFebruary2005BasicSecurityProfile10;
				}
			}

			// Token: 0x170019C0 RID: 6592
			// (get) Token: 0x06006E52 RID: 28242 RVA: 0x0019BAC7 File Offset: 0x00199CC7
			public override SecurityPolicyVersion SecurityPolicyVersion
			{
				get
				{
					return SecurityPolicyVersion.WSSecurityPolicy11;
				}
			}

			// Token: 0x06006E53 RID: 28243 RVA: 0x0019BACE File Offset: 0x00199CCE
			public override string ToString()
			{
				return "WSSecurity10WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11BasicSecurityProfile10";
			}

			// Token: 0x04003F0B RID: 16139
			private static MessageSecurityVersion instance = new MessageSecurityVersion.WSSecurity10WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11BasicSecurityProfile10MessageSecurityVersion();
		}

		// Token: 0x02000AD0 RID: 2768
		private class WSSecurity11WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11BasicSecurityProfile10MessageSecurityVersion : MessageSecurityVersion
		{
			// Token: 0x170019C1 RID: 6593
			// (get) Token: 0x06006E56 RID: 28246 RVA: 0x0019BAE9 File Offset: 0x00199CE9
			public static MessageSecurityVersion Instance
			{
				get
				{
					return MessageSecurityVersion.WSSecurity11WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11BasicSecurityProfile10MessageSecurityVersion.instance;
				}
			}

			// Token: 0x170019C2 RID: 6594
			// (get) Token: 0x06006E57 RID: 28247 RVA: 0x0019BAF0 File Offset: 0x00199CF0
			public override SecurityPolicyVersion SecurityPolicyVersion
			{
				get
				{
					return SecurityPolicyVersion.WSSecurityPolicy11;
				}
			}

			// Token: 0x170019C3 RID: 6595
			// (get) Token: 0x06006E58 RID: 28248 RVA: 0x0019BAF7 File Offset: 0x00199CF7
			public override BasicSecurityProfileVersion BasicSecurityProfileVersion
			{
				get
				{
					return BasicSecurityProfileVersion.BasicSecurityProfile10;
				}
			}

			// Token: 0x170019C4 RID: 6596
			// (get) Token: 0x06006E59 RID: 28249 RVA: 0x0019BAFE File Offset: 0x00199CFE
			internal override MessageSecurityTokenVersion MessageSecurityTokenVersion
			{
				get
				{
					return MessageSecurityTokenVersion.WSSecurity11WSTrustFebruary2005WSSecureConversationFebruary2005BasicSecurityProfile10;
				}
			}

			// Token: 0x06006E5A RID: 28250 RVA: 0x0019BB05 File Offset: 0x00199D05
			public override string ToString()
			{
				return "WSSecurity11WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11BasicSecurityProfile10";
			}

			// Token: 0x04003F0C RID: 16140
			private static MessageSecurityVersion instance = new MessageSecurityVersion.WSSecurity11WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11BasicSecurityProfile10MessageSecurityVersion();
		}

		// Token: 0x02000AD1 RID: 2769
		private class WSSecurity10WSTrust13WSSecureConversation13WSSecurityPolicy12BasicSecurityProfile10MessageSecurityVersion : MessageSecurityVersion
		{
			// Token: 0x170019C5 RID: 6597
			// (get) Token: 0x06006E5D RID: 28253 RVA: 0x0019BB20 File Offset: 0x00199D20
			public static MessageSecurityVersion Instance
			{
				get
				{
					return MessageSecurityVersion.WSSecurity10WSTrust13WSSecureConversation13WSSecurityPolicy12BasicSecurityProfile10MessageSecurityVersion.instance;
				}
			}

			// Token: 0x170019C6 RID: 6598
			// (get) Token: 0x06006E5E RID: 28254 RVA: 0x0019BB27 File Offset: 0x00199D27
			public override SecurityPolicyVersion SecurityPolicyVersion
			{
				get
				{
					return SecurityPolicyVersion.WSSecurityPolicy12;
				}
			}

			// Token: 0x170019C7 RID: 6599
			// (get) Token: 0x06006E5F RID: 28255 RVA: 0x0019BB2E File Offset: 0x00199D2E
			public override BasicSecurityProfileVersion BasicSecurityProfileVersion
			{
				get
				{
					return null;
				}
			}

			// Token: 0x170019C8 RID: 6600
			// (get) Token: 0x06006E60 RID: 28256 RVA: 0x0019BB31 File Offset: 0x00199D31
			internal override MessageSecurityTokenVersion MessageSecurityTokenVersion
			{
				get
				{
					return MessageSecurityTokenVersion.WSSecurity10WSTrust13WSSecureConversation13BasicSecurityProfile10;
				}
			}

			// Token: 0x06006E61 RID: 28257 RVA: 0x0019BB38 File Offset: 0x00199D38
			public override string ToString()
			{
				return "WSSecurity10WSTrust13WSSecureConversation13WSSecurityPolicy12BasicSecurityProfile10";
			}

			// Token: 0x04003F0D RID: 16141
			private static MessageSecurityVersion instance = new MessageSecurityVersion.WSSecurity10WSTrust13WSSecureConversation13WSSecurityPolicy12BasicSecurityProfile10MessageSecurityVersion();
		}

		// Token: 0x02000AD2 RID: 2770
		private class WSSecurity11WSTrust13WSSecureConversation13WSSecurityPolicy12MessageSecurityVersion : MessageSecurityVersion
		{
			// Token: 0x170019C9 RID: 6601
			// (get) Token: 0x06006E64 RID: 28260 RVA: 0x0019BB53 File Offset: 0x00199D53
			public static MessageSecurityVersion Instance
			{
				get
				{
					return MessageSecurityVersion.WSSecurity11WSTrust13WSSecureConversation13WSSecurityPolicy12MessageSecurityVersion.instance;
				}
			}

			// Token: 0x170019CA RID: 6602
			// (get) Token: 0x06006E65 RID: 28261 RVA: 0x0019BB5A File Offset: 0x00199D5A
			public override SecurityPolicyVersion SecurityPolicyVersion
			{
				get
				{
					return SecurityPolicyVersion.WSSecurityPolicy12;
				}
			}

			// Token: 0x170019CB RID: 6603
			// (get) Token: 0x06006E66 RID: 28262 RVA: 0x0019BB61 File Offset: 0x00199D61
			public override BasicSecurityProfileVersion BasicSecurityProfileVersion
			{
				get
				{
					return null;
				}
			}

			// Token: 0x170019CC RID: 6604
			// (get) Token: 0x06006E67 RID: 28263 RVA: 0x0019BB64 File Offset: 0x00199D64
			internal override MessageSecurityTokenVersion MessageSecurityTokenVersion
			{
				get
				{
					return MessageSecurityTokenVersion.WSSecurity11WSTrust13WSSecureConversation13;
				}
			}

			// Token: 0x06006E68 RID: 28264 RVA: 0x0019BB6B File Offset: 0x00199D6B
			public override string ToString()
			{
				return "WSSecurity11WSTrust13WSSecureConversation13WSSecurityPolicy12";
			}

			// Token: 0x04003F0E RID: 16142
			private static MessageSecurityVersion instance = new MessageSecurityVersion.WSSecurity11WSTrust13WSSecureConversation13WSSecurityPolicy12MessageSecurityVersion();
		}

		// Token: 0x02000AD3 RID: 2771
		private class WSSecurity11WSTrust13WSSecureConversation13WSSecurityPolicy12BasicSecurityProfile10MessageSecurityVersion : MessageSecurityVersion
		{
			// Token: 0x170019CD RID: 6605
			// (get) Token: 0x06006E6B RID: 28267 RVA: 0x0019BB86 File Offset: 0x00199D86
			public static MessageSecurityVersion Instance
			{
				get
				{
					return MessageSecurityVersion.WSSecurity11WSTrust13WSSecureConversation13WSSecurityPolicy12BasicSecurityProfile10MessageSecurityVersion.instance;
				}
			}

			// Token: 0x170019CE RID: 6606
			// (get) Token: 0x06006E6C RID: 28268 RVA: 0x0019BB8D File Offset: 0x00199D8D
			public override SecurityPolicyVersion SecurityPolicyVersion
			{
				get
				{
					return SecurityPolicyVersion.WSSecurityPolicy12;
				}
			}

			// Token: 0x170019CF RID: 6607
			// (get) Token: 0x06006E6D RID: 28269 RVA: 0x0019BB94 File Offset: 0x00199D94
			public override BasicSecurityProfileVersion BasicSecurityProfileVersion
			{
				get
				{
					return null;
				}
			}

			// Token: 0x170019D0 RID: 6608
			// (get) Token: 0x06006E6E RID: 28270 RVA: 0x0019BB97 File Offset: 0x00199D97
			internal override MessageSecurityTokenVersion MessageSecurityTokenVersion
			{
				get
				{
					return MessageSecurityTokenVersion.WSSecurity11WSTrust13WSSecureConversation13BasicSecurityProfile10;
				}
			}

			// Token: 0x06006E6F RID: 28271 RVA: 0x0019BB9E File Offset: 0x00199D9E
			public override string ToString()
			{
				return "WSSecurity11WSTrust13WSSecureConversation13WSSecurityPolicy12BasicSecurityProfile10";
			}

			// Token: 0x04003F0F RID: 16143
			private static MessageSecurityVersion instance = new MessageSecurityVersion.WSSecurity11WSTrust13WSSecureConversation13WSSecurityPolicy12BasicSecurityProfile10MessageSecurityVersion();
		}
	}
}
