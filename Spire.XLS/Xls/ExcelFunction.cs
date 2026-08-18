using System;
using System.ComponentModel;

namespace Spire.Xls
{
	// Token: 0x02000078 RID: 120
	[CLSCompliant(false)]
	public enum ExcelFunction
	{
		// Token: 0x04000303 RID: 771
		NONE = 65535,
		// Token: 0x04000304 RID: 772
		CustomFunction = 255,
		// Token: 0x04000305 RID: 773
		[DefaultValue(1)]
		[sprᨳ(2)]
		ABS = 24,
		// Token: 0x04000306 RID: 774
		[sprᨳ(2)]
		[DefaultValue(1)]
		ACOS = 99,
		// Token: 0x04000307 RID: 775
		[DefaultValue(1)]
		[sprᨳ(2)]
		ACOSH = 233,
		// Token: 0x04000308 RID: 776
		[sprᨳ(1)]
		ADDRESS = 219,
		// Token: 0x04000309 RID: 777
		[sprᨳ(2)]
		AND = 36,
		// Token: 0x0400030A RID: 778
		[DefaultValue(1)]
		[sprᨳ(1)]
		AREAS = 75,
		// Token: 0x0400030B RID: 779
		[DefaultValue(1)]
		[sprᨳ(2)]
		ASIN = 98,
		// Token: 0x0400030C RID: 780
		[sprᨳ(2)]
		[DefaultValue(1)]
		ASINH = 232,
		// Token: 0x0400030D RID: 781
		[sprᨳ(2)]
		[DefaultValue(1)]
		ATAN = 18,
		// Token: 0x0400030E RID: 782
		[sprᨳ(2)]
		[DefaultValue(2)]
		ATAN2 = 97,
		// Token: 0x0400030F RID: 783
		[DefaultValue(1)]
		[sprᨳ(2)]
		ATANH = 234,
		// Token: 0x04000310 RID: 784
		[sprᨳ(1)]
		[sprᨳ(typeof(spr\u2372), 3)]
		AVEDEV = 269,
		// Token: 0x04000311 RID: 785
		[sprᨳ(1)]
		AVERAGE = 5,
		// Token: 0x04000312 RID: 786
		[sprᨳ(1)]
		AVERAGEA = 361,
		// Token: 0x04000313 RID: 787
		HEX2BIN = 400,
		// Token: 0x04000314 RID: 788
		HEX2DEC,
		// Token: 0x04000315 RID: 789
		HEX2OCT,
		// Token: 0x04000316 RID: 790
		COUNTIFS,
		// Token: 0x04000317 RID: 791
		BIN2DEC,
		// Token: 0x04000318 RID: 792
		BIN2HEX,
		// Token: 0x04000319 RID: 793
		BIN2OCT,
		// Token: 0x0400031A RID: 794
		DEC2BIN,
		// Token: 0x0400031B RID: 795
		DEC2HEX,
		// Token: 0x0400031C RID: 796
		DEC2OCT,
		// Token: 0x0400031D RID: 797
		OCT2BIN,
		// Token: 0x0400031E RID: 798
		OCT2DEC,
		// Token: 0x0400031F RID: 799
		OCT2HEX,
		// Token: 0x04000320 RID: 800
		ODDFPRICE,
		// Token: 0x04000321 RID: 801
		ODDFYIELD,
		// Token: 0x04000322 RID: 802
		ODDLPRICE,
		// Token: 0x04000323 RID: 803
		ODDLYIELD,
		// Token: 0x04000324 RID: 804
		ISODD,
		// Token: 0x04000325 RID: 805
		ISEVEN,
		// Token: 0x04000326 RID: 806
		LCM,
		// Token: 0x04000327 RID: 807
		GCD,
		// Token: 0x04000328 RID: 808
		SUMIFS,
		// Token: 0x04000329 RID: 809
		AVERAGEIF,
		// Token: 0x0400032A RID: 810
		AVERAGEIFS,
		// Token: 0x0400032B RID: 811
		CONVERT,
		// Token: 0x0400032C RID: 812
		COMPLEX,
		// Token: 0x0400032D RID: 813
		COUPDAYBS,
		// Token: 0x0400032E RID: 814
		COUPDAYS,
		// Token: 0x0400032F RID: 815
		COUPDAYSNC,
		// Token: 0x04000330 RID: 816
		COUPNCD,
		// Token: 0x04000331 RID: 817
		COUPNUM,
		// Token: 0x04000332 RID: 818
		COUPPCD,
		// Token: 0x04000333 RID: 819
		DELTA,
		// Token: 0x04000334 RID: 820
		DISC,
		// Token: 0x04000335 RID: 821
		DOLLARDE,
		// Token: 0x04000336 RID: 822
		DOLLARFR,
		// Token: 0x04000337 RID: 823
		DURATION,
		// Token: 0x04000338 RID: 824
		EDATE,
		// Token: 0x04000339 RID: 825
		EFFECT,
		// Token: 0x0400033A RID: 826
		EOMONTH,
		// Token: 0x0400033B RID: 827
		ERF,
		// Token: 0x0400033C RID: 828
		ERFC,
		// Token: 0x0400033D RID: 829
		FACTDOUBLE,
		// Token: 0x0400033E RID: 830
		GESTEP,
		// Token: 0x0400033F RID: 831
		IFERROR,
		// Token: 0x04000340 RID: 832
		IMABS,
		// Token: 0x04000341 RID: 833
		IMAGINARY,
		// Token: 0x04000342 RID: 834
		IMARGUMENT,
		// Token: 0x04000343 RID: 835
		IMCONJUGATE,
		// Token: 0x04000344 RID: 836
		IMCOS,
		// Token: 0x04000345 RID: 837
		IMEXP,
		// Token: 0x04000346 RID: 838
		IMLN,
		// Token: 0x04000347 RID: 839
		IMLOG10,
		// Token: 0x04000348 RID: 840
		IMLOG2,
		// Token: 0x04000349 RID: 841
		IMREAL,
		// Token: 0x0400034A RID: 842
		IMSIN,
		// Token: 0x0400034B RID: 843
		IMSQRT,
		// Token: 0x0400034C RID: 844
		IMSUB,
		// Token: 0x0400034D RID: 845
		IMSUM,
		// Token: 0x0400034E RID: 846
		IMDIV,
		// Token: 0x0400034F RID: 847
		IMPOWER,
		// Token: 0x04000350 RID: 848
		IMPRODUCT,
		// Token: 0x04000351 RID: 849
		ACCRINT,
		// Token: 0x04000352 RID: 850
		ACCRINTM,
		// Token: 0x04000353 RID: 851
		AGGREGATE,
		// Token: 0x04000354 RID: 852
		AMORDEGRC,
		// Token: 0x04000355 RID: 853
		AMORLINC,
		// Token: 0x04000356 RID: 854
		BAHTTEXT,
		// Token: 0x04000357 RID: 855
		BESSELI,
		// Token: 0x04000358 RID: 856
		BESSELJ,
		// Token: 0x04000359 RID: 857
		BESSELK,
		// Token: 0x0400035A RID: 858
		BESSELY,
		// Token: 0x0400035B RID: 859
		CUBEKPIMEMBER,
		// Token: 0x0400035C RID: 860
		CUBEMEMBER,
		// Token: 0x0400035D RID: 861
		CUBERANKEDMEMBER,
		// Token: 0x0400035E RID: 862
		CUBESET,
		// Token: 0x0400035F RID: 863
		CUBESETCOUNT,
		// Token: 0x04000360 RID: 864
		CUBEMEMBERPROPERTY,
		// Token: 0x04000361 RID: 865
		CUMIPMT,
		// Token: 0x04000362 RID: 866
		CUMPRINC,
		// Token: 0x04000363 RID: 867
		FVSCHEDULE,
		// Token: 0x04000364 RID: 868
		INTRATE,
		// Token: 0x04000365 RID: 869
		LINTEST,
		// Token: 0x04000366 RID: 870
		CUBEVALUE,
		// Token: 0x04000367 RID: 871
		MDURATION,
		// Token: 0x04000368 RID: 872
		MROUND,
		// Token: 0x04000369 RID: 873
		MULTINOMIAL,
		// Token: 0x0400036A RID: 874
		NETWORKDAYS,
		// Token: 0x0400036B RID: 875
		NOMINAL,
		// Token: 0x0400036C RID: 876
		PRICE,
		// Token: 0x0400036D RID: 877
		PRICEDISC,
		// Token: 0x0400036E RID: 878
		PRICEMAT,
		// Token: 0x0400036F RID: 879
		QUOTIENT,
		// Token: 0x04000370 RID: 880
		RANDBETWEEN,
		// Token: 0x04000371 RID: 881
		RECEIVED,
		// Token: 0x04000372 RID: 882
		SERIESSUM,
		// Token: 0x04000373 RID: 883
		SQRTPI,
		// Token: 0x04000374 RID: 884
		TBILLEQ,
		// Token: 0x04000375 RID: 885
		TBILLPRICE,
		// Token: 0x04000376 RID: 886
		TBILLYIELD,
		// Token: 0x04000377 RID: 887
		WEEKNUM,
		// Token: 0x04000378 RID: 888
		WORKDAY,
		// Token: 0x04000379 RID: 889
		XIRR,
		// Token: 0x0400037A RID: 890
		XNPV,
		// Token: 0x0400037B RID: 891
		YEARFRAC,
		// Token: 0x0400037C RID: 892
		YIELD,
		// Token: 0x0400037D RID: 893
		YIELDDISC,
		// Token: 0x0400037E RID: 894
		YIELDMAT,
		// Token: 0x0400037F RID: 895
		WORKDAYINTL,
		// Token: 0x04000380 RID: 896
		BETA_INV,
		// Token: 0x04000381 RID: 897
		BINOM_DIST,
		// Token: 0x04000382 RID: 898
		BINOM_INV,
		// Token: 0x04000383 RID: 899
		CEILING_PRECISE,
		// Token: 0x04000384 RID: 900
		CHISQ_DIST,
		// Token: 0x04000385 RID: 901
		CHISQ_DIST_RT,
		// Token: 0x04000386 RID: 902
		CHISQ_INV,
		// Token: 0x04000387 RID: 903
		CHISQ_INV_RT,
		// Token: 0x04000388 RID: 904
		CHISQ_TEST,
		// Token: 0x04000389 RID: 905
		CONFIDENCE_NORM,
		// Token: 0x0400038A RID: 906
		CONFIDENCE_T,
		// Token: 0x0400038B RID: 907
		COVARIANCE_P,
		// Token: 0x0400038C RID: 908
		COVARIANCE_S,
		// Token: 0x0400038D RID: 909
		ERF_PRECISE,
		// Token: 0x0400038E RID: 910
		ERFC_PRECISE,
		// Token: 0x0400038F RID: 911
		F_DIST,
		// Token: 0x04000390 RID: 912
		F_DIST_RT,
		// Token: 0x04000391 RID: 913
		F_INV,
		// Token: 0x04000392 RID: 914
		F_INV_RT,
		// Token: 0x04000393 RID: 915
		F_TEST,
		// Token: 0x04000394 RID: 916
		FLOOR_PRECISE,
		// Token: 0x04000395 RID: 917
		GAMMA_DIST,
		// Token: 0x04000396 RID: 918
		GAMMA_INV,
		// Token: 0x04000397 RID: 919
		GAMMALN_PRECISE,
		// Token: 0x04000398 RID: 920
		HYPGEOM_DIST,
		// Token: 0x04000399 RID: 921
		LOGNORM_DIST,
		// Token: 0x0400039A RID: 922
		LOGNORM_INV,
		// Token: 0x0400039B RID: 923
		MODE_MULT,
		// Token: 0x0400039C RID: 924
		MODE_SNGL,
		// Token: 0x0400039D RID: 925
		NEGBINOM_DIST,
		// Token: 0x0400039E RID: 926
		NETWORKDAYS_INTL,
		// Token: 0x0400039F RID: 927
		NORM_DIST,
		// Token: 0x040003A0 RID: 928
		NORM_INV,
		// Token: 0x040003A1 RID: 929
		NORM_S_DIST,
		// Token: 0x040003A2 RID: 930
		PERCENTILE_EXC,
		// Token: 0x040003A3 RID: 931
		PERCENTILE_INC,
		// Token: 0x040003A4 RID: 932
		PERCENTRANK_EXC,
		// Token: 0x040003A5 RID: 933
		PERCENTRANK_INC,
		// Token: 0x040003A6 RID: 934
		POISSON_DIST,
		// Token: 0x040003A7 RID: 935
		QUARTILE_EXC,
		// Token: 0x040003A8 RID: 936
		QUARTILE_INC,
		// Token: 0x040003A9 RID: 937
		RANK_AVG,
		// Token: 0x040003AA RID: 938
		RANK_EQ,
		// Token: 0x040003AB RID: 939
		STDEV_P,
		// Token: 0x040003AC RID: 940
		STDEV_S,
		// Token: 0x040003AD RID: 941
		T_DIST,
		// Token: 0x040003AE RID: 942
		T_DIST_2T,
		// Token: 0x040003AF RID: 943
		T_DIST_RT,
		// Token: 0x040003B0 RID: 944
		T_INV,
		// Token: 0x040003B1 RID: 945
		T_INV_2T,
		// Token: 0x040003B2 RID: 946
		T_TEST,
		// Token: 0x040003B3 RID: 947
		VAR_P,
		// Token: 0x040003B4 RID: 948
		VAR_S,
		// Token: 0x040003B5 RID: 949
		WEIBULL_DIST,
		// Token: 0x040003B6 RID: 950
		WORKDAY_INTL,
		// Token: 0x040003B7 RID: 951
		Z_TEST,
		// Token: 0x040003B8 RID: 952
		BETA_DIST,
		// Token: 0x040003B9 RID: 953
		EUROCONVERT,
		// Token: 0x040003BA RID: 954
		PHONETIC,
		// Token: 0x040003BB RID: 955
		REGISTER_ID,
		// Token: 0x040003BC RID: 956
		SQL_REQUEST,
		// Token: 0x040003BD RID: 957
		JIS,
		// Token: 0x040003BE RID: 958
		EXPON_DIST,
		// Token: 0x040003BF RID: 959
		[sprᨳ(2)]
		BETADIST = 270,
		// Token: 0x040003C0 RID: 960
		[sprᨳ(2)]
		BETAINV = 272,
		// Token: 0x040003C1 RID: 961
		[DefaultValue(4)]
		[sprᨳ(2)]
		BINOMDIST,
		// Token: 0x040003C2 RID: 962
		[sprᨳ(2)]
		[DefaultValue(2)]
		CEILING = 288,
		// Token: 0x040003C3 RID: 963
		[sprᨳ(new int[]
		{
			2,
			1
		})]
		CELL = 125,
		// Token: 0x040003C4 RID: 964
		[DefaultValue(1)]
		[sprᨳ(2)]
		CHAR = 111,
		// Token: 0x040003C5 RID: 965
		[DefaultValue(2)]
		[sprᨳ(2)]
		CHIDIST = 274,
		// Token: 0x040003C6 RID: 966
		[DefaultValue(2)]
		[sprᨳ(2)]
		CHIINV,
		// Token: 0x040003C7 RID: 967
		[sprᨳ(3)]
		[DefaultValue(2)]
		CHITEST = 306,
		// Token: 0x040003C8 RID: 968
		CHOOSE = 100,
		// Token: 0x040003C9 RID: 969
		[DefaultValue(1)]
		[sprᨳ(2)]
		CLEAN = 162,
		// Token: 0x040003CA RID: 970
		[DefaultValue(1)]
		[sprᨳ(2)]
		CODE = 121,
		// Token: 0x040003CB RID: 971
		[sprᨳ(1)]
		COLUMN = 9,
		// Token: 0x040003CC RID: 972
		[DefaultValue(1)]
		[sprᨳ(1)]
		COLUMNS = 77,
		// Token: 0x040003CD RID: 973
		[DefaultValue(2)]
		[sprᨳ(2)]
		COMBIN = 276,
		// Token: 0x040003CE RID: 974
		[sprᨳ(2)]
		CONCATENATE = 336,
		// Token: 0x040003CF RID: 975
		[DefaultValue(3)]
		[sprᨳ(2)]
		CONFIDENCE = 277,
		// Token: 0x040003D0 RID: 976
		[sprᨳ(3)]
		[DefaultValue(2)]
		CORREL = 307,
		// Token: 0x040003D1 RID: 977
		[sprᨳ(2)]
		[DefaultValue(1)]
		COS = 16,
		// Token: 0x040003D2 RID: 978
		[sprᨳ(2)]
		[DefaultValue(1)]
		COSH = 230,
		// Token: 0x040003D3 RID: 979
		[sprᨳ(1)]
		COUNT = 0,
		// Token: 0x040003D4 RID: 980
		[sprᨳ(1)]
		COUNTA = 169,
		// Token: 0x040003D5 RID: 981
		[DefaultValue(1)]
		[sprᨳ(1)]
		COUNTBLANK = 347,
		// Token: 0x040003D6 RID: 982
		[sprᨳ(new int[]
		{
			1,
			2
		})]
		[DefaultValue(2)]
		COUNTIF = 346,
		// Token: 0x040003D7 RID: 983
		[sprᨳ(3)]
		[DefaultValue(2)]
		COVAR = 308,
		// Token: 0x040003D8 RID: 984
		[sprᨳ(2)]
		[DefaultValue(3)]
		CRITBINOM = 278,
		// Token: 0x040003D9 RID: 985
		[sprᨳ(2)]
		[DefaultValue(3)]
		DATE = 65,
		// Token: 0x040003DA RID: 986
		[DefaultValue(1)]
		[sprᨳ(2)]
		DATEVALUE = 140,
		// Token: 0x040003DB RID: 987
		[DefaultValue(3)]
		[sprᨳ(1)]
		DAVERAGE = 42,
		// Token: 0x040003DC RID: 988
		[sprᨳ(2)]
		[DefaultValue(1)]
		DAY = 67,
		// Token: 0x040003DD RID: 989
		[sprᨳ(2)]
		DAYS360 = 220,
		// Token: 0x040003DE RID: 990
		[sprᨳ(2)]
		DB = 247,
		// Token: 0x040003DF RID: 991
		[DefaultValue(3)]
		[sprᨳ(1)]
		DCOUNT = 40,
		// Token: 0x040003E0 RID: 992
		[DefaultValue(3)]
		[sprᨳ(1)]
		DCOUNTA = 199,
		// Token: 0x040003E1 RID: 993
		[sprᨳ(2)]
		DDB = 144,
		// Token: 0x040003E2 RID: 994
		[DefaultValue(1)]
		[sprᨳ(2)]
		DEGREES = 343,
		// Token: 0x040003E3 RID: 995
		[sprᨳ(1)]
		DEVSQ = 318,
		// Token: 0x040003E4 RID: 996
		[DefaultValue(3)]
		[sprᨳ(1)]
		DMAX = 44,
		// Token: 0x040003E5 RID: 997
		[DefaultValue(3)]
		[sprᨳ(1)]
		DMIN = 43,
		// Token: 0x040003E6 RID: 998
		[sprᨳ(2)]
		DOLLAR = 13,
		// Token: 0x040003E7 RID: 999
		[DefaultValue(3)]
		[sprᨳ(1)]
		DPRODUCT = 189,
		// Token: 0x040003E8 RID: 1000
		[sprᨳ(1)]
		[DefaultValue(3)]
		DSTDEV = 45,
		// Token: 0x040003E9 RID: 1001
		[sprᨳ(1)]
		[DefaultValue(3)]
		DSTDEVP = 195,
		// Token: 0x040003EA RID: 1002
		[sprᨳ(1)]
		[DefaultValue(3)]
		DSUM = 41,
		// Token: 0x040003EB RID: 1003
		[DefaultValue(3)]
		[sprᨳ(1)]
		DVAR = 47,
		// Token: 0x040003EC RID: 1004
		[DefaultValue(3)]
		[sprᨳ(1)]
		DVARP = 196,
		// Token: 0x040003ED RID: 1005
		[sprᨳ(1)]
		ERROR = 84,
		// Token: 0x040003EE RID: 1006
		[DefaultValue(1)]
		[sprᨳ(2)]
		[Description("ERROR.TYPE")]
		ERRORTYPE = 261,
		// Token: 0x040003EF RID: 1007
		[sprᨳ(2)]
		[DefaultValue(1)]
		EVEN = 279,
		// Token: 0x040003F0 RID: 1008
		[DefaultValue(2)]
		[sprᨳ(2)]
		EXACT = 117,
		// Token: 0x040003F1 RID: 1009
		[sprᨳ(2)]
		[DefaultValue(1)]
		EXP = 21,
		// Token: 0x040003F2 RID: 1010
		[sprᨳ(2)]
		[DefaultValue(3)]
		EXPONDIST = 280,
		// Token: 0x040003F3 RID: 1011
		[DefaultValue(1)]
		[sprᨳ(2)]
		FACT = 184,
		// Token: 0x040003F4 RID: 1012
		[sprᨳ(1)]
		[DefaultValue(0)]
		FALSE = 35,
		// Token: 0x040003F5 RID: 1013
		[sprᨳ(2)]
		[DefaultValue(3)]
		FDIST = 281,
		// Token: 0x040003F6 RID: 1014
		[sprᨳ(2)]
		FIND = 124,
		// Token: 0x040003F7 RID: 1015
		[sprᨳ(2)]
		FINDB = 205,
		// Token: 0x040003F8 RID: 1016
		[DefaultValue(3)]
		[sprᨳ(2)]
		FINV = 282,
		// Token: 0x040003F9 RID: 1017
		[sprᨳ(2)]
		[DefaultValue(1)]
		FISHER,
		// Token: 0x040003FA RID: 1018
		[DefaultValue(1)]
		[sprᨳ(2)]
		FISHERINV,
		// Token: 0x040003FB RID: 1019
		[sprᨳ(2)]
		FIXED = 14,
		// Token: 0x040003FC RID: 1020
		[DefaultValue(2)]
		[sprᨳ(2)]
		FLOOR = 285,
		// Token: 0x040003FD RID: 1021
		[DefaultValue(3)]
		[sprᨳ(new int[]
		{
			2,
			3,
			3
		})]
		FORECAST = 309,
		// Token: 0x040003FE RID: 1022
		[DefaultValue(2)]
		[sprᨳ(1)]
		FREQUENCY = 252,
		// Token: 0x040003FF RID: 1023
		[DefaultValue(2)]
		[sprᨳ(3)]
		FTEST = 310,
		// Token: 0x04000400 RID: 1024
		[sprᨳ(2)]
		FV = 57,
		// Token: 0x04000401 RID: 1025
		[sprᨳ(2)]
		[DefaultValue(4)]
		GAMMADIST = 286,
		// Token: 0x04000402 RID: 1026
		[DefaultValue(3)]
		[sprᨳ(2)]
		GAMMAINV,
		// Token: 0x04000403 RID: 1027
		[DefaultValue(1)]
		[sprᨳ(2)]
		GAMMALN = 271,
		// Token: 0x04000404 RID: 1028
		[sprᨳ(1)]
		GEOMEAN = 319,
		// Token: 0x04000405 RID: 1029
		[sprᨳ(1)]
		GETPIVOTDATA = 358,
		// Token: 0x04000406 RID: 1030
		[sprᨳ(1)]
		GROWTH = 52,
		// Token: 0x04000407 RID: 1031
		[sprᨳ(1)]
		HARMEAN = 320,
		// Token: 0x04000408 RID: 1032
		[sprᨳ(new int[]
		{
			2,
			1
		})]
		HLOOKUP = 101,
		// Token: 0x04000409 RID: 1033
		[sprᨳ(2)]
		[DefaultValue(1)]
		HOUR = 71,
		// Token: 0x0400040A RID: 1034
		[sprᨳ(2)]
		HYPERLINK = 359,
		// Token: 0x0400040B RID: 1035
		[DefaultValue(4)]
		[sprᨳ(2)]
		HYPGEOMDIST = 289,
		// Token: 0x0400040C RID: 1036
		[sprᨳ(2)]
		IF = 1,
		// Token: 0x0400040D RID: 1037
		[sprᨳ(typeof(spr\u2372), 3)]
		[sprᨳ(1)]
		INDEX = 29,
		// Token: 0x0400040E RID: 1038
		[sprᨳ(2)]
		INDIRECT = 148,
		// Token: 0x0400040F RID: 1039
		[sprᨳ(2)]
		[DefaultValue(1)]
		INFO = 244,
		// Token: 0x04000410 RID: 1040
		[sprᨳ(2)]
		[DefaultValue(1)]
		INT = 25,
		// Token: 0x04000411 RID: 1041
		[DefaultValue(2)]
		[sprᨳ(3)]
		INTERCEPT = 311,
		// Token: 0x04000412 RID: 1042
		[sprᨳ(2)]
		IPMT = 167,
		// Token: 0x04000413 RID: 1043
		[sprᨳ(1)]
		[sprᨳ(typeof(spr\u2372), 3)]
		IRR = 62,
		// Token: 0x04000414 RID: 1044
		[DefaultValue(1)]
		[sprᨳ(2)]
		ISBLANK = 129,
		// Token: 0x04000415 RID: 1045
		[DefaultValue(1)]
		[sprᨳ(2)]
		ISERR = 126,
		// Token: 0x04000416 RID: 1046
		[sprᨳ(2)]
		[DefaultValue(1)]
		ISERROR = 3,
		// Token: 0x04000417 RID: 1047
		[DefaultValue(1)]
		[sprᨳ(2)]
		ISLOGICAL = 198,
		// Token: 0x04000418 RID: 1048
		[sprᨳ(2)]
		[DefaultValue(1)]
		ISNA = 2,
		// Token: 0x04000419 RID: 1049
		[sprᨳ(2)]
		[DefaultValue(1)]
		ISNONTEXT = 190,
		// Token: 0x0400041A RID: 1050
		[sprᨳ(2)]
		[DefaultValue(1)]
		ISNUMBER = 128,
		// Token: 0x0400041B RID: 1051
		[sprᨳ(2)]
		[DefaultValue(4)]
		ISPMT = 350,
		// Token: 0x0400041C RID: 1052
		[sprᨳ(1)]
		[DefaultValue(1)]
		ISREF = 105,
		// Token: 0x0400041D RID: 1053
		[sprᨳ(2)]
		[DefaultValue(1)]
		ISTEXT = 127,
		// Token: 0x0400041E RID: 1054
		[sprᨳ(typeof(spr\u2372), 3)]
		[sprᨳ(1)]
		KURT = 322,
		// Token: 0x0400041F RID: 1055
		[sprᨳ(typeof(spr\u2372), 3)]
		[DefaultValue(2)]
		[sprᨳ(1)]
		LARGE = 325,
		// Token: 0x04000420 RID: 1056
		[sprᨳ(2)]
		LEFT = 115,
		// Token: 0x04000421 RID: 1057
		[sprᨳ(2)]
		LEFTB = 208,
		// Token: 0x04000422 RID: 1058
		[sprᨳ(2)]
		[DefaultValue(1)]
		LEN = 32,
		// Token: 0x04000423 RID: 1059
		[DefaultValue(1)]
		[sprᨳ(2)]
		LENB = 211,
		// Token: 0x04000424 RID: 1060
		[sprᨳ(1)]
		LINEST = 49,
		// Token: 0x04000425 RID: 1061
		[sprᨳ(2)]
		[DefaultValue(1)]
		LN = 22,
		// Token: 0x04000426 RID: 1062
		[sprᨳ(2)]
		LOG = 109,
		// Token: 0x04000427 RID: 1063
		[DefaultValue(1)]
		[sprᨳ(2)]
		LOG10 = 23,
		// Token: 0x04000428 RID: 1064
		[sprᨳ(1)]
		LOGEST = 51,
		// Token: 0x04000429 RID: 1065
		[sprᨳ(2)]
		[DefaultValue(3)]
		LOGINV = 291,
		// Token: 0x0400042A RID: 1066
		[DefaultValue(3)]
		[sprᨳ(2)]
		LOGNORMDIST = 290,
		// Token: 0x0400042B RID: 1067
		[sprᨳ(typeof(spr\u2372), 3)]
		[sprᨳ(new int[]
		{
			2,
			1,
			1
		})]
		LOOKUP = 28,
		// Token: 0x0400042C RID: 1068
		[DefaultValue(1)]
		[sprᨳ(2)]
		LOWER = 112,
		// Token: 0x0400042D RID: 1069
		[sprᨳ(new int[]
		{
			2,
			1
		})]
		[sprᨳ(typeof(spr\u2372), 3)]
		MATCH = 64,
		// Token: 0x0400042E RID: 1070
		[sprᨳ(1)]
		[sprᨳ(typeof(spr\u2372), 3)]
		MAX = 7,
		// Token: 0x0400042F RID: 1071
		[sprᨳ(typeof(spr\u2372), 3)]
		[sprᨳ(1)]
		MAXA = 362,
		// Token: 0x04000430 RID: 1072
		[sprᨳ(3)]
		[DefaultValue(1)]
		MDETERM = 163,
		// Token: 0x04000431 RID: 1073
		[sprᨳ(typeof(spr\u2372), 3)]
		[sprᨳ(1)]
		MEDIAN = 227,
		// Token: 0x04000432 RID: 1074
		[sprᨳ(2)]
		[DefaultValue(3)]
		MID = 31,
		// Token: 0x04000433 RID: 1075
		[sprᨳ(2)]
		[DefaultValue(3)]
		MIDB = 210,
		// Token: 0x04000434 RID: 1076
		[sprᨳ(1)]
		[sprᨳ(typeof(spr\u2372), 3)]
		MIN = 6,
		// Token: 0x04000435 RID: 1077
		[sprᨳ(typeof(spr\u2372), 3)]
		[sprᨳ(1)]
		MINA = 363,
		// Token: 0x04000436 RID: 1078
		[sprᨳ(2)]
		[DefaultValue(1)]
		MINUTE = 72,
		// Token: 0x04000437 RID: 1079
		[sprᨳ(3)]
		[DefaultValue(1)]
		MINVERSE = 164,
		// Token: 0x04000438 RID: 1080
		[sprᨳ(new int[]
		{
			1,
			2,
			2
		})]
		[DefaultValue(3)]
		[sprᨳ(typeof(spr\u2372), 3)]
		MIRR = 61,
		// Token: 0x04000439 RID: 1081
		[DefaultValue(2)]
		[sprᨳ(3)]
		MMULT = 165,
		// Token: 0x0400043A RID: 1082
		[sprᨳ(2)]
		[DefaultValue(2)]
		MOD = 39,
		// Token: 0x0400043B RID: 1083
		[sprᨳ(3)]
		MODE = 330,
		// Token: 0x0400043C RID: 1084
		[sprᨳ(2)]
		[DefaultValue(1)]
		MONTH = 68,
		// Token: 0x0400043D RID: 1085
		[DefaultValue(1)]
		[sprᨳ(1)]
		N = 131,
		// Token: 0x0400043E RID: 1086
		[sprᨳ(1)]
		[DefaultValue(0)]
		NA = 10,
		// Token: 0x0400043F RID: 1087
		[DefaultValue(3)]
		[sprᨳ(2)]
		NEGBINOMDIST = 292,
		// Token: 0x04000440 RID: 1088
		[DefaultValue(4)]
		[sprᨳ(2)]
		NORMDIST,
		// Token: 0x04000441 RID: 1089
		[sprᨳ(2)]
		[DefaultValue(3)]
		NORMINV = 295,
		// Token: 0x04000442 RID: 1090
		[DefaultValue(1)]
		[sprᨳ(2)]
		NORMSDIST = 294,
		// Token: 0x04000443 RID: 1091
		[DefaultValue(1)]
		[sprᨳ(2)]
		NORMSINV = 296,
		// Token: 0x04000444 RID: 1092
		[sprᨳ(2)]
		[DefaultValue(1)]
		NOT = 38,
		// Token: 0x04000445 RID: 1093
		[DefaultValue(0)]
		[sprᨳ(1)]
		NOW = 74,
		// Token: 0x04000446 RID: 1094
		[sprᨳ(2)]
		NPER = 58,
		// Token: 0x04000447 RID: 1095
		[sprᨳ(typeof(sprᲔ), new int[]
		{
			2,
			1
		})]
		[sprᨳ(2)]
		NPV = 11,
		// Token: 0x04000448 RID: 1096
		[DefaultValue(1)]
		[sprᨳ(2)]
		ODD = 298,
		// Token: 0x04000449 RID: 1097
		[sprᨳ(1)]
		OFFSET = 78,
		// Token: 0x0400044A RID: 1098
		[sprᨳ(2)]
		OR = 37,
		// Token: 0x0400044B RID: 1099
		[sprᨳ(3)]
		[DefaultValue(2)]
		PEARSON = 312,
		// Token: 0x0400044C RID: 1100
		[sprᨳ(new int[]
		{
			1,
			2
		})]
		[DefaultValue(2)]
		PERCENTILE = 328,
		// Token: 0x0400044D RID: 1101
		[sprᨳ(new int[]
		{
			1,
			2
		})]
		PERCENTRANK,
		// Token: 0x0400044E RID: 1102
		[DefaultValue(2)]
		[sprᨳ(2)]
		PERMUT = 299,
		// Token: 0x0400044F RID: 1103
		[DefaultValue(0)]
		[sprᨳ(1)]
		PI = 19,
		// Token: 0x04000450 RID: 1104
		[sprᨳ(2)]
		PMT = 59,
		// Token: 0x04000451 RID: 1105
		[sprᨳ(2)]
		[DefaultValue(3)]
		POISSON = 300,
		// Token: 0x04000452 RID: 1106
		[sprᨳ(2)]
		[DefaultValue(2)]
		POWER = 337,
		// Token: 0x04000453 RID: 1107
		[sprᨳ(2)]
		PPMT = 168,
		// Token: 0x04000454 RID: 1108
		[sprᨳ(new int[]
		{
			3,
			3,
			2
		})]
		PROB = 317,
		// Token: 0x04000455 RID: 1109
		[sprᨳ(typeof(spr\u2372), 3)]
		[sprᨳ(1)]
		PRODUCT = 183,
		// Token: 0x04000456 RID: 1110
		[DefaultValue(1)]
		[sprᨳ(2)]
		PROPER = 114,
		// Token: 0x04000457 RID: 1111
		[sprᨳ(2)]
		PV = 56,
		// Token: 0x04000458 RID: 1112
		[DefaultValue(2)]
		[sprᨳ(new int[]
		{
			1,
			2
		})]
		QUARTILE = 327,
		// Token: 0x04000459 RID: 1113
		[sprᨳ(2)]
		[DefaultValue(1)]
		RADIANS = 342,
		// Token: 0x0400045A RID: 1114
		[sprᨳ(1)]
		[DefaultValue(0)]
		RAND = 63,
		// Token: 0x0400045B RID: 1115
		[sprᨳ(new int[]
		{
			2,
			1
		})]
		RANK = 216,
		// Token: 0x0400045C RID: 1116
		[sprᨳ(2)]
		RATE = 60,
		// Token: 0x0400045D RID: 1117
		[sprᨳ(2)]
		[DefaultValue(4)]
		REPLACE = 119,
		// Token: 0x0400045E RID: 1118
		[sprᨳ(2)]
		[DefaultValue(4)]
		REPLACEB = 207,
		// Token: 0x0400045F RID: 1119
		[sprᨳ(2)]
		RIGHT = 116,
		// Token: 0x04000460 RID: 1120
		[sprᨳ(2)]
		RIGHTB = 209,
		// Token: 0x04000461 RID: 1121
		[sprᨳ(2)]
		ROMAN = 354,
		// Token: 0x04000462 RID: 1122
		[sprᨳ(2)]
		[DefaultValue(2)]
		ROUND = 27,
		// Token: 0x04000463 RID: 1123
		[DefaultValue(2)]
		[sprᨳ(2)]
		ROUNDDOWN = 213,
		// Token: 0x04000464 RID: 1124
		[DefaultValue(2)]
		[sprᨳ(2)]
		ROUNDUP = 212,
		// Token: 0x04000465 RID: 1125
		[sprᨳ(1)]
		ROW = 8,
		// Token: 0x04000466 RID: 1126
		[sprᨳ(1)]
		[DefaultValue(1)]
		ROWS = 76,
		// Token: 0x04000467 RID: 1127
		[DefaultValue(2)]
		[sprᨳ(3)]
		RSQ = 313,
		// Token: 0x04000468 RID: 1128
		[sprᨳ(2)]
		SEARCH = 82,
		// Token: 0x04000469 RID: 1129
		[sprᨳ(2)]
		SEARCHB = 206,
		// Token: 0x0400046A RID: 1130
		[DefaultValue(1)]
		[sprᨳ(2)]
		SECOND = 73,
		// Token: 0x0400046B RID: 1131
		[DefaultValue(1)]
		[sprᨳ(2)]
		SIGN = 26,
		// Token: 0x0400046C RID: 1132
		[DefaultValue(1)]
		[sprᨳ(2)]
		SIN = 15,
		// Token: 0x0400046D RID: 1133
		[DefaultValue(1)]
		[sprᨳ(2)]
		SINH = 229,
		// Token: 0x0400046E RID: 1134
		[sprᨳ(1)]
		SKEW = 323,
		// Token: 0x0400046F RID: 1135
		[DefaultValue(3)]
		[sprᨳ(2)]
		SLN = 142,
		// Token: 0x04000470 RID: 1136
		[sprᨳ(3)]
		[DefaultValue(2)]
		SLOPE = 315,
		// Token: 0x04000471 RID: 1137
		[DefaultValue(2)]
		[sprᨳ(1)]
		SMALL = 326,
		// Token: 0x04000472 RID: 1138
		[DefaultValue(1)]
		[sprᨳ(2)]
		SQRT = 20,
		// Token: 0x04000473 RID: 1139
		[DefaultValue(3)]
		[sprᨳ(2)]
		STANDARDIZE = 297,
		// Token: 0x04000474 RID: 1140
		[sprᨳ(1)]
		STDEV = 12,
		// Token: 0x04000475 RID: 1141
		[sprᨳ(1)]
		STDEVA = 366,
		// Token: 0x04000476 RID: 1142
		[sprᨳ(1)]
		STDEVP = 193,
		// Token: 0x04000477 RID: 1143
		[sprᨳ(1)]
		STDEVPA = 364,
		// Token: 0x04000478 RID: 1144
		[sprᨳ(3)]
		[DefaultValue(2)]
		STEYX = 314,
		// Token: 0x04000479 RID: 1145
		[sprᨳ(2)]
		SUBSTITUTE = 120,
		// Token: 0x0400047A RID: 1146
		[sprᨳ(new int[]
		{
			2,
			1
		})]
		SUBTOTAL = 344,
		// Token: 0x0400047B RID: 1147
		[sprᨳ(1)]
		SUM = 4,
		// Token: 0x0400047C RID: 1148
		[sprᨳ(1)]
		SUMIF = 345,
		// Token: 0x0400047D RID: 1149
		[sprᨳ(3)]
		SUMPRODUCT = 228,
		// Token: 0x0400047E RID: 1150
		[sprᨳ(1)]
		SUMSQ = 321,
		// Token: 0x0400047F RID: 1151
		[sprᨳ(3)]
		[DefaultValue(2)]
		SUMX2MY2 = 304,
		// Token: 0x04000480 RID: 1152
		[DefaultValue(2)]
		[sprᨳ(3)]
		SUMX2PY2,
		// Token: 0x04000481 RID: 1153
		[sprᨳ(3)]
		[DefaultValue(2)]
		SUMXMY2 = 303,
		// Token: 0x04000482 RID: 1154
		[sprᨳ(2)]
		[DefaultValue(4)]
		SYD = 143,
		// Token: 0x04000483 RID: 1155
		[DefaultValue(1)]
		[sprᨳ(1)]
		T = 130,
		// Token: 0x04000484 RID: 1156
		[sprᨳ(2)]
		[DefaultValue(1)]
		TAN = 17,
		// Token: 0x04000485 RID: 1157
		[sprᨳ(2)]
		[DefaultValue(1)]
		TANH = 231,
		// Token: 0x04000486 RID: 1158
		[sprᨳ(2)]
		[DefaultValue(3)]
		TDIST = 301,
		// Token: 0x04000487 RID: 1159
		[sprᨳ(2)]
		[DefaultValue(2)]
		TEXT = 48,
		// Token: 0x04000488 RID: 1160
		[DefaultValue(3)]
		[sprᨳ(2)]
		TIME = 66,
		// Token: 0x04000489 RID: 1161
		[sprᨳ(2)]
		[DefaultValue(1)]
		TIMEVALUE = 141,
		// Token: 0x0400048A RID: 1162
		[DefaultValue(2)]
		[sprᨳ(2)]
		TINV = 332,
		// Token: 0x0400048B RID: 1163
		[sprᨳ(1)]
		[DefaultValue(0)]
		TODAY = 221,
		// Token: 0x0400048C RID: 1164
		[DefaultValue(1)]
		[sprᨳ(typeof(spr\u2372), 3)]
		[sprᨳ(2)]
		TRANSPOSE = 83,
		// Token: 0x0400048D RID: 1165
		[sprᨳ(1)]
		[sprᨳ(typeof(spr\u2372), 3)]
		TREND = 50,
		// Token: 0x0400048E RID: 1166
		[DefaultValue(1)]
		[sprᨳ(2)]
		TRIM = 118,
		// Token: 0x0400048F RID: 1167
		[DefaultValue(2)]
		[sprᨳ(3)]
		TRIMMEAN = 331,
		// Token: 0x04000490 RID: 1168
		[sprᨳ(1)]
		[DefaultValue(0)]
		TRUE = 34,
		// Token: 0x04000491 RID: 1169
		[sprᨳ(2)]
		TRUNC = 197,
		// Token: 0x04000492 RID: 1170
		[sprᨳ(3)]
		[DefaultValue(4)]
		TTEST = 316,
		// Token: 0x04000493 RID: 1171
		[sprᨳ(typeof(spr\u2372), 3)]
		[sprᨳ(2)]
		[DefaultValue(1)]
		TYPE = 86,
		// Token: 0x04000494 RID: 1172
		[DefaultValue(1)]
		[sprᨳ(2)]
		UPPER = 113,
		// Token: 0x04000495 RID: 1173
		[sprᨳ(2)]
		[DefaultValue(1)]
		VALUE = 33,
		// Token: 0x04000496 RID: 1174
		[sprᨳ(1)]
		VAR = 46,
		// Token: 0x04000497 RID: 1175
		[sprᨳ(1)]
		VARA = 367,
		// Token: 0x04000498 RID: 1176
		[sprᨳ(1)]
		VARP = 194,
		// Token: 0x04000499 RID: 1177
		[sprᨳ(1)]
		VARPA = 365,
		// Token: 0x0400049A RID: 1178
		[sprᨳ(2)]
		VDB = 222,
		// Token: 0x0400049B RID: 1179
		[sprᨳ(new int[]
		{
			2,
			1
		})]
		VLOOKUP = 102,
		// Token: 0x0400049C RID: 1180
		[sprᨳ(2)]
		WEEKDAY = 70,
		// Token: 0x0400049D RID: 1181
		[DefaultValue(4)]
		[sprᨳ(2)]
		WEIBULL = 302,
		// Token: 0x0400049E RID: 1182
		[DefaultValue(1)]
		[sprᨳ(2)]
		YEAR = 69,
		// Token: 0x0400049F RID: 1183
		[sprᨳ(typeof(spr\u2372), 3)]
		[sprᨳ(1)]
		ZTEST = 324,
		// Token: 0x040004A0 RID: 1184
		[DefaultValue(0)]
		ABSREF = 79,
		// Token: 0x040004A1 RID: 1185
		[DefaultValue(0)]
		ACTIVECELL = 94,
		// Token: 0x040004A2 RID: 1186
		[DefaultValue(1)]
		ADDBAR = 151,
		// Token: 0x040004A3 RID: 1187
		[DefaultValue(1)]
		ADDCOMMAND = 153,
		// Token: 0x040004A4 RID: 1188
		[DefaultValue(1)]
		ADDMENU = 152,
		// Token: 0x040004A5 RID: 1189
		[DefaultValue(1)]
		ADDTOOLBAR = 253,
		// Token: 0x040004A6 RID: 1190
		[DefaultValue(0)]
		APPTITLE = 262,
		// Token: 0x040004A7 RID: 1191
		[DefaultValue(1)]
		ARGUMENT = 81,
		// Token: 0x040004A8 RID: 1192
		[DefaultValue(1)]
		ASC = 214,
		// Token: 0x040004A9 RID: 1193
		[DefaultValue(1)]
		CALL = 150,
		// Token: 0x040004AA RID: 1194
		[DefaultValue(0)]
		CALLER = 89,
		// Token: 0x040004AB RID: 1195
		[DefaultValue(0)]
		CANCELKEY = 170,
		// Token: 0x040004AC RID: 1196
		[DefaultValue(1)]
		CHECKCOMMAND = 155,
		// Token: 0x040004AD RID: 1197
		[DefaultValue(1)]
		CREATEOBJECT = 236,
		// Token: 0x040004AE RID: 1198
		[DefaultValue(1)]
		CUSTOMREPEAT = 240,
		// Token: 0x040004AF RID: 1199
		[DefaultValue(1)]
		CUSTOMUNDO = 239,
		// Token: 0x040004B0 RID: 1200
		[DefaultValue(3)]
		DATEDIF = 351,
		// Token: 0x040004B1 RID: 1201
		[DefaultValue(1)]
		DATESTRING,
		// Token: 0x040004B2 RID: 1202
		[DefaultValue(1)]
		DBCS = 215,
		// Token: 0x040004B3 RID: 1203
		[DefaultValue(1)]
		DELETEBAR = 200,
		// Token: 0x040004B4 RID: 1204
		[DefaultValue(1)]
		DELETECOMMAND = 159,
		// Token: 0x040004B5 RID: 1205
		[DefaultValue(1)]
		DELETEMENU = 158,
		// Token: 0x040004B6 RID: 1206
		[DefaultValue(1)]
		DELETETOOLBAR = 254,
		// Token: 0x040004B7 RID: 1207
		[DefaultValue(1)]
		DEREF = 90,
		// Token: 0x040004B8 RID: 1208
		[DefaultValue(3)]
		[sprᨳ(1)]
		DGET = 235,
		// Token: 0x040004B9 RID: 1209
		[DefaultValue(1)]
		DIALOGBOX = 161,
		// Token: 0x040004BA RID: 1210
		[DefaultValue(1)]
		DIRECTORY = 123,
		// Token: 0x040004BB RID: 1211
		[DefaultValue(1)]
		DOCUMENTS = 93,
		// Token: 0x040004BC RID: 1212
		[DefaultValue(1)]
		ECHO = 87,
		// Token: 0x040004BD RID: 1213
		[DefaultValue(1)]
		ENABLECOMMAND = 154,
		// Token: 0x040004BE RID: 1214
		[DefaultValue(1)]
		ENABLETOOL = 265,
		// Token: 0x040004BF RID: 1215
		[DefaultValue(1)]
		EVALUATE = 257,
		// Token: 0x040004C0 RID: 1216
		[DefaultValue(1)]
		EXEC = 110,
		// Token: 0x040004C1 RID: 1217
		[DefaultValue(1)]
		EXECUTE = 178,
		// Token: 0x040004C2 RID: 1218
		[DefaultValue(1)]
		FILES = 166,
		// Token: 0x040004C3 RID: 1219
		[DefaultValue(1)]
		FOPEN = 132,
		// Token: 0x040004C4 RID: 1220
		[DefaultValue(1)]
		FORMULACONVERT = 241,
		// Token: 0x040004C5 RID: 1221
		[DefaultValue(1)]
		FPOS = 139,
		// Token: 0x040004C6 RID: 1222
		[DefaultValue(1)]
		FREAD = 136,
		// Token: 0x040004C7 RID: 1223
		[DefaultValue(1)]
		FREADLN = 135,
		// Token: 0x040004C8 RID: 1224
		[DefaultValue(1)]
		FSIZE = 134,
		// Token: 0x040004C9 RID: 1225
		[DefaultValue(1)]
		FWRITE = 138,
		// Token: 0x040004CA RID: 1226
		[DefaultValue(1)]
		FWRITELN = 137,
		// Token: 0x040004CB RID: 1227
		[DefaultValue(1)]
		FCLOSE = 133,
		// Token: 0x040004CC RID: 1228
		[DefaultValue(1)]
		GETBAR = 182,
		// Token: 0x040004CD RID: 1229
		[DefaultValue(1)]
		GETCELL = 185,
		// Token: 0x040004CE RID: 1230
		[DefaultValue(1)]
		GETCHARTITEM = 160,
		// Token: 0x040004CF RID: 1231
		[DefaultValue(1)]
		GETDEF = 145,
		// Token: 0x040004D0 RID: 1232
		[DefaultValue(0)]
		GETDOCUMENT = 188,
		// Token: 0x040004D1 RID: 1233
		[DefaultValue(1)]
		GETFORMULA = 106,
		// Token: 0x040004D2 RID: 1234
		[DefaultValue(1)]
		GETLINKINFO = 242,
		// Token: 0x040004D3 RID: 1235
		[DefaultValue(1)]
		GETMOVIE = 335,
		// Token: 0x040004D4 RID: 1236
		[DefaultValue(1)]
		GETNAME = 107,
		// Token: 0x040004D5 RID: 1237
		[DefaultValue(1)]
		GETNOTE = 191,
		// Token: 0x040004D6 RID: 1238
		[DefaultValue(1)]
		GETOBJECT = 246,
		// Token: 0x040004D7 RID: 1239
		[DefaultValue(1)]
		GETPIVOTFIELD = 340,
		// Token: 0x040004D8 RID: 1240
		[DefaultValue(1)]
		GETPIVOTITEM,
		// Token: 0x040004D9 RID: 1241
		[DefaultValue(1)]
		GETPIVOTTABLE = 339,
		// Token: 0x040004DA RID: 1242
		[DefaultValue(1)]
		GETTOOL = 259,
		// Token: 0x040004DB RID: 1243
		[DefaultValue(1)]
		GETTOOLBAR = 258,
		// Token: 0x040004DC RID: 1244
		[DefaultValue(1)]
		GETWINDOW = 187,
		// Token: 0x040004DD RID: 1245
		[DefaultValue(1)]
		GETWORKBOOK = 268,
		// Token: 0x040004DE RID: 1246
		[DefaultValue(1)]
		GETWORKSPACE = 186,
		// Token: 0x040004DF RID: 1247
		[DefaultValue(1)]
		GOTO = 53,
		// Token: 0x040004E0 RID: 1248
		[DefaultValue(1)]
		GROUP = 245,
		// Token: 0x040004E1 RID: 1249
		[DefaultValue(1)]
		HALT = 54,
		// Token: 0x040004E2 RID: 1250
		[DefaultValue(1)]
		HELP = 181,
		// Token: 0x040004E3 RID: 1251
		[DefaultValue(0)]
		INITIATE = 175,
		// Token: 0x040004E4 RID: 1252
		[DefaultValue(0)]
		INPUT = 104,
		// Token: 0x040004E5 RID: 1253
		[DefaultValue(0)]
		LASTERROR = 238,
		// Token: 0x040004E6 RID: 1254
		[DefaultValue(0)]
		LINKS = 103,
		// Token: 0x040004E7 RID: 1255
		[DefaultValue(1)]
		MOVIECOMMAND = 334,
		// Token: 0x040004E8 RID: 1256
		[DefaultValue(1)]
		NAMES = 122,
		// Token: 0x040004E9 RID: 1257
		[DefaultValue(1)]
		NOTE = 192,
		// Token: 0x040004EA RID: 1258
		[DefaultValue(1)]
		NUMBERSTRING = 353,
		// Token: 0x040004EB RID: 1259
		[DefaultValue(1)]
		OPENDIALOG = 355,
		// Token: 0x040004EC RID: 1260
		[DefaultValue(1)]
		OPTIONSLISTSGET = 349,
		// Token: 0x040004ED RID: 1261
		[DefaultValue(0)]
		PAUSE = 248,
		// Token: 0x040004EE RID: 1262
		[DefaultValue(1)]
		PIVOTADDDATA = 338,
		// Token: 0x040004EF RID: 1263
		[DefaultValue(1)]
		POKE = 177,
		// Token: 0x040004F0 RID: 1264
		[DefaultValue(1)]
		PRESSTOOL = 266,
		// Token: 0x040004F1 RID: 1265
		[DefaultValue(1)]
		REFTEXT = 146,
		// Token: 0x040004F2 RID: 1266
		[DefaultValue(1)]
		REGISTER = 149,
		// Token: 0x040004F3 RID: 1267
		[DefaultValue(1)]
		REGISTERID = 267,
		// Token: 0x040004F4 RID: 1268
		[DefaultValue(1)]
		RELREF = 80,
		// Token: 0x040004F5 RID: 1269
		[DefaultValue(2)]
		RENAMECOMMAND = 156,
		// Token: 0x040004F6 RID: 1270
		[DefaultValue(2)]
		REPT = 30,
		// Token: 0x040004F7 RID: 1271
		[DefaultValue(1)]
		REQUEST = 176,
		// Token: 0x040004F8 RID: 1272
		[DefaultValue(1)]
		RESETTOOLBAR = 256,
		// Token: 0x040004F9 RID: 1273
		[DefaultValue(0)]
		RESTART = 180,
		// Token: 0x040004FA RID: 1274
		[DefaultValue(0)]
		RESULT = 96,
		// Token: 0x040004FB RID: 1275
		[DefaultValue(0)]
		RESUME = 251,
		// Token: 0x040004FC RID: 1276
		[DefaultValue(0)]
		SAVEDIALOG = 356,
		// Token: 0x040004FD RID: 1277
		[DefaultValue(0)]
		SAVETOOLBAR = 264,
		// Token: 0x040004FE RID: 1278
		[DefaultValue(0)]
		SCENARIOGET = 348,
		// Token: 0x040004FF RID: 1279
		[DefaultValue(0)]
		SELECTION = 95,
		// Token: 0x04000500 RID: 1280
		[DefaultValue(1)]
		SERIES = 92,
		// Token: 0x04000501 RID: 1281
		[DefaultValue(1)]
		SETNAME = 88,
		// Token: 0x04000502 RID: 1282
		[DefaultValue(1)]
		SETVALUE = 108,
		// Token: 0x04000503 RID: 1283
		[DefaultValue(0)]
		SHOWBAR = 157,
		// Token: 0x04000504 RID: 1284
		[DefaultValue(1)]
		SPELLINGCHECK = 260,
		// Token: 0x04000505 RID: 1285
		[DefaultValue(1)]
		STEP = 85,
		// Token: 0x04000506 RID: 1286
		[DefaultValue(1)]
		TERMINATE = 179,
		// Token: 0x04000507 RID: 1287
		[DefaultValue(1)]
		TEXTBOX = 243,
		// Token: 0x04000508 RID: 1288
		[DefaultValue(1)]
		TEXTREF = 147,
		// Token: 0x04000509 RID: 1289
		[DefaultValue(1)]
		UNREGISTER = 201,
		// Token: 0x0400050A RID: 1290
		[DefaultValue(1)]
		USDOLLAR = 204,
		// Token: 0x0400050B RID: 1291
		[DefaultValue(1)]
		VOLATILE = 237,
		// Token: 0x0400050C RID: 1292
		[DefaultValue(1)]
		WINDOWS = 91,
		// Token: 0x0400050D RID: 1293
		[DefaultValue(1)]
		WINDOWTITLE = 263
	}
}
