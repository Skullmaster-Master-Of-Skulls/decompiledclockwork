using System;
using System.Collections.Generic;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x0200048B RID: 1163
	internal class QueryConditionalBranchOpcode : Opcode
	{
		// Token: 0x06002D00 RID: 11520 RVA: 0x000AF5D3 File Offset: 0x000AD7D3
		internal QueryConditionalBranchOpcode(OpcodeID id, QueryBranchIndex branchIndex) : base(id)
		{
			this.flags |= OpcodeFlags.Branch;
			this.branchIndex = branchIndex;
			this.nextID = 0;
		}

		// Token: 0x17000ACD RID: 2765
		// (get) Token: 0x06002D01 RID: 11521 RVA: 0x000AF5F8 File Offset: 0x000AD7F8
		internal QueryBranchTable AlwaysBranches
		{
			get
			{
				if (this.alwaysBranches == null)
				{
					this.alwaysBranches = new QueryBranchTable();
				}
				return this.alwaysBranches;
			}
		}

		// Token: 0x06002D02 RID: 11522 RVA: 0x000AF614 File Offset: 0x000AD814
		internal override void Add(Opcode opcode)
		{
			LiteralRelationOpcode literalRelationOpcode = this.ValidateOpcode(opcode);
			if (literalRelationOpcode == null)
			{
				base.Add(opcode);
				return;
			}
			QueryBranch queryBranch = this.branchIndex[literalRelationOpcode.Literal];
			if (queryBranch == null)
			{
				this.nextID++;
				queryBranch = new QueryBranch(literalRelationOpcode, this.nextID);
				literalRelationOpcode.Prev = this;
				this.branchIndex[literalRelationOpcode.Literal] = queryBranch;
			}
			else
			{
				queryBranch.Branch.Next.Add(literalRelationOpcode.Next);
			}
			literalRelationOpcode.Flags |= OpcodeFlags.InConditional;
			this.AddAlwaysBranch(queryBranch, literalRelationOpcode.Next);
		}

		// Token: 0x06002D03 RID: 11523 RVA: 0x000AF6B4 File Offset: 0x000AD8B4
		internal void AddAlwaysBranch(Opcode literal, Opcode next)
		{
			LiteralRelationOpcode literalRelationOpcode = this.ValidateOpcode(literal);
			if (literalRelationOpcode != null)
			{
				this.AddAlwaysBranch(literalRelationOpcode, next);
			}
		}

		// Token: 0x06002D04 RID: 11524 RVA: 0x000AF6D4 File Offset: 0x000AD8D4
		internal void AddAlwaysBranch(LiteralRelationOpcode literal, Opcode next)
		{
			QueryBranch literalBranch = this.branchIndex[literal.Literal];
			this.AddAlwaysBranch(literalBranch, next);
		}

		// Token: 0x06002D05 RID: 11525 RVA: 0x000AF6FC File Offset: 0x000AD8FC
		private void AddAlwaysBranch(QueryBranch literalBranch, Opcode next)
		{
			if (OpcodeID.Branch == next.ID)
			{
				BranchOpcode branchOpcode = (BranchOpcode)next;
				OpcodeList branches = branchOpcode.Branches;
				for (int i = 0; i < branches.Count; i++)
				{
					Opcode opcode = branches[i];
					if (this.IsAlwaysBranch(opcode))
					{
						this.AlwaysBranches.AddInOrder(new QueryBranch(opcode, literalBranch.ID));
					}
					else
					{
						opcode.Flags |= OpcodeFlags.NoContextCopy;
					}
				}
				return;
			}
			if (this.IsAlwaysBranch(next))
			{
				this.AlwaysBranches.AddInOrder(new QueryBranch(next, literalBranch.ID));
				return;
			}
			next.Flags |= OpcodeFlags.NoContextCopy;
		}

		// Token: 0x06002D06 RID: 11526 RVA: 0x000AF7A1 File Offset: 0x000AD9A1
		internal virtual void CollectMatches(int valIndex, ref Value val, QueryBranchResultSet results)
		{
			this.branchIndex.Match(valIndex, ref val, results);
		}

		// Token: 0x06002D07 RID: 11527 RVA: 0x000AF7B4 File Offset: 0x000AD9B4
		internal override void CollectXPathFilters(ICollection<MessageFilter> filters)
		{
			if (this.alwaysBranches != null)
			{
				for (int i = 0; i < this.alwaysBranches.Count; i++)
				{
					this.alwaysBranches[i].Branch.CollectXPathFilters(filters);
				}
			}
			this.branchIndex.CollectXPathFilters(filters);
		}

		// Token: 0x06002D08 RID: 11528 RVA: 0x000AF804 File Offset: 0x000ADA04
		internal override Opcode Eval(ProcessingContext context)
		{
			StackFrame topArg = context.TopArg;
			int count = topArg.Count;
			if (count > 0)
			{
				QueryBranchResultSet queryBranchResultSet = context.Processor.CreateResultSet();
				BranchMatcher branchMatcher = new BranchMatcher(count, queryBranchResultSet);
				for (int i = 0; i < count; i++)
				{
					this.CollectMatches(i, ref context.Values[topArg[i]], queryBranchResultSet);
				}
				context.PopFrame();
				if (queryBranchResultSet.Count > 1)
				{
					queryBranchResultSet.Sort();
				}
				if (this.alwaysBranches != null && this.alwaysBranches.Count > 0)
				{
					branchMatcher.InvokeNonMatches(context, this.alwaysBranches);
				}
				branchMatcher.InvokeMatches(context);
				branchMatcher.Release(context);
			}
			else
			{
				context.PopFrame();
			}
			return this.next;
		}

		// Token: 0x06002D09 RID: 11529 RVA: 0x000AF8C0 File Offset: 0x000ADAC0
		internal QueryBranch GetBranch(Opcode op)
		{
			if (op.TestFlag(OpcodeFlags.Literal))
			{
				LiteralRelationOpcode literalRelationOpcode = this.ValidateOpcode(op);
				if (literalRelationOpcode != null)
				{
					QueryBranch queryBranch = this.branchIndex[literalRelationOpcode.Literal];
					if (queryBranch != null && queryBranch.Branch.ID == op.ID)
					{
						return queryBranch;
					}
				}
			}
			return null;
		}

		// Token: 0x06002D0A RID: 11530 RVA: 0x000AF910 File Offset: 0x000ADB10
		private bool IsAlwaysBranch(Opcode next)
		{
			JumpIfOpcode jumpIfOpcode = next as JumpIfOpcode;
			if (jumpIfOpcode != null)
			{
				if (!jumpIfOpcode.Test)
				{
					return true;
				}
				Opcode jump = jumpIfOpcode.Jump;
				if (jump == null)
				{
					return false;
				}
				Opcode next2;
				if (jump.TestFlag(OpcodeFlags.Branch))
				{
					OpcodeList branches = ((BranchOpcode)jump).Branches;
					for (int i = 0; i < branches.Count; i++)
					{
						next2 = branches[i].Next;
						if (next2 != null && !next2.TestFlag(OpcodeFlags.Result))
						{
							return true;
						}
					}
					return false;
				}
				next2 = jumpIfOpcode.Jump.Next;
				return next2 == null || !next2.TestFlag(OpcodeFlags.Result);
			}
			else
			{
				if (OpcodeID.BlockEnd == next.ID)
				{
					return !next.Next.TestFlag(OpcodeFlags.Result);
				}
				return !next.TestFlag(OpcodeFlags.Result);
			}
		}

		// Token: 0x06002D0B RID: 11531 RVA: 0x000AF9C5 File Offset: 0x000ADBC5
		internal override bool IsEquivalentForAdd(Opcode opcode)
		{
			return this.ValidateOpcode(opcode) != null || base.IsEquivalentForAdd(opcode);
		}

		// Token: 0x06002D0C RID: 11532 RVA: 0x000AF9DC File Offset: 0x000ADBDC
		internal override Opcode Locate(Opcode opcode)
		{
			QueryBranch branch = this.GetBranch(opcode);
			if (branch != null)
			{
				return branch.Branch;
			}
			return null;
		}

		// Token: 0x06002D0D RID: 11533 RVA: 0x000AF9FC File Offset: 0x000ADBFC
		internal override void Remove()
		{
			if (this.branchIndex == null || this.branchIndex.Count == 0)
			{
				base.Remove();
			}
		}

		// Token: 0x06002D0E RID: 11534 RVA: 0x000AFA1C File Offset: 0x000ADC1C
		internal override void RemoveChild(Opcode opcode)
		{
			LiteralRelationOpcode literalRelationOpcode = this.ValidateOpcode(opcode);
			QueryBranch queryBranch = this.branchIndex[literalRelationOpcode.Literal];
			this.branchIndex.Remove(literalRelationOpcode.Literal);
			queryBranch.Branch.Flags &= (OpcodeFlags)(-513);
			if (this.alwaysBranches != null)
			{
				int num = this.alwaysBranches.IndexOfID(queryBranch.ID);
				if (num >= 0)
				{
					this.alwaysBranches.RemoveAt(num);
					if (this.alwaysBranches.Count == 0)
					{
						this.alwaysBranches = null;
					}
				}
			}
		}

		// Token: 0x06002D0F RID: 11535 RVA: 0x000AFAAC File Offset: 0x000ADCAC
		internal void RemoveAlwaysBranch(Opcode opcode)
		{
			if (this.alwaysBranches == null)
			{
				return;
			}
			if (OpcodeID.Branch == opcode.ID)
			{
				OpcodeList branches = ((BranchOpcode)opcode).Branches;
				for (int i = 0; i < branches.Count; i++)
				{
					this.alwaysBranches.Remove(branches[i]);
				}
			}
			else
			{
				this.alwaysBranches.Remove(opcode);
			}
			if (this.alwaysBranches.Count == 0)
			{
				this.alwaysBranches = null;
			}
		}

		// Token: 0x06002D10 RID: 11536 RVA: 0x000AFB1E File Offset: 0x000ADD1E
		internal override void Replace(Opcode replace, Opcode with)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperCritical(new NotImplementedException(SR.GetString("FilterUnexpectedError")));
		}

		// Token: 0x06002D11 RID: 11537 RVA: 0x000AFB39 File Offset: 0x000ADD39
		internal override void Trim()
		{
			if (this.alwaysBranches != null)
			{
				this.alwaysBranches.Trim();
			}
			this.branchIndex.Trim();
		}

		// Token: 0x06002D12 RID: 11538 RVA: 0x000AFB59 File Offset: 0x000ADD59
		internal virtual LiteralRelationOpcode ValidateOpcode(Opcode opcode)
		{
			return opcode as LiteralRelationOpcode;
		}

		// Token: 0x0400245C RID: 9308
		private QueryBranchTable alwaysBranches;

		// Token: 0x0400245D RID: 9309
		private QueryBranchIndex branchIndex;

		// Token: 0x0400245E RID: 9310
		private int nextID;
	}
}
