using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Telerik.Web.Data.Expressions
{
	// Token: 0x02001BBC RID: 7100
	internal class IndexerToken : IMemberAccessToken
	{
		// Token: 0x06011282 RID: 70274 RVA: 0x003C8820 File Offset: 0x003C6A20
		public IndexerToken(IEnumerable<object> arguments)
		{
			this.arguments = arguments.ToReadOnlyCollection<object>();
		}

		// Token: 0x06011283 RID: 70275 RVA: 0x003C8834 File Offset: 0x003C6A34
		public IndexerToken(params object[] arguments) : this((IEnumerable<object>)arguments)
		{
		}

		// Token: 0x170053AF RID: 21423
		// (get) Token: 0x06011284 RID: 70276 RVA: 0x003C8842 File Offset: 0x003C6A42
		public ReadOnlyCollection<object> Arguments
		{
			get
			{
				return this.arguments;
			}
		}

		// Token: 0x04004CCA RID: 19658
		private readonly ReadOnlyCollection<object> arguments;
	}
}
