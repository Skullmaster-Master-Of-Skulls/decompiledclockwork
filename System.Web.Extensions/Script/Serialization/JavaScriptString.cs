using System;

namespace System.Web.Script.Serialization
{
	// Token: 0x02000100 RID: 256
	internal class JavaScriptString
	{
		// Token: 0x06000DB5 RID: 3509 RVA: 0x0003038E File Offset: 0x0002E58E
		internal JavaScriptString(string s)
		{
			this._s = s;
		}

		// Token: 0x06000DB6 RID: 3510 RVA: 0x000303A0 File Offset: 0x0002E5A0
		internal char? GetNextNonEmptyChar()
		{
			while (this._s.Length > this._index)
			{
				string s = this._s;
				int index = this._index;
				this._index = index + 1;
				char c = s[index];
				if (!char.IsWhiteSpace(c))
				{
					return new char?(c);
				}
			}
			return null;
		}

		// Token: 0x06000DB7 RID: 3511 RVA: 0x000303F8 File Offset: 0x0002E5F8
		internal char? MoveNext()
		{
			if (this._s.Length > this._index)
			{
				string s = this._s;
				int index = this._index;
				this._index = index + 1;
				return new char?(s[index]);
			}
			return null;
		}

		// Token: 0x06000DB8 RID: 3512 RVA: 0x00030444 File Offset: 0x0002E644
		internal string MoveNext(int count)
		{
			if (this._s.Length >= this._index + count)
			{
				string result = this._s.Substring(this._index, count);
				this._index += count;
				return result;
			}
			return null;
		}

		// Token: 0x06000DB9 RID: 3513 RVA: 0x0003048A File Offset: 0x0002E68A
		internal void MovePrev()
		{
			if (this._index > 0)
			{
				this._index--;
			}
		}

		// Token: 0x06000DBA RID: 3514 RVA: 0x000304A3 File Offset: 0x0002E6A3
		internal void MovePrev(int count)
		{
			while (this._index > 0 && count > 0)
			{
				this._index--;
				count--;
			}
		}

		// Token: 0x06000DBB RID: 3515 RVA: 0x000304C7 File Offset: 0x0002E6C7
		public override string ToString()
		{
			if (this._s.Length > this._index)
			{
				return this._s.Substring(this._index);
			}
			return string.Empty;
		}

		// Token: 0x06000DBC RID: 3516 RVA: 0x000304F3 File Offset: 0x0002E6F3
		internal string GetDebugString(string message)
		{
			return string.Concat(new string[]
			{
				message,
				" (",
				this._index.ToString(),
				"): ",
				this._s
			});
		}

		// Token: 0x06000DBD RID: 3517 RVA: 0x0003052B File Offset: 0x0002E72B
		internal int IndexOf(string substr)
		{
			if (this._s.Length > this._index)
			{
				return this._s.IndexOf(substr, this._index, StringComparison.CurrentCulture) - this._index;
			}
			return -1;
		}

		// Token: 0x06000DBE RID: 3518 RVA: 0x0003055C File Offset: 0x0002E75C
		internal int LimitedIndexOf(string substr, int count)
		{
			int num = this._s.IndexOf(substr, this._index, Math.Min(count, this._s.Length - this._index), StringComparison.Ordinal);
			if (num >= 0)
			{
				return num - this._index;
			}
			return -1;
		}

		// Token: 0x06000DBF RID: 3519 RVA: 0x000305A3 File Offset: 0x0002E7A3
		internal string Substring(int length)
		{
			if (this._s.Length > this._index + length)
			{
				return this._s.Substring(this._index, length);
			}
			return this.ToString();
		}

		// Token: 0x040003DC RID: 988
		private string _s;

		// Token: 0x040003DD RID: 989
		private int _index;
	}
}
