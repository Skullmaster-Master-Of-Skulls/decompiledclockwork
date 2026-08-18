using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Practices.Unity.Configuration.ConfigurationHelpers
{
	// Token: 0x02000010 RID: 16
	internal class SequenceResult : IList<ParseResult>, ICollection<ParseResult>, IEnumerable<ParseResult>, IEnumerable
	{
		// Token: 0x06000054 RID: 84 RVA: 0x00002E61 File Offset: 0x00001061
		public SequenceResult(ParseResult result)
		{
			this.resultData = (IList<ParseResult>)result.ResultData;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002E7A File Offset: 0x0000107A
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.resultData.GetEnumerator();
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002E87 File Offset: 0x00001087
		public IEnumerator<ParseResult> GetEnumerator()
		{
			return this.resultData.GetEnumerator();
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002E94 File Offset: 0x00001094
		public void Add(ParseResult item)
		{
			this.resultData.Add(item);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002EA2 File Offset: 0x000010A2
		public void Clear()
		{
			this.resultData.Clear();
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002EAF File Offset: 0x000010AF
		public bool Contains(ParseResult item)
		{
			return this.resultData.Contains(item);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002EBD File Offset: 0x000010BD
		public void CopyTo(ParseResult[] array, int arrayIndex)
		{
			this.resultData.CopyTo(array, arrayIndex);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002ECC File Offset: 0x000010CC
		public bool Remove(ParseResult item)
		{
			return this.resultData.Remove(item);
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00002EDA File Offset: 0x000010DA
		public int Count
		{
			get
			{
				return this.resultData.Count;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00002EE7 File Offset: 0x000010E7
		public bool IsReadOnly
		{
			get
			{
				return this.resultData.IsReadOnly;
			}
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002EF4 File Offset: 0x000010F4
		public int IndexOf(ParseResult item)
		{
			return this.resultData.IndexOf(item);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002F02 File Offset: 0x00001102
		public void Insert(int index, ParseResult item)
		{
			this.resultData.Insert(index, item);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002F11 File Offset: 0x00001111
		public void RemoveAt(int index)
		{
			this.resultData.RemoveAt(index);
		}

		// Token: 0x17000011 RID: 17
		public ParseResult this[int index]
		{
			get
			{
				return this.resultData[index];
			}
			set
			{
				this.resultData[index] = value;
			}
		}

		// Token: 0x04000010 RID: 16
		private readonly IList<ParseResult> resultData;
	}
}
