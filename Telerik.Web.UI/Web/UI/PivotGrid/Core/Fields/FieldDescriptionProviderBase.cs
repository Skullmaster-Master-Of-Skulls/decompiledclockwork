using System;

namespace Telerik.Web.UI.PivotGrid.Core.Fields
{
	// Token: 0x02000CB5 RID: 3253
	public abstract class FieldDescriptionProviderBase : IFieldDescriptionProvider
	{
		// Token: 0x1400012C RID: 300
		// (add) Token: 0x060079C2 RID: 31170 RVA: 0x001BF3C4 File Offset: 0x001BD5C4
		// (remove) Token: 0x060079C3 RID: 31171 RVA: 0x001BF3FC File Offset: 0x001BD5FC
		public event EventHandler<GetDescriptionsDataCompletedEventArgs> GetDescriptionsDataAsyncCompleted;

		// Token: 0x17002736 RID: 10038
		// (get) Token: 0x060079C4 RID: 31172 RVA: 0x001BF431 File Offset: 0x001BD631
		// (set) Token: 0x060079C5 RID: 31173 RVA: 0x001BF439 File Offset: 0x001BD639
		public bool IsBusy { get; protected set; }

		// Token: 0x060079C6 RID: 31174
		public abstract void GetDescriptionsDataAsync(object state);

		// Token: 0x060079C7 RID: 31175 RVA: 0x001BF442 File Offset: 0x001BD642
		protected virtual void OnDescriptionsDataCompleted(GetDescriptionsDataCompletedEventArgs args)
		{
			this.IsBusy = false;
			if (this.GetDescriptionsDataAsyncCompleted != null)
			{
				this.GetDescriptionsDataAsyncCompleted(this, args);
			}
		}
	}
}
