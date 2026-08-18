using System;
using System.Collections.Generic;

namespace System.ServiceModel.Channels
{
	// Token: 0x020006F8 RID: 1784
	[__DynamicallyInvokable]
	public class BindingParameterCollection : KeyedByTypeCollection<object>
	{
		// Token: 0x0600446C RID: 17516 RVA: 0x0010212F File Offset: 0x0010032F
		[__DynamicallyInvokable]
		public BindingParameterCollection()
		{
		}

		// Token: 0x0600446D RID: 17517 RVA: 0x00102138 File Offset: 0x00100338
		internal BindingParameterCollection(params object[] parameters)
		{
			if (parameters == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("parameters");
			}
			for (int i = 0; i < parameters.Length; i++)
			{
				base.Add(parameters[i]);
			}
		}

		// Token: 0x0600446E RID: 17518 RVA: 0x00102178 File Offset: 0x00100378
		internal BindingParameterCollection(BindingParameterCollection parameters)
		{
			if (parameters == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("parameters");
			}
			for (int i = 0; i < parameters.Count; i++)
			{
				base.Add(parameters[i]);
			}
		}
	}
}
