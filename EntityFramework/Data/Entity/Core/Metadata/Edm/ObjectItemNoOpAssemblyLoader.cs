using System;
using System.Reflection;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x02000520 RID: 1312
	internal class ObjectItemNoOpAssemblyLoader : ObjectItemAssemblyLoader
	{
		// Token: 0x06003173 RID: 12659 RVA: 0x000ECC1C File Offset: 0x000EAE1C
		internal ObjectItemNoOpAssemblyLoader(Assembly assembly, ObjectItemLoadingSessionData sessionData) : base(assembly, new MutableAssemblyCacheEntry(), sessionData)
		{
		}

		// Token: 0x06003174 RID: 12660 RVA: 0x000ECC2B File Offset: 0x000EAE2B
		internal override void Load()
		{
			if (!base.SessionData.KnownAssemblies.Contains(base.SourceAssembly, base.SessionData.ObjectItemAssemblyLoaderFactory, base.SessionData.EdmItemCollection))
			{
				this.AddToKnownAssemblies();
			}
		}

		// Token: 0x06003175 RID: 12661 RVA: 0x000ECC61 File Offset: 0x000EAE61
		protected override void AddToAssembliesLoaded()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06003176 RID: 12662 RVA: 0x000ECC68 File Offset: 0x000EAE68
		protected override void LoadTypesFromAssembly()
		{
			throw new NotImplementedException();
		}
	}
}
