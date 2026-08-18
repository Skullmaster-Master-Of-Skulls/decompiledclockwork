using System;

namespace Oracle.DataAccess.Client
{
	// Token: 0x020000F5 RID: 245
	internal class ErrRes
	{
		// Token: 0x060008F5 RID: 2293 RVA: 0x0005882C File Offset: 0x0005782C
		private ErrRes()
		{
		}

		// Token: 0x040007A8 RID: 1960
		internal const int PC_DESC_CATEGORY = -2801;

		// Token: 0x040007A9 RID: 1961
		internal const int PC_DESC_HARDCONNECTS = -2802;

		// Token: 0x040007AA RID: 1962
		internal const int PC_DESC_HARDDISCONNECTS = -2803;

		// Token: 0x040007AB RID: 1963
		internal const int PC_DESC_SOFTCONNECTS = -2804;

		// Token: 0x040007AC RID: 1964
		internal const int PC_DESC_SOFTDISCONNECTS = -2805;

		// Token: 0x040007AD RID: 1965
		internal const int PC_DESC_ACTIVECONNPOOLS = -2806;

		// Token: 0x040007AE RID: 1966
		internal const int PC_DESC_ACTIVECONNS = -2807;

		// Token: 0x040007AF RID: 1967
		internal const int PC_DESC_FREECONNS = -2808;

		// Token: 0x040007B0 RID: 1968
		internal const int PC_DESC_INACTIVECONNPOOLS = -2809;

		// Token: 0x040007B1 RID: 1969
		internal const int PC_DESC_NONPOOLEDCONNS = -2810;

		// Token: 0x040007B2 RID: 1970
		internal const int PC_DESC_POOLEDCONNS = -2811;

		// Token: 0x040007B3 RID: 1971
		internal const int PC_DESC_RECLAIMEDCONNS = -2812;

		// Token: 0x040007B4 RID: 1972
		internal const int PC_DESC_STASISCONNS = -2813;

		// Token: 0x040007B5 RID: 1973
		internal const int UDT_INV_CUSTOM_TYPE_MAPPING = -2901;

		// Token: 0x040007B6 RID: 1974
		internal const int UDT_INV_CUSTOM_TYPE = -2902;

		// Token: 0x040007B7 RID: 1975
		internal const int UDT_TYPE_CONVERSION_NOTSUPPORTED = -2903;

		// Token: 0x040007B8 RID: 1976
		internal const int UDT_TYPE_MAPPING_NOTSUPPORTED = -2904;

		// Token: 0x040007B9 RID: 1977
		internal const int UDT_TYPE_MAPPING_NOTSPECIFIED = -2905;

		// Token: 0x040007BA RID: 1978
		internal static int EVEN_VALUE_PARAM_REQUIRED = -2300;

		// Token: 0x040007BB RID: 1979
		internal static int OS_MEMALLOC_FAIL = -10;

		// Token: 0x040007BC RID: 1980
		internal static int INIT_DLL_VERSION_MISMATCH = -11;

		// Token: 0x040007BD RID: 1981
		internal static int CON_TIMEOUT_EXCEEDED = -1000;

		// Token: 0x040007BE RID: 1982
		internal static int CON_CLOSED = -1001;

		// Token: 0x040007BF RID: 1983
		internal static int CON_INVALID_ISO_LEVEL = -1002;

		// Token: 0x040007C0 RID: 1984
		internal static int CON_STR_NOT_UPDATABLE = -1003;

		// Token: 0x040007C1 RID: 1985
		internal static int CON_REOPENED = -1004;

		// Token: 0x040007C2 RID: 1986
		internal static int CON_ALREADY_OPEN = -1005;

		// Token: 0x040007C3 RID: 1987
		internal static int CON_ALREADY_TXNED = -1006;

		// Token: 0x040007C4 RID: 1988
		internal static int CON_STR_NOT_WELL_FORMED = -1007;

		// Token: 0x040007C5 RID: 1989
		internal static int CON_STR_INVALID_ATTRIB = -1008;

		// Token: 0x040007C6 RID: 1990
		internal static int CON_STR_INVALID_VALUE = -1009;

		// Token: 0x040007C7 RID: 1991
		internal static int CON_DIFFERENT_CONNECTIONS = -1010;

		// Token: 0x040007C8 RID: 1992
		internal static int CON_PSPE_RULE_VIOLATION = -1011;

		// Token: 0x040007C9 RID: 1993
		internal static int CON_GS_COLL_NOT_DEFINED = -1030;

		// Token: 0x040007CA RID: 1994
		internal static int CON_GS_COLL_NOT_SUPPORTED = -1031;

		// Token: 0x040007CB RID: 1995
		internal static int CON_GS_MORE_RESTRICTIONS = -1032;

		// Token: 0x040007CC RID: 1996
		internal static int CON_GS_QUERY_FAILED = -1033;

		// Token: 0x040007CD RID: 1997
		internal static int CON_GS_NO_POPULATION_STRING = -1034;

		// Token: 0x040007CE RID: 1998
		internal static int CON_GS_NO_METADATA_STREAM = -1035;

		// Token: 0x040007CF RID: 1999
		internal static int CON_GS_NO_CUSTOM_FILE = -1036;

		// Token: 0x040007D0 RID: 2000
		internal static int CON_MTS_ENLIST_FAIL = -1050;

		// Token: 0x040007D1 RID: 2001
		internal static int CON_MTS_LOAD_FAIL = -1051;

		// Token: 0x040007D2 RID: 2002
		internal static int PRM_INVALID_BIND = -1201;

		// Token: 0x040007D3 RID: 2003
		internal static int ODP_INVALID_VALUE = -1202;

		// Token: 0x040007D4 RID: 2004
		internal static int PRMCOL_ALREADY_ADDED = -1300;

		// Token: 0x040007D5 RID: 2005
		internal static int CMD_TYPE_NOT_SUPPORTED = -1450;

		// Token: 0x040007D6 RID: 2006
		internal static int DR_NULL_COL_DATA = -1501;

		// Token: 0x040007D7 RID: 2007
		internal static int DR_INV_COL_NAME = -1502;

		// Token: 0x040007D8 RID: 2008
		internal static int DR_INV_COL_INDEX = -1503;

		// Token: 0x040007D9 RID: 2009
		internal static int DR_INV_DATA_REQ = -1504;

		// Token: 0x040007DA RID: 2010
		internal static int DAC_PK_REQUIRED = -1556;

		// Token: 0x040007DB RID: 2011
		internal static int DA_FORWARD_ONLY = -1600;

		// Token: 0x040007DC RID: 2012
		internal static int DA_INV_SAFE_TYPE = -1601;

		// Token: 0x040007DD RID: 2013
		internal static int DA_BU_BIND_VIOLATION = -1602;

		// Token: 0x040007DE RID: 2014
		internal static int BLR_MULTITABLE_DS = -1701;

		// Token: 0x040007DF RID: 2015
		internal static int BLR_NO_PRIMARYKEY = -1702;

		// Token: 0x040007E0 RID: 2016
		internal static int ODP_NOT_SUPPORTED = -1703;

		// Token: 0x040007E1 RID: 2017
		internal static int BLR_PRM_NOT_SUPPORTED = -1704;

		// Token: 0x040007E2 RID: 2018
		internal static int LOB_BFILE_ALREADY_OPEN = -2201;

		// Token: 0x040007E3 RID: 2019
		internal static int TYP_COMPARE_COLLATION = -2501;

		// Token: 0x040007E4 RID: 2020
		internal static int TYP_NULLVALUE = -2502;

		// Token: 0x040007E5 RID: 2021
		internal static int TYP_GETDOTNETTYPE_FAIL = -2601;

		// Token: 0x040007E6 RID: 2022
		internal static int TYP_OFFSET_NOT_SUPPORTED = -2602;

		// Token: 0x040007E7 RID: 2023
		internal static int NTFN_CMD_ALREADY_EXIST = -2651;

		// Token: 0x040007E8 RID: 2024
		internal static int NTFN_LISTENER_ALREADY_STARTED = -2652;

		// Token: 0x040007E9 RID: 2025
		internal static int NTFN_REG_NOTVALID = -2653;

		// Token: 0x040007EA RID: 2026
		internal static int NTFN_CHGNTFN_DBVERSION = -2654;

		// Token: 0x040007EB RID: 2027
		internal static int NTFN_DEP_NOTEXIST = -2655;

		// Token: 0x040007EC RID: 2028
		internal static int INT_ERR = -3000;

		// Token: 0x040007ED RID: 2029
		internal static int INT_OCI_INVALID_HANDLE = -3001;

		// Token: 0x040007EE RID: 2030
		internal static int INT_OCI_NO_DATA = -3002;

		// Token: 0x040007EF RID: 2031
		internal static int INT_OCI_NEED_DATA = -3003;

		// Token: 0x040007F0 RID: 2032
		internal static int INT_OCI_STILL_EXECUTING = -3004;

		// Token: 0x040007F1 RID: 2033
		internal static int INT_OCI_CONTINUE = -3005;

		// Token: 0x040007F2 RID: 2034
		internal static int INT_DAC_CTX_SIG_MISMATCH = -3006;

		// Token: 0x040007F3 RID: 2035
		internal static int INT_DAC_ROWSIZE_MISMATCH = -3007;

		// Token: 0x040007F4 RID: 2036
		internal static int INT_DAC_DEL_NOT_SUPPORTED = -3008;

		// Token: 0x040007F5 RID: 2037
		internal static int INT_DAC_INS_NOT_SUPPORTED = -3009;

		// Token: 0x040007F6 RID: 2038
		internal static int INT_DAC_UPD_NOT_SUPPORTED = -3010;

		// Token: 0x040007F7 RID: 2039
		internal static int INT_DAC_ROWNUM_INVALID = -3011;

		// Token: 0x040007F8 RID: 2040
		internal static int INT_DAC_COL_ORD_INVALID = -3012;

		// Token: 0x040007F9 RID: 2041
		internal static int INT_DAC_COL_TYPE_INVALID = -3013;

		// Token: 0x040007FA RID: 2042
		internal static int INT_DAC_CACHE_TYPE_INVALID = -3014;

		// Token: 0x040007FB RID: 2043
		internal static int INT_DAC_NO_TABLE_OR_SCHEMA = -3015;

		// Token: 0x040007FC RID: 2044
		internal static int INT_OCIERRORGET_FAIL = -3016;

		// Token: 0x040007FD RID: 2045
		internal static int INT_GETERRORCNT_FAIL = -3017;

		// Token: 0x040007FE RID: 2046
		internal static int INT_ERR_CORE_MESG_GET = -3018;

		// Token: 0x040007FF RID: 2047
		internal static int INT_ERR_BATCHERRGET_FAIL = -3019;

		// Token: 0x04000800 RID: 2048
		internal static int CON_TT_MIN_VERSION = -4000;

		// Token: 0x04000801 RID: 2049
		internal static int EF_NILADIC_FUNCTION = -5000;

		// Token: 0x04000802 RID: 2050
		internal static int CLR_NOTSUPPORTED_NONORACLR_THREAD = -2701;

		// Token: 0x04000803 RID: 2051
		internal static int CLR_NOTSUPPORTED_DOTNET_SP = -2702;

		// Token: 0x04000804 RID: 2052
		internal static int CLR_NOTSUPPORTED_CTX_CONN = -2703;

		// Token: 0x04000805 RID: 2053
		internal static int CLR_CTX_CONN_OPENED_ALREADY = -2704;

		// Token: 0x04000806 RID: 2054
		internal static int CLR_NOTSUPPORTED_INTERNAL_CONN = -2705;

		// Token: 0x04000807 RID: 2055
		internal static int CLR_UDT_NOTSUPPORTED_CTX_CONN = -2706;

		// Token: 0x04000808 RID: 2056
		internal static int BC_OPER_IN_PROGRESS = -2750;

		// Token: 0x04000809 RID: 2057
		internal static int BC_INV_COL_MAPPINGS = -2751;

		// Token: 0x0400080A RID: 2058
		internal static int BC_INV_OPER_INSIDE_EVENT = -2752;

		// Token: 0x0400080B RID: 2059
		internal static int BC_OPER_ABORT = -2753;

		// Token: 0x0400080C RID: 2060
		internal static int BC_ERROR = -2754;

		// Token: 0x0400080D RID: 2061
		internal static int BC_OPER_TIMEOUT = -2755;
	}
}
