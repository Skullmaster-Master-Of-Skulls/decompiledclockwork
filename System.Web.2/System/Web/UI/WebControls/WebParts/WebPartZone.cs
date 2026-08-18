using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x020005B8 RID: 1464
	[Designer("System.Web.UI.Design.WebControls.WebParts.WebPartZoneDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[SupportsEventValidation]
	public class WebPartZone : WebPartZoneBase
	{
		// Token: 0x170015B5 RID: 5557
		// (get) Token: 0x060049F5 RID: 18933 RVA: 0x000F556F File Offset: 0x000F376F
		// (set) Token: 0x060049F6 RID: 18934 RVA: 0x000F5577 File Offset: 0x000F3777
		[Browsable(false)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateInstance(TemplateInstance.Single)]
		public virtual ITemplate ZoneTemplate
		{
			get
			{
				return this._zoneTemplate;
			}
			set
			{
				if (!base.DesignMode && this._registrationComplete)
				{
					throw new InvalidOperationException(SR.GetString("WebPart_SetZoneTemplateTooLate"));
				}
				this._zoneTemplate = value;
			}
		}

		// Token: 0x060049F7 RID: 18935 RVA: 0x000F55A0 File Offset: 0x000F37A0
		private void AddWebPartToList(WebPartCollection webParts, Control control)
		{
			WebPart webPart = control as WebPart;
			if (webPart == null && !(control is LiteralControl))
			{
				WebPartManager webPartManager = base.WebPartManager;
				if (webPartManager != null)
				{
					webPart = webPartManager.CreateWebPart(control);
				}
				else
				{
					webPart = WebPartManager.CreateWebPartStatic(control);
				}
			}
			if (webPart != null)
			{
				webParts.Add(webPart);
			}
		}

		// Token: 0x060049F8 RID: 18936 RVA: 0x000F55E8 File Offset: 0x000F37E8
		protected internal override WebPartCollection GetInitialWebParts()
		{
			WebPartCollection webPartCollection = new WebPartCollection();
			if (this.ZoneTemplate != null)
			{
				Control control = new NonParentingControl();
				this.ZoneTemplate.InstantiateIn(control);
				if (control.HasControls())
				{
					ControlCollection controls = control.Controls;
					foreach (object obj in controls)
					{
						Control control2 = (Control)obj;
						if (control2 is ContentPlaceHolder)
						{
							if (control2.HasControls())
							{
								Control[] array = new Control[control2.Controls.Count];
								control2.Controls.CopyTo(array, 0);
								foreach (Control control3 in array)
								{
									this.AddWebPartToList(webPartCollection, control3);
								}
							}
						}
						else
						{
							this.AddWebPartToList(webPartCollection, control2);
						}
					}
				}
			}
			return webPartCollection;
		}

		// Token: 0x060049F9 RID: 18937 RVA: 0x000F56D8 File Offset: 0x000F38D8
		protected internal override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			this._registrationComplete = true;
		}

		// Token: 0x040027C5 RID: 10181
		private ITemplate _zoneTemplate;

		// Token: 0x040027C6 RID: 10182
		private bool _registrationComplete;
	}
}
