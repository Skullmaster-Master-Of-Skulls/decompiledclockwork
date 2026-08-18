using System;
using System.ComponentModel;
using System.Drawing.Design;

namespace Telerik.Web.UI
{
	// Token: 0x020000FF RID: 255
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class ClientDataSourceClientEvents : StateManager
	{
		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06000AB2 RID: 2738 RVA: 0x00026848 File Offset: 0x00024A48
		// (set) Token: 0x06000AB3 RID: 2739 RVA: 0x00026875 File Offset: 0x00024A75
		[NotifyParentProperty(true)]
		[Description("Gets or sets the client-side event which will be fired when a RadClientDataSource command occurs.")]
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
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

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06000AB4 RID: 2740 RVA: 0x00026888 File Offset: 0x00024A88
		// (set) Token: 0x06000AB5 RID: 2741 RVA: 0x000268B5 File Offset: 0x00024AB5
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("Gets or sets the client-side event which will be fired when a RadClientDataSource remote service or page request is started.")]
		[Category("Client-side events")]
		public virtual string OnRequestStart
		{
			get
			{
				object obj = base.ViewState["OnRequestStart"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnRequestStart"] = value;
			}
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06000AB6 RID: 2742 RVA: 0x000268C8 File Offset: 0x00024AC8
		// (set) Token: 0x06000AB7 RID: 2743 RVA: 0x000268F5 File Offset: 0x00024AF5
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("Gets or sets the client-side event which will be fired when a RadClientDataSource remote request finished.")]
		[DefaultValue("")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		public virtual string OnRequestEnd
		{
			get
			{
				object obj = base.ViewState["OnRequestEnd"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnRequestEnd"] = value;
			}
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06000AB8 RID: 2744 RVA: 0x00026908 File Offset: 0x00024B08
		// (set) Token: 0x06000AB9 RID: 2745 RVA: 0x00026935 File Offset: 0x00024B35
		[Description("Gets or sets the RadClientDataSource client-side event which will be fired when the remote request has failed.")]
		[DefaultValue("")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public virtual string OnRequestFailed
		{
			get
			{
				object obj = base.ViewState["OnRequestFailed"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnRequestFailed"] = value;
			}
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06000ABA RID: 2746 RVA: 0x00026948 File Offset: 0x00024B48
		// (set) Token: 0x06000ABB RID: 2747 RVA: 0x00026975 File Offset: 0x00024B75
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Client-side events")]
		[Description("Gets or sets the RadClientDataSource client-side event which will be fired when a custom mapping of the request parameters can be perfomred.")]
		public virtual string OnCustomParameter
		{
			get
			{
				object obj = base.ViewState["OnCustomParameter"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnCustomParameter"] = value;
			}
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06000ABC RID: 2748 RVA: 0x00026988 File Offset: 0x00024B88
		// (set) Token: 0x06000ABD RID: 2749 RVA: 0x000269B5 File Offset: 0x00024BB5
		[NotifyParentProperty(true)]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Category("Client-side events")]
		[Description("Gets or sets the RadClientDataSource client-side event which will be fired when a change in the data is applied.")]
		public virtual string OnChange
		{
			get
			{
				object obj = base.ViewState["OnChange"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnChange"] = value;
			}
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06000ABE RID: 2750 RVA: 0x000269C8 File Offset: 0x00024BC8
		// (set) Token: 0x06000ABF RID: 2751 RVA: 0x000269F5 File Offset: 0x00024BF5
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("Gets or sets the RadClientDataSource client-side event which will be fired after the data source saves all data item changes. Used in batch editing")]
		[DefaultValue("")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		public virtual string OnSync
		{
			get
			{
				object obj = base.ViewState["OnSync"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnSync"] = value;
			}
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06000AC0 RID: 2752 RVA: 0x00026A08 File Offset: 0x00024C08
		// (set) Token: 0x06000AC1 RID: 2753 RVA: 0x00026A35 File Offset: 0x00024C35
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		[Description("Gets or sets the RadClientDataSource client-side event which will be fired after the data has been requested from the service")]
		public virtual string OnDataRequested
		{
			get
			{
				object obj = base.ViewState["OnDataRequested"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnDataRequested"] = value;
			}
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06000AC2 RID: 2754 RVA: 0x00026A48 File Offset: 0x00024C48
		// (set) Token: 0x06000AC3 RID: 2755 RVA: 0x00026A75 File Offset: 0x00024C75
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		[Description("Gets or sets the RadClientDataSource client-side event which will be fired after the count has been requested from the service")]
		public virtual string OnCountRequested
		{
			get
			{
				object obj = base.ViewState["OnCountRequested"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnCountRequested"] = value;
			}
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06000AC4 RID: 2756 RVA: 0x00026A88 File Offset: 0x00024C88
		// (set) Token: 0x06000AC5 RID: 2757 RVA: 0x00026AB5 File Offset: 0x00024CB5
		[Category("Client-side events")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[NotifyParentProperty(true)]
		[Description("Gets or sets the RadClientDataSource client-side event which can be used to additionally parse the response before it is further processed by the control")]
		public virtual string OnDataParse
		{
			get
			{
				object obj = base.ViewState["OnDataParse"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnDataParse"] = value;
			}
		}
	}
}
