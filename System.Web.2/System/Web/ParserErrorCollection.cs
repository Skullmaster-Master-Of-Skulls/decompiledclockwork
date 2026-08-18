using System;
using System.Collections;

namespace System.Web
{
	// Token: 0x0200009E RID: 158
	[Serializable]
	public sealed class ParserErrorCollection : CollectionBase
	{
		// Token: 0x06000A10 RID: 2576 RVA: 0x000170A2 File Offset: 0x000152A2
		public ParserErrorCollection()
		{
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x000170AA File Offset: 0x000152AA
		public ParserErrorCollection(ParserError[] value)
		{
			this.AddRange(value);
		}

		// Token: 0x170003EE RID: 1006
		public ParserError this[int index]
		{
			get
			{
				return (ParserError)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x000170DB File Offset: 0x000152DB
		public int Add(ParserError value)
		{
			return base.List.Add(value);
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x000170EC File Offset: 0x000152EC
		public void AddRange(ParserError[] value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			for (int i = 0; i < value.Length; i++)
			{
				this.Add(value[i]);
			}
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x00017120 File Offset: 0x00015320
		public void AddRange(ParserErrorCollection value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			foreach (object obj in value)
			{
				ParserError value2 = (ParserError)obj;
				this.Add(value2);
			}
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x00017184 File Offset: 0x00015384
		public bool Contains(ParserError value)
		{
			return base.List.Contains(value);
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x00017192 File Offset: 0x00015392
		public void CopyTo(ParserError[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x000171A1 File Offset: 0x000153A1
		public int IndexOf(ParserError value)
		{
			return base.List.IndexOf(value);
		}

		// Token: 0x06000A1A RID: 2586 RVA: 0x000171AF File Offset: 0x000153AF
		public void Insert(int index, ParserError value)
		{
			base.List.Insert(index, value);
		}

		// Token: 0x06000A1B RID: 2587 RVA: 0x000171BE File Offset: 0x000153BE
		public void Remove(ParserError value)
		{
			base.List.Remove(value);
		}
	}
}
