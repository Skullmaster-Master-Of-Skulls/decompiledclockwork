using System;
using System.Collections.ObjectModel;

namespace System.ServiceModel.Description
{
	// Token: 0x02000439 RID: 1081
	public sealed class ServiceHealthSectionCollection : Collection<ServiceHealthSection>
	{
		// Token: 0x06002A2D RID: 10797 RVA: 0x000A3158 File Offset: 0x000A1358
		public ServiceHealthSection CreateSection(string title)
		{
			if (title == null)
			{
				throw new ArgumentNullException("title");
			}
			ServiceHealthSection serviceHealthSection = new ServiceHealthSection(title);
			base.Add(serviceHealthSection);
			return serviceHealthSection;
		}

		// Token: 0x06002A2E RID: 10798 RVA: 0x000A3184 File Offset: 0x000A1384
		public ServiceHealthSection CreateSection(string title, string backgroundColor)
		{
			if (backgroundColor == null)
			{
				throw new ArgumentNullException("backgroundColor");
			}
			ServiceHealthSection serviceHealthSection = this.CreateSection(title);
			serviceHealthSection.BackgroundColor = backgroundColor;
			return serviceHealthSection;
		}

		// Token: 0x06002A2F RID: 10799 RVA: 0x000A31B0 File Offset: 0x000A13B0
		public ServiceHealthSection CreateSection(string title, string backgroundColor, string foregroundColor)
		{
			if (foregroundColor == null)
			{
				throw new ArgumentNullException("foregroundColor");
			}
			ServiceHealthSection serviceHealthSection = this.CreateSection(title, backgroundColor);
			serviceHealthSection.ForegroundColor = foregroundColor;
			return serviceHealthSection;
		}
	}
}
