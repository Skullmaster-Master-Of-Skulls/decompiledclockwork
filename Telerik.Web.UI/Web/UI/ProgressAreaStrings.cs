using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02001B7B RID: 7035
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class ProgressAreaStrings : LocalizationStrings
	{
		// Token: 0x060110A8 RID: 69800 RVA: 0x003C2E08 File Offset: 0x003C1008
		internal ProgressAreaStrings(LocalizationProvider provider) : base(provider)
		{
		}

		// Token: 0x17005333 RID: 21299
		// (get) Token: 0x060110A9 RID: 69801 RVA: 0x003C2E11 File Offset: 0x003C1011
		// (set) Token: 0x060110AA RID: 69802 RVA: 0x003C2E1E File Offset: 0x003C101E
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue("Cancel")]
		public string Cancel
		{
			get
			{
				return this.GetString("Cancel");
			}
			set
			{
				this.SetString("Cancel", value);
			}
		}

		// Token: 0x17005334 RID: 21300
		// (get) Token: 0x060110AB RID: 69803 RVA: 0x003C2E2C File Offset: 0x003C102C
		// (set) Token: 0x060110AC RID: 69804 RVA: 0x003C2E39 File Offset: 0x003C1039
		[DefaultValue("Uploading file: ")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		public string CurrentFileName
		{
			get
			{
				return this.GetString("CurrentFileName");
			}
			set
			{
				this.SetString("CurrentFileName", value);
			}
		}

		// Token: 0x17005335 RID: 21301
		// (get) Token: 0x060110AD RID: 69805 RVA: 0x003C2E47 File Offset: 0x003C1047
		// (set) Token: 0x060110AE RID: 69806 RVA: 0x003C2E54 File Offset: 0x003C1054
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue("Uploaded files: ")]
		public string UploadedFiles
		{
			get
			{
				return this.GetString("UploadedFiles");
			}
			set
			{
				this.SetString("UploadedFiles", value);
			}
		}

		// Token: 0x17005336 RID: 21302
		// (get) Token: 0x060110AF RID: 69807 RVA: 0x003C2E62 File Offset: 0x003C1062
		// (set) Token: 0x060110B0 RID: 69808 RVA: 0x003C2E6F File Offset: 0x003C106F
		[DefaultValue("Total files: ")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		public string TotalFiles
		{
			get
			{
				return this.GetString("TotalFiles");
			}
			set
			{
				this.SetString("TotalFiles", value);
			}
		}

		// Token: 0x17005337 RID: 21303
		// (get) Token: 0x060110B1 RID: 69809 RVA: 0x003C2E7D File Offset: 0x003C107D
		// (set) Token: 0x060110B2 RID: 69810 RVA: 0x003C2E8A File Offset: 0x003C108A
		[NotifyParentProperty(true)]
		[DefaultValue("Uploaded ")]
		[Localizable(true)]
		public string Uploaded
		{
			get
			{
				return this.GetString("Uploaded");
			}
			set
			{
				this.SetString("Uploaded", value);
			}
		}

		// Token: 0x17005338 RID: 21304
		// (get) Token: 0x060110B3 RID: 69811 RVA: 0x003C2E98 File Offset: 0x003C1098
		// (set) Token: 0x060110B4 RID: 69812 RVA: 0x003C2EA5 File Offset: 0x003C10A5
		[DefaultValue("Total ")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		public string Total
		{
			get
			{
				return this.GetString("Total");
			}
			set
			{
				this.SetString("Total", value);
			}
		}

		// Token: 0x17005339 RID: 21305
		// (get) Token: 0x060110B5 RID: 69813 RVA: 0x003C2EB3 File Offset: 0x003C10B3
		// (set) Token: 0x060110B6 RID: 69814 RVA: 0x003C2EC0 File Offset: 0x003C10C0
		[DefaultValue("Elapsed time: ")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		public string ElapsedTime
		{
			get
			{
				return this.GetString("ElapsedTime");
			}
			set
			{
				this.SetString("ElapsedTime", value);
			}
		}

		// Token: 0x1700533A RID: 21306
		// (get) Token: 0x060110B7 RID: 69815 RVA: 0x003C2ECE File Offset: 0x003C10CE
		// (set) Token: 0x060110B8 RID: 69816 RVA: 0x003C2EDB File Offset: 0x003C10DB
		[NotifyParentProperty(true)]
		[DefaultValue("Estimated time: ")]
		[Localizable(true)]
		public string EstimatedTime
		{
			get
			{
				return this.GetString("EstimatedTime");
			}
			set
			{
				this.SetString("EstimatedTime", value);
			}
		}

		// Token: 0x1700533B RID: 21307
		// (get) Token: 0x060110B9 RID: 69817 RVA: 0x003C2EE9 File Offset: 0x003C10E9
		// (set) Token: 0x060110BA RID: 69818 RVA: 0x003C2EF6 File Offset: 0x003C10F6
		[Localizable(true)]
		[DefaultValue("Speed: ")]
		[NotifyParentProperty(true)]
		public string TransferSpeed
		{
			get
			{
				return this.GetString("TransferSpeed");
			}
			set
			{
				this.SetString("TransferSpeed", value);
			}
		}
	}
}
