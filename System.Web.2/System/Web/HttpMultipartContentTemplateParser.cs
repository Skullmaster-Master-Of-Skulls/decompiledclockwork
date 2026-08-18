using System;
using System.Collections;
using System.Globalization;
using System.Text;
using System.Web.Util;

namespace System.Web
{
	// Token: 0x020000DC RID: 220
	internal sealed class HttpMultipartContentTemplateParser
	{
		// Token: 0x06000E21 RID: 3617 RVA: 0x00027FC8 File Offset: 0x000261C8
		private HttpMultipartContentTemplateParser(HttpRawUploadedContent data, int length, byte[] boundary, Encoding encoding)
		{
			this._data = data;
			this._length = length;
			this._boundary = boundary;
			this._encoding = encoding;
		}

		// Token: 0x06000E22 RID: 3618 RVA: 0x0002801F File Offset: 0x0002621F
		private bool AtEndOfData()
		{
			return this._pos >= this._length || this._lastBoundaryFound;
		}

		// Token: 0x06000E23 RID: 3619 RVA: 0x00028038 File Offset: 0x00026238
		private bool GetNextLine()
		{
			int i = this._pos;
			this._lineStart = -1;
			while (i < this._length)
			{
				if (this._data[i] == 10)
				{
					this._lineStart = this._pos;
					this._lineLength = i - this._pos;
					this._pos = i + 1;
					if (this._lineLength > 0 && this._data[i - 1] == 13)
					{
						this._lineLength--;
						break;
					}
					break;
				}
				else if (++i == this._length)
				{
					this._lineStart = this._pos;
					this._lineLength = i - this._pos;
					this._pos = this._length;
				}
			}
			return this._lineStart >= 0;
		}

		// Token: 0x06000E24 RID: 3620 RVA: 0x00028104 File Offset: 0x00026304
		private string ExtractValueFromContentDispositionHeader(string l, int pos, string name)
		{
			string text = " " + name + "=";
			int num = CultureInfo.InvariantCulture.CompareInfo.IndexOf(l, text, pos, CompareOptions.IgnoreCase);
			if (num < 0)
			{
				text = ";" + name + "=";
				num = CultureInfo.InvariantCulture.CompareInfo.IndexOf(l, text, pos, CompareOptions.IgnoreCase);
				if (num < 0)
				{
					text = name + "=";
					num = CultureInfo.InvariantCulture.CompareInfo.IndexOf(l, text, pos, CompareOptions.IgnoreCase);
				}
			}
			if (num < 0)
			{
				return null;
			}
			num += text.Length;
			if (num >= l.Length)
			{
				return string.Empty;
			}
			if (l[num] != '"')
			{
				int num2 = l.IndexOf(';', num);
				if (num2 < 0)
				{
					num2 = l.Length;
				}
				return l.Substring(num, num2 - num).Trim();
			}
			num++;
			int num3 = l.IndexOf('"', num);
			if (num3 < 0)
			{
				return null;
			}
			if (num3 == num)
			{
				return string.Empty;
			}
			return l.Substring(num, num3 - num);
		}

		// Token: 0x06000E25 RID: 3621 RVA: 0x000281FC File Offset: 0x000263FC
		private void ParsePartHeaders()
		{
			this._partName = null;
			this._partFilename = null;
			this._partContentType = null;
			while (this.GetNextLine() && this._lineLength != 0)
			{
				byte[] array = new byte[this._lineLength];
				this._data.CopyBytes(this._lineStart, array, 0, this._lineLength);
				string @string = this._encoding.GetString(array);
				int num = @string.IndexOf(':');
				if (num >= 0)
				{
					string s = @string.Substring(0, num);
					if (StringUtil.EqualsIgnoreCase(s, "Content-Disposition"))
					{
						this._partName = this.ExtractValueFromContentDispositionHeader(@string, num + 1, "name");
						this._partFilename = this.ExtractValueFromContentDispositionHeader(@string, num + 1, "filename");
					}
					else if (StringUtil.EqualsIgnoreCase(s, "Content-Type"))
					{
						this._partContentType = @string.Substring(num + 1).Trim();
					}
				}
			}
		}

		// Token: 0x06000E26 RID: 3622 RVA: 0x000282DC File Offset: 0x000264DC
		private bool AtBoundaryLine()
		{
			int num = this._boundary.Length;
			if (this._lineLength != num && this._lineLength != num + 2)
			{
				return false;
			}
			for (int i = 0; i < num; i++)
			{
				if (this._data[this._lineStart + i] != this._boundary[i])
				{
					return false;
				}
			}
			if (this._lineLength == num)
			{
				return true;
			}
			if (this._data[this._lineStart + num] != 45 || this._data[this._lineStart + num + 1] != 45)
			{
				return false;
			}
			this._lastBoundaryFound = true;
			return true;
		}

		// Token: 0x06000E27 RID: 3623 RVA: 0x00028378 File Offset: 0x00026578
		private void ParsePartData()
		{
			this._partDataStart = this._pos;
			this._partDataLength = -1;
			while (this.GetNextLine())
			{
				if (this.AtBoundaryLine())
				{
					int num = this._lineStart - 1;
					if (this._data[num] == 10)
					{
						num--;
					}
					if (this._data[num] == 13)
					{
						num--;
					}
					this._partDataLength = num - this._partDataStart + 1;
					return;
				}
			}
		}

		// Token: 0x06000E28 RID: 3624 RVA: 0x000283EC File Offset: 0x000265EC
		private void ParseIntoElementList()
		{
			while (this.GetNextLine() && !this.AtBoundaryLine())
			{
			}
			if (this.AtEndOfData())
			{
				return;
			}
			do
			{
				this.ParsePartHeaders();
				if (this.AtEndOfData())
				{
					break;
				}
				this.ParsePartData();
				if (this._partDataLength == -1)
				{
					break;
				}
				if (this._partName != null)
				{
					this._elements.Add(new MultipartContentElement(this._partName, this._partFilename, this._partContentType, this._data, this._partDataStart, this._partDataLength));
				}
			}
			while (!this.AtEndOfData());
		}

		// Token: 0x06000E29 RID: 3625 RVA: 0x00028478 File Offset: 0x00026678
		internal static MultipartContentElement[] Parse(HttpRawUploadedContent data, int length, byte[] boundary, Encoding encoding)
		{
			HttpMultipartContentTemplateParser httpMultipartContentTemplateParser = new HttpMultipartContentTemplateParser(data, length, boundary, encoding);
			httpMultipartContentTemplateParser.ParseIntoElementList();
			return (MultipartContentElement[])httpMultipartContentTemplateParser._elements.ToArray(typeof(MultipartContentElement));
		}

		// Token: 0x0400053C RID: 1340
		private HttpRawUploadedContent _data;

		// Token: 0x0400053D RID: 1341
		private int _length;

		// Token: 0x0400053E RID: 1342
		private int _pos;

		// Token: 0x0400053F RID: 1343
		private ArrayList _elements = new ArrayList();

		// Token: 0x04000540 RID: 1344
		private int _lineStart = -1;

		// Token: 0x04000541 RID: 1345
		private int _lineLength = -1;

		// Token: 0x04000542 RID: 1346
		private bool _lastBoundaryFound;

		// Token: 0x04000543 RID: 1347
		private byte[] _boundary;

		// Token: 0x04000544 RID: 1348
		private string _partName;

		// Token: 0x04000545 RID: 1349
		private string _partFilename;

		// Token: 0x04000546 RID: 1350
		private string _partContentType;

		// Token: 0x04000547 RID: 1351
		private int _partDataStart = -1;

		// Token: 0x04000548 RID: 1352
		private int _partDataLength = -1;

		// Token: 0x04000549 RID: 1353
		private Encoding _encoding;
	}
}
