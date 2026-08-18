using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data.Common;
using System.Design;
using System.Xml;

namespace System.Data.Design
{
	// Token: 0x02000237 RID: 567
	[DataSourceXmlClass("Connection")]
	internal class DesignConnection : DataSourceComponent, IDesignConnection, IDataSourceNamedObject, INamedObject, ICloneable, IDataSourceInitAfterLoading, IDataSourceXmlSpecialOwner, IDataSourceCollectionMember
	{
		// Token: 0x06001547 RID: 5447 RVA: 0x00078640 File Offset: 0x00076840
		public DesignConnection()
		{
		}

		// Token: 0x06001548 RID: 5448 RVA: 0x0007865E File Offset: 0x0007685E
		public DesignConnection(string connectionName, ConnectionString cs, string provider)
		{
			this.name = connectionName;
			this.connectionStringObject = cs;
			this.provider = provider;
		}

		// Token: 0x06001549 RID: 5449 RVA: 0x00078694 File Offset: 0x00076894
		public DesignConnection(string connectionName, IDbConnection conn)
		{
			if (conn == null)
			{
				throw new ArgumentNullException("conn");
			}
			this.name = connectionName;
			DbProviderFactory factoryFromType = ProviderManager.GetFactoryFromType(conn.GetType(), ProviderManager.ProviderSupportedClasses.DbConnection);
			this.provider = ProviderManager.GetInvariantProviderName(factoryFromType);
			this.connectionStringObject = new ConnectionString(this.provider, conn.ConnectionString);
		}

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x0600154A RID: 5450 RVA: 0x00078702 File Offset: 0x00076902
		internal static string ConnectionNameRegex
		{
			get
			{
				return DesignConnection.regexIdentifier;
			}
		}

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x0600154B RID: 5451 RVA: 0x00078709 File Offset: 0x00076909
		// (set) Token: 0x0600154C RID: 5452 RVA: 0x00078711 File Offset: 0x00076911
		[DefaultValue(MemberAttributes.Assembly)]
		[DataSourceXmlAttribute]
		public MemberAttributes Modifier
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

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x0600154D RID: 5453 RVA: 0x0007871A File Offset: 0x0007691A
		// (set) Token: 0x0600154E RID: 5454 RVA: 0x00078722 File Offset: 0x00076922
		[DataSourceXmlAttribute]
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				if (this.name != value)
				{
					if (this.CollectionParent != null)
					{
						this.CollectionParent.ValidateUniqueName(this, value);
					}
					this.name = value;
				}
			}
		}

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x0600154F RID: 5455 RVA: 0x0007874E File Offset: 0x0007694E
		// (set) Token: 0x06001550 RID: 5456 RVA: 0x00078758 File Offset: 0x00076958
		[DataSourceXmlAttribute(SpecialWay = true)]
		[Browsable(false)]
		public ConnectionString ConnectionStringObject
		{
			get
			{
				return this.connectionStringObject;
			}
			set
			{
				ConnectionString connectionString = this.connectionStringObject;
				this.connectionStringObject = value;
			}
		}

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x06001551 RID: 5457 RVA: 0x00078773 File Offset: 0x00076973
		// (set) Token: 0x06001552 RID: 5458 RVA: 0x0007878E File Offset: 0x0007698E
		public string ConnectionString
		{
			get
			{
				if (this.ConnectionStringObject != null)
				{
					return this.ConnectionStringObject.ToString();
				}
				return string.Empty;
			}
			set
			{
				if (this.ConnectionStringObject != null)
				{
					this.ConnectionStringObject = new ConnectionString(this.provider, value);
				}
			}
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x06001553 RID: 5459 RVA: 0x000787AA File Offset: 0x000769AA
		// (set) Token: 0x06001554 RID: 5460 RVA: 0x000787B2 File Offset: 0x000769B2
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

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x06001555 RID: 5461 RVA: 0x000787BB File Offset: 0x000769BB
		// (set) Token: 0x06001556 RID: 5462 RVA: 0x000787C3 File Offset: 0x000769C3
		[DataSourceXmlAttribute]
		[Browsable(false)]
		public bool IsAppSettingsProperty
		{
			get
			{
				return this.isAppSettingsProperty;
			}
			set
			{
				this.isAppSettingsProperty = value;
			}
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x06001557 RID: 5463 RVA: 0x000787CC File Offset: 0x000769CC
		// (set) Token: 0x06001558 RID: 5464 RVA: 0x000787D4 File Offset: 0x000769D4
		[DataSourceXmlAttribute]
		[Browsable(false)]
		public string AppSettingsObjectName
		{
			get
			{
				return this.appSettingsObjectName;
			}
			set
			{
				this.appSettingsObjectName = value;
			}
		}

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x06001559 RID: 5465 RVA: 0x000787DD File Offset: 0x000769DD
		// (set) Token: 0x0600155A RID: 5466 RVA: 0x000787E5 File Offset: 0x000769E5
		[DataSourceXmlAttribute(SpecialWay = true)]
		[Browsable(false)]
		public CodePropertyReferenceExpression PropertyReference
		{
			get
			{
				return this.propertyReference;
			}
			set
			{
				this.propertyReference = value;
			}
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x0600155B RID: 5467 RVA: 0x000787EE File Offset: 0x000769EE
		// (set) Token: 0x0600155C RID: 5468 RVA: 0x000787F6 File Offset: 0x000769F6
		[DataSourceXmlAttribute]
		[Browsable(false)]
		public string ParameterPrefix
		{
			get
			{
				return this.parameterPrefix;
			}
			set
			{
				this.parameterPrefix = value;
			}
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x0600155D RID: 5469 RVA: 0x000787FF File Offset: 0x000769FF
		[Browsable(false)]
		public IDictionary Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x0600155E RID: 5470 RVA: 0x00078807 File Offset: 0x00076A07
		[Browsable(false)]
		public string PublicTypeName
		{
			get
			{
				return "Connection";
			}
		}

		// Token: 0x0600155F RID: 5471 RVA: 0x00078810 File Offset: 0x00076A10
		public IDbConnection CreateEmptyDbConnection()
		{
			DbProviderFactory factory = ProviderManager.GetFactory(this.provider);
			return factory.CreateConnection();
		}

		// Token: 0x06001560 RID: 5472 RVA: 0x00078830 File Offset: 0x00076A30
		public object Clone()
		{
			DesignConnection designConnection = new DesignConnection();
			designConnection.Name = this.name;
			if (this.ConnectionStringObject != null)
			{
				designConnection.ConnectionStringObject = (ConnectionString)((ICloneable)this.ConnectionStringObject).Clone();
			}
			designConnection.provider = this.provider;
			designConnection.isAppSettingsProperty = this.isAppSettingsProperty;
			designConnection.propertyReference = this.propertyReference;
			designConnection.properties = (HybridDictionary)DesignUtil.CloneDictionary(this.properties);
			return designConnection;
		}

		// Token: 0x06001561 RID: 5473 RVA: 0x000788B0 File Offset: 0x00076AB0
		void IDataSourceInitAfterLoading.InitializeAfterLoading()
		{
			if (this.name == null || this.name.Length == 0)
			{
				throw new DataSourceSerializationException(SR.GetString("DTDS_NameIsRequired", new object[]
				{
					"Connection"
				}));
			}
			if (StringUtil.EmptyOrSpace(this.provider))
			{
				throw new DataSourceSerializationException(SR.GetString("DTDS_CouldNotDeserializeConnection"));
			}
			if (this.connectionStringValue != null)
			{
				this.ConnectionStringObject = new ConnectionString(this.provider, this.connectionStringValue);
			}
			this.properties.Clear();
		}

		// Token: 0x06001562 RID: 5474 RVA: 0x00078937 File Offset: 0x00076B37
		void IDataSourceXmlSpecialOwner.ReadSpecialItem(string propertyName, XmlNode xmlNode, DataSourceXmlSerializer serializer)
		{
			if (propertyName == "ConnectionStringObject")
			{
				this.connectionStringValue = xmlNode.InnerText;
				return;
			}
			if (propertyName == "PropertyReference")
			{
				this.propertyReference = PropertyReferenceSerializer.Deserialize(xmlNode.InnerText);
			}
		}

		// Token: 0x06001563 RID: 5475 RVA: 0x00078971 File Offset: 0x00076B71
		void IDataSourceXmlSpecialOwner.WriteSpecialItem(string propertyName, XmlWriter writer, DataSourceXmlSerializer serializer)
		{
			if (propertyName == "ConnectionStringObject")
			{
				writer.WriteString(this.ConnectionStringObject.ToFullString());
				return;
			}
			if (propertyName == "PropertyReference")
			{
				writer.WriteString(PropertyReferenceSerializer.Serialize(this.PropertyReference));
			}
		}

		// Token: 0x04000B22 RID: 2850
		private string name;

		// Token: 0x04000B23 RID: 2851
		private ConnectionString connectionStringObject;

		// Token: 0x04000B24 RID: 2852
		private string connectionStringValue;

		// Token: 0x04000B25 RID: 2853
		private string provider;

		// Token: 0x04000B26 RID: 2854
		private bool isAppSettingsProperty;

		// Token: 0x04000B27 RID: 2855
		private string appSettingsObjectName;

		// Token: 0x04000B28 RID: 2856
		private CodePropertyReferenceExpression propertyReference;

		// Token: 0x04000B29 RID: 2857
		private HybridDictionary properties = new HybridDictionary();

		// Token: 0x04000B2A RID: 2858
		private MemberAttributes modifier = MemberAttributes.Assembly;

		// Token: 0x04000B2B RID: 2859
		private static readonly string regexAlphaCharacter = "[\\p{L}\\p{Nl}]";

		// Token: 0x04000B2C RID: 2860
		private static readonly string regexUnderscoreCharacter = "\\p{Pc}";

		// Token: 0x04000B2D RID: 2861
		private static readonly string regexIdentifierCharacter = "[\\p{L}\\p{Nl}\\p{Nd}\\p{Mn}\\p{Mc}\\p{Cf}]";

		// Token: 0x04000B2E RID: 2862
		private static readonly string regexIdentifierStart = string.Concat(new string[]
		{
			"(",
			DesignConnection.regexAlphaCharacter,
			"|(",
			DesignConnection.regexUnderscoreCharacter,
			DesignConnection.regexIdentifierCharacter,
			"))"
		});

		// Token: 0x04000B2F RID: 2863
		private static readonly string regexIdentifier = DesignConnection.regexIdentifierStart + DesignConnection.regexIdentifierCharacter + "*";

		// Token: 0x04000B30 RID: 2864
		private string parameterPrefix;
	}
}
