using System;
using System.Collections;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x020004F8 RID: 1272
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class DataGridColumnCollection : ICollection, IEnumerable, IStateManager
	{
		// Token: 0x06003E14 RID: 15892 RVA: 0x00103624 File Offset: 0x00102624
		public DataGridColumnCollection(DataGrid owner, ArrayList columns)
		{
			this.owner = owner;
			this.columns = columns;
		}

		// Token: 0x17000E9F RID: 3743
		// (get) Token: 0x06003E15 RID: 15893 RVA: 0x0010363A File Offset: 0x0010263A
		[Browsable(false)]
		public int Count
		{
			get
			{
				return this.columns.Count;
			}
		}

		// Token: 0x17000EA0 RID: 3744
		// (get) Token: 0x06003E16 RID: 15894 RVA: 0x00103647 File Offset: 0x00102647
		[Browsable(false)]
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000EA1 RID: 3745
		// (get) Token: 0x06003E17 RID: 15895 RVA: 0x0010364A File Offset: 0x0010264A
		[Browsable(false)]
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000EA2 RID: 3746
		// (get) Token: 0x06003E18 RID: 15896 RVA: 0x0010364D File Offset: 0x0010264D
		[Browsable(false)]
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000EA3 RID: 3747
		[Browsable(false)]
		public DataGridColumn this[int index]
		{
			get
			{
				return (DataGridColumn)this.columns[index];
			}
		}

		// Token: 0x06003E1A RID: 15898 RVA: 0x00103663 File Offset: 0x00102663
		public void Add(DataGridColumn column)
		{
			this.AddAt(-1, column);
		}

		// Token: 0x06003E1B RID: 15899 RVA: 0x00103670 File Offset: 0x00102670
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

		// Token: 0x06003E1C RID: 15900 RVA: 0x001036CB File Offset: 0x001026CB
		public void Clear()
		{
			this.columns.Clear();
			this.OnColumnsChanged();
		}

		// Token: 0x06003E1D RID: 15901 RVA: 0x001036E0 File Offset: 0x001026E0
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

		// Token: 0x06003E1E RID: 15902 RVA: 0x0010371E File Offset: 0x0010271E
		public IEnumerator GetEnumerator()
		{
			return this.columns.GetEnumerator();
		}

		// Token: 0x06003E1F RID: 15903 RVA: 0x0010372B File Offset: 0x0010272B
		public int IndexOf(DataGridColumn column)
		{
			if (column != null)
			{
				return this.columns.IndexOf(column);
			}
			return -1;
		}

		// Token: 0x06003E20 RID: 15904 RVA: 0x0010373E File Offset: 0x0010273E
		private void OnColumnsChanged()
		{
			if (this.owner != null)
			{
				this.owner.OnColumnsChanged();
			}
		}

		// Token: 0x06003E21 RID: 15905 RVA: 0x00103753 File Offset: 0x00102753
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

		// Token: 0x06003E22 RID: 15906 RVA: 0x00103780 File Offset: 0x00102780
		public void Remove(DataGridColumn column)
		{
			int num = this.IndexOf(column);
			if (num >= 0)
			{
				this.RemoveAt(num);
			}
		}

		// Token: 0x17000EA4 RID: 3748
		// (get) Token: 0x06003E23 RID: 15907 RVA: 0x001037A0 File Offset: 0x001027A0
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this.marked;
			}
		}

		// Token: 0x06003E24 RID: 15908 RVA: 0x001037A8 File Offset: 0x001027A8
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

		// Token: 0x06003E25 RID: 15909 RVA: 0x001037FC File Offset: 0x001027FC
		void IStateManager.TrackViewState()
		{
			this.marked = true;
			int count = this.columns.Count;
			for (int i = 0; i < count; i++)
			{
				((IStateManager)this.columns[i]).TrackViewState();
			}
		}

		// Token: 0x06003E26 RID: 15910 RVA: 0x00103840 File Offset: 0x00102840
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

		// Token: 0x04002791 RID: 10129
		private DataGrid owner;

		// Token: 0x04002792 RID: 10130
		private ArrayList columns;

		// Token: 0x04002793 RID: 10131
		private bool marked;
	}
}
