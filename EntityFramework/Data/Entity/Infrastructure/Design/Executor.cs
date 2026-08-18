using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;

namespace System.Data.Entity.Infrastructure.Design
{
	// Token: 0x0200018A RID: 394
	internal class Executor : MarshalByRefObject
	{
		// Token: 0x06000D7B RID: 3451 RVA: 0x0003CE94 File Offset: 0x0003B094
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "anonymousArguments")]
		public Executor(string assemblyFile, IDictionary<string, object> anonymousArguments)
		{
			Check.NotEmpty(assemblyFile, "assemblyFile");
			this._assembly = Assembly.Load(AssemblyName.GetAssemblyName(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, assemblyFile)));
		}

		// Token: 0x06000D7C RID: 3452 RVA: 0x0003CEC8 File Offset: 0x0003B0C8
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
		internal virtual string GetProviderServicesInternal(string invariantName)
		{
			DbConfiguration.LoadConfiguration(this._assembly);
			IDbDependencyResolver dependencyResolver = DbConfiguration.DependencyResolver;
			DbProviderServices dbProviderServices = null;
			try
			{
				dbProviderServices = dependencyResolver.GetService(invariantName);
			}
			catch
			{
			}
			if (dbProviderServices == null)
			{
				return null;
			}
			return dbProviderServices.GetType().AssemblyQualifiedName;
		}

		// Token: 0x040003AB RID: 939
		[SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
		private readonly Assembly _assembly;

		// Token: 0x0200018B RID: 395
		[SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
		public class GetProviderServices : MarshalByRefObject
		{
			// Token: 0x06000D7D RID: 3453 RVA: 0x0003CF18 File Offset: 0x0003B118
			[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "anonymousArguments")]
			public GetProviderServices(Executor executor, object handler, string invariantName, IDictionary<string, object> anonymousArguments)
			{
				Check.NotNull<Executor>(executor, "executor");
				Check.NotNull<object>(handler, "handler");
				Check.NotEmpty(invariantName, "invariantName");
				WrappedHandler wrappedHandler = new WrappedHandler(handler);
				string providerServicesInternal = executor.GetProviderServicesInternal(invariantName);
				wrappedHandler.SetResult(providerServicesInternal);
			}
		}
	}
}
