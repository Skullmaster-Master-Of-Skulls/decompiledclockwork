using System;
using System.Collections.Generic;
using System.Data.Metadata.Edm;
using System.Globalization;

namespace System.Data.SqlClient.SqlGen
{
	// Token: 0x02000035 RID: 53
	internal class Symbol : ISqlFragment
	{
		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060004E8 RID: 1256 RVA: 0x00016F25 File Offset: 0x00015125
		internal Dictionary<string, Symbol> Columns
		{
			get
			{
				if (this.columns == null)
				{
					this.columns = new Dictionary<string, Symbol>(StringComparer.OrdinalIgnoreCase);
				}
				return this.columns;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060004E9 RID: 1257 RVA: 0x00016F45 File Offset: 0x00015145
		internal Dictionary<string, Symbol> OutputColumns
		{
			get
			{
				if (this.outputColumns == null)
				{
					this.outputColumns = new Dictionary<string, Symbol>(StringComparer.OrdinalIgnoreCase);
				}
				return this.outputColumns;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060004EA RID: 1258 RVA: 0x00016F65 File Offset: 0x00015165
		// (set) Token: 0x060004EB RID: 1259 RVA: 0x00016F6D File Offset: 0x0001516D
		internal bool NeedsRenaming
		{
			get
			{
				return this.needsRenaming;
			}
			set
			{
				this.needsRenaming = value;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060004EC RID: 1260 RVA: 0x00016F76 File Offset: 0x00015176
		// (set) Token: 0x060004ED RID: 1261 RVA: 0x00016F7E File Offset: 0x0001517E
		internal bool OutputColumnsRenamed
		{
			get
			{
				return this.outputColumnsRenamed;
			}
			set
			{
				this.outputColumnsRenamed = value;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060004EE RID: 1262 RVA: 0x00016F87 File Offset: 0x00015187
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060004EF RID: 1263 RVA: 0x00016F8F File Offset: 0x0001518F
		// (set) Token: 0x060004F0 RID: 1264 RVA: 0x00016F97 File Offset: 0x00015197
		public string NewName
		{
			get
			{
				return this.newName;
			}
			set
			{
				this.newName = value;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060004F1 RID: 1265 RVA: 0x00016FA0 File Offset: 0x000151A0
		// (set) Token: 0x060004F2 RID: 1266 RVA: 0x00016FA8 File Offset: 0x000151A8
		internal TypeUsage Type
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x00016FB1 File Offset: 0x000151B1
		public Symbol(string name, TypeUsage type)
		{
			this.name = name;
			this.newName = name;
			this.Type = type;
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x00016FCE File Offset: 0x000151CE
		public Symbol(string name, TypeUsage type, Dictionary<string, Symbol> outputColumns, bool outputColumnsRenamed)
		{
			this.name = name;
			this.newName = name;
			this.Type = type;
			this.outputColumns = outputColumns;
			this.OutputColumnsRenamed = outputColumnsRenamed;
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x00016FFC File Offset: 0x000151FC
		public void WriteSql(SqlWriter writer, SqlGenerator sqlGenerator)
		{
			if (this.NeedsRenaming)
			{
				int num;
				if (sqlGenerator.AllColumnNames.TryGetValue(this.NewName, out num))
				{
					string key;
					do
					{
						num++;
						key = this.NewName + num.ToString(CultureInfo.InvariantCulture);
					}
					while (sqlGenerator.AllColumnNames.ContainsKey(key));
					sqlGenerator.AllColumnNames[this.NewName] = num;
					this.NewName = key;
				}
				sqlGenerator.AllColumnNames[this.NewName] = 0;
				this.NeedsRenaming = false;
			}
			writer.Write(SqlGenerator.QuoteIdentifier(this.NewName));
		}

		// Token: 0x04000730 RID: 1840
		private Dictionary<string, Symbol> columns;

		// Token: 0x04000731 RID: 1841
		private Dictionary<string, Symbol> outputColumns;

		// Token: 0x04000732 RID: 1842
		private bool needsRenaming;

		// Token: 0x04000733 RID: 1843
		private bool outputColumnsRenamed;

		// Token: 0x04000734 RID: 1844
		private string name;

		// Token: 0x04000735 RID: 1845
		private string newName;

		// Token: 0x04000736 RID: 1846
		private TypeUsage type;
	}
}
