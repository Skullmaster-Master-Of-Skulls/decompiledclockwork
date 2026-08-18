using System;
using System.Runtime.InteropServices;

namespace System.Collections.Generic
{
	// Token: 0x020003C3 RID: 963
	[ComVisible(false)]
	[__DynamicallyInvokable]
	public sealed class LinkedListNode<T>
	{
		// Token: 0x0600244C RID: 9292 RVA: 0x000A9F7D File Offset: 0x000A817D
		[__DynamicallyInvokable]
		public LinkedListNode(T value)
		{
			this.item = value;
		}

		// Token: 0x0600244D RID: 9293 RVA: 0x000A9F8C File Offset: 0x000A818C
		internal LinkedListNode(LinkedList<T> list, T value)
		{
			this.list = list;
			this.item = value;
		}

		// Token: 0x17000929 RID: 2345
		// (get) Token: 0x0600244E RID: 9294 RVA: 0x000A9FA2 File Offset: 0x000A81A2
		[__DynamicallyInvokable]
		public LinkedList<T> List
		{
			[__DynamicallyInvokable]
			get
			{
				return this.list;
			}
		}

		// Token: 0x1700092A RID: 2346
		// (get) Token: 0x0600244F RID: 9295 RVA: 0x000A9FAA File Offset: 0x000A81AA
		[__DynamicallyInvokable]
		public LinkedListNode<T> Next
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.next != null && this.next != this.list.head)
				{
					return this.next;
				}
				return null;
			}
		}

		// Token: 0x1700092B RID: 2347
		// (get) Token: 0x06002450 RID: 9296 RVA: 0x000A9FCF File Offset: 0x000A81CF
		[__DynamicallyInvokable]
		public LinkedListNode<T> Previous
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.prev != null && this != this.list.head)
				{
					return this.prev;
				}
				return null;
			}
		}

		// Token: 0x1700092C RID: 2348
		// (get) Token: 0x06002451 RID: 9297 RVA: 0x000A9FEF File Offset: 0x000A81EF
		// (set) Token: 0x06002452 RID: 9298 RVA: 0x000A9FF7 File Offset: 0x000A81F7
		[__DynamicallyInvokable]
		public T Value
		{
			[__DynamicallyInvokable]
			get
			{
				return this.item;
			}
			[__DynamicallyInvokable]
			set
			{
				this.item = value;
			}
		}

		// Token: 0x06002453 RID: 9299 RVA: 0x000AA000 File Offset: 0x000A8200
		internal void Invalidate()
		{
			this.list = null;
			this.next = null;
			this.prev = null;
		}

		// Token: 0x0400200F RID: 8207
		internal LinkedList<T> list;

		// Token: 0x04002010 RID: 8208
		internal LinkedListNode<T> next;

		// Token: 0x04002011 RID: 8209
		internal LinkedListNode<T> prev;

		// Token: 0x04002012 RID: 8210
		internal T item;
	}
}
