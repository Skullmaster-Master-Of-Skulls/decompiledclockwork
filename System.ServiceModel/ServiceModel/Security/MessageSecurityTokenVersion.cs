using System;
using System.Collections.ObjectModel;
using System.IdentityModel.Selectors;

namespace System.ServiceModel.Security
{
	// Token: 0x02000285 RID: 645
	internal sealed class MessageSecurityTokenVersion : SecurityTokenVersion
	{
		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06001295 RID: 4757 RVA: 0x00043E4B File Offset: 0x0004204B
		public static MessageSecurityTokenVersion WSSecurity11WSTrustFebruary2005WSSecureConversationFebruary2005
		{
			get
			{
				return MessageSecurityTokenVersion.wss11;
			}
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06001296 RID: 4758 RVA: 0x00043E52 File Offset: 0x00042052
		public static MessageSecurityTokenVersion WSSecurity11WSTrustFebruary2005WSSecureConversationFebruary2005BasicSecurityProfile10
		{
			get
			{
				return MessageSecurityTokenVersion.wss11bsp10;
			}
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06001297 RID: 4759 RVA: 0x00043E59 File Offset: 0x00042059
		public static MessageSecurityTokenVersion WSSecurity10WSTrustFebruary2005WSSecureConversationFebruary2005BasicSecurityProfile10
		{
			get
			{
				return MessageSecurityTokenVersion.wss10bsp10;
			}
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06001298 RID: 4760 RVA: 0x00043E60 File Offset: 0x00042060
		public static MessageSecurityTokenVersion WSSecurity10WSTrust13WSSecureConversation13BasicSecurityProfile10
		{
			get
			{
				return MessageSecurityTokenVersion.wss10oasisdec2005bsp10;
			}
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06001299 RID: 4761 RVA: 0x00043E67 File Offset: 0x00042067
		public static MessageSecurityTokenVersion WSSecurity11WSTrust13WSSecureConversation13
		{
			get
			{
				return MessageSecurityTokenVersion.wss11oasisdec2005;
			}
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x0600129A RID: 4762 RVA: 0x00043E6E File Offset: 0x0004206E
		public static MessageSecurityTokenVersion WSSecurity11WSTrust13WSSecureConversation13BasicSecurityProfile10
		{
			get
			{
				return MessageSecurityTokenVersion.wss11oasisdec2005bsp10;
			}
		}

		// Token: 0x0600129B RID: 4763 RVA: 0x00043E78 File Offset: 0x00042078
		public static MessageSecurityTokenVersion GetSecurityTokenVersion(SecurityVersion version, bool emitBspAttributes)
		{
			if (version == SecurityVersion.WSSecurity10)
			{
				if (emitBspAttributes)
				{
					return MessageSecurityTokenVersion.WSSecurity10WSTrustFebruary2005WSSecureConversationFebruary2005BasicSecurityProfile10;
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
			}
			else
			{
				if (version != SecurityVersion.WSSecurity11)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
				}
				if (emitBspAttributes)
				{
					return MessageSecurityTokenVersion.WSSecurity11WSTrustFebruary2005WSSecureConversationFebruary2005BasicSecurityProfile10;
				}
				return MessageSecurityTokenVersion.WSSecurity11WSTrustFebruary2005WSSecureConversationFebruary2005;
			}
		}

		// Token: 0x0600129C RID: 4764 RVA: 0x00043ECC File Offset: 0x000420CC
		private MessageSecurityTokenVersion(SecurityVersion securityVersion, TrustVersion trustVersion, SecureConversationVersion secureConversationVersion, string toString, bool emitBspRequiredAttributes, params string[] supportedSpecs)
		{
			this.emitBspRequiredAttributes = emitBspRequiredAttributes;
			this.supportedSpecs = new ReadOnlyCollection<string>(supportedSpecs);
			this.toString = toString;
			this.securityVersion = securityVersion;
			this.trustVersion = trustVersion;
			this.secureConversationVersion = secureConversationVersion;
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x0600129D RID: 4765 RVA: 0x00043F06 File Offset: 0x00042106
		public bool EmitBspRequiredAttributes
		{
			get
			{
				return this.emitBspRequiredAttributes;
			}
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x0600129E RID: 4766 RVA: 0x00043F0E File Offset: 0x0004210E
		public SecurityVersion SecurityVersion
		{
			get
			{
				return this.securityVersion;
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x0600129F RID: 4767 RVA: 0x00043F16 File Offset: 0x00042116
		public TrustVersion TrustVersion
		{
			get
			{
				return this.trustVersion;
			}
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x060012A0 RID: 4768 RVA: 0x00043F1E File Offset: 0x0004211E
		public SecureConversationVersion SecureConversationVersion
		{
			get
			{
				return this.secureConversationVersion;
			}
		}

		// Token: 0x060012A1 RID: 4769 RVA: 0x00043F26 File Offset: 0x00042126
		public override ReadOnlyCollection<string> GetSecuritySpecifications()
		{
			return this.supportedSpecs;
		}

		// Token: 0x060012A2 RID: 4770 RVA: 0x00043F2E File Offset: 0x0004212E
		public override string ToString()
		{
			return this.toString;
		}

		// Token: 0x040019F9 RID: 6649
		private SecurityVersion securityVersion;

		// Token: 0x040019FA RID: 6650
		private TrustVersion trustVersion;

		// Token: 0x040019FB RID: 6651
		private SecureConversationVersion secureConversationVersion;

		// Token: 0x040019FC RID: 6652
		private bool emitBspRequiredAttributes;

		// Token: 0x040019FD RID: 6653
		private string toString;

		// Token: 0x040019FE RID: 6654
		private ReadOnlyCollection<string> supportedSpecs;

		// Token: 0x040019FF RID: 6655
		private const string bsp10ns = "http://ws-i.org/profiles/basic-security/core/1.0";

		// Token: 0x04001A00 RID: 6656
		private static MessageSecurityTokenVersion wss11 = new MessageSecurityTokenVersion(SecurityVersion.WSSecurity11, TrustVersion.WSTrustFeb2005, SecureConversationVersion.WSSecureConversationFeb2005, "WSSecurity11WSTrustFebruary2005WSSecureConversationFebruary2005", false, new string[]
		{
			XD.SecurityXXX2005Dictionary.Namespace.Value,
			XD.TrustFeb2005Dictionary.Namespace.Value,
			XD.SecureConversationFeb2005Dictionary.Namespace.Value
		});

		// Token: 0x04001A01 RID: 6657
		private static MessageSecurityTokenVersion wss10bsp10 = new MessageSecurityTokenVersion(SecurityVersion.WSSecurity10, TrustVersion.WSTrustFeb2005, SecureConversationVersion.WSSecureConversationFeb2005, "WSSecurity10WSTrustFebruary2005WSSecureConversationFebruary2005BasicSecurityProfile10", true, new string[]
		{
			XD.SecurityJan2004Dictionary.Namespace.Value,
			XD.TrustFeb2005Dictionary.Namespace.Value,
			XD.SecureConversationFeb2005Dictionary.Namespace.Value,
			"http://ws-i.org/profiles/basic-security/core/1.0"
		});

		// Token: 0x04001A02 RID: 6658
		private static MessageSecurityTokenVersion wss11bsp10 = new MessageSecurityTokenVersion(SecurityVersion.WSSecurity11, TrustVersion.WSTrustFeb2005, SecureConversationVersion.WSSecureConversationFeb2005, "WSSecurity11WSTrustFebruary2005WSSecureConversationFebruary2005BasicSecurityProfile10", true, new string[]
		{
			XD.SecurityXXX2005Dictionary.Namespace.Value,
			XD.TrustFeb2005Dictionary.Namespace.Value,
			XD.SecureConversationFeb2005Dictionary.Namespace.Value,
			"http://ws-i.org/profiles/basic-security/core/1.0"
		});

		// Token: 0x04001A03 RID: 6659
		private static MessageSecurityTokenVersion wss10oasisdec2005bsp10 = new MessageSecurityTokenVersion(SecurityVersion.WSSecurity10, TrustVersion.WSTrust13, SecureConversationVersion.WSSecureConversation13, "WSSecurity10WSTrust13WSSecureConversation13BasicSecurityProfile10", true, new string[]
		{
			XD.SecurityXXX2005Dictionary.Namespace.Value,
			DXD.TrustDec2005Dictionary.Namespace.Value,
			DXD.SecureConversationDec2005Dictionary.Namespace.Value
		});

		// Token: 0x04001A04 RID: 6660
		private static MessageSecurityTokenVersion wss11oasisdec2005 = new MessageSecurityTokenVersion(SecurityVersion.WSSecurity11, TrustVersion.WSTrust13, SecureConversationVersion.WSSecureConversation13, "WSSecurity11WSTrust13WSSecureConversation13", false, new string[]
		{
			XD.SecurityJan2004Dictionary.Namespace.Value,
			DXD.TrustDec2005Dictionary.Namespace.Value,
			DXD.SecureConversationDec2005Dictionary.Namespace.Value
		});

		// Token: 0x04001A05 RID: 6661
		private static MessageSecurityTokenVersion wss11oasisdec2005bsp10 = new MessageSecurityTokenVersion(SecurityVersion.WSSecurity11, TrustVersion.WSTrust13, SecureConversationVersion.WSSecureConversation13, "WSSecurity11WSTrust13WSSecureConversation13BasicSecurityProfile10", true, new string[]
		{
			XD.SecurityXXX2005Dictionary.Namespace.Value,
			DXD.TrustDec2005Dictionary.Namespace.Value,
			DXD.SecureConversationDec2005Dictionary.Namespace.Value
		});
	}
}
