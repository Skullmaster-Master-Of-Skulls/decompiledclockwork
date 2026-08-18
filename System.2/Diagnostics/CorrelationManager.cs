using System;
using System.Collections;
using System.Runtime.Remoting.Messaging;

namespace System.Diagnostics
{
	// Token: 0x02000495 RID: 1173
	public class CorrelationManager
	{
		// Token: 0x06002B6B RID: 11115 RVA: 0x000C531E File Offset: 0x000C351E
		internal CorrelationManager()
		{
		}

		// Token: 0x17000A80 RID: 2688
		// (get) Token: 0x06002B6C RID: 11116 RVA: 0x000C5328 File Offset: 0x000C3528
		// (set) Token: 0x06002B6D RID: 11117 RVA: 0x000C534F File Offset: 0x000C354F
		public Guid ActivityId
		{
			get
			{
				object obj = CallContext.LogicalGetData("E2ETrace.ActivityID");
				if (obj != null)
				{
					return (Guid)obj;
				}
				return Guid.Empty;
			}
			set
			{
				CallContext.LogicalSetData("E2ETrace.ActivityID", value);
			}
		}

		// Token: 0x17000A81 RID: 2689
		// (get) Token: 0x06002B6E RID: 11118 RVA: 0x000C5361 File Offset: 0x000C3561
		public Stack LogicalOperationStack
		{
			get
			{
				return this.GetLogicalOperationStack();
			}
		}

		// Token: 0x06002B6F RID: 11119 RVA: 0x000C536C File Offset: 0x000C356C
		public void StartLogicalOperation(object operationId)
		{
			if (operationId == null)
			{
				throw new ArgumentNullException("operationId");
			}
			Stack logicalOperationStack = this.GetLogicalOperationStack();
			logicalOperationStack.Push(operationId);
		}

		// Token: 0x06002B70 RID: 11120 RVA: 0x000C5395 File Offset: 0x000C3595
		public void StartLogicalOperation()
		{
			this.StartLogicalOperation(Guid.NewGuid());
		}

		// Token: 0x06002B71 RID: 11121 RVA: 0x000C53A8 File Offset: 0x000C35A8
		public void StopLogicalOperation()
		{
			Stack logicalOperationStack = this.GetLogicalOperationStack();
			logicalOperationStack.Pop();
		}

		// Token: 0x06002B72 RID: 11122 RVA: 0x000C53C4 File Offset: 0x000C35C4
		private Stack GetLogicalOperationStack()
		{
			Stack stack = CallContext.LogicalGetData("System.Diagnostics.Trace.CorrelationManagerSlot") as Stack;
			if (stack == null)
			{
				stack = new Stack();
				CallContext.LogicalSetData("System.Diagnostics.Trace.CorrelationManagerSlot", stack);
			}
			return stack;
		}

		// Token: 0x0400268A RID: 9866
		private const string transactionSlotName = "System.Diagnostics.Trace.CorrelationManagerSlot";

		// Token: 0x0400268B RID: 9867
		private const string activityIdSlotName = "E2ETrace.ActivityID";
	}
}
