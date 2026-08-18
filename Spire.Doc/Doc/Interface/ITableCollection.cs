using System;

namespace Spire.Doc.Interface
{
	// Token: 0x02000503 RID: 1283
	public interface ITableCollection : IDocumentObjectCollection
	{
		// Token: 0x1700042E RID: 1070
		ITable this[int index]
		{
			get;
		}

		// Token: 0x06004243 RID: 16963
		int Add(ITable table);

		// Token: 0x06004244 RID: 16964
		int IndexOf(ITable table);

		// Token: 0x06004245 RID: 16965
		bool Contains(ITable table);
	}
}
