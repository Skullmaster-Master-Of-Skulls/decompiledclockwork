using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x020004E1 RID: 1249
	[DefaultProperty("MethodName")]
	[Designer("System.Web.UI.Design.WebControls.SubstitutionDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ParseChildren(true)]
	[PersistChildren(false)]
	public class Substitution : Control
	{
		// Token: 0x1700122F RID: 4655
		// (get) Token: 0x06003E66 RID: 15974 RVA: 0x000C92F0 File Offset: 0x000C74F0
		// (set) Token: 0x06003E67 RID: 15975 RVA: 0x000C931D File Offset: 0x000C751D
		[DefaultValue("")]
		[WebCategory("Behavior")]
		[WebSysDescription("Substitution_MethodNameDescr")]
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

		// Token: 0x06003E68 RID: 15976 RVA: 0x00060B2F File Offset: 0x0005ED2F
		protected override ControlCollection CreateControlCollection()
		{
			return new EmptyControlCollection(this);
		}

		// Token: 0x06003E69 RID: 15977 RVA: 0x000C9330 File Offset: 0x000C7530
		[ReflectionPermission(SecurityAction.Assert, Flags = ReflectionPermissionFlag.MemberAccess)]
		private HttpResponseSubstitutionCallback GetDelegate(Type targetType, string methodName)
		{
			return (HttpResponseSubstitutionCallback)Delegate.CreateDelegate(typeof(HttpResponseSubstitutionCallback), targetType, methodName);
		}

		// Token: 0x06003E6A RID: 15978 RVA: 0x000C9348 File Offset: 0x000C7548
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

		// Token: 0x06003E6B RID: 15979 RVA: 0x000C9387 File Offset: 0x000C7587
		protected internal override void Render(HtmlTextWriter writer)
		{
			this.RenderMarkup(writer);
		}

		// Token: 0x06003E6C RID: 15980 RVA: 0x000C9390 File Offset: 0x000C7590
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
