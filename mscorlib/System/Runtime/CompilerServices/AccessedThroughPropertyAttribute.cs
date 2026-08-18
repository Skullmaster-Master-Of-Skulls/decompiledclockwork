using System;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020005D2 RID: 1490
	[AttributeUsage(AttributeTargets.Field)]
	[ComVisible(true)]
	public sealed class AccessedThroughPropertyAttribute : Attribute
	{
		// Token: 0x060037B1 RID: 14257 RVA: 0x000BBA3F File Offset: 0x000BAA3F
		public AccessedThroughPropertyAttribute(string propertyName)
		{
			this.propertyName = propertyName;
		}

		// Token: 0x17000962 RID: 2402
		// (get) Token: 0x060037B2 RID: 14258 RVA: 0x000BBA4E File Offset: 0x000BAA4E
		public string PropertyName
		{
			get
			{
				return this.propertyName;
			}
		}

		// Token: 0x04001CE0 RID: 7392
		private readonly string propertyName;
	}
}
