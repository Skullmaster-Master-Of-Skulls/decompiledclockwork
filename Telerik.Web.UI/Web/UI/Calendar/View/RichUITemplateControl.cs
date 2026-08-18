using System;
using System.Collections;
using System.ComponentModel;
using Telerik.Web.UI.Calendar.Persistence;
using Telerik.Web.UI.Calendar.TypeConverters;

namespace Telerik.Web.UI.Calendar.View
{
	// Token: 0x02001015 RID: 4117
	public class RichUITemplateControl : PropertiesObject, IClientData
	{
		// Token: 0x0600A1F6 RID: 41462 RVA: 0x00240448 File Offset: 0x0023E648
		public void Reset()
		{
			if (base.Properties != null)
			{
				base.Properties.Clear();
			}
		}

		// Token: 0x0600A1F7 RID: 41463 RVA: 0x0024045D File Offset: 0x0023E65D
		public RichUITemplateControl()
		{
		}

		// Token: 0x0600A1F8 RID: 41464 RVA: 0x00240465 File Offset: 0x0023E665
		public RichUITemplateControl(RadCalendar calendar)
		{
			this._ParentCalendar = calendar;
		}

		// Token: 0x1700333B RID: 13115
		// (get) Token: 0x0600A1F9 RID: 41465 RVA: 0x00240474 File Offset: 0x0023E674
		// (set) Token: 0x0600A1FA RID: 41466 RVA: 0x0024047C File Offset: 0x0023E67C
		internal RadCalendar ParentCalendar
		{
			get
			{
				return this._ParentCalendar;
			}
			set
			{
				this._ParentCalendar = value;
			}
		}

		// Token: 0x1700333C RID: 13116
		// (get) Token: 0x0600A1FB RID: 41467 RVA: 0x00240485 File Offset: 0x0023E685
		// (set) Token: 0x0600A1FC RID: 41468 RVA: 0x002404A5 File Offset: 0x0023E6A5
		[NotifyParentProperty(true)]
		[TypeConverter(typeof(TemplateListTypeConverter))]
		[DefaultValue("")]
		public string TemplateID
		{
			get
			{
				return (base.Properties["B"] as string) ?? string.Empty;
			}
			set
			{
				base.Properties["B"] = value;
			}
		}

		// Token: 0x0600A1FD RID: 41469 RVA: 0x002404B8 File Offset: 0x0023E6B8
		ArrayList IClientData.GetClientData()
		{
			return this.GetClientData();
		}

		// Token: 0x0600A1FE RID: 41470 RVA: 0x002404C0 File Offset: 0x0023E6C0
		public virtual ArrayList GetClientData()
		{
			return new ArrayList
			{
				this.TemplateID
			};
		}

		// Token: 0x04002D0C RID: 11532
		internal const string ItemStyleInternalID = "A";

		// Token: 0x04002D0D RID: 11533
		internal const string ContentTemplateID = "B";

		// Token: 0x04002D0E RID: 11534
		private RadCalendar _ParentCalendar;
	}
}
