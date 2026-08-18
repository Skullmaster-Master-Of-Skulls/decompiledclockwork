using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Metadata.Edm;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000AF RID: 175
	internal class Dump : BasicOpVisitor, IDisposable
	{
		// Token: 0x06000B06 RID: 2822 RVA: 0x00037C0F File Offset: 0x00035E0F
		private Dump(Stream stream) : this(stream, Dump.DefaultEncoding, true)
		{
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x00037C20 File Offset: 0x00035E20
		private Dump(Stream stream, Encoding encoding, bool indent)
		{
			this._writer = XmlWriter.Create(stream, new XmlWriterSettings
			{
				CheckCharacters = false,
				Indent = true,
				Encoding = encoding
			});
			this._writer.WriteStartDocument(true);
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x00037C67 File Offset: 0x00035E67
		internal static string ToXml(Command itree)
		{
			return Dump.ToXml(itree, itree.Root);
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x00037C78 File Offset: 0x00035E78
		internal static string ToXml(Command itree, Node subtree)
		{
			MemoryStream memoryStream = new MemoryStream();
			using (Dump dump = new Dump(memoryStream))
			{
				using (new Dump.AutoXml(dump, "nodes"))
				{
					dump.VisitNode(subtree);
				}
			}
			return Dump.DefaultEncoding.GetString(memoryStream.ToArray());
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x00037CEC File Offset: 0x00035EEC
		internal static string ToXml(ColumnMap columnMap)
		{
			MemoryStream memoryStream = new MemoryStream();
			using (Dump dump = new Dump(memoryStream))
			{
				using (new Dump.AutoXml(dump, "columnMap"))
				{
					columnMap.Accept<Dump>(Dump.ColumnMapDumper.Instance, dump);
				}
			}
			return Dump.DefaultEncoding.GetString(memoryStream.ToArray());
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x00037D64 File Offset: 0x00035F64
		void IDisposable.Dispose()
		{
			GC.SuppressFinalize(this);
			try
			{
				this._writer.WriteEndDocument();
				this._writer.Flush();
				this._writer.Close();
			}
			catch (Exception e)
			{
				if (!EntityUtil.IsCatchableExceptionType(e))
				{
					throw;
				}
			}
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x00037DB8 File Offset: 0x00035FB8
		internal void Begin(string name, Dictionary<string, object> attrs)
		{
			this._writer.WriteStartElement(name);
			if (attrs != null)
			{
				foreach (KeyValuePair<string, object> keyValuePair in attrs)
				{
					this._writer.WriteAttributeString(keyValuePair.Key, keyValuePair.Value.ToString());
				}
			}
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x00037E2C File Offset: 0x0003602C
		internal void BeginExpression()
		{
			this.WriteString("(");
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x00037E39 File Offset: 0x00036039
		internal void EndExpression()
		{
			this.WriteString(")");
		}

		// Token: 0x06000B0F RID: 2831 RVA: 0x00037E46 File Offset: 0x00036046
		internal void End(string name)
		{
			this._writer.WriteEndElement();
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x00037E53 File Offset: 0x00036053
		internal void WriteString(string value)
		{
			this._writer.WriteString(value);
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x00037E64 File Offset: 0x00036064
		protected override void VisitDefault(Node n)
		{
			using (new Dump.AutoXml(this, n.Op))
			{
				base.VisitDefault(n);
			}
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x00037EA8 File Offset: 0x000360A8
		protected override void VisitScalarOpDefault(ScalarOp op, Node n)
		{
			using (new Dump.AutoString(this, op))
			{
				string value = string.Empty;
				foreach (Node n2 in n.Children)
				{
					this.WriteString(value);
					this.VisitNode(n2);
					value = ",";
				}
			}
		}

		// Token: 0x06000B13 RID: 2835 RVA: 0x00037F34 File Offset: 0x00036134
		protected override void VisitJoinOp(JoinBaseOp op, Node n)
		{
			using (new Dump.AutoXml(this, op))
			{
				if (n.Children.Count > 2)
				{
					using (new Dump.AutoXml(this, "condition"))
					{
						this.VisitNode(n.Child2);
					}
				}
				using (new Dump.AutoXml(this, "input"))
				{
					this.VisitNode(n.Child0);
				}
				using (new Dump.AutoXml(this, "input"))
				{
					this.VisitNode(n.Child1);
				}
			}
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x00038014 File Offset: 0x00036214
		public override void Visit(CaseOp op, Node n)
		{
			using (new Dump.AutoXml(this, op))
			{
				int i = 0;
				while (i < n.Children.Count)
				{
					if (i + 1 < n.Children.Count)
					{
						using (new Dump.AutoXml(this, "when"))
						{
							this.VisitNode(n.Children[i++]);
						}
						using (new Dump.AutoXml(this, "then"))
						{
							this.VisitNode(n.Children[i++]);
							continue;
						}
					}
					using (new Dump.AutoXml(this, "else"))
					{
						this.VisitNode(n.Children[i++]);
					}
				}
			}
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x00038130 File Offset: 0x00036330
		public override void Visit(CollectOp op, Node n)
		{
			using (new Dump.AutoXml(this, op))
			{
				this.VisitChildren(n);
			}
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x00038170 File Offset: 0x00036370
		protected override void VisitConstantOp(ConstantBaseOp op, Node n)
		{
			using (new Dump.AutoString(this, op))
			{
				if (op.Value == null)
				{
					this.WriteString("null");
				}
				else
				{
					this.WriteString("(");
					this.WriteString(op.Type.EdmType.FullName);
					this.WriteString(")");
					this.WriteString(string.Format(CultureInfo.InvariantCulture, "{0}", new object[]
					{
						op.Value
					}));
				}
				this.VisitChildren(n);
			}
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x00038214 File Offset: 0x00036414
		public override void Visit(DistinctOp op, Node n)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			StringBuilder stringBuilder = new StringBuilder();
			string value = string.Empty;
			foreach (Var var in op.Keys)
			{
				stringBuilder.Append(value);
				stringBuilder.Append(var.Id);
				value = ",";
			}
			if (stringBuilder.Length != 0)
			{
				dictionary.Add("Keys", stringBuilder.ToString());
			}
			using (new Dump.AutoXml(this, op, dictionary))
			{
				this.VisitChildren(n);
			}
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x000382D0 File Offset: 0x000364D0
		protected override void VisitGroupByOp(GroupByBaseOp op, Node n)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			StringBuilder stringBuilder = new StringBuilder();
			string value = string.Empty;
			foreach (Var var in op.Keys)
			{
				stringBuilder.Append(value);
				stringBuilder.Append(var.Id);
				value = ",";
			}
			if (stringBuilder.Length != 0)
			{
				dictionary.Add("Keys", stringBuilder.ToString());
			}
			using (new Dump.AutoXml(this, op, dictionary))
			{
				using (new Dump.AutoXml(this, "outputs"))
				{
					foreach (Var v in op.Outputs)
					{
						this.DumpVar(v);
					}
				}
				this.VisitChildren(n);
			}
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x000383F8 File Offset: 0x000365F8
		public override void Visit(IsOfOp op, Node n)
		{
			using (new Dump.AutoXml(this, op.IsOfOnly ? "IsOfOnly" : "IsOf"))
			{
				string value = string.Empty;
				foreach (Node n2 in n.Children)
				{
					this.WriteString(value);
					this.VisitNode(n2);
					value = ",";
				}
			}
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x00038494 File Offset: 0x00036694
		protected override void VisitNestOp(NestBaseOp op, Node n)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			SingleStreamNestOp singleStreamNestOp = op as SingleStreamNestOp;
			if (singleStreamNestOp != null)
			{
				dictionary.Add("Discriminator", (singleStreamNestOp.Discriminator == null) ? "<null>" : singleStreamNestOp.Discriminator.ToString());
			}
			StringBuilder stringBuilder = new StringBuilder();
			if (singleStreamNestOp != null)
			{
				stringBuilder.Length = 0;
				string value = string.Empty;
				foreach (Var var in singleStreamNestOp.Keys)
				{
					stringBuilder.Append(value);
					stringBuilder.Append(var.Id);
					value = ",";
				}
				if (stringBuilder.Length != 0)
				{
					dictionary.Add("Keys", stringBuilder.ToString());
				}
			}
			using (new Dump.AutoXml(this, op, dictionary))
			{
				using (new Dump.AutoXml(this, "outputs"))
				{
					foreach (Var v in op.Outputs)
					{
						this.DumpVar(v);
					}
				}
				foreach (CollectionInfo collectionInfo in op.CollectionInfo)
				{
					Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
					dictionary2.Add("CollectionVar", collectionInfo.CollectionVar);
					if (collectionInfo.DiscriminatorValue != null)
					{
						dictionary2.Add("DiscriminatorValue", collectionInfo.DiscriminatorValue);
					}
					if (collectionInfo.FlattenedElementVars.Count != 0)
					{
						dictionary2.Add("FlattenedElementVars", Dump.FormatVarList(stringBuilder, collectionInfo.FlattenedElementVars));
					}
					if (collectionInfo.Keys.Count != 0)
					{
						dictionary2.Add("Keys", collectionInfo.Keys);
					}
					if (collectionInfo.SortKeys.Count != 0)
					{
						dictionary2.Add("SortKeys", Dump.FormatVarList(stringBuilder, collectionInfo.SortKeys));
					}
					using (new Dump.AutoXml(this, "collection", dictionary2))
					{
						collectionInfo.ColumnMap.Accept<Dump>(Dump.ColumnMapDumper.Instance, this);
					}
				}
				this.VisitChildren(n);
			}
		}

		// Token: 0x06000B1B RID: 2843 RVA: 0x00038764 File Offset: 0x00036964
		private static string FormatVarList(StringBuilder sb, VarList varList)
		{
			sb.Length = 0;
			string value = string.Empty;
			foreach (Var var in varList)
			{
				sb.Append(value);
				sb.Append(var.Id);
				value = ",";
			}
			return sb.ToString();
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x000387DC File Offset: 0x000369DC
		private static string FormatVarList(StringBuilder sb, List<SortKey> varList)
		{
			sb.Length = 0;
			string value = string.Empty;
			foreach (SortKey sortKey in varList)
			{
				sb.Append(value);
				sb.Append(sortKey.Var.Id);
				value = ",";
			}
			return sb.ToString();
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x00038858 File Offset: 0x00036A58
		private void VisitNewOp(Op op, Node n)
		{
			using (new Dump.AutoXml(this, op))
			{
				foreach (Node n2 in n.Children)
				{
					using (new Dump.AutoXml(this, "argument", null))
					{
						this.VisitNode(n2);
					}
				}
			}
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x000388F8 File Offset: 0x00036AF8
		public override void Visit(NewEntityOp op, Node n)
		{
			this.VisitNewOp(op, n);
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x000388F8 File Offset: 0x00036AF8
		public override void Visit(NewInstanceOp op, Node n)
		{
			this.VisitNewOp(op, n);
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x000388F8 File Offset: 0x00036AF8
		public override void Visit(DiscriminatedNewEntityOp op, Node n)
		{
			this.VisitNewOp(op, n);
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x000388F8 File Offset: 0x00036AF8
		public override void Visit(NewMultisetOp op, Node n)
		{
			this.VisitNewOp(op, n);
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x000388F8 File Offset: 0x00036AF8
		public override void Visit(NewRecordOp op, Node n)
		{
			this.VisitNewOp(op, n);
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x00038904 File Offset: 0x00036B04
		public override void Visit(PhysicalProjectOp op, Node n)
		{
			using (new Dump.AutoXml(this, op))
			{
				using (new Dump.AutoXml(this, "outputs"))
				{
					foreach (Var v in op.Outputs)
					{
						this.DumpVar(v);
					}
				}
				using (new Dump.AutoXml(this, "columnMap"))
				{
					op.ColumnMap.Accept<Dump>(Dump.ColumnMapDumper.Instance, this);
				}
				using (new Dump.AutoXml(this, "input"))
				{
					this.VisitChildren(n);
				}
			}
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x00038A0C File Offset: 0x00036C0C
		public override void Visit(ProjectOp op, Node n)
		{
			using (new Dump.AutoXml(this, op))
			{
				using (new Dump.AutoXml(this, "outputs"))
				{
					foreach (Var v in op.Outputs)
					{
						this.DumpVar(v);
					}
				}
				this.VisitChildren(n);
			}
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x00038AAC File Offset: 0x00036CAC
		public override void Visit(PropertyOp op, Node n)
		{
			using (new Dump.AutoString(this, op))
			{
				this.VisitChildren(n);
				this.WriteString(".");
				this.WriteString(op.PropertyInfo.Name);
			}
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x00038B08 File Offset: 0x00036D08
		public override void Visit(RelPropertyOp op, Node n)
		{
			using (new Dump.AutoString(this, op))
			{
				this.VisitChildren(n);
				this.WriteString(".NAVIGATE(");
				this.WriteString(op.PropertyInfo.Relationship.Name);
				this.WriteString(",");
				this.WriteString(op.PropertyInfo.FromEnd.Name);
				this.WriteString(",");
				this.WriteString(op.PropertyInfo.ToEnd.Name);
				this.WriteString(")");
			}
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x00038BB4 File Offset: 0x00036DB4
		public override void Visit(ScanTableOp op, Node n)
		{
			using (new Dump.AutoXml(this, op))
			{
				this.DumpTable(op.Table);
				this.VisitChildren(n);
			}
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x00038C00 File Offset: 0x00036E00
		public override void Visit(ScanViewOp op, Node n)
		{
			using (new Dump.AutoXml(this, op))
			{
				this.DumpTable(op.Table);
				this.VisitChildren(n);
			}
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x00038C4C File Offset: 0x00036E4C
		protected override void VisitSetOp(SetOp op, Node n)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			if (OpType.UnionAll == op.OpType)
			{
				UnionAllOp unionAllOp = (UnionAllOp)op;
				if (unionAllOp.BranchDiscriminator != null)
				{
					dictionary.Add("branchDiscriminator", unionAllOp.BranchDiscriminator);
				}
			}
			using (new Dump.AutoXml(this, op, dictionary))
			{
				using (new Dump.AutoXml(this, "outputs"))
				{
					foreach (Var v in op.Outputs)
					{
						this.DumpVar(v);
					}
				}
				int num = 0;
				foreach (Node n2 in n.Children)
				{
					using (new Dump.AutoXml(this, "input", new Dictionary<string, object>
					{
						{
							"VarMap",
							op.VarMap[num++].ToString()
						}
					}))
					{
						this.VisitNode(n2);
					}
				}
			}
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x00038DB4 File Offset: 0x00036FB4
		public override void Visit(SortOp op, Node n)
		{
			using (new Dump.AutoXml(this, op))
			{
				base.Visit(op, n);
			}
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x00038DF4 File Offset: 0x00036FF4
		public override void Visit(ConstrainedSortOp op, Node n)
		{
			using (new Dump.AutoXml(this, op, new Dictionary<string, object>
			{
				{
					"WithTies",
					op.WithTies
				}
			}))
			{
				base.Visit(op, n);
			}
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x00038E50 File Offset: 0x00037050
		protected override void VisitSortOp(SortBaseOp op, Node n)
		{
			using (new Dump.AutoXml(this, "keys"))
			{
				foreach (SortKey sortKey in op.Keys)
				{
					using (new Dump.AutoXml(this, "sortKey", new Dictionary<string, object>
					{
						{
							"Var",
							sortKey.Var
						},
						{
							"Ascending",
							sortKey.AscendingSort
						},
						{
							"Collation",
							sortKey.Collation
						}
					}))
					{
					}
				}
			}
			this.VisitChildren(n);
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x00038F34 File Offset: 0x00037134
		public override void Visit(UnnestOp op, Node n)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			if (op.Var != null)
			{
				dictionary.Add("Var", op.Var.Id);
			}
			using (new Dump.AutoXml(this, op, dictionary))
			{
				this.DumpTable(op.Table);
				this.VisitChildren(n);
			}
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x00038FA8 File Offset: 0x000371A8
		public override void Visit(VarDefOp op, Node n)
		{
			using (new Dump.AutoXml(this, op, new Dictionary<string, object>
			{
				{
					"Var",
					op.Var.Id
				}
			}))
			{
				this.VisitChildren(n);
			}
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x00039008 File Offset: 0x00037208
		public override void Visit(VarRefOp op, Node n)
		{
			using (new Dump.AutoString(this, op))
			{
				this.VisitChildren(n);
				if (op.Type != null)
				{
					this.WriteString("Type=");
					this.WriteString(TypeHelpers.GetFullName(op.Type));
					this.WriteString(", ");
				}
				this.WriteString("Var=");
				this.WriteString(op.Var.Id.ToString(CultureInfo.InvariantCulture));
			}
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x000390A0 File Offset: 0x000372A0
		private void DumpVar(Var v)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary.Add("Var", v.Id);
			ColumnVar columnVar = v as ColumnVar;
			if (columnVar != null)
			{
				dictionary.Add("Name", columnVar.ColumnMetadata.Name);
				dictionary.Add("Type", TypeHelpers.GetFullName(columnVar.ColumnMetadata.Type));
			}
			using (new Dump.AutoXml(this, v.GetType().Name, dictionary))
			{
			}
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x00039138 File Offset: 0x00037338
		private void DumpVars(List<Var> vars)
		{
			foreach (Var v in vars)
			{
				this.DumpVar(v);
			}
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x00039188 File Offset: 0x00037388
		private void DumpTable(Table table)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary.Add("Table", table.TableId);
			if (table.TableMetadata.Extent != null)
			{
				dictionary.Add("Extent", table.TableMetadata.Extent.Name);
			}
			using (new Dump.AutoXml(this, "Table", dictionary))
			{
				this.DumpVars(table.Columns);
			}
		}

		// Token: 0x040008DC RID: 2268
		private XmlWriter _writer;

		// Token: 0x040008DD RID: 2269
		internal static readonly Encoding DefaultEncoding = Encoding.UTF8;

		// Token: 0x02000492 RID: 1170
		internal class ColumnMapDumper : ColumnMapVisitor<Dump>
		{
			// Token: 0x06003BE3 RID: 15331 RVA: 0x000E165D File Offset: 0x000DF85D
			private ColumnMapDumper()
			{
			}

			// Token: 0x06003BE4 RID: 15332 RVA: 0x000E1668 File Offset: 0x000DF868
			private void DumpCollection(CollectionColumnMap columnMap, Dump dumper)
			{
				if (columnMap.ForeignKeys.Length != 0)
				{
					using (new Dump.AutoXml(dumper, "foreignKeys"))
					{
						base.VisitList<SimpleColumnMap>(columnMap.ForeignKeys, dumper);
					}
				}
				if (columnMap.Keys.Length != 0)
				{
					using (new Dump.AutoXml(dumper, "keys"))
					{
						base.VisitList<SimpleColumnMap>(columnMap.Keys, dumper);
					}
				}
				using (new Dump.AutoXml(dumper, "element"))
				{
					columnMap.Element.Accept<Dump>(this, dumper);
				}
			}

			// Token: 0x06003BE5 RID: 15333 RVA: 0x000E1730 File Offset: 0x000DF930
			private static Dictionary<string, object> GetAttributes(ColumnMap columnMap)
			{
				return new Dictionary<string, object>
				{
					{
						"Type",
						columnMap.Type.ToString()
					}
				};
			}

			// Token: 0x06003BE6 RID: 15334 RVA: 0x000E175C File Offset: 0x000DF95C
			internal override void Visit(ComplexTypeColumnMap columnMap, Dump dumper)
			{
				using (new Dump.AutoXml(dumper, "ComplexType", Dump.ColumnMapDumper.GetAttributes(columnMap)))
				{
					if (columnMap.NullSentinel != null)
					{
						using (new Dump.AutoXml(dumper, "nullSentinel"))
						{
							columnMap.NullSentinel.Accept<Dump>(this, dumper);
						}
					}
					base.VisitList<ColumnMap>(columnMap.Properties, dumper);
				}
			}

			// Token: 0x06003BE7 RID: 15335 RVA: 0x000E17E8 File Offset: 0x000DF9E8
			internal override void Visit(DiscriminatedCollectionColumnMap columnMap, Dump dumper)
			{
				using (new Dump.AutoXml(dumper, "DiscriminatedCollection", Dump.ColumnMapDumper.GetAttributes(columnMap)))
				{
					using (new Dump.AutoXml(dumper, "discriminator", new Dictionary<string, object>
					{
						{
							"Value",
							columnMap.DiscriminatorValue
						}
					}))
					{
						columnMap.Discriminator.Accept<Dump>(this, dumper);
					}
					this.DumpCollection(columnMap, dumper);
				}
			}

			// Token: 0x06003BE8 RID: 15336 RVA: 0x000E1880 File Offset: 0x000DFA80
			internal override void Visit(EntityColumnMap columnMap, Dump dumper)
			{
				using (new Dump.AutoXml(dumper, "Entity", Dump.ColumnMapDumper.GetAttributes(columnMap)))
				{
					using (new Dump.AutoXml(dumper, "entityIdentity"))
					{
						base.VisitEntityIdentity(columnMap.EntityIdentity, dumper);
					}
					base.VisitList<ColumnMap>(columnMap.Properties, dumper);
				}
			}

			// Token: 0x06003BE9 RID: 15337 RVA: 0x000E1904 File Offset: 0x000DFB04
			internal override void Visit(SimplePolymorphicColumnMap columnMap, Dump dumper)
			{
				using (new Dump.AutoXml(dumper, "SimplePolymorphic", Dump.ColumnMapDumper.GetAttributes(columnMap)))
				{
					using (new Dump.AutoXml(dumper, "typeDiscriminator"))
					{
						columnMap.TypeDiscriminator.Accept<Dump>(this, dumper);
					}
					Dictionary<string, object> dictionary = new Dictionary<string, object>();
					foreach (KeyValuePair<object, TypedColumnMap> keyValuePair in columnMap.TypeChoices)
					{
						dictionary.Clear();
						dictionary.Add("DiscriminatorValue", keyValuePair.Key);
						using (new Dump.AutoXml(dumper, "choice", dictionary))
						{
							keyValuePair.Value.Accept<Dump>(this, dumper);
						}
					}
					using (new Dump.AutoXml(dumper, "default"))
					{
						base.VisitList<ColumnMap>(columnMap.Properties, dumper);
					}
				}
			}

			// Token: 0x06003BEA RID: 15338 RVA: 0x000E1A40 File Offset: 0x000DFC40
			internal override void Visit(MultipleDiscriminatorPolymorphicColumnMap columnMap, Dump dumper)
			{
				using (new Dump.AutoXml(dumper, "MultipleDiscriminatorPolymorphic", Dump.ColumnMapDumper.GetAttributes(columnMap)))
				{
					using (new Dump.AutoXml(dumper, "typeDiscriminators"))
					{
						base.VisitList<SimpleColumnMap>(columnMap.TypeDiscriminators, dumper);
					}
					Dictionary<string, object> dictionary = new Dictionary<string, object>();
					foreach (KeyValuePair<EntityType, TypedColumnMap> keyValuePair in columnMap.TypeChoices)
					{
						dictionary.Clear();
						dictionary.Add("EntityType", keyValuePair.Key);
						using (new Dump.AutoXml(dumper, "choice", dictionary))
						{
							keyValuePair.Value.Accept<Dump>(this, dumper);
						}
					}
					using (new Dump.AutoXml(dumper, "default"))
					{
						base.VisitList<ColumnMap>(columnMap.Properties, dumper);
					}
				}
			}

			// Token: 0x06003BEB RID: 15339 RVA: 0x000E1B7C File Offset: 0x000DFD7C
			internal override void Visit(RecordColumnMap columnMap, Dump dumper)
			{
				using (new Dump.AutoXml(dumper, "Record", Dump.ColumnMapDumper.GetAttributes(columnMap)))
				{
					if (columnMap.NullSentinel != null)
					{
						using (new Dump.AutoXml(dumper, "nullSentinel"))
						{
							columnMap.NullSentinel.Accept<Dump>(this, dumper);
						}
					}
					base.VisitList<ColumnMap>(columnMap.Properties, dumper);
				}
			}

			// Token: 0x06003BEC RID: 15340 RVA: 0x000E1C08 File Offset: 0x000DFE08
			internal override void Visit(RefColumnMap columnMap, Dump dumper)
			{
				using (new Dump.AutoXml(dumper, "Ref", Dump.ColumnMapDumper.GetAttributes(columnMap)))
				{
					using (new Dump.AutoXml(dumper, "entityIdentity"))
					{
						base.VisitEntityIdentity(columnMap.EntityIdentity, dumper);
					}
				}
			}

			// Token: 0x06003BED RID: 15341 RVA: 0x000E1C7C File Offset: 0x000DFE7C
			internal override void Visit(SimpleCollectionColumnMap columnMap, Dump dumper)
			{
				using (new Dump.AutoXml(dumper, "SimpleCollection", Dump.ColumnMapDumper.GetAttributes(columnMap)))
				{
					this.DumpCollection(columnMap, dumper);
				}
			}

			// Token: 0x06003BEE RID: 15342 RVA: 0x000E1CC4 File Offset: 0x000DFEC4
			internal override void Visit(ScalarColumnMap columnMap, Dump dumper)
			{
				Dictionary<string, object> attributes = Dump.ColumnMapDumper.GetAttributes(columnMap);
				attributes.Add("CommandId", columnMap.CommandId);
				attributes.Add("ColumnPos", columnMap.ColumnPos);
				using (new Dump.AutoXml(dumper, "AssignedSimple", attributes))
				{
				}
			}

			// Token: 0x06003BEF RID: 15343 RVA: 0x000E1D34 File Offset: 0x000DFF34
			internal override void Visit(VarRefColumnMap columnMap, Dump dumper)
			{
				Dictionary<string, object> attributes = Dump.ColumnMapDumper.GetAttributes(columnMap);
				attributes.Add("Var", columnMap.Var.Id);
				using (new Dump.AutoXml(dumper, "VarRef", attributes))
				{
				}
			}

			// Token: 0x06003BF0 RID: 15344 RVA: 0x000E1D94 File Offset: 0x000DFF94
			protected override void VisitEntityIdentity(DiscriminatedEntityIdentity entityIdentity, Dump dumper)
			{
				using (new Dump.AutoXml(dumper, "DiscriminatedEntityIdentity"))
				{
					using (new Dump.AutoXml(dumper, "entitySetId"))
					{
						entityIdentity.EntitySetColumnMap.Accept<Dump>(this, dumper);
					}
					if (entityIdentity.Keys.Length != 0)
					{
						using (new Dump.AutoXml(dumper, "keys"))
						{
							base.VisitList<SimpleColumnMap>(entityIdentity.Keys, dumper);
						}
					}
				}
			}

			// Token: 0x06003BF1 RID: 15345 RVA: 0x000E1E44 File Offset: 0x000E0044
			protected override void VisitEntityIdentity(SimpleEntityIdentity entityIdentity, Dump dumper)
			{
				using (new Dump.AutoXml(dumper, "SimpleEntityIdentity"))
				{
					if (entityIdentity.Keys.Length != 0)
					{
						using (new Dump.AutoXml(dumper, "keys"))
						{
							base.VisitList<SimpleColumnMap>(entityIdentity.Keys, dumper);
						}
					}
				}
			}

			// Token: 0x040019FA RID: 6650
			internal static Dump.ColumnMapDumper Instance = new Dump.ColumnMapDumper();
		}

		// Token: 0x02000493 RID: 1171
		internal struct AutoString : IDisposable
		{
			// Token: 0x06003BF3 RID: 15347 RVA: 0x000E1EC8 File Offset: 0x000E00C8
			internal AutoString(Dump dumper, Op op)
			{
				this._dumper = dumper;
				this._dumper.WriteString(Dump.AutoString.ToString(op.OpType));
				this._dumper.BeginExpression();
			}

			// Token: 0x06003BF4 RID: 15348 RVA: 0x000E1EF4 File Offset: 0x000E00F4
			public void Dispose()
			{
				try
				{
					this._dumper.EndExpression();
				}
				catch (Exception e)
				{
					if (!EntityUtil.IsCatchableExceptionType(e))
					{
						throw;
					}
				}
			}

			// Token: 0x06003BF5 RID: 15349 RVA: 0x000E1F2C File Offset: 0x000E012C
			internal static string ToString(OpType op)
			{
				switch (op)
				{
				case OpType.Constant:
					return "Constant";
				case OpType.InternalConstant:
					return "InternalConstant";
				case OpType.NullSentinel:
					return "NullSentinel";
				case OpType.Null:
					return "Null";
				case OpType.ConstantPredicate:
					return "ConstantPredicate";
				case OpType.VarRef:
					return "VarRef";
				case OpType.GT:
					return "GT";
				case OpType.GE:
					return "GE";
				case OpType.LE:
					return "LE";
				case OpType.LT:
					return "LT";
				case OpType.EQ:
					return "EQ";
				case OpType.NE:
					return "NE";
				case OpType.Like:
					return "Like";
				case OpType.Plus:
					return "Plus";
				case OpType.Minus:
					return "Minus";
				case OpType.Multiply:
					return "Multiply";
				case OpType.Divide:
					return "Divide";
				case OpType.Modulo:
					return "Modulo";
				case OpType.UnaryMinus:
					return "UnaryMinus";
				case OpType.And:
					return "And";
				case OpType.Or:
					return "Or";
				case OpType.Not:
					return "Not";
				case OpType.IsNull:
					return "IsNull";
				case OpType.Case:
					return "Case";
				case OpType.Treat:
					return "Treat";
				case OpType.IsOf:
					return "IsOf";
				case OpType.Cast:
					return "Cast";
				case OpType.SoftCast:
					return "SoftCast";
				case OpType.Aggregate:
					return "Aggregate";
				case OpType.Function:
					return "Function";
				case OpType.RelProperty:
					return "RelProperty";
				case OpType.Property:
					return "Property";
				case OpType.NewEntity:
					return "NewEntity";
				case OpType.NewInstance:
					return "NewInstance";
				case OpType.DiscriminatedNewEntity:
					return "DiscriminatedNewEntity";
				case OpType.NewMultiset:
					return "NewMultiset";
				case OpType.NewRecord:
					return "NewRecord";
				case OpType.GetRefKey:
					return "GetRefKey";
				case OpType.GetEntityRef:
					return "GetEntityRef";
				case OpType.Ref:
					return "Ref";
				case OpType.Exists:
					return "Exists";
				case OpType.Element:
					return "Element";
				case OpType.Collect:
					return "Collect";
				case OpType.Deref:
					return "Deref";
				case OpType.Navigate:
					return "Navigate";
				case OpType.ScanTable:
					return "ScanTable";
				case OpType.ScanView:
					return "ScanView";
				case OpType.Filter:
					return "Filter";
				case OpType.Project:
					return "Project";
				case OpType.InnerJoin:
					return "InnerJoin";
				case OpType.LeftOuterJoin:
					return "LeftOuterJoin";
				case OpType.FullOuterJoin:
					return "FullOuterJoin";
				case OpType.CrossJoin:
					return "CrossJoin";
				case OpType.CrossApply:
					return "CrossApply";
				case OpType.OuterApply:
					return "OuterApply";
				case OpType.Unnest:
					return "Unnest";
				case OpType.Sort:
					return "Sort";
				case OpType.ConstrainedSort:
					return "ConstrainedSort";
				case OpType.GroupBy:
					return "GroupBy";
				case OpType.GroupByInto:
					return "GroupByInto";
				case OpType.UnionAll:
					return "UnionAll";
				case OpType.Intersect:
					return "Intersect";
				case OpType.Except:
					return "Except";
				case OpType.Distinct:
					return "Distinct";
				case OpType.SingleRow:
					return "SingleRow";
				case OpType.SingleRowTable:
					return "SingleRowTable";
				case OpType.VarDef:
					return "VarDef";
				case OpType.VarDefList:
					return "VarDefList";
				case OpType.Leaf:
					return "Leaf";
				case OpType.PhysicalProject:
					return "PhysicalProject";
				case OpType.SingleStreamNest:
					return "SingleStreamNest";
				case OpType.MultiStreamNest:
					return "MultiStreamNest";
				default:
					return op.ToString();
				}
			}

			// Token: 0x040019FB RID: 6651
			private Dump _dumper;
		}

		// Token: 0x02000494 RID: 1172
		internal struct AutoXml : IDisposable
		{
			// Token: 0x06003BF6 RID: 15350 RVA: 0x000E2224 File Offset: 0x000E0424
			internal AutoXml(Dump dumper, Op op)
			{
				this._dumper = dumper;
				this._nodeName = Dump.AutoString.ToString(op.OpType);
				Dictionary<string, object> dictionary = new Dictionary<string, object>();
				if (op.Type != null)
				{
					dictionary.Add("Type", TypeHelpers.GetFullName(op.Type));
				}
				this._dumper.Begin(this._nodeName, dictionary);
			}

			// Token: 0x06003BF7 RID: 15351 RVA: 0x000E2280 File Offset: 0x000E0480
			internal AutoXml(Dump dumper, Op op, Dictionary<string, object> attrs)
			{
				this._dumper = dumper;
				this._nodeName = Dump.AutoString.ToString(op.OpType);
				Dictionary<string, object> dictionary = new Dictionary<string, object>();
				if (op.Type != null)
				{
					dictionary.Add("Type", TypeHelpers.GetFullName(op.Type));
				}
				foreach (KeyValuePair<string, object> keyValuePair in attrs)
				{
					dictionary.Add(keyValuePair.Key, keyValuePair.Value);
				}
				this._dumper.Begin(this._nodeName, dictionary);
			}

			// Token: 0x06003BF8 RID: 15352 RVA: 0x000E232C File Offset: 0x000E052C
			internal AutoXml(Dump dumper, string nodeName)
			{
				this = new Dump.AutoXml(dumper, nodeName, null);
			}

			// Token: 0x06003BF9 RID: 15353 RVA: 0x000E2337 File Offset: 0x000E0537
			internal AutoXml(Dump dumper, string nodeName, Dictionary<string, object> attrs)
			{
				this._dumper = dumper;
				this._nodeName = nodeName;
				this._dumper.Begin(this._nodeName, attrs);
			}

			// Token: 0x06003BFA RID: 15354 RVA: 0x000E2359 File Offset: 0x000E0559
			public void Dispose()
			{
				this._dumper.End(this._nodeName);
			}

			// Token: 0x040019FC RID: 6652
			private string _nodeName;

			// Token: 0x040019FD RID: 6653
			private Dump _dumper;
		}
	}
}
