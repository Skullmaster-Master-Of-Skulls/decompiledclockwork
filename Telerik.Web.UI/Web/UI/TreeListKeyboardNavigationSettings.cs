using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x0200122E RID: 4654
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class TreeListKeyboardNavigationSettings : StateManager
	{
		// Token: 0x17003DF2 RID: 15858
		// (get) Token: 0x0600C006 RID: 49158 RVA: 0x002A99D8 File Offset: 0x002A7BD8
		// (set) Token: 0x0600C007 RID: 49159 RVA: 0x002A9A01 File Offset: 0x002A7C01
		[Description("This property set whether active row should be set to first/last item when current item is last/first and down/up key is pressed(default is false).")]
		[DefaultValue(false)]
		[Category("Client")]
		[NotifyParentProperty(true)]
		public virtual bool AllowActiveRowCycle
		{
			get
			{
				object obj = base.ViewState["AllowActiveRowCycle"];
				return obj != null && (bool)obj;
			}
			set
			{
				base.ViewState["AllowActiveRowCycle"] = value;
			}
		}

		// Token: 0x17003DF3 RID: 15859
		// (get) Token: 0x0600C008 RID: 49160 RVA: 0x002A9A1C File Offset: 0x002A7C1C
		// (set) Token: 0x0600C009 RID: 49161 RVA: 0x002A9A45 File Offset: 0x002A7C45
		[DefaultValue(false)]
		[Description("This property set whether the edit form will be submited when the ENTER key is pressed (default is false).")]
		[Category("Client")]
		[NotifyParentProperty(true)]
		public virtual bool AllowSubmitOnEnter
		{
			get
			{
				object obj = base.ViewState["AllowSubmitOnEnter"];
				return obj != null && (bool)obj;
			}
			set
			{
				base.ViewState["AllowSubmitOnEnter"] = value;
			}
		}

		// Token: 0x17003DF4 RID: 15860
		// (get) Token: 0x0600C00A RID: 49162 RVA: 0x002A9A60 File Offset: 0x002A7C60
		// (set) Token: 0x0600C00B RID: 49163 RVA: 0x002A9A8A File Offset: 0x002A7C8A
		[Description("This property sets the key that is used to focus RadTreeList. It is always used with CTRL key combination.")]
		[Category("Client")]
		[DefaultValue(typeof(TreeListFocusKeys), "Y")]
		[NotifyParentProperty(true)]
		public virtual TreeListFocusKeys FocusKey
		{
			get
			{
				object obj = base.ViewState["_focusKey"];
				if (obj != null)
				{
					return (TreeListFocusKeys)obj;
				}
				return TreeListFocusKeys.Y;
			}
			set
			{
				base.ViewState["_focusKey"] = value;
			}
		}

		// Token: 0x17003DF5 RID: 15861
		// (get) Token: 0x0600C00C RID: 49164 RVA: 0x002A9AA4 File Offset: 0x002A7CA4
		// (set) Token: 0x0600C00D RID: 49165 RVA: 0x002A9ACE File Offset: 0x002A7CCE
		[NotifyParentProperty(true)]
		[Description("This property sets the key that is used to open insert edit form of RadTreeList. It is always used with CTRL key combination.")]
		[Category("Client")]
		[DefaultValue(typeof(TreeListFocusKeys), "I")]
		public virtual TreeListFocusKeys InitInsertKey
		{
			get
			{
				object obj = base.ViewState["_initInsertKey"];
				if (obj != null)
				{
					return (TreeListFocusKeys)obj;
				}
				return TreeListFocusKeys.I;
			}
			set
			{
				base.ViewState["_initInsertKey"] = value;
			}
		}

		// Token: 0x17003DF6 RID: 15862
		// (get) Token: 0x0600C00E RID: 49166 RVA: 0x002A9AE8 File Offset: 0x002A7CE8
		// (set) Token: 0x0600C00F RID: 49167 RVA: 0x002A9B12 File Offset: 0x002A7D12
		[DefaultValue(typeof(TreeListFocusKeys), "RightArrow")]
		[Description("This property set the key that is used for expanding the active row's child items.")]
		[Category("Client")]
		[NotifyParentProperty(true)]
		public virtual TreeListFocusKeys ExpandChildItemsKey
		{
			get
			{
				object obj = base.ViewState["_expandChildItemsKey"];
				if (obj != null)
				{
					return (TreeListFocusKeys)obj;
				}
				return TreeListFocusKeys.RightArrow;
			}
			set
			{
				base.ViewState["_expandChildItemsKey"] = value;
			}
		}

		// Token: 0x17003DF7 RID: 15863
		// (get) Token: 0x0600C010 RID: 49168 RVA: 0x002A9B2C File Offset: 0x002A7D2C
		// (set) Token: 0x0600C011 RID: 49169 RVA: 0x002A9B56 File Offset: 0x002A7D56
		[Category("Client")]
		[Description("This property set the key that is used for collapsing the active row's child item")]
		[DefaultValue(typeof(TreeListFocusKeys), "LeftArrow")]
		[NotifyParentProperty(true)]
		public virtual TreeListFocusKeys CollapseChildItemsKey
		{
			get
			{
				object obj = base.ViewState["_collapseChildItemsKey"];
				if (obj != null)
				{
					return (TreeListFocusKeys)obj;
				}
				return TreeListFocusKeys.LeftArrow;
			}
			set
			{
				base.ViewState["_collapseChildItemsKey"] = value;
			}
		}

		// Token: 0x17003DF8 RID: 15864
		// (get) Token: 0x0600C012 RID: 49170 RVA: 0x002A9B70 File Offset: 0x002A7D70
		[Category("Client")]
		[Description("Exit edit/insert mode key")]
		[NotifyParentProperty(true)]
		[DefaultValue(27)]
		public virtual int ExitEditInsertModeKey
		{
			get
			{
				object obj = base.ViewState["_eemk"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 27;
			}
		}

		// Token: 0x17003DF9 RID: 15865
		// (get) Token: 0x0600C013 RID: 49171 RVA: 0x002A9B9C File Offset: 0x002A7D9C
		[DefaultValue(13)]
		[Description("Update/insert item key")]
		[Category("Client")]
		[NotifyParentProperty(true)]
		public virtual int UpdateInsertItemKey
		{
			get
			{
				object obj = base.ViewState["_uiik"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 13;
			}
		}

		// Token: 0x17003DFA RID: 15866
		// (get) Token: 0x0600C014 RID: 49172 RVA: 0x002A9BC8 File Offset: 0x002A7DC8
		[Category("Client")]
		[NotifyParentProperty(true)]
		[Description("Delete the active row")]
		[DefaultValue(127)]
		public virtual int DeleteActiveRowKey
		{
			get
			{
				object obj = base.ViewState["_dsik"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 127;
			}
		}

		// Token: 0x17003DFB RID: 15867
		// (get) Token: 0x0600C015 RID: 49173 RVA: 0x002A9BF2 File Offset: 0x002A7DF2
		internal bool ShouldSerializeAllowActiveRowCycle
		{
			get
			{
				return this.AllowActiveRowCycle;
			}
		}

		// Token: 0x17003DFC RID: 15868
		// (get) Token: 0x0600C016 RID: 49174 RVA: 0x002A9BFA File Offset: 0x002A7DFA
		internal bool ShouldSerializeAllowSubmitOnEnter
		{
			get
			{
				return this.AllowSubmitOnEnter;
			}
		}

		// Token: 0x17003DFD RID: 15869
		// (get) Token: 0x0600C017 RID: 49175 RVA: 0x002A9C02 File Offset: 0x002A7E02
		internal bool ShouldSerializeCollapseChildItemsKey
		{
			get
			{
				return this.CollapseChildItemsKey != TreeListFocusKeys.LeftArrow;
			}
		}

		// Token: 0x17003DFE RID: 15870
		// (get) Token: 0x0600C018 RID: 49176 RVA: 0x002A9C11 File Offset: 0x002A7E11
		internal bool ShouldSerializeDeleteActiveRowKey
		{
			get
			{
				return this.DeleteActiveRowKey != 127;
			}
		}

		// Token: 0x17003DFF RID: 15871
		// (get) Token: 0x0600C019 RID: 49177 RVA: 0x002A9C20 File Offset: 0x002A7E20
		internal bool ShouldSerializeExitEditInsertModeKey
		{
			get
			{
				return this.ExitEditInsertModeKey != 27;
			}
		}

		// Token: 0x17003E00 RID: 15872
		// (get) Token: 0x0600C01A RID: 49178 RVA: 0x002A9C2F File Offset: 0x002A7E2F
		internal bool ShouldSerializeExpandChildItemsKey
		{
			get
			{
				return this.ExpandChildItemsKey != TreeListFocusKeys.RightArrow;
			}
		}

		// Token: 0x17003E01 RID: 15873
		// (get) Token: 0x0600C01B RID: 49179 RVA: 0x002A9C3E File Offset: 0x002A7E3E
		internal bool ShouldSerializeFocusKey
		{
			get
			{
				return this.FocusKey != TreeListFocusKeys.Y;
			}
		}

		// Token: 0x17003E02 RID: 15874
		// (get) Token: 0x0600C01C RID: 49180 RVA: 0x002A9C4D File Offset: 0x002A7E4D
		internal bool ShouldSerializeInitInsertKey
		{
			get
			{
				return this.InitInsertKey != TreeListFocusKeys.I;
			}
		}

		// Token: 0x17003E03 RID: 15875
		// (get) Token: 0x0600C01D RID: 49181 RVA: 0x002A9C5C File Offset: 0x002A7E5C
		internal bool ShouldSerializeUpdateInsertItemKey
		{
			get
			{
				return this.UpdateInsertItemKey != 13;
			}
		}
	}
}
