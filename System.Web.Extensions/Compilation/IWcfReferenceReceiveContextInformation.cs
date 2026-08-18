using System;
using System.Collections.Generic;

namespace System.Web.Compilation
{
	// Token: 0x0200000A RID: 10
	public interface IWcfReferenceReceiveContextInformation
	{
		// Token: 0x0600005D RID: 93
		void ReceiveImportContextInformation(IDictionary<string, byte[]> serviceReferenceExtensionFileContents, IServiceProvider serviceProvider);
	}
}
