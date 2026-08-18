using System;
using System.Globalization;

namespace Telerik.Web.UI.PivotGrid.DataProviders.Adomd
{
	// Token: 0x02000D56 RID: 3414
	public struct AdomdConnectionSettings
	{
		// Token: 0x1700288E RID: 10382
		// (get) Token: 0x06007F43 RID: 32579 RVA: 0x001D149D File Offset: 0x001CF69D
		// (set) Token: 0x06007F44 RID: 32580 RVA: 0x001D14A5 File Offset: 0x001CF6A5
		public string Cube { get; set; }

		// Token: 0x1700288F RID: 10383
		// (get) Token: 0x06007F45 RID: 32581 RVA: 0x001D14AE File Offset: 0x001CF6AE
		// (set) Token: 0x06007F46 RID: 32582 RVA: 0x001D14B6 File Offset: 0x001CF6B6
		public string Database { get; set; }

		// Token: 0x17002890 RID: 10384
		// (get) Token: 0x06007F47 RID: 32583 RVA: 0x001D14BF File Offset: 0x001CF6BF
		// (set) Token: 0x06007F48 RID: 32584 RVA: 0x001D14C7 File Offset: 0x001CF6C7
		public string ConnectionString { get; set; }

		// Token: 0x06007F49 RID: 32585 RVA: 0x001D14D0 File Offset: 0x001CF6D0
		public override bool Equals(object obj)
		{
			if (obj is AdomdConnectionSettings)
			{
				AdomdConnectionSettings adomdConnectionSettings = (AdomdConnectionSettings)obj;
				return this.Cube == adomdConnectionSettings.Cube && this.Database == adomdConnectionSettings.Database && this.ConnectionString == adomdConnectionSettings.ConnectionString;
			}
			return false;
		}

		// Token: 0x06007F4A RID: 32586 RVA: 0x001D152A File Offset: 0x001CF72A
		public override int GetHashCode()
		{
			return this.Cube.GetHashCode() ^ this.Database.GetHashCode() ^ this.ConnectionString.GetHashCode();
		}

		// Token: 0x06007F4B RID: 32587 RVA: 0x001D1550 File Offset: 0x001CF750
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "Database: {0}; Cube: {1}; Connection string: {2}", new object[]
			{
				this.Database,
				this.Cube,
				this.ConnectionString
			});
		}

		// Token: 0x06007F4C RID: 32588 RVA: 0x001D158F File Offset: 0x001CF78F
		public static bool operator ==(AdomdConnectionSettings left, AdomdConnectionSettings right)
		{
			return left != null && right != null && left.Equals(right);
		}

		// Token: 0x06007F4D RID: 32589 RVA: 0x001D15B6 File Offset: 0x001CF7B6
		public static bool operator !=(AdomdConnectionSettings left, AdomdConnectionSettings right)
		{
			return !(left == right);
		}
	}
}
