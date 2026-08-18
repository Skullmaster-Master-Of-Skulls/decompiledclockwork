using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.ModelConfiguration.Utilities;

namespace System.Data.Entity.ModelConfiguration.Mappers
{
	// Token: 0x02000823 RID: 2083
	internal sealed class MappingContext
	{
		// Token: 0x06005D8F RID: 23951 RVA: 0x001941D5 File Offset: 0x001923D5
		public MappingContext(ModelConfiguration modelConfiguration, ConventionsConfiguration conventionsConfiguration, EdmModel model, DbModelBuilderVersion modelBuilderVersion = DbModelBuilderVersion.Latest, AttributeProvider attributeProvider = null)
		{
			this._modelConfiguration = modelConfiguration;
			this._conventionsConfiguration = conventionsConfiguration;
			this._model = model;
			this._modelBuilderVersion = modelBuilderVersion;
			this._attributeProvider = (attributeProvider ?? new AttributeProvider());
		}

		// Token: 0x17000FDB RID: 4059
		// (get) Token: 0x06005D90 RID: 23952 RVA: 0x0019420B File Offset: 0x0019240B
		public ModelConfiguration ModelConfiguration
		{
			get
			{
				return this._modelConfiguration;
			}
		}

		// Token: 0x17000FDC RID: 4060
		// (get) Token: 0x06005D91 RID: 23953 RVA: 0x00194213 File Offset: 0x00192413
		public ConventionsConfiguration ConventionsConfiguration
		{
			get
			{
				return this._conventionsConfiguration;
			}
		}

		// Token: 0x17000FDD RID: 4061
		// (get) Token: 0x06005D92 RID: 23954 RVA: 0x0019421B File Offset: 0x0019241B
		public EdmModel Model
		{
			get
			{
				return this._model;
			}
		}

		// Token: 0x17000FDE RID: 4062
		// (get) Token: 0x06005D93 RID: 23955 RVA: 0x00194223 File Offset: 0x00192423
		public AttributeProvider AttributeProvider
		{
			get
			{
				return this._attributeProvider;
			}
		}

		// Token: 0x17000FDF RID: 4063
		// (get) Token: 0x06005D94 RID: 23956 RVA: 0x0019422B File Offset: 0x0019242B
		public DbModelBuilderVersion ModelBuilderVersion
		{
			get
			{
				return this._modelBuilderVersion;
			}
		}

		// Token: 0x040024F6 RID: 9462
		private readonly ModelConfiguration _modelConfiguration;

		// Token: 0x040024F7 RID: 9463
		private readonly ConventionsConfiguration _conventionsConfiguration;

		// Token: 0x040024F8 RID: 9464
		private readonly EdmModel _model;

		// Token: 0x040024F9 RID: 9465
		private readonly AttributeProvider _attributeProvider;

		// Token: 0x040024FA RID: 9466
		private readonly DbModelBuilderVersion _modelBuilderVersion;
	}
}
