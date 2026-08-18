using System;
using System.Collections;
using System.IO;
using System.Security.Cryptography.Xml;
using System.Xml;
using Spire.CompoundFile.Doc;
using Spire.Doc.Fields.Shape;

// Token: 0x02000385 RID: 901
internal class spr\u233A
{
	// Token: 0x0600325B RID: 12891 RVA: 0x002E6638 File Offset: 0x002E5638
	internal spr\u233A(spr\u20C4 A_0)
	{
		int a_ = 14;
		this.ᜃ = spr\u25F8.ᜀ();
		base..ctor();
		string text = A_0.ᜀ(ClipboardData.b("㕳᩵ίᕹ๻᝽", a_), null);
		string a;
		if ((a = text) != null)
		{
			if (a == ClipboardData.b("ᱳɵ౷੹䙻兽꽿ꚇﶉ뾋ꂍﾏ릕첗좙뎛겝邟銡閣覥盛莭좯\udfb1\ud8b3鮵\udbb7讹袻킽劉韛ￋￍ", a_))
			{
				this.ᜂ = TransformType.TransformC14n;
				return;
			}
			if (a == ClipboardData.b("ᱳɵ౷੹䙻兽꽿ﶍ뺏﶑욟춡횣쮥즧\udea9\udfab肭\udfaf삱펳馵좷\udbb9\udfbb햽ꆿꗁꇃ難韛ﳋￏ胑뇓뫕맗껙뗛뇝軟釡賣迥飧뻩黫迭黯臱鋳駵諷韹", a_))
			{
				this.ᜂ = TransformType.TransformRelationship;
				while (A_0.ᜃ(ClipboardData.b("⁳ѵ᥷ᑹཻ᡽", a_)))
				{
					if (A_0.\u171F() == ClipboardData.b("♳፵ᑷ᭹ࡻ᝽憎\ude8b蓮鍊", a_))
					{
						this.ᜃ.ᜂ(A_0.ᜀ(ClipboardData.b("❳᥵൷ࡹύ᭽쥿", a_), null));
					}
				}
				return;
			}
		}
		this.ᜂ = TransformType.TransformUnknown;
	}

	// Token: 0x0600325C RID: 12892 RVA: 0x002E6728 File Offset: 0x002E5728
	internal MemoryStream ᜁ(Stream A_0)
	{
		int a_ = 0;
		switch (0)
		{
		default:
		{
			MemoryStream memoryStream;
			for (;;)
			{
				for (;;)
				{
					memoryStream = null;
					A_0.Position = 0L;
					TransformType transformType = this.ᜂ;
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 0:
							switch (transformType)
							{
							case TransformType.TransformC14n:
							{
								XmlDsigC14NTransform xmlDsigC14NTransform = new XmlDsigC14NTransform();
								xmlDsigC14NTransform.LoadInput(A_0);
								memoryStream = (MemoryStream)xmlDsigC14NTransform.GetOutput(typeof(MemoryStream));
								num = 5;
								continue;
							}
							case TransformType.TransformRelationship:
							{
								XmlDocument xmlDocument = this.ᜀ(A_0);
								A_0.Position = 0L;
								XmlDocument xmlDocument2 = this.ᜀ(A_0);
								XmlNode xmlNode = xmlDocument2.GetElementsByTagName(ClipboardData.b("㑥൧٩൫ᩭ᥯ᵱᩳյၷ፹౻ൽ", a_))[0];
								xmlNode.RemoveAll();
								IEnumerator enumerator = this.ᜃ.ᜃ();
								num = 2;
								continue;
							}
							default:
								if (true)
								{
								}
								num = 1;
								continue;
							}
							break;
						case 1:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								if (false)
								{
								}
								num = 4;
								continue;
							}
							break;
						case 2:
						{
							XmlDocument xmlDocument2;
							try
							{
								num = 0;
								for (;;)
								{
									switch (num)
									{
									case 1:
										goto IL_23B;
									case 2:
										num = 1;
										continue;
									case 3:
									{
										IEnumerator enumerator;
										if (!enumerator.MoveNext())
										{
											num = 2;
											continue;
										}
										string arg = (string)enumerator.Current;
										XmlDocument xmlDocument;
										XmlNode xmlNode2 = xmlDocument.SelectSingleNode(string.Format(ClipboardData.b("䥥䝧䁩㝫⹭㥯ᙱ䥳兵ͷ䩹Ż好\udd7f", a_), arg));
										XmlAttribute xmlAttribute = xmlNode2.OwnerDocument.CreateAttribute(ClipboardData.b("㉥१ᡩ୫୭ѯ㽱᭳ትᵷ", a_));
										xmlAttribute.Value = ClipboardData.b("⽥٧ṩ५ᱭṯ፱ᡳ", a_);
										xmlNode2.Attributes.Append(xmlAttribute);
										XmlNode newChild = xmlDocument2.ImportNode(xmlNode2, true);
										XmlNode xmlNode;
										xmlNode.AppendChild(newChild);
										num = 4;
										continue;
									}
									}
									IL_17E:
									num = 3;
									continue;
									goto IL_17E;
								}
								IL_23B:
								goto IL_104;
							}
							finally
							{
								for (;;)
								{
									IEnumerator enumerator;
									IDisposable disposable = enumerator as IDisposable;
									num = 1;
									for (;;)
									{
										switch (num)
										{
										case 0:
											goto IL_286;
										case 1:
											if (disposable != null)
											{
												num = 2;
												continue;
											}
											goto IL_288;
										case 2:
											disposable.Dispose();
											num = 0;
											continue;
										}
										break;
									}
								}
								IL_286:
								IL_288:;
							}
							return memoryStream;
							IL_104:
							memoryStream = new MemoryStream();
							xmlDocument2.Save(memoryStream);
							num = 3;
							continue;
						}
						case 3:
							return memoryStream;
						case 4:
							return memoryStream;
						case 5:
							return memoryStream;
						}
						break;
					}
				}
			}
			return memoryStream;
		}
		}
	}

	// Token: 0x0600325D RID: 12893 RVA: 0x002E69D0 File Offset: 0x002E59D0
	private XmlDocument ᜀ(Stream A_0)
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
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.PreserveWhitespace = true;
		xmlDocument.Load(A_0);
		return xmlDocument;
	}

	// Token: 0x04002753 RID: 10067
	private const string ᜀ = "http://www.w3.org/TR/2001/REC-xml-c14n-20010315";

	// Token: 0x04002754 RID: 10068
	private const string ᜁ = "http://schemas.openxmlformats.org/package/2006/RelationshipTransform";

	// Token: 0x04002755 RID: 10069
	private readonly TransformType ᜂ;

	// Token: 0x04002756 RID: 10070
	private readonly spr\u25F8 ᜃ;
}
