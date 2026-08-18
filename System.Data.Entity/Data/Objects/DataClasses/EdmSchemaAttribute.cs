using System;

namespace System.Data.Objects.DataClasses
{
	// Token: 0x02000188 RID: 392
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class, AllowMultiple = true)]
	public sealed class EdmSchemaAttribute : Attribute
	{
		// Token: 0x06001C28 RID: 7208 RVA: 0x0005FBC4 File Offset: 0x0005DDC4
		public EdmSchemaAttribute()
		{
		}

		// Token: 0x06001C29 RID: 7209 RVA: 0x0005FBCC File Offset: 0x0005DDCC
		public EdmSchemaAttribute(string assemblyGuid)
		{
			if (assemblyGuid == null)
			{
				throw new ArgumentNullException("assemblyGuid");
			}
		}
	}
}
