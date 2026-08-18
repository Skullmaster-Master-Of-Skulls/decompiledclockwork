using System;
using System.Collections.Generic;
using System.IO;

namespace System.Web.Optimization
{
	// Token: 0x02000027 RID: 39
	public static class Optimizer
	{
		// Token: 0x0600013E RID: 318 RVA: 0x00004F48 File Offset: 0x00003148
		public static BundleResponse BuildBundle(string bundlePath, OptimizationSettings settings)
		{
			if (settings == null)
			{
				throw new ArgumentNullException("settings");
			}
			if (string.IsNullOrEmpty(settings.ApplicationPath))
			{
				throw ExceptionUtil.ParameterNullOrEmpty("settings.ApplicationPath");
			}
			if (string.IsNullOrEmpty(bundlePath))
			{
				throw ExceptionUtil.ParameterNullOrEmpty("bundlePath");
			}
			BundleCollection bundleCollection = Optimizer.InitializeBundleCollection(settings);
			FileVirtualPathProvider virtualPathProvider = new FileVirtualPathProvider(settings.ApplicationPath);
			BundleContext bundleContext = new BundleContext();
			bundleContext.VirtualPathProvider = virtualPathProvider;
			bundleContext.BundleCollection = bundleCollection;
			bundleContext.BundleVirtualPath = bundlePath;
			Bundle bundleFor = bundleCollection.GetBundleFor(bundlePath);
			if (bundleFor != null)
			{
				return bundleFor.GetBundleResponse(bundleContext);
			}
			return null;
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00004FD0 File Offset: 0x000031D0
		private static BundleCollection InitializeBundleCollection(OptimizationSettings settings)
		{
			BundleCollection bundleCollection = settings.BundleTable ?? new BundleCollection();
			if (!string.IsNullOrEmpty(settings.BundleManifestPath))
			{
				using (FileStream fileStream = File.OpenRead(settings.BundleManifestPath))
				{
					BundleManifest bundleManifest = BundleManifest.ReadBundleManifest(fileStream);
					bundleManifest.Register(bundleCollection);
				}
			}
			if (settings.BundleSetupMethod != null)
			{
				settings.BundleSetupMethod(bundleCollection);
			}
			return bundleCollection;
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00005048 File Offset: 0x00003248
		public static void BuildAllBundles(OptimizationSettings settings)
		{
			if (settings == null)
			{
				throw new ArgumentNullException("settings");
			}
			if (string.IsNullOrEmpty(settings.ApplicationPath))
			{
				throw ExceptionUtil.ParameterNullOrEmpty("settings.ApplicationPath");
			}
			FileVirtualPathProvider virtualPathProvider = new FileVirtualPathProvider(settings.ApplicationPath);
			BundleCollection bundleCollection = Optimizer.InitializeBundleCollection(settings);
			foreach (Bundle bundle in ((IEnumerable<Bundle>)bundleCollection))
			{
				if (!(bundle is DynamicFolderBundle))
				{
					bundle.GetBundleResponse(new BundleContext
					{
						VirtualPathProvider = virtualPathProvider,
						BundleCollection = bundleCollection,
						BundleVirtualPath = bundle.Path
					});
				}
			}
		}
	}
}
