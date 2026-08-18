using System;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x02000281 RID: 641
	internal abstract class SecureConversationDriver
	{
		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06001258 RID: 4696 RVA: 0x000438CB File Offset: 0x00041ACB
		public virtual XmlDictionaryString CloseAction
		{
			get
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SecureConversationDriverVersionDoesNotSupportSession")));
			}
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06001259 RID: 4697 RVA: 0x000438E6 File Offset: 0x00041AE6
		public virtual XmlDictionaryString CloseResponseAction
		{
			get
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SecureConversationDriverVersionDoesNotSupportSession")));
			}
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x0600125A RID: 4698 RVA: 0x00043901 File Offset: 0x00041B01
		public virtual bool IsSessionSupported
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x0600125B RID: 4699
		public abstract XmlDictionaryString IssueAction { get; }

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x0600125C RID: 4700
		public abstract XmlDictionaryString IssueResponseAction { get; }

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x0600125D RID: 4701 RVA: 0x00043904 File Offset: 0x00041B04
		public virtual XmlDictionaryString RenewAction
		{
			get
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SecureConversationDriverVersionDoesNotSupportSession")));
			}
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x0600125E RID: 4702 RVA: 0x0004391F File Offset: 0x00041B1F
		public virtual XmlDictionaryString RenewResponseAction
		{
			get
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SecureConversationDriverVersionDoesNotSupportSession")));
			}
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x0600125F RID: 4703
		public abstract XmlDictionaryString Namespace { get; }

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06001260 RID: 4704
		public abstract XmlDictionaryString RenewNeededFaultCode { get; }

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06001261 RID: 4705
		public abstract XmlDictionaryString BadContextTokenFaultCode { get; }

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06001262 RID: 4706
		public abstract string TokenTypeUri { get; }

		// Token: 0x06001263 RID: 4707
		public abstract UniqueId GetSecurityContextTokenId(XmlDictionaryReader reader);

		// Token: 0x06001264 RID: 4708
		public abstract bool IsAtSecurityContextToken(XmlDictionaryReader reader);
	}
}
