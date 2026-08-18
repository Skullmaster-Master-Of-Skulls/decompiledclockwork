using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	// Token: 0x02000631 RID: 1585
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeDirectiveCollection : CollectionBase
	{
		// Token: 0x060039C4 RID: 14788 RVA: 0x000F341F File Offset: 0x000F161F
		public CodeDirectiveCollection()
		{
		}

		// Token: 0x060039C5 RID: 14789 RVA: 0x000F3427 File Offset: 0x000F1627
		public CodeDirectiveCollection(CodeDirectiveCollection value)
		{
			this.AddRange(value);
		}

		// Token: 0x060039C6 RID: 14790 RVA: 0x000F3436 File Offset: 0x000F1636
		public CodeDirectiveCollection(CodeDirective[] value)
		{
			this.AddRange(value);
		}

		// Token: 0x17000DDA RID: 3546
		public CodeDirective this[int index]
		{
			get
			{
				return (CodeDirective)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		// Token: 0x060039C9 RID: 14793 RVA: 0x000F3467 File Offset: 0x000F1667
		public int Add(CodeDirective value)
		{
			return base.List.Add(value);
		}

		// Token: 0x060039CA RID: 14794 RVA: 0x000F3478 File Offset: 0x000F1678
		public void AddRange(CodeDirective[] value)
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

		// Token: 0x060039CB RID: 14795 RVA: 0x000F34AC File Offset: 0x000F16AC
		public void AddRange(CodeDirectiveCollection value)
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

		// Token: 0x060039CC RID: 14796 RVA: 0x000F34E8 File Offset: 0x000F16E8
		public bool Contains(CodeDirective value)
		{
			return base.List.Contains(value);
		}

		// Token: 0x060039CD RID: 14797 RVA: 0x000F34F6 File Offset: 0x000F16F6
		public void CopyTo(CodeDirective[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		// Token: 0x060039CE RID: 14798 RVA: 0x000F3505 File Offset: 0x000F1705
		public int IndexOf(CodeDirective value)
		{
			return base.List.IndexOf(value);
		}

		// Token: 0x060039CF RID: 14799 RVA: 0x000F3513 File Offset: 0x000F1713
		public void Insert(int index, CodeDirective value)
		{
			base.List.Insert(index, value);
		}

		// Token: 0x060039D0 RID: 14800 RVA: 0x000F3522 File Offset: 0x000F1722
		public void Remove(CodeDirective value)
		{
			base.List.Remove(value);
		}
	}
}
