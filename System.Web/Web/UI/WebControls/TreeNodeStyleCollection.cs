using System;
using System.Collections;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000673 RID: 1651
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class TreeNodeStyleCollection : StateManagedCollection
	{
		// Token: 0x060050EB RID: 20715 RVA: 0x00145312 File Offset: 0x00144312
		internal TreeNodeStyleCollection()
		{
		}

		// Token: 0x060050EC RID: 20716 RVA: 0x0014531C File Offset: 0x0014431C
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

		// Token: 0x1700148D RID: 5261
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

		// Token: 0x060050EF RID: 20719 RVA: 0x00145383 File Offset: 0x00144383
		public int Add(TreeNodeStyle style)
		{
			return ((IList)this).Add(style);
		}

		// Token: 0x060050F0 RID: 20720 RVA: 0x0014538C File Offset: 0x0014438C
		public bool Contains(TreeNodeStyle style)
		{
			return ((IList)this).Contains(style);
		}

		// Token: 0x060050F1 RID: 20721 RVA: 0x00145395 File Offset: 0x00144395
		public void CopyTo(TreeNodeStyle[] styleArray, int index)
		{
			base.CopyTo(styleArray, index);
		}

		// Token: 0x060050F2 RID: 20722 RVA: 0x0014539F File Offset: 0x0014439F
		public int IndexOf(TreeNodeStyle style)
		{
			return ((IList)this).IndexOf(style);
		}

		// Token: 0x060050F3 RID: 20723 RVA: 0x001453A8 File Offset: 0x001443A8
		public void Insert(int index, TreeNodeStyle style)
		{
			((IList)this).Insert(index, style);
		}

		// Token: 0x060050F4 RID: 20724 RVA: 0x001453B2 File Offset: 0x001443B2
		protected override object CreateKnownType(int index)
		{
			return new TreeNodeStyle();
		}

		// Token: 0x060050F5 RID: 20725 RVA: 0x001453B9 File Offset: 0x001443B9
		protected override Type[] GetKnownTypes()
		{
			return TreeNodeStyleCollection.knownTypes;
		}

		// Token: 0x060050F6 RID: 20726 RVA: 0x001453C0 File Offset: 0x001443C0
		public void Remove(TreeNodeStyle style)
		{
			((IList)this).Remove(style);
		}

		// Token: 0x060050F7 RID: 20727 RVA: 0x001453C9 File Offset: 0x001443C9
		public void RemoveAt(int index)
		{
			((IList)this).RemoveAt(index);
		}

		// Token: 0x060050F8 RID: 20728 RVA: 0x001453D2 File Offset: 0x001443D2
		protected override void SetDirtyObject(object o)
		{
			if (o is TreeNodeStyle)
			{
				((TreeNodeStyle)o).SetDirty();
			}
		}

		// Token: 0x04002D43 RID: 11587
		private static readonly Type[] knownTypes = new Type[]
		{
			typeof(TreeNodeStyle)
		};
	}
}
