using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000E11 RID: 3601
	[SuppressMessage("Microsoft.Design", "CA1010:CollectionsShouldImplementGenericInterface")]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[Serializable]
	public class PivotGridSortExpressionCollection : IList, ICollection, IEnumerable, IStateManager
	{
		// Token: 0x060085CD RID: 34253 RVA: 0x001E7D03 File Offset: 0x001E5F03
		public PivotGridSortExpressionCollection() : this(new ArrayList())
		{
		}

		// Token: 0x060085CE RID: 34254 RVA: 0x001E7D10 File Offset: 0x001E5F10
		public PivotGridSortExpressionCollection(ArrayList list)
		{
			this.list = list;
			this._stateManager = new PivotGridControlStateManager();
		}

		// Token: 0x17002A5E RID: 10846
		public PivotGridSortExpression this[int index]
		{
			get
			{
				return (PivotGridSortExpression)this.list[index];
			}
			set
			{
				this.list[index] = value;
			}
		}

		// Token: 0x060085D1 RID: 34257 RVA: 0x001E7D4C File Offset: 0x001E5F4C
		void IStateManager.LoadViewState(object state)
		{
			((IStateManager)this._stateManager).LoadViewState(state);
			this.list.Clear();
			int num = Convert.ToInt32(this._stateManager["count"]);
			for (int i = 0; i < num; i++)
			{
				PivotGridSortExpression pivotGridSortExpression = new PivotGridSortExpression();
				this.list.Add(pivotGridSortExpression);
				((IStateManager)pivotGridSortExpression).LoadViewState(this._stateManager[i.ToString()]);
				if (((IStateManager)this).IsTrackingViewState)
				{
					((IStateManager)pivotGridSortExpression).TrackViewState();
				}
			}
		}

		// Token: 0x060085D2 RID: 34258 RVA: 0x001E7DCC File Offset: 0x001E5FCC
		object IStateManager.SaveViewState()
		{
			this._stateManager["count"] = this.Count;
			int num = 0;
			foreach (object obj in this)
			{
				PivotGridSortExpression pivotGridSortExpression = (PivotGridSortExpression)obj;
				this._stateManager[num.ToString()] = ((IStateManager)pivotGridSortExpression).SaveViewState();
				num++;
			}
			return ((IStateManager)this._stateManager).SaveViewState();
		}

		// Token: 0x060085D3 RID: 34259 RVA: 0x001E7E60 File Offset: 0x001E6060
		void IStateManager.TrackViewState()
		{
			((IStateManager)this._stateManager).TrackViewState();
		}

		// Token: 0x17002A5F RID: 10847
		// (get) Token: 0x060085D4 RID: 34260 RVA: 0x001E7E6D File Offset: 0x001E606D
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return ((IStateManager)this._stateManager).IsTrackingViewState;
			}
		}

		// Token: 0x060085D5 RID: 34261 RVA: 0x001E7E7A File Offset: 0x001E607A
		public void CopyTo(Array array, int index)
		{
			this.list.CopyTo(array, index);
		}

		// Token: 0x060085D6 RID: 34262 RVA: 0x001E7E89 File Offset: 0x001E6089
		public IEnumerator GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		// Token: 0x17002A60 RID: 10848
		// (get) Token: 0x060085D7 RID: 34263 RVA: 0x001E7E96 File Offset: 0x001E6096
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		// Token: 0x17002A61 RID: 10849
		// (get) Token: 0x060085D8 RID: 34264 RVA: 0x001E7EA3 File Offset: 0x001E60A3
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool IsSynchronized
		{
			get
			{
				return this.list.IsSynchronized;
			}
		}

		// Token: 0x17002A62 RID: 10850
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

		// Token: 0x060085DB RID: 34267 RVA: 0x001E7ECD File Offset: 0x001E60CD
		public int Add(object value)
		{
			return this.AddEx((PivotGridSortExpression)value);
		}

		// Token: 0x060085DC RID: 34268 RVA: 0x001E7EDB File Offset: 0x001E60DB
		bool IList.Contains(object value)
		{
			return this.ContainsSortExpression((PivotGridSortExpression)value);
		}

		// Token: 0x060085DD RID: 34269 RVA: 0x001E7EE9 File Offset: 0x001E60E9
		int IList.IndexOf(object value)
		{
			return this.list.IndexOf((PivotGridSortExpression)value);
		}

		// Token: 0x060085DE RID: 34270 RVA: 0x001E7EFC File Offset: 0x001E60FC
		void IList.Insert(int index, object value)
		{
			this.list.Insert(index, (PivotGridSortExpression)value);
		}

		// Token: 0x17002A63 RID: 10851
		// (get) Token: 0x060085DF RID: 34271 RVA: 0x001E7F10 File Offset: 0x001E6110
		bool IList.IsFixedSize
		{
			get
			{
				return this.list.IsFixedSize;
			}
		}

		// Token: 0x17002A64 RID: 10852
		// (get) Token: 0x060085E0 RID: 34272 RVA: 0x001E7F1D File Offset: 0x001E611D
		bool IList.IsReadOnly
		{
			get
			{
				return this.list.IsReadOnly;
			}
		}

		// Token: 0x060085E1 RID: 34273 RVA: 0x001E7F2A File Offset: 0x001E612A
		void IList.Remove(object value)
		{
			this.RemoveSortExpression((PivotGridSortExpression)value);
		}

		// Token: 0x060085E2 RID: 34274 RVA: 0x001E7F38 File Offset: 0x001E6138
		void IList.RemoveAt(int index)
		{
			this.list.RemoveAt(index);
		}

		// Token: 0x060085E3 RID: 34275 RVA: 0x001E7F46 File Offset: 0x001E6146
		public void Clear()
		{
			this.list.Clear();
		}

		// Token: 0x17002A65 RID: 10853
		// (get) Token: 0x060085E4 RID: 34276 RVA: 0x001E7F53 File Offset: 0x001E6153
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public object SyncRoot
		{
			get
			{
				return this.list.SyncRoot;
			}
		}

		// Token: 0x060085E5 RID: 34277 RVA: 0x001E7F60 File Offset: 0x001E6160
		public void CopyTo(PivotGridSortExpressionCollection dest)
		{
			foreach (object obj in this)
			{
				PivotGridSortExpression pivotGridSortExpression = (PivotGridSortExpression)obj;
				dest.Add(pivotGridSortExpression.Clone());
			}
		}

		// Token: 0x060085E6 RID: 34278 RVA: 0x001E7FBC File Offset: 0x001E61BC
		public PivotGridSortExpression GetExpression(string expression)
		{
			PivotGridSortExpression value = new PivotGridSortExpression
			{
				FieldName = expression
			};
			return (PivotGridSortExpression)this.list[this.list.IndexOf(value)];
		}

		// Token: 0x060085E7 RID: 34279 RVA: 0x001E7FF4 File Offset: 0x001E61F4
		private int AddEx(PivotGridSortExpression sortExpression)
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
			if (!this.ContainsSortExpression(sortExpression))
			{
				num = this.list.Add(sortExpression);
			}
			else
			{
				num = this.list.IndexOf(sortExpression);
				PivotGridSortExpression pivotGridSortExpression = (PivotGridSortExpression)this.list[num];
				pivotGridSortExpression.SortOrder = sortExpression.SortOrder;
			}
			return num;
		}

		// Token: 0x060085E8 RID: 34280 RVA: 0x001E805B File Offset: 0x001E625B
		public void AddSortExpression(PivotGridSortExpression sortExpression)
		{
			this.AddEx(sortExpression);
		}

		// Token: 0x060085E9 RID: 34281 RVA: 0x001E8065 File Offset: 0x001E6265
		public void AddSortExpression(string expression)
		{
			this.AddEx(PivotGridSortExpression.Parse(expression));
		}

		// Token: 0x060085EA RID: 34282 RVA: 0x001E8074 File Offset: 0x001E6274
		public void AddAt(int index, PivotGridSortExpression sortExpression)
		{
			int num = this.list.IndexOf(sortExpression);
			if (num >= 0)
			{
				this.RemoveSortExpression(sortExpression);
			}
			if (num >= 0 && num <= index && index != 0)
			{
				this.list.Insert(index - 1, sortExpression);
				return;
			}
			this.list.Insert(index, sortExpression);
		}

		// Token: 0x060085EB RID: 34283 RVA: 0x001E80C1 File Offset: 0x001E62C1
		public void RemoveSortExpression(PivotGridSortExpression sortExpression)
		{
			if (this.ContainsSortExpression(sortExpression))
			{
				this.list.Remove(sortExpression);
			}
		}

		// Token: 0x060085EC RID: 34284 RVA: 0x001E80D8 File Offset: 0x001E62D8
		public bool ContainsSortExpression(PivotGridSortExpression sortExpression)
		{
			int num = this.list.IndexOf(sortExpression);
			return num != -1;
		}

		// Token: 0x060085ED RID: 34285 RVA: 0x001E80FC File Offset: 0x001E62FC
		public bool ContainsExpression(string expression)
		{
			PivotGridSortExpression value = new PivotGridSortExpression
			{
				FieldName = expression
			};
			int num = this.list.IndexOf(value);
			return num != -1;
		}

		// Token: 0x060085EE RID: 34286 RVA: 0x001E812C File Offset: 0x001E632C
		public void ChangeSortOrder(string expression, bool AllowNaturalSort = false)
		{
			if (string.IsNullOrEmpty(expression))
			{
				return;
			}
			PivotGridSortOrder sortOrder;
			bool flag = this.TryParseSortOrder(expression, out sortOrder);
			PivotGridSortExpression pivotGridSortExpression = new PivotGridSortExpression();
			pivotGridSortExpression.FieldName = expression;
			if (!this.ContainsSortExpression(pivotGridSortExpression))
			{
				if (flag)
				{
					pivotGridSortExpression.SortOrder = sortOrder;
				}
				else if (pivotGridSortExpression.SortOrder == PivotGridSortOrder.Ascending)
				{
					pivotGridSortExpression.SortOrder = PivotGridSortOrder.Descending;
				}
				else if (pivotGridSortExpression.SortOrder == PivotGridSortOrder.Descending)
				{
					if (AllowNaturalSort)
					{
						pivotGridSortExpression.SortOrder = PivotGridSortOrder.None;
					}
					else
					{
						pivotGridSortExpression.SortOrder = PivotGridSortOrder.Ascending;
					}
				}
				else if (pivotGridSortExpression.SortOrder == PivotGridSortOrder.None)
				{
					pivotGridSortExpression.SortOrder = PivotGridSortOrder.Ascending;
				}
				this.list.Add(pivotGridSortExpression);
				return;
			}
			PivotGridSortExpression pivotGridSortExpression2 = (PivotGridSortExpression)this.list[this.list.IndexOf(pivotGridSortExpression)];
			if (flag)
			{
				pivotGridSortExpression2.SortOrder = sortOrder;
				return;
			}
			if (pivotGridSortExpression2.SortOrder == PivotGridSortOrder.Ascending)
			{
				pivotGridSortExpression2.SortOrder = PivotGridSortOrder.Descending;
				return;
			}
			if (pivotGridSortExpression2.SortOrder != PivotGridSortOrder.Descending)
			{
				if (pivotGridSortExpression2.SortOrder == PivotGridSortOrder.None)
				{
					pivotGridSortExpression2.SortOrder = PivotGridSortOrder.Ascending;
				}
				return;
			}
			if (AllowNaturalSort)
			{
				pivotGridSortExpression2.SortOrder = PivotGridSortOrder.None;
				return;
			}
			pivotGridSortExpression2.SortOrder = PivotGridSortOrder.Ascending;
		}

		// Token: 0x060085EF RID: 34287 RVA: 0x001E8220 File Offset: 0x001E6420
		private bool TryParseSortOrder(string expression, out PivotGridSortOrder sortOrder)
		{
			if (expression != null)
			{
				expression = expression.Trim();
				int num = expression.LastIndexOf(" ");
				if (num > 0)
				{
					string text = expression.Substring(num);
					if (text.Trim().ToUpper() == "ASC")
					{
						sortOrder = PivotGridSortOrder.Ascending;
						return true;
					}
					if (text.Trim().ToUpper() == "DESC")
					{
						sortOrder = PivotGridSortOrder.Descending;
						return true;
					}
					if (text.Trim().ToUpper() == "NONE")
					{
						sortOrder = PivotGridSortOrder.None;
						return true;
					}
				}
			}
			sortOrder = PivotGridSortOrder.Ascending;
			return false;
		}

		// Token: 0x060085F0 RID: 34288 RVA: 0x001E82A8 File Offset: 0x001E64A8
		internal ArrayList GetList()
		{
			return this.list;
		}

		// Token: 0x060085F1 RID: 34289 RVA: 0x001E82B0 File Offset: 0x001E64B0
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		public string GetSortString()
		{
			string text = null;
			foreach (object obj in this)
			{
				PivotGridSortExpression pivotGridSortExpression = (PivotGridSortExpression)obj;
				text = text + pivotGridSortExpression.ToString() + ", ";
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

		// Token: 0x060085F2 RID: 34290 RVA: 0x001E8334 File Offset: 0x001E6534
		public int IndexOf(PivotGridSortExpression viewSortExpression)
		{
			return this.list.IndexOf(viewSortExpression);
		}

		// Token: 0x04002548 RID: 9544
		private readonly PivotGridControlStateManager _stateManager;

		// Token: 0x04002549 RID: 9545
		private ArrayList list;
	}
}
