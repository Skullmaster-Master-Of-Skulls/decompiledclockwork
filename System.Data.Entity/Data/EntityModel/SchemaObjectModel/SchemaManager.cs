using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Common.Utils;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.Diagnostics;
using System.Linq;
using System.Xml;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x02000313 RID: 787
	[DebuggerDisplay("DataModel={DataModel}")]
	internal class SchemaManager
	{
		// Token: 0x06002E95 RID: 11925 RVA: 0x000B0024 File Offset: 0x000AE224
		private SchemaManager(SchemaDataModelOption dataModel, AttributeValueNotification providerNotification, AttributeValueNotification providerManifestTokenNotification, ProviderManifestNeeded providerManifestNeeded)
		{
			this._dataModel = dataModel;
			this._providerNotification = providerNotification;
			this._providerManifestTokenNotification = providerManifestTokenNotification;
			this._providerManifestNeeded = providerManifestNeeded;
		}

		// Token: 0x06002E96 RID: 11926 RVA: 0x000B0064 File Offset: 0x000AE264
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

		// Token: 0x06002E97 RID: 11927 RVA: 0x000089D0 File Offset: 0x00006BD0
		public static void NoOpAttributeValueNotification(string attributeValue, Action<string, ErrorCode, EdmSchemaErrorSeverity> addError)
		{
		}

		// Token: 0x06002E98 RID: 11928 RVA: 0x000B00BC File Offset: 0x000AE2BC
		public static IList<EdmSchemaError> ParseAndValidate(IEnumerable<XmlReader> xmlReaders, IEnumerable<string> sourceFilePaths, SchemaDataModelOption dataModel, DbProviderManifest providerManifest, out IList<Schema> schemaCollection)
		{
			return SchemaManager.ParseAndValidate(xmlReaders, sourceFilePaths, dataModel, new AttributeValueNotification(SchemaManager.NoOpAttributeValueNotification), new AttributeValueNotification(SchemaManager.NoOpAttributeValueNotification), delegate(Action<string, ErrorCode, EdmSchemaErrorSeverity> addError)
			{
				if (providerManifest != null)
				{
					return providerManifest;
				}
				return MetadataItem.EdmProviderManifest;
			}, out schemaCollection);
		}

		// Token: 0x06002E99 RID: 11929 RVA: 0x000B0104 File Offset: 0x000AE304
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
					double schemaVersion = schema.SchemaVersion;
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

		// Token: 0x06002E9A RID: 11930 RVA: 0x000B02A8 File Offset: 0x000AE4A8
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

		// Token: 0x06002E9B RID: 11931 RVA: 0x000B0320 File Offset: 0x000AE520
		internal static bool TryGetSchemaVersion(string xmlNamespaceName, out double version, out DataSpace dataSpace)
		{
			uint num = <PrivateImplementationDetails>.ComputeStringHash(xmlNamespaceName);
			if (num <= 2737002321U)
			{
				if (num <= 276889131U)
				{
					if (num != 54480152U)
					{
						if (num == 276889131U)
						{
							if (xmlNamespaceName == "http://schemas.microsoft.com/ado/2006/04/edm/ssdl")
							{
								version = 1.0;
								dataSpace = DataSpace.SSpace;
								return true;
							}
						}
					}
					else if (xmlNamespaceName == "http://schemas.microsoft.com/ado/2009/11/edm/ssdl")
					{
						version = 3.0;
						dataSpace = DataSpace.SSpace;
						return true;
					}
				}
				else if (num != 334451066U)
				{
					if (num != 1932155917U)
					{
						if (num == 2737002321U)
						{
							if (xmlNamespaceName == "http://schemas.microsoft.com/ado/2009/11/edm")
							{
								version = 3.0;
								dataSpace = DataSpace.CSpace;
								return true;
							}
						}
					}
					else if (xmlNamespaceName == "urn:schemas-microsoft-com:windows:storage:mapping:CS")
					{
						version = 1.0;
						dataSpace = DataSpace.CSSpace;
						return true;
					}
				}
				else if (xmlNamespaceName == "http://schemas.microsoft.com/ado/2008/09/mapping/cs")
				{
					version = 2.0;
					dataSpace = DataSpace.CSSpace;
					return true;
				}
			}
			else if (num <= 2886803144U)
			{
				if (num != 2826911950U)
				{
					if (num == 2886803144U)
					{
						if (xmlNamespaceName == "http://schemas.microsoft.com/ado/2006/04/edm")
						{
							version = 1.0;
							dataSpace = DataSpace.CSpace;
							return true;
						}
					}
				}
				else if (xmlNamespaceName == "http://schemas.microsoft.com/ado/2007/05/edm")
				{
					version = 1.1;
					dataSpace = DataSpace.CSpace;
					return true;
				}
			}
			else if (num != 3075483009U)
			{
				if (num != 3250650152U)
				{
					if (num == 4049421514U)
					{
						if (xmlNamespaceName == "http://schemas.microsoft.com/ado/2009/11/mapping/cs")
						{
							version = 3.0;
							dataSpace = DataSpace.CSSpace;
							return true;
						}
					}
				}
				else if (xmlNamespaceName == "http://schemas.microsoft.com/ado/2009/02/edm/ssdl")
				{
					version = 2.0;
					dataSpace = DataSpace.SSpace;
					return true;
				}
			}
			else if (xmlNamespaceName == "http://schemas.microsoft.com/ado/2008/09/edm")
			{
				version = 2.0;
				dataSpace = DataSpace.CSpace;
				return true;
			}
			version = 0.0;
			dataSpace = DataSpace.OSpace;
			return false;
		}

		// Token: 0x06002E9C RID: 11932 RVA: 0x000B0544 File Offset: 0x000AE744
		private static bool CheckIsSameVersion(Schema schemaToBeAdded, IEnumerable<Schema> schemaCollection, List<EdmSchemaError> errorCollection)
		{
			if (schemaToBeAdded.SchemaVersion != 0.0 && schemaCollection.Count<Schema>() > 0 && schemaCollection.Any((Schema s) => s.SchemaVersion != 0.0 && s.SchemaVersion != schemaToBeAdded.SchemaVersion))
			{
				errorCollection.Add(new EdmSchemaError(Strings.CannotLoadDifferentVersionOfSchemaInTheSameItemCollection, 194, EdmSchemaErrorSeverity.Error));
			}
			return true;
		}

		// Token: 0x1700091F RID: 2335
		// (get) Token: 0x06002E9D RID: 11933 RVA: 0x000B05A8 File Offset: 0x000AE7A8
		public double SchemaVersion
		{
			get
			{
				return this.effectiveSchemaVersion;
			}
		}

		// Token: 0x06002E9E RID: 11934 RVA: 0x000B05B0 File Offset: 0x000AE7B0
		public void AddSchema(Schema schema)
		{
			if (this._namespaceLookUpTable.Count == 0 && schema.DataModel != SchemaDataModelOption.ProviderManifestModel && this.PrimitiveSchema.Namespace != null)
			{
				this._namespaceLookUpTable.Add(this.PrimitiveSchema.Namespace);
			}
			this._namespaceLookUpTable.Add(schema.Namespace);
		}

		// Token: 0x06002E9F RID: 11935 RVA: 0x000B060C File Offset: 0x000AE80C
		public bool TryResolveType(string namespaceName, string typeName, out SchemaType schemaType)
		{
			string key = string.IsNullOrEmpty(namespaceName) ? typeName : (namespaceName + "." + typeName);
			schemaType = this.SchemaTypes.LookUpEquivalentKey(key);
			return schemaType != null;
		}

		// Token: 0x06002EA0 RID: 11936 RVA: 0x000B0646 File Offset: 0x000AE846
		public bool IsValidNamespaceName(string namespaceName)
		{
			return this._namespaceLookUpTable.Contains(namespaceName);
		}

		// Token: 0x06002EA1 RID: 11937 RVA: 0x000B0654 File Offset: 0x000AE854
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

		// Token: 0x06002EA2 RID: 11938 RVA: 0x000B06A0 File Offset: 0x000AE8A0
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

		// Token: 0x17000920 RID: 2336
		// (get) Token: 0x06002EA3 RID: 11939 RVA: 0x000B06F8 File Offset: 0x000AE8F8
		internal SchemaElementLookUpTable<SchemaType> SchemaTypes
		{
			get
			{
				return this._schemaTypes;
			}
		}

		// Token: 0x06002EA4 RID: 11940 RVA: 0x000B0700 File Offset: 0x000AE900
		internal DbProviderManifest GetProviderManifest(Action<string, ErrorCode, EdmSchemaErrorSeverity> addError)
		{
			if (this._providerManifest == null)
			{
				this._providerManifest = this._providerManifestNeeded(addError);
			}
			return this._providerManifest;
		}

		// Token: 0x17000921 RID: 2337
		// (get) Token: 0x06002EA5 RID: 11941 RVA: 0x000B0722 File Offset: 0x000AE922
		internal SchemaDataModelOption DataModel
		{
			get
			{
				return this._dataModel;
			}
		}

		// Token: 0x06002EA6 RID: 11942 RVA: 0x000B072A File Offset: 0x000AE92A
		internal void EnsurePrimitiveSchemaIsLoaded(double forSchemaVersion)
		{
			if (this._primitiveSchema == null)
			{
				this.effectiveSchemaVersion = forSchemaVersion;
				this._primitiveSchema = new PrimitiveSchema(this);
			}
		}

		// Token: 0x17000922 RID: 2338
		// (get) Token: 0x06002EA7 RID: 11943 RVA: 0x000B0747 File Offset: 0x000AE947
		internal PrimitiveSchema PrimitiveSchema
		{
			get
			{
				return this._primitiveSchema;
			}
		}

		// Token: 0x17000923 RID: 2339
		// (get) Token: 0x06002EA8 RID: 11944 RVA: 0x000B074F File Offset: 0x000AE94F
		internal AttributeValueNotification ProviderNotification
		{
			get
			{
				return this._providerNotification;
			}
		}

		// Token: 0x17000924 RID: 2340
		// (get) Token: 0x06002EA9 RID: 11945 RVA: 0x000B0757 File Offset: 0x000AE957
		internal AttributeValueNotification ProviderManifestTokenNotification
		{
			get
			{
				return this._providerManifestTokenNotification;
			}
		}

		// Token: 0x0400142E RID: 5166
		private readonly HashSet<string> _namespaceLookUpTable = new HashSet<string>(StringComparer.Ordinal);

		// Token: 0x0400142F RID: 5167
		private readonly SchemaElementLookUpTable<SchemaType> _schemaTypes = new SchemaElementLookUpTable<SchemaType>();

		// Token: 0x04001430 RID: 5168
		private const int MaxErrorCount = 100;

		// Token: 0x04001431 RID: 5169
		private DbProviderManifest _providerManifest;

		// Token: 0x04001432 RID: 5170
		private PrimitiveSchema _primitiveSchema;

		// Token: 0x04001433 RID: 5171
		private double effectiveSchemaVersion;

		// Token: 0x04001434 RID: 5172
		private readonly SchemaDataModelOption _dataModel;

		// Token: 0x04001435 RID: 5173
		private readonly ProviderManifestNeeded _providerManifestNeeded;

		// Token: 0x04001436 RID: 5174
		private readonly AttributeValueNotification _providerNotification;

		// Token: 0x04001437 RID: 5175
		private readonly AttributeValueNotification _providerManifestTokenNotification;
	}
}
