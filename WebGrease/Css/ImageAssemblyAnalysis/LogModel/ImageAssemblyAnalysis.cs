using System;
using WebGrease.Css.Ast;
using WebGrease.ImageAssemble;

namespace WebGrease.Css.ImageAssemblyAnalysis.LogModel
{
	// Token: 0x02000190 RID: 400
	internal class ImageAssemblyAnalysis
	{
		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x060014A4 RID: 5284 RVA: 0x000785F5 File Offset: 0x000767F5
		// (set) Token: 0x060014A5 RID: 5285 RVA: 0x000785FD File Offset: 0x000767FD
		internal FailureReason? FailureReason { get; set; }

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x060014A6 RID: 5286 RVA: 0x00078606 File Offset: 0x00076806
		// (set) Token: 0x060014A7 RID: 5287 RVA: 0x0007860E File Offset: 0x0007680E
		internal AstNode AstNode { get; set; }

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x060014A8 RID: 5288 RVA: 0x00078617 File Offset: 0x00076817
		// (set) Token: 0x060014A9 RID: 5289 RVA: 0x0007861F File Offset: 0x0007681F
		internal string Image { get; set; }

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x060014AA RID: 5290 RVA: 0x00078628 File Offset: 0x00076828
		// (set) Token: 0x060014AB RID: 5291 RVA: 0x00078630 File Offset: 0x00076830
		internal ImageType? ImageType { get; set; }

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x060014AC RID: 5292 RVA: 0x00078639 File Offset: 0x00076839
		// (set) Token: 0x060014AD RID: 5293 RVA: 0x00078641 File Offset: 0x00076841
		internal string SpritedImage { get; set; }
	}
}
