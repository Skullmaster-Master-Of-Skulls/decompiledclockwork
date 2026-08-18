using System;
using System.Collections;
using System.Security.Util;
using System.Text;

namespace System.Security
{
	// Token: 0x02000613 RID: 1555
	[Serializable]
	internal sealed class SecurityDocument
	{
		// Token: 0x06003829 RID: 14377 RVA: 0x000BC326 File Offset: 0x000BB326
		public SecurityDocument(int numData)
		{
			this.m_data = new byte[numData];
		}

		// Token: 0x0600382A RID: 14378 RVA: 0x000BC33A File Offset: 0x000BB33A
		public SecurityDocument(byte[] data)
		{
			this.m_data = data;
		}

		// Token: 0x0600382B RID: 14379 RVA: 0x000BC34C File Offset: 0x000BB34C
		public SecurityDocument(SecurityElement elRoot)
		{
			this.m_data = new byte[32];
			int num = 0;
			this.ConvertElement(elRoot, ref num);
		}

		// Token: 0x0600382C RID: 14380 RVA: 0x000BC378 File Offset: 0x000BB378
		public void GuaranteeSize(int size)
		{
			if (this.m_data.Length < size)
			{
				byte[] array = new byte[(size / 32 + 1) * 32];
				Array.Copy(this.m_data, 0, array, 0, this.m_data.Length);
				this.m_data = array;
			}
		}

		// Token: 0x0600382D RID: 14381 RVA: 0x000BC3BC File Offset: 0x000BB3BC
		public void AddString(string str, ref int position)
		{
			this.GuaranteeSize(position + str.Length * 2 + 2);
			for (int i = 0; i < str.Length; i++)
			{
				this.m_data[position + 2 * i] = (byte)(str[i] >> 8);
				this.m_data[position + 2 * i + 1] = (byte)(str[i] & 'ÿ');
			}
			this.m_data[position + str.Length * 2] = 0;
			this.m_data[position + str.Length * 2 + 1] = 0;
			position += str.Length * 2 + 2;
		}

		// Token: 0x0600382E RID: 14382 RVA: 0x000BC458 File Offset: 0x000BB458
		public void AppendString(string str, ref int position)
		{
			if (position <= 1 || this.m_data[position - 1] != 0 || this.m_data[position - 2] != 0)
			{
				throw new XmlSyntaxException();
			}
			position -= 2;
			this.AddString(str, ref position);
		}

		// Token: 0x0600382F RID: 14383 RVA: 0x000BC48D File Offset: 0x000BB48D
		public static int EncodedStringSize(string str)
		{
			return str.Length * 2 + 2;
		}

		// Token: 0x06003830 RID: 14384 RVA: 0x000BC499 File Offset: 0x000BB499
		public string GetString(ref int position)
		{
			return this.GetString(ref position, true);
		}

		// Token: 0x06003831 RID: 14385 RVA: 0x000BC4A4 File Offset: 0x000BB4A4
		public string GetString(ref int position, bool bCreate)
		{
			int num = position;
			while (num < this.m_data.Length - 1 && (this.m_data[num] != 0 || this.m_data[num + 1] != 0))
			{
				num += 2;
			}
			Tokenizer.StringMaker sharedStringMaker = SharedStatics.GetSharedStringMaker();
			string result;
			try
			{
				if (bCreate)
				{
					sharedStringMaker._outStringBuilder = null;
					sharedStringMaker._outIndex = 0;
					for (int i = position; i < num; i += 2)
					{
						char c = (char)((int)this.m_data[i] << 8 | (int)this.m_data[i + 1]);
						if (sharedStringMaker._outIndex < 512)
						{
							sharedStringMaker._outChars[sharedStringMaker._outIndex++] = c;
						}
						else
						{
							if (sharedStringMaker._outStringBuilder == null)
							{
								sharedStringMaker._outStringBuilder = new StringBuilder();
							}
							sharedStringMaker._outStringBuilder.Append(sharedStringMaker._outChars, 0, 512);
							sharedStringMaker._outChars[0] = c;
							sharedStringMaker._outIndex = 1;
						}
					}
				}
				position = num + 2;
				if (bCreate)
				{
					result = sharedStringMaker.MakeString();
				}
				else
				{
					result = null;
				}
			}
			finally
			{
				SharedStatics.ReleaseSharedStringMaker(ref sharedStringMaker);
			}
			return result;
		}

		// Token: 0x06003832 RID: 14386 RVA: 0x000BC5B8 File Offset: 0x000BB5B8
		public void AddToken(byte b, ref int position)
		{
			this.GuaranteeSize(position + 1);
			this.m_data[position++] = b;
		}

		// Token: 0x06003833 RID: 14387 RVA: 0x000BC5E0 File Offset: 0x000BB5E0
		public void ConvertElement(SecurityElement elCurrent, ref int position)
		{
			this.AddToken(1, ref position);
			this.AddString(elCurrent.m_strTag, ref position);
			if (elCurrent.m_lAttributes != null)
			{
				for (int i = 0; i < elCurrent.m_lAttributes.Count; i += 2)
				{
					this.AddToken(2, ref position);
					this.AddString((string)elCurrent.m_lAttributes[i], ref position);
					this.AddString((string)elCurrent.m_lAttributes[i + 1], ref position);
				}
			}
			if (elCurrent.m_strText != null)
			{
				this.AddToken(3, ref position);
				this.AddString(elCurrent.m_strText, ref position);
			}
			if (elCurrent.InternalChildren != null)
			{
				for (int j = 0; j < elCurrent.InternalChildren.Count; j++)
				{
					this.ConvertElement((SecurityElement)elCurrent.Children[j], ref position);
				}
			}
			this.AddToken(4, ref position);
		}

		// Token: 0x06003834 RID: 14388 RVA: 0x000BC6B5 File Offset: 0x000BB6B5
		public SecurityElement GetRootElement()
		{
			return this.GetElement(0, true);
		}

		// Token: 0x06003835 RID: 14389 RVA: 0x000BC6C0 File Offset: 0x000BB6C0
		public SecurityElement GetElement(int position, bool bCreate)
		{
			return this.InternalGetElement(ref position, bCreate);
		}

		// Token: 0x06003836 RID: 14390 RVA: 0x000BC6D8 File Offset: 0x000BB6D8
		internal SecurityElement InternalGetElement(ref int position, bool bCreate)
		{
			if (this.m_data.Length <= position)
			{
				throw new XmlSyntaxException();
			}
			if (this.m_data[position++] != 1)
			{
				throw new XmlSyntaxException();
			}
			SecurityElement securityElement = null;
			string @string = this.GetString(ref position, bCreate);
			if (bCreate)
			{
				securityElement = new SecurityElement(@string);
			}
			while (this.m_data[position] == 2)
			{
				position++;
				string string2 = this.GetString(ref position, bCreate);
				string string3 = this.GetString(ref position, bCreate);
				if (bCreate)
				{
					securityElement.AddAttribute(string2, string3);
				}
			}
			if (this.m_data[position] == 3)
			{
				position++;
				string string4 = this.GetString(ref position, bCreate);
				if (bCreate)
				{
					securityElement.m_strText = string4;
				}
			}
			while (this.m_data[position] != 4)
			{
				SecurityElement child = this.InternalGetElement(ref position, bCreate);
				if (bCreate)
				{
					securityElement.AddChild(child);
				}
			}
			position++;
			return securityElement;
		}

		// Token: 0x06003837 RID: 14391 RVA: 0x000BC7AC File Offset: 0x000BB7AC
		public string GetTagForElement(int position)
		{
			if (this.m_data.Length <= position)
			{
				throw new XmlSyntaxException();
			}
			if (this.m_data[position++] != 1)
			{
				throw new XmlSyntaxException();
			}
			return this.GetString(ref position);
		}

		// Token: 0x06003838 RID: 14392 RVA: 0x000BC7EC File Offset: 0x000BB7EC
		public ArrayList GetChildrenPositionForElement(int position)
		{
			if (this.m_data.Length <= position)
			{
				throw new XmlSyntaxException();
			}
			if (this.m_data[position++] != 1)
			{
				throw new XmlSyntaxException();
			}
			ArrayList arrayList = new ArrayList();
			this.GetString(ref position);
			while (this.m_data[position] == 2)
			{
				position++;
				this.GetString(ref position, false);
				this.GetString(ref position, false);
			}
			if (this.m_data[position] == 3)
			{
				position++;
				this.GetString(ref position, false);
			}
			while (this.m_data[position] != 4)
			{
				arrayList.Add(position);
				this.InternalGetElement(ref position, false);
			}
			position++;
			return arrayList;
		}

		// Token: 0x06003839 RID: 14393 RVA: 0x000BC89C File Offset: 0x000BB89C
		public string GetAttributeForElement(int position, string attributeName)
		{
			if (this.m_data.Length <= position)
			{
				throw new XmlSyntaxException();
			}
			if (this.m_data[position++] != 1)
			{
				throw new XmlSyntaxException();
			}
			string result = null;
			this.GetString(ref position, false);
			while (this.m_data[position] == 2)
			{
				position++;
				string @string = this.GetString(ref position);
				string string2 = this.GetString(ref position);
				if (string.Equals(@string, attributeName))
				{
					result = string2;
					break;
				}
			}
			return result;
		}

		// Token: 0x04001D19 RID: 7449
		internal const byte c_element = 1;

		// Token: 0x04001D1A RID: 7450
		internal const byte c_attribute = 2;

		// Token: 0x04001D1B RID: 7451
		internal const byte c_text = 3;

		// Token: 0x04001D1C RID: 7452
		internal const byte c_children = 4;

		// Token: 0x04001D1D RID: 7453
		internal const int c_growthSize = 32;

		// Token: 0x04001D1E RID: 7454
		internal byte[] m_data;
	}
}
