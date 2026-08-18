using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Globalization;

namespace System.Windows.Forms
{
	// Token: 0x0200020A RID: 522
	[ListBindable(false)]
	[DesignerSerializer("System.Windows.Forms.Design.DataGridViewRowCollectionCodeDomSerializer, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.Serialization.CodeDomSerializer, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public class DataGridViewRowCollection : ICollection, IEnumerable, IList
	{
		// Token: 0x06002217 RID: 8727 RVA: 0x000A1F91 File Offset: 0x000A0191
		int IList.Add(object value)
		{
			return this.Add((DataGridViewRow)value);
		}

		// Token: 0x06002218 RID: 8728 RVA: 0x000A1F9F File Offset: 0x000A019F
		void IList.Clear()
		{
			this.Clear();
		}

		// Token: 0x06002219 RID: 8729 RVA: 0x000A1FA7 File Offset: 0x000A01A7
		bool IList.Contains(object value)
		{
			return this.items.Contains(value);
		}

		// Token: 0x0600221A RID: 8730 RVA: 0x000A1FB5 File Offset: 0x000A01B5
		int IList.IndexOf(object value)
		{
			return this.items.IndexOf(value);
		}

		// Token: 0x0600221B RID: 8731 RVA: 0x000A1FC3 File Offset: 0x000A01C3
		void IList.Insert(int index, object value)
		{
			this.Insert(index, (DataGridViewRow)value);
		}

		// Token: 0x0600221C RID: 8732 RVA: 0x000A1FD2 File Offset: 0x000A01D2
		void IList.Remove(object value)
		{
			this.Remove((DataGridViewRow)value);
		}

		// Token: 0x0600221D RID: 8733 RVA: 0x000A1FE0 File Offset: 0x000A01E0
		void IList.RemoveAt(int index)
		{
			this.RemoveAt(index);
		}

		// Token: 0x170007B9 RID: 1977
		// (get) Token: 0x0600221E RID: 8734 RVA: 0x00011A20 File Offset: 0x0000FC20
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170007BA RID: 1978
		// (get) Token: 0x0600221F RID: 8735 RVA: 0x00011A20 File Offset: 0x0000FC20
		bool IList.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170007BB RID: 1979
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06002222 RID: 8738 RVA: 0x000A1FF2 File Offset: 0x000A01F2
		void ICollection.CopyTo(Array array, int index)
		{
			this.items.CopyTo(array, index);
		}

		// Token: 0x170007BC RID: 1980
		// (get) Token: 0x06002223 RID: 8739 RVA: 0x000A2001 File Offset: 0x000A0201
		int ICollection.Count
		{
			get
			{
				return this.Count;
			}
		}

		// Token: 0x170007BD RID: 1981
		// (get) Token: 0x06002224 RID: 8740 RVA: 0x00011A20 File Offset: 0x0000FC20
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170007BE RID: 1982
		// (get) Token: 0x06002225 RID: 8741 RVA: 0x00006C59 File Offset: 0x00004E59
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06002226 RID: 8742 RVA: 0x000A2009 File Offset: 0x000A0209
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new DataGridViewRowCollection.UnsharingRowEnumerator(this);
		}

		// Token: 0x06002227 RID: 8743 RVA: 0x000A2011 File Offset: 0x000A0211
		public DataGridViewRowCollection(DataGridView dataGridView)
		{
			this.InvalidateCachedRowCounts();
			this.InvalidateCachedRowsHeights();
			this.dataGridView = dataGridView;
			this.rowStates = new List<DataGridViewElementStates>();
			this.items = new DataGridViewRowCollection.RowArrayList(this);
		}

		// Token: 0x170007BF RID: 1983
		// (get) Token: 0x06002228 RID: 8744 RVA: 0x000A2043 File Offset: 0x000A0243
		public int Count
		{
			get
			{
				return this.items.Count;
			}
		}

		// Token: 0x170007C0 RID: 1984
		// (get) Token: 0x06002229 RID: 8745 RVA: 0x000A2050 File Offset: 0x000A0250
		internal bool IsCollectionChangedListenedTo
		{
			get
			{
				return this.onCollectionChanged != null;
			}
		}

		// Token: 0x170007C1 RID: 1985
		// (get) Token: 0x0600222A RID: 8746 RVA: 0x000A205C File Offset: 0x000A025C
		protected ArrayList List
		{
			get
			{
				int count = this.Count;
				for (int i = 0; i < count; i++)
				{
					DataGridViewRow dataGridViewRow = this[i];
				}
				return this.items;
			}
		}

		// Token: 0x170007C2 RID: 1986
		// (get) Token: 0x0600222B RID: 8747 RVA: 0x000A208A File Offset: 0x000A028A
		internal ArrayList SharedList
		{
			get
			{
				return this.items;
			}
		}

		// Token: 0x0600222C RID: 8748 RVA: 0x000A2092 File Offset: 0x000A0292
		public DataGridViewRow SharedRow(int rowIndex)
		{
			return (DataGridViewRow)this.SharedList[rowIndex];
		}

		// Token: 0x170007C3 RID: 1987
		// (get) Token: 0x0600222D RID: 8749 RVA: 0x000A20A5 File Offset: 0x000A02A5
		protected DataGridView DataGridView
		{
			get
			{
				return this.dataGridView;
			}
		}

		// Token: 0x170007C4 RID: 1988
		public DataGridViewRow this[int index]
		{
			get
			{
				DataGridViewRow dataGridViewRow = this.SharedRow(index);
				if (dataGridViewRow.Index != -1)
				{
					return dataGridViewRow;
				}
				if (index == 0 && this.items.Count == 1)
				{
					dataGridViewRow.IndexInternal = 0;
					dataGridViewRow.StateInternal = this.SharedRowState(0);
					if (this.DataGridView != null)
					{
						this.DataGridView.OnRowUnshared(dataGridViewRow);
					}
					return dataGridViewRow;
				}
				DataGridViewRow dataGridViewRow2 = (DataGridViewRow)dataGridViewRow.Clone();
				dataGridViewRow2.IndexInternal = index;
				dataGridViewRow2.DataGridViewInternal = dataGridViewRow.DataGridView;
				dataGridViewRow2.StateInternal = this.SharedRowState(index);
				this.SharedList[index] = dataGridViewRow2;
				int num = 0;
				foreach (object obj in dataGridViewRow2.Cells)
				{
					DataGridViewCell dataGridViewCell = (DataGridViewCell)obj;
					dataGridViewCell.DataGridViewInternal = dataGridViewRow.DataGridView;
					dataGridViewCell.OwningRowInternal = dataGridViewRow2;
					dataGridViewCell.OwningColumnInternal = this.DataGridView.Columns[num];
					num++;
				}
				if (dataGridViewRow2.HasHeaderCell)
				{
					dataGridViewRow2.HeaderCell.DataGridViewInternal = dataGridViewRow.DataGridView;
					dataGridViewRow2.HeaderCell.OwningRowInternal = dataGridViewRow2;
				}
				if (this.DataGridView != null)
				{
					this.DataGridView.OnRowUnshared(dataGridViewRow2);
				}
				return dataGridViewRow2;
			}
		}

		// Token: 0x14000186 RID: 390
		// (add) Token: 0x0600222F RID: 8751 RVA: 0x000A2200 File Offset: 0x000A0400
		// (remove) Token: 0x06002230 RID: 8752 RVA: 0x000A2219 File Offset: 0x000A0419
		public event CollectionChangeEventHandler CollectionChanged
		{
			add
			{
				this.onCollectionChanged = (CollectionChangeEventHandler)Delegate.Combine(this.onCollectionChanged, value);
			}
			remove
			{
				this.onCollectionChanged = (CollectionChangeEventHandler)Delegate.Remove(this.onCollectionChanged, value);
			}
		}

		// Token: 0x06002231 RID: 8753 RVA: 0x000A2234 File Offset: 0x000A0434
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual int Add()
		{
			if (this.DataGridView.DataSource != null)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewRowCollection_AddUnboundRow"));
			}
			if (this.DataGridView.NoDimensionChangeAllowed)
			{
				throw new InvalidOperationException(SR.GetString("DataGridView_ForbiddenOperationInEventHandler"));
			}
			return this.AddInternal(false, null);
		}

		// Token: 0x06002232 RID: 8754 RVA: 0x000A2284 File Offset: 0x000A0484
		internal int AddInternal(bool newRow, object[] values)
		{
			if (this.DataGridView.Columns.Count == 0)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewRowCollection_NoColumns"));
			}
			if (this.DataGridView.RowTemplate.Cells.Count > this.DataGridView.Columns.Count)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewRowCollection_RowTemplateTooManyCells"));
			}
			DataGridViewRow rowTemplateClone = this.DataGridView.RowTemplateClone;
			if (newRow)
			{
				rowTemplateClone.StateInternal = (rowTemplateClone.State | DataGridViewElementStates.Visible);
				foreach (object obj in rowTemplateClone.Cells)
				{
					DataGridViewCell dataGridViewCell = (DataGridViewCell)obj;
					dataGridViewCell.Value = dataGridViewCell.DefaultNewRowValue;
				}
			}
			if (values != null)
			{
				rowTemplateClone.SetValuesInternal(values);
			}
			if (this.DataGridView.NewRowIndex != -1)
			{
				int num = this.Count - 1;
				this.Insert(num, rowTemplateClone);
				return num;
			}
			DataGridViewElementStates state = rowTemplateClone.State;
			this.DataGridView.OnAddingRow(rowTemplateClone, state, true);
			rowTemplateClone.DataGridViewInternal = this.dataGridView;
			int num2 = 0;
			foreach (object obj2 in rowTemplateClone.Cells)
			{
				DataGridViewCell dataGridViewCell2 = (DataGridViewCell)obj2;
				dataGridViewCell2.DataGridViewInternal = this.dataGridView;
				dataGridViewCell2.OwningColumnInternal = this.DataGridView.Columns[num2];
				num2++;
			}
			if (rowTemplateClone.HasHeaderCell)
			{
				rowTemplateClone.HeaderCell.DataGridViewInternal = this.DataGridView;
				rowTemplateClone.HeaderCell.OwningRowInternal = rowTemplateClone;
			}
			int num3 = this.SharedList.Add(rowTemplateClone);
			this.rowStates.Add(state);
			if (values != null || !this.RowIsSharable(num3) || DataGridViewRowCollection.RowHasValueOrToolTipText(rowTemplateClone) || this.IsCollectionChangedListenedTo)
			{
				rowTemplateClone.IndexInternal = num3;
			}
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, rowTemplateClone), num3, 1);
			return num3;
		}

		// Token: 0x06002233 RID: 8755 RVA: 0x000A2498 File Offset: 0x000A0698
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual int Add(params object[] values)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			if (this.DataGridView.VirtualMode)
			{
				throw new InvalidOperationException(SR.GetString("DataGridView_InvalidOperationInVirtualMode"));
			}
			if (this.DataGridView.DataSource != null)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewRowCollection_AddUnboundRow"));
			}
			if (this.DataGridView.NoDimensionChangeAllowed)
			{
				throw new InvalidOperationException(SR.GetString("DataGridView_ForbiddenOperationInEventHandler"));
			}
			return this.AddInternal(false, values);
		}

		// Token: 0x06002234 RID: 8756 RVA: 0x000A2514 File Offset: 0x000A0714
		public virtual int Add(DataGridViewRow dataGridViewRow)
		{
			if (this.DataGridView.Columns.Count == 0)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewRowCollection_NoColumns"));
			}
			if (this.DataGridView.DataSource != null)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewRowCollection_AddUnboundRow"));
			}
			if (this.DataGridView.NoDimensionChangeAllowed)
			{
				throw new InvalidOperationException(SR.GetString("DataGridView_ForbiddenOperationInEventHandler"));
			}
			return this.AddInternal(dataGridViewRow);
		}

		// Token: 0x06002235 RID: 8757 RVA: 0x000A2584 File Offset: 0x000A0784
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual int Add(int count)
		{
			if (count <= 0)
			{
				throw new ArgumentOutOfRangeException("count", SR.GetString("DataGridViewRowCollection_CountOutOfRange"));
			}
			if (this.DataGridView.Columns.Count == 0)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewRowCollection_NoColumns"));
			}
			if (this.DataGridView.DataSource != null)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewRowCollection_AddUnboundRow"));
			}
			if (this.DataGridView.NoDimensionChangeAllowed)
			{
				throw new InvalidOperationException(SR.GetString("DataGridView_ForbiddenOperationInEventHandler"));
			}
			if (this.DataGridView.RowTemplate.Cells.Count > this.DataGridView.Columns.Count)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewRowCollection_RowTemplateTooManyCells"));
			}
			DataGridViewRow rowTemplateClone = this.DataGridView.RowTemplateClone;
			DataGridViewElementStates state = rowTemplateClone.State;
			rowTemplateClone.DataGridViewInternal = this.dataGridView;
			int num = 0;
			foreach (object obj in rowTemplateClone.Cells)
			{
				DataGridViewCell dataGridViewCell = (DataGridViewCell)obj;
				dataGridViewCell.DataGridViewInternal = this.dataGridView;
				dataGridViewCell.OwningColumnInternal = this.DataGridView.Columns[num];
				num++;
			}
			if (rowTemplateClone.HasHeaderCell)
			{
				rowTemplateClone.HeaderCell.DataGridViewInternal = this.dataGridView;
				rowTemplateClone.HeaderCell.OwningRowInternal = rowTemplateClone;
			}
			if (this.DataGridView.NewRowIndex != -1)
			{
				int num2 = this.Count - 1;
				this.InsertCopiesPrivate(rowTemplateClone, state, num2, count);
				return num2 + count - 1;
			}
			return this.AddCopiesPrivate(rowTemplateClone, state, count);
		}

		// Token: 0x06002236 RID: 8758 RVA: 0x000A2728 File Offset: 0x000A0928
		internal int AddInternal(DataGridViewRow dataGridViewRow)
		{
			if (dataGridViewRow == null)
			{
				throw new ArgumentNullException("dataGridViewRow");
			}
			if (dataGridViewRow.DataGridView != null)
			{
				throw new InvalidOperationException(SR.GetString("DataGridView_RowAlreadyBelongsToDataGridView"));
			}
			if (this.DataGridView.Columns.Count == 0)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewRowCollection_NoColumns"));
			}
			if (dataGridViewRow.Cells.Count > this.DataGridView.Columns.Count)
			{
				throw new ArgumentException(SR.GetString("DataGridViewRowCollection_TooManyCells"), "dataGridViewRow");
			}
			if (dataGridViewRow.Selected)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewRowCollection_CannotAddOrInsertSelectedRow"));
			}
			if (this.DataGridView.NewRowIndex != -1)
			{
				int num = this.Count - 1;
				this.InsertInternal(num, dataGridViewRow);
				return num;
			}
			this.DataGridView.CompleteCellsCollection(dataGridViewRow);
			this.DataGridView.OnAddingRow(dataGridViewRow, dataGridViewRow.State, true);
			int num2 = 0;
			foreach (object obj in dataGridViewRow.Cells)
			{
				DataGridViewCell dataGridViewCell = (DataGridViewCell)obj;
				dataGridViewCell.DataGridViewInternal = this.dataGridView;
				if (dataGridViewCell.ColumnIndex == -1)
				{
					dataGridViewCell.OwningColumnInternal = this.DataGridView.Columns[num2];
				}
				num2++;
			}
			if (dataGridViewRow.HasHeaderCell)
			{
				dataGridViewRow.HeaderCell.DataGridViewInternal = this.DataGridView;
				dataGridViewRow.HeaderCell.OwningRowInternal = dataGridViewRow;
			}
			int num3 = this.SharedList.Add(dataGridViewRow);
			this.rowStates.Add(dataGridViewRow.State);
			dataGridViewRow.DataGridViewInternal = this.dataGridView;
			if (!this.RowIsSharable(num3) || DataGridViewRowCollection.RowHasValueOrToolTipText(dataGridViewRow) || this.IsCollectionChangedListenedTo)
			{
				dataGridViewRow.IndexInternal = num3;
			}
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, dataGridViewRow), num3, 1);
			return num3;
		}

		// Token: 0x06002237 RID: 8759 RVA: 0x000A2904 File Offset: 0x000A0B04
		public virtual int AddCopy(int indexSource)
		{
			if (this.DataGridView.DataSource != null)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewRowCollection_AddUnboundRow"));
			}
			if (this.DataGridView.NoDimensionChangeAllowed)
			{
				throw new InvalidOperationException(SR.GetString("DataGridView_ForbiddenOperationInEventHandler"));
			}
			return this.AddCopyInternal(indexSource, DataGridViewElementStates.None, DataGridViewElementStates.Displayed | DataGridViewElementStates.Selected, false);
		}

		// Token: 0x06002238 RID: 8760 RVA: 0x000A2958 File Offset: 0x000A0B58
		internal int AddCopyInternal(int indexSource, DataGridViewElementStates dgvesAdd, DataGridViewElementStates dgvesRemove, bool newRow)
		{
			if (this.DataGridView.NewRowIndex != -1)
			{
				int num = this.Count - 1;
				this.InsertCopy(indexSource, num);
				return num;
			}
			if (indexSource < 0 || indexSource >= this.Count)
			{
				throw new ArgumentOutOfRangeException("indexSource", SR.GetString("DataGridViewRowCollection_IndexSourceOutOfRange"));
			}
			DataGridViewRow dataGridViewRow = this.SharedRow(indexSource);
			int num2;
			if (dataGridViewRow.Index == -1 && !this.IsCollectionChangedListenedTo && !newRow)
			{
				DataGridViewElementStates dataGridViewElementStates = this.rowStates[indexSource] & ~dgvesRemove;
				dataGridViewElementStates |= dgvesAdd;
				this.DataGridView.OnAddingRow(dataGridViewRow, dataGridViewElementStates, true);
				num2 = this.SharedList.Add(dataGridViewRow);
				this.rowStates.Add(dataGridViewElementStates);
				this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, dataGridViewRow), num2, 1);
				return num2;
			}
			num2 = this.AddDuplicateRow(dataGridViewRow, newRow);
			if (!this.RowIsSharable(num2) || DataGridViewRowCollection.RowHasValueOrToolTipText(this.SharedRow(num2)) || this.IsCollectionChangedListenedTo)
			{
				this.UnshareRow(num2);
			}
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, this.SharedRow(num2)), num2, 1);
			return num2;
		}

		// Token: 0x06002239 RID: 8761 RVA: 0x000A2A58 File Offset: 0x000A0C58
		public virtual int AddCopies(int indexSource, int count)
		{
			if (this.DataGridView.DataSource != null)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewRowCollection_AddUnboundRow"));
			}
			if (this.DataGridView.NoDimensionChangeAllowed)
			{
				throw new InvalidOperationException(SR.GetString("DataGridView_ForbiddenOperationInEventHandler"));
			}
			return this.AddCopiesInternal(indexSource, count);
		}

		// Token: 0x0600223A RID: 8762 RVA: 0x000A2AA8 File Offset: 0x000A0CA8
		internal int AddCopiesInternal(int indexSource, int count)
		{
			if (this.DataGridView.NewRowIndex != -1)
			{
				int num = this.Count - 1;
				this.InsertCopiesPrivate(indexSource, num, count);
				return num + count - 1;
			}
			return this.AddCopiesInternal(indexSource, count, DataGridViewElementStates.None, DataGridViewElementStates.Displayed | DataGridViewElementStates.Selected);
		}

		// Token: 0x0600223B RID: 8763 RVA: 0x000A2AE8 File Offset: 0x000A0CE8
		internal int AddCopiesInternal(int indexSource, int count, DataGridViewElementStates dgvesAdd, DataGridViewElementStates dgvesRemove)
		{
			if (indexSource < 0 || this.Count <= indexSource)
			{
				throw new ArgumentOutOfRangeException("indexSource", SR.GetString("DataGridViewRowCollection_IndexSourceOutOfRange"));
			}
			if (count <= 0)
			{
				throw new ArgumentOutOfRangeException("count", SR.GetString("DataGridViewRowCollection_CountOutOfRange"));
			}
			DataGridViewElementStates dataGridViewElementStates = this.rowStates[indexSource] & ~dgvesRemove;
			dataGridViewElementStates |= dgvesAdd;
			return this.AddCopiesPrivate(this.SharedRow(indexSource), dataGridViewElementStates, count);
		}

		// Token: 0x0600223C RID: 8764 RVA: 0x000A2B54 File Offset: 0x000A0D54
		private int AddCopiesPrivate(DataGridViewRow rowTemplate, DataGridViewElementStates rowTemplateState, int count)
		{
			int count2 = this.items.Count;
			int num;
			if (rowTemplate.Index == -1)
			{
				this.DataGridView.OnAddingRow(rowTemplate, rowTemplateState, true);
				for (int i = 0; i < count - 1; i++)
				{
					this.SharedList.Add(rowTemplate);
					this.rowStates.Add(rowTemplateState);
				}
				num = this.SharedList.Add(rowTemplate);
				this.rowStates.Add(rowTemplateState);
				this.DataGridView.OnAddedRow_PreNotification(num);
				this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null), count2, count);
				for (int j = 0; j < count; j++)
				{
					this.DataGridView.OnAddedRow_PostNotification(num - (count - 1) + j);
				}
				return num;
			}
			num = this.AddDuplicateRow(rowTemplate, false);
			if (count > 1)
			{
				this.DataGridView.OnAddedRow_PreNotification(num);
				if (this.RowIsSharable(num))
				{
					DataGridViewRow dataGridViewRow = this.SharedRow(num);
					this.DataGridView.OnAddingRow(dataGridViewRow, rowTemplateState, true);
					for (int k = 1; k < count - 1; k++)
					{
						this.SharedList.Add(dataGridViewRow);
						this.rowStates.Add(rowTemplateState);
					}
					num = this.SharedList.Add(dataGridViewRow);
					this.rowStates.Add(rowTemplateState);
					this.DataGridView.OnAddedRow_PreNotification(num);
				}
				else
				{
					this.UnshareRow(num);
					for (int l = 1; l < count; l++)
					{
						num = this.AddDuplicateRow(rowTemplate, false);
						this.UnshareRow(num);
						this.DataGridView.OnAddedRow_PreNotification(num);
					}
				}
				this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null), count2, count);
				for (int m = 0; m < count; m++)
				{
					this.DataGridView.OnAddedRow_PostNotification(num - (count - 1) + m);
				}
				return num;
			}
			if (this.IsCollectionChangedListenedTo)
			{
				this.UnshareRow(num);
			}
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, this.SharedRow(num)), num, 1);
			return num;
		}

		// Token: 0x0600223D RID: 8765 RVA: 0x000A2D20 File Offset: 0x000A0F20
		private int AddDuplicateRow(DataGridViewRow rowTemplate, bool newRow)
		{
			DataGridViewRow dataGridViewRow = (DataGridViewRow)rowTemplate.Clone();
			dataGridViewRow.StateInternal = DataGridViewElementStates.None;
			dataGridViewRow.DataGridViewInternal = this.dataGridView;
			DataGridViewCellCollection cells = dataGridViewRow.Cells;
			int num = 0;
			foreach (object obj in cells)
			{
				DataGridViewCell dataGridViewCell = (DataGridViewCell)obj;
				if (newRow)
				{
					dataGridViewCell.Value = dataGridViewCell.DefaultNewRowValue;
				}
				dataGridViewCell.DataGridViewInternal = this.dataGridView;
				dataGridViewCell.OwningColumnInternal = this.DataGridView.Columns[num];
				num++;
			}
			DataGridViewElementStates dataGridViewElementStates = rowTemplate.State & ~(DataGridViewElementStates.Displayed | DataGridViewElementStates.Selected);
			if (dataGridViewRow.HasHeaderCell)
			{
				dataGridViewRow.HeaderCell.DataGridViewInternal = this.dataGridView;
				dataGridViewRow.HeaderCell.OwningRowInternal = dataGridViewRow;
			}
			this.DataGridView.OnAddingRow(dataGridViewRow, dataGridViewElementStates, true);
			this.rowStates.Add(dataGridViewElementStates);
			return this.SharedList.Add(dataGridViewRow);
		}

		// Token: 0x0600223E RID: 8766 RVA: 0x000A2E2C File Offset: 0x000A102C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual void AddRange(params DataGridViewRow[] dataGridViewRows)
		{
			if (dataGridViewRows == null)
			{
				throw new ArgumentNullException("dataGridViewRows");
			}
			if (this.DataGridView.NoDimensionChangeAllowed)
			{
				throw new InvalidOperationException(SR.GetString("DataGridView_ForbiddenOperationInEventHandler"));
			}
			if (this.DataGridView.DataSource != null)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewRowCollection_AddUnboundRow"));
			}
			if (this.DataGridView.NewRowIndex != -1)
			{
				this.InsertRange(this.Count - 1, dataGridViewRows);
				return;
			}
			if (this.DataGridView.Columns.Count == 0)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewRowCollection_NoColumns"));
			}
			int count = this.items.Count;
			this.DataGridView.OnAddingRows(dataGridViewRows, true);
			foreach (DataGridViewRow dataGridViewRow in dataGridViewRows)
			{
				int num = 0;
				foreach (object obj in dataGridViewRow.Cells)
				{
					DataGridViewCell dataGridViewCell = (DataGridViewCell)obj;
					dataGridViewCell.DataGridViewInternal = this.dataGridView;
					dataGridViewCell.OwningColumnInternal = this.DataGridView.Columns[num];
					num++;
				}
				if (dataGridViewRow.HasHeaderCell)
				{
					dataGridViewRow.HeaderCell.DataGridViewInternal = this.dataGridView;
					dataGridViewRow.HeaderCell.OwningRowInternal = dataGridViewRow;
				}
				int indexInternal = this.SharedList.Add(dataGridViewRow);
				this.rowStates.Add(dataGridViewRow.State);
				dataGridViewRow.IndexInternal = indexInternal;
				dataGridViewRow.DataGridViewInternal = this.dataGridView;
			}
			this.DataGridView.OnAddedRows_PreNotification(dataGridViewRows);
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null), count, dataGridViewRows.Length);
			this.DataGridView.OnAddedRows_PostNotification(dataGridViewRows);
		}

		// Token: 0x0600223F RID: 8767 RVA: 0x000A2FF4 File Offset: 0x000A11F4
		public virtual void Clear()
		{
			if (this.DataGridView.NoDimensionChangeAllowed)
			{
				throw new InvalidOperationException(SR.GetString("DataGridView_ForbiddenOperationInEventHandler"));
			}
			if (this.DataGridView.DataSource == null)
			{
				this.ClearInternal(true);
				return;
			}
			IBindingList bindingList = this.DataGridView.DataConnection.List as IBindingList;
			if (bindingList != null && bindingList.AllowRemove && bindingList.SupportsChangeNotification)
			{
				bindingList.Clear();
				return;
			}
			throw new InvalidOperationException(SR.GetString("DataGridViewRowCollection_CantClearRowCollectionWithWrongSource"));
		}

		// Token: 0x06002240 RID: 8768 RVA: 0x000A3074 File Offset: 0x000A1274
		internal void ClearInternal(bool recreateNewRow)
		{
			int count = this.items.Count;
			if (count > 0)
			{
				this.DataGridView.OnClearingRows();
				for (int i = 0; i < count; i++)
				{
					this.SharedRow(i).DetachFromDataGridView();
				}
				this.SharedList.Clear();
				this.rowStates.Clear();
				this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null), 0, count, true, false, recreateNewRow, new Point(-1, -1));
				return;
			}
			if (recreateNewRow && this.DataGridView.Columns.Count != 0 && this.DataGridView.AllowUserToAddRowsInternal && this.items.Count == 0)
			{
				this.DataGridView.AddNewRow(false);
			}
		}

		// Token: 0x06002241 RID: 8769 RVA: 0x000A311F File Offset: 0x000A131F
		public virtual bool Contains(DataGridViewRow dataGridViewRow)
		{
			return this.items.IndexOf(dataGridViewRow) != -1;
		}

		// Token: 0x06002242 RID: 8770 RVA: 0x000A1FF2 File Offset: 0x000A01F2
		public void CopyTo(DataGridViewRow[] array, int index)
		{
			this.items.CopyTo(array, index);
		}

		// Token: 0x06002243 RID: 8771 RVA: 0x000A3134 File Offset: 0x000A1334
		internal int DisplayIndexToRowIndex(int visibleRowIndex)
		{
			int num = -1;
			for (int i = 0; i < this.Count; i++)
			{
				if ((this.GetRowState(i) & DataGridViewElementStates.Visible) == DataGridViewElementStates.Visible)
				{
					num++;
				}
				if (num == visibleRowIndex)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06002244 RID: 8772 RVA: 0x000A3170 File Offset: 0x000A1370
		public int GetFirstRow(DataGridViewElementStates includeFilter)
		{
			if ((includeFilter & ~(DataGridViewElementStates.Displayed | DataGridViewElementStates.Frozen | DataGridViewElementStates.ReadOnly | DataGridViewElementStates.Resizable | DataGridViewElementStates.Selected | DataGridViewElementStates.Visible)) != DataGridViewElementStates.None)
			{
				throw new ArgumentException(SR.GetString("DataGridView_InvalidDataGridViewElementStateCombination", new object[]
				{
					"includeFilter"
				}));
			}
			if (includeFilter != DataGridViewElementStates.Visible)
			{
				if (includeFilter != (DataGridViewElementStates.Frozen | DataGridViewElementStates.Visible))
				{
					if (includeFilter == (DataGridViewElementStates.Selected | DataGridViewElementStates.Visible))
					{
						if (this.rowCountsVisibleSelected == 0)
						{
							return -1;
						}
					}
				}
				else if (this.rowCountsVisibleFrozen == 0)
				{
					return -1;
				}
			}
			else if (this.rowCountsVisible == 0)
			{
				return -1;
			}
			int num = 0;
			while (num < this.items.Count && (this.GetRowState(num) & includeFilter) != includeFilter)
			{
				num++;
			}
			if (num >= this.items.Count)
			{
				return -1;
			}
			return num;
		}

		// Token: 0x06002245 RID: 8773 RVA: 0x000A3204 File Offset: 0x000A1404
		public int GetFirstRow(DataGridViewElementStates includeFilter, DataGridViewElementStates excludeFilter)
		{
			if (excludeFilter == DataGridViewElementStates.None)
			{
				return this.GetFirstRow(includeFilter);
			}
			if ((includeFilter & ~(DataGridViewElementStates.Displayed | DataGridViewElementStates.Frozen | DataGridViewElementStates.ReadOnly | DataGridViewElementStates.Resizable | DataGridViewElementStates.Selected | DataGridViewElementStates.Visible)) != DataGridViewElementStates.None)
			{
				throw new ArgumentException(SR.GetString("DataGridView_InvalidDataGridViewElementStateCombination", new object[]
				{
					"includeFilter"
				}));
			}
			if ((excludeFilter & ~(DataGridViewElementStates.Displayed | DataGridViewElementStates.Frozen | DataGridViewElementStates.ReadOnly | DataGridViewElementStates.Resizable | DataGridViewElementStates.Selected | DataGridViewElementStates.Visible)) != DataGridViewElementStates.None)
			{
				throw new ArgumentException(SR.GetString("DataGridView_InvalidDataGridViewElementStateCombination", new object[]
				{
					"excludeFilter"
				}));
			}
			if (includeFilter != DataGridViewElementStates.Visible)
			{
				if (includeFilter != (DataGridViewElementStates.Frozen | DataGridViewElementStates.Visible))
				{
					if (includeFilter == (DataGridViewElementStates.Selected | DataGridViewElementStates.Visible))
					{
						if (this.rowCountsVisibleSelected == 0)
						{
							return -1;
						}
					}
				}
				else if (this.rowCountsVisibleFrozen == 0)
				{
					return -1;
				}
			}
			else if (this.rowCountsVisible == 0)
			{
				return -1;
			}
			int num = 0;
			while (num < this.items.Count && ((this.GetRowState(num) & includeFilter) != includeFilter || (this.GetRowState(num) & excludeFilter) != DataGridViewElementStates.None))
			{
				num++;
			}
			if (num >= this.items.Count)
			{
				return -1;
			}
			return num;
		}

		// Token: 0x06002246 RID: 8774 RVA: 0x000A32D4 File Offset: 0x000A14D4
		public int GetLastRow(DataGridViewElementStates includeFilter)
		{
			if ((includeFilter & ~(DataGridViewElementStates.Displayed | DataGridViewElementStates.Frozen | DataGridViewElementStates.ReadOnly | DataGridViewElementStates.Resizable | DataGridViewElementStates.Selected | DataGridViewElementStates.Visible)) != DataGridViewElementStates.None)
			{
				throw new ArgumentException(SR.GetString("DataGridView_InvalidDataGridViewElementStateCombination", new object[]
				{
					"includeFilter"
				}));
			}
			if (includeFilter != DataGridViewElementStates.Visible)
			{
				if (includeFilter != (DataGridViewElementStates.Frozen | DataGridViewElementStates.Visible))
				{
					if (includeFilter == (DataGridViewElementStates.Selected | DataGridViewElementStates.Visible))
					{
						if (this.rowCountsVisibleSelected == 0)
						{
							return -1;
						}
					}
				}
				else if (this.rowCountsVisibleFrozen == 0)
				{
					return -1;
				}
			}
			else if (this.rowCountsVisible == 0)
			{
				return -1;
			}
			int num = this.items.Count - 1;
			while (num >= 0 && (this.GetRowState(num) & includeFilter) != includeFilter)
			{
				num--;
			}
			if (num < 0)
			{
				return -1;
			}
			return num;
		}

		// Token: 0x06002247 RID: 8775 RVA: 0x000A3360 File Offset: 0x000A1560
		internal int GetNextRow(int indexStart, DataGridViewElementStates includeFilter, int skipRows)
		{
			int num = indexStart;
			do
			{
				num = this.GetNextRow(num, includeFilter);
				skipRows--;
			}
			while (skipRows >= 0 && num != -1);
			return num;
		}

		// Token: 0x06002248 RID: 8776 RVA: 0x000A3388 File Offset: 0x000A1588
		public int GetNextRow(int indexStart, DataGridViewElementStates includeFilter)
		{
			if ((includeFilter & ~(DataGridViewElementStates.Displayed | DataGridViewElementStates.Frozen | DataGridViewElementStates.ReadOnly | DataGridViewElementStates.Resizable | DataGridViewElementStates.Selected | DataGridViewElementStates.Visible)) != DataGridViewElementStates.None)
			{
				throw new ArgumentException(SR.GetString("DataGridView_InvalidDataGridViewElementStateCombination", new object[]
				{
					"includeFilter"
				}));
			}
			if (indexStart < -1)
			{
				throw new ArgumentOutOfRangeException("indexStart", SR.GetString("InvalidLowBoundArgumentEx", new object[]
				{
					"indexStart",
					indexStart.ToString(CultureInfo.CurrentCulture),
					-1.ToString(CultureInfo.CurrentCulture)
				}));
			}
			int num = indexStart + 1;
			while (num < this.items.Count && (this.GetRowState(num) & includeFilter) != includeFilter)
			{
				num++;
			}
			if (num >= this.items.Count)
			{
				return -1;
			}
			return num;
		}

		// Token: 0x06002249 RID: 8777 RVA: 0x000A3438 File Offset: 0x000A1638
		public int GetNextRow(int indexStart, DataGridViewElementStates includeFilter, DataGridViewElementStates excludeFilter)
		{
			if (excludeFilter == DataGridViewElementStates.None)
			{
				return this.GetNextRow(indexStart, includeFilter);
			}
			if ((includeFilter & ~(DataGridViewElementStates.Displayed | DataGridViewElementStates.Frozen | DataGridViewElementStates.ReadOnly | DataGridViewElementStates.Resizable | DataGridViewElementStates.Selected | DataGridViewElementStates.Visible)) != DataGridViewElementStates.None)
			{
				throw new ArgumentException(SR.GetString("DataGridView_InvalidDataGridViewElementStateCombination", new object[]
				{
					"includeFilter"
				}));
			}
			if ((excludeFilter & ~(DataGridViewElementStates.Displayed | DataGridViewElementStates.Frozen | DataGridViewElementStates.ReadOnly | DataGridViewElementStates.Resizable | DataGridViewElementStates.Selected | DataGridViewElementStates.Visible)) != DataGridViewElementStates.None)
			{
				throw new ArgumentException(SR.GetString("DataGridView_InvalidDataGridViewElementStateCombination", new object[]
				{
					"excludeFilter"
				}));
			}
			if (indexStart < -1)
			{
				throw new ArgumentOutOfRangeException("indexStart", SR.GetString("InvalidLowBoundArgumentEx", new object[]
				{
					"indexStart",
					indexStart.ToString(CultureInfo.CurrentCulture),
					-1.ToString(CultureInfo.CurrentCulture)
				}));
			}
			int num = indexStart + 1;
			while (num < this.items.Count && ((this.GetRowState(num) & includeFilter) != includeFilter || (this.GetRowState(num) & excludeFilter) != DataGridViewElementStates.None))
			{
				num++;
			}
			if (num >= this.items.Count)
			{
				return -1;
			}
			return num;
		}

		// Token: 0x0600224A RID: 8778 RVA: 0x000A3520 File Offset: 0x000A1720
		public int GetPreviousRow(int indexStart, DataGridViewElementStates includeFilter)
		{
			if ((includeFilter & ~(DataGridViewElementStates.Displayed | DataGridViewElementStates.Frozen | DataGridViewElementStates.ReadOnly | DataGridViewElementStates.Resizable | DataGridViewElementStates.Selected | DataGridViewElementStates.Visible)) != DataGridViewElementStates.None)
			{
				throw new ArgumentException(SR.GetString("DataGridView_InvalidDataGridViewElementStateCombination", new object[]
				{
					"includeFilter"
				}));
			}
			if (indexStart > this.items.Count)
			{
				throw new ArgumentOutOfRangeException("indexStart", SR.GetString("InvalidHighBoundArgumentEx", new object[]
				{
					"indexStart",
					indexStart.ToString(CultureInfo.CurrentCulture),
					this.items.Count.ToString(CultureInfo.CurrentCulture)
				}));
			}
			int num = indexStart - 1;
			while (num >= 0 && (this.GetRowState(num) & includeFilter) != includeFilter)
			{
				num--;
			}
			if (num < 0)
			{
				return -1;
			}
			return num;
		}

		// Token: 0x0600224B RID: 8779 RVA: 0x000A35D0 File Offset: 0x000A17D0
		public int GetPreviousRow(int indexStart, DataGridViewElementStates includeFilter, DataGridViewElementStates excludeFilter)
		{
			if (excludeFilter == DataGridViewElementStates.None)
			{
				return this.GetPreviousRow(indexStart, includeFilter);
			}
			if ((includeFilter & ~(DataGridViewElementStates.Displayed | DataGridViewElementStates.Frozen | DataGridViewElementStates.ReadOnly | DataGridViewElementStates.Resizable | DataGridViewElementStates.Selected | DataGridViewElementStates.Visible)) != DataGridViewElementStates.None)
			{
				throw new ArgumentException(SR.GetString("DataGridView_InvalidDataGridViewElementStateCombination", new object[]
				{
					"includeFilter"
				}));
			}
			if ((excludeFilter & ~(DataGridViewElementStates.Displayed | DataGridViewElementStates.Frozen | DataGridViewElementStates.ReadOnly | DataGridViewElementStates.Resizable | DataGridViewElementStates.Selected | DataGridViewElementStates.Visible)) != DataGridViewElementStates.None)
			{
				throw new ArgumentException(SR.GetString("DataGridView_InvalidDataGridViewElementStateCombination", new object[]
				{
					"excludeFilter"
				}));
			}
			if (indexStart > this.items.Count)
			{
				throw new ArgumentOutOfRangeException("indexStart", SR.GetString("InvalidHighBoundArgumentEx", new object[]
				{
					"indexStart",
					indexStart.ToString(CultureInfo.CurrentCulture),
					this.items.Count.ToString(CultureInfo.CurrentCulture)
				}));
			}
			int num = indexStart - 1;
			while (num >= 0 && ((this.GetRowState(num) & includeFilter) != includeFilter || (this.GetRowState(num) & excludeFilter) != DataGridViewElementStates.None))
			{
				num--;
			}
			if (num < 0)
			{
				return -1;
			}
			return num;
		}

		// Token: 0x0600224C RID: 8780 RVA: 0x000A36B8 File Offset: 0x000A18B8
		internal int GetVisibleIndex(DataGridViewRow row)
		{
			for (int i = 0; i < this.Count; i++)
			{
				int num = this.DisplayIndexToRowIndex(i);
				if (num != -1 && this.items[num] == row)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600224D RID: 8781 RVA: 0x000A36F4 File Offset: 0x000A18F4
		public int GetRowCount(DataGridViewElementStates includeFilter)
		{
			if ((includeFilter & ~(DataGridViewElementStates.Displayed | DataGridViewElementStates.Frozen | DataGridViewElementStates.ReadOnly | DataGridViewElementStates.Resizable | DataGridViewElementStates.Selected | DataGridViewElementStates.Visible)) != DataGridViewElementStates.None)
			{
				throw new ArgumentException(SR.GetString("DataGridView_InvalidDataGridViewElementStateCombination", new object[]
				{
					"includeFilter"
				}));
			}
			if (includeFilter != DataGridViewElementStates.Visible)
			{
				if (includeFilter != (DataGridViewElementStates.Frozen | DataGridViewElementStates.Visible))
				{
					if (includeFilter == (DataGridViewElementStates.Selected | DataGridViewElementStates.Visible))
					{
						if (this.rowCountsVisibleSelected != -1)
						{
							return this.rowCountsVisibleSelected;
						}
					}
				}
				else if (this.rowCountsVisibleFrozen != -1)
				{
					return this.rowCountsVisibleFrozen;
				}
			}
			else if (this.rowCountsVisible != -1)
			{
				return this.rowCountsVisible;
			}
			int num = 0;
			for (int i = 0; i < this.items.Count; i++)
			{
				if ((this.GetRowState(i) & includeFilter) == includeFilter)
				{
					num++;
				}
			}
			if (includeFilter != DataGridViewElementStates.Visible)
			{
				if (includeFilter != (DataGridViewElementStates.Frozen | DataGridViewElementStates.Visible))
				{
					if (includeFilter == (DataGridViewElementStates.Selected | DataGridViewElementStates.Visible))
					{
						this.rowCountsVisibleSelected = num;
					}
				}
				else
				{
					this.rowCountsVisibleFrozen = num;
				}
			}
			else
			{
				this.rowCountsVisible = num;
			}
			return num;
		}

		// Token: 0x0600224E RID: 8782 RVA: 0x000A37BC File Offset: 0x000A19BC
		internal int GetRowCount(DataGridViewElementStates includeFilter, int fromRowIndex, int toRowIndex)
		{
			int num = 0;
			for (int i = fromRowIndex + 1; i <= toRowIndex; i++)
			{
				if ((this.GetRowState(i) & includeFilter) == includeFilter)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x0600224F RID: 8783 RVA: 0x000A37EC File Offset: 0x000A19EC
		public int GetRowsHeight(DataGridViewElementStates includeFilter)
		{
			if ((includeFilter & ~(DataGridViewElementStates.Displayed | DataGridViewElementStates.Frozen | DataGridViewElementStates.ReadOnly | DataGridViewElementStates.Resizable | DataGridViewElementStates.Selected | DataGridViewElementStates.Visible)) != DataGridViewElementStates.None)
			{
				throw new ArgumentException(SR.GetString("DataGridView_InvalidDataGridViewElementStateCombination", new object[]
				{
					"includeFilter"
				}));
			}
			if (includeFilter != DataGridViewElementStates.Visible)
			{
				if (includeFilter == (DataGridViewElementStates.Frozen | DataGridViewElementStates.Visible))
				{
					if (this.rowsHeightVisibleFrozen != -1)
					{
						return this.rowsHeightVisibleFrozen;
					}
				}
			}
			else if (this.rowsHeightVisible != -1)
			{
				return this.rowsHeightVisible;
			}
			int num = 0;
			for (int i = 0; i < this.items.Count; i++)
			{
				if ((this.GetRowState(i) & includeFilter) == includeFilter)
				{
					num += ((DataGridViewRow)this.items[i]).GetHeight(i);
				}
			}
			if (includeFilter != DataGridViewElementStates.Visible)
			{
				if (includeFilter == (DataGridViewElementStates.Frozen | DataGridViewElementStates.Visible))
				{
					this.rowsHeightVisibleFrozen = num;
				}
			}
			else
			{
				this.rowsHeightVisible = num;
			}
			return num;
		}

		// Token: 0x06002250 RID: 8784 RVA: 0x000A38A4 File Offset: 0x000A1AA4
		internal int GetRowsHeight(DataGridViewElementStates includeFilter, int fromRowIndex, int toRowIndex)
		{
			int num = 0;
			for (int i = fromRowIndex; i < toRowIndex; i++)
			{
				if ((this.GetRowState(i) & includeFilter) == includeFilter)
				{
					num += ((DataGridViewRow)this.items[i]).GetHeight(i);
				}
			}
			return num;
		}

		// Token: 0x06002251 RID: 8785 RVA: 0x000A38E8 File Offset: 0x000A1AE8
		private bool GetRowsHeightExceedLimit(DataGridViewElementStates includeFilter, int fromRowIndex, int toRowIndex, int heightLimit)
		{
			int num = 0;
			for (int i = fromRowIndex; i < toRowIndex; i++)
			{
				if ((this.GetRowState(i) & includeFilter) == includeFilter)
				{
					num += ((DataGridViewRow)this.items[i]).GetHeight(i);
					if (num > heightLimit)
					{
						return true;
					}
				}
			}
			return num > heightLimit;
		}

		// Token: 0x06002252 RID: 8786 RVA: 0x000A3938 File Offset: 0x000A1B38
		public virtual DataGridViewElementStates GetRowState(int rowIndex)
		{
			if (rowIndex < 0 || rowIndex >= this.items.Count)
			{
				throw new ArgumentOutOfRangeException("rowIndex", SR.GetString("DataGridViewRowCollection_RowIndexOutOfRange"));
			}
			DataGridViewRow dataGridViewRow = this.SharedRow(rowIndex);
			if (dataGridViewRow.Index == -1)
			{
				return this.SharedRowState(rowIndex);
			}
			return dataGridViewRow.GetState(rowIndex);
		}

		// Token: 0x06002253 RID: 8787 RVA: 0x000A1FB5 File Offset: 0x000A01B5
		public int IndexOf(DataGridViewRow dataGridViewRow)
		{
			return this.items.IndexOf(dataGridViewRow);
		}

		// Token: 0x06002254 RID: 8788 RVA: 0x000A398C File Offset: 0x000A1B8C
		public virtual void Insert(int rowIndex, params object[] values)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			if (this.DataGridView.VirtualMode)
			{
				throw new InvalidOperationException(SR.GetString("DataGridView_InvalidOperationInVirtualMode"));
			}
			if (this.DataGridView.DataSource != null)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewRowCollection_AddUnboundRow"));
			}
			DataGridViewRow rowTemplateClone = this.DataGridView.RowTemplateClone;
			rowTemplateClone.SetValuesInternal(values);
			this.Insert(rowIndex, rowTemplateClone);
		}

		// Token: 0x06002255 RID: 8789 RVA: 0x000A3A00 File Offset: 0x000A1C00
		public virtual void Insert(int rowIndex, DataGridViewRow dataGridViewRow)
		{
			if (this.DataGridView.DataSource != null)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewRowCollection_AddUnboundRow"));
			}
			if (this.DataGridView.NoDimensionChangeAllowed)
			{
				throw new InvalidOperationException(SR.GetString("DataGridView_ForbiddenOperationInEventHandler"));
			}
			this.InsertInternal(rowIndex, dataGridViewRow);
		}

		// Token: 0x06002256 RID: 8790 RVA: 0x000A3A50 File Offset: 0x000A1C50
		public virtual void Insert(int rowIndex, int count)
		{
			if (this.DataGridView.DataSource != null)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewRowCollection_AddUnboundRow"));
			}
			if (rowIndex < 0 || this.Count < rowIndex)
			{
				throw new ArgumentOutOfRangeException("rowIndex", SR.GetString("DataGridViewRowCollection_IndexDestinationOutOfRange"));
			}
			if (count <= 0)
			{
				throw new ArgumentOutOfRangeException("count", SR.GetString("DataGridViewRowCollection_CountOutOfRange"));
			}
			if (this.DataGridView.NoDimensionChangeAllowed)
			{
				throw new InvalidOperationException(SR.GetString("DataGridView_ForbiddenOperationInEventHandler"));
			}
			if (this.DataGridView.Columns.Count == 0)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewRowCollection_NoColumns"));
			}
			if (this.DataGridView.RowTemplate.Cells.Count > this.DataGridView.Columns.Count)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewRowCollection_RowTemplateTooManyCells"));
			}
			if (this.DataGridView.NewRowIndex != -1 && rowIndex == this.Count)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewRowCollection_NoInsertionAfterNewRow"));
			}
			DataGridViewRow rowTemplateClone = this.DataGridView.RowTemplateClone;
			DataGridViewElementStates state = rowTemplateClone.State;
			rowTemplateClone.DataGridViewInternal = this.dataGridView;
			int num = 0;
			foreach (object obj in rowTemplateClone.Cells)
			{
				DataGridViewCell dataGridViewCell = (DataGridViewCell)obj;
				dataGridViewCell.DataGridViewInternal = this.dataGridView;
				dataGridViewCell.OwningColumnInternal = this.DataGridView.Columns[num];
				num++;
			}
			if (rowTemplateClone.HasHeaderCell)
			{
				rowTemplateClone.HeaderCell.DataGridViewInternal = this.dataGridView;
				rowTemplateClone.HeaderCell.OwningRowInternal = rowTemplateClone;
			}
			this.InsertCopiesPrivate(rowTemplateClone, state, rowIndex, count);
		}

		// Token: 0x06002257 RID: 8791 RVA: 0x000A3C14 File Offset: 0x000A1E14
		internal void InsertInternal(int rowIndex, DataGridViewRow dataGridViewRow)
		{
			if (rowIndex < 0 || this.Count < rowIndex)
			{
				throw new ArgumentOutOfRangeException("rowIndex", SR.GetString("DataGridViewRowCollection_RowIndexOutOfRange"));
			}
			if (dataGridViewRow == null)
			{
				throw new ArgumentNullException("dataGridViewRow");
			}
			if (dataGridViewRow.DataGridView != null)
			{
				throw new InvalidOperationException(SR.GetString("DataGridView_RowAlreadyBelongsToDataGridView"));
			}
			if (this.DataGridView.NewRowIndex != -1 && rowIndex == this.Count)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewRowCollection_NoInsertionAfterNewRow"));
			}
			if (this.DataGridView.Columns.Count == 0)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewRowCollection_NoColumns"));
			}
			if (dataGridViewRow.Cells.Count > this.DataGridView.Columns.Count)
			{
				throw new ArgumentException(SR.GetString("DataGridViewRowCollection_TooManyCells"), "dataGridViewRow");
			}
			if (dataGridViewRow.Selected)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewRowCollection_CannotAddOrInsertSelectedRow"));
			}
			this.InsertInternal(rowIndex, dataGridViewRow, false);
		}

		// Token: 0x06002258 RID: 8792 RVA: 0x000A3D08 File Offset: 0x000A1F08
		internal void InsertInternal(int rowIndex, DataGridViewRow dataGridViewRow, bool force)
		{
			Point newCurrentCell = new Point(-1, -1);
			if (force)
			{
				if (this.DataGridView.Columns.Count == 0)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewRowCollection_NoColumns"));
				}
				if (dataGridViewRow.Cells.Count > this.DataGridView.Columns.Count)
				{
					throw new ArgumentException(SR.GetString("DataGridViewRowCollection_TooManyCells"), "dataGridViewRow");
				}
			}
			this.DataGridView.CompleteCellsCollection(dataGridViewRow);
			this.DataGridView.OnInsertingRow(rowIndex, dataGridViewRow, dataGridViewRow.State, ref newCurrentCell, true, 1, force);
			int num = 0;
			foreach (object obj in dataGridViewRow.Cells)
			{
				DataGridViewCell dataGridViewCell = (DataGridViewCell)obj;
				dataGridViewCell.DataGridViewInternal = this.dataGridView;
				if (dataGridViewCell.ColumnIndex == -1)
				{
					dataGridViewCell.OwningColumnInternal = this.DataGridView.Columns[num];
				}
				num++;
			}
			if (dataGridViewRow.HasHeaderCell)
			{
				dataGridViewRow.HeaderCell.DataGridViewInternal = this.DataGridView;
				dataGridViewRow.HeaderCell.OwningRowInternal = dataGridViewRow;
			}
			this.SharedList.Insert(rowIndex, dataGridViewRow);
			this.rowStates.Insert(rowIndex, dataGridViewRow.State);
			dataGridViewRow.DataGridViewInternal = this.dataGridView;
			if (!this.RowIsSharable(rowIndex) || DataGridViewRowCollection.RowHasValueOrToolTipText(dataGridViewRow) || this.IsCollectionChangedListenedTo)
			{
				dataGridViewRow.IndexInternal = rowIndex;
			}
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, dataGridViewRow), rowIndex, 1, false, true, false, newCurrentCell);
		}

		// Token: 0x06002259 RID: 8793 RVA: 0x000A3E98 File Offset: 0x000A2098
		public virtual void InsertCopy(int indexSource, int indexDestination)
		{
			this.InsertCopies(indexSource, indexDestination, 1);
		}

		// Token: 0x0600225A RID: 8794 RVA: 0x000A3EA4 File Offset: 0x000A20A4
		public virtual void InsertCopies(int indexSource, int indexDestination, int count)
		{
			if (this.DataGridView.DataSource != null)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewRowCollection_AddUnboundRow"));
			}
			if (this.DataGridView.NoDimensionChangeAllowed)
			{
				throw new InvalidOperationException(SR.GetString("DataGridView_ForbiddenOperationInEventHandler"));
			}
			this.InsertCopiesPrivate(indexSource, indexDestination, count);
		}

		// Token: 0x0600225B RID: 8795 RVA: 0x000A3EF4 File Offset: 0x000A20F4
		private void InsertCopiesPrivate(int indexSource, int indexDestination, int count)
		{
			if (indexSource < 0 || this.Count <= indexSource)
			{
				throw new ArgumentOutOfRangeException("indexSource", SR.GetString("DataGridViewRowCollection_IndexSourceOutOfRange"));
			}
			if (indexDestination < 0 || this.Count < indexDestination)
			{
				throw new ArgumentOutOfRangeException("indexDestination", SR.GetString("DataGridViewRowCollection_IndexDestinationOutOfRange"));
			}
			if (count <= 0)
			{
				throw new ArgumentOutOfRangeException("count", SR.GetString("DataGridViewRowCollection_CountOutOfRange"));
			}
			if (this.DataGridView.NewRowIndex != -1 && indexDestination == this.Count)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewRowCollection_NoInsertionAfterNewRow"));
			}
			DataGridViewElementStates rowTemplateState = this.GetRowState(indexSource) & ~(DataGridViewElementStates.Displayed | DataGridViewElementStates.Selected);
			this.InsertCopiesPrivate(this.SharedRow(indexSource), rowTemplateState, indexDestination, count);
		}

		// Token: 0x0600225C RID: 8796 RVA: 0x000A3FA0 File Offset: 0x000A21A0
		private void InsertCopiesPrivate(DataGridViewRow rowTemplate, DataGridViewElementStates rowTemplateState, int indexDestination, int count)
		{
			Point newCurrentCell = new Point(-1, -1);
			if (rowTemplate.Index == -1)
			{
				if (count > 1)
				{
					this.DataGridView.OnInsertingRow(indexDestination, rowTemplate, rowTemplateState, ref newCurrentCell, true, count, false);
					for (int i = 0; i < count; i++)
					{
						this.SharedList.Insert(indexDestination + i, rowTemplate);
						this.rowStates.Insert(indexDestination + i, rowTemplateState);
					}
					this.DataGridView.OnInsertedRow_PreNotification(indexDestination, count);
					this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null), indexDestination, count, false, true, false, newCurrentCell);
					for (int j = 0; j < count; j++)
					{
						this.DataGridView.OnInsertedRow_PostNotification(indexDestination + j, newCurrentCell, j == count - 1);
					}
					return;
				}
				this.DataGridView.OnInsertingRow(indexDestination, rowTemplate, rowTemplateState, ref newCurrentCell, true, 1, false);
				this.SharedList.Insert(indexDestination, rowTemplate);
				this.rowStates.Insert(indexDestination, rowTemplateState);
				this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, this.SharedRow(indexDestination)), indexDestination, count, false, true, false, newCurrentCell);
				return;
			}
			else
			{
				this.InsertDuplicateRow(indexDestination, rowTemplate, true, ref newCurrentCell);
				if (count > 1)
				{
					this.DataGridView.OnInsertedRow_PreNotification(indexDestination, 1);
					if (this.RowIsSharable(indexDestination))
					{
						DataGridViewRow dataGridViewRow = this.SharedRow(indexDestination);
						this.DataGridView.OnInsertingRow(indexDestination + 1, dataGridViewRow, rowTemplateState, ref newCurrentCell, false, count - 1, false);
						for (int k = 1; k < count; k++)
						{
							this.SharedList.Insert(indexDestination + k, dataGridViewRow);
							this.rowStates.Insert(indexDestination + k, rowTemplateState);
						}
						this.DataGridView.OnInsertedRow_PreNotification(indexDestination + 1, count - 1);
						this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null), indexDestination, count, false, true, false, newCurrentCell);
					}
					else
					{
						this.UnshareRow(indexDestination);
						for (int l = 1; l < count; l++)
						{
							this.InsertDuplicateRow(indexDestination + l, rowTemplate, false, ref newCurrentCell);
							this.UnshareRow(indexDestination + l);
							this.DataGridView.OnInsertedRow_PreNotification(indexDestination + l, 1);
						}
						this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null), indexDestination, count, false, true, false, newCurrentCell);
					}
					for (int m = 0; m < count; m++)
					{
						this.DataGridView.OnInsertedRow_PostNotification(indexDestination + m, newCurrentCell, m == count - 1);
					}
					return;
				}
				if (this.IsCollectionChangedListenedTo)
				{
					this.UnshareRow(indexDestination);
				}
				this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, this.SharedRow(indexDestination)), indexDestination, 1, false, true, false, newCurrentCell);
				return;
			}
		}

		// Token: 0x0600225D RID: 8797 RVA: 0x000A41E0 File Offset: 0x000A23E0
		private void InsertDuplicateRow(int indexDestination, DataGridViewRow rowTemplate, bool firstInsertion, ref Point newCurrentCell)
		{
			DataGridViewRow dataGridViewRow = (DataGridViewRow)rowTemplate.Clone();
			dataGridViewRow.StateInternal = DataGridViewElementStates.None;
			dataGridViewRow.DataGridViewInternal = this.dataGridView;
			DataGridViewCellCollection cells = dataGridViewRow.Cells;
			int num = 0;
			foreach (object obj in cells)
			{
				DataGridViewCell dataGridViewCell = (DataGridViewCell)obj;
				dataGridViewCell.DataGridViewInternal = this.dataGridView;
				dataGridViewCell.OwningColumnInternal = this.DataGridView.Columns[num];
				num++;
			}
			DataGridViewElementStates dataGridViewElementStates = rowTemplate.State & ~(DataGridViewElementStates.Displayed | DataGridViewElementStates.Selected);
			if (dataGridViewRow.HasHeaderCell)
			{
				dataGridViewRow.HeaderCell.DataGridViewInternal = this.dataGridView;
				dataGridViewRow.HeaderCell.OwningRowInternal = dataGridViewRow;
			}
			this.DataGridView.OnInsertingRow(indexDestination, dataGridViewRow, dataGridViewElementStates, ref newCurrentCell, firstInsertion, 1, false);
			this.SharedList.Insert(indexDestination, dataGridViewRow);
			this.rowStates.Insert(indexDestination, dataGridViewElementStates);
		}

		// Token: 0x0600225E RID: 8798 RVA: 0x000A42E4 File Offset: 0x000A24E4
		public virtual void InsertRange(int rowIndex, params DataGridViewRow[] dataGridViewRows)
		{
			if (dataGridViewRows == null)
			{
				throw new ArgumentNullException("dataGridViewRows");
			}
			if (dataGridViewRows.Length == 1)
			{
				this.Insert(rowIndex, dataGridViewRows[0]);
				return;
			}
			if (rowIndex < 0 || rowIndex > this.Count)
			{
				throw new ArgumentOutOfRangeException("rowIndex", SR.GetString("DataGridViewRowCollection_IndexDestinationOutOfRange"));
			}
			if (this.DataGridView.NoDimensionChangeAllowed)
			{
				throw new InvalidOperationException(SR.GetString("DataGridView_ForbiddenOperationInEventHandler"));
			}
			if (this.DataGridView.NewRowIndex != -1 && rowIndex == this.Count)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewRowCollection_NoInsertionAfterNewRow"));
			}
			if (this.DataGridView.DataSource != null)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewRowCollection_AddUnboundRow"));
			}
			if (this.DataGridView.Columns.Count == 0)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewRowCollection_NoColumns"));
			}
			Point newCurrentCell = new Point(-1, -1);
			this.DataGridView.OnInsertingRows(rowIndex, dataGridViewRows, ref newCurrentCell);
			int num = rowIndex;
			foreach (DataGridViewRow dataGridViewRow in dataGridViewRows)
			{
				int num2 = 0;
				foreach (object obj in dataGridViewRow.Cells)
				{
					DataGridViewCell dataGridViewCell = (DataGridViewCell)obj;
					dataGridViewCell.DataGridViewInternal = this.dataGridView;
					if (dataGridViewCell.ColumnIndex == -1)
					{
						dataGridViewCell.OwningColumnInternal = this.DataGridView.Columns[num2];
					}
					num2++;
				}
				if (dataGridViewRow.HasHeaderCell)
				{
					dataGridViewRow.HeaderCell.DataGridViewInternal = this.DataGridView;
					dataGridViewRow.HeaderCell.OwningRowInternal = dataGridViewRow;
				}
				this.SharedList.Insert(num, dataGridViewRow);
				this.rowStates.Insert(num, dataGridViewRow.State);
				dataGridViewRow.IndexInternal = num;
				dataGridViewRow.DataGridViewInternal = this.dataGridView;
				num++;
			}
			this.DataGridView.OnInsertedRows_PreNotification(rowIndex, dataGridViewRows);
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null), rowIndex, dataGridViewRows.Length, false, true, false, newCurrentCell);
			this.DataGridView.OnInsertedRows_PostNotification(dataGridViewRows, newCurrentCell);
		}

		// Token: 0x0600225F RID: 8799 RVA: 0x000A4504 File Offset: 0x000A2704
		internal void InvalidateCachedRowCount(DataGridViewElementStates includeFilter)
		{
			if (includeFilter == DataGridViewElementStates.Visible)
			{
				this.InvalidateCachedRowCounts();
				return;
			}
			if (includeFilter == DataGridViewElementStates.Frozen)
			{
				this.rowCountsVisibleFrozen = -1;
				return;
			}
			if (includeFilter == DataGridViewElementStates.Selected)
			{
				this.rowCountsVisibleSelected = -1;
			}
		}

		// Token: 0x06002260 RID: 8800 RVA: 0x000A452C File Offset: 0x000A272C
		internal void InvalidateCachedRowCounts()
		{
			this.rowCountsVisible = (this.rowCountsVisibleFrozen = (this.rowCountsVisibleSelected = -1));
		}

		// Token: 0x06002261 RID: 8801 RVA: 0x000A4552 File Offset: 0x000A2752
		internal void InvalidateCachedRowsHeight(DataGridViewElementStates includeFilter)
		{
			if (includeFilter == DataGridViewElementStates.Visible)
			{
				this.InvalidateCachedRowsHeights();
				return;
			}
			if (includeFilter == DataGridViewElementStates.Frozen)
			{
				this.rowsHeightVisibleFrozen = -1;
			}
		}

		// Token: 0x06002262 RID: 8802 RVA: 0x000A456C File Offset: 0x000A276C
		internal void InvalidateCachedRowsHeights()
		{
			this.rowsHeightVisible = (this.rowsHeightVisibleFrozen = -1);
		}

		// Token: 0x06002263 RID: 8803 RVA: 0x000A4589 File Offset: 0x000A2789
		protected virtual void OnCollectionChanged(CollectionChangeEventArgs e)
		{
			if (this.onCollectionChanged != null)
			{
				this.onCollectionChanged(this, e);
			}
		}

		// Token: 0x06002264 RID: 8804 RVA: 0x000A45A0 File Offset: 0x000A27A0
		private void OnCollectionChanged(CollectionChangeEventArgs e, int rowIndex, int rowCount)
		{
			Point newCurrentCell = new Point(-1, -1);
			DataGridViewRow dataGridViewRow = (DataGridViewRow)e.Element;
			int num = 0;
			if (dataGridViewRow != null && e.Action == CollectionChangeAction.Add)
			{
				num = this.SharedRow(rowIndex).Index;
			}
			this.OnCollectionChanged_PreNotification(e.Action, rowIndex, rowCount, ref dataGridViewRow, false);
			if (num == -1 && this.SharedRow(rowIndex).Index != -1)
			{
				e = new CollectionChangeEventArgs(e.Action, dataGridViewRow);
			}
			this.OnCollectionChanged(e);
			this.OnCollectionChanged_PostNotification(e.Action, rowIndex, rowCount, dataGridViewRow, false, false, false, newCurrentCell);
		}

		// Token: 0x06002265 RID: 8805 RVA: 0x000A462C File Offset: 0x000A282C
		private void OnCollectionChanged(CollectionChangeEventArgs e, int rowIndex, int rowCount, bool changeIsDeletion, bool changeIsInsertion, bool recreateNewRow, Point newCurrentCell)
		{
			DataGridViewRow dataGridViewRow = (DataGridViewRow)e.Element;
			int num = 0;
			if (dataGridViewRow != null && e.Action == CollectionChangeAction.Add)
			{
				num = this.SharedRow(rowIndex).Index;
			}
			this.OnCollectionChanged_PreNotification(e.Action, rowIndex, rowCount, ref dataGridViewRow, changeIsInsertion);
			if (num == -1 && this.SharedRow(rowIndex).Index != -1)
			{
				e = new CollectionChangeEventArgs(e.Action, dataGridViewRow);
			}
			this.OnCollectionChanged(e);
			this.OnCollectionChanged_PostNotification(e.Action, rowIndex, rowCount, dataGridViewRow, changeIsDeletion, changeIsInsertion, recreateNewRow, newCurrentCell);
		}

		// Token: 0x06002266 RID: 8806 RVA: 0x000A46B4 File Offset: 0x000A28B4
		private void OnCollectionChanged_PreNotification(CollectionChangeAction cca, int rowIndex, int rowCount, ref DataGridViewRow dataGridViewRow, bool changeIsInsertion)
		{
			bool flag = false;
			bool computeVisibleRows = false;
			switch (cca)
			{
			case CollectionChangeAction.Add:
			{
				int num = 0;
				this.UpdateRowCaches(rowIndex, ref dataGridViewRow, true);
				if ((this.GetRowState(rowIndex) & DataGridViewElementStates.Visible) == DataGridViewElementStates.None)
				{
					flag = true;
					computeVisibleRows = changeIsInsertion;
				}
				else
				{
					int firstDisplayedRowIndex = this.DataGridView.FirstDisplayedRowIndex;
					if (firstDisplayedRowIndex != -1)
					{
						num = this.SharedRow(firstDisplayedRowIndex).GetHeight(firstDisplayedRowIndex);
					}
				}
				if (changeIsInsertion)
				{
					this.DataGridView.OnInsertedRow_PreNotification(rowIndex, 1);
					if (!flag)
					{
						if ((this.GetRowState(rowIndex) & DataGridViewElementStates.Frozen) != DataGridViewElementStates.None)
						{
							flag = (this.DataGridView.FirstDisplayedScrollingRowIndex == -1 && this.GetRowsHeightExceedLimit(DataGridViewElementStates.Visible, 0, rowIndex, this.DataGridView.LayoutInfo.Data.Height));
						}
						else if (this.DataGridView.FirstDisplayedScrollingRowIndex != -1 && rowIndex > this.DataGridView.FirstDisplayedScrollingRowIndex)
						{
							flag = (this.GetRowsHeightExceedLimit(DataGridViewElementStates.Visible, 0, rowIndex, this.DataGridView.LayoutInfo.Data.Height + this.DataGridView.VerticalScrollingOffset) && num <= this.DataGridView.LayoutInfo.Data.Height);
						}
					}
				}
				else
				{
					this.DataGridView.OnAddedRow_PreNotification(rowIndex);
					if (!flag)
					{
						int num2 = this.GetRowsHeight(DataGridViewElementStates.Visible) - this.DataGridView.VerticalScrollingOffset - dataGridViewRow.GetHeight(rowIndex);
						dataGridViewRow = this.SharedRow(rowIndex);
						flag = (this.DataGridView.LayoutInfo.Data.Height < num2 && num <= this.DataGridView.LayoutInfo.Data.Height);
					}
				}
				break;
			}
			case CollectionChangeAction.Remove:
			{
				DataGridViewElementStates rowState = this.GetRowState(rowIndex);
				bool flag2 = (rowState & DataGridViewElementStates.Visible) > DataGridViewElementStates.None;
				bool flag3 = (rowState & DataGridViewElementStates.Frozen) > DataGridViewElementStates.None;
				this.rowStates.RemoveAt(rowIndex);
				this.SharedList.RemoveAt(rowIndex);
				this.DataGridView.OnRemovedRow_PreNotification(rowIndex);
				if (flag2)
				{
					if (flag3)
					{
						flag = (this.DataGridView.FirstDisplayedScrollingRowIndex == -1 && this.GetRowsHeightExceedLimit(DataGridViewElementStates.Visible, 0, rowIndex, this.DataGridView.LayoutInfo.Data.Height + SystemInformation.HorizontalScrollBarHeight));
					}
					else if (this.DataGridView.FirstDisplayedScrollingRowIndex != -1 && rowIndex > this.DataGridView.FirstDisplayedScrollingRowIndex)
					{
						int num3 = 0;
						int firstDisplayedRowIndex2 = this.DataGridView.FirstDisplayedRowIndex;
						if (firstDisplayedRowIndex2 != -1)
						{
							num3 = this.SharedRow(firstDisplayedRowIndex2).GetHeight(firstDisplayedRowIndex2);
						}
						flag = (this.GetRowsHeightExceedLimit(DataGridViewElementStates.Visible, 0, rowIndex, this.DataGridView.LayoutInfo.Data.Height + this.DataGridView.VerticalScrollingOffset + SystemInformation.HorizontalScrollBarHeight) && num3 <= this.DataGridView.LayoutInfo.Data.Height);
					}
				}
				else
				{
					flag = true;
				}
				break;
			}
			case CollectionChangeAction.Refresh:
				this.InvalidateCachedRowCounts();
				this.InvalidateCachedRowsHeights();
				break;
			}
			this.DataGridView.ResetUIState(flag, computeVisibleRows);
		}

		// Token: 0x06002267 RID: 8807 RVA: 0x000A49A4 File Offset: 0x000A2BA4
		private void OnCollectionChanged_PostNotification(CollectionChangeAction cca, int rowIndex, int rowCount, DataGridViewRow dataGridViewRow, bool changeIsDeletion, bool changeIsInsertion, bool recreateNewRow, Point newCurrentCell)
		{
			if (changeIsDeletion)
			{
				this.DataGridView.OnRowsRemovedInternal(rowIndex, rowCount);
			}
			else
			{
				this.DataGridView.OnRowsAddedInternal(rowIndex, rowCount);
			}
			switch (cca)
			{
			case CollectionChangeAction.Add:
				if (changeIsInsertion)
				{
					this.DataGridView.OnInsertedRow_PostNotification(rowIndex, newCurrentCell, true);
				}
				else
				{
					this.DataGridView.OnAddedRow_PostNotification(rowIndex);
				}
				break;
			case CollectionChangeAction.Remove:
				this.DataGridView.OnRemovedRow_PostNotification(dataGridViewRow, newCurrentCell);
				break;
			case CollectionChangeAction.Refresh:
				if (changeIsDeletion)
				{
					this.DataGridView.OnClearedRows();
				}
				break;
			}
			this.DataGridView.OnRowCollectionChanged_PostNotification(recreateNewRow, newCurrentCell.X == -1, cca, dataGridViewRow, rowIndex);
		}

		// Token: 0x06002268 RID: 8808 RVA: 0x000A4A48 File Offset: 0x000A2C48
		public virtual void Remove(DataGridViewRow dataGridViewRow)
		{
			if (dataGridViewRow == null)
			{
				throw new ArgumentNullException("dataGridViewRow");
			}
			if (dataGridViewRow.DataGridView != this.DataGridView)
			{
				throw new ArgumentException(SR.GetString("DataGridView_RowDoesNotBelongToDataGridView"), "dataGridViewRow");
			}
			if (dataGridViewRow.Index == -1)
			{
				throw new ArgumentException(SR.GetString("DataGridView_RowMustBeUnshared"), "dataGridViewRow");
			}
			this.RemoveAt(dataGridViewRow.Index);
		}

		// Token: 0x06002269 RID: 8809 RVA: 0x000A4AB0 File Offset: 0x000A2CB0
		public virtual void RemoveAt(int index)
		{
			if (index < 0 || index >= this.Count)
			{
				throw new ArgumentOutOfRangeException("index", SR.GetString("DataGridViewRowCollection_RowIndexOutOfRange"));
			}
			if (this.DataGridView.NewRowIndex == index)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewRowCollection_CannotDeleteNewRow"));
			}
			if (this.DataGridView.NoDimensionChangeAllowed)
			{
				throw new InvalidOperationException(SR.GetString("DataGridView_ForbiddenOperationInEventHandler"));
			}
			if (this.DataGridView.DataSource == null)
			{
				this.RemoveAtInternal(index, false);
				return;
			}
			IBindingList bindingList = this.DataGridView.DataConnection.List as IBindingList;
			if (bindingList != null && bindingList.AllowRemove && bindingList.SupportsChangeNotification)
			{
				bindingList.RemoveAt(index);
				return;
			}
			throw new InvalidOperationException(SR.GetString("DataGridViewRowCollection_CantRemoveRowsWithWrongSource"));
		}

		// Token: 0x0600226A RID: 8810 RVA: 0x000A4B70 File Offset: 0x000A2D70
		internal void RemoveAtInternal(int index, bool force)
		{
			DataGridViewRow dataGridViewRow = this.SharedRow(index);
			Point newCurrentCell = new Point(-1, -1);
			if (this.IsCollectionChangedListenedTo || dataGridViewRow.GetDisplayed(index))
			{
				dataGridViewRow = this[index];
			}
			dataGridViewRow = this.SharedRow(index);
			this.DataGridView.OnRemovingRow(index, out newCurrentCell, force);
			this.UpdateRowCaches(index, ref dataGridViewRow, false);
			if (dataGridViewRow.Index != -1)
			{
				this.rowStates[index] = dataGridViewRow.State;
				dataGridViewRow.DetachFromDataGridView();
			}
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Remove, dataGridViewRow), index, 1, true, false, false, newCurrentCell);
		}

		// Token: 0x0600226B RID: 8811 RVA: 0x000A4BFC File Offset: 0x000A2DFC
		private static bool RowHasValueOrToolTipText(DataGridViewRow dataGridViewRow)
		{
			DataGridViewCellCollection cells = dataGridViewRow.Cells;
			foreach (object obj in cells)
			{
				DataGridViewCell dataGridViewCell = (DataGridViewCell)obj;
				if (dataGridViewCell.HasValue || dataGridViewCell.HasToolTipText)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600226C RID: 8812 RVA: 0x000A4C6C File Offset: 0x000A2E6C
		internal bool RowIsSharable(int index)
		{
			DataGridViewRow dataGridViewRow = this.SharedRow(index);
			if (dataGridViewRow.Index != -1)
			{
				return false;
			}
			DataGridViewCellCollection cells = dataGridViewRow.Cells;
			foreach (object obj in cells)
			{
				DataGridViewCell dataGridViewCell = (DataGridViewCell)obj;
				if ((dataGridViewCell.State & ~(dataGridViewCell.CellStateFromColumnRowStates(this.rowStates[index]) != DataGridViewElementStates.None)) != DataGridViewElementStates.None)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600226D RID: 8813 RVA: 0x000A4CFC File Offset: 0x000A2EFC
		internal void SetRowState(int rowIndex, DataGridViewElementStates state, bool value)
		{
			DataGridViewRow dataGridViewRow = this.SharedRow(rowIndex);
			if (dataGridViewRow.Index == -1)
			{
				if ((this.rowStates[rowIndex] & state) > DataGridViewElementStates.None != value)
				{
					if (state == DataGridViewElementStates.Frozen || state == DataGridViewElementStates.Visible || state == DataGridViewElementStates.ReadOnly)
					{
						dataGridViewRow.OnSharedStateChanging(rowIndex, state);
					}
					if (value)
					{
						this.rowStates[rowIndex] = (this.rowStates[rowIndex] | state);
					}
					else
					{
						this.rowStates[rowIndex] = (this.rowStates[rowIndex] & ~state);
					}
					dataGridViewRow.OnSharedStateChanged(rowIndex, state);
					return;
				}
			}
			else if (state <= DataGridViewElementStates.Resizable)
			{
				switch (state)
				{
				case DataGridViewElementStates.Displayed:
					dataGridViewRow.DisplayedInternal = value;
					return;
				case DataGridViewElementStates.Frozen:
					dataGridViewRow.Frozen = value;
					return;
				case DataGridViewElementStates.Displayed | DataGridViewElementStates.Frozen:
					break;
				case DataGridViewElementStates.ReadOnly:
					dataGridViewRow.ReadOnlyInternal = value;
					return;
				default:
					if (state != DataGridViewElementStates.Resizable)
					{
						return;
					}
					dataGridViewRow.Resizable = (value ? DataGridViewTriState.True : DataGridViewTriState.False);
					break;
				}
			}
			else
			{
				if (state == DataGridViewElementStates.Selected)
				{
					dataGridViewRow.SelectedInternal = value;
					return;
				}
				if (state != DataGridViewElementStates.Visible)
				{
					return;
				}
				dataGridViewRow.Visible = value;
				return;
			}
		}

		// Token: 0x0600226E RID: 8814 RVA: 0x000A4DEA File Offset: 0x000A2FEA
		internal DataGridViewElementStates SharedRowState(int rowIndex)
		{
			return this.rowStates[rowIndex];
		}

		// Token: 0x0600226F RID: 8815 RVA: 0x000A4DF8 File Offset: 0x000A2FF8
		internal void Sort(IComparer customComparer, bool ascending)
		{
			if (this.items.Count > 0)
			{
				DataGridViewRowCollection.RowComparer rowComparer = new DataGridViewRowCollection.RowComparer(this, customComparer, ascending);
				this.items.CustomSort(rowComparer);
			}
		}

		// Token: 0x06002270 RID: 8816 RVA: 0x000A4E28 File Offset: 0x000A3028
		internal void SwapSortedRows(int rowIndex1, int rowIndex2)
		{
			this.DataGridView.SwapSortedRows(rowIndex1, rowIndex2);
			DataGridViewRow dataGridViewRow = this.SharedRow(rowIndex1);
			DataGridViewRow dataGridViewRow2 = this.SharedRow(rowIndex2);
			if (dataGridViewRow.Index != -1)
			{
				dataGridViewRow.IndexInternal = rowIndex2;
			}
			if (dataGridViewRow2.Index != -1)
			{
				dataGridViewRow2.IndexInternal = rowIndex1;
			}
			if (this.DataGridView.VirtualMode)
			{
				int count = this.DataGridView.Columns.Count;
				for (int i = 0; i < count; i++)
				{
					DataGridViewCell dataGridViewCell = dataGridViewRow.Cells[i];
					DataGridViewCell dataGridViewCell2 = dataGridViewRow2.Cells[i];
					object valueInternal = dataGridViewCell.GetValueInternal(rowIndex1);
					object valueInternal2 = dataGridViewCell2.GetValueInternal(rowIndex2);
					dataGridViewCell.SetValueInternal(rowIndex1, valueInternal2);
					dataGridViewCell2.SetValueInternal(rowIndex2, valueInternal);
				}
			}
			object value = this.items[rowIndex1];
			this.items[rowIndex1] = this.items[rowIndex2];
			this.items[rowIndex2] = value;
			DataGridViewElementStates value2 = this.rowStates[rowIndex1];
			this.rowStates[rowIndex1] = this.rowStates[rowIndex2];
			this.rowStates[rowIndex2] = value2;
		}

		// Token: 0x06002271 RID: 8817 RVA: 0x000A4F4E File Offset: 0x000A314E
		private void UnshareRow(int rowIndex)
		{
			this.SharedRow(rowIndex).IndexInternal = rowIndex;
			this.SharedRow(rowIndex).StateInternal = this.SharedRowState(rowIndex);
		}

		// Token: 0x06002272 RID: 8818 RVA: 0x000A4F70 File Offset: 0x000A3170
		private void UpdateRowCaches(int rowIndex, ref DataGridViewRow dataGridViewRow, bool adding)
		{
			if (this.rowCountsVisible != -1 || this.rowCountsVisibleFrozen != -1 || this.rowCountsVisibleSelected != -1 || this.rowsHeightVisible != -1 || this.rowsHeightVisibleFrozen != -1)
			{
				DataGridViewElementStates rowState = this.GetRowState(rowIndex);
				if ((rowState & DataGridViewElementStates.Visible) != DataGridViewElementStates.None)
				{
					int num = adding ? 1 : -1;
					int num2 = 0;
					if (this.rowsHeightVisible != -1 || (this.rowsHeightVisibleFrozen != -1 && (rowState & (DataGridViewElementStates.Frozen | DataGridViewElementStates.Visible)) == (DataGridViewElementStates.Frozen | DataGridViewElementStates.Visible)))
					{
						num2 = (adding ? dataGridViewRow.GetHeight(rowIndex) : (-dataGridViewRow.GetHeight(rowIndex)));
						dataGridViewRow = this.SharedRow(rowIndex);
					}
					if (this.rowCountsVisible != -1)
					{
						this.rowCountsVisible += num;
					}
					if (this.rowsHeightVisible != -1)
					{
						this.rowsHeightVisible += num2;
					}
					if ((rowState & (DataGridViewElementStates.Frozen | DataGridViewElementStates.Visible)) == (DataGridViewElementStates.Frozen | DataGridViewElementStates.Visible))
					{
						if (this.rowCountsVisibleFrozen != -1)
						{
							this.rowCountsVisibleFrozen += num;
						}
						if (this.rowsHeightVisibleFrozen != -1)
						{
							this.rowsHeightVisibleFrozen += num2;
						}
					}
					if ((rowState & (DataGridViewElementStates.Selected | DataGridViewElementStates.Visible)) == (DataGridViewElementStates.Selected | DataGridViewElementStates.Visible) && this.rowCountsVisibleSelected != -1)
					{
						this.rowCountsVisibleSelected += num;
					}
				}
			}
		}

		// Token: 0x04000E2C RID: 3628
		private CollectionChangeEventHandler onCollectionChanged;

		// Token: 0x04000E2D RID: 3629
		private DataGridViewRowCollection.RowArrayList items;

		// Token: 0x04000E2E RID: 3630
		private List<DataGridViewElementStates> rowStates;

		// Token: 0x04000E2F RID: 3631
		private int rowCountsVisible;

		// Token: 0x04000E30 RID: 3632
		private int rowCountsVisibleFrozen;

		// Token: 0x04000E31 RID: 3633
		private int rowCountsVisibleSelected;

		// Token: 0x04000E32 RID: 3634
		private int rowsHeightVisible;

		// Token: 0x04000E33 RID: 3635
		private int rowsHeightVisibleFrozen;

		// Token: 0x04000E34 RID: 3636
		private DataGridView dataGridView;

		// Token: 0x02000677 RID: 1655
		private class RowArrayList : ArrayList
		{
			// Token: 0x060066B5 RID: 26293 RVA: 0x0017FFD7 File Offset: 0x0017E1D7
			public RowArrayList(DataGridViewRowCollection owner)
			{
				this.owner = owner;
			}

			// Token: 0x060066B6 RID: 26294 RVA: 0x0017FFE6 File Offset: 0x0017E1E6
			public void CustomSort(DataGridViewRowCollection.RowComparer rowComparer)
			{
				this.rowComparer = rowComparer;
				this.CustomQuickSort(0, this.Count - 1);
			}

			// Token: 0x060066B7 RID: 26295 RVA: 0x00180000 File Offset: 0x0017E200
			private void CustomQuickSort(int left, int right)
			{
				while (right - left >= 2)
				{
					int num = left + right >> 1;
					object obj = this.Pivot(left, num, right);
					int num2 = left + 1;
					int num3 = right - 1;
					do
					{
						if (num != num2)
						{
							if (this.rowComparer.CompareObjects(this.rowComparer.GetComparedObject(num2), obj, num2, num) < 0)
							{
								num2++;
								continue;
							}
						}
						while (num != num3 && this.rowComparer.CompareObjects(obj, this.rowComparer.GetComparedObject(num3), num, num3) < 0)
						{
							num3--;
						}
						if (num2 > num3)
						{
							break;
						}
						if (num2 < num3)
						{
							this.owner.SwapSortedRows(num2, num3);
							if (num2 == num)
							{
								num = num3;
							}
							else if (num3 == num)
							{
								num = num2;
							}
						}
						num2++;
						num3--;
					}
					while (num2 <= num3);
					if (num3 - left <= right - num2)
					{
						if (left < num3)
						{
							this.CustomQuickSort(left, num3);
						}
						left = num2;
					}
					else
					{
						if (num2 < right)
						{
							this.CustomQuickSort(num2, right);
						}
						right = num3;
					}
					if (left >= right)
					{
						return;
					}
				}
				if (right - left > 0 && this.rowComparer.CompareObjects(this.rowComparer.GetComparedObject(left), this.rowComparer.GetComparedObject(right), left, right) > 0)
				{
					this.owner.SwapSortedRows(left, right);
				}
			}

			// Token: 0x060066B8 RID: 26296 RVA: 0x00180114 File Offset: 0x0017E314
			private object Pivot(int left, int center, int right)
			{
				if (this.rowComparer.CompareObjects(this.rowComparer.GetComparedObject(left), this.rowComparer.GetComparedObject(center), left, center) > 0)
				{
					this.owner.SwapSortedRows(left, center);
				}
				if (this.rowComparer.CompareObjects(this.rowComparer.GetComparedObject(left), this.rowComparer.GetComparedObject(right), left, right) > 0)
				{
					this.owner.SwapSortedRows(left, right);
				}
				if (this.rowComparer.CompareObjects(this.rowComparer.GetComparedObject(center), this.rowComparer.GetComparedObject(right), center, right) > 0)
				{
					this.owner.SwapSortedRows(center, right);
				}
				return this.rowComparer.GetComparedObject(center);
			}

			// Token: 0x04003A79 RID: 14969
			private DataGridViewRowCollection owner;

			// Token: 0x04003A7A RID: 14970
			private DataGridViewRowCollection.RowComparer rowComparer;
		}

		// Token: 0x02000678 RID: 1656
		private class RowComparer
		{
			// Token: 0x060066B9 RID: 26297 RVA: 0x001801CC File Offset: 0x0017E3CC
			public RowComparer(DataGridViewRowCollection dataGridViewRows, IComparer customComparer, bool ascending)
			{
				this.dataGridView = dataGridViewRows.DataGridView;
				this.dataGridViewRows = dataGridViewRows;
				this.dataGridViewSortedColumn = this.dataGridView.SortedColumn;
				if (this.dataGridViewSortedColumn == null)
				{
					this.sortedColumnIndex = -1;
				}
				else
				{
					this.sortedColumnIndex = this.dataGridViewSortedColumn.Index;
				}
				this.customComparer = customComparer;
				this.ascending = ascending;
			}

			// Token: 0x060066BA RID: 26298 RVA: 0x00180234 File Offset: 0x0017E434
			internal object GetComparedObject(int rowIndex)
			{
				if (this.dataGridView.NewRowIndex != -1 && rowIndex == this.dataGridView.NewRowIndex)
				{
					return DataGridViewRowCollection.RowComparer.max;
				}
				if (this.customComparer == null)
				{
					DataGridViewRow dataGridViewRow = this.dataGridViewRows.SharedRow(rowIndex);
					return dataGridViewRow.Cells[this.sortedColumnIndex].GetValueInternal(rowIndex);
				}
				return this.dataGridViewRows[rowIndex];
			}

			// Token: 0x060066BB RID: 26299 RVA: 0x0018029C File Offset: 0x0017E49C
			internal int CompareObjects(object value1, object value2, int rowIndex1, int rowIndex2)
			{
				if (value1 is DataGridViewRowCollection.RowComparer.ComparedObjectMax)
				{
					return 1;
				}
				if (value2 is DataGridViewRowCollection.RowComparer.ComparedObjectMax)
				{
					return -1;
				}
				int num = 0;
				if (this.customComparer == null)
				{
					if (!this.dataGridView.OnSortCompare(this.dataGridViewSortedColumn, value1, value2, rowIndex1, rowIndex2, out num))
					{
						if (!(value1 is IComparable) && !(value2 is IComparable))
						{
							if (value1 == null)
							{
								if (value2 == null)
								{
									num = 0;
								}
								else
								{
									num = 1;
								}
							}
							else if (value2 == null)
							{
								num = -1;
							}
							else
							{
								num = Comparer.Default.Compare(value1.ToString(), value2.ToString());
							}
						}
						else
						{
							num = Comparer.Default.Compare(value1, value2);
						}
						if (num == 0)
						{
							if (this.ascending)
							{
								num = rowIndex1 - rowIndex2;
							}
							else
							{
								num = rowIndex2 - rowIndex1;
							}
						}
					}
				}
				else
				{
					num = this.customComparer.Compare(value1, value2);
				}
				if (this.ascending)
				{
					return num;
				}
				return -num;
			}

			// Token: 0x04003A7B RID: 14971
			private DataGridView dataGridView;

			// Token: 0x04003A7C RID: 14972
			private DataGridViewRowCollection dataGridViewRows;

			// Token: 0x04003A7D RID: 14973
			private DataGridViewColumn dataGridViewSortedColumn;

			// Token: 0x04003A7E RID: 14974
			private int sortedColumnIndex;

			// Token: 0x04003A7F RID: 14975
			private IComparer customComparer;

			// Token: 0x04003A80 RID: 14976
			private bool ascending;

			// Token: 0x04003A81 RID: 14977
			private static DataGridViewRowCollection.RowComparer.ComparedObjectMax max = new DataGridViewRowCollection.RowComparer.ComparedObjectMax();

			// Token: 0x020008B9 RID: 2233
			private class ComparedObjectMax
			{
			}
		}

		// Token: 0x02000679 RID: 1657
		private class UnsharingRowEnumerator : IEnumerator
		{
			// Token: 0x060066BD RID: 26301 RVA: 0x0018036B File Offset: 0x0017E56B
			public UnsharingRowEnumerator(DataGridViewRowCollection owner)
			{
				this.owner = owner;
				this.current = -1;
			}

			// Token: 0x060066BE RID: 26302 RVA: 0x00180381 File Offset: 0x0017E581
			bool IEnumerator.MoveNext()
			{
				if (this.current < this.owner.Count - 1)
				{
					this.current++;
					return true;
				}
				this.current = this.owner.Count;
				return false;
			}

			// Token: 0x060066BF RID: 26303 RVA: 0x001803BA File Offset: 0x0017E5BA
			void IEnumerator.Reset()
			{
				this.current = -1;
			}

			// Token: 0x1700165C RID: 5724
			// (get) Token: 0x060066C0 RID: 26304 RVA: 0x001803C4 File Offset: 0x0017E5C4
			object IEnumerator.Current
			{
				get
				{
					if (this.current == -1)
					{
						throw new InvalidOperationException(SR.GetString("DataGridViewRowCollection_EnumNotStarted"));
					}
					if (this.current == this.owner.Count)
					{
						throw new InvalidOperationException(SR.GetString("DataGridViewRowCollection_EnumFinished"));
					}
					return this.owner[this.current];
				}
			}

			// Token: 0x04003A82 RID: 14978
			private DataGridViewRowCollection owner;

			// Token: 0x04003A83 RID: 14979
			private int current;
		}
	}
}
