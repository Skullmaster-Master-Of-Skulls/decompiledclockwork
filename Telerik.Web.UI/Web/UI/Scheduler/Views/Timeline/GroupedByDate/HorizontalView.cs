using System;
using System.Collections.Generic;
using Telerik.Web.UI.Scheduler.Views.Timeline.GroupedByResource;

namespace Telerik.Web.UI.Scheduler.Views.Timeline.GroupedByDate
{
	// Token: 0x02001A86 RID: 6790
	internal class HorizontalView : HorizontalView
	{
		// Token: 0x06010707 RID: 67335 RVA: 0x003AC862 File Offset: 0x003AAA62
		public HorizontalView(Model model) : base(model)
		{
		}

		// Token: 0x06010708 RID: 67336 RVA: 0x003AC86C File Offset: 0x003AAA6C
		protected override void InitializeColumnHeaders()
		{
			IList<ViewHeader> list = base.CreateSlotHeaders();
			foreach (ViewHeader viewHeader in list)
			{
				if (this.Owner.TimelineView.ShowDateHeadersResolved)
				{
					if (viewHeader.SubHeaders.Count > 0)
					{
						using (IEnumerator<ViewHeader> enumerator2 = viewHeader.SubHeaders.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								ViewHeader viewHeader2 = enumerator2.Current;
								this.AddResourceHeaders(viewHeader2.SubHeaders);
							}
							goto IL_80;
						}
						goto IL_74;
					}
					goto IL_74;
					IL_80:
					base.ColumnHeaders.Add(viewHeader);
					continue;
					IL_74:
					this.AddResourceHeaders(viewHeader.SubHeaders);
					goto IL_80;
				}
				this.AddResourceHeaders(base.ColumnHeaders);
			}
		}

		// Token: 0x06010709 RID: 67337 RVA: 0x003AC948 File Offset: 0x003AAB48
		private void AddResourceHeaders(ICollection<ViewHeader> targetCollection)
		{
			if (this.Owner.TimelineView.ShowResourceHeadersResolved)
			{
				foreach (Resource resource in base.Model.Resources)
				{
					targetCollection.Add(HorizontalView.CreateResourceHeader(resource));
				}
			}
		}
	}
}
