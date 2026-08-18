using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Xml;
using Spire.CompoundFile.Doc;
using Spire.Doc.Interface;

namespace Spire.Doc.Documents.XML
{
	// Token: 0x02000547 RID: 1351
	public class XDLSReader : IXDLSAttributeReader, IXDLSContentReader
	{
		// Token: 0x0600465E RID: 18014 RVA: 0x0040EAA0 File Offset: 0x0040DAA0
		public XDLSReader(XmlReader reader)
		{
			this.ᜁ = reader;
		}

		// Token: 0x0600465F RID: 18015 RVA: 0x0040EAC8 File Offset: 0x0040DAC8
		public void Deserialize(IDocumentSerializable value)
		{
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					if (this.ᜁ.NodeType == XmlNodeType.Element)
					{
						num = 3;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						this.ᜁ.Read();
						break;
					}
					num = 2;
					continue;
				case 2:
					goto IL_34;
				case 3:
					goto IL_52;
				}
				goto IL_2A;
				IL_34:
				num = 1;
				continue;
				IL_2A:
				if (true)
				{
				}
				goto IL_34;
			}
			IL_52:
			this.ᜀ(value);
			value.XDLSHolder.AfterDeserialization(value);
		}

		// Token: 0x06004660 RID: 18016 RVA: 0x0040EB70 File Offset: 0x0040DB70
		public bool HasAttribute(string name)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return this.ᜁ.GetAttribute(name) != null;
		}

		// Token: 0x06004661 RID: 18017 RVA: 0x0040EBC0 File Offset: 0x0040DBC0
		public string ReadString(string name)
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			return this.ᜁ.GetAttribute(name);
		}

		// Token: 0x06004662 RID: 18018 RVA: 0x0040EC08 File Offset: 0x0040DC08
		public int ReadInt(string name)
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			return XmlConvert.ToInt32(this.ᜁ.GetAttribute(name));
		}

		// Token: 0x06004663 RID: 18019 RVA: 0x0040EC54 File Offset: 0x0040DC54
		public short ReadShort(string name)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return XmlConvert.ToInt16(this.ᜁ.GetAttribute(name));
		}

		// Token: 0x06004664 RID: 18020 RVA: 0x0040ECA0 File Offset: 0x0040DCA0
		public double ReadDouble(string name)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return XmlConvert.ToDouble(this.ᜁ.GetAttribute(name));
		}

		// Token: 0x06004665 RID: 18021 RVA: 0x0040ECEC File Offset: 0x0040DCEC
		public float ReadFloat(string name)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return XmlConvert.ToSingle(this.ᜁ.GetAttribute(name));
		}

		// Token: 0x06004666 RID: 18022 RVA: 0x0040ED38 File Offset: 0x0040DD38
		public bool ReadBoolean(string name)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			string attribute = this.ᜁ.GetAttribute(name);
			return XmlConvert.ToBoolean(attribute);
		}

		// Token: 0x06004667 RID: 18023 RVA: 0x0040ED88 File Offset: 0x0040DD88
		public byte ReadByte(string name)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			string attribute = this.ᜁ.GetAttribute(name);
			return XmlConvert.ToByte(attribute);
		}

		// Token: 0x06004668 RID: 18024 RVA: 0x0040EDD8 File Offset: 0x0040DDD8
		public Enum ReadEnum(string name, Type enumType)
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			string attribute = this.ᜁ.GetAttribute(name);
			return (Enum)Enum.Parse(enumType, attribute);
		}

		// Token: 0x06004669 RID: 18025 RVA: 0x0040EE2C File Offset: 0x0040DE2C
		public Color ReadColor(string name)
		{
			int a_ = 15;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (name.Length != 0)
					{
						goto IL_A6;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_90;
					default:
						if (false)
						{
						}
						num = 1;
						continue;
					}
					break;
				case 1:
					goto IL_90;
				case 2:
					if (true)
					{
					}
					break;
				case 3:
					goto IL_46;
				}
				if (name == null)
				{
					num = 3;
				}
				else
				{
					num = 0;
				}
			}
			IL_46:
			throw new ArgumentNullException(ClipboardData.b("᭴ᙶᑸṺ", a_));
			IL_90:
			throw new ArgumentException(ClipboardData.b("᭴ᙶᑸṺ嵼剾ꆀ꾎ﮔ랖뾞쎠욢薤슦쒨\udbaa\ud9ac횮", a_));
			IL_A6:
			string attribute = this.ᜁ.GetAttribute(name);
			return this.ᜀ(attribute);
		}

		// Token: 0x0600466A RID: 18026 RVA: 0x0040EEF8 File Offset: 0x0040DEF8
		private Color ᜀ(string A_0)
		{
			int a_ = 7;
			A_0 = A_0.Replace(ClipboardData.b("乬", a_), string.Empty);
			Color result;
			try
			{
				if (true)
				{
				}
				string s = A_0.Substring(0, 2);
				string s2 = A_0.Substring(2, 2);
				string s3 = A_0.Substring(4, 2);
				string s4 = A_0.Substring(6, 2);
				int alpha = int.Parse(s, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
				int red = int.Parse(s2, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
				int green = int.Parse(s3, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
				int blue = int.Parse(s4, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
				result = Color.FromArgb(alpha, red, green, blue);
			}
			catch
			{
				goto IL_26;
			}
			goto IL_C1;
			IL_26:
			return Color.Empty;
			IL_C1:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_26;
			default:
				if (false)
				{
				}
				return result;
			}
		}

		// Token: 0x0600466B RID: 18027 RVA: 0x0040EFF4 File Offset: 0x0040DFF4
		public DateTime ReadDateTime(string name)
		{
			int a_ = 0;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (name.Length != 0)
					{
						goto IL_A6;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_90;
					default:
						if (false)
						{
						}
						num = 1;
						continue;
					}
					break;
				case 1:
					goto IL_90;
				case 3:
					goto IL_46;
				}
				if (name == null)
				{
					if (true)
					{
					}
					num = 3;
				}
				else
				{
					num = 0;
				}
			}
			IL_46:
			throw new ArgumentNullException(ClipboardData.b("ࡥ१ݩ५", a_));
			IL_90:
			throw new ArgumentException(ClipboardData.b("ࡥ१ݩ५乭嵯剱ݳɵ੷፹ቻ᥽ꁿꢇ揄낏뚕ﶗ\ud99f", a_));
			IL_A6:
			string attribute = this.ᜁ.GetAttribute(name);
			return XmlConvert.ToDateTime(attribute, XmlDateTimeSerializationMode.Utc);
		}

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x0600466C RID: 18028 RVA: 0x0040F0C0 File Offset: 0x0040E0C0
		public string TagName
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜁ.LocalName;
			}
		}

		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x0600466D RID: 18029 RVA: 0x0040F108 File Offset: 0x0040E108
		public XmlNodeType NodeType
		{
			get
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				return this.ᜁ.NodeType;
			}
		}

		// Token: 0x0600466E RID: 18030 RVA: 0x0040F150 File Offset: 0x0040E150
		public string GetAttributeValue(string name)
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			return this.ᜁ.GetAttribute(name);
		}

		// Token: 0x0600466F RID: 18031 RVA: 0x0040F198 File Offset: 0x0040E198
		public bool ParseElementType(Type enumType, out Enum elementType)
		{
			int a_ = 2;
			switch (0)
			{
			default:
			{
				Array array2;
				int num2;
				for (;;)
				{
					string[] array = null;
					array2 = null;
					object obj = null;
					int num = 11;
					for (;;)
					{
						string attributeValue;
						switch (num)
						{
						case 0:
							goto IL_141;
						case 1:
							goto IL_96;
						case 2:
							if (num2 >= array.Length)
							{
								num = 12;
								continue;
							}
							num = 8;
							continue;
						case 3:
							goto IL_141;
						case 4:
							goto IL_96;
						case 5:
							goto IL_BF;
						case 6:
							array = Enum.GetNames(enumType);
							array2 = Enum.GetValues(enumType);
							obj = new object[]
							{
								array,
								array2
							};
							XDLSReader.ᜀ.Add(enumType, obj);
							goto IL_1DA;
						case 7:
							if (true)
							{
							}
							if (obj == null)
							{
								num = 6;
								continue;
							}
							array = (string[])((object[])obj)[0];
							array2 = (Array)((object[])obj)[1];
							num = 4;
							continue;
						case 8:
							if (array[num2] == attributeValue)
							{
								num = 9;
								continue;
							}
							num2++;
							num = 3;
							continue;
						case 9:
							goto IL_13C;
						case 10:
							obj = XDLSReader.ᜀ[enumType];
							num = 5;
							continue;
						case 11:
							if (XDLSReader.ᜀ.ContainsKey(enumType))
							{
								num = 10;
								continue;
							}
							goto IL_BF;
						case 12:
							goto IL_17C;
						}
						break;
						IL_96:
						attributeValue = this.GetAttributeValue(ClipboardData.b("ᱧ፩ᱫ୭", a_));
						num2 = 0;
						num = 0;
						continue;
						IL_BF:
						num = 7;
						continue;
						IL_141:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							IL_1DA:
							num = 1;
							break;
						default:
							if (false)
							{
							}
							num = 2;
							break;
						}
					}
				}
				IL_13C:
				elementType = (Enum)array2.GetValue(num2);
				return true;
				IL_17C:
				elementType = (Enum)array2.GetValue(0);
				return false;
			}
			}
		}

		// Token: 0x06004670 RID: 18032 RVA: 0x0040F3A0 File Offset: 0x0040E3A0
		public bool ReadChildElement(object value)
		{
			for (;;)
			{
				IL_20:
				IDocumentSerializable documentSerializable = value as IDocumentSerializable;
				for (;;)
				{
					IL_27:
					int num = 4;
					for (;;)
					{
						switch (num)
						{
						case 0:
						{
							IXDLSSerializableCollection ixdlsserializableCollection;
							if (ixdlsserializableCollection != null)
							{
								num = 1;
								continue;
							}
							return false;
						}
						case 1:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_27;
							default:
							{
								if (false)
								{
								}
								IXDLSSerializableCollection ixdlsserializableCollection;
								this.ᜀ(ixdlsserializableCollection);
								num = 3;
								continue;
							}
							}
							break;
						case 2:
							goto IL_97;
						case 3:
							goto IL_4B;
						case 4:
						{
							if (documentSerializable != null)
							{
								num = 5;
								continue;
							}
							IXDLSSerializableCollection ixdlsserializableCollection = value as IXDLSSerializableCollection;
							num = 0;
							continue;
						}
						case 5:
							this.ᜀ(documentSerializable);
							num = 2;
							continue;
						}
						goto IL_20;
					}
				}
			}
			IL_4B:
			IL_97:
			if (true)
			{
			}
			return true;
		}

		// Token: 0x06004671 RID: 18033 RVA: 0x0040F45C File Offset: 0x0040E45C
		public object ReadChildElement(Type type)
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			return this.ᜂ.ᜀ(this.ᜁ, type);
		}

		// Token: 0x06004672 RID: 18034 RVA: 0x0040F4AC File Offset: 0x0040E4AC
		public string ReadChildStringContent()
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			string empty = string.Empty;
			return this.ᜁ.ReadInnerXml();
		}

		// Token: 0x06004673 RID: 18035 RVA: 0x0040F4FC File Offset: 0x0040E4FC
		public byte[] ReadChildBinaryElement()
		{
			switch (0)
			{
			default:
			{
				byte[] array2;
				byte[] array;
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_F5:
					array = new byte[array2.Length * 2];
					num = 0;
					break;
				default:
					if (false)
					{
					}
					goto IL_47;
				}
				XmlReader xmlReader;
				int num2;
				for (;;)
				{
					IL_2C:
					switch (num)
					{
					case 0:
						if (xmlReader.EOF)
						{
							num = 3;
							continue;
						}
						goto IL_94;
					case 1:
						goto IL_94;
					case 2:
						if (true)
						{
						}
						if (num2 >= array.Length)
						{
							num = 4;
							continue;
						}
						return array2;
					case 3:
						return array2;
					case 4:
						goto IL_F5;
					}
					goto IL_47;
					IL_94:
					num2 = xmlReader.ReadElementContentAsBase64(array, 0, array.Length);
					byte[] array3 = new byte[array2.Length + num2];
					array2.CopyTo(array3, 0);
					Array.Copy(array, 0, array3, array2.Length, num2);
					array2 = array3;
					num = 2;
				}
				return array2;
				IL_47:
				xmlReader = this.ᜁ;
				num2 = 0;
				array2 = new byte[0];
				array = new byte[1000];
				num = 1;
				goto IL_2C;
			}
			}
		}

		// Token: 0x06004674 RID: 18036 RVA: 0x0040F604 File Offset: 0x0040E604
		internal Image ᜀ()
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			return this.ᜀ(false);
		}

		// Token: 0x06004675 RID: 18037 RVA: 0x0040F648 File Offset: 0x0040E648
		internal Image ᜀ(bool A_0)
		{
			Image result;
			for (;;)
			{
				result = null;
				byte[] array = this.ReadChildBinaryElement();
				int num = 0;
				for (;;)
				{
					MemoryStream stream;
					switch (num)
					{
					case 0:
						if (array.Length > 0)
						{
							num = 3;
							continue;
						}
						return result;
					case 1:
						return result;
					case 2:
						return result;
					case 3:
						goto IL_6D;
					case 4:
						result = new Metafile(stream);
						num = 2;
						continue;
					case 5:
						if (!A_0)
						{
							result = new Bitmap(stream);
							num = 1;
							continue;
						}
						if (true)
						{
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_6D;
						default:
							if (false)
							{
							}
							num = 4;
							continue;
						}
						break;
					}
					break;
					IL_6D:
					stream = new MemoryStream(array);
					num = 5;
				}
			}
			return result;
		}

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x06004676 RID: 18038 RVA: 0x0040F708 File Offset: 0x0040E708
		public XmlReader InnerReader
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				return this.ᜁ;
			}
		}

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x06004677 RID: 18039 RVA: 0x0040F74C File Offset: 0x0040E74C
		public IXDLSAttributeReader AttributeReader
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				return this;
			}
		}

		// Token: 0x06004678 RID: 18040 RVA: 0x0040F788 File Offset: 0x0040E788
		private void ᜀ(IDocumentSerializable A_0)
		{
			int a_ = 15;
			int num = 7;
			for (;;)
			{
				bool flag;
				int depth;
				switch (num)
				{
				case 0:
					goto IL_87;
				case 1:
					if (flag)
					{
						num = 15;
						continue;
					}
					return;
				case 2:
					if (this.ᜁ.Depth > depth)
					{
						num = 5;
						continue;
					}
					goto IL_1FB;
				case 3:
					return;
				case 4:
					if (!A_0.ReadXmlContent(this))
					{
						num = 9;
						continue;
					}
					goto IL_228;
				case 5:
					if (true)
					{
					}
					num = 10;
					continue;
				case 6:
					this.ᜁ.ReadEndElement();
					goto IL_1A8;
				case 8:
					A_0.XDLSHolder.ID = XmlConvert.ToInt32(this.ᜁ.GetAttribute(ClipboardData.b("ᱴ፶", a_)));
					num = 13;
					continue;
				case 9:
					this.ᜁ.Skip();
					num = 17;
					continue;
				case 10:
					if (this.ᜁ.EOF)
					{
						num = 11;
						continue;
					}
					num = 12;
					continue;
				case 11:
					goto IL_1FB;
				case 12:
					if (this.ᜁ.NodeType != XmlNodeType.Element)
					{
						num = 14;
						continue;
					}
					num = 4;
					continue;
				case 13:
					goto IL_25B;
				case 14:
					this.ᜁ.Read();
					num = 20;
					continue;
				case 15:
					num = 19;
					continue;
				case 16:
					num = 23;
					continue;
				case 17:
					goto IL_228;
				case 18:
					goto IL_287;
				case 19:
					goto IL_228;
				case 20:
					goto IL_228;
				case 21:
					if (!this.ᜁ.HasAttributes)
					{
						goto IL_287;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1A8;
					default:
						if (false)
						{
						}
						num = 16;
						continue;
					}
					break;
				case 22:
					if (this.ᜁ.NodeType == XmlNodeType.EndElement)
					{
						num = 6;
						continue;
					}
					return;
				case 23:
					if (this.ᜁ.MoveToAttribute(ClipboardData.b("ᱴ፶", a_)))
					{
						num = 8;
						continue;
					}
					goto IL_25B;
				}
				if (A_0 == null)
				{
					num = 0;
					continue;
				}
				num = 21;
				continue;
				IL_1A8:
				num = 3;
				continue;
				IL_1FB:
				num = 22;
				continue;
				IL_228:
				num = 2;
				continue;
				IL_25B:
				A_0.ReadXmlAttributes(this);
				this.ᜁ.MoveToElement();
				num = 18;
				continue;
				IL_287:
				flag = !this.ᜁ.IsEmptyElement;
				depth = this.ᜁ.Depth;
				this.ᜁ.ReadStartElement();
				num = 1;
			}
			IL_87:
			this.ᜁ.Skip();
		}

		// Token: 0x06004679 RID: 18041 RVA: 0x0040FA8C File Offset: 0x0040EA8C
		private void ᜀ(IXDLSSerializableCollection A_0)
		{
			for (;;)
			{
				bool flag = !this.ᜁ.IsEmptyElement;
				int depth = this.ᜁ.Depth;
				this.ᜁ.ReadStartElement();
				int num = 9;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_F9;
					case 1:
						num = 6;
						continue;
					case 2:
					{
						IDocumentSerializable a_ = A_0.AddNewItem(this);
						this.ᜀ(a_);
						num = 11;
						continue;
					}
					case 3:
						goto IL_138;
					case 4:
						if (this.ᜁ.NodeType == XmlNodeType.EndElement)
						{
							num = 5;
							continue;
						}
						return;
					case 5:
						this.ᜁ.ReadEndElement();
						num = 12;
						continue;
					case 6:
						if (this.ᜁ.EOF)
						{
							num = 0;
							continue;
						}
						num = 10;
						continue;
					case 7:
						goto IL_13A;
					case 8:
						if (this.ᜁ.Depth > depth)
						{
							num = 1;
							continue;
						}
						goto IL_F9;
					case 9:
						if (flag)
						{
							num = 14;
							continue;
						}
						return;
					case 10:
						if (this.ᜁ.NodeType != XmlNodeType.Element)
						{
							num = 13;
							continue;
						}
						num = 15;
						continue;
					case 11:
						goto IL_13A;
					case 12:
						goto IL_179;
					case 13:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_138;
						default:
							if (false)
							{
							}
							this.ᜁ.Read();
							num = 7;
							continue;
						}
						break;
					case 14:
						num = 3;
						continue;
					case 15:
						if (this.ᜁ.LocalName == A_0.TagItemName)
						{
							num = 2;
							continue;
						}
						goto IL_13A;
					}
					break;
					IL_F9:
					num = 4;
					continue;
					IL_13A:
					num = 8;
					continue;
					IL_138:
					goto IL_13A;
				}
			}
			IL_179:
			if (true)
			{
			}
		}

		// Token: 0x0600467A RID: 18042 RVA: 0x0040FC88 File Offset: 0x0040EC88
		// Note: this type is marked as 'beforefieldinit'.
		static XDLSReader()
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			XDLSReader.ᜀ = new Dictionary<Type, object>();
		}

		// Token: 0x040036A5 RID: 13989
		private bool \u2593\u0091\u0090\u00A8;

		// Token: 0x040036A6 RID: 13990
		private static Dictionary<Type, object> ᜀ;

		// Token: 0x040036A7 RID: 13991
		private XmlReader ᜁ;

		// Token: 0x040036A8 RID: 13992
		private spr\u1B3B ᜂ = new spr\u1B3B();
	}
}
