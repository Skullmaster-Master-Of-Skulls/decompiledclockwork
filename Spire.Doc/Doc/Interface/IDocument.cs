using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using Spire.Doc.Collections;
using Spire.Doc.Documents;
using Spire.Doc.Formatting;
using Spire.Doc.Reporting;

namespace Spire.Doc.Interface
{
	// Token: 0x020000F2 RID: 242
	public interface IDocument : ICompositeObject
	{
		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x060005B2 RID: 1458
		BuiltinDocumentProperties BuiltinDocumentProperties { get; }

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x060005B3 RID: 1459
		CustomDocumentProperties CustomDocumentProperties { get; }

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x060005B4 RID: 1460
		SectionCollection Sections { get; }

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x060005B5 RID: 1461
		StyleCollection Styles { get; }

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x060005B6 RID: 1462
		ListStyleCollection ListStyles { get; }

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x060005B7 RID: 1463
		BookmarkCollection Bookmarks { get; }

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x060005B8 RID: 1464
		TextBoxCollection TextBoxes { get; }

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x060005B9 RID: 1465
		CommentsCollection Comments { get; }

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x060005BA RID: 1466
		Section LastSection { get; }

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x060005BB RID: 1467
		Paragraph LastParagraph { get; }

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x060005BC RID: 1468
		// (set) Token: 0x060005BD RID: 1469
		ProtectionType ProtectionType { get; set; }

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x060005BE RID: 1470
		ViewSetup ViewSetup { get; }

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x060005BF RID: 1471
		// (set) Token: 0x060005C0 RID: 1472
		WatermarkBase Watermark { get; set; }

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x060005C1 RID: 1473
		MailMerge MailMerge { get; }

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x060005C2 RID: 1474
		Background Background { get; }

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x060005C3 RID: 1475
		VariableCollection Variables { get; }

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x060005C4 RID: 1476
		DocumentProperties Properties { get; }

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x060005C5 RID: 1477
		bool HasChanges { get; }

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x060005C6 RID: 1478
		// (set) Token: 0x060005C7 RID: 1479
		bool IsUpdateFields { get; set; }

		// Token: 0x060005C8 RID: 1480
		void CreateMinialDocument();

		// Token: 0x060005C9 RID: 1481
		Section AddSection();

		// Token: 0x060005CA RID: 1482
		ParagraphStyle AddParagraphStyle(string styleName);

		// Token: 0x060005CB RID: 1483
		ListStyle AddListStyle(ListType listType, string styleName);

		// Token: 0x060005CC RID: 1484
		string GetText();

		// Token: 0x060005CD RID: 1485
		Image[] SaveToImages(ImageType type);

		// Token: 0x060005CE RID: 1486
		Stream SaveToImages(int pageIndex, System.Drawing.Imaging.ImageFormat imageFormat);

		// Token: 0x060005CF RID: 1487
		Image SaveToImages(int pageIndex, ImageType type);

		// Token: 0x060005D0 RID: 1488
		Image[] SaveToImages(int pageIndex, int noOfPages, ImageType type);

		// Token: 0x060005D1 RID: 1489
		Paragraph CreateParagraph();

		// Token: 0x060005D2 RID: 1490
		Document Clone();

		// Token: 0x060005D3 RID: 1491
		Style AddStyle(BuiltinStyle builtinStyle);

		// Token: 0x060005D4 RID: 1492
		void Protect(ProtectionType type);

		// Token: 0x060005D5 RID: 1493
		void Protect(ProtectionType type, string password);

		// Token: 0x060005D6 RID: 1494
		void Encrypt(string password);

		// Token: 0x060005D7 RID: 1495
		void RemoveEncryption();

		// Token: 0x060005D8 RID: 1496
		void UpdateWordCount();

		// Token: 0x060005D9 RID: 1497
		TextSelection FindPattern(Regex pattern);

		// Token: 0x060005DA RID: 1498
		TextSelection FindString(string given, bool caseSensitive, bool wholeWord);

		// Token: 0x060005DB RID: 1499
		TextSelection[] FindPatternInLine(Regex pattern);

		// Token: 0x060005DC RID: 1500
		TextSelection[] FindStringInLine(string given, bool caseSensitive, bool wholeWord);

		// Token: 0x060005DD RID: 1501
		TextSelection[] FindAllPattern(Regex pattern);

		// Token: 0x060005DE RID: 1502
		TextSelection[] FindAllString(string given, bool caseSensitive, bool wholeWord);

		// Token: 0x060005DF RID: 1503
		int Replace(Regex pattern, string replace);

		// Token: 0x060005E0 RID: 1504
		int Replace(string given, string replace, bool caseSensitive, bool wholeWord);

		// Token: 0x060005E1 RID: 1505
		int Replace(Regex pattern, TextSelection textSelection);

		// Token: 0x060005E2 RID: 1506
		int Replace(string given, TextSelection textSelection, bool caseSensitive, bool wholeWord);

		// Token: 0x060005E3 RID: 1507
		int ReplaceInLine(string given, string replace, bool caseSensitive, bool wholeWord);

		// Token: 0x060005E4 RID: 1508
		int ReplaceInLine(Regex pattern, string replace);

		// Token: 0x060005E5 RID: 1509
		int ReplaceInLine(string given, TextSelection replacement, bool caseSensitive, bool wholeWord);

		// Token: 0x060005E6 RID: 1510
		int ReplaceInLine(Regex pattern, TextSelection replacement);

		// Token: 0x060005E7 RID: 1511
		TextSelection FindString(BodyRegion startTextBodyItem, string given, bool caseSensitive, bool wholeWord);

		// Token: 0x060005E8 RID: 1512
		TextSelection FindPattern(BodyRegion startBodyItem, Regex pattern);

		// Token: 0x060005E9 RID: 1513
		TextSelection[] FindStringInLine(BodyRegion startTextBodyItem, string given, bool caseSensitive, bool wholeWord);

		// Token: 0x060005EA RID: 1514
		TextSelection[] FindPatternInLine(BodyRegion startBodyItem, Regex pattern);

		// Token: 0x060005EB RID: 1515
		void ResetFindState();

		// Token: 0x060005EC RID: 1516
		void LoadFromStream(Stream stream, FileFormat fileFormat);

		// Token: 0x060005ED RID: 1517
		void SaveToFile(Stream stream, FileFormat fileFormat);

		// Token: 0x060005EE RID: 1518
		void LoadFromFile(string fileName);

		// Token: 0x060005EF RID: 1519
		void LoadFromFile(string fileName, FileFormat fileFormat);

		// Token: 0x060005F0 RID: 1520
		void SaveToFile(string fileName, FileFormat fileFormat, HttpResponse response, HttpContentType contentDisposotion);

		// Token: 0x060005F1 RID: 1521
		void LoadFromFileInReadMode(string strFileName, FileFormat fileFormat);

		// Token: 0x060005F2 RID: 1522
		void SaveToFile(string fileName);

		// Token: 0x060005F3 RID: 1523
		void SaveToFile(string fileName, FileFormat fileFormat);

		// Token: 0x060005F4 RID: 1524
		void ImportContent(IDocument doc);

		// Token: 0x060005F5 RID: 1525
		void ImportContent(IDocument doc, bool importStyles);
	}
}
