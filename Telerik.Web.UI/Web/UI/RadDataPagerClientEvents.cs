using System;
using System.ComponentModel;
using System.Drawing.Design;

namespace Telerik.Web.UI
{
	// Token: 0x0200195A RID: 6490
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class RadDataPagerClientEvents : StateManager
	{
		// Token: 0x17004BF2 RID: 19442
		// (get) Token: 0x0600FB53 RID: 64339 RVA: 0x00389F4C File Offset: 0x0038814C
		// (set) Token: 0x0600FB54 RID: 64340 RVA: 0x00389F79 File Offset: 0x00388179
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Description("This client-side event is fired after the RadDataPager is created.")]
		[NotifyParentProperty(true)]
		public virtual string OnDataPagerCreated
		{
			get
			{
				object obj = base.ViewState["OnDataPagerCreated"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnDataPagerCreated"] = value;
			}
		}

		// Token: 0x17004BF3 RID: 19443
		// (get) Token: 0x0600FB55 RID: 64341 RVA: 0x00389F8C File Offset: 0x0038818C
		// (set) Token: 0x0600FB56 RID: 64342 RVA: 0x00389FB9 File Offset: 0x003881B9
		[Description("This client-side event is fired before the RadDataPager is created.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		public virtual string OnDataPagerCreating
		{
			get
			{
				object obj = base.ViewState["OnDataPagerCreating"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnDataPagerCreating"] = value;
			}
		}

		// Token: 0x17004BF4 RID: 19444
		// (get) Token: 0x0600FB57 RID: 64343 RVA: 0x00389FCC File Offset: 0x003881CC
		// (set) Token: 0x0600FB58 RID: 64344 RVA: 0x00389FF9 File Offset: 0x003881F9
		[Description("This client-side event is fired when RadDataPager object is destroyed, i.e. on each <em>window.onunload</em>")]
		[NotifyParentProperty(true)]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Category("Client-side events")]
		public virtual string OnDataPagerDestroying
		{
			get
			{
				object obj = base.ViewState["OnDataPagerDestroying"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnDataPagerDestroying"] = value;
			}
		}

		// Token: 0x17004BF5 RID: 19445
		// (get) Token: 0x0600FB59 RID: 64345 RVA: 0x0038A00C File Offset: 0x0038820C
		// (set) Token: 0x0600FB5A RID: 64346 RVA: 0x0038A039 File Offset: 0x00388239
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("This client-side event is fired when current page index is set on RadDataPager object")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		public virtual string OnPageIndexChanging
		{
			get
			{
				object obj = base.ViewState["OnPageIndexChanging"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnPageIndexChanging"] = value;
			}
		}

		// Token: 0x17004BF6 RID: 19446
		// (get) Token: 0x0600FB5B RID: 64347 RVA: 0x0038A04C File Offset: 0x0038824C
		// (set) Token: 0x0600FB5C RID: 64348 RVA: 0x0038A079 File Offset: 0x00388279
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("This client-side event is fired when current page size is set on RadDataPager object")]
		[DefaultValue("")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		public virtual string OnPageSizeChanging
		{
			get
			{
				object obj = base.ViewState["OnPageSizeChanging"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnPageSizeChanging"] = value;
			}
		}
	}
}
