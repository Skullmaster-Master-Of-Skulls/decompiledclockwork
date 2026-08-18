using System;

namespace TechnoPro.Common.Public.Entities.MailMergeEntities.MailMergeValues
{
	// Token: 0x020002D2 RID: 722
	public class MailMergeValueByteArray : MailMergeValueBase
	{
		// Token: 0x17000903 RID: 2307
		// (get) Token: 0x060015D4 RID: 5588 RVA: 0x0001B3D9 File Offset: 0x000195D9
		// (set) Token: 0x060015D5 RID: 5589 RVA: 0x0001B3E1 File Offset: 0x000195E1
		public byte[] Value { get; set; }

		// Token: 0x060015D6 RID: 5590 RVA: 0x0001B3EA File Offset: 0x000195EA
		public override void SetValue(object obj)
		{
			this.Value = base.GetValue<byte[]>(obj, null);
		}

		// Token: 0x060015D7 RID: 5591 RVA: 0x0001B3FC File Offset: 0x000195FC
		public override object GetValue()
		{
			return this.Value;
		}
	}
}
