using System;
using System.Xml;

namespace System.IdentityModel
{
	// Token: 0x020000B7 RID: 183
	internal sealed class XmlTokenStream : ISecurityElement
	{
		// Token: 0x0600058F RID: 1423 RVA: 0x00014EBB File Offset: 0x000130BB
		public XmlTokenStream(int initialSize)
		{
			if (initialSize < 1)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("initialSize", SR.GetString("ValueMustBeGreaterThanZero")));
			}
			this.entries = new XmlTokenStream.XmlTokenEntry[initialSize];
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x00014EF4 File Offset: 0x000130F4
		public XmlTokenStream(XmlTokenStream other)
		{
			this.count = other.count;
			this.excludedElement = other.excludedElement;
			this.excludedElementDepth = other.excludedElementDepth;
			this.excludedElementNamespace = other.excludedElementNamespace;
			this.entries = new XmlTokenStream.XmlTokenEntry[this.count];
			Array.Copy(other.entries, this.entries, this.count);
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x00014F60 File Offset: 0x00013160
		public void Add(XmlNodeType type, string value)
		{
			this.EnsureCapacityToAdd();
			XmlTokenStream.XmlTokenEntry[] array = this.entries;
			int num = this.count;
			this.count = num + 1;
			array[num].Set(type, value);
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x00014F98 File Offset: 0x00013198
		public void AddAttribute(string prefix, string localName, string namespaceUri, string value)
		{
			this.EnsureCapacityToAdd();
			XmlTokenStream.XmlTokenEntry[] array = this.entries;
			int num = this.count;
			this.count = num + 1;
			array[num].SetAttribute(prefix, localName, namespaceUri, value);
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x00014FD4 File Offset: 0x000131D4
		public void AddElement(string prefix, string localName, string namespaceUri, bool isEmptyElement)
		{
			this.EnsureCapacityToAdd();
			XmlTokenStream.XmlTokenEntry[] array = this.entries;
			int num = this.count;
			this.count = num + 1;
			array[num].SetElement(prefix, localName, namespaceUri, isEmptyElement);
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x00015010 File Offset: 0x00013210
		private void EnsureCapacityToAdd()
		{
			if (this.count == this.entries.Length)
			{
				XmlTokenStream.XmlTokenEntry[] destinationArray = new XmlTokenStream.XmlTokenEntry[this.entries.Length * 2];
				Array.Copy(this.entries, 0, destinationArray, 0, this.count);
				this.entries = destinationArray;
			}
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x00015058 File Offset: 0x00013258
		public void SetElementExclusion(string excludedElement, string excludedElementNamespace)
		{
			this.SetElementExclusion(excludedElement, excludedElementNamespace, null);
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x00015076 File Offset: 0x00013276
		public void SetElementExclusion(string excludedElement, string excludedElementNamespace, int? excludedElementDepth)
		{
			this.excludedElement = excludedElement;
			this.excludedElementDepth = excludedElementDepth;
			this.excludedElementNamespace = excludedElementNamespace;
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x0001508D File Offset: 0x0001328D
		public XmlTokenStream Trim()
		{
			return new XmlTokenStream(this);
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x00015095 File Offset: 0x00013295
		public XmlTokenStream.XmlTokenStreamWriter GetWriter()
		{
			return new XmlTokenStream.XmlTokenStreamWriter(this.entries, this.count, this.excludedElement, this.excludedElementDepth, this.excludedElementNamespace);
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x000150BA File Offset: 0x000132BA
		public void WriteTo(XmlDictionaryWriter writer, DictionaryManager dictionaryManager)
		{
			this.GetWriter().WriteTo(writer, dictionaryManager);
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x0600059A RID: 1434 RVA: 0x00002D09 File Offset: 0x00000F09
		bool ISecurityElement.HasId
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x0600059B RID: 1435 RVA: 0x00003459 File Offset: 0x00001659
		string ISecurityElement.Id
		{
			get
			{
				return null;
			}
		}

		// Token: 0x040004D7 RID: 1239
		private int count;

		// Token: 0x040004D8 RID: 1240
		private XmlTokenStream.XmlTokenEntry[] entries;

		// Token: 0x040004D9 RID: 1241
		private string excludedElement;

		// Token: 0x040004DA RID: 1242
		private int? excludedElementDepth;

		// Token: 0x040004DB RID: 1243
		private string excludedElementNamespace;

		// Token: 0x0200023E RID: 574
		internal class XmlTokenStreamWriter : ISecurityElement
		{
			// Token: 0x0600122D RID: 4653 RVA: 0x0004FBED File Offset: 0x0004DDED
			public XmlTokenStreamWriter(XmlTokenStream.XmlTokenEntry[] entries, int count, string excludedElement, int? excludedElementDepth, string excludedElementNamespace)
			{
				if (entries == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("entries");
				}
				this.entries = entries;
				this.count = count;
				this.excludedElement = excludedElement;
				this.excludedElementDepth = excludedElementDepth;
				this.excludedElementNamespace = excludedElementNamespace;
			}

			// Token: 0x1700050E RID: 1294
			// (get) Token: 0x0600122E RID: 4654 RVA: 0x0004FC2D File Offset: 0x0004DE2D
			public int Count
			{
				get
				{
					return this.count;
				}
			}

			// Token: 0x1700050F RID: 1295
			// (get) Token: 0x0600122F RID: 4655 RVA: 0x0004FC35 File Offset: 0x0004DE35
			public int Position
			{
				get
				{
					return this.position;
				}
			}

			// Token: 0x17000510 RID: 1296
			// (get) Token: 0x06001230 RID: 4656 RVA: 0x0004FC3D File Offset: 0x0004DE3D
			public XmlNodeType NodeType
			{
				get
				{
					return this.entries[this.position].nodeType;
				}
			}

			// Token: 0x17000511 RID: 1297
			// (get) Token: 0x06001231 RID: 4657 RVA: 0x0004FC55 File Offset: 0x0004DE55
			public bool IsEmptyElement
			{
				get
				{
					return this.entries[this.position].IsEmptyElement;
				}
			}

			// Token: 0x17000512 RID: 1298
			// (get) Token: 0x06001232 RID: 4658 RVA: 0x0004FC6D File Offset: 0x0004DE6D
			public string Prefix
			{
				get
				{
					return this.entries[this.position].prefix;
				}
			}

			// Token: 0x17000513 RID: 1299
			// (get) Token: 0x06001233 RID: 4659 RVA: 0x0004FC85 File Offset: 0x0004DE85
			public string LocalName
			{
				get
				{
					return this.entries[this.position].localName;
				}
			}

			// Token: 0x17000514 RID: 1300
			// (get) Token: 0x06001234 RID: 4660 RVA: 0x0004FC9D File Offset: 0x0004DE9D
			public string NamespaceUri
			{
				get
				{
					return this.entries[this.position].namespaceUri;
				}
			}

			// Token: 0x17000515 RID: 1301
			// (get) Token: 0x06001235 RID: 4661 RVA: 0x0004FCB5 File Offset: 0x0004DEB5
			public string Value
			{
				get
				{
					return this.entries[this.position].Value;
				}
			}

			// Token: 0x17000516 RID: 1302
			// (get) Token: 0x06001236 RID: 4662 RVA: 0x0004FCCD File Offset: 0x0004DECD
			public string ExcludedElement
			{
				get
				{
					return this.excludedElement;
				}
			}

			// Token: 0x17000517 RID: 1303
			// (get) Token: 0x06001237 RID: 4663 RVA: 0x0004FCD5 File Offset: 0x0004DED5
			public string ExcludedElementNamespace
			{
				get
				{
					return this.excludedElementNamespace;
				}
			}

			// Token: 0x17000518 RID: 1304
			// (get) Token: 0x06001238 RID: 4664 RVA: 0x00002D09 File Offset: 0x00000F09
			bool ISecurityElement.HasId
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000519 RID: 1305
			// (get) Token: 0x06001239 RID: 4665 RVA: 0x00003459 File Offset: 0x00001659
			string ISecurityElement.Id
			{
				get
				{
					return null;
				}
			}

			// Token: 0x0600123A RID: 4666 RVA: 0x0004FCDD File Offset: 0x0004DEDD
			public bool MoveToFirst()
			{
				this.position = 0;
				return this.count > 0;
			}

			// Token: 0x0600123B RID: 4667 RVA: 0x0004FCEF File Offset: 0x0004DEEF
			public bool MoveToFirstAttribute()
			{
				if (this.position < this.Count - 1 && this.entries[this.position + 1].nodeType == XmlNodeType.Attribute)
				{
					this.position++;
					return true;
				}
				return false;
			}

			// Token: 0x0600123C RID: 4668 RVA: 0x0004FD2D File Offset: 0x0004DF2D
			public bool MoveToNext()
			{
				if (this.position < this.count - 1)
				{
					this.position++;
					return true;
				}
				return false;
			}

			// Token: 0x0600123D RID: 4669 RVA: 0x0004FD50 File Offset: 0x0004DF50
			public bool MoveToNextAttribute()
			{
				if (this.position < this.count - 1 && this.entries[this.position + 1].nodeType == XmlNodeType.Attribute)
				{
					this.position++;
					return true;
				}
				return false;
			}

			// Token: 0x0600123E RID: 4670 RVA: 0x0004FD90 File Offset: 0x0004DF90
			public void WriteTo(XmlDictionaryWriter writer, DictionaryManager dictionaryManager)
			{
				if (writer == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("writer"));
				}
				if (!this.MoveToFirst())
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("XmlTokenBufferIsEmpty")));
				}
				int num = 0;
				int num2 = -1;
				bool flag = true;
				for (;;)
				{
					switch (this.NodeType)
					{
					case XmlNodeType.Element:
					{
						bool isEmptyElement = this.IsEmptyElement;
						num++;
						if (flag)
						{
							if (this.excludedElementDepth != null)
							{
								int? num3 = this.excludedElementDepth;
								int num4 = num - 1;
								if (!(num3.GetValueOrDefault() == num4 & num3 != null))
								{
									goto IL_101;
								}
							}
							if (this.LocalName == this.excludedElement && this.NamespaceUri == this.excludedElementNamespace)
							{
								flag = false;
								num2 = num;
							}
						}
						IL_101:
						if (flag)
						{
							writer.WriteStartElement(this.Prefix, this.LocalName, this.NamespaceUri);
						}
						if (this.MoveToFirstAttribute())
						{
							do
							{
								if (flag)
								{
									writer.WriteAttributeString(this.Prefix, this.LocalName, this.NamespaceUri, this.Value);
								}
							}
							while (this.MoveToNextAttribute());
						}
						if (isEmptyElement)
						{
							goto IL_150;
						}
						break;
					}
					case XmlNodeType.Text:
						if (flag)
						{
							writer.WriteString(this.Value);
						}
						break;
					case XmlNodeType.CDATA:
						if (flag)
						{
							writer.WriteCData(this.Value);
						}
						break;
					case XmlNodeType.Comment:
						if (flag)
						{
							writer.WriteComment(this.Value);
						}
						break;
					case XmlNodeType.Whitespace:
					case XmlNodeType.SignificantWhitespace:
						if (flag)
						{
							writer.WriteWhitespace(this.Value);
						}
						break;
					case XmlNodeType.EndElement:
						goto IL_150;
					}
					IL_1AB:
					if (!this.MoveToNext())
					{
						break;
					}
					continue;
					IL_150:
					if (flag)
					{
						writer.WriteEndElement();
					}
					else if (num2 == num)
					{
						flag = true;
						num2 = -1;
					}
					num--;
					goto IL_1AB;
				}
			}

			// Token: 0x04000F6D RID: 3949
			private XmlTokenStream.XmlTokenEntry[] entries;

			// Token: 0x04000F6E RID: 3950
			private int count;

			// Token: 0x04000F6F RID: 3951
			private int position;

			// Token: 0x04000F70 RID: 3952
			private string excludedElement;

			// Token: 0x04000F71 RID: 3953
			private int? excludedElementDepth;

			// Token: 0x04000F72 RID: 3954
			private string excludedElementNamespace;
		}

		// Token: 0x0200023F RID: 575
		internal struct XmlTokenEntry
		{
			// Token: 0x1700051A RID: 1306
			// (get) Token: 0x0600123F RID: 4671 RVA: 0x0004FF53 File Offset: 0x0004E153
			// (set) Token: 0x06001240 RID: 4672 RVA: 0x0004FF5E File Offset: 0x0004E15E
			public bool IsEmptyElement
			{
				get
				{
					return this.value == null;
				}
				set
				{
					this.value = (value ? null : "");
				}
			}

			// Token: 0x1700051B RID: 1307
			// (get) Token: 0x06001241 RID: 4673 RVA: 0x0004FF71 File Offset: 0x0004E171
			public string Value
			{
				get
				{
					return this.value;
				}
			}

			// Token: 0x06001242 RID: 4674 RVA: 0x0004FF79 File Offset: 0x0004E179
			public void Set(XmlNodeType nodeType, string value)
			{
				this.nodeType = nodeType;
				this.value = value;
			}

			// Token: 0x06001243 RID: 4675 RVA: 0x0004FF89 File Offset: 0x0004E189
			public void SetAttribute(string prefix, string localName, string namespaceUri, string value)
			{
				this.nodeType = XmlNodeType.Attribute;
				this.prefix = prefix;
				this.localName = localName;
				this.namespaceUri = namespaceUri;
				this.value = value;
			}

			// Token: 0x06001244 RID: 4676 RVA: 0x0004FFAF File Offset: 0x0004E1AF
			public void SetElement(string prefix, string localName, string namespaceUri, bool isEmptyElement)
			{
				this.nodeType = XmlNodeType.Element;
				this.prefix = prefix;
				this.localName = localName;
				this.namespaceUri = namespaceUri;
				this.IsEmptyElement = isEmptyElement;
			}

			// Token: 0x04000F73 RID: 3955
			internal XmlNodeType nodeType;

			// Token: 0x04000F74 RID: 3956
			internal string prefix;

			// Token: 0x04000F75 RID: 3957
			internal string localName;

			// Token: 0x04000F76 RID: 3958
			internal string namespaceUri;

			// Token: 0x04000F77 RID: 3959
			private string value;
		}
	}
}
