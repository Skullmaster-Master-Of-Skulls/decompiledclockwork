using System;
using System.Collections;
using System.IO;
using System.Xml;

namespace Spire.Xls.Core
{
	// Token: 0x02000424 RID: 1060
	public interface IWorkbooks : IEnumerable
	{
		// Token: 0x17000D1C RID: 3356
		// (get) Token: 0x06004016 RID: 16406
		int Count { get; }

		// Token: 0x17000D1D RID: 3357
		IWorkbook this[int Index]
		{
			get;
		}

		// Token: 0x17000D1E RID: 3358
		// (get) Token: 0x06004018 RID: 16408
		object Parent { get; }

		// Token: 0x06004019 RID: 16409
		IWorkbook Create();

		// Token: 0x0600401A RID: 16410
		IWorkbook Create(int worksheetsQuantity);

		// Token: 0x0600401B RID: 16411
		IWorkbook Create(string[] names);

		// Token: 0x0600401C RID: 16412
		IWorkbook Add(string strTemplateFile);

		// Token: 0x0600401D RID: 16413
		IWorkbook Add(string strTemplateFile, ExcelParseOptions options);

		// Token: 0x0600401E RID: 16414
		IWorkbook Add();

		// Token: 0x0600401F RID: 16415
		IWorkbook Open(string Filename);

		// Token: 0x06004020 RID: 16416
		IWorkbook OpenReadOnly(string strFileName, string seperator);

		// Token: 0x06004021 RID: 16417
		IWorkbook Open(string Filename, ExcelParseOptions options);

		// Token: 0x06004022 RID: 16418
		IWorkbook Open(Stream stream);

		// Token: 0x06004023 RID: 16419
		IWorkbook Open(Stream stream, ExcelParseOptions options);

		// Token: 0x06004024 RID: 16420
		IWorkbook Open(Stream stream, string separator, int row, int column);

		// Token: 0x06004025 RID: 16421
		IWorkbook Open(string fileName, string separator, int row, int column);

		// Token: 0x06004026 RID: 16422
		IWorkbook Open(Stream stream, string separator);

		// Token: 0x06004027 RID: 16423
		IWorkbook Open(string fileName, string separator);

		// Token: 0x06004028 RID: 16424
		IWorkbook OpenReadOnly(string strFileName);

		// Token: 0x06004029 RID: 16425
		IWorkbook OpenReadOnly(string strFileName, ExcelParseOptions options);

		// Token: 0x0600402A RID: 16426
		IWorkbook Open(string fileName, ExcelParseOptions options, bool isReadOnly, string password, ExcelVersion version);

		// Token: 0x0600402B RID: 16427
		void Close();

		// Token: 0x0600402C RID: 16428
		IWorkbook PasteWorkbook();

		// Token: 0x0600402D RID: 16429
		IWorkbook OpenFromXml(string strPath, XmlOpenType openType);

		// Token: 0x0600402E RID: 16430
		IWorkbook OpenFromXml(Stream stream, XmlOpenType openType);

		// Token: 0x0600402F RID: 16431
		IWorkbook OpenFromXml(XmlReader reader, XmlOpenType openType);

		// Token: 0x06004030 RID: 16432
		IWorkbook Open(string filename, ExcelVersion version);

		// Token: 0x06004031 RID: 16433
		IWorkbook Open(Stream stream, ExcelVersion version);
	}
}
