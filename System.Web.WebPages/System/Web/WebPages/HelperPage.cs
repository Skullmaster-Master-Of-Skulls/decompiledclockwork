using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Web.Caching;
using System.Web.WebPages.Html;
using System.Web.WebPages.Instrumentation;

namespace System.Web.WebPages
{
	// Token: 0x02000083 RID: 131
	public class HelperPage
	{
		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060003E2 RID: 994 RVA: 0x0000CC1B File Offset: 0x0000AE1B
		private static InstrumentationService InstrumentationService
		{
			get
			{
				if (HelperPage._instrumentationService == null)
				{
					HelperPage._instrumentationService = new InstrumentationService();
				}
				return HelperPage._instrumentationService;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060003E3 RID: 995 RVA: 0x0000CC33 File Offset: 0x0000AE33
		public static HttpContextBase Context
		{
			get
			{
				return new HttpContextWrapper(HttpContext.Current);
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060003E4 RID: 996 RVA: 0x0000CC3F File Offset: 0x0000AE3F
		public static WebPageRenderingBase CurrentPage
		{
			get
			{
				return HelperPage.PageContext.Page;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060003E5 RID: 997 RVA: 0x0000CC4B File Offset: 0x0000AE4B
		[Dynamic]
		public static dynamic Page
		{
			[return: Dynamic]
			get
			{
				return HelperPage.CurrentPage.Page;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060003E6 RID: 998 RVA: 0x0000CC58 File Offset: 0x0000AE58
		[Dynamic]
		public static dynamic Model
		{
			[return: Dynamic]
			get
			{
				WebPage webPage = HelperPage.CurrentPage as WebPage;
				if (webPage == null)
				{
					return null;
				}
				return webPage.Model;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060003E7 RID: 999 RVA: 0x0000CC7C File Offset: 0x0000AE7C
		public static ModelStateDictionary ModelState
		{
			get
			{
				WebPage webPage = HelperPage.CurrentPage as WebPage;
				if (webPage == null)
				{
					return null;
				}
				return webPage.ModelState;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060003E8 RID: 1000 RVA: 0x0000CCA0 File Offset: 0x0000AEA0
		public static HtmlHelper Html
		{
			get
			{
				WebPage webPage = HelperPage.CurrentPage as WebPage;
				if (webPage == null)
				{
					return null;
				}
				return webPage.Html;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x0000CCC3 File Offset: 0x0000AEC3
		// (set) Token: 0x060003EA RID: 1002 RVA: 0x0000CCD3 File Offset: 0x0000AED3
		public static WebPageContext PageContext
		{
			get
			{
				return HelperPage._pageContext ?? WebPageContext.Current;
			}
			set
			{
				HelperPage._pageContext = value;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060003EB RID: 1003 RVA: 0x0000CCDB File Offset: 0x0000AEDB
		public static HttpApplicationStateBase AppState
		{
			get
			{
				if (HelperPage.Context != null)
				{
					return HelperPage.Context.Application;
				}
				return null;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060003EC RID: 1004 RVA: 0x0000CCF0 File Offset: 0x0000AEF0
		[Dynamic]
		public static dynamic App
		{
			[return: Dynamic]
			get
			{
				return HelperPage.CurrentPage.App;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060003ED RID: 1005 RVA: 0x0000CCFC File Offset: 0x0000AEFC
		public static string VirtualPath
		{
			get
			{
				return HelperPage.PageContext.Page.VirtualPath;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060003EE RID: 1006 RVA: 0x0000CD0D File Offset: 0x0000AF0D
		public static Cache Cache
		{
			get
			{
				if (HelperPage.Context != null)
				{
					return HelperPage.Context.Cache;
				}
				return null;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x0000CD22 File Offset: 0x0000AF22
		public static HttpRequestBase Request
		{
			get
			{
				if (HelperPage.Context != null)
				{
					return HelperPage.Context.Request;
				}
				return null;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060003F0 RID: 1008 RVA: 0x0000CD37 File Offset: 0x0000AF37
		public static HttpResponseBase Response
		{
			get
			{
				if (HelperPage.Context != null)
				{
					return HelperPage.Context.Response;
				}
				return null;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060003F1 RID: 1009 RVA: 0x0000CD4C File Offset: 0x0000AF4C
		public static HttpServerUtilityBase Server
		{
			get
			{
				if (HelperPage.Context != null)
				{
					return HelperPage.Context.Server;
				}
				return null;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060003F2 RID: 1010 RVA: 0x0000CD61 File Offset: 0x0000AF61
		public static HttpSessionStateBase Session
		{
			get
			{
				if (HelperPage.Context != null)
				{
					return HelperPage.Context.Session;
				}
				return null;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060003F3 RID: 1011 RVA: 0x0000CD76 File Offset: 0x0000AF76
		public static IList<string> UrlData
		{
			get
			{
				return HelperPage.CurrentPage.UrlData;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060003F4 RID: 1012 RVA: 0x0000CD82 File Offset: 0x0000AF82
		public static IPrincipal User
		{
			get
			{
				return HelperPage.CurrentPage.User;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060003F5 RID: 1013 RVA: 0x0000CD8E File Offset: 0x0000AF8E
		public static bool IsPost
		{
			get
			{
				return HelperPage.CurrentPage.IsPost;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060003F6 RID: 1014 RVA: 0x0000CD9A File Offset: 0x0000AF9A
		public static bool IsAjax
		{
			get
			{
				return HelperPage.CurrentPage.IsAjax;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060003F7 RID: 1015 RVA: 0x0000CDA6 File Offset: 0x0000AFA6
		[Dynamic(new bool[]
		{
			false,
			false,
			true
		})]
		public static IDictionary<object, dynamic> PageData
		{
			[return: Dynamic(new bool[]
			{
				false,
				false,
				true
			})]
			get
			{
				return HelperPage.PageContext.PageData;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060003F8 RID: 1016 RVA: 0x0000CDB2 File Offset: 0x0000AFB2
		// (set) Token: 0x060003F9 RID: 1017 RVA: 0x0000CDB9 File Offset: 0x0000AFB9
		protected static string HelperVirtualPath { get; set; }

		// Token: 0x060003FA RID: 1018 RVA: 0x0000CDC1 File Offset: 0x0000AFC1
		public static string Href(string path, params object[] pathParts)
		{
			return HelperPage.CurrentPage.Href(path, pathParts);
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x0000CDCF File Offset: 0x0000AFCF
		public static void WriteTo(TextWriter writer, object value)
		{
			WebPageExecutingBase.WriteTo(writer, value);
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x0000CDD8 File Offset: 0x0000AFD8
		public static void WriteLiteralTo(TextWriter writer, object value)
		{
			WebPageExecutingBase.WriteLiteralTo(writer, value);
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0000CDE1 File Offset: 0x0000AFE1
		public static void WriteTo(TextWriter writer, HelperResult value)
		{
			WebPageExecutingBase.WriteTo(writer, value);
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x0000CDEA File Offset: 0x0000AFEA
		public static void WriteLiteralTo(TextWriter writer, HelperResult value)
		{
			WebPageExecutingBase.WriteLiteralTo(writer, value);
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0000CDF3 File Offset: 0x0000AFF3
		public static void WriteAttributeTo(TextWriter writer, string name, PositionTagged<string> prefix, PositionTagged<string> suffix, params AttributeValue[] values)
		{
			HelperPage.CurrentPage.WriteAttributeTo(HelperPage.VirtualPath, writer, name, prefix, suffix, values);
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x0000CE0A File Offset: 0x0000B00A
		public static void BeginContext(string virtualPath, int startPosition, int length, bool isLiteral)
		{
			HelperPage.BeginContext(HelperPage.PageContext.Page.GetOutputWriter(), virtualPath, startPosition, length, isLiteral);
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0000CE24 File Offset: 0x0000B024
		public static void BeginContext(TextWriter writer, string virtualPath, int startPosition, int length, bool isLiteral)
		{
			if (HelperPage.InstrumentationService.IsAvailable)
			{
				HelperPage.InstrumentationService.BeginContext(HelperPage.Context, virtualPath, writer, startPosition, length, isLiteral);
			}
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x0000CE47 File Offset: 0x0000B047
		public static void EndContext(string virtualPath, int startPosition, int length, bool isLiteral)
		{
			HelperPage.EndContext(HelperPage.PageContext.Page.GetOutputWriter(), virtualPath, startPosition, length, isLiteral);
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0000CE61 File Offset: 0x0000B061
		public static void EndContext(TextWriter writer, string virtualPath, int startPosition, int length, bool isLiteral)
		{
			if (HelperPage.InstrumentationService.IsAvailable)
			{
				HelperPage.InstrumentationService.EndContext(HelperPage.Context, virtualPath, writer, startPosition, length, isLiteral);
			}
		}

		// Token: 0x04000123 RID: 291
		private static WebPageContext _pageContext;

		// Token: 0x04000124 RID: 292
		private static InstrumentationService _instrumentationService = null;
	}
}
