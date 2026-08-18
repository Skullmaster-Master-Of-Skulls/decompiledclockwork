using System;
using System.Collections;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02001174 RID: 4468
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[Serializable]
	public class GridSortExpressionCollection : IList, ICollection, IEnumerable, IStateManager
	{
		// Token: 0x0600B606 RID: 46598 RVA: 0x00280F8C File Offset: 0x0027F18C
		public GridSortExpressionCollection()
		{
			this.list = new ArrayList();
		}

		// Token: 0x0600B607 RID: 46599 RVA: 0x00280FAA File Offset: 0x0027F1AA
		public GridSortExpressionCollection(ArrayList list)
		{
			this.list = list;
		}

		// Token: 0x17003ADF RID: 15071
		// (get) Token: 0x0600B608 RID: 46600 RVA: 0x00280FC4 File Offset: 0x0027F1C4
		// (set) Token: 0x0600B609 RID: 46601 RVA: 0x00280FD7 File Offset: 0x0027F1D7
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool AllowMultiColumnSorting
		{
			get
			{
				return this.StateManager.ViewStateGetBool("_amcs", false);
			}
			set
			{
				this.StateManager.ViewState["_amcs"] = value;
			}
		}

		// Token: 0x0600B60A RID: 46602 RVA: 0x00280FF4 File Offset: 0x0027F1F4
		public void CopyTo(Array array, int index)
		{
			this.list.CopyTo(array, index);
		}

		// Token: 0x0600B60B RID: 46603 RVA: 0x00281004 File Offset: 0x0027F204
		public void CopyTo(GridSortExpressionCollection dest)
		{
			foreach (object obj in this)
			{
				GridSortExpression gridSortExpression = (GridSortExpression)obj;
				dest.Add(gridSortExpression.Clone());
			}
		}

		// Token: 0x0600B60C RID: 46604 RVA: 0x00281060 File Offset: 0x0027F260
		public IEnumerator GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		// Token: 0x17003AE0 RID: 15072
		// (get) Token: 0x0600B60D RID: 46605 RVA: 0x0028106D File Offset: 0x0027F26D
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		// Token: 0x17003AE1 RID: 15073
		// (get) Token: 0x0600B60E RID: 46606 RVA: 0x0028107A File Offset: 0x0027F27A
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public bool IsSynchronized
		{
			get
			{
				return this.list.IsSynchronized;
			}
		}

		// Token: 0x17003AE2 RID: 15074
		object IList.this[int index]
		{
			get
			{
				return this.list[index];
			}
			set
			{
				this.list[index] = value;
			}
		}

		// Token: 0x17003AE3 RID: 15075
		public GridSortExpression this[int index]
		{
			get
			{
				return (GridSortExpression)this.list[index];
			}
			set
			{
				this.list[index] = value;
			}
		}

		// Token: 0x0600B613 RID: 46611 RVA: 0x002810C6 File Offset: 0x0027F2C6
		public int Add(object value)
		{
			return this.AddEx((GridSortExpression)value);
		}

		// Token: 0x0600B614 RID: 46612 RVA: 0x002810D4 File Offset: 0x0027F2D4
		bool IList.Contains(object value)
		{
			return this.ContainsSortExpression((GridSortExpression)value);
		}

		// Token: 0x0600B615 RID: 46613 RVA: 0x002810E2 File Offset: 0x0027F2E2
		int IList.IndexOf(object value)
		{
			return this.list.IndexOf((GridSortExpression)value);
		}

		// Token: 0x0600B616 RID: 46614 RVA: 0x002810F5 File Offset: 0x0027F2F5
		void IList.Insert(int index, object value)
		{
			this.list.Insert(index, (GridSortExpression)value);
		}

		// Token: 0x17003AE4 RID: 15076
		// (get) Token: 0x0600B617 RID: 46615 RVA: 0x00281109 File Offset: 0x0027F309
		bool IList.IsFixedSize
		{
			get
			{
				return this.list.IsFixedSize;
			}
		}

		// Token: 0x17003AE5 RID: 15077
		// (get) Token: 0x0600B618 RID: 46616 RVA: 0x00281116 File Offset: 0x0027F316
		bool IList.IsReadOnly
		{
			get
			{
				return this.list.IsReadOnly;
			}
		}

		// Token: 0x0600B619 RID: 46617 RVA: 0x00281123 File Offset: 0x0027F323
		void IList.Remove(object value)
		{
			this.RemoveSortExpression((GridSortExpression)value);
		}

		// Token: 0x0600B61A RID: 46618 RVA: 0x00281131 File Offset: 0x0027F331
		void IList.RemoveAt(int index)
		{
			this.list.RemoveAt(index);
		}

		// Token: 0x0600B61B RID: 46619 RVA: 0x0028113F File Offset: 0x0027F33F
		public void Clear()
		{
			this.list.Clear();
		}

		// Token: 0x0600B61C RID: 46620 RVA: 0x0028114C File Offset: 0x0027F34C
		public GridSortExpression GetExpression(string expression)
		{
			GridSortExpression gridSortExpression = new GridSortExpression();
			gridSortExpression.FieldName = expression;
			return (GridSortExpression)this.list[this.list.IndexOf(gridSortExpression)];
		}

		// Token: 0x0600B61D RID: 46621 RVA: 0x002811A0 File Offset: 0x0027F3A0
		public bool TryGetExpression(string expression, out GridSortExpression sortExpression)
		{
			sortExpression = null;
			if (!string.IsNullOrEmpty(expression))
			{
				string dataFieldName = this.ExtractFieldFrom(expression);
				sortExpression = Array.Find<GridSortExpression>((GridSortExpression[])this.GetList().ToArray(typeof(GridSortExpression)), (GridSortExpression item) => item.FieldName.Equals(dataFieldName, StringComparison.OrdinalIgnoreCase));
			}
			return sortExpression != null;
		}

		// Token: 0x0600B61E RID: 46622 RVA: 0x00281200 File Offset: 0x0027F400
		private string ExtractFieldFrom(string expression)
		{
			string text = expression.Trim();
			if (text != null)
			{
				int num = text.LastIndexOf(" ");
				if (num > 0)
				{
					string text2 = text.Substring(num);
					if (text2.Trim().ToUpper() == "ASC")
					{
						text = text.Substring(0, num);
					}
					else if (text2.Trim().ToUpper() == "DESC")
					{
						text = text.Substring(0, num);
					}
				}
			}
			return text;
		}

		// Token: 0x17003AE6 RID: 15078
		// (get) Token: 0x0600B61F RID: 46623 RVA: 0x00281272 File Offset: 0x0027F472
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public object SyncRoot
		{
			get
			{
				return this.list.SyncRoot;
			}
		}

		// Token: 0x0600B620 RID: 46624 RVA: 0x00281280 File Offset: 0x0027F480
		private int AddEx(GridSortExpression sortExpression)
		{
			int num = -1;
			if (sortExpression == null)
			{
				return num;
			}
			if (string.IsNullOrEmpty(sortExpression.FieldName))
			{
				return num;
			}
			if (!this.AllowMultiColumnSorting)
			{
				this.list.Clear();
			}
			if (!this.ContainsSortExpression(sortExpression))
			{
				num = this.list.Add(sortExpression);
			}
			else
			{
				num = this.list.IndexOf(sortExpression);
				GridSortExpression gridSortExpression = (GridSortExpression)this.list[num];
				gridSortExpression.SortOrder = sortExpression.SortOrder;
			}
			return num;
		}

		// Token: 0x0600B621 RID: 46625 RVA: 0x002812FA File Offset: 0x0027F4FA
		public void AddSortExpression(GridSortExpression sortExpression)
		{
			this.AddEx(sortExpression);
		}

		// Token: 0x0600B622 RID: 46626 RVA: 0x00281304 File Offset: 0x0027F504
		public void AddSortExpression(string expression)
		{
			this.AddEx(GridSortExpression.Parse(expression));
		}

		// Token: 0x0600B623 RID: 46627 RVA: 0x00281314 File Offset: 0x0027F514
		public void AddAt(int index, GridSortExpression sortExpression)
		{
			int num = this.list.IndexOf(sortExpression);
			if (num >= 0)
			{
				this.RemoveSortExpression(sortExpression);
			}
			if (index > 0)
			{
				this.AllowMultiColumnSorting = true;
			}
			if (num >= 0 && num <= index && index != 0)
			{
				this.list.Insert(index - 1, sortExpression);
				return;
			}
			this.list.Insert(index, sortExpression);
		}

		// Token: 0x0600B624 RID: 46628 RVA: 0x0028136C File Offset: 0x0027F56C
		public void RemoveSortExpression(GridSortExpression sortExpression)
		{
			if (this.ContainsSortExpression(sortExpression))
			{
				this.list.Remove(sortExpression);
			}
		}

		// Token: 0x0600B625 RID: 46629 RVA: 0x00281384 File Offset: 0x0027F584
		public bool ContainsSortExpression(GridSortExpression sortExpression)
		{
			int num = this.list.IndexOf(sortExpression);
			return num != -1;
		}

		// Token: 0x0600B626 RID: 46630 RVA: 0x002813A8 File Offset: 0x0027F5A8
		public bool ContainsExpression(string expression)
		{
			GridSortExpression gridSortExpression = new GridSortExpression();
			gridSortExpression.FieldName = expression;
			int num = this.list.IndexOf(gridSortExpression);
			return num != -1;
		}

		// Token: 0x17003AE7 RID: 15079
		// (get) Token: 0x0600B627 RID: 46631 RVA: 0x002813D6 File Offset: 0x0027F5D6
		// (set) Token: 0x0600B628 RID: 46632 RVA: 0x002813E9 File Offset: 0x0027F5E9
		[Description("Switch to 'no sort' state after new sorting when descending order")]
		[Browsable(false)]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue(true)]
		public bool AllowNaturalSort
		{
			get
			{
				return this.StateManager.ViewStateGetBool("_ans", true);
			}
			set
			{
				this.StateManager.ViewState["_ans"] = value;
			}
		}

		// Token: 0x0600B629 RID: 46633 RVA: 0x00281408 File Offset: 0x0027F608
		public void ChangeSortOrder(string expression)
		{
			if (string.IsNullOrEmpty(expression))
			{
				return;
			}
			GridSortExpression gridSortExpression = new GridSortExpression();
			gridSortExpression.FieldName = expression;
			if (!this.ContainsSortExpression(gridSortExpression))
			{
				if (!this.AllowMultiColumnSorting)
				{
					this.list.Clear();
				}
				this.list.Add(gridSortExpression);
				if (this._tableView != null)
				{
					this._tableView.OwnerGrid.TrackSorting(gridSortExpression.ToString());
					return;
				}
			}
			else
			{
				GridSortExpression gridSortExpression2 = (GridSortExpression)this.list[this.list.IndexOf(gridSortExpression)];
				if (gridSortExpression2.SortOrder == GridSortOrder.Ascending)
				{
					gridSortExpression2.SortOrder = GridSortOrder.Descending;
				}
				else if (gridSortExpression2.SortOrder == GridSortOrder.None)
				{
					gridSortExpression2.SortOrder = GridSortOrder.Ascending;
				}
				else if (gridSortExpression2.SortOrder == GridSortOrder.Descending)
				{
					if (this.AllowNaturalSort)
					{
						gridSortExpression2.SortOrder = GridSortOrder.None;
					}
					else
					{
						gridSortExpression2.SortOrder = GridSortOrder.Ascending;
					}
				}
				if (!this.AllowMultiColumnSorting)
				{
					this.list.Clear();
					if (gridSortExpression2.SortOrder != GridSortOrder.None)
					{
						this.list.Add(gridSortExpression2);
					}
				}
				else if (gridSortExpression2.SortOrder == GridSortOrder.None)
				{
					this.list.Remove(gridSortExpression2);
				}
				if (this._tableView != null)
				{
					this._tableView.OwnerGrid.TrackSorting(gridSortExpression2.ToString());
				}
			}
		}

		// Token: 0x0600B62A RID: 46634 RVA: 0x00281533 File Offset: 0x0027F733
		internal ArrayList GetList()
		{
			return this.list;
		}

		// Token: 0x0600B62B RID: 46635 RVA: 0x0028153C File Offset: 0x0027F73C
		public string GetSortString()
		{
			string text = null;
			foreach (object obj in this)
			{
				GridSortExpression gridSortExpression = (GridSortExpression)obj;
				if (gridSortExpression.SortOrder != GridSortOrder.None)
				{
					text = text + gridSortExpression.ToString() + ", ";
				}
			}
			if (text != null)
			{
				text = text.TrimEnd(new char[]
				{
					',',
					' '
				});
			}
			return text;
		}

		// Token: 0x0600B62C RID: 46636 RVA: 0x002815C8 File Offset: 0x0027F7C8
		internal string GetEFSortString()
		{
			string text = null;
			foreach (object obj in this)
			{
				GridSortExpression gridSortExpression = (GridSortExpression)obj;
				if (gridSortExpression.SortOrder != GridSortOrder.None)
				{
					text += string.Format("it.{0}, ", gridSortExpression.ToString());
				}
			}
			if (text != null)
			{
				text = text.TrimEnd(new char[]
				{
					',',
					' '
				});
			}
			return text;
		}

		// Token: 0x0600B62D RID: 46637 RVA: 0x00281658 File Offset: 0x0027F858
		internal string GetGroupingSortString()
		{
			string text = null;
			foreach (object obj in this)
			{
				GridSortExpression gridSortExpression = (GridSortExpression)obj;
				text = text + gridSortExpression.ToString() + ", ";
			}
			if (text != null)
			{
				text = text.TrimEnd(new char[]
				{
					',',
					' '
				});
			}
			return text;
		}

		// Token: 0x0600B62E RID: 46638 RVA: 0x002816DC File Offset: 0x0027F8DC
		public int IndexOf(GridSortExpression gridSortExpression)
		{
			return this.list.IndexOf(gridSortExpression);
		}

		// Token: 0x0600B62F RID: 46639 RVA: 0x002816EC File Offset: 0x0027F8EC
		void IStateManager.LoadViewState(object state)
		{
			this.StateManager.LoadViewState(state);
			this.list.Clear();
			int num = this.StateManager.ViewStateGetInt("_c");
			for (int i = 0; i < num; i++)
			{
				GridSortExpression gridSortExpression = new GridSortExpression();
				this.list.Add(gridSortExpression);
				this.StateManager.AddManager(gridSortExpression, i.ToString());
			}
			this.StateManager.LoadViewState(state);
		}

		// Token: 0x0600B630 RID: 46640 RVA: 0x00281760 File Offset: 0x0027F960
		object IStateManager.SaveViewState()
		{
			this.StateManager.ViewState["_c"] = this.Count;
			int num = 0;
			foreach (object obj in this)
			{
				GridSortExpression manager = (GridSortExpression)obj;
				this.StateManager.AddManager(manager, num.ToString());
				num++;
			}
			return this.StateManager.SaveViewState();
		}

		// Token: 0x0600B631 RID: 46641 RVA: 0x002817F4 File Offset: 0x0027F9F4
		void IStateManager.TrackViewState()
		{
			this.StateManager.TrackViewState();
		}

		// Token: 0x17003AE8 RID: 15080
		// (get) Token: 0x0600B632 RID: 46642 RVA: 0x00281801 File Offset: 0x0027FA01
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this.StateManager.IsTrackingViewState;
			}
		}

		// Token: 0x04002FFC RID: 12284
		private ArrayList list;

		// Token: 0x04002FFD RID: 12285
		private GridStateManager StateManager = new GridStateManager();

		// Token: 0x04002FFE RID: 12286
		internal GridTableView _tableView;
	}
}
