using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020004AC RID: 1196
	internal class NumberIntervalOpcode : NumberRelationOpcode
	{
		// Token: 0x06002DC9 RID: 11721 RVA: 0x000B2927 File Offset: 0x000B0B27
		internal NumberIntervalOpcode(double literal, RelationOperator op) : base(OpcodeID.NumberInterval, literal, op)
		{
		}

		// Token: 0x17000AE6 RID: 2790
		// (get) Token: 0x06002DCA RID: 11722 RVA: 0x000B2933 File Offset: 0x000B0B33
		internal override object Literal
		{
			get
			{
				if (this.interval == null)
				{
					this.interval = base.ToInterval();
				}
				return this.interval;
			}
		}

		// Token: 0x06002DCB RID: 11723 RVA: 0x000B2950 File Offset: 0x000B0B50
		internal override void Add(Opcode op)
		{
			NumberIntervalOpcode numberIntervalOpcode = op as NumberIntervalOpcode;
			if (numberIntervalOpcode == null)
			{
				base.Add(op);
				return;
			}
			NumberIntervalBranchOpcode numberIntervalBranchOpcode = new NumberIntervalBranchOpcode();
			this.prev.Replace(this, numberIntervalBranchOpcode);
			numberIntervalBranchOpcode.Add(this);
			numberIntervalBranchOpcode.Add(numberIntervalOpcode);
		}

		// Token: 0x040024E5 RID: 9445
		private Interval interval;
	}
}
