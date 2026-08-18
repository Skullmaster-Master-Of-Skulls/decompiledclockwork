using System;
using System.Collections;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000502 RID: 1282
	public sealed class TreeNodeStyleCollection : StateManagedCollection
	{
		// Token: 0x06004031 RID: 16433 RVA: 0x00095F2B File Offset: 0x0009412B
		internal TreeNodeStyleCollection()
		{
		}

		// Token: 0x06004032 RID: 16434 RVA: 0x000CF86C File Offset: 0x000CDA6C
		protected override void OnInsert(int index, object value)
		{
			base.OnInsert(index, value);
			if (value is TreeNodeStyle)
			{
				TreeNodeStyle treeNodeStyle = (TreeNodeStyle)value;
				treeNodeStyle.Font.Underline = treeNodeStyle.Font.Underline;
				return;
			}
			throw new ArgumentException(SR.GetString("TreeNodeStyleCollection_InvalidArgument"), "value");
		}

		// Token: 0x170012C4 RID: 4804
		public TreeNodeStyle this[int i]
		{
			get
			{
				return (TreeNodeStyle)((IList)this)[i];
			}
			set
			{
				((IList)this)[i] = value;
			}
		}

		// Token: 0x06004035 RID: 16437 RVA: 0x000A9CAD File Offset: 0x000A7EAD
		public int Add(TreeNodeStyle style)
		{
			return ((IList)this).Add(style);
		}

		// Token: 0x06004036 RID: 16438 RVA: 0x00095DD0 File Offset: 0x00093FD0
		public bool Contains(TreeNodeStyle style)
		{
			return ((IList)this).Contains(style);
		}

		// Token: 0x06004037 RID: 16439 RVA: 0x000B7C0D File Offset: 0x000B5E0D
		public void CopyTo(TreeNodeStyle[] styleArray, int index)
		{
			base.CopyTo(styleArray, index);
		}

		// Token: 0x06004038 RID: 16440 RVA: 0x00095E55 File Offset: 0x00094055
		public int IndexOf(TreeNodeStyle style)
		{
			return ((IList)this).IndexOf(style);
		}

		// Token: 0x06004039 RID: 16441 RVA: 0x00095E5E File Offset: 0x0009405E
		public void Insert(int index, TreeNodeStyle style)
		{
			((IList)this).Insert(index, style);
		}

		// Token: 0x0600403A RID: 16442 RVA: 0x000CF8C9 File Offset: 0x000CDAC9
		protected override object CreateKnownType(int index)
		{
			return new TreeNodeStyle();
		}

		// Token: 0x0600403B RID: 16443 RVA: 0x000CF8D0 File Offset: 0x000CDAD0
		protected override Type[] GetKnownTypes()
		{
			return TreeNodeStyleCollection.knownTypes;
		}

		// Token: 0x0600403C RID: 16444 RVA: 0x00095F15 File Offset: 0x00094115
		public void Remove(TreeNodeStyle style)
		{
			((IList)this).Remove(style);
		}

		// Token: 0x0600403D RID: 16445 RVA: 0x00095F0C File Offset: 0x0009410C
		public void RemoveAt(int index)
		{
			((IList)this).RemoveAt(index);
		}

		// Token: 0x0600403E RID: 16446 RVA: 0x000CF8D7 File Offset: 0x000CDAD7
		protected override void SetDirtyObject(object o)
		{
			if (o is TreeNodeStyle)
			{
				((TreeNodeStyle)o).SetDirty();
			}
		}

		// Token: 0x04002478 RID: 9336
		private static readonly Type[] knownTypes = new Type[]
		{
			typeof(TreeNodeStyle)
		};
	}
}
