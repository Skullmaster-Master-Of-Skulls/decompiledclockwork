using System;
using System.Runtime.InteropServices;

namespace System.IO
{
	// Token: 0x020005CF RID: 1487
	[ComVisible(true)]
	[Serializable]
	public class StringReader : TextReader
	{
		// Token: 0x0600378A RID: 14218 RVA: 0x000BB440 File Offset: 0x000BA440
		public StringReader(string s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			this._s = s;
			this._length = ((s == null) ? 0 : s.Length);
		}

		// Token: 0x0600378B RID: 14219 RVA: 0x000BB46F File Offset: 0x000BA46F
		public override void Close()
		{
			this.Dispose(true);
		}

		// Token: 0x0600378C RID: 14220 RVA: 0x000BB478 File Offset: 0x000BA478
		protected override void Dispose(bool disposing)
		{
			this._s = null;
			this._pos = 0;
			this._length = 0;
			base.Dispose(disposing);
		}

		// Token: 0x0600378D RID: 14221 RVA: 0x000BB496 File Offset: 0x000BA496
		public override int Peek()
		{
			if (this._s == null)
			{
				__Error.ReaderClosed();
			}
			if (this._pos == this._length)
			{
				return -1;
			}
			return (int)this._s[this._pos];
		}

		// Token: 0x0600378E RID: 14222 RVA: 0x000BB4C8 File Offset: 0x000BA4C8
		public override int Read()
		{
			if (this._s == null)
			{
				__Error.ReaderClosed();
			}
			if (this._pos == this._length)
			{
				return -1;
			}
			return (int)this._s[this._pos++];
		}

		// Token: 0x0600378F RID: 14223 RVA: 0x000BB510 File Offset: 0x000BA510
		public override int Read([In] [Out] char[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", Environment.GetResourceString("ArgumentNull_Buffer"));
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			if (this._s == null)
			{
				__Error.ReaderClosed();
			}
			int num = this._length - this._pos;
			if (num > 0)
			{
				if (num > count)
				{
					num = count;
				}
				this._s.CopyTo(this._pos, buffer, index, num);
				this._pos += num;
			}
			return num;
		}

		// Token: 0x06003790 RID: 14224 RVA: 0x000BB5C8 File Offset: 0x000BA5C8
		public override string ReadToEnd()
		{
			if (this._s == null)
			{
				__Error.ReaderClosed();
			}
			string result;
			if (this._pos == 0)
			{
				result = this._s;
			}
			else
			{
				result = this._s.Substring(this._pos, this._length - this._pos);
			}
			this._pos = this._length;
			return result;
		}

		// Token: 0x06003791 RID: 14225 RVA: 0x000BB620 File Offset: 0x000BA620
		public override string ReadLine()
		{
			if (this._s == null)
			{
				__Error.ReaderClosed();
			}
			int i;
			for (i = this._pos; i < this._length; i++)
			{
				char c = this._s[i];
				if (c == '\r' || c == '\n')
				{
					string result = this._s.Substring(this._pos, i - this._pos);
					this._pos = i + 1;
					if (c == '\r' && this._pos < this._length && this._s[this._pos] == '\n')
					{
						this._pos++;
					}
					return result;
				}
			}
			if (i > this._pos)
			{
				string result2 = this._s.Substring(this._pos, i - this._pos);
				this._pos = i;
				return result2;
			}
			return null;
		}

		// Token: 0x04001CD9 RID: 7385
		private string _s;

		// Token: 0x04001CDA RID: 7386
		private int _pos;

		// Token: 0x04001CDB RID: 7387
		private int _length;
	}
}
