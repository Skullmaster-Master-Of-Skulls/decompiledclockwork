using System;
using System.Collections;
using System.Text;
using Telerik.Pdf;
using Telerik.Web.Apoc.Layout;

namespace Telerik.Web.Apoc.DataTypes
{
	// Token: 0x02001380 RID: 4992
	internal class IDReferences
	{
		// Token: 0x0600D039 RID: 53305 RVA: 0x002E2A67 File Offset: 0x002E0C67
		public IDReferences()
		{
			this.idReferences = new Hashtable();
			this.idValidation = new Hashtable();
			this.idUnvalidated = new Hashtable();
		}

		// Token: 0x0600D03A RID: 53306 RVA: 0x002E2A90 File Offset: 0x002E0C90
		public void InitializeID(string id, Area area)
		{
			this.CreateID(id);
			this.ConfigureID(id, area);
		}

		// Token: 0x0600D03B RID: 53307 RVA: 0x002E2AA4 File Offset: 0x002E0CA4
		public void CreateID(string id)
		{
			if (!string.IsNullOrEmpty(id))
			{
				if (this.DoesUnvalidatedIDExist(id))
				{
					this.RemoveFromUnvalidatedIDList(id);
					this.RemoveFromIdValidationList(id);
					return;
				}
				if (this.doesIDExist(id))
				{
					throw new ApocException("The id \"" + id + "\" already exists in this document");
				}
				this.createNewId(id);
				this.RemoveFromIdValidationList(id);
			}
		}

		// Token: 0x0600D03C RID: 53308 RVA: 0x002E2AFE File Offset: 0x002E0CFE
		public void CreateUnvalidatedID(string id)
		{
			if (!string.IsNullOrEmpty(id) && !this.doesIDExist(id))
			{
				this.createNewId(id);
				this.AddToUnvalidatedIdList(id);
			}
		}

		// Token: 0x0600D03D RID: 53309 RVA: 0x002E2B1F File Offset: 0x002E0D1F
		public void AddToUnvalidatedIdList(string id)
		{
			this.idUnvalidated[id] = "";
		}

		// Token: 0x0600D03E RID: 53310 RVA: 0x002E2B32 File Offset: 0x002E0D32
		public void RemoveFromUnvalidatedIDList(string id)
		{
			this.idUnvalidated.Remove(id);
		}

		// Token: 0x0600D03F RID: 53311 RVA: 0x002E2B40 File Offset: 0x002E0D40
		public bool DoesUnvalidatedIDExist(string id)
		{
			return this.idUnvalidated.ContainsKey(id);
		}

		// Token: 0x0600D040 RID: 53312 RVA: 0x002E2B50 File Offset: 0x002E0D50
		public void ConfigureID(string id, Area area)
		{
			if (!string.IsNullOrEmpty(id))
			{
				this.setPosition(id, area.getPage().getBody().getXPosition() + area.getTableCellXOffset() - 5000, area.getPage().getBody().GetYPosition() - area.getAbsoluteHeight() + 5000);
				this.setPageNumber(id, area.getPage().getNumber());
				area.getPage().addToIDList(id);
			}
		}

		// Token: 0x0600D041 RID: 53313 RVA: 0x002E2BC4 File Offset: 0x002E0DC4
		public void AddToIdValidationList(string id)
		{
			this.idValidation[id] = "";
		}

		// Token: 0x0600D042 RID: 53314 RVA: 0x002E2BD7 File Offset: 0x002E0DD7
		public void RemoveFromIdValidationList(string id)
		{
			this.idValidation.Remove(id);
		}

		// Token: 0x0600D043 RID: 53315 RVA: 0x002E2BE5 File Offset: 0x002E0DE5
		public void RemoveID(string id)
		{
			this.idReferences.Remove(id);
		}

		// Token: 0x0600D044 RID: 53316 RVA: 0x002E2BF3 File Offset: 0x002E0DF3
		public bool IsEveryIdValid()
		{
			return this.idValidation.Count == 0;
		}

		// Token: 0x0600D045 RID: 53317 RVA: 0x002E2C04 File Offset: 0x002E0E04
		public string GetInvalidIds()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (object obj in this.idValidation.Keys)
			{
				stringBuilder.Append("\n\"");
				stringBuilder.Append(obj.ToString());
				stringBuilder.Append("\" ");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600D046 RID: 53318 RVA: 0x002E2C88 File Offset: 0x002E0E88
		public bool doesIDExist(string id)
		{
			return this.idReferences.ContainsKey(id);
		}

		// Token: 0x0600D047 RID: 53319 RVA: 0x002E2C98 File Offset: 0x002E0E98
		public bool doesGoToReferenceExist(string id)
		{
			IDNode idnode = (IDNode)this.idReferences[id];
			return idnode.IsThereInternalLinkGoTo();
		}

		// Token: 0x0600D048 RID: 53320 RVA: 0x002E2CC0 File Offset: 0x002E0EC0
		public PdfGoTo getInternalLinkGoTo(string id)
		{
			IDNode idnode = (IDNode)this.idReferences[id];
			return idnode.GetInternalLinkGoTo();
		}

		// Token: 0x0600D049 RID: 53321 RVA: 0x002E2CE8 File Offset: 0x002E0EE8
		public PdfGoTo createInternalLinkGoTo(string id, PdfObjectId objectId)
		{
			IDNode idnode = (IDNode)this.idReferences[id];
			idnode.CreateInternalLinkGoTo(objectId);
			return idnode.GetInternalLinkGoTo();
		}

		// Token: 0x0600D04A RID: 53322 RVA: 0x002E2D14 File Offset: 0x002E0F14
		public void createNewId(string id)
		{
			IDNode value = new IDNode(id);
			this.idReferences[id] = value;
		}

		// Token: 0x0600D04B RID: 53323 RVA: 0x002E2D38 File Offset: 0x002E0F38
		public PdfGoTo getPDFGoTo(string id)
		{
			IDNode idnode = (IDNode)this.idReferences[id];
			return idnode.GetInternalLinkGoTo();
		}

		// Token: 0x0600D04C RID: 53324 RVA: 0x002E2D60 File Offset: 0x002E0F60
		public void setInternalGoToPageReference(string id, PdfObjectReference pageReference)
		{
			IDNode idnode = (IDNode)this.idReferences[id];
			if (idnode != null)
			{
				idnode.SetInternalLinkGoToPageReference(pageReference);
			}
		}

		// Token: 0x0600D04D RID: 53325 RVA: 0x002E2D8C File Offset: 0x002E0F8C
		public void setPageNumber(string id, int pageNumber)
		{
			IDNode idnode = (IDNode)this.idReferences[id];
			idnode.SetPageNumber(pageNumber);
		}

		// Token: 0x0600D04E RID: 53326 RVA: 0x002E2DB4 File Offset: 0x002E0FB4
		public string getPageNumber(string id)
		{
			if (this.doesIDExist(id))
			{
				IDNode idnode = (IDNode)this.idReferences[id];
				return idnode.GetPageNumber();
			}
			this.AddToIdValidationList(id);
			return null;
		}

		// Token: 0x0600D04F RID: 53327 RVA: 0x002E2DEC File Offset: 0x002E0FEC
		public void setPosition(string id, int x, int y)
		{
			IDNode idnode = (IDNode)this.idReferences[id];
			idnode.SetPosition(x, y);
		}

		// Token: 0x0600D050 RID: 53328 RVA: 0x002E2E13 File Offset: 0x002E1013
		public ICollection getInvalidElements()
		{
			return this.idValidation.Keys;
		}

		// Token: 0x040037D7 RID: 14295
		private const int ID_PADDING = 5000;

		// Token: 0x040037D8 RID: 14296
		private Hashtable idReferences;

		// Token: 0x040037D9 RID: 14297
		private Hashtable idValidation;

		// Token: 0x040037DA RID: 14298
		private Hashtable idUnvalidated;
	}
}
