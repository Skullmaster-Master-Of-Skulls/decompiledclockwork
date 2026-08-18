using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x02000635 RID: 1589
	internal class Dump : BasicOpVisitor, IDisposable
	{
		// Token: 0x06003E58 RID: 15960 RVA: 0x0011D30F File Offset: 0x0011B50F
		private Dump(Stream stream) : this(stream, Dump.DefaultEncoding)
		{
		}

		// Token: 0x06003E59 RID: 15961 RVA: 0x0011D320 File Offset: 0x0011B520
		private Dump(Stream stream, Encoding encoding)
		{
			this._writer = XmlWriter.Create(stream, new XmlWriterSettings
			{
				CheckCharacters = false,
				Indent = true,
				Encoding = encoding
			});
			this._writer.WriteStartDocument(true);
		}

		// Token: 0x06003E5A RID: 15962 RVA: 0x0011D367 File Offset: 0x0011B567
		internal static string ToXml(Command itree)
		{
			return Dump.ToXml(itree.Root);
		}

		// Token: 0x06003E5B RID: 15963 RVA: 0x0011D374 File Offset: 0x0011B574
		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
		internal static string ToXml(Node subtree)
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

		// Token: 0x06003E5C RID: 15964 RVA: 0x0011D3EC File Offset: 0x0011B5EC
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
				if (!e.IsCatchableExceptionType())
				{
					throw;
				}
			}
		}

		// Token: 0x06003E5D RID: 15965 RVA: 0x0011D440 File Offset: 0x0011B640
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

		// Token: 0x06003E5E RID: 15966 RVA: 0x0011D4B4 File Offset: 0x0011B6B4
		internal void BeginExpression()
		{
			this.WriteString("(");
		}

		// Token: 0x06003E5F RID: 15967 RVA: 0x0011D4C1 File Offset: 0x0011B6C1
		internal void EndExpression()
		{
			this.WriteString(")");
		}

		// Token: 0x06003E60 RID: 15968 RVA: 0x0011D4CE File Offset: 0x0011B6CE
		internal void End()
		{
			this._writer.WriteEndElement();
		}

		// Token: 0x06003E61 RID: 15969 RVA: 0x0011D4DB File Offset: 0x0011B6DB
		internal void WriteString(string value)
		{
			this._writer.WriteString(value);
		}

		// Token: 0x06003E62 RID: 15970 RVA: 0x0011D4EC File Offset: 0x0011B6EC
		protected override void VisitDefault(Node n)
		{
			using (new Dump.AutoXml(this, n.Op))
			{
				base.VisitDefault(n);
			}
		}

		// Token: 0x06003E63 RID: 15971 RVA: 0x0011D530 File Offset: 0x0011B730
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

		// Token: 0x06003E64 RID: 15972 RVA: 0x0011D5BC File Offset: 0x0011B7BC
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

		// Token: 0x06003E65 RID: 15973 RVA: 0x0011D69C File Offset: 0x0011B89C
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

		// Token: 0x06003E66 RID: 15974 RVA: 0x0011D7B4 File Offset: 0x0011B9B4
		public override void Visit(CollectOp op, Node n)
		{
			using (new Dump.AutoXml(this, op))
			{
				this.VisitChildren(n);
			}
		}

		// Token: 0x06003E67 RID: 15975 RVA: 0x0011D7F0 File Offset: 0x0011B9F0
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

		// Token: 0x06003E68 RID: 15976 RVA: 0x0011D894 File Offset: 0x0011BA94
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

		// Token: 0x06003E69 RID: 15977 RVA: 0x0011D954 File Offset: 0x0011BB54
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

		// Token: 0x06003E6A RID: 15978 RVA: 0x0011DA80 File Offset: 0x0011BC80
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

		// Token: 0x06003E6B RID: 15979 RVA: 0x0011DB20 File Offset: 0x0011BD20
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

		// Token: 0x06003E6C RID: 15980 RVA: 0x0011DDF4 File Offset: 0x0011BFF4
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

		// Token: 0x06003E6D RID: 15981 RVA: 0x0011DE6C File Offset: 0x0011C06C
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

		// Token: 0x06003E6E RID: 15982 RVA: 0x0011DEE8 File Offset: 0x0011C0E8
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

		// Token: 0x06003E6F RID: 15983 RVA: 0x0011DF88 File Offset: 0x0011C188
		public override void Visit(NewEntityOp op, Node n)
		{
			this.VisitNewOp(op, n);
		}

		// Token: 0x06003E70 RID: 15984 RVA: 0x0011DF92 File Offset: 0x0011C192
		public override void Visit(NewInstanceOp op, Node n)
		{
			this.VisitNewOp(op, n);
		}

		// Token: 0x06003E71 RID: 15985 RVA: 0x0011DF9C File Offset: 0x0011C19C
		public override void Visit(DiscriminatedNewEntityOp op, Node n)
		{
			this.VisitNewOp(op, n);
		}

		// Token: 0x06003E72 RID: 15986 RVA: 0x0011DFA6 File Offset: 0x0011C1A6
		public override void Visit(NewMultisetOp op, Node n)
		{
			this.VisitNewOp(op, n);
		}

		// Token: 0x06003E73 RID: 15987 RVA: 0x0011DFB0 File Offset: 0x0011C1B0
		public override void Visit(NewRecordOp op, Node n)
		{
			this.VisitNewOp(op, n);
		}

		// Token: 0x06003E74 RID: 15988 RVA: 0x0011DFBC File Offset: 0x0011C1BC
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

		// Token: 0x06003E75 RID: 15989 RVA: 0x0011E0C8 File Offset: 0x0011C2C8
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

		// Token: 0x06003E76 RID: 15990 RVA: 0x0011E168 File Offset: 0x0011C368
		public override void Visit(PropertyOp op, Node n)
		{
			using (new Dump.AutoString(this, op))
			{
				this.VisitChildren(n);
				this.WriteString(".");
				this.WriteString(op.PropertyInfo.Name);
			}
		}

		// Token: 0x06003E77 RID: 15991 RVA: 0x0011E1C0 File Offset: 0x0011C3C0
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

		// Token: 0x06003E78 RID: 15992 RVA: 0x0011E26C File Offset: 0x0011C46C
		public override void Visit(ScanTableOp op, Node n)
		{
			using (new Dump.AutoXml(this, op))
			{
				this.DumpTable(op.Table);
				this.VisitChildren(n);
			}
		}

		// Token: 0x06003E79 RID: 15993 RVA: 0x0011E2B4 File Offset: 0x0011C4B4
		public override void Visit(ScanViewOp op, Node n)
		{
			using (new Dump.AutoXml(this, op))
			{
				this.DumpTable(op.Table);
				this.VisitChildren(n);
			}
		}

		// Token: 0x06003E7A RID: 15994 RVA: 0x0011E2FC File Offset: 0x0011C4FC
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

		// Token: 0x06003E7B RID: 15995 RVA: 0x0011E464 File Offset: 0x0011C664
		public override void Visit(SortOp op, Node n)
		{
			using (new Dump.AutoXml(this, op))
			{
				base.Visit(op, n);
			}
		}

		// Token: 0x06003E7C RID: 15996 RVA: 0x0011E4A4 File Offset: 0x0011C6A4
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

		// Token: 0x06003E7D RID: 15997 RVA: 0x0011E500 File Offset: 0x0011C700
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

		// Token: 0x06003E7E RID: 15998 RVA: 0x0011E5E4 File Offset: 0x0011C7E4
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

		// Token: 0x06003E7F RID: 15999 RVA: 0x0011E658 File Offset: 0x0011C858
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

		// Token: 0x06003E80 RID: 16000 RVA: 0x0011E6B8 File Offset: 0x0011C8B8
		public override void Visit(VarRefOp op, Node n)
		{
			using (new Dump.AutoString(this, op))
			{
				this.VisitChildren(n);
				if (op.Type != null)
				{
					this.WriteString("Type=");
					this.WriteString(op.Type.ToString());
					this.WriteString(", ");
				}
				this.WriteString("Var=");
				this.WriteString(op.Var.Id.ToString(CultureInfo.InvariantCulture));
			}
		}

		// Token: 0x06003E81 RID: 16001 RVA: 0x0011E74C File Offset: 0x0011C94C
		private void DumpVar(Var v)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary.Add("Var", v.Id);
			ColumnVar columnVar = v as ColumnVar;
			if (columnVar != null)
			{
				dictionary.Add("Name", columnVar.ColumnMetadata.Name);
				dictionary.Add("Type", columnVar.ColumnMetadata.Type.ToString());
			}
			using (new Dump.AutoXml(this, v.GetType().Name, dictionary))
			{
			}
		}

		// Token: 0x06003E82 RID: 16002 RVA: 0x0011E7E4 File Offset: 0x0011C9E4
		private void DumpVars(List<Var> vars)
		{
			foreach (Var v in vars)
			{
				this.DumpVar(v);
			}
		}

		// Token: 0x06003E83 RID: 16003 RVA: 0x0011E834 File Offset: 0x0011CA34
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

		// Token: 0x04001766 RID: 5990
		private readonly XmlWriter _writer;

		// Token: 0x04001767 RID: 5991
		internal static readonly Encoding DefaultEncoding = Encoding.UTF8;

		// Token: 0x02000636 RID: 1590
		internal class ColumnMapDumper : ColumnMapVisitor<Dump>
		{
			// Token: 0x06003E85 RID: 16005 RVA: 0x0011E8CC File Offset: 0x0011CACC
			private ColumnMapDumper()
			{
			}

			// Token: 0x06003E86 RID: 16006 RVA: 0x0011E8D4 File Offset: 0x0011CAD4
			private void DumpCollection(CollectionColumnMap columnMap, Dump dumper)
			{
				if (columnMap.ForeignKeys.Length > 0)
				{
					using (new Dump.AutoXml(dumper, "foreignKeys"))
					{
						base.VisitList<SimpleColumnMap>(columnMap.ForeignKeys, dumper);
					}
				}
				if (columnMap.Keys.Length > 0)
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

			// Token: 0x06003E87 RID: 16007 RVA: 0x0011E99C File Offset: 0x0011CB9C
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

			// Token: 0x06003E88 RID: 16008 RVA: 0x0011E9C8 File Offset: 0x0011CBC8
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

			// Token: 0x06003E89 RID: 16009 RVA: 0x0011EA54 File Offset: 0x0011CC54
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

			// Token: 0x06003E8A RID: 16010 RVA: 0x0011EAE8 File Offset: 0x0011CCE8
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

			// Token: 0x06003E8B RID: 16011 RVA: 0x0011EB6C File Offset: 0x0011CD6C
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

			// Token: 0x06003E8C RID: 16012 RVA: 0x0011ECA8 File Offset: 0x0011CEA8
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

			// Token: 0x06003E8D RID: 16013 RVA: 0x0011EDE4 File Offset: 0x0011CFE4
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

			// Token: 0x06003E8E RID: 16014 RVA: 0x0011EE70 File Offset: 0x0011D070
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

			// Token: 0x06003E8F RID: 16015 RVA: 0x0011EEE4 File Offset: 0x0011D0E4
			internal override void Visit(SimpleCollectionColumnMap columnMap, Dump dumper)
			{
				using (new Dump.AutoXml(dumper, "SimpleCollection", Dump.ColumnMapDumper.GetAttributes(columnMap)))
				{
					this.DumpCollection(columnMap, dumper);
				}
			}

			// Token: 0x06003E90 RID: 16016 RVA: 0x0011EF2C File Offset: 0x0011D12C
			internal override void Visit(ScalarColumnMap columnMap, Dump dumper)
			{
				Dictionary<string, object> attributes = Dump.ColumnMapDumper.GetAttributes(columnMap);
				attributes.Add("CommandId", columnMap.CommandId);
				attributes.Add("ColumnPos", columnMap.ColumnPos);
				using (new Dump.AutoXml(dumper, "AssignedSimple", attributes))
				{
				}
			}

			// Token: 0x06003E91 RID: 16017 RVA: 0x0011EF9C File Offset: 0x0011D19C
			internal override void Visit(VarRefColumnMap columnMap, Dump dumper)
			{
				Dictionary<string, object> attributes = Dump.ColumnMapDumper.GetAttributes(columnMap);
				attributes.Add("Var", columnMap.Var.Id);
				using (new Dump.AutoXml(dumper, "VarRef", attributes))
				{
				}
			}

			// Token: 0x06003E92 RID: 16018 RVA: 0x0011EFF8 File Offset: 0x0011D1F8
			protected override void VisitEntityIdentity(DiscriminatedEntityIdentity entityIdentity, Dump dumper)
			{
				using (new Dump.AutoXml(dumper, "DiscriminatedEntityIdentity"))
				{
					using (new Dump.AutoXml(dumper, "entitySetId"))
					{
						entityIdentity.EntitySetColumnMap.Accept<Dump>(this, dumper);
					}
					if (entityIdentity.Keys.Length > 0)
					{
						using (new Dump.AutoXml(dumper, "keys"))
						{
							base.VisitList<SimpleColumnMap>(entityIdentity.Keys, dumper);
						}
					}
				}
			}

			// Token: 0x06003E93 RID: 16019 RVA: 0x0011F0A8 File Offset: 0x0011D2A8
			protected override void VisitEntityIdentity(SimpleEntityIdentity entityIdentity, Dump dumper)
			{
				using (new Dump.AutoXml(dumper, "SimpleEntityIdentity"))
				{
					if (entityIdentity.Keys.Length > 0)
					{
						using (new Dump.AutoXml(dumper, "keys"))
						{
							base.VisitList<SimpleColumnMap>(entityIdentity.Keys, dumper);
						}
					}
				}
			}

			// Token: 0x04001768 RID: 5992
			internal static Dump.ColumnMapDumper Instance = new Dump.ColumnMapDumper();
		}

		// Token: 0x02000637 RID: 1591
		internal struct AutoString : IDisposable
		{
			// Token: 0x06003E95 RID: 16021 RVA: 0x0011F130 File Offset: 0x0011D330
			internal AutoString(Dump dumper, Op op)
			{
				this._dumper = dumper;
				this._dumper.WriteString(Dump.AutoString.ToString(op.OpType));
				this._dumper.BeginExpression();
			}

			// Token: 0x06003E96 RID: 16022 RVA: 0x0011F15C File Offset: 0x0011D35C
			public void Dispose()
			{
				try
				{
					this._dumper.EndExpression();
				}
				catch (Exception e)
				{
					if (!e.IsCatchableExceptionType())
					{
						throw;
					}
				}
			}

			// Token: 0x06003E97 RID: 16023 RVA: 0x0011F194 File Offset: 0x0011D394
			[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
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
				case OpType.In:
					return "In";
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

			// Token: 0x04001769 RID: 5993
			private readonly Dump _dumper;
		}

		// Token: 0x02000638 RID: 1592
		internal struct AutoXml : IDisposable
		{
			// Token: 0x06003E98 RID: 16024 RVA: 0x0011F494 File Offset: 0x0011D694
			internal AutoXml(Dump dumper, Op op)
			{
				this._dumper = dumper;
				this._nodeName = Dump.AutoString.ToString(op.OpType);
				Dictionary<string, object> dictionary = new Dictionary<string, object>();
				if (op.Type != null)
				{
					dictionary.Add("Type", op.Type.ToString());
				}
				this._dumper.Begin(this._nodeName, dictionary);
			}

			// Token: 0x06003E99 RID: 16025 RVA: 0x0011F4F0 File Offset: 0x0011D6F0
			internal AutoXml(Dump dumper, Op op, Dictionary<string, object> attrs)
			{
				this._dumper = dumper;
				this._nodeName = Dump.AutoString.ToString(op.OpType);
				Dictionary<string, object> dictionary = new Dictionary<string, object>();
				if (op.Type != null)
				{
					dictionary.Add("Type", op.Type.ToString());
				}
				foreach (KeyValuePair<string, object> keyValuePair in attrs)
				{
					dictionary.Add(keyValuePair.Key, keyValuePair.Value);
				}
				this._dumper.Begin(this._nodeName, dictionary);
			}

			// Token: 0x06003E9A RID: 16026 RVA: 0x0011F59C File Offset: 0x0011D79C
			internal AutoXml(Dump dumper, string nodeName)
			{
				this = new Dump.AutoXml(dumper, nodeName, null);
			}

			// Token: 0x06003E9B RID: 16027 RVA: 0x0011F5A7 File Offset: 0x0011D7A7
			internal AutoXml(Dump dumper, string nodeName, Dictionary<string, object> attrs)
			{
				this._dumper = dumper;
				this._nodeName = nodeName;
				this._dumper.Begin(this._nodeName, attrs);
			}

			// Token: 0x06003E9C RID: 16028 RVA: 0x0011F5C9 File Offset: 0x0011D7C9
			public void Dispose()
			{
				this._dumper.End();
			}

			// Token: 0x0400176A RID: 5994
			private readonly string _nodeName;

			// Token: 0x0400176B RID: 5995
			private readonly Dump _dumper;
		}
	}
}
