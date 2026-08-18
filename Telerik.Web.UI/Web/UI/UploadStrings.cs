using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02001B7C RID: 7036
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class UploadStrings : LocalizationStrings
	{
		// Token: 0x060110BB RID: 69819 RVA: 0x003C2F04 File Offset: 0x003C1104
		internal UploadStrings(LocalizationProvider provider) : base(provider)
		{
		}

		// Token: 0x1700533C RID: 21308
		// (get) Token: 0x060110BC RID: 69820 RVA: 0x003C2F0D File Offset: 0x003C110D
		// (set) Token: 0x060110BD RID: 69821 RVA: 0x003C2F1A File Offset: 0x003C111A
		[DefaultValue("Select")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		public string Select
		{
			get
			{
				return this.GetString("Select");
			}
			set
			{
				this.SetString("Select", value);
			}
		}

		// Token: 0x1700533D RID: 21309
		// (get) Token: 0x060110BE RID: 69822 RVA: 0x003C2F28 File Offset: 0x003C1128
		// (set) Token: 0x060110BF RID: 69823 RVA: 0x003C2F35 File Offset: 0x003C1135
		[NotifyParentProperty(true)]
		[DefaultValue("Remove")]
		[Localizable(true)]
		public string Remove
		{
			get
			{
				return this.GetString("Remove");
			}
			set
			{
				this.SetString("Remove", value);
			}
		}

		// Token: 0x1700533E RID: 21310
		// (get) Token: 0x060110C0 RID: 69824 RVA: 0x003C2F43 File Offset: 0x003C1143
		// (set) Token: 0x060110C1 RID: 69825 RVA: 0x003C2F50 File Offset: 0x003C1150
		[Localizable(true)]
		[DefaultValue("Add")]
		[NotifyParentProperty(true)]
		public string Add
		{
			get
			{
				return this.GetString("Add");
			}
			set
			{
				this.SetString("Add", value);
			}
		}

		// Token: 0x1700533F RID: 21311
		// (get) Token: 0x060110C2 RID: 69826 RVA: 0x003C2F5E File Offset: 0x003C115E
		// (set) Token: 0x060110C3 RID: 69827 RVA: 0x003C2F6B File Offset: 0x003C116B
		[DefaultValue("Clear")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		public string Clear
		{
			get
			{
				return this.GetString("Clear");
			}
			set
			{
				this.SetString("Clear", value);
			}
		}

		// Token: 0x17005340 RID: 21312
		// (get) Token: 0x060110C4 RID: 69828 RVA: 0x003C2F79 File Offset: 0x003C1179
		// (set) Token: 0x060110C5 RID: 69829 RVA: 0x003C2F86 File Offset: 0x003C1186
		[Localizable(true)]
		[DefaultValue("Delete")]
		[NotifyParentProperty(true)]
		public string Delete
		{
			get
			{
				return this.GetString("Delete");
			}
			set
			{
				this.SetString("Delete", value);
			}
		}
	}
}
