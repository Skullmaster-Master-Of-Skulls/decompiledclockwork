using System;

namespace TechnoPro.Common.Public.Entities.Settings
{
	// Token: 0x020001D5 RID: 469
	public class FormSettingAttribute : SettingDataAttribute
	{
		// Token: 0x06000D9C RID: 3484 RVA: 0x0001572A File Offset: 0x0001392A
		public FormSettingAttribute(string name, string description, Group group, SettingSemantic semanticType, int formSettingCode, FormSettingType type) : base(name, description, group, semanticType)
		{
			this.formSettingCode = formSettingCode;
			this.type = type;
		}

		// Token: 0x06000D9D RID: 3485 RVA: 0x00015749 File Offset: 0x00013949
		public FormSettingAttribute(string name, Group group, SettingSemantic semanticType, int formSettingCode, FormSettingType type) : base(name, group, semanticType)
		{
			this.formSettingCode = formSettingCode;
			this.type = type;
		}

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x06000D9E RID: 3486 RVA: 0x00015768 File Offset: 0x00013968
		public int FormSettingCode
		{
			get
			{
				return this.formSettingCode;
			}
		}

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x06000D9F RID: 3487 RVA: 0x00015780 File Offset: 0x00013980
		public FormSettingType Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x04000949 RID: 2377
		protected int formSettingCode;

		// Token: 0x0400094A RID: 2378
		protected FormSettingType type;
	}
}
