using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Common.QueryCache;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm.Provider;
using System.Data.Entity.Core.SchemaObjectModel;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Xml;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x02000522 RID: 1314
	public class StoreItemCollection : ItemCollection
	{
		// Token: 0x06003177 RID: 12663 RVA: 0x000ECC6F File Offset: 0x000EAE6F
		internal StoreItemCollection() : base(DataSpace.SSpace)
		{
		}

		// Token: 0x06003178 RID: 12664 RVA: 0x000ECC90 File Offset: 0x000EAE90
		internal StoreItemCollection(DbProviderFactory factory, DbProviderManifest manifest, string providerInvariantName, string providerManifestToken) : base(DataSpace.SSpace)
		{
			this._providerFactory = factory;
			this._providerManifest = manifest;
			this._providerInvariantName = providerInvariantName;
			this._providerManifestToken = providerManifestToken;
			this._cachedCTypeFunction = new Memoizer<EdmFunction, EdmFunction>(new Func<EdmFunction, EdmFunction>(StoreItemCollection.ConvertFunctionSignatureToCType), null);
			this.LoadProviderManifest(this._providerManifest);
		}

		// Token: 0x06003179 RID: 12665 RVA: 0x000ECCFC File Offset: 0x000EAEFC
		private StoreItemCollection(IEnumerable<XmlReader> xmlReaders, ReadOnlyCollection<string> filePaths, IDbDependencyResolver resolver, out IList<EdmSchemaError> errors) : base(DataSpace.SSpace)
		{
			errors = this.Init(xmlReaders, filePaths, false, resolver, out this._providerManifest, out this._providerFactory, out this._providerInvariantName, out this._providerManifestToken, out this._cachedCTypeFunction);
		}

		// Token: 0x0600317A RID: 12666 RVA: 0x000ECD54 File Offset: 0x000EAF54
		internal StoreItemCollection(IEnumerable<XmlReader> xmlReaders, IEnumerable<string> filePaths) : base(DataSpace.SSpace)
		{
			EntityUtil.CheckArgumentEmpty<XmlReader>(ref xmlReaders, new Func<string, string>(Strings.StoreItemCollectionMustHaveOneArtifact), "xmlReader");
			this.Init(xmlReaders, filePaths, true, null, out this._providerManifest, out this._providerFactory, out this._providerInvariantName, out this._providerManifestToken, out this._cachedCTypeFunction);
		}

		// Token: 0x0600317B RID: 12667 RVA: 0x000ECDC0 File Offset: 0x000EAFC0
		public StoreItemCollection(IEnumerable<XmlReader> xmlReaders) : base(DataSpace.SSpace)
		{
			Check.NotNull<IEnumerable<XmlReader>>(xmlReaders, "xmlReaders");
			EntityUtil.CheckArgumentEmpty<XmlReader>(ref xmlReaders, new Func<string, string>(Strings.StoreItemCollectionMustHaveOneArtifact), "xmlReader");
			MetadataArtifactLoader metadataArtifactLoader = MetadataArtifactLoader.CreateCompositeFromXmlReaders(xmlReaders);
			this.Init(metadataArtifactLoader.GetReaders(), metadataArtifactLoader.GetPaths(), true, null, out this._providerManifest, out this._providerFactory, out this._providerInvariantName, out this._providerManifestToken, out this._cachedCTypeFunction);
		}

		// Token: 0x0600317C RID: 12668 RVA: 0x000ECE4C File Offset: 0x000EB04C
		public StoreItemCollection(EdmModel model) : base(DataSpace.SSpace)
		{
			Check.NotNull<EdmModel>(model, "model");
			this._providerManifest = model.ProviderManifest;
			this._providerInvariantName = model.ProviderInfo.ProviderInvariantName;
			this._providerFactory = DbConfiguration.DependencyResolver.GetService(this._providerInvariantName);
			this._providerManifestToken = model.ProviderInfo.ProviderManifestToken;
			this._cachedCTypeFunction = new Memoizer<EdmFunction, EdmFunction>(new Func<EdmFunction, EdmFunction>(StoreItemCollection.ConvertFunctionSignatureToCType), null);
			this.LoadProviderManifest(this._providerManifest);
			this._schemaVersion = model.SchemaVersion;
			model.Validate();
			foreach (GlobalItem globalItem in model.GlobalItems)
			{
				globalItem.SetReadOnly();
				base.AddInternal(globalItem);
			}
		}

		// Token: 0x0600317D RID: 12669 RVA: 0x000ECF44 File Offset: 0x000EB144
		public StoreItemCollection(params string[] filePaths) : base(DataSpace.SSpace)
		{
			Check.NotNull<string[]>(filePaths, "filePaths");
			IEnumerable<string> filePaths2 = filePaths;
			EntityUtil.CheckArgumentEmpty<string>(ref filePaths2, new Func<string, string>(Strings.StoreItemCollectionMustHaveOneArtifact), "filePaths");
			List<XmlReader> list = null;
			try
			{
				MetadataArtifactLoader metadataArtifactLoader = MetadataArtifactLoader.CreateCompositeFromFilePaths(filePaths2, ".ssdl");
				list = metadataArtifactLoader.CreateReaders(DataSpace.SSpace);
				IEnumerable<XmlReader> enumerable = list.AsEnumerable<XmlReader>();
				EntityUtil.CheckArgumentEmpty<XmlReader>(ref enumerable, new Func<string, string>(Strings.StoreItemCollectionMustHaveOneArtifact), "filePaths");
				this.Init(list, metadataArtifactLoader.GetPaths(DataSpace.SSpace), true, null, out this._providerManifest, out this._providerFactory, out this._providerInvariantName, out this._providerManifestToken, out this._cachedCTypeFunction);
			}
			finally
			{
				if (list != null)
				{
					Helper.DisposeXmlReaders(list);
				}
			}
		}

		// Token: 0x0600317E RID: 12670 RVA: 0x000ED018 File Offset: 0x000EB218
		private IList<EdmSchemaError> Init(IEnumerable<XmlReader> xmlReaders, IEnumerable<string> filePaths, bool throwOnError, IDbDependencyResolver resolver, out DbProviderManifest providerManifest, out DbProviderFactory providerFactory, out string providerInvariantName, out string providerManifestToken, out Memoizer<EdmFunction, EdmFunction> cachedCTypeFunction)
		{
			cachedCTypeFunction = new Memoizer<EdmFunction, EdmFunction>(new Func<EdmFunction, EdmFunction>(StoreItemCollection.ConvertFunctionSignatureToCType), null);
			StoreItemCollection.Loader loader = new StoreItemCollection.Loader(xmlReaders, filePaths, throwOnError, resolver);
			providerFactory = loader.ProviderFactory;
			providerManifest = loader.ProviderManifest;
			providerManifestToken = loader.ProviderManifestToken;
			providerInvariantName = loader.ProviderInvariantName;
			if (!loader.HasNonWarningErrors)
			{
				this.LoadProviderManifest(loader.ProviderManifest);
				List<EdmSchemaError> list = EdmItemCollection.LoadItems(this._providerManifest, loader.Schemas, this);
				foreach (EdmSchemaError item in list)
				{
					loader.Errors.Add(item);
				}
				if (throwOnError && list.Count != 0)
				{
					loader.ThrowOnNonWarningErrors();
				}
			}
			return loader.Errors;
		}

		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x0600317F RID: 12671 RVA: 0x000ED0F0 File Offset: 0x000EB2F0
		internal QueryCacheManager QueryCacheManager
		{
			get
			{
				return this._queryCacheManager;
			}
		}

		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x06003180 RID: 12672 RVA: 0x000ED0F8 File Offset: 0x000EB2F8
		public virtual DbProviderFactory ProviderFactory
		{
			get
			{
				return this._providerFactory;
			}
		}

		// Token: 0x1700076A RID: 1898
		// (get) Token: 0x06003181 RID: 12673 RVA: 0x000ED100 File Offset: 0x000EB300
		public virtual DbProviderManifest ProviderManifest
		{
			get
			{
				return this._providerManifest;
			}
		}

		// Token: 0x1700076B RID: 1899
		// (get) Token: 0x06003182 RID: 12674 RVA: 0x000ED108 File Offset: 0x000EB308
		public virtual string ProviderManifestToken
		{
			get
			{
				return this._providerManifestToken;
			}
		}

		// Token: 0x1700076C RID: 1900
		// (get) Token: 0x06003183 RID: 12675 RVA: 0x000ED110 File Offset: 0x000EB310
		public virtual string ProviderInvariantName
		{
			get
			{
				return this._providerInvariantName;
			}
		}

		// Token: 0x1700076D RID: 1901
		// (get) Token: 0x06003184 RID: 12676 RVA: 0x000ED118 File Offset: 0x000EB318
		// (set) Token: 0x06003185 RID: 12677 RVA: 0x000ED120 File Offset: 0x000EB320
		public double StoreSchemaVersion
		{
			get
			{
				return this._schemaVersion;
			}
			internal set
			{
				this._schemaVersion = value;
			}
		}

		// Token: 0x06003186 RID: 12678 RVA: 0x000ED129 File Offset: 0x000EB329
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		public virtual ReadOnlyCollection<PrimitiveType> GetPrimitiveTypes()
		{
			return this._primitiveTypeMaps.GetTypes();
		}

		// Token: 0x06003187 RID: 12679 RVA: 0x000ED138 File Offset: 0x000EB338
		internal override PrimitiveType GetMappedPrimitiveType(PrimitiveTypeKind primitiveTypeKind)
		{
			PrimitiveType result = null;
			this._primitiveTypeMaps.TryGetType(primitiveTypeKind, null, out result);
			return result;
		}

		// Token: 0x06003188 RID: 12680 RVA: 0x000ED158 File Offset: 0x000EB358
		private void LoadProviderManifest(DbProviderManifest storeManifest)
		{
			foreach (PrimitiveType primitiveType in storeManifest.GetStoreTypes())
			{
				base.AddInternal(primitiveType);
				this._primitiveTypeMaps.Add(primitiveType);
			}
			foreach (EdmFunction item in storeManifest.GetStoreFunctions())
			{
				base.AddInternal(item);
			}
		}

		// Token: 0x06003189 RID: 12681 RVA: 0x000ED1F0 File Offset: 0x000EB3F0
		internal ReadOnlyCollection<EdmFunction> GetCTypeFunctions(string functionName, bool ignoreCase)
		{
			ReadOnlyCollection<EdmFunction> readOnlyCollection;
			if (!base.FunctionLookUpTable.TryGetValue(functionName, out readOnlyCollection))
			{
				return Helper.EmptyEdmFunctionReadOnlyCollection;
			}
			readOnlyCollection = this.ConvertToCTypeFunctions(readOnlyCollection);
			if (ignoreCase)
			{
				return readOnlyCollection;
			}
			return ItemCollection.GetCaseSensitiveFunctions(readOnlyCollection, functionName);
		}

		// Token: 0x0600318A RID: 12682 RVA: 0x000ED228 File Offset: 0x000EB428
		private ReadOnlyCollection<EdmFunction> ConvertToCTypeFunctions(ReadOnlyCollection<EdmFunction> functionOverloads)
		{
			List<EdmFunction> list = new List<EdmFunction>();
			foreach (EdmFunction sTypeFunction in functionOverloads)
			{
				list.Add(this.ConvertToCTypeFunction(sTypeFunction));
			}
			return new ReadOnlyCollection<EdmFunction>(list);
		}

		// Token: 0x0600318B RID: 12683 RVA: 0x000ED284 File Offset: 0x000EB484
		internal EdmFunction ConvertToCTypeFunction(EdmFunction sTypeFunction)
		{
			return this._cachedCTypeFunction.Evaluate(sTypeFunction);
		}

		// Token: 0x0600318C RID: 12684 RVA: 0x000ED294 File Offset: 0x000EB494
		internal static EdmFunction ConvertFunctionSignatureToCType(EdmFunction sTypeFunction)
		{
			if (sTypeFunction.IsFromProviderManifest)
			{
				return sTypeFunction;
			}
			FunctionParameter functionParameter = null;
			if (sTypeFunction.ReturnParameter != null)
			{
				TypeUsage typeUsage = MetadataHelper.ConvertStoreTypeUsageToEdmTypeUsage(sTypeFunction.ReturnParameter.TypeUsage);
				functionParameter = new FunctionParameter(sTypeFunction.ReturnParameter.Name, typeUsage, sTypeFunction.ReturnParameter.GetParameterMode());
			}
			List<FunctionParameter> list = new List<FunctionParameter>();
			if (sTypeFunction.Parameters.Count > 0)
			{
				foreach (FunctionParameter functionParameter2 in sTypeFunction.Parameters)
				{
					TypeUsage typeUsage2 = MetadataHelper.ConvertStoreTypeUsageToEdmTypeUsage(functionParameter2.TypeUsage);
					FunctionParameter item = new FunctionParameter(functionParameter2.Name, typeUsage2, functionParameter2.GetParameterMode());
					list.Add(item);
				}
			}
			FunctionParameter[] returnParameters = (functionParameter == null) ? new FunctionParameter[0] : new FunctionParameter[]
			{
				functionParameter
			};
			EdmFunction edmFunction = new EdmFunction(sTypeFunction.Name, sTypeFunction.NamespaceName, DataSpace.CSpace, new EdmFunctionPayload
			{
				Schema = sTypeFunction.Schema,
				StoreFunctionName = sTypeFunction.StoreFunctionNameAttribute,
				CommandText = sTypeFunction.CommandTextAttribute,
				IsAggregate = new bool?(sTypeFunction.AggregateAttribute),
				IsBuiltIn = new bool?(sTypeFunction.BuiltInAttribute),
				IsNiladic = new bool?(sTypeFunction.NiladicFunctionAttribute),
				IsComposable = new bool?(sTypeFunction.IsComposableAttribute),
				IsFromProviderManifest = new bool?(sTypeFunction.IsFromProviderManifest),
				IsCachedStoreFunction = new bool?(true),
				IsFunctionImport = new bool?(sTypeFunction.IsFunctionImport),
				ReturnParameters = returnParameters,
				Parameters = list.ToArray(),
				ParameterTypeSemantics = new ParameterTypeSemantics?(sTypeFunction.ParameterTypeSemanticsAttribute)
			});
			edmFunction.SetReadOnly();
			return edmFunction;
		}

		// Token: 0x0600318D RID: 12685 RVA: 0x000ED46C File Offset: 0x000EB66C
		public static StoreItemCollection Create(IEnumerable<XmlReader> xmlReaders, ReadOnlyCollection<string> filePaths, IDbDependencyResolver resolver, out IList<EdmSchemaError> errors)
		{
			Check.NotNull<IEnumerable<XmlReader>>(xmlReaders, "xmlReaders");
			EntityUtil.CheckArgumentContainsNull<XmlReader>(ref xmlReaders, "xmlReaders");
			EntityUtil.CheckArgumentEmpty<XmlReader>(ref xmlReaders, new Func<string, string>(Strings.StoreItemCollectionMustHaveOneArtifact), "xmlReaders");
			StoreItemCollection result = new StoreItemCollection(xmlReaders, filePaths, resolver, ref errors);
			if (errors == null || errors.Count <= 0)
			{
				return result;
			}
			return null;
		}

		// Token: 0x040012B0 RID: 4784
		private double _schemaVersion;

		// Token: 0x040012B1 RID: 4785
		private readonly CacheForPrimitiveTypes _primitiveTypeMaps = new CacheForPrimitiveTypes();

		// Token: 0x040012B2 RID: 4786
		private readonly Memoizer<EdmFunction, EdmFunction> _cachedCTypeFunction;

		// Token: 0x040012B3 RID: 4787
		private readonly DbProviderManifest _providerManifest;

		// Token: 0x040012B4 RID: 4788
		private readonly string _providerInvariantName;

		// Token: 0x040012B5 RID: 4789
		private readonly string _providerManifestToken;

		// Token: 0x040012B6 RID: 4790
		private readonly DbProviderFactory _providerFactory;

		// Token: 0x040012B7 RID: 4791
		private readonly QueryCacheManager _queryCacheManager = QueryCacheManager.Create();

		// Token: 0x02000523 RID: 1315
		private class Loader
		{
			// Token: 0x0600318E RID: 12686 RVA: 0x000ED4C6 File Offset: 0x000EB6C6
			public Loader(IEnumerable<XmlReader> xmlReaders, IEnumerable<string> sourceFilePaths, bool throwOnError, IDbDependencyResolver resolver)
			{
				this._throwOnError = throwOnError;
				this._resolver = ((resolver == null) ? DbConfiguration.DependencyResolver : new CompositeResolver<IDbDependencyResolver, IDbDependencyResolver>(resolver, DbConfiguration.DependencyResolver));
				this.LoadItems(xmlReaders, sourceFilePaths);
			}

			// Token: 0x1700076E RID: 1902
			// (get) Token: 0x0600318F RID: 12687 RVA: 0x000ED4FA File Offset: 0x000EB6FA
			public IList<EdmSchemaError> Errors
			{
				get
				{
					return this._errors;
				}
			}

			// Token: 0x1700076F RID: 1903
			// (get) Token: 0x06003190 RID: 12688 RVA: 0x000ED502 File Offset: 0x000EB702
			public IList<Schema> Schemas
			{
				get
				{
					return this._schemas;
				}
			}

			// Token: 0x17000770 RID: 1904
			// (get) Token: 0x06003191 RID: 12689 RVA: 0x000ED50A File Offset: 0x000EB70A
			public DbProviderManifest ProviderManifest
			{
				get
				{
					return this._providerManifest;
				}
			}

			// Token: 0x17000771 RID: 1905
			// (get) Token: 0x06003192 RID: 12690 RVA: 0x000ED512 File Offset: 0x000EB712
			public DbProviderFactory ProviderFactory
			{
				get
				{
					return this._providerFactory;
				}
			}

			// Token: 0x17000772 RID: 1906
			// (get) Token: 0x06003193 RID: 12691 RVA: 0x000ED51A File Offset: 0x000EB71A
			public string ProviderManifestToken
			{
				get
				{
					return this._providerManifestToken;
				}
			}

			// Token: 0x17000773 RID: 1907
			// (get) Token: 0x06003194 RID: 12692 RVA: 0x000ED522 File Offset: 0x000EB722
			public string ProviderInvariantName
			{
				get
				{
					return this._provider;
				}
			}

			// Token: 0x17000774 RID: 1908
			// (get) Token: 0x06003195 RID: 12693 RVA: 0x000ED52A File Offset: 0x000EB72A
			public bool HasNonWarningErrors
			{
				get
				{
					return !MetadataHelper.CheckIfAllErrorsAreWarnings(this._errors);
				}
			}

			// Token: 0x06003196 RID: 12694 RVA: 0x000ED53C File Offset: 0x000EB73C
			private void LoadItems(IEnumerable<XmlReader> xmlReaders, IEnumerable<string> sourceFilePaths)
			{
				this._errors = SchemaManager.ParseAndValidate(xmlReaders, sourceFilePaths, SchemaDataModelOption.ProviderDataModel, new AttributeValueNotification(this.OnProviderNotification), new AttributeValueNotification(this.OnProviderManifestTokenNotification), new ProviderManifestNeeded(this.OnProviderManifestNeeded), out this._schemas);
				if (this._throwOnError)
				{
					this.ThrowOnNonWarningErrors();
				}
			}

			// Token: 0x06003197 RID: 12695 RVA: 0x000ED58F File Offset: 0x000EB78F
			internal void ThrowOnNonWarningErrors()
			{
				if (!MetadataHelper.CheckIfAllErrorsAreWarnings(this._errors))
				{
					throw EntityUtil.InvalidSchemaEncountered(Helper.CombineErrorMessage(this._errors));
				}
			}

			// Token: 0x06003198 RID: 12696 RVA: 0x000ED5B0 File Offset: 0x000EB7B0
			private void OnProviderNotification(string provider, Action<string, ErrorCode, EdmSchemaErrorSeverity> addError)
			{
				string provider2 = this._provider;
				if (this._provider == null)
				{
					this._provider = provider;
					this.InitializeProviderManifest(addError);
					return;
				}
				if (this._provider == provider)
				{
					return;
				}
				addError(Strings.AllArtifactsMustTargetSameProvider_InvariantName(provider2, this._provider), ErrorCode.InconsistentProvider, EdmSchemaErrorSeverity.Error);
			}

			// Token: 0x06003199 RID: 12697 RVA: 0x000ED604 File Offset: 0x000EB804
			private void InitializeProviderManifest(Action<string, ErrorCode, EdmSchemaErrorSeverity> addError)
			{
				if (this._providerManifest == null && this._providerManifestToken != null && this._provider != null)
				{
					DbProviderFactory providerFactory = null;
					try
					{
						providerFactory = DbConfiguration.DependencyResolver.GetService(this._provider);
					}
					catch (ArgumentException ex)
					{
						addError(ex.Message, ErrorCode.InvalidProvider, EdmSchemaErrorSeverity.Error);
						return;
					}
					try
					{
						DbProviderServices service = this._resolver.GetService(this._provider);
						this._providerManifest = service.GetProviderManifest(this._providerManifestToken);
						this._providerFactory = providerFactory;
						if (this._providerManifest is EdmProviderManifest)
						{
							if (this._throwOnError)
							{
								throw new NotSupportedException(Strings.OnlyStoreConnectionsSupported);
							}
							addError(Strings.OnlyStoreConnectionsSupported, ErrorCode.InvalidProvider, EdmSchemaErrorSeverity.Error);
						}
					}
					catch (ProviderIncompatibleException provEx)
					{
						if (this._throwOnError)
						{
							throw;
						}
						StoreItemCollection.Loader.AddProviderIncompatibleError(provEx, addError);
					}
				}
			}

			// Token: 0x0600319A RID: 12698 RVA: 0x000ED6EC File Offset: 0x000EB8EC
			private void OnProviderManifestTokenNotification(string token, Action<string, ErrorCode, EdmSchemaErrorSeverity> addError)
			{
				if (this._providerManifestToken == null)
				{
					this._providerManifestToken = token;
					this.InitializeProviderManifest(addError);
					return;
				}
				if (this._providerManifestToken != token)
				{
					addError(Strings.AllArtifactsMustTargetSameProvider_ManifestToken(token, this._providerManifestToken), ErrorCode.ProviderManifestTokenMismatch, EdmSchemaErrorSeverity.Error);
				}
			}

			// Token: 0x0600319B RID: 12699 RVA: 0x000ED72B File Offset: 0x000EB92B
			private DbProviderManifest OnProviderManifestNeeded(Action<string, ErrorCode, EdmSchemaErrorSeverity> addError)
			{
				if (this._providerManifest == null)
				{
					addError(Strings.ProviderManifestTokenNotFound, ErrorCode.ProviderManifestTokenNotFound, EdmSchemaErrorSeverity.Error);
				}
				return this._providerManifest;
			}

			// Token: 0x0600319C RID: 12700 RVA: 0x000ED74C File Offset: 0x000EB94C
			private static void AddProviderIncompatibleError(ProviderIncompatibleException provEx, Action<string, ErrorCode, EdmSchemaErrorSeverity> addError)
			{
				StringBuilder stringBuilder = new StringBuilder(provEx.Message);
				if (provEx.InnerException != null && !string.IsNullOrEmpty(provEx.InnerException.Message))
				{
					stringBuilder.AppendFormat(" {0}", provEx.InnerException.Message);
				}
				addError(stringBuilder.ToString(), ErrorCode.FailedToRetrieveProviderManifest, EdmSchemaErrorSeverity.Error);
			}

			// Token: 0x040012B8 RID: 4792
			private string _provider;

			// Token: 0x040012B9 RID: 4793
			private string _providerManifestToken;

			// Token: 0x040012BA RID: 4794
			private DbProviderManifest _providerManifest;

			// Token: 0x040012BB RID: 4795
			private DbProviderFactory _providerFactory;

			// Token: 0x040012BC RID: 4796
			private IList<EdmSchemaError> _errors;

			// Token: 0x040012BD RID: 4797
			private IList<Schema> _schemas;

			// Token: 0x040012BE RID: 4798
			private readonly bool _throwOnError;

			// Token: 0x040012BF RID: 4799
			private readonly IDbDependencyResolver _resolver;
		}
	}
}
