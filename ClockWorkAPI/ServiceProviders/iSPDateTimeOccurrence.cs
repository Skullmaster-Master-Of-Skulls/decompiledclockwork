using System;
using System.Collections.Generic;

namespace ClockWorkAPI.ServiceProviders
{
	// Token: 0x02000024 RID: 36
	public interface iSPDateTimeOccurrence
	{
		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000202 RID: 514
		char DateTimeOccurrenceType { get; }

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000203 RID: 515
		List<SPMatching> Matchings { get; }

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000204 RID: 516
		string Caption { get; }
	}
}
