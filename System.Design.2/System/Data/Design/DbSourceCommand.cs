using System;
using System.ComponentModel;
using System.Design;

namespace System.Data.Design
{
	// Token: 0x02000232 RID: 562
	[DataSourceXmlClass("DbCommand")]
	[DefaultProperty("CommandText")]
	internal class DbSourceCommand : DataSourceComponent, ICloneable, INamedObject
	{
		// Token: 0x060014EB RID: 5355 RVA: 0x00077B35 File Offset: 0x00075D35
		public DbSourceCommand()
		{
			this.commandText = string.Empty;
			this.commandType = CommandType.Text;
			this.parameterCollection = new DbSourceParameterCollection(this);
		}

		// Token: 0x060014EC RID: 5356 RVA: 0x00077B5B File Offset: 0x00075D5B
		public DbSourceCommand(DbSource parent, CommandOperation operation) : this()
		{
			this.SetParent(parent);
			this.CommandOperation = operation;
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x060014ED RID: 5357 RVA: 0x00077B71 File Offset: 0x00075D71
		// (set) Token: 0x060014EE RID: 5358 RVA: 0x00077B79 File Offset: 0x00075D79
		internal CommandOperation CommandOperation
		{
			get
			{
				return this.commandOperation;
			}
			set
			{
				this.commandOperation = value;
			}
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x060014EF RID: 5359 RVA: 0x00077B82 File Offset: 0x00075D82
		// (set) Token: 0x060014F0 RID: 5360 RVA: 0x00077B8A File Offset: 0x00075D8A
		[DataSourceXmlElement]
		[Browsable(false)]
		public string CommandText
		{
			get
			{
				return this.commandText;
			}
			set
			{
				this.commandText = value;
			}
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x060014F1 RID: 5361 RVA: 0x00077B93 File Offset: 0x00075D93
		// (set) Token: 0x060014F2 RID: 5362 RVA: 0x00077B9C File Offset: 0x00075D9C
		[DataSourceXmlAttribute(ItemType = typeof(CommandType))]
		[DefaultValue(CommandType.Text)]
		public CommandType CommandType
		{
			get
			{
				return this.commandType;
			}
			set
			{
				if (value == CommandType.TableDirect && this._parent != null && this._parent.Connection != null)
				{
					string provider = this._parent.Connection.Provider;
					if (!StringUtil.EqualValue(provider, "System.Data.OleDb"))
					{
						throw new Exception(SR.GetString("DD_E_TableDirectValidForOleDbOnly"));
					}
				}
				this.commandType = value;
			}
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x060014F3 RID: 5363 RVA: 0x00077BFB File Offset: 0x00075DFB
		// (set) Token: 0x060014F4 RID: 5364 RVA: 0x00077C03 File Offset: 0x00075E03
		[Browsable(false)]
		[DataSourceXmlAttribute(ItemType = typeof(bool))]
		public bool ModifiedByUser
		{
			get
			{
				return this.modifiedByUser;
			}
			set
			{
				this.modifiedByUser = value;
			}
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x060014F5 RID: 5365 RVA: 0x00077C0C File Offset: 0x00075E0C
		// (set) Token: 0x060014F6 RID: 5366 RVA: 0x00077C14 File Offset: 0x00075E14
		[Browsable(false)]
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x060014F7 RID: 5367 RVA: 0x00077C1D File Offset: 0x00075E1D
		[DataSourceXmlSubItem(ItemType = typeof(DesignParameter))]
		public DbSourceParameterCollection Parameters
		{
			get
			{
				return this.parameterCollection;
			}
		}

		// Token: 0x060014F8 RID: 5368 RVA: 0x00077C25 File Offset: 0x00075E25
		private bool ShouldSerializeParameters()
		{
			return this.parameterCollection != null && 0 < this.parameterCollection.Count;
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x060014F9 RID: 5369 RVA: 0x00077C3F File Offset: 0x00075E3F
		[Browsable(false)]
		public override object Parent
		{
			get
			{
				return this._parent;
			}
		}

		// Token: 0x060014FA RID: 5370 RVA: 0x00077C48 File Offset: 0x00075E48
		public object Clone()
		{
			DbSourceCommand dbSourceCommand = new DbSourceCommand();
			dbSourceCommand.commandText = this.commandText;
			dbSourceCommand.commandType = this.commandType;
			dbSourceCommand.commandOperation = this.commandOperation;
			dbSourceCommand.parameterCollection = (DbSourceParameterCollection)this.parameterCollection.Clone();
			dbSourceCommand.parameterCollection.CollectionHost = dbSourceCommand;
			return dbSourceCommand;
		}

		// Token: 0x060014FB RID: 5371 RVA: 0x00077CA2 File Offset: 0x00075EA2
		internal void SetParent(DbSource parent)
		{
			this._parent = parent;
		}

		// Token: 0x060014FC RID: 5372 RVA: 0x00077CAB File Offset: 0x00075EAB
		public override string ToString()
		{
			if (StringUtil.NotEmptyAfterTrim(((INamedObject)this).Name))
			{
				return ((INamedObject)this).Name;
			}
			return base.ToString();
		}

		// Token: 0x04000B0F RID: 2831
		private DbSource _parent;

		// Token: 0x04000B10 RID: 2832
		private CommandOperation commandOperation;

		// Token: 0x04000B11 RID: 2833
		private string commandText;

		// Token: 0x04000B12 RID: 2834
		private CommandType commandType;

		// Token: 0x04000B13 RID: 2835
		private DbSourceParameterCollection parameterCollection;

		// Token: 0x04000B14 RID: 2836
		private bool modifiedByUser;

		// Token: 0x04000B15 RID: 2837
		private string name;
	}
}
