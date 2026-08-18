using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Contracts;

namespace TechnoPro.ClockWorkServer.Client.Messaging.Core
{
	// Token: 0x02000004 RID: 4
	public static class MessagingFilter
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000020 RID: 32 RVA: 0x0000261A File Offset: 0x0000081A
		// (set) Token: 0x06000021 RID: 33 RVA: 0x00002621 File Offset: 0x00000821
		public static IList<Predicate<InstantMessage>> Rules { get; private set; } = new List<Predicate<InstantMessage>>
		{
			(InstantMessage im) => im.Code == MessageCode.REGULAR_MESSAGE || im.Code == MessageCode.ERROR_MESSAGE || im.Code == MessageCode.SERVER_INFO_MESSAGE || im.Code == MessageCode.DOUBLE_LOGIN || im.Code == MessageCode.STUDENT_WAITING
		};

		// Token: 0x06000023 RID: 35 RVA: 0x0000264C File Offset: 0x0000084C
		public static bool MatchRules(this InstantMessage im)
		{
			return MessagingFilter.Rules.All((Predicate<InstantMessage> f) => f(im));
		}
	}
}
