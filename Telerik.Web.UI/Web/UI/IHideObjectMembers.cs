using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Web.UI
{
	// Token: 0x0200196B RID: 6507
	[EditorBrowsable(EditorBrowsableState.Never)]
	public interface IHideObjectMembers
	{
		// Token: 0x0600FC1D RID: 64541
		[EditorBrowsable(EditorBrowsableState.Never)]
		bool Equals(object value);

		// Token: 0x0600FC1E RID: 64542
		[EditorBrowsable(EditorBrowsableState.Never)]
		int GetHashCode();

		// Token: 0x0600FC1F RID: 64543
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "In that case it is an issue of the .NET Framework itself")]
		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "GetType", Justification = "This should not be visible in auto complete list of VS, distracts when writing fluent syntax.")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		Type GetType();

		// Token: 0x0600FC20 RID: 64544
		[EditorBrowsable(EditorBrowsableState.Never)]
		string ToString();
	}
}
