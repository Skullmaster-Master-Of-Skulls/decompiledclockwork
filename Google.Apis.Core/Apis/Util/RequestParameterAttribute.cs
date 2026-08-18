using System;

namespace Google.Apis.Util
{
	// Token: 0x0200000A RID: 10
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	public class RequestParameterAttribute : Attribute
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000024 RID: 36 RVA: 0x000022AC File Offset: 0x000004AC
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000025 RID: 37 RVA: 0x000022B4 File Offset: 0x000004B4
		public RequestParameterType Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000022BC File Offset: 0x000004BC
		public RequestParameterAttribute(string name) : this(name, RequestParameterType.Query)
		{
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000022C6 File Offset: 0x000004C6
		public RequestParameterAttribute(string name, RequestParameterType type)
		{
			this.name = name;
			this.type = type;
		}

		// Token: 0x0400000B RID: 11
		private readonly string name;

		// Token: 0x0400000C RID: 12
		private readonly RequestParameterType type;
	}
}
