using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;

namespace Telerik.Web.UI.PageLayout
{
	// Token: 0x0200063C RID: 1596
	public abstract class BaseContainerCollection<T> : ControlCollectionBase, IList<T>, ICollection<T>, IEnumerable<!0>, IEnumerable where T : BaseContainer
	{
		// Token: 0x06003A32 RID: 14898 RVA: 0x000BE44C File Offset: 0x000BC64C
		public BaseContainerCollection(Control parent) : base(parent)
		{
		}

		// Token: 0x06003A33 RID: 14899 RVA: 0x000BE458 File Offset: 0x000BC658
		protected override void OnInsertComplete(int index, object value)
		{
			BaseContainer baseContainer = (BaseContainer)value;
			if (this._owner != null)
			{
				baseContainer.SetOwner(this._owner);
			}
			base.OnInsertComplete(index, value);
		}

		// Token: 0x06003A34 RID: 14900 RVA: 0x000BE488 File Offset: 0x000BC688
		internal void SetOwner(RadPageLayout owner)
		{
			this._owner = owner;
			foreach (object obj in base.List)
			{
				BaseContainer baseContainer = (BaseContainer)obj;
				baseContainer.SetOwner(owner);
			}
		}

		// Token: 0x06003A35 RID: 14901 RVA: 0x000BE4E8 File Offset: 0x000BC6E8
		public virtual void Add(T child)
		{
			base.List.Add(child);
		}

		// Token: 0x06003A36 RID: 14902 RVA: 0x000BE4FC File Offset: 0x000BC6FC
		public int IndexOf(T item)
		{
			return base.List.IndexOf(item);
		}

		// Token: 0x06003A37 RID: 14903 RVA: 0x000BE50F File Offset: 0x000BC70F
		public void Insert(int index, T item)
		{
			base.List.Insert(index, item);
		}

		// Token: 0x06003A38 RID: 14904 RVA: 0x000BE523 File Offset: 0x000BC723
		public void RemoveAt(int index)
		{
			base.List.RemoveAt(index);
		}

		// Token: 0x17001324 RID: 4900
		public T this[int index]
		{
			get
			{
				return (T)((object)base.List[index]);
			}
			set
			{
				base.List[index] = value;
			}
		}

		// Token: 0x06003A3B RID: 14907 RVA: 0x000BE558 File Offset: 0x000BC758
		public bool Contains(T item)
		{
			return base.List.Contains(item);
		}

		// Token: 0x06003A3C RID: 14908 RVA: 0x000BE56B File Offset: 0x000BC76B
		public void CopyTo(T[] array, int arrayIndex)
		{
			base.List.CopyTo(array, arrayIndex);
		}

		// Token: 0x17001325 RID: 4901
		// (get) Token: 0x06003A3D RID: 14909 RVA: 0x000BE57A File Offset: 0x000BC77A
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06003A3E RID: 14910 RVA: 0x000BE57D File Offset: 0x000BC77D
		public bool Remove(T item)
		{
			base.List.Remove(item);
			return true;
		}

		// Token: 0x06003A3F RID: 14911 RVA: 0x000BE6C4 File Offset: 0x000BC8C4
		public new IEnumerator<T> GetEnumerator()
		{
			foreach (T control in this)
			{
				yield return control;
			}
			yield break;
		}

		// Token: 0x06003A40 RID: 14912 RVA: 0x000BE6E0 File Offset: 0x000BC8E0
		void ICollection<!0>.Clear()
		{
			base.Clear();
		}

		// Token: 0x06003A41 RID: 14913 RVA: 0x000BE6E8 File Offset: 0x000BC8E8
		int ICollection<!0>.get_Count()
		{
			return base.Count;
		}

		// Token: 0x04000F8F RID: 3983
		private RadPageLayout _owner;
	}
}
