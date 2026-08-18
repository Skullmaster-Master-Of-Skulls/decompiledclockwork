using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000574 RID: 1396
	[Editor("System.Web.UI.Design.WebControls.EmbeddedMailObjectCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class EmbeddedMailObjectsCollection : CollectionBase
	{
		// Token: 0x170010CD RID: 4301
		public EmbeddedMailObject this[int index]
		{
			get
			{
				return (EmbeddedMailObject)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		// Token: 0x06004490 RID: 17552 RVA: 0x0011A0FE File Offset: 0x001190FE
		public int Add(EmbeddedMailObject value)
		{
			return base.List.Add(value);
		}

		// Token: 0x06004491 RID: 17553 RVA: 0x0011A10C File Offset: 0x0011910C
		public bool Contains(EmbeddedMailObject value)
		{
			return base.List.Contains(value);
		}

		// Token: 0x06004492 RID: 17554 RVA: 0x0011A11A File Offset: 0x0011911A
		public void CopyTo(EmbeddedMailObject[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		// Token: 0x06004493 RID: 17555 RVA: 0x0011A129 File Offset: 0x00119129
		public int IndexOf(EmbeddedMailObject value)
		{
			return base.List.IndexOf(value);
		}

		// Token: 0x06004494 RID: 17556 RVA: 0x0011A137 File Offset: 0x00119137
		public void Insert(int index, EmbeddedMailObject value)
		{
			base.List.Insert(index, value);
		}

		// Token: 0x06004495 RID: 17557 RVA: 0x0011A148 File Offset: 0x00119148
		protected override void OnValidate(object value)
		{
			base.OnValidate(value);
			if (value == null)
			{
				throw new ArgumentNullException("value", SR.GetString("Collection_CantAddNull"));
			}
			if (!(value is EmbeddedMailObject))
			{
				throw new ArgumentException(SR.GetString("Collection_InvalidType", new object[]
				{
					"EmbeddedMailObject"
				}), "value");
			}
		}

		// Token: 0x06004496 RID: 17558 RVA: 0x0011A1A1 File Offset: 0x001191A1
		public void Remove(EmbeddedMailObject value)
		{
			base.List.Remove(value);
		}
	}
}
