using System;
using System.ComponentModel;
using System.Design;
using System.Security.Permissions;
using System.Web.UI.WebControls.WebParts;

namespace System.Web.UI.Design.WebControls.WebParts
{
	// Token: 0x02000537 RID: 1335
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class DeclarativeCatalogPartDesigner : CatalogPartDesigner
	{
		// Token: 0x170008EE RID: 2286
		// (get) Token: 0x06002F46 RID: 12102 RVA: 0x0010DE4C File Offset: 0x0010CE4C
		public override TemplateGroupCollection TemplateGroups
		{
			get
			{
				TemplateGroupCollection templateGroups = base.TemplateGroups;
				if (this._templateGroup == null)
				{
					this._templateGroup = new TemplateGroup("WebPartsTemplate", this._catalogPart.ControlStyle);
					this._templateGroup.AddTemplateDefinition(new TemplateDefinition(this, "WebPartsTemplate", this._catalogPart, "WebPartsTemplate", this._catalogPart.ControlStyle));
				}
				templateGroups.Add(this._templateGroup);
				return templateGroups;
			}
		}

		// Token: 0x06002F47 RID: 12103 RVA: 0x0010DEC0 File Offset: 0x0010CEC0
		public override string GetDesignTimeHtml()
		{
			if (!(this._catalogPart.Parent is CatalogZoneBase))
			{
				return base.CreateInvalidParentDesignTimeHtml(typeof(CatalogPart), typeof(CatalogZoneBase));
			}
			string result = string.Empty;
			try
			{
				if (((DeclarativeCatalogPart)base.ViewControl).WebPartsTemplate == null)
				{
					result = this.GetEmptyDesignTimeHtml();
				}
				else
				{
					result = string.Empty;
				}
			}
			catch (Exception e)
			{
				result = this.GetErrorDesignTimeHtml(e);
			}
			return result;
		}

		// Token: 0x06002F48 RID: 12104 RVA: 0x0010DF40 File Offset: 0x0010CF40
		protected override string GetEmptyDesignTimeHtml()
		{
			return base.CreatePlaceHolderDesignTimeHtml(SR.GetString("DeclarativeCatalogPartDesigner_Empty"));
		}

		// Token: 0x06002F49 RID: 12105 RVA: 0x0010DF52 File Offset: 0x0010CF52
		public override void Initialize(IComponent component)
		{
			ControlDesigner.VerifyInitializeArgument(component, typeof(DeclarativeCatalogPart));
			base.Initialize(component);
			this._catalogPart = (DeclarativeCatalogPart)component;
			if (base.View != null)
			{
				base.View.SetFlags(ViewFlags.TemplateEditing, true);
			}
		}

		// Token: 0x04002039 RID: 8249
		private const string templateName = "WebPartsTemplate";

		// Token: 0x0400203A RID: 8250
		private DeclarativeCatalogPart _catalogPart;

		// Token: 0x0400203B RID: 8251
		private TemplateGroup _templateGroup;
	}
}
