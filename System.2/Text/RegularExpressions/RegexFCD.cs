using System;
using System.Globalization;

namespace System.Text.RegularExpressions
{
	// Token: 0x02000695 RID: 1685
	internal sealed class RegexFCD
	{
		// Token: 0x06003EB5 RID: 16053 RVA: 0x00104FF8 File Offset: 0x001031F8
		internal static RegexPrefix FirstChars(RegexTree t)
		{
			RegexFCD regexFCD = new RegexFCD();
			RegexFC regexFC = regexFCD.RegexFCFromRegexTree(t);
			if (regexFC == null || regexFC._nullable)
			{
				return null;
			}
			CultureInfo culture = ((t._options & RegexOptions.CultureInvariant) != RegexOptions.None) ? CultureInfo.InvariantCulture : CultureInfo.CurrentCulture;
			return new RegexPrefix(regexFC.GetFirstChars(culture), regexFC.IsCaseInsensitive());
		}

		// Token: 0x06003EB6 RID: 16054 RVA: 0x00105050 File Offset: 0x00103250
		internal static RegexPrefix Prefix(RegexTree tree)
		{
			RegexNode regexNode = null;
			int num = 0;
			RegexNode regexNode2 = tree._root;
			for (;;)
			{
				int type = regexNode2._type;
				switch (type)
				{
				case 3:
				case 6:
					goto IL_C3;
				case 4:
				case 5:
				case 7:
				case 8:
				case 10:
				case 11:
				case 13:
				case 17:
				case 22:
				case 24:
				case 26:
				case 27:
				case 29:
					goto IL_135;
				case 9:
					goto IL_FD;
				case 12:
					goto IL_11E;
				case 14:
				case 15:
				case 16:
				case 18:
				case 19:
				case 20:
				case 21:
				case 23:
				case 30:
				case 31:
					break;
				case 25:
					if (regexNode2.ChildCount() > 0)
					{
						regexNode = regexNode2;
						num = 0;
					}
					break;
				case 28:
				case 32:
					regexNode2 = regexNode2.Child(0);
					regexNode = null;
					continue;
				default:
					if (type != 41)
					{
						goto Block_2;
					}
					break;
				}
				if (regexNode == null || num >= regexNode.ChildCount())
				{
					goto IL_147;
				}
				regexNode2 = regexNode.Child(num++);
			}
			Block_2:
			goto IL_135;
			IL_C3:
			if (regexNode2._m > 0)
			{
				string prefix = string.Empty.PadRight(regexNode2._m, regexNode2._ch);
				return new RegexPrefix(prefix, (regexNode2._options & RegexOptions.IgnoreCase) > RegexOptions.None);
			}
			return RegexPrefix.Empty;
			IL_FD:
			return new RegexPrefix(regexNode2._ch.ToString(CultureInfo.InvariantCulture), (regexNode2._options & RegexOptions.IgnoreCase) > RegexOptions.None);
			IL_11E:
			return new RegexPrefix(regexNode2._str, (regexNode2._options & RegexOptions.IgnoreCase) > RegexOptions.None);
			IL_135:
			return RegexPrefix.Empty;
			IL_147:
			return RegexPrefix.Empty;
		}

		// Token: 0x06003EB7 RID: 16055 RVA: 0x001051BC File Offset: 0x001033BC
		internal static int Anchors(RegexTree tree)
		{
			RegexNode regexNode = null;
			int num = 0;
			int num2 = 0;
			RegexNode regexNode2 = tree._root;
			int type;
			for (;;)
			{
				type = regexNode2._type;
				switch (type)
				{
				case 14:
				case 15:
				case 16:
				case 18:
				case 19:
				case 20:
				case 21:
					goto IL_91;
				case 17:
				case 22:
				case 24:
				case 26:
				case 27:
				case 29:
					return num2;
				case 23:
				case 30:
				case 31:
					goto IL_A1;
				case 25:
					if (regexNode2.ChildCount() > 0)
					{
						regexNode = regexNode2;
						num = 0;
						goto IL_A1;
					}
					goto IL_A1;
				case 28:
				case 32:
					regexNode2 = regexNode2.Child(0);
					regexNode = null;
					continue;
				}
				break;
				IL_A1:
				if (regexNode == null || num >= regexNode.ChildCount())
				{
					return num2;
				}
				regexNode2 = regexNode.Child(num++);
			}
			if (type != 41)
			{
				return num2;
			}
			IL_91:
			return num2 | RegexFCD.AnchorFromType(regexNode2._type);
		}

		// Token: 0x06003EB8 RID: 16056 RVA: 0x00105288 File Offset: 0x00103488
		private static int AnchorFromType(int type)
		{
			switch (type)
			{
			case 14:
				return 2;
			case 15:
				return 8;
			case 16:
				return 64;
			case 17:
				break;
			case 18:
				return 1;
			case 19:
				return 4;
			case 20:
				return 16;
			case 21:
				return 32;
			default:
				if (type == 41)
				{
					return 128;
				}
				break;
			}
			return 0;
		}

		// Token: 0x06003EB9 RID: 16057 RVA: 0x001052DD File Offset: 0x001034DD
		private RegexFCD()
		{
			this._fcStack = new RegexFC[32];
			this._intStack = new int[32];
		}

		// Token: 0x06003EBA RID: 16058 RVA: 0x00105300 File Offset: 0x00103500
		private void PushInt(int I)
		{
			if (this._intDepth >= this._intStack.Length)
			{
				int[] array = new int[this._intDepth * 2];
				Array.Copy(this._intStack, 0, array, 0, this._intDepth);
				this._intStack = array;
			}
			int[] intStack = this._intStack;
			int intDepth = this._intDepth;
			this._intDepth = intDepth + 1;
			intStack[intDepth] = I;
		}

		// Token: 0x06003EBB RID: 16059 RVA: 0x0010535F File Offset: 0x0010355F
		private bool IntIsEmpty()
		{
			return this._intDepth == 0;
		}

		// Token: 0x06003EBC RID: 16060 RVA: 0x0010536C File Offset: 0x0010356C
		private int PopInt()
		{
			int[] intStack = this._intStack;
			int num = this._intDepth - 1;
			this._intDepth = num;
			return intStack[num];
		}

		// Token: 0x06003EBD RID: 16061 RVA: 0x00105394 File Offset: 0x00103594
		private void PushFC(RegexFC fc)
		{
			if (this._fcDepth >= this._fcStack.Length)
			{
				RegexFC[] array = new RegexFC[this._fcDepth * 2];
				Array.Copy(this._fcStack, 0, array, 0, this._fcDepth);
				this._fcStack = array;
			}
			RegexFC[] fcStack = this._fcStack;
			int fcDepth = this._fcDepth;
			this._fcDepth = fcDepth + 1;
			fcStack[fcDepth] = fc;
		}

		// Token: 0x06003EBE RID: 16062 RVA: 0x001053F3 File Offset: 0x001035F3
		private bool FCIsEmpty()
		{
			return this._fcDepth == 0;
		}

		// Token: 0x06003EBF RID: 16063 RVA: 0x00105400 File Offset: 0x00103600
		private RegexFC PopFC()
		{
			RegexFC[] fcStack = this._fcStack;
			int num = this._fcDepth - 1;
			this._fcDepth = num;
			return fcStack[num];
		}

		// Token: 0x06003EC0 RID: 16064 RVA: 0x00105425 File Offset: 0x00103625
		private RegexFC TopFC()
		{
			return this._fcStack[this._fcDepth - 1];
		}

		// Token: 0x06003EC1 RID: 16065 RVA: 0x00105438 File Offset: 0x00103638
		private RegexFC RegexFCFromRegexTree(RegexTree tree)
		{
			RegexNode regexNode = tree._root;
			int num = 0;
			for (;;)
			{
				if (regexNode._children == null)
				{
					this.CalculateFC(regexNode._type, regexNode, 0);
				}
				else if (num < regexNode._children.Count && !this._skipAllChildren)
				{
					this.CalculateFC(regexNode._type | 64, regexNode, num);
					if (!this._skipchild)
					{
						regexNode = regexNode._children[num];
						this.PushInt(num);
						num = 0;
						continue;
					}
					num++;
					this._skipchild = false;
					continue;
				}
				this._skipAllChildren = false;
				if (this.IntIsEmpty())
				{
					goto IL_B9;
				}
				num = this.PopInt();
				regexNode = regexNode._next;
				this.CalculateFC(regexNode._type | 128, regexNode, num);
				if (this._failed)
				{
					break;
				}
				num++;
			}
			return null;
			IL_B9:
			if (this.FCIsEmpty())
			{
				return null;
			}
			return this.PopFC();
		}

		// Token: 0x06003EC2 RID: 16066 RVA: 0x0010550E File Offset: 0x0010370E
		private void SkipChild()
		{
			this._skipchild = true;
		}

		// Token: 0x06003EC3 RID: 16067 RVA: 0x00105518 File Offset: 0x00103718
		private void CalculateFC(int NodeType, RegexNode node, int CurIndex)
		{
			bool caseInsensitive = false;
			bool flag = false;
			if (NodeType <= 13)
			{
				if ((node._options & RegexOptions.IgnoreCase) != RegexOptions.None)
				{
					caseInsensitive = true;
				}
				if ((node._options & RegexOptions.RightToLeft) != RegexOptions.None)
				{
					flag = true;
				}
			}
			switch (NodeType)
			{
			case 3:
			case 6:
				this.PushFC(new RegexFC(node._ch, false, node._m == 0, caseInsensitive));
				return;
			case 4:
			case 7:
				this.PushFC(new RegexFC(node._ch, true, node._m == 0, caseInsensitive));
				return;
			case 5:
			case 8:
				this.PushFC(new RegexFC(node._str, node._m == 0, caseInsensitive));
				return;
			case 9:
			case 10:
				this.PushFC(new RegexFC(node._ch, NodeType == 10, false, caseInsensitive));
				return;
			case 11:
				this.PushFC(new RegexFC(node._str, false, caseInsensitive));
				return;
			case 12:
				if (node._str.Length == 0)
				{
					this.PushFC(new RegexFC(true));
					return;
				}
				if (!flag)
				{
					this.PushFC(new RegexFC(node._str[0], false, false, caseInsensitive));
					return;
				}
				this.PushFC(new RegexFC(node._str[node._str.Length - 1], false, false, caseInsensitive));
				return;
			case 13:
				this.PushFC(new RegexFC("\0\u0001\0\0", true, false));
				return;
			case 14:
			case 15:
			case 16:
			case 17:
			case 18:
			case 19:
			case 20:
			case 21:
			case 22:
			case 41:
			case 42:
				this.PushFC(new RegexFC(true));
				return;
			case 23:
				this.PushFC(new RegexFC(true));
				return;
			case 24:
			case 25:
			case 26:
			case 27:
			case 28:
			case 29:
			case 30:
			case 31:
			case 32:
			case 33:
			case 34:
			case 35:
			case 36:
			case 37:
			case 38:
			case 39:
			case 40:
				break;
			default:
				switch (NodeType)
				{
				case 88:
				case 89:
				case 90:
				case 91:
				case 92:
				case 93:
				case 96:
				case 97:
					break;
				case 94:
				case 95:
					this.SkipChild();
					this.PushFC(new RegexFC(true));
					return;
				case 98:
					if (CurIndex == 0)
					{
						this.SkipChild();
						return;
					}
					break;
				default:
					switch (NodeType)
					{
					case 152:
					case 161:
						if (CurIndex != 0)
						{
							RegexFC fc = this.PopFC();
							RegexFC regexFC = this.TopFC();
							this._failed = !regexFC.AddFC(fc, false);
							return;
						}
						break;
					case 153:
						if (CurIndex != 0)
						{
							RegexFC fc2 = this.PopFC();
							RegexFC regexFC2 = this.TopFC();
							this._failed = !regexFC2.AddFC(fc2, true);
						}
						if (!this.TopFC()._nullable)
						{
							this._skipAllChildren = true;
							return;
						}
						break;
					case 154:
					case 155:
						if (node._m == 0)
						{
							this.TopFC()._nullable = true;
							return;
						}
						break;
					case 156:
					case 157:
					case 158:
					case 159:
					case 160:
						break;
					case 162:
						if (CurIndex > 1)
						{
							RegexFC fc3 = this.PopFC();
							RegexFC regexFC3 = this.TopFC();
							this._failed = !regexFC3.AddFC(fc3, false);
							return;
						}
						break;
					default:
						goto IL_312;
					}
					break;
				}
				return;
			}
			IL_312:
			throw new ArgumentException(SR.GetString("UnexpectedOpcode", new object[]
			{
				NodeType.ToString(CultureInfo.CurrentCulture)
			}));
		}

		// Token: 0x04002DC7 RID: 11719
		private int[] _intStack;

		// Token: 0x04002DC8 RID: 11720
		private int _intDepth;

		// Token: 0x04002DC9 RID: 11721
		private RegexFC[] _fcStack;

		// Token: 0x04002DCA RID: 11722
		private int _fcDepth;

		// Token: 0x04002DCB RID: 11723
		private bool _skipAllChildren;

		// Token: 0x04002DCC RID: 11724
		private bool _skipchild;

		// Token: 0x04002DCD RID: 11725
		private bool _failed;

		// Token: 0x04002DCE RID: 11726
		private const int BeforeChild = 64;

		// Token: 0x04002DCF RID: 11727
		private const int AfterChild = 128;

		// Token: 0x04002DD0 RID: 11728
		internal const int Beginning = 1;

		// Token: 0x04002DD1 RID: 11729
		internal const int Bol = 2;

		// Token: 0x04002DD2 RID: 11730
		internal const int Start = 4;

		// Token: 0x04002DD3 RID: 11731
		internal const int Eol = 8;

		// Token: 0x04002DD4 RID: 11732
		internal const int EndZ = 16;

		// Token: 0x04002DD5 RID: 11733
		internal const int End = 32;

		// Token: 0x04002DD6 RID: 11734
		internal const int Boundary = 64;

		// Token: 0x04002DD7 RID: 11735
		internal const int ECMABoundary = 128;
	}
}
