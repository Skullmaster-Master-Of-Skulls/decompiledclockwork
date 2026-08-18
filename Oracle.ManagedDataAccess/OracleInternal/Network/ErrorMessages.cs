using System;

namespace OracleInternal.Network
{
	// Token: 0x02000168 RID: 360
	internal class ErrorMessages
	{
		// Token: 0x04000FA3 RID: 4003
		internal const int OC_NOT_IN_BREAK_MODE = -6000;

		// Token: 0x04000FA4 RID: 4004
		internal const int OC_CONNECT_FAILURE_OR_CONNECT_STR_PARSE_ERR = -6001;

		// Token: 0x04000FA5 RID: 4005
		internal const int OC_UNINITIALIZED_LISTEN_ENDPOINT = -6002;

		// Token: 0x04000FA6 RID: 4006
		internal const int NA_KERBEROS_ERROR = -6330;

		// Token: 0x04000FA7 RID: 4007
		internal const int NA_NTS_KERBEROS_ERROR = -6331;

		// Token: 0x04000FA8 RID: 4008
		internal const int NT_SSL_FAILURE_PARSING_WALLET_LOC = -6400;

		// Token: 0x04000FA9 RID: 4009
		internal const int NT_TCP_ADDRESS_CONNECT_FAILURE = -6403;

		// Token: 0x04000FAA RID: 4010
		internal const int NL_TNS_CONNECT_STRING_MISSING = -6500;

		// Token: 0x04000FAB RID: 4011
		internal const int NL_ORABUF_SEGMENTS_MUST_MONOTONICALLY_INCREASE = -6501;

		// Token: 0x04000FAC RID: 4012
		internal const int NL_ORABUF_OVERFLOW = -6502;

		// Token: 0x04000FAD RID: 4013
		internal const int NL_ORABUF_MUST_HAVE_ONE_SEGMENT = -6503;

		// Token: 0x04000FAE RID: 4014
		internal const int NN_LDAP_NO_SERVER_CONFIGURED = -6800;

		// Token: 0x04000FAF RID: 4015
		internal const int NLNV_SYNTAX_ERROR = 303;

		// Token: 0x04000FB0 RID: 4016
		internal const int NLNV_PREMATURE_END_OF_STRING = 351;

		// Token: 0x04000FB1 RID: 4017
		internal const int INVALID_ENCRYPTION_PARAMETER = -6304;

		// Token: 0x04000FB2 RID: 4018
		internal const int SUPERVISOR_STATUS_FAILURE = -6306;

		// Token: 0x04000FB3 RID: 4019
		internal const int AUTHENTICATION_STATUS_FAILURE = -6307;

		// Token: 0x04000FB4 RID: 4020
		internal const int SERVICE_CLASSES_NOT_INSTALLED = -6308;

		// Token: 0x04000FB5 RID: 4021
		internal const int INVALID_DRIVER = -6309;

		// Token: 0x04000FB6 RID: 4022
		internal const int ARRAY_HEADER_ERROR = -6310;

		// Token: 0x04000FB7 RID: 4023
		internal const int INVALID_NA_PACKET_TYPE_LENGTH = -6312;

		// Token: 0x04000FB8 RID: 4024
		internal const int INVALID_NA_PACKET_TYPE = -6313;

		// Token: 0x04000FB9 RID: 4025
		internal const int UNEXPECTED_NA_PACKET_TYPE_RECEIVED = -6314;

		// Token: 0x04000FBA RID: 4026
		internal const int INVALID_SERVICES_FROM_SERVER = -6320;

		// Token: 0x04000FBB RID: 4027
		internal const int INCOMPLETE_SERVICES_FROM_SERVER = -6321;

		// Token: 0x04000FBC RID: 4028
		internal const int INVALID_LEVEL = -6322;

		// Token: 0x04000FBD RID: 4029
		internal const int NA_NTS_INVALID_SSPI_PROTOCOL = -6328;

		// Token: 0x04000FBE RID: 4030
		internal const int NA_NTS_INVALID_SSPI_PACKET = -6329;

		// Token: 0x04000FBF RID: 4031
		internal const int NAERROR_PACKET_RECEIVE_FAILED = 12637;

		// Token: 0x04000FC0 RID: 4032
		internal const int NAERROR_PARAM_UNKNOWN_ALGORITHM = 12649;

		// Token: 0x04000FC1 RID: 4033
		internal const int NAERROR_INVALID_PACKET_RECEIVED = 2514;

		// Token: 0x04000FC2 RID: 4034
		internal const int IFCR_BRK = 3111;

		// Token: 0x04000FC3 RID: 4035
		internal const int IFCR_UFL = 3107;

		// Token: 0x04000FC4 RID: 4036
		internal const int IFCR_OFL = 3109;

		// Token: 0x04000FC5 RID: 4037
		internal const int IFCR_EOF = 3113;

		// Token: 0x04000FC6 RID: 4038
		internal const int IFCR_CLC = 3135;

		// Token: 0x04000FC7 RID: 4039
		internal const int IFCR_CTO = 3136;

		// Token: 0x04000FC8 RID: 4040
		internal const int IFCR_RTO = 12609;

		// Token: 0x04000FC9 RID: 4041
		internal const int IFCR_INP = 3140;

		// Token: 0x04000FCA RID: 4042
		internal const int IFCR_RQF = 3141;

		// Token: 0x04000FCB RID: 4043
		internal const int IFCR_STO = 12608;

		// Token: 0x04000FCC RID: 4044
		internal const int NTZSSLHANDSHAKEFAIL = 542;

		// Token: 0x04000FCD RID: 4045
		internal const int NTZSSLDNMISMATCH = 29003;

		// Token: 0x04000FCE RID: 4046
		internal const int NIRECRA = 12154;

		// Token: 0x04000FCF RID: 4047
		internal const int NSEBASE = 12530;

		// Token: 0x04000FD0 RID: 4048
		internal const int NSEALLOCATE = 12531;

		// Token: 0x04000FD1 RID: 4049
		internal const int NSEINVARG = 12532;

		// Token: 0x04000FD2 RID: 4050
		internal const int NSEADDRESS = 12533;

		// Token: 0x04000FD3 RID: 4051
		internal const int NSENOTSUPPORTED = 12534;

		// Token: 0x04000FD4 RID: 4052
		internal const int NSETIMEDOUT = 12535;

		// Token: 0x04000FD5 RID: 4053
		internal const int NSEWOULDBLOCK = 12536;

		// Token: 0x04000FD6 RID: 4054
		internal const int NSEENDOFFILE = 12537;

		// Token: 0x04000FD7 RID: 4055
		internal const int NSENODRIVER = 12538;

		// Token: 0x04000FD8 RID: 4056
		internal const int NSESIZE = 12539;

		// Token: 0x04000FD9 RID: 4057
		internal const int NSELIMIT = 12540;

		// Token: 0x04000FDA RID: 4058
		internal const int NSENOLISTENER = 12541;

		// Token: 0x04000FDB RID: 4059
		internal const int NSELISTENERALREADY = 12542;

		// Token: 0x04000FDC RID: 4060
		internal const int NSEUNREACHABLE = 12543;

		// Token: 0x04000FDD RID: 4061
		internal const int NSENOTTHERE = 12545;

		// Token: 0x04000FDE RID: 4062
		internal const int NSEACCESSDENIED = 12546;

		// Token: 0x04000FDF RID: 4063
		internal const int NSELOSTCONTACT = 12547;

		// Token: 0x04000FE0 RID: 4064
		internal const int NSEPARTIAL = 12548;

		// Token: 0x04000FE1 RID: 4065
		internal const int NSERESOURCE = 12549;

		// Token: 0x04000FE2 RID: 4066
		internal const int NSESYNTAX = 12550;

		// Token: 0x04000FE3 RID: 4067
		internal const int NSEKEYWORD = 12551;

		// Token: 0x04000FE4 RID: 4068
		internal const int NSEINTERRUPTED = 12552;

		// Token: 0x04000FE5 RID: 4069
		internal const int NSEWORKING = 12554;

		// Token: 0x04000FE6 RID: 4070
		internal const int NSENOPRIVILEGE = 12555;

		// Token: 0x04000FE7 RID: 4071
		internal const int NSENOCALLER = 12556;

		// Token: 0x04000FE8 RID: 4072
		internal const int NSENOTLOADABLE = 12557;

		// Token: 0x04000FE9 RID: 4073
		internal const int NSENOTLOADED = 12558;

		// Token: 0x04000FEA RID: 4074
		internal const int NSEEVPOSTED = 12559;

		// Token: 0x04000FEB RID: 4075
		internal const int NSENT = 12560;

		// Token: 0x04000FEC RID: 4076
		internal const int NSEERROR = 12561;

		// Token: 0x04000FED RID: 4077
		internal const int NSEBADGBH = 12562;

		// Token: 0x04000FEE RID: 4078
		internal const int NSEREFUSE = 12564;

		// Token: 0x04000FEF RID: 4079
		internal const int NSENORESPONSE = 12565;

		// Token: 0x04000FF0 RID: 4080
		internal const int NSEPROTOCOL = 12566;

		// Token: 0x04000FF1 RID: 4081
		internal const int NSEUSERABORT = 12567;

		// Token: 0x04000FF2 RID: 4082
		internal const int NSESYSABORT = 12568;

		// Token: 0x04000FF3 RID: 4083
		internal const int NSECHECKSUM = 12569;

		// Token: 0x04000FF4 RID: 4084
		internal const int NSEREADER = 12570;

		// Token: 0x04000FF5 RID: 4085
		internal const int NSEWRITER = 12571;

		// Token: 0x04000FF6 RID: 4086
		internal const int NSENOREDIRECT = 12574;

		// Token: 0x04000FF7 RID: 4087
		internal const int NSEDHCTXBUSY = 12575;

		// Token: 0x04000FF8 RID: 4088
		internal const int NSEDHNOTSUPPORTED = 12576;

		// Token: 0x04000FF9 RID: 4089
		internal const int NSEVECTOREDIO = 12577;

		// Token: 0x04000FFA RID: 4090
		internal const int NSEWALLETOPNFAIL = 12578;

		// Token: 0x04000FFB RID: 4091
		internal const int NSEINVOPN = 12582;

		// Token: 0x04000FFC RID: 4092
		internal const int NSENOREADER = 12583;

		// Token: 0x04000FFD RID: 4093
		internal const int NSETRUNCATED = 12585;

		// Token: 0x04000FFE RID: 4094
		internal const int NSERESEND = 12586;

		// Token: 0x04000FFF RID: 4095
		internal const int NSEREDIRECT = 12587;

		// Token: 0x04001000 RID: 4096
		internal const int NSENOTBEQABLE = 12589;

		// Token: 0x04001001 RID: 4097
		internal const int NSENOBUFFER = 12590;

		// Token: 0x04001002 RID: 4098
		internal const int NSEEVNOTFN = 12591;

		// Token: 0x04001003 RID: 4099
		internal const int NSEBADPACKET = 12592;

		// Token: 0x04001004 RID: 4100
		internal const int NSEREGNONE = 12593;

		// Token: 0x04001005 RID: 4101
		internal const int NSEREFDUMDAT = 12594;

		// Token: 0x04001006 RID: 4102
		internal const int NSENOCONFIRM = 12595;

		// Token: 0x04001007 RID: 4103
		internal const int NSEINCONSISTENT = 12596;

		// Token: 0x04001008 RID: 4104
		internal const int NSECXDINUSE = 12597;

		// Token: 0x04001009 RID: 4105
		internal const int NSEBANNERREGISTRATIONFAILED = 12598;

		// Token: 0x0400100A RID: 4106
		internal const int NSECRYPTCHKSUM = 12599;

		// Token: 0x0400100B RID: 4107
		internal const int NSENLSOPENFAILED = 12600;

		// Token: 0x0400100C RID: 4108
		internal const int NSEINFOFLAGSCHECKFAILED = 12601;

		// Token: 0x0400100D RID: 4109
		internal const int NSECPLIMIT = 12602;

		// Token: 0x0400100E RID: 4110
		internal const int NSEEVPOSTFAIL = 12603;

		// Token: 0x0400100F RID: 4111
		internal const int NSEAPPTIMEOUT = 12606;

		// Token: 0x04001010 RID: 4112
		internal const int NSECONNTIMEOUT = 12607;

		// Token: 0x04001011 RID: 4113
		internal const int NSEPINGTIMEOUT = 12607;

		// Token: 0x04001012 RID: 4114
		internal const int NSESENDTIMEOUT = 12608;

		// Token: 0x04001013 RID: 4115
		internal const int NSERECVTIMEOUT = 12609;

		// Token: 0x04001014 RID: 4116
		internal const int NSESESSION = 12610;

		// Token: 0x04001015 RID: 4117
		internal const int NSENONPORTABLE = 12611;

		// Token: 0x04001016 RID: 4118
		internal const int NSEBUSY = 12612;

		// Token: 0x04001017 RID: 4119
		internal const int NSEBUFFER = 12613;

		// Token: 0x04001018 RID: 4120
		internal const int NSENOTCONNECTED = 12614;

		// Token: 0x04001019 RID: 4121
		internal const int NSEPREEMPT = 12615;

		// Token: 0x0400101A RID: 4122
		internal const int NSESETSIG = 12616;

		// Token: 0x0400101B RID: 4123
		internal const int NSEWHAT = 12617;

		// Token: 0x0400101C RID: 4124
		internal const int NSEVERSION = 12618;

		// Token: 0x0400101D RID: 4125
		internal const int NSEOPTION = 12619;

		// Token: 0x0400101E RID: 4126
		internal const int NSECHAR = 12620;

		// Token: 0x0400101F RID: 4127
		internal const int NSEREADAHEAD = 12621;

		// Token: 0x04001020 RID: 4128
		internal const int NSEHOMOEVVIO = 12622;

		// Token: 0x04001021 RID: 4129
		internal const int NSESTATE = 12623;

		// Token: 0x04001022 RID: 4130
		internal const int NSEREGALREADY = 12624;

		// Token: 0x04001023 RID: 4131
		internal const int NSEMISSING = 12625;

		// Token: 0x04001024 RID: 4132
		internal const int NSEBADEVENT = 12626;

		// Token: 0x04001025 RID: 4133
		internal const int NSEDIRECT = 12627;

		// Token: 0x04001026 RID: 4134
		internal const int NSENOCALLBACK = 12628;

		// Token: 0x04001027 RID: 4135
		internal const int NSENOTEST = 12629;
	}
}
