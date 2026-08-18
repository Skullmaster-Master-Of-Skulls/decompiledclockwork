using System;
using System.IO;
using System.Xml.Linq;

namespace WebGrease.Configuration
{
	// Token: 0x020000F4 RID: 244
	public class InputSpec
	{
		// Token: 0x06000FAA RID: 4010 RVA: 0x00047AA8 File Offset: 0x00045CA8
		public InputSpec()
		{
			this.SearchOption = SearchOption.AllDirectories;
		}

		// Token: 0x06000FAB RID: 4011 RVA: 0x00047AB8 File Offset: 0x00045CB8
		internal InputSpec(XElement element, string sourceDirectory)
		{
			XAttribute xattribute = element.Attribute("optional");
			bool isOptional;
			if (xattribute != null && bool.TryParse(xattribute.Value, out isOptional))
			{
				this.IsOptional = isOptional;
			}
			XAttribute xattribute2 = element.Attribute("searchPattern");
			this.SearchPattern = ((xattribute2 != null) ? xattribute2.Value : string.Empty);
			XAttribute xattribute3 = element.Attribute("searchOption");
			if (xattribute3 != null)
			{
				SearchOption searchOption;
				this.SearchOption = (Enum.TryParse<SearchOption>(xattribute3.Value, out searchOption) ? searchOption : SearchOption.AllDirectories);
			}
			else
			{
				this.SearchOption = SearchOption.AllDirectories;
			}
			if (!string.IsNullOrWhiteSpace(element.Value))
			{
				this.Path = System.IO.Path.GetFullPath(System.IO.Path.Combine(sourceDirectory, element.Value));
			}
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06000FAC RID: 4012 RVA: 0x00047B77 File Offset: 0x00045D77
		// (set) Token: 0x06000FAD RID: 4013 RVA: 0x00047B7F File Offset: 0x00045D7F
		public string Path { get; set; }

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06000FAE RID: 4014 RVA: 0x00047B88 File Offset: 0x00045D88
		// (set) Token: 0x06000FAF RID: 4015 RVA: 0x00047B90 File Offset: 0x00045D90
		public string SearchPattern { get; set; }

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06000FB0 RID: 4016 RVA: 0x00047B99 File Offset: 0x00045D99
		// (set) Token: 0x06000FB1 RID: 4017 RVA: 0x00047BA1 File Offset: 0x00045DA1
		public SearchOption SearchOption { get; set; }

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06000FB2 RID: 4018 RVA: 0x00047BAA File Offset: 0x00045DAA
		// (set) Token: 0x06000FB3 RID: 4019 RVA: 0x00047BB2 File Offset: 0x00045DB2
		public bool IsOptional { get; set; }

		// Token: 0x06000FB4 RID: 4020 RVA: 0x00047BBC File Offset: 0x00045DBC
		public override bool Equals(object obj)
		{
			InputSpec inputSpec = obj as InputSpec;
			return inputSpec != null && (inputSpec.Path == this.Path && inputSpec.SearchOption == this.SearchOption && inputSpec.SearchPattern == this.SearchPattern) && inputSpec.IsOptional == this.IsOptional;
		}

		// Token: 0x06000FB5 RID: 4021 RVA: 0x00047C1C File Offset: 0x00045E1C
		public override int GetHashCode()
		{
			int num = 17;
			num = num * 23 + InputSpec.GetObjectHashCode(this.Path);
			num = num * 23 + InputSpec.GetObjectHashCode(this.SearchOption);
			num = num * 23 + InputSpec.GetObjectHashCode(this.SearchPattern);
			return num * 23 + this.IsOptional.GetHashCode();
		}

		// Token: 0x06000FB6 RID: 4022 RVA: 0x00047C79 File Offset: 0x00045E79
		private static int GetObjectHashCode(object obj)
		{
			if (obj == null)
			{
				return 0;
			}
			return obj.GetHashCode();
		}
	}
}
