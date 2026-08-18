using System;
using System.Globalization;
using System.util;

namespace iTextSharp.text
{
	// Token: 0x02000227 RID: 551
	public class ElementTags
	{
		// Token: 0x0600157D RID: 5501 RVA: 0x0007B0C4 File Offset: 0x0007A0C4
		public static string GetAlignment(int alignment)
		{
			switch (alignment)
			{
			case 0:
				return "Left";
			case 1:
				return "Center";
			case 2:
				return "Right";
			case 3:
			case 8:
				return "Justify";
			case 4:
				return "Top";
			case 5:
				return "Middle";
			case 6:
				return "Bottom";
			case 7:
				return "Baseline";
			default:
				return "Default";
			}
		}

		// Token: 0x0600157E RID: 5502 RVA: 0x0007B134 File Offset: 0x0007A134
		public static int AlignmentValue(string alignment)
		{
			if (alignment == null)
			{
				return -1;
			}
			if (Util.EqualsIgnoreCase("Center", alignment))
			{
				return 1;
			}
			if (Util.EqualsIgnoreCase("Left", alignment))
			{
				return 0;
			}
			if (Util.EqualsIgnoreCase("Right", alignment))
			{
				return 2;
			}
			if (Util.EqualsIgnoreCase("Justify", alignment))
			{
				return 3;
			}
			if (Util.EqualsIgnoreCase("JustifyAll", alignment))
			{
				return 8;
			}
			if (Util.EqualsIgnoreCase("Top", alignment))
			{
				return 4;
			}
			if (Util.EqualsIgnoreCase("Middle", alignment))
			{
				return 5;
			}
			if (Util.EqualsIgnoreCase("Bottom", alignment))
			{
				return 6;
			}
			if (Util.EqualsIgnoreCase("Baseline", alignment))
			{
				return 7;
			}
			return -1;
		}

		// Token: 0x04000E89 RID: 3721
		public const string ITEXT = "itext";

		// Token: 0x04000E8A RID: 3722
		public const string TITLE = "title";

		// Token: 0x04000E8B RID: 3723
		public const string SUBJECT = "subject";

		// Token: 0x04000E8C RID: 3724
		public const string KEYWORDS = "keywords";

		// Token: 0x04000E8D RID: 3725
		public const string AUTHOR = "author";

		// Token: 0x04000E8E RID: 3726
		public const string CREATIONDATE = "creationdate";

		// Token: 0x04000E8F RID: 3727
		public const string PRODUCER = "producer";

		// Token: 0x04000E90 RID: 3728
		public const string CHAPTER = "chapter";

		// Token: 0x04000E91 RID: 3729
		public const string SECTION = "section";

		// Token: 0x04000E92 RID: 3730
		public const string NUMBERDEPTH = "numberdepth";

		// Token: 0x04000E93 RID: 3731
		public const string DEPTH = "depth";

		// Token: 0x04000E94 RID: 3732
		public const string NUMBER = "number";

		// Token: 0x04000E95 RID: 3733
		public const string INDENT = "indent";

		// Token: 0x04000E96 RID: 3734
		public const string LEFT = "left";

		// Token: 0x04000E97 RID: 3735
		public const string RIGHT = "right";

		// Token: 0x04000E98 RID: 3736
		public const string PHRASE = "phrase";

		// Token: 0x04000E99 RID: 3737
		public const string ANCHOR = "anchor";

		// Token: 0x04000E9A RID: 3738
		public const string LIST = "list";

		// Token: 0x04000E9B RID: 3739
		public const string LISTITEM = "listitem";

		// Token: 0x04000E9C RID: 3740
		public const string PARAGRAPH = "paragraph";

		// Token: 0x04000E9D RID: 3741
		public const string LEADING = "leading";

		// Token: 0x04000E9E RID: 3742
		public const string ALIGN = "align";

		// Token: 0x04000E9F RID: 3743
		public const string KEEPTOGETHER = "keeptogether";

		// Token: 0x04000EA0 RID: 3744
		public const string NAME = "name";

		// Token: 0x04000EA1 RID: 3745
		public const string REFERENCE = "reference";

		// Token: 0x04000EA2 RID: 3746
		public const string LISTSYMBOL = "listsymbol";

		// Token: 0x04000EA3 RID: 3747
		public const string NUMBERED = "numbered";

		// Token: 0x04000EA4 RID: 3748
		public const string LETTERED = "lettered";

		// Token: 0x04000EA5 RID: 3749
		public const string FIRST = "first";

		// Token: 0x04000EA6 RID: 3750
		public const string SYMBOLINDENT = "symbolindent";

		// Token: 0x04000EA7 RID: 3751
		public const string INDENTATIONLEFT = "indentationleft";

		// Token: 0x04000EA8 RID: 3752
		public const string INDENTATIONRIGHT = "indentationright";

		// Token: 0x04000EA9 RID: 3753
		public const string IGNORE = "ignore";

		// Token: 0x04000EAA RID: 3754
		public const string ENTITY = "entity";

		// Token: 0x04000EAB RID: 3755
		public const string ID = "id";

		// Token: 0x04000EAC RID: 3756
		public const string CHUNK = "chunk";

		// Token: 0x04000EAD RID: 3757
		public const string ENCODING = "encoding";

		// Token: 0x04000EAE RID: 3758
		public const string EMBEDDED = "embedded";

		// Token: 0x04000EAF RID: 3759
		public const string COLOR = "color";

		// Token: 0x04000EB0 RID: 3760
		public const string RED = "red";

		// Token: 0x04000EB1 RID: 3761
		public const string GREEN = "green";

		// Token: 0x04000EB2 RID: 3762
		public const string BLUE = "blue";

		// Token: 0x04000EB3 RID: 3763
		public const string TABLE = "table";

		// Token: 0x04000EB4 RID: 3764
		public const string ROW = "row";

		// Token: 0x04000EB5 RID: 3765
		public const string CELL = "cell";

		// Token: 0x04000EB6 RID: 3766
		public const string COLUMNS = "columns";

		// Token: 0x04000EB7 RID: 3767
		public const string LASTHEADERROW = "lastHeaderRow";

		// Token: 0x04000EB8 RID: 3768
		public const string CELLPADDING = "cellpadding";

		// Token: 0x04000EB9 RID: 3769
		public const string CELLSPACING = "cellspacing";

		// Token: 0x04000EBA RID: 3770
		public const string OFFSET = "offset";

		// Token: 0x04000EBB RID: 3771
		public const string WIDTHS = "widths";

		// Token: 0x04000EBC RID: 3772
		public const string TABLEFITSPAGE = "tablefitspage";

		// Token: 0x04000EBD RID: 3773
		public const string CELLSFITPAGE = "cellsfitpage";

		// Token: 0x04000EBE RID: 3774
		public const string CONVERT2PDFP = "convert2pdfp";

		// Token: 0x04000EBF RID: 3775
		public const string HORIZONTALALIGN = "horizontalalign";

		// Token: 0x04000EC0 RID: 3776
		public const string VERTICALALIGN = "verticalalign";

		// Token: 0x04000EC1 RID: 3777
		public const string COLSPAN = "colspan";

		// Token: 0x04000EC2 RID: 3778
		public const string ROWSPAN = "rowspan";

		// Token: 0x04000EC3 RID: 3779
		public const string HEADER = "header";

		// Token: 0x04000EC4 RID: 3780
		public const string FOOTER = "footer";

		// Token: 0x04000EC5 RID: 3781
		public const string NOWRAP = "nowrap";

		// Token: 0x04000EC6 RID: 3782
		public const string BORDERWIDTH = "borderwidth";

		// Token: 0x04000EC7 RID: 3783
		public const string TOP = "top";

		// Token: 0x04000EC8 RID: 3784
		public const string BOTTOM = "bottom";

		// Token: 0x04000EC9 RID: 3785
		public const string WIDTH = "width";

		// Token: 0x04000ECA RID: 3786
		public const string BORDERCOLOR = "bordercolor";

		// Token: 0x04000ECB RID: 3787
		public const string BACKGROUNDCOLOR = "backgroundcolor";

		// Token: 0x04000ECC RID: 3788
		public const string BGRED = "bgred";

		// Token: 0x04000ECD RID: 3789
		public const string BGGREEN = "bggreen";

		// Token: 0x04000ECE RID: 3790
		public const string BGBLUE = "bgblue";

		// Token: 0x04000ECF RID: 3791
		public const string GRAYFILL = "grayfill";

		// Token: 0x04000ED0 RID: 3792
		public const string IMAGE = "image";

		// Token: 0x04000ED1 RID: 3793
		public const string BOOKMARKOPEN = "bookmarkopen";

		// Token: 0x04000ED2 RID: 3794
		public const string URL = "url";

		// Token: 0x04000ED3 RID: 3795
		public const string UNDERLYING = "underlying";

		// Token: 0x04000ED4 RID: 3796
		public const string TEXTWRAP = "textwrap";

		// Token: 0x04000ED5 RID: 3797
		public const string ALT = "alt";

		// Token: 0x04000ED6 RID: 3798
		public const string ABSOLUTEX = "absolutex";

		// Token: 0x04000ED7 RID: 3799
		public const string ABSOLUTEY = "absolutey";

		// Token: 0x04000ED8 RID: 3800
		public const string PLAINWIDTH = "plainwidth";

		// Token: 0x04000ED9 RID: 3801
		public const string PLAINHEIGHT = "plainheight";

		// Token: 0x04000EDA RID: 3802
		public const string SCALEDWIDTH = "scaledwidth";

		// Token: 0x04000EDB RID: 3803
		public const string SCALEDHEIGHT = "scaledheight";

		// Token: 0x04000EDC RID: 3804
		public const string ROTATION = "rotation";

		// Token: 0x04000EDD RID: 3805
		public const string NEWPAGE = "newpage";

		// Token: 0x04000EDE RID: 3806
		public const string NEWLINE = "newline";

		// Token: 0x04000EDF RID: 3807
		public const string ANNOTATION = "annotation";

		// Token: 0x04000EE0 RID: 3808
		public const string FILE = "file";

		// Token: 0x04000EE1 RID: 3809
		public const string DESTINATION = "destination";

		// Token: 0x04000EE2 RID: 3810
		public const string PAGE = "page";

		// Token: 0x04000EE3 RID: 3811
		public const string NAMED = "named";

		// Token: 0x04000EE4 RID: 3812
		public const string APPLICATION = "application";

		// Token: 0x04000EE5 RID: 3813
		public const string PARAMETERS = "parameters";

		// Token: 0x04000EE6 RID: 3814
		public const string OPERATION = "operation";

		// Token: 0x04000EE7 RID: 3815
		public const string DEFAULTDIR = "defaultdir";

		// Token: 0x04000EE8 RID: 3816
		public const string LLX = "llx";

		// Token: 0x04000EE9 RID: 3817
		public const string LLY = "lly";

		// Token: 0x04000EEA RID: 3818
		public const string URX = "urx";

		// Token: 0x04000EEB RID: 3819
		public const string URY = "ury";

		// Token: 0x04000EEC RID: 3820
		public const string CONTENT = "content";

		// Token: 0x04000EED RID: 3821
		public const string ALIGN_LEFT = "Left";

		// Token: 0x04000EEE RID: 3822
		public const string ALIGN_CENTER = "Center";

		// Token: 0x04000EEF RID: 3823
		public const string ALIGN_RIGHT = "Right";

		// Token: 0x04000EF0 RID: 3824
		public const string ALIGN_JUSTIFIED = "Justify";

		// Token: 0x04000EF1 RID: 3825
		public const string ALIGN_JUSTIFIED_ALL = "JustifyAll";

		// Token: 0x04000EF2 RID: 3826
		public const string ALIGN_TOP = "Top";

		// Token: 0x04000EF3 RID: 3827
		public const string ALIGN_MIDDLE = "Middle";

		// Token: 0x04000EF4 RID: 3828
		public const string ALIGN_BOTTOM = "Bottom";

		// Token: 0x04000EF5 RID: 3829
		public const string ALIGN_BASELINE = "Baseline";

		// Token: 0x04000EF6 RID: 3830
		public const string DEFAULT = "Default";

		// Token: 0x04000EF7 RID: 3831
		public const string UNKNOWN = "unknown";

		// Token: 0x04000EF8 RID: 3832
		public const string FONT = "font";

		// Token: 0x04000EF9 RID: 3833
		public const string SIZE = "size";

		// Token: 0x04000EFA RID: 3834
		public const string STYLE = "fontstyle";

		// Token: 0x04000EFB RID: 3835
		public const string HORIZONTALRULE = "horizontalrule";

		// Token: 0x04000EFC RID: 3836
		public const string PAGE_SIZE = "pagesize";

		// Token: 0x04000EFD RID: 3837
		public const string ORIENTATION = "orientation";

		// Token: 0x04000EFE RID: 3838
		public const string ALIGN_INDENTATION_ITEMS = "alignindent";

		// Token: 0x04000EFF RID: 3839
		public const string AUTO_INDENT_ITEMS = "autoindent";

		// Token: 0x04000F00 RID: 3840
		public const string LOWERCASE = "lowercase";

		// Token: 0x04000F01 RID: 3841
		public const string FACE = "face";

		// Token: 0x04000F02 RID: 3842
		public const string SRC = "src";

		// Token: 0x04000F03 RID: 3843
		public static readonly string SUBSUPSCRIPT = "SUBSUPSCRIPT".ToLower(CultureInfo.InvariantCulture);

		// Token: 0x04000F04 RID: 3844
		public static readonly string LOCALGOTO = "LOCALGOTO".ToLower(CultureInfo.InvariantCulture);

		// Token: 0x04000F05 RID: 3845
		public static readonly string REMOTEGOTO = "REMOTEGOTO".ToLower(CultureInfo.InvariantCulture);

		// Token: 0x04000F06 RID: 3846
		public static readonly string LOCALDESTINATION = "LOCALDESTINATION".ToLower(CultureInfo.InvariantCulture);

		// Token: 0x04000F07 RID: 3847
		public static readonly string GENERICTAG = "GENERICTAG".ToLower(CultureInfo.InvariantCulture);
	}
}
