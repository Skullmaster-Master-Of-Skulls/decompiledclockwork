using System;
using System.Collections;

namespace System.Web.UI.WebControls
{
	// Token: 0x020004DE RID: 1246
	public class StyleCollection : StateManagedCollection
	{
		// Token: 0x06003E32 RID: 15922 RVA: 0x00095F2B File Offset: 0x0009412B
		internal StyleCollection()
		{
		}

		// Token: 0x1700122B RID: 4651
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

		// Token: 0x06003E35 RID: 15925 RVA: 0x000A9CAD File Offset: 0x000A7EAD
		public int Add(Style style)
		{
			return ((IList)this).Add(style);
		}

		// Token: 0x06003E36 RID: 15926 RVA: 0x00095DD0 File Offset: 0x00093FD0
		public bool Contains(Style style)
		{
			return ((IList)this).Contains(style);
		}

		// Token: 0x06003E37 RID: 15927 RVA: 0x000B7C0D File Offset: 0x000B5E0D
		public void CopyTo(Style[] styleArray, int index)
		{
			base.CopyTo(styleArray, index);
		}

		// Token: 0x06003E38 RID: 15928 RVA: 0x00095E55 File Offset: 0x00094055
		public int IndexOf(Style style)
		{
			return ((IList)this).IndexOf(style);
		}

		// Token: 0x06003E39 RID: 15929 RVA: 0x00095E5E File Offset: 0x0009405E
		public void Insert(int index, Style style)
		{
			((IList)this).Insert(index, style);
		}

		// Token: 0x06003E3A RID: 15930 RVA: 0x000C8D24 File Offset: 0x000C6F24
		protected override object CreateKnownType(int index)
		{
			return new Style();
		}

		// Token: 0x06003E3B RID: 15931 RVA: 0x000C8D2B File Offset: 0x000C6F2B
		protected override Type[] GetKnownTypes()
		{
			return StyleCollection.knownTypes;
		}

		// Token: 0x06003E3C RID: 15932 RVA: 0x00095F15 File Offset: 0x00094115
		public void Remove(Style style)
		{
			((IList)this).Remove(style);
		}

		// Token: 0x06003E3D RID: 15933 RVA: 0x00095F0C File Offset: 0x0009410C
		public void RemoveAt(int index)
		{
			((IList)this).RemoveAt(index);
		}

		// Token: 0x06003E3E RID: 15934 RVA: 0x000C8D32 File Offset: 0x000C6F32
		protected override void SetDirtyObject(object o)
		{
			if (o is Style)
			{
				((Style)o).SetDirty();
			}
		}

		// Token: 0x04002407 RID: 9223
		private static readonly Type[] knownTypes = new Type[]
		{
			typeof(Style)
		};
	}
}
