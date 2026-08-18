using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Data.Common.CommandTrees;
using System.Data.Common.Utils;
using System.Data.Entity;
using System.Data.EntityModel.SchemaObjectModel;
using System.Data.Mapping.ViewGeneration.Utils;
using System.Data.Objects.ELinq;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml;

namespace System.Data.Metadata.Edm
{
	// Token: 0x020001B9 RID: 441
	[CLSCompliant(false)]
	public sealed class EdmItemCollection : ItemCollection
	{
		// Token: 0x06001EE8 RID: 7912 RVA: 0x0006CF90 File Offset: 0x0006B190
		internal EdmItemCollection(IEnumerable<XmlReader> xmlReaders, ReadOnlyCollection<string> filePaths, out IList<EdmSchemaError> errors) : base(DataSpace.CSpace)
		{
			EntityUtil.CheckArgumentNull<IEnumerable<XmlReader>>(xmlReaders, "xmlReaders");
			EntityUtil.CheckArgumentContainsNull<XmlReader>(ref xmlReaders, "xmlReaders");
			errors = this.Init(xmlReaders, filePaths, false);
		}

		// Token: 0x06001EE9 RID: 7913 RVA: 0x0006CFDE File Offset: 0x0006B1DE
		internal EdmItemCollection(IList<Schema> schemas) : base(DataSpace.CSpace)
		{
			this.Init();
			EdmItemCollection.LoadItems(MetadataItem.EdmProviderManifest, schemas, this);
		}

		// Token: 0x06001EEA RID: 7914 RVA: 0x0006D010 File Offset: 0x0006B210
		internal EdmItemCollection(IEnumerable<XmlReader> xmlReaders, IEnumerable<string> filePaths) : base(DataSpace.CSpace)
		{
			this.Init(xmlReaders, filePaths, true);
		}

		// Token: 0x06001EEB RID: 7915 RVA: 0x0006D03C File Offset: 0x0006B23C
		public EdmItemCollection(IEnumerable<XmlReader> xmlReaders) : base(DataSpace.CSpace)
		{
			EntityUtil.CheckArgumentNull<IEnumerable<XmlReader>>(xmlReaders, "xmlReaders");
			EntityUtil.CheckArgumentContainsNull<XmlReader>(ref xmlReaders, "xmlReaders");
			MetadataArtifactLoader metadataArtifactLoader = MetadataArtifactLoader.CreateCompositeFromXmlReaders(xmlReaders);
			this.Init(metadataArtifactLoader.GetReaders(), metadataArtifactLoader.GetPaths(), true);
		}

		// Token: 0x06001EEC RID: 7916 RVA: 0x0006D09C File Offset: 0x0006B29C
		public EdmItemCollection(params string[] filePaths) : base(DataSpace.CSpace)
		{
			EntityUtil.CheckArgumentNull<string[]>(filePaths, "filePaths");
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

		// Token: 0x06001EED RID: 7917 RVA: 0x0006D118 File Offset: 0x0006B318
		private void Init()
		{
			this.LoadEdmPrimitiveTypesAndFunctions();
		}

		// Token: 0x06001EEE RID: 7918 RVA: 0x0006D120 File Offset: 0x0006B320
		private IList<EdmSchemaError> Init(IEnumerable<XmlReader> xmlReaders, IEnumerable<string> filePaths, bool throwOnError)
		{
			EntityUtil.CheckArgumentNull<IEnumerable<XmlReader>>(xmlReaders, "xmlReaders");
			this.Init();
			return EdmItemCollection.LoadItems(xmlReaders, filePaths, SchemaDataModelOption.EntityDataModel, MetadataItem.EdmProviderManifest, this, throwOnError);
		}

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x06001EEF RID: 7919 RVA: 0x0006D150 File Offset: 0x0006B350
		// (set) Token: 0x06001EF0 RID: 7920 RVA: 0x0006D158 File Offset: 0x0006B358
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

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x06001EF1 RID: 7921 RVA: 0x0006D161 File Offset: 0x0006B361
		internal OcAssemblyCache ConventionalOcCache
		{
			get
			{
				return this._conventionalOcCache;
			}
		}

		// Token: 0x06001EF2 RID: 7922 RVA: 0x0006D16C File Offset: 0x0006B36C
		internal InitializerMetadata GetCanonicalInitializerMetadata(InitializerMetadata metadata)
		{
			if (this._getCanonicalInitializerMetadataMemoizer == null)
			{
				Interlocked.CompareExchange<Memoizer<InitializerMetadata, InitializerMetadata>>(ref this._getCanonicalInitializerMetadataMemoizer, new Memoizer<InitializerMetadata, InitializerMetadata>((InitializerMetadata m) => m, EqualityComparer<InitializerMetadata>.Default), null);
			}
			return this._getCanonicalInitializerMetadataMemoizer.Evaluate(metadata);
		}

		// Token: 0x06001EF3 RID: 7923 RVA: 0x0006D1C8 File Offset: 0x0006B3C8
		internal static bool IsSystemNamespace(DbProviderManifest manifest, string namespaceName)
		{
			if (manifest == MetadataItem.EdmProviderManifest)
			{
				return namespaceName == "Transient" || namespaceName == "Edm" || namespaceName == "System";
			}
			return namespaceName == "Transient" || namespaceName == "Edm" || namespaceName == "System" || (manifest != null && namespaceName == manifest.NamespaceName);
		}

		// Token: 0x06001EF4 RID: 7924 RVA: 0x0006D240 File Offset: 0x0006B440
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

		// Token: 0x06001EF5 RID: 7925 RVA: 0x0006D2CC File Offset: 0x0006B4CC
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

		// Token: 0x06001EF6 RID: 7926 RVA: 0x0006D40C File Offset: 0x0006B60C
		internal static IEnumerable<GlobalItem> LoadSomSchema(IList<Schema> somSchemas, DbProviderManifest providerManifest, ItemCollection itemCollection)
		{
			return Converter.ConvertSchema(somSchemas, providerManifest, itemCollection);
		}

		// Token: 0x06001EF7 RID: 7927 RVA: 0x0006D423 File Offset: 0x0006B623
		public ReadOnlyCollection<PrimitiveType> GetPrimitiveTypes()
		{
			return this._primitiveTypeMaps.GetTypes();
		}

		// Token: 0x06001EF8 RID: 7928 RVA: 0x0006D430 File Offset: 0x0006B630
		public ReadOnlyCollection<PrimitiveType> GetPrimitiveTypes(double edmVersion)
		{
			if (edmVersion == 1.0 || edmVersion == 1.1 || edmVersion == 2.0)
			{
				return (from type in this._primitiveTypeMaps.GetTypes()
				where !Helper.IsSpatialType(type)
				select type).ToList<PrimitiveType>().AsReadOnly();
			}
			if (edmVersion == 3.0)
			{
				return this._primitiveTypeMaps.GetTypes();
			}
			throw EntityUtil.InvalidEDMVersion(edmVersion);
		}

		// Token: 0x06001EF9 RID: 7929 RVA: 0x0006D4BC File Offset: 0x0006B6BC
		internal override PrimitiveType GetMappedPrimitiveType(PrimitiveTypeKind primitiveTypeKind)
		{
			PrimitiveType result = null;
			this._primitiveTypeMaps.TryGetType(primitiveTypeKind, null, out result);
			return result;
		}

		// Token: 0x06001EFA RID: 7930 RVA: 0x0006D4DC File Offset: 0x0006B6DC
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

		// Token: 0x06001EFB RID: 7931 RVA: 0x0006D550 File Offset: 0x0006B750
		internal DbLambda GetGeneratedFunctionDefinition(EdmFunction function)
		{
			if (this._getGeneratedFunctionDefinitionsMemoizer == null)
			{
				Interlocked.CompareExchange<Memoizer<EdmFunction, DbLambda>>(ref this._getGeneratedFunctionDefinitionsMemoizer, new Memoizer<EdmFunction, DbLambda>(new Func<EdmFunction, DbLambda>(this.GenerateFunctionDefinition), null), null);
			}
			return this._getGeneratedFunctionDefinitionsMemoizer.Evaluate(function);
		}

		// Token: 0x06001EFC RID: 7932 RVA: 0x0006D588 File Offset: 0x0006B788
		internal DbLambda GenerateFunctionDefinition(EdmFunction function)
		{
			if (!function.HasUserDefinedBody)
			{
				throw EntityUtil.FunctionHasNoDefinition(function);
			}
			DbLambda dbLambda = ExternalCalls.CompileFunctionDefinition(function.FullName, function.CommandTextAttribute, function.Parameters, this);
			if (!TypeSemantics.IsStructurallyEqual(function.ReturnParameter.TypeUsage, dbLambda.Body.ResultType))
			{
				throw EntityUtil.FunctionDefinitionResultTypeMismatch(function, dbLambda.Body.ResultType);
			}
			return dbLambda;
		}

		// Token: 0x04000CFF RID: 3327
		private CacheForPrimitiveTypes _primitiveTypeMaps = new CacheForPrimitiveTypes();

		// Token: 0x04000D00 RID: 3328
		private double _edmVersion;

		// Token: 0x04000D01 RID: 3329
		private Memoizer<InitializerMetadata, InitializerMetadata> _getCanonicalInitializerMetadataMemoizer;

		// Token: 0x04000D02 RID: 3330
		private Memoizer<EdmFunction, DbLambda> _getGeneratedFunctionDefinitionsMemoizer;

		// Token: 0x04000D03 RID: 3331
		private OcAssemblyCache _conventionalOcCache = new OcAssemblyCache();
	}
}
