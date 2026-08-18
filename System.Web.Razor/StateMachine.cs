using System;

namespace System.Web.Razor
{
	// Token: 0x0200005A RID: 90
	public abstract class StateMachine<TReturn>
	{
		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x0600044A RID: 1098
		protected abstract StateMachine<TReturn>.State StartState { get; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x0600044B RID: 1099 RVA: 0x00011B34 File Offset: 0x0000FD34
		// (set) Token: 0x0600044C RID: 1100 RVA: 0x00011B3C File Offset: 0x0000FD3C
		protected StateMachine<TReturn>.State CurrentState { get; set; }

		// Token: 0x0600044D RID: 1101 RVA: 0x00011B48 File Offset: 0x0000FD48
		protected virtual TReturn Turn()
		{
			if (this.CurrentState == null)
			{
				return default(TReturn);
			}
			StateMachine<TReturn>.StateResult stateResult;
			do
			{
				stateResult = this.CurrentState();
				this.CurrentState = stateResult.Next;
			}
			while (stateResult != null && !stateResult.HasOutput);
			if (stateResult == null)
			{
				return default(TReturn);
			}
			return stateResult.Output;
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x00011B9D File Offset: 0x0000FD9D
		protected StateMachine<TReturn>.StateResult Stop()
		{
			return null;
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x00011BA0 File Offset: 0x0000FDA0
		protected StateMachine<TReturn>.StateResult Transition(StateMachine<TReturn>.State newState)
		{
			return new StateMachine<TReturn>.StateResult(newState);
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x00011BA8 File Offset: 0x0000FDA8
		protected StateMachine<TReturn>.StateResult Transition(TReturn output, StateMachine<TReturn>.State newState)
		{
			return new StateMachine<TReturn>.StateResult(output, newState);
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x00011BB1 File Offset: 0x0000FDB1
		protected StateMachine<TReturn>.StateResult Stay()
		{
			return new StateMachine<TReturn>.StateResult(this.CurrentState);
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x00011BBE File Offset: 0x0000FDBE
		protected StateMachine<TReturn>.StateResult Stay(TReturn output)
		{
			return new StateMachine<TReturn>.StateResult(output, this.CurrentState);
		}

		// Token: 0x0200005B RID: 91
		// (Invoke) Token: 0x06000455 RID: 1109
		protected delegate StateMachine<TReturn>.StateResult State();

		// Token: 0x0200005C RID: 92
		protected class StateResult
		{
			// Token: 0x06000458 RID: 1112 RVA: 0x00011BD4 File Offset: 0x0000FDD4
			public StateResult(StateMachine<TReturn>.State next)
			{
				this.HasOutput = false;
				this.Next = next;
			}

			// Token: 0x06000459 RID: 1113 RVA: 0x00011BEA File Offset: 0x0000FDEA
			public StateResult(TReturn output, StateMachine<TReturn>.State next)
			{
				this.HasOutput = true;
				this.Output = output;
				this.Next = next;
			}

			// Token: 0x170000A6 RID: 166
			// (get) Token: 0x0600045A RID: 1114 RVA: 0x00011C07 File Offset: 0x0000FE07
			// (set) Token: 0x0600045B RID: 1115 RVA: 0x00011C0F File Offset: 0x0000FE0F
			public bool HasOutput { get; set; }

			// Token: 0x170000A7 RID: 167
			// (get) Token: 0x0600045C RID: 1116 RVA: 0x00011C18 File Offset: 0x0000FE18
			// (set) Token: 0x0600045D RID: 1117 RVA: 0x00011C20 File Offset: 0x0000FE20
			public TReturn Output { get; set; }

			// Token: 0x170000A8 RID: 168
			// (get) Token: 0x0600045E RID: 1118 RVA: 0x00011C29 File Offset: 0x0000FE29
			// (set) Token: 0x0600045F RID: 1119 RVA: 0x00011C31 File Offset: 0x0000FE31
			public StateMachine<TReturn>.State Next { get; set; }
		}
	}
}
