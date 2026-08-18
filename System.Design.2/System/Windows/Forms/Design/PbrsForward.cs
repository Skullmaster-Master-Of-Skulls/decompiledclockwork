using System;
using System.Collections;
using System.ComponentModel.Design;
using System.Design;

namespace System.Windows.Forms.Design
{
	// Token: 0x0200031E RID: 798
	internal class PbrsForward : IWindowTarget
	{
		// Token: 0x06001FBA RID: 8122 RVA: 0x000C0771 File Offset: 0x000BE971
		public PbrsForward(Control target, IServiceProvider sp)
		{
			this.target = target;
			this.oldTarget = target.WindowTarget;
			this.sp = sp;
			target.WindowTarget = this;
		}

		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x06001FBB RID: 8123 RVA: 0x000C079A File Offset: 0x000BE99A
		private IMenuCommandService MenuCommandService
		{
			get
			{
				if (this.menuCommandSvc == null && this.sp != null)
				{
					this.menuCommandSvc = (IMenuCommandService)this.sp.GetService(typeof(IMenuCommandService));
				}
				return this.menuCommandSvc;
			}
		}

		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x06001FBC RID: 8124 RVA: 0x000C07D2 File Offset: 0x000BE9D2
		private ISupportInSituService InSituSupportService
		{
			get
			{
				return (ISupportInSituService)this.sp.GetService(typeof(ISupportInSituService));
			}
		}

		// Token: 0x06001FBD RID: 8125 RVA: 0x000C07EE File Offset: 0x000BE9EE
		public void Dispose()
		{
			this.target.WindowTarget = this.oldTarget;
		}

		// Token: 0x06001FBE RID: 8126 RVA: 0x00003937 File Offset: 0x00001B37
		void IWindowTarget.OnHandleChange(IntPtr newHandle)
		{
		}

		// Token: 0x06001FBF RID: 8127 RVA: 0x000C0804 File Offset: 0x000BEA04
		void IWindowTarget.OnMessage(ref Message m)
		{
			this.ignoreMessages = false;
			if (((m.Msg >= 256 && m.Msg <= 264) || (m.Msg >= 269 && m.Msg <= 271)) && this.InSituSupportService != null)
			{
				this.ignoreMessages = this.InSituSupportService.IgnoreMessages;
			}
			int msg = m.Msg;
			if (msg <= 258)
			{
				if (msg != 8)
				{
					switch (msg)
					{
					case 256:
						this.lastKeyDown = m;
						goto IL_32D;
					case 257:
						break;
					case 258:
						goto IL_251;
					default:
						goto IL_32D;
					}
				}
				else
				{
					if (this.postCharMessage)
					{
						UnsafeNativeMethods.PostMessage(this.target.Handle, 6552, IntPtr.Zero, IntPtr.Zero);
						this.postCharMessage = false;
						goto IL_32D;
					}
					goto IL_32D;
				}
			}
			else
			{
				switch (msg)
				{
				case 269:
				case 271:
					goto IL_251;
				case 270:
					break;
				default:
				{
					if (msg != 6552)
					{
						goto IL_32D;
					}
					if (this.bufferedChars == null)
					{
						return;
					}
					IntPtr intPtr = IntPtr.Zero;
					if (!this.ignoreMessages)
					{
						intPtr = NativeMethods.GetFocus();
					}
					else if (this.InSituSupportService != null)
					{
						intPtr = this.InSituSupportService.GetEditWindow();
					}
					else
					{
						intPtr = NativeMethods.GetFocus();
					}
					if (intPtr != m.HWnd)
					{
						foreach (object obj in this.bufferedChars)
						{
							PbrsForward.BufferedKey bufferedKey = (PbrsForward.BufferedKey)obj;
							if (bufferedKey.KeyChar.Msg == 258)
							{
								if (bufferedKey.KeyDown.Msg != 0)
								{
									NativeMethods.SendMessage(intPtr, 256, bufferedKey.KeyDown.WParam, bufferedKey.KeyDown.LParam);
								}
								NativeMethods.SendMessage(intPtr, 258, bufferedKey.KeyChar.WParam, bufferedKey.KeyChar.LParam);
								if (bufferedKey.KeyUp.Msg != 0)
								{
									NativeMethods.SendMessage(intPtr, 257, bufferedKey.KeyUp.WParam, bufferedKey.KeyUp.LParam);
								}
							}
							else
							{
								NativeMethods.SendMessage(intPtr, bufferedKey.KeyChar.Msg, bufferedKey.KeyChar.WParam, bufferedKey.KeyChar.LParam);
							}
						}
					}
					this.bufferedChars.Clear();
					return;
				}
				}
			}
			this.lastKeyDown.Msg = 0;
			goto IL_32D;
			IL_251:
			if ((Control.ModifierKeys & (Keys.Control | Keys.Alt)) == Keys.None)
			{
				if (this.bufferedChars == null)
				{
					this.bufferedChars = new ArrayList();
				}
				this.bufferedChars.Add(new PbrsForward.BufferedKey(this.lastKeyDown, m, this.lastKeyDown));
				if (!this.ignoreMessages && this.MenuCommandService != null)
				{
					this.postCharMessage = true;
					this.MenuCommandService.GlobalInvoke(StandardCommands.PropertiesWindow);
				}
				else if (this.ignoreMessages && m.Msg != 271 && this.InSituSupportService != null)
				{
					this.postCharMessage = true;
					this.InSituSupportService.HandleKeyChar();
				}
				if (this.postCharMessage)
				{
					return;
				}
			}
			IL_32D:
			if (this.oldTarget != null)
			{
				this.oldTarget.OnMessage(ref m);
			}
		}

		// Token: 0x04001887 RID: 6279
		private Control target;

		// Token: 0x04001888 RID: 6280
		private IWindowTarget oldTarget;

		// Token: 0x04001889 RID: 6281
		private Message lastKeyDown;

		// Token: 0x0400188A RID: 6282
		private ArrayList bufferedChars;

		// Token: 0x0400188B RID: 6283
		private const int WM_PRIVATE_POSTCHAR = 6552;

		// Token: 0x0400188C RID: 6284
		private bool postCharMessage;

		// Token: 0x0400188D RID: 6285
		private IMenuCommandService menuCommandSvc;

		// Token: 0x0400188E RID: 6286
		private IServiceProvider sp;

		// Token: 0x0400188F RID: 6287
		private bool ignoreMessages;

		// Token: 0x02000588 RID: 1416
		private struct BufferedKey
		{
			// Token: 0x0600329F RID: 12959 RVA: 0x00111CA1 File Offset: 0x0010FEA1
			public BufferedKey(Message keyDown, Message keyChar, Message keyUp)
			{
				this.KeyChar = keyChar;
				this.KeyDown = keyDown;
				this.KeyUp = keyUp;
			}

			// Token: 0x040021B1 RID: 8625
			public readonly Message KeyDown;

			// Token: 0x040021B2 RID: 8626
			public readonly Message KeyUp;

			// Token: 0x040021B3 RID: 8627
			public readonly Message KeyChar;
		}
	}
}
