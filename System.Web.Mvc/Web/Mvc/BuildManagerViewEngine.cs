using System;
using System.Threading;
using System.Web.Hosting;
using System.Web.WebPages;
using Microsoft.Internal.Web.Utils;

namespace System.Web.Mvc
{
	// Token: 0x02000068 RID: 104
	public abstract class BuildManagerViewEngine : VirtualPathProviderViewEngine
	{
		// Token: 0x060002D0 RID: 720 RVA: 0x00009485 File Offset: 0x00007685
		protected BuildManagerViewEngine() : this(null, null, null, null)
		{
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x00009491 File Offset: 0x00007691
		protected BuildManagerViewEngine(IViewPageActivator viewPageActivator) : this(viewPageActivator, null, null, null)
		{
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x000094B8 File Offset: 0x000076B8
		internal BuildManagerViewEngine(IViewPageActivator viewPageActivator, IResolver<IViewPageActivator> activatorResolver, IDependencyResolver dependencyResolver, VirtualPathProvider pathProvider)
		{
			if (viewPageActivator != null)
			{
				this._viewPageActivator = viewPageActivator;
			}
			else
			{
				IResolver<IViewPageActivator> activatorResolver2 = activatorResolver;
				if (activatorResolver == null)
				{
					activatorResolver2 = new SingleServiceResolver<IViewPageActivator>(() => null, new BuildManagerViewEngine.DefaultViewPageActivator(dependencyResolver), "BuildManagerViewEngine constructor");
				}
				this._activatorResolver = activatorResolver2;
			}
			if (pathProvider != null)
			{
				Func<VirtualPathProvider> virtualPathProviderFunc = () => pathProvider;
				this._fileExistsCache = new FileExistenceCache(virtualPathProviderFunc, 1000);
				base.VirtualPathProviderFunc = virtualPathProviderFunc;
				return;
			}
			if (BuildManagerViewEngine._sharedFileExistsCache == null)
			{
				BuildManagerViewEngine._sharedFileExistsCache = new FileExistenceCache(() => HostingEnvironment.VirtualPathProvider, 1000);
			}
			this._fileExistsCache = BuildManagerViewEngine._sharedFileExistsCache;
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x00009590 File Offset: 0x00007790
		// (set) Token: 0x060002D4 RID: 724 RVA: 0x000095AB File Offset: 0x000077AB
		internal IBuildManager BuildManager
		{
			get
			{
				if (this._buildManager == null)
				{
					this._buildManager = new BuildManagerWrapper();
				}
				return this._buildManager;
			}
			set
			{
				this._buildManager = value;
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x000095B4 File Offset: 0x000077B4
		protected IViewPageActivator ViewPageActivator
		{
			get
			{
				if (this._viewPageActivator != null)
				{
					return this._viewPageActivator;
				}
				this._viewPageActivator = this._activatorResolver.Current;
				return this._viewPageActivator;
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x000095DC File Offset: 0x000077DC
		protected virtual bool IsPrecompiledNonUpdateableSite
		{
			get
			{
				return LazyInitializer.EnsureInitialized<bool>(ref BuildManagerViewEngine._isPrecompiledNonUpdateableSite, ref BuildManagerViewEngine._isPrecompiledNonUpdateableSiteInitialized, ref BuildManagerViewEngine._isPrecompiledNonUpdateableSiteInitializedLock, new Func<bool>(BuildManagerViewEngine.GetPrecompiledNonUpdateable));
			}
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x000095FE File Offset: 0x000077FE
		protected override bool FileExists(ControllerContext controllerContext, string virtualPath)
		{
			return this._fileExistsCache.FileExists(virtualPath) || (this.IsPrecompiledNonUpdateableSite && this.BuildManager.FileExists(virtualPath));
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x00009628 File Offset: 0x00007828
		private static bool GetPrecompiledNonUpdateable()
		{
			IVirtualPathUtility virtualPathUtility = new VirtualPathUtilityWrapper();
			return BuildManagerWrapper.IsNonUpdateablePrecompiledApp(HostingEnvironment.VirtualPathProvider, virtualPathUtility);
		}

		// Token: 0x0400009E RID: 158
		private static object _isPrecompiledNonUpdateableSiteInitializedLock = new object();

		// Token: 0x0400009F RID: 159
		private static bool _isPrecompiledNonUpdateableSite;

		// Token: 0x040000A0 RID: 160
		private static bool _isPrecompiledNonUpdateableSiteInitialized;

		// Token: 0x040000A1 RID: 161
		private static FileExistenceCache _sharedFileExistsCache;

		// Token: 0x040000A2 RID: 162
		private IBuildManager _buildManager;

		// Token: 0x040000A3 RID: 163
		private IViewPageActivator _viewPageActivator;

		// Token: 0x040000A4 RID: 164
		private IResolver<IViewPageActivator> _activatorResolver;

		// Token: 0x040000A5 RID: 165
		private FileExistenceCache _fileExistsCache;

		// Token: 0x0200006A RID: 106
		internal class DefaultViewPageActivator : IViewPageActivator
		{
			// Token: 0x060002DD RID: 733 RVA: 0x00009652 File Offset: 0x00007852
			public DefaultViewPageActivator() : this(null)
			{
			}

			// Token: 0x060002DE RID: 734 RVA: 0x00009674 File Offset: 0x00007874
			public DefaultViewPageActivator(IDependencyResolver resolver)
			{
				if (resolver == null)
				{
					this._resolverThunk = (() => DependencyResolver.Current);
					return;
				}
				this._resolverThunk = (() => resolver);
			}

			// Token: 0x060002DF RID: 735 RVA: 0x000096DC File Offset: 0x000078DC
			public object Create(ControllerContext controllerContext, Type type)
			{
				object result;
				try
				{
					result = (this._resolverThunk().GetService(type) ?? Activator.CreateInstance(type));
				}
				catch (MissingMethodException originalException)
				{
					MissingMethodException ex = TypeHelpers.EnsureDebuggableException(originalException, type.FullName);
					if (ex != null)
					{
						throw ex;
					}
					throw;
				}
				return result;
			}

			// Token: 0x040000A8 RID: 168
			private Func<IDependencyResolver> _resolverThunk;
		}
	}
}
