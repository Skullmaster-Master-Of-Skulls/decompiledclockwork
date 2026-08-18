using System;
using System.Collections;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000397 RID: 919
	public sealed class DataGridColumnCollection : ICollection, IEnumerable, IStateManager
	{
		// Token: 0x06002BE3 RID: 11235 RVA: 0x0008F42C File Offset: 0x0008D62C
		public DataGridColumnCollection(DataGrid owner, ArrayList columns)
		{
			this.owner = owner;
			this.columns = columns;
		}

		// Token: 0x17000C6B RID: 3179
		// (get) Token: 0x06002BE4 RID: 11236 RVA: 0x0008F442 File Offset: 0x0008D642
		[Browsable(false)]
		public int Count
		{
			get
			{
				return this.columns.Count;
			}
		}

		// Token: 0x17000C6C RID: 3180
		// (get) Token: 0x06002BE5 RID: 11237 RVA: 0x00007722 File Offset: 0x00005922
		[Browsable(false)]
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C6D RID: 3181
		// (get) Token: 0x06002BE6 RID: 11238 RVA: 0x00007722 File Offset: 0x00005922
		[Browsable(false)]
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C6E RID: 3182
		// (get) Token: 0x06002BE7 RID: 11239 RVA: 0x00004335 File Offset: 0x00002535
		[Browsable(false)]
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000C6F RID: 3183
		[Browsable(false)]
		public DataGridColumn this[int index]
		{
			get
			{
				return (DataGridColumn)this.columns[index];
			}
		}

		// Token: 0x06002BE9 RID: 11241 RVA: 0x0008F462 File Offset: 0x0008D662
		public void Add(DataGridColumn column)
		{
			this.AddAt(-1, column);
		}

		// Token: 0x06002BEA RID: 11242 RVA: 0x0008F46C File Offset: 0x0008D66C
		public void AddAt(int index, DataGridColumn column)
		{
			if (column == null)
			{
				throw new ArgumentNullException("column");
			}
			if (index == -1)
			{
				this.columns.Add(column);
			}
			else
			{
				this.columns.Insert(index, column);
			}
			column.SetOwner(this.owner);
			if (this.marked)
			{
				((IStateManager)column).TrackViewState();
			}
			this.OnColumnsChanged();
		}

		// Token: 0x06002BEB RID: 11243 RVA: 0x0008F4C7 File Offset: 0x0008D6C7
		public void Clear()
		{
			this.columns.Clear();
			this.OnColumnsChanged();
		}

		// Token: 0x06002BEC RID: 11244 RVA: 0x0008F4DC File Offset: 0x0008D6DC
		public void CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			foreach (object value in this)
			{
				array.SetValue(value, index++);
			}
		}

		// Token: 0x06002BED RID: 11245 RVA: 0x0008F51A File Offset: 0x0008D71A
		public IEnumerator GetEnumerator()
		{
			return this.columns.GetEnumerator();
		}

		// Token: 0x06002BEE RID: 11246 RVA: 0x0008F527 File Offset: 0x0008D727
		public int IndexOf(DataGridColumn column)
		{
			if (column != null)
			{
				return this.columns.IndexOf(column);
			}
			return -1;
		}

		// Token: 0x06002BEF RID: 11247 RVA: 0x0008F53A File Offset: 0x0008D73A
		private void OnColumnsChanged()
		{
			if (this.owner != null)
			{
				this.owner.OnColumnsChanged();
			}
		}

		// Token: 0x06002BF0 RID: 11248 RVA: 0x0008F54F File Offset: 0x0008D74F
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

		// Token: 0x06002BF1 RID: 11249 RVA: 0x0008F57C File Offset: 0x0008D77C
		public void Remove(DataGridColumn column)
		{
			int num = this.IndexOf(column);
			if (num >= 0)
			{
				this.RemoveAt(num);
			}
		}

		// Token: 0x17000C70 RID: 3184
		// (get) Token: 0x06002BF2 RID: 11250 RVA: 0x0008F59C File Offset: 0x0008D79C
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this.marked;
			}
		}

		// Token: 0x06002BF3 RID: 11251 RVA: 0x0008F5A4 File Offset: 0x0008D7A4
		void IStateManager.LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				object[] array = (object[])savedState;
				if (array.Length == this.columns.Count)
				{
					for (int i = 0; i < array.Length; i++)
					{
						if (array[i] != null)
						{
							((IStateManager)this.columns[i]).LoadViewState(array[i]);
						}
					}
				}
			}
		}

		// Token: 0x06002BF4 RID: 11252 RVA: 0x0008F5F8 File Offset: 0x0008D7F8
		void IStateManager.TrackViewState()
		{
			this.marked = true;
			int count = this.columns.Count;
			for (int i = 0; i < count; i++)
			{
				((IStateManager)this.columns[i]).TrackViewState();
			}
		}

		// Token: 0x06002BF5 RID: 11253 RVA: 0x0008F63C File Offset: 0x0008D83C
		object IStateManager.SaveViewState()
		{
			int count = this.columns.Count;
			object[] array = new object[count];
			bool flag = false;
			for (int i = 0; i < count; i++)
			{
				array[i] = ((IStateManager)this.columns[i]).SaveViewState();
				if (array[i] != null)
				{
					flag = true;
				}
			}
			if (!flag)
			{
				return null;
			}
			return array;
		}

		// Token: 0x04001F27 RID: 7975
		private DataGrid owner;

		// Token: 0x04001F28 RID: 7976
		private ArrayList columns;

		// Token: 0x04001F29 RID: 7977
		private bool marked;
	}
}
