using System;
using System.Text;

namespace WebGrease.Css
{
	// Token: 0x0200019A RID: 410
	internal sealed class PrinterFormatter
	{
		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x06001510 RID: 5392 RVA: 0x0007A690 File Offset: 0x00078890
		// (set) Token: 0x06001511 RID: 5393 RVA: 0x0007A698 File Offset: 0x00078898
		public bool PrettyPrint { get; set; }

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x06001512 RID: 5394 RVA: 0x0007A6A1 File Offset: 0x000788A1
		// (set) Token: 0x06001513 RID: 5395 RVA: 0x0007A6A9 File Offset: 0x000788A9
		public char IndentCharacter { get; set; }

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x06001514 RID: 5396 RVA: 0x0007A6B2 File Offset: 0x000788B2
		// (set) Token: 0x06001515 RID: 5397 RVA: 0x0007A6BA File Offset: 0x000788BA
		public int IndentSize { get; set; }

		// Token: 0x06001516 RID: 5398 RVA: 0x0007A6C3 File Offset: 0x000788C3
		public override string ToString()
		{
			return this._buffer.ToString();
		}

		// Token: 0x06001517 RID: 5399 RVA: 0x0007A6D0 File Offset: 0x000788D0
		public void Append(string content)
		{
			this._buffer.Append(content);
		}

		// Token: 0x06001518 RID: 5400 RVA: 0x0007A6DF File Offset: 0x000788DF
		public void Append(char content)
		{
			this._buffer.Append(content);
		}

		// Token: 0x06001519 RID: 5401 RVA: 0x0007A6EE File Offset: 0x000788EE
		public void AppendLine(char content)
		{
			if (this.PrettyPrint)
			{
				this._buffer.AppendLine(content.ToString());
				return;
			}
			this._buffer.Append(content);
		}

		// Token: 0x0600151A RID: 5402 RVA: 0x0007A719 File Offset: 0x00078919
		public void AppendLine()
		{
			if (this.PrettyPrint)
			{
				this._buffer.AppendLine();
			}
		}

		// Token: 0x0600151B RID: 5403 RVA: 0x0007A72F File Offset: 0x0007892F
		public void Remove(int startIndex, int length)
		{
			this._buffer.Remove(startIndex, length);
		}

		// Token: 0x0600151C RID: 5404 RVA: 0x0007A73F File Offset: 0x0007893F
		public int Length()
		{
			return this._buffer.Length;
		}

		// Token: 0x0600151D RID: 5405 RVA: 0x0007A74C File Offset: 0x0007894C
		public void IncrementIndentLevel()
		{
			this._indentLevel++;
		}

		// Token: 0x0600151E RID: 5406 RVA: 0x0007A75C File Offset: 0x0007895C
		public void DecrementIndentLevel()
		{
			if (this._indentLevel > 0)
			{
				this._indentLevel--;
			}
		}

		// Token: 0x0600151F RID: 5407 RVA: 0x0007A778 File Offset: 0x00078978
		public void WriteIndent()
		{
			if (!this.PrettyPrint)
			{
				return;
			}
			string value = new string(this.IndentCharacter, this._indentLevel * this.IndentSize);
			this._buffer.Append(value);
		}

		// Token: 0x04000B52 RID: 2898
		private readonly StringBuilder _buffer = new StringBuilder(1024);

		// Token: 0x04000B53 RID: 2899
		private int _indentLevel;
	}
}
