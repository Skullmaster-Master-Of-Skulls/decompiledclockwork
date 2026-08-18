using System;
using System.Globalization;
using System.Resources;

namespace System.ServiceModel.Discovery
{
	// Token: 0x0200005A RID: 90
	internal class SR
	{
		// Token: 0x06000430 RID: 1072 RVA: 0x00006351 File Offset: 0x00004551
		private SR()
		{
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000431 RID: 1073 RVA: 0x0000CED0 File Offset: 0x0000B0D0
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (SR.resourceManager == null)
				{
					ResourceManager resourceManager = new ResourceManager("System.ServiceModel.Discovery.SR", typeof(SR).Assembly);
					SR.resourceManager = resourceManager;
				}
				return SR.resourceManager;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000432 RID: 1074 RVA: 0x0000CF09 File Offset: 0x0000B109
		// (set) Token: 0x06000433 RID: 1075 RVA: 0x0000CF10 File Offset: 0x0000B110
		internal static CultureInfo Culture
		{
			get
			{
				return SR.resourceCulture;
			}
			set
			{
				SR.resourceCulture = value;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000434 RID: 1076 RVA: 0x0000CF18 File Offset: 0x0000B118
		internal static string DiscoveryCannotAddMatchingEndpoint
		{
			get
			{
				return SR.ResourceManager.GetString("DiscoveryCannotAddMatchingEndpoint", SR.Culture);
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000435 RID: 1077 RVA: 0x0000CF2E File Offset: 0x0000B12E
		internal static string DiscoveryClientBindingElementNotFirst
		{
			get
			{
				return SR.ResourceManager.GetString("DiscoveryClientBindingElementNotFirst", SR.Culture);
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000436 RID: 1078 RVA: 0x0000CF44 File Offset: 0x0000B144
		internal static string DiscoveryClientBindingElementPresentInDynamicEndpoint
		{
			get
			{
				return SR.ResourceManager.GetString("DiscoveryClientBindingElementPresentInDynamicEndpoint", SR.Culture);
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000437 RID: 1079 RVA: 0x0000CF5A File Offset: 0x0000B15A
		internal static string DiscoveryClientChannelEndpointNotFound
		{
			get
			{
				return SR.ResourceManager.GetString("DiscoveryClientChannelEndpointNotFound", SR.Culture);
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000438 RID: 1080 RVA: 0x0000CF70 File Offset: 0x0000B170
		internal static string DiscoveryExtensionAlreadyAttached
		{
			get
			{
				return SR.ResourceManager.GetString("DiscoveryExtensionAlreadyAttached", SR.Culture);
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000439 RID: 1081 RVA: 0x0000CF86 File Offset: 0x0000B186
		internal static string DiscoveryExtensionCannotBeDetached
		{
			get
			{
				return SR.ResourceManager.GetString("DiscoveryExtensionCannotBeDetached", SR.Culture);
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600043A RID: 1082 RVA: 0x0000CF9C File Offset: 0x0000B19C
		internal static string DiscoveryFindCanNeverComplete
		{
			get
			{
				return SR.ResourceManager.GetString("DiscoveryFindCanNeverComplete", SR.Culture);
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600043B RID: 1083 RVA: 0x0000CFB2 File Offset: 0x0000B1B2
		internal static string DiscoveryFindDurationLessThanZero
		{
			get
			{
				return SR.ResourceManager.GetString("DiscoveryFindDurationLessThanZero", SR.Culture);
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600043C RID: 1084 RVA: 0x0000CFC8 File Offset: 0x0000B1C8
		internal static string DiscoveryFindMaxResultsLessThanZero
		{
			get
			{
				return SR.ResourceManager.GetString("DiscoveryFindMaxResultsLessThanZero", SR.Culture);
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x0600043D RID: 1085 RVA: 0x0000CFDE File Offset: 0x0000B1DE
		internal static string DiscoveryMetadataVersionLessThanZero
		{
			get
			{
				return SR.ResourceManager.GetString("DiscoveryMetadataVersionLessThanZero", SR.Culture);
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x0600043E RID: 1086 RVA: 0x0000CFF4 File Offset: 0x0000B1F4
		internal static string DiscoveryResolveDurationLessThanZero
		{
			get
			{
				return SR.ResourceManager.GetString("DiscoveryResolveDurationLessThanZero", SR.Culture);
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x0600043F RID: 1087 RVA: 0x0000D00A File Offset: 0x0000B20A
		internal static string DiscoveryMetadataAlreadyOpen
		{
			get
			{
				return SR.ResourceManager.GetString("DiscoveryMetadataAlreadyOpen", SR.Culture);
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000440 RID: 1088 RVA: 0x0000D020 File Offset: 0x0000B220
		internal static string DiscoveryXmlEndpointNull
		{
			get
			{
				return SR.ResourceManager.GetString("DiscoveryXmlEndpointNull", SR.Culture);
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000441 RID: 1089 RVA: 0x0000D036 File Offset: 0x0000B236
		internal static string DiscoveryFindResponseMessageSequenceNotFound
		{
			get
			{
				return SR.ResourceManager.GetString("DiscoveryFindResponseMessageSequenceNotFound", SR.Culture);
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000442 RID: 1090 RVA: 0x0000D04C File Offset: 0x0000B24C
		internal static string DiscoveryRequestMessageError
		{
			get
			{
				return SR.ResourceManager.GetString("DiscoveryRequestMessageError", SR.Culture);
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000443 RID: 1091 RVA: 0x0000D062 File Offset: 0x0000B262
		internal static string DiscoveryIncompatibleMessageSequence
		{
			get
			{
				return SR.ResourceManager.GetString("DiscoveryIncompatibleMessageSequence", SR.Culture);
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000444 RID: 1092 RVA: 0x0000D078 File Offset: 0x0000B278
		internal static string DiscoveryMultiplePendingOperationsPerUserState
		{
			get
			{
				return SR.ResourceManager.GetString("DiscoveryMultiplePendingOperationsPerUserState", SR.Culture);
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000445 RID: 1093 RVA: 0x0000D08E File Offset: 0x0000B28E
		internal static string DiscoverySetMessageSequenceInvalidState
		{
			get
			{
				return SR.ResourceManager.GetString("DiscoverySetMessageSequenceInvalidState", SR.Culture);
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000446 RID: 1094 RVA: 0x0000D0A4 File Offset: 0x0000B2A4
		internal static string DiscoveryAppSequenceInstanceIdOutOfRange
		{
			get
			{
				return SR.ResourceManager.GetString("DiscoveryAppSequenceInstanceIdOutOfRange", SR.Culture);
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000447 RID: 1095 RVA: 0x0000D0BA File Offset: 0x0000B2BA
		internal static string DiscoveryXmlInvalidAppSequenceInstanceId
		{
			get
			{
				return SR.ResourceManager.GetString("DiscoveryXmlInvalidAppSequenceInstanceId", SR.Culture);
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000448 RID: 1096 RVA: 0x0000D0D0 File Offset: 0x0000B2D0
		internal static string DiscoveryXmlInvalidAppSequenceMessageNumber
		{
			get
			{
				return SR.ResourceManager.GetString("DiscoveryXmlInvalidAppSequenceMessageNumber", SR.Culture);
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000449 RID: 1097 RVA: 0x0000D0E6 File Offset: 0x0000B2E6
		internal static string DiscoveryXmlMissingAppSequenceInstanceId
		{
			get
			{
				return SR.ResourceManager.GetString("DiscoveryXmlMissingAppSequenceInstanceId", SR.Culture);
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600044A RID: 1098 RVA: 0x0000D0FC File Offset: 0x0000B2FC
		internal static string DiscoveryXmlMissingAppSequenceMessageNumber
		{
			get
			{
				return SR.ResourceManager.GetString("DiscoveryXmlMissingAppSequenceMessageNumber", SR.Culture);
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600044B RID: 1099 RVA: 0x0000D112 File Offset: 0x0000B312
		internal static string DiscoveryArgumentEmptyContractTypeName
		{
			get
			{
				return SR.ResourceManager.GetString("DiscoveryArgumentEmptyContractTypeName", SR.Culture);
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600044C RID: 1100 RVA: 0x0000D128 File Offset: 0x0000B328
		internal static string DiscoveryConfigInitializeFromNotSupported
		{
			get
			{
				return SR.ResourceManager.GetString("DiscoveryConfigInitializeFromNotSupported", SR.Culture);
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x0600044D RID: 1101 RVA: 0x0000D13E File Offset: 0x0000B33E
		internal static string DiscoveryNegativeDuplicateMessageHistoryLength
		{
			get
			{
				return SR.ResourceManager.GetString("DiscoveryNegativeDuplicateMessageHistoryLength", SR.Culture);
			}
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x0000D154 File Offset: 0x0000B354
		internal static string DiscoveryClientChannelCreationFailed(object param0)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("DiscoveryClientChannelCreationFailed", SR.Culture), new object[]
			{
				param0
			});
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x0000D17E File Offset: 0x0000B37E
		internal static string DiscoveryClientChannelOpenTimeout(object param0)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("DiscoveryClientChannelOpenTimeout", SR.Culture), new object[]
			{
				param0
			});
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x0000D1A8 File Offset: 0x0000B3A8
		internal static string DiscoveryMatchingRuleNotSupported(object param0, object param1, object param2, object param3)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("DiscoveryMatchingRuleNotSupported", SR.Culture), new object[]
			{
				param0,
				param1,
				param2,
				param3
			});
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x0000D1DE File Offset: 0x0000B3DE
		internal static string DiscoverySdmCollectionIsOpen(object param0)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("DiscoverySdmCollectionIsOpen", SR.Culture), new object[]
			{
				param0
			});
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x0000D208 File Offset: 0x0000B408
		internal static string DiscoveryVersionToString(object param0, object param1)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("DiscoveryVersionToString", SR.Culture), new object[]
			{
				param0,
				param1
			});
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x0000D236 File Offset: 0x0000B436
		internal static string DiscoveryXmlMaxResultsLessThanZero(object param0)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("DiscoveryXmlMaxResultsLessThanZero", SR.Culture), new object[]
			{
				param0
			});
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x0000D260 File Offset: 0x0000B460
		internal static string DiscoveryXmlDurationDeserializationError(object param0)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("DiscoveryXmlDurationDeserializationError", SR.Culture), new object[]
			{
				param0
			});
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x0000D28A File Offset: 0x0000B48A
		internal static string DiscoveryXmlDurationLessThanZero(object param0)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("DiscoveryXmlDurationLessThanZero", SR.Culture), new object[]
			{
				param0
			});
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x0000D2B4 File Offset: 0x0000B4B4
		internal static string DiscoveryXmlMetadataVersionLessThanZero(object param0)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("DiscoveryXmlMetadataVersionLessThanZero", SR.Culture), new object[]
			{
				param0
			});
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x0000D2DE File Offset: 0x0000B4DE
		internal static string DiscoveryXmlQNameLocalnameNotDefined(object param0)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("DiscoveryXmlQNameLocalnameNotDefined", SR.Culture), new object[]
			{
				param0
			});
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x0000D308 File Offset: 0x0000B508
		internal static string DiscoveryXmlQNamePrefixNotDefined(object param0, object param1)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("DiscoveryXmlQNamePrefixNotDefined", SR.Culture), new object[]
			{
				param0,
				param1
			});
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x0000D336 File Offset: 0x0000B536
		internal static string DiscoveryXmlUriFormatError(object param0)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("DiscoveryXmlUriFormatError", SR.Culture), new object[]
			{
				param0
			});
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x0000D360 File Offset: 0x0000B560
		internal static string DiscoveryCloseTimedOut(object param0)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("DiscoveryCloseTimedOut", SR.Culture), new object[]
			{
				param0
			});
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x0000D38A File Offset: 0x0000B58A
		internal static string DiscoveryArgumentInvalidScopeUri(object param0)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("DiscoveryArgumentInvalidScopeUri", SR.Culture), new object[]
			{
				param0
			});
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x0000D3B4 File Offset: 0x0000B5B4
		internal static string DiscoveryXmlAbsoluteUriFormatError(object param0)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("DiscoveryXmlAbsoluteUriFormatError", SR.Culture), new object[]
			{
				param0
			});
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x0000D3DE File Offset: 0x0000B5DE
		internal static string DiscoveryConfigInvalidScopeUri(object param0)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("DiscoveryConfigInvalidScopeUri", SR.Culture), new object[]
			{
				param0
			});
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x0000D408 File Offset: 0x0000B608
		internal static string DiscoveryFormatInvalidScopeUuidUri(object param0)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("DiscoveryFormatInvalidScopeUuidUri", SR.Culture), new object[]
			{
				param0
			});
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x0000D432 File Offset: 0x0000B632
		internal static string DiscoveryFormatInvalidScopeLdapUri(object param0)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("DiscoveryFormatInvalidScopeLdapUri", SR.Culture), new object[]
			{
				param0
			});
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x0000D45C File Offset: 0x0000B65C
		internal static string DiscoveryIncorrectVersion(object param0, object param1, object param2, object param3)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("DiscoveryIncorrectVersion", SR.Culture), new object[]
			{
				param0,
				param1,
				param2,
				param3
			});
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x0000D492 File Offset: 0x0000B692
		internal static string DiscoveryConfigAddressSpecifiedForUdpDiscoveryEndpoint(object param0)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("DiscoveryConfigAddressSpecifiedForUdpDiscoveryEndpoint", SR.Culture), new object[]
			{
				param0
			});
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x0000D4BC File Offset: 0x0000B6BC
		internal static string DiscoveryConfigContractNotSpecified(object param0)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("DiscoveryConfigContractNotSpecified", SR.Culture), new object[]
			{
				param0
			});
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x0000D4E6 File Offset: 0x0000B6E6
		internal static string DiscoveryConfigContractSpecified(object param0)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("DiscoveryConfigContractSpecified", SR.Culture), new object[]
			{
				param0
			});
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x0000D510 File Offset: 0x0000B710
		internal static string DiscoveryConfigListenUriSpecifiedForUdpDiscoveryEndpoint(object param0)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("DiscoveryConfigListenUriSpecifiedForUdpDiscoveryEndpoint", SR.Culture), new object[]
			{
				param0
			});
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x0000D53A File Offset: 0x0000B73A
		internal static string DiscoveryConfigAnnouncementEndpointMissingKind(object param0)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("DiscoveryConfigAnnouncementEndpointMissingKind", SR.Culture), new object[]
			{
				param0
			});
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x0000D564 File Offset: 0x0000B764
		internal static string DiscoveryConfigInvalidAnnouncementEndpoint(object param0, object param1, object param2)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("DiscoveryConfigInvalidAnnouncementEndpoint", SR.Culture), new object[]
			{
				param0,
				param1,
				param2
			});
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x0000D596 File Offset: 0x0000B796
		internal static string DiscoveryConfigInvalidEndpointConfiguration(object param0)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("DiscoveryConfigInvalidEndpointConfiguration", SR.Culture), new object[]
			{
				param0
			});
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x0000D5C0 File Offset: 0x0000B7C0
		internal static string DiscoveryConfigInvalidDiscoveryEndpoint(object param0, object param1, object param2)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("DiscoveryConfigInvalidDiscoveryEndpoint", SR.Culture), new object[]
			{
				param0,
				param1,
				param2
			});
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x0000D5F2 File Offset: 0x0000B7F2
		internal static string DiscoveryConfigDiscoveryEndpointMissingKind(object param0)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("DiscoveryConfigDiscoveryEndpointMissingKind", SR.Culture), new object[]
			{
				param0
			});
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x0000D61C File Offset: 0x0000B81C
		internal static string DiscoveryConfigMultipleEndpointsMatchWildcard(object param0, object param1)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("DiscoveryConfigMultipleEndpointsMatchWildcard", SR.Culture), new object[]
			{
				param0,
				param1
			});
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x0000D64A File Offset: 0x0000B84A
		internal static string DiscoveryConfigMultipleEndpointsMatch(object param0, object param1, object param2)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("DiscoveryConfigMultipleEndpointsMatch", SR.Culture), new object[]
			{
				param0,
				param1,
				param2
			});
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x0000D67C File Offset: 0x0000B87C
		internal static string DiscoveryConfigNoEndpointsMatchWildcard(object param0, object param1)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("DiscoveryConfigNoEndpointsMatchWildcard", SR.Culture), new object[]
			{
				param0,
				param1
			});
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x0000D6AA File Offset: 0x0000B8AA
		internal static string DiscoveryConfigNoEndpointsMatch(object param0, object param1, object param2)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("DiscoveryConfigNoEndpointsMatch", SR.Culture), new object[]
			{
				param0,
				param1,
				param2
			});
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x0000D6DC File Offset: 0x0000B8DC
		internal static string DiscoveryConfigDynamicEndpointInService(object param0)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("DiscoveryConfigDynamicEndpointInService", SR.Culture), new object[]
			{
				param0
			});
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x0000D706 File Offset: 0x0000B906
		internal static string DiscoveryEndpointAddressIncorrect(object param0, object param1, object param2)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("DiscoveryEndpointAddressIncorrect", SR.Culture), new object[]
			{
				param0,
				param1,
				param2
			});
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x0000D738 File Offset: 0x0000B938
		internal static string DiscoveryEndpointWithoutBehavior(object param0)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("DiscoveryEndpointWithoutBehavior", SR.Culture), new object[]
			{
				param0
			});
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x0000D762 File Offset: 0x0000B962
		internal static string DiscoveryMessageSequenceToString(object param0, object param1, object param2)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("DiscoveryMessageSequenceToString", SR.Culture), new object[]
			{
				param0,
				param1,
				param2
			});
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x0000D794 File Offset: 0x0000B994
		internal static string DiscoveryIncorrectMode(object param0)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("DiscoveryIncorrectMode", SR.Culture), new object[]
			{
				param0
			});
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x0000D7BE File Offset: 0x0000B9BE
		internal static string DiscoveryDuplicateOperationId(object param0)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("DiscoveryDuplicateOperationId", SR.Culture), new object[]
			{
				param0
			});
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x0000D7E8 File Offset: 0x0000B9E8
		internal static string DiscoveryMethodImplementationReturnsNull(object param0, object param1)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("DiscoveryMethodImplementationReturnsNull", SR.Culture), new object[]
			{
				param0,
				param1
			});
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x0000D816 File Offset: 0x0000BA16
		internal static string EndpointWithInvalidMessageVersion(object param0, object param1, object param2, object param3, object param4)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("EndpointWithInvalidMessageVersion", SR.Culture), new object[]
			{
				param0,
				param1,
				param2,
				param3,
				param4
			});
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x0000D851 File Offset: 0x0000BA51
		internal static string TimeoutOnOperation(object param0)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("TimeoutOnOperation", SR.Culture), new object[]
			{
				param0
			});
		}

		// Token: 0x04000114 RID: 276
		private static ResourceManager resourceManager;

		// Token: 0x04000115 RID: 277
		private static CultureInfo resourceCulture;
	}
}
