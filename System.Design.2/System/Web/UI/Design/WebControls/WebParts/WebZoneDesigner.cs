using System;
using System.ComponentModel;
using System.Security.Permissions;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace System.Web.UI.Design.WebControls.WebParts
{
	// Token: 0x02000156 RID: 342
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public abstract class WebZoneDesigner : ControlDesigner
	{
		// Token: 0x06000BEF RID: 3055 RVA: 0x0000C822 File Offset: 0x0000AA22
		internal WebZoneDesigner()
		{
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000BF0 RID: 3056 RVA: 0x0004B7A5 File Offset: 0x000499A5
		internal TemplateDefinition TemplateDefinition
		{
			get
			{
				return new TemplateDefinition(this, "ZoneTemplate", base.Component, "ZoneTemplate", ((WebControl)base.ViewControl).ControlStyle, true);
			}
		}

		// Token: 0x06000BF1 RID: 3057 RVA: 0x0004B7D0 File Offset: 0x000499D0
		internal TemplateGroup CreateZoneTemplateGroup()
		{
			TemplateGroup templateGroup = new TemplateGroup("ZoneTemplate", ((WebControl)base.ViewControl).ControlStyle);
			templateGroup.AddTemplateDefinition(new TemplateDefinition(this, "ZoneTemplate", base.Component, "ZoneTemplate", ((WebControl)base.ViewControl).ControlStyle));
			return templateGroup;
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000BF2 RID: 3058 RVA: 0x00003B0F File Offset: 0x00001D0F
		protected override bool UsePreviewControl
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000BF3 RID: 3059 RVA: 0x0004B825 File Offset: 0x00049A25
		public override void Initialize(IComponent component)
		{
			ControlDesigner.VerifyInitializeArgument(component, typeof(WebZone));
			base.Initialize(component);
		}

		// Token: 0x04000718 RID: 1816
		internal const string _templateName = "ZoneTemplate";
	}
}
