using System;
using System.Collections.Generic;
using System.Xml.Linq;
using WebGrease.Extensions;

namespace WebGrease.Configuration
{
	// Token: 0x020000F2 RID: 242
	internal class CssMinificationConfig : INamedConfig
	{
		// Token: 0x06000F78 RID: 3960 RVA: 0x000473CC File Offset: 0x000455CC
		public CssMinificationConfig()
		{
			this.ShouldMinify = true;
			this.ForbiddenSelectors = new string[0];
			this.RemoveSelectors = new string[0];
			this.NonMergeSelectors = new string[0];
		}

		// Token: 0x06000F79 RID: 3961 RVA: 0x00047400 File Offset: 0x00045600
		public CssMinificationConfig(XElement element) : this()
		{
			this.Name = (((string)element.Attribute("config")) ?? string.Empty);
			foreach (XElement xelement in element.Descendants())
			{
				string text = xelement.Name.ToString();
				string value = xelement.Value;
				string key;
				switch (key = text)
				{
				case "MergeMediaQueries":
					this.ShouldMergeMediaQueries = value.TryParseBool();
					break;
				case "Optimize":
					this.ShouldOptimize = value.TryParseBool();
					break;
				case "Minify":
					this.ShouldMinify = value.TryParseBool();
					break;
				case "ValidateLowerCase":
				case "ValidateForLowerCase":
					this.ShouldValidateLowerCase = value.TryParseBool();
					break;
				case "ExcludeProperties":
					this.ShouldExcludeProperties = value.TryParseBool();
					break;
				case "ProhibitedSelectors":
					this.ForbiddenSelectors = (value.IsNullOrWhitespace() ? new string[0] : value.Split(new char[]
					{
						';'
					}, StringSplitOptions.RemoveEmptyEntries));
					break;
				case "RemoveSelectors":
					this.RemoveSelectors = (value.IsNullOrWhitespace() ? new string[0] : value.Split(new char[]
					{
						';'
					}, StringSplitOptions.RemoveEmptyEntries));
					break;
				case "NonMergeSelectors":
					this.NonMergeSelectors = (value.IsNullOrWhitespace() ? new string[0] : value.Split(new char[]
					{
						';'
					}, StringSplitOptions.RemoveEmptyEntries));
					break;
				case "PreventOrderBasedConflict":
					this.ShouldPreventOrderBasedConflict = value.TryParseBool();
					break;
				case "MergeBasedOnCommonDeclarations":
					this.ShouldMergeBasedOnCommonDeclarations = value.TryParseBool();
					break;
				}
			}
		}

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06000F7A RID: 3962 RVA: 0x00047688 File Offset: 0x00045888
		// (set) Token: 0x06000F7B RID: 3963 RVA: 0x00047690 File Offset: 0x00045890
		public string Name { get; set; }

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06000F7C RID: 3964 RVA: 0x00047699 File Offset: 0x00045899
		// (set) Token: 0x06000F7D RID: 3965 RVA: 0x000476A1 File Offset: 0x000458A1
		internal bool ShouldMinify { get; set; }

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06000F7E RID: 3966 RVA: 0x000476AA File Offset: 0x000458AA
		// (set) Token: 0x06000F7F RID: 3967 RVA: 0x000476B2 File Offset: 0x000458B2
		internal bool ShouldOptimize { get; set; }

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06000F80 RID: 3968 RVA: 0x000476BB File Offset: 0x000458BB
		// (set) Token: 0x06000F81 RID: 3969 RVA: 0x000476C3 File Offset: 0x000458C3
		internal bool ShouldMergeMediaQueries { get; set; }

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06000F82 RID: 3970 RVA: 0x000476CC File Offset: 0x000458CC
		// (set) Token: 0x06000F83 RID: 3971 RVA: 0x000476D4 File Offset: 0x000458D4
		internal bool ShouldValidateLowerCase { get; set; }

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06000F84 RID: 3972 RVA: 0x000476DD File Offset: 0x000458DD
		// (set) Token: 0x06000F85 RID: 3973 RVA: 0x000476E5 File Offset: 0x000458E5
		internal bool ShouldExcludeProperties { get; set; }

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06000F86 RID: 3974 RVA: 0x000476EE File Offset: 0x000458EE
		// (set) Token: 0x06000F87 RID: 3975 RVA: 0x000476F6 File Offset: 0x000458F6
		internal bool ShouldPreventOrderBasedConflict { get; set; }

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06000F88 RID: 3976 RVA: 0x000476FF File Offset: 0x000458FF
		// (set) Token: 0x06000F89 RID: 3977 RVA: 0x00047707 File Offset: 0x00045907
		internal bool ShouldMergeBasedOnCommonDeclarations { get; set; }

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06000F8A RID: 3978 RVA: 0x00047710 File Offset: 0x00045910
		// (set) Token: 0x06000F8B RID: 3979 RVA: 0x00047718 File Offset: 0x00045918
		internal IEnumerable<string> ForbiddenSelectors { get; set; }

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06000F8C RID: 3980 RVA: 0x00047721 File Offset: 0x00045921
		// (set) Token: 0x06000F8D RID: 3981 RVA: 0x00047729 File Offset: 0x00045929
		internal IEnumerable<string> RemoveSelectors { get; set; }

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06000F8E RID: 3982 RVA: 0x00047732 File Offset: 0x00045932
		// (set) Token: 0x06000F8F RID: 3983 RVA: 0x0004773A File Offset: 0x0004593A
		internal IEnumerable<string> NonMergeSelectors { get; set; }
	}
}
