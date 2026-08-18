using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace Telerik.Web.UI.Barcode
{
	// Token: 0x020000A2 RID: 162
	internal class QRCode
	{
		// Token: 0x06000631 RID: 1585 RVA: 0x00010F00 File Offset: 0x0000F100
		static QRCode()
		{
			QRCode.PopulatePositionValues();
			QRCode.PopulateDataCapacityTable();
			QRCode.PopulateCodeModeValues();
			QRCode.PopulateCodeWordsLengthTable();
			QRCode.PopulateExponentsOfAlphaToValues();
			QRCode.PopulateValuesOfExponentsOfAlpha();
			QRCode.PopulateGeneratorExponentsOfAlpha();
			QRCode.PopulatePositionAdjustmentTable();
			QRCode.PopulateErrorCorrectionToMask();
			QRCode.PopulateFormatInformation();
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x000124C8 File Offset: 0x000106C8
		private static void PopulatePositionValues()
		{
			QRCode.positionValues.Add(7, QRCode.version7);
			QRCode.positionValues.Add(8, QRCode.version8);
			QRCode.positionValues.Add(9, QRCode.version9);
			QRCode.positionValues.Add(10, QRCode.version10);
			QRCode.positionValues.Add(11, QRCode.version11);
			QRCode.positionValues.Add(12, QRCode.version12);
			QRCode.positionValues.Add(13, QRCode.version13);
			QRCode.positionValues.Add(14, QRCode.version14);
			QRCode.positionValues.Add(15, QRCode.version15);
			QRCode.positionValues.Add(16, QRCode.version16);
			QRCode.positionValues.Add(17, QRCode.version17);
			QRCode.positionValues.Add(18, QRCode.version18);
			QRCode.positionValues.Add(19, QRCode.version19);
			QRCode.positionValues.Add(20, QRCode.version20);
			QRCode.positionValues.Add(21, QRCode.version21);
			QRCode.positionValues.Add(22, QRCode.version22);
			QRCode.positionValues.Add(23, QRCode.version23);
			QRCode.positionValues.Add(24, QRCode.version24);
			QRCode.positionValues.Add(25, QRCode.version25);
			QRCode.positionValues.Add(26, QRCode.version26);
			QRCode.positionValues.Add(27, QRCode.version27);
			QRCode.positionValues.Add(28, QRCode.version28);
			QRCode.positionValues.Add(29, QRCode.version29);
			QRCode.positionValues.Add(30, QRCode.version30);
			QRCode.positionValues.Add(31, QRCode.version31);
			QRCode.positionValues.Add(32, QRCode.version32);
			QRCode.positionValues.Add(33, QRCode.version33);
			QRCode.positionValues.Add(34, QRCode.version34);
			QRCode.positionValues.Add(35, QRCode.version35);
			QRCode.positionValues.Add(36, QRCode.version36);
			QRCode.positionValues.Add(37, QRCode.version37);
			QRCode.positionValues.Add(38, QRCode.version38);
			QRCode.positionValues.Add(39, QRCode.version39);
			QRCode.positionValues.Add(40, QRCode.version40);
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x00012718 File Offset: 0x00010918
		private static void PopulateDataCapacityTable()
		{
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(1, Modes.ErrorCorrectionLevel.L), 152);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(1, Modes.ErrorCorrectionLevel.M), 128);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(1, Modes.ErrorCorrectionLevel.Q), 104);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(1, Modes.ErrorCorrectionLevel.H), 72);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(2, Modes.ErrorCorrectionLevel.L), 272);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(2, Modes.ErrorCorrectionLevel.M), 224);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(2, Modes.ErrorCorrectionLevel.Q), 176);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(2, Modes.ErrorCorrectionLevel.H), 128);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(3, Modes.ErrorCorrectionLevel.L), 440);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(3, Modes.ErrorCorrectionLevel.M), 352);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(3, Modes.ErrorCorrectionLevel.Q), 272);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(3, Modes.ErrorCorrectionLevel.H), 208);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(4, Modes.ErrorCorrectionLevel.L), 640);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(4, Modes.ErrorCorrectionLevel.M), 512);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(4, Modes.ErrorCorrectionLevel.Q), 384);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(4, Modes.ErrorCorrectionLevel.H), 288);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(5, Modes.ErrorCorrectionLevel.L), 864);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(5, Modes.ErrorCorrectionLevel.M), 688);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(5, Modes.ErrorCorrectionLevel.Q), 496);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(5, Modes.ErrorCorrectionLevel.H), 368);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(6, Modes.ErrorCorrectionLevel.L), 1088);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(6, Modes.ErrorCorrectionLevel.M), 864);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(6, Modes.ErrorCorrectionLevel.Q), 608);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(6, Modes.ErrorCorrectionLevel.H), 480);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(7, Modes.ErrorCorrectionLevel.L), 1248);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(7, Modes.ErrorCorrectionLevel.M), 992);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(7, Modes.ErrorCorrectionLevel.Q), 704);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(7, Modes.ErrorCorrectionLevel.H), 528);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(8, Modes.ErrorCorrectionLevel.L), 1552);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(8, Modes.ErrorCorrectionLevel.M), 1232);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(8, Modes.ErrorCorrectionLevel.Q), 880);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(8, Modes.ErrorCorrectionLevel.H), 688);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(9, Modes.ErrorCorrectionLevel.L), 1856);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(9, Modes.ErrorCorrectionLevel.M), 1456);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(9, Modes.ErrorCorrectionLevel.Q), 1056);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(9, Modes.ErrorCorrectionLevel.H), 800);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(10, Modes.ErrorCorrectionLevel.L), 2192);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(10, Modes.ErrorCorrectionLevel.M), 1728);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(10, Modes.ErrorCorrectionLevel.Q), 1232);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(10, Modes.ErrorCorrectionLevel.H), 976);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(11, Modes.ErrorCorrectionLevel.L), 2592);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(11, Modes.ErrorCorrectionLevel.M), 2032);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(11, Modes.ErrorCorrectionLevel.Q), 1440);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(11, Modes.ErrorCorrectionLevel.H), 1120);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(12, Modes.ErrorCorrectionLevel.L), 2960);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(12, Modes.ErrorCorrectionLevel.M), 2320);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(12, Modes.ErrorCorrectionLevel.Q), 1648);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(12, Modes.ErrorCorrectionLevel.H), 1264);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(13, Modes.ErrorCorrectionLevel.L), 3424);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(13, Modes.ErrorCorrectionLevel.M), 2672);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(13, Modes.ErrorCorrectionLevel.Q), 1952);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(13, Modes.ErrorCorrectionLevel.H), 1440);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(14, Modes.ErrorCorrectionLevel.L), 3688);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(14, Modes.ErrorCorrectionLevel.M), 2920);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(14, Modes.ErrorCorrectionLevel.Q), 2088);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(14, Modes.ErrorCorrectionLevel.H), 1576);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(15, Modes.ErrorCorrectionLevel.L), 4184);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(15, Modes.ErrorCorrectionLevel.M), 3320);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(15, Modes.ErrorCorrectionLevel.Q), 2360);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(15, Modes.ErrorCorrectionLevel.H), 1784);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(16, Modes.ErrorCorrectionLevel.L), 4712);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(16, Modes.ErrorCorrectionLevel.M), 3624);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(16, Modes.ErrorCorrectionLevel.Q), 2600);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(16, Modes.ErrorCorrectionLevel.H), 2024);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(17, Modes.ErrorCorrectionLevel.L), 5176);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(17, Modes.ErrorCorrectionLevel.M), 4056);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(17, Modes.ErrorCorrectionLevel.Q), 2936);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(17, Modes.ErrorCorrectionLevel.H), 2264);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(18, Modes.ErrorCorrectionLevel.L), 5768);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(18, Modes.ErrorCorrectionLevel.M), 4504);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(18, Modes.ErrorCorrectionLevel.Q), 3176);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(18, Modes.ErrorCorrectionLevel.H), 2504);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(19, Modes.ErrorCorrectionLevel.L), 6360);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(19, Modes.ErrorCorrectionLevel.M), 5016);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(19, Modes.ErrorCorrectionLevel.Q), 3560);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(19, Modes.ErrorCorrectionLevel.H), 2728);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(20, Modes.ErrorCorrectionLevel.L), 6888);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(20, Modes.ErrorCorrectionLevel.M), 5352);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(20, Modes.ErrorCorrectionLevel.Q), 3880);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(20, Modes.ErrorCorrectionLevel.H), 3080);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(21, Modes.ErrorCorrectionLevel.L), 7456);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(21, Modes.ErrorCorrectionLevel.M), 5712);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(21, Modes.ErrorCorrectionLevel.Q), 4096);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(21, Modes.ErrorCorrectionLevel.H), 3248);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(22, Modes.ErrorCorrectionLevel.L), 8048);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(22, Modes.ErrorCorrectionLevel.M), 6256);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(22, Modes.ErrorCorrectionLevel.Q), 4544);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(22, Modes.ErrorCorrectionLevel.H), 3536);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(23, Modes.ErrorCorrectionLevel.L), 8752);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(23, Modes.ErrorCorrectionLevel.M), 6880);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(23, Modes.ErrorCorrectionLevel.Q), 4912);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(23, Modes.ErrorCorrectionLevel.H), 3712);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(24, Modes.ErrorCorrectionLevel.L), 9392);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(24, Modes.ErrorCorrectionLevel.M), 7312);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(24, Modes.ErrorCorrectionLevel.Q), 5312);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(24, Modes.ErrorCorrectionLevel.H), 4112);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(25, Modes.ErrorCorrectionLevel.L), 10208);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(25, Modes.ErrorCorrectionLevel.M), 8000);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(25, Modes.ErrorCorrectionLevel.Q), 5744);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(25, Modes.ErrorCorrectionLevel.H), 4304);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(26, Modes.ErrorCorrectionLevel.L), 10960);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(26, Modes.ErrorCorrectionLevel.M), 8496);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(26, Modes.ErrorCorrectionLevel.Q), 6032);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(26, Modes.ErrorCorrectionLevel.H), 4768);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(27, Modes.ErrorCorrectionLevel.L), 11744);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(27, Modes.ErrorCorrectionLevel.M), 9024);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(27, Modes.ErrorCorrectionLevel.Q), 6464);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(27, Modes.ErrorCorrectionLevel.H), 5024);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(28, Modes.ErrorCorrectionLevel.L), 12248);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(28, Modes.ErrorCorrectionLevel.M), 9544);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(28, Modes.ErrorCorrectionLevel.Q), 6968);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(28, Modes.ErrorCorrectionLevel.H), 5288);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(29, Modes.ErrorCorrectionLevel.L), 13048);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(29, Modes.ErrorCorrectionLevel.M), 10136);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(29, Modes.ErrorCorrectionLevel.Q), 7288);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(29, Modes.ErrorCorrectionLevel.H), 5608);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(30, Modes.ErrorCorrectionLevel.L), 13880);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(30, Modes.ErrorCorrectionLevel.M), 10984);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(30, Modes.ErrorCorrectionLevel.Q), 7880);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(30, Modes.ErrorCorrectionLevel.H), 5960);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(31, Modes.ErrorCorrectionLevel.L), 14744);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(31, Modes.ErrorCorrectionLevel.M), 11640);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(31, Modes.ErrorCorrectionLevel.Q), 8264);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(31, Modes.ErrorCorrectionLevel.H), 6344);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(32, Modes.ErrorCorrectionLevel.L), 15640);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(32, Modes.ErrorCorrectionLevel.M), 12328);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(32, Modes.ErrorCorrectionLevel.Q), 8920);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(32, Modes.ErrorCorrectionLevel.H), 6760);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(33, Modes.ErrorCorrectionLevel.L), 16568);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(33, Modes.ErrorCorrectionLevel.M), 13048);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(33, Modes.ErrorCorrectionLevel.Q), 9368);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(33, Modes.ErrorCorrectionLevel.H), 7208);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(34, Modes.ErrorCorrectionLevel.L), 17528);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(34, Modes.ErrorCorrectionLevel.M), 13800);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(34, Modes.ErrorCorrectionLevel.Q), 9848);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(34, Modes.ErrorCorrectionLevel.H), 7688);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(35, Modes.ErrorCorrectionLevel.L), 18448);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(35, Modes.ErrorCorrectionLevel.M), 14496);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(35, Modes.ErrorCorrectionLevel.Q), 10288);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(35, Modes.ErrorCorrectionLevel.H), 7888);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(36, Modes.ErrorCorrectionLevel.L), 19472);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(36, Modes.ErrorCorrectionLevel.M), 15312);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(36, Modes.ErrorCorrectionLevel.Q), 10832);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(36, Modes.ErrorCorrectionLevel.H), 8432);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(37, Modes.ErrorCorrectionLevel.L), 20528);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(37, Modes.ErrorCorrectionLevel.M), 15936);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(37, Modes.ErrorCorrectionLevel.Q), 11408);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(37, Modes.ErrorCorrectionLevel.H), 8768);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(38, Modes.ErrorCorrectionLevel.L), 21616);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(38, Modes.ErrorCorrectionLevel.M), 16816);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(38, Modes.ErrorCorrectionLevel.Q), 12016);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(38, Modes.ErrorCorrectionLevel.H), 9136);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(39, Modes.ErrorCorrectionLevel.L), 22496);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(39, Modes.ErrorCorrectionLevel.M), 17728);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(39, Modes.ErrorCorrectionLevel.Q), 12656);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(39, Modes.ErrorCorrectionLevel.H), 9776);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(40, Modes.ErrorCorrectionLevel.L), 23648);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(40, Modes.ErrorCorrectionLevel.M), 18672);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(40, Modes.ErrorCorrectionLevel.Q), 13328);
			QRCode.dataCapacityTable.Add(new DataCapacityIndexer(40, Modes.ErrorCorrectionLevel.H), 10208);
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x00013560 File Offset: 0x00011760
		private static void PopulateCodeModeValues()
		{
			QRCode.codeModeValues.Add(Modes.CodeMode.Numeric, "0001");
			QRCode.codeModeValues.Add(Modes.CodeMode.Alphanumeric, "0010");
			QRCode.codeModeValues.Add(Modes.CodeMode.Byte, "0100");
			QRCode.codeModeValues.Add(Modes.CodeMode.Kanji, "1000");
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x000135B0 File Offset: 0x000117B0
		private static void PopulateCodeWordsLengthTable()
		{
			List<string> list = new List<string>
			{
				"L",
				"M",
				"Q",
				"H"
			};
			List<int> list2 = new List<int>
			{
				7,
				1,
				19,
				0,
				0,
				10,
				1,
				16,
				0,
				0,
				13,
				1,
				13,
				0,
				0,
				17,
				1,
				9,
				0,
				0,
				10,
				1,
				34,
				0,
				0,
				16,
				1,
				28,
				0,
				0,
				22,
				1,
				22,
				0,
				0,
				28,
				1,
				16,
				0,
				0,
				15,
				1,
				55,
				0,
				0,
				26,
				1,
				44,
				0,
				0,
				18,
				2,
				17,
				0,
				0,
				22,
				2,
				13,
				0,
				0,
				20,
				1,
				80,
				0,
				0,
				18,
				2,
				32,
				0,
				0,
				26,
				2,
				24,
				0,
				0,
				16,
				4,
				9,
				0,
				0,
				26,
				1,
				108,
				0,
				0,
				24,
				2,
				43,
				0,
				0,
				18,
				2,
				15,
				2,
				16,
				22,
				2,
				11,
				2,
				12,
				18,
				2,
				68,
				0,
				0,
				16,
				4,
				27,
				0,
				0,
				24,
				4,
				19,
				0,
				0,
				28,
				4,
				15,
				0,
				0,
				20,
				2,
				78,
				0,
				0,
				18,
				4,
				31,
				0,
				0,
				18,
				2,
				14,
				4,
				15,
				26,
				4,
				13,
				1,
				14,
				24,
				2,
				97,
				0,
				0,
				22,
				2,
				38,
				2,
				39,
				22,
				4,
				18,
				2,
				19,
				26,
				4,
				14,
				2,
				15,
				30,
				2,
				116,
				0,
				0,
				22,
				3,
				36,
				2,
				37,
				20,
				4,
				16,
				4,
				17,
				24,
				4,
				12,
				4,
				13,
				18,
				2,
				68,
				2,
				69,
				26,
				4,
				43,
				1,
				44,
				24,
				6,
				19,
				2,
				20,
				28,
				6,
				15,
				2,
				16,
				20,
				4,
				81,
				0,
				0,
				30,
				1,
				50,
				4,
				51,
				28,
				4,
				22,
				4,
				23,
				24,
				3,
				12,
				8,
				13,
				24,
				2,
				92,
				2,
				93,
				22,
				6,
				36,
				2,
				37,
				26,
				4,
				20,
				6,
				21,
				28,
				7,
				14,
				4,
				15,
				26,
				4,
				107,
				0,
				0,
				22,
				8,
				37,
				1,
				38,
				24,
				8,
				20,
				4,
				21,
				22,
				12,
				11,
				4,
				12,
				30,
				3,
				115,
				1,
				116,
				24,
				4,
				40,
				5,
				41,
				20,
				11,
				16,
				5,
				17,
				24,
				11,
				12,
				5,
				13,
				22,
				5,
				87,
				1,
				88,
				24,
				5,
				41,
				5,
				42,
				30,
				5,
				24,
				7,
				25,
				24,
				11,
				12,
				7,
				13,
				24,
				5,
				98,
				1,
				99,
				28,
				7,
				45,
				3,
				46,
				24,
				15,
				19,
				2,
				20,
				30,
				3,
				15,
				13,
				16,
				28,
				1,
				107,
				5,
				108,
				28,
				10,
				46,
				1,
				47,
				28,
				1,
				22,
				15,
				23,
				28,
				2,
				14,
				17,
				15,
				30,
				5,
				120,
				1,
				121,
				26,
				9,
				43,
				4,
				44,
				28,
				17,
				22,
				1,
				23,
				28,
				2,
				14,
				19,
				15,
				28,
				3,
				113,
				4,
				114,
				26,
				3,
				44,
				11,
				45,
				26,
				17,
				21,
				4,
				22,
				26,
				9,
				13,
				16,
				14,
				28,
				3,
				107,
				5,
				108,
				26,
				3,
				41,
				13,
				42,
				30,
				15,
				24,
				5,
				25,
				28,
				15,
				15,
				10,
				16,
				28,
				4,
				116,
				4,
				117,
				26,
				17,
				42,
				0,
				0,
				28,
				17,
				22,
				6,
				23,
				30,
				19,
				16,
				6,
				17,
				28,
				2,
				111,
				7,
				112,
				28,
				17,
				46,
				0,
				0,
				30,
				7,
				24,
				16,
				25,
				24,
				34,
				13,
				0,
				0,
				30,
				4,
				121,
				5,
				122,
				28,
				4,
				47,
				14,
				48,
				30,
				11,
				24,
				14,
				25,
				30,
				16,
				15,
				14,
				16,
				30,
				6,
				117,
				4,
				118,
				28,
				6,
				45,
				14,
				46,
				30,
				11,
				24,
				16,
				25,
				30,
				30,
				16,
				2,
				17,
				26,
				8,
				106,
				4,
				107,
				28,
				8,
				47,
				13,
				48,
				30,
				7,
				24,
				22,
				25,
				30,
				22,
				15,
				13,
				16,
				28,
				10,
				114,
				2,
				115,
				28,
				19,
				46,
				4,
				47,
				28,
				28,
				22,
				6,
				23,
				30,
				33,
				16,
				4,
				17,
				30,
				8,
				122,
				4,
				123,
				28,
				22,
				45,
				3,
				46,
				30,
				8,
				23,
				26,
				24,
				30,
				12,
				15,
				28,
				16,
				30,
				3,
				117,
				10,
				118,
				28,
				3,
				45,
				23,
				46,
				30,
				4,
				24,
				31,
				25,
				30,
				11,
				15,
				31,
				16,
				30,
				7,
				116,
				7,
				117,
				28,
				21,
				45,
				7,
				46,
				30,
				1,
				23,
				37,
				24,
				30,
				19,
				15,
				26,
				16,
				30,
				5,
				115,
				10,
				116,
				28,
				19,
				47,
				10,
				48,
				30,
				15,
				24,
				25,
				25,
				30,
				23,
				15,
				25,
				16,
				30,
				13,
				115,
				3,
				116,
				28,
				2,
				46,
				29,
				47,
				30,
				42,
				24,
				1,
				25,
				30,
				23,
				15,
				28,
				16,
				30,
				17,
				115,
				0,
				0,
				28,
				10,
				46,
				23,
				47,
				30,
				10,
				24,
				35,
				25,
				30,
				19,
				15,
				35,
				16,
				30,
				17,
				115,
				1,
				116,
				28,
				14,
				46,
				21,
				47,
				30,
				29,
				24,
				19,
				25,
				30,
				11,
				15,
				46,
				16,
				30,
				13,
				115,
				6,
				116,
				28,
				14,
				46,
				23,
				47,
				30,
				44,
				24,
				7,
				25,
				30,
				59,
				16,
				1,
				17,
				30,
				12,
				121,
				7,
				122,
				28,
				12,
				47,
				26,
				48,
				30,
				39,
				24,
				14,
				25,
				30,
				22,
				15,
				41,
				16,
				30,
				6,
				121,
				14,
				122,
				28,
				6,
				47,
				34,
				48,
				30,
				46,
				24,
				10,
				25,
				30,
				2,
				15,
				64,
				16,
				30,
				17,
				122,
				4,
				123,
				28,
				29,
				46,
				14,
				47,
				30,
				49,
				24,
				10,
				25,
				30,
				24,
				15,
				46,
				16,
				30,
				4,
				122,
				18,
				123,
				28,
				13,
				46,
				32,
				47,
				30,
				48,
				24,
				14,
				25,
				30,
				42,
				15,
				32,
				16,
				30,
				20,
				117,
				4,
				118,
				28,
				40,
				47,
				7,
				48,
				30,
				43,
				24,
				22,
				25,
				30,
				10,
				15,
				67,
				16,
				30,
				19,
				118,
				6,
				119,
				28,
				18,
				47,
				31,
				48,
				30,
				34,
				24,
				34,
				25,
				30,
				20,
				15,
				61,
				16
			};
			int num = 0;
			for (int i = 1; i <= 40; i++)
			{
				for (int j = 0; j < list.Count; j++)
				{
					QRCode.codeWordsLengthTable.Add(i.ToString() + list[j], new CodeWordsBlockInfo(list2[num], list2[num + 1], list2[num + 2], list2[num + 3], list2[num + 4]));
					num += 5;
				}
			}
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x000151B4 File Offset: 0x000133B4
		private static void PopulateExponentsOfAlphaToValues()
		{
			QRCode.exponentsOfAlphaToValues = new List<int>
			{
				1,
				2,
				4,
				8,
				16,
				32,
				64,
				128,
				29,
				58,
				116,
				232,
				205,
				135,
				19,
				38,
				76,
				152,
				45,
				90,
				180,
				117,
				234,
				201,
				143,
				3,
				6,
				12,
				24,
				48,
				96,
				192,
				157,
				39,
				78,
				156,
				37,
				74,
				148,
				53,
				106,
				212,
				181,
				119,
				238,
				193,
				159,
				35,
				70,
				140,
				5,
				10,
				20,
				40,
				80,
				160,
				93,
				186,
				105,
				210,
				185,
				111,
				222,
				161,
				95,
				190,
				97,
				194,
				153,
				47,
				94,
				188,
				101,
				202,
				137,
				15,
				30,
				60,
				120,
				240,
				253,
				231,
				211,
				187,
				107,
				214,
				177,
				127,
				254,
				225,
				223,
				163,
				91,
				182,
				113,
				226,
				217,
				175,
				67,
				134,
				17,
				34,
				68,
				136,
				13,
				26,
				52,
				104,
				208,
				189,
				103,
				206,
				129,
				31,
				62,
				124,
				248,
				237,
				199,
				147,
				59,
				118,
				236,
				197,
				151,
				51,
				102,
				204,
				133,
				23,
				46,
				92,
				184,
				109,
				218,
				169,
				79,
				158,
				33,
				66,
				132,
				21,
				42,
				84,
				168,
				77,
				154,
				41,
				82,
				164,
				85,
				170,
				73,
				146,
				57,
				114,
				228,
				213,
				183,
				115,
				230,
				209,
				191,
				99,
				198,
				145,
				63,
				126,
				252,
				229,
				215,
				179,
				123,
				246,
				241,
				255,
				227,
				219,
				171,
				75,
				150,
				49,
				98,
				196,
				149,
				55,
				110,
				220,
				165,
				87,
				174,
				65,
				130,
				25,
				50,
				100,
				200,
				141,
				7,
				14,
				28,
				56,
				112,
				224,
				221,
				167,
				83,
				166,
				81,
				162,
				89,
				178,
				121,
				242,
				249,
				239,
				195,
				155,
				43,
				86,
				172,
				69,
				138,
				9,
				18,
				36,
				72,
				144,
				61,
				122,
				244,
				245,
				247,
				243,
				251,
				235,
				203,
				139,
				11,
				22,
				44,
				88,
				176,
				125,
				250,
				233,
				207,
				131,
				27,
				54,
				108,
				216,
				173,
				71,
				142,
				1
			};
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x00015B44 File Offset: 0x00013D44
		private static void PopulateValuesOfExponentsOfAlpha()
		{
			QRCode.valuesToExponentsOfAlpha = new List<int>
			{
				-1,
				0,
				1,
				25,
				2,
				50,
				26,
				198,
				3,
				223,
				51,
				238,
				27,
				104,
				199,
				75,
				4,
				100,
				224,
				14,
				52,
				141,
				239,
				129,
				28,
				193,
				105,
				248,
				200,
				8,
				76,
				113,
				5,
				138,
				101,
				47,
				225,
				36,
				15,
				33,
				53,
				147,
				142,
				218,
				240,
				18,
				130,
				69,
				29,
				181,
				194,
				125,
				106,
				39,
				249,
				185,
				201,
				154,
				9,
				120,
				77,
				228,
				114,
				166,
				6,
				191,
				139,
				98,
				102,
				221,
				48,
				253,
				226,
				152,
				37,
				179,
				16,
				145,
				34,
				136,
				54,
				208,
				148,
				206,
				143,
				150,
				219,
				189,
				241,
				210,
				19,
				92,
				131,
				56,
				70,
				64,
				30,
				66,
				182,
				163,
				195,
				72,
				126,
				110,
				107,
				58,
				40,
				84,
				250,
				133,
				186,
				61,
				202,
				94,
				155,
				159,
				10,
				21,
				121,
				43,
				78,
				212,
				229,
				172,
				115,
				243,
				167,
				87,
				7,
				112,
				192,
				247,
				140,
				128,
				99,
				13,
				103,
				74,
				222,
				237,
				49,
				197,
				254,
				24,
				227,
				165,
				153,
				119,
				38,
				184,
				180,
				124,
				17,
				68,
				146,
				217,
				35,
				32,
				137,
				46,
				55,
				63,
				209,
				91,
				149,
				188,
				207,
				205,
				144,
				135,
				151,
				178,
				220,
				252,
				190,
				97,
				242,
				86,
				211,
				171,
				20,
				42,
				93,
				158,
				132,
				60,
				57,
				83,
				71,
				109,
				65,
				162,
				31,
				45,
				67,
				216,
				183,
				123,
				164,
				118,
				196,
				23,
				73,
				236,
				127,
				12,
				111,
				246,
				108,
				161,
				59,
				82,
				41,
				157,
				85,
				170,
				251,
				96,
				134,
				177,
				187,
				204,
				62,
				90,
				203,
				89,
				95,
				176,
				156,
				169,
				160,
				81,
				11,
				245,
				22,
				235,
				122,
				117,
				44,
				215,
				79,
				174,
				213,
				233,
				230,
				231,
				173,
				232,
				116,
				214,
				244,
				234,
				168,
				80,
				88,
				175
			};
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x000164D0 File Offset: 0x000146D0
		private static void PopulateGeneratorExponentsOfAlpha()
		{
			QRCode.generatorExponentsOfAlpha = new Dictionary<int, List<int>>();
			QRCode.generatorExponentsOfAlpha.Add(7, new List<int>
			{
				87,
				229,
				146,
				149,
				238,
				102,
				21
			});
			QRCode.generatorExponentsOfAlpha.Add(10, new List<int>
			{
				251,
				67,
				46,
				61,
				118,
				70,
				64,
				94,
				32,
				45
			});
			QRCode.generatorExponentsOfAlpha.Add(13, new List<int>
			{
				74,
				152,
				176,
				100,
				86,
				100,
				106,
				104,
				130,
				218,
				206,
				140,
				78
			});
			QRCode.generatorExponentsOfAlpha.Add(15, new List<int>
			{
				8,
				183,
				61,
				91,
				202,
				37,
				51,
				58,
				58,
				237,
				140,
				124,
				5,
				99,
				105
			});
			QRCode.generatorExponentsOfAlpha.Add(16, new List<int>
			{
				120,
				104,
				107,
				109,
				102,
				161,
				76,
				3,
				91,
				191,
				147,
				169,
				182,
				194,
				225,
				120
			});
			QRCode.generatorExponentsOfAlpha.Add(17, new List<int>
			{
				43,
				139,
				206,
				78,
				43,
				239,
				123,
				206,
				214,
				147,
				24,
				99,
				150,
				39,
				243,
				163,
				136
			});
			QRCode.generatorExponentsOfAlpha.Add(18, new List<int>
			{
				215,
				234,
				158,
				94,
				184,
				97,
				118,
				170,
				79,
				187,
				152,
				148,
				252,
				179,
				5,
				98,
				96,
				153
			});
			QRCode.generatorExponentsOfAlpha.Add(20, new List<int>
			{
				17,
				60,
				79,
				50,
				61,
				163,
				26,
				187,
				202,
				180,
				221,
				225,
				83,
				239,
				156,
				164,
				212,
				212,
				188,
				190
			});
			QRCode.generatorExponentsOfAlpha.Add(22, new List<int>
			{
				210,
				171,
				247,
				242,
				93,
				230,
				14,
				109,
				221,
				53,
				200,
				74,
				8,
				172,
				98,
				80,
				219,
				134,
				160,
				105,
				165,
				231
			});
			QRCode.generatorExponentsOfAlpha.Add(24, new List<int>
			{
				229,
				121,
				135,
				48,
				211,
				117,
				251,
				126,
				159,
				180,
				169,
				152,
				192,
				226,
				228,
				218,
				111,
				0,
				117,
				232,
				87,
				96,
				227,
				21
			});
			QRCode.generatorExponentsOfAlpha.Add(26, new List<int>
			{
				173,
				125,
				158,
				2,
				103,
				182,
				118,
				17,
				145,
				201,
				111,
				28,
				165,
				53,
				161,
				21,
				245,
				142,
				13,
				102,
				48,
				227,
				153,
				145,
				218,
				70
			});
			QRCode.generatorExponentsOfAlpha.Add(28, new List<int>
			{
				168,
				223,
				200,
				104,
				224,
				234,
				108,
				180,
				110,
				190,
				195,
				147,
				205,
				27,
				232,
				201,
				21,
				43,
				245,
				87,
				42,
				195,
				212,
				119,
				242,
				37,
				9,
				123
			});
			QRCode.generatorExponentsOfAlpha.Add(30, new List<int>
			{
				41,
				173,
				145,
				152,
				216,
				31,
				179,
				182,
				50,
				48,
				110,
				86,
				239,
				96,
				222,
				125,
				42,
				173,
				226,
				193,
				224,
				130,
				156,
				37,
				251,
				216,
				238,
				40,
				192,
				180
			});
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x00016FEC File Offset: 0x000151EC
		private static void PopulatePositionAdjustmentTable()
		{
			QRCode.positionAdjustmentPatternCoordinates = new Dictionary<int, List<int>>();
			int num = 2;
			QRCode.positionAdjustmentPatternCoordinates.Add(num++, new List<int>
			{
				6,
				18
			});
			QRCode.positionAdjustmentPatternCoordinates.Add(num++, new List<int>
			{
				6,
				22
			});
			QRCode.positionAdjustmentPatternCoordinates.Add(num++, new List<int>
			{
				6,
				26
			});
			QRCode.positionAdjustmentPatternCoordinates.Add(num++, new List<int>
			{
				6,
				30
			});
			QRCode.positionAdjustmentPatternCoordinates.Add(num++, new List<int>
			{
				6,
				34
			});
			QRCode.positionAdjustmentPatternCoordinates.Add(num++, new List<int>
			{
				6,
				22,
				38
			});
			QRCode.positionAdjustmentPatternCoordinates.Add(num++, new List<int>
			{
				6,
				24,
				42
			});
			QRCode.positionAdjustmentPatternCoordinates.Add(num++, new List<int>
			{
				6,
				26,
				46
			});
			QRCode.positionAdjustmentPatternCoordinates.Add(num++, new List<int>
			{
				6,
				28,
				50
			});
			QRCode.positionAdjustmentPatternCoordinates.Add(num++, new List<int>
			{
				6,
				30,
				54
			});
			QRCode.positionAdjustmentPatternCoordinates.Add(num++, new List<int>
			{
				6,
				32,
				58
			});
			QRCode.positionAdjustmentPatternCoordinates.Add(num++, new List<int>
			{
				6,
				34,
				62
			});
			QRCode.positionAdjustmentPatternCoordinates.Add(num++, new List<int>
			{
				6,
				26,
				46,
				66
			});
			QRCode.positionAdjustmentPatternCoordinates.Add(num++, new List<int>
			{
				6,
				26,
				48,
				70
			});
			QRCode.positionAdjustmentPatternCoordinates.Add(num++, new List<int>
			{
				6,
				26,
				50,
				74
			});
			QRCode.positionAdjustmentPatternCoordinates.Add(num++, new List<int>
			{
				6,
				30,
				54,
				78
			});
			QRCode.positionAdjustmentPatternCoordinates.Add(num++, new List<int>
			{
				6,
				30,
				56,
				82
			});
			QRCode.positionAdjustmentPatternCoordinates.Add(num++, new List<int>
			{
				6,
				30,
				58,
				86
			});
			QRCode.positionAdjustmentPatternCoordinates.Add(num++, new List<int>
			{
				6,
				34,
				62,
				90
			});
			QRCode.positionAdjustmentPatternCoordinates.Add(num++, new List<int>
			{
				6,
				28,
				50,
				72,
				94
			});
			QRCode.positionAdjustmentPatternCoordinates.Add(num++, new List<int>
			{
				6,
				26,
				50,
				74,
				98
			});
			QRCode.positionAdjustmentPatternCoordinates.Add(num++, new List<int>
			{
				6,
				30,
				54,
				78,
				102
			});
			QRCode.positionAdjustmentPatternCoordinates.Add(num++, new List<int>
			{
				6,
				28,
				54,
				80,
				106
			});
			QRCode.positionAdjustmentPatternCoordinates.Add(num++, new List<int>
			{
				6,
				32,
				58,
				84,
				110
			});
			QRCode.positionAdjustmentPatternCoordinates.Add(num++, new List<int>
			{
				6,
				30,
				58,
				86,
				114
			});
			QRCode.positionAdjustmentPatternCoordinates.Add(num++, new List<int>
			{
				6,
				34,
				62,
				90,
				118
			});
			QRCode.positionAdjustmentPatternCoordinates.Add(num++, new List<int>
			{
				6,
				26,
				50,
				74,
				98,
				122
			});
			QRCode.positionAdjustmentPatternCoordinates.Add(num++, new List<int>
			{
				6,
				30,
				54,
				78,
				102,
				126
			});
			QRCode.positionAdjustmentPatternCoordinates.Add(num++, new List<int>
			{
				6,
				26,
				52,
				78,
				104,
				130
			});
			QRCode.positionAdjustmentPatternCoordinates.Add(num++, new List<int>
			{
				6,
				30,
				56,
				82,
				108,
				134
			});
			QRCode.positionAdjustmentPatternCoordinates.Add(num++, new List<int>
			{
				6,
				34,
				60,
				86,
				112,
				138
			});
			QRCode.positionAdjustmentPatternCoordinates.Add(num++, new List<int>
			{
				6,
				30,
				58,
				86,
				114,
				142
			});
			QRCode.positionAdjustmentPatternCoordinates.Add(num++, new List<int>
			{
				6,
				34,
				62,
				90,
				118,
				146
			});
			QRCode.positionAdjustmentPatternCoordinates.Add(num++, new List<int>
			{
				6,
				30,
				54,
				78,
				102,
				126,
				150
			});
			QRCode.positionAdjustmentPatternCoordinates.Add(num++, new List<int>
			{
				6,
				24,
				50,
				76,
				102,
				128,
				154
			});
			QRCode.positionAdjustmentPatternCoordinates.Add(num++, new List<int>
			{
				6,
				28,
				54,
				80,
				106,
				132,
				158
			});
			QRCode.positionAdjustmentPatternCoordinates.Add(num++, new List<int>
			{
				6,
				32,
				58,
				84,
				110,
				136,
				162
			});
			QRCode.positionAdjustmentPatternCoordinates.Add(num++, new List<int>
			{
				6,
				26,
				54,
				82,
				110,
				138,
				166
			});
			QRCode.positionAdjustmentPatternCoordinates.Add(num++, new List<int>
			{
				6,
				30,
				58,
				86,
				114,
				142,
				170
			});
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x000179EC File Offset: 0x00015BEC
		private static void PopulateErrorCorrectionToMask()
		{
			QRCode.errorCorrectionToMask = new Dictionary<Modes.ErrorCorrectionLevel, string>();
			QRCode.errorCorrectionToMask.Add(Modes.ErrorCorrectionLevel.L, "01");
			QRCode.errorCorrectionToMask.Add(Modes.ErrorCorrectionLevel.M, "00");
			QRCode.errorCorrectionToMask.Add(Modes.ErrorCorrectionLevel.Q, "11");
			QRCode.errorCorrectionToMask.Add(Modes.ErrorCorrectionLevel.H, "10");
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x00017A44 File Offset: 0x00015C44
		private static void PopulateFormatInformation()
		{
			QRCode.formatInformation = new Dictionary<string, string>();
			QRCode.formatInformation.Add("00000", "101010000010010");
			QRCode.formatInformation.Add("00001", "101000100100101");
			QRCode.formatInformation.Add("00010", "101111001111100");
			QRCode.formatInformation.Add("00011", "101101101001011");
			QRCode.formatInformation.Add("00100", "100010111111001");
			QRCode.formatInformation.Add("00101", "100000011001110");
			QRCode.formatInformation.Add("00110", "100111110010111");
			QRCode.formatInformation.Add("00111", "100101010100000");
			QRCode.formatInformation.Add("01000", "111011111000100");
			QRCode.formatInformation.Add("01001", "111001011110011");
			QRCode.formatInformation.Add("01010", "111110110101010");
			QRCode.formatInformation.Add("01011", "111100010011101");
			QRCode.formatInformation.Add("01100", "110011000101111");
			QRCode.formatInformation.Add("01101", "110001100011000");
			QRCode.formatInformation.Add("01110", "110110001000001");
			QRCode.formatInformation.Add("01111", "110100101110110");
			QRCode.formatInformation.Add("10000", "001011010001001");
			QRCode.formatInformation.Add("10001", "001001110111110");
			QRCode.formatInformation.Add("10010", "001110011100111");
			QRCode.formatInformation.Add("10011", "001100111010000");
			QRCode.formatInformation.Add("10100", "000011101100010");
			QRCode.formatInformation.Add("10101", "000001001010101");
			QRCode.formatInformation.Add("10110", "000110100001100");
			QRCode.formatInformation.Add("10111", "000100000111011");
			QRCode.formatInformation.Add("11000", "011010101011111");
			QRCode.formatInformation.Add("11001", "011000001101000");
			QRCode.formatInformation.Add("11010", "011111100110001");
			QRCode.formatInformation.Add("11011", "011101000000110");
			QRCode.formatInformation.Add("11100", "010010010110100");
			QRCode.formatInformation.Add("11101", "010000110000011");
			QRCode.formatInformation.Add("11110", "010111011011010");
			QRCode.formatInformation.Add("11111", "010101111101101");
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x00017CDC File Offset: 0x00015EDC
		public QRCode(Modes.CodeMode modeL, int versionL, Modes.ErrorCorrectionLevel errorLevelL, Modes.ECIMode eciModeL, Modes.FNC1Mode fncModeL, string appIndicator, bool forceAutoIncreaseVersion = false)
		{
			this.codeMode = modeL;
			this.errorCorrectionLevel = errorLevelL;
			this.eciMode = eciModeL;
			this.fnc1Mode = fncModeL;
			this.applicationIndicator = appIndicator;
			if (versionL < 1 || versionL > 40)
			{
				versionL = 1;
				this.autoSetVersion = true;
			}
			this.autoSetVersion = (this.autoSetVersion || forceAutoIncreaseVersion);
			this.SetVersion(versionL);
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x0600063D RID: 1597 RVA: 0x00017D42 File Offset: 0x00015F42
		// (set) Token: 0x0600063E RID: 1598 RVA: 0x00017D4A File Offset: 0x00015F4A
		public bool[,] BinaryMatrix
		{
			get
			{
				return this.binaryMatrix;
			}
			set
			{
				this.binaryMatrix = value;
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x0600063F RID: 1599 RVA: 0x00017D53 File Offset: 0x00015F53
		// (set) Token: 0x06000640 RID: 1600 RVA: 0x00017D5B File Offset: 0x00015F5B
		public bool[,] FilledValuesMatrix
		{
			get
			{
				return this.filledValuesMatrix;
			}
			set
			{
				this.filledValuesMatrix = value;
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000641 RID: 1601 RVA: 0x00017D64 File Offset: 0x00015F64
		// (set) Token: 0x06000642 RID: 1602 RVA: 0x00017D6C File Offset: 0x00015F6C
		internal int VersionDimension { get; private set; }

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000643 RID: 1603 RVA: 0x00017D75 File Offset: 0x00015F75
		internal int Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x00017D80 File Offset: 0x00015F80
		internal string EncodeData(string dataToEncodeL, ref int outVersion)
		{
			if (dataToEncodeL == null)
			{
				return string.Empty;
			}
			string text = dataToEncodeL;
			if (this.codeMode == Modes.CodeMode.Alphanumeric)
			{
				text = this.EncodeAlphaNumeric(dataToEncodeL);
			}
			else if (this.codeMode == Modes.CodeMode.Numeric)
			{
				text = this.EndcodeNumeric(dataToEncodeL);
			}
			else if (this.codeMode == Modes.CodeMode.Byte)
			{
				text = this.EncodeByte(dataToEncodeL);
			}
			else if (this.codeMode == Modes.CodeMode.Kanji)
			{
				text = this.EncodeKanji(dataToEncodeL);
			}
			string text2 = string.Empty;
			for (int i = 0; i < this.dataResult.Count; i++)
			{
				text2 += this.dataResult[i];
			}
			this.AddFNC1Data();
			this.encodedData += QRCode.codeModeValues[this.codeMode];
			int num = this.encodedData.Length + text2.Length;
			if (this.autoSetVersion)
			{
				for (int j = this.version; j < 41; j++)
				{
					int num2 = QRCode.dataCapacityTable[new DataCapacityIndexer(j, this.errorCorrectionLevel)] - this.DetermineCountLength(j);
					if (num <= num2)
					{
						this.SetVersion(j);
						break;
					}
				}
			}
			int num3 = QRCode.dataCapacityTable[new DataCapacityIndexer(this.version, this.errorCorrectionLevel)];
			int num4 = this.DetermineCountLength();
			if (num > num3 - num4)
			{
				throw new ArgumentOutOfRangeException("The Text cannot be encoded with the current Version, ErrorCorrectionLevel and Mode.");
			}
			string text3 = Convert.ToString(text.Length, 2);
			text3 = text3.PadLeft(num4, '0');
			this.encodedData += text3;
			this.encodedData += text2;
			this.encodedData = this.PadLength(this.encodedData, num3);
			this.encodedData = this.GenerateErrorCorrectionSequence();
			this.PopulateBinaryMatricesWithData();
			outVersion = this.version;
			return this.encodedData;
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x00017F48 File Offset: 0x00016148
		[SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow")]
		private static void SetSingleCharacter(int rowIndex, int columnIndex, char value, bool[,] matrix, string maskCode)
		{
			bool flag;
			switch (maskCode)
			{
			case "000":
				flag = ((columnIndex + rowIndex) % 2 == 0);
				goto IL_153;
			case "001":
				flag = (rowIndex % 2 == 0);
				goto IL_153;
			case "010":
				flag = (columnIndex % 3 == 0);
				goto IL_153;
			case "011":
				flag = ((rowIndex + columnIndex) % 3 == 0);
				goto IL_153;
			case "100":
				flag = ((rowIndex / 2 + columnIndex / 3) % 2 == 0);
				goto IL_153;
			case "101":
				flag = (columnIndex * rowIndex % 2 + columnIndex * rowIndex % 3 == 0);
				goto IL_153;
			case "110":
				flag = ((columnIndex * rowIndex % 2 + columnIndex * rowIndex % 3) % 2 == 0);
				goto IL_153;
			case "111":
				flag = (((rowIndex + columnIndex) % 2 + rowIndex * columnIndex % 3) % 2 == 0);
				goto IL_153;
			}
			flag = ((columnIndex + rowIndex) % 2 == 0);
			IL_153:
			bool flag2;
			if (flag)
			{
				flag2 = (value == '0');
			}
			else
			{
				flag2 = (value != '0');
			}
			matrix[rowIndex + 4, columnIndex + 4] = flag2;
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x000180D0 File Offset: 0x000162D0
		private static List<int> GetErrorCorrectionForBlock(List<int> dataList, CodeWordsBlockInfo localInfo)
		{
			int codeWordsPerBlock = localInfo.CodeWordsPerBlock;
			int[] array = new int[codeWordsPerBlock];
			List<int> list = new List<int>(dataList.Count + codeWordsPerBlock);
			for (int i = 0; i < dataList.Count + codeWordsPerBlock; i++)
			{
				if (i < dataList.Count)
				{
					list.Add(dataList[i]);
				}
				else
				{
					list.Add(0);
				}
			}
			for (int j = 0; j < dataList.Count; j++)
			{
				int num = list[0];
				list.RemoveAt(0);
				if (num != 0)
				{
					QRCode.generatorExponentsOfAlpha[codeWordsPerBlock].CopyTo(array);
					num = QRCode.valuesToExponentsOfAlpha[num];
					for (int k = 0; k < array.Length; k++)
					{
						int num2 = num + array[k];
						if (num2 > 255)
						{
							num2 %= 255;
						}
						array[k] = num2;
					}
					for (int l = 0; l < array.Length; l++)
					{
						array[l] = QRCode.exponentsOfAlphaToValues[array[l]];
					}
					for (int m = 0; m < array.Length; m++)
					{
						list[m] ^= array[m];
					}
				}
			}
			return list;
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x000181FC File Offset: 0x000163FC
		private void SetVersion(int newVersion)
		{
			this.version = newVersion;
			int num = 21 + (newVersion - 1) * 4;
			int num2 = num + 8;
			this.VersionDimension = num;
			this.sizeOfData = num;
			this.sizeOfMatrix = num2;
			this.info = QRCode.codeWordsLengthTable[newVersion.ToString() + this.errorCorrectionLevel.ToString()];
			this.PopulateValueMatrix();
			if (newVersion >= 7)
			{
				this.PopulateVersionData();
			}
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x00018270 File Offset: 0x00016470
		private string EncodeKanji(string dataToEncodeL)
		{
			KanjiMode kanjiMode = new KanjiMode();
			dataToEncodeL = kanjiMode.ValidateData(dataToEncodeL);
			this.dataResult = kanjiMode.EncodeData(dataToEncodeL);
			return dataToEncodeL;
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x0001829C File Offset: 0x0001649C
		private string EncodeByte(string dataToEncodeL)
		{
			if (this.eciMode == Modes.ECIMode.None)
			{
				ByteMode byteMode = new ByteMode();
				dataToEncodeL = byteMode.ValidateData(dataToEncodeL);
				this.dataResult = byteMode.EncodeData(dataToEncodeL);
			}
			else
			{
				this.encodedData += "0111";
				this.encodedData += Convert.ToString((int)this.eciMode, 2).PadLeft(8, '0');
				switch (this.eciMode)
				{
				case Modes.ECIMode.ISO8859_1:
				{
					ISO8859_1 iso8859_ = new ISO8859_1();
					dataToEncodeL = iso8859_.ValidateData(dataToEncodeL);
					this.dataResult = iso8859_.EncodeData(dataToEncodeL);
					break;
				}
				case Modes.ECIMode.CP437:
				{
					CP437_DOSLatinUS cp437_DOSLatinUS = new CP437_DOSLatinUS();
					dataToEncodeL = cp437_DOSLatinUS.ValidateData(dataToEncodeL);
					this.dataResult = cp437_DOSLatinUS.EncodeData(dataToEncodeL);
					break;
				}
				case Modes.ECIMode.ISO8859_1En:
				{
					ISO8859_1Eng iso8859_1Eng = new ISO8859_1Eng();
					dataToEncodeL = iso8859_1Eng.ValidateData(dataToEncodeL);
					this.dataResult = iso8859_1Eng.EncodeData(dataToEncodeL);
					break;
				}
				case Modes.ECIMode.ISO8859_2:
				{
					ISO8859_2ECEuropean iso8859_2ECEuropean = new ISO8859_2ECEuropean();
					dataToEncodeL = iso8859_2ECEuropean.ValidateData(dataToEncodeL);
					this.dataResult = iso8859_2ECEuropean.EncodeData(dataToEncodeL);
					break;
				}
				case Modes.ECIMode.ISO8859_3:
				{
					ISO8859_3Latin iso8859_3Latin = new ISO8859_3Latin();
					dataToEncodeL = iso8859_3Latin.ValidateData(dataToEncodeL);
					this.dataResult = iso8859_3Latin.EncodeData(dataToEncodeL);
					break;
				}
				case Modes.ECIMode.ISO8859_4:
				{
					ISO8859_4Baltic iso8859_4Baltic = new ISO8859_4Baltic();
					dataToEncodeL = iso8859_4Baltic.ValidateData(dataToEncodeL);
					this.dataResult = iso8859_4Baltic.EncodeData(dataToEncodeL);
					break;
				}
				case Modes.ECIMode.ISO8859_5:
				{
					ISO8859_5Cyrillic iso8859_5Cyrillic = new ISO8859_5Cyrillic();
					dataToEncodeL = iso8859_5Cyrillic.ValidateData(dataToEncodeL);
					this.dataResult = iso8859_5Cyrillic.EncodeData(dataToEncodeL);
					break;
				}
				case Modes.ECIMode.ISO8859_6:
				{
					ISO8859_6Arabic iso8859_6Arabic = new ISO8859_6Arabic();
					dataToEncodeL = iso8859_6Arabic.ValidateData(dataToEncodeL);
					this.dataResult = iso8859_6Arabic.EncodeData(dataToEncodeL);
					break;
				}
				case Modes.ECIMode.ISO8859_7:
				{
					ISO8859_7Greek iso8859_7Greek = new ISO8859_7Greek();
					dataToEncodeL = iso8859_7Greek.ValidateData(dataToEncodeL);
					this.dataResult = iso8859_7Greek.EncodeData(dataToEncodeL);
					break;
				}
				case Modes.ECIMode.ISO8859_8:
				{
					ISO8859_8Hebrew iso8859_8Hebrew = new ISO8859_8Hebrew();
					dataToEncodeL = iso8859_8Hebrew.ValidateData(dataToEncodeL);
					this.dataResult = iso8859_8Hebrew.EncodeData(dataToEncodeL);
					break;
				}
				case Modes.ECIMode.ISO8859_9:
				{
					ISO8859_9Turkish iso8859_9Turkish = new ISO8859_9Turkish();
					dataToEncodeL = iso8859_9Turkish.ValidateData(dataToEncodeL);
					this.dataResult = iso8859_9Turkish.EncodeData(dataToEncodeL);
					break;
				}
				case Modes.ECIMode.ISO8859_11:
				{
					ISO8859_11Thai iso8859_11Thai = new ISO8859_11Thai();
					dataToEncodeL = iso8859_11Thai.ValidateData(dataToEncodeL);
					this.dataResult = iso8859_11Thai.EncodeData(dataToEncodeL);
					break;
				}
				case Modes.ECIMode.ISO8859_13:
				{
					ISO8859_13 iso8859_2 = new ISO8859_13();
					dataToEncodeL = iso8859_2.ValidateData(dataToEncodeL);
					this.dataResult = iso8859_2.EncodeData(dataToEncodeL);
					break;
				}
				case Modes.ECIMode.ISO8859_15:
				{
					ISO8859_15 iso8859_3 = new ISO8859_15();
					dataToEncodeL = iso8859_3.ValidateData(dataToEncodeL);
					this.dataResult = iso8859_3.EncodeData(dataToEncodeL);
					break;
				}
				case Modes.ECIMode.Windows1250:
				{
					Windows1250 windows = new Windows1250();
					dataToEncodeL = windows.ValidateData(dataToEncodeL);
					this.dataResult = windows.EncodeData(dataToEncodeL);
					break;
				}
				case Modes.ECIMode.Windows1251:
				{
					Windows1251 windows2 = new Windows1251();
					dataToEncodeL = windows2.ValidateData(dataToEncodeL);
					this.dataResult = windows2.EncodeData(dataToEncodeL);
					break;
				}
				case Modes.ECIMode.Windows1252:
				{
					Windows1252 windows3 = new Windows1252();
					dataToEncodeL = windows3.ValidateData(dataToEncodeL);
					this.dataResult = windows3.EncodeData(dataToEncodeL);
					break;
				}
				case Modes.ECIMode.Windows1256:
				{
					Windows1256 windows4 = new Windows1256();
					dataToEncodeL = windows4.ValidateData(dataToEncodeL);
					this.dataResult = windows4.EncodeData(dataToEncodeL);
					break;
				}
				case Modes.ECIMode.ISO646US:
				{
					ISO646US iso646US = new ISO646US();
					dataToEncodeL = iso646US.ValidateData(dataToEncodeL);
					this.dataResult = iso646US.EncodeData(dataToEncodeL);
					break;
				}
				}
			}
			return dataToEncodeL;
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x00018628 File Offset: 0x00016828
		private string EndcodeNumeric(string dataToEncodeL)
		{
			Numeric numeric = new Numeric();
			dataToEncodeL = numeric.ValidateData(dataToEncodeL);
			this.dataResult = numeric.EncodeData(dataToEncodeL);
			return dataToEncodeL;
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x00018654 File Offset: 0x00016854
		private string EncodeAlphaNumeric(string dataToEncodeL)
		{
			AlphaNumeric alphaNumeric = new AlphaNumeric();
			dataToEncodeL = alphaNumeric.ValidateData(dataToEncodeL);
			this.dataResult = alphaNumeric.EncodeData(dataToEncodeL);
			return dataToEncodeL;
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x00018680 File Offset: 0x00016880
		private void AddFNC1Data()
		{
			if (this.fnc1Mode == Modes.FNC1Mode.FNC1FirstPosition)
			{
				this.encodedData += "0101";
				return;
			}
			if (this.fnc1Mode == Modes.FNC1Mode.FNC1SecondPosition)
			{
				this.encodedData += "1001";
				this.ValidateApplicationIndicatorValue();
			}
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x000186D4 File Offset: 0x000168D4
		private void ValidateApplicationIndicatorValue()
		{
			if (this.applicationIndicator.Length == 2)
			{
				char c = this.applicationIndicator[0];
				char c2 = this.applicationIndicator[1];
				int value;
				if (char.IsDigit(c) && char.IsDigit(c2) && int.TryParse(this.applicationIndicator, out value))
				{
					this.encodedData += Convert.ToString(value, 2).PadLeft(8, '0');
					return;
				}
			}
			else if (this.applicationIndicator.Length == 1)
			{
				char c3 = this.applicationIndicator.ToCharArray()[0];
				if (char.IsLetter(c3))
				{
					int value2 = (int)(c3 + 'd');
					this.encodedData += Convert.ToString(value2, 2).PadLeft(8, '0');
				}
			}
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x00018798 File Offset: 0x00016998
		private string GenerateErrorCorrectionSequence()
		{
			List<int> list = new List<int>();
			for (int i = 0; i < this.binaryValues.Count; i++)
			{
				list.Add(Convert.ToInt32(this.binaryValues[i], 2));
			}
			int j = this.info.FirstBlockCount;
			int k = this.info.SecondBlockCount;
			int num = 0;
			List<List<int>> list2 = new List<List<int>>();
			List<List<int>> list3 = new List<List<int>>();
			List<List<int>> list4 = new List<List<int>>();
			List<List<int>> list5 = new List<List<int>>();
			List<List<int>> list6 = new List<List<int>>();
			List<List<int>> list7 = new List<List<int>>();
			List<int> list8 = new List<int>();
			List<int> list9 = new List<int>();
			while (j > 0)
			{
				List<int> list10 = new List<int>();
				for (int l = 0; l < this.info.FirstDataCodeWords; l++)
				{
					list10.Add(list[num]);
					num++;
				}
				list2.Add(list10);
				list6.Add(QRCode.GetErrorCorrectionForBlock(list10, this.info));
				j--;
			}
			while (k > 0)
			{
				List<int> list11 = new List<int>();
				for (int m = 0; m < this.info.SecondBlockCodeWords; m++)
				{
					list11.Add(list[num]);
					num++;
				}
				list3.Add(list11);
				list7.Add(QRCode.GetErrorCorrectionForBlock(list11, this.info));
				k--;
			}
			int num2 = this.info.FirstDataCodeWords * this.info.FirstBlockCount + this.info.SecondBlockCodeWords * this.info.SecondBlockCount;
			int num3 = this.info.CodeWordsPerBlock * this.info.FirstBlockCount + this.info.CodeWordsPerBlock * this.info.SecondBlockCount;
			int num4 = 0;
			int num5 = list2.Count + list3.Count;
			for (int n = 0; n < num5; n++)
			{
				if (num4 == 0)
				{
					if (list2.Count > 0)
					{
						list4.Add(list2[0]);
						list2.RemoveAt(0);
					}
					else
					{
						list4.Add(list3[0]);
						num4++;
						list3.RemoveAt(0);
					}
				}
				else if (num4 == 1 && list3.Count > 0)
				{
					list4.Add(list3[0]);
					list3.RemoveAt(0);
				}
			}
			num4 = 0;
			num5 = list6.Count + list7.Count;
			for (int num6 = 0; num6 < num5; num6++)
			{
				if (num4 == 0)
				{
					if (list6.Count > 0)
					{
						list5.Add(list6[0]);
						list6.RemoveAt(0);
					}
					else
					{
						list5.Add(list7[0]);
						num4++;
						list7.RemoveAt(0);
					}
				}
				else if (num4 == 1 && list7.Count > 0)
				{
					list5.Add(list7[0]);
					list7.RemoveAt(0);
				}
			}
			int count = list4.Count;
			int count2 = list5.Count;
			num4 = 0;
			for (int num7 = 0; num7 < num2; num7++)
			{
				if (num4 == count)
				{
					num4 = 0;
				}
				if (list4[num4].Count > 0)
				{
					list8.Add(list4[num4][0]);
					list4[num4].RemoveAt(0);
					num4++;
				}
				else
				{
					num4++;
					num7--;
				}
			}
			num4 = 0;
			for (int num8 = 0; num8 < num3; num8++)
			{
				if (num4 == count2)
				{
					num4 = 0;
				}
				if (list5[num4].Count > 0)
				{
					list9.Add(list5[num4][0]);
					list5[num4].RemoveAt(0);
					num4++;
				}
				else
				{
					num4++;
					num8--;
				}
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (int value in list8)
			{
				stringBuilder.Append(Convert.ToString(value, 2).PadLeft(8, '0'));
			}
			foreach (int value2 in list9)
			{
				stringBuilder.Append(Convert.ToString(value2, 2).PadLeft(8, '0'));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x00018C00 File Offset: 0x00016E00
		private void PopulateBinaryValues(string valueToBreak)
		{
			int num = 0;
			for (int i = 0; i < valueToBreak.Length; i += 8)
			{
				if (i + 8 <= valueToBreak.Length)
				{
					this.binaryValues[num++] = valueToBreak.ToString().Substring(i, 8);
				}
				else
				{
					this.binaryValues[num++] = valueToBreak.ToString().Substring(i).PadRight(8, '0');
				}
			}
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x00018C70 File Offset: 0x00016E70
		private string PadLength(string valueToAdjust, int requiredLength)
		{
			StringBuilder stringBuilder = new StringBuilder();
			this.binaryValues = new Dictionary<int, string>();
			if (valueToAdjust.Length > requiredLength)
			{
				stringBuilder.Append(valueToAdjust.Substring(0, requiredLength));
				this.PopulateBinaryValues(stringBuilder.ToString());
			}
			else if (valueToAdjust.Length == requiredLength)
			{
				stringBuilder.Append(valueToAdjust);
				this.PopulateBinaryValues(stringBuilder.ToString());
			}
			else
			{
				stringBuilder.Append(valueToAdjust);
				for (int i = 0; i < 4; i++)
				{
					if (stringBuilder.Length < requiredLength)
					{
						stringBuilder.Append('0');
					}
				}
				this.PopulateBinaryValues(stringBuilder.ToString());
				stringBuilder = new StringBuilder();
				for (int j = 0; j < this.binaryValues.Count; j++)
				{
					stringBuilder.Append(this.binaryValues[j]);
				}
				string value = "11101100";
				string value2 = "00010001";
				bool flag = true;
				while (stringBuilder.Length < requiredLength)
				{
					if (flag)
					{
						stringBuilder.Append(value);
						this.binaryValues.Add(this.binaryValues.Count, value);
					}
					else
					{
						stringBuilder.Append(value2);
						this.binaryValues.Add(this.binaryValues.Count, value2);
					}
					flag = !flag;
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x00018DA8 File Offset: 0x00016FA8
		private void PopulateVersionData()
		{
			List<bool> values = QRCode.positionValues[this.version];
			this.AddUpperVersionInformation(values);
			this.AddLowerVersionInformation(values);
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x00018DD4 File Offset: 0x00016FD4
		private void AddUpperVersionInformation(List<bool> values)
		{
			int num = this.sizeOfMatrix - 15;
			int num2 = this.sizeOfData - 11;
			int num3 = 0;
			for (int i = 4; i < 10; i++)
			{
				this.BinaryMatrix[i, num] = values[num3];
				this.FilledValuesMatrix[i - 4, num2] = true;
				num3++;
				this.BinaryMatrix[i, num + 1] = values[num3];
				this.FilledValuesMatrix[i - 4, num2 + 1] = true;
				num3++;
				this.BinaryMatrix[i, num + 2] = values[num3];
				this.FilledValuesMatrix[i - 4, num2 + 2] = true;
				num3++;
			}
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x00018E8C File Offset: 0x0001708C
		private void AddLowerVersionInformation(List<bool> values)
		{
			int num = this.sizeOfMatrix - 13;
			int num2 = this.sizeOfData - 9;
			int num3 = 0;
			for (int i = 4; i < 10; i++)
			{
				this.BinaryMatrix[num, i] = values[num3];
				this.FilledValuesMatrix[num2, i - 4] = true;
				num3++;
				this.BinaryMatrix[num - 1, i] = values[num3];
				this.FilledValuesMatrix[num2 - 1, i - 4] = true;
				num3++;
				this.BinaryMatrix[num - 2, i] = values[num3];
				this.FilledValuesMatrix[num2 - 2, i - 4] = true;
				num3++;
			}
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x00018F42 File Offset: 0x00017142
		private void SetSingleModule(int rowIndex, int columnIndex, int binaryIndex, bool[,] matrix, string maskCode)
		{
			if (binaryIndex < this.encodedData.Length)
			{
				QRCode.SetSingleCharacter(rowIndex, columnIndex, this.encodedData[binaryIndex], matrix, maskCode);
				return;
			}
			QRCode.SetSingleCharacter(rowIndex, columnIndex, '0', matrix, maskCode);
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x00018F78 File Offset: 0x00017178
		private int CalculatePenalty1(bool[,] matrix)
		{
			int num = 0;
			for (int i = 0; i < this.sizeOfData; i++)
			{
				int num2 = 1;
				for (int j = 1; j < this.sizeOfData; j++)
				{
					bool flag = matrix[i + 4, j + 4];
					if (flag == matrix[i + 4, j + 3])
					{
						num2++;
						if (num2 == 5)
						{
							num += 3;
						}
						else if (num2 > 5)
						{
							num++;
						}
					}
					else
					{
						num2 = 1;
					}
				}
			}
			for (int k = 0; k < this.sizeOfData; k++)
			{
				int num3 = 1;
				for (int l = 1; l < this.sizeOfData; l++)
				{
					bool flag2 = matrix[l + 4, k + 4];
					if (flag2 == matrix[l + 3, k + 4])
					{
						num3++;
						if (num3 == 5)
						{
							num += 3;
						}
						else if (num3 > 5)
						{
							num++;
						}
					}
					else
					{
						num3 = 1;
					}
				}
			}
			return num;
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x00019054 File Offset: 0x00017254
		private int CalculatePenalty2(bool[,] matrix)
		{
			int num = 0;
			for (int i = 0; i < this.sizeOfData - 1; i++)
			{
				for (int j = 0; j < this.sizeOfData - 1; j++)
				{
					bool flag = matrix[i + 4, j + 4];
					if (flag == matrix[i + 4, j + 5] && flag == matrix[i + 5, j + 4] && flag == matrix[i + 5, j + 5])
					{
						num += 3;
					}
				}
			}
			return num;
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x000190C8 File Offset: 0x000172C8
		private int CalculatePenalty3(bool[,] matrix)
		{
			int num = 0;
			for (int i = 0; i < this.sizeOfData; i++)
			{
				for (int j = 0; j <= this.sizeOfData - 7; j++)
				{
					if (matrix[i + 4, j + 4] && !matrix[i + 4, j + 5] && matrix[i + 4, j + 6] && matrix[i + 4, j + 7] && matrix[i + 4, j + 8] && !matrix[i + 4, j + 9] && matrix[i + 4, j + 10])
					{
						num += 40;
					}
				}
			}
			for (int k = 0; k < this.sizeOfData; k++)
			{
				for (int l = 0; l <= this.sizeOfData - 7; l++)
				{
					if (matrix[l + 4, k + 4] && !matrix[l + 5, k + 4] && matrix[l + 6, k + 4] && matrix[l + 7, k + 4] && matrix[l + 8, k + 4] && !matrix[l + 9, k + 4] && matrix[l + 10, k + 4])
					{
						num += 40;
					}
				}
			}
			return num;
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x0001920C File Offset: 0x0001740C
		private int CalculatePenalty4(bool[,] matrix)
		{
			double num = 0.0;
			double num2 = 0.0;
			for (int i = 0; i < this.sizeOfData; i++)
			{
				for (int j = 0; j < this.sizeOfData; j++)
				{
					if (matrix[i + 4, j + 4])
					{
						num += 1.0;
					}
					else
					{
						num2 += 1.0;
					}
				}
			}
			return (int)(Math.Floor(Math.Abs(num / (num + num2) * 100.0 - 50.0)) / 5.0) * 10;
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x000192B4 File Offset: 0x000174B4
		private void PopulateBinaryMatricesWithData()
		{
			List<bool[,]> list = new List<bool[,]>();
			List<int> list2 = new List<int>(8);
			for (int i = 0; i < 8; i++)
			{
				int num = 0;
				bool[,] array = this.BinaryMatrix;
				this.PopulateFormatData(QRCode.maskCodes[i]);
				this.PopulateSingleMatrix(array, QRCode.maskCodes[i]);
				num += this.CalculatePenalty1(array);
				num += this.CalculatePenalty2(array);
				num += this.CalculatePenalty3(array);
				num += this.CalculatePenalty4(array);
				list2.Add(num);
				list.Add(array);
			}
			this.PopulateBinaryValues(this.encodedData);
			int index = list2.IndexOf(list2.Min());
			this.PopulateFormatData(QRCode.maskCodes[index]);
			this.PopulateSingleMatrix(this.BinaryMatrix, QRCode.maskCodes[index]);
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x00019388 File Offset: 0x00017588
		private void PopulateSingleMatrix(bool[,] matrix, string maskCode)
		{
			int num = 0;
			bool flag = true;
			for (int i = this.sizeOfData - 1; i >= 0; i -= 2)
			{
				if (i == 6)
				{
					i--;
				}
				if (flag)
				{
					for (int j = this.sizeOfData - 1; j >= 0; j--)
					{
						if (!this.FilledValuesMatrix[j, i])
						{
							this.SetSingleModule(j, i, num, matrix, maskCode);
							num++;
						}
						if (i - 1 >= 0 && !this.FilledValuesMatrix[j, i - 1])
						{
							this.SetSingleModule(j, i - 1, num, matrix, maskCode);
							num++;
						}
					}
					flag = !flag;
				}
				else
				{
					for (int k = 0; k < this.sizeOfData; k++)
					{
						if (!this.FilledValuesMatrix[k, i])
						{
							this.SetSingleModule(k, i, num, matrix, maskCode);
							num++;
						}
						if (i - 1 >= 0 && !this.FilledValuesMatrix[k, i - 1])
						{
							this.SetSingleModule(k, i - 1, num, matrix, maskCode);
							num++;
						}
					}
					flag = !flag;
				}
			}
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x00019484 File Offset: 0x00017684
		private void PopulateFormatData(string maskCode)
		{
			string mask = QRCode.errorCorrectionToMask[this.errorCorrectionLevel] + maskCode;
			int lastDataRowIndex = this.sizeOfMatrix - 5;
			int lastDataColumnIndex = this.sizeOfMatrix - 5;
			this.PopulateBinaryMatrix(mask, lastDataRowIndex, lastDataColumnIndex);
			this.PopulateFilledValuesMatrix();
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x000194CC File Offset: 0x000176CC
		private void PopulateFilledValuesMatrix()
		{
			this.FilledValuesMatrix[8, 0] = true;
			this.FilledValuesMatrix[8, 1] = true;
			this.FilledValuesMatrix[8, 2] = true;
			this.FilledValuesMatrix[8, 3] = true;
			this.FilledValuesMatrix[8, 4] = true;
			this.FilledValuesMatrix[8, 5] = true;
			this.FilledValuesMatrix[8, 7] = true;
			this.FilledValuesMatrix[8, 8] = true;
			this.FilledValuesMatrix[7, 8] = true;
			this.FilledValuesMatrix[5, 8] = true;
			this.FilledValuesMatrix[4, 8] = true;
			this.FilledValuesMatrix[3, 8] = true;
			this.FilledValuesMatrix[2, 8] = true;
			this.FilledValuesMatrix[1, 8] = true;
			this.FilledValuesMatrix[0, 8] = true;
			this.FilledValuesMatrix[this.sizeOfData - 1, 8] = true;
			this.FilledValuesMatrix[this.sizeOfData - 2, 8] = true;
			this.FilledValuesMatrix[this.sizeOfData - 3, 8] = true;
			this.FilledValuesMatrix[this.sizeOfData - 4, 8] = true;
			this.FilledValuesMatrix[this.sizeOfData - 5, 8] = true;
			this.FilledValuesMatrix[this.sizeOfData - 6, 8] = true;
			this.FilledValuesMatrix[this.sizeOfData - 7, 8] = true;
			this.FilledValuesMatrix[8, this.sizeOfData - 8] = true;
			this.FilledValuesMatrix[8, this.sizeOfData - 7] = true;
			this.FilledValuesMatrix[8, this.sizeOfData - 6] = true;
			this.FilledValuesMatrix[8, this.sizeOfData - 5] = true;
			this.FilledValuesMatrix[8, this.sizeOfData - 4] = true;
			this.FilledValuesMatrix[8, this.sizeOfData - 3] = true;
			this.FilledValuesMatrix[8, this.sizeOfData - 2] = true;
			this.FilledValuesMatrix[8, this.sizeOfData - 1] = true;
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x000196E6 File Offset: 0x000178E6
		private void PopulateBinaryMatrix(string mask, int lastDataRowIndex, int lastDataColumnIndex)
		{
			this.PopulateBinaryMatrixBeginning(mask);
			this.PopulateBinaryMatrixEnd(mask, lastDataRowIndex, lastDataColumnIndex);
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x000196F8 File Offset: 0x000178F8
		[SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow")]
		private void PopulateBinaryMatrixEnd(string mask, int lastDataRowIndex, int lastDataColumnIndex)
		{
			string text = QRCode.formatInformation[mask];
			this.BinaryMatrix[lastDataRowIndex, 12] = !(text[0].ToString() == "0");
			this.BinaryMatrix[lastDataRowIndex - 1, 12] = !(text[1].ToString() == "0");
			this.BinaryMatrix[lastDataRowIndex - 2, 12] = !(text[2].ToString() == "0");
			this.BinaryMatrix[lastDataRowIndex - 3, 12] = !(text[3].ToString() == "0");
			this.BinaryMatrix[lastDataRowIndex - 4, 12] = !(text[4].ToString() == "0");
			this.BinaryMatrix[lastDataRowIndex - 5, 12] = !(text[5].ToString() == "0");
			this.BinaryMatrix[lastDataRowIndex - 6, 12] = !(text[6].ToString() == "0");
			this.BinaryMatrix[12, lastDataColumnIndex - 7] = !(text[7].ToString() == "0");
			this.BinaryMatrix[12, lastDataColumnIndex - 6] = !(text[8].ToString() == "0");
			this.BinaryMatrix[12, lastDataColumnIndex - 5] = !(text[9].ToString() == "0");
			this.BinaryMatrix[12, lastDataColumnIndex - 4] = !(text[10].ToString() == "0");
			this.BinaryMatrix[12, lastDataColumnIndex - 3] = !(text[11].ToString() == "0");
			this.BinaryMatrix[12, lastDataColumnIndex - 2] = !(text[12].ToString() == "0");
			this.BinaryMatrix[12, lastDataColumnIndex - 1] = !(text[13].ToString() == "0");
			this.BinaryMatrix[12, lastDataColumnIndex] = !(text[14].ToString() == "0");
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x000199E0 File Offset: 0x00017BE0
		private void PopulateBinaryMatrixBeginning(string mask)
		{
			string text = QRCode.formatInformation[mask];
			this.BinaryMatrix[12, 4] = !(text[0].ToString() == "0");
			this.BinaryMatrix[12, 5] = !(text[1].ToString() == "0");
			this.BinaryMatrix[12, 6] = !(text[2].ToString() == "0");
			this.BinaryMatrix[12, 7] = !(text[3].ToString() == "0");
			this.BinaryMatrix[12, 8] = !(text[4].ToString() == "0");
			this.BinaryMatrix[12, 9] = !(text[5].ToString() == "0");
			this.BinaryMatrix[12, 11] = !(text[6].ToString() == "0");
			this.BinaryMatrix[12, 12] = !(text[7].ToString() == "0");
			this.BinaryMatrix[11, 12] = !(text[8].ToString() == "0");
			this.BinaryMatrix[9, 12] = !(text[9].ToString() == "0");
			this.BinaryMatrix[8, 12] = !(text[10].ToString() == "0");
			this.BinaryMatrix[7, 12] = !(text[11].ToString() == "0");
			this.BinaryMatrix[6, 12] = !(text[12].ToString() == "0");
			this.BinaryMatrix[5, 12] = !(text[13].ToString() == "0");
			this.BinaryMatrix[4, 12] = !(text[14].ToString() == "0");
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x00019CB4 File Offset: 0x00017EB4
		private void PopulateValueMatrix()
		{
			this.BinaryMatrix = new bool[this.sizeOfMatrix, this.sizeOfMatrix];
			this.FilledValuesMatrix = new bool[this.sizeOfData, this.sizeOfData];
			this.PopulateFinderPattern(4, 4);
			this.PopulateFinderPatternFilledValues(0, 0);
			this.PopulateFinderPattern(4, this.sizeOfMatrix - 11);
			this.PopulateFinderPatternFilledValues(0, this.sizeOfData - 8);
			this.PopulateFinderPattern(this.sizeOfMatrix - 11, 4);
			this.PopulateFinderPatternFilledValues(this.sizeOfData - 8, 0);
			this.AddTimingPattern();
			this.AddSinglePixel(this.sizeOfMatrix - 12, 12);
			if (this.version > 1)
			{
				this.AddPositionAdjustmentPatterns(this.sizeOfMatrix);
			}
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x00019D6C File Offset: 0x00017F6C
		[SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow")]
		private void AddPositionAdjustmentPatterns(int currentSizeOfMatrix)
		{
			List<int> list = QRCode.positionAdjustmentPatternCoordinates[this.version];
			for (int i = 0; i < list.Count; i++)
			{
				for (int j = 0; j < list.Count; j++)
				{
					int num = list[i] + 4;
					int num2 = list[j] + 4;
					if ((num != 10 || num2 != 10) && (num != 10 || num2 != currentSizeOfMatrix - 11) && (num != currentSizeOfMatrix - 11 || num2 != 10))
					{
						this.AddSingleAdjustmentPattern(num, num2);
					}
				}
			}
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x00019DF0 File Offset: 0x00017FF0
		[SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow")]
		private void AddSingleAdjustmentPattern(int row, int column)
		{
			this.BinaryMatrix[row - 2, column - 2] = true;
			this.FilledValuesMatrix[row - 6, column - 6] = true;
			this.BinaryMatrix[row - 2, column - 1] = true;
			this.FilledValuesMatrix[row - 6, column - 5] = true;
			this.BinaryMatrix[row - 2, column] = true;
			this.FilledValuesMatrix[row - 6, column - 4] = true;
			this.BinaryMatrix[row - 2, column + 1] = true;
			this.FilledValuesMatrix[row - 6, column - 3] = true;
			this.BinaryMatrix[row - 2, column + 2] = true;
			this.FilledValuesMatrix[row - 6, column - 2] = true;
			this.BinaryMatrix[row - 1, column - 2] = true;
			this.FilledValuesMatrix[row - 5, column - 6] = true;
			this.BinaryMatrix[row - 1, column - 1] = false;
			this.FilledValuesMatrix[row - 5, column - 5] = true;
			this.BinaryMatrix[row - 1, column] = false;
			this.FilledValuesMatrix[row - 5, column - 4] = true;
			this.BinaryMatrix[row - 1, column + 1] = false;
			this.FilledValuesMatrix[row - 5, column - 3] = true;
			this.BinaryMatrix[row - 1, column + 2] = true;
			this.FilledValuesMatrix[row - 5, column - 2] = true;
			this.BinaryMatrix[row, column - 2] = true;
			this.FilledValuesMatrix[row - 4, column - 6] = true;
			this.BinaryMatrix[row, column - 1] = false;
			this.FilledValuesMatrix[row - 4, column - 5] = true;
			this.BinaryMatrix[row, column] = true;
			this.FilledValuesMatrix[row - 4, column - 4] = true;
			this.BinaryMatrix[row, column + 1] = false;
			this.FilledValuesMatrix[row - 4, column - 3] = true;
			this.BinaryMatrix[row, column + 2] = true;
			this.FilledValuesMatrix[row - 4, column - 2] = true;
			this.BinaryMatrix[row + 1, column - 2] = true;
			this.FilledValuesMatrix[row - 3, column - 6] = true;
			this.BinaryMatrix[row + 1, column - 1] = false;
			this.FilledValuesMatrix[row - 3, column - 5] = true;
			this.BinaryMatrix[row + 1, column] = false;
			this.FilledValuesMatrix[row - 3, column - 4] = true;
			this.BinaryMatrix[row + 1, column + 1] = false;
			this.FilledValuesMatrix[row - 3, column - 3] = true;
			this.BinaryMatrix[row + 1, column + 2] = true;
			this.FilledValuesMatrix[row - 3, column - 2] = true;
			this.BinaryMatrix[row + 2, column - 2] = true;
			this.FilledValuesMatrix[row - 2, column - 6] = true;
			this.BinaryMatrix[row + 2, column - 1] = true;
			this.FilledValuesMatrix[row - 2, column - 5] = true;
			this.BinaryMatrix[row + 2, column] = true;
			this.FilledValuesMatrix[row - 2, column - 4] = true;
			this.BinaryMatrix[row + 2, column + 1] = true;
			this.FilledValuesMatrix[row - 2, column - 3] = true;
			this.BinaryMatrix[row + 2, column + 2] = true;
			this.FilledValuesMatrix[row - 2, column - 2] = true;
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x0001A16D File Offset: 0x0001836D
		[SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow")]
		private void AddSinglePixel(int row, int column)
		{
			this.BinaryMatrix[row, column] = true;
			this.FilledValuesMatrix[row - 4, column - 4] = true;
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x0001A190 File Offset: 0x00018390
		[SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow")]
		private void PopulateFinderPatternFilledValues(int startRow, int startColumn)
		{
			int num2;
			int num;
			if (startRow == 0 && startColumn == 0)
			{
				num = (num2 = 8);
			}
			else if (startRow == 0 && startColumn > 0)
			{
				num2 = 8;
				num = startColumn + 8;
			}
			else
			{
				num = 8;
				num2 = startRow + 8;
			}
			for (int i = startRow; i < num2; i++)
			{
				for (int j = startColumn; j < num; j++)
				{
					this.FilledValuesMatrix[i, j] = true;
				}
			}
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x0001A1E4 File Offset: 0x000183E4
		[SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow")]
		private void PopulateFinderPattern(int initialRowIndex, int initialColumnIndex)
		{
			for (int i = 0; i < 7; i++)
			{
				this.BinaryMatrix[initialRowIndex + i, initialColumnIndex] = true;
				this.BinaryMatrix[initialRowIndex, initialColumnIndex + i] = true;
				this.BinaryMatrix[initialRowIndex + i, initialColumnIndex + 6] = true;
				this.BinaryMatrix[initialRowIndex + 6, initialColumnIndex + i] = true;
			}
			int num = initialRowIndex + 2;
			int num2 = initialColumnIndex + 2;
			for (int j = 0; j < 3; j++)
			{
				this.BinaryMatrix[num + j, num2] = true;
				this.BinaryMatrix[num + j, num2 + 1] = true;
				this.BinaryMatrix[num + j, num2 + 2] = true;
			}
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x0001A290 File Offset: 0x00018490
		[SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow")]
		private void AddTimingPattern()
		{
			int i = 10;
			int j = 12;
			bool flag = true;
			while (j < this.VersionDimension - 4)
			{
				if (flag)
				{
					this.BinaryMatrix[i, j] = true;
				}
				this.FilledValuesMatrix[i - 4, j - 4] = true;
				flag = !flag;
				j++;
			}
			j = 10;
			i = 12;
			flag = true;
			while (i < this.VersionDimension - 4)
			{
				if (flag)
				{
					this.BinaryMatrix[i, j] = true;
				}
				this.FilledValuesMatrix[i - 4, j - 4] = true;
				flag = !flag;
				i++;
			}
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x0001A31F File Offset: 0x0001851F
		private int DetermineCountLength()
		{
			return this.DetermineCountLength(this.version);
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x0001A330 File Offset: 0x00018530
		private int DetermineCountLength(int codeVersion)
		{
			if (codeVersion >= 1 && codeVersion <= 9)
			{
				if (this.codeMode == Modes.CodeMode.Numeric)
				{
					return 10;
				}
				if (this.codeMode == Modes.CodeMode.Alphanumeric)
				{
					return 9;
				}
				return 8;
			}
			else if (codeVersion >= 10 && codeVersion <= 26)
			{
				if (this.codeMode == Modes.CodeMode.Numeric)
				{
					return 12;
				}
				if (this.codeMode == Modes.CodeMode.Alphanumeric)
				{
					return 11;
				}
				if (this.codeMode == Modes.CodeMode.Byte)
				{
					return 16;
				}
				return 10;
			}
			else
			{
				if (this.codeMode == Modes.CodeMode.Numeric)
				{
					return 14;
				}
				if (this.codeMode == Modes.CodeMode.Alphanumeric)
				{
					return 13;
				}
				if (this.codeMode == Modes.CodeMode.Byte)
				{
					return 16;
				}
				return 12;
			}
		}

		// Token: 0x04000111 RID: 273
		private const int QuietZone = 4;

		// Token: 0x04000112 RID: 274
		private const string ECIModeIndicator = "0111";

		// Token: 0x04000113 RID: 275
		private const string FNC1FirstPositionIndicator = "0101";

		// Token: 0x04000114 RID: 276
		private const string FNC1SecondPositionIndicator = "1001";

		// Token: 0x04000115 RID: 277
		private static List<bool> version7 = new List<bool>(18)
		{
			false,
			false,
			true,
			false,
			true,
			false,
			false,
			true,
			false,
			false,
			true,
			true,
			true,
			true,
			true,
			false,
			false,
			false
		};

		// Token: 0x04000116 RID: 278
		private static List<bool> version8 = new List<bool>(18)
		{
			false,
			false,
			true,
			true,
			true,
			true,
			false,
			true,
			true,
			false,
			true,
			false,
			false,
			false,
			false,
			true,
			false,
			false
		};

		// Token: 0x04000117 RID: 279
		private static List<bool> version9 = new List<bool>(18)
		{
			true,
			false,
			false,
			true,
			true,
			false,
			false,
			true,
			false,
			true,
			false,
			true,
			true,
			false,
			false,
			true,
			false,
			false
		};

		// Token: 0x04000118 RID: 280
		private static List<bool> version10 = new List<bool>(18)
		{
			true,
			true,
			false,
			false,
			true,
			false,
			true,
			true,
			false,
			false,
			true,
			false,
			false,
			true,
			false,
			true,
			false,
			false
		};

		// Token: 0x04000119 RID: 281
		private static List<bool> version11 = new List<bool>(18)
		{
			false,
			true,
			true,
			false,
			true,
			true,
			true,
			true,
			true,
			true,
			false,
			true,
			true,
			true,
			false,
			true,
			false,
			false
		};

		// Token: 0x0400011A RID: 282
		private static List<bool> version12 = new List<bool>(18)
		{
			false,
			true,
			false,
			false,
			false,
			true,
			true,
			false,
			true,
			true,
			true,
			false,
			false,
			false,
			true,
			true,
			false,
			false
		};

		// Token: 0x0400011B RID: 283
		private static List<bool> version13 = new List<bool>(18)
		{
			true,
			true,
			true,
			false,
			false,
			false,
			true,
			false,
			false,
			false,
			false,
			true,
			true,
			false,
			true,
			true,
			false,
			false
		};

		// Token: 0x0400011C RID: 284
		private static List<bool> version14 = new List<bool>(18)
		{
			true,
			false,
			true,
			true,
			false,
			false,
			false,
			false,
			false,
			true,
			true,
			false,
			false,
			true,
			true,
			true,
			false,
			false
		};

		// Token: 0x0400011D RID: 285
		private static List<bool> version15 = new List<bool>(18)
		{
			false,
			false,
			false,
			true,
			false,
			true,
			false,
			false,
			true,
			false,
			false,
			true,
			true,
			true,
			true,
			true,
			false,
			false
		};

		// Token: 0x0400011E RID: 286
		private static List<bool> version16 = new List<bool>(18)
		{
			false,
			false,
			false,
			true,
			true,
			true,
			true,
			false,
			true,
			true,
			false,
			true,
			false,
			false,
			false,
			false,
			true,
			false
		};

		// Token: 0x0400011F RID: 287
		private static List<bool> version17 = new List<bool>(18)
		{
			true,
			false,
			true,
			true,
			true,
			false,
			true,
			false,
			false,
			false,
			true,
			false,
			true,
			false,
			false,
			false,
			true,
			false
		};

		// Token: 0x04000120 RID: 288
		private static List<bool> version18 = new List<bool>(18)
		{
			true,
			true,
			true,
			false,
			true,
			false,
			false,
			false,
			false,
			true,
			false,
			true,
			false,
			true,
			false,
			false,
			true,
			false
		};

		// Token: 0x04000121 RID: 289
		private static List<bool> version19 = new List<bool>(18)
		{
			false,
			true,
			false,
			false,
			true,
			true,
			false,
			false,
			true,
			false,
			true,
			false,
			true,
			true,
			false,
			false,
			true,
			false
		};

		// Token: 0x04000122 RID: 290
		private static List<bool> version20 = new List<bool>(18)
		{
			false,
			true,
			true,
			false,
			false,
			true,
			false,
			true,
			true,
			false,
			false,
			true,
			false,
			false,
			true,
			false,
			true,
			false
		};

		// Token: 0x04000123 RID: 291
		private static List<bool> version21 = new List<bool>(18)
		{
			true,
			true,
			false,
			false,
			false,
			false,
			false,
			true,
			false,
			true,
			true,
			false,
			true,
			false,
			true,
			false,
			true,
			false
		};

		// Token: 0x04000124 RID: 292
		private static List<bool> version22 = new List<bool>(18)
		{
			true,
			false,
			false,
			true,
			false,
			false,
			true,
			true,
			false,
			false,
			false,
			true,
			false,
			true,
			true,
			false,
			true,
			false
		};

		// Token: 0x04000125 RID: 293
		private static List<bool> version23 = new List<bool>(18)
		{
			false,
			false,
			true,
			true,
			false,
			true,
			true,
			true,
			true,
			true,
			true,
			false,
			true,
			true,
			true,
			false,
			true,
			false
		};

		// Token: 0x04000126 RID: 294
		private static List<bool> version24 = new List<bool>(18)
		{
			false,
			false,
			true,
			false,
			false,
			false,
			true,
			true,
			false,
			true,
			true,
			true,
			false,
			false,
			false,
			true,
			true,
			false
		};

		// Token: 0x04000127 RID: 295
		private static List<bool> version25 = new List<bool>(18)
		{
			true,
			false,
			false,
			false,
			false,
			true,
			true,
			true,
			true,
			false,
			false,
			false,
			true,
			false,
			false,
			true,
			true,
			false
		};

		// Token: 0x04000128 RID: 296
		private static List<bool> version26 = new List<bool>(18)
		{
			true,
			true,
			false,
			true,
			false,
			true,
			false,
			true,
			true,
			true,
			true,
			true,
			false,
			true,
			false,
			true,
			true,
			false
		};

		// Token: 0x04000129 RID: 297
		private static List<bool> version27 = new List<bool>(18)
		{
			false,
			true,
			true,
			true,
			false,
			false,
			false,
			true,
			false,
			false,
			false,
			false,
			true,
			true,
			false,
			true,
			true,
			false
		};

		// Token: 0x0400012A RID: 298
		private static List<bool> version28 = new List<bool>(18)
		{
			false,
			true,
			false,
			true,
			true,
			false,
			false,
			false,
			false,
			false,
			true,
			true,
			false,
			false,
			true,
			true,
			true,
			false
		};

		// Token: 0x0400012B RID: 299
		private static List<bool> version29 = new List<bool>(18)
		{
			true,
			true,
			true,
			true,
			true,
			true,
			false,
			false,
			true,
			true,
			false,
			false,
			true,
			false,
			true,
			true,
			true,
			false
		};

		// Token: 0x0400012C RID: 300
		private static List<bool> version30 = new List<bool>(18)
		{
			true,
			false,
			true,
			false,
			true,
			true,
			true,
			false,
			true,
			false,
			true,
			true,
			false,
			true,
			true,
			true,
			true,
			false
		};

		// Token: 0x0400012D RID: 301
		private static List<bool> version31 = new List<bool>(18)
		{
			false,
			false,
			false,
			false,
			true,
			false,
			true,
			false,
			false,
			true,
			false,
			false,
			true,
			true,
			true,
			true,
			true,
			false
		};

		// Token: 0x0400012E RID: 302
		private static List<bool> version32 = new List<bool>(18)
		{
			true,
			false,
			true,
			false,
			true,
			false,
			true,
			true,
			true,
			false,
			false,
			true,
			false,
			false,
			false,
			false,
			false,
			true
		};

		// Token: 0x0400012F RID: 303
		private static List<bool> version33 = new List<bool>(18)
		{
			false,
			false,
			false,
			false,
			true,
			true,
			true,
			true,
			false,
			true,
			true,
			false,
			true,
			false,
			false,
			false,
			false,
			true
		};

		// Token: 0x04000130 RID: 304
		private static List<bool> version34 = new List<bool>(18)
		{
			false,
			true,
			false,
			true,
			true,
			true,
			false,
			true,
			false,
			false,
			false,
			true,
			false,
			true,
			false,
			false,
			false,
			true
		};

		// Token: 0x04000131 RID: 305
		private static List<bool> version35 = new List<bool>(18)
		{
			true,
			true,
			true,
			true,
			true,
			false,
			false,
			true,
			true,
			true,
			true,
			false,
			true,
			true,
			false,
			false,
			false,
			true
		};

		// Token: 0x04000132 RID: 306
		private static List<bool> version36 = new List<bool>(18)
		{
			true,
			true,
			false,
			true,
			false,
			false,
			false,
			false,
			true,
			true,
			false,
			true,
			false,
			false,
			true,
			false,
			false,
			true
		};

		// Token: 0x04000133 RID: 307
		private static List<bool> version37 = new List<bool>(18)
		{
			false,
			true,
			true,
			true,
			false,
			true,
			false,
			false,
			false,
			false,
			true,
			false,
			true,
			false,
			true,
			false,
			false,
			true
		};

		// Token: 0x04000134 RID: 308
		private static List<bool> version38 = new List<bool>(18)
		{
			false,
			false,
			true,
			false,
			false,
			true,
			true,
			false,
			false,
			true,
			false,
			true,
			false,
			true,
			true,
			false,
			false,
			true
		};

		// Token: 0x04000135 RID: 309
		private static List<bool> version39 = new List<bool>(18)
		{
			true,
			false,
			false,
			false,
			false,
			false,
			true,
			false,
			true,
			false,
			true,
			false,
			true,
			true,
			true,
			false,
			false,
			true
		};

		// Token: 0x04000136 RID: 310
		private static List<bool> version40 = new List<bool>(18)
		{
			true,
			false,
			false,
			true,
			false,
			true,
			true,
			false,
			false,
			false,
			true,
			true,
			false,
			false,
			false,
			true,
			false,
			true
		};

		// Token: 0x04000137 RID: 311
		private static Dictionary<int, List<bool>> positionValues = new Dictionary<int, List<bool>>();

		// Token: 0x04000138 RID: 312
		private static Dictionary<DataCapacityIndexer, int> dataCapacityTable = new Dictionary<DataCapacityIndexer, int>();

		// Token: 0x04000139 RID: 313
		private static Dictionary<Modes.CodeMode, string> codeModeValues = new Dictionary<Modes.CodeMode, string>();

		// Token: 0x0400013A RID: 314
		private static Dictionary<string, CodeWordsBlockInfo> codeWordsLengthTable = new Dictionary<string, CodeWordsBlockInfo>();

		// Token: 0x0400013B RID: 315
		private static List<int> exponentsOfAlphaToValues;

		// Token: 0x0400013C RID: 316
		private static List<int> valuesToExponentsOfAlpha;

		// Token: 0x0400013D RID: 317
		private static Dictionary<int, List<int>> generatorExponentsOfAlpha;

		// Token: 0x0400013E RID: 318
		private static Dictionary<int, List<int>> positionAdjustmentPatternCoordinates;

		// Token: 0x0400013F RID: 319
		private static Dictionary<Modes.ErrorCorrectionLevel, string> errorCorrectionToMask;

		// Token: 0x04000140 RID: 320
		private static Dictionary<string, string> formatInformation;

		// Token: 0x04000141 RID: 321
		private static List<string> maskCodes = new List<string>
		{
			"000",
			"001",
			"010",
			"011",
			"100",
			"101",
			"110",
			"111"
		};

		// Token: 0x04000142 RID: 322
		private Modes.CodeMode codeMode;

		// Token: 0x04000143 RID: 323
		private int version;

		// Token: 0x04000144 RID: 324
		private Modes.ErrorCorrectionLevel errorCorrectionLevel;

		// Token: 0x04000145 RID: 325
		private Modes.ECIMode eciMode;

		// Token: 0x04000146 RID: 326
		private Modes.FNC1Mode fnc1Mode;

		// Token: 0x04000147 RID: 327
		private string applicationIndicator;

		// Token: 0x04000148 RID: 328
		private bool[,] binaryMatrix;

		// Token: 0x04000149 RID: 329
		private bool[,] filledValuesMatrix;

		// Token: 0x0400014A RID: 330
		private string encodedData;

		// Token: 0x0400014B RID: 331
		private CodeWordsBlockInfo info;

		// Token: 0x0400014C RID: 332
		private Dictionary<int, string> binaryValues;

		// Token: 0x0400014D RID: 333
		private Dictionary<int, string> dataResult;

		// Token: 0x0400014E RID: 334
		private int sizeOfMatrix;

		// Token: 0x0400014F RID: 335
		private int sizeOfData;

		// Token: 0x04000150 RID: 336
		private bool autoSetVersion;
	}
}
