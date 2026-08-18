using System;

namespace TechnoPro.Common.WCF.Attributes
{
	// Token: 0x0200001A RID: 26
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Interface, Inherited = true, AllowMultiple = false)]
	public class XtraSizeServiceAttribute : BindingServiceAttribute
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00003E3E File Offset: 0x0000203E
		// (set) Token: 0x06000083 RID: 131 RVA: 0x00003E46 File Offset: 0x00002046
		public int SizeInBytes { get; set; }

		// Token: 0x06000084 RID: 132 RVA: 0x00003E4F File Offset: 0x0000204F
		public XtraSizeServiceAttribute()
		{
			this.SizeInBytes = int.MaxValue;
		}
	}
}
