using System;
using System.Collections.Generic;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020004D2 RID: 1234
	internal abstract class Opcode
	{
		// Token: 0x06002EBB RID: 11963 RVA: 0x000B5364 File Offset: 0x000B3564
		internal Opcode(OpcodeID id)
		{
			this.opcodeID = id;
			this.flags = OpcodeFlags.Single;
		}

		// Token: 0x17000B18 RID: 2840
		// (get) Token: 0x06002EBC RID: 11964 RVA: 0x000B537A File Offset: 0x000B357A
		// (set) Token: 0x06002EBD RID: 11965 RVA: 0x000B5382 File Offset: 0x000B3582
		internal OpcodeFlags Flags
		{
			get
			{
				return this.flags;
			}
			set
			{
				this.flags = value;
			}
		}

		// Token: 0x17000B19 RID: 2841
		// (get) Token: 0x06002EBE RID: 11966 RVA: 0x000B538B File Offset: 0x000B358B
		internal OpcodeID ID
		{
			get
			{
				return this.opcodeID;
			}
		}

		// Token: 0x17000B1A RID: 2842
		// (get) Token: 0x06002EBF RID: 11967 RVA: 0x000B5393 File Offset: 0x000B3593
		// (set) Token: 0x06002EC0 RID: 11968 RVA: 0x000B539B File Offset: 0x000B359B
		internal Opcode Next
		{
			get
			{
				return this.next;
			}
			set
			{
				this.next = value;
			}
		}

		// Token: 0x17000B1B RID: 2843
		// (get) Token: 0x06002EC1 RID: 11969 RVA: 0x000B53A4 File Offset: 0x000B35A4
		// (set) Token: 0x06002EC2 RID: 11970 RVA: 0x000B53AC File Offset: 0x000B35AC
		internal Opcode Prev
		{
			get
			{
				return this.prev;
			}
			set
			{
				this.prev = value;
			}
		}

		// Token: 0x06002EC3 RID: 11971 RVA: 0x000B53B5 File Offset: 0x000B35B5
		internal virtual void Add(Opcode op)
		{
			this.prev.AddBranch(op);
		}

		// Token: 0x06002EC4 RID: 11972 RVA: 0x000B53C4 File Offset: 0x000B35C4
		internal virtual void AddBranch(Opcode opcode)
		{
			Opcode opcode2 = this.next;
			if (this.TestFlag(OpcodeFlags.InConditional))
			{
				this.DelinkFromConditional(opcode2);
			}
			BranchOpcode branchOpcode = new BranchOpcode();
			this.next = null;
			this.Attach(branchOpcode);
			if (opcode2 != null)
			{
				branchOpcode.Add(opcode2);
			}
			branchOpcode.Add(opcode);
		}

		// Token: 0x06002EC5 RID: 11973 RVA: 0x000B5411 File Offset: 0x000B3611
		internal void Attach(Opcode op)
		{
			this.next = op;
			op.prev = this;
		}

		// Token: 0x06002EC6 RID: 11974 RVA: 0x000B5421 File Offset: 0x000B3621
		internal virtual void CollectXPathFilters(ICollection<MessageFilter> filters)
		{
			if (this.next != null)
			{
				this.next.CollectXPathFilters(filters);
			}
		}

		// Token: 0x06002EC7 RID: 11975 RVA: 0x000B5437 File Offset: 0x000B3637
		internal virtual bool IsEquivalentForAdd(Opcode opcode)
		{
			return this.ID == opcode.ID;
		}

		// Token: 0x06002EC8 RID: 11976 RVA: 0x000B5447 File Offset: 0x000B3647
		internal bool IsMultipleResult()
		{
			return (this.flags & (OpcodeFlags)10) == (OpcodeFlags)10;
		}

		// Token: 0x06002EC9 RID: 11977 RVA: 0x000B5456 File Offset: 0x000B3656
		internal virtual void DelinkFromConditional(Opcode child)
		{
			if (this.TestFlag(OpcodeFlags.InConditional))
			{
				((QueryConditionalBranchOpcode)this.prev).RemoveAlwaysBranch(child);
			}
		}

		// Token: 0x06002ECA RID: 11978 RVA: 0x000B5478 File Offset: 0x000B3678
		internal Opcode DetachChild()
		{
			Opcode opcode = this.next;
			if (opcode != null && this.IsInConditional())
			{
				this.DelinkFromConditional(opcode);
			}
			this.next = null;
			opcode.prev = null;
			return opcode;
		}

		// Token: 0x06002ECB RID: 11979 RVA: 0x000B54B0 File Offset: 0x000B36B0
		internal void DetachFromParent()
		{
			Opcode opcode = this.prev;
			if (opcode != null)
			{
				opcode.DetachChild();
			}
		}

		// Token: 0x06002ECC RID: 11980 RVA: 0x000B54CE File Offset: 0x000B36CE
		internal virtual bool Equals(Opcode op)
		{
			return op.ID == this.ID;
		}

		// Token: 0x06002ECD RID: 11981 RVA: 0x000B54DE File Offset: 0x000B36DE
		internal virtual Opcode Eval(ProcessingContext context)
		{
			return this.next;
		}

		// Token: 0x06002ECE RID: 11982 RVA: 0x000B54E6 File Offset: 0x000B36E6
		internal virtual Opcode Eval(NodeSequence sequence, SeekableXPathNavigator node)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperCritical(new QueryProcessingException(QueryProcessingError.Unexpected));
		}

		// Token: 0x06002ECF RID: 11983 RVA: 0x000B54F8 File Offset: 0x000B36F8
		internal virtual Opcode EvalSpecial(ProcessingContext context)
		{
			return this.Eval(context);
		}

		// Token: 0x06002ED0 RID: 11984 RVA: 0x000B5501 File Offset: 0x000B3701
		internal virtual bool IsInConditional()
		{
			return this.TestFlag(OpcodeFlags.InConditional);
		}

		// Token: 0x06002ED1 RID: 11985 RVA: 0x000B550E File Offset: 0x000B370E
		internal bool IsReachableFromConditional()
		{
			return this.prev != null && this.prev.IsInConditional();
		}

		// Token: 0x06002ED2 RID: 11986 RVA: 0x000B5525 File Offset: 0x000B3725
		internal virtual Opcode Locate(Opcode opcode)
		{
			if (this.next != null && this.next.Equals(opcode))
			{
				return this.next;
			}
			return null;
		}

		// Token: 0x06002ED3 RID: 11987 RVA: 0x000B5545 File Offset: 0x000B3745
		internal virtual void LinkToConditional(Opcode child)
		{
			if (this.TestFlag(OpcodeFlags.InConditional))
			{
				((QueryConditionalBranchOpcode)this.prev).AddAlwaysBranch(this, child);
			}
		}

		// Token: 0x06002ED4 RID: 11988 RVA: 0x000B5568 File Offset: 0x000B3768
		internal virtual void Remove()
		{
			if (this.next == null)
			{
				Opcode opcode = this.prev;
				if (opcode != null)
				{
					opcode.RemoveChild(this);
					opcode.Remove();
				}
			}
		}

		// Token: 0x06002ED5 RID: 11989 RVA: 0x000B5594 File Offset: 0x000B3794
		internal virtual void RemoveChild(Opcode opcode)
		{
			if (this.IsInConditional())
			{
				this.DelinkFromConditional(opcode);
			}
			opcode.prev = null;
			this.next = null;
			opcode.Flags |= OpcodeFlags.Deleted;
		}

		// Token: 0x06002ED6 RID: 11990 RVA: 0x000B55C8 File Offset: 0x000B37C8
		internal virtual void Replace(Opcode replace, Opcode with)
		{
			if (this.next == replace)
			{
				bool flag = this.IsInConditional();
				if (flag)
				{
					this.DelinkFromConditional(this.next);
				}
				this.next.prev = null;
				this.next = with;
				with.prev = this;
				if (flag)
				{
					this.LinkToConditional(with);
				}
			}
		}

		// Token: 0x06002ED7 RID: 11991 RVA: 0x000B5618 File Offset: 0x000B3818
		internal bool TestFlag(OpcodeFlags flag)
		{
			return (this.flags & flag) > OpcodeFlags.None;
		}

		// Token: 0x06002ED8 RID: 11992 RVA: 0x000B5625 File Offset: 0x000B3825
		internal virtual void Trim()
		{
			if (this.next != null)
			{
				this.next.Trim();
			}
		}

		// Token: 0x040025A4 RID: 9636
		protected OpcodeFlags flags;

		// Token: 0x040025A5 RID: 9637
		protected Opcode next;

		// Token: 0x040025A6 RID: 9638
		private OpcodeID opcodeID;

		// Token: 0x040025A7 RID: 9639
		protected Opcode prev;
	}
}
