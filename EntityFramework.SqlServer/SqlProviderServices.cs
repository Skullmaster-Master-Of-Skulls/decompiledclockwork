using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.Migrations.Sql;
using System.Data.Entity.Spatial;
using System.Data.Entity.SqlServer.Resources;
using System.Data.Entity.SqlServer.SqlGen;
using System.Data.Entity.SqlServer.Utilities;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;

namespace System.Data.Entity.SqlServer
{
	// Token: 0x02000043 RID: 67
	[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
	public sealed class SqlProviderServices : DbProviderServices
	{
		// Token: 0x0600045E RID: 1118 RVA: 0x00015960 File Offset: 0x00013B60
		private SqlProviderServices()
		{
			base.AddDependencyResolver(new SingletonDependencyResolver<IDbConnectionFactory>(new SqlConnectionFactory()));
			base.AddDependencyResolver(new ExecutionStrategyResolver<DefaultSqlExecutionStrategy>("System.Data.SqlClient", null, () => new DefaultSqlExecutionStrategy()));
			base.AddDependencyResolver(new SingletonDependencyResolver<Func<MigrationSqlGenerator>>(() => new SqlServerMigrationSqlGenerator(), "System.Data.SqlClient"));
			base.AddDependencyResolver(new SingletonDependencyResolver<TableExistenceChecker>(new SqlTableExistenceChecker(), "System.Data.SqlClient"));
			base.AddDependencyResolver(new SingletonDependencyResolver<DbSpatialServices>(SqlSpatialServices.Instance, delegate(object k)
			{
				if (k == null)
				{
					return true;
				}
				DbProviderInfo dbProviderInfo = k as DbProviderInfo;
				return dbProviderInfo != null && dbProviderInfo.ProviderInvariantName == "System.Data.SqlClient" && SqlProviderServices.SupportsSpatial(dbProviderInfo.ProviderManifestToken);
			}));
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600045F RID: 1119 RVA: 0x00015A2B File Offset: 0x00013C2B
		public static SqlProviderServices Instance
		{
			get
			{
				return SqlProviderServices._providerInstance;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000460 RID: 1120 RVA: 0x00015A32 File Offset: 0x00013C32
		// (set) Token: 0x06000461 RID: 1121 RVA: 0x00015A39 File Offset: 0x00013C39
		public static string SqlServerTypesAssemblyName { get; set; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000462 RID: 1122 RVA: 0x00015A41 File Offset: 0x00013C41
		// (set) Token: 0x06000463 RID: 1123 RVA: 0x00015A48 File Offset: 0x00013C48
		public static bool TruncateDecimalsToScale
		{
			get
			{
				return SqlProviderServices._truncateDecimalsToScale;
			}
			set
			{
				SqlProviderServices._truncateDecimalsToScale = value;
			}
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x00015A78 File Offset: 0x00013C78
		public override void RegisterInfoMessageHandler(DbConnection connection, Action<string> handler)
		{
			Check.NotNull<DbConnection>(connection, "connection");
			Check.NotNull<Action<string>>(handler, "handler");
			SqlConnection sqlConnection = connection as SqlConnection;
			if (sqlConnection == null)
			{
				throw new ArgumentException(Strings.Mapping_Provider_WrongConnectionType(typeof(SqlConnection)));
			}
			sqlConnection.InfoMessage += delegate(object _, SqlInfoMessageEventArgs e)
			{
				if (!string.IsNullOrWhiteSpace(e.Message))
				{
					handler(e.Message);
				}
			};
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x00015AE0 File Offset: 0x00013CE0
		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
		protected override DbCommandDefinition CreateDbCommandDefinition(DbProviderManifest providerManifest, DbCommandTree commandTree)
		{
			Check.NotNull<DbProviderManifest>(providerManifest, "providerManifest");
			Check.NotNull<DbCommandTree>(commandTree, "commandTree");
			DbCommand prototype = SqlProviderServices.CreateCommand(providerManifest, commandTree);
			return this.CreateCommandDefinition(prototype);
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x00015B18 File Offset: 0x00013D18
		[SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities", Justification = "Not changing the CommandText at all - simply providing a clone of the DbCommand with the same CommandText")]
		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
		protected override DbCommand CloneDbCommand(DbCommand fromDbCommand)
		{
			Check.NotNull<DbCommand>(fromDbCommand, "fromDbCommand");
			SqlCommand sqlCommand = fromDbCommand as SqlCommand;
			if (sqlCommand == null)
			{
				return base.CloneDbCommand(fromDbCommand);
			}
			SqlCommand sqlCommand2 = new SqlCommand();
			sqlCommand2.CommandText = sqlCommand.CommandText;
			sqlCommand2.CommandTimeout = sqlCommand.CommandTimeout;
			sqlCommand2.CommandType = sqlCommand.CommandType;
			sqlCommand2.Connection = sqlCommand.Connection;
			sqlCommand2.Transaction = sqlCommand.Transaction;
			sqlCommand2.UpdatedRowSource = sqlCommand.UpdatedRowSource;
			foreach (object obj in sqlCommand.Parameters)
			{
				ICloneable cloneable = obj as ICloneable;
				sqlCommand2.Parameters.Add((cloneable == null) ? obj : cloneable.Clone());
			}
			return sqlCommand2;
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x00015BF8 File Offset: 0x00013DF8
		[SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
		[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
		private static DbCommand CreateCommand(DbProviderManifest providerManifest, DbCommandTree commandTree)
		{
			SqlProviderManifest sqlProviderManifest = providerManifest as SqlProviderManifest;
			if (sqlProviderManifest == null)
			{
				throw new ArgumentException(Strings.Mapping_Provider_WrongManifestType(typeof(SqlProviderManifest)));
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
					TypeUsage type = (hashSet != null && hashSet.Contains(keyValuePair.Key)) ? keyValuePair.Value.ForceNonUnicode() : keyValuePair.Value;
					value = SqlProviderServices.CreateSqlParameter(keyValuePair.Key, type, ParameterMode.In, DBNull.Value, false, sqlVersion);
				}
				sqlCommand.Parameters.Add(value);
			}
			if (list != null && 0 < list.Count)
			{
				if (commandTree.CommandTreeKind != DbCommandTreeKind.Delete && commandTree.CommandTreeKind != DbCommandTreeKind.Insert && commandTree.CommandTreeKind != DbCommandTreeKind.Update)
				{
					throw new InvalidOperationException(Strings.ADP_InternalProviderError(1017));
				}
				foreach (SqlParameter value2 in list)
				{
					sqlCommand.Parameters.Add(value2);
				}
			}
			return sqlCommand;
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x00015DC0 File Offset: 0x00013FC0
		protected override void SetDbParameterValue(DbParameter parameter, TypeUsage parameterType, object value)
		{
			Check.NotNull<DbParameter>(parameter, "parameter");
			Check.NotNull<TypeUsage>(parameterType, "parameterType");
			value = SqlProviderServices.EnsureSqlParameterValue(value);
			if (parameterType.IsPrimitiveType(PrimitiveTypeKind.String) || parameterType.IsPrimitiveType(PrimitiveTypeKind.Binary))
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

		// Token: 0x06000469 RID: 1129 RVA: 0x00015EB0 File Offset: 0x000140B0
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
		protected override string GetDbProviderManifestToken(DbConnection connection)
		{
			Check.NotNull<DbConnection>(connection, "connection");
			if (string.IsNullOrEmpty(DbInterception.Dispatch.Connection.GetConnectionString(connection, new DbInterceptionContext())))
			{
				throw new ArgumentException(Strings.UnableToDetermineStoreVersion);
			}
			string providerManifestToken = null;
			try
			{
				SqlProviderServices.UsingConnection(connection, delegate(DbConnection conn)
				{
					providerManifestToken = SqlProviderServices.QueryForManifestToken(conn);
				});
				return providerManifestToken;
			}
			catch
			{
			}
			try
			{
				SqlProviderServices.UsingMasterConnection(connection, delegate(DbConnection conn)
				{
					providerManifestToken = SqlProviderServices.QueryForManifestToken(conn);
				});
				return providerManifestToken;
			}
			catch
			{
			}
			return "2008";
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x00015F6C File Offset: 0x0001416C
		private static string QueryForManifestToken(DbConnection conn)
		{
			SqlVersion sqlVersion = SqlVersionUtils.GetSqlVersion(conn);
			ServerType serverType = (sqlVersion >= SqlVersion.Sql11) ? SqlVersionUtils.GetServerType(conn) : ServerType.OnPremises;
			return SqlVersionUtils.GetVersionHint(sqlVersion, serverType);
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x00015F9E File Offset: 0x0001419E
		protected override DbProviderManifest GetDbProviderManifest(string versionHint)
		{
			if (string.IsNullOrEmpty(versionHint))
			{
				throw new ArgumentException(Strings.UnableToDetermineStoreVersion);
			}
			return this._providerManifests.GetOrAdd(versionHint, (string s) => new SqlProviderManifest(s));
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x00015FDC File Offset: 0x000141DC
		protected override DbSpatialDataReader GetDbSpatialDataReader(DbDataReader fromReader, string versionHint)
		{
			SqlDataReader sqlDataReader = fromReader as SqlDataReader;
			if (sqlDataReader == null)
			{
				throw new ProviderIncompatibleException(Strings.SqlProvider_NeedSqlDataReader(fromReader.GetType()));
			}
			if (!SqlProviderServices.SupportsSpatial(versionHint))
			{
				return null;
			}
			return new SqlSpatialDataReader(base.GetSpatialServices(new DbProviderInfo("System.Data.SqlClient", versionHint)), new SqlDataReaderWrapper(sqlDataReader));
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x0001602A File Offset: 0x0001422A
		[Obsolete("Return DbSpatialServices from the GetService method. See http://go.microsoft.com/fwlink/?LinkId=260882 for more information.")]
		protected override DbSpatialServices DbGetSpatialServices(string versionHint)
		{
			if (!SqlProviderServices.SupportsSpatial(versionHint))
			{
				return null;
			}
			return SqlSpatialServices.Instance;
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x0001603C File Offset: 0x0001423C
		private static bool SupportsSpatial(string versionHint)
		{
			if (string.IsNullOrEmpty(versionHint))
			{
				throw new ArgumentException(Strings.UnableToDetermineStoreVersion);
			}
			SqlVersion sqlVersion = SqlVersionUtils.GetSqlVersion(versionHint);
			return sqlVersion >= SqlVersion.Sql10;
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x0001606C File Offset: 0x0001426C
		internal static SqlParameter CreateSqlParameter(string name, TypeUsage type, ParameterMode mode, object value, bool preventTruncation, SqlVersion version)
		{
			value = SqlProviderServices.EnsureSqlParameterValue(value);
			SqlParameter sqlParameter = new SqlParameter(name, value);
			ParameterDirection parameterDirection = SqlProviderServices.ParameterModeToParameterDirection(mode);
			if (sqlParameter.Direction != parameterDirection)
			{
				sqlParameter.Direction = parameterDirection;
			}
			bool flag = mode != ParameterMode.In;
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
				PrimitiveTypeKind primitiveTypeKind = ((PrimitiveType)type.EdmType).PrimitiveTypeKind;
				if (primitiveTypeKind == PrimitiveTypeKind.String)
				{
					sqlParameter.Size = SqlProviderServices.GetDefaultStringMaxLength(version, sqlDbType);
				}
				else if (primitiveTypeKind == PrimitiveTypeKind.Binary)
				{
					sqlParameter.Size = SqlProviderServices.GetDefaultBinaryMaxLength(version);
				}
			}
			if (b != null && (flag || (sqlParameter.Precision != b.Value && SqlProviderServices._truncateDecimalsToScale)))
			{
				sqlParameter.Precision = b.Value;
			}
			if (b2 != null && (flag || (sqlParameter.Scale != b2.Value && SqlProviderServices._truncateDecimalsToScale)))
			{
				sqlParameter.Scale = b2.Value;
			}
			bool flag2 = type.IsNullable();
			if (flag || flag2 != sqlParameter.IsNullable)
			{
				sqlParameter.IsNullable = flag2;
			}
			return sqlParameter;
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x00016200 File Offset: 0x00014400
		private static ParameterDirection ParameterModeToParameterDirection(ParameterMode mode)
		{
			switch (mode)
			{
			case ParameterMode.In:
				return ParameterDirection.Input;
			case ParameterMode.Out:
				return ParameterDirection.Output;
			case ParameterMode.InOut:
				return ParameterDirection.InputOutput;
			case ParameterMode.ReturnValue:
				return ParameterDirection.ReturnValue;
			default:
				return (ParameterDirection)0;
			}
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x00016230 File Offset: 0x00014430
		internal static object EnsureSqlParameterValue(object value)
		{
			if (value != null && value != DBNull.Value && value.GetType().IsClass())
			{
				DbGeography dbGeography = value as DbGeography;
				if (dbGeography != null)
				{
					value = SqlTypesAssemblyLoader.DefaultInstance.GetSqlTypesAssembly().ConvertToSqlTypesGeography(dbGeography);
				}
				else
				{
					DbGeometry dbGeometry = value as DbGeometry;
					if (dbGeometry != null)
					{
						value = SqlTypesAssemblyLoader.DefaultInstance.GetSqlTypesAssembly().ConvertToSqlTypesGeometry(dbGeometry);
					}
				}
			}
			return value;
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x00016290 File Offset: 0x00014490
		private static SqlDbType GetSqlDbType(TypeUsage type, bool isOutParam, SqlVersion version, out int? size, out byte? precision, out byte? scale, out string udtName)
		{
			PrimitiveTypeKind primitiveTypeKind = ((PrimitiveType)type.EdmType).PrimitiveTypeKind;
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

		// Token: 0x06000473 RID: 1139 RVA: 0x000163E4 File Offset: 0x000145E4
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

		// Token: 0x06000474 RID: 1140 RVA: 0x0001643C File Offset: 0x0001463C
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

		// Token: 0x06000475 RID: 1141 RVA: 0x00016478 File Offset: 0x00014678
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

		// Token: 0x06000476 RID: 1142 RVA: 0x000164A8 File Offset: 0x000146A8
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

		// Token: 0x06000477 RID: 1143 RVA: 0x000164C8 File Offset: 0x000146C8
		private static byte? GetKatmaiDateTimePrecision(TypeUsage type, bool isOutParam)
		{
			byte? defaultIfUndefined = isOutParam ? new byte?(7) : null;
			return SqlProviderServices.GetParameterPrecision(type, defaultIfUndefined);
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x000164F4 File Offset: 0x000146F4
		private static byte? GetParameterPrecision(TypeUsage type, byte? defaultIfUndefined)
		{
			byte value;
			if (type.TryGetPrecision(out value))
			{
				return new byte?(value);
			}
			return defaultIfUndefined;
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x00016514 File Offset: 0x00014714
		private static byte? GetScale(TypeUsage type)
		{
			byte value;
			if (type.TryGetScale(out value))
			{
				return new byte?(value);
			}
			return null;
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x0001653C File Offset: 0x0001473C
		[SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase")]
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
				if (!type.TryGetIsUnicode(out flag))
				{
					flag = true;
				}
				if (type.IsFixedLength())
				{
					result = (flag ? SqlDbType.NChar : SqlDbType.Char);
				}
				else
				{
					result = (flag ? SqlDbType.NVarChar : SqlDbType.VarChar);
				}
			}
			return result;
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x00016594 File Offset: 0x00014794
		private static SqlDbType GetBinaryDbType(TypeUsage type)
		{
			if (!type.IsFixedLength())
			{
				return SqlDbType.VarBinary;
			}
			return SqlDbType.Binary;
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x000165A4 File Offset: 0x000147A4
		protected override string DbCreateDatabaseScript(string providerManifestToken, StoreItemCollection storeItemCollection)
		{
			Check.NotNull<string>(providerManifestToken, "providerManifestToken");
			Check.NotNull<StoreItemCollection>(storeItemCollection, "storeItemCollection");
			SqlVersion sqlVersion = SqlVersionUtils.GetSqlVersion(providerManifestToken);
			return SqlProviderServices.CreateObjectsScript(sqlVersion, storeItemCollection);
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x00016698 File Offset: 0x00014898
		protected override void DbCreateDatabase(DbConnection connection, int? commandTimeout, StoreItemCollection storeItemCollection)
		{
			Check.NotNull<DbConnection>(connection, "connection");
			Check.NotNull<StoreItemCollection>(storeItemCollection, "storeItemCollection");
			SqlConnection requiredSqlConnection = SqlProviderUtilities.GetRequiredSqlConnection(connection);
			string databaseName;
			string dataFileName;
			string logFileName;
			SqlProviderServices.GetOrGenerateDatabaseNameAndGetFileNames(requiredSqlConnection, out databaseName, out dataFileName, out logFileName);
			string createDatabaseScript = SqlDdlBuilder.CreateDatabaseScript(databaseName, dataFileName, logFileName);
			SqlVersion sqlVersion = SqlProviderServices.CreateDatabaseFromScript(commandTimeout, requiredSqlConnection, createDatabaseScript);
			try
			{
				SqlConnection.ClearPool(requiredSqlConnection);
				string setDatabaseOptionsScript = SqlDdlBuilder.SetDatabaseOptionsScript(sqlVersion, databaseName);
				if (!string.IsNullOrEmpty(setDatabaseOptionsScript))
				{
					SqlProviderServices.UsingMasterConnection(requiredSqlConnection, delegate(DbConnection conn)
					{
						using (DbCommand dbCommand = SqlProviderServices.CreateCommand(conn, setDatabaseOptionsScript, commandTimeout))
						{
							DbInterception.Dispatch.Command.NonQuery(dbCommand, new DbCommandInterceptionContext());
						}
					});
				}
				string createObjectsScript = SqlProviderServices.CreateObjectsScript(sqlVersion, storeItemCollection);
				if (!string.IsNullOrWhiteSpace(createObjectsScript))
				{
					SqlProviderServices.UsingConnection(requiredSqlConnection, delegate(DbConnection conn)
					{
						using (DbCommand dbCommand = SqlProviderServices.CreateCommand(conn, createObjectsScript, commandTimeout))
						{
							DbInterception.Dispatch.Command.NonQuery(dbCommand, new DbCommandInterceptionContext());
						}
					});
				}
			}
			catch (Exception ex)
			{
				try
				{
					SqlProviderServices.DropDatabase(requiredSqlConnection, commandTimeout, databaseName);
				}
				catch (Exception ex2)
				{
					throw new InvalidOperationException(Strings.SqlProvider_IncompleteCreateDatabase, new AggregateException(Strings.SqlProvider_IncompleteCreateDatabaseAggregate, new Exception[]
					{
						ex,
						ex2
					}));
				}
				throw;
			}
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x000167E8 File Offset: 0x000149E8
		private static void GetOrGenerateDatabaseNameAndGetFileNames(SqlConnection sqlConnection, out string databaseName, out string dataFileName, out string logFileName)
		{
			SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder(DbInterception.Dispatch.Connection.GetConnectionString(sqlConnection, new DbInterceptionContext()));
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
			throw new InvalidOperationException(Strings.SqlProvider_DdlGeneration_MissingInitialCatalog);
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x00016868 File Offset: 0x00014A68
		private static string GetLdfFileName(string dataFileName)
		{
			DirectoryInfo directory = new FileInfo(dataFileName).Directory;
			return Path.Combine(directory.FullName, Path.GetFileNameWithoutExtension(dataFileName) + "_log.ldf");
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x000168A0 File Offset: 0x00014AA0
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

		// Token: 0x06000481 RID: 1153 RVA: 0x0001693E File Offset: 0x00014B3E
		private static string GetMdfFileName(string attachDBFile)
		{
			return DbProviderServices.ExpandDataDirectory(attachDBFile);
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x000169B0 File Offset: 0x00014BB0
		internal static SqlVersion CreateDatabaseFromScript(int? commandTimeout, DbConnection sqlConnection, string createDatabaseScript)
		{
			SqlVersion sqlVersion = (SqlVersion)0;
			SqlProviderServices.UsingMasterConnection(sqlConnection, delegate(DbConnection conn)
			{
				using (DbCommand dbCommand = SqlProviderServices.CreateCommand(conn, createDatabaseScript, commandTimeout))
				{
					DbInterception.Dispatch.Command.NonQuery(dbCommand, new DbCommandInterceptionContext());
				}
				sqlVersion = SqlVersionUtils.GetSqlVersion(conn);
			});
			return sqlVersion;
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x00016A00 File Offset: 0x00014C00
		protected override bool DbDatabaseExists(DbConnection connection, int? commandTimeout, StoreItemCollection storeItemCollection)
		{
			return this.DbDatabaseExists(connection, commandTimeout, new Lazy<StoreItemCollection>(() => storeItemCollection));
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x00016AC4 File Offset: 0x00014CC4
		protected override bool DbDatabaseExists(DbConnection connection, int? commandTimeout, Lazy<StoreItemCollection> storeItemCollection)
		{
			Check.NotNull<DbConnection>(connection, "connection");
			Check.NotNull<Lazy<StoreItemCollection>>(storeItemCollection, "storeItemCollection");
			if (connection.State == ConnectionState.Open)
			{
				return true;
			}
			SqlConnection requiredSqlConnection = SqlProviderUtilities.GetRequiredSqlConnection(connection);
			SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder(DbInterception.Dispatch.Connection.GetConnectionString(requiredSqlConnection, new DbInterceptionContext()));
			if (string.IsNullOrEmpty(sqlConnectionStringBuilder.InitialCatalog) && string.IsNullOrEmpty(sqlConnectionStringBuilder.AttachDBFilename))
			{
				throw new InvalidOperationException(Strings.SqlProvider_DdlGeneration_MissingInitialCatalog);
			}
			if (!string.IsNullOrEmpty(sqlConnectionStringBuilder.InitialCatalog) && SqlProviderServices.CheckDatabaseExists(requiredSqlConnection, commandTimeout, sqlConnectionStringBuilder.InitialCatalog))
			{
				return true;
			}
			if (!string.IsNullOrEmpty(sqlConnectionStringBuilder.AttachDBFilename))
			{
				try
				{
					SqlProviderServices.UsingConnection(requiredSqlConnection, delegate(DbConnection con)
					{
					});
					return true;
				}
				catch (SqlException innerException)
				{
					if (!string.IsNullOrEmpty(sqlConnectionStringBuilder.InitialCatalog))
					{
						return SqlProviderServices.CheckDatabaseExists(requiredSqlConnection, commandTimeout, sqlConnectionStringBuilder.InitialCatalog);
					}
					string fileName = SqlProviderServices.GetMdfFileName(sqlConnectionStringBuilder.AttachDBFilename);
					bool databaseDoesNotExistInSysTables = false;
					SqlProviderServices.UsingMasterConnection(requiredSqlConnection, delegate(DbConnection conn)
					{
						SqlVersion sqlVersion = SqlVersionUtils.GetSqlVersion(conn);
						string commandText = SqlDdlBuilder.CreateCountDatabasesBasedOnFileNameScript(fileName, sqlVersion == SqlVersion.Sql8);
						using (DbCommand dbCommand = SqlProviderServices.CreateCommand(conn, commandText, commandTimeout))
						{
							int num = (int)DbInterception.Dispatch.Command.Scalar(dbCommand, new DbCommandInterceptionContext());
							databaseDoesNotExistInSysTables = (num == 0);
						}
					});
					if (databaseDoesNotExistInSysTables)
					{
						return false;
					}
					throw new InvalidOperationException(Strings.SqlProvider_DdlGeneration_CannotTellIfDatabaseExists, innerException);
				}
				return false;
			}
			return false;
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x00016CA4 File Offset: 0x00014EA4
		private static bool CheckDatabaseExists(SqlConnection sqlConnection, int? commandTimeout, string databaseName)
		{
			bool databaseExists = false;
			SqlProviderServices.UsingMasterConnection(sqlConnection, delegate(DbConnection conn)
			{
				string commandText = SqlDdlBuilder.CreateDatabaseExistsScript(databaseName);
				using (DbCommand dbCommand = SqlProviderServices.CreateCommand(conn, commandText, commandTimeout))
				{
					databaseExists = ((int)DbInterception.Dispatch.Command.Scalar(dbCommand, new DbCommandInterceptionContext()) >= 1);
				}
			});
			return databaseExists;
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x00016D7C File Offset: 0x00014F7C
		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
		protected override void DbDeleteDatabase(DbConnection connection, int? commandTimeout, StoreItemCollection storeItemCollection)
		{
			Check.NotNull<DbConnection>(connection, "connection");
			Check.NotNull<StoreItemCollection>(storeItemCollection, "storeItemCollection");
			SqlConnection requiredSqlConnection = SqlProviderUtilities.GetRequiredSqlConnection(connection);
			SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder(DbInterception.Dispatch.Connection.GetConnectionString(requiredSqlConnection, new DbInterceptionContext()));
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
				SqlProviderServices.UsingMasterConnection(requiredSqlConnection, delegate(DbConnection conn)
				{
					SqlVersion sqlVersion = SqlVersionUtils.GetSqlVersion(conn);
					string commandText = SqlDdlBuilder.CreateGetDatabaseNamesBasedOnFileNameScript(fullFileName, sqlVersion == SqlVersion.Sql8);
					DbCommand command = SqlProviderServices.CreateCommand(conn, commandText, commandTimeout);
					using (DbDataReader dbDataReader = DbInterception.Dispatch.Command.Reader(command, new DbCommandInterceptionContext()))
					{
						while (dbDataReader.Read())
						{
							databaseNames.Add(dbDataReader.GetString(0));
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
				throw new InvalidOperationException(Strings.SqlProvider_DdlGeneration_CannotDeleteDatabaseNoInitialCatalog);
			}
			throw new InvalidOperationException(Strings.SqlProvider_DdlGeneration_MissingInitialCatalog);
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x00016F10 File Offset: 0x00015110
		private static void DropDatabase(SqlConnection sqlConnection, int? commandTimeout, string databaseName)
		{
			SqlConnection.ClearAllPools();
			string dropDatabaseScript = SqlDdlBuilder.DropDatabaseScript(databaseName);
			try
			{
				SqlProviderServices.UsingMasterConnection(sqlConnection, delegate(DbConnection conn)
				{
					using (DbCommand dbCommand = SqlProviderServices.CreateCommand(conn, dropDatabaseScript, commandTimeout))
					{
						DbInterception.Dispatch.Command.NonQuery(dbCommand, new DbCommandInterceptionContext());
					}
				});
			}
			catch (SqlException ex)
			{
				foreach (object obj in ex.Errors)
				{
					SqlError sqlError = (SqlError)obj;
					if (sqlError.Number == 5120)
					{
						return;
					}
				}
				throw;
			}
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x00016FC0 File Offset: 0x000151C0
		private static string CreateObjectsScript(SqlVersion version, StoreItemCollection storeItemCollection)
		{
			return SqlDdlBuilder.CreateObjectsScript(storeItemCollection, version != SqlVersion.Sql8);
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x00016FD0 File Offset: 0x000151D0
		[SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Disposed by caller")]
		private static DbCommand CreateCommand(DbConnection sqlConnection, string commandText, int? commandTimeout)
		{
			if (string.IsNullOrEmpty(commandText))
			{
				commandText = Environment.NewLine;
			}
			DbCommand dbCommand = sqlConnection.CreateCommand();
			dbCommand.CommandText = commandText;
			if (commandTimeout != null)
			{
				dbCommand.CommandTimeout = commandTimeout.Value;
			}
			return dbCommand;
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x00017188 File Offset: 0x00015388
		private static void UsingConnection(DbConnection sqlConnection, Action<DbConnection> act)
		{
			DbInterceptionContext interceptionContext = new DbInterceptionContext();
			string holdConnectionString = DbInterception.Dispatch.Connection.GetConnectionString(sqlConnection, interceptionContext);
			DbProviderServices.GetExecutionStrategy(sqlConnection, "System.Data.SqlClient").Execute(delegate()
			{
				bool flag = DbInterception.Dispatch.Connection.GetState(sqlConnection, interceptionContext) == ConnectionState.Closed;
				if (flag)
				{
					if (DbInterception.Dispatch.Connection.GetState(sqlConnection, new DbInterceptionContext()) == ConnectionState.Closed && !DbInterception.Dispatch.Connection.GetConnectionString(sqlConnection, interceptionContext).Equals(holdConnectionString, StringComparison.Ordinal))
					{
						DbInterception.Dispatch.Connection.SetConnectionString(sqlConnection, new DbConnectionPropertyInterceptionContext<string>().WithValue(holdConnectionString));
					}
					DbInterception.Dispatch.Connection.Open(sqlConnection, interceptionContext);
				}
				try
				{
					act(sqlConnection);
				}
				finally
				{
					if (flag && DbInterception.Dispatch.Connection.GetState(sqlConnection, interceptionContext) == ConnectionState.Open)
					{
						DbInterception.Dispatch.Connection.Close(sqlConnection, interceptionContext);
						if (!DbInterception.Dispatch.Connection.GetConnectionString(sqlConnection, interceptionContext).Equals(holdConnectionString, StringComparison.Ordinal))
						{
							DbInterception.Dispatch.Connection.SetConnectionString(sqlConnection, new DbConnectionPropertyInterceptionContext<string>().WithValue(holdConnectionString));
						}
					}
				}
			});
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x000171F8 File Offset: 0x000153F8
		private static void UsingMasterConnection(DbConnection sqlConnection, Action<DbConnection> act)
		{
			SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder(DbInterception.Dispatch.Connection.GetConnectionString(sqlConnection, new DbInterceptionContext()))
			{
				InitialCatalog = "master",
				AttachDBFilename = string.Empty
			};
			try
			{
				using (DbConnection dbConnection = DbProviderServices.GetProviderFactory(sqlConnection).CreateConnection())
				{
					DbInterception.Dispatch.Connection.SetConnectionString(dbConnection, new DbConnectionPropertyInterceptionContext<string>().WithValue(sqlConnectionStringBuilder.ConnectionString));
					SqlProviderServices.UsingConnection(dbConnection, act);
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

		// Token: 0x0400010B RID: 267
		public const string ProviderInvariantName = "System.Data.SqlClient";

		// Token: 0x0400010C RID: 268
		private ConcurrentDictionary<string, SqlProviderManifest> _providerManifests = new ConcurrentDictionary<string, SqlProviderManifest>();

		// Token: 0x0400010D RID: 269
		private static readonly SqlProviderServices _providerInstance = new SqlProviderServices();

		// Token: 0x0400010E RID: 270
		private static bool _truncateDecimalsToScale = true;
	}
}
