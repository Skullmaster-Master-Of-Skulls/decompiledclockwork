using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x0200052E RID: 1326
	[Designer("System.Web.UI.Design.WebControls.WebParts.CatalogZoneDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[SupportsEventValidation]
	public class CatalogZone : CatalogZoneBase
	{
		// Token: 0x06004330 RID: 17200 RVA: 0x000DD0B0 File Offset: 0x000DB2B0
		protected override CatalogPartCollection CreateCatalogParts()
		{
			CatalogPartCollection catalogPartCollection = new CatalogPartCollection();
			if (this._zoneTemplate != null)
			{
				Control control = new NonParentingControl();
				this._zoneTemplate.InstantiateIn(control);
				if (control.HasControls())
				{
					foreach (object obj in control.Controls)
					{
						Control control2 = (Control)obj;
						CatalogPart catalogPart = control2 as CatalogPart;
						if (catalogPart != null)
						{
							catalogPartCollection.Add(catalogPart);
						}
						else
						{
							LiteralControl literalControl = control2 as LiteralControl;
							if ((literalControl == null || literalControl.Text.Trim().Length != 0) && !base.DesignMode)
							{
								throw new InvalidOperationException(SR.GetString("CatalogZone_OnlyCatalogParts", new object[]
								{
									this.ID
								}));
							}
						}
					}
				}
			}
			return catalogPartCollection;
		}

		// Token: 0x170013B0 RID: 5040
		// (get) Token: 0x06004331 RID: 17201 RVA: 0x000DD194 File Offset: 0x000DB394
		// (set) Token: 0x06004332 RID: 17202 RVA: 0x000DD19C File Offset: 0x000DB39C
		[Browsable(false)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(CatalogZone))]
		[TemplateInstance(TemplateInstance.Single)]
		public virtual ITemplate ZoneTemplate
		{
			get
			{
				return this._zoneTemplate;
			}
			set
			{
				base.InvalidateCatalogParts();
				this._zoneTemplate = value;
			}
		}

		// Token: 0x040025C6 RID: 9670
		private ITemplate _zoneTemplate;
	}
}
