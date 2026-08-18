using System;
using System.ComponentModel;

namespace Telerik.Web.UI.Map
{
	// Token: 0x0200059D RID: 1437
	public class Location : StateManager, IDefaultCheck
	{
		// Token: 0x170010BA RID: 4282
		// (get) Token: 0x0600337C RID: 13180 RVA: 0x000AB6BA File Offset: 0x000A98BA
		// (set) Token: 0x0600337D RID: 13181 RVA: 0x000AB6E3 File Offset: 0x000A98E3
		[DefaultValue(0.0)]
		public double Latitude
		{
			get
			{
				return (double)(base.ViewState["Latitude"] ?? 0.0);
			}
			set
			{
				base.ViewState["Latitude"] = value;
			}
		}

		// Token: 0x170010BB RID: 4283
		// (get) Token: 0x0600337E RID: 13182 RVA: 0x000AB6FB File Offset: 0x000A98FB
		// (set) Token: 0x0600337F RID: 13183 RVA: 0x000AB724 File Offset: 0x000A9924
		[DefaultValue(0.0)]
		public double Longitude
		{
			get
			{
				return (double)(base.ViewState["Longitude"] ?? 0.0);
			}
			set
			{
				base.ViewState["Longitude"] = value;
			}
		}

		// Token: 0x170010BC RID: 4284
		// (get) Token: 0x06003380 RID: 13184 RVA: 0x000AB73C File Offset: 0x000A993C
		public bool IsDefault
		{
			get
			{
				return this.Latitude == 0.0 && this.Longitude == 0.0;
			}
		}

		// Token: 0x06003381 RID: 13185 RVA: 0x000AB764 File Offset: 0x000A9964
		internal double[] ToArray()
		{
			return new double[]
			{
				this.Latitude,
				this.Longitude
			};
		}
	}
}
