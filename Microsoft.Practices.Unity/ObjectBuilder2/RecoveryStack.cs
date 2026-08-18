using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Practices.Unity.Utility;

namespace Microsoft.Practices.ObjectBuilder2
{
	// Token: 0x02000044 RID: 68
	[SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix", Justification = "The name ends in stack because the semantics are a stack, and we want that to be obvious to users")]
	public class RecoveryStack : IRecoveryStack
	{
		// Token: 0x06000130 RID: 304 RVA: 0x00004258 File Offset: 0x00002458
		public void Add(IRequiresRecovery recovery)
		{
			Guard.ArgumentNotNull(recovery, "recovery");
			lock (this.lockObj)
			{
				this.recoveries.Push(recovery);
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000131 RID: 305 RVA: 0x000042AC File Offset: 0x000024AC
		public int Count
		{
			get
			{
				int count;
				lock (this.lockObj)
				{
					count = this.recoveries.Count;
				}
				return count;
			}
		}

		// Token: 0x06000132 RID: 306 RVA: 0x000042F4 File Offset: 0x000024F4
		public void ExecuteRecovery()
		{
			while (this.recoveries.Count > 0)
			{
				this.recoveries.Pop().Recover();
			}
		}

		// Token: 0x0400003A RID: 58
		private Stack<IRequiresRecovery> recoveries = new Stack<IRequiresRecovery>();

		// Token: 0x0400003B RID: 59
		private object lockObj = new object();
	}
}
