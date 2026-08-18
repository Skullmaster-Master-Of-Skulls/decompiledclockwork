using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020005F3 RID: 1523
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
	[Serializable]
	public sealed class DependencyAttribute : Attribute
	{
		// Token: 0x060037FB RID: 14331 RVA: 0x000BBD66 File Offset: 0x000BAD66
		public DependencyAttribute(string dependentAssemblyArgument, LoadHint loadHintArgument)
		{
			this.dependentAssembly = dependentAssemblyArgument;
			this.loadHint = loadHintArgument;
		}

		// Token: 0x1700096F RID: 2415
		// (get) Token: 0x060037FC RID: 14332 RVA: 0x000BBD7C File Offset: 0x000BAD7C
		public string DependentAssembly
		{
			get
			{
				return this.dependentAssembly;
			}
		}

		// Token: 0x17000970 RID: 2416
		// (get) Token: 0x060037FD RID: 14333 RVA: 0x000BBD84 File Offset: 0x000BAD84
		public LoadHint LoadHint
		{
			get
			{
				return this.loadHint;
			}
		}

		// Token: 0x04001D05 RID: 7429
		private string dependentAssembly;

		// Token: 0x04001D06 RID: 7430
		private LoadHint loadHint;
	}
}
