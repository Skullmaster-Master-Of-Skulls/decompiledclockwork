using System;
using System.Globalization;
using System.IO;
using System.Web.Mvc.Properties;

namespace System.Web.Mvc
{
	// Token: 0x02000063 RID: 99
	public abstract class BuildManagerCompiledView : IView
	{
		// Token: 0x06000299 RID: 665 RVA: 0x00008ACF File Offset: 0x00006CCF
		protected BuildManagerCompiledView(ControllerContext controllerContext, string viewPath) : this(controllerContext, viewPath, null)
		{
		}

		// Token: 0x0600029A RID: 666 RVA: 0x00008ADA File Offset: 0x00006CDA
		protected BuildManagerCompiledView(ControllerContext controllerContext, string viewPath, IViewPageActivator viewPageActivator) : this(controllerContext, viewPath, viewPageActivator, null)
		{
		}

		// Token: 0x0600029B RID: 667 RVA: 0x00008AE8 File Offset: 0x00006CE8
		internal BuildManagerCompiledView(ControllerContext controllerContext, string viewPath, IViewPageActivator viewPageActivator, IDependencyResolver dependencyResolver)
		{
			if (controllerContext == null)
			{
				throw new ArgumentNullException("controllerContext");
			}
			if (string.IsNullOrEmpty(viewPath))
			{
				throw new ArgumentException(MvcResources.Common_NullOrEmpty, "viewPath");
			}
			this._controllerContext = controllerContext;
			this.ViewPath = viewPath;
			this.ViewPageActivator = (viewPageActivator ?? new BuildManagerViewEngine.DefaultViewPageActivator(dependencyResolver));
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x0600029C RID: 668 RVA: 0x00008B41 File Offset: 0x00006D41
		// (set) Token: 0x0600029D RID: 669 RVA: 0x00008B5C File Offset: 0x00006D5C
		internal IBuildManager BuildManager
		{
			get
			{
				if (this._buildManager == null)
				{
					this._buildManager = new BuildManagerWrapper();
				}
				return this._buildManager;
			}
			set
			{
				this._buildManager = value;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x0600029E RID: 670 RVA: 0x00008B65 File Offset: 0x00006D65
		// (set) Token: 0x0600029F RID: 671 RVA: 0x00008B6D File Offset: 0x00006D6D
		public string ViewPath { get; protected set; }

		// Token: 0x060002A0 RID: 672 RVA: 0x00008B78 File Offset: 0x00006D78
		public virtual void Render(ViewContext viewContext, TextWriter writer)
		{
			if (viewContext == null)
			{
				throw new ArgumentNullException("viewContext");
			}
			object obj = null;
			Type compiledType = this.BuildManager.GetCompiledType(this.ViewPath);
			if (compiledType != null)
			{
				obj = this.ViewPageActivator.Create(this._controllerContext, compiledType);
			}
			if (obj == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, MvcResources.CshtmlView_ViewCouldNotBeCreated, new object[]
				{
					this.ViewPath
				}));
			}
			this.RenderView(viewContext, writer, obj);
		}

		// Token: 0x060002A1 RID: 673
		protected abstract void RenderView(ViewContext viewContext, TextWriter writer, object instance);

		// Token: 0x04000088 RID: 136
		internal IViewPageActivator ViewPageActivator;

		// Token: 0x04000089 RID: 137
		private IBuildManager _buildManager;

		// Token: 0x0400008A RID: 138
		private ControllerContext _controllerContext;
	}
}
