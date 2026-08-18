using System;
using System.Collections.Generic;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x02000005 RID: 5
	public abstract class AstNode
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000015 RID: 21 RVA: 0x000024CC File Offset: 0x000006CC
		// (set) Token: 0x06000016 RID: 22 RVA: 0x000024D4 File Offset: 0x000006D4
		public AstNode Parent { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000024DD File Offset: 0x000006DD
		// (set) Token: 0x06000018 RID: 24 RVA: 0x000024E5 File Offset: 0x000006E5
		public Context Context { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000019 RID: 25 RVA: 0x000024EE File Offset: 0x000006EE
		// (set) Token: 0x0600001A RID: 26 RVA: 0x000024F6 File Offset: 0x000006F6
		public virtual Context TerminatingContext { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600001B RID: 27 RVA: 0x000024FF File Offset: 0x000006FF
		public virtual bool IsExpression
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002502 File Offset: 0x00000702
		public virtual bool IsConstant
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002505 File Offset: 0x00000705
		// (set) Token: 0x0600001E RID: 30 RVA: 0x0000250D File Offset: 0x0000070D
		public bool IsDebugOnly { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002516 File Offset: 0x00000716
		// (set) Token: 0x06000020 RID: 32 RVA: 0x0000251E File Offset: 0x0000071E
		public long Index { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002527 File Offset: 0x00000727
		public virtual OperatorPrecedence Precedence
		{
			get
			{
				return OperatorPrecedence.None;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000022 RID: 34 RVA: 0x0000252A File Offset: 0x0000072A
		public virtual bool IsDeclaration
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002530 File Offset: 0x00000730
		public bool IsWindowLookup
		{
			get
			{
				Lookup lookup = this as Lookup;
				return lookup != null && string.CompareOrdinal(lookup.Name, "window") == 0 && (lookup.VariableField == null || lookup.VariableField.FieldType == FieldType.Predefined);
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002573 File Offset: 0x00000773
		public virtual AstNode LeftHandSide
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002576 File Offset: 0x00000776
		// (set) Token: 0x06000026 RID: 38 RVA: 0x00002597 File Offset: 0x00000797
		public virtual ActivationObject EnclosingScope
		{
			get
			{
				ActivationObject result;
				if ((result = this.m_enclosingScope) == null)
				{
					if (this.Parent != null)
					{
						return this.Parent.EnclosingScope;
					}
					result = null;
				}
				return result;
			}
			set
			{
				this.m_enclosingScope = value;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000027 RID: 39 RVA: 0x000025A0 File Offset: 0x000007A0
		public bool HasOwnScope
		{
			get
			{
				return this.m_enclosingScope != null;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000028 RID: 40 RVA: 0x000025AE File Offset: 0x000007AE
		public virtual IEnumerable<AstNode> Children
		{
			get
			{
				return AstNode.s_emptyChildrenCollection;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000029 RID: 41 RVA: 0x000025B8 File Offset: 0x000007B8
		public virtual bool ContainsInOperator
		{
			get
			{
				foreach (AstNode astNode in this.Children)
				{
					if (astNode.ContainsInOperator)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002610 File Offset: 0x00000810
		protected AstNode(Context context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			this.Context = context;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000262D File Offset: 0x0000082D
		internal virtual string GetFunctionGuess(AstNode target)
		{
			return string.Empty;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002634 File Offset: 0x00000834
		internal virtual bool EncloseBlock(EncloseBlockType type)
		{
			return false;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002637 File Offset: 0x00000837
		public virtual PrimitiveType FindPrimitiveType()
		{
			return PrimitiveType.Other;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x0000263A File Offset: 0x0000083A
		public virtual bool ReplaceChild(AstNode oldNode, AstNode newNode)
		{
			return false;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x0000263D File Offset: 0x0000083D
		public virtual bool IsEquivalentTo(AstNode otherNode)
		{
			return false;
		}

		// Token: 0x06000030 RID: 48
		public abstract void Accept(IVisitor visitor);

		// Token: 0x06000031 RID: 49 RVA: 0x00002640 File Offset: 0x00000840
		public void UpdateWith(Context context)
		{
			if (context != null)
			{
				if (this.Context == null)
				{
					this.Context = context;
					return;
				}
				this.Context.UpdateWith(context);
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002664 File Offset: 0x00000864
		public static Block ForceToBlock(AstNode node)
		{
			Block block = node as Block;
			if (block == null && node != null)
			{
				block = new Block(node.Context.Clone());
				block.Append(node);
			}
			return block;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000027BC File Offset: 0x000009BC
		internal static IEnumerable<AstNode> EnumerateNonNullNodes<T>(IList<T> nodes) where T : AstNode
		{
			for (int ndx = 0; ndx < nodes.Count; ndx++)
			{
				if (nodes[ndx] != null)
				{
					yield return nodes[ndx];
				}
			}
			yield break;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000027DC File Offset: 0x000009DC
		internal static IEnumerable<AstNode> EnumerateNonNullNodes(AstNode n1, AstNode n2 = null, AstNode n3 = null, AstNode n4 = null)
		{
			return AstNode.EnumerateNonNullNodes<AstNode>(new AstNode[]
			{
				n1,
				n2,
				n3,
				n4
			});
		}

		// Token: 0x04000009 RID: 9
		private static readonly IEnumerable<AstNode> s_emptyChildrenCollection = new AstNode[0];

		// Token: 0x0400000A RID: 10
		private ActivationObject m_enclosingScope;
	}
}
