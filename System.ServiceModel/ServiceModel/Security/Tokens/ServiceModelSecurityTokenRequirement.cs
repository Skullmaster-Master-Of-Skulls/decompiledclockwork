using System;
using System.Globalization;
using System.IdentityModel.Selectors;
using System.ServiceModel.Channels;
using System.Text;

namespace System.ServiceModel.Security.Tokens
{
	// Token: 0x0200038C RID: 908
	public abstract class ServiceModelSecurityTokenRequirement : SecurityTokenRequirement
	{
		// Token: 0x06002186 RID: 8582 RVA: 0x0007BB34 File Offset: 0x00079D34
		protected ServiceModelSecurityTokenRequirement()
		{
			base.Properties[ServiceModelSecurityTokenRequirement.SupportSecurityContextCancellationProperty] = false;
		}

		// Token: 0x17000815 RID: 2069
		// (get) Token: 0x06002187 RID: 8583 RVA: 0x0007BB52 File Offset: 0x00079D52
		public static string SecurityAlgorithmSuiteProperty
		{
			get
			{
				return "http://schemas.microsoft.com/ws/2006/05/servicemodel/securitytokenrequirement/SecurityAlgorithmSuite";
			}
		}

		// Token: 0x17000816 RID: 2070
		// (get) Token: 0x06002188 RID: 8584 RVA: 0x0007BB59 File Offset: 0x00079D59
		public static string SecurityBindingElementProperty
		{
			get
			{
				return "http://schemas.microsoft.com/ws/2006/05/servicemodel/securitytokenrequirement/SecurityBindingElement";
			}
		}

		// Token: 0x17000817 RID: 2071
		// (get) Token: 0x06002189 RID: 8585 RVA: 0x0007BB60 File Offset: 0x00079D60
		public static string IssuerAddressProperty
		{
			get
			{
				return "http://schemas.microsoft.com/ws/2006/05/servicemodel/securitytokenrequirement/IssuerAddress";
			}
		}

		// Token: 0x17000818 RID: 2072
		// (get) Token: 0x0600218A RID: 8586 RVA: 0x0007BB67 File Offset: 0x00079D67
		public static string IssuerBindingProperty
		{
			get
			{
				return "http://schemas.microsoft.com/ws/2006/05/servicemodel/securitytokenrequirement/IssuerBinding";
			}
		}

		// Token: 0x17000819 RID: 2073
		// (get) Token: 0x0600218B RID: 8587 RVA: 0x0007BB6E File Offset: 0x00079D6E
		public static string SecureConversationSecurityBindingElementProperty
		{
			get
			{
				return "http://schemas.microsoft.com/ws/2006/05/servicemodel/securitytokenrequirement/SecureConversationSecurityBindingElement";
			}
		}

		// Token: 0x1700081A RID: 2074
		// (get) Token: 0x0600218C RID: 8588 RVA: 0x0007BB75 File Offset: 0x00079D75
		public static string SupportSecurityContextCancellationProperty
		{
			get
			{
				return "http://schemas.microsoft.com/ws/2006/05/servicemodel/securitytokenrequirement/SupportSecurityContextCancellation";
			}
		}

		// Token: 0x1700081B RID: 2075
		// (get) Token: 0x0600218D RID: 8589 RVA: 0x0007BB7C File Offset: 0x00079D7C
		public static string MessageSecurityVersionProperty
		{
			get
			{
				return "http://schemas.microsoft.com/ws/2006/05/servicemodel/securitytokenrequirement/MessageSecurityVersion";
			}
		}

		// Token: 0x1700081C RID: 2076
		// (get) Token: 0x0600218E RID: 8590 RVA: 0x0007BB83 File Offset: 0x00079D83
		internal static string DefaultMessageSecurityVersionProperty
		{
			get
			{
				return "http://schemas.microsoft.com/ws/2006/05/servicemodel/securitytokenrequirement/DefaultMessageSecurityVersion";
			}
		}

		// Token: 0x1700081D RID: 2077
		// (get) Token: 0x0600218F RID: 8591 RVA: 0x0007BB8A File Offset: 0x00079D8A
		public static string IssuerBindingContextProperty
		{
			get
			{
				return "http://schemas.microsoft.com/ws/2006/05/servicemodel/securitytokenrequirement/IssuerBindingContext";
			}
		}

		// Token: 0x1700081E RID: 2078
		// (get) Token: 0x06002190 RID: 8592 RVA: 0x0007BB91 File Offset: 0x00079D91
		public static string TransportSchemeProperty
		{
			get
			{
				return "http://schemas.microsoft.com/ws/2006/05/servicemodel/securitytokenrequirement/TransportScheme";
			}
		}

		// Token: 0x1700081F RID: 2079
		// (get) Token: 0x06002191 RID: 8593 RVA: 0x0007BB98 File Offset: 0x00079D98
		public static string IsInitiatorProperty
		{
			get
			{
				return "http://schemas.microsoft.com/ws/2006/05/servicemodel/securitytokenrequirement/IsInitiator";
			}
		}

		// Token: 0x17000820 RID: 2080
		// (get) Token: 0x06002192 RID: 8594 RVA: 0x0007BB9F File Offset: 0x00079D9F
		public static string TargetAddressProperty
		{
			get
			{
				return "http://schemas.microsoft.com/ws/2006/05/servicemodel/securitytokenrequirement/TargetAddress";
			}
		}

		// Token: 0x17000821 RID: 2081
		// (get) Token: 0x06002193 RID: 8595 RVA: 0x0007BBA6 File Offset: 0x00079DA6
		public static string ViaProperty
		{
			get
			{
				return "http://schemas.microsoft.com/ws/2006/05/servicemodel/securitytokenrequirement/Via";
			}
		}

		// Token: 0x17000822 RID: 2082
		// (get) Token: 0x06002194 RID: 8596 RVA: 0x0007BBAD File Offset: 0x00079DAD
		public static string ListenUriProperty
		{
			get
			{
				return "http://schemas.microsoft.com/ws/2006/05/servicemodel/securitytokenrequirement/ListenUri";
			}
		}

		// Token: 0x17000823 RID: 2083
		// (get) Token: 0x06002195 RID: 8597 RVA: 0x0007BBB4 File Offset: 0x00079DB4
		public static string AuditLogLocationProperty
		{
			get
			{
				return "http://schemas.microsoft.com/ws/2006/05/servicemodel/securitytokenrequirement/AuditLogLocation";
			}
		}

		// Token: 0x17000824 RID: 2084
		// (get) Token: 0x06002196 RID: 8598 RVA: 0x0007BBBB File Offset: 0x00079DBB
		public static string SuppressAuditFailureProperty
		{
			get
			{
				return "http://schemas.microsoft.com/ws/2006/05/servicemodel/securitytokenrequirement/SuppressAuditFailure";
			}
		}

		// Token: 0x17000825 RID: 2085
		// (get) Token: 0x06002197 RID: 8599 RVA: 0x0007BBC2 File Offset: 0x00079DC2
		public static string MessageAuthenticationAuditLevelProperty
		{
			get
			{
				return "http://schemas.microsoft.com/ws/2006/05/servicemodel/securitytokenrequirement/MessageAuthenticationAuditLevel";
			}
		}

		// Token: 0x17000826 RID: 2086
		// (get) Token: 0x06002198 RID: 8600 RVA: 0x0007BBC9 File Offset: 0x00079DC9
		public static string IsOutOfBandTokenProperty
		{
			get
			{
				return "http://schemas.microsoft.com/ws/2006/05/servicemodel/securitytokenrequirement/IsOutOfBandToken";
			}
		}

		// Token: 0x17000827 RID: 2087
		// (get) Token: 0x06002199 RID: 8601 RVA: 0x0007BBD0 File Offset: 0x00079DD0
		public static string PreferSslCertificateAuthenticatorProperty
		{
			get
			{
				return "http://schemas.microsoft.com/ws/2006/05/servicemodel/securitytokenrequirement/PreferSslCertificateAuthenticator";
			}
		}

		// Token: 0x17000828 RID: 2088
		// (get) Token: 0x0600219A RID: 8602 RVA: 0x0007BBD7 File Offset: 0x00079DD7
		public static string SupportingTokenAttachmentModeProperty
		{
			get
			{
				return "http://schemas.microsoft.com/ws/2006/05/servicemodel/securitytokenrequirement/SupportingTokenAttachmentMode";
			}
		}

		// Token: 0x17000829 RID: 2089
		// (get) Token: 0x0600219B RID: 8603 RVA: 0x0007BBDE File Offset: 0x00079DDE
		public static string MessageDirectionProperty
		{
			get
			{
				return "http://schemas.microsoft.com/ws/2006/05/servicemodel/securitytokenrequirement/MessageDirection";
			}
		}

		// Token: 0x1700082A RID: 2090
		// (get) Token: 0x0600219C RID: 8604 RVA: 0x0007BBE5 File Offset: 0x00079DE5
		public static string HttpAuthenticationSchemeProperty
		{
			get
			{
				return "http://schemas.microsoft.com/ws/2006/05/servicemodel/securitytokenrequirement/HttpAuthenticationScheme";
			}
		}

		// Token: 0x1700082B RID: 2091
		// (get) Token: 0x0600219D RID: 8605 RVA: 0x0007BBEC File Offset: 0x00079DEC
		public static string IssuedSecurityTokenParametersProperty
		{
			get
			{
				return "http://schemas.microsoft.com/ws/2006/05/servicemodel/securitytokenrequirement/IssuedSecurityTokenParameters";
			}
		}

		// Token: 0x1700082C RID: 2092
		// (get) Token: 0x0600219E RID: 8606 RVA: 0x0007BBF3 File Offset: 0x00079DF3
		public static string PrivacyNoticeUriProperty
		{
			get
			{
				return "http://schemas.microsoft.com/ws/2006/05/servicemodel/securitytokenrequirement/PrivacyNoticeUri";
			}
		}

		// Token: 0x1700082D RID: 2093
		// (get) Token: 0x0600219F RID: 8607 RVA: 0x0007BBFA File Offset: 0x00079DFA
		public static string PrivacyNoticeVersionProperty
		{
			get
			{
				return "http://schemas.microsoft.com/ws/2006/05/servicemodel/securitytokenrequirement/PrivacyNoticeVersion";
			}
		}

		// Token: 0x1700082E RID: 2094
		// (get) Token: 0x060021A0 RID: 8608 RVA: 0x0007BC01 File Offset: 0x00079E01
		public static string DuplexClientLocalAddressProperty
		{
			get
			{
				return "http://schemas.microsoft.com/ws/2006/05/servicemodel/securitytokenrequirement/DuplexClientLocalAddress";
			}
		}

		// Token: 0x1700082F RID: 2095
		// (get) Token: 0x060021A1 RID: 8609 RVA: 0x0007BC08 File Offset: 0x00079E08
		public static string EndpointFilterTableProperty
		{
			get
			{
				return "http://schemas.microsoft.com/ws/2006/05/servicemodel/securitytokenrequirement/EndpointFilterTable";
			}
		}

		// Token: 0x17000830 RID: 2096
		// (get) Token: 0x060021A2 RID: 8610 RVA: 0x0007BC0F File Offset: 0x00079E0F
		public static string ChannelParametersCollectionProperty
		{
			get
			{
				return "http://schemas.microsoft.com/ws/2006/05/servicemodel/securitytokenrequirement/ChannelParametersCollection";
			}
		}

		// Token: 0x17000831 RID: 2097
		// (get) Token: 0x060021A3 RID: 8611 RVA: 0x0007BC16 File Offset: 0x00079E16
		public static string ExtendedProtectionPolicy
		{
			get
			{
				return "http://schemas.microsoft.com/ws/2006/05/servicemodel/securitytokenrequirement/ExtendedProtectionPolicy";
			}
		}

		// Token: 0x17000832 RID: 2098
		// (get) Token: 0x060021A4 RID: 8612 RVA: 0x0007BC1D File Offset: 0x00079E1D
		public bool IsInitiator
		{
			get
			{
				return this.GetPropertyOrDefault<bool>(ServiceModelSecurityTokenRequirement.IsInitiatorProperty, false);
			}
		}

		// Token: 0x17000833 RID: 2099
		// (get) Token: 0x060021A5 RID: 8613 RVA: 0x0007BC2B File Offset: 0x00079E2B
		// (set) Token: 0x060021A6 RID: 8614 RVA: 0x0007BC39 File Offset: 0x00079E39
		public SecurityAlgorithmSuite SecurityAlgorithmSuite
		{
			get
			{
				return this.GetPropertyOrDefault<SecurityAlgorithmSuite>(ServiceModelSecurityTokenRequirement.SecurityAlgorithmSuiteProperty, null);
			}
			set
			{
				base.Properties[ServiceModelSecurityTokenRequirement.SecurityAlgorithmSuiteProperty] = value;
			}
		}

		// Token: 0x17000834 RID: 2100
		// (get) Token: 0x060021A7 RID: 8615 RVA: 0x0007BC4C File Offset: 0x00079E4C
		// (set) Token: 0x060021A8 RID: 8616 RVA: 0x0007BC5A File Offset: 0x00079E5A
		public SecurityBindingElement SecurityBindingElement
		{
			get
			{
				return this.GetPropertyOrDefault<SecurityBindingElement>(ServiceModelSecurityTokenRequirement.SecurityBindingElementProperty, null);
			}
			set
			{
				base.Properties[ServiceModelSecurityTokenRequirement.SecurityBindingElementProperty] = value;
			}
		}

		// Token: 0x17000835 RID: 2101
		// (get) Token: 0x060021A9 RID: 8617 RVA: 0x0007BC6D File Offset: 0x00079E6D
		// (set) Token: 0x060021AA RID: 8618 RVA: 0x0007BC7B File Offset: 0x00079E7B
		public EndpointAddress IssuerAddress
		{
			get
			{
				return this.GetPropertyOrDefault<EndpointAddress>(ServiceModelSecurityTokenRequirement.IssuerAddressProperty, null);
			}
			set
			{
				base.Properties[ServiceModelSecurityTokenRequirement.IssuerAddressProperty] = value;
			}
		}

		// Token: 0x17000836 RID: 2102
		// (get) Token: 0x060021AB RID: 8619 RVA: 0x0007BC8E File Offset: 0x00079E8E
		// (set) Token: 0x060021AC RID: 8620 RVA: 0x0007BC9C File Offset: 0x00079E9C
		public Binding IssuerBinding
		{
			get
			{
				return this.GetPropertyOrDefault<Binding>(ServiceModelSecurityTokenRequirement.IssuerBindingProperty, null);
			}
			set
			{
				base.Properties[ServiceModelSecurityTokenRequirement.IssuerBindingProperty] = value;
			}
		}

		// Token: 0x17000837 RID: 2103
		// (get) Token: 0x060021AD RID: 8621 RVA: 0x0007BCAF File Offset: 0x00079EAF
		// (set) Token: 0x060021AE RID: 8622 RVA: 0x0007BCBD File Offset: 0x00079EBD
		public SecurityBindingElement SecureConversationSecurityBindingElement
		{
			get
			{
				return this.GetPropertyOrDefault<SecurityBindingElement>(ServiceModelSecurityTokenRequirement.SecureConversationSecurityBindingElementProperty, null);
			}
			set
			{
				base.Properties[ServiceModelSecurityTokenRequirement.SecureConversationSecurityBindingElementProperty] = value;
			}
		}

		// Token: 0x17000838 RID: 2104
		// (get) Token: 0x060021AF RID: 8623 RVA: 0x0007BCD0 File Offset: 0x00079ED0
		// (set) Token: 0x060021B0 RID: 8624 RVA: 0x0007BCDE File Offset: 0x00079EDE
		public SecurityTokenVersion MessageSecurityVersion
		{
			get
			{
				return this.GetPropertyOrDefault<SecurityTokenVersion>(ServiceModelSecurityTokenRequirement.MessageSecurityVersionProperty, null);
			}
			set
			{
				base.Properties[ServiceModelSecurityTokenRequirement.MessageSecurityVersionProperty] = value;
			}
		}

		// Token: 0x17000839 RID: 2105
		// (get) Token: 0x060021B1 RID: 8625 RVA: 0x0007BCF4 File Offset: 0x00079EF4
		// (set) Token: 0x060021B2 RID: 8626 RVA: 0x0007BD13 File Offset: 0x00079F13
		internal MessageSecurityVersion DefaultMessageSecurityVersion
		{
			get
			{
				MessageSecurityVersion result;
				if (!base.TryGetProperty<MessageSecurityVersion>(ServiceModelSecurityTokenRequirement.DefaultMessageSecurityVersionProperty, out result))
				{
					return null;
				}
				return result;
			}
			set
			{
				base.Properties[ServiceModelSecurityTokenRequirement.DefaultMessageSecurityVersionProperty] = value;
			}
		}

		// Token: 0x1700083A RID: 2106
		// (get) Token: 0x060021B3 RID: 8627 RVA: 0x0007BD26 File Offset: 0x00079F26
		// (set) Token: 0x060021B4 RID: 8628 RVA: 0x0007BD34 File Offset: 0x00079F34
		public string TransportScheme
		{
			get
			{
				return this.GetPropertyOrDefault<string>(ServiceModelSecurityTokenRequirement.TransportSchemeProperty, null);
			}
			set
			{
				base.Properties[ServiceModelSecurityTokenRequirement.TransportSchemeProperty] = value;
			}
		}

		// Token: 0x1700083B RID: 2107
		// (get) Token: 0x060021B5 RID: 8629 RVA: 0x0007BD47 File Offset: 0x00079F47
		// (set) Token: 0x060021B6 RID: 8630 RVA: 0x0007BD55 File Offset: 0x00079F55
		internal bool SupportSecurityContextCancellation
		{
			get
			{
				return this.GetPropertyOrDefault<bool>(ServiceModelSecurityTokenRequirement.SupportSecurityContextCancellationProperty, false);
			}
			set
			{
				base.Properties[ServiceModelSecurityTokenRequirement.SupportSecurityContextCancellationProperty] = value;
			}
		}

		// Token: 0x1700083C RID: 2108
		// (get) Token: 0x060021B7 RID: 8631 RVA: 0x0007BD6D File Offset: 0x00079F6D
		// (set) Token: 0x060021B8 RID: 8632 RVA: 0x0007BD7B File Offset: 0x00079F7B
		internal EndpointAddress DuplexClientLocalAddress
		{
			get
			{
				return this.GetPropertyOrDefault<EndpointAddress>("http://schemas.microsoft.com/ws/2006/05/servicemodel/securitytokenrequirement/DuplexClientLocalAddress", null);
			}
			set
			{
				base.Properties["http://schemas.microsoft.com/ws/2006/05/servicemodel/securitytokenrequirement/DuplexClientLocalAddress"] = value;
			}
		}

		// Token: 0x060021B9 RID: 8633 RVA: 0x0007BD90 File Offset: 0x00079F90
		internal TValue GetPropertyOrDefault<TValue>(string propertyName, TValue defaultValue)
		{
			TValue result;
			if (!base.TryGetProperty<TValue>(propertyName, out result))
			{
				result = defaultValue;
			}
			return result;
		}

		// Token: 0x060021BA RID: 8634 RVA: 0x0007BDAC File Offset: 0x00079FAC
		internal string InternalToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(string.Format(CultureInfo.InvariantCulture, "{0}:", new object[]
			{
				base.GetType().ToString()
			}));
			foreach (string text in base.Properties.Keys)
			{
				object obj = base.Properties[text];
				stringBuilder.AppendLine(string.Format(CultureInfo.InvariantCulture, "PropertyName: {0}", new object[]
				{
					text
				}));
				stringBuilder.AppendLine(string.Format(CultureInfo.InvariantCulture, "PropertyValue: {0}", new object[]
				{
					obj
				}));
				stringBuilder.AppendLine(string.Format(CultureInfo.InvariantCulture, "---", new object[0]));
			}
			return stringBuilder.ToString().Trim();
		}

		// Token: 0x04001F5C RID: 8028
		protected const string Namespace = "http://schemas.microsoft.com/ws/2006/05/servicemodel/securitytokenrequirement";

		// Token: 0x04001F5D RID: 8029
		private const string securityAlgorithmSuiteProperty = "http://schemas.microsoft.com/ws/2006/05/servicemodel/securitytokenrequirement/SecurityAlgorithmSuite";

		// Token: 0x04001F5E RID: 8030
		private const string securityBindingElementProperty = "http://schemas.microsoft.com/ws/2006/05/servicemodel/securitytokenrequirement/SecurityBindingElement";

		// Token: 0x04001F5F RID: 8031
		private const string issuerAddressProperty = "http://schemas.microsoft.com/ws/2006/05/servicemodel/securitytokenrequirement/IssuerAddress";

		// Token: 0x04001F60 RID: 8032
		private const string issuerBindingProperty = "http://schemas.microsoft.com/ws/2006/05/servicemodel/securitytokenrequirement/IssuerBinding";

		// Token: 0x04001F61 RID: 8033
		private const string secureConversationSecurityBindingElementProperty = "http://schemas.microsoft.com/ws/2006/05/servicemodel/securitytokenrequirement/SecureConversationSecurityBindingElement";

		// Token: 0x04001F62 RID: 8034
		private const string supportSecurityContextCancellationProperty = "http://schemas.microsoft.com/ws/2006/05/servicemodel/securitytokenrequirement/SupportSecurityContextCancellation";

		// Token: 0x04001F63 RID: 8035
		private const string messageSecurityVersionProperty = "http://schemas.microsoft.com/ws/2006/05/servicemodel/securitytokenrequirement/MessageSecurityVersion";

		// Token: 0x04001F64 RID: 8036
		private const string defaultMessageSecurityVersionProperty = "http://schemas.microsoft.com/ws/2006/05/servicemodel/securitytokenrequirement/DefaultMessageSecurityVersion";

		// Token: 0x04001F65 RID: 8037
		private const string issuerBindingContextProperty = "http://schemas.microsoft.com/ws/2006/05/servicemodel/securitytokenrequirement/IssuerBindingContext";

		// Token: 0x04001F66 RID: 8038
		private const string transportSchemeProperty = "http://schemas.microsoft.com/ws/2006/05/servicemodel/securitytokenrequirement/TransportScheme";

		// Token: 0x04001F67 RID: 8039
		private const string isInitiatorProperty = "http://schemas.microsoft.com/ws/2006/05/servicemodel/securitytokenrequirement/IsInitiator";

		// Token: 0x04001F68 RID: 8040
		private const string targetAddressProperty = "http://schemas.microsoft.com/ws/2006/05/servicemodel/securitytokenrequirement/TargetAddress";

		// Token: 0x04001F69 RID: 8041
		private const string viaProperty = "http://schemas.microsoft.com/ws/2006/05/servicemodel/securitytokenrequirement/Via";

		// Token: 0x04001F6A RID: 8042
		private const string listenUriProperty = "http://schemas.microsoft.com/ws/2006/05/servicemodel/securitytokenrequirement/ListenUri";

		// Token: 0x04001F6B RID: 8043
		private const string auditLogLocationProperty = "http://schemas.microsoft.com/ws/2006/05/servicemodel/securitytokenrequirement/AuditLogLocation";

		// Token: 0x04001F6C RID: 8044
		private const string suppressAuditFailureProperty = "http://schemas.microsoft.com/ws/2006/05/servicemodel/securitytokenrequirement/SuppressAuditFailure";

		// Token: 0x04001F6D RID: 8045
		private const string messageAuthenticationAuditLevelProperty = "http://schemas.microsoft.com/ws/2006/05/servicemodel/securitytokenrequirement/MessageAuthenticationAuditLevel";

		// Token: 0x04001F6E RID: 8046
		private const string isOutOfBandTokenProperty = "http://schemas.microsoft.com/ws/2006/05/servicemodel/securitytokenrequirement/IsOutOfBandToken";

		// Token: 0x04001F6F RID: 8047
		private const string preferSslCertificateAuthenticatorProperty = "http://schemas.microsoft.com/ws/2006/05/servicemodel/securitytokenrequirement/PreferSslCertificateAuthenticator";

		// Token: 0x04001F70 RID: 8048
		private const string supportingTokenAttachmentModeProperty = "http://schemas.microsoft.com/ws/2006/05/servicemodel/securitytokenrequirement/SupportingTokenAttachmentMode";

		// Token: 0x04001F71 RID: 8049
		private const string messageDirectionProperty = "http://schemas.microsoft.com/ws/2006/05/servicemodel/securitytokenrequirement/MessageDirection";

		// Token: 0x04001F72 RID: 8050
		private const string httpAuthenticationSchemeProperty = "http://schemas.microsoft.com/ws/2006/05/servicemodel/securitytokenrequirement/HttpAuthenticationScheme";

		// Token: 0x04001F73 RID: 8051
		private const string issuedSecurityTokenParametersProperty = "http://schemas.microsoft.com/ws/2006/05/servicemodel/securitytokenrequirement/IssuedSecurityTokenParameters";

		// Token: 0x04001F74 RID: 8052
		private const string privacyNoticeUriProperty = "http://schemas.microsoft.com/ws/2006/05/servicemodel/securitytokenrequirement/PrivacyNoticeUri";

		// Token: 0x04001F75 RID: 8053
		private const string privacyNoticeVersionProperty = "http://schemas.microsoft.com/ws/2006/05/servicemodel/securitytokenrequirement/PrivacyNoticeVersion";

		// Token: 0x04001F76 RID: 8054
		private const string duplexClientLocalAddressProperty = "http://schemas.microsoft.com/ws/2006/05/servicemodel/securitytokenrequirement/DuplexClientLocalAddress";

		// Token: 0x04001F77 RID: 8055
		private const string endpointFilterTableProperty = "http://schemas.microsoft.com/ws/2006/05/servicemodel/securitytokenrequirement/EndpointFilterTable";

		// Token: 0x04001F78 RID: 8056
		private const string channelParametersCollectionProperty = "http://schemas.microsoft.com/ws/2006/05/servicemodel/securitytokenrequirement/ChannelParametersCollection";

		// Token: 0x04001F79 RID: 8057
		private const string extendedProtectionPolicy = "http://schemas.microsoft.com/ws/2006/05/servicemodel/securitytokenrequirement/ExtendedProtectionPolicy";

		// Token: 0x04001F7A RID: 8058
		private const bool defaultSupportSecurityContextCancellation = false;
	}
}
