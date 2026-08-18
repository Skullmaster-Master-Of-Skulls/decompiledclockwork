using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;

namespace System.Internal
{
	// Token: 0x020000FD RID: 253
	internal class DebugHandleTracker
	{
		// Token: 0x060003F9 RID: 1017 RVA: 0x0000C968 File Offset: 0x0000AB68
		static DebugHandleTracker()
		{
			DebugHandleTracker.tracker = new DebugHandleTracker();
			if (CompModSwitches.HandleLeak.Level > TraceLevel.Off || CompModSwitches.TraceCollect.Enabled)
			{
				HandleCollector.HandleAdded += DebugHandleTracker.tracker.OnHandleAdd;
				HandleCollector.HandleRemoved += DebugHandleTracker.tracker.OnHandleRemove;
			}
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x00002843 File Offset: 0x00000A43
		private DebugHandleTracker()
		{
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x0000C9D8 File Offset: 0x0000ABD8
		public static void IgnoreCurrentHandlesAsLeaks()
		{
			object obj = DebugHandleTracker.internalSyncObject;
			lock (obj)
			{
				if (CompModSwitches.HandleLeak.Level >= TraceLevel.Warning)
				{
					DebugHandleTracker.HandleType[] array = new DebugHandleTracker.HandleType[DebugHandleTracker.handleTypes.Values.Count];
					DebugHandleTracker.handleTypes.Values.CopyTo(array, 0);
					for (int i = 0; i < array.Length; i++)
					{
						if (array[i] != null)
						{
							array[i].IgnoreCurrentHandlesAsLeaks();
						}
					}
				}
			}
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x0000CA60 File Offset: 0x0000AC60
		public static void CheckLeaks()
		{
			object obj = DebugHandleTracker.internalSyncObject;
			lock (obj)
			{
				if (CompModSwitches.HandleLeak.Level >= TraceLevel.Warning)
				{
					GC.Collect();
					GC.WaitForPendingFinalizers();
					DebugHandleTracker.HandleType[] array = new DebugHandleTracker.HandleType[DebugHandleTracker.handleTypes.Values.Count];
					DebugHandleTracker.handleTypes.Values.CopyTo(array, 0);
					for (int i = 0; i < array.Length; i++)
					{
						if (array[i] != null)
						{
							array[i].CheckLeaks();
						}
					}
				}
			}
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x000072B6 File Offset: 0x000054B6
		public static void Initialize()
		{
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x0000CAF4 File Offset: 0x0000ACF4
		private void OnHandleAdd(string handleName, IntPtr handle, int handleCount)
		{
			DebugHandleTracker.HandleType handleType = (DebugHandleTracker.HandleType)DebugHandleTracker.handleTypes[handleName];
			if (handleType == null)
			{
				handleType = new DebugHandleTracker.HandleType(handleName);
				DebugHandleTracker.handleTypes[handleName] = handleType;
			}
			handleType.Add(handle);
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0000CB30 File Offset: 0x0000AD30
		private void OnHandleRemove(string handleName, IntPtr handle, int HandleCount)
		{
			DebugHandleTracker.HandleType handleType = (DebugHandleTracker.HandleType)DebugHandleTracker.handleTypes[handleName];
			bool flag = false;
			if (handleType != null)
			{
				flag = handleType.Remove(handle);
			}
			if (!flag)
			{
				TraceLevel level = CompModSwitches.HandleLeak.Level;
			}
		}

		// Token: 0x0400043D RID: 1085
		private static Hashtable handleTypes = new Hashtable();

		// Token: 0x0400043E RID: 1086
		private static DebugHandleTracker tracker;

		// Token: 0x0400043F RID: 1087
		private static object internalSyncObject = new object();

		// Token: 0x02000548 RID: 1352
		private class HandleType
		{
			// Token: 0x06005574 RID: 21876 RVA: 0x00166760 File Offset: 0x00164960
			public HandleType(string name)
			{
				this.name = name;
				this.buckets = new DebugHandleTracker.HandleType.HandleEntry[10];
			}

			// Token: 0x06005575 RID: 21877 RVA: 0x0016677C File Offset: 0x0016497C
			public void Add(IntPtr handle)
			{
				lock (this)
				{
					int num = this.ComputeHash(handle);
					if (CompModSwitches.HandleLeak.Level >= TraceLevel.Info)
					{
						TraceLevel level = CompModSwitches.HandleLeak.Level;
					}
					for (DebugHandleTracker.HandleType.HandleEntry handleEntry = this.buckets[num]; handleEntry != null; handleEntry = handleEntry.next)
					{
					}
					this.buckets[num] = new DebugHandleTracker.HandleType.HandleEntry(this.buckets[num], handle);
					this.handleCount++;
				}
			}

			// Token: 0x06005576 RID: 21878 RVA: 0x0016680C File Offset: 0x00164A0C
			public void CheckLeaks()
			{
				lock (this)
				{
					bool flag2 = false;
					if (this.handleCount > 0)
					{
						for (int i = 0; i < 10; i++)
						{
							for (DebugHandleTracker.HandleType.HandleEntry handleEntry = this.buckets[i]; handleEntry != null; handleEntry = handleEntry.next)
							{
								if (!handleEntry.ignorableAsLeak && !flag2)
								{
									flag2 = true;
								}
							}
						}
					}
				}
			}

			// Token: 0x06005577 RID: 21879 RVA: 0x00166880 File Offset: 0x00164A80
			public void IgnoreCurrentHandlesAsLeaks()
			{
				lock (this)
				{
					if (this.handleCount > 0)
					{
						for (int i = 0; i < 10; i++)
						{
							for (DebugHandleTracker.HandleType.HandleEntry handleEntry = this.buckets[i]; handleEntry != null; handleEntry = handleEntry.next)
							{
								handleEntry.ignorableAsLeak = true;
							}
						}
					}
				}
			}

			// Token: 0x06005578 RID: 21880 RVA: 0x001668E8 File Offset: 0x00164AE8
			private int ComputeHash(IntPtr handle)
			{
				return ((int)handle & 65535) % 10;
			}

			// Token: 0x06005579 RID: 21881 RVA: 0x001668FC File Offset: 0x00164AFC
			public bool Remove(IntPtr handle)
			{
				bool result;
				lock (this)
				{
					int num = this.ComputeHash(handle);
					if (CompModSwitches.HandleLeak.Level >= TraceLevel.Info)
					{
						TraceLevel level = CompModSwitches.HandleLeak.Level;
					}
					DebugHandleTracker.HandleType.HandleEntry handleEntry = this.buckets[num];
					DebugHandleTracker.HandleType.HandleEntry handleEntry2 = null;
					while (handleEntry != null && handleEntry.handle != handle)
					{
						handleEntry2 = handleEntry;
						handleEntry = handleEntry.next;
					}
					if (handleEntry != null)
					{
						if (handleEntry2 == null)
						{
							this.buckets[num] = handleEntry.next;
						}
						else
						{
							handleEntry2.next = handleEntry.next;
						}
						this.handleCount--;
						result = true;
					}
					else
					{
						result = false;
					}
				}
				return result;
			}

			// Token: 0x04003813 RID: 14355
			public readonly string name;

			// Token: 0x04003814 RID: 14356
			private int handleCount;

			// Token: 0x04003815 RID: 14357
			private DebugHandleTracker.HandleType.HandleEntry[] buckets;

			// Token: 0x04003816 RID: 14358
			private const int BUCKETS = 10;

			// Token: 0x020008A5 RID: 2213
			private class HandleEntry
			{
				// Token: 0x06007263 RID: 29283 RVA: 0x001A3F33 File Offset: 0x001A2133
				public HandleEntry(DebugHandleTracker.HandleType.HandleEntry next, IntPtr handle)
				{
					this.handle = handle;
					this.next = next;
					if (CompModSwitches.HandleLeak.Level > TraceLevel.Off)
					{
						this.callStack = Environment.StackTrace;
						return;
					}
					this.callStack = null;
				}

				// Token: 0x06007264 RID: 29284 RVA: 0x001A3F6C File Offset: 0x001A216C
				public string ToString(DebugHandleTracker.HandleType type)
				{
					DebugHandleTracker.HandleType.HandleEntry.StackParser stackParser = new DebugHandleTracker.HandleType.HandleEntry.StackParser(this.callStack);
					stackParser.DiscardTo("HandleCollector.Add");
					stackParser.DiscardNext();
					stackParser.Truncate(40);
					string str = "";
					return Convert.ToString((int)this.handle, 16) + str + ": " + stackParser.ToString();
				}

				// Token: 0x040044D8 RID: 17624
				public readonly IntPtr handle;

				// Token: 0x040044D9 RID: 17625
				public DebugHandleTracker.HandleType.HandleEntry next;

				// Token: 0x040044DA RID: 17626
				public readonly string callStack;

				// Token: 0x040044DB RID: 17627
				public bool ignorableAsLeak;

				// Token: 0x02000982 RID: 2434
				private class StackParser
				{
					// Token: 0x060075A5 RID: 30117 RVA: 0x001A9C0C File Offset: 0x001A7E0C
					public StackParser(string callStack)
					{
						this.releventStack = callStack;
						this.length = this.releventStack.Length;
					}

					// Token: 0x060075A6 RID: 30118 RVA: 0x001A9C2C File Offset: 0x001A7E2C
					private static bool ContainsString(string str, string token)
					{
						int num = str.Length;
						int num2 = token.Length;
						for (int i = 0; i < num; i++)
						{
							int num3 = 0;
							while (num3 < num2 && str[i + num3] == token[num3])
							{
								num3++;
							}
							if (num3 == num2)
							{
								return true;
							}
						}
						return false;
					}

					// Token: 0x060075A7 RID: 30119 RVA: 0x001A9C78 File Offset: 0x001A7E78
					public void DiscardNext()
					{
						this.GetLine();
					}

					// Token: 0x060075A8 RID: 30120 RVA: 0x001A9C84 File Offset: 0x001A7E84
					public void DiscardTo(string discardText)
					{
						while (this.startIndex < this.length)
						{
							string line = this.GetLine();
							if (line == null || DebugHandleTracker.HandleType.HandleEntry.StackParser.ContainsString(line, discardText))
							{
								break;
							}
						}
					}

					// Token: 0x060075A9 RID: 30121 RVA: 0x001A9CB4 File Offset: 0x001A7EB4
					private string GetLine()
					{
						this.endIndex = this.releventStack.IndexOf('\r', this.startIndex);
						if (this.endIndex < 0)
						{
							this.endIndex = this.length - 1;
						}
						string text = this.releventStack.Substring(this.startIndex, this.endIndex - this.startIndex);
						char c;
						while (this.endIndex < this.length && ((c = this.releventStack[this.endIndex]) == '\r' || c == '\n'))
						{
							this.endIndex++;
						}
						if (this.startIndex == this.endIndex)
						{
							return null;
						}
						this.startIndex = this.endIndex;
						return text.Replace('\t', ' ');
					}

					// Token: 0x060075AA RID: 30122 RVA: 0x001A9D72 File Offset: 0x001A7F72
					public override string ToString()
					{
						return this.releventStack.Substring(this.startIndex);
					}

					// Token: 0x060075AB RID: 30123 RVA: 0x001A9D88 File Offset: 0x001A7F88
					public void Truncate(int lines)
					{
						string text = "";
						while (lines-- > 0 && this.startIndex < this.length)
						{
							if (text == null)
							{
								text = this.GetLine();
							}
							else
							{
								text = text + ": " + this.GetLine();
							}
							text += Environment.NewLine;
						}
						this.releventStack = text;
						this.startIndex = 0;
						this.endIndex = 0;
						this.length = this.releventStack.Length;
					}

					// Token: 0x040047D6 RID: 18390
					internal string releventStack;

					// Token: 0x040047D7 RID: 18391
					internal int startIndex;

					// Token: 0x040047D8 RID: 18392
					internal int endIndex;

					// Token: 0x040047D9 RID: 18393
					internal int length;
				}
			}
		}
	}
}
