using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000485 RID: 1157
	internal class QueryBranchTable
	{
		// Token: 0x06002CC7 RID: 11463 RVA: 0x000AEAD9 File Offset: 0x000ACCD9
		internal QueryBranchTable() : this(1)
		{
		}

		// Token: 0x06002CC8 RID: 11464 RVA: 0x000AEAE2 File Offset: 0x000ACCE2
		internal QueryBranchTable(int capacity)
		{
			this.branches = new QueryBranch[capacity];
		}

		// Token: 0x17000AC2 RID: 2754
		// (get) Token: 0x06002CC9 RID: 11465 RVA: 0x000AEAF6 File Offset: 0x000ACCF6
		internal int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x17000AC3 RID: 2755
		internal QueryBranch this[int index]
		{
			get
			{
				return this.branches[index];
			}
		}

		// Token: 0x06002CCB RID: 11467 RVA: 0x000AEB08 File Offset: 0x000ACD08
		internal void AddInOrder(QueryBranch branch)
		{
			int num = 0;
			while (num < this.count && this.branches[num].ID < branch.ID)
			{
				num++;
			}
			this.InsertAt(num, branch);
		}

		// Token: 0x06002CCC RID: 11468 RVA: 0x000AEB44 File Offset: 0x000ACD44
		private void Grow()
		{
			QueryBranch[] destinationArray = new QueryBranch[this.branches.Length + 1];
			Array.Copy(this.branches, destinationArray, this.branches.Length);
			this.branches = destinationArray;
		}

		// Token: 0x06002CCD RID: 11469 RVA: 0x000AEB7C File Offset: 0x000ACD7C
		public int IndexOf(Opcode opcode)
		{
			for (int i = 0; i < this.count; i++)
			{
				if (opcode == this.branches[i].Branch)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06002CCE RID: 11470 RVA: 0x000AEBB0 File Offset: 0x000ACDB0
		public int IndexOfID(int id)
		{
			for (int i = 0; i < this.count; i++)
			{
				if (this.branches[i].ID == id)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06002CCF RID: 11471 RVA: 0x000AEBE4 File Offset: 0x000ACDE4
		internal void InsertAt(int index, QueryBranch branch)
		{
			if (this.count == this.branches.Length)
			{
				this.Grow();
			}
			if (index < this.count)
			{
				Array.Copy(this.branches, index, this.branches, index + 1, this.count - index);
			}
			this.branches[index] = branch;
			this.count++;
		}

		// Token: 0x06002CD0 RID: 11472 RVA: 0x000AEC44 File Offset: 0x000ACE44
		internal bool Remove(Opcode branch)
		{
			int num = this.IndexOf(branch);
			if (num >= 0)
			{
				this.RemoveAt(num);
				return true;
			}
			return false;
		}

		// Token: 0x06002CD1 RID: 11473 RVA: 0x000AEC68 File Offset: 0x000ACE68
		internal void RemoveAt(int index)
		{
			if (index < this.count - 1)
			{
				Array.Copy(this.branches, index + 1, this.branches, index, this.count - index - 1);
			}
			else
			{
				this.branches[index] = null;
			}
			this.count--;
		}

		// Token: 0x06002CD2 RID: 11474 RVA: 0x000AECB8 File Offset: 0x000ACEB8
		internal void Trim()
		{
			if (this.count < this.branches.Length)
			{
				QueryBranch[] destinationArray = new QueryBranch[this.count];
				Array.Copy(this.branches, destinationArray, this.count);
				this.branches = destinationArray;
			}
			for (int i = 0; i < this.branches.Length; i++)
			{
				if (this.branches[i] != null && this.branches[i].Branch != null)
				{
					this.branches[i].Branch.Trim();
				}
			}
		}

		// Token: 0x04002452 RID: 9298
		private int count;

		// Token: 0x04002453 RID: 9299
		private QueryBranch[] branches;
	}
}
