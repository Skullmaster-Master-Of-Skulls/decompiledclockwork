using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Hosting;
using System.Web.Mvc.Properties;
using System.Web.WebPages;

namespace System.Web.Mvc
{
	// Token: 0x02000065 RID: 101
	public abstract class VirtualPathProviderViewEngine : IViewEngine
	{
		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x00008BF5 File Offset: 0x00006DF5
		// (set) Token: 0x060002A6 RID: 678 RVA: 0x00008BFD File Offset: 0x00006DFD
		public string[] AreaMasterLocationFormats { get; set; }

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x00008C06 File Offset: 0x00006E06
		// (set) Token: 0x060002A8 RID: 680 RVA: 0x00008C0E File Offset: 0x00006E0E
		public string[] AreaPartialViewLocationFormats { get; set; }

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x00008C17 File Offset: 0x00006E17
		// (set) Token: 0x060002AA RID: 682 RVA: 0x00008C1F File Offset: 0x00006E1F
		public string[] AreaViewLocationFormats { get; set; }

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060002AB RID: 683 RVA: 0x00008C28 File Offset: 0x00006E28
		// (set) Token: 0x060002AC RID: 684 RVA: 0x00008C30 File Offset: 0x00006E30
		public string[] FileExtensions { get; set; }

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060002AD RID: 685 RVA: 0x00008C39 File Offset: 0x00006E39
		// (set) Token: 0x060002AE RID: 686 RVA: 0x00008C41 File Offset: 0x00006E41
		public string[] MasterLocationFormats { get; set; }

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060002AF RID: 687 RVA: 0x00008C4A File Offset: 0x00006E4A
		// (set) Token: 0x060002B0 RID: 688 RVA: 0x00008C52 File Offset: 0x00006E52
		public string[] PartialViewLocationFormats { get; set; }

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x00008C5B File Offset: 0x00006E5B
		// (set) Token: 0x060002B2 RID: 690 RVA: 0x00008C96 File Offset: 0x00006E96
		public IViewLocationCache ViewLocationCache
		{
			get
			{
				if (this._viewLocationCache == null)
				{
					if (HttpContext.Current == null || HttpContext.Current.IsDebuggingEnabled)
					{
						this._viewLocationCache = DefaultViewLocationCache.Null;
					}
					else
					{
						this._viewLocationCache = new DefaultViewLocationCache();
					}
				}
				return this._viewLocationCache;
			}
			set
			{
				if (value == null)
				{
					throw Error.ArgumentNull("value");
				}
				this._viewLocationCache = value;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x00008CAD File Offset: 0x00006EAD
		// (set) Token: 0x060002B4 RID: 692 RVA: 0x00008CB5 File Offset: 0x00006EB5
		public string[] ViewLocationFormats { get; set; }

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x00008CBE File Offset: 0x00006EBE
		// (set) Token: 0x060002B6 RID: 694 RVA: 0x00008CDC File Offset: 0x00006EDC
		protected VirtualPathProvider VirtualPathProvider
		{
			get
			{
				return this._vppFunc();
			}
			set
			{
				if (value == null)
				{
					throw Error.ArgumentNull("value");
				}
				this._vppFunc = (() => value);
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x00008D1B File Offset: 0x00006F1B
		// (set) Token: 0x060002B8 RID: 696 RVA: 0x00008D23 File Offset: 0x00006F23
		internal Func<VirtualPathProvider> VirtualPathProviderFunc
		{
			get
			{
				return this._vppFunc;
			}
			set
			{
				if (value == null)
				{
					throw Error.ArgumentNull("value");
				}
				this._vppFunc = value;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x00008D3A File Offset: 0x00006F3A
		// (set) Token: 0x060002BA RID: 698 RVA: 0x00008D4B File Offset: 0x00006F4B
		protected internal DisplayModeProvider DisplayModeProvider
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

		// Token: 0x060002BB RID: 699 RVA: 0x00008D54 File Offset: 0x00006F54
		internal virtual string CreateCacheKey(string prefix, string name, string controllerName, string areaName)
		{
			return string.Format(CultureInfo.InvariantCulture, ":ViewCacheEntry:{0}:{1}:{2}:{3}:{4}:", new object[]
			{
				base.GetType().AssemblyQualifiedName,
				prefix,
				name,
				controllerName,
				areaName
			});
		}

		// Token: 0x060002BC RID: 700 RVA: 0x00008D97 File Offset: 0x00006F97
		internal static string AppendDisplayModeToCacheKey(string cacheKey, string displayMode)
		{
			return cacheKey + displayMode + ":";
		}

		// Token: 0x060002BD RID: 701
		protected abstract IView CreatePartialView(ControllerContext controllerContext, string partialPath);

		// Token: 0x060002BE RID: 702
		protected abstract IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath);

		// Token: 0x060002BF RID: 703 RVA: 0x00008DA5 File Offset: 0x00006FA5
		protected virtual bool FileExists(ControllerContext controllerContext, string virtualPath)
		{
			return this.VirtualPathProvider.FileExists(virtualPath);
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x00008DB4 File Offset: 0x00006FB4
		public virtual ViewEngineResult FindPartialView(ControllerContext controllerContext, string partialViewName, bool useCache)
		{
			if (controllerContext == null)
			{
				throw new ArgumentNullException("controllerContext");
			}
			if (string.IsNullOrEmpty(partialViewName))
			{
				throw new ArgumentException(MvcResources.Common_NullOrEmpty, "partialViewName");
			}
			string requiredString = controllerContext.RouteData.GetRequiredString("controller");
			string[] searchedLocations;
			string path = this.GetPath(controllerContext, this.PartialViewLocationFormats, this.AreaPartialViewLocationFormats, "PartialViewLocationFormats", partialViewName, requiredString, "Partial", useCache, out searchedLocations);
			if (string.IsNullOrEmpty(path))
			{
				return new ViewEngineResult(searchedLocations);
			}
			return new ViewEngineResult(this.CreatePartialView(controllerContext, path), this);
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x00008E38 File Offset: 0x00007038
		public virtual ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache)
		{
			if (controllerContext == null)
			{
				throw new ArgumentNullException("controllerContext");
			}
			if (string.IsNullOrEmpty(viewName))
			{
				throw new ArgumentException(MvcResources.Common_NullOrEmpty, "viewName");
			}
			string requiredString = controllerContext.RouteData.GetRequiredString("controller");
			string[] first;
			string path = this.GetPath(controllerContext, this.ViewLocationFormats, this.AreaViewLocationFormats, "ViewLocationFormats", viewName, requiredString, "View", useCache, out first);
			string[] second;
			string path2 = this.GetPath(controllerContext, this.MasterLocationFormats, this.AreaMasterLocationFormats, "MasterLocationFormats", masterName, requiredString, "Master", useCache, out second);
			if (string.IsNullOrEmpty(path) || (string.IsNullOrEmpty(path2) && !string.IsNullOrEmpty(masterName)))
			{
				return new ViewEngineResult(first.Union(second));
			}
			return new ViewEngineResult(this.CreateView(controllerContext, path, path2), this);
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x00008EFC File Offset: 0x000070FC
		private string GetPath(ControllerContext controllerContext, string[] locations, string[] areaLocations, string locationsPropertyName, string name, string controllerName, string cacheKeyPrefix, bool useCache, out string[] searchedLocations)
		{
			searchedLocations = VirtualPathProviderViewEngine._emptyLocations;
			if (string.IsNullOrEmpty(name))
			{
				return string.Empty;
			}
			string areaName = AreaHelpers.GetAreaName(controllerContext.RouteData);
			List<VirtualPathProviderViewEngine.ViewLocation> viewLocations = VirtualPathProviderViewEngine.GetViewLocations(locations, (!string.IsNullOrEmpty(areaName)) ? areaLocations : null);
			if (viewLocations.Count == 0)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, MvcResources.Common_PropertyCannotBeNullOrEmpty, new object[]
				{
					locationsPropertyName
				}));
			}
			bool flag = VirtualPathProviderViewEngine.IsSpecificPath(name);
			string cacheKey = this.CreateCacheKey(cacheKeyPrefix, name, flag ? string.Empty : controllerName, areaName);
			if (useCache)
			{
				IEnumerable<IDisplayMode> availableDisplayModesForContext = this.DisplayModeProvider.GetAvailableDisplayModesForContext(controllerContext.HttpContext, controllerContext.DisplayMode);
				foreach (IDisplayMode displayMode in availableDisplayModesForContext)
				{
					string viewLocation = this.ViewLocationCache.GetViewLocation(controllerContext.HttpContext, VirtualPathProviderViewEngine.AppendDisplayModeToCacheKey(cacheKey, displayMode.DisplayModeId));
					if (viewLocation == null)
					{
						return null;
					}
					if (viewLocation.Length > 0)
					{
						if (controllerContext.DisplayMode == null)
						{
							controllerContext.DisplayMode = displayMode;
						}
						return viewLocation;
					}
				}
				return null;
			}
			if (!flag)
			{
				return this.GetPathFromGeneralName(controllerContext, viewLocations, name, controllerName, areaName, cacheKey, ref searchedLocations);
			}
			return this.GetPathFromSpecificName(controllerContext, name, cacheKey, ref searchedLocations);
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00009090 File Offset: 0x00007290
		private string GetPathFromGeneralName(ControllerContext controllerContext, List<VirtualPathProviderViewEngine.ViewLocation> locations, string name, string controllerName, string areaName, string cacheKey, ref string[] searchedLocations)
		{
			string text = string.Empty;
			searchedLocations = new string[locations.Count];
			for (int i = 0; i < locations.Count; i++)
			{
				VirtualPathProviderViewEngine.ViewLocation viewLocation = locations[i];
				string text2 = viewLocation.Format(name, controllerName, areaName);
				DisplayInfo displayInfoForVirtualPath = this.DisplayModeProvider.GetDisplayInfoForVirtualPath(text2, controllerContext.HttpContext, (string path) => this.FileExists(controllerContext, path), controllerContext.DisplayMode);
				if (displayInfoForVirtualPath != null)
				{
					string filePath = displayInfoForVirtualPath.FilePath;
					searchedLocations = VirtualPathProviderViewEngine._emptyLocations;
					text = filePath;
					this.ViewLocationCache.InsertViewLocation(controllerContext.HttpContext, VirtualPathProviderViewEngine.AppendDisplayModeToCacheKey(cacheKey, displayInfoForVirtualPath.DisplayMode.DisplayModeId), text);
					if (controllerContext.DisplayMode == null)
					{
						controllerContext.DisplayMode = displayInfoForVirtualPath.DisplayMode;
					}
					IEnumerable<IDisplayMode> modes = this.DisplayModeProvider.Modes;
					using (IEnumerator<IDisplayMode> enumerator = modes.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							IDisplayMode displayMode = enumerator.Current;
							if (displayMode.DisplayModeId != displayInfoForVirtualPath.DisplayMode.DisplayModeId)
							{
								DisplayInfo displayInfo = displayMode.GetDisplayInfo(controllerContext.HttpContext, text2, (string path) => this.FileExists(controllerContext, path));
								string virtualPath = string.Empty;
								if (displayInfo != null && displayInfo.FilePath != null)
								{
									virtualPath = displayInfo.FilePath;
								}
								this.ViewLocationCache.InsertViewLocation(controllerContext.HttpContext, VirtualPathProviderViewEngine.AppendDisplayModeToCacheKey(cacheKey, displayMode.DisplayModeId), virtualPath);
							}
						}
						break;
					}
				}
				searchedLocations[i] = text2;
			}
			return text;
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000927C File Offset: 0x0000747C
		private string GetPathFromSpecificName(ControllerContext controllerContext, string name, string cacheKey, ref string[] searchedLocations)
		{
			string text = name;
			if (!this.FilePathIsSupported(name) || !this.FileExists(controllerContext, name))
			{
				text = string.Empty;
				searchedLocations = new string[]
				{
					name
				};
			}
			this.ViewLocationCache.InsertViewLocation(controllerContext.HttpContext, cacheKey, text);
			return text;
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x000092C8 File Offset: 0x000074C8
		private bool FilePathIsSupported(string virtualPath)
		{
			if (this.FileExtensions == null)
			{
				return true;
			}
			string value = this.GetExtensionThunk(virtualPath).TrimStart(new char[]
			{
				'.'
			});
			return this.FileExtensions.Contains(value, StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x00009310 File Offset: 0x00007510
		private static List<VirtualPathProviderViewEngine.ViewLocation> GetViewLocations(string[] viewLocationFormats, string[] areaViewLocationFormats)
		{
			List<VirtualPathProviderViewEngine.ViewLocation> list = new List<VirtualPathProviderViewEngine.ViewLocation>();
			if (areaViewLocationFormats != null)
			{
				foreach (string virtualPathFormatString in areaViewLocationFormats)
				{
					list.Add(new VirtualPathProviderViewEngine.AreaAwareViewLocation(virtualPathFormatString));
				}
			}
			if (viewLocationFormats != null)
			{
				foreach (string virtualPathFormatString2 in viewLocationFormats)
				{
					list.Add(new VirtualPathProviderViewEngine.ViewLocation(virtualPathFormatString2));
				}
			}
			return list;
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x00009378 File Offset: 0x00007578
		private static bool IsSpecificPath(string name)
		{
			char c = name[0];
			return c == '~' || c == '/';
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000939C File Offset: 0x0000759C
		public virtual void ReleaseView(ControllerContext controllerContext, IView view)
		{
			IDisposable disposable = view as IDisposable;
			if (disposable != null)
			{
				disposable.Dispose();
			}
		}

		// Token: 0x0400008C RID: 140
		private const string CacheKeyFormat = ":ViewCacheEntry:{0}:{1}:{2}:{3}:{4}:";

		// Token: 0x0400008D RID: 141
		private const string CacheKeyPrefixMaster = "Master";

		// Token: 0x0400008E RID: 142
		private const string CacheKeyPrefixPartial = "Partial";

		// Token: 0x0400008F RID: 143
		private const string CacheKeyPrefixView = "View";

		// Token: 0x04000090 RID: 144
		private static readonly string[] _emptyLocations = new string[0];

		// Token: 0x04000091 RID: 145
		private DisplayModeProvider _displayModeProvider;

		// Token: 0x04000092 RID: 146
		private Func<VirtualPathProvider> _vppFunc = () => HostingEnvironment.VirtualPathProvider;

		// Token: 0x04000093 RID: 147
		internal Func<string, string> GetExtensionThunk = new Func<string, string>(VirtualPathUtility.GetExtension);

		// Token: 0x04000094 RID: 148
		private IViewLocationCache _viewLocationCache;

		// Token: 0x02000066 RID: 102
		private class ViewLocation
		{
			// Token: 0x060002CC RID: 716 RVA: 0x0000940A File Offset: 0x0000760A
			public ViewLocation(string virtualPathFormatString)
			{
				this._virtualPathFormatString = virtualPathFormatString;
			}

			// Token: 0x060002CD RID: 717 RVA: 0x0000941C File Offset: 0x0000761C
			public virtual string Format(string viewName, string controllerName, string areaName)
			{
				return string.Format(CultureInfo.InvariantCulture, this._virtualPathFormatString, new object[]
				{
					viewName,
					controllerName
				});
			}

			// Token: 0x0400009D RID: 157
			protected string _virtualPathFormatString;
		}

		// Token: 0x02000067 RID: 103
		private class AreaAwareViewLocation : VirtualPathProviderViewEngine.ViewLocation
		{
			// Token: 0x060002CE RID: 718 RVA: 0x00009449 File Offset: 0x00007649
			public AreaAwareViewLocation(string virtualPathFormatString) : base(virtualPathFormatString)
			{
			}

			// Token: 0x060002CF RID: 719 RVA: 0x00009454 File Offset: 0x00007654
			public override string Format(string viewName, string controllerName, string areaName)
			{
				return string.Format(CultureInfo.InvariantCulture, this._virtualPathFormatString, new object[]
				{
					viewName,
					controllerName,
					areaName
				});
			}
		}
	}
}
