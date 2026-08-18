using System;
using System.Data.Common;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Utilities
{
	// Token: 0x0200082A RID: 2090
	internal static class DbConnectionExtensions
	{
		// Token: 0x06005DC3 RID: 24003 RVA: 0x00195729 File Offset: 0x00193929
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
		public static string GetProviderInvariantName(this DbConnection connection)
		{
			return DbConfiguration.DependencyResolver.GetService(DbProviderServices.GetProviderFactory(connection)).Name;
		}

		// Token: 0x06005DC4 RID: 24004 RVA: 0x00195740 File Offset: 0x00193940
		public static DbProviderInfo GetProviderInfo(this DbConnection connection, out DbProviderManifest providerManifest)
		{
			string text = DbConfiguration.DependencyResolver.GetService<IManifestTokenResolver>().ResolveManifestToken(connection);
			DbProviderInfo result = new DbProviderInfo(connection.GetProviderInvariantName(), text);
			providerManifest = DbProviderServices.GetProviderServices(connection).GetProviderManifest(text);
			return result;
		}

		// Token: 0x06005DC5 RID: 24005 RVA: 0x0019577A File Offset: 0x0019397A
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
		public static DbProviderFactory GetProviderFactory(this DbConnection connection)
		{
			return DbConfiguration.DependencyResolver.GetService<IDbProviderFactoryResolver>().ResolveProviderFactory(connection);
		}
	}
}
