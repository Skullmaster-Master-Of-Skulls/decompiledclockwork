using System;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Linq;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x0200074F RID: 1871
	public class DbModel : IEdmModelAdapter
	{
		// Token: 0x060054D9 RID: 21721 RVA: 0x00172380 File Offset: 0x00170580
		internal DbModel(DbDatabaseMapping databaseMapping, DbModelBuilder modelBuilder)
		{
			this._databaseMapping = databaseMapping;
			this._cachedModelBuilder = modelBuilder;
		}

		// Token: 0x060054DA RID: 21722 RVA: 0x00172396 File Offset: 0x00170596
		internal DbModel(DbProviderInfo providerInfo, DbProviderManifest providerManifest)
		{
			this._databaseMapping = new DbDatabaseMapping().Initialize(EdmModel.CreateConceptualModel(3.0), EdmModel.CreateStoreModel(providerInfo, providerManifest, 3.0));
		}

		// Token: 0x060054DB RID: 21723 RVA: 0x001723CC File Offset: 0x001705CC
		internal DbModel(EdmModel conceptualModel, EdmModel storeModel)
		{
			this._databaseMapping = new DbDatabaseMapping
			{
				Model = conceptualModel,
				Database = storeModel
			};
		}

		// Token: 0x17000E76 RID: 3702
		// (get) Token: 0x060054DC RID: 21724 RVA: 0x001723FA File Offset: 0x001705FA
		public DbProviderInfo ProviderInfo
		{
			get
			{
				return this.StoreModel.ProviderInfo;
			}
		}

		// Token: 0x17000E77 RID: 3703
		// (get) Token: 0x060054DD RID: 21725 RVA: 0x00172407 File Offset: 0x00170607
		public DbProviderManifest ProviderManifest
		{
			get
			{
				return this.StoreModel.ProviderManifest;
			}
		}

		// Token: 0x17000E78 RID: 3704
		// (get) Token: 0x060054DE RID: 21726 RVA: 0x00172414 File Offset: 0x00170614
		public EdmModel ConceptualModel
		{
			get
			{
				return this._databaseMapping.Model;
			}
		}

		// Token: 0x17000E79 RID: 3705
		// (get) Token: 0x060054DF RID: 21727 RVA: 0x00172421 File Offset: 0x00170621
		public EdmModel StoreModel
		{
			get
			{
				return this._databaseMapping.Database;
			}
		}

		// Token: 0x17000E7A RID: 3706
		// (get) Token: 0x060054E0 RID: 21728 RVA: 0x0017242E File Offset: 0x0017062E
		public EntityContainerMapping ConceptualToStoreMapping
		{
			get
			{
				return this._databaseMapping.EntityContainerMappings.SingleOrDefault<EntityContainerMapping>();
			}
		}

		// Token: 0x17000E7B RID: 3707
		// (get) Token: 0x060054E1 RID: 21729 RVA: 0x00172440 File Offset: 0x00170640
		internal DbModelBuilder CachedModelBuilder
		{
			get
			{
				return this._cachedModelBuilder;
			}
		}

		// Token: 0x17000E7C RID: 3708
		// (get) Token: 0x060054E2 RID: 21730 RVA: 0x00172448 File Offset: 0x00170648
		internal DbDatabaseMapping DatabaseMapping
		{
			get
			{
				return this._databaseMapping;
			}
		}

		// Token: 0x060054E3 RID: 21731 RVA: 0x00172450 File Offset: 0x00170650
		public DbCompiledModel Compile()
		{
			return new DbCompiledModel(this);
		}

		// Token: 0x04002297 RID: 8855
		private readonly DbDatabaseMapping _databaseMapping;

		// Token: 0x04002298 RID: 8856
		private readonly DbModelBuilder _cachedModelBuilder;
	}
}
