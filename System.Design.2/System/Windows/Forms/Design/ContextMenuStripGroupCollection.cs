using System;
using System.Collections;

namespace System.Windows.Forms.Design
{
	// Token: 0x020002AD RID: 685
	internal class ContextMenuStripGroupCollection : DictionaryBase
	{
		// Token: 0x170005DE RID: 1502
		public ContextMenuStripGroup this[string key]
		{
			get
			{
				if (!base.InnerHashtable.ContainsKey(key))
				{
					base.InnerHashtable[key] = new ContextMenuStripGroup(key);
				}
				return base.InnerHashtable[key] as ContextMenuStripGroup;
			}
		}

		// Token: 0x06001AC4 RID: 6852 RVA: 0x0009C47F File Offset: 0x0009A67F
		public bool ContainsKey(string key)
		{
			return base.InnerHashtable.ContainsKey(key);
		}

		// Token: 0x06001AC5 RID: 6853 RVA: 0x0009C48D File Offset: 0x0009A68D
		protected override void OnInsert(object key, object value)
		{
			if (!(value is ContextMenuStripGroup))
			{
				throw new NotSupportedException();
			}
			base.OnInsert(key, value);
		}

		// Token: 0x06001AC6 RID: 6854 RVA: 0x0009C4A5 File Offset: 0x0009A6A5
		protected override void OnSet(object key, object oldValue, object newValue)
		{
			if (!(newValue is ContextMenuStripGroup))
			{
				throw new NotSupportedException();
			}
			base.OnSet(key, oldValue, newValue);
		}
	}
}
