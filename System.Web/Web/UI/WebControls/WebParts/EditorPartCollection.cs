using System;
using System.Collections;
using System.Security.Permissions;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x020006BC RID: 1724
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class EditorPartCollection : ReadOnlyCollectionBase
	{
		// Token: 0x060054B0 RID: 21680 RVA: 0x0015888F File Offset: 0x0015788F
		public EditorPartCollection()
		{
		}

		// Token: 0x060054B1 RID: 21681 RVA: 0x00158897 File Offset: 0x00157897
		public EditorPartCollection(ICollection editorParts)
		{
			this.Initialize(null, editorParts);
		}

		// Token: 0x060054B2 RID: 21682 RVA: 0x001588A7 File Offset: 0x001578A7
		public EditorPartCollection(EditorPartCollection existingEditorParts, ICollection editorParts)
		{
			this.Initialize(existingEditorParts, editorParts);
		}

		// Token: 0x170015AE RID: 5550
		public EditorPart this[int index]
		{
			get
			{
				return (EditorPart)base.InnerList[index];
			}
		}

		// Token: 0x060054B4 RID: 21684 RVA: 0x001588CA File Offset: 0x001578CA
		internal int Add(EditorPart value)
		{
			return base.InnerList.Add(value);
		}

		// Token: 0x060054B5 RID: 21685 RVA: 0x001588D8 File Offset: 0x001578D8
		public bool Contains(EditorPart editorPart)
		{
			return base.InnerList.Contains(editorPart);
		}

		// Token: 0x060054B6 RID: 21686 RVA: 0x001588E6 File Offset: 0x001578E6
		public void CopyTo(EditorPart[] array, int index)
		{
			base.InnerList.CopyTo(array, index);
		}

		// Token: 0x060054B7 RID: 21687 RVA: 0x001588F5 File Offset: 0x001578F5
		public int IndexOf(EditorPart editorPart)
		{
			return base.InnerList.IndexOf(editorPart);
		}

		// Token: 0x060054B8 RID: 21688 RVA: 0x00158904 File Offset: 0x00157904
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

		// Token: 0x04002EE8 RID: 12008
		public static readonly EditorPartCollection Empty = new EditorPartCollection();
	}
}
