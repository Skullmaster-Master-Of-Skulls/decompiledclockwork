using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000654 RID: 1620
	[PersistChildren(false)]
	[DefaultProperty("MethodName")]
	[Designer("System.Web.UI.Design.WebControls.SubstitutionDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ParseChildren(true)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class Substitution : Control
	{
		// Token: 0x17001415 RID: 5141
		// (get) Token: 0x06004F70 RID: 20336 RVA: 0x0013F7B4 File Offset: 0x0013E7B4
		// (set) Token: 0x06004F71 RID: 20337 RVA: 0x0013F7E1 File Offset: 0x0013E7E1
		[DefaultValue("")]
		[WebSysDescription("Substitution_MethodNameDescr")]
		[WebCategory("Behavior")]
		public virtual string MethodName
		{
			get
			{
				string text = this.ViewState["MethodName"] as string;
				if (text != null)
				{
					return text;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["MethodName"] = value;
			}
		}

		// Token: 0x06004F72 RID: 20338 RVA: 0x0013F7F4 File Offset: 0x0013E7F4
		protected override ControlCollection CreateControlCollection()
		{
			return new EmptyControlCollection(this);
		}

		// Token: 0x06004F73 RID: 20339 RVA: 0x0013F7FC File Offset: 0x0013E7FC
		[ReflectionPermission(SecurityAction.Assert, Flags = ReflectionPermissionFlag.MemberAccess)]
		private HttpResponseSubstitutionCallback GetDelegate(Type targetType, string methodName)
		{
			return (HttpResponseSubstitutionCallback)Delegate.CreateDelegate(typeof(HttpResponseSubstitutionCallback), targetType, methodName);
		}

		// Token: 0x06004F74 RID: 20340 RVA: 0x0013F814 File Offset: 0x0013E814
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			for (Control parent = this.Parent; parent != null; parent = parent.Parent)
			{
				if (parent is BasePartialCachingControl)
				{
					throw new HttpException(SR.GetString("Substitution_CannotBeInCachedControl"));
				}
			}
		}

		// Token: 0x06004F75 RID: 20341 RVA: 0x0013F853 File Offset: 0x0013E853
		protected internal override void Render(HtmlTextWriter writer)
		{
			this.RenderMarkup(writer);
		}

		// Token: 0x06004F76 RID: 20342 RVA: 0x0013F85C File Offset: 0x0013E85C
		internal void RenderMarkup(HtmlTextWriter writer)
		{
			if (this.MethodName.Length == 0)
			{
				return;
			}
			TemplateControl templateControl = base.TemplateControl;
			if (templateControl == null)
			{
				return;
			}
			HttpResponseSubstitutionCallback httpResponseSubstitutionCallback = null;
			try
			{
				httpResponseSubstitutionCallback = this.GetDelegate(templateControl.GetType(), this.MethodName);
			}
			catch
			{
			}
			if (httpResponseSubstitutionCallback == null)
			{
				throw new HttpException(SR.GetString("Substitution_BadMethodName", new object[]
				{
					this.MethodName
				}));
			}
			this.Page.Response.WriteSubstitution(httpResponseSubstitutionCallback);
		}
	}
}
