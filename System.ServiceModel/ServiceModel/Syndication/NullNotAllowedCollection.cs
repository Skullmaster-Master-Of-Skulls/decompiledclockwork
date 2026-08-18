using System;
using System.Collections.ObjectModel;

namespace System.ServiceModel.Syndication
{
	// Token: 0x02000190 RID: 400
	internal class NullNotAllowedCollection<TCollectionItem> : Collection<TCollectionItem> where TCollectionItem : class
	{
		// Token: 0x06000C7B RID: 3195 RVA: 0x0002CF7A File Offset: 0x0002B17A
		protected override void InsertItem(int index, TCollectionItem item)
		{
			if (item == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("item");
			}
			base.InsertItem(index, item);
		}

		// Token: 0x06000C7C RID: 3196 RVA: 0x0002CF9C File Offset: 0x0002B19C
		protected override void SetItem(int index, TCollectionItem item)
		{
			if (item == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("item");
			}
			base.SetItem(index, item);
		}
	}
}
