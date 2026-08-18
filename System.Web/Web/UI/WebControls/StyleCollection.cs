using System;
using System.Collections;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000652 RID: 1618
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class StyleCollection : StateManagedCollection
	{
		// Token: 0x06004F52 RID: 20306 RVA: 0x0013F603 File Offset: 0x0013E603
		internal StyleCollection()
		{
		}

		// Token: 0x17001413 RID: 5139
		public Style this[int i]
		{
			get
			{
				return (Style)((IList)this)[i];
			}
			set
			{
				((IList)this)[i] = value;
			}
		}

		// Token: 0x06004F55 RID: 20309 RVA: 0x0013F623 File Offset: 0x0013E623
		public int Add(Style style)
		{
			return ((IList)this).Add(style);
		}

		// Token: 0x06004F56 RID: 20310 RVA: 0x0013F62C File Offset: 0x0013E62C
		public bool Contains(Style style)
		{
			return ((IList)this).Contains(style);
		}

		// Token: 0x06004F57 RID: 20311 RVA: 0x0013F635 File Offset: 0x0013E635
		public void CopyTo(Style[] styleArray, int index)
		{
			base.CopyTo(styleArray, index);
		}

		// Token: 0x06004F58 RID: 20312 RVA: 0x0013F63F File Offset: 0x0013E63F
		public int IndexOf(Style style)
		{
			return ((IList)this).IndexOf(style);
		}

		// Token: 0x06004F59 RID: 20313 RVA: 0x0013F648 File Offset: 0x0013E648
		public void Insert(int index, Style style)
		{
			((IList)this).Insert(index, style);
		}

		// Token: 0x06004F5A RID: 20314 RVA: 0x0013F652 File Offset: 0x0013E652
		protected override object CreateKnownType(int index)
		{
			return new Style();
		}

		// Token: 0x06004F5B RID: 20315 RVA: 0x0013F659 File Offset: 0x0013E659
		protected override Type[] GetKnownTypes()
		{
			return StyleCollection.knownTypes;
		}

		// Token: 0x06004F5C RID: 20316 RVA: 0x0013F660 File Offset: 0x0013E660
		public void Remove(Style style)
		{
			((IList)this).Remove(style);
		}

		// Token: 0x06004F5D RID: 20317 RVA: 0x0013F669 File Offset: 0x0013E669
		public void RemoveAt(int index)
		{
			((IList)this).RemoveAt(index);
		}

		// Token: 0x06004F5E RID: 20318 RVA: 0x0013F672 File Offset: 0x0013E672
		protected override void SetDirtyObject(object o)
		{
			if (o is Style)
			{
				((Style)o).SetDirty();
			}
		}

		// Token: 0x04002CE1 RID: 11489
		private static readonly Type[] knownTypes = new Type[]
		{
			typeof(Style)
		};
	}
}
