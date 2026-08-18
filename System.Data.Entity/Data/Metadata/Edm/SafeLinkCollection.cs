using System;
using System.Collections.Generic;

namespace System.Data.Metadata.Edm
{
	// Token: 0x020001AF RID: 431
	internal class SafeLinkCollection<TParent, TChild> : ReadOnlyMetadataCollection<TChild> where TParent : class where TChild : MetadataItem
	{
		// Token: 0x06001EC5 RID: 7877 RVA: 0x0006C859 File Offset: 0x0006AA59
		public SafeLinkCollection(TParent parent, Func<TChild, SafeLink<TParent>> getLink, MetadataCollection<TChild> children) : base((IList<TChild>)SafeLink<TParent>.BindChildren<TChild>(parent, getLink, children))
		{
		}
	}
}
