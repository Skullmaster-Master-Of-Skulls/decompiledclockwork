using System;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;
using Telerik.Licensing;

namespace Telerik.Web.UI
{
	// Token: 0x02001337 RID: 4919
	[LightweightRendering]
	[DefaultProperty("Text")]
	[ParseChildren(false)]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[TelerikToolboxCategory("Container")]
	[Designer("Telerik.Web.Design.RadToolTipDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[ToolboxBitmap(typeof(RadToolTip), "Telerik.Web.UI.ToolTip.png")]
	public class RadToolTip : RadToolTipBase
	{
		// Token: 0x170041FF RID: 16895
		// (get) Token: 0x0600CD5F RID: 52575 RVA: 0x002DBC07 File Offset: 0x002D9E07
		// (set) Token: 0x0600CD60 RID: 52576 RVA: 0x002DBC36 File Offset: 0x002D9E36
		[ClientControlProperty]
		[DefaultValue("")]
		[Category("Behavior")]
		public string TargetControlID
		{
			get
			{
				if (this.ViewState["TargetControlID"] == null)
				{
					return "";
				}
				return (string)this.ViewState["TargetControlID"];
			}
			set
			{
				this.ViewState["TargetControlID"] = value;
			}
		}

		// Token: 0x17004200 RID: 16896
		// (get) Token: 0x0600CD61 RID: 52577 RVA: 0x002DBC49 File Offset: 0x002D9E49
		// (set) Token: 0x0600CD62 RID: 52578 RVA: 0x002DBC74 File Offset: 0x002D9E74
		[DefaultValue(false)]
		[Category("Behavior")]
		public bool IsClientID
		{
			get
			{
				return this.ViewState["IsClientID"] != null && (bool)this.ViewState["IsClientID"];
			}
			set
			{
				this.ViewState["IsClientID"] = value;
			}
		}

		// Token: 0x0600CD63 RID: 52579 RVA: 0x002DBC8C File Offset: 0x002D9E8C
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			if (string.IsNullOrEmpty(this.TargetControlID))
			{
				return;
			}
			if (!this.IsClientID)
			{
				Control control = ChildControlHelper.FindControlRecursive(this, this.TargetControlID, null);
				if (control != null)
				{
					descriptor.AddScriptProperty("targetControlID", "\"" + control.ClientID + "\"");
					return;
				}
				base.ThrowControlNotFound(this.TargetControlID);
			}
		}

		// Token: 0x0600CD64 RID: 52580 RVA: 0x002DBCF4 File Offset: 0x002D9EF4
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeProperty<string>(descriptor, "targetControlID", this.TargetControlID, "");
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x0600CD65 RID: 52581 RVA: 0x002DBD14 File Offset: 0x002D9F14
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			base.DescribeClientEvents(descriptor);
		}
	}
}
