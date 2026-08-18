using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.ServiceModel.Channels
{
	// Token: 0x020007C1 RID: 1985
	[TypeForwardedFrom("System.WorkflowServices, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
	public interface IContextManager
	{
		// Token: 0x170012D6 RID: 4822
		// (get) Token: 0x06004AE3 RID: 19171
		// (set) Token: 0x06004AE4 RID: 19172
		bool Enabled { get; set; }

		// Token: 0x06004AE5 RID: 19173
		IDictionary<string, string> GetContext();

		// Token: 0x06004AE6 RID: 19174
		void SetContext(IDictionary<string, string> context);
	}
}
