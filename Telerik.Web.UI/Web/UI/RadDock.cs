using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Text;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Licensing;
using Telerik.Web.Design;
using Telerik.Web.UI.Dock;

namespace Telerik.Web.UI
{
	// Token: 0x02000FB3 RID: 4019
	[ClientScriptResource("Telerik.Web.UI.RadDock", "Telerik.Web.UI.Common.Core.js")]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[EmbeddedSkin("Dock")]
	[EmbeddedSkin("Dock", "Default")]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Lightweight, typeof(RadDock))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.MaterialRipple.css", RenderMode.Lightweight, typeof(RadDock))]
	[TelerikToolboxCategory("Container")]
	[ToolboxBitmap(typeof(RadDock), "Telerik.Web.UI.Dock.png")]
	[RequiredScript(typeof(RadDockScripts))]
	[ParseChildren(true)]
	[DefaultEvent("Command")]
	[ToolboxData("<{0}:RadDock Runat=server Width=300px></{0}:RadDock>")]
	[Designer("Telerik.Web.Design.RadDockDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[LightweightRendering]
	public class RadDock : RadWebControl, INamingContainer, IPostBackEventHandler
	{
		// Token: 0x170030D6 RID: 12502
		// (get) Token: 0x06009A56 RID: 39510 RVA: 0x00226029 File Offset: 0x00224229
		// (set) Token: 0x06009A57 RID: 39511 RVA: 0x00226054 File Offset: 0x00224254
		[DefaultValue(false)]
		[ClientControlProperty]
		[Category("Behavior")]
		[Description("Specifies whether the control will initiate postback when it is docked/undocked or its position changes.")]
		public bool AutoPostBack
		{
			get
			{
				return this.ViewState["AutoPostBack"] != null && (bool)this.ViewState["AutoPostBack"];
			}
			set
			{
				this.ViewState["AutoPostBack"] = value;
			}
		}

		// Token: 0x170030D7 RID: 12503
		// (get) Token: 0x06009A58 RID: 39512 RVA: 0x0022606C File Offset: 0x0022426C
		// (set) Token: 0x06009A59 RID: 39513 RVA: 0x00226097 File Offset: 0x00224297
		[Category("Behavior")]
		[ClientControlProperty]
		[ClientPropertyName("_closed")]
		[SimplePersistenceSetting]
		[DefaultValue(false)]
		[Description("Specifies whether the control is closed (style='display:none;').")]
		public bool Closed
		{
			get
			{
				return this.ViewState["Closed"] != null && (bool)this.ViewState["Closed"];
			}
			set
			{
				this.ViewState["Closed"] = value;
			}
		}

		// Token: 0x170030D8 RID: 12504
		// (get) Token: 0x06009A5A RID: 39514 RVA: 0x002260AF File Offset: 0x002242AF
		// (set) Token: 0x06009A5B RID: 39515 RVA: 0x002260DE File Offset: 0x002242DE
		[DefaultValue("Close")]
		[Localizable(true)]
		[Description("Specifies the tooltip of the CloseCommand when the corresponding property was not explicitly set on the command object.")]
		public string CloseText
		{
			get
			{
				if (this.ViewState["CloseText"] == null)
				{
					return "Close";
				}
				return (string)this.ViewState["CloseText"];
			}
			set
			{
				this.ViewState["CloseText"] = value;
			}
		}

		// Token: 0x170030D9 RID: 12505
		// (get) Token: 0x06009A5C RID: 39516 RVA: 0x002260F1 File Offset: 0x002242F1
		// (set) Token: 0x06009A5D RID: 39517 RVA: 0x0022611C File Offset: 0x0022431C
		[Description("Specifies whether the control is collapsed.")]
		[DefaultValue(false)]
		[ClientControlProperty]
		[Category("Behavior")]
		[SimplePersistenceSetting]
		public bool Collapsed
		{
			get
			{
				return this.ViewState["Collapsed"] != null && (bool)this.ViewState["Collapsed"];
			}
			set
			{
				this.ViewState["Collapsed"] = value;
			}
		}

		// Token: 0x170030DA RID: 12506
		// (get) Token: 0x06009A5E RID: 39518 RVA: 0x00226134 File Offset: 0x00224334
		// (set) Token: 0x06009A5F RID: 39519 RVA: 0x00226163 File Offset: 0x00224363
		[DefaultValue("Collapse")]
		[Localizable(true)]
		[Description("Specifies the tooltip of the ExpandCollapseCommand when the dock is not collapsed and the corresponding property was not explicitly set on the command object.")]
		public string CollapseText
		{
			get
			{
				if (this.ViewState["CollapseText"] == null)
				{
					return "Collapse";
				}
				return (string)this.ViewState["CollapseText"];
			}
			set
			{
				this.ViewState["CollapseText"] = value;
			}
		}

		// Token: 0x170030DB RID: 12507
		// (get) Token: 0x06009A60 RID: 39520 RVA: 0x00226176 File Offset: 0x00224376
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Description("Gets a collection of DockCommand objects representing the individual commands within the control titlebar.")]
		public DockCommandCollection Commands
		{
			get
			{
				if (this._commands == null)
				{
					this._commands = new DockCommandCollection(this);
				}
				return this._commands;
			}
		}

		// Token: 0x170030DC RID: 12508
		// (get) Token: 0x06009A61 RID: 39521 RVA: 0x00226192 File Offset: 0x00224392
		// (set) Token: 0x06009A62 RID: 39522 RVA: 0x002261BD File Offset: 0x002243BD
		[DefaultValue(false)]
		[Description("Specifies whether whether the control will initiate postback when its command items are clicked.")]
		[Category("Behavior")]
		public bool CommandsAutoPostBack
		{
			get
			{
				return this.ViewState["CommandsAutoPostBack"] != null && (bool)this.ViewState["CommandsAutoPostBack"];
			}
			set
			{
				this.ViewState["CommandsAutoPostBack"] = value;
			}
		}

		// Token: 0x170030DD RID: 12509
		// (get) Token: 0x06009A63 RID: 39523 RVA: 0x002261D5 File Offset: 0x002243D5
		[Browsable(false)]
		[Description("Gets the control, where the ContentTemplate will be instantiated in.")]
		public Panel ContentContainer
		{
			get
			{
				this.EnsureChildControls();
				return this._contentContainer;
			}
		}

		// Token: 0x170030DE RID: 12510
		// (get) Token: 0x06009A64 RID: 39524 RVA: 0x002261E3 File Offset: 0x002243E3
		// (set) Token: 0x06009A65 RID: 39525 RVA: 0x002261F6 File Offset: 0x002243F6
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Description("Specifies the System.Web.UI.ITemplate that contains the controls which will be placed in the control content area.")]
		[Browsable(false)]
		[TemplateInstance(TemplateInstance.Single)]
		[MergableProperty(false)]
		public ITemplate ContentTemplate
		{
			get
			{
				this.EnsureChildControls();
				return this._contentContainer.Template;
			}
			set
			{
				this.EnsureChildControls();
				this._contentContainer.Template = value;
			}
		}

		// Token: 0x170030DF RID: 12511
		// (get) Token: 0x06009A66 RID: 39526 RVA: 0x0022620A File Offset: 0x0022440A
		// (set) Token: 0x06009A67 RID: 39527 RVA: 0x00226235 File Offset: 0x00224435
		[DefaultValue(DefaultCommands.Close | DefaultCommands.ExpandCollapse)]
		[Category("Behavior")]
		[Description("Specifies the commands which will appear in the RadDock titlebar when the commands collection is not modified.")]
		public DefaultCommands DefaultCommands
		{
			get
			{
				if (this.ViewState["DefaultCommands"] == null)
				{
					return DefaultCommands.Close | DefaultCommands.ExpandCollapse;
				}
				return (DefaultCommands)this.ViewState["DefaultCommands"];
			}
			set
			{
				this.ViewState["DefaultCommands"] = value;
				this.Commands.Clear();
			}
		}

		// Token: 0x170030E0 RID: 12512
		// (get) Token: 0x06009A68 RID: 39528 RVA: 0x00226258 File Offset: 0x00224458
		// (set) Token: 0x06009A69 RID: 39529 RVA: 0x00226283 File Offset: 0x00224483
		[Description("Specifies the behavior of the control titlebar and grips.")]
		[Category("Behavior")]
		[DefaultValue(DockHandle.TitleBar)]
		public DockHandle DockHandle
		{
			get
			{
				if (this.ViewState["DockHandle"] == null)
				{
					return DockHandle.TitleBar;
				}
				return (DockHandle)this.ViewState["DockHandle"];
			}
			set
			{
				this.ViewState["DockHandle"] = value;
			}
		}

		// Token: 0x170030E1 RID: 12513
		// (get) Token: 0x06009A6A RID: 39530 RVA: 0x0022629B File Offset: 0x0022449B
		// (set) Token: 0x06009A6B RID: 39531 RVA: 0x002262C6 File Offset: 0x002244C6
		[Category("Behavior")]
		[ClientControlProperty]
		[DefaultValue(DockMode.Default)]
		[Description("Specifies whether the control could be left undocked.")]
		public DockMode DockMode
		{
			get
			{
				if (this.ViewState["DockMode"] == null)
				{
					return DockMode.Default;
				}
				return (DockMode)this.ViewState["DockMode"];
			}
			set
			{
				this.ViewState["DockMode"] = value;
			}
		}

		// Token: 0x170030E2 RID: 12514
		// (get) Token: 0x06009A6C RID: 39532 RVA: 0x002262DE File Offset: 0x002244DE
		// (set) Token: 0x06009A6D RID: 39533 RVA: 0x002262E8 File Offset: 0x002244E8
		[SimplePersistenceSetting]
		internal string PersistedDockZoneID
		{
			get
			{
				return this.DockZoneID;
			}
			set
			{
				try
				{
					this.DockZoneID = value;
				}
				catch
				{
				}
			}
		}

		// Token: 0x170030E3 RID: 12515
		// (get) Token: 0x06009A6E RID: 39534 RVA: 0x00226314 File Offset: 0x00224514
		// (set) Token: 0x06009A6F RID: 39535 RVA: 0x0022632F File Offset: 0x0022452F
		[ClientControlProperty]
		[Browsable(false)]
		[Description("Gets the ClientID of the RadDockZone, in which the control is docked. When the control is undocked, this property returns string.Empty.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string DockZoneID
		{
			get
			{
				if (this.DockZone != null)
				{
					return this.DockZone.ClientID;
				}
				return string.Empty;
			}
			set
			{
				this.Dock(value);
			}
		}

		// Token: 0x170030E4 RID: 12516
		// (get) Token: 0x06009A70 RID: 39536 RVA: 0x00226338 File Offset: 0x00224538
		// (set) Token: 0x06009A71 RID: 39537 RVA: 0x00226363 File Offset: 0x00224563
		[Category("Behavior")]
		[Description("Specifies whether the control will have animation.")]
		[ClientControlProperty]
		[DefaultValue(false)]
		public bool EnableAnimation
		{
			get
			{
				return this.ViewState["EnableAnimation"] != null && (bool)this.ViewState["EnableAnimation"];
			}
			set
			{
				this.ViewState["EnableAnimation"] = value;
			}
		}

		// Token: 0x170030E5 RID: 12517
		// (get) Token: 0x06009A72 RID: 39538 RVA: 0x0022637B File Offset: 0x0022457B
		// (set) Token: 0x06009A73 RID: 39539 RVA: 0x002263A6 File Offset: 0x002245A6
		[DefaultValue(true)]
		[Description("Specifies whether the control could be dragged.")]
		[Category("Behavior")]
		[ClientPropertyName("_enableDrag")]
		[ClientControlProperty]
		public bool EnableDrag
		{
			get
			{
				return this.ViewState["EnableDrag"] == null || (bool)this.ViewState["EnableDrag"];
			}
			set
			{
				this.ViewState["EnableDrag"] = value;
			}
		}

		// Token: 0x170030E6 RID: 12518
		// (get) Token: 0x06009A74 RID: 39540 RVA: 0x002263BE File Offset: 0x002245BE
		// (set) Token: 0x06009A75 RID: 39541 RVA: 0x002263DF File Offset: 0x002245DF
		[Category("Behavior")]
		[DefaultValue(false)]
		[Description("Specifies whether the control will be with rounded corners.")]
		public bool EnableRoundedCorners
		{
			get
			{
				return (bool)(this.ViewState["EnableRoundedCorners"] ?? false);
			}
			set
			{
				this.ViewState["EnableRoundedCorners"] = value;
			}
		}

		// Token: 0x170030E7 RID: 12519
		// (get) Token: 0x06009A76 RID: 39542 RVA: 0x002263F7 File Offset: 0x002245F7
		// (set) Token: 0x06009A77 RID: 39543 RVA: 0x00226426 File Offset: 0x00224626
		[DefaultValue("Expand")]
		[Localizable(true)]
		[Description("Specifies the tooltip of the ExpandCollapseCommand when the dock is collapsed and the corresponding property was not explicitly set on the command object.")]
		public string ExpandText
		{
			get
			{
				if (this.ViewState["ExpandText"] == null)
				{
					return "Expand";
				}
				return (string)this.ViewState["ExpandText"];
			}
			set
			{
				this.ViewState["ExpandText"] = value;
			}
		}

		// Token: 0x170030E8 RID: 12520
		// (get) Token: 0x06009A78 RID: 39544 RVA: 0x00226439 File Offset: 0x00224639
		// (set) Token: 0x06009A79 RID: 39545 RVA: 0x00226469 File Offset: 0x00224669
		[Description("Specifies the UniqueNames of the RadDockZone controls, where the RadDock control will not be allowed to dock.")]
		[Category("Behavior")]
		[TypeConverter(typeof(ListConverter))]
		public string[] ForbiddenZones
		{
			get
			{
				if (this.ViewState["ForbiddenZones"] == null)
				{
					return new string[0];
				}
				return (string[])this.ViewState["ForbiddenZones"];
			}
			set
			{
				this.ViewState["ForbiddenZones"] = value;
			}
		}

		// Token: 0x170030E9 RID: 12521
		// (get) Token: 0x06009A7A RID: 39546 RVA: 0x0022647C File Offset: 0x0022467C
		// (set) Token: 0x06009A7B RID: 39547 RVA: 0x002264AC File Offset: 0x002246AC
		[Category("Behavior")]
		[Description("Specifies the UniqueNames of the RadDockZone controls, where the RadDock control will be allowed to dock.")]
		[TypeConverter(typeof(ListConverter))]
		public string[] AllowedZones
		{
			get
			{
				if (this.ViewState["AllowedZones"] == null)
				{
					return new string[0];
				}
				return (string[])this.ViewState["AllowedZones"];
			}
			set
			{
				this.ViewState["AllowedZones"] = value;
			}
		}

		// Token: 0x170030EA RID: 12522
		// (get) Token: 0x06009A7C RID: 39548 RVA: 0x002264BF File Offset: 0x002246BF
		// (set) Token: 0x06009A7D RID: 39549 RVA: 0x002264C7 File Offset: 0x002246C7
		[NotifyParentProperty(true)]
		[SimplePersistenceSetting]
		[Description("Specifies the height of the RadDock control.")]
		[ClientControlProperty]
		public override Unit Height
		{
			get
			{
				return base.Height;
			}
			set
			{
				base.Height = value;
			}
		}

		// Token: 0x170030EB RID: 12523
		// (get) Token: 0x06009A7E RID: 39550 RVA: 0x002264D0 File Offset: 0x002246D0
		// (set) Token: 0x06009A7F RID: 39551 RVA: 0x002264D8 File Offset: 0x002246D8
		[SimplePersistenceSetting]
		[ClientControlProperty]
		[ClientPropertyName("_expandedHeight")]
		[DefaultValue(0)]
		[Description("Specifies the expanded height of the RadDock control.")]
		public int ExpandedHeight
		{
			get
			{
				return this._expandedHeight;
			}
			set
			{
				this._expandedHeight = value;
			}
		}

		// Token: 0x170030EC RID: 12524
		// (get) Token: 0x06009A80 RID: 39552 RVA: 0x002264E1 File Offset: 0x002246E1
		// (set) Token: 0x06009A81 RID: 39553 RVA: 0x002264F3 File Offset: 0x002246F3
		[SimplePersistenceSetting]
		[Description("Gets the position of the RadDock control in its parent zone. If undocked returns -1.")]
		[Browsable(false)]
		[ClientControlProperty]
		public int Index
		{
			get
			{
				if (this.DockZone == null)
				{
					return -1;
				}
				return this._index;
			}
			set
			{
				this._index = value;
			}
		}

		// Token: 0x170030ED RID: 12525
		// (get) Token: 0x06009A82 RID: 39554 RVA: 0x002264FC File Offset: 0x002246FC
		[ClientControlProperty]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public string LayoutID
		{
			get
			{
				if (this.Layout != null)
				{
					return (this.Layout as Control).ID;
				}
				return string.Empty;
			}
		}

		// Token: 0x170030EE RID: 12526
		// (get) Token: 0x06009A83 RID: 39555 RVA: 0x0022651C File Offset: 0x0022471C
		// (set) Token: 0x06009A84 RID: 39556 RVA: 0x0022654C File Offset: 0x0022474C
		[ClientControlProperty]
		[Description("Specifies the horizontal position of the RadDock control in pixels. This property is ignored when the RadDock control is docked into a RadDockZone.")]
		[SimplePersistenceSetting]
		[DefaultValue(typeof(Unit), "0px")]
		[Category("Appearance")]
		public Unit Left
		{
			get
			{
				if (this.ViewState["Left"] == null)
				{
					return Unit.Pixel(0);
				}
				return (Unit)this.ViewState["Left"];
			}
			set
			{
				this.ViewState["Left"] = value;
			}
		}

		// Token: 0x170030EF RID: 12527
		// (get) Token: 0x06009A85 RID: 39557 RVA: 0x00226564 File Offset: 0x00224764
		// (set) Token: 0x06009A86 RID: 39558 RVA: 0x00226593 File Offset: 0x00224793
		[ClientPropertyName("command")]
		[DefaultValue("")]
		[Description("Specifies the client-side script that executes when a RadDock Command event is raised.")]
		[ClientControlEvent]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Client-side events")]
		public string OnClientCommand
		{
			get
			{
				if (this.ViewState["OnClientCommand"] == null)
				{
					return string.Empty;
				}
				return (string)this.ViewState["OnClientCommand"];
			}
			set
			{
				this.ViewState["OnClientCommand"] = value;
			}
		}

		// Token: 0x170030F0 RID: 12528
		// (get) Token: 0x06009A87 RID: 39559 RVA: 0x002265A6 File Offset: 0x002247A6
		// (set) Token: 0x06009A88 RID: 39560 RVA: 0x002265D5 File Offset: 0x002247D5
		[Description("Specifies the client-side script that executes when a RadDock DragStart event is raised.")]
		[ClientPropertyName("dragStart")]
		[Category("Client-side events")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		public string OnClientDragStart
		{
			get
			{
				if (this.ViewState["OnClientDragStart"] == null)
				{
					return string.Empty;
				}
				return (string)this.ViewState["OnClientDragStart"];
			}
			set
			{
				this.ViewState["OnClientDragStart"] = value;
			}
		}

		// Token: 0x170030F1 RID: 12529
		// (get) Token: 0x06009A89 RID: 39561 RVA: 0x002265E8 File Offset: 0x002247E8
		// (set) Token: 0x06009A8A RID: 39562 RVA: 0x00226617 File Offset: 0x00224817
		[Category("Client-side events")]
		[ClientPropertyName("dragEnd")]
		[Description("Specifies the client-side script that executes when a RadDock DragEnd event is raised.")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		public string OnClientDragEnd
		{
			get
			{
				if (this.ViewState["OnClientDragEnd"] == null)
				{
					return string.Empty;
				}
				return (string)this.ViewState["OnClientDragEnd"];
			}
			set
			{
				this.ViewState["OnClientDragEnd"] = value;
			}
		}

		// Token: 0x170030F2 RID: 12530
		// (get) Token: 0x06009A8B RID: 39563 RVA: 0x0022662A File Offset: 0x0022482A
		// (set) Token: 0x06009A8C RID: 39564 RVA: 0x00226659 File Offset: 0x00224859
		[DefaultValue("")]
		[Description("Specifies the client-side script that executes when a RadDock Drag event is raised.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("drag")]
		[Category("Client-side events")]
		public string OnClientDrag
		{
			get
			{
				if (this.ViewState["OnClientDrag"] == null)
				{
					return string.Empty;
				}
				return (string)this.ViewState["OnClientDrag"];
			}
			set
			{
				this.ViewState["OnClientDrag"] = value;
			}
		}

		// Token: 0x170030F3 RID: 12531
		// (get) Token: 0x06009A8D RID: 39565 RVA: 0x0022666C File Offset: 0x0022486C
		// (set) Token: 0x06009A8E RID: 39566 RVA: 0x0022669B File Offset: 0x0022489B
		[ClientControlEvent]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientPropertyName("dockPositionChanged")]
		[Category("Client-side events")]
		[Description("Specifies the client-side script that executes when the RadDock control changes its position.")]
		[DefaultValue("")]
		public string OnClientDockPositionChanged
		{
			get
			{
				if (this.ViewState["OnClientDockPositionChanged"] == null)
				{
					return string.Empty;
				}
				return (string)this.ViewState["OnClientDockPositionChanged"];
			}
			set
			{
				this.ViewState["OnClientDockPositionChanged"] = value;
			}
		}

		// Token: 0x170030F4 RID: 12532
		// (get) Token: 0x06009A8F RID: 39567 RVA: 0x002266AE File Offset: 0x002248AE
		// (set) Token: 0x06009A90 RID: 39568 RVA: 0x002266DD File Offset: 0x002248DD
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("dockPositionChanging")]
		[Description("Specifies the client-side script that executes when the RadDock control is dropped onto a zone before it changes its position.")]
		[DefaultValue("")]
		public string OnClientDockPositionChanging
		{
			get
			{
				if (this.ViewState["OnClientDockPositionChanging"] == null)
				{
					return string.Empty;
				}
				return (string)this.ViewState["OnClientDockPositionChanging"];
			}
			set
			{
				this.ViewState["OnClientDockPositionChanging"] = value;
			}
		}

		// Token: 0x170030F5 RID: 12533
		// (get) Token: 0x06009A91 RID: 39569 RVA: 0x002266F0 File Offset: 0x002248F0
		// (set) Token: 0x06009A92 RID: 39570 RVA: 0x0022671F File Offset: 0x0022491F
		[ClientPropertyName("initialize")]
		[Description("Specifies the client-side script that executes after the RadDock client-side obect is initialized.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[DefaultValue("")]
		[Category("Client-side events")]
		public string OnClientInitialize
		{
			get
			{
				if (this.ViewState["OnClientInitialize"] == null)
				{
					return string.Empty;
				}
				return (string)this.ViewState["OnClientInitialize"];
			}
			set
			{
				this.ViewState["OnClientInitialize"] = value;
			}
		}

		// Token: 0x170030F6 RID: 12534
		// (get) Token: 0x06009A93 RID: 39571 RVA: 0x00226732 File Offset: 0x00224932
		// (set) Token: 0x06009A94 RID: 39572 RVA: 0x00226761 File Offset: 0x00224961
		[DefaultValue("")]
		[Description("Specifies the client-side script that executes after the RadDock client-side object is loaded.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("load")]
		[Category("Client-side events")]
		public string OnClientLoad
		{
			get
			{
				if (this.ViewState["OnClientLoad"] == null)
				{
					return string.Empty;
				}
				return (string)this.ViewState["OnClientLoad"];
			}
			set
			{
				this.ViewState["OnClientLoad"] = value;
			}
		}

		// Token: 0x170030F7 RID: 12535
		// (get) Token: 0x06009A95 RID: 39573 RVA: 0x00226774 File Offset: 0x00224974
		// (set) Token: 0x06009A96 RID: 39574 RVA: 0x002267A3 File Offset: 0x002249A3
		[DefaultValue("")]
		[Category("Client-side events")]
		[ClientPropertyName("resizeStart")]
		[Description("Specifies the client-side script that executes when a RadDock ResizeStart event is raised.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		public string OnClientResizeStart
		{
			get
			{
				if (this.ViewState["OnClientResizeStart"] == null)
				{
					return string.Empty;
				}
				return (string)this.ViewState["OnClientResizeStart"];
			}
			set
			{
				this.ViewState["OnClientResizeStart"] = value;
			}
		}

		// Token: 0x170030F8 RID: 12536
		// (get) Token: 0x06009A97 RID: 39575 RVA: 0x002267B6 File Offset: 0x002249B6
		// (set) Token: 0x06009A98 RID: 39576 RVA: 0x002267E5 File Offset: 0x002249E5
		[ClientControlEvent]
		[ClientPropertyName("resizeEnd")]
		[Category("Client-side events")]
		[Description("Specifies the client-side script that executes when a RadDock ResizeEnd event is raised.")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public string OnClientResizeEnd
		{
			get
			{
				if (this.ViewState["OnClientResizeEnd"] == null)
				{
					return string.Empty;
				}
				return (string)this.ViewState["OnClientResizeEnd"];
			}
			set
			{
				this.ViewState["OnClientResizeEnd"] = value;
			}
		}

		// Token: 0x170030F9 RID: 12537
		// (get) Token: 0x06009A99 RID: 39577 RVA: 0x002267F8 File Offset: 0x002249F8
		// (set) Token: 0x06009A9A RID: 39578 RVA: 0x00226823 File Offset: 0x00224A23
		[Category("Behavior")]
		[ClientControlProperty]
		[DefaultValue(false)]
		[Description("Specifies whether the control is resizable.")]
		[ClientPropertyName("_resizable")]
		public bool Resizable
		{
			get
			{
				return this.ViewState["Resizable"] != null && (bool)this.ViewState["Resizable"];
			}
			set
			{
				this.ViewState["Resizable"] = value;
			}
		}

		// Token: 0x170030FA RID: 12538
		// (get) Token: 0x06009A9B RID: 39579 RVA: 0x0022683B File Offset: 0x00224A3B
		// (set) Token: 0x06009A9C RID: 39580 RVA: 0x00226866 File Offset: 0x00224A66
		[SimplePersistenceSetting]
		[ClientControlProperty]
		[DefaultValue(false)]
		[Category("Behavior")]
		[Description("Specifies whether the control is pinned.")]
		public bool Pinned
		{
			get
			{
				return this.ViewState["Pinned"] != null && (bool)this.ViewState["Pinned"];
			}
			set
			{
				this.ViewState["Pinned"] = value;
			}
		}

		// Token: 0x170030FB RID: 12539
		// (get) Token: 0x06009A9D RID: 39581 RVA: 0x0022687E File Offset: 0x00224A7E
		// (set) Token: 0x06009A9E RID: 39582 RVA: 0x002268AD File Offset: 0x00224AAD
		[Localizable(true)]
		[DefaultValue("Pin")]
		[Description("Specifies the tooltip of the PinUnpinCommand when the dock is not pinned and the corresponding property was not explicitly set on the command object.")]
		public string PinText
		{
			get
			{
				if (this.ViewState["PinText"] == null)
				{
					return "Pin";
				}
				return (string)this.ViewState["PinText"];
			}
			set
			{
				this.ViewState["PinText"] = value;
			}
		}

		// Token: 0x170030FC RID: 12540
		// (get) Token: 0x06009A9F RID: 39583 RVA: 0x002268C0 File Offset: 0x00224AC0
		// (set) Token: 0x06009AA0 RID: 39584 RVA: 0x002268C8 File Offset: 0x00224AC8
		[Description("Specifies the additional data, which could be saved in the DockState.")]
		[Category("Misc")]
		[ClientControlProperty]
		[DefaultValue("")]
		public string Tag
		{
			get
			{
				return this._tag;
			}
			set
			{
				this._tag = value;
			}
		}

		// Token: 0x170030FD RID: 12541
		// (get) Token: 0x06009AA1 RID: 39585 RVA: 0x002268D1 File Offset: 0x00224AD1
		// (set) Token: 0x06009AA2 RID: 39586 RVA: 0x00226900 File Offset: 0x00224B00
		[Description("Specifies the text which will appear in the control content area. If the ContentTemplate or the ContentContainer contain any controls, the value of this property is ignored.")]
		[Category("Appearance")]
		[DefaultValue("")]
		public string Text
		{
			get
			{
				if (this.ViewState["Text"] == null)
				{
					return string.Empty;
				}
				return (string)this.ViewState["Text"];
			}
			set
			{
				this.ViewState["Text"] = value;
			}
		}

		// Token: 0x170030FE RID: 12542
		// (get) Token: 0x06009AA3 RID: 39587 RVA: 0x00226913 File Offset: 0x00224B13
		// (set) Token: 0x06009AA4 RID: 39588 RVA: 0x00226942 File Offset: 0x00224B42
		[Description("Specifies the text which will appear in the control titlebar area. If the TitlebarTemplate or the TitlebarContainer contain any controls, the value of this property is ignored.")]
		[ClientControlProperty]
		[DefaultValue("")]
		[Category("Appearance")]
		public string Title
		{
			get
			{
				if (this.ViewState["Title"] == null)
				{
					return string.Empty;
				}
				return (string)this.ViewState["Title"];
			}
			set
			{
				this.ViewState["Title"] = value;
			}
		}

		// Token: 0x170030FF RID: 12543
		// (get) Token: 0x06009AA5 RID: 39589 RVA: 0x00226955 File Offset: 0x00224B55
		[Browsable(false)]
		[Description("Gets the control, in which the TitlebarTemplate will be instantiated.")]
		public Panel TitlebarContainer
		{
			get
			{
				this.EnsureChildControls();
				return this._titlebarContainer;
			}
		}

		// Token: 0x17003100 RID: 12544
		// (get) Token: 0x06009AA6 RID: 39590 RVA: 0x00226963 File Offset: 0x00224B63
		// (set) Token: 0x06009AA7 RID: 39591 RVA: 0x00226976 File Offset: 0x00224B76
		[MergableProperty(false)]
		[Description("Specifies the System.Web.UI.ITemplate that contains the controls which will be placed in the control titlebar.")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(false)]
		[TemplateInstance(TemplateInstance.Single)]
		public ITemplate TitlebarTemplate
		{
			get
			{
				this.EnsureChildControls();
				return this._titlebarContainer.Template;
			}
			set
			{
				this.EnsureChildControls();
				this._titlebarContainer.Template = value;
			}
		}

		// Token: 0x17003101 RID: 12545
		// (get) Token: 0x06009AA8 RID: 39592 RVA: 0x0022698A File Offset: 0x00224B8A
		// (set) Token: 0x06009AA9 RID: 39593 RVA: 0x002269BA File Offset: 0x00224BBA
		[DefaultValue(typeof(Unit), "0px")]
		[ClientControlProperty]
		[Description("Specifies the vertical position of the RadDock control in pixels. This property is ignored when the RadDock control is docked into a RadDockZone.")]
		[SimplePersistenceSetting]
		[Category("Appearance")]
		public Unit Top
		{
			get
			{
				if (this.ViewState["Top"] == null)
				{
					return Unit.Pixel(0);
				}
				return (Unit)this.ViewState["Top"];
			}
			set
			{
				this.ViewState["Top"] = value;
			}
		}

		// Token: 0x17003102 RID: 12546
		// (get) Token: 0x06009AAA RID: 39594 RVA: 0x002269D2 File Offset: 0x00224BD2
		// (set) Token: 0x06009AAB RID: 39595 RVA: 0x00226A01 File Offset: 0x00224C01
		[ClientControlProperty]
		[Category("Behavior")]
		[Description("Specifies the unique name of the control, which allows the parent RadDockLayout to automatically manage its position. If this property is not set, the control ID will be used instead.")]
		[DefaultValue("")]
		public string UniqueName
		{
			get
			{
				if (this.ViewState["UniqueName"] == null)
				{
					return string.Empty;
				}
				return (string)this.ViewState["UniqueName"];
			}
			set
			{
				this.ViewState["UniqueName"] = value;
			}
		}

		// Token: 0x17003103 RID: 12547
		// (get) Token: 0x06009AAC RID: 39596 RVA: 0x00226A14 File Offset: 0x00224C14
		// (set) Token: 0x06009AAD RID: 39597 RVA: 0x00226A43 File Offset: 0x00224C43
		[Description("Specifies the tooltip of the PinUnpinCommand when the dock is pinned and the corresponding property was not explicitly set on the command object.")]
		[Localizable(true)]
		[DefaultValue("Unpin")]
		public string UnpinText
		{
			get
			{
				if (this.ViewState["UnpinText"] == null)
				{
					return "Unpin";
				}
				return (string)this.ViewState["UnpinText"];
			}
			set
			{
				this.ViewState["UnpinText"] = value;
			}
		}

		// Token: 0x17003104 RID: 12548
		// (get) Token: 0x06009AAE RID: 39598 RVA: 0x00226A56 File Offset: 0x00224C56
		// (set) Token: 0x06009AAF RID: 39599 RVA: 0x00226A5E File Offset: 0x00224C5E
		[SimplePersistenceSetting]
		[DefaultValue(typeof(Unit), "300px")]
		[ClientControlProperty]
		[Description("Specifies the width of the RadDock control.")]
		[NotifyParentProperty(true)]
		public override Unit Width
		{
			get
			{
				return base.Width;
			}
			set
			{
				base.Width = value;
			}
		}

		// Token: 0x14000170 RID: 368
		// (add) Token: 0x06009AB0 RID: 39600 RVA: 0x00226A67 File Offset: 0x00224C67
		// (remove) Token: 0x06009AB1 RID: 39601 RVA: 0x00226A7A File Offset: 0x00224C7A
		public event DockPositionChangedEventHandler DockPositionChanged
		{
			add
			{
				base.Events.AddHandler(RadDock.DockPositionChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadDock.DockPositionChangedEvent, value);
			}
		}

		// Token: 0x06009AB2 RID: 39602 RVA: 0x00226A90 File Offset: 0x00224C90
		protected virtual void OnDockPositionChanged(DockPositionChangedEventArgs e)
		{
			DockPositionChangedEventHandler dockPositionChangedEventHandler = (DockPositionChangedEventHandler)base.Events[RadDock.DockPositionChangedEvent];
			if (dockPositionChangedEventHandler != null)
			{
				dockPositionChangedEventHandler(this, e);
			}
		}

		// Token: 0x14000171 RID: 369
		// (add) Token: 0x06009AB3 RID: 39603 RVA: 0x00226ABE File Offset: 0x00224CBE
		// (remove) Token: 0x06009AB4 RID: 39604 RVA: 0x00226AD1 File Offset: 0x00224CD1
		public event DockCommandEventHandler Command
		{
			add
			{
				base.Events.AddHandler(RadDock.CommandEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadDock.CommandEvent, value);
			}
		}

		// Token: 0x06009AB5 RID: 39605 RVA: 0x00226AE4 File Offset: 0x00224CE4
		protected virtual void OnCommand(DockCommandEventArgs e)
		{
			DockCommandEventHandler dockCommandEventHandler = (DockCommandEventHandler)base.Events[RadDock.CommandEvent];
			if (dockCommandEventHandler != null)
			{
				dockCommandEventHandler(this, e);
			}
		}

		// Token: 0x06009AB6 RID: 39606 RVA: 0x00226B12 File Offset: 0x00224D12
		public void Dock(string dockZoneID)
		{
			if (this.Layout == null)
			{
				throw new InvalidOperationException(string.Format("{0} with ID='{1}' is not placed inside RadDockLayout. Please put the {0} control inside a RadDockLayout control, or use Dock(RadDockZone) method to dock the control in a zone.", base.GetType().FullName, this.ID));
			}
			this.Layout.SetDockParent(this, dockZoneID);
		}

		// Token: 0x06009AB7 RID: 39607 RVA: 0x00226B4A File Offset: 0x00224D4A
		public void Dock(RadDockZone dockZone)
		{
			if (dockZone == null)
			{
				throw new ArgumentNullException("zone");
			}
			dockZone.Docks.Add(this);
		}

		// Token: 0x06009AB8 RID: 39608 RVA: 0x00226B66 File Offset: 0x00224D66
		public void Undock()
		{
			if (this.DockZone != null)
			{
				this.DockZone.Docks.Remove(this);
			}
		}

		// Token: 0x17003105 RID: 12549
		// (get) Token: 0x06009AB9 RID: 39609 RVA: 0x00226B82 File Offset: 0x00224D82
		// (set) Token: 0x06009ABA RID: 39610 RVA: 0x00226B8A File Offset: 0x00224D8A
		internal RadDockZone DockZone
		{
			get
			{
				return this._dockZone;
			}
			set
			{
				this._dockZone = value;
			}
		}

		// Token: 0x06009ABB RID: 39611 RVA: 0x00226B94 File Offset: 0x00224D94
		public string GetUniqueName()
		{
			string text = this.UniqueName;
			if (string.IsNullOrEmpty(text))
			{
				text = this.ID;
			}
			return text;
		}

		// Token: 0x06009ABC RID: 39612 RVA: 0x00226BB8 File Offset: 0x00224DB8
		public DockState GetState()
		{
			return new DockState
			{
				UniqueName = this.GetUniqueName(),
				DockZoneID = this.DockZoneID,
				Width = this.Width,
				Height = this.Height,
				ExpandedHeight = this.ExpandedHeight,
				Top = this.Top,
				Left = this.Left,
				Resizable = this.Resizable,
				Closed = this.Closed,
				Collapsed = this.Collapsed,
				Pinned = this.Pinned,
				Title = this.Title,
				Text = this.Text,
				Tag = this.Tag,
				Index = this.Index
			};
		}

		// Token: 0x06009ABD RID: 39613 RVA: 0x00226C80 File Offset: 0x00224E80
		public void ApplyState(DockState state)
		{
			this.UniqueName = state.UniqueName;
			this.Width = state.Width;
			this.Height = state.Height;
			this.ExpandedHeight = state.ExpandedHeight;
			this.Top = state.Top;
			this.Left = state.Left;
			this.Resizable = state.Resizable;
			this.Closed = state.Closed;
			this.Collapsed = state.Collapsed;
			this.Pinned = state.Pinned;
			this.Title = state.Title;
			this.Text = state.Text;
			this.Tag = state.Tag;
			this.Index = state.Index;
			if (this._initialized)
			{
				this.DockZoneID = state.DockZoneID;
				return;
			}
			this._delayedDockZoneID = state.DockZoneID;
		}

		// Token: 0x06009ABE RID: 39614 RVA: 0x00226D56 File Offset: 0x00224F56
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			if (this.Layout != null)
			{
				this.Layout.RegisterDock(this);
				if (this._delayedDockZoneID != null)
				{
					this.DockZoneID = this._delayedDockZoneID;
				}
			}
			this._initialized = true;
		}

		// Token: 0x17003106 RID: 12550
		// (get) Token: 0x06009ABF RID: 39615 RVA: 0x00226D90 File Offset: 0x00224F90
		private IDockLayout Layout
		{
			get
			{
				if (this._layout == null)
				{
					for (Control parent = this.Parent; parent != null; parent = parent.Parent)
					{
						IDockLayout dockLayout = parent as IDockLayout;
						if (dockLayout != null)
						{
							this._layout = dockLayout;
							break;
						}
					}
				}
				return this._layout;
			}
		}

		// Token: 0x17003107 RID: 12551
		// (get) Token: 0x06009AC0 RID: 39616 RVA: 0x00226DD1 File Offset: 0x00224FD1
		private bool ShowGrip
		{
			get
			{
				return (this.DockHandle & DockHandle.Grip) > DockHandle.None;
			}
		}

		// Token: 0x17003108 RID: 12552
		// (get) Token: 0x06009AC1 RID: 39617 RVA: 0x00226DDE File Offset: 0x00224FDE
		private bool ShowTitleBar
		{
			get
			{
				return (this.DockHandle & DockHandle.TitleBar) > DockHandle.None;
			}
		}

		// Token: 0x06009AC2 RID: 39618 RVA: 0x00226DEC File Offset: 0x00224FEC
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			this._titlebarContainer = new SingleTemplateContainer(this);
			this._titlebarContainer.CssClass = "rdTitleBar";
			this._titlebarContainer.ID = "T";
			this.Controls.Add(this._titlebarContainer);
			this._contentContainer = new SingleTemplateContainer(this);
			this._contentContainer.CssClass = "rdContent";
			this._contentContainer.ID = "C";
			this.Controls.Add(this._contentContainer);
		}

		// Token: 0x06009AC3 RID: 39619 RVA: 0x00226E7C File Offset: 0x0022507C
		protected override void LoadClientState(Dictionary<string, object> clientState)
		{
			base.LoadClientState(clientState);
			this.Resizable = (bool)clientState["Resizable"];
			this.Closed = (bool)clientState["Closed"];
			this.Collapsed = (bool)clientState["Collapsed"];
			this.Pinned = (bool)clientState["Pinned"];
			if (clientState["Top"] != null)
			{
				this.Top = Unit.Parse(clientState["Top"].ToString(), CultureInfo.InvariantCulture);
			}
			if (clientState["Left"] != null)
			{
				this.Left = Unit.Parse(clientState["Left"].ToString(), CultureInfo.InvariantCulture);
			}
			if (clientState["Width"] != null)
			{
				this.Width = Unit.Parse(this.ConvertToInvariantString(clientState["Width"]), CultureInfo.InvariantCulture);
			}
			if (clientState["Height"] != null)
			{
				this.Height = Unit.Parse(this.ConvertToInvariantString(clientState["Height"]), CultureInfo.InvariantCulture);
			}
			this.ExpandedHeight = int.Parse(clientState["ExpandedHeight"].ToString());
			int index = this.Index;
			int num = (int)clientState["Index"];
			string dockZoneID = this.DockZoneID;
			string text = (string)clientState["DockZoneID"];
			if (dockZoneID != text || index != num)
			{
				bool isDragged = (bool)clientState["IsDragged"];
				this.OnDockPositionChanged(new DockPositionChangedEventArgs(text, num, isDragged));
			}
		}

		// Token: 0x06009AC4 RID: 39620 RVA: 0x00227018 File Offset: 0x00225218
		private string ConvertToInvariantString(object value)
		{
			if (value != null)
			{
				TypeConverter typeConverter = new TypeConverter();
				return typeConverter.ConvertToInvariantString(value);
			}
			return null;
		}

		// Token: 0x06009AC5 RID: 39621 RVA: 0x00227037 File Offset: 0x00225237
		protected override void ControlPreRender()
		{
			base.ControlPreRender();
			this.InitializeDefaultTitlebarContentAndCommands();
		}

		// Token: 0x06009AC6 RID: 39622 RVA: 0x00227045 File Offset: 0x00225245
		protected override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
		{
			if (this.DockZone != null)
			{
				return new List<ScriptDescriptor>();
			}
			return base.GetScriptDescriptors();
		}

		// Token: 0x06009AC7 RID: 39623 RVA: 0x0022705B File Offset: 0x0022525B
		internal IEnumerable<ScriptDescriptor> GetDockScriptDescriptors()
		{
			return base.GetScriptDescriptors();
		}

		// Token: 0x06009AC8 RID: 39624 RVA: 0x00227064 File Offset: 0x00225264
		internal void InitializeDefaultTitlebarContentAndCommands()
		{
			if (this.TitlebarTemplate == null && !this.TitlebarContainer.HasControls())
			{
				HtmlGenericControl htmlGenericControl = new HtmlGenericControl((this.ResolvedRenderMode == RenderMode.Classic) ? "em" : "h6");
				if (this.ResolvedRenderMode == RenderMode.Lightweight)
				{
					htmlGenericControl.Attributes["class"] = "rdTitle";
				}
				htmlGenericControl.InnerHtml = this.Title;
				this.TitlebarContainer.Controls.Add(htmlGenericControl);
			}
			if (this.ContentTemplate == null && !this.ContentContainer.HasControls() && !string.IsNullOrEmpty(this.Text))
			{
				if (this.ResolvedRenderMode == RenderMode.Classic)
				{
					HtmlGenericControl htmlGenericControl2 = new HtmlGenericControl("span");
					htmlGenericControl2.InnerHtml = this.Text;
					this.ContentContainer.Controls.Add(htmlGenericControl2);
				}
				else
				{
					LiteralControl child = new LiteralControl(this.Text);
					this.ContentContainer.Controls.Add(child);
				}
			}
			if (this.Commands.Count == 0)
			{
				this.AddDefaultCommands();
			}
			this.Commands.Reverse();
			this.ApplyCommandsAutoPostBack();
			this.CreateCommands();
		}

		// Token: 0x06009AC9 RID: 39625 RVA: 0x00227178 File Offset: 0x00225378
		internal void RegisterScriptControlAndCssReferences()
		{
			this.RegisterScriptControl();
			this.RegisterCssReferences();
		}

		// Token: 0x17003109 RID: 12553
		// (get) Token: 0x06009ACA RID: 39626 RVA: 0x00227188 File Offset: 0x00225388
		internal bool IsInInvisibleParent
		{
			get
			{
				for (Control parent = this.Parent; parent != null; parent = parent.Parent)
				{
					if (!parent.Visible)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x06009ACB RID: 39627 RVA: 0x002271B3 File Offset: 0x002253B3
		protected override void RenderContents(HtmlTextWriter writer)
		{
			if (base.DesignMode)
			{
				writer.Write(SkinRegistrar.GetDesignTimeStyleSheet(this));
			}
			base.ApplyConditionalRendering(writer);
			base.RenderContents(writer);
		}

		// Token: 0x06009ACC RID: 39628 RVA: 0x002271D8 File Offset: 0x002253D8
		protected override void RenderChildren(HtmlTextWriter writer)
		{
			if (this.HasControls())
			{
				foreach (object obj in this.Controls)
				{
					Control control = (Control)obj;
					if (!object.Equals(control, this._contentContainer) && !object.Equals(control, this._titlebarContainer))
					{
						control.RenderControl(writer);
					}
				}
			}
		}

		// Token: 0x06009ACD RID: 39629 RVA: 0x00227258 File Offset: 0x00225458
		private void RenderTableRow(HtmlTextWriter writer, string rowPosition, Control templateContainerControl)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, this.GetRowCssClass(rowPosition));
			writer.RenderBeginTag(HtmlTextWriterTag.Tr);
			this.RenderCornerCell(writer, "Left");
			if (rowPosition == "Top")
			{
				this.RenderTitleCell(writer);
			}
			else
			{
				this.RenderContentCell(writer, templateContainerControl);
			}
			this.RenderCornerCell(writer, "Right");
			writer.RenderEndTag();
		}

		// Token: 0x06009ACE RID: 39630 RVA: 0x002272B8 File Offset: 0x002254B8
		private void RenderCornerCell(HtmlTextWriter writer, string cellPosition)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, string.Format("rd{0}{1}", cellPosition, this.EnableRoundedCorners ? " rdRoundedCorner" : string.Empty));
			writer.RenderBeginTag(HtmlTextWriterTag.Td);
			writer.Write("&nbsp;");
			writer.RenderEndTag();
		}

		// Token: 0x06009ACF RID: 39631 RVA: 0x00227305 File Offset: 0x00225505
		private void RenderContentCell(HtmlTextWriter writer, Control templateContainerControl)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rdCenter");
			writer.RenderBeginTag(HtmlTextWriterTag.Td);
			if (!object.Equals(null, templateContainerControl))
			{
				templateContainerControl.RenderControl(writer);
			}
			else
			{
				writer.Write("&nbsp;");
			}
			writer.RenderEndTag();
		}

		// Token: 0x06009AD0 RID: 39632 RVA: 0x00227340 File Offset: 0x00225540
		private void RenderTitleCell(HtmlTextWriter writer)
		{
			if (this.ShowGrip)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Id, string.Format("{0}_G", this.ClientID));
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, string.Format("rdCenter{0}", this.ShowGrip ? " rdDraggable" : ""));
			writer.RenderBeginTag(HtmlTextWriterTag.Td);
			if (this.ShowTitleBar)
			{
				this.TitlebarContainer.RenderControl(writer);
			}
			else if (this.ShowGrip)
			{
				writer.Write("&nbsp;");
			}
			writer.RenderEndTag();
		}

		// Token: 0x06009AD1 RID: 39633 RVA: 0x002273CC File Offset: 0x002255CC
		private void RenderWrapperTable(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rdTable");
			writer.AddAttribute("summary", "This is a layout table for the RadDock control. Its sole purpose is to provide a structure to the content and the option to make it resizable x-browser.");
			writer.RenderBeginTag(HtmlTextWriterTag.Table);
			writer.RenderBeginTag(HtmlTextWriterTag.Tbody);
			this.RenderTableRow(writer, "Top", this.TitlebarContainer);
			this.RenderTableRow(writer, "Middle", this.ContentContainer);
			this.RenderTableRow(writer, "Bottom", null);
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x06009AD2 RID: 39634 RVA: 0x00227443 File Offset: 0x00225643
		protected override void RenderClassic(HtmlTextWriter writer)
		{
			this.RenderWrapperTable(writer);
			base.RenderClassic(writer);
		}

		// Token: 0x06009AD3 RID: 39635 RVA: 0x00227453 File Offset: 0x00225653
		protected override void RenderLite(HtmlTextWriter writer)
		{
			this.RenderLightweightTitle(writer);
			this.RenderLightweightContent(writer);
			base.RenderLite(writer);
		}

		// Token: 0x06009AD4 RID: 39636 RVA: 0x0022746C File Offset: 0x0022566C
		private void RenderLightweightTitle(HtmlTextWriter writer)
		{
			if (this.ShowGrip)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Id, string.Format("{0}_G", this.ClientID));
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, string.Format("{0}", "rdTitleWrapper"));
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			if (this.ShowTitleBar)
			{
				this.TitlebarContainer.RenderControl(writer);
			}
			else if (this.ShowGrip)
			{
				writer.Write("&nbsp;");
			}
			writer.RenderEndTag();
		}

		// Token: 0x06009AD5 RID: 39637 RVA: 0x002274E8 File Offset: 0x002256E8
		private string GetRowCssClass(string rowPosition)
		{
			string arg = string.Empty;
			if (rowPosition == "Top")
			{
				arg = this.GetExtraTopCssClass();
			}
			return string.Format("rd{0}{1}", rowPosition, arg);
		}

		// Token: 0x06009AD6 RID: 39638 RVA: 0x0022751C File Offset: 0x0022571C
		private string GetExtraTopCssClass()
		{
			string result = string.Empty;
			if (this.ShowGrip)
			{
				result = " rdGripTop";
			}
			else if (!this.ShowTitleBar)
			{
				result = " rdNone";
			}
			return result;
		}

		// Token: 0x06009AD7 RID: 39639 RVA: 0x0022754E File Offset: 0x0022574E
		private void RenderLightweightContent(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rdContentWrapper");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			this.ContentContainer.RenderControl(writer);
			writer.RenderEndTag();
		}

		// Token: 0x1700310A RID: 12554
		// (get) Token: 0x06009AD8 RID: 39640 RVA: 0x00227577 File Offset: 0x00225777
		protected internal override bool SupportsRenderingMode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06009AD9 RID: 39641 RVA: 0x0022757C File Offset: 0x0022577C
		protected override void Render(HtmlTextWriter writer)
		{
			if (this.PreventRender && (this.DockMode & DockMode.Docked) == (DockMode)0)
			{
				throw new InvalidOperationException(string.Format("{0} with ID='{1}' is docked into a {2} with ID='{3}', but it is not allowed to dock. Please, set the DockMode property to Docked, or undock the {0} control.", new object[]
				{
					base.GetType().FullName,
					this.ID,
					this.DockZone.GetType().FullName,
					this.DockZone.ID
				}));
			}
			if (this.ForceRender || !this.PreventRender)
			{
				base.Render(writer);
			}
		}

		// Token: 0x06009ADA RID: 39642 RVA: 0x00227602 File Offset: 0x00225802
		internal void RenderControlAlways(HtmlTextWriter writer)
		{
			this.ForceRender = true;
			this.RenderControl(writer);
			this.ForceRender = false;
		}

		// Token: 0x1700310B RID: 12555
		// (get) Token: 0x06009ADB RID: 39643 RVA: 0x00227619 File Offset: 0x00225819
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x06009ADC RID: 39644 RVA: 0x00227620 File Offset: 0x00225820
		protected virtual void AddStyleAttributes(HtmlTextWriter writer)
		{
			if (this.Closed)
			{
				base.Style.Add(HtmlTextWriterStyle.Display, "none");
			}
			if (string.IsNullOrEmpty(this.DockZoneID))
			{
				string value = this.Pinned ? "fixed" : "absolute";
				base.Style.Add(HtmlTextWriterStyle.Position, value);
				base.Style.Add(HtmlTextWriterStyle.Top, this.Top.ToString(CultureInfo.InvariantCulture));
				base.Style.Add(HtmlTextWriterStyle.Left, this.Left.ToString(CultureInfo.InvariantCulture));
				base.Style.Add(HtmlTextWriterStyle.Visibility, "hidden");
			}
		}

		// Token: 0x06009ADD RID: 39645 RVA: 0x002276CC File Offset: 0x002258CC
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			Unit height = base.ControlStyle.Height;
			Unit width = base.ControlStyle.Width;
			if (this.DockZone != null && this.DockZone.FitDocks && this.DockZone.Orientation != Orientation.Horizontal)
			{
				this.Width = Unit.Percentage(100.0);
			}
			if (this.Collapsed)
			{
				this.Height = Unit.Empty;
			}
			string accessKey = this.AccessKey;
			this.AccessKey = string.Empty;
			short tabIndex = this.TabIndex;
			this.TabIndex = 0;
			this.AddStyleAttributes(writer);
			base.AddAttributesToRender(writer);
			this.Height = height;
			this.Width = width;
			this.AccessKey = accessKey;
			this.TabIndex = tabIndex;
		}

		// Token: 0x06009ADE RID: 39646 RVA: 0x00227784 File Offset: 0x00225984
		protected override Style CreateControlStyle()
		{
			Style style = base.CreateControlStyle();
			style.Width = Unit.Pixel(300);
			return style;
		}

		// Token: 0x1700310C RID: 12556
		// (get) Token: 0x06009ADF RID: 39647 RVA: 0x002277AC File Offset: 0x002259AC
		protected override string CssClassFormatString
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder("RadDock RadDock_{0}");
				if (this.Collapsed)
				{
					stringBuilder.Append(" rdCollapsed");
				}
				if (this.ResolvedRenderMode == RenderMode.Lightweight)
				{
					stringBuilder.Append(this.GetExtraTopCssClass());
					if (this.EnableRoundedCorners)
					{
						stringBuilder.Append(" rdRoundedCorner");
					}
				}
				else if (this.ResolvedRenderMode == RenderMode.Classic && this.EnableRoundedCorners)
				{
					stringBuilder.Append(" rdRounded");
				}
				if (this.Resizable)
				{
					stringBuilder.Append(" rdResizable");
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x06009AE0 RID: 39648 RVA: 0x0022783C File Offset: 0x00225A3C
		private void AddDefaultCommands()
		{
			if ((this.DefaultCommands & DefaultCommands.Close) > DefaultCommands.None)
			{
				this.Commands.Add(new DockCloseCommand());
			}
			if ((this.DefaultCommands & DefaultCommands.ExpandCollapse) > DefaultCommands.None)
			{
				this.Commands.Add(new DockExpandCollapseCommand());
			}
			if ((this.DefaultCommands & DefaultCommands.PinUnpin) > DefaultCommands.None)
			{
				this.Commands.Add(new DockPinUnpinCommand());
			}
		}

		// Token: 0x06009AE1 RID: 39649 RVA: 0x0022789C File Offset: 0x00225A9C
		private void CreateCommands()
		{
			if (this.Commands.Count <= 0)
			{
				return;
			}
			HtmlGenericControl htmlGenericControl = new HtmlGenericControl("ul");
			htmlGenericControl.Attributes["class"] = "rdCommands";
			foreach (DockCommand dockCommand in this.Commands)
			{
				htmlGenericControl.Controls.Add(dockCommand.CreateElement());
			}
			this.TitlebarContainer.Controls.Add(htmlGenericControl);
		}

		// Token: 0x06009AE2 RID: 39650 RVA: 0x0022793C File Offset: 0x00225B3C
		private void ApplyCommandsAutoPostBack()
		{
			if (this.CommandsAutoPostBack)
			{
				foreach (DockCommand dockCommand in this.Commands)
				{
					dockCommand.AutoPostBack = true;
				}
			}
		}

		// Token: 0x06009AE3 RID: 39651 RVA: 0x00227998 File Offset: 0x00225B98
		private string SerializeShortCuts()
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = false;
			stringBuilder.Append("[");
			foreach (DockCommand dockCommand in this.Commands)
			{
				if (!string.IsNullOrEmpty(dockCommand.ShortCut))
				{
					if (flag)
					{
						stringBuilder.Append(",");
					}
					stringBuilder.Append(string.Format("[\"{0}\",\"{1}\"]", dockCommand.Name, dockCommand.ShortCut));
					flag = true;
				}
			}
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x06009AE4 RID: 39652 RVA: 0x00227A48 File Offset: 0x00225C48
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			base.DescribeRenderMode(descriptor);
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			descriptor.AddProperty("skin", base.RuntimeSkin);
			descriptor.AddProperty("uniqueID", this.UniqueID);
			descriptor.AddProperty("_tabIndex", this.TabIndex);
			descriptor.AddProperty("_accessKey", this.AccessKey);
			descriptor.AddScriptProperty("forbiddenZones", javaScriptSerializer.Serialize(this.ForbiddenZones));
			descriptor.AddScriptProperty("allowedZones", javaScriptSerializer.Serialize(this.AllowedZones));
			descriptor.AddScriptProperty("shortcuts", this.SerializeShortCuts());
			if (this.ShowTitleBar)
			{
				string script = this.Commands.Serialize(javaScriptSerializer);
				descriptor.AddScriptProperty("commands", script);
			}
			if (this.DockZone != null)
			{
				descriptor.AddComponentProperty("dockZone", this.DockZone.ClientID);
			}
		}

		// Token: 0x06009AE5 RID: 39653 RVA: 0x00227B30 File Offset: 0x00225D30
		protected override void OnUnload(EventArgs e)
		{
			if (this.Layout != null)
			{
				this.Layout.UnRegisterDock(this);
			}
			base.OnUnload(e);
		}

		// Token: 0x1700310D RID: 12557
		// (get) Token: 0x06009AE6 RID: 39654 RVA: 0x00227B4D File Offset: 0x00225D4D
		private bool PreventRender
		{
			get
			{
				return this.DockZone != null;
			}
		}

		// Token: 0x06009AE7 RID: 39655 RVA: 0x00227B5B File Offset: 0x00225D5B
		void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
		{
			this.RaisePostBackEvent(eventArgument);
		}

		// Token: 0x06009AE8 RID: 39656 RVA: 0x00227B80 File Offset: 0x00225D80
		protected virtual void RaisePostBackEvent(string eventArgument)
		{
			if (eventArgument.ToLower() == "dockpositionchanged")
			{
				return;
			}
			if (this.Commands.Count == 0)
			{
				this.AddDefaultCommands();
			}
			DockCommand dockCommand = this.Commands.Find((DockCommand cmd) => cmd.Name == eventArgument);
			if (dockCommand != null)
			{
				this.OnCommand(new DockCommandEventArgs(dockCommand));
			}
		}

		// Token: 0x06009AE9 RID: 39657 RVA: 0x00227BEC File Offset: 0x00225DEC
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeProperty<bool>(descriptor, "autoPostBack", this.AutoPostBack, false);
			base.DescribeProperty<bool>(descriptor, "_closed", this.Closed, false);
			base.DescribeProperty<bool>(descriptor, "collapsed", this.Collapsed, false);
			base.DescribeProperty<DockMode>(descriptor, "dockMode", this.DockMode, DockMode.Default);
			base.DescribeProperty<string>(descriptor, "dockZoneID", this.DockZoneID, null);
			base.DescribeProperty<bool>(descriptor, "enableAnimation", this.EnableAnimation, false);
			base.DescribeProperty<bool>(descriptor, "_enableDrag", this.EnableDrag, true);
			base.DescribeProperty<int>(descriptor, "_expandedHeight", this.ExpandedHeight, 0);
			base.DescribeProperty<string>(descriptor, "height", this.Height.ToString(CultureInfo.InvariantCulture), "");
			base.DescribeProperty<int>(descriptor, "index", this.Index, 0);
			base.DescribeProperty<string>(descriptor, "layoutID", this.LayoutID, null);
			base.DescribeProperty<string>(descriptor, "left", this.Left.ToString(CultureInfo.InvariantCulture), "0px");
			base.DescribeProperty<bool>(descriptor, "pinned", this.Pinned, false);
			base.DescribeProperty<bool>(descriptor, "_resizable", this.Resizable, false);
			base.DescribeProperty<string>(descriptor, "tag", this.Tag, "");
			base.DescribeProperty<string>(descriptor, "title", this.Title, "");
			base.DescribeProperty<string>(descriptor, "top", this.Top.ToString(CultureInfo.InvariantCulture), "0px");
			base.DescribeProperty<string>(descriptor, "uniqueName", this.UniqueName, "");
			base.DescribeProperty<string>(descriptor, "width", this.Width.ToString(CultureInfo.InvariantCulture), "300px");
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x06009AEA RID: 39658 RVA: 0x00227DBC File Offset: 0x00225FBC
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			RadWebControl.DescribeEvent(descriptor, "command", this.OnClientCommand);
			RadWebControl.DescribeEvent(descriptor, "dockPositionChanged", this.OnClientDockPositionChanged);
			RadWebControl.DescribeEvent(descriptor, "dockPositionChanging", this.OnClientDockPositionChanging);
			RadWebControl.DescribeEvent(descriptor, "drag", this.OnClientDrag);
			RadWebControl.DescribeEvent(descriptor, "dragEnd", this.OnClientDragEnd);
			RadWebControl.DescribeEvent(descriptor, "dragStart", this.OnClientDragStart);
			RadWebControl.DescribeEvent(descriptor, "initialize", this.OnClientInitialize);
			RadWebControl.DescribeEvent(descriptor, "load", this.OnClientLoad);
			RadWebControl.DescribeEvent(descriptor, "resizeEnd", this.OnClientResizeEnd);
			RadWebControl.DescribeEvent(descriptor, "resizeStart", this.OnClientResizeStart);
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x06009AEC RID: 39660 RVA: 0x00227E7A File Offset: 0x0022607A
		// Note: this type is marked as 'beforefieldinit'.
		static RadDock()
		{
			RadDock.DockPositionChangedEvent = new object();
			RadDock.CommandEvent = new object();
		}

		// Token: 0x04002BBF RID: 11199
		internal const string DefaultCollapseText = "Collapse";

		// Token: 0x04002BC0 RID: 11200
		internal const string DefaultExpandText = "Expand";

		// Token: 0x04002BC1 RID: 11201
		internal const string DefaultPinText = "Pin";

		// Token: 0x04002BC2 RID: 11202
		internal const string DefaultUnpinText = "Unpin";

		// Token: 0x04002BC3 RID: 11203
		internal const string DefaultCloseText = "Close";

		// Token: 0x04002BC6 RID: 11206
		private RadDockZone _dockZone;

		// Token: 0x04002BC7 RID: 11207
		private IDockLayout _layout;

		// Token: 0x04002BC8 RID: 11208
		private SingleTemplateContainer _titlebarContainer;

		// Token: 0x04002BC9 RID: 11209
		private SingleTemplateContainer _contentContainer;

		// Token: 0x04002BCA RID: 11210
		private DockCommandCollection _commands;

		// Token: 0x04002BCB RID: 11211
		private bool ForceRender;

		// Token: 0x04002BCC RID: 11212
		private bool _initialized;

		// Token: 0x04002BCD RID: 11213
		private string _delayedDockZoneID;

		// Token: 0x04002BCE RID: 11214
		private int _index = -1;

		// Token: 0x04002BCF RID: 11215
		private int _expandedHeight;

		// Token: 0x04002BD0 RID: 11216
		private string _tag;
	}
}
