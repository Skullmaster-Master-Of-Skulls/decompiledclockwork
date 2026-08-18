using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Windows.Forms
{
	// Token: 0x020002B2 RID: 690
	internal sealed class KeyboardToolTipStateMachine
	{
		// Token: 0x170009E9 RID: 2537
		// (get) Token: 0x06002A5F RID: 10847 RVA: 0x000BFC0A File Offset: 0x000BDE0A
		public static KeyboardToolTipStateMachine Instance
		{
			get
			{
				if (KeyboardToolTipStateMachine.instance == null)
				{
					KeyboardToolTipStateMachine.instance = new KeyboardToolTipStateMachine();
				}
				return KeyboardToolTipStateMachine.instance;
			}
		}

		// Token: 0x06002A60 RID: 10848 RVA: 0x000BFC24 File Offset: 0x000BDE24
		private KeyboardToolTipStateMachine()
		{
			Dictionary<KeyboardToolTipStateMachine.SmTransition, Func<IKeyboardToolTip, ToolTip, KeyboardToolTipStateMachine.SmState>> dictionary = new Dictionary<KeyboardToolTipStateMachine.SmTransition, Func<IKeyboardToolTip, ToolTip, KeyboardToolTipStateMachine.SmState>>();
			KeyboardToolTipStateMachine.SmTransition key = new KeyboardToolTipStateMachine.SmTransition(KeyboardToolTipStateMachine.SmState.Hidden, KeyboardToolTipStateMachine.SmEvent.FocusedTool);
			dictionary[key] = new Func<IKeyboardToolTip, ToolTip, KeyboardToolTipStateMachine.SmState>(this.SetupInitShowTimer);
			KeyboardToolTipStateMachine.SmTransition key2 = new KeyboardToolTipStateMachine.SmTransition(KeyboardToolTipStateMachine.SmState.Hidden, KeyboardToolTipStateMachine.SmEvent.LeftTool);
			dictionary[key2] = new Func<IKeyboardToolTip, ToolTip, KeyboardToolTipStateMachine.SmState>(this.DoNothing);
			KeyboardToolTipStateMachine.SmTransition key3 = new KeyboardToolTipStateMachine.SmTransition(KeyboardToolTipStateMachine.SmState.ReadyForInitShow, KeyboardToolTipStateMachine.SmEvent.FocusedTool);
			dictionary[key3] = new Func<IKeyboardToolTip, ToolTip, KeyboardToolTipStateMachine.SmState>(this.DoNothing);
			KeyboardToolTipStateMachine.SmTransition key4 = new KeyboardToolTipStateMachine.SmTransition(KeyboardToolTipStateMachine.SmState.ReadyForInitShow, KeyboardToolTipStateMachine.SmEvent.LeftTool);
			dictionary[key4] = new Func<IKeyboardToolTip, ToolTip, KeyboardToolTipStateMachine.SmState>(this.ResetFsmToHidden);
			KeyboardToolTipStateMachine.SmTransition key5 = new KeyboardToolTipStateMachine.SmTransition(KeyboardToolTipStateMachine.SmState.ReadyForInitShow, KeyboardToolTipStateMachine.SmEvent.InitialDelayTimerExpired);
			dictionary[key5] = new Func<IKeyboardToolTip, ToolTip, KeyboardToolTipStateMachine.SmState>(this.ShowToolTip);
			KeyboardToolTipStateMachine.SmTransition key6 = new KeyboardToolTipStateMachine.SmTransition(KeyboardToolTipStateMachine.SmState.Shown, KeyboardToolTipStateMachine.SmEvent.FocusedTool);
			dictionary[key6] = new Func<IKeyboardToolTip, ToolTip, KeyboardToolTipStateMachine.SmState>(this.DoNothing);
			KeyboardToolTipStateMachine.SmTransition key7 = new KeyboardToolTipStateMachine.SmTransition(KeyboardToolTipStateMachine.SmState.Shown, KeyboardToolTipStateMachine.SmEvent.LeftTool);
			dictionary[key7] = new Func<IKeyboardToolTip, ToolTip, KeyboardToolTipStateMachine.SmState>(this.HideAndStartWaitingForRefocus);
			KeyboardToolTipStateMachine.SmTransition key8 = new KeyboardToolTipStateMachine.SmTransition(KeyboardToolTipStateMachine.SmState.Shown, KeyboardToolTipStateMachine.SmEvent.DismissTooltips);
			dictionary[key8] = new Func<IKeyboardToolTip, ToolTip, KeyboardToolTipStateMachine.SmState>(this.ResetFsmToHidden);
			KeyboardToolTipStateMachine.SmTransition key9 = new KeyboardToolTipStateMachine.SmTransition(KeyboardToolTipStateMachine.SmState.WaitForRefocus, KeyboardToolTipStateMachine.SmEvent.FocusedTool);
			dictionary[key9] = new Func<IKeyboardToolTip, ToolTip, KeyboardToolTipStateMachine.SmState>(this.SetupReshowTimer);
			KeyboardToolTipStateMachine.SmTransition key10 = new KeyboardToolTipStateMachine.SmTransition(KeyboardToolTipStateMachine.SmState.WaitForRefocus, KeyboardToolTipStateMachine.SmEvent.LeftTool);
			dictionary[key10] = new Func<IKeyboardToolTip, ToolTip, KeyboardToolTipStateMachine.SmState>(this.DoNothing);
			KeyboardToolTipStateMachine.SmTransition key11 = new KeyboardToolTipStateMachine.SmTransition(KeyboardToolTipStateMachine.SmState.WaitForRefocus, KeyboardToolTipStateMachine.SmEvent.RefocusWaitDelayExpired);
			dictionary[key11] = new Func<IKeyboardToolTip, ToolTip, KeyboardToolTipStateMachine.SmState>(this.ResetFsmToHidden);
			KeyboardToolTipStateMachine.SmTransition key12 = new KeyboardToolTipStateMachine.SmTransition(KeyboardToolTipStateMachine.SmState.ReadyForReshow, KeyboardToolTipStateMachine.SmEvent.FocusedTool);
			dictionary[key12] = new Func<IKeyboardToolTip, ToolTip, KeyboardToolTipStateMachine.SmState>(this.DoNothing);
			KeyboardToolTipStateMachine.SmTransition key13 = new KeyboardToolTipStateMachine.SmTransition(KeyboardToolTipStateMachine.SmState.ReadyForReshow, KeyboardToolTipStateMachine.SmEvent.LeftTool);
			dictionary[key13] = new Func<IKeyboardToolTip, ToolTip, KeyboardToolTipStateMachine.SmState>(this.StartWaitingForRefocus);
			KeyboardToolTipStateMachine.SmTransition key14 = new KeyboardToolTipStateMachine.SmTransition(KeyboardToolTipStateMachine.SmState.ReadyForReshow, KeyboardToolTipStateMachine.SmEvent.ReshowDelayTimerExpired);
			dictionary[key14] = new Func<IKeyboardToolTip, ToolTip, KeyboardToolTipStateMachine.SmState>(this.ShowToolTip);
			this.transitions = dictionary;
		}

		// Token: 0x06002A61 RID: 10849 RVA: 0x000BFDF6 File Offset: 0x000BDFF6
		public void ResetStateMachine(ToolTip toolTip)
		{
			this.Reset(toolTip);
		}

		// Token: 0x06002A62 RID: 10850 RVA: 0x000BFDFF File Offset: 0x000BDFFF
		public void Hook(IKeyboardToolTip tool, ToolTip toolTip)
		{
			if (tool.AllowsToolTip())
			{
				this.StartTracking(tool, toolTip);
				tool.OnHooked(toolTip);
			}
		}

		// Token: 0x06002A63 RID: 10851 RVA: 0x000BFE18 File Offset: 0x000BE018
		public void NotifyAboutMouseEnter(IKeyboardToolTip sender)
		{
			if (this.IsToolTracked(sender) && sender.ShowsOwnToolTip())
			{
				this.Reset(null);
			}
		}

		// Token: 0x06002A64 RID: 10852 RVA: 0x000BFE32 File Offset: 0x000BE032
		private bool IsToolTracked(IKeyboardToolTip sender)
		{
			return this.toolToTip[sender] != null;
		}

		// Token: 0x06002A65 RID: 10853 RVA: 0x000BFE43 File Offset: 0x000BE043
		public void NotifyAboutLostFocus(IKeyboardToolTip sender)
		{
			if (this.IsToolTracked(sender) && sender.ShowsOwnToolTip())
			{
				this.Transit(KeyboardToolTipStateMachine.SmEvent.LeftTool, sender);
				if (this.currentTool == null)
				{
					this.lastFocusedTool.SetTarget(null);
				}
			}
		}

		// Token: 0x06002A66 RID: 10854 RVA: 0x000BFE72 File Offset: 0x000BE072
		public void NotifyAboutGotFocus(IKeyboardToolTip sender)
		{
			if (this.IsToolTracked(sender) && sender.ShowsOwnToolTip() && sender.IsBeingTabbedTo())
			{
				this.Transit(KeyboardToolTipStateMachine.SmEvent.FocusedTool, sender);
				if (this.currentTool == sender)
				{
					this.lastFocusedTool.SetTarget(sender);
				}
			}
		}

		// Token: 0x06002A67 RID: 10855 RVA: 0x000BFEAA File Offset: 0x000BE0AA
		public void Unhook(IKeyboardToolTip tool, ToolTip toolTip)
		{
			if (tool.AllowsToolTip())
			{
				this.StopTracking(tool, toolTip);
				tool.OnUnhooked(toolTip);
			}
		}

		// Token: 0x06002A68 RID: 10856 RVA: 0x000BFEC3 File Offset: 0x000BE0C3
		public void NotifyAboutFormDeactivation(ToolTip sender)
		{
			this.OnFormDeactivation(sender);
		}

		// Token: 0x170009EA RID: 2538
		// (get) Token: 0x06002A69 RID: 10857 RVA: 0x000BFECC File Offset: 0x000BE0CC
		internal IKeyboardToolTip LastFocusedTool
		{
			get
			{
				IKeyboardToolTip result;
				if (this.lastFocusedTool.TryGetTarget(out result))
				{
					return result;
				}
				return Control.FromHandleInternal(UnsafeNativeMethods.GetFocus());
			}
		}

		// Token: 0x06002A6A RID: 10858 RVA: 0x000BFEF4 File Offset: 0x000BE0F4
		private KeyboardToolTipStateMachine.SmState HideAndStartWaitingForRefocus(IKeyboardToolTip tool, ToolTip toolTip)
		{
			toolTip.HideToolTip(this.currentTool);
			return this.StartWaitingForRefocus(tool, toolTip);
		}

		// Token: 0x06002A6B RID: 10859 RVA: 0x000BFF0C File Offset: 0x000BE10C
		private KeyboardToolTipStateMachine.SmState StartWaitingForRefocus(IKeyboardToolTip tool, ToolTip toolTip)
		{
			this.ResetTimer();
			this.currentTool = null;
			SendOrPostCallback expirationCallback = null;
			this.refocusDelayExpirationCallback = (expirationCallback = delegate(object toolObject)
			{
				if (this.currentState == KeyboardToolTipStateMachine.SmState.WaitForRefocus && this.refocusDelayExpirationCallback == expirationCallback)
				{
					this.Transit(KeyboardToolTipStateMachine.SmEvent.RefocusWaitDelayExpired, (IKeyboardToolTip)toolObject);
				}
			});
			SynchronizationContext.Current.Post(expirationCallback, tool);
			return KeyboardToolTipStateMachine.SmState.WaitForRefocus;
		}

		// Token: 0x06002A6C RID: 10860 RVA: 0x000BFF68 File Offset: 0x000BE168
		private KeyboardToolTipStateMachine.SmState SetupReshowTimer(IKeyboardToolTip tool, ToolTip toolTip)
		{
			this.currentTool = tool;
			this.ResetTimer();
			this.StartTimer(toolTip.GetDelayTime(1), this.GetOneRunTickHandler(delegate(Timer sender)
			{
				this.Transit(KeyboardToolTipStateMachine.SmEvent.ReshowDelayTimerExpired, tool);
			}));
			return KeyboardToolTipStateMachine.SmState.ReadyForReshow;
		}

		// Token: 0x06002A6D RID: 10861 RVA: 0x000BFFBC File Offset: 0x000BE1BC
		private KeyboardToolTipStateMachine.SmState ShowToolTip(IKeyboardToolTip tool, ToolTip toolTip)
		{
			string captionForTool = tool.GetCaptionForTool(toolTip);
			int num = toolTip.IsPersistent ? 0 : toolTip.GetDelayTime(2);
			if (!this.currentTool.IsHoveredWithMouse())
			{
				toolTip.ShowKeyboardToolTip(captionForTool, this.currentTool, num);
			}
			if (!toolTip.IsPersistent)
			{
				this.StartTimer(num, this.GetOneRunTickHandler(delegate(Timer sender)
				{
					this.Transit(KeyboardToolTipStateMachine.SmEvent.DismissTooltips, this.currentTool);
				}));
			}
			return KeyboardToolTipStateMachine.SmState.Shown;
		}

		// Token: 0x06002A6E RID: 10862 RVA: 0x000C0021 File Offset: 0x000BE221
		private KeyboardToolTipStateMachine.SmState ResetFsmToHidden(IKeyboardToolTip tool, ToolTip toolTip)
		{
			return this.FullFsmReset();
		}

		// Token: 0x06002A6F RID: 10863 RVA: 0x000C0029 File Offset: 0x000BE229
		private KeyboardToolTipStateMachine.SmState DoNothing(IKeyboardToolTip tool, ToolTip toolTip)
		{
			return this.currentState;
		}

		// Token: 0x06002A70 RID: 10864 RVA: 0x000C0031 File Offset: 0x000BE231
		private KeyboardToolTipStateMachine.SmState SetupInitShowTimer(IKeyboardToolTip tool, ToolTip toolTip)
		{
			this.currentTool = tool;
			this.ResetTimer();
			this.StartTimer(toolTip.GetDelayTime(3), this.GetOneRunTickHandler(delegate(Timer sender)
			{
				this.Transit(KeyboardToolTipStateMachine.SmEvent.InitialDelayTimerExpired, this.currentTool);
			}));
			return KeyboardToolTipStateMachine.SmState.ReadyForInitShow;
		}

		// Token: 0x06002A71 RID: 10865 RVA: 0x000C0060 File Offset: 0x000BE260
		private void StartTimer(int interval, EventHandler eventHandler)
		{
			this.timer.Interval = interval;
			this.timer.Tick += eventHandler;
			this.timer.Start();
		}

		// Token: 0x06002A72 RID: 10866 RVA: 0x000C0088 File Offset: 0x000BE288
		private EventHandler GetOneRunTickHandler(Action<Timer> handler)
		{
			EventHandler wrapper = null;
			wrapper = delegate(object sender, EventArgs eventArgs)
			{
				this.timer.Stop();
				this.timer.Tick -= wrapper;
				handler(this.timer);
			};
			return wrapper;
		}

		// Token: 0x06002A73 RID: 10867 RVA: 0x000C00C8 File Offset: 0x000BE2C8
		private void Transit(KeyboardToolTipStateMachine.SmEvent @event, IKeyboardToolTip source)
		{
			bool flag = false;
			try
			{
				ToolTip toolTip = this.toolToTip[source];
				if ((this.currentTool == null || this.currentTool.CanShowToolTipsNow()) && toolTip != null)
				{
					Func<IKeyboardToolTip, ToolTip, KeyboardToolTipStateMachine.SmState> func = this.transitions[new KeyboardToolTipStateMachine.SmTransition(this.currentState, @event)];
					this.currentState = func(source, toolTip);
				}
				else
				{
					flag = true;
				}
			}
			catch
			{
				flag = true;
				throw;
			}
			finally
			{
				if (flag)
				{
					this.FullFsmReset();
				}
			}
		}

		// Token: 0x06002A74 RID: 10868 RVA: 0x000C0154 File Offset: 0x000BE354
		internal static void HidePersistentTooltip()
		{
			KeyboardToolTipStateMachine keyboardToolTipStateMachine = KeyboardToolTipStateMachine.instance;
			if (keyboardToolTipStateMachine == null)
			{
				return;
			}
			keyboardToolTipStateMachine.HidePersistent();
		}

		// Token: 0x06002A75 RID: 10869 RVA: 0x000C0168 File Offset: 0x000BE368
		private void HidePersistent()
		{
			if (this.currentState != KeyboardToolTipStateMachine.SmState.Shown || this.currentTool == null)
			{
				return;
			}
			ToolTip toolTip = this.toolToTip[this.currentTool];
			if (toolTip != null && toolTip.IsPersistent)
			{
				toolTip.HideToolTip(this.currentTool);
				this.currentTool = null;
				this.currentState = KeyboardToolTipStateMachine.SmState.Hidden;
			}
		}

		// Token: 0x06002A76 RID: 10870 RVA: 0x000C01C0 File Offset: 0x000BE3C0
		private KeyboardToolTipStateMachine.SmState FullFsmReset()
		{
			if (this.currentState == KeyboardToolTipStateMachine.SmState.Shown && this.currentTool != null)
			{
				ToolTip toolTip = this.toolToTip[this.currentTool];
				if (toolTip != null)
				{
					toolTip.HideToolTip(this.currentTool);
				}
			}
			this.ResetTimer();
			this.currentTool = null;
			return this.currentState = KeyboardToolTipStateMachine.SmState.Hidden;
		}

		// Token: 0x06002A77 RID: 10871 RVA: 0x000C0216 File Offset: 0x000BE416
		private void ResetTimer()
		{
			this.timer.ClearTimerTickHandlers();
			this.timer.Stop();
		}

		// Token: 0x06002A78 RID: 10872 RVA: 0x000C022E File Offset: 0x000BE42E
		private void Reset(ToolTip toolTipToReset)
		{
			if (toolTipToReset == null || (this.currentTool != null && this.toolToTip[this.currentTool] == toolTipToReset))
			{
				this.FullFsmReset();
			}
		}

		// Token: 0x06002A79 RID: 10873 RVA: 0x000C0256 File Offset: 0x000BE456
		private void StartTracking(IKeyboardToolTip tool, ToolTip toolTip)
		{
			this.toolToTip[tool] = toolTip;
		}

		// Token: 0x06002A7A RID: 10874 RVA: 0x000C0265 File Offset: 0x000BE465
		private void StopTracking(IKeyboardToolTip tool, ToolTip toolTip)
		{
			this.toolToTip.Remove(tool, toolTip);
		}

		// Token: 0x06002A7B RID: 10875 RVA: 0x000C0274 File Offset: 0x000BE474
		private void OnFormDeactivation(ToolTip sender)
		{
			if (this.currentTool != null && this.toolToTip[this.currentTool] == sender)
			{
				this.FullFsmReset();
			}
		}

		// Token: 0x04001135 RID: 4405
		[ThreadStatic]
		private static KeyboardToolTipStateMachine instance;

		// Token: 0x04001136 RID: 4406
		private readonly Dictionary<KeyboardToolTipStateMachine.SmTransition, Func<IKeyboardToolTip, ToolTip, KeyboardToolTipStateMachine.SmState>> transitions;

		// Token: 0x04001137 RID: 4407
		private readonly KeyboardToolTipStateMachine.ToolToTipDictionary toolToTip = new KeyboardToolTipStateMachine.ToolToTipDictionary();

		// Token: 0x04001138 RID: 4408
		private KeyboardToolTipStateMachine.SmState currentState;

		// Token: 0x04001139 RID: 4409
		private IKeyboardToolTip currentTool;

		// Token: 0x0400113A RID: 4410
		private readonly KeyboardToolTipStateMachine.InternalStateMachineTimer timer = new KeyboardToolTipStateMachine.InternalStateMachineTimer();

		// Token: 0x0400113B RID: 4411
		private SendOrPostCallback refocusDelayExpirationCallback;

		// Token: 0x0400113C RID: 4412
		private readonly WeakReference<IKeyboardToolTip> lastFocusedTool = new WeakReference<IKeyboardToolTip>(null);

		// Token: 0x020006B1 RID: 1713
		private enum SmEvent : byte
		{
			// Token: 0x04003B06 RID: 15110
			FocusedTool,
			// Token: 0x04003B07 RID: 15111
			LeftTool,
			// Token: 0x04003B08 RID: 15112
			InitialDelayTimerExpired,
			// Token: 0x04003B09 RID: 15113
			ReshowDelayTimerExpired,
			// Token: 0x04003B0A RID: 15114
			DismissTooltips,
			// Token: 0x04003B0B RID: 15115
			RefocusWaitDelayExpired
		}

		// Token: 0x020006B2 RID: 1714
		internal enum SmState : byte
		{
			// Token: 0x04003B0D RID: 15117
			Hidden,
			// Token: 0x04003B0E RID: 15118
			ReadyForInitShow,
			// Token: 0x04003B0F RID: 15119
			Shown,
			// Token: 0x04003B10 RID: 15120
			ReadyForReshow,
			// Token: 0x04003B11 RID: 15121
			WaitForRefocus
		}

		// Token: 0x020006B3 RID: 1715
		private struct SmTransition : IEquatable<KeyboardToolTipStateMachine.SmTransition>
		{
			// Token: 0x060068CD RID: 26829 RVA: 0x00186086 File Offset: 0x00184286
			public SmTransition(KeyboardToolTipStateMachine.SmState currentState, KeyboardToolTipStateMachine.SmEvent @event)
			{
				this.currentState = currentState;
				this.@event = @event;
			}

			// Token: 0x060068CE RID: 26830 RVA: 0x00186096 File Offset: 0x00184296
			public bool Equals(KeyboardToolTipStateMachine.SmTransition other)
			{
				return this.currentState == other.currentState && this.@event == other.@event;
			}

			// Token: 0x060068CF RID: 26831 RVA: 0x001860B6 File Offset: 0x001842B6
			public override bool Equals(object obj)
			{
				return obj is KeyboardToolTipStateMachine.SmTransition && this.Equals((KeyboardToolTipStateMachine.SmTransition)obj);
			}

			// Token: 0x060068D0 RID: 26832 RVA: 0x001860CE File Offset: 0x001842CE
			public override int GetHashCode()
			{
				return (int)((int)this.currentState << 16 | (KeyboardToolTipStateMachine.SmState)this.@event);
			}

			// Token: 0x04003B12 RID: 15122
			private readonly KeyboardToolTipStateMachine.SmState currentState;

			// Token: 0x04003B13 RID: 15123
			private readonly KeyboardToolTipStateMachine.SmEvent @event;
		}

		// Token: 0x020006B4 RID: 1716
		private sealed class InternalStateMachineTimer : Timer
		{
			// Token: 0x060068D1 RID: 26833 RVA: 0x001860E0 File Offset: 0x001842E0
			public void ClearTimerTickHandlers()
			{
				this.onTimer = null;
			}
		}

		// Token: 0x020006B5 RID: 1717
		private sealed class ToolToTipDictionary
		{
			// Token: 0x170016A3 RID: 5795
			public ToolTip this[IKeyboardToolTip tool]
			{
				get
				{
					ToolTip result = null;
					WeakReference<ToolTip> weakReference;
					if (this.table.TryGetValue(tool, out weakReference) && !weakReference.TryGetTarget(out result))
					{
						this.table.Remove(tool);
					}
					return result;
				}
				set
				{
					WeakReference<ToolTip> weakReference;
					if (this.table.TryGetValue(tool, out weakReference))
					{
						weakReference.SetTarget(value);
						return;
					}
					this.table.Add(tool, new WeakReference<ToolTip>(value));
				}
			}

			// Token: 0x060068D5 RID: 26837 RVA: 0x00186164 File Offset: 0x00184364
			public void Remove(IKeyboardToolTip tool, ToolTip toolTip)
			{
				WeakReference<ToolTip> weakReference;
				if (this.table.TryGetValue(tool, out weakReference))
				{
					ToolTip toolTip2;
					if (weakReference.TryGetTarget(out toolTip2))
					{
						if (toolTip2 == toolTip)
						{
							this.table.Remove(tool);
							return;
						}
					}
					else
					{
						this.table.Remove(tool);
					}
				}
			}

			// Token: 0x04003B14 RID: 15124
			private ConditionalWeakTable<IKeyboardToolTip, WeakReference<ToolTip>> table = new ConditionalWeakTable<IKeyboardToolTip, WeakReference<ToolTip>>();
		}
	}
}
