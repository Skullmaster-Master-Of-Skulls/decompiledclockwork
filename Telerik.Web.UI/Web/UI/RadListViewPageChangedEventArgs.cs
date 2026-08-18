using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001949 RID: 6473
	public class RadListViewPageChangedEventArgs : RadListViewCommandEventArgs
	{
		// Token: 0x0600FA83 RID: 64131 RVA: 0x003868A2 File Offset: 0x00384AA2
		public RadListViewPageChangedEventArgs(RadListViewItem item, object commandSource, object argument) : base(item, commandSource, "Page", argument)
		{
		}

		// Token: 0x17004BB1 RID: 19377
		// (get) Token: 0x0600FA84 RID: 64132 RVA: 0x003868B2 File Offset: 0x00384AB2
		// (set) Token: 0x0600FA85 RID: 64133 RVA: 0x003868BA File Offset: 0x00384ABA
		public int NewPageIndex { get; internal set; }

		// Token: 0x0600FA86 RID: 64134 RVA: 0x003868C4 File Offset: 0x00384AC4
		internal static void HandlePaging(RadListView ownerListView, object commandSource, string argument)
		{
			int num = ownerListView.CurrentPageIndex;
			int pageSize = ownerListView.PageSize;
			int num2 = -1;
			if (string.Compare(argument, "Next", true) == 0)
			{
				num++;
				if (num > ownerListView.PageCount - 1)
				{
					num = ownerListView.PageCount - 1;
				}
			}
			else if (string.Compare(argument, "Prev", true) == 0)
			{
				num--;
				if (num < 0)
				{
					return;
				}
			}
			else if (string.Compare(argument, "First", true) == 0)
			{
				num = 0;
			}
			else if (string.Compare(argument, "Last", true) == 0)
			{
				num = ownerListView.PageCount - 1;
			}
			else if (int.TryParse(argument, out num2))
			{
				num = num2;
			}
			RadListViewPageChangedEventArgs.EventState eventState = new RadListViewPageChangedEventArgs.EventState
			{
				OwnerListView = ownerListView,
				EventArgs = argument,
				CommandSource = commandSource,
				NewIndex = num
			};
			if (RadListViewPageChangedEventArgs.CallPageIndexChangedEvent(eventState))
			{
				return;
			}
			ownerListView.CurrentPageIndex = eventState.NewIndex;
			if (!ownerListView.EnableViewState)
			{
				ownerListView.DataSource = null;
			}
			RadListViewRebindReason rebindReason = RadListViewRebindReason.PostBackEvent;
			ownerListView.ObtainDataSource(rebindReason);
			ownerListView.ClearSelectedIndexes();
			ownerListView.ClearEditItems();
			ownerListView.DataBind();
		}

		// Token: 0x0600FA87 RID: 64135 RVA: 0x003869C4 File Offset: 0x00384BC4
		private static bool CallPageIndexChangedEvent(RadListViewPageChangedEventArgs.EventState state)
		{
			RadListViewPageChangedEventArgs radListViewPageChangedEventArgs = new RadListViewPageChangedEventArgs(state.Item, state.CommandSource, state.EventArgs)
			{
				NewPageIndex = state.NewIndex
			};
			state.OwnerListView.FirePageIndexChanged(radListViewPageChangedEventArgs);
			state.NewIndex = radListViewPageChangedEventArgs.NewPageIndex;
			return radListViewPageChangedEventArgs.Canceled;
		}

		// Token: 0x0600FA88 RID: 64136 RVA: 0x00386A15 File Offset: 0x00384C15
		public override void ExecuteCommand(object source)
		{
			RadListViewPageChangedEventArgs.HandlePaging(this.ListViewItem.OwnerListView, base.EventSource, (string)base.CommandArgument);
		}

		// Token: 0x0200194A RID: 6474
		private class ArgumentName
		{
			// Token: 0x04004747 RID: 18247
			public const string Next = "Next";

			// Token: 0x04004748 RID: 18248
			public const string Prev = "Prev";

			// Token: 0x04004749 RID: 18249
			public const string First = "First";

			// Token: 0x0400474A RID: 18250
			public const string Last = "Last";
		}

		// Token: 0x0200194B RID: 6475
		private class EventState
		{
			// Token: 0x17004BB2 RID: 19378
			// (get) Token: 0x0600FA8A RID: 64138 RVA: 0x00386A40 File Offset: 0x00384C40
			// (set) Token: 0x0600FA8B RID: 64139 RVA: 0x00386A48 File Offset: 0x00384C48
			public RadListView OwnerListView { get; set; }

			// Token: 0x17004BB3 RID: 19379
			// (get) Token: 0x0600FA8C RID: 64140 RVA: 0x00386A51 File Offset: 0x00384C51
			// (set) Token: 0x0600FA8D RID: 64141 RVA: 0x00386A59 File Offset: 0x00384C59
			public string EventArgs { get; set; }

			// Token: 0x17004BB4 RID: 19380
			// (get) Token: 0x0600FA8E RID: 64142 RVA: 0x00386A62 File Offset: 0x00384C62
			// (set) Token: 0x0600FA8F RID: 64143 RVA: 0x00386A6A File Offset: 0x00384C6A
			public object CommandSource { get; set; }

			// Token: 0x17004BB5 RID: 19381
			// (get) Token: 0x0600FA90 RID: 64144 RVA: 0x00386A73 File Offset: 0x00384C73
			// (set) Token: 0x0600FA91 RID: 64145 RVA: 0x00386A7B File Offset: 0x00384C7B
			public int NewIndex { get; set; }

			// Token: 0x17004BB6 RID: 19382
			// (get) Token: 0x0600FA92 RID: 64146 RVA: 0x00386A84 File Offset: 0x00384C84
			// (set) Token: 0x0600FA93 RID: 64147 RVA: 0x00386A8C File Offset: 0x00384C8C
			public RadListViewItem Item { get; set; }
		}
	}
}
