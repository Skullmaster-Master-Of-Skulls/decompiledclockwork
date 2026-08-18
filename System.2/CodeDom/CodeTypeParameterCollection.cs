using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	// Token: 0x02000663 RID: 1635
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeTypeParameterCollection : CollectionBase
	{
		// Token: 0x06003B3F RID: 15167 RVA: 0x000F53CD File Offset: 0x000F35CD
		public CodeTypeParameterCollection()
		{
		}

		// Token: 0x06003B40 RID: 15168 RVA: 0x000F53D5 File Offset: 0x000F35D5
		public CodeTypeParameterCollection(CodeTypeParameterCollection value)
		{
			this.AddRange(value);
		}

		// Token: 0x06003B41 RID: 15169 RVA: 0x000F53E4 File Offset: 0x000F35E4
		public CodeTypeParameterCollection(CodeTypeParameter[] value)
		{
			this.AddRange(value);
		}

		// Token: 0x17000E4A RID: 3658
		public CodeTypeParameter this[int index]
		{
			get
			{
				return (CodeTypeParameter)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		// Token: 0x06003B44 RID: 15172 RVA: 0x000F5415 File Offset: 0x000F3615
		public int Add(CodeTypeParameter value)
		{
			return base.List.Add(value);
		}

		// Token: 0x06003B45 RID: 15173 RVA: 0x000F5423 File Offset: 0x000F3623
		public void Add(string value)
		{
			this.Add(new CodeTypeParameter(value));
		}

		// Token: 0x06003B46 RID: 15174 RVA: 0x000F5434 File Offset: 0x000F3634
		public void AddRange(CodeTypeParameter[] value)
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

		// Token: 0x06003B47 RID: 15175 RVA: 0x000F5468 File Offset: 0x000F3668
		public void AddRange(CodeTypeParameterCollection value)
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

		// Token: 0x06003B48 RID: 15176 RVA: 0x000F54A4 File Offset: 0x000F36A4
		public bool Contains(CodeTypeParameter value)
		{
			return base.List.Contains(value);
		}

		// Token: 0x06003B49 RID: 15177 RVA: 0x000F54B2 File Offset: 0x000F36B2
		public void CopyTo(CodeTypeParameter[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		// Token: 0x06003B4A RID: 15178 RVA: 0x000F54C1 File Offset: 0x000F36C1
		public int IndexOf(CodeTypeParameter value)
		{
			return base.List.IndexOf(value);
		}

		// Token: 0x06003B4B RID: 15179 RVA: 0x000F54CF File Offset: 0x000F36CF
		public void Insert(int index, CodeTypeParameter value)
		{
			base.List.Insert(index, value);
		}

		// Token: 0x06003B4C RID: 15180 RVA: 0x000F54DE File Offset: 0x000F36DE
		public void Remove(CodeTypeParameter value)
		{
			base.List.Remove(value);
		}
	}
}
