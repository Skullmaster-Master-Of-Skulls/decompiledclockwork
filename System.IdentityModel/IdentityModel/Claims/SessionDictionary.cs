using System;
using System.Xml;

namespace System.IdentityModel.Claims
{
	// Token: 0x020001E2 RID: 482
	internal sealed class SessionDictionary : XmlDictionary
	{
		// Token: 0x06001002 RID: 4098 RVA: 0x0004582C File Offset: 0x00043A2C
		private SessionDictionary()
		{
			this._claim = this.Add("Claim");
			this._sct = this.Add("SecurityContextToken");
			this._version = this.Add("Version");
			this._scVersion = this.Add("SecureConversationVersion");
			this._issuer = this.Add("Issuer");
			this._originalIssuer = this.Add("OriginalIssuer");
			this._issuerRef = this.Add("IssuerRef");
			this._claimCollection = this.Add("ClaimCollection");
			this._actor = this.Add("Actor");
			this._claimProperty = this.Add("ClaimProperty");
			this._claimProperties = this.Add("ClaimProperties");
			this._value = this.Add("Value");
			this._valueType = this.Add("ValueType");
			this._label = this.Add("Label");
			this._type = this.Add("Type");
			this._subjectId = this.Add("subjectID");
			this._claimPropertyName = this.Add("ClaimPropertyName");
			this._claimPropertyValue = this.Add("ClaimPropertyValue");
			this._authenticationType = this.Add("AuthenticationType");
			this._nameClaimType = this.Add("NameClaimType");
			this._roleClaimType = this.Add("RoleClaimType");
			this._nullValue = this.Add("Null");
			this._emptyString = this.Add(string.Empty);
			this._key = this.Add("Key");
			this._effectiveTime = this.Add("EffectiveTime");
			this._expiryTime = this.Add("ExpiryTime");
			this._keyGeneration = this.Add("KeyGeneration");
			this._keyEffectiveTime = this.Add("KeyEffectiveTime");
			this._keyExpiryTime = this.Add("KeyExpiryTime");
			this._sessionId = this.Add("SessionId");
			this._id = this.Add("Id");
			this._validFrom = this.Add("ValidFrom");
			this._validTo = this.Add("ValidTo");
			this._contextId = this.Add("ContextId");
			this._sesionToken = this.Add("SessionToken");
			this._sesionTokenCookie = this.Add("SessionTokenCookie");
			this._bootStrapToken = this.Add("BootStrapToken");
			this._context = this.Add("Context");
			this._claimsPrincipal = this.Add("ClaimsPrincipal");
			this._windowsPrincipal = this.Add("WindowsPrincipal");
			this._windowsIdentity = this.Add("WindowIdentity");
			this._identity = this.Add("Identity");
			this._identities = this.Add("Identities");
			this._windowsLogonName = this.Add("WindowsLogonName");
			this._persistentTrue = this.Add("PersistentTrue");
			this._sctAuthorizationPolicy = this.Add("SctAuthorizationPolicy");
			this._right = this.Add("Right");
			this._endpointId = this.Add("EndpointId");
			this._windowsSidClaim = this.Add("WindowsSidClaim");
			this._denyOnlySidClaim = this.Add("DenyOnlySidClaim");
			this._x500DistinguishedNameClaim = this.Add("X500DistinguishedNameClaim");
			this._x509ThumbprintClaim = this.Add("X509ThumbprintClaim");
			this._nameClaim = this.Add("NameClaim");
			this._dnsClaim = this.Add("DnsClaim");
			this._rsaClaim = this.Add("RsaClaim");
			this._mailAddressClaim = this.Add("MailAddressClaim");
			this._systemClaim = this.Add("SystemClaim");
			this._hashClaim = this.Add("HashClaim");
			this._spnClaim = this.Add("SpnClaim");
			this._upnClaim = this.Add("UpnClaim");
			this._urlClaim = this.Add("UrlClaim");
			this._sid = this.Add("Sid");
			this._referenceModeTrue = this.Add("ReferenceModeTrue");
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06001003 RID: 4099 RVA: 0x00045C6E File Offset: 0x00043E6E
		public static SessionDictionary Instance
		{
			get
			{
				return SessionDictionary.instance;
			}
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06001004 RID: 4100 RVA: 0x00045C75 File Offset: 0x00043E75
		public XmlDictionaryString PersistentTrue
		{
			get
			{
				return this._persistentTrue;
			}
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06001005 RID: 4101 RVA: 0x00045C7D File Offset: 0x00043E7D
		public XmlDictionaryString WindowsLogonName
		{
			get
			{
				return this._windowsLogonName;
			}
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06001006 RID: 4102 RVA: 0x00045C85 File Offset: 0x00043E85
		public XmlDictionaryString ClaimsPrincipal
		{
			get
			{
				return this._claimsPrincipal;
			}
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06001007 RID: 4103 RVA: 0x00045C8D File Offset: 0x00043E8D
		public XmlDictionaryString WindowsPrincipal
		{
			get
			{
				return this._windowsPrincipal;
			}
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06001008 RID: 4104 RVA: 0x00045C95 File Offset: 0x00043E95
		public XmlDictionaryString WindowsIdentity
		{
			get
			{
				return this._windowsIdentity;
			}
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06001009 RID: 4105 RVA: 0x00045C9D File Offset: 0x00043E9D
		public XmlDictionaryString Identity
		{
			get
			{
				return this._identity;
			}
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x0600100A RID: 4106 RVA: 0x00045CA5 File Offset: 0x00043EA5
		public XmlDictionaryString Identities
		{
			get
			{
				return this._identities;
			}
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x0600100B RID: 4107 RVA: 0x00045CAD File Offset: 0x00043EAD
		public XmlDictionaryString SessionId
		{
			get
			{
				return this._sessionId;
			}
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x0600100C RID: 4108 RVA: 0x00045CB5 File Offset: 0x00043EB5
		public XmlDictionaryString ReferenceModeTrue
		{
			get
			{
				return this._referenceModeTrue;
			}
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x0600100D RID: 4109 RVA: 0x00045CBD File Offset: 0x00043EBD
		public XmlDictionaryString ValidFrom
		{
			get
			{
				return this._validFrom;
			}
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x0600100E RID: 4110 RVA: 0x00045CC5 File Offset: 0x00043EC5
		public XmlDictionaryString ValidTo
		{
			get
			{
				return this._validTo;
			}
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x0600100F RID: 4111 RVA: 0x00045CCD File Offset: 0x00043ECD
		public XmlDictionaryString EffectiveTime
		{
			get
			{
				return this._effectiveTime;
			}
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06001010 RID: 4112 RVA: 0x00045CD5 File Offset: 0x00043ED5
		public XmlDictionaryString ExpiryTime
		{
			get
			{
				return this._expiryTime;
			}
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06001011 RID: 4113 RVA: 0x00045CDD File Offset: 0x00043EDD
		public XmlDictionaryString KeyEffectiveTime
		{
			get
			{
				return this._keyEffectiveTime;
			}
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06001012 RID: 4114 RVA: 0x00045CE5 File Offset: 0x00043EE5
		public XmlDictionaryString KeyExpiryTime
		{
			get
			{
				return this._keyExpiryTime;
			}
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06001013 RID: 4115 RVA: 0x00045CED File Offset: 0x00043EED
		public XmlDictionaryString Claim
		{
			get
			{
				return this._claim;
			}
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06001014 RID: 4116 RVA: 0x00045CF5 File Offset: 0x00043EF5
		public XmlDictionaryString Issuer
		{
			get
			{
				return this._issuer;
			}
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06001015 RID: 4117 RVA: 0x00045CFD File Offset: 0x00043EFD
		public XmlDictionaryString OriginalIssuer
		{
			get
			{
				return this._originalIssuer;
			}
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06001016 RID: 4118 RVA: 0x00045D05 File Offset: 0x00043F05
		public XmlDictionaryString IssuerRef
		{
			get
			{
				return this._issuerRef;
			}
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06001017 RID: 4119 RVA: 0x00045D0D File Offset: 0x00043F0D
		public XmlDictionaryString ClaimCollection
		{
			get
			{
				return this._claimCollection;
			}
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06001018 RID: 4120 RVA: 0x00045D15 File Offset: 0x00043F15
		public XmlDictionaryString Actor
		{
			get
			{
				return this._actor;
			}
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06001019 RID: 4121 RVA: 0x00045D1D File Offset: 0x00043F1D
		public XmlDictionaryString ClaimProperties
		{
			get
			{
				return this._claimProperties;
			}
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x0600101A RID: 4122 RVA: 0x00045D25 File Offset: 0x00043F25
		public XmlDictionaryString ClaimProperty
		{
			get
			{
				return this._claimProperty;
			}
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x0600101B RID: 4123 RVA: 0x00045D2D File Offset: 0x00043F2D
		public XmlDictionaryString Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x0600101C RID: 4124 RVA: 0x00045D35 File Offset: 0x00043F35
		public XmlDictionaryString ValueType
		{
			get
			{
				return this._valueType;
			}
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x0600101D RID: 4125 RVA: 0x00045D3D File Offset: 0x00043F3D
		public XmlDictionaryString Label
		{
			get
			{
				return this._label;
			}
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x0600101E RID: 4126 RVA: 0x00045D45 File Offset: 0x00043F45
		public XmlDictionaryString Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x0600101F RID: 4127 RVA: 0x00045D4D File Offset: 0x00043F4D
		public XmlDictionaryString SubjectId
		{
			get
			{
				return this._subjectId;
			}
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06001020 RID: 4128 RVA: 0x00045D55 File Offset: 0x00043F55
		public XmlDictionaryString ClaimPropertyName
		{
			get
			{
				return this._claimPropertyName;
			}
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06001021 RID: 4129 RVA: 0x00045D5D File Offset: 0x00043F5D
		public XmlDictionaryString ClaimPropertyValue
		{
			get
			{
				return this._claimPropertyValue;
			}
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06001022 RID: 4130 RVA: 0x00045D65 File Offset: 0x00043F65
		public XmlDictionaryString AuthenticationType
		{
			get
			{
				return this._authenticationType;
			}
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06001023 RID: 4131 RVA: 0x00045D6D File Offset: 0x00043F6D
		public XmlDictionaryString NameClaimType
		{
			get
			{
				return this._nameClaimType;
			}
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06001024 RID: 4132 RVA: 0x00045D75 File Offset: 0x00043F75
		public XmlDictionaryString RoleClaimType
		{
			get
			{
				return this._roleClaimType;
			}
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06001025 RID: 4133 RVA: 0x00045D7D File Offset: 0x00043F7D
		public XmlDictionaryString NullValue
		{
			get
			{
				return this._nullValue;
			}
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x06001026 RID: 4134 RVA: 0x00045D85 File Offset: 0x00043F85
		public XmlDictionaryString SecurityContextToken
		{
			get
			{
				return this._sct;
			}
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x06001027 RID: 4135 RVA: 0x00045D8D File Offset: 0x00043F8D
		public XmlDictionaryString Version
		{
			get
			{
				return this._version;
			}
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x06001028 RID: 4136 RVA: 0x00045D95 File Offset: 0x00043F95
		public XmlDictionaryString SecureConversationVersion
		{
			get
			{
				return this._scVersion;
			}
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x06001029 RID: 4137 RVA: 0x00045D9D File Offset: 0x00043F9D
		public XmlDictionaryString EmptyString
		{
			get
			{
				return this._emptyString;
			}
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x0600102A RID: 4138 RVA: 0x00045DA5 File Offset: 0x00043FA5
		public XmlDictionaryString Key
		{
			get
			{
				return this._key;
			}
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x0600102B RID: 4139 RVA: 0x00045DAD File Offset: 0x00043FAD
		public XmlDictionaryString KeyGeneration
		{
			get
			{
				return this._keyGeneration;
			}
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x0600102C RID: 4140 RVA: 0x00045DB5 File Offset: 0x00043FB5
		public XmlDictionaryString Id
		{
			get
			{
				return this._id;
			}
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x0600102D RID: 4141 RVA: 0x00045DBD File Offset: 0x00043FBD
		public XmlDictionaryString ContextId
		{
			get
			{
				return this._contextId;
			}
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x0600102E RID: 4142 RVA: 0x00045DC5 File Offset: 0x00043FC5
		public XmlDictionaryString SessionToken
		{
			get
			{
				return this._sesionToken;
			}
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x0600102F RID: 4143 RVA: 0x00045DCD File Offset: 0x00043FCD
		public XmlDictionaryString SessionTokenCookie
		{
			get
			{
				return this._sesionTokenCookie;
			}
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x06001030 RID: 4144 RVA: 0x00045DD5 File Offset: 0x00043FD5
		public XmlDictionaryString BootstrapToken
		{
			get
			{
				return this._bootStrapToken;
			}
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x06001031 RID: 4145 RVA: 0x00045DDD File Offset: 0x00043FDD
		public XmlDictionaryString Context
		{
			get
			{
				return this._context;
			}
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x06001032 RID: 4146 RVA: 0x00045DE5 File Offset: 0x00043FE5
		public XmlDictionaryString SctAuthorizationPolicy
		{
			get
			{
				return this._sctAuthorizationPolicy;
			}
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x06001033 RID: 4147 RVA: 0x00045DED File Offset: 0x00043FED
		public XmlDictionaryString Right
		{
			get
			{
				return this._right;
			}
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x06001034 RID: 4148 RVA: 0x00045DF5 File Offset: 0x00043FF5
		public XmlDictionaryString EndpointId
		{
			get
			{
				return this._endpointId;
			}
		}

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x06001035 RID: 4149 RVA: 0x00045DFD File Offset: 0x00043FFD
		public XmlDictionaryString WindowsSidClaim
		{
			get
			{
				return this._windowsSidClaim;
			}
		}

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x06001036 RID: 4150 RVA: 0x00045E05 File Offset: 0x00044005
		public XmlDictionaryString DenyOnlySidClaim
		{
			get
			{
				return this._denyOnlySidClaim;
			}
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06001037 RID: 4151 RVA: 0x00045E0D File Offset: 0x0004400D
		public XmlDictionaryString X500DistinguishedNameClaim
		{
			get
			{
				return this._x500DistinguishedNameClaim;
			}
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x06001038 RID: 4152 RVA: 0x00045E15 File Offset: 0x00044015
		public XmlDictionaryString X509ThumbprintClaim
		{
			get
			{
				return this._x509ThumbprintClaim;
			}
		}

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x06001039 RID: 4153 RVA: 0x00045E1D File Offset: 0x0004401D
		public XmlDictionaryString NameClaim
		{
			get
			{
				return this._nameClaim;
			}
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x0600103A RID: 4154 RVA: 0x00045E25 File Offset: 0x00044025
		public XmlDictionaryString DnsClaim
		{
			get
			{
				return this._dnsClaim;
			}
		}

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x0600103B RID: 4155 RVA: 0x00045E2D File Offset: 0x0004402D
		public XmlDictionaryString RsaClaim
		{
			get
			{
				return this._rsaClaim;
			}
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x0600103C RID: 4156 RVA: 0x00045E35 File Offset: 0x00044035
		public XmlDictionaryString MailAddressClaim
		{
			get
			{
				return this._mailAddressClaim;
			}
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x0600103D RID: 4157 RVA: 0x00045E3D File Offset: 0x0004403D
		public XmlDictionaryString SystemClaim
		{
			get
			{
				return this._systemClaim;
			}
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x0600103E RID: 4158 RVA: 0x00045E45 File Offset: 0x00044045
		public XmlDictionaryString HashClaim
		{
			get
			{
				return this._hashClaim;
			}
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x0600103F RID: 4159 RVA: 0x00045E4D File Offset: 0x0004404D
		public XmlDictionaryString SpnClaim
		{
			get
			{
				return this._spnClaim;
			}
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x06001040 RID: 4160 RVA: 0x00045E55 File Offset: 0x00044055
		public XmlDictionaryString UpnClaim
		{
			get
			{
				return this._upnClaim;
			}
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x06001041 RID: 4161 RVA: 0x00045E5D File Offset: 0x0004405D
		public XmlDictionaryString UrlClaim
		{
			get
			{
				return this._urlClaim;
			}
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x06001042 RID: 4162 RVA: 0x00045E65 File Offset: 0x00044065
		public XmlDictionaryString Sid
		{
			get
			{
				return this._sid;
			}
		}

		// Token: 0x04000DE3 RID: 3555
		private static readonly SessionDictionary instance = new SessionDictionary();

		// Token: 0x04000DE4 RID: 3556
		private XmlDictionaryString _claim;

		// Token: 0x04000DE5 RID: 3557
		private XmlDictionaryString _sct;

		// Token: 0x04000DE6 RID: 3558
		private XmlDictionaryString _issuer;

		// Token: 0x04000DE7 RID: 3559
		private XmlDictionaryString _originalIssuer;

		// Token: 0x04000DE8 RID: 3560
		private XmlDictionaryString _issuerRef;

		// Token: 0x04000DE9 RID: 3561
		private XmlDictionaryString _claimCollection;

		// Token: 0x04000DEA RID: 3562
		private XmlDictionaryString _actor;

		// Token: 0x04000DEB RID: 3563
		private XmlDictionaryString _claimProperty;

		// Token: 0x04000DEC RID: 3564
		private XmlDictionaryString _claimProperties;

		// Token: 0x04000DED RID: 3565
		private XmlDictionaryString _value;

		// Token: 0x04000DEE RID: 3566
		private XmlDictionaryString _valueType;

		// Token: 0x04000DEF RID: 3567
		private XmlDictionaryString _label;

		// Token: 0x04000DF0 RID: 3568
		private XmlDictionaryString _claimPropertyName;

		// Token: 0x04000DF1 RID: 3569
		private XmlDictionaryString _claimPropertyValue;

		// Token: 0x04000DF2 RID: 3570
		private XmlDictionaryString _type;

		// Token: 0x04000DF3 RID: 3571
		private XmlDictionaryString _subjectId;

		// Token: 0x04000DF4 RID: 3572
		private XmlDictionaryString _contextId;

		// Token: 0x04000DF5 RID: 3573
		private XmlDictionaryString _authenticationType;

		// Token: 0x04000DF6 RID: 3574
		private XmlDictionaryString _nameClaimType;

		// Token: 0x04000DF7 RID: 3575
		private XmlDictionaryString _roleClaimType;

		// Token: 0x04000DF8 RID: 3576
		private XmlDictionaryString _version;

		// Token: 0x04000DF9 RID: 3577
		private XmlDictionaryString _scVersion;

		// Token: 0x04000DFA RID: 3578
		private XmlDictionaryString _emptyString;

		// Token: 0x04000DFB RID: 3579
		private XmlDictionaryString _nullValue;

		// Token: 0x04000DFC RID: 3580
		private XmlDictionaryString _key;

		// Token: 0x04000DFD RID: 3581
		private XmlDictionaryString _effectiveTime;

		// Token: 0x04000DFE RID: 3582
		private XmlDictionaryString _expiryTime;

		// Token: 0x04000DFF RID: 3583
		private XmlDictionaryString _keyGeneration;

		// Token: 0x04000E00 RID: 3584
		private XmlDictionaryString _keyEffectiveTime;

		// Token: 0x04000E01 RID: 3585
		private XmlDictionaryString _keyExpiryTime;

		// Token: 0x04000E02 RID: 3586
		private XmlDictionaryString _sessionId;

		// Token: 0x04000E03 RID: 3587
		private XmlDictionaryString _id;

		// Token: 0x04000E04 RID: 3588
		private XmlDictionaryString _validFrom;

		// Token: 0x04000E05 RID: 3589
		private XmlDictionaryString _validTo;

		// Token: 0x04000E06 RID: 3590
		private XmlDictionaryString _sesionToken;

		// Token: 0x04000E07 RID: 3591
		private XmlDictionaryString _sesionTokenCookie;

		// Token: 0x04000E08 RID: 3592
		private XmlDictionaryString _bootStrapToken;

		// Token: 0x04000E09 RID: 3593
		private XmlDictionaryString _context;

		// Token: 0x04000E0A RID: 3594
		private XmlDictionaryString _claimsPrincipal;

		// Token: 0x04000E0B RID: 3595
		private XmlDictionaryString _windowsPrincipal;

		// Token: 0x04000E0C RID: 3596
		private XmlDictionaryString _windowsIdentity;

		// Token: 0x04000E0D RID: 3597
		private XmlDictionaryString _identity;

		// Token: 0x04000E0E RID: 3598
		private XmlDictionaryString _identities;

		// Token: 0x04000E0F RID: 3599
		private XmlDictionaryString _windowsLogonName;

		// Token: 0x04000E10 RID: 3600
		private XmlDictionaryString _persistentTrue;

		// Token: 0x04000E11 RID: 3601
		private XmlDictionaryString _sctAuthorizationPolicy;

		// Token: 0x04000E12 RID: 3602
		private XmlDictionaryString _right;

		// Token: 0x04000E13 RID: 3603
		private XmlDictionaryString _endpointId;

		// Token: 0x04000E14 RID: 3604
		private XmlDictionaryString _windowsSidClaim;

		// Token: 0x04000E15 RID: 3605
		private XmlDictionaryString _denyOnlySidClaim;

		// Token: 0x04000E16 RID: 3606
		private XmlDictionaryString _x500DistinguishedNameClaim;

		// Token: 0x04000E17 RID: 3607
		private XmlDictionaryString _x509ThumbprintClaim;

		// Token: 0x04000E18 RID: 3608
		private XmlDictionaryString _nameClaim;

		// Token: 0x04000E19 RID: 3609
		private XmlDictionaryString _dnsClaim;

		// Token: 0x04000E1A RID: 3610
		private XmlDictionaryString _rsaClaim;

		// Token: 0x04000E1B RID: 3611
		private XmlDictionaryString _mailAddressClaim;

		// Token: 0x04000E1C RID: 3612
		private XmlDictionaryString _systemClaim;

		// Token: 0x04000E1D RID: 3613
		private XmlDictionaryString _hashClaim;

		// Token: 0x04000E1E RID: 3614
		private XmlDictionaryString _spnClaim;

		// Token: 0x04000E1F RID: 3615
		private XmlDictionaryString _upnClaim;

		// Token: 0x04000E20 RID: 3616
		private XmlDictionaryString _urlClaim;

		// Token: 0x04000E21 RID: 3617
		private XmlDictionaryString _sid;

		// Token: 0x04000E22 RID: 3618
		private XmlDictionaryString _referenceModeTrue;
	}
}
