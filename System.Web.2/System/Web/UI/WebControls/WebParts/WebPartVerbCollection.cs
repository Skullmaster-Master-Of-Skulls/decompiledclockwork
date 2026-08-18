using System;
using System.Collections;
using System.Collections.Specialized;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x020005B4 RID: 1460
	public sealed class WebPartVerbCollection : ReadOnlyCollectionBase
	{
		// Token: 0x060049E2 RID: 18914 RVA: 0x000F5309 File Offset: 0x000F3509
		public WebPartVerbCollection()
		{
			this.Initialize(null, null);
		}

		// Token: 0x060049E3 RID: 18915 RVA: 0x000F5319 File Offset: 0x000F3519
		public WebPartVerbCollection(ICollection verbs)
		{
			this.Initialize(null, verbs);
		}

		// Token: 0x060049E4 RID: 18916 RVA: 0x000F5329 File Offset: 0x000F3529
		public WebPartVerbCollection(WebPartVerbCollection existingVerbs, ICollection verbs)
		{
			this.Initialize(existingVerbs, verbs);
		}

		// Token: 0x170015B2 RID: 5554
		public WebPartVerb this[int index]
		{
			get
			{
				return (WebPartVerb)base.InnerList[index];
			}
		}

		// Token: 0x170015B3 RID: 5555
		internal WebPartVerb this[string id]
		{
			get
			{
				return (WebPartVerb)this._ids[id];
			}
		}

		// Token: 0x060049E7 RID: 18919 RVA: 0x000DCF98 File Offset: 0x000DB198
		internal int Add(WebPartVerb value)
		{
			return base.InnerList.Add(value);
		}

		// Token: 0x060049E8 RID: 18920 RVA: 0x00043ADC File Offset: 0x00041CDC
		public bool Contains(WebPartVerb value)
		{
			return base.InnerList.Contains(value);
		}

		// Token: 0x060049E9 RID: 18921 RVA: 0x000DCFA6 File Offset: 0x000DB1A6
		public void CopyTo(WebPartVerb[] array, int index)
		{
			base.InnerList.CopyTo(array, index);
		}

		// Token: 0x060049EA RID: 18922 RVA: 0x00043ACE File Offset: 0x00041CCE
		public int IndexOf(WebPartVerb value)
		{
			return base.InnerList.IndexOf(value);
		}

		// Token: 0x060049EB RID: 18923 RVA: 0x000F5360 File Offset: 0x000F3560
		private void Initialize(WebPartVerbCollection existingVerbs, ICollection verbs)
		{
			int initialSize = ((existingVerbs != null) ? existingVerbs.Count : 0) + ((verbs != null) ? verbs.Count : 0);
			this._ids = new HybridDictionary(initialSize, true);
			if (existingVerbs != null)
			{
				foreach (object obj in existingVerbs)
				{
					WebPartVerb webPartVerb = (WebPartVerb)obj;
					if (this._ids.Contains(webPartVerb.ID))
					{
						throw new ArgumentException(SR.GetString("WebPart_Collection_DuplicateID", new object[]
						{
							"WebPartVerb",
							webPartVerb.ID
						}), "existingVerbs");
					}
					this._ids.Add(webPartVerb.ID, webPartVerb);
					base.InnerList.Add(webPartVerb);
				}
			}
			if (verbs != null)
			{
				foreach (object obj2 in verbs)
				{
					if (obj2 == null)
					{
						throw new ArgumentException(SR.GetString("Collection_CantAddNull"), "verbs");
					}
					WebPartVerb webPartVerb2 = obj2 as WebPartVerb;
					if (webPartVerb2 == null)
					{
						throw new ArgumentException(SR.GetString("Collection_InvalidType", new object[]
						{
							"WebPartVerb"
						}), "verbs");
					}
					if (this._ids.Contains(webPartVerb2.ID))
					{
						throw new ArgumentException(SR.GetString("WebPart_Collection_DuplicateID", new object[]
						{
							"WebPartVerb",
							webPartVerb2.ID
						}), "verbs");
					}
					this._ids.Add(webPartVerb2.ID, webPartVerb2);
					base.InnerList.Add(webPartVerb2);
				}
			}
		}

		// Token: 0x040027BF RID: 10175
		private HybridDictionary _ids;

		// Token: 0x040027C0 RID: 10176
		public static readonly WebPartVerbCollection Empty = new WebPartVerbCollection();
	}
}
