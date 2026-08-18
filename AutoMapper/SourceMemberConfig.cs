using System;
using System.Reflection;

namespace AutoMapper
{
	// Token: 0x02000039 RID: 57
	public class SourceMemberConfig
	{
		// Token: 0x06000267 RID: 615 RVA: 0x00005CA4 File Offset: 0x00003EA4
		public SourceMemberConfig(MemberInfo sourceMember)
		{
			this.SourceMember = sourceMember;
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000268 RID: 616 RVA: 0x00005CB3 File Offset: 0x00003EB3
		// (set) Token: 0x06000269 RID: 617 RVA: 0x00005CBB File Offset: 0x00003EBB
		public MemberInfo SourceMember { get; private set; }

		// Token: 0x0600026A RID: 618 RVA: 0x00005CC4 File Offset: 0x00003EC4
		public void Ignore()
		{
			this._ignored = true;
		}

		// Token: 0x0600026B RID: 619 RVA: 0x00005CCD File Offset: 0x00003ECD
		public bool IsIgnored()
		{
			return this._ignored;
		}

		// Token: 0x0400006A RID: 106
		private bool _ignored;
	}
}
