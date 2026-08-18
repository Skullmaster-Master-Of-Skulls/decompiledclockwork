using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Caching;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Web.Mvc.Properties;
using System.Web.UI;

namespace System.Web.Mvc
{
	// Token: 0x020001CF RID: 463
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
	public class OutputCacheAttribute : ActionFilterAttribute, IExceptionFilter
	{
		// Token: 0x06000DA1 RID: 3489 RVA: 0x00023E48 File Offset: 0x00022048
		public OutputCacheAttribute()
		{
			this._splitVaryByParamThunk = (() => OutputCacheAttribute.GetTokenizedVaryByParam(this.VaryByParam));
		}

		// Token: 0x06000DA2 RID: 3490 RVA: 0x00023EC0 File Offset: 0x000220C0
		internal OutputCacheAttribute(ObjectCache childActionCache) : this()
		{
			this._childActionCacheThunk = (() => childActionCache);
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000DA3 RID: 3491 RVA: 0x00023EF9 File Offset: 0x000220F9
		// (set) Token: 0x06000DA4 RID: 3492 RVA: 0x00023F0F File Offset: 0x0002210F
		public string CacheProfile
		{
			get
			{
				return this._cacheSettings.CacheProfile ?? string.Empty;
			}
			set
			{
				this._cacheSettings.CacheProfile = value;
			}
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06000DA5 RID: 3493 RVA: 0x00023F1D File Offset: 0x0002211D
		internal OutputCacheParameters CacheSettings
		{
			get
			{
				return this._cacheSettings;
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000DA6 RID: 3494 RVA: 0x00023F25 File Offset: 0x00022125
		// (set) Token: 0x06000DA7 RID: 3495 RVA: 0x00023F35 File Offset: 0x00022135
		public static ObjectCache ChildActionCache
		{
			get
			{
				return OutputCacheAttribute._childActionCache ?? MemoryCache.Default;
			}
			set
			{
				OutputCacheAttribute._childActionCache = value;
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000DA8 RID: 3496 RVA: 0x00023F3D File Offset: 0x0002213D
		private ObjectCache ChildActionCacheInternal
		{
			get
			{
				return this._childActionCacheThunk();
			}
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000DA9 RID: 3497 RVA: 0x00023F4A File Offset: 0x0002214A
		// (set) Token: 0x06000DAA RID: 3498 RVA: 0x00023F57 File Offset: 0x00022157
		public int Duration
		{
			get
			{
				return this._cacheSettings.Duration;
			}
			set
			{
				this._cacheSettings.Duration = value;
			}
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000DAB RID: 3499 RVA: 0x00023F65 File Offset: 0x00022165
		// (set) Token: 0x06000DAC RID: 3500 RVA: 0x00023F72 File Offset: 0x00022172
		public OutputCacheLocation Location
		{
			get
			{
				return this._cacheSettings.Location;
			}
			set
			{
				this._cacheSettings.Location = value;
				this._locationWasSet = true;
			}
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000DAD RID: 3501 RVA: 0x00023F87 File Offset: 0x00022187
		// (set) Token: 0x06000DAE RID: 3502 RVA: 0x00023F94 File Offset: 0x00022194
		public bool NoStore
		{
			get
			{
				return this._cacheSettings.NoStore;
			}
			set
			{
				this._cacheSettings.NoStore = value;
				this._noStoreWasSet = true;
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000DAF RID: 3503 RVA: 0x00023FA9 File Offset: 0x000221A9
		// (set) Token: 0x06000DB0 RID: 3504 RVA: 0x00023FBF File Offset: 0x000221BF
		public string SqlDependency
		{
			get
			{
				return this._cacheSettings.SqlDependency ?? string.Empty;
			}
			set
			{
				this._cacheSettings.SqlDependency = value;
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000DB1 RID: 3505 RVA: 0x00023FCD File Offset: 0x000221CD
		// (set) Token: 0x06000DB2 RID: 3506 RVA: 0x00023FE3 File Offset: 0x000221E3
		public string VaryByContentEncoding
		{
			get
			{
				return this._cacheSettings.VaryByContentEncoding ?? string.Empty;
			}
			set
			{
				this._cacheSettings.VaryByContentEncoding = value;
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000DB3 RID: 3507 RVA: 0x00023FF1 File Offset: 0x000221F1
		// (set) Token: 0x06000DB4 RID: 3508 RVA: 0x00024007 File Offset: 0x00022207
		public string VaryByCustom
		{
			get
			{
				return this._cacheSettings.VaryByCustom ?? string.Empty;
			}
			set
			{
				this._cacheSettings.VaryByCustom = value;
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000DB5 RID: 3509 RVA: 0x00024015 File Offset: 0x00022215
		// (set) Token: 0x06000DB6 RID: 3510 RVA: 0x0002402B File Offset: 0x0002222B
		public string VaryByHeader
		{
			get
			{
				return this._cacheSettings.VaryByHeader ?? string.Empty;
			}
			set
			{
				this._cacheSettings.VaryByHeader = value;
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000DB7 RID: 3511 RVA: 0x00024039 File Offset: 0x00022239
		// (set) Token: 0x06000DB8 RID: 3512 RVA: 0x0002404F File Offset: 0x0002224F
		public string VaryByParam
		{
			get
			{
				return this._cacheSettings.VaryByParam ?? string.Empty;
			}
			set
			{
				this._tokenizedVaryByParams = null;
				this._cacheSettings.VaryByParam = value;
			}
		}

		// Token: 0x06000DB9 RID: 3513 RVA: 0x00024064 File Offset: 0x00022264
		private static void ClearChildActionFilterFinishCallback(ControllerContext controllerContext)
		{
			controllerContext.HttpContext.Items.Remove(OutputCacheAttribute._childActionFilterFinishCallbackKey);
		}

		// Token: 0x06000DBA RID: 3514 RVA: 0x0002407C File Offset: 0x0002227C
		private static void CompleteChildAction(ControllerContext filterContext, bool wasException)
		{
			Action<bool> childActionFilterFinishCallback = OutputCacheAttribute.GetChildActionFilterFinishCallback(filterContext);
			if (childActionFilterFinishCallback != null)
			{
				OutputCacheAttribute.ClearChildActionFilterFinishCallback(filterContext);
				childActionFilterFinishCallback(wasException);
			}
		}

		// Token: 0x06000DBB RID: 3515 RVA: 0x000240A0 File Offset: 0x000222A0
		private static Action<bool> GetChildActionFilterFinishCallback(ControllerContext controllerContext)
		{
			return controllerContext.HttpContext.Items[OutputCacheAttribute._childActionFilterFinishCallbackKey] as Action<bool>;
		}

		// Token: 0x06000DBC RID: 3516 RVA: 0x000240BC File Offset: 0x000222BC
		internal string GetChildActionUniqueId(ActionExecutingContext filterContext)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("_MvcChildActionCache_");
			stringBuilder.Append(filterContext.ActionDescriptor.UniqueId);
			DescriptorUtil.AppendUniqueId(stringBuilder, this.VaryByCustom);
			if (!string.IsNullOrEmpty(this.VaryByCustom))
			{
				string varyByCustomString = filterContext.HttpContext.ApplicationInstance.GetVaryByCustomString(HttpContext.Current, this.VaryByCustom);
				stringBuilder.Append(varyByCustomString);
			}
			this.BuildUniqueIdFromActionParameters(stringBuilder, filterContext);
			string result;
			using (SHA256Cng sha256Cng = new SHA256Cng())
			{
				result = Convert.ToBase64String(sha256Cng.ComputeHash(Encoding.UTF8.GetBytes(stringBuilder.ToString())));
			}
			return result;
		}

		// Token: 0x06000DBD RID: 3517 RVA: 0x00024180 File Offset: 0x00022380
		internal void BuildUniqueIdFromActionParameters(StringBuilder builder, ActionExecutingContext filterContext)
		{
			if (string.Equals(this.VaryByParam, "none", StringComparison.OrdinalIgnoreCase))
			{
				return;
			}
			if (string.Equals(this.VaryByParam, "*", StringComparison.Ordinal))
			{
				IEnumerable<KeyValuePair<string, object>> enumerable = filterContext.ActionParameters.OrderBy((KeyValuePair<string, object> k) => k.Key, StringComparer.OrdinalIgnoreCase);
				foreach (KeyValuePair<string, object> keyValuePair in enumerable)
				{
					DescriptorUtil.AppendUniqueId(builder, keyValuePair.Key.ToUpperInvariant());
					DescriptorUtil.AppendUniqueId(builder, keyValuePair.Value);
				}
				return;
			}
			LazyInitializer.EnsureInitialized<string[]>(ref this._tokenizedVaryByParams, this._splitVaryByParamThunk);
			Dictionary<string, object> caseInsensitiveActionParametersDictionary = OutputCacheAttribute.GetCaseInsensitiveActionParametersDictionary(filterContext.ActionParameters);
			for (int i = 0; i < this._tokenizedVaryByParams.Length; i++)
			{
				string text = this._tokenizedVaryByParams[i];
				DescriptorUtil.AppendUniqueId(builder, text);
				object part;
				caseInsensitiveActionParametersDictionary.TryGetValue(text, out part);
				DescriptorUtil.AppendUniqueId(builder, part);
			}
		}

		// Token: 0x06000DBE RID: 3518 RVA: 0x00024294 File Offset: 0x00022494
		public static bool IsChildActionCacheActive(ControllerContext controllerContext)
		{
			return OutputCacheAttribute.GetChildActionFilterFinishCallback(controllerContext) != null;
		}

		// Token: 0x06000DBF RID: 3519 RVA: 0x000242A2 File Offset: 0x000224A2
		public override void OnActionExecuted(ActionExecutedContext filterContext)
		{
			if (filterContext == null)
			{
				throw new ArgumentNullException("filterContext");
			}
			if (filterContext.IsChildAction && filterContext.Exception != null)
			{
				OutputCacheAttribute.CompleteChildAction(filterContext, true);
			}
		}

		// Token: 0x06000DC0 RID: 3520 RVA: 0x00024370 File Offset: 0x00022570
		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			if (filterContext == null)
			{
				throw new ArgumentNullException("filterContext");
			}
			if (filterContext.IsChildAction)
			{
				if (this.IsServerSideCacheDisabled())
				{
					return;
				}
				this.ValidateChildActionConfiguration();
				if (OutputCacheAttribute.GetChildActionFilterFinishCallback(filterContext) != null)
				{
					throw new InvalidOperationException(MvcResources.OutputCacheAttribute_CannotNestChildCache);
				}
				string uniqueId = this.GetChildActionUniqueId(filterContext);
				string text = this.ChildActionCacheInternal.Get(uniqueId, null) as string;
				if (text != null)
				{
					filterContext.Result = new ContentResult
					{
						Content = text
					};
					return;
				}
				StringWriter cachingWriter = new StringWriter(CultureInfo.InvariantCulture);
				TextWriter originalWriter = filterContext.HttpContext.Response.Output;
				filterContext.HttpContext.Response.Output = cachingWriter;
				OutputCacheAttribute.SetChildActionFilterFinishCallback(filterContext, delegate(bool wasException)
				{
					filterContext.HttpContext.Response.Output = originalWriter;
					string text2 = cachingWriter.ToString();
					filterContext.HttpContext.Response.Write(text2);
					if (!wasException)
					{
						this.ChildActionCacheInternal.Add(uniqueId, text2, DateTimeOffset.UtcNow.AddSeconds((double)this.Duration), null);
					}
				});
			}
		}

		// Token: 0x06000DC1 RID: 3521 RVA: 0x0002448C File Offset: 0x0002268C
		public void OnException(ExceptionContext filterContext)
		{
			if (filterContext == null)
			{
				throw new ArgumentNullException("filterContext");
			}
			if (filterContext.IsChildAction)
			{
				OutputCacheAttribute.CompleteChildAction(filterContext, true);
			}
		}

		// Token: 0x06000DC2 RID: 3522 RVA: 0x000244AC File Offset: 0x000226AC
		public override void OnResultExecuting(ResultExecutingContext filterContext)
		{
			if (filterContext == null)
			{
				throw new ArgumentNullException("filterContext");
			}
			if (!filterContext.IsChildAction)
			{
				using (OutputCacheAttribute.OutputCachedPage outputCachedPage = new OutputCacheAttribute.OutputCachedPage(this._cacheSettings))
				{
					outputCachedPage.ProcessRequest(HttpContext.Current);
				}
			}
		}

		// Token: 0x06000DC3 RID: 3523 RVA: 0x00024504 File Offset: 0x00022704
		public override void OnResultExecuted(ResultExecutedContext filterContext)
		{
			if (filterContext == null)
			{
				throw new ArgumentNullException("filterContext");
			}
			if (filterContext.IsChildAction)
			{
				OutputCacheAttribute.CompleteChildAction(filterContext, filterContext.Exception != null);
			}
		}

		// Token: 0x06000DC4 RID: 3524 RVA: 0x0002452E File Offset: 0x0002272E
		private static void SetChildActionFilterFinishCallback(ControllerContext controllerContext, Action<bool> callback)
		{
			controllerContext.HttpContext.Items[OutputCacheAttribute._childActionFilterFinishCallbackKey] = callback;
		}

		// Token: 0x06000DC5 RID: 3525 RVA: 0x00024548 File Offset: 0x00022748
		private void ValidateChildActionConfiguration()
		{
			if (!string.IsNullOrWhiteSpace(this.CacheProfile) || !string.IsNullOrWhiteSpace(this.SqlDependency) || !string.IsNullOrWhiteSpace(this.VaryByContentEncoding) || !string.IsNullOrWhiteSpace(this.VaryByHeader) || this._locationWasSet || this._noStoreWasSet)
			{
				throw new InvalidOperationException(MvcResources.OutputCacheAttribute_ChildAction_UnsupportedSetting);
			}
			if (this.Duration <= 0)
			{
				throw new InvalidOperationException(MvcResources.OutputCacheAttribute_InvalidDuration);
			}
			if (string.IsNullOrWhiteSpace(this.VaryByParam))
			{
				throw new InvalidOperationException(MvcResources.OutputCacheAttribute_InvalidVaryByParam);
			}
		}

		// Token: 0x06000DC6 RID: 3526 RVA: 0x000245D0 File Offset: 0x000227D0
		private bool IsServerSideCacheDisabled()
		{
			switch (this.Location)
			{
			case OutputCacheLocation.Client:
			case OutputCacheLocation.Downstream:
			case OutputCacheLocation.None:
				return true;
			}
			return false;
		}

		// Token: 0x06000DC7 RID: 3527 RVA: 0x00024748 File Offset: 0x00022948
		private static string[] GetTokenizedVaryByParam(string varyByParam)
		{
			IEnumerable<string> source = from part in varyByParam.Split(OutputCacheAttribute._splitParameter)
			let trimmed = part.Trim()
			where !string.IsNullOrEmpty(trimmed)
			select trimmed.ToUpperInvariant();
			return source.ToArray<string>();
		}

		// Token: 0x06000DC8 RID: 3528 RVA: 0x000247D0 File Offset: 0x000229D0
		private static Dictionary<string, object> GetCaseInsensitiveActionParametersDictionary(IDictionary<string, object> actionParameters)
		{
			Dictionary<string, object> dictionary = actionParameters as Dictionary<string, object>;
			if (dictionary != null && dictionary.Comparer == StringComparer.OrdinalIgnoreCase)
			{
				return dictionary;
			}
			return new Dictionary<string, object>(actionParameters, StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x04000387 RID: 903
		private const string CacheKeyPrefix = "_MvcChildActionCache_";

		// Token: 0x04000388 RID: 904
		private static readonly char[] _splitParameter = new char[]
		{
			';'
		};

		// Token: 0x04000389 RID: 905
		private static ObjectCache _childActionCache;

		// Token: 0x0400038A RID: 906
		private static object _childActionFilterFinishCallbackKey = new object();

		// Token: 0x0400038B RID: 907
		private readonly Func<string[]> _splitVaryByParamThunk;

		// Token: 0x0400038C RID: 908
		private OutputCacheParameters _cacheSettings = new OutputCacheParameters
		{
			VaryByParam = "*"
		};

		// Token: 0x0400038D RID: 909
		private Func<ObjectCache> _childActionCacheThunk = () => OutputCacheAttribute.ChildActionCache;

		// Token: 0x0400038E RID: 910
		private bool _locationWasSet;

		// Token: 0x0400038F RID: 911
		private bool _noStoreWasSet;

		// Token: 0x04000390 RID: 912
		private string[] _tokenizedVaryByParams;

		// Token: 0x020001D0 RID: 464
		private sealed class OutputCachedPage : Page
		{
			// Token: 0x06000DD0 RID: 3536 RVA: 0x00024830 File Offset: 0x00022A30
			public OutputCachedPage(OutputCacheParameters cacheSettings)
			{
				this.ID = Guid.NewGuid().ToString();
				this._cacheSettings = cacheSettings;
			}

			// Token: 0x06000DD1 RID: 3537 RVA: 0x00024863 File Offset: 0x00022A63
			protected override void FrameworkInitialize()
			{
				base.FrameworkInitialize();
				this.InitOutputCache(this._cacheSettings);
			}

			// Token: 0x04000396 RID: 918
			private OutputCacheParameters _cacheSettings;
		}
	}
}
