using System;
using System.Collections;

namespace System.Security.Policy
{
	// Token: 0x020004B2 RID: 1202
	internal sealed class CodeGroupStack
	{
		// Token: 0x06002FD4 RID: 12244 RVA: 0x000A3EB5 File Offset: 0x000A2EB5
		internal CodeGroupStack()
		{
			this.m_array = new ArrayList();
		}

		// Token: 0x06002FD5 RID: 12245 RVA: 0x000A3EC8 File Offset: 0x000A2EC8
		internal void Push(CodeGroupStackFrame element)
		{
			this.m_array.Add(element);
		}

		// Token: 0x06002FD6 RID: 12246 RVA: 0x000A3ED8 File Offset: 0x000A2ED8
		internal CodeGroupStackFrame Pop()
		{
			if (this.IsEmpty())
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EmptyStack"));
			}
			int count = this.m_array.Count;
			CodeGroupStackFrame result = (CodeGroupStackFrame)this.m_array[count - 1];
			this.m_array.RemoveAt(count - 1);
			return result;
		}

		// Token: 0x06002FD7 RID: 12247 RVA: 0x000A3F2C File Offset: 0x000A2F2C
		internal bool IsEmpty()
		{
			return this.m_array.Count == 0;
		}

		// Token: 0x0400184F RID: 6223
		private ArrayList m_array;
	}
}
