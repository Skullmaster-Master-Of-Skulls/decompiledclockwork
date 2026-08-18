using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000489 RID: 1161
	internal struct BranchMatcher
	{
		// Token: 0x06002CEF RID: 11503 RVA: 0x000AF1B4 File Offset: 0x000AD3B4
		internal BranchMatcher(int resultCount, QueryBranchResultSet resultTable)
		{
			this.resultCount = resultCount;
			this.resultTable = resultTable;
		}

		// Token: 0x17000ACA RID: 2762
		// (get) Token: 0x06002CF0 RID: 11504 RVA: 0x000AF1C4 File Offset: 0x000AD3C4
		internal QueryBranchResultSet ResultTable
		{
			get
			{
				return this.resultTable;
			}
		}

		// Token: 0x06002CF1 RID: 11505 RVA: 0x000AF1CC File Offset: 0x000AD3CC
		private void InitResults(ProcessingContext context)
		{
			context.PushFrame();
			context.Push(false, this.resultCount);
		}

		// Token: 0x06002CF2 RID: 11506 RVA: 0x000AF1E4 File Offset: 0x000AD3E4
		internal void InvokeMatches(ProcessingContext context)
		{
			int count = this.resultTable.Count;
			if (count != 0)
			{
				if (count != 1)
				{
					this.InvokeMultiMatch(context);
					return;
				}
				this.InvokeSingleMatch(context);
			}
		}

		// Token: 0x06002CF3 RID: 11507 RVA: 0x000AF214 File Offset: 0x000AD414
		private void InvokeMultiMatch(ProcessingContext context)
		{
			int counterMarker = context.Processor.CounterMarker;
			BranchContext branchContext = new BranchContext(context);
			int count = this.resultTable.Count;
			int i = 0;
			while (i < count)
			{
				QueryBranchResult queryBranchResult = this.resultTable[i];
				QueryBranch branch = queryBranchResult.Branch;
				Opcode next = branch.Branch.Next;
				ProcessingContext processingContext;
				if (next.TestFlag(OpcodeFlags.NoContextCopy))
				{
					processingContext = context;
				}
				else
				{
					processingContext = branchContext.Create();
				}
				this.InitResults(processingContext);
				processingContext.Values[processingContext.TopArg[queryBranchResult.ValIndex]].Boolean = true;
				while (++i < count)
				{
					queryBranchResult = this.resultTable[i];
					if (branch.ID != queryBranchResult.Branch.ID)
					{
						break;
					}
					processingContext.Values[processingContext.TopArg[queryBranchResult.ValIndex]].Boolean = true;
				}
				try
				{
					processingContext.EvalCodeBlock(next);
				}
				catch (XPathNavigatorException ex)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(ex.Process(next));
				}
				catch (NavigatorInvalidBodyAccessException ex2)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(ex2.Process(next));
				}
				context.Processor.CounterMarker = counterMarker;
			}
			branchContext.Release();
		}

		// Token: 0x06002CF4 RID: 11508 RVA: 0x000AF380 File Offset: 0x000AD580
		internal void InvokeNonMatches(ProcessingContext context, QueryBranchTable nonMatchTable)
		{
			int counterMarker = context.Processor.CounterMarker;
			BranchContext branchContext = new BranchContext(context);
			int i = 0;
			int j = 0;
			while (j < this.resultTable.Count)
			{
				if (i >= nonMatchTable.Count)
				{
					break;
				}
				int num = this.resultTable[j].Branch.ID - nonMatchTable[i].ID;
				if (num > 0)
				{
					ProcessingContext context2 = branchContext.Create();
					this.InvokeNonMatch(context2, nonMatchTable[i]);
					context.Processor.CounterMarker = counterMarker;
					i++;
				}
				else if (num == 0)
				{
					i++;
				}
				else
				{
					j++;
				}
			}
			while (i < nonMatchTable.Count)
			{
				ProcessingContext context3 = branchContext.Create();
				this.InvokeNonMatch(context3, nonMatchTable[i]);
				context.Processor.CounterMarker = counterMarker;
				i++;
			}
			branchContext.Release();
		}

		// Token: 0x06002CF5 RID: 11509 RVA: 0x000AF460 File Offset: 0x000AD660
		private void InvokeNonMatch(ProcessingContext context, QueryBranch branch)
		{
			context.PushFrame();
			context.Push(false, this.resultCount);
			try
			{
				context.EvalCodeBlock(branch.Branch);
			}
			catch (XPathNavigatorException ex)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(ex.Process(branch.Branch));
			}
			catch (NavigatorInvalidBodyAccessException ex2)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(ex2.Process(branch.Branch));
			}
		}

		// Token: 0x06002CF6 RID: 11510 RVA: 0x000AF4DC File Offset: 0x000AD6DC
		private void InvokeSingleMatch(ProcessingContext context)
		{
			int counterMarker = context.Processor.CounterMarker;
			QueryBranchResult queryBranchResult = this.resultTable[0];
			this.InitResults(context);
			context.Values[context.TopArg[queryBranchResult.ValIndex]].Boolean = true;
			try
			{
				context.EvalCodeBlock(queryBranchResult.Branch.Branch.Next);
			}
			catch (XPathNavigatorException ex)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(ex.Process(queryBranchResult.Branch.Branch.Next));
			}
			catch (NavigatorInvalidBodyAccessException ex2)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(ex2.Process(queryBranchResult.Branch.Branch.Next));
			}
			context.Processor.CounterMarker = counterMarker;
		}

		// Token: 0x06002CF7 RID: 11511 RVA: 0x000AF5B8 File Offset: 0x000AD7B8
		internal void Release(ProcessingContext context)
		{
			context.Processor.ReleaseResults(this.resultTable);
		}

		// Token: 0x0400245A RID: 9306
		private int resultCount;

		// Token: 0x0400245B RID: 9307
		private QueryBranchResultSet resultTable;
	}
}
