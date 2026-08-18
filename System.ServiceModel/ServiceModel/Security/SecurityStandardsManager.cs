using System;
using System.Collections.Generic;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Runtime.CompilerServices;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Security.Tokens;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x02000282 RID: 642
	internal class SecurityStandardsManager
	{
		// Token: 0x06001266 RID: 4710 RVA: 0x00043942 File Offset: 0x00041B42
		[MethodImpl(MethodImplOptions.NoInlining)]
		public SecurityStandardsManager() : this(WSSecurityTokenSerializer.DefaultInstance)
		{
		}

		// Token: 0x06001267 RID: 4711 RVA: 0x0004394F File Offset: 0x00041B4F
		public SecurityStandardsManager(SecurityTokenSerializer tokenSerializer) : this(MessageSecurityVersion.WSSecurity11WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11, tokenSerializer)
		{
		}

		// Token: 0x06001268 RID: 4712 RVA: 0x00043960 File Offset: 0x00041B60
		public SecurityStandardsManager(MessageSecurityVersion messageSecurityVersion, SecurityTokenSerializer tokenSerializer)
		{
			if (messageSecurityVersion == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("messageSecurityVersion"));
			}
			if (tokenSerializer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("tokenSerializer");
			}
			this.messageSecurityVersion = messageSecurityVersion;
			this.tokenSerializer = tokenSerializer;
			if (messageSecurityVersion.SecureConversationVersion == SecureConversationVersion.WSSecureConversation13)
			{
				this.secureConversationDriver = new WSSecureConversationDec2005.DriverDec2005();
			}
			else
			{
				this.secureConversationDriver = new WSSecureConversationFeb2005.DriverFeb2005();
			}
			if (this.SecurityVersion != SecurityVersion.WSSecurity10 && this.SecurityVersion != SecurityVersion.WSSecurity11)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("messageSecurityVersion", SR.GetString("MessageSecurityVersionOutOfRange")));
			}
			this.idManager = WSSecurityJan2004.IdManager.Instance;
			this.wsUtilitySpecificationVersion = WSUtilitySpecificationVersion.Default;
			if (messageSecurityVersion.MessageSecurityTokenVersion.TrustVersion == TrustVersion.WSTrust13)
			{
				this.trustDriver = new WSTrustDec2005.DriverDec2005(this);
				return;
			}
			this.trustDriver = new WSTrustFeb2005.DriverFeb2005(this);
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06001269 RID: 4713 RVA: 0x00043A4D File Offset: 0x00041C4D
		public static SecurityStandardsManager DefaultInstance
		{
			get
			{
				if (SecurityStandardsManager.instance == null)
				{
					SecurityStandardsManager.instance = new SecurityStandardsManager();
				}
				return SecurityStandardsManager.instance;
			}
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x0600126A RID: 4714 RVA: 0x00043A65 File Offset: 0x00041C65
		public SecurityVersion SecurityVersion
		{
			get
			{
				if (this.messageSecurityVersion != null)
				{
					return this.messageSecurityVersion.SecurityVersion;
				}
				return null;
			}
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x0600126B RID: 4715 RVA: 0x00043A7C File Offset: 0x00041C7C
		public MessageSecurityVersion MessageSecurityVersion
		{
			get
			{
				return this.messageSecurityVersion;
			}
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x0600126C RID: 4716 RVA: 0x00043A84 File Offset: 0x00041C84
		public TrustVersion TrustVersion
		{
			get
			{
				return this.messageSecurityVersion.TrustVersion;
			}
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x0600126D RID: 4717 RVA: 0x00043A91 File Offset: 0x00041C91
		public SecureConversationVersion SecureConversationVersion
		{
			get
			{
				return this.messageSecurityVersion.SecureConversationVersion;
			}
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x0600126E RID: 4718 RVA: 0x00043A9E File Offset: 0x00041C9E
		internal SecurityTokenSerializer SecurityTokenSerializer
		{
			get
			{
				return this.tokenSerializer;
			}
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x0600126F RID: 4719 RVA: 0x00043AA6 File Offset: 0x00041CA6
		internal WSUtilitySpecificationVersion WSUtilitySpecificationVersion
		{
			get
			{
				return this.wsUtilitySpecificationVersion;
			}
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06001270 RID: 4720 RVA: 0x00043AAE File Offset: 0x00041CAE
		internal SignatureTargetIdManager IdManager
		{
			get
			{
				return this.idManager;
			}
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06001271 RID: 4721 RVA: 0x00043AB6 File Offset: 0x00041CB6
		internal SecureConversationDriver SecureConversationDriver
		{
			get
			{
				return this.secureConversationDriver;
			}
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06001272 RID: 4722 RVA: 0x00043ABE File Offset: 0x00041CBE
		internal TrustDriver TrustDriver
		{
			get
			{
				return this.trustDriver;
			}
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x06001273 RID: 4723 RVA: 0x00043AC8 File Offset: 0x00041CC8
		private WSSecurityTokenSerializer WSSecurityTokenSerializer
		{
			get
			{
				if (this.wsSecurityTokenSerializer == null)
				{
					WSSecurityTokenSerializer wssecurityTokenSerializer = this.tokenSerializer as WSSecurityTokenSerializer;
					if (wssecurityTokenSerializer == null)
					{
						wssecurityTokenSerializer = new WSSecurityTokenSerializer(this.SecurityVersion);
					}
					this.wsSecurityTokenSerializer = wssecurityTokenSerializer;
				}
				return this.wsSecurityTokenSerializer;
			}
		}

		// Token: 0x06001274 RID: 4724 RVA: 0x00043B05 File Offset: 0x00041D05
		internal bool TryCreateKeyIdentifierClauseFromTokenXml(XmlElement element, SecurityTokenReferenceStyle tokenReferenceStyle, out SecurityKeyIdentifierClause securityKeyIdentifierClause)
		{
			return this.WSSecurityTokenSerializer.TryCreateKeyIdentifierClauseFromTokenXml(element, tokenReferenceStyle, out securityKeyIdentifierClause);
		}

		// Token: 0x06001275 RID: 4725 RVA: 0x00043B15 File Offset: 0x00041D15
		internal SecurityKeyIdentifierClause CreateKeyIdentifierClauseFromTokenXml(XmlElement element, SecurityTokenReferenceStyle tokenReferenceStyle)
		{
			return this.WSSecurityTokenSerializer.CreateKeyIdentifierClauseFromTokenXml(element, tokenReferenceStyle);
		}

		// Token: 0x06001276 RID: 4726 RVA: 0x00043B24 File Offset: 0x00041D24
		internal SendSecurityHeader CreateSendSecurityHeader(Message message, string actor, bool mustUnderstand, bool relay, SecurityAlgorithmSuite algorithmSuite, MessageDirection direction)
		{
			return this.SecurityVersion.CreateSendSecurityHeader(message, actor, mustUnderstand, relay, this, algorithmSuite, direction);
		}

		// Token: 0x06001277 RID: 4727 RVA: 0x00043B3C File Offset: 0x00041D3C
		internal ReceiveSecurityHeader CreateReceiveSecurityHeader(Message message, string actor, SecurityAlgorithmSuite algorithmSuite, MessageDirection direction)
		{
			ReceiveSecurityHeader receiveSecurityHeader = this.TryCreateReceiveSecurityHeader(message, actor, algorithmSuite, direction);
			if (receiveSecurityHeader != null)
			{
				return receiveSecurityHeader;
			}
			if (string.IsNullOrEmpty(actor))
			{
				throw TraceUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("UnableToFindSecurityHeaderInMessageNoActor")), message);
			}
			throw TraceUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("UnableToFindSecurityHeaderInMessage", new object[]
			{
				actor
			})), message);
		}

		// Token: 0x06001278 RID: 4728 RVA: 0x00043B97 File Offset: 0x00041D97
		internal ReceiveSecurityHeader TryCreateReceiveSecurityHeader(Message message, string actor, SecurityAlgorithmSuite algorithmSuite, MessageDirection direction)
		{
			return this.SecurityVersion.TryCreateReceiveSecurityHeader(message, actor, this, algorithmSuite, direction);
		}

		// Token: 0x06001279 RID: 4729 RVA: 0x00043BAA File Offset: 0x00041DAA
		internal bool DoesMessageContainSecurityHeader(Message message)
		{
			return this.SecurityVersion.DoesMessageContainSecurityHeader(message);
		}

		// Token: 0x0600127A RID: 4730 RVA: 0x00043BB8 File Offset: 0x00041DB8
		internal bool TryGetSecurityContextIds(Message message, string[] actors, bool isStrictMode, ICollection<UniqueId> results)
		{
			if (results == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("results");
			}
			SecureConversationDriver secureConversationDriver = this.SecureConversationDriver;
			int num = this.SecurityVersion.FindIndexOfSecurityHeader(message, actors);
			if (num < 0)
			{
				return false;
			}
			bool result = false;
			using (XmlDictionaryReader readerAtHeader = message.Headers.GetReaderAtHeader(num))
			{
				if (!readerAtHeader.IsStartElement())
				{
					return false;
				}
				if (readerAtHeader.IsEmptyElement)
				{
					return false;
				}
				readerAtHeader.ReadStartElement();
				while (readerAtHeader.IsStartElement())
				{
					if (secureConversationDriver.IsAtSecurityContextToken(readerAtHeader))
					{
						results.Add(secureConversationDriver.GetSecurityContextTokenId(readerAtHeader));
						result = true;
						if (isStrictMode)
						{
							break;
						}
					}
					else
					{
						readerAtHeader.Skip();
					}
				}
			}
			return result;
		}

		// Token: 0x040019EC RID: 6636
		private static SecurityStandardsManager instance;

		// Token: 0x040019ED RID: 6637
		private readonly SecureConversationDriver secureConversationDriver;

		// Token: 0x040019EE RID: 6638
		private readonly TrustDriver trustDriver;

		// Token: 0x040019EF RID: 6639
		private readonly SignatureTargetIdManager idManager;

		// Token: 0x040019F0 RID: 6640
		private readonly MessageSecurityVersion messageSecurityVersion;

		// Token: 0x040019F1 RID: 6641
		private readonly WSUtilitySpecificationVersion wsUtilitySpecificationVersion;

		// Token: 0x040019F2 RID: 6642
		private readonly SecurityTokenSerializer tokenSerializer;

		// Token: 0x040019F3 RID: 6643
		private WSSecurityTokenSerializer wsSecurityTokenSerializer;
	}
}
