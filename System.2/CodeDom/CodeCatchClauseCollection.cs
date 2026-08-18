using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	// Token: 0x02000624 RID: 1572
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeCatchClauseCollection : CollectionBase
	{
		// Token: 0x0600396C RID: 14700 RVA: 0x000F2E0D File Offset: 0x000F100D
		public CodeCatchClauseCollection()
		{
		}

		// Token: 0x0600396D RID: 14701 RVA: 0x000F2E15 File Offset: 0x000F1015
		public CodeCatchClauseCollection(CodeCatchClauseCollection value)
		{
			this.AddRange(value);
		}

		// Token: 0x0600396E RID: 14702 RVA: 0x000F2E24 File Offset: 0x000F1024
		public CodeCatchClauseCollection(CodeCatchClause[] value)
		{
			this.AddRange(value);
		}

		// Token: 0x17000DC0 RID: 3520
		public CodeCatchClause this[int index]
		{
			get
			{
				return (CodeCatchClause)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		// Token: 0x06003971 RID: 14705 RVA: 0x000F2E55 File Offset: 0x000F1055
		public int Add(CodeCatchClause value)
		{
			return base.List.Add(value);
		}

		// Token: 0x06003972 RID: 14706 RVA: 0x000F2E64 File Offset: 0x000F1064
		public void AddRange(CodeCatchClause[] value)
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

		// Token: 0x06003973 RID: 14707 RVA: 0x000F2E98 File Offset: 0x000F1098
		public void AddRange(CodeCatchClauseCollection value)
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

		// Token: 0x06003974 RID: 14708 RVA: 0x000F2ED4 File Offset: 0x000F10D4
		public bool Contains(CodeCatchClause value)
		{
			return base.List.Contains(value);
		}

		// Token: 0x06003975 RID: 14709 RVA: 0x000F2EE2 File Offset: 0x000F10E2
		public void CopyTo(CodeCatchClause[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		// Token: 0x06003976 RID: 14710 RVA: 0x000F2EF1 File Offset: 0x000F10F1
		public int IndexOf(CodeCatchClause value)
		{
			return base.List.IndexOf(value);
		}

		// Token: 0x06003977 RID: 14711 RVA: 0x000F2EFF File Offset: 0x000F10FF
		public void Insert(int index, CodeCatchClause value)
		{
			base.List.Insert(index, value);
		}

		// Token: 0x06003978 RID: 14712 RVA: 0x000F2F0E File Offset: 0x000F110E
		public void Remove(CodeCatchClause value)
		{
			base.List.Remove(value);
		}
	}
}
