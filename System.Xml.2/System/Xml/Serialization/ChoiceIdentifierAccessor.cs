using System;
using System.Reflection;

namespace System.Xml.Serialization
{
	// Token: 0x02000148 RID: 328
	internal class ChoiceIdentifierAccessor : Accessor
	{
		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x06001744 RID: 5956 RVA: 0x00067394 File Offset: 0x00065594
		// (set) Token: 0x06001745 RID: 5957 RVA: 0x0006739C File Offset: 0x0006559C
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

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x06001746 RID: 5958 RVA: 0x000673A5 File Offset: 0x000655A5
		// (set) Token: 0x06001747 RID: 5959 RVA: 0x000673AD File Offset: 0x000655AD
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

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x06001748 RID: 5960 RVA: 0x000673B6 File Offset: 0x000655B6
		// (set) Token: 0x06001749 RID: 5961 RVA: 0x000673BE File Offset: 0x000655BE
		internal MemberInfo MemberInfo
		{
			get
			{
				return this.memberInfo;
			}
			set
			{
				this.memberInfo = value;
			}
		}

		// Token: 0x04000ACF RID: 2767
		private string memberName;

		// Token: 0x04000AD0 RID: 2768
		private string[] memberIds;

		// Token: 0x04000AD1 RID: 2769
		private MemberInfo memberInfo;
	}
}
