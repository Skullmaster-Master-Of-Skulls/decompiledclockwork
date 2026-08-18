using System;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000475 RID: 1141
	[Serializable]
	public class MultipleFilterMatchesException : SystemException
	{
		// Token: 0x06002C61 RID: 11361 RVA: 0x000AD7E5 File Offset: 0x000AB9E5
		protected MultipleFilterMatchesException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.filters = null;
		}

		// Token: 0x06002C62 RID: 11362 RVA: 0x000AD7F6 File Offset: 0x000AB9F6
		public MultipleFilterMatchesException() : this(SR.GetString("FilterMultipleMatches"))
		{
		}

		// Token: 0x06002C63 RID: 11363 RVA: 0x000AD808 File Offset: 0x000ABA08
		public MultipleFilterMatchesException(string message) : this(message, null, null)
		{
		}

		// Token: 0x06002C64 RID: 11364 RVA: 0x000AD813 File Offset: 0x000ABA13
		public MultipleFilterMatchesException(string message, Exception innerException) : this(message, innerException, null)
		{
		}

		// Token: 0x06002C65 RID: 11365 RVA: 0x000AD81E File Offset: 0x000ABA1E
		public MultipleFilterMatchesException(string message, Collection<MessageFilter> filters) : this(message, null, filters)
		{
		}

		// Token: 0x06002C66 RID: 11366 RVA: 0x000AD829 File Offset: 0x000ABA29
		public MultipleFilterMatchesException(string message, Exception innerException, Collection<MessageFilter> filters) : base(message, innerException)
		{
			this.filters = filters;
		}

		// Token: 0x17000AB3 RID: 2739
		// (get) Token: 0x06002C67 RID: 11367 RVA: 0x000AD83A File Offset: 0x000ABA3A
		public Collection<MessageFilter> Filters
		{
			get
			{
				return this.filters;
			}
		}

		// Token: 0x04002440 RID: 9280
		[NonSerialized]
		private Collection<MessageFilter> filters;
	}
}
