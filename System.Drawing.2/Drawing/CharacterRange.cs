using System;

namespace System.Drawing
{
	// Token: 0x02000046 RID: 70
	public struct CharacterRange
	{
		// Token: 0x060006B0 RID: 1712 RVA: 0x0001B6BE File Offset: 0x000198BE
		public CharacterRange(int First, int Length)
		{
			this.first = First;
			this.length = Length;
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x060006B1 RID: 1713 RVA: 0x0001B6CE File Offset: 0x000198CE
		// (set) Token: 0x060006B2 RID: 1714 RVA: 0x0001B6D6 File Offset: 0x000198D6
		public int First
		{
			get
			{
				return this.first;
			}
			set
			{
				this.first = value;
			}
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x060006B3 RID: 1715 RVA: 0x0001B6DF File Offset: 0x000198DF
		// (set) Token: 0x060006B4 RID: 1716 RVA: 0x0001B6E7 File Offset: 0x000198E7
		public int Length
		{
			get
			{
				return this.length;
			}
			set
			{
				this.length = value;
			}
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x0001B6F0 File Offset: 0x000198F0
		public override bool Equals(object obj)
		{
			if (obj.GetType() != typeof(CharacterRange))
			{
				return false;
			}
			CharacterRange characterRange = (CharacterRange)obj;
			return this.first == characterRange.First && this.length == characterRange.Length;
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x0001B73D File Offset: 0x0001993D
		public static bool operator ==(CharacterRange cr1, CharacterRange cr2)
		{
			return cr1.First == cr2.First && cr1.Length == cr2.Length;
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x0001B761 File Offset: 0x00019961
		public static bool operator !=(CharacterRange cr1, CharacterRange cr2)
		{
			return !(cr1 == cr2);
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x0001B76D File Offset: 0x0001996D
		public override int GetHashCode()
		{
			return this.first << 8 + this.length;
		}

		// Token: 0x04000582 RID: 1410
		private int first;

		// Token: 0x04000583 RID: 1411
		private int length;
	}
}
