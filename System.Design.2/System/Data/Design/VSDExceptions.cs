using System;

namespace System.Data.Design
{
	// Token: 0x02000280 RID: 640
	internal class VSDExceptions
	{
		// Token: 0x06001848 RID: 6216 RVA: 0x0000362F File Offset: 0x0000182F
		private VSDExceptions()
		{
		}

		// Token: 0x020004C3 RID: 1219
		internal class COMMON
		{
			// Token: 0x06002C49 RID: 11337 RVA: 0x0000362F File Offset: 0x0000182F
			private COMMON()
			{
			}

			// Token: 0x04001EBD RID: 7869
			internal const int START_CODE = 0;

			// Token: 0x04001EBE RID: 7870
			internal const string SIMPLENAMESERVICE_NAMEOVERFLOW_MSG = "Failed to create unique name after many attempts";

			// Token: 0x04001EBF RID: 7871
			internal const int SIMPLENAMESERVICE_NAMEOVERFLOW_CODE = 1;

			// Token: 0x04001EC0 RID: 7872
			internal const string NOT_A_NAMED_OBJECT_MSG = "Named object collection holds something that is not a named object";

			// Token: 0x04001EC1 RID: 7873
			internal const int NOT_A_NAMED_OBJECT_CODE = 2;
		}

		// Token: 0x020004C4 RID: 1220
		internal class DataSource
		{
			// Token: 0x06002C4A RID: 11338 RVA: 0x0000362F File Offset: 0x0000182F
			private DataSource()
			{
			}

			// Token: 0x04001EC2 RID: 7874
			internal const int START_CODE = 20000;

			// Token: 0x04001EC3 RID: 7875
			internal const string NO_CONNECTION_NAME_SERVICE_MSG = "Failed to obtain name service for connection collection";

			// Token: 0x04001EC4 RID: 7876
			internal const int NO_CONNECTION_NAME_SERVICE_CODE = 20001;

			// Token: 0x04001EC5 RID: 7877
			internal const string TABLE_BELONGS_TO_OTHER_DATA_SOURCE_MSG = "This table belongs to another DataSource already";

			// Token: 0x04001EC6 RID: 7878
			internal const int TABLE_BELONGS_TO_OTHER_DATA_SOURCE_CODE = 20002;

			// Token: 0x04001EC7 RID: 7879
			internal const string PARAMETER_NOT_FOUND_MSG = "No parameter named '{0}' found";

			// Token: 0x04001EC8 RID: 7880
			internal const int PARAMETER_NOT_FOUND_CODE = 20004;

			// Token: 0x04001EC9 RID: 7881
			internal const string INVALID_PARAMETER_VALUE_MSG = "Invalid parameter value (the object passed in must support IDbDataParameter interface)";

			// Token: 0x04001ECA RID: 7882
			internal const int INVALID_PARAMETER_VALUE_CODE = 20005;

			// Token: 0x04001ECB RID: 7883
			internal const string INVALID_SOURCE_VALUE_MSG = "SourceCollection can only hold objects of type Source";

			// Token: 0x04001ECC RID: 7884
			internal const int INVALID_SOURCE_VALUE_CODE = 20006;

			// Token: 0x04001ECD RID: 7885
			internal const string OP_VALID_FOR_RAD_TABLE_ONLY_MSG = "Operation invalid. Table gets data from something else than a database.";

			// Token: 0x04001ECE RID: 7886
			internal const int OP_VALID_FOR_RAD_TABLE_ONLY_CODE = 20007;

			// Token: 0x04001ECF RID: 7887
			internal const string INVALID_COLUMN_VALUE_MSG = "DesignColumnCollection can only hold objects of type DesignColumn";

			// Token: 0x04001ED0 RID: 7888
			internal const int INVALID_COLUMN_VALUE_CODE = 20008;

			// Token: 0x04001ED1 RID: 7889
			internal const string DESIGN_COLUMN_NEEDS_DATA_COLUMN_MSG = "DesignColumn object needs a valid DataColumn";

			// Token: 0x04001ED2 RID: 7890
			internal const int DESIGN_COLUMN_NEEDS_DATA_COLUMN_CODE = 20009;

			// Token: 0x04001ED3 RID: 7891
			internal const string RELATION_BELONGS_TO_OTHER_DATA_SOURCE_MSG = "This relation belongs to another DataSource already";

			// Token: 0x04001ED4 RID: 7892
			internal const int RELATION_BELONGS_TO_OTHER_DATA_SOURCE_CODE = 20010;

			// Token: 0x04001ED5 RID: 7893
			internal const string INVALID_COLUMN_INDEX_MSG = "Index out of range in getting DesignColumn";

			// Token: 0x04001ED6 RID: 7894
			internal const int INVALID_COLUMN_INDEX_CODE = 20011;

			// Token: 0x04001ED7 RID: 7895
			internal const string COMMAND_NOT_SET_MSG = "Command not set. Operation cannot be performed";

			// Token: 0x04001ED8 RID: 7896
			internal const int COMMAND_NOT_SET_CODE = 20012;

			// Token: 0x04001ED9 RID: 7897
			internal const string CONNECTION_NOT_SET_MSG = "Connection not set. Operation cannot be performed";

			// Token: 0x04001EDA RID: 7898
			internal const int CONNECTION_NOT_SET_CODE = 20013;

			// Token: 0x04001EDB RID: 7899
			internal const string BAD_QUERY_FOR_GENERATING_IUD_MSG = "Cannot generate updating statements from query that is not a SELECT query";

			// Token: 0x04001EDC RID: 7900
			internal const int BAD_QUERY_FOR_GENERATING_IUD_CODE = 20014;

			// Token: 0x04001EDD RID: 7901
			internal const string INVALID_DATA_SOURCE_NAME_MSG = "Data source name is empty or invalid";

			// Token: 0x04001EDE RID: 7902
			internal const int INVALID_DATA_SOURCE_NAME_CODE = 20015;

			// Token: 0x04001EDF RID: 7903
			internal const string INVALID_COLLECTIONTYPE_MSG = "{0} can hold only {1} objects";

			// Token: 0x04001EE0 RID: 7904
			internal const int INVALID_COLLECTIONTYPE_CODE = 20016;

			// Token: 0x04001EE1 RID: 7905
			internal const string CANNOT_GET_DATA_CONFIGURATION_CONTEXT_MSG = "Data configuration context could not be obtained";

			// Token: 0x04001EE2 RID: 7906
			internal const int CANNOT_GET_DATA_CONFIGURATION_CONTEXT_CODE = 20017;

			// Token: 0x04001EE3 RID: 7907
			internal const string CANNOT_GET_IDBPROVIDER_MSG = "Data provider information could not be obtained";

			// Token: 0x04001EE4 RID: 7908
			internal const int CANNOT_GET_IDBPROVIDER_CODE = 20018;

			// Token: 0x04001EE5 RID: 7909
			internal const string DBSOURCECMD_HAS_INVALID_PARENT_MSG = "Parent of the DbSourceCommand is invalid";

			// Token: 0x04001EE6 RID: 7910
			internal const int DBSOURCECMD_HAS_INVALID_PARENT_CODE = 20019;

			// Token: 0x04001EE7 RID: 7911
			internal const string DATASOURCECOLLECTIONUNDOUNIT_INVALIDPARA_MSG = "Invalid parameters in DataSourceCollectionUndounit";

			// Token: 0x04001EE8 RID: 7912
			internal const int DATASOURCECOLLECTIONUNDOUNIT_INVALIDPARA_CODE = 20020;

			// Token: 0x04001EE9 RID: 7913
			internal const string CANNOT_GET_IDBPROVIDER_FOR_CONNECTION_MSG = "Could not get data provider information for connection of type {0}";

			// Token: 0x04001EEA RID: 7914
			internal const int CANNOT_GET_IDBPROVIDER_FOR_CONNECTION_CODE = 20021;

			// Token: 0x04001EEB RID: 7915
			internal const string CANNOT_GET_IDBPROVIDER_FOR_CSTRING_MSG = "Could not get data provider information for the following connection string:\r\n{0}\r\n";

			// Token: 0x04001EEC RID: 7916
			internal const int CANNOT_GET_IDBPROVIDER_FOR_CSTRING_CODE = 20022;

			// Token: 0x04001EED RID: 7917
			internal const string INVALID_DATA_SOURCE_MSG = "Data source is invalid (null)";

			// Token: 0x04001EEE RID: 7918
			internal const int INVALID_DATA_SOURCE_CODE = 20023;

			// Token: 0x04001EEF RID: 7919
			internal const string SELECT_COMMAND_TEXT_EMPTY_MSG = "Select command text is null or empty";

			// Token: 0x04001EF0 RID: 7920
			internal const int SELECT_COMMAND_TEXT_EMPTY_CODE = 20024;

			// Token: 0x04001EF1 RID: 7921
			internal const string PK_COLUMNS_MISSING_MSG = "Some primary key columns are missing";

			// Token: 0x04001EF2 RID: 7922
			internal const int PK_COLUMNS_MISSING_CODE = 20025;

			// Token: 0x04001EF3 RID: 7923
			internal const string CONF_CONTEXT_NULL_MSG = "Data configuration context is null";

			// Token: 0x04001EF4 RID: 7924
			internal const int CONF_CONTEXT_NULL_CODE = 20026;

			// Token: 0x04001EF5 RID: 7925
			internal const string COMMAND_OPERATION_INVALID_MSG = "Specified command operation is invalid (can only be INSERT, UPDATE or DELETE)";

			// Token: 0x04001EF6 RID: 7926
			internal const int COMMAND_OPERATION_INVALID_CODE = 20027;

			// Token: 0x04001EF7 RID: 7927
			internal const string SPROC_NAME_EMPTY_MSG = "The stored procedure name is null or empty";

			// Token: 0x04001EF8 RID: 7928
			internal const int SPROC_NAME_EMPTY_CODE = 20028;

			// Token: 0x04001EF9 RID: 7929
			internal const string NO_SPROC_GENERATOR_MSG = "We have no stored procedure generator for specified backend";

			// Token: 0x04001EFA RID: 7930
			internal const int NO_SPROC_GENERATOR_CODE = 20028;

			// Token: 0x04001EFB RID: 7931
			internal const string SOURCE_IS_NOT_DBSOURCE_MSG = "The source for this table is not a DbSource";

			// Token: 0x04001EFC RID: 7932
			internal const int SOURCE_IS_NOT_DBSOURCE_CODE = 20029;

			// Token: 0x04001EFD RID: 7933
			internal const string BINDABLE_CTRL_NOT_FOUND_MSG = "Bindable control could not be found; Visual Studio data binding configuration file might be corrupt";

			// Token: 0x04001EFE RID: 7934
			internal const int BINDABLE_CTRL_NOT_FOUND_CODE = 20030;

			// Token: 0x04001EFF RID: 7935
			internal const string CANNOT_GET_PROVIDER_FACTORY_MSG = "Could not retrieve the provider factory.";

			// Token: 0x04001F00 RID: 7936
			internal const int CANNOT_GET_PROVIDER_FACTORY_CODE = 20031;

			// Token: 0x04001F01 RID: 7937
			internal const string FACTORY_CANNOT_CREATE_ADAPTERS_MSG = "The provider factory does not support creating adapters.";

			// Token: 0x04001F02 RID: 7938
			internal const int FACTORY_CANNOT_CREATE_ADAPTERS_CODE = 20032;

			// Token: 0x04001F03 RID: 7939
			internal const string FACTORY_CANNOT_CREATE_COMMAND_BUILDERS_MSG = "The provider factory does not support creating command builders.";

			// Token: 0x04001F04 RID: 7940
			internal const int FACTORY_CANNOT_CREATE_COMMAND_BUILDERS_CODE = 20033;

			// Token: 0x04001F05 RID: 7941
			internal const string FACTORY_COULD_NOT_CREATE_ADAPTER_MSG = "The provider factory failed to create an adapter.";

			// Token: 0x04001F06 RID: 7942
			internal const int FACTORY_COULD_NOT_CREATE_ADAPTER_CODE = 20034;

			// Token: 0x04001F07 RID: 7943
			internal const string FACTORY_COULD_NOT_CREATE_COMMAND_BUILDER_MSG = "The provider factory failed to create a command builder.";

			// Token: 0x04001F08 RID: 7944
			internal const int FACTORY_COULD_NOT_CREATE_COMMAND_BUILDER_CODE = 20035;

			// Token: 0x04001F09 RID: 7945
			internal const string CANNOT_GET_COMMAND_BUILDER_FROM_DBSOURCE_MSG = "Failed to get a command builder for the DbSource.";

			// Token: 0x04001F0A RID: 7946
			internal const int CANNOT_GET_COMMAND_BUILDER_FROM_DBSOURCE_CODE = 20036;
		}
	}
}
