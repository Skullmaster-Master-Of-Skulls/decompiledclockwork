using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlTypes;
using System.Globalization;
using System.Linq;
using System.Transactions;
using Microsoft.SqlServer.Server;

namespace System.Data.SqlClient
{
	// Token: 0x02000202 RID: 514
	internal static class SQL
	{
		// Token: 0x06001FAC RID: 8108 RVA: 0x000DA9E0 File Offset: 0x000D9DE0
		internal static Exception CannotGetDTCAddress()
		{
			return ADP.InvalidOperation(Res.GetString("SQL_CannotGetDTCAddress"));
		}

		// Token: 0x06001FAD RID: 8109 RVA: 0x000DA9FC File Offset: 0x000D9DFC
		internal static Exception InvalidOptionLength(string key)
		{
			return ADP.Argument(Res.GetString("SQL_InvalidOptionLength", new object[]
			{
				key
			}));
		}

		// Token: 0x06001FAE RID: 8110 RVA: 0x000DAA24 File Offset: 0x000D9E24
		internal static Exception InvalidInternalPacketSize(string str)
		{
			return ADP.ArgumentOutOfRange(str);
		}

		// Token: 0x06001FAF RID: 8111 RVA: 0x000DAA38 File Offset: 0x000D9E38
		internal static Exception InvalidPacketSize()
		{
			return ADP.ArgumentOutOfRange(Res.GetString("SQL_InvalidTDSPacketSize"));
		}

		// Token: 0x06001FB0 RID: 8112 RVA: 0x000DAA54 File Offset: 0x000D9E54
		internal static Exception InvalidPacketSizeValue()
		{
			return ADP.Argument(Res.GetString("SQL_InvalidPacketSizeValue"));
		}

		// Token: 0x06001FB1 RID: 8113 RVA: 0x000DAA70 File Offset: 0x000D9E70
		internal static Exception InvalidSSPIPacketSize()
		{
			return ADP.Argument(Res.GetString("SQL_InvalidSSPIPacketSize"));
		}

		// Token: 0x06001FB2 RID: 8114 RVA: 0x000DAA8C File Offset: 0x000D9E8C
		internal static Exception NullEmptyTransactionName()
		{
			return ADP.Argument(Res.GetString("SQL_NullEmptyTransactionName"));
		}

		// Token: 0x06001FB3 RID: 8115 RVA: 0x000DAAA8 File Offset: 0x000D9EA8
		internal static Exception SnapshotNotSupported(IsolationLevel level)
		{
			return ADP.Argument(Res.GetString("SQL_SnapshotNotSupported", new object[]
			{
				typeof(IsolationLevel),
				level.ToString()
			}));
		}

		// Token: 0x06001FB4 RID: 8116 RVA: 0x000DAAE8 File Offset: 0x000D9EE8
		internal static Exception UserInstanceFailoverNotCompatible()
		{
			return ADP.Argument(Res.GetString("SQL_UserInstanceFailoverNotCompatible"));
		}

		// Token: 0x06001FB5 RID: 8117 RVA: 0x000DAB04 File Offset: 0x000D9F04
		internal static Exception CredentialsNotProvided(SqlAuthenticationMethod auth)
		{
			return ADP.InvalidOperation(Res.GetString("SQL_CredentialsNotProvided", new object[]
			{
				DbConnectionStringBuilderUtil.AuthenticationTypeToString(auth)
			}));
		}

		// Token: 0x06001FB6 RID: 8118 RVA: 0x000DAB30 File Offset: 0x000D9F30
		internal static Exception AuthenticationAndIntegratedSecurity()
		{
			return ADP.Argument(Res.GetString("SQL_AuthenticationAndIntegratedSecurity"));
		}

		// Token: 0x06001FB7 RID: 8119 RVA: 0x000DAB4C File Offset: 0x000D9F4C
		internal static Exception IntegratedWithUserIDAndPassword()
		{
			return ADP.Argument(Res.GetString("SQL_IntegratedWithUserIDAndPassword"));
		}

		// Token: 0x06001FB8 RID: 8120 RVA: 0x000DAB68 File Offset: 0x000D9F68
		internal static Exception InteractiveWithoutUserID()
		{
			return ADP.Argument(Res.GetString("SQL_InteractiveWithoutUserID"));
		}

		// Token: 0x06001FB9 RID: 8121 RVA: 0x000DAB84 File Offset: 0x000D9F84
		internal static Exception InteractiveWithPassword()
		{
			return ADP.Argument(Res.GetString("SQL_InteractiveWithPassword"));
		}

		// Token: 0x06001FBA RID: 8122 RVA: 0x000DABA0 File Offset: 0x000D9FA0
		internal static Exception SettingIntegratedWithCredential()
		{
			return ADP.InvalidOperation(Res.GetString("SQL_SettingIntegratedWithCredential"));
		}

		// Token: 0x06001FBB RID: 8123 RVA: 0x000DABBC File Offset: 0x000D9FBC
		internal static Exception SettingCredentialWithIntegratedArgument()
		{
			return ADP.Argument(Res.GetString("SQL_SettingCredentialWithIntegrated"));
		}

		// Token: 0x06001FBC RID: 8124 RVA: 0x000DABD8 File Offset: 0x000D9FD8
		internal static Exception SettingCredentialWithIntegratedInvalid()
		{
			return ADP.InvalidOperation(Res.GetString("SQL_SettingCredentialWithIntegrated"));
		}

		// Token: 0x06001FBD RID: 8125 RVA: 0x000DABF4 File Offset: 0x000D9FF4
		internal static Exception InvalidSQLServerVersionUnknown()
		{
			return ADP.DataAdapter(Res.GetString("SQL_InvalidSQLServerVersionUnknown"));
		}

		// Token: 0x06001FBE RID: 8126 RVA: 0x000DAC10 File Offset: 0x000DA010
		internal static Exception SynchronousCallMayNotPend()
		{
			return new Exception(Res.GetString("Sql_InternalError"));
		}

		// Token: 0x06001FBF RID: 8127 RVA: 0x000DAC2C File Offset: 0x000DA02C
		internal static Exception ConnectionLockedForBcpEvent()
		{
			return ADP.InvalidOperation(Res.GetString("SQL_ConnectionLockedForBcpEvent"));
		}

		// Token: 0x06001FC0 RID: 8128 RVA: 0x000DAC48 File Offset: 0x000DA048
		internal static Exception AsyncConnectionRequired()
		{
			return ADP.InvalidOperation(Res.GetString("SQL_AsyncConnectionRequired"));
		}

		// Token: 0x06001FC1 RID: 8129 RVA: 0x000DAC64 File Offset: 0x000DA064
		internal static Exception FatalTimeout()
		{
			return ADP.InvalidOperation(Res.GetString("SQL_FatalTimeout"));
		}

		// Token: 0x06001FC2 RID: 8130 RVA: 0x000DAC80 File Offset: 0x000DA080
		internal static Exception InstanceFailure()
		{
			return ADP.InvalidOperation(Res.GetString("SQL_InstanceFailure"));
		}

		// Token: 0x06001FC3 RID: 8131 RVA: 0x000DAC9C File Offset: 0x000DA09C
		internal static Exception ChangePasswordArgumentMissing(string argumentName)
		{
			return ADP.ArgumentNull(Res.GetString("SQL_ChangePasswordArgumentMissing", new object[]
			{
				argumentName
			}));
		}

		// Token: 0x06001FC4 RID: 8132 RVA: 0x000DACC4 File Offset: 0x000DA0C4
		internal static Exception ChangePasswordConflictsWithSSPI()
		{
			return ADP.Argument(Res.GetString("SQL_ChangePasswordConflictsWithSSPI"));
		}

		// Token: 0x06001FC5 RID: 8133 RVA: 0x000DACE0 File Offset: 0x000DA0E0
		internal static Exception ChangePasswordRequiresYukon()
		{
			return ADP.InvalidOperation(Res.GetString("SQL_ChangePasswordRequiresYukon"));
		}

		// Token: 0x06001FC6 RID: 8134 RVA: 0x000DACFC File Offset: 0x000DA0FC
		internal static Exception UnknownSysTxIsolationLevel(IsolationLevel isolationLevel)
		{
			return ADP.InvalidOperation(Res.GetString("SQL_UnknownSysTxIsolationLevel", new object[]
			{
				isolationLevel.ToString()
			}));
		}

		// Token: 0x06001FC7 RID: 8135 RVA: 0x000DAD30 File Offset: 0x000DA130
		internal static Exception ChangePasswordUseOfUnallowedKey(string key)
		{
			return ADP.InvalidOperation(Res.GetString("SQL_ChangePasswordUseOfUnallowedKey", new object[]
			{
				key
			}));
		}

		// Token: 0x06001FC8 RID: 8136 RVA: 0x000DAD58 File Offset: 0x000DA158
		internal static Exception InvalidPartnerConfiguration(string server, string database)
		{
			return ADP.InvalidOperation(Res.GetString("SQL_InvalidPartnerConfiguration", new object[]
			{
				server,
				database
			}));
		}

		// Token: 0x06001FC9 RID: 8137 RVA: 0x000DAD84 File Offset: 0x000DA184
		internal static Exception BatchedUpdateColumnEncryptionSettingMismatch()
		{
			return ADP.InvalidOperation(Res.GetString("TCE_BatchedUpdateColumnEncryptionSettingMismatch", new object[]
			{
				"SqlCommandColumnEncryptionSetting",
				"SelectCommand",
				"InsertCommand",
				"UpdateCommand",
				"DeleteCommand"
			}));
		}

		// Token: 0x06001FCA RID: 8138 RVA: 0x000DADD0 File Offset: 0x000DA1D0
		internal static Exception MARSUnspportedOnConnection()
		{
			return ADP.InvalidOperation(Res.GetString("SQL_MarsUnsupportedOnConnection"));
		}

		// Token: 0x06001FCB RID: 8139 RVA: 0x000DADEC File Offset: 0x000DA1EC
		internal static Exception CannotModifyPropertyAsyncOperationInProgress(string property)
		{
			return ADP.InvalidOperation(Res.GetString("SQL_CannotModifyPropertyAsyncOperationInProgress", new object[]
			{
				property
			}));
		}

		// Token: 0x06001FCC RID: 8140 RVA: 0x000DAE14 File Offset: 0x000DA214
		internal static Exception NonLocalSSEInstance()
		{
			return ADP.NotSupported(Res.GetString("SQL_NonLocalSSEInstance"));
		}

		// Token: 0x06001FCD RID: 8141 RVA: 0x000DAE30 File Offset: 0x000DA230
		internal static Exception UnsupportedAuthentication(string authentication)
		{
			return ADP.NotSupported(Res.GetString("SQL_UnsupportedAuthentication", new object[]
			{
				authentication
			}));
		}

		// Token: 0x06001FCE RID: 8142 RVA: 0x000DAE58 File Offset: 0x000DA258
		internal static Exception UnsupportedSqlAuthenticationMethod(SqlAuthenticationMethod authentication)
		{
			return ADP.NotSupported(Res.GetString("SQL_UnsupportedSqlAuthenticationMethod", new object[]
			{
				authentication
			}));
		}

		// Token: 0x06001FCF RID: 8143 RVA: 0x000DAE84 File Offset: 0x000DA284
		internal static Exception CannotCreateAuthProvider(string authentication, string type, Exception e)
		{
			return ADP.Argument(Res.GetString("SQL_CannotCreateAuthProvider", new object[]
			{
				authentication,
				type
			}), e);
		}

		// Token: 0x06001FD0 RID: 8144 RVA: 0x000DAEB0 File Offset: 0x000DA2B0
		internal static Exception CannotCreateSqlAuthInitializer(string type, Exception e)
		{
			return ADP.Argument(Res.GetString("SQL_CannotCreateAuthInitializer", new object[]
			{
				type
			}), e);
		}

		// Token: 0x06001FD1 RID: 8145 RVA: 0x000DAED8 File Offset: 0x000DA2D8
		internal static Exception CannotInitializeAuthProvider(string type, Exception e)
		{
			return ADP.InvalidOperation(Res.GetString("SQL_CannotInitializeAuthProvider", new object[]
			{
				type
			}), e);
		}

		// Token: 0x06001FD2 RID: 8146 RVA: 0x000DAF00 File Offset: 0x000DA300
		internal static Exception UnsupportedAuthenticationByProvider(string authentication, string type)
		{
			return ADP.NotSupported(Res.GetString("SQL_UnsupportedAuthenticationByProvider", new object[]
			{
				type,
				authentication
			}));
		}

		// Token: 0x06001FD3 RID: 8147 RVA: 0x000DAF2C File Offset: 0x000DA32C
		internal static Exception CannotFindAuthProvider(string authentication)
		{
			return ADP.Argument(Res.GetString("SQL_CannotFindAuthProvider", new object[]
			{
				authentication
			}));
		}

		// Token: 0x06001FD4 RID: 8148 RVA: 0x000DAF54 File Offset: 0x000DA354
		internal static Exception CannotGetAuthProviderConfig(Exception e)
		{
			return ADP.InvalidOperation(Res.GetString("SQL_CannotGetAuthProviderConfig"), e);
		}

		// Token: 0x06001FD5 RID: 8149 RVA: 0x000DAF74 File Offset: 0x000DA374
		internal static Exception ParameterCannotBeEmpty(string paramName)
		{
			return ADP.ArgumentNull(Res.GetString("SQL_ParameterCannotBeEmpty", new object[]
			{
				paramName
			}));
		}

		// Token: 0x06001FD6 RID: 8150 RVA: 0x000DAF9C File Offset: 0x000DA39C
		internal static Exception NotificationsRequireYukon()
		{
			return ADP.NotSupported(Res.GetString("SQL_NotificationsRequireYukon"));
		}

		// Token: 0x06001FD7 RID: 8151 RVA: 0x000DAFB8 File Offset: 0x000DA3B8
		internal static ArgumentOutOfRangeException NotSupportedEnumerationValue(Type type, int value)
		{
			return ADP.ArgumentOutOfRange(Res.GetString("SQL_NotSupportedEnumerationValue", new object[]
			{
				type.Name,
				value.ToString(CultureInfo.InvariantCulture)
			}), type.Name);
		}

		// Token: 0x06001FD8 RID: 8152 RVA: 0x000DAFF8 File Offset: 0x000DA3F8
		internal static ArgumentOutOfRangeException NotSupportedCommandType(CommandType value)
		{
			return SQL.NotSupportedEnumerationValue(typeof(CommandType), (int)value);
		}

		// Token: 0x06001FD9 RID: 8153 RVA: 0x000DB018 File Offset: 0x000DA418
		internal static ArgumentOutOfRangeException NotSupportedIsolationLevel(IsolationLevel value)
		{
			return SQL.NotSupportedEnumerationValue(typeof(IsolationLevel), (int)value);
		}

		// Token: 0x06001FDA RID: 8154 RVA: 0x000DB038 File Offset: 0x000DA438
		internal static Exception OperationCancelled()
		{
			return ADP.InvalidOperation(Res.GetString("SQL_OperationCancelled"));
		}

		// Token: 0x06001FDB RID: 8155 RVA: 0x000DB058 File Offset: 0x000DA458
		internal static Exception PendingBeginXXXExists()
		{
			return ADP.InvalidOperation(Res.GetString("SQL_PendingBeginXXXExists"));
		}

		// Token: 0x06001FDC RID: 8156 RVA: 0x000DB074 File Offset: 0x000DA474
		internal static ArgumentOutOfRangeException InvalidSqlDependencyTimeout(string param)
		{
			return ADP.ArgumentOutOfRange(Res.GetString("SqlDependency_InvalidTimeout"), param);
		}

		// Token: 0x06001FDD RID: 8157 RVA: 0x000DB094 File Offset: 0x000DA494
		internal static Exception NonXmlResult()
		{
			return ADP.InvalidOperation(Res.GetString("SQL_NonXmlResult"));
		}

		// Token: 0x06001FDE RID: 8158 RVA: 0x000DB0B0 File Offset: 0x000DA4B0
		internal static Exception InvalidUdt3PartNameFormat()
		{
			return ADP.Argument(Res.GetString("SQL_InvalidUdt3PartNameFormat"));
		}

		// Token: 0x06001FDF RID: 8159 RVA: 0x000DB0CC File Offset: 0x000DA4CC
		internal static Exception InvalidParameterTypeNameFormat()
		{
			return ADP.Argument(Res.GetString("SQL_InvalidParameterTypeNameFormat"));
		}

		// Token: 0x06001FE0 RID: 8160 RVA: 0x000DB0E8 File Offset: 0x000DA4E8
		internal static Exception InvalidParameterNameLength(string value)
		{
			return ADP.Argument(Res.GetString("SQL_InvalidParameterNameLength", new object[]
			{
				value
			}));
		}

		// Token: 0x06001FE1 RID: 8161 RVA: 0x000DB110 File Offset: 0x000DA510
		internal static Exception PrecisionValueOutOfRange(byte precision)
		{
			return ADP.Argument(Res.GetString("SQL_PrecisionValueOutOfRange", new object[]
			{
				precision.ToString(CultureInfo.InvariantCulture)
			}));
		}

		// Token: 0x06001FE2 RID: 8162 RVA: 0x000DB144 File Offset: 0x000DA544
		internal static Exception ScaleValueOutOfRange(byte scale)
		{
			return ADP.Argument(Res.GetString("SQL_ScaleValueOutOfRange", new object[]
			{
				scale.ToString(CultureInfo.InvariantCulture)
			}));
		}

		// Token: 0x06001FE3 RID: 8163 RVA: 0x000DB178 File Offset: 0x000DA578
		internal static Exception TimeScaleValueOutOfRange(byte scale)
		{
			return ADP.Argument(Res.GetString("SQL_TimeScaleValueOutOfRange", new object[]
			{
				scale.ToString(CultureInfo.InvariantCulture)
			}));
		}

		// Token: 0x06001FE4 RID: 8164 RVA: 0x000DB1AC File Offset: 0x000DA5AC
		internal static Exception InvalidSqlDbType(SqlDbType value)
		{
			return ADP.InvalidEnumerationValue(typeof(SqlDbType), (int)value);
		}

		// Token: 0x06001FE5 RID: 8165 RVA: 0x000DB1CC File Offset: 0x000DA5CC
		internal static Exception UnsupportedTVPOutputParameter(ParameterDirection direction, string paramName)
		{
			return ADP.NotSupported(Res.GetString("SqlParameter_UnsupportedTVPOutputParameter", new object[]
			{
				direction.ToString(),
				paramName
			}));
		}

		// Token: 0x06001FE6 RID: 8166 RVA: 0x000DB204 File Offset: 0x000DA604
		internal static Exception DBNullNotSupportedForTVPValues(string paramName)
		{
			return ADP.NotSupported(Res.GetString("SqlParameter_DBNullNotSupportedForTVP", new object[]
			{
				paramName
			}));
		}

		// Token: 0x06001FE7 RID: 8167 RVA: 0x000DB22C File Offset: 0x000DA62C
		internal static Exception InvalidTableDerivedPrecisionForTvp(string columnName, byte precision)
		{
			return ADP.InvalidOperation(Res.GetString("SqlParameter_InvalidTableDerivedPrecisionForTvp", new object[]
			{
				precision,
				columnName,
				SqlDecimal.MaxPrecision
			}));
		}

		// Token: 0x06001FE8 RID: 8168 RVA: 0x000DB268 File Offset: 0x000DA668
		internal static Exception UnexpectedTypeNameForNonStructParams(string paramName)
		{
			return ADP.NotSupported(Res.GetString("SqlParameter_UnexpectedTypeNameForNonStruct", new object[]
			{
				paramName
			}));
		}

		// Token: 0x06001FE9 RID: 8169 RVA: 0x000DB290 File Offset: 0x000DA690
		internal static Exception SingleValuedStructNotSupported()
		{
			return ADP.NotSupported(Res.GetString("MetaType_SingleValuedStructNotSupported"));
		}

		// Token: 0x06001FEA RID: 8170 RVA: 0x000DB2AC File Offset: 0x000DA6AC
		internal static Exception ParameterInvalidVariant(string paramName)
		{
			return ADP.InvalidOperation(Res.GetString("SQL_ParameterInvalidVariant", new object[]
			{
				paramName
			}));
		}

		// Token: 0x06001FEB RID: 8171 RVA: 0x000DB2D4 File Offset: 0x000DA6D4
		internal static Exception MustSetTypeNameForParam(string paramType, string paramName)
		{
			return ADP.Argument(Res.GetString("SQL_ParameterTypeNameRequired", new object[]
			{
				paramType,
				paramName
			}));
		}

		// Token: 0x06001FEC RID: 8172 RVA: 0x000DB300 File Offset: 0x000DA700
		internal static Exception NullSchemaTableDataTypeNotSupported(string columnName)
		{
			return ADP.Argument(Res.GetString("NullSchemaTableDataTypeNotSupported", new object[]
			{
				columnName
			}));
		}

		// Token: 0x06001FED RID: 8173 RVA: 0x000DB328 File Offset: 0x000DA728
		internal static Exception InvalidSchemaTableOrdinals()
		{
			return ADP.Argument(Res.GetString("InvalidSchemaTableOrdinals"));
		}

		// Token: 0x06001FEE RID: 8174 RVA: 0x000DB344 File Offset: 0x000DA744
		internal static Exception EnumeratedRecordMetaDataChanged(string fieldName, int recordNumber)
		{
			return ADP.Argument(Res.GetString("SQL_EnumeratedRecordMetaDataChanged", new object[]
			{
				fieldName,
				recordNumber
			}));
		}

		// Token: 0x06001FEF RID: 8175 RVA: 0x000DB374 File Offset: 0x000DA774
		internal static Exception EnumeratedRecordFieldCountChanged(int recordNumber)
		{
			return ADP.Argument(Res.GetString("SQL_EnumeratedRecordFieldCountChanged", new object[]
			{
				recordNumber
			}));
		}

		// Token: 0x06001FF0 RID: 8176 RVA: 0x000DB3A0 File Offset: 0x000DA7A0
		internal static Exception InvalidTDSVersion()
		{
			return ADP.InvalidOperation(Res.GetString("SQL_InvalidTDSVersion"));
		}

		// Token: 0x06001FF1 RID: 8177 RVA: 0x000DB3BC File Offset: 0x000DA7BC
		internal static Exception ParsingError(ParsingErrorState state)
		{
			string name = "SQL_ParsingErrorWithState";
			object[] array = new object[1];
			int num = 0;
			int num2 = (int)state;
			array[num] = num2.ToString(CultureInfo.InvariantCulture);
			return ADP.InvalidOperation(Res.GetString(name, array));
		}

		// Token: 0x06001FF2 RID: 8178 RVA: 0x000DB3F0 File Offset: 0x000DA7F0
		internal static Exception ParsingError(ParsingErrorState state, Exception innerException)
		{
			string name = "SQL_ParsingErrorWithState";
			object[] array = new object[1];
			int num = 0;
			int num2 = (int)state;
			array[num] = num2.ToString(CultureInfo.InvariantCulture);
			return ADP.InvalidOperation(Res.GetString(name, array), innerException);
		}

		// Token: 0x06001FF3 RID: 8179 RVA: 0x000DB424 File Offset: 0x000DA824
		internal static Exception ParsingErrorValue(ParsingErrorState state, int value)
		{
			string name = "SQL_ParsingErrorValue";
			object[] array = new object[2];
			int num = 0;
			int num2 = (int)state;
			array[num] = num2.ToString(CultureInfo.InvariantCulture);
			array[1] = value;
			return ADP.InvalidOperation(Res.GetString(name, array));
		}

		// Token: 0x06001FF4 RID: 8180 RVA: 0x000DB460 File Offset: 0x000DA860
		internal static Exception ParsingErrorOffset(ParsingErrorState state, int offset)
		{
			string name = "SQL_ParsingErrorOffset";
			object[] array = new object[2];
			int num = 0;
			int num2 = (int)state;
			array[num] = num2.ToString(CultureInfo.InvariantCulture);
			array[1] = offset;
			return ADP.InvalidOperation(Res.GetString(name, array));
		}

		// Token: 0x06001FF5 RID: 8181 RVA: 0x000DB49C File Offset: 0x000DA89C
		internal static Exception ParsingErrorFeatureId(ParsingErrorState state, int featureId)
		{
			string name = "SQL_ParsingErrorFeatureId";
			object[] array = new object[2];
			int num = 0;
			int num2 = (int)state;
			array[num] = num2.ToString(CultureInfo.InvariantCulture);
			array[1] = featureId;
			return ADP.InvalidOperation(Res.GetString(name, array));
		}

		// Token: 0x06001FF6 RID: 8182 RVA: 0x000DB4D8 File Offset: 0x000DA8D8
		internal static Exception ParsingErrorToken(ParsingErrorState state, int token)
		{
			string name = "SQL_ParsingErrorToken";
			object[] array = new object[2];
			int num = 0;
			int num2 = (int)state;
			array[num] = num2.ToString(CultureInfo.InvariantCulture);
			array[1] = token;
			return ADP.InvalidOperation(Res.GetString(name, array));
		}

		// Token: 0x06001FF7 RID: 8183 RVA: 0x000DB514 File Offset: 0x000DA914
		internal static Exception ParsingErrorLength(ParsingErrorState state, int length)
		{
			string name = "SQL_ParsingErrorLength";
			object[] array = new object[2];
			int num = 0;
			int num2 = (int)state;
			array[num] = num2.ToString(CultureInfo.InvariantCulture);
			array[1] = length;
			return ADP.InvalidOperation(Res.GetString(name, array));
		}

		// Token: 0x06001FF8 RID: 8184 RVA: 0x000DB550 File Offset: 0x000DA950
		internal static Exception ParsingErrorStatus(ParsingErrorState state, int status)
		{
			string name = "SQL_ParsingErrorStatus";
			object[] array = new object[2];
			int num = 0;
			int num2 = (int)state;
			array[num] = num2.ToString(CultureInfo.InvariantCulture);
			array[1] = status;
			return ADP.InvalidOperation(Res.GetString(name, array));
		}

		// Token: 0x06001FF9 RID: 8185 RVA: 0x000DB58C File Offset: 0x000DA98C
		internal static Exception ParsingErrorLibraryType(ParsingErrorState state, int libraryType)
		{
			string name = "SQL_ParsingErrorAuthLibraryType";
			object[] array = new object[2];
			int num = 0;
			int num2 = (int)state;
			array[num] = num2.ToString(CultureInfo.InvariantCulture);
			array[1] = libraryType;
			return ADP.InvalidOperation(Res.GetString(name, array));
		}

		// Token: 0x06001FFA RID: 8186 RVA: 0x000DB5C8 File Offset: 0x000DA9C8
		internal static Exception MoneyOverflow(string moneyValue)
		{
			return ADP.Overflow(Res.GetString("SQL_MoneyOverflow", new object[]
			{
				moneyValue
			}));
		}

		// Token: 0x06001FFB RID: 8187 RVA: 0x000DB5F0 File Offset: 0x000DA9F0
		internal static Exception SmallDateTimeOverflow(string datetime)
		{
			return ADP.Overflow(Res.GetString("SQL_SmallDateTimeOverflow", new object[]
			{
				datetime
			}));
		}

		// Token: 0x06001FFC RID: 8188 RVA: 0x000DB618 File Offset: 0x000DAA18
		internal static Exception SNIPacketAllocationFailure()
		{
			return ADP.InvalidOperation(Res.GetString("SQL_SNIPacketAllocationFailure"));
		}

		// Token: 0x06001FFD RID: 8189 RVA: 0x000DB634 File Offset: 0x000DAA34
		internal static Exception TimeOverflow(string time)
		{
			return ADP.Overflow(Res.GetString("SQL_TimeOverflow", new object[]
			{
				time
			}));
		}

		// Token: 0x06001FFE RID: 8190 RVA: 0x000DB65C File Offset: 0x000DAA5C
		internal static Exception InvalidRead()
		{
			return ADP.InvalidOperation(Res.GetString("SQL_InvalidRead"));
		}

		// Token: 0x06001FFF RID: 8191 RVA: 0x000DB678 File Offset: 0x000DAA78
		internal static Exception NonBlobColumn(string columnName)
		{
			return ADP.InvalidCast(Res.GetString("SQL_NonBlobColumn", new object[]
			{
				columnName
			}));
		}

		// Token: 0x06002000 RID: 8192 RVA: 0x000DB6A0 File Offset: 0x000DAAA0
		internal static Exception NonCharColumn(string columnName)
		{
			return ADP.InvalidCast(Res.GetString("SQL_NonCharColumn", new object[]
			{
				columnName
			}));
		}

		// Token: 0x06002001 RID: 8193 RVA: 0x000DB6C8 File Offset: 0x000DAAC8
		internal static Exception StreamNotSupportOnColumnType(string columnName)
		{
			return ADP.InvalidCast(Res.GetString("SQL_StreamNotSupportOnColumnType", new object[]
			{
				columnName
			}));
		}

		// Token: 0x06002002 RID: 8194 RVA: 0x000DB6F0 File Offset: 0x000DAAF0
		internal static Exception StreamNotSupportOnEncryptedColumn(string columnName)
		{
			return ADP.InvalidOperation(Res.GetString("TCE_StreamNotSupportOnEncryptedColumn", new object[]
			{
				columnName,
				"Stream"
			}));
		}

		// Token: 0x06002003 RID: 8195 RVA: 0x000DB720 File Offset: 0x000DAB20
		internal static Exception SequentialAccessNotSupportedOnEncryptedColumn(string columnName)
		{
			return ADP.InvalidOperation(Res.GetString("TCE_SequentialAccessNotSupportedOnEncryptedColumn", new object[]
			{
				columnName,
				"CommandBehavior=SequentialAccess"
			}));
		}

		// Token: 0x06002004 RID: 8196 RVA: 0x000DB750 File Offset: 0x000DAB50
		internal static Exception TextReaderNotSupportOnColumnType(string columnName)
		{
			return ADP.InvalidCast(Res.GetString("SQL_TextReaderNotSupportOnColumnType", new object[]
			{
				columnName
			}));
		}

		// Token: 0x06002005 RID: 8197 RVA: 0x000DB778 File Offset: 0x000DAB78
		internal static Exception XmlReaderNotSupportOnColumnType(string columnName)
		{
			return ADP.InvalidCast(Res.GetString("SQL_XmlReaderNotSupportOnColumnType", new object[]
			{
				columnName
			}));
		}

		// Token: 0x06002006 RID: 8198 RVA: 0x000DB7A0 File Offset: 0x000DABA0
		internal static Exception UDTUnexpectedResult(string exceptionText)
		{
			return ADP.TypeLoad(Res.GetString("SQLUDT_Unexpected", new object[]
			{
				exceptionText
			}));
		}

		// Token: 0x06002007 RID: 8199 RVA: 0x000DB7C8 File Offset: 0x000DABC8
		internal static Exception CannotCompleteDelegatedTransactionWithOpenResults(SqlInternalConnectionTds internalConnection)
		{
			return SqlException.CreateException(new SqlErrorCollection
			{
				new SqlError(-2, 0, 11, null, Res.GetString("ADP_OpenReaderExists"), "", 0, 258U)
			}, null, internalConnection, null);
		}

		// Token: 0x06002008 RID: 8200 RVA: 0x000DB80C File Offset: 0x000DAC0C
		internal static TransactionPromotionException PromotionFailed(Exception inner)
		{
			TransactionPromotionException ex = new TransactionPromotionException(Res.GetString("SqlDelegatedTransaction_PromotionFailed"), inner);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06002009 RID: 8201 RVA: 0x000DB834 File Offset: 0x000DAC34
		internal static Exception SqlCommandHasExistingSqlNotificationRequest()
		{
			return ADP.InvalidOperation(Res.GetString("SQLNotify_AlreadyHasCommand"));
		}

		// Token: 0x0600200A RID: 8202 RVA: 0x000DB850 File Offset: 0x000DAC50
		internal static Exception SqlDepCannotBeCreatedInProc()
		{
			return ADP.InvalidOperation(Res.GetString("SqlNotify_SqlDepCannotBeCreatedInProc"));
		}

		// Token: 0x0600200B RID: 8203 RVA: 0x000DB86C File Offset: 0x000DAC6C
		internal static Exception SqlDepDefaultOptionsButNoStart()
		{
			return ADP.InvalidOperation(Res.GetString("SqlDependency_DefaultOptionsButNoStart"));
		}

		// Token: 0x0600200C RID: 8204 RVA: 0x000DB888 File Offset: 0x000DAC88
		internal static Exception SqlDependencyDatabaseBrokerDisabled()
		{
			return ADP.InvalidOperation(Res.GetString("SqlDependency_DatabaseBrokerDisabled"));
		}

		// Token: 0x0600200D RID: 8205 RVA: 0x000DB8A4 File Offset: 0x000DACA4
		internal static Exception SqlDependencyEventNoDuplicate()
		{
			return ADP.InvalidOperation(Res.GetString("SqlDependency_EventNoDuplicate"));
		}

		// Token: 0x0600200E RID: 8206 RVA: 0x000DB8C0 File Offset: 0x000DACC0
		internal static Exception SqlDependencyDuplicateStart()
		{
			return ADP.InvalidOperation(Res.GetString("SqlDependency_DuplicateStart"));
		}

		// Token: 0x0600200F RID: 8207 RVA: 0x000DB8DC File Offset: 0x000DACDC
		internal static Exception SqlDependencyIdMismatch()
		{
			return ADP.InvalidOperation(Res.GetString("SqlDependency_IdMismatch"));
		}

		// Token: 0x06002010 RID: 8208 RVA: 0x000DB8F8 File Offset: 0x000DACF8
		internal static Exception SqlDependencyNoMatchingServerStart()
		{
			return ADP.InvalidOperation(Res.GetString("SqlDependency_NoMatchingServerStart"));
		}

		// Token: 0x06002011 RID: 8209 RVA: 0x000DB914 File Offset: 0x000DAD14
		internal static Exception SqlDependencyNoMatchingServerDatabaseStart()
		{
			return ADP.InvalidOperation(Res.GetString("SqlDependency_NoMatchingServerDatabaseStart"));
		}

		// Token: 0x06002012 RID: 8210 RVA: 0x000DB930 File Offset: 0x000DAD30
		internal static Exception SqlNotificationException(SqlNotificationEventArgs notify)
		{
			return ADP.InvalidOperation(Res.GetString("SQLNotify_ErrorFormat", new object[]
			{
				notify.Type,
				notify.Info,
				notify.Source
			}));
		}

		// Token: 0x06002013 RID: 8211 RVA: 0x000DB97C File Offset: 0x000DAD7C
		internal static Exception SqlMetaDataNoMetaData()
		{
			return ADP.InvalidOperation(Res.GetString("SqlMetaData_NoMetadata"));
		}

		// Token: 0x06002014 RID: 8212 RVA: 0x000DB998 File Offset: 0x000DAD98
		internal static Exception MustSetUdtTypeNameForUdtParams()
		{
			return ADP.Argument(Res.GetString("SQLUDT_InvalidUdtTypeName"));
		}

		// Token: 0x06002015 RID: 8213 RVA: 0x000DB9B4 File Offset: 0x000DADB4
		internal static Exception UnexpectedUdtTypeNameForNonUdtParams()
		{
			return ADP.Argument(Res.GetString("SQLUDT_UnexpectedUdtTypeName"));
		}

		// Token: 0x06002016 RID: 8214 RVA: 0x000DB9D0 File Offset: 0x000DADD0
		internal static Exception UDTInvalidSqlType(string typeName)
		{
			return ADP.Argument(Res.GetString("SQLUDT_InvalidSqlType", new object[]
			{
				typeName
			}));
		}

		// Token: 0x06002017 RID: 8215 RVA: 0x000DB9F8 File Offset: 0x000DADF8
		internal static Exception InvalidSqlDbTypeForConstructor(SqlDbType type)
		{
			return ADP.Argument(Res.GetString("SqlMetaData_InvalidSqlDbTypeForConstructorFormat", new object[]
			{
				type.ToString()
			}));
		}

		// Token: 0x06002018 RID: 8216 RVA: 0x000DBA2C File Offset: 0x000DAE2C
		internal static Exception NameTooLong(string parameterName)
		{
			return ADP.Argument(Res.GetString("SqlMetaData_NameTooLong"), parameterName);
		}

		// Token: 0x06002019 RID: 8217 RVA: 0x000DBA4C File Offset: 0x000DAE4C
		internal static Exception InvalidSortOrder(SortOrder order)
		{
			return ADP.InvalidEnumerationValue(typeof(SortOrder), (int)order);
		}

		// Token: 0x0600201A RID: 8218 RVA: 0x000DBA6C File Offset: 0x000DAE6C
		internal static Exception MustSpecifyBothSortOrderAndOrdinal(SortOrder order, int ordinal)
		{
			return ADP.InvalidOperation(Res.GetString("SqlMetaData_SpecifyBothSortOrderAndOrdinal", new object[]
			{
				order.ToString(),
				ordinal
			}));
		}

		// Token: 0x0600201B RID: 8219 RVA: 0x000DBAA8 File Offset: 0x000DAEA8
		internal static Exception TableTypeCanOnlyBeParameter()
		{
			return ADP.Argument(Res.GetString("SQLTVP_TableTypeCanOnlyBeParameter"));
		}

		// Token: 0x0600201C RID: 8220 RVA: 0x000DBAC4 File Offset: 0x000DAEC4
		internal static Exception UnsupportedColumnTypeForSqlProvider(string columnName, string typeName)
		{
			return ADP.Argument(Res.GetString("SqlProvider_InvalidDataColumnType", new object[]
			{
				columnName,
				typeName
			}));
		}

		// Token: 0x0600201D RID: 8221 RVA: 0x000DBAF0 File Offset: 0x000DAEF0
		internal static Exception InvalidColumnMaxLength(string columnName, long maxLength)
		{
			return ADP.Argument(Res.GetString("SqlProvider_InvalidDataColumnMaxLength", new object[]
			{
				columnName,
				maxLength
			}));
		}

		// Token: 0x0600201E RID: 8222 RVA: 0x000DBB20 File Offset: 0x000DAF20
		internal static Exception InvalidColumnPrecScale()
		{
			return ADP.Argument(Res.GetString("SqlMisc_InvalidPrecScaleMessage"));
		}

		// Token: 0x0600201F RID: 8223 RVA: 0x000DBB3C File Offset: 0x000DAF3C
		internal static Exception NotEnoughColumnsInStructuredType()
		{
			return ADP.Argument(Res.GetString("SqlProvider_NotEnoughColumnsInStructuredType"));
		}

		// Token: 0x06002020 RID: 8224 RVA: 0x000DBB58 File Offset: 0x000DAF58
		internal static Exception DuplicateSortOrdinal(int sortOrdinal)
		{
			return ADP.InvalidOperation(Res.GetString("SqlProvider_DuplicateSortOrdinal", new object[]
			{
				sortOrdinal
			}));
		}

		// Token: 0x06002021 RID: 8225 RVA: 0x000DBB84 File Offset: 0x000DAF84
		internal static Exception MissingSortOrdinal(int sortOrdinal)
		{
			return ADP.InvalidOperation(Res.GetString("SqlProvider_MissingSortOrdinal", new object[]
			{
				sortOrdinal
			}));
		}

		// Token: 0x06002022 RID: 8226 RVA: 0x000DBBB0 File Offset: 0x000DAFB0
		internal static Exception SortOrdinalGreaterThanFieldCount(int columnOrdinal, int sortOrdinal)
		{
			return ADP.InvalidOperation(Res.GetString("SqlProvider_SortOrdinalGreaterThanFieldCount", new object[]
			{
				sortOrdinal,
				columnOrdinal
			}));
		}

		// Token: 0x06002023 RID: 8227 RVA: 0x000DBBE4 File Offset: 0x000DAFE4
		internal static Exception IEnumerableOfSqlDataRecordHasNoRows()
		{
			return ADP.Argument(Res.GetString("IEnumerableOfSqlDataRecordHasNoRows"));
		}

		// Token: 0x06002024 RID: 8228 RVA: 0x000DBC00 File Offset: 0x000DB000
		internal static Exception SqlPipeCommandHookedUpToNonContextConnection()
		{
			return ADP.InvalidOperation(Res.GetString("SqlPipe_CommandHookedUpToNonContextConnection"));
		}

		// Token: 0x06002025 RID: 8229 RVA: 0x000DBC1C File Offset: 0x000DB01C
		internal static Exception SqlPipeMessageTooLong(int messageLength)
		{
			return ADP.Argument(Res.GetString("SqlPipe_MessageTooLong", new object[]
			{
				messageLength
			}));
		}

		// Token: 0x06002026 RID: 8230 RVA: 0x000DBC48 File Offset: 0x000DB048
		internal static Exception SqlPipeIsBusy()
		{
			return ADP.InvalidOperation(Res.GetString("SqlPipe_IsBusy"));
		}

		// Token: 0x06002027 RID: 8231 RVA: 0x000DBC64 File Offset: 0x000DB064
		internal static Exception SqlPipeAlreadyHasAnOpenResultSet(string methodName)
		{
			return ADP.InvalidOperation(Res.GetString("SqlPipe_AlreadyHasAnOpenResultSet", new object[]
			{
				methodName
			}));
		}

		// Token: 0x06002028 RID: 8232 RVA: 0x000DBC8C File Offset: 0x000DB08C
		internal static Exception SqlPipeDoesNotHaveAnOpenResultSet(string methodName)
		{
			return ADP.InvalidOperation(Res.GetString("SqlPipe_DoesNotHaveAnOpenResultSet", new object[]
			{
				methodName
			}));
		}

		// Token: 0x06002029 RID: 8233 RVA: 0x000DBCB4 File Offset: 0x000DB0B4
		internal static Exception SqlResultSetClosed(string methodname)
		{
			if (methodname == null)
			{
				return ADP.InvalidOperation(Res.GetString("SQL_SqlResultSetClosed2"));
			}
			return ADP.InvalidOperation(Res.GetString("SQL_SqlResultSetClosed", new object[]
			{
				methodname
			}));
		}

		// Token: 0x0600202A RID: 8234 RVA: 0x000DBCF0 File Offset: 0x000DB0F0
		internal static Exception SqlResultSetNoData(string methodname)
		{
			return ADP.InvalidOperation(Res.GetString("ADP_DataReaderNoData", new object[]
			{
				methodname
			}));
		}

		// Token: 0x0600202B RID: 8235 RVA: 0x000DBD18 File Offset: 0x000DB118
		internal static Exception SqlRecordReadOnly(string methodname)
		{
			if (methodname == null)
			{
				return ADP.InvalidOperation(Res.GetString("SQL_SqlRecordReadOnly2"));
			}
			return ADP.InvalidOperation(Res.GetString("SQL_SqlRecordReadOnly", new object[]
			{
				methodname
			}));
		}

		// Token: 0x0600202C RID: 8236 RVA: 0x000DBD54 File Offset: 0x000DB154
		internal static Exception SqlResultSetRowDeleted(string methodname)
		{
			if (methodname == null)
			{
				return ADP.InvalidOperation(Res.GetString("SQL_SqlResultSetRowDeleted2"));
			}
			return ADP.InvalidOperation(Res.GetString("SQL_SqlResultSetRowDeleted", new object[]
			{
				methodname
			}));
		}

		// Token: 0x0600202D RID: 8237 RVA: 0x000DBD90 File Offset: 0x000DB190
		internal static Exception SqlResultSetCommandNotInSameConnection()
		{
			return ADP.InvalidOperation(Res.GetString("SQL_SqlResultSetCommandNotInSameConnection"));
		}

		// Token: 0x0600202E RID: 8238 RVA: 0x000DBDAC File Offset: 0x000DB1AC
		internal static Exception SqlResultSetNoAcceptableCursor()
		{
			return ADP.InvalidOperation(Res.GetString("SQL_SqlResultSetNoAcceptableCursor"));
		}

		// Token: 0x0600202F RID: 8239 RVA: 0x000DBDC8 File Offset: 0x000DB1C8
		internal static Exception BulkLoadMappingInaccessible()
		{
			return ADP.InvalidOperation(Res.GetString("SQL_BulkLoadMappingInaccessible"));
		}

		// Token: 0x06002030 RID: 8240 RVA: 0x000DBDE4 File Offset: 0x000DB1E4
		internal static Exception BulkLoadMappingsNamesOrOrdinalsOnly()
		{
			return ADP.InvalidOperation(Res.GetString("SQL_BulkLoadMappingsNamesOrOrdinalsOnly"));
		}

		// Token: 0x06002031 RID: 8241 RVA: 0x000DBE00 File Offset: 0x000DB200
		internal static Exception BulkLoadCannotConvertValue(Type sourcetype, MetaType metatype, Exception e)
		{
			return ADP.InvalidOperation(Res.GetString("SQL_BulkLoadCannotConvertValue", new object[]
			{
				sourcetype.Name,
				metatype.TypeName
			}), e);
		}

		// Token: 0x06002032 RID: 8242 RVA: 0x000DBE38 File Offset: 0x000DB238
		internal static Exception BulkLoadNonMatchingColumnMapping()
		{
			return ADP.InvalidOperation(Res.GetString("SQL_BulkLoadNonMatchingColumnMapping"));
		}

		// Token: 0x06002033 RID: 8243 RVA: 0x000DBE54 File Offset: 0x000DB254
		internal static Exception BulkLoadNonMatchingColumnName(string columnName)
		{
			return SQL.BulkLoadNonMatchingColumnName(columnName, null);
		}

		// Token: 0x06002034 RID: 8244 RVA: 0x000DBE68 File Offset: 0x000DB268
		internal static Exception BulkLoadNonMatchingColumnName(string columnName, Exception e)
		{
			return ADP.InvalidOperation(Res.GetString("SQL_BulkLoadNonMatchingColumnName", new object[]
			{
				columnName
			}), e);
		}

		// Token: 0x06002035 RID: 8245 RVA: 0x000DBE90 File Offset: 0x000DB290
		internal static Exception BulkLoadStringTooLong()
		{
			return ADP.InvalidOperation(Res.GetString("SQL_BulkLoadStringTooLong"));
		}

		// Token: 0x06002036 RID: 8246 RVA: 0x000DBEAC File Offset: 0x000DB2AC
		internal static Exception BulkLoadInvalidVariantValue()
		{
			return ADP.InvalidOperation(Res.GetString("SQL_BulkLoadInvalidVariantValue"));
		}

		// Token: 0x06002037 RID: 8247 RVA: 0x000DBEC8 File Offset: 0x000DB2C8
		internal static Exception BulkLoadInvalidTimeout(int timeout)
		{
			return ADP.Argument(Res.GetString("SQL_BulkLoadInvalidTimeout", new object[]
			{
				timeout.ToString(CultureInfo.InvariantCulture)
			}));
		}

		// Token: 0x06002038 RID: 8248 RVA: 0x000DBEFC File Offset: 0x000DB2FC
		internal static Exception BulkLoadExistingTransaction()
		{
			return ADP.InvalidOperation(Res.GetString("SQL_BulkLoadExistingTransaction"));
		}

		// Token: 0x06002039 RID: 8249 RVA: 0x000DBF18 File Offset: 0x000DB318
		internal static Exception BulkLoadNoCollation()
		{
			return ADP.InvalidOperation(Res.GetString("SQL_BulkLoadNoCollation"));
		}

		// Token: 0x0600203A RID: 8250 RVA: 0x000DBF34 File Offset: 0x000DB334
		internal static Exception BulkLoadConflictingTransactionOption()
		{
			return ADP.Argument(Res.GetString("SQL_BulkLoadConflictingTransactionOption"));
		}

		// Token: 0x0600203B RID: 8251 RVA: 0x000DBF50 File Offset: 0x000DB350
		internal static Exception BulkLoadLcidMismatch(int sourceLcid, string sourceColumnName, int destinationLcid, string destinationColumnName)
		{
			return ADP.InvalidOperation(Res.GetString("Sql_BulkLoadLcidMismatch", new object[]
			{
				sourceLcid,
				sourceColumnName,
				destinationLcid,
				destinationColumnName
			}));
		}

		// Token: 0x0600203C RID: 8252 RVA: 0x000DBF8C File Offset: 0x000DB38C
		internal static Exception InvalidOperationInsideEvent()
		{
			return ADP.InvalidOperation(Res.GetString("SQL_BulkLoadInvalidOperationInsideEvent"));
		}

		// Token: 0x0600203D RID: 8253 RVA: 0x000DBFA8 File Offset: 0x000DB3A8
		internal static Exception BulkLoadMissingDestinationTable()
		{
			return ADP.InvalidOperation(Res.GetString("SQL_BulkLoadMissingDestinationTable"));
		}

		// Token: 0x0600203E RID: 8254 RVA: 0x000DBFC4 File Offset: 0x000DB3C4
		internal static Exception BulkLoadInvalidDestinationTable(string tableName, Exception inner)
		{
			return ADP.InvalidOperation(Res.GetString("SQL_BulkLoadInvalidDestinationTable", new object[]
			{
				tableName
			}), inner);
		}

		// Token: 0x0600203F RID: 8255 RVA: 0x000DBFEC File Offset: 0x000DB3EC
		internal static Exception BulkLoadBulkLoadNotAllowDBNull(string columnName)
		{
			return ADP.InvalidOperation(Res.GetString("SQL_BulkLoadNotAllowDBNull", new object[]
			{
				columnName
			}));
		}

		// Token: 0x06002040 RID: 8256 RVA: 0x000DC014 File Offset: 0x000DB414
		internal static Exception BulkLoadPendingOperation()
		{
			return ADP.InvalidOperation(Res.GetString("SQL_BulkLoadPendingOperation"));
		}

		// Token: 0x06002041 RID: 8257 RVA: 0x000DC030 File Offset: 0x000DB430
		internal static Exception InvalidKeyEncryptionAlgorithm(string encryptionAlgorithm, string validEncryptionAlgorithm, bool isSystemOp)
		{
			if (isSystemOp)
			{
				return ADP.Argument(Res.GetString("TCE_InvalidKeyEncryptionAlgorithmSysErr", new object[]
				{
					encryptionAlgorithm,
					validEncryptionAlgorithm
				}), "encryptionAlgorithm");
			}
			return ADP.Argument(Res.GetString("TCE_InvalidKeyEncryptionAlgorithm", new object[]
			{
				encryptionAlgorithm,
				validEncryptionAlgorithm
			}), "encryptionAlgorithm");
		}

		// Token: 0x06002042 RID: 8258 RVA: 0x000DC088 File Offset: 0x000DB488
		internal static Exception NullKeyEncryptionAlgorithm(bool isSystemOp)
		{
			if (isSystemOp)
			{
				return ADP.ArgumentNull("encryptionAlgorithm", Res.GetString("TCE_NullKeyEncryptionAlgorithmSysErr"));
			}
			return ADP.ArgumentNull("encryptionAlgorithm", Res.GetString("TCE_NullKeyEncryptionAlgorithm"));
		}

		// Token: 0x06002043 RID: 8259 RVA: 0x000DC0C4 File Offset: 0x000DB4C4
		internal static Exception EmptyColumnEncryptionKey()
		{
			return ADP.Argument(Res.GetString("TCE_EmptyColumnEncryptionKey"), "columnEncryptionKey");
		}

		// Token: 0x06002044 RID: 8260 RVA: 0x000DC0E8 File Offset: 0x000DB4E8
		internal static Exception NullColumnEncryptionKey()
		{
			return ADP.ArgumentNull("columnEncryptionKey", Res.GetString("TCE_NullColumnEncryptionKey"));
		}

		// Token: 0x06002045 RID: 8261 RVA: 0x000DC10C File Offset: 0x000DB50C
		internal static Exception EmptyEncryptedColumnEncryptionKey()
		{
			return ADP.Argument(Res.GetString("TCE_EmptyEncryptedColumnEncryptionKey"), "encryptedColumnEncryptionKey");
		}

		// Token: 0x06002046 RID: 8262 RVA: 0x000DC130 File Offset: 0x000DB530
		internal static Exception NullEncryptedColumnEncryptionKey()
		{
			return ADP.ArgumentNull("encryptedColumnEncryptionKey", Res.GetString("TCE_NullEncryptedColumnEncryptionKey"));
		}

		// Token: 0x06002047 RID: 8263 RVA: 0x000DC154 File Offset: 0x000DB554
		internal static Exception LargeCertificatePathLength(int actualLength, int maxLength, bool isSystemOp)
		{
			if (isSystemOp)
			{
				return ADP.Argument(Res.GetString("TCE_LargeCertificatePathLengthSysErr", new object[]
				{
					actualLength,
					maxLength
				}), "masterKeyPath");
			}
			return ADP.Argument(Res.GetString("TCE_LargeCertificatePathLength", new object[]
			{
				actualLength,
				maxLength
			}), "masterKeyPath");
		}

		// Token: 0x06002048 RID: 8264 RVA: 0x000DC1C0 File Offset: 0x000DB5C0
		internal static Exception NullCertificatePath(string[] validLocations, bool isSystemOp)
		{
			if (isSystemOp)
			{
				return ADP.ArgumentNull("masterKeyPath", Res.GetString("TCE_NullCertificatePathSysErr", new object[]
				{
					validLocations[0],
					validLocations[1],
					"/"
				}));
			}
			return ADP.ArgumentNull("masterKeyPath", Res.GetString("TCE_NullCertificatePath", new object[]
			{
				validLocations[0],
				validLocations[1],
				"/"
			}));
		}

		// Token: 0x06002049 RID: 8265 RVA: 0x000DC230 File Offset: 0x000DB630
		internal static Exception NullCspKeyPath(bool isSystemOp)
		{
			if (isSystemOp)
			{
				return ADP.ArgumentNull("masterKeyPath", Res.GetString("TCE_NullCspPathSysErr", new object[]
				{
					"/"
				}));
			}
			return ADP.ArgumentNull("masterKeyPath", Res.GetString("TCE_NullCspPath", new object[]
			{
				"/"
			}));
		}

		// Token: 0x0600204A RID: 8266 RVA: 0x000DC288 File Offset: 0x000DB688
		internal static Exception NullCngKeyPath(bool isSystemOp)
		{
			if (isSystemOp)
			{
				return ADP.ArgumentNull("masterKeyPath", Res.GetString("TCE_NullCngPathSysErr", new object[]
				{
					"/"
				}));
			}
			return ADP.ArgumentNull("masterKeyPath", Res.GetString("TCE_NullCngPath", new object[]
			{
				"/"
			}));
		}

		// Token: 0x0600204B RID: 8267 RVA: 0x000DC2E0 File Offset: 0x000DB6E0
		internal static Exception InvalidCertificatePath(string actualCertificatePath, string[] validLocations, bool isSystemOp)
		{
			if (isSystemOp)
			{
				return ADP.Argument(Res.GetString("TCE_InvalidCertificatePathSysErr", new object[]
				{
					actualCertificatePath,
					validLocations[0],
					validLocations[1],
					"/"
				}), "masterKeyPath");
			}
			return ADP.Argument(Res.GetString("TCE_InvalidCertificatePath", new object[]
			{
				actualCertificatePath,
				validLocations[0],
				validLocations[1],
				"/"
			}), "masterKeyPath");
		}

		// Token: 0x0600204C RID: 8268 RVA: 0x000DC358 File Offset: 0x000DB758
		internal static Exception InvalidCspPath(string masterKeyPath, bool isSystemOp)
		{
			if (isSystemOp)
			{
				return ADP.Argument(Res.GetString("TCE_InvalidCspPathSysErr", new object[]
				{
					masterKeyPath,
					"/"
				}), "masterKeyPath");
			}
			return ADP.Argument(Res.GetString("TCE_InvalidCspPath", new object[]
			{
				masterKeyPath,
				"/"
			}), "masterKeyPath");
		}

		// Token: 0x0600204D RID: 8269 RVA: 0x000DC3B8 File Offset: 0x000DB7B8
		internal static Exception InvalidCngPath(string masterKeyPath, bool isSystemOp)
		{
			if (isSystemOp)
			{
				return ADP.Argument(Res.GetString("TCE_InvalidCngPathSysErr", new object[]
				{
					masterKeyPath,
					"/"
				}), "masterKeyPath");
			}
			return ADP.Argument(Res.GetString("TCE_InvalidCngPath", new object[]
			{
				masterKeyPath,
				"/"
			}), "masterKeyPath");
		}

		// Token: 0x0600204E RID: 8270 RVA: 0x000DC418 File Offset: 0x000DB818
		internal static Exception EmptyCspName(string masterKeyPath, bool isSystemOp)
		{
			if (isSystemOp)
			{
				return ADP.Argument(Res.GetString("TCE_EmptyCspNameSysErr", new object[]
				{
					masterKeyPath,
					"/"
				}), "masterKeyPath");
			}
			return ADP.Argument(Res.GetString("TCE_EmptyCspName", new object[]
			{
				masterKeyPath,
				"/"
			}), "masterKeyPath");
		}

		// Token: 0x0600204F RID: 8271 RVA: 0x000DC478 File Offset: 0x000DB878
		internal static Exception EmptyCngName(string masterKeyPath, bool isSystemOp)
		{
			if (isSystemOp)
			{
				return ADP.Argument(Res.GetString("TCE_EmptyCngNameSysErr", new object[]
				{
					masterKeyPath,
					"/"
				}), "masterKeyPath");
			}
			return ADP.Argument(Res.GetString("TCE_EmptyCngName", new object[]
			{
				masterKeyPath,
				"/"
			}), "masterKeyPath");
		}

		// Token: 0x06002050 RID: 8272 RVA: 0x000DC4D8 File Offset: 0x000DB8D8
		internal static Exception EmptyCspKeyId(string masterKeyPath, bool isSystemOp)
		{
			if (isSystemOp)
			{
				return ADP.Argument(Res.GetString("TCE_EmptyCspKeyIdSysErr", new object[]
				{
					masterKeyPath,
					"/"
				}), "masterKeyPath");
			}
			return ADP.Argument(Res.GetString("TCE_EmptyCspKeyId", new object[]
			{
				masterKeyPath,
				"/"
			}), "masterKeyPath");
		}

		// Token: 0x06002051 RID: 8273 RVA: 0x000DC538 File Offset: 0x000DB938
		internal static Exception EmptyCngKeyId(string masterKeyPath, bool isSystemOp)
		{
			if (isSystemOp)
			{
				return ADP.Argument(Res.GetString("TCE_EmptyCngKeyIdSysErr", new object[]
				{
					masterKeyPath,
					"/"
				}), "masterKeyPath");
			}
			return ADP.Argument(Res.GetString("TCE_EmptyCngKeyId", new object[]
			{
				masterKeyPath,
				"/"
			}), "masterKeyPath");
		}

		// Token: 0x06002052 RID: 8274 RVA: 0x000DC598 File Offset: 0x000DB998
		internal static Exception InvalidCspName(string cspName, string masterKeyPath, bool isSystemOp)
		{
			if (isSystemOp)
			{
				return ADP.Argument(Res.GetString("TCE_InvalidCspNameSysErr", new object[]
				{
					cspName,
					masterKeyPath
				}), "masterKeyPath");
			}
			return ADP.Argument(Res.GetString("TCE_InvalidCspName", new object[]
			{
				cspName,
				masterKeyPath
			}), "masterKeyPath");
		}

		// Token: 0x06002053 RID: 8275 RVA: 0x000DC5F0 File Offset: 0x000DB9F0
		internal static Exception InvalidCspKeyIdentifier(string keyIdentifier, string masterKeyPath, bool isSystemOp)
		{
			if (isSystemOp)
			{
				return ADP.Argument(Res.GetString("TCE_InvalidCspKeyIdSysErr", new object[]
				{
					keyIdentifier,
					masterKeyPath
				}), "masterKeyPath");
			}
			return ADP.Argument(Res.GetString("TCE_InvalidCspKeyId", new object[]
			{
				keyIdentifier,
				masterKeyPath
			}), "masterKeyPath");
		}

		// Token: 0x06002054 RID: 8276 RVA: 0x000DC648 File Offset: 0x000DBA48
		internal static Exception InvalidCngKey(string masterKeyPath, string cngProviderName, string keyIdentifier, bool isSystemOp)
		{
			if (isSystemOp)
			{
				return ADP.Argument(Res.GetString("TCE_InvalidCngKeySysErr", new object[]
				{
					masterKeyPath,
					cngProviderName,
					keyIdentifier
				}), "masterKeyPath");
			}
			return ADP.Argument(Res.GetString("TCE_InvalidCngKey", new object[]
			{
				masterKeyPath,
				cngProviderName,
				keyIdentifier
			}), "masterKeyPath");
		}

		// Token: 0x06002055 RID: 8277 RVA: 0x000DC6A8 File Offset: 0x000DBAA8
		internal static Exception InvalidCertificateLocation(string certificateLocation, string certificatePath, string[] validLocations, bool isSystemOp)
		{
			if (isSystemOp)
			{
				return ADP.Argument(Res.GetString("TCE_InvalidCertificateLocationSysErr", new object[]
				{
					certificateLocation,
					certificatePath,
					validLocations[0],
					validLocations[1],
					"/"
				}), "masterKeyPath");
			}
			return ADP.Argument(Res.GetString("TCE_InvalidCertificateLocation", new object[]
			{
				certificateLocation,
				certificatePath,
				validLocations[0],
				validLocations[1],
				"/"
			}), "masterKeyPath");
		}

		// Token: 0x06002056 RID: 8278 RVA: 0x000DC728 File Offset: 0x000DBB28
		internal static Exception InvalidCertificateStore(string certificateStore, string certificatePath, string validCertificateStore, bool isSystemOp)
		{
			if (isSystemOp)
			{
				return ADP.Argument(Res.GetString("TCE_InvalidCertificateStoreSysErr", new object[]
				{
					certificateStore,
					certificatePath,
					validCertificateStore
				}), "masterKeyPath");
			}
			return ADP.Argument(Res.GetString("TCE_InvalidCertificateStore", new object[]
			{
				certificateStore,
				certificatePath,
				validCertificateStore
			}), "masterKeyPath");
		}

		// Token: 0x06002057 RID: 8279 RVA: 0x000DC788 File Offset: 0x000DBB88
		internal static Exception EmptyCertificateThumbprint(string certificatePath, bool isSystemOp)
		{
			if (isSystemOp)
			{
				return ADP.Argument(Res.GetString("TCE_EmptyCertificateThumbprintSysErr", new object[]
				{
					certificatePath
				}), "masterKeyPath");
			}
			return ADP.Argument(Res.GetString("TCE_EmptyCertificateThumbprint", new object[]
			{
				certificatePath
			}), "masterKeyPath");
		}

		// Token: 0x06002058 RID: 8280 RVA: 0x000DC7D8 File Offset: 0x000DBBD8
		internal static Exception CertificateNotFound(string thumbprint, string certificateLocation, string certificateStore, bool isSystemOp)
		{
			if (isSystemOp)
			{
				return ADP.Argument(Res.GetString("TCE_CertificateNotFoundSysErr", new object[]
				{
					thumbprint,
					certificateLocation,
					certificateStore
				}), "masterKeyPath");
			}
			return ADP.Argument(Res.GetString("TCE_CertificateNotFound", new object[]
			{
				thumbprint,
				certificateLocation,
				certificateStore
			}), "masterKeyPath");
		}

		// Token: 0x06002059 RID: 8281 RVA: 0x000DC838 File Offset: 0x000DBC38
		internal static Exception InvalidAlgorithmVersionInEncryptedCEK(byte actual, byte expected)
		{
			return ADP.Argument(Res.GetString("TCE_InvalidAlgorithmVersionInEncryptedCEK", new object[]
			{
				actual.ToString("X2"),
				expected.ToString("X2")
			}), "encryptedColumnEncryptionKey");
		}

		// Token: 0x0600205A RID: 8282 RVA: 0x000DC880 File Offset: 0x000DBC80
		internal static Exception InvalidCiphertextLengthInEncryptedCEK(int actual, int expected, string certificateName)
		{
			return ADP.Argument(Res.GetString("TCE_InvalidCiphertextLengthInEncryptedCEK", new object[]
			{
				actual,
				expected,
				certificateName
			}), "encryptedColumnEncryptionKey");
		}

		// Token: 0x0600205B RID: 8283 RVA: 0x000DC8C0 File Offset: 0x000DBCC0
		internal static Exception InvalidCiphertextLengthInEncryptedCEKCsp(int actual, int expected, string masterKeyPath)
		{
			return ADP.Argument(Res.GetString("TCE_InvalidCiphertextLengthInEncryptedCEKCsp", new object[]
			{
				actual,
				expected,
				masterKeyPath
			}), "encryptedColumnEncryptionKey");
		}

		// Token: 0x0600205C RID: 8284 RVA: 0x000DC900 File Offset: 0x000DBD00
		internal static Exception InvalidCiphertextLengthInEncryptedCEKCng(int actual, int expected, string masterKeyPath)
		{
			return ADP.Argument(Res.GetString("TCE_InvalidCiphertextLengthInEncryptedCEKCng", new object[]
			{
				actual,
				expected,
				masterKeyPath
			}), "encryptedColumnEncryptionKey");
		}

		// Token: 0x0600205D RID: 8285 RVA: 0x000DC940 File Offset: 0x000DBD40
		internal static Exception InvalidSignatureInEncryptedCEK(int actual, int expected, string masterKeyPath)
		{
			return ADP.Argument(Res.GetString("TCE_InvalidSignatureInEncryptedCEK", new object[]
			{
				actual,
				expected,
				masterKeyPath
			}), "encryptedColumnEncryptionKey");
		}

		// Token: 0x0600205E RID: 8286 RVA: 0x000DC980 File Offset: 0x000DBD80
		internal static Exception InvalidSignatureInEncryptedCEKCsp(int actual, int expected, string masterKeyPath)
		{
			return ADP.Argument(Res.GetString("TCE_InvalidSignatureInEncryptedCEKCsp", new object[]
			{
				actual,
				expected,
				masterKeyPath
			}), "encryptedColumnEncryptionKey");
		}

		// Token: 0x0600205F RID: 8287 RVA: 0x000DC9C0 File Offset: 0x000DBDC0
		internal static Exception InvalidSignatureInEncryptedCEKCng(int actual, int expected, string masterKeyPath)
		{
			return ADP.Argument(Res.GetString("TCE_InvalidSignatureInEncryptedCEKCng", new object[]
			{
				actual,
				expected,
				masterKeyPath
			}), "encryptedColumnEncryptionKey");
		}

		// Token: 0x06002060 RID: 8288 RVA: 0x000DCA00 File Offset: 0x000DBE00
		internal static Exception InvalidCertificateSignature(string certificatePath)
		{
			return ADP.Argument(Res.GetString("TCE_InvalidCertificateSignature", new object[]
			{
				certificatePath
			}), "encryptedColumnEncryptionKey");
		}

		// Token: 0x06002061 RID: 8289 RVA: 0x000DCA2C File Offset: 0x000DBE2C
		internal static Exception InvalidSignature(string masterKeyPath)
		{
			return ADP.Argument(Res.GetString("TCE_InvalidSignature", new object[]
			{
				masterKeyPath
			}), "encryptedColumnEncryptionKey");
		}

		// Token: 0x06002062 RID: 8290 RVA: 0x000DCA58 File Offset: 0x000DBE58
		internal static Exception CertificateWithNoPrivateKey(string keyPath, bool isSystemOp)
		{
			if (isSystemOp)
			{
				return ADP.Argument(Res.GetString("TCE_CertificateWithNoPrivateKeySysErr", new object[]
				{
					keyPath
				}), "masterKeyPath");
			}
			return ADP.Argument(Res.GetString("TCE_CertificateWithNoPrivateKey", new object[]
			{
				keyPath
			}), "masterKeyPath");
		}

		// Token: 0x06002063 RID: 8291 RVA: 0x000DCAA8 File Offset: 0x000DBEA8
		internal static Exception NullColumnEncryptionKeySysErr()
		{
			return ADP.ArgumentNull("encryptionKey", Res.GetString("TCE_NullColumnEncryptionKeySysErr"));
		}

		// Token: 0x06002064 RID: 8292 RVA: 0x000DCACC File Offset: 0x000DBECC
		internal static Exception InvalidKeySize(string algorithmName, int actualKeylength, int expectedLength)
		{
			return ADP.Argument(Res.GetString("TCE_InvalidKeySize", new object[]
			{
				algorithmName,
				actualKeylength,
				expectedLength
			}), "encryptionKey");
		}

		// Token: 0x06002065 RID: 8293 RVA: 0x000DCB0C File Offset: 0x000DBF0C
		internal static Exception InvalidEncryptionType(string algorithmName, SqlClientEncryptionType encryptionType, params SqlClientEncryptionType[] validEncryptionTypes)
		{
			string name = "TCE_InvalidEncryptionType";
			object[] array = new object[3];
			array[0] = algorithmName;
			array[1] = encryptionType.ToString();
			array[2] = string.Join(", ", from validEncryptionType in validEncryptionTypes
			select "'" + validEncryptionType.ToString() + "'");
			return ADP.Argument(Res.GetString(name, array), "encryptionType");
		}

		// Token: 0x06002066 RID: 8294 RVA: 0x000DCB7C File Offset: 0x000DBF7C
		internal static Exception NullPlainText()
		{
			return ADP.ArgumentNull(Res.GetString("TCE_NullPlainText"));
		}

		// Token: 0x06002067 RID: 8295 RVA: 0x000DCB98 File Offset: 0x000DBF98
		internal static Exception VeryLargeCiphertext(long cipherTextLength, long maxCipherTextSize, long plainTextLength)
		{
			return ADP.Argument(Res.GetString("TCE_VeryLargeCiphertext", new object[]
			{
				cipherTextLength,
				maxCipherTextSize,
				plainTextLength
			}));
		}

		// Token: 0x06002068 RID: 8296 RVA: 0x000DCBD8 File Offset: 0x000DBFD8
		internal static Exception NullCipherText()
		{
			return ADP.ArgumentNull(Res.GetString("TCE_NullCipherText"));
		}

		// Token: 0x06002069 RID: 8297 RVA: 0x000DCBF4 File Offset: 0x000DBFF4
		internal static Exception InvalidCipherTextSize(int actualSize, int minimumSize)
		{
			return ADP.Argument(Res.GetString("TCE_InvalidCipherTextSize", new object[]
			{
				actualSize,
				minimumSize
			}), "cipherText");
		}

		// Token: 0x0600206A RID: 8298 RVA: 0x000DCC30 File Offset: 0x000DC030
		internal static Exception InvalidAlgorithmVersion(byte actual, byte expected)
		{
			return ADP.Argument(Res.GetString("TCE_InvalidAlgorithmVersion", new object[]
			{
				actual.ToString("X2"),
				expected.ToString("X2")
			}), "cipherText");
		}

		// Token: 0x0600206B RID: 8299 RVA: 0x000DCC78 File Offset: 0x000DC078
		internal static Exception InvalidAuthenticationTag()
		{
			return ADP.Argument(Res.GetString("TCE_InvalidAuthenticationTag"), "cipherText");
		}

		// Token: 0x0600206C RID: 8300 RVA: 0x000DCC9C File Offset: 0x000DC09C
		internal static Exception NullColumnEncryptionAlgorithm(string supportedAlgorithms)
		{
			return ADP.ArgumentNull("encryptionAlgorithm", Res.GetString("TCE_NullColumnEncryptionAlgorithm", new object[]
			{
				supportedAlgorithms
			}));
		}

		// Token: 0x0600206D RID: 8301 RVA: 0x000DCCC8 File Offset: 0x000DC0C8
		internal static Exception UnexpectedDescribeParamFormatParameterMetadata()
		{
			return ADP.Argument(Res.GetString("TCE_UnexpectedDescribeParamFormatParameterMetadata", new object[]
			{
				"sp_describe_parameter_encryption"
			}));
		}

		// Token: 0x0600206E RID: 8302 RVA: 0x000DCCF4 File Offset: 0x000DC0F4
		internal static Exception UnexpectedDescribeParamFormatAttestationInfo(string enclaveType)
		{
			return ADP.Argument(Res.GetString("TCE_UnexpectedDescribeParamFormatAttestationInfo", new object[]
			{
				"sp_describe_parameter_encryption",
				enclaveType
			}));
		}

		// Token: 0x0600206F RID: 8303 RVA: 0x000DCD24 File Offset: 0x000DC124
		internal static Exception InvalidEncryptionKeyOrdinalEnclaveMetadata(int ordinal, int maxOrdinal)
		{
			return ADP.InvalidOperation(Res.GetString("TCE_InvalidEncryptionKeyOrdinalEnclaveMetadata", new object[]
			{
				ordinal,
				maxOrdinal
			}));
		}

		// Token: 0x06002070 RID: 8304 RVA: 0x000DCD58 File Offset: 0x000DC158
		internal static Exception InvalidEncryptionKeyOrdinalParameterMetadata(int ordinal, int maxOrdinal)
		{
			return ADP.InvalidOperation(Res.GetString("TCE_InvalidEncryptionKeyOrdinalParameterMetadata", new object[]
			{
				ordinal,
				maxOrdinal
			}));
		}

		// Token: 0x06002071 RID: 8305 RVA: 0x000DCD8C File Offset: 0x000DC18C
		public static Exception MultipleRowsReturnedForAttestationInfo()
		{
			return ADP.InvalidOperation(Res.GetString("TCE_MultipleRowsReturnedForAttestationInfo", new object[]
			{
				"sp_describe_parameter_encryption"
			}));
		}

		// Token: 0x06002072 RID: 8306 RVA: 0x000DCDB8 File Offset: 0x000DC1B8
		internal static Exception ParamEncryptionMetadataMissing(string paramName, string procedureName)
		{
			return ADP.Argument(Res.GetString("TCE_ParamEncryptionMetaDataMissing", new object[]
			{
				"sp_describe_parameter_encryption",
				paramName,
				procedureName
			}));
		}

		// Token: 0x06002073 RID: 8307 RVA: 0x000DCDEC File Offset: 0x000DC1EC
		internal static Exception ParamInvalidForceColumnEncryptionSetting(string paramName, string procedureName)
		{
			return ADP.InvalidOperation(Res.GetString("TCE_ParamInvalidForceColumnEncryptionSetting", new object[]
			{
				"ForceColumnEncryption(true)",
				paramName,
				procedureName,
				"SqlParameter"
			}));
		}

		// Token: 0x06002074 RID: 8308 RVA: 0x000DCE28 File Offset: 0x000DC228
		internal static Exception ParamUnExpectedEncryptionMetadata(string paramName, string procedureName)
		{
			return ADP.InvalidOperation(Res.GetString("TCE_ParamUnExpectedEncryptionMetadata", new object[]
			{
				paramName,
				procedureName,
				"ForceColumnEncryption(true)",
				"SqlParameter"
			}));
		}

		// Token: 0x06002075 RID: 8309 RVA: 0x000DCE64 File Offset: 0x000DC264
		internal static Exception ProcEncryptionMetadataMissing(string procedureName)
		{
			return ADP.Argument(Res.GetString("TCE_ProcEncryptionMetaDataMissing", new object[]
			{
				"sp_describe_parameter_encryption",
				procedureName
			}));
		}

		// Token: 0x06002076 RID: 8310 RVA: 0x000DCE94 File Offset: 0x000DC294
		internal static Exception InvalidKeyStoreProviderName(string providerName, List<string> systemProviders, List<string> customProviders)
		{
			string text = string.Join(", ", from provider in systemProviders
			select "'" + provider + "'");
			string text2 = string.Join(", ", from provider in customProviders
			select "'" + provider + "'");
			return ADP.Argument(Res.GetString("TCE_InvalidKeyStoreProviderName", new object[]
			{
				providerName,
				text,
				text2
			}));
		}

		// Token: 0x06002077 RID: 8311 RVA: 0x000DCF24 File Offset: 0x000DC324
		internal static Exception UnableToVerifyColumnMasterKeySignature(Exception innerExeption)
		{
			return ADP.InvalidOperation(Res.GetString("TCE_UnableToVerifyColumnMasterKeySignature", new object[]
			{
				innerExeption.Message
			}), innerExeption);
		}

		// Token: 0x06002078 RID: 8312 RVA: 0x000DCF50 File Offset: 0x000DC350
		internal static Exception ColumnMasterKeySignatureVerificationFailed(string cmkPath)
		{
			return ADP.InvalidOperation(Res.GetString("TCE_ColumnMasterKeySignatureVerificationFailed", new object[]
			{
				cmkPath
			}));
		}

		// Token: 0x06002079 RID: 8313 RVA: 0x000DCF78 File Offset: 0x000DC378
		internal static Exception ColumnMasterKeySignatureNotFound(string cmkPath)
		{
			return ADP.Argument(Res.GetString("TCE_ColumnMasterKeySignatureNotFound", new object[]
			{
				cmkPath
			}));
		}

		// Token: 0x0600207A RID: 8314 RVA: 0x000DCFA0 File Offset: 0x000DC3A0
		internal static Exception ExceptionWhenGeneratingEnclavePackage(Exception innerExeption)
		{
			return ADP.InvalidOperation(Res.GetString("TCE_ExceptionWhenGeneratingEnclavePackage", new object[]
			{
				innerExeption.Message
			}), innerExeption);
		}

		// Token: 0x0600207B RID: 8315 RVA: 0x000DCFCC File Offset: 0x000DC3CC
		internal static Exception FailedToEncryptRegisterRulesBytePackage(Exception innerExeption)
		{
			return ADP.InvalidOperation(Res.GetString("TCE_FailedToEncryptRegisterRulesBytePackage", new object[]
			{
				innerExeption.Message
			}), innerExeption);
		}

		// Token: 0x0600207C RID: 8316 RVA: 0x000DCFF8 File Offset: 0x000DC3F8
		internal static Exception InvalidKeyIdUnableToCastToUnsignedShort(int keyId, Exception innerException)
		{
			return ADP.Argument(Res.GetString("TCE_InvalidKeyIdUnableToCastToUnsignedShort", new object[]
			{
				keyId,
				innerException.Message
			}), innerException);
		}

		// Token: 0x0600207D RID: 8317 RVA: 0x000DD030 File Offset: 0x000DC430
		internal static Exception InvalidDatabaseIdUnableToCastToUnsignedInt(int databaseId, Exception innerException)
		{
			return ADP.Argument(Res.GetString("TCE_InvalidDatabaseIdUnableToCastToUnsignedInt", new object[]
			{
				databaseId,
				innerException.Message
			}), innerException);
		}

		// Token: 0x0600207E RID: 8318 RVA: 0x000DD068 File Offset: 0x000DC468
		internal static Exception InvalidAttestationParameterUnableToConvertToUnsignedInt(string variableName, int intValue, string enclaveType, Exception innerException)
		{
			return ADP.Argument(Res.GetString("TCE_InvalidAttestationParameterUnableToConvertToUnsignedInt", new object[]
			{
				enclaveType,
				intValue,
				variableName,
				innerException.Message
			}), innerException);
		}

		// Token: 0x0600207F RID: 8319 RVA: 0x000DD0A8 File Offset: 0x000DC4A8
		internal static Exception OffsetOutOfBounds(string argument, string type, string method)
		{
			return ADP.Argument(Res.GetString("TCE_OffsetOutOfBounds", new object[]
			{
				type,
				method
			}));
		}

		// Token: 0x06002080 RID: 8320 RVA: 0x000DD0D4 File Offset: 0x000DC4D4
		internal static Exception InsufficientBuffer(string argument, string type, string method)
		{
			return ADP.Argument(Res.GetString("TCE_InsufficientBuffer", new object[]
			{
				argument,
				type,
				method
			}));
		}

		// Token: 0x06002081 RID: 8321 RVA: 0x000DD104 File Offset: 0x000DC504
		internal static Exception ColumnEncryptionKeysNotFound()
		{
			return ADP.Argument(Res.GetString("TCE_ColumnEncryptionKeysNotFound"));
		}

		// Token: 0x06002082 RID: 8322 RVA: 0x000DD120 File Offset: 0x000DC520
		internal static Exception AttestationInfoNotReturnedFromSqlServer(string enclaveType, string enclaveAttestationUrl)
		{
			return ADP.Argument(Res.GetString("TCE_AttestationInfoNotReturnedFromSQLServer", new object[]
			{
				enclaveType,
				enclaveAttestationUrl
			}));
		}

		// Token: 0x06002083 RID: 8323 RVA: 0x000DD14C File Offset: 0x000DC54C
		internal static Exception NullArgumentInConstructorInternal(string argumentName, string objectUnderConstruction)
		{
			return ADP.ArgumentNull(argumentName, Res.GetString("TCE_NullArgumentInConstructorInternal", new object[]
			{
				argumentName,
				objectUnderConstruction
			}));
		}

		// Token: 0x06002084 RID: 8324 RVA: 0x000DD178 File Offset: 0x000DC578
		internal static Exception EmptyArgumentInConstructorInternal(string argumentName, string objectUnderConstruction)
		{
			return ADP.Argument(Res.GetString("TCE_EmptyArgumentInConstructorInternal", new object[]
			{
				argumentName,
				objectUnderConstruction
			}));
		}

		// Token: 0x06002085 RID: 8325 RVA: 0x000DD1A4 File Offset: 0x000DC5A4
		internal static Exception NullArgumentInternal(string argumentName, string type, string method)
		{
			return ADP.ArgumentNull(argumentName, Res.GetString("TCE_NullArgumentInternal", new object[]
			{
				argumentName,
				type,
				method
			}));
		}

		// Token: 0x06002086 RID: 8326 RVA: 0x000DD1D4 File Offset: 0x000DC5D4
		internal static Exception EmptyArgumentInternal(string argumentName, string type, string method)
		{
			return ADP.Argument(Res.GetString("TCE_EmptyArgumentInternal", new object[]
			{
				argumentName,
				type,
				method
			}));
		}

		// Token: 0x06002087 RID: 8327 RVA: 0x000DD204 File Offset: 0x000DC604
		internal static Exception CannotGetSqlColumnEncryptionEnclaveProviderConfig(Exception innerException)
		{
			return ADP.InvalidOperation(Res.GetString("TCE_CannotGetSqlColumnEncryptionEnclaveProviderConfig", new object[]
			{
				innerException.Message
			}), innerException);
		}

		// Token: 0x06002088 RID: 8328 RVA: 0x000DD230 File Offset: 0x000DC630
		internal static Exception CannotCreateSqlColumnEncryptionEnclaveProvider(string providerName, string type, Exception innerException)
		{
			return ADP.InvalidOperation(Res.GetString("TCE_CannotCreateSqlColumnEncryptionEnclaveProvider", new object[]
			{
				providerName,
				type,
				innerException.Message
			}), innerException);
		}

		// Token: 0x06002089 RID: 8329 RVA: 0x000DD264 File Offset: 0x000DC664
		internal static Exception SqlColumnEncryptionEnclaveProviderNameCannotBeEmpty()
		{
			return ADP.InvalidOperation(Res.GetString("TCE_SqlColumnEncryptionEnclaveProviderNameCannotBeEmpty"));
		}

		// Token: 0x0600208A RID: 8330 RVA: 0x000DD280 File Offset: 0x000DC680
		internal static Exception NoAttestationUrlSpecifiedForEnclaveBasedQuerySpDescribe(string enclaveType)
		{
			return ADP.InvalidOperation(Res.GetString("TCE_NoAttestationUrlSpecifiedForEnclaveBasedQuerySpDescribe", new object[]
			{
				"sp_describe_parameter_encryption",
				enclaveType
			}));
		}

		// Token: 0x0600208B RID: 8331 RVA: 0x000DD2B0 File Offset: 0x000DC6B0
		internal static Exception NoAttestationUrlSpecifiedForEnclaveBasedQueryGeneratingEnclavePackage(string enclaveType)
		{
			return ADP.InvalidOperation(Res.GetString("TCE_NoAttestationUrlSpecifiedForEnclaveBasedQueryGeneratingEnclavePackage", new object[]
			{
				enclaveType
			}));
		}

		// Token: 0x0600208C RID: 8332 RVA: 0x000DD2D8 File Offset: 0x000DC6D8
		internal static Exception EnclaveTypeNullForEnclaveBasedQuery()
		{
			return ADP.InvalidOperation(Res.GetString("TCE_EnclaveTypeNullForEnclaveBasedQuery"));
		}

		// Token: 0x0600208D RID: 8333 RVA: 0x000DD2F4 File Offset: 0x000DC6F4
		internal static Exception EnclaveProvidersNotConfiguredForEnclaveBasedQuery()
		{
			return ADP.InvalidOperation(Res.GetString("TCE_EnclaveProvidersNotConfiguredForEnclaveBasedQuery"));
		}

		// Token: 0x0600208E RID: 8334 RVA: 0x000DD310 File Offset: 0x000DC710
		internal static Exception EnclaveProviderNotFound(string enclaveType)
		{
			return ADP.InvalidOperation(Res.GetString("TCE_EnclaveProviderNotFound", new object[]
			{
				enclaveType
			}));
		}

		// Token: 0x0600208F RID: 8335 RVA: 0x000DD338 File Offset: 0x000DC738
		internal static Exception NullEnclaveSessionReturnedFromProvider(string enclaveType, string attestationUrl)
		{
			return ADP.InvalidOperation(Res.GetString("TCE_NullEnclaveSessionReturnedFromProvider", new object[]
			{
				enclaveType,
				attestationUrl
			}));
		}

		// Token: 0x06002090 RID: 8336 RVA: 0x000DD364 File Offset: 0x000DC764
		internal static Exception GetExceptionArray(string serverName, string errorMessage, Exception e)
		{
			SqlErrorCollection sqlErrorCollection = new SqlErrorCollection();
			Exception innerException = (e.InnerException != null) ? e.InnerException : e;
			sqlErrorCollection.Add(new SqlError(0, 0, 11, serverName, errorMessage, null, 0));
			if (e is SqlException)
			{
				SqlException ex = (SqlException)e;
				SqlErrorCollection errors = ex.Errors;
				for (int i = 0; i < ex.Errors.Count; i++)
				{
					sqlErrorCollection.Add(errors[i]);
				}
			}
			else
			{
				sqlErrorCollection.Add(new SqlError(0, 0, 11, serverName, e.Message, null, 0));
			}
			return SqlException.CreateException(sqlErrorCollection, "", null, innerException);
		}

		// Token: 0x06002091 RID: 8337 RVA: 0x000DD400 File Offset: 0x000DC800
		internal static Exception ParamEncryptionFailed(string paramName, string serverName, Exception e)
		{
			return SQL.GetExceptionArray(serverName, Res.GetString("TCE_ParamEncryptionFailed", new object[]
			{
				paramName
			}), e);
		}

		// Token: 0x06002092 RID: 8338 RVA: 0x000DD428 File Offset: 0x000DC828
		internal static Exception ParamDecryptionFailed(string paramName, string serverName, Exception e)
		{
			return SQL.GetExceptionArray(serverName, Res.GetString("TCE_ParamDecryptionFailed", new object[]
			{
				paramName
			}), e);
		}

		// Token: 0x06002093 RID: 8339 RVA: 0x000DD450 File Offset: 0x000DC850
		internal static Exception ColumnDecryptionFailed(string columnName, string serverName, Exception e)
		{
			return SQL.GetExceptionArray(serverName, Res.GetString("TCE_ColumnDecryptionFailed", new object[]
			{
				columnName
			}), e);
		}

		// Token: 0x06002094 RID: 8340 RVA: 0x000DD478 File Offset: 0x000DC878
		internal static Exception UnknownColumnEncryptionAlgorithm(string algorithmName, string supportedAlgorithms)
		{
			return ADP.Argument(Res.GetString("TCE_UnknownColumnEncryptionAlgorithm", new object[]
			{
				algorithmName,
				supportedAlgorithms
			}));
		}

		// Token: 0x06002095 RID: 8341 RVA: 0x000DD4A4 File Offset: 0x000DC8A4
		internal static Exception UnknownColumnEncryptionAlgorithmId(int algoId, string supportAlgorithmIds)
		{
			return ADP.Argument(Res.GetString("TCE_UnknownColumnEncryptionAlgorithmId", new object[]
			{
				algoId,
				supportAlgorithmIds
			}), "cipherAlgorithmId");
		}

		// Token: 0x06002096 RID: 8342 RVA: 0x000DD4D8 File Offset: 0x000DC8D8
		internal static Exception UnsupportedNormalizationVersion(byte version)
		{
			return ADP.Argument(Res.GetString("TCE_UnsupportedNormalizationVersion", new object[]
			{
				version,
				"'1'",
				"SQL Server"
			}));
		}

		// Token: 0x06002097 RID: 8343 RVA: 0x000DD514 File Offset: 0x000DC914
		internal static Exception UnrecognizedKeyStoreProviderName(string providerName, List<string> systemProviders, List<string> customProviders)
		{
			string text = string.Join(", ", from provider in systemProviders
			select "'" + provider + "'");
			string text2 = string.Join(", ", from provider in customProviders
			select "'" + provider + "'");
			return ADP.Argument(Res.GetString("TCE_UnrecognizedKeyStoreProviderName", new object[]
			{
				providerName,
				text,
				text2
			}));
		}

		// Token: 0x06002098 RID: 8344 RVA: 0x000DD5A4 File Offset: 0x000DC9A4
		internal static Exception InvalidDataTypeForEncryptedParameter(string parameterName, int actualDataType, int expectedDataType)
		{
			return ADP.Argument(Res.GetString("TCE_NullProviderValue", new object[]
			{
				parameterName,
				actualDataType,
				expectedDataType
			}));
		}

		// Token: 0x06002099 RID: 8345 RVA: 0x000DD5DC File Offset: 0x000DC9DC
		internal static Exception KeyDecryptionFailed(string providerName, string keyHex, Exception e)
		{
			if (providerName.Equals("MSSQL_CERTIFICATE_STORE"))
			{
				return SQL.GetExceptionArray(null, Res.GetString("TCE_KeyDecryptionFailedCertStore", new object[]
				{
					providerName,
					keyHex
				}), e);
			}
			return SQL.GetExceptionArray(null, Res.GetString("TCE_KeyDecryptionFailed", new object[]
			{
				providerName,
				keyHex
			}), e);
		}

		// Token: 0x0600209A RID: 8346 RVA: 0x000DD638 File Offset: 0x000DCA38
		internal static Exception UntrustedKeyPath(string keyPath, string serverName)
		{
			return ADP.Argument(Res.GetString("TCE_UntrustedKeyPath", new object[]
			{
				keyPath,
				serverName
			}));
		}

		// Token: 0x0600209B RID: 8347 RVA: 0x000DD664 File Offset: 0x000DCA64
		internal static Exception UnsupportedDatatypeEncryption(string dataType)
		{
			return ADP.Argument(Res.GetString("TCE_UnsupportedDatatype", new object[]
			{
				dataType
			}));
		}

		// Token: 0x0600209C RID: 8348 RVA: 0x000DD68C File Offset: 0x000DCA8C
		internal static Exception ThrowDecryptionFailed(string keyStr, string valStr, Exception e)
		{
			return SQL.GetExceptionArray(null, Res.GetString("TCE_DecryptionFailed", new object[]
			{
				keyStr,
				valStr
			}), e);
		}

		// Token: 0x0600209D RID: 8349 RVA: 0x000DD6B8 File Offset: 0x000DCAB8
		internal static Exception NullEnclaveSessionDuringQueryExecution(string enclaveType, string enclaveAttestationUrl)
		{
			return ADP.Argument(Res.GetString("TCE_NullEnclaveSessionDuringQueryExecution", new object[]
			{
				enclaveType,
				enclaveAttestationUrl
			}));
		}

		// Token: 0x0600209E RID: 8350 RVA: 0x000DD6E4 File Offset: 0x000DCAE4
		internal static Exception NullEnclavePackageForEnclaveBasedQuery(string enclaveType, string enclaveAttestationUrl)
		{
			return ADP.Argument(Res.GetString("TCE_NullEnclavePackageForEnclaveBasedQuery", new object[]
			{
				enclaveType,
				enclaveAttestationUrl
			}));
		}

		// Token: 0x0600209F RID: 8351 RVA: 0x000DD710 File Offset: 0x000DCB10
		internal static Exception TceNotSupported()
		{
			return ADP.InvalidOperation(Res.GetString("TCE_NotSupportedByServer", new object[]
			{
				"SQL Server"
			}));
		}

		// Token: 0x060020A0 RID: 8352 RVA: 0x000DD73C File Offset: 0x000DCB3C
		internal static Exception EnclaveComputationsNotSupported()
		{
			return ADP.InvalidOperation(Res.GetString("TCE_EnclaveComputationsNotSupported"));
		}

		// Token: 0x060020A1 RID: 8353 RVA: 0x000DD758 File Offset: 0x000DCB58
		internal static Exception EnclaveTypeNotReturned()
		{
			return ADP.InvalidOperation(Res.GetString("TCE_EnclaveTypeNotReturned"));
		}

		// Token: 0x060020A2 RID: 8354 RVA: 0x000DD774 File Offset: 0x000DCB74
		internal static Exception CanOnlyCallOnce()
		{
			return ADP.InvalidOperation(Res.GetString("TCE_CanOnlyCallOnce"));
		}

		// Token: 0x060020A3 RID: 8355 RVA: 0x000DD790 File Offset: 0x000DCB90
		internal static Exception NullCustomKeyStoreProviderDictionary()
		{
			return ADP.ArgumentNull("clientKeyStoreProviders", Res.GetString("TCE_NullCustomKeyStoreProviderDictionary"));
		}

		// Token: 0x060020A4 RID: 8356 RVA: 0x000DD7B4 File Offset: 0x000DCBB4
		internal static Exception InvalidCustomKeyStoreProviderName(string providerName, string prefix)
		{
			return ADP.Argument(Res.GetString("TCE_InvalidCustomKeyStoreProviderName", new object[]
			{
				providerName,
				prefix
			}), "clientKeyStoreProviders");
		}

		// Token: 0x060020A5 RID: 8357 RVA: 0x000DD7E4 File Offset: 0x000DCBE4
		internal static Exception NullProviderValue(string providerName)
		{
			return ADP.ArgumentNull("clientKeyStoreProviders", Res.GetString("TCE_NullProviderValue", new object[]
			{
				providerName
			}));
		}

		// Token: 0x060020A6 RID: 8358 RVA: 0x000DD810 File Offset: 0x000DCC10
		internal static Exception EmptyProviderName()
		{
			return ADP.ArgumentNull("clientKeyStoreProviders", Res.GetString("TCE_EmptyProviderName"));
		}

		// Token: 0x060020A7 RID: 8359 RVA: 0x000DD834 File Offset: 0x000DCC34
		internal static Exception ConnectionDoomed()
		{
			return ADP.InvalidOperation(Res.GetString("SQL_ConnectionDoomed"));
		}

		// Token: 0x060020A8 RID: 8360 RVA: 0x000DD850 File Offset: 0x000DCC50
		internal static Exception OpenResultCountExceeded()
		{
			return ADP.InvalidOperation(Res.GetString("SQL_OpenResultCountExceeded"));
		}

		// Token: 0x060020A9 RID: 8361 RVA: 0x000DD86C File Offset: 0x000DCC6C
		internal static Exception GlobalTransactionsNotEnabled()
		{
			return ADP.InvalidOperation(Res.GetString("GT_Disabled"));
		}

		// Token: 0x060020AA RID: 8362 RVA: 0x000DD888 File Offset: 0x000DCC88
		internal static Exception UnsupportedSysTxForGlobalTransactions()
		{
			return ADP.InvalidOperation(Res.GetString("GT_UnsupportedSysTxVersion"));
		}

		// Token: 0x060020AB RID: 8363 RVA: 0x000DD8A4 File Offset: 0x000DCCA4
		internal static Exception MultiSubnetFailoverWithFailoverPartner(bool serverProvidedFailoverPartner, SqlInternalConnectionTds internalConnection)
		{
			string @string = Res.GetString("SQLMSF_FailoverPartnerNotSupported");
			if (serverProvidedFailoverPartner)
			{
				SqlException ex = SqlException.CreateException(new SqlErrorCollection
				{
					new SqlError(0, 0, 20, null, @string, "", 0)
				}, null, internalConnection, null);
				ex._doNotReconnect = true;
				return ex;
			}
			return ADP.Argument(@string);
		}

		// Token: 0x060020AC RID: 8364 RVA: 0x000DD8F8 File Offset: 0x000DCCF8
		internal static Exception MultiSubnetFailoverWithMoreThan64IPs()
		{
			string snierrorMessage = SQL.GetSNIErrorMessage(47);
			return ADP.InvalidOperation(snierrorMessage);
		}

		// Token: 0x060020AD RID: 8365 RVA: 0x000DD914 File Offset: 0x000DCD14
		internal static Exception MultiSubnetFailoverWithInstanceSpecified()
		{
			string snierrorMessage = SQL.GetSNIErrorMessage(48);
			return ADP.Argument(snierrorMessage);
		}

		// Token: 0x060020AE RID: 8366 RVA: 0x000DD930 File Offset: 0x000DCD30
		internal static Exception MultiSubnetFailoverWithNonTcpProtocol()
		{
			string snierrorMessage = SQL.GetSNIErrorMessage(49);
			return ADP.Argument(snierrorMessage);
		}

		// Token: 0x060020AF RID: 8367 RVA: 0x000DD94C File Offset: 0x000DCD4C
		internal static Exception ROR_FailoverNotSupportedConnString()
		{
			return ADP.Argument(Res.GetString("SQLROR_FailoverNotSupported"));
		}

		// Token: 0x060020B0 RID: 8368 RVA: 0x000DD968 File Offset: 0x000DCD68
		internal static Exception ROR_FailoverNotSupportedServer(SqlInternalConnectionTds internalConnection)
		{
			SqlException ex = SqlException.CreateException(new SqlErrorCollection
			{
				new SqlError(0, 0, 20, null, Res.GetString("SQLROR_FailoverNotSupported"), "", 0)
			}, null, internalConnection, null);
			ex._doNotReconnect = true;
			return ex;
		}

		// Token: 0x060020B1 RID: 8369 RVA: 0x000DD9B0 File Offset: 0x000DCDB0
		internal static Exception ROR_RecursiveRoutingNotSupported(SqlInternalConnectionTds internalConnection)
		{
			SqlException ex = SqlException.CreateException(new SqlErrorCollection
			{
				new SqlError(0, 0, 20, null, Res.GetString("SQLROR_RecursiveRoutingNotSupported"), "", 0)
			}, null, internalConnection, null);
			ex._doNotReconnect = true;
			return ex;
		}

		// Token: 0x060020B2 RID: 8370 RVA: 0x000DD9F8 File Offset: 0x000DCDF8
		internal static Exception ROR_UnexpectedRoutingInfo(SqlInternalConnectionTds internalConnection)
		{
			SqlException ex = SqlException.CreateException(new SqlErrorCollection
			{
				new SqlError(0, 0, 20, null, Res.GetString("SQLROR_UnexpectedRoutingInfo"), "", 0)
			}, null, internalConnection, null);
			ex._doNotReconnect = true;
			return ex;
		}

		// Token: 0x060020B3 RID: 8371 RVA: 0x000DDA40 File Offset: 0x000DCE40
		internal static Exception ROR_InvalidRoutingInfo(SqlInternalConnectionTds internalConnection)
		{
			SqlException ex = SqlException.CreateException(new SqlErrorCollection
			{
				new SqlError(0, 0, 20, null, Res.GetString("SQLROR_InvalidRoutingInfo"), "", 0)
			}, null, internalConnection, null);
			ex._doNotReconnect = true;
			return ex;
		}

		// Token: 0x060020B4 RID: 8372 RVA: 0x000DDA88 File Offset: 0x000DCE88
		internal static Exception ROR_TimeoutAfterRoutingInfo(SqlInternalConnectionTds internalConnection)
		{
			SqlException ex = SqlException.CreateException(new SqlErrorCollection
			{
				new SqlError(0, 0, 20, null, Res.GetString("SQLROR_TimeoutAfterRoutingInfo"), "", 0)
			}, null, internalConnection, null);
			ex._doNotReconnect = true;
			return ex;
		}

		// Token: 0x060020B5 RID: 8373 RVA: 0x000DDAD0 File Offset: 0x000DCED0
		internal static SqlException CR_ReconnectTimeout()
		{
			return SqlException.CreateException(new SqlErrorCollection
			{
				new SqlError(-2, 0, 11, null, SQLMessage.Timeout(), "", 0, 258U)
			}, "");
		}

		// Token: 0x060020B6 RID: 8374 RVA: 0x000DDB14 File Offset: 0x000DCF14
		internal static SqlException CR_ReconnectionCancelled()
		{
			return SqlException.CreateException(new SqlErrorCollection
			{
				new SqlError(0, 0, 11, null, SQLMessage.OperationCancelled(), "", 0)
			}, "");
		}

		// Token: 0x060020B7 RID: 8375 RVA: 0x000DDB50 File Offset: 0x000DCF50
		internal static Exception CR_NextAttemptWillExceedQueryTimeout(SqlException innerException, Guid connectionId)
		{
			return SqlException.CreateException(new SqlErrorCollection
			{
				new SqlError(0, 0, 11, null, Res.GetString("SQLCR_NextAttemptWillExceedQueryTimeout"), "", 0)
			}, "", connectionId, innerException);
		}

		// Token: 0x060020B8 RID: 8376 RVA: 0x000DDB94 File Offset: 0x000DCF94
		internal static Exception CR_EncryptionChanged(SqlInternalConnectionTds internalConnection)
		{
			return SqlException.CreateException(new SqlErrorCollection
			{
				new SqlError(0, 0, 20, null, Res.GetString("SQLCR_EncryptionChanged"), "", 0)
			}, "", internalConnection, null);
		}

		// Token: 0x060020B9 RID: 8377 RVA: 0x000DDBD8 File Offset: 0x000DCFD8
		internal static SqlException CR_AllAttemptsFailed(SqlException innerException, Guid connectionId)
		{
			return SqlException.CreateException(new SqlErrorCollection
			{
				new SqlError(0, 0, 11, null, Res.GetString("SQLCR_AllAttemptsFailed"), "", 0)
			}, "", connectionId, innerException);
		}

		// Token: 0x060020BA RID: 8378 RVA: 0x000DDC1C File Offset: 0x000DD01C
		internal static SqlException CR_NoCRAckAtReconnection(SqlInternalConnectionTds internalConnection)
		{
			return SqlException.CreateException(new SqlErrorCollection
			{
				new SqlError(0, 0, 20, null, Res.GetString("SQLCR_NoCRAckAtReconnection"), "", 0)
			}, "", internalConnection, null);
		}

		// Token: 0x060020BB RID: 8379 RVA: 0x000DDC60 File Offset: 0x000DD060
		internal static SqlException CR_TDSVersionNotPreserved(SqlInternalConnectionTds internalConnection)
		{
			return SqlException.CreateException(new SqlErrorCollection
			{
				new SqlError(0, 0, 20, null, Res.GetString("SQLCR_TDSVestionNotPreserved"), "", 0)
			}, "", internalConnection, null);
		}

		// Token: 0x060020BC RID: 8380 RVA: 0x000DDCA4 File Offset: 0x000DD0A4
		internal static SqlException CR_UnrecoverableServer(Guid connectionId)
		{
			return SqlException.CreateException(new SqlErrorCollection
			{
				new SqlError(0, 0, 20, null, Res.GetString("SQLCR_UnrecoverableServer"), "", 0)
			}, "", connectionId, null);
		}

		// Token: 0x060020BD RID: 8381 RVA: 0x000DDCE8 File Offset: 0x000DD0E8
		internal static SqlException CR_UnrecoverableClient(Guid connectionId)
		{
			return SqlException.CreateException(new SqlErrorCollection
			{
				new SqlError(0, 0, 20, null, Res.GetString("SQLCR_UnrecoverableClient"), "", 0)
			}, "", connectionId, null);
		}

		// Token: 0x060020BE RID: 8382 RVA: 0x000DDD2C File Offset: 0x000DD12C
		internal static Exception BatchedUpdatesNotAvailableOnContextConnection()
		{
			return ADP.InvalidOperation(Res.GetString("SQL_BatchedUpdatesNotAvailableOnContextConnection"));
		}

		// Token: 0x060020BF RID: 8383 RVA: 0x000DDD48 File Offset: 0x000DD148
		internal static Exception ContextAllowsLimitedKeywords()
		{
			return ADP.InvalidOperation(Res.GetString("SQL_ContextAllowsLimitedKeywords"));
		}

		// Token: 0x060020C0 RID: 8384 RVA: 0x000DDD64 File Offset: 0x000DD164
		internal static Exception ContextAllowsOnlyTypeSystem2005()
		{
			return ADP.InvalidOperation(Res.GetString("SQL_ContextAllowsOnlyTypeSystem2005"));
		}

		// Token: 0x060020C1 RID: 8385 RVA: 0x000DDD80 File Offset: 0x000DD180
		internal static Exception ContextConnectionIsInUse()
		{
			return ADP.InvalidOperation(Res.GetString("SQL_ContextConnectionIsInUse"));
		}

		// Token: 0x060020C2 RID: 8386 RVA: 0x000DDD9C File Offset: 0x000DD19C
		internal static Exception ContextUnavailableOutOfProc()
		{
			return ADP.InvalidOperation(Res.GetString("SQL_ContextUnavailableOutOfProc"));
		}

		// Token: 0x060020C3 RID: 8387 RVA: 0x000DDDB8 File Offset: 0x000DD1B8
		internal static Exception ContextUnavailableWhileInProc()
		{
			return ADP.InvalidOperation(Res.GetString("SQL_ContextUnavailableWhileInProc"));
		}

		// Token: 0x060020C4 RID: 8388 RVA: 0x000DDDD4 File Offset: 0x000DD1D4
		internal static Exception NestedTransactionScopesNotSupported()
		{
			return ADP.InvalidOperation(Res.GetString("SQL_NestedTransactionScopesNotSupported"));
		}

		// Token: 0x060020C5 RID: 8389 RVA: 0x000DDDF0 File Offset: 0x000DD1F0
		internal static Exception NotAvailableOnContextConnection()
		{
			return ADP.InvalidOperation(Res.GetString("SQL_NotAvailableOnContextConnection"));
		}

		// Token: 0x060020C6 RID: 8390 RVA: 0x000DDE0C File Offset: 0x000DD20C
		internal static Exception NotificationsNotAvailableOnContextConnection()
		{
			return ADP.InvalidOperation(Res.GetString("SQL_NotificationsNotAvailableOnContextConnection"));
		}

		// Token: 0x060020C7 RID: 8391 RVA: 0x000DDE28 File Offset: 0x000DD228
		internal static Exception UnexpectedSmiEvent(SmiEventSink_Default.UnexpectedEventType eventType)
		{
			return ADP.InvalidOperation(Res.GetString("SQL_UnexpectedSmiEvent", new object[]
			{
				(int)eventType
			}));
		}

		// Token: 0x060020C8 RID: 8392 RVA: 0x000DDE54 File Offset: 0x000DD254
		internal static Exception UserInstanceNotAvailableInProc()
		{
			return ADP.InvalidOperation(Res.GetString("SQL_UserInstanceNotAvailableInProc"));
		}

		// Token: 0x060020C9 RID: 8393 RVA: 0x000DDE70 File Offset: 0x000DD270
		internal static Exception ArgumentLengthMismatch(string arg1, string arg2)
		{
			return ADP.Argument(Res.GetString("SQL_ArgumentLengthMismatch", new object[]
			{
				arg1,
				arg2
			}));
		}

		// Token: 0x060020CA RID: 8394 RVA: 0x000DDE9C File Offset: 0x000DD29C
		internal static Exception InvalidSqlDbTypeOneAllowedType(SqlDbType invalidType, string method, SqlDbType allowedType)
		{
			return ADP.Argument(Res.GetString("SQL_InvalidSqlDbTypeWithOneAllowedType", new object[]
			{
				invalidType,
				method,
				allowedType
			}));
		}

		// Token: 0x060020CB RID: 8395 RVA: 0x000DDED4 File Offset: 0x000DD2D4
		internal static Exception SqlPipeErrorRequiresSendEnd()
		{
			return ADP.InvalidOperation(Res.GetString("SQL_PipeErrorRequiresSendEnd"));
		}

		// Token: 0x060020CC RID: 8396 RVA: 0x000DDEF0 File Offset: 0x000DD2F0
		internal static Exception TooManyValues(string arg)
		{
			return ADP.Argument(Res.GetString("SQL_TooManyValues"), arg);
		}

		// Token: 0x060020CD RID: 8397 RVA: 0x000DDF10 File Offset: 0x000DD310
		internal static Exception StreamWriteNotSupported()
		{
			return ADP.NotSupported(Res.GetString("SQL_StreamWriteNotSupported"));
		}

		// Token: 0x060020CE RID: 8398 RVA: 0x000DDF2C File Offset: 0x000DD32C
		internal static Exception StreamReadNotSupported()
		{
			return ADP.NotSupported(Res.GetString("SQL_StreamReadNotSupported"));
		}

		// Token: 0x060020CF RID: 8399 RVA: 0x000DDF48 File Offset: 0x000DD348
		internal static Exception StreamSeekNotSupported()
		{
			return ADP.NotSupported(Res.GetString("SQL_StreamSeekNotSupported"));
		}

		// Token: 0x060020D0 RID: 8400 RVA: 0x000DDF64 File Offset: 0x000DD364
		internal static SqlNullValueException SqlNullValue()
		{
			SqlNullValueException ex = new SqlNullValueException();
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x060020D1 RID: 8401 RVA: 0x000DDF80 File Offset: 0x000DD380
		internal static Exception ParameterSizeRestrictionFailure(int index)
		{
			return ADP.InvalidOperation(Res.GetString("OleDb_CommandParameterError", new object[]
			{
				index.ToString(CultureInfo.InvariantCulture),
				"SqlParameter.Size"
			}));
		}

		// Token: 0x060020D2 RID: 8402 RVA: 0x000DDFBC File Offset: 0x000DD3BC
		internal static Exception SubclassMustOverride()
		{
			return ADP.InvalidOperation(Res.GetString("SqlMisc_SubclassMustOverride"));
		}

		// Token: 0x060020D3 RID: 8403 RVA: 0x000DDFD8 File Offset: 0x000DD3D8
		internal static string GetSNIErrorMessage(int sniError)
		{
			string name = string.Format(null, "SNI_ERROR_{0}", new object[]
			{
				sniError
			});
			return Res.GetString(name);
		}

		// Token: 0x040011F1 RID: 4593
		internal static readonly byte[] AttentionHeader = new byte[]
		{
			6,
			1,
			0,
			8,
			0,
			0,
			0,
			0
		};

		// Token: 0x040011F2 RID: 4594
		internal const string WriteToServer = "WriteToServer";

		// Token: 0x040011F3 RID: 4595
		internal const int SqlDependencyTimeoutDefault = 0;

		// Token: 0x040011F4 RID: 4596
		internal const int SqlDependencyServerTimeout = 432000;

		// Token: 0x040011F5 RID: 4597
		internal const string SqlNotificationServiceDefault = "SqlQueryNotificationService";

		// Token: 0x040011F6 RID: 4598
		internal const string SqlNotificationStoredProcedureDefault = "SqlQueryNotificationStoredProcedure";

		// Token: 0x040011F7 RID: 4599
		internal const string Transaction = "Transaction";

		// Token: 0x040011F8 RID: 4600
		internal const string Connection = "Connection";
	}
}
