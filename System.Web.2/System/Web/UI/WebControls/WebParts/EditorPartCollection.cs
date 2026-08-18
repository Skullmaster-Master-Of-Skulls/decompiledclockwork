using System;
using System.Collections;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x0200053A RID: 1338
	public sealed class EditorPartCollection : ReadOnlyCollectionBase
	{
		// Token: 0x0600444F RID: 17487 RVA: 0x000DCEF2 File Offset: 0x000DB0F2
		public EditorPartCollection()
		{
		}

		// Token: 0x06004450 RID: 17488 RVA: 0x000E244F File Offset: 0x000E064F
		public EditorPartCollection(ICollection editorParts)
		{
			this.Initialize(null, editorParts);
		}

		// Token: 0x06004451 RID: 17489 RVA: 0x000E245F File Offset: 0x000E065F
		public EditorPartCollection(EditorPartCollection existingEditorParts, ICollection editorParts)
		{
			this.Initialize(existingEditorParts, editorParts);
		}

		// Token: 0x17001414 RID: 5140
		public EditorPart this[int index]
		{
			get
			{
				return (EditorPart)base.InnerList[index];
			}
		}

		// Token: 0x06004453 RID: 17491 RVA: 0x000DCF98 File Offset: 0x000DB198
		internal int Add(EditorPart value)
		{
			return base.InnerList.Add(value);
		}

		// Token: 0x06004454 RID: 17492 RVA: 0x00043ADC File Offset: 0x00041CDC
		public bool Contains(EditorPart editorPart)
		{
			return base.InnerList.Contains(editorPart);
		}

		// Token: 0x06004455 RID: 17493 RVA: 0x000DCFA6 File Offset: 0x000DB1A6
		public void CopyTo(EditorPart[] array, int index)
		{
			base.InnerList.CopyTo(array, index);
		}

		// Token: 0x06004456 RID: 17494 RVA: 0x00043ACE File Offset: 0x00041CCE
		public int IndexOf(EditorPart editorPart)
		{
			return base.InnerList.IndexOf(editorPart);
		}

		// Token: 0x06004457 RID: 17495 RVA: 0x000E2484 File Offset: 0x000E0684
		private void Initialize(EditorPartCollection existingEditorParts, ICollection editorParts)
		{
			if (existingEditorParts != null)
			{
				foreach (object obj in existingEditorParts)
				{
					EditorPart value = (EditorPart)obj;
					base.InnerList.Add(value);
				}
			}
			if (editorParts != null)
			{
				foreach (object obj2 in editorParts)
				{
					if (obj2 == null)
					{
						throw new ArgumentException(SR.GetString("Collection_CantAddNull"), "editorParts");
					}
					if (!(obj2 is EditorPart))
					{
						throw new ArgumentException(SR.GetString("Collection_InvalidType", new object[]
						{
							"EditorPart"
						}), "editorParts");
					}
					base.InnerList.Add(obj2);
				}
			}
		}

		// Token: 0x04002627 RID: 9767
		public static readonly EditorPartCollection Empty = new EditorPartCollection();
	}
}
