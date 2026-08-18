using System;
using System.Xml;

namespace System.DirectoryServices.Protocols
{
	// Token: 0x02000017 RID: 23
	public class DirectoryControl
	{
		// Token: 0x06000068 RID: 104 RVA: 0x00003BA8 File Offset: 0x00002BA8
		public DirectoryControl(string type, byte[] value, bool isCritical, bool serverSide)
		{
			Utility.CheckOSVersion();
			this.directoryControlType = type;
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (value != null)
			{
				this.directoryControlValue = new byte[value.Length];
				for (int i = 0; i < value.Length; i++)
				{
					this.directoryControlValue[i] = value[i];
				}
			}
			this.directoryControlCriticality = isCritical;
			this.directoryControlServerSide = serverSide;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003C28 File Offset: 0x00002C28
		internal DirectoryControl(XmlElement el)
		{
			XmlNamespaceManager dsmlNamespaceManager = NamespaceUtils.GetDsmlNamespaceManager();
			XmlAttribute xmlAttribute = (XmlAttribute)el.SelectSingleNode("@dsml:criticality", dsmlNamespaceManager);
			if (xmlAttribute == null)
			{
				xmlAttribute = (XmlAttribute)el.SelectSingleNode("@criticality", dsmlNamespaceManager);
			}
			if (xmlAttribute == null)
			{
				this.directoryControlCriticality = false;
			}
			else
			{
				string value = xmlAttribute.Value;
				if (value == "true" || value == "1")
				{
					this.directoryControlCriticality = true;
				}
				else
				{
					if (!(value == "false") && !(value == "0"))
					{
						throw new DsmlInvalidDocumentException(Res.GetString("BadControl"));
					}
					this.directoryControlCriticality = false;
				}
			}
			XmlAttribute xmlAttribute2 = (XmlAttribute)el.SelectSingleNode("@dsml:type", dsmlNamespaceManager);
			if (xmlAttribute2 == null)
			{
				xmlAttribute2 = (XmlAttribute)el.SelectSingleNode("@type", dsmlNamespaceManager);
			}
			if (xmlAttribute2 == null)
			{
				throw new DsmlInvalidDocumentException(Res.GetString("BadControl"));
			}
			this.directoryControlType = xmlAttribute2.Value;
			XmlElement xmlElement = (XmlElement)el.SelectSingleNode("dsml:controlValue", dsmlNamespaceManager);
			if (xmlElement != null)
			{
				try
				{
					this.directoryControlValue = Convert.FromBase64String(xmlElement.InnerText);
				}
				catch (FormatException)
				{
					throw new DsmlInvalidDocumentException(Res.GetString("BadControl"));
				}
			}
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003D7C File Offset: 0x00002D7C
		public virtual byte[] GetValue()
		{
			if (this.directoryControlValue == null)
			{
				return new byte[0];
			}
			byte[] array = new byte[this.directoryControlValue.Length];
			for (int i = 0; i < this.directoryControlValue.Length; i++)
			{
				array[i] = this.directoryControlValue[i];
			}
			return array;
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00003DC5 File Offset: 0x00002DC5
		public string Type
		{
			get
			{
				return this.directoryControlType;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00003DCD File Offset: 0x00002DCD
		// (set) Token: 0x0600006D RID: 109 RVA: 0x00003DD5 File Offset: 0x00002DD5
		public bool IsCritical
		{
			get
			{
				return this.directoryControlCriticality;
			}
			set
			{
				this.directoryControlCriticality = value;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00003DDE File Offset: 0x00002DDE
		// (set) Token: 0x0600006F RID: 111 RVA: 0x00003DE6 File Offset: 0x00002DE6
		public bool ServerSide
		{
			get
			{
				return this.directoryControlServerSide;
			}
			set
			{
				this.directoryControlServerSide = value;
			}
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003DF0 File Offset: 0x00002DF0
		internal XmlElement ToXmlNode(XmlDocument doc)
		{
			XmlElement xmlElement = doc.CreateElement("control", "urn:oasis:names:tc:DSML:2:0:core");
			XmlAttribute xmlAttribute = doc.CreateAttribute("type", null);
			xmlAttribute.InnerText = this.Type;
			xmlElement.Attributes.Append(xmlAttribute);
			XmlAttribute xmlAttribute2 = doc.CreateAttribute("criticality", null);
			xmlAttribute2.InnerText = (this.IsCritical ? "true" : "false");
			xmlElement.Attributes.Append(xmlAttribute2);
			byte[] value = this.GetValue();
			if (value.Length != 0)
			{
				XmlElement xmlElement2 = doc.CreateElement("controlValue", "urn:oasis:names:tc:DSML:2:0:core");
				XmlAttribute xmlAttribute3 = doc.CreateAttribute("xsi:type", "http://www.w3.org/2001/XMLSchema-instance");
				xmlAttribute3.InnerText = "xsd:base64Binary";
				xmlElement2.Attributes.Append(xmlAttribute3);
				string innerText = Convert.ToBase64String(value);
				xmlElement2.InnerText = innerText;
				xmlElement.AppendChild(xmlElement2);
			}
			return xmlElement;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003ED0 File Offset: 0x00002ED0
		internal static void TransformControls(DirectoryControl[] controls)
		{
			for (int i = 0; i < controls.Length; i++)
			{
				byte[] value = controls[i].GetValue();
				if (controls[i].Type == "1.2.840.113556.1.4.319")
				{
					object[] array = BerConverter.Decode("{iO}", value);
					int count = (int)array[0];
					byte[] array2 = (byte[])array[1];
					if (array2 == null)
					{
						array2 = new byte[0];
					}
					PageResultResponseControl pageResultResponseControl = new PageResultResponseControl(count, array2, controls[i].IsCritical, controls[i].GetValue());
					controls[i] = pageResultResponseControl;
				}
				else if (controls[i].Type == "1.2.840.113556.1.4.1504")
				{
					object[] array3;
					if (Utility.IsWin2kOS)
					{
						array3 = BerConverter.Decode("{i}", value);
					}
					else
					{
						array3 = BerConverter.Decode("{e}", value);
					}
					int result = (int)array3[0];
					AsqResponseControl asqResponseControl = new AsqResponseControl(result, controls[i].IsCritical, controls[i].GetValue());
					controls[i] = asqResponseControl;
				}
				else if (controls[i].Type == "1.2.840.113556.1.4.841")
				{
					object[] array4 = BerConverter.Decode("{iiO}", value);
					int moreData = (int)array4[0];
					int resultSize = (int)array4[1];
					byte[] cookie = (byte[])array4[2];
					DirSyncResponseControl dirSyncResponseControl = new DirSyncResponseControl(cookie, moreData != 0, resultSize, controls[i].IsCritical, controls[i].GetValue());
					controls[i] = dirSyncResponseControl;
				}
				else if (controls[i].Type == "1.2.840.113556.1.4.474")
				{
					string attributeName = null;
					bool flag;
					object[] array5;
					if (Utility.IsWin2kOS)
					{
						array5 = BerConverter.TryDecode("{ia}", value, out flag);
					}
					else
					{
						array5 = BerConverter.TryDecode("{ea}", value, out flag);
					}
					int result2;
					if (flag)
					{
						result2 = (int)array5[0];
						attributeName = (string)array5[1];
					}
					else
					{
						if (Utility.IsWin2kOS)
						{
							array5 = BerConverter.Decode("{i}", value);
						}
						else
						{
							array5 = BerConverter.Decode("{e}", value);
						}
						result2 = (int)array5[0];
					}
					SortResponseControl sortResponseControl = new SortResponseControl((ResultCode)result2, attributeName, controls[i].IsCritical, controls[i].GetValue());
					controls[i] = sortResponseControl;
				}
				else if (controls[i].Type == "2.16.840.1.113730.3.4.10")
				{
					byte[] context = null;
					bool flag2 = false;
					object[] array6;
					if (Utility.IsWin2kOS)
					{
						array6 = BerConverter.TryDecode("{iiiO}", value, out flag2);
					}
					else
					{
						array6 = BerConverter.TryDecode("{iieO}", value, out flag2);
					}
					int targetPosition;
					int count2;
					int result3;
					if (flag2)
					{
						targetPosition = (int)array6[0];
						count2 = (int)array6[1];
						result3 = (int)array6[2];
						context = (byte[])array6[3];
					}
					else
					{
						if (Utility.IsWin2kOS)
						{
							array6 = BerConverter.Decode("{iii}", value);
						}
						else
						{
							array6 = BerConverter.Decode("{iie}", value);
						}
						targetPosition = (int)array6[0];
						count2 = (int)array6[1];
						result3 = (int)array6[2];
					}
					VlvResponseControl vlvResponseControl = new VlvResponseControl(targetPosition, count2, context, (ResultCode)result3, controls[i].IsCritical, controls[i].GetValue());
					controls[i] = vlvResponseControl;
				}
			}
		}

		// Token: 0x040000DD RID: 221
		internal byte[] directoryControlValue;

		// Token: 0x040000DE RID: 222
		private string directoryControlType = "";

		// Token: 0x040000DF RID: 223
		private bool directoryControlCriticality = true;

		// Token: 0x040000E0 RID: 224
		private bool directoryControlServerSide = true;
	}
}
