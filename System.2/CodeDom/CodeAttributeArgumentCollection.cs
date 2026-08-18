using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	// Token: 0x0200061C RID: 1564
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeAttributeArgumentCollection : CollectionBase
	{
		// Token: 0x0600392F RID: 14639 RVA: 0x000F2976 File Offset: 0x000F0B76
		public CodeAttributeArgumentCollection()
		{
		}

		// Token: 0x06003930 RID: 14640 RVA: 0x000F297E File Offset: 0x000F0B7E
		public CodeAttributeArgumentCollection(CodeAttributeArgumentCollection value)
		{
			this.AddRange(value);
		}

		// Token: 0x06003931 RID: 14641 RVA: 0x000F298D File Offset: 0x000F0B8D
		public CodeAttributeArgumentCollection(CodeAttributeArgument[] value)
		{
			this.AddRange(value);
		}

		// Token: 0x17000DB3 RID: 3507
		public CodeAttributeArgument this[int index]
		{
			get
			{
				return (CodeAttributeArgument)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		// Token: 0x06003934 RID: 14644 RVA: 0x000F29BE File Offset: 0x000F0BBE
		public int Add(CodeAttributeArgument value)
		{
			return base.List.Add(value);
		}

		// Token: 0x06003935 RID: 14645 RVA: 0x000F29CC File Offset: 0x000F0BCC
		public void AddRange(CodeAttributeArgument[] value)
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

		// Token: 0x06003936 RID: 14646 RVA: 0x000F2A00 File Offset: 0x000F0C00
		public void AddRange(CodeAttributeArgumentCollection value)
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

		// Token: 0x06003937 RID: 14647 RVA: 0x000F2A3C File Offset: 0x000F0C3C
		public bool Contains(CodeAttributeArgument value)
		{
			return base.List.Contains(value);
		}

		// Token: 0x06003938 RID: 14648 RVA: 0x000F2A4A File Offset: 0x000F0C4A
		public void CopyTo(CodeAttributeArgument[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		// Token: 0x06003939 RID: 14649 RVA: 0x000F2A59 File Offset: 0x000F0C59
		public int IndexOf(CodeAttributeArgument value)
		{
			return base.List.IndexOf(value);
		}

		// Token: 0x0600393A RID: 14650 RVA: 0x000F2A67 File Offset: 0x000F0C67
		public void Insert(int index, CodeAttributeArgument value)
		{
			base.List.Insert(index, value);
		}

		// Token: 0x0600393B RID: 14651 RVA: 0x000F2A76 File Offset: 0x000F0C76
		public void Remove(CodeAttributeArgument value)
		{
			base.List.Remove(value);
		}
	}
}
