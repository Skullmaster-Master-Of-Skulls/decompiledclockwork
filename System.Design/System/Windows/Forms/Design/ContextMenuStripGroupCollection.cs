using System;
using System.Collections;

namespace System.Windows.Forms.Design
{
	// Token: 0x020001CF RID: 463
	internal class ContextMenuStripGroupCollection : DictionaryBase
	{
		// Token: 0x170002E4 RID: 740
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

		// Token: 0x060011FA RID: 4602 RVA: 0x0005742E File Offset: 0x0005642E
		public bool ContainsKey(string key)
		{
			return base.InnerHashtable.ContainsKey(key);
		}

		// Token: 0x060011FB RID: 4603 RVA: 0x0005743C File Offset: 0x0005643C
		protected override void OnInsert(object key, object value)
		{
			if (!(value is ContextMenuStripGroup))
			{
				throw new NotSupportedException();
			}
			base.OnInsert(key, value);
		}

		// Token: 0x060011FC RID: 4604 RVA: 0x00057454 File Offset: 0x00056454
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
