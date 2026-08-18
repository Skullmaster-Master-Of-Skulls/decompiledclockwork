using System;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace System.Xml
{
	// Token: 0x020000CD RID: 205
	internal class XmlCharCheckingWriter : XmlWrappingWriter
	{
		// Token: 0x06000809 RID: 2057 RVA: 0x0001A362 File Offset: 0x00018562
		internal XmlCharCheckingWriter(XmlWriter baseWriter, bool checkValues, bool checkNames, bool replaceNewLines, string newLineChars) : base(baseWriter)
		{
			this.checkValues = checkValues;
			this.checkNames = checkNames;
			this.replaceNewLines = replaceNewLines;
			this.newLineChars = newLineChars;
			if (checkValues)
			{
				this.xmlCharType = XmlCharType.Instance;
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x0600080A RID: 2058 RVA: 0x0001A398 File Offset: 0x00018598
		public override XmlWriterSettings Settings
		{
			get
			{
				XmlWriterSettings xmlWriterSettings = this.writer.Settings;
				xmlWriterSettings = ((xmlWriterSettings != null) ? xmlWriterSettings.Clone() : new XmlWriterSettings());
				if (this.checkValues)
				{
					xmlWriterSettings.CheckCharacters = true;
				}
				if (this.replaceNewLines)
				{
					xmlWriterSettings.NewLineHandling = NewLineHandling.Replace;
					xmlWriterSettings.NewLineChars = this.newLineChars;
				}
				xmlWriterSettings.ReadOnly = true;
				return xmlWriterSettings;
			}
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x0001A3F4 File Offset: 0x000185F4
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
			if (this.checkNames)
			{
				this.ValidateQName(name);
			}
			if (this.checkValues)
			{
				int invCharPos;
				if (pubid != null && (invCharPos = this.xmlCharType.IsPublicId(pubid)) >= 0)
				{
					throw XmlConvert.CreateInvalidCharException(pubid, invCharPos);
				}
				if (sysid != null)
				{
					this.CheckCharacters(sysid);
				}
				if (subset != null)
				{
					this.CheckCharacters(subset);
				}
			}
			if (this.replaceNewLines)
			{
				sysid = this.ReplaceNewLines(sysid);
				pubid = this.ReplaceNewLines(pubid);
				subset = this.ReplaceNewLines(subset);
			}
			this.writer.WriteDocType(name, pubid, sysid, subset);
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x0001A480 File Offset: 0x00018680
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			if (this.checkNames)
			{
				if (localName == null || localName.Length == 0)
				{
					throw new ArgumentException(Res.GetString("Xml_EmptyLocalName"));
				}
				this.ValidateNCName(localName);
				if (prefix != null && prefix.Length > 0)
				{
					this.ValidateNCName(prefix);
				}
			}
			this.writer.WriteStartElement(prefix, localName, ns);
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x0001A4D8 File Offset: 0x000186D8
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			if (this.checkNames)
			{
				if (localName == null || localName.Length == 0)
				{
					throw new ArgumentException(Res.GetString("Xml_EmptyLocalName"));
				}
				this.ValidateNCName(localName);
				if (prefix != null && prefix.Length > 0)
				{
					this.ValidateNCName(prefix);
				}
			}
			this.writer.WriteStartAttribute(prefix, localName, ns);
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x0001A530 File Offset: 0x00018730
		public override void WriteCData(string text)
		{
			if (text != null)
			{
				if (this.checkValues)
				{
					this.CheckCharacters(text);
				}
				if (this.replaceNewLines)
				{
					text = this.ReplaceNewLines(text);
				}
				int num;
				while ((num = text.IndexOf("]]>", StringComparison.Ordinal)) >= 0)
				{
					this.writer.WriteCData(text.Substring(0, num + 2));
					text = text.Substring(num + 2);
				}
			}
			this.writer.WriteCData(text);
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x0001A59F File Offset: 0x0001879F
		public override void WriteComment(string text)
		{
			if (text != null)
			{
				if (this.checkValues)
				{
					this.CheckCharacters(text);
					text = this.InterleaveInvalidChars(text, '-', '-');
				}
				if (this.replaceNewLines)
				{
					text = this.ReplaceNewLines(text);
				}
			}
			this.writer.WriteComment(text);
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x0001A5E0 File Offset: 0x000187E0
		public override void WriteProcessingInstruction(string name, string text)
		{
			if (this.checkNames)
			{
				this.ValidateNCName(name);
			}
			if (text != null)
			{
				if (this.checkValues)
				{
					this.CheckCharacters(text);
					text = this.InterleaveInvalidChars(text, '?', '>');
				}
				if (this.replaceNewLines)
				{
					text = this.ReplaceNewLines(text);
				}
			}
			this.writer.WriteProcessingInstruction(name, text);
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x0001A639 File Offset: 0x00018839
		public override void WriteEntityRef(string name)
		{
			if (this.checkNames)
			{
				this.ValidateQName(name);
			}
			this.writer.WriteEntityRef(name);
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x0001A658 File Offset: 0x00018858
		public override void WriteWhitespace(string ws)
		{
			if (ws == null)
			{
				ws = string.Empty;
			}
			int invCharIndex;
			if (this.checkNames && (invCharIndex = this.xmlCharType.IsOnlyWhitespaceWithPos(ws)) != -1)
			{
				string name = "Xml_InvalidWhitespaceCharacter";
				object[] args = XmlException.BuildCharExceptionArgs(ws, invCharIndex);
				throw new ArgumentException(Res.GetString(name, args));
			}
			if (this.replaceNewLines)
			{
				ws = this.ReplaceNewLines(ws);
			}
			this.writer.WriteWhitespace(ws);
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x0001A6BE File Offset: 0x000188BE
		public override void WriteString(string text)
		{
			if (text != null)
			{
				if (this.checkValues)
				{
					this.CheckCharacters(text);
				}
				if (this.replaceNewLines && this.WriteState != WriteState.Attribute)
				{
					text = this.ReplaceNewLines(text);
				}
			}
			this.writer.WriteString(text);
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x0001A6F8 File Offset: 0x000188F8
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			this.writer.WriteSurrogateCharEntity(lowChar, highChar);
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x0001A708 File Offset: 0x00018908
		public override void WriteChars(char[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (count > buffer.Length - index)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (this.checkValues)
			{
				this.CheckCharacters(buffer, index, count);
			}
			if (this.replaceNewLines && this.WriteState != WriteState.Attribute)
			{
				string text = this.ReplaceNewLines(buffer, index, count);
				if (text != null)
				{
					this.WriteString(text);
					return;
				}
			}
			this.writer.WriteChars(buffer, index, count);
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x0001A799 File Offset: 0x00018999
		public override void WriteNmToken(string name)
		{
			if (this.checkNames)
			{
				if (name == null || name.Length == 0)
				{
					throw new ArgumentException(Res.GetString("Xml_EmptyName"));
				}
				XmlConvert.VerifyNMTOKEN(name);
			}
			this.writer.WriteNmToken(name);
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x0001A7D1 File Offset: 0x000189D1
		public override void WriteName(string name)
		{
			if (this.checkNames)
			{
				XmlConvert.VerifyQName(name, ExceptionType.XmlException);
			}
			this.writer.WriteName(name);
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x0001A7EF File Offset: 0x000189EF
		public override void WriteQualifiedName(string localName, string ns)
		{
			if (this.checkNames)
			{
				this.ValidateNCName(localName);
			}
			this.writer.WriteQualifiedName(localName, ns);
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x0001A80D File Offset: 0x00018A0D
		private void CheckCharacters(string str)
		{
			XmlConvert.VerifyCharData(str, ExceptionType.ArgumentException);
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x0001A816 File Offset: 0x00018A16
		private void CheckCharacters(char[] data, int offset, int len)
		{
			XmlConvert.VerifyCharData(data, offset, len, ExceptionType.ArgumentException);
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x0001A824 File Offset: 0x00018A24
		private void ValidateNCName(string ncname)
		{
			if (ncname.Length == 0)
			{
				throw new ArgumentException(Res.GetString("Xml_EmptyName"));
			}
			int num = ValidateNames.ParseNCName(ncname, 0);
			if (num != ncname.Length)
			{
				string name = (num == 0) ? "Xml_BadStartNameChar" : "Xml_BadNameChar";
				object[] args = XmlException.BuildCharExceptionArgs(ncname, num);
				throw new ArgumentException(Res.GetString(name, args));
			}
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x0001A880 File Offset: 0x00018A80
		private void ValidateQName(string name)
		{
			if (name.Length == 0)
			{
				throw new ArgumentException(Res.GetString("Xml_EmptyName"));
			}
			int num2;
			int num = ValidateNames.ParseQName(name, 0, out num2);
			if (num != name.Length)
			{
				string text = (num == 0 || (num2 > -1 && num == num2 + 1)) ? "Xml_BadStartNameChar" : "Xml_BadNameChar";
				string name2 = text;
				object[] args = XmlException.BuildCharExceptionArgs(name, num);
				throw new ArgumentException(Res.GetString(name2, args));
			}
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x0001A8E8 File Offset: 0x00018AE8
		private string ReplaceNewLines(string str)
		{
			if (str == null)
			{
				return null;
			}
			StringBuilder stringBuilder = null;
			int num = 0;
			int i;
			for (i = 0; i < str.Length; i++)
			{
				char c;
				if ((c = str[i]) < ' ')
				{
					if (c == '\n')
					{
						if (this.newLineChars == "\n")
						{
							goto IL_F7;
						}
						if (stringBuilder == null)
						{
							stringBuilder = new StringBuilder(str.Length + 5);
						}
						stringBuilder.Append(str, num, i - num);
					}
					else
					{
						if (c != '\r')
						{
							goto IL_F7;
						}
						if (i + 1 < str.Length && str[i + 1] == '\n')
						{
							if (this.newLineChars == "\r\n")
							{
								i++;
								goto IL_F7;
							}
							if (stringBuilder == null)
							{
								stringBuilder = new StringBuilder(str.Length + 5);
							}
							stringBuilder.Append(str, num, i - num);
							i++;
						}
						else
						{
							if (this.newLineChars == "\r")
							{
								goto IL_F7;
							}
							if (stringBuilder == null)
							{
								stringBuilder = new StringBuilder(str.Length + 5);
							}
							stringBuilder.Append(str, num, i - num);
						}
					}
					stringBuilder.Append(this.newLineChars);
					num = i + 1;
				}
				IL_F7:;
			}
			if (stringBuilder == null)
			{
				return str;
			}
			stringBuilder.Append(str, num, i - num);
			return stringBuilder.ToString();
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x0001AA14 File Offset: 0x00018C14
		private string ReplaceNewLines(char[] data, int offset, int len)
		{
			if (data == null)
			{
				return null;
			}
			StringBuilder stringBuilder = null;
			int num = offset;
			int num2 = offset + len;
			int i;
			for (i = offset; i < num2; i++)
			{
				char c;
				if ((c = data[i]) < ' ')
				{
					if (c == '\n')
					{
						if (this.newLineChars == "\n")
						{
							goto IL_DF;
						}
						if (stringBuilder == null)
						{
							stringBuilder = new StringBuilder(len + 5);
						}
						stringBuilder.Append(data, num, i - num);
					}
					else
					{
						if (c != '\r')
						{
							goto IL_DF;
						}
						if (i + 1 < num2 && data[i + 1] == '\n')
						{
							if (this.newLineChars == "\r\n")
							{
								i++;
								goto IL_DF;
							}
							if (stringBuilder == null)
							{
								stringBuilder = new StringBuilder(len + 5);
							}
							stringBuilder.Append(data, num, i - num);
							i++;
						}
						else
						{
							if (this.newLineChars == "\r")
							{
								goto IL_DF;
							}
							if (stringBuilder == null)
							{
								stringBuilder = new StringBuilder(len + 5);
							}
							stringBuilder.Append(data, num, i - num);
						}
					}
					stringBuilder.Append(this.newLineChars);
					num = i + 1;
				}
				IL_DF:;
			}
			if (stringBuilder == null)
			{
				return null;
			}
			stringBuilder.Append(data, num, i - num);
			return stringBuilder.ToString();
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x0001AB24 File Offset: 0x00018D24
		private string InterleaveInvalidChars(string text, char invChar1, char invChar2)
		{
			StringBuilder stringBuilder = null;
			int num = 0;
			int i;
			for (i = 0; i < text.Length; i++)
			{
				if (text[i] == invChar2 && i > 0 && text[i - 1] == invChar1)
				{
					if (stringBuilder == null)
					{
						stringBuilder = new StringBuilder(text.Length + 5);
					}
					stringBuilder.Append(text, num, i - num);
					stringBuilder.Append(' ');
					num = i;
				}
			}
			if (stringBuilder != null)
			{
				stringBuilder.Append(text, num, i - num);
				if (i > 0 && text[i - 1] == invChar1)
				{
					stringBuilder.Append(' ');
				}
				return stringBuilder.ToString();
			}
			if (i != 0 && text[i - 1] == invChar1)
			{
				return text + " ";
			}
			return text;
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x0001ABD4 File Offset: 0x00018DD4
		public override Task WriteDocTypeAsync(string name, string pubid, string sysid, string subset)
		{
			if (this.checkNames)
			{
				this.ValidateQName(name);
			}
			if (this.checkValues)
			{
				int invCharPos;
				if (pubid != null && (invCharPos = this.xmlCharType.IsPublicId(pubid)) >= 0)
				{
					throw XmlConvert.CreateInvalidCharException(pubid, invCharPos);
				}
				if (sysid != null)
				{
					this.CheckCharacters(sysid);
				}
				if (subset != null)
				{
					this.CheckCharacters(subset);
				}
			}
			if (this.replaceNewLines)
			{
				sysid = this.ReplaceNewLines(sysid);
				pubid = this.ReplaceNewLines(pubid);
				subset = this.ReplaceNewLines(subset);
			}
			return this.writer.WriteDocTypeAsync(name, pubid, sysid, subset);
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x0001AC60 File Offset: 0x00018E60
		public override Task WriteStartElementAsync(string prefix, string localName, string ns)
		{
			if (this.checkNames)
			{
				if (localName == null || localName.Length == 0)
				{
					throw new ArgumentException(Res.GetString("Xml_EmptyLocalName"));
				}
				this.ValidateNCName(localName);
				if (prefix != null && prefix.Length > 0)
				{
					this.ValidateNCName(prefix);
				}
			}
			return this.writer.WriteStartElementAsync(prefix, localName, ns);
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x0001ACB8 File Offset: 0x00018EB8
		protected internal override Task WriteStartAttributeAsync(string prefix, string localName, string ns)
		{
			if (this.checkNames)
			{
				if (localName == null || localName.Length == 0)
				{
					throw new ArgumentException(Res.GetString("Xml_EmptyLocalName"));
				}
				this.ValidateNCName(localName);
				if (prefix != null && prefix.Length > 0)
				{
					this.ValidateNCName(prefix);
				}
			}
			return this.writer.WriteStartAttributeAsync(prefix, localName, ns);
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x0001AD10 File Offset: 0x00018F10
		public override Task WriteCDataAsync(string text)
		{
			XmlCharCheckingWriter.<WriteCDataAsync>d__32 <WriteCDataAsync>d__;
			<WriteCDataAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteCDataAsync>d__.<>4__this = this;
			<WriteCDataAsync>d__.text = text;
			<WriteCDataAsync>d__.<>1__state = -1;
			<WriteCDataAsync>d__.<>t__builder.Start<XmlCharCheckingWriter.<WriteCDataAsync>d__32>(ref <WriteCDataAsync>d__);
			return <WriteCDataAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x0001AD5B File Offset: 0x00018F5B
		public override Task WriteCommentAsync(string text)
		{
			if (text != null)
			{
				if (this.checkValues)
				{
					this.CheckCharacters(text);
					text = this.InterleaveInvalidChars(text, '-', '-');
				}
				if (this.replaceNewLines)
				{
					text = this.ReplaceNewLines(text);
				}
			}
			return this.writer.WriteCommentAsync(text);
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x0001AD9C File Offset: 0x00018F9C
		public override Task WriteProcessingInstructionAsync(string name, string text)
		{
			if (this.checkNames)
			{
				this.ValidateNCName(name);
			}
			if (text != null)
			{
				if (this.checkValues)
				{
					this.CheckCharacters(text);
					text = this.InterleaveInvalidChars(text, '?', '>');
				}
				if (this.replaceNewLines)
				{
					text = this.ReplaceNewLines(text);
				}
			}
			return this.writer.WriteProcessingInstructionAsync(name, text);
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x0001ADF5 File Offset: 0x00018FF5
		public override Task WriteEntityRefAsync(string name)
		{
			if (this.checkNames)
			{
				this.ValidateQName(name);
			}
			return this.writer.WriteEntityRefAsync(name);
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x0001AE14 File Offset: 0x00019014
		public override Task WriteWhitespaceAsync(string ws)
		{
			if (ws == null)
			{
				ws = string.Empty;
			}
			int invCharIndex;
			if (this.checkNames && (invCharIndex = this.xmlCharType.IsOnlyWhitespaceWithPos(ws)) != -1)
			{
				string name = "Xml_InvalidWhitespaceCharacter";
				object[] args = XmlException.BuildCharExceptionArgs(ws, invCharIndex);
				throw new ArgumentException(Res.GetString(name, args));
			}
			if (this.replaceNewLines)
			{
				ws = this.ReplaceNewLines(ws);
			}
			return this.writer.WriteWhitespaceAsync(ws);
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x0001AE7A File Offset: 0x0001907A
		public override Task WriteStringAsync(string text)
		{
			if (text != null)
			{
				if (this.checkValues)
				{
					this.CheckCharacters(text);
				}
				if (this.replaceNewLines && this.WriteState != WriteState.Attribute)
				{
					text = this.ReplaceNewLines(text);
				}
			}
			return this.writer.WriteStringAsync(text);
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x0001AEB4 File Offset: 0x000190B4
		public override Task WriteSurrogateCharEntityAsync(char lowChar, char highChar)
		{
			return this.writer.WriteSurrogateCharEntityAsync(lowChar, highChar);
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x0001AEC4 File Offset: 0x000190C4
		public override Task WriteCharsAsync(char[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (count > buffer.Length - index)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (this.checkValues)
			{
				this.CheckCharacters(buffer, index, count);
			}
			if (this.replaceNewLines && this.WriteState != WriteState.Attribute)
			{
				string text = this.ReplaceNewLines(buffer, index, count);
				if (text != null)
				{
					return this.WriteStringAsync(text);
				}
			}
			return this.writer.WriteCharsAsync(buffer, index, count);
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x0001AF55 File Offset: 0x00019155
		public override Task WriteNmTokenAsync(string name)
		{
			if (this.checkNames)
			{
				if (name == null || name.Length == 0)
				{
					throw new ArgumentException(Res.GetString("Xml_EmptyName"));
				}
				XmlConvert.VerifyNMTOKEN(name);
			}
			return this.writer.WriteNmTokenAsync(name);
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x0001AF8D File Offset: 0x0001918D
		public override Task WriteNameAsync(string name)
		{
			if (this.checkNames)
			{
				XmlConvert.VerifyQName(name, ExceptionType.XmlException);
			}
			return this.writer.WriteNameAsync(name);
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x0001AFAB File Offset: 0x000191AB
		public override Task WriteQualifiedNameAsync(string localName, string ns)
		{
			if (this.checkNames)
			{
				this.ValidateNCName(localName);
			}
			return this.writer.WriteQualifiedNameAsync(localName, ns);
		}

		// Token: 0x040002F2 RID: 754
		private bool checkValues;

		// Token: 0x040002F3 RID: 755
		private bool checkNames;

		// Token: 0x040002F4 RID: 756
		private bool replaceNewLines;

		// Token: 0x040002F5 RID: 757
		private string newLineChars;

		// Token: 0x040002F6 RID: 758
		private XmlCharType xmlCharType;
	}
}
