using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Web.WebPages.Html;
using System.Web.WebPages.Scope;
using Microsoft.CSharp.RuntimeBinder;

namespace System.Web.WebPages
{
	// Token: 0x02000099 RID: 153
	public abstract class WebPage : WebPageBase
	{
		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000528 RID: 1320 RVA: 0x0000F765 File Offset: 0x0000D965
		// (set) Token: 0x06000529 RID: 1321 RVA: 0x0000F76D File Offset: 0x0000D96D
		internal bool TopLevelPage { get; set; }

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x0600052A RID: 1322 RVA: 0x0000F776 File Offset: 0x0000D976
		// (set) Token: 0x0600052B RID: 1323 RVA: 0x0000F792 File Offset: 0x0000D992
		public override HttpContextBase Context
		{
			get
			{
				if (this._context == null)
				{
					return base.PageContext.HttpContext;
				}
				return this._context;
			}
			set
			{
				this._context = value;
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x0600052C RID: 1324 RVA: 0x0000F79B File Offset: 0x0000D99B
		// (set) Token: 0x0600052D RID: 1325 RVA: 0x0000F7A3 File Offset: 0x0000D9A3
		public HtmlHelper Html { get; private set; }

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x0600052E RID: 1326 RVA: 0x0000F7AC File Offset: 0x0000D9AC
		public ValidationHelper Validation
		{
			get
			{
				return base.PageContext.Validation;
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x0600052F RID: 1327 RVA: 0x0000F7BC File Offset: 0x0000D9BC
		[Dynamic]
		public dynamic Model
		{
			[return: Dynamic]
			get
			{
				if (WebPage.<get_Model>o__SiteContainer0.<>p__Site1 == null)
				{
					WebPage.<get_Model>o__SiteContainer0.<>p__Site1 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(WebPage), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
					}));
				}
				Func<CallSite, object, bool> target = WebPage.<get_Model>o__SiteContainer0.<>p__Site1.Target;
				CallSite <>p__Site = WebPage.<get_Model>o__SiteContainer0.<>p__Site1;
				if (WebPage.<get_Model>o__SiteContainer0.<>p__Site2 == null)
				{
					WebPage.<get_Model>o__SiteContainer0.<>p__Site2 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Equal, typeof(WebPage), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, null)
					}));
				}
				if (target(<>p__Site, WebPage.<get_Model>o__SiteContainer0.<>p__Site2.Target(WebPage.<get_Model>o__SiteContainer0.<>p__Site2, this._model, null)))
				{
					this._model = ReflectionDynamicObject.WrapObjectIfInternal(base.PageContext.Model);
				}
				return this._model;
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000530 RID: 1328 RVA: 0x0000F88A File Offset: 0x0000DA8A
		public ModelStateDictionary ModelState
		{
			get
			{
				return base.PageContext.ModelState;
			}
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x0000F897 File Offset: 0x0000DA97
		public static void RegisterPageExecutor(IWebPageRequestExecutor executor)
		{
			WebPage._executors.Add(executor);
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x0000F8A4 File Offset: 0x0000DAA4
		public override void ExecutePageHierarchy()
		{
			using (ScopeStorage.CreateTransientScope(new ScopeStorageDictionary(ScopeStorage.CurrentScope, this.PageData)))
			{
				this.ExecutePageHierarchy(WebPage._executors);
			}
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x0000F8F9 File Offset: 0x0000DAF9
		internal void ExecutePageHierarchy(IEnumerable<IWebPageRequestExecutor> executors)
		{
			if (!this.TopLevelPage || !executors.Any((IWebPageRequestExecutor executor) => executor.Execute(this)))
			{
				base.ExecutePageHierarchy();
			}
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x0000F91D File Offset: 0x0000DB1D
		public override HelperResult RenderPage(string path, params object[] data)
		{
			return base.RenderPage(path, data);
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x0000F927 File Offset: 0x0000DB27
		protected override void InitializePage()
		{
			base.InitializePage();
			this.Html = new HtmlHelper(this.ModelState, this.Validation);
		}

		// Token: 0x04000156 RID: 342
		private static readonly List<IWebPageRequestExecutor> _executors = new List<IWebPageRequestExecutor>();

		// Token: 0x04000157 RID: 343
		private HttpContextBase _context;

		// Token: 0x04000158 RID: 344
		[Dynamic]
		private dynamic _model;

		// Token: 0x020000BA RID: 186
		[CompilerGenerated]
		private static class <get_Model>o__SiteContainer0
		{
			// Token: 0x04000195 RID: 405
			public static CallSite<Func<CallSite, object, bool>> <>p__Site1;

			// Token: 0x04000196 RID: 406
			public static CallSite<Func<CallSite, object, object, object>> <>p__Site2;
		}
	}
}
