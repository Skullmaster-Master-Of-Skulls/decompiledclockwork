using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Dynamic.Utils;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000140 RID: 320
	[EditorBrowsable(EditorBrowsableState.Never)]
	[DebuggerStepThrough]
	[__DynamicallyInvokable]
	public class RuleCache<T> where T : class
	{
		// Token: 0x06000A5E RID: 2654 RVA: 0x000259A7 File Offset: 0x00023BA7
		internal RuleCache()
		{
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x000259C6 File Offset: 0x00023BC6
		internal T[] GetRules()
		{
			return this._rules;
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x000259D0 File Offset: 0x00023BD0
		internal void MoveRule(T rule, int i)
		{
			object obj = this.cacheLock;
			lock (obj)
			{
				int num = this._rules.Length - i;
				if (num > 8)
				{
					num = 8;
				}
				int num2 = -1;
				int num3 = Math.Min(this._rules.Length, i + num);
				for (int j = i; j < num3; j++)
				{
					if (this._rules[j] == rule)
					{
						num2 = j;
						break;
					}
				}
				if (num2 >= 0)
				{
					T t = this._rules[num2];
					this._rules[num2] = this._rules[num2 - 1];
					this._rules[num2 - 1] = this._rules[num2 - 2];
					this._rules[num2 - 2] = t;
				}
			}
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x00025ABC File Offset: 0x00023CBC
		internal void AddRule(T newRule)
		{
			object obj = this.cacheLock;
			lock (obj)
			{
				this._rules = RuleCache<T>.AddOrInsert(this._rules, newRule);
			}
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x00025B08 File Offset: 0x00023D08
		internal void ReplaceRule(T oldRule, T newRule)
		{
			object obj = this.cacheLock;
			lock (obj)
			{
				int num = Array.IndexOf<T>(this._rules, oldRule);
				if (num >= 0)
				{
					this._rules[num] = newRule;
				}
				else
				{
					this._rules = RuleCache<T>.AddOrInsert(this._rules, newRule);
				}
			}
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x00025B74 File Offset: 0x00023D74
		private static T[] AddOrInsert(T[] rules, T item)
		{
			if (rules.Length < 64)
			{
				return rules.AddLast(item);
			}
			int num = rules.Length + 1;
			T[] array;
			if (num > 128)
			{
				num = 128;
				array = rules;
			}
			else
			{
				array = new T[num];
			}
			Array.Copy(rules, 0, array, 0, 64);
			array[64] = item;
			Array.Copy(rules, 64, array, 65, num - 64 - 1);
			return array;
		}

		// Token: 0x0400076D RID: 1901
		private T[] _rules = new T[0];

		// Token: 0x0400076E RID: 1902
		private readonly object cacheLock = new object();

		// Token: 0x0400076F RID: 1903
		private const int MaxRules = 128;

		// Token: 0x04000770 RID: 1904
		private const int InsertPosition = 64;
	}
}
