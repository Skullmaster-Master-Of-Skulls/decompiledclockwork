using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020005F2 RID: 1522
	[AttributeUsage(AttributeTargets.Assembly)]
	[Serializable]
	public sealed class DefaultDependencyAttribute : Attribute
	{
		// Token: 0x060037F9 RID: 14329 RVA: 0x000BBD4F File Offset: 0x000BAD4F
		public DefaultDependencyAttribute(LoadHint loadHintArgument)
		{
			this.loadHint = loadHintArgument;
		}

		// Token: 0x1700096E RID: 2414
		// (get) Token: 0x060037FA RID: 14330 RVA: 0x000BBD5E File Offset: 0x000BAD5E
		public LoadHint LoadHint
		{
			get
			{
				return this.loadHint;
			}
		}

		// Token: 0x04001D04 RID: 7428
		private LoadHint loadHint;
	}
}
