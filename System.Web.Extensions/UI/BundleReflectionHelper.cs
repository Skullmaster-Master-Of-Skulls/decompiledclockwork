using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Web.Compilation;

namespace System.Web.UI
{
	// Token: 0x0200004B RID: 75
	internal sealed class BundleReflectionHelper
	{
		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060002CF RID: 719 RVA: 0x00011B20 File Offset: 0x0000FD20
		// (set) Token: 0x060002D0 RID: 720 RVA: 0x00011B28 File Offset: 0x0000FD28
		private BundleReflectionHelper.IsBundleVirtualPathDelegate IsBundleVirtualPathMethod { get; set; }

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x00011B31 File Offset: 0x0000FD31
		// (set) Token: 0x060002D2 RID: 722 RVA: 0x00011B39 File Offset: 0x0000FD39
		private BundleReflectionHelper.GetBundleContentsDelegate GetBundleContentsMethod { get; set; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x00011B42 File Offset: 0x0000FD42
		// (set) Token: 0x060002D4 RID: 724 RVA: 0x00011B4A File Offset: 0x0000FD4A
		private BundleReflectionHelper.GetBundleUrlDelegate GetBundleUrlMethod { get; set; }

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x00011B53 File Offset: 0x0000FD53
		// (set) Token: 0x060002D6 RID: 726 RVA: 0x00011B5A File Offset: 0x0000FD5A
		private static BundleReflectionHelper.BundleResolverCurrentDelegate BundleResolverCurrentMethod { get; set; }

		// Token: 0x060002D7 RID: 727 RVA: 0x00011B62 File Offset: 0x0000FD62
		public BundleReflectionHelper()
		{
			this.BundleResolver = BundleReflectionHelper.CallBundleResolverCurrent();
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x00011B75 File Offset: 0x0000FD75
		public BundleReflectionHelper(object bundleResolver)
		{
			this.BundleResolver = bundleResolver;
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060002D9 RID: 729 RVA: 0x00011B84 File Offset: 0x0000FD84
		// (set) Token: 0x060002DA RID: 730 RVA: 0x00011B8C File Offset: 0x0000FD8C
		internal object BundleResolver
		{
			get
			{
				return this._resolver;
			}
			set
			{
				if (value != null)
				{
					Type type = value.GetType();
					Type[] types = new Type[]
					{
						typeof(string)
					};
					this.IsBundleVirtualPathMethod = BundleReflectionHelper.MakeDelegate<BundleReflectionHelper.IsBundleVirtualPathDelegate>(value, type.GetMethod("IsBundleVirtualPath", types));
					this.GetBundleContentsMethod = BundleReflectionHelper.MakeDelegate<BundleReflectionHelper.GetBundleContentsDelegate>(value, type.GetMethod("GetBundleContents", types));
					this.GetBundleUrlMethod = BundleReflectionHelper.MakeDelegate<BundleReflectionHelper.GetBundleUrlDelegate>(value, type.GetMethod("GetBundleUrl", types));
					if (this.IsBundleVirtualPathMethod != null && this.GetBundleContentsMethod != null && this.GetBundleUrlMethod != null)
					{
						this._resolver = value;
						return;
					}
				}
				else
				{
					this._resolver = null;
				}
			}
		}

		// Token: 0x060002DB RID: 731 RVA: 0x00011C2C File Offset: 0x0000FE2C
		public bool IsBundleVirtualPath(string virtualPath)
		{
			if (this.BundleResolver != null)
			{
				try
				{
					return this.IsBundleVirtualPathMethod(virtualPath);
				}
				catch
				{
				}
				return false;
			}
			return false;
		}

		// Token: 0x060002DC RID: 732 RVA: 0x00011C68 File Offset: 0x0000FE68
		public IEnumerable<string> GetBundleContents(string virtualPath)
		{
			if (this.BundleResolver != null)
			{
				try
				{
					return this.GetBundleContentsMethod(virtualPath);
				}
				catch
				{
				}
			}
			return null;
		}

		// Token: 0x060002DD RID: 733 RVA: 0x00011CA4 File Offset: 0x0000FEA4
		public string GetBundleUrl(string virtualPath)
		{
			if (this.BundleResolver != null)
			{
				try
				{
					return this.GetBundleUrlMethod(virtualPath);
				}
				catch
				{
				}
				return virtualPath;
			}
			return virtualPath;
		}

		// Token: 0x060002DE RID: 734 RVA: 0x00011CE0 File Offset: 0x0000FEE0
		internal static object CallBundleResolverCurrent()
		{
			if (!Volatile.Read(ref BundleReflectionHelper.s_lookedForCurrentProperty))
			{
				try
				{
					Type type = BuildManager.GetType("System.Web.Optimization.BundleResolver", false);
					if (type != null)
					{
						PropertyInfo property = type.GetProperty("Current", BindingFlags.Static | BindingFlags.Public);
						if (property != null)
						{
							BundleReflectionHelper.BundleResolverCurrentMethod = BundleReflectionHelper.MakeDelegate<BundleReflectionHelper.BundleResolverCurrentDelegate>(null, property.GetGetMethod());
						}
					}
				}
				catch
				{
				}
				Volatile.Write(ref BundleReflectionHelper.s_lookedForCurrentProperty, true);
			}
			if (BundleReflectionHelper.BundleResolverCurrentMethod == null)
			{
				return null;
			}
			return BundleReflectionHelper.BundleResolverCurrentMethod();
		}

		// Token: 0x060002DF RID: 735 RVA: 0x00011D6C File Offset: 0x0000FF6C
		private static T MakeDelegate<T>(object target, MethodInfo method) where T : class
		{
			return Delegate.CreateDelegate(typeof(T), target, method, false) as T;
		}

		// Token: 0x04000111 RID: 273
		private object _resolver;

		// Token: 0x04000112 RID: 274
		private static bool s_lookedForCurrentProperty;

		// Token: 0x02000155 RID: 341
		// (Invoke) Token: 0x06000FE2 RID: 4066
		private delegate bool IsBundleVirtualPathDelegate(string virtualPath);

		// Token: 0x02000156 RID: 342
		// (Invoke) Token: 0x06000FE6 RID: 4070
		private delegate IEnumerable<string> GetBundleContentsDelegate(string virtualPath);

		// Token: 0x02000157 RID: 343
		// (Invoke) Token: 0x06000FEA RID: 4074
		private delegate string GetBundleUrlDelegate(string virtualPath);

		// Token: 0x02000158 RID: 344
		// (Invoke) Token: 0x06000FEE RID: 4078
		private delegate object BundleResolverCurrentDelegate();
	}
}
