using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Threading;
using System.Web.Caching;
using System.Web.Profile;
using Microsoft.Internal.Web.Utils;

namespace System.Web.WebPages
{
	// Token: 0x0200006A RID: 106
	public abstract class WebPageRenderingBase : WebPageExecutingBase, ITemplateFile
	{
		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000297 RID: 663 RVA: 0x0000A0A1 File Offset: 0x000082A1
		public virtual Cache Cache
		{
			get
			{
				if (this.Context != null)
				{
					return this.Context.Cache;
				}
				return null;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000298 RID: 664 RVA: 0x0000A0B8 File Offset: 0x000082B8
		// (set) Token: 0x06000299 RID: 665 RVA: 0x0000A0C9 File Offset: 0x000082C9
		internal DisplayModeProvider DisplayModeProvider
		{
			get
			{
				return this._displayModeProvider ?? DisplayModeProvider.Instance;
			}
			set
			{
				this._displayModeProvider = value;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600029A RID: 666 RVA: 0x0000A0D2 File Offset: 0x000082D2
		protected internal IDisplayMode DisplayMode
		{
			get
			{
				return DisplayModeProvider.GetDisplayMode(this.Context);
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600029B RID: 667
		// (set) Token: 0x0600029C RID: 668
		public abstract string Layout { get; set; }

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600029D RID: 669
		[Dynamic(new bool[]
		{
			false,
			false,
			true
		})]
		public abstract IDictionary<object, dynamic> PageData { [return: Dynamic(new bool[]
		{
			false,
			false,
			true
		})] get; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600029E RID: 670
		[Dynamic]
		public abstract dynamic Page { [return: Dynamic] get; }

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600029F RID: 671 RVA: 0x0000A0DF File Offset: 0x000082DF
		// (set) Token: 0x060002A0 RID: 672 RVA: 0x0000A0E7 File Offset: 0x000082E7
		public WebPageContext PageContext { get; internal set; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x0000A0F0 File Offset: 0x000082F0
		public ProfileBase Profile
		{
			get
			{
				if (this.Context != null)
				{
					return this.Context.Profile;
				}
				return null;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x0000A107 File Offset: 0x00008307
		public virtual HttpRequestBase Request
		{
			get
			{
				if (this.Context != null)
				{
					return this.Context.Request;
				}
				return null;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x0000A11E File Offset: 0x0000831E
		public virtual HttpResponseBase Response
		{
			get
			{
				if (this.Context != null)
				{
					return this.Context.Response;
				}
				return null;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x0000A135 File Offset: 0x00008335
		public virtual HttpServerUtilityBase Server
		{
			get
			{
				if (this.Context != null)
				{
					return this.Context.Server;
				}
				return null;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x0000A14C File Offset: 0x0000834C
		public virtual HttpSessionStateBase Session
		{
			get
			{
				if (this.Context != null)
				{
					return this.Context.Session;
				}
				return null;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x0000A164 File Offset: 0x00008364
		public virtual IList<string> UrlData
		{
			get
			{
				if (this._urlData == null)
				{
					WebPageMatch webPageMatch = WebPageRoute.GetWebPageMatch(this.Context);
					if (webPageMatch != null)
					{
						this._urlData = new UrlDataList(webPageMatch.PathInfo);
					}
					else
					{
						this._urlData = new UrlDataList(null);
					}
				}
				return this._urlData;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x0000A1AD File Offset: 0x000083AD
		// (set) Token: 0x060002A8 RID: 680 RVA: 0x0000A1C9 File Offset: 0x000083C9
		public virtual IPrincipal User
		{
			get
			{
				if (this._user == null)
				{
					return this.Context.User;
				}
				return this._user;
			}
			internal set
			{
				this._user = value;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x0000A1D2 File Offset: 0x000083D2
		public virtual TemplateFileInfo TemplateInfo
		{
			get
			{
				if (this._templateFileInfo == null)
				{
					this._templateFileInfo = new TemplateFileInfo(this.VirtualPath);
				}
				return this._templateFileInfo;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060002AA RID: 682 RVA: 0x0000A1F3 File Offset: 0x000083F3
		public virtual bool IsPost
		{
			get
			{
				return this.Request.HttpMethod == "POST";
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060002AB RID: 683 RVA: 0x0000A20C File Offset: 0x0000840C
		public virtual bool IsAjax
		{
			get
			{
				HttpRequestBase request = this.Request;
				return request != null && (request["X-Requested-With"] == "XMLHttpRequest" || (request.Headers != null && request.Headers["X-Requested-With"] == "XMLHttpRequest"));
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060002AC RID: 684 RVA: 0x0000A262 File Offset: 0x00008462
		// (set) Token: 0x060002AD RID: 685 RVA: 0x0000A273 File Offset: 0x00008473
		public string Culture
		{
			get
			{
				return Thread.CurrentThread.CurrentCulture.Name;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					throw new ArgumentException(CommonResources.Argument_Cannot_Be_Null_Or_Empty, "value");
				}
				CultureUtil.SetCulture(Thread.CurrentThread, this.Context, value);
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060002AE RID: 686 RVA: 0x0000A29E File Offset: 0x0000849E
		// (set) Token: 0x060002AF RID: 687 RVA: 0x0000A2AF File Offset: 0x000084AF
		public string UICulture
		{
			get
			{
				return Thread.CurrentThread.CurrentUICulture.Name;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					throw new ArgumentException(CommonResources.Argument_Cannot_Be_Null_Or_Empty, "value");
				}
				CultureUtil.SetUICulture(Thread.CurrentThread, this.Context, value);
			}
		}

		// Token: 0x060002B0 RID: 688
		public abstract void ExecutePageHierarchy();

		// Token: 0x060002B1 RID: 689
		public abstract HelperResult RenderPage(string path, params object[] data);

		// Token: 0x040000D9 RID: 217
		private IPrincipal _user;

		// Token: 0x040000DA RID: 218
		private UrlDataList _urlData;

		// Token: 0x040000DB RID: 219
		private TemplateFileInfo _templateFileInfo;

		// Token: 0x040000DC RID: 220
		private DisplayModeProvider _displayModeProvider;
	}
}
