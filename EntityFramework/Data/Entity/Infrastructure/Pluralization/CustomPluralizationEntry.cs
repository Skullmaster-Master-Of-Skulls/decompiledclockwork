using System;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Infrastructure.Pluralization
{
	// Token: 0x0200028B RID: 651
	[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Pluralization")]
	public class CustomPluralizationEntry
	{
		// Token: 0x17000298 RID: 664
		// (get) Token: 0x060016C7 RID: 5831 RVA: 0x0006F6B0 File Offset: 0x0006D8B0
		// (set) Token: 0x060016C8 RID: 5832 RVA: 0x0006F6B8 File Offset: 0x0006D8B8
		public string Singular { get; private set; }

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x060016C9 RID: 5833 RVA: 0x0006F6C1 File Offset: 0x0006D8C1
		// (set) Token: 0x060016CA RID: 5834 RVA: 0x0006F6C9 File Offset: 0x0006D8C9
		public string Plural { get; private set; }

		// Token: 0x060016CB RID: 5835 RVA: 0x0006F6D2 File Offset: 0x0006D8D2
		public CustomPluralizationEntry(string singular, string plural)
		{
			Check.NotEmpty(singular, "singular");
			Check.NotEmpty(plural, "plural");
			this.Singular = singular;
			this.Plural = plural;
		}
	}
}
