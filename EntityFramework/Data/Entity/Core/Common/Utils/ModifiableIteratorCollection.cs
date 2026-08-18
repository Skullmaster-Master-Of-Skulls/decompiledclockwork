using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.Entity.Core.Common.Utils
{
	// Token: 0x0200032D RID: 813
	internal class ModifiableIteratorCollection<TElement> : InternalBase
	{
		// Token: 0x06001C20 RID: 7200 RVA: 0x0008AAA2 File Offset: 0x00088CA2
		internal ModifiableIteratorCollection(IEnumerable<TElement> elements)
		{
			this.m_elements = new List<TElement>(elements);
			this.m_currentIteratorIndex = -1;
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06001C21 RID: 7201 RVA: 0x0008AABD File Offset: 0x00088CBD
		internal bool IsEmpty
		{
			get
			{
				return this.m_elements.Count == 0;
			}
		}

		// Token: 0x06001C22 RID: 7202 RVA: 0x0008AACD File Offset: 0x00088CCD
		internal TElement RemoveOneElement()
		{
			return this.Remove(this.m_elements.Count - 1);
		}

		// Token: 0x06001C23 RID: 7203 RVA: 0x0008AAE2 File Offset: 0x00088CE2
		internal void ResetIterator()
		{
			this.m_currentIteratorIndex = -1;
		}

		// Token: 0x06001C24 RID: 7204 RVA: 0x0008AAEB File Offset: 0x00088CEB
		internal void RemoveCurrentOfIterator()
		{
			this.Remove(this.m_currentIteratorIndex);
			this.m_currentIteratorIndex--;
		}

		// Token: 0x06001C25 RID: 7205 RVA: 0x0008AC28 File Offset: 0x00088E28
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

		// Token: 0x06001C26 RID: 7206 RVA: 0x0008AC45 File Offset: 0x00088E45
		internal override void ToCompactString(StringBuilder builder)
		{
			StringUtil.ToCommaSeparatedString(builder, this.m_elements);
		}

		// Token: 0x06001C27 RID: 7207 RVA: 0x0008AC54 File Offset: 0x00088E54
		private TElement Remove(int index)
		{
			TElement result = this.m_elements[index];
			int index2 = this.m_elements.Count - 1;
			this.m_elements[index] = this.m_elements[index2];
			this.m_elements.RemoveAt(index2);
			return result;
		}

		// Token: 0x040009C1 RID: 2497
		private readonly List<TElement> m_elements;

		// Token: 0x040009C2 RID: 2498
		private int m_currentIteratorIndex;
	}
}
