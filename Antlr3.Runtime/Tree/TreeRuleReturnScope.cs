using System;

namespace Antlr.Runtime.Tree
{
	// Token: 0x0200003C RID: 60
	[Serializable]
	public class TreeRuleReturnScope<TTree> : IRuleReturnScope<TTree>, IRuleReturnScope
	{
		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600028F RID: 655 RVA: 0x00007A99 File Offset: 0x00005C99
		// (set) Token: 0x06000290 RID: 656 RVA: 0x00007AA1 File Offset: 0x00005CA1
		public TTree Start
		{
			get
			{
				return this._start;
			}
			set
			{
				this._start = value;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000291 RID: 657 RVA: 0x00007AAA File Offset: 0x00005CAA
		object IRuleReturnScope.Start
		{
			get
			{
				return this.Start;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000292 RID: 658 RVA: 0x00007AB8 File Offset: 0x00005CB8
		TTree IRuleReturnScope<!0>.Stop
		{
			get
			{
				return default(TTree);
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000293 RID: 659 RVA: 0x00007AD0 File Offset: 0x00005CD0
		object IRuleReturnScope.Stop
		{
			get
			{
				return default(TTree);
			}
		}

		// Token: 0x0400008D RID: 141
		private TTree _start;
	}
}
