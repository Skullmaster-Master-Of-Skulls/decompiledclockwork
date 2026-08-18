using System;
using System.Collections;
using System.Collections.Specialized;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x02000592 RID: 1426
	public sealed class WebPartDescriptionCollection : ReadOnlyCollectionBase
	{
		// Token: 0x060047FC RID: 18428 RVA: 0x000DCEF2 File Offset: 0x000DB0F2
		public WebPartDescriptionCollection()
		{
		}

		// Token: 0x060047FD RID: 18429 RVA: 0x000ECB48 File Offset: 0x000EAD48
		public WebPartDescriptionCollection(ICollection webPartDescriptions)
		{
			if (webPartDescriptions == null)
			{
				throw new ArgumentNullException("webPartDescriptions");
			}
			this._ids = new HybridDictionary(webPartDescriptions.Count, true);
			foreach (object obj in webPartDescriptions)
			{
				if (obj == null)
				{
					throw new ArgumentException(SR.GetString("Collection_CantAddNull"), "webPartDescriptions");
				}
				WebPartDescription webPartDescription = obj as WebPartDescription;
				if (webPartDescription == null)
				{
					throw new ArgumentException(SR.GetString("Collection_InvalidType", new object[]
					{
						"WebPartDescription"
					}), "webPartDescriptions");
				}
				string id = webPartDescription.ID;
				if (this._ids.Contains(id))
				{
					throw new ArgumentException(SR.GetString("WebPart_Collection_DuplicateID", new object[]
					{
						"WebPartDescription",
						id
					}), "webPartDescriptions");
				}
				base.InnerList.Add(webPartDescription);
				this._ids.Add(id, webPartDescription);
			}
		}

		// Token: 0x060047FE RID: 18430 RVA: 0x00043ADC File Offset: 0x00041CDC
		public bool Contains(WebPartDescription value)
		{
			return base.InnerList.Contains(value);
		}

		// Token: 0x060047FF RID: 18431 RVA: 0x00043ACE File Offset: 0x00041CCE
		public int IndexOf(WebPartDescription value)
		{
			return base.InnerList.IndexOf(value);
		}

		// Token: 0x17001549 RID: 5449
		public WebPartDescription this[int index]
		{
			get
			{
				return (WebPartDescription)base.InnerList[index];
			}
		}

		// Token: 0x1700154A RID: 5450
		public WebPartDescription this[string id]
		{
			get
			{
				if (this._ids == null)
				{
					return null;
				}
				return (WebPartDescription)this._ids[id];
			}
		}

		// Token: 0x06004802 RID: 18434 RVA: 0x000DCFA6 File Offset: 0x000DB1A6
		public void CopyTo(WebPartDescription[] array, int index)
		{
			base.InnerList.CopyTo(array, index);
		}

		// Token: 0x0400271D RID: 10013
		private HybridDictionary _ids;
	}
}
