using System;

namespace System.Web.Query.Dynamic
{
	// Token: 0x02000039 RID: 57
	internal class DynamicProperty
	{
		// Token: 0x0600022A RID: 554 RVA: 0x0000D945 File Offset: 0x0000BB45
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

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600022B RID: 555 RVA: 0x0000D97D File Offset: 0x0000BB7D
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600022C RID: 556 RVA: 0x0000D985 File Offset: 0x0000BB85
		public Type Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x040000DA RID: 218
		private string name;

		// Token: 0x040000DB RID: 219
		private Type type;
	}
}
