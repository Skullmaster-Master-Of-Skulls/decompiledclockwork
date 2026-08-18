using System;
using System.Collections;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Calculation
{
	// Token: 0x020001B0 RID: 432
	public class CalcSheetList : ArrayList
	{
		// Token: 0x06001761 RID: 5985 RVA: 0x000E18C0 File Offset: 0x000E08C0
		public CalcSheetList()
		{
		}

		// Token: 0x06001762 RID: 5986 RVA: 0x000E18D4 File Offset: 0x000E08D4
		internal CalcSheetList(sprᦶ[] A_0, spr\u1AAC A_1)
		{
			if (A_0 != null)
			{
				foreach (sprᦶ value in A_0)
				{
					base.Add(value);
					CalcSheetList.ᜀ++;
				}
			}
			this.ᜁ = A_1;
		}

		// Token: 0x17000882 RID: 2178
		internal sprᦶ this[int A_0]
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return (sprᦶ)base[A_0];
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				base[A_0] = value;
			}
		}

		// Token: 0x17000883 RID: 2179
		internal sprᦶ this[string A_0]
		{
			get
			{
				int a_ = 9;
				int num = this.NameToIndex(A_0);
				if (num == -1)
				{
					for (;;)
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_2D;
						}
					}
					IL_2D:
					if (false)
					{
					}
					if (true)
					{
					}
					throw new ArgumentOutOfRangeException(string.Format(RecordTableEnumerator.b("䐾煀㹂敄⥆♈㽊浌⥎㹐♒㭔㍖睘", a_), A_0));
				}
				return (sprᦶ)base[num];
			}
			set
			{
				int a_ = 1;
				int num = this.NameToIndex(A_0);
				if (num == -1)
				{
					for (;;)
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_2D;
						}
					}
					IL_2D:
					if (false)
					{
					}
					if (true)
					{
					}
					throw new ArgumentOutOfRangeException(string.Format(RecordTableEnumerator.b("䰶स䘺ᴼ儾⹀㝂敄ⅆ♈㹊⍌⭎罐", a_), A_0));
				}
				base[num] = value;
			}
		}

		// Token: 0x06001767 RID: 5991 RVA: 0x000E1A98 File Offset: 0x000E0A98
		public new int Add(object o)
		{
			int a_ = 6;
			switch (0)
			{
			default:
			{
				sprᦶ sprᦶ;
				FormulaEngine formulaEngine;
				for (;;)
				{
					sprᦶ = (o as sprᦶ);
					int num = 6;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_7A;
						case 1:
							if (this.ᜁ.ᜃ == -1)
							{
								num = 3;
								continue;
							}
							goto IL_11D;
						case 2:
							goto IL_EC;
						case 3:
							this.ᜁ.ᜃ = FormulaEngine.ᜁ();
							num = 0;
							continue;
						case 4:
							goto IL_5F;
						case 5:
							if (this.Count == 0)
							{
								num = 7;
								continue;
							}
							formulaEngine = this[0].ᜃ;
							num = 2;
							continue;
						case 6:
							if (sprᦶ == null)
							{
								num = 4;
								continue;
							}
							formulaEngine = null;
							num = 5;
							continue;
						case 7:
							FormulaEngine.ᜀ();
							formulaEngine = new FormulaEngine(sprᦶ);
							formulaEngine.ᜀ.ᜇ(true);
							num = 1;
							continue;
						}
						break;
					}
				}
				IL_5F:
				goto IL_BF;
				IL_7A:
				goto IL_11D;
				IL_BF:
				throw new ArgumentException(RecordTableEnumerator.b("焻䬽㌿㙁摃❅ⱇ⹉汋⽍灏ᅑ㕓㩕㭗ख़㑛㭝՟ᙡ䑣॥੧i५൭ѯ", a_));
				IL_EC:
				if (true)
				{
				}
				IL_11D:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_BF;
				}
				if (false)
				{
				}
				sprᦶ.ᜃ = formulaEngine;
				string text = sprᦶ.ᜄ();
				formulaEngine.ᜀ.ᜀ(text, sprᦶ, this.ᜁ.ᜃ);
				int num2 = CalcSheetList.ᜀ;
				CalcSheetList.ᜀ++;
				this.ᜁ.ᜂ.Add(text.ToLower(), num2);
				return base.Add(sprᦶ);
			}
			}
		}

		// Token: 0x06001768 RID: 5992 RVA: 0x000E1C44 File Offset: 0x000E0C44
		public new void Insert(int index, object o)
		{
			int a_ = 15;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			throw new NotImplementedException(RecordTableEnumerator.b("ౄ⥆㩈⹊㽌㭎", a_));
		}

		// Token: 0x06001769 RID: 5993 RVA: 0x000E1C9C File Offset: 0x000E0C9C
		public new void InsertRange(int index, ICollection c)
		{
			int a_ = 2;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			throw new NotImplementedException(RecordTableEnumerator.b("焷吹伻嬽㈿㙁ᙃ❅♇ⵉ⥋", a_));
		}

		// Token: 0x0600176A RID: 5994 RVA: 0x000E1CF4 File Offset: 0x000E0CF4
		public int NameToIndex(string sheetName)
		{
			int result;
			for (;;)
			{
				result = -1;
				string b = sheetName.ToLower();
				int num = 0;
				int num2 = 0;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_A5;
					case 1:
						goto IL_6D;
					case 2:
						result = num;
						num2 = 1;
						continue;
					case 3:
						if (num >= this.Count)
						{
							num2 = 6;
							continue;
						}
						goto IL_77;
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_77;
						default:
							if (false)
							{
							}
							goto IL_A5;
						}
						break;
					case 5:
						if (this[num].ᜄ().ToLower() == b)
						{
							num2 = 2;
							continue;
						}
						num++;
						num2 = 4;
						continue;
					case 6:
						return result;
					}
					break;
					IL_77:
					num2 = 5;
					continue;
					IL_A5:
					num2 = 3;
				}
			}
			IL_6D:
			if (true)
			{
			}
			return result;
		}

		// Token: 0x0600176B RID: 5995 RVA: 0x000E1DD4 File Offset: 0x000E0DD4
		public new void Remove(object o)
		{
			int a_ = 11;
			for (;;)
			{
				sprᦶ sprᦶ = o as sprᦶ;
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_D9;
					case 1:
					{
						spr\u22D9 spr_u22D;
						spr_u22D.ᜆ.Remove(sprᦶ.ᜄ().ToUpper());
						num = 0;
						continue;
					}
					case 2:
					{
						if (true)
						{
						}
						if (sprᦶ == null)
						{
							goto IL_3F;
						}
						this.ᜁ.ᜂ.Remove(sprᦶ.ᜄ().ToLower());
						spr\u22D9 spr_u22D = FormulaEngine.ᜀ(sprᦶ);
						num = 3;
						continue;
					}
					case 3:
					{
						spr\u22D9 spr_u22D;
						if (spr_u22D.ᜆ.ContainsKey(sprᦶ.ᜄ().ToUpper()))
						{
							num = 1;
							continue;
						}
						goto IL_D9;
					}
					case 4:
						goto IL_47;
					}
					break;
					IL_3F:
					num = 4;
					continue;
					IL_D9:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_3F;
					default:
						goto IL_EF;
					}
				}
			}
			IL_47:
			throw new ArgumentException(RecordTableEnumerator.b("ీ㙂㙄㍆楈⩊⥌⭎煐㉒畔ᑖ㡘㝚㹜౞ॠ٢d፦䥨ѪཬծᑰၲŴ", a_));
			IL_EF:
			if (false)
			{
			}
			base.Remove(o);
		}

		// Token: 0x0600176C RID: 5996 RVA: 0x000E1EE0 File Offset: 0x000E0EE0
		public new void RemoveAt(int index)
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			sprᦶ o = this[index];
			this.Remove(o);
		}

		// Token: 0x0600176D RID: 5997 RVA: 0x000E1F2C File Offset: 0x000E0F2C
		internal sprᦶ[] ᜀ()
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			return (sprᦶ[])base.ToArray(typeof(sprᦶ));
		}

		// Token: 0x0600176E RID: 5998 RVA: 0x000E1F7C File Offset: 0x000E0F7C
		// Note: this type is marked as 'beforefieldinit'.
		static CalcSheetList()
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
		}

		// Token: 0x04000F9C RID: 3996
		private long \u25D8\u00A1\u009F\u0087;

		// Token: 0x04000F9D RID: 3997
		private static int ᜀ;

		// Token: 0x04000F9E RID: 3998
		private string \u25D9\u0086\u0090\u0089;

		// Token: 0x04000F9F RID: 3999
		private spr\u1AAC ᜁ;
	}
}
