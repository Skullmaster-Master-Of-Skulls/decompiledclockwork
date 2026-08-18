using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02001280 RID: 4736
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[SuppressMessage("Microsoft.Design", "CA1010:CollectionsShouldImplementGenericInterface")]
	[Serializable]
	public class TreeListSortExpressionCollection : IList, ICollection, IEnumerable, IStateManager
	{
		// Token: 0x0600C571 RID: 50545 RVA: 0x002C154B File Offset: 0x002BF74B
		public TreeListSortExpressionCollection() : this(new ArrayList())
		{
		}

		// Token: 0x0600C572 RID: 50546 RVA: 0x002C1558 File Offset: 0x002BF758
		public TreeListSortExpressionCollection(ArrayList list)
		{
			this.list = list;
			this._stateManager = new TreeListControlStateManager();
		}

		// Token: 0x17003FBF RID: 16319
		// (get) Token: 0x0600C573 RID: 50547 RVA: 0x002C1574 File Offset: 0x002BF774
		// (set) Token: 0x0600C574 RID: 50548 RVA: 0x002C15A2 File Offset: 0x002BF7A2
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public bool AllowMultiColumnSorting
		{
			get
			{
				object obj = this._stateManager["AllowMultiColumnSorting"] ?? false;
				return (bool)obj;
			}
			set
			{
				this._stateManager["AllowMultiColumnSorting"] = value;
			}
		}

		// Token: 0x17003FC0 RID: 16320
		public TreeListSortExpression this[int index]
		{
			get
			{
				return (TreeListSortExpression)this.list[index];
			}
			set
			{
				this.list[index] = value;
			}
		}

		// Token: 0x17003FC1 RID: 16321
		// (get) Token: 0x0600C577 RID: 50551 RVA: 0x002C15DC File Offset: 0x002BF7DC
		// (set) Token: 0x0600C578 RID: 50552 RVA: 0x002C1605 File Offset: 0x002BF805
		[Description("Switch to 'no sort' state after new sorting when descending order")]
		[NotifyParentProperty(true)]
		[DefaultValue(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public bool AllowNaturalSort
		{
			get
			{
				object obj = this._stateManager["AllowNaturalSort"];
				return obj == null || (bool)obj;
			}
			set
			{
				this._stateManager["AllowNaturalSort"] = value;
			}
		}

		// Token: 0x0600C579 RID: 50553 RVA: 0x002C1620 File Offset: 0x002BF820
		void IStateManager.LoadViewState(object state)
		{
			((IStateManager)this._stateManager).LoadViewState(state);
			this.list.Clear();
			int num = Convert.ToInt32(this._stateManager["count"]);
			for (int i = 0; i < num; i++)
			{
				TreeListSortExpression treeListSortExpression = new TreeListSortExpression();
				this.list.Add(treeListSortExpression);
				((IStateManager)treeListSortExpression).LoadViewState(this._stateManager[i.ToString()]);
				if (((IStateManager)this).IsTrackingViewState)
				{
					((IStateManager)treeListSortExpression).TrackViewState();
				}
			}
		}

		// Token: 0x0600C57A RID: 50554 RVA: 0x002C16A0 File Offset: 0x002BF8A0
		object IStateManager.SaveViewState()
		{
			this._stateManager["count"] = this.Count;
			int num = 0;
			foreach (object obj in this)
			{
				TreeListSortExpression treeListSortExpression = (TreeListSortExpression)obj;
				this._stateManager[num.ToString()] = ((IStateManager)treeListSortExpression).SaveViewState();
				num++;
			}
			return ((IStateManager)this._stateManager).SaveViewState();
		}

		// Token: 0x0600C57B RID: 50555 RVA: 0x002C1734 File Offset: 0x002BF934
		void IStateManager.TrackViewState()
		{
			((IStateManager)this._stateManager).TrackViewState();
		}

		// Token: 0x17003FC2 RID: 16322
		// (get) Token: 0x0600C57C RID: 50556 RVA: 0x002C1741 File Offset: 0x002BF941
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return ((IStateManager)this._stateManager).IsTrackingViewState;
			}
		}

		// Token: 0x0600C57D RID: 50557 RVA: 0x002C174E File Offset: 0x002BF94E
		public void CopyTo(Array array, int index)
		{
			this.list.CopyTo(array, index);
		}

		// Token: 0x0600C57E RID: 50558 RVA: 0x002C175D File Offset: 0x002BF95D
		public IEnumerator GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		// Token: 0x17003FC3 RID: 16323
		// (get) Token: 0x0600C57F RID: 50559 RVA: 0x002C176A File Offset: 0x002BF96A
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		// Token: 0x17003FC4 RID: 16324
		// (get) Token: 0x0600C580 RID: 50560 RVA: 0x002C1777 File Offset: 0x002BF977
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool IsSynchronized
		{
			get
			{
				return this.list.IsSynchronized;
			}
		}

		// Token: 0x17003FC5 RID: 16325
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

		// Token: 0x0600C583 RID: 50563 RVA: 0x002C17A1 File Offset: 0x002BF9A1
		public int Add(object value)
		{
			return this.AddEx((TreeListSortExpression)value);
		}

		// Token: 0x0600C584 RID: 50564 RVA: 0x002C17AF File Offset: 0x002BF9AF
		bool IList.Contains(object value)
		{
			return this.ContainsSortExpression((TreeListSortExpression)value);
		}

		// Token: 0x0600C585 RID: 50565 RVA: 0x002C17BD File Offset: 0x002BF9BD
		int IList.IndexOf(object value)
		{
			return this.list.IndexOf((TreeListSortExpression)value);
		}

		// Token: 0x0600C586 RID: 50566 RVA: 0x002C17D0 File Offset: 0x002BF9D0
		void IList.Insert(int index, object value)
		{
			this.list.Insert(index, (TreeListSortExpression)value);
		}

		// Token: 0x17003FC6 RID: 16326
		// (get) Token: 0x0600C587 RID: 50567 RVA: 0x002C17E4 File Offset: 0x002BF9E4
		bool IList.IsFixedSize
		{
			get
			{
				return this.list.IsFixedSize;
			}
		}

		// Token: 0x17003FC7 RID: 16327
		// (get) Token: 0x0600C588 RID: 50568 RVA: 0x002C17F1 File Offset: 0x002BF9F1
		bool IList.IsReadOnly
		{
			get
			{
				return this.list.IsReadOnly;
			}
		}

		// Token: 0x0600C589 RID: 50569 RVA: 0x002C17FE File Offset: 0x002BF9FE
		void IList.Remove(object value)
		{
			this.RemoveSortExpression((TreeListSortExpression)value);
		}

		// Token: 0x0600C58A RID: 50570 RVA: 0x002C180C File Offset: 0x002BFA0C
		void IList.RemoveAt(int index)
		{
			this.list.RemoveAt(index);
		}

		// Token: 0x0600C58B RID: 50571 RVA: 0x002C181A File Offset: 0x002BFA1A
		public void Clear()
		{
			this.list.Clear();
		}

		// Token: 0x17003FC8 RID: 16328
		// (get) Token: 0x0600C58C RID: 50572 RVA: 0x002C1827 File Offset: 0x002BFA27
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public object SyncRoot
		{
			get
			{
				return this.list.SyncRoot;
			}
		}

		// Token: 0x0600C58D RID: 50573 RVA: 0x002C1834 File Offset: 0x002BFA34
		public void CopyTo(TreeListSortExpressionCollection dest)
		{
			foreach (object obj in this)
			{
				TreeListSortExpression treeListSortExpression = (TreeListSortExpression)obj;
				dest.Add(treeListSortExpression.Clone());
			}
		}

		// Token: 0x0600C58E RID: 50574 RVA: 0x002C1890 File Offset: 0x002BFA90
		public TreeListSortExpression GetExpression(string expression)
		{
			TreeListSortExpression value = new TreeListSortExpression
			{
				FieldName = expression
			};
			return (TreeListSortExpression)this.list[this.list.IndexOf(value)];
		}

		// Token: 0x0600C58F RID: 50575 RVA: 0x002C18C8 File Offset: 0x002BFAC8
		private int AddEx(TreeListSortExpression sortExpression)
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
				TreeListSortExpression treeListSortExpression = (TreeListSortExpression)this.list[num];
				treeListSortExpression.SortOrder = sortExpression.SortOrder;
			}
			return num;
		}

		// Token: 0x0600C590 RID: 50576 RVA: 0x002C1942 File Offset: 0x002BFB42
		public void AddSortExpression(TreeListSortExpression sortExpression)
		{
			this.AddEx(sortExpression);
		}

		// Token: 0x0600C591 RID: 50577 RVA: 0x002C194C File Offset: 0x002BFB4C
		public void AddSortExpression(string expression)
		{
			this.AddEx(TreeListSortExpression.Parse(expression));
		}

		// Token: 0x0600C592 RID: 50578 RVA: 0x002C195C File Offset: 0x002BFB5C
		public void AddAt(int index, TreeListSortExpression sortExpression)
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

		// Token: 0x0600C593 RID: 50579 RVA: 0x002C19B4 File Offset: 0x002BFBB4
		public void RemoveSortExpression(TreeListSortExpression sortExpression)
		{
			if (this.ContainsSortExpression(sortExpression))
			{
				this.list.Remove(sortExpression);
			}
		}

		// Token: 0x0600C594 RID: 50580 RVA: 0x002C19CC File Offset: 0x002BFBCC
		public bool ContainsSortExpression(TreeListSortExpression sortExpression)
		{
			int num = this.list.IndexOf(sortExpression);
			return num != -1;
		}

		// Token: 0x0600C595 RID: 50581 RVA: 0x002C19F0 File Offset: 0x002BFBF0
		public bool ContainsExpression(string expression)
		{
			TreeListSortExpression value = new TreeListSortExpression
			{
				FieldName = expression
			};
			int num = this.list.IndexOf(value);
			return num != -1;
		}

		// Token: 0x0600C596 RID: 50582 RVA: 0x002C1A20 File Offset: 0x002BFC20
		public void ChangeSortOrder(string expression)
		{
			if (string.IsNullOrEmpty(expression))
			{
				return;
			}
			TreeListSortExpression treeListSortExpression = new TreeListSortExpression();
			treeListSortExpression.FieldName = expression;
			if (!this.ContainsSortExpression(treeListSortExpression))
			{
				if (!this.AllowMultiColumnSorting)
				{
					this.list.Clear();
				}
				this.list.Add(treeListSortExpression);
				return;
			}
			TreeListSortExpression treeListSortExpression2 = (TreeListSortExpression)this.list[this.list.IndexOf(treeListSortExpression)];
			if (treeListSortExpression2.SortOrder == TreeListSortOrder.Ascending)
			{
				treeListSortExpression2.SortOrder = TreeListSortOrder.Descending;
			}
			else if (treeListSortExpression2.SortOrder == TreeListSortOrder.None)
			{
				treeListSortExpression2.SortOrder = TreeListSortOrder.Ascending;
			}
			else if (treeListSortExpression2.SortOrder == TreeListSortOrder.Descending)
			{
				if (this.AllowNaturalSort)
				{
					treeListSortExpression2.SortOrder = TreeListSortOrder.None;
				}
				else
				{
					treeListSortExpression2.SortOrder = TreeListSortOrder.Ascending;
				}
			}
			if (!this.AllowMultiColumnSorting)
			{
				this.list.Clear();
				if (treeListSortExpression2.SortOrder != TreeListSortOrder.None)
				{
					this.list.Add(treeListSortExpression2);
					return;
				}
			}
			else if (treeListSortExpression2.SortOrder == TreeListSortOrder.None)
			{
				this.list.Remove(treeListSortExpression2);
			}
		}

		// Token: 0x0600C597 RID: 50583 RVA: 0x002C1B0B File Offset: 0x002BFD0B
		internal ArrayList GetList()
		{
			return this.list;
		}

		// Token: 0x0600C598 RID: 50584 RVA: 0x002C1B14 File Offset: 0x002BFD14
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		public string GetSortString()
		{
			string text = null;
			foreach (object obj in this)
			{
				TreeListSortExpression treeListSortExpression = (TreeListSortExpression)obj;
				if (treeListSortExpression.SortOrder != TreeListSortOrder.None)
				{
					text = text + treeListSortExpression.ToString() + ", ";
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

		// Token: 0x0600C599 RID: 50585 RVA: 0x002C1BA0 File Offset: 0x002BFDA0
		public int IndexOf(TreeListSortExpression viewSortExpression)
		{
			return this.list.IndexOf(viewSortExpression);
		}

		// Token: 0x04003433 RID: 13363
		private readonly TreeListControlStateManager _stateManager;

		// Token: 0x04003434 RID: 13364
		private ArrayList list;
	}
}
