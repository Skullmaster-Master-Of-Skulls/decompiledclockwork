using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;

namespace System.Windows.Forms
{
	// Token: 0x02000369 RID: 873
	public class SendKeys
	{
		// Token: 0x0600389D RID: 14493 RVA: 0x000FB00C File Offset: 0x000F920C
		static SendKeys()
		{
			Application.ThreadExit += SendKeys.OnThreadExit;
			SendKeys.messageWindow = new SendKeys.SKWindow();
			SendKeys.messageWindow.CreateControl();
		}

		// Token: 0x0600389E RID: 14494 RVA: 0x00002843 File Offset: 0x00000A43
		private SendKeys()
		{
		}

		// Token: 0x0600389F RID: 14495 RVA: 0x000FB36F File Offset: 0x000F956F
		private static void AddEvent(SendKeys.SKEvent skevent)
		{
			if (SendKeys.events == null)
			{
				SendKeys.events = new Queue();
			}
			SendKeys.events.Enqueue(skevent);
		}

		// Token: 0x060038A0 RID: 14496 RVA: 0x000FB390 File Offset: 0x000F9590
		private static bool AddSimpleKey(char character, int repeat, IntPtr hwnd, int[] haveKeys, bool fStartNewChar, int cGrp)
		{
			int num = (int)UnsafeNativeMethods.VkKeyScan(character);
			if (num != -1)
			{
				if (haveKeys[0] == 0 && (num & 256) != 0)
				{
					SendKeys.AddEvent(new SendKeys.SKEvent(256, 16, fStartNewChar, hwnd));
					fStartNewChar = false;
					haveKeys[0] = 10;
				}
				if (haveKeys[1] == 0 && (num & 512) != 0)
				{
					SendKeys.AddEvent(new SendKeys.SKEvent(256, 17, fStartNewChar, hwnd));
					fStartNewChar = false;
					haveKeys[1] = 10;
				}
				if (haveKeys[2] == 0 && (num & 1024) != 0)
				{
					SendKeys.AddEvent(new SendKeys.SKEvent(256, 18, fStartNewChar, hwnd));
					fStartNewChar = false;
					haveKeys[2] = 10;
				}
				SendKeys.AddMsgsForVK(num & 255, repeat, haveKeys[2] > 0 && haveKeys[1] == 0, hwnd);
				SendKeys.CancelMods(haveKeys, 10, hwnd);
			}
			else
			{
				int num2 = SafeNativeMethods.OemKeyScan((short)('ÿ' & character));
				for (int i = 0; i < repeat; i++)
				{
					SendKeys.AddEvent(new SendKeys.SKEvent(258, (int)character, num2 & 65535, hwnd));
				}
			}
			if (cGrp != 0)
			{
				fStartNewChar = true;
			}
			return fStartNewChar;
		}

		// Token: 0x060038A1 RID: 14497 RVA: 0x000FB48C File Offset: 0x000F968C
		private static void AddMsgsForVK(int vk, int repeat, bool altnoctrldown, IntPtr hwnd)
		{
			for (int i = 0; i < repeat; i++)
			{
				SendKeys.AddEvent(new SendKeys.SKEvent(altnoctrldown ? 260 : 256, vk, SendKeys.fStartNewChar, hwnd));
				SendKeys.AddEvent(new SendKeys.SKEvent(altnoctrldown ? 261 : 257, vk, SendKeys.fStartNewChar, hwnd));
			}
		}

		// Token: 0x060038A2 RID: 14498 RVA: 0x000FB4E8 File Offset: 0x000F96E8
		private static void CancelMods(int[] haveKeys, int level, IntPtr hwnd)
		{
			if (haveKeys[0] == level)
			{
				SendKeys.AddEvent(new SendKeys.SKEvent(257, 16, false, hwnd));
				haveKeys[0] = 0;
			}
			if (haveKeys[1] == level)
			{
				SendKeys.AddEvent(new SendKeys.SKEvent(257, 17, false, hwnd));
				haveKeys[1] = 0;
			}
			if (haveKeys[2] == level)
			{
				SendKeys.AddEvent(new SendKeys.SKEvent(261, 18, false, hwnd));
				haveKeys[2] = 0;
			}
		}

		// Token: 0x060038A3 RID: 14499 RVA: 0x000FB54C File Offset: 0x000F974C
		private static void InstallHook()
		{
			if (SendKeys.hhook == IntPtr.Zero)
			{
				SendKeys.hook = new NativeMethods.HookProc(new SendKeys.SendKeysHookProc().Callback);
				SendKeys.stopHook = false;
				SendKeys.hhook = UnsafeNativeMethods.SetWindowsHookEx(1, SendKeys.hook, new HandleRef(null, UnsafeNativeMethods.GetModuleHandle(null)), 0);
				if (SendKeys.hhook == IntPtr.Zero)
				{
					throw new SecurityException(SR.GetString("SendKeysHookFailed"));
				}
			}
		}

		// Token: 0x060038A4 RID: 14500 RVA: 0x000FB5C4 File Offset: 0x000F97C4
		private static void TestHook()
		{
			SendKeys.hookSupported = new bool?(false);
			try
			{
				NativeMethods.HookProc pfnhook = new NativeMethods.HookProc(SendKeys.EmptyHookCallback);
				IntPtr intPtr = UnsafeNativeMethods.SetWindowsHookEx(1, pfnhook, new HandleRef(null, UnsafeNativeMethods.GetModuleHandle(null)), 0);
				SendKeys.hookSupported = new bool?(intPtr != IntPtr.Zero);
				if (intPtr != IntPtr.Zero)
				{
					UnsafeNativeMethods.UnhookWindowsHookEx(new HandleRef(null, intPtr));
				}
			}
			catch
			{
			}
		}

		// Token: 0x060038A5 RID: 14501 RVA: 0x000F9F19 File Offset: 0x000F8119
		private static IntPtr EmptyHookCallback(int code, IntPtr wparam, IntPtr lparam)
		{
			return IntPtr.Zero;
		}

		// Token: 0x060038A6 RID: 14502 RVA: 0x000FB644 File Offset: 0x000F9844
		private static void LoadSendMethodFromConfig()
		{
			if (SendKeys.sendMethod == null)
			{
				SendKeys.sendMethod = new SendKeys.SendMethodTypes?(SendKeys.SendMethodTypes.Default);
				try
				{
					string text = ConfigurationManager.AppSettings.Get("SendKeys");
					if (!string.IsNullOrEmpty(text))
					{
						if (text.Equals("JournalHook", StringComparison.OrdinalIgnoreCase))
						{
							SendKeys.sendMethod = new SendKeys.SendMethodTypes?(SendKeys.SendMethodTypes.JournalHook);
						}
						else if (text.Equals("SendInput", StringComparison.OrdinalIgnoreCase))
						{
							SendKeys.sendMethod = new SendKeys.SendMethodTypes?(SendKeys.SendMethodTypes.SendInput);
						}
					}
				}
				catch
				{
				}
			}
		}

		// Token: 0x060038A7 RID: 14503 RVA: 0x000FB6CC File Offset: 0x000F98CC
		private static void JournalCancel()
		{
			if (SendKeys.hhook != IntPtr.Zero)
			{
				SendKeys.stopHook = false;
				if (SendKeys.events != null)
				{
					SendKeys.events.Clear();
				}
				SendKeys.hhook = IntPtr.Zero;
			}
		}

		// Token: 0x060038A8 RID: 14504 RVA: 0x000FB700 File Offset: 0x000F9900
		private static byte[] GetKeyboardState()
		{
			byte[] array = new byte[256];
			UnsafeNativeMethods.GetKeyboardState(array);
			return array;
		}

		// Token: 0x060038A9 RID: 14505 RVA: 0x000FB720 File Offset: 0x000F9920
		private static void SetKeyboardState(byte[] keystate)
		{
			UnsafeNativeMethods.SetKeyboardState(keystate);
		}

		// Token: 0x060038AA RID: 14506 RVA: 0x000FB72C File Offset: 0x000F992C
		private static void ClearKeyboardState()
		{
			byte[] keyboardState = SendKeys.GetKeyboardState();
			keyboardState[20] = 0;
			keyboardState[144] = 0;
			keyboardState[145] = 0;
			SendKeys.SetKeyboardState(keyboardState);
		}

		// Token: 0x060038AB RID: 14507 RVA: 0x000FB75C File Offset: 0x000F995C
		private static int MatchKeyword(string keyword)
		{
			for (int i = 0; i < SendKeys.keywords.Length; i++)
			{
				if (string.Equals(SendKeys.keywords[i].keyword, keyword, StringComparison.OrdinalIgnoreCase))
				{
					return SendKeys.keywords[i].vk;
				}
			}
			return -1;
		}

		// Token: 0x060038AC RID: 14508 RVA: 0x000FB7A0 File Offset: 0x000F99A0
		private static void OnThreadExit(object sender, EventArgs e)
		{
			try
			{
				SendKeys.UninstallJournalingHook();
			}
			catch
			{
			}
		}

		// Token: 0x060038AD RID: 14509 RVA: 0x000FB7C8 File Offset: 0x000F99C8
		private static void ParseKeys(string keys, IntPtr hwnd)
		{
			int i = 0;
			int[] array = new int[3];
			int num = 0;
			SendKeys.fStartNewChar = true;
			int length = keys.Length;
			while (i < length)
			{
				int repeat = 1;
				char c = keys[i];
				switch (c)
				{
				case '%':
					if (array[2] != 0)
					{
						throw new ArgumentException(SR.GetString("InvalidSendKeysString", new object[]
						{
							keys
						}));
					}
					SendKeys.AddEvent(new SendKeys.SKEvent((array[1] != 0) ? 256 : 260, 18, SendKeys.fStartNewChar, hwnd));
					SendKeys.fStartNewChar = false;
					array[2] = 10;
					break;
				case '&':
				case '\'':
				case '*':
					goto IL_46A;
				case '(':
					num++;
					if (num > 3)
					{
						throw new ArgumentException(SR.GetString("SendKeysNestingError"));
					}
					if (array[0] == 10)
					{
						array[0] = num;
					}
					if (array[1] == 10)
					{
						array[1] = num;
					}
					if (array[2] == 10)
					{
						array[2] = num;
					}
					break;
				case ')':
					if (num < 1)
					{
						throw new ArgumentException(SR.GetString("InvalidSendKeysString", new object[]
						{
							keys
						}));
					}
					SendKeys.CancelMods(array, num, hwnd);
					num--;
					if (num == 0)
					{
						SendKeys.fStartNewChar = true;
					}
					break;
				case '+':
					if (array[0] != 0)
					{
						throw new ArgumentException(SR.GetString("InvalidSendKeysString", new object[]
						{
							keys
						}));
					}
					SendKeys.AddEvent(new SendKeys.SKEvent(256, 16, SendKeys.fStartNewChar, hwnd));
					SendKeys.fStartNewChar = false;
					array[0] = 10;
					break;
				default:
					if (c != '^')
					{
						switch (c)
						{
						case '{':
						{
							int num2 = i + 1;
							if (num2 + 1 < length && keys[num2] == '}')
							{
								int num3 = num2 + 1;
								while (num3 < length && keys[num3] != '}')
								{
									num3++;
								}
								if (num3 < length)
								{
									num2++;
								}
							}
							while (num2 < length && keys[num2] != '}' && !char.IsWhiteSpace(keys[num2]))
							{
								num2++;
							}
							if (num2 >= length)
							{
								throw new ArgumentException(SR.GetString("SendKeysKeywordDelimError"));
							}
							string text = keys.Substring(i + 1, num2 - (i + 1));
							if (char.IsWhiteSpace(keys[num2]))
							{
								while (num2 < length && char.IsWhiteSpace(keys[num2]))
								{
									num2++;
								}
								if (num2 >= length)
								{
									throw new ArgumentException(SR.GetString("SendKeysKeywordDelimError"));
								}
								if (char.IsDigit(keys[num2]))
								{
									int num4 = num2;
									while (num2 < length && char.IsDigit(keys[num2]))
									{
										num2++;
									}
									repeat = int.Parse(keys.Substring(num4, num2 - num4), CultureInfo.InvariantCulture);
								}
							}
							if (num2 >= length)
							{
								throw new ArgumentException(SR.GetString("SendKeysKeywordDelimError"));
							}
							if (keys[num2] != '}')
							{
								throw new ArgumentException(SR.GetString("InvalidSendKeysRepeat"));
							}
							int num5 = SendKeys.MatchKeyword(text);
							if (num5 != -1)
							{
								if (array[0] == 0 && (num5 & 65536) != 0)
								{
									SendKeys.AddEvent(new SendKeys.SKEvent(256, 16, SendKeys.fStartNewChar, hwnd));
									SendKeys.fStartNewChar = false;
									array[0] = 10;
								}
								if (array[1] == 0 && (num5 & 131072) != 0)
								{
									SendKeys.AddEvent(new SendKeys.SKEvent(256, 17, SendKeys.fStartNewChar, hwnd));
									SendKeys.fStartNewChar = false;
									array[1] = 10;
								}
								if (array[2] == 0 && (num5 & 262144) != 0)
								{
									SendKeys.AddEvent(new SendKeys.SKEvent(256, 18, SendKeys.fStartNewChar, hwnd));
									SendKeys.fStartNewChar = false;
									array[2] = 10;
								}
								SendKeys.AddMsgsForVK(num5, repeat, array[2] > 0 && array[1] == 0, hwnd);
								SendKeys.CancelMods(array, 10, hwnd);
							}
							else
							{
								if (text.Length != 1)
								{
									throw new ArgumentException(SR.GetString("InvalidSendKeysKeyword", new object[]
									{
										keys.Substring(i + 1, num2 - (i + 1))
									}));
								}
								SendKeys.fStartNewChar = SendKeys.AddSimpleKey(text[0], repeat, hwnd, array, SendKeys.fStartNewChar, num);
							}
							i = num2;
							break;
						}
						case '|':
							goto IL_46A;
						case '}':
							throw new ArgumentException(SR.GetString("InvalidSendKeysString", new object[]
							{
								keys
							}));
						case '~':
						{
							int num5 = 13;
							SendKeys.AddMsgsForVK(num5, repeat, array[2] > 0 && array[1] == 0, hwnd);
							break;
						}
						default:
							goto IL_46A;
						}
					}
					else
					{
						if (array[1] != 0)
						{
							throw new ArgumentException(SR.GetString("InvalidSendKeysString", new object[]
							{
								keys
							}));
						}
						SendKeys.AddEvent(new SendKeys.SKEvent(256, 17, SendKeys.fStartNewChar, hwnd));
						SendKeys.fStartNewChar = false;
						array[1] = 10;
					}
					break;
				}
				IL_485:
				i++;
				continue;
				IL_46A:
				SendKeys.fStartNewChar = SendKeys.AddSimpleKey(keys[i], repeat, hwnd, array, SendKeys.fStartNewChar, num);
				goto IL_485;
			}
			if (num != 0)
			{
				throw new ArgumentException(SR.GetString("SendKeysGroupDelimError"));
			}
			SendKeys.CancelMods(array, 10, hwnd);
		}

		// Token: 0x060038AE RID: 14510 RVA: 0x000FBC84 File Offset: 0x000F9E84
		private static void SendInput(byte[] oldKeyboardState, Queue previousEvents)
		{
			SendKeys.AddCancelModifiersForPreviousEvents(previousEvents);
			NativeMethods.INPUT[] array = new NativeMethods.INPUT[2];
			array[0].type = 1;
			array[1].type = 1;
			array[1].inputUnion.ki.wVk = 0;
			array[1].inputUnion.ki.dwFlags = 6;
			array[0].inputUnion.ki.dwExtraInfo = IntPtr.Zero;
			array[0].inputUnion.ki.time = 0;
			array[1].inputUnion.ki.dwExtraInfo = IntPtr.Zero;
			array[1].inputUnion.ki.time = 0;
			int num = Marshal.SizeOf(typeof(NativeMethods.INPUT));
			uint num2 = 0U;
			object syncRoot = SendKeys.events.SyncRoot;
			int count;
			lock (syncRoot)
			{
				bool flag2 = UnsafeNativeMethods.BlockInput(true);
				try
				{
					count = SendKeys.events.Count;
					SendKeys.ClearGlobalKeys();
					for (int i = 0; i < count; i++)
					{
						SendKeys.SKEvent skevent = (SendKeys.SKEvent)SendKeys.events.Dequeue();
						array[0].inputUnion.ki.dwFlags = 0;
						if (skevent.wm == 258)
						{
							array[0].inputUnion.ki.wVk = 0;
							array[0].inputUnion.ki.wScan = (short)skevent.paramL;
							array[0].inputUnion.ki.dwFlags = 4;
							array[1].inputUnion.ki.wScan = (short)skevent.paramL;
							num2 += UnsafeNativeMethods.SendInput(2U, array, num) - 1U;
						}
						else
						{
							array[0].inputUnion.ki.wScan = 0;
							if (skevent.wm == 257 || skevent.wm == 261)
							{
								NativeMethods.INPUT[] array2 = array;
								int num3 = 0;
								array2[num3].inputUnion.ki.dwFlags = (array2[num3].inputUnion.ki.dwFlags | 2);
							}
							if (SendKeys.IsExtendedKey(skevent))
							{
								NativeMethods.INPUT[] array3 = array;
								int num4 = 0;
								array3[num4].inputUnion.ki.dwFlags = (array3[num4].inputUnion.ki.dwFlags | 1);
							}
							array[0].inputUnion.ki.wVk = (short)skevent.paramL;
							num2 += UnsafeNativeMethods.SendInput(1U, array, num);
							SendKeys.CheckGlobalKeys(skevent);
						}
						Thread.Sleep(1);
					}
					SendKeys.ResetKeyboardUsingSendInput(num);
				}
				finally
				{
					SendKeys.SetKeyboardState(oldKeyboardState);
					if (flag2)
					{
						UnsafeNativeMethods.BlockInput(false);
					}
				}
			}
			if ((ulong)num2 != (ulong)((long)count))
			{
				throw new Win32Exception();
			}
		}

		// Token: 0x060038AF RID: 14511 RVA: 0x000FBF64 File Offset: 0x000FA164
		private static void AddCancelModifiersForPreviousEvents(Queue previousEvents)
		{
			if (previousEvents == null)
			{
				return;
			}
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			while (previousEvents.Count > 0)
			{
				SendKeys.SKEvent skevent = (SendKeys.SKEvent)previousEvents.Dequeue();
				bool flag4;
				if (skevent.wm == 257 || skevent.wm == 261)
				{
					flag4 = false;
				}
				else
				{
					if (skevent.wm != 256 && skevent.wm != 260)
					{
						continue;
					}
					flag4 = true;
				}
				if (skevent.paramL == 16)
				{
					flag = flag4;
				}
				else if (skevent.paramL == 17)
				{
					flag2 = flag4;
				}
				else if (skevent.paramL == 18)
				{
					flag3 = flag4;
				}
			}
			if (flag)
			{
				SendKeys.AddEvent(new SendKeys.SKEvent(257, 16, false, IntPtr.Zero));
				return;
			}
			if (flag2)
			{
				SendKeys.AddEvent(new SendKeys.SKEvent(257, 17, false, IntPtr.Zero));
				return;
			}
			if (flag3)
			{
				SendKeys.AddEvent(new SendKeys.SKEvent(261, 18, false, IntPtr.Zero));
			}
		}

		// Token: 0x060038B0 RID: 14512 RVA: 0x000FC04C File Offset: 0x000FA24C
		private static bool IsExtendedKey(SendKeys.SKEvent skEvent)
		{
			return skEvent.paramL == 38 || skEvent.paramL == 40 || skEvent.paramL == 37 || skEvent.paramL == 39 || skEvent.paramL == 33 || skEvent.paramL == 34 || skEvent.paramL == 36 || skEvent.paramL == 35 || skEvent.paramL == 45 || skEvent.paramL == 46;
		}

		// Token: 0x060038B1 RID: 14513 RVA: 0x000FC0BF File Offset: 0x000FA2BF
		private static void ClearGlobalKeys()
		{
			SendKeys.capslockChanged = false;
			SendKeys.numlockChanged = false;
			SendKeys.scrollLockChanged = false;
			SendKeys.kanaChanged = false;
		}

		// Token: 0x060038B2 RID: 14514 RVA: 0x000FC0DC File Offset: 0x000FA2DC
		private static void CheckGlobalKeys(SendKeys.SKEvent skEvent)
		{
			if (skEvent.wm == 256)
			{
				int paramL = skEvent.paramL;
				if (paramL <= 21)
				{
					if (paramL == 20)
					{
						SendKeys.capslockChanged = !SendKeys.capslockChanged;
						return;
					}
					if (paramL != 21)
					{
						return;
					}
					SendKeys.kanaChanged = !SendKeys.kanaChanged;
				}
				else
				{
					if (paramL == 144)
					{
						SendKeys.numlockChanged = !SendKeys.numlockChanged;
						return;
					}
					if (paramL != 145)
					{
						return;
					}
					SendKeys.scrollLockChanged = !SendKeys.scrollLockChanged;
					return;
				}
			}
		}

		// Token: 0x060038B3 RID: 14515 RVA: 0x000FC158 File Offset: 0x000FA358
		private static void ResetKeyboardUsingSendInput(int INPUTSize)
		{
			if (!SendKeys.capslockChanged && !SendKeys.numlockChanged && !SendKeys.scrollLockChanged && !SendKeys.kanaChanged)
			{
				return;
			}
			NativeMethods.INPUT[] array = new NativeMethods.INPUT[2];
			array[0].type = 1;
			array[0].inputUnion.ki.dwFlags = 0;
			array[1].type = 1;
			array[1].inputUnion.ki.dwFlags = 2;
			if (SendKeys.capslockChanged)
			{
				array[0].inputUnion.ki.wVk = 20;
				array[1].inputUnion.ki.wVk = 20;
				UnsafeNativeMethods.SendInput(2U, array, INPUTSize);
			}
			if (SendKeys.numlockChanged)
			{
				array[0].inputUnion.ki.wVk = 144;
				array[1].inputUnion.ki.wVk = 144;
				UnsafeNativeMethods.SendInput(2U, array, INPUTSize);
			}
			if (SendKeys.scrollLockChanged)
			{
				array[0].inputUnion.ki.wVk = 145;
				array[1].inputUnion.ki.wVk = 145;
				UnsafeNativeMethods.SendInput(2U, array, INPUTSize);
			}
			if (SendKeys.kanaChanged)
			{
				array[0].inputUnion.ki.wVk = 21;
				array[1].inputUnion.ki.wVk = 21;
				UnsafeNativeMethods.SendInput(2U, array, INPUTSize);
			}
		}

		// Token: 0x060038B4 RID: 14516 RVA: 0x000FC2DD File Offset: 0x000FA4DD
		public static void Send(string keys)
		{
			SendKeys.Send(keys, null, false);
		}

		// Token: 0x060038B5 RID: 14517 RVA: 0x000FC2E8 File Offset: 0x000FA4E8
		private static void Send(string keys, Control control, bool wait)
		{
			IntSecurity.UnmanagedCode.Demand();
			if (keys == null || keys.Length == 0)
			{
				return;
			}
			if (!wait && !Application.MessageLoop)
			{
				throw new InvalidOperationException(SR.GetString("SendKeysNoMessageLoop"));
			}
			Queue previousEvents = null;
			if (SendKeys.events != null && SendKeys.events.Count != 0)
			{
				previousEvents = (Queue)SendKeys.events.Clone();
			}
			SendKeys.ParseKeys(keys, (control != null) ? control.Handle : IntPtr.Zero);
			if (SendKeys.events == null)
			{
				return;
			}
			SendKeys.LoadSendMethodFromConfig();
			byte[] keyboardState = SendKeys.GetKeyboardState();
			if (SendKeys.sendMethod.Value != SendKeys.SendMethodTypes.SendInput)
			{
				if (SendKeys.hookSupported == null && SendKeys.sendMethod.Value == SendKeys.SendMethodTypes.Default)
				{
					SendKeys.TestHook();
				}
				if (SendKeys.sendMethod.Value == SendKeys.SendMethodTypes.JournalHook || SendKeys.hookSupported.Value)
				{
					SendKeys.ClearKeyboardState();
					SendKeys.InstallHook();
					SendKeys.SetKeyboardState(keyboardState);
				}
			}
			if (SendKeys.sendMethod.Value == SendKeys.SendMethodTypes.SendInput || (SendKeys.sendMethod.Value == SendKeys.SendMethodTypes.Default && !SendKeys.hookSupported.Value))
			{
				SendKeys.SendInput(keyboardState, previousEvents);
			}
			if (wait)
			{
				SendKeys.Flush();
			}
		}

		// Token: 0x060038B6 RID: 14518 RVA: 0x000FC3FC File Offset: 0x000FA5FC
		public static void SendWait(string keys)
		{
			SendKeys.SendWait(keys, null);
		}

		// Token: 0x060038B7 RID: 14519 RVA: 0x000FC405 File Offset: 0x000FA605
		private static void SendWait(string keys, Control control)
		{
			SendKeys.Send(keys, control, true);
		}

		// Token: 0x060038B8 RID: 14520 RVA: 0x000FC40F File Offset: 0x000FA60F
		public static void Flush()
		{
			Application.DoEvents();
			while (SendKeys.events != null && SendKeys.events.Count > 0)
			{
				Application.DoEvents();
			}
		}

		// Token: 0x060038B9 RID: 14521 RVA: 0x000FC434 File Offset: 0x000FA634
		private static void UninstallJournalingHook()
		{
			if (SendKeys.hhook != IntPtr.Zero)
			{
				SendKeys.stopHook = false;
				if (SendKeys.events != null)
				{
					SendKeys.events.Clear();
				}
				UnsafeNativeMethods.UnhookWindowsHookEx(new HandleRef(null, SendKeys.hhook));
				SendKeys.hhook = IntPtr.Zero;
			}
		}

		// Token: 0x040021DB RID: 8667
		private const int HAVESHIFT = 0;

		// Token: 0x040021DC RID: 8668
		private const int HAVECTRL = 1;

		// Token: 0x040021DD RID: 8669
		private const int HAVEALT = 2;

		// Token: 0x040021DE RID: 8670
		private const int UNKNOWN_GROUPING = 10;

		// Token: 0x040021DF RID: 8671
		private static SendKeys.KeywordVk[] keywords = new SendKeys.KeywordVk[]
		{
			new SendKeys.KeywordVk("ENTER", 13),
			new SendKeys.KeywordVk("TAB", 9),
			new SendKeys.KeywordVk("ESC", 27),
			new SendKeys.KeywordVk("ESCAPE", 27),
			new SendKeys.KeywordVk("HOME", 36),
			new SendKeys.KeywordVk("END", 35),
			new SendKeys.KeywordVk("LEFT", 37),
			new SendKeys.KeywordVk("RIGHT", 39),
			new SendKeys.KeywordVk("UP", 38),
			new SendKeys.KeywordVk("DOWN", 40),
			new SendKeys.KeywordVk("PGUP", 33),
			new SendKeys.KeywordVk("PGDN", 34),
			new SendKeys.KeywordVk("NUMLOCK", 144),
			new SendKeys.KeywordVk("SCROLLLOCK", 145),
			new SendKeys.KeywordVk("PRTSC", 44),
			new SendKeys.KeywordVk("BREAK", 3),
			new SendKeys.KeywordVk("BACKSPACE", 8),
			new SendKeys.KeywordVk("BKSP", 8),
			new SendKeys.KeywordVk("BS", 8),
			new SendKeys.KeywordVk("CLEAR", 12),
			new SendKeys.KeywordVk("CAPSLOCK", 20),
			new SendKeys.KeywordVk("INS", 45),
			new SendKeys.KeywordVk("INSERT", 45),
			new SendKeys.KeywordVk("DEL", 46),
			new SendKeys.KeywordVk("DELETE", 46),
			new SendKeys.KeywordVk("HELP", 47),
			new SendKeys.KeywordVk("F1", 112),
			new SendKeys.KeywordVk("F2", 113),
			new SendKeys.KeywordVk("F3", 114),
			new SendKeys.KeywordVk("F4", 115),
			new SendKeys.KeywordVk("F5", 116),
			new SendKeys.KeywordVk("F6", 117),
			new SendKeys.KeywordVk("F7", 118),
			new SendKeys.KeywordVk("F8", 119),
			new SendKeys.KeywordVk("F9", 120),
			new SendKeys.KeywordVk("F10", 121),
			new SendKeys.KeywordVk("F11", 122),
			new SendKeys.KeywordVk("F12", 123),
			new SendKeys.KeywordVk("F13", 124),
			new SendKeys.KeywordVk("F14", 125),
			new SendKeys.KeywordVk("F15", 126),
			new SendKeys.KeywordVk("F16", 127),
			new SendKeys.KeywordVk("MULTIPLY", 106),
			new SendKeys.KeywordVk("ADD", 107),
			new SendKeys.KeywordVk("SUBTRACT", 109),
			new SendKeys.KeywordVk("DIVIDE", 111),
			new SendKeys.KeywordVk("+", 107),
			new SendKeys.KeywordVk("%", 65589),
			new SendKeys.KeywordVk("^", 65590)
		};

		// Token: 0x040021E0 RID: 8672
		private const int SHIFTKEYSCAN = 256;

		// Token: 0x040021E1 RID: 8673
		private const int CTRLKEYSCAN = 512;

		// Token: 0x040021E2 RID: 8674
		private const int ALTKEYSCAN = 1024;

		// Token: 0x040021E3 RID: 8675
		private static bool stopHook;

		// Token: 0x040021E4 RID: 8676
		private static IntPtr hhook;

		// Token: 0x040021E5 RID: 8677
		private static NativeMethods.HookProc hook;

		// Token: 0x040021E6 RID: 8678
		private static Queue events;

		// Token: 0x040021E7 RID: 8679
		private static bool fStartNewChar;

		// Token: 0x040021E8 RID: 8680
		private static SendKeys.SKWindow messageWindow;

		// Token: 0x040021E9 RID: 8681
		private static SendKeys.SendMethodTypes? sendMethod = null;

		// Token: 0x040021EA RID: 8682
		private static bool? hookSupported = null;

		// Token: 0x040021EB RID: 8683
		private static bool capslockChanged;

		// Token: 0x040021EC RID: 8684
		private static bool numlockChanged;

		// Token: 0x040021ED RID: 8685
		private static bool scrollLockChanged;

		// Token: 0x040021EE RID: 8686
		private static bool kanaChanged;

		// Token: 0x020007E1 RID: 2017
		private enum SendMethodTypes
		{
			// Token: 0x040042C1 RID: 17089
			Default = 1,
			// Token: 0x040042C2 RID: 17090
			JournalHook,
			// Token: 0x040042C3 RID: 17091
			SendInput
		}

		// Token: 0x020007E2 RID: 2018
		private class SKWindow : Control
		{
			// Token: 0x06006DEF RID: 28143 RVA: 0x001935A6 File Offset: 0x001917A6
			public SKWindow()
			{
				base.SetState(524288, true);
				base.SetState2(8, false);
				base.SetBounds(-1, -1, 0, 0);
				base.Visible = false;
			}

			// Token: 0x06006DF0 RID: 28144 RVA: 0x001935D4 File Offset: 0x001917D4
			protected override void WndProc(ref Message m)
			{
				if (m.Msg == 75)
				{
					try
					{
						SendKeys.JournalCancel();
					}
					catch
					{
					}
				}
			}
		}

		// Token: 0x020007E3 RID: 2019
		private class SKEvent
		{
			// Token: 0x06006DF1 RID: 28145 RVA: 0x00193608 File Offset: 0x00191808
			public SKEvent(int a, int b, bool c, IntPtr hwnd)
			{
				this.wm = a;
				this.paramL = b;
				this.paramH = (c ? 1 : 0);
				this.hwnd = hwnd;
			}

			// Token: 0x06006DF2 RID: 28146 RVA: 0x00193633 File Offset: 0x00191833
			public SKEvent(int a, int b, int c, IntPtr hwnd)
			{
				this.wm = a;
				this.paramL = b;
				this.paramH = c;
				this.hwnd = hwnd;
			}

			// Token: 0x040042C4 RID: 17092
			internal int wm;

			// Token: 0x040042C5 RID: 17093
			internal int paramL;

			// Token: 0x040042C6 RID: 17094
			internal int paramH;

			// Token: 0x040042C7 RID: 17095
			internal IntPtr hwnd;
		}

		// Token: 0x020007E4 RID: 2020
		private class KeywordVk
		{
			// Token: 0x06006DF3 RID: 28147 RVA: 0x00193658 File Offset: 0x00191858
			public KeywordVk(string key, int v)
			{
				this.keyword = key;
				this.vk = v;
			}

			// Token: 0x040042C8 RID: 17096
			internal string keyword;

			// Token: 0x040042C9 RID: 17097
			internal int vk;
		}

		// Token: 0x020007E5 RID: 2021
		private class SendKeysHookProc
		{
			// Token: 0x06006DF4 RID: 28148 RVA: 0x00193670 File Offset: 0x00191870
			public virtual IntPtr Callback(int code, IntPtr wparam, IntPtr lparam)
			{
				NativeMethods.EVENTMSG eventmsg = (NativeMethods.EVENTMSG)UnsafeNativeMethods.PtrToStructure(lparam, typeof(NativeMethods.EVENTMSG));
				if (UnsafeNativeMethods.GetAsyncKeyState(19) != 0)
				{
					SendKeys.stopHook = true;
				}
				if (code != 1)
				{
					if (code == 2)
					{
						if (this.gotNextEvent)
						{
							if (SendKeys.events != null && SendKeys.events.Count > 0)
							{
								SendKeys.events.Dequeue();
							}
							SendKeys.stopHook = (SendKeys.events == null || SendKeys.events.Count == 0);
						}
					}
					else if (code < 0)
					{
						UnsafeNativeMethods.CallNextHookEx(new HandleRef(null, SendKeys.hhook), code, wparam, lparam);
					}
				}
				else
				{
					this.gotNextEvent = true;
					SendKeys.SKEvent skevent = (SendKeys.SKEvent)SendKeys.events.Peek();
					eventmsg.message = skevent.wm;
					eventmsg.paramL = skevent.paramL;
					eventmsg.paramH = skevent.paramH;
					eventmsg.hwnd = skevent.hwnd;
					eventmsg.time = SafeNativeMethods.GetTickCount();
					Marshal.StructureToPtr(eventmsg, lparam, true);
				}
				if (SendKeys.stopHook)
				{
					SendKeys.UninstallJournalingHook();
					this.gotNextEvent = false;
				}
				return IntPtr.Zero;
			}

			// Token: 0x040042CA RID: 17098
			private bool gotNextEvent;
		}
	}
}
