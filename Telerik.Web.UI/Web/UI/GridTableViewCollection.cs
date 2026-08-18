using System;
using System.Collections;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x020010EA RID: 4330
	public class GridTableViewCollection : CollectionBase
	{
		// Token: 0x0600B14F RID: 45391 RVA: 0x0026618C File Offset: 0x0026438C
		public GridTableViewCollection(RadGrid Owner, GridTableView OwnerTableView)
		{
			this._ownerGrid = Owner;
			this._ownerTableView = OwnerTableView;
		}

		// Token: 0x0600B150 RID: 45392 RVA: 0x002661A2 File Offset: 0x002643A2
		internal void SetOwnerGrid(RadGrid owner)
		{
			this._ownerGrid = owner;
		}

		// Token: 0x0600B151 RID: 45393 RVA: 0x002661AB File Offset: 0x002643AB
		internal void SetOwnerTableView(GridTableView ownerTableView)
		{
			this._ownerTableView = ownerTableView;
		}

		// Token: 0x0600B152 RID: 45394 RVA: 0x002661B4 File Offset: 0x002643B4
		public GridTableViewCollection(GridTableViewCollection value)
		{
			this.AddRange(value);
		}

		// Token: 0x0600B153 RID: 45395 RVA: 0x002661C3 File Offset: 0x002643C3
		public GridTableViewCollection(GridTableView[] value)
		{
			this.AddRange(value);
		}

		// Token: 0x17003974 RID: 14708
		// (get) Token: 0x0600B154 RID: 45396 RVA: 0x002661D2 File Offset: 0x002643D2
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public RadGrid OwnerGrid
		{
			get
			{
				return this._ownerGrid;
			}
		}

		// Token: 0x0600B155 RID: 45397 RVA: 0x002661DA File Offset: 0x002643DA
		private void InitInCollection(GridTableView tableView)
		{
			tableView.Initialize(this.OwnerGrid);
			if (this._ownerTableView != null)
			{
				this._ownerTableView.AddedChildTable(tableView);
				return;
			}
			throw new GridException("GridTableViewCollection not initialized");
		}

		// Token: 0x17003975 RID: 14709
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public GridTableView this[int index]
		{
			get
			{
				return (GridTableView)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		// Token: 0x0600B158 RID: 45400 RVA: 0x00266229 File Offset: 0x00264429
		protected override void OnValidate(object value)
		{
			this.InitInCollection((GridTableView)value);
		}

		// Token: 0x0600B159 RID: 45401 RVA: 0x00266237 File Offset: 0x00264437
		public int Add(GridTableView value)
		{
			return base.List.Add(value);
		}

		// Token: 0x0600B15A RID: 45402 RVA: 0x00266248 File Offset: 0x00264448
		public void AddRange(GridTableView[] value)
		{
			for (int i = 0; i < value.Length; i++)
			{
				this.Add(value[i]);
			}
		}

		// Token: 0x0600B15B RID: 45403 RVA: 0x00266270 File Offset: 0x00264470
		public void AddRange(GridTableViewCollection value)
		{
			for (int i = 0; i < value.Count; i++)
			{
				this.Add(value[i]);
			}
		}

		// Token: 0x0600B15C RID: 45404 RVA: 0x0026629C File Offset: 0x0026449C
		public bool Contains(GridTableView value)
		{
			return base.List.Contains(value);
		}

		// Token: 0x0600B15D RID: 45405 RVA: 0x002662AA File Offset: 0x002644AA
		public void CopyTo(GridTableView[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		// Token: 0x0600B15E RID: 45406 RVA: 0x002662B9 File Offset: 0x002644B9
		public int IndexOf(GridTableView value)
		{
			return base.List.IndexOf(value);
		}

		// Token: 0x0600B15F RID: 45407 RVA: 0x002662C7 File Offset: 0x002644C7
		public void Insert(int index, GridTableView value)
		{
			base.List.Insert(index, value);
		}

		// Token: 0x0600B160 RID: 45408 RVA: 0x002662D6 File Offset: 0x002644D6
		public new GridTableViewCollection.GridDataTableEnumerator GetEnumerator()
		{
			return new GridTableViewCollection.GridDataTableEnumerator(this);
		}

		// Token: 0x0600B161 RID: 45409 RVA: 0x002662E0 File Offset: 0x002644E0
		protected override void OnInsertComplete(int index, object newValue)
		{
			base.OnInsertComplete(index, newValue);
			GridTableView gridTableView = newValue as GridTableView;
			if (gridTableView != null)
			{
				gridTableView.SetDetailIndex(index);
			}
		}

		// Token: 0x0600B162 RID: 45410 RVA: 0x00266308 File Offset: 0x00264508
		protected override void OnSetComplete(int index, object oldValue, object newValue)
		{
			base.OnSetComplete(index, oldValue, newValue);
			GridTableView gridTableView = newValue as GridTableView;
			if (gridTableView != null)
			{
				gridTableView.SetDetailIndex(index);
			}
		}

		// Token: 0x0600B163 RID: 45411 RVA: 0x0026632F File Offset: 0x0026452F
		public void Remove(GridTableView value)
		{
			base.List.Remove(value);
		}

		// Token: 0x04002E82 RID: 11906
		private RadGrid _ownerGrid;

		// Token: 0x04002E83 RID: 11907
		private GridTableView _ownerTableView;

		// Token: 0x020010EB RID: 4331
		public class GridDataTableEnumerator : IEnumerator
		{
			// Token: 0x0600B164 RID: 45412 RVA: 0x0026633D File Offset: 0x0026453D
			public GridDataTableEnumerator(GridTableViewCollection mappings)
			{
				this.temp = mappings;
				this.baseEnumerator = this.temp.GetEnumerator();
			}

			// Token: 0x17003976 RID: 14710
			// (get) Token: 0x0600B165 RID: 45413 RVA: 0x0026635D File Offset: 0x0026455D
			public GridTableView Current
			{
				get
				{
					return (GridTableView)this.baseEnumerator.Current;
				}
			}

			// Token: 0x17003977 RID: 14711
			// (get) Token: 0x0600B166 RID: 45414 RVA: 0x0026636F File Offset: 0x0026456F
			object IEnumerator.Current
			{
				get
				{
					return this.baseEnumerator.Current;
				}
			}

			// Token: 0x0600B167 RID: 45415 RVA: 0x0026637C File Offset: 0x0026457C
			public bool MoveNext()
			{
				return this.baseEnumerator.MoveNext();
			}

			// Token: 0x0600B168 RID: 45416 RVA: 0x00266389 File Offset: 0x00264589
			bool IEnumerator.MoveNext()
			{
				return this.baseEnumerator.MoveNext();
			}

			// Token: 0x0600B169 RID: 45417 RVA: 0x00266396 File Offset: 0x00264596
			public void Reset()
			{
				this.baseEnumerator.Reset();
			}

			// Token: 0x0600B16A RID: 45418 RVA: 0x002663A3 File Offset: 0x002645A3
			void IEnumerator.Reset()
			{
				this.baseEnumerator.Reset();
			}

			// Token: 0x04002E84 RID: 11908
			private IEnumerator baseEnumerator;

			// Token: 0x04002E85 RID: 11909
			private IEnumerable temp;
		}
	}
}
