using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x02000066 RID: 102
	public sealed class AstNodeList : AstNode, IEnumerable<AstNode>, IEnumerable
	{
		// Token: 0x17000175 RID: 373
		// (get) Token: 0x0600069C RID: 1692 RVA: 0x00021118 File Offset: 0x0001F318
		public override Context TerminatingContext
		{
			get
			{
				Context terminatingContext;
				if ((terminatingContext = base.TerminatingContext) == null)
				{
					if (this.m_list.Count <= 0)
					{
						return null;
					}
					terminatingContext = this.m_list[this.m_list.Count - 1].TerminatingContext;
				}
				return terminatingContext;
			}
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x00021151 File Offset: 0x0001F351
		public AstNodeList(Context context) : base(context)
		{
			this.m_list = new List<AstNode>();
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x00021165 File Offset: 0x0001F365
		public override void Accept(IVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x0600069F RID: 1695 RVA: 0x00021171 File Offset: 0x0001F371
		public override OperatorPrecedence Precedence
		{
			get
			{
				return OperatorPrecedence.Comma;
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060006A0 RID: 1696 RVA: 0x00021174 File Offset: 0x0001F374
		public int Count
		{
			get
			{
				return this.m_list.Count;
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060006A1 RID: 1697 RVA: 0x00021181 File Offset: 0x0001F381
		public override IEnumerable<AstNode> Children
		{
			get
			{
				return AstNode.EnumerateNonNullNodes<AstNode>(this.m_list);
			}
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x00021190 File Offset: 0x0001F390
		public void ForEach<TItem>(Action<TItem> action) where TItem : AstNode
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			foreach (AstNode astNode in this.m_list)
			{
				TItem titem = astNode as TItem;
				if (titem != null)
				{
					action(titem);
				}
			}
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x00021230 File Offset: 0x0001F430
		public override bool ReplaceChild(AstNode oldNode, AstNode newNode)
		{
			for (int i = 0; i < this.m_list.Count; i++)
			{
				if (this.m_list[i] == oldNode)
				{
					oldNode.IfNotNull((AstNode n) => n.Parent = ((n.Parent == this) ? null : n.Parent));
					if (newNode == null)
					{
						this.m_list.RemoveAt(i);
					}
					else
					{
						this.m_list[i] = newNode;
						newNode.Parent = this;
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x000212A4 File Offset: 0x0001F4A4
		public override bool IsEquivalentTo(AstNode otherNode)
		{
			bool result = false;
			AstNodeList astNodeList = otherNode as AstNodeList;
			if (astNodeList != null && this.m_list.Count == astNodeList.Count)
			{
				result = true;
				for (int i = 0; i < this.m_list.Count; i++)
				{
					if (!this.m_list[i].IsEquivalentTo(astNodeList[i]))
					{
						result = false;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x00021308 File Offset: 0x0001F508
		internal AstNodeList Append(AstNode node)
		{
			AstNodeList astNodeList = node as AstNodeList;
			if (astNodeList != null)
			{
				for (int i = 0; i < astNodeList.Count; i++)
				{
					this.Append(astNodeList[i]);
				}
			}
			else if (node != null)
			{
				node.Parent = this;
				this.m_list.Add(node);
				base.Context.UpdateWith(node.Context);
			}
			return this;
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x0002136C File Offset: 0x0001F56C
		public AstNodeList Insert(int position, AstNode node)
		{
			AstNodeList astNodeList = node as AstNodeList;
			if (astNodeList != null)
			{
				for (int i = 0; i < astNodeList.Count; i++)
				{
					this.Insert(position + i, astNodeList[i]);
				}
			}
			else if (node != null)
			{
				node.Parent = this;
				this.m_list.Insert(position, node);
				base.Context.UpdateWith(node.Context);
			}
			return this;
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x000213FC File Offset: 0x0001F5FC
		internal void RemoveAt(int position)
		{
			this.m_list[position].IfNotNull((AstNode n) => n.Parent = ((n.Parent == this) ? null : n.Parent));
			this.m_list.RemoveAt(position);
		}

		// Token: 0x17000179 RID: 377
		public AstNode this[int index]
		{
			get
			{
				return this.m_list[index];
			}
			set
			{
				this.m_list[index].IfNotNull((AstNode n) => n.Parent = ((n.Parent == this) ? null : n.Parent));
				if (value != null)
				{
					this.m_list[index] = value;
					this.m_list[index].Parent = this;
					return;
				}
				this.m_list.RemoveAt(index);
			}
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x000214BC File Offset: 0x0001F6BC
		public bool IsSingleConstantArgument(string argumentValue)
		{
			if (this.m_list.Count == 1)
			{
				ConstantWrapper constantWrapper = this.m_list[0] as ConstantWrapper;
				if (constantWrapper != null && string.CompareOrdinal(argumentValue, constantWrapper.Value.ToString()) == 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060006AB RID: 1707 RVA: 0x00021504 File Offset: 0x0001F704
		public string SingleConstantArgument
		{
			get
			{
				string result = null;
				if (this.m_list.Count == 1)
				{
					ConstantWrapper constantWrapper = this.m_list[0] as ConstantWrapper;
					if (constantWrapper != null)
					{
						result = constantWrapper.ToString();
					}
				}
				return result;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060006AC RID: 1708 RVA: 0x00021540 File Offset: 0x0001F740
		public override bool IsConstant
		{
			get
			{
				foreach (AstNode astNode in this.m_list)
				{
					if (astNode != null && !astNode.IsConstant)
					{
						return false;
					}
				}
				return true;
			}
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x000215A0 File Offset: 0x0001F7A0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.m_list.Count > 0)
			{
				stringBuilder.Append(this.m_list[0].ToString());
				for (int i = 1; i < this.m_list.Count; i++)
				{
					stringBuilder.Append(" , ");
					stringBuilder.Append(this.m_list[i].ToString());
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x00021619 File Offset: 0x0001F819
		public IEnumerator<AstNode> GetEnumerator()
		{
			return this.m_list.GetEnumerator();
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x0002162B File Offset: 0x0001F82B
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.m_list.GetEnumerator();
		}

		// Token: 0x04000264 RID: 612
		private List<AstNode> m_list;
	}
}
