using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	// Token: 0x02000399 RID: 921
	[Editor("System.Windows.Forms.Design.StyleCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	public abstract class TableLayoutStyleCollection : IList, ICollection, IEnumerable
	{
		// Token: 0x06003C2E RID: 15406 RVA: 0x00106EB8 File Offset: 0x001050B8
		internal TableLayoutStyleCollection(IArrangedElement owner)
		{
			this._owner = owner;
		}

		// Token: 0x17000EA5 RID: 3749
		// (get) Token: 0x06003C2F RID: 15407 RVA: 0x00106ED2 File Offset: 0x001050D2
		internal IArrangedElement Owner
		{
			get
			{
				return this._owner;
			}
		}

		// Token: 0x17000EA6 RID: 3750
		// (get) Token: 0x06003C30 RID: 15408 RVA: 0x00015ECC File Offset: 0x000140CC
		internal virtual string PropertyName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06003C31 RID: 15409 RVA: 0x00106EDC File Offset: 0x001050DC
		int IList.Add(object style)
		{
			this.EnsureNotOwned((TableLayoutStyle)style);
			((TableLayoutStyle)style).Owner = this.Owner;
			int result = this._innerList.Add(style);
			this.PerformLayoutIfOwned();
			return result;
		}

		// Token: 0x06003C32 RID: 15410 RVA: 0x00106F1A File Offset: 0x0010511A
		public int Add(TableLayoutStyle style)
		{
			return ((IList)this).Add(style);
		}

		// Token: 0x06003C33 RID: 15411 RVA: 0x00106F23 File Offset: 0x00105123
		void IList.Insert(int index, object style)
		{
			this.EnsureNotOwned((TableLayoutStyle)style);
			((TableLayoutStyle)style).Owner = this.Owner;
			this._innerList.Insert(index, style);
			this.PerformLayoutIfOwned();
		}

		// Token: 0x17000EA7 RID: 3751
		object IList.this[int index]
		{
			get
			{
				return this._innerList[index];
			}
			set
			{
				TableLayoutStyle tableLayoutStyle = (TableLayoutStyle)value;
				this.EnsureNotOwned(tableLayoutStyle);
				tableLayoutStyle.Owner = this.Owner;
				this._innerList[index] = tableLayoutStyle;
				this.PerformLayoutIfOwned();
			}
		}

		// Token: 0x17000EA8 RID: 3752
		public TableLayoutStyle this[int index]
		{
			get
			{
				return (TableLayoutStyle)((IList)this)[index];
			}
			set
			{
				((IList)this)[index] = value;
			}
		}

		// Token: 0x06003C38 RID: 15416 RVA: 0x00106FB6 File Offset: 0x001051B6
		void IList.Remove(object style)
		{
			((TableLayoutStyle)style).Owner = null;
			this._innerList.Remove(style);
			this.PerformLayoutIfOwned();
		}

		// Token: 0x06003C39 RID: 15417 RVA: 0x00106FD8 File Offset: 0x001051D8
		public void Clear()
		{
			foreach (object obj in this._innerList)
			{
				TableLayoutStyle tableLayoutStyle = (TableLayoutStyle)obj;
				tableLayoutStyle.Owner = null;
			}
			this._innerList.Clear();
			this.PerformLayoutIfOwned();
		}

		// Token: 0x06003C3A RID: 15418 RVA: 0x00107044 File Offset: 0x00105244
		public void RemoveAt(int index)
		{
			TableLayoutStyle tableLayoutStyle = (TableLayoutStyle)this._innerList[index];
			tableLayoutStyle.Owner = null;
			this._innerList.RemoveAt(index);
			this.PerformLayoutIfOwned();
		}

		// Token: 0x06003C3B RID: 15419 RVA: 0x0010707C File Offset: 0x0010527C
		bool IList.Contains(object style)
		{
			return this._innerList.Contains(style);
		}

		// Token: 0x06003C3C RID: 15420 RVA: 0x0010708A File Offset: 0x0010528A
		int IList.IndexOf(object style)
		{
			return this._innerList.IndexOf(style);
		}

		// Token: 0x17000EA9 RID: 3753
		// (get) Token: 0x06003C3D RID: 15421 RVA: 0x00107098 File Offset: 0x00105298
		bool IList.IsFixedSize
		{
			get
			{
				return this._innerList.IsFixedSize;
			}
		}

		// Token: 0x17000EAA RID: 3754
		// (get) Token: 0x06003C3E RID: 15422 RVA: 0x001070A5 File Offset: 0x001052A5
		bool IList.IsReadOnly
		{
			get
			{
				return this._innerList.IsReadOnly;
			}
		}

		// Token: 0x06003C3F RID: 15423 RVA: 0x001070B2 File Offset: 0x001052B2
		void ICollection.CopyTo(Array array, int startIndex)
		{
			this._innerList.CopyTo(array, startIndex);
		}

		// Token: 0x17000EAB RID: 3755
		// (get) Token: 0x06003C40 RID: 15424 RVA: 0x001070C1 File Offset: 0x001052C1
		public int Count
		{
			get
			{
				return this._innerList.Count;
			}
		}

		// Token: 0x17000EAC RID: 3756
		// (get) Token: 0x06003C41 RID: 15425 RVA: 0x001070CE File Offset: 0x001052CE
		bool ICollection.IsSynchronized
		{
			get
			{
				return this._innerList.IsSynchronized;
			}
		}

		// Token: 0x17000EAD RID: 3757
		// (get) Token: 0x06003C42 RID: 15426 RVA: 0x001070DB File Offset: 0x001052DB
		object ICollection.SyncRoot
		{
			get
			{
				return this._innerList.SyncRoot;
			}
		}

		// Token: 0x06003C43 RID: 15427 RVA: 0x001070E8 File Offset: 0x001052E8
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._innerList.GetEnumerator();
		}

		// Token: 0x06003C44 RID: 15428 RVA: 0x001070F5 File Offset: 0x001052F5
		private void EnsureNotOwned(TableLayoutStyle style)
		{
			if (style.Owner != null)
			{
				throw new ArgumentException(SR.GetString("OnlyOneControl", new object[]
				{
					style.GetType().Name
				}), "style");
			}
		}

		// Token: 0x06003C45 RID: 15429 RVA: 0x00107128 File Offset: 0x00105328
		internal void EnsureOwnership(IArrangedElement owner)
		{
			this._owner = owner;
			for (int i = 0; i < this.Count; i++)
			{
				this[i].Owner = owner;
			}
		}

		// Token: 0x06003C46 RID: 15430 RVA: 0x0010715A File Offset: 0x0010535A
		private void PerformLayoutIfOwned()
		{
			if (this.Owner != null)
			{
				LayoutTransaction.DoLayout(this.Owner, this.Owner, this.PropertyName);
			}
		}

		// Token: 0x0400239F RID: 9119
		private IArrangedElement _owner;

		// Token: 0x040023A0 RID: 9120
		private ArrayList _innerList = new ArrayList();
	}
}
