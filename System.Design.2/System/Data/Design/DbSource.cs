using System;
using System.CodeDom;
using System.ComponentModel;
using System.Xml;

namespace System.Data.Design
{
	// Token: 0x02000231 RID: 561
	[DataSourceXmlClass("DbSource")]
	internal class DbSource : Source, IDataSourceXmlSpecialOwner
	{
		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x060014BB RID: 5307 RVA: 0x000775C4 File Offset: 0x000757C4
		protected internal override DataSourceCollectionBase CollectionParent
		{
			get
			{
				if (base.CollectionParent != null)
				{
					return base.CollectionParent;
				}
				if (this.owner != null && this.owner is DesignTable && ((DesignTable)this.owner).MainSource == this)
				{
					return ((DesignTable)this.owner).Sources;
				}
				return null;
			}
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x060014BC RID: 5308 RVA: 0x0007761A File Offset: 0x0007581A
		// (set) Token: 0x060014BD RID: 5309 RVA: 0x00077636 File Offset: 0x00075836
		[Browsable(false)]
		[DataSourceXmlAttribute]
		public string ConnectionRef
		{
			get
			{
				if (this.connection != null)
				{
					return this.connection.Name;
				}
				return this.connectionRef;
			}
			set
			{
				this.connectionRef = value;
			}
		}

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x060014BE RID: 5310 RVA: 0x0007763F File Offset: 0x0007583F
		[Browsable(false)]
		[DataSourceXmlAttribute(SpecialWay = true)]
		public Type ScalarCallRetval
		{
			get
			{
				return this.scalarCallRetval;
			}
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x060014BF RID: 5311 RVA: 0x00077647 File Offset: 0x00075847
		// (set) Token: 0x060014C0 RID: 5312 RVA: 0x0007764F File Offset: 0x0007584F
		[DefaultValue(null)]
		[RefreshProperties(RefreshProperties.All)]
		public IDesignConnection Connection
		{
			get
			{
				return this.connection;
			}
			set
			{
				this.connection = value;
			}
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x060014C1 RID: 5313 RVA: 0x00077658 File Offset: 0x00075858
		// (set) Token: 0x060014C2 RID: 5314 RVA: 0x00077660 File Offset: 0x00075860
		[DefaultValue(TypeEnum.CLR)]
		[DataSourceXmlAttribute]
		public TypeEnum MethodsParameterType
		{
			get
			{
				return this.parameterType;
			}
			set
			{
				this.parameterType = value;
			}
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x060014C3 RID: 5315 RVA: 0x00077669 File Offset: 0x00075869
		public CommandOperation CommandOperation
		{
			get
			{
				if (this.SelectCommand != null)
				{
					return CommandOperation.Select;
				}
				if (this.InsertCommand != null)
				{
					return CommandOperation.Insert;
				}
				if (this.UpdateCommand != null)
				{
					return CommandOperation.Update;
				}
				if (this.DeleteCommand != null)
				{
					return CommandOperation.Delete;
				}
				return CommandOperation.Unknown;
			}
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x060014C4 RID: 5316 RVA: 0x00077694 File Offset: 0x00075894
		// (set) Token: 0x060014C5 RID: 5317 RVA: 0x0007769C File Offset: 0x0007589C
		[DefaultValue(MemberAttributes.Public)]
		[DataSourceXmlAttribute]
		public MemberAttributes FillMethodModifier
		{
			get
			{
				return base.Modifier;
			}
			set
			{
				base.Modifier = value;
			}
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x060014C6 RID: 5318 RVA: 0x000776A5 File Offset: 0x000758A5
		// (set) Token: 0x060014C7 RID: 5319 RVA: 0x000776AD File Offset: 0x000758AD
		[DefaultValue(MemberAttributes.Public)]
		[DataSourceXmlAttribute]
		public MemberAttributes GetMethodModifier
		{
			get
			{
				return this.getMethodModifier;
			}
			set
			{
				this.getMethodModifier = value;
			}
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x060014C8 RID: 5320 RVA: 0x000776B6 File Offset: 0x000758B6
		// (set) Token: 0x060014C9 RID: 5321 RVA: 0x000776DC File Offset: 0x000758DC
		[Browsable(false)]
		public override string Name
		{
			get
			{
				if (StringUtil.Empty(base.Name) && this.generateMethods == GenerateMethodTypes.Get)
				{
					return this.GetMethodName;
				}
				return base.Name;
			}
			set
			{
				if (this.name != value)
				{
					this.name = value;
					SourceCollection sourceCollection = this.CollectionParent as SourceCollection;
					if (sourceCollection != null)
					{
						sourceCollection.ValidateUniqueDbSourceName(this, value, true);
					}
				}
			}
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x060014CA RID: 5322 RVA: 0x00077716 File Offset: 0x00075916
		// (set) Token: 0x060014CB RID: 5323 RVA: 0x0007771E File Offset: 0x0007591E
		[DataSourceXmlAttribute]
		[DefaultValue("Fill")]
		public string FillMethodName
		{
			get
			{
				return this.Name;
			}
			set
			{
				this.Name = value;
			}
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x060014CC RID: 5324 RVA: 0x00077727 File Offset: 0x00075927
		// (set) Token: 0x060014CD RID: 5325 RVA: 0x00077764 File Offset: 0x00075964
		[DefaultValue("GetData")]
		[DataSourceXmlAttribute]
		public string GetMethodName
		{
			get
			{
				if (StringUtil.EmptyOrSpace(this.getMethodName) && this.CollectionParent != null)
				{
					if (base.IsMainSource)
					{
						this.GetMethodName = "GetData";
					}
					else
					{
						this.GetMethodName = "GetDataBy";
					}
				}
				return this.getMethodName;
			}
			set
			{
				this.getMethodName = value;
			}
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x060014CE RID: 5326 RVA: 0x0007776D File Offset: 0x0007596D
		// (set) Token: 0x060014CF RID: 5327 RVA: 0x00077775 File Offset: 0x00075975
		[DataSourceXmlAttribute]
		public string UserGetMethodName
		{
			get
			{
				return this.userGetMethodName;
			}
			set
			{
				this.userGetMethodName = value;
			}
		}

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x060014D0 RID: 5328 RVA: 0x0007777E File Offset: 0x0007597E
		// (set) Token: 0x060014D1 RID: 5329 RVA: 0x00077786 File Offset: 0x00075986
		[DefaultValue(GenerateMethodTypes.Both)]
		[DataSourceXmlAttribute]
		[RefreshProperties(RefreshProperties.All)]
		public GenerateMethodTypes GenerateMethods
		{
			get
			{
				return this.generateMethods;
			}
			set
			{
				this.generateMethods = value;
			}
		}

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x060014D2 RID: 5330 RVA: 0x0007778F File Offset: 0x0007598F
		// (set) Token: 0x060014D3 RID: 5331 RVA: 0x00077797 File Offset: 0x00075997
		[DefaultValue(true)]
		[DataSourceXmlAttribute]
		public bool GeneratePagingMethods
		{
			get
			{
				return this.generatePagingMethods;
			}
			set
			{
				this.generatePagingMethods = value;
			}
		}

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x060014D4 RID: 5332 RVA: 0x000777A0 File Offset: 0x000759A0
		[Browsable(false)]
		public override object Parent
		{
			get
			{
				if (base.Parent != null)
				{
					return base.Parent;
				}
				return base.Owner;
			}
		}

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x060014D5 RID: 5333 RVA: 0x000777B7 File Offset: 0x000759B7
		[Browsable(false)]
		public override string PublicTypeName
		{
			get
			{
				DesignTable designTable = base.Owner as DesignTable;
				return "Query";
			}
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x060014D6 RID: 5334 RVA: 0x000777CA File Offset: 0x000759CA
		// (set) Token: 0x060014D7 RID: 5335 RVA: 0x000777D2 File Offset: 0x000759D2
		[DataSourceXmlAttribute]
		[Browsable(false)]
		public QueryType QueryType
		{
			get
			{
				return this.queryType;
			}
			set
			{
				this.queryType = value;
				if (this.queryType != QueryType.Rowset)
				{
					this.GenerateMethods = GenerateMethodTypes.Fill;
				}
			}
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x060014D8 RID: 5336 RVA: 0x000777EA File Offset: 0x000759EA
		// (set) Token: 0x060014D9 RID: 5337 RVA: 0x000777F2 File Offset: 0x000759F2
		[DataSourceXmlSubItem(Name = "SelectCommand", ItemType = typeof(DbSourceCommand))]
		[Browsable(false)]
		public DbSourceCommand SelectCommand
		{
			get
			{
				return this.selectCommand;
			}
			set
			{
				if (this.selectCommand != null)
				{
					this.selectCommand.SetParent(null);
				}
				this.selectCommand = value;
				if (this.selectCommand != null)
				{
					this.selectCommand.SetParent(this);
					this.selectCommand.CommandOperation = CommandOperation.Select;
				}
			}
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x060014DA RID: 5338 RVA: 0x0007782F File Offset: 0x00075A2F
		// (set) Token: 0x060014DB RID: 5339 RVA: 0x00077837 File Offset: 0x00075A37
		[DataSourceXmlSubItem(Name = "UpdateCommand", ItemType = typeof(DbSourceCommand))]
		[Browsable(false)]
		public DbSourceCommand UpdateCommand
		{
			get
			{
				return this.updateCommand;
			}
			set
			{
				if (this.updateCommand != null)
				{
					this.updateCommand.SetParent(null);
				}
				this.updateCommand = value;
				if (this.updateCommand != null)
				{
					this.updateCommand.SetParent(this);
					this.updateCommand.CommandOperation = CommandOperation.Update;
				}
			}
		}

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x060014DC RID: 5340 RVA: 0x00077874 File Offset: 0x00075A74
		// (set) Token: 0x060014DD RID: 5341 RVA: 0x0007787C File Offset: 0x00075A7C
		[DataSourceXmlSubItem(Name = "DeleteCommand", ItemType = typeof(DbSourceCommand))]
		[Browsable(false)]
		public DbSourceCommand DeleteCommand
		{
			get
			{
				return this.deleteCommand;
			}
			set
			{
				if (this.deleteCommand != null)
				{
					this.deleteCommand.SetParent(null);
				}
				this.deleteCommand = value;
				if (this.deleteCommand != null)
				{
					this.deleteCommand.SetParent(this);
					this.deleteCommand.CommandOperation = CommandOperation.Delete;
				}
			}
		}

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x060014DE RID: 5342 RVA: 0x000778B9 File Offset: 0x00075AB9
		// (set) Token: 0x060014DF RID: 5343 RVA: 0x000778C1 File Offset: 0x00075AC1
		[DataSourceXmlSubItem(Name = "InsertCommand", ItemType = typeof(DbSourceCommand))]
		[Browsable(false)]
		public DbSourceCommand InsertCommand
		{
			get
			{
				return this.insertCommand;
			}
			set
			{
				if (this.insertCommand != null)
				{
					this.insertCommand.SetParent(null);
				}
				this.insertCommand = value;
				if (this.insertCommand != null)
				{
					this.insertCommand.SetParent(this);
					this.insertCommand.CommandOperation = CommandOperation.Insert;
				}
			}
		}

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x060014E0 RID: 5344 RVA: 0x000778FE File Offset: 0x00075AFE
		// (set) Token: 0x060014E1 RID: 5345 RVA: 0x00077906 File Offset: 0x00075B06
		[DataSourceXmlAttribute]
		public DbObjectType DbObjectType
		{
			get
			{
				return this.dbObjectType;
			}
			set
			{
				this.dbObjectType = value;
			}
		}

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x060014E2 RID: 5346 RVA: 0x0007790F File Offset: 0x00075B0F
		// (set) Token: 0x060014E3 RID: 5347 RVA: 0x00077917 File Offset: 0x00075B17
		[DataSourceXmlAttribute]
		public bool UseOptimisticConcurrency
		{
			get
			{
				return this.useOptimisticConcurrency;
			}
			set
			{
				this.useOptimisticConcurrency = value;
			}
		}

		// Token: 0x060014E4 RID: 5348 RVA: 0x00077920 File Offset: 0x00075B20
		internal override bool NameExist(string nameToCheck)
		{
			return StringUtil.EqualValue(this.FillMethodName, nameToCheck, true) || StringUtil.EqualValue(this.GetMethodName, nameToCheck, true);
		}

		// Token: 0x060014E5 RID: 5349 RVA: 0x00077940 File Offset: 0x00075B40
		public override object Clone()
		{
			DbSource dbSource = new DbSource();
			if (this.connection != null)
			{
				dbSource.connection = (DesignConnection)this.connection.Clone();
			}
			if (this.selectCommand != null)
			{
				dbSource.selectCommand = (DbSourceCommand)this.selectCommand.Clone();
				dbSource.selectCommand.SetParent(dbSource);
			}
			if (this.insertCommand != null)
			{
				dbSource.insertCommand = (DbSourceCommand)this.insertCommand.Clone();
				dbSource.insertCommand.SetParent(dbSource);
			}
			if (this.updateCommand != null)
			{
				dbSource.updateCommand = (DbSourceCommand)this.updateCommand.Clone();
				dbSource.updateCommand.SetParent(dbSource);
			}
			if (this.deleteCommand != null)
			{
				dbSource.deleteCommand = (DbSourceCommand)this.deleteCommand.Clone();
				dbSource.deleteCommand.SetParent(dbSource);
			}
			dbSource.Name = this.Name;
			dbSource.Modifier = base.Modifier;
			dbSource.scalarCallRetval = this.scalarCallRetval;
			dbSource.generateMethods = this.generateMethods;
			dbSource.queryType = this.queryType;
			dbSource.getMethodModifier = this.getMethodModifier;
			dbSource.getMethodName = this.getMethodName;
			dbSource.generatePagingMethods = this.generatePagingMethods;
			return dbSource;
		}

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x060014E6 RID: 5350 RVA: 0x00077A7A File Offset: 0x00075C7A
		// (set) Token: 0x060014E7 RID: 5351 RVA: 0x00077A82 File Offset: 0x00075C82
		[DataSourceXmlAttribute]
		public bool GenerateShortCommands
		{
			get
			{
				return this.generateShortCommands;
			}
			set
			{
				this.generateShortCommands = value;
			}
		}

		// Token: 0x060014E8 RID: 5352 RVA: 0x00077A8C File Offset: 0x00075C8C
		internal DbSourceCommand GetActiveCommand()
		{
			switch (this.CommandOperation)
			{
			case CommandOperation.Select:
				return this.SelectCommand;
			case CommandOperation.Insert:
				return this.InsertCommand;
			case CommandOperation.Update:
				return this.UpdateCommand;
			case CommandOperation.Delete:
				return this.DeleteCommand;
			default:
				return null;
			}
		}

		// Token: 0x060014E9 RID: 5353 RVA: 0x00077AD7 File Offset: 0x00075CD7
		void IDataSourceXmlSpecialOwner.ReadSpecialItem(string propertyName, XmlNode xmlNode, DataSourceXmlSerializer serializer)
		{
			if (propertyName.Equals("ScalarCallRetval"))
			{
				this.scalarCallRetval = typeof(object);
				if (StringUtil.NotEmptyAfterTrim(xmlNode.InnerText))
				{
					this.scalarCallRetval = Type.GetType(xmlNode.InnerText, false);
				}
			}
		}

		// Token: 0x060014EA RID: 5354 RVA: 0x00077B15 File Offset: 0x00075D15
		void IDataSourceXmlSpecialOwner.WriteSpecialItem(string propertyName, XmlWriter writer, DataSourceXmlSerializer serializer)
		{
			if (propertyName.Equals("ScalarCallRetval"))
			{
				writer.WriteString(this.scalarCallRetval.AssemblyQualifiedName);
			}
		}

		// Token: 0x04000AF6 RID: 2806
		private IDesignConnection connection;

		// Token: 0x04000AF7 RID: 2807
		private DbSourceCommand selectCommand;

		// Token: 0x04000AF8 RID: 2808
		private DbSourceCommand insertCommand;

		// Token: 0x04000AF9 RID: 2809
		private DbSourceCommand updateCommand;

		// Token: 0x04000AFA RID: 2810
		private DbSourceCommand deleteCommand;

		// Token: 0x04000AFB RID: 2811
		private DbObjectType dbObjectType;

		// Token: 0x04000AFC RID: 2812
		private string connectionRef;

		// Token: 0x04000AFD RID: 2813
		private Type scalarCallRetval = typeof(object);

		// Token: 0x04000AFE RID: 2814
		private string userGetMethodName;

		// Token: 0x04000AFF RID: 2815
		private string getMethodName;

		// Token: 0x04000B00 RID: 2816
		private MemberAttributes getMethodModifier = MemberAttributes.Public;

		// Token: 0x04000B01 RID: 2817
		private QueryType queryType;

		// Token: 0x04000B02 RID: 2818
		private GenerateMethodTypes generateMethods = GenerateMethodTypes.Both;

		// Token: 0x04000B03 RID: 2819
		private bool generatePagingMethods;

		// Token: 0x04000B04 RID: 2820
		private bool generateShortCommands = true;

		// Token: 0x04000B05 RID: 2821
		private bool useOptimisticConcurrency = true;

		// Token: 0x04000B06 RID: 2822
		private TypeEnum parameterType;

		// Token: 0x04000B07 RID: 2823
		internal const string TYPE_NAME_FOR_QUERY = "Query";

		// Token: 0x04000B08 RID: 2824
		internal const string TYPE_NAME_FOR_FUNCTION = "Query";

		// Token: 0x04000B09 RID: 2825
		private const string PROPERTY_COMMANDTEXT = "CommandText";

		// Token: 0x04000B0A RID: 2826
		internal const string INSTANCE_NAME_FOR_FILLMETHOD_MAIN = "Fill";

		// Token: 0x04000B0B RID: 2827
		internal const string INSTANCE_NAME_FOR_GETMETHOD_MAIN = "GetData";

		// Token: 0x04000B0C RID: 2828
		internal const string INSTANCE_NAME_FOR_FILLMETHOD = "FillBy";

		// Token: 0x04000B0D RID: 2829
		internal const string INSTANCE_NAME_FOR_GETMETHOD = "GetDataBy";

		// Token: 0x04000B0E RID: 2830
		internal const string INSTANCE_NAME_FOR_FUNCTION = "Query";
	}
}
