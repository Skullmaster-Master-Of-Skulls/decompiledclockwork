using System;

namespace Spire.Xls.Core
{
	// Token: 0x020005DA RID: 1498
	public interface IDocumentProperty
	{
		// Token: 0x17000DBC RID: 3516
		// (get) Token: 0x06005941 RID: 22849
		bool IsBuiltIn { get; }

		// Token: 0x17000DBD RID: 3517
		// (get) Token: 0x06005942 RID: 22850
		BuiltInPropertyType PropertyId { get; }

		// Token: 0x17000DBE RID: 3518
		// (get) Token: 0x06005943 RID: 22851
		string Name { get; }

		// Token: 0x17000DBF RID: 3519
		// (get) Token: 0x06005944 RID: 22852
		// (set) Token: 0x06005945 RID: 22853
		object Value { get; set; }

		// Token: 0x17000DC0 RID: 3520
		// (get) Token: 0x06005946 RID: 22854
		// (set) Token: 0x06005947 RID: 22855
		bool Boolean { get; set; }

		// Token: 0x17000DC1 RID: 3521
		// (get) Token: 0x06005948 RID: 22856
		// (set) Token: 0x06005949 RID: 22857
		int Integer { get; set; }

		// Token: 0x17000DC2 RID: 3522
		// (get) Token: 0x0600594A RID: 22858
		// (set) Token: 0x0600594B RID: 22859
		int Int32 { get; set; }

		// Token: 0x17000DC3 RID: 3523
		// (get) Token: 0x0600594C RID: 22860
		// (set) Token: 0x0600594D RID: 22861
		double Double { get; set; }

		// Token: 0x17000DC4 RID: 3524
		// (get) Token: 0x0600594E RID: 22862
		// (set) Token: 0x0600594F RID: 22863
		string Text { get; set; }

		// Token: 0x17000DC5 RID: 3525
		// (get) Token: 0x06005950 RID: 22864
		// (set) Token: 0x06005951 RID: 22865
		DateTime DateTime { get; set; }

		// Token: 0x17000DC6 RID: 3526
		// (get) Token: 0x06005952 RID: 22866
		// (set) Token: 0x06005953 RID: 22867
		TimeSpan TimeSpan { get; set; }

		// Token: 0x17000DC7 RID: 3527
		// (get) Token: 0x06005954 RID: 22868
		// (set) Token: 0x06005955 RID: 22869
		string LinkSource { get; set; }

		// Token: 0x17000DC8 RID: 3528
		// (get) Token: 0x06005956 RID: 22870
		// (set) Token: 0x06005957 RID: 22871
		bool LinkToContent { get; set; }
	}
}
