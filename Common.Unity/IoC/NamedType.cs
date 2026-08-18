using System;
using TechnoPro.Common.Unity.Adapters;

namespace TechnoPro.Common.Unity.IoC
{
	// Token: 0x02000009 RID: 9
	public class NamedType : IEquatable<NamedType>
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600001A RID: 26 RVA: 0x000025D0 File Offset: 0x000007D0
		// (set) Token: 0x0600001B RID: 27 RVA: 0x000025D8 File Offset: 0x000007D8
		public string Name { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600001C RID: 28 RVA: 0x000025E1 File Offset: 0x000007E1
		// (set) Token: 0x0600001D RID: 29 RVA: 0x000025E9 File Offset: 0x000007E9
		public Type Type { get; set; }

		// Token: 0x0600001E RID: 30 RVA: 0x000025F4 File Offset: 0x000007F4
		public override bool Equals(object obj)
		{
			return this.Equals(obj as NamedType);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002614 File Offset: 0x00000814
		public bool Equals(NamedType other)
		{
			bool flag = other == null;
			return !flag && other.Type == this.Type && other.Name == this.Name;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000265C File Offset: 0x0000085C
		public override int GetHashCode()
		{
			return this.GetHashCode(new string[]
			{
				"Name",
				"Type"
			});
		}
	}
}
