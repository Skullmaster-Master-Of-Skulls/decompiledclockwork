using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Data.Common;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Data
{
	// Token: 0x020000C9 RID: 201
	[Designer("Microsoft.VSDesigner.Data.VS.DataSetDesigner, Microsoft.VSDesigner, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultProperty("DataSetName")]
	[XmlRoot("DataSet")]
	[XmlSchemaProvider("GetDataSetSchema")]
	[ToolboxItem("Microsoft.VSDesigner.Data.VS.DataSetToolboxItem, Microsoft.VSDesigner, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ResDescription("DataSetDescr")]
	[Serializable]
	public class DataSet : MarshalByValueComponent, IListSource, IXmlSerializable, ISupportInitializeNotification, ISupportInitialize, ISerializable
	{
		// Token: 0x06000BA2 RID: 2978 RVA: 0x00063810 File Offset: 0x00062C10
		public DataSet()
		{
			GC.SuppressFinalize(this);
			Bid.Trace("<ds.DataSet.DataSet|API> %d#\n", this.ObjectID);
			this.tableCollection = new DataTableCollection(this);
			this.relationCollection = new DataRelationCollection.DataSetRelationCollection(this);
			this._culture = CultureInfo.CurrentCulture;
		}

		// Token: 0x06000BA3 RID: 2979 RVA: 0x000638B4 File Offset: 0x00062CB4
		public DataSet(string dataSetName) : this()
		{
			this.DataSetName = dataSetName;
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000BA4 RID: 2980 RVA: 0x000638D0 File Offset: 0x00062CD0
		// (set) Token: 0x06000BA5 RID: 2981 RVA: 0x000638E4 File Offset: 0x00062CE4
		[DefaultValue(SerializationFormat.Xml)]
		public SerializationFormat RemotingFormat
		{
			get
			{
				return this._remotingFormat;
			}
			set
			{
				if (value != SerializationFormat.Binary && value != SerializationFormat.Xml)
				{
					throw ExceptionBuilder.InvalidRemotingFormat(value);
				}
				this._remotingFormat = value;
				for (int i = 0; i < this.Tables.Count; i++)
				{
					this.Tables[i].RemotingFormat = value;
				}
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000BA6 RID: 2982 RVA: 0x00063930 File Offset: 0x00062D30
		// (set) Token: 0x06000BA7 RID: 2983 RVA: 0x00063940 File Offset: 0x00062D40
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual SchemaSerializationMode SchemaSerializationMode
		{
			get
			{
				return SchemaSerializationMode.IncludeSchema;
			}
			set
			{
				if (value != SchemaSerializationMode.IncludeSchema)
				{
					throw ExceptionBuilder.CannotChangeSchemaSerializationMode();
				}
			}
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x00063958 File Offset: 0x00062D58
		protected bool IsBinarySerialized(SerializationInfo info, StreamingContext context)
		{
			SerializationFormat serializationFormat = SerializationFormat.Xml;
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Name == "DataSet.RemotingFormat")
				{
					serializationFormat = (SerializationFormat)enumerator.Value;
					break;
				}
			}
			return serializationFormat == SerializationFormat.Binary;
		}

		// Token: 0x06000BA9 RID: 2985 RVA: 0x0006399C File Offset: 0x00062D9C
		protected SchemaSerializationMode DetermineSchemaSerializationMode(SerializationInfo info, StreamingContext context)
		{
			SchemaSerializationMode result = SchemaSerializationMode.IncludeSchema;
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Name == "SchemaSerializationMode.DataSet")
				{
					result = (SchemaSerializationMode)enumerator.Value;
					break;
				}
			}
			return result;
		}

		// Token: 0x06000BAA RID: 2986 RVA: 0x000639E0 File Offset: 0x00062DE0
		protected SchemaSerializationMode DetermineSchemaSerializationMode(XmlReader reader)
		{
			SchemaSerializationMode result = SchemaSerializationMode.IncludeSchema;
			reader.MoveToContent();
			if (reader.NodeType == XmlNodeType.Element && reader.HasAttributes)
			{
				string attribute = reader.GetAttribute("SchemaSerializationMode", "urn:schemas-microsoft-com:xml-msdata");
				if (string.Compare(attribute, "ExcludeSchema", StringComparison.OrdinalIgnoreCase) == 0)
				{
					result = SchemaSerializationMode.ExcludeSchema;
				}
				else if (string.Compare(attribute, "IncludeSchema", StringComparison.OrdinalIgnoreCase) == 0)
				{
					result = SchemaSerializationMode.IncludeSchema;
				}
				else if (attribute != null)
				{
					throw ExceptionBuilder.InvalidSchemaSerializationMode(typeof(SchemaSerializationMode), attribute);
				}
			}
			return result;
		}

		// Token: 0x06000BAB RID: 2987 RVA: 0x00063A54 File Offset: 0x00062E54
		protected void GetSerializationData(SerializationInfo info, StreamingContext context)
		{
			SerializationFormat remotingFormat = SerializationFormat.Xml;
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Name == "DataSet.RemotingFormat")
				{
					remotingFormat = (SerializationFormat)enumerator.Value;
					break;
				}
			}
			this.DeserializeDataSetData(info, context, remotingFormat);
		}

		// Token: 0x06000BAC RID: 2988 RVA: 0x00063AA0 File Offset: 0x00062EA0
		protected DataSet(SerializationInfo info, StreamingContext context) : this(info, context, true)
		{
		}

		// Token: 0x06000BAD RID: 2989 RVA: 0x00063AB8 File Offset: 0x00062EB8
		protected DataSet(SerializationInfo info, StreamingContext context, bool ConstructSchema) : this()
		{
			SerializationFormat serializationFormat = SerializationFormat.Xml;
			SchemaSerializationMode schemaSerializationMode = SchemaSerializationMode.IncludeSchema;
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			while (enumerator.MoveNext())
			{
				string name = enumerator.Name;
				if (!(name == "DataSet.RemotingFormat"))
				{
					if (name == "SchemaSerializationMode.DataSet")
					{
						schemaSerializationMode = (SchemaSerializationMode)enumerator.Value;
					}
				}
				else
				{
					serializationFormat = (SerializationFormat)enumerator.Value;
				}
			}
			if (schemaSerializationMode == SchemaSerializationMode.ExcludeSchema)
			{
				this.InitializeDerivedDataSet();
			}
			if (serializationFormat == SerializationFormat.Xml && !ConstructSchema)
			{
				return;
			}
			this.DeserializeDataSet(info, context, serializationFormat, schemaSerializationMode);
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x00063B38 File Offset: 0x00062F38
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			SerializationFormat remotingFormat = this.RemotingFormat;
			this.SerializeDataSet(info, context, remotingFormat);
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x00063B58 File Offset: 0x00062F58
		protected virtual void InitializeDerivedDataSet()
		{
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x00063B68 File Offset: 0x00062F68
		private void SerializeDataSet(SerializationInfo info, StreamingContext context, SerializationFormat remotingFormat)
		{
			info.AddValue("DataSet.RemotingVersion", new Version(2, 0));
			if (remotingFormat != SerializationFormat.Xml)
			{
				info.AddValue("DataSet.RemotingFormat", remotingFormat);
			}
			if (SchemaSerializationMode.IncludeSchema != this.SchemaSerializationMode)
			{
				info.AddValue("SchemaSerializationMode.DataSet", this.SchemaSerializationMode);
			}
			if (remotingFormat != SerializationFormat.Xml)
			{
				if (this.SchemaSerializationMode == SchemaSerializationMode.IncludeSchema)
				{
					this.SerializeDataSetProperties(info, context);
					info.AddValue("DataSet.Tables.Count", this.Tables.Count);
					for (int i = 0; i < this.Tables.Count; i++)
					{
						BinaryFormatter binaryFormatter = new BinaryFormatter(null, new StreamingContext(context.State, false));
						MemoryStream memoryStream = new MemoryStream();
						binaryFormatter.Serialize(memoryStream, this.Tables[i]);
						memoryStream.Position = 0L;
						info.AddValue(string.Format(CultureInfo.InvariantCulture, "DataSet.Tables_{0}", new object[]
						{
							i
						}), memoryStream.GetBuffer());
					}
					for (int j = 0; j < this.Tables.Count; j++)
					{
						this.Tables[j].SerializeConstraints(info, context, j, true);
					}
					this.SerializeRelations(info, context);
					for (int k = 0; k < this.Tables.Count; k++)
					{
						this.Tables[k].SerializeExpressionColumns(info, context, k);
					}
				}
				else
				{
					this.SerializeDataSetProperties(info, context);
				}
				for (int l = 0; l < this.Tables.Count; l++)
				{
					this.Tables[l].SerializeTableData(info, context, l);
				}
				return;
			}
			string xmlSchemaForRemoting = this.GetXmlSchemaForRemoting(null);
			info.AddValue("XmlSchema", xmlSchemaForRemoting);
			StringBuilder sb = new StringBuilder(this.EstimatedXmlStringSize() * 2);
			StringWriter stringWriter = new StringWriter(sb, CultureInfo.InvariantCulture);
			XmlTextWriter writer = new XmlTextWriter(stringWriter);
			this.WriteXml(writer, XmlWriteMode.DiffGram);
			string value = stringWriter.ToString();
			info.AddValue("XmlDiffGram", value);
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x00063D5C File Offset: 0x0006315C
		internal void DeserializeDataSet(SerializationInfo info, StreamingContext context, SerializationFormat remotingFormat, SchemaSerializationMode schemaSerializationMode)
		{
			this.DeserializeDataSetSchema(info, context, remotingFormat, schemaSerializationMode);
			this.DeserializeDataSetData(info, context, remotingFormat);
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x00063D80 File Offset: 0x00063180
		private void DeserializeDataSetSchema(SerializationInfo info, StreamingContext context, SerializationFormat remotingFormat, SchemaSerializationMode schemaSerializationMode)
		{
			if (remotingFormat == SerializationFormat.Xml)
			{
				string text = (string)info.GetValue("XmlSchema", typeof(string));
				if (text != null)
				{
					this.ReadXmlSchema(new XmlTextReader(new StringReader(text)), true);
				}
				return;
			}
			if (schemaSerializationMode == SchemaSerializationMode.IncludeSchema)
			{
				this.DeserializeDataSetProperties(info, context);
				int @int = info.GetInt32("DataSet.Tables.Count");
				for (int i = 0; i < @int; i++)
				{
					byte[] buffer = (byte[])info.GetValue(string.Format(CultureInfo.InvariantCulture, "DataSet.Tables_{0}", new object[]
					{
						i
					}), typeof(byte[]));
					MemoryStream memoryStream = new MemoryStream(buffer);
					memoryStream.Position = 0L;
					BinaryFormatter binaryFormatter = new BinaryFormatter(null, new StreamingContext(context.State, false));
					DataTable table = (DataTable)binaryFormatter.Deserialize(memoryStream);
					this.Tables.Add(table);
				}
				for (int j = 0; j < @int; j++)
				{
					this.Tables[j].DeserializeConstraints(info, context, j, true);
				}
				this.DeserializeRelations(info, context);
				for (int k = 0; k < @int; k++)
				{
					this.Tables[k].DeserializeExpressionColumns(info, context, k);
				}
				return;
			}
			this.DeserializeDataSetProperties(info, context);
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x00063EC8 File Offset: 0x000632C8
		private void DeserializeDataSetData(SerializationInfo info, StreamingContext context, SerializationFormat remotingFormat)
		{
			if (remotingFormat != SerializationFormat.Xml)
			{
				for (int i = 0; i < this.Tables.Count; i++)
				{
					this.Tables[i].DeserializeTableData(info, context, i);
				}
				return;
			}
			string text = (string)info.GetValue("XmlDiffGram", typeof(string));
			if (text != null)
			{
				this.ReadXml(new XmlTextReader(new StringReader(text)), XmlReadMode.DiffGram);
			}
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x00063F34 File Offset: 0x00063334
		private void SerializeDataSetProperties(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("DataSet.DataSetName", this.DataSetName);
			info.AddValue("DataSet.Namespace", this.Namespace);
			info.AddValue("DataSet.Prefix", this.Prefix);
			info.AddValue("DataSet.CaseSensitive", this.CaseSensitive);
			info.AddValue("DataSet.LocaleLCID", this.Locale.LCID);
			info.AddValue("DataSet.EnforceConstraints", this.EnforceConstraints);
			info.AddValue("DataSet.ExtendedProperties", this.ExtendedProperties);
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x00063FC0 File Offset: 0x000633C0
		private void DeserializeDataSetProperties(SerializationInfo info, StreamingContext context)
		{
			this.dataSetName = info.GetString("DataSet.DataSetName");
			this.namespaceURI = info.GetString("DataSet.Namespace");
			this._datasetPrefix = info.GetString("DataSet.Prefix");
			this._caseSensitive = info.GetBoolean("DataSet.CaseSensitive");
			int culture = (int)info.GetValue("DataSet.LocaleLCID", typeof(int));
			this._culture = new CultureInfo(culture);
			this._cultureUserSet = true;
			this.enforceConstraints = info.GetBoolean("DataSet.EnforceConstraints");
			this.extendedProperties = (PropertyCollection)info.GetValue("DataSet.ExtendedProperties", typeof(PropertyCollection));
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x00064070 File Offset: 0x00063470
		private void SerializeRelations(SerializationInfo info, StreamingContext context)
		{
			ArrayList arrayList = new ArrayList();
			foreach (object obj in this.Relations)
			{
				DataRelation dataRelation = (DataRelation)obj;
				int[] array = new int[dataRelation.ParentColumns.Length + 1];
				array[0] = this.Tables.IndexOf(dataRelation.ParentTable);
				for (int i = 1; i < array.Length; i++)
				{
					array[i] = dataRelation.ParentColumns[i - 1].Ordinal;
				}
				int[] array2 = new int[dataRelation.ChildColumns.Length + 1];
				array2[0] = this.Tables.IndexOf(dataRelation.ChildTable);
				for (int j = 1; j < array2.Length; j++)
				{
					array2[j] = dataRelation.ChildColumns[j - 1].Ordinal;
				}
				arrayList.Add(new ArrayList
				{
					dataRelation.RelationName,
					array,
					array2,
					dataRelation.Nested,
					dataRelation.extendedProperties
				});
			}
			info.AddValue("DataSet.Relations", arrayList);
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x000641CC File Offset: 0x000635CC
		private void DeserializeRelations(SerializationInfo info, StreamingContext context)
		{
			ArrayList arrayList = (ArrayList)info.GetValue("DataSet.Relations", typeof(ArrayList));
			foreach (object obj in arrayList)
			{
				ArrayList arrayList2 = (ArrayList)obj;
				string relationName = (string)arrayList2[0];
				int[] array = (int[])arrayList2[1];
				int[] array2 = (int[])arrayList2[2];
				bool nested = (bool)arrayList2[3];
				PropertyCollection propertyCollection = (PropertyCollection)arrayList2[4];
				DataColumn[] array3 = new DataColumn[array.Length - 1];
				for (int i = 0; i < array3.Length; i++)
				{
					array3[i] = this.Tables[array[0]].Columns[array[i + 1]];
				}
				DataColumn[] array4 = new DataColumn[array2.Length - 1];
				for (int j = 0; j < array4.Length; j++)
				{
					array4[j] = this.Tables[array2[0]].Columns[array2[j + 1]];
				}
				DataRelation dataRelation = new DataRelation(relationName, array3, array4, false);
				dataRelation.CheckMultipleNested = false;
				dataRelation.Nested = nested;
				dataRelation.extendedProperties = propertyCollection;
				this.Relations.Add(dataRelation);
				dataRelation.CheckMultipleNested = true;
			}
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x0006434C File Offset: 0x0006374C
		internal void FailedEnableConstraints()
		{
			this.EnforceConstraints = false;
			throw ExceptionBuilder.EnforceConstraint();
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000BB9 RID: 3001 RVA: 0x00064368 File Offset: 0x00063768
		// (set) Token: 0x06000BBA RID: 3002 RVA: 0x0006437C File Offset: 0x0006377C
		[ResDescription("DataSetCaseSensitiveDescr")]
		[ResCategory("DataCategory_Data")]
		[DefaultValue(false)]
		public bool CaseSensitive
		{
			get
			{
				return this._caseSensitive;
			}
			set
			{
				if (this._caseSensitive != value)
				{
					bool caseSensitive = this._caseSensitive;
					this._caseSensitive = value;
					if (!this.ValidateCaseConstraint())
					{
						this._caseSensitive = caseSensitive;
						throw ExceptionBuilder.CannotChangeCaseLocale();
					}
					foreach (object obj in this.Tables)
					{
						DataTable dataTable = (DataTable)obj;
						dataTable.SetCaseSensitiveValue(value, false, true);
					}
				}
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000BBB RID: 3003 RVA: 0x00064410 File Offset: 0x00063810
		bool IListSource.ContainsListCollection
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000BBC RID: 3004 RVA: 0x00064420 File Offset: 0x00063820
		[ResDescription("DataSetDefaultViewDescr")]
		[Browsable(false)]
		public DataViewManager DefaultViewManager
		{
			get
			{
				if (this.defaultViewManager == null)
				{
					object defaultViewManagerLock = this._defaultViewManagerLock;
					lock (defaultViewManagerLock)
					{
						if (this.defaultViewManager == null)
						{
							this.defaultViewManager = new DataViewManager(this, true);
						}
					}
				}
				return this.defaultViewManager;
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000BBD RID: 3005 RVA: 0x0006448C File Offset: 0x0006388C
		// (set) Token: 0x06000BBE RID: 3006 RVA: 0x000644A0 File Offset: 0x000638A0
		[ResDescription("DataSetEnforceConstraintsDescr")]
		[DefaultValue(true)]
		public bool EnforceConstraints
		{
			get
			{
				return this.enforceConstraints;
			}
			set
			{
				IntPtr intPtr;
				Bid.ScopeEnter(out intPtr, "<ds.DataSet.set_EnforceConstraints|API> %d#, %d{bool}\n", this.ObjectID, value);
				try
				{
					if (this.enforceConstraints != value)
					{
						if (value)
						{
							this.EnableConstraints();
						}
						this.enforceConstraints = value;
					}
				}
				finally
				{
					Bid.ScopeLeave(ref intPtr);
				}
			}
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x00064500 File Offset: 0x00063900
		internal void RestoreEnforceConstraints(bool value)
		{
			this.enforceConstraints = value;
		}

		// Token: 0x06000BC0 RID: 3008 RVA: 0x00064514 File Offset: 0x00063914
		internal void EnableConstraints()
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<ds.DataSet.EnableConstraints|INFO> %d#\n", this.ObjectID);
			try
			{
				bool flag = false;
				ConstraintEnumerator constraintEnumerator = new ConstraintEnumerator(this);
				while (constraintEnumerator.GetNext())
				{
					Constraint constraint = constraintEnumerator.GetConstraint();
					flag |= constraint.IsConstraintViolated();
				}
				foreach (object obj in this.Tables)
				{
					DataTable dataTable = (DataTable)obj;
					foreach (object obj2 in dataTable.Columns)
					{
						DataColumn dataColumn = (DataColumn)obj2;
						if (!dataColumn.AllowDBNull)
						{
							flag |= dataColumn.IsNotAllowDBNullViolated();
						}
						if (dataColumn.MaxLength >= 0)
						{
							flag |= dataColumn.IsMaxLengthViolated();
						}
					}
				}
				if (flag)
				{
					this.FailedEnableConstraints();
				}
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000BC1 RID: 3009 RVA: 0x00064650 File Offset: 0x00063A50
		// (set) Token: 0x06000BC2 RID: 3010 RVA: 0x00064664 File Offset: 0x00063A64
		[ResDescription("DataSetDataSetNameDescr")]
		[DefaultValue("")]
		[ResCategory("DataCategory_Data")]
		public string DataSetName
		{
			get
			{
				return this.dataSetName;
			}
			set
			{
				Bid.Trace("<ds.DataSet.set_DataSetName|API> %d#, '%ls'\n", this.ObjectID, value);
				if (value != this.dataSetName)
				{
					if (value == null || value.Length == 0)
					{
						throw ExceptionBuilder.SetDataSetNameToEmpty();
					}
					DataTable dataTable = this.Tables[value, this.Namespace];
					if (dataTable != null && !dataTable.fNestedInDataset)
					{
						throw ExceptionBuilder.SetDataSetNameConflicting(value);
					}
					this.RaisePropertyChanging("DataSetName");
					this.dataSetName = value;
				}
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000BC3 RID: 3011 RVA: 0x000646D8 File Offset: 0x00063AD8
		// (set) Token: 0x06000BC4 RID: 3012 RVA: 0x000646EC File Offset: 0x00063AEC
		[DefaultValue("")]
		[ResCategory("DataCategory_Data")]
		[ResDescription("DataSetNamespaceDescr")]
		public string Namespace
		{
			get
			{
				return this.namespaceURI;
			}
			set
			{
				Bid.Trace("<ds.DataSet.set_Namespace|API> %d#, '%ls'\n", this.ObjectID, value);
				if (value == null)
				{
					value = string.Empty;
				}
				if (value != this.namespaceURI)
				{
					this.RaisePropertyChanging("Namespace");
					foreach (object obj in this.Tables)
					{
						DataTable dataTable = (DataTable)obj;
						if (dataTable.tableNamespace == null && (dataTable.NestedParentRelations.Length == 0 || (dataTable.NestedParentRelations.Length == 1 && dataTable.NestedParentRelations[0].ChildTable == dataTable)))
						{
							if (this.Tables.Contains(dataTable.TableName, value, false, true))
							{
								throw ExceptionBuilder.DuplicateTableName2(dataTable.TableName, value);
							}
							dataTable.CheckCascadingNamespaceConflict(value);
							dataTable.DoRaiseNamespaceChange();
						}
					}
					this.namespaceURI = value;
					if (ADP.IsEmpty(value))
					{
						this._datasetPrefix = string.Empty;
					}
				}
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000BC5 RID: 3013 RVA: 0x000647F8 File Offset: 0x00063BF8
		// (set) Token: 0x06000BC6 RID: 3014 RVA: 0x0006480C File Offset: 0x00063C0C
		[ResCategory("DataCategory_Data")]
		[ResDescription("DataSetPrefixDescr")]
		[DefaultValue("")]
		public string Prefix
		{
			get
			{
				return this._datasetPrefix;
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				if (XmlConvert.DecodeName(value) == value && XmlConvert.EncodeName(value) != value)
				{
					throw ExceptionBuilder.InvalidPrefix(value);
				}
				if (value != this._datasetPrefix)
				{
					this.RaisePropertyChanging("Prefix");
					this._datasetPrefix = value;
				}
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000BC7 RID: 3015 RVA: 0x00064868 File Offset: 0x00063C68
		[ResCategory("DataCategory_Data")]
		[Browsable(false)]
		[ResDescription("ExtendedPropertiesDescr")]
		public PropertyCollection ExtendedProperties
		{
			get
			{
				if (this.extendedProperties == null)
				{
					this.extendedProperties = new PropertyCollection();
				}
				return this.extendedProperties;
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000BC8 RID: 3016 RVA: 0x00064890 File Offset: 0x00063C90
		[ResDescription("DataSetHasErrorsDescr")]
		[Browsable(false)]
		public bool HasErrors
		{
			get
			{
				for (int i = 0; i < this.Tables.Count; i++)
				{
					if (this.Tables[i].HasErrors)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000BC9 RID: 3017 RVA: 0x000648CC File Offset: 0x00063CCC
		[Browsable(false)]
		public bool IsInitialized
		{
			get
			{
				return !this.fInitInProgress;
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000BCA RID: 3018 RVA: 0x000648E4 File Offset: 0x00063CE4
		// (set) Token: 0x06000BCB RID: 3019 RVA: 0x000648F8 File Offset: 0x00063CF8
		[ResDescription("DataSetLocaleDescr")]
		[ResCategory("DataCategory_Data")]
		public CultureInfo Locale
		{
			get
			{
				return this._culture;
			}
			set
			{
				IntPtr intPtr;
				Bid.ScopeEnter(out intPtr, "<ds.DataSet.set_Locale|API> %d#\n", this.ObjectID);
				try
				{
					if (value != null)
					{
						if (!this._culture.Equals(value))
						{
							this.SetLocaleValue(value, true);
						}
						this._cultureUserSet = true;
					}
				}
				finally
				{
					Bid.ScopeLeave(ref intPtr);
				}
			}
		}

		// Token: 0x06000BCC RID: 3020 RVA: 0x00064960 File Offset: 0x00063D60
		internal void SetLocaleValue(CultureInfo value, bool userSet)
		{
			bool flag = false;
			bool flag2 = false;
			int num = 0;
			CultureInfo culture = this._culture;
			bool cultureUserSet = this._cultureUserSet;
			try
			{
				this._culture = value;
				this._cultureUserSet = userSet;
				foreach (object obj in this.Tables)
				{
					DataTable dataTable = (DataTable)obj;
					if (!dataTable.ShouldSerializeLocale())
					{
						bool flag3 = dataTable.SetLocaleValue(value, false, false);
					}
				}
				flag = this.ValidateLocaleConstraint();
				if (flag)
				{
					flag = false;
					foreach (object obj2 in this.Tables)
					{
						DataTable dataTable2 = (DataTable)obj2;
						num++;
						if (!dataTable2.ShouldSerializeLocale())
						{
							dataTable2.SetLocaleValue(value, false, true);
						}
					}
					flag = true;
				}
			}
			catch
			{
				flag2 = true;
				throw;
			}
			finally
			{
				if (!flag)
				{
					this._culture = culture;
					this._cultureUserSet = cultureUserSet;
					foreach (object obj3 in this.Tables)
					{
						DataTable dataTable3 = (DataTable)obj3;
						if (!dataTable3.ShouldSerializeLocale())
						{
							dataTable3.SetLocaleValue(culture, false, false);
						}
					}
					try
					{
						for (int i = 0; i < num; i++)
						{
							if (!this.Tables[i].ShouldSerializeLocale())
							{
								this.Tables[i].SetLocaleValue(culture, false, true);
							}
						}
					}
					catch (Exception e)
					{
						if (!ADP.IsCatchableExceptionType(e))
						{
							throw;
						}
						ADP.TraceExceptionWithoutRethrow(e);
					}
					if (!flag2)
					{
						throw ExceptionBuilder.CannotChangeCaseLocale(null);
					}
				}
			}
		}

		// Token: 0x06000BCD RID: 3021 RVA: 0x00064BA8 File Offset: 0x00063FA8
		internal bool ShouldSerializeLocale()
		{
			return this._cultureUserSet;
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000BCE RID: 3022 RVA: 0x00064BBC File Offset: 0x00063FBC
		// (set) Token: 0x06000BCF RID: 3023 RVA: 0x00064BD0 File Offset: 0x00063FD0
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public override ISite Site
		{
			get
			{
				return base.Site;
			}
			set
			{
				ISite site = this.Site;
				if (value == null && site != null)
				{
					IContainer container = site.Container;
					if (container != null)
					{
						for (int i = 0; i < this.Tables.Count; i++)
						{
							if (this.Tables[i].Site != null)
							{
								container.Remove(this.Tables[i]);
							}
						}
					}
				}
				base.Site = value;
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000BD0 RID: 3024 RVA: 0x00064C38 File Offset: 0x00064038
		[ResCategory("DataCategory_Data")]
		[ResDescription("DataSetRelationsDescr")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public DataRelationCollection Relations
		{
			get
			{
				return this.relationCollection;
			}
		}

		// Token: 0x06000BD1 RID: 3025 RVA: 0x00064C4C File Offset: 0x0006404C
		protected virtual bool ShouldSerializeRelations()
		{
			return true;
		}

		// Token: 0x06000BD2 RID: 3026 RVA: 0x00064C5C File Offset: 0x0006405C
		private void ResetRelations()
		{
			this.Relations.Clear();
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000BD3 RID: 3027 RVA: 0x00064C74 File Offset: 0x00064074
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[ResCategory("DataCategory_Data")]
		[ResDescription("DataSetTablesDescr")]
		public DataTableCollection Tables
		{
			get
			{
				return this.tableCollection;
			}
		}

		// Token: 0x06000BD4 RID: 3028 RVA: 0x00064C88 File Offset: 0x00064088
		protected virtual bool ShouldSerializeTables()
		{
			return true;
		}

		// Token: 0x06000BD5 RID: 3029 RVA: 0x00064C98 File Offset: 0x00064098
		private void ResetTables()
		{
			this.Tables.Clear();
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000BD6 RID: 3030 RVA: 0x00064CB0 File Offset: 0x000640B0
		// (set) Token: 0x06000BD7 RID: 3031 RVA: 0x00064CC4 File Offset: 0x000640C4
		internal bool FBoundToDocument
		{
			get
			{
				return this.fBoundToDocument;
			}
			set
			{
				this.fBoundToDocument = value;
			}
		}

		// Token: 0x06000BD8 RID: 3032 RVA: 0x00064CD8 File Offset: 0x000640D8
		public void AcceptChanges()
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<ds.DataSet.AcceptChanges|API> %d#\n", this.ObjectID);
			try
			{
				for (int i = 0; i < this.Tables.Count; i++)
				{
					this.Tables[i].AcceptChanges();
				}
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x06000BD9 RID: 3033 RVA: 0x00064D44 File Offset: 0x00064144
		// (remove) Token: 0x06000BDA RID: 3034 RVA: 0x00064D68 File Offset: 0x00064168
		internal event PropertyChangedEventHandler PropertyChanging
		{
			add
			{
				this.onPropertyChangingDelegate = (PropertyChangedEventHandler)Delegate.Combine(this.onPropertyChangingDelegate, value);
			}
			remove
			{
				this.onPropertyChangingDelegate = (PropertyChangedEventHandler)Delegate.Remove(this.onPropertyChangingDelegate, value);
			}
		}

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x06000BDB RID: 3035 RVA: 0x00064D8C File Offset: 0x0006418C
		// (remove) Token: 0x06000BDC RID: 3036 RVA: 0x00064DB0 File Offset: 0x000641B0
		[ResCategory("DataCategory_Action")]
		[ResDescription("DataSetMergeFailedDescr")]
		public event MergeFailedEventHandler MergeFailed
		{
			add
			{
				this.onMergeFailed = (MergeFailedEventHandler)Delegate.Combine(this.onMergeFailed, value);
			}
			remove
			{
				this.onMergeFailed = (MergeFailedEventHandler)Delegate.Remove(this.onMergeFailed, value);
			}
		}

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x06000BDD RID: 3037 RVA: 0x00064DD4 File Offset: 0x000641D4
		// (remove) Token: 0x06000BDE RID: 3038 RVA: 0x00064DF8 File Offset: 0x000641F8
		internal event DataRowCreatedEventHandler DataRowCreated
		{
			add
			{
				this.onDataRowCreated = (DataRowCreatedEventHandler)Delegate.Combine(this.onDataRowCreated, value);
			}
			remove
			{
				this.onDataRowCreated = (DataRowCreatedEventHandler)Delegate.Remove(this.onDataRowCreated, value);
			}
		}

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x06000BDF RID: 3039 RVA: 0x00064E1C File Offset: 0x0006421C
		// (remove) Token: 0x06000BE0 RID: 3040 RVA: 0x00064E40 File Offset: 0x00064240
		internal event DataSetClearEventhandler ClearFunctionCalled
		{
			add
			{
				this.onClearFunctionCalled = (DataSetClearEventhandler)Delegate.Combine(this.onClearFunctionCalled, value);
			}
			remove
			{
				this.onClearFunctionCalled = (DataSetClearEventhandler)Delegate.Remove(this.onClearFunctionCalled, value);
			}
		}

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x06000BE1 RID: 3041 RVA: 0x00064E64 File Offset: 0x00064264
		// (remove) Token: 0x06000BE2 RID: 3042 RVA: 0x00064E88 File Offset: 0x00064288
		[ResCategory("DataCategory_Action")]
		[ResDescription("DataSetInitializedDescr")]
		public event EventHandler Initialized
		{
			add
			{
				this.onInitialized = (EventHandler)Delegate.Combine(this.onInitialized, value);
			}
			remove
			{
				this.onInitialized = (EventHandler)Delegate.Remove(this.onInitialized, value);
			}
		}

		// Token: 0x06000BE3 RID: 3043 RVA: 0x00064EAC File Offset: 0x000642AC
		public void BeginInit()
		{
			this.fInitInProgress = true;
		}

		// Token: 0x06000BE4 RID: 3044 RVA: 0x00064EC0 File Offset: 0x000642C0
		public void EndInit()
		{
			this.Tables.FinishInitCollection();
			for (int i = 0; i < this.Tables.Count; i++)
			{
				this.Tables[i].Columns.FinishInitCollection();
			}
			for (int j = 0; j < this.Tables.Count; j++)
			{
				this.Tables[j].Constraints.FinishInitConstraints();
			}
			((DataRelationCollection.DataSetRelationCollection)this.Relations).FinishInitRelations();
			this.fInitInProgress = false;
			this.OnInitialized();
		}

		// Token: 0x06000BE5 RID: 3045 RVA: 0x00064F50 File Offset: 0x00064350
		public void Clear()
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<ds.DataSet.Clear|API> %d#\n", this.ObjectID);
			try
			{
				this.OnClearFunctionCalled(null);
				bool flag = this.EnforceConstraints;
				this.EnforceConstraints = false;
				for (int i = 0; i < this.Tables.Count; i++)
				{
					this.Tables[i].Clear();
				}
				this.EnforceConstraints = flag;
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x06000BE6 RID: 3046 RVA: 0x00064FD8 File Offset: 0x000643D8
		[MethodImpl(MethodImplOptions.NoInlining)]
		public virtual DataSet Clone()
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<ds.DataSet.Clone|API> %d#\n", this.ObjectID);
			DataSet result;
			try
			{
				DataSet dataSet = (DataSet)Activator.CreateInstance(base.GetType(), true);
				if (dataSet.Tables.Count > 0)
				{
					dataSet.Reset();
				}
				dataSet.DataSetName = this.DataSetName;
				dataSet.CaseSensitive = this.CaseSensitive;
				dataSet._culture = this._culture;
				dataSet._cultureUserSet = this._cultureUserSet;
				dataSet.EnforceConstraints = this.EnforceConstraints;
				dataSet.Namespace = this.Namespace;
				dataSet.Prefix = this.Prefix;
				dataSet.RemotingFormat = this.RemotingFormat;
				dataSet.fIsSchemaLoading = true;
				DataTableCollection tables = this.Tables;
				for (int i = 0; i < tables.Count; i++)
				{
					DataTable dataTable = tables[i].Clone(dataSet);
					dataTable.tableNamespace = tables[i].Namespace;
					dataSet.Tables.Add(dataTable);
				}
				for (int j = 0; j < tables.Count; j++)
				{
					ConstraintCollection constraints = tables[j].Constraints;
					for (int k = 0; k < constraints.Count; k++)
					{
						if (!(constraints[k] is UniqueConstraint))
						{
							ForeignKeyConstraint foreignKeyConstraint = constraints[k] as ForeignKeyConstraint;
							if (foreignKeyConstraint.Table != foreignKeyConstraint.RelatedTable)
							{
								dataSet.Tables[j].Constraints.Add(constraints[k].Clone(dataSet));
							}
						}
					}
				}
				DataRelationCollection relations = this.Relations;
				for (int l = 0; l < relations.Count; l++)
				{
					DataRelation dataRelation = relations[l].Clone(dataSet);
					dataRelation.CheckMultipleNested = false;
					dataSet.Relations.Add(dataRelation);
					dataRelation.CheckMultipleNested = true;
				}
				if (this.extendedProperties != null)
				{
					foreach (object key in this.extendedProperties.Keys)
					{
						dataSet.ExtendedProperties[key] = this.extendedProperties[key];
					}
				}
				foreach (object obj in this.Tables)
				{
					DataTable dataTable2 = (DataTable)obj;
					foreach (object obj2 in dataTable2.Columns)
					{
						DataColumn dataColumn = (DataColumn)obj2;
						if (dataColumn.Expression.Length != 0)
						{
							dataSet.Tables[dataTable2.TableName, dataTable2.Namespace].Columns[dataColumn.ColumnName].Expression = dataColumn.Expression;
						}
					}
				}
				for (int m = 0; m < tables.Count; m++)
				{
					dataSet.Tables[m].tableNamespace = tables[m].tableNamespace;
				}
				dataSet.fIsSchemaLoading = false;
				result = dataSet;
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
			return result;
		}

		// Token: 0x06000BE7 RID: 3047 RVA: 0x00065378 File Offset: 0x00064778
		public DataSet Copy()
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<ds.DataSet.Copy|API> %d#\n", this.ObjectID);
			DataSet result;
			try
			{
				DataSet dataSet = this.Clone();
				bool flag = dataSet.EnforceConstraints;
				dataSet.EnforceConstraints = false;
				foreach (object obj in this.Tables)
				{
					DataTable dataTable = (DataTable)obj;
					DataTable table = dataSet.Tables[dataTable.TableName, dataTable.Namespace];
					foreach (object obj2 in dataTable.Rows)
					{
						DataRow row = (DataRow)obj2;
						dataTable.CopyRow(table, row);
					}
				}
				dataSet.EnforceConstraints = flag;
				result = dataSet;
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
			return result;
		}

		// Token: 0x06000BE8 RID: 3048 RVA: 0x000654A4 File Offset: 0x000648A4
		internal int EstimatedXmlStringSize()
		{
			int num = 100;
			for (int i = 0; i < this.Tables.Count; i++)
			{
				int num2 = this.Tables[i].TableName.Length + 4 << 2;
				DataTable dataTable = this.Tables[i];
				for (int j = 0; j < dataTable.Columns.Count; j++)
				{
					num2 += dataTable.Columns[j].ColumnName.Length + 4 << 2;
					num2 += 20;
				}
				num += dataTable.Rows.Count * num2;
			}
			return num;
		}

		// Token: 0x06000BE9 RID: 3049 RVA: 0x00065548 File Offset: 0x00064948
		public DataSet GetChanges()
		{
			return this.GetChanges(DataRowState.Added | DataRowState.Deleted | DataRowState.Modified);
		}

		// Token: 0x06000BEA RID: 3050 RVA: 0x00065560 File Offset: 0x00064960
		public DataSet GetChanges(DataRowState rowStates)
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<ds.DataSet.GetChanges|API> %d#, rowStates=%d{ds.DataRowState}\n", this.ObjectID, (int)rowStates);
			DataSet result;
			try
			{
				DataSet dataSet = null;
				bool flag = false;
				if ((rowStates & ~(DataRowState.Unchanged | DataRowState.Added | DataRowState.Deleted | DataRowState.Modified)) != (DataRowState)0)
				{
					throw ExceptionBuilder.InvalidRowState(rowStates);
				}
				DataSet.TableChanges[] array = new DataSet.TableChanges[this.Tables.Count];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = new DataSet.TableChanges(this.Tables[i].Rows.Count);
				}
				this.MarkModifiedRows(array, rowStates);
				for (int j = 0; j < array.Length; j++)
				{
					if (0 < array[j].HasChanges)
					{
						if (dataSet == null)
						{
							dataSet = this.Clone();
							flag = dataSet.EnforceConstraints;
							dataSet.EnforceConstraints = false;
						}
						DataTable dataTable = this.Tables[j];
						DataTable table = dataSet.Tables[dataTable.TableName, dataTable.Namespace];
						int num = 0;
						while (0 < array[j].HasChanges)
						{
							if (array[j][num])
							{
								dataTable.CopyRow(table, dataTable.Rows[num]);
								DataSet.TableChanges[] array2 = array;
								int num2 = j;
								int hasChanges = array2[num2].HasChanges;
								array2[num2].HasChanges = hasChanges - 1;
							}
							num++;
						}
					}
				}
				if (dataSet != null)
				{
					dataSet.EnforceConstraints = flag;
				}
				result = dataSet;
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
			return result;
		}

		// Token: 0x06000BEB RID: 3051 RVA: 0x000656D4 File Offset: 0x00064AD4
		private void MarkModifiedRows(DataSet.TableChanges[] bitMatrix, DataRowState rowStates)
		{
			for (int i = 0; i < bitMatrix.Length; i++)
			{
				DataRowCollection rows = this.Tables[i].Rows;
				int count = rows.Count;
				for (int j = 0; j < count; j++)
				{
					DataRow dataRow = rows[j];
					DataRowState rowState = dataRow.RowState;
					if ((rowStates & rowState) != (DataRowState)0 && !bitMatrix[i][j])
					{
						bitMatrix[i][j] = true;
						if (DataRowState.Deleted != rowState)
						{
							this.MarkRelatedRowsAsModified(bitMatrix, dataRow);
						}
					}
				}
			}
		}

		// Token: 0x06000BEC RID: 3052 RVA: 0x00065758 File Offset: 0x00064B58
		private void MarkRelatedRowsAsModified(DataSet.TableChanges[] bitMatrix, DataRow row)
		{
			DataRelationCollection parentRelations = row.Table.ParentRelations;
			int count = parentRelations.Count;
			for (int i = 0; i < count; i++)
			{
				DataRow[] parentRows = row.GetParentRows(parentRelations[i], DataRowVersion.Current);
				foreach (DataRow dataRow in parentRows)
				{
					int num = this.Tables.IndexOf(dataRow.Table);
					int index = dataRow.Table.Rows.IndexOf(dataRow);
					if (!bitMatrix[num][index])
					{
						bitMatrix[num][index] = true;
						if (DataRowState.Deleted != dataRow.RowState)
						{
							this.MarkRelatedRowsAsModified(bitMatrix, dataRow);
						}
					}
				}
			}
		}

		// Token: 0x06000BED RID: 3053 RVA: 0x00065810 File Offset: 0x00064C10
		IList IListSource.GetList()
		{
			return this.DefaultViewManager;
		}

		// Token: 0x06000BEE RID: 3054 RVA: 0x00065824 File Offset: 0x00064C24
		internal string GetRemotingDiffGram(DataTable table)
		{
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
			xmlTextWriter.Formatting = Formatting.Indented;
			if (stringWriter != null)
			{
				new NewDiffgramGen(table, false).Save(xmlTextWriter, table);
			}
			return stringWriter.ToString();
		}

		// Token: 0x06000BEF RID: 3055 RVA: 0x00065864 File Offset: 0x00064C64
		public string GetXml()
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<ds.DataSet.GetXml|API> %d#\n", this.ObjectID);
			string result;
			try
			{
				StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
				if (stringWriter != null)
				{
					XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
					xmlTextWriter.Formatting = Formatting.Indented;
					new XmlDataTreeWriter(this).Save(xmlTextWriter, false);
				}
				result = stringWriter.ToString();
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
			return result;
		}

		// Token: 0x06000BF0 RID: 3056 RVA: 0x000658DC File Offset: 0x00064CDC
		public string GetXmlSchema()
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<ds.DataSet.GetXmlSchema|API> %d#\n", this.ObjectID);
			string result;
			try
			{
				StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
				XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
				xmlTextWriter.Formatting = Formatting.Indented;
				if (stringWriter != null)
				{
					new XmlTreeGen(SchemaFormat.Public).Save(this, xmlTextWriter);
				}
				result = stringWriter.ToString();
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
			return result;
		}

		// Token: 0x06000BF1 RID: 3057 RVA: 0x00065954 File Offset: 0x00064D54
		internal string GetXmlSchemaForRemoting(DataTable table)
		{
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
			xmlTextWriter.Formatting = Formatting.Indented;
			if (stringWriter != null)
			{
				if (table == null)
				{
					if (this.SchemaSerializationMode == SchemaSerializationMode.ExcludeSchema)
					{
						new XmlTreeGen(SchemaFormat.RemotingSkipSchema).Save(this, xmlTextWriter);
					}
					else
					{
						new XmlTreeGen(SchemaFormat.Remoting).Save(this, xmlTextWriter);
					}
				}
				else
				{
					new XmlTreeGen(SchemaFormat.Remoting).Save(table, xmlTextWriter);
				}
			}
			return stringWriter.ToString();
		}

		// Token: 0x06000BF2 RID: 3058 RVA: 0x000659BC File Offset: 0x00064DBC
		public bool HasChanges()
		{
			return this.HasChanges(DataRowState.Added | DataRowState.Deleted | DataRowState.Modified);
		}

		// Token: 0x06000BF3 RID: 3059 RVA: 0x000659D4 File Offset: 0x00064DD4
		public bool HasChanges(DataRowState rowStates)
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<ds.DataSet.HasChanges|API> %d#, rowStates=%d{ds.DataRowState}\n", this.ObjectID, (int)rowStates);
			bool result;
			try
			{
				if ((rowStates & ~(DataRowState.Detached | DataRowState.Unchanged | DataRowState.Added | DataRowState.Deleted | DataRowState.Modified)) != (DataRowState)0)
				{
					throw ExceptionBuilder.ArgumentOutOfRange("rowState");
				}
				for (int i = 0; i < this.Tables.Count; i++)
				{
					DataTable dataTable = this.Tables[i];
					for (int j = 0; j < dataTable.Rows.Count; j++)
					{
						DataRow dataRow = dataTable.Rows[j];
						if ((dataRow.RowState & rowStates) != (DataRowState)0)
						{
							return true;
						}
					}
				}
				result = false;
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
			return result;
		}

		// Token: 0x06000BF4 RID: 3060 RVA: 0x00065A84 File Offset: 0x00064E84
		public void InferXmlSchema(XmlReader reader, string[] nsArray)
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<ds.DataSet.InferXmlSchema|API> %d#\n", this.ObjectID);
			try
			{
				if (reader != null)
				{
					XmlDocument xmlDocument = new XmlDocument();
					if (reader.NodeType == XmlNodeType.Element)
					{
						XmlNode newChild = xmlDocument.ReadNode(reader);
						xmlDocument.AppendChild(newChild);
					}
					else
					{
						xmlDocument.Load(reader);
					}
					if (xmlDocument.DocumentElement != null)
					{
						this.InferSchema(xmlDocument, nsArray, XmlReadMode.InferSchema);
					}
				}
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x06000BF5 RID: 3061 RVA: 0x00065B0C File Offset: 0x00064F0C
		public void InferXmlSchema(Stream stream, string[] nsArray)
		{
			if (stream == null)
			{
				return;
			}
			this.InferXmlSchema(new XmlTextReader(stream), nsArray);
		}

		// Token: 0x06000BF6 RID: 3062 RVA: 0x00065B2C File Offset: 0x00064F2C
		public void InferXmlSchema(TextReader reader, string[] nsArray)
		{
			if (reader == null)
			{
				return;
			}
			this.InferXmlSchema(new XmlTextReader(reader), nsArray);
		}

		// Token: 0x06000BF7 RID: 3063 RVA: 0x00065B4C File Offset: 0x00064F4C
		public void InferXmlSchema(string fileName, string[] nsArray)
		{
			XmlTextReader xmlTextReader = new XmlTextReader(fileName);
			try
			{
				this.InferXmlSchema(xmlTextReader, nsArray);
			}
			finally
			{
				xmlTextReader.Close();
			}
		}

		// Token: 0x06000BF8 RID: 3064 RVA: 0x00065B90 File Offset: 0x00064F90
		public void ReadXmlSchema(XmlReader reader)
		{
			this.ReadXmlSchema(reader, false);
		}

		// Token: 0x06000BF9 RID: 3065 RVA: 0x00065BA8 File Offset: 0x00064FA8
		internal void ReadXmlSchema(XmlReader reader, bool denyResolving)
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<ds.DataSet.ReadXmlSchema|INFO> %d#, reader, denyResolving=%d{bool}\n", this.ObjectID, denyResolving);
			try
			{
				int depth = -1;
				if (reader != null)
				{
					if (reader is XmlTextReader)
					{
						((XmlTextReader)reader).WhitespaceHandling = WhitespaceHandling.None;
					}
					XmlDocument xmlDocument = new XmlDocument();
					if (reader.NodeType == XmlNodeType.Element)
					{
						depth = reader.Depth;
					}
					reader.MoveToContent();
					if (reader.NodeType == XmlNodeType.Element)
					{
						if (reader.LocalName == "Schema" && reader.NamespaceURI == "urn:schemas-microsoft-com:xml-data")
						{
							this.ReadXDRSchema(reader);
						}
						else if (reader.LocalName == "schema" && reader.NamespaceURI == "http://www.w3.org/2001/XMLSchema")
						{
							this.ReadXSDSchema(reader, denyResolving);
						}
						else
						{
							if (reader.LocalName == "schema" && reader.NamespaceURI.StartsWith("http://www.w3.org/", StringComparison.Ordinal))
							{
								throw ExceptionBuilder.DataSetUnsupportedSchema("http://www.w3.org/2001/XMLSchema");
							}
							XmlElement xmlElement = xmlDocument.CreateElement(reader.Prefix, reader.LocalName, reader.NamespaceURI);
							if (reader.HasAttributes)
							{
								int attributeCount = reader.AttributeCount;
								for (int i = 0; i < attributeCount; i++)
								{
									reader.MoveToAttribute(i);
									if (reader.NamespaceURI.Equals("http://www.w3.org/2000/xmlns/"))
									{
										xmlElement.SetAttribute(reader.Name, reader.GetAttribute(i));
									}
									else
									{
										XmlAttribute xmlAttribute = xmlElement.SetAttributeNode(reader.LocalName, reader.NamespaceURI);
										xmlAttribute.Prefix = reader.Prefix;
										xmlAttribute.Value = reader.GetAttribute(i);
									}
								}
							}
							reader.Read();
							while (this.MoveToElement(reader, depth))
							{
								if (reader.LocalName == "Schema" && reader.NamespaceURI == "urn:schemas-microsoft-com:xml-data")
								{
									this.ReadXDRSchema(reader);
									return;
								}
								if (reader.LocalName == "schema" && reader.NamespaceURI == "http://www.w3.org/2001/XMLSchema")
								{
									this.ReadXSDSchema(reader, denyResolving);
									return;
								}
								if (reader.LocalName == "schema" && reader.NamespaceURI.StartsWith("http://www.w3.org/", StringComparison.Ordinal))
								{
									throw ExceptionBuilder.DataSetUnsupportedSchema("http://www.w3.org/2001/XMLSchema");
								}
								XmlNode newChild = xmlDocument.ReadNode(reader);
								xmlElement.AppendChild(newChild);
							}
							this.ReadEndElement(reader);
							xmlDocument.AppendChild(xmlElement);
							this.InferSchema(xmlDocument, null, XmlReadMode.Auto);
						}
					}
				}
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x06000BFA RID: 3066 RVA: 0x00065E2C File Offset: 0x0006522C
		internal bool MoveToElement(XmlReader reader, int depth)
		{
			while (!reader.EOF && reader.NodeType != XmlNodeType.EndElement && reader.NodeType != XmlNodeType.Element && reader.Depth > depth)
			{
				reader.Read();
			}
			return reader.NodeType == XmlNodeType.Element;
		}

		// Token: 0x06000BFB RID: 3067 RVA: 0x00065E70 File Offset: 0x00065270
		private static void MoveToElement(XmlReader reader)
		{
			while (!reader.EOF && reader.NodeType != XmlNodeType.EndElement && reader.NodeType != XmlNodeType.Element)
			{
				reader.Read();
			}
		}

		// Token: 0x06000BFC RID: 3068 RVA: 0x00065EA4 File Offset: 0x000652A4
		internal void ReadEndElement(XmlReader reader)
		{
			while (reader.NodeType == XmlNodeType.Whitespace)
			{
				reader.Skip();
			}
			if (reader.NodeType == XmlNodeType.None)
			{
				reader.Skip();
				return;
			}
			if (reader.NodeType == XmlNodeType.EndElement)
			{
				reader.ReadEndElement();
			}
		}

		// Token: 0x06000BFD RID: 3069 RVA: 0x00065EE4 File Offset: 0x000652E4
		internal void ReadXSDSchema(XmlReader reader, bool denyResolving)
		{
			XmlSchemaSet xmlSchemaSet = new XmlSchemaSet();
			int num = 1;
			if (reader.LocalName == "schema" && reader.NamespaceURI == "http://www.w3.org/2001/XMLSchema" && reader.HasAttributes)
			{
				string attribute = reader.GetAttribute("schemafragmentcount", "urn:schemas-microsoft-com:xml-msdata");
				if (!ADP.IsEmpty(attribute))
				{
					num = int.Parse(attribute, null);
				}
			}
			while (reader.LocalName == "schema" && reader.NamespaceURI == "http://www.w3.org/2001/XMLSchema")
			{
				XmlSchema schema = XmlSchema.Read(reader, null);
				xmlSchemaSet.Add(schema);
				this.ReadEndElement(reader);
				if (--num > 0)
				{
					DataSet.MoveToElement(reader);
				}
				while (reader.NodeType == XmlNodeType.Whitespace)
				{
					reader.Skip();
				}
			}
			xmlSchemaSet.Compile();
			XSDSchema xsdschema = new XSDSchema();
			xsdschema.LoadSchema(xmlSchemaSet, this);
		}

		// Token: 0x06000BFE RID: 3070 RVA: 0x00065FBC File Offset: 0x000653BC
		internal void ReadXDRSchema(XmlReader reader)
		{
			XmlDocument xmlDocument = new XmlDocument();
			XmlNode xmlNode = xmlDocument.ReadNode(reader);
			xmlDocument.AppendChild(xmlNode);
			XDRSchema xdrschema = new XDRSchema(this, false);
			this.DataSetName = xmlDocument.DocumentElement.LocalName;
			xdrschema.LoadSchema((XmlElement)xmlNode, this);
		}

		// Token: 0x06000BFF RID: 3071 RVA: 0x00066008 File Offset: 0x00065408
		public void ReadXmlSchema(Stream stream)
		{
			if (stream == null)
			{
				return;
			}
			this.ReadXmlSchema(new XmlTextReader(stream), false);
		}

		// Token: 0x06000C00 RID: 3072 RVA: 0x00066028 File Offset: 0x00065428
		public void ReadXmlSchema(TextReader reader)
		{
			if (reader == null)
			{
				return;
			}
			this.ReadXmlSchema(new XmlTextReader(reader), false);
		}

		// Token: 0x06000C01 RID: 3073 RVA: 0x00066048 File Offset: 0x00065448
		public void ReadXmlSchema(string fileName)
		{
			XmlTextReader xmlTextReader = new XmlTextReader(fileName);
			try
			{
				this.ReadXmlSchema(xmlTextReader, false);
			}
			finally
			{
				xmlTextReader.Close();
			}
		}

		// Token: 0x06000C02 RID: 3074 RVA: 0x0006608C File Offset: 0x0006548C
		public void WriteXmlSchema(Stream stream)
		{
			this.WriteXmlSchema(stream, SchemaFormat.Public, null);
		}

		// Token: 0x06000C03 RID: 3075 RVA: 0x000660A4 File Offset: 0x000654A4
		public void WriteXmlSchema(Stream stream, Converter<Type, string> multipleTargetConverter)
		{
			ADP.CheckArgumentNull(multipleTargetConverter, "multipleTargetConverter");
			this.WriteXmlSchema(stream, SchemaFormat.Public, multipleTargetConverter);
		}

		// Token: 0x06000C04 RID: 3076 RVA: 0x000660C8 File Offset: 0x000654C8
		public void WriteXmlSchema(string fileName)
		{
			this.WriteXmlSchema(fileName, SchemaFormat.Public, null);
		}

		// Token: 0x06000C05 RID: 3077 RVA: 0x000660E0 File Offset: 0x000654E0
		public void WriteXmlSchema(string fileName, Converter<Type, string> multipleTargetConverter)
		{
			ADP.CheckArgumentNull(multipleTargetConverter, "multipleTargetConverter");
			this.WriteXmlSchema(fileName, SchemaFormat.Public, multipleTargetConverter);
		}

		// Token: 0x06000C06 RID: 3078 RVA: 0x00066104 File Offset: 0x00065504
		public void WriteXmlSchema(TextWriter writer)
		{
			this.WriteXmlSchema(writer, SchemaFormat.Public, null);
		}

		// Token: 0x06000C07 RID: 3079 RVA: 0x0006611C File Offset: 0x0006551C
		public void WriteXmlSchema(TextWriter writer, Converter<Type, string> multipleTargetConverter)
		{
			ADP.CheckArgumentNull(multipleTargetConverter, "multipleTargetConverter");
			this.WriteXmlSchema(writer, SchemaFormat.Public, multipleTargetConverter);
		}

		// Token: 0x06000C08 RID: 3080 RVA: 0x00066140 File Offset: 0x00065540
		public void WriteXmlSchema(XmlWriter writer)
		{
			this.WriteXmlSchema(writer, SchemaFormat.Public, null);
		}

		// Token: 0x06000C09 RID: 3081 RVA: 0x00066158 File Offset: 0x00065558
		public void WriteXmlSchema(XmlWriter writer, Converter<Type, string> multipleTargetConverter)
		{
			ADP.CheckArgumentNull(multipleTargetConverter, "multipleTargetConverter");
			this.WriteXmlSchema(writer, SchemaFormat.Public, multipleTargetConverter);
		}

		// Token: 0x06000C0A RID: 3082 RVA: 0x0006617C File Offset: 0x0006557C
		private void WriteXmlSchema(string fileName, SchemaFormat schemaFormat, Converter<Type, string> multipleTargetConverter)
		{
			XmlTextWriter xmlTextWriter = new XmlTextWriter(fileName, null);
			try
			{
				xmlTextWriter.Formatting = Formatting.Indented;
				xmlTextWriter.WriteStartDocument(true);
				this.WriteXmlSchema(xmlTextWriter, schemaFormat, multipleTargetConverter);
				xmlTextWriter.WriteEndDocument();
			}
			finally
			{
				xmlTextWriter.Close();
			}
		}

		// Token: 0x06000C0B RID: 3083 RVA: 0x000661D4 File Offset: 0x000655D4
		private void WriteXmlSchema(Stream stream, SchemaFormat schemaFormat, Converter<Type, string> multipleTargetConverter)
		{
			if (stream == null)
			{
				return;
			}
			this.WriteXmlSchema(new XmlTextWriter(stream, null)
			{
				Formatting = Formatting.Indented
			}, schemaFormat, multipleTargetConverter);
		}

		// Token: 0x06000C0C RID: 3084 RVA: 0x00066200 File Offset: 0x00065600
		private void WriteXmlSchema(TextWriter writer, SchemaFormat schemaFormat, Converter<Type, string> multipleTargetConverter)
		{
			if (writer == null)
			{
				return;
			}
			this.WriteXmlSchema(new XmlTextWriter(writer)
			{
				Formatting = Formatting.Indented
			}, schemaFormat, multipleTargetConverter);
		}

		// Token: 0x06000C0D RID: 3085 RVA: 0x00066228 File Offset: 0x00065628
		private void WriteXmlSchema(XmlWriter writer, SchemaFormat schemaFormat, Converter<Type, string> multipleTargetConverter)
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<ds.DataSet.WriteXmlSchema|INFO> %d#, schemaFormat=%d{ds.SchemaFormat}\n", this.ObjectID, (int)schemaFormat);
			try
			{
				if (writer != null)
				{
					XmlTreeGen xmlTreeGen;
					if (schemaFormat == SchemaFormat.WebService && this.SchemaSerializationMode == SchemaSerializationMode.ExcludeSchema && writer.WriteState == WriteState.Element)
					{
						xmlTreeGen = new XmlTreeGen(SchemaFormat.WebServiceSkipSchema);
					}
					else
					{
						xmlTreeGen = new XmlTreeGen(schemaFormat);
					}
					xmlTreeGen.Save(this, null, writer, false, multipleTargetConverter);
				}
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x06000C0E RID: 3086 RVA: 0x000662A4 File Offset: 0x000656A4
		public XmlReadMode ReadXml(XmlReader reader)
		{
			return this.ReadXml(reader, false);
		}

		// Token: 0x06000C0F RID: 3087 RVA: 0x000662BC File Offset: 0x000656BC
		internal XmlReadMode ReadXml(XmlReader reader, bool denyResolving)
		{
			IDisposable disposable = null;
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<ds.DataSet.ReadXml|INFO> %d#, denyResolving=%d{bool}\n", this.ObjectID, denyResolving);
			XmlReadMode result;
			try
			{
				disposable = TypeLimiter.EnterRestrictedScope(this);
				DataTable.DSRowDiffIdUsageSection dsrowDiffIdUsageSection = default(DataTable.DSRowDiffIdUsageSection);
				try
				{
					bool flag = false;
					bool flag2 = false;
					bool flag3 = false;
					bool isXdr = false;
					int depth = -1;
					XmlReadMode xmlReadMode = XmlReadMode.Auto;
					bool flag4 = false;
					bool flag5 = false;
					dsrowDiffIdUsageSection.Prepare(this);
					if (reader == null)
					{
						result = xmlReadMode;
					}
					else
					{
						if (this.Tables.Count == 0)
						{
							flag4 = true;
						}
						if (reader is XmlTextReader)
						{
							((XmlTextReader)reader).WhitespaceHandling = WhitespaceHandling.Significant;
						}
						XmlDocument xmlDocument = new XmlDocument();
						XmlDataLoader xmlDataLoader = null;
						reader.MoveToContent();
						if (reader.NodeType == XmlNodeType.Element)
						{
							depth = reader.Depth;
						}
						if (reader.NodeType == XmlNodeType.Element)
						{
							if (reader.LocalName == "diffgram" && reader.NamespaceURI == "urn:schemas-microsoft-com:xml-diffgram-v1")
							{
								this.ReadXmlDiffgram(reader);
								this.ReadEndElement(reader);
								return XmlReadMode.DiffGram;
							}
							if (reader.LocalName == "Schema" && reader.NamespaceURI == "urn:schemas-microsoft-com:xml-data")
							{
								this.ReadXDRSchema(reader);
								return XmlReadMode.ReadSchema;
							}
							if (reader.LocalName == "schema" && reader.NamespaceURI == "http://www.w3.org/2001/XMLSchema")
							{
								this.ReadXSDSchema(reader, denyResolving);
								return XmlReadMode.ReadSchema;
							}
							if (reader.LocalName == "schema" && reader.NamespaceURI.StartsWith("http://www.w3.org/", StringComparison.Ordinal))
							{
								throw ExceptionBuilder.DataSetUnsupportedSchema("http://www.w3.org/2001/XMLSchema");
							}
							XmlElement xmlElement = xmlDocument.CreateElement(reader.Prefix, reader.LocalName, reader.NamespaceURI);
							if (reader.HasAttributes)
							{
								int attributeCount = reader.AttributeCount;
								for (int i = 0; i < attributeCount; i++)
								{
									reader.MoveToAttribute(i);
									if (reader.NamespaceURI.Equals("http://www.w3.org/2000/xmlns/"))
									{
										xmlElement.SetAttribute(reader.Name, reader.GetAttribute(i));
									}
									else
									{
										XmlAttribute xmlAttribute = xmlElement.SetAttributeNode(reader.LocalName, reader.NamespaceURI);
										xmlAttribute.Prefix = reader.Prefix;
										xmlAttribute.Value = reader.GetAttribute(i);
									}
								}
							}
							reader.Read();
							string value = reader.Value;
							while (this.MoveToElement(reader, depth))
							{
								if (reader.LocalName == "diffgram" && reader.NamespaceURI == "urn:schemas-microsoft-com:xml-diffgram-v1")
								{
									this.ReadXmlDiffgram(reader);
									xmlReadMode = XmlReadMode.DiffGram;
								}
								if (!flag2 && !flag && reader.LocalName == "Schema" && reader.NamespaceURI == "urn:schemas-microsoft-com:xml-data")
								{
									this.ReadXDRSchema(reader);
									flag2 = true;
									isXdr = true;
								}
								else if (reader.LocalName == "schema" && reader.NamespaceURI == "http://www.w3.org/2001/XMLSchema")
								{
									this.ReadXSDSchema(reader, denyResolving);
									flag2 = true;
								}
								else
								{
									if (reader.LocalName == "schema" && reader.NamespaceURI.StartsWith("http://www.w3.org/", StringComparison.Ordinal))
									{
										throw ExceptionBuilder.DataSetUnsupportedSchema("http://www.w3.org/2001/XMLSchema");
									}
									if (reader.LocalName == "diffgram" && reader.NamespaceURI == "urn:schemas-microsoft-com:xml-diffgram-v1")
									{
										this.ReadXmlDiffgram(reader);
										flag3 = true;
										xmlReadMode = XmlReadMode.DiffGram;
									}
									else
									{
										while (!reader.EOF && reader.NodeType == XmlNodeType.Whitespace)
										{
											reader.Read();
										}
										if (reader.NodeType == XmlNodeType.Element)
										{
											flag = true;
											if (!flag2 && this.Tables.Count == 0)
											{
												XmlNode newChild = xmlDocument.ReadNode(reader);
												xmlElement.AppendChild(newChild);
											}
											else
											{
												if (xmlDataLoader == null)
												{
													xmlDataLoader = new XmlDataLoader(this, isXdr, xmlElement, false);
												}
												xmlDataLoader.LoadData(reader);
												flag5 = true;
												if (flag2)
												{
													xmlReadMode = XmlReadMode.ReadSchema;
												}
												else
												{
													xmlReadMode = XmlReadMode.IgnoreSchema;
												}
											}
										}
									}
								}
							}
							this.ReadEndElement(reader);
							bool flag6 = false;
							bool flag7 = this.fTopLevelTable;
							if (!flag2 && this.Tables.Count == 0 && !xmlElement.HasChildNodes)
							{
								this.fTopLevelTable = true;
								flag6 = true;
								if (value != null && value.Length > 0)
								{
									xmlElement.InnerText = value;
								}
							}
							if (!flag4 && value != null && value.Length > 0)
							{
								xmlElement.InnerText = value;
							}
							xmlDocument.AppendChild(xmlElement);
							if (xmlDataLoader == null)
							{
								xmlDataLoader = new XmlDataLoader(this, isXdr, xmlElement, false);
							}
							if (!flag4 && !flag5)
							{
								XmlElement documentElement = xmlDocument.DocumentElement;
								if (documentElement.ChildNodes.Count == 0 || (documentElement.ChildNodes.Count == 1 && documentElement.FirstChild.GetType() == typeof(XmlText)))
								{
									bool flag8 = this.fTopLevelTable;
									if (this.DataSetName != documentElement.Name && this.namespaceURI != documentElement.NamespaceURI && this.Tables.Contains(documentElement.Name, (documentElement.NamespaceURI.Length == 0) ? null : documentElement.NamespaceURI, false, true))
									{
										this.fTopLevelTable = true;
									}
									try
									{
										xmlDataLoader.LoadData(xmlDocument);
									}
									finally
									{
										this.fTopLevelTable = flag8;
									}
								}
							}
							if (!flag3)
							{
								if (!flag2 && this.Tables.Count == 0)
								{
									this.InferSchema(xmlDocument, null, XmlReadMode.Auto);
									xmlReadMode = XmlReadMode.InferSchema;
									xmlDataLoader.FromInference = true;
									try
									{
										xmlDataLoader.LoadData(xmlDocument);
									}
									finally
									{
										xmlDataLoader.FromInference = false;
									}
								}
								if (flag6)
								{
									this.fTopLevelTable = flag7;
								}
							}
						}
						result = xmlReadMode;
					}
				}
				finally
				{
				}
			}
			finally
			{
				if (disposable != null)
				{
					disposable.Dispose();
				}
				Bid.ScopeLeave(ref intPtr);
			}
			return result;
		}

		// Token: 0x06000C10 RID: 3088 RVA: 0x00066870 File Offset: 0x00065C70
		public XmlReadMode ReadXml(Stream stream)
		{
			if (stream == null)
			{
				return XmlReadMode.Auto;
			}
			return this.ReadXml(new XmlTextReader(stream)
			{
				XmlResolver = null
			}, false);
		}

		// Token: 0x06000C11 RID: 3089 RVA: 0x00066898 File Offset: 0x00065C98
		public XmlReadMode ReadXml(TextReader reader)
		{
			if (reader == null)
			{
				return XmlReadMode.Auto;
			}
			return this.ReadXml(new XmlTextReader(reader)
			{
				XmlResolver = null
			}, false);
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x000668C0 File Offset: 0x00065CC0
		public XmlReadMode ReadXml(string fileName)
		{
			XmlTextReader xmlTextReader = new XmlTextReader(fileName);
			xmlTextReader.XmlResolver = null;
			XmlReadMode result;
			try
			{
				result = this.ReadXml(xmlTextReader, false);
			}
			finally
			{
				xmlTextReader.Close();
			}
			return result;
		}

		// Token: 0x06000C13 RID: 3091 RVA: 0x0006690C File Offset: 0x00065D0C
		internal void InferSchema(XmlDocument xdoc, string[] excludedNamespaces, XmlReadMode mode)
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<ds.DataSet.InferSchema|INFO> %d#, mode=%d{ds.XmlReadMode}\n", this.ObjectID, (int)mode);
			try
			{
				string text = xdoc.DocumentElement.NamespaceURI;
				if (excludedNamespaces == null)
				{
					excludedNamespaces = new string[0];
				}
				XmlNodeReader instanceDocument = new XmlIgnoreNamespaceReader(xdoc, excludedNamespaces);
				XmlSchemaInference xmlSchemaInference = new XmlSchemaInference();
				xmlSchemaInference.Occurrence = XmlSchemaInference.InferenceOption.Relaxed;
				if (mode == XmlReadMode.InferTypedSchema)
				{
					xmlSchemaInference.TypeInference = XmlSchemaInference.InferenceOption.Restricted;
				}
				else
				{
					xmlSchemaInference.TypeInference = XmlSchemaInference.InferenceOption.Relaxed;
				}
				XmlSchemaSet xmlSchemaSet = xmlSchemaInference.InferSchema(instanceDocument);
				xmlSchemaSet.Compile();
				XSDSchema xsdschema = new XSDSchema();
				xsdschema.FromInference = true;
				try
				{
					xsdschema.LoadSchema(xmlSchemaSet, this);
				}
				finally
				{
					xsdschema.FromInference = false;
				}
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x06000C14 RID: 3092 RVA: 0x000669D8 File Offset: 0x00065DD8
		private bool IsEmpty()
		{
			foreach (object obj in this.Tables)
			{
				DataTable dataTable = (DataTable)obj;
				if (dataTable.Rows.Count > 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000C15 RID: 3093 RVA: 0x00066A4C File Offset: 0x00065E4C
		private void ReadXmlDiffgram(XmlReader reader)
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<ds.DataSet.ReadXmlDiffgram|INFO> %d#\n", this.ObjectID);
			try
			{
				int depth = reader.Depth;
				bool flag = this.EnforceConstraints;
				this.EnforceConstraints = false;
				bool flag2 = this.IsEmpty();
				DataSet dataSet;
				if (flag2)
				{
					dataSet = this;
				}
				else
				{
					dataSet = this.Clone();
					dataSet.EnforceConstraints = false;
				}
				foreach (object obj in dataSet.Tables)
				{
					DataTable dataTable = (DataTable)obj;
					dataTable.Rows.nullInList = 0;
				}
				reader.MoveToContent();
				if (!(reader.LocalName != "diffgram") || !(reader.NamespaceURI != "urn:schemas-microsoft-com:xml-diffgram-v1"))
				{
					reader.Read();
					if (reader.NodeType == XmlNodeType.Whitespace)
					{
						this.MoveToElement(reader, reader.Depth - 1);
					}
					dataSet.fInLoadDiffgram = true;
					if (reader.Depth > depth)
					{
						if (reader.NamespaceURI != "urn:schemas-microsoft-com:xml-diffgram-v1" && reader.NamespaceURI != "urn:schemas-microsoft-com:xml-msdata")
						{
							XmlDocument xmlDocument = new XmlDocument();
							XmlElement topNode = xmlDocument.CreateElement(reader.Prefix, reader.LocalName, reader.NamespaceURI);
							reader.Read();
							if (reader.NodeType == XmlNodeType.Whitespace)
							{
								this.MoveToElement(reader, reader.Depth - 1);
							}
							if (reader.Depth - 1 > depth)
							{
								new XmlDataLoader(dataSet, false, topNode, false)
								{
									isDiffgram = true
								}.LoadData(reader);
							}
							this.ReadEndElement(reader);
							if (reader.NodeType == XmlNodeType.Whitespace)
							{
								this.MoveToElement(reader, reader.Depth - 1);
							}
						}
						if ((reader.LocalName == "before" && reader.NamespaceURI == "urn:schemas-microsoft-com:xml-diffgram-v1") || (reader.LocalName == "errors" && reader.NamespaceURI == "urn:schemas-microsoft-com:xml-diffgram-v1"))
						{
							XMLDiffLoader xmldiffLoader = new XMLDiffLoader();
							xmldiffLoader.LoadDiffGram(dataSet, reader);
						}
						while (reader.Depth > depth)
						{
							reader.Read();
						}
						this.ReadEndElement(reader);
					}
					foreach (object obj2 in dataSet.Tables)
					{
						DataTable dataTable2 = (DataTable)obj2;
						if (dataTable2.Rows.nullInList > 0)
						{
							throw ExceptionBuilder.RowInsertMissing(dataTable2.TableName);
						}
					}
					dataSet.fInLoadDiffgram = false;
					foreach (object obj3 in dataSet.Tables)
					{
						DataTable dataTable3 = (DataTable)obj3;
						DataRelation[] nestedParentRelations = dataTable3.NestedParentRelations;
						foreach (DataRelation dataRelation in nestedParentRelations)
						{
							if (dataRelation.ParentTable == dataTable3)
							{
								foreach (object obj4 in dataTable3.Rows)
								{
									DataRow dataRow = (DataRow)obj4;
									foreach (DataRelation rel in nestedParentRelations)
									{
										dataRow.CheckForLoops(rel);
									}
								}
							}
						}
					}
					if (!flag2)
					{
						this.Merge(dataSet);
						if (this.dataSetName == "NewDataSet")
						{
							this.dataSetName = dataSet.dataSetName;
						}
						dataSet.EnforceConstraints = flag;
					}
					this.EnforceConstraints = flag;
				}
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x06000C16 RID: 3094 RVA: 0x00066E5C File Offset: 0x0006625C
		public XmlReadMode ReadXml(XmlReader reader, XmlReadMode mode)
		{
			return this.ReadXml(reader, mode, false);
		}

		// Token: 0x06000C17 RID: 3095 RVA: 0x00066E74 File Offset: 0x00066274
		internal XmlReadMode ReadXml(XmlReader reader, XmlReadMode mode, bool denyResolving)
		{
			IDisposable disposable = null;
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<ds.DataSet.ReadXml|INFO> %d#, mode=%d{ds.XmlReadMode}, denyResolving=%d{bool}\n", this.ObjectID, (int)mode, denyResolving);
			XmlReadMode result;
			try
			{
				disposable = TypeLimiter.EnterRestrictedScope(this);
				XmlReadMode xmlReadMode = mode;
				if (reader == null)
				{
					result = xmlReadMode;
				}
				else if (mode == XmlReadMode.Auto)
				{
					result = this.ReadXml(reader);
				}
				else
				{
					DataTable.DSRowDiffIdUsageSection dsrowDiffIdUsageSection = default(DataTable.DSRowDiffIdUsageSection);
					try
					{
						bool flag = false;
						bool flag2 = false;
						bool isXdr = false;
						int depth = -1;
						dsrowDiffIdUsageSection.Prepare(this);
						if (reader is XmlTextReader)
						{
							((XmlTextReader)reader).WhitespaceHandling = WhitespaceHandling.Significant;
						}
						XmlDocument xmlDocument = new XmlDocument();
						if (mode != XmlReadMode.Fragment && reader.NodeType == XmlNodeType.Element)
						{
							depth = reader.Depth;
						}
						reader.MoveToContent();
						XmlDataLoader xmlDataLoader = null;
						if (reader.NodeType == XmlNodeType.Element)
						{
							XmlElement xmlElement;
							if (mode == XmlReadMode.Fragment)
							{
								xmlDocument.AppendChild(xmlDocument.CreateElement("ds_sqlXmlWraPPeR"));
								xmlElement = xmlDocument.DocumentElement;
							}
							else
							{
								if (reader.LocalName == "diffgram" && reader.NamespaceURI == "urn:schemas-microsoft-com:xml-diffgram-v1")
								{
									if (mode == XmlReadMode.DiffGram || mode == XmlReadMode.IgnoreSchema)
									{
										this.ReadXmlDiffgram(reader);
										this.ReadEndElement(reader);
									}
									else
									{
										reader.Skip();
									}
									return xmlReadMode;
								}
								if (reader.LocalName == "Schema" && reader.NamespaceURI == "urn:schemas-microsoft-com:xml-data")
								{
									if (mode != XmlReadMode.IgnoreSchema && mode != XmlReadMode.InferSchema && mode != XmlReadMode.InferTypedSchema)
									{
										this.ReadXDRSchema(reader);
									}
									else
									{
										reader.Skip();
									}
									return xmlReadMode;
								}
								if (reader.LocalName == "schema" && reader.NamespaceURI == "http://www.w3.org/2001/XMLSchema")
								{
									if (mode != XmlReadMode.IgnoreSchema && mode != XmlReadMode.InferSchema && mode != XmlReadMode.InferTypedSchema)
									{
										this.ReadXSDSchema(reader, denyResolving);
									}
									else
									{
										reader.Skip();
									}
									return xmlReadMode;
								}
								if (reader.LocalName == "schema" && reader.NamespaceURI.StartsWith("http://www.w3.org/", StringComparison.Ordinal))
								{
									throw ExceptionBuilder.DataSetUnsupportedSchema("http://www.w3.org/2001/XMLSchema");
								}
								xmlElement = xmlDocument.CreateElement(reader.Prefix, reader.LocalName, reader.NamespaceURI);
								if (reader.HasAttributes)
								{
									int attributeCount = reader.AttributeCount;
									for (int i = 0; i < attributeCount; i++)
									{
										reader.MoveToAttribute(i);
										if (reader.NamespaceURI.Equals("http://www.w3.org/2000/xmlns/"))
										{
											xmlElement.SetAttribute(reader.Name, reader.GetAttribute(i));
										}
										else
										{
											XmlAttribute xmlAttribute = xmlElement.SetAttributeNode(reader.LocalName, reader.NamespaceURI);
											xmlAttribute.Prefix = reader.Prefix;
											xmlAttribute.Value = reader.GetAttribute(i);
										}
									}
								}
								reader.Read();
							}
							while (this.MoveToElement(reader, depth))
							{
								if (reader.LocalName == "Schema" && reader.NamespaceURI == "urn:schemas-microsoft-com:xml-data")
								{
									if (!flag && !flag2 && mode != XmlReadMode.IgnoreSchema && mode != XmlReadMode.InferSchema && mode != XmlReadMode.InferTypedSchema)
									{
										this.ReadXDRSchema(reader);
										flag = true;
										isXdr = true;
									}
									else
									{
										reader.Skip();
									}
								}
								else if (reader.LocalName == "schema" && reader.NamespaceURI == "http://www.w3.org/2001/XMLSchema")
								{
									if (mode != XmlReadMode.IgnoreSchema && mode != XmlReadMode.InferSchema && mode != XmlReadMode.InferTypedSchema)
									{
										this.ReadXSDSchema(reader, denyResolving);
										flag = true;
									}
									else
									{
										reader.Skip();
									}
								}
								else if (reader.LocalName == "diffgram" && reader.NamespaceURI == "urn:schemas-microsoft-com:xml-diffgram-v1")
								{
									if (mode == XmlReadMode.DiffGram || mode == XmlReadMode.IgnoreSchema)
									{
										this.ReadXmlDiffgram(reader);
										xmlReadMode = XmlReadMode.DiffGram;
									}
									else
									{
										reader.Skip();
									}
								}
								else
								{
									if (reader.LocalName == "schema" && reader.NamespaceURI.StartsWith("http://www.w3.org/", StringComparison.Ordinal))
									{
										throw ExceptionBuilder.DataSetUnsupportedSchema("http://www.w3.org/2001/XMLSchema");
									}
									if (mode == XmlReadMode.DiffGram)
									{
										reader.Skip();
									}
									else
									{
										flag2 = true;
										if (mode == XmlReadMode.InferSchema || mode == XmlReadMode.InferTypedSchema)
										{
											XmlNode newChild = xmlDocument.ReadNode(reader);
											xmlElement.AppendChild(newChild);
										}
										else
										{
											if (xmlDataLoader == null)
											{
												xmlDataLoader = new XmlDataLoader(this, isXdr, xmlElement, mode == XmlReadMode.IgnoreSchema);
											}
											xmlDataLoader.LoadData(reader);
										}
									}
								}
							}
							this.ReadEndElement(reader);
							xmlDocument.AppendChild(xmlElement);
							if (xmlDataLoader == null)
							{
								xmlDataLoader = new XmlDataLoader(this, isXdr, mode == XmlReadMode.IgnoreSchema);
							}
							if (mode == XmlReadMode.DiffGram)
							{
								return xmlReadMode;
							}
							if (mode == XmlReadMode.InferSchema || mode == XmlReadMode.InferTypedSchema)
							{
								this.InferSchema(xmlDocument, null, mode);
								xmlReadMode = XmlReadMode.InferSchema;
								xmlDataLoader.FromInference = true;
								try
								{
									xmlDataLoader.LoadData(xmlDocument);
								}
								finally
								{
									xmlDataLoader.FromInference = false;
								}
							}
						}
						result = xmlReadMode;
					}
					finally
					{
					}
				}
			}
			finally
			{
				if (disposable != null)
				{
					disposable.Dispose();
				}
				Bid.ScopeLeave(ref intPtr);
			}
			return result;
		}

		// Token: 0x06000C18 RID: 3096 RVA: 0x00067314 File Offset: 0x00066714
		public XmlReadMode ReadXml(Stream stream, XmlReadMode mode)
		{
			if (stream == null)
			{
				return XmlReadMode.Auto;
			}
			XmlTextReader xmlTextReader = (mode == XmlReadMode.Fragment) ? new XmlTextReader(stream, XmlNodeType.Element, null) : new XmlTextReader(stream);
			xmlTextReader.XmlResolver = null;
			return this.ReadXml(xmlTextReader, mode, false);
		}

		// Token: 0x06000C19 RID: 3097 RVA: 0x0006734C File Offset: 0x0006674C
		public XmlReadMode ReadXml(TextReader reader, XmlReadMode mode)
		{
			if (reader == null)
			{
				return XmlReadMode.Auto;
			}
			XmlTextReader xmlTextReader = (mode == XmlReadMode.Fragment) ? new XmlTextReader(reader.ReadToEnd(), XmlNodeType.Element, null) : new XmlTextReader(reader);
			xmlTextReader.XmlResolver = null;
			return this.ReadXml(xmlTextReader, mode, false);
		}

		// Token: 0x06000C1A RID: 3098 RVA: 0x00067388 File Offset: 0x00066788
		public XmlReadMode ReadXml(string fileName, XmlReadMode mode)
		{
			XmlTextReader xmlTextReader = null;
			if (mode == XmlReadMode.Fragment)
			{
				FileStream xmlFragment = new FileStream(fileName, FileMode.Open);
				xmlTextReader = new XmlTextReader(xmlFragment, XmlNodeType.Element, null);
			}
			else
			{
				xmlTextReader = new XmlTextReader(fileName);
			}
			xmlTextReader.XmlResolver = null;
			XmlReadMode result;
			try
			{
				result = this.ReadXml(xmlTextReader, mode, false);
			}
			finally
			{
				xmlTextReader.Close();
			}
			return result;
		}

		// Token: 0x06000C1B RID: 3099 RVA: 0x000673EC File Offset: 0x000667EC
		public void WriteXml(Stream stream)
		{
			this.WriteXml(stream, XmlWriteMode.IgnoreSchema);
		}

		// Token: 0x06000C1C RID: 3100 RVA: 0x00067404 File Offset: 0x00066804
		public void WriteXml(TextWriter writer)
		{
			this.WriteXml(writer, XmlWriteMode.IgnoreSchema);
		}

		// Token: 0x06000C1D RID: 3101 RVA: 0x0006741C File Offset: 0x0006681C
		public void WriteXml(XmlWriter writer)
		{
			this.WriteXml(writer, XmlWriteMode.IgnoreSchema);
		}

		// Token: 0x06000C1E RID: 3102 RVA: 0x00067434 File Offset: 0x00066834
		public void WriteXml(string fileName)
		{
			this.WriteXml(fileName, XmlWriteMode.IgnoreSchema);
		}

		// Token: 0x06000C1F RID: 3103 RVA: 0x0006744C File Offset: 0x0006684C
		public void WriteXml(Stream stream, XmlWriteMode mode)
		{
			if (stream != null)
			{
				this.WriteXml(new XmlTextWriter(stream, null)
				{
					Formatting = Formatting.Indented
				}, mode);
			}
		}

		// Token: 0x06000C20 RID: 3104 RVA: 0x00067474 File Offset: 0x00066874
		public void WriteXml(TextWriter writer, XmlWriteMode mode)
		{
			if (writer != null)
			{
				this.WriteXml(new XmlTextWriter(writer)
				{
					Formatting = Formatting.Indented
				}, mode);
			}
		}

		// Token: 0x06000C21 RID: 3105 RVA: 0x0006749C File Offset: 0x0006689C
		public void WriteXml(XmlWriter writer, XmlWriteMode mode)
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<ds.DataSet.WriteXml|API> %d#, mode=%d{ds.XmlWriteMode}\n", this.ObjectID, (int)mode);
			try
			{
				if (writer != null)
				{
					if (mode == XmlWriteMode.DiffGram)
					{
						new NewDiffgramGen(this).Save(writer);
					}
					else
					{
						new XmlDataTreeWriter(this).Save(writer, mode == XmlWriteMode.WriteSchema);
					}
				}
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x06000C22 RID: 3106 RVA: 0x00067508 File Offset: 0x00066908
		public void WriteXml(string fileName, XmlWriteMode mode)
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<ds.DataSet.WriteXml|API> %d#, fileName='%ls', mode=%d{ds.XmlWriteMode}\n", this.ObjectID, fileName, (int)mode);
			XmlTextWriter xmlTextWriter = new XmlTextWriter(fileName, null);
			try
			{
				xmlTextWriter.Formatting = Formatting.Indented;
				xmlTextWriter.WriteStartDocument(true);
				if (xmlTextWriter != null)
				{
					if (mode == XmlWriteMode.DiffGram)
					{
						new NewDiffgramGen(this).Save(xmlTextWriter);
					}
					else
					{
						new XmlDataTreeWriter(this).Save(xmlTextWriter, mode == XmlWriteMode.WriteSchema);
					}
				}
				xmlTextWriter.WriteEndDocument();
			}
			finally
			{
				xmlTextWriter.Close();
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x06000C23 RID: 3107 RVA: 0x00067598 File Offset: 0x00066998
		internal DataRelationCollection GetParentRelations(DataTable table)
		{
			return table.ParentRelations;
		}

		// Token: 0x06000C24 RID: 3108 RVA: 0x000675AC File Offset: 0x000669AC
		public void Merge(DataSet dataSet)
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<ds.DataSet.Merge|API> %d#, dataSet=%d\n", this.ObjectID, (dataSet != null) ? dataSet.ObjectID : 0);
			try
			{
				this.Merge(dataSet, false, MissingSchemaAction.Add);
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x06000C25 RID: 3109 RVA: 0x00067608 File Offset: 0x00066A08
		public void Merge(DataSet dataSet, bool preserveChanges)
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<ds.DataSet.Merge|API> %d#, dataSet=%d, preserveChanges=%d{bool}\n", this.ObjectID, (dataSet != null) ? dataSet.ObjectID : 0, preserveChanges);
			try
			{
				this.Merge(dataSet, preserveChanges, MissingSchemaAction.Add);
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x06000C26 RID: 3110 RVA: 0x00067664 File Offset: 0x00066A64
		public void Merge(DataSet dataSet, bool preserveChanges, MissingSchemaAction missingSchemaAction)
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<ds.DataSet.Merge|API> %d#, dataSet=%d, preserveChanges=%d{bool}, missingSchemaAction=%d{ds.MissingSchemaAction}\n", this.ObjectID, (dataSet != null) ? dataSet.ObjectID : 0, preserveChanges, (int)missingSchemaAction);
			try
			{
				if (dataSet == null)
				{
					throw ExceptionBuilder.ArgumentNull("dataSet");
				}
				if (missingSchemaAction - MissingSchemaAction.Add > 3)
				{
					throw ADP.InvalidMissingSchemaAction(missingSchemaAction);
				}
				Merger merger = new Merger(this, preserveChanges, missingSchemaAction);
				merger.MergeDataSet(dataSet);
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x06000C27 RID: 3111 RVA: 0x000676E4 File Offset: 0x00066AE4
		public void Merge(DataTable table)
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<ds.DataSet.Merge|API> %d#, table=%d\n", this.ObjectID, (table != null) ? table.ObjectID : 0);
			try
			{
				this.Merge(table, false, MissingSchemaAction.Add);
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x06000C28 RID: 3112 RVA: 0x00067740 File Offset: 0x00066B40
		public void Merge(DataTable table, bool preserveChanges, MissingSchemaAction missingSchemaAction)
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<ds.DataSet.Merge|API> %d#, table=%d, preserveChanges=%d{bool}, missingSchemaAction=%d{ds.MissingSchemaAction}\n", this.ObjectID, (table != null) ? table.ObjectID : 0, preserveChanges, (int)missingSchemaAction);
			try
			{
				if (table == null)
				{
					throw ExceptionBuilder.ArgumentNull("table");
				}
				if (missingSchemaAction - MissingSchemaAction.Add > 3)
				{
					throw ADP.InvalidMissingSchemaAction(missingSchemaAction);
				}
				Merger merger = new Merger(this, preserveChanges, missingSchemaAction);
				merger.MergeTable(table);
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x06000C29 RID: 3113 RVA: 0x000677C0 File Offset: 0x00066BC0
		public void Merge(DataRow[] rows)
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<ds.DataSet.Merge|API> %d#, rows\n", this.ObjectID);
			try
			{
				this.Merge(rows, false, MissingSchemaAction.Add);
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x06000C2A RID: 3114 RVA: 0x00067810 File Offset: 0x00066C10
		public void Merge(DataRow[] rows, bool preserveChanges, MissingSchemaAction missingSchemaAction)
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<ds.DataSet.Merge|API> %d#, preserveChanges=%d{bool}, missingSchemaAction=%d{ds.MissingSchemaAction}\n", this.ObjectID, preserveChanges, (int)missingSchemaAction);
			try
			{
				if (rows == null)
				{
					throw ExceptionBuilder.ArgumentNull("rows");
				}
				if (missingSchemaAction - MissingSchemaAction.Add > 3)
				{
					throw ADP.InvalidMissingSchemaAction(missingSchemaAction);
				}
				Merger merger = new Merger(this, preserveChanges, missingSchemaAction);
				merger.MergeRows(rows);
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x06000C2B RID: 3115 RVA: 0x00067884 File Offset: 0x00066C84
		protected virtual void OnPropertyChanging(PropertyChangedEventArgs pcevent)
		{
			if (this.onPropertyChangingDelegate != null)
			{
				this.onPropertyChangingDelegate(this, pcevent);
			}
		}

		// Token: 0x06000C2C RID: 3116 RVA: 0x000678A8 File Offset: 0x00066CA8
		internal void OnMergeFailed(MergeFailedEventArgs mfevent)
		{
			if (this.onMergeFailed != null)
			{
				this.onMergeFailed(this, mfevent);
				return;
			}
			throw ExceptionBuilder.MergeFailed(mfevent.Conflict);
		}

		// Token: 0x06000C2D RID: 3117 RVA: 0x000678D8 File Offset: 0x00066CD8
		internal void RaiseMergeFailed(DataTable table, string conflict, MissingSchemaAction missingSchemaAction)
		{
			if (MissingSchemaAction.Error == missingSchemaAction)
			{
				throw ExceptionBuilder.MergeFailed(conflict);
			}
			MergeFailedEventArgs mfevent = new MergeFailedEventArgs(table, conflict);
			this.OnMergeFailed(mfevent);
		}

		// Token: 0x06000C2E RID: 3118 RVA: 0x00067900 File Offset: 0x00066D00
		internal void OnDataRowCreated(DataRow row)
		{
			if (this.onDataRowCreated != null)
			{
				this.onDataRowCreated(this, row);
			}
		}

		// Token: 0x06000C2F RID: 3119 RVA: 0x00067924 File Offset: 0x00066D24
		internal void OnClearFunctionCalled(DataTable table)
		{
			if (this.onClearFunctionCalled != null)
			{
				this.onClearFunctionCalled(this, table);
			}
		}

		// Token: 0x06000C30 RID: 3120 RVA: 0x00067948 File Offset: 0x00066D48
		private void OnInitialized()
		{
			if (this.onInitialized != null)
			{
				this.onInitialized(this, EventArgs.Empty);
			}
		}

		// Token: 0x06000C31 RID: 3121 RVA: 0x00067970 File Offset: 0x00066D70
		protected internal virtual void OnRemoveTable(DataTable table)
		{
		}

		// Token: 0x06000C32 RID: 3122 RVA: 0x00067980 File Offset: 0x00066D80
		internal void OnRemovedTable(DataTable table)
		{
			DataViewManager dataViewManager = this.defaultViewManager;
			if (dataViewManager != null)
			{
				dataViewManager.DataViewSettings.Remove(table);
			}
		}

		// Token: 0x06000C33 RID: 3123 RVA: 0x000679A4 File Offset: 0x00066DA4
		protected virtual void OnRemoveRelation(DataRelation relation)
		{
		}

		// Token: 0x06000C34 RID: 3124 RVA: 0x000679B4 File Offset: 0x00066DB4
		internal void OnRemoveRelationHack(DataRelation relation)
		{
			this.OnRemoveRelation(relation);
		}

		// Token: 0x06000C35 RID: 3125 RVA: 0x000679C8 File Offset: 0x00066DC8
		protected internal void RaisePropertyChanging(string name)
		{
			this.OnPropertyChanging(new PropertyChangedEventArgs(name));
		}

		// Token: 0x06000C36 RID: 3126 RVA: 0x000679E4 File Offset: 0x00066DE4
		internal DataTable[] TopLevelTables()
		{
			return this.TopLevelTables(false);
		}

		// Token: 0x06000C37 RID: 3127 RVA: 0x000679F8 File Offset: 0x00066DF8
		internal DataTable[] TopLevelTables(bool forSchema)
		{
			List<DataTable> list = new List<DataTable>();
			if (forSchema)
			{
				for (int i = 0; i < this.Tables.Count; i++)
				{
					DataTable dataTable = this.Tables[i];
					if (dataTable.NestedParentsCount > 1 || dataTable.SelfNested)
					{
						list.Add(dataTable);
					}
				}
			}
			for (int j = 0; j < this.Tables.Count; j++)
			{
				DataTable dataTable2 = this.Tables[j];
				if (dataTable2.NestedParentsCount == 0 && !list.Contains(dataTable2))
				{
					list.Add(dataTable2);
				}
			}
			if (list.Count == 0)
			{
				return DataSet.zeroTables;
			}
			return list.ToArray();
		}

		// Token: 0x06000C38 RID: 3128 RVA: 0x00067A9C File Offset: 0x00066E9C
		public virtual void RejectChanges()
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<ds.DataSet.RejectChanges|API> %d#\n", this.ObjectID);
			try
			{
				bool flag = this.EnforceConstraints;
				this.EnforceConstraints = false;
				for (int i = 0; i < this.Tables.Count; i++)
				{
					this.Tables[i].RejectChanges();
				}
				this.EnforceConstraints = flag;
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x06000C39 RID: 3129 RVA: 0x00067B20 File Offset: 0x00066F20
		public virtual void Reset()
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<ds.DataSet.Reset|API> %d#\n", this.ObjectID);
			try
			{
				for (int i = 0; i < this.Tables.Count; i++)
				{
					ConstraintCollection constraints = this.Tables[i].Constraints;
					int j = 0;
					while (j < constraints.Count)
					{
						if (constraints[j] is ForeignKeyConstraint)
						{
							constraints.Remove(constraints[j]);
						}
						else
						{
							j++;
						}
					}
				}
				this.Clear();
				this.Relations.Clear();
				this.Tables.Clear();
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x06000C3A RID: 3130 RVA: 0x00067BD8 File Offset: 0x00066FD8
		internal bool ValidateCaseConstraint()
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<ds.DataSet.ValidateCaseConstraint|INFO> %d#\n", this.ObjectID);
			bool result;
			try
			{
				for (int i = 0; i < this.Relations.Count; i++)
				{
					DataRelation dataRelation = this.Relations[i];
					if (dataRelation.ChildTable.CaseSensitive != dataRelation.ParentTable.CaseSensitive)
					{
						return false;
					}
				}
				for (int j = 0; j < this.Tables.Count; j++)
				{
					ConstraintCollection constraints = this.Tables[j].Constraints;
					for (int k = 0; k < constraints.Count; k++)
					{
						if (constraints[k] is ForeignKeyConstraint)
						{
							ForeignKeyConstraint foreignKeyConstraint = (ForeignKeyConstraint)constraints[k];
							if (foreignKeyConstraint.Table.CaseSensitive != foreignKeyConstraint.RelatedTable.CaseSensitive)
							{
								return false;
							}
						}
					}
				}
				result = true;
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
			return result;
		}

		// Token: 0x06000C3B RID: 3131 RVA: 0x00067CE4 File Offset: 0x000670E4
		internal bool ValidateLocaleConstraint()
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<ds.DataSet.ValidateLocaleConstraint|INFO> %d#\n", this.ObjectID);
			bool result;
			try
			{
				for (int i = 0; i < this.Relations.Count; i++)
				{
					DataRelation dataRelation = this.Relations[i];
					if (dataRelation.ChildTable.Locale.LCID != dataRelation.ParentTable.Locale.LCID)
					{
						return false;
					}
				}
				for (int j = 0; j < this.Tables.Count; j++)
				{
					ConstraintCollection constraints = this.Tables[j].Constraints;
					for (int k = 0; k < constraints.Count; k++)
					{
						if (constraints[k] is ForeignKeyConstraint)
						{
							ForeignKeyConstraint foreignKeyConstraint = (ForeignKeyConstraint)constraints[k];
							if (foreignKeyConstraint.Table.Locale.LCID != foreignKeyConstraint.RelatedTable.Locale.LCID)
							{
								return false;
							}
						}
					}
				}
				result = true;
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
			return result;
		}

		// Token: 0x06000C3C RID: 3132 RVA: 0x00067E04 File Offset: 0x00067204
		internal DataTable FindTable(DataTable baseTable, PropertyDescriptor[] props, int propStart)
		{
			if (props.Length < propStart + 1)
			{
				return baseTable;
			}
			PropertyDescriptor propertyDescriptor = props[propStart];
			if (baseTable == null)
			{
				if (propertyDescriptor is DataTablePropertyDescriptor)
				{
					return this.FindTable(((DataTablePropertyDescriptor)propertyDescriptor).Table, props, propStart + 1);
				}
				return null;
			}
			else
			{
				if (propertyDescriptor is DataRelationPropertyDescriptor)
				{
					return this.FindTable(((DataRelationPropertyDescriptor)propertyDescriptor).Relation.ChildTable, props, propStart + 1);
				}
				return null;
			}
		}

		// Token: 0x06000C3D RID: 3133 RVA: 0x00067E68 File Offset: 0x00067268
		protected virtual void ReadXmlSerializable(XmlReader reader)
		{
			this.UseDataSetSchemaOnly = false;
			this.UdtIsWrapped = false;
			if (reader.HasAttributes)
			{
				if (reader.MoveToAttribute("xsi:nil"))
				{
					string attribute = reader.GetAttribute("xsi:nil");
					if (string.Compare(attribute, "true", StringComparison.Ordinal) == 0)
					{
						this.MoveToElement(reader, 1);
						return;
					}
				}
				if (reader.MoveToAttribute("msdata:UseDataSetSchemaOnly"))
				{
					string attribute2 = reader.GetAttribute("msdata:UseDataSetSchemaOnly");
					if (string.Equals(attribute2, "true", StringComparison.Ordinal) || string.Equals(attribute2, "1", StringComparison.Ordinal))
					{
						this.UseDataSetSchemaOnly = true;
					}
					else if (!string.Equals(attribute2, "false", StringComparison.Ordinal) && !string.Equals(attribute2, "0", StringComparison.Ordinal))
					{
						throw ExceptionBuilder.InvalidAttributeValue("UseDataSetSchemaOnly", attribute2);
					}
				}
				if (reader.MoveToAttribute("msdata:UDTColumnValueWrapped"))
				{
					string attribute3 = reader.GetAttribute("msdata:UDTColumnValueWrapped");
					if (string.Equals(attribute3, "true", StringComparison.Ordinal) || string.Equals(attribute3, "1", StringComparison.Ordinal))
					{
						this.UdtIsWrapped = true;
					}
					else if (!string.Equals(attribute3, "false", StringComparison.Ordinal) && !string.Equals(attribute3, "0", StringComparison.Ordinal))
					{
						throw ExceptionBuilder.InvalidAttributeValue("UDTColumnValueWrapped", attribute3);
					}
				}
			}
			this.ReadXml(reader, XmlReadMode.DiffGram, true);
		}

		// Token: 0x06000C3E RID: 3134 RVA: 0x00067F98 File Offset: 0x00067398
		protected virtual XmlSchema GetSchemaSerializable()
		{
			return null;
		}

		// Token: 0x06000C3F RID: 3135 RVA: 0x00067FA8 File Offset: 0x000673A8
		public static XmlSchemaComplexType GetDataSetSchema(XmlSchemaSet schemaSet)
		{
			if (DataSet.schemaTypeForWSDL == null)
			{
				XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
				XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
				if (DataSet.PublishLegacyWSDL())
				{
					XmlSchemaElement xmlSchemaElement = new XmlSchemaElement();
					xmlSchemaElement.RefName = new XmlQualifiedName("schema", "http://www.w3.org/2001/XMLSchema");
					xmlSchemaSequence.Items.Add(xmlSchemaElement);
					XmlSchemaAny item = new XmlSchemaAny();
					xmlSchemaSequence.Items.Add(item);
				}
				else
				{
					XmlSchemaAny xmlSchemaAny = new XmlSchemaAny();
					xmlSchemaAny.Namespace = "http://www.w3.org/2001/XMLSchema";
					xmlSchemaAny.MinOccurs = 0m;
					xmlSchemaAny.ProcessContents = XmlSchemaContentProcessing.Lax;
					xmlSchemaSequence.Items.Add(xmlSchemaAny);
					xmlSchemaAny = new XmlSchemaAny();
					xmlSchemaAny.Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1";
					xmlSchemaAny.MinOccurs = 0m;
					xmlSchemaAny.ProcessContents = XmlSchemaContentProcessing.Lax;
					xmlSchemaSequence.Items.Add(xmlSchemaAny);
					xmlSchemaSequence.MaxOccurs = decimal.MaxValue;
				}
				xmlSchemaComplexType.Particle = xmlSchemaSequence;
				DataSet.schemaTypeForWSDL = xmlSchemaComplexType;
			}
			return DataSet.schemaTypeForWSDL;
		}

		// Token: 0x06000C40 RID: 3136 RVA: 0x00068094 File Offset: 0x00067494
		private static bool PublishLegacyWSDL()
		{
			float num = 1f;
			NameValueCollection nameValueCollection = (NameValueCollection)PrivilegedConfigurationManager.GetSection("system.data.dataset");
			if (nameValueCollection != null)
			{
				string[] values = nameValueCollection.GetValues("WSDL_VERSION");
				if (values != null && values.Length != 0 && values[0] != null)
				{
					num = float.Parse(values[0], CultureInfo.InvariantCulture);
				}
			}
			return num < 2f;
		}

		// Token: 0x06000C41 RID: 3137 RVA: 0x000680E8 File Offset: 0x000674E8
		XmlSchema IXmlSerializable.GetSchema()
		{
			if (base.GetType() == typeof(DataSet))
			{
				return null;
			}
			MemoryStream memoryStream = new MemoryStream();
			XmlWriter xmlWriter = new XmlTextWriter(memoryStream, null);
			if (xmlWriter != null)
			{
				new XmlTreeGen(SchemaFormat.WebService).Save(this, xmlWriter);
			}
			memoryStream.Position = 0L;
			return XmlSchema.Read(new XmlTextReader(memoryStream), null);
		}

		// Token: 0x06000C42 RID: 3138 RVA: 0x00068140 File Offset: 0x00067540
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			bool flag = true;
			XmlTextReader xmlTextReader = null;
			IXmlTextParser xmlTextParser = reader as IXmlTextParser;
			if (xmlTextParser != null)
			{
				flag = xmlTextParser.Normalized;
				xmlTextParser.Normalized = false;
			}
			else
			{
				xmlTextReader = (reader as XmlTextReader);
				if (xmlTextReader != null)
				{
					flag = xmlTextReader.Normalization;
					xmlTextReader.Normalization = false;
				}
			}
			this.ReadXmlSerializable(reader);
			if (xmlTextParser != null)
			{
				xmlTextParser.Normalized = flag;
				return;
			}
			if (xmlTextReader != null)
			{
				xmlTextReader.Normalization = flag;
			}
		}

		// Token: 0x06000C43 RID: 3139 RVA: 0x000681A0 File Offset: 0x000675A0
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			this.WriteXmlSchema(writer, SchemaFormat.WebService, null);
			this.WriteXml(writer, XmlWriteMode.DiffGram);
		}

		// Token: 0x06000C44 RID: 3140 RVA: 0x000681C0 File Offset: 0x000675C0
		public virtual void Load(IDataReader reader, LoadOption loadOption, FillErrorEventHandler errorHandler, params DataTable[] tables)
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<ds.DataSet.Load|API> reader, loadOption=%d{ds.LoadOption}", (int)loadOption);
			try
			{
				foreach (DataTable dataTable in tables)
				{
					ADP.CheckArgumentNull(dataTable, "tables");
					if (dataTable.DataSet != this)
					{
						throw ExceptionBuilder.TableNotInTheDataSet(dataTable.TableName);
					}
				}
				LoadAdapter loadAdapter = new LoadAdapter();
				loadAdapter.FillLoadOption = loadOption;
				loadAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
				if (errorHandler != null)
				{
					loadAdapter.FillError += errorHandler;
				}
				loadAdapter.FillFromReader(tables, reader, 0, 0);
				if (!reader.IsClosed && !reader.NextResult())
				{
					reader.Close();
				}
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x06000C45 RID: 3141 RVA: 0x00068278 File Offset: 0x00067678
		public void Load(IDataReader reader, LoadOption loadOption, params DataTable[] tables)
		{
			this.Load(reader, loadOption, null, tables);
		}

		// Token: 0x06000C46 RID: 3142 RVA: 0x00068290 File Offset: 0x00067690
		public void Load(IDataReader reader, LoadOption loadOption, params string[] tables)
		{
			ADP.CheckArgumentNull(tables, "tables");
			DataTable[] array = new DataTable[tables.Length];
			for (int i = 0; i < tables.Length; i++)
			{
				DataTable dataTable = this.Tables[tables[i]];
				if (dataTable == null)
				{
					dataTable = new DataTable(tables[i]);
					this.Tables.Add(dataTable);
				}
				array[i] = dataTable;
			}
			this.Load(reader, loadOption, null, array);
		}

		// Token: 0x06000C47 RID: 3143 RVA: 0x000682F4 File Offset: 0x000676F4
		public DataTableReader CreateDataReader()
		{
			if (this.Tables.Count == 0)
			{
				throw ExceptionBuilder.CannotCreateDataReaderOnEmptyDataSet();
			}
			DataTable[] array = new DataTable[this.Tables.Count];
			for (int i = 0; i < this.Tables.Count; i++)
			{
				array[i] = this.Tables[i];
			}
			return this.CreateDataReader(array);
		}

		// Token: 0x06000C48 RID: 3144 RVA: 0x00068354 File Offset: 0x00067754
		public DataTableReader CreateDataReader(params DataTable[] dataTables)
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<ds.DataSet.GetDataReader|API> %d#\n", this.ObjectID);
			DataTableReader result;
			try
			{
				if (dataTables.Length == 0)
				{
					throw ExceptionBuilder.DataTableReaderArgumentIsEmpty();
				}
				for (int i = 0; i < dataTables.Length; i++)
				{
					if (dataTables[i] == null)
					{
						throw ExceptionBuilder.ArgumentContainsNullValue();
					}
				}
				result = new DataTableReader(dataTables);
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
			return result;
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000C49 RID: 3145 RVA: 0x000683C4 File Offset: 0x000677C4
		// (set) Token: 0x06000C4A RID: 3146 RVA: 0x000683D8 File Offset: 0x000677D8
		internal string MainTableName
		{
			get
			{
				return this.mainTableName;
			}
			set
			{
				this.mainTableName = value;
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000C4B RID: 3147 RVA: 0x000683EC File Offset: 0x000677EC
		internal int ObjectID
		{
			get
			{
				return this._objectID;
			}
		}

		// Token: 0x0400037B RID: 891
		private DataViewManager defaultViewManager;

		// Token: 0x0400037C RID: 892
		private readonly DataTableCollection tableCollection;

		// Token: 0x0400037D RID: 893
		private readonly DataRelationCollection relationCollection;

		// Token: 0x0400037E RID: 894
		internal PropertyCollection extendedProperties;

		// Token: 0x0400037F RID: 895
		private string dataSetName = "NewDataSet";

		// Token: 0x04000380 RID: 896
		private string _datasetPrefix = string.Empty;

		// Token: 0x04000381 RID: 897
		internal string namespaceURI = string.Empty;

		// Token: 0x04000382 RID: 898
		private bool enforceConstraints = true;

		// Token: 0x04000383 RID: 899
		private const string KEY_XMLSCHEMA = "XmlSchema";

		// Token: 0x04000384 RID: 900
		private const string KEY_XMLDIFFGRAM = "XmlDiffGram";

		// Token: 0x04000385 RID: 901
		private bool _caseSensitive;

		// Token: 0x04000386 RID: 902
		private CultureInfo _culture;

		// Token: 0x04000387 RID: 903
		private bool _cultureUserSet;

		// Token: 0x04000388 RID: 904
		internal bool fInReadXml;

		// Token: 0x04000389 RID: 905
		internal bool fInLoadDiffgram;

		// Token: 0x0400038A RID: 906
		internal bool fTopLevelTable;

		// Token: 0x0400038B RID: 907
		internal bool fInitInProgress;

		// Token: 0x0400038C RID: 908
		internal bool fEnableCascading = true;

		// Token: 0x0400038D RID: 909
		internal bool fIsSchemaLoading;

		// Token: 0x0400038E RID: 910
		private bool fBoundToDocument;

		// Token: 0x0400038F RID: 911
		private PropertyChangedEventHandler onPropertyChangingDelegate;

		// Token: 0x04000390 RID: 912
		private MergeFailedEventHandler onMergeFailed;

		// Token: 0x04000391 RID: 913
		private DataRowCreatedEventHandler onDataRowCreated;

		// Token: 0x04000392 RID: 914
		private DataSetClearEventhandler onClearFunctionCalled;

		// Token: 0x04000393 RID: 915
		private EventHandler onInitialized;

		// Token: 0x04000394 RID: 916
		internal static readonly DataTable[] zeroTables = new DataTable[0];

		// Token: 0x04000395 RID: 917
		internal string mainTableName = "";

		// Token: 0x04000396 RID: 918
		private SerializationFormat _remotingFormat;

		// Token: 0x04000397 RID: 919
		private object _defaultViewManagerLock = new object();

		// Token: 0x04000398 RID: 920
		private static int _objectTypeCount;

		// Token: 0x04000399 RID: 921
		private readonly int _objectID = Interlocked.Increment(ref DataSet._objectTypeCount);

		// Token: 0x0400039A RID: 922
		private static XmlSchemaComplexType schemaTypeForWSDL = null;

		// Token: 0x0400039B RID: 923
		internal bool UseDataSetSchemaOnly;

		// Token: 0x0400039C RID: 924
		internal bool UdtIsWrapped;

		// Token: 0x0200034A RID: 842
		private struct TableChanges
		{
			// Token: 0x060033FA RID: 13306 RVA: 0x0013FD54 File Offset: 0x0013F154
			internal TableChanges(int rowCount)
			{
				this._rowChanges = new BitArray(rowCount);
				this._hasChanges = 0;
			}

			// Token: 0x1700083F RID: 2111
			// (get) Token: 0x060033FB RID: 13307 RVA: 0x0013FD74 File Offset: 0x0013F174
			// (set) Token: 0x060033FC RID: 13308 RVA: 0x0013FD88 File Offset: 0x0013F188
			internal int HasChanges
			{
				get
				{
					return this._hasChanges;
				}
				set
				{
					this._hasChanges = value;
				}
			}

			// Token: 0x17000840 RID: 2112
			internal bool this[int index]
			{
				get
				{
					return this._rowChanges[index];
				}
				set
				{
					this._rowChanges[index] = value;
					this._hasChanges++;
				}
			}

			// Token: 0x04001EB7 RID: 7863
			private BitArray _rowChanges;

			// Token: 0x04001EB8 RID: 7864
			private int _hasChanges;
		}
	}
}
