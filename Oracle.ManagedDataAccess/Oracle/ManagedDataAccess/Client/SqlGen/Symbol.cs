using System;
using System.Collections.Generic;
using System.Data.Metadata.Edm;
using System.Globalization;

namespace Oracle.ManagedDataAccess.Client.SqlGen
{
	// Token: 0x020000EF RID: 239
	internal class Symbol : ISqlFragment
	{
		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000996 RID: 2454 RVA: 0x0006E0EC File Offset: 0x0006C2EC
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

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000997 RID: 2455 RVA: 0x0006E10C File Offset: 0x0006C30C
		// (set) Token: 0x06000998 RID: 2456 RVA: 0x0006E114 File Offset: 0x0006C314
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

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000999 RID: 2457 RVA: 0x0006E120 File Offset: 0x0006C320
		// (set) Token: 0x0600099A RID: 2458 RVA: 0x0006E128 File Offset: 0x0006C328
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

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x0600099B RID: 2459 RVA: 0x0006E134 File Offset: 0x0006C334
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x0600099C RID: 2460 RVA: 0x0006E13C File Offset: 0x0006C33C
		// (set) Token: 0x0600099D RID: 2461 RVA: 0x0006E144 File Offset: 0x0006C344
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

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x0600099E RID: 2462 RVA: 0x0006E150 File Offset: 0x0006C350
		// (set) Token: 0x0600099F RID: 2463 RVA: 0x0006E158 File Offset: 0x0006C358
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

		// Token: 0x060009A0 RID: 2464 RVA: 0x0006E164 File Offset: 0x0006C364
		public Symbol(string name, TypeUsage type)
		{
			this.name = name;
			this.newName = name;
			this.Type = type;
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x0006E184 File Offset: 0x0006C384
		public Symbol(string name, TypeUsage type, Dictionary<string, Symbol> columns)
		{
			this.name = name;
			this.newName = name;
			this.Type = type;
			this.columns = columns;
			this.OutputColumnsRenamed = true;
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x0006E1B0 File Offset: 0x0006C3B0
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

		// Token: 0x04000C55 RID: 3157
		private Dictionary<string, Symbol> columns;

		// Token: 0x04000C56 RID: 3158
		private bool needsRenaming;

		// Token: 0x04000C57 RID: 3159
		private bool outputColumnsRenamed;

		// Token: 0x04000C58 RID: 3160
		private string name;

		// Token: 0x04000C59 RID: 3161
		private string newName;

		// Token: 0x04000C5A RID: 3162
		private TypeUsage type;
	}
}
