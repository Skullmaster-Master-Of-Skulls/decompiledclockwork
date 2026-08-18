using System;
using System.Collections;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x02000583 RID: 1411
	public sealed class WebPartCollection : ReadOnlyCollectionBase
	{
		// Token: 0x0600477D RID: 18301 RVA: 0x000DCEF2 File Offset: 0x000DB0F2
		public WebPartCollection()
		{
		}

		// Token: 0x0600477E RID: 18302 RVA: 0x000EBC34 File Offset: 0x000E9E34
		public WebPartCollection(ICollection webParts)
		{
			if (webParts == null)
			{
				throw new ArgumentNullException("webParts");
			}
			foreach (object obj in webParts)
			{
				if (obj == null)
				{
					throw new ArgumentException(SR.GetString("Collection_CantAddNull"), "webParts");
				}
				if (!(obj is WebPart))
				{
					throw new ArgumentException(SR.GetString("Collection_InvalidType", new object[]
					{
						"WebPart"
					}), "webParts");
				}
				base.InnerList.Add(obj);
			}
		}

		// Token: 0x0600477F RID: 18303 RVA: 0x000DCF98 File Offset: 0x000DB198
		internal int Add(WebPart value)
		{
			return base.InnerList.Add(value);
		}

		// Token: 0x06004780 RID: 18304 RVA: 0x00043ADC File Offset: 0x00041CDC
		public bool Contains(WebPart value)
		{
			return base.InnerList.Contains(value);
		}

		// Token: 0x06004781 RID: 18305 RVA: 0x00043ACE File Offset: 0x00041CCE
		public int IndexOf(WebPart value)
		{
			return base.InnerList.IndexOf(value);
		}

		// Token: 0x17001514 RID: 5396
		public WebPart this[int index]
		{
			get
			{
				return (WebPart)base.InnerList[index];
			}
		}

		// Token: 0x17001515 RID: 5397
		public WebPart this[string id]
		{
			get
			{
				foreach (object obj in base.InnerList)
				{
					WebPart webPart = (WebPart)obj;
					if (string.Equals(webPart.ID, id, StringComparison.OrdinalIgnoreCase))
					{
						return webPart;
					}
					GenericWebPart genericWebPart = webPart as GenericWebPart;
					if (genericWebPart != null)
					{
						Control childControl = genericWebPart.ChildControl;
						if (childControl != null && string.Equals(childControl.ID, id, StringComparison.OrdinalIgnoreCase))
						{
							return genericWebPart;
						}
					}
					ProxyWebPart proxyWebPart = webPart as ProxyWebPart;
					if (proxyWebPart != null && (string.Equals(proxyWebPart.OriginalID, id, StringComparison.OrdinalIgnoreCase) || string.Equals(proxyWebPart.GenericWebPartID, id, StringComparison.OrdinalIgnoreCase)))
					{
						return proxyWebPart;
					}
				}
				return null;
			}
		}

		// Token: 0x06004784 RID: 18308 RVA: 0x000DCFA6 File Offset: 0x000DB1A6
		public void CopyTo(WebPart[] array, int index)
		{
			base.InnerList.CopyTo(array, index);
		}
	}
}
