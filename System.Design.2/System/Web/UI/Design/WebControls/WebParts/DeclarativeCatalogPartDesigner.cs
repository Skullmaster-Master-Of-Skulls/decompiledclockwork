using System;
using System.ComponentModel;
using System.Design;
using System.Security.Permissions;
using System.Web.UI.WebControls.WebParts;

namespace System.Web.UI.Design.WebControls.WebParts
{
	// Token: 0x02000145 RID: 325
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class DeclarativeCatalogPartDesigner : CatalogPartDesigner
	{
		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000BAB RID: 2987 RVA: 0x0004ABF4 File Offset: 0x00048DF4
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

		// Token: 0x06000BAC RID: 2988 RVA: 0x0004AC68 File Offset: 0x00048E68
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

		// Token: 0x06000BAD RID: 2989 RVA: 0x0004ACE8 File Offset: 0x00048EE8
		protected override string GetEmptyDesignTimeHtml()
		{
			return base.CreatePlaceHolderDesignTimeHtml(SR.GetString("DeclarativeCatalogPartDesigner_Empty"));
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x0004ACFA File Offset: 0x00048EFA
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

		// Token: 0x04000709 RID: 1801
		private const string templateName = "WebPartsTemplate";

		// Token: 0x0400070A RID: 1802
		private DeclarativeCatalogPart _catalogPart;

		// Token: 0x0400070B RID: 1803
		private TemplateGroup _templateGroup;
	}
}
