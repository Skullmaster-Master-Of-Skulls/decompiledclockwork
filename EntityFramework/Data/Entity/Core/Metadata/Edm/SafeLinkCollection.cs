using System;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x02000500 RID: 1280
	internal class SafeLinkCollection<TParent, TChild> : ReadOnlyMetadataCollection<TChild> where TParent : class where TChild : MetadataItem
	{
		// Token: 0x06002F96 RID: 12182 RVA: 0x000E4951 File Offset: 0x000E2B51
		public SafeLinkCollection(TParent parent, Func<TChild, SafeLink<TParent>> getLink, MetadataCollection<TChild> children) : base((MetadataCollection<TChild>)SafeLink<TParent>.BindChildren<TChild>(parent, getLink, children))
		{
		}
	}
}
