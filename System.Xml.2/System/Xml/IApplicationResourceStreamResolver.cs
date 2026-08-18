using System;
using System.ComponentModel;
using System.IO;

namespace System.Xml
{
	// Token: 0x0200006F RID: 111
	[Obsolete("This API supports the .NET Framework infrastructure and is not intended to be used directly from your code.", true)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public interface IApplicationResourceStreamResolver
	{
		// Token: 0x060003CF RID: 975
		[Obsolete("This API supports the .NET Framework infrastructure and is not intended to be used directly from your code.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		Stream GetApplicationResourceStream(Uri relativeUri);
	}
}
