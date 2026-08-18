using System;

namespace TechnoPro.Common.Public.Entities.DynamicForms.DynamicFieldConversion
{
	// Token: 0x0200037F RID: 895
	public class DynamicDataConversionItemData
	{
		// Token: 0x17000B83 RID: 2947
		// (get) Token: 0x06001BB8 RID: 7096 RVA: 0x0001F76F File Offset: 0x0001D96F
		// (set) Token: 0x06001BB9 RID: 7097 RVA: 0x0001F777 File Offset: 0x0001D977
		public eDynamicDataStorageLocation StorageLocation { get; set; }

		// Token: 0x17000B84 RID: 2948
		// (get) Token: 0x06001BBA RID: 7098 RVA: 0x0001F780 File Offset: 0x0001D980
		// (set) Token: 0x06001BBB RID: 7099 RVA: 0x0001F788 File Offset: 0x0001D988
		public int DataId { get; set; }

		// Token: 0x17000B85 RID: 2949
		// (get) Token: 0x06001BBC RID: 7100 RVA: 0x0001F791 File Offset: 0x0001D991
		// (set) Token: 0x06001BBD RID: 7101 RVA: 0x0001F799 File Offset: 0x0001D999
		public int? IntValue { get; set; }

		// Token: 0x17000B86 RID: 2950
		// (get) Token: 0x06001BBE RID: 7102 RVA: 0x0001F7A2 File Offset: 0x0001D9A2
		// (set) Token: 0x06001BBF RID: 7103 RVA: 0x0001F7AA File Offset: 0x0001D9AA
		public DateTime? DateTimeValue { get; set; }

		// Token: 0x17000B87 RID: 2951
		// (get) Token: 0x06001BC0 RID: 7104 RVA: 0x0001F7B3 File Offset: 0x0001D9B3
		// (set) Token: 0x06001BC1 RID: 7105 RVA: 0x0001F7BB File Offset: 0x0001D9BB
		public byte[] BinaryValue { get; set; }
	}
}
