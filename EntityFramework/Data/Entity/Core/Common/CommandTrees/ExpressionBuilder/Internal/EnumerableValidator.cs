using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder.Internal
{
	// Token: 0x0200011F RID: 287
	internal sealed class EnumerableValidator<TElementIn, TElementOut, TResult>
	{
		// Token: 0x060008BB RID: 2235 RVA: 0x0002D564 File Offset: 0x0002B764
		internal EnumerableValidator(IEnumerable<TElementIn> argument, string argumentName)
		{
			this.argumentName = argumentName;
			this.target = argument;
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060008BC RID: 2236 RVA: 0x0002D581 File Offset: 0x0002B781
		// (set) Token: 0x060008BD RID: 2237 RVA: 0x0002D589 File Offset: 0x0002B789
		public bool AllowEmpty { get; set; }

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060008BE RID: 2238 RVA: 0x0002D592 File Offset: 0x0002B792
		// (set) Token: 0x060008BF RID: 2239 RVA: 0x0002D59A File Offset: 0x0002B79A
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

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060008C0 RID: 2240 RVA: 0x0002D5A3 File Offset: 0x0002B7A3
		// (set) Token: 0x060008C1 RID: 2241 RVA: 0x0002D5AB File Offset: 0x0002B7AB
		public Func<TElementIn, int, TElementOut> ConvertElement { get; set; }

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060008C2 RID: 2242 RVA: 0x0002D5B4 File Offset: 0x0002B7B4
		// (set) Token: 0x060008C3 RID: 2243 RVA: 0x0002D5BC File Offset: 0x0002B7BC
		public Func<List<TElementOut>, TResult> CreateResult { get; set; }

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060008C4 RID: 2244 RVA: 0x0002D5C5 File Offset: 0x0002B7C5
		// (set) Token: 0x060008C5 RID: 2245 RVA: 0x0002D5CD File Offset: 0x0002B7CD
		public Func<TElementIn, int, string> GetName { get; set; }

		// Token: 0x060008C6 RID: 2246 RVA: 0x0002D5D6 File Offset: 0x0002B7D6
		internal TResult Validate()
		{
			return EnumerableValidator<TElementIn, TElementOut, TResult>.Validate(this.target, this.argumentName, this.ExpectedElementCount, this.AllowEmpty, this.ConvertElement, this.CreateResult, this.GetName);
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x0002D608 File Offset: 0x0002B808
		private static TResult Validate(IEnumerable<TElementIn> argument, string argumentName, int expectedElementCount, bool allowEmpty, Func<TElementIn, int, TElementOut> map, Func<List<TElementOut>, TResult> collect, Func<TElementIn, int, string> deriveName)
		{
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
					throw new ArgumentException(Strings.Cqt_ExpressionList_IncorrectElementCount, argumentName);
				}
				if (flag && telementIn == null)
				{
					throw new ArgumentNullException(StringUtil.FormatIndex(argumentName, num));
				}
				TElementOut item = map(telementIn, num);
				list.Add(item);
				if (deriveName != null)
				{
					string text = deriveName(telementIn, num);
					int num2 = -1;
					if (dictionary.TryGetValue(text, out num2))
					{
						throw new ArgumentException(Strings.Cqt_Util_CheckListDuplicateName(num2, num, text), StringUtil.FormatIndex(argumentName, num));
					}
					dictionary[text] = num;
				}
				num++;
			}
			if (flag2)
			{
				if (num != expectedElementCount)
				{
					throw new ArgumentException(Strings.Cqt_ExpressionList_IncorrectElementCount, argumentName);
				}
			}
			else if (num == 0 && !allowEmpty)
			{
				throw new ArgumentException(Strings.Cqt_Util_CheckListEmptyInvalid, argumentName);
			}
			return collect(list);
		}

		// Token: 0x04000283 RID: 643
		private readonly string argumentName;

		// Token: 0x04000284 RID: 644
		private readonly IEnumerable<TElementIn> target;

		// Token: 0x04000285 RID: 645
		private int expectedElementCount = -1;
	}
}
