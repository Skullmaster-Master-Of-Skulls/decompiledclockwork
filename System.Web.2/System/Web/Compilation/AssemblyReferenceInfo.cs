using System;
using System.Reflection;

namespace System.Web.Compilation
{
	// Token: 0x02000806 RID: 2054
	internal class AssemblyReferenceInfo
	{
		// Token: 0x06006281 RID: 25217 RVA: 0x00159757 File Offset: 0x00157957
		internal AssemblyReferenceInfo(int referenceIndex)
		{
			this.ReferenceIndex = referenceIndex;
		}

		// Token: 0x04003327 RID: 13095
		internal Assembly Assembly;

		// Token: 0x04003328 RID: 13096
		internal int ReferenceIndex;
	}
}
