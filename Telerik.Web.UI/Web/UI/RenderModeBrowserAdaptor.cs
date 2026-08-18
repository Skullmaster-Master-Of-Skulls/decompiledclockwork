using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;
using System.Web;

namespace Telerik.Web.UI
{
	// Token: 0x02000772 RID: 1906
	public class RenderModeBrowserAdaptor
	{
		// Token: 0x170015E0 RID: 5600
		// (get) Token: 0x06004338 RID: 17208 RVA: 0x000D27E4 File Offset: 0x000D09E4
		// (set) Token: 0x06004339 RID: 17209 RVA: 0x000D27EC File Offset: 0x000D09EC
		protected HttpBrowserCapabilities Browser { get; set; }

		// Token: 0x170015E1 RID: 5601
		// (get) Token: 0x0600433A RID: 17210 RVA: 0x000D27F5 File Offset: 0x000D09F5
		// (set) Token: 0x0600433B RID: 17211 RVA: 0x000D27FD File Offset: 0x000D09FD
		protected HttpContext Context { get; set; }

		// Token: 0x170015E2 RID: 5602
		// (get) Token: 0x0600433C RID: 17212 RVA: 0x000D2806 File Offset: 0x000D0A06
		private static bool DetectionAvailable
		{
			get
			{
				return RenderModeBrowserAdaptor.detectionAssemblyState == DetectionAssemblyReferenceState.Found && RenderModeBrowserAdaptor.deviceDetectionAdapter != null;
			}
		}

		// Token: 0x0600433D RID: 17213 RVA: 0x000D281D File Offset: 0x000D0A1D
		internal RenderModeBrowserAdaptor()
		{
		}

		// Token: 0x0600433E RID: 17214 RVA: 0x000D2825 File Offset: 0x000D0A25
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		private RenderModeBrowserAdaptor(RenderModeBrowserAdaptorConfiguration config)
		{
			this.Context = config.Context;
			this.Browser = config.Context.Request.Browser;
			this.IECompatibility = config.IsEdge;
		}

		// Token: 0x170015E3 RID: 5603
		// (get) Token: 0x0600433F RID: 17215 RVA: 0x000D285C File Offset: 0x000D0A5C
		public static RenderModeBrowserAdaptor Instance
		{
			get
			{
				Type typeFromHandle = typeof(RenderModeBrowserAdaptor);
				HttpContext httpContext = HttpContext.Current;
				if (httpContext == null)
				{
					throw new NotSupportedException();
				}
				RenderModeBrowserAdaptor result;
				lock (RenderModeBrowserAdaptor.locker)
				{
					if (httpContext.Items[typeFromHandle] == null)
					{
						httpContext.Items[typeFromHandle] = new RenderModeBrowserAdaptor(new RenderModeBrowserAdaptorConfiguration
						{
							Context = httpContext,
							IsEdge = new X_UA_CompatbileReader().IsEdge(httpContext)
						});
						RenderModeBrowserAdaptor.SearchForDetectionAssemblyReference();
					}
					result = (httpContext.Items[typeFromHandle] as RenderModeBrowserAdaptor);
				}
				return result;
			}
		}

		// Token: 0x170015E4 RID: 5604
		// (get) Token: 0x06004340 RID: 17216 RVA: 0x000D290C File Offset: 0x000D0B0C
		// (set) Token: 0x06004341 RID: 17217 RVA: 0x000D2914 File Offset: 0x000D0B14
		public virtual bool IECompatibility { get; set; }

		// Token: 0x170015E5 RID: 5605
		// (get) Token: 0x06004342 RID: 17218 RVA: 0x000D291D File Offset: 0x000D0B1D
		// (set) Token: 0x06004343 RID: 17219 RVA: 0x000D292A File Offset: 0x000D0B2A
		public virtual IDictionary ContextItems
		{
			get
			{
				return this.Context.Items;
			}
			internal set
			{
			}
		}

		// Token: 0x170015E6 RID: 5606
		// (get) Token: 0x06004344 RID: 17220 RVA: 0x000D292C File Offset: 0x000D0B2C
		// (set) Token: 0x06004345 RID: 17221 RVA: 0x000D293E File Offset: 0x000D0B3E
		public virtual string UserAgent
		{
			get
			{
				return this.Context.Request.UserAgent;
			}
			internal set
			{
			}
		}

		// Token: 0x170015E7 RID: 5607
		// (get) Token: 0x06004346 RID: 17222 RVA: 0x000D2940 File Offset: 0x000D0B40
		public virtual bool IsMobileDevice
		{
			get
			{
				if (this.ContextItems["Telerik.Web.Detection.IsMobileDevice"] == null)
				{
					this.ContextItems["Telerik.Web.Detection.IsMobileDevice"] = this.DetermineIsMobile();
				}
				return (bool)this.ContextItems["Telerik.Web.Detection.IsMobileDevice"];
			}
		}

		// Token: 0x170015E8 RID: 5608
		// (get) Token: 0x06004347 RID: 17223 RVA: 0x000D298F File Offset: 0x000D0B8F
		public virtual bool IsModernBrowser
		{
			get
			{
				return !this.IsBrowser("IE") || this.IsModernIE();
			}
		}

		// Token: 0x170015E9 RID: 5609
		// (get) Token: 0x06004348 RID: 17224 RVA: 0x000D29A8 File Offset: 0x000D0BA8
		public virtual double Version
		{
			get
			{
				double result = 0.0;
				double.TryParse(this.Browser.Version, NumberStyles.Any, CultureInfo.InvariantCulture, out result);
				return result;
			}
		}

		// Token: 0x06004349 RID: 17225 RVA: 0x000D29DD File Offset: 0x000D0BDD
		public virtual bool IsModernIE()
		{
			return this.Version >= 8.0 || this.IECompatibility;
		}

		// Token: 0x0600434A RID: 17226 RVA: 0x000D29F8 File Offset: 0x000D0BF8
		public virtual bool IsBrowser(string browserString)
		{
			return this.Browser.IsBrowser(browserString);
		}

		// Token: 0x0600434B RID: 17227 RVA: 0x000D2A08 File Offset: 0x000D0C08
		private bool DetermineIsMobile()
		{
			if (RenderModeBrowserAdaptor.DetectionAvailable)
			{
				return RenderModeBrowserAdaptor.DetectMobile(this);
			}
			if (!string.IsNullOrEmpty(this.UserAgent))
			{
				string text = this.UserAgent.ToLower(CultureInfo.InvariantCulture);
				if (text.Contains("iphone") || text.Contains("android") || text.Contains("ipad"))
				{
					return true;
				}
			}
			return this.Browser.IsMobileDevice;
		}

		// Token: 0x0600434C RID: 17228 RVA: 0x000D2A78 File Offset: 0x000D0C78
		private static void IntializeDeviceDetectionAdapter(Assembly assembly)
		{
			Type type = assembly.GetType("Telerik.Web.Device.Detection.DeviceDetectionAdapter");
			if (type != null)
			{
				RenderModeBrowserAdaptor.deviceDetectionAdapter = (Activator.CreateInstance(type, null) as IDeviceDetectionAdapter);
			}
		}

		// Token: 0x0600434D RID: 17229 RVA: 0x000D2AAC File Offset: 0x000D0CAC
		private static void SearchForDetectionAssemblyReference()
		{
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
			RenderModeBrowserAdaptor.detectionAssemblyState = DetectionAssemblyReferenceState.NotFound;
			foreach (Assembly assembly in assemblies)
			{
				if (assembly.FullName.Contains("Telerik.Web.Device.Detection"))
				{
					RenderModeBrowserAdaptor.detectionAssemblyState = DetectionAssemblyReferenceState.Found;
					RenderModeBrowserAdaptor.IntializeDeviceDetectionAdapter(assembly);
					return;
				}
			}
		}

		// Token: 0x0600434E RID: 17230 RVA: 0x000D2B00 File Offset: 0x000D0D00
		private static bool DetectMobile(RenderModeBrowserAdaptor browser)
		{
			DeviceScreenSizeWrapper screenSize = RenderModeBrowserAdaptor.deviceDetectionAdapter.GetScreenSize(browser.UserAgent);
			return screenSize == DeviceScreenSizeWrapper.Small || screenSize == DeviceScreenSizeWrapper.Medium;
		}

		// Token: 0x040011CC RID: 4556
		private const string DetectionAssemblyName = "Telerik.Web.Device.Detection";

		// Token: 0x040011CD RID: 4557
		private const string DeviceDetectionAdaptorFullName = "Telerik.Web.Device.Detection.DeviceDetectionAdapter";

		// Token: 0x040011CE RID: 4558
		private const string IsMobileDeviceContextKey = "Telerik.Web.Detection.IsMobileDevice";

		// Token: 0x040011CF RID: 4559
		private const string IPhone = "iphone";

		// Token: 0x040011D0 RID: 4560
		private const string IPad = "ipad";

		// Token: 0x040011D1 RID: 4561
		private const string Android = "android";

		// Token: 0x040011D2 RID: 4562
		private static DetectionAssemblyReferenceState detectionAssemblyState = DetectionAssemblyReferenceState.NotSearched;

		// Token: 0x040011D3 RID: 4563
		private static IDeviceDetectionAdapter deviceDetectionAdapter;

		// Token: 0x040011D4 RID: 4564
		private static readonly object locker = new object();
	}
}
