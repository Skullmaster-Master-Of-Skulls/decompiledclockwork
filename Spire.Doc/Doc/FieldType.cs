using System;

namespace Spire.Doc
{
	// Token: 0x020000A8 RID: 168
	public enum FieldType
	{
		// Token: 0x04000A01 RID: 2561
		FieldNone,
		// Token: 0x04000A02 RID: 2562
		FieldAddin = 81,
		// Token: 0x04000A03 RID: 2563
		FieldAdvance = 84,
		// Token: 0x04000A04 RID: 2564
		FieldAsk = 38,
		// Token: 0x04000A05 RID: 2565
		FieldAuthor = 17,
		// Token: 0x04000A06 RID: 2566
		FieldAutoNum = 54,
		// Token: 0x04000A07 RID: 2567
		FieldAutoNumLegal = 53,
		// Token: 0x04000A08 RID: 2568
		FieldAutoNumOutline = 52,
		// Token: 0x04000A09 RID: 2569
		FieldAutoText = 79,
		// Token: 0x04000A0A RID: 2570
		FieldAutoTextList = 89,
		// Token: 0x04000A0B RID: 2571
		FieldBarCode = 63,
		// Token: 0x04000A0C RID: 2572
		FieldComments = 19,
		// Token: 0x04000A0D RID: 2573
		FieldCompare = 80,
		// Token: 0x04000A0E RID: 2574
		FieldCreateDate = 21,
		// Token: 0x04000A0F RID: 2575
		FieldData = 40,
		// Token: 0x04000A10 RID: 2576
		FieldDatabase = 78,
		// Token: 0x04000A11 RID: 2577
		FieldDate = 31,
		// Token: 0x04000A12 RID: 2578
		FieldDDE = 45,
		// Token: 0x04000A13 RID: 2579
		FieldDDEAuto,
		// Token: 0x04000A14 RID: 2580
		FieldDocProperty = 85,
		// Token: 0x04000A15 RID: 2581
		FieldDocVariable = 64,
		// Token: 0x04000A16 RID: 2582
		FieldEditTime = 25,
		// Token: 0x04000A17 RID: 2583
		FieldEmbed = 58,
		// Token: 0x04000A18 RID: 2584
		FieldEmpty = -1,
		// Token: 0x04000A19 RID: 2585
		FieldExpression = 34,
		// Token: 0x04000A1A RID: 2586
		FieldFileName = 29,
		// Token: 0x04000A1B RID: 2587
		FieldFileSize = 69,
		// Token: 0x04000A1C RID: 2588
		FieldFillIn = 39,
		// Token: 0x04000A1D RID: 2589
		FieldFootnoteRef = 5,
		// Token: 0x04000A1E RID: 2590
		FieldFormCheckBox = 71,
		// Token: 0x04000A1F RID: 2591
		FieldFormDropDown = 83,
		// Token: 0x04000A20 RID: 2592
		FieldFormTextInput = 70,
		// Token: 0x04000A21 RID: 2593
		FieldFormula = 49,
		// Token: 0x04000A22 RID: 2594
		FieldGlossary = 47,
		// Token: 0x04000A23 RID: 2595
		FieldGoToButton = 50,
		// Token: 0x04000A24 RID: 2596
		FieldHTMLActiveX = 91,
		// Token: 0x04000A25 RID: 2597
		FieldHyperlink = 88,
		// Token: 0x04000A26 RID: 2598
		FieldIf = 7,
		// Token: 0x04000A27 RID: 2599
		FieldImport = 55,
		// Token: 0x04000A28 RID: 2600
		FieldInclude = 36,
		// Token: 0x04000A29 RID: 2601
		FieldIncludePicture = 67,
		// Token: 0x04000A2A RID: 2602
		FieldIncludeText,
		// Token: 0x04000A2B RID: 2603
		FieldIndex = 8,
		// Token: 0x04000A2C RID: 2604
		FieldIndexEntry = 4,
		// Token: 0x04000A2D RID: 2605
		FieldInfo = 14,
		// Token: 0x04000A2E RID: 2606
		FieldKeyWord = 18,
		// Token: 0x04000A2F RID: 2607
		FieldLastSavedBy = 20,
		// Token: 0x04000A30 RID: 2608
		FieldLink = 56,
		// Token: 0x04000A31 RID: 2609
		FieldListNum = 90,
		// Token: 0x04000A32 RID: 2610
		FieldMacroButton = 51,
		// Token: 0x04000A33 RID: 2611
		FieldMergeField = 59,
		// Token: 0x04000A34 RID: 2612
		FieldMergeRec = 44,
		// Token: 0x04000A35 RID: 2613
		FieldMergeSeq = 75,
		// Token: 0x04000A36 RID: 2614
		FieldNext = 41,
		// Token: 0x04000A37 RID: 2615
		FieldNextIf,
		// Token: 0x04000A38 RID: 2616
		FieldNoteRef = 72,
		// Token: 0x04000A39 RID: 2617
		FieldNumChars = 28,
		// Token: 0x04000A3A RID: 2618
		FieldNumPages = 26,
		// Token: 0x04000A3B RID: 2619
		FieldNumWords,
		// Token: 0x04000A3C RID: 2620
		FieldOCX = 87,
		// Token: 0x04000A3D RID: 2621
		FieldPage = 33,
		// Token: 0x04000A3E RID: 2622
		FieldPageRef = 37,
		// Token: 0x04000A3F RID: 2623
		FieldPrint = 48,
		// Token: 0x04000A40 RID: 2624
		FieldPrintDate = 23,
		// Token: 0x04000A41 RID: 2625
		FieldPrivate = 77,
		// Token: 0x04000A42 RID: 2626
		FieldQuote = 35,
		// Token: 0x04000A43 RID: 2627
		FieldRef = 3,
		// Token: 0x04000A44 RID: 2628
		FieldRefDoc = 11,
		// Token: 0x04000A45 RID: 2629
		FieldRevisionNum = 24,
		// Token: 0x04000A46 RID: 2630
		FieldSaveDate = 22,
		// Token: 0x04000A47 RID: 2631
		FieldSection = 65,
		// Token: 0x04000A48 RID: 2632
		FieldSectionPages,
		// Token: 0x04000A49 RID: 2633
		FieldSequence = 12,
		// Token: 0x04000A4A RID: 2634
		FieldSet = 6,
		// Token: 0x04000A4B RID: 2635
		FieldSkipIf = 43,
		// Token: 0x04000A4C RID: 2636
		FieldStyleRef = 10,
		// Token: 0x04000A4D RID: 2637
		FieldSubject = 16,
		// Token: 0x04000A4E RID: 2638
		FieldSubscriber = 82,
		// Token: 0x04000A4F RID: 2639
		FieldSymbol = 57,
		// Token: 0x04000A50 RID: 2640
		FieldTemplate = 30,
		// Token: 0x04000A51 RID: 2641
		FieldTime = 32,
		// Token: 0x04000A52 RID: 2642
		FieldTitle = 15,
		// Token: 0x04000A53 RID: 2643
		FieldTOA = 73,
		// Token: 0x04000A54 RID: 2644
		FieldTOAEntry,
		// Token: 0x04000A55 RID: 2645
		FieldTOC = 13,
		// Token: 0x04000A56 RID: 2646
		FieldTOCEntry = 9,
		// Token: 0x04000A57 RID: 2647
		FieldUserAddress = 62,
		// Token: 0x04000A58 RID: 2648
		FieldUserInitials = 61,
		// Token: 0x04000A59 RID: 2649
		FieldUserName = 60,
		// Token: 0x04000A5A RID: 2650
		FieldShape = 95,
		// Token: 0x04000A5B RID: 2651
		FieldBidiOutline = 92,
		// Token: 0x04000A5C RID: 2652
		FieldAddressBlock,
		// Token: 0x04000A5D RID: 2653
		FieldUnknown = 1000
	}
}
