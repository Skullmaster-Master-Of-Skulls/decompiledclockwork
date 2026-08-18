using System;
using System.Runtime.CompilerServices;

namespace System.Dynamic
{
	// Token: 0x020000D1 RID: 209
	internal static class UpdateDelegates
	{
		// Token: 0x06000630 RID: 1584 RVA: 0x00012768 File Offset: 0x00010968
		[Obsolete("pregenerated CallSite<T>.Update delegate", true)]
		internal static TRet UpdateAndExecute0<TRet>(CallSite site)
		{
			CallSite<Func<CallSite, TRet>> callSite = (CallSite<Func<CallSite, TRet>>)site;
			Func<CallSite, TRet> target = callSite.Target;
			site = CallSiteOps.CreateMatchmaker<Func<CallSite, TRet>>(callSite);
			Func<CallSite, TRet>[] rules;
			Func<CallSite, TRet> func;
			if ((rules = CallSiteOps.GetRules<Func<CallSite, TRet>>(callSite)) != null)
			{
				for (int i = 0; i < rules.Length; i++)
				{
					func = rules[i];
					if (func != target)
					{
						callSite.Target = func;
						TRet result = func(site);
						if (CallSiteOps.GetMatch(site))
						{
							CallSiteOps.UpdateRules<Func<CallSite, TRet>>(callSite, i);
							return result;
						}
						CallSiteOps.ClearMatch(site);
					}
				}
			}
			RuleCache<Func<CallSite, TRet>> ruleCache = CallSiteOps.GetRuleCache<Func<CallSite, TRet>>(callSite);
			rules = ruleCache.GetRules();
			for (int j = 0; j < rules.Length; j++)
			{
				func = rules[j];
				callSite.Target = func;
				try
				{
					TRet result = func(site);
					if (CallSiteOps.GetMatch(site))
					{
						return result;
					}
				}
				finally
				{
					if (CallSiteOps.GetMatch(site))
					{
						CallSiteOps.AddRule<Func<CallSite, TRet>>(callSite, func);
						CallSiteOps.MoveRule<Func<CallSite, TRet>>(ruleCache, func, j);
					}
				}
				CallSiteOps.ClearMatch(site);
			}
			func = null;
			object[] args = new object[0];
			for (;;)
			{
				callSite.Target = target;
				func = (callSite.Target = callSite.Binder.BindCore<Func<CallSite, TRet>>(callSite, args));
				try
				{
					TRet result = func(site);
					if (CallSiteOps.GetMatch(site))
					{
						return result;
					}
				}
				finally
				{
					if (CallSiteOps.GetMatch(site))
					{
						CallSiteOps.AddRule<Func<CallSite, TRet>>(callSite, func);
					}
				}
				CallSiteOps.ClearMatch(site);
			}
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x000128C0 File Offset: 0x00010AC0
		[Obsolete("pregenerated CallSite<T>.Update delegate", true)]
		internal static TRet NoMatch0<TRet>(CallSite site)
		{
			site._match = false;
			return default(TRet);
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x000128E0 File Offset: 0x00010AE0
		[Obsolete("pregenerated CallSite<T>.Update delegate", true)]
		internal static TRet UpdateAndExecute1<T0, TRet>(CallSite site, T0 arg0)
		{
			CallSite<Func<CallSite, T0, TRet>> callSite = (CallSite<Func<CallSite, T0, TRet>>)site;
			Func<CallSite, T0, TRet> target = callSite.Target;
			site = CallSiteOps.CreateMatchmaker<Func<CallSite, T0, TRet>>(callSite);
			Func<CallSite, T0, TRet>[] rules;
			Func<CallSite, T0, TRet> func;
			if ((rules = CallSiteOps.GetRules<Func<CallSite, T0, TRet>>(callSite)) != null)
			{
				for (int i = 0; i < rules.Length; i++)
				{
					func = rules[i];
					if (func != target)
					{
						callSite.Target = func;
						TRet result = func(site, arg0);
						if (CallSiteOps.GetMatch(site))
						{
							CallSiteOps.UpdateRules<Func<CallSite, T0, TRet>>(callSite, i);
							return result;
						}
						CallSiteOps.ClearMatch(site);
					}
				}
			}
			RuleCache<Func<CallSite, T0, TRet>> ruleCache = CallSiteOps.GetRuleCache<Func<CallSite, T0, TRet>>(callSite);
			rules = ruleCache.GetRules();
			for (int j = 0; j < rules.Length; j++)
			{
				func = rules[j];
				callSite.Target = func;
				try
				{
					TRet result = func(site, arg0);
					if (CallSiteOps.GetMatch(site))
					{
						return result;
					}
				}
				finally
				{
					if (CallSiteOps.GetMatch(site))
					{
						CallSiteOps.AddRule<Func<CallSite, T0, TRet>>(callSite, func);
						CallSiteOps.MoveRule<Func<CallSite, T0, TRet>>(ruleCache, func, j);
					}
				}
				CallSiteOps.ClearMatch(site);
			}
			func = null;
			object[] args = new object[]
			{
				arg0
			};
			for (;;)
			{
				callSite.Target = target;
				func = (callSite.Target = callSite.Binder.BindCore<Func<CallSite, T0, TRet>>(callSite, args));
				try
				{
					TRet result = func(site, arg0);
					if (CallSiteOps.GetMatch(site))
					{
						return result;
					}
				}
				finally
				{
					if (CallSiteOps.GetMatch(site))
					{
						CallSiteOps.AddRule<Func<CallSite, T0, TRet>>(callSite, func);
					}
				}
				CallSiteOps.ClearMatch(site);
			}
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x00012A44 File Offset: 0x00010C44
		[Obsolete("pregenerated CallSite<T>.Update delegate", true)]
		internal static TRet NoMatch1<T0, TRet>(CallSite site, T0 arg0)
		{
			site._match = false;
			return default(TRet);
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x00012A64 File Offset: 0x00010C64
		[Obsolete("pregenerated CallSite<T>.Update delegate", true)]
		internal static TRet UpdateAndExecute2<T0, T1, TRet>(CallSite site, T0 arg0, T1 arg1)
		{
			CallSite<Func<CallSite, T0, T1, TRet>> callSite = (CallSite<Func<CallSite, T0, T1, TRet>>)site;
			Func<CallSite, T0, T1, TRet> target = callSite.Target;
			site = CallSiteOps.CreateMatchmaker<Func<CallSite, T0, T1, TRet>>(callSite);
			Func<CallSite, T0, T1, TRet>[] rules;
			Func<CallSite, T0, T1, TRet> func;
			if ((rules = CallSiteOps.GetRules<Func<CallSite, T0, T1, TRet>>(callSite)) != null)
			{
				for (int i = 0; i < rules.Length; i++)
				{
					func = rules[i];
					if (func != target)
					{
						callSite.Target = func;
						TRet result = func(site, arg0, arg1);
						if (CallSiteOps.GetMatch(site))
						{
							CallSiteOps.UpdateRules<Func<CallSite, T0, T1, TRet>>(callSite, i);
							return result;
						}
						CallSiteOps.ClearMatch(site);
					}
				}
			}
			RuleCache<Func<CallSite, T0, T1, TRet>> ruleCache = CallSiteOps.GetRuleCache<Func<CallSite, T0, T1, TRet>>(callSite);
			rules = ruleCache.GetRules();
			for (int j = 0; j < rules.Length; j++)
			{
				func = rules[j];
				callSite.Target = func;
				try
				{
					TRet result = func(site, arg0, arg1);
					if (CallSiteOps.GetMatch(site))
					{
						return result;
					}
				}
				finally
				{
					if (CallSiteOps.GetMatch(site))
					{
						CallSiteOps.AddRule<Func<CallSite, T0, T1, TRet>>(callSite, func);
						CallSiteOps.MoveRule<Func<CallSite, T0, T1, TRet>>(ruleCache, func, j);
					}
				}
				CallSiteOps.ClearMatch(site);
			}
			func = null;
			object[] args = new object[]
			{
				arg0,
				arg1
			};
			for (;;)
			{
				callSite.Target = target;
				func = (callSite.Target = callSite.Binder.BindCore<Func<CallSite, T0, T1, TRet>>(callSite, args));
				try
				{
					TRet result = func(site, arg0, arg1);
					if (CallSiteOps.GetMatch(site))
					{
						return result;
					}
				}
				finally
				{
					if (CallSiteOps.GetMatch(site))
					{
						CallSiteOps.AddRule<Func<CallSite, T0, T1, TRet>>(callSite, func);
					}
				}
				CallSiteOps.ClearMatch(site);
			}
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x00012BD4 File Offset: 0x00010DD4
		[Obsolete("pregenerated CallSite<T>.Update delegate", true)]
		internal static TRet NoMatch2<T0, T1, TRet>(CallSite site, T0 arg0, T1 arg1)
		{
			site._match = false;
			return default(TRet);
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x00012BF4 File Offset: 0x00010DF4
		[Obsolete("pregenerated CallSite<T>.Update delegate", true)]
		internal static TRet UpdateAndExecute3<T0, T1, T2, TRet>(CallSite site, T0 arg0, T1 arg1, T2 arg2)
		{
			CallSite<Func<CallSite, T0, T1, T2, TRet>> callSite = (CallSite<Func<CallSite, T0, T1, T2, TRet>>)site;
			Func<CallSite, T0, T1, T2, TRet> target = callSite.Target;
			site = CallSiteOps.CreateMatchmaker<Func<CallSite, T0, T1, T2, TRet>>(callSite);
			Func<CallSite, T0, T1, T2, TRet>[] rules;
			Func<CallSite, T0, T1, T2, TRet> func;
			if ((rules = CallSiteOps.GetRules<Func<CallSite, T0, T1, T2, TRet>>(callSite)) != null)
			{
				for (int i = 0; i < rules.Length; i++)
				{
					func = rules[i];
					if (func != target)
					{
						callSite.Target = func;
						TRet result = func(site, arg0, arg1, arg2);
						if (CallSiteOps.GetMatch(site))
						{
							CallSiteOps.UpdateRules<Func<CallSite, T0, T1, T2, TRet>>(callSite, i);
							return result;
						}
						CallSiteOps.ClearMatch(site);
					}
				}
			}
			RuleCache<Func<CallSite, T0, T1, T2, TRet>> ruleCache = CallSiteOps.GetRuleCache<Func<CallSite, T0, T1, T2, TRet>>(callSite);
			rules = ruleCache.GetRules();
			for (int j = 0; j < rules.Length; j++)
			{
				func = rules[j];
				callSite.Target = func;
				try
				{
					TRet result = func(site, arg0, arg1, arg2);
					if (CallSiteOps.GetMatch(site))
					{
						return result;
					}
				}
				finally
				{
					if (CallSiteOps.GetMatch(site))
					{
						CallSiteOps.AddRule<Func<CallSite, T0, T1, T2, TRet>>(callSite, func);
						CallSiteOps.MoveRule<Func<CallSite, T0, T1, T2, TRet>>(ruleCache, func, j);
					}
				}
				CallSiteOps.ClearMatch(site);
			}
			func = null;
			object[] args = new object[]
			{
				arg0,
				arg1,
				arg2
			};
			for (;;)
			{
				callSite.Target = target;
				func = (callSite.Target = callSite.Binder.BindCore<Func<CallSite, T0, T1, T2, TRet>>(callSite, args));
				try
				{
					TRet result = func(site, arg0, arg1, arg2);
					if (CallSiteOps.GetMatch(site))
					{
						return result;
					}
				}
				finally
				{
					if (CallSiteOps.GetMatch(site))
					{
						CallSiteOps.AddRule<Func<CallSite, T0, T1, T2, TRet>>(callSite, func);
					}
				}
				CallSiteOps.ClearMatch(site);
			}
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x00012D70 File Offset: 0x00010F70
		[Obsolete("pregenerated CallSite<T>.Update delegate", true)]
		internal static TRet NoMatch3<T0, T1, T2, TRet>(CallSite site, T0 arg0, T1 arg1, T2 arg2)
		{
			site._match = false;
			return default(TRet);
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x00012D90 File Offset: 0x00010F90
		[Obsolete("pregenerated CallSite<T>.Update delegate", true)]
		internal static TRet UpdateAndExecute4<T0, T1, T2, T3, TRet>(CallSite site, T0 arg0, T1 arg1, T2 arg2, T3 arg3)
		{
			CallSite<Func<CallSite, T0, T1, T2, T3, TRet>> callSite = (CallSite<Func<CallSite, T0, T1, T2, T3, TRet>>)site;
			Func<CallSite, T0, T1, T2, T3, TRet> target = callSite.Target;
			site = CallSiteOps.CreateMatchmaker<Func<CallSite, T0, T1, T2, T3, TRet>>(callSite);
			Func<CallSite, T0, T1, T2, T3, TRet>[] rules;
			Func<CallSite, T0, T1, T2, T3, TRet> func;
			if ((rules = CallSiteOps.GetRules<Func<CallSite, T0, T1, T2, T3, TRet>>(callSite)) != null)
			{
				for (int i = 0; i < rules.Length; i++)
				{
					func = rules[i];
					if (func != target)
					{
						callSite.Target = func;
						TRet result = func(site, arg0, arg1, arg2, arg3);
						if (CallSiteOps.GetMatch(site))
						{
							CallSiteOps.UpdateRules<Func<CallSite, T0, T1, T2, T3, TRet>>(callSite, i);
							return result;
						}
						CallSiteOps.ClearMatch(site);
					}
				}
			}
			RuleCache<Func<CallSite, T0, T1, T2, T3, TRet>> ruleCache = CallSiteOps.GetRuleCache<Func<CallSite, T0, T1, T2, T3, TRet>>(callSite);
			rules = ruleCache.GetRules();
			for (int j = 0; j < rules.Length; j++)
			{
				func = rules[j];
				callSite.Target = func;
				try
				{
					TRet result = func(site, arg0, arg1, arg2, arg3);
					if (CallSiteOps.GetMatch(site))
					{
						return result;
					}
				}
				finally
				{
					if (CallSiteOps.GetMatch(site))
					{
						CallSiteOps.AddRule<Func<CallSite, T0, T1, T2, T3, TRet>>(callSite, func);
						CallSiteOps.MoveRule<Func<CallSite, T0, T1, T2, T3, TRet>>(ruleCache, func, j);
					}
				}
				CallSiteOps.ClearMatch(site);
			}
			func = null;
			object[] args = new object[]
			{
				arg0,
				arg1,
				arg2,
				arg3
			};
			for (;;)
			{
				callSite.Target = target;
				func = (callSite.Target = callSite.Binder.BindCore<Func<CallSite, T0, T1, T2, T3, TRet>>(callSite, args));
				try
				{
					TRet result = func(site, arg0, arg1, arg2, arg3);
					if (CallSiteOps.GetMatch(site))
					{
						return result;
					}
				}
				finally
				{
					if (CallSiteOps.GetMatch(site))
					{
						CallSiteOps.AddRule<Func<CallSite, T0, T1, T2, T3, TRet>>(callSite, func);
					}
				}
				CallSiteOps.ClearMatch(site);
			}
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x00012F1C File Offset: 0x0001111C
		[Obsolete("pregenerated CallSite<T>.Update delegate", true)]
		internal static TRet NoMatch4<T0, T1, T2, T3, TRet>(CallSite site, T0 arg0, T1 arg1, T2 arg2, T3 arg3)
		{
			site._match = false;
			return default(TRet);
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x00012F3C File Offset: 0x0001113C
		[Obsolete("pregenerated CallSite<T>.Update delegate", true)]
		internal static TRet UpdateAndExecute5<T0, T1, T2, T3, T4, TRet>(CallSite site, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
		{
			CallSite<Func<CallSite, T0, T1, T2, T3, T4, TRet>> callSite = (CallSite<Func<CallSite, T0, T1, T2, T3, T4, TRet>>)site;
			Func<CallSite, T0, T1, T2, T3, T4, TRet> target = callSite.Target;
			site = CallSiteOps.CreateMatchmaker<Func<CallSite, T0, T1, T2, T3, T4, TRet>>(callSite);
			Func<CallSite, T0, T1, T2, T3, T4, TRet>[] rules;
			Func<CallSite, T0, T1, T2, T3, T4, TRet> func;
			if ((rules = CallSiteOps.GetRules<Func<CallSite, T0, T1, T2, T3, T4, TRet>>(callSite)) != null)
			{
				for (int i = 0; i < rules.Length; i++)
				{
					func = rules[i];
					if (func != target)
					{
						callSite.Target = func;
						TRet result = func(site, arg0, arg1, arg2, arg3, arg4);
						if (CallSiteOps.GetMatch(site))
						{
							CallSiteOps.UpdateRules<Func<CallSite, T0, T1, T2, T3, T4, TRet>>(callSite, i);
							return result;
						}
						CallSiteOps.ClearMatch(site);
					}
				}
			}
			RuleCache<Func<CallSite, T0, T1, T2, T3, T4, TRet>> ruleCache = CallSiteOps.GetRuleCache<Func<CallSite, T0, T1, T2, T3, T4, TRet>>(callSite);
			rules = ruleCache.GetRules();
			for (int j = 0; j < rules.Length; j++)
			{
				func = rules[j];
				callSite.Target = func;
				try
				{
					TRet result = func(site, arg0, arg1, arg2, arg3, arg4);
					if (CallSiteOps.GetMatch(site))
					{
						return result;
					}
				}
				finally
				{
					if (CallSiteOps.GetMatch(site))
					{
						CallSiteOps.AddRule<Func<CallSite, T0, T1, T2, T3, T4, TRet>>(callSite, func);
						CallSiteOps.MoveRule<Func<CallSite, T0, T1, T2, T3, T4, TRet>>(ruleCache, func, j);
					}
				}
				CallSiteOps.ClearMatch(site);
			}
			func = null;
			object[] args = new object[]
			{
				arg0,
				arg1,
				arg2,
				arg3,
				arg4
			};
			for (;;)
			{
				callSite.Target = target;
				func = (callSite.Target = callSite.Binder.BindCore<Func<CallSite, T0, T1, T2, T3, T4, TRet>>(callSite, args));
				try
				{
					TRet result = func(site, arg0, arg1, arg2, arg3, arg4);
					if (CallSiteOps.GetMatch(site))
					{
						return result;
					}
				}
				finally
				{
					if (CallSiteOps.GetMatch(site))
					{
						CallSiteOps.AddRule<Func<CallSite, T0, T1, T2, T3, T4, TRet>>(callSite, func);
					}
				}
				CallSiteOps.ClearMatch(site);
			}
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x000130D8 File Offset: 0x000112D8
		[Obsolete("pregenerated CallSite<T>.Update delegate", true)]
		internal static TRet NoMatch5<T0, T1, T2, T3, T4, TRet>(CallSite site, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
		{
			site._match = false;
			return default(TRet);
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x000130F8 File Offset: 0x000112F8
		[Obsolete("pregenerated CallSite<T>.Update delegate", true)]
		internal static TRet UpdateAndExecute6<T0, T1, T2, T3, T4, T5, TRet>(CallSite site, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
		{
			CallSite<Func<CallSite, T0, T1, T2, T3, T4, T5, TRet>> callSite = (CallSite<Func<CallSite, T0, T1, T2, T3, T4, T5, TRet>>)site;
			Func<CallSite, T0, T1, T2, T3, T4, T5, TRet> target = callSite.Target;
			site = CallSiteOps.CreateMatchmaker<Func<CallSite, T0, T1, T2, T3, T4, T5, TRet>>(callSite);
			Func<CallSite, T0, T1, T2, T3, T4, T5, TRet>[] rules;
			Func<CallSite, T0, T1, T2, T3, T4, T5, TRet> func;
			if ((rules = CallSiteOps.GetRules<Func<CallSite, T0, T1, T2, T3, T4, T5, TRet>>(callSite)) != null)
			{
				for (int i = 0; i < rules.Length; i++)
				{
					func = rules[i];
					if (func != target)
					{
						callSite.Target = func;
						TRet result = func(site, arg0, arg1, arg2, arg3, arg4, arg5);
						if (CallSiteOps.GetMatch(site))
						{
							CallSiteOps.UpdateRules<Func<CallSite, T0, T1, T2, T3, T4, T5, TRet>>(callSite, i);
							return result;
						}
						CallSiteOps.ClearMatch(site);
					}
				}
			}
			RuleCache<Func<CallSite, T0, T1, T2, T3, T4, T5, TRet>> ruleCache = CallSiteOps.GetRuleCache<Func<CallSite, T0, T1, T2, T3, T4, T5, TRet>>(callSite);
			rules = ruleCache.GetRules();
			for (int j = 0; j < rules.Length; j++)
			{
				func = rules[j];
				callSite.Target = func;
				try
				{
					TRet result = func(site, arg0, arg1, arg2, arg3, arg4, arg5);
					if (CallSiteOps.GetMatch(site))
					{
						return result;
					}
				}
				finally
				{
					if (CallSiteOps.GetMatch(site))
					{
						CallSiteOps.AddRule<Func<CallSite, T0, T1, T2, T3, T4, T5, TRet>>(callSite, func);
						CallSiteOps.MoveRule<Func<CallSite, T0, T1, T2, T3, T4, T5, TRet>>(ruleCache, func, j);
					}
				}
				CallSiteOps.ClearMatch(site);
			}
			func = null;
			object[] args = new object[]
			{
				arg0,
				arg1,
				arg2,
				arg3,
				arg4,
				arg5
			};
			for (;;)
			{
				callSite.Target = target;
				func = (callSite.Target = callSite.Binder.BindCore<Func<CallSite, T0, T1, T2, T3, T4, T5, TRet>>(callSite, args));
				try
				{
					TRet result = func(site, arg0, arg1, arg2, arg3, arg4, arg5);
					if (CallSiteOps.GetMatch(site))
					{
						return result;
					}
				}
				finally
				{
					if (CallSiteOps.GetMatch(site))
					{
						CallSiteOps.AddRule<Func<CallSite, T0, T1, T2, T3, T4, T5, TRet>>(callSite, func);
					}
				}
				CallSiteOps.ClearMatch(site);
			}
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x000132A4 File Offset: 0x000114A4
		[Obsolete("pregenerated CallSite<T>.Update delegate", true)]
		internal static TRet NoMatch6<T0, T1, T2, T3, T4, T5, TRet>(CallSite site, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
		{
			site._match = false;
			return default(TRet);
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x000132C4 File Offset: 0x000114C4
		[Obsolete("pregenerated CallSite<T>.Update delegate", true)]
		internal static TRet UpdateAndExecute7<T0, T1, T2, T3, T4, T5, T6, TRet>(CallSite site, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
		{
			CallSite<Func<CallSite, T0, T1, T2, T3, T4, T5, T6, TRet>> callSite = (CallSite<Func<CallSite, T0, T1, T2, T3, T4, T5, T6, TRet>>)site;
			Func<CallSite, T0, T1, T2, T3, T4, T5, T6, TRet> target = callSite.Target;
			site = CallSiteOps.CreateMatchmaker<Func<CallSite, T0, T1, T2, T3, T4, T5, T6, TRet>>(callSite);
			Func<CallSite, T0, T1, T2, T3, T4, T5, T6, TRet>[] rules;
			Func<CallSite, T0, T1, T2, T3, T4, T5, T6, TRet> func;
			if ((rules = CallSiteOps.GetRules<Func<CallSite, T0, T1, T2, T3, T4, T5, T6, TRet>>(callSite)) != null)
			{
				for (int i = 0; i < rules.Length; i++)
				{
					func = rules[i];
					if (func != target)
					{
						callSite.Target = func;
						TRet result = func(site, arg0, arg1, arg2, arg3, arg4, arg5, arg6);
						if (CallSiteOps.GetMatch(site))
						{
							CallSiteOps.UpdateRules<Func<CallSite, T0, T1, T2, T3, T4, T5, T6, TRet>>(callSite, i);
							return result;
						}
						CallSiteOps.ClearMatch(site);
					}
				}
			}
			RuleCache<Func<CallSite, T0, T1, T2, T3, T4, T5, T6, TRet>> ruleCache = CallSiteOps.GetRuleCache<Func<CallSite, T0, T1, T2, T3, T4, T5, T6, TRet>>(callSite);
			rules = ruleCache.GetRules();
			for (int j = 0; j < rules.Length; j++)
			{
				func = rules[j];
				callSite.Target = func;
				try
				{
					TRet result = func(site, arg0, arg1, arg2, arg3, arg4, arg5, arg6);
					if (CallSiteOps.GetMatch(site))
					{
						return result;
					}
				}
				finally
				{
					if (CallSiteOps.GetMatch(site))
					{
						CallSiteOps.AddRule<Func<CallSite, T0, T1, T2, T3, T4, T5, T6, TRet>>(callSite, func);
						CallSiteOps.MoveRule<Func<CallSite, T0, T1, T2, T3, T4, T5, T6, TRet>>(ruleCache, func, j);
					}
				}
				CallSiteOps.ClearMatch(site);
			}
			func = null;
			object[] args = new object[]
			{
				arg0,
				arg1,
				arg2,
				arg3,
				arg4,
				arg5,
				arg6
			};
			for (;;)
			{
				callSite.Target = target;
				func = (callSite.Target = callSite.Binder.BindCore<Func<CallSite, T0, T1, T2, T3, T4, T5, T6, TRet>>(callSite, args));
				try
				{
					TRet result = func(site, arg0, arg1, arg2, arg3, arg4, arg5, arg6);
					if (CallSiteOps.GetMatch(site))
					{
						return result;
					}
				}
				finally
				{
					if (CallSiteOps.GetMatch(site))
					{
						CallSiteOps.AddRule<Func<CallSite, T0, T1, T2, T3, T4, T5, T6, TRet>>(callSite, func);
					}
				}
				CallSiteOps.ClearMatch(site);
			}
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x00013480 File Offset: 0x00011680
		[Obsolete("pregenerated CallSite<T>.Update delegate", true)]
		internal static TRet NoMatch7<T0, T1, T2, T3, T4, T5, T6, TRet>(CallSite site, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
		{
			site._match = false;
			return default(TRet);
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x000134A0 File Offset: 0x000116A0
		[Obsolete("pregenerated CallSite<T>.Update delegate", true)]
		internal static TRet UpdateAndExecute8<T0, T1, T2, T3, T4, T5, T6, T7, TRet>(CallSite site, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
		{
			CallSite<Func<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, TRet>> callSite = (CallSite<Func<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, TRet>>)site;
			Func<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, TRet> target = callSite.Target;
			site = CallSiteOps.CreateMatchmaker<Func<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, TRet>>(callSite);
			Func<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, TRet>[] rules;
			Func<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, TRet> func;
			if ((rules = CallSiteOps.GetRules<Func<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, TRet>>(callSite)) != null)
			{
				for (int i = 0; i < rules.Length; i++)
				{
					func = rules[i];
					if (func != target)
					{
						callSite.Target = func;
						TRet result = func(site, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
						if (CallSiteOps.GetMatch(site))
						{
							CallSiteOps.UpdateRules<Func<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, TRet>>(callSite, i);
							return result;
						}
						CallSiteOps.ClearMatch(site);
					}
				}
			}
			RuleCache<Func<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, TRet>> ruleCache = CallSiteOps.GetRuleCache<Func<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, TRet>>(callSite);
			rules = ruleCache.GetRules();
			for (int j = 0; j < rules.Length; j++)
			{
				func = rules[j];
				callSite.Target = func;
				try
				{
					TRet result = func(site, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
					if (CallSiteOps.GetMatch(site))
					{
						return result;
					}
				}
				finally
				{
					if (CallSiteOps.GetMatch(site))
					{
						CallSiteOps.AddRule<Func<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, TRet>>(callSite, func);
						CallSiteOps.MoveRule<Func<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, TRet>>(ruleCache, func, j);
					}
				}
				CallSiteOps.ClearMatch(site);
			}
			func = null;
			object[] args = new object[]
			{
				arg0,
				arg1,
				arg2,
				arg3,
				arg4,
				arg5,
				arg6,
				arg7
			};
			for (;;)
			{
				callSite.Target = target;
				func = (callSite.Target = callSite.Binder.BindCore<Func<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, TRet>>(callSite, args));
				try
				{
					TRet result = func(site, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
					if (CallSiteOps.GetMatch(site))
					{
						return result;
					}
				}
				finally
				{
					if (CallSiteOps.GetMatch(site))
					{
						CallSiteOps.AddRule<Func<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, TRet>>(callSite, func);
					}
				}
				CallSiteOps.ClearMatch(site);
			}
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x0001366C File Offset: 0x0001186C
		[Obsolete("pregenerated CallSite<T>.Update delegate", true)]
		internal static TRet NoMatch8<T0, T1, T2, T3, T4, T5, T6, T7, TRet>(CallSite site, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
		{
			site._match = false;
			return default(TRet);
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x0001368C File Offset: 0x0001188C
		[Obsolete("pregenerated CallSite<T>.Update delegate", true)]
		internal static TRet UpdateAndExecute9<T0, T1, T2, T3, T4, T5, T6, T7, T8, TRet>(CallSite site, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
		{
			CallSite<Func<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8, TRet>> callSite = (CallSite<Func<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8, TRet>>)site;
			Func<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8, TRet> target = callSite.Target;
			site = CallSiteOps.CreateMatchmaker<Func<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8, TRet>>(callSite);
			Func<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8, TRet>[] rules;
			Func<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8, TRet> func;
			if ((rules = CallSiteOps.GetRules<Func<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8, TRet>>(callSite)) != null)
			{
				for (int i = 0; i < rules.Length; i++)
				{
					func = rules[i];
					if (func != target)
					{
						callSite.Target = func;
						TRet result = func(site, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
						if (CallSiteOps.GetMatch(site))
						{
							CallSiteOps.UpdateRules<Func<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8, TRet>>(callSite, i);
							return result;
						}
						CallSiteOps.ClearMatch(site);
					}
				}
			}
			RuleCache<Func<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8, TRet>> ruleCache = CallSiteOps.GetRuleCache<Func<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8, TRet>>(callSite);
			rules = ruleCache.GetRules();
			for (int j = 0; j < rules.Length; j++)
			{
				func = rules[j];
				callSite.Target = func;
				try
				{
					TRet result = func(site, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
					if (CallSiteOps.GetMatch(site))
					{
						return result;
					}
				}
				finally
				{
					if (CallSiteOps.GetMatch(site))
					{
						CallSiteOps.AddRule<Func<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8, TRet>>(callSite, func);
						CallSiteOps.MoveRule<Func<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8, TRet>>(ruleCache, func, j);
					}
				}
				CallSiteOps.ClearMatch(site);
			}
			func = null;
			object[] args = new object[]
			{
				arg0,
				arg1,
				arg2,
				arg3,
				arg4,
				arg5,
				arg6,
				arg7,
				arg8
			};
			for (;;)
			{
				callSite.Target = target;
				func = (callSite.Target = callSite.Binder.BindCore<Func<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8, TRet>>(callSite, args));
				try
				{
					TRet result = func(site, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
					if (CallSiteOps.GetMatch(site))
					{
						return result;
					}
				}
				finally
				{
					if (CallSiteOps.GetMatch(site))
					{
						CallSiteOps.AddRule<Func<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8, TRet>>(callSite, func);
					}
				}
				CallSiteOps.ClearMatch(site);
			}
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x00013868 File Offset: 0x00011A68
		[Obsolete("pregenerated CallSite<T>.Update delegate", true)]
		internal static TRet NoMatch9<T0, T1, T2, T3, T4, T5, T6, T7, T8, TRet>(CallSite site, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
		{
			site._match = false;
			return default(TRet);
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x00013888 File Offset: 0x00011A88
		[Obsolete("pregenerated CallSite<T>.Update delegate", true)]
		internal static TRet UpdateAndExecute10<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, TRet>(CallSite site, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
		{
			CallSite<Func<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, TRet>> callSite = (CallSite<Func<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, TRet>>)site;
			Func<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, TRet> target = callSite.Target;
			site = CallSiteOps.CreateMatchmaker<Func<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, TRet>>(callSite);
			Func<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, TRet>[] rules;
			Func<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, TRet> func;
			if ((rules = CallSiteOps.GetRules<Func<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, TRet>>(callSite)) != null)
			{
				for (int i = 0; i < rules.Length; i++)
				{
					func = rules[i];
					if (func != target)
					{
						callSite.Target = func;
						TRet result = func(site, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
						if (CallSiteOps.GetMatch(site))
						{
							CallSiteOps.UpdateRules<Func<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, TRet>>(callSite, i);
							return result;
						}
						CallSiteOps.ClearMatch(site);
					}
				}
			}
			RuleCache<Func<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, TRet>> ruleCache = CallSiteOps.GetRuleCache<Func<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, TRet>>(callSite);
			rules = ruleCache.GetRules();
			for (int j = 0; j < rules.Length; j++)
			{
				func = rules[j];
				callSite.Target = func;
				try
				{
					TRet result = func(site, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
					if (CallSiteOps.GetMatch(site))
					{
						return result;
					}
				}
				finally
				{
					if (CallSiteOps.GetMatch(site))
					{
						CallSiteOps.AddRule<Func<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, TRet>>(callSite, func);
						CallSiteOps.MoveRule<Func<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, TRet>>(ruleCache, func, j);
					}
				}
				CallSiteOps.ClearMatch(site);
			}
			func = null;
			object[] args = new object[]
			{
				arg0,
				arg1,
				arg2,
				arg3,
				arg4,
				arg5,
				arg6,
				arg7,
				arg8,
				arg9
			};
			for (;;)
			{
				callSite.Target = target;
				func = (callSite.Target = callSite.Binder.BindCore<Func<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, TRet>>(callSite, args));
				try
				{
					TRet result = func(site, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
					if (CallSiteOps.GetMatch(site))
					{
						return result;
					}
				}
				finally
				{
					if (CallSiteOps.GetMatch(site))
					{
						CallSiteOps.AddRule<Func<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, TRet>>(callSite, func);
					}
				}
				CallSiteOps.ClearMatch(site);
			}
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x00013A74 File Offset: 0x00011C74
		[Obsolete("pregenerated CallSite<T>.Update delegate", true)]
		internal static TRet NoMatch10<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, TRet>(CallSite site, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
		{
			site._match = false;
			return default(TRet);
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x00013A94 File Offset: 0x00011C94
		[Obsolete("pregenerated CallSite<T>.Update delegate", true)]
		internal static void UpdateAndExecuteVoid1<T0>(CallSite site, T0 arg0)
		{
			CallSite<Action<CallSite, T0>> callSite = (CallSite<Action<CallSite, T0>>)site;
			Action<CallSite, T0> target = callSite.Target;
			site = CallSiteOps.CreateMatchmaker<Action<CallSite, T0>>(callSite);
			Action<CallSite, T0>[] rules;
			Action<CallSite, T0> action;
			if ((rules = CallSiteOps.GetRules<Action<CallSite, T0>>(callSite)) != null)
			{
				for (int i = 0; i < rules.Length; i++)
				{
					action = rules[i];
					if (action != target)
					{
						callSite.Target = action;
						action(site, arg0);
						if (CallSiteOps.GetMatch(site))
						{
							CallSiteOps.UpdateRules<Action<CallSite, T0>>(callSite, i);
							return;
						}
						CallSiteOps.ClearMatch(site);
					}
				}
			}
			RuleCache<Action<CallSite, T0>> ruleCache = CallSiteOps.GetRuleCache<Action<CallSite, T0>>(callSite);
			rules = ruleCache.GetRules();
			for (int j = 0; j < rules.Length; j++)
			{
				action = rules[j];
				callSite.Target = action;
				try
				{
					action(site, arg0);
					if (CallSiteOps.GetMatch(site))
					{
						return;
					}
				}
				finally
				{
					if (CallSiteOps.GetMatch(site))
					{
						CallSiteOps.AddRule<Action<CallSite, T0>>(callSite, action);
						CallSiteOps.MoveRule<Action<CallSite, T0>>(ruleCache, action, j);
					}
				}
				CallSiteOps.ClearMatch(site);
			}
			action = null;
			object[] args = new object[]
			{
				arg0
			};
			for (;;)
			{
				callSite.Target = target;
				action = (callSite.Target = callSite.Binder.BindCore<Action<CallSite, T0>>(callSite, args));
				try
				{
					action(site, arg0);
					if (CallSiteOps.GetMatch(site))
					{
						return;
					}
				}
				finally
				{
					if (CallSiteOps.GetMatch(site))
					{
						CallSiteOps.AddRule<Action<CallSite, T0>>(callSite, action);
					}
				}
				CallSiteOps.ClearMatch(site);
			}
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x00013BE4 File Offset: 0x00011DE4
		[Obsolete("pregenerated CallSite<T>.Update delegate", true)]
		internal static void NoMatchVoid1<T0>(CallSite site, T0 arg0)
		{
			site._match = false;
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x00013BF0 File Offset: 0x00011DF0
		[Obsolete("pregenerated CallSite<T>.Update delegate", true)]
		internal static void UpdateAndExecuteVoid2<T0, T1>(CallSite site, T0 arg0, T1 arg1)
		{
			CallSite<Action<CallSite, T0, T1>> callSite = (CallSite<Action<CallSite, T0, T1>>)site;
			Action<CallSite, T0, T1> target = callSite.Target;
			site = CallSiteOps.CreateMatchmaker<Action<CallSite, T0, T1>>(callSite);
			Action<CallSite, T0, T1>[] rules;
			Action<CallSite, T0, T1> action;
			if ((rules = CallSiteOps.GetRules<Action<CallSite, T0, T1>>(callSite)) != null)
			{
				for (int i = 0; i < rules.Length; i++)
				{
					action = rules[i];
					if (action != target)
					{
						callSite.Target = action;
						action(site, arg0, arg1);
						if (CallSiteOps.GetMatch(site))
						{
							CallSiteOps.UpdateRules<Action<CallSite, T0, T1>>(callSite, i);
							return;
						}
						CallSiteOps.ClearMatch(site);
					}
				}
			}
			RuleCache<Action<CallSite, T0, T1>> ruleCache = CallSiteOps.GetRuleCache<Action<CallSite, T0, T1>>(callSite);
			rules = ruleCache.GetRules();
			for (int j = 0; j < rules.Length; j++)
			{
				action = rules[j];
				callSite.Target = action;
				try
				{
					action(site, arg0, arg1);
					if (CallSiteOps.GetMatch(site))
					{
						return;
					}
				}
				finally
				{
					if (CallSiteOps.GetMatch(site))
					{
						CallSiteOps.AddRule<Action<CallSite, T0, T1>>(callSite, action);
						CallSiteOps.MoveRule<Action<CallSite, T0, T1>>(ruleCache, action, j);
					}
				}
				CallSiteOps.ClearMatch(site);
			}
			action = null;
			object[] args = new object[]
			{
				arg0,
				arg1
			};
			for (;;)
			{
				callSite.Target = target;
				action = (callSite.Target = callSite.Binder.BindCore<Action<CallSite, T0, T1>>(callSite, args));
				try
				{
					action(site, arg0, arg1);
					if (CallSiteOps.GetMatch(site))
					{
						return;
					}
				}
				finally
				{
					if (CallSiteOps.GetMatch(site))
					{
						CallSiteOps.AddRule<Action<CallSite, T0, T1>>(callSite, action);
					}
				}
				CallSiteOps.ClearMatch(site);
			}
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x00013D4C File Offset: 0x00011F4C
		[Obsolete("pregenerated CallSite<T>.Update delegate", true)]
		internal static void NoMatchVoid2<T0, T1>(CallSite site, T0 arg0, T1 arg1)
		{
			site._match = false;
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x00013D58 File Offset: 0x00011F58
		[Obsolete("pregenerated CallSite<T>.Update delegate", true)]
		internal static void UpdateAndExecuteVoid3<T0, T1, T2>(CallSite site, T0 arg0, T1 arg1, T2 arg2)
		{
			CallSite<Action<CallSite, T0, T1, T2>> callSite = (CallSite<Action<CallSite, T0, T1, T2>>)site;
			Action<CallSite, T0, T1, T2> target = callSite.Target;
			site = CallSiteOps.CreateMatchmaker<Action<CallSite, T0, T1, T2>>(callSite);
			Action<CallSite, T0, T1, T2>[] rules;
			Action<CallSite, T0, T1, T2> action;
			if ((rules = CallSiteOps.GetRules<Action<CallSite, T0, T1, T2>>(callSite)) != null)
			{
				for (int i = 0; i < rules.Length; i++)
				{
					action = rules[i];
					if (action != target)
					{
						callSite.Target = action;
						action(site, arg0, arg1, arg2);
						if (CallSiteOps.GetMatch(site))
						{
							CallSiteOps.UpdateRules<Action<CallSite, T0, T1, T2>>(callSite, i);
							return;
						}
						CallSiteOps.ClearMatch(site);
					}
				}
			}
			RuleCache<Action<CallSite, T0, T1, T2>> ruleCache = CallSiteOps.GetRuleCache<Action<CallSite, T0, T1, T2>>(callSite);
			rules = ruleCache.GetRules();
			for (int j = 0; j < rules.Length; j++)
			{
				action = rules[j];
				callSite.Target = action;
				try
				{
					action(site, arg0, arg1, arg2);
					if (CallSiteOps.GetMatch(site))
					{
						return;
					}
				}
				finally
				{
					if (CallSiteOps.GetMatch(site))
					{
						CallSiteOps.AddRule<Action<CallSite, T0, T1, T2>>(callSite, action);
						CallSiteOps.MoveRule<Action<CallSite, T0, T1, T2>>(ruleCache, action, j);
					}
				}
				CallSiteOps.ClearMatch(site);
			}
			action = null;
			object[] args = new object[]
			{
				arg0,
				arg1,
				arg2
			};
			for (;;)
			{
				callSite.Target = target;
				action = (callSite.Target = callSite.Binder.BindCore<Action<CallSite, T0, T1, T2>>(callSite, args));
				try
				{
					action(site, arg0, arg1, arg2);
					if (CallSiteOps.GetMatch(site))
					{
						return;
					}
				}
				finally
				{
					if (CallSiteOps.GetMatch(site))
					{
						CallSiteOps.AddRule<Action<CallSite, T0, T1, T2>>(callSite, action);
					}
				}
				CallSiteOps.ClearMatch(site);
			}
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x00013EC0 File Offset: 0x000120C0
		[Obsolete("pregenerated CallSite<T>.Update delegate", true)]
		internal static void NoMatchVoid3<T0, T1, T2>(CallSite site, T0 arg0, T1 arg1, T2 arg2)
		{
			site._match = false;
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x00013ECC File Offset: 0x000120CC
		[Obsolete("pregenerated CallSite<T>.Update delegate", true)]
		internal static void UpdateAndExecuteVoid4<T0, T1, T2, T3>(CallSite site, T0 arg0, T1 arg1, T2 arg2, T3 arg3)
		{
			CallSite<Action<CallSite, T0, T1, T2, T3>> callSite = (CallSite<Action<CallSite, T0, T1, T2, T3>>)site;
			Action<CallSite, T0, T1, T2, T3> target = callSite.Target;
			site = CallSiteOps.CreateMatchmaker<Action<CallSite, T0, T1, T2, T3>>(callSite);
			Action<CallSite, T0, T1, T2, T3>[] rules;
			Action<CallSite, T0, T1, T2, T3> action;
			if ((rules = CallSiteOps.GetRules<Action<CallSite, T0, T1, T2, T3>>(callSite)) != null)
			{
				for (int i = 0; i < rules.Length; i++)
				{
					action = rules[i];
					if (action != target)
					{
						callSite.Target = action;
						action(site, arg0, arg1, arg2, arg3);
						if (CallSiteOps.GetMatch(site))
						{
							CallSiteOps.UpdateRules<Action<CallSite, T0, T1, T2, T3>>(callSite, i);
							return;
						}
						CallSiteOps.ClearMatch(site);
					}
				}
			}
			RuleCache<Action<CallSite, T0, T1, T2, T3>> ruleCache = CallSiteOps.GetRuleCache<Action<CallSite, T0, T1, T2, T3>>(callSite);
			rules = ruleCache.GetRules();
			for (int j = 0; j < rules.Length; j++)
			{
				action = rules[j];
				callSite.Target = action;
				try
				{
					action(site, arg0, arg1, arg2, arg3);
					if (CallSiteOps.GetMatch(site))
					{
						return;
					}
				}
				finally
				{
					if (CallSiteOps.GetMatch(site))
					{
						CallSiteOps.AddRule<Action<CallSite, T0, T1, T2, T3>>(callSite, action);
						CallSiteOps.MoveRule<Action<CallSite, T0, T1, T2, T3>>(ruleCache, action, j);
					}
				}
				CallSiteOps.ClearMatch(site);
			}
			action = null;
			object[] args = new object[]
			{
				arg0,
				arg1,
				arg2,
				arg3
			};
			for (;;)
			{
				callSite.Target = target;
				action = (callSite.Target = callSite.Binder.BindCore<Action<CallSite, T0, T1, T2, T3>>(callSite, args));
				try
				{
					action(site, arg0, arg1, arg2, arg3);
					if (CallSiteOps.GetMatch(site))
					{
						return;
					}
				}
				finally
				{
					if (CallSiteOps.GetMatch(site))
					{
						CallSiteOps.AddRule<Action<CallSite, T0, T1, T2, T3>>(callSite, action);
					}
				}
				CallSiteOps.ClearMatch(site);
			}
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x00014044 File Offset: 0x00012244
		[Obsolete("pregenerated CallSite<T>.Update delegate", true)]
		internal static void NoMatchVoid4<T0, T1, T2, T3>(CallSite site, T0 arg0, T1 arg1, T2 arg2, T3 arg3)
		{
			site._match = false;
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x00014050 File Offset: 0x00012250
		[Obsolete("pregenerated CallSite<T>.Update delegate", true)]
		internal static void UpdateAndExecuteVoid5<T0, T1, T2, T3, T4>(CallSite site, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
		{
			CallSite<Action<CallSite, T0, T1, T2, T3, T4>> callSite = (CallSite<Action<CallSite, T0, T1, T2, T3, T4>>)site;
			Action<CallSite, T0, T1, T2, T3, T4> target = callSite.Target;
			site = CallSiteOps.CreateMatchmaker<Action<CallSite, T0, T1, T2, T3, T4>>(callSite);
			Action<CallSite, T0, T1, T2, T3, T4>[] rules;
			Action<CallSite, T0, T1, T2, T3, T4> action;
			if ((rules = CallSiteOps.GetRules<Action<CallSite, T0, T1, T2, T3, T4>>(callSite)) != null)
			{
				for (int i = 0; i < rules.Length; i++)
				{
					action = rules[i];
					if (action != target)
					{
						callSite.Target = action;
						action(site, arg0, arg1, arg2, arg3, arg4);
						if (CallSiteOps.GetMatch(site))
						{
							CallSiteOps.UpdateRules<Action<CallSite, T0, T1, T2, T3, T4>>(callSite, i);
							return;
						}
						CallSiteOps.ClearMatch(site);
					}
				}
			}
			RuleCache<Action<CallSite, T0, T1, T2, T3, T4>> ruleCache = CallSiteOps.GetRuleCache<Action<CallSite, T0, T1, T2, T3, T4>>(callSite);
			rules = ruleCache.GetRules();
			for (int j = 0; j < rules.Length; j++)
			{
				action = rules[j];
				callSite.Target = action;
				try
				{
					action(site, arg0, arg1, arg2, arg3, arg4);
					if (CallSiteOps.GetMatch(site))
					{
						return;
					}
				}
				finally
				{
					if (CallSiteOps.GetMatch(site))
					{
						CallSiteOps.AddRule<Action<CallSite, T0, T1, T2, T3, T4>>(callSite, action);
						CallSiteOps.MoveRule<Action<CallSite, T0, T1, T2, T3, T4>>(ruleCache, action, j);
					}
				}
				CallSiteOps.ClearMatch(site);
			}
			action = null;
			object[] args = new object[]
			{
				arg0,
				arg1,
				arg2,
				arg3,
				arg4
			};
			for (;;)
			{
				callSite.Target = target;
				action = (callSite.Target = callSite.Binder.BindCore<Action<CallSite, T0, T1, T2, T3, T4>>(callSite, args));
				try
				{
					action(site, arg0, arg1, arg2, arg3, arg4);
					if (CallSiteOps.GetMatch(site))
					{
						return;
					}
				}
				finally
				{
					if (CallSiteOps.GetMatch(site))
					{
						CallSiteOps.AddRule<Action<CallSite, T0, T1, T2, T3, T4>>(callSite, action);
					}
				}
				CallSiteOps.ClearMatch(site);
			}
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x000141D8 File Offset: 0x000123D8
		[Obsolete("pregenerated CallSite<T>.Update delegate", true)]
		internal static void NoMatchVoid5<T0, T1, T2, T3, T4>(CallSite site, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
		{
			site._match = false;
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x000141E4 File Offset: 0x000123E4
		[Obsolete("pregenerated CallSite<T>.Update delegate", true)]
		internal static void UpdateAndExecuteVoid6<T0, T1, T2, T3, T4, T5>(CallSite site, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
		{
			CallSite<Action<CallSite, T0, T1, T2, T3, T4, T5>> callSite = (CallSite<Action<CallSite, T0, T1, T2, T3, T4, T5>>)site;
			Action<CallSite, T0, T1, T2, T3, T4, T5> target = callSite.Target;
			site = CallSiteOps.CreateMatchmaker<Action<CallSite, T0, T1, T2, T3, T4, T5>>(callSite);
			Action<CallSite, T0, T1, T2, T3, T4, T5>[] rules;
			Action<CallSite, T0, T1, T2, T3, T4, T5> action;
			if ((rules = CallSiteOps.GetRules<Action<CallSite, T0, T1, T2, T3, T4, T5>>(callSite)) != null)
			{
				for (int i = 0; i < rules.Length; i++)
				{
					action = rules[i];
					if (action != target)
					{
						callSite.Target = action;
						action(site, arg0, arg1, arg2, arg3, arg4, arg5);
						if (CallSiteOps.GetMatch(site))
						{
							CallSiteOps.UpdateRules<Action<CallSite, T0, T1, T2, T3, T4, T5>>(callSite, i);
							return;
						}
						CallSiteOps.ClearMatch(site);
					}
				}
			}
			RuleCache<Action<CallSite, T0, T1, T2, T3, T4, T5>> ruleCache = CallSiteOps.GetRuleCache<Action<CallSite, T0, T1, T2, T3, T4, T5>>(callSite);
			rules = ruleCache.GetRules();
			for (int j = 0; j < rules.Length; j++)
			{
				action = rules[j];
				callSite.Target = action;
				try
				{
					action(site, arg0, arg1, arg2, arg3, arg4, arg5);
					if (CallSiteOps.GetMatch(site))
					{
						return;
					}
				}
				finally
				{
					if (CallSiteOps.GetMatch(site))
					{
						CallSiteOps.AddRule<Action<CallSite, T0, T1, T2, T3, T4, T5>>(callSite, action);
						CallSiteOps.MoveRule<Action<CallSite, T0, T1, T2, T3, T4, T5>>(ruleCache, action, j);
					}
				}
				CallSiteOps.ClearMatch(site);
			}
			action = null;
			object[] args = new object[]
			{
				arg0,
				arg1,
				arg2,
				arg3,
				arg4,
				arg5
			};
			for (;;)
			{
				callSite.Target = target;
				action = (callSite.Target = callSite.Binder.BindCore<Action<CallSite, T0, T1, T2, T3, T4, T5>>(callSite, args));
				try
				{
					action(site, arg0, arg1, arg2, arg3, arg4, arg5);
					if (CallSiteOps.GetMatch(site))
					{
						return;
					}
				}
				finally
				{
					if (CallSiteOps.GetMatch(site))
					{
						CallSiteOps.AddRule<Action<CallSite, T0, T1, T2, T3, T4, T5>>(callSite, action);
					}
				}
				CallSiteOps.ClearMatch(site);
			}
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x0001437C File Offset: 0x0001257C
		[Obsolete("pregenerated CallSite<T>.Update delegate", true)]
		internal static void NoMatchVoid6<T0, T1, T2, T3, T4, T5>(CallSite site, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
		{
			site._match = false;
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x00014388 File Offset: 0x00012588
		[Obsolete("pregenerated CallSite<T>.Update delegate", true)]
		internal static void UpdateAndExecuteVoid7<T0, T1, T2, T3, T4, T5, T6>(CallSite site, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
		{
			CallSite<Action<CallSite, T0, T1, T2, T3, T4, T5, T6>> callSite = (CallSite<Action<CallSite, T0, T1, T2, T3, T4, T5, T6>>)site;
			Action<CallSite, T0, T1, T2, T3, T4, T5, T6> target = callSite.Target;
			site = CallSiteOps.CreateMatchmaker<Action<CallSite, T0, T1, T2, T3, T4, T5, T6>>(callSite);
			Action<CallSite, T0, T1, T2, T3, T4, T5, T6>[] rules;
			Action<CallSite, T0, T1, T2, T3, T4, T5, T6> action;
			if ((rules = CallSiteOps.GetRules<Action<CallSite, T0, T1, T2, T3, T4, T5, T6>>(callSite)) != null)
			{
				for (int i = 0; i < rules.Length; i++)
				{
					action = rules[i];
					if (action != target)
					{
						callSite.Target = action;
						action(site, arg0, arg1, arg2, arg3, arg4, arg5, arg6);
						if (CallSiteOps.GetMatch(site))
						{
							CallSiteOps.UpdateRules<Action<CallSite, T0, T1, T2, T3, T4, T5, T6>>(callSite, i);
							return;
						}
						CallSiteOps.ClearMatch(site);
					}
				}
			}
			RuleCache<Action<CallSite, T0, T1, T2, T3, T4, T5, T6>> ruleCache = CallSiteOps.GetRuleCache<Action<CallSite, T0, T1, T2, T3, T4, T5, T6>>(callSite);
			rules = ruleCache.GetRules();
			for (int j = 0; j < rules.Length; j++)
			{
				action = rules[j];
				callSite.Target = action;
				try
				{
					action(site, arg0, arg1, arg2, arg3, arg4, arg5, arg6);
					if (CallSiteOps.GetMatch(site))
					{
						return;
					}
				}
				finally
				{
					if (CallSiteOps.GetMatch(site))
					{
						CallSiteOps.AddRule<Action<CallSite, T0, T1, T2, T3, T4, T5, T6>>(callSite, action);
						CallSiteOps.MoveRule<Action<CallSite, T0, T1, T2, T3, T4, T5, T6>>(ruleCache, action, j);
					}
				}
				CallSiteOps.ClearMatch(site);
			}
			action = null;
			object[] args = new object[]
			{
				arg0,
				arg1,
				arg2,
				arg3,
				arg4,
				arg5,
				arg6
			};
			for (;;)
			{
				callSite.Target = target;
				action = (callSite.Target = callSite.Binder.BindCore<Action<CallSite, T0, T1, T2, T3, T4, T5, T6>>(callSite, args));
				try
				{
					action(site, arg0, arg1, arg2, arg3, arg4, arg5, arg6);
					if (CallSiteOps.GetMatch(site))
					{
						return;
					}
				}
				finally
				{
					if (CallSiteOps.GetMatch(site))
					{
						CallSiteOps.AddRule<Action<CallSite, T0, T1, T2, T3, T4, T5, T6>>(callSite, action);
					}
				}
				CallSiteOps.ClearMatch(site);
			}
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x00014530 File Offset: 0x00012730
		[Obsolete("pregenerated CallSite<T>.Update delegate", true)]
		internal static void NoMatchVoid7<T0, T1, T2, T3, T4, T5, T6>(CallSite site, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
		{
			site._match = false;
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x0001453C File Offset: 0x0001273C
		[Obsolete("pregenerated CallSite<T>.Update delegate", true)]
		internal static void UpdateAndExecuteVoid8<T0, T1, T2, T3, T4, T5, T6, T7>(CallSite site, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
		{
			CallSite<Action<CallSite, T0, T1, T2, T3, T4, T5, T6, T7>> callSite = (CallSite<Action<CallSite, T0, T1, T2, T3, T4, T5, T6, T7>>)site;
			Action<CallSite, T0, T1, T2, T3, T4, T5, T6, T7> target = callSite.Target;
			site = CallSiteOps.CreateMatchmaker<Action<CallSite, T0, T1, T2, T3, T4, T5, T6, T7>>(callSite);
			Action<CallSite, T0, T1, T2, T3, T4, T5, T6, T7>[] rules;
			Action<CallSite, T0, T1, T2, T3, T4, T5, T6, T7> action;
			if ((rules = CallSiteOps.GetRules<Action<CallSite, T0, T1, T2, T3, T4, T5, T6, T7>>(callSite)) != null)
			{
				for (int i = 0; i < rules.Length; i++)
				{
					action = rules[i];
					if (action != target)
					{
						callSite.Target = action;
						action(site, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
						if (CallSiteOps.GetMatch(site))
						{
							CallSiteOps.UpdateRules<Action<CallSite, T0, T1, T2, T3, T4, T5, T6, T7>>(callSite, i);
							return;
						}
						CallSiteOps.ClearMatch(site);
					}
				}
			}
			RuleCache<Action<CallSite, T0, T1, T2, T3, T4, T5, T6, T7>> ruleCache = CallSiteOps.GetRuleCache<Action<CallSite, T0, T1, T2, T3, T4, T5, T6, T7>>(callSite);
			rules = ruleCache.GetRules();
			for (int j = 0; j < rules.Length; j++)
			{
				action = rules[j];
				callSite.Target = action;
				try
				{
					action(site, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
					if (CallSiteOps.GetMatch(site))
					{
						return;
					}
				}
				finally
				{
					if (CallSiteOps.GetMatch(site))
					{
						CallSiteOps.AddRule<Action<CallSite, T0, T1, T2, T3, T4, T5, T6, T7>>(callSite, action);
						CallSiteOps.MoveRule<Action<CallSite, T0, T1, T2, T3, T4, T5, T6, T7>>(ruleCache, action, j);
					}
				}
				CallSiteOps.ClearMatch(site);
			}
			action = null;
			object[] args = new object[]
			{
				arg0,
				arg1,
				arg2,
				arg3,
				arg4,
				arg5,
				arg6,
				arg7
			};
			for (;;)
			{
				callSite.Target = target;
				action = (callSite.Target = callSite.Binder.BindCore<Action<CallSite, T0, T1, T2, T3, T4, T5, T6, T7>>(callSite, args));
				try
				{
					action(site, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
					if (CallSiteOps.GetMatch(site))
					{
						return;
					}
				}
				finally
				{
					if (CallSiteOps.GetMatch(site))
					{
						CallSiteOps.AddRule<Action<CallSite, T0, T1, T2, T3, T4, T5, T6, T7>>(callSite, action);
					}
				}
				CallSiteOps.ClearMatch(site);
			}
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x000146F4 File Offset: 0x000128F4
		[Obsolete("pregenerated CallSite<T>.Update delegate", true)]
		internal static void NoMatchVoid8<T0, T1, T2, T3, T4, T5, T6, T7>(CallSite site, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
		{
			site._match = false;
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x00014700 File Offset: 0x00012900
		[Obsolete("pregenerated CallSite<T>.Update delegate", true)]
		internal static void UpdateAndExecuteVoid9<T0, T1, T2, T3, T4, T5, T6, T7, T8>(CallSite site, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
		{
			CallSite<Action<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8>> callSite = (CallSite<Action<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8>>)site;
			Action<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8> target = callSite.Target;
			site = CallSiteOps.CreateMatchmaker<Action<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8>>(callSite);
			Action<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8>[] rules;
			Action<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8> action;
			if ((rules = CallSiteOps.GetRules<Action<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8>>(callSite)) != null)
			{
				for (int i = 0; i < rules.Length; i++)
				{
					action = rules[i];
					if (action != target)
					{
						callSite.Target = action;
						action(site, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
						if (CallSiteOps.GetMatch(site))
						{
							CallSiteOps.UpdateRules<Action<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8>>(callSite, i);
							return;
						}
						CallSiteOps.ClearMatch(site);
					}
				}
			}
			RuleCache<Action<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8>> ruleCache = CallSiteOps.GetRuleCache<Action<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8>>(callSite);
			rules = ruleCache.GetRules();
			for (int j = 0; j < rules.Length; j++)
			{
				action = rules[j];
				callSite.Target = action;
				try
				{
					action(site, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
					if (CallSiteOps.GetMatch(site))
					{
						return;
					}
				}
				finally
				{
					if (CallSiteOps.GetMatch(site))
					{
						CallSiteOps.AddRule<Action<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8>>(callSite, action);
						CallSiteOps.MoveRule<Action<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8>>(ruleCache, action, j);
					}
				}
				CallSiteOps.ClearMatch(site);
			}
			action = null;
			object[] args = new object[]
			{
				arg0,
				arg1,
				arg2,
				arg3,
				arg4,
				arg5,
				arg6,
				arg7,
				arg8
			};
			for (;;)
			{
				callSite.Target = target;
				action = (callSite.Target = callSite.Binder.BindCore<Action<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8>>(callSite, args));
				try
				{
					action(site, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
					if (CallSiteOps.GetMatch(site))
					{
						return;
					}
				}
				finally
				{
					if (CallSiteOps.GetMatch(site))
					{
						CallSiteOps.AddRule<Action<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8>>(callSite, action);
					}
				}
				CallSiteOps.ClearMatch(site);
			}
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x000148C8 File Offset: 0x00012AC8
		[Obsolete("pregenerated CallSite<T>.Update delegate", true)]
		internal static void NoMatchVoid9<T0, T1, T2, T3, T4, T5, T6, T7, T8>(CallSite site, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
		{
			site._match = false;
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x000148D4 File Offset: 0x00012AD4
		[Obsolete("pregenerated CallSite<T>.Update delegate", true)]
		internal static void UpdateAndExecuteVoid10<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(CallSite site, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
		{
			CallSite<Action<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>> callSite = (CallSite<Action<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>>)site;
			Action<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> target = callSite.Target;
			site = CallSiteOps.CreateMatchmaker<Action<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>>(callSite);
			Action<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>[] rules;
			Action<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> action;
			if ((rules = CallSiteOps.GetRules<Action<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>>(callSite)) != null)
			{
				for (int i = 0; i < rules.Length; i++)
				{
					action = rules[i];
					if (action != target)
					{
						callSite.Target = action;
						action(site, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
						if (CallSiteOps.GetMatch(site))
						{
							CallSiteOps.UpdateRules<Action<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>>(callSite, i);
							return;
						}
						CallSiteOps.ClearMatch(site);
					}
				}
			}
			RuleCache<Action<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>> ruleCache = CallSiteOps.GetRuleCache<Action<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>>(callSite);
			rules = ruleCache.GetRules();
			for (int j = 0; j < rules.Length; j++)
			{
				action = rules[j];
				callSite.Target = action;
				try
				{
					action(site, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
					if (CallSiteOps.GetMatch(site))
					{
						return;
					}
				}
				finally
				{
					if (CallSiteOps.GetMatch(site))
					{
						CallSiteOps.AddRule<Action<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>>(callSite, action);
						CallSiteOps.MoveRule<Action<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>>(ruleCache, action, j);
					}
				}
				CallSiteOps.ClearMatch(site);
			}
			action = null;
			object[] args = new object[]
			{
				arg0,
				arg1,
				arg2,
				arg3,
				arg4,
				arg5,
				arg6,
				arg7,
				arg8,
				arg9
			};
			for (;;)
			{
				callSite.Target = target;
				action = (callSite.Target = callSite.Binder.BindCore<Action<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>>(callSite, args));
				try
				{
					action(site, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
					if (CallSiteOps.GetMatch(site))
					{
						return;
					}
				}
				finally
				{
					if (CallSiteOps.GetMatch(site))
					{
						CallSiteOps.AddRule<Action<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>>(callSite, action);
					}
				}
				CallSiteOps.ClearMatch(site);
			}
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x00014AB0 File Offset: 0x00012CB0
		[Obsolete("pregenerated CallSite<T>.Update delegate", true)]
		internal static void NoMatchVoid10<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(CallSite site, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
		{
			site._match = false;
		}
	}
}
