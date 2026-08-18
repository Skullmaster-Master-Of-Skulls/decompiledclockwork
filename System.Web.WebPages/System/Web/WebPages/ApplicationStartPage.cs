using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Web.Caching;
using System.Web.Hosting;
using Microsoft.Web.Infrastructure;

namespace System.Web.WebPages
{
	// Token: 0x02000012 RID: 18
	public abstract class ApplicationStartPage : WebPageExecutingBase
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600008A RID: 138 RVA: 0x000033FD File Offset: 0x000015FD
		// (set) Token: 0x0600008B RID: 139 RVA: 0x00003405 File Offset: 0x00001605
		public HttpApplication Application { get; internal set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600008C RID: 140 RVA: 0x0000340E File Offset: 0x0000160E
		public override HttpContextBase Context
		{
			get
			{
				return new HttpContextWrapper(this.Application.Context);
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00003420 File Offset: 0x00001620
		// (set) Token: 0x0600008E RID: 142 RVA: 0x00003427 File Offset: 0x00001627
		public static HtmlString Markup { get; private set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600008F RID: 143 RVA: 0x0000342F File Offset: 0x0000162F
		// (set) Token: 0x06000090 RID: 144 RVA: 0x00003436 File Offset: 0x00001636
		internal static Exception Exception { get; private set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000091 RID: 145 RVA: 0x0000343E File Offset: 0x0000163E
		// (set) Token: 0x06000092 RID: 146 RVA: 0x00003446 File Offset: 0x00001646
		public TextWriter Output { get; internal set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000093 RID: 147 RVA: 0x0000344F File Offset: 0x0000164F
		// (set) Token: 0x06000094 RID: 148 RVA: 0x00003456 File Offset: 0x00001656
		public override string VirtualPath
		{
			get
			{
				return ApplicationStartPage.StartPageVirtualPath;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000095 RID: 149 RVA: 0x0000348A File Offset: 0x0000168A
		internal void ExecuteInternal()
		{
			ApplicationStartPage._safeExecuteStartPageThunk(delegate
			{
				this.Output = new StringWriter(CultureInfo.InvariantCulture);
				this.Execute();
				ApplicationStartPage.Markup = new HtmlString(this.Output.ToString());
			});
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000034AA File Offset: 0x000016AA
		internal static void ExecuteStartPage(HttpApplication application)
		{
			ApplicationStartPage.ExecuteStartPage(application, delegate(string vpath)
			{
				ApplicationStartPage.MonitorFile(vpath);
			}, VirtualPathFactoryManager.Instance, WebPageHttpHandler.GetRegisteredExtensions());
		}

		// Token: 0x06000097 RID: 151 RVA: 0x000034DC File Offset: 0x000016DC
		internal static void ExecuteStartPage(HttpApplication application, Action<string> monitorFile, IVirtualPathFactory virtualPathFactory, IEnumerable<string> supportedExtensions)
		{
			try
			{
				ApplicationStartPage.ExecuteStartPageInternal(application, monitorFile, virtualPathFactory, supportedExtensions);
			}
			catch (Exception ex)
			{
				ApplicationStartPage.Exception = ex;
				throw new HttpException(null, ex);
			}
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003514 File Offset: 0x00001714
		internal static void ExecuteStartPageInternal(HttpApplication application, Action<string> monitorFile, IVirtualPathFactory virtualPathFactory, IEnumerable<string> supportedExtensions)
		{
			ApplicationStartPage applicationStartPage = null;
			foreach (string str in supportedExtensions)
			{
				string text = ApplicationStartPage.StartPageVirtualPath + str;
				monitorFile(text);
				if (virtualPathFactory.Exists(text) && applicationStartPage == null)
				{
					applicationStartPage = virtualPathFactory.CreateInstance(text);
					applicationStartPage.Application = application;
					applicationStartPage.VirtualPathFactory = virtualPathFactory;
					applicationStartPage.ExecuteInternal();
				}
			}
		}

		// Token: 0x06000099 RID: 153 RVA: 0x0000359C File Offset: 0x0000179C
		private static Action<Action> GetSafeExecuteStartPageThunk()
		{
			if (typeof(HttpResponse).GetProperty("DisableCustomHttpEncoder", BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.NonPublic) != null)
			{
				return new Action<Action>(HttpContextHelper.ExecuteInNullContext);
			}
			return delegate(Action action)
			{
				action();
			};
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000035F1 File Offset: 0x000017F1
		private static void InitiateShutdown(string key, object value, CacheItemRemovedReason reason)
		{
			if (reason != CacheItemRemovedReason.DependencyChanged)
			{
				return;
			}
			ThreadPool.QueueUserWorkItem(new WaitCallback(ApplicationStartPage.ShutdownCallBack));
		}

		// Token: 0x0600009B RID: 155 RVA: 0x0000360C File Offset: 0x0000180C
		private static void MonitorFile(string virtualPath)
		{
			List<string> list = new List<string>();
			list.Add(virtualPath);
			CacheDependency cacheDependency = HostingEnvironment.VirtualPathProvider.GetCacheDependency(virtualPath, list, DateTime.UtcNow);
			string key = ApplicationStartPage.CacheKeyPrefix + virtualPath;
			HttpRuntime.Cache.Insert(key, virtualPath, cacheDependency, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, new CacheItemRemovedCallback(ApplicationStartPage.InitiateShutdown));
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003668 File Offset: 0x00001868
		private static void ShutdownCallBack(object state)
		{
			InfrastructureHelper.UnloadAppDomain();
		}

		// Token: 0x0600009D RID: 157 RVA: 0x0000366F File Offset: 0x0000186F
		public override void Write(HelperResult result)
		{
			if (result != null)
			{
				result.WriteTo(this.Output);
			}
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00003680 File Offset: 0x00001880
		public override void WriteLiteral(object value)
		{
			this.Output.Write(value);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x0000368E File Offset: 0x0000188E
		public override void Write(object value)
		{
			this.Output.Write(HttpUtility.HtmlEncode(value));
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000036A1 File Offset: 0x000018A1
		protected internal override TextWriter GetOutputWriter()
		{
			return this.Output;
		}

		// Token: 0x04000022 RID: 34
		private static readonly Action<Action> _safeExecuteStartPageThunk = ApplicationStartPage.GetSafeExecuteStartPageThunk();

		// Token: 0x04000023 RID: 35
		public static readonly string StartPageVirtualPath = "~/_appstart.";

		// Token: 0x04000024 RID: 36
		public static readonly string CacheKeyPrefix = "__AppStartPage__";
	}
}
