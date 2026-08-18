using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web.Caching;
using System.Web.Configuration;
using System.Web.Instrumentation;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.WebSockets;

namespace System.Web
{
	// Token: 0x02000029 RID: 41
	[TypeForwardedFrom("System.Web.Abstractions, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	public class HttpContextWrapper : HttpContextBase
	{
		// Token: 0x0600027E RID: 638 RVA: 0x00004C2B File Offset: 0x00002E2B
		public HttpContextWrapper(HttpContext httpContext)
		{
			if (httpContext == null)
			{
				throw new ArgumentNullException("httpContext");
			}
			this._context = httpContext;
		}

		// Token: 0x0600027F RID: 639 RVA: 0x00004C48 File Offset: 0x00002E48
		public override ISubscriptionToken AddOnRequestCompleted(Action<HttpContextBase> callback)
		{
			return this._context.AddOnRequestCompleted(HttpContextWrapper.WrapCallback(callback));
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000280 RID: 640 RVA: 0x00004C5B File Offset: 0x00002E5B
		public override Exception[] AllErrors
		{
			get
			{
				return this._context.AllErrors;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000281 RID: 641 RVA: 0x00004C68 File Offset: 0x00002E68
		// (set) Token: 0x06000282 RID: 642 RVA: 0x00004C75 File Offset: 0x00002E75
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public override bool AllowAsyncDuringSyncStages
		{
			get
			{
				return this._context.AllowAsyncDuringSyncStages;
			}
			set
			{
				this._context.AllowAsyncDuringSyncStages = value;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000283 RID: 643 RVA: 0x00004C83 File Offset: 0x00002E83
		public override HttpApplicationStateBase Application
		{
			get
			{
				return new HttpApplicationStateWrapper(this._context.Application);
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000284 RID: 644 RVA: 0x00004C95 File Offset: 0x00002E95
		// (set) Token: 0x06000285 RID: 645 RVA: 0x00004CA2 File Offset: 0x00002EA2
		public override HttpApplication ApplicationInstance
		{
			get
			{
				return this._context.ApplicationInstance;
			}
			set
			{
				this._context.ApplicationInstance = value;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000286 RID: 646 RVA: 0x00004CB0 File Offset: 0x00002EB0
		// (set) Token: 0x06000287 RID: 647 RVA: 0x00004CBD File Offset: 0x00002EBD
		public override AsyncPreloadModeFlags AsyncPreloadMode
		{
			get
			{
				return this._context.AsyncPreloadMode;
			}
			set
			{
				this._context.AsyncPreloadMode = value;
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000288 RID: 648 RVA: 0x00004CCB File Offset: 0x00002ECB
		public override Cache Cache
		{
			get
			{
				return this._context.Cache;
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000289 RID: 649 RVA: 0x00004CD8 File Offset: 0x00002ED8
		public override IHttpHandler CurrentHandler
		{
			get
			{
				return this._context.CurrentHandler;
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x0600028A RID: 650 RVA: 0x00004CE5 File Offset: 0x00002EE5
		public override RequestNotification CurrentNotification
		{
			get
			{
				return this._context.CurrentNotification;
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x0600028B RID: 651 RVA: 0x00004CF2 File Offset: 0x00002EF2
		public override Exception Error
		{
			get
			{
				return this._context.Error;
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x0600028C RID: 652 RVA: 0x00004CFF File Offset: 0x00002EFF
		// (set) Token: 0x0600028D RID: 653 RVA: 0x00004D0C File Offset: 0x00002F0C
		public override IHttpHandler Handler
		{
			get
			{
				return this._context.Handler;
			}
			set
			{
				this._context.Handler = value;
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x0600028E RID: 654 RVA: 0x00004D1A File Offset: 0x00002F1A
		public override bool IsCustomErrorEnabled
		{
			get
			{
				return this._context.IsCustomErrorEnabled;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x0600028F RID: 655 RVA: 0x00004D27 File Offset: 0x00002F27
		public override bool IsDebuggingEnabled
		{
			get
			{
				return this._context.IsDebuggingEnabled;
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000290 RID: 656 RVA: 0x00004D34 File Offset: 0x00002F34
		public override bool IsPostNotification
		{
			get
			{
				return this._context.IsPostNotification;
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000291 RID: 657 RVA: 0x00004D41 File Offset: 0x00002F41
		public override bool IsWebSocketRequest
		{
			get
			{
				return this._context.IsWebSocketRequest;
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000292 RID: 658 RVA: 0x00004D4E File Offset: 0x00002F4E
		public override bool IsWebSocketRequestUpgrading
		{
			get
			{
				return this._context.IsWebSocketRequestUpgrading;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000293 RID: 659 RVA: 0x00004D5B File Offset: 0x00002F5B
		public override IDictionary Items
		{
			get
			{
				return this._context.Items;
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000294 RID: 660 RVA: 0x00004D68 File Offset: 0x00002F68
		public override PageInstrumentationService PageInstrumentation
		{
			get
			{
				return this._context.PageInstrumentation;
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000295 RID: 661 RVA: 0x00004D75 File Offset: 0x00002F75
		public override IHttpHandler PreviousHandler
		{
			get
			{
				return this._context.PreviousHandler;
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000296 RID: 662 RVA: 0x00004D82 File Offset: 0x00002F82
		public override ProfileBase Profile
		{
			get
			{
				return this._context.Profile;
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000297 RID: 663 RVA: 0x00004D8F File Offset: 0x00002F8F
		public override HttpRequestBase Request
		{
			get
			{
				return new HttpRequestWrapper(this._context.Request);
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000298 RID: 664 RVA: 0x00004DA1 File Offset: 0x00002FA1
		public override HttpResponseBase Response
		{
			get
			{
				return new HttpResponseWrapper(this._context.Response);
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000299 RID: 665 RVA: 0x00004DB3 File Offset: 0x00002FB3
		public override HttpServerUtilityBase Server
		{
			get
			{
				return new HttpServerUtilityWrapper(this._context.Server);
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x0600029A RID: 666 RVA: 0x00004DC8 File Offset: 0x00002FC8
		public override HttpSessionStateBase Session
		{
			get
			{
				HttpSessionState session = this._context.Session;
				if (session == null)
				{
					return null;
				}
				return new HttpSessionStateWrapper(session);
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x0600029B RID: 667 RVA: 0x00004DEC File Offset: 0x00002FEC
		// (set) Token: 0x0600029C RID: 668 RVA: 0x00004DF9 File Offset: 0x00002FF9
		public override bool SkipAuthorization
		{
			get
			{
				return this._context.SkipAuthorization;
			}
			set
			{
				this._context.SkipAuthorization = value;
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x0600029D RID: 669 RVA: 0x00004E07 File Offset: 0x00003007
		public override DateTime Timestamp
		{
			get
			{
				return this._context.Timestamp;
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x0600029E RID: 670 RVA: 0x00004E14 File Offset: 0x00003014
		// (set) Token: 0x0600029F RID: 671 RVA: 0x00004E21 File Offset: 0x00003021
		public override bool ThreadAbortOnTimeout
		{
			get
			{
				return this._context.ThreadAbortOnTimeout;
			}
			set
			{
				this._context.ThreadAbortOnTimeout = value;
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x00004E2F File Offset: 0x0000302F
		public override TraceContext Trace
		{
			get
			{
				return this._context.Trace;
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x00004E3C File Offset: 0x0000303C
		// (set) Token: 0x060002A2 RID: 674 RVA: 0x00004E49 File Offset: 0x00003049
		public override IPrincipal User
		{
			get
			{
				return this._context.User;
			}
			set
			{
				this._context.User = value;
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x00004E57 File Offset: 0x00003057
		public override string WebSocketNegotiatedProtocol
		{
			get
			{
				return this._context.WebSocketNegotiatedProtocol;
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x00004E64 File Offset: 0x00003064
		public override IList<string> WebSocketRequestedProtocols
		{
			get
			{
				return this._context.WebSocketRequestedProtocols;
			}
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x00004E71 File Offset: 0x00003071
		public override void AcceptWebSocketRequest(Func<AspNetWebSocketContext, Task> userFunc)
		{
			this._context.AcceptWebSocketRequest(userFunc);
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x00004E7F File Offset: 0x0000307F
		public override void AcceptWebSocketRequest(Func<AspNetWebSocketContext, Task> userFunc, AspNetWebSocketOptions options)
		{
			this._context.AcceptWebSocketRequest(userFunc, options);
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x00004E8E File Offset: 0x0000308E
		public override void AddError(Exception errorInfo)
		{
			this._context.AddError(errorInfo);
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x00004E9C File Offset: 0x0000309C
		public override void ClearError()
		{
			this._context.ClearError();
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x00004EA9 File Offset: 0x000030A9
		public override ISubscriptionToken DisposeOnPipelineCompleted(IDisposable target)
		{
			return this._context.DisposeOnPipelineCompleted(target);
		}

		// Token: 0x060002AA RID: 682 RVA: 0x00004EB7 File Offset: 0x000030B7
		public override object GetGlobalResourceObject(string classKey, string resourceKey)
		{
			return HttpContext.GetGlobalResourceObject(classKey, resourceKey);
		}

		// Token: 0x060002AB RID: 683 RVA: 0x00004EC0 File Offset: 0x000030C0
		public override object GetGlobalResourceObject(string classKey, string resourceKey, CultureInfo culture)
		{
			return HttpContext.GetGlobalResourceObject(classKey, resourceKey, culture);
		}

		// Token: 0x060002AC RID: 684 RVA: 0x00004ECA File Offset: 0x000030CA
		public override object GetLocalResourceObject(string virtualPath, string resourceKey)
		{
			return HttpContext.GetLocalResourceObject(virtualPath, resourceKey);
		}

		// Token: 0x060002AD RID: 685 RVA: 0x00004ED3 File Offset: 0x000030D3
		public override object GetLocalResourceObject(string virtualPath, string resourceKey, CultureInfo culture)
		{
			return HttpContext.GetLocalResourceObject(virtualPath, resourceKey, culture);
		}

		// Token: 0x060002AE RID: 686 RVA: 0x00004EDD File Offset: 0x000030DD
		public override object GetSection(string sectionName)
		{
			return this._context.GetSection(sectionName);
		}

		// Token: 0x060002AF RID: 687 RVA: 0x00004EEB File Offset: 0x000030EB
		public override void RemapHandler(IHttpHandler handler)
		{
			this._context.RemapHandler(handler);
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x00004EF9 File Offset: 0x000030F9
		public override void RewritePath(string path)
		{
			this._context.RewritePath(path);
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x00004F07 File Offset: 0x00003107
		public override void RewritePath(string path, bool rebaseClientPath)
		{
			this._context.RewritePath(path, rebaseClientPath);
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x00004F16 File Offset: 0x00003116
		public override void RewritePath(string filePath, string pathInfo, string queryString)
		{
			this._context.RewritePath(filePath, pathInfo, queryString);
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x00004F26 File Offset: 0x00003126
		public override void RewritePath(string filePath, string pathInfo, string queryString, bool setClientFilePath)
		{
			this._context.RewritePath(filePath, pathInfo, queryString, setClientFilePath);
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x00004F38 File Offset: 0x00003138
		public override void SetSessionStateBehavior(SessionStateBehavior sessionStateBehavior)
		{
			this._context.SetSessionStateBehavior(sessionStateBehavior);
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x00004F46 File Offset: 0x00003146
		public override object GetService(Type serviceType)
		{
			return ((IServiceProvider)this._context).GetService(serviceType);
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x00004F54 File Offset: 0x00003154
		internal static Action<HttpContext> WrapCallback(Action<HttpContextBase> callback)
		{
			if (callback != null)
			{
				return delegate(HttpContext context)
				{
					callback(new HttpContextWrapper(context));
				};
			}
			return null;
		}

		// Token: 0x0400010A RID: 266
		private readonly HttpContext _context;
	}
}
