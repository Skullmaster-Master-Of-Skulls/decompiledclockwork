using System;
using System.Drawing;
using Spire.Xls.Core;

namespace Spire.Xls
{
	// Token: 0x020000FD RID: 253
	public class CellStyleObject : IStyle
	{
		// Token: 0x06000B43 RID: 2883 RVA: 0x00070BE0 File Offset: 0x0006FBE0
		internal CellStyleObject(IStyle A_0)
		{
			this.ᜀ = A_0;
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06000B44 RID: 2884 RVA: 0x00070BFC File Offset: 0x0006FBFC
		// (set) Token: 0x06000B45 RID: 2885 RVA: 0x00070C44 File Offset: 0x0006FC44
		public bool JustifyLast
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜀ.JustifyLast;
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᜀ.JustifyLast = value;
			}
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06000B46 RID: 2886 RVA: 0x00070C8C File Offset: 0x0006FC8C
		// (set) Token: 0x06000B47 RID: 2887 RVA: 0x00070CD4 File Offset: 0x0006FCD4
		public string NumberFormatLocal
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				return this.ᜀ.NumberFormatLocal;
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				this.ᜀ.NumberFormatLocal = value;
			}
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06000B48 RID: 2888 RVA: 0x00070D1C File Offset: 0x0006FD1C
		public IBorders Borders
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜀ.Borders;
			}
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06000B49 RID: 2889 RVA: 0x00070D64 File Offset: 0x0006FD64
		public bool BuiltIn
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜀ.BuiltIn;
			}
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06000B4A RID: 2890 RVA: 0x00070DAC File Offset: 0x0006FDAC
		// (set) Token: 0x06000B4B RID: 2891 RVA: 0x00070DF4 File Offset: 0x0006FDF4
		public ExcelPatternType FillPattern
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜀ.FillPattern;
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᜀ.FillPattern = value;
			}
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06000B4C RID: 2892 RVA: 0x00070E3C File Offset: 0x0006FE3C
		public IFont Font
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜀ.Font;
			}
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06000B4D RID: 2893 RVA: 0x00070E84 File Offset: 0x0006FE84
		// (set) Token: 0x06000B4E RID: 2894 RVA: 0x00070ECC File Offset: 0x0006FECC
		public bool FormulaHidden
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				return this.ᜀ.FormulaHidden;
			}
			set
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				this.ᜀ.FormulaHidden = value;
			}
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06000B4F RID: 2895 RVA: 0x00070F14 File Offset: 0x0006FF14
		// (set) Token: 0x06000B50 RID: 2896 RVA: 0x00070F5C File Offset: 0x0006FF5C
		public HorizontalAlignType HorizontalAlignment
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜀ.HorizontalAlignment;
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᜀ.HorizontalAlignment = value;
			}
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06000B51 RID: 2897 RVA: 0x00070FA4 File Offset: 0x0006FFA4
		// (set) Token: 0x06000B52 RID: 2898 RVA: 0x00070FEC File Offset: 0x0006FFEC
		public bool IncludeAlignment
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜀ.IncludeAlignment;
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᜀ.IncludeAlignment = value;
			}
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06000B53 RID: 2899 RVA: 0x00071034 File Offset: 0x00070034
		// (set) Token: 0x06000B54 RID: 2900 RVA: 0x0007107C File Offset: 0x0007007C
		public bool IncludeBorder
		{
			get
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				return this.ᜀ.IncludeBorder;
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				this.ᜀ.IncludeBorder = value;
			}
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06000B55 RID: 2901 RVA: 0x000710C4 File Offset: 0x000700C4
		// (set) Token: 0x06000B56 RID: 2902 RVA: 0x0007110C File Offset: 0x0007010C
		public bool IncludeFont
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜀ.IncludeFont;
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				this.ᜀ.IncludeFont = value;
			}
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06000B57 RID: 2903 RVA: 0x00071154 File Offset: 0x00070154
		// (set) Token: 0x06000B58 RID: 2904 RVA: 0x0007119C File Offset: 0x0007019C
		public bool IncludeNumberFormat
		{
			get
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				return this.ᜀ.IncludeNumberFormat;
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᜀ.IncludeNumberFormat = value;
			}
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06000B59 RID: 2905 RVA: 0x000711E4 File Offset: 0x000701E4
		// (set) Token: 0x06000B5A RID: 2906 RVA: 0x0007122C File Offset: 0x0007022C
		public bool IncludePatterns
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				return this.ᜀ.IncludePatterns;
			}
			set
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				this.ᜀ.IncludePatterns = value;
			}
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06000B5B RID: 2907 RVA: 0x00071274 File Offset: 0x00070274
		// (set) Token: 0x06000B5C RID: 2908 RVA: 0x000712BC File Offset: 0x000702BC
		public bool IncludeProtection
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				return this.ᜀ.IncludeProtection;
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				this.ᜀ.IncludeProtection = value;
			}
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06000B5D RID: 2909 RVA: 0x00071304 File Offset: 0x00070304
		// (set) Token: 0x06000B5E RID: 2910 RVA: 0x0007134C File Offset: 0x0007034C
		public int IndentLevel
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				return this.ᜀ.IndentLevel;
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᜀ.IndentLevel = value;
			}
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06000B5F RID: 2911 RVA: 0x00071394 File Offset: 0x00070394
		public bool IsInitialized
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜀ.IsInitialized;
			}
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06000B60 RID: 2912 RVA: 0x000713DC File Offset: 0x000703DC
		// (set) Token: 0x06000B61 RID: 2913 RVA: 0x00071424 File Offset: 0x00070424
		public bool Locked
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				return this.ᜀ.Locked;
			}
			set
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				this.ᜀ.Locked = value;
			}
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06000B62 RID: 2914 RVA: 0x0007146C File Offset: 0x0007046C
		public string Name
		{
			get
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				return this.ᜀ.Name;
			}
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06000B63 RID: 2915 RVA: 0x000714B4 File Offset: 0x000704B4
		// (set) Token: 0x06000B64 RID: 2916 RVA: 0x000714FC File Offset: 0x000704FC
		public string NumberFormat
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜀ.NumberFormat;
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᜀ.NumberFormat = value;
			}
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06000B65 RID: 2917 RVA: 0x00071544 File Offset: 0x00070544
		// (set) Token: 0x06000B66 RID: 2918 RVA: 0x0007158C File Offset: 0x0007058C
		public int NumberFormatIndex
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜀ.NumberFormatIndex;
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᜀ.NumberFormatIndex = value;
			}
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06000B67 RID: 2919 RVA: 0x000715D4 File Offset: 0x000705D4
		public INumberFormat NumberFormatSettings
		{
			get
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				return this.ᜀ.NumberFormatSettings;
			}
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06000B68 RID: 2920 RVA: 0x0007161C File Offset: 0x0007061C
		// (set) Token: 0x06000B69 RID: 2921 RVA: 0x00071664 File Offset: 0x00070664
		public int Rotation
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜀ.Rotation;
			}
			set
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				this.ᜀ.Rotation = value;
			}
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06000B6A RID: 2922 RVA: 0x000716AC File Offset: 0x000706AC
		// (set) Token: 0x06000B6B RID: 2923 RVA: 0x000716F4 File Offset: 0x000706F4
		public bool ShrinkToFit
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜀ.ShrinkToFit;
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				this.ᜀ.ShrinkToFit = value;
			}
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06000B6C RID: 2924 RVA: 0x0007173C File Offset: 0x0007073C
		// (set) Token: 0x06000B6D RID: 2925 RVA: 0x00071784 File Offset: 0x00070784
		public VerticalAlignType VerticalAlignment
		{
			get
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				return this.ᜀ.VerticalAlignment;
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᜀ.VerticalAlignment = value;
			}
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06000B6E RID: 2926 RVA: 0x000717CC File Offset: 0x000707CC
		// (set) Token: 0x06000B6F RID: 2927 RVA: 0x00071814 File Offset: 0x00070814
		public bool WrapText
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜀ.WrapText;
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᜀ.WrapText = value;
			}
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06000B70 RID: 2928 RVA: 0x0007185C File Offset: 0x0007085C
		// (set) Token: 0x06000B71 RID: 2929 RVA: 0x000718A4 File Offset: 0x000708A4
		public ReadingOrderType ReadingOrder
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜀ.ReadingOrder;
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᜀ.ReadingOrder = value;
			}
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06000B72 RID: 2930 RVA: 0x000718EC File Offset: 0x000708EC
		// (set) Token: 0x06000B73 RID: 2931 RVA: 0x00071934 File Offset: 0x00070934
		public bool IsFirstSymbolApostrophe
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜀ.IsFirstSymbolApostrophe;
			}
			set
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				this.ᜀ.IsFirstSymbolApostrophe = value;
			}
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06000B74 RID: 2932 RVA: 0x0007197C File Offset: 0x0007097C
		// (set) Token: 0x06000B75 RID: 2933 RVA: 0x000719C4 File Offset: 0x000709C4
		public ExcelColors PatternKnownColor
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜀ.PatternKnownColor;
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				this.ᜀ.PatternKnownColor = value;
			}
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06000B76 RID: 2934 RVA: 0x00071A0C File Offset: 0x00070A0C
		// (set) Token: 0x06000B77 RID: 2935 RVA: 0x00071A54 File Offset: 0x00070A54
		public Color PatternColor
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				return this.ᜀ.PatternColor;
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᜀ.PatternColor = value;
			}
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06000B78 RID: 2936 RVA: 0x00071A9C File Offset: 0x00070A9C
		// (set) Token: 0x06000B79 RID: 2937 RVA: 0x00071AE4 File Offset: 0x00070AE4
		public ExcelColors KnownColor
		{
			get
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				return this.ᜀ.KnownColor;
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				this.ᜀ.KnownColor = value;
			}
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06000B7A RID: 2938 RVA: 0x00071B2C File Offset: 0x00070B2C
		// (set) Token: 0x06000B7B RID: 2939 RVA: 0x00071B74 File Offset: 0x00070B74
		public Color Color
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜀ.Color;
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᜀ.Color = value;
			}
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06000B7C RID: 2940 RVA: 0x00071BBC File Offset: 0x00070BBC
		public object Parent
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜀ.Parent;
			}
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06000B7D RID: 2941 RVA: 0x00071C04 File Offset: 0x00070C04
		internal IStyle Wrapped
		{
			get
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				return this.ᜀ;
			}
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x00071C48 File Offset: 0x00070C48
		public void BeginUpdate()
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			this.ᜀ.BeginUpdate();
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x00071C90 File Offset: 0x00070C90
		public void EndUpdate()
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			this.ᜀ.EndUpdate();
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06000B80 RID: 2944 RVA: 0x00071CD8 File Offset: 0x00070CD8
		public IInterior Interior
		{
			get
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				return this.ᜀ.Interior;
			}
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06000B81 RID: 2945 RVA: 0x00071D20 File Offset: 0x00070D20
		public bool IsModified
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜀ.IsModified;
			}
		}

		// Token: 0x040009E5 RID: 2533
		private string \u2609\u00A0\u00AF\u00AE;

		// Token: 0x040009E6 RID: 2534
		private float[] \u2609\u00AF\u00AE\u009C;

		// Token: 0x040009E7 RID: 2535
		private float[] \u2609\u00AD\u00A5\u0099;

		// Token: 0x040009E8 RID: 2536
		private bool \u25D9\u0087\u0091\u0099;

		// Token: 0x040009E9 RID: 2537
		private IStyle ᜀ;
	}
}
