using System;
using System.Collections.Generic;

namespace System.Linq
{
	// Token: 0x02000162 RID: 354
	internal sealed class SingleLinkedNode<TSource>
	{
		// Token: 0x06000C36 RID: 3126 RVA: 0x0002D3CF File Offset: 0x0002B5CF
		public SingleLinkedNode(TSource item)
		{
			this.Item = item;
		}

		// Token: 0x06000C37 RID: 3127 RVA: 0x0002D3DE File Offset: 0x0002B5DE
		private SingleLinkedNode(SingleLinkedNode<TSource> linked, TSource item)
		{
			this.Linked = linked;
			this.Item = item;
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000C38 RID: 3128 RVA: 0x0002D3F4 File Offset: 0x0002B5F4
		public TSource Item { get; }

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000C39 RID: 3129 RVA: 0x0002D3FC File Offset: 0x0002B5FC
		public SingleLinkedNode<TSource> Linked { get; }

		// Token: 0x06000C3A RID: 3130 RVA: 0x0002D404 File Offset: 0x0002B604
		public SingleLinkedNode<TSource> Add(TSource item)
		{
			return new SingleLinkedNode<TSource>(this, item);
		}

		// Token: 0x06000C3B RID: 3131 RVA: 0x0002D410 File Offset: 0x0002B610
		public int GetCount()
		{
			int num = 0;
			for (SingleLinkedNode<TSource> singleLinkedNode = this; singleLinkedNode != null; singleLinkedNode = singleLinkedNode.Linked)
			{
				num++;
			}
			return num;
		}

		// Token: 0x06000C3C RID: 3132 RVA: 0x0002D432 File Offset: 0x0002B632
		public IEnumerator<TSource> GetEnumerator(int count)
		{
			return this.ToArray(count).GetEnumerator();
		}

		// Token: 0x06000C3D RID: 3133 RVA: 0x0002D440 File Offset: 0x0002B640
		public SingleLinkedNode<TSource> GetNode(int index)
		{
			SingleLinkedNode<TSource> singleLinkedNode = this;
			while (index > 0)
			{
				singleLinkedNode = singleLinkedNode.Linked;
				index--;
			}
			return singleLinkedNode;
		}

		// Token: 0x06000C3E RID: 3134 RVA: 0x0002D464 File Offset: 0x0002B664
		public TSource[] ToArray(int count)
		{
			TSource[] array = new TSource[count];
			int num = count;
			for (SingleLinkedNode<TSource> singleLinkedNode = this; singleLinkedNode != null; singleLinkedNode = singleLinkedNode.Linked)
			{
				num--;
				array[num] = singleLinkedNode.Item;
			}
			return array;
		}
	}
}
