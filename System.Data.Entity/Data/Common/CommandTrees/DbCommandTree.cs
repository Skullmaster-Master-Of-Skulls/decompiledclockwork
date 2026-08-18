using System;
using System.Collections.Generic;
using System.Data.Common.CommandTrees.Internal;
using System.Data.Common.Utils;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.IO;
using System.Text.RegularExpressions;

namespace System.Data.Common.CommandTrees
{
	// Token: 0x020003E4 RID: 996
	public abstract class DbCommandTree
	{
		// Token: 0x06003555 RID: 13653 RVA: 0x000CFA54 File Offset: 0x000CDC54
		internal DbCommandTree(MetadataWorkspace metadata, DataSpace dataSpace)
		{
			EntityUtil.CheckArgumentNull<MetadataWorkspace>(metadata, "metadata");
			if (!DbCommandTree.IsValidDataSpace(dataSpace))
			{
				throw EntityUtil.Argument(Strings.Cqt_CommandTree_InvalidDataSpace, "dataSpace");
			}
			MetadataWorkspace metadataWorkspace = new MetadataWorkspace();
			ItemCollection collection;
			if (metadata.TryGetItemCollection(DataSpace.OSpace, out collection))
			{
				metadataWorkspace.RegisterItemCollection(collection);
			}
			metadataWorkspace.RegisterItemCollection(metadata.GetItemCollection(DataSpace.CSpace));
			metadataWorkspace.RegisterItemCollection(metadata.GetItemCollection(DataSpace.CSSpace));
			metadataWorkspace.RegisterItemCollection(metadata.GetItemCollection(DataSpace.SSpace));
			this._metadata = metadataWorkspace;
			this._dataSpace = dataSpace;
		}

		// Token: 0x17000A3B RID: 2619
		// (get) Token: 0x06003556 RID: 13654 RVA: 0x000CFAD8 File Offset: 0x000CDCD8
		public IEnumerable<KeyValuePair<string, TypeUsage>> Parameters
		{
			get
			{
				return this.GetParameters();
			}
		}

		// Token: 0x17000A3C RID: 2620
		// (get) Token: 0x06003557 RID: 13655
		internal abstract DbCommandTreeKind CommandTreeKind { get; }

		// Token: 0x06003558 RID: 13656
		internal abstract IEnumerable<KeyValuePair<string, TypeUsage>> GetParameters();

		// Token: 0x17000A3D RID: 2621
		// (get) Token: 0x06003559 RID: 13657 RVA: 0x000CFAE0 File Offset: 0x000CDCE0
		internal MetadataWorkspace MetadataWorkspace
		{
			get
			{
				return this._metadata;
			}
		}

		// Token: 0x17000A3E RID: 2622
		// (get) Token: 0x0600355A RID: 13658 RVA: 0x000CFAE8 File Offset: 0x000CDCE8
		internal DataSpace DataSpace
		{
			get
			{
				return this._dataSpace;
			}
		}

		// Token: 0x0600355B RID: 13659 RVA: 0x000CFAF0 File Offset: 0x000CDCF0
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

		// Token: 0x0600355C RID: 13660
		internal abstract void DumpStructure(ExpressionDumper dumper);

		// Token: 0x0600355D RID: 13661 RVA: 0x000CFBD4 File Offset: 0x000CDDD4
		internal string DumpXml()
		{
			MemoryStream memoryStream = new MemoryStream();
			XmlExpressionDumper xmlExpressionDumper = new XmlExpressionDumper(memoryStream);
			this.Dump(xmlExpressionDumper);
			xmlExpressionDumper.Close();
			return XmlExpressionDumper.DefaultEncoding.GetString(memoryStream.ToArray());
		}

		// Token: 0x0600355E RID: 13662 RVA: 0x000CFC0B File Offset: 0x000CDE0B
		internal string Print()
		{
			return this.PrintTree(new ExpressionPrinter());
		}

		// Token: 0x0600355F RID: 13663
		internal abstract string PrintTree(ExpressionPrinter printer);

		// Token: 0x06003560 RID: 13664 RVA: 0x000CFC18 File Offset: 0x000CDE18
		internal static bool IsValidDataSpace(DataSpace dataSpace)
		{
			return dataSpace == DataSpace.OSpace || DataSpace.CSpace == dataSpace || DataSpace.SSpace == dataSpace;
		}

		// Token: 0x06003561 RID: 13665 RVA: 0x000CFC27 File Offset: 0x000CDE27
		internal static bool IsValidParameterName(string name)
		{
			return !StringUtil.IsNullOrEmptyOrWhiteSpace(name) && DbCommandTree._paramNameRegex.IsMatch(name);
		}

		// Token: 0x040017A7 RID: 6055
		private readonly MetadataWorkspace _metadata;

		// Token: 0x040017A8 RID: 6056
		private readonly DataSpace _dataSpace;

		// Token: 0x040017A9 RID: 6057
		private static readonly Regex _paramNameRegex = new Regex("^([A-Za-z])([A-Za-z0-9_])*$");
	}
}
