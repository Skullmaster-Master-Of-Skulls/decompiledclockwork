using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Text;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000FC9 RID: 4041
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class AjaxClientEvents : ObjectWithState
	{
		// Token: 0x06009CDE RID: 40158 RVA: 0x0022ED73 File Offset: 0x0022CF73
		public AjaxClientEvents(StateBag OwnerStateBag) : base("ce_", OwnerStateBag)
		{
		}

		// Token: 0x170031AA RID: 12714
		// (get) Token: 0x06009CDF RID: 40159 RVA: 0x0022ED84 File Offset: 0x0022CF84
		internal string ClientObjectString
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("{");
				stringBuilder.AppendFormat("OnRequestStart:\"{0}\"", this.OnRequestStart);
				stringBuilder.Append(",");
				stringBuilder.AppendFormat("OnResponseEnd:\"{0}\"", this.OnResponseEnd);
				stringBuilder.Append("}");
				return stringBuilder.ToString();
			}
		}

		// Token: 0x170031AB RID: 12715
		// (get) Token: 0x06009CE0 RID: 40160 RVA: 0x0022EDE8 File Offset: 0x0022CFE8
		// (set) Token: 0x06009CE1 RID: 40161 RVA: 0x0022EE15 File Offset: 0x0022D015
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("This event is fired when a request to the server is started.")]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
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
				return "";
			}
			set
			{
				base.ViewState["OnRequestStart"] = value;
			}
		}

		// Token: 0x170031AC RID: 12716
		// (get) Token: 0x06009CE2 RID: 40162 RVA: 0x0022EE28 File Offset: 0x0022D028
		// (set) Token: 0x06009CE3 RID: 40163 RVA: 0x0022EE55 File Offset: 0x0022D055
		[NotifyParentProperty(true)]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Category("Client-side events")]
		[Description("This event is fired when a response from the server is processed.")]
		public virtual string OnResponseEnd
		{
			get
			{
				object obj = base.ViewState["OnResponseEnd"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				base.ViewState["OnResponseEnd"] = value;
			}
		}
	}
}
