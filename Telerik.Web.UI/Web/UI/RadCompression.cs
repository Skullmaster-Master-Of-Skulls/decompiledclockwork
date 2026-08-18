using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Security;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.UI;
using System.Web.UI.Adapters;

namespace Telerik.Web.UI
{
	// Token: 0x0200182B RID: 6187
	public class RadCompression : PageAdapter, IHttpModule
	{
		// Token: 0x0600F08A RID: 61578 RVA: 0x0036AD88 File Offset: 0x00368F88
		private static RadCompression GetCurrent()
		{
			return HttpContext.Current.ApplicationInstance.Modules["RadCompression"] as RadCompression;
		}

		// Token: 0x0600F08B RID: 61579 RVA: 0x0036ADA8 File Offset: 0x00368FA8
		public virtual bool IsHttpCompressionEnabled()
		{
			RadCompressionSettingsAttribute compressionSettingAttribute = this.GetCompressionSettingAttribute();
			return compressionSettingAttribute == null || compressionSettingAttribute.HttpCompression != CompressionType.None;
		}

		// Token: 0x0600F08C RID: 61580 RVA: 0x0036ADCD File Offset: 0x00368FCD
		protected virtual RadCompressionConfigurationSection GetConfigurationSection()
		{
			return WebConfigurationManager.GetSection("telerik.web.ui/radCompression") as RadCompressionConfigurationSection;
		}

		// Token: 0x0600F08D RID: 61581 RVA: 0x0036ADE0 File Offset: 0x00368FE0
		public virtual bool ShouldApplyOnPostback()
		{
			bool result = false;
			RadCompressionConfigurationSection configurationSection = this.GetConfigurationSection();
			if (configurationSection != null)
			{
				result = configurationSection.EnablePostbackCompression;
			}
			RadCompressionSettingsAttribute compressionSettingAttribute = this.GetCompressionSettingAttribute();
			if (compressionSettingAttribute != null)
			{
				result = compressionSettingAttribute.EnablePostbackCompression;
			}
			return result;
		}

		// Token: 0x170048B8 RID: 18616
		// (get) Token: 0x0600F08E RID: 61582 RVA: 0x0036AE14 File Offset: 0x00369014
		protected virtual bool IsTraceEnabled
		{
			get
			{
				RadCompressionConfigurationSection configurationSection = this.GetConfigurationSection();
				return configurationSection != null && configurationSection.EnableTracing;
			}
		}

		// Token: 0x0600F08F RID: 61583 RVA: 0x0036AE34 File Offset: 0x00369034
		private RadCompressionSettingsAttribute GetCompressionSettingAttribute()
		{
			IHttpHandler handler = HttpContext.Current.Handler;
			if (handler != null)
			{
				object[] customAttributes = handler.GetType().GetCustomAttributes(typeof(RadCompressionSettingsAttribute), true);
				if (customAttributes.Length > 0)
				{
					return (RadCompressionSettingsAttribute)customAttributes[0];
				}
			}
			return null;
		}

		// Token: 0x0600F090 RID: 61584 RVA: 0x0036AE78 File Offset: 0x00369078
		public virtual bool IsStateCompressionEnabled()
		{
			RadCompressionSettingsAttribute compressionSettingAttribute = this.GetCompressionSettingAttribute();
			return compressionSettingAttribute == null || compressionSettingAttribute.StateCompression != CompressionType.None;
		}

		// Token: 0x170048B9 RID: 18617
		// (get) Token: 0x0600F091 RID: 61585 RVA: 0x0036AE9D File Offset: 0x0036909D
		// (set) Token: 0x0600F092 RID: 61586 RVA: 0x0036AEA5 File Offset: 0x003690A5
		private protected List<RadCompressionExcludeSettingContainer> ExcludedPaths { protected get; private set; }

		// Token: 0x170048BA RID: 18618
		// (get) Token: 0x0600F093 RID: 61587 RVA: 0x0036AEAE File Offset: 0x003690AE
		// (set) Token: 0x0600F094 RID: 61588 RVA: 0x0036AEC9 File Offset: 0x003690C9
		protected string CurrentCompressionEncodingType
		{
			get
			{
				if (this._currentCompressionEncodingType == null)
				{
					this._currentCompressionEncodingType = string.Empty;
				}
				return this._currentCompressionEncodingType;
			}
			set
			{
				this._currentCompressionEncodingType = value;
			}
		}

		// Token: 0x0600F095 RID: 61589 RVA: 0x0036AEE0 File Offset: 0x003690E0
		public void Init(HttpApplication application)
		{
			this.Logger.Write(() => this.PrepareContextInfoString("Init"));
			this.PopulateExcludeHandlers();
			application.PreRequestHandlerExecute += this.PreRequestHandlerExecute;
			application.PostAcquireRequestState += this.application_PostAcquireRequestState;
			application.AcquireRequestState += this.application_AcquireRequestState;
			application.EndRequest += this.application_EndRequest;
		}

		// Token: 0x0600F096 RID: 61590 RVA: 0x0036AF54 File Offset: 0x00369154
		private void PopulateExcludeHandlers()
		{
			this.ExcludedPaths = new List<RadCompressionExcludeSettingContainer>();
			this.ExcludedPaths.Add(new RadCompressionExcludeSettingContainer
			{
				HandlerPath = ".axd",
				MatchExact = false
			});
			RadCompressionConfigurationSection configurationSection = this.GetConfigurationSection();
			if (configurationSection != null && configurationSection.ExcludeHandlers != null)
			{
				foreach (object obj in configurationSection.ExcludeHandlers)
				{
					RadCompressionExcludeSetting radCompressionExcludeSetting = (RadCompressionExcludeSetting)obj;
					this.ExcludedPaths.Add(new RadCompressionExcludeSettingContainer
					{
						HandlerPath = radCompressionExcludeSetting.HandlerPath,
						MatchExact = radCompressionExcludeSetting.MatchExact
					});
				}
			}
		}

		// Token: 0x0600F097 RID: 61591 RVA: 0x0036B025 File Offset: 0x00369225
		private void application_EndRequest(object sender, EventArgs e)
		{
			this.Logger.Write(() => this.PrepareContextInfoString("EndRequest"));
			if (this.ShouldExplicitlyAddContentEncoding())
			{
				HttpContext.Current.Response.AppendHeader("Content-encoding", this.CurrentCompressionEncodingType);
			}
		}

		// Token: 0x0600F098 RID: 61592 RVA: 0x0036B060 File Offset: 0x00369260
		private bool IsResponseCompressed()
		{
			return HttpContext.Current.Response.Filter is GZipStream || HttpContext.Current.Response.Filter is DeflateStream;
		}

		// Token: 0x0600F099 RID: 61593 RVA: 0x0036B094 File Offset: 0x00369294
		protected virtual bool ShouldExplicitlyAddContentEncoding()
		{
			return (this.ShouldApplyOnPostback() && HttpContext.Current.Response.StatusCode == 500) || (this.HasBeenCompressed && (HttpContext.Current.Request.ContentType.ToLower(CultureInfo.InvariantCulture).IndexOf("application/json") > -1 || HttpContext.Current.Response.StatusCode == 404 || this.IsResposeAjaxRedirect));
		}

		// Token: 0x0600F09A RID: 61594 RVA: 0x0036B11C File Offset: 0x0036931C
		private void application_AcquireRequestState(object sender, EventArgs e)
		{
			this.Logger.Write(() => this.PrepareContextInfoString("AcquireRequestState"));
			HttpApplication httpApplication = (HttpApplication)sender;
			if (httpApplication.Context.Handler is Page && RadCompression.IsMethodCall(httpApplication.Request))
			{
				this.Compress(httpApplication);
			}
		}

		// Token: 0x170048BB RID: 18619
		// (get) Token: 0x0600F09B RID: 61595 RVA: 0x0036B16D File Offset: 0x0036936D
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		protected bool IsResposeAjaxRedirect
		{
			get
			{
				return HttpContext.Current.Response.IsRequestBeingRedirected || (bool)(HttpContext.Current.Items["IsResposeRedirected"] ?? false);
			}
		}

		// Token: 0x0600F09C RID: 61596 RVA: 0x0036B1B4 File Offset: 0x003693B4
		private void application_PostAcquireRequestState(object sender, EventArgs e)
		{
			this.Logger.Write(() => this.PrepareContextInfoString("PostAcquireRequestState"));
			HttpApplication httpApplication = (HttpApplication)sender;
			if (httpApplication.Context.Handler is Page && RadCompression.IsMethodCall(httpApplication.Request))
			{
				this.Compress(httpApplication);
			}
		}

		// Token: 0x0600F09D RID: 61597 RVA: 0x0036B205 File Offset: 0x00369405
		private static bool IsMethodCall(HttpRequest request)
		{
			return !string.IsNullOrEmpty(request.PathInfo) && (request.ContentType.StartsWith("application/json;", StringComparison.OrdinalIgnoreCase) || string.Equals(request.ContentType, "application/json", StringComparison.OrdinalIgnoreCase));
		}

		// Token: 0x0600F09E RID: 61598 RVA: 0x0036B23C File Offset: 0x0036943C
		private void PreRequestHandlerExecute(object sender, EventArgs e)
		{
			this.Compress((HttpApplication)sender);
		}

		// Token: 0x0600F09F RID: 61599 RVA: 0x0036B24C File Offset: 0x0036944C
		protected virtual bool IsAjaxRequest()
		{
			return HttpContext.Current.Request["HTTP_X_MICROSOFTAJAX"] != null || HttpContext.Current.Request["http_x-microsoftajax"] != null || HttpContext.Current.Request["X-MICROSOFTAJAX"] != null || HttpContext.Current.Request["x-microsoftajax"] != null;
		}

		// Token: 0x170048BC RID: 18620
		// (get) Token: 0x0600F0A0 RID: 61600 RVA: 0x0036B2B8 File Offset: 0x003694B8
		// (set) Token: 0x0600F0A1 RID: 61601 RVA: 0x0036B2EA File Offset: 0x003694EA
		protected virtual bool HasBeenCompressed
		{
			get
			{
				object obj = HttpContext.Current.Items["hasBeenCompressed"];
				if (obj == null)
				{
					obj = false;
				}
				return (bool)obj;
			}
			set
			{
				HttpContext.Current.Items["hasBeenCompressed"] = value;
			}
		}

		// Token: 0x0600F0A2 RID: 61602 RVA: 0x0036B308 File Offset: 0x00369508
		protected virtual bool IsRIADataService(IHttpHandler handler)
		{
			try
			{
				if (handler != null)
				{
					return handler.GetType().FullName.IndexOf("System.Web.Ria.DataService") > -1 || (HostingEnvironment.VirtualPathProvider != null && (HostingEnvironment.VirtualPathProvider.GetType().FullName.IndexOf("System.Web.Ria.Service") > -1 || HostingEnvironment.VirtualPathProvider.GetType().FullName.IndexOf("System.ServiceModel.DomainServices.Hosting.DomainServiceVirtualPathProvider") > -1));
				}
			}
			catch (SecurityException)
			{
			}
			return false;
		}

		// Token: 0x0600F0A3 RID: 61603 RVA: 0x0036B394 File Offset: 0x00369594
		protected virtual bool IsIISRootRequest()
		{
			return HttpContext.Current.Handler == null && HttpContext.Current.Request.Path.Remove(0, HttpContext.Current.Request.ApplicationPath.Length).Equals("/");
		}

		// Token: 0x0600F0A4 RID: 61604 RVA: 0x0036B428 File Offset: 0x00369628
		private void Compress(HttpApplication application)
		{
			if (!this.IsHttpCompressionEnabled())
			{
				return;
			}
			this.Logger.Write(() => this.PrepareContextInfoString("Compress -- Enter"));
			HttpRequest request = application.Request;
			HttpResponse response = application.Response;
			string text = request.ContentType.ToLower(CultureInfo.InvariantCulture);
			string acceptTypes = (request.AcceptTypes == null) ? string.Empty : string.Join(";", request.AcceptTypes);
			string text2 = request.Path.Remove(0, request.ApplicationPath.Length);
			if (text2.StartsWith("/"))
			{
				text2 = text2.Substring(1);
			}
			if (this.ShouldExclude(text2))
			{
				this.Logger.Write(() => this.PrepareContextInfoString("Compress -- Exit -- handler is excluded"));
				return;
			}
			if (this.IsIISRootRequest())
			{
				return;
			}
			if ((text.StartsWith("application/x-www-form-urlencoded") && !this.IsAjaxRequest() && !this.ShouldApplyOnPostback()) || text.StartsWith("multipart/form-data"))
			{
				this.Logger.Write(() => this.PrepareContextInfoString("Compress -- Exit -- post"));
				return;
			}
			if ((string.IsNullOrEmpty(text) && (application.Context.Handler == null || application.Context.Handler is Page || application.Context.Handler is DefaultHttpHandler)) || this.HasBeenCompressed || string.Compare(application.Context.Handler.GetType().FullName, "System.Web.Handlers.TransferRequestHandler") == 0)
			{
				this.Logger.Write(() => this.PrepareContextInfoString("Compress -- Exit -- initial load"));
				return;
			}
			if ((this.ShouldCompressContentType(text) || this.ShouldCompressAcceptType(acceptTypes) || this.IsRIADataService(application.Context.Handler)) && (!request.Browser.IsBrowser("IE") || request.Browser.MajorVersion > 6))
			{
				string text3 = request.Headers["Accept-Encoding"];
				if (!string.IsNullOrEmpty(text3))
				{
					text3 = text3.ToLower(CultureInfo.InvariantCulture);
					string text4 = "gzip";
					if (text3.Contains("gzip"))
					{
						this.HasBeenCompressed = true;
						response.Filter = new RadCompression.RadGZipStreamNet40(response.Filter, CompressionMode.Compress);
						if (!text.StartsWith("application/json"))
						{
							response.AddHeader("Content-encoding", text4);
						}
					}
					else if (text3.Contains("deflate"))
					{
						text4 = "deflate";
						this.HasBeenCompressed = true;
						response.Filter = new DeflateStream(response.Filter, CompressionMode.Compress);
						if (!text.StartsWith("application/json"))
						{
							response.AddHeader("Content-encoding", text4);
						}
					}
					this.CurrentCompressionEncodingType = text4;
				}
			}
			this.Logger.Write(() => this.PrepareContextInfoString("Compress -- Exit"));
		}

		// Token: 0x0600F0A5 RID: 61605 RVA: 0x0036B6F5 File Offset: 0x003698F5
		protected virtual bool ShouldCompressContentType(string contentType)
		{
			return contentType.StartsWith("application/x-www-form-urlencoded") || contentType.StartsWith("application/json") || contentType.StartsWith("text/xml") || contentType.StartsWith("application/soap+msbin1");
		}

		// Token: 0x0600F0A6 RID: 61606 RVA: 0x0036B72B File Offset: 0x0036992B
		protected virtual bool ShouldCompressAcceptType(string acceptTypes)
		{
			return acceptTypes.IndexOf("application/xml") != -1 || acceptTypes.IndexOf("application/atom+xml") != -1 || acceptTypes.IndexOf("application/json") != -1;
		}

		// Token: 0x0600F0A7 RID: 61607 RVA: 0x0036B7E4 File Offset: 0x003699E4
		protected virtual bool ShouldExclude(string handlerPath)
		{
			return this.ExcludedPaths != null && this.ExcludedPaths.Find(delegate(RadCompressionExcludeSettingContainer entry)
			{
				if (entry == null)
				{
					return false;
				}
				string text = handlerPath.Trim();
				if (entry.MatchExact)
				{
					return text.Equals(entry.HandlerPath, StringComparison.OrdinalIgnoreCase);
				}
				if (entry.HandlerPath.Equals(".asp", StringComparison.OrdinalIgnoreCase))
				{
					return text.IndexOf(".asp") > -1 && text.IndexOf(".aspx") == -1;
				}
				return text.ToLower().IndexOf(entry.HandlerPath.ToLower()) > -1;
			}) != null;
		}

		// Token: 0x170048BD RID: 18621
		// (get) Token: 0x0600F0A8 RID: 61608 RVA: 0x0036B822 File Offset: 0x00369A22
		private IRadCompressionLogger Logger
		{
			get
			{
				this._logger = (this._logger ?? new SimpleCompressionLogger(this.IsTraceEnabled));
				return this._logger;
			}
		}

		// Token: 0x0600F0A9 RID: 61609 RVA: 0x0036B845 File Offset: 0x00369A45
		private string PrepareContextInfoString(string methodName)
		{
			return this.PrepareContextInfoString(methodName, HttpContext.Current);
		}

		// Token: 0x0600F0AA RID: 61610 RVA: 0x0036B854 File Offset: 0x00369A54
		private string PrepareContextInfoString(string methodName, HttpContext context)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("{0}", methodName);
			stringBuilder.AppendLine();
			stringBuilder.AppendFormat("Request-Path:{0}", context.Request.Path);
			stringBuilder.AppendLine();
			stringBuilder.AppendFormat("Request-ContentType:{0}", context.Request.ContentType.ToLower(CultureInfo.InvariantCulture));
			stringBuilder.AppendLine();
			stringBuilder.AppendFormat("Request-AcceptTypes:{0}", (context.Request.AcceptTypes == null) ? string.Empty : string.Join(";", context.Request.AcceptTypes));
			stringBuilder.AppendLine();
			return stringBuilder.ToString();
		}

		// Token: 0x0600F0AB RID: 61611 RVA: 0x0036B903 File Offset: 0x00369B03
		public void Dispose()
		{
		}

		// Token: 0x0400454C RID: 17740
		private string _currentCompressionEncodingType;

		// Token: 0x0400454D RID: 17741
		private IRadCompressionLogger _logger;

		// Token: 0x0200182C RID: 6188
		internal class RadGZipStreamNet40 : GZipStream
		{
			// Token: 0x0600F0B5 RID: 61621 RVA: 0x0036B905 File Offset: 0x00369B05
			public RadGZipStreamNet40(Stream stream, CompressionMode mode) : base(stream, mode)
			{
			}

			// Token: 0x0600F0B6 RID: 61622 RVA: 0x0036B90F File Offset: 0x00369B0F
			public RadGZipStreamNet40(Stream stream, CompressionMode mode, bool leaveOpen) : base(stream, mode, leaveOpen)
			{
			}

			// Token: 0x0600F0B7 RID: 61623 RVA: 0x0036B91C File Offset: 0x00369B1C
			public override void Write(byte[] array, int offset, int count)
			{
				if (offset < 40 && array.Length > 0 && !this.IsResposeAjaxRedirect)
				{
					string @string = Encoding.Default.GetString(array);
					if (!string.IsNullOrWhiteSpace(@string) && @string.Contains("1|#||4|") && @string.Contains("pageRedirect"))
					{
						this.IsResposeAjaxRedirect = true;
					}
				}
				base.Write(array, offset, count);
			}

			// Token: 0x170048BE RID: 18622
			// (get) Token: 0x0600F0B8 RID: 61624 RVA: 0x0036B97C File Offset: 0x00369B7C
			// (set) Token: 0x0600F0B9 RID: 61625 RVA: 0x0036B9AE File Offset: 0x00369BAE
			[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
			protected bool IsResposeAjaxRedirect
			{
				get
				{
					object obj = HttpContext.Current.Items["IsResposeRedirected"] ?? false;
					return (bool)obj;
				}
				set
				{
					HttpContext.Current.Items["IsResposeRedirected"] = value;
				}
			}
		}
	}
}
