using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Data.Common;
using System.Globalization;
using System.IO;
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
	// Token: 0x02000094 RID: 148
	[XmlRoot("DataSet")]
	[DefaultProperty("DataSetName")]
	[Designer("Microsoft.VSDesigner.Data.VS.DataSetDesigner, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ToolboxItem("Microsoft.VSDesigner.Data.VS.DataSetToolboxItem, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ResDescription("DataSetDescr")]
	[XmlSchemaProvider("GetDataSetSchema")]
	[Serializable]
	public class DataSet : MarshalByValueComponent, IListSource, IXmlSerializable, ISupportInitializeNotification, ISupportInitialize, ISerializable
	{
		// Token: 0x06000864 RID: 2148 RVA: 0x001F8558 File Offset: 0x001F7958
		public DataSet()
		{
			GC.SuppressFinalize(this);
			Bid.Trace("<ds.DataSet.DataSet|API> %d#\n", this.ObjectID);
			this.tableCollection = new DataTableCollection(this);
			this.relationCollection = new DataRelationCollection.DataSetRelationCollection(this);
			this._culture = CultureInfo.CurrentCulture;
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x001F8608 File Offset: 0x001F7A08
		public DataSet(string dataSetName) : this()
		{
			this.DataSetName = dataSetName;
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000866 RID: 2150 RVA: 0x001F8628 File Offset: 0x001F7A28
		// (set) Token: 0x06000867 RID: 2151 RVA: 0x001F8648 File Offset: 0x001F7A48
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

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000868 RID: 2152 RVA: 0x001F8698 File Offset: 0x001F7A98
		// (set) Token: 0x06000869 RID: 2153 RVA: 0x001F86A8 File Offset: 0x001F7AA8
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

		// Token: 0x0600086A RID: 2154 RVA: 0x001F86C8 File Offset: 0x001F7AC8
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

		// Token: 0x0600086B RID: 2155 RVA: 0x001F8718 File Offset: 0x001F7B18
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

		// Token: 0x0600086C RID: 2156 RVA: 0x001F8768 File Offset: 0x001F7B68
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

		// Token: 0x0600086D RID: 2157 RVA: 0x001F87E8 File Offset: 0x001F7BE8
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

		// Token: 0x0600086E RID: 2158 RVA: 0x001F8838 File Offset: 0x001F7C38
		protected DataSet(SerializationInfo info, StreamingContext context) : this(info, context, true)
		{
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x001F8858 File Offset: 0x001F7C58
		protected DataSet(SerializationInfo info, StreamingContext context, bool ConstructSchema) : this()
		{
			SerializationFormat serializationFormat = SerializationFormat.Xml;
			SchemaSerializationMode schemaSerializationMode = SchemaSerializationMode.IncludeSchema;
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			while (enumerator.MoveNext())
			{
				string name;
				if ((name = enumerator.Name) != null)
				{
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

		// Token: 0x06000870 RID: 2160 RVA: 0x001F88E8 File Offset: 0x001F7CE8
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			SerializationFormat remotingFormat = this.RemotingFormat;
			this.SerializeDataSet(info, context, remotingFormat);
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x001F8908 File Offset: 0x001F7D08
		protected virtual void InitializeDerivedDataSet()
		{
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x001F8918 File Offset: 0x001F7D18
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

		// Token: 0x06000873 RID: 2163 RVA: 0x001F8B18 File Offset: 0x001F7F18
		internal void DeserializeDataSet(SerializationInfo info, StreamingContext context, SerializationFormat remotingFormat, SchemaSerializationMode schemaSerializationMode)
		{
			this.DeserializeDataSetSchema(info, context, remotingFormat, schemaSerializationMode);
			this.DeserializeDataSetData(info, context, remotingFormat);
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x001F8B48 File Offset: 0x001F7F48
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

		// Token: 0x06000875 RID: 2165 RVA: 0x001F8C98 File Offset: 0x001F8098
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

		// Token: 0x06000876 RID: 2166 RVA: 0x001F8D08 File Offset: 0x001F8108
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

		// Token: 0x06000877 RID: 2167 RVA: 0x001F8D98 File Offset: 0x001F8198
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

		// Token: 0x06000878 RID: 2168 RVA: 0x001F8E48 File Offset: 0x001F8248
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

		// Token: 0x06000879 RID: 2169 RVA: 0x001F8FA8 File Offset: 0x001F83A8
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

		// Token: 0x0600087A RID: 2170 RVA: 0x001F9128 File Offset: 0x001F8528
		internal void FailedEnableConstraints()
		{
			this.EnforceConstraints = false;
			throw ExceptionBuilder.EnforceConstraint();
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x0600087B RID: 2171 RVA: 0x001F9148 File Offset: 0x001F8548
		// (set) Token: 0x0600087C RID: 2172 RVA: 0x001F9168 File Offset: 0x001F8568
		[DefaultValue(false)]
		[ResDescription("DataSetCaseSensitiveDescr")]
		[ResCategory("DataCategory_Data")]
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

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x0600087D RID: 2173 RVA: 0x001F9208 File Offset: 0x001F8608
		bool IListSource.ContainsListCollection
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x0600087E RID: 2174 RVA: 0x001F9218 File Offset: 0x001F8618
		[Browsable(false)]
		[ResDescription("DataSetDefaultViewDescr")]
		public DataViewManager DefaultViewManager
		{
			get
			{
				if (this.defaultViewManager == null)
				{
					lock (this._defaultViewManagerLock)
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

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x0600087F RID: 2175 RVA: 0x001F9288 File Offset: 0x001F8688
		// (set) Token: 0x06000880 RID: 2176 RVA: 0x001F92A8 File Offset: 0x001F86A8
		[DefaultValue(true)]
		[ResDescription("DataSetEnforceConstraintsDescr")]
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

		// Token: 0x06000881 RID: 2177 RVA: 0x001F9308 File Offset: 0x001F8708
		internal void RestoreEnforceConstraints(bool value)
		{
			this.enforceConstraints = value;
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x001F9328 File Offset: 0x001F8728
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

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000883 RID: 2179 RVA: 0x001F9468 File Offset: 0x001F8868
		// (set) Token: 0x06000884 RID: 2180 RVA: 0x001F9488 File Offset: 0x001F8888
		[ResDescription("DataSetDataSetNameDescr")]
		[ResCategory("DataCategory_Data")]
		[DefaultValue("")]
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

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000885 RID: 2181 RVA: 0x001F9508 File Offset: 0x001F8908
		// (set) Token: 0x06000886 RID: 2182 RVA: 0x001F9528 File Offset: 0x001F8928
		[ResCategory("DataCategory_Data")]
		[DefaultValue("")]
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

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000887 RID: 2183 RVA: 0x001F9638 File Offset: 0x001F8A38
		// (set) Token: 0x06000888 RID: 2184 RVA: 0x001F9658 File Offset: 0x001F8A58
		[DefaultValue("")]
		[ResCategory("DataCategory_Data")]
		[ResDescription("DataSetPrefixDescr")]
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

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000889 RID: 2185 RVA: 0x001F96B8 File Offset: 0x001F8AB8
		[ResCategory("DataCategory_Data")]
		[ResDescription("ExtendedPropertiesDescr")]
		[Browsable(false)]
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

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x0600088A RID: 2186 RVA: 0x001F96E8 File Offset: 0x001F8AE8
		[Browsable(false)]
		[ResDescription("DataSetHasErrorsDescr")]
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

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x0600088B RID: 2187 RVA: 0x001F9728 File Offset: 0x001F8B28
		[Browsable(false)]
		public bool IsInitialized
		{
			get
			{
				return !this.fInitInProgress;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x0600088C RID: 2188 RVA: 0x001F9748 File Offset: 0x001F8B48
		// (set) Token: 0x0600088D RID: 2189 RVA: 0x001F9768 File Offset: 0x001F8B68
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

		// Token: 0x0600088E RID: 2190 RVA: 0x001F97D8 File Offset: 0x001F8BD8
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
						dataTable.SetLocaleValue(value, false, false);
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

		// Token: 0x0600088F RID: 2191 RVA: 0x001F9A28 File Offset: 0x001F8E28
		internal bool ShouldSerializeLocale()
		{
			return this._cultureUserSet;
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000890 RID: 2192 RVA: 0x001F9A48 File Offset: 0x001F8E48
		// (set) Token: 0x06000891 RID: 2193 RVA: 0x001F9A68 File Offset: 0x001F8E68
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000892 RID: 2194 RVA: 0x001F9AD8 File Offset: 0x001F8ED8
		[ResCategory("DataCategory_Data")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[ResDescription("DataSetRelationsDescr")]
		public DataRelationCollection Relations
		{
			get
			{
				return this.relationCollection;
			}
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x001F9AF8 File Offset: 0x001F8EF8
		protected virtual bool ShouldSerializeRelations()
		{
			return true;
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x001F9B08 File Offset: 0x001F8F08
		private void ResetRelations()
		{
			this.Relations.Clear();
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000895 RID: 2197 RVA: 0x001F9B28 File Offset: 0x001F8F28
		[ResDescription("DataSetTablesDescr")]
		[ResCategory("DataCategory_Data")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public DataTableCollection Tables
		{
			get
			{
				return this.tableCollection;
			}
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x001F9B48 File Offset: 0x001F8F48
		protected virtual bool ShouldSerializeTables()
		{
			return true;
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x001F9B58 File Offset: 0x001F8F58
		private void ResetTables()
		{
			this.Tables.Clear();
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000898 RID: 2200 RVA: 0x001F9B78 File Offset: 0x001F8F78
		// (set) Token: 0x06000899 RID: 2201 RVA: 0x001F9B98 File Offset: 0x001F8F98
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

		// Token: 0x0600089A RID: 2202 RVA: 0x001F9BB8 File Offset: 0x001F8FB8
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

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x0600089B RID: 2203 RVA: 0x001F9C28 File Offset: 0x001F9028
		// (remove) Token: 0x0600089C RID: 2204 RVA: 0x001F9C58 File Offset: 0x001F9058
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

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x0600089D RID: 2205 RVA: 0x001F9C88 File Offset: 0x001F9088
		// (remove) Token: 0x0600089E RID: 2206 RVA: 0x001F9CB8 File Offset: 0x001F90B8
		[ResDescription("DataSetMergeFailedDescr")]
		[ResCategory("DataCategory_Action")]
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

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x0600089F RID: 2207 RVA: 0x001F9CE8 File Offset: 0x001F90E8
		// (remove) Token: 0x060008A0 RID: 2208 RVA: 0x001F9D18 File Offset: 0x001F9118
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

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x060008A1 RID: 2209 RVA: 0x001F9D48 File Offset: 0x001F9148
		// (remove) Token: 0x060008A2 RID: 2210 RVA: 0x001F9D78 File Offset: 0x001F9178
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

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x060008A3 RID: 2211 RVA: 0x001F9DA8 File Offset: 0x001F91A8
		// (remove) Token: 0x060008A4 RID: 2212 RVA: 0x001F9DD8 File Offset: 0x001F91D8
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

		// Token: 0x060008A5 RID: 2213 RVA: 0x001F9E08 File Offset: 0x001F9208
		public void BeginInit()
		{
			this.fInitInProgress = true;
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x001F9E28 File Offset: 0x001F9228
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

		// Token: 0x060008A7 RID: 2215 RVA: 0x001F9EB8 File Offset: 0x001F92B8
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

		// Token: 0x060008A8 RID: 2216 RVA: 0x001F9F48 File Offset: 0x001F9348
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

		// Token: 0x060008A9 RID: 2217 RVA: 0x001FA2E8 File Offset: 0x001F96E8
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

		// Token: 0x060008AA RID: 2218 RVA: 0x001FA418 File Offset: 0x001F9818
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

		// Token: 0x060008AB RID: 2219 RVA: 0x001FA4C8 File Offset: 0x001F98C8
		public DataSet GetChanges()
		{
			return this.GetChanges(DataRowState.Added | DataRowState.Deleted | DataRowState.Modified);
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x001FA4E8 File Offset: 0x001F98E8
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
								array2[num2].HasChanges = array2[num2].HasChanges - 1;
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

		// Token: 0x060008AD RID: 2221 RVA: 0x001FA668 File Offset: 0x001F9A68
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

		// Token: 0x060008AE RID: 2222 RVA: 0x001FA6F8 File Offset: 0x001F9AF8
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

		// Token: 0x060008AF RID: 2223 RVA: 0x001FA7B8 File Offset: 0x001F9BB8
		IList IListSource.GetList()
		{
			return this.DefaultViewManager;
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x001FA7D8 File Offset: 0x001F9BD8
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

		// Token: 0x060008B1 RID: 2225 RVA: 0x001FA818 File Offset: 0x001F9C18
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

		// Token: 0x060008B2 RID: 2226 RVA: 0x001FA898 File Offset: 0x001F9C98
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

		// Token: 0x060008B3 RID: 2227 RVA: 0x001FA918 File Offset: 0x001F9D18
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

		// Token: 0x060008B4 RID: 2228 RVA: 0x001FA988 File Offset: 0x001F9D88
		public bool HasChanges()
		{
			return this.HasChanges(DataRowState.Added | DataRowState.Deleted | DataRowState.Modified);
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x001FA9A8 File Offset: 0x001F9DA8
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

		// Token: 0x060008B6 RID: 2230 RVA: 0x001FAA58 File Offset: 0x001F9E58
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

		// Token: 0x060008B7 RID: 2231 RVA: 0x001FAAE8 File Offset: 0x001F9EE8
		public void InferXmlSchema(Stream stream, string[] nsArray)
		{
			if (stream == null)
			{
				return;
			}
			this.InferXmlSchema(new XmlTextReader(stream), nsArray);
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x001FAB08 File Offset: 0x001F9F08
		public void InferXmlSchema(TextReader reader, string[] nsArray)
		{
			if (reader == null)
			{
				return;
			}
			this.InferXmlSchema(new XmlTextReader(reader), nsArray);
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x001FAB28 File Offset: 0x001F9F28
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

		// Token: 0x060008BA RID: 2234 RVA: 0x001FAB78 File Offset: 0x001F9F78
		public void ReadXmlSchema(XmlReader reader)
		{
			this.ReadXmlSchema(reader, false);
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x001FAB98 File Offset: 0x001F9F98
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

		// Token: 0x060008BC RID: 2236 RVA: 0x001FAE28 File Offset: 0x001FA228
		internal bool MoveToElement(XmlReader reader, int depth)
		{
			while (!reader.EOF && reader.NodeType != XmlNodeType.EndElement && reader.NodeType != XmlNodeType.Element && reader.Depth > depth)
			{
				reader.Read();
			}
			return reader.NodeType == XmlNodeType.Element;
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x001FAE78 File Offset: 0x001FA278
		private static void MoveToElement(XmlReader reader)
		{
			while (!reader.EOF && reader.NodeType != XmlNodeType.EndElement && reader.NodeType != XmlNodeType.Element)
			{
				reader.Read();
			}
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x001FAEB8 File Offset: 0x001FA2B8
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

		// Token: 0x060008BF RID: 2239 RVA: 0x001FAEF8 File Offset: 0x001FA2F8
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

		// Token: 0x060008C0 RID: 2240 RVA: 0x001FAFD8 File Offset: 0x001FA3D8
		internal void ReadXDRSchema(XmlReader reader)
		{
			XmlDocument xmlDocument = new XmlDocument();
			XmlNode xmlNode = xmlDocument.ReadNode(reader);
			xmlDocument.AppendChild(xmlNode);
			XDRSchema xdrschema = new XDRSchema(this, false);
			this.DataSetName = xmlDocument.DocumentElement.LocalName;
			xdrschema.LoadSchema((XmlElement)xmlNode, this);
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x001FB028 File Offset: 0x001FA428
		public void ReadXmlSchema(Stream stream)
		{
			if (stream == null)
			{
				return;
			}
			this.ReadXmlSchema(new XmlTextReader(stream), false);
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x001FB048 File Offset: 0x001FA448
		public void ReadXmlSchema(TextReader reader)
		{
			if (reader == null)
			{
				return;
			}
			this.ReadXmlSchema(new XmlTextReader(reader), false);
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x001FB068 File Offset: 0x001FA468
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

		// Token: 0x060008C4 RID: 2244 RVA: 0x001FB0B8 File Offset: 0x001FA4B8
		public void WriteXmlSchema(Stream stream)
		{
			if (stream == null)
			{
				return;
			}
			this.WriteXmlSchema(new XmlTextWriter(stream, null)
			{
				Formatting = Formatting.Indented
			});
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x001FB0E8 File Offset: 0x001FA4E8
		public void WriteXmlSchema(TextWriter writer)
		{
			if (writer == null)
			{
				return;
			}
			this.WriteXmlSchema(new XmlTextWriter(writer)
			{
				Formatting = Formatting.Indented
			});
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x001FB118 File Offset: 0x001FA518
		public void WriteXmlSchema(XmlWriter writer)
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<ds.DataSet.WriteXmlSchema|API> %d#\n", this.ObjectID);
			try
			{
				this.WriteXmlSchema(writer, SchemaFormat.Public);
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x001FB168 File Offset: 0x001FA568
		private void WriteXmlSchema(XmlWriter writer, SchemaFormat schemaFormat)
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
					xmlTreeGen.Save(this, writer);
				}
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x001FB1E8 File Offset: 0x001FA5E8
		public void WriteXmlSchema(string fileName)
		{
			XmlTextWriter xmlTextWriter = new XmlTextWriter(fileName, null);
			try
			{
				xmlTextWriter.Formatting = Formatting.Indented;
				xmlTextWriter.WriteStartDocument(true);
				this.WriteXmlSchema(xmlTextWriter);
				xmlTextWriter.WriteEndDocument();
			}
			finally
			{
				xmlTextWriter.Close();
			}
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x001FB248 File Offset: 0x001FA648
		public XmlReadMode ReadXml(XmlReader reader)
		{
			return this.ReadXml(reader, false);
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x001FB268 File Offset: 0x001FA668
		internal XmlReadMode ReadXml(XmlReader reader, bool denyResolving)
		{
			IDisposable disposable = null;
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<ds.DataSet.ReadXml|INFO> %d#, denyResolving=%d{bool}\n", this.ObjectID, denyResolving);
			XmlReadMode result;
			try
			{
				disposable = TypeLimiter.EnterRestrictedScope(this);
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
					for (int i = 0; i < this.Tables.Count; i++)
					{
						this.Tables[i].rowDiffId = null;
					}
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
								for (int j = 0; j < attributeCount; j++)
								{
									reader.MoveToAttribute(j);
									if (reader.NamespaceURI.Equals("http://www.w3.org/2000/xmlns/"))
									{
										xmlElement.SetAttribute(reader.Name, reader.GetAttribute(j));
									}
									else
									{
										XmlAttribute xmlAttribute = xmlElement.SetAttributeNode(reader.LocalName, reader.NamespaceURI);
										xmlAttribute.Prefix = reader.Prefix;
										xmlAttribute.Value = reader.GetAttribute(j);
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
					for (int k = 0; k < this.Tables.Count; k++)
					{
						this.Tables[k].rowDiffId = null;
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

		// Token: 0x060008CB RID: 2251 RVA: 0x001FB868 File Offset: 0x001FAC68
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

		// Token: 0x060008CC RID: 2252 RVA: 0x001FB898 File Offset: 0x001FAC98
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

		// Token: 0x060008CD RID: 2253 RVA: 0x001FB8C8 File Offset: 0x001FACC8
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

		// Token: 0x060008CE RID: 2254 RVA: 0x001FB918 File Offset: 0x001FAD18
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

		// Token: 0x060008CF RID: 2255 RVA: 0x001FB9E8 File Offset: 0x001FADE8
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

		// Token: 0x060008D0 RID: 2256 RVA: 0x001FBA68 File Offset: 0x001FAE68
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

		// Token: 0x060008D1 RID: 2257 RVA: 0x001FBE88 File Offset: 0x001FB288
		public XmlReadMode ReadXml(XmlReader reader, XmlReadMode mode)
		{
			return this.ReadXml(reader, mode, false);
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x001FBEA8 File Offset: 0x001FB2A8
		internal XmlReadMode ReadXml(XmlReader reader, XmlReadMode mode, bool denyResolving)
		{
			IDisposable disposable = null;
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<ds.DataSet.ReadXml|INFO> %d#, mode=%d{ds.XmlReadMode}, denyResolving=%d{bool}\n", this.ObjectID, (int)mode, denyResolving);
			XmlReadMode result;
			try
			{
				disposable = TypeLimiter.EnterRestrictedScope(this);
				bool flag = false;
				bool flag2 = false;
				bool isXdr = false;
				int depth = -1;
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

		// Token: 0x060008D3 RID: 2259 RVA: 0x001FC328 File Offset: 0x001FB728
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

		// Token: 0x060008D4 RID: 2260 RVA: 0x001FC368 File Offset: 0x001FB768
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

		// Token: 0x060008D5 RID: 2261 RVA: 0x001FC3A8 File Offset: 0x001FB7A8
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

		// Token: 0x060008D6 RID: 2262 RVA: 0x001FC418 File Offset: 0x001FB818
		public void WriteXml(Stream stream)
		{
			this.WriteXml(stream, XmlWriteMode.IgnoreSchema);
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x001FC438 File Offset: 0x001FB838
		public void WriteXml(TextWriter writer)
		{
			this.WriteXml(writer, XmlWriteMode.IgnoreSchema);
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x001FC458 File Offset: 0x001FB858
		public void WriteXml(XmlWriter writer)
		{
			this.WriteXml(writer, XmlWriteMode.IgnoreSchema);
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x001FC478 File Offset: 0x001FB878
		public void WriteXml(string fileName)
		{
			this.WriteXml(fileName, XmlWriteMode.IgnoreSchema);
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x001FC498 File Offset: 0x001FB898
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

		// Token: 0x060008DB RID: 2267 RVA: 0x001FC4C8 File Offset: 0x001FB8C8
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

		// Token: 0x060008DC RID: 2268 RVA: 0x001FC4F8 File Offset: 0x001FB8F8
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

		// Token: 0x060008DD RID: 2269 RVA: 0x001FC568 File Offset: 0x001FB968
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

		// Token: 0x060008DE RID: 2270 RVA: 0x001FC5F8 File Offset: 0x001FB9F8
		internal DataRelationCollection GetParentRelations(DataTable table)
		{
			return table.ParentRelations;
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x001FC618 File Offset: 0x001FBA18
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

		// Token: 0x060008E0 RID: 2272 RVA: 0x001FC678 File Offset: 0x001FBA78
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

		// Token: 0x060008E1 RID: 2273 RVA: 0x001FC6D8 File Offset: 0x001FBAD8
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
				switch (missingSchemaAction)
				{
				case MissingSchemaAction.Add:
				case MissingSchemaAction.Ignore:
				case MissingSchemaAction.Error:
				case MissingSchemaAction.AddWithKey:
				{
					Merger merger = new Merger(this, preserveChanges, missingSchemaAction);
					merger.MergeDataSet(dataSet);
					break;
				}
				default:
					throw ADP.InvalidMissingSchemaAction(missingSchemaAction);
				}
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x001FC778 File Offset: 0x001FBB78
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

		// Token: 0x060008E3 RID: 2275 RVA: 0x001FC7D8 File Offset: 0x001FBBD8
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
				switch (missingSchemaAction)
				{
				case MissingSchemaAction.Add:
				case MissingSchemaAction.Ignore:
				case MissingSchemaAction.Error:
				case MissingSchemaAction.AddWithKey:
				{
					Merger merger = new Merger(this, preserveChanges, missingSchemaAction);
					merger.MergeTable(table);
					break;
				}
				default:
					throw ADP.InvalidMissingSchemaAction(missingSchemaAction);
				}
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x001FC878 File Offset: 0x001FBC78
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

		// Token: 0x060008E5 RID: 2277 RVA: 0x001FC8C8 File Offset: 0x001FBCC8
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
				switch (missingSchemaAction)
				{
				case MissingSchemaAction.Add:
				case MissingSchemaAction.Ignore:
				case MissingSchemaAction.Error:
				case MissingSchemaAction.AddWithKey:
				{
					Merger merger = new Merger(this, preserveChanges, missingSchemaAction);
					merger.MergeRows(rows);
					break;
				}
				default:
					throw ADP.InvalidMissingSchemaAction(missingSchemaAction);
				}
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x001FC958 File Offset: 0x001FBD58
		protected virtual void OnPropertyChanging(PropertyChangedEventArgs pcevent)
		{
			if (this.onPropertyChangingDelegate != null)
			{
				this.onPropertyChangingDelegate(this, pcevent);
			}
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x001FC988 File Offset: 0x001FBD88
		internal void OnMergeFailed(MergeFailedEventArgs mfevent)
		{
			if (this.onMergeFailed != null)
			{
				this.onMergeFailed(this, mfevent);
				return;
			}
			throw ExceptionBuilder.MergeFailed(mfevent.Conflict);
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x001FC9B8 File Offset: 0x001FBDB8
		internal void RaiseMergeFailed(DataTable table, string conflict, MissingSchemaAction missingSchemaAction)
		{
			if (MissingSchemaAction.Error == missingSchemaAction)
			{
				throw ExceptionBuilder.MergeFailed(conflict);
			}
			MergeFailedEventArgs mfevent = new MergeFailedEventArgs(table, conflict);
			this.OnMergeFailed(mfevent);
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x001FC9E8 File Offset: 0x001FBDE8
		internal void OnDataRowCreated(DataRow row)
		{
			if (this.onDataRowCreated != null)
			{
				this.onDataRowCreated(this, row);
			}
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x001FCA18 File Offset: 0x001FBE18
		internal void OnClearFunctionCalled(DataTable table)
		{
			if (this.onClearFunctionCalled != null)
			{
				this.onClearFunctionCalled(this, table);
			}
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x001FCA48 File Offset: 0x001FBE48
		private void OnInitialized()
		{
			if (this.onInitialized != null)
			{
				this.onInitialized(this, EventArgs.Empty);
			}
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x001FCA78 File Offset: 0x001FBE78
		protected internal virtual void OnRemoveTable(DataTable table)
		{
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x001FCA88 File Offset: 0x001FBE88
		internal void OnRemovedTable(DataTable table)
		{
			DataViewManager dataViewManager = this.defaultViewManager;
			if (dataViewManager != null)
			{
				dataViewManager.DataViewSettings.Remove(table);
			}
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x001FCAB8 File Offset: 0x001FBEB8
		protected virtual void OnRemoveRelation(DataRelation relation)
		{
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x001FCAC8 File Offset: 0x001FBEC8
		internal void OnRemoveRelationHack(DataRelation relation)
		{
			this.OnRemoveRelation(relation);
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x001FCAE8 File Offset: 0x001FBEE8
		protected internal void RaisePropertyChanging(string name)
		{
			this.OnPropertyChanging(new PropertyChangedEventArgs(name));
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x001FCB08 File Offset: 0x001FBF08
		internal DataTable[] TopLevelTables()
		{
			return this.TopLevelTables(false);
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x001FCB28 File Offset: 0x001FBF28
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

		// Token: 0x060008F3 RID: 2291 RVA: 0x001FCBD8 File Offset: 0x001FBFD8
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

		// Token: 0x060008F4 RID: 2292 RVA: 0x001FCC68 File Offset: 0x001FC068
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

		// Token: 0x060008F5 RID: 2293 RVA: 0x001FCD28 File Offset: 0x001FC128
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

		// Token: 0x060008F6 RID: 2294 RVA: 0x001FCE38 File Offset: 0x001FC238
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

		// Token: 0x060008F7 RID: 2295 RVA: 0x001FCF58 File Offset: 0x001FC358
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

		// Token: 0x060008F8 RID: 2296 RVA: 0x001FCFC8 File Offset: 0x001FC3C8
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

		// Token: 0x060008F9 RID: 2297 RVA: 0x001FD0F8 File Offset: 0x001FC4F8
		protected virtual XmlSchema GetSchemaSerializable()
		{
			return null;
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x001FD108 File Offset: 0x001FC508
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

		// Token: 0x060008FB RID: 2299 RVA: 0x001FD1F8 File Offset: 0x001FC5F8
		private static bool PublishLegacyWSDL()
		{
			float num = 1f;
			NameValueCollection nameValueCollection = (NameValueCollection)PrivilegedConfigurationManager.GetSection("system.data.dataset");
			if (nameValueCollection != null)
			{
				string[] values = nameValueCollection.GetValues("WSDL_VERSION");
				if (values != null && 0 < values.Length && values[0] != null)
				{
					num = float.Parse(values[0], CultureInfo.InvariantCulture);
				}
			}
			return num < 2f;
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x001FD258 File Offset: 0x001FC658
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

		// Token: 0x060008FD RID: 2301 RVA: 0x001FD2B8 File Offset: 0x001FC6B8
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

		// Token: 0x060008FE RID: 2302 RVA: 0x001FD318 File Offset: 0x001FC718
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			this.WriteXmlSchema(writer, SchemaFormat.WebService);
			this.WriteXml(writer, XmlWriteMode.DiffGram);
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x001FD338 File Offset: 0x001FC738
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

		// Token: 0x06000900 RID: 2304 RVA: 0x001FD3F8 File Offset: 0x001FC7F8
		public void Load(IDataReader reader, LoadOption loadOption, params DataTable[] tables)
		{
			this.Load(reader, loadOption, null, tables);
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x001FD418 File Offset: 0x001FC818
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

		// Token: 0x06000902 RID: 2306 RVA: 0x001FD488 File Offset: 0x001FC888
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

		// Token: 0x06000903 RID: 2307 RVA: 0x001FD4E8 File Offset: 0x001FC8E8
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

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000904 RID: 2308 RVA: 0x001FD568 File Offset: 0x001FC968
		// (set) Token: 0x06000905 RID: 2309 RVA: 0x001FD588 File Offset: 0x001FC988
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

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000906 RID: 2310 RVA: 0x001FD5A8 File Offset: 0x001FC9A8
		internal int ObjectID
		{
			get
			{
				return this._objectID;
			}
		}

		// Token: 0x0400079B RID: 1947
		private const string KEY_XMLSCHEMA = "XmlSchema";

		// Token: 0x0400079C RID: 1948
		private const string KEY_XMLDIFFGRAM = "XmlDiffGram";

		// Token: 0x0400079D RID: 1949
		private DataViewManager defaultViewManager;

		// Token: 0x0400079E RID: 1950
		private readonly DataTableCollection tableCollection;

		// Token: 0x0400079F RID: 1951
		private readonly DataRelationCollection relationCollection;

		// Token: 0x040007A0 RID: 1952
		internal PropertyCollection extendedProperties;

		// Token: 0x040007A1 RID: 1953
		private string dataSetName = "NewDataSet";

		// Token: 0x040007A2 RID: 1954
		private string _datasetPrefix = string.Empty;

		// Token: 0x040007A3 RID: 1955
		internal string namespaceURI = string.Empty;

		// Token: 0x040007A4 RID: 1956
		private bool enforceConstraints = true;

		// Token: 0x040007A5 RID: 1957
		private bool _caseSensitive;

		// Token: 0x040007A6 RID: 1958
		private CultureInfo _culture;

		// Token: 0x040007A7 RID: 1959
		private bool _cultureUserSet;

		// Token: 0x040007A8 RID: 1960
		internal bool fInReadXml;

		// Token: 0x040007A9 RID: 1961
		internal bool fInLoadDiffgram;

		// Token: 0x040007AA RID: 1962
		internal bool fTopLevelTable;

		// Token: 0x040007AB RID: 1963
		internal bool fInitInProgress;

		// Token: 0x040007AC RID: 1964
		internal bool fEnableCascading = true;

		// Token: 0x040007AD RID: 1965
		internal bool fIsSchemaLoading;

		// Token: 0x040007AE RID: 1966
		private bool fBoundToDocument;

		// Token: 0x040007AF RID: 1967
		private PropertyChangedEventHandler onPropertyChangingDelegate;

		// Token: 0x040007B0 RID: 1968
		private MergeFailedEventHandler onMergeFailed;

		// Token: 0x040007B1 RID: 1969
		private DataRowCreatedEventHandler onDataRowCreated;

		// Token: 0x040007B2 RID: 1970
		private DataSetClearEventhandler onClearFunctionCalled;

		// Token: 0x040007B3 RID: 1971
		private EventHandler onInitialized;

		// Token: 0x040007B4 RID: 1972
		internal static readonly DataTable[] zeroTables = new DataTable[0];

		// Token: 0x040007B5 RID: 1973
		internal string mainTableName = "";

		// Token: 0x040007B6 RID: 1974
		private SerializationFormat _remotingFormat;

		// Token: 0x040007B7 RID: 1975
		private object _defaultViewManagerLock = new object();

		// Token: 0x040007B8 RID: 1976
		private static int _objectTypeCount;

		// Token: 0x040007B9 RID: 1977
		private readonly int _objectID = Interlocked.Increment(ref DataSet._objectTypeCount);

		// Token: 0x040007BA RID: 1978
		private static XmlSchemaComplexType schemaTypeForWSDL = null;

		// Token: 0x040007BB RID: 1979
		internal bool UseDataSetSchemaOnly;

		// Token: 0x040007BC RID: 1980
		internal bool UdtIsWrapped;

		// Token: 0x02000095 RID: 149
		private struct TableChanges
		{
			// Token: 0x06000908 RID: 2312 RVA: 0x001FD5E8 File Offset: 0x001FC9E8
			internal TableChanges(int rowCount)
			{
				this._rowChanges = new BitArray(rowCount);
				this._hasChanges = 0;
			}

			// Token: 0x1700011D RID: 285
			// (get) Token: 0x06000909 RID: 2313 RVA: 0x001FD608 File Offset: 0x001FCA08
			// (set) Token: 0x0600090A RID: 2314 RVA: 0x001FD628 File Offset: 0x001FCA28
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

			// Token: 0x1700011E RID: 286
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

			// Token: 0x040007BD RID: 1981
			private BitArray _rowChanges;

			// Token: 0x040007BE RID: 1982
			private int _hasChanges;
		}
	}
}
