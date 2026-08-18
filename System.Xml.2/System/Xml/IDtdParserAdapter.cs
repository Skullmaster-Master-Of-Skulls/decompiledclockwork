using System;
using System.Text;
using System.Threading.Tasks;

namespace System.Xml
{
	// Token: 0x020000AB RID: 171
	internal interface IDtdParserAdapter
	{
		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060005E9 RID: 1513
		XmlNameTable NameTable { get; }

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060005EA RID: 1514
		IXmlNamespaceResolver NamespaceResolver { get; }

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060005EB RID: 1515
		Uri BaseUri { get; }

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060005EC RID: 1516
		char[] ParsingBuffer { get; }

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060005ED RID: 1517
		int ParsingBufferLength { get; }

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060005EE RID: 1518
		// (set) Token: 0x060005EF RID: 1519
		int CurrentPosition { get; set; }

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060005F0 RID: 1520
		int LineNo { get; }

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060005F1 RID: 1521
		int LineStartPosition { get; }

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060005F2 RID: 1522
		bool IsEof { get; }

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060005F3 RID: 1523
		int EntityStackLength { get; }

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060005F4 RID: 1524
		bool IsEntityEolNormalized { get; }

		// Token: 0x060005F5 RID: 1525
		int ReadData();

		// Token: 0x060005F6 RID: 1526
		void OnNewLine(int pos);

		// Token: 0x060005F7 RID: 1527
		int ParseNumericCharRef(StringBuilder internalSubsetBuilder);

		// Token: 0x060005F8 RID: 1528
		int ParseNamedCharRef(bool expand, StringBuilder internalSubsetBuilder);

		// Token: 0x060005F9 RID: 1529
		void ParsePI(StringBuilder sb);

		// Token: 0x060005FA RID: 1530
		void ParseComment(StringBuilder sb);

		// Token: 0x060005FB RID: 1531
		bool PushEntity(IDtdEntityInfo entity, out int entityId);

		// Token: 0x060005FC RID: 1532
		bool PopEntity(out IDtdEntityInfo oldEntity, out int newEntityId);

		// Token: 0x060005FD RID: 1533
		bool PushExternalSubset(string systemId, string publicId);

		// Token: 0x060005FE RID: 1534
		void PushInternalDtd(string baseUri, string internalDtd);

		// Token: 0x060005FF RID: 1535
		void OnSystemId(string systemId, LineInfo keywordLineInfo, LineInfo systemLiteralLineInfo);

		// Token: 0x06000600 RID: 1536
		void OnPublicId(string publicId, LineInfo keywordLineInfo, LineInfo publicLiteralLineInfo);

		// Token: 0x06000601 RID: 1537
		void Throw(Exception e);

		// Token: 0x06000602 RID: 1538
		Task<int> ReadDataAsync();

		// Token: 0x06000603 RID: 1539
		Task<int> ParseNumericCharRefAsync(StringBuilder internalSubsetBuilder);

		// Token: 0x06000604 RID: 1540
		Task<int> ParseNamedCharRefAsync(bool expand, StringBuilder internalSubsetBuilder);

		// Token: 0x06000605 RID: 1541
		Task ParsePIAsync(StringBuilder sb);

		// Token: 0x06000606 RID: 1542
		Task ParseCommentAsync(StringBuilder sb);

		// Token: 0x06000607 RID: 1543
		Task<Tuple<int, bool>> PushEntityAsync(IDtdEntityInfo entity);

		// Token: 0x06000608 RID: 1544
		Task<bool> PushExternalSubsetAsync(string systemId, string publicId);
	}
}
