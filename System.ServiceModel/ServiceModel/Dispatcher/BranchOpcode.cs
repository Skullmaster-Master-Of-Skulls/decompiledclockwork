using System;
using System.Collections.Generic;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000486 RID: 1158
	internal class BranchOpcode : Opcode
	{
		// Token: 0x06002CD3 RID: 11475 RVA: 0x000AED38 File Offset: 0x000ACF38
		internal BranchOpcode() : this(OpcodeID.Branch)
		{
		}

		// Token: 0x06002CD4 RID: 11476 RVA: 0x000AED41 File Offset: 0x000ACF41
		internal BranchOpcode(OpcodeID id) : base(id)
		{
			this.flags |= OpcodeFlags.Branch;
			this.branches = new OpcodeList(2);
		}

		// Token: 0x17000AC4 RID: 2756
		// (get) Token: 0x06002CD5 RID: 11477 RVA: 0x000AED64 File Offset: 0x000ACF64
		internal OpcodeList Branches
		{
			get
			{
				return this.branches;
			}
		}

		// Token: 0x06002CD6 RID: 11478 RVA: 0x000AED6C File Offset: 0x000ACF6C
		internal override void Add(Opcode opcode)
		{
			for (int i = 0; i < this.branches.Count; i++)
			{
				if (this.branches[i].IsEquivalentForAdd(opcode))
				{
					this.branches[i].Add(opcode);
					return;
				}
			}
			this.AddBranch(opcode);
		}

		// Token: 0x06002CD7 RID: 11479 RVA: 0x000AEDBD File Offset: 0x000ACFBD
		internal override void AddBranch(Opcode opcode)
		{
			this.branches.Add(opcode);
			opcode.Prev = this;
			if (this.IsInConditional())
			{
				this.LinkToConditional(opcode);
			}
		}

		// Token: 0x06002CD8 RID: 11480 RVA: 0x000AEDE4 File Offset: 0x000ACFE4
		internal override void CollectXPathFilters(ICollection<MessageFilter> filters)
		{
			for (int i = 0; i < this.branches.Count; i++)
			{
				this.branches[i].CollectXPathFilters(filters);
			}
		}

		// Token: 0x06002CD9 RID: 11481 RVA: 0x000AEE19 File Offset: 0x000AD019
		internal override void DelinkFromConditional(Opcode child)
		{
			if (this.prev != null)
			{
				this.prev.DelinkFromConditional(child);
			}
		}

		// Token: 0x06002CDA RID: 11482 RVA: 0x000AEE30 File Offset: 0x000AD030
		internal override Opcode Eval(ProcessingContext context)
		{
			QueryProcessor processor = context.Processor;
			SeekableXPathNavigator contextNode = processor.ContextNode;
			int counterMarker = processor.CounterMarker;
			long currentPosition = contextNode.CurrentPosition;
			int i = 0;
			int num = this.branches.Count;
			try
			{
				if (context.StacksInUse)
				{
					if (--num > 0)
					{
						BranchContext branchContext = new BranchContext(context);
						while (i < num)
						{
							Opcode opcode = this.branches[i];
							if ((opcode.Flags & OpcodeFlags.Fx) != OpcodeFlags.None)
							{
								opcode.Eval(context);
							}
							else
							{
								ProcessingContext context2 = branchContext.Create();
								while (opcode != null)
								{
									opcode = opcode.Eval(context2);
								}
							}
							contextNode.CurrentPosition = currentPosition;
							processor.CounterMarker = counterMarker;
							i++;
						}
						branchContext.Release();
					}
					for (Opcode opcode = this.branches[i]; opcode != null; opcode = opcode.Eval(context))
					{
					}
				}
				else
				{
					int nodeCount = context.NodeCount;
					while (i < num)
					{
						for (Opcode opcode = this.branches[i]; opcode != null; opcode = opcode.Eval(context))
						{
						}
						context.ClearContext();
						context.NodeCount = nodeCount;
						contextNode.CurrentPosition = currentPosition;
						processor.CounterMarker = counterMarker;
						i++;
					}
				}
			}
			catch (XPathNavigatorException ex)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(ex.Process(this.branches[i]));
			}
			catch (NavigatorInvalidBodyAccessException ex2)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(ex2.Process(this.branches[i]));
			}
			processor.CounterMarker = counterMarker;
			return this.next;
		}

		// Token: 0x06002CDB RID: 11483 RVA: 0x000AEFCC File Offset: 0x000AD1CC
		internal override bool IsInConditional()
		{
			return this.prev == null || this.prev.IsInConditional();
		}

		// Token: 0x06002CDC RID: 11484 RVA: 0x000AEFE3 File Offset: 0x000AD1E3
		internal override void LinkToConditional(Opcode child)
		{
			if (this.prev != null)
			{
				this.prev.LinkToConditional(child);
			}
		}

		// Token: 0x06002CDD RID: 11485 RVA: 0x000AEFFC File Offset: 0x000AD1FC
		internal override Opcode Locate(Opcode opcode)
		{
			int i = 0;
			int count = this.branches.Count;
			while (i < count)
			{
				Opcode opcode2 = this.branches[i];
				if (opcode2.TestFlag(OpcodeFlags.Branch))
				{
					Opcode opcode3 = opcode2.Locate(opcode);
					if (opcode3 != null)
					{
						return opcode3;
					}
				}
				else if (opcode2.Equals(opcode))
				{
					return opcode2;
				}
				i++;
			}
			return null;
		}

		// Token: 0x06002CDE RID: 11486 RVA: 0x000AF050 File Offset: 0x000AD250
		internal override void Remove()
		{
			if (this.branches.Count == 0)
			{
				base.Remove();
			}
		}

		// Token: 0x06002CDF RID: 11487 RVA: 0x000AF065 File Offset: 0x000AD265
		internal override void RemoveChild(Opcode opcode)
		{
			if (this.IsInConditional())
			{
				this.DelinkFromConditional(opcode);
			}
			this.branches.Remove(opcode);
			this.branches.Trim();
		}

		// Token: 0x06002CE0 RID: 11488 RVA: 0x000AF090 File Offset: 0x000AD290
		internal override void Replace(Opcode replace, Opcode with)
		{
			int num = this.branches.IndexOf(replace);
			if (num >= 0)
			{
				replace.Prev = null;
				this.branches[num] = with;
				with.Prev = this;
			}
		}

		// Token: 0x06002CE1 RID: 11489 RVA: 0x000AF0CC File Offset: 0x000AD2CC
		internal override void Trim()
		{
			this.branches.Trim();
			for (int i = 0; i < this.branches.Count; i++)
			{
				this.branches[i].Trim();
			}
		}

		// Token: 0x04002454 RID: 9300
		private OpcodeList branches;
	}
}
