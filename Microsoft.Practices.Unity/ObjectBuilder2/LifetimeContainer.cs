using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Practices.ObjectBuilder2
{
	// Token: 0x0200003E RID: 62
	[SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "See ILifetimeContainer")]
	public class LifetimeContainer : ILifetimeContainer, IEnumerable<object>, IEnumerable, IDisposable
	{
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000FC RID: 252 RVA: 0x00003BF4 File Offset: 0x00001DF4
		public int Count
		{
			get
			{
				int count;
				lock (this.items)
				{
					count = this.items.Count;
				}
				return count;
			}
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00003C3C File Offset: 0x00001E3C
		public void Add(object item)
		{
			lock (this.items)
			{
				this.items.Add(item);
			}
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00003C84 File Offset: 0x00001E84
		public bool Contains(object item)
		{
			bool result;
			lock (this.items)
			{
				result = this.items.Contains(item);
			}
			return result;
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00003CCC File Offset: 0x00001ECC
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00003CDC File Offset: 0x00001EDC
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				lock (this.items)
				{
					List<object> list = new List<object>(this.items);
					list.Reverse();
					foreach (object obj2 in list)
					{
						IDisposable disposable = obj2 as IDisposable;
						if (disposable != null)
						{
							disposable.Dispose();
						}
					}
					this.items.Clear();
				}
			}
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00003D80 File Offset: 0x00001F80
		public IEnumerator<object> GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00003D92 File Offset: 0x00001F92
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00003D9C File Offset: 0x00001F9C
		public void Remove(object item)
		{
			lock (this.items)
			{
				if (this.items.Contains(item))
				{
					this.items.Remove(item);
				}
			}
		}

		// Token: 0x04000033 RID: 51
		private readonly List<object> items = new List<object>();
	}
}
