using System;

namespace Spire.Xls.Core
{
	// Token: 0x02000214 RID: 532
	public interface ITabSheets
	{
		// Token: 0x17000B66 RID: 2918
		// (get) Token: 0x06001EEB RID: 7915
		int Count { get; }

		// Token: 0x17000B67 RID: 2919
		ITabSheet this[int index]
		{
			get;
		}

		// Token: 0x06001EED RID: 7917
		void Move(int iOldIndex, int iNewIndex);

		// Token: 0x06001EEE RID: 7918
		void MoveBefore(ITabSheet sheetToMove, ITabSheet sheetForPlacement);

		// Token: 0x06001EEF RID: 7919
		void MoveAfter(ITabSheet sheetToCopy, ITabSheet sheetForPlacement);
	}
}
