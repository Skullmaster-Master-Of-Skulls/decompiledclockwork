using System;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Web.UI
{
	// Token: 0x02000C3B RID: 3131
	public class PivotGridPageChangedEventArgs : PivotGridCommandEventArgs
	{
		// Token: 0x06007681 RID: 30337 RVA: 0x001B8426 File Offset: 0x001B6626
		public PivotGridPageChangedEventArgs(PivotGridItem item, object commandSource, object argument) : base(item, commandSource, "Page", argument)
		{
		}

		// Token: 0x17002687 RID: 9863
		// (get) Token: 0x06007682 RID: 30338 RVA: 0x001B8436 File Offset: 0x001B6636
		// (set) Token: 0x06007683 RID: 30339 RVA: 0x001B843E File Offset: 0x001B663E
		public int NewPageIndex { get; internal set; }

		// Token: 0x06007684 RID: 30340 RVA: 0x001B8448 File Offset: 0x001B6648
		[SuppressMessage("Microsoft.Globalization", "CA1304:SpecifyCultureInfo", MessageId = "System.String.Compare(System.String,System.String,System.Boolean)")]
		[SuppressMessage("Microsoft.Globalization", "CA1307:SpecifyStringComparison", MessageId = "System.String.Compare(System.String,System.String,System.Boolean)")]
		internal static void HandlePaging(PivotGridItem pivotGridItem, object commandSource, string argument)
		{
			RadPivotGrid ownerPivotGrid = pivotGridItem.OwnerPivotGrid;
			int num = ownerPivotGrid.CurrentPageIndex;
			int pageSize = ownerPivotGrid.PageSize;
			int num2 = -1;
			if (string.Compare(argument, "Next", true) == 0)
			{
				num++;
				if (num > ownerPivotGrid.PageCount - 1)
				{
					num = ownerPivotGrid.PageCount - 1;
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
				num = ownerPivotGrid.PageCount - 1;
			}
			else if (int.TryParse(argument, out num2))
			{
				num = num2;
			}
			PivotGridPageChangedEventArgs.EventState eventState = new PivotGridPageChangedEventArgs.EventState
			{
				OwnerPivotGrid = ownerPivotGrid,
				EventArgs = argument,
				CommandSource = commandSource,
				NewIndex = num,
				Item = pivotGridItem
			};
			if (PivotGridPageChangedEventArgs.CallPageIndexChangedEvent(eventState))
			{
				return;
			}
			ownerPivotGrid.CurrentPageIndex = Math.Min(eventState.NewIndex, ownerPivotGrid.PageCount - 1);
			if (!ownerPivotGrid.EnableViewState)
			{
				ownerPivotGrid.DataSource = null;
			}
			PivotGridRebindReason rebindReason = PivotGridRebindReason.PostBackEvent;
			ownerPivotGrid.ObtainDataSource(rebindReason, false);
			ownerPivotGrid.DataBind();
		}

		// Token: 0x06007685 RID: 30341 RVA: 0x001B855C File Offset: 0x001B675C
		private static bool CallPageIndexChangedEvent(PivotGridPageChangedEventArgs.EventState state)
		{
			PivotGridPageChangedEventArgs pivotGridPageChangedEventArgs = new PivotGridPageChangedEventArgs(state.Item, state.CommandSource, state.EventArgs)
			{
				NewPageIndex = state.NewIndex
			};
			state.OwnerPivotGrid.FirePageIndexChanged(pivotGridPageChangedEventArgs);
			state.NewIndex = pivotGridPageChangedEventArgs.NewPageIndex;
			return pivotGridPageChangedEventArgs.Canceled;
		}

		// Token: 0x06007686 RID: 30342 RVA: 0x001B85AD File Offset: 0x001B67AD
		public override void ExecuteCommand(object source)
		{
			PivotGridPageChangedEventArgs.HandlePaging(this.Item, base.EventSource, (string)base.CommandArgument);
		}

		// Token: 0x02000C3C RID: 3132
		private class EventState
		{
			// Token: 0x17002688 RID: 9864
			// (get) Token: 0x06007687 RID: 30343 RVA: 0x001B85CB File Offset: 0x001B67CB
			// (set) Token: 0x06007688 RID: 30344 RVA: 0x001B85D3 File Offset: 0x001B67D3
			public RadPivotGrid OwnerPivotGrid { get; set; }

			// Token: 0x17002689 RID: 9865
			// (get) Token: 0x06007689 RID: 30345 RVA: 0x001B85DC File Offset: 0x001B67DC
			// (set) Token: 0x0600768A RID: 30346 RVA: 0x001B85E4 File Offset: 0x001B67E4
			public string EventArgs { get; set; }

			// Token: 0x1700268A RID: 9866
			// (get) Token: 0x0600768B RID: 30347 RVA: 0x001B85ED File Offset: 0x001B67ED
			// (set) Token: 0x0600768C RID: 30348 RVA: 0x001B85F5 File Offset: 0x001B67F5
			public object CommandSource { get; set; }

			// Token: 0x1700268B RID: 9867
			// (get) Token: 0x0600768D RID: 30349 RVA: 0x001B85FE File Offset: 0x001B67FE
			// (set) Token: 0x0600768E RID: 30350 RVA: 0x001B8606 File Offset: 0x001B6806
			public int NewIndex { get; set; }

			// Token: 0x1700268C RID: 9868
			// (get) Token: 0x0600768F RID: 30351 RVA: 0x001B860F File Offset: 0x001B680F
			// (set) Token: 0x06007690 RID: 30352 RVA: 0x001B8617 File Offset: 0x001B6817
			public PivotGridItem Item { get; set; }
		}
	}
}
