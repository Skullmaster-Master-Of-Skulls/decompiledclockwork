using System;

namespace Telerik.Web.UI
{
	// Token: 0x0200110C RID: 4364
	public class GridGroupsChangingEventArgs : EventArgs
	{
		// Token: 0x0600B2A9 RID: 45737 RVA: 0x0026DC43 File Offset: 0x0026BE43
		public GridGroupsChangingEventArgs(GridTableView tableView, GridGroupByExpression expression, GridGroupsChangingAction action)
		{
			this._tableView = tableView;
			this._expression = expression;
			this._action = action;
		}

		// Token: 0x0600B2AA RID: 45738 RVA: 0x0026DC60 File Offset: 0x0026BE60
		public GridGroupsChangingEventArgs(GridTableView tableView, GridGroupByExpression expression, GridGroupByField sortedField)
		{
			this._tableView = tableView;
			this._expression = expression;
			this._action = GridGroupsChangingAction.ChangeSortOrder;
			this._sortedField = sortedField;
		}

		// Token: 0x0600B2AB RID: 45739 RVA: 0x0026DC84 File Offset: 0x0026BE84
		public GridGroupsChangingEventArgs(GridTableView tableView, GridGroupByExpression expression1, GridGroupByExpression expression2, GridGroupsChangingAction action)
		{
			this._tableView = tableView;
			this._expression = expression1;
			this._swapExpression = expression2;
			this._action = action;
		}

		// Token: 0x170039DC RID: 14812
		// (get) Token: 0x0600B2AC RID: 45740 RVA: 0x0026DCA9 File Offset: 0x0026BEA9
		public GridGroupsChangingAction Action
		{
			get
			{
				return this._action;
			}
		}

		// Token: 0x170039DD RID: 14813
		// (get) Token: 0x0600B2AD RID: 45741 RVA: 0x0026DCB1 File Offset: 0x0026BEB1
		public GridTableView TableView
		{
			get
			{
				return this._tableView;
			}
		}

		// Token: 0x170039DE RID: 14814
		// (get) Token: 0x0600B2AE RID: 45742 RVA: 0x0026DCB9 File Offset: 0x0026BEB9
		// (set) Token: 0x0600B2AF RID: 45743 RVA: 0x0026DCC1 File Offset: 0x0026BEC1
		public GridGroupByExpression Expression
		{
			get
			{
				return this._expression;
			}
			set
			{
				this._expression = value;
			}
		}

		// Token: 0x170039DF RID: 14815
		// (get) Token: 0x0600B2B0 RID: 45744 RVA: 0x0026DCCA File Offset: 0x0026BECA
		public GridGroupByExpression SwapExpression
		{
			get
			{
				return this._swapExpression;
			}
		}

		// Token: 0x170039E0 RID: 14816
		// (get) Token: 0x0600B2B1 RID: 45745 RVA: 0x0026DCD2 File Offset: 0x0026BED2
		public GridGroupByField SortedField
		{
			get
			{
				return this._sortedField;
			}
		}

		// Token: 0x170039E1 RID: 14817
		// (get) Token: 0x0600B2B2 RID: 45746 RVA: 0x0026DCDA File Offset: 0x0026BEDA
		// (set) Token: 0x0600B2B3 RID: 45747 RVA: 0x0026DCE2 File Offset: 0x0026BEE2
		public bool Canceled
		{
			get
			{
				return this._canceled;
			}
			set
			{
				this._canceled = value;
			}
		}

		// Token: 0x04002F12 RID: 12050
		private GridTableView _tableView;

		// Token: 0x04002F13 RID: 12051
		private GridGroupByExpression _expression;

		// Token: 0x04002F14 RID: 12052
		private GridGroupsChangingAction _action;

		// Token: 0x04002F15 RID: 12053
		private bool _canceled;

		// Token: 0x04002F16 RID: 12054
		private GridGroupByExpression _swapExpression;

		// Token: 0x04002F17 RID: 12055
		private GridGroupByField _sortedField;
	}
}
