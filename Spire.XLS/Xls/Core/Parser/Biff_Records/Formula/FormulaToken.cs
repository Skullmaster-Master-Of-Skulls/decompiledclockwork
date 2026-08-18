using System;

namespace Spire.Xls.Core.Parser.Biff_Records.Formula
{
	// Token: 0x0200063F RID: 1599
	public enum FormulaToken
	{
		// Token: 0x04002EAD RID: 11949
		None,
		// Token: 0x04002EAE RID: 11950
		tAdd = 3,
		// Token: 0x04002EAF RID: 11951
		tSub,
		// Token: 0x04002EB0 RID: 11952
		tMul,
		// Token: 0x04002EB1 RID: 11953
		tDiv,
		// Token: 0x04002EB2 RID: 11954
		tPower,
		// Token: 0x04002EB3 RID: 11955
		tConcat,
		// Token: 0x04002EB4 RID: 11956
		tLessThan,
		// Token: 0x04002EB5 RID: 11957
		tLessEqual,
		// Token: 0x04002EB6 RID: 11958
		tEqual,
		// Token: 0x04002EB7 RID: 11959
		tGreaterEqual,
		// Token: 0x04002EB8 RID: 11960
		tGreater,
		// Token: 0x04002EB9 RID: 11961
		tNotEqual,
		// Token: 0x04002EBA RID: 11962
		tCellRangeIntersection,
		// Token: 0x04002EBB RID: 11963
		tCellRangeList,
		// Token: 0x04002EBC RID: 11964
		tCellRange,
		// Token: 0x04002EBD RID: 11965
		tUnaryPlus,
		// Token: 0x04002EBE RID: 11966
		tUnaryMinus,
		// Token: 0x04002EBF RID: 11967
		tPercent,
		// Token: 0x04002EC0 RID: 11968
		tParentheses,
		// Token: 0x04002EC1 RID: 11969
		tFunction1 = 33,
		// Token: 0x04002EC2 RID: 11970
		tFunction2 = 65,
		// Token: 0x04002EC3 RID: 11971
		tFunction3 = 97,
		// Token: 0x04002EC4 RID: 11972
		tFunctionVar1 = 34,
		// Token: 0x04002EC5 RID: 11973
		tFunctionVar2 = 66,
		// Token: 0x04002EC6 RID: 11974
		tFunctionVar3 = 98,
		// Token: 0x04002EC7 RID: 11975
		tFunctionCE1 = 56,
		// Token: 0x04002EC8 RID: 11976
		tFunctionCE2 = 88,
		// Token: 0x04002EC9 RID: 11977
		tFunctionCE3 = 120,
		// Token: 0x04002ECA RID: 11978
		tMissingArgument = 22,
		// Token: 0x04002ECB RID: 11979
		tStringConstant,
		// Token: 0x04002ECC RID: 11980
		tError = 28,
		// Token: 0x04002ECD RID: 11981
		tBoolean,
		// Token: 0x04002ECE RID: 11982
		tInteger,
		// Token: 0x04002ECF RID: 11983
		tNumber,
		// Token: 0x04002ED0 RID: 11984
		tExp = 1,
		// Token: 0x04002ED1 RID: 11985
		tTbl,
		// Token: 0x04002ED2 RID: 11986
		tExtended = 24,
		// Token: 0x04002ED3 RID: 11987
		tAttr,
		// Token: 0x04002ED4 RID: 11988
		tSheet,
		// Token: 0x04002ED5 RID: 11989
		tEndSheet,
		// Token: 0x04002ED6 RID: 11990
		tArray1 = 32,
		// Token: 0x04002ED7 RID: 11991
		tArray2 = 64,
		// Token: 0x04002ED8 RID: 11992
		tArray3 = 96,
		// Token: 0x04002ED9 RID: 11993
		tName1 = 35,
		// Token: 0x04002EDA RID: 11994
		tName2 = 67,
		// Token: 0x04002EDB RID: 11995
		tName3 = 99,
		// Token: 0x04002EDC RID: 11996
		tRef1 = 36,
		// Token: 0x04002EDD RID: 11997
		tRef2 = 68,
		// Token: 0x04002EDE RID: 11998
		tRef3 = 100,
		// Token: 0x04002EDF RID: 11999
		tArea1 = 37,
		// Token: 0x04002EE0 RID: 12000
		tArea2 = 69,
		// Token: 0x04002EE1 RID: 12001
		tArea3 = 101,
		// Token: 0x04002EE2 RID: 12002
		tMemArea1 = 38,
		// Token: 0x04002EE3 RID: 12003
		tMemArea2 = 70,
		// Token: 0x04002EE4 RID: 12004
		tMemArea3 = 102,
		// Token: 0x04002EE5 RID: 12005
		tMemErr1 = 39,
		// Token: 0x04002EE6 RID: 12006
		tMemErr2 = 71,
		// Token: 0x04002EE7 RID: 12007
		tMemErr3 = 103,
		// Token: 0x04002EE8 RID: 12008
		tMemNoMem1 = 40,
		// Token: 0x04002EE9 RID: 12009
		tMemNoMem2 = 72,
		// Token: 0x04002EEA RID: 12010
		tMemNoMem3 = 104,
		// Token: 0x04002EEB RID: 12011
		tMemFunc1 = 41,
		// Token: 0x04002EEC RID: 12012
		tMemFunc2 = 73,
		// Token: 0x04002EED RID: 12013
		tMemFunc3 = 105,
		// Token: 0x04002EEE RID: 12014
		tRefErr1 = 42,
		// Token: 0x04002EEF RID: 12015
		tRefErr2 = 74,
		// Token: 0x04002EF0 RID: 12016
		tRefErr3 = 106,
		// Token: 0x04002EF1 RID: 12017
		tAreaErr1 = 43,
		// Token: 0x04002EF2 RID: 12018
		tAreaErr2 = 75,
		// Token: 0x04002EF3 RID: 12019
		tAreaErr3 = 107,
		// Token: 0x04002EF4 RID: 12020
		tRefN1 = 44,
		// Token: 0x04002EF5 RID: 12021
		tRefN2 = 76,
		// Token: 0x04002EF6 RID: 12022
		tRefN3 = 108,
		// Token: 0x04002EF7 RID: 12023
		tAreaN1 = 45,
		// Token: 0x04002EF8 RID: 12024
		tAreaN2 = 77,
		// Token: 0x04002EF9 RID: 12025
		tAreaN3 = 109,
		// Token: 0x04002EFA RID: 12026
		tMemAreaN1 = 46,
		// Token: 0x04002EFB RID: 12027
		tMemAreaN2 = 78,
		// Token: 0x04002EFC RID: 12028
		tMemAreaN3 = 110,
		// Token: 0x04002EFD RID: 12029
		tMemNoMemN1 = 47,
		// Token: 0x04002EFE RID: 12030
		tMemNoMemN2 = 79,
		// Token: 0x04002EFF RID: 12031
		tMemNoMemN3 = 111,
		// Token: 0x04002F00 RID: 12032
		tNameX1 = 57,
		// Token: 0x04002F01 RID: 12033
		tNameX2 = 89,
		// Token: 0x04002F02 RID: 12034
		tNameX3 = 121,
		// Token: 0x04002F03 RID: 12035
		tRef3d1 = 58,
		// Token: 0x04002F04 RID: 12036
		tRef3d2 = 90,
		// Token: 0x04002F05 RID: 12037
		tRef3d3 = 122,
		// Token: 0x04002F06 RID: 12038
		tArea3d1 = 59,
		// Token: 0x04002F07 RID: 12039
		tArea3d2 = 91,
		// Token: 0x04002F08 RID: 12040
		tArea3d3 = 123,
		// Token: 0x04002F09 RID: 12041
		tRefErr3d1 = 60,
		// Token: 0x04002F0A RID: 12042
		tRefErr3d2 = 92,
		// Token: 0x04002F0B RID: 12043
		tRefErr3d3 = 124,
		// Token: 0x04002F0C RID: 12044
		tAreaErr3d1 = 61,
		// Token: 0x04002F0D RID: 12045
		tAreaErr3d2 = 93,
		// Token: 0x04002F0E RID: 12046
		tAreaErr3d3 = 125,
		// Token: 0x04002F0F RID: 12047
		EndOfFormula = 4097,
		// Token: 0x04002F10 RID: 12048
		CloseParenthesis,
		// Token: 0x04002F11 RID: 12049
		Comma,
		// Token: 0x04002F12 RID: 12050
		OpenBracket,
		// Token: 0x04002F13 RID: 12051
		CloseBracket,
		// Token: 0x04002F14 RID: 12052
		ValueTrue,
		// Token: 0x04002F15 RID: 12053
		ValueFalse,
		// Token: 0x04002F16 RID: 12054
		Space,
		// Token: 0x04002F17 RID: 12055
		Identifier,
		// Token: 0x04002F18 RID: 12056
		DDELink,
		// Token: 0x04002F19 RID: 12057
		Identifier3D
	}
}
