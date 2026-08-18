using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x02000585 RID: 1413
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class ListChangedEventArgs : EventArgs
	{
		// Token: 0x06003434 RID: 13364 RVA: 0x000E4CE2 File Offset: 0x000E2EE2
		public ListChangedEventArgs(ListChangedType listChangedType, int newIndex) : this(listChangedType, newIndex, -1)
		{
		}

		// Token: 0x06003435 RID: 13365 RVA: 0x000E4CED File Offset: 0x000E2EED
		public ListChangedEventArgs(ListChangedType listChangedType, int newIndex, PropertyDescriptor propDesc) : this(listChangedType, newIndex)
		{
			this.propDesc = propDesc;
			this.oldIndex = newIndex;
		}

		// Token: 0x06003436 RID: 13366 RVA: 0x000E4D05 File Offset: 0x000E2F05
		public ListChangedEventArgs(ListChangedType listChangedType, PropertyDescriptor propDesc)
		{
			this.listChangedType = listChangedType;
			this.propDesc = propDesc;
		}

		// Token: 0x06003437 RID: 13367 RVA: 0x000E4D1B File Offset: 0x000E2F1B
		public ListChangedEventArgs(ListChangedType listChangedType, int newIndex, int oldIndex)
		{
			this.listChangedType = listChangedType;
			this.newIndex = newIndex;
			this.oldIndex = oldIndex;
		}

		// Token: 0x17000CC1 RID: 3265
		// (get) Token: 0x06003438 RID: 13368 RVA: 0x000E4D38 File Offset: 0x000E2F38
		public ListChangedType ListChangedType
		{
			get
			{
				return this.listChangedType;
			}
		}

		// Token: 0x17000CC2 RID: 3266
		// (get) Token: 0x06003439 RID: 13369 RVA: 0x000E4D40 File Offset: 0x000E2F40
		public int NewIndex
		{
			get
			{
				return this.newIndex;
			}
		}

		// Token: 0x17000CC3 RID: 3267
		// (get) Token: 0x0600343A RID: 13370 RVA: 0x000E4D48 File Offset: 0x000E2F48
		public int OldIndex
		{
			get
			{
				return this.oldIndex;
			}
		}

		// Token: 0x17000CC4 RID: 3268
		// (get) Token: 0x0600343B RID: 13371 RVA: 0x000E4D50 File Offset: 0x000E2F50
		public PropertyDescriptor PropertyDescriptor
		{
			get
			{
				return this.propDesc;
			}
		}

		// Token: 0x040029DC RID: 10716
		private ListChangedType listChangedType;

		// Token: 0x040029DD RID: 10717
		private int newIndex;

		// Token: 0x040029DE RID: 10718
		private int oldIndex;

		// Token: 0x040029DF RID: 10719
		private PropertyDescriptor propDesc;
	}
}
