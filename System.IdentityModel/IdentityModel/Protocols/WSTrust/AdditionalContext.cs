using System;
using System.Collections.Generic;

namespace System.IdentityModel.Protocols.WSTrust
{
	// Token: 0x020001EF RID: 495
	public class AdditionalContext
	{
		// Token: 0x06001088 RID: 4232 RVA: 0x00046E2D File Offset: 0x0004502D
		public AdditionalContext()
		{
		}

		// Token: 0x06001089 RID: 4233 RVA: 0x00046E40 File Offset: 0x00045040
		public AdditionalContext(IEnumerable<ContextItem> items)
		{
			if (items == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("items");
			}
			foreach (ContextItem item in items)
			{
				this._contextItems.Add(item);
			}
		}

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x0600108A RID: 4234 RVA: 0x00046EB4 File Offset: 0x000450B4
		public IList<ContextItem> Items
		{
			get
			{
				return this._contextItems;
			}
		}

		// Token: 0x04000E63 RID: 3683
		private List<ContextItem> _contextItems = new List<ContextItem>();
	}
}
