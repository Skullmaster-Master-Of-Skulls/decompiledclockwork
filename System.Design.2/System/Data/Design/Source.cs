using System;
using System.CodeDom;
using System.ComponentModel;

namespace System.Data.Design
{
	// Token: 0x02000264 RID: 612
	internal abstract class Source : DataSourceComponent, IDataSourceNamedObject, INamedObject, ICloneable
	{
		// Token: 0x06001774 RID: 6004 RVA: 0x00081858 File Offset: 0x0007FA58
		internal Source()
		{
			this.modifier = MemberAttributes.Public;
		}

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x06001775 RID: 6005 RVA: 0x0008186B File Offset: 0x0007FA6B
		// (set) Token: 0x06001776 RID: 6006 RVA: 0x00081873 File Offset: 0x0007FA73
		[DataSourceXmlAttribute]
		[DefaultValue(false)]
		public bool EnableWebMethods
		{
			get
			{
				return this.webMethod;
			}
			set
			{
				this.webMethod = value;
			}
		}

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x06001777 RID: 6007 RVA: 0x0008187C File Offset: 0x0007FA7C
		internal bool IsMainSource
		{
			get
			{
				DesignTable designTable = this.Owner as DesignTable;
				return designTable != null && designTable.MainSource == this;
			}
		}

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x06001778 RID: 6008 RVA: 0x000818A3 File Offset: 0x0007FAA3
		// (set) Token: 0x06001779 RID: 6009 RVA: 0x000818AB File Offset: 0x0007FAAB
		[DefaultValue(MemberAttributes.Public)]
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

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x0600177A RID: 6010 RVA: 0x000818B4 File Offset: 0x0007FAB4
		// (set) Token: 0x0600177B RID: 6011 RVA: 0x000818BC File Offset: 0x0007FABC
		[DefaultValue("")]
		[DataSourceXmlAttribute]
		[MergableProperty(false)]
		public virtual string Name
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

		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x0600177C RID: 6012 RVA: 0x00077716 File Offset: 0x00075916
		// (set) Token: 0x0600177D RID: 6013 RVA: 0x00003937 File Offset: 0x00001B37
		internal virtual string DisplayName
		{
			get
			{
				return this.Name;
			}
			set
			{
			}
		}

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x0600177E RID: 6014 RVA: 0x000818E8 File Offset: 0x0007FAE8
		// (set) Token: 0x0600177F RID: 6015 RVA: 0x00081926 File Offset: 0x0007FB26
		[Browsable(false)]
		internal DataSourceComponent Owner
		{
			get
			{
				if (this.owner == null && this.CollectionParent != null)
				{
					SourceCollection sourceCollection = this.CollectionParent as SourceCollection;
					if (sourceCollection != null)
					{
						this.owner = sourceCollection.CollectionHost;
					}
				}
				return this.owner;
			}
			set
			{
				this.owner = value;
			}
		}

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x06001780 RID: 6016 RVA: 0x0008192F File Offset: 0x0007FB2F
		[Browsable(false)]
		public virtual string PublicTypeName
		{
			get
			{
				return "Function";
			}
		}

		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x06001781 RID: 6017 RVA: 0x00081936 File Offset: 0x0007FB36
		// (set) Token: 0x06001782 RID: 6018 RVA: 0x0008193E File Offset: 0x0007FB3E
		[DataSourceXmlAttribute]
		[DefaultValue("")]
		public string WebMethodDescription
		{
			get
			{
				return this.webMethodDescription;
			}
			set
			{
				this.webMethodDescription = value;
			}
		}

		// Token: 0x06001783 RID: 6019
		public abstract object Clone();

		// Token: 0x06001784 RID: 6020 RVA: 0x00081947 File Offset: 0x0007FB47
		internal virtual bool NameExist(string nameToCheck)
		{
			return StringUtil.EqualValue(this.Name, nameToCheck, true);
		}

		// Token: 0x06001785 RID: 6021 RVA: 0x00081956 File Offset: 0x0007FB56
		public override void SetCollection(DataSourceCollectionBase collection)
		{
			base.SetCollection(collection);
			if (collection != null)
			{
				this.Owner = collection.CollectionHost;
				return;
			}
			this.Owner = null;
		}

		// Token: 0x06001786 RID: 6022 RVA: 0x00081976 File Offset: 0x0007FB76
		public override string ToString()
		{
			return this.PublicTypeName + " " + this.DisplayName;
		}

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x06001787 RID: 6023 RVA: 0x0008198E File Offset: 0x0007FB8E
		// (set) Token: 0x06001788 RID: 6024 RVA: 0x00081996 File Offset: 0x0007FB96
		[DataSourceXmlAttribute]
		[Browsable(false)]
		[DefaultValue(null)]
		public string UserSourceName
		{
			get
			{
				return this.userSourceName;
			}
			set
			{
				this.userSourceName = value;
			}
		}

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x06001789 RID: 6025 RVA: 0x0008199F File Offset: 0x0007FB9F
		// (set) Token: 0x0600178A RID: 6026 RVA: 0x000819A7 File Offset: 0x0007FBA7
		[DataSourceXmlAttribute]
		[Browsable(false)]
		[DefaultValue(null)]
		public string GeneratorSourceName
		{
			get
			{
				return this.generatorSourceName;
			}
			set
			{
				this.generatorSourceName = value;
			}
		}

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x0600178B RID: 6027 RVA: 0x000819B0 File Offset: 0x0007FBB0
		// (set) Token: 0x0600178C RID: 6028 RVA: 0x000819B8 File Offset: 0x0007FBB8
		[DataSourceXmlAttribute]
		[Browsable(false)]
		[DefaultValue(null)]
		public string GeneratorGetMethodName
		{
			get
			{
				return this.generatorGetMethodName;
			}
			set
			{
				this.generatorGetMethodName = value;
			}
		}

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x0600178D RID: 6029 RVA: 0x000819C1 File Offset: 0x0007FBC1
		// (set) Token: 0x0600178E RID: 6030 RVA: 0x000819C9 File Offset: 0x0007FBC9
		[DataSourceXmlAttribute]
		[Browsable(false)]
		[DefaultValue(null)]
		public string GeneratorSourceNameForPaging
		{
			get
			{
				return this.generatorSourceNameForPaging;
			}
			set
			{
				this.generatorSourceNameForPaging = value;
			}
		}

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x0600178F RID: 6031 RVA: 0x000819D2 File Offset: 0x0007FBD2
		// (set) Token: 0x06001790 RID: 6032 RVA: 0x000819DA File Offset: 0x0007FBDA
		[DataSourceXmlAttribute]
		[Browsable(false)]
		[DefaultValue(null)]
		public string GeneratorGetMethodNameForPaging
		{
			get
			{
				return this.generatorGetMethodNameForPaging;
			}
			set
			{
				this.generatorGetMethodNameForPaging = value;
			}
		}

		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x06001791 RID: 6033 RVA: 0x000819E3 File Offset: 0x0007FBE3
		[Browsable(false)]
		public override string GeneratorName
		{
			get
			{
				return this.GeneratorSourceName;
			}
		}

		// Token: 0x04000BFF RID: 3071
		protected string name;

		// Token: 0x04000C00 RID: 3072
		private MemberAttributes modifier;

		// Token: 0x04000C01 RID: 3073
		protected DataSourceComponent owner;

		// Token: 0x04000C02 RID: 3074
		private bool webMethod;

		// Token: 0x04000C03 RID: 3075
		private string webMethodDescription;

		// Token: 0x04000C04 RID: 3076
		private string userSourceName;

		// Token: 0x04000C05 RID: 3077
		private string generatorSourceName;

		// Token: 0x04000C06 RID: 3078
		private string generatorGetMethodName;

		// Token: 0x04000C07 RID: 3079
		private string generatorSourceNameForPaging;

		// Token: 0x04000C08 RID: 3080
		private string generatorGetMethodNameForPaging;
	}
}
