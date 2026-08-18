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
	// Token: 0x02000028 RID: 40
	[TypeForwardedFrom("System.Web.Abstractions, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	public abstract class HttpContextBase : IServiceProvider
	{
		// Token: 0x06000246 RID: 582 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual ISubscriptionToken AddOnRequestCompleted(Action<HttpContextBase> callback)
		{
			throw new NotImplementedException();
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000247 RID: 583 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual Exception[] AllErrors
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000248 RID: 584 RVA: 0x00003ABB File Offset: 0x00001CBB
		// (set) Token: 0x06000249 RID: 585 RVA: 0x00003ABB File Offset: 0x00001CBB
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public virtual bool AllowAsyncDuringSyncStages
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x0600024A RID: 586 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual HttpApplicationStateBase Application
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x0600024B RID: 587 RVA: 0x00003ABB File Offset: 0x00001CBB
		// (set) Token: 0x0600024C RID: 588 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual HttpApplication ApplicationInstance
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x0600024D RID: 589 RVA: 0x00003ABB File Offset: 0x00001CBB
		// (set) Token: 0x0600024E RID: 590 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual AsyncPreloadModeFlags AsyncPreloadMode
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x0600024F RID: 591 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual Cache Cache
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000250 RID: 592 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual IHttpHandler CurrentHandler
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000251 RID: 593 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual RequestNotification CurrentNotification
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000252 RID: 594 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual Exception Error
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000253 RID: 595 RVA: 0x00003ABB File Offset: 0x00001CBB
		// (set) Token: 0x06000254 RID: 596 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual IHttpHandler Handler
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000255 RID: 597 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool IsCustomErrorEnabled
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000256 RID: 598 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool IsDebuggingEnabled
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000257 RID: 599 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool IsPostNotification
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000258 RID: 600 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool IsWebSocketRequest
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000259 RID: 601 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool IsWebSocketRequestUpgrading
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x0600025A RID: 602 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual IDictionary Items
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x0600025B RID: 603 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual PageInstrumentationService PageInstrumentation
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x0600025C RID: 604 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual IHttpHandler PreviousHandler
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x0600025D RID: 605 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual ProfileBase Profile
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x0600025E RID: 606 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual HttpRequestBase Request
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x0600025F RID: 607 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual HttpResponseBase Response
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000260 RID: 608 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual HttpServerUtilityBase Server
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000261 RID: 609 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual HttpSessionStateBase Session
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000262 RID: 610 RVA: 0x00003ABB File Offset: 0x00001CBB
		// (set) Token: 0x06000263 RID: 611 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool SkipAuthorization
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000264 RID: 612 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual DateTime Timestamp
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000265 RID: 613 RVA: 0x00003ABB File Offset: 0x00001CBB
		// (set) Token: 0x06000266 RID: 614 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool ThreadAbortOnTimeout
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000267 RID: 615 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual TraceContext Trace
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000268 RID: 616 RVA: 0x00003ABB File Offset: 0x00001CBB
		// (set) Token: 0x06000269 RID: 617 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual IPrincipal User
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x0600026A RID: 618 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual string WebSocketNegotiatedProtocol
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x0600026B RID: 619 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual IList<string> WebSocketRequestedProtocols
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x0600026C RID: 620 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void AcceptWebSocketRequest(Func<AspNetWebSocketContext, Task> userFunc)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600026D RID: 621 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void AcceptWebSocketRequest(Func<AspNetWebSocketContext, Task> userFunc, AspNetWebSocketOptions options)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void AddError(Exception errorInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600026F RID: 623 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void ClearError()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000270 RID: 624 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual ISubscriptionToken DisposeOnPipelineCompleted(IDisposable target)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000271 RID: 625 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual object GetGlobalResourceObject(string classKey, string resourceKey)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000272 RID: 626 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual object GetGlobalResourceObject(string classKey, string resourceKey, CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000273 RID: 627 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual object GetLocalResourceObject(string virtualPath, string resourceKey)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000274 RID: 628 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual object GetLocalResourceObject(string virtualPath, string resourceKey, CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000275 RID: 629 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual object GetSection(string sectionName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000276 RID: 630 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void RemapHandler(IHttpHandler handler)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000277 RID: 631 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void RewritePath(string path)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000278 RID: 632 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void RewritePath(string path, bool rebaseClientPath)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000279 RID: 633 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void RewritePath(string filePath, string pathInfo, string queryString)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600027A RID: 634 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void RewritePath(string filePath, string pathInfo, string queryString, bool setClientFilePath)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600027B RID: 635 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void SetSessionStateBehavior(SessionStateBehavior sessionStateBehavior)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600027C RID: 636 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual object GetService(Type serviceType)
		{
			throw new NotImplementedException();
		}
	}
}
