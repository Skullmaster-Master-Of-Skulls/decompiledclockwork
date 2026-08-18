using System;
using System.ComponentModel;
using System.Drawing.Design;

namespace Telerik.Web.UI
{
	// Token: 0x02000212 RID: 530
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class RadDataFormClientEvents : StateManager
	{
		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x0600137C RID: 4988 RVA: 0x00044AC8 File Offset: 0x00042CC8
		// (set) Token: 0x0600137D RID: 4989 RVA: 0x00044AF5 File Offset: 0x00042CF5
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("This client-side event is fired after the RadDataForm is created.")]
		[DefaultValue("")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		public virtual string OnDataFormCreated
		{
			get
			{
				object obj = base.ViewState["OnDataFormCreated"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnDataFormCreated"] = value;
			}
		}

		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x0600137E RID: 4990 RVA: 0x00044B08 File Offset: 0x00042D08
		// (set) Token: 0x0600137F RID: 4991 RVA: 0x00044B35 File Offset: 0x00042D35
		[Description("This client-side event is fired before the RadDataForm is created.")]
		[NotifyParentProperty(true)]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Category("Client-side events")]
		public virtual string OnDataFormCreating
		{
			get
			{
				object obj = base.ViewState["OnDataFormCreating"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnDataFormCreating"] = value;
			}
		}

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x06001380 RID: 4992 RVA: 0x00044B48 File Offset: 0x00042D48
		// (set) Token: 0x06001381 RID: 4993 RVA: 0x00044B75 File Offset: 0x00042D75
		[Category("Client-side events")]
		[Description("This client-side event is fired when RadDataForm object is destroyed, i.e. on each <em>window.onunload</em>")]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public virtual string OnDataFormDestroying
		{
			get
			{
				object obj = base.ViewState["OnDataFormDestroying"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnDataFormDestroying"] = value;
			}
		}

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x06001382 RID: 4994 RVA: 0x00044B88 File Offset: 0x00042D88
		// (set) Token: 0x06001383 RID: 4995 RVA: 0x00044BA8 File Offset: 0x00042DA8
		[Description("This client-side event is fired when a RadDataForm command occurs.")]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Client-side events")]
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

		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x06001384 RID: 4996 RVA: 0x00044BBB File Offset: 0x00042DBB
		// (set) Token: 0x06001385 RID: 4997 RVA: 0x00044BDB File Offset: 0x00042DDB
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("This client-side event is fired when values from form elements need to be used for insert and update methods")]
		[DefaultValue("")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		public virtual string OnGetValues
		{
			get
			{
				return ((string)base.ViewState["OnGetValues"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["OnGetValues"] = value;
			}
		}

		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x06001386 RID: 4998 RVA: 0x00044BEE File Offset: 0x00042DEE
		// (set) Token: 0x06001387 RID: 4999 RVA: 0x00044C0E File Offset: 0x00042E0E
		[Category("Client-side events")]
		[Description("This client-side event is fired when values are available and need to be set to the input fields in the template.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		public virtual string OnSetValues
		{
			get
			{
				return ((string)base.ViewState["OnSetValues"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["OnSetValues"] = value;
			}
		}
	}
}
