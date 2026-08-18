using System;

namespace Telerik.Web.UI.PivotGrid.Core.Fields
{
	// Token: 0x02000CB4 RID: 3252
	public interface IFieldDescriptionProvider
	{
		// Token: 0x1400012B RID: 299
		// (add) Token: 0x060079BD RID: 31165
		// (remove) Token: 0x060079BE RID: 31166
		event EventHandler<GetDescriptionsDataCompletedEventArgs> GetDescriptionsDataAsyncCompleted;

		// Token: 0x17002735 RID: 10037
		// (get) Token: 0x060079BF RID: 31167
		bool IsBusy { get; }

		// Token: 0x060079C0 RID: 31168
		void GetDescriptionsDataAsync(object state);
	}
}
