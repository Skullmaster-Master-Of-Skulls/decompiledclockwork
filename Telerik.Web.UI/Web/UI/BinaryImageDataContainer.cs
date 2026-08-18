using System;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Web.UI
{
	// Token: 0x020016BB RID: 5819
	[Serializable]
	public class BinaryImageDataContainer
	{
		// Token: 0x170044DB RID: 17627
		// (get) Token: 0x0600E0AA RID: 57514 RVA: 0x0031F150 File Offset: 0x0031D350
		// (set) Token: 0x0600E0AB RID: 57515 RVA: 0x0031F158 File Offset: 0x0031D358
		[SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
		public byte[] Data { get; set; }

		// Token: 0x170044DC RID: 17628
		// (get) Token: 0x0600E0AC RID: 57516 RVA: 0x0031F161 File Offset: 0x0031D361
		// (set) Token: 0x0600E0AD RID: 57517 RVA: 0x0031F169 File Offset: 0x0031D369
		public string ImageName { get; set; }
	}
}
