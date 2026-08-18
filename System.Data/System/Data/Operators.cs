using System;

namespace System.Data
{
	// Token: 0x020001B7 RID: 439
	internal sealed class Operators
	{
		// Token: 0x06001923 RID: 6435 RVA: 0x00258538 File Offset: 0x00257938
		private Operators()
		{
		}

		// Token: 0x06001924 RID: 6436 RVA: 0x00258558 File Offset: 0x00257958
		internal static bool IsArithmetical(int op)
		{
			return op == 15 || op == 16 || op == 17 || op == 18 || op == 20;
		}

		// Token: 0x06001925 RID: 6437 RVA: 0x00258588 File Offset: 0x00257988
		internal static bool IsLogical(int op)
		{
			return op == 26 || op == 27 || op == 3 || op == 13 || op == 39;
		}

		// Token: 0x06001926 RID: 6438 RVA: 0x002585B8 File Offset: 0x002579B8
		internal static bool IsRelational(int op)
		{
			return 7 <= op && op <= 12;
		}

		// Token: 0x06001927 RID: 6439 RVA: 0x002585D8 File Offset: 0x002579D8
		internal static int Priority(int op)
		{
			if (op > Operators.priority.Length)
			{
				return 24;
			}
			return Operators.priority[op];
		}

		// Token: 0x06001928 RID: 6440 RVA: 0x00258608 File Offset: 0x00257A08
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

		// Token: 0x04000DF2 RID: 3570
		internal const int Noop = 0;

		// Token: 0x04000DF3 RID: 3571
		internal const int Negative = 1;

		// Token: 0x04000DF4 RID: 3572
		internal const int UnaryPlus = 2;

		// Token: 0x04000DF5 RID: 3573
		internal const int Not = 3;

		// Token: 0x04000DF6 RID: 3574
		internal const int BetweenAnd = 4;

		// Token: 0x04000DF7 RID: 3575
		internal const int In = 5;

		// Token: 0x04000DF8 RID: 3576
		internal const int Between = 6;

		// Token: 0x04000DF9 RID: 3577
		internal const int EqualTo = 7;

		// Token: 0x04000DFA RID: 3578
		internal const int GreaterThen = 8;

		// Token: 0x04000DFB RID: 3579
		internal const int LessThen = 9;

		// Token: 0x04000DFC RID: 3580
		internal const int GreaterOrEqual = 10;

		// Token: 0x04000DFD RID: 3581
		internal const int LessOrEqual = 11;

		// Token: 0x04000DFE RID: 3582
		internal const int NotEqual = 12;

		// Token: 0x04000DFF RID: 3583
		internal const int Is = 13;

		// Token: 0x04000E00 RID: 3584
		internal const int Like = 14;

		// Token: 0x04000E01 RID: 3585
		internal const int Plus = 15;

		// Token: 0x04000E02 RID: 3586
		internal const int Minus = 16;

		// Token: 0x04000E03 RID: 3587
		internal const int Multiply = 17;

		// Token: 0x04000E04 RID: 3588
		internal const int Divide = 18;

		// Token: 0x04000E05 RID: 3589
		internal const int Modulo = 20;

		// Token: 0x04000E06 RID: 3590
		internal const int BitwiseAnd = 22;

		// Token: 0x04000E07 RID: 3591
		internal const int BitwiseOr = 23;

		// Token: 0x04000E08 RID: 3592
		internal const int BitwiseXor = 24;

		// Token: 0x04000E09 RID: 3593
		internal const int BitwiseNot = 25;

		// Token: 0x04000E0A RID: 3594
		internal const int And = 26;

		// Token: 0x04000E0B RID: 3595
		internal const int Or = 27;

		// Token: 0x04000E0C RID: 3596
		internal const int Proc = 28;

		// Token: 0x04000E0D RID: 3597
		internal const int Iff = 29;

		// Token: 0x04000E0E RID: 3598
		internal const int Qual = 30;

		// Token: 0x04000E0F RID: 3599
		internal const int Dot = 31;

		// Token: 0x04000E10 RID: 3600
		internal const int Null = 32;

		// Token: 0x04000E11 RID: 3601
		internal const int True = 33;

		// Token: 0x04000E12 RID: 3602
		internal const int False = 34;

		// Token: 0x04000E13 RID: 3603
		internal const int Date = 35;

		// Token: 0x04000E14 RID: 3604
		internal const int GenUniqueId = 36;

		// Token: 0x04000E15 RID: 3605
		internal const int GenGUID = 37;

		// Token: 0x04000E16 RID: 3606
		internal const int GUID = 38;

		// Token: 0x04000E17 RID: 3607
		internal const int IsNot = 39;

		// Token: 0x04000E18 RID: 3608
		internal const int priStart = 0;

		// Token: 0x04000E19 RID: 3609
		internal const int priSubstr = 1;

		// Token: 0x04000E1A RID: 3610
		internal const int priParen = 2;

		// Token: 0x04000E1B RID: 3611
		internal const int priLow = 3;

		// Token: 0x04000E1C RID: 3612
		internal const int priImp = 4;

		// Token: 0x04000E1D RID: 3613
		internal const int priEqv = 5;

		// Token: 0x04000E1E RID: 3614
		internal const int priXor = 6;

		// Token: 0x04000E1F RID: 3615
		internal const int priOr = 7;

		// Token: 0x04000E20 RID: 3616
		internal const int priAnd = 8;

		// Token: 0x04000E21 RID: 3617
		internal const int priNot = 9;

		// Token: 0x04000E22 RID: 3618
		internal const int priIs = 10;

		// Token: 0x04000E23 RID: 3619
		internal const int priBetweenInLike = 11;

		// Token: 0x04000E24 RID: 3620
		internal const int priBetweenAnd = 12;

		// Token: 0x04000E25 RID: 3621
		internal const int priRelOp = 13;

		// Token: 0x04000E26 RID: 3622
		internal const int priConcat = 14;

		// Token: 0x04000E27 RID: 3623
		internal const int priContains = 15;

		// Token: 0x04000E28 RID: 3624
		internal const int priPlusMinus = 16;

		// Token: 0x04000E29 RID: 3625
		internal const int priMod = 17;

		// Token: 0x04000E2A RID: 3626
		internal const int priIDiv = 18;

		// Token: 0x04000E2B RID: 3627
		internal const int priMulDiv = 19;

		// Token: 0x04000E2C RID: 3628
		internal const int priNeg = 20;

		// Token: 0x04000E2D RID: 3629
		internal const int priExp = 21;

		// Token: 0x04000E2E RID: 3630
		internal const int priProc = 22;

		// Token: 0x04000E2F RID: 3631
		internal const int priDot = 23;

		// Token: 0x04000E30 RID: 3632
		internal const int priMax = 24;

		// Token: 0x04000E31 RID: 3633
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

		// Token: 0x04000E32 RID: 3634
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
