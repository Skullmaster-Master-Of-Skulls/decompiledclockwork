using System;

namespace System.Data
{
	// Token: 0x020000FA RID: 250
	internal sealed class Operators
	{
		// Token: 0x0600100E RID: 4110 RVA: 0x0008052C File Offset: 0x0007F92C
		private Operators()
		{
		}

		// Token: 0x0600100F RID: 4111 RVA: 0x00080540 File Offset: 0x0007F940
		internal static bool IsArithmetical(int op)
		{
			return op == 15 || op == 16 || op == 17 || op == 18 || op == 20;
		}

		// Token: 0x06001010 RID: 4112 RVA: 0x00080568 File Offset: 0x0007F968
		internal static bool IsLogical(int op)
		{
			return op == 26 || op == 27 || op == 3 || op == 13 || op == 39;
		}

		// Token: 0x06001011 RID: 4113 RVA: 0x00080590 File Offset: 0x0007F990
		internal static bool IsRelational(int op)
		{
			return 7 <= op && op <= 12;
		}

		// Token: 0x06001012 RID: 4114 RVA: 0x000805AC File Offset: 0x0007F9AC
		internal static int Priority(int op)
		{
			if (op > Operators.priority.Length)
			{
				return 24;
			}
			return Operators.priority[op];
		}

		// Token: 0x06001013 RID: 4115 RVA: 0x000805D0 File Offset: 0x0007F9D0
		internal static string ToString(int op)
		{
			string result;
			if (op <= Operators.Looks.Length)
			{
				result = Operators.Looks[op];
			}
			else
			{
				result = "Unknown op";
			}
			return result;
		}

		// Token: 0x04000521 RID: 1313
		internal const int Noop = 0;

		// Token: 0x04000522 RID: 1314
		internal const int Negative = 1;

		// Token: 0x04000523 RID: 1315
		internal const int UnaryPlus = 2;

		// Token: 0x04000524 RID: 1316
		internal const int Not = 3;

		// Token: 0x04000525 RID: 1317
		internal const int BetweenAnd = 4;

		// Token: 0x04000526 RID: 1318
		internal const int In = 5;

		// Token: 0x04000527 RID: 1319
		internal const int Between = 6;

		// Token: 0x04000528 RID: 1320
		internal const int EqualTo = 7;

		// Token: 0x04000529 RID: 1321
		internal const int GreaterThen = 8;

		// Token: 0x0400052A RID: 1322
		internal const int LessThen = 9;

		// Token: 0x0400052B RID: 1323
		internal const int GreaterOrEqual = 10;

		// Token: 0x0400052C RID: 1324
		internal const int LessOrEqual = 11;

		// Token: 0x0400052D RID: 1325
		internal const int NotEqual = 12;

		// Token: 0x0400052E RID: 1326
		internal const int Is = 13;

		// Token: 0x0400052F RID: 1327
		internal const int Like = 14;

		// Token: 0x04000530 RID: 1328
		internal const int Plus = 15;

		// Token: 0x04000531 RID: 1329
		internal const int Minus = 16;

		// Token: 0x04000532 RID: 1330
		internal const int Multiply = 17;

		// Token: 0x04000533 RID: 1331
		internal const int Divide = 18;

		// Token: 0x04000534 RID: 1332
		internal const int Modulo = 20;

		// Token: 0x04000535 RID: 1333
		internal const int BitwiseAnd = 22;

		// Token: 0x04000536 RID: 1334
		internal const int BitwiseOr = 23;

		// Token: 0x04000537 RID: 1335
		internal const int BitwiseXor = 24;

		// Token: 0x04000538 RID: 1336
		internal const int BitwiseNot = 25;

		// Token: 0x04000539 RID: 1337
		internal const int And = 26;

		// Token: 0x0400053A RID: 1338
		internal const int Or = 27;

		// Token: 0x0400053B RID: 1339
		internal const int Proc = 28;

		// Token: 0x0400053C RID: 1340
		internal const int Iff = 29;

		// Token: 0x0400053D RID: 1341
		internal const int Qual = 30;

		// Token: 0x0400053E RID: 1342
		internal const int Dot = 31;

		// Token: 0x0400053F RID: 1343
		internal const int Null = 32;

		// Token: 0x04000540 RID: 1344
		internal const int True = 33;

		// Token: 0x04000541 RID: 1345
		internal const int False = 34;

		// Token: 0x04000542 RID: 1346
		internal const int Date = 35;

		// Token: 0x04000543 RID: 1347
		internal const int GenUniqueId = 36;

		// Token: 0x04000544 RID: 1348
		internal const int GenGUID = 37;

		// Token: 0x04000545 RID: 1349
		internal const int GUID = 38;

		// Token: 0x04000546 RID: 1350
		internal const int IsNot = 39;

		// Token: 0x04000547 RID: 1351
		internal const int priStart = 0;

		// Token: 0x04000548 RID: 1352
		internal const int priSubstr = 1;

		// Token: 0x04000549 RID: 1353
		internal const int priParen = 2;

		// Token: 0x0400054A RID: 1354
		internal const int priLow = 3;

		// Token: 0x0400054B RID: 1355
		internal const int priImp = 4;

		// Token: 0x0400054C RID: 1356
		internal const int priEqv = 5;

		// Token: 0x0400054D RID: 1357
		internal const int priXor = 6;

		// Token: 0x0400054E RID: 1358
		internal const int priOr = 7;

		// Token: 0x0400054F RID: 1359
		internal const int priAnd = 8;

		// Token: 0x04000550 RID: 1360
		internal const int priNot = 9;

		// Token: 0x04000551 RID: 1361
		internal const int priIs = 10;

		// Token: 0x04000552 RID: 1362
		internal const int priBetweenInLike = 11;

		// Token: 0x04000553 RID: 1363
		internal const int priBetweenAnd = 12;

		// Token: 0x04000554 RID: 1364
		internal const int priRelOp = 13;

		// Token: 0x04000555 RID: 1365
		internal const int priConcat = 14;

		// Token: 0x04000556 RID: 1366
		internal const int priContains = 15;

		// Token: 0x04000557 RID: 1367
		internal const int priPlusMinus = 16;

		// Token: 0x04000558 RID: 1368
		internal const int priMod = 17;

		// Token: 0x04000559 RID: 1369
		internal const int priIDiv = 18;

		// Token: 0x0400055A RID: 1370
		internal const int priMulDiv = 19;

		// Token: 0x0400055B RID: 1371
		internal const int priNeg = 20;

		// Token: 0x0400055C RID: 1372
		internal const int priExp = 21;

		// Token: 0x0400055D RID: 1373
		internal const int priProc = 22;

		// Token: 0x0400055E RID: 1374
		internal const int priDot = 23;

		// Token: 0x0400055F RID: 1375
		internal const int priMax = 24;

		// Token: 0x04000560 RID: 1376
		private static readonly int[] priority = new int[]
		{
			0,
			20,
			20,
			9,
			12,
			11,
			11,
			13,
			13,
			13,
			13,
			13,
			13,
			10,
			11,
			16,
			16,
			19,
			19,
			18,
			17,
			21,
			8,
			7,
			6,
			9,
			8,
			7,
			2,
			22,
			23,
			23,
			24,
			24,
			24,
			24,
			24,
			24,
			24,
			24,
			24,
			24,
			24,
			24
		};

		// Token: 0x04000561 RID: 1377
		private static readonly string[] Looks = new string[]
		{
			"",
			"-",
			"+",
			"Not",
			"BetweenAnd",
			"In",
			"Between",
			"=",
			">",
			"<",
			">=",
			"<=",
			"<>",
			"Is",
			"Like",
			"+",
			"-",
			"*",
			"/",
			"\\",
			"Mod",
			"**",
			"&",
			"|",
			"^",
			"~",
			"And",
			"Or",
			"Proc",
			"Iff",
			".",
			".",
			"Null",
			"True",
			"False",
			"Date",
			"GenUniqueId()",
			"GenGuid()",
			"Guid {..}",
			"Is Not"
		};
	}
}
