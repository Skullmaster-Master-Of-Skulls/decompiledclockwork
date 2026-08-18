using System;

namespace Novell.Directory.Ldap.Extensions
{
	// Token: 0x020000A9 RID: 169
	public class NamingContextConstants
	{
		// Token: 0x0400035B RID: 859
		public const string CREATE_NAMING_CONTEXT_REQ = "2.16.840.1.113719.1.27.100.3";

		// Token: 0x0400035C RID: 860
		public const string CREATE_NAMING_CONTEXT_RES = "2.16.840.1.113719.1.27.100.4";

		// Token: 0x0400035D RID: 861
		public const string MERGE_NAMING_CONTEXT_REQ = "2.16.840.1.113719.1.27.100.5";

		// Token: 0x0400035E RID: 862
		public const string MERGE_NAMING_CONTEXT_RES = "2.16.840.1.113719.1.27.100.6";

		// Token: 0x0400035F RID: 863
		public const string ADD_REPLICA_REQ = "2.16.840.1.113719.1.27.100.7";

		// Token: 0x04000360 RID: 864
		public const string ADD_REPLICA_RES = "2.16.840.1.113719.1.27.100.8";

		// Token: 0x04000361 RID: 865
		public const string REFRESH_SERVER_REQ = "2.16.840.1.113719.1.27.100.9";

		// Token: 0x04000362 RID: 866
		public const string REFRESH_SERVER_RES = "2.16.840.1.113719.1.27.100.10";

		// Token: 0x04000363 RID: 867
		public const string DELETE_REPLICA_REQ = "2.16.840.1.113719.1.27.100.11";

		// Token: 0x04000364 RID: 868
		public const string DELETE_REPLICA_RES = "2.16.840.1.113719.1.27.100.12";

		// Token: 0x04000365 RID: 869
		public const string NAMING_CONTEXT_COUNT_REQ = "2.16.840.1.113719.1.27.100.13";

		// Token: 0x04000366 RID: 870
		public const string NAMING_CONTEXT_COUNT_RES = "2.16.840.1.113719.1.27.100.14";

		// Token: 0x04000367 RID: 871
		public const string CHANGE_REPLICA_TYPE_REQ = "2.16.840.1.113719.1.27.100.15";

		// Token: 0x04000368 RID: 872
		public const string CHANGE_REPLICA_TYPE_RES = "2.16.840.1.113719.1.27.100.16";

		// Token: 0x04000369 RID: 873
		public const string GET_REPLICA_INFO_REQ = "2.16.840.1.113719.1.27.100.17";

		// Token: 0x0400036A RID: 874
		public const string GET_REPLICA_INFO_RES = "2.16.840.1.113719.1.27.100.18";

		// Token: 0x0400036B RID: 875
		public const string LIST_REPLICAS_REQ = "2.16.840.1.113719.1.27.100.19";

		// Token: 0x0400036C RID: 876
		public const string LIST_REPLICAS_RES = "2.16.840.1.113719.1.27.100.20";

		// Token: 0x0400036D RID: 877
		public const string RECEIVE_ALL_UPDATES_REQ = "2.16.840.1.113719.1.27.100.21";

		// Token: 0x0400036E RID: 878
		public const string RECEIVE_ALL_UPDATES_RES = "2.16.840.1.113719.1.27.100.22";

		// Token: 0x0400036F RID: 879
		public const string SEND_ALL_UPDATES_REQ = "2.16.840.1.113719.1.27.100.23";

		// Token: 0x04000370 RID: 880
		public const string SEND_ALL_UPDATES_RES = "2.16.840.1.113719.1.27.100.24";

		// Token: 0x04000371 RID: 881
		public const string NAMING_CONTEXT_SYNC_REQ = "2.16.840.1.113719.1.27.100.25";

		// Token: 0x04000372 RID: 882
		public const string NAMING_CONTEXT_SYNC_RES = "2.16.840.1.113719.1.27.100.26";

		// Token: 0x04000373 RID: 883
		public const string SCHEMA_SYNC_REQ = "2.16.840.1.113719.1.27.100.27";

		// Token: 0x04000374 RID: 884
		public const string SCHEMA_SYNC_RES = "2.16.840.1.113719.1.27.100.28";

		// Token: 0x04000375 RID: 885
		public const string ABORT_NAMING_CONTEXT_OP_REQ = "2.16.840.1.113719.1.27.100.29";

		// Token: 0x04000376 RID: 886
		public const string ABORT_NAMING_CONTEXT_OP_RES = "2.16.840.1.113719.1.27.100.30";

		// Token: 0x04000377 RID: 887
		public const string GET_IDENTITY_NAME_REQ = "2.16.840.1.113719.1.27.100.31";

		// Token: 0x04000378 RID: 888
		public const string GET_IDENTITY_NAME_RES = "2.16.840.1.113719.1.27.100.32";

		// Token: 0x04000379 RID: 889
		public const string GET_EFFECTIVE_PRIVILEGES_REQ = "2.16.840.1.113719.1.27.100.33";

		// Token: 0x0400037A RID: 890
		public const string GET_EFFECTIVE_PRIVILEGES_RES = "2.16.840.1.113719.1.27.100.34";

		// Token: 0x0400037B RID: 891
		public const string SET_REPLICATION_FILTER_REQ = "2.16.840.1.113719.1.27.100.35";

		// Token: 0x0400037C RID: 892
		public const string SET_REPLICATION_FILTER_RES = "2.16.840.1.113719.1.27.100.36";

		// Token: 0x0400037D RID: 893
		public const string GET_REPLICATION_FILTER_REQ = "2.16.840.1.113719.1.27.100.37";

		// Token: 0x0400037E RID: 894
		public const string GET_REPLICATION_FILTER_RES = "2.16.840.1.113719.1.27.100.38";

		// Token: 0x0400037F RID: 895
		public const string CREATE_ORPHAN_NAMING_CONTEXT_REQ = "2.16.840.1.113719.1.27.100.39";

		// Token: 0x04000380 RID: 896
		public const string CREATE_ORPHAN_NAMING_CONTEXT_RES = "2.16.840.1.113719.1.27.100.40";

		// Token: 0x04000381 RID: 897
		public const string REMOVE_ORPHAN_NAMING_CONTEXT_REQ = "2.16.840.1.113719.1.27.100.41";

		// Token: 0x04000382 RID: 898
		public const string REMOVE_ORPHAN_NAMING_CONTEXT_RES = "2.16.840.1.113719.1.27.100.42";

		// Token: 0x04000383 RID: 899
		public const string TRIGGER_BKLINKER_REQ = "2.16.840.1.113719.1.27.100.43";

		// Token: 0x04000384 RID: 900
		public const string TRIGGER_BKLINKER_RES = "2.16.840.1.113719.1.27.100.44";

		// Token: 0x04000385 RID: 901
		public const string TRIGGER_JANITOR_REQ = "2.16.840.1.113719.1.27.100.47";

		// Token: 0x04000386 RID: 902
		public const string TRIGGER_JANITOR_RES = "2.16.840.1.113719.1.27.100.48";

		// Token: 0x04000387 RID: 903
		public const string TRIGGER_LIMBER_REQ = "2.16.840.1.113719.1.27.100.49";

		// Token: 0x04000388 RID: 904
		public const string TRIGGER_LIMBER_RES = "2.16.840.1.113719.1.27.100.50";

		// Token: 0x04000389 RID: 905
		public const string TRIGGER_SKULKER_REQ = "2.16.840.1.113719.1.27.100.51";

		// Token: 0x0400038A RID: 906
		public const string TRIGGER_SKULKER_RES = "2.16.840.1.113719.1.27.100.52";

		// Token: 0x0400038B RID: 907
		public const string TRIGGER_SCHEMA_SYNC_REQ = "2.16.840.1.113719.1.27.100.53";

		// Token: 0x0400038C RID: 908
		public const string TRIGGER_SCHEMA_SYNC_RES = "2.16.840.1.113719.1.27.100.54";

		// Token: 0x0400038D RID: 909
		public const string TRIGGER_PART_PURGE_REQ = "2.16.840.1.113719.1.27.100.55";

		// Token: 0x0400038E RID: 910
		public const string TRIGGER_PART_PURGE_RES = "2.16.840.1.113719.1.27.100.56";

		// Token: 0x0400038F RID: 911
		public const int Ldap_ENSURE_SERVERS_UP = 1;

		// Token: 0x04000390 RID: 912
		public const int Ldap_RT_MASTER = 0;

		// Token: 0x04000391 RID: 913
		public const int Ldap_RT_SECONDARY = 1;

		// Token: 0x04000392 RID: 914
		public const int Ldap_RT_READONLY = 2;

		// Token: 0x04000393 RID: 915
		public const int Ldap_RT_SUBREF = 3;

		// Token: 0x04000394 RID: 916
		public const int Ldap_RT_SPARSE_WRITE = 4;

		// Token: 0x04000395 RID: 917
		public const int Ldap_RT_SPARSE_READ = 5;

		// Token: 0x04000396 RID: 918
		public const int Ldap_RS_ON = 0;

		// Token: 0x04000397 RID: 919
		public const int Ldap_RS_NEW_REPLICA = 1;

		// Token: 0x04000398 RID: 920
		public const int Ldap_RS_DYING_REPLICA = 2;

		// Token: 0x04000399 RID: 921
		public const int Ldap_RS_LOCKED = 3;

		// Token: 0x0400039A RID: 922
		public const int Ldap_RS_TRANSITION_ON = 6;

		// Token: 0x0400039B RID: 923
		public const int Ldap_RS_DEAD_REPLICA = 7;

		// Token: 0x0400039C RID: 924
		public const int Ldap_RS_BEGIN_ADD = 8;

		// Token: 0x0400039D RID: 925
		public const int Ldap_RS_MASTER_START = 11;

		// Token: 0x0400039E RID: 926
		public const int Ldap_RS_MASTER_DONE = 12;

		// Token: 0x0400039F RID: 927
		public const int Ldap_RS_SS_0 = 48;

		// Token: 0x040003A0 RID: 928
		public const int Ldap_RS_SS_1 = 49;

		// Token: 0x040003A1 RID: 929
		public const int Ldap_RS_JS_0 = 64;

		// Token: 0x040003A2 RID: 930
		public const int Ldap_RS_JS_1 = 65;

		// Token: 0x040003A3 RID: 931
		public const int Ldap_RS_JS_2 = 66;

		// Token: 0x040003A4 RID: 932
		public const int Ldap_DS_FLAG_BUSY = 1;

		// Token: 0x040003A5 RID: 933
		public const int Ldap_DS_FLAG_BOUNDARY = 2;
	}
}
