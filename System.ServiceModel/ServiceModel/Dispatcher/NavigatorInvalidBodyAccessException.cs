using System;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000476 RID: 1142
	[Serializable]
	public class NavigatorInvalidBodyAccessException : InvalidBodyAccessException
	{
		// Token: 0x06002C68 RID: 11368 RVA: 0x000AD842 File Offset: 0x000ABA42
		protected NavigatorInvalidBodyAccessException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06002C69 RID: 11369 RVA: 0x000AD84C File Offset: 0x000ABA4C
		public NavigatorInvalidBodyAccessException() : this(SR.GetString("SeekableMessageNavBodyForbidden"))
		{
		}

		// Token: 0x06002C6A RID: 11370 RVA: 0x000AD85E File Offset: 0x000ABA5E
		public NavigatorInvalidBodyAccessException(string message) : this(message, null)
		{
		}

		// Token: 0x06002C6B RID: 11371 RVA: 0x000AD868 File Offset: 0x000ABA68
		public NavigatorInvalidBodyAccessException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06002C6C RID: 11372 RVA: 0x000AD874 File Offset: 0x000ABA74
		internal FilterInvalidBodyAccessException Process(Opcode op)
		{
			Collection<MessageFilter> filters = new Collection<MessageFilter>();
			op.CollectXPathFilters(filters);
			return new FilterInvalidBodyAccessException(this.Message, base.InnerException, filters);
		}
	}
}
