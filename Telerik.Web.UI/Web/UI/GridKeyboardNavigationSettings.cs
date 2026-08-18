using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02001903 RID: 6403
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class GridKeyboardNavigationSettings : ObjectWithState
	{
		// Token: 0x0600F71C RID: 63260 RVA: 0x00381170 File Offset: 0x0037F370
		public GridKeyboardNavigationSettings(StateBag OwnerStateBag) : base("cs_keyboradnavigationsettings_", OwnerStateBag)
		{
		}

		// Token: 0x17004A69 RID: 19049
		// (get) Token: 0x0600F71D RID: 63261 RVA: 0x003811D4 File Offset: 0x0037F3D4
		// (set) Token: 0x0600F71E RID: 63262 RVA: 0x003811FD File Offset: 0x0037F3FD
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

		// Token: 0x17004A6A RID: 19050
		// (get) Token: 0x0600F71F RID: 63263 RVA: 0x00381218 File Offset: 0x0037F418
		// (set) Token: 0x0600F720 RID: 63264 RVA: 0x00381241 File Offset: 0x0037F441
		[Description("This property set whether the edit form will be submited when the ENTER key is pressed (default is false).")]
		[DefaultValue(false)]
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

		// Token: 0x17004A6B RID: 19051
		// (get) Token: 0x0600F721 RID: 63265 RVA: 0x0038125C File Offset: 0x0037F45C
		// (set) Token: 0x0600F722 RID: 63266 RVA: 0x00381289 File Offset: 0x0037F489
		[Category("Client")]
		[DefaultValue("")]
		[Description("This property set the validation group of all controls placed into the Edit/Insert form of the RadGrid (default is empty string).")]
		[NotifyParentProperty(true)]
		public virtual string ValidationGroup
		{
			get
			{
				object obj = base.ViewState["ValidationGroup"];
				if (obj != null)
				{
					return obj.ToString();
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["ValidationGroup"] = value;
			}
		}

		// Token: 0x17004A6C RID: 19052
		// (get) Token: 0x0600F723 RID: 63267 RVA: 0x0038129C File Offset: 0x0037F49C
		// (set) Token: 0x0600F724 RID: 63268 RVA: 0x003812A4 File Offset: 0x0037F4A4
		[DefaultValue(typeof(GridFocusKeys), "Y")]
		[Description("This property sets the key that is used to focus RadGrid. It is always used with CTRL key combination.")]
		[NotifyParentProperty(true)]
		[Category("Client")]
		public virtual GridFocusKeys FocusKey
		{
			get
			{
				return this._focusKey;
			}
			set
			{
				this._focusKey = value;
			}
		}

		// Token: 0x17004A6D RID: 19053
		// (get) Token: 0x0600F725 RID: 63269 RVA: 0x003812AD File Offset: 0x0037F4AD
		// (set) Token: 0x0600F726 RID: 63270 RVA: 0x003812B5 File Offset: 0x0037F4B5
		[DefaultValue(typeof(GridFocusKeys), "I")]
		[Description("This property sets the key that is used to open insert edit form of RadGrid. It is always used with CTRL key combination.")]
		[NotifyParentProperty(true)]
		[Category("Client")]
		public virtual GridFocusKeys InitInsertKey
		{
			get
			{
				return this._initInserKey;
			}
			set
			{
				this._initInserKey = value;
			}
		}

		// Token: 0x17004A6E RID: 19054
		// (get) Token: 0x0600F727 RID: 63271 RVA: 0x003812BE File Offset: 0x0037F4BE
		// (set) Token: 0x0600F728 RID: 63272 RVA: 0x003812C6 File Offset: 0x0037F4C6
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(GridFocusKeys), "R")]
		[Description("This property sets the key that is used to rebind RadGrid. It is always used with CTRL key combination.")]
		[Category("Client")]
		public virtual GridFocusKeys RebindKey
		{
			get
			{
				return this._rebindKey;
			}
			set
			{
				this._rebindKey = value;
			}
		}

		// Token: 0x17004A6F RID: 19055
		// (get) Token: 0x0600F729 RID: 63273 RVA: 0x003812CF File Offset: 0x0037F4CF
		// (set) Token: 0x0600F72A RID: 63274 RVA: 0x003812D7 File Offset: 0x0037F4D7
		[DefaultValue(typeof(GridFocusKeys), "RightArrow")]
		[Description("This property set the key that is used for expanding the active row's detail table.")]
		[Category("Client")]
		[NotifyParentProperty(true)]
		public virtual GridFocusKeys ExpandDetailTableKey
		{
			get
			{
				return this._expandDetailTableKey;
			}
			set
			{
				this._expandDetailTableKey = value;
			}
		}

		// Token: 0x17004A70 RID: 19056
		// (get) Token: 0x0600F72B RID: 63275 RVA: 0x003812E0 File Offset: 0x0037F4E0
		// (set) Token: 0x0600F72C RID: 63276 RVA: 0x003812E8 File Offset: 0x0037F4E8
		[Description("This property sets the key that is used for collapsing the active row's detail table")]
		[DefaultValue(typeof(GridFocusKeys), "LeftArrow")]
		[Category("Client")]
		[NotifyParentProperty(true)]
		public virtual GridFocusKeys CollapseDetailTableKey
		{
			get
			{
				return this._collapseDetailTableKey;
			}
			set
			{
				this._collapseDetailTableKey = value;
			}
		}

		// Token: 0x17004A71 RID: 19057
		// (get) Token: 0x0600F72D RID: 63277 RVA: 0x003812F1 File Offset: 0x0037F4F1
		// (set) Token: 0x0600F72E RID: 63278 RVA: 0x003812F9 File Offset: 0x0037F4F9
		[DefaultValue(typeof(GridFocusKeys), "UpArrow")]
		[Category("Client")]
		[NotifyParentProperty(true)]
		[Description("This property sets the key that is used for moving up the active and selected rows")]
		public virtual GridFocusKeys MoveUpKey
		{
			get
			{
				return this._moveUpKey;
			}
			set
			{
				this._moveUpKey = value;
			}
		}

		// Token: 0x17004A72 RID: 19058
		// (get) Token: 0x0600F72F RID: 63279 RVA: 0x00381302 File Offset: 0x0037F502
		// (set) Token: 0x0600F730 RID: 63280 RVA: 0x0038130A File Offset: 0x0037F50A
		[Category("Client")]
		[DefaultValue(typeof(GridFocusKeys), "DownArrow")]
		[NotifyParentProperty(true)]
		[Description("This property sets the key that is used for moving Down the active and selected rows")]
		public virtual GridFocusKeys MoveDownKey
		{
			get
			{
				return this._moveDownKey;
			}
			set
			{
				this._moveDownKey = value;
			}
		}

		// Token: 0x17004A73 RID: 19059
		// (get) Token: 0x0600F731 RID: 63281 RVA: 0x00381314 File Offset: 0x0037F514
		[Category("Client")]
		[DefaultValue(27)]
		[Description("Exit edit/insert mode key")]
		[NotifyParentProperty(true)]
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

		// Token: 0x17004A74 RID: 19060
		// (get) Token: 0x0600F732 RID: 63282 RVA: 0x00381340 File Offset: 0x0037F540
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

		// Token: 0x17004A75 RID: 19061
		// (get) Token: 0x0600F733 RID: 63283 RVA: 0x0038136C File Offset: 0x0037F56C
		[Category("Client")]
		[Description("Delete the active row")]
		[DefaultValue(127)]
		[NotifyParentProperty(true)]
		public virtual int DeleteActiveRow
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

		// Token: 0x17004A76 RID: 19062
		// (get) Token: 0x0600F734 RID: 63284 RVA: 0x00381398 File Offset: 0x0037F598
		// (set) Token: 0x0600F735 RID: 63285 RVA: 0x003813C1 File Offset: 0x0037F5C1
		[Description("If set to false, prevents the keyboard short-cuts such as update/insert on ENTER,exit edit/insert mode on ESC, etc. from being active")]
		[DefaultValue(true)]
		[Category("Client")]
		[NotifyParentProperty(true)]
		public virtual bool EnableKeyboardShortcuts
		{
			get
			{
				object obj = base.ViewState["_ekbsc"];
				return obj == null || (bool)obj;
			}
			set
			{
				base.ViewState["_ekbsc"] = value;
			}
		}

		// Token: 0x17004A77 RID: 19063
		// (get) Token: 0x0600F736 RID: 63286 RVA: 0x003813D9 File Offset: 0x0037F5D9
		// (set) Token: 0x0600F737 RID: 63287 RVA: 0x003813E1 File Offset: 0x0037F5E1
		[Category("Client")]
		[DefaultValue(typeof(GridFocusKeys), "U")]
		[Description("")]
		[NotifyParentProperty(true)]
		public virtual GridFocusKeys SaveChangesKey
		{
			get
			{
				return this.saveChangesKey;
			}
			set
			{
				this.saveChangesKey = value;
			}
		}

		// Token: 0x17004A78 RID: 19064
		// (get) Token: 0x0600F738 RID: 63288 RVA: 0x003813EA File Offset: 0x0037F5EA
		// (set) Token: 0x0600F739 RID: 63289 RVA: 0x003813F2 File Offset: 0x0037F5F2
		[DefaultValue(typeof(GridFocusKeys), "Q")]
		[Description("")]
		[Category("Client")]
		[NotifyParentProperty(true)]
		public virtual GridFocusKeys CancelChangesKey
		{
			get
			{
				return this.cancelChangesKey;
			}
			set
			{
				this.cancelChangesKey = value;
			}
		}

		// Token: 0x0400468F RID: 18063
		private GridFocusKeys _focusKey = GridFocusKeys.Y;

		// Token: 0x04004690 RID: 18064
		private GridFocusKeys _initInserKey = GridFocusKeys.I;

		// Token: 0x04004691 RID: 18065
		private GridFocusKeys _rebindKey = GridFocusKeys.R;

		// Token: 0x04004692 RID: 18066
		private GridFocusKeys _expandDetailTableKey = GridFocusKeys.RightArrow;

		// Token: 0x04004693 RID: 18067
		private GridFocusKeys _collapseDetailTableKey = GridFocusKeys.LeftArrow;

		// Token: 0x04004694 RID: 18068
		private GridFocusKeys _moveUpKey = GridFocusKeys.UpArrow;

		// Token: 0x04004695 RID: 18069
		private GridFocusKeys _moveDownKey = GridFocusKeys.DownArrow;

		// Token: 0x04004696 RID: 18070
		private GridFocusKeys saveChangesKey = GridFocusKeys.U;

		// Token: 0x04004697 RID: 18071
		private GridFocusKeys cancelChangesKey = GridFocusKeys.Q;
	}
}
