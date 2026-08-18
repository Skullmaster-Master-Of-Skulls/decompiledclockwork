using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Globalization;

namespace System.Data.Entity.SqlServer.SqlGen
{
	// Token: 0x02000030 RID: 48
	internal class Symbol : ISqlFragment
	{
		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x0000B8F5 File Offset: 0x00009AF5
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

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x0000B915 File Offset: 0x00009B15
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

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060002A8 RID: 680 RVA: 0x0000B935 File Offset: 0x00009B35
		// (set) Token: 0x060002A9 RID: 681 RVA: 0x0000B93D File Offset: 0x00009B3D
		internal bool NeedsRenaming { get; set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060002AA RID: 682 RVA: 0x0000B946 File Offset: 0x00009B46
		// (set) Token: 0x060002AB RID: 683 RVA: 0x0000B94E File Offset: 0x00009B4E
		internal bool OutputColumnsRenamed { get; set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060002AC RID: 684 RVA: 0x0000B957 File Offset: 0x00009B57
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060002AD RID: 685 RVA: 0x0000B95F File Offset: 0x00009B5F
		// (set) Token: 0x060002AE RID: 686 RVA: 0x0000B967 File Offset: 0x00009B67
		public string NewName { get; set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060002AF RID: 687 RVA: 0x0000B970 File Offset: 0x00009B70
		// (set) Token: 0x060002B0 RID: 688 RVA: 0x0000B978 File Offset: 0x00009B78
		internal TypeUsage Type { get; set; }

		// Token: 0x060002B1 RID: 689 RVA: 0x0000B981 File Offset: 0x00009B81
		public Symbol(string name, TypeUsage type)
		{
			this.name = name;
			this.NewName = name;
			this.Type = type;
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0000B99E File Offset: 0x00009B9E
		public Symbol(string name, TypeUsage type, Dictionary<string, Symbol> outputColumns, bool outputColumnsRenamed)
		{
			this.name = name;
			this.NewName = name;
			this.Type = type;
			this.outputColumns = outputColumns;
			this.OutputColumnsRenamed = outputColumnsRenamed;
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0000B9CC File Offset: 0x00009BCC
		public void WriteSql(SqlWriter writer, SqlGenerator sqlGenerator)
		{
			if (this.NeedsRenaming)
			{
				int num;
				if (sqlGenerator.AllColumnNames.TryGetValue(this.NewName, out num))
				{
					string text;
					do
					{
						num++;
						text = this.NewName + num.ToString(CultureInfo.InvariantCulture);
					}
					while (sqlGenerator.AllColumnNames.ContainsKey(text));
					sqlGenerator.AllColumnNames[this.NewName] = num;
					this.NewName = text;
				}
				sqlGenerator.AllColumnNames[this.NewName] = 0;
				this.NeedsRenaming = false;
			}
			writer.Write(SqlGenerator.QuoteIdentifier(this.NewName));
		}

		// Token: 0x04000080 RID: 128
		private Dictionary<string, Symbol> columns;

		// Token: 0x04000081 RID: 129
		private Dictionary<string, Symbol> outputColumns;

		// Token: 0x04000082 RID: 130
		private readonly string name;
	}
}
