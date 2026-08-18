using System;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AjaxControlToolkit.MaskedEditValidatorCompatibility
{
	// Token: 0x02000134 RID: 308
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public abstract class BaseValidator : BaseValidator, IBaseValidatorAccessor, IWebControlAccessor
	{
		// Token: 0x170002DF RID: 735
		// (get) Token: 0x060007A4 RID: 1956 RVA: 0x00014540 File Offset: 0x00012740
		internal ScriptManager ScriptManager
		{
			get
			{
				if (!this._scriptManagerChecked)
				{
					this._scriptManagerChecked = true;
					Page page = this.Page;
					if (page != null)
					{
						this._scriptManager = ScriptManager.GetCurrent(page);
					}
				}
				return this._scriptManager;
			}
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x00014578 File Offset: 0x00012778
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			if (this.ScriptManager == null || !this.ScriptManager.SupportsPartialRendering)
			{
				base.AddAttributesToRender(writer);
				return;
			}
			ValidatorHelper.DoBaseValidatorAddAttributes(this, this, writer);
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x0001459F File Offset: 0x0001279F
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			if (this.ScriptManager == null || !this.ScriptManager.SupportsPartialRendering)
			{
				return;
			}
			ValidatorHelper.DoInitRegistration(this.Page);
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x000145C9 File Offset: 0x000127C9
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (this.ScriptManager == null || !this.ScriptManager.SupportsPartialRendering)
			{
				return;
			}
			ValidatorHelper.DoPreRenderRegistration(this, this);
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x000145EF File Offset: 0x000127EF
		protected override void RegisterValidatorDeclaration()
		{
			if (this.ScriptManager == null || !this.ScriptManager.SupportsPartialRendering)
			{
				base.RegisterValidatorDeclaration();
				return;
			}
			ValidatorHelper.DoValidatorArrayDeclaration(this, typeof(BaseValidator));
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x060007A9 RID: 1961 RVA: 0x0001461D File Offset: 0x0001281D
		bool IBaseValidatorAccessor.RenderUpLevel
		{
			get
			{
				return base.RenderUplevel;
			}
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x060007AA RID: 1962 RVA: 0x00014625 File Offset: 0x00012825
		HtmlTextWriterTag IWebControlAccessor.TagKey
		{
			get
			{
				return this.TagKey;
			}
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x0001462D File Offset: 0x0001282D
		void IBaseValidatorAccessor.EnsureID()
		{
			base.EnsureID();
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x00014635 File Offset: 0x00012835
		string IBaseValidatorAccessor.GetControlRenderID(string name)
		{
			return base.GetControlRenderID(name);
		}

		// Token: 0x04000329 RID: 809
		private ScriptManager _scriptManager;

		// Token: 0x0400032A RID: 810
		private bool _scriptManagerChecked;
	}
}
