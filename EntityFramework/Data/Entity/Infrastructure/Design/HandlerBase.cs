using System;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Infrastructure.Design
{
	// Token: 0x0200018D RID: 397
	internal abstract class HandlerBase : MarshalByRefObject
	{
		// Token: 0x06000D81 RID: 3457 RVA: 0x0003CFC0 File Offset: 0x0003B1C0
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
		public virtual bool ImplementsContract(string interfaceName)
		{
			Type type;
			try
			{
				type = Type.GetType(interfaceName, true);
			}
			catch
			{
				return false;
			}
			return type.IsAssignableFrom(base.GetType());
		}
	}
}
