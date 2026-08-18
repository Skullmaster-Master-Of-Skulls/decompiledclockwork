using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Data.Common.QueryCache;
using System.Data.Common.Utils;
using System.Data.Entity;
using System.Data.EntityModel.SchemaObjectModel;
using System.Linq;
using System.Text;
using System.Xml;

namespace System.Data.Metadata.Edm
{
	// Token: 0x02000208 RID: 520
	[CLSCompliant(false)]
	public sealed class StoreItemCollection : ItemCollection
	{
		// Token: 0x06002270 RID: 8816 RVA: 0x00079230 File Offset: 0x00077430
		internal StoreItemCollection(DbProviderFactory factory, DbProviderManifest manifest, string providerManifestToken) : base(DataSpace.SSpace)
		{
			this._providerFactory = factory;
			this._providerManifest = manifest;
			this._providerManifestToken = providerManifestToken;
			this._cachedCTypeFunction = new Memoizer<EdmFunction, EdmFunction>(new Func<EdmFunction, EdmFunction>(this.ConvertFunctionSignatureToCType), null);
			this.LoadProviderManifest(this._providerManifest, true);
		}

		// Token: 0x06002271 RID: 8817 RVA: 0x00079294 File Offset: 0x00077494
		internal StoreItemCollection(IEnumerable<XmlReader> xmlReaders, ReadOnlyCollection<string> filePaths, out IList<EdmSchemaError> errors) : base(DataSpace.SSpace)
		{
			EntityUtil.CheckArgumentNull<IEnumerable<XmlReader>>(xmlReaders, "xmlReaders");
			EntityUtil.CheckArgumentContainsNull<XmlReader>(ref xmlReaders, "xmlReaders");
			EntityUtil.CheckArgumentEmpty<XmlReader>(ref xmlReaders, new Func<string, string>(Strings.StoreItemCollectionMustHaveOneArtifact), "xmlReader");
			errors = this.Init(xmlReaders, filePaths, false, out this._providerManifest, out this._providerFactory, out this._providerManifestToken, out this._cachedCTypeFunction);
		}

		// Token: 0x06002272 RID: 8818 RVA: 0x00079314 File Offset: 0x00077514
		internal StoreItemCollection(IEnumerable<XmlReader> xmlReaders, IEnumerable<string> filePaths) : base(DataSpace.SSpace)
		{
			EntityUtil.CheckArgumentNull<IEnumerable<string>>(filePaths, "filePaths");
			EntityUtil.CheckArgumentEmpty<XmlReader>(ref xmlReaders, new Func<string, string>(Strings.StoreItemCollectionMustHaveOneArtifact), "xmlReader");
			this.Init(xmlReaders, filePaths, true, out this._providerManifest, out this._providerFactory, out this._providerManifestToken, out this._cachedCTypeFunction);
		}

		// Token: 0x06002273 RID: 8819 RVA: 0x00079388 File Offset: 0x00077588
		public StoreItemCollection(IEnumerable<XmlReader> xmlReaders) : base(DataSpace.SSpace)
		{
			EntityUtil.CheckArgumentNull<IEnumerable<XmlReader>>(xmlReaders, "xmlReaders");
			EntityUtil.CheckArgumentEmpty<XmlReader>(ref xmlReaders, new Func<string, string>(Strings.StoreItemCollectionMustHaveOneArtifact), "xmlReader");
			MetadataArtifactLoader metadataArtifactLoader = MetadataArtifactLoader.CreateCompositeFromXmlReaders(xmlReaders);
			this.Init(metadataArtifactLoader.GetReaders(), metadataArtifactLoader.GetPaths(), true, out this._providerManifest, out this._providerFactory, out this._providerManifestToken, out this._cachedCTypeFunction);
		}

		// Token: 0x06002274 RID: 8820 RVA: 0x0007940C File Offset: 0x0007760C
		public StoreItemCollection(params string[] filePaths) : base(DataSpace.SSpace)
		{
			EntityUtil.CheckArgumentNull<string[]>(filePaths, "filePaths");
			IEnumerable<string> filePaths2 = filePaths;
			EntityUtil.CheckArgumentEmpty<string>(ref filePaths2, new Func<string, string>(Strings.StoreItemCollectionMustHaveOneArtifact), "filePaths");
			List<XmlReader> list = null;
			try
			{
				MetadataArtifactLoader metadataArtifactLoader = MetadataArtifactLoader.CreateCompositeFromFilePaths(filePaths2, ".ssdl");
				list = metadataArtifactLoader.CreateReaders(DataSpace.SSpace);
				IEnumerable<XmlReader> enumerable = list.AsEnumerable<XmlReader>();
				EntityUtil.CheckArgumentEmpty<XmlReader>(ref enumerable, new Func<string, string>(Strings.StoreItemCollectionMustHaveOneArtifact), "filePaths");
				this.Init(list, metadataArtifactLoader.GetPaths(DataSpace.SSpace), true, out this._providerManifest, out this._providerFactory, out this._providerManifestToken, out this._cachedCTypeFunction);
			}
			finally
			{
				if (list != null)
				{
					Helper.DisposeXmlReaders(list);
				}
			}
		}

		// Token: 0x06002275 RID: 8821 RVA: 0x000794DC File Offset: 0x000776DC
		private IList<EdmSchemaError> Init(IEnumerable<XmlReader> xmlReaders, IEnumerable<string> filePaths, bool throwOnError, out DbProviderManifest providerManifest, out DbProviderFactory providerFactory, out string providerManifestToken, out Memoizer<EdmFunction, EdmFunction> cachedCTypeFunction)
		{
			EntityUtil.CheckArgumentNull<IEnumerable<XmlReader>>(xmlReaders, "xmlReaders");
			cachedCTypeFunction = new Memoizer<EdmFunction, EdmFunction>(new Func<EdmFunction, EdmFunction>(this.ConvertFunctionSignatureToCType), null);
			StoreItemCollection.Loader loader = new StoreItemCollection.Loader(xmlReaders, filePaths, throwOnError);
			providerFactory = loader.ProviderFactory;
			providerManifest = loader.ProviderManifest;
			providerManifestToken = loader.ProviderManifestToken;
			if (!loader.HasNonWarningErrors)
			{
				this.LoadProviderManifest(loader.ProviderManifest, true);
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

		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x06002276 RID: 8822 RVA: 0x000795B4 File Offset: 0x000777B4
		internal QueryCacheManager QueryCacheManager
		{
			get
			{
				return this._queryCacheManager;
			}
		}

		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x06002277 RID: 8823 RVA: 0x000795BC File Offset: 0x000777BC
		internal DbProviderFactory StoreProviderFactory
		{
			get
			{
				return this._providerFactory;
			}
		}

		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x06002278 RID: 8824 RVA: 0x000795C4 File Offset: 0x000777C4
		internal DbProviderManifest StoreProviderManifest
		{
			get
			{
				return this._providerManifest;
			}
		}

		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x06002279 RID: 8825 RVA: 0x000795CC File Offset: 0x000777CC
		internal string StoreProviderManifestToken
		{
			get
			{
				return this._providerManifestToken;
			}
		}

		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x0600227A RID: 8826 RVA: 0x000795D4 File Offset: 0x000777D4
		// (set) Token: 0x0600227B RID: 8827 RVA: 0x000795DC File Offset: 0x000777DC
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

		// Token: 0x0600227C RID: 8828 RVA: 0x000795E5 File Offset: 0x000777E5
		public ReadOnlyCollection<PrimitiveType> GetPrimitiveTypes()
		{
			return this._primitiveTypeMaps.GetTypes();
		}

		// Token: 0x0600227D RID: 8829 RVA: 0x000795F4 File Offset: 0x000777F4
		internal override PrimitiveType GetMappedPrimitiveType(PrimitiveTypeKind primitiveTypeKind)
		{
			PrimitiveType result = null;
			this._primitiveTypeMaps.TryGetType(primitiveTypeKind, null, out result);
			return result;
		}

		// Token: 0x0600227E RID: 8830 RVA: 0x00079614 File Offset: 0x00077814
		private void LoadProviderManifest(DbProviderManifest storeManifest, bool checkForSystemNamespace)
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

		// Token: 0x0600227F RID: 8831 RVA: 0x000796AC File Offset: 0x000778AC
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

		// Token: 0x06002280 RID: 8832 RVA: 0x000796E4 File Offset: 0x000778E4
		private ReadOnlyCollection<EdmFunction> ConvertToCTypeFunctions(ReadOnlyCollection<EdmFunction> functionOverloads)
		{
			List<EdmFunction> list = new List<EdmFunction>();
			foreach (EdmFunction sTypeFunction in functionOverloads)
			{
				list.Add(this.ConvertToCTypeFunction(sTypeFunction));
			}
			return list.AsReadOnly();
		}

		// Token: 0x06002281 RID: 8833 RVA: 0x00079740 File Offset: 0x00077940
		internal EdmFunction ConvertToCTypeFunction(EdmFunction sTypeFunction)
		{
			return this._cachedCTypeFunction.Evaluate(sTypeFunction);
		}

		// Token: 0x06002282 RID: 8834 RVA: 0x00079750 File Offset: 0x00077950
		private EdmFunction ConvertFunctionSignatureToCType(EdmFunction sTypeFunction)
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
			FunctionParameter[] array;
			if (functionParameter != null)
			{
				(array = new FunctionParameter[1])[0] = functionParameter;
			}
			else
			{
				array = new FunctionParameter[0];
			}
			FunctionParameter[] returnParameters = array;
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

		// Token: 0x04000EF2 RID: 3826
		private double _schemaVersion;

		// Token: 0x04000EF3 RID: 3827
		private readonly CacheForPrimitiveTypes _primitiveTypeMaps = new CacheForPrimitiveTypes();

		// Token: 0x04000EF4 RID: 3828
		private readonly Memoizer<EdmFunction, EdmFunction> _cachedCTypeFunction;

		// Token: 0x04000EF5 RID: 3829
		private readonly DbProviderManifest _providerManifest;

		// Token: 0x04000EF6 RID: 3830
		private readonly string _providerManifestToken;

		// Token: 0x04000EF7 RID: 3831
		private readonly DbProviderFactory _providerFactory;

		// Token: 0x04000EF8 RID: 3832
		private readonly QueryCacheManager _queryCacheManager = QueryCacheManager.Create();

		// Token: 0x02000537 RID: 1335
		private class Loader
		{
			// Token: 0x06003E8F RID: 16015 RVA: 0x000E8A26 File Offset: 0x000E6C26
			public Loader(IEnumerable<XmlReader> xmlReaders, IEnumerable<string> sourceFilePaths, bool throwOnError)
			{
				this._throwOnError = throwOnError;
				this.LoadItems(xmlReaders, sourceFilePaths);
			}

			// Token: 0x17000B24 RID: 2852
			// (get) Token: 0x06003E90 RID: 16016 RVA: 0x000E8A3D File Offset: 0x000E6C3D
			public IList<EdmSchemaError> Errors
			{
				get
				{
					return this._errors;
				}
			}

			// Token: 0x17000B25 RID: 2853
			// (get) Token: 0x06003E91 RID: 16017 RVA: 0x000E8A45 File Offset: 0x000E6C45
			public IList<Schema> Schemas
			{
				get
				{
					return this._schemas;
				}
			}

			// Token: 0x17000B26 RID: 2854
			// (get) Token: 0x06003E92 RID: 16018 RVA: 0x000E8A4D File Offset: 0x000E6C4D
			public DbProviderManifest ProviderManifest
			{
				get
				{
					return this._providerManifest;
				}
			}

			// Token: 0x17000B27 RID: 2855
			// (get) Token: 0x06003E93 RID: 16019 RVA: 0x000E8A55 File Offset: 0x000E6C55
			public DbProviderFactory ProviderFactory
			{
				get
				{
					return this._providerFactory;
				}
			}

			// Token: 0x17000B28 RID: 2856
			// (get) Token: 0x06003E94 RID: 16020 RVA: 0x000E8A5D File Offset: 0x000E6C5D
			public string ProviderManifestToken
			{
				get
				{
					return this._providerManifestToken;
				}
			}

			// Token: 0x17000B29 RID: 2857
			// (get) Token: 0x06003E95 RID: 16021 RVA: 0x000E8A65 File Offset: 0x000E6C65
			public bool HasNonWarningErrors
			{
				get
				{
					return !MetadataHelper.CheckIfAllErrorsAreWarnings(this._errors);
				}
			}

			// Token: 0x06003E96 RID: 16022 RVA: 0x000E8A78 File Offset: 0x000E6C78
			private void LoadItems(IEnumerable<XmlReader> xmlReaders, IEnumerable<string> sourceFilePaths)
			{
				this._errors = SchemaManager.ParseAndValidate(xmlReaders, sourceFilePaths, SchemaDataModelOption.ProviderDataModel, new AttributeValueNotification(this.OnProviderNotification), new AttributeValueNotification(this.OnProviderManifestTokenNotification), new ProviderManifestNeeded(this.OnProviderManifestNeeded), out this._schemas);
				if (this._throwOnError)
				{
					this.ThrowOnNonWarningErrors();
				}
			}

			// Token: 0x06003E97 RID: 16023 RVA: 0x000E8ACB File Offset: 0x000E6CCB
			internal void ThrowOnNonWarningErrors()
			{
				if (!MetadataHelper.CheckIfAllErrorsAreWarnings(this._errors))
				{
					throw EntityUtil.InvalidSchemaEncountered(Helper.CombineErrorMessage(this._errors));
				}
			}

			// Token: 0x06003E98 RID: 16024 RVA: 0x000E8AEC File Offset: 0x000E6CEC
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

			// Token: 0x06003E99 RID: 16025 RVA: 0x000E8B40 File Offset: 0x000E6D40
			private void InitializeProviderManifest(Action<string, ErrorCode, EdmSchemaErrorSeverity> addError)
			{
				if (this._providerManifest == null && this._providerManifestToken != null && this._provider != null)
				{
					DbProviderFactory dbProviderFactory = null;
					try
					{
						dbProviderFactory = DbProviderServices.GetProviderFactory(this._provider);
					}
					catch (ArgumentException ex)
					{
						addError(ex.Message, ErrorCode.InvalidProvider, EdmSchemaErrorSeverity.Error);
						return;
					}
					try
					{
						DbProviderServices providerServices = DbProviderServices.GetProviderServices(dbProviderFactory);
						this._providerManifest = providerServices.GetProviderManifest(this._providerManifestToken);
						this._providerFactory = dbProviderFactory;
						if (this._providerManifest is EdmProviderManifest)
						{
							if (this._throwOnError)
							{
								throw EntityUtil.NotSupported(Strings.OnlyStoreConnectionsSupported);
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
						this.AddProviderIncompatibleError(provEx, addError);
					}
				}
			}

			// Token: 0x06003E9A RID: 16026 RVA: 0x000E8C18 File Offset: 0x000E6E18
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

			// Token: 0x06003E9B RID: 16027 RVA: 0x000E8C57 File Offset: 0x000E6E57
			private DbProviderManifest OnProviderManifestNeeded(Action<string, ErrorCode, EdmSchemaErrorSeverity> addError)
			{
				if (this._providerManifest == null)
				{
					addError(Strings.ProviderManifestTokenNotFound, ErrorCode.ProviderManifestTokenNotFound, EdmSchemaErrorSeverity.Error);
				}
				return this._providerManifest;
			}

			// Token: 0x06003E9C RID: 16028 RVA: 0x000E8C78 File Offset: 0x000E6E78
			private void AddProviderIncompatibleError(ProviderIncompatibleException provEx, Action<string, ErrorCode, EdmSchemaErrorSeverity> addError)
			{
				StringBuilder stringBuilder = new StringBuilder(provEx.Message);
				if (provEx.InnerException != null && !string.IsNullOrEmpty(provEx.InnerException.Message))
				{
					stringBuilder.AppendFormat(" {0}", provEx.InnerException.Message);
				}
				addError(stringBuilder.ToString(), ErrorCode.FailedToRetrieveProviderManifest, EdmSchemaErrorSeverity.Error);
			}

			// Token: 0x04001B90 RID: 7056
			private string _provider;

			// Token: 0x04001B91 RID: 7057
			private string _providerManifestToken;

			// Token: 0x04001B92 RID: 7058
			private DbProviderManifest _providerManifest;

			// Token: 0x04001B93 RID: 7059
			private DbProviderFactory _providerFactory;

			// Token: 0x04001B94 RID: 7060
			private IList<EdmSchemaError> _errors;

			// Token: 0x04001B95 RID: 7061
			private IList<Schema> _schemas;

			// Token: 0x04001B96 RID: 7062
			private bool _throwOnError;
		}
	}
}
