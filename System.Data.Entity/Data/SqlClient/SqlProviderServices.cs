using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Common.CommandTrees;
using System.Data.Common.Utils;
using System.Data.Entity;
using System.Data.EntityClient;
using System.Data.Metadata.Edm;
using System.Data.Spatial;
using System.Data.SqlClient.SqlGen;
using System.Globalization;
using System.IO;

namespace System.Data.SqlClient
{
	// Token: 0x02000029 RID: 41
	[CLSCompliant(false)]
	public sealed class SqlProviderServices : DbProviderServices
	{
		// Token: 0x060003B4 RID: 948 RVA: 0x0000DD88 File Offset: 0x0000BF88
		private SqlProviderServices()
		{
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060003B5 RID: 949 RVA: 0x0000DD90 File Offset: 0x0000BF90
		public static SqlProviderServices SingletonInstance
		{
			get
			{
				return SqlProviderServices.Instance;
			}
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x0000DD98 File Offset: 0x0000BF98
		protected override DbCommandDefinition CreateDbCommandDefinition(DbProviderManifest providerManifest, DbCommandTree commandTree)
		{
			DbCommand prototype = this.CreateCommand(providerManifest, commandTree);
			return this.CreateCommandDefinition(prototype);
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0000DDB8 File Offset: 0x0000BFB8
		internal override DbCommand CreateCommand(DbCommandTree commandTree)
		{
			EntityUtil.CheckArgumentNull<DbCommandTree>(commandTree, "commandTree");
			StoreItemCollection storeItemCollection = (StoreItemCollection)commandTree.MetadataWorkspace.GetItemCollection(DataSpace.SSpace);
			return this.CreateCommand(storeItemCollection.StoreProviderManifest, commandTree);
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x0000DDF0 File Offset: 0x0000BFF0
		private DbCommand CreateCommand(DbProviderManifest providerManifest, DbCommandTree commandTree)
		{
			EntityUtil.CheckArgumentNull<DbProviderManifest>(providerManifest, "providerManifest");
			EntityUtil.CheckArgumentNull<DbCommandTree>(commandTree, "commandTree");
			SqlProviderManifest sqlProviderManifest = providerManifest as SqlProviderManifest;
			if (sqlProviderManifest == null)
			{
				throw EntityUtil.Argument(Strings.Mapping_Provider_WrongManifestType(typeof(SqlProviderManifest)));
			}
			SqlVersion sqlVersion = sqlProviderManifest.SqlVersion;
			SqlCommand sqlCommand = new SqlCommand();
			List<SqlParameter> list;
			CommandType commandType;
			HashSet<string> hashSet;
			sqlCommand.CommandText = SqlGenerator.GenerateSql(commandTree, sqlVersion, out list, out commandType, out hashSet);
			sqlCommand.CommandType = commandType;
			EdmFunction edmFunction = null;
			if (commandTree.CommandTreeKind == DbCommandTreeKind.Function)
			{
				edmFunction = ((DbFunctionCommandTree)commandTree).EdmFunction;
			}
			foreach (KeyValuePair<string, TypeUsage> keyValuePair in commandTree.Parameters)
			{
				FunctionParameter functionParameter;
				SqlParameter value;
				if (edmFunction != null && edmFunction.Parameters.TryGetValue(keyValuePair.Key, false, out functionParameter))
				{
					value = SqlProviderServices.CreateSqlParameter(functionParameter.Name, functionParameter.TypeUsage, functionParameter.Mode, DBNull.Value, false, sqlVersion);
				}
				else
				{
					TypeUsage type;
					if (hashSet != null && hashSet.Contains(keyValuePair.Key))
					{
						type = keyValuePair.Value.ShallowCopy(new FacetValues
						{
							Unicode = new bool?(false)
						});
					}
					else
					{
						type = keyValuePair.Value;
					}
					value = SqlProviderServices.CreateSqlParameter(keyValuePair.Key, type, ParameterMode.In, DBNull.Value, false, sqlVersion);
				}
				sqlCommand.Parameters.Add(value);
			}
			if (list != null && 0 < list.Count)
			{
				if (commandTree.CommandTreeKind != DbCommandTreeKind.Delete && commandTree.CommandTreeKind != DbCommandTreeKind.Insert && commandTree.CommandTreeKind != DbCommandTreeKind.Update)
				{
					throw EntityUtil.InternalError(EntityUtil.InternalErrorCode.SqlGenParametersNotPermitted);
				}
				foreach (SqlParameter value2 in list)
				{
					sqlCommand.Parameters.Add(value2);
				}
			}
			return sqlCommand;
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0000DFE0 File Offset: 0x0000C1E0
		protected override void SetDbParameterValue(DbParameter parameter, TypeUsage parameterType, object value)
		{
			value = SqlProviderServices.EnsureSqlParameterValue(value);
			if (TypeSemantics.IsPrimitiveType(parameterType, PrimitiveTypeKind.String) || TypeSemantics.IsPrimitiveType(parameterType, PrimitiveTypeKind.Binary))
			{
				if (SqlProviderServices.GetParameterSize(parameterType, (parameter.Direction & ParameterDirection.Output) == ParameterDirection.Output) != null)
				{
					parameter.Value = value;
					return;
				}
				int size = parameter.Size;
				parameter.Size = 0;
				parameter.Value = value;
				if (size > -1)
				{
					if (parameter.Size < size)
					{
						parameter.Size = size;
						return;
					}
				}
				else
				{
					int nonMaxLength = SqlProviderServices.GetNonMaxLength(((SqlParameter)parameter).SqlDbType);
					if (parameter.Size < nonMaxLength)
					{
						parameter.Size = nonMaxLength;
						return;
					}
					if (parameter.Size > nonMaxLength)
					{
						parameter.Size = -1;
						return;
					}
				}
			}
			else
			{
				parameter.Value = value;
			}
		}

		// Token: 0x060003BA RID: 954 RVA: 0x0000E094 File Offset: 0x0000C294
		protected override string GetDbProviderManifestToken(DbConnection connection)
		{
			EntityUtil.CheckArgumentNull<DbConnection>(connection, "connection");
			SqlConnection requiredSqlConnection = SqlProviderUtilities.GetRequiredSqlConnection(connection);
			if (string.IsNullOrEmpty(requiredSqlConnection.ConnectionString))
			{
				throw EntityUtil.Argument(Strings.UnableToDetermineStoreVersion);
			}
			string providerManifestToken = null;
			try
			{
				SqlProviderServices.UsingConnection(requiredSqlConnection, delegate(SqlConnection conn)
				{
					providerManifestToken = SqlVersionUtils.GetVersionHint(SqlVersionUtils.GetSqlVersion(conn));
				});
			}
			catch
			{
				SqlProviderServices.UsingMasterConnection(requiredSqlConnection, delegate(SqlConnection conn)
				{
					providerManifestToken = SqlVersionUtils.GetVersionHint(SqlVersionUtils.GetSqlVersion(conn));
				});
			}
			return providerManifestToken;
		}

		// Token: 0x060003BB RID: 955 RVA: 0x0000E118 File Offset: 0x0000C318
		protected override DbProviderManifest GetDbProviderManifest(string versionHint)
		{
			if (string.IsNullOrEmpty(versionHint))
			{
				throw EntityUtil.Argument(Strings.UnableToDetermineStoreVersion);
			}
			return new SqlProviderManifest(versionHint);
		}

		// Token: 0x060003BC RID: 956 RVA: 0x0000E134 File Offset: 0x0000C334
		protected override DbSpatialDataReader GetDbSpatialDataReader(DbDataReader fromReader, string versionHint)
		{
			EntityUtil.CheckArgumentNull<DbDataReader>(fromReader, "fromReader");
			this.ValidateVersionHint(versionHint);
			SqlDataReader sqlDataReader = fromReader as SqlDataReader;
			if (sqlDataReader == null)
			{
				throw EntityUtil.ProviderIncompatible(Strings.SqlProvider_NeedSqlDataReader(fromReader.GetType()));
			}
			return new SqlSpatialDataReader(sqlDataReader);
		}

		// Token: 0x060003BD RID: 957 RVA: 0x0000E175 File Offset: 0x0000C375
		protected override DbSpatialServices DbGetSpatialServices(string versionHint)
		{
			this.ValidateVersionHint(versionHint);
			return SqlSpatialServices.Instance;
		}

		// Token: 0x060003BE RID: 958 RVA: 0x0000E184 File Offset: 0x0000C384
		private void ValidateVersionHint(string versionHint)
		{
			if (string.IsNullOrEmpty(versionHint))
			{
				throw EntityUtil.Argument(Strings.UnableToDetermineStoreVersion);
			}
			SqlVersion sqlVersion = SqlVersionUtils.GetSqlVersion(versionHint);
			if (sqlVersion < SqlVersion.Sql10)
			{
				throw EntityUtil.ProviderIncompatible(Strings.SqlProvider_Sql2008RequiredForSpatial);
			}
		}

		// Token: 0x060003BF RID: 959 RVA: 0x0000E1BC File Offset: 0x0000C3BC
		internal static SqlTypesAssembly GetSqlTypesAssembly()
		{
			SqlTypesAssembly result;
			if (!SqlProviderServices.TryGetSqlTypesAssembly(out result))
			{
				throw EntityUtil.SqlTypesAssemblyNotFound();
			}
			return result;
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x0000E1DC File Offset: 0x0000C3DC
		internal static bool SqlTypesAssemblyIsAvailable
		{
			get
			{
				SqlTypesAssembly sqlTypesAssembly;
				return SqlProviderServices.TryGetSqlTypesAssembly(out sqlTypesAssembly);
			}
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x0000E1F0 File Offset: 0x0000C3F0
		private static bool TryGetSqlTypesAssembly(out SqlTypesAssembly sqlTypesAssembly)
		{
			sqlTypesAssembly = SqlTypesAssembly.Latest;
			return sqlTypesAssembly != null;
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x0000E200 File Offset: 0x0000C400
		internal static SqlParameter CreateSqlParameter(string name, TypeUsage type, ParameterMode mode, object value, bool preventTruncation, SqlVersion version)
		{
			value = SqlProviderServices.EnsureSqlParameterValue(value);
			SqlParameter sqlParameter = new SqlParameter(name, value);
			ParameterDirection parameterDirection = MetadataHelper.ParameterModeToParameterDirection(mode);
			if (sqlParameter.Direction != parameterDirection)
			{
				sqlParameter.Direction = parameterDirection;
			}
			bool flag = mode > ParameterMode.In;
			int? num;
			byte? b;
			byte? b2;
			string udtTypeName;
			SqlDbType sqlDbType = SqlProviderServices.GetSqlDbType(type, flag, version, out num, out b, out b2, out udtTypeName);
			if (sqlParameter.SqlDbType != sqlDbType)
			{
				sqlParameter.SqlDbType = sqlDbType;
			}
			if (sqlDbType == SqlDbType.Udt)
			{
				sqlParameter.UdtTypeName = udtTypeName;
			}
			if (num != null)
			{
				if (flag || sqlParameter.Size != num.Value)
				{
					if (preventTruncation && num.Value != -1)
					{
						sqlParameter.Size = Math.Max(sqlParameter.Size, num.Value);
					}
					else
					{
						sqlParameter.Size = num.Value;
					}
				}
			}
			else
			{
				PrimitiveTypeKind primitiveTypeKind = MetadataHelper.GetPrimitiveTypeKind(type);
				if (primitiveTypeKind == PrimitiveTypeKind.String)
				{
					sqlParameter.Size = SqlProviderServices.GetDefaultStringMaxLength(version, sqlDbType);
				}
				else if (primitiveTypeKind == PrimitiveTypeKind.Binary)
				{
					sqlParameter.Size = SqlProviderServices.GetDefaultBinaryMaxLength(version);
				}
			}
			if (b != null && (flag || sqlParameter.Precision != b.Value))
			{
				sqlParameter.Precision = b.Value;
			}
			if (b2 != null && (flag || sqlParameter.Scale != b2.Value))
			{
				sqlParameter.Scale = b2.Value;
			}
			bool flag2 = TypeSemantics.IsNullable(type);
			if (flag || flag2 != sqlParameter.IsNullable)
			{
				sqlParameter.IsNullable = flag2;
			}
			return sqlParameter;
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x0000E378 File Offset: 0x0000C578
		internal static object EnsureSqlParameterValue(object value)
		{
			if (value != null && value != DBNull.Value && Type.GetTypeCode(value.GetType()) == TypeCode.Object)
			{
				DbGeography dbGeography = value as DbGeography;
				if (dbGeography != null)
				{
					value = SqlProviderServices.GetSqlTypesAssembly().ConvertToSqlTypesGeography(dbGeography);
				}
				else
				{
					DbGeometry dbGeometry = value as DbGeometry;
					if (dbGeometry != null)
					{
						value = SqlProviderServices.GetSqlTypesAssembly().ConvertToSqlTypesGeometry(dbGeometry);
					}
				}
			}
			return value;
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x0000E3D0 File Offset: 0x0000C5D0
		private static SqlDbType GetSqlDbType(TypeUsage type, bool isOutParam, SqlVersion version, out int? size, out byte? precision, out byte? scale, out string udtName)
		{
			PrimitiveTypeKind primitiveTypeKind = MetadataHelper.GetPrimitiveTypeKind(type);
			size = null;
			precision = null;
			scale = null;
			udtName = null;
			switch (primitiveTypeKind)
			{
			case PrimitiveTypeKind.Binary:
				size = SqlProviderServices.GetParameterSize(type, isOutParam);
				return SqlProviderServices.GetBinaryDbType(type);
			case PrimitiveTypeKind.Boolean:
				return SqlDbType.Bit;
			case PrimitiveTypeKind.Byte:
				return SqlDbType.TinyInt;
			case PrimitiveTypeKind.DateTime:
				if (!SqlVersionUtils.IsPreKatmai(version))
				{
					precision = SqlProviderServices.GetKatmaiDateTimePrecision(type, isOutParam);
					return SqlDbType.DateTime2;
				}
				return SqlDbType.DateTime;
			case PrimitiveTypeKind.Decimal:
				precision = SqlProviderServices.GetParameterPrecision(type, null);
				scale = SqlProviderServices.GetScale(type);
				return SqlDbType.Decimal;
			case PrimitiveTypeKind.Double:
				return SqlDbType.Float;
			case PrimitiveTypeKind.Guid:
				return SqlDbType.UniqueIdentifier;
			case PrimitiveTypeKind.Single:
				return SqlDbType.Real;
			case PrimitiveTypeKind.SByte:
				return SqlDbType.SmallInt;
			case PrimitiveTypeKind.Int16:
				return SqlDbType.SmallInt;
			case PrimitiveTypeKind.Int32:
				return SqlDbType.Int;
			case PrimitiveTypeKind.Int64:
				return SqlDbType.BigInt;
			case PrimitiveTypeKind.String:
				size = SqlProviderServices.GetParameterSize(type, isOutParam);
				return SqlProviderServices.GetStringDbType(type);
			case PrimitiveTypeKind.Time:
				if (!SqlVersionUtils.IsPreKatmai(version))
				{
					precision = SqlProviderServices.GetKatmaiDateTimePrecision(type, isOutParam);
				}
				return SqlDbType.Time;
			case PrimitiveTypeKind.DateTimeOffset:
				if (!SqlVersionUtils.IsPreKatmai(version))
				{
					precision = SqlProviderServices.GetKatmaiDateTimePrecision(type, isOutParam);
				}
				return SqlDbType.DateTimeOffset;
			case PrimitiveTypeKind.Geometry:
				udtName = "geometry";
				return SqlDbType.Udt;
			case PrimitiveTypeKind.Geography:
				udtName = "geography";
				return SqlDbType.Udt;
			default:
				return SqlDbType.Variant;
			}
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0000E518 File Offset: 0x0000C718
		private static int? GetParameterSize(TypeUsage type, bool isOutParam)
		{
			Facet facet;
			if (type.Facets.TryGetValue("MaxLength", false, out facet) && facet.Value != null)
			{
				if (facet.IsUnbounded)
				{
					return new int?(-1);
				}
				return (int?)facet.Value;
			}
			else
			{
				if (isOutParam)
				{
					return new int?(-1);
				}
				return null;
			}
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0000E570 File Offset: 0x0000C770
		private static int GetNonMaxLength(SqlDbType type)
		{
			int result = -1;
			if (type == SqlDbType.NChar || type == SqlDbType.NVarChar)
			{
				result = 4000;
			}
			else if (type == SqlDbType.Char || type == SqlDbType.VarChar || type == SqlDbType.Binary || type == SqlDbType.VarBinary)
			{
				result = 8000;
			}
			return result;
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x0000E5AC File Offset: 0x0000C7AC
		private static int GetDefaultStringMaxLength(SqlVersion version, SqlDbType type)
		{
			int result;
			if (version < SqlVersion.Sql9)
			{
				if (type == SqlDbType.NChar || type == SqlDbType.NVarChar)
				{
					result = 4000;
				}
				else
				{
					result = 8000;
				}
			}
			else
			{
				result = -1;
			}
			return result;
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x0000E5DC File Offset: 0x0000C7DC
		private static int GetDefaultBinaryMaxLength(SqlVersion version)
		{
			int result;
			if (version < SqlVersion.Sql9)
			{
				result = 8000;
			}
			else
			{
				result = -1;
			}
			return result;
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x0000E5FC File Offset: 0x0000C7FC
		private static byte? GetKatmaiDateTimePrecision(TypeUsage type, bool isOutParam)
		{
			byte? defaultIfUndefined = isOutParam ? new byte?(7) : null;
			return SqlProviderServices.GetParameterPrecision(type, defaultIfUndefined);
		}

		// Token: 0x060003CA RID: 970 RVA: 0x0000E628 File Offset: 0x0000C828
		private static byte? GetParameterPrecision(TypeUsage type, byte? defaultIfUndefined)
		{
			byte value;
			if (TypeHelpers.TryGetPrecision(type, out value))
			{
				return new byte?(value);
			}
			return defaultIfUndefined;
		}

		// Token: 0x060003CB RID: 971 RVA: 0x0000E648 File Offset: 0x0000C848
		private static byte? GetScale(TypeUsage type)
		{
			byte value;
			if (TypeHelpers.TryGetScale(type, out value))
			{
				return new byte?(value);
			}
			return null;
		}

		// Token: 0x060003CC RID: 972 RVA: 0x0000E670 File Offset: 0x0000C870
		private static SqlDbType GetStringDbType(TypeUsage type)
		{
			SqlDbType result;
			if (type.EdmType.Name.ToLowerInvariant() == "xml")
			{
				result = SqlDbType.Xml;
			}
			else
			{
				bool flag;
				if (!TypeHelpers.TryGetIsFixedLength(type, out flag))
				{
					flag = false;
				}
				bool flag2;
				if (!TypeHelpers.TryGetIsUnicode(type, out flag2))
				{
					flag2 = true;
				}
				if (flag)
				{
					result = (flag2 ? SqlDbType.NChar : SqlDbType.Char);
				}
				else
				{
					result = (flag2 ? SqlDbType.NVarChar : SqlDbType.VarChar);
				}
			}
			return result;
		}

		// Token: 0x060003CD RID: 973 RVA: 0x0000E6D0 File Offset: 0x0000C8D0
		private static SqlDbType GetBinaryDbType(TypeUsage type)
		{
			bool flag;
			if (!TypeHelpers.TryGetIsFixedLength(type, out flag))
			{
				flag = false;
			}
			if (!flag)
			{
				return SqlDbType.VarBinary;
			}
			return SqlDbType.Binary;
		}

		// Token: 0x060003CE RID: 974 RVA: 0x0000E6F0 File Offset: 0x0000C8F0
		protected override string DbCreateDatabaseScript(string providerManifestToken, StoreItemCollection storeItemCollection)
		{
			EntityUtil.CheckArgumentNull<string>(providerManifestToken, "providerManifestToken");
			EntityUtil.CheckArgumentNull<StoreItemCollection>(storeItemCollection, "storeItemCollection");
			SqlVersion sqlVersion = SqlVersionUtils.GetSqlVersion(providerManifestToken);
			return SqlProviderServices.CreateObjectsScript(sqlVersion, storeItemCollection);
		}

		// Token: 0x060003CF RID: 975 RVA: 0x0000E724 File Offset: 0x0000C924
		protected override void DbCreateDatabase(DbConnection connection, int? commandTimeout, StoreItemCollection storeItemCollection)
		{
			EntityUtil.CheckArgumentNull<DbConnection>(connection, "connection");
			EntityUtil.CheckArgumentNull<StoreItemCollection>(storeItemCollection, "storeItemCollection");
			SqlConnection requiredSqlConnection = SqlProviderUtilities.GetRequiredSqlConnection(connection);
			string databaseName;
			string dataFileName;
			string logFileName;
			SqlProviderServices.GetOrGenerateDatabaseNameAndGetFileNames(requiredSqlConnection, out databaseName, out dataFileName, out logFileName);
			string createDatabaseScript = SqlDdlBuilder.CreateDatabaseScript(databaseName, dataFileName, logFileName);
			SqlVersion sqlVersion = SqlProviderServices.GetSqlVersion(storeItemCollection);
			string createObjectsScript = SqlProviderServices.CreateObjectsScript(sqlVersion, storeItemCollection);
			SqlProviderServices.UsingMasterConnection(requiredSqlConnection, delegate(SqlConnection conn)
			{
				SqlProviderServices.CreateCommand(conn, createDatabaseScript, commandTimeout).ExecuteNonQuery();
			});
			try
			{
				SqlConnection.ClearPool(requiredSqlConnection);
				SqlProviderServices.UsingConnection(requiredSqlConnection, delegate(SqlConnection conn)
				{
					SqlProviderServices.CreateCommand(conn, createObjectsScript, commandTimeout).ExecuteNonQuery();
				});
			}
			catch (Exception ex)
			{
				if (EntityUtil.IsCatchableExceptionType(ex))
				{
					try
					{
						SqlProviderServices.DropDatabase(requiredSqlConnection, commandTimeout, databaseName);
					}
					catch (Exception ex2)
					{
						if (EntityUtil.IsCatchableExceptionType(ex2))
						{
							throw new InvalidOperationException(Strings.SqlProvider_IncompleteCreateDatabase, new AggregateException(Strings.SqlProvider_IncompleteCreateDatabaseAggregate, new Exception[]
							{
								ex,
								ex2
							}));
						}
						throw;
					}
					throw;
				}
				throw;
			}
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x0000E828 File Offset: 0x0000CA28
		private static SqlVersion GetSqlVersion(StoreItemCollection storeItemCollection)
		{
			SqlProviderManifest sqlProviderManifest = storeItemCollection.StoreProviderManifest as SqlProviderManifest;
			if (sqlProviderManifest == null)
			{
				throw EntityUtil.Argument(Strings.Mapping_Provider_WrongManifestType(typeof(SqlProviderManifest)));
			}
			return sqlProviderManifest.SqlVersion;
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x0000E864 File Offset: 0x0000CA64
		private static void GetOrGenerateDatabaseNameAndGetFileNames(SqlConnection sqlConnection, out string databaseName, out string dataFileName, out string logFileName)
		{
			SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder(sqlConnection.ConnectionString);
			string attachDBFilename = sqlConnectionStringBuilder.AttachDBFilename;
			if (string.IsNullOrEmpty(attachDBFilename))
			{
				dataFileName = null;
				logFileName = null;
			}
			else
			{
				dataFileName = SqlProviderServices.GetMdfFileName(attachDBFilename);
				logFileName = SqlProviderServices.GetLdfFileName(dataFileName);
			}
			if (!string.IsNullOrEmpty(sqlConnectionStringBuilder.InitialCatalog))
			{
				databaseName = sqlConnectionStringBuilder.InitialCatalog;
				return;
			}
			if (dataFileName != null)
			{
				databaseName = SqlProviderServices.GenerateDatabaseName(dataFileName);
				return;
			}
			throw EntityUtil.InvalidOperation(Strings.SqlProvider_DdlGeneration_MissingInitialCatalog);
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x0000E8D4 File Offset: 0x0000CAD4
		private static string GetLdfFileName(string dataFileName)
		{
			DirectoryInfo directory = new FileInfo(dataFileName).Directory;
			return Path.Combine(directory.FullName, Path.GetFileNameWithoutExtension(dataFileName) + "_log.ldf");
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x0000E90C File Offset: 0x0000CB0C
		private static string GenerateDatabaseName(string mdfFileName)
		{
			string path = mdfFileName.ToUpper(CultureInfo.InvariantCulture);
			char[] array = Path.GetFileNameWithoutExtension(path).ToCharArray();
			for (int i = 0; i < array.Length; i++)
			{
				if (!char.IsLetterOrDigit(array[i]))
				{
					array[i] = '_';
				}
			}
			string text = new string(array);
			text = ((text.Length > 30) ? text.Substring(0, 30) : text);
			return string.Format(CultureInfo.InvariantCulture, "{0}_{1}", new object[]
			{
				text,
				Guid.NewGuid().ToString("N", CultureInfo.InvariantCulture)
			});
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0000E9AC File Offset: 0x0000CBAC
		private static string GetMdfFileName(string attachDBFile)
		{
			string text = DbConnectionOptions.ExpandDataDirectory("AttachDBFilename", attachDBFile);
			return text ?? attachDBFile;
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x0000E9D0 File Offset: 0x0000CBD0
		protected override bool DbDatabaseExists(DbConnection connection, int? commandTimeout, StoreItemCollection storeItemCollection)
		{
			EntityUtil.CheckArgumentNull<DbConnection>(connection, "connection");
			EntityUtil.CheckArgumentNull<StoreItemCollection>(storeItemCollection, "storeItemCollection");
			SqlConnection requiredSqlConnection = SqlProviderUtilities.GetRequiredSqlConnection(connection);
			SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder(requiredSqlConnection.ConnectionString);
			if (string.IsNullOrEmpty(sqlConnectionStringBuilder.InitialCatalog) && string.IsNullOrEmpty(sqlConnectionStringBuilder.AttachDBFilename))
			{
				throw EntityUtil.InvalidOperation(Strings.SqlProvider_DdlGeneration_MissingInitialCatalog);
			}
			if (!string.IsNullOrEmpty(sqlConnectionStringBuilder.InitialCatalog) && SqlProviderServices.CheckDatabaseExists(requiredSqlConnection, commandTimeout, sqlConnectionStringBuilder.InitialCatalog))
			{
				return true;
			}
			if (!string.IsNullOrEmpty(sqlConnectionStringBuilder.AttachDBFilename))
			{
				try
				{
					SqlProviderServices.UsingConnection(requiredSqlConnection, delegate(SqlConnection con)
					{
					});
					return true;
				}
				catch (SqlException inner)
				{
					if (!string.IsNullOrEmpty(sqlConnectionStringBuilder.InitialCatalog))
					{
						return SqlProviderServices.CheckDatabaseExists(requiredSqlConnection, commandTimeout, sqlConnectionStringBuilder.InitialCatalog);
					}
					string fileName = SqlProviderServices.GetMdfFileName(sqlConnectionStringBuilder.AttachDBFilename);
					bool databaseDoesNotExistInSysTables = false;
					SqlProviderServices.UsingMasterConnection(requiredSqlConnection, delegate(SqlConnection conn)
					{
						SqlVersion sqlVersion = SqlVersionUtils.GetSqlVersion(conn);
						string commandText = SqlDdlBuilder.CreateCountDatabasesBasedOnFileNameScript(fileName, sqlVersion == SqlVersion.Sql8);
						int num = (int)SqlProviderServices.CreateCommand(conn, commandText, commandTimeout).ExecuteScalar();
						databaseDoesNotExistInSysTables = (num == 0);
					});
					if (databaseDoesNotExistInSysTables)
					{
						return false;
					}
					throw EntityUtil.InvalidOperation(Strings.SqlProvider_DdlGeneration_CannotTellIfDatabaseExists, inner);
				}
				return false;
			}
			return false;
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x0000EB10 File Offset: 0x0000CD10
		private static bool CheckDatabaseExists(SqlConnection sqlConnection, int? commandTimeout, string databaseName)
		{
			bool databaseExistsInSysTables = false;
			SqlProviderServices.UsingMasterConnection(sqlConnection, delegate(SqlConnection conn)
			{
				SqlVersion sqlVersion = SqlVersionUtils.GetSqlVersion(conn);
				string commandText = SqlDdlBuilder.CreateDatabaseExistsScript(databaseName, sqlVersion == SqlVersion.Sql8);
				int num = (int)SqlProviderServices.CreateCommand(conn, commandText, commandTimeout).ExecuteScalar();
				databaseExistsInSysTables = (num > 0);
			});
			return databaseExistsInSysTables;
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x0000EB50 File Offset: 0x0000CD50
		protected override void DbDeleteDatabase(DbConnection connection, int? commandTimeout, StoreItemCollection storeItemCollection)
		{
			EntityUtil.CheckArgumentNull<DbConnection>(connection, "connection");
			EntityUtil.CheckArgumentNull<StoreItemCollection>(storeItemCollection, "storeItemCollection");
			SqlConnection requiredSqlConnection = SqlProviderUtilities.GetRequiredSqlConnection(connection);
			SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder(requiredSqlConnection.ConnectionString);
			string initialCatalog = sqlConnectionStringBuilder.InitialCatalog;
			string attachDBFilename = sqlConnectionStringBuilder.AttachDBFilename;
			if (!string.IsNullOrEmpty(initialCatalog))
			{
				SqlProviderServices.DropDatabase(requiredSqlConnection, commandTimeout, initialCatalog);
				return;
			}
			if (!string.IsNullOrEmpty(attachDBFilename))
			{
				string fullFileName = SqlProviderServices.GetMdfFileName(attachDBFilename);
				List<string> databaseNames = new List<string>();
				SqlProviderServices.UsingMasterConnection(requiredSqlConnection, delegate(SqlConnection conn)
				{
					SqlVersion sqlVersion = SqlVersionUtils.GetSqlVersion(conn);
					string commandText = SqlDdlBuilder.CreateGetDatabaseNamesBasedOnFileNameScript(fullFileName, sqlVersion == SqlVersion.Sql8);
					SqlCommand sqlCommand = SqlProviderServices.CreateCommand(conn, commandText, commandTimeout);
					using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
					{
						while (sqlDataReader.Read())
						{
							databaseNames.Add(sqlDataReader.GetString(0));
						}
					}
				});
				if (databaseNames.Count > 0)
				{
					using (List<string>.Enumerator enumerator = databaseNames.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							string databaseName = enumerator.Current;
							SqlProviderServices.DropDatabase(requiredSqlConnection, commandTimeout, databaseName);
						}
						return;
					}
				}
				throw EntityUtil.InvalidOperation(Strings.SqlProvider_DdlGeneration_CannotDeleteDatabaseNoInitialCatalog);
			}
			throw EntityUtil.InvalidOperation(Strings.SqlProvider_DdlGeneration_MissingInitialCatalog);
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x0000EC64 File Offset: 0x0000CE64
		private static void DropDatabase(SqlConnection sqlConnection, int? commandTimeout, string databaseName)
		{
			SqlConnection.ClearPool(sqlConnection);
			string dropDatabaseScript = SqlDdlBuilder.DropDatabaseScript(databaseName);
			SqlProviderServices.UsingMasterConnection(sqlConnection, delegate(SqlConnection conn)
			{
				SqlProviderServices.CreateCommand(conn, dropDatabaseScript, commandTimeout).ExecuteNonQuery();
			});
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x0000ECA2 File Offset: 0x0000CEA2
		private static string CreateObjectsScript(SqlVersion version, StoreItemCollection storeItemCollection)
		{
			return SqlDdlBuilder.CreateObjectsScript(storeItemCollection, version != SqlVersion.Sql8);
		}

		// Token: 0x060003DA RID: 986 RVA: 0x0000ECB4 File Offset: 0x0000CEB4
		private static SqlCommand CreateCommand(SqlConnection sqlConnection, string commandText, int? commandTimeout)
		{
			if (string.IsNullOrEmpty(commandText))
			{
				commandText = Environment.NewLine;
			}
			SqlCommand sqlCommand = new SqlCommand(commandText, sqlConnection);
			if (commandTimeout != null)
			{
				sqlCommand.CommandTimeout = commandTimeout.Value;
			}
			return sqlCommand;
		}

		// Token: 0x060003DB RID: 987 RVA: 0x0000ECF0 File Offset: 0x0000CEF0
		private static void UsingConnection(SqlConnection sqlConnection, Action<SqlConnection> act)
		{
			string connectionString = sqlConnection.ConnectionString;
			bool flag = sqlConnection.State == ConnectionState.Closed;
			if (flag)
			{
				sqlConnection.Open();
			}
			try
			{
				act(sqlConnection);
			}
			finally
			{
				if (flag && sqlConnection.State == ConnectionState.Open)
				{
					sqlConnection.Close();
				}
				if (sqlConnection.ConnectionString != connectionString)
				{
					sqlConnection.ConnectionString = connectionString;
				}
			}
		}

		// Token: 0x060003DC RID: 988 RVA: 0x0000ED5C File Offset: 0x0000CF5C
		private static void UsingMasterConnection(SqlConnection sqlConnection, Action<SqlConnection> act)
		{
			SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder(sqlConnection.ConnectionString)
			{
				InitialCatalog = "master",
				AttachDBFilename = string.Empty
			};
			try
			{
				using (SqlConnection sqlConnection2 = new SqlConnection(sqlConnectionStringBuilder.ConnectionString))
				{
					SqlProviderServices.UsingConnection(sqlConnection2, act);
				}
			}
			catch (SqlException innerException)
			{
				if (!sqlConnectionStringBuilder.IntegratedSecurity && (string.IsNullOrEmpty(sqlConnectionStringBuilder.UserID) || string.IsNullOrEmpty(sqlConnectionStringBuilder.Password)))
				{
					throw new InvalidOperationException(Strings.SqlProvider_CredentialsMissingForMasterConnection, innerException);
				}
				throw;
			}
		}

		// Token: 0x040006EE RID: 1774
		internal static readonly SqlProviderServices Instance = new SqlProviderServices();
	}
}
