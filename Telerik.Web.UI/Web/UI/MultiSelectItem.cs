using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;
using Telerik.Web.UI.MultiSelect;

namespace Telerik.Web.UI
{
	// Token: 0x02000606 RID: 1542
	[Serializable]
	public class MultiSelectItem : StateManager, IItem
	{
		// Token: 0x17001233 RID: 4659
		// (get) Token: 0x06003791 RID: 14225 RVA: 0x000B78F5 File Offset: 0x000B5AF5
		// (set) Token: 0x06003792 RID: 14226 RVA: 0x000B78FD File Offset: 0x000B5AFD
		[Browsable(false)]
		public RadMultiSelect Owner { get; set; }

		// Token: 0x17001234 RID: 4660
		// (get) Token: 0x06003793 RID: 14227 RVA: 0x000B7906 File Offset: 0x000B5B06
		// (set) Token: 0x06003794 RID: 14228 RVA: 0x000B7922 File Offset: 0x000B5B22
		internal Dictionary<string, object> TemplateData
		{
			get
			{
				return (Dictionary<string, object>)(base.ViewState["TemplateData"] ?? null);
			}
			set
			{
				base.ViewState["TemplateData"] = value;
			}
		}

		// Token: 0x06003795 RID: 14229 RVA: 0x000B7935 File Offset: 0x000B5B35
		void IItem.DataBind()
		{
		}

		// Token: 0x06003796 RID: 14230 RVA: 0x000B7938 File Offset: 0x000B5B38
		void IItem.PopulateFromDataItem(PropertyDescriptorCache properties, object dataItem, string dataMember, int depth)
		{
			if (!string.IsNullOrEmpty(this.Owner.DataTextField))
			{
				try
				{
					this.Text = properties.GetPropertyValue(dataItem, this.Owner.DataTextField).ToString();
					goto IL_40;
				}
				catch (ArgumentException)
				{
					throw;
				}
			}
			this.Text = dataItem.ToString();
			IL_40:
			if (!string.IsNullOrEmpty(this.Owner.DataValueField))
			{
				try
				{
					this.Value = DataBinder.GetPropertyValue(dataItem, this.Owner.DataValueField, null);
				}
				catch
				{
					throw;
				}
			}
			if (string.IsNullOrEmpty(this.Owner.DataTextField) && string.IsNullOrEmpty(this.Owner.DataValueField))
			{
				this.Value = dataItem.ToString();
			}
		}

		// Token: 0x17001235 RID: 4661
		// (get) Token: 0x06003797 RID: 14231 RVA: 0x000B7A00 File Offset: 0x000B5C00
		IList IItem.Children
		{
			get
			{
				return this.Items;
			}
		}

		// Token: 0x06003798 RID: 14232 RVA: 0x000B7A08 File Offset: 0x000B5C08
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			base.LoadViewState(array[0]);
			this.LoadChildViewState(array[1]);
		}

		// Token: 0x06003799 RID: 14233 RVA: 0x000B7A30 File Offset: 0x000B5C30
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				this.SaveChildViewState()
			};
		}

		// Token: 0x0600379A RID: 14234 RVA: 0x000B7A57 File Offset: 0x000B5C57
		protected override void TrackViewState()
		{
			base.TrackViewState();
			this.TrackChildViewState();
		}

		// Token: 0x0600379B RID: 14235 RVA: 0x000B7A65 File Offset: 0x000B5C65
		protected void LoadChildViewState(object viewState)
		{
			if (viewState == null)
			{
				this.Items.Clear();
				return;
			}
			((IStateManager)this.Items).LoadViewState(viewState);
		}

		// Token: 0x0600379C RID: 14236 RVA: 0x000B7A82 File Offset: 0x000B5C82
		protected object SaveChildViewState()
		{
			return ((IStateManager)this.Items).SaveViewState();
		}

		// Token: 0x0600379D RID: 14237 RVA: 0x000B7A8F File Offset: 0x000B5C8F
		protected void TrackChildViewState()
		{
			((IStateManager)this.Items).TrackViewState();
		}

		// Token: 0x0600379E RID: 14238 RVA: 0x000B7A9C File Offset: 0x000B5C9C
		protected void SetChildrenDirty()
		{
			foreach (IMarkableStateManager markableStateManager in this.Items)
			{
				markableStateManager.SetDirty();
			}
		}

		// Token: 0x17001236 RID: 4662
		// (get) Token: 0x0600379F RID: 14239 RVA: 0x000B7AE8 File Offset: 0x000B5CE8
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public MultiSelectItemCollection Items
		{
			get
			{
				if (this._items == null)
				{
					this._items = new MultiSelectItemCollection(this.Owner);
				}
				return this._items;
			}
		}

		// Token: 0x17001237 RID: 4663
		// (get) Token: 0x060037A0 RID: 14240 RVA: 0x000B7B09 File Offset: 0x000B5D09
		// (set) Token: 0x060037A1 RID: 14241 RVA: 0x000B7B25 File Offset: 0x000B5D25
		[Browsable(false)]
		public Dictionary<string, string> Attributes
		{
			get
			{
				return (Dictionary<string, string>)(base.ViewState["Attributes"] ?? null);
			}
			set
			{
				base.ViewState["Attributes"] = value;
			}
		}

		// Token: 0x17001238 RID: 4664
		// (get) Token: 0x060037A2 RID: 14242 RVA: 0x000B7B38 File Offset: 0x000B5D38
		// (set) Token: 0x060037A3 RID: 14243 RVA: 0x000B7B58 File Offset: 0x000B5D58
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

		// Token: 0x17001239 RID: 4665
		// (get) Token: 0x060037A4 RID: 14244 RVA: 0x000B7B6B File Offset: 0x000B5D6B
		// (set) Token: 0x060037A5 RID: 14245 RVA: 0x000B7B8B File Offset: 0x000B5D8B
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

		// Token: 0x1700123A RID: 4666
		// (get) Token: 0x060037A6 RID: 14246 RVA: 0x000B7B9E File Offset: 0x000B5D9E
		// (set) Token: 0x060037A7 RID: 14247 RVA: 0x000B7BBE File Offset: 0x000B5DBE
		[DefaultValue("")]
		[Category("Behavior")]
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

		// Token: 0x1700123B RID: 4667
		// (get) Token: 0x060037A8 RID: 14248 RVA: 0x000B7BD1 File Offset: 0x000B5DD1
		// (set) Token: 0x060037A9 RID: 14249 RVA: 0x000B7BD9 File Offset: 0x000B5DD9
		[Browsable(false)]
		public object DataItem { get; set; }

		// Token: 0x1700123C RID: 4668
		// (get) Token: 0x060037AA RID: 14250 RVA: 0x000B7BE2 File Offset: 0x000B5DE2
		// (set) Token: 0x060037AB RID: 14251 RVA: 0x000B7C03 File Offset: 0x000B5E03
		[DefaultValue(false)]
		[Description("Whether the item is selected or not.")]
		[Category("Behavior")]
		public bool Selected
		{
			get
			{
				return (bool)(base.ViewState["Selected"] ?? false);
			}
			set
			{
				base.ViewState["Selected"] = value;
			}
		}

		// Token: 0x1700123D RID: 4669
		// (get) Token: 0x060037AC RID: 14252 RVA: 0x000B7C1B File Offset: 0x000B5E1B
		// (set) Token: 0x060037AD RID: 14253 RVA: 0x000B7C3C File Offset: 0x000B5E3C
		[DefaultValue(true)]
		[Description("Whether the item is enabled or not.")]
		[Category("Behavior")]
		public bool Enabled
		{
			get
			{
				return (bool)(base.ViewState["Enabled"] ?? true);
			}
			set
			{
				this.TrackViewState();
				base.ViewState["Enabled"] = value;
			}
		}

		// Token: 0x1700123E RID: 4670
		// (get) Token: 0x060037AE RID: 14254 RVA: 0x000B7C5A File Offset: 0x000B5E5A
		// (set) Token: 0x060037AF RID: 14255 RVA: 0x000B7C7B File Offset: 0x000B5E7B
		[Description("Whether the item is visible or not.")]
		[DefaultValue(true)]
		[Category("Behavior")]
		public bool Visible
		{
			get
			{
				return (bool)(base.ViewState["Visible"] ?? true);
			}
			set
			{
				this.TrackViewState();
				base.ViewState["Visible"] = value;
			}
		}

		// Token: 0x060037B0 RID: 14256 RVA: 0x000B7C99 File Offset: 0x000B5E99
		public MultiSelectItem()
		{
			if (this.Attributes == null)
			{
				this.Attributes = new Dictionary<string, string>();
			}
		}

		// Token: 0x060037B1 RID: 14257 RVA: 0x000B7CB4 File Offset: 0x000B5EB4
		public MultiSelectItem(string text) : this()
		{
			this.Text = text;
		}

		// Token: 0x060037B2 RID: 14258 RVA: 0x000B7CC3 File Offset: 0x000B5EC3
		public MultiSelectItem(string text, string value) : this(text)
		{
			this.Value = value;
		}

		// Token: 0x060037B3 RID: 14259 RVA: 0x000B7CD3 File Offset: 0x000B5ED3
		public MultiSelectItem(RadMultiSelect owner) : this()
		{
			this.Owner = owner;
		}

		// Token: 0x060037B4 RID: 14260 RVA: 0x000B7CE4 File Offset: 0x000B5EE4
		public int CompareTo(object obj)
		{
			MultiSelectItem multiSelectItem = obj as MultiSelectItem;
			if (multiSelectItem == null)
			{
				throw new ArgumentException();
			}
			MultiSelectItem multiSelectItem2 = multiSelectItem;
			return string.Compare(this.Text, multiSelectItem2.Text, true);
		}

		// Token: 0x04000EE8 RID: 3816
		private MultiSelectItemCollection _items;
	}
}
