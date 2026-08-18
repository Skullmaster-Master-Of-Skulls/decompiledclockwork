using System;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design
{
	// Token: 0x02000058 RID: 88
	[Obsolete("Use of this type is not recommended because template editing is handled in ControlDesigner. To support template editing expose template data in the TemplateGroups property and call SetViewFlags(ViewFlags.TemplateEditing, true). http://go.microsoft.com/fwlink/?linkid=14202")]
	public interface ITemplateEditingFrame : IDisposable
	{
		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060002C1 RID: 705
		Style ControlStyle { get; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060002C2 RID: 706
		string Name { get; }

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060002C3 RID: 707
		// (set) Token: 0x060002C4 RID: 708
		int InitialHeight { get; set; }

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060002C5 RID: 709
		// (set) Token: 0x060002C6 RID: 710
		int InitialWidth { get; set; }

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060002C7 RID: 711
		string[] TemplateNames { get; }

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060002C8 RID: 712
		Style[] TemplateStyles { get; }

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060002C9 RID: 713
		// (set) Token: 0x060002CA RID: 714
		TemplateEditingVerb Verb { get; set; }

		// Token: 0x060002CB RID: 715
		void Close(bool saveChanges);

		// Token: 0x060002CC RID: 716
		void Open();

		// Token: 0x060002CD RID: 717
		void Resize(int width, int height);

		// Token: 0x060002CE RID: 718
		void Save();

		// Token: 0x060002CF RID: 719
		void UpdateControlName(string newName);
	}
}
