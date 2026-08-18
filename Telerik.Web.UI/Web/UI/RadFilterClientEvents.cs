using System;
using System.ComponentModel;
using System.Drawing.Design;

namespace Telerik.Web.UI
{
	// Token: 0x020018C3 RID: 6339
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class RadFilterClientEvents : StateManager
	{
		// Token: 0x170049F6 RID: 18934
		// (get) Token: 0x0600F56A RID: 62826 RVA: 0x0037BDB8 File Offset: 0x00379FB8
		// (set) Token: 0x0600F56B RID: 62827 RVA: 0x0037BDE5 File Offset: 0x00379FE5
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("This client-side event is fired after the RadFilter is created.")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		public virtual string OnFilterCreated
		{
			get
			{
				object obj = base.ViewState["OnFilterCreated"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnFilterCreated"] = value;
			}
		}

		// Token: 0x170049F7 RID: 18935
		// (get) Token: 0x0600F56C RID: 62828 RVA: 0x0037BDF8 File Offset: 0x00379FF8
		// (set) Token: 0x0600F56D RID: 62829 RVA: 0x0037BE25 File Offset: 0x0037A025
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[NotifyParentProperty(true)]
		[Description("This client-side event is fired before the RadFilter is created.")]
		[Category("Client-side events")]
		public virtual string OnFilterCreating
		{
			get
			{
				object obj = base.ViewState["OnFilterCreating"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnFilterCreating"] = value;
			}
		}

		// Token: 0x170049F8 RID: 18936
		// (get) Token: 0x0600F56E RID: 62830 RVA: 0x0037BE38 File Offset: 0x0037A038
		// (set) Token: 0x0600F56F RID: 62831 RVA: 0x0037BE65 File Offset: 0x0037A065
		[DefaultValue("")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		[Description("This client-side event is fired when RadFilter object is destroyed, i.e. on each <em>window.onunload</em>")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public virtual string OnFilterDestroying
		{
			get
			{
				object obj = base.ViewState["OnFilterDestroying"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnFilterDestroying"] = value;
			}
		}

		// Token: 0x170049F9 RID: 18937
		// (get) Token: 0x0600F570 RID: 62832 RVA: 0x0037BE78 File Offset: 0x0037A078
		// (set) Token: 0x0600F571 RID: 62833 RVA: 0x0037BEA5 File Offset: 0x0037A0A5
		[DefaultValue("")]
		[Description("Gets or sets the client-side event which is fired before RadFilter.ContextMenu is shown.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		public virtual string OnMenuShowing
		{
			get
			{
				object obj = base.ViewState["OnMenuShowing"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnMenuShowing"] = value;
			}
		}

		// Token: 0x170049FA RID: 18938
		// (get) Token: 0x0600F572 RID: 62834 RVA: 0x0037BEB8 File Offset: 0x0037A0B8
		// (set) Token: 0x0600F573 RID: 62835 RVA: 0x0037BEE5 File Offset: 0x0037A0E5
		[NotifyParentProperty(true)]
		[Category("Client-side events")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("Gets or sets the client-side event which is fired when RadFilter.ContextMenu is shown.")]
		public virtual string OnMenuShown
		{
			get
			{
				object obj = base.ViewState["OnMenuShown"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnMenuShown"] = value;
			}
		}
	}
}
