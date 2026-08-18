using System;

namespace System.Diagnostics
{
	// Token: 0x020004AA RID: 1194
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class SwitchLevelAttribute : Attribute
	{
		// Token: 0x06002C40 RID: 11328 RVA: 0x000C7A05 File Offset: 0x000C5C05
		public SwitchLevelAttribute(Type switchLevelType)
		{
			this.SwitchLevelType = switchLevelType;
		}

		// Token: 0x17000ABB RID: 2747
		// (get) Token: 0x06002C41 RID: 11329 RVA: 0x000C7A14 File Offset: 0x000C5C14
		// (set) Token: 0x06002C42 RID: 11330 RVA: 0x000C7A1C File Offset: 0x000C5C1C
		public Type SwitchLevelType
		{
			get
			{
				return this.type;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.type = value;
			}
		}

		// Token: 0x040026C6 RID: 9926
		private Type type;
	}
}
