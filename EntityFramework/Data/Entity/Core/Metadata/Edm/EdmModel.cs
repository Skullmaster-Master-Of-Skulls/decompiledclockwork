using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x02000731 RID: 1841
	public class EdmModel : MetadataItem
	{
		// Token: 0x0600531A RID: 21274 RVA: 0x0016EA24 File Offset: 0x0016CC24
		private EdmModel(EntityContainer entityContainer, double version = 3.0)
		{
			this._container = entityContainer;
			this.SchemaVersion = version;
		}

		// Token: 0x0600531B RID: 21275 RVA: 0x0016EA7C File Offset: 0x0016CC7C
		internal EdmModel(DataSpace dataSpace, double schemaVersion = 3.0)
		{
			if (dataSpace != DataSpace.CSpace && dataSpace != DataSpace.SSpace)
			{
				throw new ArgumentException(Strings.MetadataItem_InvalidDataSpace(dataSpace, typeof(EdmModel).Name), "dataSpace");
			}
			this._container = new EntityContainer((dataSpace == DataSpace.CSpace) ? "CodeFirstContainer" : "CodeFirstDatabase", dataSpace);
			this._schemaVersion = schemaVersion;
		}

		// Token: 0x17000E0B RID: 3595
		// (get) Token: 0x0600531C RID: 21276 RVA: 0x0016EB16 File Offset: 0x0016CD16
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.MetadataItem;
			}
		}

		// Token: 0x17000E0C RID: 3596
		// (get) Token: 0x0600531D RID: 21277 RVA: 0x0016EB1A File Offset: 0x0016CD1A
		internal override string Identity
		{
			get
			{
				return "EdmModel" + this.Container.Identity;
			}
		}

		// Token: 0x17000E0D RID: 3597
		// (get) Token: 0x0600531E RID: 21278 RVA: 0x0016EB31 File Offset: 0x0016CD31
		public DataSpace DataSpace
		{
			get
			{
				return this.Container.DataSpace;
			}
		}

		// Token: 0x17000E0E RID: 3598
		// (get) Token: 0x0600531F RID: 21279 RVA: 0x0016EB3E File Offset: 0x0016CD3E
		public IEnumerable<AssociationType> AssociationTypes
		{
			get
			{
				return this._associationTypes;
			}
		}

		// Token: 0x17000E0F RID: 3599
		// (get) Token: 0x06005320 RID: 21280 RVA: 0x0016EB46 File Offset: 0x0016CD46
		public IEnumerable<ComplexType> ComplexTypes
		{
			get
			{
				return this._complexTypes;
			}
		}

		// Token: 0x17000E10 RID: 3600
		// (get) Token: 0x06005321 RID: 21281 RVA: 0x0016EB4E File Offset: 0x0016CD4E
		public IEnumerable<EntityType> EntityTypes
		{
			get
			{
				return this._entityTypes;
			}
		}

		// Token: 0x17000E11 RID: 3601
		// (get) Token: 0x06005322 RID: 21282 RVA: 0x0016EB56 File Offset: 0x0016CD56
		public IEnumerable<EnumType> EnumTypes
		{
			get
			{
				return this._enumTypes;
			}
		}

		// Token: 0x17000E12 RID: 3602
		// (get) Token: 0x06005323 RID: 21283 RVA: 0x0016EB5E File Offset: 0x0016CD5E
		public IEnumerable<EdmFunction> Functions
		{
			get
			{
				return this._functions;
			}
		}

		// Token: 0x17000E13 RID: 3603
		// (get) Token: 0x06005324 RID: 21284 RVA: 0x0016EB66 File Offset: 0x0016CD66
		public EntityContainer Container
		{
			get
			{
				return this._container;
			}
		}

		// Token: 0x17000E14 RID: 3604
		// (get) Token: 0x06005325 RID: 21285 RVA: 0x0016EB6E File Offset: 0x0016CD6E
		// (set) Token: 0x06005326 RID: 21286 RVA: 0x0016EB76 File Offset: 0x0016CD76
		internal double SchemaVersion
		{
			get
			{
				return this._schemaVersion;
			}
			set
			{
				this._schemaVersion = value;
			}
		}

		// Token: 0x17000E15 RID: 3605
		// (get) Token: 0x06005327 RID: 21287 RVA: 0x0016EB7F File Offset: 0x0016CD7F
		// (set) Token: 0x06005328 RID: 21288 RVA: 0x0016EB87 File Offset: 0x0016CD87
		internal DbProviderInfo ProviderInfo
		{
			get
			{
				return this._providerInfo;
			}
			private set
			{
				this._providerInfo = value;
			}
		}

		// Token: 0x17000E16 RID: 3606
		// (get) Token: 0x06005329 RID: 21289 RVA: 0x0016EB90 File Offset: 0x0016CD90
		// (set) Token: 0x0600532A RID: 21290 RVA: 0x0016EB98 File Offset: 0x0016CD98
		internal DbProviderManifest ProviderManifest
		{
			get
			{
				return this._providerManifest;
			}
			private set
			{
				this._providerManifest = value;
			}
		}

		// Token: 0x17000E17 RID: 3607
		// (get) Token: 0x0600532B RID: 21291 RVA: 0x0016EBA9 File Offset: 0x0016CDA9
		internal virtual IEnumerable<string> NamespaceNames
		{
			get
			{
				return (from t in this.NamespaceItems
				select t.NamespaceName).Distinct<string>();
			}
		}

		// Token: 0x17000E18 RID: 3608
		// (get) Token: 0x0600532C RID: 21292 RVA: 0x0016EBD8 File Offset: 0x0016CDD8
		internal IEnumerable<EdmType> NamespaceItems
		{
			get
			{
				return this._associationTypes.Concat(this._complexTypes).Concat(this._entityTypes).Concat(this._enumTypes).Concat(this._functions);
			}
		}

		// Token: 0x17000E19 RID: 3609
		// (get) Token: 0x0600532D RID: 21293 RVA: 0x0016EC0C File Offset: 0x0016CE0C
		public IEnumerable<GlobalItem> GlobalItems
		{
			get
			{
				return this.NamespaceItems.Concat(this.Containers);
			}
		}

		// Token: 0x17000E1A RID: 3610
		// (get) Token: 0x0600532E RID: 21294 RVA: 0x0016ECEC File Offset: 0x0016CEEC
		internal virtual IEnumerable<EntityContainer> Containers
		{
			get
			{
				yield return this.Container;
				yield break;
			}
		}

		// Token: 0x0600532F RID: 21295 RVA: 0x0016ED09 File Offset: 0x0016CF09
		public void AddItem(AssociationType item)
		{
			Check.NotNull<AssociationType>(item, "item");
			this.ValidateSpace(item);
			this._associationTypes.Add(item);
		}

		// Token: 0x06005330 RID: 21296 RVA: 0x0016ED2A File Offset: 0x0016CF2A
		public void AddItem(ComplexType item)
		{
			Check.NotNull<ComplexType>(item, "item");
			this.ValidateSpace(item);
			this._complexTypes.Add(item);
		}

		// Token: 0x06005331 RID: 21297 RVA: 0x0016ED4B File Offset: 0x0016CF4B
		public void AddItem(EntityType item)
		{
			Check.NotNull<EntityType>(item, "item");
			this.ValidateSpace(item);
			this._entityTypes.Add(item);
		}

		// Token: 0x06005332 RID: 21298 RVA: 0x0016ED6C File Offset: 0x0016CF6C
		public void AddItem(EnumType item)
		{
			Check.NotNull<EnumType>(item, "item");
			this.ValidateSpace(item);
			this._enumTypes.Add(item);
		}

		// Token: 0x06005333 RID: 21299 RVA: 0x0016ED8D File Offset: 0x0016CF8D
		public void AddItem(EdmFunction item)
		{
			Check.NotNull<EdmFunction>(item, "item");
			this.ValidateSpace(item);
			this._functions.Add(item);
		}

		// Token: 0x06005334 RID: 21300 RVA: 0x0016EDAE File Offset: 0x0016CFAE
		public void RemoveItem(AssociationType item)
		{
			Check.NotNull<AssociationType>(item, "item");
			this._associationTypes.Remove(item);
		}

		// Token: 0x06005335 RID: 21301 RVA: 0x0016EDC9 File Offset: 0x0016CFC9
		public void RemoveItem(ComplexType item)
		{
			Check.NotNull<ComplexType>(item, "item");
			this._complexTypes.Remove(item);
		}

		// Token: 0x06005336 RID: 21302 RVA: 0x0016EDE4 File Offset: 0x0016CFE4
		public void RemoveItem(EntityType item)
		{
			Check.NotNull<EntityType>(item, "item");
			this._entityTypes.Remove(item);
		}

		// Token: 0x06005337 RID: 21303 RVA: 0x0016EDFF File Offset: 0x0016CFFF
		public void RemoveItem(EnumType item)
		{
			Check.NotNull<EnumType>(item, "item");
			this._enumTypes.Remove(item);
		}

		// Token: 0x06005338 RID: 21304 RVA: 0x0016EE1A File Offset: 0x0016D01A
		public void RemoveItem(EdmFunction item)
		{
			Check.NotNull<EdmFunction>(item, "item");
			this._functions.Remove(item);
		}

		// Token: 0x06005339 RID: 21305 RVA: 0x0016EE4C File Offset: 0x0016D04C
		internal virtual void Validate()
		{
			List<DataModelErrorEventArgs> validationErrors = new List<DataModelErrorEventArgs>();
			DataModelValidator dataModelValidator = new DataModelValidator();
			dataModelValidator.OnError += delegate(object _, DataModelErrorEventArgs e)
			{
				validationErrors.Add(e);
			};
			dataModelValidator.Validate(this, true);
			if (validationErrors.Count > 0)
			{
				throw new ModelValidationException(validationErrors);
			}
		}

		// Token: 0x0600533A RID: 21306 RVA: 0x0016EEA4 File Offset: 0x0016D0A4
		private void ValidateSpace(EdmType item)
		{
			if (item.DataSpace != this.DataSpace)
			{
				throw new ArgumentException(Strings.EdmModel_AddItem_NonMatchingNamespace, "item");
			}
		}

		// Token: 0x0600533B RID: 21307 RVA: 0x0016EEC4 File Offset: 0x0016D0C4
		internal static EdmModel CreateStoreModel(DbProviderInfo providerInfo, DbProviderManifest providerManifest, double schemaVersion = 3.0)
		{
			return new EdmModel(DataSpace.SSpace, schemaVersion)
			{
				ProviderInfo = providerInfo,
				ProviderManifest = providerManifest
			};
		}

		// Token: 0x0600533C RID: 21308 RVA: 0x0016EEE8 File Offset: 0x0016D0E8
		internal static EdmModel CreateStoreModel(EntityContainer entityContainer, DbProviderInfo providerInfo, DbProviderManifest providerManifest, double schemaVersion = 3.0)
		{
			EdmModel edmModel = new EdmModel(entityContainer, schemaVersion);
			if (providerInfo != null)
			{
				edmModel.ProviderInfo = providerInfo;
			}
			if (providerManifest != null)
			{
				edmModel.ProviderManifest = providerManifest;
			}
			return edmModel;
		}

		// Token: 0x0600533D RID: 21309 RVA: 0x0016EF12 File Offset: 0x0016D112
		internal static EdmModel CreateConceptualModel(double schemaVersion = 3.0)
		{
			return new EdmModel(DataSpace.CSpace, schemaVersion);
		}

		// Token: 0x0600533E RID: 21310 RVA: 0x0016EF1B File Offset: 0x0016D11B
		internal static EdmModel CreateConceptualModel(EntityContainer entityContainer, double schemaVersion = 3.0)
		{
			return new EdmModel(entityContainer, schemaVersion);
		}

		// Token: 0x04002250 RID: 8784
		private readonly List<AssociationType> _associationTypes = new List<AssociationType>();

		// Token: 0x04002251 RID: 8785
		private readonly List<ComplexType> _complexTypes = new List<ComplexType>();

		// Token: 0x04002252 RID: 8786
		private readonly List<EntityType> _entityTypes = new List<EntityType>();

		// Token: 0x04002253 RID: 8787
		private readonly List<EnumType> _enumTypes = new List<EnumType>();

		// Token: 0x04002254 RID: 8788
		private readonly List<EdmFunction> _functions = new List<EdmFunction>();

		// Token: 0x04002255 RID: 8789
		private readonly EntityContainer _container;

		// Token: 0x04002256 RID: 8790
		private double _schemaVersion;

		// Token: 0x04002257 RID: 8791
		private DbProviderInfo _providerInfo;

		// Token: 0x04002258 RID: 8792
		private DbProviderManifest _providerManifest;
	}
}
