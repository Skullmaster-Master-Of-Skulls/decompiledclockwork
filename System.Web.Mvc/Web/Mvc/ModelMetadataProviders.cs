using System;

namespace System.Web.Mvc
{
	// Token: 0x02000150 RID: 336
	public class ModelMetadataProviders
	{
		// Token: 0x06000899 RID: 2201 RVA: 0x00017DE8 File Offset: 0x00015FE8
		internal ModelMetadataProviders(IResolver<ModelMetadataProvider> resolver = null)
		{
			IResolver<ModelMetadataProvider> resolver2 = resolver;
			if (resolver == null)
			{
				resolver2 = new SingleServiceResolver<ModelMetadataProvider>(() => this._currentProvider, new CachedDataAnnotationsModelMetadataProvider(), "ModelMetadataProviders.Current");
			}
			this._resolver = resolver2;
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x0600089A RID: 2202 RVA: 0x00017E28 File Offset: 0x00016028
		// (set) Token: 0x0600089B RID: 2203 RVA: 0x00017E34 File Offset: 0x00016034
		public static ModelMetadataProvider Current
		{
			get
			{
				return ModelMetadataProviders._instance.CurrentInternal;
			}
			set
			{
				ModelMetadataProviders._instance.CurrentInternal = value;
			}
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x0600089C RID: 2204 RVA: 0x00017E41 File Offset: 0x00016041
		// (set) Token: 0x0600089D RID: 2205 RVA: 0x00017E4E File Offset: 0x0001604E
		internal ModelMetadataProvider CurrentInternal
		{
			get
			{
				return this._resolver.Current;
			}
			set
			{
				this._currentProvider = (value ?? new EmptyModelMetadataProvider());
			}
		}

		// Token: 0x0400026F RID: 623
		private static ModelMetadataProviders _instance = new ModelMetadataProviders(null);

		// Token: 0x04000270 RID: 624
		private ModelMetadataProvider _currentProvider;

		// Token: 0x04000271 RID: 625
		private IResolver<ModelMetadataProvider> _resolver;
	}
}
