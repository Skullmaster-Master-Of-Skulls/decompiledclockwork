using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000972 RID: 2418
	public class TreeMapItem : StateManager, IItem
	{
		// Token: 0x17001E48 RID: 7752
		// (get) Token: 0x06005BE2 RID: 23522 RVA: 0x00118427 File Offset: 0x00116627
		// (set) Token: 0x06005BE3 RID: 23523 RVA: 0x00118443 File Offset: 0x00116643
		internal Dictionary<string, string> TemplateData
		{
			get
			{
				return (Dictionary<string, string>)(base.ViewState["TemplateData"] ?? null);
			}
			set
			{
				base.ViewState["TemplateData"] = value;
			}
		}

		// Token: 0x06005BE4 RID: 23524 RVA: 0x00118456 File Offset: 0x00116656
		void IItem.DataBind()
		{
		}

		// Token: 0x06005BE5 RID: 23525 RVA: 0x00118458 File Offset: 0x00116658
		void IItem.PopulateFromDataItem(PropertyDescriptorCache properties, object dataItem, string dataMember, int depth)
		{
			if (!string.IsNullOrEmpty(this._treeMap.DataFieldID))
			{
				this.ID = properties.GetPropertyValue(dataItem, this._treeMap.DataFieldID).ToString();
			}
			if (!string.IsNullOrEmpty(this._treeMap.DataTextField))
			{
				this.Text = properties.GetPropertyValue(dataItem, this._treeMap.DataTextField).ToString();
			}
			if (!string.IsNullOrEmpty(this._treeMap.DataValueField))
			{
				this.Value = properties.GetPropertyValue(dataItem, this._treeMap.DataValueField).ToString();
			}
			if (!string.IsNullOrEmpty(this._treeMap.DataColorField))
			{
				this.Color = ColorTranslator.FromHtml(properties.GetPropertyValue(dataItem, this._treeMap.DataColorField).ToString());
			}
		}

		// Token: 0x17001E49 RID: 7753
		// (get) Token: 0x06005BE6 RID: 23526 RVA: 0x00118526 File Offset: 0x00116726
		IList IItem.Children
		{
			get
			{
				return this.Items;
			}
		}

		// Token: 0x06005BE7 RID: 23527 RVA: 0x00118530 File Offset: 0x00116730
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			base.LoadViewState(array[0]);
			this.LoadChildViewState(array[1]);
		}

		// Token: 0x06005BE8 RID: 23528 RVA: 0x00118558 File Offset: 0x00116758
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				this.SaveChildViewState()
			};
		}

		// Token: 0x06005BE9 RID: 23529 RVA: 0x0011857F File Offset: 0x0011677F
		protected override void TrackViewState()
		{
			base.TrackViewState();
			this.TrackChildViewState();
		}

		// Token: 0x06005BEA RID: 23530 RVA: 0x0011858D File Offset: 0x0011678D
		protected void LoadChildViewState(object viewState)
		{
			if (viewState == null)
			{
				this.Items.Clear();
				return;
			}
			((IStateManager)this.Items).LoadViewState(viewState);
		}

		// Token: 0x06005BEB RID: 23531 RVA: 0x001185AA File Offset: 0x001167AA
		protected object SaveChildViewState()
		{
			return ((IStateManager)this.Items).SaveViewState();
		}

		// Token: 0x06005BEC RID: 23532 RVA: 0x001185B7 File Offset: 0x001167B7
		protected void TrackChildViewState()
		{
			((IStateManager)this.Items).TrackViewState();
		}

		// Token: 0x06005BED RID: 23533 RVA: 0x001185C4 File Offset: 0x001167C4
		protected void SetChildrenDirty()
		{
			foreach (object obj in this.Items)
			{
				IMarkableStateManager markableStateManager = (IMarkableStateManager)obj;
				markableStateManager.SetDirty();
			}
		}

		// Token: 0x17001E4A RID: 7754
		// (get) Token: 0x06005BEE RID: 23534 RVA: 0x0011861C File Offset: 0x0011681C
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public TreeMapItemCollection Items
		{
			get
			{
				if (this._items == null)
				{
					this._items = new TreeMapItemCollection(this._treeMap);
				}
				return this._items;
			}
		}

		// Token: 0x17001E4B RID: 7755
		// (get) Token: 0x06005BEF RID: 23535 RVA: 0x0011863D File Offset: 0x0011683D
		// (set) Token: 0x06005BF0 RID: 23536 RVA: 0x0011865D File Offset: 0x0011685D
		[DefaultValue("")]
		public virtual string ID
		{
			get
			{
				return (string)(base.ViewState["ID"] ?? string.Empty);
			}
			internal set
			{
				base.ViewState["ID"] = value;
			}
		}

		// Token: 0x17001E4C RID: 7756
		// (get) Token: 0x06005BF1 RID: 23537 RVA: 0x00118670 File Offset: 0x00116870
		// (set) Token: 0x06005BF2 RID: 23538 RVA: 0x00118690 File Offset: 0x00116890
		[DefaultValue("")]
		public virtual string Text
		{
			get
			{
				return (string)(base.ViewState["Text"] ?? string.Empty);
			}
			set
			{
				base.ViewState["Text"] = value;
			}
		}

		// Token: 0x17001E4D RID: 7757
		// (get) Token: 0x06005BF3 RID: 23539 RVA: 0x001186A3 File Offset: 0x001168A3
		// (set) Token: 0x06005BF4 RID: 23540 RVA: 0x001186C3 File Offset: 0x001168C3
		[Category("Behavior")]
		[DefaultValue("")]
		public virtual string Value
		{
			get
			{
				return (string)(base.ViewState["Value"] ?? string.Empty);
			}
			set
			{
				base.ViewState["Value"] = value;
			}
		}

		// Token: 0x17001E4E RID: 7758
		// (get) Token: 0x06005BF5 RID: 23541 RVA: 0x001186D6 File Offset: 0x001168D6
		// (set) Token: 0x06005BF6 RID: 23542 RVA: 0x001186FB File Offset: 0x001168FB
		[DefaultValue("")]
		[Category("Behavior")]
		public virtual Color Color
		{
			get
			{
				return (Color)(base.ViewState["Color"] ?? Color.Empty);
			}
			set
			{
				base.ViewState["Color"] = value;
			}
		}

		// Token: 0x17001E4F RID: 7759
		// (get) Token: 0x06005BF7 RID: 23543 RVA: 0x00118713 File Offset: 0x00116913
		// (set) Token: 0x06005BF8 RID: 23544 RVA: 0x0011871B File Offset: 0x0011691B
		[Browsable(false)]
		public object DataItem { get; set; }

		// Token: 0x06005BF9 RID: 23545 RVA: 0x00118724 File Offset: 0x00116924
		public TreeMapItem()
		{
		}

		// Token: 0x06005BFA RID: 23546 RVA: 0x0011872C File Offset: 0x0011692C
		public TreeMapItem(RadTreeMap control)
		{
			this._treeMap = control;
		}

		// Token: 0x04001616 RID: 5654
		private RadTreeMap _treeMap;

		// Token: 0x04001617 RID: 5655
		private TreeMapItemCollection _items;
	}
}
