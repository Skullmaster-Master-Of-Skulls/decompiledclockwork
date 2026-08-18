using System;

namespace System.Web.UI.Design
{
	// Token: 0x02000048 RID: 72
	public interface IControlDesignerTag
	{
		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600026E RID: 622
		bool IsDirty { get; }

		// Token: 0x0600026F RID: 623
		string GetAttribute(string name);

		// Token: 0x06000270 RID: 624
		string GetContent();

		// Token: 0x06000271 RID: 625
		void RemoveAttribute(string name);

		// Token: 0x06000272 RID: 626
		void SetAttribute(string name, string value);

		// Token: 0x06000273 RID: 627
		void SetContent(string content);

		// Token: 0x06000274 RID: 628
		void SetDirty(bool dirty);

		// Token: 0x06000275 RID: 629
		string GetOuterContent();
	}
}
