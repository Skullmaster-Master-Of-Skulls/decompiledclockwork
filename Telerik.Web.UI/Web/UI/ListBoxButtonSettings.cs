using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x0200193B RID: 6459
	public sealed class ListBoxButtonSettings : ObjectWithState
	{
		// Token: 0x0600F9C9 RID: 63945 RVA: 0x003851DE File Offset: 0x003833DE
		internal ListBoxButtonSettings(StateBag ownerViewState) : base("ButtonSettings", ownerViewState)
		{
		}

		// Token: 0x17004B6C RID: 19308
		// (get) Token: 0x0600F9CA RID: 63946 RVA: 0x003851EC File Offset: 0x003833EC
		internal bool IsVertical
		{
			get
			{
				return this.Position == ListBoxButtonPosition.Left || this.Position == ListBoxButtonPosition.Right;
			}
		}

		// Token: 0x17004B6D RID: 19309
		// (get) Token: 0x0600F9CB RID: 63947 RVA: 0x00385202 File Offset: 0x00383402
		// (set) Token: 0x0600F9CC RID: 63948 RVA: 0x0038522C File Offset: 0x0038342C
		[DefaultValue(typeof(Unit), "30px")]
		[TypeConverter(typeof(UnitConverter))]
		[Description("The width of the button area")]
		public Unit AreaWidth
		{
			get
			{
				return (Unit)(base.ViewState["AreaWidth"] ?? Unit.Parse("30px"));
			}
			set
			{
				base.ViewState["AreaWidth"] = value;
			}
		}

		// Token: 0x17004B6E RID: 19310
		// (get) Token: 0x0600F9CD RID: 63949 RVA: 0x00385244 File Offset: 0x00383444
		// (set) Token: 0x0600F9CE RID: 63950 RVA: 0x0038526E File Offset: 0x0038346E
		[Description("The height of the button area")]
		[DefaultValue(typeof(Unit), "30px")]
		[TypeConverter(typeof(UnitConverter))]
		public Unit AreaHeight
		{
			get
			{
				return (Unit)(base.ViewState["AreaHeight"] ?? Unit.Parse("30px"));
			}
			set
			{
				base.ViewState["AreaHeight"] = value;
			}
		}

		// Token: 0x17004B6F RID: 19311
		// (get) Token: 0x0600F9CF RID: 63951 RVA: 0x00385286 File Offset: 0x00383486
		// (set) Token: 0x0600F9D0 RID: 63952 RVA: 0x003852A7 File Offset: 0x003834A7
		[DefaultValue(ListBoxButtonPosition.Right)]
		[Description("The position of the buttons")]
		public ListBoxButtonPosition Position
		{
			get
			{
				return (ListBoxButtonPosition)(base.ViewState["Position"] ?? ListBoxButtonPosition.Right);
			}
			set
			{
				base.ViewState["Position"] = value;
			}
		}

		// Token: 0x17004B70 RID: 19312
		// (get) Token: 0x0600F9D1 RID: 63953 RVA: 0x003852BF File Offset: 0x003834BF
		// (set) Token: 0x0600F9D2 RID: 63954 RVA: 0x003852E0 File Offset: 0x003834E0
		[DefaultValue(false)]
		[Description("When set to true enables rendering of text on the buttons")]
		public bool RenderButtonText
		{
			get
			{
				return (bool)(base.ViewState["RenderButtonText"] ?? false);
			}
			set
			{
				base.ViewState["RenderButtonText"] = value;
			}
		}

		// Token: 0x17004B71 RID: 19313
		// (get) Token: 0x0600F9D3 RID: 63955 RVA: 0x003852F8 File Offset: 0x003834F8
		// (set) Token: 0x0600F9D4 RID: 63956 RVA: 0x00385319 File Offset: 0x00383519
		[Description("The horizontal align of the buttons")]
		[DefaultValue(ListBoxHorizontalAlign.Left)]
		public ListBoxHorizontalAlign HorizontalAlign
		{
			get
			{
				return (ListBoxHorizontalAlign)(base.ViewState["HorizontalAlign"] ?? ListBoxHorizontalAlign.Left);
			}
			set
			{
				base.ViewState["HorizontalAlign"] = value;
			}
		}

		// Token: 0x17004B72 RID: 19314
		// (get) Token: 0x0600F9D5 RID: 63957 RVA: 0x00385331 File Offset: 0x00383531
		// (set) Token: 0x0600F9D6 RID: 63958 RVA: 0x00385352 File Offset: 0x00383552
		[Description("The vertical align of the buttons")]
		[DefaultValue(ListBoxVerticalAlign.Top)]
		public ListBoxVerticalAlign VerticalAlign
		{
			get
			{
				return (ListBoxVerticalAlign)(base.ViewState["VerticalAlign"] ?? ListBoxVerticalAlign.Top);
			}
			set
			{
				base.ViewState["VerticalAlign"] = value;
			}
		}

		// Token: 0x17004B73 RID: 19315
		// (get) Token: 0x0600F9D7 RID: 63959 RVA: 0x0038536A File Offset: 0x0038356A
		// (set) Token: 0x0600F9D8 RID: 63960 RVA: 0x0038538B File Offset: 0x0038358B
		[Description("Whether to show the delete button")]
		[DefaultValue(true)]
		public bool ShowDelete
		{
			get
			{
				return (bool)(base.ViewState["ShowDelete"] ?? true);
			}
			set
			{
				base.ViewState["ShowDelete"] = value;
			}
		}

		// Token: 0x17004B74 RID: 19316
		// (get) Token: 0x0600F9D9 RID: 63961 RVA: 0x003853A3 File Offset: 0x003835A3
		// (set) Token: 0x0600F9DA RID: 63962 RVA: 0x003853C4 File Offset: 0x003835C4
		[Description("Whether to show the reorder buttons")]
		[DefaultValue(true)]
		public bool ShowReorder
		{
			get
			{
				return (bool)(base.ViewState["ShowReorder"] ?? true);
			}
			set
			{
				base.ViewState["ShowReorder"] = value;
			}
		}

		// Token: 0x17004B75 RID: 19317
		// (get) Token: 0x0600F9DB RID: 63963 RVA: 0x003853DC File Offset: 0x003835DC
		// (set) Token: 0x0600F9DC RID: 63964 RVA: 0x003853FD File Offset: 0x003835FD
		[DefaultValue(true)]
		[Description("Whether to show the transfer buttons")]
		public bool ShowTransfer
		{
			get
			{
				return (bool)(base.ViewState["ShowTransfer"] ?? true);
			}
			set
			{
				base.ViewState["ShowTransfer"] = value;
			}
		}

		// Token: 0x17004B76 RID: 19318
		// (get) Token: 0x0600F9DD RID: 63965 RVA: 0x00385415 File Offset: 0x00383615
		// (set) Token: 0x0600F9DE RID: 63966 RVA: 0x00385436 File Offset: 0x00383636
		[Description("Whether to show the 'transfer all' buttons")]
		[DefaultValue(true)]
		public bool ShowTransferAll
		{
			get
			{
				return (bool)(base.ViewState["ShowTransferAll"] ?? true);
			}
			set
			{
				base.ViewState["ShowTransferAll"] = value;
			}
		}

		// Token: 0x17004B77 RID: 19319
		// (get) Token: 0x0600F9DF RID: 63967 RVA: 0x0038544E File Offset: 0x0038364E
		// (set) Token: 0x0600F9E0 RID: 63968 RVA: 0x0038546F File Offset: 0x0038366F
		[Browsable(true)]
		[Description("A value that specifies which reorder buttons should be rendered")]
		[DefaultValue(ListBoxReorderButtons.Common)]
		[Editor("Telerik.Web.Design.Common.FlagEnumUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Bindable(false)]
		public ListBoxReorderButtons ReorderButtons
		{
			get
			{
				return (ListBoxReorderButtons)(base.ViewState["ReorderButtons"] ?? ListBoxReorderButtons.Common);
			}
			set
			{
				base.ViewState["ReorderButtons"] = value;
			}
		}

		// Token: 0x17004B78 RID: 19320
		// (get) Token: 0x0600F9E1 RID: 63969 RVA: 0x00385487 File Offset: 0x00383687
		// (set) Token: 0x0600F9E2 RID: 63970 RVA: 0x003854A9 File Offset: 0x003836A9
		[Editor("Telerik.Web.Design.Common.FlagEnumUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Bindable(false)]
		[DefaultValue(ListBoxReorderButtons.All)]
		[Description("A value that specifies which transfer buttons should be rendered")]
		[Browsable(true)]
		public ListBoxTransferButtons TransferButtons
		{
			get
			{
				return (ListBoxTransferButtons)(base.ViewState["TransferButtons"] ?? ListBoxTransferButtons.All);
			}
			set
			{
				base.ViewState["TransferButtons"] = value;
			}
		}
	}
}
