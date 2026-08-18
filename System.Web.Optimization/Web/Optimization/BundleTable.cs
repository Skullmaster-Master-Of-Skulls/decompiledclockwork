using System;
using System.Web.Hosting;

namespace System.Web.Optimization
{
	// Token: 0x02000013 RID: 19
	public static class BundleTable
	{
		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000BA RID: 186 RVA: 0x00003DF6 File Offset: 0x00001FF6
		public static BundleCollection Bundles
		{
			get
			{
				BundleTable.EnsureBundleSetup();
				return BundleTable._instance;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00003E02 File Offset: 0x00002002
		// (set) Token: 0x060000BC RID: 188 RVA: 0x00003E25 File Offset: 0x00002025
		public static bool EnableOptimizations
		{
			get
			{
				if (!BundleTable._enableOptimizationsSet && HttpContext.Current != null)
				{
					return !HttpContext.Current.IsDebuggingEnabled;
				}
				return BundleTable._enableOptimizations;
			}
			set
			{
				BundleTable._enableOptimizations = value;
				BundleTable._enableOptimizationsSet = true;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000BD RID: 189 RVA: 0x00003E33 File Offset: 0x00002033
		// (set) Token: 0x060000BE RID: 190 RVA: 0x00003E43 File Offset: 0x00002043
		public static VirtualPathProvider VirtualPathProvider
		{
			get
			{
				return BundleTable._vpp ?? HostingEnvironment.VirtualPathProvider;
			}
			set
			{
				BundleTable._vpp = value;
			}
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00003E4C File Offset: 0x0000204C
		private static void EnsureBundleSetup()
		{
			if (!BundleTable._readBundleManifest)
			{
				BundleTable._readBundleManifest = true;
				BundleManifest bundleManifest = BundleManifest.ReadBundleManifest();
				if (bundleManifest != null)
				{
					bundleManifest.Register(BundleTable.Bundles);
				}
			}
		}

		// Token: 0x0400003B RID: 59
		private static BundleCollection _instance = new BundleCollection();

		// Token: 0x0400003C RID: 60
		private static bool _enableOptimizations = true;

		// Token: 0x0400003D RID: 61
		private static bool _enableOptimizationsSet = false;

		// Token: 0x0400003E RID: 62
		private static VirtualPathProvider _vpp;

		// Token: 0x0400003F RID: 63
		private static bool _readBundleManifest = false;
	}
}
