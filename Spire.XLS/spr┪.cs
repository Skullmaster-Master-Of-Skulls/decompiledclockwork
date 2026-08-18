using System;
using Spire.Xls.Core.Interfaces;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet;

// Token: 0x020001B8 RID: 440
internal interface spr\u252A : INamedObject
{
	// Token: 0x06001879 RID: 6265
	string get_Name();

	// Token: 0x0600187A RID: 6266
	void set_Name(string value);

	// Token: 0x0600187B RID: 6267
	int get_RealIndex();

	// Token: 0x0600187C RID: 6268
	void set_RealIndex(int value);

	// Token: 0x0600187D RID: 6269
	void ᜀ(RecordArrayList A_0);

	// Token: 0x0600187E RID: 6270
	void add_NameChanged(XlsEventHandler value);

	// Token: 0x0600187F RID: 6271
	void remove_NameChanged(XlsEventHandler value);
}
