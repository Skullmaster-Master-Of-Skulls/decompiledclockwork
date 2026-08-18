using System;
using System.Collections;
using System.ComponentModel;
using Telerik.Web.UI.Calendar;
using Telerik.Web.UI.Calendar.Persistence;

namespace Telerik.Web.UI
{
	// Token: 0x02001006 RID: 4102
	public class RadDate : PropertiesObject, IClientData
	{
		// Token: 0x0600A049 RID: 41033 RVA: 0x0023A7F4 File Offset: 0x002389F4
		public RadDate()
		{
			this.Date = DateTime.Today;
		}

		// Token: 0x0600A04A RID: 41034 RVA: 0x0023A807 File Offset: 0x00238A07
		public RadDate(int year, int month, int day)
		{
			this.Date = new DateTime(year, month, day);
		}

		// Token: 0x0600A04B RID: 41035 RVA: 0x0023A81D File Offset: 0x00238A1D
		public RadDate(DateTime inputDate)
		{
			this.Date = inputDate;
		}

		// Token: 0x170032A9 RID: 12969
		// (get) Token: 0x0600A04C RID: 41036 RVA: 0x0023A82C File Offset: 0x00238A2C
		// (set) Token: 0x0600A04D RID: 41037 RVA: 0x0023A85E File Offset: 0x00238A5E
		[NotifyParentProperty(true)]
		[DefaultValue(null)]
		public DateTime Date
		{
			get
			{
				object obj = base.Properties["C"];
				if (!(obj is DateTime))
				{
					return DateTime.MinValue;
				}
				return (DateTime)obj;
			}
			set
			{
				base.Properties["C"] = value;
			}
		}

		// Token: 0x0600A04E RID: 41038 RVA: 0x0023A876 File Offset: 0x00238A76
		ArrayList IClientData.GetClientData()
		{
			return this.GetClientData();
		}

		// Token: 0x0600A04F RID: 41039 RVA: 0x0023A880 File Offset: 0x00238A80
		public virtual ArrayList GetClientData()
		{
			return new ArrayList
			{
				this.Date
			};
		}

		// Token: 0x04002CDF RID: 11487
		internal const string DateID = "C";
	}
}
