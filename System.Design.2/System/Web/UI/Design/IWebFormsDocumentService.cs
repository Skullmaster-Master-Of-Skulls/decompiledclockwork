using System;

namespace System.Web.UI.Design
{
	// Token: 0x0200005F RID: 95
	[Obsolete("The recommended alternative is System.Web.UI.Design.WebFormsRootDesigner. The WebFormsRootDesigner contains additional functionality and allows for more extensibility. To get the WebFormsRootDesigner use the RootDesigner property from your ControlDesigner. http://go.microsoft.com/fwlink/?linkid=14202")]
	public interface IWebFormsDocumentService
	{
		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060002E1 RID: 737
		string DocumentUrl { get; }

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060002E2 RID: 738
		bool IsLoading { get; }

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x060002E3 RID: 739
		// (remove) Token: 0x060002E4 RID: 740
		event EventHandler LoadComplete;

		// Token: 0x060002E5 RID: 741
		object CreateDiscardableUndoUnit();

		// Token: 0x060002E6 RID: 742
		void DiscardUndoUnit(object discardableUndoUnit);

		// Token: 0x060002E7 RID: 743
		void EnableUndo(bool enable);

		// Token: 0x060002E8 RID: 744
		void UpdateSelection();
	}
}
