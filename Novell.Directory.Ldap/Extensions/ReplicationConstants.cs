using System;

namespace Novell.Directory.Ldap.Extensions
{
	// Token: 0x020000B1 RID: 177
	public class ReplicationConstants
	{
		// Token: 0x040003A7 RID: 935
		public const string CREATE_NAMING_CONTEXT_REQ = "2.16.840.1.113719.1.27.100.3";

		// Token: 0x040003A8 RID: 936
		public const string CREATE_NAMING_CONTEXT_RES = "2.16.840.1.113719.1.27.100.4";

		// Token: 0x040003A9 RID: 937
		public const string MERGE_NAMING_CONTEXT_REQ = "2.16.840.1.113719.1.27.100.5";

		// Token: 0x040003AA RID: 938
		public const string MERGE_NAMING_CONTEXT_RES = "2.16.840.1.113719.1.27.100.6";

		// Token: 0x040003AB RID: 939
		public const string ADD_REPLICA_REQ = "2.16.840.1.113719.1.27.100.7";

		// Token: 0x040003AC RID: 940
		public const string ADD_REPLICA_RES = "2.16.840.1.113719.1.27.100.8";

		// Token: 0x040003AD RID: 941
		public const string REFRESH_SERVER_REQ = "2.16.840.1.113719.1.27.100.9";

		// Token: 0x040003AE RID: 942
		public const string REFRESH_SERVER_RES = "2.16.840.1.113719.1.27.100.10";

		// Token: 0x040003AF RID: 943
		public const string DELETE_REPLICA_REQ = "2.16.840.1.113719.1.27.100.11";

		// Token: 0x040003B0 RID: 944
		public const string DELETE_REPLICA_RES = "2.16.840.1.113719.1.27.100.12";

		// Token: 0x040003B1 RID: 945
		public const string NAMING_CONTEXT_COUNT_REQ = "2.16.840.1.113719.1.27.100.13";

		// Token: 0x040003B2 RID: 946
		public const string NAMING_CONTEXT_COUNT_RES = "2.16.840.1.113719.1.27.100.14";

		// Token: 0x040003B3 RID: 947
		public const string CHANGE_REPLICA_TYPE_REQ = "2.16.840.1.113719.1.27.100.15";

		// Token: 0x040003B4 RID: 948
		public const string CHANGE_REPLICA_TYPE_RES = "2.16.840.1.113719.1.27.100.16";

		// Token: 0x040003B5 RID: 949
		public const string GET_REPLICA_INFO_REQ = "2.16.840.1.113719.1.27.100.17";

		// Token: 0x040003B6 RID: 950
		public const string GET_REPLICA_INFO_RES = "2.16.840.1.113719.1.27.100.18";

		// Token: 0x040003B7 RID: 951
		public const string LIST_REPLICAS_REQ = "2.16.840.1.113719.1.27.100.19";

		// Token: 0x040003B8 RID: 952
		public const string LIST_REPLICAS_RES = "2.16.840.1.113719.1.27.100.20";

		// Token: 0x040003B9 RID: 953
		public const string RECEIVE_ALL_UPDATES_REQ = "2.16.840.1.113719.1.27.100.21";

		// Token: 0x040003BA RID: 954
		public const string RECEIVE_ALL_UPDATES_RES = "2.16.840.1.113719.1.27.100.22";

		// Token: 0x040003BB RID: 955
		public const string SEND_ALL_UPDATES_REQ = "2.16.840.1.113719.1.27.100.23";

		// Token: 0x040003BC RID: 956
		public const string SEND_ALL_UPDATES_RES = "2.16.840.1.113719.1.27.100.24";

		// Token: 0x040003BD RID: 957
		public const string NAMING_CONTEXT_SYNC_REQ = "2.16.840.1.113719.1.27.100.25";

		// Token: 0x040003BE RID: 958
		public const string NAMING_CONTEXT_SYNC_RES = "2.16.840.1.113719.1.27.100.26";

		// Token: 0x040003BF RID: 959
		public const string SCHEMA_SYNC_REQ = "2.16.840.1.113719.1.27.100.27";

		// Token: 0x040003C0 RID: 960
		public const string SCHEMA_SYNC_RES = "2.16.840.1.113719.1.27.100.28";

		// Token: 0x040003C1 RID: 961
		public const string ABORT_NAMING_CONTEXT_OP_REQ = "2.16.840.1.113719.1.27.100.29";

		// Token: 0x040003C2 RID: 962
		public const string ABORT_NAMING_CONTEXT_OP_RES = "2.16.840.1.113719.1.27.100.30";

		// Token: 0x040003C3 RID: 963
		public const string GET_IDENTITY_NAME_REQ = "2.16.840.1.113719.1.27.100.31";

		// Token: 0x040003C4 RID: 964
		public const string GET_IDENTITY_NAME_RES = "2.16.840.1.113719.1.27.100.32";

		// Token: 0x040003C5 RID: 965
		public const string GET_EFFECTIVE_PRIVILEGES_REQ = "2.16.840.1.113719.1.27.100.33";

		// Token: 0x040003C6 RID: 966
		public const string GET_EFFECTIVE_PRIVILEGES_RES = "2.16.840.1.113719.1.27.100.34";

		// Token: 0x040003C7 RID: 967
		public const string SET_REPLICATION_FILTER_REQ = "2.16.840.1.113719.1.27.100.35";

		// Token: 0x040003C8 RID: 968
		public const string SET_REPLICATION_FILTER_RES = "2.16.840.1.113719.1.27.100.36";

		// Token: 0x040003C9 RID: 969
		public const string GET_REPLICATION_FILTER_REQ = "2.16.840.1.113719.1.27.100.37";

		// Token: 0x040003CA RID: 970
		public const string GET_REPLICATION_FILTER_RES = "2.16.840.1.113719.1.27.100.38";

		// Token: 0x040003CB RID: 971
		public const string CREATE_ORPHAN_NAMING_CONTEXT_REQ = "2.16.840.1.113719.1.27.100.39";

		// Token: 0x040003CC RID: 972
		public const string CREATE_ORPHAN_NAMING_CONTEXT_RES = "2.16.840.1.113719.1.27.100.40";

		// Token: 0x040003CD RID: 973
		public const string REMOVE_ORPHAN_NAMING_CONTEXT_REQ = "2.16.840.1.113719.1.27.100.41";

		// Token: 0x040003CE RID: 974
		public const string REMOVE_ORPHAN_NAMING_CONTEXT_RES = "2.16.840.1.113719.1.27.100.42";

		// Token: 0x040003CF RID: 975
		public const string TRIGGER_BKLINKER_REQ = "2.16.840.1.113719.1.27.100.43";

		// Token: 0x040003D0 RID: 976
		public const string TRIGGER_BKLINKER_RES = "2.16.840.1.113719.1.27.100.44";

		// Token: 0x040003D1 RID: 977
		public const string TRIGGER_JANITOR_REQ = "2.16.840.1.113719.1.27.100.47";

		// Token: 0x040003D2 RID: 978
		public const string TRIGGER_JANITOR_RES = "2.16.840.1.113719.1.27.100.48";

		// Token: 0x040003D3 RID: 979
		public const string TRIGGER_LIMBER_REQ = "2.16.840.1.113719.1.27.100.49";

		// Token: 0x040003D4 RID: 980
		public const string TRIGGER_LIMBER_RES = "2.16.840.1.113719.1.27.100.50";

		// Token: 0x040003D5 RID: 981
		public const string TRIGGER_SKULKER_REQ = "2.16.840.1.113719.1.27.100.51";

		// Token: 0x040003D6 RID: 982
		public const string TRIGGER_SKULKER_RES = "2.16.840.1.113719.1.27.100.52";

		// Token: 0x040003D7 RID: 983
		public const string TRIGGER_SCHEMA_SYNC_REQ = "2.16.840.1.113719.1.27.100.53";

		// Token: 0x040003D8 RID: 984
		public const string TRIGGER_SCHEMA_SYNC_RES = "2.16.840.1.113719.1.27.100.54";

		// Token: 0x040003D9 RID: 985
		public const string TRIGGER_PART_PURGE_REQ = "2.16.840.1.113719.1.27.100.55";

		// Token: 0x040003DA RID: 986
		public const string TRIGGER_PART_PURGE_RES = "2.16.840.1.113719.1.27.100.56";

		// Token: 0x040003DB RID: 987
		public const int Ldap_ENSURE_SERVERS_UP = 1;

		// Token: 0x040003DC RID: 988
		public const int Ldap_RT_MASTER = 0;

		// Token: 0x040003DD RID: 989
		public const int Ldap_RT_SECONDARY = 1;

		// Token: 0x040003DE RID: 990
		public const int Ldap_RT_READONLY = 2;

		// Token: 0x040003DF RID: 991
		public const int Ldap_RT_SUBREF = 3;

		// Token: 0x040003E0 RID: 992
		public const int Ldap_RT_SPARSE_WRITE = 4;

		// Token: 0x040003E1 RID: 993
		public const int Ldap_RT_SPARSE_READ = 5;

		// Token: 0x040003E2 RID: 994
		public const int Ldap_RS_ON = 0;

		// Token: 0x040003E3 RID: 995
		public const int Ldap_RS_NEW_REPLICA = 1;

		// Token: 0x040003E4 RID: 996
		public const int Ldap_RS_DYING_REPLICA = 2;

		// Token: 0x040003E5 RID: 997
		public const int Ldap_RS_LOCKED = 3;

		// Token: 0x040003E6 RID: 998
		public const int Ldap_RS_TRANSITION_ON = 6;

		// Token: 0x040003E7 RID: 999
		public const int Ldap_RS_DEAD_REPLICA = 7;

		// Token: 0x040003E8 RID: 1000
		public const int Ldap_RS_BEGIN_ADD = 8;

		// Token: 0x040003E9 RID: 1001
		public const int Ldap_RS_MASTER_START = 11;

		// Token: 0x040003EA RID: 1002
		public const int Ldap_RS_MASTER_DONE = 12;

		// Token: 0x040003EB RID: 1003
		public const int Ldap_RS_SS_0 = 48;

		// Token: 0x040003EC RID: 1004
		public const int Ldap_RS_SS_1 = 49;

		// Token: 0x040003ED RID: 1005
		public const int Ldap_RS_JS_0 = 64;

		// Token: 0x040003EE RID: 1006
		public const int Ldap_RS_JS_1 = 65;

		// Token: 0x040003EF RID: 1007
		public const int Ldap_RS_JS_2 = 66;

		// Token: 0x040003F0 RID: 1008
		public const int Ldap_DS_FLAG_BUSY = 1;

		// Token: 0x040003F1 RID: 1009
		public const int Ldap_DS_FLAG_BOUNDARY = 2;
	}
}
