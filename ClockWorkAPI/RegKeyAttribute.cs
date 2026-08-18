using System;

namespace ClockWorkAPI
{
	// Token: 0x0200002F RID: 47
	internal class RegKeyAttribute : Attribute
	{
		// Token: 0x06000259 RID: 601 RVA: 0x0000DD8C File Offset: 0x0000CD8C
		internal RegKeyAttribute(string name)
		{
			this.Name = name;
		}

		// Token: 0x04000138 RID: 312
		internal string Name;
	}
}
