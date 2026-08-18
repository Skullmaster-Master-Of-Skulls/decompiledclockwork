using System;
using System.Security.Permissions;
using System.Security.Principal;
using System.Web.Caching;
using System.Web.Configuration;
using System.Web.Management;
using System.Web.Util;

namespace System.Web.Security
{
	// Token: 0x020005DA RID: 1498
	public sealed class FileAuthorizationModule : IHttpModule
	{
		// Token: 0x06004BAC RID: 19372 RVA: 0x000030B5 File Offset: 0x000012B5
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		public FileAuthorizationModule()
		{
		}

		// Token: 0x06004BAD RID: 19373 RVA: 0x00101A5C File Offset: 0x000FFC5C
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		public static bool CheckFileAccessForUser(string virtualPath, IntPtr token, string verb)
		{
			if (virtualPath == null)
			{
				throw new ArgumentNullException("virtualPath");
			}
			if (token == IntPtr.Zero)
			{
				throw new ArgumentNullException("token");
			}
			if (verb == null)
			{
				throw new ArgumentNullException("verb");
			}
			VirtualPath virtualPath2 = VirtualPath.Create(virtualPath);
			if (!virtualPath2.IsWithinAppRoot)
			{
				throw new ArgumentException(SR.GetString("Virtual_path_outside_application_not_supported"), "virtualPath");
			}
			if (!FileAuthorizationModule.s_EnabledDetermined)
			{
				if (HttpRuntime.UseIntegratedPipeline)
				{
					FileAuthorizationModule.s_Enabled = true;
				}
				else
				{
					HttpModulesSection httpModules = RuntimeConfig.GetConfig().HttpModules;
					int count = httpModules.Modules.Count;
					for (int i = 0; i < count; i++)
					{
						HttpModuleAction httpModuleAction = httpModules.Modules[i];
						if (Type.GetType(httpModuleAction.Type, false) == typeof(FileAuthorizationModule))
						{
							FileAuthorizationModule.s_Enabled = true;
							break;
						}
					}
				}
				FileAuthorizationModule.s_EnabledDetermined = true;
			}
			if (!FileAuthorizationModule.s_Enabled)
			{
				return true;
			}
			bool flag;
			FileSecurityDescriptorWrapper fileSecurityDescriptorWrapper = FileAuthorizationModule.GetFileSecurityDescriptorWrapper(virtualPath2.MapPath(), out flag);
			int iAccess = 3;
			if (verb == "GET" || verb == "POST" || verb == "HEAD" || verb == "OPTIONS")
			{
				iAccess = 1;
			}
			bool result = fileSecurityDescriptorWrapper.IsAccessAllowed(token, iAccess);
			if (flag)
			{
				fileSecurityDescriptorWrapper.FreeSecurityDescriptor();
			}
			return result;
		}

		// Token: 0x06004BAE RID: 19374 RVA: 0x00101BA2 File Offset: 0x000FFDA2
		public void Init(HttpApplication app)
		{
			app.AuthorizeRequest += this.OnEnter;
		}

		// Token: 0x06004BAF RID: 19375 RVA: 0x00006164 File Offset: 0x00004364
		public void Dispose()
		{
		}

		// Token: 0x06004BB0 RID: 19376 RVA: 0x00101BB8 File Offset: 0x000FFDB8
		private void OnEnter(object source, EventArgs eventArgs)
		{
			if (HttpRuntime.IsOnUNCShareInternal)
			{
				return;
			}
			HttpApplication httpApplication = (HttpApplication)source;
			HttpContext context = httpApplication.Context;
			if (!FileAuthorizationModule.IsUserAllowedToFile(context, null))
			{
				context.Response.SetStatusCode(401, 3);
				this.WriteErrorMessage(context);
				httpApplication.CompleteRequest();
			}
		}

		// Token: 0x06004BB1 RID: 19377 RVA: 0x00101C02 File Offset: 0x000FFE02
		internal static bool IsWindowsIdentity(HttpContext context)
		{
			return context.User != null && context.User.Identity != null && context.User.Identity is WindowsIdentity;
		}

		// Token: 0x06004BB2 RID: 19378 RVA: 0x00101C30 File Offset: 0x000FFE30
		private static bool IsUserAllowedToFile(HttpContext context, string fileName)
		{
			if (!FileAuthorizationModule.IsWindowsIdentity(context))
			{
				return true;
			}
			if (fileName == null)
			{
				fileName = context.Request.PhysicalPathInternal;
			}
			bool flag = context.User == null || !context.User.Identity.IsAuthenticated;
			CachedPathData cachedPathData = null;
			int num = 3;
			HttpVerb httpVerb = context.Request.HttpVerb;
			if (httpVerb == HttpVerb.GET || httpVerb == HttpVerb.POST || httpVerb == HttpVerb.HEAD || context.Request.HttpMethod == "OPTIONS")
			{
				num = 1;
				if (!CachedPathData.DoNotCacheUrlMetadata)
				{
					cachedPathData = context.GetConfigurationPathData();
					if (!StringUtil.EqualsIgnoreCase(fileName, cachedPathData.PhysicalPath))
					{
						cachedPathData = null;
					}
					else
					{
						if (cachedPathData.AnonymousAccessAllowed)
						{
							return true;
						}
						if (cachedPathData.AnonymousAccessChecked && flag)
						{
							return cachedPathData.AnonymousAccessAllowed;
						}
					}
				}
			}
			bool flag2;
			FileSecurityDescriptorWrapper fileSecurityDescriptorWrapper = FileAuthorizationModule.GetFileSecurityDescriptorWrapper(fileName, out flag2);
			bool flag3;
			if (num == 1)
			{
				if (fileSecurityDescriptorWrapper._AnonymousAccessChecked && flag)
				{
					flag3 = fileSecurityDescriptorWrapper._AnonymousAccess;
				}
				else
				{
					flag3 = fileSecurityDescriptorWrapper.IsAccessAllowed(context.WorkerRequest.GetUserToken(), num);
				}
				if (!fileSecurityDescriptorWrapper._AnonymousAccessChecked && flag)
				{
					fileSecurityDescriptorWrapper._AnonymousAccess = flag3;
					fileSecurityDescriptorWrapper._AnonymousAccessChecked = true;
				}
				if (cachedPathData != null && cachedPathData.Exists && fileSecurityDescriptorWrapper._AnonymousAccessChecked)
				{
					cachedPathData.AnonymousAccessAllowed = fileSecurityDescriptorWrapper._AnonymousAccess;
					cachedPathData.AnonymousAccessChecked = true;
				}
			}
			else
			{
				flag3 = fileSecurityDescriptorWrapper.IsAccessAllowed(context.WorkerRequest.GetUserToken(), num);
			}
			if (flag2)
			{
				fileSecurityDescriptorWrapper.FreeSecurityDescriptor();
			}
			if (flag3)
			{
				WebBaseEvent.RaiseSystemEvent(null, 4004);
			}
			else if (!flag)
			{
				WebBaseEvent.RaiseSystemEvent(null, 4008);
			}
			return flag3;
		}

		// Token: 0x06004BB3 RID: 19379 RVA: 0x00101DAC File Offset: 0x000FFFAC
		private static FileSecurityDescriptorWrapper GetFileSecurityDescriptorWrapper(string fileName, out bool freeDescriptor)
		{
			if (CachedPathData.DoNotCacheUrlMetadata)
			{
				freeDescriptor = true;
				return new FileSecurityDescriptorWrapper(fileName);
			}
			freeDescriptor = false;
			string key = "h" + fileName;
			FileSecurityDescriptorWrapper fileSecurityDescriptorWrapper = HttpRuntime.Cache.InternalCache.Get(key) as FileSecurityDescriptorWrapper;
			if (fileSecurityDescriptorWrapper == null)
			{
				fileSecurityDescriptorWrapper = new FileSecurityDescriptorWrapper(fileName);
				string cacheDependencyPath = fileSecurityDescriptorWrapper.GetCacheDependencyPath();
				if (cacheDependencyPath != null)
				{
					try
					{
						CacheDependency dependencies = new CacheDependency(0, cacheDependencyPath);
						TimeSpan urlMetadataSlidingExpiration = CachedPathData.UrlMetadataSlidingExpiration;
						HttpRuntime.Cache.InternalCache.Insert(key, fileSecurityDescriptorWrapper, new CacheInsertOptions
						{
							Dependencies = dependencies,
							SlidingExpiration = urlMetadataSlidingExpiration,
							OnRemovedCallback = new CacheItemRemovedCallback(fileSecurityDescriptorWrapper.OnCacheItemRemoved)
						});
					}
					catch (Exception ex)
					{
						freeDescriptor = true;
					}
				}
			}
			return fileSecurityDescriptorWrapper;
		}

		// Token: 0x06004BB4 RID: 19380 RVA: 0x00101E64 File Offset: 0x00100064
		private void WriteErrorMessage(HttpContext context)
		{
			if (!context.IsCustomErrorEnabled)
			{
				context.Response.Write(new FileAccessFailedErrorFormatter(context.Request.PhysicalPathInternal).GetErrorMessage(context, false));
			}
			else
			{
				context.Response.Write(new FileAccessFailedErrorFormatter(null).GetErrorMessage(context, true));
			}
			context.Response.GenerateResponseHeadersForHandler();
		}

		// Token: 0x06004BB5 RID: 19381 RVA: 0x00101EC0 File Offset: 0x001000C0
		internal static bool RequestRequiresAuthorization(HttpContext context)
		{
			if (!FileAuthorizationModule.IsWindowsIdentity(context))
			{
				return false;
			}
			string key = "h" + context.Request.PhysicalPathInternal;
			object obj = HttpRuntime.Cache.InternalCache.Get(key);
			if (obj == null || !(obj is FileSecurityDescriptorWrapper))
			{
				return true;
			}
			FileSecurityDescriptorWrapper fileSecurityDescriptorWrapper = (FileSecurityDescriptorWrapper)obj;
			return !fileSecurityDescriptorWrapper._AnonymousAccessChecked || !fileSecurityDescriptorWrapper._AnonymousAccess;
		}

		// Token: 0x06004BB6 RID: 19382 RVA: 0x00101F25 File Offset: 0x00100125
		internal static bool IsUserAllowedToPath(HttpContext context, VirtualPath virtualPath)
		{
			return FileAuthorizationModule.IsUserAllowedToFile(context, virtualPath.MapPath());
		}

		// Token: 0x040028C7 RID: 10439
		private static bool s_EnabledDetermined;

		// Token: 0x040028C8 RID: 10440
		private static bool s_Enabled;
	}
}
