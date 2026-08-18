using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using Oracle.ManagedDataAccess.Types;
using OracleInternal.Common;
using OracleInternal.ServiceObjects;

namespace Oracle.ManagedDataAccess.Client
{
	// Token: 0x02000053 RID: 83
	[ToolboxBitmap(typeof(resfinder), "Oracle.ManagedDataAccess.src.Client.Icons.OracleCommandBuilderToolBox_hc.bmp")]
	public sealed class OracleCommandBuilder : DbCommandBuilder
	{
		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000377 RID: 887 RVA: 0x000198A4 File Offset: 0x00017AA4
		// (set) Token: 0x06000378 RID: 888 RVA: 0x000198AC File Offset: 0x00017AAC
		private bool ODTDesignMode
		{
			get
			{
				return this.m_ODTDesignMode;
			}
			set
			{
				this.m_ODTDesignMode = value;
			}
		}

		// Token: 0x06000379 RID: 889 RVA: 0x000198B8 File Offset: 0x00017AB8
		public OracleCommandBuilder()
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				this.m_caseSensitive = true;
				this.m_handler = new OracleRowUpdatingEventHandler(this.RowUpdating);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x0600037A RID: 890 RVA: 0x00019948 File Offset: 0x00017B48
		public OracleCommandBuilder(OracleDataAdapter dataAdapter)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				this.m_dataAdapter = dataAdapter;
				this.m_caseSensitive = true;
				this.m_handler = new OracleRowUpdatingEventHandler(this.RowUpdating);
				if (dataAdapter != null)
				{
					dataAdapter.RowUpdating += this.m_handler;
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x0600037B RID: 891 RVA: 0x000199EC File Offset: 0x00017BEC
		public static void DeriveParameters(OracleCommand command)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (command == null)
				{
					throw new ArgumentNullException("command");
				}
				if (command.CommandType != CommandType.StoredProcedure)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.ODP_NOT_SUPPORTED, new string[]
					{
						"OracleCommandBuilder.DeriveParameters",
						command.CommandType.ToString()
					}));
				}
				OracleConnection connection = command.Connection;
				if (connection == null || connection.m_connectionState != ConnectionState.Open)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_CLOSED, new string[0]));
				}
				string commandText = command.CommandText;
				if (commandText == null || commandText.Length == 0)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.ODP_INVALID_VALUE, new string[]
					{
						"OracleCommand.CommandText"
					}));
				}
				DeriveParamInfo deriveParamInfo = null;
				SyncDictionary<string, DeriveParamInfoPool> dictDeriveParamInfoPool = connection.m_oracleConnectionImpl.m_pm.m_dictDeriveParamInfoPool;
				if (dictDeriveParamInfoPool != null && dictDeriveParamInfoPool.ContainsKey(connection.m_oracleConnectionImpl.ServiceName))
				{
					deriveParamInfo = dictDeriveParamInfoPool[connection.m_oracleConnectionImpl.ServiceName][commandText];
				}
				if (deriveParamInfo == null)
				{
					bool flag = false;
					int num = 0;
					string[] paramNameArray = null;
					int[] directionArray = null;
					int[] oraDbTypeArray = null;
					int[] sizeArray = null;
					string[] typeNameArray = null;
					int[] positionArray = null;
					int[] dataLevelOutArray = null;
					lock (OracleCommandBuilder.m_dpLock)
					{
						if (dictDeriveParamInfoPool != null && dictDeriveParamInfoPool.ContainsKey(connection.m_oracleConnectionImpl.ServiceName))
						{
							deriveParamInfo = dictDeriveParamInfoPool[connection.m_oracleConnectionImpl.ServiceName][commandText];
						}
						if (deriveParamInfo == null)
						{
							OracleCommandBuilder.SetUpDpCommand(command);
							OracleCommandBuilder.m_dpCommand.ExecuteNonQuery();
							num = (int)OracleCommandBuilder.m_dpCommandParams[2].Value;
							if (num == -1002)
							{
								throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.ODP_INVALID_VALUE, new string[]
								{
									"OracleCommand.CommandText"
								}));
							}
							if (num > 128)
							{
								OracleCommandBuilder.m_dpCommandParams[1].Value = num;
								OracleCommandBuilder.m_dpCommandParams[3].Size = num;
								if (OracleCommandBuilder.m_dpCommandParams[3].ArrayBindSize.Length < num)
								{
									OracleCommandBuilder.m_dpCommandParams[3].ArrayBindSize = new int[num];
								}
								for (int i = 0; i < num; i++)
								{
									OracleCommandBuilder.m_dpCommandParams[3].ArrayBindSize[i] = 128;
								}
								OracleCommandBuilder.m_dpCommandParams[4].Size = num;
								OracleCommandBuilder.m_dpCommandParams[5].Size = num;
								OracleCommandBuilder.m_dpCommandParams[6].Size = num;
								OracleCommandBuilder.m_dpCommandParams[7].Size = num;
								if (OracleCommandBuilder.m_dpCommandParams[7].ArrayBindSize.Length < num)
								{
									OracleCommandBuilder.m_dpCommandParams[7].ArrayBindSize = new int[num];
								}
								for (int j = 0; j < num; j++)
								{
									OracleCommandBuilder.m_dpCommandParams[7].ArrayBindSize[j] = 128;
								}
								OracleCommandBuilder.m_dpCommandParams[8].Size = num;
								OracleCommandBuilder.m_dpCommandParams[9].Size = num;
								OracleCommandBuilder.m_dpCommand.ExecuteNonQuery();
								num = (int)OracleCommandBuilder.m_dpCommandParams[2].Value;
							}
							paramNameArray = (string[])OracleCommandBuilder.m_dpCommandParams[3].Value;
							directionArray = (int[])OracleCommandBuilder.m_dpCommandParams[4].Value;
							oraDbTypeArray = (int[])OracleCommandBuilder.m_dpCommandParams[5].Value;
							sizeArray = (int[])OracleCommandBuilder.m_dpCommandParams[6].Value;
							typeNameArray = (string[])OracleCommandBuilder.m_dpCommandParams[7].Value;
							positionArray = (int[])OracleCommandBuilder.m_dpCommandParams[8].Value;
							dataLevelOutArray = (int[])OracleCommandBuilder.m_dpCommandParams[9].Value;
							flag = true;
						}
					}
					if (flag)
					{
						deriveParamInfo = new DeriveParamInfo(num);
						OracleCommandBuilder.PopulateDeriveParamInfoFromDpCommandExecution(ref deriveParamInfo, paramNameArray, directionArray, oraDbTypeArray, sizeArray, typeNameArray, positionArray, dataLevelOutArray);
						if (dictDeriveParamInfoPool != null)
						{
							if (!dictDeriveParamInfoPool.ContainsKey(connection.m_oracleConnectionImpl.ServiceName))
							{
								lock (connection.m_oracleConnectionImpl.m_pm.m_dictDeriveParamInfoPoolLock)
								{
									if (!dictDeriveParamInfoPool.ContainsKey(connection.m_oracleConnectionImpl.ServiceName))
									{
										dictDeriveParamInfoPool[connection.m_oracleConnectionImpl.ServiceName] = new DeriveParamInfoPool(50);
									}
								}
							}
							dictDeriveParamInfoPool[connection.m_oracleConnectionImpl.ServiceName][commandText] = deriveParamInfo;
						}
					}
				}
				lock (command)
				{
					OracleCommandBuilder.PopulateCommandParamsFromDeriveParamInfo(command, deriveParamInfo);
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x0600037C RID: 892 RVA: 0x00019EFC File Offset: 0x000180FC
		private static void PopulateDeriveParamInfoFromDpCommandExecution(ref DeriveParamInfo deriveParamInfo, string[] paramNameArray, int[] directionArray, int[] oraDbTypeArray, int[] sizeArray, string[] typeNameArray, int[] positionArray, int[] dataLevelOutArray)
		{
			int num = 0;
			for (int i = 0; i < deriveParamInfo.m_allocCount; i++)
			{
				if (dataLevelOutArray[i] == 0)
				{
					if (oraDbTypeArray[i] == 0)
					{
						break;
					}
					int num2 = positionArray[i];
					if (oraDbTypeArray[i] == 100)
					{
						if (typeNameArray[i] == "SYS.XMLTYPE" || typeNameArray[i] == "PUBLIC.XMLTYPE")
						{
							oraDbTypeArray[i] = 127;
						}
						else
						{
							oraDbTypeArray[i] = -1;
						}
					}
					if (oraDbTypeArray[i] == -1)
					{
						throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.BLR_PRM_NOT_SUPPORTED, new string[]
						{
							num2.ToString()
						}));
					}
					deriveParamInfo.m_paramName[num] = paramNameArray[i];
					deriveParamInfo.m_direction[num] = (ParameterDirection)directionArray[i];
					deriveParamInfo.m_size[num] = sizeArray[i];
					if (oraDbTypeArray[i] != 1)
					{
						deriveParamInfo.m_oraCollType[num] = OracleCollectionType.None;
						deriveParamInfo.m_oraDbType[num] = (OracleDbType)oraDbTypeArray[i];
					}
					else
					{
						deriveParamInfo.m_oraCollType[num] = OracleCollectionType.PLSQLAssociativeArray;
						i++;
						if (oraDbTypeArray[i] == 100)
						{
							if (typeNameArray[i] == "SYS.XMLTYPE" || typeNameArray[i] == "PUBLIC.XMLTYPE")
							{
								oraDbTypeArray[i] = 127;
							}
							else
							{
								oraDbTypeArray[i] = -1;
							}
						}
						if (oraDbTypeArray[i] == -1)
						{
							throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.BLR_PRM_NOT_SUPPORTED, new string[]
							{
								num2.ToString()
							}));
						}
						deriveParamInfo.m_oraDbType[num] = (OracleDbType)oraDbTypeArray[i];
						deriveParamInfo.m_arrayBindSize[num] = sizeArray[i];
					}
					num++;
				}
			}
			deriveParamInfo.m_paramCount = num;
		}

		// Token: 0x0600037D RID: 893 RVA: 0x0001A06C File Offset: 0x0001826C
		private static void SetUpDpCommand(OracleCommand command)
		{
			if (OracleCommandBuilder.m_dpCommand == null)
			{
				OracleCommandBuilder.m_dpCommand = new OracleCommand();
				OracleCommandBuilder.m_dpCommandParams = new OracleParameter[10];
				for (int i = 0; i < 10; i++)
				{
					OracleCommandBuilder.m_dpCommandParams[i] = new OracleParameter();
					OracleCommandBuilder.m_dpCommand.Parameters.Add(OracleCommandBuilder.m_dpCommandParams[i]);
				}
				OracleCommandBuilder.m_dpCommandParams[0].DbType = DbType.String;
				OracleCommandBuilder.m_dpCommandParams[1].DbType = DbType.Int32;
				OracleCommandBuilder.m_dpCommandParams[2].DbType = DbType.Int32;
				OracleCommandBuilder.m_dpCommandParams[2].Direction = ParameterDirection.Output;
				OracleCommandBuilder.m_dpCommandParams[3].DbType = DbType.String;
				OracleCommandBuilder.m_dpCommandParams[3].Direction = ParameterDirection.Output;
				OracleCommandBuilder.m_dpCommandParams[3].CollectionType = OracleCollectionType.PLSQLAssociativeArray;
				OracleCommandBuilder.m_dpCommandParams[4].DbType = DbType.Int32;
				OracleCommandBuilder.m_dpCommandParams[4].Direction = ParameterDirection.Output;
				OracleCommandBuilder.m_dpCommandParams[4].CollectionType = OracleCollectionType.PLSQLAssociativeArray;
				OracleCommandBuilder.m_dpCommandParams[5].DbType = DbType.Int32;
				OracleCommandBuilder.m_dpCommandParams[5].Direction = ParameterDirection.Output;
				OracleCommandBuilder.m_dpCommandParams[5].CollectionType = OracleCollectionType.PLSQLAssociativeArray;
				OracleCommandBuilder.m_dpCommandParams[6].DbType = DbType.Int32;
				OracleCommandBuilder.m_dpCommandParams[6].Direction = ParameterDirection.Output;
				OracleCommandBuilder.m_dpCommandParams[6].CollectionType = OracleCollectionType.PLSQLAssociativeArray;
				OracleCommandBuilder.m_dpCommandParams[7].DbType = DbType.String;
				OracleCommandBuilder.m_dpCommandParams[7].Direction = ParameterDirection.Output;
				OracleCommandBuilder.m_dpCommandParams[7].CollectionType = OracleCollectionType.PLSQLAssociativeArray;
				OracleCommandBuilder.m_dpCommandParams[8].DbType = DbType.Int32;
				OracleCommandBuilder.m_dpCommandParams[8].Direction = ParameterDirection.Output;
				OracleCommandBuilder.m_dpCommandParams[8].CollectionType = OracleCollectionType.PLSQLAssociativeArray;
				OracleCommandBuilder.m_dpCommandParams[9].DbType = DbType.Int32;
				OracleCommandBuilder.m_dpCommandParams[9].Direction = ParameterDirection.Output;
				OracleCommandBuilder.m_dpCommandParams[9].CollectionType = OracleCollectionType.PLSQLAssociativeArray;
			}
			OracleCommandBuilder.m_dpCommand.CommandText = "DECLARE TYPE NUM_TABLE IS TABLE OF NUMBER INDEX BY PLS_INTEGER; TYPE STR_TABLE IS TABLE OF VARCHAR2(128) INDEX BY PLS_INTEGER; name_in          VARCHAR2(2000); param_count_in   PLS_INTEGER; link\t     \t      VARCHAR2(2000); context          NUMBER := 1; schema           VARCHAR2(2000); part1            VARCHAR2(2000); part2            VARCHAR2(2000); dblink           VARCHAR2(2000); part1_type       NUMBER; object_number    NUMBER; refcur\t          SYS_REFCURSOR; idx\t\t          PLS_INTEGER := 0; proc_count       PLS_INTEGER := 0; procrefcur\t      SYS_REFCURSOR; procobjectnames  ALL_PROCEDURES.OBJECT_NAME%TYPE ; param_count_out        PLS_INTEGER := 0; param_name_out         STR_TABLE; direction_out          NUM_TABLE; oradbtype_out          NUM_TABLE; size_out               NUM_TABLE; type_name_out          STR_TABLE; position_out           NUM_TABLE; data_level_out         NUM_TABLE; param_name_null_out    STR_TABLE; direction_null_out     NUM_TABLE; oradbtype_null_out     NUM_TABLE; size_null_out          NUM_TABLE; type_name_null_out     STR_TABLE; position_null_out      NUM_TABLE; data_level_null_out    NUM_TABLE; BEGIN name_in := :1; param_count_in := :2; link := NULL; DBMS_UTILITY.NAME_RESOLVE( name_in, context, schema, part1, part2, dblink, part1_type, object_number); WHILE (dblink IS NOT NULL) LOOP link := '@' || dblink; name_in := NULL; IF (schema IS NOT NULL) THEN name_in := '\"' || schema || '\"' || '.'; END IF; IF (part1 IS NOT NULL) THEN name_in := name_in || '\"' || part1 || '\"' || '.'; END IF; IF (part2 IS NULL) THEN name_in := RTRIM(name_in, '.'); ELSE name_in := name_in || '\"' || part2 || '\"'; END IF; EXECUTE IMMEDIATE 'BEGIN DBMS_UTILITY.NAME_RESOLVE' || link || '(:1, :2, :3, :4, :5, :6, :7, :8); END;' USING name_in, context, OUT schema, OUT part1, OUT part2, OUT dblink, OUT part1_type, OUT object_number; END LOOP; IF (param_count_out = 0) THEN OPEN refcur FOR 'SELECT  DECODE (POSITION, 0, ''RETURN_VALUE'', ARGUMENT_NAME) param_name, DECODE(IN_OUT, ''IN'', \t  1,  ''IN/OUT'', 3, ''OUT'', DECODE(ARGUMENT_NAME, NULL, 6, 2), 1) direction, DECODE(DATA_TYPE, ''BINARY_DOUBLE'',  132, ''BINARY_FLOAT'',   133, ''BINARY_INTEGER'', 112, ''BFILE'',     101, ''BLOB'',\t    102, ''CHAR'',\t    104, ''CLOB'',\t    105, ''DATE'',\t    106, ''FLOAT'',\t    107, ''INTERVAL YEAR TO MONTH'', 115, ''INTERVAL DAY TO SECOND'', 114, ''LONG'',\t    109, ''LONG RAW'',\t110, ''NCHAR'',\t    117, ''NCLOB'',\t    116, ''NUMBER'',\t  107, ''NVARCHAR2'',\t119, ''PL/SQL BOOLEAN'',   134, ''RAW'',\t      120, ''REF CURSOR'',121, ''ROWID'',\t    126, ''TIMESTAMP'',\t123, ''TIMESTAMP WITH LOCAL TIME ZONE'', 124, ''TIMESTAMP WITH TIME ZONE'', 125, ''OPAQUE/XMLTYPE'', 127, ''UNDEFINED'', 100, ''UROWID'',\t  126, ''VARCHAR'', \t126, ''VARCHAR2'',\t126, ''PL/SQL TABLE'',1, NULL,            0, -1) oradbtype, DECODE(DATA_TYPE, ''CHAR'', \t      2000, ''LONG'',      32760, ''LONG RAW'',    32760, ''NCHAR'', \t    2000, ''NVARCHAR2'',   32767, ''RAW'', \t      2000, ''ROWID'', \t    4000, ''UROWID'',\t    4000, ''VARCHAR'', \t  32767, ''VARCHAR2'', \t  32767, ''PL/SQL TABLE'',  16, 0) length, (TYPE_OWNER ||  DECODE(TYPE_OWNER, NULL, NULL, ''.'') ||  TYPE_NAME ) type_name, POSITION   position, DATA_LEVEL data_level FROM ALL_ARGUMENTS' || link || ' WHERE OWNER \t\t  = :1 \t  AND (PACKAGE_NAME \t= :2 \t  OR (:3 IS NULL AND PACKAGE_NAME is null)) AND OBJECT_NAME \t  = :4 \t  AND NVL(overload, 1) = 1 ORDER BY SEQUENCE' USING schema, part1, part1, part2; FETCH refcur BULK COLLECT INTO param_name_out, direction_out, oradbtype_out, size_out, type_name_out, position_out, data_level_out; param_count_out := refcur%ROWCOUNT; CLOSE refcur; END IF; IF (part1_type = 9 AND param_count_out = 0) THEN OPEN procrefcur FOR 'SELECT OBJECT_NAME FROM ALL_PROCEDURES' || link || ' WHERE OWNER = :1 AND (((OBJECT_NAME = :2) AND (PROCEDURE_NAME IS NULL)) OR ((OBJECT_NAME = :3) AND (PROCEDURE_NAME = :4)))' USING schema, part2, part1, part2; FETCH procrefcur INTO procobjectnames;proc_count := procrefcur%ROWCOUNT; CLOSE procrefcur; IF (proc_count = 0) THEN param_count_out := -1002; END IF; END IF; :3 := param_count_out; IF (param_count_out > param_count_in OR param_count_out < 0) THEN param_name_out:= param_name_null_out; direction_out := direction_null_out; oradbtype_out := oradbtype_null_out; size_out      := size_null_out; type_name_out := type_name_null_out; position_out  := position_null_out; data_level_out:= data_level_null_out; END IF; :4 := param_name_out; :5 := direction_out; :6 := oradbtype_out; :7 := size_out; :8 := type_name_out; :9 := position_out; :10:= data_level_out; END;";
			OracleCommandBuilder.m_dpCommand.Connection = command.Connection;
			OracleCommandBuilder.m_dpCommand.AddToStatementCache = command.AddToStatementCache;
			OracleCommandBuilder.m_dpCommandParams[0].Value = command.CommandText;
			OracleCommandBuilder.m_dpCommandParams[1].Value = 128;
			OracleCommandBuilder.m_dpCommandParams[3].Size = 128;
			if (OracleCommandBuilder.m_dpCommandParams[3].ArrayBindSize == null)
			{
				OracleCommandBuilder.m_dpCommandParams[3].ArrayBindSize = new int[128];
			}
			for (int j = 0; j < 128; j++)
			{
				OracleCommandBuilder.m_dpCommandParams[3].ArrayBindSize[j] = 128;
			}
			OracleCommandBuilder.m_dpCommandParams[4].Size = 128;
			OracleCommandBuilder.m_dpCommandParams[5].Size = 128;
			OracleCommandBuilder.m_dpCommandParams[6].Size = 128;
			OracleCommandBuilder.m_dpCommandParams[7].Size = 128;
			if (OracleCommandBuilder.m_dpCommandParams[7].ArrayBindSize == null)
			{
				OracleCommandBuilder.m_dpCommandParams[7].ArrayBindSize = new int[128];
			}
			for (int k = 0; k < 128; k++)
			{
				OracleCommandBuilder.m_dpCommandParams[7].ArrayBindSize[k] = 128;
			}
			OracleCommandBuilder.m_dpCommandParams[8].Size = 128;
			OracleCommandBuilder.m_dpCommandParams[9].Size = 128;
		}

		// Token: 0x0600037E RID: 894 RVA: 0x0001A378 File Offset: 0x00018578
		private static void PopulateCommandParamsFromDeriveParamInfo(OracleCommand command, DeriveParamInfo deriveParamInfo)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				command.Parameters.Clear();
				for (int i = 0; i < deriveParamInfo.m_paramCount; i++)
				{
					OracleParameter oracleParameter = new OracleParameter();
					oracleParameter.ParameterName = deriveParamInfo.m_paramName[i];
					oracleParameter.Direction = deriveParamInfo.m_direction[i];
					oracleParameter.CollectionType = deriveParamInfo.m_oraCollType[i];
					oracleParameter.OracleDbTypeEx = deriveParamInfo.m_oraDbType[i];
					if (deriveParamInfo.m_size[i] != 0 && oracleParameter.Direction != ParameterDirection.Input)
					{
						oracleParameter.Size = deriveParamInfo.m_size[i];
					}
					if (oracleParameter.CollectionType == OracleCollectionType.PLSQLAssociativeArray && oracleParameter.Size > 0)
					{
						oracleParameter.ArrayBindStatus = new OracleParameterStatus[oracleParameter.Size];
						for (int j = 0; j < oracleParameter.Size; j++)
						{
							oracleParameter.ArrayBindStatus[j] = OracleParameterStatus.Success;
						}
						if (deriveParamInfo.m_arrayBindSize[i] != 0)
						{
							oracleParameter.ArrayBindSize = new int[oracleParameter.Size];
							for (int k = 0; k < oracleParameter.Size; k++)
							{
								oracleParameter.ArrayBindSize[k] = deriveParamInfo.m_arrayBindSize[i];
							}
						}
					}
					command.Parameters.Add(oracleParameter);
				}
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x0600037F RID: 895 RVA: 0x0001A4DC File Offset: 0x000186DC
		public new OracleCommand GetDeleteCommand(bool useColumnsForParameterNames)
		{
			return this.GetDeleteCommand();
		}

		// Token: 0x06000380 RID: 896 RVA: 0x0001A4E4 File Offset: 0x000186E4
		public new OracleCommand GetInsertCommand(bool useColumnsForParameterNames)
		{
			return this.GetInsertCommand();
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0001A4EC File Offset: 0x000186EC
		public new OracleCommand GetUpdateCommand(bool useColumnsForParameterNames)
		{
			return this.GetUpdateCommand();
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0001A4F4 File Offset: 0x000186F4
		internal OracleCommand GetInsertCommand(DataRow row)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			OracleCommand result;
			try
			{
				int num = 1;
				StringBuilder stringBuilder = new StringBuilder();
				StringBuilder stringBuilder2 = new StringBuilder();
				if (this.m_cachedInsertParams == null)
				{
					this.m_cachedInsertParams = new ArrayList();
				}
				if (this.m_insertCmd == null || this.m_insertCmd.m_disposed)
				{
					if (this.m_insertCmd != null)
					{
						int i = 0;
						while (i < this.m_cachedInsertParams.Count)
						{
							if (((OracleParameter)this.m_cachedInsertParams[0]).m_collRef != null)
							{
								this.m_cachedInsertParams.RemoveAt(0);
							}
							else
							{
								i++;
							}
						}
					}
					this.m_insertCmd = new OracleCommand();
				}
				else
				{
					if (this.m_insertCmd.m_modified)
					{
						this.m_insertCmd.ArrayBindCount = 0;
						this.m_insertCmd.AddRowid = false;
						this.m_insertCmd.BindByName = false;
						this.m_insertCmd.CommandType = CommandType.Text;
						this.m_insertCmd.FetchSize = 131072L;
						this.m_insertCmd.m_initialLongFS = 0;
						this.m_insertCmd.CommandTimeout = 0;
					}
					this.m_insertCmd.Parameters.Clear();
				}
				DataTable table = row.Table;
				if (!table.ExtendedProperties.Contains("REFCursorName"))
				{
					this.GetBaseTableName();
				}
				else
				{
					this.CheckDataTable(table);
				}
				foreach (object obj in table.Columns)
				{
					DataColumn dataColumn = (DataColumn)obj;
					string columnName = this.GetColumnName(dataColumn, true);
					string columnName2 = this.GetColumnName(dataColumn, false);
					OracleDbType columnType = this.GetColumnType(dataColumn);
					if (columnName != null)
					{
						stringBuilder.Append(" ");
						stringBuilder.Append(columnName);
						stringBuilder.Append(",");
						if (!this.IsRowCurrentValueNull(row, dataColumn))
						{
							stringBuilder2.Append(" :");
							stringBuilder2.Append(num);
							stringBuilder2.Append(",");
							OracleParameter oracleParameter = null;
							if (this.m_cachedInsertParams.Count > num - 1)
							{
								oracleParameter = (OracleParameter)this.m_cachedInsertParams[num - 1];
								if (oracleParameter.m_disposed)
								{
									this.m_cachedInsertParams.RemoveAt(num - 1);
									oracleParameter = null;
								}
							}
							if (oracleParameter != null)
							{
								this.SetParam(columnName2, columnType, DataRowVersion.Current, row[dataColumn, DataRowVersion.Current], oracleParameter);
							}
							else
							{
								oracleParameter = this.CreateParams(null, columnName2, columnType, DataRowVersion.Current, row[dataColumn, DataRowVersion.Current]);
								this.m_cachedInsertParams.Insert(num - 1, oracleParameter);
							}
							this.m_insertCmd.Parameters.Add(oracleParameter);
							num++;
						}
						else
						{
							stringBuilder2.Append(" NULL,");
						}
					}
				}
				if (stringBuilder.Length == 0)
				{
					result = null;
				}
				else
				{
					StringBuilder stringBuilder3 = new StringBuilder();
					stringBuilder3.Append("INSERT INTO ");
					stringBuilder3.Append(this.GetSchemaName(table));
					stringBuilder3.Append(this.GetBaseTableName(table));
					stringBuilder3.Append("(");
					stringBuilder3.Append(stringBuilder.ToString().TrimEnd(new char[]
					{
						','
					}));
					stringBuilder3.Append(") VALUES (");
					stringBuilder3.Append(stringBuilder2.ToString().TrimEnd(new char[]
					{
						','
					}));
					stringBuilder3.Append(")");
					this.m_insertCmd.CommandText = stringBuilder3.ToString();
					this.m_insertCmd.Connection = this.DataAdapter.SelectCommand.Connection;
					this.m_insertCmd.UpdatedRowSource = UpdateRowSource.None;
					result = this.m_insertCmd;
				}
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0001A8FC File Offset: 0x00018AFC
		internal OracleCommand GetUpdateCommand(DataRow row)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			OracleCommand result;
			try
			{
				int num = 1;
				StringBuilder stringBuilder = new StringBuilder();
				StringBuilder stringBuilder2 = new StringBuilder();
				if (row.RowState != DataRowState.Modified)
				{
					result = null;
				}
				else
				{
					if (this.m_cachedUpdateParams == null)
					{
						this.m_cachedUpdateParams = new ArrayList();
					}
					if (this.m_updateCmd == null || this.m_updateCmd.m_disposed)
					{
						if (this.m_updateCmd != null)
						{
							int i = 0;
							while (i < this.m_cachedUpdateParams.Count)
							{
								if (((OracleParameter)this.m_cachedUpdateParams[i]).m_collRef != null)
								{
									this.m_cachedUpdateParams.RemoveAt(i);
								}
								else
								{
									i++;
								}
							}
						}
						this.m_updateCmd = new OracleCommand();
					}
					else
					{
						if (this.m_updateCmd.m_modified)
						{
							this.m_updateCmd.ArrayBindCount = 0;
							this.m_updateCmd.AddRowid = false;
							this.m_updateCmd.BindByName = false;
							this.m_updateCmd.CommandType = CommandType.Text;
							this.m_updateCmd.FetchSize = 131072L;
							this.m_updateCmd.m_initialLongFS = 0;
							this.m_updateCmd.CommandTimeout = 0;
						}
						this.m_updateCmd.Parameters.Clear();
					}
					DataTable table = row.Table;
					if (!table.ExtendedProperties.Contains("REFCursorName"))
					{
						this.GetBaseTableName();
					}
					else
					{
						this.CheckDataTable(table);
					}
					this.CheckPrimaryKey(row);
					foreach (object obj in table.Columns)
					{
						DataColumn dataColumn = (DataColumn)obj;
						string columnName = this.GetColumnName(dataColumn, true);
						string columnName2 = this.GetColumnName(dataColumn, false);
						OracleDbType columnType = this.GetColumnType(dataColumn);
						if (columnName != null && this.IsColumnModified(row, dataColumn))
						{
							stringBuilder.Append(" ");
							stringBuilder.Append(columnName);
							stringBuilder.Append("=:");
							stringBuilder.Append(num);
							stringBuilder.Append(",");
							OracleParameter oracleParameter = null;
							if (this.m_cachedUpdateParams.Count > num - 1)
							{
								oracleParameter = (OracleParameter)this.m_cachedUpdateParams[num - 1];
								if (oracleParameter.m_disposed)
								{
									this.m_cachedUpdateParams.RemoveAt(num - 1);
									oracleParameter = null;
								}
							}
							if (oracleParameter != null)
							{
								this.SetParam(columnName2, columnType, DataRowVersion.Current, row[dataColumn, DataRowVersion.Current], oracleParameter);
							}
							else
							{
								oracleParameter = this.CreateParams(null, columnName2, columnType, DataRowVersion.Current, row[dataColumn, DataRowVersion.Current]);
								this.m_cachedUpdateParams.Insert(num - 1, oracleParameter);
							}
							this.m_updateCmd.Parameters.Add(oracleParameter);
							num++;
						}
					}
					if (stringBuilder.ToString().Length == 0 && row.RowState == DataRowState.Modified)
					{
						foreach (object obj2 in table.Columns)
						{
							DataColumn dataColumn2 = (DataColumn)obj2;
							OracleDbType columnType2 = this.GetColumnType(dataColumn2);
							string columnName3 = this.GetColumnName(dataColumn2, true);
							string columnName4 = this.GetColumnName(dataColumn2, false);
							if (columnName3 != null)
							{
								stringBuilder.Append(" ");
								stringBuilder.Append(columnName3);
								stringBuilder.Append("=:");
								stringBuilder.Append(num);
								stringBuilder.Append(",");
								OracleParameter oracleParameter2 = null;
								if (this.m_cachedUpdateParams.Count > num - 1)
								{
									oracleParameter2 = (OracleParameter)this.m_cachedUpdateParams[num - 1];
									if (oracleParameter2.m_disposed)
									{
										this.m_cachedUpdateParams.RemoveAt(num - 1);
										oracleParameter2 = null;
									}
								}
								if (oracleParameter2 != null)
								{
									this.SetParam(columnName4, columnType2, DataRowVersion.Current, row[dataColumn2, DataRowVersion.Current], oracleParameter2);
								}
								else
								{
									oracleParameter2 = this.CreateParams(null, columnName4, columnType2, DataRowVersion.Current, row[dataColumn2, DataRowVersion.Current]);
									this.m_cachedUpdateParams.Insert(num - 1, oracleParameter2);
								}
								this.m_updateCmd.Parameters.Add(oracleParameter2);
								num++;
							}
						}
					}
					if (stringBuilder.ToString().Length == 0)
					{
						result = null;
					}
					else
					{
						foreach (object obj3 in table.Columns)
						{
							DataColumn dataColumn3 = (DataColumn)obj3;
							OracleDbType columnType3 = this.GetColumnType(dataColumn3);
							string columnName5 = this.GetColumnName(dataColumn3, true);
							string columnName6 = this.GetColumnName(dataColumn3, false);
							if (!this.IsOraLOB(columnType3) && !this.IsOraLONG(columnType3) && !this.IsOraXmlType(columnType3) && columnName5 != null)
							{
								if (!this.IsRowOrigValueNull(row, dataColumn3))
								{
									stringBuilder2.Append(" ");
									stringBuilder2.Append(columnName5);
									stringBuilder2.Append("=:");
									stringBuilder2.Append(num);
									stringBuilder2.Append(" AND");
									OracleParameter oracleParameter3 = null;
									if (this.m_cachedUpdateParams.Count > num - 1)
									{
										oracleParameter3 = (OracleParameter)this.m_cachedUpdateParams[num - 1];
										if (oracleParameter3.m_disposed)
										{
											this.m_cachedUpdateParams.RemoveAt(num - 1);
											oracleParameter3 = null;
										}
									}
									if (oracleParameter3 != null)
									{
										this.SetParam(columnName6, columnType3, DataRowVersion.Original, row[dataColumn3, DataRowVersion.Original], oracleParameter3);
									}
									else
									{
										oracleParameter3 = this.CreateParams(null, columnName6, columnType3, DataRowVersion.Original, row[dataColumn3, DataRowVersion.Original]);
										this.m_cachedUpdateParams.Insert(num - 1, oracleParameter3);
									}
									this.m_updateCmd.Parameters.Add(oracleParameter3);
									num++;
								}
								else
								{
									stringBuilder2.Append(" ");
									stringBuilder2.Append(columnName5);
									stringBuilder2.Append(" IS NULL AND");
								}
							}
						}
						StringBuilder stringBuilder3 = new StringBuilder();
						stringBuilder3.Append("UPDATE ");
						stringBuilder3.Append(this.GetSchemaName(table));
						stringBuilder3.Append(this.GetBaseTableName(table));
						stringBuilder3.Append(" SET");
						stringBuilder3.Append(stringBuilder.ToString().TrimEnd(new char[]
						{
							','
						}));
						stringBuilder3.Append(" WHERE");
						stringBuilder3.Append(stringBuilder2.ToString().TrimEnd("AND".ToCharArray()).TrimEnd(new char[0]));
						this.m_updateCmd.CommandText = stringBuilder3.ToString();
						this.m_updateCmd.Connection = this.DataAdapter.SelectCommand.Connection;
						this.m_updateCmd.UpdatedRowSource = UpdateRowSource.None;
						result = this.m_updateCmd;
					}
				}
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0001B03C File Offset: 0x0001923C
		private bool CheckDataTable(DataTable table)
		{
			if (table.ExtendedProperties["BaseTable.1"] != null)
			{
				throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.BLR_MULTITABLE_DS, new string[0]));
			}
			return true;
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0001B068 File Offset: 0x00019268
		private void SetParam(string colName, OracleDbType colType, DataRowVersion version, object value, OracleParameter param)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (param.m_modified)
				{
					param.Direction = ParameterDirection.Input;
					param.IsNullable = false;
					param.Offset = 0;
					param.Status = OracleParameterStatus.Success;
					param.Precision = 100;
					param.Scale = 129;
				}
				int size = -1;
				if (value != null)
				{
					string text;
					char[] array;
					byte[] array2;
					if (value is char)
					{
						size = 1;
					}
					else if ((text = (value as string)) != null)
					{
						size = text.Length;
					}
					else if ((array = (value as char[])) != null)
					{
						size = array.Length;
					}
					else if ((array2 = (value as byte[])) != null)
					{
						size = array2.Length;
					}
				}
				if (colType == OracleDbType.Clob)
				{
					if (!(value is OracleClob))
					{
						colType = OracleDbType.Varchar2;
					}
				}
				else if (colType == OracleDbType.NClob)
				{
					if (!(value is OracleClob))
					{
						colType = OracleDbType.NVarchar2;
					}
				}
				else if (colType == OracleDbType.Blob && !(value is OracleBlob))
				{
					colType = OracleDbType.Raw;
				}
				param.ParameterName = null;
				param.OracleDbType = colType;
				param.SetSize(size);
				param.SourceColumn = colName;
				param.SourceVersion = version;
				param.Value = value;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x06000386 RID: 902 RVA: 0x0001B1A8 File Offset: 0x000193A8
		private void CheckPrimaryKey()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (this.m_sqlMetaData == null)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.BLR_NO_PRIMARYKEY, new string[0]));
				}
				if (!this.m_sqlMetaData.bPkFetched)
				{
					if (this.m_dataAdapter.SelectCommand.Connection == null || this.m_dataAdapter.SelectCommand.Connection.m_oracleConnectionImpl == null)
					{
						throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.BLR_NO_PRIMARYKEY, new string[0]));
					}
					SQLMetaData.GetPrimaryKey(this.m_dataAdapter.SelectCommand.Connection, this.m_sqlMetaData, this.m_numberOfHiddenColumns, true);
				}
				if (this.m_sqlMetaData.m_sqlMetaInfo == null || !this.m_sqlMetaData.m_sqlMetaInfo.bPkPresent)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.BLR_NO_PRIMARYKEY, new string[0]));
				}
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x06000387 RID: 903 RVA: 0x0001B2BC File Offset: 0x000194BC
		private void CheckPrimaryKey(DataRow row)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				for (int i = 0; i < row.Table.Constraints.Count; i++)
				{
					if (row.Table.Constraints[i] is UniqueConstraint)
					{
						return;
					}
				}
				if (row.Table.ExtendedProperties.Contains("REFCursorName"))
				{
					if (this.m_dataAdapter != null && this.m_dataAdapter.SelectCommand != null)
					{
						ConfigBaseClass.StoredProcedureInfo storedProcInfo = ConfigBaseClass.GetInstance(true).GetStoredProcInfo(this.m_dataAdapter.SelectCommand.CommandText);
						if (storedProcInfo != null)
						{
							string text = (string)row.Table.ExtendedProperties["REFCursorName"];
							int num;
							if (text.Equals("REFCursor"))
							{
								num = 0;
							}
							else
							{
								num = int.Parse(text.Substring("RefCursor".Length));
							}
							if (num > -1)
							{
								RefCursorInfo refCursorInfo = storedProcInfo.GetRefCursorInfo(num);
								if (refCursorInfo.isPrimaryKeyPresent)
								{
									return;
								}
							}
						}
					}
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.BLR_NO_PRIMARYKEY, new string[0]));
				}
				if (this.m_dataAdapter == null || this.m_dataAdapter.SelectCommand == null)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.BLR_NO_PRIMARYKEY, new string[0]));
				}
				this.FillSchemaMetaData();
				this.CheckPrimaryKey();
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x06000388 RID: 904 RVA: 0x0001B44C File Offset: 0x0001964C
		private OracleDbType GetColumnType(int col)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			OracleDbType result;
			try
			{
				OracleDbType oracleDbType = OracleDbType.Varchar2;
				bool flag = true;
				if (!this.FillSchemaMetaData())
				{
					result = oracleDbType;
				}
				else
				{
					ColumnDescribeInfo columnDescribeInfo = this.m_sqlMetaData.m_columnDescribeInfo[col];
					short dataType = columnDescribeInfo.m_dataType;
					if (dataType == 2)
					{
						int scale = (int)columnDescribeInfo.m_scale;
						int precision = (int)columnDescribeInfo.m_precision;
						result = OraDb_DbTypeTable.ConvertNumberToOraDbType(precision, scale);
					}
					else
					{
						oracleDbType = (OracleDbType)OraDb_DbTypeTable.oraTypeToOracleDbTypeMapping[(int)dataType];
						if (columnDescribeInfo.m_characterSetForm != 2)
						{
							flag = false;
						}
						OracleDbType oracleDbType2 = oracleDbType;
						switch (oracleDbType2)
						{
						case OracleDbType.Char:
							if (flag)
							{
								oracleDbType = OracleDbType.NChar;
							}
							break;
						case OracleDbType.Clob:
							if (flag)
							{
								oracleDbType = OracleDbType.NClob;
							}
							break;
						default:
							if (oracleDbType2 == OracleDbType.Varchar2)
							{
								if (flag)
								{
									oracleDbType = OracleDbType.NVarchar2;
								}
							}
							break;
						}
						result = oracleDbType;
					}
				}
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06000389 RID: 905 RVA: 0x0001B534 File Offset: 0x00019734
		private OracleDbType GetColumnType(DataColumn col)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			OracleDbType result;
			try
			{
				OracleDbType oracleDbType = OracleDbType.Varchar2;
				object obj = col.ExtendedProperties["OraDbType"];
				string s;
				if (obj == null)
				{
					int num = this.FindBaseColumnOrdinal(col);
					if (num != -1)
					{
						oracleDbType = this.GetColumnType(num);
					}
				}
				else if ((s = (obj as string)) != null)
				{
					oracleDbType = (OracleDbType)int.Parse(s);
				}
				else
				{
					oracleDbType = (OracleDbType)obj;
				}
				result = oracleDbType;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0001B5D0 File Offset: 0x000197D0
		private string GetSchemaName()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			string result;
			try
			{
				StringBuilder stringBuilder = new StringBuilder();
				string text = null;
				if (!this.FillSchemaMetaData())
				{
					result = text;
				}
				else
				{
					text = this.m_sqlMetaData.m_sqlMetaInfo.m_schemaName;
					if (text != null && text.Length != 0)
					{
						if (this.m_caseSensitive)
						{
							stringBuilder.Append("\"");
							stringBuilder.Append(text);
							stringBuilder.Append("\"");
						}
						else
						{
							stringBuilder.Append(text);
						}
						stringBuilder.Append(".");
					}
					result = stringBuilder.ToString();
				}
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0001B698 File Offset: 0x00019898
		private string GetSchemaName(DataTable table)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			string result;
			try
			{
				StringBuilder stringBuilder = new StringBuilder();
				string text = null;
				object obj = table.ExtendedProperties["BaseSchema"];
				if (obj == null)
				{
					if (!this.FillSchemaMetaData())
					{
						return text;
					}
					text = this.m_sqlMetaData.m_sqlMetaInfo.m_schemaName;
				}
				else
				{
					text = (string)obj;
				}
				if (text != null && text.Length != 0)
				{
					if (this.m_caseSensitive)
					{
						stringBuilder.Append("\"");
						stringBuilder.Append(text);
						stringBuilder.Append("\"");
					}
					else
					{
						stringBuilder.Append(text);
					}
					stringBuilder.Append(".");
				}
				result = stringBuilder.ToString();
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0001B780 File Offset: 0x00019980
		private string GetBaseTableName()
		{
			return this.GetBaseTableName(this.m_caseSensitive);
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0001B790 File Offset: 0x00019990
		private string GetBaseTableName(bool caseSensitive)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			string result;
			try
			{
				string text = null;
				if (!this.FillSchemaMetaData())
				{
					result = text;
				}
				else
				{
					text = this.m_sqlMetaData.m_sqlMetaInfo.m_tableName;
					if (text == null)
					{
						throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.BLR_MULTITABLE_DS, new string[0]));
					}
					if (caseSensitive)
					{
						result = text.Insert(text.Length, "\"").Insert(0, "\"");
					}
					else
					{
						result = text;
					}
				}
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0001B840 File Offset: 0x00019A40
		private string GetBaseTableName(DataTable table)
		{
			return this.GetBaseTableName(table, this.m_caseSensitive);
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0001B850 File Offset: 0x00019A50
		private string GetBaseTableName(DataTable table, bool caseSensitive)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			string result;
			try
			{
				string text = null;
				object obj = table.ExtendedProperties["BaseTable.0"];
				if (obj == null)
				{
					if (!this.FillSchemaMetaData())
					{
						return text;
					}
					text = this.m_sqlMetaData.m_sqlMetaInfo.m_tableName;
				}
				else
				{
					text = (string)obj;
				}
				if (text == null)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.BLR_MULTITABLE_DS, new string[0]));
				}
				if (caseSensitive)
				{
					result = text.Insert(text.Length, "\"").Insert(0, "\"");
				}
				else
				{
					result = text;
				}
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0001B91C File Offset: 0x00019B1C
		private int FindBaseColumnOrdinal(DataColumn col)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			int result;
			try
			{
				string a = col.ColumnName;
				string tableName = col.Table.TableName;
				if (this.m_dataAdapter.TableMappings != null && this.m_dataAdapter.TableMappings.IndexOfDataSetTable(tableName) != -1)
				{
					DataTableMapping byDataSetTable = this.m_dataAdapter.TableMappings.GetByDataSetTable(tableName);
					if (byDataSetTable != null && byDataSetTable.ColumnMappings.IndexOfDataSetColumn(col.ColumnName) != -1)
					{
						DataColumnMapping byDataSetColumn = byDataSetTable.ColumnMappings.GetByDataSetColumn(col.ColumnName);
						if (byDataSetColumn != null)
						{
							a = byDataSetColumn.SourceColumn;
						}
					}
				}
				if (!this.FillSchemaMetaData() || this.m_sqlMetaData == null || this.m_sqlMetaData.m_columnDescribeInfo == null)
				{
					result = -1;
				}
				else
				{
					for (int i = 0; i < this.m_sqlMetaData.m_columnDescribeInfo.Length; i++)
					{
						if (this.m_sqlMetaData.m_columnDescribeInfo[i].pColAlias != null && a == this.m_sqlMetaData.m_columnDescribeInfo[i].pColAlias)
						{
							return i;
						}
					}
					result = -1;
				}
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06000391 RID: 913 RVA: 0x0001BA60 File Offset: 0x00019C60
		public new OracleCommand GetDeleteCommand()
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleCommand result;
			try
			{
				if (this.m_disposed)
				{
					throw new ObjectDisposedException(this.ToString());
				}
				if (this.m_dataAdapter == null || this.m_dataAdapter.SelectCommand == null)
				{
					throw new InvalidOperationException();
				}
				int num = 0;
				StringBuilder stringBuilder = new StringBuilder();
				OracleCommand oracleCommand = new OracleCommand();
				bool flag = false;
				if (this.m_sqlMetaData == null || !this.m_sqlMetaData.bPkFetched)
				{
					if (this.m_dataAdapter.SelectCommand.Connection == null)
					{
						throw new InvalidOperationException();
					}
					if (this.m_dataAdapter.SelectCommand.Connection.State == ConnectionState.Closed)
					{
						this.m_dataAdapter.SelectCommand.Connection.Open();
						flag = true;
					}
				}
				try
				{
					this.FillSchemaMetaData();
					this.CheckPrimaryKey();
					if (this.m_dataAdapter.SelectCommand.Connection.m_oracleConnectionImpl != null && this.m_dataAdapter.SelectCommand.Connection.m_oracleConnectionImpl.m_maxIdentifierLength > 0 && this.m_dataAdapter.SelectCommand.Connection.m_oracleConnectionImpl.m_maxIdentifierLength != OracleCommandBuilder.m_sMaxParamNameLen)
					{
						OracleCommandBuilder.m_sMaxParamNameLen = this.m_dataAdapter.SelectCommand.Connection.m_oracleConnectionImpl.m_maxIdentifierLength;
					}
				}
				finally
				{
					if (flag && this.m_dataAdapter.SelectCommand.Connection.State == ConnectionState.Open)
					{
						this.m_dataAdapter.SelectCommand.Connection.Close();
					}
				}
				int noOfColumns = (int)this.m_sqlMetaData.m_noOfColumns;
				int num2 = 2 * noOfColumns;
				int num3 = OracleCommandBuilder.m_sMaxParamNameLen - (6 + num2.ToString().Length);
				if (num3 < 0)
				{
					num3 = 0;
				}
				bool flag2 = this.m_sqlMetaData.m_sqlMetaInfo != null && this.m_sqlMetaData.m_sqlMetaInfo.m_columnMetaInfo != null;
				for (int i = 0; i < noOfColumns; i++)
				{
					if (this.m_numberOfHiddenColumns <= 0 || !flag2 || !(this.m_sqlMetaData.m_sqlMetaInfo.m_columnMetaInfo[i].m_columnName == "ROWID"))
					{
						string columnName = this.GetColumnName(i, true);
						string columnName2 = this.GetColumnName(i, false);
						OracleDbType columnType = this.GetColumnType(i);
						if (!this.IsOraLOB(columnType) && !this.IsOraLONG(columnType) && !this.IsOraXmlType(columnType) && columnName != null)
						{
							bool isNullAllowed;
							string text;
							if (isNullAllowed = this.m_sqlMetaData.m_columnDescribeInfo[i].m_isNullAllowed)
							{
								if (this.m_ODTDesignMode)
								{
									text = this.GenerateParameterName(":ori_", num3, columnName2, num);
									stringBuilder.Append(" ((");
									stringBuilder.Append(text);
									stringBuilder.Append(" IS NULL AND ");
									stringBuilder.Append(columnName);
									stringBuilder.Append(" IS NULL) OR");
									oracleCommand.Parameters.Add(this.CreateParams(text, columnName2, columnType, DataRowVersion.Original, 1));
								}
								else
								{
									text = this.GenerateParameterName(":ind_", num3, columnName2, num);
									stringBuilder.Append(" ((");
									stringBuilder.Append(text);
									stringBuilder.Append(" = 1 AND ");
									stringBuilder.Append(columnName);
									stringBuilder.Append(" IS NULL) OR");
									oracleCommand.Parameters.Add(this.CreateParams(text, null, OracleDbType.Int32, DataRowVersion.Current, 1));
								}
								num++;
							}
							text = this.GenerateParameterName(":ori_", num3, columnName2, num);
							stringBuilder.Append(" ");
							stringBuilder.Append(columnName);
							stringBuilder.Append("=");
							stringBuilder.Append(text);
							if (isNullAllowed)
							{
								stringBuilder.Append(")");
							}
							stringBuilder.Append(" AND");
							DataRowVersion version = DataRowVersion.Original;
							oracleCommand.Parameters.Add(this.CreateParams(text, columnName2, columnType, version, null));
							num++;
						}
					}
				}
				StringBuilder stringBuilder2 = new StringBuilder();
				stringBuilder2.Append("DELETE FROM ");
				stringBuilder2.Append(this.GetSchemaName());
				stringBuilder2.Append(this.GetBaseTableName());
				stringBuilder2.Append(" WHERE");
				stringBuilder2.Append(stringBuilder.ToString().TrimEnd("AND".ToCharArray()).TrimEnd(new char[0]));
				oracleCommand.CommandText = stringBuilder2.ToString();
				oracleCommand.Connection = this.m_dataAdapter.SelectCommand.Connection;
				oracleCommand.UpdatedRowSource = UpdateRowSource.None;
				result = oracleCommand;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0001BF58 File Offset: 0x0001A158
		private bool FillSchemaMetaData()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				if (this.m_dataAdapter == null || this.m_dataAdapter.SelectCommand == null)
				{
					throw new InvalidOperationException();
				}
				if (this.m_sqlMetaData == null || !this.m_sqlMetaData.bGotDescribeInfoFromDB)
				{
					if (this.m_dataAdapter.SelectCommand.Connection == null || this.m_dataAdapter.SelectCommand.Connection.m_oracleConnectionImpl == null || string.IsNullOrWhiteSpace(this.m_dataAdapter.SelectCommand.CommandText))
					{
						throw new InvalidOperationException();
					}
					this.m_sqlMetaData = this.m_dataAdapter.SelectCommand.DoDescribeSelectQuery(out this.m_numberOfHiddenColumns);
				}
				if (this.m_sqlMetaData != null && !this.m_sqlMetaData.bStmtParsed)
				{
					if (this.m_dataAdapter.SelectCommand.Connection == null || this.m_dataAdapter.SelectCommand.Connection.m_oracleConnectionImpl == null)
					{
						throw new InvalidOperationException();
					}
					SQLParser.GetSchemaMetaData(this.m_sqlMetaData, this.m_dataAdapter.SelectCommand.Connection, this.m_dataAdapter.SelectCommand.Connection.m_oracleConnectionImpl, this.m_numberOfHiddenColumns > 0);
				}
				result = (this.m_sqlMetaData != null);
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0001C0D4 File Offset: 0x0001A2D4
		internal OracleCommand GetDeleteCommand(DataRow row)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			OracleCommand deleteCmd;
			try
			{
				int num = 1;
				StringBuilder stringBuilder = new StringBuilder();
				DataTable table = row.Table;
				if (!table.ExtendedProperties.Contains("REFCursorName"))
				{
					this.GetBaseTableName();
				}
				else
				{
					this.CheckDataTable(table);
				}
				this.CheckPrimaryKey(row);
				if (this.m_cachedDeleteParams == null)
				{
					this.m_cachedDeleteParams = new ArrayList();
				}
				if (this.m_deleteCmd == null || this.m_deleteCmd.m_disposed)
				{
					if (this.m_deleteCmd != null)
					{
						int i = 0;
						while (i < this.m_cachedDeleteParams.Count)
						{
							if (((OracleParameter)this.m_cachedDeleteParams[i]).m_collRef != null)
							{
								this.m_cachedDeleteParams.RemoveAt(i);
							}
							else
							{
								i++;
							}
						}
					}
					this.m_deleteCmd = new OracleCommand();
				}
				else
				{
					if (this.m_deleteCmd.m_modified)
					{
						this.m_deleteCmd.ArrayBindCount = 0;
						this.m_deleteCmd.AddRowid = false;
						this.m_deleteCmd.BindByName = false;
						this.m_deleteCmd.CommandType = CommandType.Text;
						this.m_deleteCmd.FetchSize = 131072L;
						this.m_deleteCmd.m_initialLongFS = 0;
						this.m_deleteCmd.CommandTimeout = 0;
					}
					this.m_deleteCmd.Parameters.Clear();
				}
				foreach (object obj in table.Columns)
				{
					DataColumn dataColumn = (DataColumn)obj;
					string columnName = this.GetColumnName(dataColumn, true);
					string columnName2 = this.GetColumnName(dataColumn, false);
					OracleDbType columnType = this.GetColumnType(dataColumn);
					if (!this.IsOraLOB(columnType) && !this.IsOraLONG(columnType) && !this.IsOraXmlType(columnType) && columnName != null)
					{
						if (!this.IsRowOrigValueNull(row, dataColumn))
						{
							stringBuilder.Append(" ");
							stringBuilder.Append(columnName);
							stringBuilder.Append("=:");
							stringBuilder.Append(num);
							stringBuilder.Append(" AND");
							DataRowVersion version = DataRowVersion.Original;
							OracleParameter oracleParameter = null;
							if (this.m_cachedDeleteParams.Count > num - 1)
							{
								oracleParameter = (OracleParameter)this.m_cachedDeleteParams[num - 1];
								if (oracleParameter.m_disposed)
								{
									this.m_cachedDeleteParams.RemoveAt(num - 1);
									oracleParameter = null;
								}
							}
							if (oracleParameter != null)
							{
								this.SetParam(columnName2, columnType, version, row[dataColumn, DataRowVersion.Original], oracleParameter);
							}
							else
							{
								oracleParameter = this.CreateParams(null, columnName2, columnType, version, row[dataColumn, DataRowVersion.Original]);
								this.m_cachedDeleteParams.Insert(num - 1, oracleParameter);
							}
							this.m_deleteCmd.Parameters.Add(oracleParameter);
							num++;
						}
						else
						{
							stringBuilder.Append(" ");
							stringBuilder.Append(columnName);
							stringBuilder.Append(" IS NULL AND");
						}
					}
				}
				StringBuilder stringBuilder2 = new StringBuilder();
				stringBuilder2.Append("DELETE FROM ");
				stringBuilder2.Append(this.GetSchemaName(table));
				stringBuilder2.Append(this.GetBaseTableName(table));
				stringBuilder2.Append(" WHERE");
				stringBuilder2.Append(stringBuilder.ToString().TrimEnd("AND".ToCharArray()).TrimEnd(new char[0]));
				this.m_deleteCmd.CommandText = stringBuilder2.ToString();
				this.m_deleteCmd.Connection = this.DataAdapter.SelectCommand.Connection;
				this.m_deleteCmd.UpdatedRowSource = UpdateRowSource.None;
				deleteCmd = this.m_deleteCmd;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
			return deleteCmd;
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0001C4C0 File Offset: 0x0001A6C0
		public new OracleCommand GetInsertCommand()
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleCommand result;
			try
			{
				if (this.m_disposed)
				{
					throw new ObjectDisposedException(this.ToString());
				}
				if (this.m_dataAdapter == null || this.m_dataAdapter.SelectCommand == null)
				{
					throw new InvalidOperationException();
				}
				int num = 0;
				StringBuilder stringBuilder = new StringBuilder();
				StringBuilder stringBuilder2 = new StringBuilder();
				OracleCommand oracleCommand = new OracleCommand();
				bool flag = false;
				if (this.m_sqlMetaData == null || !this.m_sqlMetaData.bStmtParsed)
				{
					if (this.m_dataAdapter.SelectCommand.Connection == null)
					{
						throw new InvalidOperationException();
					}
					if (this.m_dataAdapter.SelectCommand.Connection.State == ConnectionState.Closed)
					{
						this.m_dataAdapter.SelectCommand.Connection.Open();
						flag = true;
					}
				}
				try
				{
					this.FillSchemaMetaData();
					if (this.m_dataAdapter.SelectCommand.Connection.m_oracleConnectionImpl != null && this.m_dataAdapter.SelectCommand.Connection.m_oracleConnectionImpl.m_maxIdentifierLength > 0 && this.m_dataAdapter.SelectCommand.Connection.m_oracleConnectionImpl.m_maxIdentifierLength != OracleCommandBuilder.m_sMaxParamNameLen)
					{
						OracleCommandBuilder.m_sMaxParamNameLen = this.m_dataAdapter.SelectCommand.Connection.m_oracleConnectionImpl.m_maxIdentifierLength;
					}
				}
				finally
				{
					if (flag && this.m_dataAdapter.SelectCommand.Connection.State == ConnectionState.Open)
					{
						this.m_dataAdapter.SelectCommand.Connection.Close();
					}
				}
				if (this.m_sqlMetaData == null)
				{
					result = null;
				}
				else
				{
					int noOfColumns = (int)this.m_sqlMetaData.m_noOfColumns;
					int num2 = noOfColumns;
					int num3 = OracleCommandBuilder.m_sMaxParamNameLen - (6 + num2.ToString().Length);
					if (num3 < 0)
					{
						num3 = 0;
					}
					bool flag2 = this.m_sqlMetaData.m_sqlMetaInfo != null && this.m_sqlMetaData.m_sqlMetaInfo.m_columnMetaInfo != null;
					for (int i = 0; i < noOfColumns; i++)
					{
						if (!flag2 || (this.m_sqlMetaData.m_sqlMetaInfo.m_columnMetaInfo[i].Updatable && (this.m_numberOfHiddenColumns <= 0 || !(this.m_sqlMetaData.m_sqlMetaInfo.m_columnMetaInfo[i].m_columnName == "ROWID"))))
						{
							OracleDbType columnType = this.GetColumnType(i);
							string columnName = this.GetColumnName(i, true);
							string columnName2 = this.GetColumnName(i, false);
							if (columnName != null)
							{
								stringBuilder.Append(" ");
								stringBuilder.Append(columnName);
								stringBuilder.Append(",");
								string text = this.GenerateParameterName(":cur_", num3, columnName2, num);
								stringBuilder2.Append(" ");
								stringBuilder2.Append(text);
								stringBuilder2.Append(",");
								oracleCommand.Parameters.Add(this.CreateParams(text, columnName2, columnType, DataRowVersion.Current, null));
								num++;
							}
						}
					}
					if (stringBuilder.Length == 0)
					{
						result = null;
					}
					else
					{
						StringBuilder stringBuilder3 = new StringBuilder();
						stringBuilder3.Append("INSERT INTO ");
						stringBuilder3.Append(this.GetSchemaName());
						stringBuilder3.Append(this.GetBaseTableName());
						stringBuilder3.Append("(");
						stringBuilder3.Append(stringBuilder.ToString().TrimEnd(new char[]
						{
							','
						}));
						stringBuilder3.Append(") VALUES (");
						stringBuilder3.Append(stringBuilder2.ToString().TrimEnd(new char[]
						{
							','
						}));
						stringBuilder3.Append(")");
						oracleCommand.CommandText = stringBuilder3.ToString();
						oracleCommand.Connection = this.DataAdapter.SelectCommand.Connection;
						oracleCommand.UpdatedRowSource = UpdateRowSource.None;
						result = oracleCommand;
					}
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0001C8FC File Offset: 0x0001AAFC
		public new OracleCommand GetUpdateCommand()
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleCommand result;
			try
			{
				if (this.m_disposed)
				{
					throw new ObjectDisposedException(this.ToString());
				}
				if (this.m_dataAdapter == null || this.m_dataAdapter.SelectCommand == null)
				{
					throw new InvalidOperationException();
				}
				int num = 0;
				StringBuilder stringBuilder = new StringBuilder();
				StringBuilder stringBuilder2 = new StringBuilder();
				OracleCommand oracleCommand = new OracleCommand();
				bool flag = false;
				if (this.m_sqlMetaData == null || !this.m_sqlMetaData.bPkFetched)
				{
					if (this.m_dataAdapter.SelectCommand.Connection == null)
					{
						throw new InvalidOperationException();
					}
					if (this.m_dataAdapter.SelectCommand.Connection.State == ConnectionState.Closed)
					{
						this.m_dataAdapter.SelectCommand.Connection.Open();
						flag = true;
					}
				}
				try
				{
					this.FillSchemaMetaData();
					this.CheckPrimaryKey();
					if (this.m_dataAdapter.SelectCommand.Connection.m_oracleConnectionImpl != null && this.m_dataAdapter.SelectCommand.Connection.m_oracleConnectionImpl.m_maxIdentifierLength > 0 && this.m_dataAdapter.SelectCommand.Connection.m_oracleConnectionImpl.m_maxIdentifierLength != OracleCommandBuilder.m_sMaxParamNameLen)
					{
						OracleCommandBuilder.m_sMaxParamNameLen = this.m_dataAdapter.SelectCommand.Connection.m_oracleConnectionImpl.m_maxIdentifierLength;
					}
				}
				finally
				{
					if (flag && this.m_dataAdapter.SelectCommand.Connection.State == ConnectionState.Open)
					{
						this.m_dataAdapter.SelectCommand.Connection.Close();
					}
				}
				int noOfColumns = (int)this.m_sqlMetaData.m_noOfColumns;
				int num2 = 3 * noOfColumns;
				int num3 = OracleCommandBuilder.m_sMaxParamNameLen - (6 + num2.ToString().Length);
				if (num3 < 0)
				{
					num3 = 0;
				}
				bool flag2 = this.m_sqlMetaData.m_sqlMetaInfo != null && this.m_sqlMetaData.m_sqlMetaInfo.m_columnMetaInfo != null;
				for (int i = 0; i < noOfColumns; i++)
				{
					if (!flag2 || (this.m_sqlMetaData.m_sqlMetaInfo.m_columnMetaInfo[i].Updatable && (this.m_numberOfHiddenColumns <= 0 || !(this.m_sqlMetaData.m_sqlMetaInfo.m_columnMetaInfo[i].m_columnName == "ROWID"))))
					{
						OracleDbType columnType = this.GetColumnType(i);
						string columnName = this.GetColumnName(i, true);
						string columnName2 = this.GetColumnName(i, false);
						if (columnName != null)
						{
							string text = this.GenerateParameterName(":cur_", num3, columnName2, num);
							stringBuilder.Append(" ");
							stringBuilder.Append(columnName);
							stringBuilder.Append("=");
							stringBuilder.Append(text);
							stringBuilder.Append(",");
							oracleCommand.Parameters.Add(this.CreateParams(text, columnName2, columnType, DataRowVersion.Current, null));
							num++;
						}
					}
				}
				if (stringBuilder.ToString().Length == 0)
				{
					result = null;
				}
				else
				{
					for (int j = 0; j < noOfColumns; j++)
					{
						OracleDbType columnType2 = this.GetColumnType(j);
						string columnName3 = this.GetColumnName(j, true);
						string columnName4 = this.GetColumnName(j, false);
						if ((!flag2 || this.m_numberOfHiddenColumns <= 0 || !(this.m_sqlMetaData.m_sqlMetaInfo.m_columnMetaInfo[j].m_columnName == "ROWID")) && !this.IsOraLOB(columnType2) && !this.IsOraLONG(columnType2) && !this.IsOraXmlType(columnType2) && columnName3 != null)
						{
							string text;
							bool isNullAllowed;
							if (isNullAllowed = this.m_sqlMetaData.m_columnDescribeInfo[j].m_isNullAllowed)
							{
								if (this.m_ODTDesignMode)
								{
									text = this.GenerateParameterName(":ori_", num3, columnName4, num);
									stringBuilder2.Append(" ((");
									stringBuilder2.Append(text);
									stringBuilder2.Append(" IS NULL AND ");
									stringBuilder2.Append(columnName3);
									stringBuilder2.Append(" IS NULL) OR");
									oracleCommand.Parameters.Add(this.CreateParams(text, columnName4, columnType2, DataRowVersion.Original, 1));
								}
								else
								{
									text = this.GenerateParameterName(":ind_", num3, columnName4, num);
									stringBuilder2.Append(" ((");
									stringBuilder2.Append(text);
									stringBuilder2.Append(" = 1 AND ");
									stringBuilder2.Append(columnName3);
									stringBuilder2.Append(" IS NULL) OR");
									oracleCommand.Parameters.Add(this.CreateParams(text, null, OracleDbType.Int32, DataRowVersion.Current, 1));
								}
								num++;
							}
							text = this.GenerateParameterName(":ori_", num3, columnName4, num);
							stringBuilder2.Append(" ");
							stringBuilder2.Append(columnName3);
							stringBuilder2.Append("=");
							stringBuilder2.Append(text);
							if (isNullAllowed)
							{
								stringBuilder2.Append(")");
							}
							stringBuilder2.Append(" AND");
							oracleCommand.Parameters.Add(this.CreateParams(text, columnName4, columnType2, DataRowVersion.Original, null));
							num++;
						}
					}
					StringBuilder stringBuilder3 = new StringBuilder();
					stringBuilder3.Append("UPDATE ");
					stringBuilder3.Append(this.GetSchemaName());
					stringBuilder3.Append(this.GetBaseTableName());
					stringBuilder3.Append(" SET");
					stringBuilder3.Append(stringBuilder.ToString().TrimEnd(new char[]
					{
						','
					}));
					stringBuilder3.Append(" WHERE");
					stringBuilder3.Append(stringBuilder2.ToString().TrimEnd("AND".ToCharArray()).TrimEnd(new char[0]));
					oracleCommand.CommandText = stringBuilder3.ToString();
					oracleCommand.Connection = this.DataAdapter.SelectCommand.Connection;
					oracleCommand.UpdatedRowSource = UpdateRowSource.None;
					result = oracleCommand;
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0001CF44 File Offset: 0x0001B144
		public override string QuoteIdentifier(string unquotedIdentifier)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			string result;
			try
			{
				if (unquotedIdentifier == null)
				{
					throw new ArgumentNullException();
				}
				if (unquotedIdentifier.Length == 0)
				{
					result = this.QuotePrefix + this.QuoteSuffix;
				}
				else
				{
					result = string.Format("{0}{1}{2}", this.QuotePrefix, unquotedIdentifier.Replace("\"", "\"\""), this.QuoteSuffix);
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0001D000 File Offset: 0x0001B200
		public override string UnquoteIdentifier(string quotedIdentifier)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			string result;
			try
			{
				if (quotedIdentifier == null)
				{
					throw new ArgumentNullException();
				}
				int length = quotedIdentifier.Length;
				if (length < 2 || quotedIdentifier[0] != '"' || quotedIdentifier[length - 1] != '"')
				{
					throw new ArgumentException();
				}
				result = quotedIdentifier.Substring(1, length - 2).Replace("\"\"", "\"");
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0001D0BC File Offset: 0x0001B2BC
		public override void RefreshSchema()
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (this.m_dataAdapter != null)
				{
					this.m_dataAdapter.InsertCommand = null;
					this.m_dataAdapter.UpdateCommand = null;
					this.m_dataAdapter.DeleteCommand = null;
					this.m_sqlMetaData = null;
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0001D160 File Offset: 0x0001B360
		protected override void ApplyParameterInfo(DbParameter parameter, DataRow row, StatementType statementType, bool whereClause)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				OracleParameter oracleParameter = parameter as OracleParameter;
				if (oracleParameter != null)
				{
					oracleParameter.OracleDbType = (OracleDbType)row["ProviderType"];
					oracleParameter.IsNullable = (bool)row["AllowDBNull"];
					oracleParameter.Size = (int)row["ColumnSize"];
					oracleParameter.SourceColumn = (string)row["ColumnName"];
					object obj = row["NumericScale"];
					if (obj != null && obj != DBNull.Value)
					{
						oracleParameter.Scale = (byte)((short)obj);
					}
					obj = row["NumericPrecision"];
					if (obj != null && obj != DBNull.Value)
					{
						oracleParameter.Precision = (byte)((short)obj);
					}
					if (ParameterDirection.Input == oracleParameter.Direction && parameter.Size == 0)
					{
						(parameter as OracleParameter).SetSize(-1);
					}
				}
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0001D27C File Offset: 0x0001B47C
		private bool IsOraLOB(OracleDbType colType)
		{
			return colType == OracleDbType.Blob || colType == OracleDbType.Clob || colType == OracleDbType.NClob || colType == OracleDbType.BFile;
		}

		// Token: 0x0600039B RID: 923 RVA: 0x0001D294 File Offset: 0x0001B494
		private bool IsOraLONG(OracleDbType colType)
		{
			return colType == OracleDbType.Long || colType == OracleDbType.LongRaw;
		}

		// Token: 0x0600039C RID: 924 RVA: 0x0001D2A4 File Offset: 0x0001B4A4
		private bool IsOraXmlType(OracleDbType colType)
		{
			return colType == OracleDbType.XmlType;
		}

		// Token: 0x0600039D RID: 925 RVA: 0x0001D2AC File Offset: 0x0001B4AC
		private bool IsRowOrigValueNull(DataRow row, DataColumn col)
		{
			object obj = row[col, DataRowVersion.Original];
			return obj == DBNull.Value || (obj is INullable && (obj as INullable).IsNull);
		}

		// Token: 0x0600039E RID: 926 RVA: 0x0001D2E8 File Offset: 0x0001B4E8
		private string GenerateParameterName(string prefix, int srcColumnLen, string srcColumn, int paramId)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			string result;
			try
			{
				string text = Regex.Replace(srcColumn, "[^\\w]", "");
				int byteCount = Encoding.UTF8.GetByteCount(text);
				if (srcColumnLen < byteCount)
				{
					char[] array = text.ToCharArray();
					int num = 0;
					StringBuilder stringBuilder = new StringBuilder(srcColumnLen);
					foreach (char value in array)
					{
						int byteCount2 = Encoding.UTF8.GetByteCount(value.ToString());
						if (num + byteCount2 > srcColumnLen)
						{
							break;
						}
						stringBuilder.Append(value);
						num += byteCount2;
					}
					if (stringBuilder.Length > 0)
					{
						text = stringBuilder.ToString();
					}
				}
				StringBuilder stringBuilder2 = new StringBuilder(128);
				stringBuilder2.Append(prefix);
				stringBuilder2.Append(text);
				stringBuilder2.Append("_p");
				stringBuilder2.Append(paramId);
				result = stringBuilder2.ToString();
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0001D408 File Offset: 0x0001B608
		private OracleParameter CreateParams(string paramName, string colName, OracleDbType colType, DataRowVersion version, object value)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			OracleParameter result;
			try
			{
				int size = -1;
				if (value != null)
				{
					string text;
					char[] array;
					byte[] array2;
					if (value is char)
					{
						size = 1;
					}
					else if ((text = (value as string)) != null)
					{
						size = text.Length;
					}
					else if ((array = (value as char[])) != null)
					{
						size = array.Length;
					}
					else if ((array2 = (value as byte[])) != null)
					{
						size = array2.Length;
					}
				}
				if (colType == OracleDbType.Clob)
				{
					if (!(value is OracleClob))
					{
						colType = OracleDbType.Varchar2;
					}
				}
				else if (colType == OracleDbType.NClob)
				{
					if (!(value is OracleClob))
					{
						colType = OracleDbType.NVarchar2;
					}
				}
				else if (colType == OracleDbType.Blob && !(value is OracleBlob))
				{
					colType = OracleDbType.Raw;
				}
				result = new OracleParameter(paramName, colType, size, colName, version, value)
				{
					m_modified = false
				};
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0001D4F8 File Offset: 0x0001B6F8
		protected override DbCommand InitializeCommand(DbCommand command)
		{
			return base.InitializeCommand(command);
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x0001D504 File Offset: 0x0001B704
		protected override string GetParameterName(int parameterOrdinal)
		{
			return "p" + parameterOrdinal.ToString();
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0001D518 File Offset: 0x0001B718
		protected override string GetParameterName(string parameterName)
		{
			return parameterName;
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x0001D51C File Offset: 0x0001B71C
		protected override string GetParameterPlaceholder(int parameterOrdinal)
		{
			return ":p" + parameterOrdinal.ToString();
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0001D530 File Offset: 0x0001B730
		protected override DataTable GetSchemaTable(DbCommand srcCommand)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			DataTable schemaTable;
			try
			{
				OracleCommand oracleCommand = srcCommand as OracleCommand;
				OracleDataReader oracleDataReader = oracleCommand.ExecuteReader(CommandBehavior.SchemaOnly | CommandBehavior.KeyInfo);
				schemaTable = oracleDataReader.GetSchemaTable();
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
			return schemaTable;
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x0001D598 File Offset: 0x0001B798
		protected override void SetRowUpdatingHandler(DbDataAdapter adapter)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (adapter == null)
				{
					throw new ArgumentNullException();
				}
				if (this.m_dataAdapter == adapter)
				{
					((OracleDataAdapter)adapter).RowUpdating -= this.m_handler;
				}
				else
				{
					((OracleDataAdapter)adapter).RowUpdating += this.m_handler;
					this.m_dataAdapter = (adapter as OracleDataAdapter);
				}
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060003A6 RID: 934 RVA: 0x0001D62C File Offset: 0x0001B82C
		// (set) Token: 0x060003A7 RID: 935 RVA: 0x0001D634 File Offset: 0x0001B834
		[DefaultValue(true)]
		[Description("")]
		public bool CaseSensitive
		{
			get
			{
				return this.m_caseSensitive;
			}
			set
			{
				this.m_caseSensitive = value;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060003A8 RID: 936 RVA: 0x0001D640 File Offset: 0x0001B840
		// (set) Token: 0x060003A9 RID: 937 RVA: 0x0001D648 File Offset: 0x0001B848
		[DefaultValue(null)]
		[Description("")]
		public new OracleDataAdapter DataAdapter
		{
			get
			{
				return this.m_dataAdapter;
			}
			set
			{
				if (this.m_dataAdapter != value)
				{
					if (this.m_dataAdapter != null)
					{
						this.m_dataAdapter.RowUpdating -= this.m_handler;
					}
					this.m_dataAdapter = value;
					if (this.m_dataAdapter != null)
					{
						this.m_disposed = false;
						this.m_dataAdapter.RowUpdating += this.m_handler;
					}
				}
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060003AA RID: 938 RVA: 0x0001D6A0 File Offset: 0x0001B8A0
		// (set) Token: 0x060003AB RID: 939 RVA: 0x0001D6A8 File Offset: 0x0001B8A8
		[DefaultValue("\"")]
		public override string QuoteSuffix
		{
			get
			{
				return "\"";
			}
			set
			{
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060003AC RID: 940 RVA: 0x0001D6AC File Offset: 0x0001B8AC
		// (set) Token: 0x060003AD RID: 941 RVA: 0x0001D6B4 File Offset: 0x0001B8B4
		[DefaultValue(".")]
		public override string SchemaSeparator
		{
			get
			{
				return ".";
			}
			set
			{
				if (value != ".")
				{
					throw new NotSupportedException();
				}
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060003AE RID: 942 RVA: 0x0001D6CC File Offset: 0x0001B8CC
		// (set) Token: 0x060003AF RID: 943 RVA: 0x0001D6D4 File Offset: 0x0001B8D4
		[DefaultValue("\"")]
		public override string QuotePrefix
		{
			get
			{
				return "\"";
			}
			set
			{
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060003B0 RID: 944 RVA: 0x0001D6D8 File Offset: 0x0001B8D8
		// (set) Token: 0x060003B1 RID: 945 RVA: 0x0001D6DC File Offset: 0x0001B8DC
		public override CatalogLocation CatalogLocation
		{
			get
			{
				return CatalogLocation.End;
			}
			set
			{
				if (CatalogLocation.End != value)
				{
					throw new NotSupportedException();
				}
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060003B2 RID: 946 RVA: 0x0001D6E8 File Offset: 0x0001B8E8
		// (set) Token: 0x060003B3 RID: 947 RVA: 0x0001D6F0 File Offset: 0x0001B8F0
		public override string CatalogSeparator
		{
			get
			{
				return "@";
			}
			set
			{
				if ("@" != value)
				{
					throw new NotSupportedException();
				}
			}
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x0001D708 File Offset: 0x0001B908
		private void RowUpdating(object src, OracleRowUpdatingEventArgs arg)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			DataRow row = arg.Row;
			try
			{
				switch (arg.StatementType)
				{
				case StatementType.Insert:
					if (this.DataAdapter.InsertCommand == null)
					{
						arg.Command = this.GetInsertCommand(row);
					}
					break;
				case StatementType.Update:
					if (this.DataAdapter.UpdateCommand == null)
					{
						arg.Command = this.GetUpdateCommand(row);
					}
					else
					{
						int count = arg.Command.Parameters.Count;
						for (int i = 0; i < count - 1; i++)
						{
							OracleParameter oracleParameter = arg.Command.Parameters[i];
							if (oracleParameter.SourceColumn.Length == 0)
							{
								object value = arg.Command.Parameters[i + 1].Value;
								if (value == DBNull.Value || (value is INullable && (value as INullable).IsNull))
								{
									oracleParameter.Value = 1;
								}
								else
								{
									oracleParameter.Value = 0;
								}
							}
						}
					}
					break;
				case StatementType.Delete:
					if (this.DataAdapter.DeleteCommand == null)
					{
						arg.Command = this.GetDeleteCommand(row);
					}
					else
					{
						int count2 = arg.Command.Parameters.Count;
						for (int j = 0; j < count2 - 1; j++)
						{
							OracleParameter oracleParameter2 = arg.Command.Parameters[j];
							if (oracleParameter2.SourceColumn.Length == 0)
							{
								object value2 = arg.Command.Parameters[j + 1].Value;
								if (value2 == DBNull.Value || (value2 is INullable && (value2 as INullable).IsNull))
								{
									oracleParameter2.Value = 1;
								}
								else
								{
									oracleParameter2.Value = 0;
								}
							}
						}
					}
					break;
				default:
					throw new ArgumentException();
				}
			}
			catch (Exception ex)
			{
				row.RowError = ex.Message;
				if (!this.m_dataAdapter.ContinueUpdateOnError)
				{
					throw;
				}
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x0001D96C File Offset: 0x0001BB6C
		private string GetColumnName(DataColumn col, bool baseColumn)
		{
			return this.GetColumnName(col, baseColumn, this.m_caseSensitive);
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x0001D97C File Offset: 0x0001BB7C
		private string GetColumnName(int col, bool baseColumn)
		{
			return this.GetColumnName(col, baseColumn, this.m_caseSensitive);
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0001D98C File Offset: 0x0001BB8C
		private string GetColumnName(int col, bool baseColumn, bool caseSensitive)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			string result;
			try
			{
				string text = null;
				if (baseColumn)
				{
					if (this.m_sqlMetaData.m_sqlMetaInfo != null && this.m_sqlMetaData.m_sqlMetaInfo.m_columnMetaInfo != null)
					{
						text = this.m_sqlMetaData.m_sqlMetaInfo.m_columnMetaInfo[col].m_columnName;
					}
				}
				else
				{
					text = this.m_sqlMetaData.m_columnDescribeInfo[col].pColAlias;
				}
				if (text != null && caseSensitive && baseColumn)
				{
					result = text.Insert(text.Length, "\"").Insert(0, "\"");
				}
				else
				{
					result = text;
				}
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x0001DA58 File Offset: 0x0001BC58
		private string GetColumnName(DataColumn col, bool baseColumn, bool caseSensitive)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			string result;
			try
			{
				string text = (string)col.ExtendedProperties["BaseColumn"];
				if (text == null || !baseColumn)
				{
					int num = this.FindBaseColumnOrdinal(col);
					if (num == -1)
					{
						return null;
					}
					if (baseColumn)
					{
						if (this.m_sqlMetaData.m_sqlMetaInfo != null && this.m_sqlMetaData.m_sqlMetaInfo.m_columnMetaInfo != null && !this.m_sqlMetaData.m_sqlMetaInfo.m_columnMetaInfo[num].bIsExpression)
						{
							text = this.m_sqlMetaData.m_sqlMetaInfo.m_columnMetaInfo[num].m_columnName;
						}
					}
					else
					{
						text = this.m_sqlMetaData.m_columnDescribeInfo[num].pColAlias;
					}
				}
				if (text != null && this.m_caseSensitive && baseColumn)
				{
					result = text.Insert(text.Length, "\"").Insert(0, "\"");
				}
				else
				{
					result = text;
				}
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0001DB74 File Offset: 0x0001BD74
		protected override void Dispose(bool disposing)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (!this.m_disposed)
				{
					try
					{
						if (disposing)
						{
							this.m_deleteCmd = null;
							this.m_insertCmd = null;
							this.m_updateCmd = null;
							if (this.m_dataAdapter != null)
							{
								try
								{
									this.m_dataAdapter.RowUpdating -= this.m_handler;
								}
								catch
								{
								}
								this.m_dataAdapter = null;
							}
							if (this.m_cachedInsertParams != null)
							{
								try
								{
									this.m_cachedInsertParams.Clear();
								}
								catch
								{
								}
								this.m_cachedInsertParams = null;
							}
							if (this.m_cachedUpdateParams != null)
							{
								try
								{
									this.m_cachedUpdateParams.Clear();
								}
								catch
								{
								}
								this.m_cachedUpdateParams = null;
							}
							if (this.m_cachedDeleteParams != null)
							{
								try
								{
									this.m_cachedDeleteParams.Clear();
								}
								catch
								{
								}
								this.m_cachedDeleteParams = null;
							}
						}
						this.m_disposed = true;
					}
					finally
					{
						try
						{
							base.Dispose(disposing);
						}
						catch
						{
						}
					}
				}
			}
			catch (Exception ex)
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Error, new string[]
					{
						ex.Message
					});
				}
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x060003BA RID: 954 RVA: 0x0001DD00 File Offset: 0x0001BF00
		private bool IsRowCurrentValueNull(DataRow row, DataColumn col)
		{
			object obj = row[col, DataRowVersion.Current];
			return obj == DBNull.Value || (obj is INullable && (obj as INullable).IsNull);
		}

		// Token: 0x060003BB RID: 955 RVA: 0x0001DD3C File Offset: 0x0001BF3C
		private bool IsColumnModified(DataRow row, DataColumn col)
		{
			return !row[col, DataRowVersion.Current].Equals(row[col, DataRowVersion.Original]);
		}

		// Token: 0x04000558 RID: 1368
		private const string QUOTE = "\"";

		// Token: 0x04000559 RID: 1369
		private const string SCHEMA_SEPERATOR = ".";

		// Token: 0x0400055A RID: 1370
		private const int NO_OF_PARAMS = 10;

		// Token: 0x0400055B RID: 1371
		private const int NAME_IN = 0;

		// Token: 0x0400055C RID: 1372
		private const int PARAM_COUNT_IN = 1;

		// Token: 0x0400055D RID: 1373
		private const int PARAM_COUNT_OUT = 2;

		// Token: 0x0400055E RID: 1374
		private const int PARAM_NAME_OUT = 3;

		// Token: 0x0400055F RID: 1375
		private const int DIRECTION_OUT = 4;

		// Token: 0x04000560 RID: 1376
		private const int ORADBTYPE_OUT = 5;

		// Token: 0x04000561 RID: 1377
		private const int SIZE_OUT = 6;

		// Token: 0x04000562 RID: 1378
		private const int TYPE_NAME_OUT = 7;

		// Token: 0x04000563 RID: 1379
		private const int POSITION_OUT = 8;

		// Token: 0x04000564 RID: 1380
		private const int DATA_LEVEL_OUT = 9;

		// Token: 0x04000565 RID: 1381
		private const int MAX_ARG_NAME_LENGTH = 128;

		// Token: 0x04000566 RID: 1382
		private const int FIRST_FETCH_COUNT = 128;

		// Token: 0x04000567 RID: 1383
		private const string DP_COMMAND_TEXT = "DECLARE TYPE NUM_TABLE IS TABLE OF NUMBER INDEX BY PLS_INTEGER; TYPE STR_TABLE IS TABLE OF VARCHAR2(128) INDEX BY PLS_INTEGER; name_in          VARCHAR2(2000); param_count_in   PLS_INTEGER; link\t     \t      VARCHAR2(2000); context          NUMBER := 1; schema           VARCHAR2(2000); part1            VARCHAR2(2000); part2            VARCHAR2(2000); dblink           VARCHAR2(2000); part1_type       NUMBER; object_number    NUMBER; refcur\t          SYS_REFCURSOR; idx\t\t          PLS_INTEGER := 0; proc_count       PLS_INTEGER := 0; procrefcur\t      SYS_REFCURSOR; procobjectnames  ALL_PROCEDURES.OBJECT_NAME%TYPE ; param_count_out        PLS_INTEGER := 0; param_name_out         STR_TABLE; direction_out          NUM_TABLE; oradbtype_out          NUM_TABLE; size_out               NUM_TABLE; type_name_out          STR_TABLE; position_out           NUM_TABLE; data_level_out         NUM_TABLE; param_name_null_out    STR_TABLE; direction_null_out     NUM_TABLE; oradbtype_null_out     NUM_TABLE; size_null_out          NUM_TABLE; type_name_null_out     STR_TABLE; position_null_out      NUM_TABLE; data_level_null_out    NUM_TABLE; BEGIN name_in := :1; param_count_in := :2; link := NULL; DBMS_UTILITY.NAME_RESOLVE( name_in, context, schema, part1, part2, dblink, part1_type, object_number); WHILE (dblink IS NOT NULL) LOOP link := '@' || dblink; name_in := NULL; IF (schema IS NOT NULL) THEN name_in := '\"' || schema || '\"' || '.'; END IF; IF (part1 IS NOT NULL) THEN name_in := name_in || '\"' || part1 || '\"' || '.'; END IF; IF (part2 IS NULL) THEN name_in := RTRIM(name_in, '.'); ELSE name_in := name_in || '\"' || part2 || '\"'; END IF; EXECUTE IMMEDIATE 'BEGIN DBMS_UTILITY.NAME_RESOLVE' || link || '(:1, :2, :3, :4, :5, :6, :7, :8); END;' USING name_in, context, OUT schema, OUT part1, OUT part2, OUT dblink, OUT part1_type, OUT object_number; END LOOP; IF (param_count_out = 0) THEN OPEN refcur FOR 'SELECT  DECODE (POSITION, 0, ''RETURN_VALUE'', ARGUMENT_NAME) param_name, DECODE(IN_OUT, ''IN'', \t  1,  ''IN/OUT'', 3, ''OUT'', DECODE(ARGUMENT_NAME, NULL, 6, 2), 1) direction, DECODE(DATA_TYPE, ''BINARY_DOUBLE'',  132, ''BINARY_FLOAT'',   133, ''BINARY_INTEGER'', 112, ''BFILE'',     101, ''BLOB'',\t    102, ''CHAR'',\t    104, ''CLOB'',\t    105, ''DATE'',\t    106, ''FLOAT'',\t    107, ''INTERVAL YEAR TO MONTH'', 115, ''INTERVAL DAY TO SECOND'', 114, ''LONG'',\t    109, ''LONG RAW'',\t110, ''NCHAR'',\t    117, ''NCLOB'',\t    116, ''NUMBER'',\t  107, ''NVARCHAR2'',\t119, ''PL/SQL BOOLEAN'',   134, ''RAW'',\t      120, ''REF CURSOR'',121, ''ROWID'',\t    126, ''TIMESTAMP'',\t123, ''TIMESTAMP WITH LOCAL TIME ZONE'', 124, ''TIMESTAMP WITH TIME ZONE'', 125, ''OPAQUE/XMLTYPE'', 127, ''UNDEFINED'', 100, ''UROWID'',\t  126, ''VARCHAR'', \t126, ''VARCHAR2'',\t126, ''PL/SQL TABLE'',1, NULL,            0, -1) oradbtype, DECODE(DATA_TYPE, ''CHAR'', \t      2000, ''LONG'',      32760, ''LONG RAW'',    32760, ''NCHAR'', \t    2000, ''NVARCHAR2'',   32767, ''RAW'', \t      2000, ''ROWID'', \t    4000, ''UROWID'',\t    4000, ''VARCHAR'', \t  32767, ''VARCHAR2'', \t  32767, ''PL/SQL TABLE'',  16, 0) length, (TYPE_OWNER ||  DECODE(TYPE_OWNER, NULL, NULL, ''.'') ||  TYPE_NAME ) type_name, POSITION   position, DATA_LEVEL data_level FROM ALL_ARGUMENTS' || link || ' WHERE OWNER \t\t  = :1 \t  AND (PACKAGE_NAME \t= :2 \t  OR (:3 IS NULL AND PACKAGE_NAME is null)) AND OBJECT_NAME \t  = :4 \t  AND NVL(overload, 1) = 1 ORDER BY SEQUENCE' USING schema, part1, part1, part2; FETCH refcur BULK COLLECT INTO param_name_out, direction_out, oradbtype_out, size_out, type_name_out, position_out, data_level_out; param_count_out := refcur%ROWCOUNT; CLOSE refcur; END IF; IF (part1_type = 9 AND param_count_out = 0) THEN OPEN procrefcur FOR 'SELECT OBJECT_NAME FROM ALL_PROCEDURES' || link || ' WHERE OWNER = :1 AND (((OBJECT_NAME = :2) AND (PROCEDURE_NAME IS NULL)) OR ((OBJECT_NAME = :3) AND (PROCEDURE_NAME = :4)))' USING schema, part2, part1, part2; FETCH procrefcur INTO procobjectnames;proc_count := procrefcur%ROWCOUNT; CLOSE procrefcur; IF (proc_count = 0) THEN param_count_out := -1002; END IF; END IF; :3 := param_count_out; IF (param_count_out > param_count_in OR param_count_out < 0) THEN param_name_out:= param_name_null_out; direction_out := direction_null_out; oradbtype_out := oradbtype_null_out; size_out      := size_null_out; type_name_out := type_name_null_out; position_out  := position_null_out; data_level_out:= data_level_null_out; END IF; :4 := param_name_out; :5 := direction_out; :6 := oradbtype_out; :7 := size_out; :8 := type_name_out; :9 := position_out; :10:= data_level_out; END;";

		// Token: 0x04000568 RID: 1384
		private OracleDataAdapter m_dataAdapter;

		// Token: 0x04000569 RID: 1385
		private bool m_caseSensitive;

		// Token: 0x0400056A RID: 1386
		private OracleRowUpdatingEventHandler m_handler;

		// Token: 0x0400056B RID: 1387
		private OracleCommand m_deleteCmd;

		// Token: 0x0400056C RID: 1388
		private OracleCommand m_insertCmd;

		// Token: 0x0400056D RID: 1389
		private OracleCommand m_updateCmd;

		// Token: 0x0400056E RID: 1390
		private ArrayList m_cachedInsertParams;

		// Token: 0x0400056F RID: 1391
		private ArrayList m_cachedUpdateParams;

		// Token: 0x04000570 RID: 1392
		private ArrayList m_cachedDeleteParams;

		// Token: 0x04000571 RID: 1393
		private bool m_disposed;

		// Token: 0x04000572 RID: 1394
		private static object m_dpLock = new object();

		// Token: 0x04000573 RID: 1395
		private static OracleCommand m_dpCommand;

		// Token: 0x04000574 RID: 1396
		private static OracleParameter[] m_dpCommandParams;

		// Token: 0x04000575 RID: 1397
		private bool m_ODTDesignMode;

		// Token: 0x04000576 RID: 1398
		private static int m_sMaxParamNameLen = 30;

		// Token: 0x04000577 RID: 1399
		private SQLMetaData m_sqlMetaData;

		// Token: 0x04000578 RID: 1400
		private int m_numberOfHiddenColumns;
	}
}
