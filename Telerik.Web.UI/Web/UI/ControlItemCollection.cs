using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000B15 RID: 2837
	public abstract class ControlItemCollection : StateManagedCollection
	{
		// Token: 0x170022B9 RID: 8889
		// (get) Token: 0x060069FC RID: 27132 RVA: 0x0018DFDF File Offset: 0x0018C1DF
		// (set) Token: 0x060069FD RID: 27133 RVA: 0x0018DFE7 File Offset: 0x0018C1E7
		internal int ControlsCount
		{
			get
			{
				return this._controlsCount;
			}
			set
			{
				this._controlsCount = value;
			}
		}

		// Token: 0x170022BA RID: 8890
		// (get) Token: 0x060069FE RID: 27134 RVA: 0x0018DFF0 File Offset: 0x0018C1F0
		protected Control Parent
		{
			get
			{
				return this._parent;
			}
		}

		// Token: 0x170022BB RID: 8891
		protected internal virtual ControlItem this[int index]
		{
			[DebuggerStepThrough]
			get
			{
				return (ControlItem)this.List[index];
			}
			set
			{
				this.List[index] = value;
			}
		}

		// Token: 0x170022BC RID: 8892
		// (get) Token: 0x06006A01 RID: 27137 RVA: 0x0018E01A File Offset: 0x0018C21A
		protected internal IList<ControlItem> VisibleItems
		{
			get
			{
				return this.VisibleChildren<ControlItem>();
			}
		}

		// Token: 0x06006A02 RID: 27138 RVA: 0x0018E024 File Offset: 0x0018C224
		protected internal IList<T> VisibleChildren<T>() where T : ControlItem
		{
			List<T> list = new List<T>();
			foreach (object obj in this)
			{
				T item = (T)((object)obj);
				if (item.Visible)
				{
					list.Add(item);
				}
			}
			return list.AsReadOnly();
		}

		// Token: 0x170022BD RID: 8893
		// (get) Token: 0x06006A03 RID: 27139 RVA: 0x0018E094 File Offset: 0x0018C294
		protected IList List
		{
			[DebuggerStepThrough]
			get
			{
				return this;
			}
		}

		// Token: 0x06006A04 RID: 27140 RVA: 0x0018E097 File Offset: 0x0018C297
		protected internal virtual void Add(ControlItem item)
		{
			this.List.Add(item);
		}

		// Token: 0x06006A05 RID: 27141 RVA: 0x0018E0A6 File Offset: 0x0018C2A6
		protected internal virtual bool Contains(ControlItem item)
		{
			return this.List.Contains(item);
		}

		// Token: 0x06006A06 RID: 27142 RVA: 0x0018E0B4 File Offset: 0x0018C2B4
		protected internal virtual void CopyTo(ControlItem[] array, int index)
		{
			this.List.CopyTo(array, index);
		}

		// Token: 0x06006A07 RID: 27143 RVA: 0x0018E0C4 File Offset: 0x0018C2C4
		protected internal virtual void AddRange(IEnumerable<ControlItem> items)
		{
			foreach (ControlItem item in items)
			{
				this.Add(item);
			}
		}

		// Token: 0x06006A08 RID: 27144 RVA: 0x0018E10C File Offset: 0x0018C30C
		protected internal virtual int IndexOf(ControlItem item)
		{
			return this.List.IndexOf(item);
		}

		// Token: 0x06006A09 RID: 27145 RVA: 0x0018E11A File Offset: 0x0018C31A
		protected internal virtual void Insert(int index, ControlItem item)
		{
			this.List.Insert(index, item);
		}

		// Token: 0x06006A0A RID: 27146 RVA: 0x0018E129 File Offset: 0x0018C329
		protected internal virtual void Remove(ControlItem item)
		{
			this.List.Remove(item);
		}

		// Token: 0x06006A0B RID: 27147 RVA: 0x0018E137 File Offset: 0x0018C337
		protected internal virtual void RemoveAt(int index)
		{
			this.List.RemoveAt(index);
		}

		// Token: 0x06006A0C RID: 27148 RVA: 0x0018E148 File Offset: 0x0018C348
		protected internal TControlItem FindChild<TControlItem>(Predicate<TControlItem> match) where TControlItem : ControlItem
		{
			if (match == null)
			{
				throw new ArgumentNullException("match");
			}
			foreach (object obj in this)
			{
				TControlItem tcontrolItem = (TControlItem)((object)obj);
				if (match(tcontrolItem))
				{
					return tcontrolItem;
				}
			}
			return default(TControlItem);
		}

		// Token: 0x06006A0D RID: 27149 RVA: 0x0018E1BC File Offset: 0x0018C3BC
		protected internal TControlItem FindChildByText<TControlItem>(string text) where TControlItem : ControlItem
		{
			return this.FindChildByText<TControlItem>(text, false);
		}

		// Token: 0x06006A0E RID: 27150 RVA: 0x0018E1C6 File Offset: 0x0018C3C6
		protected internal TControlItem FindChildByValue<TControlItem>(string value) where TControlItem : ControlItem
		{
			return this.FindChildByValue<TControlItem>(value, false);
		}

		// Token: 0x06006A0F RID: 27151 RVA: 0x0018E1D0 File Offset: 0x0018C3D0
		protected internal TControlItem FindChildByAttribute<TControlItem>(string attributeName, string attributeValue) where TControlItem : ControlItem
		{
			foreach (object obj in this)
			{
				TControlItem result = (TControlItem)((object)obj);
				if (result.Attributes[attributeName] == attributeValue)
				{
					return result;
				}
			}
			return default(TControlItem);
		}

		// Token: 0x06006A10 RID: 27152 RVA: 0x0018E248 File Offset: 0x0018C448
		public TControlItem FindChildByValue<TControlItem>(string value, bool ignoreCase) where TControlItem : ControlItem
		{
			foreach (object obj in this)
			{
				TControlItem result = (TControlItem)((object)obj);
				if (string.Compare(result.Value, value, ignoreCase) == 0)
				{
					return result;
				}
			}
			return default(TControlItem);
		}

		// Token: 0x06006A11 RID: 27153 RVA: 0x0018E2BC File Offset: 0x0018C4BC
		protected internal TControlItem FindChildByText<TControlItem>(string text, bool ignoreCase) where TControlItem : ControlItem
		{
			foreach (object obj in this)
			{
				TControlItem result = (TControlItem)((object)obj);
				if (string.Compare(result.Text, text, ignoreCase) == 0)
				{
					return result;
				}
			}
			return default(TControlItem);
		}

		// Token: 0x06006A12 RID: 27154 RVA: 0x0018E330 File Offset: 0x0018C530
		protected virtual void AddItemToParentControls(int index, ControlItem item)
		{
			if (index == -1)
			{
				this._parent.Controls.AddAt(index, item);
				return;
			}
			this._parent.Controls.AddAt(this.ControlsCount + index, item);
		}

		// Token: 0x06006A13 RID: 27155 RVA: 0x0018E364 File Offset: 0x0018C564
		internal void SetItemContainer(ControlItemContainer itemContainer)
		{
			this._itemContainer = itemContainer;
			foreach (object obj in this)
			{
				ControlItem controlItem = (ControlItem)obj;
				controlItem.SetItemContainer(itemContainer);
			}
		}

		// Token: 0x06006A14 RID: 27156 RVA: 0x0018E3C0 File Offset: 0x0018C5C0
		public ControlItemCollection(Control parent)
		{
			this._parent = parent;
		}

		// Token: 0x06006A15 RID: 27157 RVA: 0x0018E3D0 File Offset: 0x0018C5D0
		protected override void OnInsertComplete(int index, object value)
		{
			ControlItem controlItem = (ControlItem)value;
			this.AddItemToParentControls(index, controlItem);
			this.SetOwner(controlItem);
			if (index >= 0)
			{
				this.AssignIdToChildren(index);
			}
			else
			{
				ControlItemCollection.AssignId(controlItem, base.Count - 1);
			}
			if (this._itemContainer != null)
			{
				controlItem.SetItemContainer(this._itemContainer);
			}
		}

		// Token: 0x06006A16 RID: 27158 RVA: 0x0018E424 File Offset: 0x0018C624
		protected override void OnRemoveComplete(int index, object value)
		{
			Control control = (Control)value;
			if (this._parent.Controls.Contains(control))
			{
				this._parent.Controls.Remove(control);
			}
			this.AssignIdToChildren(index);
		}

		// Token: 0x06006A17 RID: 27159 RVA: 0x0018E464 File Offset: 0x0018C664
		protected override void OnClear()
		{
			foreach (object obj in this)
			{
				ControlItem value = (ControlItem)obj;
				this._parent.Controls.Remove(value);
			}
		}

		// Token: 0x06006A18 RID: 27160 RVA: 0x0018E4C4 File Offset: 0x0018C6C4
		private void AssignIdToChildren(int startIndex)
		{
			for (int i = startIndex; i < base.Count; i++)
			{
				ControlItemCollection.AssignId(this[i], i);
			}
		}

		// Token: 0x06006A19 RID: 27161 RVA: 0x0018E4EF File Offset: 0x0018C6EF
		private static void AssignId(ControlItem item, int index)
		{
			item.ID = "i" + index;
		}

		// Token: 0x06006A1A RID: 27162
		protected abstract void SetOwner(ControlItem item);

		// Token: 0x06006A1B RID: 27163 RVA: 0x0018E507 File Offset: 0x0018C707
		protected override void SetDirtyObject(object o)
		{
			((IMarkableStateManager)o).SetDirty();
		}

		// Token: 0x04001CBB RID: 7355
		private readonly Control _parent;

		// Token: 0x04001CBC RID: 7356
		private ControlItemContainer _itemContainer;

		// Token: 0x04001CBD RID: 7357
		private int _controlsCount;
	}
}
