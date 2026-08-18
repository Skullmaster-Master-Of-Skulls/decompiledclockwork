using System;
using System.Text;
using Ionic.Zip;

namespace Ionic
{
	// Token: 0x0200001F RID: 31
	internal class CompoundCriterion : SelectionCriterion
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00002F4B File Offset: 0x0000114B
		// (set) Token: 0x06000085 RID: 133 RVA: 0x00002F53 File Offset: 0x00001153
		internal SelectionCriterion Right
		{
			get
			{
				return this._Right;
			}
			set
			{
				this._Right = value;
				if (value == null)
				{
					this.Conjunction = LogicalConjunction.NONE;
					return;
				}
				if (this.Conjunction == LogicalConjunction.NONE)
				{
					this.Conjunction = LogicalConjunction.AND;
				}
			}
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00002F78 File Offset: 0x00001178
		internal override bool Evaluate(string filename)
		{
			bool flag = this.Left.Evaluate(filename);
			switch (this.Conjunction)
			{
			case LogicalConjunction.AND:
				if (flag)
				{
					flag = this.Right.Evaluate(filename);
				}
				break;
			case LogicalConjunction.OR:
				if (!flag)
				{
					flag = this.Right.Evaluate(filename);
				}
				break;
			case LogicalConjunction.XOR:
				flag ^= this.Right.Evaluate(filename);
				break;
			default:
				throw new ArgumentException("Conjunction");
			}
			return flag;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00002FF0 File Offset: 0x000011F0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("(").Append((this.Left != null) ? this.Left.ToString() : "null").Append(" ").Append(this.Conjunction.ToString()).Append(" ").Append((this.Right != null) ? this.Right.ToString() : "null").Append(")");
			return stringBuilder.ToString();
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003088 File Offset: 0x00001288
		internal override bool Evaluate(ZipEntry entry)
		{
			bool flag = this.Left.Evaluate(entry);
			switch (this.Conjunction)
			{
			case LogicalConjunction.AND:
				if (flag)
				{
					flag = this.Right.Evaluate(entry);
				}
				break;
			case LogicalConjunction.OR:
				if (!flag)
				{
					flag = this.Right.Evaluate(entry);
				}
				break;
			case LogicalConjunction.XOR:
				flag ^= this.Right.Evaluate(entry);
				break;
			}
			return flag;
		}

		// Token: 0x0400004E RID: 78
		internal LogicalConjunction Conjunction;

		// Token: 0x0400004F RID: 79
		internal SelectionCriterion Left;

		// Token: 0x04000050 RID: 80
		private SelectionCriterion _Right;
	}
}
