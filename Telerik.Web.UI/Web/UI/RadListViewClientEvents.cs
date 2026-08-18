using System;
using System.ComponentModel;
using System.Drawing.Design;

namespace Telerik.Web.UI
{
	// Token: 0x020019B3 RID: 6579
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class RadListViewClientEvents : StateManager
	{
		// Token: 0x17004CBC RID: 19644
		// (get) Token: 0x0600FE4B RID: 65099 RVA: 0x003920A4 File Offset: 0x003902A4
		// (set) Token: 0x0600FE4C RID: 65100 RVA: 0x003920D1 File Offset: 0x003902D1
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		[Description("This client-side event is fired after the RadListView is created.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Client-side events")]
		public virtual string OnListViewCreated
		{
			get
			{
				object obj = base.ViewState["OnListViewCreated"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnListViewCreated"] = value;
			}
		}

		// Token: 0x17004CBD RID: 19645
		// (get) Token: 0x0600FE4D RID: 65101 RVA: 0x003920E4 File Offset: 0x003902E4
		// (set) Token: 0x0600FE4E RID: 65102 RVA: 0x00392111 File Offset: 0x00390311
		[Category("Client-side events")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[NotifyParentProperty(true)]
		[Description("This client-side event is fired before the RadListView is created.")]
		public virtual string OnListViewCreating
		{
			get
			{
				object obj = base.ViewState["OnListViewCreating"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnListViewCreating"] = value;
			}
		}

		// Token: 0x17004CBE RID: 19646
		// (get) Token: 0x0600FE4F RID: 65103 RVA: 0x00392124 File Offset: 0x00390324
		// (set) Token: 0x0600FE50 RID: 65104 RVA: 0x00392151 File Offset: 0x00390351
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		[Description("This client-side event is fired when RadListView object is destroyed, i.e. on each <em>window.onunload</em>")]
		public virtual string OnListViewDestroying
		{
			get
			{
				object obj = base.ViewState["OnListViewDestroying"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnListViewDestroying"] = value;
			}
		}

		// Token: 0x17004CBF RID: 19647
		// (get) Token: 0x0600FE51 RID: 65105 RVA: 0x00392164 File Offset: 0x00390364
		// (set) Token: 0x0600FE52 RID: 65106 RVA: 0x00392184 File Offset: 0x00390384
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		[Description("This client-side event is fired when a RadListView item is about to be selected.")]
		public virtual string OnItemSelecting
		{
			get
			{
				return ((string)base.ViewState["OnItemSelecting"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["OnItemSelecting"] = value;
			}
		}

		// Token: 0x17004CC0 RID: 19648
		// (get) Token: 0x0600FE53 RID: 65107 RVA: 0x00392197 File Offset: 0x00390397
		// (set) Token: 0x0600FE54 RID: 65108 RVA: 0x003921B7 File Offset: 0x003903B7
		[Description("This client-side event is fired when a RadListView item is selected.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		public virtual string OnItemSelected
		{
			get
			{
				return ((string)base.ViewState["OnItemSelected"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["OnItemSelected"] = value;
			}
		}

		// Token: 0x17004CC1 RID: 19649
		// (get) Token: 0x0600FE55 RID: 65109 RVA: 0x003921CA File Offset: 0x003903CA
		// (set) Token: 0x0600FE56 RID: 65110 RVA: 0x003921EA File Offset: 0x003903EA
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("This client-side event is fired when a RadListView item is about to be deselected.")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		public virtual string OnItemDeselecting
		{
			get
			{
				return ((string)base.ViewState["OnItemDeselecting"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["OnItemDeselecting"] = value;
			}
		}

		// Token: 0x17004CC2 RID: 19650
		// (get) Token: 0x0600FE57 RID: 65111 RVA: 0x003921FD File Offset: 0x003903FD
		// (set) Token: 0x0600FE58 RID: 65112 RVA: 0x0039221D File Offset: 0x0039041D
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("This client-side event is fired when a RadListView item is deselected.")]
		[DefaultValue("")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		public virtual string OnItemDeselected
		{
			get
			{
				return ((string)base.ViewState["OnItemDeselected"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["OnItemDeselected"] = value;
			}
		}

		// Token: 0x17004CC3 RID: 19651
		// (get) Token: 0x0600FE59 RID: 65113 RVA: 0x00392230 File Offset: 0x00390430
		// (set) Token: 0x0600FE5A RID: 65114 RVA: 0x00392250 File Offset: 0x00390450
		[Description("This client-side event is fired when a RadListView item is about to be dragged.")]
		[NotifyParentProperty(true)]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Category("Client-side events")]
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

		// Token: 0x17004CC4 RID: 19652
		// (get) Token: 0x0600FE5B RID: 65115 RVA: 0x00392263 File Offset: 0x00390463
		// (set) Token: 0x0600FE5C RID: 65116 RVA: 0x00392283 File Offset: 0x00390483
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("This client-side event is fired when a RadListView item is dragged.")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
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

		// Token: 0x17004CC5 RID: 19653
		// (get) Token: 0x0600FE5D RID: 65117 RVA: 0x00392296 File Offset: 0x00390496
		// (set) Token: 0x0600FE5E RID: 65118 RVA: 0x003922B6 File Offset: 0x003904B6
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("This client-side event is fired when a RadListView item is about to be dropped after dragging. This event can be canceled")]
		[DefaultValue("")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
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

		// Token: 0x17004CC6 RID: 19654
		// (get) Token: 0x0600FE5F RID: 65119 RVA: 0x003922C9 File Offset: 0x003904C9
		// (set) Token: 0x0600FE60 RID: 65120 RVA: 0x003922E9 File Offset: 0x003904E9
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		[Description("This client-side event is fired when a RadListView item is dropped after dragging. This event cannot be canceled.")]
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

		// Token: 0x17004CC7 RID: 19655
		// (get) Token: 0x0600FE61 RID: 65121 RVA: 0x003922FC File Offset: 0x003904FC
		// (set) Token: 0x0600FE62 RID: 65122 RVA: 0x0039231C File Offset: 0x0039051C
		[Description("This client-side event is fired when a RadListView command occurs.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		public virtual string OnCommand
		{
			get
			{
				return ((string)base.ViewState["OnCommand"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["OnCommand"] = value;
			}
		}

		// Token: 0x17004CC8 RID: 19656
		// (get) Token: 0x0600FE63 RID: 65123 RVA: 0x0039232F File Offset: 0x0039052F
		// (set) Token: 0x0600FE64 RID: 65124 RVA: 0x0039234F File Offset: 0x0039054F
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("This client-side event is fired before RadListView databinds.")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		public virtual string OnDataBinding
		{
			get
			{
				return ((string)base.ViewState["OnDataBinding"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["OnDataBinding"] = value;
			}
		}

		// Token: 0x17004CC9 RID: 19657
		// (get) Token: 0x0600FE65 RID: 65125 RVA: 0x00392362 File Offset: 0x00390562
		// (set) Token: 0x0600FE66 RID: 65126 RVA: 0x00392382 File Offset: 0x00390582
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("This client-side event is fired after RadListView databinds.")]
		[DefaultValue("")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		public virtual string OnDataBound
		{
			get
			{
				return ((string)base.ViewState["OnDataBound"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["OnDataBound"] = value;
			}
		}

		// Token: 0x17004CCA RID: 19658
		// (get) Token: 0x0600FE67 RID: 65127 RVA: 0x00392395 File Offset: 0x00390595
		// (set) Token: 0x0600FE68 RID: 65128 RVA: 0x003923B5 File Offset: 0x003905B5
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("This client-side event is fired when RadListView fails to databind automatically to a web service.")]
		[DefaultValue("")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		public virtual string OnDataBindingFailed
		{
			get
			{
				return ((string)base.ViewState["OnDataBindingFailed"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["OnDataBindingFailed"] = value;
			}
		}

		// Token: 0x17004CCB RID: 19659
		// (get) Token: 0x0600FE69 RID: 65129 RVA: 0x003923C8 File Offset: 0x003905C8
		// (set) Token: 0x0600FE6A RID: 65130 RVA: 0x003923E8 File Offset: 0x003905E8
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[NotifyParentProperty(true)]
		[Description("This client-side event is fired during automatic databinding to a web service when the data source is resolved.")]
		[DefaultValue("")]
		[Category("Client-side events")]
		public virtual string OnDataSourceResolved
		{
			get
			{
				return ((string)base.ViewState["OnDataSourceResolved"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["OnDataSourceResolved"] = value;
			}
		}

		// Token: 0x17004CCC RID: 19660
		// (get) Token: 0x0600FE6B RID: 65131 RVA: 0x003923FB File Offset: 0x003905FB
		// (set) Token: 0x0600FE6C RID: 65132 RVA: 0x0039241B File Offset: 0x0039061B
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("This client-side event is fired during databinding when a client-side template is created.")]
		[DefaultValue("")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		public virtual string OnTemplateCreated
		{
			get
			{
				return ((string)base.ViewState["OnTemplateCreated"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["OnTemplateCreated"] = value;
			}
		}

		// Token: 0x17004CCD RID: 19661
		// (get) Token: 0x0600FE6D RID: 65133 RVA: 0x0039242E File Offset: 0x0039062E
		// (set) Token: 0x0600FE6E RID: 65134 RVA: 0x0039244E File Offset: 0x0039064E
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		[Category("Client-side events")]
		[Description("This client-side event is fired during databinding when a client-side template is databound.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public virtual string OnTemplateDataBound
		{
			get
			{
				return ((string)base.ViewState["OnTemplateDataBound"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["OnTemplateDataBound"] = value;
			}
		}
	}
}
