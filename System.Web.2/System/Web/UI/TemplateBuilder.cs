using System;
using System.Collections;
using System.ComponentModel.Design;

namespace System.Web.UI
{
	// Token: 0x0200030C RID: 780
	public class TemplateBuilder : ControlBuilder, ITemplate
	{
		// Token: 0x06002401 RID: 9217 RVA: 0x00075A89 File Offset: 0x00073C89
		public TemplateBuilder()
		{
			this._allowMultipleInstances = true;
		}

		// Token: 0x06002402 RID: 9218 RVA: 0x00075A98 File Offset: 0x00073C98
		public override void Init(TemplateParser parser, ControlBuilder parentBuilder, Type type, string tagName, string ID, IDictionary attribs)
		{
			base.Init(parser, parentBuilder, type, tagName, ID, attribs);
			if (base.InPageTheme && base.ParentBuilder != null && base.ParentBuilder.IsControlSkin)
			{
				((PageThemeParser)base.Parser).CurrentSkinBuilder = parentBuilder;
			}
		}

		// Token: 0x06002403 RID: 9219 RVA: 0x00075AD7 File Offset: 0x00073CD7
		public override void CloseControl()
		{
			base.CloseControl();
			if (base.InPageTheme && base.ParentBuilder != null && base.ParentBuilder.IsControlSkin)
			{
				((PageThemeParser)base.Parser).CurrentSkinBuilder = null;
			}
		}

		// Token: 0x17000A0A RID: 2570
		// (get) Token: 0x06002404 RID: 9220 RVA: 0x00075B0D File Offset: 0x00073D0D
		// (set) Token: 0x06002405 RID: 9221 RVA: 0x00075B15 File Offset: 0x00073D15
		internal bool AllowMultipleInstances
		{
			get
			{
				return this._allowMultipleInstances;
			}
			set
			{
				this._allowMultipleInstances = value;
			}
		}

		// Token: 0x06002406 RID: 9222 RVA: 0x00004335 File Offset: 0x00002535
		public override object BuildObject()
		{
			return this;
		}

		// Token: 0x06002407 RID: 9223 RVA: 0x00075B1E File Offset: 0x00073D1E
		public override bool NeedsTagInnerText()
		{
			return base.InDesigner;
		}

		// Token: 0x06002408 RID: 9224 RVA: 0x00075B26 File Offset: 0x00073D26
		internal void SetDesignerHost(IDesignerHost designerHost)
		{
			this._designerHost = designerHost;
		}

		// Token: 0x06002409 RID: 9225 RVA: 0x00075B2F File Offset: 0x00073D2F
		public override void SetTagInnerText(string text)
		{
			this._tagInnerText = text;
		}

		// Token: 0x0600240A RID: 9226 RVA: 0x00075B38 File Offset: 0x00073D38
		public virtual void InstantiateIn(Control container)
		{
			IServiceProvider serviceProvider = null;
			if (this._designerHost != null)
			{
				serviceProvider = this._designerHost;
			}
			else if (!base.IsNoCompile)
			{
				ServiceContainer serviceContainer = new ServiceContainer();
				if (container is IThemeResolutionService)
				{
					serviceContainer.AddService(typeof(IThemeResolutionService), (IThemeResolutionService)container);
				}
				if (container is IFilterResolutionService)
				{
					serviceContainer.AddService(typeof(IFilterResolutionService), (IFilterResolutionService)container);
				}
				serviceProvider = serviceContainer;
			}
			HttpContext httpContext = null;
			TemplateControl templateControl = null;
			TemplateControl templateControl2 = container as TemplateControl;
			if (templateControl2 != null)
			{
				httpContext = HttpContext.Current;
				if (httpContext != null)
				{
					templateControl = httpContext.TemplateControl;
				}
			}
			try
			{
				if (!base.IsNoCompile)
				{
					base.SetServiceProvider(serviceProvider);
				}
				if (httpContext != null)
				{
					httpContext.TemplateControl = templateControl2;
				}
				this.BuildChildren(container);
			}
			finally
			{
				if (!base.IsNoCompile)
				{
					base.SetServiceProvider(null);
				}
				if (httpContext != null)
				{
					httpContext.TemplateControl = templateControl;
				}
			}
		}

		// Token: 0x17000A0B RID: 2571
		// (get) Token: 0x0600240B RID: 9227 RVA: 0x00075C14 File Offset: 0x00073E14
		// (set) Token: 0x0600240C RID: 9228 RVA: 0x00075B2F File Offset: 0x00073D2F
		public virtual string Text
		{
			get
			{
				return this._tagInnerText;
			}
			set
			{
				this._tagInnerText = value;
			}
		}

		// Token: 0x04001CE2 RID: 7394
		internal string _tagInnerText;

		// Token: 0x04001CE3 RID: 7395
		private bool _allowMultipleInstances;

		// Token: 0x04001CE4 RID: 7396
		private IDesignerHost _designerHost;
	}
}
