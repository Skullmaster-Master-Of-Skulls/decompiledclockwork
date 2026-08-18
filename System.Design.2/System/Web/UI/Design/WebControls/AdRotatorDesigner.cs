using System;
using System.Globalization;
using System.IO;
using System.Security.Permissions;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x02000099 RID: 153
	[SupportsPreviewControl(true)]
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class AdRotatorDesigner : DataBoundControlDesigner
	{
		// Token: 0x06000497 RID: 1175 RVA: 0x000150D0 File Offset: 0x000132D0
		public override string GetDesignTimeHtml()
		{
			AdRotator adRotator = (AdRotator)base.ViewControl;
			StringWriter stringWriter = new StringWriter(CultureInfo.CurrentCulture);
			DesignTimeHtmlTextWriter writer = new DesignTimeHtmlTextWriter(stringWriter);
			HyperLink hyperLink = new HyperLink();
			hyperLink.ID = adRotator.ID;
			hyperLink.NavigateUrl = "";
			hyperLink.Target = adRotator.Target;
			hyperLink.AccessKey = adRotator.AccessKey;
			hyperLink.Enabled = adRotator.Enabled;
			hyperLink.TabIndex = adRotator.TabIndex;
			hyperLink.Style.Value = adRotator.Style.Value;
			hyperLink.RenderBeginTag(writer);
			Image image = new Image();
			image.ApplyStyle(adRotator.ControlStyle);
			image.SetDesignMode();
			image.ImageUrl = "";
			image.AlternateText = adRotator.ID;
			image.ToolTip = adRotator.ToolTip;
			image.RenderControl(writer);
			hyperLink.RenderEndTag(writer);
			return stringWriter.ToString();
		}
	}
}
