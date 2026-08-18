using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.Internal;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x0200010E RID: 270
	public abstract class DbCommandTree
	{
		// Token: 0x0600070C RID: 1804 RVA: 0x00026BD5 File Offset: 0x00024DD5
		internal DbCommandTree()
		{
			this._useDatabaseNullSemantics = true;
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x00026BE4 File Offset: 0x00024DE4
		internal DbCommandTree(MetadataWorkspace metadata, DataSpace dataSpace, bool useDatabaseNullSemantics = true)
		{
			if (!DbCommandTree.IsValidDataSpace(dataSpace))
			{
				throw new ArgumentException(Strings.Cqt_CommandTree_InvalidDataSpace, "dataSpace");
			}
			this._metadata = metadata;
			this._dataSpace = dataSpace;
			this._useDatabaseNullSemantics = useDatabaseNullSemantics;
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600070E RID: 1806 RVA: 0x00026C19 File Offset: 0x00024E19
		public bool UseDatabaseNullSemantics
		{
			get
			{
				return this._useDatabaseNullSemantics;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600070F RID: 1807 RVA: 0x00026C21 File Offset: 0x00024E21
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public IEnumerable<KeyValuePair<string, TypeUsage>> Parameters
		{
			get
			{
				return this.GetParameters();
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000710 RID: 1808
		public abstract DbCommandTreeKind CommandTreeKind { get; }

		// Token: 0x06000711 RID: 1809
		internal abstract IEnumerable<KeyValuePair<string, TypeUsage>> GetParameters();

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000712 RID: 1810 RVA: 0x00026C29 File Offset: 0x00024E29
		public virtual MetadataWorkspace MetadataWorkspace
		{
			get
			{
				return this._metadata;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000713 RID: 1811 RVA: 0x00026C31 File Offset: 0x00024E31
		public virtual DataSpace DataSpace
		{
			get
			{
				return this._dataSpace;
			}
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x00026C3C File Offset: 0x00024E3C
		internal void Dump(ExpressionDumper dumper)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary.Add("DataSpace", this.DataSpace);
			dumper.Begin(base.GetType().Name, dictionary);
			dumper.Begin("Parameters", null);
			foreach (KeyValuePair<string, TypeUsage> keyValuePair in this.Parameters)
			{
				dumper.Begin("Parameter", new Dictionary<string, object>
				{
					{
						"Name",
						keyValuePair.Key
					}
				});
				dumper.Dump(keyValuePair.Value, "ParameterType");
				dumper.End("Parameter");
			}
			dumper.End("Parameters");
			this.DumpStructure(dumper);
			dumper.End(base.GetType().Name);
		}

		// Token: 0x06000715 RID: 1813
		internal abstract void DumpStructure(ExpressionDumper dumper);

		// Token: 0x06000716 RID: 1814 RVA: 0x00026D20 File Offset: 0x00024F20
		public override string ToString()
		{
			return this.Print();
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x00026D28 File Offset: 0x00024F28
		internal string Print()
		{
			return this.PrintTree(new ExpressionPrinter());
		}

		// Token: 0x06000718 RID: 1816
		internal abstract string PrintTree(ExpressionPrinter printer);

		// Token: 0x06000719 RID: 1817 RVA: 0x00026D35 File Offset: 0x00024F35
		internal static bool IsValidDataSpace(DataSpace dataSpace)
		{
			return dataSpace == DataSpace.OSpace || DataSpace.CSpace == dataSpace || DataSpace.SSpace == dataSpace;
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x00026D44 File Offset: 0x00024F44
		internal static bool IsValidParameterName(string name)
		{
			return !string.IsNullOrWhiteSpace(name) && name.IsValidUndottedName();
		}

		// Token: 0x04000202 RID: 514
		private readonly MetadataWorkspace _metadata;

		// Token: 0x04000203 RID: 515
		private readonly DataSpace _dataSpace;

		// Token: 0x04000204 RID: 516
		private readonly bool _useDatabaseNullSemantics;
	}
}
