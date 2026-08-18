using System;

namespace System.Data.Entity
{
	// Token: 0x02000126 RID: 294
	internal static class Strings
	{
		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000FC8 RID: 4040 RVA: 0x00041CCC File Offset: 0x0003FECC
		internal static string EntityKey_DataRecordMustBeEntity
		{
			get
			{
				return EntityRes.GetString("EntityKey_DataRecordMustBeEntity");
			}
		}

		// Token: 0x06000FC9 RID: 4041 RVA: 0x00041CD8 File Offset: 0x0003FED8
		internal static string EntityKey_EntitySetDoesNotMatch(object p0)
		{
			return EntityRes.GetString("EntityKey_EntitySetDoesNotMatch", new object[]
			{
				p0
			});
		}

		// Token: 0x06000FCA RID: 4042 RVA: 0x00041CEE File Offset: 0x0003FEEE
		internal static string EntityKey_EntityTypesDoNotMatch(object p0, object p1)
		{
			return EntityRes.GetString("EntityKey_EntityTypesDoNotMatch", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06000FCB RID: 4043 RVA: 0x00041D08 File Offset: 0x0003FF08
		internal static string EntityKey_IncorrectNumberOfKeyValuePairs(object p0, object p1, object p2)
		{
			return EntityRes.GetString("EntityKey_IncorrectNumberOfKeyValuePairs", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06000FCC RID: 4044 RVA: 0x00041D26 File Offset: 0x0003FF26
		internal static string EntityKey_IncorrectValueType(object p0, object p1, object p2)
		{
			return EntityRes.GetString("EntityKey_IncorrectValueType", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06000FCD RID: 4045 RVA: 0x00041D44 File Offset: 0x0003FF44
		internal static string EntityKey_NoCorrespondingOSpaceTypeForEnumKeyMember(object p0, object p1)
		{
			return EntityRes.GetString("EntityKey_NoCorrespondingOSpaceTypeForEnumKeyMember", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06000FCE RID: 4046 RVA: 0x00041D5E File Offset: 0x0003FF5E
		internal static string EntityKey_MissingKeyValue(object p0, object p1)
		{
			return EntityRes.GetString("EntityKey_MissingKeyValue", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000FCF RID: 4047 RVA: 0x00041D78 File Offset: 0x0003FF78
		internal static string EntityKey_NoNullsAllowedInKeyValuePairs
		{
			get
			{
				return EntityRes.GetString("EntityKey_NoNullsAllowedInKeyValuePairs");
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000FD0 RID: 4048 RVA: 0x00041D84 File Offset: 0x0003FF84
		internal static string EntityKey_UnexpectedNull
		{
			get
			{
				return EntityRes.GetString("EntityKey_UnexpectedNull");
			}
		}

		// Token: 0x06000FD1 RID: 4049 RVA: 0x00041D90 File Offset: 0x0003FF90
		internal static string EntityKey_DoesntMatchKeyOnEntity(object p0)
		{
			return EntityRes.GetString("EntityKey_DoesntMatchKeyOnEntity", new object[]
			{
				p0
			});
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000FD2 RID: 4050 RVA: 0x00041DA6 File Offset: 0x0003FFA6
		internal static string EntityKey_EntityKeyMustHaveValues
		{
			get
			{
				return EntityRes.GetString("EntityKey_EntityKeyMustHaveValues");
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000FD3 RID: 4051 RVA: 0x00041DB2 File Offset: 0x0003FFB2
		internal static string EntityKey_InvalidQualifiedEntitySetName
		{
			get
			{
				return EntityRes.GetString("EntityKey_InvalidQualifiedEntitySetName");
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000FD4 RID: 4052 RVA: 0x00041DBE File Offset: 0x0003FFBE
		internal static string EntityKey_MissingEntitySetName
		{
			get
			{
				return EntityRes.GetString("EntityKey_MissingEntitySetName");
			}
		}

		// Token: 0x06000FD5 RID: 4053 RVA: 0x00041DCA File Offset: 0x0003FFCA
		internal static string EntityKey_InvalidName(object p0)
		{
			return EntityRes.GetString("EntityKey_InvalidName", new object[]
			{
				p0
			});
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000FD6 RID: 4054 RVA: 0x00041DE0 File Offset: 0x0003FFE0
		internal static string EntityKey_CannotChangeKey
		{
			get
			{
				return EntityRes.GetString("EntityKey_CannotChangeKey");
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000FD7 RID: 4055 RVA: 0x00041DEC File Offset: 0x0003FFEC
		internal static string EntityTypesDoNotAgree
		{
			get
			{
				return EntityRes.GetString("EntityTypesDoNotAgree");
			}
		}

		// Token: 0x06000FD8 RID: 4056 RVA: 0x00041DF8 File Offset: 0x0003FFF8
		internal static string EntityKey_NullKeyValue(object p0, object p1)
		{
			return EntityRes.GetString("EntityKey_NullKeyValue", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000FD9 RID: 4057 RVA: 0x00041E12 File Offset: 0x00040012
		internal static string EdmMembersDefiningTypeDoNotAgreeWithMetadataType
		{
			get
			{
				return EntityRes.GetString("EdmMembersDefiningTypeDoNotAgreeWithMetadataType");
			}
		}

		// Token: 0x06000FDA RID: 4058 RVA: 0x00041E1E File Offset: 0x0004001E
		internal static string InvalidStringArgument(object p0)
		{
			return EntityRes.GetString("InvalidStringArgument", new object[]
			{
				p0
			});
		}

		// Token: 0x06000FDB RID: 4059 RVA: 0x00041E34 File Offset: 0x00040034
		internal static string CannotCallNoncomposableFunction(object p0)
		{
			return EntityRes.GetString("CannotCallNoncomposableFunction", new object[]
			{
				p0
			});
		}

		// Token: 0x06000FDC RID: 4060 RVA: 0x00041E4A File Offset: 0x0004004A
		internal static string EntityClient_ConnectionStringMissingInfo(object p0)
		{
			return EntityRes.GetString("EntityClient_ConnectionStringMissingInfo", new object[]
			{
				p0
			});
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000FDD RID: 4061 RVA: 0x00041E60 File Offset: 0x00040060
		internal static string EntityClient_ValueNotString
		{
			get
			{
				return EntityRes.GetString("EntityClient_ValueNotString");
			}
		}

		// Token: 0x06000FDE RID: 4062 RVA: 0x00041E6C File Offset: 0x0004006C
		internal static string EntityClient_KeywordNotSupported(object p0)
		{
			return EntityRes.GetString("EntityClient_KeywordNotSupported", new object[]
			{
				p0
			});
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000FDF RID: 4063 RVA: 0x00041E82 File Offset: 0x00040082
		internal static string EntityClient_NoCommandText
		{
			get
			{
				return EntityRes.GetString("EntityClient_NoCommandText");
			}
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000FE0 RID: 4064 RVA: 0x00041E8E File Offset: 0x0004008E
		internal static string EntityClient_ConnectionStringNeededBeforeOperation
		{
			get
			{
				return EntityRes.GetString("EntityClient_ConnectionStringNeededBeforeOperation");
			}
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000FE1 RID: 4065 RVA: 0x00041E9A File Offset: 0x0004009A
		internal static string EntityClient_CannotReopenConnection
		{
			get
			{
				return EntityRes.GetString("EntityClient_CannotReopenConnection");
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000FE2 RID: 4066 RVA: 0x00041EA6 File Offset: 0x000400A6
		internal static string EntityClient_ConnectionNotOpen
		{
			get
			{
				return EntityRes.GetString("EntityClient_ConnectionNotOpen");
			}
		}

		// Token: 0x06000FE3 RID: 4067 RVA: 0x00041EB2 File Offset: 0x000400B2
		internal static string EntityClient_DuplicateParameterNames(object p0)
		{
			return EntityRes.GetString("EntityClient_DuplicateParameterNames", new object[]
			{
				p0
			});
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000FE4 RID: 4068 RVA: 0x00041EC8 File Offset: 0x000400C8
		internal static string EntityClient_NoConnectionForCommand
		{
			get
			{
				return EntityRes.GetString("EntityClient_NoConnectionForCommand");
			}
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000FE5 RID: 4069 RVA: 0x00041ED4 File Offset: 0x000400D4
		internal static string EntityClient_NoConnectionForAdapter
		{
			get
			{
				return EntityRes.GetString("EntityClient_NoConnectionForAdapter");
			}
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000FE6 RID: 4070 RVA: 0x00041EE0 File Offset: 0x000400E0
		internal static string EntityClient_ClosedConnectionForUpdate
		{
			get
			{
				return EntityRes.GetString("EntityClient_ClosedConnectionForUpdate");
			}
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000FE7 RID: 4071 RVA: 0x00041EEC File Offset: 0x000400EC
		internal static string EntityClient_InvalidNamedConnection
		{
			get
			{
				return EntityRes.GetString("EntityClient_InvalidNamedConnection");
			}
		}

		// Token: 0x06000FE8 RID: 4072 RVA: 0x00041EF8 File Offset: 0x000400F8
		internal static string EntityClient_NestedNamedConnection(object p0)
		{
			return EntityRes.GetString("EntityClient_NestedNamedConnection", new object[]
			{
				p0
			});
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000FE9 RID: 4073 RVA: 0x00041F0E File Offset: 0x0004010E
		internal static string EntityClient_InvalidStoreProvider
		{
			get
			{
				return EntityRes.GetString("EntityClient_InvalidStoreProvider");
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000FEA RID: 4074 RVA: 0x00041F1A File Offset: 0x0004011A
		internal static string EntityClient_DataReaderIsStillOpen
		{
			get
			{
				return EntityRes.GetString("EntityClient_DataReaderIsStillOpen");
			}
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000FEB RID: 4075 RVA: 0x00041F26 File Offset: 0x00040126
		internal static string EntityClient_SettingsCannotBeChangedOnOpenConnection
		{
			get
			{
				return EntityRes.GetString("EntityClient_SettingsCannotBeChangedOnOpenConnection");
			}
		}

		// Token: 0x06000FEC RID: 4076 RVA: 0x00041F32 File Offset: 0x00040132
		internal static string EntityClient_ExecutingOnClosedConnection(object p0)
		{
			return EntityRes.GetString("EntityClient_ExecutingOnClosedConnection", new object[]
			{
				p0
			});
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000FED RID: 4077 RVA: 0x00041F48 File Offset: 0x00040148
		internal static string EntityClient_ConnectionStateClosed
		{
			get
			{
				return EntityRes.GetString("EntityClient_ConnectionStateClosed");
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000FEE RID: 4078 RVA: 0x00041F54 File Offset: 0x00040154
		internal static string EntityClient_ConnectionStateBroken
		{
			get
			{
				return EntityRes.GetString("EntityClient_ConnectionStateBroken");
			}
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000FEF RID: 4079 RVA: 0x00041F60 File Offset: 0x00040160
		internal static string EntityClient_CannotCloneStoreProvider
		{
			get
			{
				return EntityRes.GetString("EntityClient_CannotCloneStoreProvider");
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000FF0 RID: 4080 RVA: 0x00041F6C File Offset: 0x0004016C
		internal static string EntityClient_UnsupportedCommandType
		{
			get
			{
				return EntityRes.GetString("EntityClient_UnsupportedCommandType");
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000FF1 RID: 4081 RVA: 0x00041F78 File Offset: 0x00040178
		internal static string EntityClient_ErrorInClosingConnection
		{
			get
			{
				return EntityRes.GetString("EntityClient_ErrorInClosingConnection");
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000FF2 RID: 4082 RVA: 0x00041F84 File Offset: 0x00040184
		internal static string EntityClient_ErrorInBeginningTransaction
		{
			get
			{
				return EntityRes.GetString("EntityClient_ErrorInBeginningTransaction");
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000FF3 RID: 4083 RVA: 0x00041F90 File Offset: 0x00040190
		internal static string EntityClient_ExtraParametersWithNamedConnection
		{
			get
			{
				return EntityRes.GetString("EntityClient_ExtraParametersWithNamedConnection");
			}
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000FF4 RID: 4084 RVA: 0x00041F9C File Offset: 0x0004019C
		internal static string EntityClient_CommandDefinitionPreparationFailed
		{
			get
			{
				return EntityRes.GetString("EntityClient_CommandDefinitionPreparationFailed");
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000FF5 RID: 4085 RVA: 0x00041FA8 File Offset: 0x000401A8
		internal static string EntityClient_CommandDefinitionExecutionFailed
		{
			get
			{
				return EntityRes.GetString("EntityClient_CommandDefinitionExecutionFailed");
			}
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000FF6 RID: 4086 RVA: 0x00041FB4 File Offset: 0x000401B4
		internal static string EntityClient_CommandExecutionFailed
		{
			get
			{
				return EntityRes.GetString("EntityClient_CommandExecutionFailed");
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000FF7 RID: 4087 RVA: 0x00041FC0 File Offset: 0x000401C0
		internal static string EntityClient_StoreReaderFailed
		{
			get
			{
				return EntityRes.GetString("EntityClient_StoreReaderFailed");
			}
		}

		// Token: 0x06000FF8 RID: 4088 RVA: 0x00041FCC File Offset: 0x000401CC
		internal static string EntityClient_FailedToGetInformation(object p0)
		{
			return EntityRes.GetString("EntityClient_FailedToGetInformation", new object[]
			{
				p0
			});
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000FF9 RID: 4089 RVA: 0x00041FE2 File Offset: 0x000401E2
		internal static string EntityClient_TooFewColumns
		{
			get
			{
				return EntityRes.GetString("EntityClient_TooFewColumns");
			}
		}

		// Token: 0x06000FFA RID: 4090 RVA: 0x00041FEE File Offset: 0x000401EE
		internal static string EntityClient_InvalidParameterName(object p0)
		{
			return EntityRes.GetString("EntityClient_InvalidParameterName", new object[]
			{
				p0
			});
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000FFB RID: 4091 RVA: 0x00042004 File Offset: 0x00040204
		internal static string EntityClient_EmptyParameterName
		{
			get
			{
				return EntityRes.GetString("EntityClient_EmptyParameterName");
			}
		}

		// Token: 0x06000FFC RID: 4092 RVA: 0x00042010 File Offset: 0x00040210
		internal static string EntityClient_ReturnedNullOnProviderMethod(object p0, object p1)
		{
			return EntityRes.GetString("EntityClient_ReturnedNullOnProviderMethod", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000FFD RID: 4093 RVA: 0x0004202A File Offset: 0x0004022A
		internal static string EntityClient_CannotDeduceDbType
		{
			get
			{
				return EntityRes.GetString("EntityClient_CannotDeduceDbType");
			}
		}

		// Token: 0x06000FFE RID: 4094 RVA: 0x00042036 File Offset: 0x00040236
		internal static string EntityClient_InvalidParameterDirection(object p0)
		{
			return EntityRes.GetString("EntityClient_InvalidParameterDirection", new object[]
			{
				p0
			});
		}

		// Token: 0x06000FFF RID: 4095 RVA: 0x0004204C File Offset: 0x0004024C
		internal static string EntityClient_UnknownParameterType(object p0)
		{
			return EntityRes.GetString("EntityClient_UnknownParameterType", new object[]
			{
				p0
			});
		}

		// Token: 0x06001000 RID: 4096 RVA: 0x00042062 File Offset: 0x00040262
		internal static string EntityClient_UnsupportedDbType(object p0, object p1)
		{
			return EntityRes.GetString("EntityClient_UnsupportedDbType", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06001001 RID: 4097 RVA: 0x0004207C File Offset: 0x0004027C
		internal static string EntityClient_DoesNotImplementIServiceProvider(object p0)
		{
			return EntityRes.GetString("EntityClient_DoesNotImplementIServiceProvider", new object[]
			{
				p0
			});
		}

		// Token: 0x06001002 RID: 4098 RVA: 0x00042092 File Offset: 0x00040292
		internal static string EntityClient_IncompatibleNavigationPropertyResult(object p0, object p1)
		{
			return EntityRes.GetString("EntityClient_IncompatibleNavigationPropertyResult", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06001003 RID: 4099 RVA: 0x000420AC File Offset: 0x000402AC
		internal static string EntityClient_TransactionAlreadyStarted
		{
			get
			{
				return EntityRes.GetString("EntityClient_TransactionAlreadyStarted");
			}
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06001004 RID: 4100 RVA: 0x000420B8 File Offset: 0x000402B8
		internal static string EntityClient_InvalidTransactionForCommand
		{
			get
			{
				return EntityRes.GetString("EntityClient_InvalidTransactionForCommand");
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06001005 RID: 4101 RVA: 0x000420C4 File Offset: 0x000402C4
		internal static string EntityClient_NoStoreConnectionForUpdate
		{
			get
			{
				return EntityRes.GetString("EntityClient_NoStoreConnectionForUpdate");
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06001006 RID: 4102 RVA: 0x000420D0 File Offset: 0x000402D0
		internal static string EntityClient_CommandTreeMetadataIncompatible
		{
			get
			{
				return EntityRes.GetString("EntityClient_CommandTreeMetadataIncompatible");
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06001007 RID: 4103 RVA: 0x000420DC File Offset: 0x000402DC
		internal static string EntityClient_ProviderGeneralError
		{
			get
			{
				return EntityRes.GetString("EntityClient_ProviderGeneralError");
			}
		}

		// Token: 0x06001008 RID: 4104 RVA: 0x000420E8 File Offset: 0x000402E8
		internal static string EntityClient_ProviderSpecificError(object p0)
		{
			return EntityRes.GetString("EntityClient_ProviderSpecificError", new object[]
			{
				p0
			});
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06001009 RID: 4105 RVA: 0x000420FE File Offset: 0x000402FE
		internal static string EntityClient_FunctionImportEmptyCommandText
		{
			get
			{
				return EntityRes.GetString("EntityClient_FunctionImportEmptyCommandText");
			}
		}

		// Token: 0x0600100A RID: 4106 RVA: 0x0004210A File Offset: 0x0004030A
		internal static string EntityClient_UnableToFindFunctionImportContainer(object p0)
		{
			return EntityRes.GetString("EntityClient_UnableToFindFunctionImportContainer", new object[]
			{
				p0
			});
		}

		// Token: 0x0600100B RID: 4107 RVA: 0x00042120 File Offset: 0x00040320
		internal static string EntityClient_UnableToFindFunctionImport(object p0, object p1)
		{
			return EntityRes.GetString("EntityClient_UnableToFindFunctionImport", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x0600100C RID: 4108 RVA: 0x0004213A File Offset: 0x0004033A
		internal static string EntityClient_FunctionImportMustBeNonComposable(object p0)
		{
			return EntityRes.GetString("EntityClient_FunctionImportMustBeNonComposable", new object[]
			{
				p0
			});
		}

		// Token: 0x0600100D RID: 4109 RVA: 0x00042150 File Offset: 0x00040350
		internal static string EntityClient_UnmappedFunctionImport(object p0)
		{
			return EntityRes.GetString("EntityClient_UnmappedFunctionImport", new object[]
			{
				p0
			});
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x0600100E RID: 4110 RVA: 0x00042166 File Offset: 0x00040366
		internal static string EntityClient_InvalidStoredProcedureCommandText
		{
			get
			{
				return EntityRes.GetString("EntityClient_InvalidStoredProcedureCommandText");
			}
		}

		// Token: 0x0600100F RID: 4111 RVA: 0x00042172 File Offset: 0x00040372
		internal static string EntityClient_ItemCollectionsNotRegisteredInWorkspace(object p0)
		{
			return EntityRes.GetString("EntityClient_ItemCollectionsNotRegisteredInWorkspace", new object[]
			{
				p0
			});
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06001010 RID: 4112 RVA: 0x00042188 File Offset: 0x00040388
		internal static string EntityClient_ConnectionMustBeClosed
		{
			get
			{
				return EntityRes.GetString("EntityClient_ConnectionMustBeClosed");
			}
		}

		// Token: 0x06001011 RID: 4113 RVA: 0x00042194 File Offset: 0x00040394
		internal static string EntityClient_DbConnectionHasNoProvider(object p0)
		{
			return EntityRes.GetString("EntityClient_DbConnectionHasNoProvider", new object[]
			{
				p0
			});
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06001012 RID: 4114 RVA: 0x000421AA File Offset: 0x000403AA
		internal static string EntityClient_RequiresNonStoreCommandTree
		{
			get
			{
				return EntityRes.GetString("EntityClient_RequiresNonStoreCommandTree");
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06001013 RID: 4115 RVA: 0x000421B6 File Offset: 0x000403B6
		internal static string EntityClient_CannotReprepareCommandDefinitionBasedCommand
		{
			get
			{
				return EntityRes.GetString("EntityClient_CannotReprepareCommandDefinitionBasedCommand");
			}
		}

		// Token: 0x06001014 RID: 4116 RVA: 0x000421C2 File Offset: 0x000403C2
		internal static string EntityClient_EntityParameterEdmTypeNotScalar(object p0)
		{
			return EntityRes.GetString("EntityClient_EntityParameterEdmTypeNotScalar", new object[]
			{
				p0
			});
		}

		// Token: 0x06001015 RID: 4117 RVA: 0x000421D8 File Offset: 0x000403D8
		internal static string EntityClient_EntityParameterInconsistentEdmType(object p0, object p1)
		{
			return EntityRes.GetString("EntityClient_EntityParameterInconsistentEdmType", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06001016 RID: 4118 RVA: 0x000421F2 File Offset: 0x000403F2
		internal static string EntityClient_CannotGetCommandText
		{
			get
			{
				return EntityRes.GetString("EntityClient_CannotGetCommandText");
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06001017 RID: 4119 RVA: 0x000421FE File Offset: 0x000403FE
		internal static string EntityClient_CannotSetCommandText
		{
			get
			{
				return EntityRes.GetString("EntityClient_CannotSetCommandText");
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06001018 RID: 4120 RVA: 0x0004220A File Offset: 0x0004040A
		internal static string EntityClient_CannotGetCommandTree
		{
			get
			{
				return EntityRes.GetString("EntityClient_CannotGetCommandTree");
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06001019 RID: 4121 RVA: 0x00042216 File Offset: 0x00040416
		internal static string EntityClient_CannotSetCommandTree
		{
			get
			{
				return EntityRes.GetString("EntityClient_CannotSetCommandTree");
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x0600101A RID: 4122 RVA: 0x00042222 File Offset: 0x00040422
		internal static string ELinq_ExpressionMustBeIQueryable
		{
			get
			{
				return EntityRes.GetString("ELinq_ExpressionMustBeIQueryable");
			}
		}

		// Token: 0x0600101B RID: 4123 RVA: 0x0004222E File Offset: 0x0004042E
		internal static string ELinq_UnsupportedExpressionType(object p0)
		{
			return EntityRes.GetString("ELinq_UnsupportedExpressionType", new object[]
			{
				p0
			});
		}

		// Token: 0x0600101C RID: 4124 RVA: 0x00042244 File Offset: 0x00040444
		internal static string ELinq_UnsupportedUseOfContextParameter(object p0)
		{
			return EntityRes.GetString("ELinq_UnsupportedUseOfContextParameter", new object[]
			{
				p0
			});
		}

		// Token: 0x0600101D RID: 4125 RVA: 0x0004225A File Offset: 0x0004045A
		internal static string ELinq_UnboundParameterExpression(object p0)
		{
			return EntityRes.GetString("ELinq_UnboundParameterExpression", new object[]
			{
				p0
			});
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x0600101E RID: 4126 RVA: 0x00042270 File Offset: 0x00040470
		internal static string ELinq_UnsupportedConstructor
		{
			get
			{
				return EntityRes.GetString("ELinq_UnsupportedConstructor");
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x0600101F RID: 4127 RVA: 0x0004227C File Offset: 0x0004047C
		internal static string ELinq_UnsupportedInitializers
		{
			get
			{
				return EntityRes.GetString("ELinq_UnsupportedInitializers");
			}
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06001020 RID: 4128 RVA: 0x00042288 File Offset: 0x00040488
		internal static string ELinq_UnsupportedBinding
		{
			get
			{
				return EntityRes.GetString("ELinq_UnsupportedBinding");
			}
		}

		// Token: 0x06001021 RID: 4129 RVA: 0x00042294 File Offset: 0x00040494
		internal static string ELinq_UnsupportedMethod(object p0)
		{
			return EntityRes.GetString("ELinq_UnsupportedMethod", new object[]
			{
				p0
			});
		}

		// Token: 0x06001022 RID: 4130 RVA: 0x000422AA File Offset: 0x000404AA
		internal static string ELinq_UnsupportedMethodSuggestedAlternative(object p0, object p1)
		{
			return EntityRes.GetString("ELinq_UnsupportedMethodSuggestedAlternative", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06001023 RID: 4131 RVA: 0x000422C4 File Offset: 0x000404C4
		internal static string ELinq_ThenByDoesNotFollowOrderBy
		{
			get
			{
				return EntityRes.GetString("ELinq_ThenByDoesNotFollowOrderBy");
			}
		}

		// Token: 0x06001024 RID: 4132 RVA: 0x000422D0 File Offset: 0x000404D0
		internal static string ELinq_UnrecognizedMember(object p0)
		{
			return EntityRes.GetString("ELinq_UnrecognizedMember", new object[]
			{
				p0
			});
		}

		// Token: 0x06001025 RID: 4133 RVA: 0x000422E6 File Offset: 0x000404E6
		internal static string ELinq_UnresolvableFunctionForMethod(object p0, object p1)
		{
			return EntityRes.GetString("ELinq_UnresolvableFunctionForMethod", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06001026 RID: 4134 RVA: 0x00042300 File Offset: 0x00040500
		internal static string ELinq_UnresolvableFunctionForMethodAmbiguousMatch(object p0, object p1)
		{
			return EntityRes.GetString("ELinq_UnresolvableFunctionForMethodAmbiguousMatch", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06001027 RID: 4135 RVA: 0x0004231A File Offset: 0x0004051A
		internal static string ELinq_UnresolvableFunctionForMethodNotFound(object p0, object p1)
		{
			return EntityRes.GetString("ELinq_UnresolvableFunctionForMethodNotFound", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06001028 RID: 4136 RVA: 0x00042334 File Offset: 0x00040534
		internal static string ELinq_UnresolvableFunctionForMember(object p0, object p1)
		{
			return EntityRes.GetString("ELinq_UnresolvableFunctionForMember", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06001029 RID: 4137 RVA: 0x0004234E File Offset: 0x0004054E
		internal static string ELinq_UnresolvableStoreFunctionForMember(object p0, object p1)
		{
			return EntityRes.GetString("ELinq_UnresolvableStoreFunctionForMember", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x0600102A RID: 4138 RVA: 0x00042368 File Offset: 0x00040568
		internal static string ELinq_UnresolvableFunctionForExpression(object p0)
		{
			return EntityRes.GetString("ELinq_UnresolvableFunctionForExpression", new object[]
			{
				p0
			});
		}

		// Token: 0x0600102B RID: 4139 RVA: 0x0004237E File Offset: 0x0004057E
		internal static string ELinq_UnresolvableStoreFunctionForExpression(object p0)
		{
			return EntityRes.GetString("ELinq_UnresolvableStoreFunctionForExpression", new object[]
			{
				p0
			});
		}

		// Token: 0x0600102C RID: 4140 RVA: 0x00042394 File Offset: 0x00040594
		internal static string ELinq_UnsupportedType(object p0)
		{
			return EntityRes.GetString("ELinq_UnsupportedType", new object[]
			{
				p0
			});
		}

		// Token: 0x0600102D RID: 4141 RVA: 0x000423AA File Offset: 0x000405AA
		internal static string ELinq_UnsupportedNullConstant(object p0)
		{
			return EntityRes.GetString("ELinq_UnsupportedNullConstant", new object[]
			{
				p0
			});
		}

		// Token: 0x0600102E RID: 4142 RVA: 0x000423C0 File Offset: 0x000405C0
		internal static string ELinq_UnsupportedConstant(object p0)
		{
			return EntityRes.GetString("ELinq_UnsupportedConstant", new object[]
			{
				p0
			});
		}

		// Token: 0x0600102F RID: 4143 RVA: 0x000423D6 File Offset: 0x000405D6
		internal static string ELinq_UnsupportedCast(object p0, object p1)
		{
			return EntityRes.GetString("ELinq_UnsupportedCast", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06001030 RID: 4144 RVA: 0x000423F0 File Offset: 0x000405F0
		internal static string ELinq_UnsupportedIsOrAs(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ELinq_UnsupportedIsOrAs", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06001031 RID: 4145 RVA: 0x0004240E File Offset: 0x0004060E
		internal static string ELinq_UnsupportedQueryableMethod
		{
			get
			{
				return EntityRes.GetString("ELinq_UnsupportedQueryableMethod");
			}
		}

		// Token: 0x06001032 RID: 4146 RVA: 0x0004241A File Offset: 0x0004061A
		internal static string ELinq_InvalidOfTypeResult(object p0)
		{
			return EntityRes.GetString("ELinq_InvalidOfTypeResult", new object[]
			{
				p0
			});
		}

		// Token: 0x06001033 RID: 4147 RVA: 0x00042430 File Offset: 0x00040630
		internal static string ELinq_UnsupportedNominalType(object p0)
		{
			return EntityRes.GetString("ELinq_UnsupportedNominalType", new object[]
			{
				p0
			});
		}

		// Token: 0x06001034 RID: 4148 RVA: 0x00042446 File Offset: 0x00040646
		internal static string ELinq_UnsupportedEnumerableType(object p0)
		{
			return EntityRes.GetString("ELinq_UnsupportedEnumerableType", new object[]
			{
				p0
			});
		}

		// Token: 0x06001035 RID: 4149 RVA: 0x0004245C File Offset: 0x0004065C
		internal static string ELinq_UnsupportedHeterogeneousInitializers(object p0)
		{
			return EntityRes.GetString("ELinq_UnsupportedHeterogeneousInitializers", new object[]
			{
				p0
			});
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06001036 RID: 4150 RVA: 0x00042472 File Offset: 0x00040672
		internal static string ELinq_UnsupportedDifferentContexts
		{
			get
			{
				return EntityRes.GetString("ELinq_UnsupportedDifferentContexts");
			}
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06001037 RID: 4151 RVA: 0x0004247E File Offset: 0x0004067E
		internal static string ELinq_UnsupportedCastToDecimal
		{
			get
			{
				return EntityRes.GetString("ELinq_UnsupportedCastToDecimal");
			}
		}

		// Token: 0x06001038 RID: 4152 RVA: 0x0004248A File Offset: 0x0004068A
		internal static string ELinq_UnsupportedKeySelector(object p0)
		{
			return EntityRes.GetString("ELinq_UnsupportedKeySelector", new object[]
			{
				p0
			});
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06001039 RID: 4153 RVA: 0x000424A0 File Offset: 0x000406A0
		internal static string ELinq_CreateOrderedEnumerableNotSupported
		{
			get
			{
				return EntityRes.GetString("ELinq_CreateOrderedEnumerableNotSupported");
			}
		}

		// Token: 0x0600103A RID: 4154 RVA: 0x000424AC File Offset: 0x000406AC
		internal static string ELinq_UnsupportedPassthrough(object p0, object p1)
		{
			return EntityRes.GetString("ELinq_UnsupportedPassthrough", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x0600103B RID: 4155 RVA: 0x000424C6 File Offset: 0x000406C6
		internal static string ELinq_UnexpectedTypeForNavigationProperty(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("ELinq_UnexpectedTypeForNavigationProperty", new object[]
			{
				p0,
				p1,
				p2,
				p3
			});
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x0600103C RID: 4156 RVA: 0x000424E8 File Offset: 0x000406E8
		internal static string ELinq_SkipWithoutOrder
		{
			get
			{
				return EntityRes.GetString("ELinq_SkipWithoutOrder");
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x0600103D RID: 4157 RVA: 0x000424F4 File Offset: 0x000406F4
		internal static string ELinq_PropertyIndexNotSupported
		{
			get
			{
				return EntityRes.GetString("ELinq_PropertyIndexNotSupported");
			}
		}

		// Token: 0x0600103E RID: 4158 RVA: 0x00042500 File Offset: 0x00040700
		internal static string ELinq_NotPropertyOrField(object p0)
		{
			return EntityRes.GetString("ELinq_NotPropertyOrField", new object[]
			{
				p0
			});
		}

		// Token: 0x0600103F RID: 4159 RVA: 0x00042516 File Offset: 0x00040716
		internal static string ELinq_UnsupportedStringRemoveCase(object p0, object p1)
		{
			return EntityRes.GetString("ELinq_UnsupportedStringRemoveCase", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06001040 RID: 4160 RVA: 0x00042530 File Offset: 0x00040730
		internal static string ELinq_UnsupportedTrimStartTrimEndCase(object p0)
		{
			return EntityRes.GetString("ELinq_UnsupportedTrimStartTrimEndCase", new object[]
			{
				p0
			});
		}

		// Token: 0x06001041 RID: 4161 RVA: 0x00042546 File Offset: 0x00040746
		internal static string ELinq_UnsupportedVBDatePartNonConstantInterval(object p0, object p1)
		{
			return EntityRes.GetString("ELinq_UnsupportedVBDatePartNonConstantInterval", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06001042 RID: 4162 RVA: 0x00042560 File Offset: 0x00040760
		internal static string ELinq_UnsupportedVBDatePartInvalidInterval(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ELinq_UnsupportedVBDatePartInvalidInterval", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06001043 RID: 4163 RVA: 0x0004257E File Offset: 0x0004077E
		internal static string ELinq_UnsupportedAsUnicodeAndAsNonUnicode(object p0)
		{
			return EntityRes.GetString("ELinq_UnsupportedAsUnicodeAndAsNonUnicode", new object[]
			{
				p0
			});
		}

		// Token: 0x06001044 RID: 4164 RVA: 0x00042594 File Offset: 0x00040794
		internal static string ELinq_UnsupportedComparison(object p0)
		{
			return EntityRes.GetString("ELinq_UnsupportedComparison", new object[]
			{
				p0
			});
		}

		// Token: 0x06001045 RID: 4165 RVA: 0x000425AA File Offset: 0x000407AA
		internal static string ELinq_UnsupportedRefComparison(object p0, object p1)
		{
			return EntityRes.GetString("ELinq_UnsupportedRefComparison", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06001046 RID: 4166 RVA: 0x000425C4 File Offset: 0x000407C4
		internal static string ELinq_UnsupportedRowComparison(object p0)
		{
			return EntityRes.GetString("ELinq_UnsupportedRowComparison", new object[]
			{
				p0
			});
		}

		// Token: 0x06001047 RID: 4167 RVA: 0x000425DA File Offset: 0x000407DA
		internal static string ELinq_UnsupportedRowMemberComparison(object p0)
		{
			return EntityRes.GetString("ELinq_UnsupportedRowMemberComparison", new object[]
			{
				p0
			});
		}

		// Token: 0x06001048 RID: 4168 RVA: 0x000425F0 File Offset: 0x000407F0
		internal static string ELinq_UnsupportedRowTypeComparison(object p0)
		{
			return EntityRes.GetString("ELinq_UnsupportedRowTypeComparison", new object[]
			{
				p0
			});
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06001049 RID: 4169 RVA: 0x00042606 File Offset: 0x00040806
		internal static string ELinq_AnonymousType
		{
			get
			{
				return EntityRes.GetString("ELinq_AnonymousType");
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x0600104A RID: 4170 RVA: 0x00042612 File Offset: 0x00040812
		internal static string ELinq_ClosureType
		{
			get
			{
				return EntityRes.GetString("ELinq_ClosureType");
			}
		}

		// Token: 0x0600104B RID: 4171 RVA: 0x0004261E File Offset: 0x0004081E
		internal static string ELinq_UnhandledExpressionType(object p0)
		{
			return EntityRes.GetString("ELinq_UnhandledExpressionType", new object[]
			{
				p0
			});
		}

		// Token: 0x0600104C RID: 4172 RVA: 0x00042634 File Offset: 0x00040834
		internal static string ELinq_UnhandledBindingType(object p0)
		{
			return EntityRes.GetString("ELinq_UnhandledBindingType", new object[]
			{
				p0
			});
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x0600104D RID: 4173 RVA: 0x0004264A File Offset: 0x0004084A
		internal static string ELinq_UnsupportedNestedFirst
		{
			get
			{
				return EntityRes.GetString("ELinq_UnsupportedNestedFirst");
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x0600104E RID: 4174 RVA: 0x00042656 File Offset: 0x00040856
		internal static string ELinq_UnsupportedNestedSingle
		{
			get
			{
				return EntityRes.GetString("ELinq_UnsupportedNestedSingle");
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x0600104F RID: 4175 RVA: 0x00042662 File Offset: 0x00040862
		internal static string ELinq_UnsupportedInclude
		{
			get
			{
				return EntityRes.GetString("ELinq_UnsupportedInclude");
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06001050 RID: 4176 RVA: 0x0004266E File Offset: 0x0004086E
		internal static string ELinq_UnsupportedMergeAs
		{
			get
			{
				return EntityRes.GetString("ELinq_UnsupportedMergeAs");
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06001051 RID: 4177 RVA: 0x0004267A File Offset: 0x0004087A
		internal static string ELinq_MethodNotDirectlyCallable
		{
			get
			{
				return EntityRes.GetString("ELinq_MethodNotDirectlyCallable");
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06001052 RID: 4178 RVA: 0x00042686 File Offset: 0x00040886
		internal static string ELinq_CycleDetected
		{
			get
			{
				return EntityRes.GetString("ELinq_CycleDetected");
			}
		}

		// Token: 0x06001053 RID: 4179 RVA: 0x00042692 File Offset: 0x00040892
		internal static string ELinq_EdmFunctionAttributeParameterNameNotValid(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ELinq_EdmFunctionAttributeParameterNameNotValid", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06001054 RID: 4180 RVA: 0x000426B0 File Offset: 0x000408B0
		internal static string ELinq_EdmFunctionAttributedFunctionWithWrongReturnType(object p0, object p1)
		{
			return EntityRes.GetString("ELinq_EdmFunctionAttributedFunctionWithWrongReturnType", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06001055 RID: 4181 RVA: 0x000426CA File Offset: 0x000408CA
		internal static string ELinq_EdmFunctionDirectCall
		{
			get
			{
				return EntityRes.GetString("ELinq_EdmFunctionDirectCall");
			}
		}

		// Token: 0x06001056 RID: 4182 RVA: 0x000426D6 File Offset: 0x000408D6
		internal static string CompiledELinq_UnsupportedParameterTypes(object p0)
		{
			return EntityRes.GetString("CompiledELinq_UnsupportedParameterTypes", new object[]
			{
				p0
			});
		}

		// Token: 0x06001057 RID: 4183 RVA: 0x000426EC File Offset: 0x000408EC
		internal static string CompiledELinq_UnsupportedNamedParameterType(object p0, object p1)
		{
			return EntityRes.GetString("CompiledELinq_UnsupportedNamedParameterType", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06001058 RID: 4184 RVA: 0x00042706 File Offset: 0x00040906
		internal static string CompiledELinq_UnsupportedNamedParameterUseAsType(object p0, object p1)
		{
			return EntityRes.GetString("CompiledELinq_UnsupportedNamedParameterUseAsType", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06001059 RID: 4185 RVA: 0x00042720 File Offset: 0x00040920
		internal static string Update_UnsupportedExpressionKind(object p0, object p1)
		{
			return EntityRes.GetString("Update_UnsupportedExpressionKind", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x0600105A RID: 4186 RVA: 0x0004273A File Offset: 0x0004093A
		internal static string Update_UnsupportedCastArgument(object p0)
		{
			return EntityRes.GetString("Update_UnsupportedCastArgument", new object[]
			{
				p0
			});
		}

		// Token: 0x0600105B RID: 4187 RVA: 0x00042750 File Offset: 0x00040950
		internal static string Update_UnsupportedExtentType(object p0, object p1)
		{
			return EntityRes.GetString("Update_UnsupportedExtentType", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x0600105C RID: 4188 RVA: 0x0004276A File Offset: 0x0004096A
		internal static string Update_ConstraintCycle
		{
			get
			{
				return EntityRes.GetString("Update_ConstraintCycle");
			}
		}

		// Token: 0x0600105D RID: 4189 RVA: 0x00042776 File Offset: 0x00040976
		internal static string Update_UnsupportedJoinType(object p0)
		{
			return EntityRes.GetString("Update_UnsupportedJoinType", new object[]
			{
				p0
			});
		}

		// Token: 0x0600105E RID: 4190 RVA: 0x0004278C File Offset: 0x0004098C
		internal static string Update_UnsupportedProjection(object p0)
		{
			return EntityRes.GetString("Update_UnsupportedProjection", new object[]
			{
				p0
			});
		}

		// Token: 0x0600105F RID: 4191 RVA: 0x000427A2 File Offset: 0x000409A2
		internal static string Update_ConcurrencyError(object p0)
		{
			return EntityRes.GetString("Update_ConcurrencyError", new object[]
			{
				p0
			});
		}

		// Token: 0x06001060 RID: 4192 RVA: 0x000427B8 File Offset: 0x000409B8
		internal static string Update_MissingEntity(object p0, object p1)
		{
			return EntityRes.GetString("Update_MissingEntity", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06001061 RID: 4193 RVA: 0x000427D2 File Offset: 0x000409D2
		internal static string Update_RelationshipCardinalityConstraintViolation(object p0, object p1, object p2, object p3, object p4, object p5)
		{
			return EntityRes.GetString("Update_RelationshipCardinalityConstraintViolation", new object[]
			{
				p0,
				p1,
				p2,
				p3,
				p4,
				p5
			});
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06001062 RID: 4194 RVA: 0x000427FE File Offset: 0x000409FE
		internal static string Update_GeneralExecutionException
		{
			get
			{
				return EntityRes.GetString("Update_GeneralExecutionException");
			}
		}

		// Token: 0x06001063 RID: 4195 RVA: 0x0004280A File Offset: 0x00040A0A
		internal static string Update_MissingRequiredEntity(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Update_MissingRequiredEntity", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06001064 RID: 4196 RVA: 0x00042828 File Offset: 0x00040A28
		internal static string Update_RelationshipCardinalityViolation(object p0, object p1, object p2, object p3, object p4, object p5)
		{
			return EntityRes.GetString("Update_RelationshipCardinalityViolation", new object[]
			{
				p0,
				p1,
				p2,
				p3,
				p4,
				p5
			});
		}

		// Token: 0x06001065 RID: 4197 RVA: 0x00042854 File Offset: 0x00040A54
		internal static string Update_NotSupportedServerGenKey(object p0)
		{
			return EntityRes.GetString("Update_NotSupportedServerGenKey", new object[]
			{
				p0
			});
		}

		// Token: 0x06001066 RID: 4198 RVA: 0x0004286A File Offset: 0x00040A6A
		internal static string Update_NotSupportedIdentityType(object p0, object p1)
		{
			return EntityRes.GetString("Update_NotSupportedIdentityType", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06001067 RID: 4199 RVA: 0x00042884 File Offset: 0x00040A84
		internal static string Update_NotSupportedComputedKeyColumn(object p0, object p1, object p2, object p3, object p4)
		{
			return EntityRes.GetString("Update_NotSupportedComputedKeyColumn", new object[]
			{
				p0,
				p1,
				p2,
				p3,
				p4
			});
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06001068 RID: 4200 RVA: 0x000428AB File Offset: 0x00040AAB
		internal static string Update_AmbiguousServerGenIdentifier
		{
			get
			{
				return EntityRes.GetString("Update_AmbiguousServerGenIdentifier");
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06001069 RID: 4201 RVA: 0x000428B7 File Offset: 0x00040AB7
		internal static string Update_WorkspaceMismatch
		{
			get
			{
				return EntityRes.GetString("Update_WorkspaceMismatch");
			}
		}

		// Token: 0x0600106A RID: 4202 RVA: 0x000428C3 File Offset: 0x00040AC3
		internal static string Update_MissingRequiredRelationshipValue(object p0, object p1)
		{
			return EntityRes.GetString("Update_MissingRequiredRelationshipValue", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x0600106B RID: 4203 RVA: 0x000428DD File Offset: 0x00040ADD
		internal static string Update_MissingResultColumn(object p0)
		{
			return EntityRes.GetString("Update_MissingResultColumn", new object[]
			{
				p0
			});
		}

		// Token: 0x0600106C RID: 4204 RVA: 0x000428F3 File Offset: 0x00040AF3
		internal static string Update_NullReturnValueForNonNullableMember(object p0, object p1)
		{
			return EntityRes.GetString("Update_NullReturnValueForNonNullableMember", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x0600106D RID: 4205 RVA: 0x0004290D File Offset: 0x00040B0D
		internal static string Update_ReturnValueHasUnexpectedType(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("Update_ReturnValueHasUnexpectedType", new object[]
			{
				p0,
				p1,
				p2,
				p3
			});
		}

		// Token: 0x0600106E RID: 4206 RVA: 0x0004292F File Offset: 0x00040B2F
		internal static string Update_SqlEntitySetWithoutDmlFunctions(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Update_SqlEntitySetWithoutDmlFunctions", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x0600106F RID: 4207 RVA: 0x0004294D File Offset: 0x00040B4D
		internal static string Update_UnableToConvertRowsAffectedParameterToInt32(object p0, object p1)
		{
			return EntityRes.GetString("Update_UnableToConvertRowsAffectedParameterToInt32", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06001070 RID: 4208 RVA: 0x00042967 File Offset: 0x00040B67
		internal static string Update_MappingNotFound(object p0)
		{
			return EntityRes.GetString("Update_MappingNotFound", new object[]
			{
				p0
			});
		}

		// Token: 0x06001071 RID: 4209 RVA: 0x0004297D File Offset: 0x00040B7D
		internal static string Update_ModifyingIdentityColumn(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Update_ModifyingIdentityColumn", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06001072 RID: 4210 RVA: 0x0004299B File Offset: 0x00040B9B
		internal static string Update_GeneratedDependent(object p0)
		{
			return EntityRes.GetString("Update_GeneratedDependent", new object[]
			{
				p0
			});
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06001073 RID: 4211 RVA: 0x000429B1 File Offset: 0x00040BB1
		internal static string Update_ReferentialConstraintIntegrityViolation
		{
			get
			{
				return EntityRes.GetString("Update_ReferentialConstraintIntegrityViolation");
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06001074 RID: 4212 RVA: 0x000429BD File Offset: 0x00040BBD
		internal static string Update_ErrorLoadingRecord
		{
			get
			{
				return EntityRes.GetString("Update_ErrorLoadingRecord");
			}
		}

		// Token: 0x06001075 RID: 4213 RVA: 0x000429C9 File Offset: 0x00040BC9
		internal static string Update_NullValue(object p0)
		{
			return EntityRes.GetString("Update_NullValue", new object[]
			{
				p0
			});
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06001076 RID: 4214 RVA: 0x000429DF File Offset: 0x00040BDF
		internal static string Update_CircularRelationships
		{
			get
			{
				return EntityRes.GetString("Update_CircularRelationships");
			}
		}

		// Token: 0x06001077 RID: 4215 RVA: 0x000429EB File Offset: 0x00040BEB
		internal static string Update_RelationshipCardinalityConstraintViolationSingleValue(object p0, object p1, object p2, object p3, object p4)
		{
			return EntityRes.GetString("Update_RelationshipCardinalityConstraintViolationSingleValue", new object[]
			{
				p0,
				p1,
				p2,
				p3,
				p4
			});
		}

		// Token: 0x06001078 RID: 4216 RVA: 0x00042A12 File Offset: 0x00040C12
		internal static string Update_MissingFunctionMapping(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Update_MissingFunctionMapping", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06001079 RID: 4217 RVA: 0x00042A30 File Offset: 0x00040C30
		internal static string Update_InvalidChanges
		{
			get
			{
				return EntityRes.GetString("Update_InvalidChanges");
			}
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x0600107A RID: 4218 RVA: 0x00042A3C File Offset: 0x00040C3C
		internal static string Update_DuplicateKeys
		{
			get
			{
				return EntityRes.GetString("Update_DuplicateKeys");
			}
		}

		// Token: 0x0600107B RID: 4219 RVA: 0x00042A48 File Offset: 0x00040C48
		internal static string Update_AmbiguousForeignKey(object p0)
		{
			return EntityRes.GetString("Update_AmbiguousForeignKey", new object[]
			{
				p0
			});
		}

		// Token: 0x0600107C RID: 4220 RVA: 0x00042A5E File Offset: 0x00040C5E
		internal static string Update_InsertingOrUpdatingReferenceToDeletedEntity(object p0)
		{
			return EntityRes.GetString("Update_InsertingOrUpdatingReferenceToDeletedEntity", new object[]
			{
				p0
			});
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x0600107D RID: 4221 RVA: 0x00042A74 File Offset: 0x00040C74
		internal static string ViewGen_Extent
		{
			get
			{
				return EntityRes.GetString("ViewGen_Extent");
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x0600107E RID: 4222 RVA: 0x00042A80 File Offset: 0x00040C80
		internal static string ViewGen_Null
		{
			get
			{
				return EntityRes.GetString("ViewGen_Null");
			}
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x0600107F RID: 4223 RVA: 0x00042A8C File Offset: 0x00040C8C
		internal static string ViewGen_CommaBlank
		{
			get
			{
				return EntityRes.GetString("ViewGen_CommaBlank");
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06001080 RID: 4224 RVA: 0x00042A98 File Offset: 0x00040C98
		internal static string ViewGen_Entities
		{
			get
			{
				return EntityRes.GetString("ViewGen_Entities");
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06001081 RID: 4225 RVA: 0x00042AA4 File Offset: 0x00040CA4
		internal static string ViewGen_Tuples
		{
			get
			{
				return EntityRes.GetString("ViewGen_Tuples");
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06001082 RID: 4226 RVA: 0x00042AB0 File Offset: 0x00040CB0
		internal static string ViewGen_NotNull
		{
			get
			{
				return EntityRes.GetString("ViewGen_NotNull");
			}
		}

		// Token: 0x06001083 RID: 4227 RVA: 0x00042ABC File Offset: 0x00040CBC
		internal static string ViewGen_NegatedCellConstant(object p0)
		{
			return EntityRes.GetString("ViewGen_NegatedCellConstant", new object[]
			{
				p0
			});
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06001084 RID: 4228 RVA: 0x00042AD2 File Offset: 0x00040CD2
		internal static string ViewGen_Error
		{
			get
			{
				return EntityRes.GetString("ViewGen_Error");
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06001085 RID: 4229 RVA: 0x00042ADE File Offset: 0x00040CDE
		internal static string ViewGen_AND
		{
			get
			{
				return EntityRes.GetString("ViewGen_AND");
			}
		}

		// Token: 0x06001086 RID: 4230 RVA: 0x00042AEA File Offset: 0x00040CEA
		internal static string Viewgen_CannotGenerateQueryViewUnderNoValidation(object p0)
		{
			return EntityRes.GetString("Viewgen_CannotGenerateQueryViewUnderNoValidation", new object[]
			{
				p0
			});
		}

		// Token: 0x06001087 RID: 4231 RVA: 0x00042B00 File Offset: 0x00040D00
		internal static string ViewGen_Missing_Sets_Mapping(object p0)
		{
			return EntityRes.GetString("ViewGen_Missing_Sets_Mapping", new object[]
			{
				p0
			});
		}

		// Token: 0x06001088 RID: 4232 RVA: 0x00042B16 File Offset: 0x00040D16
		internal static string ViewGen_Missing_Type_Mapping(object p0)
		{
			return EntityRes.GetString("ViewGen_Missing_Type_Mapping", new object[]
			{
				p0
			});
		}

		// Token: 0x06001089 RID: 4233 RVA: 0x00042B2C File Offset: 0x00040D2C
		internal static string ViewGen_Missing_Set_Mapping(object p0)
		{
			return EntityRes.GetString("ViewGen_Missing_Set_Mapping", new object[]
			{
				p0
			});
		}

		// Token: 0x0600108A RID: 4234 RVA: 0x00042B42 File Offset: 0x00040D42
		internal static string ViewGen_Concurrency_Derived_Class(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ViewGen_Concurrency_Derived_Class", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x0600108B RID: 4235 RVA: 0x00042B60 File Offset: 0x00040D60
		internal static string ViewGen_Concurrency_Invalid_Condition(object p0, object p1)
		{
			return EntityRes.GetString("ViewGen_Concurrency_Invalid_Condition", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x0600108C RID: 4236 RVA: 0x00042B7A File Offset: 0x00040D7A
		internal static string ViewGen_TableKey_Missing(object p0, object p1)
		{
			return EntityRes.GetString("ViewGen_TableKey_Missing", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x0600108D RID: 4237 RVA: 0x00042B94 File Offset: 0x00040D94
		internal static string ViewGen_EntitySetKey_Missing(object p0, object p1)
		{
			return EntityRes.GetString("ViewGen_EntitySetKey_Missing", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x0600108E RID: 4238 RVA: 0x00042BAE File Offset: 0x00040DAE
		internal static string ViewGen_AssociationSetKey_Missing(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ViewGen_AssociationSetKey_Missing", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x0600108F RID: 4239 RVA: 0x00042BCC File Offset: 0x00040DCC
		internal static string ViewGen_Cannot_Recover_Attributes(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ViewGen_Cannot_Recover_Attributes", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06001090 RID: 4240 RVA: 0x00042BEA File Offset: 0x00040DEA
		internal static string ViewGen_Cannot_Recover_Types(object p0, object p1)
		{
			return EntityRes.GetString("ViewGen_Cannot_Recover_Types", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06001091 RID: 4241 RVA: 0x00042C04 File Offset: 0x00040E04
		internal static string ViewGen_Cannot_Disambiguate_MultiConstant(object p0, object p1)
		{
			return EntityRes.GetString("ViewGen_Cannot_Disambiguate_MultiConstant", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06001092 RID: 4242 RVA: 0x00042C1E File Offset: 0x00040E1E
		internal static string ViewGen_No_Default_Value(object p0, object p1)
		{
			return EntityRes.GetString("ViewGen_No_Default_Value", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06001093 RID: 4243 RVA: 0x00042C38 File Offset: 0x00040E38
		internal static string ViewGen_No_Default_Value_For_Configuration(object p0)
		{
			return EntityRes.GetString("ViewGen_No_Default_Value_For_Configuration", new object[]
			{
				p0
			});
		}

		// Token: 0x06001094 RID: 4244 RVA: 0x00042C4E File Offset: 0x00040E4E
		internal static string ViewGen_KeyConstraint_Violation(object p0, object p1, object p2, object p3, object p4, object p5)
		{
			return EntityRes.GetString("ViewGen_KeyConstraint_Violation", new object[]
			{
				p0,
				p1,
				p2,
				p3,
				p4,
				p5
			});
		}

		// Token: 0x06001095 RID: 4245 RVA: 0x00042C7A File Offset: 0x00040E7A
		internal static string ViewGen_KeyConstraint_Update_Violation_EntitySet(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("ViewGen_KeyConstraint_Update_Violation_EntitySet", new object[]
			{
				p0,
				p1,
				p2,
				p3
			});
		}

		// Token: 0x06001096 RID: 4246 RVA: 0x00042C9C File Offset: 0x00040E9C
		internal static string ViewGen_KeyConstraint_Update_Violation_AssociationSet(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ViewGen_KeyConstraint_Update_Violation_AssociationSet", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06001097 RID: 4247 RVA: 0x00042CBA File Offset: 0x00040EBA
		internal static string ViewGen_AssociationEndShouldBeMappedToKey(object p0, object p1)
		{
			return EntityRes.GetString("ViewGen_AssociationEndShouldBeMappedToKey", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06001098 RID: 4248 RVA: 0x00042CD4 File Offset: 0x00040ED4
		internal static string ViewGen_Duplicate_CProperties(object p0)
		{
			return EntityRes.GetString("ViewGen_Duplicate_CProperties", new object[]
			{
				p0
			});
		}

		// Token: 0x06001099 RID: 4249 RVA: 0x00042CEA File Offset: 0x00040EEA
		internal static string ViewGen_Duplicate_CProperties_IsMapped(object p0, object p1)
		{
			return EntityRes.GetString("ViewGen_Duplicate_CProperties_IsMapped", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x0600109A RID: 4250 RVA: 0x00042D04 File Offset: 0x00040F04
		internal static string ViewGen_NotNull_No_Projected_Slot(object p0)
		{
			return EntityRes.GetString("ViewGen_NotNull_No_Projected_Slot", new object[]
			{
				p0
			});
		}

		// Token: 0x0600109B RID: 4251 RVA: 0x00042D1A File Offset: 0x00040F1A
		internal static string ViewGen_InvalidCondition(object p0)
		{
			return EntityRes.GetString("ViewGen_InvalidCondition", new object[]
			{
				p0
			});
		}

		// Token: 0x0600109C RID: 4252 RVA: 0x00042D30 File Offset: 0x00040F30
		internal static string ViewGen_NonKeyProjectedWithOverlappingPartitions(object p0)
		{
			return EntityRes.GetString("ViewGen_NonKeyProjectedWithOverlappingPartitions", new object[]
			{
				p0
			});
		}

		// Token: 0x0600109D RID: 4253 RVA: 0x00042D46 File Offset: 0x00040F46
		internal static string ViewGen_CQ_PartitionConstraint(object p0)
		{
			return EntityRes.GetString("ViewGen_CQ_PartitionConstraint", new object[]
			{
				p0
			});
		}

		// Token: 0x0600109E RID: 4254 RVA: 0x00042D5C File Offset: 0x00040F5C
		internal static string ViewGen_CQ_DomainConstraint(object p0)
		{
			return EntityRes.GetString("ViewGen_CQ_DomainConstraint", new object[]
			{
				p0
			});
		}

		// Token: 0x0600109F RID: 4255 RVA: 0x00042D72 File Offset: 0x00040F72
		internal static string ViewGen_OneOfConst_MustBeNonNullable(object p0)
		{
			return EntityRes.GetString("ViewGen_OneOfConst_MustBeNonNullable", new object[]
			{
				p0
			});
		}

		// Token: 0x060010A0 RID: 4256 RVA: 0x00042D88 File Offset: 0x00040F88
		internal static string ViewGen_OneOfConst_MustBeNull(object p0)
		{
			return EntityRes.GetString("ViewGen_OneOfConst_MustBeNull", new object[]
			{
				p0
			});
		}

		// Token: 0x060010A1 RID: 4257 RVA: 0x00042D9E File Offset: 0x00040F9E
		internal static string ViewGen_OneOfConst_MustBeEqualTo(object p0, object p1)
		{
			return EntityRes.GetString("ViewGen_OneOfConst_MustBeEqualTo", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060010A2 RID: 4258 RVA: 0x00042DB8 File Offset: 0x00040FB8
		internal static string ViewGen_OneOfConst_MustNotBeEqualTo(object p0, object p1)
		{
			return EntityRes.GetString("ViewGen_OneOfConst_MustNotBeEqualTo", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060010A3 RID: 4259 RVA: 0x00042DD2 File Offset: 0x00040FD2
		internal static string ViewGen_OneOfConst_MustBeOneOf(object p0, object p1)
		{
			return EntityRes.GetString("ViewGen_OneOfConst_MustBeOneOf", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060010A4 RID: 4260 RVA: 0x00042DEC File Offset: 0x00040FEC
		internal static string ViewGen_OneOfConst_MustNotBeOneOf(object p0, object p1)
		{
			return EntityRes.GetString("ViewGen_OneOfConst_MustNotBeOneOf", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060010A5 RID: 4261 RVA: 0x00042E06 File Offset: 0x00041006
		internal static string ViewGen_OneOfConst_IsNonNullable(object p0)
		{
			return EntityRes.GetString("ViewGen_OneOfConst_IsNonNullable", new object[]
			{
				p0
			});
		}

		// Token: 0x060010A6 RID: 4262 RVA: 0x00042E1C File Offset: 0x0004101C
		internal static string ViewGen_OneOfConst_IsEqualTo(object p0, object p1)
		{
			return EntityRes.GetString("ViewGen_OneOfConst_IsEqualTo", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060010A7 RID: 4263 RVA: 0x00042E36 File Offset: 0x00041036
		internal static string ViewGen_OneOfConst_IsNotEqualTo(object p0, object p1)
		{
			return EntityRes.GetString("ViewGen_OneOfConst_IsNotEqualTo", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060010A8 RID: 4264 RVA: 0x00042E50 File Offset: 0x00041050
		internal static string ViewGen_OneOfConst_IsOneOf(object p0, object p1)
		{
			return EntityRes.GetString("ViewGen_OneOfConst_IsOneOf", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060010A9 RID: 4265 RVA: 0x00042E6A File Offset: 0x0004106A
		internal static string ViewGen_OneOfConst_IsNotOneOf(object p0, object p1)
		{
			return EntityRes.GetString("ViewGen_OneOfConst_IsNotOneOf", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060010AA RID: 4266 RVA: 0x00042E84 File Offset: 0x00041084
		internal static string ViewGen_OneOfConst_IsOneOfTypes(object p0)
		{
			return EntityRes.GetString("ViewGen_OneOfConst_IsOneOfTypes", new object[]
			{
				p0
			});
		}

		// Token: 0x060010AB RID: 4267 RVA: 0x00042E9A File Offset: 0x0004109A
		internal static string ViewGen_ErrorLog(object p0)
		{
			return EntityRes.GetString("ViewGen_ErrorLog", new object[]
			{
				p0
			});
		}

		// Token: 0x060010AC RID: 4268 RVA: 0x00042EB0 File Offset: 0x000410B0
		internal static string ViewGen_ErrorLog2(object p0)
		{
			return EntityRes.GetString("ViewGen_ErrorLog2", new object[]
			{
				p0
			});
		}

		// Token: 0x060010AD RID: 4269 RVA: 0x00042EC6 File Offset: 0x000410C6
		internal static string ViewGen_Foreign_Key_Missing_Table_Mapping(object p0, object p1)
		{
			return EntityRes.GetString("ViewGen_Foreign_Key_Missing_Table_Mapping", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060010AE RID: 4270 RVA: 0x00042EE0 File Offset: 0x000410E0
		internal static string ViewGen_Foreign_Key_ParentTable_NotMappedToEnd(object p0, object p1, object p2, object p3, object p4, object p5)
		{
			return EntityRes.GetString("ViewGen_Foreign_Key_ParentTable_NotMappedToEnd", new object[]
			{
				p0,
				p1,
				p2,
				p3,
				p4,
				p5
			});
		}

		// Token: 0x060010AF RID: 4271 RVA: 0x00042F0C File Offset: 0x0004110C
		internal static string ViewGen_Foreign_Key(object p0, object p1, object p2, object p3, object p4)
		{
			return EntityRes.GetString("ViewGen_Foreign_Key", new object[]
			{
				p0,
				p1,
				p2,
				p3,
				p4
			});
		}

		// Token: 0x060010B0 RID: 4272 RVA: 0x00042F33 File Offset: 0x00041133
		internal static string ViewGen_Foreign_Key_UpperBound_MustBeOne(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ViewGen_Foreign_Key_UpperBound_MustBeOne", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x060010B1 RID: 4273 RVA: 0x00042F51 File Offset: 0x00041151
		internal static string ViewGen_Foreign_Key_LowerBound_MustBeOne(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ViewGen_Foreign_Key_LowerBound_MustBeOne", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x060010B2 RID: 4274 RVA: 0x00042F6F File Offset: 0x0004116F
		internal static string ViewGen_Foreign_Key_Missing_Relationship_Mapping(object p0)
		{
			return EntityRes.GetString("ViewGen_Foreign_Key_Missing_Relationship_Mapping", new object[]
			{
				p0
			});
		}

		// Token: 0x060010B3 RID: 4275 RVA: 0x00042F85 File Offset: 0x00041185
		internal static string ViewGen_Foreign_Key_Not_Guaranteed_InCSpace(object p0)
		{
			return EntityRes.GetString("ViewGen_Foreign_Key_Not_Guaranteed_InCSpace", new object[]
			{
				p0
			});
		}

		// Token: 0x060010B4 RID: 4276 RVA: 0x00042F9B File Offset: 0x0004119B
		internal static string ViewGen_Foreign_Key_ColumnOrder_Incorrect(object p0, object p1, object p2, object p3, object p4, object p5, object p6)
		{
			return EntityRes.GetString("ViewGen_Foreign_Key_ColumnOrder_Incorrect", new object[]
			{
				p0,
				p1,
				p2,
				p3,
				p4,
				p5,
				p6
			});
		}

		// Token: 0x060010B5 RID: 4277 RVA: 0x00042FCC File Offset: 0x000411CC
		internal static string ViewGen_AssociationSet_AsUserString(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ViewGen_AssociationSet_AsUserString", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x060010B6 RID: 4278 RVA: 0x00042FEA File Offset: 0x000411EA
		internal static string ViewGen_AssociationSet_AsUserString_Negated(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ViewGen_AssociationSet_AsUserString_Negated", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x060010B7 RID: 4279 RVA: 0x00043008 File Offset: 0x00041208
		internal static string ViewGen_EntitySet_AsUserString(object p0, object p1)
		{
			return EntityRes.GetString("ViewGen_EntitySet_AsUserString", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060010B8 RID: 4280 RVA: 0x00043022 File Offset: 0x00041222
		internal static string ViewGen_EntitySet_AsUserString_Negated(object p0, object p1)
		{
			return EntityRes.GetString("ViewGen_EntitySet_AsUserString_Negated", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x060010B9 RID: 4281 RVA: 0x0004303C File Offset: 0x0004123C
		internal static string ViewGen_EntityInstanceToken
		{
			get
			{
				return EntityRes.GetString("ViewGen_EntityInstanceToken");
			}
		}

		// Token: 0x060010BA RID: 4282 RVA: 0x00043048 File Offset: 0x00041248
		internal static string Viewgen_ConfigurationErrorMsg(object p0)
		{
			return EntityRes.GetString("Viewgen_ConfigurationErrorMsg", new object[]
			{
				p0
			});
		}

		// Token: 0x060010BB RID: 4283 RVA: 0x0004305E File Offset: 0x0004125E
		internal static string ViewGen_HashOnMappingClosure_Not_Matching(object p0)
		{
			return EntityRes.GetString("ViewGen_HashOnMappingClosure_Not_Matching", new object[]
			{
				p0
			});
		}

		// Token: 0x060010BC RID: 4284 RVA: 0x00043074 File Offset: 0x00041274
		internal static string Viewgen_RightSideNotDisjoint(object p0)
		{
			return EntityRes.GetString("Viewgen_RightSideNotDisjoint", new object[]
			{
				p0
			});
		}

		// Token: 0x060010BD RID: 4285 RVA: 0x0004308A File Offset: 0x0004128A
		internal static string Viewgen_QV_RewritingNotFound(object p0)
		{
			return EntityRes.GetString("Viewgen_QV_RewritingNotFound", new object[]
			{
				p0
			});
		}

		// Token: 0x060010BE RID: 4286 RVA: 0x000430A0 File Offset: 0x000412A0
		internal static string Viewgen_NullableMappingForNonNullableColumn(object p0, object p1)
		{
			return EntityRes.GetString("Viewgen_NullableMappingForNonNullableColumn", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060010BF RID: 4287 RVA: 0x000430BA File Offset: 0x000412BA
		internal static string Viewgen_ErrorPattern_ConditionMemberIsMapped(object p0)
		{
			return EntityRes.GetString("Viewgen_ErrorPattern_ConditionMemberIsMapped", new object[]
			{
				p0
			});
		}

		// Token: 0x060010C0 RID: 4288 RVA: 0x000430D0 File Offset: 0x000412D0
		internal static string Viewgen_ErrorPattern_DuplicateConditionValue(object p0)
		{
			return EntityRes.GetString("Viewgen_ErrorPattern_DuplicateConditionValue", new object[]
			{
				p0
			});
		}

		// Token: 0x060010C1 RID: 4289 RVA: 0x000430E6 File Offset: 0x000412E6
		internal static string Viewgen_ErrorPattern_TableMappedToMultipleES(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Viewgen_ErrorPattern_TableMappedToMultipleES", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x060010C2 RID: 4290 RVA: 0x00043104 File Offset: 0x00041304
		internal static string Viewgen_ErrorPattern_Partition_Disj_Eq
		{
			get
			{
				return EntityRes.GetString("Viewgen_ErrorPattern_Partition_Disj_Eq");
			}
		}

		// Token: 0x060010C3 RID: 4291 RVA: 0x00043110 File Offset: 0x00041310
		internal static string Viewgen_ErrorPattern_NotNullConditionMappedToNullableMember(object p0, object p1)
		{
			return EntityRes.GetString("Viewgen_ErrorPattern_NotNullConditionMappedToNullableMember", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060010C4 RID: 4292 RVA: 0x0004312A File Offset: 0x0004132A
		internal static string Viewgen_ErrorPattern_Partition_MultipleTypesMappedToSameTable_WithoutCondition(object p0, object p1)
		{
			return EntityRes.GetString("Viewgen_ErrorPattern_Partition_MultipleTypesMappedToSameTable_WithoutCondition", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x060010C5 RID: 4293 RVA: 0x00043144 File Offset: 0x00041344
		internal static string Viewgen_ErrorPattern_Partition_Disj_Subs_Ref
		{
			get
			{
				return EntityRes.GetString("Viewgen_ErrorPattern_Partition_Disj_Subs_Ref");
			}
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x060010C6 RID: 4294 RVA: 0x00043150 File Offset: 0x00041350
		internal static string Viewgen_ErrorPattern_Partition_Disj_Subs
		{
			get
			{
				return EntityRes.GetString("Viewgen_ErrorPattern_Partition_Disj_Subs");
			}
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x060010C7 RID: 4295 RVA: 0x0004315C File Offset: 0x0004135C
		internal static string Viewgen_ErrorPattern_Partition_Disj_Unk
		{
			get
			{
				return EntityRes.GetString("Viewgen_ErrorPattern_Partition_Disj_Unk");
			}
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x060010C8 RID: 4296 RVA: 0x00043168 File Offset: 0x00041368
		internal static string Viewgen_ErrorPattern_Partition_Eq_Disj
		{
			get
			{
				return EntityRes.GetString("Viewgen_ErrorPattern_Partition_Eq_Disj");
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x060010C9 RID: 4297 RVA: 0x00043174 File Offset: 0x00041374
		internal static string Viewgen_ErrorPattern_Partition_Eq_Subs_Ref
		{
			get
			{
				return EntityRes.GetString("Viewgen_ErrorPattern_Partition_Eq_Subs_Ref");
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x060010CA RID: 4298 RVA: 0x00043180 File Offset: 0x00041380
		internal static string Viewgen_ErrorPattern_Partition_Eq_Subs
		{
			get
			{
				return EntityRes.GetString("Viewgen_ErrorPattern_Partition_Eq_Subs");
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x060010CB RID: 4299 RVA: 0x0004318C File Offset: 0x0004138C
		internal static string Viewgen_ErrorPattern_Partition_Eq_Unk
		{
			get
			{
				return EntityRes.GetString("Viewgen_ErrorPattern_Partition_Eq_Unk");
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x060010CC RID: 4300 RVA: 0x00043198 File Offset: 0x00041398
		internal static string Viewgen_ErrorPattern_Partition_Eq_Unk_Association
		{
			get
			{
				return EntityRes.GetString("Viewgen_ErrorPattern_Partition_Eq_Unk_Association");
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x060010CD RID: 4301 RVA: 0x000431A4 File Offset: 0x000413A4
		internal static string Viewgen_ErrorPattern_Partition_Sub_Disj
		{
			get
			{
				return EntityRes.GetString("Viewgen_ErrorPattern_Partition_Sub_Disj");
			}
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x060010CE RID: 4302 RVA: 0x000431B0 File Offset: 0x000413B0
		internal static string Viewgen_ErrorPattern_Partition_Sub_Eq
		{
			get
			{
				return EntityRes.GetString("Viewgen_ErrorPattern_Partition_Sub_Eq");
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x060010CF RID: 4303 RVA: 0x000431BC File Offset: 0x000413BC
		internal static string Viewgen_ErrorPattern_Partition_Sub_Eq_Ref
		{
			get
			{
				return EntityRes.GetString("Viewgen_ErrorPattern_Partition_Sub_Eq_Ref");
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x060010D0 RID: 4304 RVA: 0x000431C8 File Offset: 0x000413C8
		internal static string Viewgen_ErrorPattern_Partition_Sub_Unk
		{
			get
			{
				return EntityRes.GetString("Viewgen_ErrorPattern_Partition_Sub_Unk");
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x060010D1 RID: 4305 RVA: 0x000431D4 File Offset: 0x000413D4
		internal static string Viewgen_NoJoinKeyOrFK
		{
			get
			{
				return EntityRes.GetString("Viewgen_NoJoinKeyOrFK");
			}
		}

		// Token: 0x060010D2 RID: 4306 RVA: 0x000431E0 File Offset: 0x000413E0
		internal static string Viewgen_MultipleFragmentsBetweenCandSExtentWithDistinct(object p0, object p1)
		{
			return EntityRes.GetString("Viewgen_MultipleFragmentsBetweenCandSExtentWithDistinct", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x060010D3 RID: 4307 RVA: 0x000431FA File Offset: 0x000413FA
		internal static string Validator_EmptyIdentity
		{
			get
			{
				return EntityRes.GetString("Validator_EmptyIdentity");
			}
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x060010D4 RID: 4308 RVA: 0x00043206 File Offset: 0x00041406
		internal static string Validator_CollectionHasNoTypeUsage
		{
			get
			{
				return EntityRes.GetString("Validator_CollectionHasNoTypeUsage");
			}
		}

		// Token: 0x060010D5 RID: 4309 RVA: 0x00043212 File Offset: 0x00041412
		internal static string Validator_NoKeyMembers(object p0)
		{
			return EntityRes.GetString("Validator_NoKeyMembers", new object[]
			{
				p0
			});
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x060010D6 RID: 4310 RVA: 0x00043228 File Offset: 0x00041428
		internal static string Validator_FacetTypeIsNull
		{
			get
			{
				return EntityRes.GetString("Validator_FacetTypeIsNull");
			}
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x060010D7 RID: 4311 RVA: 0x00043234 File Offset: 0x00041434
		internal static string Validator_MemberHasNullDeclaringType
		{
			get
			{
				return EntityRes.GetString("Validator_MemberHasNullDeclaringType");
			}
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x060010D8 RID: 4312 RVA: 0x00043240 File Offset: 0x00041440
		internal static string Validator_MemberHasNullTypeUsage
		{
			get
			{
				return EntityRes.GetString("Validator_MemberHasNullTypeUsage");
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x060010D9 RID: 4313 RVA: 0x0004324C File Offset: 0x0004144C
		internal static string Validator_ItemAttributeHasNullTypeUsage
		{
			get
			{
				return EntityRes.GetString("Validator_ItemAttributeHasNullTypeUsage");
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x060010DA RID: 4314 RVA: 0x00043258 File Offset: 0x00041458
		internal static string Validator_RefTypeHasNullEntityType
		{
			get
			{
				return EntityRes.GetString("Validator_RefTypeHasNullEntityType");
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x060010DB RID: 4315 RVA: 0x00043264 File Offset: 0x00041464
		internal static string Validator_TypeUsageHasNullEdmType
		{
			get
			{
				return EntityRes.GetString("Validator_TypeUsageHasNullEdmType");
			}
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x060010DC RID: 4316 RVA: 0x00043270 File Offset: 0x00041470
		internal static string Validator_BaseTypeHasMemberOfSameName
		{
			get
			{
				return EntityRes.GetString("Validator_BaseTypeHasMemberOfSameName");
			}
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x060010DD RID: 4317 RVA: 0x0004327C File Offset: 0x0004147C
		internal static string Validator_CollectionTypesCannotHaveBaseType
		{
			get
			{
				return EntityRes.GetString("Validator_CollectionTypesCannotHaveBaseType");
			}
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x060010DE RID: 4318 RVA: 0x00043288 File Offset: 0x00041488
		internal static string Validator_RefTypesCannotHaveBaseType
		{
			get
			{
				return EntityRes.GetString("Validator_RefTypesCannotHaveBaseType");
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x060010DF RID: 4319 RVA: 0x00043294 File Offset: 0x00041494
		internal static string Validator_TypeHasNoName
		{
			get
			{
				return EntityRes.GetString("Validator_TypeHasNoName");
			}
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x060010E0 RID: 4320 RVA: 0x000432A0 File Offset: 0x000414A0
		internal static string Validator_TypeHasNoNamespace
		{
			get
			{
				return EntityRes.GetString("Validator_TypeHasNoNamespace");
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x060010E1 RID: 4321 RVA: 0x000432AC File Offset: 0x000414AC
		internal static string Validator_FacetHasNoName
		{
			get
			{
				return EntityRes.GetString("Validator_FacetHasNoName");
			}
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x060010E2 RID: 4322 RVA: 0x000432B8 File Offset: 0x000414B8
		internal static string Validator_MemberHasNoName
		{
			get
			{
				return EntityRes.GetString("Validator_MemberHasNoName");
			}
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x060010E3 RID: 4323 RVA: 0x000432C4 File Offset: 0x000414C4
		internal static string Validator_MetadataPropertyHasNoName
		{
			get
			{
				return EntityRes.GetString("Validator_MetadataPropertyHasNoName");
			}
		}

		// Token: 0x060010E4 RID: 4324 RVA: 0x000432D0 File Offset: 0x000414D0
		internal static string Validator_NullableEntityKeyProperty(object p0, object p1)
		{
			return EntityRes.GetString("Validator_NullableEntityKeyProperty", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060010E5 RID: 4325 RVA: 0x000432EA File Offset: 0x000414EA
		internal static string Validator_OSpace_InvalidNavPropReturnType(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Validator_OSpace_InvalidNavPropReturnType", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x060010E6 RID: 4326 RVA: 0x00043308 File Offset: 0x00041508
		internal static string Validator_OSpace_ScalarPropertyNotPrimitive(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Validator_OSpace_ScalarPropertyNotPrimitive", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x060010E7 RID: 4327 RVA: 0x00043326 File Offset: 0x00041526
		internal static string Validator_OSpace_ComplexPropertyNotComplex(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Validator_OSpace_ComplexPropertyNotComplex", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x060010E8 RID: 4328 RVA: 0x00043344 File Offset: 0x00041544
		internal static string Validator_OSpace_Convention_MultipleTypesWithSameName(object p0)
		{
			return EntityRes.GetString("Validator_OSpace_Convention_MultipleTypesWithSameName", new object[]
			{
				p0
			});
		}

		// Token: 0x060010E9 RID: 4329 RVA: 0x0004335A File Offset: 0x0004155A
		internal static string Validator_OSpace_Convention_NonPrimitiveTypeProperty(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Validator_OSpace_Convention_NonPrimitiveTypeProperty", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x060010EA RID: 4330 RVA: 0x00043378 File Offset: 0x00041578
		internal static string Validator_OSpace_Convention_MissingRequiredProperty(object p0, object p1)
		{
			return EntityRes.GetString("Validator_OSpace_Convention_MissingRequiredProperty", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060010EB RID: 4331 RVA: 0x00043392 File Offset: 0x00041592
		internal static string Validator_OSpace_Convention_BaseTypeIncompatible(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Validator_OSpace_Convention_BaseTypeIncompatible", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x060010EC RID: 4332 RVA: 0x000433B0 File Offset: 0x000415B0
		internal static string Validator_OSpace_Convention_MissingOSpaceType(object p0)
		{
			return EntityRes.GetString("Validator_OSpace_Convention_MissingOSpaceType", new object[]
			{
				p0
			});
		}

		// Token: 0x060010ED RID: 4333 RVA: 0x000433C6 File Offset: 0x000415C6
		internal static string Validator_OSpace_Convention_RelationshipNotLoaded(object p0, object p1)
		{
			return EntityRes.GetString("Validator_OSpace_Convention_RelationshipNotLoaded", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060010EE RID: 4334 RVA: 0x000433E0 File Offset: 0x000415E0
		internal static string Validator_OSpace_Convention_AttributeAssemblyReferenced(object p0)
		{
			return EntityRes.GetString("Validator_OSpace_Convention_AttributeAssemblyReferenced", new object[]
			{
				p0
			});
		}

		// Token: 0x060010EF RID: 4335 RVA: 0x000433F6 File Offset: 0x000415F6
		internal static string Validator_OSpace_Convention_ScalarPropertyMissginGetterOrSetter(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Validator_OSpace_Convention_ScalarPropertyMissginGetterOrSetter", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x060010F0 RID: 4336 RVA: 0x00043414 File Offset: 0x00041614
		internal static string Validator_OSpace_Convention_AmbiguousClrType(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Validator_OSpace_Convention_AmbiguousClrType", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x060010F1 RID: 4337 RVA: 0x00043432 File Offset: 0x00041632
		internal static string Validator_OSpace_Convention_Struct(object p0, object p1)
		{
			return EntityRes.GetString("Validator_OSpace_Convention_Struct", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060010F2 RID: 4338 RVA: 0x0004344C File Offset: 0x0004164C
		internal static string Validator_OSpace_Convention_BaseTypeNotLoaded(object p0, object p1)
		{
			return EntityRes.GetString("Validator_OSpace_Convention_BaseTypeNotLoaded", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060010F3 RID: 4339 RVA: 0x00043466 File Offset: 0x00041666
		internal static string Validator_OSpace_Convention_SSpaceOSpaceTypeMismatch(object p0, object p1)
		{
			return EntityRes.GetString("Validator_OSpace_Convention_SSpaceOSpaceTypeMismatch", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x060010F4 RID: 4340 RVA: 0x00043480 File Offset: 0x00041680
		internal static string Validator_OSpace_Convention_NonMatchingUnderlyingTypes
		{
			get
			{
				return EntityRes.GetString("Validator_OSpace_Convention_NonMatchingUnderlyingTypes");
			}
		}

		// Token: 0x060010F5 RID: 4341 RVA: 0x0004348C File Offset: 0x0004168C
		internal static string Validator_UnsupportedEnumUnderlyingType(object p0)
		{
			return EntityRes.GetString("Validator_UnsupportedEnumUnderlyingType", new object[]
			{
				p0
			});
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x060010F6 RID: 4342 RVA: 0x000434A2 File Offset: 0x000416A2
		internal static string ExtraInfo
		{
			get
			{
				return EntityRes.GetString("ExtraInfo");
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x060010F7 RID: 4343 RVA: 0x000434AE File Offset: 0x000416AE
		internal static string Metadata_General_Error
		{
			get
			{
				return EntityRes.GetString("Metadata_General_Error");
			}
		}

		// Token: 0x060010F8 RID: 4344 RVA: 0x000434BA File Offset: 0x000416BA
		internal static string InvalidNumberOfParametersForAggregateFunction(object p0)
		{
			return EntityRes.GetString("InvalidNumberOfParametersForAggregateFunction", new object[]
			{
				p0
			});
		}

		// Token: 0x060010F9 RID: 4345 RVA: 0x000434D0 File Offset: 0x000416D0
		internal static string InvalidParameterTypeForAggregateFunction(object p0, object p1)
		{
			return EntityRes.GetString("InvalidParameterTypeForAggregateFunction", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060010FA RID: 4346 RVA: 0x000434EA File Offset: 0x000416EA
		internal static string ItemCollectionAlreadyRegistered(object p0)
		{
			return EntityRes.GetString("ItemCollectionAlreadyRegistered", new object[]
			{
				p0
			});
		}

		// Token: 0x060010FB RID: 4347 RVA: 0x00043500 File Offset: 0x00041700
		internal static string InvalidSchemaEncountered(object p0)
		{
			return EntityRes.GetString("InvalidSchemaEncountered", new object[]
			{
				p0
			});
		}

		// Token: 0x060010FC RID: 4348 RVA: 0x00043516 File Offset: 0x00041716
		internal static string SystemNamespaceEncountered(object p0)
		{
			return EntityRes.GetString("SystemNamespaceEncountered", new object[]
			{
				p0
			});
		}

		// Token: 0x060010FD RID: 4349 RVA: 0x0004352C File Offset: 0x0004172C
		internal static string NoCollectionForSpace(object p0)
		{
			return EntityRes.GetString("NoCollectionForSpace", new object[]
			{
				p0
			});
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x060010FE RID: 4350 RVA: 0x00043542 File Offset: 0x00041742
		internal static string OperationOnReadOnlyCollection
		{
			get
			{
				return EntityRes.GetString("OperationOnReadOnlyCollection");
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x060010FF RID: 4351 RVA: 0x0004354E File Offset: 0x0004174E
		internal static string OperationOnReadOnlyItem
		{
			get
			{
				return EntityRes.GetString("OperationOnReadOnlyItem");
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06001100 RID: 4352 RVA: 0x0004355A File Offset: 0x0004175A
		internal static string EntitySetInAnotherContainer
		{
			get
			{
				return EntityRes.GetString("EntitySetInAnotherContainer");
			}
		}

		// Token: 0x06001101 RID: 4353 RVA: 0x00043566 File Offset: 0x00041766
		internal static string InvalidKeyMember(object p0)
		{
			return EntityRes.GetString("InvalidKeyMember", new object[]
			{
				p0
			});
		}

		// Token: 0x06001102 RID: 4354 RVA: 0x0004357C File Offset: 0x0004177C
		internal static string InvalidFileExtension(object p0, object p1, object p2)
		{
			return EntityRes.GetString("InvalidFileExtension", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06001103 RID: 4355 RVA: 0x0004359A File Offset: 0x0004179A
		internal static string NewTypeConflictsWithExistingType(object p0, object p1)
		{
			return EntityRes.GetString("NewTypeConflictsWithExistingType", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06001104 RID: 4356 RVA: 0x000435B4 File Offset: 0x000417B4
		internal static string NotValidInputPath
		{
			get
			{
				return EntityRes.GetString("NotValidInputPath");
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06001105 RID: 4357 RVA: 0x000435C0 File Offset: 0x000417C0
		internal static string UnableToDetermineApplicationContext
		{
			get
			{
				return EntityRes.GetString("UnableToDetermineApplicationContext");
			}
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06001106 RID: 4358 RVA: 0x000435CC File Offset: 0x000417CC
		internal static string WildcardEnumeratorReturnedNull
		{
			get
			{
				return EntityRes.GetString("WildcardEnumeratorReturnedNull");
			}
		}

		// Token: 0x06001107 RID: 4359 RVA: 0x000435D8 File Offset: 0x000417D8
		internal static string InvalidUseOfWebPath(object p0)
		{
			return EntityRes.GetString("InvalidUseOfWebPath", new object[]
			{
				p0
			});
		}

		// Token: 0x06001108 RID: 4360 RVA: 0x000435EE File Offset: 0x000417EE
		internal static string UnableToFindReflectedType(object p0, object p1)
		{
			return EntityRes.GetString("UnableToFindReflectedType", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06001109 RID: 4361 RVA: 0x00043608 File Offset: 0x00041808
		internal static string AssemblyMissingFromAssembliesToConsider(object p0)
		{
			return EntityRes.GetString("AssemblyMissingFromAssembliesToConsider", new object[]
			{
				p0
			});
		}

		// Token: 0x0600110A RID: 4362 RVA: 0x0004361E File Offset: 0x0004181E
		internal static string InvalidCollectionSpecified(object p0)
		{
			return EntityRes.GetString("InvalidCollectionSpecified", new object[]
			{
				p0
			});
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x0600110B RID: 4363 RVA: 0x00043634 File Offset: 0x00041834
		internal static string UnableToLoadResource
		{
			get
			{
				return EntityRes.GetString("UnableToLoadResource");
			}
		}

		// Token: 0x0600110C RID: 4364 RVA: 0x00043640 File Offset: 0x00041840
		internal static string EdmVersionNotSupportedByRuntime(object p0, object p1)
		{
			return EntityRes.GetString("EdmVersionNotSupportedByRuntime", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x0600110D RID: 4365 RVA: 0x0004365A File Offset: 0x0004185A
		internal static string AtleastOneSSDLNeeded
		{
			get
			{
				return EntityRes.GetString("AtleastOneSSDLNeeded");
			}
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x0600110E RID: 4366 RVA: 0x00043666 File Offset: 0x00041866
		internal static string InvalidMetadataPath
		{
			get
			{
				return EntityRes.GetString("InvalidMetadataPath");
			}
		}

		// Token: 0x0600110F RID: 4367 RVA: 0x00043672 File Offset: 0x00041872
		internal static string UnableToResolveAssembly(object p0)
		{
			return EntityRes.GetString("UnableToResolveAssembly", new object[]
			{
				p0
			});
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06001110 RID: 4368 RVA: 0x00043688 File Offset: 0x00041888
		internal static string UnableToDetermineStoreVersion
		{
			get
			{
				return EntityRes.GetString("UnableToDetermineStoreVersion");
			}
		}

		// Token: 0x06001111 RID: 4369 RVA: 0x00043694 File Offset: 0x00041894
		internal static string DuplicatedFunctionoverloads(object p0, object p1)
		{
			return EntityRes.GetString("DuplicatedFunctionoverloads", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06001112 RID: 4370 RVA: 0x000436AE File Offset: 0x000418AE
		internal static string EntitySetNotInCSPace(object p0)
		{
			return EntityRes.GetString("EntitySetNotInCSPace", new object[]
			{
				p0
			});
		}

		// Token: 0x06001113 RID: 4371 RVA: 0x000436C4 File Offset: 0x000418C4
		internal static string TypeNotInEntitySet(object p0, object p1, object p2)
		{
			return EntityRes.GetString("TypeNotInEntitySet", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06001114 RID: 4372 RVA: 0x000436E2 File Offset: 0x000418E2
		internal static string TypeNotInAssociationSet(object p0, object p1, object p2)
		{
			return EntityRes.GetString("TypeNotInAssociationSet", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06001115 RID: 4373 RVA: 0x00043700 File Offset: 0x00041900
		internal static string DifferentSchemaVersionInCollection(object p0, object p1, object p2)
		{
			return EntityRes.GetString("DifferentSchemaVersionInCollection", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06001116 RID: 4374 RVA: 0x0004371E File Offset: 0x0004191E
		internal static string InvalidCollectionForMapping(object p0)
		{
			return EntityRes.GetString("InvalidCollectionForMapping", new object[]
			{
				p0
			});
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06001117 RID: 4375 RVA: 0x00043734 File Offset: 0x00041934
		internal static string OnlyStoreConnectionsSupported
		{
			get
			{
				return EntityRes.GetString("OnlyStoreConnectionsSupported");
			}
		}

		// Token: 0x06001118 RID: 4376 RVA: 0x00043740 File Offset: 0x00041940
		internal static string StoreItemCollectionMustHaveOneArtifact(object p0)
		{
			return EntityRes.GetString("StoreItemCollectionMustHaveOneArtifact", new object[]
			{
				p0
			});
		}

		// Token: 0x06001119 RID: 4377 RVA: 0x00043756 File Offset: 0x00041956
		internal static string CheckArgumentContainsNullFailed(object p0)
		{
			return EntityRes.GetString("CheckArgumentContainsNullFailed", new object[]
			{
				p0
			});
		}

		// Token: 0x0600111A RID: 4378 RVA: 0x0004376C File Offset: 0x0004196C
		internal static string InvalidRelationshipSetName(object p0)
		{
			return EntityRes.GetString("InvalidRelationshipSetName", new object[]
			{
				p0
			});
		}

		// Token: 0x0600111B RID: 4379 RVA: 0x00043782 File Offset: 0x00041982
		internal static string MemberInvalidIdentity(object p0)
		{
			return EntityRes.GetString("MemberInvalidIdentity", new object[]
			{
				p0
			});
		}

		// Token: 0x0600111C RID: 4380 RVA: 0x00043798 File Offset: 0x00041998
		internal static string InvalidEntitySetName(object p0)
		{
			return EntityRes.GetString("InvalidEntitySetName", new object[]
			{
				p0
			});
		}

		// Token: 0x0600111D RID: 4381 RVA: 0x000437AE File Offset: 0x000419AE
		internal static string ItemInvalidIdentity(object p0)
		{
			return EntityRes.GetString("ItemInvalidIdentity", new object[]
			{
				p0
			});
		}

		// Token: 0x0600111E RID: 4382 RVA: 0x000437C4 File Offset: 0x000419C4
		internal static string ItemDuplicateIdentity(object p0)
		{
			return EntityRes.GetString("ItemDuplicateIdentity", new object[]
			{
				p0
			});
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x0600111F RID: 4383 RVA: 0x000437DA File Offset: 0x000419DA
		internal static string NotStringTypeForTypeUsage
		{
			get
			{
				return EntityRes.GetString("NotStringTypeForTypeUsage");
			}
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06001120 RID: 4384 RVA: 0x000437E6 File Offset: 0x000419E6
		internal static string NotBinaryTypeForTypeUsage
		{
			get
			{
				return EntityRes.GetString("NotBinaryTypeForTypeUsage");
			}
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06001121 RID: 4385 RVA: 0x000437F2 File Offset: 0x000419F2
		internal static string NotDateTimeTypeForTypeUsage
		{
			get
			{
				return EntityRes.GetString("NotDateTimeTypeForTypeUsage");
			}
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06001122 RID: 4386 RVA: 0x000437FE File Offset: 0x000419FE
		internal static string NotDateTimeOffsetTypeForTypeUsage
		{
			get
			{
				return EntityRes.GetString("NotDateTimeOffsetTypeForTypeUsage");
			}
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06001123 RID: 4387 RVA: 0x0004380A File Offset: 0x00041A0A
		internal static string NotTimeTypeForTypeUsage
		{
			get
			{
				return EntityRes.GetString("NotTimeTypeForTypeUsage");
			}
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06001124 RID: 4388 RVA: 0x00043816 File Offset: 0x00041A16
		internal static string NotDecimalTypeForTypeUsage
		{
			get
			{
				return EntityRes.GetString("NotDecimalTypeForTypeUsage");
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06001125 RID: 4389 RVA: 0x00043822 File Offset: 0x00041A22
		internal static string ArrayTooSmall
		{
			get
			{
				return EntityRes.GetString("ArrayTooSmall");
			}
		}

		// Token: 0x06001126 RID: 4390 RVA: 0x0004382E File Offset: 0x00041A2E
		internal static string MoreThanOneItemMatchesIdentity(object p0)
		{
			return EntityRes.GetString("MoreThanOneItemMatchesIdentity", new object[]
			{
				p0
			});
		}

		// Token: 0x06001127 RID: 4391 RVA: 0x00043844 File Offset: 0x00041A44
		internal static string MissingDefaultValueForConstantFacet(object p0, object p1)
		{
			return EntityRes.GetString("MissingDefaultValueForConstantFacet", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06001128 RID: 4392 RVA: 0x0004385E File Offset: 0x00041A5E
		internal static string MinAndMaxValueMustBeSameForConstantFacet(object p0, object p1)
		{
			return EntityRes.GetString("MinAndMaxValueMustBeSameForConstantFacet", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06001129 RID: 4393 RVA: 0x00043878 File Offset: 0x00041A78
		internal static string BothMinAndMaxValueMustBeSpecifiedForNonConstantFacet(object p0, object p1)
		{
			return EntityRes.GetString("BothMinAndMaxValueMustBeSpecifiedForNonConstantFacet", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x0600112A RID: 4394 RVA: 0x00043892 File Offset: 0x00041A92
		internal static string MinAndMaxValueMustBeDifferentForNonConstantFacet(object p0, object p1)
		{
			return EntityRes.GetString("MinAndMaxValueMustBeDifferentForNonConstantFacet", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x0600112B RID: 4395 RVA: 0x000438AC File Offset: 0x00041AAC
		internal static string MinAndMaxMustBePositive(object p0, object p1)
		{
			return EntityRes.GetString("MinAndMaxMustBePositive", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x0600112C RID: 4396 RVA: 0x000438C6 File Offset: 0x00041AC6
		internal static string MinMustBeLessThanMax(object p0, object p1, object p2)
		{
			return EntityRes.GetString("MinMustBeLessThanMax", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x0600112D RID: 4397 RVA: 0x000438E4 File Offset: 0x00041AE4
		internal static string SameRoleNameOnRelationshipAttribute(object p0, object p1)
		{
			return EntityRes.GetString("SameRoleNameOnRelationshipAttribute", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x0600112E RID: 4398 RVA: 0x000438FE File Offset: 0x00041AFE
		internal static string RoleTypeInEdmRelationshipAttributeIsInvalidType(object p0, object p1, object p2)
		{
			return EntityRes.GetString("RoleTypeInEdmRelationshipAttributeIsInvalidType", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x0600112F RID: 4399 RVA: 0x0004391C File Offset: 0x00041B1C
		internal static string TargetRoleNameInNavigationPropertyNotValid(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("TargetRoleNameInNavigationPropertyNotValid", new object[]
			{
				p0,
				p1,
				p2,
				p3
			});
		}

		// Token: 0x06001130 RID: 4400 RVA: 0x0004393E File Offset: 0x00041B3E
		internal static string RelationshipNameInNavigationPropertyNotValid(object p0, object p1, object p2)
		{
			return EntityRes.GetString("RelationshipNameInNavigationPropertyNotValid", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06001131 RID: 4401 RVA: 0x0004395C File Offset: 0x00041B5C
		internal static string NestedClassNotSupported(object p0, object p1)
		{
			return EntityRes.GetString("NestedClassNotSupported", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06001132 RID: 4402 RVA: 0x00043976 File Offset: 0x00041B76
		internal static string NullParameterForEdmRelationshipAttribute(object p0, object p1)
		{
			return EntityRes.GetString("NullParameterForEdmRelationshipAttribute", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06001133 RID: 4403 RVA: 0x00043990 File Offset: 0x00041B90
		internal static string NullRelationshipNameforEdmRelationshipAttribute(object p0)
		{
			return EntityRes.GetString("NullRelationshipNameforEdmRelationshipAttribute", new object[]
			{
				p0
			});
		}

		// Token: 0x06001134 RID: 4404 RVA: 0x000439A6 File Offset: 0x00041BA6
		internal static string NavigationPropertyRelationshipEndTypeMismatch(object p0, object p1, object p2, object p3, object p4)
		{
			return EntityRes.GetString("NavigationPropertyRelationshipEndTypeMismatch", new object[]
			{
				p0,
				p1,
				p2,
				p3,
				p4
			});
		}

		// Token: 0x06001135 RID: 4405 RVA: 0x000439CD File Offset: 0x00041BCD
		internal static string AllArtifactsMustTargetSameProvider_InvariantName(object p0, object p1)
		{
			return EntityRes.GetString("AllArtifactsMustTargetSameProvider_InvariantName", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06001136 RID: 4406 RVA: 0x000439E7 File Offset: 0x00041BE7
		internal static string AllArtifactsMustTargetSameProvider_ManifestToken(object p0, object p1)
		{
			return EntityRes.GetString("AllArtifactsMustTargetSameProvider_ManifestToken", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06001137 RID: 4407 RVA: 0x00043A01 File Offset: 0x00041C01
		internal static string ProviderManifestTokenNotFound
		{
			get
			{
				return EntityRes.GetString("ProviderManifestTokenNotFound");
			}
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06001138 RID: 4408 RVA: 0x00043A0D File Offset: 0x00041C0D
		internal static string FailedToRetrieveProviderManifest
		{
			get
			{
				return EntityRes.GetString("FailedToRetrieveProviderManifest");
			}
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06001139 RID: 4409 RVA: 0x00043A19 File Offset: 0x00041C19
		internal static string InvalidMaxLengthSize
		{
			get
			{
				return EntityRes.GetString("InvalidMaxLengthSize");
			}
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x0600113A RID: 4410 RVA: 0x00043A25 File Offset: 0x00041C25
		internal static string ArgumentMustBeCSpaceType
		{
			get
			{
				return EntityRes.GetString("ArgumentMustBeCSpaceType");
			}
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x0600113B RID: 4411 RVA: 0x00043A31 File Offset: 0x00041C31
		internal static string ArgumentMustBeOSpaceType
		{
			get
			{
				return EntityRes.GetString("ArgumentMustBeOSpaceType");
			}
		}

		// Token: 0x0600113C RID: 4412 RVA: 0x00043A3D File Offset: 0x00041C3D
		internal static string FailedToFindOSpaceTypeMapping(object p0)
		{
			return EntityRes.GetString("FailedToFindOSpaceTypeMapping", new object[]
			{
				p0
			});
		}

		// Token: 0x0600113D RID: 4413 RVA: 0x00043A53 File Offset: 0x00041C53
		internal static string FailedToFindCSpaceTypeMapping(object p0)
		{
			return EntityRes.GetString("FailedToFindCSpaceTypeMapping", new object[]
			{
				p0
			});
		}

		// Token: 0x0600113E RID: 4414 RVA: 0x00043A69 File Offset: 0x00041C69
		internal static string FailedToFindClrTypeMapping(object p0)
		{
			return EntityRes.GetString("FailedToFindClrTypeMapping", new object[]
			{
				p0
			});
		}

		// Token: 0x0600113F RID: 4415 RVA: 0x00043A7F File Offset: 0x00041C7F
		internal static string GenericTypeNotSupported(object p0)
		{
			return EntityRes.GetString("GenericTypeNotSupported", new object[]
			{
				p0
			});
		}

		// Token: 0x06001140 RID: 4416 RVA: 0x00043A95 File Offset: 0x00041C95
		internal static string InvalidEDMVersion(object p0)
		{
			return EntityRes.GetString("InvalidEDMVersion", new object[]
			{
				p0
			});
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06001141 RID: 4417 RVA: 0x00043AAB File Offset: 0x00041CAB
		internal static string Mapping_General_Error
		{
			get
			{
				return EntityRes.GetString("Mapping_General_Error");
			}
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06001142 RID: 4418 RVA: 0x00043AB7 File Offset: 0x00041CB7
		internal static string Mapping_InvalidContent_General
		{
			get
			{
				return EntityRes.GetString("Mapping_InvalidContent_General");
			}
		}

		// Token: 0x06001143 RID: 4419 RVA: 0x00043AC3 File Offset: 0x00041CC3
		internal static string Mapping_InvalidContent_EntityContainer(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidContent_EntityContainer", new object[]
			{
				p0
			});
		}

		// Token: 0x06001144 RID: 4420 RVA: 0x00043AD9 File Offset: 0x00041CD9
		internal static string Mapping_InvalidContent_StorageEntityContainer(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidContent_StorageEntityContainer", new object[]
			{
				p0
			});
		}

		// Token: 0x06001145 RID: 4421 RVA: 0x00043AEF File Offset: 0x00041CEF
		internal static string Mapping_AlreadyMapped_StorageEntityContainer(object p0)
		{
			return EntityRes.GetString("Mapping_AlreadyMapped_StorageEntityContainer", new object[]
			{
				p0
			});
		}

		// Token: 0x06001146 RID: 4422 RVA: 0x00043B05 File Offset: 0x00041D05
		internal static string Mapping_InvalidContent_Entity_Set(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidContent_Entity_Set", new object[]
			{
				p0
			});
		}

		// Token: 0x06001147 RID: 4423 RVA: 0x00043B1B File Offset: 0x00041D1B
		internal static string Mapping_InvalidContent_Entity_Type(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidContent_Entity_Type", new object[]
			{
				p0
			});
		}

		// Token: 0x06001148 RID: 4424 RVA: 0x00043B31 File Offset: 0x00041D31
		internal static string Mapping_InvalidContent_AbstractEntity_FunctionMapping(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidContent_AbstractEntity_FunctionMapping", new object[]
			{
				p0
			});
		}

		// Token: 0x06001149 RID: 4425 RVA: 0x00043B47 File Offset: 0x00041D47
		internal static string Mapping_InvalidContent_AbstractEntity_Type(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidContent_AbstractEntity_Type", new object[]
			{
				p0
			});
		}

		// Token: 0x0600114A RID: 4426 RVA: 0x00043B5D File Offset: 0x00041D5D
		internal static string Mapping_InvalidContent_AbstractEntity_IsOfType(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidContent_AbstractEntity_IsOfType", new object[]
			{
				p0
			});
		}

		// Token: 0x0600114B RID: 4427 RVA: 0x00043B73 File Offset: 0x00041D73
		internal static string Mapping_InvalidContent_Entity_Type_For_Entity_Set(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Mapping_InvalidContent_Entity_Type_For_Entity_Set", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x0600114C RID: 4428 RVA: 0x00043B91 File Offset: 0x00041D91
		internal static string Mapping_Invalid_Association_Type_For_Association_Set(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Mapping_Invalid_Association_Type_For_Association_Set", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x0600114D RID: 4429 RVA: 0x00043BAF File Offset: 0x00041DAF
		internal static string Mapping_InvalidContent_Table(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidContent_Table", new object[]
			{
				p0
			});
		}

		// Token: 0x0600114E RID: 4430 RVA: 0x00043BC5 File Offset: 0x00041DC5
		internal static string Mapping_InvalidContent_Complex_Type(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidContent_Complex_Type", new object[]
			{
				p0
			});
		}

		// Token: 0x0600114F RID: 4431 RVA: 0x00043BDB File Offset: 0x00041DDB
		internal static string Mapping_InvalidContent_Association_Set(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidContent_Association_Set", new object[]
			{
				p0
			});
		}

		// Token: 0x06001150 RID: 4432 RVA: 0x00043BF1 File Offset: 0x00041DF1
		internal static string Mapping_InvalidContent_AssociationSet_Condition(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidContent_AssociationSet_Condition", new object[]
			{
				p0
			});
		}

		// Token: 0x06001151 RID: 4433 RVA: 0x00043C07 File Offset: 0x00041E07
		internal static string Mapping_InvalidContent_ForeignKey_Association_Set(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidContent_ForeignKey_Association_Set", new object[]
			{
				p0
			});
		}

		// Token: 0x06001152 RID: 4434 RVA: 0x00043C1D File Offset: 0x00041E1D
		internal static string Mapping_InvalidContent_ForeignKey_Association_Set_PKtoPK(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidContent_ForeignKey_Association_Set_PKtoPK", new object[]
			{
				p0
			});
		}

		// Token: 0x06001153 RID: 4435 RVA: 0x00043C33 File Offset: 0x00041E33
		internal static string Mapping_InvalidContent_Association_Type(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidContent_Association_Type", new object[]
			{
				p0
			});
		}

		// Token: 0x06001154 RID: 4436 RVA: 0x00043C49 File Offset: 0x00041E49
		internal static string Mapping_InvalidContent_EndProperty(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidContent_EndProperty", new object[]
			{
				p0
			});
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06001155 RID: 4437 RVA: 0x00043C5F File Offset: 0x00041E5F
		internal static string Mapping_InvalidContent_Association_Type_Empty
		{
			get
			{
				return EntityRes.GetString("Mapping_InvalidContent_Association_Type_Empty");
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06001156 RID: 4438 RVA: 0x00043C6B File Offset: 0x00041E6B
		internal static string Mapping_InvalidContent_Table_Expected
		{
			get
			{
				return EntityRes.GetString("Mapping_InvalidContent_Table_Expected");
			}
		}

		// Token: 0x06001157 RID: 4439 RVA: 0x00043C77 File Offset: 0x00041E77
		internal static string Mapping_InvalidContent_Cdm_Member(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidContent_Cdm_Member", new object[]
			{
				p0
			});
		}

		// Token: 0x06001158 RID: 4440 RVA: 0x00043C8D File Offset: 0x00041E8D
		internal static string Mapping_InvalidContent_Column(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidContent_Column", new object[]
			{
				p0
			});
		}

		// Token: 0x06001159 RID: 4441 RVA: 0x00043CA3 File Offset: 0x00041EA3
		internal static string Mapping_InvalidContent_End(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidContent_End", new object[]
			{
				p0
			});
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x0600115A RID: 4442 RVA: 0x00043CB9 File Offset: 0x00041EB9
		internal static string Mapping_InvalidContent_Container_SubElement
		{
			get
			{
				return EntityRes.GetString("Mapping_InvalidContent_Container_SubElement");
			}
		}

		// Token: 0x0600115B RID: 4443 RVA: 0x00043CC5 File Offset: 0x00041EC5
		internal static string Mapping_InvalidContent_Duplicate_Cdm_Member(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidContent_Duplicate_Cdm_Member", new object[]
			{
				p0
			});
		}

		// Token: 0x0600115C RID: 4444 RVA: 0x00043CDB File Offset: 0x00041EDB
		internal static string Mapping_InvalidContent_Duplicate_Condition_Member(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidContent_Duplicate_Condition_Member", new object[]
			{
				p0
			});
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x0600115D RID: 4445 RVA: 0x00043CF1 File Offset: 0x00041EF1
		internal static string Mapping_InvalidContent_ConditionMapping_Both_Members
		{
			get
			{
				return EntityRes.GetString("Mapping_InvalidContent_ConditionMapping_Both_Members");
			}
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x0600115E RID: 4446 RVA: 0x00043CFD File Offset: 0x00041EFD
		internal static string Mapping_InvalidContent_ConditionMapping_Either_Members
		{
			get
			{
				return EntityRes.GetString("Mapping_InvalidContent_ConditionMapping_Either_Members");
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x0600115F RID: 4447 RVA: 0x00043D09 File Offset: 0x00041F09
		internal static string Mapping_InvalidContent_ConditionMapping_Both_Values
		{
			get
			{
				return EntityRes.GetString("Mapping_InvalidContent_ConditionMapping_Both_Values");
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06001160 RID: 4448 RVA: 0x00043D15 File Offset: 0x00041F15
		internal static string Mapping_InvalidContent_ConditionMapping_Either_Values
		{
			get
			{
				return EntityRes.GetString("Mapping_InvalidContent_ConditionMapping_Either_Values");
			}
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06001161 RID: 4449 RVA: 0x00043D21 File Offset: 0x00041F21
		internal static string Mapping_InvalidContent_ConditionMapping_NonScalar
		{
			get
			{
				return EntityRes.GetString("Mapping_InvalidContent_ConditionMapping_NonScalar");
			}
		}

		// Token: 0x06001162 RID: 4450 RVA: 0x00043D2D File Offset: 0x00041F2D
		internal static string Mapping_InvalidContent_ConditionMapping_InvalidPrimitiveTypeKind(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_InvalidContent_ConditionMapping_InvalidPrimitiveTypeKind", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06001163 RID: 4451 RVA: 0x00043D47 File Offset: 0x00041F47
		internal static string Mapping_InvalidContent_ConditionMapping_InvalidMember(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidContent_ConditionMapping_InvalidMember", new object[]
			{
				p0
			});
		}

		// Token: 0x06001164 RID: 4452 RVA: 0x00043D5D File Offset: 0x00041F5D
		internal static string Mapping_InvalidContent_ConditionMapping_Computed(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidContent_ConditionMapping_Computed", new object[]
			{
				p0
			});
		}

		// Token: 0x06001165 RID: 4453 RVA: 0x00043D73 File Offset: 0x00041F73
		internal static string Mapping_InvalidContent_Emtpty_SetMap(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidContent_Emtpty_SetMap", new object[]
			{
				p0
			});
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06001166 RID: 4454 RVA: 0x00043D89 File Offset: 0x00041F89
		internal static string Mapping_InvalidContent_TypeMapping_QueryView
		{
			get
			{
				return EntityRes.GetString("Mapping_InvalidContent_TypeMapping_QueryView");
			}
		}

		// Token: 0x06001167 RID: 4455 RVA: 0x00043D95 File Offset: 0x00041F95
		internal static string Mapping_Default_OCMapping_Clr_Member(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Mapping_Default_OCMapping_Clr_Member", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06001168 RID: 4456 RVA: 0x00043DB3 File Offset: 0x00041FB3
		internal static string Mapping_Default_OCMapping_Clr_Member2(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Mapping_Default_OCMapping_Clr_Member2", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06001169 RID: 4457 RVA: 0x00043DD1 File Offset: 0x00041FD1
		internal static string Mapping_Default_OCMapping_Invalid_MemberType(object p0, object p1, object p2, object p3, object p4, object p5)
		{
			return EntityRes.GetString("Mapping_Default_OCMapping_Invalid_MemberType", new object[]
			{
				p0,
				p1,
				p2,
				p3,
				p4,
				p5
			});
		}

		// Token: 0x0600116A RID: 4458 RVA: 0x00043DFD File Offset: 0x00041FFD
		internal static string Mapping_Default_OCMapping_MemberKind_Mismatch(object p0, object p1, object p2, object p3, object p4, object p5)
		{
			return EntityRes.GetString("Mapping_Default_OCMapping_MemberKind_Mismatch", new object[]
			{
				p0,
				p1,
				p2,
				p3,
				p4,
				p5
			});
		}

		// Token: 0x0600116B RID: 4459 RVA: 0x00043E29 File Offset: 0x00042029
		internal static string Mapping_Default_OCMapping_MultiplicityMismatch(object p0, object p1, object p2, object p3, object p4, object p5)
		{
			return EntityRes.GetString("Mapping_Default_OCMapping_MultiplicityMismatch", new object[]
			{
				p0,
				p1,
				p2,
				p3,
				p4,
				p5
			});
		}

		// Token: 0x0600116C RID: 4460 RVA: 0x00043E55 File Offset: 0x00042055
		internal static string Mapping_Default_OCMapping_Member_Count_Mismatch(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_Default_OCMapping_Member_Count_Mismatch", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x0600116D RID: 4461 RVA: 0x00043E6F File Offset: 0x0004206F
		internal static string Mapping_Default_OCMapping_Member_Type_Mismatch(object p0, object p1, object p2, object p3, object p4, object p5, object p6)
		{
			return EntityRes.GetString("Mapping_Default_OCMapping_Member_Type_Mismatch", new object[]
			{
				p0,
				p1,
				p2,
				p3,
				p4,
				p5,
				p6
			});
		}

		// Token: 0x0600116E RID: 4462 RVA: 0x00043EA0 File Offset: 0x000420A0
		internal static string Mapping_Enum_OCMapping_UnderlyingTypesMismatch(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("Mapping_Enum_OCMapping_UnderlyingTypesMismatch", new object[]
			{
				p0,
				p1,
				p2,
				p3
			});
		}

		// Token: 0x0600116F RID: 4463 RVA: 0x00043EC2 File Offset: 0x000420C2
		internal static string Mapping_Enum_OCMapping_MemberMismatch(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("Mapping_Enum_OCMapping_MemberMismatch", new object[]
			{
				p0,
				p1,
				p2,
				p3
			});
		}

		// Token: 0x06001170 RID: 4464 RVA: 0x00043EE4 File Offset: 0x000420E4
		internal static string Mapping_NotFound_EntityContainer(object p0)
		{
			return EntityRes.GetString("Mapping_NotFound_EntityContainer", new object[]
			{
				p0
			});
		}

		// Token: 0x06001171 RID: 4465 RVA: 0x00043EFA File Offset: 0x000420FA
		internal static string Mapping_Duplicate_CdmAssociationSet_StorageMap(object p0)
		{
			return EntityRes.GetString("Mapping_Duplicate_CdmAssociationSet_StorageMap", new object[]
			{
				p0
			});
		}

		// Token: 0x06001172 RID: 4466 RVA: 0x00043F10 File Offset: 0x00042110
		internal static string Mapping_Invalid_CSRootElementMissing(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Mapping_Invalid_CSRootElementMissing", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06001173 RID: 4467 RVA: 0x00043F2E File Offset: 0x0004212E
		internal static string Mapping_ConditionValueTypeMismatch
		{
			get
			{
				return EntityRes.GetString("Mapping_ConditionValueTypeMismatch");
			}
		}

		// Token: 0x06001174 RID: 4468 RVA: 0x00043F3A File Offset: 0x0004213A
		internal static string Mapping_Storage_InvalidSpace(object p0)
		{
			return EntityRes.GetString("Mapping_Storage_InvalidSpace", new object[]
			{
				p0
			});
		}

		// Token: 0x06001175 RID: 4469 RVA: 0x00043F50 File Offset: 0x00042150
		internal static string Mapping_Invalid_Member_Mapping(object p0, object p1, object p2, object p3, object p4, object p5)
		{
			return EntityRes.GetString("Mapping_Invalid_Member_Mapping", new object[]
			{
				p0,
				p1,
				p2,
				p3,
				p4,
				p5
			});
		}

		// Token: 0x06001176 RID: 4470 RVA: 0x00043F7C File Offset: 0x0004217C
		internal static string Mapping_Invalid_CSide_ScalarProperty(object p0)
		{
			return EntityRes.GetString("Mapping_Invalid_CSide_ScalarProperty", new object[]
			{
				p0
			});
		}

		// Token: 0x06001177 RID: 4471 RVA: 0x00043F92 File Offset: 0x00042192
		internal static string Mapping_Duplicate_Type(object p0)
		{
			return EntityRes.GetString("Mapping_Duplicate_Type", new object[]
			{
				p0
			});
		}

		// Token: 0x06001178 RID: 4472 RVA: 0x00043FA8 File Offset: 0x000421A8
		internal static string Mapping_Duplicate_PropertyMap_CaseInsensitive(object p0)
		{
			return EntityRes.GetString("Mapping_Duplicate_PropertyMap_CaseInsensitive", new object[]
			{
				p0
			});
		}

		// Token: 0x06001179 RID: 4473 RVA: 0x00043FBE File Offset: 0x000421BE
		internal static string Mapping_Enum_EmptyValue(object p0)
		{
			return EntityRes.GetString("Mapping_Enum_EmptyValue", new object[]
			{
				p0
			});
		}

		// Token: 0x0600117A RID: 4474 RVA: 0x00043FD4 File Offset: 0x000421D4
		internal static string Mapping_Enum_InvalidValue(object p0)
		{
			return EntityRes.GetString("Mapping_Enum_InvalidValue", new object[]
			{
				p0
			});
		}

		// Token: 0x0600117B RID: 4475 RVA: 0x00043FEA File Offset: 0x000421EA
		internal static string Mapping_InvalidMappingSchema_Parsing(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidMappingSchema_Parsing", new object[]
			{
				p0
			});
		}

		// Token: 0x0600117C RID: 4476 RVA: 0x00044000 File Offset: 0x00042200
		internal static string Mapping_InvalidMappingSchema_validation(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidMappingSchema_validation", new object[]
			{
				p0
			});
		}

		// Token: 0x0600117D RID: 4477 RVA: 0x00044016 File Offset: 0x00042216
		internal static string Mapping_Object_InvalidType(object p0)
		{
			return EntityRes.GetString("Mapping_Object_InvalidType", new object[]
			{
				p0
			});
		}

		// Token: 0x0600117E RID: 4478 RVA: 0x0004402C File Offset: 0x0004222C
		internal static string Mapping_Provider_WrongConnectionType(object p0)
		{
			return EntityRes.GetString("Mapping_Provider_WrongConnectionType", new object[]
			{
				p0
			});
		}

		// Token: 0x0600117F RID: 4479 RVA: 0x00044042 File Offset: 0x00042242
		internal static string Mapping_Provider_WrongManifestType(object p0)
		{
			return EntityRes.GetString("Mapping_Provider_WrongManifestType", new object[]
			{
				p0
			});
		}

		// Token: 0x06001180 RID: 4480 RVA: 0x00044058 File Offset: 0x00042258
		internal static string Mapping_Views_For_Extent_Not_Generated(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_Views_For_Extent_Not_Generated", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06001181 RID: 4481 RVA: 0x00044072 File Offset: 0x00042272
		internal static string Mapping_TableName_QueryView(object p0)
		{
			return EntityRes.GetString("Mapping_TableName_QueryView", new object[]
			{
				p0
			});
		}

		// Token: 0x06001182 RID: 4482 RVA: 0x00044088 File Offset: 0x00042288
		internal static string Mapping_Empty_QueryView(object p0)
		{
			return EntityRes.GetString("Mapping_Empty_QueryView", new object[]
			{
				p0
			});
		}

		// Token: 0x06001183 RID: 4483 RVA: 0x0004409E File Offset: 0x0004229E
		internal static string Mapping_Empty_QueryView_OfType(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_Empty_QueryView_OfType", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06001184 RID: 4484 RVA: 0x000440B8 File Offset: 0x000422B8
		internal static string Mapping_Empty_QueryView_OfTypeOnly(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_Empty_QueryView_OfTypeOnly", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06001185 RID: 4485 RVA: 0x000440D2 File Offset: 0x000422D2
		internal static string Mapping_QueryView_PropertyMaps(object p0)
		{
			return EntityRes.GetString("Mapping_QueryView_PropertyMaps", new object[]
			{
				p0
			});
		}

		// Token: 0x06001186 RID: 4486 RVA: 0x000440E8 File Offset: 0x000422E8
		internal static string Mapping_Invalid_QueryView(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_Invalid_QueryView", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06001187 RID: 4487 RVA: 0x00044102 File Offset: 0x00042302
		internal static string Mapping_Invalid_QueryView2(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_Invalid_QueryView2", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06001188 RID: 4488 RVA: 0x0004411C File Offset: 0x0004231C
		internal static string Mapping_Invalid_QueryView_Type(object p0)
		{
			return EntityRes.GetString("Mapping_Invalid_QueryView_Type", new object[]
			{
				p0
			});
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06001189 RID: 4489 RVA: 0x00044132 File Offset: 0x00042332
		internal static string Mapping_TypeName_For_First_QueryView
		{
			get
			{
				return EntityRes.GetString("Mapping_TypeName_For_First_QueryView");
			}
		}

		// Token: 0x0600118A RID: 4490 RVA: 0x0004413E File Offset: 0x0004233E
		internal static string Mapping_AllQueryViewAtCompileTime(object p0)
		{
			return EntityRes.GetString("Mapping_AllQueryViewAtCompileTime", new object[]
			{
				p0
			});
		}

		// Token: 0x0600118B RID: 4491 RVA: 0x00044154 File Offset: 0x00042354
		internal static string Mapping_QueryViewMultipleTypeInTypeName(object p0)
		{
			return EntityRes.GetString("Mapping_QueryViewMultipleTypeInTypeName", new object[]
			{
				p0
			});
		}

		// Token: 0x0600118C RID: 4492 RVA: 0x0004416A File Offset: 0x0004236A
		internal static string Mapping_QueryView_Duplicate_OfType(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_QueryView_Duplicate_OfType", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x0600118D RID: 4493 RVA: 0x00044184 File Offset: 0x00042384
		internal static string Mapping_QueryView_Duplicate_OfTypeOnly(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_QueryView_Duplicate_OfTypeOnly", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x0600118E RID: 4494 RVA: 0x0004419E File Offset: 0x0004239E
		internal static string Mapping_QueryView_TypeName_Not_Defined(object p0)
		{
			return EntityRes.GetString("Mapping_QueryView_TypeName_Not_Defined", new object[]
			{
				p0
			});
		}

		// Token: 0x0600118F RID: 4495 RVA: 0x000441B4 File Offset: 0x000423B4
		internal static string Mapping_QueryView_For_Base_Type(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_QueryView_For_Base_Type", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06001190 RID: 4496 RVA: 0x000441CE File Offset: 0x000423CE
		internal static string Mapping_UnsupportedExpressionKind_QueryView(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Mapping_UnsupportedExpressionKind_QueryView", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06001191 RID: 4497 RVA: 0x000441EC File Offset: 0x000423EC
		internal static string Mapping_UnsupportedFunctionCall_QueryView(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_UnsupportedFunctionCall_QueryView", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06001192 RID: 4498 RVA: 0x00044206 File Offset: 0x00042406
		internal static string Mapping_UnsupportedScanTarget_QueryView(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_UnsupportedScanTarget_QueryView", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06001193 RID: 4499 RVA: 0x00044220 File Offset: 0x00042420
		internal static string Mapping_UnsupportedPropertyKind_QueryView(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Mapping_UnsupportedPropertyKind_QueryView", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06001194 RID: 4500 RVA: 0x0004423E File Offset: 0x0004243E
		internal static string Mapping_UnsupportedInitialization_QueryView(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_UnsupportedInitialization_QueryView", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06001195 RID: 4501 RVA: 0x00044258 File Offset: 0x00042458
		internal static string Mapping_EntitySetMismatchOnAssociationSetEnd_QueryView(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("Mapping_EntitySetMismatchOnAssociationSetEnd_QueryView", new object[]
			{
				p0,
				p1,
				p2,
				p3
			});
		}

		// Token: 0x06001196 RID: 4502 RVA: 0x0004427A File Offset: 0x0004247A
		internal static string Mapping_Invalid_Query_Views_MissingSetClosure(object p0)
		{
			return EntityRes.GetString("Mapping_Invalid_Query_Views_MissingSetClosure", new object[]
			{
				p0
			});
		}

		// Token: 0x06001197 RID: 4503 RVA: 0x00044290 File Offset: 0x00042490
		internal static string Generated_View_Type_Super_Class(object p0)
		{
			return EntityRes.GetString("Generated_View_Type_Super_Class", new object[]
			{
				p0
			});
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06001198 RID: 4504 RVA: 0x000442A6 File Offset: 0x000424A6
		internal static string Generated_Views_Changed
		{
			get
			{
				return EntityRes.GetString("Generated_Views_Changed");
			}
		}

		// Token: 0x06001199 RID: 4505 RVA: 0x000442B2 File Offset: 0x000424B2
		internal static string Generated_Views_Invalid_Extent(object p0)
		{
			return EntityRes.GetString("Generated_Views_Invalid_Extent", new object[]
			{
				p0
			});
		}

		// Token: 0x0600119A RID: 4506 RVA: 0x000442C8 File Offset: 0x000424C8
		internal static string Mapping_ItemWithSameNameExistsBothInCSpaceAndSSpace(object p0)
		{
			return EntityRes.GetString("Mapping_ItemWithSameNameExistsBothInCSpaceAndSSpace", new object[]
			{
				p0
			});
		}

		// Token: 0x0600119B RID: 4507 RVA: 0x000442DE File Offset: 0x000424DE
		internal static string Mapping_AbstractTypeMappingToNonAbstractType(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_AbstractTypeMappingToNonAbstractType", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x0600119C RID: 4508 RVA: 0x000442F8 File Offset: 0x000424F8
		internal static string Mapping_EnumTypeMappingToNonEnumType(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_EnumTypeMappingToNonEnumType", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x0600119D RID: 4509 RVA: 0x00044312 File Offset: 0x00042512
		internal static string StorageEntityContainerNameMismatchWhileSpecifyingPartialMapping(object p0, object p1, object p2)
		{
			return EntityRes.GetString("StorageEntityContainerNameMismatchWhileSpecifyingPartialMapping", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x0600119E RID: 4510 RVA: 0x00044330 File Offset: 0x00042530
		internal static string Mapping_InvalidContent_IsTypeOfNotTerminated
		{
			get
			{
				return EntityRes.GetString("Mapping_InvalidContent_IsTypeOfNotTerminated");
			}
		}

		// Token: 0x0600119F RID: 4511 RVA: 0x0004433C File Offset: 0x0004253C
		internal static string Mapping_CannotMapCLRTypeMultipleTimes(object p0)
		{
			return EntityRes.GetString("Mapping_CannotMapCLRTypeMultipleTimes", new object[]
			{
				p0
			});
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x060011A0 RID: 4512 RVA: 0x00044352 File Offset: 0x00042552
		internal static string Mapping_ModificationFunction_In_Table_Context
		{
			get
			{
				return EntityRes.GetString("Mapping_ModificationFunction_In_Table_Context");
			}
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x060011A1 RID: 4513 RVA: 0x0004435E File Offset: 0x0004255E
		internal static string Mapping_ModificationFunction_Multiple_Types
		{
			get
			{
				return EntityRes.GetString("Mapping_ModificationFunction_Multiple_Types");
			}
		}

		// Token: 0x060011A2 RID: 4514 RVA: 0x0004436A File Offset: 0x0004256A
		internal static string Mapping_ModificationFunction_UnknownFunction(object p0)
		{
			return EntityRes.GetString("Mapping_ModificationFunction_UnknownFunction", new object[]
			{
				p0
			});
		}

		// Token: 0x060011A3 RID: 4515 RVA: 0x00044380 File Offset: 0x00042580
		internal static string Mapping_ModificationFunction_AmbiguousFunction(object p0)
		{
			return EntityRes.GetString("Mapping_ModificationFunction_AmbiguousFunction", new object[]
			{
				p0
			});
		}

		// Token: 0x060011A4 RID: 4516 RVA: 0x00044396 File Offset: 0x00042596
		internal static string Mapping_ModificationFunction_NotValidFunction(object p0)
		{
			return EntityRes.GetString("Mapping_ModificationFunction_NotValidFunction", new object[]
			{
				p0
			});
		}

		// Token: 0x060011A5 RID: 4517 RVA: 0x000443AC File Offset: 0x000425AC
		internal static string Mapping_ModificationFunction_NotValidFunctionParameter(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Mapping_ModificationFunction_NotValidFunctionParameter", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x060011A6 RID: 4518 RVA: 0x000443CA File Offset: 0x000425CA
		internal static string Mapping_ModificationFunction_MissingParameter(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_ModificationFunction_MissingParameter", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060011A7 RID: 4519 RVA: 0x000443E4 File Offset: 0x000425E4
		internal static string Mapping_ModificationFunction_AssociationSetDoesNotExist(object p0)
		{
			return EntityRes.GetString("Mapping_ModificationFunction_AssociationSetDoesNotExist", new object[]
			{
				p0
			});
		}

		// Token: 0x060011A8 RID: 4520 RVA: 0x000443FA File Offset: 0x000425FA
		internal static string Mapping_ModificationFunction_AssociationSetRoleDoesNotExist(object p0)
		{
			return EntityRes.GetString("Mapping_ModificationFunction_AssociationSetRoleDoesNotExist", new object[]
			{
				p0
			});
		}

		// Token: 0x060011A9 RID: 4521 RVA: 0x00044410 File Offset: 0x00042610
		internal static string Mapping_ModificationFunction_AssociationSetFromRoleIsNotEntitySet(object p0)
		{
			return EntityRes.GetString("Mapping_ModificationFunction_AssociationSetFromRoleIsNotEntitySet", new object[]
			{
				p0
			});
		}

		// Token: 0x060011AA RID: 4522 RVA: 0x00044426 File Offset: 0x00042626
		internal static string Mapping_ModificationFunction_AssociationSetCardinality(object p0)
		{
			return EntityRes.GetString("Mapping_ModificationFunction_AssociationSetCardinality", new object[]
			{
				p0
			});
		}

		// Token: 0x060011AB RID: 4523 RVA: 0x0004443C File Offset: 0x0004263C
		internal static string Mapping_ModificationFunction_ComplexTypeNotFound(object p0)
		{
			return EntityRes.GetString("Mapping_ModificationFunction_ComplexTypeNotFound", new object[]
			{
				p0
			});
		}

		// Token: 0x060011AC RID: 4524 RVA: 0x00044452 File Offset: 0x00042652
		internal static string Mapping_ModificationFunction_WrongComplexType(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_ModificationFunction_WrongComplexType", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x060011AD RID: 4525 RVA: 0x0004446C File Offset: 0x0004266C
		internal static string Mapping_ModificationFunction_MissingVersion
		{
			get
			{
				return EntityRes.GetString("Mapping_ModificationFunction_MissingVersion");
			}
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x060011AE RID: 4526 RVA: 0x00044478 File Offset: 0x00042678
		internal static string Mapping_ModificationFunction_VersionMustBeOriginal
		{
			get
			{
				return EntityRes.GetString("Mapping_ModificationFunction_VersionMustBeOriginal");
			}
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x060011AF RID: 4527 RVA: 0x00044484 File Offset: 0x00042684
		internal static string Mapping_ModificationFunction_VersionMustBeCurrent
		{
			get
			{
				return EntityRes.GetString("Mapping_ModificationFunction_VersionMustBeCurrent");
			}
		}

		// Token: 0x060011B0 RID: 4528 RVA: 0x00044490 File Offset: 0x00042690
		internal static string Mapping_ModificationFunction_ParameterNotFound(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_ModificationFunction_ParameterNotFound", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060011B1 RID: 4529 RVA: 0x000444AA File Offset: 0x000426AA
		internal static string Mapping_ModificationFunction_PropertyNotFound(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_ModificationFunction_PropertyNotFound", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060011B2 RID: 4530 RVA: 0x000444C4 File Offset: 0x000426C4
		internal static string Mapping_ModificationFunction_PropertyNotKey(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_ModificationFunction_PropertyNotKey", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060011B3 RID: 4531 RVA: 0x000444DE File Offset: 0x000426DE
		internal static string Mapping_ModificationFunction_ParameterBoundTwice(object p0)
		{
			return EntityRes.GetString("Mapping_ModificationFunction_ParameterBoundTwice", new object[]
			{
				p0
			});
		}

		// Token: 0x060011B4 RID: 4532 RVA: 0x000444F4 File Offset: 0x000426F4
		internal static string Mapping_ModificationFunction_RedundantEntityTypeMapping(object p0)
		{
			return EntityRes.GetString("Mapping_ModificationFunction_RedundantEntityTypeMapping", new object[]
			{
				p0
			});
		}

		// Token: 0x060011B5 RID: 4533 RVA: 0x0004450A File Offset: 0x0004270A
		internal static string Mapping_ModificationFunction_MissingSetClosure(object p0)
		{
			return EntityRes.GetString("Mapping_ModificationFunction_MissingSetClosure", new object[]
			{
				p0
			});
		}

		// Token: 0x060011B6 RID: 4534 RVA: 0x00044520 File Offset: 0x00042720
		internal static string Mapping_ModificationFunction_MissingEntityType(object p0)
		{
			return EntityRes.GetString("Mapping_ModificationFunction_MissingEntityType", new object[]
			{
				p0
			});
		}

		// Token: 0x060011B7 RID: 4535 RVA: 0x00044536 File Offset: 0x00042736
		internal static string Mapping_ModificationFunction_PropertyParameterTypeMismatch(object p0, object p1, object p2, object p3, object p4, object p5)
		{
			return EntityRes.GetString("Mapping_ModificationFunction_PropertyParameterTypeMismatch", new object[]
			{
				p0,
				p1,
				p2,
				p3,
				p4,
				p5
			});
		}

		// Token: 0x060011B8 RID: 4536 RVA: 0x00044562 File Offset: 0x00042762
		internal static string Mapping_ModificationFunction_AssociationSetAmbiguous(object p0)
		{
			return EntityRes.GetString("Mapping_ModificationFunction_AssociationSetAmbiguous", new object[]
			{
				p0
			});
		}

		// Token: 0x060011B9 RID: 4537 RVA: 0x00044578 File Offset: 0x00042778
		internal static string Mapping_ModificationFunction_MultipleEndsOfAssociationMapped(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Mapping_ModificationFunction_MultipleEndsOfAssociationMapped", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x060011BA RID: 4538 RVA: 0x00044596 File Offset: 0x00042796
		internal static string Mapping_ModificationFunction_AmbiguousResultBinding(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_ModificationFunction_AmbiguousResultBinding", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060011BB RID: 4539 RVA: 0x000445B0 File Offset: 0x000427B0
		internal static string Mapping_ModificationFunction_AssociationSetNotMappedForOperation(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("Mapping_ModificationFunction_AssociationSetNotMappedForOperation", new object[]
			{
				p0,
				p1,
				p2,
				p3
			});
		}

		// Token: 0x060011BC RID: 4540 RVA: 0x000445D2 File Offset: 0x000427D2
		internal static string Mapping_ModificationFunction_AssociationEndMappingInvalidForEntityType(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Mapping_ModificationFunction_AssociationEndMappingInvalidForEntityType", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x060011BD RID: 4541 RVA: 0x000445F0 File Offset: 0x000427F0
		internal static string Mapping_ModificationFunction_AssociationEndMappingForeignKeyAssociation(object p0)
		{
			return EntityRes.GetString("Mapping_ModificationFunction_AssociationEndMappingForeignKeyAssociation", new object[]
			{
				p0
			});
		}

		// Token: 0x060011BE RID: 4542 RVA: 0x00044606 File Offset: 0x00042806
		internal static string Mapping_StoreTypeMismatch_ScalarPropertyMapping(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_StoreTypeMismatch_ScalarPropertyMapping", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x060011BF RID: 4543 RVA: 0x00044620 File Offset: 0x00042820
		internal static string Mapping_DistinctFlagInReadWriteContainer
		{
			get
			{
				return EntityRes.GetString("Mapping_DistinctFlagInReadWriteContainer");
			}
		}

		// Token: 0x060011C0 RID: 4544 RVA: 0x0004462C File Offset: 0x0004282C
		internal static string Mapping_ProviderReturnsNullType(object p0)
		{
			return EntityRes.GetString("Mapping_ProviderReturnsNullType", new object[]
			{
				p0
			});
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x060011C1 RID: 4545 RVA: 0x00044642 File Offset: 0x00042842
		internal static string Mapping_DifferentEdmStoreVersion
		{
			get
			{
				return EntityRes.GetString("Mapping_DifferentEdmStoreVersion");
			}
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x060011C2 RID: 4546 RVA: 0x0004464E File Offset: 0x0004284E
		internal static string Mapping_DifferentMappingEdmStoreVersion
		{
			get
			{
				return EntityRes.GetString("Mapping_DifferentMappingEdmStoreVersion");
			}
		}

		// Token: 0x060011C3 RID: 4547 RVA: 0x0004465A File Offset: 0x0004285A
		internal static string Mapping_FunctionImport_StoreFunctionDoesNotExist(object p0)
		{
			return EntityRes.GetString("Mapping_FunctionImport_StoreFunctionDoesNotExist", new object[]
			{
				p0
			});
		}

		// Token: 0x060011C4 RID: 4548 RVA: 0x00044670 File Offset: 0x00042870
		internal static string Mapping_FunctionImport_FunctionImportDoesNotExist(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_FunctionImport_FunctionImportDoesNotExist", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060011C5 RID: 4549 RVA: 0x0004468A File Offset: 0x0004288A
		internal static string Mapping_FunctionImport_FunctionImportMappedMultipleTimes(object p0)
		{
			return EntityRes.GetString("Mapping_FunctionImport_FunctionImportMappedMultipleTimes", new object[]
			{
				p0
			});
		}

		// Token: 0x060011C6 RID: 4550 RVA: 0x000446A0 File Offset: 0x000428A0
		internal static string Mapping_FunctionImport_TargetFunctionMustBeNonComposable(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_FunctionImport_TargetFunctionMustBeNonComposable", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060011C7 RID: 4551 RVA: 0x000446BA File Offset: 0x000428BA
		internal static string Mapping_FunctionImport_TargetFunctionMustBeComposable(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_FunctionImport_TargetFunctionMustBeComposable", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060011C8 RID: 4552 RVA: 0x000446D4 File Offset: 0x000428D4
		internal static string Mapping_FunctionImport_TargetParameterHasNoCorrespondingImportParameter(object p0)
		{
			return EntityRes.GetString("Mapping_FunctionImport_TargetParameterHasNoCorrespondingImportParameter", new object[]
			{
				p0
			});
		}

		// Token: 0x060011C9 RID: 4553 RVA: 0x000446EA File Offset: 0x000428EA
		internal static string Mapping_FunctionImport_ImportParameterHasNoCorrespondingTargetParameter(object p0)
		{
			return EntityRes.GetString("Mapping_FunctionImport_ImportParameterHasNoCorrespondingTargetParameter", new object[]
			{
				p0
			});
		}

		// Token: 0x060011CA RID: 4554 RVA: 0x00044700 File Offset: 0x00042900
		internal static string Mapping_FunctionImport_IncompatibleParameterMode(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Mapping_FunctionImport_IncompatibleParameterMode", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x060011CB RID: 4555 RVA: 0x0004471E File Offset: 0x0004291E
		internal static string Mapping_FunctionImport_IncompatibleParameterType(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Mapping_FunctionImport_IncompatibleParameterType", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x060011CC RID: 4556 RVA: 0x0004473C File Offset: 0x0004293C
		internal static string Mapping_FunctionImport_IncompatibleEnumParameterType(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("Mapping_FunctionImport_IncompatibleEnumParameterType", new object[]
			{
				p0,
				p1,
				p2,
				p3
			});
		}

		// Token: 0x060011CD RID: 4557 RVA: 0x0004475E File Offset: 0x0004295E
		internal static string Mapping_FunctionImport_RowsAffectedParameterDoesNotExist(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_FunctionImport_RowsAffectedParameterDoesNotExist", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060011CE RID: 4558 RVA: 0x00044778 File Offset: 0x00042978
		internal static string Mapping_FunctionImport_RowsAffectedParameterHasWrongType(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_FunctionImport_RowsAffectedParameterHasWrongType", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060011CF RID: 4559 RVA: 0x00044792 File Offset: 0x00042992
		internal static string Mapping_FunctionImport_RowsAffectedParameterHasWrongMode(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("Mapping_FunctionImport_RowsAffectedParameterHasWrongMode", new object[]
			{
				p0,
				p1,
				p2,
				p3
			});
		}

		// Token: 0x060011D0 RID: 4560 RVA: 0x000447B4 File Offset: 0x000429B4
		internal static string Mapping_FunctionImport_EntityTypeMappingForFunctionNotReturningEntitySet(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_FunctionImport_EntityTypeMappingForFunctionNotReturningEntitySet", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060011D1 RID: 4561 RVA: 0x000447CE File Offset: 0x000429CE
		internal static string Mapping_FunctionImport_InvalidContentEntityTypeForEntitySet(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("Mapping_FunctionImport_InvalidContentEntityTypeForEntitySet", new object[]
			{
				p0,
				p1,
				p2,
				p3
			});
		}

		// Token: 0x060011D2 RID: 4562 RVA: 0x000447F0 File Offset: 0x000429F0
		internal static string Mapping_FunctionImport_ConditionValueTypeMismatch(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Mapping_FunctionImport_ConditionValueTypeMismatch", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x060011D3 RID: 4563 RVA: 0x0004480E File Offset: 0x00042A0E
		internal static string Mapping_FunctionImport_UnsupportedType(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_FunctionImport_UnsupportedType", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060011D4 RID: 4564 RVA: 0x00044828 File Offset: 0x00042A28
		internal static string Mapping_FunctionImport_ResultMappingCountDoesNotMatchResultCount(object p0)
		{
			return EntityRes.GetString("Mapping_FunctionImport_ResultMappingCountDoesNotMatchResultCount", new object[]
			{
				p0
			});
		}

		// Token: 0x060011D5 RID: 4565 RVA: 0x0004483E File Offset: 0x00042A3E
		internal static string Mapping_FunctionImport_ResultMapping_MappedTypeDoesNotMatchReturnType(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_FunctionImport_ResultMapping_MappedTypeDoesNotMatchReturnType", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060011D6 RID: 4566 RVA: 0x00044858 File Offset: 0x00042A58
		internal static string Mapping_FunctionImport_ResultMapping_InvalidCTypeCTExpected(object p0)
		{
			return EntityRes.GetString("Mapping_FunctionImport_ResultMapping_InvalidCTypeCTExpected", new object[]
			{
				p0
			});
		}

		// Token: 0x060011D7 RID: 4567 RVA: 0x0004486E File Offset: 0x00042A6E
		internal static string Mapping_FunctionImport_ResultMapping_InvalidCTypeETExpected(object p0)
		{
			return EntityRes.GetString("Mapping_FunctionImport_ResultMapping_InvalidCTypeETExpected", new object[]
			{
				p0
			});
		}

		// Token: 0x060011D8 RID: 4568 RVA: 0x00044884 File Offset: 0x00042A84
		internal static string Mapping_FunctionImport_ResultMapping_InvalidSType(object p0)
		{
			return EntityRes.GetString("Mapping_FunctionImport_ResultMapping_InvalidSType", new object[]
			{
				p0
			});
		}

		// Token: 0x060011D9 RID: 4569 RVA: 0x0004489A File Offset: 0x00042A9A
		internal static string Mapping_FunctionImport_PropertyNotMapped(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Mapping_FunctionImport_PropertyNotMapped", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x060011DA RID: 4570 RVA: 0x000448B8 File Offset: 0x00042AB8
		internal static string Mapping_FunctionImport_ImplicitMappingForAbstractReturnType(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_FunctionImport_ImplicitMappingForAbstractReturnType", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060011DB RID: 4571 RVA: 0x000448D2 File Offset: 0x00042AD2
		internal static string Mapping_FunctionImport_ScalarMappingToMulticolumnTVF(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_FunctionImport_ScalarMappingToMulticolumnTVF", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060011DC RID: 4572 RVA: 0x000448EC File Offset: 0x00042AEC
		internal static string Mapping_FunctionImport_ScalarMappingTypeMismatch(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("Mapping_FunctionImport_ScalarMappingTypeMismatch", new object[]
			{
				p0,
				p1,
				p2,
				p3
			});
		}

		// Token: 0x060011DD RID: 4573 RVA: 0x0004490E File Offset: 0x00042B0E
		internal static string Mapping_FunctionImport_UnreachableType(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_FunctionImport_UnreachableType", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060011DE RID: 4574 RVA: 0x00044928 File Offset: 0x00042B28
		internal static string Mapping_FunctionImport_UnreachableIsTypeOf(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_FunctionImport_UnreachableIsTypeOf", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060011DF RID: 4575 RVA: 0x00044942 File Offset: 0x00042B42
		internal static string Mapping_FunctionImport_FunctionAmbiguous(object p0)
		{
			return EntityRes.GetString("Mapping_FunctionImport_FunctionAmbiguous", new object[]
			{
				p0
			});
		}

		// Token: 0x060011E0 RID: 4576 RVA: 0x00044958 File Offset: 0x00042B58
		internal static string Mapping_FunctionImport_CannotInferTargetFunctionKeys(object p0)
		{
			return EntityRes.GetString("Mapping_FunctionImport_CannotInferTargetFunctionKeys", new object[]
			{
				p0
			});
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x060011E1 RID: 4577 RVA: 0x0004496E File Offset: 0x00042B6E
		internal static string SqlProvider_DdlGeneration_MissingInitialCatalog
		{
			get
			{
				return EntityRes.GetString("SqlProvider_DdlGeneration_MissingInitialCatalog");
			}
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x060011E2 RID: 4578 RVA: 0x0004497A File Offset: 0x00042B7A
		internal static string SqlProvider_DdlGeneration_CannotDeleteDatabaseNoInitialCatalog
		{
			get
			{
				return EntityRes.GetString("SqlProvider_DdlGeneration_CannotDeleteDatabaseNoInitialCatalog");
			}
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x060011E3 RID: 4579 RVA: 0x00044986 File Offset: 0x00042B86
		internal static string SqlProvider_DdlGeneration_CannotTellIfDatabaseExists
		{
			get
			{
				return EntityRes.GetString("SqlProvider_DdlGeneration_CannotTellIfDatabaseExists");
			}
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x060011E4 RID: 4580 RVA: 0x00044992 File Offset: 0x00042B92
		internal static string SqlProvider_CredentialsMissingForMasterConnection
		{
			get
			{
				return EntityRes.GetString("SqlProvider_CredentialsMissingForMasterConnection");
			}
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x060011E5 RID: 4581 RVA: 0x0004499E File Offset: 0x00042B9E
		internal static string SqlProvider_IncompleteCreateDatabase
		{
			get
			{
				return EntityRes.GetString("SqlProvider_IncompleteCreateDatabase");
			}
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x060011E6 RID: 4582 RVA: 0x000449AA File Offset: 0x00042BAA
		internal static string SqlProvider_IncompleteCreateDatabaseAggregate
		{
			get
			{
				return EntityRes.GetString("SqlProvider_IncompleteCreateDatabaseAggregate");
			}
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x060011E7 RID: 4583 RVA: 0x000449B6 File Offset: 0x00042BB6
		internal static string SqlProvider_SqlTypesAssemblyNotFound
		{
			get
			{
				return EntityRes.GetString("SqlProvider_SqlTypesAssemblyNotFound");
			}
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x060011E8 RID: 4584 RVA: 0x000449C2 File Offset: 0x00042BC2
		internal static string SqlProvider_Sql2008RequiredForSpatial
		{
			get
			{
				return EntityRes.GetString("SqlProvider_Sql2008RequiredForSpatial");
			}
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x060011E9 RID: 4585 RVA: 0x000449CE File Offset: 0x00042BCE
		internal static string SqlProvider_GeographyValueNotSqlCompatible
		{
			get
			{
				return EntityRes.GetString("SqlProvider_GeographyValueNotSqlCompatible");
			}
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x060011EA RID: 4586 RVA: 0x000449DA File Offset: 0x00042BDA
		internal static string SqlProvider_GeometryValueNotSqlCompatible
		{
			get
			{
				return EntityRes.GetString("SqlProvider_GeometryValueNotSqlCompatible");
			}
		}

		// Token: 0x060011EB RID: 4587 RVA: 0x000449E6 File Offset: 0x00042BE6
		internal static string SqlProvider_NeedSqlDataReader(object p0)
		{
			return EntityRes.GetString("SqlProvider_NeedSqlDataReader", new object[]
			{
				p0
			});
		}

		// Token: 0x060011EC RID: 4588 RVA: 0x000449FC File Offset: 0x00042BFC
		internal static string SqlProvider_InvalidGeographyColumn(object p0)
		{
			return EntityRes.GetString("SqlProvider_InvalidGeographyColumn", new object[]
			{
				p0
			});
		}

		// Token: 0x060011ED RID: 4589 RVA: 0x00044A12 File Offset: 0x00042C12
		internal static string SqlProvider_InvalidGeometryColumn(object p0)
		{
			return EntityRes.GetString("SqlProvider_InvalidGeometryColumn", new object[]
			{
				p0
			});
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x060011EE RID: 4590 RVA: 0x00044A28 File Offset: 0x00042C28
		internal static string Entity_EntityCantHaveMultipleChangeTrackers
		{
			get
			{
				return EntityRes.GetString("Entity_EntityCantHaveMultipleChangeTrackers");
			}
		}

		// Token: 0x060011EF RID: 4591 RVA: 0x00044A34 File Offset: 0x00042C34
		internal static string ComplexObject_NullableComplexTypesNotSupported(object p0)
		{
			return EntityRes.GetString("ComplexObject_NullableComplexTypesNotSupported", new object[]
			{
				p0
			});
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x060011F0 RID: 4592 RVA: 0x00044A4A File Offset: 0x00042C4A
		internal static string ComplexObject_ComplexObjectAlreadyAttachedToParent
		{
			get
			{
				return EntityRes.GetString("ComplexObject_ComplexObjectAlreadyAttachedToParent");
			}
		}

		// Token: 0x060011F1 RID: 4593 RVA: 0x00044A56 File Offset: 0x00042C56
		internal static string ComplexObject_ComplexChangeRequestedOnScalarProperty(object p0)
		{
			return EntityRes.GetString("ComplexObject_ComplexChangeRequestedOnScalarProperty", new object[]
			{
				p0
			});
		}

		// Token: 0x060011F2 RID: 4594 RVA: 0x00044A6C File Offset: 0x00042C6C
		internal static string ObjectStateEntry_SetModifiedOnInvalidProperty(object p0)
		{
			return EntityRes.GetString("ObjectStateEntry_SetModifiedOnInvalidProperty", new object[]
			{
				p0
			});
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x060011F3 RID: 4595 RVA: 0x00044A82 File Offset: 0x00042C82
		internal static string ObjectStateEntry_OriginalValuesDoesNotExist
		{
			get
			{
				return EntityRes.GetString("ObjectStateEntry_OriginalValuesDoesNotExist");
			}
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x060011F4 RID: 4596 RVA: 0x00044A8E File Offset: 0x00042C8E
		internal static string ObjectStateEntry_CurrentValuesDoesNotExist
		{
			get
			{
				return EntityRes.GetString("ObjectStateEntry_CurrentValuesDoesNotExist");
			}
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x060011F5 RID: 4597 RVA: 0x00044A9A File Offset: 0x00042C9A
		internal static string ObjectStateEntry_InvalidState
		{
			get
			{
				return EntityRes.GetString("ObjectStateEntry_InvalidState");
			}
		}

		// Token: 0x060011F6 RID: 4598 RVA: 0x00044AA6 File Offset: 0x00042CA6
		internal static string ObjectStateEntry_CannotModifyKeyProperty(object p0)
		{
			return EntityRes.GetString("ObjectStateEntry_CannotModifyKeyProperty", new object[]
			{
				p0
			});
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x060011F7 RID: 4599 RVA: 0x00044ABC File Offset: 0x00042CBC
		internal static string ObjectStateEntry_CantModifyRelationValues
		{
			get
			{
				return EntityRes.GetString("ObjectStateEntry_CantModifyRelationValues");
			}
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x060011F8 RID: 4600 RVA: 0x00044AC8 File Offset: 0x00042CC8
		internal static string ObjectStateEntry_CantModifyRelationState
		{
			get
			{
				return EntityRes.GetString("ObjectStateEntry_CantModifyRelationState");
			}
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x060011F9 RID: 4601 RVA: 0x00044AD4 File Offset: 0x00042CD4
		internal static string ObjectStateEntry_CantModifyDetachedDeletedEntries
		{
			get
			{
				return EntityRes.GetString("ObjectStateEntry_CantModifyDetachedDeletedEntries");
			}
		}

		// Token: 0x060011FA RID: 4602 RVA: 0x00044AE0 File Offset: 0x00042CE0
		internal static string ObjectStateEntry_SetModifiedStates(object p0)
		{
			return EntityRes.GetString("ObjectStateEntry_SetModifiedStates", new object[]
			{
				p0
			});
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x060011FB RID: 4603 RVA: 0x00044AF6 File Offset: 0x00042CF6
		internal static string ObjectStateEntry_CantSetEntityKey
		{
			get
			{
				return EntityRes.GetString("ObjectStateEntry_CantSetEntityKey");
			}
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x060011FC RID: 4604 RVA: 0x00044B02 File Offset: 0x00042D02
		internal static string ObjectStateEntry_CannotAccessKeyEntryValues
		{
			get
			{
				return EntityRes.GetString("ObjectStateEntry_CannotAccessKeyEntryValues");
			}
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x060011FD RID: 4605 RVA: 0x00044B0E File Offset: 0x00042D0E
		internal static string ObjectStateEntry_CannotModifyKeyEntryState
		{
			get
			{
				return EntityRes.GetString("ObjectStateEntry_CannotModifyKeyEntryState");
			}
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x060011FE RID: 4606 RVA: 0x00044B1A File Offset: 0x00042D1A
		internal static string ObjectStateEntry_CannotDeleteOnKeyEntry
		{
			get
			{
				return EntityRes.GetString("ObjectStateEntry_CannotDeleteOnKeyEntry");
			}
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x060011FF RID: 4607 RVA: 0x00044B26 File Offset: 0x00042D26
		internal static string ObjectStateEntry_EntityMemberChangedWithoutEntityMemberChanging
		{
			get
			{
				return EntityRes.GetString("ObjectStateEntry_EntityMemberChangedWithoutEntityMemberChanging");
			}
		}

		// Token: 0x06001200 RID: 4608 RVA: 0x00044B32 File Offset: 0x00042D32
		internal static string ObjectStateEntry_ChangeOnUnmappedProperty(object p0)
		{
			return EntityRes.GetString("ObjectStateEntry_ChangeOnUnmappedProperty", new object[]
			{
				p0
			});
		}

		// Token: 0x06001201 RID: 4609 RVA: 0x00044B48 File Offset: 0x00042D48
		internal static string ObjectStateEntry_ChangeOnUnmappedComplexProperty(object p0)
		{
			return EntityRes.GetString("ObjectStateEntry_ChangeOnUnmappedComplexProperty", new object[]
			{
				p0
			});
		}

		// Token: 0x06001202 RID: 4610 RVA: 0x00044B5E File Offset: 0x00042D5E
		internal static string ObjectStateEntry_ChangedInDifferentStateFromChanging(object p0, object p1)
		{
			return EntityRes.GetString("ObjectStateEntry_ChangedInDifferentStateFromChanging", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06001203 RID: 4611 RVA: 0x00044B78 File Offset: 0x00042D78
		internal static string ObjectStateEntry_UnableToEnumerateCollection(object p0, object p1)
		{
			return EntityRes.GetString("ObjectStateEntry_UnableToEnumerateCollection", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06001204 RID: 4612 RVA: 0x00044B92 File Offset: 0x00042D92
		internal static string ObjectStateEntry_RelationshipAndKeyEntriesDoNotHaveRelationshipManagers
		{
			get
			{
				return EntityRes.GetString("ObjectStateEntry_RelationshipAndKeyEntriesDoNotHaveRelationshipManagers");
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06001205 RID: 4613 RVA: 0x00044B9E File Offset: 0x00042D9E
		internal static string ObjectStateEntry_InvalidTypeForComplexTypeProperty
		{
			get
			{
				return EntityRes.GetString("ObjectStateEntry_InvalidTypeForComplexTypeProperty");
			}
		}

		// Token: 0x06001206 RID: 4614 RVA: 0x00044BAA File Offset: 0x00042DAA
		internal static string ObjectStateEntry_ComplexObjectUsedMultipleTimes(object p0, object p1)
		{
			return EntityRes.GetString("ObjectStateEntry_ComplexObjectUsedMultipleTimes", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06001207 RID: 4615 RVA: 0x00044BC4 File Offset: 0x00042DC4
		internal static string ObjectStateEntry_SetOriginalComplexProperties(object p0)
		{
			return EntityRes.GetString("ObjectStateEntry_SetOriginalComplexProperties", new object[]
			{
				p0
			});
		}

		// Token: 0x06001208 RID: 4616 RVA: 0x00044BDA File Offset: 0x00042DDA
		internal static string ObjectStateEntry_NullOriginalValueForNonNullableProperty(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ObjectStateEntry_NullOriginalValueForNonNullableProperty", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06001209 RID: 4617 RVA: 0x00044BF8 File Offset: 0x00042DF8
		internal static string ObjectStateEntry_SetOriginalPrimaryKey(object p0)
		{
			return EntityRes.GetString("ObjectStateEntry_SetOriginalPrimaryKey", new object[]
			{
				p0
			});
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x0600120A RID: 4618 RVA: 0x00044C0E File Offset: 0x00042E0E
		internal static string ObjectStateManager_NoEntryExistForEntityKey
		{
			get
			{
				return EntityRes.GetString("ObjectStateManager_NoEntryExistForEntityKey");
			}
		}

		// Token: 0x0600120B RID: 4619 RVA: 0x00044C1A File Offset: 0x00042E1A
		internal static string ObjectStateManager_NoEntryExistsForObject(object p0)
		{
			return EntityRes.GetString("ObjectStateManager_NoEntryExistsForObject", new object[]
			{
				p0
			});
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x0600120C RID: 4620 RVA: 0x00044C30 File Offset: 0x00042E30
		internal static string ObjectStateManager_EntityNotTracked
		{
			get
			{
				return EntityRes.GetString("ObjectStateManager_EntityNotTracked");
			}
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x0600120D RID: 4621 RVA: 0x00044C3C File Offset: 0x00042E3C
		internal static string ObjectStateManager_DetachedObjectStateEntriesDoesNotExistInObjectStateManager
		{
			get
			{
				return EntityRes.GetString("ObjectStateManager_DetachedObjectStateEntriesDoesNotExistInObjectStateManager");
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x0600120E RID: 4622 RVA: 0x00044C48 File Offset: 0x00042E48
		internal static string ObjectStateManager_ObjectStateManagerContainsThisEntityKey
		{
			get
			{
				return EntityRes.GetString("ObjectStateManager_ObjectStateManagerContainsThisEntityKey");
			}
		}

		// Token: 0x0600120F RID: 4623 RVA: 0x00044C54 File Offset: 0x00042E54
		internal static string ObjectStateManager_DoesnotAllowToReAddUnchangedOrModifiedOrDeletedEntity(object p0)
		{
			return EntityRes.GetString("ObjectStateManager_DoesnotAllowToReAddUnchangedOrModifiedOrDeletedEntity", new object[]
			{
				p0
			});
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06001210 RID: 4624 RVA: 0x00044C6A File Offset: 0x00042E6A
		internal static string ObjectStateManager_CannotFixUpKeyToExistingValues
		{
			get
			{
				return EntityRes.GetString("ObjectStateManager_CannotFixUpKeyToExistingValues");
			}
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06001211 RID: 4625 RVA: 0x00044C76 File Offset: 0x00042E76
		internal static string ObjectStateManager_KeyPropertyDoesntMatchValueInKey
		{
			get
			{
				return EntityRes.GetString("ObjectStateManager_KeyPropertyDoesntMatchValueInKey");
			}
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06001212 RID: 4626 RVA: 0x00044C82 File Offset: 0x00042E82
		internal static string ObjectStateManager_KeyPropertyDoesntMatchValueInKeyForAttach
		{
			get
			{
				return EntityRes.GetString("ObjectStateManager_KeyPropertyDoesntMatchValueInKeyForAttach");
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06001213 RID: 4627 RVA: 0x00044C8E File Offset: 0x00042E8E
		internal static string ObjectStateManager_InvalidKey
		{
			get
			{
				return EntityRes.GetString("ObjectStateManager_InvalidKey");
			}
		}

		// Token: 0x06001214 RID: 4628 RVA: 0x00044C9A File Offset: 0x00042E9A
		internal static string ObjectStateManager_EntityTypeDoesnotMatchtoEntitySetType(object p0, object p1)
		{
			return EntityRes.GetString("ObjectStateManager_EntityTypeDoesnotMatchtoEntitySetType", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06001215 RID: 4629 RVA: 0x00044CB4 File Offset: 0x00042EB4
		internal static string ObjectStateManager_GetEntityKeyRequiresObjectToHaveAKey(object p0)
		{
			return EntityRes.GetString("ObjectStateManager_GetEntityKeyRequiresObjectToHaveAKey", new object[]
			{
				p0
			});
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06001216 RID: 4630 RVA: 0x00044CCA File Offset: 0x00042ECA
		internal static string ObjectStateManager_AcceptChangesEntityKeyIsNotValid
		{
			get
			{
				return EntityRes.GetString("ObjectStateManager_AcceptChangesEntityKeyIsNotValid");
			}
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06001217 RID: 4631 RVA: 0x00044CD6 File Offset: 0x00042ED6
		internal static string ObjectStateManager_EntityConflictsWithKeyEntry
		{
			get
			{
				return EntityRes.GetString("ObjectStateManager_EntityConflictsWithKeyEntry");
			}
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06001218 RID: 4632 RVA: 0x00044CE2 File Offset: 0x00042EE2
		internal static string ObjectStateManager_CannotGetRelationshipManagerForDetachedPocoEntity
		{
			get
			{
				return EntityRes.GetString("ObjectStateManager_CannotGetRelationshipManagerForDetachedPocoEntity");
			}
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06001219 RID: 4633 RVA: 0x00044CEE File Offset: 0x00042EEE
		internal static string ObjectStateManager_CannotChangeRelationshipStateEntityDeleted
		{
			get
			{
				return EntityRes.GetString("ObjectStateManager_CannotChangeRelationshipStateEntityDeleted");
			}
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x0600121A RID: 4634 RVA: 0x00044CFA File Offset: 0x00042EFA
		internal static string ObjectStateManager_CannotChangeRelationshipStateEntityAdded
		{
			get
			{
				return EntityRes.GetString("ObjectStateManager_CannotChangeRelationshipStateEntityAdded");
			}
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x0600121B RID: 4635 RVA: 0x00044D06 File Offset: 0x00042F06
		internal static string ObjectStateManager_CannotChangeRelationshipStateKeyEntry
		{
			get
			{
				return EntityRes.GetString("ObjectStateManager_CannotChangeRelationshipStateKeyEntry");
			}
		}

		// Token: 0x0600121C RID: 4636 RVA: 0x00044D12 File Offset: 0x00042F12
		internal static string ObjectStateManager_ConflictingChangesOfRelationshipDetected(object p0, object p1)
		{
			return EntityRes.GetString("ObjectStateManager_ConflictingChangesOfRelationshipDetected", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x0600121D RID: 4637 RVA: 0x00044D2C File Offset: 0x00042F2C
		internal static string ObjectStateManager_ChangeRelationshipStateNotSupportedForForeignKeyAssociations
		{
			get
			{
				return EntityRes.GetString("ObjectStateManager_ChangeRelationshipStateNotSupportedForForeignKeyAssociations");
			}
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x0600121E RID: 4638 RVA: 0x00044D38 File Offset: 0x00042F38
		internal static string ObjectStateManager_ChangeStateFromAddedWithNullKeyIsInvalid
		{
			get
			{
				return EntityRes.GetString("ObjectStateManager_ChangeStateFromAddedWithNullKeyIsInvalid");
			}
		}

		// Token: 0x0600121F RID: 4639 RVA: 0x00044D44 File Offset: 0x00042F44
		internal static string ObjectContext_ClientEntityRemovedFromStore(object p0)
		{
			return EntityRes.GetString("ObjectContext_ClientEntityRemovedFromStore", new object[]
			{
				p0
			});
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06001220 RID: 4640 RVA: 0x00044D5A File Offset: 0x00042F5A
		internal static string ObjectContext_StoreEntityNotPresentInClient
		{
			get
			{
				return EntityRes.GetString("ObjectContext_StoreEntityNotPresentInClient");
			}
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06001221 RID: 4641 RVA: 0x00044D66 File Offset: 0x00042F66
		internal static string ObjectContext_InvalidConnectionString
		{
			get
			{
				return EntityRes.GetString("ObjectContext_InvalidConnectionString");
			}
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06001222 RID: 4642 RVA: 0x00044D72 File Offset: 0x00042F72
		internal static string ObjectContext_InvalidConnection
		{
			get
			{
				return EntityRes.GetString("ObjectContext_InvalidConnection");
			}
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06001223 RID: 4643 RVA: 0x00044D7E File Offset: 0x00042F7E
		internal static string ObjectContext_InvalidDataAdapter
		{
			get
			{
				return EntityRes.GetString("ObjectContext_InvalidDataAdapter");
			}
		}

		// Token: 0x06001224 RID: 4644 RVA: 0x00044D8A File Offset: 0x00042F8A
		internal static string ObjectContext_InvalidDefaultContainerName(object p0)
		{
			return EntityRes.GetString("ObjectContext_InvalidDefaultContainerName", new object[]
			{
				p0
			});
		}

		// Token: 0x06001225 RID: 4645 RVA: 0x00044DA0 File Offset: 0x00042FA0
		internal static string ObjectContext_NthElementInAddedState(object p0)
		{
			return EntityRes.GetString("ObjectContext_NthElementInAddedState", new object[]
			{
				p0
			});
		}

		// Token: 0x06001226 RID: 4646 RVA: 0x00044DB6 File Offset: 0x00042FB6
		internal static string ObjectContext_NthElementIsDuplicate(object p0)
		{
			return EntityRes.GetString("ObjectContext_NthElementIsDuplicate", new object[]
			{
				p0
			});
		}

		// Token: 0x06001227 RID: 4647 RVA: 0x00044DCC File Offset: 0x00042FCC
		internal static string ObjectContext_NthElementIsNull(object p0)
		{
			return EntityRes.GetString("ObjectContext_NthElementIsNull", new object[]
			{
				p0
			});
		}

		// Token: 0x06001228 RID: 4648 RVA: 0x00044DE2 File Offset: 0x00042FE2
		internal static string ObjectContext_NthElementNotInObjectStateManager(object p0)
		{
			return EntityRes.GetString("ObjectContext_NthElementNotInObjectStateManager", new object[]
			{
				p0
			});
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06001229 RID: 4649 RVA: 0x00044DF8 File Offset: 0x00042FF8
		internal static string ObjectContext_ObjectNotFound
		{
			get
			{
				return EntityRes.GetString("ObjectContext_ObjectNotFound");
			}
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x0600122A RID: 4650 RVA: 0x00044E04 File Offset: 0x00043004
		internal static string ObjectContext_CannotDeleteEntityNotInObjectStateManager
		{
			get
			{
				return EntityRes.GetString("ObjectContext_CannotDeleteEntityNotInObjectStateManager");
			}
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x0600122B RID: 4651 RVA: 0x00044E10 File Offset: 0x00043010
		internal static string ObjectContext_CannotDetachEntityNotInObjectStateManager
		{
			get
			{
				return EntityRes.GetString("ObjectContext_CannotDetachEntityNotInObjectStateManager");
			}
		}

		// Token: 0x0600122C RID: 4652 RVA: 0x00044E1C File Offset: 0x0004301C
		internal static string ObjectContext_EntitySetNotFoundForName(object p0)
		{
			return EntityRes.GetString("ObjectContext_EntitySetNotFoundForName", new object[]
			{
				p0
			});
		}

		// Token: 0x0600122D RID: 4653 RVA: 0x00044E32 File Offset: 0x00043032
		internal static string ObjectContext_EntityContainerNotFoundForName(object p0)
		{
			return EntityRes.GetString("ObjectContext_EntityContainerNotFoundForName", new object[]
			{
				p0
			});
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x0600122E RID: 4654 RVA: 0x00044E48 File Offset: 0x00043048
		internal static string ObjectContext_InvalidCommandTimeout
		{
			get
			{
				return EntityRes.GetString("ObjectContext_InvalidCommandTimeout");
			}
		}

		// Token: 0x0600122F RID: 4655 RVA: 0x00044E54 File Offset: 0x00043054
		internal static string ObjectContext_NoMappingForEntityType(object p0)
		{
			return EntityRes.GetString("ObjectContext_NoMappingForEntityType", new object[]
			{
				p0
			});
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06001230 RID: 4656 RVA: 0x00044E6A File Offset: 0x0004306A
		internal static string ObjectContext_EntityAlreadyExistsInObjectStateManager
		{
			get
			{
				return EntityRes.GetString("ObjectContext_EntityAlreadyExistsInObjectStateManager");
			}
		}

		// Token: 0x06001231 RID: 4657 RVA: 0x00044E76 File Offset: 0x00043076
		internal static string ObjectContext_InvalidEntitySetInKey(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("ObjectContext_InvalidEntitySetInKey", new object[]
			{
				p0,
				p1,
				p2,
				p3
			});
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06001232 RID: 4658 RVA: 0x00044E98 File Offset: 0x00043098
		internal static string ObjectContext_CannotAttachEntityWithoutKey
		{
			get
			{
				return EntityRes.GetString("ObjectContext_CannotAttachEntityWithoutKey");
			}
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06001233 RID: 4659 RVA: 0x00044EA4 File Offset: 0x000430A4
		internal static string ObjectContext_CannotAttachEntityWithTemporaryKey
		{
			get
			{
				return EntityRes.GetString("ObjectContext_CannotAttachEntityWithTemporaryKey");
			}
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06001234 RID: 4660 RVA: 0x00044EB0 File Offset: 0x000430B0
		internal static string ObjectContext_EntitySetNameOrEntityKeyRequired
		{
			get
			{
				return EntityRes.GetString("ObjectContext_EntitySetNameOrEntityKeyRequired");
			}
		}

		// Token: 0x06001235 RID: 4661 RVA: 0x00044EBC File Offset: 0x000430BC
		internal static string ObjectContext_ExecuteFunctionTypeMismatch(object p0, object p1)
		{
			return EntityRes.GetString("ObjectContext_ExecuteFunctionTypeMismatch", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06001236 RID: 4662 RVA: 0x00044ED6 File Offset: 0x000430D6
		internal static string ObjectContext_ExecuteFunctionCalledWithScalarFunction(object p0, object p1)
		{
			return EntityRes.GetString("ObjectContext_ExecuteFunctionCalledWithScalarFunction", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06001237 RID: 4663 RVA: 0x00044EF0 File Offset: 0x000430F0
		internal static string ObjectContext_ExecuteFunctionCalledWithNonQueryFunction(object p0)
		{
			return EntityRes.GetString("ObjectContext_ExecuteFunctionCalledWithNonQueryFunction", new object[]
			{
				p0
			});
		}

		// Token: 0x06001238 RID: 4664 RVA: 0x00044F06 File Offset: 0x00043106
		internal static string ObjectContext_ExecuteFunctionCalledWithNullParameter(object p0)
		{
			return EntityRes.GetString("ObjectContext_ExecuteFunctionCalledWithNullParameter", new object[]
			{
				p0
			});
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06001239 RID: 4665 RVA: 0x00044F1C File Offset: 0x0004311C
		internal static string ObjectContext_ContainerQualifiedEntitySetNameRequired
		{
			get
			{
				return EntityRes.GetString("ObjectContext_ContainerQualifiedEntitySetNameRequired");
			}
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x0600123A RID: 4666 RVA: 0x00044F28 File Offset: 0x00043128
		internal static string ObjectContext_CannotSetDefaultContainerName
		{
			get
			{
				return EntityRes.GetString("ObjectContext_CannotSetDefaultContainerName");
			}
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x0600123B RID: 4667 RVA: 0x00044F34 File Offset: 0x00043134
		internal static string ObjectContext_QualfiedEntitySetName
		{
			get
			{
				return EntityRes.GetString("ObjectContext_QualfiedEntitySetName");
			}
		}

		// Token: 0x0600123C RID: 4668 RVA: 0x00044F40 File Offset: 0x00043140
		internal static string ObjectContext_EntitiesHaveDifferentType(object p0, object p1)
		{
			return EntityRes.GetString("ObjectContext_EntitiesHaveDifferentType", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x0600123D RID: 4669 RVA: 0x00044F5A File Offset: 0x0004315A
		internal static string ObjectContext_EntityMustBeUnchangedOrModified(object p0)
		{
			return EntityRes.GetString("ObjectContext_EntityMustBeUnchangedOrModified", new object[]
			{
				p0
			});
		}

		// Token: 0x0600123E RID: 4670 RVA: 0x00044F70 File Offset: 0x00043170
		internal static string ObjectContext_EntityMustBeUnchangedOrModifiedOrDeleted(object p0)
		{
			return EntityRes.GetString("ObjectContext_EntityMustBeUnchangedOrModifiedOrDeleted", new object[]
			{
				p0
			});
		}

		// Token: 0x0600123F RID: 4671 RVA: 0x00044F86 File Offset: 0x00043186
		internal static string ObjectContext_AcceptAllChangesFailure(object p0)
		{
			return EntityRes.GetString("ObjectContext_AcceptAllChangesFailure", new object[]
			{
				p0
			});
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06001240 RID: 4672 RVA: 0x00044F9C File Offset: 0x0004319C
		internal static string ObjectContext_CommitWithConceptualNull
		{
			get
			{
				return EntityRes.GetString("ObjectContext_CommitWithConceptualNull");
			}
		}

		// Token: 0x06001241 RID: 4673 RVA: 0x00044FA8 File Offset: 0x000431A8
		internal static string ObjectContext_InvalidEntitySetOnEntity(object p0, object p1)
		{
			return EntityRes.GetString("ObjectContext_InvalidEntitySetOnEntity", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06001242 RID: 4674 RVA: 0x00044FC2 File Offset: 0x000431C2
		internal static string ObjectContext_InvalidObjectSetTypeForEntitySet(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ObjectContext_InvalidObjectSetTypeForEntitySet", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06001243 RID: 4675 RVA: 0x00044FE0 File Offset: 0x000431E0
		internal static string ObjectContext_RequiredMetadataNotAvailble
		{
			get
			{
				return EntityRes.GetString("ObjectContext_RequiredMetadataNotAvailble");
			}
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06001244 RID: 4676 RVA: 0x00044FEC File Offset: 0x000431EC
		internal static string ObjectContext_MetadataHasChanged
		{
			get
			{
				return EntityRes.GetString("ObjectContext_MetadataHasChanged");
			}
		}

		// Token: 0x06001245 RID: 4677 RVA: 0x00044FF8 File Offset: 0x000431F8
		internal static string ObjectContext_InvalidEntitySetInKeyFromName(object p0, object p1, object p2, object p3, object p4)
		{
			return EntityRes.GetString("ObjectContext_InvalidEntitySetInKeyFromName", new object[]
			{
				p0,
				p1,
				p2,
				p3,
				p4
			});
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06001246 RID: 4678 RVA: 0x0004501F File Offset: 0x0004321F
		internal static string ObjectContext_ObjectDisposed
		{
			get
			{
				return EntityRes.GetString("ObjectContext_ObjectDisposed");
			}
		}

		// Token: 0x06001247 RID: 4679 RVA: 0x0004502B File Offset: 0x0004322B
		internal static string ObjectContext_CannotExplicitlyLoadDetachedRelationships(object p0)
		{
			return EntityRes.GetString("ObjectContext_CannotExplicitlyLoadDetachedRelationships", new object[]
			{
				p0
			});
		}

		// Token: 0x06001248 RID: 4680 RVA: 0x00045041 File Offset: 0x00043241
		internal static string ObjectContext_CannotLoadReferencesUsingDifferentContext(object p0)
		{
			return EntityRes.GetString("ObjectContext_CannotLoadReferencesUsingDifferentContext", new object[]
			{
				p0
			});
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06001249 RID: 4681 RVA: 0x00045057 File Offset: 0x00043257
		internal static string ObjectContext_SelectorExpressionMustBeMemberAccess
		{
			get
			{
				return EntityRes.GetString("ObjectContext_SelectorExpressionMustBeMemberAccess");
			}
		}

		// Token: 0x0600124A RID: 4682 RVA: 0x00045063 File Offset: 0x00043263
		internal static string ObjectContext_MultipleEntitySetsFoundInSingleContainer(object p0, object p1)
		{
			return EntityRes.GetString("ObjectContext_MultipleEntitySetsFoundInSingleContainer", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x0600124B RID: 4683 RVA: 0x0004507D File Offset: 0x0004327D
		internal static string ObjectContext_MultipleEntitySetsFoundInAllContainers(object p0)
		{
			return EntityRes.GetString("ObjectContext_MultipleEntitySetsFoundInAllContainers", new object[]
			{
				p0
			});
		}

		// Token: 0x0600124C RID: 4684 RVA: 0x00045093 File Offset: 0x00043293
		internal static string ObjectContext_NoEntitySetFoundForType(object p0)
		{
			return EntityRes.GetString("ObjectContext_NoEntitySetFoundForType", new object[]
			{
				p0
			});
		}

		// Token: 0x0600124D RID: 4685 RVA: 0x000450A9 File Offset: 0x000432A9
		internal static string ObjectContext_EntityNotInObjectSet_Delete(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("ObjectContext_EntityNotInObjectSet_Delete", new object[]
			{
				p0,
				p1,
				p2,
				p3
			});
		}

		// Token: 0x0600124E RID: 4686 RVA: 0x000450CB File Offset: 0x000432CB
		internal static string ObjectContext_EntityNotInObjectSet_Detach(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("ObjectContext_EntityNotInObjectSet_Detach", new object[]
			{
				p0,
				p1,
				p2,
				p3
			});
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x0600124F RID: 4687 RVA: 0x000450ED File Offset: 0x000432ED
		internal static string ObjectContext_InvalidEntityState
		{
			get
			{
				return EntityRes.GetString("ObjectContext_InvalidEntityState");
			}
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06001250 RID: 4688 RVA: 0x000450F9 File Offset: 0x000432F9
		internal static string ObjectContext_InvalidRelationshipState
		{
			get
			{
				return EntityRes.GetString("ObjectContext_InvalidRelationshipState");
			}
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06001251 RID: 4689 RVA: 0x00045105 File Offset: 0x00043305
		internal static string ObjectContext_EntityNotTrackedOrHasTempKey
		{
			get
			{
				return EntityRes.GetString("ObjectContext_EntityNotTrackedOrHasTempKey");
			}
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06001252 RID: 4690 RVA: 0x00045111 File Offset: 0x00043311
		internal static string ObjectContext_ExecuteCommandWithMixOfDbParameterAndValues
		{
			get
			{
				return EntityRes.GetString("ObjectContext_ExecuteCommandWithMixOfDbParameterAndValues");
			}
		}

		// Token: 0x06001253 RID: 4691 RVA: 0x0004511D File Offset: 0x0004331D
		internal static string ObjectContext_InvalidEntitySetForStoreQuery(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ObjectContext_InvalidEntitySetForStoreQuery", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06001254 RID: 4692 RVA: 0x0004513B File Offset: 0x0004333B
		internal static string ObjectContext_InvalidTypeForStoreQuery(object p0)
		{
			return EntityRes.GetString("ObjectContext_InvalidTypeForStoreQuery", new object[]
			{
				p0
			});
		}

		// Token: 0x06001255 RID: 4693 RVA: 0x00045151 File Offset: 0x00043351
		internal static string ObjectContext_TwoPropertiesMappedToSameColumn(object p0, object p1)
		{
			return EntityRes.GetString("ObjectContext_TwoPropertiesMappedToSameColumn", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06001256 RID: 4694 RVA: 0x0004516B File Offset: 0x0004336B
		internal static string RelatedEnd_InvalidOwnerStateForAttach
		{
			get
			{
				return EntityRes.GetString("RelatedEnd_InvalidOwnerStateForAttach");
			}
		}

		// Token: 0x06001257 RID: 4695 RVA: 0x00045177 File Offset: 0x00043377
		internal static string RelatedEnd_InvalidNthElementNullForAttach(object p0)
		{
			return EntityRes.GetString("RelatedEnd_InvalidNthElementNullForAttach", new object[]
			{
				p0
			});
		}

		// Token: 0x06001258 RID: 4696 RVA: 0x0004518D File Offset: 0x0004338D
		internal static string RelatedEnd_InvalidNthElementContextForAttach(object p0)
		{
			return EntityRes.GetString("RelatedEnd_InvalidNthElementContextForAttach", new object[]
			{
				p0
			});
		}

		// Token: 0x06001259 RID: 4697 RVA: 0x000451A3 File Offset: 0x000433A3
		internal static string RelatedEnd_InvalidNthElementStateForAttach(object p0)
		{
			return EntityRes.GetString("RelatedEnd_InvalidNthElementStateForAttach", new object[]
			{
				p0
			});
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x0600125A RID: 4698 RVA: 0x000451B9 File Offset: 0x000433B9
		internal static string RelatedEnd_InvalidEntityContextForAttach
		{
			get
			{
				return EntityRes.GetString("RelatedEnd_InvalidEntityContextForAttach");
			}
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x0600125B RID: 4699 RVA: 0x000451C5 File Offset: 0x000433C5
		internal static string RelatedEnd_InvalidEntityStateForAttach
		{
			get
			{
				return EntityRes.GetString("RelatedEnd_InvalidEntityStateForAttach");
			}
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x0600125C RID: 4700 RVA: 0x000451D1 File Offset: 0x000433D1
		internal static string RelatedEnd_UnableToAddEntity
		{
			get
			{
				return EntityRes.GetString("RelatedEnd_UnableToAddEntity");
			}
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x0600125D RID: 4701 RVA: 0x000451DD File Offset: 0x000433DD
		internal static string RelatedEnd_UnableToRemoveEntity
		{
			get
			{
				return EntityRes.GetString("RelatedEnd_UnableToRemoveEntity");
			}
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x0600125E RID: 4702 RVA: 0x000451E9 File Offset: 0x000433E9
		internal static string RelatedEnd_UnableToAddRelationshipWithDeletedEntity
		{
			get
			{
				return EntityRes.GetString("RelatedEnd_UnableToAddRelationshipWithDeletedEntity");
			}
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x0600125F RID: 4703 RVA: 0x000451F5 File Offset: 0x000433F5
		internal static string RelatedEnd_ConflictingChangeOfRelationshipDetected
		{
			get
			{
				return EntityRes.GetString("RelatedEnd_ConflictingChangeOfRelationshipDetected");
			}
		}

		// Token: 0x06001260 RID: 4704 RVA: 0x00045201 File Offset: 0x00043401
		internal static string RelatedEnd_InvalidRelationshipFixupDetected(object p0, object p1)
		{
			return EntityRes.GetString("RelatedEnd_InvalidRelationshipFixupDetected", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06001261 RID: 4705 RVA: 0x0004521B File Offset: 0x0004341B
		internal static string RelatedEnd_CannotSerialize(object p0)
		{
			return EntityRes.GetString("RelatedEnd_CannotSerialize", new object[]
			{
				p0
			});
		}

		// Token: 0x06001262 RID: 4706 RVA: 0x00045231 File Offset: 0x00043431
		internal static string RelatedEnd_CannotAddToFixedSizeArray(object p0)
		{
			return EntityRes.GetString("RelatedEnd_CannotAddToFixedSizeArray", new object[]
			{
				p0
			});
		}

		// Token: 0x06001263 RID: 4707 RVA: 0x00045247 File Offset: 0x00043447
		internal static string RelatedEnd_CannotRemoveFromFixedSizeArray(object p0)
		{
			return EntityRes.GetString("RelatedEnd_CannotRemoveFromFixedSizeArray", new object[]
			{
				p0
			});
		}

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06001264 RID: 4708 RVA: 0x0004525D File Offset: 0x0004345D
		internal static string Materializer_PropertyIsNotNullable
		{
			get
			{
				return EntityRes.GetString("Materializer_PropertyIsNotNullable");
			}
		}

		// Token: 0x06001265 RID: 4709 RVA: 0x00045269 File Offset: 0x00043469
		internal static string Materializer_PropertyIsNotNullableWithName(object p0)
		{
			return EntityRes.GetString("Materializer_PropertyIsNotNullableWithName", new object[]
			{
				p0
			});
		}

		// Token: 0x06001266 RID: 4710 RVA: 0x0004527F File Offset: 0x0004347F
		internal static string Materializer_SetInvalidValue(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("Materializer_SetInvalidValue", new object[]
			{
				p0,
				p1,
				p2,
				p3
			});
		}

		// Token: 0x06001267 RID: 4711 RVA: 0x000452A1 File Offset: 0x000434A1
		internal static string Materializer_InvalidCastReference(object p0, object p1)
		{
			return EntityRes.GetString("Materializer_InvalidCastReference", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06001268 RID: 4712 RVA: 0x000452BB File Offset: 0x000434BB
		internal static string Materializer_InvalidCastNullable(object p0, object p1)
		{
			return EntityRes.GetString("Materializer_InvalidCastNullable", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06001269 RID: 4713 RVA: 0x000452D5 File Offset: 0x000434D5
		internal static string Materializer_NullReferenceCast(object p0)
		{
			return EntityRes.GetString("Materializer_NullReferenceCast", new object[]
			{
				p0
			});
		}

		// Token: 0x0600126A RID: 4714 RVA: 0x000452EB File Offset: 0x000434EB
		internal static string Materializer_RecyclingEntity(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("Materializer_RecyclingEntity", new object[]
			{
				p0,
				p1,
				p2,
				p3
			});
		}

		// Token: 0x0600126B RID: 4715 RVA: 0x0004530D File Offset: 0x0004350D
		internal static string Materializer_AddedEntityAlreadyExists(object p0)
		{
			return EntityRes.GetString("Materializer_AddedEntityAlreadyExists", new object[]
			{
				p0
			});
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x0600126C RID: 4716 RVA: 0x00045323 File Offset: 0x00043523
		internal static string Materializer_CannotReEnumerateQueryResults
		{
			get
			{
				return EntityRes.GetString("Materializer_CannotReEnumerateQueryResults");
			}
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x0600126D RID: 4717 RVA: 0x0004532F File Offset: 0x0004352F
		internal static string Materializer_UnsupportedType
		{
			get
			{
				return EntityRes.GetString("Materializer_UnsupportedType");
			}
		}

		// Token: 0x0600126E RID: 4718 RVA: 0x0004533B File Offset: 0x0004353B
		internal static string Collections_NoRelationshipSetMatched(object p0)
		{
			return EntityRes.GetString("Collections_NoRelationshipSetMatched", new object[]
			{
				p0
			});
		}

		// Token: 0x0600126F RID: 4719 RVA: 0x00045351 File Offset: 0x00043551
		internal static string Collections_ExpectedCollectionGotReference(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Collections_ExpectedCollectionGotReference", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06001270 RID: 4720 RVA: 0x0004536F File Offset: 0x0004356F
		internal static string Collections_InvalidEntityStateSource
		{
			get
			{
				return EntityRes.GetString("Collections_InvalidEntityStateSource");
			}
		}

		// Token: 0x06001271 RID: 4721 RVA: 0x0004537B File Offset: 0x0004357B
		internal static string Collections_InvalidEntityStateLoad(object p0)
		{
			return EntityRes.GetString("Collections_InvalidEntityStateLoad", new object[]
			{
				p0
			});
		}

		// Token: 0x06001272 RID: 4722 RVA: 0x00045391 File Offset: 0x00043591
		internal static string Collections_CannotFillTryDifferentMergeOption(object p0, object p1)
		{
			return EntityRes.GetString("Collections_CannotFillTryDifferentMergeOption", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06001273 RID: 4723 RVA: 0x000453AB File Offset: 0x000435AB
		internal static string Collections_UnableToMergeCollections
		{
			get
			{
				return EntityRes.GetString("Collections_UnableToMergeCollections");
			}
		}

		// Token: 0x06001274 RID: 4724 RVA: 0x000453B7 File Offset: 0x000435B7
		internal static string EntityReference_ExpectedReferenceGotCollection(object p0, object p1, object p2)
		{
			return EntityRes.GetString("EntityReference_ExpectedReferenceGotCollection", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06001275 RID: 4725 RVA: 0x000453D5 File Offset: 0x000435D5
		internal static string EntityReference_CannotAddMoreThanOneEntityToEntityReference(object p0, object p1)
		{
			return EntityRes.GetString("EntityReference_CannotAddMoreThanOneEntityToEntityReference", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06001276 RID: 4726 RVA: 0x000453EF File Offset: 0x000435EF
		internal static string EntityReference_LessThanExpectedRelatedEntitiesFound
		{
			get
			{
				return EntityRes.GetString("EntityReference_LessThanExpectedRelatedEntitiesFound");
			}
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06001277 RID: 4727 RVA: 0x000453FB File Offset: 0x000435FB
		internal static string EntityReference_MoreThanExpectedRelatedEntitiesFound
		{
			get
			{
				return EntityRes.GetString("EntityReference_MoreThanExpectedRelatedEntitiesFound");
			}
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06001278 RID: 4728 RVA: 0x00045407 File Offset: 0x00043607
		internal static string EntityReference_CannotChangeReferentialConstraintProperty
		{
			get
			{
				return EntityRes.GetString("EntityReference_CannotChangeReferentialConstraintProperty");
			}
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06001279 RID: 4729 RVA: 0x00045413 File Offset: 0x00043613
		internal static string EntityReference_CannotSetSpecialKeys
		{
			get
			{
				return EntityRes.GetString("EntityReference_CannotSetSpecialKeys");
			}
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x0600127A RID: 4730 RVA: 0x0004541F File Offset: 0x0004361F
		internal static string EntityReference_EntityKeyValueMismatch
		{
			get
			{
				return EntityRes.GetString("EntityReference_EntityKeyValueMismatch");
			}
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x0600127B RID: 4731 RVA: 0x0004542B File Offset: 0x0004362B
		internal static string RelatedEnd_RelatedEndNotFound
		{
			get
			{
				return EntityRes.GetString("RelatedEnd_RelatedEndNotFound");
			}
		}

		// Token: 0x0600127C RID: 4732 RVA: 0x00045437 File Offset: 0x00043637
		internal static string RelatedEnd_RelatedEndNotAttachedToContext(object p0)
		{
			return EntityRes.GetString("RelatedEnd_RelatedEndNotAttachedToContext", new object[]
			{
				p0
			});
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x0600127D RID: 4733 RVA: 0x0004544D File Offset: 0x0004364D
		internal static string RelatedEnd_LoadCalledOnNonEmptyNoTrackedRelatedEnd
		{
			get
			{
				return EntityRes.GetString("RelatedEnd_LoadCalledOnNonEmptyNoTrackedRelatedEnd");
			}
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x0600127E RID: 4734 RVA: 0x00045459 File Offset: 0x00043659
		internal static string RelatedEnd_LoadCalledOnAlreadyLoadedNoTrackedRelatedEnd
		{
			get
			{
				return EntityRes.GetString("RelatedEnd_LoadCalledOnAlreadyLoadedNoTrackedRelatedEnd");
			}
		}

		// Token: 0x0600127F RID: 4735 RVA: 0x00045465 File Offset: 0x00043665
		internal static string RelatedEnd_InvalidContainedType_Collection(object p0, object p1)
		{
			return EntityRes.GetString("RelatedEnd_InvalidContainedType_Collection", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06001280 RID: 4736 RVA: 0x0004547F File Offset: 0x0004367F
		internal static string RelatedEnd_InvalidContainedType_Reference(object p0, object p1)
		{
			return EntityRes.GetString("RelatedEnd_InvalidContainedType_Reference", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06001281 RID: 4737 RVA: 0x00045499 File Offset: 0x00043699
		internal static string RelatedEnd_CannotCreateRelationshipBetweenTrackedAndNoTrackedEntities(object p0)
		{
			return EntityRes.GetString("RelatedEnd_CannotCreateRelationshipBetweenTrackedAndNoTrackedEntities", new object[]
			{
				p0
			});
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06001282 RID: 4738 RVA: 0x000454AF File Offset: 0x000436AF
		internal static string RelatedEnd_CannotCreateRelationshipEntitiesInDifferentContexts
		{
			get
			{
				return EntityRes.GetString("RelatedEnd_CannotCreateRelationshipEntitiesInDifferentContexts");
			}
		}

		// Token: 0x06001283 RID: 4739 RVA: 0x000454BB File Offset: 0x000436BB
		internal static string RelatedEnd_MismatchedMergeOptionOnLoad(object p0)
		{
			return EntityRes.GetString("RelatedEnd_MismatchedMergeOptionOnLoad", new object[]
			{
				p0
			});
		}

		// Token: 0x06001284 RID: 4740 RVA: 0x000454D1 File Offset: 0x000436D1
		internal static string RelatedEnd_EntitySetIsNotValidForRelationship(object p0, object p1, object p2, object p3, object p4)
		{
			return EntityRes.GetString("RelatedEnd_EntitySetIsNotValidForRelationship", new object[]
			{
				p0,
				p1,
				p2,
				p3,
				p4
			});
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06001285 RID: 4741 RVA: 0x000454F8 File Offset: 0x000436F8
		internal static string RelatedEnd_OwnerIsNull
		{
			get
			{
				return EntityRes.GetString("RelatedEnd_OwnerIsNull");
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06001286 RID: 4742 RVA: 0x00045504 File Offset: 0x00043704
		internal static string RelationshipManager_UnableToRetrieveReferentialConstraintProperties
		{
			get
			{
				return EntityRes.GetString("RelationshipManager_UnableToRetrieveReferentialConstraintProperties");
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06001287 RID: 4743 RVA: 0x00045510 File Offset: 0x00043710
		internal static string RelationshipManager_InconsistentReferentialConstraintProperties
		{
			get
			{
				return EntityRes.GetString("RelationshipManager_InconsistentReferentialConstraintProperties");
			}
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06001288 RID: 4744 RVA: 0x0004551C File Offset: 0x0004371C
		internal static string RelationshipManager_CircularRelationshipsWithReferentialConstraints
		{
			get
			{
				return EntityRes.GetString("RelationshipManager_CircularRelationshipsWithReferentialConstraints");
			}
		}

		// Token: 0x06001289 RID: 4745 RVA: 0x00045528 File Offset: 0x00043728
		internal static string RelationshipManager_UnableToFindRelationshipTypeInMetadata(object p0)
		{
			return EntityRes.GetString("RelationshipManager_UnableToFindRelationshipTypeInMetadata", new object[]
			{
				p0
			});
		}

		// Token: 0x0600128A RID: 4746 RVA: 0x0004553E File Offset: 0x0004373E
		internal static string RelationshipManager_InvalidTargetRole(object p0, object p1)
		{
			return EntityRes.GetString("RelationshipManager_InvalidTargetRole", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x0600128B RID: 4747 RVA: 0x00045558 File Offset: 0x00043758
		internal static string RelationshipManager_UnexpectedNull
		{
			get
			{
				return EntityRes.GetString("RelationshipManager_UnexpectedNull");
			}
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x0600128C RID: 4748 RVA: 0x00045564 File Offset: 0x00043764
		internal static string RelationshipManager_InvalidRelationshipManagerOwner
		{
			get
			{
				return EntityRes.GetString("RelationshipManager_InvalidRelationshipManagerOwner");
			}
		}

		// Token: 0x0600128D RID: 4749 RVA: 0x00045570 File Offset: 0x00043770
		internal static string RelationshipManager_OwnerIsNotSourceType(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("RelationshipManager_OwnerIsNotSourceType", new object[]
			{
				p0,
				p1,
				p2,
				p3
			});
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x0600128E RID: 4750 RVA: 0x00045592 File Offset: 0x00043792
		internal static string RelationshipManager_UnexpectedNullContext
		{
			get
			{
				return EntityRes.GetString("RelationshipManager_UnexpectedNullContext");
			}
		}

		// Token: 0x0600128F RID: 4751 RVA: 0x0004559E File Offset: 0x0004379E
		internal static string RelationshipManager_ReferenceAlreadyInitialized(object p0)
		{
			return EntityRes.GetString("RelationshipManager_ReferenceAlreadyInitialized", new object[]
			{
				p0
			});
		}

		// Token: 0x06001290 RID: 4752 RVA: 0x000455B4 File Offset: 0x000437B4
		internal static string RelationshipManager_RelationshipManagerAttached(object p0)
		{
			return EntityRes.GetString("RelationshipManager_RelationshipManagerAttached", new object[]
			{
				p0
			});
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06001291 RID: 4753 RVA: 0x000455CA File Offset: 0x000437CA
		internal static string RelationshipManager_InitializeIsForDeserialization
		{
			get
			{
				return EntityRes.GetString("RelationshipManager_InitializeIsForDeserialization");
			}
		}

		// Token: 0x06001292 RID: 4754 RVA: 0x000455D6 File Offset: 0x000437D6
		internal static string RelationshipManager_CollectionAlreadyInitialized(object p0)
		{
			return EntityRes.GetString("RelationshipManager_CollectionAlreadyInitialized", new object[]
			{
				p0
			});
		}

		// Token: 0x06001293 RID: 4755 RVA: 0x000455EC File Offset: 0x000437EC
		internal static string RelationshipManager_CollectionRelationshipManagerAttached(object p0)
		{
			return EntityRes.GetString("RelationshipManager_CollectionRelationshipManagerAttached", new object[]
			{
				p0
			});
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06001294 RID: 4756 RVA: 0x00045602 File Offset: 0x00043802
		internal static string RelationshipManager_CollectionInitializeIsForDeserialization
		{
			get
			{
				return EntityRes.GetString("RelationshipManager_CollectionInitializeIsForDeserialization");
			}
		}

		// Token: 0x06001295 RID: 4757 RVA: 0x0004560E File Offset: 0x0004380E
		internal static string RelationshipManager_NavigationPropertyNotFound(object p0)
		{
			return EntityRes.GetString("RelationshipManager_NavigationPropertyNotFound", new object[]
			{
				p0
			});
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06001296 RID: 4758 RVA: 0x00045624 File Offset: 0x00043824
		internal static string RelationshipManager_CannotGetRelatEndForDetachedPocoEntity
		{
			get
			{
				return EntityRes.GetString("RelationshipManager_CannotGetRelatEndForDetachedPocoEntity");
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06001297 RID: 4759 RVA: 0x00045630 File Offset: 0x00043830
		internal static string ObjectView_CannotReplacetheEntityorRow
		{
			get
			{
				return EntityRes.GetString("ObjectView_CannotReplacetheEntityorRow");
			}
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06001298 RID: 4760 RVA: 0x0004563C File Offset: 0x0004383C
		internal static string ObjectView_IndexBasedInsertIsNotSupported
		{
			get
			{
				return EntityRes.GetString("ObjectView_IndexBasedInsertIsNotSupported");
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06001299 RID: 4761 RVA: 0x00045648 File Offset: 0x00043848
		internal static string ObjectView_WriteOperationNotAllowedOnReadOnlyBindingList
		{
			get
			{
				return EntityRes.GetString("ObjectView_WriteOperationNotAllowedOnReadOnlyBindingList");
			}
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x0600129A RID: 4762 RVA: 0x00045654 File Offset: 0x00043854
		internal static string ObjectView_AddNewOperationNotAllowedOnAbstractBindingList
		{
			get
			{
				return EntityRes.GetString("ObjectView_AddNewOperationNotAllowedOnAbstractBindingList");
			}
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x0600129B RID: 4763 RVA: 0x00045660 File Offset: 0x00043860
		internal static string ObjectView_IncompatibleArgument
		{
			get
			{
				return EntityRes.GetString("ObjectView_IncompatibleArgument");
			}
		}

		// Token: 0x0600129C RID: 4764 RVA: 0x0004566C File Offset: 0x0004386C
		internal static string ObjectView_CannotResolveTheEntitySet(object p0)
		{
			return EntityRes.GetString("ObjectView_CannotResolveTheEntitySet", new object[]
			{
				p0
			});
		}

		// Token: 0x0600129D RID: 4765 RVA: 0x00045682 File Offset: 0x00043882
		internal static string CodeGen_ConstructorNoParameterless(object p0)
		{
			return EntityRes.GetString("CodeGen_ConstructorNoParameterless", new object[]
			{
				p0
			});
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x0600129E RID: 4766 RVA: 0x00045698 File Offset: 0x00043898
		internal static string CodeGen_PropertyDeclaringTypeIsValueType
		{
			get
			{
				return EntityRes.GetString("CodeGen_PropertyDeclaringTypeIsValueType");
			}
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x0600129F RID: 4767 RVA: 0x000456A4 File Offset: 0x000438A4
		internal static string CodeGen_PropertyStrongNameIdentity
		{
			get
			{
				return EntityRes.GetString("CodeGen_PropertyStrongNameIdentity");
			}
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x060012A0 RID: 4768 RVA: 0x000456B0 File Offset: 0x000438B0
		internal static string CodeGen_PropertyUnsupportedForm
		{
			get
			{
				return EntityRes.GetString("CodeGen_PropertyUnsupportedForm");
			}
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x060012A1 RID: 4769 RVA: 0x000456BC File Offset: 0x000438BC
		internal static string CodeGen_PropertyUnsupportedType
		{
			get
			{
				return EntityRes.GetString("CodeGen_PropertyUnsupportedType");
			}
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x060012A2 RID: 4770 RVA: 0x000456C8 File Offset: 0x000438C8
		internal static string CodeGen_PropertyIsIndexed
		{
			get
			{
				return EntityRes.GetString("CodeGen_PropertyIsIndexed");
			}
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x060012A3 RID: 4771 RVA: 0x000456D4 File Offset: 0x000438D4
		internal static string CodeGen_PropertyIsStatic
		{
			get
			{
				return EntityRes.GetString("CodeGen_PropertyIsStatic");
			}
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x060012A4 RID: 4772 RVA: 0x000456E0 File Offset: 0x000438E0
		internal static string CodeGen_PropertyNoGetter
		{
			get
			{
				return EntityRes.GetString("CodeGen_PropertyNoGetter");
			}
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x060012A5 RID: 4773 RVA: 0x000456EC File Offset: 0x000438EC
		internal static string CodeGen_PropertyNoSetter
		{
			get
			{
				return EntityRes.GetString("CodeGen_PropertyNoSetter");
			}
		}

		// Token: 0x060012A6 RID: 4774 RVA: 0x000456F8 File Offset: 0x000438F8
		internal static string PocoEntityWrapper_UnableToSetFieldOrProperty(object p0, object p1)
		{
			return EntityRes.GetString("PocoEntityWrapper_UnableToSetFieldOrProperty", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060012A7 RID: 4775 RVA: 0x00045712 File Offset: 0x00043912
		internal static string PocoEntityWrapper_UnexpectedTypeForNavigationProperty(object p0, object p1)
		{
			return EntityRes.GetString("PocoEntityWrapper_UnexpectedTypeForNavigationProperty", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060012A8 RID: 4776 RVA: 0x0004572C File Offset: 0x0004392C
		internal static string PocoEntityWrapper_UnableToMaterializeArbitaryNavPropType(object p0, object p1)
		{
			return EntityRes.GetString("PocoEntityWrapper_UnableToMaterializeArbitaryNavPropType", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x060012A9 RID: 4777 RVA: 0x00045746 File Offset: 0x00043946
		internal static string GeneralQueryError
		{
			get
			{
				return EntityRes.GetString("GeneralQueryError");
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x060012AA RID: 4778 RVA: 0x00045752 File Offset: 0x00043952
		internal static string CtxAlias
		{
			get
			{
				return EntityRes.GetString("CtxAlias");
			}
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x060012AB RID: 4779 RVA: 0x0004575E File Offset: 0x0004395E
		internal static string CtxAliasedNamespaceImport
		{
			get
			{
				return EntityRes.GetString("CtxAliasedNamespaceImport");
			}
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x060012AC RID: 4780 RVA: 0x0004576A File Offset: 0x0004396A
		internal static string CtxAnd
		{
			get
			{
				return EntityRes.GetString("CtxAnd");
			}
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x060012AD RID: 4781 RVA: 0x00045776 File Offset: 0x00043976
		internal static string CtxAnyElement
		{
			get
			{
				return EntityRes.GetString("CtxAnyElement");
			}
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x060012AE RID: 4782 RVA: 0x00045782 File Offset: 0x00043982
		internal static string CtxApplyClause
		{
			get
			{
				return EntityRes.GetString("CtxApplyClause");
			}
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x060012AF RID: 4783 RVA: 0x0004578E File Offset: 0x0004398E
		internal static string CtxBetween
		{
			get
			{
				return EntityRes.GetString("CtxBetween");
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x060012B0 RID: 4784 RVA: 0x0004579A File Offset: 0x0004399A
		internal static string CtxCase
		{
			get
			{
				return EntityRes.GetString("CtxCase");
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x060012B1 RID: 4785 RVA: 0x000457A6 File Offset: 0x000439A6
		internal static string CtxCaseElse
		{
			get
			{
				return EntityRes.GetString("CtxCaseElse");
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x060012B2 RID: 4786 RVA: 0x000457B2 File Offset: 0x000439B2
		internal static string CtxCaseWhenThen
		{
			get
			{
				return EntityRes.GetString("CtxCaseWhenThen");
			}
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x060012B3 RID: 4787 RVA: 0x000457BE File Offset: 0x000439BE
		internal static string CtxCast
		{
			get
			{
				return EntityRes.GetString("CtxCast");
			}
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x060012B4 RID: 4788 RVA: 0x000457CA File Offset: 0x000439CA
		internal static string CtxCollatedOrderByClauseItem
		{
			get
			{
				return EntityRes.GetString("CtxCollatedOrderByClauseItem");
			}
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x060012B5 RID: 4789 RVA: 0x000457D6 File Offset: 0x000439D6
		internal static string CtxCollectionTypeDefinition
		{
			get
			{
				return EntityRes.GetString("CtxCollectionTypeDefinition");
			}
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x060012B6 RID: 4790 RVA: 0x000457E2 File Offset: 0x000439E2
		internal static string CtxCommandExpression
		{
			get
			{
				return EntityRes.GetString("CtxCommandExpression");
			}
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x060012B7 RID: 4791 RVA: 0x000457EE File Offset: 0x000439EE
		internal static string CtxCreateRef
		{
			get
			{
				return EntityRes.GetString("CtxCreateRef");
			}
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x060012B8 RID: 4792 RVA: 0x000457FA File Offset: 0x000439FA
		internal static string CtxDeref
		{
			get
			{
				return EntityRes.GetString("CtxDeref");
			}
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x060012B9 RID: 4793 RVA: 0x00045806 File Offset: 0x00043A06
		internal static string CtxDivide
		{
			get
			{
				return EntityRes.GetString("CtxDivide");
			}
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x060012BA RID: 4794 RVA: 0x00045812 File Offset: 0x00043A12
		internal static string CtxElement
		{
			get
			{
				return EntityRes.GetString("CtxElement");
			}
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x060012BB RID: 4795 RVA: 0x0004581E File Offset: 0x00043A1E
		internal static string CtxEquals
		{
			get
			{
				return EntityRes.GetString("CtxEquals");
			}
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x060012BC RID: 4796 RVA: 0x0004582A File Offset: 0x00043A2A
		internal static string CtxEscapedIdentifier
		{
			get
			{
				return EntityRes.GetString("CtxEscapedIdentifier");
			}
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x060012BD RID: 4797 RVA: 0x00045836 File Offset: 0x00043A36
		internal static string CtxExcept
		{
			get
			{
				return EntityRes.GetString("CtxExcept");
			}
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x060012BE RID: 4798 RVA: 0x00045842 File Offset: 0x00043A42
		internal static string CtxExists
		{
			get
			{
				return EntityRes.GetString("CtxExists");
			}
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x060012BF RID: 4799 RVA: 0x0004584E File Offset: 0x00043A4E
		internal static string CtxExpressionList
		{
			get
			{
				return EntityRes.GetString("CtxExpressionList");
			}
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x060012C0 RID: 4800 RVA: 0x0004585A File Offset: 0x00043A5A
		internal static string CtxFlatten
		{
			get
			{
				return EntityRes.GetString("CtxFlatten");
			}
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x060012C1 RID: 4801 RVA: 0x00045866 File Offset: 0x00043A66
		internal static string CtxFromApplyClause
		{
			get
			{
				return EntityRes.GetString("CtxFromApplyClause");
			}
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x060012C2 RID: 4802 RVA: 0x00045872 File Offset: 0x00043A72
		internal static string CtxFromClause
		{
			get
			{
				return EntityRes.GetString("CtxFromClause");
			}
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x060012C3 RID: 4803 RVA: 0x0004587E File Offset: 0x00043A7E
		internal static string CtxFromClauseItem
		{
			get
			{
				return EntityRes.GetString("CtxFromClauseItem");
			}
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x060012C4 RID: 4804 RVA: 0x0004588A File Offset: 0x00043A8A
		internal static string CtxFromClauseList
		{
			get
			{
				return EntityRes.GetString("CtxFromClauseList");
			}
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x060012C5 RID: 4805 RVA: 0x00045896 File Offset: 0x00043A96
		internal static string CtxFromJoinClause
		{
			get
			{
				return EntityRes.GetString("CtxFromJoinClause");
			}
		}

		// Token: 0x060012C6 RID: 4806 RVA: 0x000458A2 File Offset: 0x00043AA2
		internal static string CtxFunction(object p0)
		{
			return EntityRes.GetString("CtxFunction", new object[]
			{
				p0
			});
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x060012C7 RID: 4807 RVA: 0x000458B8 File Offset: 0x00043AB8
		internal static string CtxFunctionDefinition
		{
			get
			{
				return EntityRes.GetString("CtxFunctionDefinition");
			}
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x060012C8 RID: 4808 RVA: 0x000458C4 File Offset: 0x00043AC4
		internal static string CtxGreaterThan
		{
			get
			{
				return EntityRes.GetString("CtxGreaterThan");
			}
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x060012C9 RID: 4809 RVA: 0x000458D0 File Offset: 0x00043AD0
		internal static string CtxGreaterThanEqual
		{
			get
			{
				return EntityRes.GetString("CtxGreaterThanEqual");
			}
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x060012CA RID: 4810 RVA: 0x000458DC File Offset: 0x00043ADC
		internal static string CtxGroupByClause
		{
			get
			{
				return EntityRes.GetString("CtxGroupByClause");
			}
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x060012CB RID: 4811 RVA: 0x000458E8 File Offset: 0x00043AE8
		internal static string CtxGroupPartition
		{
			get
			{
				return EntityRes.GetString("CtxGroupPartition");
			}
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x060012CC RID: 4812 RVA: 0x000458F4 File Offset: 0x00043AF4
		internal static string CtxHavingClause
		{
			get
			{
				return EntityRes.GetString("CtxHavingClause");
			}
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x060012CD RID: 4813 RVA: 0x00045900 File Offset: 0x00043B00
		internal static string CtxIdentifier
		{
			get
			{
				return EntityRes.GetString("CtxIdentifier");
			}
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x060012CE RID: 4814 RVA: 0x0004590C File Offset: 0x00043B0C
		internal static string CtxIn
		{
			get
			{
				return EntityRes.GetString("CtxIn");
			}
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x060012CF RID: 4815 RVA: 0x00045918 File Offset: 0x00043B18
		internal static string CtxIntersect
		{
			get
			{
				return EntityRes.GetString("CtxIntersect");
			}
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x060012D0 RID: 4816 RVA: 0x00045924 File Offset: 0x00043B24
		internal static string CtxIsNotNull
		{
			get
			{
				return EntityRes.GetString("CtxIsNotNull");
			}
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x060012D1 RID: 4817 RVA: 0x00045930 File Offset: 0x00043B30
		internal static string CtxIsNotOf
		{
			get
			{
				return EntityRes.GetString("CtxIsNotOf");
			}
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x060012D2 RID: 4818 RVA: 0x0004593C File Offset: 0x00043B3C
		internal static string CtxIsNull
		{
			get
			{
				return EntityRes.GetString("CtxIsNull");
			}
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x060012D3 RID: 4819 RVA: 0x00045948 File Offset: 0x00043B48
		internal static string CtxIsOf
		{
			get
			{
				return EntityRes.GetString("CtxIsOf");
			}
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x060012D4 RID: 4820 RVA: 0x00045954 File Offset: 0x00043B54
		internal static string CtxJoinClause
		{
			get
			{
				return EntityRes.GetString("CtxJoinClause");
			}
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x060012D5 RID: 4821 RVA: 0x00045960 File Offset: 0x00043B60
		internal static string CtxJoinOnClause
		{
			get
			{
				return EntityRes.GetString("CtxJoinOnClause");
			}
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x060012D6 RID: 4822 RVA: 0x0004596C File Offset: 0x00043B6C
		internal static string CtxKey
		{
			get
			{
				return EntityRes.GetString("CtxKey");
			}
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x060012D7 RID: 4823 RVA: 0x00045978 File Offset: 0x00043B78
		internal static string CtxLessThan
		{
			get
			{
				return EntityRes.GetString("CtxLessThan");
			}
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x060012D8 RID: 4824 RVA: 0x00045984 File Offset: 0x00043B84
		internal static string CtxLessThanEqual
		{
			get
			{
				return EntityRes.GetString("CtxLessThanEqual");
			}
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x060012D9 RID: 4825 RVA: 0x00045990 File Offset: 0x00043B90
		internal static string CtxLike
		{
			get
			{
				return EntityRes.GetString("CtxLike");
			}
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x060012DA RID: 4826 RVA: 0x0004599C File Offset: 0x00043B9C
		internal static string CtxLimitSubClause
		{
			get
			{
				return EntityRes.GetString("CtxLimitSubClause");
			}
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x060012DB RID: 4827 RVA: 0x000459A8 File Offset: 0x00043BA8
		internal static string CtxLiteral
		{
			get
			{
				return EntityRes.GetString("CtxLiteral");
			}
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x060012DC RID: 4828 RVA: 0x000459B4 File Offset: 0x00043BB4
		internal static string CtxMemberAccess
		{
			get
			{
				return EntityRes.GetString("CtxMemberAccess");
			}
		}

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x060012DD RID: 4829 RVA: 0x000459C0 File Offset: 0x00043BC0
		internal static string CtxMethod
		{
			get
			{
				return EntityRes.GetString("CtxMethod");
			}
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x060012DE RID: 4830 RVA: 0x000459CC File Offset: 0x00043BCC
		internal static string CtxMinus
		{
			get
			{
				return EntityRes.GetString("CtxMinus");
			}
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x060012DF RID: 4831 RVA: 0x000459D8 File Offset: 0x00043BD8
		internal static string CtxModulus
		{
			get
			{
				return EntityRes.GetString("CtxModulus");
			}
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x060012E0 RID: 4832 RVA: 0x000459E4 File Offset: 0x00043BE4
		internal static string CtxMultiply
		{
			get
			{
				return EntityRes.GetString("CtxMultiply");
			}
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x060012E1 RID: 4833 RVA: 0x000459F0 File Offset: 0x00043BF0
		internal static string CtxMultisetCtor
		{
			get
			{
				return EntityRes.GetString("CtxMultisetCtor");
			}
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x060012E2 RID: 4834 RVA: 0x000459FC File Offset: 0x00043BFC
		internal static string CtxNamespaceImport
		{
			get
			{
				return EntityRes.GetString("CtxNamespaceImport");
			}
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x060012E3 RID: 4835 RVA: 0x00045A08 File Offset: 0x00043C08
		internal static string CtxNamespaceImportList
		{
			get
			{
				return EntityRes.GetString("CtxNamespaceImportList");
			}
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x060012E4 RID: 4836 RVA: 0x00045A14 File Offset: 0x00043C14
		internal static string CtxNavigate
		{
			get
			{
				return EntityRes.GetString("CtxNavigate");
			}
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x060012E5 RID: 4837 RVA: 0x00045A20 File Offset: 0x00043C20
		internal static string CtxNot
		{
			get
			{
				return EntityRes.GetString("CtxNot");
			}
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x060012E6 RID: 4838 RVA: 0x00045A2C File Offset: 0x00043C2C
		internal static string CtxNotBetween
		{
			get
			{
				return EntityRes.GetString("CtxNotBetween");
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x060012E7 RID: 4839 RVA: 0x00045A38 File Offset: 0x00043C38
		internal static string CtxNotEqual
		{
			get
			{
				return EntityRes.GetString("CtxNotEqual");
			}
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x060012E8 RID: 4840 RVA: 0x00045A44 File Offset: 0x00043C44
		internal static string CtxNotIn
		{
			get
			{
				return EntityRes.GetString("CtxNotIn");
			}
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x060012E9 RID: 4841 RVA: 0x00045A50 File Offset: 0x00043C50
		internal static string CtxNotLike
		{
			get
			{
				return EntityRes.GetString("CtxNotLike");
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x060012EA RID: 4842 RVA: 0x00045A5C File Offset: 0x00043C5C
		internal static string CtxNullLiteral
		{
			get
			{
				return EntityRes.GetString("CtxNullLiteral");
			}
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x060012EB RID: 4843 RVA: 0x00045A68 File Offset: 0x00043C68
		internal static string CtxOfType
		{
			get
			{
				return EntityRes.GetString("CtxOfType");
			}
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x060012EC RID: 4844 RVA: 0x00045A74 File Offset: 0x00043C74
		internal static string CtxOfTypeOnly
		{
			get
			{
				return EntityRes.GetString("CtxOfTypeOnly");
			}
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x060012ED RID: 4845 RVA: 0x00045A80 File Offset: 0x00043C80
		internal static string CtxOr
		{
			get
			{
				return EntityRes.GetString("CtxOr");
			}
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x060012EE RID: 4846 RVA: 0x00045A8C File Offset: 0x00043C8C
		internal static string CtxOrderByClause
		{
			get
			{
				return EntityRes.GetString("CtxOrderByClause");
			}
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x060012EF RID: 4847 RVA: 0x00045A98 File Offset: 0x00043C98
		internal static string CtxOrderByClauseItem
		{
			get
			{
				return EntityRes.GetString("CtxOrderByClauseItem");
			}
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x060012F0 RID: 4848 RVA: 0x00045AA4 File Offset: 0x00043CA4
		internal static string CtxOverlaps
		{
			get
			{
				return EntityRes.GetString("CtxOverlaps");
			}
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x060012F1 RID: 4849 RVA: 0x00045AB0 File Offset: 0x00043CB0
		internal static string CtxParen
		{
			get
			{
				return EntityRes.GetString("CtxParen");
			}
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x060012F2 RID: 4850 RVA: 0x00045ABC File Offset: 0x00043CBC
		internal static string CtxPlus
		{
			get
			{
				return EntityRes.GetString("CtxPlus");
			}
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x060012F3 RID: 4851 RVA: 0x00045AC8 File Offset: 0x00043CC8
		internal static string CtxTypeNameWithTypeSpec
		{
			get
			{
				return EntityRes.GetString("CtxTypeNameWithTypeSpec");
			}
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x060012F4 RID: 4852 RVA: 0x00045AD4 File Offset: 0x00043CD4
		internal static string CtxQueryExpression
		{
			get
			{
				return EntityRes.GetString("CtxQueryExpression");
			}
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x060012F5 RID: 4853 RVA: 0x00045AE0 File Offset: 0x00043CE0
		internal static string CtxQueryStatement
		{
			get
			{
				return EntityRes.GetString("CtxQueryStatement");
			}
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x060012F6 RID: 4854 RVA: 0x00045AEC File Offset: 0x00043CEC
		internal static string CtxRef
		{
			get
			{
				return EntityRes.GetString("CtxRef");
			}
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x060012F7 RID: 4855 RVA: 0x00045AF8 File Offset: 0x00043CF8
		internal static string CtxRefTypeDefinition
		{
			get
			{
				return EntityRes.GetString("CtxRefTypeDefinition");
			}
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x060012F8 RID: 4856 RVA: 0x00045B04 File Offset: 0x00043D04
		internal static string CtxRelationship
		{
			get
			{
				return EntityRes.GetString("CtxRelationship");
			}
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x060012F9 RID: 4857 RVA: 0x00045B10 File Offset: 0x00043D10
		internal static string CtxRelationshipList
		{
			get
			{
				return EntityRes.GetString("CtxRelationshipList");
			}
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x060012FA RID: 4858 RVA: 0x00045B1C File Offset: 0x00043D1C
		internal static string CtxRowCtor
		{
			get
			{
				return EntityRes.GetString("CtxRowCtor");
			}
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x060012FB RID: 4859 RVA: 0x00045B28 File Offset: 0x00043D28
		internal static string CtxRowTypeDefinition
		{
			get
			{
				return EntityRes.GetString("CtxRowTypeDefinition");
			}
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x060012FC RID: 4860 RVA: 0x00045B34 File Offset: 0x00043D34
		internal static string CtxSelectRowClause
		{
			get
			{
				return EntityRes.GetString("CtxSelectRowClause");
			}
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x060012FD RID: 4861 RVA: 0x00045B40 File Offset: 0x00043D40
		internal static string CtxSelectValueClause
		{
			get
			{
				return EntityRes.GetString("CtxSelectValueClause");
			}
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x060012FE RID: 4862 RVA: 0x00045B4C File Offset: 0x00043D4C
		internal static string CtxSet
		{
			get
			{
				return EntityRes.GetString("CtxSet");
			}
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x060012FF RID: 4863 RVA: 0x00045B58 File Offset: 0x00043D58
		internal static string CtxSimpleIdentifier
		{
			get
			{
				return EntityRes.GetString("CtxSimpleIdentifier");
			}
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06001300 RID: 4864 RVA: 0x00045B64 File Offset: 0x00043D64
		internal static string CtxSkipSubClause
		{
			get
			{
				return EntityRes.GetString("CtxSkipSubClause");
			}
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06001301 RID: 4865 RVA: 0x00045B70 File Offset: 0x00043D70
		internal static string CtxTopSubClause
		{
			get
			{
				return EntityRes.GetString("CtxTopSubClause");
			}
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06001302 RID: 4866 RVA: 0x00045B7C File Offset: 0x00043D7C
		internal static string CtxTreat
		{
			get
			{
				return EntityRes.GetString("CtxTreat");
			}
		}

		// Token: 0x06001303 RID: 4867 RVA: 0x00045B88 File Offset: 0x00043D88
		internal static string CtxTypeCtor(object p0)
		{
			return EntityRes.GetString("CtxTypeCtor", new object[]
			{
				p0
			});
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06001304 RID: 4868 RVA: 0x00045B9E File Offset: 0x00043D9E
		internal static string CtxTypeName
		{
			get
			{
				return EntityRes.GetString("CtxTypeName");
			}
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06001305 RID: 4869 RVA: 0x00045BAA File Offset: 0x00043DAA
		internal static string CtxUnaryMinus
		{
			get
			{
				return EntityRes.GetString("CtxUnaryMinus");
			}
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06001306 RID: 4870 RVA: 0x00045BB6 File Offset: 0x00043DB6
		internal static string CtxUnaryPlus
		{
			get
			{
				return EntityRes.GetString("CtxUnaryPlus");
			}
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06001307 RID: 4871 RVA: 0x00045BC2 File Offset: 0x00043DC2
		internal static string CtxUnion
		{
			get
			{
				return EntityRes.GetString("CtxUnion");
			}
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06001308 RID: 4872 RVA: 0x00045BCE File Offset: 0x00043DCE
		internal static string CtxUnionAll
		{
			get
			{
				return EntityRes.GetString("CtxUnionAll");
			}
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06001309 RID: 4873 RVA: 0x00045BDA File Offset: 0x00043DDA
		internal static string CtxWhereClause
		{
			get
			{
				return EntityRes.GetString("CtxWhereClause");
			}
		}

		// Token: 0x0600130A RID: 4874 RVA: 0x00045BE6 File Offset: 0x00043DE6
		internal static string CannotConvertNumericLiteral(object p0, object p1)
		{
			return EntityRes.GetString("CannotConvertNumericLiteral", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x0600130B RID: 4875 RVA: 0x00045C00 File Offset: 0x00043E00
		internal static string GenericSyntaxError
		{
			get
			{
				return EntityRes.GetString("GenericSyntaxError");
			}
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x0600130C RID: 4876 RVA: 0x00045C0C File Offset: 0x00043E0C
		internal static string InFromClause
		{
			get
			{
				return EntityRes.GetString("InFromClause");
			}
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x0600130D RID: 4877 RVA: 0x00045C18 File Offset: 0x00043E18
		internal static string InGroupClause
		{
			get
			{
				return EntityRes.GetString("InGroupClause");
			}
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x0600130E RID: 4878 RVA: 0x00045C24 File Offset: 0x00043E24
		internal static string InRowCtor
		{
			get
			{
				return EntityRes.GetString("InRowCtor");
			}
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x0600130F RID: 4879 RVA: 0x00045C30 File Offset: 0x00043E30
		internal static string InSelectProjectionList
		{
			get
			{
				return EntityRes.GetString("InSelectProjectionList");
			}
		}

		// Token: 0x06001310 RID: 4880 RVA: 0x00045C3C File Offset: 0x00043E3C
		internal static string InvalidAliasName(object p0)
		{
			return EntityRes.GetString("InvalidAliasName", new object[]
			{
				p0
			});
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06001311 RID: 4881 RVA: 0x00045C52 File Offset: 0x00043E52
		internal static string InvalidEmptyIdentifier
		{
			get
			{
				return EntityRes.GetString("InvalidEmptyIdentifier");
			}
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06001312 RID: 4882 RVA: 0x00045C5E File Offset: 0x00043E5E
		internal static string InvalidEmptyQuery
		{
			get
			{
				return EntityRes.GetString("InvalidEmptyQuery");
			}
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06001313 RID: 4883 RVA: 0x00045C6A File Offset: 0x00043E6A
		internal static string InvalidEmptyQueryTextArgument
		{
			get
			{
				return EntityRes.GetString("InvalidEmptyQueryTextArgument");
			}
		}

		// Token: 0x06001314 RID: 4884 RVA: 0x00045C76 File Offset: 0x00043E76
		internal static string InvalidEscapedIdentifier(object p0)
		{
			return EntityRes.GetString("InvalidEscapedIdentifier", new object[]
			{
				p0
			});
		}

		// Token: 0x06001315 RID: 4885 RVA: 0x00045C8C File Offset: 0x00043E8C
		internal static string InvalidEscapedIdentifierUnbalanced(object p0)
		{
			return EntityRes.GetString("InvalidEscapedIdentifierUnbalanced", new object[]
			{
				p0
			});
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06001316 RID: 4886 RVA: 0x00045CA2 File Offset: 0x00043EA2
		internal static string InvalidOperatorSymbol
		{
			get
			{
				return EntityRes.GetString("InvalidOperatorSymbol");
			}
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06001317 RID: 4887 RVA: 0x00045CAE File Offset: 0x00043EAE
		internal static string InvalidPunctuatorSymbol
		{
			get
			{
				return EntityRes.GetString("InvalidPunctuatorSymbol");
			}
		}

		// Token: 0x06001318 RID: 4888 RVA: 0x00045CBA File Offset: 0x00043EBA
		internal static string InvalidSimpleIdentifier(object p0)
		{
			return EntityRes.GetString("InvalidSimpleIdentifier", new object[]
			{
				p0
			});
		}

		// Token: 0x06001319 RID: 4889 RVA: 0x00045CD0 File Offset: 0x00043ED0
		internal static string InvalidSimpleIdentifierNonASCII(object p0)
		{
			return EntityRes.GetString("InvalidSimpleIdentifierNonASCII", new object[]
			{
				p0
			});
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x0600131A RID: 4890 RVA: 0x00045CE6 File Offset: 0x00043EE6
		internal static string LocalizedCollection
		{
			get
			{
				return EntityRes.GetString("LocalizedCollection");
			}
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x0600131B RID: 4891 RVA: 0x00045CF2 File Offset: 0x00043EF2
		internal static string LocalizedColumn
		{
			get
			{
				return EntityRes.GetString("LocalizedColumn");
			}
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x0600131C RID: 4892 RVA: 0x00045CFE File Offset: 0x00043EFE
		internal static string LocalizedComplex
		{
			get
			{
				return EntityRes.GetString("LocalizedComplex");
			}
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x0600131D RID: 4893 RVA: 0x00045D0A File Offset: 0x00043F0A
		internal static string LocalizedEntity
		{
			get
			{
				return EntityRes.GetString("LocalizedEntity");
			}
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x0600131E RID: 4894 RVA: 0x00045D16 File Offset: 0x00043F16
		internal static string LocalizedEntityContainerExpression
		{
			get
			{
				return EntityRes.GetString("LocalizedEntityContainerExpression");
			}
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x0600131F RID: 4895 RVA: 0x00045D22 File Offset: 0x00043F22
		internal static string LocalizedFunction
		{
			get
			{
				return EntityRes.GetString("LocalizedFunction");
			}
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06001320 RID: 4896 RVA: 0x00045D2E File Offset: 0x00043F2E
		internal static string LocalizedInlineFunction
		{
			get
			{
				return EntityRes.GetString("LocalizedInlineFunction");
			}
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06001321 RID: 4897 RVA: 0x00045D3A File Offset: 0x00043F3A
		internal static string LocalizedKeyword
		{
			get
			{
				return EntityRes.GetString("LocalizedKeyword");
			}
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06001322 RID: 4898 RVA: 0x00045D46 File Offset: 0x00043F46
		internal static string LocalizedLeft
		{
			get
			{
				return EntityRes.GetString("LocalizedLeft");
			}
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06001323 RID: 4899 RVA: 0x00045D52 File Offset: 0x00043F52
		internal static string LocalizedLine
		{
			get
			{
				return EntityRes.GetString("LocalizedLine");
			}
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06001324 RID: 4900 RVA: 0x00045D5E File Offset: 0x00043F5E
		internal static string LocalizedMetadataMemberExpression
		{
			get
			{
				return EntityRes.GetString("LocalizedMetadataMemberExpression");
			}
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06001325 RID: 4901 RVA: 0x00045D6A File Offset: 0x00043F6A
		internal static string LocalizedNamespace
		{
			get
			{
				return EntityRes.GetString("LocalizedNamespace");
			}
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06001326 RID: 4902 RVA: 0x00045D76 File Offset: 0x00043F76
		internal static string LocalizedNear
		{
			get
			{
				return EntityRes.GetString("LocalizedNear");
			}
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06001327 RID: 4903 RVA: 0x00045D82 File Offset: 0x00043F82
		internal static string LocalizedPrimitive
		{
			get
			{
				return EntityRes.GetString("LocalizedPrimitive");
			}
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06001328 RID: 4904 RVA: 0x00045D8E File Offset: 0x00043F8E
		internal static string LocalizedReference
		{
			get
			{
				return EntityRes.GetString("LocalizedReference");
			}
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06001329 RID: 4905 RVA: 0x00045D9A File Offset: 0x00043F9A
		internal static string LocalizedRight
		{
			get
			{
				return EntityRes.GetString("LocalizedRight");
			}
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x0600132A RID: 4906 RVA: 0x00045DA6 File Offset: 0x00043FA6
		internal static string LocalizedRow
		{
			get
			{
				return EntityRes.GetString("LocalizedRow");
			}
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x0600132B RID: 4907 RVA: 0x00045DB2 File Offset: 0x00043FB2
		internal static string LocalizedTerm
		{
			get
			{
				return EntityRes.GetString("LocalizedTerm");
			}
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x0600132C RID: 4908 RVA: 0x00045DBE File Offset: 0x00043FBE
		internal static string LocalizedType
		{
			get
			{
				return EntityRes.GetString("LocalizedType");
			}
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x0600132D RID: 4909 RVA: 0x00045DCA File Offset: 0x00043FCA
		internal static string LocalizedEnumMember
		{
			get
			{
				return EntityRes.GetString("LocalizedEnumMember");
			}
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x0600132E RID: 4910 RVA: 0x00045DD6 File Offset: 0x00043FD6
		internal static string LocalizedValueExpression
		{
			get
			{
				return EntityRes.GetString("LocalizedValueExpression");
			}
		}

		// Token: 0x0600132F RID: 4911 RVA: 0x00045DE2 File Offset: 0x00043FE2
		internal static string AliasNameAlreadyUsed(object p0)
		{
			return EntityRes.GetString("AliasNameAlreadyUsed", new object[]
			{
				p0
			});
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06001330 RID: 4912 RVA: 0x00045DF8 File Offset: 0x00043FF8
		internal static string AmbiguousFunctionArguments
		{
			get
			{
				return EntityRes.GetString("AmbiguousFunctionArguments");
			}
		}

		// Token: 0x06001331 RID: 4913 RVA: 0x00045E04 File Offset: 0x00044004
		internal static string AmbiguousMetadataMemberName(object p0, object p1, object p2)
		{
			return EntityRes.GetString("AmbiguousMetadataMemberName", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06001332 RID: 4914 RVA: 0x00045E22 File Offset: 0x00044022
		internal static string ArgumentTypesAreIncompatible(object p0, object p1)
		{
			return EntityRes.GetString("ArgumentTypesAreIncompatible", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06001333 RID: 4915 RVA: 0x00045E3C File Offset: 0x0004403C
		internal static string BetweenLimitsCannotBeUntypedNulls
		{
			get
			{
				return EntityRes.GetString("BetweenLimitsCannotBeUntypedNulls");
			}
		}

		// Token: 0x06001334 RID: 4916 RVA: 0x00045E48 File Offset: 0x00044048
		internal static string BetweenLimitsTypesAreNotCompatible(object p0, object p1)
		{
			return EntityRes.GetString("BetweenLimitsTypesAreNotCompatible", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06001335 RID: 4917 RVA: 0x00045E62 File Offset: 0x00044062
		internal static string BetweenLimitsTypesAreNotOrderComparable(object p0, object p1)
		{
			return EntityRes.GetString("BetweenLimitsTypesAreNotOrderComparable", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06001336 RID: 4918 RVA: 0x00045E7C File Offset: 0x0004407C
		internal static string BetweenValueIsNotOrderComparable(object p0, object p1)
		{
			return EntityRes.GetString("BetweenValueIsNotOrderComparable", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06001337 RID: 4919 RVA: 0x00045E96 File Offset: 0x00044096
		internal static string CannotCreateEmptyMultiset
		{
			get
			{
				return EntityRes.GetString("CannotCreateEmptyMultiset");
			}
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06001338 RID: 4920 RVA: 0x00045EA2 File Offset: 0x000440A2
		internal static string CannotCreateMultisetofNulls
		{
			get
			{
				return EntityRes.GetString("CannotCreateMultisetofNulls");
			}
		}

		// Token: 0x06001339 RID: 4921 RVA: 0x00045EAE File Offset: 0x000440AE
		internal static string CannotInstantiateAbstractType(object p0)
		{
			return EntityRes.GetString("CannotInstantiateAbstractType", new object[]
			{
				p0
			});
		}

		// Token: 0x0600133A RID: 4922 RVA: 0x00045EC4 File Offset: 0x000440C4
		internal static string CannotResolveNameToTypeOrFunction(object p0)
		{
			return EntityRes.GetString("CannotResolveNameToTypeOrFunction", new object[]
			{
				p0
			});
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x0600133B RID: 4923 RVA: 0x00045EDA File Offset: 0x000440DA
		internal static string ConcatBuiltinNotSupported
		{
			get
			{
				return EntityRes.GetString("ConcatBuiltinNotSupported");
			}
		}

		// Token: 0x0600133C RID: 4924 RVA: 0x00045EE6 File Offset: 0x000440E6
		internal static string CouldNotResolveIdentifier(object p0)
		{
			return EntityRes.GetString("CouldNotResolveIdentifier", new object[]
			{
				p0
			});
		}

		// Token: 0x0600133D RID: 4925 RVA: 0x00045EFC File Offset: 0x000440FC
		internal static string CreateRefTypeIdentifierMustBeASubOrSuperType(object p0, object p1)
		{
			return EntityRes.GetString("CreateRefTypeIdentifierMustBeASubOrSuperType", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x0600133E RID: 4926 RVA: 0x00045F16 File Offset: 0x00044116
		internal static string CreateRefTypeIdentifierMustSpecifyAnEntityType(object p0, object p1)
		{
			return EntityRes.GetString("CreateRefTypeIdentifierMustSpecifyAnEntityType", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x0600133F RID: 4927 RVA: 0x00045F30 File Offset: 0x00044130
		internal static string DeRefArgIsNotOfRefType(object p0)
		{
			return EntityRes.GetString("DeRefArgIsNotOfRefType", new object[]
			{
				p0
			});
		}

		// Token: 0x06001340 RID: 4928 RVA: 0x00045F46 File Offset: 0x00044146
		internal static string DuplicatedInlineFunctionOverload(object p0)
		{
			return EntityRes.GetString("DuplicatedInlineFunctionOverload", new object[]
			{
				p0
			});
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06001341 RID: 4929 RVA: 0x00045F5C File Offset: 0x0004415C
		internal static string ElementOperatorIsNotSupported
		{
			get
			{
				return EntityRes.GetString("ElementOperatorIsNotSupported");
			}
		}

		// Token: 0x06001342 RID: 4930 RVA: 0x00045F68 File Offset: 0x00044168
		internal static string MemberDoesNotBelongToEntityContainer(object p0, object p1)
		{
			return EntityRes.GetString("MemberDoesNotBelongToEntityContainer", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06001343 RID: 4931 RVA: 0x00045F82 File Offset: 0x00044182
		internal static string ExpressionCannotBeNull
		{
			get
			{
				return EntityRes.GetString("ExpressionCannotBeNull");
			}
		}

		// Token: 0x06001344 RID: 4932 RVA: 0x00045F8E File Offset: 0x0004418E
		internal static string OfTypeExpressionElementTypeMustBeEntityType(object p0, object p1)
		{
			return EntityRes.GetString("OfTypeExpressionElementTypeMustBeEntityType", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06001345 RID: 4933 RVA: 0x00045FA8 File Offset: 0x000441A8
		internal static string OfTypeExpressionElementTypeMustBeNominalType(object p0, object p1)
		{
			return EntityRes.GetString("OfTypeExpressionElementTypeMustBeNominalType", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06001346 RID: 4934 RVA: 0x00045FC2 File Offset: 0x000441C2
		internal static string ExpressionMustBeCollection
		{
			get
			{
				return EntityRes.GetString("ExpressionMustBeCollection");
			}
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06001347 RID: 4935 RVA: 0x00045FCE File Offset: 0x000441CE
		internal static string ExpressionMustBeNumericType
		{
			get
			{
				return EntityRes.GetString("ExpressionMustBeNumericType");
			}
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06001348 RID: 4936 RVA: 0x00045FDA File Offset: 0x000441DA
		internal static string ExpressionTypeMustBeBoolean
		{
			get
			{
				return EntityRes.GetString("ExpressionTypeMustBeBoolean");
			}
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06001349 RID: 4937 RVA: 0x00045FE6 File Offset: 0x000441E6
		internal static string ExpressionTypeMustBeEqualComparable
		{
			get
			{
				return EntityRes.GetString("ExpressionTypeMustBeEqualComparable");
			}
		}

		// Token: 0x0600134A RID: 4938 RVA: 0x00045FF2 File Offset: 0x000441F2
		internal static string ExpressionTypeMustBeEntityType(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ExpressionTypeMustBeEntityType", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x0600134B RID: 4939 RVA: 0x00046010 File Offset: 0x00044210
		internal static string ExpressionTypeMustBeNominalType(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ExpressionTypeMustBeNominalType", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x0600134C RID: 4940 RVA: 0x0004602E File Offset: 0x0004422E
		internal static string ExpressionTypeMustNotBeCollection
		{
			get
			{
				return EntityRes.GetString("ExpressionTypeMustNotBeCollection");
			}
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x0600134D RID: 4941 RVA: 0x0004603A File Offset: 0x0004423A
		internal static string ExprIsNotValidEntitySetForCreateRef
		{
			get
			{
				return EntityRes.GetString("ExprIsNotValidEntitySetForCreateRef");
			}
		}

		// Token: 0x0600134E RID: 4942 RVA: 0x00046046 File Offset: 0x00044246
		internal static string FailedToResolveAggregateFunction(object p0)
		{
			return EntityRes.GetString("FailedToResolveAggregateFunction", new object[]
			{
				p0
			});
		}

		// Token: 0x0600134F RID: 4943 RVA: 0x0004605C File Offset: 0x0004425C
		internal static string GeneralExceptionAsQueryInnerException(object p0)
		{
			return EntityRes.GetString("GeneralExceptionAsQueryInnerException", new object[]
			{
				p0
			});
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06001350 RID: 4944 RVA: 0x00046072 File Offset: 0x00044272
		internal static string GroupingKeysMustBeEqualComparable
		{
			get
			{
				return EntityRes.GetString("GroupingKeysMustBeEqualComparable");
			}
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06001351 RID: 4945 RVA: 0x0004607E File Offset: 0x0004427E
		internal static string GroupPartitionOutOfContext
		{
			get
			{
				return EntityRes.GetString("GroupPartitionOutOfContext");
			}
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06001352 RID: 4946 RVA: 0x0004608A File Offset: 0x0004428A
		internal static string HavingRequiresGroupClause
		{
			get
			{
				return EntityRes.GetString("HavingRequiresGroupClause");
			}
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06001353 RID: 4947 RVA: 0x00046096 File Offset: 0x00044296
		internal static string ImcompatibleCreateRefKeyElementType
		{
			get
			{
				return EntityRes.GetString("ImcompatibleCreateRefKeyElementType");
			}
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06001354 RID: 4948 RVA: 0x000460A2 File Offset: 0x000442A2
		internal static string ImcompatibleCreateRefKeyType
		{
			get
			{
				return EntityRes.GetString("ImcompatibleCreateRefKeyType");
			}
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06001355 RID: 4949 RVA: 0x000460AE File Offset: 0x000442AE
		internal static string InnerJoinMustHaveOnPredicate
		{
			get
			{
				return EntityRes.GetString("InnerJoinMustHaveOnPredicate");
			}
		}

		// Token: 0x06001356 RID: 4950 RVA: 0x000460BA File Offset: 0x000442BA
		internal static string InvalidAssociationTypeForUnion(object p0)
		{
			return EntityRes.GetString("InvalidAssociationTypeForUnion", new object[]
			{
				p0
			});
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06001357 RID: 4951 RVA: 0x000460D0 File Offset: 0x000442D0
		internal static string InvalidCaseResultTypes
		{
			get
			{
				return EntityRes.GetString("InvalidCaseResultTypes");
			}
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06001358 RID: 4952 RVA: 0x000460DC File Offset: 0x000442DC
		internal static string InvalidCaseWhenThenNullType
		{
			get
			{
				return EntityRes.GetString("InvalidCaseWhenThenNullType");
			}
		}

		// Token: 0x06001359 RID: 4953 RVA: 0x000460E8 File Offset: 0x000442E8
		internal static string InvalidCast(object p0, object p1)
		{
			return EntityRes.GetString("InvalidCast", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x0600135A RID: 4954 RVA: 0x00046102 File Offset: 0x00044302
		internal static string InvalidCastExpressionType
		{
			get
			{
				return EntityRes.GetString("InvalidCastExpressionType");
			}
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x0600135B RID: 4955 RVA: 0x0004610E File Offset: 0x0004430E
		internal static string InvalidCastType
		{
			get
			{
				return EntityRes.GetString("InvalidCastType");
			}
		}

		// Token: 0x0600135C RID: 4956 RVA: 0x0004611A File Offset: 0x0004431A
		internal static string InvalidComplexType(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("InvalidComplexType", new object[]
			{
				p0,
				p1,
				p2,
				p3
			});
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x0600135D RID: 4957 RVA: 0x0004613C File Offset: 0x0004433C
		internal static string InvalidCreateRefKeyType
		{
			get
			{
				return EntityRes.GetString("InvalidCreateRefKeyType");
			}
		}

		// Token: 0x0600135E RID: 4958 RVA: 0x00046148 File Offset: 0x00044348
		internal static string InvalidCtorArgumentType(object p0, object p1, object p2)
		{
			return EntityRes.GetString("InvalidCtorArgumentType", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x0600135F RID: 4959 RVA: 0x00046166 File Offset: 0x00044366
		internal static string InvalidCtorUseOnType(object p0)
		{
			return EntityRes.GetString("InvalidCtorUseOnType", new object[]
			{
				p0
			});
		}

		// Token: 0x06001360 RID: 4960 RVA: 0x0004617C File Offset: 0x0004437C
		internal static string InvalidDateTimeOffsetLiteral(object p0)
		{
			return EntityRes.GetString("InvalidDateTimeOffsetLiteral", new object[]
			{
				p0
			});
		}

		// Token: 0x06001361 RID: 4961 RVA: 0x00046192 File Offset: 0x00044392
		internal static string InvalidDay(object p0, object p1)
		{
			return EntityRes.GetString("InvalidDay", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06001362 RID: 4962 RVA: 0x000461AC File Offset: 0x000443AC
		internal static string InvalidDayInMonth(object p0, object p1, object p2)
		{
			return EntityRes.GetString("InvalidDayInMonth", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06001363 RID: 4963 RVA: 0x000461CA File Offset: 0x000443CA
		internal static string InvalidDeRefProperty(object p0, object p1, object p2)
		{
			return EntityRes.GetString("InvalidDeRefProperty", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06001364 RID: 4964 RVA: 0x000461E8 File Offset: 0x000443E8
		internal static string InvalidDistinctArgumentInCtor
		{
			get
			{
				return EntityRes.GetString("InvalidDistinctArgumentInCtor");
			}
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06001365 RID: 4965 RVA: 0x000461F4 File Offset: 0x000443F4
		internal static string InvalidDistinctArgumentInNonAggFunction
		{
			get
			{
				return EntityRes.GetString("InvalidDistinctArgumentInNonAggFunction");
			}
		}

		// Token: 0x06001366 RID: 4966 RVA: 0x00046200 File Offset: 0x00044400
		internal static string InvalidEntityRootTypeArgument(object p0, object p1)
		{
			return EntityRes.GetString("InvalidEntityRootTypeArgument", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06001367 RID: 4967 RVA: 0x0004621A File Offset: 0x0004441A
		internal static string InvalidEntityTypeArgument(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("InvalidEntityTypeArgument", new object[]
			{
				p0,
				p1,
				p2,
				p3
			});
		}

		// Token: 0x06001368 RID: 4968 RVA: 0x0004623C File Offset: 0x0004443C
		internal static string InvalidExpressionResolutionClass(object p0, object p1)
		{
			return EntityRes.GetString("InvalidExpressionResolutionClass", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06001369 RID: 4969 RVA: 0x00046256 File Offset: 0x00044456
		internal static string InvalidFlattenArgument
		{
			get
			{
				return EntityRes.GetString("InvalidFlattenArgument");
			}
		}

		// Token: 0x0600136A RID: 4970 RVA: 0x00046262 File Offset: 0x00044462
		internal static string InvalidGroupIdentifierReference(object p0)
		{
			return EntityRes.GetString("InvalidGroupIdentifierReference", new object[]
			{
				p0
			});
		}

		// Token: 0x0600136B RID: 4971 RVA: 0x00046278 File Offset: 0x00044478
		internal static string InvalidHour(object p0, object p1)
		{
			return EntityRes.GetString("InvalidHour", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x0600136C RID: 4972 RVA: 0x00046292 File Offset: 0x00044492
		internal static string InvalidImplicitRelationshipFromEnd(object p0)
		{
			return EntityRes.GetString("InvalidImplicitRelationshipFromEnd", new object[]
			{
				p0
			});
		}

		// Token: 0x0600136D RID: 4973 RVA: 0x000462A8 File Offset: 0x000444A8
		internal static string InvalidImplicitRelationshipToEnd(object p0)
		{
			return EntityRes.GetString("InvalidImplicitRelationshipToEnd", new object[]
			{
				p0
			});
		}

		// Token: 0x0600136E RID: 4974 RVA: 0x000462BE File Offset: 0x000444BE
		internal static string InvalidInExprArgs(object p0, object p1)
		{
			return EntityRes.GetString("InvalidInExprArgs", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x0600136F RID: 4975 RVA: 0x000462D8 File Offset: 0x000444D8
		internal static string InvalidJoinLeftCorrelation
		{
			get
			{
				return EntityRes.GetString("InvalidJoinLeftCorrelation");
			}
		}

		// Token: 0x06001370 RID: 4976 RVA: 0x000462E4 File Offset: 0x000444E4
		internal static string InvalidKeyArgument(object p0)
		{
			return EntityRes.GetString("InvalidKeyArgument", new object[]
			{
				p0
			});
		}

		// Token: 0x06001371 RID: 4977 RVA: 0x000462FA File Offset: 0x000444FA
		internal static string InvalidKeyTypeForCollation(object p0)
		{
			return EntityRes.GetString("InvalidKeyTypeForCollation", new object[]
			{
				p0
			});
		}

		// Token: 0x06001372 RID: 4978 RVA: 0x00046310 File Offset: 0x00044510
		internal static string InvalidLiteralFormat(object p0, object p1)
		{
			return EntityRes.GetString("InvalidLiteralFormat", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06001373 RID: 4979 RVA: 0x0004632A File Offset: 0x0004452A
		internal static string InvalidMetadataMemberName
		{
			get
			{
				return EntityRes.GetString("InvalidMetadataMemberName");
			}
		}

		// Token: 0x06001374 RID: 4980 RVA: 0x00046336 File Offset: 0x00044536
		internal static string InvalidMinute(object p0, object p1)
		{
			return EntityRes.GetString("InvalidMinute", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06001375 RID: 4981 RVA: 0x00046350 File Offset: 0x00044550
		internal static string InvalidModeForWithRelationshipClause
		{
			get
			{
				return EntityRes.GetString("InvalidModeForWithRelationshipClause");
			}
		}

		// Token: 0x06001376 RID: 4982 RVA: 0x0004635C File Offset: 0x0004455C
		internal static string InvalidMonth(object p0, object p1)
		{
			return EntityRes.GetString("InvalidMonth", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06001377 RID: 4983 RVA: 0x00046376 File Offset: 0x00044576
		internal static string InvalidNamespaceAlias
		{
			get
			{
				return EntityRes.GetString("InvalidNamespaceAlias");
			}
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06001378 RID: 4984 RVA: 0x00046382 File Offset: 0x00044582
		internal static string InvalidNullArithmetic
		{
			get
			{
				return EntityRes.GetString("InvalidNullArithmetic");
			}
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06001379 RID: 4985 RVA: 0x0004638E File Offset: 0x0004458E
		internal static string InvalidNullComparison
		{
			get
			{
				return EntityRes.GetString("InvalidNullComparison");
			}
		}

		// Token: 0x0600137A RID: 4986 RVA: 0x0004639A File Offset: 0x0004459A
		internal static string InvalidNullLiteralForNonNullableMember(object p0, object p1)
		{
			return EntityRes.GetString("InvalidNullLiteralForNonNullableMember", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x0600137B RID: 4987 RVA: 0x000463B4 File Offset: 0x000445B4
		internal static string InvalidParameterFormat(object p0)
		{
			return EntityRes.GetString("InvalidParameterFormat", new object[]
			{
				p0
			});
		}

		// Token: 0x0600137C RID: 4988 RVA: 0x000463CA File Offset: 0x000445CA
		internal static string InvalidPlaceholderRootTypeArgument(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("InvalidPlaceholderRootTypeArgument", new object[]
			{
				p0,
				p1,
				p2,
				p3
			});
		}

		// Token: 0x0600137D RID: 4989 RVA: 0x000463EC File Offset: 0x000445EC
		internal static string InvalidPlaceholderTypeArgument(object p0, object p1, object p2, object p3, object p4, object p5)
		{
			return EntityRes.GetString("InvalidPlaceholderTypeArgument", new object[]
			{
				p0,
				p1,
				p2,
				p3,
				p4,
				p5
			});
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x0600137E RID: 4990 RVA: 0x00046418 File Offset: 0x00044618
		internal static string InvalidPredicateForCrossJoin
		{
			get
			{
				return EntityRes.GetString("InvalidPredicateForCrossJoin");
			}
		}

		// Token: 0x0600137F RID: 4991 RVA: 0x00046424 File Offset: 0x00044624
		internal static string InvalidRelationshipMember(object p0, object p1)
		{
			return EntityRes.GetString("InvalidRelationshipMember", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06001380 RID: 4992 RVA: 0x0004643E File Offset: 0x0004463E
		internal static string InvalidMetadataMemberClassResolution(object p0, object p1, object p2)
		{
			return EntityRes.GetString("InvalidMetadataMemberClassResolution", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06001381 RID: 4993 RVA: 0x0004645C File Offset: 0x0004465C
		internal static string InvalidRootComplexType(object p0, object p1)
		{
			return EntityRes.GetString("InvalidRootComplexType", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06001382 RID: 4994 RVA: 0x00046476 File Offset: 0x00044676
		internal static string InvalidRootRowType(object p0, object p1)
		{
			return EntityRes.GetString("InvalidRootRowType", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06001383 RID: 4995 RVA: 0x00046490 File Offset: 0x00044690
		internal static string InvalidRowType(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("InvalidRowType", new object[]
			{
				p0,
				p1,
				p2,
				p3
			});
		}

		// Token: 0x06001384 RID: 4996 RVA: 0x000464B2 File Offset: 0x000446B2
		internal static string InvalidSecond(object p0, object p1)
		{
			return EntityRes.GetString("InvalidSecond", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06001385 RID: 4997 RVA: 0x000464CC File Offset: 0x000446CC
		internal static string InvalidSelectValueAliasedExpression
		{
			get
			{
				return EntityRes.GetString("InvalidSelectValueAliasedExpression");
			}
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06001386 RID: 4998 RVA: 0x000464D8 File Offset: 0x000446D8
		internal static string InvalidSelectValueList
		{
			get
			{
				return EntityRes.GetString("InvalidSelectValueList");
			}
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06001387 RID: 4999 RVA: 0x000464E4 File Offset: 0x000446E4
		internal static string InvalidTypeForWithRelationshipClause
		{
			get
			{
				return EntityRes.GetString("InvalidTypeForWithRelationshipClause");
			}
		}

		// Token: 0x06001388 RID: 5000 RVA: 0x000464F0 File Offset: 0x000446F0
		internal static string InvalidUnarySetOpArgument(object p0)
		{
			return EntityRes.GetString("InvalidUnarySetOpArgument", new object[]
			{
				p0
			});
		}

		// Token: 0x06001389 RID: 5001 RVA: 0x00046506 File Offset: 0x00044706
		internal static string InvalidUnsignedTypeForUnaryMinusOperation(object p0)
		{
			return EntityRes.GetString("InvalidUnsignedTypeForUnaryMinusOperation", new object[]
			{
				p0
			});
		}

		// Token: 0x0600138A RID: 5002 RVA: 0x0004651C File Offset: 0x0004471C
		internal static string InvalidYear(object p0, object p1)
		{
			return EntityRes.GetString("InvalidYear", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x0600138B RID: 5003 RVA: 0x00046536 File Offset: 0x00044736
		internal static string InvalidWithRelationshipTargetEndMultiplicity(object p0, object p1)
		{
			return EntityRes.GetString("InvalidWithRelationshipTargetEndMultiplicity", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x0600138C RID: 5004 RVA: 0x00046550 File Offset: 0x00044750
		internal static string InvalidQueryResultType(object p0)
		{
			return EntityRes.GetString("InvalidQueryResultType", new object[]
			{
				p0
			});
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x0600138D RID: 5005 RVA: 0x00046566 File Offset: 0x00044766
		internal static string IsNullInvalidType
		{
			get
			{
				return EntityRes.GetString("IsNullInvalidType");
			}
		}

		// Token: 0x0600138E RID: 5006 RVA: 0x00046572 File Offset: 0x00044772
		internal static string KeyMustBeCorrelated(object p0)
		{
			return EntityRes.GetString("KeyMustBeCorrelated", new object[]
			{
				p0
			});
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x0600138F RID: 5007 RVA: 0x00046588 File Offset: 0x00044788
		internal static string LeftSetExpressionArgsMustBeCollection
		{
			get
			{
				return EntityRes.GetString("LeftSetExpressionArgsMustBeCollection");
			}
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06001390 RID: 5008 RVA: 0x00046594 File Offset: 0x00044794
		internal static string LikeArgMustBeStringType
		{
			get
			{
				return EntityRes.GetString("LikeArgMustBeStringType");
			}
		}

		// Token: 0x06001391 RID: 5009 RVA: 0x000465A0 File Offset: 0x000447A0
		internal static string LiteralTypeNotFoundInMetadata(object p0)
		{
			return EntityRes.GetString("LiteralTypeNotFoundInMetadata", new object[]
			{
				p0
			});
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06001392 RID: 5010 RVA: 0x000465B6 File Offset: 0x000447B6
		internal static string MalformedSingleQuotePayload
		{
			get
			{
				return EntityRes.GetString("MalformedSingleQuotePayload");
			}
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06001393 RID: 5011 RVA: 0x000465C2 File Offset: 0x000447C2
		internal static string MalformedStringLiteralPayload
		{
			get
			{
				return EntityRes.GetString("MalformedStringLiteralPayload");
			}
		}

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06001394 RID: 5012 RVA: 0x000465CE File Offset: 0x000447CE
		internal static string MethodInvocationNotSupported
		{
			get
			{
				return EntityRes.GetString("MethodInvocationNotSupported");
			}
		}

		// Token: 0x06001395 RID: 5013 RVA: 0x000465DA File Offset: 0x000447DA
		internal static string MultipleDefinitionsOfParameter(object p0)
		{
			return EntityRes.GetString("MultipleDefinitionsOfParameter", new object[]
			{
				p0
			});
		}

		// Token: 0x06001396 RID: 5014 RVA: 0x000465F0 File Offset: 0x000447F0
		internal static string MultipleDefinitionsOfVariable(object p0)
		{
			return EntityRes.GetString("MultipleDefinitionsOfVariable", new object[]
			{
				p0
			});
		}

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06001397 RID: 5015 RVA: 0x00046606 File Offset: 0x00044806
		internal static string MultisetElemsAreNotTypeCompatible
		{
			get
			{
				return EntityRes.GetString("MultisetElemsAreNotTypeCompatible");
			}
		}

		// Token: 0x06001398 RID: 5016 RVA: 0x00046612 File Offset: 0x00044812
		internal static string NamespaceAliasAlreadyUsed(object p0)
		{
			return EntityRes.GetString("NamespaceAliasAlreadyUsed", new object[]
			{
				p0
			});
		}

		// Token: 0x06001399 RID: 5017 RVA: 0x00046628 File Offset: 0x00044828
		internal static string NamespaceAlreadyImported(object p0)
		{
			return EntityRes.GetString("NamespaceAlreadyImported", new object[]
			{
				p0
			});
		}

		// Token: 0x0600139A RID: 5018 RVA: 0x0004663E File Offset: 0x0004483E
		internal static string NestedAggregateCannotBeUsedInAggregate(object p0, object p1)
		{
			return EntityRes.GetString("NestedAggregateCannotBeUsedInAggregate", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x0600139B RID: 5019 RVA: 0x00046658 File Offset: 0x00044858
		internal static string NoAggrFunctionOverloadMatch(object p0, object p1, object p2)
		{
			return EntityRes.GetString("NoAggrFunctionOverloadMatch", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x0600139C RID: 5020 RVA: 0x00046676 File Offset: 0x00044876
		internal static string NoCanonicalAggrFunctionOverloadMatch(object p0, object p1, object p2)
		{
			return EntityRes.GetString("NoCanonicalAggrFunctionOverloadMatch", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x0600139D RID: 5021 RVA: 0x00046694 File Offset: 0x00044894
		internal static string NoCanonicalFunctionOverloadMatch(object p0, object p1, object p2)
		{
			return EntityRes.GetString("NoCanonicalFunctionOverloadMatch", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x0600139E RID: 5022 RVA: 0x000466B2 File Offset: 0x000448B2
		internal static string NoFunctionOverloadMatch(object p0, object p1, object p2)
		{
			return EntityRes.GetString("NoFunctionOverloadMatch", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x0600139F RID: 5023 RVA: 0x000466D0 File Offset: 0x000448D0
		internal static string NotAMemberOfCollection(object p0, object p1)
		{
			return EntityRes.GetString("NotAMemberOfCollection", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060013A0 RID: 5024 RVA: 0x000466EA File Offset: 0x000448EA
		internal static string NotAMemberOfType(object p0, object p1)
		{
			return EntityRes.GetString("NotAMemberOfType", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060013A1 RID: 5025 RVA: 0x00046704 File Offset: 0x00044904
		internal static string NotASuperOrSubType(object p0, object p1)
		{
			return EntityRes.GetString("NotASuperOrSubType", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x060013A2 RID: 5026 RVA: 0x0004671E File Offset: 0x0004491E
		internal static string NullLiteralCannotBePromotedToCollectionOfNulls
		{
			get
			{
				return EntityRes.GetString("NullLiteralCannotBePromotedToCollectionOfNulls");
			}
		}

		// Token: 0x060013A3 RID: 5027 RVA: 0x0004672A File Offset: 0x0004492A
		internal static string NumberOfTypeCtorIsLessThenFormalSpec(object p0)
		{
			return EntityRes.GetString("NumberOfTypeCtorIsLessThenFormalSpec", new object[]
			{
				p0
			});
		}

		// Token: 0x060013A4 RID: 5028 RVA: 0x00046740 File Offset: 0x00044940
		internal static string NumberOfTypeCtorIsMoreThenFormalSpec(object p0)
		{
			return EntityRes.GetString("NumberOfTypeCtorIsMoreThenFormalSpec", new object[]
			{
				p0
			});
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x060013A5 RID: 5029 RVA: 0x00046756 File Offset: 0x00044956
		internal static string OrderByKeyIsNotOrderComparable
		{
			get
			{
				return EntityRes.GetString("OrderByKeyIsNotOrderComparable");
			}
		}

		// Token: 0x060013A6 RID: 5030 RVA: 0x00046762 File Offset: 0x00044962
		internal static string OfTypeOnlyTypeArgumentCannotBeAbstract(object p0)
		{
			return EntityRes.GetString("OfTypeOnlyTypeArgumentCannotBeAbstract", new object[]
			{
				p0
			});
		}

		// Token: 0x060013A7 RID: 5031 RVA: 0x00046778 File Offset: 0x00044978
		internal static string ParameterTypeNotSupported(object p0, object p1)
		{
			return EntityRes.GetString("ParameterTypeNotSupported", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060013A8 RID: 5032 RVA: 0x00046792 File Offset: 0x00044992
		internal static string ParameterWasNotDefined(object p0)
		{
			return EntityRes.GetString("ParameterWasNotDefined", new object[]
			{
				p0
			});
		}

		// Token: 0x060013A9 RID: 5033 RVA: 0x000467A8 File Offset: 0x000449A8
		internal static string PlaceholderExpressionMustBeCompatibleWithEdm64(object p0, object p1)
		{
			return EntityRes.GetString("PlaceholderExpressionMustBeCompatibleWithEdm64", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060013AA RID: 5034 RVA: 0x000467C2 File Offset: 0x000449C2
		internal static string PlaceholderExpressionMustBeConstant(object p0)
		{
			return EntityRes.GetString("PlaceholderExpressionMustBeConstant", new object[]
			{
				p0
			});
		}

		// Token: 0x060013AB RID: 5035 RVA: 0x000467D8 File Offset: 0x000449D8
		internal static string PlaceholderExpressionMustBeGreaterThanOrEqualToZero(object p0)
		{
			return EntityRes.GetString("PlaceholderExpressionMustBeGreaterThanOrEqualToZero", new object[]
			{
				p0
			});
		}

		// Token: 0x060013AC RID: 5036 RVA: 0x000467EE File Offset: 0x000449EE
		internal static string PlaceholderSetArgTypeIsNotEqualComparable(object p0, object p1, object p2)
		{
			return EntityRes.GetString("PlaceholderSetArgTypeIsNotEqualComparable", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x060013AD RID: 5037 RVA: 0x0004680C File Offset: 0x00044A0C
		internal static string PlusLeftExpressionInvalidType
		{
			get
			{
				return EntityRes.GetString("PlusLeftExpressionInvalidType");
			}
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x060013AE RID: 5038 RVA: 0x00046818 File Offset: 0x00044A18
		internal static string PlusRightExpressionInvalidType
		{
			get
			{
				return EntityRes.GetString("PlusRightExpressionInvalidType");
			}
		}

		// Token: 0x060013AF RID: 5039 RVA: 0x00046824 File Offset: 0x00044A24
		internal static string PrecisionMustBeGreaterThanScale(object p0, object p1)
		{
			return EntityRes.GetString("PrecisionMustBeGreaterThanScale", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060013B0 RID: 5040 RVA: 0x0004683E File Offset: 0x00044A3E
		internal static string RefArgIsNotOfEntityType(object p0)
		{
			return EntityRes.GetString("RefArgIsNotOfEntityType", new object[]
			{
				p0
			});
		}

		// Token: 0x060013B1 RID: 5041 RVA: 0x00046854 File Offset: 0x00044A54
		internal static string RefTypeIdentifierMustSpecifyAnEntityType(object p0, object p1)
		{
			return EntityRes.GetString("RefTypeIdentifierMustSpecifyAnEntityType", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x060013B2 RID: 5042 RVA: 0x0004686E File Offset: 0x00044A6E
		internal static string RelatedEndExprTypeMustBeReference
		{
			get
			{
				return EntityRes.GetString("RelatedEndExprTypeMustBeReference");
			}
		}

		// Token: 0x060013B3 RID: 5043 RVA: 0x0004687A File Offset: 0x00044A7A
		internal static string RelatedEndExprTypeMustBePromotoableToToEnd(object p0, object p1)
		{
			return EntityRes.GetString("RelatedEndExprTypeMustBePromotoableToToEnd", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x060013B4 RID: 5044 RVA: 0x00046894 File Offset: 0x00044A94
		internal static string RelationshipFromEndIsAmbiguos
		{
			get
			{
				return EntityRes.GetString("RelationshipFromEndIsAmbiguos");
			}
		}

		// Token: 0x060013B5 RID: 5045 RVA: 0x000468A0 File Offset: 0x00044AA0
		internal static string RelationshipTypeExpected(object p0)
		{
			return EntityRes.GetString("RelationshipTypeExpected", new object[]
			{
				p0
			});
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x060013B6 RID: 5046 RVA: 0x000468B6 File Offset: 0x00044AB6
		internal static string RelationshipToEndIsAmbiguos
		{
			get
			{
				return EntityRes.GetString("RelationshipToEndIsAmbiguos");
			}
		}

		// Token: 0x060013B7 RID: 5047 RVA: 0x000468C2 File Offset: 0x00044AC2
		internal static string RelationshipTargetMustBeUnique(object p0)
		{
			return EntityRes.GetString("RelationshipTargetMustBeUnique", new object[]
			{
				p0
			});
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x060013B8 RID: 5048 RVA: 0x000468D8 File Offset: 0x00044AD8
		internal static string ResultingExpressionTypeCannotBeNull
		{
			get
			{
				return EntityRes.GetString("ResultingExpressionTypeCannotBeNull");
			}
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x060013B9 RID: 5049 RVA: 0x000468E4 File Offset: 0x00044AE4
		internal static string RightSetExpressionArgsMustBeCollection
		{
			get
			{
				return EntityRes.GetString("RightSetExpressionArgsMustBeCollection");
			}
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x060013BA RID: 5050 RVA: 0x000468F0 File Offset: 0x00044AF0
		internal static string RowCtorElementCannotBeNull
		{
			get
			{
				return EntityRes.GetString("RowCtorElementCannotBeNull");
			}
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x060013BB RID: 5051 RVA: 0x000468FC File Offset: 0x00044AFC
		internal static string SelectDistinctMustBeEqualComparable
		{
			get
			{
				return EntityRes.GetString("SelectDistinctMustBeEqualComparable");
			}
		}

		// Token: 0x060013BC RID: 5052 RVA: 0x00046908 File Offset: 0x00044B08
		internal static string SourceTypeMustBePromotoableToFromEndRelationType(object p0, object p1)
		{
			return EntityRes.GetString("SourceTypeMustBePromotoableToFromEndRelationType", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x060013BD RID: 5053 RVA: 0x00046922 File Offset: 0x00044B22
		internal static string TopAndLimitCannotCoexist
		{
			get
			{
				return EntityRes.GetString("TopAndLimitCannotCoexist");
			}
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x060013BE RID: 5054 RVA: 0x0004692E File Offset: 0x00044B2E
		internal static string TopAndSkipCannotCoexist
		{
			get
			{
				return EntityRes.GetString("TopAndSkipCannotCoexist");
			}
		}

		// Token: 0x060013BF RID: 5055 RVA: 0x0004693A File Offset: 0x00044B3A
		internal static string TypeDoesNotSupportSpec(object p0)
		{
			return EntityRes.GetString("TypeDoesNotSupportSpec", new object[]
			{
				p0
			});
		}

		// Token: 0x060013C0 RID: 5056 RVA: 0x00046950 File Offset: 0x00044B50
		internal static string TypeDoesNotSupportFacet(object p0, object p1)
		{
			return EntityRes.GetString("TypeDoesNotSupportFacet", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060013C1 RID: 5057 RVA: 0x0004696A File Offset: 0x00044B6A
		internal static string TypeArgumentCountMismatch(object p0, object p1)
		{
			return EntityRes.GetString("TypeArgumentCountMismatch", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x060013C2 RID: 5058 RVA: 0x00046984 File Offset: 0x00044B84
		internal static string TypeArgumentMustBeLiteral
		{
			get
			{
				return EntityRes.GetString("TypeArgumentMustBeLiteral");
			}
		}

		// Token: 0x060013C3 RID: 5059 RVA: 0x00046990 File Offset: 0x00044B90
		internal static string TypeArgumentBelowMin(object p0)
		{
			return EntityRes.GetString("TypeArgumentBelowMin", new object[]
			{
				p0
			});
		}

		// Token: 0x060013C4 RID: 5060 RVA: 0x000469A6 File Offset: 0x00044BA6
		internal static string TypeArgumentExceedsMax(object p0)
		{
			return EntityRes.GetString("TypeArgumentExceedsMax", new object[]
			{
				p0
			});
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x060013C5 RID: 5061 RVA: 0x000469BC File Offset: 0x00044BBC
		internal static string TypeArgumentIsNotValid
		{
			get
			{
				return EntityRes.GetString("TypeArgumentIsNotValid");
			}
		}

		// Token: 0x060013C6 RID: 5062 RVA: 0x000469C8 File Offset: 0x00044BC8
		internal static string TypeKindMismatch(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("TypeKindMismatch", new object[]
			{
				p0,
				p1,
				p2,
				p3
			});
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x060013C7 RID: 5063 RVA: 0x000469EA File Offset: 0x00044BEA
		internal static string TypeMustBeInheritableType
		{
			get
			{
				return EntityRes.GetString("TypeMustBeInheritableType");
			}
		}

		// Token: 0x060013C8 RID: 5064 RVA: 0x000469F6 File Offset: 0x00044BF6
		internal static string TypeMustBeEntityType(object p0, object p1, object p2)
		{
			return EntityRes.GetString("TypeMustBeEntityType", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x060013C9 RID: 5065 RVA: 0x00046A14 File Offset: 0x00044C14
		internal static string TypeMustBeNominalType(object p0, object p1, object p2)
		{
			return EntityRes.GetString("TypeMustBeNominalType", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x060013CA RID: 5066 RVA: 0x00046A32 File Offset: 0x00044C32
		internal static string TypeNameNotFound(object p0)
		{
			return EntityRes.GetString("TypeNameNotFound", new object[]
			{
				p0
			});
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x060013CB RID: 5067 RVA: 0x00046A48 File Offset: 0x00044C48
		internal static string GroupVarNotFoundInScope
		{
			get
			{
				return EntityRes.GetString("GroupVarNotFoundInScope");
			}
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x060013CC RID: 5068 RVA: 0x00046A54 File Offset: 0x00044C54
		internal static string InvalidArgumentTypeForAggregateFunction
		{
			get
			{
				return EntityRes.GetString("InvalidArgumentTypeForAggregateFunction");
			}
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x060013CD RID: 5069 RVA: 0x00046A60 File Offset: 0x00044C60
		internal static string InvalidSavePoint
		{
			get
			{
				return EntityRes.GetString("InvalidSavePoint");
			}
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x060013CE RID: 5070 RVA: 0x00046A6C File Offset: 0x00044C6C
		internal static string InvalidScopeIndex
		{
			get
			{
				return EntityRes.GetString("InvalidScopeIndex");
			}
		}

		// Token: 0x060013CF RID: 5071 RVA: 0x00046A78 File Offset: 0x00044C78
		internal static string LiteralTypeNotSupported(object p0)
		{
			return EntityRes.GetString("LiteralTypeNotSupported", new object[]
			{
				p0
			});
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x060013D0 RID: 5072 RVA: 0x00046A8E File Offset: 0x00044C8E
		internal static string ParserFatalError
		{
			get
			{
				return EntityRes.GetString("ParserFatalError");
			}
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x060013D1 RID: 5073 RVA: 0x00046A9A File Offset: 0x00044C9A
		internal static string ParserInputError
		{
			get
			{
				return EntityRes.GetString("ParserInputError");
			}
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x060013D2 RID: 5074 RVA: 0x00046AA6 File Offset: 0x00044CA6
		internal static string StackOverflowInParser
		{
			get
			{
				return EntityRes.GetString("StackOverflowInParser");
			}
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x060013D3 RID: 5075 RVA: 0x00046AB2 File Offset: 0x00044CB2
		internal static string UnknownAstCommandExpression
		{
			get
			{
				return EntityRes.GetString("UnknownAstCommandExpression");
			}
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x060013D4 RID: 5076 RVA: 0x00046ABE File Offset: 0x00044CBE
		internal static string UnknownAstExpressionType
		{
			get
			{
				return EntityRes.GetString("UnknownAstExpressionType");
			}
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x060013D5 RID: 5077 RVA: 0x00046ACA File Offset: 0x00044CCA
		internal static string UnknownBuiltInAstExpressionType
		{
			get
			{
				return EntityRes.GetString("UnknownBuiltInAstExpressionType");
			}
		}

		// Token: 0x060013D6 RID: 5078 RVA: 0x00046AD6 File Offset: 0x00044CD6
		internal static string UnknownExpressionResolutionClass(object p0)
		{
			return EntityRes.GetString("UnknownExpressionResolutionClass", new object[]
			{
				p0
			});
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x060013D7 RID: 5079 RVA: 0x00046AEC File Offset: 0x00044CEC
		internal static string SqlGen_ApplyNotSupportedOnSql8
		{
			get
			{
				return EntityRes.GetString("SqlGen_ApplyNotSupportedOnSql8");
			}
		}

		// Token: 0x060013D8 RID: 5080 RVA: 0x00046AF8 File Offset: 0x00044CF8
		internal static string SqlGen_InvalidDatePartArgumentExpression(object p0, object p1)
		{
			return EntityRes.GetString("SqlGen_InvalidDatePartArgumentExpression", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060013D9 RID: 5081 RVA: 0x00046B12 File Offset: 0x00044D12
		internal static string SqlGen_InvalidDatePartArgumentValue(object p0, object p1, object p2)
		{
			return EntityRes.GetString("SqlGen_InvalidDatePartArgumentValue", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x060013DA RID: 5082 RVA: 0x00046B30 File Offset: 0x00044D30
		internal static string SqlGen_NiladicFunctionsCannotHaveParameters
		{
			get
			{
				return EntityRes.GetString("SqlGen_NiladicFunctionsCannotHaveParameters");
			}
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x060013DB RID: 5083 RVA: 0x00046B3C File Offset: 0x00044D3C
		internal static string SqlGen_ParameterForLimitNotSupportedOnSql8
		{
			get
			{
				return EntityRes.GetString("SqlGen_ParameterForLimitNotSupportedOnSql8");
			}
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x060013DC RID: 5084 RVA: 0x00046B48 File Offset: 0x00044D48
		internal static string SqlGen_ParameterForSkipNotSupportedOnSql8
		{
			get
			{
				return EntityRes.GetString("SqlGen_ParameterForSkipNotSupportedOnSql8");
			}
		}

		// Token: 0x060013DD RID: 5085 RVA: 0x00046B54 File Offset: 0x00044D54
		internal static string SqlGen_PrimitiveTypeNotSupportedPriorSql10(object p0)
		{
			return EntityRes.GetString("SqlGen_PrimitiveTypeNotSupportedPriorSql10", new object[]
			{
				p0
			});
		}

		// Token: 0x060013DE RID: 5086 RVA: 0x00046B6A File Offset: 0x00044D6A
		internal static string SqlGen_CanonicalFunctionNotSupportedPriorSql10(object p0)
		{
			return EntityRes.GetString("SqlGen_CanonicalFunctionNotSupportedPriorSql10", new object[]
			{
				p0
			});
		}

		// Token: 0x060013DF RID: 5087 RVA: 0x00046B80 File Offset: 0x00044D80
		internal static string SqlGen_TypedPositiveInfinityNotSupported(object p0, object p1)
		{
			return EntityRes.GetString("SqlGen_TypedPositiveInfinityNotSupported", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060013E0 RID: 5088 RVA: 0x00046B9A File Offset: 0x00044D9A
		internal static string SqlGen_TypedNegativeInfinityNotSupported(object p0, object p1)
		{
			return EntityRes.GetString("SqlGen_TypedNegativeInfinityNotSupported", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060013E1 RID: 5089 RVA: 0x00046BB4 File Offset: 0x00044DB4
		internal static string SqlGen_TypedNaNNotSupported(object p0)
		{
			return EntityRes.GetString("SqlGen_TypedNaNNotSupported", new object[]
			{
				p0
			});
		}

		// Token: 0x060013E2 RID: 5090 RVA: 0x00046BCA File Offset: 0x00044DCA
		internal static string Cqt_General_PolymorphicTypeRequired(object p0)
		{
			return EntityRes.GetString("Cqt_General_PolymorphicTypeRequired", new object[]
			{
				p0
			});
		}

		// Token: 0x060013E3 RID: 5091 RVA: 0x00046BE0 File Offset: 0x00044DE0
		internal static string Cqt_General_PolymorphicArgRequired(object p0)
		{
			return EntityRes.GetString("Cqt_General_PolymorphicArgRequired", new object[]
			{
				p0
			});
		}

		// Token: 0x060013E4 RID: 5092 RVA: 0x00046BF6 File Offset: 0x00044DF6
		internal static string Cqt_General_UnsupportedExpression(object p0)
		{
			return EntityRes.GetString("Cqt_General_UnsupportedExpression", new object[]
			{
				p0
			});
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x060013E5 RID: 5093 RVA: 0x00046C0C File Offset: 0x00044E0C
		internal static string Cqt_General_MetadataNotReadOnly
		{
			get
			{
				return EntityRes.GetString("Cqt_General_MetadataNotReadOnly");
			}
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x060013E6 RID: 5094 RVA: 0x00046C18 File Offset: 0x00044E18
		internal static string Cqt_General_NoProviderBooleanType
		{
			get
			{
				return EntityRes.GetString("Cqt_General_NoProviderBooleanType");
			}
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x060013E7 RID: 5095 RVA: 0x00046C24 File Offset: 0x00044E24
		internal static string Cqt_General_NoProviderIntegerType
		{
			get
			{
				return EntityRes.GetString("Cqt_General_NoProviderIntegerType");
			}
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x060013E8 RID: 5096 RVA: 0x00046C30 File Offset: 0x00044E30
		internal static string Cqt_General_NoProviderStringType
		{
			get
			{
				return EntityRes.GetString("Cqt_General_NoProviderStringType");
			}
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x060013E9 RID: 5097 RVA: 0x00046C3C File Offset: 0x00044E3C
		internal static string Cqt_Metadata_EdmMemberIncorrectSpace
		{
			get
			{
				return EntityRes.GetString("Cqt_Metadata_EdmMemberIncorrectSpace");
			}
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x060013EA RID: 5098 RVA: 0x00046C48 File Offset: 0x00044E48
		internal static string Cqt_Metadata_EntitySetEntityContainerNull
		{
			get
			{
				return EntityRes.GetString("Cqt_Metadata_EntitySetEntityContainerNull");
			}
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x060013EB RID: 5099 RVA: 0x00046C54 File Offset: 0x00044E54
		internal static string Cqt_Metadata_EntitySetIncorrectSpace
		{
			get
			{
				return EntityRes.GetString("Cqt_Metadata_EntitySetIncorrectSpace");
			}
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x060013EC RID: 5100 RVA: 0x00046C60 File Offset: 0x00044E60
		internal static string Cqt_Metadata_EntityTypeNullKeyMembersInvalid
		{
			get
			{
				return EntityRes.GetString("Cqt_Metadata_EntityTypeNullKeyMembersInvalid");
			}
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x060013ED RID: 5101 RVA: 0x00046C6C File Offset: 0x00044E6C
		internal static string Cqt_Metadata_EntityTypeEmptyKeyMembersInvalid
		{
			get
			{
				return EntityRes.GetString("Cqt_Metadata_EntityTypeEmptyKeyMembersInvalid");
			}
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x060013EE RID: 5102 RVA: 0x00046C78 File Offset: 0x00044E78
		internal static string Cqt_Metadata_FunctionReturnParameterNull
		{
			get
			{
				return EntityRes.GetString("Cqt_Metadata_FunctionReturnParameterNull");
			}
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x060013EF RID: 5103 RVA: 0x00046C84 File Offset: 0x00044E84
		internal static string Cqt_Metadata_FunctionIncorrectSpace
		{
			get
			{
				return EntityRes.GetString("Cqt_Metadata_FunctionIncorrectSpace");
			}
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x060013F0 RID: 5104 RVA: 0x00046C90 File Offset: 0x00044E90
		internal static string Cqt_Metadata_FunctionParameterIncorrectSpace
		{
			get
			{
				return EntityRes.GetString("Cqt_Metadata_FunctionParameterIncorrectSpace");
			}
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x060013F1 RID: 5105 RVA: 0x00046C9C File Offset: 0x00044E9C
		internal static string Cqt_Metadata_TypeUsageIncorrectSpace
		{
			get
			{
				return EntityRes.GetString("Cqt_Metadata_TypeUsageIncorrectSpace");
			}
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x060013F2 RID: 5106 RVA: 0x00046CA8 File Offset: 0x00044EA8
		internal static string Cqt_Exceptions_InvalidCommandTree
		{
			get
			{
				return EntityRes.GetString("Cqt_Exceptions_InvalidCommandTree");
			}
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x060013F3 RID: 5107 RVA: 0x00046CB4 File Offset: 0x00044EB4
		internal static string Cqt_Util_CheckListEmptyInvalid
		{
			get
			{
				return EntityRes.GetString("Cqt_Util_CheckListEmptyInvalid");
			}
		}

		// Token: 0x060013F4 RID: 5108 RVA: 0x00046CC0 File Offset: 0x00044EC0
		internal static string Cqt_Util_CheckListDuplicateName(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Cqt_Util_CheckListDuplicateName", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x060013F5 RID: 5109 RVA: 0x00046CDE File Offset: 0x00044EDE
		internal static string Cqt_ExpressionLink_TypeMismatch(object p0, object p1)
		{
			return EntityRes.GetString("Cqt_ExpressionLink_TypeMismatch", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x060013F6 RID: 5110 RVA: 0x00046CF8 File Offset: 0x00044EF8
		internal static string Cqt_ExpressionList_IncorrectElementCount
		{
			get
			{
				return EntityRes.GetString("Cqt_ExpressionList_IncorrectElementCount");
			}
		}

		// Token: 0x060013F7 RID: 5111 RVA: 0x00046D04 File Offset: 0x00044F04
		internal static string Cqt_Copier_EntityContainerNotFound(object p0)
		{
			return EntityRes.GetString("Cqt_Copier_EntityContainerNotFound", new object[]
			{
				p0
			});
		}

		// Token: 0x060013F8 RID: 5112 RVA: 0x00046D1A File Offset: 0x00044F1A
		internal static string Cqt_Copier_EntitySetNotFound(object p0, object p1)
		{
			return EntityRes.GetString("Cqt_Copier_EntitySetNotFound", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060013F9 RID: 5113 RVA: 0x00046D34 File Offset: 0x00044F34
		internal static string Cqt_Copier_FunctionNotFound(object p0)
		{
			return EntityRes.GetString("Cqt_Copier_FunctionNotFound", new object[]
			{
				p0
			});
		}

		// Token: 0x060013FA RID: 5114 RVA: 0x00046D4A File Offset: 0x00044F4A
		internal static string Cqt_Copier_PropertyNotFound(object p0, object p1)
		{
			return EntityRes.GetString("Cqt_Copier_PropertyNotFound", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060013FB RID: 5115 RVA: 0x00046D64 File Offset: 0x00044F64
		internal static string Cqt_Copier_NavPropertyNotFound(object p0, object p1)
		{
			return EntityRes.GetString("Cqt_Copier_NavPropertyNotFound", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060013FC RID: 5116 RVA: 0x00046D7E File Offset: 0x00044F7E
		internal static string Cqt_Copier_EndNotFound(object p0, object p1)
		{
			return EntityRes.GetString("Cqt_Copier_EndNotFound", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060013FD RID: 5117 RVA: 0x00046D98 File Offset: 0x00044F98
		internal static string Cqt_Copier_TypeNotFound(object p0)
		{
			return EntityRes.GetString("Cqt_Copier_TypeNotFound", new object[]
			{
				p0
			});
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x060013FE RID: 5118 RVA: 0x00046DAE File Offset: 0x00044FAE
		internal static string Cqt_CommandTree_InvalidDataSpace
		{
			get
			{
				return EntityRes.GetString("Cqt_CommandTree_InvalidDataSpace");
			}
		}

		// Token: 0x060013FF RID: 5119 RVA: 0x00046DBA File Offset: 0x00044FBA
		internal static string Cqt_CommandTree_InvalidParameterName(object p0)
		{
			return EntityRes.GetString("Cqt_CommandTree_InvalidParameterName", new object[]
			{
				p0
			});
		}

		// Token: 0x06001400 RID: 5120 RVA: 0x00046DD0 File Offset: 0x00044FD0
		internal static string Cqt_Validator_InvalidIncompatibleParameterReferences(object p0)
		{
			return EntityRes.GetString("Cqt_Validator_InvalidIncompatibleParameterReferences", new object[]
			{
				p0
			});
		}

		// Token: 0x06001401 RID: 5121 RVA: 0x00046DE6 File Offset: 0x00044FE6
		internal static string Cqt_Validator_InvalidOtherWorkspaceMetadata(object p0)
		{
			return EntityRes.GetString("Cqt_Validator_InvalidOtherWorkspaceMetadata", new object[]
			{
				p0
			});
		}

		// Token: 0x06001402 RID: 5122 RVA: 0x00046DFC File Offset: 0x00044FFC
		internal static string Cqt_Validator_InvalidIncorrectDataSpaceMetadata(object p0, object p1)
		{
			return EntityRes.GetString("Cqt_Validator_InvalidIncorrectDataSpaceMetadata", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06001403 RID: 5123 RVA: 0x00046E16 File Offset: 0x00045016
		internal static string Cqt_Factory_NewCollectionInvalidCommonType
		{
			get
			{
				return EntityRes.GetString("Cqt_Factory_NewCollectionInvalidCommonType");
			}
		}

		// Token: 0x06001404 RID: 5124 RVA: 0x00046E22 File Offset: 0x00045022
		internal static string Cqt_Factory_NoSuchProperty(object p0, object p1)
		{
			return EntityRes.GetString("Cqt_Factory_NoSuchProperty", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06001405 RID: 5125 RVA: 0x00046E3C File Offset: 0x0004503C
		internal static string Cqt_Factory_NoSuchRelationEnd
		{
			get
			{
				return EntityRes.GetString("Cqt_Factory_NoSuchRelationEnd");
			}
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06001406 RID: 5126 RVA: 0x00046E48 File Offset: 0x00045048
		internal static string Cqt_Factory_IncompatibleRelationEnds
		{
			get
			{
				return EntityRes.GetString("Cqt_Factory_IncompatibleRelationEnds");
			}
		}

		// Token: 0x06001407 RID: 5127 RVA: 0x00046E54 File Offset: 0x00045054
		internal static string Cqt_Factory_MethodResultTypeNotSupported(object p0)
		{
			return EntityRes.GetString("Cqt_Factory_MethodResultTypeNotSupported", new object[]
			{
				p0
			});
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06001408 RID: 5128 RVA: 0x00046E6A File Offset: 0x0004506A
		internal static string Cqt_Aggregate_InvalidFunction
		{
			get
			{
				return EntityRes.GetString("Cqt_Aggregate_InvalidFunction");
			}
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06001409 RID: 5129 RVA: 0x00046E76 File Offset: 0x00045076
		internal static string Cqt_Binding_CollectionRequired
		{
			get
			{
				return EntityRes.GetString("Cqt_Binding_CollectionRequired");
			}
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x0600140A RID: 5130 RVA: 0x00046E82 File Offset: 0x00045082
		internal static string Cqt_Binding_VariableNameNotValid
		{
			get
			{
				return EntityRes.GetString("Cqt_Binding_VariableNameNotValid");
			}
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x0600140B RID: 5131 RVA: 0x00046E8E File Offset: 0x0004508E
		internal static string Cqt_GroupBinding_CollectionRequired
		{
			get
			{
				return EntityRes.GetString("Cqt_GroupBinding_CollectionRequired");
			}
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x0600140C RID: 5132 RVA: 0x00046E9A File Offset: 0x0004509A
		internal static string Cqt_GroupBinding_GroupVariableNameNotValid
		{
			get
			{
				return EntityRes.GetString("Cqt_GroupBinding_GroupVariableNameNotValid");
			}
		}

		// Token: 0x0600140D RID: 5133 RVA: 0x00046EA6 File Offset: 0x000450A6
		internal static string Cqt_Binary_CollectionsRequired(object p0)
		{
			return EntityRes.GetString("Cqt_Binary_CollectionsRequired", new object[]
			{
				p0
			});
		}

		// Token: 0x0600140E RID: 5134 RVA: 0x00046EBC File Offset: 0x000450BC
		internal static string Cqt_Unary_CollectionRequired(object p0)
		{
			return EntityRes.GetString("Cqt_Unary_CollectionRequired", new object[]
			{
				p0
			});
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x0600140F RID: 5135 RVA: 0x00046ED2 File Offset: 0x000450D2
		internal static string Cqt_And_BooleanArgumentsRequired
		{
			get
			{
				return EntityRes.GetString("Cqt_And_BooleanArgumentsRequired");
			}
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06001410 RID: 5136 RVA: 0x00046EDE File Offset: 0x000450DE
		internal static string Cqt_Apply_DuplicateVariableNames
		{
			get
			{
				return EntityRes.GetString("Cqt_Apply_DuplicateVariableNames");
			}
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06001411 RID: 5137 RVA: 0x00046EEA File Offset: 0x000450EA
		internal static string Cqt_Arithmetic_NumericCommonType
		{
			get
			{
				return EntityRes.GetString("Cqt_Arithmetic_NumericCommonType");
			}
		}

		// Token: 0x06001412 RID: 5138 RVA: 0x00046EF6 File Offset: 0x000450F6
		internal static string Cqt_Arithmetic_InvalidUnsignedTypeForUnaryMinus(object p0)
		{
			return EntityRes.GetString("Cqt_Arithmetic_InvalidUnsignedTypeForUnaryMinus", new object[]
			{
				p0
			});
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06001413 RID: 5139 RVA: 0x00046F0C File Offset: 0x0004510C
		internal static string Cqt_Case_WhensMustEqualThens
		{
			get
			{
				return EntityRes.GetString("Cqt_Case_WhensMustEqualThens");
			}
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06001414 RID: 5140 RVA: 0x00046F18 File Offset: 0x00045118
		internal static string Cqt_Case_InvalidResultType
		{
			get
			{
				return EntityRes.GetString("Cqt_Case_InvalidResultType");
			}
		}

		// Token: 0x06001415 RID: 5141 RVA: 0x00046F24 File Offset: 0x00045124
		internal static string Cqt_Cast_InvalidCast(object p0, object p1)
		{
			return EntityRes.GetString("Cqt_Cast_InvalidCast", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06001416 RID: 5142 RVA: 0x00046F3E File Offset: 0x0004513E
		internal static string Cqt_Comparison_ComparableRequired
		{
			get
			{
				return EntityRes.GetString("Cqt_Comparison_ComparableRequired");
			}
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06001417 RID: 5143 RVA: 0x00046F4A File Offset: 0x0004514A
		internal static string Cqt_Constant_InvalidType
		{
			get
			{
				return EntityRes.GetString("Cqt_Constant_InvalidType");
			}
		}

		// Token: 0x06001418 RID: 5144 RVA: 0x00046F56 File Offset: 0x00045156
		internal static string Cqt_Constant_InvalidValueForType(object p0)
		{
			return EntityRes.GetString("Cqt_Constant_InvalidValueForType", new object[]
			{
				p0
			});
		}

		// Token: 0x06001419 RID: 5145 RVA: 0x00046F6C File Offset: 0x0004516C
		internal static string Cqt_Constant_InvalidConstantType(object p0)
		{
			return EntityRes.GetString("Cqt_Constant_InvalidConstantType", new object[]
			{
				p0
			});
		}

		// Token: 0x0600141A RID: 5146 RVA: 0x00046F82 File Offset: 0x00045182
		internal static string Cqt_Constant_ClrEnumTypeDoesNotMatchEdmEnumType(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Cqt_Constant_ClrEnumTypeDoesNotMatchEdmEnumType", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x0600141B RID: 5147 RVA: 0x00046FA0 File Offset: 0x000451A0
		internal static string Cqt_Distinct_InvalidCollection
		{
			get
			{
				return EntityRes.GetString("Cqt_Distinct_InvalidCollection");
			}
		}

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x0600141C RID: 5148 RVA: 0x00046FAC File Offset: 0x000451AC
		internal static string Cqt_DeRef_RefRequired
		{
			get
			{
				return EntityRes.GetString("Cqt_DeRef_RefRequired");
			}
		}

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x0600141D RID: 5149 RVA: 0x00046FB8 File Offset: 0x000451B8
		internal static string Cqt_Element_InvalidArgumentForUnwrapSingleProperty
		{
			get
			{
				return EntityRes.GetString("Cqt_Element_InvalidArgumentForUnwrapSingleProperty");
			}
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x0600141E RID: 5150 RVA: 0x00046FC4 File Offset: 0x000451C4
		internal static string Cqt_Function_VoidResultInvalid
		{
			get
			{
				return EntityRes.GetString("Cqt_Function_VoidResultInvalid");
			}
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x0600141F RID: 5151 RVA: 0x00046FD0 File Offset: 0x000451D0
		internal static string Cqt_Function_NonComposableInExpression
		{
			get
			{
				return EntityRes.GetString("Cqt_Function_NonComposableInExpression");
			}
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06001420 RID: 5152 RVA: 0x00046FDC File Offset: 0x000451DC
		internal static string Cqt_Function_CommandTextInExpression
		{
			get
			{
				return EntityRes.GetString("Cqt_Function_CommandTextInExpression");
			}
		}

		// Token: 0x06001421 RID: 5153 RVA: 0x00046FE8 File Offset: 0x000451E8
		internal static string Cqt_Function_CanonicalFunction_NotFound(object p0)
		{
			return EntityRes.GetString("Cqt_Function_CanonicalFunction_NotFound", new object[]
			{
				p0
			});
		}

		// Token: 0x06001422 RID: 5154 RVA: 0x00046FFE File Offset: 0x000451FE
		internal static string Cqt_Function_CanonicalFunction_AmbiguousMatch(object p0)
		{
			return EntityRes.GetString("Cqt_Function_CanonicalFunction_AmbiguousMatch", new object[]
			{
				p0
			});
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06001423 RID: 5155 RVA: 0x00047014 File Offset: 0x00045214
		internal static string Cqt_GetEntityRef_EntityRequired
		{
			get
			{
				return EntityRes.GetString("Cqt_GetEntityRef_EntityRequired");
			}
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06001424 RID: 5156 RVA: 0x00047020 File Offset: 0x00045220
		internal static string Cqt_GetRefKey_RefRequired
		{
			get
			{
				return EntityRes.GetString("Cqt_GetRefKey_RefRequired");
			}
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06001425 RID: 5157 RVA: 0x0004702C File Offset: 0x0004522C
		internal static string Cqt_GroupBy_AtLeastOneKeyOrAggregate
		{
			get
			{
				return EntityRes.GetString("Cqt_GroupBy_AtLeastOneKeyOrAggregate");
			}
		}

		// Token: 0x06001426 RID: 5158 RVA: 0x00047038 File Offset: 0x00045238
		internal static string Cqt_GroupBy_KeyNotEqualityComparable(object p0)
		{
			return EntityRes.GetString("Cqt_GroupBy_KeyNotEqualityComparable", new object[]
			{
				p0
			});
		}

		// Token: 0x06001427 RID: 5159 RVA: 0x0004704E File Offset: 0x0004524E
		internal static string Cqt_GroupBy_AggregateColumnExistsAsGroupColumn(object p0)
		{
			return EntityRes.GetString("Cqt_GroupBy_AggregateColumnExistsAsGroupColumn", new object[]
			{
				p0
			});
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06001428 RID: 5160 RVA: 0x00047064 File Offset: 0x00045264
		internal static string Cqt_GroupBy_MoreThanOneGroupAggregate
		{
			get
			{
				return EntityRes.GetString("Cqt_GroupBy_MoreThanOneGroupAggregate");
			}
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06001429 RID: 5161 RVA: 0x00047070 File Offset: 0x00045270
		internal static string Cqt_CrossJoin_AtLeastTwoInputs
		{
			get
			{
				return EntityRes.GetString("Cqt_CrossJoin_AtLeastTwoInputs");
			}
		}

		// Token: 0x0600142A RID: 5162 RVA: 0x0004707C File Offset: 0x0004527C
		internal static string Cqt_CrossJoin_DuplicateVariableNames(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Cqt_CrossJoin_DuplicateVariableNames", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x0600142B RID: 5163 RVA: 0x0004709A File Offset: 0x0004529A
		internal static string Cqt_IsNull_CollectionNotAllowed
		{
			get
			{
				return EntityRes.GetString("Cqt_IsNull_CollectionNotAllowed");
			}
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x0600142C RID: 5164 RVA: 0x000470A6 File Offset: 0x000452A6
		internal static string Cqt_IsNull_InvalidType
		{
			get
			{
				return EntityRes.GetString("Cqt_IsNull_InvalidType");
			}
		}

		// Token: 0x0600142D RID: 5165 RVA: 0x000470B2 File Offset: 0x000452B2
		internal static string Cqt_InvalidTypeForSetOperation(object p0, object p1)
		{
			return EntityRes.GetString("Cqt_InvalidTypeForSetOperation", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x0600142E RID: 5166 RVA: 0x000470CC File Offset: 0x000452CC
		internal static string Cqt_Join_DuplicateVariableNames
		{
			get
			{
				return EntityRes.GetString("Cqt_Join_DuplicateVariableNames");
			}
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x0600142F RID: 5167 RVA: 0x000470D8 File Offset: 0x000452D8
		internal static string Cqt_Limit_ConstantOrParameterRefRequired
		{
			get
			{
				return EntityRes.GetString("Cqt_Limit_ConstantOrParameterRefRequired");
			}
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06001430 RID: 5168 RVA: 0x000470E4 File Offset: 0x000452E4
		internal static string Cqt_Limit_IntegerRequired
		{
			get
			{
				return EntityRes.GetString("Cqt_Limit_IntegerRequired");
			}
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06001431 RID: 5169 RVA: 0x000470F0 File Offset: 0x000452F0
		internal static string Cqt_Limit_NonNegativeLimitRequired
		{
			get
			{
				return EntityRes.GetString("Cqt_Limit_NonNegativeLimitRequired");
			}
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06001432 RID: 5170 RVA: 0x000470FC File Offset: 0x000452FC
		internal static string Cqt_NewInstance_CollectionTypeRequired
		{
			get
			{
				return EntityRes.GetString("Cqt_NewInstance_CollectionTypeRequired");
			}
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06001433 RID: 5171 RVA: 0x00047108 File Offset: 0x00045308
		internal static string Cqt_NewInstance_StructuralTypeRequired
		{
			get
			{
				return EntityRes.GetString("Cqt_NewInstance_StructuralTypeRequired");
			}
		}

		// Token: 0x06001434 RID: 5172 RVA: 0x00047114 File Offset: 0x00045314
		internal static string Cqt_NewInstance_CannotInstantiateMemberlessType(object p0)
		{
			return EntityRes.GetString("Cqt_NewInstance_CannotInstantiateMemberlessType", new object[]
			{
				p0
			});
		}

		// Token: 0x06001435 RID: 5173 RVA: 0x0004712A File Offset: 0x0004532A
		internal static string Cqt_NewInstance_CannotInstantiateAbstractType(object p0)
		{
			return EntityRes.GetString("Cqt_NewInstance_CannotInstantiateAbstractType", new object[]
			{
				p0
			});
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06001436 RID: 5174 RVA: 0x00047140 File Offset: 0x00045340
		internal static string Cqt_NewInstance_IncompatibleRelatedEntity_SourceTypeNotValid
		{
			get
			{
				return EntityRes.GetString("Cqt_NewInstance_IncompatibleRelatedEntity_SourceTypeNotValid");
			}
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06001437 RID: 5175 RVA: 0x0004714C File Offset: 0x0004534C
		internal static string Cqt_Not_BooleanArgumentRequired
		{
			get
			{
				return EntityRes.GetString("Cqt_Not_BooleanArgumentRequired");
			}
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06001438 RID: 5176 RVA: 0x00047158 File Offset: 0x00045358
		internal static string Cqt_Or_BooleanArgumentsRequired
		{
			get
			{
				return EntityRes.GetString("Cqt_Or_BooleanArgumentsRequired");
			}
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06001439 RID: 5177 RVA: 0x00047164 File Offset: 0x00045364
		internal static string Cqt_Property_InstanceRequiredForInstance
		{
			get
			{
				return EntityRes.GetString("Cqt_Property_InstanceRequiredForInstance");
			}
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x0600143A RID: 5178 RVA: 0x00047170 File Offset: 0x00045370
		internal static string Cqt_Ref_PolymorphicArgRequired
		{
			get
			{
				return EntityRes.GetString("Cqt_Ref_PolymorphicArgRequired");
			}
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x0600143B RID: 5179 RVA: 0x0004717C File Offset: 0x0004537C
		internal static string Cqt_RelatedEntityRef_TargetEndFromDifferentRelationship
		{
			get
			{
				return EntityRes.GetString("Cqt_RelatedEntityRef_TargetEndFromDifferentRelationship");
			}
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x0600143C RID: 5180 RVA: 0x00047188 File Offset: 0x00045388
		internal static string Cqt_RelatedEntityRef_TargetEndMustBeAtMostOne
		{
			get
			{
				return EntityRes.GetString("Cqt_RelatedEntityRef_TargetEndMustBeAtMostOne");
			}
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x0600143D RID: 5181 RVA: 0x00047194 File Offset: 0x00045394
		internal static string Cqt_RelatedEntityRef_TargetEndSameAsSourceEnd
		{
			get
			{
				return EntityRes.GetString("Cqt_RelatedEntityRef_TargetEndSameAsSourceEnd");
			}
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x0600143E RID: 5182 RVA: 0x000471A0 File Offset: 0x000453A0
		internal static string Cqt_RelatedEntityRef_TargetEntityNotRef
		{
			get
			{
				return EntityRes.GetString("Cqt_RelatedEntityRef_TargetEntityNotRef");
			}
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x0600143F RID: 5183 RVA: 0x000471AC File Offset: 0x000453AC
		internal static string Cqt_RelatedEntityRef_TargetEntityNotCompatible
		{
			get
			{
				return EntityRes.GetString("Cqt_RelatedEntityRef_TargetEntityNotCompatible");
			}
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06001440 RID: 5184 RVA: 0x000471B8 File Offset: 0x000453B8
		internal static string Cqt_RelNav_NoCompositions
		{
			get
			{
				return EntityRes.GetString("Cqt_RelNav_NoCompositions");
			}
		}

		// Token: 0x06001441 RID: 5185 RVA: 0x000471C4 File Offset: 0x000453C4
		internal static string Cqt_RelNav_WrongSourceType(object p0)
		{
			return EntityRes.GetString("Cqt_RelNav_WrongSourceType", new object[]
			{
				p0
			});
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06001442 RID: 5186 RVA: 0x000471DA File Offset: 0x000453DA
		internal static string Cqt_Skip_ConstantOrParameterRefRequired
		{
			get
			{
				return EntityRes.GetString("Cqt_Skip_ConstantOrParameterRefRequired");
			}
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06001443 RID: 5187 RVA: 0x000471E6 File Offset: 0x000453E6
		internal static string Cqt_Skip_IntegerRequired
		{
			get
			{
				return EntityRes.GetString("Cqt_Skip_IntegerRequired");
			}
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06001444 RID: 5188 RVA: 0x000471F2 File Offset: 0x000453F2
		internal static string Cqt_Skip_NonNegativeCountRequired
		{
			get
			{
				return EntityRes.GetString("Cqt_Skip_NonNegativeCountRequired");
			}
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06001445 RID: 5189 RVA: 0x000471FE File Offset: 0x000453FE
		internal static string Cqt_Sort_EmptyCollationInvalid
		{
			get
			{
				return EntityRes.GetString("Cqt_Sort_EmptyCollationInvalid");
			}
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06001446 RID: 5190 RVA: 0x0004720A File Offset: 0x0004540A
		internal static string Cqt_Sort_NonStringCollationInvalid
		{
			get
			{
				return EntityRes.GetString("Cqt_Sort_NonStringCollationInvalid");
			}
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06001447 RID: 5191 RVA: 0x00047216 File Offset: 0x00045416
		internal static string Cqt_Sort_OrderComparable
		{
			get
			{
				return EntityRes.GetString("Cqt_Sort_OrderComparable");
			}
		}

		// Token: 0x06001448 RID: 5192 RVA: 0x00047222 File Offset: 0x00045422
		internal static string Cqt_UDF_FunctionDefinitionGenerationFailed(object p0)
		{
			return EntityRes.GetString("Cqt_UDF_FunctionDefinitionGenerationFailed", new object[]
			{
				p0
			});
		}

		// Token: 0x06001449 RID: 5193 RVA: 0x00047238 File Offset: 0x00045438
		internal static string Cqt_UDF_FunctionDefinitionWithCircularReference(object p0)
		{
			return EntityRes.GetString("Cqt_UDF_FunctionDefinitionWithCircularReference", new object[]
			{
				p0
			});
		}

		// Token: 0x0600144A RID: 5194 RVA: 0x0004724E File Offset: 0x0004544E
		internal static string Cqt_UDF_FunctionDefinitionResultTypeMismatch(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Cqt_UDF_FunctionDefinitionResultTypeMismatch", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x0600144B RID: 5195 RVA: 0x0004726C File Offset: 0x0004546C
		internal static string Cqt_UDF_FunctionHasNoDefinition(object p0)
		{
			return EntityRes.GetString("Cqt_UDF_FunctionHasNoDefinition", new object[]
			{
				p0
			});
		}

		// Token: 0x0600144C RID: 5196 RVA: 0x00047282 File Offset: 0x00045482
		internal static string Cqt_Validator_VarRefInvalid(object p0)
		{
			return EntityRes.GetString("Cqt_Validator_VarRefInvalid", new object[]
			{
				p0
			});
		}

		// Token: 0x0600144D RID: 5197 RVA: 0x00047298 File Offset: 0x00045498
		internal static string Cqt_Validator_VarRefTypeMismatch(object p0)
		{
			return EntityRes.GetString("Cqt_Validator_VarRefTypeMismatch", new object[]
			{
				p0
			});
		}

		// Token: 0x0600144E RID: 5198 RVA: 0x000472AE File Offset: 0x000454AE
		internal static string Iqt_General_UnsupportedOp(object p0)
		{
			return EntityRes.GetString("Iqt_General_UnsupportedOp", new object[]
			{
				p0
			});
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x0600144F RID: 5199 RVA: 0x000472C4 File Offset: 0x000454C4
		internal static string Iqt_CTGen_UnexpectedAggregate
		{
			get
			{
				return EntityRes.GetString("Iqt_CTGen_UnexpectedAggregate");
			}
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06001450 RID: 5200 RVA: 0x000472D0 File Offset: 0x000454D0
		internal static string Iqt_CTGen_UnexpectedVarDefList
		{
			get
			{
				return EntityRes.GetString("Iqt_CTGen_UnexpectedVarDefList");
			}
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06001451 RID: 5201 RVA: 0x000472DC File Offset: 0x000454DC
		internal static string Iqt_CTGen_UnexpectedVarDef
		{
			get
			{
				return EntityRes.GetString("Iqt_CTGen_UnexpectedVarDef");
			}
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06001452 RID: 5202 RVA: 0x000472E8 File Offset: 0x000454E8
		internal static string ADP_MustUseSequentialAccess
		{
			get
			{
				return EntityRes.GetString("ADP_MustUseSequentialAccess");
			}
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06001453 RID: 5203 RVA: 0x000472F4 File Offset: 0x000454F4
		internal static string ADP_ProviderDoesNotSupportCommandTrees
		{
			get
			{
				return EntityRes.GetString("ADP_ProviderDoesNotSupportCommandTrees");
			}
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x06001454 RID: 5204 RVA: 0x00047300 File Offset: 0x00045500
		internal static string ADP_ClosedDataReaderError
		{
			get
			{
				return EntityRes.GetString("ADP_ClosedDataReaderError");
			}
		}

		// Token: 0x06001455 RID: 5205 RVA: 0x0004730C File Offset: 0x0004550C
		internal static string ADP_DataReaderClosed(object p0)
		{
			return EntityRes.GetString("ADP_DataReaderClosed", new object[]
			{
				p0
			});
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06001456 RID: 5206 RVA: 0x00047322 File Offset: 0x00045522
		internal static string ADP_ImplicitlyClosedDataReaderError
		{
			get
			{
				return EntityRes.GetString("ADP_ImplicitlyClosedDataReaderError");
			}
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x06001457 RID: 5207 RVA: 0x0004732E File Offset: 0x0004552E
		internal static string ADP_NoData
		{
			get
			{
				return EntityRes.GetString("ADP_NoData");
			}
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06001458 RID: 5208 RVA: 0x0004733A File Offset: 0x0004553A
		internal static string ADP_GetSchemaTableIsNotSupported
		{
			get
			{
				return EntityRes.GetString("ADP_GetSchemaTableIsNotSupported");
			}
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06001459 RID: 5209 RVA: 0x00047346 File Offset: 0x00045546
		internal static string ADP_InvalidDataReaderFieldCountForScalarType
		{
			get
			{
				return EntityRes.GetString("ADP_InvalidDataReaderFieldCountForScalarType");
			}
		}

		// Token: 0x0600145A RID: 5210 RVA: 0x00047352 File Offset: 0x00045552
		internal static string ADP_InvalidDataReaderMissingColumnForType(object p0, object p1)
		{
			return EntityRes.GetString("ADP_InvalidDataReaderMissingColumnForType", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x0600145B RID: 5211 RVA: 0x0004736C File Offset: 0x0004556C
		internal static string ADP_InvalidDataReaderMissingDiscriminatorColumn(object p0, object p1)
		{
			return EntityRes.GetString("ADP_InvalidDataReaderMissingDiscriminatorColumn", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x0600145C RID: 5212 RVA: 0x00047386 File Offset: 0x00045586
		internal static string ADP_InvalidDataReaderUnableToDetermineType
		{
			get
			{
				return EntityRes.GetString("ADP_InvalidDataReaderUnableToDetermineType");
			}
		}

		// Token: 0x0600145D RID: 5213 RVA: 0x00047392 File Offset: 0x00045592
		internal static string ADP_InvalidDataReaderUnableToMaterializeNonScalarType(object p0, object p1)
		{
			return EntityRes.GetString("ADP_InvalidDataReaderUnableToMaterializeNonScalarType", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x0600145E RID: 5214 RVA: 0x000473AC File Offset: 0x000455AC
		internal static string ADP_KeysRequiredForJoinOverNest(object p0)
		{
			return EntityRes.GetString("ADP_KeysRequiredForJoinOverNest", new object[]
			{
				p0
			});
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x0600145F RID: 5215 RVA: 0x000473C2 File Offset: 0x000455C2
		internal static string ADP_KeysRequiredForNesting
		{
			get
			{
				return EntityRes.GetString("ADP_KeysRequiredForNesting");
			}
		}

		// Token: 0x06001460 RID: 5216 RVA: 0x000473CE File Offset: 0x000455CE
		internal static string ADP_NestingNotSupported(object p0, object p1)
		{
			return EntityRes.GetString("ADP_NestingNotSupported", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06001461 RID: 5217 RVA: 0x000473E8 File Offset: 0x000455E8
		internal static string ADP_NoQueryMappingView(object p0, object p1)
		{
			return EntityRes.GetString("ADP_NoQueryMappingView", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06001462 RID: 5218 RVA: 0x00047402 File Offset: 0x00045602
		internal static string ADP_InternalProviderError(object p0)
		{
			return EntityRes.GetString("ADP_InternalProviderError", new object[]
			{
				p0
			});
		}

		// Token: 0x06001463 RID: 5219 RVA: 0x00047418 File Offset: 0x00045618
		internal static string ADP_InvalidEnumerationValue(object p0, object p1)
		{
			return EntityRes.GetString("ADP_InvalidEnumerationValue", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06001464 RID: 5220 RVA: 0x00047432 File Offset: 0x00045632
		internal static string ADP_InvalidBufferSizeOrIndex(object p0, object p1)
		{
			return EntityRes.GetString("ADP_InvalidBufferSizeOrIndex", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06001465 RID: 5221 RVA: 0x0004744C File Offset: 0x0004564C
		internal static string ADP_InvalidDataLength(object p0)
		{
			return EntityRes.GetString("ADP_InvalidDataLength", new object[]
			{
				p0
			});
		}

		// Token: 0x06001466 RID: 5222 RVA: 0x00047462 File Offset: 0x00045662
		internal static string ADP_InvalidDataType(object p0)
		{
			return EntityRes.GetString("ADP_InvalidDataType", new object[]
			{
				p0
			});
		}

		// Token: 0x06001467 RID: 5223 RVA: 0x00047478 File Offset: 0x00045678
		internal static string ADP_InvalidDestinationBufferIndex(object p0, object p1)
		{
			return EntityRes.GetString("ADP_InvalidDestinationBufferIndex", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06001468 RID: 5224 RVA: 0x00047492 File Offset: 0x00045692
		internal static string ADP_InvalidSourceBufferIndex(object p0, object p1)
		{
			return EntityRes.GetString("ADP_InvalidSourceBufferIndex", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06001469 RID: 5225 RVA: 0x000474AC File Offset: 0x000456AC
		internal static string ADP_NonSequentialChunkAccess(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ADP_NonSequentialChunkAccess", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x0600146A RID: 5226 RVA: 0x000474CA File Offset: 0x000456CA
		internal static string ADP_NonSequentialColumnAccess(object p0, object p1)
		{
			return EntityRes.GetString("ADP_NonSequentialColumnAccess", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x0600146B RID: 5227 RVA: 0x000474E4 File Offset: 0x000456E4
		internal static string ADP_UnknownDataTypeCode(object p0, object p1)
		{
			return EntityRes.GetString("ADP_UnknownDataTypeCode", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x0600146C RID: 5228 RVA: 0x000474FE File Offset: 0x000456FE
		internal static string DataCategory_Data
		{
			get
			{
				return EntityRes.GetString("DataCategory_Data");
			}
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x0600146D RID: 5229 RVA: 0x0004750A File Offset: 0x0004570A
		internal static string DbParameter_Direction
		{
			get
			{
				return EntityRes.GetString("DbParameter_Direction");
			}
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x0600146E RID: 5230 RVA: 0x00047516 File Offset: 0x00045716
		internal static string DbParameter_Size
		{
			get
			{
				return EntityRes.GetString("DbParameter_Size");
			}
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x0600146F RID: 5231 RVA: 0x00047522 File Offset: 0x00045722
		internal static string DataCategory_Update
		{
			get
			{
				return EntityRes.GetString("DataCategory_Update");
			}
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06001470 RID: 5232 RVA: 0x0004752E File Offset: 0x0004572E
		internal static string DbParameter_SourceColumn
		{
			get
			{
				return EntityRes.GetString("DbParameter_SourceColumn");
			}
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06001471 RID: 5233 RVA: 0x0004753A File Offset: 0x0004573A
		internal static string DbParameter_SourceVersion
		{
			get
			{
				return EntityRes.GetString("DbParameter_SourceVersion");
			}
		}

		// Token: 0x06001472 RID: 5234 RVA: 0x00047546 File Offset: 0x00045746
		internal static string ADP_CollectionParameterElementIsNull(object p0)
		{
			return EntityRes.GetString("ADP_CollectionParameterElementIsNull", new object[]
			{
				p0
			});
		}

		// Token: 0x06001473 RID: 5235 RVA: 0x0004755C File Offset: 0x0004575C
		internal static string ADP_CollectionParameterElementIsNullOrEmpty(object p0)
		{
			return EntityRes.GetString("ADP_CollectionParameterElementIsNullOrEmpty", new object[]
			{
				p0
			});
		}

		// Token: 0x06001474 RID: 5236 RVA: 0x00047572 File Offset: 0x00045772
		internal static string EntityParameterCollectionInvalidParameterName(object p0)
		{
			return EntityRes.GetString("EntityParameterCollectionInvalidParameterName", new object[]
			{
				p0
			});
		}

		// Token: 0x06001475 RID: 5237 RVA: 0x00047588 File Offset: 0x00045788
		internal static string EntityParameterCollectionInvalidIndex(object p0, object p1)
		{
			return EntityRes.GetString("EntityParameterCollectionInvalidIndex", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06001476 RID: 5238 RVA: 0x000475A2 File Offset: 0x000457A2
		internal static string InvalidEntityParameterType(object p0)
		{
			return EntityRes.GetString("InvalidEntityParameterType", new object[]
			{
				p0
			});
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06001477 RID: 5239 RVA: 0x000475B8 File Offset: 0x000457B8
		internal static string EntityParameterContainedByAnotherCollection
		{
			get
			{
				return EntityRes.GetString("EntityParameterContainedByAnotherCollection");
			}
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06001478 RID: 5240 RVA: 0x000475C4 File Offset: 0x000457C4
		internal static string EntityParameterNull
		{
			get
			{
				return EntityRes.GetString("EntityParameterNull");
			}
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06001479 RID: 5241 RVA: 0x000475D0 File Offset: 0x000457D0
		internal static string EntityParameterCollectionRemoveInvalidObject
		{
			get
			{
				return EntityRes.GetString("EntityParameterCollectionRemoveInvalidObject");
			}
		}

		// Token: 0x0600147A RID: 5242 RVA: 0x000475DC File Offset: 0x000457DC
		internal static string ADP_ConnectionStringSyntax(object p0)
		{
			return EntityRes.GetString("ADP_ConnectionStringSyntax", new object[]
			{
				p0
			});
		}

		// Token: 0x0600147B RID: 5243 RVA: 0x000475F2 File Offset: 0x000457F2
		internal static string ADP_InvalidConnectionOptionValue(object p0)
		{
			return EntityRes.GetString("ADP_InvalidConnectionOptionValue", new object[]
			{
				p0
			});
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x0600147C RID: 5244 RVA: 0x00047608 File Offset: 0x00045808
		internal static string ADP_InvalidDataDirectory
		{
			get
			{
				return EntityRes.GetString("ADP_InvalidDataDirectory");
			}
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x0600147D RID: 5245 RVA: 0x00047614 File Offset: 0x00045814
		internal static string ADP_InvalidMultipartNameDelimiterUsage
		{
			get
			{
				return EntityRes.GetString("ADP_InvalidMultipartNameDelimiterUsage");
			}
		}

		// Token: 0x0600147E RID: 5246 RVA: 0x00047620 File Offset: 0x00045820
		internal static string ADP_InvalidSizeValue(object p0)
		{
			return EntityRes.GetString("ADP_InvalidSizeValue", new object[]
			{
				p0
			});
		}

		// Token: 0x0600147F RID: 5247 RVA: 0x00047636 File Offset: 0x00045836
		internal static string ADP_KeywordNotSupported(object p0)
		{
			return EntityRes.GetString("ADP_KeywordNotSupported", new object[]
			{
				p0
			});
		}

		// Token: 0x06001480 RID: 5248 RVA: 0x0004764C File Offset: 0x0004584C
		internal static string ConstantFacetSpecifiedInSchema(object p0, object p1)
		{
			return EntityRes.GetString("ConstantFacetSpecifiedInSchema", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06001481 RID: 5249 RVA: 0x00047666 File Offset: 0x00045866
		internal static string DuplicateAnnotation(object p0, object p1)
		{
			return EntityRes.GetString("DuplicateAnnotation", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06001482 RID: 5250 RVA: 0x00047680 File Offset: 0x00045880
		internal static string EmptyFile(object p0)
		{
			return EntityRes.GetString("EmptyFile", new object[]
			{
				p0
			});
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06001483 RID: 5251 RVA: 0x00047696 File Offset: 0x00045896
		internal static string EmptySchemaTextReader
		{
			get
			{
				return EntityRes.GetString("EmptySchemaTextReader");
			}
		}

		// Token: 0x06001484 RID: 5252 RVA: 0x000476A2 File Offset: 0x000458A2
		internal static string EmptyName(object p0)
		{
			return EntityRes.GetString("EmptyName", new object[]
			{
				p0
			});
		}

		// Token: 0x06001485 RID: 5253 RVA: 0x000476B8 File Offset: 0x000458B8
		internal static string InvalidName(object p0, object p1)
		{
			return EntityRes.GetString("InvalidName", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06001486 RID: 5254 RVA: 0x000476D2 File Offset: 0x000458D2
		internal static string MissingName
		{
			get
			{
				return EntityRes.GetString("MissingName");
			}
		}

		// Token: 0x06001487 RID: 5255 RVA: 0x000476DE File Offset: 0x000458DE
		internal static string UnexpectedXmlAttribute(object p0)
		{
			return EntityRes.GetString("UnexpectedXmlAttribute", new object[]
			{
				p0
			});
		}

		// Token: 0x06001488 RID: 5256 RVA: 0x000476F4 File Offset: 0x000458F4
		internal static string UnexpectedXmlElement(object p0)
		{
			return EntityRes.GetString("UnexpectedXmlElement", new object[]
			{
				p0
			});
		}

		// Token: 0x06001489 RID: 5257 RVA: 0x0004770A File Offset: 0x0004590A
		internal static string TextNotAllowed(object p0)
		{
			return EntityRes.GetString("TextNotAllowed", new object[]
			{
				p0
			});
		}

		// Token: 0x0600148A RID: 5258 RVA: 0x00047720 File Offset: 0x00045920
		internal static string UnexpectedXmlNodeType(object p0)
		{
			return EntityRes.GetString("UnexpectedXmlNodeType", new object[]
			{
				p0
			});
		}

		// Token: 0x0600148B RID: 5259 RVA: 0x00047736 File Offset: 0x00045936
		internal static string MalformedXml(object p0, object p1)
		{
			return EntityRes.GetString("MalformedXml", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x0600148C RID: 5260 RVA: 0x00047750 File Offset: 0x00045950
		internal static string ValueNotUnderstood(object p0, object p1)
		{
			return EntityRes.GetString("ValueNotUnderstood", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x0600148D RID: 5261 RVA: 0x0004776A File Offset: 0x0004596A
		internal static string EntityContainerAlreadyExists(object p0)
		{
			return EntityRes.GetString("EntityContainerAlreadyExists", new object[]
			{
				p0
			});
		}

		// Token: 0x0600148E RID: 5262 RVA: 0x00047780 File Offset: 0x00045980
		internal static string TypeNameAlreadyDefinedDuplicate(object p0)
		{
			return EntityRes.GetString("TypeNameAlreadyDefinedDuplicate", new object[]
			{
				p0
			});
		}

		// Token: 0x0600148F RID: 5263 RVA: 0x00047796 File Offset: 0x00045996
		internal static string PropertyNameAlreadyDefinedDuplicate(object p0)
		{
			return EntityRes.GetString("PropertyNameAlreadyDefinedDuplicate", new object[]
			{
				p0
			});
		}

		// Token: 0x06001490 RID: 5264 RVA: 0x000477AC File Offset: 0x000459AC
		internal static string DuplicateMemberNameInExtendedEntityContainer(object p0, object p1, object p2)
		{
			return EntityRes.GetString("DuplicateMemberNameInExtendedEntityContainer", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06001491 RID: 5265 RVA: 0x000477CA File Offset: 0x000459CA
		internal static string DuplicateEntityContainerMemberName(object p0)
		{
			return EntityRes.GetString("DuplicateEntityContainerMemberName", new object[]
			{
				p0
			});
		}

		// Token: 0x06001492 RID: 5266 RVA: 0x000477E0 File Offset: 0x000459E0
		internal static string PropertyTypeAlreadyDefined(object p0)
		{
			return EntityRes.GetString("PropertyTypeAlreadyDefined", new object[]
			{
				p0
			});
		}

		// Token: 0x06001493 RID: 5267 RVA: 0x000477F6 File Offset: 0x000459F6
		internal static string InvalidSize(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("InvalidSize", new object[]
			{
				p0,
				p1,
				p2,
				p3
			});
		}

		// Token: 0x06001494 RID: 5268 RVA: 0x00047818 File Offset: 0x00045A18
		internal static string InvalidSystemReferenceId(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("InvalidSystemReferenceId", new object[]
			{
				p0,
				p1,
				p2,
				p3
			});
		}

		// Token: 0x06001495 RID: 5269 RVA: 0x0004783A File Offset: 0x00045A3A
		internal static string BadNamespaceOrAlias(object p0)
		{
			return EntityRes.GetString("BadNamespaceOrAlias", new object[]
			{
				p0
			});
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06001496 RID: 5270 RVA: 0x00047850 File Offset: 0x00045A50
		internal static string MissingNamespaceAttribute
		{
			get
			{
				return EntityRes.GetString("MissingNamespaceAttribute");
			}
		}

		// Token: 0x06001497 RID: 5271 RVA: 0x0004785C File Offset: 0x00045A5C
		internal static string InvalidBaseTypeForStructuredType(object p0, object p1)
		{
			return EntityRes.GetString("InvalidBaseTypeForStructuredType", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06001498 RID: 5272 RVA: 0x00047876 File Offset: 0x00045A76
		internal static string InvalidPropertyType(object p0)
		{
			return EntityRes.GetString("InvalidPropertyType", new object[]
			{
				p0
			});
		}

		// Token: 0x06001499 RID: 5273 RVA: 0x0004788C File Offset: 0x00045A8C
		internal static string InvalidBaseTypeForItemType(object p0, object p1)
		{
			return EntityRes.GetString("InvalidBaseTypeForItemType", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x0600149A RID: 5274 RVA: 0x000478A6 File Offset: 0x00045AA6
		internal static string InvalidBaseTypeForNestedType(object p0, object p1)
		{
			return EntityRes.GetString("InvalidBaseTypeForNestedType", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x0600149B RID: 5275 RVA: 0x000478C0 File Offset: 0x00045AC0
		internal static string DefaultNotAllowed
		{
			get
			{
				return EntityRes.GetString("DefaultNotAllowed");
			}
		}

		// Token: 0x0600149C RID: 5276 RVA: 0x000478CC File Offset: 0x00045ACC
		internal static string FacetNotAllowed(object p0, object p1)
		{
			return EntityRes.GetString("FacetNotAllowed", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x0600149D RID: 5277 RVA: 0x000478E6 File Offset: 0x00045AE6
		internal static string RequiredFacetMissing(object p0, object p1)
		{
			return EntityRes.GetString("RequiredFacetMissing", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x0600149E RID: 5278 RVA: 0x00047900 File Offset: 0x00045B00
		internal static string InvalidDefaultBinaryWithNoMaxLength(object p0)
		{
			return EntityRes.GetString("InvalidDefaultBinaryWithNoMaxLength", new object[]
			{
				p0
			});
		}

		// Token: 0x0600149F RID: 5279 RVA: 0x00047916 File Offset: 0x00045B16
		internal static string InvalidDefaultIntegral(object p0, object p1, object p2)
		{
			return EntityRes.GetString("InvalidDefaultIntegral", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x060014A0 RID: 5280 RVA: 0x00047934 File Offset: 0x00045B34
		internal static string InvalidDefaultDateTime(object p0, object p1)
		{
			return EntityRes.GetString("InvalidDefaultDateTime", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060014A1 RID: 5281 RVA: 0x0004794E File Offset: 0x00045B4E
		internal static string InvalidDefaultTime(object p0, object p1)
		{
			return EntityRes.GetString("InvalidDefaultTime", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060014A2 RID: 5282 RVA: 0x00047968 File Offset: 0x00045B68
		internal static string InvalidDefaultDateTimeOffset(object p0, object p1)
		{
			return EntityRes.GetString("InvalidDefaultDateTimeOffset", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060014A3 RID: 5283 RVA: 0x00047982 File Offset: 0x00045B82
		internal static string InvalidDefaultDecimal(object p0, object p1, object p2)
		{
			return EntityRes.GetString("InvalidDefaultDecimal", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x060014A4 RID: 5284 RVA: 0x000479A0 File Offset: 0x00045BA0
		internal static string InvalidDefaultFloatingPoint(object p0, object p1, object p2)
		{
			return EntityRes.GetString("InvalidDefaultFloatingPoint", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x060014A5 RID: 5285 RVA: 0x000479BE File Offset: 0x00045BBE
		internal static string InvalidDefaultGuid(object p0)
		{
			return EntityRes.GetString("InvalidDefaultGuid", new object[]
			{
				p0
			});
		}

		// Token: 0x060014A6 RID: 5286 RVA: 0x000479D4 File Offset: 0x00045BD4
		internal static string InvalidDefaultBoolean(object p0)
		{
			return EntityRes.GetString("InvalidDefaultBoolean", new object[]
			{
				p0
			});
		}

		// Token: 0x060014A7 RID: 5287 RVA: 0x000479EA File Offset: 0x00045BEA
		internal static string DuplicateMemberName(object p0, object p1, object p2)
		{
			return EntityRes.GetString("DuplicateMemberName", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x060014A8 RID: 5288 RVA: 0x00047A08 File Offset: 0x00045C08
		internal static string GeneratorErrorSeverityError
		{
			get
			{
				return EntityRes.GetString("GeneratorErrorSeverityError");
			}
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x060014A9 RID: 5289 RVA: 0x00047A14 File Offset: 0x00045C14
		internal static string GeneratorErrorSeverityWarning
		{
			get
			{
				return EntityRes.GetString("GeneratorErrorSeverityWarning");
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x060014AA RID: 5290 RVA: 0x00047A20 File Offset: 0x00045C20
		internal static string GeneratorErrorSeverityUnknown
		{
			get
			{
				return EntityRes.GetString("GeneratorErrorSeverityUnknown");
			}
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x060014AB RID: 5291 RVA: 0x00047A2C File Offset: 0x00045C2C
		internal static string SourceUriUnknown
		{
			get
			{
				return EntityRes.GetString("SourceUriUnknown");
			}
		}

		// Token: 0x060014AC RID: 5292 RVA: 0x00047A38 File Offset: 0x00045C38
		internal static string BadPrecisionAndScale(object p0, object p1)
		{
			return EntityRes.GetString("BadPrecisionAndScale", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060014AD RID: 5293 RVA: 0x00047A52 File Offset: 0x00045C52
		internal static string InvalidNamespaceInUsing(object p0)
		{
			return EntityRes.GetString("InvalidNamespaceInUsing", new object[]
			{
				p0
			});
		}

		// Token: 0x060014AE RID: 5294 RVA: 0x00047A68 File Offset: 0x00045C68
		internal static string BadNavigationPropertyRelationshipNotRelationship(object p0)
		{
			return EntityRes.GetString("BadNavigationPropertyRelationshipNotRelationship", new object[]
			{
				p0
			});
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x060014AF RID: 5295 RVA: 0x00047A7E File Offset: 0x00045C7E
		internal static string BadNavigationPropertyRolesCannotBeTheSame
		{
			get
			{
				return EntityRes.GetString("BadNavigationPropertyRolesCannotBeTheSame");
			}
		}

		// Token: 0x060014B0 RID: 5296 RVA: 0x00047A8A File Offset: 0x00045C8A
		internal static string BadNavigationPropertyUndefinedRole(object p0, object p1)
		{
			return EntityRes.GetString("BadNavigationPropertyUndefinedRole", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060014B1 RID: 5297 RVA: 0x00047AA4 File Offset: 0x00045CA4
		internal static string BadNavigationPropertyBadFromRoleType(object p0, object p1, object p2, object p3, object p4)
		{
			return EntityRes.GetString("BadNavigationPropertyBadFromRoleType", new object[]
			{
				p0,
				p1,
				p2,
				p3,
				p4
			});
		}

		// Token: 0x060014B2 RID: 5298 RVA: 0x00047ACB File Offset: 0x00045CCB
		internal static string InvalidMemberNameMatchesTypeName(object p0, object p1)
		{
			return EntityRes.GetString("InvalidMemberNameMatchesTypeName", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060014B3 RID: 5299 RVA: 0x00047AE5 File Offset: 0x00045CE5
		internal static string InvalidKeyKeyDefinedInBaseClass(object p0, object p1)
		{
			return EntityRes.GetString("InvalidKeyKeyDefinedInBaseClass", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060014B4 RID: 5300 RVA: 0x00047AFF File Offset: 0x00045CFF
		internal static string InvalidKeyNullablePart(object p0, object p1)
		{
			return EntityRes.GetString("InvalidKeyNullablePart", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060014B5 RID: 5301 RVA: 0x00047B19 File Offset: 0x00045D19
		internal static string InvalidKeyNoProperty(object p0, object p1)
		{
			return EntityRes.GetString("InvalidKeyNoProperty", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060014B6 RID: 5302 RVA: 0x00047B33 File Offset: 0x00045D33
		internal static string KeyMissingOnEntityType(object p0)
		{
			return EntityRes.GetString("KeyMissingOnEntityType", new object[]
			{
				p0
			});
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x060014B7 RID: 5303 RVA: 0x00047B49 File Offset: 0x00045D49
		internal static string InvalidDocumentationBothTextAndStructure
		{
			get
			{
				return EntityRes.GetString("InvalidDocumentationBothTextAndStructure");
			}
		}

		// Token: 0x060014B8 RID: 5304 RVA: 0x00047B55 File Offset: 0x00045D55
		internal static string ArgumentOutOfRangeExpectedPostiveNumber(object p0)
		{
			return EntityRes.GetString("ArgumentOutOfRangeExpectedPostiveNumber", new object[]
			{
				p0
			});
		}

		// Token: 0x060014B9 RID: 5305 RVA: 0x00047B6B File Offset: 0x00045D6B
		internal static string ArgumentOutOfRange(object p0)
		{
			return EntityRes.GetString("ArgumentOutOfRange", new object[]
			{
				p0
			});
		}

		// Token: 0x060014BA RID: 5306 RVA: 0x00047B81 File Offset: 0x00045D81
		internal static string UnacceptableUri(object p0)
		{
			return EntityRes.GetString("UnacceptableUri", new object[]
			{
				p0
			});
		}

		// Token: 0x060014BB RID: 5307 RVA: 0x00047B97 File Offset: 0x00045D97
		internal static string UnexpectedTypeInCollection(object p0, object p1)
		{
			return EntityRes.GetString("UnexpectedTypeInCollection", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x060014BC RID: 5308 RVA: 0x00047BB1 File Offset: 0x00045DB1
		internal static string AllElementsMustBeInSchema
		{
			get
			{
				return EntityRes.GetString("AllElementsMustBeInSchema");
			}
		}

		// Token: 0x060014BD RID: 5309 RVA: 0x00047BBD File Offset: 0x00045DBD
		internal static string AliasNameIsAlreadyDefined(object p0)
		{
			return EntityRes.GetString("AliasNameIsAlreadyDefined", new object[]
			{
				p0
			});
		}

		// Token: 0x060014BE RID: 5310 RVA: 0x00047BD3 File Offset: 0x00045DD3
		internal static string NeedNotUseSystemNamespaceInUsing(object p0)
		{
			return EntityRes.GetString("NeedNotUseSystemNamespaceInUsing", new object[]
			{
				p0
			});
		}

		// Token: 0x060014BF RID: 5311 RVA: 0x00047BE9 File Offset: 0x00045DE9
		internal static string CannotUseSystemNamespaceAsAlias(object p0)
		{
			return EntityRes.GetString("CannotUseSystemNamespaceAsAlias", new object[]
			{
				p0
			});
		}

		// Token: 0x060014C0 RID: 5312 RVA: 0x00047BFF File Offset: 0x00045DFF
		internal static string EntitySetTypeHasNoKeys(object p0, object p1)
		{
			return EntityRes.GetString("EntitySetTypeHasNoKeys", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060014C1 RID: 5313 RVA: 0x00047C19 File Offset: 0x00045E19
		internal static string TableAndSchemaAreMutuallyExclusiveWithDefiningQuery(object p0)
		{
			return EntityRes.GetString("TableAndSchemaAreMutuallyExclusiveWithDefiningQuery", new object[]
			{
				p0
			});
		}

		// Token: 0x060014C2 RID: 5314 RVA: 0x00047C2F File Offset: 0x00045E2F
		internal static string UnexpectedRootElement(object p0, object p1, object p2)
		{
			return EntityRes.GetString("UnexpectedRootElement", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x060014C3 RID: 5315 RVA: 0x00047C4D File Offset: 0x00045E4D
		internal static string UnexpectedRootElementNoNamespace(object p0, object p1, object p2)
		{
			return EntityRes.GetString("UnexpectedRootElementNoNamespace", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x060014C4 RID: 5316 RVA: 0x00047C6B File Offset: 0x00045E6B
		internal static string ParameterNameAlreadyDefinedDuplicate(object p0)
		{
			return EntityRes.GetString("ParameterNameAlreadyDefinedDuplicate", new object[]
			{
				p0
			});
		}

		// Token: 0x060014C5 RID: 5317 RVA: 0x00047C81 File Offset: 0x00045E81
		internal static string FunctionWithNonPrimitiveTypeNotSupported(object p0, object p1)
		{
			return EntityRes.GetString("FunctionWithNonPrimitiveTypeNotSupported", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060014C6 RID: 5318 RVA: 0x00047C9B File Offset: 0x00045E9B
		internal static string FunctionWithNonEdmPrimitiveTypeNotSupported(object p0, object p1)
		{
			return EntityRes.GetString("FunctionWithNonEdmPrimitiveTypeNotSupported", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060014C7 RID: 5319 RVA: 0x00047CB5 File Offset: 0x00045EB5
		internal static string FunctionImportWithUnsupportedReturnTypeV1(object p0)
		{
			return EntityRes.GetString("FunctionImportWithUnsupportedReturnTypeV1", new object[]
			{
				p0
			});
		}

		// Token: 0x060014C8 RID: 5320 RVA: 0x00047CCB File Offset: 0x00045ECB
		internal static string FunctionImportWithUnsupportedReturnTypeV1_1(object p0)
		{
			return EntityRes.GetString("FunctionImportWithUnsupportedReturnTypeV1_1", new object[]
			{
				p0
			});
		}

		// Token: 0x060014C9 RID: 5321 RVA: 0x00047CE1 File Offset: 0x00045EE1
		internal static string FunctionImportWithUnsupportedReturnTypeV2(object p0)
		{
			return EntityRes.GetString("FunctionImportWithUnsupportedReturnTypeV2", new object[]
			{
				p0
			});
		}

		// Token: 0x060014CA RID: 5322 RVA: 0x00047CF7 File Offset: 0x00045EF7
		internal static string FunctionImportUnknownEntitySet(object p0, object p1)
		{
			return EntityRes.GetString("FunctionImportUnknownEntitySet", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060014CB RID: 5323 RVA: 0x00047D11 File Offset: 0x00045F11
		internal static string FunctionImportReturnEntitiesButDoesNotSpecifyEntitySet(object p0)
		{
			return EntityRes.GetString("FunctionImportReturnEntitiesButDoesNotSpecifyEntitySet", new object[]
			{
				p0
			});
		}

		// Token: 0x060014CC RID: 5324 RVA: 0x00047D27 File Offset: 0x00045F27
		internal static string FunctionImportEntityTypeDoesNotMatchEntitySet(object p0, object p1, object p2)
		{
			return EntityRes.GetString("FunctionImportEntityTypeDoesNotMatchEntitySet", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x060014CD RID: 5325 RVA: 0x00047D45 File Offset: 0x00045F45
		internal static string FunctionImportSpecifiesEntitySetButNotEntityType(object p0)
		{
			return EntityRes.GetString("FunctionImportSpecifiesEntitySetButNotEntityType", new object[]
			{
				p0
			});
		}

		// Token: 0x060014CE RID: 5326 RVA: 0x00047D5B File Offset: 0x00045F5B
		internal static string FunctionImportEntitySetAndEntitySetPathDeclared(object p0)
		{
			return EntityRes.GetString("FunctionImportEntitySetAndEntitySetPathDeclared", new object[]
			{
				p0
			});
		}

		// Token: 0x060014CF RID: 5327 RVA: 0x00047D71 File Offset: 0x00045F71
		internal static string FunctionImportComposableAndSideEffectingNotAllowed(object p0)
		{
			return EntityRes.GetString("FunctionImportComposableAndSideEffectingNotAllowed", new object[]
			{
				p0
			});
		}

		// Token: 0x060014D0 RID: 5328 RVA: 0x00047D87 File Offset: 0x00045F87
		internal static string FunctionImportCollectionAndRefParametersNotAllowed(object p0)
		{
			return EntityRes.GetString("FunctionImportCollectionAndRefParametersNotAllowed", new object[]
			{
				p0
			});
		}

		// Token: 0x060014D1 RID: 5329 RVA: 0x00047D9D File Offset: 0x00045F9D
		internal static string FunctionImportNonNullableParametersNotAllowed(object p0)
		{
			return EntityRes.GetString("FunctionImportNonNullableParametersNotAllowed", new object[]
			{
				p0
			});
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x060014D2 RID: 5330 RVA: 0x00047DB3 File Offset: 0x00045FB3
		internal static string TVFReturnTypeRowHasNonScalarProperty
		{
			get
			{
				return EntityRes.GetString("TVFReturnTypeRowHasNonScalarProperty");
			}
		}

		// Token: 0x060014D3 RID: 5331 RVA: 0x00047DBF File Offset: 0x00045FBF
		internal static string DuplicateEntitySetTable(object p0, object p1, object p2)
		{
			return EntityRes.GetString("DuplicateEntitySetTable", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x060014D4 RID: 5332 RVA: 0x00047DDD File Offset: 0x00045FDD
		internal static string ConcurrencyRedefinedOnSubTypeOfEntitySetType(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ConcurrencyRedefinedOnSubTypeOfEntitySetType", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x060014D5 RID: 5333 RVA: 0x00047DFB File Offset: 0x00045FFB
		internal static string SimilarRelationshipEnd(object p0, object p1, object p2, object p3, object p4)
		{
			return EntityRes.GetString("SimilarRelationshipEnd", new object[]
			{
				p0,
				p1,
				p2,
				p3,
				p4
			});
		}

		// Token: 0x060014D6 RID: 5334 RVA: 0x00047E22 File Offset: 0x00046022
		internal static string InvalidRelationshipEndMultiplicity(object p0, object p1)
		{
			return EntityRes.GetString("InvalidRelationshipEndMultiplicity", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060014D7 RID: 5335 RVA: 0x00047E3C File Offset: 0x0004603C
		internal static string EndNameAlreadyDefinedDuplicate(object p0)
		{
			return EntityRes.GetString("EndNameAlreadyDefinedDuplicate", new object[]
			{
				p0
			});
		}

		// Token: 0x060014D8 RID: 5336 RVA: 0x00047E52 File Offset: 0x00046052
		internal static string InvalidRelationshipEndType(object p0, object p1)
		{
			return EntityRes.GetString("InvalidRelationshipEndType", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060014D9 RID: 5337 RVA: 0x00047E6C File Offset: 0x0004606C
		internal static string BadParameterDirection(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("BadParameterDirection", new object[]
			{
				p0,
				p1,
				p2,
				p3
			});
		}

		// Token: 0x060014DA RID: 5338 RVA: 0x00047E8E File Offset: 0x0004608E
		internal static string BadParameterDirectionForComposableFunctions(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("BadParameterDirectionForComposableFunctions", new object[]
			{
				p0,
				p1,
				p2,
				p3
			});
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x060014DB RID: 5339 RVA: 0x00047EB0 File Offset: 0x000460B0
		internal static string InvalidOperationMultipleEndsInAssociation
		{
			get
			{
				return EntityRes.GetString("InvalidOperationMultipleEndsInAssociation");
			}
		}

		// Token: 0x060014DC RID: 5340 RVA: 0x00047EBC File Offset: 0x000460BC
		internal static string InvalidAction(object p0, object p1)
		{
			return EntityRes.GetString("InvalidAction", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060014DD RID: 5341 RVA: 0x00047ED6 File Offset: 0x000460D6
		internal static string DuplicationOperation(object p0)
		{
			return EntityRes.GetString("DuplicationOperation", new object[]
			{
				p0
			});
		}

		// Token: 0x060014DE RID: 5342 RVA: 0x00047EEC File Offset: 0x000460EC
		internal static string NotInNamespaceAlias(object p0, object p1, object p2)
		{
			return EntityRes.GetString("NotInNamespaceAlias", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x060014DF RID: 5343 RVA: 0x00047F0A File Offset: 0x0004610A
		internal static string NotNamespaceQualified(object p0)
		{
			return EntityRes.GetString("NotNamespaceQualified", new object[]
			{
				p0
			});
		}

		// Token: 0x060014E0 RID: 5344 RVA: 0x00047F20 File Offset: 0x00046120
		internal static string NotInNamespaceNoAlias(object p0, object p1)
		{
			return EntityRes.GetString("NotInNamespaceNoAlias", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060014E1 RID: 5345 RVA: 0x00047F3A File Offset: 0x0004613A
		internal static string InvalidValueForParameterTypeSemanticsAttribute(object p0)
		{
			return EntityRes.GetString("InvalidValueForParameterTypeSemanticsAttribute", new object[]
			{
				p0
			});
		}

		// Token: 0x060014E2 RID: 5346 RVA: 0x00047F50 File Offset: 0x00046150
		internal static string DuplicatePropertyNameSpecifiedInEntityKey(object p0, object p1)
		{
			return EntityRes.GetString("DuplicatePropertyNameSpecifiedInEntityKey", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060014E3 RID: 5347 RVA: 0x00047F6A File Offset: 0x0004616A
		internal static string InvalidEntitySetType(object p0)
		{
			return EntityRes.GetString("InvalidEntitySetType", new object[]
			{
				p0
			});
		}

		// Token: 0x060014E4 RID: 5348 RVA: 0x00047F80 File Offset: 0x00046180
		internal static string InvalidRelationshipSetType(object p0)
		{
			return EntityRes.GetString("InvalidRelationshipSetType", new object[]
			{
				p0
			});
		}

		// Token: 0x060014E5 RID: 5349 RVA: 0x00047F96 File Offset: 0x00046196
		internal static string InvalidEntityContainerNameInExtends(object p0)
		{
			return EntityRes.GetString("InvalidEntityContainerNameInExtends", new object[]
			{
				p0
			});
		}

		// Token: 0x060014E6 RID: 5350 RVA: 0x00047FAC File Offset: 0x000461AC
		internal static string InvalidNamespaceOrAliasSpecified(object p0)
		{
			return EntityRes.GetString("InvalidNamespaceOrAliasSpecified", new object[]
			{
				p0
			});
		}

		// Token: 0x060014E7 RID: 5351 RVA: 0x00047FC2 File Offset: 0x000461C2
		internal static string PrecisionOutOfRange(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("PrecisionOutOfRange", new object[]
			{
				p0,
				p1,
				p2,
				p3
			});
		}

		// Token: 0x060014E8 RID: 5352 RVA: 0x00047FE4 File Offset: 0x000461E4
		internal static string ScaleOutOfRange(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("ScaleOutOfRange", new object[]
			{
				p0,
				p1,
				p2,
				p3
			});
		}

		// Token: 0x060014E9 RID: 5353 RVA: 0x00048006 File Offset: 0x00046206
		internal static string InvalidEntitySetNameReference(object p0, object p1)
		{
			return EntityRes.GetString("InvalidEntitySetNameReference", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060014EA RID: 5354 RVA: 0x00048020 File Offset: 0x00046220
		internal static string InvalidEntityEndName(object p0, object p1)
		{
			return EntityRes.GetString("InvalidEntityEndName", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060014EB RID: 5355 RVA: 0x0004803A File Offset: 0x0004623A
		internal static string DuplicateEndName(object p0)
		{
			return EntityRes.GetString("DuplicateEndName", new object[]
			{
				p0
			});
		}

		// Token: 0x060014EC RID: 5356 RVA: 0x00048050 File Offset: 0x00046250
		internal static string AmbiguousEntityContainerEnd(object p0, object p1)
		{
			return EntityRes.GetString("AmbiguousEntityContainerEnd", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060014ED RID: 5357 RVA: 0x0004806A File Offset: 0x0004626A
		internal static string MissingEntityContainerEnd(object p0, object p1)
		{
			return EntityRes.GetString("MissingEntityContainerEnd", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060014EE RID: 5358 RVA: 0x00048084 File Offset: 0x00046284
		internal static string InvalidEndEntitySetTypeMismatch(object p0)
		{
			return EntityRes.GetString("InvalidEndEntitySetTypeMismatch", new object[]
			{
				p0
			});
		}

		// Token: 0x060014EF RID: 5359 RVA: 0x0004809A File Offset: 0x0004629A
		internal static string InferRelationshipEndFailedNoEntitySetMatch(object p0, object p1, object p2, object p3, object p4)
		{
			return EntityRes.GetString("InferRelationshipEndFailedNoEntitySetMatch", new object[]
			{
				p0,
				p1,
				p2,
				p3,
				p4
			});
		}

		// Token: 0x060014F0 RID: 5360 RVA: 0x000480C1 File Offset: 0x000462C1
		internal static string InferRelationshipEndAmbiguous(object p0, object p1, object p2, object p3, object p4)
		{
			return EntityRes.GetString("InferRelationshipEndAmbiguous", new object[]
			{
				p0,
				p1,
				p2,
				p3,
				p4
			});
		}

		// Token: 0x060014F1 RID: 5361 RVA: 0x000480E8 File Offset: 0x000462E8
		internal static string InferRelationshipEndGivesAlreadyDefinedEnd(object p0, object p1)
		{
			return EntityRes.GetString("InferRelationshipEndGivesAlreadyDefinedEnd", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060014F2 RID: 5362 RVA: 0x00048102 File Offset: 0x00046302
		internal static string TooManyAssociationEnds(object p0)
		{
			return EntityRes.GetString("TooManyAssociationEnds", new object[]
			{
				p0
			});
		}

		// Token: 0x060014F3 RID: 5363 RVA: 0x00048118 File Offset: 0x00046318
		internal static string InvalidEndRoleInRelationshipConstraint(object p0, object p1)
		{
			return EntityRes.GetString("InvalidEndRoleInRelationshipConstraint", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060014F4 RID: 5364 RVA: 0x00048132 File Offset: 0x00046332
		internal static string InvalidFromPropertyInRelationshipConstraint(object p0, object p1, object p2)
		{
			return EntityRes.GetString("InvalidFromPropertyInRelationshipConstraint", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x060014F5 RID: 5365 RVA: 0x00048150 File Offset: 0x00046350
		internal static string InvalidToPropertyInRelationshipConstraint(object p0, object p1, object p2)
		{
			return EntityRes.GetString("InvalidToPropertyInRelationshipConstraint", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x060014F6 RID: 5366 RVA: 0x0004816E File Offset: 0x0004636E
		internal static string InvalidPropertyInRelationshipConstraint(object p0, object p1)
		{
			return EntityRes.GetString("InvalidPropertyInRelationshipConstraint", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060014F7 RID: 5367 RVA: 0x00048188 File Offset: 0x00046388
		internal static string TypeMismatchRelationshipConstaint(object p0, object p1, object p2, object p3, object p4)
		{
			return EntityRes.GetString("TypeMismatchRelationshipConstaint", new object[]
			{
				p0,
				p1,
				p2,
				p3,
				p4
			});
		}

		// Token: 0x060014F8 RID: 5368 RVA: 0x000481AF File Offset: 0x000463AF
		internal static string InvalidMultiplicityFromRoleUpperBoundMustBeOne(object p0, object p1)
		{
			return EntityRes.GetString("InvalidMultiplicityFromRoleUpperBoundMustBeOne", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060014F9 RID: 5369 RVA: 0x000481C9 File Offset: 0x000463C9
		internal static string InvalidMultiplicityFromRoleToPropertyNonNullableV1(object p0, object p1)
		{
			return EntityRes.GetString("InvalidMultiplicityFromRoleToPropertyNonNullableV1", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060014FA RID: 5370 RVA: 0x000481E3 File Offset: 0x000463E3
		internal static string InvalidMultiplicityFromRoleToPropertyNonNullableV2(object p0, object p1)
		{
			return EntityRes.GetString("InvalidMultiplicityFromRoleToPropertyNonNullableV2", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060014FB RID: 5371 RVA: 0x000481FD File Offset: 0x000463FD
		internal static string InvalidMultiplicityFromRoleToPropertyNullableV1(object p0, object p1)
		{
			return EntityRes.GetString("InvalidMultiplicityFromRoleToPropertyNullableV1", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060014FC RID: 5372 RVA: 0x00048217 File Offset: 0x00046417
		internal static string InvalidMultiplicityToRoleLowerBoundMustBeZero(object p0, object p1)
		{
			return EntityRes.GetString("InvalidMultiplicityToRoleLowerBoundMustBeZero", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060014FD RID: 5373 RVA: 0x00048231 File Offset: 0x00046431
		internal static string InvalidMultiplicityToRoleUpperBoundMustBeOne(object p0, object p1)
		{
			return EntityRes.GetString("InvalidMultiplicityToRoleUpperBoundMustBeOne", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060014FE RID: 5374 RVA: 0x0004824B File Offset: 0x0004644B
		internal static string InvalidMultiplicityToRoleUpperBoundMustBeMany(object p0, object p1)
		{
			return EntityRes.GetString("InvalidMultiplicityToRoleUpperBoundMustBeMany", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x060014FF RID: 5375 RVA: 0x00048265 File Offset: 0x00046465
		internal static string MismatchNumberOfPropertiesinRelationshipConstraint
		{
			get
			{
				return EntityRes.GetString("MismatchNumberOfPropertiesinRelationshipConstraint");
			}
		}

		// Token: 0x06001500 RID: 5376 RVA: 0x00048271 File Offset: 0x00046471
		internal static string MissingConstraintOnRelationshipType(object p0)
		{
			return EntityRes.GetString("MissingConstraintOnRelationshipType", new object[]
			{
				p0
			});
		}

		// Token: 0x06001501 RID: 5377 RVA: 0x00048287 File Offset: 0x00046487
		internal static string SameRoleReferredInReferentialConstraint(object p0)
		{
			return EntityRes.GetString("SameRoleReferredInReferentialConstraint", new object[]
			{
				p0
			});
		}

		// Token: 0x06001502 RID: 5378 RVA: 0x0004829D File Offset: 0x0004649D
		internal static string InvalidPrimitiveTypeKind(object p0)
		{
			return EntityRes.GetString("InvalidPrimitiveTypeKind", new object[]
			{
				p0
			});
		}

		// Token: 0x06001503 RID: 5379 RVA: 0x000482B3 File Offset: 0x000464B3
		internal static string EntityKeyMustBeScalar(object p0, object p1)
		{
			return EntityRes.GetString("EntityKeyMustBeScalar", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06001504 RID: 5380 RVA: 0x000482CD File Offset: 0x000464CD
		internal static string EntityKeyTypeCurrentlyNotSupportedInSSDL(object p0, object p1, object p2, object p3, object p4)
		{
			return EntityRes.GetString("EntityKeyTypeCurrentlyNotSupportedInSSDL", new object[]
			{
				p0,
				p1,
				p2,
				p3,
				p4
			});
		}

		// Token: 0x06001505 RID: 5381 RVA: 0x000482F4 File Offset: 0x000464F4
		internal static string EntityKeyTypeCurrentlyNotSupported(object p0, object p1, object p2)
		{
			return EntityRes.GetString("EntityKeyTypeCurrentlyNotSupported", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06001506 RID: 5382 RVA: 0x00048312 File Offset: 0x00046512
		internal static string MissingFacetDescription(object p0, object p1, object p2)
		{
			return EntityRes.GetString("MissingFacetDescription", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06001507 RID: 5383 RVA: 0x00048330 File Offset: 0x00046530
		internal static string EndWithManyMultiplicityCannotHaveOperationsSpecified(object p0, object p1)
		{
			return EntityRes.GetString("EndWithManyMultiplicityCannotHaveOperationsSpecified", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06001508 RID: 5384 RVA: 0x0004834A File Offset: 0x0004654A
		internal static string EndWithoutMultiplicity(object p0, object p1)
		{
			return EntityRes.GetString("EndWithoutMultiplicity", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06001509 RID: 5385 RVA: 0x00048364 File Offset: 0x00046564
		internal static string EntityContainerCannotExtendItself(object p0)
		{
			return EntityRes.GetString("EntityContainerCannotExtendItself", new object[]
			{
				p0
			});
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x0600150A RID: 5386 RVA: 0x0004837A File Offset: 0x0004657A
		internal static string ComposableFunctionOrFunctionImportMustDeclareReturnType
		{
			get
			{
				return EntityRes.GetString("ComposableFunctionOrFunctionImportMustDeclareReturnType");
			}
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x0600150B RID: 5387 RVA: 0x00048386 File Offset: 0x00046586
		internal static string NonComposableFunctionMustNotDeclareReturnType
		{
			get
			{
				return EntityRes.GetString("NonComposableFunctionMustNotDeclareReturnType");
			}
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x0600150C RID: 5388 RVA: 0x00048392 File Offset: 0x00046592
		internal static string CommandTextFunctionsNotComposable
		{
			get
			{
				return EntityRes.GetString("CommandTextFunctionsNotComposable");
			}
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x0600150D RID: 5389 RVA: 0x0004839E File Offset: 0x0004659E
		internal static string CommandTextFunctionsCannotDeclareStoreFunctionName
		{
			get
			{
				return EntityRes.GetString("CommandTextFunctionsCannotDeclareStoreFunctionName");
			}
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x0600150E RID: 5390 RVA: 0x000483AA File Offset: 0x000465AA
		internal static string NonComposableFunctionHasDisallowedAttribute
		{
			get
			{
				return EntityRes.GetString("NonComposableFunctionHasDisallowedAttribute");
			}
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x0600150F RID: 5391 RVA: 0x000483B6 File Offset: 0x000465B6
		internal static string EmptyDefiningQuery
		{
			get
			{
				return EntityRes.GetString("EmptyDefiningQuery");
			}
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06001510 RID: 5392 RVA: 0x000483C2 File Offset: 0x000465C2
		internal static string EmptyCommandText
		{
			get
			{
				return EntityRes.GetString("EmptyCommandText");
			}
		}

		// Token: 0x06001511 RID: 5393 RVA: 0x000483CE File Offset: 0x000465CE
		internal static string AmbiguousFunctionOverload(object p0, object p1)
		{
			return EntityRes.GetString("AmbiguousFunctionOverload", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06001512 RID: 5394 RVA: 0x000483E8 File Offset: 0x000465E8
		internal static string AmbiguousFunctionAndType(object p0, object p1)
		{
			return EntityRes.GetString("AmbiguousFunctionAndType", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06001513 RID: 5395 RVA: 0x00048402 File Offset: 0x00046602
		internal static string CycleInTypeHierarchy(object p0)
		{
			return EntityRes.GetString("CycleInTypeHierarchy", new object[]
			{
				p0
			});
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06001514 RID: 5396 RVA: 0x00048418 File Offset: 0x00046618
		internal static string IncorrectProviderManifest
		{
			get
			{
				return EntityRes.GetString("IncorrectProviderManifest");
			}
		}

		// Token: 0x06001515 RID: 5397 RVA: 0x00048424 File Offset: 0x00046624
		internal static string ComplexTypeAsReturnTypeAndDefinedEntitySet(object p0, object p1)
		{
			return EntityRes.GetString("ComplexTypeAsReturnTypeAndDefinedEntitySet", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06001516 RID: 5398 RVA: 0x0004843E File Offset: 0x0004663E
		internal static string ComplexTypeAsReturnTypeAndNestedComplexProperty(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ComplexTypeAsReturnTypeAndNestedComplexProperty", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06001517 RID: 5399 RVA: 0x0004845C File Offset: 0x0004665C
		internal static string FacetsOnNonScalarType(object p0)
		{
			return EntityRes.GetString("FacetsOnNonScalarType", new object[]
			{
				p0
			});
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06001518 RID: 5400 RVA: 0x00048472 File Offset: 0x00046672
		internal static string FacetDeclarationRequiresTypeAttribute
		{
			get
			{
				return EntityRes.GetString("FacetDeclarationRequiresTypeAttribute");
			}
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06001519 RID: 5401 RVA: 0x0004847E File Offset: 0x0004667E
		internal static string TypeMustBeDeclared
		{
			get
			{
				return EntityRes.GetString("TypeMustBeDeclared");
			}
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x0600151A RID: 5402 RVA: 0x0004848A File Offset: 0x0004668A
		internal static string RowTypeWithoutProperty
		{
			get
			{
				return EntityRes.GetString("RowTypeWithoutProperty");
			}
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x0600151B RID: 5403 RVA: 0x00048496 File Offset: 0x00046696
		internal static string TypeDeclaredAsAttributeAndElement
		{
			get
			{
				return EntityRes.GetString("TypeDeclaredAsAttributeAndElement");
			}
		}

		// Token: 0x0600151C RID: 5404 RVA: 0x000484A2 File Offset: 0x000466A2
		internal static string ReferenceToNonEntityType(object p0)
		{
			return EntityRes.GetString("ReferenceToNonEntityType", new object[]
			{
				p0
			});
		}

		// Token: 0x0600151D RID: 5405 RVA: 0x000484B8 File Offset: 0x000466B8
		internal static string NoCodeGenNamespaceInStructuralAnnotation(object p0)
		{
			return EntityRes.GetString("NoCodeGenNamespaceInStructuralAnnotation", new object[]
			{
				p0
			});
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x0600151E RID: 5406 RVA: 0x000484CE File Offset: 0x000466CE
		internal static string CannotLoadDifferentVersionOfSchemaInTheSameItemCollection
		{
			get
			{
				return EntityRes.GetString("CannotLoadDifferentVersionOfSchemaInTheSameItemCollection");
			}
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x0600151F RID: 5407 RVA: 0x000484DA File Offset: 0x000466DA
		internal static string InvalidEnumUnderlyingType
		{
			get
			{
				return EntityRes.GetString("InvalidEnumUnderlyingType");
			}
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06001520 RID: 5408 RVA: 0x000484E6 File Offset: 0x000466E6
		internal static string DuplicateEnumMember
		{
			get
			{
				return EntityRes.GetString("DuplicateEnumMember");
			}
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06001521 RID: 5409 RVA: 0x000484F2 File Offset: 0x000466F2
		internal static string CalculatedEnumValueOutOfRange
		{
			get
			{
				return EntityRes.GetString("CalculatedEnumValueOutOfRange");
			}
		}

		// Token: 0x06001522 RID: 5410 RVA: 0x000484FE File Offset: 0x000466FE
		internal static string EnumMemberValueOutOfItsUnderylingTypeRange(object p0, object p1, object p2)
		{
			return EntityRes.GetString("EnumMemberValueOutOfItsUnderylingTypeRange", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06001523 RID: 5411 RVA: 0x0004851C File Offset: 0x0004671C
		internal static string SpatialWithUseStrongSpatialTypesFalse
		{
			get
			{
				return EntityRes.GetString("SpatialWithUseStrongSpatialTypesFalse");
			}
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06001524 RID: 5412 RVA: 0x00048528 File Offset: 0x00046728
		internal static string ObjectQuery_QueryBuilder_InvalidProjectionList
		{
			get
			{
				return EntityRes.GetString("ObjectQuery_QueryBuilder_InvalidProjectionList");
			}
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06001525 RID: 5413 RVA: 0x00048534 File Offset: 0x00046734
		internal static string ObjectQuery_QueryBuilder_InvalidSortKeyList
		{
			get
			{
				return EntityRes.GetString("ObjectQuery_QueryBuilder_InvalidSortKeyList");
			}
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06001526 RID: 5414 RVA: 0x00048540 File Offset: 0x00046740
		internal static string ObjectQuery_QueryBuilder_InvalidGroupKeyList
		{
			get
			{
				return EntityRes.GetString("ObjectQuery_QueryBuilder_InvalidGroupKeyList");
			}
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06001527 RID: 5415 RVA: 0x0004854C File Offset: 0x0004674C
		internal static string ObjectQuery_QueryBuilder_InvalidSkipCount
		{
			get
			{
				return EntityRes.GetString("ObjectQuery_QueryBuilder_InvalidSkipCount");
			}
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x06001528 RID: 5416 RVA: 0x00048558 File Offset: 0x00046758
		internal static string ObjectQuery_QueryBuilder_InvalidTopCount
		{
			get
			{
				return EntityRes.GetString("ObjectQuery_QueryBuilder_InvalidTopCount");
			}
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x06001529 RID: 5417 RVA: 0x00048564 File Offset: 0x00046764
		internal static string ObjectQuery_QueryBuilder_InvalidFilterPredicate
		{
			get
			{
				return EntityRes.GetString("ObjectQuery_QueryBuilder_InvalidFilterPredicate");
			}
		}

		// Token: 0x0600152A RID: 5418 RVA: 0x00048570 File Offset: 0x00046770
		internal static string ObjectQuery_QueryBuilder_InvalidResultType(object p0)
		{
			return EntityRes.GetString("ObjectQuery_QueryBuilder_InvalidResultType", new object[]
			{
				p0
			});
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x0600152B RID: 5419 RVA: 0x00048586 File Offset: 0x00046786
		internal static string ObjectQuery_QueryBuilder_InvalidQueryArgument
		{
			get
			{
				return EntityRes.GetString("ObjectQuery_QueryBuilder_InvalidQueryArgument");
			}
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x0600152C RID: 5420 RVA: 0x00048592 File Offset: 0x00046792
		internal static string ObjectQuery_QueryBuilder_NotSupportedLinqSource
		{
			get
			{
				return EntityRes.GetString("ObjectQuery_QueryBuilder_NotSupportedLinqSource");
			}
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x0600152D RID: 5421 RVA: 0x0004859E File Offset: 0x0004679E
		internal static string ObjectQuery_InvalidEmptyQuery
		{
			get
			{
				return EntityRes.GetString("ObjectQuery_InvalidEmptyQuery");
			}
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x0600152E RID: 5422 RVA: 0x000485AA File Offset: 0x000467AA
		internal static string ObjectQuery_InvalidConnection
		{
			get
			{
				return EntityRes.GetString("ObjectQuery_InvalidConnection");
			}
		}

		// Token: 0x0600152F RID: 5423 RVA: 0x000485B6 File Offset: 0x000467B6
		internal static string ObjectQuery_InvalidQueryName(object p0)
		{
			return EntityRes.GetString("ObjectQuery_InvalidQueryName", new object[]
			{
				p0
			});
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06001530 RID: 5424 RVA: 0x000485CC File Offset: 0x000467CC
		internal static string ObjectQuery_UnableToMapResultType
		{
			get
			{
				return EntityRes.GetString("ObjectQuery_UnableToMapResultType");
			}
		}

		// Token: 0x06001531 RID: 5425 RVA: 0x000485D8 File Offset: 0x000467D8
		internal static string ObjectQuery_UnableToMaterializeArray(object p0, object p1)
		{
			return EntityRes.GetString("ObjectQuery_UnableToMaterializeArray", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06001532 RID: 5426 RVA: 0x000485F2 File Offset: 0x000467F2
		internal static string ObjectQuery_UnableToMaterializeArbitaryProjectionType(object p0)
		{
			return EntityRes.GetString("ObjectQuery_UnableToMaterializeArbitaryProjectionType", new object[]
			{
				p0
			});
		}

		// Token: 0x06001533 RID: 5427 RVA: 0x00048608 File Offset: 0x00046808
		internal static string ObjectParameter_InvalidParameterName(object p0)
		{
			return EntityRes.GetString("ObjectParameter_InvalidParameterName", new object[]
			{
				p0
			});
		}

		// Token: 0x06001534 RID: 5428 RVA: 0x0004861E File Offset: 0x0004681E
		internal static string ObjectParameter_InvalidParameterType(object p0)
		{
			return EntityRes.GetString("ObjectParameter_InvalidParameterType", new object[]
			{
				p0
			});
		}

		// Token: 0x06001535 RID: 5429 RVA: 0x00048634 File Offset: 0x00046834
		internal static string ObjectParameterCollection_ParameterNameNotFound(object p0)
		{
			return EntityRes.GetString("ObjectParameterCollection_ParameterNameNotFound", new object[]
			{
				p0
			});
		}

		// Token: 0x06001536 RID: 5430 RVA: 0x0004864A File Offset: 0x0004684A
		internal static string ObjectParameterCollection_ParameterAlreadyExists(object p0)
		{
			return EntityRes.GetString("ObjectParameterCollection_ParameterAlreadyExists", new object[]
			{
				p0
			});
		}

		// Token: 0x06001537 RID: 5431 RVA: 0x00048660 File Offset: 0x00046860
		internal static string ObjectParameterCollection_DuplicateParameterName(object p0)
		{
			return EntityRes.GetString("ObjectParameterCollection_DuplicateParameterName", new object[]
			{
				p0
			});
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06001538 RID: 5432 RVA: 0x00048676 File Offset: 0x00046876
		internal static string ObjectParameterCollection_ParametersLocked
		{
			get
			{
				return EntityRes.GetString("ObjectParameterCollection_ParametersLocked");
			}
		}

		// Token: 0x06001539 RID: 5433 RVA: 0x00048682 File Offset: 0x00046882
		internal static string ProviderReturnedNullForGetDbInformation(object p0)
		{
			return EntityRes.GetString("ProviderReturnedNullForGetDbInformation", new object[]
			{
				p0
			});
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x0600153A RID: 5434 RVA: 0x00048698 File Offset: 0x00046898
		internal static string ProviderReturnedNullForCreateCommandDefinition
		{
			get
			{
				return EntityRes.GetString("ProviderReturnedNullForCreateCommandDefinition");
			}
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x0600153B RID: 5435 RVA: 0x000486A4 File Offset: 0x000468A4
		internal static string ProviderDidNotReturnAProviderManifest
		{
			get
			{
				return EntityRes.GetString("ProviderDidNotReturnAProviderManifest");
			}
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x0600153C RID: 5436 RVA: 0x000486B0 File Offset: 0x000468B0
		internal static string ProviderDidNotReturnAProviderManifestToken
		{
			get
			{
				return EntityRes.GetString("ProviderDidNotReturnAProviderManifestToken");
			}
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x0600153D RID: 5437 RVA: 0x000486BC File Offset: 0x000468BC
		internal static string ProviderDidNotReturnSpatialServices
		{
			get
			{
				return EntityRes.GetString("ProviderDidNotReturnSpatialServices");
			}
		}

		// Token: 0x0600153E RID: 5438 RVA: 0x000486C8 File Offset: 0x000468C8
		internal static string ProviderDoesNotSupportType(object p0)
		{
			return EntityRes.GetString("ProviderDoesNotSupportType", new object[]
			{
				p0
			});
		}

		// Token: 0x0600153F RID: 5439 RVA: 0x000486DE File Offset: 0x000468DE
		internal static string NoStoreTypeForEdmType(object p0, object p1)
		{
			return EntityRes.GetString("NoStoreTypeForEdmType", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06001540 RID: 5440 RVA: 0x000486F8 File Offset: 0x000468F8
		internal static string ProviderRequiresStoreCommandTree
		{
			get
			{
				return EntityRes.GetString("ProviderRequiresStoreCommandTree");
			}
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x06001541 RID: 5441 RVA: 0x00048704 File Offset: 0x00046904
		internal static string ProviderShouldOverrideEscapeLikeArgument
		{
			get
			{
				return EntityRes.GetString("ProviderShouldOverrideEscapeLikeArgument");
			}
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x06001542 RID: 5442 RVA: 0x00048710 File Offset: 0x00046910
		internal static string ProviderEscapeLikeArgumentReturnedNull
		{
			get
			{
				return EntityRes.GetString("ProviderEscapeLikeArgumentReturnedNull");
			}
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x06001543 RID: 5443 RVA: 0x0004871C File Offset: 0x0004691C
		internal static string ProviderDidNotCreateACommandDefinition
		{
			get
			{
				return EntityRes.GetString("ProviderDidNotCreateACommandDefinition");
			}
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x06001544 RID: 5444 RVA: 0x00048728 File Offset: 0x00046928
		internal static string ProviderDoesNotSupportCreateDatabaseScript
		{
			get
			{
				return EntityRes.GetString("ProviderDoesNotSupportCreateDatabaseScript");
			}
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x06001545 RID: 5445 RVA: 0x00048734 File Offset: 0x00046934
		internal static string ProviderDoesNotSupportCreateDatabase
		{
			get
			{
				return EntityRes.GetString("ProviderDoesNotSupportCreateDatabase");
			}
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x06001546 RID: 5446 RVA: 0x00048740 File Offset: 0x00046940
		internal static string ProviderDoesNotSupportDatabaseExists
		{
			get
			{
				return EntityRes.GetString("ProviderDoesNotSupportDatabaseExists");
			}
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x06001547 RID: 5447 RVA: 0x0004874C File Offset: 0x0004694C
		internal static string ProviderDoesNotSupportDeleteDatabase
		{
			get
			{
				return EntityRes.GetString("ProviderDoesNotSupportDeleteDatabase");
			}
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x06001548 RID: 5448 RVA: 0x00048758 File Offset: 0x00046958
		internal static string Spatial_GeographyValueNotCompatibleWithSpatialServices
		{
			get
			{
				return EntityRes.GetString("Spatial_GeographyValueNotCompatibleWithSpatialServices");
			}
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x06001549 RID: 5449 RVA: 0x00048764 File Offset: 0x00046964
		internal static string Spatial_GeometryValueNotCompatibleWithSpatialServices
		{
			get
			{
				return EntityRes.GetString("Spatial_GeometryValueNotCompatibleWithSpatialServices");
			}
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x0600154A RID: 5450 RVA: 0x00048770 File Offset: 0x00046970
		internal static string Spatial_ProviderValueNotCompatibleWithSpatialServices
		{
			get
			{
				return EntityRes.GetString("Spatial_ProviderValueNotCompatibleWithSpatialServices");
			}
		}

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x0600154B RID: 5451 RVA: 0x0004877C File Offset: 0x0004697C
		internal static string Spatial_WellKnownGeographyValueNotValid
		{
			get
			{
				return EntityRes.GetString("Spatial_WellKnownGeographyValueNotValid");
			}
		}

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x0600154C RID: 5452 RVA: 0x00048788 File Offset: 0x00046988
		internal static string Spatial_WellKnownGeometryValueNotValid
		{
			get
			{
				return EntityRes.GetString("Spatial_WellKnownGeometryValueNotValid");
			}
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x0600154D RID: 5453 RVA: 0x00048794 File Offset: 0x00046994
		internal static string Spatial_WellKnownValueSerializationPropertyNotDirectlySettable
		{
			get
			{
				return EntityRes.GetString("Spatial_WellKnownValueSerializationPropertyNotDirectlySettable");
			}
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x0600154E RID: 5454 RVA: 0x000487A0 File Offset: 0x000469A0
		internal static string SqlSpatialservices_CouldNotCreateWellKnownGeographyValueNoSrid
		{
			get
			{
				return EntityRes.GetString("SqlSpatialservices_CouldNotCreateWellKnownGeographyValueNoSrid");
			}
		}

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x0600154F RID: 5455 RVA: 0x000487AC File Offset: 0x000469AC
		internal static string SqlSpatialservices_CouldNotCreateWellKnownGeographyValueNoWkbOrWkt
		{
			get
			{
				return EntityRes.GetString("SqlSpatialservices_CouldNotCreateWellKnownGeographyValueNoWkbOrWkt");
			}
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x06001550 RID: 5456 RVA: 0x000487B8 File Offset: 0x000469B8
		internal static string SqlSpatialservices_CouldNotCreateWellKnownGeometryValueNoSrid
		{
			get
			{
				return EntityRes.GetString("SqlSpatialservices_CouldNotCreateWellKnownGeometryValueNoSrid");
			}
		}

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06001551 RID: 5457 RVA: 0x000487C4 File Offset: 0x000469C4
		internal static string SqlSpatialservices_CouldNotCreateWellKnownGeometryValueNoWkbOrWkt
		{
			get
			{
				return EntityRes.GetString("SqlSpatialservices_CouldNotCreateWellKnownGeometryValueNoWkbOrWkt");
			}
		}

		// Token: 0x06001552 RID: 5458 RVA: 0x000487D0 File Offset: 0x000469D0
		internal static string SqlSpatialServices_ProviderValueNotSqlType(object p0)
		{
			return EntityRes.GetString("SqlSpatialServices_ProviderValueNotSqlType", new object[]
			{
				p0
			});
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06001553 RID: 5459 RVA: 0x000487E6 File Offset: 0x000469E6
		internal static string EntityConnectionString_Name
		{
			get
			{
				return EntityRes.GetString("EntityConnectionString_Name");
			}
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06001554 RID: 5460 RVA: 0x000487F2 File Offset: 0x000469F2
		internal static string EntityConnectionString_Provider
		{
			get
			{
				return EntityRes.GetString("EntityConnectionString_Provider");
			}
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06001555 RID: 5461 RVA: 0x000487FE File Offset: 0x000469FE
		internal static string EntityConnectionString_Metadata
		{
			get
			{
				return EntityRes.GetString("EntityConnectionString_Metadata");
			}
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06001556 RID: 5462 RVA: 0x0004880A File Offset: 0x00046A0A
		internal static string EntityConnectionString_ProviderConnectionString
		{
			get
			{
				return EntityRes.GetString("EntityConnectionString_ProviderConnectionString");
			}
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x06001557 RID: 5463 RVA: 0x00048816 File Offset: 0x00046A16
		internal static string EntityDataCategory_Context
		{
			get
			{
				return EntityRes.GetString("EntityDataCategory_Context");
			}
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x06001558 RID: 5464 RVA: 0x00048822 File Offset: 0x00046A22
		internal static string EntityDataCategory_NamedConnectionString
		{
			get
			{
				return EntityRes.GetString("EntityDataCategory_NamedConnectionString");
			}
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x06001559 RID: 5465 RVA: 0x0004882E File Offset: 0x00046A2E
		internal static string EntityDataCategory_Source
		{
			get
			{
				return EntityRes.GetString("EntityDataCategory_Source");
			}
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x0600155A RID: 5466 RVA: 0x0004883A File Offset: 0x00046A3A
		internal static string ObjectQuery_Span_IncludeRequiresEntityOrEntityCollection
		{
			get
			{
				return EntityRes.GetString("ObjectQuery_Span_IncludeRequiresEntityOrEntityCollection");
			}
		}

		// Token: 0x0600155B RID: 5467 RVA: 0x00048846 File Offset: 0x00046A46
		internal static string ObjectQuery_Span_NoNavProp(object p0, object p1)
		{
			return EntityRes.GetString("ObjectQuery_Span_NoNavProp", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x0600155C RID: 5468 RVA: 0x00048860 File Offset: 0x00046A60
		internal static string ObjectQuery_Span_SpanPathSyntaxError
		{
			get
			{
				return EntityRes.GetString("ObjectQuery_Span_SpanPathSyntaxError");
			}
		}

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x0600155D RID: 5469 RVA: 0x0004886C File Offset: 0x00046A6C
		internal static string ObjectQuery_Span_WhiteSpacePath
		{
			get
			{
				return EntityRes.GetString("ObjectQuery_Span_WhiteSpacePath");
			}
		}

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x0600155E RID: 5470 RVA: 0x00048878 File Offset: 0x00046A78
		internal static string EntityProxyTypeInfo_ProxyHasWrongWrapper
		{
			get
			{
				return EntityRes.GetString("EntityProxyTypeInfo_ProxyHasWrongWrapper");
			}
		}

		// Token: 0x0600155F RID: 5471 RVA: 0x00048884 File Offset: 0x00046A84
		internal static string EntityProxyTypeInfo_CannotSetEntityCollectionProperty(object p0, object p1)
		{
			return EntityRes.GetString("EntityProxyTypeInfo_CannotSetEntityCollectionProperty", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06001560 RID: 5472 RVA: 0x0004889E File Offset: 0x00046A9E
		internal static string EntityProxyTypeInfo_ProxyMetadataIsUnavailable(object p0)
		{
			return EntityRes.GetString("EntityProxyTypeInfo_ProxyMetadataIsUnavailable", new object[]
			{
				p0
			});
		}

		// Token: 0x06001561 RID: 5473 RVA: 0x000488B4 File Offset: 0x00046AB4
		internal static string EntityProxyTypeInfo_DuplicateOSpaceType(object p0)
		{
			return EntityRes.GetString("EntityProxyTypeInfo_DuplicateOSpaceType", new object[]
			{
				p0
			});
		}
	}
}
