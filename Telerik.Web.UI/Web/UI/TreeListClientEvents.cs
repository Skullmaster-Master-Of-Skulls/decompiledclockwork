using System;
using System.ComponentModel;
using System.Drawing.Design;

namespace Telerik.Web.UI
{
	// Token: 0x02001268 RID: 4712
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class TreeListClientEvents : StateManager
	{
		// Token: 0x17003F04 RID: 16132
		// (get) Token: 0x0600C3CA RID: 50122 RVA: 0x002BE3A4 File Offset: 0x002BC5A4
		// (set) Token: 0x0600C3CB RID: 50123 RVA: 0x002BE3C4 File Offset: 0x002BC5C4
		[DefaultValue("")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		[Description("This client event will be fired when the RadTreeList client component is initializing.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public virtual string OnTreeListCreating
		{
			get
			{
				return (base.ViewState["OnTreeListCreating"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["OnTreeListCreating"] = value;
			}
		}

		// Token: 0x17003F05 RID: 16133
		// (get) Token: 0x0600C3CC RID: 50124 RVA: 0x002BE3D7 File Offset: 0x002BC5D7
		// (set) Token: 0x0600C3CD RID: 50125 RVA: 0x002BE3F7 File Offset: 0x002BC5F7
		[DefaultValue("")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		[Description("This client event will be fired when the RadTreeList client component is initialized.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public virtual string OnTreeListCreated
		{
			get
			{
				return (base.ViewState["OnTreeListCreated"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["OnTreeListCreated"] = value;
			}
		}

		// Token: 0x17003F06 RID: 16134
		// (get) Token: 0x0600C3CE RID: 50126 RVA: 0x002BE40A File Offset: 0x002BC60A
		// (set) Token: 0x0600C3CF RID: 50127 RVA: 0x002BE42A File Offset: 0x002BC62A
		[DefaultValue("")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		[Description("This client event will be fired when the RadTreeList client component is about to be disposed.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public virtual string OnTreeListDestroying
		{
			get
			{
				return (base.ViewState["OnTreeListDestroying"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["OnTreeListDestroying"] = value;
			}
		}

		// Token: 0x17003F07 RID: 16135
		// (get) Token: 0x0600C3D0 RID: 50128 RVA: 0x002BE43D File Offset: 0x002BC63D
		// (set) Token: 0x0600C3D1 RID: 50129 RVA: 0x002BE45D File Offset: 0x002BC65D
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[NotifyParentProperty(true)]
		[Description("This client event will be fired when each of the RadTreeListDataItem client components is created.")]
		[Category("Client-side events")]
		public virtual string OnItemCreated
		{
			get
			{
				return (base.ViewState["OnItemCreated"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["OnItemCreated"] = value;
			}
		}

		// Token: 0x17003F08 RID: 16136
		// (get) Token: 0x0600C3D2 RID: 50130 RVA: 0x002BE470 File Offset: 0x002BC670
		// (set) Token: 0x0600C3D3 RID: 50131 RVA: 0x002BE490 File Offset: 0x002BC690
		[DefaultValue("")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		[Description("This client event will be fired when a RadTreeListDataItem is about to be selected on the client. This event can be canceled.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public virtual string OnItemSelecting
		{
			get
			{
				return (base.ViewState["OnItemSelecting"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["OnItemSelecting"] = value;
			}
		}

		// Token: 0x17003F09 RID: 16137
		// (get) Token: 0x0600C3D4 RID: 50132 RVA: 0x002BE4A3 File Offset: 0x002BC6A3
		// (set) Token: 0x0600C3D5 RID: 50133 RVA: 0x002BE4C3 File Offset: 0x002BC6C3
		[NotifyParentProperty(true)]
		[Description("This client event will be fired when a RadTreeListDataItem is selected on the client.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Category("Client-side events")]
		public virtual string OnItemSelected
		{
			get
			{
				return (base.ViewState["OnItemSelected"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["OnItemSelected"] = value;
			}
		}

		// Token: 0x17003F0A RID: 16138
		// (get) Token: 0x0600C3D6 RID: 50134 RVA: 0x002BE4D6 File Offset: 0x002BC6D6
		// (set) Token: 0x0600C3D7 RID: 50135 RVA: 0x002BE4F6 File Offset: 0x002BC6F6
		[DefaultValue("")]
		[Description("This client event will be fired when a RadTreeListDataItem is about to be deselected on the client. This event can be canceled.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		public virtual string OnItemDeselecting
		{
			get
			{
				return (base.ViewState["OnItemDeselecting"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["OnItemDeselecting"] = value;
			}
		}

		// Token: 0x17003F0B RID: 16139
		// (get) Token: 0x0600C3D8 RID: 50136 RVA: 0x002BE509 File Offset: 0x002BC709
		// (set) Token: 0x0600C3D9 RID: 50137 RVA: 0x002BE529 File Offset: 0x002BC729
		[Description("This client event will be fired when a RadTreeListDataItem is deselected on the client.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		public virtual string OnItemDeselected
		{
			get
			{
				return (base.ViewState["OnItemDeselected"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["OnItemDeselected"] = value;
			}
		}

		// Token: 0x17003F0C RID: 16140
		// (get) Token: 0x0600C3DA RID: 50138 RVA: 0x002BE53C File Offset: 0x002BC73C
		// (set) Token: 0x0600C3DB RID: 50139 RVA: 0x002BE55C File Offset: 0x002BC75C
		[DefaultValue("")]
		[Category("Client-side events")]
		[Description("This client event will be fired when a data row is clicked in RadTreeList.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[NotifyParentProperty(true)]
		public virtual string OnItemClick
		{
			get
			{
				return (base.ViewState["OnItemClick"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["OnItemClick"] = value;
			}
		}

		// Token: 0x17003F0D RID: 16141
		// (get) Token: 0x0600C3DC RID: 50140 RVA: 0x002BE56F File Offset: 0x002BC76F
		// (set) Token: 0x0600C3DD RID: 50141 RVA: 0x002BE58F File Offset: 0x002BC78F
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		[Description("This client event will be fired when a RadTreeList is scrolled.")]
		public virtual string OnScroll
		{
			get
			{
				return (base.ViewState["OnScroll"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["OnScroll"] = value;
			}
		}

		// Token: 0x17003F0E RID: 16142
		// (get) Token: 0x0600C3DE RID: 50142 RVA: 0x002BE5A2 File Offset: 0x002BC7A2
		// (set) Token: 0x0600C3DF RID: 50143 RVA: 0x002BE5C2 File Offset: 0x002BC7C2
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		[Category("Client-side events")]
		[Description("This client event will be fired when a data row is double-clicked in RadTreeList.")]
		public virtual string OnItemDblClick
		{
			get
			{
				return (base.ViewState["OnItemDblClick"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["OnItemDblClick"] = value;
			}
		}

		// Token: 0x17003F0F RID: 16143
		// (get) Token: 0x0600C3E0 RID: 50144 RVA: 0x002BE5D5 File Offset: 0x002BC7D5
		// (set) Token: 0x0600C3E1 RID: 50145 RVA: 0x002BE5F5 File Offset: 0x002BC7F5
		[DefaultValue("")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("This client-side event is fired when a RadTreeList item is about to be dragged.")]
		public virtual string OnItemDragStarted
		{
			get
			{
				return ((string)base.ViewState["OnItemDragStarted"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["OnItemDragStarted"] = value;
			}
		}

		// Token: 0x17003F10 RID: 16144
		// (get) Token: 0x0600C3E2 RID: 50146 RVA: 0x002BE608 File Offset: 0x002BC808
		// (set) Token: 0x0600C3E3 RID: 50147 RVA: 0x002BE628 File Offset: 0x002BC828
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		[Description("This client-side event is fired when a RadTreeList item is dragged.")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public virtual string OnItemDragging
		{
			get
			{
				return ((string)base.ViewState["OnItemDragging"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["OnItemDragging"] = value;
			}
		}

		// Token: 0x17003F11 RID: 16145
		// (get) Token: 0x0600C3E4 RID: 50148 RVA: 0x002BE63B File Offset: 0x002BC83B
		// (set) Token: 0x0600C3E5 RID: 50149 RVA: 0x002BE65B File Offset: 0x002BC85B
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		[Description("This client-side event is fired when a RadTreeList item is about to be dropped after dragging. This event can be canceled")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public virtual string OnItemDropping
		{
			get
			{
				return ((string)base.ViewState["OnItemDropping"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["OnItemDropping"] = value;
			}
		}

		// Token: 0x17003F12 RID: 16146
		// (get) Token: 0x0600C3E6 RID: 50150 RVA: 0x002BE66E File Offset: 0x002BC86E
		// (set) Token: 0x0600C3E7 RID: 50151 RVA: 0x002BE68E File Offset: 0x002BC88E
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("This client-side event is fired when a RadTreeList item is dropped after dragging. This event cannot be canceled.")]
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		public virtual string OnItemDropped
		{
			get
			{
				return ((string)base.ViewState["OnItemDropped"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["OnItemDropped"] = value;
			}
		}

		// Token: 0x17003F13 RID: 16147
		// (get) Token: 0x0600C3E8 RID: 50152 RVA: 0x002BE6A1 File Offset: 0x002BC8A1
		// (set) Token: 0x0600C3E9 RID: 50153 RVA: 0x002BE6C1 File Offset: 0x002BC8C1
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		[Description("This client-side event is fired when a RadTreeList item is right clicked to show its context menu.")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public virtual string OnItemContextMenu
		{
			get
			{
				return ((string)base.ViewState["OnItemContextMenu"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["OnItemContextMenu"] = value;
			}
		}

		// Token: 0x17003F14 RID: 16148
		// (get) Token: 0x0600C3EA RID: 50154 RVA: 0x002BE6D4 File Offset: 0x002BC8D4
		// (set) Token: 0x0600C3EB RID: 50155 RVA: 0x002BE701 File Offset: 0x002BC901
		[Category("Client-side events")]
		[DefaultValue("")]
		[Description("RadTreeList OnKeyPress client-side event")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[NotifyParentProperty(true)]
		public virtual string OnKeyPress
		{
			get
			{
				object obj = base.ViewState["OnKeyPress"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnKeyPress"] = value;
			}
		}

		// Token: 0x17003F15 RID: 16149
		// (get) Token: 0x0600C3EC RID: 50156 RVA: 0x002BE714 File Offset: 0x002BC914
		// (set) Token: 0x0600C3ED RID: 50157 RVA: 0x002BE741 File Offset: 0x002BC941
		[Category("Client-side events")]
		[Description("RadTreeList OnColumnResizing client-side event")]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		public virtual string OnColumnResizing
		{
			get
			{
				object obj = base.ViewState["OnColumnResizing"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnColumnResizing"] = value;
			}
		}

		// Token: 0x17003F16 RID: 16150
		// (get) Token: 0x0600C3EE RID: 50158 RVA: 0x002BE754 File Offset: 0x002BC954
		// (set) Token: 0x0600C3EF RID: 50159 RVA: 0x002BE781 File Offset: 0x002BC981
		[Description("RadTreeList OnColumnResized client-side event")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		public virtual string OnColumnResized
		{
			get
			{
				object obj = base.ViewState["OnColumnResized"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnColumnResized"] = value;
			}
		}

		// Token: 0x17003F17 RID: 16151
		// (get) Token: 0x0600C3F0 RID: 50160 RVA: 0x002BE794 File Offset: 0x002BC994
		// (set) Token: 0x0600C3F1 RID: 50161 RVA: 0x002BE7C1 File Offset: 0x002BC9C1
		[NotifyParentProperty(true)]
		[Description("RadTreeList OnColumnShowing client-side event")]
		[Category("Client-side events")]
		[DefaultValue("")]
		public virtual string OnColumnShowing
		{
			get
			{
				object obj = base.ViewState["OnColumnShowing"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnColumnShowing"] = value;
			}
		}

		// Token: 0x17003F18 RID: 16152
		// (get) Token: 0x0600C3F2 RID: 50162 RVA: 0x002BE7D4 File Offset: 0x002BC9D4
		// (set) Token: 0x0600C3F3 RID: 50163 RVA: 0x002BE801 File Offset: 0x002BCA01
		[DefaultValue("")]
		[Description("RadTreeList OnColumnShown client-side event")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		public virtual string OnColumnShown
		{
			get
			{
				object obj = base.ViewState["OnColumnShown"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnColumnShown"] = value;
			}
		}

		// Token: 0x17003F19 RID: 16153
		// (get) Token: 0x0600C3F4 RID: 50164 RVA: 0x002BE814 File Offset: 0x002BCA14
		// (set) Token: 0x0600C3F5 RID: 50165 RVA: 0x002BE841 File Offset: 0x002BCA41
		[Category("Client-side events")]
		[Description("RadTreeList OnColumnHiding client-side event")]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		public virtual string OnColumnHiding
		{
			get
			{
				object obj = base.ViewState["OnColumnHiding"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnColumnHiding"] = value;
			}
		}

		// Token: 0x17003F1A RID: 16154
		// (get) Token: 0x0600C3F6 RID: 50166 RVA: 0x002BE854 File Offset: 0x002BCA54
		// (set) Token: 0x0600C3F7 RID: 50167 RVA: 0x002BE881 File Offset: 0x002BCA81
		[Description("RadTreeList OnColumnHidden client-side event")]
		[DefaultValue("")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		public virtual string OnColumnHidden
		{
			get
			{
				object obj = base.ViewState["OnColumnHidden"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnColumnHidden"] = value;
			}
		}

		// Token: 0x17003F1B RID: 16155
		// (get) Token: 0x0600C3F8 RID: 50168 RVA: 0x002BE894 File Offset: 0x002BCA94
		// (set) Token: 0x0600C3F9 RID: 50169 RVA: 0x002BE8C1 File Offset: 0x002BCAC1
		[Category("Client-side events")]
		[DefaultValue("")]
		[Description("RadTreeList OnColumnSwapping client-side event")]
		[NotifyParentProperty(true)]
		public virtual string OnColumnSwapping
		{
			get
			{
				object obj = base.ViewState["OnColumnSwapping"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnColumnSwapping"] = value;
			}
		}

		// Token: 0x17003F1C RID: 16156
		// (get) Token: 0x0600C3FA RID: 50170 RVA: 0x002BE8D4 File Offset: 0x002BCAD4
		// (set) Token: 0x0600C3FB RID: 50171 RVA: 0x002BE901 File Offset: 0x002BCB01
		[DefaultValue("")]
		[Description("RadTreeList OnColumnSwapped client-side event")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		public virtual string OnColumnSwapped
		{
			get
			{
				object obj = base.ViewState["OnColumnSwapped"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnColumnSwapped"] = value;
			}
		}

		// Token: 0x17003F1D RID: 16157
		// (get) Token: 0x0600C3FC RID: 50172 RVA: 0x002BE914 File Offset: 0x002BCB14
		// (set) Token: 0x0600C3FD RID: 50173 RVA: 0x002BE941 File Offset: 0x002BCB41
		[DefaultValue("")]
		[Description("RadTreeList OnColumnReordering client-side event")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		public virtual string OnColumnReordering
		{
			get
			{
				object obj = base.ViewState["OnColumnReordering"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnColumnReordering"] = value;
			}
		}

		// Token: 0x17003F1E RID: 16158
		// (get) Token: 0x0600C3FE RID: 50174 RVA: 0x002BE954 File Offset: 0x002BCB54
		// (set) Token: 0x0600C3FF RID: 50175 RVA: 0x002BE981 File Offset: 0x002BCB81
		[DefaultValue("")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		[Description("RadTreeList OnColumnReordered client-side event")]
		public virtual string OnColumnReordered
		{
			get
			{
				object obj = base.ViewState["OnColumnReordered"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnColumnReordered"] = value;
			}
		}
	}
}
