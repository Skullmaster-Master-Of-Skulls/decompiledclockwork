using System;
using Telerik.Pdf;

namespace Telerik.Web.Apoc.DataTypes
{
	// Token: 0x0200137F RID: 4991
	internal class IDNode
	{
		// Token: 0x0600D02F RID: 53295 RVA: 0x002E28FF File Offset: 0x002E0AFF
		internal IDNode(string idValue)
		{
			this.idValue = idValue;
		}

		// Token: 0x0600D030 RID: 53296 RVA: 0x002E2915 File Offset: 0x002E0B15
		internal void SetPageNumber(int number)
		{
			this.pageNumber = number;
		}

		// Token: 0x0600D031 RID: 53297 RVA: 0x002E291E File Offset: 0x002E0B1E
		public string GetPageNumber()
		{
			if (this.pageNumber == -1)
			{
				return null;
			}
			return this.pageNumber.ToString();
		}

		// Token: 0x0600D032 RID: 53298 RVA: 0x002E2938 File Offset: 0x002E0B38
		internal void CreateInternalLinkGoTo(PdfObjectId objectId)
		{
			if (this.internalLinkGoToPageReference == null)
			{
				this.internalLinkGoTo = new PdfGoTo(null, objectId);
			}
			else
			{
				this.internalLinkGoTo = new PdfGoTo(this.internalLinkGoToPageReference, objectId);
			}
			if (this.xPosition != 0)
			{
				this.internalLinkGoTo.X = this.xPosition;
				this.internalLinkGoTo.Y = this.yPosition;
			}
		}

		// Token: 0x0600D033 RID: 53299 RVA: 0x002E2998 File Offset: 0x002E0B98
		internal void SetInternalLinkGoToPageReference(PdfObjectReference pageReference)
		{
			if (this.internalLinkGoTo != null)
			{
				this.internalLinkGoTo.PageReference = pageReference;
				return;
			}
			this.internalLinkGoToPageReference = pageReference;
		}

		// Token: 0x0600D034 RID: 53300 RVA: 0x002E29B8 File Offset: 0x002E0BB8
		internal string GetInternalLinkGoToReference()
		{
			return string.Concat(new object[]
			{
				this.internalLinkGoTo.ObjectId.ObjectNumber,
				" ",
				this.internalLinkGoTo.ObjectId.GenerationNumber,
				" R"
			});
		}

		// Token: 0x0600D035 RID: 53301 RVA: 0x002E2A18 File Offset: 0x002E0C18
		protected string GetIDValue()
		{
			return this.idValue;
		}

		// Token: 0x0600D036 RID: 53302 RVA: 0x002E2A20 File Offset: 0x002E0C20
		internal PdfGoTo GetInternalLinkGoTo()
		{
			return this.internalLinkGoTo;
		}

		// Token: 0x0600D037 RID: 53303 RVA: 0x002E2A28 File Offset: 0x002E0C28
		internal bool IsThereInternalLinkGoTo()
		{
			return this.internalLinkGoTo != null;
		}

		// Token: 0x0600D038 RID: 53304 RVA: 0x002E2A36 File Offset: 0x002E0C36
		internal void SetPosition(int x, int y)
		{
			if (this.internalLinkGoTo != null)
			{
				this.internalLinkGoTo.X = x;
				this.internalLinkGoTo.Y = y;
				return;
			}
			this.xPosition = x;
			this.yPosition = y;
		}

		// Token: 0x040037D1 RID: 14289
		private string idValue;

		// Token: 0x040037D2 RID: 14290
		private PdfObjectReference internalLinkGoToPageReference;

		// Token: 0x040037D3 RID: 14291
		private PdfGoTo internalLinkGoTo;

		// Token: 0x040037D4 RID: 14292
		private int pageNumber = -1;

		// Token: 0x040037D5 RID: 14293
		private int xPosition;

		// Token: 0x040037D6 RID: 14294
		private int yPosition;
	}
}
