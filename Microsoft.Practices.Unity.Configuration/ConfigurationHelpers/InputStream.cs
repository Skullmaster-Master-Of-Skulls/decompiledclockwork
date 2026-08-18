using System;

namespace Microsoft.Practices.Unity.Configuration.ConfigurationHelpers
{
	// Token: 0x0200000D RID: 13
	internal class InputStream
	{
		// Token: 0x06000034 RID: 52 RVA: 0x0000274D File Offset: 0x0000094D
		public InputStream(string input)
		{
			this.input = (input ?? string.Empty);
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002765 File Offset: 0x00000965
		public bool AtEnd
		{
			get
			{
				return this.currentPosition == this.input.Length;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000036 RID: 54 RVA: 0x0000277A File Offset: 0x0000097A
		public char CurrentChar
		{
			get
			{
				return this.input[this.currentPosition];
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000037 RID: 55 RVA: 0x0000278D File Offset: 0x0000098D
		public int CurrentPosition
		{
			get
			{
				return this.currentPosition;
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002795 File Offset: 0x00000995
		public void Consume(int numChars)
		{
			this.currentPosition = this.Clamp(this.currentPosition + numChars);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000027AB File Offset: 0x000009AB
		public void BackupTo(int bookmark)
		{
			this.currentPosition = this.Clamp(bookmark);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000027BA File Offset: 0x000009BA
		private int Clamp(int newPosition)
		{
			if (newPosition < 0)
			{
				newPosition = 0;
			}
			if (newPosition > this.input.Length)
			{
				newPosition = this.input.Length;
			}
			return newPosition;
		}

		// Token: 0x0400000A RID: 10
		private readonly string input;

		// Token: 0x0400000B RID: 11
		private int currentPosition;
	}
}
