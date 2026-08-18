using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using NLog.Internal;

namespace NLog.Config
{
	// Token: 0x02000056 RID: 86
	internal class NLogXmlElement
	{
		// Token: 0x060001C6 RID: 454 RVA: 0x00006748 File Offset: 0x00004948
		public NLogXmlElement(string inputUri) : this()
		{
			using (XmlReader xmlReader = XmlReader.Create(inputUri))
			{
				xmlReader.MoveToContent();
				this.Parse(xmlReader);
			}
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x0000678C File Offset: 0x0000498C
		public NLogXmlElement(XmlReader reader) : this()
		{
			this.Parse(reader);
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x0000679B File Offset: 0x0000499B
		private NLogXmlElement()
		{
			this.AttributeValues = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
			this.Children = new List<NLogXmlElement>();
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x000067BE File Offset: 0x000049BE
		// (set) Token: 0x060001CA RID: 458 RVA: 0x000067C6 File Offset: 0x000049C6
		public string LocalName { get; private set; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060001CB RID: 459 RVA: 0x000067CF File Offset: 0x000049CF
		// (set) Token: 0x060001CC RID: 460 RVA: 0x000067D7 File Offset: 0x000049D7
		public Dictionary<string, string> AttributeValues { get; private set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060001CD RID: 461 RVA: 0x000067E0 File Offset: 0x000049E0
		// (set) Token: 0x060001CE RID: 462 RVA: 0x000067E8 File Offset: 0x000049E8
		public IList<NLogXmlElement> Children { get; private set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060001CF RID: 463 RVA: 0x000067F1 File Offset: 0x000049F1
		// (set) Token: 0x060001D0 RID: 464 RVA: 0x000067F9 File Offset: 0x000049F9
		public string Value { get; private set; }

		// Token: 0x060001D1 RID: 465 RVA: 0x00006804 File Offset: 0x00004A04
		public IEnumerable<NLogXmlElement> Elements(string elementName)
		{
			List<NLogXmlElement> list = new List<NLogXmlElement>();
			foreach (NLogXmlElement nlogXmlElement in this.Children)
			{
				if (nlogXmlElement.LocalName.Equals(elementName, StringComparison.OrdinalIgnoreCase))
				{
					list.Add(nlogXmlElement);
				}
			}
			return list;
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00006868 File Offset: 0x00004A68
		public string GetRequiredAttribute(string attributeName)
		{
			string optionalAttribute = this.GetOptionalAttribute(attributeName, null);
			if (optionalAttribute == null)
			{
				throw new NLogConfigurationException(string.Concat(new string[]
				{
					"Expected ",
					attributeName,
					" on <",
					this.LocalName,
					" />"
				}));
			}
			return optionalAttribute;
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x000068BC File Offset: 0x00004ABC
		public bool GetOptionalBooleanAttribute(string attributeName, bool defaultValue)
		{
			string value;
			if (!this.AttributeValues.TryGetValue(attributeName, out value))
			{
				return defaultValue;
			}
			return Convert.ToBoolean(value, CultureInfo.InvariantCulture);
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x000068E8 File Offset: 0x00004AE8
		public bool? GetOptionalBooleanAttribute(string attributeName, bool? defaultValue)
		{
			string value;
			if (!this.AttributeValues.TryGetValue(attributeName, out value))
			{
				return defaultValue;
			}
			if (StringHelpers.IsNullOrWhiteSpace(value))
			{
				return null;
			}
			return new bool?(Convert.ToBoolean(value, CultureInfo.InvariantCulture));
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000692C File Offset: 0x00004B2C
		public string GetOptionalAttribute(string attributeName, string defaultValue)
		{
			string result;
			if (!this.AttributeValues.TryGetValue(attributeName, out result))
			{
				result = defaultValue;
			}
			return result;
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000694C File Offset: 0x00004B4C
		public void AssertName(params string[] allowedNames)
		{
			foreach (string value in allowedNames)
			{
				if (this.LocalName.Equals(value, StringComparison.OrdinalIgnoreCase))
				{
					return;
				}
			}
			throw new InvalidOperationException(string.Concat(new string[]
			{
				"Assertion failed. Expected element name '",
				string.Join("|", allowedNames),
				"', actual: '",
				this.LocalName,
				"'."
			}));
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x000069C0 File Offset: 0x00004BC0
		private void Parse(XmlReader reader)
		{
			if (reader.MoveToFirstAttribute())
			{
				do
				{
					this.AttributeValues.Add(reader.LocalName, reader.Value);
				}
				while (reader.MoveToNextAttribute());
				reader.MoveToElement();
			}
			this.LocalName = reader.LocalName;
			if (!reader.IsEmptyElement)
			{
				while (reader.Read())
				{
					if (reader.NodeType == XmlNodeType.EndElement)
					{
						return;
					}
					if (reader.NodeType == XmlNodeType.CDATA || reader.NodeType == XmlNodeType.Text)
					{
						this.Value += reader.Value;
					}
					else if (reader.NodeType == XmlNodeType.Element)
					{
						this.Children.Add(new NLogXmlElement(reader));
					}
				}
			}
		}
	}
}
