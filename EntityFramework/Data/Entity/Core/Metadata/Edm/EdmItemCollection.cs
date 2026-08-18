using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.Utils;
using System.Data.Entity.Core.Metadata.Edm.Provider;
using System.Data.Entity.Core.Objects.ELinq;
using System.Data.Entity.Core.SchemaObjectModel;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004BA RID: 1210
	public sealed class EdmItemCollection : ItemCollection
	{
		// Token: 0x06002C84 RID: 11396 RVA: 0x000D9346 File Offset: 0x000D7546
		internal EdmItemCollection(IEnumerable<XmlReader> xmlReaders, IEnumerable<string> filePaths, bool skipInitialization = false) : base(DataSpace.CSpace)
		{
			if (!skipInitialization)
			{
				this.Init(xmlReaders, filePaths, true);
			}
		}

		// Token: 0x06002C85 RID: 11397 RVA: 0x000D9374 File Offset: 0x000D7574
		public EdmItemCollection(IEnumerable<XmlReader> xmlReaders) : base(DataSpace.CSpace)
		{
			Check.NotNull<IEnumerable<XmlReader>>(xmlReaders, "xmlReaders");
			EntityUtil.CheckArgumentContainsNull<XmlReader>(ref xmlReaders, "xmlReaders");
			MetadataArtifactLoader metadataArtifactLoader = MetadataArtifactLoader.CreateCompositeFromXmlReaders(xmlReaders);
			this.Init(metadataArtifactLoader.GetReaders(), metadataArtifactLoader.GetPaths(), true);
		}

		// Token: 0x06002C86 RID: 11398 RVA: 0x000D93D4 File Offset: 0x000D75D4
		public EdmItemCollection(EdmModel model) : base(DataSpace.CSpace)
		{
			Check.NotNull<EdmModel>(model, "model");
			this.Init();
			this._edmVersion = model.SchemaVersion;
			model.Validate();
			foreach (GlobalItem globalItem in model.GlobalItems)
			{
				globalItem.SetReadOnly();
				base.AddInternal(globalItem);
			}
		}

		// Token: 0x06002C87 RID: 11399 RVA: 0x000D9468 File Offset: 0x000D7668
		public EdmItemCollection(params string[] filePaths) : base(DataSpace.CSpace)
		{
			Check.NotNull<string[]>(filePaths, "filePaths");
			List<XmlReader> list = null;
			try
			{
				MetadataArtifactLoader metadataArtifactLoader = MetadataArtifactLoader.CreateCompositeFromFilePaths(filePaths, ".csdl");
				list = metadataArtifactLoader.CreateReaders(DataSpace.CSpace);
				this.Init(list, metadataArtifactLoader.GetPaths(DataSpace.CSpace), true);
			}
			finally
			{
				if (list != null)
				{
					Helper.DisposeXmlReaders(list);
				}
			}
		}

		// Token: 0x06002C88 RID: 11400 RVA: 0x000D94E4 File Offset: 0x000D76E4
		private EdmItemCollection(IEnumerable<XmlReader> xmlReaders, ReadOnlyCollection<string> filePaths, out IList<EdmSchemaError> errors) : base(DataSpace.CSpace)
		{
			errors = this.Init(xmlReaders, filePaths, false);
		}

		// Token: 0x06002C89 RID: 11401 RVA: 0x000D950E File Offset: 0x000D770E
		private void Init()
		{
			this.LoadEdmPrimitiveTypesAndFunctions();
		}

		// Token: 0x06002C8A RID: 11402 RVA: 0x000D9518 File Offset: 0x000D7718
		private IList<EdmSchemaError> Init(IEnumerable<XmlReader> xmlReaders, IEnumerable<string> filePaths, bool throwOnError)
		{
			this.Init();
			return EdmItemCollection.LoadItems(xmlReaders, filePaths, SchemaDataModelOption.EntityDataModel, MetadataItem.EdmProviderManifest, this, throwOnError);
		}

		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x06002C8B RID: 11403 RVA: 0x000D953C File Offset: 0x000D773C
		// (set) Token: 0x06002C8C RID: 11404 RVA: 0x000D9544 File Offset: 0x000D7744
		public double EdmVersion
		{
			get
			{
				return this._edmVersion;
			}
			internal set
			{
				this._edmVersion = value;
			}
		}

		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x06002C8D RID: 11405 RVA: 0x000D954D File Offset: 0x000D774D
		internal OcAssemblyCache ConventionalOcCache
		{
			get
			{
				return this._conventionalOcCache;
			}
		}

		// Token: 0x06002C8E RID: 11406 RVA: 0x000D9558 File Offset: 0x000D7758
		internal InitializerMetadata GetCanonicalInitializerMetadata(InitializerMetadata metadata)
		{
			if (this._getCanonicalInitializerMetadataMemoizer == null)
			{
				Interlocked.CompareExchange<Memoizer<InitializerMetadata, InitializerMetadata>>(ref this._getCanonicalInitializerMetadataMemoizer, new Memoizer<InitializerMetadata, InitializerMetadata>((InitializerMetadata m) => m, EqualityComparer<InitializerMetadata>.Default), null);
			}
			return this._getCanonicalInitializerMetadataMemoizer.Evaluate(metadata);
		}

		// Token: 0x06002C8F RID: 11407 RVA: 0x000D95B0 File Offset: 0x000D77B0
		internal static bool IsSystemNamespace(DbProviderManifest manifest, string namespaceName)
		{
			if (manifest == MetadataItem.EdmProviderManifest)
			{
				return namespaceName == "Transient" || namespaceName == "Edm" || namespaceName == "System";
			}
			return namespaceName == "Transient" || namespaceName == "Edm" || namespaceName == "System" || (manifest != null && namespaceName == manifest.NamespaceName);
		}

		// Token: 0x06002C90 RID: 11408 RVA: 0x000D9628 File Offset: 0x000D7828
		internal static IList<EdmSchemaError> LoadItems(IEnumerable<XmlReader> xmlReaders, IEnumerable<string> sourceFilePaths, SchemaDataModelOption dataModelOption, DbProviderManifest providerManifest, ItemCollection itemCollection, bool throwOnError)
		{
			IList<Schema> somSchemas = null;
			IList<EdmSchemaError> list = SchemaManager.ParseAndValidate(xmlReaders, sourceFilePaths, dataModelOption, providerManifest, out somSchemas);
			if (MetadataHelper.CheckIfAllErrorsAreWarnings(list))
			{
				List<EdmSchemaError> list2 = EdmItemCollection.LoadItems(providerManifest, somSchemas, itemCollection);
				foreach (EdmSchemaError item in list2)
				{
					list.Add(item);
				}
			}
			if (!MetadataHelper.CheckIfAllErrorsAreWarnings(list) && throwOnError)
			{
				throw EntityUtil.InvalidSchemaEncountered(Helper.CombineErrorMessage(list));
			}
			return list;
		}

		// Token: 0x06002C91 RID: 11409 RVA: 0x000D96C8 File Offset: 0x000D78C8
		internal static List<EdmSchemaError> LoadItems(DbProviderManifest manifest, IList<Schema> somSchemas, ItemCollection itemCollection)
		{
			List<EdmSchemaError> list = new List<EdmSchemaError>();
			IEnumerable<GlobalItem> enumerable = EdmItemCollection.LoadSomSchema(somSchemas, manifest, itemCollection);
			List<string> list2 = new List<string>();
			foreach (GlobalItem globalItem in enumerable)
			{
				if (globalItem.BuiltInTypeKind == BuiltInTypeKind.EdmFunction && globalItem.DataSpace == DataSpace.SSpace)
				{
					EdmFunction edmFunction = (EdmFunction)globalItem;
					StringBuilder stringBuilder = new StringBuilder();
					EdmFunction.BuildIdentity<FunctionParameter>(stringBuilder, edmFunction.FullName, edmFunction.Parameters, (FunctionParameter param) => MetadataHelper.ConvertStoreTypeUsageToEdmTypeUsage(param.TypeUsage), (FunctionParameter param) => param.Mode);
					string text = stringBuilder.ToString();
					if (list2.Contains(text))
					{
						list.Add(new EdmSchemaError(Strings.DuplicatedFunctionoverloads(edmFunction.FullName, text.Substring(edmFunction.FullName.Length)).Trim(), 174, EdmSchemaErrorSeverity.Error));
						continue;
					}
					list2.Add(text);
				}
				globalItem.SetReadOnly();
				itemCollection.AddInternal(globalItem);
			}
			return list;
		}

		// Token: 0x06002C92 RID: 11410 RVA: 0x000D9800 File Offset: 0x000D7A00
		internal static IEnumerable<GlobalItem> LoadSomSchema(IList<Schema> somSchemas, DbProviderManifest providerManifest, ItemCollection itemCollection)
		{
			return Converter.ConvertSchema(somSchemas, providerManifest, itemCollection);
		}

		// Token: 0x06002C93 RID: 11411 RVA: 0x000D9817 File Offset: 0x000D7A17
		public ReadOnlyCollection<PrimitiveType> GetPrimitiveTypes()
		{
			return this._primitiveTypeMaps.GetTypes();
		}

		// Token: 0x06002C94 RID: 11412 RVA: 0x000D9830 File Offset: 0x000D7A30
		public ReadOnlyCollection<PrimitiveType> GetPrimitiveTypes(double edmVersion)
		{
			if (edmVersion == 1.0 || edmVersion == 1.1 || edmVersion == 2.0)
			{
				return new ReadOnlyCollection<PrimitiveType>((from type in this._primitiveTypeMaps.GetTypes()
				where !Helper.IsSpatialType(type)
				select type).ToList<PrimitiveType>());
			}
			if (edmVersion == 3.0)
			{
				return this._primitiveTypeMaps.GetTypes();
			}
			throw new ArgumentException(Strings.InvalidEDMVersion(edmVersion.ToString(CultureInfo.CurrentCulture)));
		}

		// Token: 0x06002C95 RID: 11413 RVA: 0x000D98C8 File Offset: 0x000D7AC8
		internal override PrimitiveType GetMappedPrimitiveType(PrimitiveTypeKind primitiveTypeKind)
		{
			PrimitiveType result = null;
			this._primitiveTypeMaps.TryGetType(primitiveTypeKind, null, out result);
			return result;
		}

		// Token: 0x06002C96 RID: 11414 RVA: 0x000D98E8 File Offset: 0x000D7AE8
		private void LoadEdmPrimitiveTypesAndFunctions()
		{
			EdmProviderManifest instance = EdmProviderManifest.Instance;
			ReadOnlyCollection<PrimitiveType> storeTypes = instance.GetStoreTypes();
			for (int i = 0; i < storeTypes.Count; i++)
			{
				base.AddInternal(storeTypes[i]);
				this._primitiveTypeMaps.Add(storeTypes[i]);
			}
			ReadOnlyCollection<EdmFunction> storeFunctions = instance.GetStoreFunctions();
			for (int j = 0; j < storeFunctions.Count; j++)
			{
				base.AddInternal(storeFunctions[j]);
			}
		}

		// Token: 0x06002C97 RID: 11415 RVA: 0x000D995C File Offset: 0x000D7B5C
		internal DbLambda GetGeneratedFunctionDefinition(EdmFunction function)
		{
			if (this._getGeneratedFunctionDefinitionsMemoizer == null)
			{
				Interlocked.CompareExchange<Memoizer<EdmFunction, DbLambda>>(ref this._getGeneratedFunctionDefinitionsMemoizer, new Memoizer<EdmFunction, DbLambda>(new Func<EdmFunction, DbLambda>(this.GenerateFunctionDefinition), null), null);
			}
			return this._getGeneratedFunctionDefinitionsMemoizer.Evaluate(function);
		}

		// Token: 0x06002C98 RID: 11416 RVA: 0x000D9994 File Offset: 0x000D7B94
		internal DbLambda GenerateFunctionDefinition(EdmFunction function)
		{
			if (!function.HasUserDefinedBody)
			{
				throw new InvalidOperationException(Strings.Cqt_UDF_FunctionHasNoDefinition(function.Identity));
			}
			DbLambda dbLambda = ExternalCalls.CompileFunctionDefinition(function.CommandTextAttribute, function.Parameters, this);
			if (!TypeSemantics.IsStructurallyEqual(function.ReturnParameter.TypeUsage, dbLambda.Body.ResultType))
			{
				throw new InvalidOperationException(Strings.Cqt_UDF_FunctionDefinitionResultTypeMismatch(function.ReturnParameter.TypeUsage.ToString(), function.FullName, dbLambda.Body.ResultType.ToString()));
			}
			return dbLambda;
		}

		// Token: 0x06002C99 RID: 11417 RVA: 0x000D9A1C File Offset: 0x000D7C1C
		public static EdmItemCollection Create(IEnumerable<XmlReader> xmlReaders, ReadOnlyCollection<string> filePaths, out IList<EdmSchemaError> errors)
		{
			Check.NotNull<IEnumerable<XmlReader>>(xmlReaders, "xmlReaders");
			EntityUtil.CheckArgumentContainsNull<XmlReader>(ref xmlReaders, "xmlReaders");
			EdmItemCollection result = new EdmItemCollection(xmlReaders, filePaths, ref errors);
			if (errors == null || errors.Count <= 0)
			{
				return result;
			}
			return null;
		}

		// Token: 0x0400106A RID: 4202
		private readonly CacheForPrimitiveTypes _primitiveTypeMaps = new CacheForPrimitiveTypes();

		// Token: 0x0400106B RID: 4203
		private double _edmVersion;

		// Token: 0x0400106C RID: 4204
		private Memoizer<InitializerMetadata, InitializerMetadata> _getCanonicalInitializerMetadataMemoizer;

		// Token: 0x0400106D RID: 4205
		private Memoizer<EdmFunction, DbLambda> _getGeneratedFunctionDefinitionsMemoizer;

		// Token: 0x0400106E RID: 4206
		private readonly OcAssemblyCache _conventionalOcCache = new OcAssemblyCache();
	}
}
