using System;

namespace System.ServiceModel
{
	// Token: 0x020000DA RID: 218
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = false)]
	public sealed class MessagePropertyAttribute : Attribute
	{
		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000401 RID: 1025 RVA: 0x000159CA File Offset: 0x00013BCA
		// (set) Token: 0x06000402 RID: 1026 RVA: 0x000159D2 File Offset: 0x00013BD2
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.isNameSetExplicit = true;
				this.name = value;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000403 RID: 1027 RVA: 0x000159E2 File Offset: 0x00013BE2
		internal bool IsNameSetExplicit
		{
			get
			{
				return this.isNameSetExplicit;
			}
		}

		// Token: 0x040009C4 RID: 2500
		private string name;

		// Token: 0x040009C5 RID: 2501
		private bool isNameSetExplicit;
	}
}
