using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Razor.Generator;
using System.Web.Razor.Resources;
using System.Web.Razor.Text;

namespace System.Web.Razor.Parser.SyntaxTree
{
	// Token: 0x0200008A RID: 138
	public class Block : SyntaxTreeNode
	{
		// Token: 0x060005B9 RID: 1465 RVA: 0x000164B4 File Offset: 0x000146B4
		public Block(BlockBuilder source)
		{
			if (source.Type == null)
			{
				throw new InvalidOperationException(RazorResources.Block_Type_Not_Specified);
			}
			this.Type = source.Type.Value;
			this.Children = source.Children;
			this.Name = source.Name;
			this.CodeGenerator = source.CodeGenerator;
			source.Reset();
			foreach (SyntaxTreeNode syntaxTreeNode in this.Children)
			{
				syntaxTreeNode.Parent = this;
			}
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x00016560 File Offset: 0x00014760
		internal Block(BlockType type, IEnumerable<SyntaxTreeNode> contents, IBlockCodeGenerator generator)
		{
			this.Type = type;
			this.CodeGenerator = generator;
			this.Children = contents;
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060005BB RID: 1467 RVA: 0x0001657D File Offset: 0x0001477D
		// (set) Token: 0x060005BC RID: 1468 RVA: 0x00016585 File Offset: 0x00014785
		public BlockType Type { get; private set; }

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060005BD RID: 1469 RVA: 0x0001658E File Offset: 0x0001478E
		// (set) Token: 0x060005BE RID: 1470 RVA: 0x00016596 File Offset: 0x00014796
		public IEnumerable<SyntaxTreeNode> Children { get; private set; }

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060005BF RID: 1471 RVA: 0x0001659F File Offset: 0x0001479F
		// (set) Token: 0x060005C0 RID: 1472 RVA: 0x000165A7 File Offset: 0x000147A7
		public string Name { get; private set; }

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060005C1 RID: 1473 RVA: 0x000165B0 File Offset: 0x000147B0
		// (set) Token: 0x060005C2 RID: 1474 RVA: 0x000165B8 File Offset: 0x000147B8
		public IBlockCodeGenerator CodeGenerator { get; private set; }

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060005C3 RID: 1475 RVA: 0x000165C1 File Offset: 0x000147C1
		public override bool IsBlock
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060005C4 RID: 1476 RVA: 0x000165C4 File Offset: 0x000147C4
		public override SourceLocation Start
		{
			get
			{
				SyntaxTreeNode syntaxTreeNode = this.Children.FirstOrDefault<SyntaxTreeNode>();
				if (syntaxTreeNode == null)
				{
					return SourceLocation.Zero;
				}
				return syntaxTreeNode.Start;
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060005C5 RID: 1477 RVA: 0x000165F4 File Offset: 0x000147F4
		public override int Length
		{
			get
			{
				return this.Children.Sum((SyntaxTreeNode child) => child.Length);
			}
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x00016620 File Offset: 0x00014820
		public Span FindFirstDescendentSpan()
		{
			SyntaxTreeNode syntaxTreeNode = this;
			while (syntaxTreeNode != null && syntaxTreeNode.IsBlock)
			{
				syntaxTreeNode = ((Block)syntaxTreeNode).Children.FirstOrDefault<SyntaxTreeNode>();
			}
			return syntaxTreeNode as Span;
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x00016654 File Offset: 0x00014854
		public Span FindLastDescendentSpan()
		{
			SyntaxTreeNode syntaxTreeNode = this;
			while (syntaxTreeNode != null && syntaxTreeNode.IsBlock)
			{
				syntaxTreeNode = ((Block)syntaxTreeNode).Children.LastOrDefault<SyntaxTreeNode>();
			}
			return syntaxTreeNode as Span;
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x00016687 File Offset: 0x00014887
		public override void Accept(ParserVisitor visitor)
		{
			visitor.VisitBlock(this);
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x00016690 File Offset: 0x00014890
		public override string ToString()
		{
			return string.Format(CultureInfo.CurrentCulture, "{0} Block at {1}::{2} (Gen:{3})", new object[]
			{
				this.Type,
				this.Start,
				this.Length,
				this.CodeGenerator
			});
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x000166E8 File Offset: 0x000148E8
		public override bool Equals(object obj)
		{
			Block block = obj as Block;
			return block != null && this.Type == block.Type && object.Equals(this.CodeGenerator, block.CodeGenerator) && Block.ChildrenEqual(this.Children, block.Children);
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x00016733 File Offset: 0x00014933
		public override int GetHashCode()
		{
			return (int)this.Type;
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x000169C4 File Offset: 0x00014BC4
		public IEnumerable<Span> Flatten()
		{
			foreach (SyntaxTreeNode element in this.Children)
			{
				Span span = element as Span;
				if (span != null)
				{
					yield return span;
				}
				else
				{
					Block block = element as Block;
					foreach (Span childSpan in block.Flatten())
					{
						yield return childSpan;
					}
				}
			}
			yield break;
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x000169E4 File Offset: 0x00014BE4
		public Span LocateOwner(TextChange change)
		{
			Span span = null;
			foreach (SyntaxTreeNode syntaxTreeNode in this.Children)
			{
				Span span2 = syntaxTreeNode as Span;
				if (span2 == null)
				{
					span = ((Block)syntaxTreeNode).LocateOwner(change);
				}
				else
				{
					if (change.OldPosition < span2.Start.AbsoluteIndex)
					{
						break;
					}
					span = (span2.EditHandler.OwnsChange(span2, change) ? span2 : span);
				}
				if (span != null)
				{
					break;
				}
			}
			return span;
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x00016A78 File Offset: 0x00014C78
		private static bool ChildrenEqual(IEnumerable<SyntaxTreeNode> left, IEnumerable<SyntaxTreeNode> right)
		{
			IEnumerator<SyntaxTreeNode> enumerator = left.GetEnumerator();
			IEnumerator<SyntaxTreeNode> enumerator2 = right.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (!enumerator2.MoveNext() || !object.Equals(enumerator.Current, enumerator2.Current))
				{
					return false;
				}
			}
			return !enumerator2.MoveNext();
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x00016AC8 File Offset: 0x00014CC8
		public override bool EquivalentTo(SyntaxTreeNode node)
		{
			Block block = node as Block;
			return block != null && block.Type == this.Type && this.Children.SequenceEqual(block.Children, new EquivalenceComparer());
		}
	}
}
