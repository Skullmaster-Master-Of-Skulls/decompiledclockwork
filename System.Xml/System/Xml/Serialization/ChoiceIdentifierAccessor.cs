using System;

namespace System.Xml.Serialization
{
	// Token: 0x020002C1 RID: 705
	internal class ChoiceIdentifierAccessor : Accessor
	{
		// Token: 0x1700080C RID: 2060
		// (get) Token: 0x0600219A RID: 8602 RVA: 0x0009F0D4 File Offset: 0x0009E0D4
		// (set) Token: 0x0600219B RID: 8603 RVA: 0x0009F0DC File Offset: 0x0009E0DC
		internal string MemberName
		{
			get
			{
				return this.memberName;
			}
			set
			{
				this.memberName = value;
			}
		}

		// Token: 0x1700080D RID: 2061
		// (get) Token: 0x0600219C RID: 8604 RVA: 0x0009F0E5 File Offset: 0x0009E0E5
		// (set) Token: 0x0600219D RID: 8605 RVA: 0x0009F0ED File Offset: 0x0009E0ED
		internal string[] MemberIds
		{
			get
			{
				return this.memberIds;
			}
			set
			{
				this.memberIds = value;
			}
		}

		// Token: 0x0400146A RID: 5226
		private string memberName;

		// Token: 0x0400146B RID: 5227
		private string[] memberIds;
	}
}
