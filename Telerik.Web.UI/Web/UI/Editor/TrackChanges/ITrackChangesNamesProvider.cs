using System;

namespace Telerik.Web.UI.Editor.TrackChanges
{
	// Token: 0x02000B56 RID: 2902
	public interface ITrackChangesNamesProvider
	{
		// Token: 0x170023D7 RID: 9175
		// (get) Token: 0x06006D5C RID: 27996
		string DeleteTagName { get; }

		// Token: 0x170023D8 RID: 9176
		// (get) Token: 0x06006D5D RID: 27997
		string InsertTagName { get; }

		// Token: 0x170023D9 RID: 9177
		// (get) Token: 0x06006D5E RID: 27998
		string TitleAttribute { get; }

		// Token: 0x170023DA RID: 9178
		// (get) Token: 0x06006D5F RID: 27999
		string TimestampAttribute { get; }

		// Token: 0x170023DB RID: 9179
		// (get) Token: 0x06006D60 RID: 28000
		string CiteAttribute { get; }

		// Token: 0x170023DC RID: 9180
		// (get) Token: 0x06006D61 RID: 28001
		string AuthorAttribute { get; }

		// Token: 0x170023DD RID: 9181
		// (get) Token: 0x06006D62 RID: 28002
		string BrowserCommandAttribute { get; }

		// Token: 0x170023DE RID: 9182
		// (get) Token: 0x06006D63 RID: 28003
		string AlignOrigAttribute { get; }
	}
}
