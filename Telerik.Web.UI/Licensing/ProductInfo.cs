using System;
using System.Linq;
using System.Reflection;

namespace Telerik.Licensing
{
	// Token: 0x02000406 RID: 1030
	internal class ProductInfo
	{
		// Token: 0x060025A0 RID: 9632 RVA: 0x0007CD23 File Offset: 0x0007AF23
		public ProductInfo(Type type)
		{
			this.Type = type;
			this.ProductType = this.ReadStatus();
			this.ProductName = this.SanitizeProductName(this.ReadProductName());
			this.Version = this.ReadVersion();
		}

		// Token: 0x17000C36 RID: 3126
		// (get) Token: 0x060025A1 RID: 9633 RVA: 0x0007CD5C File Offset: 0x0007AF5C
		// (set) Token: 0x060025A2 RID: 9634 RVA: 0x0007CD64 File Offset: 0x0007AF64
		public ProductType ProductType { get; private set; }

		// Token: 0x17000C37 RID: 3127
		// (get) Token: 0x060025A3 RID: 9635 RVA: 0x0007CD6D File Offset: 0x0007AF6D
		// (set) Token: 0x060025A4 RID: 9636 RVA: 0x0007CD75 File Offset: 0x0007AF75
		public string ProductName { get; private set; }

		// Token: 0x17000C38 RID: 3128
		// (get) Token: 0x060025A5 RID: 9637 RVA: 0x0007CD7E File Offset: 0x0007AF7E
		// (set) Token: 0x060025A6 RID: 9638 RVA: 0x0007CD86 File Offset: 0x0007AF86
		public string Version { get; private set; }

		// Token: 0x17000C39 RID: 3129
		// (get) Token: 0x060025A7 RID: 9639 RVA: 0x0007CD8F File Offset: 0x0007AF8F
		// (set) Token: 0x060025A8 RID: 9640 RVA: 0x0007CD97 File Offset: 0x0007AF97
		protected Type Type { get; set; }

		// Token: 0x060025A9 RID: 9641 RVA: 0x0007CDA0 File Offset: 0x0007AFA0
		public static ProductInfo GetProductInfo(Type type)
		{
			return new ProductInfo(type);
		}

		// Token: 0x060025AA RID: 9642 RVA: 0x0007CDB4 File Offset: 0x0007AFB4
		private ProductType ReadStatus()
		{
			object obj = this.Type.Assembly.GetCustomAttributes(true).FirstOrDefault((object a) => a is AssemblyTitleAttribute);
			if (obj != null && ((AssemblyTitleAttribute)obj).Title.Contains("Trial"))
			{
				return ProductType.Trial;
			}
			return ProductType.Dev;
		}

		// Token: 0x060025AB RID: 9643 RVA: 0x0007CE12 File Offset: 0x0007B012
		private string SanitizeProductName(string name)
		{
			if (name.IndexOf("design time", StringComparison.InvariantCultureIgnoreCase) >= 0)
			{
				name = name.Replace("design time", string.Empty);
			}
			return name.Trim();
		}

		// Token: 0x060025AC RID: 9644 RVA: 0x0007CE48 File Offset: 0x0007B048
		private string ReadProductName()
		{
			object obj = this.Type.Assembly.GetCustomAttributes(true).FirstOrDefault((object a) => a is AssemblyDescriptionAttribute);
			if (obj == null)
			{
				return string.Empty;
			}
			return ((AssemblyDescriptionAttribute)obj).Description;
		}

		// Token: 0x060025AD RID: 9645 RVA: 0x0007CEA8 File Offset: 0x0007B0A8
		private string ReadVersion()
		{
			object obj = this.Type.Assembly.GetCustomAttributes(true).FirstOrDefault((object a) => a is AssemblyFileVersionAttribute);
			if (obj == null)
			{
				return string.Empty;
			}
			return ((AssemblyFileVersionAttribute)obj).Version;
		}
	}
}
