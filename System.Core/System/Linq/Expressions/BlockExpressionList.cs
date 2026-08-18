using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic.Utils;

namespace System.Linq.Expressions
{
	// Token: 0x02000221 RID: 545
	internal class BlockExpressionList : IList<Expression>, ICollection<Expression>, IEnumerable<Expression>, IEnumerable
	{
		// Token: 0x060013E6 RID: 5094 RVA: 0x00043CCD File Offset: 0x00041ECD
		internal BlockExpressionList(BlockExpression provider, Expression arg0)
		{
			this._block = provider;
			this._arg0 = arg0;
		}

		// Token: 0x060013E7 RID: 5095 RVA: 0x00043CE4 File Offset: 0x00041EE4
		public int IndexOf(Expression item)
		{
			if (this._arg0 == item)
			{
				return 0;
			}
			for (int i = 1; i < this._block.ExpressionCount; i++)
			{
				if (this._block.GetExpression(i) == item)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060013E8 RID: 5096 RVA: 0x00043D24 File Offset: 0x00041F24
		public void Insert(int index, Expression item)
		{
			throw ContractUtils.Unreachable;
		}

		// Token: 0x060013E9 RID: 5097 RVA: 0x00043D2B File Offset: 0x00041F2B
		public void RemoveAt(int index)
		{
			throw ContractUtils.Unreachable;
		}

		// Token: 0x1700035A RID: 858
		public Expression this[int index]
		{
			get
			{
				if (index == 0)
				{
					return this._arg0;
				}
				return this._block.GetExpression(index);
			}
			set
			{
				throw ContractUtils.Unreachable;
			}
		}

		// Token: 0x060013EC RID: 5100 RVA: 0x00043D51 File Offset: 0x00041F51
		public void Add(Expression item)
		{
			throw ContractUtils.Unreachable;
		}

		// Token: 0x060013ED RID: 5101 RVA: 0x00043D58 File Offset: 0x00041F58
		public void Clear()
		{
			throw ContractUtils.Unreachable;
		}

		// Token: 0x060013EE RID: 5102 RVA: 0x00043D5F File Offset: 0x00041F5F
		public bool Contains(Expression item)
		{
			return this.IndexOf(item) != -1;
		}

		// Token: 0x060013EF RID: 5103 RVA: 0x00043D70 File Offset: 0x00041F70
		public void CopyTo(Expression[] array, int arrayIndex)
		{
			array[arrayIndex++] = this._arg0;
			for (int i = 1; i < this._block.ExpressionCount; i++)
			{
				array[arrayIndex++] = this._block.GetExpression(i);
			}
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x060013F0 RID: 5104 RVA: 0x00043DB5 File Offset: 0x00041FB5
		public int Count
		{
			get
			{
				return this._block.ExpressionCount;
			}
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x060013F1 RID: 5105 RVA: 0x00043DC2 File Offset: 0x00041FC2
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060013F2 RID: 5106 RVA: 0x00043DC5 File Offset: 0x00041FC5
		public bool Remove(Expression item)
		{
			throw ContractUtils.Unreachable;
		}

		// Token: 0x060013F3 RID: 5107 RVA: 0x00043DCC File Offset: 0x00041FCC
		public IEnumerator<Expression> GetEnumerator()
		{
			yield return this._arg0;
			int num;
			for (int i = 1; i < this._block.ExpressionCount; i = num + 1)
			{
				yield return this._block.GetExpression(i);
				num = i;
			}
			yield break;
		}

		// Token: 0x060013F4 RID: 5108 RVA: 0x00043DDB File Offset: 0x00041FDB
		IEnumerator IEnumerable.GetEnumerator()
		{
			yield return this._arg0;
			int num;
			for (int i = 1; i < this._block.ExpressionCount; i = num + 1)
			{
				yield return this._block.GetExpression(i);
				num = i;
			}
			yield break;
		}

		// Token: 0x04000975 RID: 2421
		private readonly BlockExpression _block;

		// Token: 0x04000976 RID: 2422
		private readonly Expression _arg0;
	}
}
