using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;
using Telerik.Web.UI.MultiColumnComboBox;

namespace Telerik.Web.UI
{
	// Token: 0x0200005F RID: 95
	[Serializable]
	public class MultiColumnComboBoxItem : StateManager, IItem
	{
		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x000077AE File Offset: 0x000059AE
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public MultiColumnComboBoxItemCollection Items
		{
			get
			{
				if (this._items == null)
				{
					this._items = new MultiColumnComboBoxItemCollection(this.Owner);
				}
				return this._items;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x000077CF File Offset: 0x000059CF
		// (set) Token: 0x060002C8 RID: 712 RVA: 0x000077EB File Offset: 0x000059EB
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

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060002C9 RID: 713 RVA: 0x000077FE File Offset: 0x000059FE
		// (set) Token: 0x060002CA RID: 714 RVA: 0x0000781E File Offset: 0x00005A1E
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

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060002CB RID: 715 RVA: 0x00007831 File Offset: 0x00005A31
		// (set) Token: 0x060002CC RID: 716 RVA: 0x00007851 File Offset: 0x00005A51
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

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060002CD RID: 717 RVA: 0x00007864 File Offset: 0x00005A64
		// (set) Token: 0x060002CE RID: 718 RVA: 0x00007884 File Offset: 0x00005A84
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

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060002CF RID: 719 RVA: 0x00007897 File Offset: 0x00005A97
		// (set) Token: 0x060002D0 RID: 720 RVA: 0x0000789F File Offset: 0x00005A9F
		[Browsable(false)]
		public object DataItem { get; set; }

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x000078A8 File Offset: 0x00005AA8
		// (set) Token: 0x060002D2 RID: 722 RVA: 0x000078C9 File Offset: 0x00005AC9
		[Description("Whether the item is selected or not.")]
		[Category("Behavior")]
		[DefaultValue(false)]
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

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x000078E1 File Offset: 0x00005AE1
		// (set) Token: 0x060002D4 RID: 724 RVA: 0x00007902 File Offset: 0x00005B02
		[Category("Behavior")]
		[DefaultValue(true)]
		[Description("Whether the item is enabled or not.")]
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

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x00007920 File Offset: 0x00005B20
		// (set) Token: 0x060002D6 RID: 726 RVA: 0x00007941 File Offset: 0x00005B41
		[Category("Behavior")]
		[DefaultValue(true)]
		[Description("Whether the item is visible or not.")]
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

		// Token: 0x060002D7 RID: 727 RVA: 0x0000795F File Offset: 0x00005B5F
		public MultiColumnComboBoxItem()
		{
			if (this.Attributes == null)
			{
				this.Attributes = new Dictionary<string, string>();
			}
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0000797A File Offset: 0x00005B7A
		public MultiColumnComboBoxItem(string text) : this()
		{
			this.Text = text;
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x00007989 File Offset: 0x00005B89
		public MultiColumnComboBoxItem(string text, string value) : this(text)
		{
			this.Value = value;
		}

		// Token: 0x060002DA RID: 730 RVA: 0x00007999 File Offset: 0x00005B99
		public MultiColumnComboBoxItem(RadMultiColumnComboBox owner) : this()
		{
			this.Owner = owner;
		}

		// Token: 0x060002DB RID: 731 RVA: 0x000079A8 File Offset: 0x00005BA8
		public int CompareTo(object obj)
		{
			MultiColumnComboBoxItem multiColumnComboBoxItem = obj as MultiColumnComboBoxItem;
			if (multiColumnComboBoxItem == null)
			{
				throw new ArgumentException();
			}
			MultiColumnComboBoxItem multiColumnComboBoxItem2 = multiColumnComboBoxItem;
			return string.Compare(this.Text, multiColumnComboBoxItem2.Text, true);
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060002DC RID: 732 RVA: 0x000079D9 File Offset: 0x00005BD9
		// (set) Token: 0x060002DD RID: 733 RVA: 0x000079E1 File Offset: 0x00005BE1
		[Browsable(false)]
		public RadMultiColumnComboBox Owner { get; set; }

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060002DE RID: 734 RVA: 0x000079EA File Offset: 0x00005BEA
		// (set) Token: 0x060002DF RID: 735 RVA: 0x00007A06 File Offset: 0x00005C06
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

		// Token: 0x060002E0 RID: 736 RVA: 0x00007A19 File Offset: 0x00005C19
		void IItem.DataBind()
		{
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x00007A1C File Offset: 0x00005C1C
		void IItem.PopulateFromDataItem(PropertyDescriptorCache properties, object dataItem, string dataMember, int depth)
		{
			if (!string.IsNullOrEmpty(this.Owner.DataTextField))
			{
				this.Text = properties.GetPropertyValue(dataItem, this.Owner.DataTextField).ToString();
			}
			if (!string.IsNullOrEmpty(this.Owner.DataValueField))
			{
				this.Value = properties.GetPropertyValue(dataItem, this.Owner.DataValueField).ToString();
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x00007A87 File Offset: 0x00005C87
		IList IItem.Children
		{
			get
			{
				return this.Items;
			}
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x00007A90 File Offset: 0x00005C90
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			base.LoadViewState(array[0]);
			this.LoadChildViewState(array[1]);
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x00007AB8 File Offset: 0x00005CB8
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				this.SaveChildViewState()
			};
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x00007ADF File Offset: 0x00005CDF
		protected override void TrackViewState()
		{
			base.TrackViewState();
			this.TrackChildViewState();
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x00007AED File Offset: 0x00005CED
		protected void LoadChildViewState(object viewState)
		{
			if (viewState == null)
			{
				this.Items.Clear();
				return;
			}
			((IStateManager)this.Items).LoadViewState(viewState);
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x00007B0A File Offset: 0x00005D0A
		protected object SaveChildViewState()
		{
			return ((IStateManager)this.Items).SaveViewState();
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x00007B17 File Offset: 0x00005D17
		protected void TrackChildViewState()
		{
			((IStateManager)this.Items).TrackViewState();
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x00007B24 File Offset: 0x00005D24
		protected void SetChildrenDirty()
		{
			foreach (IMarkableStateManager markableStateManager in this.Items)
			{
				markableStateManager.SetDirty();
			}
		}

		// Token: 0x0400005B RID: 91
		private MultiColumnComboBoxItemCollection _items;
	}
}
