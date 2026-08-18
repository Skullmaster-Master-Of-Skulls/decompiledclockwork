using System;
using System.Collections;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02001107 RID: 4359
	public class GridGroupByFieldList : IList, ICollection, IEnumerable
	{
		// Token: 0x14000181 RID: 385
		// (add) Token: 0x0600B24A RID: 45642 RVA: 0x0026D1CC File Offset: 0x0026B3CC
		// (remove) Token: 0x0600B24B RID: 45643 RVA: 0x0026D204 File Offset: 0x0026B404
		internal event GridGroupByFieldList.ValidateFieldDelegate ValidateField;

		// Token: 0x0600B24C RID: 45644 RVA: 0x0026D239 File Offset: 0x0026B439
		internal void OnValidateField(GridGroupByField newField)
		{
			if (this.ValidateField != null)
			{
				this.ValidateField(this, new ValidateFieldEventArgs(newField));
			}
		}

		// Token: 0x0600B24D RID: 45645 RVA: 0x0026D255 File Offset: 0x0026B455
		public GridGroupByFieldList()
		{
			this._list = new ArrayList();
		}

		// Token: 0x0600B24E RID: 45646 RVA: 0x0026D268 File Offset: 0x0026B468
		private void CheckAll(IEnumerable value)
		{
			foreach (object value2 in value)
			{
				this.CheckValue(value2);
			}
		}

		// Token: 0x0600B24F RID: 45647 RVA: 0x0026D2B8 File Offset: 0x0026B4B8
		private void CheckValue(object value)
		{
			GridGroupByField gridGroupByField = value as GridGroupByField;
			if (gridGroupByField == null)
			{
				throw new GridGroupByException("Field list should contain only Field objects");
			}
			gridGroupByField.Validate();
			foreach (object obj in this)
			{
				GridGroupByField gridGroupByField2 = (GridGroupByField)obj;
				if (gridGroupByField2.FieldAlias == gridGroupByField.FieldAlias)
				{
					throw new GridGroupByException("Duplicated field definition");
				}
			}
			this.OnValidateField(gridGroupByField);
		}

		// Token: 0x0600B250 RID: 45648 RVA: 0x0026D348 File Offset: 0x0026B548
		public GridGroupByField FindByName(string FieldName)
		{
			GridGroupByField result = null;
			foreach (object obj in this)
			{
				GridGroupByField gridGroupByField = (GridGroupByField)obj;
				if (FieldName.ToUpper() == gridGroupByField.FieldName.ToUpper())
				{
					result = gridGroupByField;
					break;
				}
			}
			return result;
		}

		// Token: 0x0600B251 RID: 45649 RVA: 0x0026D3B4 File Offset: 0x0026B5B4
		public int Add(GridGroupByField value)
		{
			this.CheckValue(value);
			return this._list.Add(value);
		}

		// Token: 0x0600B252 RID: 45650 RVA: 0x0026D3C9 File Offset: 0x0026B5C9
		public void Insert(int index, GridGroupByField value)
		{
			this.CheckValue(value);
			this._list.Insert(index, value);
		}

		// Token: 0x0600B253 RID: 45651 RVA: 0x0026D3DF File Offset: 0x0026B5DF
		public void AddRange(GridGroupByFieldList c)
		{
			this.CheckAll(c);
			this._list.AddRange(c);
		}

		// Token: 0x0600B254 RID: 45652 RVA: 0x0026D3F4 File Offset: 0x0026B5F4
		public void InsertRange(int index, GridGroupByFieldList c)
		{
			this.CheckAll(c);
			this._list.InsertRange(index, c);
		}

		// Token: 0x170039BE RID: 14782
		public GridGroupByField this[int index]
		{
			get
			{
				return (GridGroupByField)this._list[index];
			}
			set
			{
				this._list[index] = value;
			}
		}

		// Token: 0x0600B257 RID: 45655 RVA: 0x0026D42C File Offset: 0x0026B62C
		int IList.Add(object value)
		{
			this.CheckValue(value);
			return this._list.Add(value);
		}

		// Token: 0x0600B258 RID: 45656 RVA: 0x0026D441 File Offset: 0x0026B641
		public bool Contains(object value)
		{
			return this._list.Contains(value);
		}

		// Token: 0x0600B259 RID: 45657 RVA: 0x0026D44F File Offset: 0x0026B64F
		public void Clear()
		{
			this._list.Clear();
		}

		// Token: 0x0600B25A RID: 45658 RVA: 0x0026D45C File Offset: 0x0026B65C
		public int IndexOf(object value)
		{
			return this._list.IndexOf(value);
		}

		// Token: 0x0600B25B RID: 45659 RVA: 0x0026D46A File Offset: 0x0026B66A
		void IList.Insert(int index, object value)
		{
			this.CheckValue(value);
			this._list.Insert(index, value);
		}

		// Token: 0x0600B25C RID: 45660 RVA: 0x0026D480 File Offset: 0x0026B680
		void IList.Remove(object value)
		{
			this._list.Remove(value);
		}

		// Token: 0x0600B25D RID: 45661 RVA: 0x0026D48E File Offset: 0x0026B68E
		public void RemoveAt(int index)
		{
			this._list.RemoveAt(index);
		}

		// Token: 0x170039BF RID: 14783
		// (get) Token: 0x0600B25E RID: 45662 RVA: 0x0026D49C File Offset: 0x0026B69C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public bool IsReadOnly
		{
			get
			{
				return this._list.IsReadOnly;
			}
		}

		// Token: 0x170039C0 RID: 14784
		// (get) Token: 0x0600B25F RID: 45663 RVA: 0x0026D4A9 File Offset: 0x0026B6A9
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool IsFixedSize
		{
			get
			{
				return this._list.IsFixedSize;
			}
		}

		// Token: 0x170039C1 RID: 14785
		object IList.this[int index]
		{
			get
			{
				return this._list[index];
			}
			set
			{
				this._list[index] = value;
			}
		}

		// Token: 0x0600B262 RID: 45666 RVA: 0x0026D4D3 File Offset: 0x0026B6D3
		public void CopyTo(Array array, int index)
		{
			this._list.CopyTo(array, index);
		}

		// Token: 0x170039C2 RID: 14786
		// (get) Token: 0x0600B263 RID: 45667 RVA: 0x0026D4E2 File Offset: 0x0026B6E2
		public int Count
		{
			get
			{
				return this._list.Count;
			}
		}

		// Token: 0x170039C3 RID: 14787
		// (get) Token: 0x0600B264 RID: 45668 RVA: 0x0026D4EF File Offset: 0x0026B6EF
		public object SyncRoot
		{
			get
			{
				return this._list.SyncRoot;
			}
		}

		// Token: 0x170039C4 RID: 14788
		// (get) Token: 0x0600B265 RID: 45669 RVA: 0x0026D4FC File Offset: 0x0026B6FC
		public bool IsSynchronized
		{
			get
			{
				return this._list.IsSynchronized;
			}
		}

		// Token: 0x0600B266 RID: 45670 RVA: 0x0026D509 File Offset: 0x0026B709
		public IEnumerator GetEnumerator()
		{
			return this._list.GetEnumerator();
		}

		// Token: 0x04002EFC RID: 12028
		private ArrayList _list;

		// Token: 0x02001108 RID: 4360
		// (Invoke) Token: 0x0600B268 RID: 45672
		internal delegate void ValidateFieldDelegate(object sender, ValidateFieldEventArgs e);
	}
}
