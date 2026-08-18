using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web.Script.Serialization;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02001A01 RID: 6657
	public class RadRotatorItemCollection : StateManagedCollection
	{
		// Token: 0x060101C1 RID: 65985 RVA: 0x0039EDD7 File Offset: 0x0039CFD7
		public RadRotatorItemCollection(Control parent)
		{
			this._parent = parent;
		}

		// Token: 0x17004DC2 RID: 19906
		// (get) Token: 0x060101C2 RID: 65986 RVA: 0x0039EDE6 File Offset: 0x0039CFE6
		protected Control Parent
		{
			get
			{
				return this._parent;
			}
		}

		// Token: 0x17004DC3 RID: 19907
		public RadRotatorItem this[int index]
		{
			get
			{
				return (RadRotatorItem)this.List[index];
			}
			set
			{
				this.List[index] = value;
			}
		}

		// Token: 0x17004DC4 RID: 19908
		// (get) Token: 0x060101C5 RID: 65989 RVA: 0x0039EE10 File Offset: 0x0039D010
		protected IList List
		{
			get
			{
				return this;
			}
		}

		// Token: 0x060101C6 RID: 65990 RVA: 0x0039EE13 File Offset: 0x0039D013
		public void Add(RadRotatorItem item)
		{
			this.List.Add(item);
		}

		// Token: 0x060101C7 RID: 65991 RVA: 0x0039EE22 File Offset: 0x0039D022
		public bool Contains(RadRotatorItem item)
		{
			return this.List.Contains(item);
		}

		// Token: 0x060101C8 RID: 65992 RVA: 0x0039EE30 File Offset: 0x0039D030
		public void CopyTo(RadRotatorItem[] array, int index)
		{
			this.List.CopyTo(array, index);
		}

		// Token: 0x060101C9 RID: 65993 RVA: 0x0039EE40 File Offset: 0x0039D040
		public void AddRange(IEnumerable<RadRotatorItem> items)
		{
			foreach (RadRotatorItem item in items)
			{
				this.Add(item);
			}
		}

		// Token: 0x060101CA RID: 65994 RVA: 0x0039EE88 File Offset: 0x0039D088
		public int IndexOf(RadRotatorItem item)
		{
			return this.List.IndexOf(item);
		}

		// Token: 0x060101CB RID: 65995 RVA: 0x0039EE96 File Offset: 0x0039D096
		public void Insert(int index, RadRotatorItem item)
		{
			this.List.Insert(index, item);
		}

		// Token: 0x060101CC RID: 65996 RVA: 0x0039EEA8 File Offset: 0x0039D0A8
		protected override void OnInsertComplete(int index, object value)
		{
			RadRotatorItem radRotatorItem = (RadRotatorItem)value;
			this._parent.Controls.AddAt(index, radRotatorItem);
			if (index >= 0)
			{
				for (int i = index; i < base.Count; i++)
				{
					this[i].ID = "i" + i;
				}
			}
			else
			{
				radRotatorItem.ID = "i" + this.IndexOf(radRotatorItem);
			}
			if (this._itemContainer != null)
			{
				radRotatorItem.SetItemContainer(this._itemContainer);
			}
		}

		// Token: 0x060101CD RID: 65997 RVA: 0x0039EF34 File Offset: 0x0039D134
		protected override void OnClear()
		{
			foreach (object obj in this)
			{
				RadRotatorItem value = (RadRotatorItem)obj;
				this._parent.Controls.Remove(value);
			}
			base.OnClear();
		}

		// Token: 0x060101CE RID: 65998 RVA: 0x0039EF98 File Offset: 0x0039D198
		protected override void OnRemoveComplete(int index, object value)
		{
			Control control = value as Control;
			if (this._parent.Controls.Contains(control))
			{
				this._parent.Controls.Remove(control);
			}
		}

		// Token: 0x060101CF RID: 65999 RVA: 0x0039EFD0 File Offset: 0x0039D1D0
		public void Remove(RadRotatorItem item)
		{
			this.List.Remove(item);
		}

		// Token: 0x060101D0 RID: 66000 RVA: 0x0039EFDE File Offset: 0x0039D1DE
		public void RemoveAt(int index)
		{
			this.List.RemoveAt(index);
		}

		// Token: 0x060101D1 RID: 66001 RVA: 0x0039EFEC File Offset: 0x0039D1EC
		internal void SetItemContainer(RadRotator itemContainer)
		{
			this._itemContainer = itemContainer;
			foreach (object obj in this)
			{
				RadRotatorItem radRotatorItem = (RadRotatorItem)obj;
				radRotatorItem.SetItemContainer(itemContainer);
			}
		}

		// Token: 0x060101D2 RID: 66002 RVA: 0x0039F048 File Offset: 0x0039D248
		protected override void SetDirtyObject(object o)
		{
			((IMarkableStateManager)o).SetDirty();
		}

		// Token: 0x060101D3 RID: 66003 RVA: 0x0039F058 File Offset: 0x0039D258
		internal string Serialize()
		{
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			StringBuilder stringBuilder = new StringBuilder("[");
			if (base.Count > 0)
			{
				foreach (object obj in this)
				{
					RadRotatorItem radRotatorItem = (RadRotatorItem)obj;
					stringBuilder.Append("{");
					if (!string.IsNullOrEmpty(radRotatorItem.CssClass))
					{
						stringBuilder.Append(string.Format("\"cssClass\":{0}", javaScriptSerializer.Serialize(radRotatorItem.CssClass)));
						if (!radRotatorItem.Visible)
						{
							stringBuilder.Append(",");
						}
					}
					if (!radRotatorItem.Visible)
					{
						stringBuilder.Append("\"visible\":false");
					}
					stringBuilder.Append("},");
				}
				stringBuilder.Remove(stringBuilder.Length - 1, 1);
			}
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x040048F2 RID: 18674
		private readonly Control _parent;

		// Token: 0x040048F3 RID: 18675
		private RadRotator _itemContainer;
	}
}
