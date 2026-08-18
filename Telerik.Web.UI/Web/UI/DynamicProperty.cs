using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000371 RID: 881
	internal class DynamicProperty
	{
		// Token: 0x06001E37 RID: 7735 RVA: 0x0005E371 File Offset: 0x0005C571
		public DynamicProperty(string name, Type type)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			this.name = name;
			this.type = type;
		}

		// Token: 0x17000A48 RID: 2632
		// (get) Token: 0x06001E38 RID: 7736 RVA: 0x0005E3A9 File Offset: 0x0005C5A9
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000A49 RID: 2633
		// (get) Token: 0x06001E39 RID: 7737 RVA: 0x0005E3B1 File Offset: 0x0005C5B1
		public Type Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x0400077B RID: 1915
		private string name;

		// Token: 0x0400077C RID: 1916
		private Type type;
	}
}
