using System;
using System.Globalization;
using System.Resources;
using System.Threading;

namespace System.Data
{
	// Token: 0x0200014A RID: 330
	internal sealed class Res
	{
		// Token: 0x06001356 RID: 4950 RVA: 0x00099FC8 File Offset: 0x000993C8
		internal Res()
		{
			this.resources = new ResourceManager("System.Data", base.GetType().Assembly);
		}

		// Token: 0x06001357 RID: 4951 RVA: 0x00099FF8 File Offset: 0x000993F8
		private static Res GetLoader()
		{
			if (Res.loader == null)
			{
				Res value = new Res();
				Interlocked.CompareExchange<Res>(ref Res.loader, value, null);
			}
			return Res.loader;
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06001358 RID: 4952 RVA: 0x0009A024 File Offset: 0x00099424
		private static CultureInfo Culture
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06001359 RID: 4953 RVA: 0x0009A034 File Offset: 0x00099434
		public static ResourceManager Resources
		{
			get
			{
				return Res.GetLoader().resources;
			}
		}

		// Token: 0x0600135A RID: 4954 RVA: 0x0009A04C File Offset: 0x0009944C
		public static string GetString(string name, params object[] args)
		{
			Res res = Res.GetLoader();
			if (res == null)
			{
				return null;
			}
			string @string = res.resources.GetString(name, Res.Culture);
			if (args != null && args.Length != 0)
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

		// Token: 0x0600135B RID: 4955 RVA: 0x0009A0CC File Offset: 0x000994CC
		public static string GetString(string name)
		{
			Res res = Res.GetLoader();
			if (res == null)
			{
				return null;
			}
			return res.resources.GetString(name, Res.Culture);
		}

		// Token: 0x0600135C RID: 4956 RVA: 0x0009A0F8 File Offset: 0x000994F8
		public static string GetString(string name, out bool usedFallback)
		{
			usedFallback = false;
			return Res.GetString(name);
		}

		// Token: 0x0600135D RID: 4957 RVA: 0x0009A110 File Offset: 0x00099510
		public static object GetObject(string name)
		{
			Res res = Res.GetLoader();
			if (res == null)
			{
				return null;
			}
			return res.resources.GetObject(name, Res.Culture);
		}

		// Token: 0x0400078E RID: 1934
		internal const string ADP_Ascending = "ADP_Ascending";

		// Token: 0x0400078F RID: 1935
		internal const string ADP_CollectionIndexInt32 = "ADP_CollectionIndexInt32";

		// Token: 0x04000790 RID: 1936
		internal const string ADP_CollectionIndexString = "ADP_CollectionIndexString";

		// Token: 0x04000791 RID: 1937
		internal const string ADP_CollectionInvalidType = "ADP_CollectionInvalidType";

		// Token: 0x04000792 RID: 1938
		internal const string ADP_CollectionIsNotParent = "ADP_CollectionIsNotParent";

		// Token: 0x04000793 RID: 1939
		internal const string ADP_CollectionIsParent = "ADP_CollectionIsParent";

		// Token: 0x04000794 RID: 1940
		internal const string ADP_CollectionNullValue = "ADP_CollectionNullValue";

		// Token: 0x04000795 RID: 1941
		internal const string ADP_CollectionRemoveInvalidObject = "ADP_CollectionRemoveInvalidObject";

		// Token: 0x04000796 RID: 1942
		internal const string ADP_CollectionUniqueValue = "ADP_CollectionUniqueValue";

		// Token: 0x04000797 RID: 1943
		internal const string ADP_ConnectionAlreadyOpen = "ADP_ConnectionAlreadyOpen";

		// Token: 0x04000798 RID: 1944
		internal const string ADP_ConnectionStateMsg_Closed = "ADP_ConnectionStateMsg_Closed";

		// Token: 0x04000799 RID: 1945
		internal const string ADP_ConnectionStateMsg_Connecting = "ADP_ConnectionStateMsg_Connecting";

		// Token: 0x0400079A RID: 1946
		internal const string ADP_ConnectionStateMsg_Open = "ADP_ConnectionStateMsg_Open";

		// Token: 0x0400079B RID: 1947
		internal const string ADP_ConnectionStateMsg_OpenExecuting = "ADP_ConnectionStateMsg_OpenExecuting";

		// Token: 0x0400079C RID: 1948
		internal const string ADP_ConnectionStateMsg_OpenFetching = "ADP_ConnectionStateMsg_OpenFetching";

		// Token: 0x0400079D RID: 1949
		internal const string ADP_ConnectionStateMsg = "ADP_ConnectionStateMsg";

		// Token: 0x0400079E RID: 1950
		internal const string ADP_ConnectionStringSyntax = "ADP_ConnectionStringSyntax";

		// Token: 0x0400079F RID: 1951
		internal const string ADP_DataReaderClosed = "ADP_DataReaderClosed";

		// Token: 0x040007A0 RID: 1952
		internal const string ADP_DelegatedTransactionPresent = "ADP_DelegatedTransactionPresent";

		// Token: 0x040007A1 RID: 1953
		internal const string ADP_Descending = "ADP_Descending";

		// Token: 0x040007A2 RID: 1954
		internal const string ADP_EmptyString = "ADP_EmptyString";

		// Token: 0x040007A3 RID: 1955
		internal const string ADP_InternalConnectionError = "ADP_InternalConnectionError";

		// Token: 0x040007A4 RID: 1956
		internal const string ADP_InvalidDataDirectory = "ADP_InvalidDataDirectory";

		// Token: 0x040007A5 RID: 1957
		internal const string ADP_InvalidEnumerationValue = "ADP_InvalidEnumerationValue";

		// Token: 0x040007A6 RID: 1958
		internal const string ADP_InvalidKey = "ADP_InvalidKey";

		// Token: 0x040007A7 RID: 1959
		internal const string ADP_InvalidOffsetValue = "ADP_InvalidOffsetValue";

		// Token: 0x040007A8 RID: 1960
		internal const string ADP_InvalidValue = "ADP_InvalidValue";

		// Token: 0x040007A9 RID: 1961
		internal const string ADP_InvalidXMLBadVersion = "ADP_InvalidXMLBadVersion";

		// Token: 0x040007AA RID: 1962
		internal const string ADP_NoConnectionString = "ADP_NoConnectionString";

		// Token: 0x040007AB RID: 1963
		internal const string ADP_NonCLSException = "ADP_NonCLSException";

		// Token: 0x040007AC RID: 1964
		internal const string ADP_NotAPermissionElement = "ADP_NotAPermissionElement";

		// Token: 0x040007AD RID: 1965
		internal const string ADP_OpenConnectionPropertySet = "ADP_OpenConnectionPropertySet";

		// Token: 0x040007AE RID: 1966
		internal const string ADP_PendingAsyncOperation = "ADP_PendingAsyncOperation";

		// Token: 0x040007AF RID: 1967
		internal const string ADP_PermissionTypeMismatch = "ADP_PermissionTypeMismatch";

		// Token: 0x040007B0 RID: 1968
		internal const string ADP_PooledOpenTimeout = "ADP_PooledOpenTimeout";

		// Token: 0x040007B1 RID: 1969
		internal const string ADP_NonPooledOpenTimeout = "ADP_NonPooledOpenTimeout";

		// Token: 0x040007B2 RID: 1970
		internal const string ADP_InvalidMixedUsageOfSecureAndClearCredential = "ADP_InvalidMixedUsageOfSecureAndClearCredential";

		// Token: 0x040007B3 RID: 1971
		internal const string ADP_InvalidMixedUsageOfSecureCredentialAndIntegratedSecurity = "ADP_InvalidMixedUsageOfSecureCredentialAndIntegratedSecurity";

		// Token: 0x040007B4 RID: 1972
		internal const string ADP_InvalidMixedUsageOfSecureCredentialAndContextConnection = "ADP_InvalidMixedUsageOfSecureCredentialAndContextConnection";

		// Token: 0x040007B5 RID: 1973
		internal const string ADP_InvalidMixedUsageOfAccessTokenAndUserIDPassword = "ADP_InvalidMixedUsageOfAccessTokenAndUserIDPassword";

		// Token: 0x040007B6 RID: 1974
		internal const string ADP_InvalidMixedUsageOfAccessTokenAndIntegratedSecurity = "ADP_InvalidMixedUsageOfAccessTokenAndIntegratedSecurity";

		// Token: 0x040007B7 RID: 1975
		internal const string ADP_InvalidMixedUsageOfAccessTokenAndContextConnection = "ADP_InvalidMixedUsageOfAccessTokenAndContextConnection";

		// Token: 0x040007B8 RID: 1976
		internal const string ADP_InvalidMixedUsageOfAccessTokenAndCredential = "ADP_InvalidMixedUsageOfAccessTokenAndCredential";

		// Token: 0x040007B9 RID: 1977
		internal const string ADP_InvalidMixedUsageOfCredentialAndAccessToken = "ADP_InvalidMixedUsageOfCredentialAndAccessToken";

		// Token: 0x040007BA RID: 1978
		internal const string ADP_InvalidMixedUsageOfAccessTokenAndAuthentication = "ADP_InvalidMixedUsageOfAccessTokenAndAuthentication";

		// Token: 0x040007BB RID: 1979
		internal const string ADP_MustBeReadOnly = "ADP_MustBeReadOnly";

		// Token: 0x040007BC RID: 1980
		internal const string DataCategory_Data = "DataCategory_Data";

		// Token: 0x040007BD RID: 1981
		internal const string DataCategory_StateChange = "DataCategory_StateChange";

		// Token: 0x040007BE RID: 1982
		internal const string DataCategory_Update = "DataCategory_Update";

		// Token: 0x040007BF RID: 1983
		internal const string DbCommand_CommandTimeout = "DbCommand_CommandTimeout";

		// Token: 0x040007C0 RID: 1984
		internal const string DbConnection_State = "DbConnection_State";

		// Token: 0x040007C1 RID: 1985
		internal const string DbConnection_StateChange = "DbConnection_StateChange";

		// Token: 0x040007C2 RID: 1986
		internal const string DbParameter_DbType = "DbParameter_DbType";

		// Token: 0x040007C3 RID: 1987
		internal const string DbParameter_Direction = "DbParameter_Direction";

		// Token: 0x040007C4 RID: 1988
		internal const string DbParameter_IsNullable = "DbParameter_IsNullable";

		// Token: 0x040007C5 RID: 1989
		internal const string DbParameter_Offset = "DbParameter_Offset";

		// Token: 0x040007C6 RID: 1990
		internal const string DbParameter_ParameterName = "DbParameter_ParameterName";

		// Token: 0x040007C7 RID: 1991
		internal const string DbParameter_Size = "DbParameter_Size";

		// Token: 0x040007C8 RID: 1992
		internal const string DbParameter_SourceColumn = "DbParameter_SourceColumn";

		// Token: 0x040007C9 RID: 1993
		internal const string DbParameter_SourceVersion = "DbParameter_SourceVersion";

		// Token: 0x040007CA RID: 1994
		internal const string DbParameter_SourceColumnNullMapping = "DbParameter_SourceColumnNullMapping";

		// Token: 0x040007CB RID: 1995
		internal const string DbParameter_Value = "DbParameter_Value";

		// Token: 0x040007CC RID: 1996
		internal const string MDF_QueryFailed = "MDF_QueryFailed";

		// Token: 0x040007CD RID: 1997
		internal const string MDF_TooManyRestrictions = "MDF_TooManyRestrictions";

		// Token: 0x040007CE RID: 1998
		internal const string MDF_InvalidRestrictionValue = "MDF_InvalidRestrictionValue";

		// Token: 0x040007CF RID: 1999
		internal const string MDF_UndefinedCollection = "MDF_UndefinedCollection";

		// Token: 0x040007D0 RID: 2000
		internal const string MDF_UndefinedPopulationMechanism = "MDF_UndefinedPopulationMechanism";

		// Token: 0x040007D1 RID: 2001
		internal const string MDF_UnsupportedVersion = "MDF_UnsupportedVersion";

		// Token: 0x040007D2 RID: 2002
		internal const string MDF_MissingDataSourceInformationColumn = "MDF_MissingDataSourceInformationColumn";

		// Token: 0x040007D3 RID: 2003
		internal const string MDF_IncorrectNumberOfDataSourceInformationRows = "MDF_IncorrectNumberOfDataSourceInformationRows";

		// Token: 0x040007D4 RID: 2004
		internal const string MDF_MissingRestrictionColumn = "MDF_MissingRestrictionColumn";

		// Token: 0x040007D5 RID: 2005
		internal const string MDF_MissingRestrictionRow = "MDF_MissingRestrictionRow";

		// Token: 0x040007D6 RID: 2006
		internal const string MDF_NoColumns = "MDF_NoColumns";

		// Token: 0x040007D7 RID: 2007
		internal const string MDF_UnableToBuildCollection = "MDF_UnableToBuildCollection";

		// Token: 0x040007D8 RID: 2008
		internal const string MDF_AmbigousCollectionName = "MDF_AmbigousCollectionName";

		// Token: 0x040007D9 RID: 2009
		internal const string MDF_CollectionNameISNotUnique = "MDF_CollectionNameISNotUnique";

		// Token: 0x040007DA RID: 2010
		internal const string MDF_DataTableDoesNotExist = "MDF_DataTableDoesNotExist";

		// Token: 0x040007DB RID: 2011
		internal const string MDF_InvalidXml = "MDF_InvalidXml";

		// Token: 0x040007DC RID: 2012
		internal const string MDF_InvalidXmlMissingColumn = "MDF_InvalidXmlMissingColumn";

		// Token: 0x040007DD RID: 2013
		internal const string MDF_InvalidXmlInvalidValue = "MDF_InvalidXmlInvalidValue";

		// Token: 0x040007DE RID: 2014
		internal const string DataCategory_Action = "DataCategory_Action";

		// Token: 0x040007DF RID: 2015
		internal const string DataCategory_Behavior = "DataCategory_Behavior";

		// Token: 0x040007E0 RID: 2016
		internal const string DataCategory_Fill = "DataCategory_Fill";

		// Token: 0x040007E1 RID: 2017
		internal const string DataCategory_InfoMessage = "DataCategory_InfoMessage";

		// Token: 0x040007E2 RID: 2018
		internal const string DataCategory_Mapping = "DataCategory_Mapping";

		// Token: 0x040007E3 RID: 2019
		internal const string DataCategory_StatementCompleted = "DataCategory_StatementCompleted";

		// Token: 0x040007E4 RID: 2020
		internal const string DataCategory_Udt = "DataCategory_Udt";

		// Token: 0x040007E5 RID: 2021
		internal const string DataCategory_Notification = "DataCategory_Notification";

		// Token: 0x040007E6 RID: 2022
		internal const string DataCategory_Schema = "DataCategory_Schema";

		// Token: 0x040007E7 RID: 2023
		internal const string DataCategory_Xml = "DataCategory_Xml";

		// Token: 0x040007E8 RID: 2024
		internal const string DataCategory_Advanced = "DataCategory_Advanced";

		// Token: 0x040007E9 RID: 2025
		internal const string DataCategory_Context = "DataCategory_Context";

		// Token: 0x040007EA RID: 2026
		internal const string DataCategory_Initialization = "DataCategory_Initialization";

		// Token: 0x040007EB RID: 2027
		internal const string DataCategory_Pooling = "DataCategory_Pooling";

		// Token: 0x040007EC RID: 2028
		internal const string DataCategory_NamedConnectionString = "DataCategory_NamedConnectionString";

		// Token: 0x040007ED RID: 2029
		internal const string DataCategory_Security = "DataCategory_Security";

		// Token: 0x040007EE RID: 2030
		internal const string DataCategory_Source = "DataCategory_Source";

		// Token: 0x040007EF RID: 2031
		internal const string DataCategory_Replication = "DataCategory_Replication";

		// Token: 0x040007F0 RID: 2032
		internal const string DataCategory_ConnectionResilency = "DataCategory_ConnectionResilency";

		// Token: 0x040007F1 RID: 2033
		internal const string ExtendedPropertiesDescr = "ExtendedPropertiesDescr";

		// Token: 0x040007F2 RID: 2034
		internal const string DataSetCaseSensitiveDescr = "DataSetCaseSensitiveDescr";

		// Token: 0x040007F3 RID: 2035
		internal const string DataSetDataSetNameDescr = "DataSetDataSetNameDescr";

		// Token: 0x040007F4 RID: 2036
		internal const string DataSetDefaultViewDescr = "DataSetDefaultViewDescr";

		// Token: 0x040007F5 RID: 2037
		internal const string DataSetEnforceConstraintsDescr = "DataSetEnforceConstraintsDescr";

		// Token: 0x040007F6 RID: 2038
		internal const string DataSetHasErrorsDescr = "DataSetHasErrorsDescr";

		// Token: 0x040007F7 RID: 2039
		internal const string DataSetLocaleDescr = "DataSetLocaleDescr";

		// Token: 0x040007F8 RID: 2040
		internal const string DataSetNamespaceDescr = "DataSetNamespaceDescr";

		// Token: 0x040007F9 RID: 2041
		internal const string DataSetPrefixDescr = "DataSetPrefixDescr";

		// Token: 0x040007FA RID: 2042
		internal const string DataSetRelationsDescr = "DataSetRelationsDescr";

		// Token: 0x040007FB RID: 2043
		internal const string DataSetTablesDescr = "DataSetTablesDescr";

		// Token: 0x040007FC RID: 2044
		internal const string DataSetMergeFailedDescr = "DataSetMergeFailedDescr";

		// Token: 0x040007FD RID: 2045
		internal const string DataSetInitializedDescr = "DataSetInitializedDescr";

		// Token: 0x040007FE RID: 2046
		internal const string DataSetDescr = "DataSetDescr";

		// Token: 0x040007FF RID: 2047
		internal const string DataTableCaseSensitiveDescr = "DataTableCaseSensitiveDescr";

		// Token: 0x04000800 RID: 2048
		internal const string DataTableChildRelationsDescr = "DataTableChildRelationsDescr";

		// Token: 0x04000801 RID: 2049
		internal const string DataTableColumnsDescr = "DataTableColumnsDescr";

		// Token: 0x04000802 RID: 2050
		internal const string DataTableConstraintsDescr = "DataTableConstraintsDescr";

		// Token: 0x04000803 RID: 2051
		internal const string DataTableDataSetDescr = "DataTableDataSetDescr";

		// Token: 0x04000804 RID: 2052
		internal const string DataTableDefaultViewDescr = "DataTableDefaultViewDescr";

		// Token: 0x04000805 RID: 2053
		internal const string DataTableDisplayExpressionDescr = "DataTableDisplayExpressionDescr";

		// Token: 0x04000806 RID: 2054
		internal const string DataTableHasErrorsDescr = "DataTableHasErrorsDescr";

		// Token: 0x04000807 RID: 2055
		internal const string DataTableLocaleDescr = "DataTableLocaleDescr";

		// Token: 0x04000808 RID: 2056
		internal const string DataTableMinimumCapacityDescr = "DataTableMinimumCapacityDescr";

		// Token: 0x04000809 RID: 2057
		internal const string DataTableNamespaceDescr = "DataTableNamespaceDescr";

		// Token: 0x0400080A RID: 2058
		internal const string DataTablePrefixDescr = "DataTablePrefixDescr";

		// Token: 0x0400080B RID: 2059
		internal const string DataTableParentRelationsDescr = "DataTableParentRelationsDescr";

		// Token: 0x0400080C RID: 2060
		internal const string DataTablePrimaryKeyDescr = "DataTablePrimaryKeyDescr";

		// Token: 0x0400080D RID: 2061
		internal const string DataTableRowsDescr = "DataTableRowsDescr";

		// Token: 0x0400080E RID: 2062
		internal const string DataTableTableNameDescr = "DataTableTableNameDescr";

		// Token: 0x0400080F RID: 2063
		internal const string DataTableRowChangedDescr = "DataTableRowChangedDescr";

		// Token: 0x04000810 RID: 2064
		internal const string DataTableRowChangingDescr = "DataTableRowChangingDescr";

		// Token: 0x04000811 RID: 2065
		internal const string DataTableRowDeletedDescr = "DataTableRowDeletedDescr";

		// Token: 0x04000812 RID: 2066
		internal const string DataTableRowDeletingDescr = "DataTableRowDeletingDescr";

		// Token: 0x04000813 RID: 2067
		internal const string DataTableColumnChangingDescr = "DataTableColumnChangingDescr";

		// Token: 0x04000814 RID: 2068
		internal const string DataTableColumnChangedDescr = "DataTableColumnChangedDescr";

		// Token: 0x04000815 RID: 2069
		internal const string DataTableRowsClearingDescr = "DataTableRowsClearingDescr";

		// Token: 0x04000816 RID: 2070
		internal const string DataTableRowsClearedDescr = "DataTableRowsClearedDescr";

		// Token: 0x04000817 RID: 2071
		internal const string DataTableRowsNewRowDescr = "DataTableRowsNewRowDescr";

		// Token: 0x04000818 RID: 2072
		internal const string DataRelationRelationNameDescr = "DataRelationRelationNameDescr";

		// Token: 0x04000819 RID: 2073
		internal const string DataRelationChildColumnsDescr = "DataRelationChildColumnsDescr";

		// Token: 0x0400081A RID: 2074
		internal const string DataRelationParentColumnsDescr = "DataRelationParentColumnsDescr";

		// Token: 0x0400081B RID: 2075
		internal const string DataRelationNested = "DataRelationNested";

		// Token: 0x0400081C RID: 2076
		internal const string ForeignKeyConstraintDeleteRuleDescr = "ForeignKeyConstraintDeleteRuleDescr";

		// Token: 0x0400081D RID: 2077
		internal const string ForeignKeyConstraintUpdateRuleDescr = "ForeignKeyConstraintUpdateRuleDescr";

		// Token: 0x0400081E RID: 2078
		internal const string ForeignKeyConstraintAcceptRejectRuleDescr = "ForeignKeyConstraintAcceptRejectRuleDescr";

		// Token: 0x0400081F RID: 2079
		internal const string ForeignKeyConstraintChildColumnsDescr = "ForeignKeyConstraintChildColumnsDescr";

		// Token: 0x04000820 RID: 2080
		internal const string ForeignKeyConstraintParentColumnsDescr = "ForeignKeyConstraintParentColumnsDescr";

		// Token: 0x04000821 RID: 2081
		internal const string ForeignKeyRelatedTableDescr = "ForeignKeyRelatedTableDescr";

		// Token: 0x04000822 RID: 2082
		internal const string KeyConstraintColumnsDescr = "KeyConstraintColumnsDescr";

		// Token: 0x04000823 RID: 2083
		internal const string KeyConstraintIsPrimaryKeyDescr = "KeyConstraintIsPrimaryKeyDescr";

		// Token: 0x04000824 RID: 2084
		internal const string ConstraintNameDescr = "ConstraintNameDescr";

		// Token: 0x04000825 RID: 2085
		internal const string ConstraintTableDescr = "ConstraintTableDescr";

		// Token: 0x04000826 RID: 2086
		internal const string DataColumnAllowNullDescr = "DataColumnAllowNullDescr";

		// Token: 0x04000827 RID: 2087
		internal const string DataColumnAutoIncrementDescr = "DataColumnAutoIncrementDescr";

		// Token: 0x04000828 RID: 2088
		internal const string DataColumnAutoIncrementSeedDescr = "DataColumnAutoIncrementSeedDescr";

		// Token: 0x04000829 RID: 2089
		internal const string DataColumnAutoIncrementStepDescr = "DataColumnAutoIncrementStepDescr";

		// Token: 0x0400082A RID: 2090
		internal const string DataColumnCaptionDescr = "DataColumnCaptionDescr";

		// Token: 0x0400082B RID: 2091
		internal const string DataColumnColumnNameDescr = "DataColumnColumnNameDescr";

		// Token: 0x0400082C RID: 2092
		internal const string DataColumnDataTableDescr = "DataColumnDataTableDescr";

		// Token: 0x0400082D RID: 2093
		internal const string DataColumnDataTypeDescr = "DataColumnDataTypeDescr";

		// Token: 0x0400082E RID: 2094
		internal const string DataColumnDefaultValueDescr = "DataColumnDefaultValueDescr";

		// Token: 0x0400082F RID: 2095
		internal const string DataColumnExpressionDescr = "DataColumnExpressionDescr";

		// Token: 0x04000830 RID: 2096
		internal const string DataColumnMappingDescr = "DataColumnMappingDescr";

		// Token: 0x04000831 RID: 2097
		internal const string DataColumnNamespaceDescr = "DataColumnNamespaceDescr";

		// Token: 0x04000832 RID: 2098
		internal const string DataColumnPrefixDescr = "DataColumnPrefixDescr";

		// Token: 0x04000833 RID: 2099
		internal const string DataColumnOrdinalDescr = "DataColumnOrdinalDescr";

		// Token: 0x04000834 RID: 2100
		internal const string DataColumnReadOnlyDescr = "DataColumnReadOnlyDescr";

		// Token: 0x04000835 RID: 2101
		internal const string DataColumnUniqueDescr = "DataColumnUniqueDescr";

		// Token: 0x04000836 RID: 2102
		internal const string DataColumnMaxLengthDescr = "DataColumnMaxLengthDescr";

		// Token: 0x04000837 RID: 2103
		internal const string DataColumnDateTimeModeDescr = "DataColumnDateTimeModeDescr";

		// Token: 0x04000838 RID: 2104
		internal const string DataViewAllowDeleteDescr = "DataViewAllowDeleteDescr";

		// Token: 0x04000839 RID: 2105
		internal const string DataViewAllowEditDescr = "DataViewAllowEditDescr";

		// Token: 0x0400083A RID: 2106
		internal const string DataViewAllowNewDescr = "DataViewAllowNewDescr";

		// Token: 0x0400083B RID: 2107
		internal const string DataViewCountDescr = "DataViewCountDescr";

		// Token: 0x0400083C RID: 2108
		internal const string DataViewDataViewManagerDescr = "DataViewDataViewManagerDescr";

		// Token: 0x0400083D RID: 2109
		internal const string DataViewIsOpenDescr = "DataViewIsOpenDescr";

		// Token: 0x0400083E RID: 2110
		internal const string DataViewRowFilterDescr = "DataViewRowFilterDescr";

		// Token: 0x0400083F RID: 2111
		internal const string DataViewRowStateFilterDescr = "DataViewRowStateFilterDescr";

		// Token: 0x04000840 RID: 2112
		internal const string DataViewSortDescr = "DataViewSortDescr";

		// Token: 0x04000841 RID: 2113
		internal const string DataViewApplyDefaultSortDescr = "DataViewApplyDefaultSortDescr";

		// Token: 0x04000842 RID: 2114
		internal const string DataViewTableDescr = "DataViewTableDescr";

		// Token: 0x04000843 RID: 2115
		internal const string DataViewListChangedDescr = "DataViewListChangedDescr";

		// Token: 0x04000844 RID: 2116
		internal const string DataViewManagerDataSetDescr = "DataViewManagerDataSetDescr";

		// Token: 0x04000845 RID: 2117
		internal const string DataViewManagerTableSettingsDescr = "DataViewManagerTableSettingsDescr";

		// Token: 0x04000846 RID: 2118
		internal const string Xml_SimpleTypeNotSupported = "Xml_SimpleTypeNotSupported";

		// Token: 0x04000847 RID: 2119
		internal const string Xml_MissingAttribute = "Xml_MissingAttribute";

		// Token: 0x04000848 RID: 2120
		internal const string Xml_ValueOutOfRange = "Xml_ValueOutOfRange";

		// Token: 0x04000849 RID: 2121
		internal const string Xml_AttributeValues = "Xml_AttributeValues";

		// Token: 0x0400084A RID: 2122
		internal const string Xml_ElementTypeNotFound = "Xml_ElementTypeNotFound";

		// Token: 0x0400084B RID: 2123
		internal const string Xml_RelationParentNameMissing = "Xml_RelationParentNameMissing";

		// Token: 0x0400084C RID: 2124
		internal const string Xml_RelationChildNameMissing = "Xml_RelationChildNameMissing";

		// Token: 0x0400084D RID: 2125
		internal const string Xml_RelationTableKeyMissing = "Xml_RelationTableKeyMissing";

		// Token: 0x0400084E RID: 2126
		internal const string Xml_RelationChildKeyMissing = "Xml_RelationChildKeyMissing";

		// Token: 0x0400084F RID: 2127
		internal const string Xml_UndefinedDatatype = "Xml_UndefinedDatatype";

		// Token: 0x04000850 RID: 2128
		internal const string Xml_DatatypeNotDefined = "Xml_DatatypeNotDefined";

		// Token: 0x04000851 RID: 2129
		internal const string Xml_InvalidField = "Xml_InvalidField";

		// Token: 0x04000852 RID: 2130
		internal const string Xml_InvalidSelector = "Xml_InvalidSelector";

		// Token: 0x04000853 RID: 2131
		internal const string Xml_InvalidKey = "Xml_InvalidKey";

		// Token: 0x04000854 RID: 2132
		internal const string Xml_DuplicateConstraint = "Xml_DuplicateConstraint";

		// Token: 0x04000855 RID: 2133
		internal const string Xml_CannotConvert = "Xml_CannotConvert";

		// Token: 0x04000856 RID: 2134
		internal const string Xml_MissingRefer = "Xml_MissingRefer";

		// Token: 0x04000857 RID: 2135
		internal const string Xml_MismatchKeyLength = "Xml_MismatchKeyLength";

		// Token: 0x04000858 RID: 2136
		internal const string Xml_CircularComplexType = "Xml_CircularComplexType";

		// Token: 0x04000859 RID: 2137
		internal const string Xml_CannotInstantiateAbstract = "Xml_CannotInstantiateAbstract";

		// Token: 0x0400085A RID: 2138
		internal const string Xml_MultipleTargetConverterError = "Xml_MultipleTargetConverterError";

		// Token: 0x0400085B RID: 2139
		internal const string Xml_MultipleTargetConverterEmpty = "Xml_MultipleTargetConverterEmpty";

		// Token: 0x0400085C RID: 2140
		internal const string Xml_MergeDuplicateDeclaration = "Xml_MergeDuplicateDeclaration";

		// Token: 0x0400085D RID: 2141
		internal const string Xml_MissingTable = "Xml_MissingTable";

		// Token: 0x0400085E RID: 2142
		internal const string Xml_MissingSQL = "Xml_MissingSQL";

		// Token: 0x0400085F RID: 2143
		internal const string Xml_ColumnConflict = "Xml_ColumnConflict";

		// Token: 0x04000860 RID: 2144
		internal const string Xml_InvalidPrefix = "Xml_InvalidPrefix";

		// Token: 0x04000861 RID: 2145
		internal const string Xml_NestedCircular = "Xml_NestedCircular";

		// Token: 0x04000862 RID: 2146
		internal const string Xml_FoundEntity = "Xml_FoundEntity";

		// Token: 0x04000863 RID: 2147
		internal const string Xml_PolymorphismNotSupported = "Xml_PolymorphismNotSupported";

		// Token: 0x04000864 RID: 2148
		internal const string Xml_CanNotDeserializeObjectType = "Xml_CanNotDeserializeObjectType";

		// Token: 0x04000865 RID: 2149
		internal const string Xml_DataTableInferenceNotSupported = "Xml_DataTableInferenceNotSupported";

		// Token: 0x04000866 RID: 2150
		internal const string Xml_MultipleParentRows = "Xml_MultipleParentRows";

		// Token: 0x04000867 RID: 2151
		internal const string Xml_IsDataSetAttributeMissingInSchema = "Xml_IsDataSetAttributeMissingInSchema";

		// Token: 0x04000868 RID: 2152
		internal const string Xml_TooManyIsDataSetAtributeInSchema = "Xml_TooManyIsDataSetAtributeInSchema";

		// Token: 0x04000869 RID: 2153
		internal const string Xml_DynamicWithoutXmlSerializable = "Xml_DynamicWithoutXmlSerializable";

		// Token: 0x0400086A RID: 2154
		internal const string Expr_NYI = "Expr_NYI";

		// Token: 0x0400086B RID: 2155
		internal const string Expr_MissingOperand = "Expr_MissingOperand";

		// Token: 0x0400086C RID: 2156
		internal const string Expr_TypeMismatch = "Expr_TypeMismatch";

		// Token: 0x0400086D RID: 2157
		internal const string Expr_ExpressionTooComplex = "Expr_ExpressionTooComplex";

		// Token: 0x0400086E RID: 2158
		internal const string Expr_UnboundName = "Expr_UnboundName";

		// Token: 0x0400086F RID: 2159
		internal const string Expr_InvalidString = "Expr_InvalidString";

		// Token: 0x04000870 RID: 2160
		internal const string Expr_UndefinedFunction = "Expr_UndefinedFunction";

		// Token: 0x04000871 RID: 2161
		internal const string Expr_Syntax = "Expr_Syntax";

		// Token: 0x04000872 RID: 2162
		internal const string Expr_FunctionArgumentCount = "Expr_FunctionArgumentCount";

		// Token: 0x04000873 RID: 2163
		internal const string Expr_MissingRightParen = "Expr_MissingRightParen";

		// Token: 0x04000874 RID: 2164
		internal const string Expr_UnknownToken = "Expr_UnknownToken";

		// Token: 0x04000875 RID: 2165
		internal const string Expr_UnknownToken1 = "Expr_UnknownToken1";

		// Token: 0x04000876 RID: 2166
		internal const string Expr_DatatypeConvertion = "Expr_DatatypeConvertion";

		// Token: 0x04000877 RID: 2167
		internal const string Expr_DatavalueConvertion = "Expr_DatavalueConvertion";

		// Token: 0x04000878 RID: 2168
		internal const string Expr_InvalidName = "Expr_InvalidName";

		// Token: 0x04000879 RID: 2169
		internal const string Expr_InvalidDate = "Expr_InvalidDate";

		// Token: 0x0400087A RID: 2170
		internal const string Expr_NonConstantArgument = "Expr_NonConstantArgument";

		// Token: 0x0400087B RID: 2171
		internal const string Expr_InvalidPattern = "Expr_InvalidPattern";

		// Token: 0x0400087C RID: 2172
		internal const string Expr_InWithoutParentheses = "Expr_InWithoutParentheses";

		// Token: 0x0400087D RID: 2173
		internal const string Expr_ArgumentType = "Expr_ArgumentType";

		// Token: 0x0400087E RID: 2174
		internal const string Expr_ArgumentTypeInteger = "Expr_ArgumentTypeInteger";

		// Token: 0x0400087F RID: 2175
		internal const string Expr_TypeMismatchInBinop = "Expr_TypeMismatchInBinop";

		// Token: 0x04000880 RID: 2176
		internal const string Expr_AmbiguousBinop = "Expr_AmbiguousBinop";

		// Token: 0x04000881 RID: 2177
		internal const string Expr_InWithoutList = "Expr_InWithoutList";

		// Token: 0x04000882 RID: 2178
		internal const string Expr_UnsupportedOperator = "Expr_UnsupportedOperator";

		// Token: 0x04000883 RID: 2179
		internal const string Expr_InvalidNameBracketing = "Expr_InvalidNameBracketing";

		// Token: 0x04000884 RID: 2180
		internal const string Expr_MissingOperandBefore = "Expr_MissingOperandBefore";

		// Token: 0x04000885 RID: 2181
		internal const string Expr_TooManyRightParentheses = "Expr_TooManyRightParentheses";

		// Token: 0x04000886 RID: 2182
		internal const string Expr_UnresolvedRelation = "Expr_UnresolvedRelation";

		// Token: 0x04000887 RID: 2183
		internal const string Expr_AggregateArgument = "Expr_AggregateArgument";

		// Token: 0x04000888 RID: 2184
		internal const string Expr_AggregateUnbound = "Expr_AggregateUnbound";

		// Token: 0x04000889 RID: 2185
		internal const string Expr_EvalNoContext = "Expr_EvalNoContext";

		// Token: 0x0400088A RID: 2186
		internal const string Expr_ExpressionUnbound = "Expr_ExpressionUnbound";

		// Token: 0x0400088B RID: 2187
		internal const string Expr_ComputeNotAggregate = "Expr_ComputeNotAggregate";

		// Token: 0x0400088C RID: 2188
		internal const string Expr_FilterConvertion = "Expr_FilterConvertion";

		// Token: 0x0400088D RID: 2189
		internal const string Expr_InvalidType = "Expr_InvalidType";

		// Token: 0x0400088E RID: 2190
		internal const string Expr_LookupArgument = "Expr_LookupArgument";

		// Token: 0x0400088F RID: 2191
		internal const string Expr_InvokeArgument = "Expr_InvokeArgument";

		// Token: 0x04000890 RID: 2192
		internal const string Expr_ArgumentOutofRange = "Expr_ArgumentOutofRange";

		// Token: 0x04000891 RID: 2193
		internal const string Expr_IsSyntax = "Expr_IsSyntax";

		// Token: 0x04000892 RID: 2194
		internal const string Expr_Overflow = "Expr_Overflow";

		// Token: 0x04000893 RID: 2195
		internal const string Expr_DivideByZero = "Expr_DivideByZero";

		// Token: 0x04000894 RID: 2196
		internal const string Expr_BindFailure = "Expr_BindFailure";

		// Token: 0x04000895 RID: 2197
		internal const string Expr_InvalidHoursArgument = "Expr_InvalidHoursArgument";

		// Token: 0x04000896 RID: 2198
		internal const string Expr_InvalidMinutesArgument = "Expr_InvalidMinutesArgument";

		// Token: 0x04000897 RID: 2199
		internal const string Expr_InvalidTimeZoneRange = "Expr_InvalidTimeZoneRange";

		// Token: 0x04000898 RID: 2200
		internal const string Expr_MismatchKindandTimeSpan = "Expr_MismatchKindandTimeSpan";

		// Token: 0x04000899 RID: 2201
		internal const string Expr_UnsupportedType = "Expr_UnsupportedType";

		// Token: 0x0400089A RID: 2202
		internal const string Data_EnforceConstraints = "Data_EnforceConstraints";

		// Token: 0x0400089B RID: 2203
		internal const string Data_CannotModifyCollection = "Data_CannotModifyCollection";

		// Token: 0x0400089C RID: 2204
		internal const string Data_CaseInsensitiveNameConflict = "Data_CaseInsensitiveNameConflict";

		// Token: 0x0400089D RID: 2205
		internal const string Data_NamespaceNameConflict = "Data_NamespaceNameConflict";

		// Token: 0x0400089E RID: 2206
		internal const string Data_InvalidOffsetLength = "Data_InvalidOffsetLength";

		// Token: 0x0400089F RID: 2207
		internal const string Data_ArgumentOutOfRange = "Data_ArgumentOutOfRange";

		// Token: 0x040008A0 RID: 2208
		internal const string Data_ArgumentNull = "Data_ArgumentNull";

		// Token: 0x040008A1 RID: 2209
		internal const string Data_ArgumentContainsNull = "Data_ArgumentContainsNull";

		// Token: 0x040008A2 RID: 2210
		internal const string Data_TypeNotAllowed = "Data_TypeNotAllowed";

		// Token: 0x040008A3 RID: 2211
		internal const string Config_ElementNotAllowed = "Config_ElementNotAllowed";

		// Token: 0x040008A4 RID: 2212
		internal const string DataColumns_OutOfRange = "DataColumns_OutOfRange";

		// Token: 0x040008A5 RID: 2213
		internal const string DataColumns_Add1 = "DataColumns_Add1";

		// Token: 0x040008A6 RID: 2214
		internal const string DataColumns_Add2 = "DataColumns_Add2";

		// Token: 0x040008A7 RID: 2215
		internal const string DataColumns_Add3 = "DataColumns_Add3";

		// Token: 0x040008A8 RID: 2216
		internal const string DataColumns_Add4 = "DataColumns_Add4";

		// Token: 0x040008A9 RID: 2217
		internal const string DataColumns_AddDuplicate = "DataColumns_AddDuplicate";

		// Token: 0x040008AA RID: 2218
		internal const string DataColumns_AddDuplicate2 = "DataColumns_AddDuplicate2";

		// Token: 0x040008AB RID: 2219
		internal const string DataColumns_AddDuplicate3 = "DataColumns_AddDuplicate3";

		// Token: 0x040008AC RID: 2220
		internal const string DataColumns_Remove = "DataColumns_Remove";

		// Token: 0x040008AD RID: 2221
		internal const string DataColumns_RemovePrimaryKey = "DataColumns_RemovePrimaryKey";

		// Token: 0x040008AE RID: 2222
		internal const string DataColumns_RemoveChildKey = "DataColumns_RemoveChildKey";

		// Token: 0x040008AF RID: 2223
		internal const string DataColumns_RemoveConstraint = "DataColumns_RemoveConstraint";

		// Token: 0x040008B0 RID: 2224
		internal const string DataColumns_RemoveExpression = "DataColumns_RemoveExpression";

		// Token: 0x040008B1 RID: 2225
		internal const string DataColumn_AutoIncrementAndExpression = "DataColumn_AutoIncrementAndExpression";

		// Token: 0x040008B2 RID: 2226
		internal const string DataColumn_AutoIncrementAndDefaultValue = "DataColumn_AutoIncrementAndDefaultValue";

		// Token: 0x040008B3 RID: 2227
		internal const string DataColumn_DefaultValueAndAutoIncrement = "DataColumn_DefaultValueAndAutoIncrement";

		// Token: 0x040008B4 RID: 2228
		internal const string DataColumn_AutoIncrementSeed = "DataColumn_AutoIncrementSeed";

		// Token: 0x040008B5 RID: 2229
		internal const string DataColumn_NameRequired = "DataColumn_NameRequired";

		// Token: 0x040008B6 RID: 2230
		internal const string DataColumn_ChangeDataType = "DataColumn_ChangeDataType";

		// Token: 0x040008B7 RID: 2231
		internal const string DataColumn_NullDataType = "DataColumn_NullDataType";

		// Token: 0x040008B8 RID: 2232
		internal const string DataColumn_DefaultValueDataType = "DataColumn_DefaultValueDataType";

		// Token: 0x040008B9 RID: 2233
		internal const string DataColumn_DefaultValueDataType1 = "DataColumn_DefaultValueDataType1";

		// Token: 0x040008BA RID: 2234
		internal const string DataColumn_DefaultValueColumnDataType = "DataColumn_DefaultValueColumnDataType";

		// Token: 0x040008BB RID: 2235
		internal const string DataColumn_ReadOnlyAndExpression = "DataColumn_ReadOnlyAndExpression";

		// Token: 0x040008BC RID: 2236
		internal const string DataColumn_UniqueAndExpression = "DataColumn_UniqueAndExpression";

		// Token: 0x040008BD RID: 2237
		internal const string DataColumn_ExpressionAndUnique = "DataColumn_ExpressionAndUnique";

		// Token: 0x040008BE RID: 2238
		internal const string DataColumn_ExpressionAndReadOnly = "DataColumn_ExpressionAndReadOnly";

		// Token: 0x040008BF RID: 2239
		internal const string DataColumn_ExpressionAndConstraint = "DataColumn_ExpressionAndConstraint";

		// Token: 0x040008C0 RID: 2240
		internal const string DataColumn_ExpressionInConstraint = "DataColumn_ExpressionInConstraint";

		// Token: 0x040008C1 RID: 2241
		internal const string DataColumn_ExpressionCircular = "DataColumn_ExpressionCircular";

		// Token: 0x040008C2 RID: 2242
		internal const string DataColumn_NullKeyValues = "DataColumn_NullKeyValues";

		// Token: 0x040008C3 RID: 2243
		internal const string DataColumn_NullValues = "DataColumn_NullValues";

		// Token: 0x040008C4 RID: 2244
		internal const string DataColumn_ReadOnly = "DataColumn_ReadOnly";

		// Token: 0x040008C5 RID: 2245
		internal const string DataColumn_NonUniqueValues = "DataColumn_NonUniqueValues";

		// Token: 0x040008C6 RID: 2246
		internal const string DataColumn_NotInTheTable = "DataColumn_NotInTheTable";

		// Token: 0x040008C7 RID: 2247
		internal const string DataColumn_NotInAnyTable = "DataColumn_NotInAnyTable";

		// Token: 0x040008C8 RID: 2248
		internal const string DataColumn_SetFailed = "DataColumn_SetFailed";

		// Token: 0x040008C9 RID: 2249
		internal const string DataColumn_CannotSetToNull = "DataColumn_CannotSetToNull";

		// Token: 0x040008CA RID: 2250
		internal const string DataColumn_LongerThanMaxLength = "DataColumn_LongerThanMaxLength";

		// Token: 0x040008CB RID: 2251
		internal const string DataColumn_HasToBeStringType = "DataColumn_HasToBeStringType";

		// Token: 0x040008CC RID: 2252
		internal const string DataColumn_CannotSetMaxLength = "DataColumn_CannotSetMaxLength";

		// Token: 0x040008CD RID: 2253
		internal const string DataColumn_CannotSetMaxLength2 = "DataColumn_CannotSetMaxLength2";

		// Token: 0x040008CE RID: 2254
		internal const string DataColumn_CannotSimpleContentType = "DataColumn_CannotSimpleContentType";

		// Token: 0x040008CF RID: 2255
		internal const string DataColumn_CannotSimpleContent = "DataColumn_CannotSimpleContent";

		// Token: 0x040008D0 RID: 2256
		internal const string DataColumn_ExceedMaxLength = "DataColumn_ExceedMaxLength";

		// Token: 0x040008D1 RID: 2257
		internal const string DataColumn_NotAllowDBNull = "DataColumn_NotAllowDBNull";

		// Token: 0x040008D2 RID: 2258
		internal const string DataColumn_CannotChangeNamespace = "DataColumn_CannotChangeNamespace";

		// Token: 0x040008D3 RID: 2259
		internal const string DataColumn_AutoIncrementCannotSetIfHasData = "DataColumn_AutoIncrementCannotSetIfHasData";

		// Token: 0x040008D4 RID: 2260
		internal const string DataColumn_NotInTheUnderlyingTable = "DataColumn_NotInTheUnderlyingTable";

		// Token: 0x040008D5 RID: 2261
		internal const string DataColumn_InvalidDataColumnMapping = "DataColumn_InvalidDataColumnMapping";

		// Token: 0x040008D6 RID: 2262
		internal const string DataColumn_CannotSetDateTimeModeForNonDateTimeColumns = "DataColumn_CannotSetDateTimeModeForNonDateTimeColumns";

		// Token: 0x040008D7 RID: 2263
		internal const string DataColumn_InvalidDateTimeMode = "DataColumn_InvalidDateTimeMode";

		// Token: 0x040008D8 RID: 2264
		internal const string DataColumn_DateTimeMode = "DataColumn_DateTimeMode";

		// Token: 0x040008D9 RID: 2265
		internal const string DataColumn_INullableUDTwithoutStaticNull = "DataColumn_INullableUDTwithoutStaticNull";

		// Token: 0x040008DA RID: 2266
		internal const string DataColumn_UDTImplementsIChangeTrackingButnotIRevertible = "DataColumn_UDTImplementsIChangeTrackingButnotIRevertible";

		// Token: 0x040008DB RID: 2267
		internal const string DataColumn_SetAddedAndModifiedCalledOnNonUnchanged = "DataColumn_SetAddedAndModifiedCalledOnNonUnchanged";

		// Token: 0x040008DC RID: 2268
		internal const string DataColumn_OrdinalExceedMaximun = "DataColumn_OrdinalExceedMaximun";

		// Token: 0x040008DD RID: 2269
		internal const string DataColumn_NullableTypesNotSupported = "DataColumn_NullableTypesNotSupported";

		// Token: 0x040008DE RID: 2270
		internal const string DataConstraint_NoName = "DataConstraint_NoName";

		// Token: 0x040008DF RID: 2271
		internal const string DataConstraint_Violation = "DataConstraint_Violation";

		// Token: 0x040008E0 RID: 2272
		internal const string DataConstraint_ViolationValue = "DataConstraint_ViolationValue";

		// Token: 0x040008E1 RID: 2273
		internal const string DataConstraint_NotInTheTable = "DataConstraint_NotInTheTable";

		// Token: 0x040008E2 RID: 2274
		internal const string DataConstraint_OutOfRange = "DataConstraint_OutOfRange";

		// Token: 0x040008E3 RID: 2275
		internal const string DataConstraint_Duplicate = "DataConstraint_Duplicate";

		// Token: 0x040008E4 RID: 2276
		internal const string DataConstraint_DuplicateName = "DataConstraint_DuplicateName";

		// Token: 0x040008E5 RID: 2277
		internal const string DataConstraint_UniqueViolation = "DataConstraint_UniqueViolation";

		// Token: 0x040008E6 RID: 2278
		internal const string DataConstraint_ForeignTable = "DataConstraint_ForeignTable";

		// Token: 0x040008E7 RID: 2279
		internal const string DataConstraint_ParentValues = "DataConstraint_ParentValues";

		// Token: 0x040008E8 RID: 2280
		internal const string DataConstraint_AddFailed = "DataConstraint_AddFailed";

		// Token: 0x040008E9 RID: 2281
		internal const string DataConstraint_RemoveFailed = "DataConstraint_RemoveFailed";

		// Token: 0x040008EA RID: 2282
		internal const string DataConstraint_NeededForForeignKeyConstraint = "DataConstraint_NeededForForeignKeyConstraint";

		// Token: 0x040008EB RID: 2283
		internal const string DataConstraint_CascadeDelete = "DataConstraint_CascadeDelete";

		// Token: 0x040008EC RID: 2284
		internal const string DataConstraint_CascadeUpdate = "DataConstraint_CascadeUpdate";

		// Token: 0x040008ED RID: 2285
		internal const string DataConstraint_ClearParentTable = "DataConstraint_ClearParentTable";

		// Token: 0x040008EE RID: 2286
		internal const string DataConstraint_ForeignKeyViolation = "DataConstraint_ForeignKeyViolation";

		// Token: 0x040008EF RID: 2287
		internal const string DataConstraint_BadObjectPropertyAccess = "DataConstraint_BadObjectPropertyAccess";

		// Token: 0x040008F0 RID: 2288
		internal const string DataConstraint_RemoveParentRow = "DataConstraint_RemoveParentRow";

		// Token: 0x040008F1 RID: 2289
		internal const string DataConstraint_AddPrimaryKeyConstraint = "DataConstraint_AddPrimaryKeyConstraint";

		// Token: 0x040008F2 RID: 2290
		internal const string DataConstraint_CantAddConstraintToMultipleNestedTable = "DataConstraint_CantAddConstraintToMultipleNestedTable";

		// Token: 0x040008F3 RID: 2291
		internal const string DataKey_TableMismatch = "DataKey_TableMismatch";

		// Token: 0x040008F4 RID: 2292
		internal const string DataKey_NoColumns = "DataKey_NoColumns";

		// Token: 0x040008F5 RID: 2293
		internal const string DataKey_TooManyColumns = "DataKey_TooManyColumns";

		// Token: 0x040008F6 RID: 2294
		internal const string DataKey_DuplicateColumns = "DataKey_DuplicateColumns";

		// Token: 0x040008F7 RID: 2295
		internal const string DataKey_RemovePrimaryKey = "DataKey_RemovePrimaryKey";

		// Token: 0x040008F8 RID: 2296
		internal const string DataKey_RemovePrimaryKey1 = "DataKey_RemovePrimaryKey1";

		// Token: 0x040008F9 RID: 2297
		internal const string DataRelation_ColumnsTypeMismatch = "DataRelation_ColumnsTypeMismatch";

		// Token: 0x040008FA RID: 2298
		internal const string DataRelation_KeyColumnsIdentical = "DataRelation_KeyColumnsIdentical";

		// Token: 0x040008FB RID: 2299
		internal const string DataRelation_KeyLengthMismatch = "DataRelation_KeyLengthMismatch";

		// Token: 0x040008FC RID: 2300
		internal const string DataRelation_KeyZeroLength = "DataRelation_KeyZeroLength";

		// Token: 0x040008FD RID: 2301
		internal const string DataRelation_ForeignRow = "DataRelation_ForeignRow";

		// Token: 0x040008FE RID: 2302
		internal const string DataRelation_NoName = "DataRelation_NoName";

		// Token: 0x040008FF RID: 2303
		internal const string DataRelation_ForeignTable = "DataRelation_ForeignTable";

		// Token: 0x04000900 RID: 2304
		internal const string DataRelation_ForeignDataSet = "DataRelation_ForeignDataSet";

		// Token: 0x04000901 RID: 2305
		internal const string DataRelation_GetParentRowTableMismatch = "DataRelation_GetParentRowTableMismatch";

		// Token: 0x04000902 RID: 2306
		internal const string DataRelation_SetParentRowTableMismatch = "DataRelation_SetParentRowTableMismatch";

		// Token: 0x04000903 RID: 2307
		internal const string DataRelation_DataSetMismatch = "DataRelation_DataSetMismatch";

		// Token: 0x04000904 RID: 2308
		internal const string DataRelation_TablesInDifferentSets = "DataRelation_TablesInDifferentSets";

		// Token: 0x04000905 RID: 2309
		internal const string DataRelation_AlreadyExists = "DataRelation_AlreadyExists";

		// Token: 0x04000906 RID: 2310
		internal const string DataRelation_DoesNotExist = "DataRelation_DoesNotExist";

		// Token: 0x04000907 RID: 2311
		internal const string DataRelation_AlreadyInOtherDataSet = "DataRelation_AlreadyInOtherDataSet";

		// Token: 0x04000908 RID: 2312
		internal const string DataRelation_AlreadyInTheDataSet = "DataRelation_AlreadyInTheDataSet";

		// Token: 0x04000909 RID: 2313
		internal const string DataRelation_DuplicateName = "DataRelation_DuplicateName";

		// Token: 0x0400090A RID: 2314
		internal const string DataRelation_NotInTheDataSet = "DataRelation_NotInTheDataSet";

		// Token: 0x0400090B RID: 2315
		internal const string DataRelation_OutOfRange = "DataRelation_OutOfRange";

		// Token: 0x0400090C RID: 2316
		internal const string DataRelation_TableNull = "DataRelation_TableNull";

		// Token: 0x0400090D RID: 2317
		internal const string DataRelation_TableWasRemoved = "DataRelation_TableWasRemoved";

		// Token: 0x0400090E RID: 2318
		internal const string DataRelation_ChildTableMismatch = "DataRelation_ChildTableMismatch";

		// Token: 0x0400090F RID: 2319
		internal const string DataRelation_ParentTableMismatch = "DataRelation_ParentTableMismatch";

		// Token: 0x04000910 RID: 2320
		internal const string DataRelation_RelationNestedReadOnly = "DataRelation_RelationNestedReadOnly";

		// Token: 0x04000911 RID: 2321
		internal const string DataRelation_TableCantBeNestedInTwoTables = "DataRelation_TableCantBeNestedInTwoTables";

		// Token: 0x04000912 RID: 2322
		internal const string DataRelation_LoopInNestedRelations = "DataRelation_LoopInNestedRelations";

		// Token: 0x04000913 RID: 2323
		internal const string DataRelation_CaseLocaleMismatch = "DataRelation_CaseLocaleMismatch";

		// Token: 0x04000914 RID: 2324
		internal const string DataRelation_ParentOrChildColumnsDoNotHaveDataSet = "DataRelation_ParentOrChildColumnsDoNotHaveDataSet";

		// Token: 0x04000915 RID: 2325
		internal const string DataRelation_InValidNestedRelation = "DataRelation_InValidNestedRelation";

		// Token: 0x04000916 RID: 2326
		internal const string DataRelation_InValidNamespaceInNestedRelation = "DataRelation_InValidNamespaceInNestedRelation";

		// Token: 0x04000917 RID: 2327
		internal const string DataRow_NotInTheDataSet = "DataRow_NotInTheDataSet";

		// Token: 0x04000918 RID: 2328
		internal const string DataRow_NotInTheTable = "DataRow_NotInTheTable";

		// Token: 0x04000919 RID: 2329
		internal const string DataRow_ParentRowNotInTheDataSet = "DataRow_ParentRowNotInTheDataSet";

		// Token: 0x0400091A RID: 2330
		internal const string DataRow_EditInRowChanging = "DataRow_EditInRowChanging";

		// Token: 0x0400091B RID: 2331
		internal const string DataRow_EndEditInRowChanging = "DataRow_EndEditInRowChanging";

		// Token: 0x0400091C RID: 2332
		internal const string DataRow_BeginEditInRowChanging = "DataRow_BeginEditInRowChanging";

		// Token: 0x0400091D RID: 2333
		internal const string DataRow_CancelEditInRowChanging = "DataRow_CancelEditInRowChanging";

		// Token: 0x0400091E RID: 2334
		internal const string DataRow_DeleteInRowDeleting = "DataRow_DeleteInRowDeleting";

		// Token: 0x0400091F RID: 2335
		internal const string DataRow_ValuesArrayLength = "DataRow_ValuesArrayLength";

		// Token: 0x04000920 RID: 2336
		internal const string DataRow_NoCurrentData = "DataRow_NoCurrentData";

		// Token: 0x04000921 RID: 2337
		internal const string DataRow_NoOriginalData = "DataRow_NoOriginalData";

		// Token: 0x04000922 RID: 2338
		internal const string DataRow_NoProposedData = "DataRow_NoProposedData";

		// Token: 0x04000923 RID: 2339
		internal const string DataRow_RemovedFromTheTable = "DataRow_RemovedFromTheTable";

		// Token: 0x04000924 RID: 2340
		internal const string DataRow_DeletedRowInaccessible = "DataRow_DeletedRowInaccessible";

		// Token: 0x04000925 RID: 2341
		internal const string DataRow_InvalidVersion = "DataRow_InvalidVersion";

		// Token: 0x04000926 RID: 2342
		internal const string DataRow_OutOfRange = "DataRow_OutOfRange";

		// Token: 0x04000927 RID: 2343
		internal const string DataRow_RowInsertOutOfRange = "DataRow_RowInsertOutOfRange";

		// Token: 0x04000928 RID: 2344
		internal const string DataRow_RowInsertTwice = "DataRow_RowInsertTwice";

		// Token: 0x04000929 RID: 2345
		internal const string DataRow_RowInsertMissing = "DataRow_RowInsertMissing";

		// Token: 0x0400092A RID: 2346
		internal const string DataRow_RowOutOfRange = "DataRow_RowOutOfRange";

		// Token: 0x0400092B RID: 2347
		internal const string DataRow_AlreadyInOtherCollection = "DataRow_AlreadyInOtherCollection";

		// Token: 0x0400092C RID: 2348
		internal const string DataRow_AlreadyInTheCollection = "DataRow_AlreadyInTheCollection";

		// Token: 0x0400092D RID: 2349
		internal const string DataRow_AlreadyDeleted = "DataRow_AlreadyDeleted";

		// Token: 0x0400092E RID: 2350
		internal const string DataRow_Empty = "DataRow_Empty";

		// Token: 0x0400092F RID: 2351
		internal const string DataRow_AlreadyRemoved = "DataRow_AlreadyRemoved";

		// Token: 0x04000930 RID: 2352
		internal const string DataRow_MultipleParents = "DataRow_MultipleParents";

		// Token: 0x04000931 RID: 2353
		internal const string DataRow_InvalidRowBitPattern = "DataRow_InvalidRowBitPattern";

		// Token: 0x04000932 RID: 2354
		internal const string DataSet_SetNameToEmpty = "DataSet_SetNameToEmpty";

		// Token: 0x04000933 RID: 2355
		internal const string DataSet_SetDataSetNameConflicting = "DataSet_SetDataSetNameConflicting";

		// Token: 0x04000934 RID: 2356
		internal const string DataSet_UnsupportedSchema = "DataSet_UnsupportedSchema";

		// Token: 0x04000935 RID: 2357
		internal const string DataSet_CannotChangeCaseLocale = "DataSet_CannotChangeCaseLocale";

		// Token: 0x04000936 RID: 2358
		internal const string DataSet_CannotChangeSchemaSerializationMode = "DataSet_CannotChangeSchemaSerializationMode";

		// Token: 0x04000937 RID: 2359
		internal const string DataTable_ForeignPrimaryKey = "DataTable_ForeignPrimaryKey";

		// Token: 0x04000938 RID: 2360
		internal const string DataTable_CannotAddToSimpleContent = "DataTable_CannotAddToSimpleContent";

		// Token: 0x04000939 RID: 2361
		internal const string DataTable_NoName = "DataTable_NoName";

		// Token: 0x0400093A RID: 2362
		internal const string DataTable_MultipleSimpleContentColumns = "DataTable_MultipleSimpleContentColumns";

		// Token: 0x0400093B RID: 2363
		internal const string DataTable_MissingPrimaryKey = "DataTable_MissingPrimaryKey";

		// Token: 0x0400093C RID: 2364
		internal const string DataTable_InvalidSortString = "DataTable_InvalidSortString";

		// Token: 0x0400093D RID: 2365
		internal const string DataTable_CanNotSerializeDataTableHierarchy = "DataTable_CanNotSerializeDataTableHierarchy";

		// Token: 0x0400093E RID: 2366
		internal const string DataTable_CanNotRemoteDataTable = "DataTable_CanNotRemoteDataTable";

		// Token: 0x0400093F RID: 2367
		internal const string DataTable_CanNotSetRemotingFormat = "DataTable_CanNotSetRemotingFormat";

		// Token: 0x04000940 RID: 2368
		internal const string DataTable_CanNotSerializeDataTableWithEmptyName = "DataTable_CanNotSerializeDataTableWithEmptyName";

		// Token: 0x04000941 RID: 2369
		internal const string DataTable_DuplicateName = "DataTable_DuplicateName";

		// Token: 0x04000942 RID: 2370
		internal const string DataTable_DuplicateName2 = "DataTable_DuplicateName2";

		// Token: 0x04000943 RID: 2371
		internal const string DataTable_SelfnestedDatasetConflictingName = "DataTable_SelfnestedDatasetConflictingName";

		// Token: 0x04000944 RID: 2372
		internal const string DataTable_DatasetConflictingName = "DataTable_DatasetConflictingName";

		// Token: 0x04000945 RID: 2373
		internal const string DataTable_AlreadyInOtherDataSet = "DataTable_AlreadyInOtherDataSet";

		// Token: 0x04000946 RID: 2374
		internal const string DataTable_AlreadyInTheDataSet = "DataTable_AlreadyInTheDataSet";

		// Token: 0x04000947 RID: 2375
		internal const string DataTable_NotInTheDataSet = "DataTable_NotInTheDataSet";

		// Token: 0x04000948 RID: 2376
		internal const string DataTable_OutOfRange = "DataTable_OutOfRange";

		// Token: 0x04000949 RID: 2377
		internal const string DataTable_InRelation = "DataTable_InRelation";

		// Token: 0x0400094A RID: 2378
		internal const string DataTable_InConstraint = "DataTable_InConstraint";

		// Token: 0x0400094B RID: 2379
		internal const string DataTable_TableNotFound = "DataTable_TableNotFound";

		// Token: 0x0400094C RID: 2380
		internal const string DataMerge_MissingDefinition = "DataMerge_MissingDefinition";

		// Token: 0x0400094D RID: 2381
		internal const string DataMerge_MissingConstraint = "DataMerge_MissingConstraint";

		// Token: 0x0400094E RID: 2382
		internal const string DataMerge_DataTypeMismatch = "DataMerge_DataTypeMismatch";

		// Token: 0x0400094F RID: 2383
		internal const string DataMerge_PrimaryKeyMismatch = "DataMerge_PrimaryKeyMismatch";

		// Token: 0x04000950 RID: 2384
		internal const string DataMerge_PrimaryKeyColumnsMismatch = "DataMerge_PrimaryKeyColumnsMismatch";

		// Token: 0x04000951 RID: 2385
		internal const string DataMerge_ReltionKeyColumnsMismatch = "DataMerge_ReltionKeyColumnsMismatch";

		// Token: 0x04000952 RID: 2386
		internal const string DataMerge_MissingColumnDefinition = "DataMerge_MissingColumnDefinition";

		// Token: 0x04000953 RID: 2387
		internal const string DataMerge_MissingPrimaryKeyColumnInSource = "DataMerge_MissingPrimaryKeyColumnInSource";

		// Token: 0x04000954 RID: 2388
		internal const string DataIndex_RecordStateRange = "DataIndex_RecordStateRange";

		// Token: 0x04000955 RID: 2389
		internal const string DataIndex_FindWithoutSortOrder = "DataIndex_FindWithoutSortOrder";

		// Token: 0x04000956 RID: 2390
		internal const string DataIndex_KeyLength = "DataIndex_KeyLength";

		// Token: 0x04000957 RID: 2391
		internal const string DataStorage_AggregateException = "DataStorage_AggregateException";

		// Token: 0x04000958 RID: 2392
		internal const string DataStorage_InvalidStorageType = "DataStorage_InvalidStorageType";

		// Token: 0x04000959 RID: 2393
		internal const string DataStorage_ProblematicChars = "DataStorage_ProblematicChars";

		// Token: 0x0400095A RID: 2394
		internal const string DataStorage_SetInvalidDataType = "DataStorage_SetInvalidDataType";

		// Token: 0x0400095B RID: 2395
		internal const string DataStorage_IComparableNotDefined = "DataStorage_IComparableNotDefined";

		// Token: 0x0400095C RID: 2396
		internal const string DataView_SetFailed = "DataView_SetFailed";

		// Token: 0x0400095D RID: 2397
		internal const string DataView_SetDataSetFailed = "DataView_SetDataSetFailed";

		// Token: 0x0400095E RID: 2398
		internal const string DataView_SetRowStateFilter = "DataView_SetRowStateFilter";

		// Token: 0x0400095F RID: 2399
		internal const string DataView_SetTable = "DataView_SetTable";

		// Token: 0x04000960 RID: 2400
		internal const string DataView_CanNotSetDataSet = "DataView_CanNotSetDataSet";

		// Token: 0x04000961 RID: 2401
		internal const string DataView_CanNotUseDataViewManager = "DataView_CanNotUseDataViewManager";

		// Token: 0x04000962 RID: 2402
		internal const string DataView_CanNotSetTable = "DataView_CanNotSetTable";

		// Token: 0x04000963 RID: 2403
		internal const string DataView_CanNotUse = "DataView_CanNotUse";

		// Token: 0x04000964 RID: 2404
		internal const string DataView_CanNotBindTable = "DataView_CanNotBindTable";

		// Token: 0x04000965 RID: 2405
		internal const string DataView_SetIListObject = "DataView_SetIListObject";

		// Token: 0x04000966 RID: 2406
		internal const string DataView_AddNewNotAllowNull = "DataView_AddNewNotAllowNull";

		// Token: 0x04000967 RID: 2407
		internal const string DataView_NotOpen = "DataView_NotOpen";

		// Token: 0x04000968 RID: 2408
		internal const string DataView_CreateChildView = "DataView_CreateChildView";

		// Token: 0x04000969 RID: 2409
		internal const string DataView_CanNotDelete = "DataView_CanNotDelete";

		// Token: 0x0400096A RID: 2410
		internal const string DataView_CanNotEdit = "DataView_CanNotEdit";

		// Token: 0x0400096B RID: 2411
		internal const string DataView_GetElementIndex = "DataView_GetElementIndex";

		// Token: 0x0400096C RID: 2412
		internal const string DataView_AddExternalObject = "DataView_AddExternalObject";

		// Token: 0x0400096D RID: 2413
		internal const string DataView_CanNotClear = "DataView_CanNotClear";

		// Token: 0x0400096E RID: 2414
		internal const string DataView_InsertExternalObject = "DataView_InsertExternalObject";

		// Token: 0x0400096F RID: 2415
		internal const string DataView_RemoveExternalObject = "DataView_RemoveExternalObject";

		// Token: 0x04000970 RID: 2416
		internal const string DataROWView_PropertyNotFound = "DataROWView_PropertyNotFound";

		// Token: 0x04000971 RID: 2417
		internal const string Range_Argument = "Range_Argument";

		// Token: 0x04000972 RID: 2418
		internal const string Range_NullRange = "Range_NullRange";

		// Token: 0x04000973 RID: 2419
		internal const string RecordManager_MinimumCapacity = "RecordManager_MinimumCapacity";

		// Token: 0x04000974 RID: 2420
		internal const string CodeGen_InvalidIdentifier = "CodeGen_InvalidIdentifier";

		// Token: 0x04000975 RID: 2421
		internal const string CodeGen_DuplicateTableName = "CodeGen_DuplicateTableName";

		// Token: 0x04000976 RID: 2422
		internal const string CodeGen_TypeCantBeNull = "CodeGen_TypeCantBeNull";

		// Token: 0x04000977 RID: 2423
		internal const string CodeGen_NoCtor0 = "CodeGen_NoCtor0";

		// Token: 0x04000978 RID: 2424
		internal const string CodeGen_NoCtor1 = "CodeGen_NoCtor1";

		// Token: 0x04000979 RID: 2425
		internal const string SqlConvert_ConvertFailed = "SqlConvert_ConvertFailed";

		// Token: 0x0400097A RID: 2426
		internal const string DataSet_DefaultDataException = "DataSet_DefaultDataException";

		// Token: 0x0400097B RID: 2427
		internal const string DataSet_DefaultConstraintException = "DataSet_DefaultConstraintException";

		// Token: 0x0400097C RID: 2428
		internal const string DataSet_DefaultDeletedRowInaccessibleException = "DataSet_DefaultDeletedRowInaccessibleException";

		// Token: 0x0400097D RID: 2429
		internal const string DataSet_DefaultDuplicateNameException = "DataSet_DefaultDuplicateNameException";

		// Token: 0x0400097E RID: 2430
		internal const string DataSet_DefaultInRowChangingEventException = "DataSet_DefaultInRowChangingEventException";

		// Token: 0x0400097F RID: 2431
		internal const string DataSet_DefaultInvalidConstraintException = "DataSet_DefaultInvalidConstraintException";

		// Token: 0x04000980 RID: 2432
		internal const string DataSet_DefaultMissingPrimaryKeyException = "DataSet_DefaultMissingPrimaryKeyException";

		// Token: 0x04000981 RID: 2433
		internal const string DataSet_DefaultNoNullAllowedException = "DataSet_DefaultNoNullAllowedException";

		// Token: 0x04000982 RID: 2434
		internal const string DataSet_DefaultReadOnlyException = "DataSet_DefaultReadOnlyException";

		// Token: 0x04000983 RID: 2435
		internal const string DataSet_DefaultRowNotInTableException = "DataSet_DefaultRowNotInTableException";

		// Token: 0x04000984 RID: 2436
		internal const string DataSet_DefaultVersionNotFoundException = "DataSet_DefaultVersionNotFoundException";

		// Token: 0x04000985 RID: 2437
		internal const string Load_ReadOnlyDataModified = "Load_ReadOnlyDataModified";

		// Token: 0x04000986 RID: 2438
		internal const string DataTableReader_InvalidDataTableReader = "DataTableReader_InvalidDataTableReader";

		// Token: 0x04000987 RID: 2439
		internal const string DataTableReader_SchemaInvalidDataTableReader = "DataTableReader_SchemaInvalidDataTableReader";

		// Token: 0x04000988 RID: 2440
		internal const string DataTableReader_CannotCreateDataReaderOnEmptyDataSet = "DataTableReader_CannotCreateDataReaderOnEmptyDataSet";

		// Token: 0x04000989 RID: 2441
		internal const string DataTableReader_DataTableReaderArgumentIsEmpty = "DataTableReader_DataTableReaderArgumentIsEmpty";

		// Token: 0x0400098A RID: 2442
		internal const string DataTableReader_ArgumentContainsNullValue = "DataTableReader_ArgumentContainsNullValue";

		// Token: 0x0400098B RID: 2443
		internal const string DataTableReader_InvalidRowInDataTableReader = "DataTableReader_InvalidRowInDataTableReader";

		// Token: 0x0400098C RID: 2444
		internal const string DataTableReader_DataTableCleared = "DataTableReader_DataTableCleared";

		// Token: 0x0400098D RID: 2445
		internal const string RbTree_InvalidState = "RbTree_InvalidState";

		// Token: 0x0400098E RID: 2446
		internal const string RbTree_EnumerationBroken = "RbTree_EnumerationBroken";

		// Token: 0x0400098F RID: 2447
		internal const string NamedSimpleType_InvalidDuplicateNamedSimpleTypeDelaration = "NamedSimpleType_InvalidDuplicateNamedSimpleTypeDelaration";

		// Token: 0x04000990 RID: 2448
		internal const string DataDom_Foliation = "DataDom_Foliation";

		// Token: 0x04000991 RID: 2449
		internal const string DataDom_TableNameChange = "DataDom_TableNameChange";

		// Token: 0x04000992 RID: 2450
		internal const string DataDom_TableNamespaceChange = "DataDom_TableNamespaceChange";

		// Token: 0x04000993 RID: 2451
		internal const string DataDom_ColumnNameChange = "DataDom_ColumnNameChange";

		// Token: 0x04000994 RID: 2452
		internal const string DataDom_ColumnNamespaceChange = "DataDom_ColumnNamespaceChange";

		// Token: 0x04000995 RID: 2453
		internal const string DataDom_ColumnMappingChange = "DataDom_ColumnMappingChange";

		// Token: 0x04000996 RID: 2454
		internal const string DataDom_TableColumnsChange = "DataDom_TableColumnsChange";

		// Token: 0x04000997 RID: 2455
		internal const string DataDom_DataSetTablesChange = "DataDom_DataSetTablesChange";

		// Token: 0x04000998 RID: 2456
		internal const string DataDom_DataSetNestedRelationsChange = "DataDom_DataSetNestedRelationsChange";

		// Token: 0x04000999 RID: 2457
		internal const string DataDom_DataSetNull = "DataDom_DataSetNull";

		// Token: 0x0400099A RID: 2458
		internal const string DataDom_DataSetNameChange = "DataDom_DataSetNameChange";

		// Token: 0x0400099B RID: 2459
		internal const string DataDom_CloneNode = "DataDom_CloneNode";

		// Token: 0x0400099C RID: 2460
		internal const string DataDom_MultipleLoad = "DataDom_MultipleLoad";

		// Token: 0x0400099D RID: 2461
		internal const string DataDom_MultipleDataSet = "DataDom_MultipleDataSet";

		// Token: 0x0400099E RID: 2462
		internal const string DataDom_EnforceConstraintsShouldBeOff = "DataDom_EnforceConstraintsShouldBeOff";

		// Token: 0x0400099F RID: 2463
		internal const string DataDom_NotSupport_GetElementById = "DataDom_NotSupport_GetElementById";

		// Token: 0x040009A0 RID: 2464
		internal const string DataDom_NotSupport_EntRef = "DataDom_NotSupport_EntRef";

		// Token: 0x040009A1 RID: 2465
		internal const string DataDom_NotSupport_Clear = "DataDom_NotSupport_Clear";

		// Token: 0x040009A2 RID: 2466
		internal const string StrongTyping_CannotRemoveColumn = "StrongTyping_CannotRemoveColumn";

		// Token: 0x040009A3 RID: 2467
		internal const string StrongTyping_CananotRemoveRelation = "StrongTyping_CananotRemoveRelation";

		// Token: 0x040009A4 RID: 2468
		internal const string propertyChangedEventDescr = "propertyChangedEventDescr";

		// Token: 0x040009A5 RID: 2469
		internal const string collectionChangedEventDescr = "collectionChangedEventDescr";

		// Token: 0x040009A6 RID: 2470
		internal const string StrongTyping_CananotAccessDBNull = "StrongTyping_CananotAccessDBNull";

		// Token: 0x040009A7 RID: 2471
		internal const string ADP_PropertyNotSupported = "ADP_PropertyNotSupported";

		// Token: 0x040009A8 RID: 2472
		internal const string ConfigProviderNotFound = "ConfigProviderNotFound";

		// Token: 0x040009A9 RID: 2473
		internal const string ConfigProviderInvalid = "ConfigProviderInvalid";

		// Token: 0x040009AA RID: 2474
		internal const string ConfigProviderNotInstalled = "ConfigProviderNotInstalled";

		// Token: 0x040009AB RID: 2475
		internal const string ConfigProviderMissing = "ConfigProviderMissing";

		// Token: 0x040009AC RID: 2476
		internal const string ConfigBaseElementsOnly = "ConfigBaseElementsOnly";

		// Token: 0x040009AD RID: 2477
		internal const string ConfigBaseNoChildNodes = "ConfigBaseNoChildNodes";

		// Token: 0x040009AE RID: 2478
		internal const string ConfigUnrecognizedAttributes = "ConfigUnrecognizedAttributes";

		// Token: 0x040009AF RID: 2479
		internal const string ConfigUnrecognizedElement = "ConfigUnrecognizedElement";

		// Token: 0x040009B0 RID: 2480
		internal const string ConfigSectionsUnique = "ConfigSectionsUnique";

		// Token: 0x040009B1 RID: 2481
		internal const string ConfigRequiredAttributeMissing = "ConfigRequiredAttributeMissing";

		// Token: 0x040009B2 RID: 2482
		internal const string ConfigRequiredAttributeEmpty = "ConfigRequiredAttributeEmpty";

		// Token: 0x040009B3 RID: 2483
		internal const string ADP_EmptyArray = "ADP_EmptyArray";

		// Token: 0x040009B4 RID: 2484
		internal const string ADP_SingleValuedProperty = "ADP_SingleValuedProperty";

		// Token: 0x040009B5 RID: 2485
		internal const string ADP_DoubleValuedProperty = "ADP_DoubleValuedProperty";

		// Token: 0x040009B6 RID: 2486
		internal const string ADP_InvalidPrefixSuffix = "ADP_InvalidPrefixSuffix";

		// Token: 0x040009B7 RID: 2487
		internal const string ADP_InvalidArgumentLength = "ADP_InvalidArgumentLength";

		// Token: 0x040009B8 RID: 2488
		internal const string SQL_WrongType = "SQL_WrongType";

		// Token: 0x040009B9 RID: 2489
		internal const string ADP_InvalidConnectionOptionValue = "ADP_InvalidConnectionOptionValue";

		// Token: 0x040009BA RID: 2490
		internal const string ADP_MissingConnectionOptionValue = "ADP_MissingConnectionOptionValue";

		// Token: 0x040009BB RID: 2491
		internal const string ADP_InvalidConnectionOptionValueLength = "ADP_InvalidConnectionOptionValueLength";

		// Token: 0x040009BC RID: 2492
		internal const string ADP_KeywordNotSupported = "ADP_KeywordNotSupported";

		// Token: 0x040009BD RID: 2493
		internal const string ADP_UdlFileError = "ADP_UdlFileError";

		// Token: 0x040009BE RID: 2494
		internal const string ADP_InvalidUDL = "ADP_InvalidUDL";

		// Token: 0x040009BF RID: 2495
		internal const string ADP_InternalProviderError = "ADP_InternalProviderError";

		// Token: 0x040009C0 RID: 2496
		internal const string ADP_NoQuoteChange = "ADP_NoQuoteChange";

		// Token: 0x040009C1 RID: 2497
		internal const string ADP_MissingSourceCommand = "ADP_MissingSourceCommand";

		// Token: 0x040009C2 RID: 2498
		internal const string ADP_MissingSourceCommandConnection = "ADP_MissingSourceCommandConnection";

		// Token: 0x040009C3 RID: 2499
		internal const string ADP_InvalidMultipartName = "ADP_InvalidMultipartName";

		// Token: 0x040009C4 RID: 2500
		internal const string ADP_InvalidMultipartNameQuoteUsage = "ADP_InvalidMultipartNameQuoteUsage";

		// Token: 0x040009C5 RID: 2501
		internal const string ADP_InvalidMultipartNameToManyParts = "ADP_InvalidMultipartNameToManyParts";

		// Token: 0x040009C6 RID: 2502
		internal const string SQL_BulkCopyDestinationTableName = "SQL_BulkCopyDestinationTableName";

		// Token: 0x040009C7 RID: 2503
		internal const string SQL_TDSParserTableName = "SQL_TDSParserTableName";

		// Token: 0x040009C8 RID: 2504
		internal const string SQL_UDTTypeName = "SQL_UDTTypeName";

		// Token: 0x040009C9 RID: 2505
		internal const string SQL_TypeName = "SQL_TypeName";

		// Token: 0x040009CA RID: 2506
		internal const string SQL_SqlCommandCommandText = "SQL_SqlCommandCommandText";

		// Token: 0x040009CB RID: 2507
		internal const string ODBC_ODBCCommandText = "ODBC_ODBCCommandText";

		// Token: 0x040009CC RID: 2508
		internal const string OLEDB_OLEDBCommandText = "OLEDB_OLEDBCommandText";

		// Token: 0x040009CD RID: 2509
		internal const string SQLMSF_FailoverPartnerNotSupported = "SQLMSF_FailoverPartnerNotSupported";

		// Token: 0x040009CE RID: 2510
		internal const string ADP_ColumnSchemaExpression = "ADP_ColumnSchemaExpression";

		// Token: 0x040009CF RID: 2511
		internal const string ADP_ColumnSchemaMismatch = "ADP_ColumnSchemaMismatch";

		// Token: 0x040009D0 RID: 2512
		internal const string ADP_ColumnSchemaMissing1 = "ADP_ColumnSchemaMissing1";

		// Token: 0x040009D1 RID: 2513
		internal const string ADP_ColumnSchemaMissing2 = "ADP_ColumnSchemaMissing2";

		// Token: 0x040009D2 RID: 2514
		internal const string ADP_InvalidSourceColumn = "ADP_InvalidSourceColumn";

		// Token: 0x040009D3 RID: 2515
		internal const string ADP_MissingColumnMapping = "ADP_MissingColumnMapping";

		// Token: 0x040009D4 RID: 2516
		internal const string ADP_NotSupportedEnumerationValue = "ADP_NotSupportedEnumerationValue";

		// Token: 0x040009D5 RID: 2517
		internal const string ODBC_NotSupportedEnumerationValue = "ODBC_NotSupportedEnumerationValue";

		// Token: 0x040009D6 RID: 2518
		internal const string OLEDB_NotSupportedEnumerationValue = "OLEDB_NotSupportedEnumerationValue";

		// Token: 0x040009D7 RID: 2519
		internal const string SQL_NotSupportedEnumerationValue = "SQL_NotSupportedEnumerationValue";

		// Token: 0x040009D8 RID: 2520
		internal const string ADP_ComputerNameEx = "ADP_ComputerNameEx";

		// Token: 0x040009D9 RID: 2521
		internal const string ADP_MissingTableSchema = "ADP_MissingTableSchema";

		// Token: 0x040009DA RID: 2522
		internal const string ADP_InvalidSourceTable = "ADP_InvalidSourceTable";

		// Token: 0x040009DB RID: 2523
		internal const string ADP_MissingTableMapping = "ADP_MissingTableMapping";

		// Token: 0x040009DC RID: 2524
		internal const string ADP_CommandTextRequired = "ADP_CommandTextRequired";

		// Token: 0x040009DD RID: 2525
		internal const string ADP_ConnectionRequired = "ADP_ConnectionRequired";

		// Token: 0x040009DE RID: 2526
		internal const string ADP_OpenConnectionRequired = "ADP_OpenConnectionRequired";

		// Token: 0x040009DF RID: 2527
		internal const string ADP_ConnectionRequired_Fill = "ADP_ConnectionRequired_Fill";

		// Token: 0x040009E0 RID: 2528
		internal const string ADP_ConnectionRequired_FillPage = "ADP_ConnectionRequired_FillPage";

		// Token: 0x040009E1 RID: 2529
		internal const string ADP_ConnectionRequired_FillSchema = "ADP_ConnectionRequired_FillSchema";

		// Token: 0x040009E2 RID: 2530
		internal const string ADP_ConnectionRequired_Insert = "ADP_ConnectionRequired_Insert";

		// Token: 0x040009E3 RID: 2531
		internal const string ADP_ConnectionRequired_Update = "ADP_ConnectionRequired_Update";

		// Token: 0x040009E4 RID: 2532
		internal const string ADP_ConnectionRequired_Delete = "ADP_ConnectionRequired_Delete";

		// Token: 0x040009E5 RID: 2533
		internal const string ADP_ConnectionRequired_Batch = "ADP_ConnectionRequired_Batch";

		// Token: 0x040009E6 RID: 2534
		internal const string ADP_ConnectionRequired_Clone = "ADP_ConnectionRequired_Clone";

		// Token: 0x040009E7 RID: 2535
		internal const string ADP_ConnecitonRequired_UpdateRows = "ADP_ConnecitonRequired_UpdateRows";

		// Token: 0x040009E8 RID: 2536
		internal const string ADP_OpenConnectionRequired_Insert = "ADP_OpenConnectionRequired_Insert";

		// Token: 0x040009E9 RID: 2537
		internal const string ADP_OpenConnectionRequired_Update = "ADP_OpenConnectionRequired_Update";

		// Token: 0x040009EA RID: 2538
		internal const string ADP_OpenConnectionRequired_Delete = "ADP_OpenConnectionRequired_Delete";

		// Token: 0x040009EB RID: 2539
		internal const string ADP_OpenConnectionRequired_Clone = "ADP_OpenConnectionRequired_Clone";

		// Token: 0x040009EC RID: 2540
		internal const string ADP_NoStoredProcedureExists = "ADP_NoStoredProcedureExists";

		// Token: 0x040009ED RID: 2541
		internal const string ADP_TransactionCompleted = "ADP_TransactionCompleted";

		// Token: 0x040009EE RID: 2542
		internal const string ADP_TransactionConnectionMismatch = "ADP_TransactionConnectionMismatch";

		// Token: 0x040009EF RID: 2543
		internal const string ADP_TransactionCompletedButNotDisposed = "ADP_TransactionCompletedButNotDisposed";

		// Token: 0x040009F0 RID: 2544
		internal const string ADP_TransactionRequired = "ADP_TransactionRequired";

		// Token: 0x040009F1 RID: 2545
		internal const string ADP_OpenResultSetExists = "ADP_OpenResultSetExists";

		// Token: 0x040009F2 RID: 2546
		internal const string ADP_OpenReaderExists = "ADP_OpenReaderExists";

		// Token: 0x040009F3 RID: 2547
		internal const string ADP_DeriveParametersNotSupported = "ADP_DeriveParametersNotSupported";

		// Token: 0x040009F4 RID: 2548
		internal const string ADP_CalledTwice = "ADP_CalledTwice";

		// Token: 0x040009F5 RID: 2549
		internal const string ADP_IncorrectAsyncResult = "ADP_IncorrectAsyncResult";

		// Token: 0x040009F6 RID: 2550
		internal const string ADP_MissingSelectCommand = "ADP_MissingSelectCommand";

		// Token: 0x040009F7 RID: 2551
		internal const string ADP_UnwantedStatementType = "ADP_UnwantedStatementType";

		// Token: 0x040009F8 RID: 2552
		internal const string ADP_FillSchemaRequiresSourceTableName = "ADP_FillSchemaRequiresSourceTableName";

		// Token: 0x040009F9 RID: 2553
		internal const string ADP_InvalidMaxRecords = "ADP_InvalidMaxRecords";

		// Token: 0x040009FA RID: 2554
		internal const string ADP_InvalidStartRecord = "ADP_InvalidStartRecord";

		// Token: 0x040009FB RID: 2555
		internal const string ADP_FillRequiresSourceTableName = "ADP_FillRequiresSourceTableName";

		// Token: 0x040009FC RID: 2556
		internal const string ADP_FillChapterAutoIncrement = "ADP_FillChapterAutoIncrement";

		// Token: 0x040009FD RID: 2557
		internal const string ADP_MissingDataReaderFieldType = "ADP_MissingDataReaderFieldType";

		// Token: 0x040009FE RID: 2558
		internal const string ADP_OnlyOneTableForStartRecordOrMaxRecords = "ADP_OnlyOneTableForStartRecordOrMaxRecords";

		// Token: 0x040009FF RID: 2559
		internal const string ADP_UpdateRequiresSourceTable = "ADP_UpdateRequiresSourceTable";

		// Token: 0x04000A00 RID: 2560
		internal const string ADP_UpdateRequiresSourceTableName = "ADP_UpdateRequiresSourceTableName";

		// Token: 0x04000A01 RID: 2561
		internal const string ADP_MissingTableMappingDestination = "ADP_MissingTableMappingDestination";

		// Token: 0x04000A02 RID: 2562
		internal const string ADP_UpdateRequiresCommandClone = "ADP_UpdateRequiresCommandClone";

		// Token: 0x04000A03 RID: 2563
		internal const string ADP_UpdateRequiresCommandSelect = "ADP_UpdateRequiresCommandSelect";

		// Token: 0x04000A04 RID: 2564
		internal const string ADP_UpdateRequiresCommandInsert = "ADP_UpdateRequiresCommandInsert";

		// Token: 0x04000A05 RID: 2565
		internal const string ADP_UpdateRequiresCommandUpdate = "ADP_UpdateRequiresCommandUpdate";

		// Token: 0x04000A06 RID: 2566
		internal const string ADP_UpdateRequiresCommandDelete = "ADP_UpdateRequiresCommandDelete";

		// Token: 0x04000A07 RID: 2567
		internal const string ADP_UpdateMismatchRowTable = "ADP_UpdateMismatchRowTable";

		// Token: 0x04000A08 RID: 2568
		internal const string ADP_RowUpdatedErrors = "ADP_RowUpdatedErrors";

		// Token: 0x04000A09 RID: 2569
		internal const string ADP_RowUpdatingErrors = "ADP_RowUpdatingErrors";

		// Token: 0x04000A0A RID: 2570
		internal const string ADP_ResultsNotAllowedDuringBatch = "ADP_ResultsNotAllowedDuringBatch";

		// Token: 0x04000A0B RID: 2571
		internal const string ADP_UpdateConcurrencyViolation_Update = "ADP_UpdateConcurrencyViolation_Update";

		// Token: 0x04000A0C RID: 2572
		internal const string ADP_UpdateConcurrencyViolation_Delete = "ADP_UpdateConcurrencyViolation_Delete";

		// Token: 0x04000A0D RID: 2573
		internal const string ADP_UpdateConcurrencyViolation_Batch = "ADP_UpdateConcurrencyViolation_Batch";

		// Token: 0x04000A0E RID: 2574
		internal const string ADP_InvalidCommandTimeout = "ADP_InvalidCommandTimeout";

		// Token: 0x04000A0F RID: 2575
		internal const string ADP_UninitializedParameterSize = "ADP_UninitializedParameterSize";

		// Token: 0x04000A10 RID: 2576
		internal const string ADP_PrepareParameterType = "ADP_PrepareParameterType";

		// Token: 0x04000A11 RID: 2577
		internal const string ADP_PrepareParameterSize = "ADP_PrepareParameterSize";

		// Token: 0x04000A12 RID: 2578
		internal const string ADP_PrepareParameterScale = "ADP_PrepareParameterScale";

		// Token: 0x04000A13 RID: 2579
		internal const string ADP_MismatchedAsyncResult = "ADP_MismatchedAsyncResult";

		// Token: 0x04000A14 RID: 2580
		internal const string ADP_ClosedConnectionError = "ADP_ClosedConnectionError";

		// Token: 0x04000A15 RID: 2581
		internal const string ADP_ConnectionIsDisabled = "ADP_ConnectionIsDisabled";

		// Token: 0x04000A16 RID: 2582
		internal const string ADP_LocalTransactionPresent = "ADP_LocalTransactionPresent";

		// Token: 0x04000A17 RID: 2583
		internal const string ADP_TransactionPresent = "ADP_TransactionPresent";

		// Token: 0x04000A18 RID: 2584
		internal const string ADP_EmptyDatabaseName = "ADP_EmptyDatabaseName";

		// Token: 0x04000A19 RID: 2585
		internal const string ADP_DatabaseNameTooLong = "ADP_DatabaseNameTooLong";

		// Token: 0x04000A1A RID: 2586
		internal const string ADP_InvalidConnectTimeoutValue = "ADP_InvalidConnectTimeoutValue";

		// Token: 0x04000A1B RID: 2587
		internal const string ADP_InvalidSourceBufferIndex = "ADP_InvalidSourceBufferIndex";

		// Token: 0x04000A1C RID: 2588
		internal const string ADP_InvalidDestinationBufferIndex = "ADP_InvalidDestinationBufferIndex";

		// Token: 0x04000A1D RID: 2589
		internal const string ADP_DataReaderNoData = "ADP_DataReaderNoData";

		// Token: 0x04000A1E RID: 2590
		internal const string ADP_NumericToDecimalOverflow = "ADP_NumericToDecimalOverflow";

		// Token: 0x04000A1F RID: 2591
		internal const string ADP_StreamClosed = "ADP_StreamClosed";

		// Token: 0x04000A20 RID: 2592
		internal const string ADP_InvalidSeekOrigin = "ADP_InvalidSeekOrigin";

		// Token: 0x04000A21 RID: 2593
		internal const string ADP_DynamicSQLJoinUnsupported = "ADP_DynamicSQLJoinUnsupported";

		// Token: 0x04000A22 RID: 2594
		internal const string ADP_DynamicSQLNoTableInfo = "ADP_DynamicSQLNoTableInfo";

		// Token: 0x04000A23 RID: 2595
		internal const string ADP_DynamicSQLNoKeyInfoDelete = "ADP_DynamicSQLNoKeyInfoDelete";

		// Token: 0x04000A24 RID: 2596
		internal const string ADP_DynamicSQLNoKeyInfoUpdate = "ADP_DynamicSQLNoKeyInfoUpdate";

		// Token: 0x04000A25 RID: 2597
		internal const string ADP_DynamicSQLNoKeyInfoRowVersionDelete = "ADP_DynamicSQLNoKeyInfoRowVersionDelete";

		// Token: 0x04000A26 RID: 2598
		internal const string ADP_DynamicSQLNoKeyInfoRowVersionUpdate = "ADP_DynamicSQLNoKeyInfoRowVersionUpdate";

		// Token: 0x04000A27 RID: 2599
		internal const string ADP_DynamicSQLNestedQuote = "ADP_DynamicSQLNestedQuote";

		// Token: 0x04000A28 RID: 2600
		internal const string ADP_NonSequentialColumnAccess = "ADP_NonSequentialColumnAccess";

		// Token: 0x04000A29 RID: 2601
		internal const string ADP_InvalidDateTimeDigits = "ADP_InvalidDateTimeDigits";

		// Token: 0x04000A2A RID: 2602
		internal const string ADP_InvalidFormatValue = "ADP_InvalidFormatValue";

		// Token: 0x04000A2B RID: 2603
		internal const string ADP_InvalidMaximumScale = "ADP_InvalidMaximumScale";

		// Token: 0x04000A2C RID: 2604
		internal const string ADP_LiteralValueIsInvalid = "ADP_LiteralValueIsInvalid";

		// Token: 0x04000A2D RID: 2605
		internal const string ADP_EvenLengthLiteralValue = "ADP_EvenLengthLiteralValue";

		// Token: 0x04000A2E RID: 2606
		internal const string ADP_HexDigitLiteralValue = "ADP_HexDigitLiteralValue";

		// Token: 0x04000A2F RID: 2607
		internal const string ADP_QuotePrefixNotSet = "ADP_QuotePrefixNotSet";

		// Token: 0x04000A30 RID: 2608
		internal const string ADP_UnableToCreateBooleanLiteral = "ADP_UnableToCreateBooleanLiteral";

		// Token: 0x04000A31 RID: 2609
		internal const string ADP_UnsupportedNativeDataTypeOleDb = "ADP_UnsupportedNativeDataTypeOleDb";

		// Token: 0x04000A32 RID: 2610
		internal const string ADP_InvalidDataType = "ADP_InvalidDataType";

		// Token: 0x04000A33 RID: 2611
		internal const string ADP_UnknownDataType = "ADP_UnknownDataType";

		// Token: 0x04000A34 RID: 2612
		internal const string ADP_UnknownDataTypeCode = "ADP_UnknownDataTypeCode";

		// Token: 0x04000A35 RID: 2613
		internal const string ADP_DbTypeNotSupported = "ADP_DbTypeNotSupported";

		// Token: 0x04000A36 RID: 2614
		internal const string ADP_VersionDoesNotSupportDataType = "ADP_VersionDoesNotSupportDataType";

		// Token: 0x04000A37 RID: 2615
		internal const string ADP_ParameterValueOutOfRange = "ADP_ParameterValueOutOfRange";

		// Token: 0x04000A38 RID: 2616
		internal const string ADP_BadParameterName = "ADP_BadParameterName";

		// Token: 0x04000A39 RID: 2617
		internal const string ADP_MultipleReturnValue = "ADP_MultipleReturnValue";

		// Token: 0x04000A3A RID: 2618
		internal const string ADP_InvalidSizeValue = "ADP_InvalidSizeValue";

		// Token: 0x04000A3B RID: 2619
		internal const string ADP_NegativeParameter = "ADP_NegativeParameter";

		// Token: 0x04000A3C RID: 2620
		internal const string ADP_InvalidMetaDataValue = "ADP_InvalidMetaDataValue";

		// Token: 0x04000A3D RID: 2621
		internal const string ADP_NotRowType = "ADP_NotRowType";

		// Token: 0x04000A3E RID: 2622
		internal const string ADP_ParameterConversionFailed = "ADP_ParameterConversionFailed";

		// Token: 0x04000A3F RID: 2623
		internal const string ADP_ParallelTransactionsNotSupported = "ADP_ParallelTransactionsNotSupported";

		// Token: 0x04000A40 RID: 2624
		internal const string ADP_TransactionZombied = "ADP_TransactionZombied";

		// Token: 0x04000A41 RID: 2625
		internal const string ADP_DbRecordReadOnly = "ADP_DbRecordReadOnly";

		// Token: 0x04000A42 RID: 2626
		internal const string ADP_DbDataUpdatableRecordReadOnly = "ADP_DbDataUpdatableRecordReadOnly";

		// Token: 0x04000A43 RID: 2627
		internal const string ADP_InvalidImplicitConversion = "ADP_InvalidImplicitConversion";

		// Token: 0x04000A44 RID: 2628
		internal const string ADP_InvalidBufferSizeOrIndex = "ADP_InvalidBufferSizeOrIndex";

		// Token: 0x04000A45 RID: 2629
		internal const string ADP_InvalidDataLength = "ADP_InvalidDataLength";

		// Token: 0x04000A46 RID: 2630
		internal const string ADP_InvalidDataLength2 = "ADP_InvalidDataLength2";

		// Token: 0x04000A47 RID: 2631
		internal const string ADP_NonSeqByteAccess = "ADP_NonSeqByteAccess";

		// Token: 0x04000A48 RID: 2632
		internal const string ADP_OffsetOutOfRangeException = "ADP_OffsetOutOfRangeException";

		// Token: 0x04000A49 RID: 2633
		internal const string ODBC_GetSchemaRestrictionRequired = "ODBC_GetSchemaRestrictionRequired";

		// Token: 0x04000A4A RID: 2634
		internal const string ADP_InvalidArgumentValue = "ADP_InvalidArgumentValue";

		// Token: 0x04000A4B RID: 2635
		internal const string ADP_OdbcNoTypesFromProvider = "ADP_OdbcNoTypesFromProvider";

		// Token: 0x04000A4C RID: 2636
		internal const string ADP_NullDataTable = "ADP_NullDataTable";

		// Token: 0x04000A4D RID: 2637
		internal const string ADP_NullDataSet = "ADP_NullDataSet";

		// Token: 0x04000A4E RID: 2638
		internal const string OdbcConnection_ConnectionStringTooLong = "OdbcConnection_ConnectionStringTooLong";

		// Token: 0x04000A4F RID: 2639
		internal const string Odbc_GetTypeMapping_UnknownType = "Odbc_GetTypeMapping_UnknownType";

		// Token: 0x04000A50 RID: 2640
		internal const string Odbc_UnknownSQLType = "Odbc_UnknownSQLType";

		// Token: 0x04000A51 RID: 2641
		internal const string Odbc_UnknownURTType = "Odbc_UnknownURTType";

		// Token: 0x04000A52 RID: 2642
		internal const string Odbc_NegativeArgument = "Odbc_NegativeArgument";

		// Token: 0x04000A53 RID: 2643
		internal const string Odbc_CantSetPropertyOnOpenConnection = "Odbc_CantSetPropertyOnOpenConnection";

		// Token: 0x04000A54 RID: 2644
		internal const string Odbc_NoMappingForSqlTransactionLevel = "Odbc_NoMappingForSqlTransactionLevel";

		// Token: 0x04000A55 RID: 2645
		internal const string Odbc_CantEnableConnectionpooling = "Odbc_CantEnableConnectionpooling";

		// Token: 0x04000A56 RID: 2646
		internal const string Odbc_CantAllocateEnvironmentHandle = "Odbc_CantAllocateEnvironmentHandle";

		// Token: 0x04000A57 RID: 2647
		internal const string Odbc_FailedToGetDescriptorHandle = "Odbc_FailedToGetDescriptorHandle";

		// Token: 0x04000A58 RID: 2648
		internal const string Odbc_NotInTransaction = "Odbc_NotInTransaction";

		// Token: 0x04000A59 RID: 2649
		internal const string Odbc_UnknownOdbcType = "Odbc_UnknownOdbcType";

		// Token: 0x04000A5A RID: 2650
		internal const string Odbc_NullData = "Odbc_NullData";

		// Token: 0x04000A5B RID: 2651
		internal const string Odbc_ExceptionMessage = "Odbc_ExceptionMessage";

		// Token: 0x04000A5C RID: 2652
		internal const string Odbc_ExceptionNoInfoMsg = "Odbc_ExceptionNoInfoMsg";

		// Token: 0x04000A5D RID: 2653
		internal const string Odbc_ConnectionClosed = "Odbc_ConnectionClosed";

		// Token: 0x04000A5E RID: 2654
		internal const string Odbc_OpenConnectionNoOwner = "Odbc_OpenConnectionNoOwner";

		// Token: 0x04000A5F RID: 2655
		internal const string Odbc_MDACWrongVersion = "Odbc_MDACWrongVersion";

		// Token: 0x04000A60 RID: 2656
		internal const string OleDb_MDACWrongVersion = "OleDb_MDACWrongVersion";

		// Token: 0x04000A61 RID: 2657
		internal const string OleDb_SchemaRowsetsNotSupported = "OleDb_SchemaRowsetsNotSupported";

		// Token: 0x04000A62 RID: 2658
		internal const string OleDb_NoErrorInformation2 = "OleDb_NoErrorInformation2";

		// Token: 0x04000A63 RID: 2659
		internal const string OleDb_NoErrorInformation = "OleDb_NoErrorInformation";

		// Token: 0x04000A64 RID: 2660
		internal const string OleDb_MDACNotAvailable = "OleDb_MDACNotAvailable";

		// Token: 0x04000A65 RID: 2661
		internal const string OleDb_MSDASQLNotSupported = "OleDb_MSDASQLNotSupported";

		// Token: 0x04000A66 RID: 2662
		internal const string OleDb_PossiblePromptNotUserInteractive = "OleDb_PossiblePromptNotUserInteractive";

		// Token: 0x04000A67 RID: 2663
		internal const string OleDb_ProviderUnavailable = "OleDb_ProviderUnavailable";

		// Token: 0x04000A68 RID: 2664
		internal const string OleDb_CommandTextNotSupported = "OleDb_CommandTextNotSupported";

		// Token: 0x04000A69 RID: 2665
		internal const string OleDb_TransactionsNotSupported = "OleDb_TransactionsNotSupported";

		// Token: 0x04000A6A RID: 2666
		internal const string OleDb_ConnectionStringSyntax = "OleDb_ConnectionStringSyntax";

		// Token: 0x04000A6B RID: 2667
		internal const string OleDb_AsynchronousNotSupported = "OleDb_AsynchronousNotSupported";

		// Token: 0x04000A6C RID: 2668
		internal const string OleDb_NoProviderSpecified = "OleDb_NoProviderSpecified";

		// Token: 0x04000A6D RID: 2669
		internal const string OleDb_InvalidProviderSpecified = "OleDb_InvalidProviderSpecified";

		// Token: 0x04000A6E RID: 2670
		internal const string OleDb_InvalidRestrictionsDbInfoKeywords = "OleDb_InvalidRestrictionsDbInfoKeywords";

		// Token: 0x04000A6F RID: 2671
		internal const string OleDb_InvalidRestrictionsDbInfoLiteral = "OleDb_InvalidRestrictionsDbInfoLiteral";

		// Token: 0x04000A70 RID: 2672
		internal const string OleDb_InvalidRestrictionsSchemaGuids = "OleDb_InvalidRestrictionsSchemaGuids";

		// Token: 0x04000A71 RID: 2673
		internal const string OleDb_NotSupportedSchemaTable = "OleDb_NotSupportedSchemaTable";

		// Token: 0x04000A72 RID: 2674
		internal const string OleDb_ConfigWrongNumberOfValues = "OleDb_ConfigWrongNumberOfValues";

		// Token: 0x04000A73 RID: 2675
		internal const string OleDb_ConfigUnableToLoadXmlMetaDataFile = "OleDb_ConfigUnableToLoadXmlMetaDataFile";

		// Token: 0x04000A74 RID: 2676
		internal const string OleDb_CommandParameterBadAccessor = "OleDb_CommandParameterBadAccessor";

		// Token: 0x04000A75 RID: 2677
		internal const string OleDb_CommandParameterCantConvertValue = "OleDb_CommandParameterCantConvertValue";

		// Token: 0x04000A76 RID: 2678
		internal const string OleDb_CommandParameterSignMismatch = "OleDb_CommandParameterSignMismatch";

		// Token: 0x04000A77 RID: 2679
		internal const string OleDb_CommandParameterDataOverflow = "OleDb_CommandParameterDataOverflow";

		// Token: 0x04000A78 RID: 2680
		internal const string OleDb_CommandParameterUnavailable = "OleDb_CommandParameterUnavailable";

		// Token: 0x04000A79 RID: 2681
		internal const string OleDb_CommandParameterDefault = "OleDb_CommandParameterDefault";

		// Token: 0x04000A7A RID: 2682
		internal const string OleDb_CommandParameterError = "OleDb_CommandParameterError";

		// Token: 0x04000A7B RID: 2683
		internal const string OleDb_BadStatus_ParamAcc = "OleDb_BadStatus_ParamAcc";

		// Token: 0x04000A7C RID: 2684
		internal const string OleDb_UninitializedParameters = "OleDb_UninitializedParameters";

		// Token: 0x04000A7D RID: 2685
		internal const string OleDb_NoProviderSupportForParameters = "OleDb_NoProviderSupportForParameters";

		// Token: 0x04000A7E RID: 2686
		internal const string OleDb_NoProviderSupportForSProcResetParameters = "OleDb_NoProviderSupportForSProcResetParameters";

		// Token: 0x04000A7F RID: 2687
		internal const string OleDb_CanNotDetermineDecimalSeparator = "OleDb_CanNotDetermineDecimalSeparator";

		// Token: 0x04000A80 RID: 2688
		internal const string OleDb_Fill_NotADODB = "OleDb_Fill_NotADODB";

		// Token: 0x04000A81 RID: 2689
		internal const string OleDb_Fill_EmptyRecordSet = "OleDb_Fill_EmptyRecordSet";

		// Token: 0x04000A82 RID: 2690
		internal const string OleDb_Fill_EmptyRecord = "OleDb_Fill_EmptyRecord";

		// Token: 0x04000A83 RID: 2691
		internal const string OleDb_ISourcesRowsetNotSupported = "OleDb_ISourcesRowsetNotSupported";

		// Token: 0x04000A84 RID: 2692
		internal const string OleDb_IDBInfoNotSupported = "OleDb_IDBInfoNotSupported";

		// Token: 0x04000A85 RID: 2693
		internal const string OleDb_PropertyNotSupported = "OleDb_PropertyNotSupported";

		// Token: 0x04000A86 RID: 2694
		internal const string OleDb_PropertyBadValue = "OleDb_PropertyBadValue";

		// Token: 0x04000A87 RID: 2695
		internal const string OleDb_PropertyBadOption = "OleDb_PropertyBadOption";

		// Token: 0x04000A88 RID: 2696
		internal const string OleDb_PropertyBadColumn = "OleDb_PropertyBadColumn";

		// Token: 0x04000A89 RID: 2697
		internal const string OleDb_PropertyNotAllSettable = "OleDb_PropertyNotAllSettable";

		// Token: 0x04000A8A RID: 2698
		internal const string OleDb_PropertyNotSettable = "OleDb_PropertyNotSettable";

		// Token: 0x04000A8B RID: 2699
		internal const string OleDb_PropertyNotSet = "OleDb_PropertyNotSet";

		// Token: 0x04000A8C RID: 2700
		internal const string OleDb_PropertyConflicting = "OleDb_PropertyConflicting";

		// Token: 0x04000A8D RID: 2701
		internal const string OleDb_PropertyNotAvailable = "OleDb_PropertyNotAvailable";

		// Token: 0x04000A8E RID: 2702
		internal const string OleDb_PropertyStatusUnknown = "OleDb_PropertyStatusUnknown";

		// Token: 0x04000A8F RID: 2703
		internal const string OleDb_BadAccessor = "OleDb_BadAccessor";

		// Token: 0x04000A90 RID: 2704
		internal const string OleDb_BadStatusRowAccessor = "OleDb_BadStatusRowAccessor";

		// Token: 0x04000A91 RID: 2705
		internal const string OleDb_CantConvertValue = "OleDb_CantConvertValue";

		// Token: 0x04000A92 RID: 2706
		internal const string OleDb_CantCreate = "OleDb_CantCreate";

		// Token: 0x04000A93 RID: 2707
		internal const string OleDb_DataOverflow = "OleDb_DataOverflow";

		// Token: 0x04000A94 RID: 2708
		internal const string OleDb_GVtUnknown = "OleDb_GVtUnknown";

		// Token: 0x04000A95 RID: 2709
		internal const string OleDb_SignMismatch = "OleDb_SignMismatch";

		// Token: 0x04000A96 RID: 2710
		internal const string OleDb_SVtUnknown = "OleDb_SVtUnknown";

		// Token: 0x04000A97 RID: 2711
		internal const string OleDb_Unavailable = "OleDb_Unavailable";

		// Token: 0x04000A98 RID: 2712
		internal const string OleDb_UnexpectedStatusValue = "OleDb_UnexpectedStatusValue";

		// Token: 0x04000A99 RID: 2713
		internal const string OleDb_ThreadApartmentState = "OleDb_ThreadApartmentState";

		// Token: 0x04000A9A RID: 2714
		internal const string OleDb_NoErrorMessage = "OleDb_NoErrorMessage";

		// Token: 0x04000A9B RID: 2715
		internal const string OleDb_FailedGetDescription = "OleDb_FailedGetDescription";

		// Token: 0x04000A9C RID: 2716
		internal const string OleDb_FailedGetSource = "OleDb_FailedGetSource";

		// Token: 0x04000A9D RID: 2717
		internal const string OleDb_DBBindingGetVector = "OleDb_DBBindingGetVector";

		// Token: 0x04000A9E RID: 2718
		internal const string ADP_InvalidMinMaxPoolSizeValues = "ADP_InvalidMinMaxPoolSizeValues";

		// Token: 0x04000A9F RID: 2719
		internal const string ADP_ObsoleteKeyword = "ADP_ObsoleteKeyword";

		// Token: 0x04000AA0 RID: 2720
		internal const string SQL_CannotGetDTCAddress = "SQL_CannotGetDTCAddress";

		// Token: 0x04000AA1 RID: 2721
		internal const string SQL_InvalidOptionLength = "SQL_InvalidOptionLength";

		// Token: 0x04000AA2 RID: 2722
		internal const string SQL_InvalidPacketSizeValue = "SQL_InvalidPacketSizeValue";

		// Token: 0x04000AA3 RID: 2723
		internal const string SQL_NullEmptyTransactionName = "SQL_NullEmptyTransactionName";

		// Token: 0x04000AA4 RID: 2724
		internal const string SQL_SnapshotNotSupported = "SQL_SnapshotNotSupported";

		// Token: 0x04000AA5 RID: 2725
		internal const string SQL_UserInstanceFailoverNotCompatible = "SQL_UserInstanceFailoverNotCompatible";

		// Token: 0x04000AA6 RID: 2726
		internal const string SQL_AuthenticationAndIntegratedSecurity = "SQL_AuthenticationAndIntegratedSecurity";

		// Token: 0x04000AA7 RID: 2727
		internal const string SQL_IntegratedWithUserIDAndPassword = "SQL_IntegratedWithUserIDAndPassword";

		// Token: 0x04000AA8 RID: 2728
		internal const string SQL_InteractiveWithPassword = "SQL_InteractiveWithPassword";

		// Token: 0x04000AA9 RID: 2729
		internal const string SQL_InteractiveWithoutUserID = "SQL_InteractiveWithoutUserID";

		// Token: 0x04000AAA RID: 2730
		internal const string SQL_SettingIntegratedWithCredential = "SQL_SettingIntegratedWithCredential";

		// Token: 0x04000AAB RID: 2731
		internal const string SQL_SettingCredentialWithIntegrated = "SQL_SettingCredentialWithIntegrated";

		// Token: 0x04000AAC RID: 2732
		internal const string SQL_EncryptionNotSupportedByClient = "SQL_EncryptionNotSupportedByClient";

		// Token: 0x04000AAD RID: 2733
		internal const string SQL_EncryptionNotSupportedByServer = "SQL_EncryptionNotSupportedByServer";

		// Token: 0x04000AAE RID: 2734
		internal const string SQL_InvalidSQLServerVersionUnknown = "SQL_InvalidSQLServerVersionUnknown";

		// Token: 0x04000AAF RID: 2735
		internal const string SQL_CannotModifyPropertyAsyncOperationInProgress = "SQL_CannotModifyPropertyAsyncOperationInProgress";

		// Token: 0x04000AB0 RID: 2736
		internal const string SQL_AsyncConnectionRequired = "SQL_AsyncConnectionRequired";

		// Token: 0x04000AB1 RID: 2737
		internal const string SQL_FatalTimeout = "SQL_FatalTimeout";

		// Token: 0x04000AB2 RID: 2738
		internal const string SQL_InstanceFailure = "SQL_InstanceFailure";

		// Token: 0x04000AB3 RID: 2739
		internal const string SQL_CredentialsNotProvided = "SQL_CredentialsNotProvided";

		// Token: 0x04000AB4 RID: 2740
		internal const string SQL_ChangePasswordArgumentMissing = "SQL_ChangePasswordArgumentMissing";

		// Token: 0x04000AB5 RID: 2741
		internal const string SQL_ChangePasswordConflictsWithSSPI = "SQL_ChangePasswordConflictsWithSSPI";

		// Token: 0x04000AB6 RID: 2742
		internal const string SQL_ChangePasswordUseOfUnallowedKey = "SQL_ChangePasswordUseOfUnallowedKey";

		// Token: 0x04000AB7 RID: 2743
		internal const string SQL_UnknownSysTxIsolationLevel = "SQL_UnknownSysTxIsolationLevel";

		// Token: 0x04000AB8 RID: 2744
		internal const string SQL_InvalidPartnerConfiguration = "SQL_InvalidPartnerConfiguration";

		// Token: 0x04000AB9 RID: 2745
		internal const string SQL_MarsUnsupportedOnConnection = "SQL_MarsUnsupportedOnConnection";

		// Token: 0x04000ABA RID: 2746
		internal const string SQL_ADALFailure = "SQL_ADALFailure";

		// Token: 0x04000ABB RID: 2747
		internal const string SQL_ADALInnerException = "SQL_ADALInnerException";

		// Token: 0x04000ABC RID: 2748
		internal const string SQL_ChangePasswordRequiresYukon = "SQL_ChangePasswordRequiresYukon";

		// Token: 0x04000ABD RID: 2749
		internal const string SQL_NonLocalSSEInstance = "SQL_NonLocalSSEInstance";

		// Token: 0x04000ABE RID: 2750
		internal const string SQL_UnsupportedAuthentication = "SQL_UnsupportedAuthentication";

		// Token: 0x04000ABF RID: 2751
		internal const string SQL_UnsupportedSqlAuthenticationMethod = "SQL_UnsupportedSqlAuthenticationMethod";

		// Token: 0x04000AC0 RID: 2752
		internal const string SQL_CannotCreateAuthProvider = "SQL_CannotCreateAuthProvider";

		// Token: 0x04000AC1 RID: 2753
		internal const string SQL_CannotCreateAuthInitializer = "SQL_CannotCreateAuthInitializer";

		// Token: 0x04000AC2 RID: 2754
		internal const string SQL_CannotInitializeAuthProvider = "SQL_CannotInitializeAuthProvider";

		// Token: 0x04000AC3 RID: 2755
		internal const string SQL_UnsupportedAuthenticationByProvider = "SQL_UnsupportedAuthenticationByProvider";

		// Token: 0x04000AC4 RID: 2756
		internal const string SQL_CannotFindAuthProvider = "SQL_CannotFindAuthProvider";

		// Token: 0x04000AC5 RID: 2757
		internal const string SQL_CannotGetAuthProviderConfig = "SQL_CannotGetAuthProviderConfig";

		// Token: 0x04000AC6 RID: 2758
		internal const string SQL_ParameterCannotBeEmpty = "SQL_ParameterCannotBeEmpty";

		// Token: 0x04000AC7 RID: 2759
		internal const string SQL_AsyncOperationCompleted = "SQL_AsyncOperationCompleted";

		// Token: 0x04000AC8 RID: 2760
		internal const string SQL_PendingBeginXXXExists = "SQL_PendingBeginXXXExists";

		// Token: 0x04000AC9 RID: 2761
		internal const string SQL_NonXmlResult = "SQL_NonXmlResult";

		// Token: 0x04000ACA RID: 2762
		internal const string SQL_NotificationsRequireYukon = "SQL_NotificationsRequireYukon";

		// Token: 0x04000ACB RID: 2763
		internal const string SQL_InvalidUdt3PartNameFormat = "SQL_InvalidUdt3PartNameFormat";

		// Token: 0x04000ACC RID: 2764
		internal const string SQL_InvalidParameterTypeNameFormat = "SQL_InvalidParameterTypeNameFormat";

		// Token: 0x04000ACD RID: 2765
		internal const string SQL_InvalidParameterNameLength = "SQL_InvalidParameterNameLength";

		// Token: 0x04000ACE RID: 2766
		internal const string SQL_PrecisionValueOutOfRange = "SQL_PrecisionValueOutOfRange";

		// Token: 0x04000ACF RID: 2767
		internal const string SQL_ScaleValueOutOfRange = "SQL_ScaleValueOutOfRange";

		// Token: 0x04000AD0 RID: 2768
		internal const string SQL_TimeScaleValueOutOfRange = "SQL_TimeScaleValueOutOfRange";

		// Token: 0x04000AD1 RID: 2769
		internal const string SQL_ParameterInvalidVariant = "SQL_ParameterInvalidVariant";

		// Token: 0x04000AD2 RID: 2770
		internal const string SQL_ParameterTypeNameRequired = "SQL_ParameterTypeNameRequired";

		// Token: 0x04000AD3 RID: 2771
		internal const string SQL_ADALInitializeError = "SQL_ADALInitializeError";

		// Token: 0x04000AD4 RID: 2772
		internal const string SQL_InvalidInternalPacketSize = "SQL_InvalidInternalPacketSize";

		// Token: 0x04000AD5 RID: 2773
		internal const string SQL_InvalidTDSVersion = "SQL_InvalidTDSVersion";

		// Token: 0x04000AD6 RID: 2774
		internal const string SQL_InvalidTDSPacketSize = "SQL_InvalidTDSPacketSize";

		// Token: 0x04000AD7 RID: 2775
		internal const string SQL_ParsingError = "SQL_ParsingError";

		// Token: 0x04000AD8 RID: 2776
		internal const string SQL_ParsingErrorWithState = "SQL_ParsingErrorWithState";

		// Token: 0x04000AD9 RID: 2777
		internal const string SQL_ParsingErrorValue = "SQL_ParsingErrorValue";

		// Token: 0x04000ADA RID: 2778
		internal const string SQL_ParsingErrorOffset = "SQL_ParsingErrorOffset";

		// Token: 0x04000ADB RID: 2779
		internal const string SQL_ParsingErrorFeatureId = "SQL_ParsingErrorFeatureId";

		// Token: 0x04000ADC RID: 2780
		internal const string SQL_ParsingErrorToken = "SQL_ParsingErrorToken";

		// Token: 0x04000ADD RID: 2781
		internal const string SQL_ParsingErrorLength = "SQL_ParsingErrorLength";

		// Token: 0x04000ADE RID: 2782
		internal const string SQL_ParsingErrorStatus = "SQL_ParsingErrorStatus";

		// Token: 0x04000ADF RID: 2783
		internal const string SQL_ParsingErrorAuthLibraryType = "SQL_ParsingErrorAuthLibraryType";

		// Token: 0x04000AE0 RID: 2784
		internal const string SQL_ConnectionLockedForBcpEvent = "SQL_ConnectionLockedForBcpEvent";

		// Token: 0x04000AE1 RID: 2785
		internal const string SQL_SNIPacketAllocationFailure = "SQL_SNIPacketAllocationFailure";

		// Token: 0x04000AE2 RID: 2786
		internal const string SQL_SmallDateTimeOverflow = "SQL_SmallDateTimeOverflow";

		// Token: 0x04000AE3 RID: 2787
		internal const string SQL_TimeOverflow = "SQL_TimeOverflow";

		// Token: 0x04000AE4 RID: 2788
		internal const string SQL_MoneyOverflow = "SQL_MoneyOverflow";

		// Token: 0x04000AE5 RID: 2789
		internal const string SQL_CultureIdError = "SQL_CultureIdError";

		// Token: 0x04000AE6 RID: 2790
		internal const string SQL_OperationCancelled = "SQL_OperationCancelled";

		// Token: 0x04000AE7 RID: 2791
		internal const string SQL_SevereError = "SQL_SevereError";

		// Token: 0x04000AE8 RID: 2792
		internal const string SQL_SSPIGenerateError = "SQL_SSPIGenerateError";

		// Token: 0x04000AE9 RID: 2793
		internal const string SQL_InvalidSSPIPacketSize = "SQL_InvalidSSPIPacketSize";

		// Token: 0x04000AEA RID: 2794
		internal const string SQL_SSPIInitializeError = "SQL_SSPIInitializeError";

		// Token: 0x04000AEB RID: 2795
		internal const string SQL_Timeout = "SQL_Timeout";

		// Token: 0x04000AEC RID: 2796
		internal const string SQL_Timeout_PreLogin_Begin = "SQL_Timeout_PreLogin_Begin";

		// Token: 0x04000AED RID: 2797
		internal const string SQL_Timeout_PreLogin_InitializeConnection = "SQL_Timeout_PreLogin_InitializeConnection";

		// Token: 0x04000AEE RID: 2798
		internal const string SQL_Timeout_PreLogin_SendHandshake = "SQL_Timeout_PreLogin_SendHandshake";

		// Token: 0x04000AEF RID: 2799
		internal const string SQL_Timeout_PreLogin_ConsumeHandshake = "SQL_Timeout_PreLogin_ConsumeHandshake";

		// Token: 0x04000AF0 RID: 2800
		internal const string SQL_Timeout_Login_Begin = "SQL_Timeout_Login_Begin";

		// Token: 0x04000AF1 RID: 2801
		internal const string SQL_Timeout_Login_ProcessConnectionAuth = "SQL_Timeout_Login_ProcessConnectionAuth";

		// Token: 0x04000AF2 RID: 2802
		internal const string SQL_Timeout_PostLogin = "SQL_Timeout_PostLogin";

		// Token: 0x04000AF3 RID: 2803
		internal const string SQL_Timeout_FailoverInfo = "SQL_Timeout_FailoverInfo";

		// Token: 0x04000AF4 RID: 2804
		internal const string SQL_Timeout_RoutingDestinationInfo = "SQL_Timeout_RoutingDestinationInfo";

		// Token: 0x04000AF5 RID: 2805
		internal const string SQL_Duration_PreLogin_Begin = "SQL_Duration_PreLogin_Begin";

		// Token: 0x04000AF6 RID: 2806
		internal const string SQL_Duration_PreLoginHandshake = "SQL_Duration_PreLoginHandshake";

		// Token: 0x04000AF7 RID: 2807
		internal const string SQL_Duration_Login_Begin = "SQL_Duration_Login_Begin";

		// Token: 0x04000AF8 RID: 2808
		internal const string SQL_Duration_Login_ProcessConnectionAuth = "SQL_Duration_Login_ProcessConnectionAuth";

		// Token: 0x04000AF9 RID: 2809
		internal const string SQL_Duration_PostLogin = "SQL_Duration_PostLogin";

		// Token: 0x04000AFA RID: 2810
		internal const string SQL_UserInstanceFailure = "SQL_UserInstanceFailure";

		// Token: 0x04000AFB RID: 2811
		internal const string SQL_ExceedsMaxDataLength = "SQL_ExceedsMaxDataLength";

		// Token: 0x04000AFC RID: 2812
		internal const string SQL_InvalidRead = "SQL_InvalidRead";

		// Token: 0x04000AFD RID: 2813
		internal const string SQL_NonBlobColumn = "SQL_NonBlobColumn";

		// Token: 0x04000AFE RID: 2814
		internal const string SQL_NonCharColumn = "SQL_NonCharColumn";

		// Token: 0x04000AFF RID: 2815
		internal const string SQL_StreamNotSupportOnColumnType = "SQL_StreamNotSupportOnColumnType";

		// Token: 0x04000B00 RID: 2816
		internal const string SQL_TextReaderNotSupportOnColumnType = "SQL_TextReaderNotSupportOnColumnType";

		// Token: 0x04000B01 RID: 2817
		internal const string SQL_XmlReaderNotSupportOnColumnType = "SQL_XmlReaderNotSupportOnColumnType";

		// Token: 0x04000B02 RID: 2818
		internal const string SQL_InvalidBufferSizeOrIndex = "SQL_InvalidBufferSizeOrIndex";

		// Token: 0x04000B03 RID: 2819
		internal const string SQL_InvalidDataLength = "SQL_InvalidDataLength";

		// Token: 0x04000B04 RID: 2820
		internal const string SQL_SqlResultSetClosed = "SQL_SqlResultSetClosed";

		// Token: 0x04000B05 RID: 2821
		internal const string SQL_SqlResultSetClosed2 = "SQL_SqlResultSetClosed2";

		// Token: 0x04000B06 RID: 2822
		internal const string SQL_SqlRecordReadOnly = "SQL_SqlRecordReadOnly";

		// Token: 0x04000B07 RID: 2823
		internal const string SQL_SqlRecordReadOnly2 = "SQL_SqlRecordReadOnly2";

		// Token: 0x04000B08 RID: 2824
		internal const string SQL_SqlResultSetRowDeleted = "SQL_SqlResultSetRowDeleted";

		// Token: 0x04000B09 RID: 2825
		internal const string SQL_SqlResultSetRowDeleted2 = "SQL_SqlResultSetRowDeleted2";

		// Token: 0x04000B0A RID: 2826
		internal const string SQL_SqlResultSetCommandNotInSameConnection = "SQL_SqlResultSetCommandNotInSameConnection";

		// Token: 0x04000B0B RID: 2827
		internal const string SQL_SqlResultSetNoAcceptableCursor = "SQL_SqlResultSetNoAcceptableCursor";

		// Token: 0x04000B0C RID: 2828
		internal const string SQL_SqlUpdatableRecordReadOnly = "SQL_SqlUpdatableRecordReadOnly";

		// Token: 0x04000B0D RID: 2829
		internal const string SQL_BulkLoadMappingInaccessible = "SQL_BulkLoadMappingInaccessible";

		// Token: 0x04000B0E RID: 2830
		internal const string SQL_BulkLoadMappingsNamesOrOrdinalsOnly = "SQL_BulkLoadMappingsNamesOrOrdinalsOnly";

		// Token: 0x04000B0F RID: 2831
		internal const string SQL_BulkLoadCannotConvertValue = "SQL_BulkLoadCannotConvertValue";

		// Token: 0x04000B10 RID: 2832
		internal const string SQL_BulkLoadNonMatchingColumnMapping = "SQL_BulkLoadNonMatchingColumnMapping";

		// Token: 0x04000B11 RID: 2833
		internal const string SQL_BulkLoadNonMatchingColumnName = "SQL_BulkLoadNonMatchingColumnName";

		// Token: 0x04000B12 RID: 2834
		internal const string SQL_BulkLoadStringTooLong = "SQL_BulkLoadStringTooLong";

		// Token: 0x04000B13 RID: 2835
		internal const string SQL_BulkLoadInvalidTimeout = "SQL_BulkLoadInvalidTimeout";

		// Token: 0x04000B14 RID: 2836
		internal const string SQL_BulkLoadInvalidVariantValue = "SQL_BulkLoadInvalidVariantValue";

		// Token: 0x04000B15 RID: 2837
		internal const string SQL_BulkLoadExistingTransaction = "SQL_BulkLoadExistingTransaction";

		// Token: 0x04000B16 RID: 2838
		internal const string SQL_BulkLoadNoCollation = "SQL_BulkLoadNoCollation";

		// Token: 0x04000B17 RID: 2839
		internal const string SQL_BulkLoadConflictingTransactionOption = "SQL_BulkLoadConflictingTransactionOption";

		// Token: 0x04000B18 RID: 2840
		internal const string SQL_BulkLoadInvalidOperationInsideEvent = "SQL_BulkLoadInvalidOperationInsideEvent";

		// Token: 0x04000B19 RID: 2841
		internal const string SQL_BulkLoadMissingDestinationTable = "SQL_BulkLoadMissingDestinationTable";

		// Token: 0x04000B1A RID: 2842
		internal const string SQL_BulkLoadInvalidDestinationTable = "SQL_BulkLoadInvalidDestinationTable";

		// Token: 0x04000B1B RID: 2843
		internal const string SQL_BulkLoadNotAllowDBNull = "SQL_BulkLoadNotAllowDBNull";

		// Token: 0x04000B1C RID: 2844
		internal const string Sql_BulkLoadLcidMismatch = "Sql_BulkLoadLcidMismatch";

		// Token: 0x04000B1D RID: 2845
		internal const string SQL_BulkLoadPendingOperation = "SQL_BulkLoadPendingOperation";

		// Token: 0x04000B1E RID: 2846
		internal const string SQL_ConnectionDoomed = "SQL_ConnectionDoomed";

		// Token: 0x04000B1F RID: 2847
		internal const string SQL_OpenResultCountExceeded = "SQL_OpenResultCountExceeded";

		// Token: 0x04000B20 RID: 2848
		internal const string GT_Disabled = "GT_Disabled";

		// Token: 0x04000B21 RID: 2849
		internal const string GT_UnsupportedSysTxVersion = "GT_UnsupportedSysTxVersion";

		// Token: 0x04000B22 RID: 2850
		internal const string SQL_BatchedUpdatesNotAvailableOnContextConnection = "SQL_BatchedUpdatesNotAvailableOnContextConnection";

		// Token: 0x04000B23 RID: 2851
		internal const string SQL_ContextAllowsLimitedKeywords = "SQL_ContextAllowsLimitedKeywords";

		// Token: 0x04000B24 RID: 2852
		internal const string SQL_ContextAllowsOnlyTypeSystem2005 = "SQL_ContextAllowsOnlyTypeSystem2005";

		// Token: 0x04000B25 RID: 2853
		internal const string SQL_ContextConnectionIsInUse = "SQL_ContextConnectionIsInUse";

		// Token: 0x04000B26 RID: 2854
		internal const string SQL_ContextUnavailableOutOfProc = "SQL_ContextUnavailableOutOfProc";

		// Token: 0x04000B27 RID: 2855
		internal const string SQL_ContextUnavailableWhileInProc = "SQL_ContextUnavailableWhileInProc";

		// Token: 0x04000B28 RID: 2856
		internal const string SQL_NestedTransactionScopesNotSupported = "SQL_NestedTransactionScopesNotSupported";

		// Token: 0x04000B29 RID: 2857
		internal const string SQL_NotAvailableOnContextConnection = "SQL_NotAvailableOnContextConnection";

		// Token: 0x04000B2A RID: 2858
		internal const string SQL_NotificationsNotAvailableOnContextConnection = "SQL_NotificationsNotAvailableOnContextConnection";

		// Token: 0x04000B2B RID: 2859
		internal const string SQL_UnexpectedSmiEvent = "SQL_UnexpectedSmiEvent";

		// Token: 0x04000B2C RID: 2860
		internal const string SQL_UserInstanceNotAvailableInProc = "SQL_UserInstanceNotAvailableInProc";

		// Token: 0x04000B2D RID: 2861
		internal const string SQL_ArgumentLengthMismatch = "SQL_ArgumentLengthMismatch";

		// Token: 0x04000B2E RID: 2862
		internal const string SQL_InvalidSqlDbTypeWithOneAllowedType = "SQL_InvalidSqlDbTypeWithOneAllowedType";

		// Token: 0x04000B2F RID: 2863
		internal const string SQL_PipeErrorRequiresSendEnd = "SQL_PipeErrorRequiresSendEnd";

		// Token: 0x04000B30 RID: 2864
		internal const string SQL_TooManyValues = "SQL_TooManyValues";

		// Token: 0x04000B31 RID: 2865
		internal const string SQL_StreamWriteNotSupported = "SQL_StreamWriteNotSupported";

		// Token: 0x04000B32 RID: 2866
		internal const string SQL_StreamReadNotSupported = "SQL_StreamReadNotSupported";

		// Token: 0x04000B33 RID: 2867
		internal const string SQL_StreamSeekNotSupported = "SQL_StreamSeekNotSupported";

		// Token: 0x04000B34 RID: 2868
		internal const string SQL_ExClientConnectionId = "SQL_ExClientConnectionId";

		// Token: 0x04000B35 RID: 2869
		internal const string SQL_ExErrorNumberStateClass = "SQL_ExErrorNumberStateClass";

		// Token: 0x04000B36 RID: 2870
		internal const string SQL_ExOriginalClientConnectionId = "SQL_ExOriginalClientConnectionId";

		// Token: 0x04000B37 RID: 2871
		internal const string SQL_ExRoutingDestination = "SQL_ExRoutingDestination";

		// Token: 0x04000B38 RID: 2872
		internal const string SqlMisc_NullString = "SqlMisc_NullString";

		// Token: 0x04000B39 RID: 2873
		internal const string SqlMisc_MessageString = "SqlMisc_MessageString";

		// Token: 0x04000B3A RID: 2874
		internal const string SqlMisc_ArithOverflowMessage = "SqlMisc_ArithOverflowMessage";

		// Token: 0x04000B3B RID: 2875
		internal const string SqlMisc_DivideByZeroMessage = "SqlMisc_DivideByZeroMessage";

		// Token: 0x04000B3C RID: 2876
		internal const string SqlMisc_NullValueMessage = "SqlMisc_NullValueMessage";

		// Token: 0x04000B3D RID: 2877
		internal const string SqlMisc_TruncationMessage = "SqlMisc_TruncationMessage";

		// Token: 0x04000B3E RID: 2878
		internal const string SqlMisc_DateTimeOverflowMessage = "SqlMisc_DateTimeOverflowMessage";

		// Token: 0x04000B3F RID: 2879
		internal const string SqlMisc_ConcatDiffCollationMessage = "SqlMisc_ConcatDiffCollationMessage";

		// Token: 0x04000B40 RID: 2880
		internal const string SqlMisc_CompareDiffCollationMessage = "SqlMisc_CompareDiffCollationMessage";

		// Token: 0x04000B41 RID: 2881
		internal const string SqlMisc_InvalidFlagMessage = "SqlMisc_InvalidFlagMessage";

		// Token: 0x04000B42 RID: 2882
		internal const string SqlMisc_NumeToDecOverflowMessage = "SqlMisc_NumeToDecOverflowMessage";

		// Token: 0x04000B43 RID: 2883
		internal const string SqlMisc_ConversionOverflowMessage = "SqlMisc_ConversionOverflowMessage";

		// Token: 0x04000B44 RID: 2884
		internal const string SqlMisc_InvalidDateTimeMessage = "SqlMisc_InvalidDateTimeMessage";

		// Token: 0x04000B45 RID: 2885
		internal const string SqlMisc_TimeZoneSpecifiedMessage = "SqlMisc_TimeZoneSpecifiedMessage";

		// Token: 0x04000B46 RID: 2886
		internal const string SqlMisc_InvalidArraySizeMessage = "SqlMisc_InvalidArraySizeMessage";

		// Token: 0x04000B47 RID: 2887
		internal const string SqlMisc_InvalidPrecScaleMessage = "SqlMisc_InvalidPrecScaleMessage";

		// Token: 0x04000B48 RID: 2888
		internal const string SqlMisc_FormatMessage = "SqlMisc_FormatMessage";

		// Token: 0x04000B49 RID: 2889
		internal const string SqlMisc_SqlTypeMessage = "SqlMisc_SqlTypeMessage";

		// Token: 0x04000B4A RID: 2890
		internal const string SqlMisc_LenTooLargeMessage = "SqlMisc_LenTooLargeMessage";

		// Token: 0x04000B4B RID: 2891
		internal const string SqlMisc_StreamErrorMessage = "SqlMisc_StreamErrorMessage";

		// Token: 0x04000B4C RID: 2892
		internal const string SqlMisc_StreamClosedMessage = "SqlMisc_StreamClosedMessage";

		// Token: 0x04000B4D RID: 2893
		internal const string SqlMisc_NoBufferMessage = "SqlMisc_NoBufferMessage";

		// Token: 0x04000B4E RID: 2894
		internal const string SqlMisc_SetNonZeroLenOnNullMessage = "SqlMisc_SetNonZeroLenOnNullMessage";

		// Token: 0x04000B4F RID: 2895
		internal const string SqlMisc_BufferInsufficientMessage = "SqlMisc_BufferInsufficientMessage";

		// Token: 0x04000B50 RID: 2896
		internal const string SqlMisc_WriteNonZeroOffsetOnNullMessage = "SqlMisc_WriteNonZeroOffsetOnNullMessage";

		// Token: 0x04000B51 RID: 2897
		internal const string SqlMisc_WriteOffsetLargerThanLenMessage = "SqlMisc_WriteOffsetLargerThanLenMessage";

		// Token: 0x04000B52 RID: 2898
		internal const string SqlMisc_TruncationMaxDataMessage = "SqlMisc_TruncationMaxDataMessage";

		// Token: 0x04000B53 RID: 2899
		internal const string SqlMisc_InvalidFirstDayMessage = "SqlMisc_InvalidFirstDayMessage";

		// Token: 0x04000B54 RID: 2900
		internal const string SqlMisc_NotFilledMessage = "SqlMisc_NotFilledMessage";

		// Token: 0x04000B55 RID: 2901
		internal const string SqlMisc_AlreadyFilledMessage = "SqlMisc_AlreadyFilledMessage";

		// Token: 0x04000B56 RID: 2902
		internal const string SqlMisc_ClosedXmlReaderMessage = "SqlMisc_ClosedXmlReaderMessage";

		// Token: 0x04000B57 RID: 2903
		internal const string SqlMisc_InvalidOpStreamClosed = "SqlMisc_InvalidOpStreamClosed";

		// Token: 0x04000B58 RID: 2904
		internal const string SqlMisc_InvalidOpStreamNonWritable = "SqlMisc_InvalidOpStreamNonWritable";

		// Token: 0x04000B59 RID: 2905
		internal const string SqlMisc_InvalidOpStreamNonReadable = "SqlMisc_InvalidOpStreamNonReadable";

		// Token: 0x04000B5A RID: 2906
		internal const string SqlMisc_InvalidOpStreamNonSeekable = "SqlMisc_InvalidOpStreamNonSeekable";

		// Token: 0x04000B5B RID: 2907
		internal const string SqlMisc_SubclassMustOverride = "SqlMisc_SubclassMustOverride";

		// Token: 0x04000B5C RID: 2908
		internal const string Sql_CanotCreateNormalizer = "Sql_CanotCreateNormalizer";

		// Token: 0x04000B5D RID: 2909
		internal const string Sql_InternalError = "Sql_InternalError";

		// Token: 0x04000B5E RID: 2910
		internal const string Sql_NullCommandText = "Sql_NullCommandText";

		// Token: 0x04000B5F RID: 2911
		internal const string Sql_MismatchedMetaDataDirectionArrayLengths = "Sql_MismatchedMetaDataDirectionArrayLengths";

		// Token: 0x04000B60 RID: 2912
		internal const string ADP_AdapterMappingExceptionMessage = "ADP_AdapterMappingExceptionMessage";

		// Token: 0x04000B61 RID: 2913
		internal const string ADP_DataAdapterExceptionMessage = "ADP_DataAdapterExceptionMessage";

		// Token: 0x04000B62 RID: 2914
		internal const string ADP_DBConcurrencyExceptionMessage = "ADP_DBConcurrencyExceptionMessage";

		// Token: 0x04000B63 RID: 2915
		internal const string ADP_OperationAborted = "ADP_OperationAborted";

		// Token: 0x04000B64 RID: 2916
		internal const string ADP_OperationAbortedExceptionMessage = "ADP_OperationAbortedExceptionMessage";

		// Token: 0x04000B65 RID: 2917
		internal const string DataAdapter_AcceptChangesDuringFill = "DataAdapter_AcceptChangesDuringFill";

		// Token: 0x04000B66 RID: 2918
		internal const string DataAdapter_AcceptChangesDuringUpdate = "DataAdapter_AcceptChangesDuringUpdate";

		// Token: 0x04000B67 RID: 2919
		internal const string DataAdapter_ContinueUpdateOnError = "DataAdapter_ContinueUpdateOnError";

		// Token: 0x04000B68 RID: 2920
		internal const string DataAdapter_FillLoadOption = "DataAdapter_FillLoadOption";

		// Token: 0x04000B69 RID: 2921
		internal const string DataAdapter_MissingMappingAction = "DataAdapter_MissingMappingAction";

		// Token: 0x04000B6A RID: 2922
		internal const string DataAdapter_MissingSchemaAction = "DataAdapter_MissingSchemaAction";

		// Token: 0x04000B6B RID: 2923
		internal const string DataAdapter_TableMappings = "DataAdapter_TableMappings";

		// Token: 0x04000B6C RID: 2924
		internal const string DataAdapter_FillError = "DataAdapter_FillError";

		// Token: 0x04000B6D RID: 2925
		internal const string DataAdapter_ReturnProviderSpecificTypes = "DataAdapter_ReturnProviderSpecificTypes";

		// Token: 0x04000B6E RID: 2926
		internal const string DataColumnMapping_DataSetColumn = "DataColumnMapping_DataSetColumn";

		// Token: 0x04000B6F RID: 2927
		internal const string DataColumnMapping_SourceColumn = "DataColumnMapping_SourceColumn";

		// Token: 0x04000B70 RID: 2928
		internal const string DataColumnMappings_Count = "DataColumnMappings_Count";

		// Token: 0x04000B71 RID: 2929
		internal const string DataColumnMappings_Item = "DataColumnMappings_Item";

		// Token: 0x04000B72 RID: 2930
		internal const string DataTableMapping_ColumnMappings = "DataTableMapping_ColumnMappings";

		// Token: 0x04000B73 RID: 2931
		internal const string DataTableMapping_DataSetTable = "DataTableMapping_DataSetTable";

		// Token: 0x04000B74 RID: 2932
		internal const string DataTableMapping_SourceTable = "DataTableMapping_SourceTable";

		// Token: 0x04000B75 RID: 2933
		internal const string DataTableMappings_Count = "DataTableMappings_Count";

		// Token: 0x04000B76 RID: 2934
		internal const string DataTableMappings_Item = "DataTableMappings_Item";

		// Token: 0x04000B77 RID: 2935
		internal const string DbDataAdapter_DeleteCommand = "DbDataAdapter_DeleteCommand";

		// Token: 0x04000B78 RID: 2936
		internal const string DbDataAdapter_InsertCommand = "DbDataAdapter_InsertCommand";

		// Token: 0x04000B79 RID: 2937
		internal const string DbDataAdapter_SelectCommand = "DbDataAdapter_SelectCommand";

		// Token: 0x04000B7A RID: 2938
		internal const string DbDataAdapter_UpdateCommand = "DbDataAdapter_UpdateCommand";

		// Token: 0x04000B7B RID: 2939
		internal const string DbDataAdapter_RowUpdated = "DbDataAdapter_RowUpdated";

		// Token: 0x04000B7C RID: 2940
		internal const string DbDataAdapter_RowUpdating = "DbDataAdapter_RowUpdating";

		// Token: 0x04000B7D RID: 2941
		internal const string DbDataAdapter_UpdateBatchSize = "DbDataAdapter_UpdateBatchSize";

		// Token: 0x04000B7E RID: 2942
		internal const string DbTable_Connection = "DbTable_Connection";

		// Token: 0x04000B7F RID: 2943
		internal const string DbTable_DeleteCommand = "DbTable_DeleteCommand";

		// Token: 0x04000B80 RID: 2944
		internal const string DbTable_InsertCommand = "DbTable_InsertCommand";

		// Token: 0x04000B81 RID: 2945
		internal const string DbTable_SelectCommand = "DbTable_SelectCommand";

		// Token: 0x04000B82 RID: 2946
		internal const string DbTable_UpdateCommand = "DbTable_UpdateCommand";

		// Token: 0x04000B83 RID: 2947
		internal const string DbTable_ReturnProviderSpecificTypes = "DbTable_ReturnProviderSpecificTypes";

		// Token: 0x04000B84 RID: 2948
		internal const string DbTable_TableMapping = "DbTable_TableMapping";

		// Token: 0x04000B85 RID: 2949
		internal const string DbTable_ConflictDetection = "DbTable_ConflictDetection";

		// Token: 0x04000B86 RID: 2950
		internal const string DbTable_UpdateBatchSize = "DbTable_UpdateBatchSize";

		// Token: 0x04000B87 RID: 2951
		internal const string DbConnectionString_ConnectionString = "DbConnectionString_ConnectionString";

		// Token: 0x04000B88 RID: 2952
		internal const string DbConnectionString_Driver = "DbConnectionString_Driver";

		// Token: 0x04000B89 RID: 2953
		internal const string DbConnectionString_DSN = "DbConnectionString_DSN";

		// Token: 0x04000B8A RID: 2954
		internal const string DbConnectionString_AdoNetPooler = "DbConnectionString_AdoNetPooler";

		// Token: 0x04000B8B RID: 2955
		internal const string DbConnectionString_FileName = "DbConnectionString_FileName";

		// Token: 0x04000B8C RID: 2956
		internal const string DbConnectionString_OleDbServices = "DbConnectionString_OleDbServices";

		// Token: 0x04000B8D RID: 2957
		internal const string DbConnectionString_Provider = "DbConnectionString_Provider";

		// Token: 0x04000B8E RID: 2958
		internal const string DbConnectionString_ApplicationName = "DbConnectionString_ApplicationName";

		// Token: 0x04000B8F RID: 2959
		internal const string DbConnectionString_AsynchronousProcessing = "DbConnectionString_AsynchronousProcessing";

		// Token: 0x04000B90 RID: 2960
		internal const string DbConnectionString_AttachDBFilename = "DbConnectionString_AttachDBFilename";

		// Token: 0x04000B91 RID: 2961
		internal const string DbConnectionString_ConnectTimeout = "DbConnectionString_ConnectTimeout";

		// Token: 0x04000B92 RID: 2962
		internal const string DbConnectionString_ConnectionReset = "DbConnectionString_ConnectionReset";

		// Token: 0x04000B93 RID: 2963
		internal const string DbConnectionString_ContextConnection = "DbConnectionString_ContextConnection";

		// Token: 0x04000B94 RID: 2964
		internal const string DbConnectionString_CurrentLanguage = "DbConnectionString_CurrentLanguage";

		// Token: 0x04000B95 RID: 2965
		internal const string DbConnectionString_DataSource = "DbConnectionString_DataSource";

		// Token: 0x04000B96 RID: 2966
		internal const string DbConnectionString_Encrypt = "DbConnectionString_Encrypt";

		// Token: 0x04000B97 RID: 2967
		internal const string DbConnectionString_Enlist = "DbConnectionString_Enlist";

		// Token: 0x04000B98 RID: 2968
		internal const string DbConnectionString_InitialCatalog = "DbConnectionString_InitialCatalog";

		// Token: 0x04000B99 RID: 2969
		internal const string DbConnectionString_FailoverPartner = "DbConnectionString_FailoverPartner";

		// Token: 0x04000B9A RID: 2970
		internal const string DbConnectionString_IntegratedSecurity = "DbConnectionString_IntegratedSecurity";

		// Token: 0x04000B9B RID: 2971
		internal const string DbConnectionString_LoadBalanceTimeout = "DbConnectionString_LoadBalanceTimeout";

		// Token: 0x04000B9C RID: 2972
		internal const string DbConnectionString_MaxPoolSize = "DbConnectionString_MaxPoolSize";

		// Token: 0x04000B9D RID: 2973
		internal const string DbConnectionString_MinPoolSize = "DbConnectionString_MinPoolSize";

		// Token: 0x04000B9E RID: 2974
		internal const string DbConnectionString_MultipleActiveResultSets = "DbConnectionString_MultipleActiveResultSets";

		// Token: 0x04000B9F RID: 2975
		internal const string DbConnectionString_MultiSubnetFailover = "DbConnectionString_MultiSubnetFailover";

		// Token: 0x04000BA0 RID: 2976
		internal const string DbConnectionString_TransparentNetworkIPResolution = "DbConnectionString_TransparentNetworkIPResolution";

		// Token: 0x04000BA1 RID: 2977
		internal const string DbConnectionString_NetworkLibrary = "DbConnectionString_NetworkLibrary";

		// Token: 0x04000BA2 RID: 2978
		internal const string DbConnectionString_PacketSize = "DbConnectionString_PacketSize";

		// Token: 0x04000BA3 RID: 2979
		internal const string DbConnectionString_Password = "DbConnectionString_Password";

		// Token: 0x04000BA4 RID: 2980
		internal const string DbConnectionString_PersistSecurityInfo = "DbConnectionString_PersistSecurityInfo";

		// Token: 0x04000BA5 RID: 2981
		internal const string DbConnectionString_Pooling = "DbConnectionString_Pooling";

		// Token: 0x04000BA6 RID: 2982
		internal const string DbConnectionString_Replication = "DbConnectionString_Replication";

		// Token: 0x04000BA7 RID: 2983
		internal const string DbConnectionString_TransactionBinding = "DbConnectionString_TransactionBinding";

		// Token: 0x04000BA8 RID: 2984
		internal const string DbConnectionString_TrustServerCertificate = "DbConnectionString_TrustServerCertificate";

		// Token: 0x04000BA9 RID: 2985
		internal const string DbConnectionString_TypeSystemVersion = "DbConnectionString_TypeSystemVersion";

		// Token: 0x04000BAA RID: 2986
		internal const string DbConnectionString_UserID = "DbConnectionString_UserID";

		// Token: 0x04000BAB RID: 2987
		internal const string DbConnectionString_UserInstance = "DbConnectionString_UserInstance";

		// Token: 0x04000BAC RID: 2988
		internal const string DbConnectionString_WorkstationID = "DbConnectionString_WorkstationID";

		// Token: 0x04000BAD RID: 2989
		internal const string DbConnectionString_ApplicationIntent = "DbConnectionString_ApplicationIntent";

		// Token: 0x04000BAE RID: 2990
		internal const string DbConnectionString_ConnectRetryCount = "DbConnectionString_ConnectRetryCount";

		// Token: 0x04000BAF RID: 2991
		internal const string DbConnectionString_ConnectRetryInterval = "DbConnectionString_ConnectRetryInterval";

		// Token: 0x04000BB0 RID: 2992
		internal const string DbConnectionString_Authentication = "DbConnectionString_Authentication";

		// Token: 0x04000BB1 RID: 2993
		internal const string OdbcConnection_ConnectionString = "OdbcConnection_ConnectionString";

		// Token: 0x04000BB2 RID: 2994
		internal const string OdbcConnection_ConnectionTimeout = "OdbcConnection_ConnectionTimeout";

		// Token: 0x04000BB3 RID: 2995
		internal const string OdbcConnection_Database = "OdbcConnection_Database";

		// Token: 0x04000BB4 RID: 2996
		internal const string OdbcConnection_DataSource = "OdbcConnection_DataSource";

		// Token: 0x04000BB5 RID: 2997
		internal const string OdbcConnection_Driver = "OdbcConnection_Driver";

		// Token: 0x04000BB6 RID: 2998
		internal const string OdbcConnection_ServerVersion = "OdbcConnection_ServerVersion";

		// Token: 0x04000BB7 RID: 2999
		internal const string OleDbConnection_ConnectionString = "OleDbConnection_ConnectionString";

		// Token: 0x04000BB8 RID: 3000
		internal const string OleDbConnection_ConnectionTimeout = "OleDbConnection_ConnectionTimeout";

		// Token: 0x04000BB9 RID: 3001
		internal const string OleDbConnection_Database = "OleDbConnection_Database";

		// Token: 0x04000BBA RID: 3002
		internal const string OleDbConnection_DataSource = "OleDbConnection_DataSource";

		// Token: 0x04000BBB RID: 3003
		internal const string OleDbConnection_Provider = "OleDbConnection_Provider";

		// Token: 0x04000BBC RID: 3004
		internal const string OleDbConnection_ServerVersion = "OleDbConnection_ServerVersion";

		// Token: 0x04000BBD RID: 3005
		internal const string SqlConnection_AccessToken = "SqlConnection_AccessToken";

		// Token: 0x04000BBE RID: 3006
		internal const string SqlConnection_Asynchronous = "SqlConnection_Asynchronous";

		// Token: 0x04000BBF RID: 3007
		internal const string SqlConnection_Replication = "SqlConnection_Replication";

		// Token: 0x04000BC0 RID: 3008
		internal const string SqlConnection_ConnectionString = "SqlConnection_ConnectionString";

		// Token: 0x04000BC1 RID: 3009
		internal const string SqlConnection_ConnectionTimeout = "SqlConnection_ConnectionTimeout";

		// Token: 0x04000BC2 RID: 3010
		internal const string SqlConnection_Database = "SqlConnection_Database";

		// Token: 0x04000BC3 RID: 3011
		internal const string SqlConnection_DataSource = "SqlConnection_DataSource";

		// Token: 0x04000BC4 RID: 3012
		internal const string SqlConnection_PacketSize = "SqlConnection_PacketSize";

		// Token: 0x04000BC5 RID: 3013
		internal const string SqlConnection_ServerVersion = "SqlConnection_ServerVersion";

		// Token: 0x04000BC6 RID: 3014
		internal const string SqlConnection_WorkstationId = "SqlConnection_WorkstationId";

		// Token: 0x04000BC7 RID: 3015
		internal const string SqlConnection_StatisticsEnabled = "SqlConnection_StatisticsEnabled";

		// Token: 0x04000BC8 RID: 3016
		internal const string SqlConnection_CustomColumnEncryptionKeyStoreProviders = "SqlConnection_CustomColumnEncryptionKeyStoreProviders";

		// Token: 0x04000BC9 RID: 3017
		internal const string SqlConnection_ClientConnectionId = "SqlConnection_ClientConnectionId";

		// Token: 0x04000BCA RID: 3018
		internal const string SqlConnection_Credential = "SqlConnection_Credential";

		// Token: 0x04000BCB RID: 3019
		internal const string DbConnection_InfoMessage = "DbConnection_InfoMessage";

		// Token: 0x04000BCC RID: 3020
		internal const string DbCommand_CommandText = "DbCommand_CommandText";

		// Token: 0x04000BCD RID: 3021
		internal const string DbCommand_CommandType = "DbCommand_CommandType";

		// Token: 0x04000BCE RID: 3022
		internal const string DbCommand_Connection = "DbCommand_Connection";

		// Token: 0x04000BCF RID: 3023
		internal const string DbCommand_Parameters = "DbCommand_Parameters";

		// Token: 0x04000BD0 RID: 3024
		internal const string DbCommand_Transaction = "DbCommand_Transaction";

		// Token: 0x04000BD1 RID: 3025
		internal const string DbCommand_UpdatedRowSource = "DbCommand_UpdatedRowSource";

		// Token: 0x04000BD2 RID: 3026
		internal const string DbCommand_StatementCompleted = "DbCommand_StatementCompleted";

		// Token: 0x04000BD3 RID: 3027
		internal const string SqlCommand_Notification = "SqlCommand_Notification";

		// Token: 0x04000BD4 RID: 3028
		internal const string SqlCommand_NotificationAutoEnlist = "SqlCommand_NotificationAutoEnlist";

		// Token: 0x04000BD5 RID: 3029
		internal const string DbCommandBuilder_ConflictOption = "DbCommandBuilder_ConflictOption";

		// Token: 0x04000BD6 RID: 3030
		internal const string DbCommandBuilder_CatalogLocation = "DbCommandBuilder_CatalogLocation";

		// Token: 0x04000BD7 RID: 3031
		internal const string DbCommandBuilder_CatalogSeparator = "DbCommandBuilder_CatalogSeparator";

		// Token: 0x04000BD8 RID: 3032
		internal const string DbCommandBuilder_SchemaSeparator = "DbCommandBuilder_SchemaSeparator";

		// Token: 0x04000BD9 RID: 3033
		internal const string DbCommandBuilder_QuotePrefix = "DbCommandBuilder_QuotePrefix";

		// Token: 0x04000BDA RID: 3034
		internal const string DbCommandBuilder_QuoteSuffix = "DbCommandBuilder_QuoteSuffix";

		// Token: 0x04000BDB RID: 3035
		internal const string DbCommandBuilder_DataAdapter = "DbCommandBuilder_DataAdapter";

		// Token: 0x04000BDC RID: 3036
		internal const string DbCommandBuilder_SchemaLocation = "DbCommandBuilder_SchemaLocation";

		// Token: 0x04000BDD RID: 3037
		internal const string DbCommandBuilder_SetAllValues = "DbCommandBuilder_SetAllValues";

		// Token: 0x04000BDE RID: 3038
		internal const string OdbcCommandBuilder_DataAdapter = "OdbcCommandBuilder_DataAdapter";

		// Token: 0x04000BDF RID: 3039
		internal const string OdbcCommandBuilder_QuotePrefix = "OdbcCommandBuilder_QuotePrefix";

		// Token: 0x04000BE0 RID: 3040
		internal const string OdbcCommandBuilder_QuoteSuffix = "OdbcCommandBuilder_QuoteSuffix";

		// Token: 0x04000BE1 RID: 3041
		internal const string OleDbCommandBuilder_DataAdapter = "OleDbCommandBuilder_DataAdapter";

		// Token: 0x04000BE2 RID: 3042
		internal const string OleDbCommandBuilder_DecimalSeparator = "OleDbCommandBuilder_DecimalSeparator";

		// Token: 0x04000BE3 RID: 3043
		internal const string OleDbCommandBuilder_QuotePrefix = "OleDbCommandBuilder_QuotePrefix";

		// Token: 0x04000BE4 RID: 3044
		internal const string OleDbCommandBuilder_QuoteSuffix = "OleDbCommandBuilder_QuoteSuffix";

		// Token: 0x04000BE5 RID: 3045
		internal const string SqlCommandBuilder_DataAdapter = "SqlCommandBuilder_DataAdapter";

		// Token: 0x04000BE6 RID: 3046
		internal const string SqlCommandBuilder_DecimalSeparator = "SqlCommandBuilder_DecimalSeparator";

		// Token: 0x04000BE7 RID: 3047
		internal const string SqlCommandBuilder_QuotePrefix = "SqlCommandBuilder_QuotePrefix";

		// Token: 0x04000BE8 RID: 3048
		internal const string SqlCommandBuilder_QuoteSuffix = "SqlCommandBuilder_QuoteSuffix";

		// Token: 0x04000BE9 RID: 3049
		internal const string DbDataParameter_Precision = "DbDataParameter_Precision";

		// Token: 0x04000BEA RID: 3050
		internal const string DbDataParameter_Scale = "DbDataParameter_Scale";

		// Token: 0x04000BEB RID: 3051
		internal const string OdbcParameter_OdbcType = "OdbcParameter_OdbcType";

		// Token: 0x04000BEC RID: 3052
		internal const string OleDbParameter_OleDbType = "OleDbParameter_OleDbType";

		// Token: 0x04000BED RID: 3053
		internal const string SqlParameter_ParameterName = "SqlParameter_ParameterName";

		// Token: 0x04000BEE RID: 3054
		internal const string SqlParameter_SqlDbType = "SqlParameter_SqlDbType";

		// Token: 0x04000BEF RID: 3055
		internal const string SqlParameter_TypeName = "SqlParameter_TypeName";

		// Token: 0x04000BF0 RID: 3056
		internal const string SqlParameter_Offset = "SqlParameter_Offset";

		// Token: 0x04000BF1 RID: 3057
		internal const string SqlParameter_XmlSchemaCollectionDatabase = "SqlParameter_XmlSchemaCollectionDatabase";

		// Token: 0x04000BF2 RID: 3058
		internal const string SqlParameter_XmlSchemaCollectionOwningSchema = "SqlParameter_XmlSchemaCollectionOwningSchema";

		// Token: 0x04000BF3 RID: 3059
		internal const string SqlParameter_XmlSchemaCollectionName = "SqlParameter_XmlSchemaCollectionName";

		// Token: 0x04000BF4 RID: 3060
		internal const string SqlParameter_UnsupportedTVPOutputParameter = "SqlParameter_UnsupportedTVPOutputParameter";

		// Token: 0x04000BF5 RID: 3061
		internal const string SqlParameter_DBNullNotSupportedForTVP = "SqlParameter_DBNullNotSupportedForTVP";

		// Token: 0x04000BF6 RID: 3062
		internal const string SqlParameter_InvalidTableDerivedPrecisionForTvp = "SqlParameter_InvalidTableDerivedPrecisionForTvp";

		// Token: 0x04000BF7 RID: 3063
		internal const string SqlParameter_UnexpectedTypeNameForNonStruct = "SqlParameter_UnexpectedTypeNameForNonStruct";

		// Token: 0x04000BF8 RID: 3064
		internal const string MetaType_SingleValuedStructNotSupported = "MetaType_SingleValuedStructNotSupported";

		// Token: 0x04000BF9 RID: 3065
		internal const string NullSchemaTableDataTypeNotSupported = "NullSchemaTableDataTypeNotSupported";

		// Token: 0x04000BFA RID: 3066
		internal const string InvalidSchemaTableOrdinals = "InvalidSchemaTableOrdinals";

		// Token: 0x04000BFB RID: 3067
		internal const string SQL_EnumeratedRecordMetaDataChanged = "SQL_EnumeratedRecordMetaDataChanged";

		// Token: 0x04000BFC RID: 3068
		internal const string SQL_EnumeratedRecordFieldCountChanged = "SQL_EnumeratedRecordFieldCountChanged";

		// Token: 0x04000BFD RID: 3069
		internal const string SQLUDT_MaxByteSizeValue = "SQLUDT_MaxByteSizeValue";

		// Token: 0x04000BFE RID: 3070
		internal const string SQLUDT_Unexpected = "SQLUDT_Unexpected";

		// Token: 0x04000BFF RID: 3071
		internal const string SQLUDT_InvalidDbId = "SQLUDT_InvalidDbId";

		// Token: 0x04000C00 RID: 3072
		internal const string SQLUDT_CantLoadAssembly = "SQLUDT_CantLoadAssembly";

		// Token: 0x04000C01 RID: 3073
		internal const string SQLUDT_InvalidUdtTypeName = "SQLUDT_InvalidUdtTypeName";

		// Token: 0x04000C02 RID: 3074
		internal const string SQLUDT_UnexpectedUdtTypeName = "SQLUDT_UnexpectedUdtTypeName";

		// Token: 0x04000C03 RID: 3075
		internal const string SQLUDT_InvalidSqlType = "SQLUDT_InvalidSqlType";

		// Token: 0x04000C04 RID: 3076
		internal const string SQLUDT_InWhereClause = "SQLUDT_InWhereClause";

		// Token: 0x04000C05 RID: 3077
		internal const string SqlUdt_InvalidUdtMessage = "SqlUdt_InvalidUdtMessage";

		// Token: 0x04000C06 RID: 3078
		internal const string SqlUdtReason_MultipleSerFormats = "SqlUdtReason_MultipleSerFormats";

		// Token: 0x04000C07 RID: 3079
		internal const string SqlUdtReason_CannotSupportNative = "SqlUdtReason_CannotSupportNative";

		// Token: 0x04000C08 RID: 3080
		internal const string SqlUdtReason_CannotSupportUserDefined = "SqlUdtReason_CannotSupportUserDefined";

		// Token: 0x04000C09 RID: 3081
		internal const string SqlUdtReason_NotSerializable = "SqlUdtReason_NotSerializable";

		// Token: 0x04000C0A RID: 3082
		internal const string SqlUdtReason_NoPublicConstructors = "SqlUdtReason_NoPublicConstructors";

		// Token: 0x04000C0B RID: 3083
		internal const string SqlUdtReason_NotNullable = "SqlUdtReason_NotNullable";

		// Token: 0x04000C0C RID: 3084
		internal const string SqlUdtReason_NoPublicConstructor = "SqlUdtReason_NoPublicConstructor";

		// Token: 0x04000C0D RID: 3085
		internal const string SqlUdtReason_NoUdtAttribute = "SqlUdtReason_NoUdtAttribute";

		// Token: 0x04000C0E RID: 3086
		internal const string SqlUdtReason_MaplessNotYetSupported = "SqlUdtReason_MaplessNotYetSupported";

		// Token: 0x04000C0F RID: 3087
		internal const string SqlUdtReason_ParseMethodMissing = "SqlUdtReason_ParseMethodMissing";

		// Token: 0x04000C10 RID: 3088
		internal const string SqlUdtReason_ToStringMethodMissing = "SqlUdtReason_ToStringMethodMissing";

		// Token: 0x04000C11 RID: 3089
		internal const string SqlUdtReason_NullPropertyMissing = "SqlUdtReason_NullPropertyMissing";

		// Token: 0x04000C12 RID: 3090
		internal const string SqlUdtReason_NativeFormatNoFieldSupport = "SqlUdtReason_NativeFormatNoFieldSupport";

		// Token: 0x04000C13 RID: 3091
		internal const string SqlUdtReason_TypeNotPublic = "SqlUdtReason_TypeNotPublic";

		// Token: 0x04000C14 RID: 3092
		internal const string SqlUdtReason_NativeUdtNotSequentialLayout = "SqlUdtReason_NativeUdtNotSequentialLayout";

		// Token: 0x04000C15 RID: 3093
		internal const string SqlUdtReason_NativeUdtMaxByteSize = "SqlUdtReason_NativeUdtMaxByteSize";

		// Token: 0x04000C16 RID: 3094
		internal const string SqlUdtReason_NonSerializableField = "SqlUdtReason_NonSerializableField";

		// Token: 0x04000C17 RID: 3095
		internal const string SqlUdtReason_NativeFormatExplictLayoutNotAllowed = "SqlUdtReason_NativeFormatExplictLayoutNotAllowed";

		// Token: 0x04000C18 RID: 3096
		internal const string SqlUdtReason_MultivaluedAssemblyId = "SqlUdtReason_MultivaluedAssemblyId";

		// Token: 0x04000C19 RID: 3097
		internal const string SQLTVP_TableTypeCanOnlyBeParameter = "SQLTVP_TableTypeCanOnlyBeParameter";

		// Token: 0x04000C1A RID: 3098
		internal const string SqlFileStream_InvalidPath = "SqlFileStream_InvalidPath";

		// Token: 0x04000C1B RID: 3099
		internal const string SqlFileStream_InvalidParameter = "SqlFileStream_InvalidParameter";

		// Token: 0x04000C1C RID: 3100
		internal const string SqlFileStream_FileAlreadyInTransaction = "SqlFileStream_FileAlreadyInTransaction";

		// Token: 0x04000C1D RID: 3101
		internal const string SqlFileStream_PathNotValidDiskResource = "SqlFileStream_PathNotValidDiskResource";

		// Token: 0x04000C1E RID: 3102
		internal const string SqlDelegatedTransaction_PromotionFailed = "SqlDelegatedTransaction_PromotionFailed";

		// Token: 0x04000C1F RID: 3103
		internal const string SqlDependency_SqlDependency = "SqlDependency_SqlDependency";

		// Token: 0x04000C20 RID: 3104
		internal const string SqlDependency_HasChanges = "SqlDependency_HasChanges";

		// Token: 0x04000C21 RID: 3105
		internal const string SqlDependency_Id = "SqlDependency_Id";

		// Token: 0x04000C22 RID: 3106
		internal const string SqlDependency_OnChange = "SqlDependency_OnChange";

		// Token: 0x04000C23 RID: 3107
		internal const string SqlDependency_AddCommandDependency = "SqlDependency_AddCommandDependency";

		// Token: 0x04000C24 RID: 3108
		internal const string SqlDependency_Duplicate = "SqlDependency_Duplicate";

		// Token: 0x04000C25 RID: 3109
		internal const string SQLNotify_AlreadyHasCommand = "SQLNotify_AlreadyHasCommand";

		// Token: 0x04000C26 RID: 3110
		internal const string SqlNotify_SqlDepCannotBeCreatedInProc = "SqlNotify_SqlDepCannotBeCreatedInProc";

		// Token: 0x04000C27 RID: 3111
		internal const string SqlDependency_DatabaseBrokerDisabled = "SqlDependency_DatabaseBrokerDisabled";

		// Token: 0x04000C28 RID: 3112
		internal const string SqlDependency_DefaultOptionsButNoStart = "SqlDependency_DefaultOptionsButNoStart";

		// Token: 0x04000C29 RID: 3113
		internal const string SqlDependency_EventNoDuplicate = "SqlDependency_EventNoDuplicate";

		// Token: 0x04000C2A RID: 3114
		internal const string SqlDependency_DuplicateStart = "SqlDependency_DuplicateStart";

		// Token: 0x04000C2B RID: 3115
		internal const string SqlDependency_IdMismatch = "SqlDependency_IdMismatch";

		// Token: 0x04000C2C RID: 3116
		internal const string SqlDependency_NoMatchingServerStart = "SqlDependency_NoMatchingServerStart";

		// Token: 0x04000C2D RID: 3117
		internal const string SqlDependency_NoMatchingServerDatabaseStart = "SqlDependency_NoMatchingServerDatabaseStart";

		// Token: 0x04000C2E RID: 3118
		internal const string SqlDependency_InvalidTimeout = "SqlDependency_InvalidTimeout";

		// Token: 0x04000C2F RID: 3119
		internal const string SQLNotify_ErrorFormat = "SQLNotify_ErrorFormat";

		// Token: 0x04000C30 RID: 3120
		internal const string SqlMetaData_NoMetadata = "SqlMetaData_NoMetadata";

		// Token: 0x04000C31 RID: 3121
		internal const string SqlMetaData_InvalidSqlDbTypeForConstructorFormat = "SqlMetaData_InvalidSqlDbTypeForConstructorFormat";

		// Token: 0x04000C32 RID: 3122
		internal const string SqlMetaData_NameTooLong = "SqlMetaData_NameTooLong";

		// Token: 0x04000C33 RID: 3123
		internal const string SqlMetaData_SpecifyBothSortOrderAndOrdinal = "SqlMetaData_SpecifyBothSortOrderAndOrdinal";

		// Token: 0x04000C34 RID: 3124
		internal const string SqlProvider_InvalidDataColumnType = "SqlProvider_InvalidDataColumnType";

		// Token: 0x04000C35 RID: 3125
		internal const string SqlProvider_InvalidDataColumnMaxLength = "SqlProvider_InvalidDataColumnMaxLength";

		// Token: 0x04000C36 RID: 3126
		internal const string SqlProvider_NotEnoughColumnsInStructuredType = "SqlProvider_NotEnoughColumnsInStructuredType";

		// Token: 0x04000C37 RID: 3127
		internal const string SqlProvider_DuplicateSortOrdinal = "SqlProvider_DuplicateSortOrdinal";

		// Token: 0x04000C38 RID: 3128
		internal const string SqlProvider_MissingSortOrdinal = "SqlProvider_MissingSortOrdinal";

		// Token: 0x04000C39 RID: 3129
		internal const string SqlProvider_SortOrdinalGreaterThanFieldCount = "SqlProvider_SortOrdinalGreaterThanFieldCount";

		// Token: 0x04000C3A RID: 3130
		internal const string IEnumerableOfSqlDataRecordHasNoRows = "IEnumerableOfSqlDataRecordHasNoRows";

		// Token: 0x04000C3B RID: 3131
		internal const string SqlPipe_CommandHookedUpToNonContextConnection = "SqlPipe_CommandHookedUpToNonContextConnection";

		// Token: 0x04000C3C RID: 3132
		internal const string SqlPipe_MessageTooLong = "SqlPipe_MessageTooLong";

		// Token: 0x04000C3D RID: 3133
		internal const string SqlPipe_IsBusy = "SqlPipe_IsBusy";

		// Token: 0x04000C3E RID: 3134
		internal const string SqlPipe_AlreadyHasAnOpenResultSet = "SqlPipe_AlreadyHasAnOpenResultSet";

		// Token: 0x04000C3F RID: 3135
		internal const string SqlPipe_DoesNotHaveAnOpenResultSet = "SqlPipe_DoesNotHaveAnOpenResultSet";

		// Token: 0x04000C40 RID: 3136
		internal const string SNI_PN0 = "SNI_PN0";

		// Token: 0x04000C41 RID: 3137
		internal const string SNI_PN1 = "SNI_PN1";

		// Token: 0x04000C42 RID: 3138
		internal const string SNI_PN2 = "SNI_PN2";

		// Token: 0x04000C43 RID: 3139
		internal const string SNI_PN3 = "SNI_PN3";

		// Token: 0x04000C44 RID: 3140
		internal const string SNI_PN4 = "SNI_PN4";

		// Token: 0x04000C45 RID: 3141
		internal const string SNI_PN5 = "SNI_PN5";

		// Token: 0x04000C46 RID: 3142
		internal const string SNI_PN6 = "SNI_PN6";

		// Token: 0x04000C47 RID: 3143
		internal const string SNI_PN7 = "SNI_PN7";

		// Token: 0x04000C48 RID: 3144
		internal const string SNI_PN8 = "SNI_PN8";

		// Token: 0x04000C49 RID: 3145
		internal const string SNI_PN9 = "SNI_PN9";

		// Token: 0x04000C4A RID: 3146
		internal const string SNI_PN10 = "SNI_PN10";

		// Token: 0x04000C4B RID: 3147
		internal const string SNI_ERROR_1 = "SNI_ERROR_1";

		// Token: 0x04000C4C RID: 3148
		internal const string SNI_ERROR_2 = "SNI_ERROR_2";

		// Token: 0x04000C4D RID: 3149
		internal const string SNI_ERROR_3 = "SNI_ERROR_3";

		// Token: 0x04000C4E RID: 3150
		internal const string SNI_ERROR_4 = "SNI_ERROR_4";

		// Token: 0x04000C4F RID: 3151
		internal const string SNI_ERROR_5 = "SNI_ERROR_5";

		// Token: 0x04000C50 RID: 3152
		internal const string SNI_ERROR_6 = "SNI_ERROR_6";

		// Token: 0x04000C51 RID: 3153
		internal const string SNI_ERROR_7 = "SNI_ERROR_7";

		// Token: 0x04000C52 RID: 3154
		internal const string SNI_ERROR_8 = "SNI_ERROR_8";

		// Token: 0x04000C53 RID: 3155
		internal const string SNI_ERROR_9 = "SNI_ERROR_9";

		// Token: 0x04000C54 RID: 3156
		internal const string SNI_ERROR_10 = "SNI_ERROR_10";

		// Token: 0x04000C55 RID: 3157
		internal const string SNI_ERROR_11 = "SNI_ERROR_11";

		// Token: 0x04000C56 RID: 3158
		internal const string SNI_ERROR_12 = "SNI_ERROR_12";

		// Token: 0x04000C57 RID: 3159
		internal const string SNI_ERROR_13 = "SNI_ERROR_13";

		// Token: 0x04000C58 RID: 3160
		internal const string SNI_ERROR_14 = "SNI_ERROR_14";

		// Token: 0x04000C59 RID: 3161
		internal const string SNI_ERROR_15 = "SNI_ERROR_15";

		// Token: 0x04000C5A RID: 3162
		internal const string SNI_ERROR_16 = "SNI_ERROR_16";

		// Token: 0x04000C5B RID: 3163
		internal const string SNI_ERROR_17 = "SNI_ERROR_17";

		// Token: 0x04000C5C RID: 3164
		internal const string SNI_ERROR_18 = "SNI_ERROR_18";

		// Token: 0x04000C5D RID: 3165
		internal const string SNI_ERROR_19 = "SNI_ERROR_19";

		// Token: 0x04000C5E RID: 3166
		internal const string SNI_ERROR_20 = "SNI_ERROR_20";

		// Token: 0x04000C5F RID: 3167
		internal const string SNI_ERROR_21 = "SNI_ERROR_21";

		// Token: 0x04000C60 RID: 3168
		internal const string SNI_ERROR_22 = "SNI_ERROR_22";

		// Token: 0x04000C61 RID: 3169
		internal const string SNI_ERROR_23 = "SNI_ERROR_23";

		// Token: 0x04000C62 RID: 3170
		internal const string SNI_ERROR_24 = "SNI_ERROR_24";

		// Token: 0x04000C63 RID: 3171
		internal const string SNI_ERROR_25 = "SNI_ERROR_25";

		// Token: 0x04000C64 RID: 3172
		internal const string SNI_ERROR_26 = "SNI_ERROR_26";

		// Token: 0x04000C65 RID: 3173
		internal const string SNI_ERROR_27 = "SNI_ERROR_27";

		// Token: 0x04000C66 RID: 3174
		internal const string SNI_ERROR_28 = "SNI_ERROR_28";

		// Token: 0x04000C67 RID: 3175
		internal const string SNI_ERROR_29 = "SNI_ERROR_29";

		// Token: 0x04000C68 RID: 3176
		internal const string SNI_ERROR_30 = "SNI_ERROR_30";

		// Token: 0x04000C69 RID: 3177
		internal const string SNI_ERROR_31 = "SNI_ERROR_31";

		// Token: 0x04000C6A RID: 3178
		internal const string SNI_ERROR_32 = "SNI_ERROR_32";

		// Token: 0x04000C6B RID: 3179
		internal const string SNI_ERROR_33 = "SNI_ERROR_33";

		// Token: 0x04000C6C RID: 3180
		internal const string SNI_ERROR_34 = "SNI_ERROR_34";

		// Token: 0x04000C6D RID: 3181
		internal const string SNI_ERROR_35 = "SNI_ERROR_35";

		// Token: 0x04000C6E RID: 3182
		internal const string SNI_ERROR_36 = "SNI_ERROR_36";

		// Token: 0x04000C6F RID: 3183
		internal const string SNI_ERROR_37 = "SNI_ERROR_37";

		// Token: 0x04000C70 RID: 3184
		internal const string SNI_ERROR_38 = "SNI_ERROR_38";

		// Token: 0x04000C71 RID: 3185
		internal const string SNI_ERROR_39 = "SNI_ERROR_39";

		// Token: 0x04000C72 RID: 3186
		internal const string SNI_ERROR_40 = "SNI_ERROR_40";

		// Token: 0x04000C73 RID: 3187
		internal const string SNI_ERROR_41 = "SNI_ERROR_41";

		// Token: 0x04000C74 RID: 3188
		internal const string SNI_ERROR_42 = "SNI_ERROR_42";

		// Token: 0x04000C75 RID: 3189
		internal const string SNI_ERROR_43 = "SNI_ERROR_43";

		// Token: 0x04000C76 RID: 3190
		internal const string SNI_ERROR_44 = "SNI_ERROR_44";

		// Token: 0x04000C77 RID: 3191
		internal const string SNI_ERROR_47 = "SNI_ERROR_47";

		// Token: 0x04000C78 RID: 3192
		internal const string SNI_ERROR_48 = "SNI_ERROR_48";

		// Token: 0x04000C79 RID: 3193
		internal const string SNI_ERROR_49 = "SNI_ERROR_49";

		// Token: 0x04000C7A RID: 3194
		internal const string SNI_ERROR_50 = "SNI_ERROR_50";

		// Token: 0x04000C7B RID: 3195
		internal const string SNI_ERROR_51 = "SNI_ERROR_51";

		// Token: 0x04000C7C RID: 3196
		internal const string SNI_ERROR_52 = "SNI_ERROR_52";

		// Token: 0x04000C7D RID: 3197
		internal const string SNI_ERROR_53 = "SNI_ERROR_53";

		// Token: 0x04000C7E RID: 3198
		internal const string SNI_ERROR_54 = "SNI_ERROR_54";

		// Token: 0x04000C7F RID: 3199
		internal const string SNI_ERROR_55 = "SNI_ERROR_55";

		// Token: 0x04000C80 RID: 3200
		internal const string SNI_ERROR_56 = "SNI_ERROR_56";

		// Token: 0x04000C81 RID: 3201
		internal const string SNI_ERROR_57 = "SNI_ERROR_57";

		// Token: 0x04000C82 RID: 3202
		internal const string Snix_Connect = "Snix_Connect";

		// Token: 0x04000C83 RID: 3203
		internal const string Snix_PreLoginBeforeSuccessfullWrite = "Snix_PreLoginBeforeSuccessfullWrite";

		// Token: 0x04000C84 RID: 3204
		internal const string Snix_PreLogin = "Snix_PreLogin";

		// Token: 0x04000C85 RID: 3205
		internal const string Snix_LoginSspi = "Snix_LoginSspi";

		// Token: 0x04000C86 RID: 3206
		internal const string Snix_Login = "Snix_Login";

		// Token: 0x04000C87 RID: 3207
		internal const string Snix_EnableMars = "Snix_EnableMars";

		// Token: 0x04000C88 RID: 3208
		internal const string Snix_AutoEnlist = "Snix_AutoEnlist";

		// Token: 0x04000C89 RID: 3209
		internal const string Snix_GetMarsSession = "Snix_GetMarsSession";

		// Token: 0x04000C8A RID: 3210
		internal const string Snix_Execute = "Snix_Execute";

		// Token: 0x04000C8B RID: 3211
		internal const string Snix_Read = "Snix_Read";

		// Token: 0x04000C8C RID: 3212
		internal const string Snix_Close = "Snix_Close";

		// Token: 0x04000C8D RID: 3213
		internal const string Snix_SendRows = "Snix_SendRows";

		// Token: 0x04000C8E RID: 3214
		internal const string Snix_ProcessSspi = "Snix_ProcessSspi";

		// Token: 0x04000C8F RID: 3215
		internal const string LocalDB_CreateFailed = "LocalDB_CreateFailed";

		// Token: 0x04000C90 RID: 3216
		internal const string LocalDB_BadConfigSectionType = "LocalDB_BadConfigSectionType";

		// Token: 0x04000C91 RID: 3217
		internal const string LocalDB_FailedGetDLLHandle = "LocalDB_FailedGetDLLHandle";

		// Token: 0x04000C92 RID: 3218
		internal const string LocalDB_MethodNotFound = "LocalDB_MethodNotFound";

		// Token: 0x04000C93 RID: 3219
		internal const string LocalDB_UnobtainableMessage = "LocalDB_UnobtainableMessage";

		// Token: 0x04000C94 RID: 3220
		internal const string LocalDB_InvalidVersion = "LocalDB_InvalidVersion";

		// Token: 0x04000C95 RID: 3221
		internal const string TCE_InvalidKeyEncryptionAlgorithm = "TCE_InvalidKeyEncryptionAlgorithm";

		// Token: 0x04000C96 RID: 3222
		internal const string TCE_InvalidKeyEncryptionAlgorithmSysErr = "TCE_InvalidKeyEncryptionAlgorithmSysErr";

		// Token: 0x04000C97 RID: 3223
		internal const string TCE_NullKeyEncryptionAlgorithm = "TCE_NullKeyEncryptionAlgorithm";

		// Token: 0x04000C98 RID: 3224
		internal const string TCE_NullKeyEncryptionAlgorithmSysErr = "TCE_NullKeyEncryptionAlgorithmSysErr";

		// Token: 0x04000C99 RID: 3225
		internal const string TCE_EmptyColumnEncryptionKey = "TCE_EmptyColumnEncryptionKey";

		// Token: 0x04000C9A RID: 3226
		internal const string TCE_NullColumnEncryptionKey = "TCE_NullColumnEncryptionKey";

		// Token: 0x04000C9B RID: 3227
		internal const string TCE_EmptyEncryptedColumnEncryptionKey = "TCE_EmptyEncryptedColumnEncryptionKey";

		// Token: 0x04000C9C RID: 3228
		internal const string TCE_NullEncryptedColumnEncryptionKey = "TCE_NullEncryptedColumnEncryptionKey";

		// Token: 0x04000C9D RID: 3229
		internal const string TCE_LargeCertificatePathLength = "TCE_LargeCertificatePathLength";

		// Token: 0x04000C9E RID: 3230
		internal const string TCE_LargeCertificatePathLengthSysErr = "TCE_LargeCertificatePathLengthSysErr";

		// Token: 0x04000C9F RID: 3231
		internal const string TCE_NullCertificatePath = "TCE_NullCertificatePath";

		// Token: 0x04000CA0 RID: 3232
		internal const string TCE_NullCertificatePathSysErr = "TCE_NullCertificatePathSysErr";

		// Token: 0x04000CA1 RID: 3233
		internal const string TCE_NullCspPath = "TCE_NullCspPath";

		// Token: 0x04000CA2 RID: 3234
		internal const string TCE_NullCspPathSysErr = "TCE_NullCspPathSysErr";

		// Token: 0x04000CA3 RID: 3235
		internal const string TCE_NullCngPath = "TCE_NullCngPath";

		// Token: 0x04000CA4 RID: 3236
		internal const string TCE_NullCngPathSysErr = "TCE_NullCngPathSysErr";

		// Token: 0x04000CA5 RID: 3237
		internal const string TCE_InvalidCertificatePath = "TCE_InvalidCertificatePath";

		// Token: 0x04000CA6 RID: 3238
		internal const string TCE_InvalidCertificatePathSysErr = "TCE_InvalidCertificatePathSysErr";

		// Token: 0x04000CA7 RID: 3239
		internal const string TCE_InvalidCspPath = "TCE_InvalidCspPath";

		// Token: 0x04000CA8 RID: 3240
		internal const string TCE_InvalidCspPathSysErr = "TCE_InvalidCspPathSysErr";

		// Token: 0x04000CA9 RID: 3241
		internal const string TCE_InvalidCngPath = "TCE_InvalidCngPath";

		// Token: 0x04000CAA RID: 3242
		internal const string TCE_InvalidCngPathSysErr = "TCE_InvalidCngPathSysErr";

		// Token: 0x04000CAB RID: 3243
		internal const string TCE_InvalidCertificateLocation = "TCE_InvalidCertificateLocation";

		// Token: 0x04000CAC RID: 3244
		internal const string TCE_InvalidCertificateLocationSysErr = "TCE_InvalidCertificateLocationSysErr";

		// Token: 0x04000CAD RID: 3245
		internal const string TCE_InvalidCertificateStore = "TCE_InvalidCertificateStore";

		// Token: 0x04000CAE RID: 3246
		internal const string TCE_InvalidCertificateStoreSysErr = "TCE_InvalidCertificateStoreSysErr";

		// Token: 0x04000CAF RID: 3247
		internal const string TCE_EmptyCertificateThumbprint = "TCE_EmptyCertificateThumbprint";

		// Token: 0x04000CB0 RID: 3248
		internal const string TCE_EmptyCertificateThumbprintSysErr = "TCE_EmptyCertificateThumbprintSysErr";

		// Token: 0x04000CB1 RID: 3249
		internal const string TCE_EmptyCspName = "TCE_EmptyCspName";

		// Token: 0x04000CB2 RID: 3250
		internal const string TCE_EmptyCspNameSysErr = "TCE_EmptyCspNameSysErr";

		// Token: 0x04000CB3 RID: 3251
		internal const string TCE_EmptyCngName = "TCE_EmptyCngName";

		// Token: 0x04000CB4 RID: 3252
		internal const string TCE_EmptyCngNameSysErr = "TCE_EmptyCngNameSysErr";

		// Token: 0x04000CB5 RID: 3253
		internal const string TCE_EmptyCspKeyId = "TCE_EmptyCspKeyId";

		// Token: 0x04000CB6 RID: 3254
		internal const string TCE_EmptyCspKeyIdSysErr = "TCE_EmptyCspKeyIdSysErr";

		// Token: 0x04000CB7 RID: 3255
		internal const string TCE_EmptyCngKeyId = "TCE_EmptyCngKeyId";

		// Token: 0x04000CB8 RID: 3256
		internal const string TCE_EmptyCngKeyIdSysErr = "TCE_EmptyCngKeyIdSysErr";

		// Token: 0x04000CB9 RID: 3257
		internal const string TCE_InvalidCspName = "TCE_InvalidCspName";

		// Token: 0x04000CBA RID: 3258
		internal const string TCE_InvalidCspNameSysErr = "TCE_InvalidCspNameSysErr";

		// Token: 0x04000CBB RID: 3259
		internal const string TCE_InvalidCspKeyId = "TCE_InvalidCspKeyId";

		// Token: 0x04000CBC RID: 3260
		internal const string TCE_InvalidCspKeyIdSysErr = "TCE_InvalidCspKeyIdSysErr";

		// Token: 0x04000CBD RID: 3261
		internal const string TCE_InvalidCngKey = "TCE_InvalidCngKey";

		// Token: 0x04000CBE RID: 3262
		internal const string TCE_InvalidCngKeySysErr = "TCE_InvalidCngKeySysErr";

		// Token: 0x04000CBF RID: 3263
		internal const string TCE_CertificateNotFound = "TCE_CertificateNotFound";

		// Token: 0x04000CC0 RID: 3264
		internal const string TCE_CertificateNotFoundSysErr = "TCE_CertificateNotFoundSysErr";

		// Token: 0x04000CC1 RID: 3265
		internal const string TCE_InvalidAlgorithmVersionInEncryptedCEK = "TCE_InvalidAlgorithmVersionInEncryptedCEK";

		// Token: 0x04000CC2 RID: 3266
		internal const string TCE_InvalidCiphertextLengthInEncryptedCEK = "TCE_InvalidCiphertextLengthInEncryptedCEK";

		// Token: 0x04000CC3 RID: 3267
		internal const string TCE_InvalidCiphertextLengthInEncryptedCEKCsp = "TCE_InvalidCiphertextLengthInEncryptedCEKCsp";

		// Token: 0x04000CC4 RID: 3268
		internal const string TCE_InvalidCiphertextLengthInEncryptedCEKCng = "TCE_InvalidCiphertextLengthInEncryptedCEKCng";

		// Token: 0x04000CC5 RID: 3269
		internal const string TCE_InvalidSignatureInEncryptedCEK = "TCE_InvalidSignatureInEncryptedCEK";

		// Token: 0x04000CC6 RID: 3270
		internal const string TCE_InvalidSignatureInEncryptedCEKCsp = "TCE_InvalidSignatureInEncryptedCEKCsp";

		// Token: 0x04000CC7 RID: 3271
		internal const string TCE_InvalidSignatureInEncryptedCEKCng = "TCE_InvalidSignatureInEncryptedCEKCng";

		// Token: 0x04000CC8 RID: 3272
		internal const string TCE_InvalidCertificateSignature = "TCE_InvalidCertificateSignature";

		// Token: 0x04000CC9 RID: 3273
		internal const string TCE_InvalidSignature = "TCE_InvalidSignature";

		// Token: 0x04000CCA RID: 3274
		internal const string TCE_CertificateWithNoPrivateKey = "TCE_CertificateWithNoPrivateKey";

		// Token: 0x04000CCB RID: 3275
		internal const string TCE_CertificateWithNoPrivateKeySysErr = "TCE_CertificateWithNoPrivateKeySysErr";

		// Token: 0x04000CCC RID: 3276
		internal const string TCE_NullColumnEncryptionKeySysErr = "TCE_NullColumnEncryptionKeySysErr";

		// Token: 0x04000CCD RID: 3277
		internal const string TCE_InvalidKeySize = "TCE_InvalidKeySize";

		// Token: 0x04000CCE RID: 3278
		internal const string TCE_InvalidEncryptionType = "TCE_InvalidEncryptionType";

		// Token: 0x04000CCF RID: 3279
		internal const string TCE_NullPlainText = "TCE_NullPlainText";

		// Token: 0x04000CD0 RID: 3280
		internal const string TCE_VeryLargeCiphertext = "TCE_VeryLargeCiphertext";

		// Token: 0x04000CD1 RID: 3281
		internal const string TCE_NullCipherText = "TCE_NullCipherText";

		// Token: 0x04000CD2 RID: 3282
		internal const string TCE_InvalidCipherTextSize = "TCE_InvalidCipherTextSize";

		// Token: 0x04000CD3 RID: 3283
		internal const string TCE_InvalidAlgorithmVersion = "TCE_InvalidAlgorithmVersion";

		// Token: 0x04000CD4 RID: 3284
		internal const string TCE_InvalidAuthenticationTag = "TCE_InvalidAuthenticationTag";

		// Token: 0x04000CD5 RID: 3285
		internal const string TCE_NullColumnEncryptionAlgorithm = "TCE_NullColumnEncryptionAlgorithm";

		// Token: 0x04000CD6 RID: 3286
		internal const string TCE_UnexpectedDescribeParamFormatParameterMetadata = "TCE_UnexpectedDescribeParamFormatParameterMetadata";

		// Token: 0x04000CD7 RID: 3287
		internal const string TCE_UnexpectedDescribeParamFormatAttestationInfo = "TCE_UnexpectedDescribeParamFormatAttestationInfo";

		// Token: 0x04000CD8 RID: 3288
		internal const string TCE_InvalidEncryptionKeyOrdinalEnclaveMetadata = "TCE_InvalidEncryptionKeyOrdinalEnclaveMetadata";

		// Token: 0x04000CD9 RID: 3289
		internal const string TCE_InvalidEncryptionKeyOrdinalParameterMetadata = "TCE_InvalidEncryptionKeyOrdinalParameterMetadata";

		// Token: 0x04000CDA RID: 3290
		internal const string TCE_MultipleRowsReturnedForAttestationInfo = "TCE_MultipleRowsReturnedForAttestationInfo";

		// Token: 0x04000CDB RID: 3291
		internal const string TCE_ParamEncryptionMetaDataMissing = "TCE_ParamEncryptionMetaDataMissing";

		// Token: 0x04000CDC RID: 3292
		internal const string TCE_ProcEncryptionMetaDataMissing = "TCE_ProcEncryptionMetaDataMissing";

		// Token: 0x04000CDD RID: 3293
		internal const string TCE_ParamEncryptionFailed = "TCE_ParamEncryptionFailed";

		// Token: 0x04000CDE RID: 3294
		internal const string TCE_ColumnDecryptionFailed = "TCE_ColumnDecryptionFailed";

		// Token: 0x04000CDF RID: 3295
		internal const string TCE_ParamDecryptionFailed = "TCE_ParamDecryptionFailed";

		// Token: 0x04000CE0 RID: 3296
		internal const string TCE_ColumnMasterKeySignatureVerificationFailed = "TCE_ColumnMasterKeySignatureVerificationFailed";

		// Token: 0x04000CE1 RID: 3297
		internal const string TCE_ColumnMasterKeySignatureNotFound = "TCE_ColumnMasterKeySignatureNotFound";

		// Token: 0x04000CE2 RID: 3298
		internal const string TCE_UnableToVerifyColumnMasterKeySignature = "TCE_UnableToVerifyColumnMasterKeySignature";

		// Token: 0x04000CE3 RID: 3299
		internal const string TCE_UnknownColumnEncryptionAlgorithm = "TCE_UnknownColumnEncryptionAlgorithm";

		// Token: 0x04000CE4 RID: 3300
		internal const string TCE_UnknownColumnEncryptionAlgorithmId = "TCE_UnknownColumnEncryptionAlgorithmId";

		// Token: 0x04000CE5 RID: 3301
		internal const string TCE_UnsupportedNormalizationVersion = "TCE_UnsupportedNormalizationVersion";

		// Token: 0x04000CE6 RID: 3302
		internal const string TCE_UnrecognizedKeyStoreProviderName = "TCE_UnrecognizedKeyStoreProviderName";

		// Token: 0x04000CE7 RID: 3303
		internal const string TCE_KeyDecryptionFailedCertStore = "TCE_KeyDecryptionFailedCertStore";

		// Token: 0x04000CE8 RID: 3304
		internal const string TCE_UntrustedKeyPath = "TCE_UntrustedKeyPath";

		// Token: 0x04000CE9 RID: 3305
		internal const string TCE_KeyDecryptionFailed = "TCE_KeyDecryptionFailed";

		// Token: 0x04000CEA RID: 3306
		internal const string TCE_UnsupportedDatatype = "TCE_UnsupportedDatatype";

		// Token: 0x04000CEB RID: 3307
		internal const string TCE_DecryptionFailed = "TCE_DecryptionFailed";

		// Token: 0x04000CEC RID: 3308
		internal const string TCE_ExceptionWhenGeneratingEnclavePackage = "TCE_ExceptionWhenGeneratingEnclavePackage";

		// Token: 0x04000CED RID: 3309
		internal const string TCE_InvalidKeyIdUnableToCastToUnsignedShort = "TCE_InvalidKeyIdUnableToCastToUnsignedShort";

		// Token: 0x04000CEE RID: 3310
		internal const string TCE_InvalidDatabaseIdUnableToCastToUnsignedInt = "TCE_InvalidDatabaseIdUnableToCastToUnsignedInt";

		// Token: 0x04000CEF RID: 3311
		internal const string TCE_InvalidAttestationParameterUnableToConvertToUnsignedInt = "TCE_InvalidAttestationParameterUnableToConvertToUnsignedInt";

		// Token: 0x04000CF0 RID: 3312
		internal const string TCE_InvalidKeyStoreProviderName = "TCE_InvalidKeyStoreProviderName";

		// Token: 0x04000CF1 RID: 3313
		internal const string TCE_FailedToEncryptRegisterRulesBytePackage = "TCE_FailedToEncryptRegisterRulesBytePackage";

		// Token: 0x04000CF2 RID: 3314
		internal const string TCE_OffsetOutOfBounds = "TCE_OffsetOutOfBounds";

		// Token: 0x04000CF3 RID: 3315
		internal const string TCE_InsufficientBuffer = "TCE_InsufficientBuffer";

		// Token: 0x04000CF4 RID: 3316
		internal const string TCE_ColumnEncryptionKeysNotFound = "TCE_ColumnEncryptionKeysNotFound";

		// Token: 0x04000CF5 RID: 3317
		internal const string TCE_NullEnclaveSessionDuringQueryExecution = "TCE_NullEnclaveSessionDuringQueryExecution";

		// Token: 0x04000CF6 RID: 3318
		internal const string TCE_NullEnclavePackageForEnclaveBasedQuery = "TCE_NullEnclavePackageForEnclaveBasedQuery";

		// Token: 0x04000CF7 RID: 3319
		internal const string TCE_AttestationInfoNotReturnedFromSQLServer = "TCE_AttestationInfoNotReturnedFromSQLServer";

		// Token: 0x04000CF8 RID: 3320
		internal const string TCE_UnableToEstablishSecureChannel = "TCE_UnableToEstablishSecureChannel";

		// Token: 0x04000CF9 RID: 3321
		internal const string TCE_NullArgumentInConstructorInternal = "TCE_NullArgumentInConstructorInternal";

		// Token: 0x04000CFA RID: 3322
		internal const string TCE_EmptyArgumentInConstructorInternal = "TCE_EmptyArgumentInConstructorInternal";

		// Token: 0x04000CFB RID: 3323
		internal const string TCE_NullArgumentInternal = "TCE_NullArgumentInternal";

		// Token: 0x04000CFC RID: 3324
		internal const string TCE_EmptyArgumentInternal = "TCE_EmptyArgumentInternal";

		// Token: 0x04000CFD RID: 3325
		internal const string TCE_DbConnectionString_EnclaveAttestationUrl = "TCE_DbConnectionString_EnclaveAttestationUrl";

		// Token: 0x04000CFE RID: 3326
		internal const string TCE_CannotGetSqlColumnEncryptionEnclaveProviderConfig = "TCE_CannotGetSqlColumnEncryptionEnclaveProviderConfig";

		// Token: 0x04000CFF RID: 3327
		internal const string TCE_CannotCreateSqlColumnEncryptionEnclaveProvider = "TCE_CannotCreateSqlColumnEncryptionEnclaveProvider";

		// Token: 0x04000D00 RID: 3328
		internal const string TCE_SqlColumnEncryptionEnclaveProviderNameCannotBeEmpty = "TCE_SqlColumnEncryptionEnclaveProviderNameCannotBeEmpty";

		// Token: 0x04000D01 RID: 3329
		internal const string TCE_NoAttestationUrlSpecifiedForEnclaveBasedQuerySpDescribe = "TCE_NoAttestationUrlSpecifiedForEnclaveBasedQuerySpDescribe";

		// Token: 0x04000D02 RID: 3330
		internal const string TCE_NoAttestationUrlSpecifiedForEnclaveBasedQueryGeneratingEnclavePackage = "TCE_NoAttestationUrlSpecifiedForEnclaveBasedQueryGeneratingEnclavePackage";

		// Token: 0x04000D03 RID: 3331
		internal const string TCE_EnclaveTypeNullForEnclaveBasedQuery = "TCE_EnclaveTypeNullForEnclaveBasedQuery";

		// Token: 0x04000D04 RID: 3332
		internal const string TCE_EnclaveProvidersNotConfiguredForEnclaveBasedQuery = "TCE_EnclaveProvidersNotConfiguredForEnclaveBasedQuery";

		// Token: 0x04000D05 RID: 3333
		internal const string TCE_EnclaveProviderNotFound = "TCE_EnclaveProviderNotFound";

		// Token: 0x04000D06 RID: 3334
		internal const string TCE_NullEnclaveSessionReturnedFromProvider = "TCE_NullEnclaveSessionReturnedFromProvider";

		// Token: 0x04000D07 RID: 3335
		internal const string TCE_ParamInvalidForceColumnEncryptionSetting = "TCE_ParamInvalidForceColumnEncryptionSetting";

		// Token: 0x04000D08 RID: 3336
		internal const string TCE_ParamUnExpectedEncryptionMetadata = "TCE_ParamUnExpectedEncryptionMetadata";

		// Token: 0x04000D09 RID: 3337
		internal const string TCE_NotSupportedByServer = "TCE_NotSupportedByServer";

		// Token: 0x04000D0A RID: 3338
		internal const string TCE_EnclaveComputationsNotSupported = "TCE_EnclaveComputationsNotSupported";

		// Token: 0x04000D0B RID: 3339
		internal const string TCE_EnclaveTypeNotReturned = "TCE_EnclaveTypeNotReturned";

		// Token: 0x04000D0C RID: 3340
		internal const string TCE_BatchedUpdateColumnEncryptionSettingMismatch = "TCE_BatchedUpdateColumnEncryptionSettingMismatch";

		// Token: 0x04000D0D RID: 3341
		internal const string TCE_StreamNotSupportOnEncryptedColumn = "TCE_StreamNotSupportOnEncryptedColumn";

		// Token: 0x04000D0E RID: 3342
		internal const string TCE_SequentialAccessNotSupportedOnEncryptedColumn = "TCE_SequentialAccessNotSupportedOnEncryptedColumn";

		// Token: 0x04000D0F RID: 3343
		internal const string TCE_CanOnlyCallOnce = "TCE_CanOnlyCallOnce";

		// Token: 0x04000D10 RID: 3344
		internal const string TCE_NullCustomKeyStoreProviderDictionary = "TCE_NullCustomKeyStoreProviderDictionary";

		// Token: 0x04000D11 RID: 3345
		internal const string TCE_InvalidCustomKeyStoreProviderName = "TCE_InvalidCustomKeyStoreProviderName";

		// Token: 0x04000D12 RID: 3346
		internal const string TCE_NullProviderValue = "TCE_NullProviderValue";

		// Token: 0x04000D13 RID: 3347
		internal const string TCE_EmptyProviderName = "TCE_EmptyProviderName";

		// Token: 0x04000D14 RID: 3348
		internal const string TCE_SqlCommand_ColumnEncryptionSetting = "TCE_SqlCommand_ColumnEncryptionSetting";

		// Token: 0x04000D15 RID: 3349
		internal const string TCE_DbConnectionString_ColumnEncryptionSetting = "TCE_DbConnectionString_ColumnEncryptionSetting";

		// Token: 0x04000D16 RID: 3350
		internal const string TCE_SqlParameter_ForceColumnEncryption = "TCE_SqlParameter_ForceColumnEncryption";

		// Token: 0x04000D17 RID: 3351
		internal const string TCE_SqlConnection_TrustedColumnMasterKeyPaths = "TCE_SqlConnection_TrustedColumnMasterKeyPaths";

		// Token: 0x04000D18 RID: 3352
		internal const string SQLROR_RecursiveRoutingNotSupported = "SQLROR_RecursiveRoutingNotSupported";

		// Token: 0x04000D19 RID: 3353
		internal const string SQLROR_FailoverNotSupported = "SQLROR_FailoverNotSupported";

		// Token: 0x04000D1A RID: 3354
		internal const string SQLROR_UnexpectedRoutingInfo = "SQLROR_UnexpectedRoutingInfo";

		// Token: 0x04000D1B RID: 3355
		internal const string SQLROR_InvalidRoutingInfo = "SQLROR_InvalidRoutingInfo";

		// Token: 0x04000D1C RID: 3356
		internal const string SQLROR_TimeoutAfterRoutingInfo = "SQLROR_TimeoutAfterRoutingInfo";

		// Token: 0x04000D1D RID: 3357
		internal const string SQLCR_InvalidConnectRetryCountValue = "SQLCR_InvalidConnectRetryCountValue";

		// Token: 0x04000D1E RID: 3358
		internal const string SQLCR_InvalidConnectRetryIntervalValue = "SQLCR_InvalidConnectRetryIntervalValue";

		// Token: 0x04000D1F RID: 3359
		internal const string SQLCR_NextAttemptWillExceedQueryTimeout = "SQLCR_NextAttemptWillExceedQueryTimeout";

		// Token: 0x04000D20 RID: 3360
		internal const string SQLCR_EncryptionChanged = "SQLCR_EncryptionChanged";

		// Token: 0x04000D21 RID: 3361
		internal const string SQLCR_TDSVestionNotPreserved = "SQLCR_TDSVestionNotPreserved";

		// Token: 0x04000D22 RID: 3362
		internal const string SQLCR_AllAttemptsFailed = "SQLCR_AllAttemptsFailed";

		// Token: 0x04000D23 RID: 3363
		internal const string SQLCR_UnrecoverableServer = "SQLCR_UnrecoverableServer";

		// Token: 0x04000D24 RID: 3364
		internal const string SQLCR_UnrecoverableClient = "SQLCR_UnrecoverableClient";

		// Token: 0x04000D25 RID: 3365
		internal const string SQLCR_NoCRAckAtReconnection = "SQLCR_NoCRAckAtReconnection";

		// Token: 0x04000D26 RID: 3366
		internal const string DbConnectionString_PoolBlockingPeriod = "DbConnectionString_PoolBlockingPeriod";

		// Token: 0x04000D27 RID: 3367
		internal const string AZURESQL_GenericEndpoint = "AZURESQL_GenericEndpoint";

		// Token: 0x04000D28 RID: 3368
		internal const string AZURESQL_GermanEndpoint = "AZURESQL_GermanEndpoint";

		// Token: 0x04000D29 RID: 3369
		internal const string AZURESQL_UsGovEndpoint = "AZURESQL_UsGovEndpoint";

		// Token: 0x04000D2A RID: 3370
		internal const string AZURESQL_ChinaEndpoint = "AZURESQL_ChinaEndpoint";

		// Token: 0x04000D2B RID: 3371
		internal const string TCE_SqlConnection_ColumnEncryptionQueryMetadataCacheEnabled = "TCE_SqlConnection_ColumnEncryptionQueryMetadataCacheEnabled";

		// Token: 0x04000D2C RID: 3372
		internal const string TCE_SqlConnection_ColumnEncryptionKeyCacheTtl = "TCE_SqlConnection_ColumnEncryptionKeyCacheTtl";

		// Token: 0x04000D2D RID: 3373
		internal const string SQL_Timeout_Execution = "SQL_Timeout_Execution";

		// Token: 0x04000D2E RID: 3374
		private static Res loader;

		// Token: 0x04000D2F RID: 3375
		private ResourceManager resources;
	}
}
