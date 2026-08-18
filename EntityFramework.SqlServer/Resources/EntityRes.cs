using System;
using System.CodeDom.Compiler;
using System.Data.Entity.SqlServer.Utilities;
using System.Globalization;
using System.Resources;
using System.Threading;

namespace System.Data.Entity.SqlServer.Resources
{
	// Token: 0x02000016 RID: 22
	[GeneratedCode("Resources.SqlServer.tt", "1.0.0.0")]
	internal sealed class EntityRes
	{
		// Token: 0x060000F1 RID: 241 RVA: 0x00004B1C File Offset: 0x00002D1C
		private EntityRes()
		{
			this.resources = new ResourceManager("System.Data.Entity.SqlServer.Properties.Resources.SqlServer", typeof(SqlProviderServices).Assembly());
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00004B44 File Offset: 0x00002D44
		private static EntityRes GetLoader()
		{
			if (EntityRes.loader == null)
			{
				EntityRes value = new EntityRes();
				Interlocked.CompareExchange<EntityRes>(ref EntityRes.loader, value, null);
			}
			return EntityRes.loader;
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x00004B70 File Offset: 0x00002D70
		private static CultureInfo Culture
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x00004B73 File Offset: 0x00002D73
		public static ResourceManager Resources
		{
			get
			{
				return EntityRes.GetLoader().resources;
			}
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00004B80 File Offset: 0x00002D80
		public static string GetString(string name, params object[] args)
		{
			EntityRes entityRes = EntityRes.GetLoader();
			if (entityRes == null)
			{
				return null;
			}
			string @string = entityRes.resources.GetString(name, EntityRes.Culture);
			if (args != null && args.Length > 0)
			{
				for (int i = 0; i < args.Length; i++)
				{
					string text = args[i] as string;
					if (text != null && text.Length > 1024)
					{
						args[i] = text.Substring(0, 1021) + "...";
					}
				}
				return string.Format(CultureInfo.CurrentCulture, @string, args);
			}
			return @string;
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00004C04 File Offset: 0x00002E04
		public static string GetString(string name)
		{
			EntityRes entityRes = EntityRes.GetLoader();
			if (entityRes == null)
			{
				return null;
			}
			return entityRes.resources.GetString(name, EntityRes.Culture);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00004C2D File Offset: 0x00002E2D
		public static string GetString(string name, out bool usedFallback)
		{
			usedFallback = false;
			return EntityRes.GetString(name);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00004C38 File Offset: 0x00002E38
		public static object GetObject(string name)
		{
			EntityRes entityRes = EntityRes.GetLoader();
			if (entityRes == null)
			{
				return null;
			}
			return entityRes.resources.GetObject(name, EntityRes.Culture);
		}

		// Token: 0x0400001A RID: 26
		internal const string ArgumentIsNullOrWhitespace = "ArgumentIsNullOrWhitespace";

		// Token: 0x0400001B RID: 27
		internal const string SqlProvider_GeographyValueNotSqlCompatible = "SqlProvider_GeographyValueNotSqlCompatible";

		// Token: 0x0400001C RID: 28
		internal const string SqlProvider_GeometryValueNotSqlCompatible = "SqlProvider_GeometryValueNotSqlCompatible";

		// Token: 0x0400001D RID: 29
		internal const string ProviderReturnedNullForGetDbInformation = "ProviderReturnedNullForGetDbInformation";

		// Token: 0x0400001E RID: 30
		internal const string ProviderDoesNotSupportType = "ProviderDoesNotSupportType";

		// Token: 0x0400001F RID: 31
		internal const string NoStoreTypeForEdmType = "NoStoreTypeForEdmType";

		// Token: 0x04000020 RID: 32
		internal const string Mapping_Provider_WrongManifestType = "Mapping_Provider_WrongManifestType";

		// Token: 0x04000021 RID: 33
		internal const string ADP_InternalProviderError = "ADP_InternalProviderError";

		// Token: 0x04000022 RID: 34
		internal const string UnableToDetermineStoreVersion = "UnableToDetermineStoreVersion";

		// Token: 0x04000023 RID: 35
		internal const string SqlProvider_NeedSqlDataReader = "SqlProvider_NeedSqlDataReader";

		// Token: 0x04000024 RID: 36
		internal const string SqlProvider_Sql2008RequiredForSpatial = "SqlProvider_Sql2008RequiredForSpatial";

		// Token: 0x04000025 RID: 37
		internal const string SqlProvider_SqlTypesAssemblyNotFound = "SqlProvider_SqlTypesAssemblyNotFound";

		// Token: 0x04000026 RID: 38
		internal const string SqlProvider_IncompleteCreateDatabase = "SqlProvider_IncompleteCreateDatabase";

		// Token: 0x04000027 RID: 39
		internal const string SqlProvider_IncompleteCreateDatabaseAggregate = "SqlProvider_IncompleteCreateDatabaseAggregate";

		// Token: 0x04000028 RID: 40
		internal const string SqlProvider_DdlGeneration_MissingInitialCatalog = "SqlProvider_DdlGeneration_MissingInitialCatalog";

		// Token: 0x04000029 RID: 41
		internal const string SqlProvider_DdlGeneration_CannotDeleteDatabaseNoInitialCatalog = "SqlProvider_DdlGeneration_CannotDeleteDatabaseNoInitialCatalog";

		// Token: 0x0400002A RID: 42
		internal const string SqlProvider_DdlGeneration_CannotTellIfDatabaseExists = "SqlProvider_DdlGeneration_CannotTellIfDatabaseExists";

		// Token: 0x0400002B RID: 43
		internal const string SqlProvider_CredentialsMissingForMasterConnection = "SqlProvider_CredentialsMissingForMasterConnection";

		// Token: 0x0400002C RID: 44
		internal const string SqlProvider_InvalidGeographyColumn = "SqlProvider_InvalidGeographyColumn";

		// Token: 0x0400002D RID: 45
		internal const string SqlProvider_InvalidGeometryColumn = "SqlProvider_InvalidGeometryColumn";

		// Token: 0x0400002E RID: 46
		internal const string Mapping_Provider_WrongConnectionType = "Mapping_Provider_WrongConnectionType";

		// Token: 0x0400002F RID: 47
		internal const string Update_NotSupportedServerGenKey = "Update_NotSupportedServerGenKey";

		// Token: 0x04000030 RID: 48
		internal const string Update_NotSupportedIdentityType = "Update_NotSupportedIdentityType";

		// Token: 0x04000031 RID: 49
		internal const string Update_SqlEntitySetWithoutDmlFunctions = "Update_SqlEntitySetWithoutDmlFunctions";

		// Token: 0x04000032 RID: 50
		internal const string Cqt_General_UnsupportedExpression = "Cqt_General_UnsupportedExpression";

		// Token: 0x04000033 RID: 51
		internal const string SqlGen_ApplyNotSupportedOnSql8 = "SqlGen_ApplyNotSupportedOnSql8";

		// Token: 0x04000034 RID: 52
		internal const string SqlGen_NiladicFunctionsCannotHaveParameters = "SqlGen_NiladicFunctionsCannotHaveParameters";

		// Token: 0x04000035 RID: 53
		internal const string SqlGen_InvalidDatePartArgumentExpression = "SqlGen_InvalidDatePartArgumentExpression";

		// Token: 0x04000036 RID: 54
		internal const string SqlGen_InvalidDatePartArgumentValue = "SqlGen_InvalidDatePartArgumentValue";

		// Token: 0x04000037 RID: 55
		internal const string SqlGen_TypedNaNNotSupported = "SqlGen_TypedNaNNotSupported";

		// Token: 0x04000038 RID: 56
		internal const string SqlGen_TypedPositiveInfinityNotSupported = "SqlGen_TypedPositiveInfinityNotSupported";

		// Token: 0x04000039 RID: 57
		internal const string SqlGen_TypedNegativeInfinityNotSupported = "SqlGen_TypedNegativeInfinityNotSupported";

		// Token: 0x0400003A RID: 58
		internal const string SqlGen_PrimitiveTypeNotSupportedPriorSql10 = "SqlGen_PrimitiveTypeNotSupportedPriorSql10";

		// Token: 0x0400003B RID: 59
		internal const string SqlGen_CanonicalFunctionNotSupportedPriorSql10 = "SqlGen_CanonicalFunctionNotSupportedPriorSql10";

		// Token: 0x0400003C RID: 60
		internal const string SqlGen_ParameterForLimitNotSupportedOnSql8 = "SqlGen_ParameterForLimitNotSupportedOnSql8";

		// Token: 0x0400003D RID: 61
		internal const string SqlGen_ParameterForSkipNotSupportedOnSql8 = "SqlGen_ParameterForSkipNotSupportedOnSql8";

		// Token: 0x0400003E RID: 62
		internal const string Spatial_WellKnownGeographyValueNotValid = "Spatial_WellKnownGeographyValueNotValid";

		// Token: 0x0400003F RID: 63
		internal const string Spatial_WellKnownGeometryValueNotValid = "Spatial_WellKnownGeometryValueNotValid";

		// Token: 0x04000040 RID: 64
		internal const string SqlSpatialServices_ProviderValueNotSqlType = "SqlSpatialServices_ProviderValueNotSqlType";

		// Token: 0x04000041 RID: 65
		internal const string SqlSpatialservices_CouldNotCreateWellKnownGeographyValueNoSrid = "SqlSpatialservices_CouldNotCreateWellKnownGeographyValueNoSrid";

		// Token: 0x04000042 RID: 66
		internal const string SqlSpatialservices_CouldNotCreateWellKnownGeographyValueNoWkbOrWkt = "SqlSpatialservices_CouldNotCreateWellKnownGeographyValueNoWkbOrWkt";

		// Token: 0x04000043 RID: 67
		internal const string SqlSpatialservices_CouldNotCreateWellKnownGeometryValueNoSrid = "SqlSpatialservices_CouldNotCreateWellKnownGeometryValueNoSrid";

		// Token: 0x04000044 RID: 68
		internal const string SqlSpatialservices_CouldNotCreateWellKnownGeometryValueNoWkbOrWkt = "SqlSpatialservices_CouldNotCreateWellKnownGeometryValueNoWkbOrWkt";

		// Token: 0x04000045 RID: 69
		internal const string TransientExceptionDetected = "TransientExceptionDetected";

		// Token: 0x04000046 RID: 70
		internal const string ELinq_DbFunctionDirectCall = "ELinq_DbFunctionDirectCall";

		// Token: 0x04000047 RID: 71
		internal const string AutomaticMigration = "AutomaticMigration";

		// Token: 0x04000048 RID: 72
		internal const string InvalidDatabaseName = "InvalidDatabaseName";

		// Token: 0x04000049 RID: 73
		internal const string SqlServerMigrationSqlGenerator_UnknownOperation = "SqlServerMigrationSqlGenerator_UnknownOperation";

		// Token: 0x0400004A RID: 74
		private static EntityRes loader;

		// Token: 0x0400004B RID: 75
		private readonly ResourceManager resources;
	}
}
