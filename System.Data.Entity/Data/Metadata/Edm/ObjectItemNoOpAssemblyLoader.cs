using System;
using System.Reflection;

namespace System.Data.Metadata.Edm
{
	// Token: 0x02000220 RID: 544
	internal class ObjectItemNoOpAssemblyLoader : ObjectItemAssemblyLoader
	{
		// Token: 0x0600238E RID: 9102 RVA: 0x0007F9E0 File Offset: 0x0007DBE0
		internal ObjectItemNoOpAssemblyLoader(Assembly assembly, ObjectItemLoadingSessionData sessionData) : base(assembly, new MutableAssemblyCacheEntry(), sessionData)
		{
		}

		// Token: 0x0600238F RID: 9103 RVA: 0x0007F9EF File Offset: 0x0007DBEF
		internal override void Load()
		{
			if (!base.SessionData.KnownAssemblies.Contains(base.SourceAssembly, base.SessionData.ObjectItemAssemblyLoaderFactory, base.SessionData.EdmItemCollection))
			{
				this.AddToKnownAssemblies();
			}
		}

		// Token: 0x06002390 RID: 9104 RVA: 0x00072E1F File Offset: 0x0007101F
		protected override void AddToAssembliesLoaded()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002391 RID: 9105 RVA: 0x00072E1F File Offset: 0x0007101F
		protected override void LoadTypesFromAssembly()
		{
			throw new NotImplementedException();
		}
	}
}
