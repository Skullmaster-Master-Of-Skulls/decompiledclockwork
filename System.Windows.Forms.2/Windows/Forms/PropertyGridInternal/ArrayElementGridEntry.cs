using System;
using System.Globalization;

namespace System.Windows.Forms.PropertyGridInternal
{
	// Token: 0x020004FB RID: 1275
	internal class ArrayElementGridEntry : GridEntry
	{
		// Token: 0x0600538A RID: 21386 RVA: 0x0015E209 File Offset: 0x0015C409
		public ArrayElementGridEntry(PropertyGrid ownerGrid, GridEntry peParent, int index) : base(ownerGrid, peParent)
		{
			this.index = index;
			this.SetFlag(256, (peParent.Flags & 256) != 0 || peParent.ForceReadOnly);
		}

		// Token: 0x170013EF RID: 5103
		// (get) Token: 0x0600538B RID: 21387 RVA: 0x0001627D File Offset: 0x0001447D
		public override GridItemType GridItemType
		{
			get
			{
				return GridItemType.ArrayValue;
			}
		}

		// Token: 0x170013F0 RID: 5104
		// (get) Token: 0x0600538C RID: 21388 RVA: 0x0015E23C File Offset: 0x0015C43C
		public override bool IsValueEditable
		{
			get
			{
				return this.ParentGridEntry.IsValueEditable;
			}
		}

		// Token: 0x170013F1 RID: 5105
		// (get) Token: 0x0600538D RID: 21389 RVA: 0x0015E249 File Offset: 0x0015C449
		public override string PropertyLabel
		{
			get
			{
				return "[" + this.index.ToString(CultureInfo.CurrentCulture) + "]";
			}
		}

		// Token: 0x170013F2 RID: 5106
		// (get) Token: 0x0600538E RID: 21390 RVA: 0x0015E26A File Offset: 0x0015C46A
		public override Type PropertyType
		{
			get
			{
				return this.parentPE.PropertyType.GetElementType();
			}
		}

		// Token: 0x170013F3 RID: 5107
		// (get) Token: 0x0600538F RID: 21391 RVA: 0x0015E27C File Offset: 0x0015C47C
		// (set) Token: 0x06005390 RID: 21392 RVA: 0x0015E2A4 File Offset: 0x0015C4A4
		public override object PropertyValue
		{
			get
			{
				object valueOwner = this.GetValueOwner();
				return ((Array)valueOwner).GetValue(this.index);
			}
			set
			{
				object valueOwner = this.GetValueOwner();
				((Array)valueOwner).SetValue(value, this.index);
			}
		}

		// Token: 0x170013F4 RID: 5108
		// (get) Token: 0x06005391 RID: 21393 RVA: 0x0015E2CA File Offset: 0x0015C4CA
		public override bool ShouldRenderReadOnly
		{
			get
			{
				return this.ParentGridEntry.ShouldRenderReadOnly;
			}
		}

		// Token: 0x040036BF RID: 14015
		protected int index;
	}
}
