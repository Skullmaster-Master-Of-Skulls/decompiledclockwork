using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Core
{
	// Token: 0x020005D8 RID: 1496
	public interface IWorkbook : IExcelApplication
	{
		// Token: 0x17000D92 RID: 3474
		// (get) Token: 0x060058C9 RID: 22729
		IWorksheet ActiveSheet { get; }

		// Token: 0x17000D93 RID: 3475
		// (get) Token: 0x060058CA RID: 22730
		// (set) Token: 0x060058CB RID: 22731
		int ActiveSheetIndex { get; set; }

		// Token: 0x17000D94 RID: 3476
		// (get) Token: 0x060058CC RID: 22732
		IAddInFunctions AddInFunctions { get; }

		// Token: 0x17000D95 RID: 3477
		// (get) Token: 0x060058CD RID: 22733
		// (set) Token: 0x060058CE RID: 22734
		string Author { get; set; }

		// Token: 0x17000D96 RID: 3478
		// (get) Token: 0x060058CF RID: 22735
		// (set) Token: 0x060058D0 RID: 22736
		bool IsHScrollBarVisible { get; set; }

		// Token: 0x17000D97 RID: 3479
		// (get) Token: 0x060058D1 RID: 22737
		// (set) Token: 0x060058D2 RID: 22738
		bool IsVScrollBarVisible { get; set; }

		// Token: 0x17000D98 RID: 3480
		// (get) Token: 0x060058D3 RID: 22739
		IBuiltInDocumentProperties BuiltInDocumentProperties { get; }

		// Token: 0x17000D99 RID: 3481
		// (get) Token: 0x060058D4 RID: 22740
		// (set) Token: 0x060058D5 RID: 22741
		string CodeName { get; set; }

		// Token: 0x17000D9A RID: 3482
		// (get) Token: 0x060058D6 RID: 22742
		ICustomDocumentProperties CustomDocumentProperties { get; }

		// Token: 0x17000D9B RID: 3483
		// (get) Token: 0x060058D7 RID: 22743
		// (set) Token: 0x060058D8 RID: 22744
		bool Date1904 { get; set; }

		// Token: 0x17000D9C RID: 3484
		// (get) Token: 0x060058D9 RID: 22745
		// (set) Token: 0x060058DA RID: 22746
		bool IsDisplayPrecision { get; set; }

		// Token: 0x17000D9D RID: 3485
		// (get) Token: 0x060058DB RID: 22747
		bool IsCellProtection { get; }

		// Token: 0x17000D9E RID: 3486
		// (get) Token: 0x060058DC RID: 22748
		bool IsWindowProtection { get; }

		// Token: 0x17000D9F RID: 3487
		// (get) Token: 0x060058DD RID: 22749
		INameRanges Names { get; }

		// Token: 0x17000DA0 RID: 3488
		// (get) Token: 0x060058DE RID: 22750
		bool ReadOnly { get; }

		// Token: 0x17000DA1 RID: 3489
		// (get) Token: 0x060058DF RID: 22751
		// (set) Token: 0x060058E0 RID: 22752
		bool Saved { get; set; }

		// Token: 0x17000DA2 RID: 3490
		// (get) Token: 0x060058E1 RID: 22753
		IStyles Styles { get; }

		// Token: 0x17000DA3 RID: 3491
		// (get) Token: 0x060058E2 RID: 22754
		IWorksheets Worksheets { get; }

		// Token: 0x17000DA4 RID: 3492
		// (get) Token: 0x060058E3 RID: 22755
		bool HasMacros { get; }

		// Token: 0x17000DA5 RID: 3493
		// (get) Token: 0x060058E4 RID: 22756
		Color[] Palette { get; }

		// Token: 0x17000DA6 RID: 3494
		// (get) Token: 0x060058E5 RID: 22757
		// (set) Token: 0x060058E6 RID: 22758
		int DisplayedTab { get; set; }

		// Token: 0x17000DA7 RID: 3495
		// (get) Token: 0x060058E7 RID: 22759
		ICharts Charts { get; }

		// Token: 0x17000DA8 RID: 3496
		// (get) Token: 0x060058E8 RID: 22760
		// (set) Token: 0x060058E9 RID: 22761
		bool ThrowOnUnknownNames { get; set; }

		// Token: 0x17000DA9 RID: 3497
		// (get) Token: 0x060058EA RID: 22762
		// (set) Token: 0x060058EB RID: 22763
		bool DisableMacrosStart { get; set; }

		// Token: 0x17000DAA RID: 3498
		// (get) Token: 0x060058EC RID: 22764
		// (set) Token: 0x060058ED RID: 22765
		double StandardFontSize { get; set; }

		// Token: 0x17000DAB RID: 3499
		// (get) Token: 0x060058EE RID: 22766
		// (set) Token: 0x060058EF RID: 22767
		string StandardFont { get; set; }

		// Token: 0x17000DAC RID: 3500
		// (get) Token: 0x060058F0 RID: 22768
		// (set) Token: 0x060058F1 RID: 22769
		bool Allow3DRangesInDataValidation { get; set; }

		// Token: 0x17000DAD RID: 3501
		// (get) Token: 0x060058F2 RID: 22770
		string RowSeparator { get; }

		// Token: 0x17000DAE RID: 3502
		// (get) Token: 0x060058F3 RID: 22771
		string ArgumentsSeparator { get; }

		// Token: 0x17000DAF RID: 3503
		// (get) Token: 0x060058F4 RID: 22772
		IWorksheetGroup WorksheetGroup { get; }

		// Token: 0x17000DB0 RID: 3504
		// (get) Token: 0x060058F5 RID: 22773
		// (set) Token: 0x060058F6 RID: 22774
		bool IsRightToLeft { get; set; }

		// Token: 0x17000DB1 RID: 3505
		// (get) Token: 0x060058F7 RID: 22775
		// (set) Token: 0x060058F8 RID: 22776
		bool DisplayWorkbookTabs { get; set; }

		// Token: 0x17000DB2 RID: 3506
		// (get) Token: 0x060058F9 RID: 22777
		ITabSheets TabSheets { get; }

		// Token: 0x17000DB3 RID: 3507
		// (get) Token: 0x060058FA RID: 22778
		// (set) Token: 0x060058FB RID: 22779
		bool DetectDateTimeInValue { get; set; }

		// Token: 0x17000DB4 RID: 3508
		// (get) Token: 0x060058FC RID: 22780
		// (set) Token: 0x060058FD RID: 22781
		bool UseFastStringSearching { get; set; }

		// Token: 0x17000DB5 RID: 3509
		// (get) Token: 0x060058FE RID: 22782
		// (set) Token: 0x060058FF RID: 22783
		bool ReadOnlyRecommended { get; set; }

		// Token: 0x17000DB6 RID: 3510
		// (get) Token: 0x06005900 RID: 22784
		// (set) Token: 0x06005901 RID: 22785
		string PasswordToOpen { get; set; }

		// Token: 0x17000DB7 RID: 3511
		// (get) Token: 0x06005902 RID: 22786
		int MaxRowCount { get; }

		// Token: 0x17000DB8 RID: 3512
		// (get) Token: 0x06005903 RID: 22787
		int MaxColumnCount { get; }

		// Token: 0x17000DB9 RID: 3513
		// (get) Token: 0x06005904 RID: 22788
		// (set) Token: 0x06005905 RID: 22789
		ExcelVersion Version { get; set; }

		// Token: 0x17000DBA RID: 3514
		// (get) Token: 0x06005906 RID: 22790
		XlsPivotCachesCollection PivotCaches { get; }

		// Token: 0x06005907 RID: 22791
		void Activate();

		// Token: 0x06005908 RID: 22792
		IFont AddFont(IFont fontToAdd);

		// Token: 0x06005909 RID: 22793
		void Close(bool SaveChanges, string Filename);

		// Token: 0x0600590A RID: 22794
		void Close(bool saveChanges);

		// Token: 0x0600590B RID: 22795
		void Close();

		// Token: 0x0600590C RID: 22796
		void Close(string Filename);

		// Token: 0x0600590D RID: 22797
		void CopyToClipboard();

		// Token: 0x0600590E RID: 22798
		IMarkersDesigner CreateTemplateMarkersProcessor();

		// Token: 0x0600590F RID: 22799
		void MarkAsFinal();

		// Token: 0x06005910 RID: 22800
		void Save();

		// Token: 0x06005911 RID: 22801
		void SaveAs(string Filename);

		// Token: 0x06005912 RID: 22802
		void SaveAs(string Filename, ExcelSaveType saveType);

		// Token: 0x06005913 RID: 22803
		void SaveAsHtml(string filename, HTMLOptions saveOptions);

		// Token: 0x06005914 RID: 22804
		void SaveAs(Stream stream);

		// Token: 0x06005915 RID: 22805
		void SaveAs(Stream stream, ExcelSaveType saveType);

		// Token: 0x06005916 RID: 22806
		void SaveAs(string fileName, ExcelSaveType saveType, HttpResponse response);

		// Token: 0x06005917 RID: 22807
		void SaveAs(string fileName, HttpResponse response);

		// Token: 0x06005918 RID: 22808
		void SetPaletteColor(int index, Color color);

		// Token: 0x06005919 RID: 22809
		void ResetPalette();

		// Token: 0x0600591A RID: 22810
		Color GetPaletteColor(ExcelColors color);

		// Token: 0x0600591B RID: 22811
		ExcelColors GetNearestColor(Color color);

		// Token: 0x0600591C RID: 22812
		ExcelColors GetNearestColor(int r, int g, int b);

		// Token: 0x0600591D RID: 22813
		ExcelColors SetColorOrGetNearest(Color color);

		// Token: 0x0600591E RID: 22814
		ExcelColors SetColorOrGetNearest(int r, int g, int b);

		// Token: 0x0600591F RID: 22815
		IFont CreateFont();

		// Token: 0x06005920 RID: 22816
		IFont CreateFont(IFont baseFont);

		// Token: 0x06005921 RID: 22817
		IFont CreateFont(Font nativeFont);

		// Token: 0x06005922 RID: 22818
		void Replace(string oldValue, string newValue);

		// Token: 0x06005923 RID: 22819
		void Replace(string oldValue, double newValue);

		// Token: 0x06005924 RID: 22820
		void Replace(string oldValue, DateTime newValue);

		// Token: 0x06005925 RID: 22821
		void Replace(string oldValue, string[] newValues, bool isVertical);

		// Token: 0x06005926 RID: 22822
		void Replace(string oldValue, int[] newValues, bool isVertical);

		// Token: 0x06005927 RID: 22823
		void Replace(string oldValue, double[] newValues, bool isVertical);

		// Token: 0x06005928 RID: 22824
		void Replace(string oldValue, DataTable newValues, bool isFieldNamesShown);

		// Token: 0x06005929 RID: 22825
		void Replace(string oldValue, DataColumn newValues, bool isFieldNamesShown);

		// Token: 0x0600592A RID: 22826
		IXLSRange FindOne(string findValue, FindType flags);

		// Token: 0x0600592B RID: 22827
		IXLSRange FindOne(double findValue, FindType flags);

		// Token: 0x0600592C RID: 22828
		IXLSRange FindOne(bool findValue);

		// Token: 0x0600592D RID: 22829
		IXLSRange FindOne(DateTime findValue);

		// Token: 0x0600592E RID: 22830
		IXLSRange FindOne(TimeSpan findValue);

		// Token: 0x0600592F RID: 22831
		CellRange[] FindAll(string findValue, FindType flags);

		// Token: 0x06005930 RID: 22832
		CellRange[] FindAll(double findValue, FindType flags);

		// Token: 0x06005931 RID: 22833
		CellRange[] FindAll(bool findValue);

		// Token: 0x06005932 RID: 22834
		CellRange[] FindAll(DateTime findValue);

		// Token: 0x06005933 RID: 22835
		CellRange[] FindAll(TimeSpan findValue);

		// Token: 0x06005934 RID: 22836
		void SaveAs(string fileName, string separator);

		// Token: 0x06005935 RID: 22837
		void SaveAs(Stream stream, string separator);

		// Token: 0x06005936 RID: 22838
		void SetSeparators(char argumentsSeparator, char arrayRowsSeparator);

		// Token: 0x06005937 RID: 22839
		IHFEngine CreateHFEngine();

		// Token: 0x06005938 RID: 22840
		void Protect(bool bIsProtectWindow, bool bIsProtectContent);

		// Token: 0x06005939 RID: 22841
		void Unprotect();

		// Token: 0x0600593A RID: 22842
		IWorkbook Clone();

		// Token: 0x0600593B RID: 22843
		void SetWriteProtectionPassword(string password);

		// Token: 0x1400001C RID: 28
		// (add) Token: 0x0600593C RID: 22844
		// (remove) Token: 0x0600593D RID: 22845
		event EventHandler OnFileSaved;

		// Token: 0x1400001D RID: 29
		// (add) Token: 0x0600593E RID: 22846
		// (remove) Token: 0x0600593F RID: 22847
		event ReadOnlyFileEventHandler OnReadOnlyFile;
	}
}
