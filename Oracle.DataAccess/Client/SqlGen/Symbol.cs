using System;
using System.Collections.Generic;
using System.Data.Metadata.Edm;
using System.Globalization;

namespace Oracle.DataAccess.Client.SqlGen
{
	// Token: 0x02000016 RID: 22
	internal class Symbol : ISqlFragment
	{
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x0000F2B4 File Offset: 0x0000E2B4
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

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x0000F2D4 File Offset: 0x0000E2D4
		// (set) Token: 0x060000B2 RID: 178 RVA: 0x0000F2DC File Offset: 0x0000E2DC
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

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x0000F2E5 File Offset: 0x0000E2E5
		// (set) Token: 0x060000B4 RID: 180 RVA: 0x0000F2ED File Offset: 0x0000E2ED
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

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x0000F2F6 File Offset: 0x0000E2F6
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x0000F2FE File Offset: 0x0000E2FE
		// (set) Token: 0x060000B7 RID: 183 RVA: 0x0000F306 File Offset: 0x0000E306
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

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x0000F30F File Offset: 0x0000E30F
		// (set) Token: 0x060000B9 RID: 185 RVA: 0x0000F317 File Offset: 0x0000E317
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

		// Token: 0x060000BA RID: 186 RVA: 0x0000F320 File Offset: 0x0000E320
		public Symbol(string name, TypeUsage type)
		{
			this.name = name;
			this.newName = name;
			this.Type = type;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x0000F33D File Offset: 0x0000E33D
		public Symbol(string name, TypeUsage type, Dictionary<string, Symbol> columns)
		{
			this.name = name;
			this.newName = name;
			this.Type = type;
			this.columns = columns;
			this.OutputColumnsRenamed = true;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x0000F368 File Offset: 0x0000E368
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

		// Token: 0x0400009D RID: 157
		private Dictionary<string, Symbol> columns;

		// Token: 0x0400009E RID: 158
		private bool needsRenaming;

		// Token: 0x0400009F RID: 159
		private bool outputColumnsRenamed;

		// Token: 0x040000A0 RID: 160
		private string name;

		// Token: 0x040000A1 RID: 161
		private string newName;

		// Token: 0x040000A2 RID: 162
		private TypeUsage type;
	}
}
