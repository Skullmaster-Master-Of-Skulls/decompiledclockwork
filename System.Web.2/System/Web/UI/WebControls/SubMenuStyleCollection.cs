using System;
using System.Collections;

namespace System.Web.UI.WebControls
{
	// Token: 0x020004E0 RID: 1248
	public class SubMenuStyleCollection : StateManagedCollection
	{
		// Token: 0x06003E56 RID: 15958 RVA: 0x00095F2B File Offset: 0x0009412B
		internal SubMenuStyleCollection()
		{
		}

		// Token: 0x06003E57 RID: 15959 RVA: 0x000C9254 File Offset: 0x000C7454
		protected override void OnInsert(int index, object value)
		{
			base.OnInsert(index, value);
			if (value is SubMenuStyle)
			{
				SubMenuStyle subMenuStyle = (SubMenuStyle)value;
				subMenuStyle.Font.Underline = subMenuStyle.Font.Underline;
				return;
			}
			throw new ArgumentException(SR.GetString("SubMenuStyleCollection_InvalidArgument"), "value");
		}

		// Token: 0x1700122E RID: 4654
		public SubMenuStyle this[int i]
		{
			get
			{
				return (SubMenuStyle)((IList)this)[i];
			}
			set
			{
				((IList)this)[i] = value;
			}
		}

		// Token: 0x06003E5A RID: 15962 RVA: 0x000A9CAD File Offset: 0x000A7EAD
		public int Add(SubMenuStyle style)
		{
			return ((IList)this).Add(style);
		}

		// Token: 0x06003E5B RID: 15963 RVA: 0x00095DD0 File Offset: 0x00093FD0
		public bool Contains(SubMenuStyle style)
		{
			return ((IList)this).Contains(style);
		}

		// Token: 0x06003E5C RID: 15964 RVA: 0x000B7C0D File Offset: 0x000B5E0D
		public void CopyTo(SubMenuStyle[] styleArray, int index)
		{
			base.CopyTo(styleArray, index);
		}

		// Token: 0x06003E5D RID: 15965 RVA: 0x00095E55 File Offset: 0x00094055
		public int IndexOf(SubMenuStyle style)
		{
			return ((IList)this).IndexOf(style);
		}

		// Token: 0x06003E5E RID: 15966 RVA: 0x00095E5E File Offset: 0x0009405E
		public void Insert(int index, SubMenuStyle style)
		{
			((IList)this).Insert(index, style);
		}

		// Token: 0x06003E5F RID: 15967 RVA: 0x000C92B1 File Offset: 0x000C74B1
		protected override object CreateKnownType(int index)
		{
			return new SubMenuStyle();
		}

		// Token: 0x06003E60 RID: 15968 RVA: 0x000C92B8 File Offset: 0x000C74B8
		protected override Type[] GetKnownTypes()
		{
			return SubMenuStyleCollection.knownTypes;
		}

		// Token: 0x06003E61 RID: 15969 RVA: 0x00095F15 File Offset: 0x00094115
		public void Remove(SubMenuStyle style)
		{
			((IList)this).Remove(style);
		}

		// Token: 0x06003E62 RID: 15970 RVA: 0x00095F0C File Offset: 0x0009410C
		public void RemoveAt(int index)
		{
			((IList)this).RemoveAt(index);
		}

		// Token: 0x06003E63 RID: 15971 RVA: 0x000C92BF File Offset: 0x000C74BF
		protected override void SetDirtyObject(object o)
		{
			if (o is SubMenuStyle)
			{
				((SubMenuStyle)o).SetDirty();
			}
		}

		// Token: 0x0400240A RID: 9226
		private static readonly Type[] knownTypes = new Type[]
		{
			typeof(SubMenuStyle)
		};
	}
}
