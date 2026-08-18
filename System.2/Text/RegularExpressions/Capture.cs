using System;

namespace System.Text.RegularExpressions
{
	// Token: 0x0200068C RID: 1676
	[__DynamicallyInvokable]
	[Serializable]
	public class Capture
	{
		// Token: 0x06003DF3 RID: 15859 RVA: 0x000FDFC0 File Offset: 0x000FC1C0
		internal Capture(string text, int i, int l)
		{
			this._text = text;
			this._index = i;
			this._length = l;
		}

		// Token: 0x17000EAF RID: 3759
		// (get) Token: 0x06003DF4 RID: 15860 RVA: 0x000FDFDD File Offset: 0x000FC1DD
		[__DynamicallyInvokable]
		public int Index
		{
			[__DynamicallyInvokable]
			get
			{
				return this._index;
			}
		}

		// Token: 0x17000EB0 RID: 3760
		// (get) Token: 0x06003DF5 RID: 15861 RVA: 0x000FDFE5 File Offset: 0x000FC1E5
		[__DynamicallyInvokable]
		public int Length
		{
			[__DynamicallyInvokable]
			get
			{
				return this._length;
			}
		}

		// Token: 0x17000EB1 RID: 3761
		// (get) Token: 0x06003DF6 RID: 15862 RVA: 0x000FDFED File Offset: 0x000FC1ED
		[__DynamicallyInvokable]
		public string Value
		{
			[__DynamicallyInvokable]
			get
			{
				return this._text.Substring(this._index, this._length);
			}
		}

		// Token: 0x06003DF7 RID: 15863 RVA: 0x000FE006 File Offset: 0x000FC206
		[__DynamicallyInvokable]
		public override string ToString()
		{
			return this.Value;
		}

		// Token: 0x06003DF8 RID: 15864 RVA: 0x000FE00E File Offset: 0x000FC20E
		internal string GetOriginalString()
		{
			return this._text;
		}

		// Token: 0x06003DF9 RID: 15865 RVA: 0x000FE016 File Offset: 0x000FC216
		internal string GetLeftSubstring()
		{
			return this._text.Substring(0, this._index);
		}

		// Token: 0x06003DFA RID: 15866 RVA: 0x000FE02A File Offset: 0x000FC22A
		internal string GetRightSubstring()
		{
			return this._text.Substring(this._index + this._length, this._text.Length - this._index - this._length);
		}

		// Token: 0x04002D02 RID: 11522
		internal string _text;

		// Token: 0x04002D03 RID: 11523
		internal int _index;

		// Token: 0x04002D04 RID: 11524
		internal int _length;
	}
}
