using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x020008D6 RID: 2262
	internal class SpreadsheetToolInfo
	{
		// Token: 0x17001C25 RID: 7205
		// (get) Token: 0x0600551C RID: 21788 RVA: 0x001042CC File Offset: 0x001024CC
		// (set) Token: 0x0600551D RID: 21789 RVA: 0x001042D4 File Offset: 0x001024D4
		public string CommandName { get; set; }

		// Token: 0x17001C26 RID: 7206
		// (get) Token: 0x0600551E RID: 21790 RVA: 0x001042DD File Offset: 0x001024DD
		// (set) Token: 0x0600551F RID: 21791 RVA: 0x001042E5 File Offset: 0x001024E5
		public string CommandArgument { get; set; }

		// Token: 0x17001C27 RID: 7207
		// (get) Token: 0x06005520 RID: 21792 RVA: 0x001042EE File Offset: 0x001024EE
		// (set) Token: 0x06005521 RID: 21793 RVA: 0x001042F6 File Offset: 0x001024F6
		public string Value { get; set; }

		// Token: 0x17001C28 RID: 7208
		// (get) Token: 0x06005522 RID: 21794 RVA: 0x001042FF File Offset: 0x001024FF
		// (set) Token: 0x06005523 RID: 21795 RVA: 0x00104307 File Offset: 0x00102507
		public string Group { get; set; }

		// Token: 0x17001C29 RID: 7209
		// (get) Token: 0x06005524 RID: 21796 RVA: 0x00104310 File Offset: 0x00102510
		// (set) Token: 0x06005525 RID: 21797 RVA: 0x00104318 File Offset: 0x00102518
		public string IconClass { get; set; }

		// Token: 0x17001C2A RID: 7210
		// (get) Token: 0x06005526 RID: 21798 RVA: 0x00104321 File Offset: 0x00102521
		// (set) Token: 0x06005527 RID: 21799 RVA: 0x00104329 File Offset: 0x00102529
		public string LocalizationTextKey { get; set; }

		// Token: 0x17001C2B RID: 7211
		// (get) Token: 0x06005528 RID: 21800 RVA: 0x00104332 File Offset: 0x00102532
		// (set) Token: 0x06005529 RID: 21801 RVA: 0x0010433A File Offset: 0x0010253A
		public List<SpreadsheetToolInfo> ChildTools { get; set; }

		// Token: 0x0600552A RID: 21802 RVA: 0x00104343 File Offset: 0x00102543
		public SpreadsheetToolInfo(string commandName, string commandArgument, string value, string group, string iconClass, string localizationTextKey) : this(commandName, commandArgument, value, group, iconClass, localizationTextKey, new List<SpreadsheetToolInfo>())
		{
		}

		// Token: 0x0600552B RID: 21803 RVA: 0x00104359 File Offset: 0x00102559
		public SpreadsheetToolInfo(string commandName, string commandArgument, string value, string group, string iconClass, string localizationTextKey, List<SpreadsheetToolInfo> childTools)
		{
			this.CommandName = commandName;
			this.CommandArgument = commandArgument;
			this.Value = value;
			this.Group = group;
			this.IconClass = iconClass;
			this.LocalizationTextKey = localizationTextKey;
			this.ChildTools = childTools;
		}
	}
}
