using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Metadata.Edm.Provider;
using System.Data.Entity.Resources;
using System.Diagnostics;
using System.Linq;
using System.Xml;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x02000391 RID: 913
	[DebuggerDisplay("DataModel={DataModel}")]
	internal class SchemaManager
	{
		// Token: 0x060020F1 RID: 8433 RVA: 0x0009AED0 File Offset: 0x000990D0
		private SchemaManager(SchemaDataModelOption dataModel, AttributeValueNotification providerNotification, AttributeValueNotification providerManifestTokenNotification, ProviderManifestNeeded providerManifestNeeded)
		{
			this._dataModel = dataModel;
			this._providerNotification = providerNotification;
			this._providerManifestTokenNotification = providerManifestTokenNotification;
			this._providerManifestNeeded = providerManifestNeeded;
		}

		// Token: 0x060020F2 RID: 8434 RVA: 0x0009AF10 File Offset: 0x00099110
		public static IList<EdmSchemaError> LoadProviderManifest(XmlReader xmlReader, string location, bool checkForSystemNamespace, out Schema schema)
		{
			IList<Schema> list = new List<Schema>(1);
			DbProviderManifest providerManifest = checkForSystemNamespace ? EdmProviderManifest.Instance : null;
			IList<EdmSchemaError> result = SchemaManager.ParseAndValidate(new XmlReader[]
			{
				xmlReader
			}, new string[]
			{
				location
			}, SchemaDataModelOption.ProviderManifestModel, providerManifest, out list);
			if (list.Count != 0)
			{
				schema = list[0];
			}
			else
			{
				schema = null;
			}
			return result;
		}

		// Token: 0x060020F3 RID: 8435 RVA: 0x0009AF6C File Offset: 0x0009916C
		public static void NoOpAttributeValueNotification(string attributeValue, Action<string, ErrorCode, EdmSchemaErrorSeverity> addError)
		{
		}

		// Token: 0x060020F4 RID: 8436 RVA: 0x0009AF88 File Offset: 0x00099188
		public static IList<EdmSchemaError> ParseAndValidate(IEnumerable<XmlReader> xmlReaders, IEnumerable<string> sourceFilePaths, SchemaDataModelOption dataModel, DbProviderManifest providerManifest, out IList<Schema> schemaCollection)
		{
			return SchemaManager.ParseAndValidate(xmlReaders, sourceFilePaths, dataModel, new AttributeValueNotification(SchemaManager.NoOpAttributeValueNotification), new AttributeValueNotification(SchemaManager.NoOpAttributeValueNotification), (Action<string, ErrorCode, EdmSchemaErrorSeverity> error) => providerManifest ?? MetadataItem.EdmProviderManifest, out schemaCollection);
		}

		// Token: 0x060020F5 RID: 8437 RVA: 0x0009AFD0 File Offset: 0x000991D0
		public static IList<EdmSchemaError> ParseAndValidate(IEnumerable<XmlReader> xmlReaders, IEnumerable<string> sourceFilePaths, SchemaDataModelOption dataModel, AttributeValueNotification providerNotification, AttributeValueNotification providerManifestTokenNotification, ProviderManifestNeeded providerManifestNeeded, out IList<Schema> schemaCollection)
		{
			SchemaManager schemaManager = new SchemaManager(dataModel, providerNotification, providerManifestTokenNotification, providerManifestNeeded);
			List<EdmSchemaError> list = new List<EdmSchemaError>();
			schemaCollection = new List<Schema>();
			bool flag = false;
			List<string> list2;
			if (sourceFilePaths != null)
			{
				list2 = new List<string>(sourceFilePaths);
			}
			else
			{
				list2 = new List<string>();
			}
			int num = 0;
			foreach (XmlReader xmlReader in xmlReaders)
			{
				string sourceLocation = null;
				if (list2.Count <= num)
				{
					SchemaManager.TryGetBaseUri(xmlReader, out sourceLocation);
				}
				else
				{
					sourceLocation = list2[num];
				}
				Schema schema = new Schema(schemaManager);
				IList<EdmSchemaError> newErrors = schema.Parse(xmlReader, sourceLocation);
				SchemaManager.CheckIsSameVersion(schema, schemaCollection, list);
				if (SchemaManager.UpdateErrorCollectionAndCheckForMaxErrors(list, newErrors, ref flag))
				{
					return list;
				}
				if (!flag)
				{
					schemaCollection.Add(schema);
					schemaManager.AddSchema(schema);
				}
				num++;
			}
			if (!flag)
			{
				foreach (Schema schema2 in schemaCollection)
				{
					if (SchemaManager.UpdateErrorCollectionAndCheckForMaxErrors(list, schema2.Resolve(), ref flag))
					{
						return list;
					}
				}
				if (!flag)
				{
					foreach (Schema schema3 in schemaCollection)
					{
						if (SchemaManager.UpdateErrorCollectionAndCheckForMaxErrors(list, schema3.ValidateSchema(), ref flag))
						{
							return list;
						}
					}
				}
			}
			return list;
		}

		// Token: 0x060020F6 RID: 8438 RVA: 0x0009B168 File Offset: 0x00099368
		internal static bool TryGetSchemaVersion(XmlReader reader, out double version, out DataSpace dataSpace)
		{
			if (!reader.EOF && reader.NodeType != XmlNodeType.Element)
			{
				while (reader.Read() && reader.NodeType != XmlNodeType.Element)
				{
				}
			}
			if (!reader.EOF && (reader.LocalName == "Schema" || reader.LocalName == "Mapping"))
			{
				return SchemaManager.TryGetSchemaVersion(reader.NamespaceURI, out version, out dataSpace);
			}
			version = 0.0;
			dataSpace = DataSpace.OSpace;
			return false;
		}

		// Token: 0x060020F7 RID: 8439 RVA: 0x0009B1E0 File Offset: 0x000993E0
		internal static bool TryGetSchemaVersion(string xmlNamespaceName, out double version, out DataSpace dataSpace)
		{
			switch (xmlNamespaceName)
			{
			case "http://schemas.microsoft.com/ado/2006/04/edm":
				version = 1.0;
				dataSpace = DataSpace.CSpace;
				return true;
			case "http://schemas.microsoft.com/ado/2007/05/edm":
				version = 1.1;
				dataSpace = DataSpace.CSpace;
				return true;
			case "http://schemas.microsoft.com/ado/2008/09/edm":
				version = 2.0;
				dataSpace = DataSpace.CSpace;
				return true;
			case "http://schemas.microsoft.com/ado/2009/11/edm":
				version = 3.0;
				dataSpace = DataSpace.CSpace;
				return true;
			case "http://schemas.microsoft.com/ado/2006/04/edm/ssdl":
				version = 1.0;
				dataSpace = DataSpace.SSpace;
				return true;
			case "http://schemas.microsoft.com/ado/2009/02/edm/ssdl":
				version = 2.0;
				dataSpace = DataSpace.SSpace;
				return true;
			case "http://schemas.microsoft.com/ado/2009/11/edm/ssdl":
				version = 3.0;
				dataSpace = DataSpace.SSpace;
				return true;
			case "urn:schemas-microsoft-com:windows:storage:mapping:CS":
				version = 1.0;
				dataSpace = DataSpace.CSSpace;
				return true;
			case "http://schemas.microsoft.com/ado/2008/09/mapping/cs":
				version = 2.0;
				dataSpace = DataSpace.CSSpace;
				return true;
			case "http://schemas.microsoft.com/ado/2009/11/mapping/cs":
				version = 3.0;
				dataSpace = DataSpace.CSSpace;
				return true;
			}
			version = 0.0;
			dataSpace = DataSpace.OSpace;
			return false;
		}

		// Token: 0x060020F8 RID: 8440 RVA: 0x0009B3B4 File Offset: 0x000995B4
		private static bool CheckIsSameVersion(Schema schemaToBeAdded, IEnumerable<Schema> schemaCollection, List<EdmSchemaError> errorCollection)
		{
			if (schemaToBeAdded.SchemaVersion != 0.0 && schemaCollection.Count<Schema>() > 0)
			{
				if (schemaCollection.Any((Schema s) => s.SchemaVersion != 0.0 && s.SchemaVersion != schemaToBeAdded.SchemaVersion))
				{
					errorCollection.Add(new EdmSchemaError(Strings.CannotLoadDifferentVersionOfSchemaInTheSameItemCollection, 194, EdmSchemaErrorSeverity.Error));
				}
			}
			return true;
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x060020F9 RID: 8441 RVA: 0x0009B41F File Offset: 0x0009961F
		public double SchemaVersion
		{
			get
			{
				return this.effectiveSchemaVersion;
			}
		}

		// Token: 0x060020FA RID: 8442 RVA: 0x0009B428 File Offset: 0x00099628
		public void AddSchema(Schema schema)
		{
			if (this._namespaceLookUpTable.Count == 0 && schema.DataModel != SchemaDataModelOption.ProviderManifestModel && this.PrimitiveSchema.Namespace != null)
			{
				this._namespaceLookUpTable.Add(this.PrimitiveSchema.Namespace);
			}
			this._namespaceLookUpTable.Add(schema.Namespace);
		}

		// Token: 0x060020FB RID: 8443 RVA: 0x0009B484 File Offset: 0x00099684
		public bool TryResolveType(string namespaceName, string typeName, out SchemaType schemaType)
		{
			string key = string.IsNullOrEmpty(namespaceName) ? typeName : (namespaceName + "." + typeName);
			schemaType = this.SchemaTypes.LookUpEquivalentKey(key);
			return schemaType != null;
		}

		// Token: 0x060020FC RID: 8444 RVA: 0x0009B4BE File Offset: 0x000996BE
		public bool IsValidNamespaceName(string namespaceName)
		{
			return this._namespaceLookUpTable.Contains(namespaceName);
		}

		// Token: 0x060020FD RID: 8445 RVA: 0x0009B4CC File Offset: 0x000996CC
		internal static bool TryGetBaseUri(XmlReader xmlReader, out string location)
		{
			string baseURI = xmlReader.BaseURI;
			Uri uri = null;
			if (!string.IsNullOrEmpty(baseURI) && Uri.TryCreate(baseURI, UriKind.Absolute, out uri) && uri.Scheme == "file")
			{
				location = Helper.GetFileNameFromUri(uri);
				return true;
			}
			location = null;
			return false;
		}

		// Token: 0x060020FE RID: 8446 RVA: 0x0009B520 File Offset: 0x00099720
		private static bool UpdateErrorCollectionAndCheckForMaxErrors(List<EdmSchemaError> errorCollection, IList<EdmSchemaError> newErrors, ref bool errorEncountered)
		{
			if (!errorEncountered && !MetadataHelper.CheckIfAllErrorsAreWarnings(newErrors))
			{
				errorEncountered = true;
			}
			errorCollection.AddRange(newErrors);
			if (errorEncountered)
			{
				if ((from e in errorCollection
				where e.Severity == EdmSchemaErrorSeverity.Error
				select e).Count<EdmSchemaError>() > 100)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x060020FF RID: 8447 RVA: 0x0009B576 File Offset: 0x00099776
		internal SchemaElementLookUpTable<SchemaType> SchemaTypes
		{
			get
			{
				return this._schemaTypes;
			}
		}

		// Token: 0x06002100 RID: 8448 RVA: 0x0009B57E File Offset: 0x0009977E
		internal DbProviderManifest GetProviderManifest(Action<string, ErrorCode, EdmSchemaErrorSeverity> addError)
		{
			if (this._providerManifest == null)
			{
				this._providerManifest = this._providerManifestNeeded(addError);
			}
			return this._providerManifest;
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06002101 RID: 8449 RVA: 0x0009B5A0 File Offset: 0x000997A0
		internal SchemaDataModelOption DataModel
		{
			get
			{
				return this._dataModel;
			}
		}

		// Token: 0x06002102 RID: 8450 RVA: 0x0009B5A8 File Offset: 0x000997A8
		internal void EnsurePrimitiveSchemaIsLoaded(double forSchemaVersion)
		{
			if (this._primitiveSchema == null)
			{
				this.effectiveSchemaVersion = forSchemaVersion;
				this._primitiveSchema = new PrimitiveSchema(this);
			}
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06002103 RID: 8451 RVA: 0x0009B5C5 File Offset: 0x000997C5
		internal PrimitiveSchema PrimitiveSchema
		{
			get
			{
				return this._primitiveSchema;
			}
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06002104 RID: 8452 RVA: 0x0009B5CD File Offset: 0x000997CD
		internal AttributeValueNotification ProviderNotification
		{
			get
			{
				return this._providerNotification;
			}
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06002105 RID: 8453 RVA: 0x0009B5D5 File Offset: 0x000997D5
		internal AttributeValueNotification ProviderManifestTokenNotification
		{
			get
			{
				return this._providerManifestTokenNotification;
			}
		}

		// Token: 0x04000BAB RID: 2987
		private const int MaxErrorCount = 100;

		// Token: 0x04000BAC RID: 2988
		private readonly HashSet<string> _namespaceLookUpTable = new HashSet<string>(StringComparer.Ordinal);

		// Token: 0x04000BAD RID: 2989
		private readonly SchemaElementLookUpTable<SchemaType> _schemaTypes = new SchemaElementLookUpTable<SchemaType>();

		// Token: 0x04000BAE RID: 2990
		private DbProviderManifest _providerManifest;

		// Token: 0x04000BAF RID: 2991
		private PrimitiveSchema _primitiveSchema;

		// Token: 0x04000BB0 RID: 2992
		private double effectiveSchemaVersion;

		// Token: 0x04000BB1 RID: 2993
		private readonly SchemaDataModelOption _dataModel;

		// Token: 0x04000BB2 RID: 2994
		private readonly ProviderManifestNeeded _providerManifestNeeded;

		// Token: 0x04000BB3 RID: 2995
		private readonly AttributeValueNotification _providerNotification;

		// Token: 0x04000BB4 RID: 2996
		private readonly AttributeValueNotification _providerManifestTokenNotification;
	}
}
