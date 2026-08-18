using System;

namespace System.Web.UI.Design
{
	// Token: 0x0200004C RID: 76
	public interface IDataSourceFieldSchema
	{
		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600028F RID: 655
		Type DataType { get; }

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000290 RID: 656
		bool Identity { get; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000291 RID: 657
		bool IsReadOnly { get; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000292 RID: 658
		bool IsUnique { get; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000293 RID: 659
		int Length { get; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000294 RID: 660
		string Name { get; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000295 RID: 661
		bool Nullable { get; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000296 RID: 662
		int Precision { get; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000297 RID: 663
		bool PrimaryKey { get; }

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000298 RID: 664
		int Scale { get; }
	}
}
