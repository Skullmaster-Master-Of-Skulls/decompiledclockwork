using System;
using Spire.Xls.Core.Interfaces;

namespace Spire.Xls.Core
{
	// Token: 0x020001FE RID: 510
	public interface IShape : IExcelApplication
	{
		// Token: 0x17000AB2 RID: 2738
		// (get) Token: 0x06001CD0 RID: 7376
		// (set) Token: 0x06001CD1 RID: 7377
		int Height { get; set; }

		// Token: 0x17000AB3 RID: 2739
		// (get) Token: 0x06001CD2 RID: 7378
		int ID { get; }

		// Token: 0x17000AB4 RID: 2740
		// (get) Token: 0x06001CD3 RID: 7379
		// (set) Token: 0x06001CD4 RID: 7380
		int Left { get; set; }

		// Token: 0x17000AB5 RID: 2741
		// (get) Token: 0x06001CD5 RID: 7381
		// (set) Token: 0x06001CD6 RID: 7382
		string Name { get; set; }

		// Token: 0x17000AB6 RID: 2742
		// (get) Token: 0x06001CD7 RID: 7383
		// (set) Token: 0x06001CD8 RID: 7384
		int Top { get; set; }

		// Token: 0x17000AB7 RID: 2743
		// (get) Token: 0x06001CD9 RID: 7385
		// (set) Token: 0x06001CDA RID: 7386
		int Width { get; set; }

		// Token: 0x17000AB8 RID: 2744
		// (get) Token: 0x06001CDB RID: 7387
		// (set) Token: 0x06001CDC RID: 7388
		ExcelShapeType ShapeType { get; set; }

		// Token: 0x17000AB9 RID: 2745
		// (get) Token: 0x06001CDD RID: 7389
		// (set) Token: 0x06001CDE RID: 7390
		bool Visible { get; set; }

		// Token: 0x17000ABA RID: 2746
		// (get) Token: 0x06001CDF RID: 7391
		// (set) Token: 0x06001CE0 RID: 7392
		string AlternativeText { get; set; }

		// Token: 0x17000ABB RID: 2747
		// (get) Token: 0x06001CE1 RID: 7393
		IShapeFill Fill { get; }

		// Token: 0x17000ABC RID: 2748
		// (get) Token: 0x06001CE2 RID: 7394
		// (set) Token: 0x06001CE3 RID: 7395
		string OnAction { get; set; }

		// Token: 0x17000ABD RID: 2749
		// (get) Token: 0x06001CE4 RID: 7396
		IShadow Shadow { get; }

		// Token: 0x17000ABE RID: 2750
		// (get) Token: 0x06001CE5 RID: 7397
		IFormat3D ThreeD { get; }

		// Token: 0x17000ABF RID: 2751
		// (get) Token: 0x06001CE6 RID: 7398
		// (set) Token: 0x06001CE7 RID: 7399
		int Rotation { get; set; }

		// Token: 0x06001CE8 RID: 7400
		void Remove();

		// Token: 0x06001CE9 RID: 7401
		void Scale(int scaleWidth, int scaleHeight);
	}
}
