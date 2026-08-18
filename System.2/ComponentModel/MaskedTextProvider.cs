using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Security.Permissions;
using System.Text;

namespace System.ComponentModel
{
	// Token: 0x0200058E RID: 1422
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class MaskedTextProvider : ICloneable
	{
		// Token: 0x06003476 RID: 13430 RVA: 0x000E51F7 File Offset: 0x000E33F7
		public MaskedTextProvider(string mask) : this(mask, null, true, '_', '\0', false)
		{
		}

		// Token: 0x06003477 RID: 13431 RVA: 0x000E5206 File Offset: 0x000E3406
		public MaskedTextProvider(string mask, bool restrictToAscii) : this(mask, null, true, '_', '\0', restrictToAscii)
		{
		}

		// Token: 0x06003478 RID: 13432 RVA: 0x000E5215 File Offset: 0x000E3415
		public MaskedTextProvider(string mask, CultureInfo culture) : this(mask, culture, true, '_', '\0', false)
		{
		}

		// Token: 0x06003479 RID: 13433 RVA: 0x000E5224 File Offset: 0x000E3424
		public MaskedTextProvider(string mask, CultureInfo culture, bool restrictToAscii) : this(mask, culture, true, '_', '\0', restrictToAscii)
		{
		}

		// Token: 0x0600347A RID: 13434 RVA: 0x000E5233 File Offset: 0x000E3433
		public MaskedTextProvider(string mask, char passwordChar, bool allowPromptAsInput) : this(mask, null, allowPromptAsInput, '_', passwordChar, false)
		{
		}

		// Token: 0x0600347B RID: 13435 RVA: 0x000E5242 File Offset: 0x000E3442
		public MaskedTextProvider(string mask, CultureInfo culture, char passwordChar, bool allowPromptAsInput) : this(mask, culture, allowPromptAsInput, '_', passwordChar, false)
		{
		}

		// Token: 0x0600347C RID: 13436 RVA: 0x000E5254 File Offset: 0x000E3454
		public MaskedTextProvider(string mask, CultureInfo culture, bool allowPromptAsInput, char promptChar, char passwordChar, bool restrictToAscii)
		{
			if (string.IsNullOrEmpty(mask))
			{
				throw new ArgumentException(SR.GetString("MaskedTextProviderMaskNullOrEmpty"), "mask");
			}
			foreach (char c in mask)
			{
				if (!MaskedTextProvider.IsPrintableChar(c))
				{
					throw new ArgumentException(SR.GetString("MaskedTextProviderMaskInvalidChar"));
				}
			}
			if (culture == null)
			{
				culture = CultureInfo.CurrentCulture;
			}
			this.flagState = default(BitVector32);
			this.mask = mask;
			this.promptChar = promptChar;
			this.passwordChar = passwordChar;
			if (culture.IsNeutralCulture)
			{
				foreach (CultureInfo cultureInfo in CultureInfo.GetCultures(CultureTypes.SpecificCultures))
				{
					if (culture.Equals(cultureInfo.Parent))
					{
						this.culture = cultureInfo;
						break;
					}
				}
				if (this.culture == null)
				{
					this.culture = CultureInfo.InvariantCulture;
				}
			}
			else
			{
				this.culture = culture;
			}
			if (!this.culture.IsReadOnly)
			{
				this.culture = CultureInfo.ReadOnly(this.culture);
			}
			this.flagState[MaskedTextProvider.ALLOW_PROMPT_AS_INPUT] = allowPromptAsInput;
			this.flagState[MaskedTextProvider.ASCII_ONLY] = restrictToAscii;
			this.flagState[MaskedTextProvider.INCLUDE_PROMPT] = false;
			this.flagState[MaskedTextProvider.INCLUDE_LITERALS] = true;
			this.flagState[MaskedTextProvider.RESET_ON_PROMPT] = true;
			this.flagState[MaskedTextProvider.SKIP_SPACE] = true;
			this.flagState[MaskedTextProvider.RESET_ON_LITERALS] = true;
			this.Initialize();
		}

		// Token: 0x0600347D RID: 13437 RVA: 0x000E53DC File Offset: 0x000E35DC
		private void Initialize()
		{
			this.testString = new StringBuilder();
			this.stringDescriptor = new List<MaskedTextProvider.CharDescriptor>();
			MaskedTextProvider.CaseConversion caseConversion = MaskedTextProvider.CaseConversion.None;
			bool flag = false;
			int num = 0;
			MaskedTextProvider.CharType charType = MaskedTextProvider.CharType.Literal;
			string text = string.Empty;
			int i = 0;
			while (i < this.mask.Length)
			{
				char c = this.mask[i];
				if (!flag)
				{
					if (c <= 'C')
					{
						switch (c)
						{
						case '#':
							goto IL_19E;
						case '$':
							text = this.culture.NumberFormat.CurrencySymbol;
							charType = MaskedTextProvider.CharType.Separator;
							goto IL_1BE;
						case '%':
							goto IL_1B8;
						case '&':
							break;
						default:
							switch (c)
							{
							case ',':
								text = this.culture.NumberFormat.NumberGroupSeparator;
								charType = MaskedTextProvider.CharType.Separator;
								goto IL_1BE;
							case '-':
								goto IL_1B8;
							case '.':
								text = this.culture.NumberFormat.NumberDecimalSeparator;
								charType = MaskedTextProvider.CharType.Separator;
								goto IL_1BE;
							case '/':
								text = this.culture.DateTimeFormat.DateSeparator;
								charType = MaskedTextProvider.CharType.Separator;
								goto IL_1BE;
							case '0':
								break;
							default:
								switch (c)
								{
								case '9':
								case '?':
								case 'C':
									goto IL_19E;
								case ':':
									text = this.culture.DateTimeFormat.TimeSeparator;
									charType = MaskedTextProvider.CharType.Separator;
									goto IL_1BE;
								case ';':
								case '=':
								case '@':
								case 'B':
									goto IL_1B8;
								case '<':
									caseConversion = MaskedTextProvider.CaseConversion.ToLower;
									goto IL_22A;
								case '>':
									caseConversion = MaskedTextProvider.CaseConversion.ToUpper;
									goto IL_22A;
								case 'A':
									break;
								default:
									goto IL_1B8;
								}
								break;
							}
							break;
						}
					}
					else if (c <= '\\')
					{
						if (c != 'L')
						{
							if (c != '\\')
							{
								goto IL_1B8;
							}
							flag = true;
							charType = MaskedTextProvider.CharType.Literal;
							goto IL_22A;
						}
					}
					else
					{
						if (c == 'a')
						{
							goto IL_19E;
						}
						if (c != '|')
						{
							goto IL_1B8;
						}
						caseConversion = MaskedTextProvider.CaseConversion.None;
						goto IL_22A;
					}
					this.requiredEditChars++;
					c = this.promptChar;
					charType = MaskedTextProvider.CharType.EditRequired;
					goto IL_1BE;
					IL_19E:
					this.optionalEditChars++;
					c = this.promptChar;
					charType = MaskedTextProvider.CharType.EditOptional;
					goto IL_1BE;
					IL_1B8:
					charType = MaskedTextProvider.CharType.Literal;
					goto IL_1BE;
				}
				flag = false;
				goto IL_1BE;
				IL_22A:
				i++;
				continue;
				IL_1BE:
				MaskedTextProvider.CharDescriptor charDescriptor = new MaskedTextProvider.CharDescriptor(i, charType);
				if (MaskedTextProvider.IsEditPosition(charDescriptor))
				{
					charDescriptor.CaseConversion = caseConversion;
				}
				if (charType != MaskedTextProvider.CharType.Separator)
				{
					text = c.ToString();
				}
				foreach (char value in text)
				{
					this.testString.Append(value);
					this.stringDescriptor.Add(charDescriptor);
					num++;
				}
				goto IL_22A;
			}
			this.testString.Capacity = this.testString.Length;
		}

		// Token: 0x17000CD7 RID: 3287
		// (get) Token: 0x0600347E RID: 13438 RVA: 0x000E5641 File Offset: 0x000E3841
		public bool AllowPromptAsInput
		{
			get
			{
				return this.flagState[MaskedTextProvider.ALLOW_PROMPT_AS_INPUT];
			}
		}

		// Token: 0x17000CD8 RID: 3288
		// (get) Token: 0x0600347F RID: 13439 RVA: 0x000E5653 File Offset: 0x000E3853
		public int AssignedEditPositionCount
		{
			get
			{
				return this.assignedCharCount;
			}
		}

		// Token: 0x17000CD9 RID: 3289
		// (get) Token: 0x06003480 RID: 13440 RVA: 0x000E565B File Offset: 0x000E385B
		public int AvailableEditPositionCount
		{
			get
			{
				return this.EditPositionCount - this.assignedCharCount;
			}
		}

		// Token: 0x06003481 RID: 13441 RVA: 0x000E566C File Offset: 0x000E386C
		public object Clone()
		{
			Type type = base.GetType();
			MaskedTextProvider maskedTextProvider;
			if (type == MaskedTextProvider.maskTextProviderType)
			{
				maskedTextProvider = new MaskedTextProvider(this.Mask, this.Culture, this.AllowPromptAsInput, this.PromptChar, this.PasswordChar, this.AsciiOnly);
			}
			else
			{
				object[] args = new object[]
				{
					this.Mask,
					this.Culture,
					this.AllowPromptAsInput,
					this.PromptChar,
					this.PasswordChar,
					this.AsciiOnly
				};
				maskedTextProvider = (SecurityUtils.SecureCreateInstance(type, args) as MaskedTextProvider);
			}
			maskedTextProvider.ResetOnPrompt = false;
			maskedTextProvider.ResetOnSpace = false;
			maskedTextProvider.SkipLiterals = false;
			for (int i = 0; i < this.testString.Length; i++)
			{
				MaskedTextProvider.CharDescriptor charDescriptor = this.stringDescriptor[i];
				if (MaskedTextProvider.IsEditPosition(charDescriptor) && charDescriptor.IsAssigned)
				{
					maskedTextProvider.Replace(this.testString[i], i);
				}
			}
			maskedTextProvider.ResetOnPrompt = this.ResetOnPrompt;
			maskedTextProvider.ResetOnSpace = this.ResetOnSpace;
			maskedTextProvider.SkipLiterals = this.SkipLiterals;
			maskedTextProvider.IncludeLiterals = this.IncludeLiterals;
			maskedTextProvider.IncludePrompt = this.IncludePrompt;
			return maskedTextProvider;
		}

		// Token: 0x17000CDA RID: 3290
		// (get) Token: 0x06003482 RID: 13442 RVA: 0x000E57B3 File Offset: 0x000E39B3
		public CultureInfo Culture
		{
			get
			{
				return this.culture;
			}
		}

		// Token: 0x17000CDB RID: 3291
		// (get) Token: 0x06003483 RID: 13443 RVA: 0x000E57BB File Offset: 0x000E39BB
		public static char DefaultPasswordChar
		{
			get
			{
				return '*';
			}
		}

		// Token: 0x17000CDC RID: 3292
		// (get) Token: 0x06003484 RID: 13444 RVA: 0x000E57BF File Offset: 0x000E39BF
		public int EditPositionCount
		{
			get
			{
				return this.optionalEditChars + this.requiredEditChars;
			}
		}

		// Token: 0x17000CDD RID: 3293
		// (get) Token: 0x06003485 RID: 13445 RVA: 0x000E57D0 File Offset: 0x000E39D0
		public IEnumerator EditPositions
		{
			get
			{
				List<int> list = new List<int>();
				int num = 0;
				foreach (MaskedTextProvider.CharDescriptor charDescriptor in this.stringDescriptor)
				{
					if (MaskedTextProvider.IsEditPosition(charDescriptor))
					{
						list.Add(num);
					}
					num++;
				}
				return ((IEnumerable)list).GetEnumerator();
			}
		}

		// Token: 0x17000CDE RID: 3294
		// (get) Token: 0x06003486 RID: 13446 RVA: 0x000E5840 File Offset: 0x000E3A40
		// (set) Token: 0x06003487 RID: 13447 RVA: 0x000E5852 File Offset: 0x000E3A52
		public bool IncludeLiterals
		{
			get
			{
				return this.flagState[MaskedTextProvider.INCLUDE_LITERALS];
			}
			set
			{
				this.flagState[MaskedTextProvider.INCLUDE_LITERALS] = value;
			}
		}

		// Token: 0x17000CDF RID: 3295
		// (get) Token: 0x06003488 RID: 13448 RVA: 0x000E5865 File Offset: 0x000E3A65
		// (set) Token: 0x06003489 RID: 13449 RVA: 0x000E5877 File Offset: 0x000E3A77
		public bool IncludePrompt
		{
			get
			{
				return this.flagState[MaskedTextProvider.INCLUDE_PROMPT];
			}
			set
			{
				this.flagState[MaskedTextProvider.INCLUDE_PROMPT] = value;
			}
		}

		// Token: 0x17000CE0 RID: 3296
		// (get) Token: 0x0600348A RID: 13450 RVA: 0x000E588A File Offset: 0x000E3A8A
		public bool AsciiOnly
		{
			get
			{
				return this.flagState[MaskedTextProvider.ASCII_ONLY];
			}
		}

		// Token: 0x17000CE1 RID: 3297
		// (get) Token: 0x0600348B RID: 13451 RVA: 0x000E589C File Offset: 0x000E3A9C
		// (set) Token: 0x0600348C RID: 13452 RVA: 0x000E58A7 File Offset: 0x000E3AA7
		public bool IsPassword
		{
			get
			{
				return this.passwordChar > '\0';
			}
			set
			{
				if (this.IsPassword != value)
				{
					this.passwordChar = (value ? MaskedTextProvider.DefaultPasswordChar : '\0');
				}
			}
		}

		// Token: 0x17000CE2 RID: 3298
		// (get) Token: 0x0600348D RID: 13453 RVA: 0x000E58C3 File Offset: 0x000E3AC3
		public static int InvalidIndex
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x17000CE3 RID: 3299
		// (get) Token: 0x0600348E RID: 13454 RVA: 0x000E58C6 File Offset: 0x000E3AC6
		public int LastAssignedPosition
		{
			get
			{
				return this.FindAssignedEditPositionFrom(this.testString.Length - 1, false);
			}
		}

		// Token: 0x17000CE4 RID: 3300
		// (get) Token: 0x0600348F RID: 13455 RVA: 0x000E58DC File Offset: 0x000E3ADC
		public int Length
		{
			get
			{
				return this.testString.Length;
			}
		}

		// Token: 0x17000CE5 RID: 3301
		// (get) Token: 0x06003490 RID: 13456 RVA: 0x000E58E9 File Offset: 0x000E3AE9
		public string Mask
		{
			get
			{
				return this.mask;
			}
		}

		// Token: 0x17000CE6 RID: 3302
		// (get) Token: 0x06003491 RID: 13457 RVA: 0x000E58F1 File Offset: 0x000E3AF1
		public bool MaskCompleted
		{
			get
			{
				return this.requiredCharCount == this.requiredEditChars;
			}
		}

		// Token: 0x17000CE7 RID: 3303
		// (get) Token: 0x06003492 RID: 13458 RVA: 0x000E5901 File Offset: 0x000E3B01
		public bool MaskFull
		{
			get
			{
				return this.assignedCharCount == this.EditPositionCount;
			}
		}

		// Token: 0x17000CE8 RID: 3304
		// (get) Token: 0x06003493 RID: 13459 RVA: 0x000E5911 File Offset: 0x000E3B11
		// (set) Token: 0x06003494 RID: 13460 RVA: 0x000E591C File Offset: 0x000E3B1C
		public char PasswordChar
		{
			get
			{
				return this.passwordChar;
			}
			set
			{
				if (value == this.promptChar)
				{
					throw new InvalidOperationException(SR.GetString("MaskedTextProviderPasswordAndPromptCharError"));
				}
				if (!MaskedTextProvider.IsValidPasswordChar(value) && value != '\0')
				{
					throw new ArgumentException(SR.GetString("MaskedTextProviderInvalidCharError"));
				}
				if (value != this.passwordChar)
				{
					this.passwordChar = value;
				}
			}
		}

		// Token: 0x17000CE9 RID: 3305
		// (get) Token: 0x06003495 RID: 13461 RVA: 0x000E596D File Offset: 0x000E3B6D
		// (set) Token: 0x06003496 RID: 13462 RVA: 0x000E5978 File Offset: 0x000E3B78
		public char PromptChar
		{
			get
			{
				return this.promptChar;
			}
			set
			{
				if (value == this.passwordChar)
				{
					throw new InvalidOperationException(SR.GetString("MaskedTextProviderPasswordAndPromptCharError"));
				}
				if (!MaskedTextProvider.IsPrintableChar(value))
				{
					throw new ArgumentException(SR.GetString("MaskedTextProviderInvalidCharError"));
				}
				if (value != this.promptChar)
				{
					this.promptChar = value;
					for (int i = 0; i < this.testString.Length; i++)
					{
						MaskedTextProvider.CharDescriptor charDescriptor = this.stringDescriptor[i];
						if (this.IsEditPosition(i) && !charDescriptor.IsAssigned)
						{
							this.testString[i] = this.promptChar;
						}
					}
				}
			}
		}

		// Token: 0x17000CEA RID: 3306
		// (get) Token: 0x06003497 RID: 13463 RVA: 0x000E5A0C File Offset: 0x000E3C0C
		// (set) Token: 0x06003498 RID: 13464 RVA: 0x000E5A1E File Offset: 0x000E3C1E
		public bool ResetOnPrompt
		{
			get
			{
				return this.flagState[MaskedTextProvider.RESET_ON_PROMPT];
			}
			set
			{
				this.flagState[MaskedTextProvider.RESET_ON_PROMPT] = value;
			}
		}

		// Token: 0x17000CEB RID: 3307
		// (get) Token: 0x06003499 RID: 13465 RVA: 0x000E5A31 File Offset: 0x000E3C31
		// (set) Token: 0x0600349A RID: 13466 RVA: 0x000E5A43 File Offset: 0x000E3C43
		public bool ResetOnSpace
		{
			get
			{
				return this.flagState[MaskedTextProvider.SKIP_SPACE];
			}
			set
			{
				this.flagState[MaskedTextProvider.SKIP_SPACE] = value;
			}
		}

		// Token: 0x17000CEC RID: 3308
		// (get) Token: 0x0600349B RID: 13467 RVA: 0x000E5A56 File Offset: 0x000E3C56
		// (set) Token: 0x0600349C RID: 13468 RVA: 0x000E5A68 File Offset: 0x000E3C68
		public bool SkipLiterals
		{
			get
			{
				return this.flagState[MaskedTextProvider.RESET_ON_LITERALS];
			}
			set
			{
				this.flagState[MaskedTextProvider.RESET_ON_LITERALS] = value;
			}
		}

		// Token: 0x17000CED RID: 3309
		public char this[int index]
		{
			get
			{
				if (index < 0 || index >= this.testString.Length)
				{
					throw new IndexOutOfRangeException(index.ToString(CultureInfo.CurrentCulture));
				}
				return this.testString[index];
			}
		}

		// Token: 0x0600349E RID: 13470 RVA: 0x000E5AB0 File Offset: 0x000E3CB0
		public bool Add(char input)
		{
			int num;
			MaskedTextResultHint maskedTextResultHint;
			return this.Add(input, out num, out maskedTextResultHint);
		}

		// Token: 0x0600349F RID: 13471 RVA: 0x000E5AC8 File Offset: 0x000E3CC8
		public bool Add(char input, out int testPosition, out MaskedTextResultHint resultHint)
		{
			int lastAssignedPosition = this.LastAssignedPosition;
			if (lastAssignedPosition == this.testString.Length - 1)
			{
				testPosition = this.testString.Length;
				resultHint = MaskedTextResultHint.UnavailableEditPosition;
				return false;
			}
			testPosition = lastAssignedPosition + 1;
			testPosition = this.FindEditPositionFrom(testPosition, true);
			if (testPosition == -1)
			{
				resultHint = MaskedTextResultHint.UnavailableEditPosition;
				testPosition = this.testString.Length;
				return false;
			}
			return this.TestSetChar(input, testPosition, out resultHint);
		}

		// Token: 0x060034A0 RID: 13472 RVA: 0x000E5B38 File Offset: 0x000E3D38
		public bool Add(string input)
		{
			int num;
			MaskedTextResultHint maskedTextResultHint;
			return this.Add(input, out num, out maskedTextResultHint);
		}

		// Token: 0x060034A1 RID: 13473 RVA: 0x000E5B50 File Offset: 0x000E3D50
		public bool Add(string input, out int testPosition, out MaskedTextResultHint resultHint)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			testPosition = this.LastAssignedPosition + 1;
			if (input.Length == 0)
			{
				resultHint = MaskedTextResultHint.NoEffect;
				return true;
			}
			return this.TestSetString(input, testPosition, out testPosition, out resultHint);
		}

		// Token: 0x060034A2 RID: 13474 RVA: 0x000E5B84 File Offset: 0x000E3D84
		public void Clear()
		{
			MaskedTextResultHint maskedTextResultHint;
			this.Clear(out maskedTextResultHint);
		}

		// Token: 0x060034A3 RID: 13475 RVA: 0x000E5B9C File Offset: 0x000E3D9C
		public void Clear(out MaskedTextResultHint resultHint)
		{
			if (this.assignedCharCount == 0)
			{
				resultHint = MaskedTextResultHint.NoEffect;
				return;
			}
			resultHint = MaskedTextResultHint.Success;
			for (int i = 0; i < this.testString.Length; i++)
			{
				this.ResetChar(i);
			}
		}

		// Token: 0x060034A4 RID: 13476 RVA: 0x000E5BD8 File Offset: 0x000E3DD8
		public int FindAssignedEditPositionFrom(int position, bool direction)
		{
			if (this.assignedCharCount == 0)
			{
				return -1;
			}
			int startPosition;
			int endPosition;
			if (direction)
			{
				startPosition = position;
				endPosition = this.testString.Length - 1;
			}
			else
			{
				startPosition = 0;
				endPosition = position;
			}
			return this.FindAssignedEditPositionInRange(startPosition, endPosition, direction);
		}

		// Token: 0x060034A5 RID: 13477 RVA: 0x000E5C11 File Offset: 0x000E3E11
		public int FindAssignedEditPositionInRange(int startPosition, int endPosition, bool direction)
		{
			if (this.assignedCharCount == 0)
			{
				return -1;
			}
			return this.FindEditPositionInRange(startPosition, endPosition, direction, 2);
		}

		// Token: 0x060034A6 RID: 13478 RVA: 0x000E5C28 File Offset: 0x000E3E28
		public int FindEditPositionFrom(int position, bool direction)
		{
			int startPosition;
			int endPosition;
			if (direction)
			{
				startPosition = position;
				endPosition = this.testString.Length - 1;
			}
			else
			{
				startPosition = 0;
				endPosition = position;
			}
			return this.FindEditPositionInRange(startPosition, endPosition, direction);
		}

		// Token: 0x060034A7 RID: 13479 RVA: 0x000E5C58 File Offset: 0x000E3E58
		public int FindEditPositionInRange(int startPosition, int endPosition, bool direction)
		{
			MaskedTextProvider.CharType charTypeFlags = MaskedTextProvider.CharType.EditOptional | MaskedTextProvider.CharType.EditRequired;
			return this.FindPositionInRange(startPosition, endPosition, direction, charTypeFlags);
		}

		// Token: 0x060034A8 RID: 13480 RVA: 0x000E5C74 File Offset: 0x000E3E74
		private int FindEditPositionInRange(int startPosition, int endPosition, bool direction, byte assignedStatus)
		{
			int num;
			for (;;)
			{
				num = this.FindEditPositionInRange(startPosition, endPosition, direction);
				if (num == -1)
				{
					return -1;
				}
				MaskedTextProvider.CharDescriptor charDescriptor = this.stringDescriptor[num];
				if (assignedStatus != 1)
				{
					if (assignedStatus != 2)
					{
						break;
					}
					if (charDescriptor.IsAssigned)
					{
						return num;
					}
				}
				else if (!charDescriptor.IsAssigned)
				{
					return num;
				}
				if (direction)
				{
					startPosition++;
				}
				else
				{
					endPosition--;
				}
				if (startPosition > endPosition)
				{
					return -1;
				}
			}
			return num;
		}

		// Token: 0x060034A9 RID: 13481 RVA: 0x000E5CD4 File Offset: 0x000E3ED4
		public int FindNonEditPositionFrom(int position, bool direction)
		{
			int startPosition;
			int endPosition;
			if (direction)
			{
				startPosition = position;
				endPosition = this.testString.Length - 1;
			}
			else
			{
				startPosition = 0;
				endPosition = position;
			}
			return this.FindNonEditPositionInRange(startPosition, endPosition, direction);
		}

		// Token: 0x060034AA RID: 13482 RVA: 0x000E5D04 File Offset: 0x000E3F04
		public int FindNonEditPositionInRange(int startPosition, int endPosition, bool direction)
		{
			MaskedTextProvider.CharType charTypeFlags = MaskedTextProvider.CharType.Separator | MaskedTextProvider.CharType.Literal;
			return this.FindPositionInRange(startPosition, endPosition, direction, charTypeFlags);
		}

		// Token: 0x060034AB RID: 13483 RVA: 0x000E5D20 File Offset: 0x000E3F20
		private int FindPositionInRange(int startPosition, int endPosition, bool direction, MaskedTextProvider.CharType charTypeFlags)
		{
			if (startPosition < 0)
			{
				startPosition = 0;
			}
			if (endPosition >= this.testString.Length)
			{
				endPosition = this.testString.Length - 1;
			}
			if (startPosition > endPosition)
			{
				return -1;
			}
			while (startPosition <= endPosition)
			{
				int num;
				if (!direction)
				{
					endPosition = (num = endPosition) - 1;
				}
				else
				{
					startPosition = (num = startPosition) + 1;
				}
				int num2 = num;
				MaskedTextProvider.CharDescriptor charDescriptor = this.stringDescriptor[num2];
				if ((charDescriptor.CharType & charTypeFlags) == charDescriptor.CharType)
				{
					return num2;
				}
			}
			return -1;
		}

		// Token: 0x060034AC RID: 13484 RVA: 0x000E5D90 File Offset: 0x000E3F90
		public int FindUnassignedEditPositionFrom(int position, bool direction)
		{
			int startPosition;
			int endPosition;
			if (direction)
			{
				startPosition = position;
				endPosition = this.testString.Length - 1;
			}
			else
			{
				startPosition = 0;
				endPosition = position;
			}
			return this.FindEditPositionInRange(startPosition, endPosition, direction, 1);
		}

		// Token: 0x060034AD RID: 13485 RVA: 0x000E5DC0 File Offset: 0x000E3FC0
		public int FindUnassignedEditPositionInRange(int startPosition, int endPosition, bool direction)
		{
			for (;;)
			{
				int num = this.FindEditPositionInRange(startPosition, endPosition, direction, 0);
				if (num == -1)
				{
					break;
				}
				MaskedTextProvider.CharDescriptor charDescriptor = this.stringDescriptor[num];
				if (!charDescriptor.IsAssigned)
				{
					return num;
				}
				if (direction)
				{
					startPosition++;
				}
				else
				{
					endPosition--;
				}
			}
			return -1;
		}

		// Token: 0x060034AE RID: 13486 RVA: 0x000E5E05 File Offset: 0x000E4005
		public static bool GetOperationResultFromHint(MaskedTextResultHint hint)
		{
			return hint > MaskedTextResultHint.Unknown;
		}

		// Token: 0x060034AF RID: 13487 RVA: 0x000E5E0B File Offset: 0x000E400B
		public bool InsertAt(char input, int position)
		{
			return position >= 0 && position < this.testString.Length && this.InsertAt(input.ToString(), position);
		}

		// Token: 0x060034B0 RID: 13488 RVA: 0x000E5E2F File Offset: 0x000E402F
		public bool InsertAt(char input, int position, out int testPosition, out MaskedTextResultHint resultHint)
		{
			return this.InsertAt(input.ToString(), position, out testPosition, out resultHint);
		}

		// Token: 0x060034B1 RID: 13489 RVA: 0x000E5E44 File Offset: 0x000E4044
		public bool InsertAt(string input, int position)
		{
			int num;
			MaskedTextResultHint maskedTextResultHint;
			return this.InsertAt(input, position, out num, out maskedTextResultHint);
		}

		// Token: 0x060034B2 RID: 13490 RVA: 0x000E5E5D File Offset: 0x000E405D
		public bool InsertAt(string input, int position, out int testPosition, out MaskedTextResultHint resultHint)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			if (position < 0 || position >= this.testString.Length)
			{
				testPosition = position;
				resultHint = MaskedTextResultHint.PositionOutOfRange;
				return false;
			}
			return this.InsertAtInt(input, position, out testPosition, out resultHint, false);
		}

		// Token: 0x060034B3 RID: 13491 RVA: 0x000E5E98 File Offset: 0x000E4098
		private bool InsertAtInt(string input, int position, out int testPosition, out MaskedTextResultHint resultHint, bool testOnly)
		{
			if (input.Length == 0)
			{
				testPosition = position;
				resultHint = MaskedTextResultHint.NoEffect;
				return true;
			}
			if (!this.TestString(input, position, out testPosition, out resultHint))
			{
				return false;
			}
			int i = this.FindEditPositionFrom(position, true);
			bool flag = this.FindAssignedEditPositionInRange(i, testPosition, true) != -1;
			int lastAssignedPosition = this.LastAssignedPosition;
			if (flag && testPosition == this.testString.Length - 1)
			{
				resultHint = MaskedTextResultHint.UnavailableEditPosition;
				testPosition = this.testString.Length;
				return false;
			}
			int num = this.FindEditPositionFrom(testPosition + 1, true);
			if (flag)
			{
				MaskedTextResultHint maskedTextResultHint = MaskedTextResultHint.Unknown;
				while (num != -1)
				{
					MaskedTextProvider.CharDescriptor charDescriptor = this.stringDescriptor[i];
					if (charDescriptor.IsAssigned && !this.TestChar(this.testString[i], num, out maskedTextResultHint))
					{
						resultHint = maskedTextResultHint;
						testPosition = num;
						return false;
					}
					if (i != lastAssignedPosition)
					{
						i = this.FindEditPositionFrom(i + 1, true);
						num = this.FindEditPositionFrom(num + 1, true);
					}
					else
					{
						if (maskedTextResultHint > resultHint)
						{
							resultHint = maskedTextResultHint;
							goto IL_F3;
						}
						goto IL_F3;
					}
				}
				resultHint = MaskedTextResultHint.UnavailableEditPosition;
				testPosition = this.testString.Length;
				return false;
			}
			IL_F3:
			if (testOnly)
			{
				return true;
			}
			if (flag)
			{
				while (i >= position)
				{
					MaskedTextProvider.CharDescriptor charDescriptor2 = this.stringDescriptor[i];
					if (charDescriptor2.IsAssigned)
					{
						this.SetChar(this.testString[i], num);
					}
					else
					{
						this.ResetChar(num);
					}
					num = this.FindEditPositionFrom(num - 1, false);
					i = this.FindEditPositionFrom(i - 1, false);
				}
			}
			this.SetString(input, position);
			return true;
		}

		// Token: 0x060034B4 RID: 13492 RVA: 0x000E5FF9 File Offset: 0x000E41F9
		private static bool IsAscii(char c)
		{
			return c >= '!' && c <= '~';
		}

		// Token: 0x060034B5 RID: 13493 RVA: 0x000E600A File Offset: 0x000E420A
		private static bool IsAciiAlphanumeric(char c)
		{
			return (c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z');
		}

		// Token: 0x060034B6 RID: 13494 RVA: 0x000E6031 File Offset: 0x000E4231
		private static bool IsAlphanumeric(char c)
		{
			return char.IsLetter(c) || char.IsDigit(c);
		}

		// Token: 0x060034B7 RID: 13495 RVA: 0x000E6043 File Offset: 0x000E4243
		private static bool IsAsciiLetter(char c)
		{
			return (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z');
		}

		// Token: 0x060034B8 RID: 13496 RVA: 0x000E6060 File Offset: 0x000E4260
		public bool IsAvailablePosition(int position)
		{
			if (position < 0 || position >= this.testString.Length)
			{
				return false;
			}
			MaskedTextProvider.CharDescriptor charDescriptor = this.stringDescriptor[position];
			return MaskedTextProvider.IsEditPosition(charDescriptor) && !charDescriptor.IsAssigned;
		}

		// Token: 0x060034B9 RID: 13497 RVA: 0x000E60A4 File Offset: 0x000E42A4
		public bool IsEditPosition(int position)
		{
			if (position < 0 || position >= this.testString.Length)
			{
				return false;
			}
			MaskedTextProvider.CharDescriptor charDescriptor = this.stringDescriptor[position];
			return MaskedTextProvider.IsEditPosition(charDescriptor);
		}

		// Token: 0x060034BA RID: 13498 RVA: 0x000E60D8 File Offset: 0x000E42D8
		private static bool IsEditPosition(MaskedTextProvider.CharDescriptor charDescriptor)
		{
			return charDescriptor.CharType == MaskedTextProvider.CharType.EditRequired || charDescriptor.CharType == MaskedTextProvider.CharType.EditOptional;
		}

		// Token: 0x060034BB RID: 13499 RVA: 0x000E60EE File Offset: 0x000E42EE
		private static bool IsLiteralPosition(MaskedTextProvider.CharDescriptor charDescriptor)
		{
			return charDescriptor.CharType == MaskedTextProvider.CharType.Literal || charDescriptor.CharType == MaskedTextProvider.CharType.Separator;
		}

		// Token: 0x060034BC RID: 13500 RVA: 0x000E6104 File Offset: 0x000E4304
		private static bool IsPrintableChar(char c)
		{
			return char.IsLetterOrDigit(c) || char.IsPunctuation(c) || char.IsSymbol(c) || c == ' ';
		}

		// Token: 0x060034BD RID: 13501 RVA: 0x000E6125 File Offset: 0x000E4325
		public static bool IsValidInputChar(char c)
		{
			return MaskedTextProvider.IsPrintableChar(c);
		}

		// Token: 0x060034BE RID: 13502 RVA: 0x000E612D File Offset: 0x000E432D
		public static bool IsValidMaskChar(char c)
		{
			return MaskedTextProvider.IsPrintableChar(c);
		}

		// Token: 0x060034BF RID: 13503 RVA: 0x000E6135 File Offset: 0x000E4335
		public static bool IsValidPasswordChar(char c)
		{
			return MaskedTextProvider.IsPrintableChar(c) || c == '\0';
		}

		// Token: 0x060034C0 RID: 13504 RVA: 0x000E6148 File Offset: 0x000E4348
		public bool Remove()
		{
			int num;
			MaskedTextResultHint maskedTextResultHint;
			return this.Remove(out num, out maskedTextResultHint);
		}

		// Token: 0x060034C1 RID: 13505 RVA: 0x000E6160 File Offset: 0x000E4360
		public bool Remove(out int testPosition, out MaskedTextResultHint resultHint)
		{
			int lastAssignedPosition = this.LastAssignedPosition;
			if (lastAssignedPosition == -1)
			{
				testPosition = 0;
				resultHint = MaskedTextResultHint.NoEffect;
				return true;
			}
			this.ResetChar(lastAssignedPosition);
			testPosition = lastAssignedPosition;
			resultHint = MaskedTextResultHint.Success;
			return true;
		}

		// Token: 0x060034C2 RID: 13506 RVA: 0x000E618E File Offset: 0x000E438E
		public bool RemoveAt(int position)
		{
			return this.RemoveAt(position, position);
		}

		// Token: 0x060034C3 RID: 13507 RVA: 0x000E6198 File Offset: 0x000E4398
		public bool RemoveAt(int startPosition, int endPosition)
		{
			int num;
			MaskedTextResultHint maskedTextResultHint;
			return this.RemoveAt(startPosition, endPosition, out num, out maskedTextResultHint);
		}

		// Token: 0x060034C4 RID: 13508 RVA: 0x000E61B1 File Offset: 0x000E43B1
		public bool RemoveAt(int startPosition, int endPosition, out int testPosition, out MaskedTextResultHint resultHint)
		{
			if (endPosition >= this.testString.Length)
			{
				testPosition = endPosition;
				resultHint = MaskedTextResultHint.PositionOutOfRange;
				return false;
			}
			if (startPosition < 0 || startPosition > endPosition)
			{
				testPosition = startPosition;
				resultHint = MaskedTextResultHint.PositionOutOfRange;
				return false;
			}
			return this.RemoveAtInt(startPosition, endPosition, out testPosition, out resultHint, false);
		}

		// Token: 0x060034C5 RID: 13509 RVA: 0x000E61EC File Offset: 0x000E43EC
		private bool RemoveAtInt(int startPosition, int endPosition, out int testPosition, out MaskedTextResultHint resultHint, bool testOnly)
		{
			int lastAssignedPosition = this.LastAssignedPosition;
			int num = this.FindEditPositionInRange(startPosition, endPosition, true);
			resultHint = MaskedTextResultHint.NoEffect;
			if (num == -1 || num > lastAssignedPosition)
			{
				testPosition = startPosition;
				return true;
			}
			testPosition = startPosition;
			bool flag = endPosition < lastAssignedPosition;
			if (this.FindAssignedEditPositionInRange(startPosition, endPosition, true) != -1)
			{
				resultHint = MaskedTextResultHint.Success;
			}
			if (flag)
			{
				int num2 = this.FindEditPositionFrom(endPosition + 1, true);
				int num3 = num2;
				startPosition = num;
				MaskedTextResultHint maskedTextResultHint;
				for (;;)
				{
					char c = this.testString[num2];
					MaskedTextProvider.CharDescriptor charDescriptor = this.stringDescriptor[num2];
					if ((c != this.PromptChar || charDescriptor.IsAssigned) && !this.TestChar(c, num, out maskedTextResultHint))
					{
						break;
					}
					if (num2 == lastAssignedPosition)
					{
						goto IL_B3;
					}
					num2 = this.FindEditPositionFrom(num2 + 1, true);
					num = this.FindEditPositionFrom(num + 1, true);
				}
				resultHint = maskedTextResultHint;
				testPosition = num;
				return false;
				IL_B3:
				if (MaskedTextResultHint.SideEffect > resultHint)
				{
					resultHint = MaskedTextResultHint.SideEffect;
				}
				if (testOnly)
				{
					return true;
				}
				num2 = num3;
				num = startPosition;
				for (;;)
				{
					char c2 = this.testString[num2];
					MaskedTextProvider.CharDescriptor charDescriptor2 = this.stringDescriptor[num2];
					if (c2 == this.PromptChar && !charDescriptor2.IsAssigned)
					{
						this.ResetChar(num);
					}
					else
					{
						this.SetChar(c2, num);
						this.ResetChar(num2);
					}
					if (num2 == lastAssignedPosition)
					{
						break;
					}
					num2 = this.FindEditPositionFrom(num2 + 1, true);
					num = this.FindEditPositionFrom(num + 1, true);
				}
				startPosition = num + 1;
			}
			if (startPosition <= endPosition)
			{
				this.ResetString(startPosition, endPosition);
			}
			return true;
		}

		// Token: 0x060034C6 RID: 13510 RVA: 0x000E6338 File Offset: 0x000E4538
		public bool Replace(char input, int position)
		{
			int num;
			MaskedTextResultHint maskedTextResultHint;
			return this.Replace(input, position, out num, out maskedTextResultHint);
		}

		// Token: 0x060034C7 RID: 13511 RVA: 0x000E6354 File Offset: 0x000E4554
		public bool Replace(char input, int position, out int testPosition, out MaskedTextResultHint resultHint)
		{
			if (position < 0 || position >= this.testString.Length)
			{
				testPosition = position;
				resultHint = MaskedTextResultHint.PositionOutOfRange;
				return false;
			}
			testPosition = position;
			if (!this.TestEscapeChar(input, testPosition))
			{
				testPosition = this.FindEditPositionFrom(testPosition, true);
			}
			if (testPosition == -1)
			{
				resultHint = MaskedTextResultHint.UnavailableEditPosition;
				testPosition = position;
				return false;
			}
			return this.TestSetChar(input, testPosition, out resultHint);
		}

		// Token: 0x060034C8 RID: 13512 RVA: 0x000E63B8 File Offset: 0x000E45B8
		public bool Replace(char input, int startPosition, int endPosition, out int testPosition, out MaskedTextResultHint resultHint)
		{
			if (endPosition >= this.testString.Length)
			{
				testPosition = endPosition;
				resultHint = MaskedTextResultHint.PositionOutOfRange;
				return false;
			}
			if (startPosition < 0 || startPosition > endPosition)
			{
				testPosition = startPosition;
				resultHint = MaskedTextResultHint.PositionOutOfRange;
				return false;
			}
			if (startPosition == endPosition)
			{
				testPosition = startPosition;
				return this.TestSetChar(input, startPosition, out resultHint);
			}
			return this.Replace(input.ToString(), startPosition, endPosition, out testPosition, out resultHint);
		}

		// Token: 0x060034C9 RID: 13513 RVA: 0x000E6418 File Offset: 0x000E4618
		public bool Replace(string input, int position)
		{
			int num;
			MaskedTextResultHint maskedTextResultHint;
			return this.Replace(input, position, out num, out maskedTextResultHint);
		}

		// Token: 0x060034CA RID: 13514 RVA: 0x000E6434 File Offset: 0x000E4634
		public bool Replace(string input, int position, out int testPosition, out MaskedTextResultHint resultHint)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			if (position < 0 || position >= this.testString.Length)
			{
				testPosition = position;
				resultHint = MaskedTextResultHint.PositionOutOfRange;
				return false;
			}
			if (input.Length == 0)
			{
				return this.RemoveAt(position, position, out testPosition, out resultHint);
			}
			return this.TestSetString(input, position, out testPosition, out resultHint);
		}

		// Token: 0x060034CB RID: 13515 RVA: 0x000E6490 File Offset: 0x000E4690
		public bool Replace(string input, int startPosition, int endPosition, out int testPosition, out MaskedTextResultHint resultHint)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			if (endPosition >= this.testString.Length)
			{
				testPosition = endPosition;
				resultHint = MaskedTextResultHint.PositionOutOfRange;
				return false;
			}
			if (startPosition < 0 || startPosition > endPosition)
			{
				testPosition = startPosition;
				resultHint = MaskedTextResultHint.PositionOutOfRange;
				return false;
			}
			if (input.Length == 0)
			{
				return this.RemoveAt(startPosition, endPosition, out testPosition, out resultHint);
			}
			if (!this.TestString(input, startPosition, out testPosition, out resultHint))
			{
				return false;
			}
			if (this.assignedCharCount > 0)
			{
				if (testPosition < endPosition)
				{
					int num;
					MaskedTextResultHint maskedTextResultHint;
					if (!this.RemoveAtInt(testPosition + 1, endPosition, out num, out maskedTextResultHint, false))
					{
						testPosition = num;
						resultHint = maskedTextResultHint;
						return false;
					}
					if (maskedTextResultHint == MaskedTextResultHint.Success && resultHint != maskedTextResultHint)
					{
						resultHint = MaskedTextResultHint.SideEffect;
					}
				}
				else if (testPosition > endPosition)
				{
					int lastAssignedPosition = this.LastAssignedPosition;
					int i = testPosition + 1;
					int num2 = endPosition + 1;
					MaskedTextResultHint maskedTextResultHint;
					for (;;)
					{
						num2 = this.FindEditPositionFrom(num2, true);
						i = this.FindEditPositionFrom(i, true);
						if (i == -1)
						{
							goto Block_12;
						}
						if (!this.TestChar(this.testString[num2], i, out maskedTextResultHint))
						{
							goto Block_13;
						}
						if (maskedTextResultHint == MaskedTextResultHint.Success && resultHint != maskedTextResultHint)
						{
							resultHint = MaskedTextResultHint.Success;
						}
						if (num2 == lastAssignedPosition)
						{
							break;
						}
						num2++;
						i++;
					}
					while (i > testPosition)
					{
						this.SetChar(this.testString[num2], i);
						num2 = this.FindEditPositionFrom(num2 - 1, false);
						i = this.FindEditPositionFrom(i - 1, false);
					}
					goto IL_162;
					Block_12:
					testPosition = this.testString.Length;
					resultHint = MaskedTextResultHint.UnavailableEditPosition;
					return false;
					Block_13:
					testPosition = i;
					resultHint = maskedTextResultHint;
					return false;
				}
			}
			IL_162:
			this.SetString(input, startPosition);
			return true;
		}

		// Token: 0x060034CC RID: 13516 RVA: 0x000E6608 File Offset: 0x000E4808
		private void ResetChar(int testPosition)
		{
			MaskedTextProvider.CharDescriptor charDescriptor = this.stringDescriptor[testPosition];
			if (this.IsEditPosition(testPosition) && charDescriptor.IsAssigned)
			{
				charDescriptor.IsAssigned = false;
				this.testString[testPosition] = this.promptChar;
				this.assignedCharCount--;
				if (charDescriptor.CharType == MaskedTextProvider.CharType.EditRequired)
				{
					this.requiredCharCount--;
				}
			}
		}

		// Token: 0x060034CD RID: 13517 RVA: 0x000E6671 File Offset: 0x000E4871
		private void ResetString(int startPosition, int endPosition)
		{
			startPosition = this.FindAssignedEditPositionFrom(startPosition, true);
			if (startPosition != -1)
			{
				endPosition = this.FindAssignedEditPositionFrom(endPosition, false);
				while (startPosition <= endPosition)
				{
					startPosition = this.FindAssignedEditPositionFrom(startPosition, true);
					this.ResetChar(startPosition);
					startPosition++;
				}
			}
		}

		// Token: 0x060034CE RID: 13518 RVA: 0x000E66A8 File Offset: 0x000E48A8
		public bool Set(string input)
		{
			int num;
			MaskedTextResultHint maskedTextResultHint;
			return this.Set(input, out num, out maskedTextResultHint);
		}

		// Token: 0x060034CF RID: 13519 RVA: 0x000E66C0 File Offset: 0x000E48C0
		public bool Set(string input, out int testPosition, out MaskedTextResultHint resultHint)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			resultHint = MaskedTextResultHint.Unknown;
			testPosition = 0;
			if (input.Length == 0)
			{
				this.Clear(out resultHint);
				return true;
			}
			if (!this.TestSetString(input, testPosition, out testPosition, out resultHint))
			{
				return false;
			}
			int num = this.FindAssignedEditPositionFrom(testPosition + 1, true);
			if (num != -1)
			{
				this.ResetString(num, this.testString.Length - 1);
			}
			return true;
		}

		// Token: 0x060034D0 RID: 13520 RVA: 0x000E6728 File Offset: 0x000E4928
		private void SetChar(char input, int position)
		{
			MaskedTextProvider.CharDescriptor charDescriptor = this.stringDescriptor[position];
			this.SetChar(input, position, charDescriptor);
		}

		// Token: 0x060034D1 RID: 13521 RVA: 0x000E674C File Offset: 0x000E494C
		private void SetChar(char input, int position, MaskedTextProvider.CharDescriptor charDescriptor)
		{
			MaskedTextProvider.CharDescriptor charDescriptor2 = this.stringDescriptor[position];
			if (this.TestEscapeChar(input, position, charDescriptor))
			{
				this.ResetChar(position);
				return;
			}
			if (char.IsLetter(input))
			{
				if (char.IsUpper(input))
				{
					if (charDescriptor.CaseConversion == MaskedTextProvider.CaseConversion.ToLower)
					{
						input = this.culture.TextInfo.ToLower(input);
					}
				}
				else if (charDescriptor.CaseConversion == MaskedTextProvider.CaseConversion.ToUpper)
				{
					input = this.culture.TextInfo.ToUpper(input);
				}
			}
			this.testString[position] = input;
			if (!charDescriptor.IsAssigned)
			{
				charDescriptor.IsAssigned = true;
				this.assignedCharCount++;
				if (charDescriptor.CharType == MaskedTextProvider.CharType.EditRequired)
				{
					this.requiredCharCount++;
				}
			}
		}

		// Token: 0x060034D2 RID: 13522 RVA: 0x000E6804 File Offset: 0x000E4A04
		private void SetString(string input, int testPosition)
		{
			foreach (char input2 in input)
			{
				if (!this.TestEscapeChar(input2, testPosition))
				{
					testPosition = this.FindEditPositionFrom(testPosition, true);
				}
				this.SetChar(input2, testPosition);
				testPosition++;
			}
		}

		// Token: 0x060034D3 RID: 13523 RVA: 0x000E6850 File Offset: 0x000E4A50
		private bool TestChar(char input, int position, out MaskedTextResultHint resultHint)
		{
			if (!MaskedTextProvider.IsPrintableChar(input))
			{
				resultHint = MaskedTextResultHint.InvalidInput;
				return false;
			}
			MaskedTextProvider.CharDescriptor charDescriptor = this.stringDescriptor[position];
			if (MaskedTextProvider.IsLiteralPosition(charDescriptor))
			{
				if (this.SkipLiterals && input == this.testString[position])
				{
					resultHint = MaskedTextResultHint.CharacterEscaped;
					return true;
				}
				resultHint = MaskedTextResultHint.NonEditPosition;
				return false;
			}
			else
			{
				if (input == this.promptChar)
				{
					if (this.ResetOnPrompt)
					{
						if (MaskedTextProvider.IsEditPosition(charDescriptor) && charDescriptor.IsAssigned)
						{
							resultHint = MaskedTextResultHint.SideEffect;
						}
						else
						{
							resultHint = MaskedTextResultHint.CharacterEscaped;
						}
						return true;
					}
					if (!this.AllowPromptAsInput)
					{
						resultHint = MaskedTextResultHint.PromptCharNotAllowed;
						return false;
					}
				}
				if (input == ' ' && this.ResetOnSpace)
				{
					if (MaskedTextProvider.IsEditPosition(charDescriptor) && charDescriptor.IsAssigned)
					{
						resultHint = MaskedTextResultHint.SideEffect;
					}
					else
					{
						resultHint = MaskedTextResultHint.CharacterEscaped;
					}
					return true;
				}
				char c = this.mask[charDescriptor.MaskPosition];
				if (c <= '0')
				{
					if (c != '#')
					{
						if (c != '&')
						{
							if (c == '0')
							{
								if (!char.IsDigit(input))
								{
									resultHint = MaskedTextResultHint.DigitExpected;
									return false;
								}
							}
						}
						else if (!MaskedTextProvider.IsAscii(input) && this.AsciiOnly)
						{
							resultHint = MaskedTextResultHint.AsciiCharacterExpected;
							return false;
						}
					}
					else if (!char.IsDigit(input) && input != '-' && input != '+' && input != ' ')
					{
						resultHint = MaskedTextResultHint.DigitExpected;
						return false;
					}
				}
				else if (c <= 'C')
				{
					if (c != '9')
					{
						switch (c)
						{
						case '?':
							if (!char.IsLetter(input) && input != ' ')
							{
								resultHint = MaskedTextResultHint.LetterExpected;
								return false;
							}
							if (!MaskedTextProvider.IsAsciiLetter(input) && this.AsciiOnly)
							{
								resultHint = MaskedTextResultHint.AsciiCharacterExpected;
								return false;
							}
							break;
						case 'A':
							if (!MaskedTextProvider.IsAlphanumeric(input))
							{
								resultHint = MaskedTextResultHint.AlphanumericCharacterExpected;
								return false;
							}
							if (!MaskedTextProvider.IsAciiAlphanumeric(input) && this.AsciiOnly)
							{
								resultHint = MaskedTextResultHint.AsciiCharacterExpected;
								return false;
							}
							break;
						case 'C':
							if (!MaskedTextProvider.IsAscii(input) && this.AsciiOnly && input != ' ')
							{
								resultHint = MaskedTextResultHint.AsciiCharacterExpected;
								return false;
							}
							break;
						}
					}
					else if (!char.IsDigit(input) && input != ' ')
					{
						resultHint = MaskedTextResultHint.DigitExpected;
						return false;
					}
				}
				else if (c != 'L')
				{
					if (c == 'a')
					{
						if (!MaskedTextProvider.IsAlphanumeric(input) && input != ' ')
						{
							resultHint = MaskedTextResultHint.AlphanumericCharacterExpected;
							return false;
						}
						if (!MaskedTextProvider.IsAciiAlphanumeric(input) && this.AsciiOnly)
						{
							resultHint = MaskedTextResultHint.AsciiCharacterExpected;
							return false;
						}
					}
				}
				else
				{
					if (!char.IsLetter(input))
					{
						resultHint = MaskedTextResultHint.LetterExpected;
						return false;
					}
					if (!MaskedTextProvider.IsAsciiLetter(input) && this.AsciiOnly)
					{
						resultHint = MaskedTextResultHint.AsciiCharacterExpected;
						return false;
					}
				}
				if (input == this.testString[position] && charDescriptor.IsAssigned)
				{
					resultHint = MaskedTextResultHint.NoEffect;
				}
				else
				{
					resultHint = MaskedTextResultHint.Success;
				}
				return true;
			}
		}

		// Token: 0x060034D4 RID: 13524 RVA: 0x000E6AB0 File Offset: 0x000E4CB0
		private bool TestEscapeChar(char input, int position)
		{
			MaskedTextProvider.CharDescriptor charDex = this.stringDescriptor[position];
			return this.TestEscapeChar(input, position, charDex);
		}

		// Token: 0x060034D5 RID: 13525 RVA: 0x000E6AD4 File Offset: 0x000E4CD4
		private bool TestEscapeChar(char input, int position, MaskedTextProvider.CharDescriptor charDex)
		{
			if (MaskedTextProvider.IsLiteralPosition(charDex))
			{
				return this.SkipLiterals && input == this.testString[position];
			}
			return (this.ResetOnPrompt && input == this.promptChar) || (this.ResetOnSpace && input == ' ');
		}

		// Token: 0x060034D6 RID: 13526 RVA: 0x000E6B24 File Offset: 0x000E4D24
		private bool TestSetChar(char input, int position, out MaskedTextResultHint resultHint)
		{
			if (this.TestChar(input, position, out resultHint))
			{
				if (resultHint == MaskedTextResultHint.Success || resultHint == MaskedTextResultHint.SideEffect)
				{
					this.SetChar(input, position);
				}
				return true;
			}
			return false;
		}

		// Token: 0x060034D7 RID: 13527 RVA: 0x000E6B46 File Offset: 0x000E4D46
		private bool TestSetString(string input, int position, out int testPosition, out MaskedTextResultHint resultHint)
		{
			if (this.TestString(input, position, out testPosition, out resultHint))
			{
				this.SetString(input, position);
				return true;
			}
			return false;
		}

		// Token: 0x060034D8 RID: 13528 RVA: 0x000E6B60 File Offset: 0x000E4D60
		private bool TestString(string input, int position, out int testPosition, out MaskedTextResultHint resultHint)
		{
			resultHint = MaskedTextResultHint.Unknown;
			testPosition = position;
			if (input.Length == 0)
			{
				return true;
			}
			MaskedTextResultHint maskedTextResultHint = resultHint;
			foreach (char input2 in input)
			{
				if (testPosition >= this.testString.Length)
				{
					resultHint = MaskedTextResultHint.UnavailableEditPosition;
					return false;
				}
				if (!this.TestEscapeChar(input2, testPosition))
				{
					testPosition = this.FindEditPositionFrom(testPosition, true);
					if (testPosition == -1)
					{
						testPosition = this.testString.Length;
						resultHint = MaskedTextResultHint.UnavailableEditPosition;
						return false;
					}
				}
				if (!this.TestChar(input2, testPosition, out maskedTextResultHint))
				{
					resultHint = maskedTextResultHint;
					return false;
				}
				if (maskedTextResultHint > resultHint)
				{
					resultHint = maskedTextResultHint;
				}
				testPosition++;
			}
			testPosition--;
			return true;
		}

		// Token: 0x060034D9 RID: 13529 RVA: 0x000E6C0C File Offset: 0x000E4E0C
		public string ToDisplayString()
		{
			if (!this.IsPassword || this.assignedCharCount == 0)
			{
				return this.testString.ToString();
			}
			StringBuilder stringBuilder = new StringBuilder(this.testString.Length);
			for (int i = 0; i < this.testString.Length; i++)
			{
				MaskedTextProvider.CharDescriptor charDescriptor = this.stringDescriptor[i];
				stringBuilder.Append((MaskedTextProvider.IsEditPosition(charDescriptor) && charDescriptor.IsAssigned) ? this.passwordChar : this.testString[i]);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060034DA RID: 13530 RVA: 0x000E6C9A File Offset: 0x000E4E9A
		public override string ToString()
		{
			return this.ToString(true, this.IncludePrompt, this.IncludeLiterals, 0, this.testString.Length);
		}

		// Token: 0x060034DB RID: 13531 RVA: 0x000E6CBB File Offset: 0x000E4EBB
		public string ToString(bool ignorePasswordChar)
		{
			return this.ToString(ignorePasswordChar, this.IncludePrompt, this.IncludeLiterals, 0, this.testString.Length);
		}

		// Token: 0x060034DC RID: 13532 RVA: 0x000E6CDC File Offset: 0x000E4EDC
		public string ToString(int startPosition, int length)
		{
			return this.ToString(true, this.IncludePrompt, this.IncludeLiterals, startPosition, length);
		}

		// Token: 0x060034DD RID: 13533 RVA: 0x000E6CF3 File Offset: 0x000E4EF3
		public string ToString(bool ignorePasswordChar, int startPosition, int length)
		{
			return this.ToString(ignorePasswordChar, this.IncludePrompt, this.IncludeLiterals, startPosition, length);
		}

		// Token: 0x060034DE RID: 13534 RVA: 0x000E6D0A File Offset: 0x000E4F0A
		public string ToString(bool includePrompt, bool includeLiterals)
		{
			return this.ToString(true, includePrompt, includeLiterals, 0, this.testString.Length);
		}

		// Token: 0x060034DF RID: 13535 RVA: 0x000E6D21 File Offset: 0x000E4F21
		public string ToString(bool includePrompt, bool includeLiterals, int startPosition, int length)
		{
			return this.ToString(true, includePrompt, includeLiterals, startPosition, length);
		}

		// Token: 0x060034E0 RID: 13536 RVA: 0x000E6D30 File Offset: 0x000E4F30
		public string ToString(bool ignorePasswordChar, bool includePrompt, bool includeLiterals, int startPosition, int length)
		{
			if (length <= 0)
			{
				return string.Empty;
			}
			if (startPosition < 0)
			{
				startPosition = 0;
			}
			if (startPosition >= this.testString.Length)
			{
				return string.Empty;
			}
			int num = this.testString.Length - startPosition;
			if (length > num)
			{
				length = num;
			}
			if ((!this.IsPassword || ignorePasswordChar) && (includePrompt && includeLiterals))
			{
				return this.testString.ToString(startPosition, length);
			}
			StringBuilder stringBuilder = new StringBuilder();
			int num2 = startPosition + length - 1;
			if (!includePrompt)
			{
				int num3 = includeLiterals ? this.FindNonEditPositionInRange(startPosition, num2, false) : MaskedTextProvider.InvalidIndex;
				int num4 = this.FindAssignedEditPositionInRange((num3 == MaskedTextProvider.InvalidIndex) ? startPosition : num3, num2, false);
				num2 = ((num4 != MaskedTextProvider.InvalidIndex) ? num4 : num3);
				if (num2 == MaskedTextProvider.InvalidIndex)
				{
					return string.Empty;
				}
			}
			int i = startPosition;
			while (i <= num2)
			{
				char value = this.testString[i];
				MaskedTextProvider.CharDescriptor charDescriptor = this.stringDescriptor[i];
				MaskedTextProvider.CharType charType = charDescriptor.CharType;
				if (charType - MaskedTextProvider.CharType.EditOptional > 1)
				{
					if (charType != MaskedTextProvider.CharType.Separator && charType != MaskedTextProvider.CharType.Literal)
					{
						goto IL_12F;
					}
					if (includeLiterals)
					{
						goto IL_12F;
					}
				}
				else if (charDescriptor.IsAssigned)
				{
					if (!this.IsPassword || ignorePasswordChar)
					{
						goto IL_12F;
					}
					stringBuilder.Append(this.passwordChar);
				}
				else
				{
					if (includePrompt)
					{
						goto IL_12F;
					}
					stringBuilder.Append(' ');
				}
				IL_138:
				i++;
				continue;
				IL_12F:
				stringBuilder.Append(value);
				goto IL_138;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060034E1 RID: 13537 RVA: 0x000E6E89 File Offset: 0x000E5089
		public bool VerifyChar(char input, int position, out MaskedTextResultHint hint)
		{
			hint = MaskedTextResultHint.NoEffect;
			if (position < 0 || position >= this.testString.Length)
			{
				hint = MaskedTextResultHint.PositionOutOfRange;
				return false;
			}
			return this.TestChar(input, position, out hint);
		}

		// Token: 0x060034E2 RID: 13538 RVA: 0x000E6EAF File Offset: 0x000E50AF
		public bool VerifyEscapeChar(char input, int position)
		{
			return position >= 0 && position < this.testString.Length && this.TestEscapeChar(input, position);
		}

		// Token: 0x060034E3 RID: 13539 RVA: 0x000E6ED0 File Offset: 0x000E50D0
		public bool VerifyString(string input)
		{
			int num;
			MaskedTextResultHint maskedTextResultHint;
			return this.VerifyString(input, out num, out maskedTextResultHint);
		}

		// Token: 0x060034E4 RID: 13540 RVA: 0x000E6EE8 File Offset: 0x000E50E8
		public bool VerifyString(string input, out int testPosition, out MaskedTextResultHint resultHint)
		{
			testPosition = 0;
			if (input == null || input.Length == 0)
			{
				resultHint = MaskedTextResultHint.NoEffect;
				return true;
			}
			return this.TestString(input, 0, out testPosition, out resultHint);
		}

		// Token: 0x040029FB RID: 10747
		private const char spaceChar = ' ';

		// Token: 0x040029FC RID: 10748
		private const char defaultPromptChar = '_';

		// Token: 0x040029FD RID: 10749
		private const char nullPasswordChar = '\0';

		// Token: 0x040029FE RID: 10750
		private const bool defaultAllowPrompt = true;

		// Token: 0x040029FF RID: 10751
		private const int invalidIndex = -1;

		// Token: 0x04002A00 RID: 10752
		private const byte editAny = 0;

		// Token: 0x04002A01 RID: 10753
		private const byte editUnassigned = 1;

		// Token: 0x04002A02 RID: 10754
		private const byte editAssigned = 2;

		// Token: 0x04002A03 RID: 10755
		private const bool forward = true;

		// Token: 0x04002A04 RID: 10756
		private const bool backward = false;

		// Token: 0x04002A05 RID: 10757
		private static int ASCII_ONLY = BitVector32.CreateMask();

		// Token: 0x04002A06 RID: 10758
		private static int ALLOW_PROMPT_AS_INPUT = BitVector32.CreateMask(MaskedTextProvider.ASCII_ONLY);

		// Token: 0x04002A07 RID: 10759
		private static int INCLUDE_PROMPT = BitVector32.CreateMask(MaskedTextProvider.ALLOW_PROMPT_AS_INPUT);

		// Token: 0x04002A08 RID: 10760
		private static int INCLUDE_LITERALS = BitVector32.CreateMask(MaskedTextProvider.INCLUDE_PROMPT);

		// Token: 0x04002A09 RID: 10761
		private static int RESET_ON_PROMPT = BitVector32.CreateMask(MaskedTextProvider.INCLUDE_LITERALS);

		// Token: 0x04002A0A RID: 10762
		private static int RESET_ON_LITERALS = BitVector32.CreateMask(MaskedTextProvider.RESET_ON_PROMPT);

		// Token: 0x04002A0B RID: 10763
		private static int SKIP_SPACE = BitVector32.CreateMask(MaskedTextProvider.RESET_ON_LITERALS);

		// Token: 0x04002A0C RID: 10764
		private static Type maskTextProviderType = typeof(MaskedTextProvider);

		// Token: 0x04002A0D RID: 10765
		private BitVector32 flagState;

		// Token: 0x04002A0E RID: 10766
		private CultureInfo culture;

		// Token: 0x04002A0F RID: 10767
		private StringBuilder testString;

		// Token: 0x04002A10 RID: 10768
		private int assignedCharCount;

		// Token: 0x04002A11 RID: 10769
		private int requiredCharCount;

		// Token: 0x04002A12 RID: 10770
		private int requiredEditChars;

		// Token: 0x04002A13 RID: 10771
		private int optionalEditChars;

		// Token: 0x04002A14 RID: 10772
		private string mask;

		// Token: 0x04002A15 RID: 10773
		private char passwordChar;

		// Token: 0x04002A16 RID: 10774
		private char promptChar;

		// Token: 0x04002A17 RID: 10775
		private List<MaskedTextProvider.CharDescriptor> stringDescriptor;

		// Token: 0x02000897 RID: 2199
		private enum CaseConversion
		{
			// Token: 0x040037DA RID: 14298
			None,
			// Token: 0x040037DB RID: 14299
			ToLower,
			// Token: 0x040037DC RID: 14300
			ToUpper
		}

		// Token: 0x02000898 RID: 2200
		[Flags]
		private enum CharType
		{
			// Token: 0x040037DE RID: 14302
			EditOptional = 1,
			// Token: 0x040037DF RID: 14303
			EditRequired = 2,
			// Token: 0x040037E0 RID: 14304
			Separator = 4,
			// Token: 0x040037E1 RID: 14305
			Literal = 8,
			// Token: 0x040037E2 RID: 14306
			Modifier = 16
		}

		// Token: 0x02000899 RID: 2201
		private class CharDescriptor
		{
			// Token: 0x060045A8 RID: 17832 RVA: 0x001235FD File Offset: 0x001217FD
			public CharDescriptor(int maskPos, MaskedTextProvider.CharType charType)
			{
				this.MaskPosition = maskPos;
				this.CharType = charType;
			}

			// Token: 0x060045A9 RID: 17833 RVA: 0x00123614 File Offset: 0x00121814
			public override string ToString()
			{
				return string.Format(CultureInfo.InvariantCulture, "MaskPosition[{0}] <CaseConversion.{1}><CharType.{2}><IsAssigned: {3}", new object[]
				{
					this.MaskPosition,
					this.CaseConversion,
					this.CharType,
					this.IsAssigned
				});
			}

			// Token: 0x040037E3 RID: 14307
			public int MaskPosition;

			// Token: 0x040037E4 RID: 14308
			public MaskedTextProvider.CaseConversion CaseConversion;

			// Token: 0x040037E5 RID: 14309
			public MaskedTextProvider.CharType CharType;

			// Token: 0x040037E6 RID: 14310
			public bool IsAssigned;
		}
	}
}
