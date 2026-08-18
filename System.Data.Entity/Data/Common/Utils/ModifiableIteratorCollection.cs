using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.Common.Utils
{
	// Token: 0x02000397 RID: 919
	internal class ModifiableIteratorCollection<TElement> : InternalBase
	{
		// Token: 0x060032E9 RID: 13033 RVA: 0x000C6D0A File Offset: 0x000C4F0A
		internal ModifiableIteratorCollection(IEnumerable<TElement> elements)
		{
			this.m_elements = new List<TElement>(elements);
			this.m_currentIteratorIndex = -1;
		}

		// Token: 0x170009F6 RID: 2550
		// (get) Token: 0x060032EA RID: 13034 RVA: 0x000C6D25 File Offset: 0x000C4F25
		internal bool IsEmpty
		{
			get
			{
				return this.m_elements.Count == 0;
			}
		}

		// Token: 0x060032EB RID: 13035 RVA: 0x000C6D35 File Offset: 0x000C4F35
		internal TElement RemoveOneElement()
		{
			return this.Remove(this.m_elements.Count - 1);
		}

		// Token: 0x060032EC RID: 13036 RVA: 0x000C6D4A File Offset: 0x000C4F4A
		internal void ResetIterator()
		{
			this.m_currentIteratorIndex = -1;
		}

		// Token: 0x060032ED RID: 13037 RVA: 0x000C6D53 File Offset: 0x000C4F53
		internal void RemoveCurrentOfIterator()
		{
			this.Remove(this.m_currentIteratorIndex);
			this.m_currentIteratorIndex--;
		}

		// Token: 0x060032EE RID: 13038 RVA: 0x000C6D70 File Offset: 0x000C4F70
		internal IEnumerable<TElement> Elements()
		{
			this.m_currentIteratorIndex = 0;
			while (this.m_currentIteratorIndex < this.m_elements.Count)
			{
				yield return this.m_elements[this.m_currentIteratorIndex];
				this.m_currentIteratorIndex++;
			}
			yield break;
		}

		// Token: 0x060032EF RID: 13039 RVA: 0x000C6D80 File Offset: 0x000C4F80
		internal override void ToCompactString(StringBuilder builder)
		{
			StringUtil.ToCommaSeparatedString(builder, this.m_elements);
		}

		// Token: 0x060032F0 RID: 13040 RVA: 0x000C6D90 File Offset: 0x000C4F90
		private TElement Remove(int index)
		{
			TElement result = this.m_elements[index];
			int index2 = this.m_elements.Count - 1;
			this.m_elements[index] = this.m_elements[index2];
			this.m_elements.RemoveAt(index2);
			return result;
		}

		// Token: 0x04001663 RID: 5731
		private List<TElement> m_elements;

		// Token: 0x04001664 RID: 5732
		private int m_currentIteratorIndex;
	}
}
