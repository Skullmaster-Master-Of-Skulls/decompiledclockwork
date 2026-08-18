using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Web.UI.Barcode.PDF417ClassLibrary
{
	// Token: 0x0200009D RID: 157
	internal static class SpecificationData
	{
		// Token: 0x060005EF RID: 1519 RVA: 0x0000FC6C File Offset: 0x0000DE6C
		static SpecificationData()
		{
			SpecificationData.InitializeBarSpaceSequence();
			SpecificationData.ErrorCorrectionLevels = new List<List<int>>();
			SpecificationData.InitializeLevelZeroErrorCorrection();
			SpecificationData.InitializeLevelOneErrorCorrection();
			SpecificationData.InitializeLevelTwoErrorCorrection();
			SpecificationData.InitializeLevelThreeErrorCorrection();
			SpecificationData.InitializeLevelFourErrorCorrection();
			SpecificationData.InitializeLevelFiveErrorCorrection();
			SpecificationData.InitializeLevelSixErrorCorrection();
			SpecificationData.InitializeLevelSevenErrorCorrection();
			SpecificationData.InitializeLevelEightErrorCorrection();
			SpecificationData.InitializeTextSubmodes();
			SpecificationData.InitializeByteModeValues();
			SpecificationData.InitializeStartStopSequence();
			SpecificationData.InitializeECNumberPerLevel();
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x060005F0 RID: 1520 RVA: 0x0000FCC9 File Offset: 0x0000DEC9
		// (set) Token: 0x060005F1 RID: 1521 RVA: 0x0000FCD0 File Offset: 0x0000DED0
		internal static List<List<int>> BarSpaceSequence { get; set; }

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x060005F2 RID: 1522 RVA: 0x0000FCD8 File Offset: 0x0000DED8
		// (set) Token: 0x060005F3 RID: 1523 RVA: 0x0000FCDF File Offset: 0x0000DEDF
		internal static List<List<int>> ErrorCorrectionLevels { get; set; }

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x060005F4 RID: 1524 RVA: 0x0000FCE7 File Offset: 0x0000DEE7
		// (set) Token: 0x060005F5 RID: 1525 RVA: 0x0000FCEE File Offset: 0x0000DEEE
		internal static List<TextModeDefinitionEntry> TextSubmodes { get; set; }

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x060005F6 RID: 1526 RVA: 0x0000FCF6 File Offset: 0x0000DEF6
		// (set) Token: 0x060005F7 RID: 1527 RVA: 0x0000FCFD File Offset: 0x0000DEFD
		internal static List<int> ByteModeValues { get; set; }

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x060005F8 RID: 1528 RVA: 0x0000FD05 File Offset: 0x0000DF05
		// (set) Token: 0x060005F9 RID: 1529 RVA: 0x0000FD0C File Offset: 0x0000DF0C
		internal static List<int> ECNumberPerLevel { get; set; }

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x060005FA RID: 1530 RVA: 0x0000FD14 File Offset: 0x0000DF14
		// (set) Token: 0x060005FB RID: 1531 RVA: 0x0000FD1B File Offset: 0x0000DF1B
		internal static List<Cluster> Start { get; set; }

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x060005FC RID: 1532 RVA: 0x0000FD23 File Offset: 0x0000DF23
		// (set) Token: 0x060005FD RID: 1533 RVA: 0x0000FD2A File Offset: 0x0000DF2A
		internal static List<Cluster> Stop { get; set; }

		// Token: 0x060005FE RID: 1534 RVA: 0x0000FD34 File Offset: 0x0000DF34
		private static void InitializeStartStopSequence()
		{
			SpecificationData.Start = new List<Cluster>();
			SpecificationData.Stop = new List<Cluster>();
			SpecificationData.Start.Add(new Cluster(0, true, 8));
			SpecificationData.Start.Add(new Cluster(1, false, 1));
			SpecificationData.Start.Add(new Cluster(2, true, 1));
			SpecificationData.Start.Add(new Cluster(3, false, 1));
			SpecificationData.Start.Add(new Cluster(4, true, 1));
			SpecificationData.Start.Add(new Cluster(5, false, 1));
			SpecificationData.Start.Add(new Cluster(6, true, 1));
			SpecificationData.Start.Add(new Cluster(7, false, 3));
			SpecificationData.Stop.Add(new Cluster(0, true, 7));
			SpecificationData.Stop.Add(new Cluster(1, false, 1));
			SpecificationData.Stop.Add(new Cluster(2, true, 1));
			SpecificationData.Stop.Add(new Cluster(3, false, 3));
			SpecificationData.Stop.Add(new Cluster(4, true, 1));
			SpecificationData.Stop.Add(new Cluster(5, false, 1));
			SpecificationData.Stop.Add(new Cluster(6, true, 1));
			SpecificationData.Stop.Add(new Cluster(7, false, 2));
			SpecificationData.Stop.Add(new Cluster(8, true, 1));
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x0000FE88 File Offset: 0x0000E088
		private static void InitializeECNumberPerLevel()
		{
			SpecificationData.ECNumberPerLevel = new List<int>();
			for (int i = 0; i < 9; i++)
			{
				if (i == 0)
				{
					SpecificationData.ECNumberPerLevel.Add(2);
				}
				else
				{
					SpecificationData.ECNumberPerLevel.Add(SpecificationData.ECNumberPerLevel[i - 1] * 2);
				}
			}
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x0000FED8 File Offset: 0x0000E0D8
		private static void InitializeByteModeValues()
		{
			SpecificationData.ByteModeValues = new List<int>();
			for (int i = 0; i < 255; i++)
			{
				SpecificationData.ByteModeValues.Add(i);
			}
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x0000FF0C File Offset: 0x0000E10C
		private static void InitializeLevelZeroErrorCorrection()
		{
			List<int> item = new List<int>
			{
				27,
				917
			};
			SpecificationData.ErrorCorrectionLevels.Add(item);
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x0000FF40 File Offset: 0x0000E140
		private static void InitializeTextSubmodes()
		{
			SpecificationData.TextSubmodes = new List<TextModeDefinitionEntry>();
			SpecificationData.TextSubmodesAddRow(new List<int>
			{
				65,
				97,
				48,
				59
			}, 0);
			SpecificationData.TextSubmodesAddRow(new List<int>
			{
				66,
				98,
				49,
				60
			}, 1);
			SpecificationData.TextSubmodesAddRow(new List<int>
			{
				67,
				99,
				50,
				62
			}, 2);
			SpecificationData.TextSubmodesAddRow(new List<int>
			{
				68,
				100,
				51,
				64
			}, 3);
			SpecificationData.TextSubmodesAddRow(new List<int>
			{
				69,
				101,
				52,
				91
			}, 4);
			SpecificationData.TextSubmodesAddRow(new List<int>
			{
				70,
				102,
				53,
				92
			}, 5);
			SpecificationData.TextSubmodesAddRow(new List<int>
			{
				71,
				103,
				54,
				93
			}, 6);
			SpecificationData.TextSubmodesAddRow(new List<int>
			{
				72,
				104,
				55,
				95
			}, 7);
			SpecificationData.TextSubmodesAddRow(new List<int>
			{
				73,
				105,
				56,
				96
			}, 8);
			SpecificationData.TextSubmodesAddRow(new List<int>
			{
				74,
				106,
				57,
				126
			}, 9);
			SpecificationData.TextSubmodesAddRow(new List<int>
			{
				75,
				107,
				38,
				33
			}, 10);
			SpecificationData.TextSubmodesAddRow(new List<int>
			{
				76,
				108,
				13,
				13
			}, 11);
			SpecificationData.TextSubmodesAddRow(new List<int>
			{
				77,
				109,
				9,
				9
			}, 12);
			SpecificationData.TextSubmodesAddRow(new List<int>
			{
				78,
				110,
				44,
				44
			}, 13);
			SpecificationData.TextSubmodesAddRow(new List<int>
			{
				79,
				111,
				58,
				58
			}, 14);
			SpecificationData.TextSubmodesAddRow(new List<int>
			{
				80,
				112,
				35,
				10
			}, 15);
			SpecificationData.TextSubmodesAddRow(new List<int>
			{
				81,
				113,
				45,
				45
			}, 16);
			SpecificationData.TextSubmodesAddRow(new List<int>
			{
				82,
				114,
				46,
				46
			}, 17);
			SpecificationData.TextSubmodesAddRow(new List<int>
			{
				83,
				115,
				36,
				36
			}, 18);
			SpecificationData.TextSubmodesAddRow(new List<int>
			{
				84,
				116,
				47,
				47
			}, 19);
			SpecificationData.TextSubmodesAddRow(new List<int>
			{
				85,
				117,
				43,
				34
			}, 20);
			SpecificationData.TextSubmodesAddRow(new List<int>
			{
				86,
				118,
				37,
				124
			}, 21);
			SpecificationData.TextSubmodesAddRow(new List<int>
			{
				87,
				119,
				42,
				42
			}, 22);
			SpecificationData.TextSubmodesAddRow(new List<int>
			{
				88,
				120,
				61,
				40
			}, 23);
			SpecificationData.TextSubmodesAddRow(new List<int>
			{
				89,
				121,
				94,
				41
			}, 24);
			SpecificationData.TextSubmodesAddRow(new List<int>
			{
				90,
				122,
				1004,
				63
			}, 25);
			SpecificationData.TextSubmodesAddRow(new List<int>
			{
				32,
				32,
				32,
				123
			}, 26);
			SpecificationData.TextSubmodesAddRow(new List<int>
			{
				1005,
				1003,
				1005,
				125
			}, 27);
			SpecificationData.TextSubmodesAddRow(new List<int>
			{
				1002,
				1002,
				1001,
				39
			}, 28);
			SpecificationData.TextSubmodesAddRow(new List<int>
			{
				1006,
				1006,
				1004,
				1001
			}, 29);
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x00010570 File Offset: 0x0000E770
		private static void TextSubmodesAddRow(List<int> rowValues, int rowIndex)
		{
			int num = 0;
			foreach (int asciiValue in rowValues)
			{
				SpecificationData.TextSubmodes.Add(new TextModeDefinitionEntry(asciiValue, num, rowIndex));
				num++;
			}
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x000105D0 File Offset: 0x0000E7D0
		private static void InitializeLevelOneErrorCorrection()
		{
			List<int> csvalues = BarcodeResources.GetCSValues("LevelOneEC.txt");
			SpecificationData.ErrorCorrectionLevels.Add(csvalues);
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x000105F4 File Offset: 0x0000E7F4
		private static void InitializeLevelTwoErrorCorrection()
		{
			List<int> csvalues = BarcodeResources.GetCSValues("LevelTwoEC.txt");
			SpecificationData.ErrorCorrectionLevels.Add(csvalues);
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x00010618 File Offset: 0x0000E818
		private static void InitializeLevelThreeErrorCorrection()
		{
			List<int> csvalues = BarcodeResources.GetCSValues("LevelThreeEC.txt");
			SpecificationData.ErrorCorrectionLevels.Add(csvalues);
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x0001063C File Offset: 0x0000E83C
		private static void InitializeLevelFourErrorCorrection()
		{
			List<int> csvalues = BarcodeResources.GetCSValues("LevelFourEC.txt");
			SpecificationData.ErrorCorrectionLevels.Add(csvalues);
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x00010660 File Offset: 0x0000E860
		private static void InitializeLevelFiveErrorCorrection()
		{
			List<int> csvalues = BarcodeResources.GetCSValues("LevelFiveEC.txt");
			SpecificationData.ErrorCorrectionLevels.Add(csvalues);
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x00010684 File Offset: 0x0000E884
		private static void InitializeLevelSixErrorCorrection()
		{
			List<int> csvalues = BarcodeResources.GetCSValues("LevelSixEC.txt");
			SpecificationData.ErrorCorrectionLevels.Add(csvalues);
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x000106A8 File Offset: 0x0000E8A8
		private static void InitializeLevelSevenErrorCorrection()
		{
			List<int> csvalues = BarcodeResources.GetCSValues("LevelSevenEC.txt");
			SpecificationData.ErrorCorrectionLevels.Add(csvalues);
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x000106CC File Offset: 0x0000E8CC
		private static void InitializeLevelEightErrorCorrection()
		{
			List<int> csvalues = BarcodeResources.GetCSValues("LevelEightEC.txt");
			SpecificationData.ErrorCorrectionLevels.Add(csvalues);
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x000106EF File Offset: 0x0000E8EF
		[SuppressMessage("Microsoft.Performance", "CA1809:AvoidExcessiveLocals")]
		private static void InitializeBarSpaceSequence()
		{
			SpecificationData.BarSpaceSequence = new List<List<int>>();
			SpecificationData.BarSpaceSequence = BarcodeResources.GetBarSpaceSequence("BarSpaceSequence.txt", 8);
		}

		// Token: 0x040000DD RID: 221
		internal const int AlphaLatch = 1001;

		// Token: 0x040000DE RID: 222
		internal const int MixedLatch = 1002;

		// Token: 0x040000DF RID: 223
		internal const int AlphaShift = 1003;

		// Token: 0x040000E0 RID: 224
		internal const int PunctuationLatch = 1004;

		// Token: 0x040000E1 RID: 225
		internal const int LowerLatch = 1005;

		// Token: 0x040000E2 RID: 226
		internal const int PunctuationShift = 1006;

		// Token: 0x040000E3 RID: 227
		internal const int MaxCodeWords = 928;

		// Token: 0x040000E4 RID: 228
		internal const int MaxDataCodeWords = 925;

		// Token: 0x040000E5 RID: 229
		internal const int RowIndicatorsCount = 2;

		// Token: 0x040000E6 RID: 230
		internal const int AlphaIndexPosition = 0;

		// Token: 0x040000E7 RID: 231
		internal const int LowerIndexPosition = 1;

		// Token: 0x040000E8 RID: 232
		internal const int MixedIndexPosition = 2;

		// Token: 0x040000E9 RID: 233
		internal const int PunctuationIndexPosition = 3;

		// Token: 0x040000EA RID: 234
		internal const int StartLength = 17;

		// Token: 0x040000EB RID: 235
		internal const int StopLength = 18;

		// Token: 0x040000EC RID: 236
		internal const int ClusterLength = 17;

		// Token: 0x040000ED RID: 237
		internal const int QuietZoneLength = 2;

		// Token: 0x040000EE RID: 238
		internal const int NumericListLength = 10;

		// Token: 0x040000EF RID: 239
		internal const int TextCompactionModeLatch = 900;

		// Token: 0x040000F0 RID: 240
		internal const int FirstByteCompactionModeLatch = 901;

		// Token: 0x040000F1 RID: 241
		internal const int ByteCompactionModeShift = 913;

		// Token: 0x040000F2 RID: 242
		internal const int NumericComactionModeLatch = 902;

		// Token: 0x040000F3 RID: 243
		internal const int SecondByteCompactionModeLatch = 924;

		// Token: 0x040000F4 RID: 244
		internal const int BeginMacroPDF417ControlBlock = 928;

		// Token: 0x040000F5 RID: 245
		internal const int BeginMacroPDF417OptionalField = 923;

		// Token: 0x040000F6 RID: 246
		internal const int MacroPDF417Terminator = 922;

		// Token: 0x040000F7 RID: 247
		internal const int ReaderInitialisation = 921;

		// Token: 0x040000F8 RID: 248
		internal const int LengthIndicatorCount = 1;

		// Token: 0x040000F9 RID: 249
		internal const int MaxTotalDataCount = 928;

		// Token: 0x040000FA RID: 250
		internal const int MaxDataOnlyCount = 925;

		// Token: 0x040000FB RID: 251
		internal const int MaxColumns = 30;

		// Token: 0x040000FC RID: 252
		internal const int MaxRows = 90;
	}
}
