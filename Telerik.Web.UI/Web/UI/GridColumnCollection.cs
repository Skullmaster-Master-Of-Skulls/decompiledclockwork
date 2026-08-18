using System;
using System.Collections;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x020010AD RID: 4269
	[PersistChildren(false)]
	public class GridColumnCollection : IList, ICollection, IEnumerable, IStateManager
	{
		// Token: 0x0600ADF7 RID: 44535 RVA: 0x00258057 File Offset: 0x00256257
		public GridColumnCollection(GridTableView owner, ArrayList columns)
		{
			this.owner = owner;
			this.columns = columns;
		}

		// Token: 0x0600ADF8 RID: 44536 RVA: 0x0025806D File Offset: 0x0025626D
		public GridColumnCollection(GridTableView owner)
		{
			this.owner = owner;
			this.columns = new ArrayList();
		}

		// Token: 0x0600ADF9 RID: 44537 RVA: 0x00258087 File Offset: 0x00256287
		public void Add(GridColumn column)
		{
			this.AddAt(-1, column);
		}

		// Token: 0x0600ADFA RID: 44538 RVA: 0x00258094 File Offset: 0x00256294
		public int Add(object Val)
		{
			this.InitInCollection((GridColumn)Val);
			int result = this.columns.Add(Val);
			this.OnColumnsChanged();
			return result;
		}

		// Token: 0x0600ADFB RID: 44539 RVA: 0x002580C1 File Offset: 0x002562C1
		public bool Contains(object Val)
		{
			return this.columns.Contains(Val);
		}

		// Token: 0x0600ADFC RID: 44540 RVA: 0x002580CF File Offset: 0x002562CF
		public int IndexOf(object Val)
		{
			return this.columns.IndexOf(Val);
		}

		// Token: 0x0600ADFD RID: 44541 RVA: 0x002580DD File Offset: 0x002562DD
		public void Insert(int Index, object Val)
		{
			this.columns.Insert(Index, Val);
			this.InitInCollection((GridColumn)Val);
			if (this.marked)
			{
				((IStateManager)Val).TrackViewState();
			}
			this.OnColumnsChanged();
		}

		// Token: 0x0600ADFE RID: 44542 RVA: 0x00258111 File Offset: 0x00256311
		public void Remove(object Val)
		{
			this.columns.Remove(Val);
			this.OnColumnsChanged();
		}

		// Token: 0x1700383B RID: 14395
		// (get) Token: 0x0600ADFF RID: 44543 RVA: 0x00258125 File Offset: 0x00256325
		public bool IsFixedSize
		{
			get
			{
				return this.columns.IsFixedSize;
			}
		}

		// Token: 0x1700383C RID: 14396
		object IList.this[int Index]
		{
			get
			{
				return this.columns[Index];
			}
			set
			{
				this.columns[Index] = value;
			}
		}

		// Token: 0x0600AE02 RID: 44546 RVA: 0x0025814F File Offset: 0x0025634F
		public void AddAt(int index, GridColumn column)
		{
			if (index == -1)
			{
				this.columns.Add(column);
			}
			else
			{
				this.columns.Insert(index, column);
			}
			this.InitInCollection(column);
			if (this.marked)
			{
				((IStateManager)column).TrackViewState();
			}
			this.OnColumnsChanged();
		}

		// Token: 0x0600AE03 RID: 44547 RVA: 0x0025818C File Offset: 0x0025638C
		public void Clear()
		{
			this.columns.Clear();
			this.OnColumnsChanged();
		}

		// Token: 0x0600AE04 RID: 44548 RVA: 0x002581A0 File Offset: 0x002563A0
		public void CopyTo(Array array, int index)
		{
			foreach (object value in this)
			{
				array.SetValue(value, index++);
			}
		}

		// Token: 0x0600AE05 RID: 44549 RVA: 0x002581D0 File Offset: 0x002563D0
		public IEnumerator GetEnumerator()
		{
			return this.columns.GetEnumerator();
		}

		// Token: 0x0600AE06 RID: 44550 RVA: 0x002581DD File Offset: 0x002563DD
		public int IndexOf(GridColumn column)
		{
			if (column != null)
			{
				return this.columns.IndexOf(column);
			}
			return -1;
		}

		// Token: 0x0600AE07 RID: 44551 RVA: 0x002581F0 File Offset: 0x002563F0
		private void OnColumnsChanged()
		{
			if (this.owner != null)
			{
				this.owner.ResetRenderColumns();
			}
		}

		// Token: 0x0600AE08 RID: 44552 RVA: 0x00258208 File Offset: 0x00256408
		public void Remove(GridColumn column)
		{
			int num = this.IndexOf(column);
			if (num >= 0)
			{
				this.RemoveAt(num);
			}
		}

		// Token: 0x0600AE09 RID: 44553 RVA: 0x00258228 File Offset: 0x00256428
		public void RemoveAt(int index)
		{
			if (index >= 0 && index < this.Count)
			{
				this.columns.RemoveAt(index);
				this.OnColumnsChanged();
				return;
			}
			throw new ArgumentOutOfRangeException("index");
		}

		// Token: 0x1700383D RID: 14397
		// (get) Token: 0x0600AE0A RID: 44554 RVA: 0x00258254 File Offset: 0x00256454
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this._isTrackViewState;
			}
		}

		// Token: 0x0600AE0B RID: 44555 RVA: 0x0025825C File Offset: 0x0025645C
		protected virtual GridColumn CreateColumnFromTypeName(string columnTypeName)
		{
			GridColumn gridColumn = null;
			switch (columnTypeName)
			{
			case "GridBoundColumn":
				gridColumn = new GridBoundColumn();
				break;
			case "GridTemplateColumn":
				gridColumn = new GridTemplateColumn();
				break;
			case "GridButtonColumn":
				gridColumn = new GridButtonColumn();
				break;
			case "GridCheckBoxColumn":
				gridColumn = new GridCheckBoxColumn();
				break;
			case "GridDropDownColumn":
				gridColumn = new GridDropDownColumn();
				break;
			case "GridExpandColumn":
				gridColumn = new GridExpandColumn();
				break;
			case "GridClientDeleteColumn":
				gridColumn = new GridClientDeleteColumn();
				break;
			case "GridClientSelectColumn":
				gridColumn = new GridClientSelectColumn();
				break;
			case "GridRowIndicatorColumn":
				gridColumn = new GridRowIndicatorColumn();
				break;
			case "GridGroupSplitterColumn":
				gridColumn = new GridGroupSplitterColumn();
				break;
			case "GridHyperLinkColumn":
				gridColumn = new GridHyperLinkColumn();
				break;
			case "GridEditCommandColumn":
				gridColumn = new GridEditCommandColumn();
				break;
			case "GridDateTimeColumn":
				gridColumn = new GridDateTimeColumn();
				break;
			case "GridNumericColumn":
				gridColumn = new GridNumericColumn();
				break;
			case "GridImageColumn":
				gridColumn = new GridImageColumn();
				break;
			case "GridCalculatedColumn":
				gridColumn = new GridCalculatedColumn();
				break;
			case "GridBinaryImageColumn":
				gridColumn = new GridBinaryImageColumn();
				break;
			case "GridAttachmentColumn":
				gridColumn = new GridAttachmentColumn();
				break;
			case "GridDragDropColumn":
				gridColumn = new GridDragDropColumn();
				break;
			case "GridHTMLEditorColumn":
				gridColumn = new GridHTMLEditorColumn();
				break;
			case "GridRatingColumn":
				gridColumn = new GridRatingColumn();
				break;
			case "GridMaskedColumn":
				gridColumn = new GridMaskedColumn();
				break;
			}
			GridColumnCreatingEventArgs gridColumnCreatingEventArgs = new GridColumnCreatingEventArgs(gridColumn, this.owner, columnTypeName);
			this.owner.OwnerGrid.CallOnColumnCreating(gridColumnCreatingEventArgs);
			gridColumn = gridColumnCreatingEventArgs.Column;
			if (gridColumn != null)
			{
				return gridColumn;
			}
			throw new GridException("Cannot create column with the specified type name: " + columnTypeName);
		}

		// Token: 0x0600AE0C RID: 44556 RVA: 0x00258514 File Offset: 0x00256714
		void IStateManager.LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				Pair pair = (Pair)savedState;
				this.viewStateNotManagedCount = (int)pair.First;
				object[] array = (object[])pair.Second;
				for (int i = 0; i < this.viewStateNotManagedCount; i++)
				{
					GridColumn gridColumn = (GridColumn)this.columns[i];
					((IStateManager)gridColumn).LoadViewState(array[i]);
				}
				for (int j = this.viewStateNotManagedCount; j < array.Length; j++)
				{
					Pair pair2 = (Pair)array[j];
					GridColumn gridColumn2 = this.CreateColumnFromTypeName((string)pair2.First);
					this.Add(gridColumn2);
					((IStateManager)gridColumn2).LoadViewState(pair2.Second);
				}
			}
		}

		// Token: 0x0600AE0D RID: 44557 RVA: 0x002585C4 File Offset: 0x002567C4
		object IStateManager.SaveViewState()
		{
			int count = this.columns.Count;
			Pair pair = new Pair();
			pair.First = this.viewStateNotManagedCount;
			object[] array = new object[count];
			pair.Second = array;
			bool flag = false;
			if (!this.owner.EnableColumnsViewState)
			{
				return null;
			}
			for (int i = 0; i < this.viewStateNotManagedCount; i++)
			{
				array[i] = ((IStateManager)this.columns[i]).SaveViewState();
				if (array[i] != null)
				{
					flag = true;
				}
			}
			for (int j = this.viewStateNotManagedCount; j < count; j++)
			{
				array[j] = new Pair(((GridColumn)this.columns[j]).ColumnType, ((IStateManager)this.columns[j]).SaveViewState());
				if (array[j] != null)
				{
					flag = true;
				}
			}
			if (!flag)
			{
				return null;
			}
			return pair;
		}

		// Token: 0x0600AE0E RID: 44558 RVA: 0x002586A8 File Offset: 0x002568A8
		void IStateManager.TrackViewState()
		{
			if (this.marked)
			{
				return;
			}
			this._isTrackViewState = true;
			this.marked = true;
			this.viewStateNotManagedCount = this.columns.Count;
			foreach (object obj in this.columns)
			{
				GridColumn gridColumn = (GridColumn)obj;
				((IStateManager)gridColumn).TrackViewState();
			}
		}

		// Token: 0x1700383E RID: 14398
		// (get) Token: 0x0600AE0F RID: 44559 RVA: 0x00258728 File Offset: 0x00256928
		[Browsable(false)]
		public int Count
		{
			get
			{
				return this.columns.Count;
			}
		}

		// Token: 0x1700383F RID: 14399
		// (get) Token: 0x0600AE10 RID: 44560 RVA: 0x00258735 File Offset: 0x00256935
		[Browsable(false)]
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17003840 RID: 14400
		// (get) Token: 0x0600AE11 RID: 44561 RVA: 0x00258738 File Offset: 0x00256938
		[Browsable(false)]
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17003841 RID: 14401
		[NotifyParentProperty(true)]
		public GridColumn this[int index]
		{
			get
			{
				GridColumn result;
				try
				{
					result = (GridColumn)this.columns[index];
				}
				catch (ArgumentOutOfRangeException inner)
				{
					throw new GridException("Failed accessing GridColumn by index. Please verify that you have specified the structure of RadGrid correctly.", inner);
				}
				return result;
			}
		}

		// Token: 0x17003842 RID: 14402
		// (get) Token: 0x0600AE13 RID: 44563 RVA: 0x0025877C File Offset: 0x0025697C
		[Browsable(false)]
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x0600AE14 RID: 44564 RVA: 0x00258780 File Offset: 0x00256980
		public GridColumn FindByUniqueName(string UniqueName)
		{
			GridColumn gridColumn = this.FindByUniqueNameSafe(UniqueName);
			if (gridColumn != null)
			{
				return gridColumn;
			}
			throw new GridException("Cannot find column with UniqueName '" + UniqueName + "'");
		}

		// Token: 0x0600AE15 RID: 44565 RVA: 0x002587B0 File Offset: 0x002569B0
		public GridColumn FindByUniqueNameSafe(string UniqueName)
		{
			GridColumn result = null;
			foreach (object obj in this)
			{
				GridColumn gridColumn = (GridColumn)obj;
				if (gridColumn.UniqueName.Trim().ToUpper() == UniqueName.Trim().ToUpper())
				{
					result = gridColumn;
					break;
				}
			}
			return result;
		}

		// Token: 0x0600AE16 RID: 44566 RVA: 0x00258828 File Offset: 0x00256A28
		public GridColumn FindByDataField(string DataField)
		{
			GridColumn gridColumn = this.FindByDataFieldSafe(DataField);
			if (gridColumn != null)
			{
				return gridColumn;
			}
			throw new GridException("Cannot find column bound to field '" + DataField + "'");
		}

		// Token: 0x0600AE17 RID: 44567 RVA: 0x00258858 File Offset: 0x00256A58
		public GridColumn FindByDataFieldSafe(string DataField)
		{
			GridColumn result = null;
			foreach (object obj in this)
			{
				GridColumn gridColumn = (GridColumn)obj;
				if (gridColumn.IsBoundToFieldName(DataField))
				{
					result = gridColumn;
					break;
				}
			}
			return result;
		}

		// Token: 0x0600AE18 RID: 44568 RVA: 0x002588B8 File Offset: 0x00256AB8
		public GridColumn[] FindAllByDataField(string DataField)
		{
			ArrayList arrayList = new ArrayList();
			foreach (object obj in this)
			{
				GridColumn gridColumn = (GridColumn)obj;
				if (gridColumn.IsBoundToFieldName(DataField))
				{
					arrayList.Add(gridColumn);
				}
			}
			GridColumn[] array = new GridColumn[arrayList.Count];
			arrayList.CopyTo(array, 0);
			return array;
		}

		// Token: 0x0600AE19 RID: 44569 RVA: 0x00258934 File Offset: 0x00256B34
		private void InitInCollection(GridColumn val)
		{
			val.SetOwner(this.owner);
		}

		// Token: 0x04002DF3 RID: 11763
		private bool _isTrackViewState;

		// Token: 0x04002DF4 RID: 11764
		private ArrayList columns;

		// Token: 0x04002DF5 RID: 11765
		private bool marked;

		// Token: 0x04002DF6 RID: 11766
		private GridTableView owner;

		// Token: 0x04002DF7 RID: 11767
		private int viewStateNotManagedCount;
	}
}
