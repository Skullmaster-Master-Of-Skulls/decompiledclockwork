using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic.Utils;

namespace System.Linq.Expressions
{
	// Token: 0x02000248 RID: 584
	internal class ListArgumentProvider : IList<Expression>, ICollection<Expression>, IEnumerable<Expression>, IEnumerable
	{
		// Token: 0x0600156A RID: 5482 RVA: 0x000484D7 File Offset: 0x000466D7
		internal ListArgumentProvider(IArgumentProvider provider, Expression arg0)
		{
			this._provider = provider;
			this._arg0 = arg0;
		}

		// Token: 0x0600156B RID: 5483 RVA: 0x000484F0 File Offset: 0x000466F0
		public int IndexOf(Expression item)
		{
			if (this._arg0 == item)
			{
				return 0;
			}
			for (int i = 1; i < this._provider.ArgumentCount; i++)
			{
				if (this._provider.GetArgument(i) == item)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600156C RID: 5484 RVA: 0x00048530 File Offset: 0x00046730
		public void Insert(int index, Expression item)
		{
			throw ContractUtils.Unreachable;
		}

		// Token: 0x0600156D RID: 5485 RVA: 0x00048537 File Offset: 0x00046737
		public void RemoveAt(int index)
		{
			throw ContractUtils.Unreachable;
		}

		// Token: 0x170003B5 RID: 949
		public Expression this[int index]
		{
			get
			{
				if (index == 0)
				{
					return this._arg0;
				}
				return this._provider.GetArgument(index);
			}
			set
			{
				throw ContractUtils.Unreachable;
			}
		}

		// Token: 0x06001570 RID: 5488 RVA: 0x0004855D File Offset: 0x0004675D
		public void Add(Expression item)
		{
			throw ContractUtils.Unreachable;
		}

		// Token: 0x06001571 RID: 5489 RVA: 0x00048564 File Offset: 0x00046764
		public void Clear()
		{
			throw ContractUtils.Unreachable;
		}

		// Token: 0x06001572 RID: 5490 RVA: 0x0004856B File Offset: 0x0004676B
		public bool Contains(Expression item)
		{
			return this.IndexOf(item) != -1;
		}

		// Token: 0x06001573 RID: 5491 RVA: 0x0004857C File Offset: 0x0004677C
		public void CopyTo(Expression[] array, int arrayIndex)
		{
			array[arrayIndex++] = this._arg0;
			for (int i = 1; i < this._provider.ArgumentCount; i++)
			{
				array[arrayIndex++] = this._provider.GetArgument(i);
			}
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06001574 RID: 5492 RVA: 0x000485C1 File Offset: 0x000467C1
		public int Count
		{
			get
			{
				return this._provider.ArgumentCount;
			}
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06001575 RID: 5493 RVA: 0x000485CE File Offset: 0x000467CE
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001576 RID: 5494 RVA: 0x000485D1 File Offset: 0x000467D1
		public bool Remove(Expression item)
		{
			throw ContractUtils.Unreachable;
		}

		// Token: 0x06001577 RID: 5495 RVA: 0x000485D8 File Offset: 0x000467D8
		public IEnumerator<Expression> GetEnumerator()
		{
			yield return this._arg0;
			int num;
			for (int i = 1; i < this._provider.ArgumentCount; i = num + 1)
			{
				yield return this._provider.GetArgument(i);
				num = i;
			}
			yield break;
		}

		// Token: 0x06001578 RID: 5496 RVA: 0x000485E7 File Offset: 0x000467E7
		IEnumerator IEnumerable.GetEnumerator()
		{
			yield return this._arg0;
			int num;
			for (int i = 1; i < this._provider.ArgumentCount; i = num + 1)
			{
				yield return this._provider.GetArgument(i);
				num = i;
			}
			yield break;
		}

		// Token: 0x04000A16 RID: 2582
		private readonly IArgumentProvider _provider;

		// Token: 0x04000A17 RID: 2583
		private readonly Expression _arg0;
	}
}
