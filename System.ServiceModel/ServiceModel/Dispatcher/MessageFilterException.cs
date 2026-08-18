using System;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000470 RID: 1136
	[Serializable]
	public class MessageFilterException : CommunicationException
	{
		// Token: 0x06002C16 RID: 11286 RVA: 0x000ACA4E File Offset: 0x000AAC4E
		protected MessageFilterException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.filters = null;
		}

		// Token: 0x06002C17 RID: 11287 RVA: 0x000ACA5F File Offset: 0x000AAC5F
		public MessageFilterException()
		{
		}

		// Token: 0x06002C18 RID: 11288 RVA: 0x000ACA67 File Offset: 0x000AAC67
		public MessageFilterException(string message) : this(message, null, null)
		{
		}

		// Token: 0x06002C19 RID: 11289 RVA: 0x000ACA72 File Offset: 0x000AAC72
		public MessageFilterException(string message, Exception innerException) : this(message, innerException, null)
		{
		}

		// Token: 0x06002C1A RID: 11290 RVA: 0x000ACA7D File Offset: 0x000AAC7D
		public MessageFilterException(string message, Collection<MessageFilter> filters) : this(message, null, filters)
		{
		}

		// Token: 0x06002C1B RID: 11291 RVA: 0x000ACA88 File Offset: 0x000AAC88
		public MessageFilterException(string message, Exception innerException, Collection<MessageFilter> filters) : base(message, innerException)
		{
			this.filters = filters;
		}

		// Token: 0x17000AA6 RID: 2726
		// (get) Token: 0x06002C1C RID: 11292 RVA: 0x000ACA99 File Offset: 0x000AAC99
		public Collection<MessageFilter> Filters
		{
			get
			{
				return this.filters;
			}
		}

		// Token: 0x04002438 RID: 9272
		[NonSerialized]
		private Collection<MessageFilter> filters;
	}
}
