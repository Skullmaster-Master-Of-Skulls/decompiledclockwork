using System;
using System.Collections;
using System.Security.Permissions;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x02000743 RID: 1859
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class WebPartTransformerCollection : CollectionBase
	{
		// Token: 0x1700174F RID: 5967
		// (get) Token: 0x06005A31 RID: 23089 RVA: 0x0016C306 File Offset: 0x0016B306
		public bool IsReadOnly
		{
			get
			{
				return this._readOnly;
			}
		}

		// Token: 0x17001750 RID: 5968
		public WebPartTransformer this[int index]
		{
			get
			{
				return (WebPartTransformer)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		// Token: 0x06005A34 RID: 23092 RVA: 0x0016C330 File Offset: 0x0016B330
		public int Add(WebPartTransformer transformer)
		{
			return base.List.Add(transformer);
		}

		// Token: 0x06005A35 RID: 23093 RVA: 0x0016C33E File Offset: 0x0016B33E
		private void CheckReadOnly()
		{
			if (this._readOnly)
			{
				throw new InvalidOperationException(SR.GetString("WebPartTransformerCollection_ReadOnly"));
			}
		}

		// Token: 0x06005A36 RID: 23094 RVA: 0x0016C358 File Offset: 0x0016B358
		public bool Contains(WebPartTransformer transformer)
		{
			return base.List.Contains(transformer);
		}

		// Token: 0x06005A37 RID: 23095 RVA: 0x0016C366 File Offset: 0x0016B366
		public void CopyTo(WebPartTransformer[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		// Token: 0x06005A38 RID: 23096 RVA: 0x0016C375 File Offset: 0x0016B375
		public int IndexOf(WebPartTransformer transformer)
		{
			return base.List.IndexOf(transformer);
		}

		// Token: 0x06005A39 RID: 23097 RVA: 0x0016C383 File Offset: 0x0016B383
		public void Insert(int index, WebPartTransformer transformer)
		{
			base.List.Insert(index, transformer);
		}

		// Token: 0x06005A3A RID: 23098 RVA: 0x0016C392 File Offset: 0x0016B392
		protected override void OnClear()
		{
			this.CheckReadOnly();
			base.OnClear();
		}

		// Token: 0x06005A3B RID: 23099 RVA: 0x0016C3A0 File Offset: 0x0016B3A0
		protected override void OnInsert(int index, object value)
		{
			this.CheckReadOnly();
			if (base.List.Count > 0)
			{
				throw new InvalidOperationException(SR.GetString("WebPartTransformerCollection_NotEmpty"));
			}
			base.OnInsert(index, value);
		}

		// Token: 0x06005A3C RID: 23100 RVA: 0x0016C3CE File Offset: 0x0016B3CE
		protected override void OnRemove(int index, object value)
		{
			this.CheckReadOnly();
			base.OnRemove(index, value);
		}

		// Token: 0x06005A3D RID: 23101 RVA: 0x0016C3DE File Offset: 0x0016B3DE
		protected override void OnSet(int index, object oldValue, object newValue)
		{
			this.CheckReadOnly();
			base.OnSet(index, oldValue, newValue);
		}

		// Token: 0x06005A3E RID: 23102 RVA: 0x0016C3F0 File Offset: 0x0016B3F0
		protected override void OnValidate(object value)
		{
			base.OnValidate(value);
			if (value == null)
			{
				throw new ArgumentNullException("value", SR.GetString("Collection_CantAddNull"));
			}
			if (!(value is WebPartTransformer))
			{
				throw new ArgumentException(SR.GetString("Collection_InvalidType", new object[]
				{
					"WebPartTransformer"
				}), "value");
			}
		}

		// Token: 0x06005A3F RID: 23103 RVA: 0x0016C449 File Offset: 0x0016B449
		public void Remove(WebPartTransformer transformer)
		{
			base.List.Remove(transformer);
		}

		// Token: 0x06005A40 RID: 23104 RVA: 0x0016C457 File Offset: 0x0016B457
		internal void SetReadOnly()
		{
			this._readOnly = true;
		}

		// Token: 0x04003083 RID: 12419
		private bool _readOnly;
	}
}
