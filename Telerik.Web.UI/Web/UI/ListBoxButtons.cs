using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x0200193E RID: 6462
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class ListBoxButtons : LocalizationStrings
	{
		// Token: 0x0600F9FC RID: 63996 RVA: 0x00385787 File Offset: 0x00383987
		internal ListBoxButtons(LocalizationProvider provider) : base(provider)
		{
		}

		// Token: 0x17004B84 RID: 19332
		// (get) Token: 0x0600F9FD RID: 63997 RVA: 0x00385790 File Offset: 0x00383990
		// (set) Token: 0x0600F9FE RID: 63998 RVA: 0x0038579D File Offset: 0x0038399D
		[NotifyParentProperty(true)]
		[DefaultValue("Move Up")]
		[Localizable(true)]
		public string MoveUp
		{
			get
			{
				return this.GetString("MoveUp");
			}
			set
			{
				this.SetString("MoveUp", value);
			}
		}

		// Token: 0x17004B85 RID: 19333
		// (get) Token: 0x0600F9FF RID: 63999 RVA: 0x003857AB File Offset: 0x003839AB
		// (set) Token: 0x0600FA00 RID: 64000 RVA: 0x003857B8 File Offset: 0x003839B8
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[DefaultValue("Move Down")]
		public string MoveDown
		{
			get
			{
				return this.GetString("MoveDown");
			}
			set
			{
				this.SetString("MoveDown", value);
			}
		}

		// Token: 0x17004B86 RID: 19334
		// (get) Token: 0x0600FA01 RID: 64001 RVA: 0x003857C6 File Offset: 0x003839C6
		// (set) Token: 0x0600FA02 RID: 64002 RVA: 0x003857D3 File Offset: 0x003839D3
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[DefaultValue("Move Top")]
		public string MoveTop
		{
			get
			{
				return this.GetString("MoveTop");
			}
			set
			{
				this.SetString("MoveTop", value);
			}
		}

		// Token: 0x17004B87 RID: 19335
		// (get) Token: 0x0600FA03 RID: 64003 RVA: 0x003857E1 File Offset: 0x003839E1
		// (set) Token: 0x0600FA04 RID: 64004 RVA: 0x003857EE File Offset: 0x003839EE
		[Localizable(true)]
		[DefaultValue("Move Bottom")]
		[NotifyParentProperty(true)]
		public string MoveBottom
		{
			get
			{
				return this.GetString("MoveBottom");
			}
			set
			{
				this.SetString("MoveBottom", value);
			}
		}

		// Token: 0x17004B88 RID: 19336
		// (get) Token: 0x0600FA05 RID: 64005 RVA: 0x003857FC File Offset: 0x003839FC
		// (set) Token: 0x0600FA06 RID: 64006 RVA: 0x00385809 File Offset: 0x00383A09
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue("Delete")]
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

		// Token: 0x17004B89 RID: 19337
		// (get) Token: 0x0600FA07 RID: 64007 RVA: 0x00385817 File Offset: 0x00383A17
		// (set) Token: 0x0600FA08 RID: 64008 RVA: 0x00385824 File Offset: 0x00383A24
		[NotifyParentProperty(true)]
		[DefaultValue("To Left")]
		[Localizable(true)]
		public string ToLeft
		{
			get
			{
				return this.GetString("ToLeft");
			}
			set
			{
				this.SetString("ToLeft", value);
			}
		}

		// Token: 0x17004B8A RID: 19338
		// (get) Token: 0x0600FA09 RID: 64009 RVA: 0x00385832 File Offset: 0x00383A32
		// (set) Token: 0x0600FA0A RID: 64010 RVA: 0x0038583F File Offset: 0x00383A3F
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[DefaultValue("To Right")]
		public string ToRight
		{
			get
			{
				return this.GetString("ToRight");
			}
			set
			{
				this.SetString("ToRight", value);
			}
		}

		// Token: 0x17004B8B RID: 19339
		// (get) Token: 0x0600FA0B RID: 64011 RVA: 0x0038584D File Offset: 0x00383A4D
		// (set) Token: 0x0600FA0C RID: 64012 RVA: 0x0038585A File Offset: 0x00383A5A
		[DefaultValue("To Top")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		public string ToTop
		{
			get
			{
				return this.GetString("ToTop");
			}
			set
			{
				this.SetString("ToTop", value);
			}
		}

		// Token: 0x17004B8C RID: 19340
		// (get) Token: 0x0600FA0D RID: 64013 RVA: 0x00385868 File Offset: 0x00383A68
		// (set) Token: 0x0600FA0E RID: 64014 RVA: 0x00385875 File Offset: 0x00383A75
		[NotifyParentProperty(true)]
		[DefaultValue("To Bottom")]
		[Localizable(true)]
		public string ToBottom
		{
			get
			{
				return this.GetString("ToBottom");
			}
			set
			{
				this.SetString("ToBottom", value);
			}
		}

		// Token: 0x17004B8D RID: 19341
		// (get) Token: 0x0600FA0F RID: 64015 RVA: 0x00385883 File Offset: 0x00383A83
		// (set) Token: 0x0600FA10 RID: 64016 RVA: 0x00385890 File Offset: 0x00383A90
		[NotifyParentProperty(true)]
		[DefaultValue("All to Top")]
		[Localizable(true)]
		public string AllToTop
		{
			get
			{
				return this.GetString("AllToTop");
			}
			set
			{
				this.SetString("AllToTop", value);
			}
		}

		// Token: 0x17004B8E RID: 19342
		// (get) Token: 0x0600FA11 RID: 64017 RVA: 0x0038589E File Offset: 0x00383A9E
		// (set) Token: 0x0600FA12 RID: 64018 RVA: 0x003858AB File Offset: 0x00383AAB
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue("All to Bottom")]
		public string AllToBottom
		{
			get
			{
				return this.GetString("AllToBottom");
			}
			set
			{
				this.SetString("AllToBottom", value);
			}
		}

		// Token: 0x17004B8F RID: 19343
		// (get) Token: 0x0600FA13 RID: 64019 RVA: 0x003858B9 File Offset: 0x00383AB9
		// (set) Token: 0x0600FA14 RID: 64020 RVA: 0x003858C6 File Offset: 0x00383AC6
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue("All to Left")]
		public string AllToLeft
		{
			get
			{
				return this.GetString("AllToLeft");
			}
			set
			{
				this.SetString("AllToLeft", value);
			}
		}

		// Token: 0x17004B90 RID: 19344
		// (get) Token: 0x0600FA15 RID: 64021 RVA: 0x003858D4 File Offset: 0x00383AD4
		// (set) Token: 0x0600FA16 RID: 64022 RVA: 0x003858E1 File Offset: 0x00383AE1
		[DefaultValue("All to Right")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		public string AllToRight
		{
			get
			{
				return this.GetString("AllToRight");
			}
			set
			{
				this.SetString("AllToRight", value);
			}
		}

		// Token: 0x17004B91 RID: 19345
		// (get) Token: 0x0600FA17 RID: 64023 RVA: 0x003858EF File Offset: 0x00383AEF
		// (set) Token: 0x0600FA18 RID: 64024 RVA: 0x003858FC File Offset: 0x00383AFC
		[NotifyParentProperty(true)]
		[DefaultValue("Check All")]
		[Localizable(true)]
		public string CheckAll
		{
			get
			{
				return this.GetString("CheckAll");
			}
			set
			{
				this.SetString("CheckAll", value);
			}
		}
	}
}
