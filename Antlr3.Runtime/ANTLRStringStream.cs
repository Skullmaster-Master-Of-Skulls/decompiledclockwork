using System;
using System.Collections.Generic;

namespace Antlr.Runtime
{
	// Token: 0x02000004 RID: 4
	[Serializable]
	public class ANTLRStringStream : ICharStream, IIntStream
	{
		// Token: 0x06000011 RID: 17 RVA: 0x000020D0 File Offset: 0x000002D0
		public ANTLRStringStream(string input) : this(input, null)
		{
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000020DA File Offset: 0x000002DA
		public ANTLRStringStream(string input, string sourceName) : this(input.ToCharArray(), input.Length, sourceName)
		{
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000020EF File Offset: 0x000002EF
		public ANTLRStringStream(char[] data, int numberOfActualCharsInArray) : this(data, numberOfActualCharsInArray, null)
		{
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000020FC File Offset: 0x000002FC
		public ANTLRStringStream(char[] data, int numberOfActualCharsInArray, string sourceName)
		{
			this.line = 1;
			base..ctor();
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (numberOfActualCharsInArray < 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (numberOfActualCharsInArray > data.Length)
			{
				throw new ArgumentException();
			}
			this.data = data;
			this.n = numberOfActualCharsInArray;
			this.name = sourceName;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000214F File Offset: 0x0000034F
		protected ANTLRStringStream()
		{
			this.line = 1;
			base..ctor();
			this.data = new char[0];
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000016 RID: 22 RVA: 0x0000216A File Offset: 0x0000036A
		public virtual int Index
		{
			get
			{
				return this.p;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002172 File Offset: 0x00000372
		// (set) Token: 0x06000018 RID: 24 RVA: 0x0000217A File Offset: 0x0000037A
		public virtual int Line
		{
			get
			{
				return this.line;
			}
			set
			{
				this.line = value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002183 File Offset: 0x00000383
		// (set) Token: 0x0600001A RID: 26 RVA: 0x0000218B File Offset: 0x0000038B
		public virtual int CharPositionInLine
		{
			get
			{
				return this.charPositionInLine;
			}
			set
			{
				this.charPositionInLine = value;
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002194 File Offset: 0x00000394
		public virtual void Reset()
		{
			this.p = 0;
			this.line = 1;
			this.charPositionInLine = 0;
			this.markDepth = 0;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000021B4 File Offset: 0x000003B4
		public virtual void Consume()
		{
			if (this.p < this.n)
			{
				this.charPositionInLine++;
				if (this.data[this.p] == '\n')
				{
					this.line++;
					this.charPositionInLine = 0;
				}
				this.p++;
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002214 File Offset: 0x00000414
		public virtual int LA(int i)
		{
			if (i == 0)
			{
				return 0;
			}
			if (i < 0)
			{
				i++;
				if (this.p + i - 1 < 0)
				{
					return -1;
				}
			}
			if (this.p + i - 1 >= this.n)
			{
				return -1;
			}
			return (int)this.data[this.p + i - 1];
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002263 File Offset: 0x00000463
		public virtual int LT(int i)
		{
			return this.LA(i);
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001F RID: 31 RVA: 0x0000226C File Offset: 0x0000046C
		public virtual int Count
		{
			get
			{
				return this.n;
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002274 File Offset: 0x00000474
		public virtual int Mark()
		{
			if (this.markers == null)
			{
				this.markers = new List<CharStreamState>();
				this.markers.Add(null);
			}
			this.markDepth++;
			CharStreamState charStreamState;
			if (this.markDepth >= this.markers.Count)
			{
				charStreamState = new CharStreamState();
				this.markers.Add(charStreamState);
			}
			else
			{
				charStreamState = this.markers[this.markDepth];
			}
			charStreamState.p = this.Index;
			charStreamState.line = this.Line;
			charStreamState.charPositionInLine = this.CharPositionInLine;
			this.lastMarker = this.markDepth;
			return this.markDepth;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002320 File Offset: 0x00000520
		public virtual void Rewind(int m)
		{
			if (m < 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			CharStreamState charStreamState = this.markers[m];
			this.Seek(charStreamState.p);
			this.line = charStreamState.line;
			this.charPositionInLine = charStreamState.charPositionInLine;
			this.Release(m);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x0000236F File Offset: 0x0000056F
		public virtual void Rewind()
		{
			this.Rewind(this.lastMarker);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000237D File Offset: 0x0000057D
		public virtual void Release(int marker)
		{
			this.markDepth = marker;
			this.markDepth--;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002394 File Offset: 0x00000594
		public virtual void Seek(int index)
		{
			if (index <= this.p)
			{
				this.p = index;
				return;
			}
			while (this.p < index)
			{
				this.Consume();
			}
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000023B6 File Offset: 0x000005B6
		public virtual string Substring(int start, int length)
		{
			if (start < 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (start + length > this.data.Length)
			{
				throw new ArgumentException();
			}
			if (length == 0)
			{
				return string.Empty;
			}
			return new string(this.data, start, length);
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000026 RID: 38 RVA: 0x000023F5 File Offset: 0x000005F5
		public virtual string SourceName
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000023FD File Offset: 0x000005FD
		public override string ToString()
		{
			return new string(this.data);
		}

		// Token: 0x04000001 RID: 1
		protected char[] data;

		// Token: 0x04000002 RID: 2
		protected int n;

		// Token: 0x04000003 RID: 3
		protected int p;

		// Token: 0x04000004 RID: 4
		private int line;

		// Token: 0x04000005 RID: 5
		private int charPositionInLine;

		// Token: 0x04000006 RID: 6
		protected int markDepth;

		// Token: 0x04000007 RID: 7
		protected IList<CharStreamState> markers;

		// Token: 0x04000008 RID: 8
		protected int lastMarker;

		// Token: 0x04000009 RID: 9
		public string name;
	}
}
