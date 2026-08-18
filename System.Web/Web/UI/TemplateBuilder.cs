using System;
using System.Collections;
using System.ComponentModel.Design;
using System.Security.Permissions;

namespace System.Web.UI
{
	// Token: 0x02000398 RID: 920
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class TemplateBuilder : ControlBuilder, ITemplate
	{
		// Token: 0x06002CFC RID: 11516 RVA: 0x000CA373 File Offset: 0x000C9373
		public TemplateBuilder()
		{
			this._allowMultipleInstances = true;
		}

		// Token: 0x06002CFD RID: 11517 RVA: 0x000CA382 File Offset: 0x000C9382
		public override void Init(TemplateParser parser, ControlBuilder parentBuilder, Type type, string tagName, string ID, IDictionary attribs)
		{
			base.Init(parser, parentBuilder, type, tagName, ID, attribs);
			if (base.InPageTheme && base.ParentBuilder != null && base.ParentBuilder.IsControlSkin)
			{
				((PageThemeParser)base.Parser).CurrentSkinBuilder = parentBuilder;
			}
		}

		// Token: 0x06002CFE RID: 11518 RVA: 0x000CA3C1 File Offset: 0x000C93C1
		public override void CloseControl()
		{
			base.CloseControl();
			if (base.InPageTheme && base.ParentBuilder != null && base.ParentBuilder.IsControlSkin)
			{
				((PageThemeParser)base.Parser).CurrentSkinBuilder = null;
			}
		}

		// Token: 0x170009CF RID: 2511
		// (get) Token: 0x06002CFF RID: 11519 RVA: 0x000CA3F7 File Offset: 0x000C93F7
		// (set) Token: 0x06002D00 RID: 11520 RVA: 0x000CA3FF File Offset: 0x000C93FF
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

		// Token: 0x06002D01 RID: 11521 RVA: 0x000CA408 File Offset: 0x000C9408
		public override object BuildObject()
		{
			return this;
		}

		// Token: 0x06002D02 RID: 11522 RVA: 0x000CA40B File Offset: 0x000C940B
		public override bool NeedsTagInnerText()
		{
			return base.InDesigner;
		}

		// Token: 0x06002D03 RID: 11523 RVA: 0x000CA413 File Offset: 0x000C9413
		internal void SetDesignerHost(IDesignerHost designerHost)
		{
			this._designerHost = designerHost;
		}

		// Token: 0x06002D04 RID: 11524 RVA: 0x000CA41C File Offset: 0x000C941C
		public override void SetTagInnerText(string text)
		{
			this._tagInnerText = text;
		}

		// Token: 0x06002D05 RID: 11525 RVA: 0x000CA428 File Offset: 0x000C9428
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

		// Token: 0x170009D0 RID: 2512
		// (get) Token: 0x06002D06 RID: 11526 RVA: 0x000CA504 File Offset: 0x000C9504
		// (set) Token: 0x06002D07 RID: 11527 RVA: 0x000CA50C File Offset: 0x000C950C
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

		// Token: 0x040020D2 RID: 8402
		internal string _tagInnerText;

		// Token: 0x040020D3 RID: 8403
		private bool _allowMultipleInstances;

		// Token: 0x040020D4 RID: 8404
		private IDesignerHost _designerHost;
	}
}
