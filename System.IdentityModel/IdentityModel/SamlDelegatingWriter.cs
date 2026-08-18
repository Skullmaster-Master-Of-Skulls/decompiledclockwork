using System;
using System.IO;
using System.Xml;
using System.Xml.XPath;

namespace System.IdentityModel
{
	// Token: 0x02000071 RID: 113
	internal sealed class SamlDelegatingWriter : XmlDictionaryWriter
	{
		// Token: 0x06000368 RID: 872 RVA: 0x0000D6D0 File Offset: 0x0000B8D0
		public SamlDelegatingWriter(XmlDictionaryWriter innerWriter, Stream canonicalStream, ICanonicalWriterEndRootElementCallback callback, IXmlDictionary dictionary)
		{
			if (innerWriter == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("innerWriter");
			}
			if (canonicalStream == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("canonicalStream");
			}
			if (callback == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("callback");
			}
			if (dictionary == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("dictionary");
			}
			this.innerWriter = innerWriter;
			this.canonicalStream = canonicalStream;
			this.callback = callback;
			this.dictionary = dictionary;
			this.elementCount = 0;
			this.startFragment = new MemoryStream();
			this.signatureFragment = new MemoryStream();
			this.endFragment = new MemoryStream();
			this.writerStream = new MemoryStream();
			this.effectiveWriter = XmlDictionaryWriter.CreateBinaryWriter(this.writerStream, this.dictionary);
			this.effectiveWriter.StartCanonicalization(this.canonicalStream, false, null);
			((IFragmentCapableXmlDictionaryWriter)this.effectiveWriter).StartFragment(this.startFragment, false);
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0000D7C4 File Offset: 0x0000B9C4
		private void OnEndOfRootElement()
		{
			this.elementCount--;
			if (this.elementCount == 0 && this.endFragment.Length == 0L)
			{
				((IFragmentCapableXmlDictionaryWriter)this.effectiveWriter).EndFragment();
				((IFragmentCapableXmlDictionaryWriter)this.effectiveWriter).StartFragment(this.endFragment, false);
				this.effectiveWriter.WriteEndElement();
				((IFragmentCapableXmlDictionaryWriter)this.effectiveWriter).EndFragment();
				this.effectiveWriter.EndCanonicalization();
				((IFragmentCapableXmlDictionaryWriter)this.effectiveWriter).StartFragment(this.signatureFragment, false);
				this.callback.OnEndOfRootElement(this);
				return;
			}
			if (this.elementCount == 0)
			{
				this.effectiveWriter.WriteEndElement();
				((IFragmentCapableXmlDictionaryWriter)this.effectiveWriter).EndFragment();
				((IFragmentCapableXmlDictionaryWriter)this.effectiveWriter).WriteFragment(this.startFragment.GetBuffer(), 0, (int)this.startFragment.Length);
				((IFragmentCapableXmlDictionaryWriter)this.effectiveWriter).WriteFragment(this.signatureFragment.GetBuffer(), 0, (int)this.signatureFragment.Length);
				((IFragmentCapableXmlDictionaryWriter)this.effectiveWriter).WriteFragment(this.endFragment.GetBuffer(), 0, (int)this.endFragment.Length);
				this.startFragment.Close();
				this.signatureFragment.Close();
				this.endFragment.Close();
				this.writerStream.Position = 0L;
				XmlDictionaryReader xmlDictionaryReader = XmlDictionaryReader.CreateBinaryReader(this.writerStream, this.dictionary, XmlDictionaryReaderQuotas.Max);
				xmlDictionaryReader.MoveToContent();
				this.innerWriter.WriteNode(xmlDictionaryReader, false);
				this.innerWriter.Flush();
				xmlDictionaryReader.Close();
				this.writerStream.Close();
				this.effectiveWriter.Close();
				return;
			}
			this.effectiveWriter.WriteEndElement();
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0000D98F File Offset: 0x0000BB8F
		public override void Close()
		{
			this.effectiveWriter.Close();
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0000D99C File Offset: 0x0000BB9C
		public override void Flush()
		{
			this.effectiveWriter.Flush();
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0000D9A9 File Offset: 0x0000BBA9
		public override void WriteArray(string prefix, string localName, string namespaceUri, bool[] array, int offset, int count)
		{
			this.effectiveWriter.WriteArray(prefix, localName, namespaceUri, array, offset, count);
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0000D9BF File Offset: 0x0000BBBF
		public override void WriteArray(string prefix, string localName, string namespaceUri, double[] array, int offset, int count)
		{
			this.effectiveWriter.WriteArray(prefix, localName, namespaceUri, array, offset, count);
		}

		// Token: 0x0600036E RID: 878 RVA: 0x0000D9D5 File Offset: 0x0000BBD5
		public override void WriteArray(string prefix, string localName, string namespaceUri, decimal[] array, int offset, int count)
		{
			this.effectiveWriter.WriteArray(prefix, localName, namespaceUri, array, offset, count);
		}

		// Token: 0x0600036F RID: 879 RVA: 0x0000D9EB File Offset: 0x0000BBEB
		public override void WriteArray(string prefix, string localName, string namespaceUri, float[] array, int offset, int count)
		{
			this.effectiveWriter.WriteArray(prefix, localName, namespaceUri, array, offset, count);
		}

		// Token: 0x06000370 RID: 880 RVA: 0x0000DA01 File Offset: 0x0000BC01
		public override void WriteArray(string prefix, string localName, string namespaceUri, int[] array, int offset, int count)
		{
			this.effectiveWriter.WriteArray(prefix, localName, namespaceUri, array, offset, count);
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0000DA17 File Offset: 0x0000BC17
		public override void WriteArray(string prefix, string localName, string namespaceUri, long[] array, int offset, int count)
		{
			this.effectiveWriter.WriteArray(prefix, localName, namespaceUri, array, offset, count);
		}

		// Token: 0x06000372 RID: 882 RVA: 0x0000DA2D File Offset: 0x0000BC2D
		public override void WriteArray(string prefix, string localName, string namespaceUri, short[] array, int offset, int count)
		{
			this.effectiveWriter.WriteArray(prefix, localName, namespaceUri, array, offset, count);
		}

		// Token: 0x06000373 RID: 883 RVA: 0x0000DA43 File Offset: 0x0000BC43
		public override void WriteArray(string prefix, string localName, string namespaceUri, DateTime[] array, int offset, int count)
		{
			this.effectiveWriter.WriteArray(prefix, localName, namespaceUri, array, offset, count);
		}

		// Token: 0x06000374 RID: 884 RVA: 0x0000DA59 File Offset: 0x0000BC59
		public override void WriteArray(string prefix, string localName, string namespaceUri, Guid[] array, int offset, int count)
		{
			this.effectiveWriter.WriteArray(prefix, localName, namespaceUri, array, offset, count);
		}

		// Token: 0x06000375 RID: 885 RVA: 0x0000DA6F File Offset: 0x0000BC6F
		public override void WriteArray(string prefix, string localName, string namespaceUri, TimeSpan[] array, int offset, int count)
		{
			this.effectiveWriter.WriteArray(prefix, localName, namespaceUri, array, offset, count);
		}

		// Token: 0x06000376 RID: 886 RVA: 0x0000DA85 File Offset: 0x0000BC85
		public override void WriteArray(string prefix, XmlDictionaryString localName, XmlDictionaryString namespaceUri, bool[] array, int offset, int count)
		{
			this.effectiveWriter.WriteArray(prefix, localName, namespaceUri, array, offset, count);
		}

		// Token: 0x06000377 RID: 887 RVA: 0x0000DA9B File Offset: 0x0000BC9B
		public override void WriteArray(string prefix, XmlDictionaryString localName, XmlDictionaryString namespaceUri, decimal[] array, int offset, int count)
		{
			this.effectiveWriter.WriteArray(prefix, localName, namespaceUri, array, offset, count);
		}

		// Token: 0x06000378 RID: 888 RVA: 0x0000DAB1 File Offset: 0x0000BCB1
		public override void WriteArray(string prefix, XmlDictionaryString localName, XmlDictionaryString namespaceUri, double[] array, int offset, int count)
		{
			this.effectiveWriter.WriteArray(prefix, localName, namespaceUri, array, offset, count);
		}

		// Token: 0x06000379 RID: 889 RVA: 0x0000DAC7 File Offset: 0x0000BCC7
		public override void WriteArray(string prefix, XmlDictionaryString localName, XmlDictionaryString namespaceUri, float[] array, int offset, int count)
		{
			this.effectiveWriter.WriteArray(prefix, localName, namespaceUri, array, offset, count);
		}

		// Token: 0x0600037A RID: 890 RVA: 0x0000DADD File Offset: 0x0000BCDD
		public override void WriteArray(string prefix, XmlDictionaryString localName, XmlDictionaryString namespaceUri, int[] array, int offset, int count)
		{
			this.effectiveWriter.WriteArray(prefix, localName, namespaceUri, array, offset, count);
		}

		// Token: 0x0600037B RID: 891 RVA: 0x0000DAF3 File Offset: 0x0000BCF3
		public override void WriteArray(string prefix, XmlDictionaryString localName, XmlDictionaryString namespaceUri, long[] array, int offset, int count)
		{
			this.effectiveWriter.WriteArray(prefix, localName, namespaceUri, array, offset, count);
		}

		// Token: 0x0600037C RID: 892 RVA: 0x0000DB09 File Offset: 0x0000BD09
		public override void WriteArray(string prefix, XmlDictionaryString localName, XmlDictionaryString namespaceUri, short[] array, int offset, int count)
		{
			this.effectiveWriter.WriteArray(prefix, localName, namespaceUri, array, offset, count);
		}

		// Token: 0x0600037D RID: 893 RVA: 0x0000DB1F File Offset: 0x0000BD1F
		public override void WriteArray(string prefix, XmlDictionaryString localName, XmlDictionaryString namespaceUri, DateTime[] array, int offset, int count)
		{
			this.effectiveWriter.WriteArray(prefix, localName, namespaceUri, array, offset, count);
		}

		// Token: 0x0600037E RID: 894 RVA: 0x0000DB35 File Offset: 0x0000BD35
		public override void WriteArray(string prefix, XmlDictionaryString localName, XmlDictionaryString namespaceUri, Guid[] array, int offset, int count)
		{
			this.effectiveWriter.WriteArray(prefix, localName, namespaceUri, array, offset, count);
		}

		// Token: 0x0600037F RID: 895 RVA: 0x0000DB4B File Offset: 0x0000BD4B
		public override void WriteArray(string prefix, XmlDictionaryString localName, XmlDictionaryString namespaceUri, TimeSpan[] array, int offset, int count)
		{
			this.effectiveWriter.WriteArray(prefix, localName, namespaceUri, array, offset, count);
		}

		// Token: 0x06000380 RID: 896 RVA: 0x0000DB61 File Offset: 0x0000BD61
		public override void WriteAttributes(XmlReader reader, bool defattr)
		{
			this.effectiveWriter.WriteAttributes(reader, defattr);
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0000DB70 File Offset: 0x0000BD70
		public override void WriteBase64(byte[] buffer, int index, int count)
		{
			this.effectiveWriter.WriteBase64(buffer, index, count);
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0000DB80 File Offset: 0x0000BD80
		public override void WriteBinHex(byte[] buffer, int index, int count)
		{
			this.effectiveWriter.WriteBinHex(buffer, index, count);
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0000DB90 File Offset: 0x0000BD90
		public override void WriteCData(string text)
		{
			this.effectiveWriter.WriteCData(text);
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0000DB9E File Offset: 0x0000BD9E
		public override void WriteCharEntity(char ch)
		{
			this.effectiveWriter.WriteCharEntity(ch);
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0000DBAC File Offset: 0x0000BDAC
		public override void WriteChars(char[] buffer, int index, int count)
		{
			this.effectiveWriter.WriteChars(buffer, index, count);
		}

		// Token: 0x06000386 RID: 902 RVA: 0x0000DBBC File Offset: 0x0000BDBC
		public override void WriteComment(string text)
		{
			this.effectiveWriter.WriteComment(text);
		}

		// Token: 0x06000387 RID: 903 RVA: 0x0000DBCA File Offset: 0x0000BDCA
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
			this.effectiveWriter.WriteDocType(name, pubid, sysid, subset);
		}

		// Token: 0x06000388 RID: 904 RVA: 0x0000DBDC File Offset: 0x0000BDDC
		public override void WriteEndAttribute()
		{
			this.effectiveWriter.WriteEndAttribute();
		}

		// Token: 0x06000389 RID: 905 RVA: 0x0000DBE9 File Offset: 0x0000BDE9
		public override void WriteEndDocument()
		{
			this.effectiveWriter.WriteEndDocument();
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0000DBF6 File Offset: 0x0000BDF6
		public override void WriteEndElement()
		{
			this.OnEndOfRootElement();
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0000DBFE File Offset: 0x0000BDFE
		public override void WriteEntityRef(string name)
		{
			this.effectiveWriter.WriteEntityRef(name);
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0000DC0C File Offset: 0x0000BE0C
		public override void WriteFullEndElement()
		{
			this.effectiveWriter.WriteFullEndElement();
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0000DC19 File Offset: 0x0000BE19
		public override void WriteName(string name)
		{
			this.effectiveWriter.WriteName(name);
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0000DC27 File Offset: 0x0000BE27
		public override void WriteNmToken(string name)
		{
			this.effectiveWriter.WriteNmToken(name);
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0000DC35 File Offset: 0x0000BE35
		public override void WriteNode(XmlDictionaryReader reader, bool defattr)
		{
			this.effectiveWriter.WriteNode(reader, defattr);
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0000DC44 File Offset: 0x0000BE44
		public override void WriteNode(XmlReader reader, bool defattr)
		{
			this.effectiveWriter.WriteNode(reader, defattr);
		}

		// Token: 0x06000391 RID: 913 RVA: 0x0000DC53 File Offset: 0x0000BE53
		public override void WriteNode(XPathNavigator navigator, bool defattr)
		{
			this.effectiveWriter.WriteNode(navigator, defattr);
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0000DC62 File Offset: 0x0000BE62
		public override void WriteProcessingInstruction(string name, string text)
		{
			this.effectiveWriter.WriteProcessingInstruction(name, text);
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0000DC71 File Offset: 0x0000BE71
		public override void WriteQualifiedName(string localName, string ns)
		{
			this.effectiveWriter.WriteQualifiedName(localName, ns);
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0000DC80 File Offset: 0x0000BE80
		public override void WriteQualifiedName(XmlDictionaryString localName, XmlDictionaryString namespaceUri)
		{
			this.effectiveWriter.WriteQualifiedName(localName, namespaceUri);
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0000DC8F File Offset: 0x0000BE8F
		public override void WriteRaw(char[] buffer, int index, int count)
		{
			this.effectiveWriter.WriteRaw(buffer, index, count);
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0000DC9F File Offset: 0x0000BE9F
		public override void WriteRaw(string data)
		{
			this.effectiveWriter.WriteRaw(data);
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0000DCAD File Offset: 0x0000BEAD
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			this.effectiveWriter.WriteStartAttribute(prefix, localName, ns);
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0000DCBD File Offset: 0x0000BEBD
		public override void WriteStartAttribute(string prefix, XmlDictionaryString localName, XmlDictionaryString namespaceUri)
		{
			this.effectiveWriter.WriteStartAttribute(prefix, localName, namespaceUri);
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0000DCCD File Offset: 0x0000BECD
		public override void WriteStartDocument()
		{
			this.effectiveWriter.WriteStartDocument();
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0000DCDA File Offset: 0x0000BEDA
		public override void WriteStartDocument(bool standalone)
		{
			this.effectiveWriter.WriteStartDocument(standalone);
		}

		// Token: 0x0600039B RID: 923 RVA: 0x0000DCE8 File Offset: 0x0000BEE8
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			this.elementCount++;
			this.effectiveWriter.WriteStartElement(prefix, localName, ns);
		}

		// Token: 0x0600039C RID: 924 RVA: 0x0000DD06 File Offset: 0x0000BF06
		public override void WriteStartElement(string prefix, XmlDictionaryString localName, XmlDictionaryString namespaceUri)
		{
			this.elementCount++;
			this.effectiveWriter.WriteStartElement(prefix, localName, namespaceUri);
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x0600039D RID: 925 RVA: 0x0000DD24 File Offset: 0x0000BF24
		public override WriteState WriteState
		{
			get
			{
				return this.effectiveWriter.WriteState;
			}
		}

		// Token: 0x0600039E RID: 926 RVA: 0x0000DD31 File Offset: 0x0000BF31
		public override void WriteString(string text)
		{
			this.effectiveWriter.WriteString(text);
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0000DD3F File Offset: 0x0000BF3F
		public override void WriteString(XmlDictionaryString value)
		{
			this.effectiveWriter.WriteString(value);
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0000DD4D File Offset: 0x0000BF4D
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			this.effectiveWriter.WriteSurrogateCharEntity(lowChar, highChar);
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x0000DD5C File Offset: 0x0000BF5C
		public override void WriteValue(bool value)
		{
			this.effectiveWriter.WriteValue(value);
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0000DD6A File Offset: 0x0000BF6A
		public override void WriteValue(decimal value)
		{
			this.effectiveWriter.WriteValue(value);
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x0000DD78 File Offset: 0x0000BF78
		public override void WriteValue(double value)
		{
			this.effectiveWriter.WriteValue(value);
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0000DD86 File Offset: 0x0000BF86
		public override void WriteValue(float value)
		{
			this.effectiveWriter.WriteValue(value);
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x0000DD94 File Offset: 0x0000BF94
		public override void WriteValue(int value)
		{
			this.effectiveWriter.WriteValue(value);
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x0000DDA2 File Offset: 0x0000BFA2
		public override void WriteValue(long value)
		{
			this.effectiveWriter.WriteValue(value);
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0000DDB0 File Offset: 0x0000BFB0
		public override void WriteValue(object value)
		{
			this.effectiveWriter.WriteValue(value);
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x0000DDBE File Offset: 0x0000BFBE
		public override void WriteValue(string value)
		{
			this.effectiveWriter.WriteValue(value);
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x0000DDCC File Offset: 0x0000BFCC
		public override void WriteValue(DateTime value)
		{
			this.effectiveWriter.WriteValue(value);
		}

		// Token: 0x060003AA RID: 938 RVA: 0x0000DDDA File Offset: 0x0000BFDA
		public override void WriteValue(Guid value)
		{
			this.effectiveWriter.WriteValue(value);
		}

		// Token: 0x060003AB RID: 939 RVA: 0x0000DDE8 File Offset: 0x0000BFE8
		public override void WriteValue(TimeSpan value)
		{
			this.effectiveWriter.WriteValue(value);
		}

		// Token: 0x060003AC RID: 940 RVA: 0x0000DDF6 File Offset: 0x0000BFF6
		public override void WriteValue(IStreamProvider value)
		{
			this.effectiveWriter.WriteValue(value);
		}

		// Token: 0x060003AD RID: 941 RVA: 0x0000DE04 File Offset: 0x0000C004
		public override void WriteValue(UniqueId value)
		{
			this.effectiveWriter.WriteValue(value);
		}

		// Token: 0x060003AE RID: 942 RVA: 0x0000DE12 File Offset: 0x0000C012
		public override void WriteValue(XmlDictionaryString value)
		{
			this.effectiveWriter.WriteValue(value);
		}

		// Token: 0x060003AF RID: 943 RVA: 0x0000DE20 File Offset: 0x0000C020
		public override void WriteWhitespace(string ws)
		{
			this.effectiveWriter.WriteWhitespace(ws);
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x0000DE2E File Offset: 0x0000C02E
		public override void WriteXmlAttribute(string localName, string value)
		{
			this.effectiveWriter.WriteXmlAttribute(localName, value);
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x0000DE3D File Offset: 0x0000C03D
		public override void WriteXmlAttribute(XmlDictionaryString localName, XmlDictionaryString value)
		{
			this.effectiveWriter.WriteXmlAttribute(localName, value);
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x0000DE4C File Offset: 0x0000C04C
		public override void WriteXmlnsAttribute(string prefix, string namespaceUri)
		{
			this.effectiveWriter.WriteXmlnsAttribute(prefix, namespaceUri);
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x0000DE5B File Offset: 0x0000C05B
		public override void WriteXmlnsAttribute(string prefix, XmlDictionaryString namespaceUri)
		{
			this.effectiveWriter.WriteXmlnsAttribute(prefix, namespaceUri);
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x0000DE6A File Offset: 0x0000C06A
		public override string LookupPrefix(string ns)
		{
			return this.effectiveWriter.LookupPrefix(ns);
		}

		// Token: 0x04000365 RID: 869
		private XmlDictionaryWriter innerWriter;

		// Token: 0x04000366 RID: 870
		private Stream canonicalStream;

		// Token: 0x04000367 RID: 871
		private ICanonicalWriterEndRootElementCallback callback;

		// Token: 0x04000368 RID: 872
		private IXmlDictionary dictionary;

		// Token: 0x04000369 RID: 873
		private int elementCount;

		// Token: 0x0400036A RID: 874
		private MemoryStream startFragment;

		// Token: 0x0400036B RID: 875
		private MemoryStream signatureFragment;

		// Token: 0x0400036C RID: 876
		private MemoryStream endFragment;

		// Token: 0x0400036D RID: 877
		private XmlDictionaryWriter effectiveWriter;

		// Token: 0x0400036E RID: 878
		private MemoryStream writerStream;
	}
}
