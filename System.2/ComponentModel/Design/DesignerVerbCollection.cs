using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.ComponentModel.Design
{
	// Token: 0x020005D9 RID: 1497
	[ComVisible(true)]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public class DesignerVerbCollection : CollectionBase
	{
		// Token: 0x060037AB RID: 14251 RVA: 0x000F0BD5 File Offset: 0x000EEDD5
		public DesignerVerbCollection()
		{
		}

		// Token: 0x060037AC RID: 14252 RVA: 0x000F0BDD File Offset: 0x000EEDDD
		public DesignerVerbCollection(DesignerVerb[] value)
		{
			this.AddRange(value);
		}

		// Token: 0x17000D67 RID: 3431
		public DesignerVerb this[int index]
		{
			get
			{
				return (DesignerVerb)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		// Token: 0x060037AF RID: 14255 RVA: 0x000F0C0E File Offset: 0x000EEE0E
		public int Add(DesignerVerb value)
		{
			return base.List.Add(value);
		}

		// Token: 0x060037B0 RID: 14256 RVA: 0x000F0C1C File Offset: 0x000EEE1C
		public void AddRange(DesignerVerb[] value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			for (int i = 0; i < value.Length; i++)
			{
				this.Add(value[i]);
			}
		}

		// Token: 0x060037B1 RID: 14257 RVA: 0x000F0C50 File Offset: 0x000EEE50
		public void AddRange(DesignerVerbCollection value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			int count = value.Count;
			for (int i = 0; i < count; i++)
			{
				this.Add(value[i]);
			}
		}

		// Token: 0x060037B2 RID: 14258 RVA: 0x000F0C8C File Offset: 0x000EEE8C
		public void Insert(int index, DesignerVerb value)
		{
			base.List.Insert(index, value);
		}

		// Token: 0x060037B3 RID: 14259 RVA: 0x000F0C9B File Offset: 0x000EEE9B
		public int IndexOf(DesignerVerb value)
		{
			return base.List.IndexOf(value);
		}

		// Token: 0x060037B4 RID: 14260 RVA: 0x000F0CA9 File Offset: 0x000EEEA9
		public bool Contains(DesignerVerb value)
		{
			return base.List.Contains(value);
		}

		// Token: 0x060037B5 RID: 14261 RVA: 0x000F0CB7 File Offset: 0x000EEEB7
		public void Remove(DesignerVerb value)
		{
			base.List.Remove(value);
		}

		// Token: 0x060037B6 RID: 14262 RVA: 0x000F0CC5 File Offset: 0x000EEEC5
		public void CopyTo(DesignerVerb[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		// Token: 0x060037B7 RID: 14263 RVA: 0x000F0CD4 File Offset: 0x000EEED4
		protected override void OnSet(int index, object oldValue, object newValue)
		{
		}

		// Token: 0x060037B8 RID: 14264 RVA: 0x000F0CD6 File Offset: 0x000EEED6
		protected override void OnInsert(int index, object value)
		{
		}

		// Token: 0x060037B9 RID: 14265 RVA: 0x000F0CD8 File Offset: 0x000EEED8
		protected override void OnClear()
		{
		}

		// Token: 0x060037BA RID: 14266 RVA: 0x000F0CDA File Offset: 0x000EEEDA
		protected override void OnRemove(int index, object value)
		{
		}

		// Token: 0x060037BB RID: 14267 RVA: 0x000F0CDC File Offset: 0x000EEEDC
		protected override void OnValidate(object value)
		{
		}
	}
}
