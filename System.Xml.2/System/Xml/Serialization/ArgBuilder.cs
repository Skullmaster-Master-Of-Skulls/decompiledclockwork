using System;

namespace System.Xml.Serialization
{
	// Token: 0x02000131 RID: 305
	internal class ArgBuilder
	{
		// Token: 0x06001696 RID: 5782 RVA: 0x00063D45 File Offset: 0x00061F45
		internal ArgBuilder(string name, int index, Type argType)
		{
			this.Name = name;
			this.Index = index;
			this.ArgType = argType;
		}

		// Token: 0x04000A80 RID: 2688
		internal string Name;

		// Token: 0x04000A81 RID: 2689
		internal int Index;

		// Token: 0x04000A82 RID: 2690
		internal Type ArgType;
	}
}
