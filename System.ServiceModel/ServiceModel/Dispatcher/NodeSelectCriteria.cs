using System;
using System.Xml.XPath;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020004F4 RID: 1268
	internal class NodeSelectCriteria
	{
		// Token: 0x0600302A RID: 12330 RVA: 0x000B804F File Offset: 0x000B624F
		internal NodeSelectCriteria(QueryAxisType axis, NodeQName qname, QueryNodeType nodeType)
		{
			this.axis = QueryDataModel.GetAxis(axis);
			this.qname = qname;
			this.qnameType = qname.GetQNameType();
			this.type = nodeType;
		}

		// Token: 0x17000B70 RID: 2928
		// (get) Token: 0x0600302B RID: 12331 RVA: 0x000B807E File Offset: 0x000B627E
		internal QueryAxis Axis
		{
			get
			{
				return this.axis;
			}
		}

		// Token: 0x17000B71 RID: 2929
		// (get) Token: 0x0600302C RID: 12332 RVA: 0x000B8086 File Offset: 0x000B6286
		internal bool IsCompressable
		{
			get
			{
				return QueryAxisType.Self == this.axis.Type || QueryAxisType.Child == this.axis.Type;
			}
		}

		// Token: 0x17000B72 RID: 2930
		// (get) Token: 0x0600302D RID: 12333 RVA: 0x000B80A7 File Offset: 0x000B62A7
		internal NodeQName QName
		{
			get
			{
				return this.qname;
			}
		}

		// Token: 0x17000B73 RID: 2931
		// (get) Token: 0x0600302E RID: 12334 RVA: 0x000B80AF File Offset: 0x000B62AF
		internal QueryNodeType Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x0600302F RID: 12335 RVA: 0x000B80B7 File Offset: 0x000B62B7
		public bool Equals(NodeSelectCriteria criteria)
		{
			return this.axis.Type == criteria.axis.Type && this.type == criteria.type && this.qname.Equals(criteria.qname);
		}

		// Token: 0x06003030 RID: 12336 RVA: 0x000B80F4 File Offset: 0x000B62F4
		internal bool MatchType(SeekableXPathNavigator node)
		{
			QueryNodeType queryNodeType;
			switch (node.NodeType)
			{
			case XPathNodeType.Root:
				queryNodeType = QueryNodeType.Root;
				break;
			case XPathNodeType.Element:
				queryNodeType = QueryNodeType.Element;
				break;
			case XPathNodeType.Attribute:
				queryNodeType = QueryNodeType.Attribute;
				break;
			default:
				return false;
			case XPathNodeType.Text:
			case XPathNodeType.SignificantWhitespace:
			case XPathNodeType.Whitespace:
				queryNodeType = QueryNodeType.Text;
				break;
			case XPathNodeType.ProcessingInstruction:
				queryNodeType = QueryNodeType.Processing;
				break;
			case XPathNodeType.Comment:
				queryNodeType = QueryNodeType.Comment;
				break;
			}
			return queryNodeType == (this.type & queryNodeType);
		}

		// Token: 0x06003031 RID: 12337 RVA: 0x000B8158 File Offset: 0x000B6358
		internal bool MatchQName(SeekableXPathNavigator node)
		{
			NodeQNameType nodeQNameType = this.qnameType & NodeQNameType.Standard;
			if (nodeQNameType == NodeQNameType.Name)
			{
				return node.NamespaceURI.Length == 0 && this.qname.EqualsName(node.LocalName);
			}
			if (nodeQNameType != NodeQNameType.Standard)
			{
				if (this.qnameType == NodeQNameType.Empty)
				{
					return true;
				}
				NodeQNameType nodeQNameType2 = this.qnameType & NodeQNameType.Wildcard;
				if (nodeQNameType2 != NodeQNameType.NameWildcard)
				{
					return nodeQNameType2 == NodeQNameType.Wildcard;
				}
				return this.qname.EqualsNamespace(node.NamespaceURI);
			}
			else
			{
				string text = node.LocalName;
				if (this.qname.name.Length == text.Length && this.qname.name == text)
				{
					text = node.NamespaceURI;
					return this.qname.ns.Length == text.Length && this.qname.ns == text;
				}
				return false;
			}
		}

		// Token: 0x06003032 RID: 12338 RVA: 0x000B8238 File Offset: 0x000B6438
		internal void Select(SeekableXPathNavigator contextNode, NodeSequence destSequence)
		{
			QueryNodeType queryNodeType = this.type;
			switch (queryNodeType)
			{
			case QueryNodeType.Root:
				contextNode.MoveToRoot();
				destSequence.Add(contextNode);
				return;
			case QueryNodeType.Attribute:
				if (contextNode.MoveToFirstAttribute())
				{
					do
					{
						if (this.MatchQName(contextNode))
						{
							destSequence.Add(contextNode);
							if ((this.qnameType & NodeQNameType.Wildcard) == NodeQNameType.Empty)
							{
								return;
							}
						}
					}
					while (contextNode.MoveToNextAttribute());
					return;
				}
				return;
			case (QueryNodeType)3:
				break;
			case QueryNodeType.Element:
				if (QueryAxisType.Descendant == this.axis.Type)
				{
					this.SelectDescendants(contextNode, destSequence);
					return;
				}
				if (QueryAxisType.DescendantOrSelf == this.axis.Type)
				{
					destSequence.Add(contextNode);
					this.SelectDescendants(contextNode, destSequence);
					return;
				}
				if (contextNode.MoveToFirstChild())
				{
					do
					{
						if (XPathNodeType.Element == contextNode.NodeType && this.MatchQName(contextNode))
						{
							destSequence.Add(contextNode);
						}
					}
					while (contextNode.MoveToNext());
					return;
				}
				return;
			default:
				if (queryNodeType != QueryNodeType.Text)
				{
					if (queryNodeType == QueryNodeType.ChildNodes)
					{
						if (QueryAxisType.Descendant == this.axis.Type)
						{
							this.SelectDescendants(contextNode, destSequence);
							return;
						}
						if (contextNode.MoveToFirstChild())
						{
							do
							{
								if (this.MatchType(contextNode) && this.MatchQName(contextNode))
								{
									destSequence.Add(contextNode);
								}
							}
							while (contextNode.MoveToNext());
							return;
						}
						return;
					}
				}
				else
				{
					if (!contextNode.MoveToFirstChild())
					{
						return;
					}
					for (;;)
					{
						if (this.MatchType(contextNode))
						{
							destSequence.Add(contextNode);
						}
						if (!contextNode.MoveToNext())
						{
							return;
						}
					}
				}
				break;
			}
			if (QueryAxisType.Self == this.axis.Type)
			{
				if (this.MatchType(contextNode) && this.MatchQName(contextNode))
				{
					destSequence.Add(contextNode);
					return;
				}
			}
			else
			{
				if (QueryAxisType.Descendant == this.axis.Type)
				{
					this.SelectDescendants(contextNode, destSequence);
					return;
				}
				if (QueryAxisType.DescendantOrSelf == this.axis.Type)
				{
					destSequence.Add(contextNode);
					this.SelectDescendants(contextNode, destSequence);
					return;
				}
				if (QueryAxisType.Child == this.axis.Type)
				{
					if (contextNode.MoveToFirstChild())
					{
						do
						{
							if (this.MatchType(contextNode) && this.MatchQName(contextNode))
							{
								destSequence.Add(contextNode);
							}
						}
						while (contextNode.MoveToNext());
						return;
					}
				}
				else
				{
					if (QueryAxisType.Attribute != this.axis.Type)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperCritical(new QueryProcessingException(QueryProcessingError.Unexpected));
					}
					if (contextNode.MoveToFirstAttribute())
					{
						do
						{
							if (this.MatchType(contextNode) && this.MatchQName(contextNode))
							{
								destSequence.Add(contextNode);
								if ((this.qnameType & NodeQNameType.Wildcard) == NodeQNameType.Empty)
								{
									return;
								}
							}
						}
						while (contextNode.MoveToNextAttribute());
						return;
					}
				}
			}
		}

		// Token: 0x06003033 RID: 12339 RVA: 0x000B8474 File Offset: 0x000B6674
		internal Opcode Select(SeekableXPathNavigator contextNode, NodeSequence destSequence, SelectOpcode next)
		{
			Opcode result = next.Next;
			QueryNodeType queryNodeType = this.type;
			if (queryNodeType != QueryNodeType.Root)
			{
				if (queryNodeType != QueryNodeType.Element)
				{
					if (queryNodeType != QueryNodeType.ChildNodes)
					{
						if (QueryAxisType.Self != this.axis.Type)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperCritical(new QueryProcessingException(QueryProcessingError.Unexpected));
						}
						if (this.MatchType(contextNode) && this.MatchQName(contextNode))
						{
							long currentPosition = contextNode.CurrentPosition;
							result = next.Eval(destSequence, contextNode);
							contextNode.CurrentPosition = currentPosition;
						}
					}
					else if (contextNode.MoveToFirstChild())
					{
						do
						{
							if (this.MatchType(contextNode) && this.MatchQName(contextNode))
							{
								destSequence.Add(contextNode);
							}
						}
						while (contextNode.MoveToNext());
					}
				}
				else if (contextNode.MoveToFirstChild())
				{
					do
					{
						if (XPathNodeType.Element == contextNode.NodeType && this.MatchQName(contextNode))
						{
							long currentPosition2 = contextNode.CurrentPosition;
							result = next.Eval(destSequence, contextNode);
							contextNode.CurrentPosition = currentPosition2;
						}
					}
					while (contextNode.MoveToNext());
				}
			}
			else
			{
				contextNode.MoveToRoot();
				result = next.Eval(destSequence, contextNode);
			}
			return result;
		}

		// Token: 0x06003034 RID: 12340 RVA: 0x000B8570 File Offset: 0x000B6770
		private void SelectDescendants(SeekableXPathNavigator contextNode, NodeSequence destSequence)
		{
			int i = 1;
			if (!contextNode.MoveToFirstChild())
			{
				return;
			}
			while (i > 0)
			{
				if (this.MatchQName(contextNode))
				{
					destSequence.Add(contextNode);
				}
				if (contextNode.MoveToFirstChild())
				{
					i++;
				}
				else if (!contextNode.MoveToNext())
				{
					while (i > 0)
					{
						contextNode.MoveToParent();
						i--;
						if (contextNode.MoveToNext())
						{
							break;
						}
					}
				}
			}
		}

		// Token: 0x040025F1 RID: 9713
		protected QueryAxis axis;

		// Token: 0x040025F2 RID: 9714
		protected NodeQName qname;

		// Token: 0x040025F3 RID: 9715
		protected NodeQNameType qnameType;

		// Token: 0x040025F4 RID: 9716
		protected QueryNodeType type;
	}
}
