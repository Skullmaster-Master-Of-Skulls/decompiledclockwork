using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI;
using Telerik.Web.Data;

namespace Telerik.Web.UI
{
	// Token: 0x020019C7 RID: 6599
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[Serializable]
	public class RadListViewSortExpressionCollection : IList, ICollection, IEnumerable, IStateManager
	{
		// Token: 0x0600FEC4 RID: 65220 RVA: 0x003931FE File Offset: 0x003913FE
		public RadListViewSortExpressionCollection() : this(new ArrayList())
		{
		}

		// Token: 0x0600FEC5 RID: 65221 RVA: 0x0039320B File Offset: 0x0039140B
		public RadListViewSortExpressionCollection(ArrayList list)
		{
			this.list = list;
			this._stateManager = new GridStateManager();
		}

		// Token: 0x17004CE3 RID: 19683
		// (get) Token: 0x0600FEC6 RID: 65222 RVA: 0x00393228 File Offset: 0x00391428
		// (set) Token: 0x0600FEC7 RID: 65223 RVA: 0x0039325B File Offset: 0x0039145B
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool AllowMultiFieldSorting
		{
			get
			{
				object obj = this._stateManager.ViewState["AllowMultiFieldSorting"] ?? false;
				return (bool)obj;
			}
			set
			{
				this._stateManager.ViewState["AllowMultiFieldSorting"] = value;
			}
		}

		// Token: 0x17004CE4 RID: 19684
		public RadListViewSortExpression this[int index]
		{
			get
			{
				return (RadListViewSortExpression)this.list[index];
			}
			set
			{
				this.list[index] = value;
			}
		}

		// Token: 0x17004CE5 RID: 19685
		// (get) Token: 0x0600FECA RID: 65226 RVA: 0x0039329C File Offset: 0x0039149C
		// (set) Token: 0x0600FECB RID: 65227 RVA: 0x003932CA File Offset: 0x003914CA
		[Browsable(false)]
		[DefaultValue(true)]
		[Description("Switch to 'no sort' state after new sorting when descending order")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[NotifyParentProperty(true)]
		public bool AllowNaturalSort
		{
			get
			{
				object obj = this._stateManager.ViewState["AllowNaturalSort"];
				return obj != null && (bool)obj;
			}
			set
			{
				this._stateManager.ViewState["AllowNaturalSort"] = value;
			}
		}

		// Token: 0x0600FECC RID: 65228 RVA: 0x003932E7 File Offset: 0x003914E7
		public void CopyTo(Array array, int index)
		{
			this.list.CopyTo(array, index);
		}

		// Token: 0x0600FECD RID: 65229 RVA: 0x003932F6 File Offset: 0x003914F6
		public IEnumerator GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		// Token: 0x17004CE6 RID: 19686
		// (get) Token: 0x0600FECE RID: 65230 RVA: 0x00393303 File Offset: 0x00391503
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		// Token: 0x17004CE7 RID: 19687
		// (get) Token: 0x0600FECF RID: 65231 RVA: 0x00393310 File Offset: 0x00391510
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public bool IsSynchronized
		{
			get
			{
				return this.list.IsSynchronized;
			}
		}

		// Token: 0x17004CE8 RID: 19688
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

		// Token: 0x0600FED2 RID: 65234 RVA: 0x0039333A File Offset: 0x0039153A
		public int Add(object value)
		{
			return this.AddEx((RadListViewSortExpression)value);
		}

		// Token: 0x0600FED3 RID: 65235 RVA: 0x00393348 File Offset: 0x00391548
		bool IList.Contains(object value)
		{
			return this.ContainsSortExpression((RadListViewSortExpression)value);
		}

		// Token: 0x0600FED4 RID: 65236 RVA: 0x00393356 File Offset: 0x00391556
		int IList.IndexOf(object value)
		{
			return this.list.IndexOf((RadListViewSortExpression)value);
		}

		// Token: 0x0600FED5 RID: 65237 RVA: 0x00393369 File Offset: 0x00391569
		void IList.Insert(int index, object value)
		{
			this.list.Insert(index, (RadListViewSortExpression)value);
		}

		// Token: 0x17004CE9 RID: 19689
		// (get) Token: 0x0600FED6 RID: 65238 RVA: 0x0039337D File Offset: 0x0039157D
		bool IList.IsFixedSize
		{
			get
			{
				return this.list.IsFixedSize;
			}
		}

		// Token: 0x17004CEA RID: 19690
		// (get) Token: 0x0600FED7 RID: 65239 RVA: 0x0039338A File Offset: 0x0039158A
		bool IList.IsReadOnly
		{
			get
			{
				return this.list.IsReadOnly;
			}
		}

		// Token: 0x0600FED8 RID: 65240 RVA: 0x00393397 File Offset: 0x00391597
		void IList.Remove(object value)
		{
			this.RemoveSortExpression((RadListViewSortExpression)value);
		}

		// Token: 0x0600FED9 RID: 65241 RVA: 0x003933A5 File Offset: 0x003915A5
		void IList.RemoveAt(int index)
		{
			this.list.RemoveAt(index);
		}

		// Token: 0x0600FEDA RID: 65242 RVA: 0x003933B3 File Offset: 0x003915B3
		public void Clear()
		{
			this.list.Clear();
		}

		// Token: 0x17004CEB RID: 19691
		// (get) Token: 0x0600FEDB RID: 65243 RVA: 0x003933C0 File Offset: 0x003915C0
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public object SyncRoot
		{
			get
			{
				return this.list.SyncRoot;
			}
		}

		// Token: 0x0600FEDC RID: 65244 RVA: 0x003933D0 File Offset: 0x003915D0
		void IStateManager.LoadViewState(object state)
		{
			((IStateManager)this._stateManager).LoadViewState(state);
			this.list.Clear();
			int num = this._stateManager.ViewStateGetInt("_c");
			for (int i = 0; i < num; i++)
			{
				RadListViewSortExpression radListViewSortExpression = new RadListViewSortExpression();
				this.list.Add(radListViewSortExpression);
				this._stateManager.AddManager(radListViewSortExpression, i.ToString());
			}
			this._stateManager.LoadViewState(state);
		}

		// Token: 0x0600FEDD RID: 65245 RVA: 0x00393444 File Offset: 0x00391644
		object IStateManager.SaveViewState()
		{
			this._stateManager.ViewState["_c"] = this.Count;
			int num = 0;
			foreach (object obj in this)
			{
				RadListViewSortExpression manager = (RadListViewSortExpression)obj;
				this._stateManager.AddManager(manager, num.ToString());
				num++;
			}
			return this._stateManager.SaveViewState();
		}

		// Token: 0x0600FEDE RID: 65246 RVA: 0x003934D8 File Offset: 0x003916D8
		void IStateManager.TrackViewState()
		{
			this._stateManager.TrackViewState();
		}

		// Token: 0x17004CEC RID: 19692
		// (get) Token: 0x0600FEDF RID: 65247 RVA: 0x003934E5 File Offset: 0x003916E5
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this._stateManager.IsTrackingViewState;
			}
		}

		// Token: 0x0600FEE0 RID: 65248 RVA: 0x003934F4 File Offset: 0x003916F4
		public void CopyTo(RadListViewSortExpressionCollection dest)
		{
			foreach (object obj in this)
			{
				RadListViewSortExpression radListViewSortExpression = (RadListViewSortExpression)obj;
				dest.Add(radListViewSortExpression.Clone());
			}
		}

		// Token: 0x0600FEE1 RID: 65249 RVA: 0x00393550 File Offset: 0x00391750
		public RadListViewSortExpression GetExpression(string expression)
		{
			RadListViewSortExpression value = new RadListViewSortExpression
			{
				FieldName = expression
			};
			return (RadListViewSortExpression)this.list[this.list.IndexOf(value)];
		}

		// Token: 0x0600FEE2 RID: 65250 RVA: 0x00393588 File Offset: 0x00391788
		private int AddEx(RadListViewSortExpression sortExpression)
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
			if (!this.AllowMultiFieldSorting)
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
				RadListViewSortExpression radListViewSortExpression = (RadListViewSortExpression)this.list[num];
				radListViewSortExpression.SortOrder = sortExpression.SortOrder;
			}
			return num;
		}

		// Token: 0x0600FEE3 RID: 65251 RVA: 0x00393602 File Offset: 0x00391802
		public void AddSortExpression(RadListViewSortExpression sortExpression)
		{
			this.AddEx(sortExpression);
		}

		// Token: 0x0600FEE4 RID: 65252 RVA: 0x0039360C File Offset: 0x0039180C
		public void AddSortExpression(string expression)
		{
			this.AddEx(RadListViewSortExpression.Parse(expression));
		}

		// Token: 0x0600FEE5 RID: 65253 RVA: 0x0039361C File Offset: 0x0039181C
		public void AddAt(int index, RadListViewSortExpression sortExpression)
		{
			int num = this.list.IndexOf(sortExpression);
			if (num >= 0)
			{
				this.RemoveSortExpression(sortExpression);
			}
			if (index > 0)
			{
				this.AllowMultiFieldSorting = true;
			}
			if (num >= 0 && num <= index && index != 0)
			{
				this.list.Insert(index - 1, sortExpression);
				return;
			}
			this.list.Insert(index, sortExpression);
		}

		// Token: 0x0600FEE6 RID: 65254 RVA: 0x00393674 File Offset: 0x00391874
		public void RemoveSortExpression(RadListViewSortExpression sortExpression)
		{
			if (this.ContainsSortExpression(sortExpression))
			{
				this.list.Remove(sortExpression);
			}
		}

		// Token: 0x0600FEE7 RID: 65255 RVA: 0x0039368C File Offset: 0x0039188C
		public bool ContainsSortExpression(RadListViewSortExpression sortExpression)
		{
			int num = this.list.IndexOf(sortExpression);
			return num != -1;
		}

		// Token: 0x0600FEE8 RID: 65256 RVA: 0x003936B0 File Offset: 0x003918B0
		public bool ContainsExpression(string expression)
		{
			RadListViewSortExpression value = new RadListViewSortExpression
			{
				FieldName = expression
			};
			int num = this.list.IndexOf(value);
			return num != -1;
		}

		// Token: 0x0600FEE9 RID: 65257 RVA: 0x003936E0 File Offset: 0x003918E0
		public void ChangeSortOrder(string expression)
		{
			if (string.IsNullOrEmpty(expression))
			{
				return;
			}
			RadListViewSortExpression radListViewSortExpression = new RadListViewSortExpression();
			radListViewSortExpression.FieldName = expression;
			if (!this.ContainsSortExpression(radListViewSortExpression))
			{
				if (!this.AllowMultiFieldSorting)
				{
					this.list.Clear();
				}
				this.list.Add(radListViewSortExpression);
				return;
			}
			RadListViewSortExpression radListViewSortExpression2 = (RadListViewSortExpression)this.list[this.list.IndexOf(radListViewSortExpression)];
			if (radListViewSortExpression2.SortOrder == RadListViewSortOrder.Ascending)
			{
				radListViewSortExpression2.SortOrder = RadListViewSortOrder.Descending;
			}
			else if (radListViewSortExpression2.SortOrder == RadListViewSortOrder.None)
			{
				radListViewSortExpression2.SortOrder = RadListViewSortOrder.Ascending;
			}
			else if (radListViewSortExpression2.SortOrder == RadListViewSortOrder.Descending)
			{
				if (this.AllowNaturalSort)
				{
					radListViewSortExpression2.SortOrder = RadListViewSortOrder.None;
				}
				else
				{
					radListViewSortExpression2.SortOrder = RadListViewSortOrder.Ascending;
				}
			}
			if (!this.AllowMultiFieldSorting)
			{
				this.list.Clear();
				if (radListViewSortExpression2.SortOrder != RadListViewSortOrder.None)
				{
					this.list.Add(radListViewSortExpression2);
					return;
				}
			}
			else if (radListViewSortExpression2.SortOrder == RadListViewSortOrder.None)
			{
				this.list.Remove(radListViewSortExpression2);
			}
		}

		// Token: 0x0600FEEA RID: 65258 RVA: 0x003937CB File Offset: 0x003919CB
		internal ArrayList GetList()
		{
			return this.list;
		}

		// Token: 0x0600FEEB RID: 65259 RVA: 0x003937D4 File Offset: 0x003919D4
		public string GetSortString()
		{
			string text = null;
			foreach (object obj in this)
			{
				RadListViewSortExpression radListViewSortExpression = (RadListViewSortExpression)obj;
				if (radListViewSortExpression.SortOrder != RadListViewSortOrder.None)
				{
					text = text + radListViewSortExpression.ToString() + ", ";
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

		// Token: 0x0600FEEC RID: 65260 RVA: 0x00393860 File Offset: 0x00391A60
		internal string GetEFSortString()
		{
			string text = null;
			foreach (object obj in this)
			{
				RadListViewSortExpression radListViewSortExpression = (RadListViewSortExpression)obj;
				if (radListViewSortExpression.SortOrder != RadListViewSortOrder.None)
				{
					text += string.Format("it.{0}, ", radListViewSortExpression);
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

		// Token: 0x0600FEED RID: 65261 RVA: 0x003938EC File Offset: 0x00391AEC
		internal IList<SortDescriptor> GetSortDescriptors()
		{
			List<SortDescriptor> list = new List<SortDescriptor>();
			foreach (object obj in this)
			{
				RadListViewSortExpression radListViewSortExpression = (RadListViewSortExpression)obj;
				if (radListViewSortExpression.SortOrder != RadListViewSortOrder.None)
				{
					list.Add(new SortDescriptor
					{
						Member = radListViewSortExpression.FieldName,
						SortDirection = this.ConvertToSortDirection(radListViewSortExpression.SortOrder)
					});
				}
			}
			return list;
		}

		// Token: 0x0600FEEE RID: 65262 RVA: 0x00393978 File Offset: 0x00391B78
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		private ListSortDirection ConvertToSortDirection(RadListViewSortOrder order)
		{
			if (order == RadListViewSortOrder.Descending)
			{
				return ListSortDirection.Descending;
			}
			return ListSortDirection.Ascending;
		}

		// Token: 0x0600FEEF RID: 65263 RVA: 0x00393984 File Offset: 0x00391B84
		internal string GetGroupingSortString()
		{
			string text = null;
			foreach (object obj in this)
			{
				RadListViewSortExpression radListViewSortExpression = (RadListViewSortExpression)obj;
				text = text + radListViewSortExpression.ToString() + ", ";
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

		// Token: 0x0600FEF0 RID: 65264 RVA: 0x00393A08 File Offset: 0x00391C08
		public int IndexOf(RadListViewSortExpression viewSortExpression)
		{
			return this.list.IndexOf(viewSortExpression);
		}

		// Token: 0x0400484D RID: 18509
		private readonly GridStateManager _stateManager;

		// Token: 0x0400484E RID: 18510
		private ArrayList list;
	}
}
