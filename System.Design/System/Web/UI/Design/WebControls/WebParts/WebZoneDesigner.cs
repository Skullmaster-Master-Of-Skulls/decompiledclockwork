using System;
using System.ComponentModel;
using System.Security.Permissions;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace System.Web.UI.Design.WebControls.WebParts
{
	// Token: 0x02000530 RID: 1328
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public abstract class WebZoneDesigner : ControlDesigner
	{
		// Token: 0x06002F21 RID: 12065 RVA: 0x0010D872 File Offset: 0x0010C872
		internal WebZoneDesigner()
		{
		}

		// Token: 0x170008E5 RID: 2277
		// (get) Token: 0x06002F22 RID: 12066 RVA: 0x0010D87A File Offset: 0x0010C87A
		internal TemplateDefinition TemplateDefinition
		{
			get
			{
				return new TemplateDefinition(this, "ZoneTemplate", base.Component, "ZoneTemplate", ((WebControl)base.ViewControl).ControlStyle, true);
			}
		}

		// Token: 0x06002F23 RID: 12067 RVA: 0x0010D8A4 File Offset: 0x0010C8A4
		internal TemplateGroup CreateZoneTemplateGroup()
		{
			TemplateGroup templateGroup = new TemplateGroup("ZoneTemplate", ((WebControl)base.ViewControl).ControlStyle);
			templateGroup.AddTemplateDefinition(new TemplateDefinition(this, "ZoneTemplate", base.Component, "ZoneTemplate", ((WebControl)base.ViewControl).ControlStyle));
			return templateGroup;
		}

		// Token: 0x170008E6 RID: 2278
		// (get) Token: 0x06002F24 RID: 12068 RVA: 0x0010D8F9 File Offset: 0x0010C8F9
		protected override bool UsePreviewControl
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002F25 RID: 12069 RVA: 0x0010D8FC File Offset: 0x0010C8FC
		public override void Initialize(IComponent component)
		{
			ControlDesigner.VerifyInitializeArgument(component, typeof(WebZone));
			base.Initialize(component);
		}

		// Token: 0x0400202D RID: 8237
		internal const string _templateName = "ZoneTemplate";
	}
}
