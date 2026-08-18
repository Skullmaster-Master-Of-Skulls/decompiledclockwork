using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace System.Text.RegularExpressions
{
	// Token: 0x020006A8 RID: 1704
	internal sealed class RegexWriter
	{
		// Token: 0x06003FC5 RID: 16325 RVA: 0x0010C13C File Offset: 0x0010A33C
		internal static RegexCode Write(RegexTree t)
		{
			RegexWriter regexWriter = new RegexWriter();
			return regexWriter.RegexCodeFromRegexTree(t);
		}

		// Token: 0x06003FC6 RID: 16326 RVA: 0x0010C158 File Offset: 0x0010A358
		private RegexWriter()
		{
			this._intStack = new int[32];
			this._emitted = new int[32];
			this._stringhash = new Dictionary<string, int>();
			this._stringtable = new List<string>();
		}

		// Token: 0x06003FC7 RID: 16327 RVA: 0x0010C190 File Offset: 0x0010A390
		internal void PushInt(int I)
		{
			if (this._depth >= this._intStack.Length)
			{
				int[] array = new int[this._depth * 2];
				Array.Copy(this._intStack, 0, array, 0, this._depth);
				this._intStack = array;
			}
			int[] intStack = this._intStack;
			int depth = this._depth;
			this._depth = depth + 1;
			intStack[depth] = I;
		}

		// Token: 0x06003FC8 RID: 16328 RVA: 0x0010C1EF File Offset: 0x0010A3EF
		internal bool EmptyStack()
		{
			return this._depth == 0;
		}

		// Token: 0x06003FC9 RID: 16329 RVA: 0x0010C1FC File Offset: 0x0010A3FC
		internal int PopInt()
		{
			int[] intStack = this._intStack;
			int num = this._depth - 1;
			this._depth = num;
			return intStack[num];
		}

		// Token: 0x06003FCA RID: 16330 RVA: 0x0010C221 File Offset: 0x0010A421
		internal int CurPos()
		{
			return this._curpos;
		}

		// Token: 0x06003FCB RID: 16331 RVA: 0x0010C229 File Offset: 0x0010A429
		internal void PatchJump(int Offset, int jumpDest)
		{
			this._emitted[Offset + 1] = jumpDest;
		}

		// Token: 0x06003FCC RID: 16332 RVA: 0x0010C238 File Offset: 0x0010A438
		internal void Emit(int op)
		{
			if (this._counting)
			{
				this._count++;
				if (RegexCode.OpcodeBacktracks(op))
				{
					this._trackcount++;
				}
				return;
			}
			int[] emitted = this._emitted;
			int curpos = this._curpos;
			this._curpos = curpos + 1;
			emitted[curpos] = op;
		}

		// Token: 0x06003FCD RID: 16333 RVA: 0x0010C28C File Offset: 0x0010A48C
		internal void Emit(int op, int opd1)
		{
			if (this._counting)
			{
				this._count += 2;
				if (RegexCode.OpcodeBacktracks(op))
				{
					this._trackcount++;
				}
				return;
			}
			int[] emitted = this._emitted;
			int curpos = this._curpos;
			this._curpos = curpos + 1;
			emitted[curpos] = op;
			int[] emitted2 = this._emitted;
			curpos = this._curpos;
			this._curpos = curpos + 1;
			emitted2[curpos] = opd1;
		}

		// Token: 0x06003FCE RID: 16334 RVA: 0x0010C2F8 File Offset: 0x0010A4F8
		internal void Emit(int op, int opd1, int opd2)
		{
			if (this._counting)
			{
				this._count += 3;
				if (RegexCode.OpcodeBacktracks(op))
				{
					this._trackcount++;
				}
				return;
			}
			int[] emitted = this._emitted;
			int curpos = this._curpos;
			this._curpos = curpos + 1;
			emitted[curpos] = op;
			int[] emitted2 = this._emitted;
			curpos = this._curpos;
			this._curpos = curpos + 1;
			emitted2[curpos] = opd1;
			int[] emitted3 = this._emitted;
			curpos = this._curpos;
			this._curpos = curpos + 1;
			emitted3[curpos] = opd2;
		}

		// Token: 0x06003FCF RID: 16335 RVA: 0x0010C380 File Offset: 0x0010A580
		internal int StringCode(string str)
		{
			if (this._counting)
			{
				return 0;
			}
			if (str == null)
			{
				str = string.Empty;
			}
			int num;
			if (this._stringhash.ContainsKey(str))
			{
				num = this._stringhash[str];
			}
			else
			{
				num = this._stringtable.Count;
				this._stringhash[str] = num;
				this._stringtable.Add(str);
			}
			return num;
		}

		// Token: 0x06003FD0 RID: 16336 RVA: 0x0010C3E4 File Offset: 0x0010A5E4
		internal ArgumentException MakeException(string message)
		{
			return new ArgumentException(message);
		}

		// Token: 0x06003FD1 RID: 16337 RVA: 0x0010C3EC File Offset: 0x0010A5EC
		internal int MapCapnum(int capnum)
		{
			if (capnum == -1)
			{
				return -1;
			}
			if (this._caps != null)
			{
				return (int)this._caps[capnum];
			}
			return capnum;
		}

		// Token: 0x06003FD2 RID: 16338 RVA: 0x0010C414 File Offset: 0x0010A614
		internal RegexCode RegexCodeFromRegexTree(RegexTree tree)
		{
			int capsize;
			if (tree._capnumlist == null || tree._captop == tree._capnumlist.Length)
			{
				capsize = tree._captop;
				this._caps = null;
			}
			else
			{
				capsize = tree._capnumlist.Length;
				this._caps = tree._caps;
				for (int i = 0; i < tree._capnumlist.Length; i++)
				{
					this._caps[tree._capnumlist[i]] = i;
				}
			}
			this._counting = true;
			for (;;)
			{
				if (!this._counting)
				{
					this._emitted = new int[this._count];
				}
				RegexNode regexNode = tree._root;
				int num = 0;
				this.Emit(23, 0);
				for (;;)
				{
					if (regexNode._children == null)
					{
						this.EmitFragment(regexNode._type, regexNode, 0);
					}
					else if (num < regexNode._children.Count)
					{
						this.EmitFragment(regexNode._type | 64, regexNode, num);
						regexNode = regexNode._children[num];
						this.PushInt(num);
						num = 0;
						continue;
					}
					if (this.EmptyStack())
					{
						break;
					}
					num = this.PopInt();
					regexNode = regexNode._next;
					this.EmitFragment(regexNode._type | 128, regexNode, num);
					num++;
				}
				this.PatchJump(0, this.CurPos());
				this.Emit(40);
				if (!this._counting)
				{
					break;
				}
				this._counting = false;
			}
			RegexPrefix fcPrefix = RegexFCD.FirstChars(tree);
			RegexPrefix regexPrefix = RegexFCD.Prefix(tree);
			bool rightToLeft = (tree._options & RegexOptions.RightToLeft) > RegexOptions.None;
			CultureInfo culture = ((tree._options & RegexOptions.CultureInvariant) != RegexOptions.None) ? CultureInfo.InvariantCulture : CultureInfo.CurrentCulture;
			RegexBoyerMoore bmPrefix;
			if (regexPrefix != null && regexPrefix.Prefix.Length > 0)
			{
				bmPrefix = new RegexBoyerMoore(regexPrefix.Prefix, regexPrefix.CaseInsensitive, rightToLeft, culture);
			}
			else
			{
				bmPrefix = null;
			}
			int anchors = RegexFCD.Anchors(tree);
			return new RegexCode(this._emitted, this._stringtable, this._trackcount, this._caps, capsize, bmPrefix, fcPrefix, anchors, rightToLeft);
		}

		// Token: 0x06003FD3 RID: 16339 RVA: 0x0010C608 File Offset: 0x0010A808
		internal void EmitFragment(int nodetype, RegexNode node, int CurIndex)
		{
			int num = 0;
			if (nodetype <= 13)
			{
				if (node.UseOptionR())
				{
					num |= 64;
				}
				if ((node._options & RegexOptions.IgnoreCase) != RegexOptions.None)
				{
					num |= 512;
				}
			}
			switch (nodetype)
			{
			case 3:
			case 4:
			case 6:
			case 7:
				if (node._m > 0)
				{
					this.Emit(((node._type == 3 || node._type == 6) ? 0 : 1) | num, (int)node._ch, node._m);
				}
				if (node._n > node._m)
				{
					this.Emit(node._type | num, (int)node._ch, (node._n == int.MaxValue) ? int.MaxValue : (node._n - node._m));
					return;
				}
				return;
			case 5:
			case 8:
				if (node._m > 0)
				{
					this.Emit(2 | num, this.StringCode(node._str), node._m);
				}
				if (node._n > node._m)
				{
					this.Emit(node._type | num, this.StringCode(node._str), (node._n == int.MaxValue) ? int.MaxValue : (node._n - node._m));
					return;
				}
				return;
			case 9:
			case 10:
				this.Emit(node._type | num, (int)node._ch);
				return;
			case 11:
				this.Emit(node._type | num, this.StringCode(node._str));
				return;
			case 12:
				this.Emit(node._type | num, this.StringCode(node._str));
				return;
			case 13:
				this.Emit(node._type | num, this.MapCapnum(node._m));
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
				this.Emit(node._type);
				return;
			case 23:
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
				switch (nodetype)
				{
				case 88:
					if (CurIndex < node._children.Count - 1)
					{
						this.PushInt(this.CurPos());
						this.Emit(23, 0);
						return;
					}
					return;
				case 89:
				case 93:
					return;
				case 90:
				case 91:
					if (node._n < 2147483647 || node._m > 1)
					{
						this.Emit((node._m == 0) ? 26 : 27, (node._m == 0) ? 0 : (1 - node._m));
					}
					else
					{
						this.Emit((node._m == 0) ? 30 : 31);
					}
					if (node._m == 0)
					{
						this.PushInt(this.CurPos());
						this.Emit(38, 0);
					}
					this.PushInt(this.CurPos());
					return;
				case 92:
					this.Emit(31);
					return;
				case 94:
					this.Emit(34);
					this.Emit(31);
					return;
				case 95:
					this.Emit(34);
					this.PushInt(this.CurPos());
					this.Emit(23, 0);
					return;
				case 96:
					this.Emit(34);
					return;
				case 97:
					if (CurIndex == 0)
					{
						this.Emit(34);
						this.PushInt(this.CurPos());
						this.Emit(23, 0);
						this.Emit(37, this.MapCapnum(node._m));
						this.Emit(36);
						return;
					}
					return;
				case 98:
					if (CurIndex == 0)
					{
						this.Emit(34);
						this.Emit(31);
						this.PushInt(this.CurPos());
						this.Emit(23, 0);
						return;
					}
					return;
				default:
					switch (nodetype)
					{
					case 152:
						if (CurIndex < node._children.Count - 1)
						{
							int offset = this.PopInt();
							this.PushInt(this.CurPos());
							this.Emit(38, 0);
							this.PatchJump(offset, this.CurPos());
							return;
						}
						for (int i = 0; i < CurIndex; i++)
						{
							this.PatchJump(this.PopInt(), this.CurPos());
						}
						return;
					case 153:
					case 157:
						return;
					case 154:
					case 155:
					{
						int jumpDest = this.CurPos();
						int num2 = nodetype - 154;
						if (node._n < 2147483647 || node._m > 1)
						{
							this.Emit(28 + num2, this.PopInt(), (node._n == int.MaxValue) ? int.MaxValue : (node._n - node._m));
						}
						else
						{
							this.Emit(24 + num2, this.PopInt());
						}
						if (node._m == 0)
						{
							this.PatchJump(this.PopInt(), jumpDest);
							return;
						}
						return;
					}
					case 156:
						this.Emit(32, this.MapCapnum(node._m), this.MapCapnum(node._n));
						return;
					case 158:
						this.Emit(33);
						this.Emit(36);
						return;
					case 159:
						this.Emit(35);
						this.PatchJump(this.PopInt(), this.CurPos());
						this.Emit(36);
						return;
					case 160:
						this.Emit(36);
						return;
					case 161:
						if (CurIndex != 0)
						{
							if (CurIndex != 1)
							{
								return;
							}
						}
						else
						{
							int offset2 = this.PopInt();
							this.PushInt(this.CurPos());
							this.Emit(38, 0);
							this.PatchJump(offset2, this.CurPos());
							this.Emit(36);
							if (node._children.Count > 1)
							{
								return;
							}
						}
						this.PatchJump(this.PopInt(), this.CurPos());
						return;
					case 162:
						switch (CurIndex)
						{
						case 0:
							this.Emit(33);
							this.Emit(36);
							return;
						case 1:
						{
							int offset3 = this.PopInt();
							this.PushInt(this.CurPos());
							this.Emit(38, 0);
							this.PatchJump(offset3, this.CurPos());
							this.Emit(33);
							this.Emit(36);
							if (node._children.Count > 2)
							{
								return;
							}
							break;
						}
						case 2:
							break;
						default:
							return;
						}
						this.PatchJump(this.PopInt(), this.CurPos());
						return;
					}
					break;
				}
				break;
			}
			throw this.MakeException(SR.GetString("UnexpectedOpcode", new object[]
			{
				nodetype.ToString(CultureInfo.CurrentCulture)
			}));
		}

		// Token: 0x04002E82 RID: 11906
		internal int[] _intStack;

		// Token: 0x04002E83 RID: 11907
		internal int _depth;

		// Token: 0x04002E84 RID: 11908
		internal int[] _emitted;

		// Token: 0x04002E85 RID: 11909
		internal int _curpos;

		// Token: 0x04002E86 RID: 11910
		internal Dictionary<string, int> _stringhash;

		// Token: 0x04002E87 RID: 11911
		internal List<string> _stringtable;

		// Token: 0x04002E88 RID: 11912
		internal bool _counting;

		// Token: 0x04002E89 RID: 11913
		internal int _count;

		// Token: 0x04002E8A RID: 11914
		internal int _trackcount;

		// Token: 0x04002E8B RID: 11915
		internal Hashtable _caps;

		// Token: 0x04002E8C RID: 11916
		internal const int BeforeChild = 64;

		// Token: 0x04002E8D RID: 11917
		internal const int AfterChild = 128;
	}
}
