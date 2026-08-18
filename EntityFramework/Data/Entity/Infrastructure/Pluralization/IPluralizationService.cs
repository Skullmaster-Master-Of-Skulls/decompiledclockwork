using System;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Infrastructure.Pluralization
{
	// Token: 0x0200028C RID: 652
	[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Pluralization")]
	public interface IPluralizationService
	{
		// Token: 0x060016CC RID: 5836
		string Pluralize(string word);

		// Token: 0x060016CD RID: 5837
		string Singularize(string word);
	}
}
