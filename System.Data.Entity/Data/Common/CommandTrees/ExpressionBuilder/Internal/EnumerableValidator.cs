using System;
using System.Collections.Generic;
using System.Data.Common.Utils;
using System.Data.Entity;

namespace System.Data.Common.CommandTrees.ExpressionBuilder.Internal
{
	// Token: 0x0200042B RID: 1067
	internal sealed class EnumerableValidator<TElementIn, TElementOut, TResult>
	{
		// Token: 0x060038DE RID: 14558 RVA: 0x000D83C6 File Offset: 0x000D65C6
		internal EnumerableValidator(IEnumerable<TElementIn> argument, string argumentName)
		{
			this.argumentName = argumentName;
			this.target = argument;
		}

		// Token: 0x17000AA4 RID: 2724
		// (get) Token: 0x060038DF RID: 14559 RVA: 0x000D83E3 File Offset: 0x000D65E3
		// (set) Token: 0x060038E0 RID: 14560 RVA: 0x000D83EB File Offset: 0x000D65EB
		public bool AllowEmpty
		{
			get
			{
				return this.allowEmpty;
			}
			set
			{
				this.allowEmpty = value;
			}
		}

		// Token: 0x17000AA5 RID: 2725
		// (get) Token: 0x060038E1 RID: 14561 RVA: 0x000D83F4 File Offset: 0x000D65F4
		// (set) Token: 0x060038E2 RID: 14562 RVA: 0x000D83FC File Offset: 0x000D65FC
		public int ExpectedElementCount
		{
			get
			{
				return this.expectedElementCount;
			}
			set
			{
				this.expectedElementCount = value;
			}
		}

		// Token: 0x17000AA6 RID: 2726
		// (get) Token: 0x060038E3 RID: 14563 RVA: 0x000D8405 File Offset: 0x000D6605
		// (set) Token: 0x060038E4 RID: 14564 RVA: 0x000D840D File Offset: 0x000D660D
		public Func<TElementIn, int, TElementOut> ConvertElement
		{
			get
			{
				return this.map;
			}
			set
			{
				this.map = value;
			}
		}

		// Token: 0x17000AA7 RID: 2727
		// (get) Token: 0x060038E5 RID: 14565 RVA: 0x000D8416 File Offset: 0x000D6616
		// (set) Token: 0x060038E6 RID: 14566 RVA: 0x000D841E File Offset: 0x000D661E
		public Func<List<TElementOut>, TResult> CreateResult
		{
			get
			{
				return this.collect;
			}
			set
			{
				this.collect = value;
			}
		}

		// Token: 0x17000AA8 RID: 2728
		// (get) Token: 0x060038E7 RID: 14567 RVA: 0x000D8427 File Offset: 0x000D6627
		// (set) Token: 0x060038E8 RID: 14568 RVA: 0x000D842F File Offset: 0x000D662F
		public Func<TElementIn, int, string> GetName
		{
			get
			{
				return this.deriveName;
			}
			set
			{
				this.deriveName = value;
			}
		}

		// Token: 0x060038E9 RID: 14569 RVA: 0x000D8438 File Offset: 0x000D6638
		internal TResult Validate()
		{
			return EnumerableValidator<TElementIn, TElementOut, TResult>.Validate(this.target, this.argumentName, this.ExpectedElementCount, this.AllowEmpty, this.ConvertElement, this.CreateResult, this.GetName);
		}

		// Token: 0x060038EA RID: 14570 RVA: 0x000D846C File Offset: 0x000D666C
		private static TResult Validate(IEnumerable<TElementIn> argument, string argumentName, int expectedElementCount, bool allowEmpty, Func<TElementIn, int, TElementOut> map, Func<List<TElementOut>, TResult> collect, Func<TElementIn, int, string> deriveName)
		{
			EntityUtil.CheckArgumentNull<IEnumerable<TElementIn>>(argument, argumentName);
			bool flag = default(TElementIn) == null;
			bool flag2 = expectedElementCount != -1;
			Dictionary<string, int> dictionary = null;
			if (deriveName != null)
			{
				dictionary = new Dictionary<string, int>();
			}
			int num = 0;
			List<TElementOut> list = new List<TElementOut>();
			foreach (TElementIn telementIn in argument)
			{
				if (flag2 && num == expectedElementCount)
				{
					throw EntityUtil.Argument(Strings.Cqt_ExpressionList_IncorrectElementCount, argumentName);
				}
				if (flag && telementIn == null)
				{
					throw EntityUtil.ArgumentNull(StringUtil.FormatIndex(argumentName, num));
				}
				TElementOut item = map(telementIn, num);
				list.Add(item);
				if (deriveName != null)
				{
					string text = deriveName(telementIn, num);
					int num2 = -1;
					if (dictionary.TryGetValue(text, out num2))
					{
						throw EntityUtil.Argument(Strings.Cqt_Util_CheckListDuplicateName(num2, num, text), StringUtil.FormatIndex(argumentName, num));
					}
					dictionary[text] = num;
				}
				num++;
			}
			if (flag2)
			{
				if (num != expectedElementCount)
				{
					throw EntityUtil.Argument(Strings.Cqt_ExpressionList_IncorrectElementCount, argumentName);
				}
			}
			else if (num == 0 && !allowEmpty)
			{
				throw EntityUtil.Argument(Strings.Cqt_Util_CheckListEmptyInvalid, argumentName);
			}
			return collect(list);
		}

		// Token: 0x04001852 RID: 6226
		private readonly string argumentName;

		// Token: 0x04001853 RID: 6227
		private readonly IEnumerable<TElementIn> target;

		// Token: 0x04001854 RID: 6228
		private bool allowEmpty;

		// Token: 0x04001855 RID: 6229
		private int expectedElementCount = -1;

		// Token: 0x04001856 RID: 6230
		private Func<TElementIn, int, TElementOut> map;

		// Token: 0x04001857 RID: 6231
		private Func<List<TElementOut>, TResult> collect;

		// Token: 0x04001858 RID: 6232
		private Func<TElementIn, int, string> deriveName;
	}
}
