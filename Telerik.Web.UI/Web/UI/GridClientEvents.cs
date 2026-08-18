using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x020010D0 RID: 4304
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class GridClientEvents : ObjectWithState
	{
		// Token: 0x0600AFB8 RID: 44984 RVA: 0x002613E3 File Offset: 0x0025F5E3
		public GridClientEvents(StateBag OwnerStateBag) : base("cs_events_", OwnerStateBag)
		{
		}

		// Token: 0x170038C6 RID: 14534
		// (get) Token: 0x0600AFB9 RID: 44985 RVA: 0x002613F1 File Offset: 0x0025F5F1
		// (set) Token: 0x0600AFBA RID: 44986 RVA: 0x00261411 File Offset: 0x0025F611
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Client-side events")]
		[Description("This event is fired when the grid request data using client-side data-binding.")]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		public virtual string OnDataBinding
		{
			get
			{
				return ((string)base.ViewState["OnDataBinding"]) ?? "";
			}
			set
			{
				base.ViewState["OnDataBinding"] = value;
			}
		}

		// Token: 0x170038C7 RID: 14535
		// (get) Token: 0x0600AFBB RID: 44987 RVA: 0x00261424 File Offset: 0x0025F624
		// (set) Token: 0x0600AFBC RID: 44988 RVA: 0x00261444 File Offset: 0x0025F644
		[Category("Client-side events")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[NotifyParentProperty(true)]
		[Description("This event is fired if request for data fails when using client-side data-binding.")]
		public virtual string OnDataBindingFailed
		{
			get
			{
				return ((string)base.ViewState["OnDataBindingFailed"]) ?? "";
			}
			set
			{
				base.ViewState["OnDataBindingFailed"] = value;
			}
		}

		// Token: 0x170038C8 RID: 14536
		// (get) Token: 0x0600AFBD RID: 44989 RVA: 0x00261457 File Offset: 0x0025F657
		// (set) Token: 0x0600AFBE RID: 44990 RVA: 0x00261477 File Offset: 0x0025F677
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		[Description("This event is fired when the grid client-side data is retrieved from the server.")]
		public virtual string OnDataSourceResolved
		{
			get
			{
				return ((string)base.ViewState["OnDataSourceResolved"]) ?? "";
			}
			set
			{
				base.ViewState["OnDataSourceResolved"] = value;
			}
		}

		// Token: 0x170038C9 RID: 14537
		// (get) Token: 0x0600AFBF RID: 44991 RVA: 0x0026148A File Offset: 0x0025F68A
		// (set) Token: 0x0600AFC0 RID: 44992 RVA: 0x002614AA File Offset: 0x0025F6AA
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		[Description("This event is fired when the grid client-side data-binding is finished.")]
		public virtual string OnDataBound
		{
			get
			{
				return ((string)base.ViewState["OnDataBound"]) ?? "";
			}
			set
			{
				base.ViewState["OnDataBound"] = value;
			}
		}

		// Token: 0x170038CA RID: 14538
		// (get) Token: 0x0600AFC1 RID: 44993 RVA: 0x002614C0 File Offset: 0x0025F6C0
		// (set) Token: 0x0600AFC2 RID: 44994 RVA: 0x002614ED File Offset: 0x0025F6ED
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		[Description("RadGrid_OnGridCreating")]
		public virtual string OnGridCreating
		{
			get
			{
				object obj = base.ViewState["OnGridCreating"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnGridCreating"] = value;
			}
		}

		// Token: 0x170038CB RID: 14539
		// (get) Token: 0x0600AFC3 RID: 44995 RVA: 0x00261500 File Offset: 0x0025F700
		// (set) Token: 0x0600AFC4 RID: 44996 RVA: 0x0026152D File Offset: 0x0025F72D
		[Category("Client-side events")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[NotifyParentProperty(true)]
		[Description("Gets or sets the client-side event which is fired when RadGrid header RadContextMenu fires its 'Showing' event.")]
		public virtual string OnHeaderMenuShowing
		{
			get
			{
				object obj = base.ViewState["OnHeaderMenuShowing"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnHeaderMenuShowing"] = value;
			}
		}

		// Token: 0x170038CC RID: 14540
		// (get) Token: 0x0600AFC5 RID: 44997 RVA: 0x00261540 File Offset: 0x0025F740
		// (set) Token: 0x0600AFC6 RID: 44998 RVA: 0x0026156D File Offset: 0x0025F76D
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		[Description("Gets or sets the client-side event which is fired when a RadGrid row is dropped.")]
		public virtual string OnRowDropping
		{
			get
			{
				object obj = base.ViewState["OnRowDropping"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnRowDropping"] = value;
			}
		}

		// Token: 0x170038CD RID: 14541
		// (get) Token: 0x0600AFC7 RID: 44999 RVA: 0x00261580 File Offset: 0x0025F780
		// (set) Token: 0x0600AFC8 RID: 45000 RVA: 0x002615AD File Offset: 0x0025F7AD
		[Category("Client-side events")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[NotifyParentProperty(true)]
		[Description("Gets or sets the client-side event which is fired when a RadGrid row is dropped.")]
		public virtual string OnRowDropped
		{
			get
			{
				object obj = base.ViewState["OnRowDropped"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnRowDropped"] = value;
			}
		}

		// Token: 0x170038CE RID: 14542
		// (get) Token: 0x0600AFC9 RID: 45001 RVA: 0x002615C0 File Offset: 0x0025F7C0
		// (set) Token: 0x0600AFCA RID: 45002 RVA: 0x002615ED File Offset: 0x0025F7ED
		[Category("Client-side events")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[NotifyParentProperty(true)]
		[Description("Gets or sets the client-side event which will be fired when a RadGrid row is being dragged.")]
		public virtual string OnRowDragging
		{
			get
			{
				object obj = base.ViewState["OnRowDragging"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnRowDragging"] = value;
			}
		}

		// Token: 0x170038CF RID: 14543
		// (get) Token: 0x0600AFCB RID: 45003 RVA: 0x00261600 File Offset: 0x0025F800
		// (set) Token: 0x0600AFCC RID: 45004 RVA: 0x0026162D File Offset: 0x0025F82D
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		[Description("Gets or sets the client-side event which will be fired when a RadGrid row drag starts.")]
		public virtual string OnRowDragStarted
		{
			get
			{
				object obj = base.ViewState["OnRowDragStarted"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnRowDragStarted"] = value;
			}
		}

		// Token: 0x170038D0 RID: 14544
		// (get) Token: 0x0600AFCD RID: 45005 RVA: 0x00261640 File Offset: 0x0025F840
		// (set) Token: 0x0600AFCE RID: 45006 RVA: 0x0026166D File Offset: 0x0025F86D
		[Category("Client-side events")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[NotifyParentProperty(true)]
		[Description("RadGrid_OnGridCreated")]
		public virtual string OnGridCreated
		{
			get
			{
				object obj = base.ViewState["OnGridCreated"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnGridCreated"] = value;
			}
		}

		// Token: 0x170038D1 RID: 14545
		// (get) Token: 0x0600AFCF RID: 45007 RVA: 0x00261680 File Offset: 0x0025F880
		// (set) Token: 0x0600AFD0 RID: 45008 RVA: 0x002616AD File Offset: 0x0025F8AD
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		[Description("RadGrid_OnGridDestroying")]
		public virtual string OnGridDestroying
		{
			get
			{
				object obj = base.ViewState["OnGridDestroying"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnGridDestroying"] = value;
			}
		}

		// Token: 0x170038D2 RID: 14546
		// (get) Token: 0x0600AFD1 RID: 45009 RVA: 0x002616C0 File Offset: 0x0025F8C0
		// (set) Token: 0x0600AFD2 RID: 45010 RVA: 0x002616ED File Offset: 0x0025F8ED
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		[Description("RadGrid_OnMasterTableViewCreating")]
		public virtual string OnMasterTableViewCreating
		{
			get
			{
				object obj = base.ViewState["OnMasterTableViewCreating"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnMasterTableViewCreating"] = value;
			}
		}

		// Token: 0x170038D3 RID: 14547
		// (get) Token: 0x0600AFD3 RID: 45011 RVA: 0x00261700 File Offset: 0x0025F900
		// (set) Token: 0x0600AFD4 RID: 45012 RVA: 0x0026172D File Offset: 0x0025F92D
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		[Description("RadGrid_OnMasterTableViewCreated")]
		public virtual string OnMasterTableViewCreated
		{
			get
			{
				object obj = base.ViewState["OnMasterTableViewCreated"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnMasterTableViewCreated"] = value;
			}
		}

		// Token: 0x170038D4 RID: 14548
		// (get) Token: 0x0600AFD5 RID: 45013 RVA: 0x00261740 File Offset: 0x0025F940
		// (set) Token: 0x0600AFD6 RID: 45014 RVA: 0x0026176D File Offset: 0x0025F96D
		[Category("Client-side events")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[NotifyParentProperty(true)]
		[Description("RadGrid_OnTableCreating")]
		public virtual string OnTableCreating
		{
			get
			{
				object obj = base.ViewState["OnTableCreating"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnTableCreating"] = value;
			}
		}

		// Token: 0x170038D5 RID: 14549
		// (get) Token: 0x0600AFD7 RID: 45015 RVA: 0x00261780 File Offset: 0x0025F980
		// (set) Token: 0x0600AFD8 RID: 45016 RVA: 0x002617AD File Offset: 0x0025F9AD
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		[Description("RadGrid_OnTableCreated")]
		public virtual string OnTableCreated
		{
			get
			{
				object obj = base.ViewState["OnTableCreated"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnTableCreated"] = value;
			}
		}

		// Token: 0x170038D6 RID: 14550
		// (get) Token: 0x0600AFD9 RID: 45017 RVA: 0x002617C0 File Offset: 0x0025F9C0
		// (set) Token: 0x0600AFDA RID: 45018 RVA: 0x002617ED File Offset: 0x0025F9ED
		[Category("Client-side events")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[NotifyParentProperty(true)]
		[Description("RadGrid_OnTableDestroying")]
		public virtual string OnTableDestroying
		{
			get
			{
				object obj = base.ViewState["OnTableDestroying"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnTableDestroying"] = value;
			}
		}

		// Token: 0x170038D7 RID: 14551
		// (get) Token: 0x0600AFDB RID: 45019 RVA: 0x00261800 File Offset: 0x0025FA00
		// (set) Token: 0x0600AFDC RID: 45020 RVA: 0x0026182D File Offset: 0x0025FA2D
		[Category("Client-side events")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[NotifyParentProperty(true)]
		[Description("RadGrid_OnCellSelecting")]
		public virtual string OnCellSelecting
		{
			get
			{
				object obj = base.ViewState["OnCellSelecting"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnCellSelecting"] = value;
			}
		}

		// Token: 0x170038D8 RID: 14552
		// (get) Token: 0x0600AFDD RID: 45021 RVA: 0x00261840 File Offset: 0x0025FA40
		// (set) Token: 0x0600AFDE RID: 45022 RVA: 0x0026186D File Offset: 0x0025FA6D
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		[Description("RadGrid_OnCellSelected")]
		public virtual string OnCellSelected
		{
			get
			{
				object obj = base.ViewState["OnCellSelected"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnCellSelected"] = value;
			}
		}

		// Token: 0x170038D9 RID: 14553
		// (get) Token: 0x0600AFDF RID: 45023 RVA: 0x00261880 File Offset: 0x0025FA80
		// (set) Token: 0x0600AFE0 RID: 45024 RVA: 0x002618AD File Offset: 0x0025FAAD
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		[Description("RadGrid_OnCellDeselecting")]
		public virtual string OnCellDeselecting
		{
			get
			{
				object obj = base.ViewState["OnCellDeselecting"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnCellDeselecting"] = value;
			}
		}

		// Token: 0x170038DA RID: 14554
		// (get) Token: 0x0600AFE1 RID: 45025 RVA: 0x002618C0 File Offset: 0x0025FAC0
		// (set) Token: 0x0600AFE2 RID: 45026 RVA: 0x002618ED File Offset: 0x0025FAED
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		[Description("RadGrid_OnCellDeselected")]
		public virtual string OnCellDeselected
		{
			get
			{
				object obj = base.ViewState["OnCellDeselected"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnCellDeselected"] = value;
			}
		}

		// Token: 0x170038DB RID: 14555
		// (get) Token: 0x0600AFE3 RID: 45027 RVA: 0x00261900 File Offset: 0x0025FB00
		// (set) Token: 0x0600AFE4 RID: 45028 RVA: 0x0026192D File Offset: 0x0025FB2D
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		[Description("RadGrid_OnColumnCreating")]
		public virtual string OnColumnCreating
		{
			get
			{
				object obj = base.ViewState["OnColumnCreating"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnColumnCreating"] = value;
			}
		}

		// Token: 0x170038DC RID: 14556
		// (get) Token: 0x0600AFE5 RID: 45029 RVA: 0x00261940 File Offset: 0x0025FB40
		// (set) Token: 0x0600AFE6 RID: 45030 RVA: 0x0026196D File Offset: 0x0025FB6D
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		[Description("RadGrid_OnColumnCreated")]
		public virtual string OnColumnCreated
		{
			get
			{
				object obj = base.ViewState["OnColumnCreated"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnColumnCreated"] = value;
			}
		}

		// Token: 0x170038DD RID: 14557
		// (get) Token: 0x0600AFE7 RID: 45031 RVA: 0x00261980 File Offset: 0x0025FB80
		// (set) Token: 0x0600AFE8 RID: 45032 RVA: 0x002619AD File Offset: 0x0025FBAD
		[Category("Client-side events")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[NotifyParentProperty(true)]
		[Description("RadGrid_OnColumnDestroying")]
		public virtual string OnColumnDestroying
		{
			get
			{
				object obj = base.ViewState["OnColumnDestroying"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnColumnDestroying"] = value;
			}
		}

		// Token: 0x170038DE RID: 14558
		// (get) Token: 0x0600AFE9 RID: 45033 RVA: 0x002619C0 File Offset: 0x0025FBC0
		// (set) Token: 0x0600AFEA RID: 45034 RVA: 0x002619ED File Offset: 0x0025FBED
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		[Description("RadGrid_OnColumnResizing")]
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

		// Token: 0x170038DF RID: 14559
		// (get) Token: 0x0600AFEB RID: 45035 RVA: 0x00261A00 File Offset: 0x0025FC00
		// (set) Token: 0x0600AFEC RID: 45036 RVA: 0x00261A2D File Offset: 0x0025FC2D
		[Category("Client-side events")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[NotifyParentProperty(true)]
		[Description("Gets or sets the client-side event which will be fired when a column have been resized.")]
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

		// Token: 0x170038E0 RID: 14560
		// (get) Token: 0x0600AFED RID: 45037 RVA: 0x00261A40 File Offset: 0x0025FC40
		// (set) Token: 0x0600AFEE RID: 45038 RVA: 0x00261A6D File Offset: 0x0025FC6D
		[Category("Client-side events")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[NotifyParentProperty(true)]
		[Description("Gets or sets the client-side event which will be fired before a column have been swapped.")]
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

		// Token: 0x170038E1 RID: 14561
		// (get) Token: 0x0600AFEF RID: 45039 RVA: 0x00261A80 File Offset: 0x0025FC80
		// (set) Token: 0x0600AFF0 RID: 45040 RVA: 0x00261AAD File Offset: 0x0025FCAD
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		[Description("Gets or sets the client-side event which will be fired when a column have been swaped.")]
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

		// Token: 0x170038E2 RID: 14562
		// (get) Token: 0x0600AFF1 RID: 45041 RVA: 0x00261AC0 File Offset: 0x0025FCC0
		// (set) Token: 0x0600AFF2 RID: 45042 RVA: 0x00261AED File Offset: 0x0025FCED
		[Category("Client-side events")]
		[Description("Gets or sets the clint-side event which will be fired before a column have been moved to the left.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		public virtual string OnColumnMovingToLeft
		{
			get
			{
				object obj = base.ViewState["OnColumnMovingToLeft"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnColumnMovingToLeft"] = value;
			}
		}

		// Token: 0x170038E3 RID: 14563
		// (get) Token: 0x0600AFF3 RID: 45043 RVA: 0x00261B00 File Offset: 0x0025FD00
		// (set) Token: 0x0600AFF4 RID: 45044 RVA: 0x00261B2D File Offset: 0x0025FD2D
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("Gets or sets the client-side event which will be fired after a column have been moved to the left.")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		public virtual string OnColumnMovedToLeft
		{
			get
			{
				object obj = base.ViewState["OnColumnMovedToLeft"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnColumnMovedToLeft"] = value;
			}
		}

		// Token: 0x170038E4 RID: 14564
		// (get) Token: 0x0600AFF5 RID: 45045 RVA: 0x00261B40 File Offset: 0x0025FD40
		// (set) Token: 0x0600AFF6 RID: 45046 RVA: 0x00261B6D File Offset: 0x0025FD6D
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("Gets or sets the clint-side event which will be fired before a column have been moved to the right.")]
		[DefaultValue("")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		public virtual string OnColumnMovingToRight
		{
			get
			{
				object obj = base.ViewState["OnColumnMovingToRight"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnColumnMovingToRight"] = value;
			}
		}

		// Token: 0x170038E5 RID: 14565
		// (get) Token: 0x0600AFF7 RID: 45047 RVA: 0x00261B80 File Offset: 0x0025FD80
		// (set) Token: 0x0600AFF8 RID: 45048 RVA: 0x00261BAD File Offset: 0x0025FDAD
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		[Description("Gets or sets the client-side event which will be fired after a column have been moved to the right.")]
		public virtual string OnColumnMovedToRight
		{
			get
			{
				object obj = base.ViewState["OnColumnMovedToRight"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnColumnMovedToRight"] = value;
			}
		}

		// Token: 0x170038E6 RID: 14566
		// (get) Token: 0x0600AFF9 RID: 45049 RVA: 0x00261BC0 File Offset: 0x0025FDC0
		// (set) Token: 0x0600AFFA RID: 45050 RVA: 0x00261BED File Offset: 0x0025FDED
		[Description("Gets or sets the client-side event which is fired before a column have been hidden.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Category("Client-side events")]
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

		// Token: 0x170038E7 RID: 14567
		// (get) Token: 0x0600AFFB RID: 45051 RVA: 0x00261C00 File Offset: 0x0025FE00
		// (set) Token: 0x0600AFFC RID: 45052 RVA: 0x00261C2D File Offset: 0x0025FE2D
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("Gets or sets the client-side event which is fired when a column have been hidden.")]
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

		// Token: 0x170038E8 RID: 14568
		// (get) Token: 0x0600AFFD RID: 45053 RVA: 0x00261C40 File Offset: 0x0025FE40
		// (set) Token: 0x0600AFFE RID: 45054 RVA: 0x00261C6D File Offset: 0x0025FE6D
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("RadGrid_OnColumnShowing")]
		[DefaultValue("")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
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

		// Token: 0x170038E9 RID: 14569
		// (get) Token: 0x0600AFFF RID: 45055 RVA: 0x00261C80 File Offset: 0x0025FE80
		// (set) Token: 0x0600B000 RID: 45056 RVA: 0x00261CAD File Offset: 0x0025FEAD
		[NotifyParentProperty(true)]
		[Description("RadGrid_OnColumnShown")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Category("Client-side events")]
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

		// Token: 0x170038EA RID: 14570
		// (get) Token: 0x0600B001 RID: 45057 RVA: 0x00261CC0 File Offset: 0x0025FEC0
		// (set) Token: 0x0600B002 RID: 45058 RVA: 0x00261CED File Offset: 0x0025FEED
		[Description("Gets or sets the client-side event which will be fired before a row have been created.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		public virtual string OnRowCreating
		{
			get
			{
				object obj = base.ViewState["OnRowCreating"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnRowCreating"] = value;
			}
		}

		// Token: 0x170038EB RID: 14571
		// (get) Token: 0x0600B003 RID: 45059 RVA: 0x00261D00 File Offset: 0x0025FF00
		// (set) Token: 0x0600B004 RID: 45060 RVA: 0x00261D2D File Offset: 0x0025FF2D
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("Gets or sets the client-side event which will be fired when a row have been created.")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		public virtual string OnRowCreated
		{
			get
			{
				object obj = base.ViewState["OnRowCreated"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnRowCreated"] = value;
			}
		}

		// Token: 0x170038EC RID: 14572
		// (get) Token: 0x0600B005 RID: 45061 RVA: 0x00261D40 File Offset: 0x0025FF40
		// (set) Token: 0x0600B006 RID: 45062 RVA: 0x00261D6D File Offset: 0x0025FF6D
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("Gets or sets the client-side event which will be fired when a row have been destroyed.")]
		[DefaultValue("")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		public virtual string OnRowDestroying
		{
			get
			{
				object obj = base.ViewState["OnRowDestroying"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnRowDestroying"] = value;
			}
		}

		// Token: 0x170038ED RID: 14573
		// (get) Token: 0x0600B007 RID: 45063 RVA: 0x00261D80 File Offset: 0x0025FF80
		// (set) Token: 0x0600B008 RID: 45064 RVA: 0x00261DAD File Offset: 0x0025FFAD
		[DefaultValue("")]
		[Description("Gets or sets the client-side event which will be fired before a row have been resized.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		public virtual string OnRowResizing
		{
			get
			{
				object obj = base.ViewState["OnRowResizing"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnRowResizing"] = value;
			}
		}

		// Token: 0x170038EE RID: 14574
		// (get) Token: 0x0600B009 RID: 45065 RVA: 0x00261DC0 File Offset: 0x0025FFC0
		// (set) Token: 0x0600B00A RID: 45066 RVA: 0x00261DED File Offset: 0x0025FFED
		[Description("Gets or sets the client-side event which is fired when a row have been resized.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		public virtual string OnRowResized
		{
			get
			{
				object obj = base.ViewState["OnRowResized"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnRowResized"] = value;
			}
		}

		// Token: 0x170038EF RID: 14575
		// (get) Token: 0x0600B00B RID: 45067 RVA: 0x00261E00 File Offset: 0x00260000
		// (set) Token: 0x0600B00C RID: 45068 RVA: 0x00261E2D File Offset: 0x0026002D
		[DefaultValue("")]
		[Description("/// Gets or sets the client-side event which will be fired before a row is hidden.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		public virtual string OnRowHiding
		{
			get
			{
				object obj = base.ViewState["OnRowHiding"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnRowHiding"] = value;
			}
		}

		// Token: 0x170038F0 RID: 14576
		// (get) Token: 0x0600B00D RID: 45069 RVA: 0x00261E40 File Offset: 0x00260040
		// (set) Token: 0x0600B00E RID: 45070 RVA: 0x00261E6D File Offset: 0x0026006D
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("Gets or sets the client-side event which will be fired when a row have been hidden.")]
		[Category("Client-side events")]
		public virtual string OnRowHidden
		{
			get
			{
				object obj = base.ViewState["OnRowHidden"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnRowHidden"] = value;
			}
		}

		// Token: 0x170038F1 RID: 14577
		// (get) Token: 0x0600B00F RID: 45071 RVA: 0x00261E80 File Offset: 0x00260080
		// (set) Token: 0x0600B010 RID: 45072 RVA: 0x00261EAD File Offset: 0x002600AD
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Description("Gets or sets the client-side event which will be fired before a row is shown.")]
		[Category("Client-side events")]
		public virtual string OnRowShowing
		{
			get
			{
				object obj = base.ViewState["OnRowShowing"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnRowShowing"] = value;
			}
		}

		// Token: 0x170038F2 RID: 14578
		// (get) Token: 0x0600B011 RID: 45073 RVA: 0x00261EC0 File Offset: 0x002600C0
		// (set) Token: 0x0600B012 RID: 45074 RVA: 0x00261EED File Offset: 0x002600ED
		[DefaultValue("")]
		[Description("Gets or sets the client-side event which will be fired when a row have been shown.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		public virtual string OnRowShown
		{
			get
			{
				object obj = base.ViewState["OnRowShown"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnRowShown"] = value;
			}
		}

		// Token: 0x170038F3 RID: 14579
		// (get) Token: 0x0600B013 RID: 45075 RVA: 0x00261F00 File Offset: 0x00260100
		// (set) Token: 0x0600B014 RID: 45076 RVA: 0x00261F2D File Offset: 0x0026012D
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[NotifyParentProperty(true)]
		[Description("Gets or sets the client-side event which will be fired when a row have been clicked.")]
		[DefaultValue("")]
		[Category("Client-side events")]
		public virtual string OnRowClick
		{
			get
			{
				object obj = base.ViewState["OnRowClick"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnRowClick"] = value;
			}
		}

		// Token: 0x170038F4 RID: 14580
		// (get) Token: 0x0600B015 RID: 45077 RVA: 0x00261F40 File Offset: 0x00260140
		// (set) Token: 0x0600B016 RID: 45078 RVA: 0x00261F6D File Offset: 0x0026016D
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("Gets or sets the client-side event which will be fired when a row have been double clicked.")]
		[DefaultValue("")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		public virtual string OnRowDblClick
		{
			get
			{
				object obj = base.ViewState["OnRowDblClick"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnRowDblClick"] = value;
			}
		}

		// Token: 0x170038F5 RID: 14581
		// (get) Token: 0x0600B017 RID: 45079 RVA: 0x00261F80 File Offset: 0x00260180
		// (set) Token: 0x0600B018 RID: 45080 RVA: 0x00261FAD File Offset: 0x002601AD
		[Category("Client-side events")]
		[Description("Gets or sets the client-side event which will be fired when a colum have been clicked.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		public virtual string OnColumnClick
		{
			get
			{
				object obj = base.ViewState["OnColumnClick"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnColumnClick"] = value;
			}
		}

		// Token: 0x170038F6 RID: 14582
		// (get) Token: 0x0600B019 RID: 45081 RVA: 0x00261FC0 File Offset: 0x002601C0
		// (set) Token: 0x0600B01A RID: 45082 RVA: 0x00261FED File Offset: 0x002601ED
		[Description("Gets or sets the client-side event which will be fired when a column have been double clicked.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		public virtual string OnColumnDblClick
		{
			get
			{
				object obj = base.ViewState["OnColumnDblClick"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnColumnDblClick"] = value;
			}
		}

		// Token: 0x170038F7 RID: 14583
		// (get) Token: 0x0600B01B RID: 45083 RVA: 0x00262000 File Offset: 0x00260200
		// (set) Token: 0x0600B01C RID: 45084 RVA: 0x0026202D File Offset: 0x0026022D
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		[Description("Gets or sets the client-side event which will be fired before a row is selected.")]
		public virtual string OnRowSelecting
		{
			get
			{
				object obj = base.ViewState["OnRowSelecting"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnRowSelecting"] = value;
			}
		}

		// Token: 0x170038F8 RID: 14584
		// (get) Token: 0x0600B01D RID: 45085 RVA: 0x00262040 File Offset: 0x00260240
		// (set) Token: 0x0600B01E RID: 45086 RVA: 0x0026206D File Offset: 0x0026026D
		[Category("Client-side events")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[NotifyParentProperty(true)]
		[Description("Gets or sets the client-side event which will be fired when a row have been selected.")]
		public virtual string OnRowSelected
		{
			get
			{
				object obj = base.ViewState["OnRowSelected"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnRowSelected"] = value;
			}
		}

		// Token: 0x170038F9 RID: 14585
		// (get) Token: 0x0600B01F RID: 45087 RVA: 0x00262080 File Offset: 0x00260280
		// (set) Token: 0x0600B020 RID: 45088 RVA: 0x002620AD File Offset: 0x002602AD
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		[Description("Gets or sets the client-side event which will be fired before a row is deselected.")]
		public virtual string OnRowDeselecting
		{
			get
			{
				object obj = base.ViewState["OnRowDeselecting"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnRowDeselecting"] = value;
			}
		}

		// Token: 0x170038FA RID: 14586
		// (get) Token: 0x0600B021 RID: 45089 RVA: 0x002620C0 File Offset: 0x002602C0
		// (set) Token: 0x0600B022 RID: 45090 RVA: 0x002620ED File Offset: 0x002602ED
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		[Description("Gets or sets the client-side event which will be fired when a row have been deselected.")]
		public virtual string OnRowDeselected
		{
			get
			{
				object obj = base.ViewState["OnRowDeselected"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnRowDeselected"] = value;
			}
		}

		// Token: 0x170038FB RID: 14587
		// (get) Token: 0x0600B023 RID: 45091 RVA: 0x00262100 File Offset: 0x00260300
		// (set) Token: 0x0600B024 RID: 45092 RVA: 0x0026212D File Offset: 0x0026032D
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		[Description("Gets or sets the client-side event which will be fired when a mouse hovers over a row element.")]
		public virtual string OnRowMouseOver
		{
			get
			{
				object obj = base.ViewState["OnRowMouseOver"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnRowMouseOver"] = value;
			}
		}

		// Token: 0x170038FC RID: 14588
		// (get) Token: 0x0600B025 RID: 45093 RVA: 0x00262140 File Offset: 0x00260340
		// (set) Token: 0x0600B026 RID: 45094 RVA: 0x0026216D File Offset: 0x0026036D
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		[Description("Gets or sets the client-side event which will be fired when a mouse leaves a row element.")]
		public virtual string OnRowMouseOut
		{
			get
			{
				object obj = base.ViewState["OnRowMouseOut"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnRowMouseOut"] = value;
			}
		}

		// Token: 0x170038FD RID: 14589
		// (get) Token: 0x0600B027 RID: 45095 RVA: 0x00262180 File Offset: 0x00260380
		// (set) Token: 0x0600B028 RID: 45096 RVA: 0x002621AD File Offset: 0x002603AD
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		[Description("Gets or sets the client-side event which will be fired when a mouse hovers over a column element.")]
		public virtual string OnColumnMouseOver
		{
			get
			{
				object obj = base.ViewState["OnColumnMouseOver"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnColumnMouseOver"] = value;
			}
		}

		// Token: 0x170038FE RID: 14590
		// (get) Token: 0x0600B029 RID: 45097 RVA: 0x002621C0 File Offset: 0x002603C0
		// (set) Token: 0x0600B02A RID: 45098 RVA: 0x002621ED File Offset: 0x002603ED
		[Description("Gets or sets the client-side event which will be fired when a mouse leaves a column element.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		public virtual string OnColumnMouseOut
		{
			get
			{
				object obj = base.ViewState["OnColumnMouseOut"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnColumnMouseOut"] = value;
			}
		}

		// Token: 0x170038FF RID: 14591
		// (get) Token: 0x0600B02B RID: 45099 RVA: 0x00262200 File Offset: 0x00260400
		// (set) Token: 0x0600B02C RID: 45100 RVA: 0x0026222D File Offset: 0x0026042D
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		[Description("Gets or sets the client-side event which will be fired when a column is right clicked.")]
		public virtual string OnColumnContextMenu
		{
			get
			{
				object obj = base.ViewState["OnColumnContextMenu"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnColumnContextMenu"] = value;
			}
		}

		// Token: 0x17003900 RID: 14592
		// (get) Token: 0x0600B02D RID: 45101 RVA: 0x00262240 File Offset: 0x00260440
		// (set) Token: 0x0600B02E RID: 45102 RVA: 0x0026226D File Offset: 0x0026046D
		[Description("RadGrid_OnRowContextMenu")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		public virtual string OnRowContextMenu
		{
			get
			{
				object obj = base.ViewState["OnRowContextMenu"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnRowContextMenu"] = value;
			}
		}

		// Token: 0x17003901 RID: 14593
		// (get) Token: 0x0600B02F RID: 45103 RVA: 0x00262280 File Offset: 0x00260480
		// (set) Token: 0x0600B030 RID: 45104 RVA: 0x002622AD File Offset: 0x002604AD
		[Description("Gets or sets the client-side event which will be fired when RadGrid is scrolled.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		public virtual string OnScroll
		{
			get
			{
				object obj = base.ViewState["OnScroll"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnScroll"] = value;
			}
		}

		// Token: 0x17003902 RID: 14594
		// (get) Token: 0x0600B031 RID: 45105 RVA: 0x002622C0 File Offset: 0x002604C0
		// (set) Token: 0x0600B032 RID: 45106 RVA: 0x002622ED File Offset: 0x002604ED
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("RadGrid OnKeyPress client-side event")]
		[Category("Client-side events")]
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

		// Token: 0x17003903 RID: 14595
		// (get) Token: 0x0600B033 RID: 45107 RVA: 0x00262300 File Offset: 0x00260500
		// (set) Token: 0x0600B034 RID: 45108 RVA: 0x0026232D File Offset: 0x0026052D
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("Gets or sets the client-side event which is fired before a hierarchy is expanded.")]
		[DefaultValue("")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		public virtual string OnHierarchyExpanding
		{
			get
			{
				object obj = base.ViewState["OnHierarchyExpanding"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnHierarchyExpanding"] = value;
			}
		}

		// Token: 0x17003904 RID: 14596
		// (get) Token: 0x0600B035 RID: 45109 RVA: 0x00262340 File Offset: 0x00260540
		// (set) Token: 0x0600B036 RID: 45110 RVA: 0x0026236D File Offset: 0x0026056D
		[Description("Gets or sets the client-side event which is fired after a hierarchy have been expanded.")]
		[NotifyParentProperty(true)]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Category("Client-side events")]
		public virtual string OnHierarchyExpanded
		{
			get
			{
				object obj = base.ViewState["OnHierarchyExpanded"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnHierarchyExpanded"] = value;
			}
		}

		// Token: 0x17003905 RID: 14597
		// (get) Token: 0x0600B037 RID: 45111 RVA: 0x00262380 File Offset: 0x00260580
		// (set) Token: 0x0600B038 RID: 45112 RVA: 0x002623AD File Offset: 0x002605AD
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("Gets or sets the client-side event which will be fired before a hierarchy is collapsed.")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		public virtual string OnHierarchyCollapsing
		{
			get
			{
				object obj = base.ViewState["OnHierarchyCollapsing"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnHierarchyCollapsing"] = value;
			}
		}

		// Token: 0x17003906 RID: 14598
		// (get) Token: 0x0600B039 RID: 45113 RVA: 0x002623C0 File Offset: 0x002605C0
		// (set) Token: 0x0600B03A RID: 45114 RVA: 0x002623ED File Offset: 0x002605ED
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("OnHierarchyCollapsed")]
		[DefaultValue("")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		public virtual string OnHierarchyCollapsed
		{
			get
			{
				object obj = base.ViewState["OnHierarchyCollapsed"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnHierarchyCollapsed"] = value;
			}
		}

		// Token: 0x17003907 RID: 14599
		// (get) Token: 0x0600B03B RID: 45115 RVA: 0x00262400 File Offset: 0x00260600
		// (set) Token: 0x0600B03C RID: 45116 RVA: 0x0026242D File Offset: 0x0026062D
		[Description("Gets or sets the client-side event which is fired before a group is expanded.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		public virtual string OnGroupExpanding
		{
			get
			{
				object obj = base.ViewState["OnGroupExpanding"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnGroupExpanding"] = value;
			}
		}

		// Token: 0x17003908 RID: 14600
		// (get) Token: 0x0600B03D RID: 45117 RVA: 0x00262440 File Offset: 0x00260640
		// (set) Token: 0x0600B03E RID: 45118 RVA: 0x0026246D File Offset: 0x0026066D
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		[Description("Gets or sets the client-side event which is fired after a group have been expanded.")]
		public virtual string OnGroupExpanded
		{
			get
			{
				object obj = base.ViewState["OnGroupExpanded"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnGroupExpanded"] = value;
			}
		}

		// Token: 0x17003909 RID: 14601
		// (get) Token: 0x0600B03F RID: 45119 RVA: 0x00262480 File Offset: 0x00260680
		// (set) Token: 0x0600B040 RID: 45120 RVA: 0x002624AD File Offset: 0x002606AD
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		[Description("Gets or sets the client-side event which is fired before a group is collapsed.")]
		public virtual string OnGroupCollapsing
		{
			get
			{
				object obj = base.ViewState["OnGroupCollapsing"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnGroupCollapsing"] = value;
			}
		}

		// Token: 0x1700390A RID: 14602
		// (get) Token: 0x0600B041 RID: 45121 RVA: 0x002624C0 File Offset: 0x002606C0
		// (set) Token: 0x0600B042 RID: 45122 RVA: 0x002624ED File Offset: 0x002606ED
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("Gets or sets the client-side event which is fired after a group have been collapsed.")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		public virtual string OnGroupCollapsed
		{
			get
			{
				object obj = base.ViewState["OnGroupCollapsed"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnGroupCollapsed"] = value;
			}
		}

		// Token: 0x1700390B RID: 14603
		// (get) Token: 0x0600B043 RID: 45123 RVA: 0x00262500 File Offset: 0x00260700
		// (set) Token: 0x0600B044 RID: 45124 RVA: 0x0026252D File Offset: 0x0026072D
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("Gets or sets the client-side event which will be fired before a active row changes.")]
		[DefaultValue("")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		public virtual string OnActiveRowChanging
		{
			get
			{
				object obj = base.ViewState["OnActiveRowChanging"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnActiveRowChanging"] = value;
			}
		}

		// Token: 0x1700390C RID: 14604
		// (get) Token: 0x0600B045 RID: 45125 RVA: 0x00262540 File Offset: 0x00260740
		// (set) Token: 0x0600B046 RID: 45126 RVA: 0x0026256D File Offset: 0x0026076D
		[Category("Client-side events")]
		[Description("Gets or sets the client-side event which will be fired after active row have changed.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		public virtual string OnActiveRowChanged
		{
			get
			{
				object obj = base.ViewState["OnActiveRowChanged"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnActiveRowChanged"] = value;
			}
		}

		// Token: 0x1700390D RID: 14605
		// (get) Token: 0x0600B047 RID: 45127 RVA: 0x00262580 File Offset: 0x00260780
		// (set) Token: 0x0600B048 RID: 45128 RVA: 0x002625AD File Offset: 0x002607AD
		[NotifyParentProperty(true)]
		[Description("Gets or sets the client-side event which will be fired before row have been deleted with GridClientDeleteColumn or deleteItem method.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Category("Client-side events")]
		public virtual string OnRowDeleting
		{
			get
			{
				object obj = base.ViewState["OnRowDeleting"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnRowDeleting"] = value;
			}
		}

		// Token: 0x1700390E RID: 14606
		// (get) Token: 0x0600B049 RID: 45129 RVA: 0x002625C0 File Offset: 0x002607C0
		// (set) Token: 0x0600B04A RID: 45130 RVA: 0x002625ED File Offset: 0x002607ED
		[DefaultValue("")]
		[Description("Gets or sets the client-side event which will be fired after a row have been deleted with GridClientDeleteColumn or deleteItem method.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		public virtual string OnRowDeleted
		{
			get
			{
				object obj = base.ViewState["OnRowDeleted"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnRowDeleted"] = value;
			}
		}

		// Token: 0x1700390F RID: 14607
		// (get) Token: 0x0600B04B RID: 45131 RVA: 0x00262600 File Offset: 0x00260800
		// (set) Token: 0x0600B04C RID: 45132 RVA: 0x0026262D File Offset: 0x0026082D
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		[Description("Gets or sets the client-side event which will be fired before the filter menu is shown.")]
		public virtual string OnFilterMenuShowing
		{
			get
			{
				object obj = base.ViewState["OnFilterMenuShowing"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnFilterMenuShowing"] = value;
			}
		}

		// Token: 0x17003910 RID: 14608
		// (get) Token: 0x0600B04D RID: 45133 RVA: 0x00262640 File Offset: 0x00260840
		// (set) Token: 0x0600B04E RID: 45134 RVA: 0x0026266D File Offset: 0x0026086D
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Description("Gets or sets the client-side event which will fired before a popup is shown.")]
		public virtual string OnPopUpShowing
		{
			get
			{
				object obj = base.ViewState["OnPopUpShowing"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnPopUpShowing"] = value;
			}
		}

		// Token: 0x17003911 RID: 14609
		// (get) Token: 0x0600B04F RID: 45135 RVA: 0x00262680 File Offset: 0x00260880
		// (set) Token: 0x0600B050 RID: 45136 RVA: 0x002626AD File Offset: 0x002608AD
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		[Description("Gets or sets the client-side event which will be fired when a RadGrid command occurs.")]
		public virtual string OnCommand
		{
			get
			{
				object obj = base.ViewState["OnCommand"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnCommand"] = value;
			}
		}

		// Token: 0x17003912 RID: 14610
		// (get) Token: 0x0600B051 RID: 45137 RVA: 0x002626C0 File Offset: 0x002608C0
		// (set) Token: 0x0600B052 RID: 45138 RVA: 0x002626ED File Offset: 0x002608ED
		[Description("Gets or sets the client-side event which will be fired when a user performs an action to the RadGrid control which will cause a postback or change the data. The event could be used to popup a dialog and verify if the user is certain in performing the current action.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		public virtual string OnUserAction
		{
			get
			{
				object obj = base.ViewState["OnUserAction"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnUserAction"] = value;
			}
		}

		// Token: 0x17003913 RID: 14611
		// (get) Token: 0x0600B053 RID: 45139 RVA: 0x00262700 File Offset: 0x00260900
		// (set) Token: 0x0600B054 RID: 45140 RVA: 0x0026272D File Offset: 0x0026092D
		[Description("Gets or sets the client-side event which will be fired when a row is data bound. Note that the event could only be used in client-side data binding scenario.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		public virtual string OnRowDataBound
		{
			get
			{
				object obj = base.ViewState["OnRowDataBound"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnRowDataBound"] = value;
			}
		}

		// Token: 0x17003914 RID: 14612
		// (get) Token: 0x0600B055 RID: 45141 RVA: 0x00262740 File Offset: 0x00260940
		// (set) Token: 0x0600B056 RID: 45142 RVA: 0x0026276D File Offset: 0x0026096D
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("Gets or sets a client-side event which helps in custom implementation of the batch editing functionality. Gets the value from the edit control which is positioned in the GridTemplateColumn.EditItemTemplate.")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		public virtual string OnBatchEditGetEditorValue
		{
			get
			{
				object obj = base.ViewState["OnBatchEditGetEditorValue"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnBatchEditGetEditorValue"] = value;
			}
		}

		// Token: 0x17003915 RID: 14613
		// (get) Token: 0x0600B057 RID: 45143 RVA: 0x00262780 File Offset: 0x00260980
		// (set) Token: 0x0600B058 RID: 45144 RVA: 0x002627AD File Offset: 0x002609AD
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("Gets or sets a client-side event which helps in custom implementation of the batch editing functionality. Gets the value from the cell which is positioned in the GridTemplateColumn.ItemTemplate.")]
		[DefaultValue("")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		public virtual string OnBatchEditGetCellValue
		{
			get
			{
				object obj = base.ViewState["OnBatchEditGetCellValue"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnBatchEditGetCellValue"] = value;
			}
		}

		// Token: 0x17003916 RID: 14614
		// (get) Token: 0x0600B059 RID: 45145 RVA: 0x002627C0 File Offset: 0x002609C0
		// (set) Token: 0x0600B05A RID: 45146 RVA: 0x002627ED File Offset: 0x002609ED
		[Category("Client-side events")]
		[Description("Gets or sets a client-side event which helps in custom implementation of the batch editing functionality. Sets the value from the edit control which is positioned in the GridTemplateColumn.EditItemTemplate.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		public virtual string OnBatchEditSetEditorValue
		{
			get
			{
				object obj = base.ViewState["OnBatchEditSetEditorValue"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnBatchEditSetEditorValue"] = value;
			}
		}

		// Token: 0x17003917 RID: 14615
		// (get) Token: 0x0600B05B RID: 45147 RVA: 0x00262800 File Offset: 0x00260A00
		// (set) Token: 0x0600B05C RID: 45148 RVA: 0x0026282D File Offset: 0x00260A2D
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[NotifyParentProperty(true)]
		[Description("Gets or sets a client-side event which helps in custom implementation of the batch editing functionality. Sets the value from the cell which is positioned in the GridTemplateColumn.ItemTemplate.")]
		[DefaultValue("")]
		[Category("Client-side events")]
		public virtual string OnBatchEditSetCellValue
		{
			get
			{
				object obj = base.ViewState["OnBatchEditSetCellValue"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnBatchEditSetCellValue"] = value;
			}
		}

		// Token: 0x17003918 RID: 14616
		// (get) Token: 0x0600B05D RID: 45149 RVA: 0x00262840 File Offset: 0x00260A40
		// (set) Token: 0x0600B05E RID: 45150 RVA: 0x0026286D File Offset: 0x00260A6D
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("Gets or sets the client-side event which will be fired when the GridTableView.EditMode is Batch and a cell value is changing. The event could be canceled.")]
		[DefaultValue("")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		public virtual string OnBatchEditCellValueChanging
		{
			get
			{
				object obj = base.ViewState["OnBatchEditCellValueChanging"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnBatchEditCellValueChanging"] = value;
			}
		}

		// Token: 0x17003919 RID: 14617
		// (get) Token: 0x0600B05F RID: 45151 RVA: 0x00262880 File Offset: 0x00260A80
		// (set) Token: 0x0600B060 RID: 45152 RVA: 0x002628AD File Offset: 0x00260AAD
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		[Description("Gets or sets the client-side event which will be fired when the GridTableView.EditMode is Batch and a cell value have been changed.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public virtual string OnBatchEditCellValueChanged
		{
			get
			{
				object obj = base.ViewState["OnBatchEditCellValueChanged"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnBatchEditCellValueChanged"] = value;
			}
		}

		// Token: 0x1700391A RID: 14618
		// (get) Token: 0x0600B061 RID: 45153 RVA: 0x002628C0 File Offset: 0x00260AC0
		// (set) Token: 0x0600B062 RID: 45154 RVA: 0x002628ED File Offset: 0x00260AED
		[Description("Gets or sets the client-side event which will be fired before opening a cell for edit.")]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Client-side events")]
		public virtual string OnBatchEditOpening
		{
			get
			{
				object obj = base.ViewState["OnBatchEditOpening"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnBatchEditOpening"] = value;
			}
		}

		// Token: 0x1700391B RID: 14619
		// (get) Token: 0x0600B063 RID: 45155 RVA: 0x00262900 File Offset: 0x00260B00
		// (set) Token: 0x0600B064 RID: 45156 RVA: 0x0026292D File Offset: 0x00260B2D
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("Gets or sets the client-side event which will be fired after a cell have been opened for edit.")]
		[Category("Client-side events")]
		public virtual string OnBatchEditOpened
		{
			get
			{
				object obj = base.ViewState["OnBatchEditOpened"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnBatchEditOpened"] = value;
			}
		}

		// Token: 0x1700391C RID: 14620
		// (get) Token: 0x0600B065 RID: 45157 RVA: 0x00262940 File Offset: 0x00260B40
		// (set) Token: 0x0600B066 RID: 45158 RVA: 0x0026296D File Offset: 0x00260B6D
		[Category("Client-side events")]
		[DefaultValue("")]
		[Description("Gets or sets the client-side event which will be fired before closing a cell for edit.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[NotifyParentProperty(true)]
		public virtual string OnBatchEditClosing
		{
			get
			{
				object obj = base.ViewState["OnBatchEditClosing"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnBatchEditClosing"] = value;
			}
		}

		// Token: 0x1700391D RID: 14621
		// (get) Token: 0x0600B067 RID: 45159 RVA: 0x00262980 File Offset: 0x00260B80
		// (set) Token: 0x0600B068 RID: 45160 RVA: 0x002629AD File Offset: 0x00260BAD
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[NotifyParentProperty(true)]
		[Description("Gets or sets the client-side event which will be fired after a cell have been closed for edit.")]
		[DefaultValue("")]
		[Category("Client-side events")]
		public virtual string OnBatchEditClosed
		{
			get
			{
				object obj = base.ViewState["OnBatchEditClosed"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnBatchEditClosed"] = value;
			}
		}
	}
}
