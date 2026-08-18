using System;
using System.Collections;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000653 RID: 1619
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class SubMenuStyleCollection : StateManagedCollection
	{
		// Token: 0x06004F60 RID: 20320 RVA: 0x0013F6AF File Offset: 0x0013E6AF
		internal SubMenuStyleCollection()
		{
		}

		// Token: 0x06004F61 RID: 20321 RVA: 0x0013F6B8 File Offset: 0x0013E6B8
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

		// Token: 0x17001414 RID: 5140
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

		// Token: 0x06004F64 RID: 20324 RVA: 0x0013F71F File Offset: 0x0013E71F
		public int Add(SubMenuStyle style)
		{
			return ((IList)this).Add(style);
		}

		// Token: 0x06004F65 RID: 20325 RVA: 0x0013F728 File Offset: 0x0013E728
		public bool Contains(SubMenuStyle style)
		{
			return ((IList)this).Contains(style);
		}

		// Token: 0x06004F66 RID: 20326 RVA: 0x0013F731 File Offset: 0x0013E731
		public void CopyTo(SubMenuStyle[] styleArray, int index)
		{
			base.CopyTo(styleArray, index);
		}

		// Token: 0x06004F67 RID: 20327 RVA: 0x0013F73B File Offset: 0x0013E73B
		public int IndexOf(SubMenuStyle style)
		{
			return ((IList)this).IndexOf(style);
		}

		// Token: 0x06004F68 RID: 20328 RVA: 0x0013F744 File Offset: 0x0013E744
		public void Insert(int index, SubMenuStyle style)
		{
			((IList)this).Insert(index, style);
		}

		// Token: 0x06004F69 RID: 20329 RVA: 0x0013F74E File Offset: 0x0013E74E
		protected override object CreateKnownType(int index)
		{
			return new SubMenuStyle();
		}

		// Token: 0x06004F6A RID: 20330 RVA: 0x0013F755 File Offset: 0x0013E755
		protected override Type[] GetKnownTypes()
		{
			return SubMenuStyleCollection.knownTypes;
		}

		// Token: 0x06004F6B RID: 20331 RVA: 0x0013F75C File Offset: 0x0013E75C
		public void Remove(SubMenuStyle style)
		{
			((IList)this).Remove(style);
		}

		// Token: 0x06004F6C RID: 20332 RVA: 0x0013F765 File Offset: 0x0013E765
		public void RemoveAt(int index)
		{
			((IList)this).RemoveAt(index);
		}

		// Token: 0x06004F6D RID: 20333 RVA: 0x0013F76E File Offset: 0x0013E76E
		protected override void SetDirtyObject(object o)
		{
			if (o is SubMenuStyle)
			{
				((SubMenuStyle)o).SetDirty();
			}
		}

		// Token: 0x04002CE2 RID: 11490
		private static readonly Type[] knownTypes = new Type[]
		{
			typeof(SubMenuStyle)
		};
	}
}
