using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data.Common;
using System.Design;
using System.Globalization;
using System.Reflection;
using System.Xml;

namespace System.Data.Design
{
	// Token: 0x0200023E RID: 574
	internal class DesignTable : DataSourceComponent, IDataSourceNamedObject, INamedObject, IDataSourceXmlSerializable, IDataSourceXmlSpecialOwner, IDataSourceInitAfterLoading, IDataSourceCommandTarget
	{
		// Token: 0x14000043 RID: 67
		// (add) Token: 0x060015F1 RID: 5617 RVA: 0x00079E78 File Offset: 0x00078078
		// (remove) Token: 0x060015F2 RID: 5618 RVA: 0x00079EB0 File Offset: 0x000780B0
		private event EventHandler tableTypeChanged;

		// Token: 0x14000044 RID: 68
		// (add) Token: 0x060015F3 RID: 5619 RVA: 0x00079EE8 File Offset: 0x000780E8
		// (remove) Token: 0x060015F4 RID: 5620 RVA: 0x00079F20 File Offset: 0x00078120
		private event EventHandler constraintsChanged;

		// Token: 0x14000045 RID: 69
		// (add) Token: 0x060015F5 RID: 5621 RVA: 0x00079F58 File Offset: 0x00078158
		// (remove) Token: 0x060015F6 RID: 5622 RVA: 0x00079F90 File Offset: 0x00078190
		private event EventHandler dataAccessorChanged;

		// Token: 0x14000046 RID: 70
		// (add) Token: 0x060015F7 RID: 5623 RVA: 0x00079FC8 File Offset: 0x000781C8
		// (remove) Token: 0x060015F8 RID: 5624 RVA: 0x0007A000 File Offset: 0x00078200
		private event EventHandler dataAccessorChanging;

		// Token: 0x060015F9 RID: 5625 RVA: 0x0007A035 File Offset: 0x00078235
		public DesignTable() : this(null, TableType.DataTable)
		{
		}

		// Token: 0x060015FA RID: 5626 RVA: 0x0007A03F File Offset: 0x0007823F
		public DesignTable(DataTable dataTable) : this(dataTable, TableType.DataTable)
		{
		}

		// Token: 0x060015FB RID: 5627 RVA: 0x0007A04C File Offset: 0x0007824C
		public DesignTable(DataTable dataTable, TableType tableType)
		{
			if (dataTable == null)
			{
				this.dataTable = new DataTable();
				this.dataTable.Locale = CultureInfo.InvariantCulture;
			}
			else
			{
				this.dataTable = dataTable;
			}
			this.TableType = tableType;
			this.AddRemoveConstraintMonitor(true);
			this.namingPropNames.AddRange(new string[]
			{
				"typedPlural",
				"typedName"
			});
		}

		// Token: 0x060015FC RID: 5628 RVA: 0x0007A0C7 File Offset: 0x000782C7
		public DesignTable(DataTable dataTable, TableType tableType, DataColumnMappingCollection mappings) : this(dataTable, tableType)
		{
			this.mappings = mappings;
		}

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x060015FD RID: 5629 RVA: 0x0007A0D8 File Offset: 0x000782D8
		// (set) Token: 0x060015FE RID: 5630 RVA: 0x0007A0F3 File Offset: 0x000782F3
		[DataSourceXmlAttribute]
		[Browsable(false)]
		public string BaseClass
		{
			get
			{
				if (StringUtil.NotEmptyAfterTrim(this.baseClass))
				{
					return this.baseClass;
				}
				return "System.ComponentModel.Component";
			}
			set
			{
				this.baseClass = value;
			}
		}

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x060015FF RID: 5631 RVA: 0x0007A0FC File Offset: 0x000782FC
		// (set) Token: 0x06001600 RID: 5632 RVA: 0x0007A124 File Offset: 0x00078324
		public IDesignConnection Connection
		{
			get
			{
				if (this.TableType == TableType.RadTable)
				{
					DbSource dbSource = this.EnsureDbSource();
					return dbSource.Connection;
				}
				return null;
			}
			set
			{
				if (this.TableType == TableType.RadTable)
				{
					DbSource dbSource = this.EnsureDbSource();
					dbSource.Connection = value;
				}
			}
		}

		// Token: 0x14000047 RID: 71
		// (add) Token: 0x06001601 RID: 5633 RVA: 0x0007A148 File Offset: 0x00078348
		// (remove) Token: 0x06001602 RID: 5634 RVA: 0x0007A151 File Offset: 0x00078351
		internal event EventHandler ConstraintChanged
		{
			add
			{
				this.constraintsChanged += value;
			}
			remove
			{
				this.constraintsChanged -= value;
			}
		}

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x06001603 RID: 5635 RVA: 0x0007A15A File Offset: 0x0007835A
		// (set) Token: 0x06001604 RID: 5636 RVA: 0x0007A162 File Offset: 0x00078362
		internal DataAccessor DataAccessor
		{
			get
			{
				return this.dataAccessor;
			}
			set
			{
				if (this.dataAccessorChanging != null)
				{
					this.dataAccessorChanging(this, new EventArgs());
				}
				this.dataAccessor = value;
				if (this.dataAccessorChanged != null)
				{
					this.dataAccessorChanged(this, new EventArgs());
				}
			}
		}

		// Token: 0x14000048 RID: 72
		// (add) Token: 0x06001605 RID: 5637 RVA: 0x0007A19D File Offset: 0x0007839D
		// (remove) Token: 0x06001606 RID: 5638 RVA: 0x0007A1A6 File Offset: 0x000783A6
		internal event EventHandler DataAccessorChanged
		{
			add
			{
				this.dataAccessorChanged += value;
			}
			remove
			{
				this.dataAccessorChanged -= value;
			}
		}

		// Token: 0x14000049 RID: 73
		// (add) Token: 0x06001607 RID: 5639 RVA: 0x0007A1AF File Offset: 0x000783AF
		// (remove) Token: 0x06001608 RID: 5640 RVA: 0x0007A1B8 File Offset: 0x000783B8
		internal event EventHandler DataAccessorChanging
		{
			add
			{
				this.dataAccessorChanging += value;
			}
			remove
			{
				this.dataAccessorChanging -= value;
			}
		}

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x06001609 RID: 5641 RVA: 0x0007A1C1 File Offset: 0x000783C1
		// (set) Token: 0x0600160A RID: 5642 RVA: 0x0007A1E7 File Offset: 0x000783E7
		[DataSourceXmlAttribute]
		[Browsable(false)]
		public string DataAccessorName
		{
			get
			{
				if (StringUtil.NotEmptyAfterTrim(this.dataAccessorName))
				{
					return this.dataAccessorName;
				}
				return this.Name + "TableAdapter";
			}
			set
			{
				this.dataAccessorName = value;
			}
		}

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x0600160B RID: 5643 RVA: 0x0007A1F0 File Offset: 0x000783F0
		// (set) Token: 0x0600160C RID: 5644 RVA: 0x0007A1F8 File Offset: 0x000783F8
		[Browsable(false)]
		public DataTable DataTable
		{
			get
			{
				return this.dataTable;
			}
			set
			{
				if (this.dataTable != value)
				{
					if (this.dataTable != null)
					{
						this.AddRemoveConstraintMonitor(false);
					}
					this.dataTable = value;
					if (this.dataTable != null)
					{
						this.AddRemoveConstraintMonitor(true);
					}
				}
			}
		}

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x0600160D RID: 5645 RVA: 0x0007A228 File Offset: 0x00078428
		// (set) Token: 0x0600160E RID: 5646 RVA: 0x0007A244 File Offset: 0x00078444
		[DefaultValue(null)]
		public DbSourceCommand DeleteCommand
		{
			get
			{
				DbSource dbSource = this.EnsureDbSource();
				return dbSource.DeleteCommand;
			}
			set
			{
				DbSource dbSource = this.EnsureDbSource();
				dbSource.DeleteCommand = value;
			}
		}

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x0600160F RID: 5647 RVA: 0x0007A25F File Offset: 0x0007845F
		[Browsable(false)]
		public DesignColumnCollection DesignColumns
		{
			get
			{
				if (this.designColumns == null)
				{
					this.designColumns = new DesignColumnCollection(this);
				}
				return this.designColumns;
			}
		}

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x06001610 RID: 5648 RVA: 0x0007A1F0 File Offset: 0x000783F0
		protected override object ExternalPropertyHost
		{
			get
			{
				return this.dataTable;
			}
		}

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x06001611 RID: 5649 RVA: 0x0007A27C File Offset: 0x0007847C
		internal bool HasAnyUpdateCommand
		{
			get
			{
				return this.TableType == TableType.RadTable && this.MainSource != null && this.MainSource is DbSource && ((DbSource)this.MainSource).CommandOperation == CommandOperation.Select && (this.DeleteCommand != null || this.InsertCommand != null || this.UpdateCommand != null);
			}
		}

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x06001612 RID: 5650 RVA: 0x0007A2D8 File Offset: 0x000784D8
		internal bool HasAnyExpressionColumn
		{
			get
			{
				DataTable dataTable = this.DataTable;
				foreach (object obj in dataTable.Columns)
				{
					DataColumn dataColumn = (DataColumn)obj;
					if (dataColumn.Expression != null && dataColumn.Expression.Length > 0)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x06001613 RID: 5651 RVA: 0x0007A354 File Offset: 0x00078554
		// (set) Token: 0x06001614 RID: 5652 RVA: 0x0007A370 File Offset: 0x00078570
		[DefaultValue(null)]
		public DbSourceCommand InsertCommand
		{
			get
			{
				DbSource dbSource = this.EnsureDbSource();
				return dbSource.InsertCommand;
			}
			set
			{
				DbSource dbSource = this.EnsureDbSource();
				dbSource.InsertCommand = value;
			}
		}

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x06001615 RID: 5653 RVA: 0x0007A38C File Offset: 0x0007858C
		// (set) Token: 0x06001616 RID: 5654 RVA: 0x0007A3D0 File Offset: 0x000785D0
		[Browsable(false)]
		[DataSourceXmlSubItem(Name = "MainSource", ItemType = typeof(Source))]
		public Source MainSource
		{
			get
			{
				if (this.mainSource == null)
				{
					DbSource dbSource = new DbSource();
					if (this.Owner != null)
					{
						dbSource.Connection = this.Owner.DefaultConnection;
					}
					this.MainSource = dbSource;
				}
				return this.mainSource;
			}
			set
			{
				if (this.mainSource != null)
				{
					this.mainSource.Owner = null;
				}
				this.mainSource = value;
				if (value != null)
				{
					this.mainSource.Owner = this;
					if (StringUtil.EmptyOrSpace(this.mainSource.Name))
					{
						this.mainSource.Name = "Fill";
					}
				}
			}
		}

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x06001617 RID: 5655 RVA: 0x0007A429 File Offset: 0x00078629
		// (set) Token: 0x06001618 RID: 5656 RVA: 0x0007A444 File Offset: 0x00078644
		[Browsable(false)]
		[DataSourceXmlElement(Name = "Mappings", SpecialWay = true)]
		public DataColumnMappingCollection Mappings
		{
			get
			{
				if (this.mappings == null)
				{
					this.mappings = new DataColumnMappingCollection();
				}
				return this.mappings;
			}
			set
			{
				this.mappings = value;
			}
		}

		// Token: 0x06001619 RID: 5657 RVA: 0x0007A44D File Offset: 0x0007864D
		private bool ShouldSerializeMappings()
		{
			return this.mappings != null && this.mappings.Count > 0;
		}

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x0600161A RID: 5658 RVA: 0x0007A467 File Offset: 0x00078667
		// (set) Token: 0x0600161B RID: 5659 RVA: 0x0007A46F File Offset: 0x0007866F
		[DefaultValue(TypeAttributes.Public)]
		[DataSourceXmlAttribute]
		public TypeAttributes DataAccessorModifier
		{
			get
			{
				return this.dataAccessorModifier;
			}
			set
			{
				this.dataAccessorModifier = value;
			}
		}

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x0600161C RID: 5660 RVA: 0x0007A478 File Offset: 0x00078678
		// (set) Token: 0x0600161D RID: 5661 RVA: 0x0007A485 File Offset: 0x00078685
		[DefaultValue("")]
		[DataSourceXmlAttribute]
		[MergableProperty(false)]
		public string Name
		{
			get
			{
				return this.dataTable.TableName;
			}
			set
			{
				if (this.dataTable.TableName != value)
				{
					if (this.CollectionParent != null)
					{
						this.CollectionParent.ValidateUniqueName(this, value);
					}
					this.dataTable.TableName = value;
				}
			}
		}

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x0600161E RID: 5662 RVA: 0x0007A4BB File Offset: 0x000786BB
		// (set) Token: 0x0600161F RID: 5663 RVA: 0x0007A4C4 File Offset: 0x000786C4
		internal DesignDataSource Owner
		{
			get
			{
				return this.owner;
			}
			set
			{
				if (this.owner != value)
				{
					string text = (this.owner != null) ? this.owner.DataSet.Namespace : "";
					object obj = (value != null) ? null : "";
					this.owner = value;
				}
			}
		}

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x06001620 RID: 5664 RVA: 0x0007A510 File Offset: 0x00078710
		public DbSourceParameterCollection Parameters
		{
			get
			{
				DbSource dbSource = this.MainSource as DbSource;
				if (dbSource != null && dbSource.SelectCommand != null)
				{
					return dbSource.SelectCommand.Parameters;
				}
				return null;
			}
		}

		// Token: 0x06001621 RID: 5665 RVA: 0x0007A544 File Offset: 0x00078744
		private bool ShouldSerializeParameters()
		{
			if (this.TableType != TableType.RadTable)
			{
				return false;
			}
			DbSourceParameterCollection parameters = this.Parameters;
			return parameters != null && 0 < parameters.Count;
		}

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x06001622 RID: 5666 RVA: 0x0007A571 File Offset: 0x00078771
		// (set) Token: 0x06001623 RID: 5667 RVA: 0x0007A580 File Offset: 0x00078780
		[Browsable(false)]
		public DataColumn[] PrimaryKeyColumns
		{
			get
			{
				return this.DataTable.PrimaryKey;
			}
			set
			{
				this.AddRemoveConstraintMonitor(false);
				try
				{
					base.SetPropertyValue("PrimaryKey", value);
					this.OnConstraintChanged();
				}
				finally
				{
					this.AddRemoveConstraintMonitor(true);
				}
			}
		}

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x06001624 RID: 5668 RVA: 0x0007A5C0 File Offset: 0x000787C0
		// (set) Token: 0x06001625 RID: 5669 RVA: 0x0007A5C8 File Offset: 0x000787C8
		[DefaultValue(null)]
		[DataSourceXmlAttribute]
		[Browsable(false)]
		public string Provider
		{
			get
			{
				return this.provider;
			}
			set
			{
				this.provider = value;
			}
		}

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x06001626 RID: 5670 RVA: 0x0007A5D4 File Offset: 0x000787D4
		[Browsable(false)]
		public string PublicTypeName
		{
			get
			{
				TableType tableType = this.tableType;
				string result;
				if (tableType != TableType.DataTable)
				{
					if (tableType != TableType.RadTable)
					{
						return null;
					}
					result = "DataTable";
				}
				else
				{
					result = "DataTable";
				}
				return result;
			}
		}

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x06001627 RID: 5671 RVA: 0x0007A604 File Offset: 0x00078804
		// (set) Token: 0x06001628 RID: 5672 RVA: 0x0007A620 File Offset: 0x00078820
		[Browsable(false)]
		public DbSourceCommand SelectCommand
		{
			get
			{
				DbSource dbSource = this.EnsureDbSource();
				return dbSource.SelectCommand;
			}
			set
			{
				DbSource dbSource = this.EnsureDbSource();
				dbSource.SelectCommand = value;
			}
		}

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x06001629 RID: 5673 RVA: 0x0007A63B File Offset: 0x0007883B
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

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x0600162A RID: 5674 RVA: 0x0007A657 File Offset: 0x00078857
		// (set) Token: 0x0600162B RID: 5675 RVA: 0x0007A65F File Offset: 0x0007885F
		[Browsable(false)]
		public TableType TableType
		{
			get
			{
				return this.tableType;
			}
			set
			{
				this.tableType = value;
				if (this.tableType == TableType.RadTable)
				{
					this.DataAccessor = new DataAccessor(this);
					return;
				}
				this.DataAccessor = null;
			}
		}

		// Token: 0x1400004A RID: 74
		// (add) Token: 0x0600162C RID: 5676 RVA: 0x0007A685 File Offset: 0x00078885
		// (remove) Token: 0x0600162D RID: 5677 RVA: 0x0007A68E File Offset: 0x0007888E
		internal event EventHandler TableTypeChanged
		{
			add
			{
				this.tableTypeChanged += value;
			}
			remove
			{
				this.tableTypeChanged -= value;
			}
		}

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x0600162E RID: 5678 RVA: 0x0007A698 File Offset: 0x00078898
		// (set) Token: 0x0600162F RID: 5679 RVA: 0x0007A6B4 File Offset: 0x000788B4
		[DefaultValue(null)]
		public DbSourceCommand UpdateCommand
		{
			get
			{
				DbSource dbSource = this.EnsureDbSource();
				return dbSource.UpdateCommand;
			}
			set
			{
				DbSource dbSource = this.EnsureDbSource();
				dbSource.UpdateCommand = value;
			}
		}

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x06001630 RID: 5680 RVA: 0x0007A6CF File Offset: 0x000788CF
		// (set) Token: 0x06001631 RID: 5681 RVA: 0x0007A6D7 File Offset: 0x000788D7
		[DataSourceXmlAttribute(ItemType = typeof(bool))]
		[Browsable(false)]
		[DefaultValue(false)]
		public bool WebServiceAttribute
		{
			get
			{
				return this.webServiceAttribute;
			}
			set
			{
				this.webServiceAttribute = value;
			}
		}

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x06001632 RID: 5682 RVA: 0x0007A6E0 File Offset: 0x000788E0
		// (set) Token: 0x06001633 RID: 5683 RVA: 0x0007A6E8 File Offset: 0x000788E8
		[DataSourceXmlAttribute]
		[Browsable(false)]
		public string WebServiceDescription
		{
			get
			{
				return this.webServiceDescription;
			}
			set
			{
				this.webServiceDescription = value;
			}
		}

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x06001634 RID: 5684 RVA: 0x0007A6F1 File Offset: 0x000788F1
		// (set) Token: 0x06001635 RID: 5685 RVA: 0x0007A6F9 File Offset: 0x000788F9
		[DataSourceXmlAttribute]
		[Browsable(false)]
		public string WebServiceNamespace
		{
			get
			{
				return this.webServiceNamespace;
			}
			set
			{
				this.webServiceNamespace = value;
			}
		}

		// Token: 0x06001636 RID: 5686 RVA: 0x0007A704 File Offset: 0x00078904
		void IDataSourceCommandTarget.AddChild(object child, bool fixName)
		{
			if (child is DesignColumn)
			{
				this.DesignColumns.Add((DesignColumn)child);
				return;
			}
			if (child is Source)
			{
				if (child is DbSource)
				{
					((DbSource)child).Connection = this.Connection;
					if (this.Connection != null)
					{
						((DbSource)child).ConnectionRef = this.Connection.Name;
					}
				}
				this.Sources.Add((Source)child);
			}
		}

		// Token: 0x06001637 RID: 5687 RVA: 0x0007A77C File Offset: 0x0007897C
		private void AddRemoveConstraintMonitor(bool addEventHandler)
		{
			if (addEventHandler)
			{
				if (this.DataTable != null)
				{
					this.DataTable.Constraints.CollectionChanged += this.OnConstraintCollectionChanged;
					return;
				}
			}
			else if (this.DataTable != null)
			{
				this.DataTable.Constraints.CollectionChanged -= this.OnConstraintCollectionChanged;
			}
		}

		// Token: 0x06001638 RID: 5688 RVA: 0x0007A7D8 File Offset: 0x000789D8
		bool IDataSourceCommandTarget.CanAddChildOfType(Type childType)
		{
			return typeof(DesignColumn).IsAssignableFrom(childType) || (this.TableType != TableType.DataTable && typeof(Source).IsAssignableFrom(childType)) || (typeof(DesignRelation).IsAssignableFrom(childType) && this.DesignColumns.Count > 0);
		}

		// Token: 0x06001639 RID: 5689 RVA: 0x0007A838 File Offset: 0x00078A38
		bool IDataSourceCommandTarget.CanInsertChildOfType(Type childType, object refChild)
		{
			if (typeof(DesignColumn).IsAssignableFrom(childType))
			{
				return refChild is DesignColumn;
			}
			return typeof(Source).IsAssignableFrom(childType) && this.TableType != TableType.DataTable && refChild is Source;
		}

		// Token: 0x0600163A RID: 5690 RVA: 0x0007A888 File Offset: 0x00078A88
		bool IDataSourceCommandTarget.CanRemoveChildren(ICollection children)
		{
			bool result = true;
			foreach (object obj in children)
			{
				if (obj is DesignColumn)
				{
					if (((DesignColumn)obj).DesignTable != this)
					{
						result = false;
						break;
					}
				}
				else if (obj is Source)
				{
					if (!this.Sources.Contains((Source)obj))
					{
						result = false;
						break;
					}
				}
				else
				{
					if (!(obj is DataAccessor))
					{
						result = false;
						break;
					}
					if (((DataAccessor)obj).DesignTable != this)
					{
						result = false;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x0600163B RID: 5691 RVA: 0x0007A92C File Offset: 0x00078B2C
		internal void ConvertTableTypeTo(TableType newTableType)
		{
			if (newTableType != this.tableType)
			{
				IComponentChangeService componentChangeService = (IComponentChangeService)this.GetService(typeof(IComponentChangeService));
				if (componentChangeService != null)
				{
					componentChangeService.OnComponentChanging(this, null);
				}
				try
				{
					this.TableType = newTableType;
					this.mainSource = null;
					this.sources = null;
					this.mappings = null;
					this.provider = string.Empty;
					this.OnTableTypeChanged();
				}
				finally
				{
					if (componentChangeService != null)
					{
						componentChangeService.OnComponentChanged(this, null, null, null);
					}
				}
			}
		}

		// Token: 0x0600163C RID: 5692 RVA: 0x0007A9B4 File Offset: 0x00078BB4
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.AddRemoveConstraintMonitor(false);
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600163D RID: 5693 RVA: 0x0007A9C8 File Offset: 0x00078BC8
		private DbSource EnsureDbSource()
		{
			if (this.tableType != TableType.RadTable)
			{
				throw new InternalException(null, "Operation invalid. Table gets data from something else than a database.", 20007, false, false);
			}
			if (this.MainSource == null)
			{
				this.MainSource = new DbSource();
			}
			DbSource dbSource = this.mainSource as DbSource;
			if (dbSource == null)
			{
				throw new InternalException(null, "Operation invalid. Table gets data from something else than a database.", 20007, false, false);
			}
			if (dbSource.DeleteCommand != null && StringUtil.EmptyOrSpace(dbSource.DeleteCommand.Name))
			{
				dbSource.DeleteCommand.Name = "(DeleteCommand)";
			}
			if (dbSource.UpdateCommand != null && StringUtil.EmptyOrSpace(dbSource.UpdateCommand.Name))
			{
				dbSource.UpdateCommand.Name = "(UpdateCommand)";
			}
			if (dbSource.SelectCommand != null && StringUtil.EmptyOrSpace(dbSource.SelectCommand.Name))
			{
				dbSource.SelectCommand.Name = "(SelectCommand)";
			}
			if (dbSource.InsertCommand != null && StringUtil.EmptyOrSpace(dbSource.InsertCommand.Name))
			{
				dbSource.InsertCommand.Name = "(InsertCommand)";
			}
			return dbSource;
		}

		// Token: 0x0600163E RID: 5694 RVA: 0x0007AAD0 File Offset: 0x00078CD0
		object IDataSourceCommandTarget.GetObject(int index, bool getSiblingIfOutOfRange)
		{
			int count = this.DesignColumns.Count;
			int num = (this.TableType == TableType.DataTable) ? 0 : this.Sources.Count;
			int num2 = (this.TableType == TableType.DataTable) ? count : (count + num + 1);
			if (num2 <= 0)
			{
				return null;
			}
			if (!getSiblingIfOutOfRange && (index < 0 || index >= num2))
			{
				return null;
			}
			if (index >= num2)
			{
				index = num2 - 1;
			}
			IList list = this.Sources;
			if (index < 0)
			{
				if (count > 0)
				{
					return this.DesignColumns[0];
				}
				if (this.mainSource != null)
				{
					return this.mainSource;
				}
				if (num > 0)
				{
					return list[0];
				}
				return null;
			}
			else
			{
				if (index < count)
				{
					return this.DesignColumns[index];
				}
				if (this.TableType != TableType.DataTable)
				{
					index -= count;
					if (index == 0)
					{
						return this.MainSource;
					}
					index--;
					if (index < num)
					{
						return list[index];
					}
				}
				return null;
			}
		}

		// Token: 0x0600163F RID: 5695 RVA: 0x0007ABA0 File Offset: 0x00078DA0
		internal ArrayList GetRelatedDataConstraints(ICollection columns, bool uniqueOnly)
		{
			ArrayList arrayList = new ArrayList();
			foreach (object obj in this.dataTable.Constraints)
			{
				Constraint constraint = (Constraint)obj;
				DataColumn[] array = null;
				if (constraint is UniqueConstraint)
				{
					array = ((UniqueConstraint)constraint).Columns;
				}
				else if (!uniqueOnly && constraint is ForeignKeyConstraint)
				{
					array = ((ForeignKeyConstraint)constraint).Columns;
				}
				if (array != null)
				{
					foreach (object obj2 in columns)
					{
						if (obj2 is DesignColumn)
						{
							DesignColumn designColumn = obj2 as DesignColumn;
							if (((IList)array).Contains(designColumn.DataColumn))
							{
								arrayList.Add(constraint);
								break;
							}
						}
					}
				}
			}
			return arrayList;
		}

		// Token: 0x06001640 RID: 5696 RVA: 0x0007ACA4 File Offset: 0x00078EA4
		internal bool IsForeignKeyConstraint(DataColumn column)
		{
			foreach (object obj in this.dataTable.Constraints)
			{
				Constraint constraint = (Constraint)obj;
				DataColumn[] array = null;
				if (constraint is ForeignKeyConstraint)
				{
					array = ((ForeignKeyConstraint)constraint).Columns;
				}
				if (array != null && ((IList)array).Contains(column))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001641 RID: 5697 RVA: 0x0007AD28 File Offset: 0x00078F28
		internal string GetUniqueRelationName(string proposedName)
		{
			return this.GetUniqueRelationName(proposedName, true, 1);
		}

		// Token: 0x06001642 RID: 5698 RVA: 0x0007AD33 File Offset: 0x00078F33
		internal string GetUniqueRelationName(string proposedName, int startSuffix)
		{
			return this.GetUniqueRelationName(proposedName, false, startSuffix);
		}

		// Token: 0x06001643 RID: 5699 RVA: 0x0007AD40 File Offset: 0x00078F40
		internal string GetUniqueRelationName(string proposedName, bool firstTryProposedName, int startSuffix)
		{
			if (this.Owner == null)
			{
				throw new InternalException("Need have DataSource");
			}
			SimpleNamedObjectCollection simpleNamedObjectCollection = new SimpleNamedObjectCollection();
			foreach (object obj in this.Owner.DesignRelations)
			{
				DesignRelation designRelation = (DesignRelation)obj;
				simpleNamedObjectCollection.Add(new SimpleNamedObject(designRelation.Name));
			}
			foreach (object obj2 in this.DataTable.Constraints)
			{
				Constraint constraint = (Constraint)obj2;
				simpleNamedObjectCollection.Add(new SimpleNamedObject(constraint.ConstraintName));
			}
			INameService nameService = simpleNamedObjectCollection.GetNameService();
			if (firstTryProposedName)
			{
				return nameService.CreateUniqueName(simpleNamedObjectCollection, proposedName);
			}
			return nameService.CreateUniqueName(simpleNamedObjectCollection, proposedName, startSuffix);
		}

		// Token: 0x06001644 RID: 5700 RVA: 0x0007AE44 File Offset: 0x00079044
		int IDataSourceCommandTarget.IndexOf(object child)
		{
			if (child is DesignColumn)
			{
				return this.DesignColumns.IndexOf((DesignColumn)child);
			}
			if (child is Source && this.TableType != TableType.DataTable)
			{
				if (child == this.mainSource)
				{
					return this.DesignColumns.Count;
				}
				int num = this.Sources.IndexOf((Source)child);
				if (num >= 0)
				{
					return this.DesignColumns.Count + num + 1;
				}
			}
			return -1;
		}

		// Token: 0x06001645 RID: 5701 RVA: 0x0007AEB8 File Offset: 0x000790B8
		void IDataSourceInitAfterLoading.InitializeAfterLoading()
		{
			if (this.Name == null || this.Name.Length == 0)
			{
				throw new DataSourceSerializationException(SR.GetString("DTDS_NameIsRequired", new object[]
				{
					"RadTable"
				}));
			}
			if (this.dataTable.DataSet != this.Owner.DataSet)
			{
				throw new DataSourceSerializationException(SR.GetString("DTDS_TableNotMatch", new object[]
				{
					this.Name
				}));
			}
		}

		// Token: 0x06001646 RID: 5702 RVA: 0x0007AF30 File Offset: 0x00079130
		void IDataSourceCommandTarget.InsertChild(object child, object refChild)
		{
			if (refChild == null)
			{
				((IDataSourceCommandTarget)this).AddChild(child, true);
				return;
			}
			if (child is DesignColumn)
			{
				this.DesignColumns.InsertBefore(child, refChild);
				return;
			}
			if (this.TableType != TableType.DataTable && child is Source)
			{
				this.Sources.InsertBefore(child, refChild);
			}
		}

		// Token: 0x06001647 RID: 5703 RVA: 0x0007AF7C File Offset: 0x0007917C
		private bool IsInConstraintCollection(Constraint constraint)
		{
			return this.DataTable != null && this.DataTable.Constraints[constraint.ConstraintName] == constraint;
		}

		// Token: 0x06001648 RID: 5704 RVA: 0x0007AFA1 File Offset: 0x000791A1
		private void OnConstraintCollectionChanged(object sender, CollectionChangeEventArgs ccevent)
		{
			if (!this.inAccessConstraints)
			{
				this.OnConstraintChanged();
			}
		}

		// Token: 0x06001649 RID: 5705 RVA: 0x0007AFB4 File Offset: 0x000791B4
		private void OnConstraintChanged()
		{
			if (this.constraintsChanged != null)
			{
				this.constraintsChanged(this, new EventArgs());
				IComponentChangeService componentChangeService = (IComponentChangeService)this.GetService(typeof(IComponentChangeService));
				if (componentChangeService != null)
				{
					componentChangeService.OnComponentChanged(this, null, null, null);
				}
			}
		}

		// Token: 0x0600164A RID: 5706 RVA: 0x0007AFFD File Offset: 0x000791FD
		internal void OnTableTypeChanged()
		{
			if (this.tableTypeChanged != null)
			{
				this.tableTypeChanged(this, EventArgs.Empty);
			}
		}

		// Token: 0x0600164B RID: 5707 RVA: 0x0007B018 File Offset: 0x00079218
		private bool AddPrimaryKeyFromSchemaTable(DataTable schemaTable)
		{
			if (schemaTable.PrimaryKey.Length != 0 && this.DataTable.PrimaryKey.Length == 0)
			{
				DataColumn[] array = new DataColumn[schemaTable.PrimaryKey.Length];
				for (int i = 0; i < schemaTable.PrimaryKey.Length; i++)
				{
					DataColumn dataColumn = schemaTable.PrimaryKey[i];
					if (!this.Mappings.Contains(dataColumn.ColumnName))
					{
						return false;
					}
					string dataSetColumn = this.Mappings[dataColumn.ColumnName].DataSetColumn;
					if (!this.DataTable.Columns.Contains(dataSetColumn))
					{
						return false;
					}
					DataColumn dataColumn2 = this.DataTable.Columns[dataSetColumn];
					array[i] = dataColumn2;
				}
				this.PrimaryKeyColumns = array;
				return true;
			}
			return false;
		}

		// Token: 0x0600164C RID: 5708 RVA: 0x0007B0D0 File Offset: 0x000792D0
		void IDataSourceXmlSpecialOwner.ReadSpecialItem(string propertyName, XmlNode xmlNode, DataSourceXmlSerializer serializer)
		{
			if (propertyName == "Mappings")
			{
				string sourceColumn = string.Empty;
				string dataSetColumn = string.Empty;
				XmlElement xmlElement = xmlNode as XmlElement;
				if (xmlElement != null)
				{
					foreach (object obj in xmlElement.ChildNodes)
					{
						XmlNode xmlNode2 = (XmlNode)obj;
						XmlElement xmlElement2 = xmlNode2 as XmlElement;
						if (xmlElement2 != null && xmlElement2.LocalName == "Mapping")
						{
							XmlAttribute xmlAttribute = xmlElement2.Attributes["SourceColumn"];
							if (xmlAttribute != null)
							{
								sourceColumn = xmlAttribute.InnerText;
							}
							xmlAttribute = xmlElement2.Attributes["DataSetColumn"];
							if (xmlAttribute != null)
							{
								dataSetColumn = xmlAttribute.InnerText;
							}
							DataColumnMapping value = new DataColumnMapping(sourceColumn, dataSetColumn);
							this.Mappings.Add(value);
						}
					}
				}
			}
		}

		// Token: 0x0600164D RID: 5709 RVA: 0x0007B1CC File Offset: 0x000793CC
		void IDataSourceXmlSerializable.ReadXml(XmlElement xmlElement, DataSourceXmlSerializer serializer)
		{
			if (xmlElement.LocalName == "TableAdapter" || xmlElement.LocalName == "DbTable")
			{
				this.TableType = TableType.RadTable;
				serializer.DeserializeBody(xmlElement, this);
			}
		}

		// Token: 0x0600164E RID: 5710 RVA: 0x0007B204 File Offset: 0x00079404
		private DataColumn FindSharedColumn(ICollection dataColumns, ICollection designColumns)
		{
			foreach (object obj in dataColumns)
			{
				DataColumn dataColumn = (DataColumn)obj;
				foreach (object obj2 in designColumns)
				{
					DesignColumn designColumn = obj2 as DesignColumn;
					if (designColumn != null && designColumn.DataColumn == dataColumn)
					{
						return dataColumn;
					}
				}
			}
			return null;
		}

		// Token: 0x0600164F RID: 5711 RVA: 0x00003937 File Offset: 0x00001B37
		private void RemoveColumnsFromSource(Source source, string[] colsToRemove)
		{
		}

		// Token: 0x06001650 RID: 5712 RVA: 0x0007B2B0 File Offset: 0x000794B0
		void IDataSourceCommandTarget.RemoveChildren(ICollection children)
		{
			if (this.owner != null)
			{
				ArrayList relatedRelations = this.owner.GetRelatedRelations(new DesignTable[]
				{
					this
				});
				if (relatedRelations.Count > 0)
				{
					int num = 0;
					ArrayList arrayList = new ArrayList();
					foreach (object obj in relatedRelations)
					{
						DesignRelation designRelation = (DesignRelation)obj;
						if (designRelation.ParentDesignTable == this)
						{
							DataColumn dataColumn = this.FindSharedColumn(designRelation.ParentDataColumns, children);
							if (dataColumn != null)
							{
								num++;
								arrayList.Add(designRelation);
								continue;
							}
						}
						if (designRelation.ChildDesignTable == this)
						{
							DataColumn dataColumn2 = this.FindSharedColumn(designRelation.ChildDataColumns, children);
							if (dataColumn2 != null)
							{
								num++;
								arrayList.Add(designRelation);
							}
						}
					}
					if (num > 0)
					{
						foreach (object obj2 in arrayList)
						{
							DesignRelation designRelation2 = (DesignRelation)obj2;
							if (designRelation2.Owner != null)
							{
								designRelation2.Owner.DesignRelations.Remove(designRelation2);
							}
						}
					}
				}
			}
			ArrayList relatedDataConstraints = this.GetRelatedDataConstraints(children, true);
			foreach (object obj3 in relatedDataConstraints)
			{
				UniqueConstraint uniqueConstraint = (UniqueConstraint)obj3;
				if (uniqueConstraint.IsPrimaryKey)
				{
					this.PrimaryKeyColumns = null;
				}
				else
				{
					this.RemoveConstraint(uniqueConstraint);
				}
			}
			relatedDataConstraints = this.GetRelatedDataConstraints(children, false);
			foreach (object obj4 in relatedDataConstraints)
			{
				Constraint constraint = (Constraint)obj4;
				this.RemoveConstraint(constraint);
			}
			ArrayList arrayList2 = new ArrayList();
			foreach (object obj5 in children)
			{
				if (obj5 is DesignColumn)
				{
					DesignColumn designColumn = (DesignColumn)obj5;
					string[] array = DataDesignUtil.MapColumnNames(this.Mappings, new string[]
					{
						designColumn.Name
					}, DataDesignUtil.MappingDirection.DataSetToSource);
					arrayList2.Add(array[0]);
					this.DesignColumns.Remove((DesignColumn)obj5);
					this.RemoveColumnMapping(designColumn.Name);
				}
				else if (obj5 is Source)
				{
					this.Sources.Remove((Source)obj5);
				}
				else if (obj5 is DataAccessor)
				{
					this.ConvertTableTypeTo(TableType.DataTable);
				}
			}
			if (arrayList2.Count > 0)
			{
				string[] colsToRemove = (string[])arrayList2.ToArray(typeof(string));
				this.RemoveColumnsFromSource(this.MainSource, colsToRemove);
				foreach (object obj6 in this.Sources)
				{
					Source source = (Source)obj6;
					this.RemoveColumnsFromSource(source, colsToRemove);
				}
			}
		}

		// Token: 0x06001651 RID: 5713 RVA: 0x0007B60C File Offset: 0x0007980C
		internal void RemoveConstraint(Constraint constraint)
		{
			IComponentChangeService componentChangeService = (IComponentChangeService)this.GetService(typeof(IComponentChangeService));
			if (componentChangeService != null)
			{
				componentChangeService.OnComponentChanging(this, null);
			}
			try
			{
				this.inAccessConstraints = true;
				if (this.dataTable.Constraints.CanRemove(constraint))
				{
					this.dataTable.Constraints.Remove(constraint);
				}
				else if (this.dataTable.Constraints.Count == 1)
				{
					if (this.dataTable.Constraints[0] == constraint)
					{
						this.dataTable.Constraints.Clear();
					}
				}
				else
				{
					Constraint[] array = new Constraint[this.dataTable.Constraints.Count - 1];
					ArrayList arrayList = new ArrayList();
					int num = 0;
					foreach (object obj in this.dataTable.Constraints)
					{
						Constraint constraint2 = (Constraint)obj;
						if (constraint2 != constraint)
						{
							array[num++] = constraint2;
						}
					}
					if (this.Owner != null)
					{
						foreach (object obj2 in this.Owner.DataSet.Relations)
						{
							DataRelation dataRelation = (DataRelation)obj2;
							if (dataRelation.ChildTable == this.dataTable)
							{
								arrayList.Add(dataRelation);
							}
						}
						foreach (object obj3 in arrayList)
						{
							DataRelation relation = (DataRelation)obj3;
							this.Owner.DataSet.Relations.Remove(relation);
						}
					}
					this.dataTable.Constraints.Clear();
					this.dataTable.Constraints.AddRange(array);
					if (this.Owner != null)
					{
						foreach (object obj4 in arrayList)
						{
							DataRelation relation2 = (DataRelation)obj4;
							this.Owner.DataSet.Relations.Add(relation2);
						}
					}
				}
			}
			finally
			{
				this.inAccessConstraints = false;
				this.OnConstraintChanged();
			}
		}

		// Token: 0x06001652 RID: 5714 RVA: 0x00003937 File Offset: 0x00001B37
		internal void RemoveColumnMapping(string columnName)
		{
		}

		// Token: 0x06001653 RID: 5715 RVA: 0x0007B8D8 File Offset: 0x00079AD8
		internal void RemoveKey(UniqueConstraint constraint)
		{
			ArrayList arrayList = new ArrayList();
			foreach (object obj in this.owner.DesignRelations)
			{
				DesignRelation designRelation = (DesignRelation)obj;
				DataRelation dataRelation = designRelation.DataRelation;
				if (dataRelation != null && dataRelation.ParentKeyConstraint == constraint)
				{
					arrayList.Add(designRelation);
				}
			}
			foreach (object obj2 in arrayList)
			{
				DesignRelation rel = (DesignRelation)obj2;
				this.owner.DesignRelations.Remove(rel);
			}
			this.RemoveConstraint(constraint);
		}

		// Token: 0x06001654 RID: 5716 RVA: 0x0007B9B0 File Offset: 0x00079BB0
		internal void SetTypeForUndo(TableType newType)
		{
			this.tableType = newType;
		}

		// Token: 0x06001655 RID: 5717 RVA: 0x0007B9BC File Offset: 0x00079BBC
		void IDataSourceXmlSpecialOwner.WriteSpecialItem(string propertyName, XmlWriter writer, DataSourceXmlSerializer serializer)
		{
			if (propertyName == "Mappings")
			{
				foreach (object obj in this.Mappings)
				{
					DataColumnMapping dataColumnMapping = (DataColumnMapping)obj;
					writer.WriteStartElement(string.Empty, "Mapping", "urn:schemas-microsoft-com:xml-msdatasource");
					writer.WriteAttributeString("SourceColumn", dataColumnMapping.SourceColumn);
					writer.WriteAttributeString("DataSetColumn", dataColumnMapping.DataSetColumn);
					writer.WriteEndElement();
				}
			}
		}

		// Token: 0x06001656 RID: 5718 RVA: 0x0007BA58 File Offset: 0x00079C58
		void IDataSourceXmlSerializable.WriteXml(XmlWriter xmlWriter, DataSourceXmlSerializer serializer)
		{
			TableType tableType = this.TableType;
			if (tableType != TableType.DataTable && tableType == TableType.RadTable)
			{
				xmlWriter.WriteStartElement(string.Empty, "TableAdapter", "urn:schemas-microsoft-com:xml-msdatasource");
				serializer.SerializeBody(xmlWriter, this);
				xmlWriter.WriteFullEndElement();
			}
		}

		// Token: 0x06001657 RID: 5719 RVA: 0x00003937 File Offset: 0x00001B37
		internal void UpdateColumnMappingDataSetColumnName(string oldName, string newName)
		{
		}

		// Token: 0x06001658 RID: 5720 RVA: 0x00003937 File Offset: 0x00001B37
		internal void UpdateColumnMappingSourceColumnName(string dataSetColumn, string newSourceColumn)
		{
		}

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x06001659 RID: 5721 RVA: 0x0007BA96 File Offset: 0x00079C96
		// (set) Token: 0x0600165A RID: 5722 RVA: 0x0007BAB2 File Offset: 0x00079CB2
		internal string UserTableName
		{
			get
			{
				return this.dataTable.ExtendedProperties[DesignTable.EXTPROPNAME_USER_TABLENAME] as string;
			}
			set
			{
				this.dataTable.ExtendedProperties[DesignTable.EXTPROPNAME_USER_TABLENAME] = value;
			}
		}

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x0600165B RID: 5723 RVA: 0x0007BACA File Offset: 0x00079CCA
		// (set) Token: 0x0600165C RID: 5724 RVA: 0x0007BAD2 File Offset: 0x00079CD2
		internal string GeneratorRunFillName
		{
			get
			{
				return this.generatorRunFillName;
			}
			set
			{
				this.generatorRunFillName = value;
			}
		}

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x0600165D RID: 5725 RVA: 0x0007BADB File Offset: 0x00079CDB
		// (set) Token: 0x0600165E RID: 5726 RVA: 0x0007BAF7 File Offset: 0x00079CF7
		internal string GeneratorTablePropName
		{
			get
			{
				return this.dataTable.ExtendedProperties[DesignTable.EXTPROPNAME_GENERATOR_TABLEPROPNAME] as string;
			}
			set
			{
				this.dataTable.ExtendedProperties[DesignTable.EXTPROPNAME_GENERATOR_TABLEPROPNAME] = value;
			}
		}

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x0600165F RID: 5727 RVA: 0x0007BB0F File Offset: 0x00079D0F
		// (set) Token: 0x06001660 RID: 5728 RVA: 0x0007BB2B File Offset: 0x00079D2B
		internal string GeneratorTableVarName
		{
			get
			{
				return this.dataTable.ExtendedProperties[DesignTable.EXTPROPNAME_GENERATOR_TABLEVARNAME] as string;
			}
			set
			{
				this.dataTable.ExtendedProperties[DesignTable.EXTPROPNAME_GENERATOR_TABLEVARNAME] = value;
			}
		}

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x06001661 RID: 5729 RVA: 0x0007BB43 File Offset: 0x00079D43
		// (set) Token: 0x06001662 RID: 5730 RVA: 0x0007BB5F File Offset: 0x00079D5F
		internal string GeneratorTableClassName
		{
			get
			{
				return this.dataTable.ExtendedProperties[DesignTable.EXTPROPNAME_GENERATOR_TABLECLASSNAME] as string;
			}
			set
			{
				this.dataTable.ExtendedProperties[DesignTable.EXTPROPNAME_GENERATOR_TABLECLASSNAME] = value;
			}
		}

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x06001663 RID: 5731 RVA: 0x0007BB77 File Offset: 0x00079D77
		// (set) Token: 0x06001664 RID: 5732 RVA: 0x0007BB93 File Offset: 0x00079D93
		internal string GeneratorRowClassName
		{
			get
			{
				return this.dataTable.ExtendedProperties[DesignTable.EXTPROPNAME_GENERATOR_ROWCLASSNAME] as string;
			}
			set
			{
				this.dataTable.ExtendedProperties[DesignTable.EXTPROPNAME_GENERATOR_ROWCLASSNAME] = value;
			}
		}

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x06001665 RID: 5733 RVA: 0x0007BBAB File Offset: 0x00079DAB
		// (set) Token: 0x06001666 RID: 5734 RVA: 0x0007BBC7 File Offset: 0x00079DC7
		internal string GeneratorRowEvHandlerName
		{
			get
			{
				return this.dataTable.ExtendedProperties[DesignTable.EXTPROPNAME_GENERATOR_ROWEVHANDLERNAME] as string;
			}
			set
			{
				this.dataTable.ExtendedProperties[DesignTable.EXTPROPNAME_GENERATOR_ROWEVHANDLERNAME] = value;
			}
		}

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x06001667 RID: 5735 RVA: 0x0007BBDF File Offset: 0x00079DDF
		// (set) Token: 0x06001668 RID: 5736 RVA: 0x0007BBFB File Offset: 0x00079DFB
		internal string GeneratorRowEvArgName
		{
			get
			{
				return this.dataTable.ExtendedProperties[DesignTable.EXTPROPNAME_GENERATOR_ROWEVARGNAME] as string;
			}
			set
			{
				this.dataTable.ExtendedProperties[DesignTable.EXTPROPNAME_GENERATOR_ROWEVARGNAME] = value;
			}
		}

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x06001669 RID: 5737 RVA: 0x0007BC13 File Offset: 0x00079E13
		// (set) Token: 0x0600166A RID: 5738 RVA: 0x0007BC2F File Offset: 0x00079E2F
		internal string GeneratorRowChangingName
		{
			get
			{
				return this.dataTable.ExtendedProperties[DesignTable.EXTPROPNAME_GENERATOR_ROWCHANGINGNAME] as string;
			}
			set
			{
				this.dataTable.ExtendedProperties[DesignTable.EXTPROPNAME_GENERATOR_ROWCHANGINGNAME] = value;
			}
		}

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x0600166B RID: 5739 RVA: 0x0007BC47 File Offset: 0x00079E47
		// (set) Token: 0x0600166C RID: 5740 RVA: 0x0007BC63 File Offset: 0x00079E63
		internal string GeneratorRowChangedName
		{
			get
			{
				return this.dataTable.ExtendedProperties[DesignTable.EXTPROPNAME_GENERATOR_ROWCHANGEDNAME] as string;
			}
			set
			{
				this.dataTable.ExtendedProperties[DesignTable.EXTPROPNAME_GENERATOR_ROWCHANGEDNAME] = value;
			}
		}

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x0600166D RID: 5741 RVA: 0x0007BC7B File Offset: 0x00079E7B
		// (set) Token: 0x0600166E RID: 5742 RVA: 0x0007BC97 File Offset: 0x00079E97
		internal string GeneratorRowDeletingName
		{
			get
			{
				return this.dataTable.ExtendedProperties[DesignTable.EXTPROPNAME_GENERATOR_ROWDELETINGNAME] as string;
			}
			set
			{
				this.dataTable.ExtendedProperties[DesignTable.EXTPROPNAME_GENERATOR_ROWDELETINGNAME] = value;
			}
		}

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x0600166F RID: 5743 RVA: 0x0007BCAF File Offset: 0x00079EAF
		// (set) Token: 0x06001670 RID: 5744 RVA: 0x0007BCCB File Offset: 0x00079ECB
		internal string GeneratorRowDeletedName
		{
			get
			{
				return this.dataTable.ExtendedProperties[DesignTable.EXTPROPNAME_GENERATOR_ROWDELETEDNAME] as string;
			}
			set
			{
				this.dataTable.ExtendedProperties[DesignTable.EXTPROPNAME_GENERATOR_ROWDELETEDNAME] = value;
			}
		}

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x06001671 RID: 5745 RVA: 0x0007BCE3 File Offset: 0x00079EE3
		internal override StringCollection NamingPropertyNames
		{
			get
			{
				return this.namingPropNames;
			}
		}

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x06001672 RID: 5746 RVA: 0x0007BCEB File Offset: 0x00079EEB
		// (set) Token: 0x06001673 RID: 5747 RVA: 0x0007BCF3 File Offset: 0x00079EF3
		[DataSourceXmlAttribute]
		[Browsable(false)]
		[DefaultValue(null)]
		public string GeneratorDataComponentClassName
		{
			get
			{
				return this.generatorDataComponentClassName;
			}
			set
			{
				this.generatorDataComponentClassName = value;
			}
		}

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x06001674 RID: 5748 RVA: 0x0007BCFC File Offset: 0x00079EFC
		// (set) Token: 0x06001675 RID: 5749 RVA: 0x0007BD04 File Offset: 0x00079F04
		[DataSourceXmlAttribute]
		[Browsable(false)]
		[DefaultValue(null)]
		public string UserDataComponentName
		{
			get
			{
				return this.userDataComponentName;
			}
			set
			{
				this.userDataComponentName = value;
			}
		}

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x06001676 RID: 5750 RVA: 0x0007BD0D File Offset: 0x00079F0D
		[Browsable(false)]
		public override string GeneratorName
		{
			get
			{
				return this.GeneratorTablePropName;
			}
		}

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x06001677 RID: 5751 RVA: 0x0007BD15 File Offset: 0x00079F15
		// (set) Token: 0x06001678 RID: 5752 RVA: 0x0007BD1D File Offset: 0x00079F1D
		internal DesignTable.CodeGenPropertyCache PropertyCache
		{
			get
			{
				return this.codeGenPropertyCache;
			}
			set
			{
				this.codeGenPropertyCache = value;
			}
		}

		// Token: 0x04000B5D RID: 2909
		private TableType tableType;

		// Token: 0x04000B5E RID: 2910
		private DataTable dataTable;

		// Token: 0x04000B5F RID: 2911
		private DataAccessor dataAccessor;

		// Token: 0x04000B60 RID: 2912
		private DesignColumnCollection designColumns;

		// Token: 0x04000B61 RID: 2913
		private DesignDataSource owner;

		// Token: 0x04000B62 RID: 2914
		private TypeAttributes dataAccessorModifier = TypeAttributes.Public;

		// Token: 0x04000B63 RID: 2915
		private Source mainSource;

		// Token: 0x04000B64 RID: 2916
		private SourceCollection sources;

		// Token: 0x04000B65 RID: 2917
		private DataColumnMappingCollection mappings;

		// Token: 0x04000B66 RID: 2918
		private bool webServiceAttribute;

		// Token: 0x04000B67 RID: 2919
		private string webServiceNamespace;

		// Token: 0x04000B68 RID: 2920
		private string webServiceDescription;

		// Token: 0x04000B69 RID: 2921
		private string provider;

		// Token: 0x04000B6A RID: 2922
		private string generatorRunFillName;

		// Token: 0x04000B6B RID: 2923
		private string baseClass;

		// Token: 0x04000B6C RID: 2924
		private string dataAccessorName;

		// Token: 0x04000B6F RID: 2927
		private bool inAccessConstraints;

		// Token: 0x04000B72 RID: 2930
		private const string DATATABLE_NAMEROOT = "DataTable";

		// Token: 0x04000B73 RID: 2931
		private const string RADTABLE_NAMEROOT = "DataTable";

		// Token: 0x04000B74 RID: 2932
		private const string KEY_NAMEROOT = "Key";

		// Token: 0x04000B75 RID: 2933
		private const string PRIMARYKEY_PROPERTY = "PrimaryKey";

		// Token: 0x04000B76 RID: 2934
		internal const string MAINSOURCE_PROPERTY = "MainSource";

		// Token: 0x04000B77 RID: 2935
		private const string MAINSOURCE_NAME = "Fill";

		// Token: 0x04000B78 RID: 2936
		internal const string NAME_PROPERTY = "Name";

		// Token: 0x04000B79 RID: 2937
		private string generatorDataComponentClassName;

		// Token: 0x04000B7A RID: 2938
		private string userDataComponentName;

		// Token: 0x04000B7B RID: 2939
		private DesignTable.CodeGenPropertyCache codeGenPropertyCache;

		// Token: 0x04000B7C RID: 2940
		private StringCollection namingPropNames = new StringCollection();

		// Token: 0x04000B7D RID: 2941
		internal static string EXTPROPNAME_USER_TABLENAME = "Generator_UserTableName";

		// Token: 0x04000B7E RID: 2942
		internal static string EXTPROPNAME_GENERATOR_TABLEPROPNAME = "Generator_TablePropName";

		// Token: 0x04000B7F RID: 2943
		internal static string EXTPROPNAME_GENERATOR_TABLEVARNAME = "Generator_TableVarName";

		// Token: 0x04000B80 RID: 2944
		internal static string EXTPROPNAME_GENERATOR_TABLECLASSNAME = "Generator_TableClassName";

		// Token: 0x04000B81 RID: 2945
		internal static string EXTPROPNAME_GENERATOR_ROWCLASSNAME = "Generator_RowClassName";

		// Token: 0x04000B82 RID: 2946
		internal static string EXTPROPNAME_GENERATOR_ROWEVHANDLERNAME = "Generator_RowEvHandlerName";

		// Token: 0x04000B83 RID: 2947
		internal static string EXTPROPNAME_GENERATOR_ROWEVARGNAME = "Generator_RowEvArgName";

		// Token: 0x04000B84 RID: 2948
		internal static string EXTPROPNAME_GENERATOR_ROWCHANGINGNAME = "Generator_RowChangingName";

		// Token: 0x04000B85 RID: 2949
		internal static string EXTPROPNAME_GENERATOR_ROWCHANGEDNAME = "Generator_RowChangedName";

		// Token: 0x04000B86 RID: 2950
		internal static string EXTPROPNAME_GENERATOR_ROWDELETINGNAME = "Generator_RowDeletingName";

		// Token: 0x04000B87 RID: 2951
		internal static string EXTPROPNAME_GENERATOR_ROWDELETEDNAME = "Generator_RowDeletedName";

		// Token: 0x020004BE RID: 1214
		internal class CodeGenPropertyCache
		{
			// Token: 0x17000952 RID: 2386
			// (get) Token: 0x06002C30 RID: 11312 RVA: 0x0010701C File Offset: 0x0010521C
			internal Type AdapterType
			{
				get
				{
					if (this.adapterType == null)
					{
						if (this.designTable == null || this.designTable.Connection == null || this.designTable.Connection.Provider == null)
						{
							return null;
						}
						DbProviderFactory factory = ProviderManager.GetFactory(this.designTable.Connection.Provider);
						if (factory != null)
						{
							DataAdapter dataAdapter = factory.CreateDataAdapter();
							if (dataAdapter != null)
							{
								this.adapterType = dataAdapter.GetType();
							}
						}
					}
					return this.adapterType;
				}
			}

			// Token: 0x17000953 RID: 2387
			// (get) Token: 0x06002C31 RID: 11313 RVA: 0x00107098 File Offset: 0x00105298
			internal Type ConnectionType
			{
				get
				{
					if (this.connectionType == null && this.designTable != null && this.designTable.Connection != null)
					{
						IDbConnection dbConnection = this.designTable.Connection.CreateEmptyDbConnection();
						if (dbConnection != null)
						{
							this.connectionType = dbConnection.GetType();
						}
					}
					return this.connectionType;
				}
			}

			// Token: 0x17000954 RID: 2388
			// (get) Token: 0x06002C32 RID: 11314 RVA: 0x001070F0 File Offset: 0x001052F0
			internal Type TransactionType
			{
				get
				{
					if (this.transactionType == null)
					{
						if (this.designTable == null || this.designTable.Connection == null || this.designTable.Connection.Provider == null)
						{
							return null;
						}
						DbProviderFactory factory = ProviderManager.GetFactory(this.designTable.Connection.Provider);
						if (factory != null)
						{
							Type type = factory.CreateCommand().GetType();
							foreach (object obj in TypeDescriptor.GetProperties(type))
							{
								PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
								if (StringUtil.EqualValue(propertyDescriptor.Name, "Transaction"))
								{
									this.transactionType = propertyDescriptor.PropertyType;
									break;
								}
							}
						}
						if (this.transactionType == null)
						{
							this.transactionType = typeof(IDbTransaction);
						}
					}
					return this.transactionType;
				}
			}

			// Token: 0x17000955 RID: 2389
			// (get) Token: 0x06002C33 RID: 11315 RVA: 0x001071E8 File Offset: 0x001053E8
			// (set) Token: 0x06002C34 RID: 11316 RVA: 0x001071F0 File Offset: 0x001053F0
			internal string TAMAdapterPropName
			{
				get
				{
					return this.tamAdapterPropName;
				}
				set
				{
					this.tamAdapterPropName = value;
				}
			}

			// Token: 0x17000956 RID: 2390
			// (get) Token: 0x06002C35 RID: 11317 RVA: 0x001071F9 File Offset: 0x001053F9
			// (set) Token: 0x06002C36 RID: 11318 RVA: 0x00107201 File Offset: 0x00105401
			internal string TAMAdapterVarName
			{
				get
				{
					return this.tamAdapterVarName;
				}
				set
				{
					this.tamAdapterVarName = value;
				}
			}

			// Token: 0x06002C37 RID: 11319 RVA: 0x0010720A File Offset: 0x0010540A
			internal CodeGenPropertyCache(DesignTable designTable)
			{
				this.designTable = designTable;
			}

			// Token: 0x04001EA1 RID: 7841
			private DesignTable designTable;

			// Token: 0x04001EA2 RID: 7842
			private Type connectionType;

			// Token: 0x04001EA3 RID: 7843
			private Type transactionType;

			// Token: 0x04001EA4 RID: 7844
			private Type adapterType;

			// Token: 0x04001EA5 RID: 7845
			private string tamAdapterPropName;

			// Token: 0x04001EA6 RID: 7846
			private string tamAdapterVarName;
		}
	}
}
