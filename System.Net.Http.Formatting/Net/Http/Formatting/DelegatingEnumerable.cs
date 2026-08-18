using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace System.Net.Http.Formatting
{
	// Token: 0x0200003F RID: 63
	public sealed class DelegatingEnumerable<T> : IEnumerable<T>, IEnumerable
	{
		// Token: 0x06000247 RID: 583 RVA: 0x00008B93 File Offset: 0x00006D93
		public DelegatingEnumerable()
		{
			this._source = Enumerable.Empty<T>();
		}

		// Token: 0x06000248 RID: 584 RVA: 0x00008BA6 File Offset: 0x00006DA6
		public DelegatingEnumerable(IEnumerable<T> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			this._source = source;
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00008BC3 File Offset: 0x00006DC3
		public IEnumerator<T> GetEnumerator()
		{
			return this._source.GetEnumerator();
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00008BD0 File Offset: 0x00006DD0
		public void Add(object item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600024B RID: 587 RVA: 0x00008BD7 File Offset: 0x00006DD7
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._source.GetEnumerator();
		}

		// Token: 0x0400009B RID: 155
		private IEnumerable<T> _source;
	}
}
