using System;

namespace TechnoPro.Common.WCF.Attributes
{
	// Token: 0x0200001B RID: 27
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Interface, Inherited = true, AllowMultiple = false)]
	public class StreamingServiceAttribute : BindingServiceAttribute
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00003E65 File Offset: 0x00002065
		// (set) Token: 0x06000086 RID: 134 RVA: 0x00003E6D File Offset: 0x0000206D
		public int SizeInBytes { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00003E76 File Offset: 0x00002076
		// (set) Token: 0x06000088 RID: 136 RVA: 0x00003E7E File Offset: 0x0000207E
		public int TimeoutInMinutes { get; set; }

		// Token: 0x06000089 RID: 137 RVA: 0x00003E87 File Offset: 0x00002087
		public StreamingServiceAttribute()
		{
			this.SizeInBytes = int.MaxValue;
			this.TimeoutInMinutes = 30;
		}
	}
}
