using System;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Web.UI
{
	// Token: 0x02001216 RID: 4630
	public class TreeListPageChangedEventArgs : TreeListCommandEventArgs
	{
		// Token: 0x0600BF34 RID: 48948 RVA: 0x002A577A File Offset: 0x002A397A
		public TreeListPageChangedEventArgs(TreeListItem item, object commandSource, object argument) : base(item, commandSource, "Page", argument)
		{
		}

		// Token: 0x17003DB1 RID: 15793
		// (get) Token: 0x0600BF35 RID: 48949 RVA: 0x002A578A File Offset: 0x002A398A
		// (set) Token: 0x0600BF36 RID: 48950 RVA: 0x002A5792 File Offset: 0x002A3992
		public int NewPageIndex { get; internal set; }

		// Token: 0x0600BF37 RID: 48951 RVA: 0x002A579C File Offset: 0x002A399C
		[SuppressMessage("Microsoft.Globalization", "CA1307:SpecifyStringComparison", MessageId = "System.String.Compare(System.String,System.String,System.Boolean)")]
		[SuppressMessage("Microsoft.Globalization", "CA1304:SpecifyCultureInfo", MessageId = "System.String.Compare(System.String,System.String,System.Boolean)")]
		internal static void HandlePaging(TreeListItem treeListItem, object commandSource, string argument)
		{
			RadTreeList ownerTreeList = treeListItem.OwnerTreeList;
			int num = ownerTreeList.CurrentPageIndex;
			int pageSize = ownerTreeList.PageSize;
			int num2 = -1;
			if (string.Compare(argument, "Next", true) == 0)
			{
				num++;
				if (num > ownerTreeList.PageCount - 1)
				{
					num = ownerTreeList.PageCount - 1;
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
				num = ownerTreeList.PageCount - 1;
			}
			else if (int.TryParse(argument, out num2))
			{
				num = num2;
			}
			TreeListPageChangedEventArgs.EventState eventState = new TreeListPageChangedEventArgs.EventState
			{
				OwnerTreeList = ownerTreeList,
				EventArgs = argument,
				CommandSource = commandSource,
				NewIndex = num,
				Item = treeListItem
			};
			if (TreeListPageChangedEventArgs.CallPageIndexChangedEvent(eventState))
			{
				return;
			}
			ownerTreeList.CurrentPageIndex = Math.Min(eventState.NewIndex, ownerTreeList.PageCount - 1);
			if (!ownerTreeList.EnableViewState)
			{
				ownerTreeList.DataSource = null;
			}
			TreeListRebindReason rebindReason = TreeListRebindReason.PostBackEvent;
			ownerTreeList.ObtainDataSource(rebindReason);
			ownerTreeList.DataBind();
		}

		// Token: 0x0600BF38 RID: 48952 RVA: 0x002A58B0 File Offset: 0x002A3AB0
		private static bool CallPageIndexChangedEvent(TreeListPageChangedEventArgs.EventState state)
		{
			TreeListPageChangedEventArgs treeListPageChangedEventArgs = new TreeListPageChangedEventArgs(state.Item, state.CommandSource, state.EventArgs)
			{
				NewPageIndex = state.NewIndex
			};
			state.OwnerTreeList.FirePageIndexChanged(treeListPageChangedEventArgs);
			state.NewIndex = treeListPageChangedEventArgs.NewPageIndex;
			return treeListPageChangedEventArgs.Canceled;
		}

		// Token: 0x0600BF39 RID: 48953 RVA: 0x002A5901 File Offset: 0x002A3B01
		public override void ExecuteCommand(object source)
		{
			TreeListPageChangedEventArgs.HandlePaging(this.Item, base.EventSource, (string)base.CommandArgument);
		}

		// Token: 0x02001217 RID: 4631
		private class EventState
		{
			// Token: 0x17003DB2 RID: 15794
			// (get) Token: 0x0600BF3A RID: 48954 RVA: 0x002A591F File Offset: 0x002A3B1F
			// (set) Token: 0x0600BF3B RID: 48955 RVA: 0x002A5927 File Offset: 0x002A3B27
			public RadTreeList OwnerTreeList { get; set; }

			// Token: 0x17003DB3 RID: 15795
			// (get) Token: 0x0600BF3C RID: 48956 RVA: 0x002A5930 File Offset: 0x002A3B30
			// (set) Token: 0x0600BF3D RID: 48957 RVA: 0x002A5938 File Offset: 0x002A3B38
			public string EventArgs { get; set; }

			// Token: 0x17003DB4 RID: 15796
			// (get) Token: 0x0600BF3E RID: 48958 RVA: 0x002A5941 File Offset: 0x002A3B41
			// (set) Token: 0x0600BF3F RID: 48959 RVA: 0x002A5949 File Offset: 0x002A3B49
			public object CommandSource { get; set; }

			// Token: 0x17003DB5 RID: 15797
			// (get) Token: 0x0600BF40 RID: 48960 RVA: 0x002A5952 File Offset: 0x002A3B52
			// (set) Token: 0x0600BF41 RID: 48961 RVA: 0x002A595A File Offset: 0x002A3B5A
			public int NewIndex { get; set; }

			// Token: 0x17003DB6 RID: 15798
			// (get) Token: 0x0600BF42 RID: 48962 RVA: 0x002A5963 File Offset: 0x002A3B63
			// (set) Token: 0x0600BF43 RID: 48963 RVA: 0x002A596B File Offset: 0x002A3B6B
			public TreeListItem Item { get; set; }
		}
	}
}
