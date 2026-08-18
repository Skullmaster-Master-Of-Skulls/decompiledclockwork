using System;
using System.Collections.Generic;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x020009D8 RID: 2520
	internal class BinaryFormatBuilder
	{
		// Token: 0x0600639C RID: 25500 RVA: 0x00174014 File Offset: 0x00172214
		public BinaryFormatBuilder()
		{
			this.bytes = new List<byte>();
		}

		// Token: 0x1700180F RID: 6159
		// (get) Token: 0x0600639D RID: 25501 RVA: 0x00174027 File Offset: 0x00172227
		public int Count
		{
			get
			{
				return this.bytes.Count;
			}
		}

		// Token: 0x0600639E RID: 25502 RVA: 0x00174034 File Offset: 0x00172234
		public void AppendPrefixDictionaryElement(char prefix, int key)
		{
			this.AppendNode(XmlBinaryNodeType.PrefixDictionaryElementA + this.GetPrefixOffset(prefix));
			this.AppendKey(key);
		}

		// Token: 0x0600639F RID: 25503 RVA: 0x0017404D File Offset: 0x0017224D
		public void AppendDictionaryXmlnsAttribute(char prefix, int key)
		{
			this.AppendNode(XmlBinaryNodeType.DictionaryXmlnsAttribute);
			this.AppendUtf8(prefix);
			this.AppendKey(key);
		}

		// Token: 0x060063A0 RID: 25504 RVA: 0x00174065 File Offset: 0x00172265
		public void AppendPrefixDictionaryAttribute(char prefix, int key, char value)
		{
			this.AppendNode(XmlBinaryNodeType.PrefixDictionaryAttributeA + this.GetPrefixOffset(prefix));
			this.AppendKey(key);
			if (value == '1')
			{
				this.AppendNode(XmlBinaryNodeType.OneText);
				return;
			}
			this.AppendNode(XmlBinaryNodeType.Chars8Text);
			this.AppendUtf8(value);
		}

		// Token: 0x060063A1 RID: 25505 RVA: 0x001740A1 File Offset: 0x001722A1
		public void AppendDictionaryAttribute(char prefix, int key, char value)
		{
			this.AppendNode(XmlBinaryNodeType.DictionaryAttribute);
			this.AppendUtf8(prefix);
			this.AppendKey(key);
			this.AppendNode(XmlBinaryNodeType.Chars8Text);
			this.AppendUtf8(value);
		}

		// Token: 0x060063A2 RID: 25506 RVA: 0x001740CA File Offset: 0x001722CA
		public void AppendDictionaryTextWithEndElement(int key)
		{
			this.AppendNode(XmlBinaryNodeType.DictionaryTextWithEndElement);
			this.AppendKey(key);
		}

		// Token: 0x060063A3 RID: 25507 RVA: 0x001740DE File Offset: 0x001722DE
		public void AppendDictionaryTextWithEndElement()
		{
			this.AppendNode(XmlBinaryNodeType.DictionaryTextWithEndElement);
		}

		// Token: 0x060063A4 RID: 25508 RVA: 0x001740EB File Offset: 0x001722EB
		public void AppendUniqueIDWithEndElement()
		{
			this.AppendNode(XmlBinaryNodeType.UniqueIdTextWithEndElement);
		}

		// Token: 0x060063A5 RID: 25509 RVA: 0x001740F8 File Offset: 0x001722F8
		public void AppendEndElement()
		{
			this.AppendNode(XmlBinaryNodeType.EndElement);
		}

		// Token: 0x060063A6 RID: 25510 RVA: 0x00174104 File Offset: 0x00172304
		private void AppendKey(int key)
		{
			if (key < 0 || key >= 16384)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("key", key, SR.GetString("ValueMustBeInRange", new object[]
				{
					0,
					16384
				})));
			}
			if (key >= 128)
			{
				this.AppendByte((key & 127) | 128);
				this.AppendByte(key >> 7);
				return;
			}
			this.AppendByte(key);
		}

		// Token: 0x060063A7 RID: 25511 RVA: 0x00174187 File Offset: 0x00172387
		private void AppendNode(XmlBinaryNodeType value)
		{
			this.AppendByte((int)value);
		}

		// Token: 0x060063A8 RID: 25512 RVA: 0x00174190 File Offset: 0x00172390
		private void AppendByte(int value)
		{
			if (value < 0 || value > 255)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value, SR.GetString("ValueMustBeInRange", new object[]
				{
					0,
					255
				})));
			}
			this.bytes.Add((byte)value);
		}

		// Token: 0x060063A9 RID: 25513 RVA: 0x001741F7 File Offset: 0x001723F7
		private void AppendUtf8(char value)
		{
			this.AppendByte(1);
			this.AppendByte((int)value);
		}

		// Token: 0x060063AA RID: 25514 RVA: 0x00174207 File Offset: 0x00172407
		public int GetStaticKey(int value)
		{
			return value * 2;
		}

		// Token: 0x060063AB RID: 25515 RVA: 0x0017420C File Offset: 0x0017240C
		public int GetSessionKey(int value)
		{
			return value * 2 + 1;
		}

		// Token: 0x060063AC RID: 25516 RVA: 0x00174214 File Offset: 0x00172414
		private int GetPrefixOffset(char prefix)
		{
			if (prefix < 'a' && prefix > 'z')
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("prefix", prefix, SR.GetString("ValueMustBeInRange", new object[]
				{
					'a',
					'z'
				})));
			}
			return (int)(prefix - 'a');
		}

		// Token: 0x060063AD RID: 25517 RVA: 0x00174270 File Offset: 0x00172470
		public byte[] ToByteArray()
		{
			byte[] result = this.bytes.ToArray();
			this.bytes.Clear();
			return result;
		}

		// Token: 0x04003985 RID: 14725
		private List<byte> bytes;
	}
}
