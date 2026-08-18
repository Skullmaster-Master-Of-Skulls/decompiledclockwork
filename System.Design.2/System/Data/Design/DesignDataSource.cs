using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Xml;

namespace System.Data.Design
{
	// Token: 0x0200023A RID: 570
	[DataSourceXmlClass("DataSource")]
	internal class DesignDataSource : DataSourceComponent, IDataSourceNamedObject, INamedObject, IDataSourceCommandTarget
	{
		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x06001572 RID: 5490 RVA: 0x00078AD2 File Offset: 0x00076CD2
		internal DataSet DataSet
		{
			get
			{
				if (this.dataSet == null)
				{
					this.dataSet = new DataSet();
					this.dataSet.Locale = CultureInfo.InvariantCulture;
					this.dataSet.EnforceConstraints = false;
				}
				return this.dataSet;
			}
		}

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x06001573 RID: 5491 RVA: 0x00078B0C File Offset: 0x00076D0C
		[DisplayName("DefaultConnection")]
		public DesignConnection DefaultConnection
		{
			get
			{
				if (this.DesignConnections.Count > 0 && this.defaultConnectionIndex >= 0 && this.defaultConnectionIndex < this.DesignConnections.Count)
				{
					return ((IList)this.DesignConnections)[this.defaultConnectionIndex] as DesignConnection;
				}
				return null;
			}
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x06001574 RID: 5492 RVA: 0x00078B5B File Offset: 0x00076D5B
		[DisplayName("Connections")]
		[DataSourceXmlSubItem(Name = "Connections", ItemType = typeof(DesignConnection))]
		[Browsable(false)]
		public DesignConnectionCollection DesignConnections
		{
			get
			{
				if (this.designConnections == null)
				{
					this.designConnections = new DesignConnectionCollection(this);
				}
				return this.designConnections;
			}
		}

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x06001575 RID: 5493 RVA: 0x00078B77 File Offset: 0x00076D77
		[Browsable(false)]
		public DesignRelationCollection DesignRelations
		{
			get
			{
				if (this.designRelations == null)
				{
					this.designRelations = new DesignRelationCollection(this);
				}
				return this.designRelations;
			}
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x06001576 RID: 5494 RVA: 0x00078B93 File Offset: 0x00076D93
		[DataSourceXmlSubItem(Name = "Tables", ItemType = typeof(DesignConnection))]
		[Browsable(false)]
		public DesignTableCollection DesignTables
		{
			get
			{
				if (this.designTables == null)
				{
					this.designTables = new DesignTableCollection(this);
				}
				return this.designTables;
			}
		}

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x06001577 RID: 5495 RVA: 0x00078BB0 File Offset: 0x00076DB0
		// (set) Token: 0x06001578 RID: 5496 RVA: 0x00078BE2 File Offset: 0x00076DE2
		[DefaultValue(true)]
		public bool EnableTableAdapterManager
		{
			get
			{
				bool result = false;
				bool.TryParse(this.DataSet.ExtendedProperties["EnableTableAdapterManager"] as string, out result);
				return result;
			}
			set
			{
				this.DataSet.ExtendedProperties["EnableTableAdapterManager"] = value.ToString();
			}
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x06001579 RID: 5497 RVA: 0x00078C00 File Offset: 0x00076E00
		// (set) Token: 0x0600157A RID: 5498 RVA: 0x00078C08 File Offset: 0x00076E08
		[DefaultValue(TypeAttributes.Public)]
		[DataSourceXmlAttribute]
		public TypeAttributes Modifier
		{
			get
			{
				return this.modifier;
			}
			set
			{
				this.modifier = value;
			}
		}

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x0600157B RID: 5499 RVA: 0x00078C11 File Offset: 0x00076E11
		// (set) Token: 0x0600157C RID: 5500 RVA: 0x00078C1E File Offset: 0x00076E1E
		[MergableProperty(false)]
		[DefaultValue("")]
		public string Name
		{
			get
			{
				return this.DataSet.DataSetName;
			}
			set
			{
				this.DataSet.DataSetName = value;
			}
		}

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x0600157D RID: 5501 RVA: 0x00078C2C File Offset: 0x00076E2C
		[Browsable(false)]
		public string PublicTypeName
		{
			get
			{
				return "DataSet";
			}
		}

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x0600157E RID: 5502 RVA: 0x00078C33 File Offset: 0x00076E33
		[DataSourceXmlSubItem(typeof(Source))]
		[Browsable(false)]
		public SourceCollection Sources
		{
			get
			{
				if (this.sources == null)
				{
					this.sources = new SourceCollection(this);
				}
				return this.sources;
			}
		}

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x0600157F RID: 5503 RVA: 0x00078C4F File Offset: 0x00076E4F
		// (set) Token: 0x06001580 RID: 5504 RVA: 0x00078C57 File Offset: 0x00076E57
		[DataSourceXmlAttribute]
		public SchemaSerializationMode SchemaSerializationMode
		{
			get
			{
				return this.schemaSerializationMode;
			}
			set
			{
				this.schemaSerializationMode = value;
			}
		}

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x06001581 RID: 5505 RVA: 0x00078C60 File Offset: 0x00076E60
		// (set) Token: 0x06001582 RID: 5506 RVA: 0x00078C7C File Offset: 0x00076E7C
		internal string UserDataSetName
		{
			get
			{
				return this.DataSet.ExtendedProperties[DesignDataSource.EXTPROPNAME_USER_DATASETNAME] as string;
			}
			set
			{
				this.DataSet.ExtendedProperties[DesignDataSource.EXTPROPNAME_USER_DATASETNAME] = value;
			}
		}

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x06001583 RID: 5507 RVA: 0x00078C94 File Offset: 0x00076E94
		// (set) Token: 0x06001584 RID: 5508 RVA: 0x00078CB0 File Offset: 0x00076EB0
		internal string GeneratorDataSetName
		{
			get
			{
				return this.DataSet.ExtendedProperties[DesignDataSource.EXTPROPNAME_GENERATOR_DATASETNAME] as string;
			}
			set
			{
				this.DataSet.ExtendedProperties[DesignDataSource.EXTPROPNAME_GENERATOR_DATASETNAME] = value;
			}
		}

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x06001585 RID: 5509 RVA: 0x00078CC8 File Offset: 0x00076EC8
		// (set) Token: 0x06001586 RID: 5510 RVA: 0x00078CD0 File Offset: 0x00076ED0
		[DataSourceXmlAttribute]
		[Browsable(false)]
		[DefaultValue(null)]
		public string FunctionsComponentName
		{
			get
			{
				return this.functionsComponentName;
			}
			set
			{
				this.functionsComponentName = value;
			}
		}

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x06001587 RID: 5511 RVA: 0x00078CD9 File Offset: 0x00076ED9
		// (set) Token: 0x06001588 RID: 5512 RVA: 0x00078CE1 File Offset: 0x00076EE1
		[DataSourceXmlAttribute]
		[Browsable(false)]
		[DefaultValue(null)]
		public string UserFunctionsComponentName
		{
			get
			{
				return this.userFunctionsComponentName;
			}
			set
			{
				this.userFunctionsComponentName = value;
			}
		}

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x06001589 RID: 5513 RVA: 0x00078CEA File Offset: 0x00076EEA
		// (set) Token: 0x0600158A RID: 5514 RVA: 0x00078CF2 File Offset: 0x00076EF2
		[DataSourceXmlAttribute]
		[Browsable(false)]
		[DefaultValue(null)]
		public string GeneratorFunctionsComponentClassName
		{
			get
			{
				return this.generatorFunctionsComponentClassName;
			}
			set
			{
				this.generatorFunctionsComponentClassName = value;
			}
		}

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x0600158B RID: 5515 RVA: 0x00078CFB File Offset: 0x00076EFB
		internal override StringCollection NamingPropertyNames
		{
			get
			{
				return this.namingPropNames;
			}
		}

		// Token: 0x0600158C RID: 5516 RVA: 0x00078D04 File Offset: 0x00076F04
		void IDataSourceCommandTarget.AddChild(object child, bool fixName)
		{
			Type type = child.GetType();
			if (typeof(DesignTable).IsAssignableFrom(type))
			{
				this.DesignTables.Add((DesignTable)child);
				return;
			}
			if (typeof(DesignRelation).IsAssignableFrom(type))
			{
				this.DesignRelations.Add((DesignRelation)child);
				return;
			}
			if (typeof(IDesignConnection).IsAssignableFrom(type))
			{
				this.DesignConnections.Add((IDesignConnection)child);
				return;
			}
			if (typeof(Source).IsAssignableFrom(type))
			{
				this.Sources.Add((Source)child);
			}
		}

		// Token: 0x0600158D RID: 5517 RVA: 0x00078DAC File Offset: 0x00076FAC
		bool IDataSourceCommandTarget.CanAddChildOfType(Type childType)
		{
			return typeof(DesignTable).IsAssignableFrom(childType) || typeof(IDesignConnection).IsAssignableFrom(childType) || typeof(Source).IsAssignableFrom(childType) || (typeof(DesignRelation).IsAssignableFrom(childType) && ((ICollection)this.DesignTables).Count > 0);
		}

		// Token: 0x0600158E RID: 5518 RVA: 0x00078E14 File Offset: 0x00077014
		bool IDataSourceCommandTarget.CanInsertChildOfType(Type childType, object refChild)
		{
			if (typeof(Source).IsAssignableFrom(childType))
			{
				return refChild is Source;
			}
			if (typeof(IDesignConnection).IsAssignableFrom(childType))
			{
				return refChild is IDesignConnection;
			}
			return typeof(DesignTable).IsAssignableFrom(childType);
		}

		// Token: 0x0600158F RID: 5519 RVA: 0x00078E70 File Offset: 0x00077070
		bool IDataSourceCommandTarget.CanRemoveChildren(ICollection children)
		{
			foreach (object child in children)
			{
				if (!this.CanRemoveChild(child))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001590 RID: 5520 RVA: 0x00078EC8 File Offset: 0x000770C8
		private bool CanRemoveChild(object child)
		{
			bool result = false;
			Type type = child.GetType();
			if (typeof(DesignTable).IsAssignableFrom(type))
			{
				result = this.DesignTables.Contains((DesignTable)child);
			}
			else if (typeof(DesignRelation).IsAssignableFrom(type))
			{
				result = this.DesignRelations.Contains((DesignRelation)child);
			}
			else if (typeof(IDesignConnection).IsAssignableFrom(type))
			{
				result = this.DesignConnections.Contains((IDesignConnection)child);
			}
			else if (typeof(Source).IsAssignableFrom(type))
			{
				result = this.Sources.Contains((Source)child);
			}
			return result;
		}

		// Token: 0x06001591 RID: 5521 RVA: 0x00078F78 File Offset: 0x00077178
		internal ArrayList GetRelatedRelations(ICollection tableList)
		{
			ArrayList arrayList = new ArrayList();
			foreach (object obj in this.DesignRelations)
			{
				DesignRelation designRelation = (DesignRelation)obj;
				DesignTable parentDesignTable = designRelation.ParentDesignTable;
				DesignTable childDesignTable = designRelation.ChildDesignTable;
				foreach (object obj2 in tableList)
				{
					if (parentDesignTable == obj2 || childDesignTable == obj2)
					{
						arrayList.Add(designRelation);
						break;
					}
				}
			}
			return arrayList;
		}

		// Token: 0x06001592 RID: 5522 RVA: 0x00079038 File Offset: 0x00077238
		void IDataSourceCommandTarget.InsertChild(object child, object refChild)
		{
			if (child is DesignTable)
			{
				this.DesignTables.InsertBefore(child, refChild);
				return;
			}
			if (child is DesignRelation)
			{
				this.DesignRelations.InsertBefore(child, refChild);
				return;
			}
			if (child is Source)
			{
				this.Sources.InsertBefore(child, refChild);
				return;
			}
			if (child is IDesignConnection)
			{
				this.DesignConnections.InsertBefore(child, refChild);
			}
		}

		// Token: 0x06001593 RID: 5523 RVA: 0x0007909C File Offset: 0x0007729C
		object IDataSourceCommandTarget.GetObject(int index, bool getSiblingIfOutOfRange)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001594 RID: 5524 RVA: 0x0007909C File Offset: 0x0007729C
		int IDataSourceCommandTarget.IndexOf(object child)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001595 RID: 5525 RVA: 0x000790A4 File Offset: 0x000772A4
		public void ReadXmlSchema(Stream stream, string baseURI)
		{
			DataSourceXmlTextReader xmlReader = new DataSourceXmlTextReader(this, stream, baseURI);
			this.ReadXmlSchema(xmlReader);
		}

		// Token: 0x06001596 RID: 5526 RVA: 0x000790C4 File Offset: 0x000772C4
		public void ReadXmlSchema(TextReader textReader, string baseURI)
		{
			DataSourceXmlTextReader xmlReader = new DataSourceXmlTextReader(this, textReader, baseURI);
			this.ReadXmlSchema(xmlReader);
		}

		// Token: 0x06001597 RID: 5527 RVA: 0x000790E4 File Offset: 0x000772E4
		private void ReadXmlSchema(DataSourceXmlTextReader xmlReader)
		{
			this.designConnections = new DesignConnectionCollection(this);
			this.designTables = new DesignTableCollection(this);
			this.designRelations = new DesignRelationCollection(this);
			this.sources = new SourceCollection(this);
			this.serializer = new DataSourceXmlSerializer();
			this.dataSet = new DataSet();
			this.dataSet.Locale = CultureInfo.InvariantCulture;
			DataSet dataSet = new DataSet();
			dataSet.Locale = CultureInfo.InvariantCulture;
			dataSet.ReadXmlSchema(xmlReader);
			this.dataSet = dataSet;
			foreach (object obj in this.dataSet.Tables)
			{
				DataTable dataTable = (DataTable)obj;
				DesignTable designTable = this.designTables[dataTable.TableName];
				if (designTable == null)
				{
					this.designTables.Add(new DesignTable(dataTable, TableType.DataTable));
				}
				else
				{
					designTable.DataTable = dataTable;
				}
				foreach (object obj2 in dataTable.Constraints)
				{
					Constraint constraint = (Constraint)obj2;
					ForeignKeyConstraint foreignKeyConstraint = constraint as ForeignKeyConstraint;
					if (foreignKeyConstraint != null)
					{
						this.designRelations.Add(new DesignRelation(foreignKeyConstraint));
					}
				}
			}
			foreach (object obj3 in this.dataSet.Relations)
			{
				DataRelation dataRelation = (DataRelation)obj3;
				DesignRelation designRelation = this.designRelations[dataRelation.ChildKeyConstraint];
				if (designRelation != null)
				{
					designRelation.DataRelation = dataRelation;
				}
				else
				{
					this.designRelations.Add(new DesignRelation(dataRelation));
				}
			}
			foreach (object obj4 in this.Sources)
			{
				Source connectionProperty = (Source)obj4;
				this.SetConnectionProperty(connectionProperty);
			}
			foreach (object obj5 in this.DesignTables)
			{
				DesignTable designTable2 = (DesignTable)obj5;
				this.SetConnectionProperty(designTable2.MainSource);
				foreach (object obj6 in designTable2.Sources)
				{
					Source connectionProperty2 = (Source)obj6;
					this.SetConnectionProperty(connectionProperty2);
				}
			}
			this.serializer.InitializeObjects();
		}

		// Token: 0x06001598 RID: 5528 RVA: 0x000793DC File Offset: 0x000775DC
		private void SetConnectionProperty(Source source)
		{
			DbSource dbSource = source as DbSource;
			if (dbSource == null)
			{
				return;
			}
			string connectionRef = dbSource.ConnectionRef;
			if (connectionRef != null && connectionRef.Length != 0)
			{
				IDesignConnection designConnection = this.DesignConnections.Get(connectionRef);
				if (designConnection != null)
				{
					dbSource.Connection = designConnection;
				}
			}
		}

		// Token: 0x06001599 RID: 5529 RVA: 0x00079420 File Offset: 0x00077620
		internal void ReadDataSourceExtraInformation(XmlTextReader xmlTextReader)
		{
			XmlDocument xmlDocument = new XmlDocument();
			XmlNode xmlNode = xmlDocument.ReadNode(xmlTextReader);
			xmlDocument.AppendChild(xmlNode);
			if (this.serializer != null)
			{
				this.serializer.DeserializeBody((XmlElement)xmlNode, this);
			}
		}

		// Token: 0x0600159A RID: 5530 RVA: 0x00079460 File Offset: 0x00077660
		void IDataSourceCommandTarget.RemoveChildren(ICollection children)
		{
			SortedList sortedList = new SortedList();
			foreach (object obj in children)
			{
				if (obj is DesignTable)
				{
					sortedList.Add(-this.DesignTables.IndexOf((DesignTable)obj), obj);
				}
				else
				{
					this.RemoveChild(obj);
				}
			}
			ArrayList relatedRelations = this.GetRelatedRelations(children);
			foreach (object obj2 in relatedRelations)
			{
				DesignRelation child = (DesignRelation)obj2;
				this.RemoveChild(child);
			}
			foreach (object obj3 in sortedList.Values)
			{
				if (obj3 is DesignTable)
				{
					this.RemoveChild(obj3);
				}
			}
		}

		// Token: 0x0600159B RID: 5531 RVA: 0x0007958C File Offset: 0x0007778C
		private void RemoveChild(object child)
		{
			Type type = child.GetType();
			if (typeof(DesignTable).IsAssignableFrom(type))
			{
				this.DesignTables.Remove((DesignTable)child);
				return;
			}
			if (typeof(DesignRelation).IsAssignableFrom(type))
			{
				this.DesignRelations.Remove((DesignRelation)child);
				return;
			}
			if (typeof(IDesignConnection).IsAssignableFrom(type))
			{
				this.DesignConnections.Remove((IDesignConnection)child);
				return;
			}
			if (typeof(Source).IsAssignableFrom(type))
			{
				this.Sources.Remove((Source)child);
			}
		}

		// Token: 0x04000B31 RID: 2865
		private DataSet dataSet;

		// Token: 0x04000B32 RID: 2866
		private DesignTableCollection designTables;

		// Token: 0x04000B33 RID: 2867
		private DesignRelationCollection designRelations;

		// Token: 0x04000B34 RID: 2868
		private DesignConnectionCollection designConnections;

		// Token: 0x04000B35 RID: 2869
		private int defaultConnectionIndex;

		// Token: 0x04000B36 RID: 2870
		private SourceCollection sources;

		// Token: 0x04000B37 RID: 2871
		private TypeAttributes modifier = TypeAttributes.Public;

		// Token: 0x04000B38 RID: 2872
		private SchemaSerializationMode schemaSerializationMode = SchemaSerializationMode.IncludeSchema;

		// Token: 0x04000B39 RID: 2873
		private DataSourceXmlSerializer serializer;

		// Token: 0x04000B3A RID: 2874
		private StringCollection namingPropNames = new StringCollection();

		// Token: 0x04000B3B RID: 2875
		internal static string EXTPROPNAME_USER_DATASETNAME = "Generator_UserDSName";

		// Token: 0x04000B3C RID: 2876
		internal static string EXTPROPNAME_GENERATOR_DATASETNAME = "Generator_DataSetName";

		// Token: 0x04000B3D RID: 2877
		private const string EXTPROPNAME_ENABLE_TABLEADAPTERMANAGER = "EnableTableAdapterManager";

		// Token: 0x04000B3E RID: 2878
		private string functionsComponentName;

		// Token: 0x04000B3F RID: 2879
		private string userFunctionsComponentName;

		// Token: 0x04000B40 RID: 2880
		private string generatorFunctionsComponentClassName;
	}
}
