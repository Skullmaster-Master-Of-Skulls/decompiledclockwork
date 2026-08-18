using System;
using System.ComponentModel;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Infrastructure.DependencyResolution
{
	// Token: 0x02000156 RID: 342
	public class DbConfigurationLoadedEventArgs : EventArgs
	{
		// Token: 0x06000B23 RID: 2851 RVA: 0x00038002 File Offset: 0x00036202
		internal DbConfigurationLoadedEventArgs(InternalConfiguration configuration)
		{
			this._internalConfiguration = configuration;
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000B24 RID: 2852 RVA: 0x00038011 File Offset: 0x00036211
		public IDbDependencyResolver DependencyResolver
		{
			get
			{
				return this._internalConfiguration.ResolverSnapshot;
			}
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x0003801E File Offset: 0x0003621E
		public void AddDependencyResolver(IDbDependencyResolver resolver, bool overrideConfigFile)
		{
			Check.NotNull<IDbDependencyResolver>(resolver, "resolver");
			this._internalConfiguration.CheckNotLocked("AddDependencyResolver");
			this._internalConfiguration.AddDependencyResolver(resolver, overrideConfigFile);
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x00038049 File Offset: 0x00036249
		public void AddDefaultResolver(IDbDependencyResolver resolver)
		{
			Check.NotNull<IDbDependencyResolver>(resolver, "resolver");
			this._internalConfiguration.CheckNotLocked("AddDefaultResolver");
			this._internalConfiguration.AddDefaultResolver(resolver);
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x00038073 File Offset: 0x00036273
		public void ReplaceService<TService>(Func<TService, object, TService> serviceInterceptor)
		{
			Check.NotNull<Func<TService, object, TService>>(serviceInterceptor, "serviceInterceptor");
			this.AddDependencyResolver(new WrappingDependencyResolver<TService>(this.DependencyResolver, serviceInterceptor), true);
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x00038094 File Offset: 0x00036294
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x0003809C File Offset: 0x0003629C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x000380A5 File Offset: 0x000362A5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x000380AD File Offset: 0x000362AD
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x0400030D RID: 781
		private readonly InternalConfiguration _internalConfiguration;
	}
}
