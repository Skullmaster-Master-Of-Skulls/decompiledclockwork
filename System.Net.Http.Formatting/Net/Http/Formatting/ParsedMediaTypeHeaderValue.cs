using System;
using System.Net.Http.Headers;

namespace System.Net.Http.Formatting
{
	// Token: 0x02000049 RID: 73
	internal struct ParsedMediaTypeHeaderValue
	{
		// Token: 0x060002AF RID: 687 RVA: 0x0000A3BC File Offset: 0x000085BC
		public ParsedMediaTypeHeaderValue(MediaTypeHeaderValue mediaTypeHeaderValue)
		{
			string text = this._mediaType = mediaTypeHeaderValue.MediaType;
			this._delimiterIndex = text.IndexOf('/');
			this._isAllMediaRange = false;
			this._isSubtypeMediaRange = false;
			int length = text.Length;
			if (this._delimiterIndex == length - 2 && text[length - 1] == '*')
			{
				this._isSubtypeMediaRange = true;
				if (this._delimiterIndex == 1 && text[0] == '*')
				{
					this._isAllMediaRange = true;
				}
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x0000A436 File Offset: 0x00008636
		public bool IsAllMediaRange
		{
			get
			{
				return this._isAllMediaRange;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x0000A43E File Offset: 0x0000863E
		public bool IsSubtypeMediaRange
		{
			get
			{
				return this._isSubtypeMediaRange;
			}
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0000A446 File Offset: 0x00008646
		public bool TypesEqual(ref ParsedMediaTypeHeaderValue other)
		{
			return this._delimiterIndex == other._delimiterIndex && string.Compare(this._mediaType, 0, other._mediaType, 0, this._delimiterIndex, StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0000A478 File Offset: 0x00008678
		public bool SubTypesEqual(ref ParsedMediaTypeHeaderValue other)
		{
			int num = this._mediaType.Length - this._delimiterIndex - 1;
			return num == other._mediaType.Length - other._delimiterIndex - 1 && string.Compare(this._mediaType, this._delimiterIndex + 1, other._mediaType, other._delimiterIndex + 1, num, StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x040000B6 RID: 182
		private const char MediaRangeAsterisk = '*';

		// Token: 0x040000B7 RID: 183
		private const char MediaTypeSubtypeDelimiter = '/';

		// Token: 0x040000B8 RID: 184
		private readonly string _mediaType;

		// Token: 0x040000B9 RID: 185
		private readonly int _delimiterIndex;

		// Token: 0x040000BA RID: 186
		private readonly bool _isAllMediaRange;

		// Token: 0x040000BB RID: 187
		private readonly bool _isSubtypeMediaRange;
	}
}
