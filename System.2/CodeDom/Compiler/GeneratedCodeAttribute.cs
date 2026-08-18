using System;

namespace System.CodeDom.Compiler
{
	// Token: 0x0200067C RID: 1660
	[AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = false)]
	[__DynamicallyInvokable]
	public sealed class GeneratedCodeAttribute : Attribute
	{
		// Token: 0x06003D39 RID: 15673 RVA: 0x000FBFF9 File Offset: 0x000FA1F9
		[__DynamicallyInvokable]
		public GeneratedCodeAttribute(string tool, string version)
		{
			this.tool = tool;
			this.version = version;
		}

		// Token: 0x17000E9A RID: 3738
		// (get) Token: 0x06003D3A RID: 15674 RVA: 0x000FC00F File Offset: 0x000FA20F
		[__DynamicallyInvokable]
		public string Tool
		{
			[__DynamicallyInvokable]
			get
			{
				return this.tool;
			}
		}

		// Token: 0x17000E9B RID: 3739
		// (get) Token: 0x06003D3B RID: 15675 RVA: 0x000FC017 File Offset: 0x000FA217
		[__DynamicallyInvokable]
		public string Version
		{
			[__DynamicallyInvokable]
			get
			{
				return this.version;
			}
		}

		// Token: 0x04002CA0 RID: 11424
		private readonly string tool;

		// Token: 0x04002CA1 RID: 11425
		private readonly string version;
	}
}
