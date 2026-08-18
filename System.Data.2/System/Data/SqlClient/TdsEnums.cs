using System;

namespace System.Data.SqlClient
{
	// Token: 0x02000206 RID: 518
	internal static class TdsEnums
	{
		// Token: 0x040011FC RID: 4604
		public const short SQL_SERVER_VERSION_SEVEN = 7;

		// Token: 0x040011FD RID: 4605
		public const string SQL_PROVIDER_NAME = ".Net SqlClient Data Provider";

		// Token: 0x040011FE RID: 4606
		public static readonly decimal SQL_SMALL_MONEY_MIN = new decimal(-214748.3648);

		// Token: 0x040011FF RID: 4607
		public static readonly decimal SQL_SMALL_MONEY_MAX = new decimal(214748.3647);

		// Token: 0x04001200 RID: 4608
		public const string SDCI_MAPFILENAME = "SqlClientSSDebug";

		// Token: 0x04001201 RID: 4609
		public const byte SDCI_MAX_MACHINENAME = 32;

		// Token: 0x04001202 RID: 4610
		public const byte SDCI_MAX_DLLNAME = 16;

		// Token: 0x04001203 RID: 4611
		public const byte SDCI_MAX_DATA = 255;

		// Token: 0x04001204 RID: 4612
		public const int SQLDEBUG_OFF = 0;

		// Token: 0x04001205 RID: 4613
		public const int SQLDEBUG_ON = 1;

		// Token: 0x04001206 RID: 4614
		public const int SQLDEBUG_CONTEXT = 2;

		// Token: 0x04001207 RID: 4615
		public const string SP_SDIDEBUG = "sp_sdidebug";

		// Token: 0x04001208 RID: 4616
		public static readonly string[] SQLDEBUG_MODE_NAMES = new string[]
		{
			"off",
			"on",
			"context"
		};

		// Token: 0x04001209 RID: 4617
		public const SqlDbType SmallVarBinary = (SqlDbType)24;

		// Token: 0x0400120A RID: 4618
		public const string TCP = "tcp";

		// Token: 0x0400120B RID: 4619
		public const string NP = "np";

		// Token: 0x0400120C RID: 4620
		public const string RPC = "rpc";

		// Token: 0x0400120D RID: 4621
		public const string BV = "bv";

		// Token: 0x0400120E RID: 4622
		public const string ADSP = "adsp";

		// Token: 0x0400120F RID: 4623
		public const string SPX = "spx";

		// Token: 0x04001210 RID: 4624
		public const string VIA = "via";

		// Token: 0x04001211 RID: 4625
		public const string LPC = "lpc";

		// Token: 0x04001212 RID: 4626
		public const string INIT_SSPI_PACKAGE = "InitSSPIPackage";

		// Token: 0x04001213 RID: 4627
		public const string INIT_ADAL_PACKAGE = "InitADALPackage";

		// Token: 0x04001214 RID: 4628
		public const string INIT_SESSION = "InitSession";

		// Token: 0x04001215 RID: 4629
		public const string CONNECTION_GET_SVR_USER = "ConnectionGetSvrUser";

		// Token: 0x04001216 RID: 4630
		public const string GEN_CLIENT_CONTEXT = "GenClientContext";

		// Token: 0x04001217 RID: 4631
		public const byte SOFTFLUSH = 0;

		// Token: 0x04001218 RID: 4632
		public const byte HARDFLUSH = 1;

		// Token: 0x04001219 RID: 4633
		public const byte IGNORE = 2;

		// Token: 0x0400121A RID: 4634
		public const int HEADER_LEN = 8;

		// Token: 0x0400121B RID: 4635
		public const int HEADER_LEN_FIELD_OFFSET = 2;

		// Token: 0x0400121C RID: 4636
		public const int YUKON_HEADER_LEN = 12;

		// Token: 0x0400121D RID: 4637
		public const int MARS_ID_OFFSET = 8;

		// Token: 0x0400121E RID: 4638
		public const int HEADERTYPE_QNOTIFICATION = 1;

		// Token: 0x0400121F RID: 4639
		public const int HEADERTYPE_MARS = 2;

		// Token: 0x04001220 RID: 4640
		public const int HEADERTYPE_TRACE = 3;

		// Token: 0x04001221 RID: 4641
		public const int SUCCEED = 1;

		// Token: 0x04001222 RID: 4642
		public const int FAIL = 0;

		// Token: 0x04001223 RID: 4643
		public const short TYPE_SIZE_LIMIT = 8000;

		// Token: 0x04001224 RID: 4644
		public const int MIN_PACKET_SIZE = 512;

		// Token: 0x04001225 RID: 4645
		public const int DEFAULT_LOGIN_PACKET_SIZE = 4096;

		// Token: 0x04001226 RID: 4646
		public const int MAX_PRELOGIN_PAYLOAD_LENGTH = 1024;

		// Token: 0x04001227 RID: 4647
		public const int MAX_PACKET_SIZE = 32768;

		// Token: 0x04001228 RID: 4648
		public const int MAX_SERVER_USER_NAME = 256;

		// Token: 0x04001229 RID: 4649
		public const byte MIN_ERROR_CLASS = 11;

		// Token: 0x0400122A RID: 4650
		public const byte MAX_USER_CORRECTABLE_ERROR_CLASS = 16;

		// Token: 0x0400122B RID: 4651
		public const byte FATAL_ERROR_CLASS = 20;

		// Token: 0x0400122C RID: 4652
		public const byte MT_SQL = 1;

		// Token: 0x0400122D RID: 4653
		public const byte MT_LOGIN = 2;

		// Token: 0x0400122E RID: 4654
		public const byte MT_RPC = 3;

		// Token: 0x0400122F RID: 4655
		public const byte MT_TOKENS = 4;

		// Token: 0x04001230 RID: 4656
		public const byte MT_BINARY = 5;

		// Token: 0x04001231 RID: 4657
		public const byte MT_ATTN = 6;

		// Token: 0x04001232 RID: 4658
		public const byte MT_BULK = 7;

		// Token: 0x04001233 RID: 4659
		public const byte MT_FEDAUTH = 8;

		// Token: 0x04001234 RID: 4660
		public const byte MT_CLOSE = 9;

		// Token: 0x04001235 RID: 4661
		public const byte MT_ERROR = 10;

		// Token: 0x04001236 RID: 4662
		public const byte MT_ACK = 11;

		// Token: 0x04001237 RID: 4663
		public const byte MT_ECHO = 12;

		// Token: 0x04001238 RID: 4664
		public const byte MT_LOGOUT = 13;

		// Token: 0x04001239 RID: 4665
		public const byte MT_TRANS = 14;

		// Token: 0x0400123A RID: 4666
		public const byte MT_OLEDB = 15;

		// Token: 0x0400123B RID: 4667
		public const byte MT_LOGIN7 = 16;

		// Token: 0x0400123C RID: 4668
		public const byte MT_SSPI = 17;

		// Token: 0x0400123D RID: 4669
		public const byte MT_PRELOGIN = 18;

		// Token: 0x0400123E RID: 4670
		public const byte ST_EOM = 1;

		// Token: 0x0400123F RID: 4671
		public const byte ST_AACK = 2;

		// Token: 0x04001240 RID: 4672
		public const byte ST_IGNORE = 2;

		// Token: 0x04001241 RID: 4673
		public const byte ST_BATCH = 4;

		// Token: 0x04001242 RID: 4674
		public const byte ST_RESET_CONNECTION = 8;

		// Token: 0x04001243 RID: 4675
		public const byte ST_RESET_CONNECTION_PRESERVE_TRANSACTION = 16;

		// Token: 0x04001244 RID: 4676
		public const byte SQLCOLFMT = 161;

		// Token: 0x04001245 RID: 4677
		public const byte SQLPROCID = 124;

		// Token: 0x04001246 RID: 4678
		public const byte SQLCOLNAME = 160;

		// Token: 0x04001247 RID: 4679
		public const byte SQLTABNAME = 164;

		// Token: 0x04001248 RID: 4680
		public const byte SQLCOLINFO = 165;

		// Token: 0x04001249 RID: 4681
		public const byte SQLALTNAME = 167;

		// Token: 0x0400124A RID: 4682
		public const byte SQLALTFMT = 168;

		// Token: 0x0400124B RID: 4683
		public const byte SQLERROR = 170;

		// Token: 0x0400124C RID: 4684
		public const byte SQLINFO = 171;

		// Token: 0x0400124D RID: 4685
		public const byte SQLRETURNVALUE = 172;

		// Token: 0x0400124E RID: 4686
		public const byte SQLRETURNSTATUS = 121;

		// Token: 0x0400124F RID: 4687
		public const byte SQLRETURNTOK = 219;

		// Token: 0x04001250 RID: 4688
		public const byte SQLALTCONTROL = 175;

		// Token: 0x04001251 RID: 4689
		public const byte SQLROW = 209;

		// Token: 0x04001252 RID: 4690
		public const byte SQLNBCROW = 210;

		// Token: 0x04001253 RID: 4691
		public const byte SQLALTROW = 211;

		// Token: 0x04001254 RID: 4692
		public const byte SQLDONE = 253;

		// Token: 0x04001255 RID: 4693
		public const byte SQLDONEPROC = 254;

		// Token: 0x04001256 RID: 4694
		public const byte SQLDONEINPROC = 255;

		// Token: 0x04001257 RID: 4695
		public const byte SQLOFFSET = 120;

		// Token: 0x04001258 RID: 4696
		public const byte SQLORDER = 169;

		// Token: 0x04001259 RID: 4697
		public const byte SQLDEBUG_CMD = 96;

		// Token: 0x0400125A RID: 4698
		public const byte SQLLOGINACK = 173;

		// Token: 0x0400125B RID: 4699
		public const byte SQLFEATUREEXTACK = 174;

		// Token: 0x0400125C RID: 4700
		public const byte SQLSESSIONSTATE = 228;

		// Token: 0x0400125D RID: 4701
		public const byte SQLENVCHANGE = 227;

		// Token: 0x0400125E RID: 4702
		public const byte SQLSECLEVEL = 237;

		// Token: 0x0400125F RID: 4703
		public const byte SQLROWCRC = 57;

		// Token: 0x04001260 RID: 4704
		public const byte SQLCOLMETADATA = 129;

		// Token: 0x04001261 RID: 4705
		public const byte SQLALTMETADATA = 136;

		// Token: 0x04001262 RID: 4706
		public const byte SQLSSPI = 237;

		// Token: 0x04001263 RID: 4707
		public const byte SQLFEDAUTHINFO = 238;

		// Token: 0x04001264 RID: 4708
		public const byte ENV_DATABASE = 1;

		// Token: 0x04001265 RID: 4709
		public const byte ENV_LANG = 2;

		// Token: 0x04001266 RID: 4710
		public const byte ENV_CHARSET = 3;

		// Token: 0x04001267 RID: 4711
		public const byte ENV_PACKETSIZE = 4;

		// Token: 0x04001268 RID: 4712
		public const byte ENV_LOCALEID = 5;

		// Token: 0x04001269 RID: 4713
		public const byte ENV_COMPFLAGS = 6;

		// Token: 0x0400126A RID: 4714
		public const byte ENV_COLLATION = 7;

		// Token: 0x0400126B RID: 4715
		public const byte ENV_BEGINTRAN = 8;

		// Token: 0x0400126C RID: 4716
		public const byte ENV_COMMITTRAN = 9;

		// Token: 0x0400126D RID: 4717
		public const byte ENV_ROLLBACKTRAN = 10;

		// Token: 0x0400126E RID: 4718
		public const byte ENV_ENLISTDTC = 11;

		// Token: 0x0400126F RID: 4719
		public const byte ENV_DEFECTDTC = 12;

		// Token: 0x04001270 RID: 4720
		public const byte ENV_LOGSHIPNODE = 13;

		// Token: 0x04001271 RID: 4721
		public const byte ENV_PROMOTETRANSACTION = 15;

		// Token: 0x04001272 RID: 4722
		public const byte ENV_TRANSACTIONMANAGERADDRESS = 16;

		// Token: 0x04001273 RID: 4723
		public const byte ENV_TRANSACTIONENDED = 17;

		// Token: 0x04001274 RID: 4724
		public const byte ENV_SPRESETCONNECTIONACK = 18;

		// Token: 0x04001275 RID: 4725
		public const byte ENV_USERINSTANCE = 19;

		// Token: 0x04001276 RID: 4726
		public const byte ENV_ROUTING = 20;

		// Token: 0x04001277 RID: 4727
		public const int DONE_MORE = 1;

		// Token: 0x04001278 RID: 4728
		public const int DONE_ERROR = 2;

		// Token: 0x04001279 RID: 4729
		public const int DONE_INXACT = 4;

		// Token: 0x0400127A RID: 4730
		public const int DONE_PROC = 8;

		// Token: 0x0400127B RID: 4731
		public const int DONE_COUNT = 16;

		// Token: 0x0400127C RID: 4732
		public const int DONE_ATTN = 32;

		// Token: 0x0400127D RID: 4733
		public const int DONE_INPROC = 64;

		// Token: 0x0400127E RID: 4734
		public const int DONE_RPCINBATCH = 128;

		// Token: 0x0400127F RID: 4735
		public const int DONE_SRVERROR = 256;

		// Token: 0x04001280 RID: 4736
		public const int DONE_FMTSENT = 32768;

		// Token: 0x04001281 RID: 4737
		public const byte FEATUREEXT_TERMINATOR = 255;

		// Token: 0x04001282 RID: 4738
		public const byte FEATUREEXT_SRECOVERY = 1;

		// Token: 0x04001283 RID: 4739
		public const byte FEATUREEXT_FEDAUTH = 2;

		// Token: 0x04001284 RID: 4740
		public const byte FEATUREEXT_TCE = 4;

		// Token: 0x04001285 RID: 4741
		public const byte FEATUREEXT_GLOBALTRANSACTIONS = 5;

		// Token: 0x04001286 RID: 4742
		public const byte FEATUREEXT_AZURESQLSUPPORT = 8;

		// Token: 0x04001287 RID: 4743
		public const byte FEDAUTHLIB_LIVEID = 0;

		// Token: 0x04001288 RID: 4744
		public const byte FEDAUTHLIB_SECURITYTOKEN = 1;

		// Token: 0x04001289 RID: 4745
		public const byte FEDAUTHLIB_ADAL = 2;

		// Token: 0x0400128A RID: 4746
		public const byte FEDAUTHLIB_RESERVED = 127;

		// Token: 0x0400128B RID: 4747
		public const byte ADALWORKFLOW_ACTIVEDIRECTORYPASSWORD = 1;

		// Token: 0x0400128C RID: 4748
		public const byte ADALWORKFLOW_ACTIVEDIRECTORYINTEGRATED = 2;

		// Token: 0x0400128D RID: 4749
		public const byte ADALWORKFLOW_ACTIVEDIRECTORYINTERACTIVE = 3;

		// Token: 0x0400128E RID: 4750
		public const string NTAUTHORITYANONYMOUSLOGON = "NT Authority\\Anonymous Logon";

		// Token: 0x0400128F RID: 4751
		public const byte MAX_LOG_NAME = 30;

		// Token: 0x04001290 RID: 4752
		public const byte MAX_PROG_NAME = 10;

		// Token: 0x04001291 RID: 4753
		public const byte SEC_COMP_LEN = 8;

		// Token: 0x04001292 RID: 4754
		public const byte MAX_PK_LEN = 6;

		// Token: 0x04001293 RID: 4755
		public const byte MAX_NIC_SIZE = 6;

		// Token: 0x04001294 RID: 4756
		public const byte SQLVARIANT_SIZE = 2;

		// Token: 0x04001295 RID: 4757
		public const byte VERSION_SIZE = 4;

		// Token: 0x04001296 RID: 4758
		public const int CLIENT_PROG_VER = 100663296;

		// Token: 0x04001297 RID: 4759
		public const int YUKON_LOG_REC_FIXED_LEN = 94;

		// Token: 0x04001298 RID: 4760
		public const int TEXT_TIME_STAMP_LEN = 8;

		// Token: 0x04001299 RID: 4761
		public const int COLLATION_INFO_LEN = 4;

		// Token: 0x0400129A RID: 4762
		public const int SPHINXORSHILOH_MAJOR = 7;

		// Token: 0x0400129B RID: 4763
		public const int SPHINX_INCREMENT = 0;

		// Token: 0x0400129C RID: 4764
		public const int SHILOH_INCREMENT = 1;

		// Token: 0x0400129D RID: 4765
		public const int DEFAULT_MINOR = 0;

		// Token: 0x0400129E RID: 4766
		public const int SHILOHSP1_MAJOR = 113;

		// Token: 0x0400129F RID: 4767
		public const int YUKON_MAJOR = 114;

		// Token: 0x040012A0 RID: 4768
		public const int KATMAI_MAJOR = 115;

		// Token: 0x040012A1 RID: 4769
		public const int DENALI_MAJOR = 116;

		// Token: 0x040012A2 RID: 4770
		public const int SHILOHSP1_INCREMENT = 0;

		// Token: 0x040012A3 RID: 4771
		public const int YUKON_INCREMENT = 9;

		// Token: 0x040012A4 RID: 4772
		public const int KATMAI_INCREMENT = 11;

		// Token: 0x040012A5 RID: 4773
		public const int DENALI_INCREMENT = 0;

		// Token: 0x040012A6 RID: 4774
		public const int SHILOHSP1_MINOR = 1;

		// Token: 0x040012A7 RID: 4775
		public const int YUKON_RTM_MINOR = 2;

		// Token: 0x040012A8 RID: 4776
		public const int KATMAI_MINOR = 3;

		// Token: 0x040012A9 RID: 4777
		public const int DENALI_MINOR = 4;

		// Token: 0x040012AA RID: 4778
		public const int ORDER_68000 = 1;

		// Token: 0x040012AB RID: 4779
		public const int USE_DB_ON = 1;

		// Token: 0x040012AC RID: 4780
		public const int INIT_DB_FATAL = 1;

		// Token: 0x040012AD RID: 4781
		public const int SET_LANG_ON = 1;

		// Token: 0x040012AE RID: 4782
		public const int INIT_LANG_FATAL = 1;

		// Token: 0x040012AF RID: 4783
		public const int ODBC_ON = 1;

		// Token: 0x040012B0 RID: 4784
		public const int SSPI_ON = 1;

		// Token: 0x040012B1 RID: 4785
		public const int REPL_ON = 3;

		// Token: 0x040012B2 RID: 4786
		public const int READONLY_INTENT_ON = 1;

		// Token: 0x040012B3 RID: 4787
		public const byte SQLLenMask = 48;

		// Token: 0x040012B4 RID: 4788
		public const byte SQLFixedLen = 48;

		// Token: 0x040012B5 RID: 4789
		public const byte SQLVarLen = 32;

		// Token: 0x040012B6 RID: 4790
		public const byte SQLZeroLen = 16;

		// Token: 0x040012B7 RID: 4791
		public const byte SQLVarCnt = 0;

		// Token: 0x040012B8 RID: 4792
		public const byte SQLDifferentName = 32;

		// Token: 0x040012B9 RID: 4793
		public const byte SQLExpression = 4;

		// Token: 0x040012BA RID: 4794
		public const byte SQLKey = 8;

		// Token: 0x040012BB RID: 4795
		public const byte SQLHidden = 16;

		// Token: 0x040012BC RID: 4796
		public const byte Nullable = 1;

		// Token: 0x040012BD RID: 4797
		public const byte Identity = 16;

		// Token: 0x040012BE RID: 4798
		public const byte Updatability = 11;

		// Token: 0x040012BF RID: 4799
		public const byte ClrFixedLen = 1;

		// Token: 0x040012C0 RID: 4800
		public const byte IsColumnSet = 4;

		// Token: 0x040012C1 RID: 4801
		public const byte IsEncrypted = 8;

		// Token: 0x040012C2 RID: 4802
		public const uint VARLONGNULL = 4294967295U;

		// Token: 0x040012C3 RID: 4803
		public const int VARNULL = 65535;

		// Token: 0x040012C4 RID: 4804
		public const int MAXSIZE = 8000;

		// Token: 0x040012C5 RID: 4805
		public const byte FIXEDNULL = 0;

		// Token: 0x040012C6 RID: 4806
		public const ulong UDTNULL = 18446744073709551615UL;

		// Token: 0x040012C7 RID: 4807
		public const int SQLVOID = 31;

		// Token: 0x040012C8 RID: 4808
		public const int SQLTEXT = 35;

		// Token: 0x040012C9 RID: 4809
		public const int SQLVARBINARY = 37;

		// Token: 0x040012CA RID: 4810
		public const int SQLINTN = 38;

		// Token: 0x040012CB RID: 4811
		public const int SQLVARCHAR = 39;

		// Token: 0x040012CC RID: 4812
		public const int SQLBINARY = 45;

		// Token: 0x040012CD RID: 4813
		public const int SQLIMAGE = 34;

		// Token: 0x040012CE RID: 4814
		public const int SQLCHAR = 47;

		// Token: 0x040012CF RID: 4815
		public const int SQLINT1 = 48;

		// Token: 0x040012D0 RID: 4816
		public const int SQLBIT = 50;

		// Token: 0x040012D1 RID: 4817
		public const int SQLINT2 = 52;

		// Token: 0x040012D2 RID: 4818
		public const int SQLINT4 = 56;

		// Token: 0x040012D3 RID: 4819
		public const int SQLMONEY = 60;

		// Token: 0x040012D4 RID: 4820
		public const int SQLDATETIME = 61;

		// Token: 0x040012D5 RID: 4821
		public const int SQLFLT8 = 62;

		// Token: 0x040012D6 RID: 4822
		public const int SQLFLTN = 109;

		// Token: 0x040012D7 RID: 4823
		public const int SQLMONEYN = 110;

		// Token: 0x040012D8 RID: 4824
		public const int SQLDATETIMN = 111;

		// Token: 0x040012D9 RID: 4825
		public const int SQLFLT4 = 59;

		// Token: 0x040012DA RID: 4826
		public const int SQLMONEY4 = 122;

		// Token: 0x040012DB RID: 4827
		public const int SQLDATETIM4 = 58;

		// Token: 0x040012DC RID: 4828
		public const int SQLDECIMALN = 106;

		// Token: 0x040012DD RID: 4829
		public const int SQLNUMERICN = 108;

		// Token: 0x040012DE RID: 4830
		public const int SQLUNIQUEID = 36;

		// Token: 0x040012DF RID: 4831
		public const int SQLBIGCHAR = 175;

		// Token: 0x040012E0 RID: 4832
		public const int SQLBIGVARCHAR = 167;

		// Token: 0x040012E1 RID: 4833
		public const int SQLBIGBINARY = 173;

		// Token: 0x040012E2 RID: 4834
		public const int SQLBIGVARBINARY = 165;

		// Token: 0x040012E3 RID: 4835
		public const int SQLBITN = 104;

		// Token: 0x040012E4 RID: 4836
		public const int SQLNCHAR = 239;

		// Token: 0x040012E5 RID: 4837
		public const int SQLNVARCHAR = 231;

		// Token: 0x040012E6 RID: 4838
		public const int SQLNTEXT = 99;

		// Token: 0x040012E7 RID: 4839
		public const int SQLUDT = 240;

		// Token: 0x040012E8 RID: 4840
		public const int AOPCNTB = 9;

		// Token: 0x040012E9 RID: 4841
		public const int AOPSTDEV = 48;

		// Token: 0x040012EA RID: 4842
		public const int AOPSTDEVP = 49;

		// Token: 0x040012EB RID: 4843
		public const int AOPVAR = 50;

		// Token: 0x040012EC RID: 4844
		public const int AOPVARP = 51;

		// Token: 0x040012ED RID: 4845
		public const int AOPCNT = 75;

		// Token: 0x040012EE RID: 4846
		public const int AOPSUM = 77;

		// Token: 0x040012EF RID: 4847
		public const int AOPAVG = 79;

		// Token: 0x040012F0 RID: 4848
		public const int AOPMIN = 81;

		// Token: 0x040012F1 RID: 4849
		public const int AOPMAX = 82;

		// Token: 0x040012F2 RID: 4850
		public const int AOPANY = 83;

		// Token: 0x040012F3 RID: 4851
		public const int AOPNOOP = 86;

		// Token: 0x040012F4 RID: 4852
		public const int SQLTIMESTAMP = 80;

		// Token: 0x040012F5 RID: 4853
		public const int MAX_NUMERIC_LEN = 17;

		// Token: 0x040012F6 RID: 4854
		public const int DEFAULT_NUMERIC_PRECISION = 29;

		// Token: 0x040012F7 RID: 4855
		public const int SPHINX_DEFAULT_NUMERIC_PRECISION = 28;

		// Token: 0x040012F8 RID: 4856
		public const int MAX_NUMERIC_PRECISION = 38;

		// Token: 0x040012F9 RID: 4857
		public const byte UNKNOWN_PRECISION_SCALE = 255;

		// Token: 0x040012FA RID: 4858
		public const int SQLINT8 = 127;

		// Token: 0x040012FB RID: 4859
		public const int SQLVARIANT = 98;

		// Token: 0x040012FC RID: 4860
		public const int SQLXMLTYPE = 241;

		// Token: 0x040012FD RID: 4861
		public const int XMLUNICODEBOM = 65279;

		// Token: 0x040012FE RID: 4862
		public static readonly byte[] XMLUNICODEBOMBYTES = new byte[]
		{
			byte.MaxValue,
			254
		};

		// Token: 0x040012FF RID: 4863
		public const int SQLTABLE = 243;

		// Token: 0x04001300 RID: 4864
		public const int SQLDATE = 40;

		// Token: 0x04001301 RID: 4865
		public const int SQLTIME = 41;

		// Token: 0x04001302 RID: 4866
		public const int SQLDATETIME2 = 42;

		// Token: 0x04001303 RID: 4867
		public const int SQLDATETIMEOFFSET = 43;

		// Token: 0x04001304 RID: 4868
		public const int DEFAULT_VARTIME_SCALE = 7;

		// Token: 0x04001305 RID: 4869
		public const ulong SQL_PLP_NULL = 18446744073709551615UL;

		// Token: 0x04001306 RID: 4870
		public const ulong SQL_PLP_UNKNOWNLEN = 18446744073709551614UL;

		// Token: 0x04001307 RID: 4871
		public const int SQL_PLP_CHUNK_TERMINATOR = 0;

		// Token: 0x04001308 RID: 4872
		public const ushort SQL_USHORTVARMAXLEN = 65535;

		// Token: 0x04001309 RID: 4873
		public const byte TVP_ROWCOUNT_ESTIMATE = 18;

		// Token: 0x0400130A RID: 4874
		public const byte TVP_ROW_TOKEN = 1;

		// Token: 0x0400130B RID: 4875
		public const byte TVP_END_TOKEN = 0;

		// Token: 0x0400130C RID: 4876
		public const ushort TVP_NOMETADATA_TOKEN = 65535;

		// Token: 0x0400130D RID: 4877
		public const byte TVP_ORDER_UNIQUE_TOKEN = 16;

		// Token: 0x0400130E RID: 4878
		public const int TVP_DEFAULT_COLUMN = 512;

		// Token: 0x0400130F RID: 4879
		public const byte TVP_ORDERASC_FLAG = 1;

		// Token: 0x04001310 RID: 4880
		public const byte TVP_ORDERDESC_FLAG = 2;

		// Token: 0x04001311 RID: 4881
		public const byte TVP_UNIQUE_FLAG = 4;

		// Token: 0x04001312 RID: 4882
		public const bool Is68K = false;

		// Token: 0x04001313 RID: 4883
		public const bool TraceTDS = false;

		// Token: 0x04001314 RID: 4884
		public const string SP_EXECUTESQL = "sp_executesql";

		// Token: 0x04001315 RID: 4885
		public const string SP_PREPEXEC = "sp_prepexec";

		// Token: 0x04001316 RID: 4886
		public const string SP_PREPARE = "sp_prepare";

		// Token: 0x04001317 RID: 4887
		public const string SP_EXECUTE = "sp_execute";

		// Token: 0x04001318 RID: 4888
		public const string SP_UNPREPARE = "sp_unprepare";

		// Token: 0x04001319 RID: 4889
		public const string SP_PARAMS = "sp_procedure_params_rowset";

		// Token: 0x0400131A RID: 4890
		public const string SP_PARAMS_MANAGED = "sp_procedure_params_managed";

		// Token: 0x0400131B RID: 4891
		public const string SP_PARAMS_MGD10 = "sp_procedure_params_100_managed";

		// Token: 0x0400131C RID: 4892
		public const ushort RPC_PROCID_CURSOR = 1;

		// Token: 0x0400131D RID: 4893
		public const ushort RPC_PROCID_CURSOROPEN = 2;

		// Token: 0x0400131E RID: 4894
		public const ushort RPC_PROCID_CURSORPREPARE = 3;

		// Token: 0x0400131F RID: 4895
		public const ushort RPC_PROCID_CURSOREXECUTE = 4;

		// Token: 0x04001320 RID: 4896
		public const ushort RPC_PROCID_CURSORPREPEXEC = 5;

		// Token: 0x04001321 RID: 4897
		public const ushort RPC_PROCID_CURSORUNPREPARE = 6;

		// Token: 0x04001322 RID: 4898
		public const ushort RPC_PROCID_CURSORFETCH = 7;

		// Token: 0x04001323 RID: 4899
		public const ushort RPC_PROCID_CURSOROPTION = 8;

		// Token: 0x04001324 RID: 4900
		public const ushort RPC_PROCID_CURSORCLOSE = 9;

		// Token: 0x04001325 RID: 4901
		public const ushort RPC_PROCID_EXECUTESQL = 10;

		// Token: 0x04001326 RID: 4902
		public const ushort RPC_PROCID_PREPARE = 11;

		// Token: 0x04001327 RID: 4903
		public const ushort RPC_PROCID_EXECUTE = 12;

		// Token: 0x04001328 RID: 4904
		public const ushort RPC_PROCID_PREPEXEC = 13;

		// Token: 0x04001329 RID: 4905
		public const ushort RPC_PROCID_PREPEXECRPC = 14;

		// Token: 0x0400132A RID: 4906
		public const ushort RPC_PROCID_UNPREPARE = 15;

		// Token: 0x0400132B RID: 4907
		public const string TRANS_BEGIN = "BEGIN TRANSACTION";

		// Token: 0x0400132C RID: 4908
		public const string TRANS_COMMIT = "COMMIT TRANSACTION";

		// Token: 0x0400132D RID: 4909
		public const string TRANS_ROLLBACK = "ROLLBACK TRANSACTION";

		// Token: 0x0400132E RID: 4910
		public const string TRANS_IF_ROLLBACK = "IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION";

		// Token: 0x0400132F RID: 4911
		public const string TRANS_SAVE = "SAVE TRANSACTION";

		// Token: 0x04001330 RID: 4912
		public const string TRANS_READ_COMMITTED = "SET TRANSACTION ISOLATION LEVEL READ COMMITTED";

		// Token: 0x04001331 RID: 4913
		public const string TRANS_READ_UNCOMMITTED = "SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED";

		// Token: 0x04001332 RID: 4914
		public const string TRANS_REPEATABLE_READ = "SET TRANSACTION ISOLATION LEVEL REPEATABLE READ";

		// Token: 0x04001333 RID: 4915
		public const string TRANS_SERIALIZABLE = "SET TRANSACTION ISOLATION LEVEL SERIALIZABLE";

		// Token: 0x04001334 RID: 4916
		public const string TRANS_SNAPSHOT = "SET TRANSACTION ISOLATION LEVEL SNAPSHOT";

		// Token: 0x04001335 RID: 4917
		public const byte SHILOH_RPCBATCHFLAG = 128;

		// Token: 0x04001336 RID: 4918
		public const byte YUKON_RPCBATCHFLAG = 255;

		// Token: 0x04001337 RID: 4919
		public const byte RPC_RECOMPILE = 1;

		// Token: 0x04001338 RID: 4920
		public const byte RPC_NOMETADATA = 2;

		// Token: 0x04001339 RID: 4921
		public const byte RPC_PARAM_BYREF = 1;

		// Token: 0x0400133A RID: 4922
		public const byte RPC_PARAM_DEFAULT = 2;

		// Token: 0x0400133B RID: 4923
		public const byte RPC_PARAM_ENCRYPTED = 8;

		// Token: 0x0400133C RID: 4924
		public const string PARAM_OUTPUT = "output";

		// Token: 0x0400133D RID: 4925
		public const int MAX_PARAMETER_NAME_LENGTH = 128;

		// Token: 0x0400133E RID: 4926
		public const string FMTONLY_ON = " SET FMTONLY ON;";

		// Token: 0x0400133F RID: 4927
		public const string FMTONLY_OFF = " SET FMTONLY OFF;";

		// Token: 0x04001340 RID: 4928
		public const string BROWSE_ON = " SET NO_BROWSETABLE ON;";

		// Token: 0x04001341 RID: 4929
		public const string BROWSE_OFF = " SET NO_BROWSETABLE OFF;";

		// Token: 0x04001342 RID: 4930
		public const string TABLE = "Table";

		// Token: 0x04001343 RID: 4931
		public const int EXEC_THRESHOLD = 3;

		// Token: 0x04001344 RID: 4932
		public const short TIMEOUT_EXPIRED = -2;

		// Token: 0x04001345 RID: 4933
		public const short ENCRYPTION_NOT_SUPPORTED = 20;

		// Token: 0x04001346 RID: 4934
		public const int LOGON_FAILED = 18456;

		// Token: 0x04001347 RID: 4935
		public const int PASSWORD_EXPIRED = 18488;

		// Token: 0x04001348 RID: 4936
		public const int IMPERSONATION_FAILED = 1346;

		// Token: 0x04001349 RID: 4937
		public const int P_TOKENTOOLONG = 103;

		// Token: 0x0400134A RID: 4938
		public const int TCE_CONVERSION_ERROR_CLIENT_RETRY = 33514;

		// Token: 0x0400134B RID: 4939
		public const int TCE_ENCLAVE_INVALID_SESSION_HANDLE = 33195;

		// Token: 0x0400134C RID: 4940
		public const uint SNI_UNINITIALIZED = 4294967295U;

		// Token: 0x0400134D RID: 4941
		public const uint SNI_SUCCESS = 0U;

		// Token: 0x0400134E RID: 4942
		public const uint SNI_WAIT_TIMEOUT = 258U;

		// Token: 0x0400134F RID: 4943
		public const uint SNI_SUCCESS_IO_PENDING = 997U;

		// Token: 0x04001350 RID: 4944
		public const short SNI_WSAECONNRESET = 10054;

		// Token: 0x04001351 RID: 4945
		public const uint SNI_SSL_VALIDATE_CERTIFICATE = 1U;

		// Token: 0x04001352 RID: 4946
		public const uint SNI_SSL_USE_SCHANNEL_CACHE = 2U;

		// Token: 0x04001353 RID: 4947
		public const uint SNI_SSL_IGNORE_CHANNEL_BINDINGS = 16U;

		// Token: 0x04001354 RID: 4948
		public const string DEFAULT_ENGLISH_CODE_PAGE_STRING = "iso_1";

		// Token: 0x04001355 RID: 4949
		public const short DEFAULT_ENGLISH_CODE_PAGE_VALUE = 1252;

		// Token: 0x04001356 RID: 4950
		public const short CHARSET_CODE_PAGE_OFFSET = 2;

		// Token: 0x04001357 RID: 4951
		internal const int MAX_SERVERNAME = 255;

		// Token: 0x04001358 RID: 4952
		internal const ushort SELECT = 193;

		// Token: 0x04001359 RID: 4953
		internal const ushort INSERT = 195;

		// Token: 0x0400135A RID: 4954
		internal const ushort DELETE = 196;

		// Token: 0x0400135B RID: 4955
		internal const ushort UPDATE = 197;

		// Token: 0x0400135C RID: 4956
		internal const ushort ABORT = 210;

		// Token: 0x0400135D RID: 4957
		internal const ushort BEGINXACT = 212;

		// Token: 0x0400135E RID: 4958
		internal const ushort ENDXACT = 213;

		// Token: 0x0400135F RID: 4959
		internal const ushort BULKINSERT = 240;

		// Token: 0x04001360 RID: 4960
		internal const ushort OPENCURSOR = 32;

		// Token: 0x04001361 RID: 4961
		internal const ushort MERGE = 279;

		// Token: 0x04001362 RID: 4962
		internal const ushort MAXLEN_HOSTNAME = 128;

		// Token: 0x04001363 RID: 4963
		internal const ushort MAXLEN_USERNAME = 128;

		// Token: 0x04001364 RID: 4964
		internal const ushort MAXLEN_PASSWORD = 128;

		// Token: 0x04001365 RID: 4965
		internal const ushort MAXLEN_APPNAME = 128;

		// Token: 0x04001366 RID: 4966
		internal const ushort MAXLEN_SERVERNAME = 128;

		// Token: 0x04001367 RID: 4967
		internal const ushort MAXLEN_CLIENTINTERFACE = 128;

		// Token: 0x04001368 RID: 4968
		internal const ushort MAXLEN_LANGUAGE = 128;

		// Token: 0x04001369 RID: 4969
		internal const ushort MAXLEN_DATABASE = 128;

		// Token: 0x0400136A RID: 4970
		internal const ushort MAXLEN_ATTACHDBFILE = 260;

		// Token: 0x0400136B RID: 4971
		internal const ushort MAXLEN_NEWPASSWORD = 128;

		// Token: 0x0400136C RID: 4972
		public static readonly ushort[] CODE_PAGE_FROM_SORT_ID = new ushort[]
		{
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			437,
			437,
			437,
			437,
			437,
			0,
			0,
			0,
			0,
			0,
			850,
			850,
			850,
			850,
			850,
			0,
			0,
			0,
			0,
			850,
			1252,
			1252,
			1252,
			1252,
			1252,
			850,
			850,
			850,
			850,
			850,
			850,
			850,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			1252,
			1252,
			1252,
			1252,
			1252,
			0,
			0,
			0,
			0,
			1250,
			1250,
			1250,
			1250,
			1250,
			1250,
			1250,
			1250,
			1250,
			1250,
			1250,
			1250,
			1250,
			1250,
			1250,
			1250,
			1250,
			1250,
			1250,
			0,
			0,
			0,
			0,
			0,
			1251,
			1251,
			1251,
			1251,
			1251,
			0,
			0,
			0,
			1253,
			1253,
			1253,
			0,
			0,
			0,
			0,
			0,
			1253,
			1253,
			1253,
			0,
			1253,
			0,
			0,
			0,
			1254,
			1254,
			1254,
			0,
			0,
			0,
			0,
			0,
			1255,
			1255,
			1255,
			0,
			0,
			0,
			0,
			0,
			1256,
			1256,
			1256,
			0,
			0,
			0,
			0,
			0,
			1257,
			1257,
			1257,
			1257,
			1257,
			1257,
			1257,
			1257,
			1257,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			1252,
			1252,
			1252,
			1252,
			0,
			0,
			0,
			0,
			0,
			932,
			932,
			949,
			949,
			950,
			950,
			936,
			936,
			932,
			949,
			950,
			936,
			874,
			874,
			874,
			0,
			0,
			0,
			1252,
			1252,
			1252,
			1252,
			1252,
			1252,
			1252,
			1252,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0
		};

		// Token: 0x0400136D RID: 4973
		internal static readonly long[] TICKS_FROM_SCALE = new long[]
		{
			10000000L,
			1000000L,
			100000L,
			10000L,
			1000L,
			100L,
			10L,
			1L
		};

		// Token: 0x0400136E RID: 4974
		internal const int MAX_TIME_SCALE = 7;

		// Token: 0x0400136F RID: 4975
		internal const int MAX_TIME_LENGTH = 5;

		// Token: 0x04001370 RID: 4976
		internal const int MAX_DATETIME2_LENGTH = 8;

		// Token: 0x04001371 RID: 4977
		internal const int WHIDBEY_DATE_LENGTH = 10;

		// Token: 0x04001372 RID: 4978
		internal static readonly int[] WHIDBEY_TIME_LENGTH = new int[]
		{
			8,
			10,
			11,
			12,
			13,
			14,
			15,
			16
		};

		// Token: 0x04001373 RID: 4979
		internal static readonly int[] WHIDBEY_DATETIME2_LENGTH = new int[]
		{
			19,
			21,
			22,
			23,
			24,
			25,
			26,
			27
		};

		// Token: 0x04001374 RID: 4980
		internal static readonly int[] WHIDBEY_DATETIMEOFFSET_LENGTH = new int[]
		{
			26,
			28,
			29,
			30,
			31,
			32,
			33,
			34
		};

		// Token: 0x04001375 RID: 4981
		internal const byte MAX_SUPPORTED_TCE_VERSION = 2;

		// Token: 0x04001376 RID: 4982
		internal const byte MIN_TCE_VERSION_WITH_ENCLAVE_SUPPORT = 2;

		// Token: 0x04001377 RID: 4983
		internal const ushort MAX_TCE_CIPHERINFO_SIZE = 2048;

		// Token: 0x04001378 RID: 4984
		internal const long MAX_TCE_CIPHERTEXT_SIZE = 2147483648L;

		// Token: 0x04001379 RID: 4985
		internal const byte CustomCipherAlgorithmId = 0;

		// Token: 0x0400137A RID: 4986
		internal const int AES_256_CBC = 1;

		// Token: 0x0400137B RID: 4987
		internal const int AEAD_AES_256_CBC_HMAC_SHA256 = 2;

		// Token: 0x0400137C RID: 4988
		internal const string TCE_PARAM_CIPHERTEXT = "cipherText";

		// Token: 0x0400137D RID: 4989
		internal const string TCE_PARAM_CIPHER_ALGORITHM_ID = "cipherAlgorithmId";

		// Token: 0x0400137E RID: 4990
		internal const string TCE_PARAM_COLUMNENCRYPTION_KEY = "columnEncryptionKey";

		// Token: 0x0400137F RID: 4991
		internal const string TCE_PARAM_ENCRYPTION_ALGORITHM = "encryptionAlgorithm";

		// Token: 0x04001380 RID: 4992
		internal const string TCE_PARAM_ENCRYPTIONTYPE = "encryptionType";

		// Token: 0x04001381 RID: 4993
		internal const string TCE_PARAM_ENCRYPTIONKEY = "encryptionKey";

		// Token: 0x04001382 RID: 4994
		internal const string TCE_PARAM_MASTERKEY_PATH = "masterKeyPath";

		// Token: 0x04001383 RID: 4995
		internal const string TCE_PARAM_ENCRYPTED_CEK = "encryptedColumnEncryptionKey";

		// Token: 0x04001384 RID: 4996
		internal const string TCE_PARAM_CLIENT_KEYSTORE_PROVIDERS = "clientKeyStoreProviders";

		// Token: 0x04001385 RID: 4997
		internal const string TCE_PARAM_FORCE_COLUMN_ENCRYPTION = "ForceColumnEncryption(true)";

		// Token: 0x020003DD RID: 989
		[Flags]
		public enum FeatureExtension : uint
		{
			// Token: 0x04002117 RID: 8471
			None = 0U,
			// Token: 0x04002118 RID: 8472
			SessionRecovery = 1U,
			// Token: 0x04002119 RID: 8473
			FedAuth = 2U,
			// Token: 0x0400211A RID: 8474
			Tce = 4U,
			// Token: 0x0400211B RID: 8475
			GlobalTransactions = 8U,
			// Token: 0x0400211C RID: 8476
			AzureSQLSupport = 16U
		}

		// Token: 0x020003DE RID: 990
		public enum FedAuthLibrary : byte
		{
			// Token: 0x0400211E RID: 8478
			LiveId,
			// Token: 0x0400211F RID: 8479
			SecurityToken,
			// Token: 0x04002120 RID: 8480
			ADAL,
			// Token: 0x04002121 RID: 8481
			Default = 127
		}

		// Token: 0x020003DF RID: 991
		public enum ActiveDirectoryWorkflow : byte
		{
			// Token: 0x04002123 RID: 8483
			Password = 1,
			// Token: 0x04002124 RID: 8484
			Integrated,
			// Token: 0x04002125 RID: 8485
			Interactive
		}

		// Token: 0x020003E0 RID: 992
		internal enum TransactionManagerRequestType
		{
			// Token: 0x04002127 RID: 8487
			GetDTCAddress,
			// Token: 0x04002128 RID: 8488
			Propagate,
			// Token: 0x04002129 RID: 8489
			Begin = 5,
			// Token: 0x0400212A RID: 8490
			Promote,
			// Token: 0x0400212B RID: 8491
			Commit,
			// Token: 0x0400212C RID: 8492
			Rollback,
			// Token: 0x0400212D RID: 8493
			Save
		}

		// Token: 0x020003E1 RID: 993
		internal enum TransactionManagerIsolationLevel
		{
			// Token: 0x0400212F RID: 8495
			Unspecified,
			// Token: 0x04002130 RID: 8496
			ReadUncommitted,
			// Token: 0x04002131 RID: 8497
			ReadCommitted,
			// Token: 0x04002132 RID: 8498
			RepeatableRead,
			// Token: 0x04002133 RID: 8499
			Serializable,
			// Token: 0x04002134 RID: 8500
			Snapshot
		}

		// Token: 0x020003E2 RID: 994
		internal enum FedAuthInfoId : byte
		{
			// Token: 0x04002136 RID: 8502
			Stsurl = 1,
			// Token: 0x04002137 RID: 8503
			Spn
		}
	}
}
