using System;
using System.Web.UI;
using System.Web.Util;

namespace System.Web.Compilation
{
	// Token: 0x0200081B RID: 2075
	internal abstract class BuildResultNoCompileTemplateControl : BuildResult, ITypedWebObjectFactory, IWebObjectFactory
	{
		// Token: 0x0600635C RID: 25436 RVA: 0x0015C104 File Offset: 0x0015A304
		internal BuildResultNoCompileTemplateControl(Type baseType, TemplateParser parser)
		{
			this._baseType = baseType;
			this._rootBuilder = parser.RootBuilder;
			this._rootBuilder.PrepareNoCompilePageSupport();
		}

		// Token: 0x0600635D RID: 25437 RVA: 0x0015B65E File Offset: 0x0015985E
		internal override BuildResultTypeCode GetCode()
		{
			return BuildResultTypeCode.Invalid;
		}

		// Token: 0x17001C23 RID: 7203
		// (get) Token: 0x0600635E RID: 25438 RVA: 0x00007722 File Offset: 0x00005922
		internal override bool CacheToDisk
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001C24 RID: 7204
		// (get) Token: 0x0600635F RID: 25439 RVA: 0x0015C12A File Offset: 0x0015A32A
		internal override TimeSpan MemoryCacheSlidingExpiration
		{
			get
			{
				return TimeSpan.FromMinutes(5.0);
			}
		}

		// Token: 0x17001C25 RID: 7205
		// (get) Token: 0x06006360 RID: 25440 RVA: 0x0015C13A File Offset: 0x0015A33A
		internal Type BaseType
		{
			get
			{
				return this._baseType;
			}
		}

		// Token: 0x06006361 RID: 25441 RVA: 0x0015C144 File Offset: 0x0015A344
		public virtual object CreateInstance()
		{
			TemplateControl templateControl = (TemplateControl)HttpRuntime.FastCreatePublicInstance(this._baseType);
			templateControl.TemplateControlVirtualPath = base.VirtualPath;
			templateControl.TemplateControlVirtualDirectory = base.VirtualPath.Parent;
			templateControl.SetNoCompileBuildResult(this);
			return templateControl;
		}

		// Token: 0x17001C26 RID: 7206
		// (get) Token: 0x06006362 RID: 25442 RVA: 0x0015C13A File Offset: 0x0015A33A
		public virtual Type InstantiatedType
		{
			get
			{
				return this._baseType;
			}
		}

		// Token: 0x06006363 RID: 25443 RVA: 0x0015C188 File Offset: 0x0015A388
		internal virtual void FrameworkInitialize(TemplateControl templateControl)
		{
			HttpContext httpContext = HttpContext.Current;
			TemplateControl templateControl2 = httpContext.TemplateControl;
			httpContext.TemplateControl = templateControl;
			try
			{
				if (!this._initialized)
				{
					lock (this)
					{
						this._rootBuilder.InitObject(templateControl);
					}
					this._initialized = true;
				}
				else
				{
					this._rootBuilder.InitObject(templateControl);
				}
			}
			finally
			{
				if (templateControl2 != null)
				{
					httpContext.TemplateControl = templateControl2;
				}
			}
		}

		// Token: 0x0400337A RID: 13178
		protected Type _baseType;

		// Token: 0x0400337B RID: 13179
		protected RootBuilder _rootBuilder;

		// Token: 0x0400337C RID: 13180
		protected bool _initialized;
	}
}
