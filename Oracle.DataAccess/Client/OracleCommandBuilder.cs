using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Oracle.DataAccess.Types;

namespace Oracle.DataAccess.Client
{
	// Token: 0x0200014F RID: 335
	[ToolboxBitmap(typeof(resfinder), "Oracle.DataAccess.src.Client.Icons.OracleCommandBuilderToolBox_hc.bmp")]
	public sealed class OracleCommandBuilder : DbCommandBuilder
	{
		// Token: 0x06000D04 RID: 3332 RVA: 0x00086638 File Offset: 0x00085638
		static OracleCommandBuilder()
		{
			if (!OracleInit.bSetDllDirectoryInvoked)
			{
				OracleInit.Initialize();
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000D05 RID: 3333 RVA: 0x0008665D File Offset: 0x0008565D
		// (set) Token: 0x06000D06 RID: 3334 RVA: 0x00086668 File Offset: 0x00085668
		[Description("")]
		[DefaultValue(null)]
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
						this.m_dataAdapter.RowUpdating -= this.m_hndr;
					}
					this.m_dataAdapter = value;
					if (this.m_dataAdapter != null)
					{
						this.m_disposed = false;
						this.m_dataAdapter.RowUpdating += this.m_hndr;
					}
				}
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000D07 RID: 3335 RVA: 0x000866BE File Offset: 0x000856BE
		// (set) Token: 0x06000D08 RID: 3336 RVA: 0x000866C6 File Offset: 0x000856C6
		[Description("")]
		[DefaultValue(true)]
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

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000D09 RID: 3337 RVA: 0x000866CF File Offset: 0x000856CF
		// (set) Token: 0x06000D0A RID: 3338 RVA: 0x000866D6 File Offset: 0x000856D6
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

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000D0B RID: 3339 RVA: 0x000866EB File Offset: 0x000856EB
		// (set) Token: 0x06000D0C RID: 3340 RVA: 0x000866F2 File Offset: 0x000856F2
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

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000D0D RID: 3341 RVA: 0x000866F4 File Offset: 0x000856F4
		// (set) Token: 0x06000D0E RID: 3342 RVA: 0x000866FB File Offset: 0x000856FB
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

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000D0F RID: 3343 RVA: 0x000866FD File Offset: 0x000856FD
		// (set) Token: 0x06000D10 RID: 3344 RVA: 0x00086700 File Offset: 0x00085700
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

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000D11 RID: 3345 RVA: 0x0008670C File Offset: 0x0008570C
		// (set) Token: 0x06000D12 RID: 3346 RVA: 0x00086713 File Offset: 0x00085713
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

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000D13 RID: 3347 RVA: 0x00086728 File Offset: 0x00085728
		// (set) Token: 0x06000D14 RID: 3348 RVA: 0x00086730 File Offset: 0x00085730
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

		// Token: 0x06000D15 RID: 3349 RVA: 0x0008673C File Offset: 0x0008573C
		public OracleCommandBuilder()
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleCommandBuilder::OracleCommandBuilder()\n"
				});
			}
			this.m_caseSensitive = true;
			this.m_hndr = new OracleRowUpdatingEventHandler(this.RowUpdating);
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleCommandBuilder::OracleCommandBuilder()\n"
				});
			}
		}

		// Token: 0x06000D16 RID: 3350 RVA: 0x000867A4 File Offset: 0x000857A4
		public OracleCommandBuilder(OracleDataAdapter da)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleCommandBuilder::OracleCommandBuilder()\n"
				});
			}
			this.m_dataAdapter = da;
			this.m_caseSensitive = true;
			this.m_hndr = new OracleRowUpdatingEventHandler(this.RowUpdating);
			if (da != null)
			{
				da.RowUpdating += this.m_hndr;
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleCommandBuilder::OracleCommandBuilder()\n"
				});
			}
		}

		// Token: 0x06000D17 RID: 3351 RVA: 0x00086820 File Offset: 0x00085820
		public static void DeriveParameters(OracleCommand command)
		{
			DeriveParamInfo deriveParamInfo = null;
			if (command == null)
			{
				throw new ArgumentNullException("command");
			}
			if (command.CommandType != CommandType.StoredProcedure)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.ODP_NOT_SUPPORTED, new string[]
				{
					"OracleCommandBuilder.DeriveParameters",
					command.CommandType.ToString()
				}));
			}
			string commandText = command.CommandText;
			OracleConnection connection = command.Connection;
			if (connection == null || connection.m_state != ConnectionState.Open)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
			}
			if (commandText == null || commandText.Length == 0)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.ODP_INVALID_VALUE, new string[]
				{
					"OracleCommand.CommandText"
				}));
			}
			if ((deriveParamInfo = (DeriveParamInfo)DeriveParamInfo.m_pooler.Get(connection.m_internalConStr, commandText)) == null)
			{
				bool flag = false;
				try
				{
					Monitor.Enter(OracleCommandBuilder.m_staticLock);
					flag = true;
					if ((deriveParamInfo = (DeriveParamInfo)DeriveParamInfo.m_pooler.Get(connection.m_internalConStr, commandText)) == null)
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
						OracleCommandBuilder.m_dpCommand.AddToStatementCache = command.AddToStatementCache;
						if (connection.m_majorVersion == 8)
						{
							OracleCommandBuilder.m_dpCommand.CommandText = "DECLARE TYPE NUM_TABLE IS TABLE OF NUMBER INDEX BY BINARY_INTEGER; TYPE STR_TABLE IS TABLE OF VARCHAR2(128) INDEX BY BINARY_INTEGER; TYPE REF_CURSOR IS REF CURSOR; name_in          VARCHAR2(2000); param_count_in   PLS_INTEGER; link\t     \t      VARCHAR2(2000); context          NUMBER := 1; schema           VARCHAR2(2000); part1            VARCHAR2(2000); part2            VARCHAR2(2000); dblink           VARCHAR2(2000); part1_type       NUMBER; object_number    NUMBER; refcur           REF_CURSOR; idx\t\t          PLS_INTEGER := 1; param_count_out        PLS_INTEGER := 0; param_name_out         STR_TABLE; direction_out          NUM_TABLE; oradbtype_out          NUM_TABLE; size_out               NUM_TABLE; type_name_out          STR_TABLE; position_out           NUM_TABLE; data_level_out         NUM_TABLE; param_name_null_out    STR_TABLE; direction_null_out     NUM_TABLE; oradbtype_null_out     NUM_TABLE; size_null_out          NUM_TABLE; type_name_null_out     STR_TABLE; position_null_out      NUM_TABLE; data_level_null_out    NUM_TABLE; BEGIN name_in := :1; param_count_in := :2; link := NULL; DBMS_UTILITY.NAME_RESOLVE( name_in, context, schema, part1, part2, dblink, part1_type, object_number); WHILE (dblink IS NOT NULL) LOOP link := '@' || dblink; name_in := NULL; IF (schema IS NOT NULL) THEN name_in := '\"' || schema || '\"' || '.'; END IF; IF (part1 IS NOT NULL) THEN name_in := name_in || '\"' || part1 || '\"' || '.'; END IF; IF (part2 IS NULL) THEN name_in := RTRIM(name_in, '.'); ELSE name_in := name_in || '\"' || part2 || '\"'; END IF; EXECUTE IMMEDIATE 'BEGIN DBMS_UTILITY.NAME_RESOLVE' || link || '(:1, :2, :3, :4, :5, :6, :7, :8); END;' USING name_in, context, OUT schema, OUT part1, OUT part2, OUT dblink, OUT part1_type, OUT object_number; END LOOP; IF (param_count_out = 0) THEN OPEN refcur FOR 'SELECT  DECODE (POSITION, 0, ''RETURN_VALUE'', ARGUMENT_NAME) param_name, DECODE(IN_OUT, ''IN'', \t  1,  ''IN/OUT'', 3, ''OUT'', DECODE(ARGUMENT_NAME, NULL, 6, 2), 1) direction, DECODE(DATA_TYPE, ''BINARY_DOUBLE'',  108, ''BINARY_FLOAT'',   122, ''BINARY_INTEGER'', 112, ''BFILE'',     101, ''BLOB'',\t    102, ''CHAR'',\t    104, ''CLOB'',\t    105, ''DATE'',\t    106, ''FLOAT'',\t    107, ''INTERVAL YEAR TO MONTH'', 115, ''INTERVAL DAY TO SECOND'', 114, ''LONG'',\t    109, ''LONG RAW'',\t110, ''NCHAR'',\t    117, ''NCLOB'',\t    116, ''NUMBER'',\t  107, ''NVARCHAR2'',\t119, ''OBJECT'', \t  129, ''RAW'',\t      120, ''REF'',       130, ''REF CURSOR'',121, ''ROWID'',\t    126, ''TABLE'', \t  128, ''TIMESTAMP'',\t123, ''TIMESTAMP WITH LOCAL TIME ZONE'', 124, ''TIMESTAMP WITH TIME ZONE'', 125, ''UNDEFINED'', 100, ''UROWID'',\t  126, ''VARCHAR'', \t126, ''VARCHAR2'',\t126, ''VARRAY'', \t  128, ''PL/SQL TABLE'',1, NULL,            0, -1) oradbtype, DECODE(DATA_TYPE, ''CHAR'', \t      2000, ''LONG'',\t      4000, ''LONG RAW'',    4000, ''NCHAR'', \t    2000, ''NVARCHAR2'',   4000, ''RAW'', \t      2000, ''ROWID'', \t    4000, ''UROWID'',\t    4000, ''VARCHAR'', \t  4000, ''VARCHAR2'', \t  4000, ''PL/SQL TABLE'',  16, 0) length, (TYPE_OWNER ||  DECODE(TYPE_OWNER, NULL, NULL, ''.'') ||  TYPE_NAME ) type_name, POSITION   position, DATA_LEVEL data_level FROM ALL_ARGUMENTS' || link || ' WHERE OWNER \t\t  = :1 \t  AND (PACKAGE_NAME \t= :2 \t  OR (:3 IS NULL AND PACKAGE_NAME is null)) AND OBJECT_NAME \t  = :4 \t  AND NVL(overload, 1) = 1 ORDER BY SEQUENCE' USING schema, part1, part1, part2; LOOP   FETCH refcur INTO param_name_out(idx), direction_out(idx),     oradbtype_out(idx), size_out(idx), type_name_out(idx),     position_out(idx), data_level_out(idx);   EXIT WHEN refcur%NOTFOUND;   idx := idx + 1; END LOOP; param_count_out := refcur%ROWCOUNT; CLOSE refcur; END IF; IF (part1_type = 9 AND param_count_out = 0) THEN param_count_out := -1002; END IF; :3 := param_count_out; IF (param_count_out > param_count_in OR param_count_out < 0) THEN param_name_out:= param_name_null_out; direction_out := direction_null_out; oradbtype_out := oradbtype_null_out; size_out      := size_null_out; type_name_out := type_name_null_out; position_out  := position_null_out; data_level_out:= data_level_null_out; END IF; :4 := param_name_out; :5 := direction_out; :6 := oradbtype_out; :7 := size_out; :8 := type_name_out; :9 := position_out; :10:= data_level_out; END;";
						}
						else if (connection.m_majorVersion == 9 && connection.m_minorVersion == 0)
						{
							OracleCommandBuilder.m_dpCommand.CommandText = "DECLARE TYPE NUM_TABLE IS TABLE OF NUMBER INDEX BY BINARY_INTEGER; TYPE STR_TABLE IS TABLE OF VARCHAR2(128) INDEX BY BINARY_INTEGER; TYPE REF_CURSOR IS REF CURSOR; name_in          VARCHAR2(2000); param_count_in   BINARY_INTEGER; link                   VARCHAR2(2000); context          NUMBER := 1; schema           VARCHAR2(2000); part1            VARCHAR2(2000); part2            VARCHAR2(2000); dblink           VARCHAR2(2000); part1_type       NUMBER; object_number    NUMBER; refcur              SYS_REFCURSOR; idx                  BINARY_INTEGER := 1; param_count_out        BINARY_INTEGER := 0; param_name_out         STR_TABLE; direction_out          NUM_TABLE; oradbtype_out          NUM_TABLE; size_out               NUM_TABLE; type_name_out          STR_TABLE; position_out           NUM_TABLE; data_level_out         NUM_TABLE; param_name_null_out    STR_TABLE; direction_null_out     NUM_TABLE; oradbtype_null_out     NUM_TABLE; size_null_out          NUM_TABLE; type_name_null_out     STR_TABLE; position_null_out      NUM_TABLE; data_level_null_out    NUM_TABLE; BEGIN name_in := :1; param_count_in := :2; link := NULL; DBMS_UTILITY.NAME_RESOLVE( name_in, context, schema, part1, part2, dblink, part1_type, object_number); WHILE (dblink IS NOT NULL) LOOP link := '@' || dblink; name_in := NULL; IF (schema IS NOT NULL) THEN name_in := '\"' || schema || '\"' || '.'; END IF; IF (part1 IS NOT NULL) THEN name_in := name_in || '\"' || part1 || '\"' || '.'; END IF; IF (part2 IS NULL) THEN name_in := RTRIM(name_in, '.'); ELSE name_in := name_in || '\"' || part2 || '\"'; END IF; EXECUTE IMMEDIATE 'BEGIN DBMS_UTILITY.NAME_RESOLVE' || link || '(:1, :2, :3, :4, :5, :6, :7, :8); END;' USING name_in, context, OUT schema, OUT part1, OUT part2, OUT dblink, OUT part1_type, OUT object_number; END LOOP; IF (param_count_out = 0) THEN OPEN refcur FOR 'SELECT  DECODE (POSITION, 0, ''RETURN_VALUE'', ARGUMENT_NAME) param_name, DECODE(IN_OUT, ''IN'',       1,  ''IN/OUT'', 3, ''OUT'', DECODE(ARGUMENT_NAME, NULL, 6, 2), 1) direction, DECODE(DATA_TYPE, ''BINARY_DOUBLE'',  108, ''BINARY_FLOAT'',   122, ''BINARY_INTEGER'', 112, ''BFILE'',     101, ''BLOB'',        102, ''CHAR'',        104, ''CLOB'',        105, ''DATE'',        106, ''FLOAT'',        107, ''INTERVAL YEAR TO MONTH'', 115, ''INTERVAL DAY TO SECOND'', 114, ''LONG'',        109, ''LONG RAW'',    110, ''NCHAR'',        117, ''NCLOB'',        116, ''NUMBER'',      107, ''NVARCHAR2'',    119, ''OBJECT'',      129, ''RAW'',\t        120, ''REF'',         130, ''REF CURSOR'',121, ''ROWID'',        126, ''TABLE'',       128, ''TIMESTAMP'',    123, ''TIMESTAMP WITH LOCAL TIME ZONE'', 124, ''TIMESTAMP WITH TIME ZONE'', 125, ''UNDEFINED'', 100, ''UROWID'',      126, ''VARCHAR'',     126, ''VARCHAR2'',    126, ''VARRAY'',      128, ''PL/SQL TABLE'',1, NULL,            0, -1) oradbtype, DECODE(DATA_TYPE, ''CHAR'',           2000, ''LONG'',          4000, ''LONG RAW'',    4000, ''NCHAR'',         2000, ''NVARCHAR2'',   4000, ''RAW'',           2000, ''ROWID'',         4000, ''UROWID'',        4000, ''VARCHAR'',       4000, ''VARCHAR2'',       4000, ''PL/SQL TABLE'',  16, 0) length, (TYPE_OWNER ||  DECODE(TYPE_OWNER, NULL, NULL, ''.'') ||  TYPE_NAME ) type_name, POSITION   position, DATA_LEVEL data_level FROM ALL_ARGUMENTS' || link || ' WHERE OWNER = :1 AND  (PACKAGE_NAME = :2 OR  (:3 IS NULL AND PACKAGE_NAME = OBJECT_NAME)) AND  OBJECT_NAME = :4 AND NVL(overload, 1) = 1 ORDER BY SEQUENCE' USING schema, part1, part1, part2; FETCH refcur BULK COLLECT INTO param_name_out, direction_out, oradbtype_out, size_out, type_name_out, position_out, data_level_out; param_count_out := refcur%ROWCOUNT; CLOSE refcur; END IF; IF (part1_type = 9 AND param_count_out = 0) THEN param_count_out := -1002; END IF; :3 := param_count_out; IF (param_count_out > param_count_in OR param_count_out < 0) THEN param_name_out:= param_name_null_out; direction_out := direction_null_out; oradbtype_out := oradbtype_null_out; size_out      := size_null_out; type_name_out := type_name_null_out; position_out  := position_null_out; data_level_out:= data_level_null_out; END IF; :4 := param_name_out; :5 := direction_out; :6 := oradbtype_out; :7 := size_out; :8 := type_name_out; :9 := position_out; :10:= data_level_out; END;";
						}
						else
						{
							OracleCommandBuilder.m_dpCommand.CommandText = "DECLARE TYPE NUM_TABLE IS TABLE OF NUMBER INDEX BY PLS_INTEGER; TYPE STR_TABLE IS TABLE OF VARCHAR2(128) INDEX BY PLS_INTEGER; name_in          VARCHAR2(2000); param_count_in   PLS_INTEGER; link\t     \t      VARCHAR2(2000); context          NUMBER := 1; schema           VARCHAR2(2000); part1            VARCHAR2(2000); part2            VARCHAR2(2000); dblink           VARCHAR2(2000); part1_type       NUMBER; object_number    NUMBER; refcur\t          SYS_REFCURSOR; idx\t\t          PLS_INTEGER := 0; param_count_out        PLS_INTEGER := 0; param_name_out         STR_TABLE; direction_out          NUM_TABLE; oradbtype_out          NUM_TABLE; size_out               NUM_TABLE; type_name_out          STR_TABLE; position_out           NUM_TABLE; data_level_out         NUM_TABLE; param_name_null_out    STR_TABLE; direction_null_out     NUM_TABLE; oradbtype_null_out     NUM_TABLE; size_null_out          NUM_TABLE; type_name_null_out     STR_TABLE; position_null_out      NUM_TABLE; data_level_null_out    NUM_TABLE; BEGIN name_in := :1; param_count_in := :2; link := NULL; DBMS_UTILITY.NAME_RESOLVE( name_in, context, schema, part1, part2, dblink, part1_type, object_number); WHILE (dblink IS NOT NULL) LOOP link := '@' || dblink; name_in := NULL; IF (schema IS NOT NULL) THEN name_in := '\"' || schema || '\"' || '.'; END IF; IF (part1 IS NOT NULL) THEN name_in := name_in || '\"' || part1 || '\"' || '.'; END IF; IF (part2 IS NULL) THEN name_in := RTRIM(name_in, '.'); ELSE name_in := name_in || '\"' || part2 || '\"'; END IF; EXECUTE IMMEDIATE 'BEGIN DBMS_UTILITY.NAME_RESOLVE' || link || '(:1, :2, :3, :4, :5, :6, :7, :8); END;' USING name_in, context, OUT schema, OUT part1, OUT part2, OUT dblink, OUT part1_type, OUT object_number; END LOOP; IF (param_count_out = 0) THEN OPEN refcur FOR 'SELECT  DECODE (POSITION, 0, ''RETURN_VALUE'', ARGUMENT_NAME) param_name, DECODE(IN_OUT, ''IN'', \t  1,  ''IN/OUT'', 3, ''OUT'', DECODE(ARGUMENT_NAME, NULL, 6, 2), 1) direction, DECODE(DATA_TYPE, ''BINARY_DOUBLE'',  108, ''BINARY_FLOAT'',   122, ''BINARY_INTEGER'', 112, ''BFILE'',     101, ''BLOB'',\t    102, ''CHAR'',\t    104, ''CLOB'',\t    105, ''DATE'',\t    106, ''FLOAT'',\t    107, ''INTERVAL YEAR TO MONTH'', 115, ''INTERVAL DAY TO SECOND'', 114, ''LONG'',\t    109, ''LONG RAW'',\t110, ''NCHAR'',\t    117, ''NCLOB'',\t    116, ''NUMBER'',\t  107, ''NVARCHAR2'',\t119, ''OBJECT'', \t  129, ''RAW'',\t      120, ''REF'',       130, ''REF CURSOR'',121, ''ROWID'',\t    126, ''TABLE'', \t  128, ''TIMESTAMP'',\t123, ''TIMESTAMP WITH LOCAL TIME ZONE'', 124, ''TIMESTAMP WITH TIME ZONE'', 125, ''UNDEFINED'', 100, ''UROWID'',\t  126, ''VARCHAR'', \t126, ''VARCHAR2'',\t126, ''VARRAY'', \t  128, ''PL/SQL TABLE'',1, NULL,            0, -1) oradbtype, DECODE(DATA_TYPE, ''CHAR'', \t      2000, ''LONG'',\t      4000, ''LONG RAW'',    4000, ''NCHAR'', \t    2000, ''NVARCHAR2'',   4000, ''RAW'', \t      2000, ''ROWID'', \t    4000, ''UROWID'',\t    4000, ''VARCHAR'', \t  4000, ''VARCHAR2'', \t  4000, ''PL/SQL TABLE'',  16, 0) length, (TYPE_OWNER ||  DECODE(TYPE_OWNER, NULL, NULL, ''.'') ||  TYPE_NAME ) type_name, POSITION   position, DATA_LEVEL data_level FROM ALL_ARGUMENTS' || link || ' WHERE OWNER \t\t  = :1 \t  AND (PACKAGE_NAME \t= :2 \t  OR (:3 IS NULL AND PACKAGE_NAME is null)) AND OBJECT_NAME \t  = :4 \t  AND NVL(overload, 1) = 1 ORDER BY SEQUENCE' USING schema, part1, part1, part2; FETCH refcur BULK COLLECT INTO param_name_out, direction_out, oradbtype_out, size_out, type_name_out, position_out, data_level_out; param_count_out := refcur%ROWCOUNT; CLOSE refcur; END IF; IF (part1_type = 9 AND param_count_out = 0) THEN param_count_out := -1002; END IF; :3 := param_count_out; IF (param_count_out > param_count_in OR param_count_out < 0) THEN param_name_out:= param_name_null_out; direction_out := direction_null_out; oradbtype_out := oradbtype_null_out; size_out      := size_null_out; type_name_out := type_name_null_out; position_out  := position_null_out; data_level_out:= data_level_null_out; END IF; :4 := param_name_out; :5 := direction_out; :6 := oradbtype_out; :7 := size_out; :8 := type_name_out; :9 := position_out; :10:= data_level_out; END;";
						}
						OracleCommandBuilder.m_dpCommand.Connection = connection;
						OracleCommandBuilder.m_dpCommandParams[0].Value = commandText;
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
						if (OracleCommandBuilder.m_dpCommand.Connection.ConnectionType == OracleConnectionType.TimesTen)
						{
							OracleCommandBuilder.TTExecDeriveParameters(OracleCommandBuilder.m_dpCommand);
						}
						else
						{
							OracleCommandBuilder.m_dpCommand.ExecuteNonQuery();
						}
						int num = (int)OracleCommandBuilder.m_dpCommandParams[2].Value;
						if (num == -1002)
						{
							throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.ODP_INVALID_VALUE, new string[]
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
							for (int l = 0; l < num; l++)
							{
								OracleCommandBuilder.m_dpCommandParams[3].ArrayBindSize[l] = 128;
							}
							OracleCommandBuilder.m_dpCommandParams[4].Size = num;
							OracleCommandBuilder.m_dpCommandParams[5].Size = num;
							OracleCommandBuilder.m_dpCommandParams[6].Size = num;
							OracleCommandBuilder.m_dpCommandParams[7].Size = num;
							if (OracleCommandBuilder.m_dpCommandParams[7].ArrayBindSize.Length < num)
							{
								OracleCommandBuilder.m_dpCommandParams[7].ArrayBindSize = new int[num];
							}
							for (int m = 0; m < num; m++)
							{
								OracleCommandBuilder.m_dpCommandParams[7].ArrayBindSize[m] = 128;
							}
							OracleCommandBuilder.m_dpCommandParams[8].Size = num;
							OracleCommandBuilder.m_dpCommandParams[9].Size = num;
							if (OracleCommandBuilder.m_dpCommand.Connection.ConnectionType == OracleConnectionType.TimesTen)
							{
								OracleCommandBuilder.TTExecDeriveParameters(OracleCommandBuilder.m_dpCommand);
							}
							else
							{
								OracleCommandBuilder.m_dpCommand.ExecuteNonQuery();
							}
							num = (int)OracleCommandBuilder.m_dpCommandParams[2].Value;
						}
						string[] array = (string[])OracleCommandBuilder.m_dpCommandParams[3].Value;
						int[] array2 = (int[])OracleCommandBuilder.m_dpCommandParams[4].Value;
						int[] array3 = (int[])OracleCommandBuilder.m_dpCommandParams[5].Value;
						int[] array4 = (int[])OracleCommandBuilder.m_dpCommandParams[6].Value;
						string[] array5 = (string[])OracleCommandBuilder.m_dpCommandParams[7].Value;
						int[] array6 = (int[])OracleCommandBuilder.m_dpCommandParams[8].Value;
						int[] array7 = (int[])OracleCommandBuilder.m_dpCommandParams[9].Value;
						Monitor.Exit(OracleCommandBuilder.m_staticLock);
						flag = false;
						if ((deriveParamInfo = (DeriveParamInfo)DeriveParamInfo.m_pooler.Get(connection.m_internalConStr, commandText)) == null)
						{
							deriveParamInfo = new DeriveParamInfo(num);
							int num2 = 0;
							for (int n = 0; n < deriveParamInfo.m_allocCount; n++)
							{
								if (array7[n] == 0)
								{
									if (array3[n] == 0)
									{
										break;
									}
									int num3 = array6[n];
									if (array3[n] == 100)
									{
										if (array5[n] == "SYS.XMLTYPE" || array5[n] == "PUBLIC.XMLTYPE")
										{
											array3[n] = 127;
										}
										else
										{
											array3[n] = -1;
										}
									}
									if (array3[n] == -1)
									{
										throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.BLR_PRM_NOT_SUPPORTED, new string[]
										{
											num3.ToString()
										}));
									}
									deriveParamInfo.m_paramName[num2] = array[n];
									deriveParamInfo.m_direction[num2] = (ParameterDirection)array2[n];
									deriveParamInfo.m_size[num2] = array4[n];
									if (array3[n] != 1)
									{
										deriveParamInfo.m_oraCollType[num2] = OracleCollectionType.None;
										deriveParamInfo.m_oraDbType[num2] = (OracleDbType)array3[n];
										deriveParamInfo.m_typeName[num2] = array5[n];
									}
									else
									{
										deriveParamInfo.m_oraCollType[num2] = OracleCollectionType.PLSQLAssociativeArray;
										n++;
										if (array3[n] == 100)
										{
											if (array5[n] == "SYS.XMLTYPE" || array5[n] == "PUBLIC.XMLTYPE")
											{
												array3[n] = 127;
											}
											else
											{
												array3[n] = -1;
											}
										}
										if (array3[n] == -1)
										{
											throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.BLR_PRM_NOT_SUPPORTED, new string[]
											{
												num3.ToString()
											}));
										}
										deriveParamInfo.m_oraDbType[num2] = (OracleDbType)array3[n];
										deriveParamInfo.m_typeName[num2] = array5[n];
										deriveParamInfo.m_arrayBindSize[num2] = array4[n];
									}
									num2++;
								}
							}
							deriveParamInfo.m_paramCount = num2;
							DeriveParamInfo.m_pooler.Put(connection.m_internalConStr, commandText, deriveParamInfo);
						}
					}
				}
				finally
				{
					if (flag)
					{
						Monitor.Exit(OracleCommandBuilder.m_staticLock);
					}
				}
			}
			lock (command)
			{
				command.Parameters.Clear();
				for (int num4 = 0; num4 < deriveParamInfo.m_paramCount; num4++)
				{
					OracleParameter oracleParameter = new OracleParameter();
					oracleParameter.ParameterName = deriveParamInfo.m_paramName[num4];
					oracleParameter.Direction = deriveParamInfo.m_direction[num4];
					oracleParameter.CollectionType = deriveParamInfo.m_oraCollType[num4];
					oracleParameter.OracleDbTypeEx = deriveParamInfo.m_oraDbType[num4];
					if (deriveParamInfo.m_size[num4] != 0 && oracleParameter.Direction != ParameterDirection.Input)
					{
						oracleParameter.Size = deriveParamInfo.m_size[num4];
					}
					if (oracleParameter.CollectionType == OracleCollectionType.PLSQLAssociativeArray && oracleParameter.Size > 0)
					{
						oracleParameter.ArrayBindStatus = new OracleParameterStatus[oracleParameter.Size];
						for (int num5 = 0; num5 < oracleParameter.Size; num5++)
						{
							oracleParameter.ArrayBindStatus[num5] = OracleParameterStatus.Success;
						}
						if (deriveParamInfo.m_arrayBindSize[num4] != 0)
						{
							oracleParameter.ArrayBindSize = new int[oracleParameter.Size];
							for (int num6 = 0; num6 < oracleParameter.Size; num6++)
							{
								oracleParameter.ArrayBindSize[num6] = deriveParamInfo.m_arrayBindSize[num4];
							}
						}
					}
					if (oracleParameter.OracleDbType == OracleDbType.Object || oracleParameter.OracleDbType == OracleDbType.Ref || oracleParameter.OracleDbType == OracleDbType.Array)
					{
						oracleParameter.UdtTypeName = deriveParamInfo.m_typeName[num4];
					}
					command.Parameters.Add(oracleParameter);
				}
			}
		}

		// Token: 0x06000D18 RID: 3352 RVA: 0x00087260 File Offset: 0x00086260
		private static void TTExecDeriveParameters(OracleCommand dpCommand)
		{
			OracleCommand oracleCommand = dpCommand.Connection.CreateCommand();
			oracleCommand.CommandText = "BEGIN DBMS_UTILITY.NAME_RESOLVE( :name, 1, :schema, :part1, :part2, :dblink, :part1_type, :object_number);END;";
			oracleCommand.Parameters.Add("name", OracleDbType.Varchar2, dpCommand.Parameters[0].Value, ParameterDirection.Input);
			oracleCommand.Parameters.Add("schema", OracleDbType.Varchar2, 128, null, ParameterDirection.Output);
			oracleCommand.Parameters.Add("part1", OracleDbType.Varchar2, 128, null, ParameterDirection.Output);
			oracleCommand.Parameters.Add("part2", OracleDbType.Varchar2, 128, null, ParameterDirection.Output);
			oracleCommand.Parameters.Add("dblink", OracleDbType.Varchar2, 128, null, ParameterDirection.Output);
			oracleCommand.Parameters.Add("part1_type", OracleDbType.Decimal, 0, ParameterDirection.Output);
			oracleCommand.Parameters.Add("object_number", OracleDbType.Decimal, 0, ParameterDirection.Output);
			oracleCommand.ExecuteNonQuery();
			OracleCommand oracleCommand2 = dpCommand.Connection.CreateCommand();
			oracleCommand2.CommandText = "SELECT   DECODE (POSITION, 0, 'RETURN_VALUE', ARGUMENT_NAME) param_name,   CAST (DECODE (IN_OUT, 'IN', 1, 'IN/OUT', 3, 'OUT',     DECODE(ARGUMENT_NAME, NULL, 6, 2), 1) AS TT_SMALLINT) direction,   CAST (DECODE(DATA_TYPE,     'BINARY_DOUBLE', 108,     'BINARY_FLOAT', 122,     'BINARY_INTEGER', 112,     'BFILE', 101,     'BLOB',\t102,     'CHAR', 104,     'CLOB',\t105,     'DATE',\t106,     'FLOAT', 107,     'INTERVAL YEAR TO MONTH', 115,     'INTERVAL DAY TO SECOND', 114,     'LONG',\t109,     'LONG RAW',\t110,     'NCHAR', 117,     'NCLOB', 116,     'NUMBER', 107,     'NVARCHAR2', 119,     'RAW', 120,     'REF', 130,     'REF CURSOR', 121,     'ROWID', 126,     'TABLE', 128,     'TIMESTAMP',\t123,     'TIMESTAMP WITH LOCAL TIME ZONE', 124,     'TIMESTAMP WITH TIME ZONE', 125,     'UNDEFINED', 100,     'UROWID', 126,     'VARCHAR', 126,     'VARCHAR2',\t126,     'VARRAY', 128,     'PL/SQL TABLE', 1,      NULL, 0,     -1) AS TT_SMALLINT) oradbtype,   CAST (DECODE(DATA_TYPE,     'CHAR', 2000,     'LONG', 4000,     'LONG RAW', 4000,     'NCHAR', 2000,     'NVARCHAR2', 4000,     'RAW', 2000,     'ROWID', 4000,     'UROWID', 4000,     'VARCHAR', 4000,     'VARCHAR2', 4000,     'PL/SQL TABLE', 16,     0) AS TT_SMALLINT) length,   (TYPE_OWNER || DECODE (TYPE_OWNER, NULL, NULL, '.') || TYPE_NAME) type_name,   CAST (POSITION AS TT_SMALLINT) position,   CAST (DATA_LEVEL AS TT_SMALLINT) data_level   FROM ALL_ARGUMENTS   WHERE OWNER = TRIM (:1)     AND (PACKAGE_NAME = TRIM (:2)       OR (CAST (TRIM (:3) AS VARCHAR2 (30)) IS NULL AND PACKAGE_NAME IS NULL))     AND OBJECT_NAME = TRIM (:4)     AND NVL (OVERLOAD, '1') = '1'   ORDER BY SEQUENCE  ";
			oracleCommand2.Parameters.Add("1", OracleDbType.Varchar2, oracleCommand.Parameters[1].Value, ParameterDirection.Input);
			oracleCommand2.Parameters.Add("2", OracleDbType.Varchar2, oracleCommand.Parameters[2].Value, ParameterDirection.Input);
			oracleCommand2.Parameters.Add("3", OracleDbType.Varchar2, oracleCommand.Parameters[2].Value, ParameterDirection.Input);
			oracleCommand2.Parameters.Add("4", OracleDbType.Varchar2, oracleCommand.Parameters[3].Value, ParameterDirection.Input);
			OracleDataReader oracleDataReader = oracleCommand2.ExecuteReader();
			int num = 0;
			ArrayList arrayList = new ArrayList();
			ArrayList arrayList2 = new ArrayList();
			ArrayList arrayList3 = new ArrayList();
			ArrayList arrayList4 = new ArrayList();
			ArrayList arrayList5 = new ArrayList();
			ArrayList arrayList6 = new ArrayList();
			ArrayList arrayList7 = new ArrayList();
			while (oracleDataReader.Read())
			{
				num++;
				if (oracleDataReader.IsDBNull(0))
				{
					arrayList.Add(null);
				}
				else
				{
					arrayList.Add(oracleDataReader.GetString(0));
				}
				arrayList2.Add(oracleDataReader.GetInt32(1));
				arrayList3.Add(oracleDataReader.GetInt32(2));
				arrayList4.Add(oracleDataReader.GetInt32(3));
				if (oracleDataReader.IsDBNull(4))
				{
					arrayList5.Add(null);
				}
				else
				{
					arrayList5.Add(oracleDataReader.GetString(4));
				}
				arrayList6.Add(oracleDataReader.GetInt32(5));
				arrayList7.Add(oracleDataReader.GetInt32(6));
			}
			oracleDataReader.Close();
			dpCommand.Parameters[2].Value = num;
			dpCommand.Parameters[3].Value = arrayList.ToArray(typeof(string));
			dpCommand.Parameters[4].Value = arrayList2.ToArray(typeof(int));
			dpCommand.Parameters[5].Value = arrayList3.ToArray(typeof(int));
			dpCommand.Parameters[6].Value = arrayList4.ToArray(typeof(int));
			dpCommand.Parameters[7].Value = arrayList5.ToArray(typeof(string));
			dpCommand.Parameters[8].Value = arrayList6.ToArray(typeof(int));
			dpCommand.Parameters[9].Value = arrayList7.ToArray(typeof(int));
			oracleDataReader.Dispose();
			oracleCommand2.Dispose();
			oracleCommand.Dispose();
		}

		// Token: 0x06000D19 RID: 3353 RVA: 0x00087610 File Offset: 0x00086610
		public override string QuoteIdentifier(string unquotedIdentifier)
		{
			if (unquotedIdentifier == null)
			{
				throw new ArgumentNullException();
			}
			if (unquotedIdentifier.Length == 0)
			{
				return this.QuotePrefix + this.QuoteSuffix;
			}
			return string.Format("{0}{1}{2}", this.QuotePrefix, unquotedIdentifier.Replace("\"", "\"\""), this.QuoteSuffix);
		}

		// Token: 0x06000D1A RID: 3354 RVA: 0x00087668 File Offset: 0x00086668
		public override string UnquoteIdentifier(string quotedIdentifier)
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
			return quotedIdentifier.Substring(1, length - 2).Replace("\"\"", "\"");
		}

		// Token: 0x06000D1B RID: 3355 RVA: 0x000876C0 File Offset: 0x000866C0
		private string GenerateParameterName(string prefix, int srcColumnLen, string srcColumn, int paramId)
		{
			string text = Regex.Replace(srcColumn, "[^\\w]", "");
			if (srcColumnLen < text.Length)
			{
				text = text.Substring(0, srcColumnLen);
			}
			StringBuilder stringBuilder = new StringBuilder(32);
			stringBuilder.Append(prefix);
			stringBuilder.Append(text);
			stringBuilder.Append("_p");
			stringBuilder.Append(paramId);
			return stringBuilder.ToString();
		}

		// Token: 0x06000D1C RID: 3356 RVA: 0x00087724 File Offset: 0x00086724
		public new unsafe OracleCommand GetDeleteCommand()
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleCommandBuilder::GetDeleteCommand()\n"
				});
			}
			if (this.m_disposed)
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
			OracleCommand oracleCommand = new OracleCommand();
			int num = 0;
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = false;
			if (this.m_pOpoMetValCtx == null || this.m_pOpoMetValCtx->bPkFetched == 0)
			{
				if (this.m_dataAdapter == null || this.m_dataAdapter.SelectCommand == null || this.m_dataAdapter.SelectCommand.Connection == null)
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
				if (this.m_pOpoMetValCtx == null)
				{
					this.GetOpoMetValCtx();
				}
				this.CheckPrimaryKey();
			}
			finally
			{
				if (flag && this.m_dataAdapter.SelectCommand.Connection.State == ConnectionState.Open)
				{
					this.m_dataAdapter.SelectCommand.Connection.Close();
				}
			}
			int numOfColumns = this.GetNumOfColumns();
			int num2 = 2 * numOfColumns;
			int num3 = (OracleCommandBuilder.m_sMaxParamNameLen - (6 + num2.ToString().Length)) / OracleCommandBuilder.m_sMaxExpansionRatio;
			if (num3 < 0)
			{
				num3 = 0;
			}
			for (int i = 0; i < numOfColumns; i++)
			{
				if (this.m_pOpoMetValCtx->pColMetaVal[i].bIsHiddenCol != 1)
				{
					string columnName = this.GetColumnName(i, true);
					string columnName2 = this.GetColumnName(i, false);
					OracleDbType columnType = this.GetColumnType(i);
					if (!this.IsOraLOB(columnType) && !this.IsOraLONG(columnType) && !this.IsOraXmlType(columnType) && !this.IsOraUDT(columnType) && columnName != null)
					{
						bool flag2;
						string text;
						OracleParameter oracleParameter;
						if (flag2 = this.IsNullableCol(i))
						{
							if (this.m_ODTDesignMode)
							{
								text = this.GenerateParameterName(":ori_", num3, columnName2, num);
								stringBuilder.Append(" ((");
								stringBuilder.Append(text);
								stringBuilder.Append(" IS NULL AND ");
								stringBuilder.Append(columnName);
								stringBuilder.Append(" IS NULL) OR");
								oracleParameter = oracleCommand.Parameters.Add(this.CreateParams(text, columnName2, columnType, DataRowVersion.Original, 1));
								if (columnType == OracleDbType.Ref)
								{
									oracleParameter.UdtTypeName = this.GetUdtTypeName(i);
								}
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
						if (flag2)
						{
							stringBuilder.Append(")");
						}
						stringBuilder.Append(" AND");
						DataRowVersion version = DataRowVersion.Original;
						oracleParameter = oracleCommand.Parameters.Add(this.CreateParams(text, columnName2, columnType, version, null));
						if (columnType == OracleDbType.Ref)
						{
							oracleParameter.UdtTypeName = this.GetUdtTypeName(i);
						}
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
			oracleCommand.Connection = this.DataAdapter.SelectCommand.Connection;
			oracleCommand.UpdatedRowSource = UpdateRowSource.None;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleCommandBuilder::GetDeleteCommand()\n"
				});
			}
			return oracleCommand;
		}

		// Token: 0x06000D1D RID: 3357 RVA: 0x00087B7C File Offset: 0x00086B7C
		public new OracleCommand GetDeleteCommand(bool opt)
		{
			return this.GetDeleteCommand();
		}

		// Token: 0x06000D1E RID: 3358 RVA: 0x00087B84 File Offset: 0x00086B84
		internal OracleCommand GetDeleteCommand(DataRow row)
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
					this.m_deleteCmd.m_initialLobFS = 0;
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
				if (!this.IsOraLOB(columnType) && !this.IsOraLONG(columnType) && !this.IsOraXmlType(columnType) && !this.IsOraUDT(columnType) && columnName != null)
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
						if (columnType == OracleDbType.Ref)
						{
							oracleParameter.UdtTypeName = this.GetUdtTypeName(dataColumn);
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
			return this.m_deleteCmd;
		}

		// Token: 0x06000D1F RID: 3359 RVA: 0x00087F54 File Offset: 0x00086F54
		internal OracleCommand GetInsertCommand(DataRow row)
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
					this.m_insertCmd.m_initialLobFS = 0;
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
						if (this.IsOraUDT(columnType) || columnType == OracleDbType.Ref)
						{
							oracleParameter.UdtTypeName = this.GetUdtTypeName(dataColumn);
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
				return null;
			}
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
			return this.m_insertCmd;
		}

		// Token: 0x06000D20 RID: 3360 RVA: 0x00088338 File Offset: 0x00087338
		public new unsafe OracleCommand GetInsertCommand()
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleCommandBuilder::GetInsertCommand()\n"
				});
			}
			if (this.m_disposed)
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
			int num = 0;
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder stringBuilder2 = new StringBuilder();
			OracleCommand oracleCommand = new OracleCommand();
			bool flag = false;
			if (this.m_pOpoMetValCtx == null)
			{
				if (this.m_dataAdapter == null || this.m_dataAdapter.SelectCommand == null || this.m_dataAdapter.SelectCommand.Connection == null)
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
				if (this.m_pOpoMetValCtx == null)
				{
					this.GetOpoMetValCtx();
				}
			}
			finally
			{
				if (flag && this.m_dataAdapter.SelectCommand.Connection.State == ConnectionState.Open)
				{
					this.m_dataAdapter.SelectCommand.Connection.Close();
				}
			}
			if (this.m_pOpoMetValCtx == null)
			{
				return null;
			}
			int numOfColumns = this.GetNumOfColumns();
			int num2 = numOfColumns;
			int num3 = (OracleCommandBuilder.m_sMaxParamNameLen - (6 + num2.ToString().Length)) / OracleCommandBuilder.m_sMaxExpansionRatio;
			if (num3 < 0)
			{
				num3 = 0;
			}
			for (int i = 0; i < numOfColumns; i++)
			{
				if (this.m_pOpoMetValCtx->pColMetaVal[i].Updatable == 1 && this.m_pOpoMetValCtx->pColMetaVal[i].bIsHiddenCol != 1)
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
						OracleParameter oracleParameter = oracleCommand.Parameters.Add(this.CreateParams(text, columnName2, columnType, DataRowVersion.Current, null));
						if (this.IsOraUDT(columnType) || columnType == OracleDbType.Ref)
						{
							oracleParameter.UdtTypeName = this.GetUdtTypeName(i);
						}
						num++;
					}
				}
			}
			if (stringBuilder.Length == 0)
			{
				return null;
			}
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
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleCommandBuilder::GetInsertCommand()\n"
				});
			}
			return oracleCommand;
		}

		// Token: 0x06000D21 RID: 3361 RVA: 0x000886A0 File Offset: 0x000876A0
		public new OracleCommand GetInsertCommand(bool opt)
		{
			return this.GetInsertCommand();
		}

		// Token: 0x06000D22 RID: 3362 RVA: 0x000886A8 File Offset: 0x000876A8
		internal OracleCommand GetUpdateCommand(DataRow row)
		{
			int num = 1;
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder stringBuilder2 = new StringBuilder();
			if (row.RowState != DataRowState.Modified)
			{
				return null;
			}
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
					this.m_updateCmd.m_initialLobFS = 0;
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
					if (this.IsOraUDT(columnType) || columnType == OracleDbType.Ref)
					{
						oracleParameter.UdtTypeName = this.GetUdtTypeName(dataColumn);
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
						if (this.IsOraUDT(columnType2) || columnType2 == OracleDbType.Ref)
						{
							oracleParameter2.UdtTypeName = this.GetUdtTypeName(dataColumn2);
						}
						this.m_updateCmd.Parameters.Add(oracleParameter2);
						num++;
					}
				}
			}
			if (stringBuilder.ToString().Length == 0)
			{
				return null;
			}
			foreach (object obj3 in table.Columns)
			{
				DataColumn dataColumn3 = (DataColumn)obj3;
				OracleDbType columnType3 = this.GetColumnType(dataColumn3);
				string columnName5 = this.GetColumnName(dataColumn3, true);
				string columnName6 = this.GetColumnName(dataColumn3, false);
				if (!this.IsOraLOB(columnType3) && !this.IsOraLONG(columnType3) && !this.IsOraXmlType(columnType3) && !this.IsOraUDT(columnType3) && columnName5 != null)
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
						if (columnType3 == OracleDbType.Ref)
						{
							oracleParameter3.UdtTypeName = this.GetUdtTypeName(dataColumn3);
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
			return this.m_updateCmd;
		}

		// Token: 0x06000D23 RID: 3363 RVA: 0x00088E04 File Offset: 0x00087E04
		public new unsafe OracleCommand GetUpdateCommand()
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleCommandBuilder::GetUpdateCommand()\n"
				});
			}
			if (this.m_disposed)
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
			int num = 0;
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder stringBuilder2 = new StringBuilder();
			OracleCommand oracleCommand = new OracleCommand();
			bool flag = false;
			if (this.m_pOpoMetValCtx == null || this.m_pOpoMetValCtx->bPkFetched == 0)
			{
				if (this.m_dataAdapter == null || this.m_dataAdapter.SelectCommand == null || this.m_dataAdapter.SelectCommand.Connection == null)
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
				if (this.m_pOpoMetValCtx == null)
				{
					this.GetOpoMetValCtx();
				}
				this.CheckPrimaryKey();
			}
			finally
			{
				if (flag && this.m_dataAdapter.SelectCommand.Connection.State == ConnectionState.Open)
				{
					this.m_dataAdapter.SelectCommand.Connection.Close();
				}
			}
			int numOfColumns = this.GetNumOfColumns();
			int num2 = 3 * numOfColumns;
			int num3 = (OracleCommandBuilder.m_sMaxParamNameLen - (6 + num2.ToString().Length)) / OracleCommandBuilder.m_sMaxExpansionRatio;
			if (num3 < 0)
			{
				num3 = 0;
			}
			for (int i = 0; i < numOfColumns; i++)
			{
				if (this.m_pOpoMetValCtx->pColMetaVal[i].Updatable == 1 && this.m_pOpoMetValCtx->pColMetaVal[i].bIsHiddenCol != 1)
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
						OracleParameter oracleParameter = oracleCommand.Parameters.Add(this.CreateParams(text, columnName2, columnType, DataRowVersion.Current, null));
						if (this.IsOraUDT(columnType) || columnType == OracleDbType.Ref)
						{
							oracleParameter.UdtTypeName = this.GetUdtTypeName(i);
						}
						num++;
					}
				}
			}
			if (stringBuilder.ToString().Length == 0)
			{
				return null;
			}
			numOfColumns = this.GetNumOfColumns();
			for (int j = 0; j < numOfColumns; j++)
			{
				if (this.m_pOpoMetValCtx->pColMetaVal[j].bIsHiddenCol != 1)
				{
					OracleDbType columnType2 = this.GetColumnType(j);
					string columnName3 = this.GetColumnName(j, true);
					string columnName4 = this.GetColumnName(j, false);
					if (!this.IsOraLOB(columnType2) && !this.IsOraLONG(columnType2) && !this.IsOraXmlType(columnType2) && !this.IsOraUDT(columnType2) && columnName3 != null)
					{
						string text;
						OracleParameter oracleParameter;
						bool flag2;
						if (flag2 = this.IsNullableCol(j))
						{
							if (this.m_ODTDesignMode)
							{
								text = this.GenerateParameterName(":ori_", num3, columnName4, num);
								stringBuilder2.Append(" ((");
								stringBuilder2.Append(text);
								stringBuilder2.Append(" IS NULL AND ");
								stringBuilder2.Append(columnName3);
								stringBuilder2.Append(" IS NULL) OR");
								oracleParameter = oracleCommand.Parameters.Add(this.CreateParams(text, columnName4, columnType2, DataRowVersion.Original, 1));
								if (columnType2 == OracleDbType.Ref)
								{
									oracleParameter.UdtTypeName = this.GetUdtTypeName(j);
								}
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
						if (flag2)
						{
							stringBuilder2.Append(")");
						}
						stringBuilder2.Append(" AND");
						oracleParameter = oracleCommand.Parameters.Add(this.CreateParams(text, columnName4, columnType2, DataRowVersion.Original, null));
						if (columnType2 == OracleDbType.Ref)
						{
							oracleParameter.UdtTypeName = this.GetUdtTypeName(j);
						}
						num++;
					}
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
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleCommandBuilder::GetUpdateCommand()\n"
				});
			}
			return oracleCommand;
		}

		// Token: 0x06000D24 RID: 3364 RVA: 0x000893B4 File Offset: 0x000883B4
		public new OracleCommand GetUpdateCommand(bool opt)
		{
			return this.GetUpdateCommand();
		}

		// Token: 0x06000D25 RID: 3365 RVA: 0x000893BC File Offset: 0x000883BC
		protected override void Dispose(bool disposing)
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
								this.m_dataAdapter.RowUpdating -= this.m_hndr;
							}
							catch
							{
							}
							this.m_dataAdapter = null;
						}
						this.m_pOpoMetValCtx = null;
						this.m_opoMetRefCtx = null;
						this.m_colMetaRef = null;
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
					this.m_metaData = null;
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

		// Token: 0x06000D26 RID: 3366 RVA: 0x000894F0 File Offset: 0x000884F0
		public override void RefreshSchema()
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleCommandBuilder::RefreshSchema()\n"
				});
			}
			if (this.m_dataAdapter != null)
			{
				this.m_dataAdapter.InsertCommand = null;
				this.m_dataAdapter.UpdateCommand = null;
				this.m_dataAdapter.DeleteCommand = null;
			}
			this.m_pOpoMetValCtx = null;
			this.m_metaData = null;
			this.m_opoMetRefCtx = null;
			this.m_colMetaRef = null;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleCommandBuilder::RefreshSchema()\n"
				});
			}
		}

		// Token: 0x06000D27 RID: 3367 RVA: 0x00089580 File Offset: 0x00088580
		private void RowUpdating(object src, OracleRowUpdatingEventArgs arg)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleCommandBuilder::RowUpdating()\n"
				});
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
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleCommandBuilder::RowUpdating()\n"
				});
			}
		}

		// Token: 0x06000D28 RID: 3368 RVA: 0x000897D4 File Offset: 0x000887D4
		protected override string GetParameterName(int parameterOrdinal)
		{
			return "p" + parameterOrdinal.ToString();
		}

		// Token: 0x06000D29 RID: 3369 RVA: 0x000897E7 File Offset: 0x000887E7
		protected override string GetParameterName(string parameterName)
		{
			return parameterName;
		}

		// Token: 0x06000D2A RID: 3370 RVA: 0x000897EC File Offset: 0x000887EC
		protected override void ApplyParameterInfo(DbParameter parameter, DataRow row, StatementType statementType, bool whereClause)
		{
			OracleParameter oracleParameter = parameter as OracleParameter;
			if (oracleParameter != null)
			{
				oracleParameter.OracleDbType = (OracleDbType)row["ProviderType"];
				oracleParameter.SourceColumn = (string)row["ColumnName"];
				if (ParameterDirection.Input == oracleParameter.Direction && parameter.Size == 0)
				{
					(parameter as OracleParameter).SetSize(-1);
				}
			}
		}

		// Token: 0x06000D2B RID: 3371 RVA: 0x0008984C File Offset: 0x0008884C
		protected override string GetParameterPlaceholder(int parameterOrdinal)
		{
			return ":p" + parameterOrdinal.ToString();
		}

		// Token: 0x06000D2C RID: 3372 RVA: 0x0008985F File Offset: 0x0008885F
		protected override DbCommand InitializeCommand(DbCommand command)
		{
			return base.InitializeCommand(command);
		}

		// Token: 0x06000D2D RID: 3373 RVA: 0x00089868 File Offset: 0x00088868
		protected override void SetRowUpdatingHandler(DbDataAdapter adapter)
		{
			if (adapter == null)
			{
				throw new ArgumentNullException();
			}
			if (this.m_dataAdapter == adapter)
			{
				((OracleDataAdapter)adapter).RowUpdating -= this.m_hndr;
				return;
			}
			((OracleDataAdapter)adapter).RowUpdating += this.m_hndr;
			this.m_dataAdapter = (adapter as OracleDataAdapter);
		}

		// Token: 0x06000D2E RID: 3374 RVA: 0x000898B6 File Offset: 0x000888B6
		private unsafe int GetNumOfColumns()
		{
			return (int)this.m_pOpoMetValCtx->NoOfCols;
		}

		// Token: 0x06000D2F RID: 3375 RVA: 0x000898C3 File Offset: 0x000888C3
		private string GetColumnName(DataColumn col, bool baseColumn)
		{
			return this.GetColumnName(col, baseColumn, this.m_caseSensitive);
		}

		// Token: 0x06000D30 RID: 3376 RVA: 0x000898D4 File Offset: 0x000888D4
		private string GetColumnName(DataColumn col, bool baseColumn, bool caseSensitive)
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
					text = this.m_colMetaRef[num].pColName;
				}
				else
				{
					text = this.m_colMetaRef[num].pColAlias;
				}
			}
			if (text != null && this.m_caseSensitive && baseColumn)
			{
				return text.Insert(text.Length, "\"").Insert(0, "\"");
			}
			return text;
		}

		// Token: 0x06000D31 RID: 3377 RVA: 0x00089958 File Offset: 0x00088958
		private string GetColumnName(int col, bool baseColumn)
		{
			return this.GetColumnName(col, baseColumn, this.m_caseSensitive);
		}

		// Token: 0x06000D32 RID: 3378 RVA: 0x00089968 File Offset: 0x00088968
		private string GetColumnName(int col, bool baseColumn, bool caseSensitive)
		{
			if (this.m_colMetaRef == null)
			{
				this.GetColMetaRef();
				if (this.m_colMetaRef == null)
				{
					return null;
				}
			}
			string text;
			if (baseColumn)
			{
				text = this.m_colMetaRef[col].pColName;
			}
			else
			{
				text = this.m_colMetaRef[col].pColAlias;
			}
			if (text != null && caseSensitive && baseColumn)
			{
				return text.Insert(text.Length, "\"").Insert(0, "\"");
			}
			return text;
		}

		// Token: 0x06000D33 RID: 3379 RVA: 0x000899D8 File Offset: 0x000889D8
		private unsafe OpoMetValCtx* GetOpoMetValCtx()
		{
			if (this.m_pOpoMetValCtx == null)
			{
				if (this.m_dataAdapter == null)
				{
					throw new InvalidOperationException();
				}
				if (this.m_dataAdapter.SelectCommand == null)
				{
					throw new InvalidOperationException();
				}
				this.m_metaData = this.m_dataAdapter.SelectCommand.InternalPrepare(true);
				if (this.m_metaData != null)
				{
					if (!this.m_dataAdapter.SelectCommand.AddRowid && this.m_metaData.m_pOpoMetValCtx != null)
					{
						this.m_pOpoMetValCtx = this.m_metaData.m_pOpoMetValCtx;
					}
					else if (this.m_dataAdapter.SelectCommand.AddRowid && this.m_metaData.m_pOpoMetValCtxWRowid != null)
					{
						this.m_pOpoMetValCtx = this.m_metaData.m_pOpoMetValCtxWRowid;
					}
					else
					{
						this.m_pOpoMetValCtx = null;
					}
				}
			}
			return this.m_pOpoMetValCtx;
		}

		// Token: 0x06000D34 RID: 3380 RVA: 0x00089AA8 File Offset: 0x00088AA8
		private unsafe OpoMetRefCtx GetOpoMetRefCtx()
		{
			IntPtr ptr = IntPtr.Zero;
			if (this.m_opoMetRefCtx == null)
			{
				if (this.m_pOpoMetValCtx == null)
				{
					this.GetOpoMetValCtx();
					if (this.m_pOpoMetValCtx == null)
					{
						return null;
					}
				}
				ptr = this.m_pOpoMetValCtx->pOpoMetRefCtx;
				this.m_opoMetRefCtx = new OpoMetRefCtx();
				Marshal.PtrToStructure(ptr, this.m_opoMetRefCtx);
			}
			return this.m_opoMetRefCtx;
		}

		// Token: 0x06000D35 RID: 3381 RVA: 0x00089B0C File Offset: 0x00088B0C
		private unsafe ColMetaRef[] GetColMetaRef()
		{
			IntPtr intPtr = IntPtr.Zero;
			if (this.m_colMetaRef == null)
			{
				if (this.m_pOpoMetValCtx == null)
				{
					this.GetOpoMetValCtx();
					if (this.m_pOpoMetValCtx == null)
					{
						return null;
					}
				}
				if (this.m_opoMetRefCtx == null)
				{
					this.GetOpoMetRefCtx();
					if (this.m_opoMetRefCtx == null)
					{
						return null;
					}
				}
				int noOfCols = (int)this.m_pOpoMetValCtx->NoOfCols;
				intPtr = this.m_opoMetRefCtx.pColMetaRef;
				this.m_colMetaRef = new ColMetaRef[noOfCols];
				for (int i = 0; i < noOfCols; i++)
				{
					this.m_colMetaRef[i] = new ColMetaRef();
					Marshal.PtrToStructure(intPtr, this.m_colMetaRef[i]);
					intPtr = (IntPtr)((void*)((byte*)((void*)intPtr) + Marshal.SizeOf(this.m_colMetaRef[i])));
				}
			}
			return this.m_colMetaRef;
		}

		// Token: 0x06000D36 RID: 3382 RVA: 0x00089BD0 File Offset: 0x00088BD0
		private OracleDbType GetColumnType(DataColumn col)
		{
			OracleDbType result = OracleDbType.Varchar2;
			object obj = col.ExtendedProperties["OraDbType"];
			string s;
			if (obj == null)
			{
				int num = this.FindBaseColumnOrdinal(col);
				if (num != -1)
				{
					result = this.GetColumnType(num);
				}
			}
			else if ((s = (obj as string)) != null)
			{
				result = (OracleDbType)int.Parse(s);
			}
			else
			{
				result = (OracleDbType)obj;
			}
			return result;
		}

		// Token: 0x06000D37 RID: 3383 RVA: 0x00089C28 File Offset: 0x00088C28
		private string GetUdtTypeName(DataColumn col)
		{
			object obj = col.ExtendedProperties["UdtTypeName"];
			if (obj == null)
			{
				int num = this.FindBaseColumnOrdinal(col);
				if (num != -1)
				{
					obj = this.GetUdtTypeName(num);
				}
			}
			return (string)obj;
		}

		// Token: 0x06000D38 RID: 3384 RVA: 0x00089C64 File Offset: 0x00088C64
		private int FindBaseColumnOrdinal(DataColumn col)
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
			if (this.m_colMetaRef == null)
			{
				this.GetColMetaRef();
				if (this.m_colMetaRef == null)
				{
					return -1;
				}
			}
			for (int i = 0; i < this.m_colMetaRef.Length; i++)
			{
				if (this.m_colMetaRef[i].pColAlias != null && a == this.m_colMetaRef[i].pColAlias)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000D39 RID: 3385 RVA: 0x00089D44 File Offset: 0x00088D44
		private unsafe OracleDbType GetColumnType(int col)
		{
			OracleDbType oracleDbType = OracleDbType.Varchar2;
			bool flag = true;
			if (this.m_pOpoMetValCtx == null)
			{
				this.GetOpoMetValCtx();
				if (this.m_pOpoMetValCtx == null)
				{
					return oracleDbType;
				}
			}
			ushort oraType = this.m_pOpoMetValCtx->pColMetaVal[col].OraType;
			if (oraType == 2)
			{
				int scale = (int)this.m_pOpoMetValCtx->pColMetaVal[col].Scale;
				int precision = (int)this.m_pOpoMetValCtx->pColMetaVal[col].Precision;
				return OraDb_DbTypeTable.ConvertNumberToOraDbType(precision, scale);
			}
			oracleDbType = (OracleDbType)OraDb_DbTypeTable.oraTypeToOracleDbTypeMapping[(int)oraType];
			if (this.m_pOpoMetValCtx->pColMetaVal[col].CharSetForm != 2)
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
				switch (oracleDbType2)
				{
				case OracleDbType.Varchar2:
					if (flag)
					{
						oracleDbType = OracleDbType.NVarchar2;
					}
					break;
				case OracleDbType.XmlType:
					if (this.m_pOpoMetValCtx->pColMetaVal[col].bIsXmlType != 1)
					{
						oracleDbType = OracleDbType.Object;
					}
					break;
				}
				break;
			}
			return oracleDbType;
		}

		// Token: 0x06000D3A RID: 3386 RVA: 0x00089E60 File Offset: 0x00088E60
		private string GetUdtTypeName(int col)
		{
			if (this.m_colMetaRef == null)
			{
				this.GetColMetaRef();
				if (this.m_colMetaRef == null)
				{
					return null;
				}
			}
			return this.m_colMetaRef[col].pUdtSchemaName + "." + this.m_colMetaRef[col].pUdtTypeName;
		}

		// Token: 0x06000D3B RID: 3387 RVA: 0x00089EAC File Offset: 0x00088EAC
		private bool IsColumnModified(DataRow row, DataColumn col)
		{
			return !row[col, DataRowVersion.Current].Equals(row[col, DataRowVersion.Original]);
		}

		// Token: 0x06000D3C RID: 3388 RVA: 0x00089ED0 File Offset: 0x00088ED0
		private bool IsRowOrigValueNull(DataRow row, DataColumn col)
		{
			object obj = row[col, DataRowVersion.Original];
			return obj == DBNull.Value || (obj is INullable && (obj as INullable).IsNull);
		}

		// Token: 0x06000D3D RID: 3389 RVA: 0x00089F0C File Offset: 0x00088F0C
		private OracleParameter CreateParams(string paramName, string colName, OracleDbType colType, DataRowVersion version, object value)
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
			return new OracleParameter(paramName, colType, size, colName, version, value)
			{
				m_modified = false
			};
		}

		// Token: 0x06000D3E RID: 3390 RVA: 0x00089FB4 File Offset: 0x00088FB4
		private void SetParam(string colName, OracleDbType colType, DataRowVersion version, object value, OracleParameter param)
		{
			if (param.m_modified)
			{
				param.Direction = ParameterDirection.Input;
				param.IsNullable = false;
				param.Offset = 0;
				param.Status = OracleParameterStatus.Success;
				param.Precision = 100;
				param.Scale = 129;
				param.UdtTypeName = null;
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

		// Token: 0x06000D3F RID: 3391 RVA: 0x0008A0B9 File Offset: 0x000890B9
		private bool IsOraLOB(OracleDbType colType)
		{
			return colType == OracleDbType.Blob || colType == OracleDbType.Clob || colType == OracleDbType.NClob || colType == OracleDbType.BFile;
		}

		// Token: 0x06000D40 RID: 3392 RVA: 0x0008A0D1 File Offset: 0x000890D1
		private bool IsOraLONG(OracleDbType colType)
		{
			return colType == OracleDbType.Long || colType == OracleDbType.LongRaw;
		}

		// Token: 0x06000D41 RID: 3393 RVA: 0x0008A0DF File Offset: 0x000890DF
		private bool IsOraXmlType(OracleDbType colType)
		{
			return colType == OracleDbType.XmlType;
		}

		// Token: 0x06000D42 RID: 3394 RVA: 0x0008A0E6 File Offset: 0x000890E6
		private bool IsOraUDT(OracleDbType colType)
		{
			return colType == OracleDbType.Object || colType == OracleDbType.Array;
		}

		// Token: 0x06000D43 RID: 3395 RVA: 0x0008A0FA File Offset: 0x000890FA
		private string GetBaseTableName()
		{
			return this.GetBaseTableName(this.m_caseSensitive);
		}

		// Token: 0x06000D44 RID: 3396 RVA: 0x0008A108 File Offset: 0x00089108
		private string GetBaseTableName(bool caseSensitive)
		{
			string text = null;
			this.GetOpoMetRefCtx();
			if (this.m_opoMetRefCtx != null)
			{
				text = this.m_opoMetRefCtx.pTableName;
			}
			if (text == null)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.BLR_MULTITABLE_DS, new string[0]));
			}
			if (caseSensitive)
			{
				return text.Insert(text.Length, "\"").Insert(0, "\"");
			}
			return text;
		}

		// Token: 0x06000D45 RID: 3397 RVA: 0x0008A16C File Offset: 0x0008916C
		private string GetBaseTableName(DataTable table)
		{
			return this.GetBaseTableName(table, this.m_caseSensitive);
		}

		// Token: 0x06000D46 RID: 3398 RVA: 0x0008A17C File Offset: 0x0008917C
		private string GetBaseTableName(DataTable table, bool caseSensitive)
		{
			string text = null;
			object obj = table.ExtendedProperties["BaseTable.0"];
			if (obj == null)
			{
				this.GetOpoMetRefCtx();
				if (this.m_opoMetRefCtx != null)
				{
					text = this.m_opoMetRefCtx.pTableName;
				}
			}
			else
			{
				text = (string)obj;
			}
			if (text == null)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.BLR_MULTITABLE_DS, new string[0]));
			}
			if (caseSensitive)
			{
				return text.Insert(text.Length, "\"").Insert(0, "\"");
			}
			return text;
		}

		// Token: 0x06000D47 RID: 3399 RVA: 0x0008A200 File Offset: 0x00089200
		protected override DataTable GetSchemaTable(DbCommand srcCommand)
		{
			OracleCommand oracleCommand = srcCommand as OracleCommand;
			OracleDataReader oracleDataReader = oracleCommand.ExecuteReader(CommandBehavior.SchemaOnly | CommandBehavior.KeyInfo);
			return oracleDataReader.GetSchemaTable();
		}

		// Token: 0x06000D48 RID: 3400 RVA: 0x0008A224 File Offset: 0x00089224
		private string GetSchemaName()
		{
			StringBuilder stringBuilder = new StringBuilder();
			string text = null;
			this.GetOpoMetRefCtx();
			if (this.m_opoMetRefCtx != null)
			{
				text = this.m_opoMetRefCtx.pSchemaName;
			}
			if (text != null && text.Length != 0)
			{
				if (this.m_caseSensitive)
				{
					stringBuilder.Append("\"");
					stringBuilder.Append(this.m_opoMetRefCtx.pSchemaName);
					stringBuilder.Append("\"");
				}
				else
				{
					stringBuilder.Append(this.m_opoMetRefCtx.pSchemaName);
				}
				stringBuilder.Append(".");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000D49 RID: 3401 RVA: 0x0008A2B8 File Offset: 0x000892B8
		private string GetSchemaName(DataTable table)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string text = null;
			object obj = table.ExtendedProperties["BaseSchema"];
			if (obj == null)
			{
				this.GetOpoMetRefCtx();
				if (this.m_opoMetRefCtx != null)
				{
					text = this.m_opoMetRefCtx.pSchemaName;
				}
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
			return stringBuilder.ToString();
		}

		// Token: 0x06000D4A RID: 3402 RVA: 0x0008A354 File Offset: 0x00089354
		private bool CheckDataTable(DataTable table)
		{
			if (table.ExtendedProperties["BaseTable.1"] != null)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.BLR_MULTITABLE_DS, new string[0]));
			}
			return true;
		}

		// Token: 0x06000D4B RID: 3403 RVA: 0x0008A380 File Offset: 0x00089380
		private void CheckPrimaryKey(DataRow row)
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
					StoredProcedureInfo storedProcInfo = RegAndConfigRdr.GetStoredProcInfo(this.m_dataAdapter.SelectCommand.CommandText);
					if (storedProcInfo != null)
					{
						string text = (string)row.Table.ExtendedProperties["REFCursorName"];
						int num = -1;
						if (text.Equals("REFCursor"))
						{
							num = 0;
						}
						else
						{
							int.TryParse(text.Substring("RefCursor".Length), out num);
						}
						if (num > -1)
						{
							RefCursorInfo refCursorInfo = (RefCursorInfo)storedProcInfo.refCursors[num];
							if (refCursorInfo.isPrimaryKeyPresent)
							{
								return;
							}
						}
					}
				}
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.BLR_NO_PRIMARYKEY, new string[0]));
			}
			if (this.m_pOpoMetValCtx == null)
			{
				this.GetOpoMetValCtx();
			}
			this.CheckPrimaryKey();
		}

		// Token: 0x06000D4C RID: 3404 RVA: 0x0008A4A4 File Offset: 0x000894A4
		private unsafe void CheckPrimaryKey()
		{
			if (this.m_pOpoMetValCtx == null)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.BLR_NO_PRIMARYKEY, new string[0]));
			}
			if (this.m_pOpoMetValCtx->bPkFetched != 1)
			{
				this.m_dataAdapter.SelectCommand.GetPrimaryKey(this.m_metaData, true);
			}
			if (this.m_pOpoMetValCtx->bPkPresent != 1)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.BLR_NO_PRIMARYKEY, new string[0]));
			}
		}

		// Token: 0x06000D4D RID: 3405 RVA: 0x0008A51A File Offset: 0x0008951A
		private unsafe bool IsNullableCol(int index)
		{
			return this.m_pOpoMetValCtx->pColMetaVal[index].NullOK == 1;
		}

		// Token: 0x06000D4E RID: 3406 RVA: 0x0008A53C File Offset: 0x0008953C
		private bool IsRowCurrentValueNull(DataRow row, DataColumn col)
		{
			object obj = row[col, DataRowVersion.Current];
			return obj == DBNull.Value || (obj is INullable && (obj as INullable).IsNull);
		}

		// Token: 0x04000A73 RID: 2675
		private const string QUOTE = "\"";

		// Token: 0x04000A74 RID: 2676
		private const string SCHEMA_SEPERATOR = ".";

		// Token: 0x04000A75 RID: 2677
		private const int NO_OF_PARAMS = 10;

		// Token: 0x04000A76 RID: 2678
		private const int NAME_IN = 0;

		// Token: 0x04000A77 RID: 2679
		private const int PARAM_COUNT_IN = 1;

		// Token: 0x04000A78 RID: 2680
		private const int PARAM_COUNT_OUT = 2;

		// Token: 0x04000A79 RID: 2681
		private const int PARAM_NAME_OUT = 3;

		// Token: 0x04000A7A RID: 2682
		private const int DIRECTION_OUT = 4;

		// Token: 0x04000A7B RID: 2683
		private const int ORADBTYPE_OUT = 5;

		// Token: 0x04000A7C RID: 2684
		private const int SIZE_OUT = 6;

		// Token: 0x04000A7D RID: 2685
		private const int TYPE_NAME_OUT = 7;

		// Token: 0x04000A7E RID: 2686
		private const int POSITION_OUT = 8;

		// Token: 0x04000A7F RID: 2687
		private const int DATA_LEVEL_OUT = 9;

		// Token: 0x04000A80 RID: 2688
		private const int MAX_ARG_NAME_LENGTH = 128;

		// Token: 0x04000A81 RID: 2689
		private const int FIRST_FETCH_COUNT = 128;

		// Token: 0x04000A82 RID: 2690
		private const string DP_COMMAND_TEXT = "DECLARE TYPE NUM_TABLE IS TABLE OF NUMBER INDEX BY PLS_INTEGER; TYPE STR_TABLE IS TABLE OF VARCHAR2(128) INDEX BY PLS_INTEGER; name_in          VARCHAR2(2000); param_count_in   PLS_INTEGER; link\t     \t      VARCHAR2(2000); context          NUMBER := 1; schema           VARCHAR2(2000); part1            VARCHAR2(2000); part2            VARCHAR2(2000); dblink           VARCHAR2(2000); part1_type       NUMBER; object_number    NUMBER; refcur\t          SYS_REFCURSOR; idx\t\t          PLS_INTEGER := 0; param_count_out        PLS_INTEGER := 0; param_name_out         STR_TABLE; direction_out          NUM_TABLE; oradbtype_out          NUM_TABLE; size_out               NUM_TABLE; type_name_out          STR_TABLE; position_out           NUM_TABLE; data_level_out         NUM_TABLE; param_name_null_out    STR_TABLE; direction_null_out     NUM_TABLE; oradbtype_null_out     NUM_TABLE; size_null_out          NUM_TABLE; type_name_null_out     STR_TABLE; position_null_out      NUM_TABLE; data_level_null_out    NUM_TABLE; BEGIN name_in := :1; param_count_in := :2; link := NULL; DBMS_UTILITY.NAME_RESOLVE( name_in, context, schema, part1, part2, dblink, part1_type, object_number); WHILE (dblink IS NOT NULL) LOOP link := '@' || dblink; name_in := NULL; IF (schema IS NOT NULL) THEN name_in := '\"' || schema || '\"' || '.'; END IF; IF (part1 IS NOT NULL) THEN name_in := name_in || '\"' || part1 || '\"' || '.'; END IF; IF (part2 IS NULL) THEN name_in := RTRIM(name_in, '.'); ELSE name_in := name_in || '\"' || part2 || '\"'; END IF; EXECUTE IMMEDIATE 'BEGIN DBMS_UTILITY.NAME_RESOLVE' || link || '(:1, :2, :3, :4, :5, :6, :7, :8); END;' USING name_in, context, OUT schema, OUT part1, OUT part2, OUT dblink, OUT part1_type, OUT object_number; END LOOP; IF (param_count_out = 0) THEN OPEN refcur FOR 'SELECT  DECODE (POSITION, 0, ''RETURN_VALUE'', ARGUMENT_NAME) param_name, DECODE(IN_OUT, ''IN'', \t  1,  ''IN/OUT'', 3, ''OUT'', DECODE(ARGUMENT_NAME, NULL, 6, 2), 1) direction, DECODE(DATA_TYPE, ''BINARY_DOUBLE'',  108, ''BINARY_FLOAT'',   122, ''BINARY_INTEGER'', 112, ''BFILE'',     101, ''BLOB'',\t    102, ''CHAR'',\t    104, ''CLOB'',\t    105, ''DATE'',\t    106, ''FLOAT'',\t    107, ''INTERVAL YEAR TO MONTH'', 115, ''INTERVAL DAY TO SECOND'', 114, ''LONG'',\t    109, ''LONG RAW'',\t110, ''NCHAR'',\t    117, ''NCLOB'',\t    116, ''NUMBER'',\t  107, ''NVARCHAR2'',\t119, ''OBJECT'', \t  129, ''RAW'',\t      120, ''REF'',       130, ''REF CURSOR'',121, ''ROWID'',\t    126, ''TABLE'', \t  128, ''TIMESTAMP'',\t123, ''TIMESTAMP WITH LOCAL TIME ZONE'', 124, ''TIMESTAMP WITH TIME ZONE'', 125, ''UNDEFINED'', 100, ''UROWID'',\t  126, ''VARCHAR'', \t126, ''VARCHAR2'',\t126, ''VARRAY'', \t  128, ''PL/SQL TABLE'',1, NULL,            0, -1) oradbtype, DECODE(DATA_TYPE, ''CHAR'', \t      2000, ''LONG'',\t      4000, ''LONG RAW'',    4000, ''NCHAR'', \t    2000, ''NVARCHAR2'',   4000, ''RAW'', \t      2000, ''ROWID'', \t    4000, ''UROWID'',\t    4000, ''VARCHAR'', \t  4000, ''VARCHAR2'', \t  4000, ''PL/SQL TABLE'',  16, 0) length, (TYPE_OWNER ||  DECODE(TYPE_OWNER, NULL, NULL, ''.'') ||  TYPE_NAME ) type_name, POSITION   position, DATA_LEVEL data_level FROM ALL_ARGUMENTS' || link || ' WHERE OWNER \t\t  = :1 \t  AND (PACKAGE_NAME \t= :2 \t  OR (:3 IS NULL AND PACKAGE_NAME is null)) AND OBJECT_NAME \t  = :4 \t  AND NVL(overload, 1) = 1 ORDER BY SEQUENCE' USING schema, part1, part1, part2; FETCH refcur BULK COLLECT INTO param_name_out, direction_out, oradbtype_out, size_out, type_name_out, position_out, data_level_out; param_count_out := refcur%ROWCOUNT; CLOSE refcur; END IF; IF (part1_type = 9 AND param_count_out = 0) THEN param_count_out := -1002; END IF; :3 := param_count_out; IF (param_count_out > param_count_in OR param_count_out < 0) THEN param_name_out:= param_name_null_out; direction_out := direction_null_out; oradbtype_out := oradbtype_null_out; size_out      := size_null_out; type_name_out := type_name_null_out; position_out  := position_null_out; data_level_out:= data_level_null_out; END IF; :4 := param_name_out; :5 := direction_out; :6 := oradbtype_out; :7 := size_out; :8 := type_name_out; :9 := position_out; :10:= data_level_out; END;";

		// Token: 0x04000A83 RID: 2691
		private const string DP_COMMAND_TEXT_db9i = "DECLARE TYPE NUM_TABLE IS TABLE OF NUMBER INDEX BY BINARY_INTEGER; TYPE STR_TABLE IS TABLE OF VARCHAR2(128) INDEX BY BINARY_INTEGER; TYPE REF_CURSOR IS REF CURSOR; name_in          VARCHAR2(2000); param_count_in   BINARY_INTEGER; link                   VARCHAR2(2000); context          NUMBER := 1; schema           VARCHAR2(2000); part1            VARCHAR2(2000); part2            VARCHAR2(2000); dblink           VARCHAR2(2000); part1_type       NUMBER; object_number    NUMBER; refcur              SYS_REFCURSOR; idx                  BINARY_INTEGER := 1; param_count_out        BINARY_INTEGER := 0; param_name_out         STR_TABLE; direction_out          NUM_TABLE; oradbtype_out          NUM_TABLE; size_out               NUM_TABLE; type_name_out          STR_TABLE; position_out           NUM_TABLE; data_level_out         NUM_TABLE; param_name_null_out    STR_TABLE; direction_null_out     NUM_TABLE; oradbtype_null_out     NUM_TABLE; size_null_out          NUM_TABLE; type_name_null_out     STR_TABLE; position_null_out      NUM_TABLE; data_level_null_out    NUM_TABLE; BEGIN name_in := :1; param_count_in := :2; link := NULL; DBMS_UTILITY.NAME_RESOLVE( name_in, context, schema, part1, part2, dblink, part1_type, object_number); WHILE (dblink IS NOT NULL) LOOP link := '@' || dblink; name_in := NULL; IF (schema IS NOT NULL) THEN name_in := '\"' || schema || '\"' || '.'; END IF; IF (part1 IS NOT NULL) THEN name_in := name_in || '\"' || part1 || '\"' || '.'; END IF; IF (part2 IS NULL) THEN name_in := RTRIM(name_in, '.'); ELSE name_in := name_in || '\"' || part2 || '\"'; END IF; EXECUTE IMMEDIATE 'BEGIN DBMS_UTILITY.NAME_RESOLVE' || link || '(:1, :2, :3, :4, :5, :6, :7, :8); END;' USING name_in, context, OUT schema, OUT part1, OUT part2, OUT dblink, OUT part1_type, OUT object_number; END LOOP; IF (param_count_out = 0) THEN OPEN refcur FOR 'SELECT  DECODE (POSITION, 0, ''RETURN_VALUE'', ARGUMENT_NAME) param_name, DECODE(IN_OUT, ''IN'',       1,  ''IN/OUT'', 3, ''OUT'', DECODE(ARGUMENT_NAME, NULL, 6, 2), 1) direction, DECODE(DATA_TYPE, ''BINARY_DOUBLE'',  108, ''BINARY_FLOAT'',   122, ''BINARY_INTEGER'', 112, ''BFILE'',     101, ''BLOB'',        102, ''CHAR'',        104, ''CLOB'',        105, ''DATE'',        106, ''FLOAT'',        107, ''INTERVAL YEAR TO MONTH'', 115, ''INTERVAL DAY TO SECOND'', 114, ''LONG'',        109, ''LONG RAW'',    110, ''NCHAR'',        117, ''NCLOB'',        116, ''NUMBER'',      107, ''NVARCHAR2'',    119, ''OBJECT'',      129, ''RAW'',\t        120, ''REF'',         130, ''REF CURSOR'',121, ''ROWID'',        126, ''TABLE'',       128, ''TIMESTAMP'',    123, ''TIMESTAMP WITH LOCAL TIME ZONE'', 124, ''TIMESTAMP WITH TIME ZONE'', 125, ''UNDEFINED'', 100, ''UROWID'',      126, ''VARCHAR'',     126, ''VARCHAR2'',    126, ''VARRAY'',      128, ''PL/SQL TABLE'',1, NULL,            0, -1) oradbtype, DECODE(DATA_TYPE, ''CHAR'',           2000, ''LONG'',          4000, ''LONG RAW'',    4000, ''NCHAR'',         2000, ''NVARCHAR2'',   4000, ''RAW'',           2000, ''ROWID'',         4000, ''UROWID'',        4000, ''VARCHAR'',       4000, ''VARCHAR2'',       4000, ''PL/SQL TABLE'',  16, 0) length, (TYPE_OWNER ||  DECODE(TYPE_OWNER, NULL, NULL, ''.'') ||  TYPE_NAME ) type_name, POSITION   position, DATA_LEVEL data_level FROM ALL_ARGUMENTS' || link || ' WHERE OWNER = :1 AND  (PACKAGE_NAME = :2 OR  (:3 IS NULL AND PACKAGE_NAME = OBJECT_NAME)) AND  OBJECT_NAME = :4 AND NVL(overload, 1) = 1 ORDER BY SEQUENCE' USING schema, part1, part1, part2; FETCH refcur BULK COLLECT INTO param_name_out, direction_out, oradbtype_out, size_out, type_name_out, position_out, data_level_out; param_count_out := refcur%ROWCOUNT; CLOSE refcur; END IF; IF (part1_type = 9 AND param_count_out = 0) THEN param_count_out := -1002; END IF; :3 := param_count_out; IF (param_count_out > param_count_in OR param_count_out < 0) THEN param_name_out:= param_name_null_out; direction_out := direction_null_out; oradbtype_out := oradbtype_null_out; size_out      := size_null_out; type_name_out := type_name_null_out; position_out  := position_null_out; data_level_out:= data_level_null_out; END IF; :4 := param_name_out; :5 := direction_out; :6 := oradbtype_out; :7 := size_out; :8 := type_name_out; :9 := position_out; :10:= data_level_out; END;";

		// Token: 0x04000A84 RID: 2692
		private const string DP_COMMAND_TEXT_db8i = "DECLARE TYPE NUM_TABLE IS TABLE OF NUMBER INDEX BY BINARY_INTEGER; TYPE STR_TABLE IS TABLE OF VARCHAR2(128) INDEX BY BINARY_INTEGER; TYPE REF_CURSOR IS REF CURSOR; name_in          VARCHAR2(2000); param_count_in   PLS_INTEGER; link\t     \t      VARCHAR2(2000); context          NUMBER := 1; schema           VARCHAR2(2000); part1            VARCHAR2(2000); part2            VARCHAR2(2000); dblink           VARCHAR2(2000); part1_type       NUMBER; object_number    NUMBER; refcur           REF_CURSOR; idx\t\t          PLS_INTEGER := 1; param_count_out        PLS_INTEGER := 0; param_name_out         STR_TABLE; direction_out          NUM_TABLE; oradbtype_out          NUM_TABLE; size_out               NUM_TABLE; type_name_out          STR_TABLE; position_out           NUM_TABLE; data_level_out         NUM_TABLE; param_name_null_out    STR_TABLE; direction_null_out     NUM_TABLE; oradbtype_null_out     NUM_TABLE; size_null_out          NUM_TABLE; type_name_null_out     STR_TABLE; position_null_out      NUM_TABLE; data_level_null_out    NUM_TABLE; BEGIN name_in := :1; param_count_in := :2; link := NULL; DBMS_UTILITY.NAME_RESOLVE( name_in, context, schema, part1, part2, dblink, part1_type, object_number); WHILE (dblink IS NOT NULL) LOOP link := '@' || dblink; name_in := NULL; IF (schema IS NOT NULL) THEN name_in := '\"' || schema || '\"' || '.'; END IF; IF (part1 IS NOT NULL) THEN name_in := name_in || '\"' || part1 || '\"' || '.'; END IF; IF (part2 IS NULL) THEN name_in := RTRIM(name_in, '.'); ELSE name_in := name_in || '\"' || part2 || '\"'; END IF; EXECUTE IMMEDIATE 'BEGIN DBMS_UTILITY.NAME_RESOLVE' || link || '(:1, :2, :3, :4, :5, :6, :7, :8); END;' USING name_in, context, OUT schema, OUT part1, OUT part2, OUT dblink, OUT part1_type, OUT object_number; END LOOP; IF (param_count_out = 0) THEN OPEN refcur FOR 'SELECT  DECODE (POSITION, 0, ''RETURN_VALUE'', ARGUMENT_NAME) param_name, DECODE(IN_OUT, ''IN'', \t  1,  ''IN/OUT'', 3, ''OUT'', DECODE(ARGUMENT_NAME, NULL, 6, 2), 1) direction, DECODE(DATA_TYPE, ''BINARY_DOUBLE'',  108, ''BINARY_FLOAT'',   122, ''BINARY_INTEGER'', 112, ''BFILE'',     101, ''BLOB'',\t    102, ''CHAR'',\t    104, ''CLOB'',\t    105, ''DATE'',\t    106, ''FLOAT'',\t    107, ''INTERVAL YEAR TO MONTH'', 115, ''INTERVAL DAY TO SECOND'', 114, ''LONG'',\t    109, ''LONG RAW'',\t110, ''NCHAR'',\t    117, ''NCLOB'',\t    116, ''NUMBER'',\t  107, ''NVARCHAR2'',\t119, ''OBJECT'', \t  129, ''RAW'',\t      120, ''REF'',       130, ''REF CURSOR'',121, ''ROWID'',\t    126, ''TABLE'', \t  128, ''TIMESTAMP'',\t123, ''TIMESTAMP WITH LOCAL TIME ZONE'', 124, ''TIMESTAMP WITH TIME ZONE'', 125, ''UNDEFINED'', 100, ''UROWID'',\t  126, ''VARCHAR'', \t126, ''VARCHAR2'',\t126, ''VARRAY'', \t  128, ''PL/SQL TABLE'',1, NULL,            0, -1) oradbtype, DECODE(DATA_TYPE, ''CHAR'', \t      2000, ''LONG'',\t      4000, ''LONG RAW'',    4000, ''NCHAR'', \t    2000, ''NVARCHAR2'',   4000, ''RAW'', \t      2000, ''ROWID'', \t    4000, ''UROWID'',\t    4000, ''VARCHAR'', \t  4000, ''VARCHAR2'', \t  4000, ''PL/SQL TABLE'',  16, 0) length, (TYPE_OWNER ||  DECODE(TYPE_OWNER, NULL, NULL, ''.'') ||  TYPE_NAME ) type_name, POSITION   position, DATA_LEVEL data_level FROM ALL_ARGUMENTS' || link || ' WHERE OWNER \t\t  = :1 \t  AND (PACKAGE_NAME \t= :2 \t  OR (:3 IS NULL AND PACKAGE_NAME is null)) AND OBJECT_NAME \t  = :4 \t  AND NVL(overload, 1) = 1 ORDER BY SEQUENCE' USING schema, part1, part1, part2; LOOP   FETCH refcur INTO param_name_out(idx), direction_out(idx),     oradbtype_out(idx), size_out(idx), type_name_out(idx),     position_out(idx), data_level_out(idx);   EXIT WHEN refcur%NOTFOUND;   idx := idx + 1; END LOOP; param_count_out := refcur%ROWCOUNT; CLOSE refcur; END IF; IF (part1_type = 9 AND param_count_out = 0) THEN param_count_out := -1002; END IF; :3 := param_count_out; IF (param_count_out > param_count_in OR param_count_out < 0) THEN param_name_out:= param_name_null_out; direction_out := direction_null_out; oradbtype_out := oradbtype_null_out; size_out      := size_null_out; type_name_out := type_name_null_out; position_out  := position_null_out; data_level_out:= data_level_null_out; END IF; :4 := param_name_out; :5 := direction_out; :6 := oradbtype_out; :7 := size_out; :8 := type_name_out; :9 := position_out; :10:= data_level_out; END;";

		// Token: 0x04000A85 RID: 2693
		private OracleDataAdapter m_dataAdapter;

		// Token: 0x04000A86 RID: 2694
		private bool m_caseSensitive;

		// Token: 0x04000A87 RID: 2695
		private unsafe OpoMetValCtx* m_pOpoMetValCtx;

		// Token: 0x04000A88 RID: 2696
		private OpoMetRefCtx m_opoMetRefCtx;

		// Token: 0x04000A89 RID: 2697
		private ColMetaRef[] m_colMetaRef;

		// Token: 0x04000A8A RID: 2698
		private OracleRowUpdatingEventHandler m_hndr;

		// Token: 0x04000A8B RID: 2699
		private bool m_disposed;

		// Token: 0x04000A8C RID: 2700
		private OracleCommand m_deleteCmd;

		// Token: 0x04000A8D RID: 2701
		private OracleCommand m_insertCmd;

		// Token: 0x04000A8E RID: 2702
		private OracleCommand m_updateCmd;

		// Token: 0x04000A8F RID: 2703
		private ArrayList m_cachedInsertParams;

		// Token: 0x04000A90 RID: 2704
		private ArrayList m_cachedUpdateParams;

		// Token: 0x04000A91 RID: 2705
		private ArrayList m_cachedDeleteParams;

		// Token: 0x04000A92 RID: 2706
		private MetaData m_metaData;

		// Token: 0x04000A93 RID: 2707
		private static OracleCommand m_dpCommand;

		// Token: 0x04000A94 RID: 2708
		private static OracleParameter[] m_dpCommandParams;

		// Token: 0x04000A95 RID: 2709
		private bool m_ODTDesignMode;

		// Token: 0x04000A96 RID: 2710
		private static int m_sMaxParamNameLen = 30;

		// Token: 0x04000A97 RID: 2711
		private static int m_sMaxExpansionRatio = 3;

		// Token: 0x04000A98 RID: 2712
		private static object m_staticLock = new object();
	}
}
