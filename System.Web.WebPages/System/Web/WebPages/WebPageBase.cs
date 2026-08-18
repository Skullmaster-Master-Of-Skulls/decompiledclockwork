using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Web.WebPages.Resources;
using Microsoft.Internal.Web.Utils;

namespace System.Web.WebPages
{
	// Token: 0x02000095 RID: 149
	public abstract class WebPageBase : WebPageRenderingBase
	{
		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060004EB RID: 1259 RVA: 0x0000EA10 File Offset: 0x0000CC10
		// (set) Token: 0x060004EC RID: 1260 RVA: 0x0000EA18 File Offset: 0x0000CC18
		public override string Layout { get; set; }

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060004ED RID: 1261 RVA: 0x0000EA21 File Offset: 0x0000CC21
		public TextWriter Output
		{
			get
			{
				return this.OutputStack.Peek();
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060004EE RID: 1262 RVA: 0x0000EA2E File Offset: 0x0000CC2E
		public Stack<TextWriter> OutputStack
		{
			get
			{
				return base.PageContext.OutputStack;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060004EF RID: 1263 RVA: 0x0000EA3B File Offset: 0x0000CC3B
		[Dynamic(new bool[]
		{
			false,
			false,
			true
		})]
		public override IDictionary<object, dynamic> PageData
		{
			[return: Dynamic(new bool[]
			{
				false,
				false,
				true
			})]
			get
			{
				return base.PageContext.PageData;
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060004F0 RID: 1264 RVA: 0x0000EA48 File Offset: 0x0000CC48
		[Dynamic]
		public override dynamic Page
		{
			[return: Dynamic]
			get
			{
				if (this._dynamicPageData == null)
				{
					this._dynamicPageData = new DynamicPageDataDictionary<object>((PageDataDictionary<object>)this.PageData);
				}
				return this._dynamicPageData;
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060004F1 RID: 1265 RVA: 0x0000EA70 File Offset: 0x0000CC70
		private Dictionary<string, SectionWriter> PreviousSectionWriters
		{
			get
			{
				Dictionary<string, SectionWriter> item = this.SectionWritersStack.Pop();
				Dictionary<string, SectionWriter> result = (this.SectionWritersStack.Count > 0) ? this.SectionWritersStack.Peek() : null;
				this.SectionWritersStack.Push(item);
				return result;
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060004F2 RID: 1266 RVA: 0x0000EAB3 File Offset: 0x0000CCB3
		private Dictionary<string, SectionWriter> SectionWriters
		{
			get
			{
				return this.SectionWritersStack.Peek();
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060004F3 RID: 1267 RVA: 0x0000EAC0 File Offset: 0x0000CCC0
		private Stack<Dictionary<string, SectionWriter>> SectionWritersStack
		{
			get
			{
				return base.PageContext.SectionWritersStack;
			}
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x0000EACD File Offset: 0x0000CCCD
		protected virtual void ConfigurePage(WebPageBase parentPage)
		{
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x0000EACF File Offset: 0x0000CCCF
		public static WebPageBase CreateInstanceFromVirtualPath(string virtualPath)
		{
			return WebPageBase.CreateInstanceFromVirtualPath(virtualPath, VirtualPathFactoryManager.Instance);
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x0000EADC File Offset: 0x0000CCDC
		internal static WebPageBase CreateInstanceFromVirtualPath(string virtualPath, IVirtualPathFactory virtualPathFactory)
		{
			WebPageBase result;
			try
			{
				WebPageBase webPageBase = virtualPathFactory.CreateInstance(virtualPath);
				webPageBase.VirtualPath = virtualPath;
				webPageBase.VirtualPathFactory = virtualPathFactory;
				result = webPageBase;
			}
			catch (HttpException e)
			{
				BuildManagerExceptionUtil.ThrowIfUnsupportedExtension(virtualPath, e);
				throw;
			}
			return result;
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x0000EB20 File Offset: 0x0000CD20
		protected virtual WebPageBase CreatePageFromVirtualPath(string virtualPath, HttpContextBase httpContext, Func<string, bool> virtualPathExists, DisplayModeProvider displayModeProvider, IDisplayMode displayMode)
		{
			try
			{
				DisplayInfo displayInfoForVirtualPath = displayModeProvider.GetDisplayInfoForVirtualPath(virtualPath, httpContext, virtualPathExists, displayMode);
				if (displayInfoForVirtualPath != null)
				{
					WebPageBase webPageBase = this.VirtualPathFactory.CreateInstance(displayInfoForVirtualPath.FilePath);
					if (webPageBase != null)
					{
						webPageBase.VirtualPath = virtualPath;
						webPageBase.VirtualPathFactory = this.VirtualPathFactory;
						webPageBase.DisplayModeProvider = base.DisplayModeProvider;
						return webPageBase;
					}
				}
			}
			catch (HttpException e)
			{
				BuildManagerExceptionUtil.ThrowIfUnsupportedExtension(virtualPath, e);
				BuildManagerExceptionUtil.ThrowIfCodeDomDefinedExtension(virtualPath, e);
				throw;
			}
			throw new HttpException(string.Format(CultureInfo.CurrentCulture, WebPageResources.WebPage_InvalidPageType, new object[]
			{
				virtualPath
			}));
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x0000EBC0 File Offset: 0x0000CDC0
		private WebPageContext CreatePageContextFromParameters(bool isLayoutPage, params object[] data)
		{
			object model = null;
			if (data != null && data.Length > 0)
			{
				model = data[0];
			}
			IDictionary<object, object> pageData = PageDataDictionary<object>.CreatePageDataFromParameters(this.PageData, data);
			return WebPageContext.CreateNestedPageContext<object>(base.PageContext, pageData, model, isLayoutPage);
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x0000EBF8 File Offset: 0x0000CDF8
		public void DefineSection(string name, SectionWriter action)
		{
			if (this.SectionWriters.ContainsKey(name))
			{
				throw new HttpException(string.Format(CultureInfo.InvariantCulture, WebPageResources.WebPage_SectionAleadyDefined, new object[]
				{
					name
				}));
			}
			this.SectionWriters[name] = action;
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x0000EC44 File Offset: 0x0000CE44
		internal void EnsurePageCanBeRequestedDirectly(string methodName)
		{
			if (this.PreviousSectionWriters == null)
			{
				throw new HttpException(string.Format(CultureInfo.CurrentCulture, WebPageResources.WebPage_CannotRequestDirectly, new object[]
				{
					this.VirtualPath,
					methodName
				}));
			}
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x0000EC83 File Offset: 0x0000CE83
		public void ExecutePageHierarchy(WebPageContext pageContext, TextWriter writer)
		{
			this.ExecutePageHierarchy(pageContext, writer, null);
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x0000EC90 File Offset: 0x0000CE90
		public void ExecutePageHierarchy(WebPageContext pageContext, TextWriter writer, WebPageRenderingBase startPage)
		{
			this.PushContext(pageContext, writer);
			if (startPage != null)
			{
				if (startPage != this)
				{
					WebPageContext webPageContext = WebPageContext.CreateNestedPageContext<object>(pageContext, null, null, false);
					webPageContext.Page = startPage;
					startPage.PageContext = webPageContext;
				}
				startPage.ExecutePageHierarchy();
			}
			else
			{
				this.ExecutePageHierarchy();
			}
			this.PopContext();
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x0000ECD8 File Offset: 0x0000CED8
		public override void ExecutePageHierarchy()
		{
			if (WebPageHttpHandler.ShouldGenerateSourceHeader(this.Context))
			{
				try
				{
					string virtualPath = this.VirtualPath;
					if (virtualPath != null)
					{
						string text = this.Context.Request.MapPath(virtualPath);
						if (!text.IsEmpty())
						{
							base.PageContext.SourceFiles.Add(text);
						}
					}
				}
				catch
				{
				}
			}
			TemplateStack.Push(this.Context, this);
			try
			{
				this.Execute();
			}
			finally
			{
				TemplateStack.Pop(this.Context);
			}
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x0000ED6C File Offset: 0x0000CF6C
		protected virtual void InitializePage()
		{
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x0000ED6E File Offset: 0x0000CF6E
		public bool IsSectionDefined(string name)
		{
			this.EnsurePageCanBeRequestedDirectly("IsSectionDefined");
			return this.PreviousSectionWriters.ContainsKey(name);
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x0000ED88 File Offset: 0x0000CF88
		public void PopContext()
		{
			this.OutputStack.Pop();
			if (!string.IsNullOrEmpty(this.Layout))
			{
				string partialViewName = this.NormalizeLayoutPagePath(this.Layout);
				this.OutputStack.Push(this._currentWriter);
				this.RenderSurrounding(partialViewName, new Action<TextWriter>(this._tempWriter.CopyTo));
				this.OutputStack.Pop();
			}
			else
			{
				this._tempWriter.CopyTo(this._currentWriter);
			}
			this.VerifyRenderedBodyOrSections();
			this.SectionWritersStack.Pop();
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x0000EE18 File Offset: 0x0000D018
		public void PushContext(WebPageContext pageContext, TextWriter writer)
		{
			this._currentWriter = writer;
			base.PageContext = pageContext;
			pageContext.Page = this;
			this.InitializePage();
			this._tempWriter = new StringWriter(CultureInfo.InvariantCulture);
			this.OutputStack.Push(this._tempWriter);
			this.SectionWritersStack.Push(new Dictionary<string, SectionWriter>(StringComparer.OrdinalIgnoreCase));
			if (base.PageContext.BodyAction != null)
			{
				this._body = base.PageContext.BodyAction;
				base.PageContext.BodyAction = null;
			}
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x0000EEB0 File Offset: 0x0000D0B0
		public HelperResult RenderBody()
		{
			this.EnsurePageCanBeRequestedDirectly("RenderBody");
			if (this._renderedBody)
			{
				throw new HttpException(WebPageResources.WebPage_RenderBodyAlreadyCalled);
			}
			this._renderedBody = true;
			if (this._body != null)
			{
				return new HelperResult(delegate(TextWriter tw)
				{
					this._body(tw);
				});
			}
			throw new HttpException(string.Format(CultureInfo.CurrentCulture, WebPageResources.WebPage_CannotRequestDirectly, new object[]
			{
				this.VirtualPath,
				"RenderBody"
			}));
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x0000EF30 File Offset: 0x0000D130
		public override HelperResult RenderPage(string path, params object[] data)
		{
			return this.RenderPageCore(path, false, data);
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x0000EFE0 File Offset: 0x0000D1E0
		private HelperResult RenderPageCore(string path, bool isLayoutPage, object[] data)
		{
			if (string.IsNullOrEmpty(path))
			{
				throw new ArgumentException(CommonResources.Argument_Cannot_Be_Null_Or_Empty, "path");
			}
			return new HelperResult(delegate(TextWriter writer)
			{
				path = this.NormalizePath(path);
				WebPageBase webPageBase = this.CreatePageFromVirtualPath(path, this.Context, new Func<string, bool>(this.VirtualPathFactory.Exists), this.DisplayModeProvider, this.DisplayMode);
				WebPageContext pageContext = this.CreatePageContextFromParameters(isLayoutPage, data);
				webPageBase.ConfigurePage(this);
				webPageBase.ExecutePageHierarchy(pageContext, writer);
			});
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x0000F03D File Offset: 0x0000D23D
		public HelperResult RenderSection(string name)
		{
			return this.RenderSection(name, true);
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x0000F13C File Offset: 0x0000D33C
		public HelperResult RenderSection(string name, bool required)
		{
			this.EnsurePageCanBeRequestedDirectly("RenderSection");
			if (this.PreviousSectionWriters.ContainsKey(name))
			{
				return new HelperResult(delegate(TextWriter tw)
				{
					if (this._renderedSections.Contains(name))
					{
						throw new HttpException(string.Format(CultureInfo.InvariantCulture, WebPageResources.WebPage_SectionAleadyRendered, new object[]
						{
							name
						}));
					}
					SectionWriter sectionWriter = this.PreviousSectionWriters[name];
					Dictionary<string, SectionWriter> item = this.SectionWritersStack.Pop();
					bool flag = false;
					try
					{
						if (this.Output != tw)
						{
							this.OutputStack.Push(tw);
							flag = true;
						}
						sectionWriter();
					}
					finally
					{
						if (flag)
						{
							this.OutputStack.Pop();
						}
					}
					this.SectionWritersStack.Push(item);
					this._renderedSections.Add(name);
				});
			}
			if (required)
			{
				throw new HttpException(string.Format(CultureInfo.InvariantCulture, WebPageResources.WebPage_SectionNotDefined, new object[]
				{
					name
				}));
			}
			return null;
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x0000F1C0 File Offset: 0x0000D3C0
		private void RenderSurrounding(string partialViewName, Action<TextWriter> body)
		{
			Action<TextWriter> bodyAction = base.PageContext.BodyAction;
			base.PageContext.BodyAction = body;
			this.Write(this.RenderPageCore(partialViewName, true, new object[0]));
			base.PageContext.BodyAction = bodyAction;
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x0000F208 File Offset: 0x0000D408
		private void VerifyRenderedBodyOrSections()
		{
			if (this._body != null)
			{
				if (this.SectionWritersStack.Count > 1 && this.PreviousSectionWriters != null && this.PreviousSectionWriters.Count > 0)
				{
					StringBuilder stringBuilder = new StringBuilder();
					foreach (string text in this.PreviousSectionWriters.Keys)
					{
						if (!this._renderedSections.Contains(text))
						{
							if (stringBuilder.Length > 0)
							{
								stringBuilder.Append("; ");
							}
							stringBuilder.Append(text);
						}
					}
					if (stringBuilder.Length > 0)
					{
						throw new HttpException(string.Format(CultureInfo.CurrentCulture, WebPageResources.WebPage_SectionsNotRendered, new object[]
						{
							this.VirtualPath,
							stringBuilder.ToString()
						}));
					}
				}
				else if (!this._renderedBody)
				{
					throw new HttpException(string.Format(CultureInfo.CurrentCulture, WebPageResources.WebPage_RenderBodyNotCalled, new object[]
					{
						this.VirtualPath
					}));
				}
			}
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x0000F32C File Offset: 0x0000D52C
		public override void Write(HelperResult result)
		{
			WebPageExecutingBase.WriteTo(this.Output, result);
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x0000F33A File Offset: 0x0000D53A
		public override void Write(object value)
		{
			WebPageExecutingBase.WriteTo(this.Output, value);
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x0000F348 File Offset: 0x0000D548
		public override void WriteLiteral(object value)
		{
			this.Output.Write(value);
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x0000F356 File Offset: 0x0000D556
		protected internal override TextWriter GetOutputWriter()
		{
			return this.Output;
		}

		// Token: 0x04000147 RID: 327
		private readonly HashSet<string> _renderedSections = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x04000148 RID: 328
		private bool _renderedBody;

		// Token: 0x04000149 RID: 329
		private Action<TextWriter> _body;

		// Token: 0x0400014A RID: 330
		private StringWriter _tempWriter;

		// Token: 0x0400014B RID: 331
		private TextWriter _currentWriter;

		// Token: 0x0400014C RID: 332
		[Dynamic(new bool[]
		{
			false,
			true
		})]
		private DynamicPageDataDictionary<dynamic> _dynamicPageData;
	}
}
