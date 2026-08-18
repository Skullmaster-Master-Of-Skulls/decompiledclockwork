using System;
using System.Collections.Generic;
using System.Text;

namespace Oracle.SqlAndPlsqlParser
{
	// Token: 0x02000286 RID: 646
	internal class Set<T> : HashSet<T>
	{
		// Token: 0x0600193D RID: 6461 RVA: 0x00108658 File Offset: 0x00106858
		public Set()
		{
		}

		// Token: 0x0600193E RID: 6462 RVA: 0x00108660 File Offset: 0x00106860
		public Set(IEnumerable<T> collection) : base(collection)
		{
		}

		// Token: 0x0600193F RID: 6463 RVA: 0x0010866C File Offset: 0x0010686C
		public void RemoveAll(ICollection<T> col)
		{
			foreach (T item in col)
			{
				base.Remove(item);
			}
		}

		// Token: 0x06001940 RID: 6464 RVA: 0x001086B8 File Offset: 0x001068B8
		public void AddRange(ICollection<T> src)
		{
			foreach (T item in src)
			{
				base.Add(item);
			}
		}

		// Token: 0x06001941 RID: 6465 RVA: 0x00108704 File Offset: 0x00106904
		public T[] ToArray()
		{
			T[] array = new T[base.Count];
			int num = 0;
			foreach (T t in this)
			{
				array[num++] = t;
			}
			return array;
		}

		// Token: 0x06001942 RID: 6466 RVA: 0x00108768 File Offset: 0x00106968
		public int RemoveAll(Predicate<T> match)
		{
			return base.RemoveWhere(match);
		}

		// Token: 0x06001943 RID: 6467 RVA: 0x00108774 File Offset: 0x00106974
		public void ForEach(Action<T> action)
		{
			foreach (T obj in this)
			{
				action(obj);
			}
		}

		// Token: 0x06001944 RID: 6468 RVA: 0x001087C4 File Offset: 0x001069C4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (T t in this)
			{
				stringBuilder.Append(t);
				stringBuilder.Append("\n\r");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001945 RID: 6469 RVA: 0x00108830 File Offset: 0x00106A30
		public List<T> SortedList()
		{
			List<T> list = new List<T>(this);
			list.Sort();
			return list;
		}
	}
}
