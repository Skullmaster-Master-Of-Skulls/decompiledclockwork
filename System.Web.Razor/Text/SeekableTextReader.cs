using System;
using System.IO;

namespace System.Web.Razor.Text
{
	// Token: 0x02000064 RID: 100
	public class SeekableTextReader : TextReader, ITextDocument, ITextBuffer
	{
		// Token: 0x06000493 RID: 1171 RVA: 0x000121AA File Offset: 0x000103AA
		public SeekableTextReader(string content)
		{
			this._buffer.Append(content);
			this.UpdateState();
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x000121DA File Offset: 0x000103DA
		public SeekableTextReader(TextReader source) : this(source.ReadToEnd())
		{
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x000121E8 File Offset: 0x000103E8
		public SeekableTextReader(ITextBuffer buffer) : this(buffer.ReadToEnd())
		{
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000496 RID: 1174 RVA: 0x000121F6 File Offset: 0x000103F6
		public SourceLocation Location
		{
			get
			{
				return this._location;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000497 RID: 1175 RVA: 0x000121FE File Offset: 0x000103FE
		public int Length
		{
			get
			{
				return this._buffer.Length;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000498 RID: 1176 RVA: 0x0001220B File Offset: 0x0001040B
		// (set) Token: 0x06000499 RID: 1177 RVA: 0x00012213 File Offset: 0x00010413
		public int Position
		{
			get
			{
				return this._position;
			}
			set
			{
				if (this._position != value)
				{
					this._position = value;
					this.UpdateState();
				}
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600049A RID: 1178 RVA: 0x0001222B File Offset: 0x0001042B
		internal LineTrackingStringBuffer Buffer
		{
			get
			{
				return this._buffer;
			}
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x00012234 File Offset: 0x00010434
		public override int Read()
		{
			char? current = this._current;
			int? num = (current != null) ? new int?((int)current.GetValueOrDefault()) : null;
			if (num == null)
			{
				return -1;
			}
			char value = this._current.Value;
			this._position++;
			this.UpdateState();
			return (int)value;
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x00012298 File Offset: 0x00010498
		public override int Peek()
		{
			char? current = this._current;
			int? num = (current != null) ? new int?((int)current.GetValueOrDefault()) : null;
			if (num == null)
			{
				return -1;
			}
			return (int)this._current.Value;
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x000122E4 File Offset: 0x000104E4
		private void UpdateState()
		{
			if (this._position < this._buffer.Length)
			{
				LineTrackingStringBuffer.CharacterReference characterReference = this._buffer.CharAt(this._position);
				this._current = new char?(characterReference.Character);
				this._location = characterReference.Location;
				return;
			}
			if (this._buffer.Length == 0)
			{
				this._current = null;
				this._location = SourceLocation.Zero;
				return;
			}
			this._current = null;
			this._location = this._buffer.EndLocation;
		}

		// Token: 0x04000147 RID: 327
		private int _position;

		// Token: 0x04000148 RID: 328
		private LineTrackingStringBuffer _buffer = new LineTrackingStringBuffer();

		// Token: 0x04000149 RID: 329
		private SourceLocation _location = SourceLocation.Zero;

		// Token: 0x0400014A RID: 330
		private char? _current;
	}
}
