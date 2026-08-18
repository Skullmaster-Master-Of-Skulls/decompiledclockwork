using System;
using System.Collections;
using System.Security.Permissions;

namespace System.ComponentModel.Design.Serialization
{
	// Token: 0x02000604 RID: 1540
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public sealed class ContextStack
	{
		// Token: 0x17000D8B RID: 3467
		// (get) Token: 0x060038A2 RID: 14498 RVA: 0x000F1D25 File Offset: 0x000EFF25
		public object Current
		{
			get
			{
				if (this.contextStack != null && this.contextStack.Count > 0)
				{
					return this.contextStack[this.contextStack.Count - 1];
				}
				return null;
			}
		}

		// Token: 0x17000D8C RID: 3468
		public object this[int level]
		{
			get
			{
				if (level < 0)
				{
					throw new ArgumentOutOfRangeException("level");
				}
				if (this.contextStack != null && level < this.contextStack.Count)
				{
					return this.contextStack[this.contextStack.Count - 1 - level];
				}
				return null;
			}
		}

		// Token: 0x17000D8D RID: 3469
		public object this[Type type]
		{
			get
			{
				if (type == null)
				{
					throw new ArgumentNullException("type");
				}
				if (this.contextStack != null)
				{
					int i = this.contextStack.Count;
					while (i > 0)
					{
						object obj = this.contextStack[--i];
						if (type.IsInstanceOfType(obj))
						{
							return obj;
						}
					}
				}
				return null;
			}
		}

		// Token: 0x060038A5 RID: 14501 RVA: 0x000F1E00 File Offset: 0x000F0000
		public void Append(object context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			if (this.contextStack == null)
			{
				this.contextStack = new ArrayList();
			}
			this.contextStack.Insert(0, context);
		}

		// Token: 0x060038A6 RID: 14502 RVA: 0x000F1E30 File Offset: 0x000F0030
		public object Pop()
		{
			object result = null;
			if (this.contextStack != null && this.contextStack.Count > 0)
			{
				int index = this.contextStack.Count - 1;
				result = this.contextStack[index];
				this.contextStack.RemoveAt(index);
			}
			return result;
		}

		// Token: 0x060038A7 RID: 14503 RVA: 0x000F1E7D File Offset: 0x000F007D
		public void Push(object context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			if (this.contextStack == null)
			{
				this.contextStack = new ArrayList();
			}
			this.contextStack.Add(context);
		}

		// Token: 0x04002B77 RID: 11127
		private ArrayList contextStack;
	}
}
