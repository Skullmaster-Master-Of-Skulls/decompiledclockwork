using System;
using System.Drawing;
using Spire.Xls.Core.Interfaces;

namespace Spire.Xls.Core
{
	// Token: 0x02000209 RID: 521
	public interface IConditionalFormat : IExcelApplication, IOptimizedUpdate
	{
		// Token: 0x17000B23 RID: 2851
		// (get) Token: 0x06001E4E RID: 7758
		// (set) Token: 0x06001E4F RID: 7759
		ConditionalFormatType FormatType { get; set; }

		// Token: 0x17000B24 RID: 2852
		// (get) Token: 0x06001E50 RID: 7760
		// (set) Token: 0x06001E51 RID: 7761
		ComparisonOperatorType Operator { get; set; }

		// Token: 0x17000B25 RID: 2853
		// (get) Token: 0x06001E52 RID: 7762
		// (set) Token: 0x06001E53 RID: 7763
		bool IsBold { get; set; }

		// Token: 0x17000B26 RID: 2854
		// (get) Token: 0x06001E54 RID: 7764
		// (set) Token: 0x06001E55 RID: 7765
		bool IsItalic { get; set; }

		// Token: 0x17000B27 RID: 2855
		// (get) Token: 0x06001E56 RID: 7766
		// (set) Token: 0x06001E57 RID: 7767
		ExcelColors FontKnownColor { get; set; }

		// Token: 0x17000B28 RID: 2856
		// (get) Token: 0x06001E58 RID: 7768
		// (set) Token: 0x06001E59 RID: 7769
		Color FontColor { get; set; }

		// Token: 0x17000B29 RID: 2857
		// (get) Token: 0x06001E5A RID: 7770
		// (set) Token: 0x06001E5B RID: 7771
		FontUnderlineType Underline { get; set; }

		// Token: 0x17000B2A RID: 2858
		// (get) Token: 0x06001E5C RID: 7772
		// (set) Token: 0x06001E5D RID: 7773
		bool IsStrikeThrough { get; set; }

		// Token: 0x17000B2B RID: 2859
		// (get) Token: 0x06001E5E RID: 7774
		// (set) Token: 0x06001E5F RID: 7775
		ExcelColors LeftBorderKnownColor { get; set; }

		// Token: 0x17000B2C RID: 2860
		// (get) Token: 0x06001E60 RID: 7776
		// (set) Token: 0x06001E61 RID: 7777
		Color LeftBorderColor { get; set; }

		// Token: 0x17000B2D RID: 2861
		// (get) Token: 0x06001E62 RID: 7778
		// (set) Token: 0x06001E63 RID: 7779
		LineStyleType LeftBorderStyle { get; set; }

		// Token: 0x17000B2E RID: 2862
		// (get) Token: 0x06001E64 RID: 7780
		// (set) Token: 0x06001E65 RID: 7781
		ExcelColors RightBorderKnownColor { get; set; }

		// Token: 0x17000B2F RID: 2863
		// (get) Token: 0x06001E66 RID: 7782
		// (set) Token: 0x06001E67 RID: 7783
		Color RightBorderColor { get; set; }

		// Token: 0x17000B30 RID: 2864
		// (get) Token: 0x06001E68 RID: 7784
		// (set) Token: 0x06001E69 RID: 7785
		LineStyleType RightBorderStyle { get; set; }

		// Token: 0x17000B31 RID: 2865
		// (get) Token: 0x06001E6A RID: 7786
		// (set) Token: 0x06001E6B RID: 7787
		ExcelColors TopBorderKnownColor { get; set; }

		// Token: 0x17000B32 RID: 2866
		// (get) Token: 0x06001E6C RID: 7788
		// (set) Token: 0x06001E6D RID: 7789
		Color TopBorderColor { get; set; }

		// Token: 0x17000B33 RID: 2867
		// (get) Token: 0x06001E6E RID: 7790
		// (set) Token: 0x06001E6F RID: 7791
		LineStyleType TopBorderStyle { get; set; }

		// Token: 0x17000B34 RID: 2868
		// (get) Token: 0x06001E70 RID: 7792
		// (set) Token: 0x06001E71 RID: 7793
		ExcelColors BottomBorderKnownColor { get; set; }

		// Token: 0x17000B35 RID: 2869
		// (get) Token: 0x06001E72 RID: 7794
		// (set) Token: 0x06001E73 RID: 7795
		Color BottomBorderColor { get; set; }

		// Token: 0x17000B36 RID: 2870
		// (get) Token: 0x06001E74 RID: 7796
		// (set) Token: 0x06001E75 RID: 7797
		LineStyleType BottomBorderStyle { get; set; }

		// Token: 0x17000B37 RID: 2871
		// (get) Token: 0x06001E76 RID: 7798
		// (set) Token: 0x06001E77 RID: 7799
		string FirstFormula { get; set; }

		// Token: 0x17000B38 RID: 2872
		// (get) Token: 0x06001E78 RID: 7800
		// (set) Token: 0x06001E79 RID: 7801
		string FirstFormulaR1C1 { get; set; }

		// Token: 0x17000B39 RID: 2873
		// (get) Token: 0x06001E7A RID: 7802
		// (set) Token: 0x06001E7B RID: 7803
		string SecondFormula { get; set; }

		// Token: 0x17000B3A RID: 2874
		// (get) Token: 0x06001E7C RID: 7804
		// (set) Token: 0x06001E7D RID: 7805
		string SecondFormulaR1C1 { get; set; }

		// Token: 0x17000B3B RID: 2875
		// (get) Token: 0x06001E7E RID: 7806
		// (set) Token: 0x06001E7F RID: 7807
		ExcelColors KnownColor { get; set; }

		// Token: 0x17000B3C RID: 2876
		// (get) Token: 0x06001E80 RID: 7808
		// (set) Token: 0x06001E81 RID: 7809
		Color Color { get; set; }

		// Token: 0x17000B3D RID: 2877
		// (get) Token: 0x06001E82 RID: 7810
		// (set) Token: 0x06001E83 RID: 7811
		ExcelColors BackKnownColor { get; set; }

		// Token: 0x17000B3E RID: 2878
		// (get) Token: 0x06001E84 RID: 7812
		// (set) Token: 0x06001E85 RID: 7813
		Color BackColor { get; set; }

		// Token: 0x17000B3F RID: 2879
		// (get) Token: 0x06001E86 RID: 7814
		// (set) Token: 0x06001E87 RID: 7815
		ExcelPatternType FillPattern { get; set; }

		// Token: 0x17000B40 RID: 2880
		// (get) Token: 0x06001E88 RID: 7816
		// (set) Token: 0x06001E89 RID: 7817
		bool IsSuperScript { get; set; }

		// Token: 0x17000B41 RID: 2881
		// (get) Token: 0x06001E8A RID: 7818
		// (set) Token: 0x06001E8B RID: 7819
		bool IsSubScript { get; set; }

		// Token: 0x17000B42 RID: 2882
		// (get) Token: 0x06001E8C RID: 7820
		// (set) Token: 0x06001E8D RID: 7821
		bool IsFontFormatPresent { get; set; }

		// Token: 0x17000B43 RID: 2883
		// (get) Token: 0x06001E8E RID: 7822
		// (set) Token: 0x06001E8F RID: 7823
		bool IsBorderFormatPresent { get; set; }

		// Token: 0x17000B44 RID: 2884
		// (get) Token: 0x06001E90 RID: 7824
		// (set) Token: 0x06001E91 RID: 7825
		bool IsPatternFormatPresent { get; set; }

		// Token: 0x17000B45 RID: 2885
		// (get) Token: 0x06001E92 RID: 7826
		// (set) Token: 0x06001E93 RID: 7827
		bool IsFontColorPresent { get; set; }

		// Token: 0x17000B46 RID: 2886
		// (get) Token: 0x06001E94 RID: 7828
		// (set) Token: 0x06001E95 RID: 7829
		bool IsBackgroundColorPresent { get; set; }

		// Token: 0x17000B47 RID: 2887
		// (get) Token: 0x06001E96 RID: 7830
		// (set) Token: 0x06001E97 RID: 7831
		bool IsLeftBorderModified { get; set; }

		// Token: 0x17000B48 RID: 2888
		// (get) Token: 0x06001E98 RID: 7832
		// (set) Token: 0x06001E99 RID: 7833
		bool IsRightBorderModified { get; set; }

		// Token: 0x17000B49 RID: 2889
		// (get) Token: 0x06001E9A RID: 7834
		// (set) Token: 0x06001E9B RID: 7835
		bool IsTopBorderModified { get; set; }

		// Token: 0x17000B4A RID: 2890
		// (get) Token: 0x06001E9C RID: 7836
		// (set) Token: 0x06001E9D RID: 7837
		bool IsBottomBorderModified { get; set; }

		// Token: 0x17000B4B RID: 2891
		// (get) Token: 0x06001E9E RID: 7838
		DataBar DataBar { get; }

		// Token: 0x17000B4C RID: 2892
		// (get) Token: 0x06001E9F RID: 7839
		IconSet IconSet { get; }

		// Token: 0x17000B4D RID: 2893
		// (get) Token: 0x06001EA0 RID: 7840
		ColorScale ColorScale { get; }
	}
}
