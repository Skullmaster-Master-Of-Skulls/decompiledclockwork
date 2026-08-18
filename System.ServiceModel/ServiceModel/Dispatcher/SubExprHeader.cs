using System;
using System.Collections.Generic;
using System.Xml.XPath;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000501 RID: 1281
	internal class SubExprHeader : SubExpr
	{
		// Token: 0x06003086 RID: 12422 RVA: 0x000B9CED File Offset: 0x000B7EED
		internal SubExprHeader(Opcode ops, int var) : base(null, ops, var)
		{
			this.nameLookup = new Dictionary<string, Dictionary<string, List<SubExpr>>>();
			this.indexLookup = new Dictionary<SubExpr, SubExprHeader.MyInt>();
			base.IncRef();
		}

		// Token: 0x06003087 RID: 12423 RVA: 0x000B9D14 File Offset: 0x000B7F14
		internal override void AddChild(SubExpr expr)
		{
			base.AddChild(expr);
			this.RebuildIndex();
			if (expr.useSpecial)
			{
				NodeQName qname = ((SelectOpcode)expr.FirstOp).Criteria.QName;
				string @namespace = qname.Namespace;
				Dictionary<string, List<SubExpr>> dictionary;
				if (!this.nameLookup.TryGetValue(@namespace, out dictionary))
				{
					dictionary = new Dictionary<string, List<SubExpr>>();
					this.nameLookup.Add(@namespace, dictionary);
				}
				string name = qname.Name;
				List<SubExpr> list = new List<SubExpr>();
				if (!dictionary.TryGetValue(name, out list))
				{
					list = new List<SubExpr>();
					dictionary.Add(name, list);
				}
				list.Add(expr);
			}
		}

		// Token: 0x06003088 RID: 12424 RVA: 0x000B9DAC File Offset: 0x000B7FAC
		internal override void EvalSpecial(ProcessingContext context)
		{
			int counterMarker = context.Processor.CounterMarker;
			if (!context.LoadVariable(this.var))
			{
				XPathMessageContext.HeaderFun.InvokeInternal(context, 0);
				context.SaveVariable(this.var, context.Processor.ElapsedCount(counterMarker));
			}
			NodeSequence[] array = new NodeSequence[this.children.Count];
			NodeSequence sequence = context.Sequences[context.TopSequenceArg.basePtr].Sequence;
			for (int i = 0; i < this.children.Count; i++)
			{
				array[i] = context.CreateSequence();
				array[i].StartNodeset();
			}
			SeekableXPathNavigator navigator = sequence[0].GetNavigator();
			if (navigator.MoveToFirstChild())
			{
				do
				{
					if (navigator.NodeType == XPathNodeType.Element)
					{
						string localName = navigator.LocalName;
						string namespaceURI = navigator.NamespaceURI;
						Dictionary<string, List<SubExpr>> dictionary;
						List<SubExpr> list;
						if (this.nameLookup.TryGetValue(namespaceURI, out dictionary))
						{
							if (dictionary.TryGetValue(localName, out list))
							{
								for (int j = 0; j < list.Count; j++)
								{
									array[this.indexLookup[list[j]].i].Add(navigator);
								}
							}
							if (dictionary.TryGetValue(QueryDataModel.Wildcard, out list))
							{
								for (int k = 0; k < list.Count; k++)
								{
									array[this.indexLookup[list[k]].i].Add(navigator);
								}
							}
						}
						if (this.nameLookup.TryGetValue(QueryDataModel.Wildcard, out dictionary) && dictionary.TryGetValue(QueryDataModel.Wildcard, out list))
						{
							for (int l = 0; l < list.Count; l++)
							{
								array[this.indexLookup[list[l]].i].Add(navigator);
							}
						}
					}
				}
				while (navigator.MoveToNext());
			}
			int counterMarker2 = context.Processor.CounterMarker;
			for (int m = 0; m < this.children.Count; m++)
			{
				if (this.children[m].useSpecial)
				{
					array[m].StopNodeset();
					context.Processor.CounterMarker = counterMarker2;
					context.PushSequenceFrame();
					context.PushSequence(array[m]);
					for (Opcode opcode = this.children[m].FirstOp.Next; opcode != null; opcode = opcode.Eval(context))
					{
					}
					context.SaveVariable(this.children[m].var, context.Processor.ElapsedCount(counterMarker));
					context.PopSequenceFrame();
				}
				else
				{
					context.ReleaseSequence(array[m]);
				}
			}
			context.Processor.CounterMarker = counterMarker;
		}

		// Token: 0x06003089 RID: 12425 RVA: 0x000BA060 File Offset: 0x000B8260
		internal void RebuildIndex()
		{
			this.indexLookup.Clear();
			for (int i = 0; i < this.children.Count; i++)
			{
				this.indexLookup.Add(this.children[i], new SubExprHeader.MyInt(i));
			}
		}

		// Token: 0x0600308A RID: 12426 RVA: 0x000BA0AC File Offset: 0x000B82AC
		internal override void RemoveChild(SubExpr expr)
		{
			base.RemoveChild(expr);
			this.RebuildIndex();
			if (expr.useSpecial)
			{
				NodeQName qname = ((SelectOpcode)expr.FirstOp).Criteria.QName;
				string @namespace = qname.Namespace;
				Dictionary<string, List<SubExpr>> dictionary;
				if (this.nameLookup.TryGetValue(@namespace, out dictionary))
				{
					string name = qname.Name;
					List<SubExpr> list;
					if (dictionary.TryGetValue(name, out list))
					{
						list.Remove(expr);
						if (list.Count == 0)
						{
							dictionary.Remove(name);
						}
					}
					if (dictionary.Count == 0)
					{
						this.nameLookup.Remove(@namespace);
					}
				}
			}
		}

		// Token: 0x04002606 RID: 9734
		private Dictionary<string, Dictionary<string, List<SubExpr>>> nameLookup;

		// Token: 0x04002607 RID: 9735
		private Dictionary<SubExpr, SubExprHeader.MyInt> indexLookup;

		// Token: 0x02000C4A RID: 3146
		internal class MyInt
		{
			// Token: 0x06007773 RID: 30579 RVA: 0x001BDFAF File Offset: 0x001BC1AF
			internal MyInt(int i)
			{
				this.i = i;
			}

			// Token: 0x04004453 RID: 17491
			internal int i;
		}
	}
}
