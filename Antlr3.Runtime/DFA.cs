using System;
using System.Diagnostics;
using Antlr.Runtime.Debug;

namespace Antlr.Runtime
{
	// Token: 0x0200001B RID: 27
	public class DFA
	{
		// Token: 0x0600013F RID: 319 RVA: 0x000044F3 File Offset: 0x000026F3
		public DFA() : this(new SpecialStateTransitionHandler(DFA.SpecialStateTransitionDefault))
		{
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00004507 File Offset: 0x00002707
		public DFA(SpecialStateTransitionHandler specialStateTransition)
		{
			this.SpecialStateTransition = (specialStateTransition ?? new SpecialStateTransitionHandler(DFA.SpecialStateTransitionDefault));
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000141 RID: 321 RVA: 0x00004526 File Offset: 0x00002726
		public virtual string Description
		{
			get
			{
				return "n/a";
			}
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00004530 File Offset: 0x00002730
		public virtual int Predict(IIntStream input)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			int marker = input.Mark();
			int num = 0;
			int result;
			try
			{
				char c;
				for (;;)
				{
					int num2 = (int)this.special[num];
					if (num2 >= 0)
					{
						num = this.SpecialStateTransition(this, num2, input);
						if (num == -1)
						{
							break;
						}
						input.Consume();
					}
					else
					{
						if (this.accept[num] >= 1)
						{
							goto Block_5;
						}
						c = (char)input.LA(1);
						if (c >= this.min[num] && c <= this.max[num])
						{
							int num3 = (int)this.transition[num][(int)(c - this.min[num])];
							if (num3 < 0)
							{
								if (this.eot[num] < 0)
								{
									goto IL_C1;
								}
								num = (int)this.eot[num];
								input.Consume();
							}
							else
							{
								num = num3;
								input.Consume();
							}
						}
						else
						{
							if (this.eot[num] < 0)
							{
								goto IL_FB;
							}
							num = (int)this.eot[num];
							input.Consume();
						}
					}
				}
				this.NoViableAlt(num, input);
				return 0;
				Block_5:
				return (int)this.accept[num];
				IL_C1:
				this.NoViableAlt(num, input);
				return 0;
				IL_FB:
				if (c == '￿' && this.eof[num] >= 0)
				{
					result = (int)this.accept[(int)this.eof[num]];
				}
				else
				{
					this.NoViableAlt(num, input);
					result = 0;
				}
			}
			finally
			{
				input.Rewind(marker);
			}
			return result;
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00004694 File Offset: 0x00002894
		[Conditional("DEBUG_DFA")]
		private void DfaDebugMessage(string format, params object[] args)
		{
			Console.Error.WriteLine(format, args);
		}

		// Token: 0x06000144 RID: 324 RVA: 0x000046A4 File Offset: 0x000028A4
		[Conditional("DEBUG_DFA")]
		private void DfaDebugInvalidSymbol(int s)
		{
			Console.Error.WriteLine("min[{0}]={1}", s, this.min[s]);
			Console.Error.WriteLine("max[{0}]={1}", s, this.max[s]);
			Console.Error.WriteLine("eot[{0}]={1}", s, this.eot[s]);
			Console.Error.WriteLine("eof[{0}]={1}", s, this.eof[s]);
			for (int i = 0; i < this.transition[s].Length; i++)
			{
				Console.Error.Write(this.transition[s][i] + " ");
			}
			Console.Error.WriteLine();
		}

		// Token: 0x06000145 RID: 325 RVA: 0x0000477C File Offset: 0x0000297C
		protected virtual void NoViableAlt(int s, IIntStream input)
		{
			if (this.recognizer.state.backtracking > 0)
			{
				this.recognizer.state.failed = true;
				return;
			}
			NoViableAltException ex = new NoViableAltException(this.Description, this.decisionNumber, s, input);
			this.Error(ex);
			throw ex;
		}

		// Token: 0x06000146 RID: 326 RVA: 0x000047CA File Offset: 0x000029CA
		public virtual void Error(NoViableAltException nvae)
		{
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000147 RID: 327 RVA: 0x000047CC File Offset: 0x000029CC
		// (set) Token: 0x06000148 RID: 328 RVA: 0x000047D4 File Offset: 0x000029D4
		public SpecialStateTransitionHandler SpecialStateTransition { get; private set; }

		// Token: 0x06000149 RID: 329 RVA: 0x000047DD File Offset: 0x000029DD
		private static int SpecialStateTransitionDefault(DFA dfa, int s, IIntStream input)
		{
			return -1;
		}

		// Token: 0x0600014A RID: 330 RVA: 0x000047E0 File Offset: 0x000029E0
		public static short[] UnpackEncodedString(string encodedString)
		{
			int num = 0;
			for (int i = 0; i < encodedString.Length; i += 2)
			{
				num += (int)encodedString[i];
			}
			short[] array = new short[num];
			int num2 = 0;
			for (int j = 0; j < encodedString.Length; j += 2)
			{
				char c = encodedString[j];
				char c2 = encodedString[j + 1];
				for (int k = 1; k <= (int)c; k++)
				{
					array[num2++] = (short)c2;
				}
			}
			return array;
		}

		// Token: 0x0600014B RID: 331 RVA: 0x0000485C File Offset: 0x00002A5C
		public static char[] UnpackEncodedStringToUnsignedChars(string encodedString)
		{
			int num = 0;
			for (int i = 0; i < encodedString.Length; i += 2)
			{
				num += (int)encodedString[i];
			}
			char[] array = new char[num];
			int num2 = 0;
			for (int j = 0; j < encodedString.Length; j += 2)
			{
				char c = encodedString[j];
				char c2 = encodedString[j + 1];
				for (int k = 1; k <= (int)c; k++)
				{
					array[num2++] = c2;
				}
			}
			return array;
		}

		// Token: 0x0600014C RID: 332 RVA: 0x000048D8 File Offset: 0x00002AD8
		[Conditional("ANTLR_DEBUG")]
		protected virtual void DebugRecognitionException(RecognitionException ex)
		{
			IDebugEventListener debugListener = this.recognizer.DebugListener;
			if (debugListener != null)
			{
				debugListener.RecognitionException(ex);
			}
		}

		// Token: 0x04000035 RID: 53
		protected short[] eot;

		// Token: 0x04000036 RID: 54
		protected short[] eof;

		// Token: 0x04000037 RID: 55
		protected char[] min;

		// Token: 0x04000038 RID: 56
		protected char[] max;

		// Token: 0x04000039 RID: 57
		protected short[] accept;

		// Token: 0x0400003A RID: 58
		protected short[] special;

		// Token: 0x0400003B RID: 59
		protected short[][] transition;

		// Token: 0x0400003C RID: 60
		protected int decisionNumber;

		// Token: 0x0400003D RID: 61
		protected BaseRecognizer recognizer;

		// Token: 0x0400003E RID: 62
		public bool debug;
	}
}
