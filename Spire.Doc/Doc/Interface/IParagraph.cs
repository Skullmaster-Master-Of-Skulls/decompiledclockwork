using System;
using System.Drawing;
using System.IO;
using Spire.Doc.Collections;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using Spire.Doc.Formatting;

namespace Spire.Doc.Interface
{
	// Token: 0x020004EF RID: 1263
	public interface IParagraph : IBodyRegion, IStyleHolder, ICompositeObject
	{
		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x060041A9 RID: 16809
		// (set) Token: 0x060041AA RID: 16810
		string Text { get; set; }

		// Token: 0x1700040A RID: 1034
		ParagraphBase this[int index]
		{
			get;
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x060041AC RID: 16812
		ParagraphItemCollection Items { get; }

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x060041AD RID: 16813
		ParagraphFormat Format { get; }

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x060041AE RID: 16814
		ListFormat ListFormat { get; }

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x060041AF RID: 16815
		CharacterFormat BreakCharacterFormat { get; }

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x060041B0 RID: 16816
		bool IsInCell { get; }

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x060041B1 RID: 16817
		bool IsEndOfSection { get; }

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x060041B2 RID: 16818
		bool IsEndOfDocument { get; }

		// Token: 0x060041B3 RID: 16819
		TextRange AppendText(string text);

		// Token: 0x060041B4 RID: 16820
		DocPicture AppendPicture(Image image);

		// Token: 0x060041B5 RID: 16821
		DocPicture AppendPicture(byte[] imageBytes);

		// Token: 0x060041B6 RID: 16822
		Field AppendField(string fieldName, FieldType fieldType);

		// Token: 0x060041B7 RID: 16823
		BookmarkStart AppendBookmarkStart(string name);

		// Token: 0x060041B8 RID: 16824
		BookmarkEnd AppendBookmarkEnd(string name);

		// Token: 0x060041B9 RID: 16825
		Comment AppendComment(string text);

		// Token: 0x060041BA RID: 16826
		Footnote AppendFootnote(FootnoteType type);

		// Token: 0x060041BB RID: 16827
		TextBox AppendTextBox(float width, float height);

		// Token: 0x060041BC RID: 16828
		Symbol AppendSymbol(byte characterCode);

		// Token: 0x060041BD RID: 16829
		Break AppendBreak(BreakType breakType);

		// Token: 0x060041BE RID: 16830
		void AppendHTML(string html);

		// Token: 0x060041BF RID: 16831
		ParagraphStyle GetStyle();

		// Token: 0x060041C0 RID: 16832
		int Replace(string given, TextSelection textSelection, bool caseSensitive, bool wholeWord);

		// Token: 0x060041C1 RID: 16833
		CheckBoxFormField AppendCheckBox();

		// Token: 0x060041C2 RID: 16834
		TextFormField AppendTextFormField(string defaultText);

		// Token: 0x060041C3 RID: 16835
		DropDownFormField AppendDropDownFormField();

		// Token: 0x060041C4 RID: 16836
		CheckBoxFormField AppendCheckBox(string checkBoxName, bool defaultCheckBoxValue);

		// Token: 0x060041C5 RID: 16837
		TextFormField AppendTextFormField(string formFieldName, string defaultText);

		// Token: 0x060041C6 RID: 16838
		DropDownFormField AppendDropDownFormField(string dropDropDownName);

		// Token: 0x060041C7 RID: 16839
		Field AppendHyperlink(string link, string text, HyperlinkType type);

		// Token: 0x060041C8 RID: 16840
		Field AppendHyperlink(string link, DocPicture picture, HyperlinkType type);

		// Token: 0x060041C9 RID: 16841
		void RemoveAbsPosition();

		// Token: 0x060041CA RID: 16842
		TableOfContent AppendTOC(int lowerHeadingLevel, int upperHeadingLevel);

		// Token: 0x060041CB RID: 16843
		DocOleObject AppendOleObject(Stream oleStream, DocPicture olePicture, OleObjectType type);

		// Token: 0x060041CC RID: 16844
		DocOleObject AppendOleObject(byte[] oleBytes, DocPicture olePicture, OleObjectType type);

		// Token: 0x060041CD RID: 16845
		DocOleObject AppendOleObject(string pathToFile, DocPicture olePicture, OleObjectType type);

		// Token: 0x060041CE RID: 16846
		DocOleObject AppendOleObject(string pathToFile, DocPicture olePicture);

		// Token: 0x060041CF RID: 16847
		DocOleObject AppendOleObject(Stream stream, DocPicture pic, OleLinkType oleLinkType);

		// Token: 0x060041D0 RID: 16848
		DocOleObject AppendOleObject(byte[] oleBytes, DocPicture olePicture, OleLinkType oleLinkType);

		// Token: 0x060041D1 RID: 16849
		DocOleObject AppendOleObject(byte[] oleBytes, DocPicture olePicture, string fileExtension);

		// Token: 0x060041D2 RID: 16850
		DocOleObject AppendOleObject(Stream oleStream, DocPicture olePicture, string fileExtension);
	}
}
