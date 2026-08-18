using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using Spire.Xls.Core.Spreadsheet.Collections;
using Spire.Xls.Core.Spreadsheet.Shapes;
using Spire.Xls.Core.Spreadsheet.XmlSerialization;

namespace Spire.Xls.Core.Spreadsheet.XmlReaders.Shapes
{
	// Token: 0x020001AE RID: 430
	public abstract class ShapeParser
	{
		// Token: 0x06001754 RID: 5972
		public abstract XlsShape ParseShapeType(XmlReader reader, ShapeCollectionBase shapes);

		// Token: 0x06001755 RID: 5973
		public abstract bool ParseShape(XmlReader reader, XlsShape defaultShape, RelationsCollection relations, string parentItemPath);

		// Token: 0x06001756 RID: 5974 RVA: 0x000E1310 File Offset: 0x000E0310
		public static Stream ReadNodeAsStream(XmlReader reader)
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
			return ShapeParser.ReadNodeAsStream(reader, false);
		}

		// Token: 0x06001757 RID: 5975 RVA: 0x000E1354 File Offset: 0x000E0354
		public static Stream ReadNodeAsStream(XmlReader reader, bool writeNamespaces)
		{
			int a_ = 5;
			while (reader == null)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				}
				if (true)
				{
				}
				if (false)
				{
				}
				throw new ArgumentNullException(RecordTableEnumerator.b("䤺堼帾╀♂㝄", a_));
			}
			MemoryStream memoryStream = new MemoryStream();
			XmlWriter xmlWriter = UtilityMethods.ᜀ(memoryStream, Encoding.UTF8);
			xmlWriter.WriteNode(reader, writeNamespaces);
			xmlWriter.Flush();
			return memoryStream;
		}

		// Token: 0x06001758 RID: 5976 RVA: 0x000E13D4 File Offset: 0x000E03D4
		public static void WriteNodeFromStream(XmlWriter writer, Stream stream)
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
			ShapeParser.WriteNodeFromStream(writer, stream, false);
		}

		// Token: 0x06001759 RID: 5977 RVA: 0x000E1418 File Offset: 0x000E0418
		public static void WriteNodeFromStream(XmlWriter writer, Stream stream, bool writeNamespaces)
		{
			int a_ = 8;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (stream == null)
					{
						num = 1;
						continue;
					}
					goto IL_A1;
				case 1:
					goto IL_83;
				case 3:
					goto IL_34;
				}
				if (writer == null)
				{
					num = 3;
				}
				else
				{
					num = 0;
				}
			}
			IL_34:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
			{
				IL_A1:
				XmlReader reader = UtilityMethods.ᜀ(stream);
				writer.WriteNode(reader, writeNamespaces);
				writer.Flush();
				return;
			}
			default:
				if (false)
				{
				}
				throw new ArgumentNullException(RecordTableEnumerator.b("䤽㈿⭁ぃ⍅㩇", a_));
			}
			IL_83:
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("䴽㐿ぁ⅃❅╇", a_));
		}

		// Token: 0x0600175A RID: 5978 RVA: 0x000E14DC File Offset: 0x000E04DC
		protected void ParseAnchor(XmlReader reader, XlsShape shape)
		{
			int a_ = 12;
			switch (0)
			{
			default:
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_AB;
					case 1:
					{
						if (shape == null)
						{
							num = 5;
							continue;
						}
						string text = reader.ReadElementContentAsString();
						string[] array = text.Split(new char[]
						{
							','
						});
						num = 6;
						continue;
					}
					case 3:
						goto IL_67;
					case 4:
						if (true)
						{
						}
						goto IL_AB;
					case 5:
						goto IL_A6;
					case 6:
					{
						string[] array;
						if (array.Length != 8)
						{
							num = 8;
							continue;
						}
						int num2 = 0;
						int num3 = array.Length;
						num = 0;
						continue;
					}
					case 7:
						goto IL_C7;
					case 8:
						goto IL_144;
					case 9:
					{
						int num2;
						int num3;
						if (num2 >= num3)
						{
							num = 7;
							continue;
						}
						string[] array;
						array[num2] = array[num2].Trim();
						num2++;
						num = 4;
						continue;
					}
					}
					if (reader == null)
					{
						num = 3;
						continue;
					}
					num = 1;
					continue;
					IL_AB:
					num = 9;
				}
				IL_67:
				throw new ArgumentNullException(RecordTableEnumerator.b("ぁ⅃❅ⱇ⽉㹋", a_));
				IL_A6:
				throw new ArgumentNullException(RecordTableEnumerator.b("ㅁⱃ❅㡇⽉", a_));
				IL_C7:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_A6;
				default:
				{
					if (false)
					{
					}
					sprᮋ sprᮋ = shape.ClientAnchor;
					string[] array;
					int num4 = int.Parse(array[0]);
					int a_2 = int.Parse(array[1]);
					sprᮋ.ᜇ(num4);
					sprᮋ.ᜀ(shape.ᜆ(num4 + 1, a_2, true));
					shape.ᜐ();
					num4 = int.Parse(array[2]);
					a_2 = int.Parse(array[3]);
					sprᮋ.ᜆ(num4);
					sprᮋ.ᜁ(shape.ᜆ(num4 + 1, a_2, false));
					num4 = int.Parse(array[4]);
					a_2 = int.Parse(array[5]);
					sprᮋ.ᜂ(num4);
					sprᮋ.ᜃ(shape.ᜆ(num4 + 1, a_2, true));
					num4 = int.Parse(array[6]);
					a_2 = int.Parse(array[7]);
					sprᮋ.ᜅ(num4);
					sprᮋ.ᜄ(shape.ᜆ(num4 + 1, a_2, false));
					shape.UpdateHeight();
					shape.UpdateWidth();
					return;
				}
				}
				IL_144:
				throw new XmlException(RecordTableEnumerator.b("ᕁ㙃⥅♇ⵉ汋⽍㹏ㅑ㱓㥕⩗穙㩛ㅝ቟ཡգብ", a_));
			}
			}
		}

		// Token: 0x0600175B RID: 5979 RVA: 0x000E1740 File Offset: 0x000E0740
		protected Dictionary<string, string> SplitStyle(string styleValue)
		{
			switch (0)
			{
			default:
			{
				Dictionary<string, string> dictionary;
				for (;;)
				{
					dictionary = new Dictionary<string, string>();
					int num = 4;
					for (;;)
					{
						int num3;
						switch (num)
						{
						case 0:
							goto IL_E4;
						case 1:
						{
							int num2;
							if (num2 >= 0)
							{
								num = 7;
								continue;
							}
							goto IL_5B;
						}
						case 2:
						{
							string[] array = styleValue.Split(new char[]
							{
								';'
							});
							num3 = 0;
							int num4 = array.Length;
							num = 8;
							continue;
						}
						case 3:
							goto IL_5B;
						case 4:
							if (styleValue != null)
							{
								num = 2;
								continue;
							}
							goto IL_137;
						case 5:
							goto IL_137;
						case 6:
						{
							int num4;
							if (num3 >= num4)
							{
								num = 5;
								continue;
							}
							string[] array;
							string text = array[num3];
							int num2 = text.IndexOf(':');
							num = 1;
							continue;
						}
						case 7:
						{
							int num2;
							string text;
							string key = text.Substring(0, num2).Trim();
							string value = text.Substring(num2 + 1, text.Length - num2 - 1).Trim();
							dictionary.Add(key, value);
							num = 3;
							continue;
						}
						case 8:
							goto IL_E4;
						}
						break;
						IL_5B:
						num3++;
						if (true)
						{
						}
						num = 0;
						continue;
						IL_E4:
						num = 6;
						continue;
						IL_137:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						}
						goto Block_4;
					}
				}
				Block_4:
				if (false)
				{
				}
				return dictionary;
			}
			}
		}
	}
}
