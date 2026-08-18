using System;
using System.Collections;
using System.Collections.Generic;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x0200046A RID: 1130
	public interface IMessageFilterTable<TFilterData> : IDictionary<MessageFilter, TFilterData>, ICollection<KeyValuePair<MessageFilter, !0>>, IEnumerable<KeyValuePair<MessageFilter, !0>>, IEnumerable
	{
		// Token: 0x06002BE7 RID: 11239
		bool GetMatchingValue(Message message, out TFilterData value);

		// Token: 0x06002BE8 RID: 11240
		bool GetMatchingValue(MessageBuffer messageBuffer, out TFilterData value);

		// Token: 0x06002BE9 RID: 11241
		bool GetMatchingValues(Message message, ICollection<TFilterData> results);

		// Token: 0x06002BEA RID: 11242
		bool GetMatchingValues(MessageBuffer messageBuffer, ICollection<TFilterData> results);

		// Token: 0x06002BEB RID: 11243
		bool GetMatchingFilter(Message message, out MessageFilter filter);

		// Token: 0x06002BEC RID: 11244
		bool GetMatchingFilter(MessageBuffer messageBuffer, out MessageFilter filter);

		// Token: 0x06002BED RID: 11245
		bool GetMatchingFilters(Message message, ICollection<MessageFilter> results);

		// Token: 0x06002BEE RID: 11246
		bool GetMatchingFilters(MessageBuffer messageBuffer, ICollection<MessageFilter> results);
	}
}
