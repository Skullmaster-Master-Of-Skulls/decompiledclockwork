using System;
using System.Collections.ObjectModel;

namespace System.ServiceModel.Description
{
	// Token: 0x02000438 RID: 1080
	public sealed class ServiceHealthSection : Collection<ServiceHealthDataCollection>
	{
		// Token: 0x06002A23 RID: 10787 RVA: 0x000A3088 File Offset: 0x000A1288
		public ServiceHealthSection()
		{
			this.BackgroundColor = ServiceHealthSection.DefaultBackgroundColor;
			this.ForegroundColor = ServiceHealthSection.DefaultForegroundColor;
		}

		// Token: 0x06002A24 RID: 10788 RVA: 0x000A30A6 File Offset: 0x000A12A6
		public ServiceHealthSection(string title) : this()
		{
			this.Title = title;
		}

		// Token: 0x17000A4D RID: 2637
		// (get) Token: 0x06002A25 RID: 10789 RVA: 0x000A30B5 File Offset: 0x000A12B5
		// (set) Token: 0x06002A26 RID: 10790 RVA: 0x000A30BD File Offset: 0x000A12BD
		public string BackgroundColor
		{
			get
			{
				return this.backgroundColor;
			}
			set
			{
				if (string.IsNullOrWhiteSpace(value))
				{
					throw new ArgumentNullException("BackgroundColor");
				}
				this.backgroundColor = value;
			}
		}

		// Token: 0x17000A4E RID: 2638
		// (get) Token: 0x06002A27 RID: 10791 RVA: 0x000A30D9 File Offset: 0x000A12D9
		// (set) Token: 0x06002A28 RID: 10792 RVA: 0x000A30E1 File Offset: 0x000A12E1
		public string ForegroundColor
		{
			get
			{
				return this.foregroundColor;
			}
			set
			{
				if (string.IsNullOrWhiteSpace(value))
				{
					throw new ArgumentNullException("ForegroundColor");
				}
				this.foregroundColor = value;
			}
		}

		// Token: 0x17000A4F RID: 2639
		// (get) Token: 0x06002A29 RID: 10793 RVA: 0x000A30FD File Offset: 0x000A12FD
		// (set) Token: 0x06002A2A RID: 10794 RVA: 0x000A3105 File Offset: 0x000A1305
		public string Title
		{
			get
			{
				return this.title;
			}
			set
			{
				if (string.IsNullOrWhiteSpace(value))
				{
					throw new ArgumentNullException("Title");
				}
				this.title = value;
			}
		}

		// Token: 0x06002A2B RID: 10795 RVA: 0x000A3124 File Offset: 0x000A1324
		public ServiceHealthDataCollection CreateElementsCollection()
		{
			ServiceHealthDataCollection serviceHealthDataCollection = new ServiceHealthDataCollection();
			base.Add(serviceHealthDataCollection);
			return serviceHealthDataCollection;
		}

		// Token: 0x040022B9 RID: 8889
		private static string DefaultForegroundColor = "#000000";

		// Token: 0x040022BA RID: 8890
		private static string DefaultBackgroundColor = "#ffffff";

		// Token: 0x040022BB RID: 8891
		private string backgroundColor;

		// Token: 0x040022BC RID: 8892
		private string foregroundColor;

		// Token: 0x040022BD RID: 8893
		private string title;
	}
}
