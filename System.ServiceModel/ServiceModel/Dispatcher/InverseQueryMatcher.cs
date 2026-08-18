using System;
using System.Collections.Generic;
using System.Xml;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020004B5 RID: 1205
	internal class InverseQueryMatcher : QueryMatcher
	{
		// Token: 0x06002E04 RID: 11780 RVA: 0x000B3810 File Offset: 0x000B1A10
		internal InverseQueryMatcher(bool match)
		{
			this.elim = new SubExprEliminator();
			this.lastLookup = new Dictionary<object, Opcode>();
			this.match = match;
		}

		// Token: 0x06002E05 RID: 11781 RVA: 0x000B3838 File Offset: 0x000B1A38
		internal void Add(string expression, XmlNamespaceManager names, object item, bool forceExternal)
		{
			bool flag = false;
			OpcodeBlock newBlock = default(OpcodeBlock);
			newBlock.Append(new NoOpOpcode(OpcodeID.QueryTree));
			if (!forceExternal)
			{
				try
				{
					ValueDataType valueDataType = ValueDataType.None;
					newBlock.Append(QueryMatcher.CompileForInternalEngine(expression, names, QueryCompilerFlags.InverseQuery, out valueDataType));
					MultipleResultOpcode multipleResultOpcode;
					if (!this.match)
					{
						multipleResultOpcode = new QueryMultipleResultOpcode();
					}
					else
					{
						multipleResultOpcode = new MatchMultipleResultOpcode();
					}
					multipleResultOpcode.AddItem(item);
					newBlock.Append(multipleResultOpcode);
					flag = true;
					newBlock = new OpcodeBlock(this.elim.Add(item, newBlock.First));
					this.subExprVars = this.elim.VariableCount;
				}
				catch (QueryCompileException)
				{
				}
			}
			if (!flag)
			{
				newBlock.Append(QueryMatcher.CompileForExternalEngine(expression, names, item, this.match));
			}
			QueryTreeBuilder queryTreeBuilder = new QueryTreeBuilder();
			this.query = queryTreeBuilder.Build(this.query, newBlock);
			this.lastLookup[item] = queryTreeBuilder.LastOpcode;
		}

		// Token: 0x06002E06 RID: 11782 RVA: 0x000B3924 File Offset: 0x000B1B24
		internal void Clear()
		{
			foreach (object obj in this.lastLookup.Keys)
			{
				this.Remove(this.lastLookup[obj], obj);
				this.elim.Remove(obj);
			}
			this.subExprVars = this.elim.VariableCount;
			this.lastLookup.Clear();
		}

		// Token: 0x06002E07 RID: 11783 RVA: 0x000B39B0 File Offset: 0x000B1BB0
		internal void Remove(object item)
		{
			this.Remove(this.lastLookup[item], item);
			this.lastLookup.Remove(item);
			this.elim.Remove(item);
			this.subExprVars = this.elim.VariableCount;
		}

		// Token: 0x06002E08 RID: 11784 RVA: 0x000B39F0 File Offset: 0x000B1BF0
		private void Remove(Opcode opcode, object item)
		{
			MultipleResultOpcode multipleResultOpcode = opcode as MultipleResultOpcode;
			if (multipleResultOpcode != null)
			{
				multipleResultOpcode.RemoveItem(item);
				return;
			}
			opcode.Remove();
		}

		// Token: 0x06002E09 RID: 11785 RVA: 0x000B3A15 File Offset: 0x000B1C15
		internal override void Trim()
		{
			base.Trim();
			this.elim.Trim();
		}

		// Token: 0x040024FD RID: 9469
		private SubExprEliminator elim;

		// Token: 0x040024FE RID: 9470
		private Dictionary<object, Opcode> lastLookup;

		// Token: 0x040024FF RID: 9471
		private bool match;
	}
}
