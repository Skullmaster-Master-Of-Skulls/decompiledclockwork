using System;
using System.Data.Entity.Core.Objects;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x0200075D RID: 1885
	public sealed class ReplacementDbQueryWrapper<TElement>
	{
		// Token: 0x06005532 RID: 21810 RVA: 0x00172AEC File Offset: 0x00170CEC
		private ReplacementDbQueryWrapper(ObjectQuery<TElement> query)
		{
			this._query = query;
		}

		// Token: 0x06005533 RID: 21811 RVA: 0x00172AFB File Offset: 0x00170CFB
		internal static ReplacementDbQueryWrapper<TElement> Create(ObjectQuery query)
		{
			return new ReplacementDbQueryWrapper<TElement>((ObjectQuery<TElement>)query);
		}

		// Token: 0x17000E90 RID: 3728
		// (get) Token: 0x06005534 RID: 21812 RVA: 0x00172B08 File Offset: 0x00170D08
		public ObjectQuery<TElement> Query
		{
			get
			{
				return this._query;
			}
		}

		// Token: 0x040022A5 RID: 8869
		private readonly ObjectQuery<TElement> _query;
	}
}
