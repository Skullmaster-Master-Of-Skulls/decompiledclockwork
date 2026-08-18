using System;

namespace OracleInternal.Common
{
	// Token: 0x02000038 RID: 56
	internal class ConfigInfo
	{
		// Token: 0x0400032C RID: 812
		public const string SectionName = "oracle.dataaccess.client";

		// Token: 0x0400032D RID: 813
		public const string Section = "oracle.unmanageddataaccess.client";

		// Token: 0x0400032E RID: 814
		public const string ODPMSectionName = "oracle.manageddataaccess.client";

		// Token: 0x0400032F RID: 815
		public const string VersionSubSection = "version";

		// Token: 0x04000330 RID: 816
		public const string VersionNumber = "number";

		// Token: 0x04000331 RID: 817
		public const string VersionNumberGeneric = "*";

		// Token: 0x04000332 RID: 818
		public const string DataSourcesElement = "dataSources";

		// Token: 0x04000333 RID: 819
		public const string LDAPsettingsElement = "LDAPsettings";

		// Token: 0x04000334 RID: 820
		public const string DataSourceAlias = "alias";

		// Token: 0x04000335 RID: 821
		public const string DataSourceDesc = "descriptor";

		// Token: 0x04000336 RID: 822
		public const string StoredProcedure = "storedProcedure";

		// Token: 0x04000337 RID: 823
		public const string ConfigParamName = "name";

		// Token: 0x04000338 RID: 824
		public const string ConfigParamValue = "value";

		// Token: 0x04000339 RID: 825
		public const string ConfigNetTypeName = "NETType";

		// Token: 0x0400033A RID: 826
		public const string ConfigDbTypeName = "DBType";

		// Token: 0x0400033B RID: 827
		public const string UdtCustomClass = "udtcustomclass";

		// Token: 0x0400033C RID: 828
		public const string Schema = "schema";

		// Token: 0x0400033D RID: 829
		public const string ConnectionPoolsElement = "connectionPools";

		// Token: 0x0400033E RID: 830
		public const string ConnectionPoolConnectionString = "connectionString";

		// Token: 0x0400033F RID: 831
		public const string ConnectionPoolName = "poolName";

		// Token: 0x04000340 RID: 832
		public const string Mode = "mode";

		// Token: 0x04000341 RID: 833
		public const string Position = "position";

		// Token: 0x04000342 RID: 834
		public const string Implicit = "Implicit";

		// Token: 0x04000343 RID: 835
		public const string UDTMappingsElement = "udtMappings";

		// Token: 0x04000344 RID: 836
		public const string SettingsElement = "settings";

		// Token: 0x04000345 RID: 837
		public const string DbNtfnPort = "DbNotificationPort";

		// Token: 0x04000346 RID: 838
		public const string DbNtfnRegInterval = "DbNotificationRegInterval";

		// Token: 0x04000347 RID: 839
		public const string XMLTypeClientSideDecoding = "XMLTypeClientSideDecoding";

		// Token: 0x04000348 RID: 840
		public const string XMLTypeOpcodeDump = "XMLTypeOpcodeDump";

		// Token: 0x04000349 RID: 841
		public const string XMLTypeMaxCacheEntries = "XMLTypeMaxCacheEntries";

		// Token: 0x0400034A RID: 842
		public const string XMLTypeMaxCacheSize = "XMLTypeMaxCacheSize";

		// Token: 0x0400034B RID: 843
		public const string XMLTypeParseAllXml = "XMLTypeParseAllXml";

		// Token: 0x0400034C RID: 844
		public const string XMLTypeParseXml = "XMLTypeParseXml";

		// Token: 0x0400034D RID: 845
		public const string XMLTypeUseHeaderEncodingFromServer = "XMLTypeUseHeaderEncodingFromServer";

		// Token: 0x0400034E RID: 846
		public const string XMLTypeOptimizationLevel = "XMLTypeOptimizationLevel";

		// Token: 0x0400034F RID: 847
		public const string FetchSize = "FetchSize";

		// Token: 0x04000350 RID: 848
		public const string ImplicitRefCursor = "implicitRefCursor";

		// Token: 0x04000351 RID: 849
		public const string OraDebugJDWP = "ORA_DEBUG_JDWP";

		// Token: 0x04000352 RID: 850
		public const string AttemptConnectDuringSvcRelocation = "AttemptConnectDuringSvcRelocation";

		// Token: 0x04000353 RID: 851
		public const string ServiceRelocationConnectionTimeout = "ServiceRelocationConnectionTimeout";

		// Token: 0x04000354 RID: 852
		public const string Hostname_Default_Service_is_Host = "Hostname.Default_Service_is_Host";

		// Token: 0x04000355 RID: 853
		public const string ConnectionPoolType = "ConnectionPoolType";

		// Token: 0x04000356 RID: 854
		public const string CPVersion = "CPVersion";

		// Token: 0x04000357 RID: 855
		public const string ConfigSchemaFile = "Oracle.ManagedDataAccess.src.Common.Resources.Oracle.ManagedDataAccess.Client.Configuration.Section.xsd";

		// Token: 0x04000358 RID: 856
		public const string CommonConfigSchemaFile = "Oracle.ManagedDataAccess.src.Common.Resources.Oracle.DataAccess.Common.Configuration.Section.xsd";

		// Token: 0x04000359 RID: 857
		public const string ColumnOrdinal = "columnOrdinal";

		// Token: 0x0400035A RID: 858
		public const string MetaDataXml = "MetaDataXml";

		// Token: 0x0400035B RID: 859
		public const string RevertBUErrHandling = "RevertBatchUpdateErrorHandling";

		// Token: 0x0400035C RID: 860
		public const string PerfCounters = "PerformanceCounters";

		// Token: 0x0400035D RID: 861
		public const string DistributedTransaction = "distributedTransaction";

		// Token: 0x0400035E RID: 862
		public const string distTxnSessTxnTimeToLive = "oramts_sess_txntimetolive";

		// Token: 0x0400035F RID: 863
		public const string distTxnUseDTCDLL = "UseManagedDTC";

		// Token: 0x04000360 RID: 864
		public const string PromotableTxn = "PromotableTransaction";

		// Token: 0x04000361 RID: 865
		public const string distTxnRecoveryHost = "omtsreco_ip_address";

		// Token: 0x04000362 RID: 866
		public const string distTxnRecoveryPort = "omtsreco_port";

		// Token: 0x04000363 RID: 867
		public const string distTxnUseManagedDTC = "useoramtsmanaged";

		// Token: 0x04000364 RID: 868
		public const string LegacyEntireLOBFetch = "LegacyEntireLOBFetch";

		// Token: 0x04000365 RID: 869
		public const string HAEvents = "HAEvents";

		// Token: 0x04000366 RID: 870
		public const string LoadBalancing = "LoadBalancing";

		// Token: 0x04000367 RID: 871
		public const string StmtCacheSize = "StatementCacheSize";

		// Token: 0x04000368 RID: 872
		public const string TraceLevel = "TraceLevel";

		// Token: 0x04000369 RID: 873
		public const string BindByName = "BindByName";

		// Token: 0x0400036A RID: 874
		public const string TraceOption = "TraceOption";

		// Token: 0x0400036B RID: 875
		public const string TraceFileLocation = "TraceFileLocation";

		// Token: 0x0400036C RID: 876
		public const string TnsAdmin = "TNS_ADMIN";

		// Token: 0x0400036D RID: 877
		public const string LdapAdmin = "LDAP_ADMIN";

		// Token: 0x0400036E RID: 878
		public const string NoPSPESupport = "DoNotUsePSPE";

		// Token: 0x0400036F RID: 879
		public const string UdtMapping = "udtMapping";

		// Token: 0x04000370 RID: 880
		public const string ConnectionPool = "ConnectionPool";

		// Token: 0x04000371 RID: 881
		public const string DataSource = "dataSource";

		// Token: 0x04000372 RID: 882
		public const string SchemaName = "schemaName";

		// Token: 0x04000373 RID: 883
		public const string TypeName = "typeName";

		// Token: 0x04000374 RID: 884
		public const string schemaName = "schema";

		// Token: 0x04000375 RID: 885
		public const string typeName = "typename";

		// Token: 0x04000376 RID: 886
		public const string FactoryName = "factoryName";

		// Token: 0x04000377 RID: 887
		public const string DemandOrclPermission = "DemandOraclePermission";

		// Token: 0x04000378 RID: 888
		public const string SelfTuning = "SelfTuning";

		// Token: 0x04000379 RID: 889
		public const string MaxStatementCacheSize = "MaxStatementCacheSize";

		// Token: 0x0400037A RID: 890
		public const string AppEdition = "Edition";

		// Token: 0x0400037B RID: 891
		public const string UseLegacyLocalParser = "UseLegacyLocalParser";

		// Token: 0x0400037C RID: 892
		public const string ColumnCacheSize = "ColumnCacheSize";

		// Token: 0x0400037D RID: 893
		public const string DRCPConnectionClass = "DRCPConnectionClass";

		// Token: 0x0400037E RID: 894
		public const string RefCursorBindInfo = "bindInfo";

		// Token: 0x0400037F RID: 895
		public const string RefCursorbindInfo = "bindInfo";

		// Token: 0x04000380 RID: 896
		public const string RefCursorMetadata = "metadata";

		// Token: 0x04000381 RID: 897
		public const string RefCursorKey = "RefCursor";

		// Token: 0x04000382 RID: 898
		public const string refCursorKey = "refCursor";

		// Token: 0x04000383 RID: 899
		public const string RefCursorMetadataKey = "RefCursorMetaData";

		// Token: 0x04000384 RID: 900
		public const string ImplicitRefCursorMetadataKey = "ImplicitRefCursorMetaData";

		// Token: 0x04000385 RID: 901
		public const string RefCursor = "refCursor";

		// Token: 0x04000386 RID: 902
		public const string Name = "name";

		// Token: 0x04000387 RID: 903
		public const string EdmMappingsElement = "edmMappings";

		// Token: 0x04000388 RID: 904
		public const string EdmMappingElement = "edmMapping";

		// Token: 0x04000389 RID: 905
		public const string EdmNumberMappingElement = "edmNumberMapping";

		// Token: 0x0400038A RID: 906
		public const string EdmNumberMappingMinPrecision = "MinPrecision";

		// Token: 0x0400038B RID: 907
		public const string EdmNumberMappingMaxPrecision = "MaxPrecision";

		// Token: 0x0400038C RID: 908
		public const string DataType = "dataType";

		// Token: 0x0400038D RID: 909
		public const string Precision = "precision";

		// Token: 0x0400038E RID: 910
		public const string Scale = "scale";

		// Token: 0x0400038F RID: 911
		public const string Add = "add";

		// Token: 0x04000390 RID: 912
		public const string NumberDataType = "number";

		// Token: 0x04000391 RID: 913
		public const string edmMapping = "EDMMAPPING";

		// Token: 0x04000392 RID: 914
		public const string PromotableTransaction = "PROMOTABLE";

		// Token: 0x04000393 RID: 915
		public const string LocalTransaction = "LOCAL";

		// Token: 0x04000394 RID: 916
		public const string LegacyIsolationLevelBehavior = "LegacyIsolationLevelBehavior";

		// Token: 0x04000395 RID: 917
		internal const string ONSConfig = "onsConfig";

		// Token: 0x04000396 RID: 918
		internal const string ONSParamName = "name";

		// Token: 0x04000397 RID: 919
		internal const string ONSParamValue = "value";

		// Token: 0x04000398 RID: 920
		internal const string ONSNodeList = "nodeList";

		// Token: 0x04000399 RID: 921
		internal const string VERSION = "(VERSION)";

		// Token: 0x0400039A RID: 922
		internal const string CONFIG = "(CONFIG)";

		// Token: 0x0400039B RID: 923
		internal const string TNSNAMES = "(TNSNAMES.ORA)";

		// Token: 0x0400039C RID: 924
		internal const string SQLNET = "(SQLNET.ORA)";

		// Token: 0x0400039D RID: 925
		internal const string ENVIRONMENT = "(ENVIRONMENT)";

		// Token: 0x0400039E RID: 926
		public const string InitialLOBFetchSize = "InitialLOBFetchSize";

		// Token: 0x0400039F RID: 927
		public const string InitialLONGFetchSize = "InitialLONGFetchSize";

		// Token: 0x040003A0 RID: 928
		public const string ODPMRegistryKey = "SOFTWARE\\Oracle\\ODP.NET.Managed";

		// Token: 0x040003A1 RID: 929
		public const string ODPURegistryKey = "SOFTWARE\\Oracle\\ODP.NET";

		// Token: 0x040003A2 RID: 930
		internal static string ONSConfigFile = "configFile";

		// Token: 0x040003A3 RID: 931
		internal static string ONSDatabase = "database";

		// Token: 0x040003A4 RID: 932
		internal static string ONSMode = "mode";

		// Token: 0x040003A5 RID: 933
		internal static string ONSConfigFileName = "ons.config";

		// Token: 0x040003A6 RID: 934
		internal static string ONSNodes = "nodes";

		// Token: 0x040003A7 RID: 935
		internal static string ONSRemotePort = "remoteport";
	}
}
