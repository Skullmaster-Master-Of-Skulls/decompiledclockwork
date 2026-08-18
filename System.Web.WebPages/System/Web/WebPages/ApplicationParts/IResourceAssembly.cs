using System;
using System.Collections.Generic;
using System.IO;

namespace System.Web.WebPages.ApplicationParts
{
	// Token: 0x0200000C RID: 12
	internal interface IResourceAssembly
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000050 RID: 80
		string Name { get; }

		// Token: 0x06000051 RID: 81
		Stream GetManifestResourceStream(string name);

		// Token: 0x06000052 RID: 82
		IEnumerable<string> GetManifestResourceNames();

		// Token: 0x06000053 RID: 83
		IEnumerable<Type> GetTypes();
	}
}
