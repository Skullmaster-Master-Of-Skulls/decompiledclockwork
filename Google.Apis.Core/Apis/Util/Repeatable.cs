using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Google.Apis.Util
{
	// Token: 0x02000009 RID: 9
	public class Repeatable<T> : IEnumerable<T>, IEnumerable
	{
		// Token: 0x0600001E RID: 30 RVA: 0x00002249 File Offset: 0x00000449
		public Repeatable(IEnumerable<T> enumeration)
		{
			this.values = new ReadOnlyCollection<T>(new List<T>(enumeration));
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002262 File Offset: 0x00000462
		public IEnumerator<T> GetEnumerator()
		{
			return this.values.GetEnumerator();
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000226F File Offset: 0x0000046F
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002277 File Offset: 0x00000477
		public static implicit operator Repeatable<T>(T elem)
		{
			if (elem == null)
			{
				return null;
			}
			return new Repeatable<T>(new T[]
			{
				elem
			});
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002296 File Offset: 0x00000496
		public static implicit operator Repeatable<T>(T[] elem)
		{
			if (elem.Length == 0)
			{
				return null;
			}
			return new Repeatable<T>(elem);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000022A4 File Offset: 0x000004A4
		public static implicit operator Repeatable<T>(List<T> elem)
		{
			return new Repeatable<T>(elem);
		}

		// Token: 0x0400000A RID: 10
		private readonly IList<T> values;
	}
}
