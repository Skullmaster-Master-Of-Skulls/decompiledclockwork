using System;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Web.Data
{
	// Token: 0x02001B8E RID: 7054
	public class DynamicProperty
	{
		// Token: 0x06011169 RID: 69993 RVA: 0x003C5465 File Offset: 0x003C3665
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

		// Token: 0x17005370 RID: 21360
		// (get) Token: 0x0601116A RID: 69994 RVA: 0x003C549D File Offset: 0x003C369D
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17005371 RID: 21361
		// (get) Token: 0x0601116B RID: 69995 RVA: 0x003C54A5 File Offset: 0x003C36A5
		[SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods")]
		public Type Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x04004C76 RID: 19574
		private string name;

		// Token: 0x04004C77 RID: 19575
		private Type type;
	}
}
