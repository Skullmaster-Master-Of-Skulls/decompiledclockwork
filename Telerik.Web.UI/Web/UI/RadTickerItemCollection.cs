using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Web.UI;
using Telerik.Web.UI.Rotator;

namespace Telerik.Web.UI
{
	// Token: 0x020019D6 RID: 6614
	public class RadTickerItemCollection : StateManagedCollection
	{
		// Token: 0x06010026 RID: 65574 RVA: 0x00397368 File Offset: 0x00395568
		public RadTickerItemCollection(Control parent)
		{
			this._parent = parent;
		}

		// Token: 0x17004D52 RID: 19794
		// (get) Token: 0x06010027 RID: 65575 RVA: 0x00397377 File Offset: 0x00395577
		protected Control Parent
		{
			get
			{
				return this._parent;
			}
		}

		// Token: 0x17004D53 RID: 19795
		public RadTickerItem this[int index]
		{
			get
			{
				return (RadTickerItem)this.List[index];
			}
			set
			{
				this.List[index] = value;
			}
		}

		// Token: 0x17004D54 RID: 19796
		// (get) Token: 0x0601002A RID: 65578 RVA: 0x003973A1 File Offset: 0x003955A1
		protected IList List
		{
			get
			{
				return this;
			}
		}

		// Token: 0x0601002B RID: 65579 RVA: 0x003973A4 File Offset: 0x003955A4
		public void Add(RadTickerItem item)
		{
			this.List.Add(item);
		}

		// Token: 0x0601002C RID: 65580 RVA: 0x003973B3 File Offset: 0x003955B3
		public bool Contains(RadTickerItem item)
		{
			return this.List.Contains(item);
		}

		// Token: 0x0601002D RID: 65581 RVA: 0x003973C1 File Offset: 0x003955C1
		public void CopyTo(RadTickerItem[] array, int index)
		{
			this.List.CopyTo(array, index);
		}

		// Token: 0x0601002E RID: 65582 RVA: 0x003973D0 File Offset: 0x003955D0
		public void AddRange(IEnumerable<RadTickerItem> items)
		{
			foreach (RadTickerItem item in items)
			{
				this.Add(item);
			}
		}

		// Token: 0x0601002F RID: 65583 RVA: 0x00397418 File Offset: 0x00395618
		public int IndexOf(RadTickerItem item)
		{
			return this.List.IndexOf(item);
		}

		// Token: 0x06010030 RID: 65584 RVA: 0x00397426 File Offset: 0x00395626
		public void Insert(int index, RadTickerItem item)
		{
			this.List.Insert(index, item);
		}

		// Token: 0x06010031 RID: 65585 RVA: 0x00397438 File Offset: 0x00395638
		protected override void OnInsertComplete(int index, object value)
		{
			RadTickerItem radTickerItem = (RadTickerItem)value;
			this._parent.Controls.AddAt(index, radTickerItem);
			if (index >= 0)
			{
				for (int i = index; i < base.Count; i++)
				{
					this[i].ID = "i" + i;
				}
			}
			else
			{
				radTickerItem.ID = "i" + this.IndexOf(radTickerItem);
			}
			if (this._itemContainer != null)
			{
				radTickerItem.SetItemContainer(this._itemContainer);
			}
		}

		// Token: 0x06010032 RID: 65586 RVA: 0x003974C4 File Offset: 0x003956C4
		protected override void OnClear()
		{
			foreach (object obj in this)
			{
				RadTickerItem value = (RadTickerItem)obj;
				this._parent.Controls.Remove(value);
			}
			base.OnClear();
		}

		// Token: 0x06010033 RID: 65587 RVA: 0x00397528 File Offset: 0x00395728
		protected override void OnRemoveComplete(int index, object value)
		{
			Control control = value as Control;
			if (this._parent.Controls.Contains(control))
			{
				this._parent.Controls.Remove(control);
			}
		}

		// Token: 0x06010034 RID: 65588 RVA: 0x00397560 File Offset: 0x00395760
		public void Remove(RadTickerItem item)
		{
			this.List.Remove(item);
		}

		// Token: 0x06010035 RID: 65589 RVA: 0x0039756E File Offset: 0x0039576E
		public void RemoveAt(int index)
		{
			this.List.RemoveAt(index);
		}

		// Token: 0x06010036 RID: 65590 RVA: 0x0039757C File Offset: 0x0039577C
		internal void SetItemContainer(RadTicker itemContainer)
		{
			this._itemContainer = itemContainer;
			foreach (object obj in this)
			{
				RadTickerItem radTickerItem = (RadTickerItem)obj;
				radTickerItem.SetItemContainer(itemContainer);
			}
		}

		// Token: 0x06010037 RID: 65591 RVA: 0x003975D8 File Offset: 0x003957D8
		protected override void SetDirtyObject(object o)
		{
			((IMarkableStateManager)o).SetDirty();
		}

		// Token: 0x06010038 RID: 65592 RVA: 0x003975E8 File Offset: 0x003957E8
		internal string Serialize()
		{
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			javaScriptSerializer.RegisterConverters(new RadTickerItemConverter[]
			{
				new RadTickerItemConverter()
			});
			return javaScriptSerializer.Serialize(this);
		}

		// Token: 0x04004877 RID: 18551
		private readonly Control _parent;

		// Token: 0x04004878 RID: 18552
		private RadTicker _itemContainer;
	}
}
