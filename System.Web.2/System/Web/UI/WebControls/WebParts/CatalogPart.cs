using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x0200052B RID: 1323
	[Bindable(false)]
	[Designer("System.Web.UI.Design.WebControls.WebParts.CatalogPartDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public abstract class CatalogPart : Part
	{
		// Token: 0x170013AA RID: 5034
		// (get) Token: 0x0600430E RID: 17166 RVA: 0x000DC938 File Offset: 0x000DAB38
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string DisplayTitle
		{
			get
			{
				string text = this.Title;
				if (string.IsNullOrEmpty(text))
				{
					text = SR.GetString("Part_Untitled");
				}
				return text;
			}
		}

		// Token: 0x170013AB RID: 5035
		// (get) Token: 0x0600430F RID: 17167 RVA: 0x000DC960 File Offset: 0x000DAB60
		protected WebPartManager WebPartManager
		{
			get
			{
				return this._webPartManager;
			}
		}

		// Token: 0x170013AC RID: 5036
		// (get) Token: 0x06004310 RID: 17168 RVA: 0x000DC968 File Offset: 0x000DAB68
		protected CatalogZoneBase Zone
		{
			get
			{
				return this._zone;
			}
		}

		// Token: 0x06004311 RID: 17169
		public abstract WebPartDescriptionCollection GetAvailableWebPartDescriptions();

		// Token: 0x06004312 RID: 17170 RVA: 0x000DC970 File Offset: 0x000DAB70
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		protected override IDictionary GetDesignModeState()
		{
			IDictionary dictionary = new HybridDictionary(1);
			dictionary["Zone"] = this.Zone;
			return dictionary;
		}

		// Token: 0x06004313 RID: 17171
		public abstract WebPart GetWebPart(WebPartDescription description);

		// Token: 0x06004314 RID: 17172 RVA: 0x000DC996 File Offset: 0x000DAB96
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (this.Zone == null)
			{
				throw new InvalidOperationException(SR.GetString("CatalogPart_MustBeInZone", new object[]
				{
					this.ID
				}));
			}
		}

		// Token: 0x06004315 RID: 17173 RVA: 0x000DC9C8 File Offset: 0x000DABC8
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		protected override void SetDesignModeState(IDictionary data)
		{
			if (data != null)
			{
				object obj = data["Zone"];
				if (obj != null)
				{
					this.SetZone((CatalogZoneBase)obj);
				}
			}
		}

		// Token: 0x06004316 RID: 17174 RVA: 0x000DC9F3 File Offset: 0x000DABF3
		internal void SetWebPartManager(WebPartManager webPartManager)
		{
			this._webPartManager = webPartManager;
		}

		// Token: 0x06004317 RID: 17175 RVA: 0x000DC9FC File Offset: 0x000DABFC
		internal void SetZone(CatalogZoneBase zone)
		{
			this._zone = zone;
		}

		// Token: 0x040025BF RID: 9663
		private WebPartManager _webPartManager;

		// Token: 0x040025C0 RID: 9664
		private CatalogZoneBase _zone;
	}
}
