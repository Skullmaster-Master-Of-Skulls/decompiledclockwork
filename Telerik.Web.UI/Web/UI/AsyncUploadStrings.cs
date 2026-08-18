using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x0200018F RID: 399
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class AsyncUploadStrings : LocalizationStrings
	{
		// Token: 0x06000DA2 RID: 3490 RVA: 0x00033FFD File Offset: 0x000321FD
		internal AsyncUploadStrings(LocalizationProvider provider) : base(provider)
		{
		}

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x06000DA3 RID: 3491 RVA: 0x00034006 File Offset: 0x00032206
		// (set) Token: 0x06000DA4 RID: 3492 RVA: 0x00034013 File Offset: 0x00032213
		[NotifyParentProperty(true)]
		[DefaultValue("Select")]
		[Localizable(true)]
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

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x06000DA5 RID: 3493 RVA: 0x00034021 File Offset: 0x00032221
		// (set) Token: 0x06000DA6 RID: 3494 RVA: 0x0003402E File Offset: 0x0003222E
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

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x06000DA7 RID: 3495 RVA: 0x0003403C File Offset: 0x0003223C
		// (set) Token: 0x06000DA8 RID: 3496 RVA: 0x00034049 File Offset: 0x00032249
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

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x06000DA9 RID: 3497 RVA: 0x00034057 File Offset: 0x00032257
		// (set) Token: 0x06000DAA RID: 3498 RVA: 0x00034064 File Offset: 0x00032264
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue("Drop files here")]
		public string DropZone
		{
			get
			{
				return this.GetString("DropZone");
			}
			set
			{
				this.SetString("DropZone", value);
			}
		}
	}
}
