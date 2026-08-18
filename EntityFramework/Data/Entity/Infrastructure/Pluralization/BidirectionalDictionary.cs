using System;
using System.Collections.Generic;

namespace System.Data.Entity.Infrastructure.Pluralization
{
	// Token: 0x02000807 RID: 2055
	internal class BidirectionalDictionary<TFirst, TSecond>
	{
		// Token: 0x17000FD8 RID: 4056
		// (get) Token: 0x06005C92 RID: 23698 RVA: 0x0018FED0 File Offset: 0x0018E0D0
		// (set) Token: 0x06005C93 RID: 23699 RVA: 0x0018FED8 File Offset: 0x0018E0D8
		internal Dictionary<TFirst, TSecond> FirstToSecondDictionary { get; set; }

		// Token: 0x17000FD9 RID: 4057
		// (get) Token: 0x06005C94 RID: 23700 RVA: 0x0018FEE1 File Offset: 0x0018E0E1
		// (set) Token: 0x06005C95 RID: 23701 RVA: 0x0018FEE9 File Offset: 0x0018E0E9
		internal Dictionary<TSecond, TFirst> SecondToFirstDictionary { get; set; }

		// Token: 0x06005C96 RID: 23702 RVA: 0x0018FEF2 File Offset: 0x0018E0F2
		internal BidirectionalDictionary()
		{
			this.FirstToSecondDictionary = new Dictionary<TFirst, TSecond>();
			this.SecondToFirstDictionary = new Dictionary<TSecond, TFirst>();
		}

		// Token: 0x06005C97 RID: 23703 RVA: 0x0018FF10 File Offset: 0x0018E110
		internal BidirectionalDictionary(Dictionary<TFirst, TSecond> firstToSecondDictionary) : this()
		{
			foreach (TFirst tfirst in firstToSecondDictionary.Keys)
			{
				this.AddValue(tfirst, firstToSecondDictionary[tfirst]);
			}
		}

		// Token: 0x06005C98 RID: 23704 RVA: 0x0018FF70 File Offset: 0x0018E170
		internal virtual bool ExistsInFirst(TFirst value)
		{
			return this.FirstToSecondDictionary.ContainsKey(value);
		}

		// Token: 0x06005C99 RID: 23705 RVA: 0x0018FF83 File Offset: 0x0018E183
		internal virtual bool ExistsInSecond(TSecond value)
		{
			return this.SecondToFirstDictionary.ContainsKey(value);
		}

		// Token: 0x06005C9A RID: 23706 RVA: 0x0018FF98 File Offset: 0x0018E198
		internal virtual TSecond GetSecondValue(TFirst value)
		{
			if (this.ExistsInFirst(value))
			{
				return this.FirstToSecondDictionary[value];
			}
			return default(TSecond);
		}

		// Token: 0x06005C9B RID: 23707 RVA: 0x0018FFC4 File Offset: 0x0018E1C4
		internal virtual TFirst GetFirstValue(TSecond value)
		{
			if (this.ExistsInSecond(value))
			{
				return this.SecondToFirstDictionary[value];
			}
			return default(TFirst);
		}

		// Token: 0x06005C9C RID: 23708 RVA: 0x0018FFF0 File Offset: 0x0018E1F0
		internal void AddValue(TFirst firstValue, TSecond secondValue)
		{
			this.FirstToSecondDictionary.Add(firstValue, secondValue);
			if (!this.SecondToFirstDictionary.ContainsKey(secondValue))
			{
				this.SecondToFirstDictionary.Add(secondValue, firstValue);
			}
		}
	}
}
