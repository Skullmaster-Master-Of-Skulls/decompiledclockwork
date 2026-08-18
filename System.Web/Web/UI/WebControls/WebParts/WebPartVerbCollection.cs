using System;
using System.Collections;
using System.Collections.Specialized;
using System.Security.Permissions;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x02000746 RID: 1862
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class WebPartVerbCollection : ReadOnlyCollectionBase
	{
		// Token: 0x06005A4B RID: 23115 RVA: 0x0016C59F File Offset: 0x0016B59F
		public WebPartVerbCollection()
		{
			this.Initialize(null, null);
		}

		// Token: 0x06005A4C RID: 23116 RVA: 0x0016C5AF File Offset: 0x0016B5AF
		public WebPartVerbCollection(ICollection verbs)
		{
			this.Initialize(null, verbs);
		}

		// Token: 0x06005A4D RID: 23117 RVA: 0x0016C5BF File Offset: 0x0016B5BF
		public WebPartVerbCollection(WebPartVerbCollection existingVerbs, ICollection verbs)
		{
			this.Initialize(existingVerbs, verbs);
		}

		// Token: 0x17001752 RID: 5970
		public WebPartVerb this[int index]
		{
			get
			{
				return (WebPartVerb)base.InnerList[index];
			}
		}

		// Token: 0x17001753 RID: 5971
		internal WebPartVerb this[string id]
		{
			get
			{
				return (WebPartVerb)this._ids[id];
			}
		}

		// Token: 0x06005A50 RID: 23120 RVA: 0x0016C5F5 File Offset: 0x0016B5F5
		internal int Add(WebPartVerb value)
		{
			return base.InnerList.Add(value);
		}

		// Token: 0x06005A51 RID: 23121 RVA: 0x0016C603 File Offset: 0x0016B603
		public bool Contains(WebPartVerb value)
		{
			return base.InnerList.Contains(value);
		}

		// Token: 0x06005A52 RID: 23122 RVA: 0x0016C611 File Offset: 0x0016B611
		public void CopyTo(WebPartVerb[] array, int index)
		{
			base.InnerList.CopyTo(array, index);
		}

		// Token: 0x06005A53 RID: 23123 RVA: 0x0016C620 File Offset: 0x0016B620
		public int IndexOf(WebPartVerb value)
		{
			return base.InnerList.IndexOf(value);
		}

		// Token: 0x06005A54 RID: 23124 RVA: 0x0016C630 File Offset: 0x0016B630
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

		// Token: 0x04003085 RID: 12421
		private HybridDictionary _ids;

		// Token: 0x04003086 RID: 12422
		public static readonly WebPartVerbCollection Empty = new WebPartVerbCollection();
	}
}
