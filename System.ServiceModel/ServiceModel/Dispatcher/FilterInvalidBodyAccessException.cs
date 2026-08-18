using System;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000468 RID: 1128
	[Serializable]
	public class FilterInvalidBodyAccessException : InvalidBodyAccessException
	{
		// Token: 0x06002BDE RID: 11230 RVA: 0x000AC368 File Offset: 0x000AA568
		protected FilterInvalidBodyAccessException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.filters = null;
		}

		// Token: 0x06002BDF RID: 11231 RVA: 0x000AC379 File Offset: 0x000AA579
		public FilterInvalidBodyAccessException() : this(SR.GetString("SeekableMessageNavBodyForbidden"))
		{
		}

		// Token: 0x06002BE0 RID: 11232 RVA: 0x000AC38B File Offset: 0x000AA58B
		public FilterInvalidBodyAccessException(string message) : this(message, null, null)
		{
		}

		// Token: 0x06002BE1 RID: 11233 RVA: 0x000AC396 File Offset: 0x000AA596
		public FilterInvalidBodyAccessException(string message, Exception innerException) : this(message, innerException, null)
		{
		}

		// Token: 0x06002BE2 RID: 11234 RVA: 0x000AC3A1 File Offset: 0x000AA5A1
		public FilterInvalidBodyAccessException(string message, Collection<MessageFilter> filters) : this(message, null, filters)
		{
		}

		// Token: 0x06002BE3 RID: 11235 RVA: 0x000AC3AC File Offset: 0x000AA5AC
		public FilterInvalidBodyAccessException(string message, Exception innerException, Collection<MessageFilter> filters) : base(message, innerException)
		{
			this.filters = filters;
		}

		// Token: 0x17000AA0 RID: 2720
		// (get) Token: 0x06002BE4 RID: 11236 RVA: 0x000AC3BD File Offset: 0x000AA5BD
		public Collection<MessageFilter> Filters
		{
			get
			{
				return this.filters;
			}
		}

		// Token: 0x04002436 RID: 9270
		[NonSerialized]
		private Collection<MessageFilter> filters;
	}
}
