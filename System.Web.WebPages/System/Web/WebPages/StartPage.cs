using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using Microsoft.Internal.Web.Utils;

namespace System.Web.WebPages
{
	// Token: 0x0200006B RID: 107
	public abstract class StartPage : WebPageRenderingBase
	{
		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x0000A2E2 File Offset: 0x000084E2
		// (set) Token: 0x060002B4 RID: 692 RVA: 0x0000A2EA File Offset: 0x000084EA
		public WebPageRenderingBase ChildPage { get; set; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x0000A2F3 File Offset: 0x000084F3
		// (set) Token: 0x060002B6 RID: 694 RVA: 0x0000A300 File Offset: 0x00008500
		public override HttpContextBase Context
		{
			get
			{
				return this.ChildPage.Context;
			}
			set
			{
				this.ChildPage.Context = value;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x0000A30E File Offset: 0x0000850E
		// (set) Token: 0x060002B8 RID: 696 RVA: 0x0000A31B File Offset: 0x0000851B
		public override string Layout
		{
			get
			{
				return this.ChildPage.Layout;
			}
			set
			{
				if (value == null)
				{
					this.ChildPage.Layout = null;
					return;
				}
				this.ChildPage.Layout = this.NormalizeLayoutPagePath(value);
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x0000A33F File Offset: 0x0000853F
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
				return this.ChildPage.PageData;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060002BA RID: 698 RVA: 0x0000A34C File Offset: 0x0000854C
		[Dynamic]
		public override dynamic Page
		{
			[return: Dynamic]
			get
			{
				return this.ChildPage.Page;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060002BB RID: 699 RVA: 0x0000A359 File Offset: 0x00008559
		// (set) Token: 0x060002BC RID: 700 RVA: 0x0000A361 File Offset: 0x00008561
		internal bool RunPageCalled { get; set; }

		// Token: 0x060002BD RID: 701 RVA: 0x0000A36C File Offset: 0x0000856C
		public override void ExecutePageHierarchy()
		{
			TemplateStack.Push(this.Context, this);
			try
			{
				this.Execute();
				if (!this.RunPageCalled)
				{
					this.RunPage();
				}
			}
			finally
			{
				TemplateStack.Pop(this.Context);
			}
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000A3B8 File Offset: 0x000085B8
		public static WebPageRenderingBase GetStartPage(WebPageRenderingBase page, string fileName, IEnumerable<string> supportedExtensions)
		{
			if (page == null)
			{
				throw new ArgumentNullException("page");
			}
			if (string.IsNullOrEmpty(fileName))
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, CommonResources.Argument_Cannot_Be_Null_Or_Empty, new object[]
				{
					"fileName"
				}), "fileName");
			}
			if (supportedExtensions == null)
			{
				throw new ArgumentNullException("supportedExtensions");
			}
			return StartPage.GetStartPage(page, page.VirtualPathFactory ?? VirtualPathFactoryManager.Instance, HttpRuntime.AppDomainAppVirtualPath, fileName, supportedExtensions);
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0000A430 File Offset: 0x00008630
		internal static WebPageRenderingBase GetStartPage(WebPageRenderingBase page, IVirtualPathFactory virtualPathFactory, string appDomainAppVirtualPath, string fileName, IEnumerable<string> supportedExtensions)
		{
			WebPageRenderingBase webPageRenderingBase = page;
			string directory = VirtualPathUtility.GetDirectory(page.VirtualPath);
			while (!string.IsNullOrEmpty(directory) && directory != "/" && PathUtil.IsWithinAppRoot(appDomainAppVirtualPath, directory))
			{
				foreach (string str in supportedExtensions)
				{
					string virtualPath = VirtualPathUtility.Combine(directory, fileName + "." + str);
					if (virtualPathFactory.Exists(virtualPath))
					{
						StartPage startPage = virtualPathFactory.CreateInstance(virtualPath);
						startPage.VirtualPath = virtualPath;
						startPage.ChildPage = webPageRenderingBase;
						startPage.VirtualPathFactory = virtualPathFactory;
						webPageRenderingBase = startPage;
						break;
					}
				}
				directory = webPageRenderingBase.GetDirectory(directory);
			}
			return webPageRenderingBase;
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000A4F4 File Offset: 0x000086F4
		public override HelperResult RenderPage(string path, params object[] data)
		{
			return this.ChildPage.RenderPage(this.NormalizePath(path), data);
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0000A509 File Offset: 0x00008709
		public void RunPage()
		{
			this.RunPageCalled = true;
			this.ChildPage.ExecutePageHierarchy();
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000A51D File Offset: 0x0000871D
		public override void Write(HelperResult result)
		{
			this.ChildPage.Write(result);
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0000A52B File Offset: 0x0000872B
		public override void WriteLiteral(object value)
		{
			this.ChildPage.WriteLiteral(value);
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000A539 File Offset: 0x00008739
		public override void Write(object value)
		{
			this.ChildPage.Write(value);
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0000A547 File Offset: 0x00008747
		protected internal override TextWriter GetOutputWriter()
		{
			return this.ChildPage.GetOutputWriter();
		}
	}
}
