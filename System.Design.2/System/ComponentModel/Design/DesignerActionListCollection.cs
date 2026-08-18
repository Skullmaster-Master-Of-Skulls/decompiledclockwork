using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.ComponentModel.Design
{
	// Token: 0x0200019E RID: 414
	[ComVisible(true)]
	[SecurityCritical]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public class DesignerActionListCollection : CollectionBase
	{
		// Token: 0x06000F3A RID: 3898 RVA: 0x00057954 File Offset: 0x00055B54
		public DesignerActionListCollection()
		{
		}

		// Token: 0x06000F3B RID: 3899 RVA: 0x0005795C File Offset: 0x00055B5C
		internal DesignerActionListCollection(DesignerActionList actionList)
		{
			this.Add(actionList);
		}

		// Token: 0x06000F3C RID: 3900 RVA: 0x0005796C File Offset: 0x00055B6C
		public DesignerActionListCollection(DesignerActionList[] value)
		{
			this.AddRange(value);
		}

		// Token: 0x1700039B RID: 923
		public DesignerActionList this[int index]
		{
			get
			{
				return (DesignerActionList)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		// Token: 0x06000F3F RID: 3903 RVA: 0x0005799D File Offset: 0x00055B9D
		public int Add(DesignerActionList value)
		{
			return base.List.Add(value);
		}

		// Token: 0x06000F40 RID: 3904 RVA: 0x000579AC File Offset: 0x00055BAC
		public void AddRange(DesignerActionList[] value)
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

		// Token: 0x06000F41 RID: 3905 RVA: 0x000579E0 File Offset: 0x00055BE0
		public void AddRange(DesignerActionListCollection value)
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

		// Token: 0x06000F42 RID: 3906 RVA: 0x00057A1C File Offset: 0x00055C1C
		public void Insert(int index, DesignerActionList value)
		{
			base.List.Insert(index, value);
		}

		// Token: 0x06000F43 RID: 3907 RVA: 0x00057A2B File Offset: 0x00055C2B
		public int IndexOf(DesignerActionList value)
		{
			return base.List.IndexOf(value);
		}

		// Token: 0x06000F44 RID: 3908 RVA: 0x00057A39 File Offset: 0x00055C39
		public bool Contains(DesignerActionList value)
		{
			return base.List.Contains(value);
		}

		// Token: 0x06000F45 RID: 3909 RVA: 0x00057A47 File Offset: 0x00055C47
		public void Remove(DesignerActionList value)
		{
			base.List.Remove(value);
		}

		// Token: 0x06000F46 RID: 3910 RVA: 0x00057A55 File Offset: 0x00055C55
		public void CopyTo(DesignerActionList[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		// Token: 0x06000F47 RID: 3911 RVA: 0x00003937 File Offset: 0x00001B37
		protected override void OnSet(int index, object oldValue, object newValue)
		{
		}

		// Token: 0x06000F48 RID: 3912 RVA: 0x00003937 File Offset: 0x00001B37
		protected override void OnInsert(int index, object value)
		{
		}

		// Token: 0x06000F49 RID: 3913 RVA: 0x00003937 File Offset: 0x00001B37
		protected override void OnClear()
		{
		}

		// Token: 0x06000F4A RID: 3914 RVA: 0x00003937 File Offset: 0x00001B37
		protected override void OnRemove(int index, object value)
		{
		}

		// Token: 0x06000F4B RID: 3915 RVA: 0x00003937 File Offset: 0x00001B37
		protected override void OnValidate(object value)
		{
		}
	}
}
