using System;
using System.Collections;
using System.Drawing;

namespace Spire.Xls.Core
{
	// Token: 0x020001E0 RID: 480
	public interface IXLSRange : IExcelApplication, IEnumerable
	{
		// Token: 0x170009D7 RID: 2519
		// (get) Token: 0x06001A83 RID: 6787
		string RangeAddress { get; }

		// Token: 0x170009D8 RID: 2520
		// (get) Token: 0x06001A84 RID: 6788
		string RangeAddressLocal { get; }

		// Token: 0x170009D9 RID: 2521
		// (get) Token: 0x06001A85 RID: 6789
		string RangeGlobalAddress { get; }

		// Token: 0x170009DA RID: 2522
		// (get) Token: 0x06001A86 RID: 6790
		string RangeR1C1Address { get; }

		// Token: 0x170009DB RID: 2523
		// (get) Token: 0x06001A87 RID: 6791
		string RangeR1C1AddressLocal { get; }

		// Token: 0x170009DC RID: 2524
		// (get) Token: 0x06001A88 RID: 6792
		// (set) Token: 0x06001A89 RID: 6793
		bool BooleanValue { get; set; }

		// Token: 0x170009DD RID: 2525
		// (get) Token: 0x06001A8A RID: 6794
		IBorders Borders { get; }

		// Token: 0x170009DE RID: 2526
		// (get) Token: 0x06001A8B RID: 6795
		CellRange[] Cells { get; }

		// Token: 0x170009DF RID: 2527
		// (get) Token: 0x06001A8C RID: 6796
		int Column { get; }

		// Token: 0x170009E0 RID: 2528
		// (get) Token: 0x06001A8D RID: 6797
		int ColumnGroupLevel { get; }

		// Token: 0x170009E1 RID: 2529
		// (get) Token: 0x06001A8E RID: 6798
		// (set) Token: 0x06001A8F RID: 6799
		double ColumnWidth { get; set; }

		// Token: 0x170009E2 RID: 2530
		// (get) Token: 0x06001A90 RID: 6800
		int Count { get; }

		// Token: 0x170009E3 RID: 2531
		// (get) Token: 0x06001A91 RID: 6801
		// (set) Token: 0x06001A92 RID: 6802
		DateTime DateTimeValue { get; set; }

		// Token: 0x170009E4 RID: 2532
		// (get) Token: 0x06001A93 RID: 6803
		string NumberText { get; }

		// Token: 0x170009E5 RID: 2533
		// (get) Token: 0x06001A94 RID: 6804
		IXLSRange EndCell { get; }

		// Token: 0x170009E6 RID: 2534
		// (get) Token: 0x06001A95 RID: 6805
		IXLSRange EntireColumn { get; }

		// Token: 0x170009E7 RID: 2535
		// (get) Token: 0x06001A96 RID: 6806
		IXLSRange EntireRow { get; }

		// Token: 0x170009E8 RID: 2536
		// (get) Token: 0x06001A97 RID: 6807
		// (set) Token: 0x06001A98 RID: 6808
		string ErrorValue { get; set; }

		// Token: 0x170009E9 RID: 2537
		// (get) Token: 0x06001A99 RID: 6809
		// (set) Token: 0x06001A9A RID: 6810
		string Formula { get; set; }

		// Token: 0x170009EA RID: 2538
		// (get) Token: 0x06001A9B RID: 6811
		// (set) Token: 0x06001A9C RID: 6812
		string FormulaArray { get; set; }

		// Token: 0x170009EB RID: 2539
		// (get) Token: 0x06001A9D RID: 6813
		// (set) Token: 0x06001A9E RID: 6814
		string FormulaArrayR1C1 { get; set; }

		// Token: 0x170009EC RID: 2540
		// (get) Token: 0x06001A9F RID: 6815
		// (set) Token: 0x06001AA0 RID: 6816
		bool IsFormulaHidden { get; set; }

		// Token: 0x170009ED RID: 2541
		// (get) Token: 0x06001AA1 RID: 6817
		// (set) Token: 0x06001AA2 RID: 6818
		DateTime FormulaDateTime { get; set; }

		// Token: 0x170009EE RID: 2542
		// (get) Token: 0x06001AA3 RID: 6819
		// (set) Token: 0x06001AA4 RID: 6820
		string FormulaR1C1 { get; set; }

		// Token: 0x170009EF RID: 2543
		// (get) Token: 0x06001AA5 RID: 6821
		// (set) Token: 0x06001AA6 RID: 6822
		bool FormulaBoolValue { get; set; }

		// Token: 0x170009F0 RID: 2544
		// (get) Token: 0x06001AA7 RID: 6823
		// (set) Token: 0x06001AA8 RID: 6824
		string FormulaErrorValue { get; set; }

		// Token: 0x170009F1 RID: 2545
		// (get) Token: 0x06001AA9 RID: 6825
		bool HasDataValidation { get; }

		// Token: 0x170009F2 RID: 2546
		// (get) Token: 0x06001AAA RID: 6826
		bool HasBoolean { get; }

		// Token: 0x170009F3 RID: 2547
		// (get) Token: 0x06001AAB RID: 6827
		bool HasDateTime { get; }

		// Token: 0x170009F4 RID: 2548
		// (get) Token: 0x06001AAC RID: 6828
		bool HasFormula { get; }

		// Token: 0x170009F5 RID: 2549
		// (get) Token: 0x06001AAD RID: 6829
		bool HasFormulaArray { get; }

		// Token: 0x170009F6 RID: 2550
		// (get) Token: 0x06001AAE RID: 6830
		bool HasFormulaDateTime { get; }

		// Token: 0x170009F7 RID: 2551
		// (get) Token: 0x06001AAF RID: 6831
		bool HasFormulaNumberValue { get; }

		// Token: 0x170009F8 RID: 2552
		// (get) Token: 0x06001AB0 RID: 6832
		bool HasFormulaStringValue { get; }

		// Token: 0x170009F9 RID: 2553
		// (get) Token: 0x06001AB1 RID: 6833
		bool HasNumber { get; }

		// Token: 0x170009FA RID: 2554
		// (get) Token: 0x06001AB2 RID: 6834
		bool HasRichText { get; }

		// Token: 0x170009FB RID: 2555
		// (get) Token: 0x06001AB3 RID: 6835
		bool HasString { get; }

		// Token: 0x170009FC RID: 2556
		// (get) Token: 0x06001AB4 RID: 6836
		bool HasStyle { get; }

		// Token: 0x170009FD RID: 2557
		// (get) Token: 0x06001AB5 RID: 6837
		// (set) Token: 0x06001AB6 RID: 6838
		HorizontalAlignType HorizontalAlignment { get; set; }

		// Token: 0x170009FE RID: 2558
		// (get) Token: 0x06001AB7 RID: 6839
		// (set) Token: 0x06001AB8 RID: 6840
		int IndentLevel { get; set; }

		// Token: 0x170009FF RID: 2559
		// (get) Token: 0x06001AB9 RID: 6841
		bool IsBlank { get; }

		// Token: 0x17000A00 RID: 2560
		// (get) Token: 0x06001ABA RID: 6842
		bool HasError { get; }

		// Token: 0x17000A01 RID: 2561
		// (get) Token: 0x06001ABB RID: 6843
		bool IsGroupedByColumn { get; }

		// Token: 0x17000A02 RID: 2562
		// (get) Token: 0x06001ABC RID: 6844
		bool IsGroupedByRow { get; }

		// Token: 0x17000A03 RID: 2563
		// (get) Token: 0x06001ABD RID: 6845
		bool IsInitialized { get; }

		// Token: 0x17000A04 RID: 2564
		// (get) Token: 0x06001ABE RID: 6846
		// (set) Token: 0x06001ABF RID: 6847
		int LastColumn { get; set; }

		// Token: 0x17000A05 RID: 2565
		// (get) Token: 0x06001AC0 RID: 6848
		// (set) Token: 0x06001AC1 RID: 6849
		int LastRow { get; set; }

		// Token: 0x17000A06 RID: 2566
		// (get) Token: 0x06001AC2 RID: 6850
		// (set) Token: 0x06001AC3 RID: 6851
		double NumberValue { get; set; }

		// Token: 0x17000A07 RID: 2567
		// (get) Token: 0x06001AC4 RID: 6852
		// (set) Token: 0x06001AC5 RID: 6853
		string NumberFormat { get; set; }

		// Token: 0x17000A08 RID: 2568
		// (get) Token: 0x06001AC6 RID: 6854
		int Row { get; }

		// Token: 0x17000A09 RID: 2569
		// (get) Token: 0x06001AC7 RID: 6855
		int RowGroupLevel { get; }

		// Token: 0x17000A0A RID: 2570
		// (get) Token: 0x06001AC8 RID: 6856
		// (set) Token: 0x06001AC9 RID: 6857
		double RowHeight { get; set; }

		// Token: 0x17000A0B RID: 2571
		// (get) Token: 0x06001ACA RID: 6858
		IXLSRange[] Rows { get; }

		// Token: 0x17000A0C RID: 2572
		// (get) Token: 0x06001ACB RID: 6859
		IXLSRange[] Columns { get; }

		// Token: 0x17000A0D RID: 2573
		// (get) Token: 0x06001ACC RID: 6860
		// (set) Token: 0x06001ACD RID: 6861
		IStyle Style { get; set; }

		// Token: 0x17000A0E RID: 2574
		// (get) Token: 0x06001ACE RID: 6862
		// (set) Token: 0x06001ACF RID: 6863
		string CellStyleName { get; set; }

		// Token: 0x17000A0F RID: 2575
		// (get) Token: 0x06001AD0 RID: 6864
		// (set) Token: 0x06001AD1 RID: 6865
		string Text { get; set; }

		// Token: 0x17000A10 RID: 2576
		// (get) Token: 0x06001AD2 RID: 6866
		// (set) Token: 0x06001AD3 RID: 6867
		TimeSpan TimeSpanValue { get; set; }

		// Token: 0x17000A11 RID: 2577
		// (get) Token: 0x06001AD4 RID: 6868
		// (set) Token: 0x06001AD5 RID: 6869
		string Value { get; set; }

		// Token: 0x17000A12 RID: 2578
		// (get) Token: 0x06001AD6 RID: 6870
		string EnvalutedValue { get; }

		// Token: 0x17000A13 RID: 2579
		// (get) Token: 0x06001AD7 RID: 6871
		// (set) Token: 0x06001AD8 RID: 6872
		object Value2 { get; set; }

		// Token: 0x17000A14 RID: 2580
		// (get) Token: 0x06001AD9 RID: 6873
		// (set) Token: 0x06001ADA RID: 6874
		VerticalAlignType VerticalAlignment { get; set; }

		// Token: 0x17000A15 RID: 2581
		// (get) Token: 0x06001ADB RID: 6875
		IWorksheet Worksheet { get; }

		// Token: 0x17000A16 RID: 2582
		IXLSRange this[int row, int column]
		{
			get;
			set;
		}

		// Token: 0x17000A17 RID: 2583
		IXLSRange this[int row, int column, int lastRow, int lastColumn]
		{
			get;
		}

		// Token: 0x17000A18 RID: 2584
		IXLSRange this[string name]
		{
			get;
		}

		// Token: 0x17000A19 RID: 2585
		IXLSRange this[string name, bool IsR1C1Notation]
		{
			get;
		}

		// Token: 0x17000A1A RID: 2586
		// (get) Token: 0x06001AE1 RID: 6881
		ConditionalFormats ConditionalFormats { get; }

		// Token: 0x17000A1B RID: 2587
		// (get) Token: 0x06001AE2 RID: 6882
		Validation DataValidation { get; }

		// Token: 0x17000A1C RID: 2588
		// (get) Token: 0x06001AE3 RID: 6883
		// (set) Token: 0x06001AE4 RID: 6884
		string FormulaStringValue { get; set; }

		// Token: 0x17000A1D RID: 2589
		// (get) Token: 0x06001AE5 RID: 6885
		// (set) Token: 0x06001AE6 RID: 6886
		double FormulaNumberValue { get; set; }

		// Token: 0x17000A1E RID: 2590
		// (get) Token: 0x06001AE7 RID: 6887
		bool HasFormulaBoolValue { get; }

		// Token: 0x17000A1F RID: 2591
		// (get) Token: 0x06001AE8 RID: 6888
		bool HasFormulaErrorValue { get; }

		// Token: 0x17000A20 RID: 2592
		// (get) Token: 0x06001AE9 RID: 6889
		ICommentShape Comment { get; }

		// Token: 0x17000A21 RID: 2593
		// (get) Token: 0x06001AEA RID: 6890
		IRichTextString RichText { get; }

		// Token: 0x17000A22 RID: 2594
		// (get) Token: 0x06001AEB RID: 6891
		bool HasMerged { get; }

		// Token: 0x17000A23 RID: 2595
		// (get) Token: 0x06001AEC RID: 6892
		IXLSRange MergeArea { get; }

		// Token: 0x17000A24 RID: 2596
		// (get) Token: 0x06001AED RID: 6893
		// (set) Token: 0x06001AEE RID: 6894
		bool IsWrapText { get; set; }

		// Token: 0x17000A25 RID: 2597
		// (get) Token: 0x06001AEF RID: 6895
		bool HasExternalFormula { get; }

		// Token: 0x17000A26 RID: 2598
		// (get) Token: 0x06001AF0 RID: 6896
		// (set) Token: 0x06001AF1 RID: 6897
		IgnoreErrorType IgnoreErrorOptions { get; set; }

		// Token: 0x17000A27 RID: 2599
		// (get) Token: 0x06001AF2 RID: 6898
		// (set) Token: 0x06001AF3 RID: 6899
		bool? IsStringsPreserved { get; set; }

		// Token: 0x17000A28 RID: 2600
		// (get) Token: 0x06001AF4 RID: 6900
		// (set) Token: 0x06001AF5 RID: 6901
		BuiltInStyles? BuiltInStyle { get; set; }

		// Token: 0x17000A29 RID: 2601
		// (get) Token: 0x06001AF6 RID: 6902
		IHyperLinks Hyperlinks { get; }

		// Token: 0x06001AF7 RID: 6903
		IXLSRange Activate(bool scroll);

		// Token: 0x06001AF8 RID: 6904
		void Merge();

		// Token: 0x06001AF9 RID: 6905
		void Merge(bool clearCells);

		// Token: 0x06001AFA RID: 6906
		void UnMerge();

		// Token: 0x06001AFB RID: 6907
		void FreezePanes();

		// Token: 0x06001AFC RID: 6908
		void ClearContents();

		// Token: 0x06001AFD RID: 6909
		void Clear(ExcelClearOptions option);

		// Token: 0x06001AFE RID: 6910
		IXLSRange Intersect(IXLSRange range);

		// Token: 0x06001AFF RID: 6911
		IXLSRange Merge(IXLSRange range);

		// Token: 0x06001B00 RID: 6912
		void AutoFitRows();

		// Token: 0x06001B01 RID: 6913
		void AutoFitColumns();

		// Token: 0x06001B02 RID: 6914
		ICommentShape AddComment();

		// Token: 0x06001B03 RID: 6915
		void BorderAround();

		// Token: 0x06001B04 RID: 6916
		void BorderAround(LineStyleType borderLine);

		// Token: 0x06001B05 RID: 6917
		void BorderAround(LineStyleType borderLine, Color borderColor);

		// Token: 0x06001B06 RID: 6918
		void BorderAround(LineStyleType borderLine, ExcelColors borderColor);

		// Token: 0x06001B07 RID: 6919
		void BorderInside();

		// Token: 0x06001B08 RID: 6920
		void BorderInside(LineStyleType borderLine);

		// Token: 0x06001B09 RID: 6921
		void BorderInside(LineStyleType borderLine, Color borderColor);

		// Token: 0x06001B0A RID: 6922
		void BorderInside(LineStyleType borderLine, ExcelColors borderColor);

		// Token: 0x06001B0B RID: 6923
		void BorderNone();

		// Token: 0x06001B0C RID: 6924
		void CollapseGroup(GroupByType groupBy);

		// Token: 0x06001B0D RID: 6925
		void ExpandGroup(GroupByType groupBy);

		// Token: 0x06001B0E RID: 6926
		void ExpandGroup(GroupByType groupBy, ExpandCollapseFlags flags);
	}
}
