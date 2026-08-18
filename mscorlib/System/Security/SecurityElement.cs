using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Util;
using System.Text;

namespace System.Security
{
	// Token: 0x02000615 RID: 1557
	[ComVisible(true)]
	[Serializable]
	public sealed class SecurityElement : ISecurityElementFactory
	{
		// Token: 0x0600383A RID: 14394 RVA: 0x000BC90E File Offset: 0x000BB90E
		internal SecurityElement()
		{
		}

		// Token: 0x0600383B RID: 14395 RVA: 0x000BC916 File Offset: 0x000BB916
		SecurityElement ISecurityElementFactory.CreateSecurityElement()
		{
			return this;
		}

		// Token: 0x0600383C RID: 14396 RVA: 0x000BC919 File Offset: 0x000BB919
		string ISecurityElementFactory.GetTag()
		{
			return this.Tag;
		}

		// Token: 0x0600383D RID: 14397 RVA: 0x000BC921 File Offset: 0x000BB921
		object ISecurityElementFactory.Copy()
		{
			return this.Copy();
		}

		// Token: 0x0600383E RID: 14398 RVA: 0x000BC929 File Offset: 0x000BB929
		string ISecurityElementFactory.Attribute(string attributeName)
		{
			return this.Attribute(attributeName);
		}

		// Token: 0x0600383F RID: 14399 RVA: 0x000BC932 File Offset: 0x000BB932
		public static SecurityElement FromString(string xml)
		{
			if (xml == null)
			{
				throw new ArgumentNullException("xml");
			}
			return new Parser(xml).GetTopElement();
		}

		// Token: 0x06003840 RID: 14400 RVA: 0x000BC950 File Offset: 0x000BB950
		public SecurityElement(string tag)
		{
			if (tag == null)
			{
				throw new ArgumentNullException("tag");
			}
			if (!SecurityElement.IsValidTag(tag))
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_InvalidElementTag"), new object[]
				{
					tag
				}));
			}
			this.m_strTag = tag;
			this.m_strText = null;
		}

		// Token: 0x06003841 RID: 14401 RVA: 0x000BC9B0 File Offset: 0x000BB9B0
		public SecurityElement(string tag, string text)
		{
			if (tag == null)
			{
				throw new ArgumentNullException("tag");
			}
			if (!SecurityElement.IsValidTag(tag))
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_InvalidElementTag"), new object[]
				{
					tag
				}));
			}
			if (text != null && !SecurityElement.IsValidText(text))
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_InvalidElementText"), new object[]
				{
					text
				}));
			}
			this.m_strTag = tag;
			this.m_strText = text;
		}

		// Token: 0x17000978 RID: 2424
		// (get) Token: 0x06003842 RID: 14402 RVA: 0x000BCA3E File Offset: 0x000BBA3E
		// (set) Token: 0x06003843 RID: 14403 RVA: 0x000BCA48 File Offset: 0x000BBA48
		public string Tag
		{
			get
			{
				return this.m_strTag;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("Tag");
				}
				if (!SecurityElement.IsValidTag(value))
				{
					throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_InvalidElementTag"), new object[]
					{
						value
					}));
				}
				this.m_strTag = value;
			}
		}

		// Token: 0x17000979 RID: 2425
		// (get) Token: 0x06003844 RID: 14404 RVA: 0x000BCA98 File Offset: 0x000BBA98
		// (set) Token: 0x06003845 RID: 14405 RVA: 0x000BCB08 File Offset: 0x000BBB08
		public Hashtable Attributes
		{
			get
			{
				if (this.m_lAttributes == null || this.m_lAttributes.Count == 0)
				{
					return null;
				}
				Hashtable hashtable = new Hashtable(this.m_lAttributes.Count);
				int count = this.m_lAttributes.Count;
				for (int i = 0; i < count; i += 2)
				{
					hashtable.Add(this.m_lAttributes[i], this.m_lAttributes[i + 1]);
				}
				return hashtable;
			}
			set
			{
				if (value == null || value.Count == 0)
				{
					this.m_lAttributes = null;
					return;
				}
				ArrayList arrayList = new ArrayList(value.Count);
				IDictionaryEnumerator enumerator = value.GetEnumerator();
				while (enumerator.MoveNext())
				{
					string text = (string)enumerator.Key;
					string value2 = (string)enumerator.Value;
					if (!SecurityElement.IsValidAttributeName(text))
					{
						throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_InvalidElementName"), new object[]
						{
							(string)enumerator.Current
						}));
					}
					if (!SecurityElement.IsValidAttributeValue(value2))
					{
						throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_InvalidElementValue"), new object[]
						{
							(string)enumerator.Value
						}));
					}
					arrayList.Add(text);
					arrayList.Add(value2);
				}
				this.m_lAttributes = arrayList;
			}
		}

		// Token: 0x1700097A RID: 2426
		// (get) Token: 0x06003846 RID: 14406 RVA: 0x000BCBF0 File Offset: 0x000BBBF0
		// (set) Token: 0x06003847 RID: 14407 RVA: 0x000BCC00 File Offset: 0x000BBC00
		public string Text
		{
			get
			{
				return SecurityElement.Unescape(this.m_strText);
			}
			set
			{
				if (value == null)
				{
					this.m_strText = null;
					return;
				}
				if (!SecurityElement.IsValidText(value))
				{
					throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_InvalidElementTag"), new object[]
					{
						value
					}));
				}
				this.m_strText = value;
			}
		}

		// Token: 0x1700097B RID: 2427
		// (get) Token: 0x06003848 RID: 14408 RVA: 0x000BCC4D File Offset: 0x000BBC4D
		// (set) Token: 0x06003849 RID: 14409 RVA: 0x000BCC5C File Offset: 0x000BBC5C
		public ArrayList Children
		{
			get
			{
				this.ConvertSecurityElementFactories();
				return this.m_lChildren;
			}
			set
			{
				if (value != null)
				{
					IEnumerator enumerator = value.GetEnumerator();
					while (enumerator.MoveNext())
					{
						if (enumerator.Current == null)
						{
							throw new ArgumentException(Environment.GetResourceString("ArgumentNull_Child"));
						}
					}
				}
				this.m_lChildren = value;
			}
		}

		// Token: 0x0600384A RID: 14410 RVA: 0x000BCC9C File Offset: 0x000BBC9C
		internal void ConvertSecurityElementFactories()
		{
			if (this.m_lChildren == null)
			{
				return;
			}
			for (int i = 0; i < this.m_lChildren.Count; i++)
			{
				ISecurityElementFactory securityElementFactory = this.m_lChildren[i] as ISecurityElementFactory;
				if (securityElementFactory != null && !(this.m_lChildren[i] is SecurityElement))
				{
					this.m_lChildren[i] = securityElementFactory.CreateSecurityElement();
				}
			}
		}

		// Token: 0x1700097C RID: 2428
		// (get) Token: 0x0600384B RID: 14411 RVA: 0x000BCD02 File Offset: 0x000BBD02
		internal ArrayList InternalChildren
		{
			get
			{
				return this.m_lChildren;
			}
		}

		// Token: 0x0600384C RID: 14412 RVA: 0x000BCD0C File Offset: 0x000BBD0C
		internal void AddAttributeSafe(string name, string value)
		{
			if (this.m_lAttributes == null)
			{
				this.m_lAttributes = new ArrayList(8);
			}
			else
			{
				int count = this.m_lAttributes.Count;
				for (int i = 0; i < count; i += 2)
				{
					string a = (string)this.m_lAttributes[i];
					if (string.Equals(a, name))
					{
						throw new ArgumentException(Environment.GetResourceString("Argument_AttributeNamesMustBeUnique"));
					}
				}
			}
			this.m_lAttributes.Add(name);
			this.m_lAttributes.Add(value);
		}

		// Token: 0x0600384D RID: 14413 RVA: 0x000BCD8C File Offset: 0x000BBD8C
		public void AddAttribute(string name, string value)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (!SecurityElement.IsValidAttributeName(name))
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_InvalidElementName"), new object[]
				{
					name
				}));
			}
			if (!SecurityElement.IsValidAttributeValue(value))
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_InvalidElementValue"), new object[]
				{
					value
				}));
			}
			this.AddAttributeSafe(name, value);
		}

		// Token: 0x0600384E RID: 14414 RVA: 0x000BCE19 File Offset: 0x000BBE19
		public void AddChild(SecurityElement child)
		{
			if (child == null)
			{
				throw new ArgumentNullException("child");
			}
			if (this.m_lChildren == null)
			{
				this.m_lChildren = new ArrayList(1);
			}
			this.m_lChildren.Add(child);
		}

		// Token: 0x0600384F RID: 14415 RVA: 0x000BCE4A File Offset: 0x000BBE4A
		internal void AddChild(ISecurityElementFactory child)
		{
			if (child == null)
			{
				throw new ArgumentNullException("child");
			}
			if (this.m_lChildren == null)
			{
				this.m_lChildren = new ArrayList(1);
			}
			this.m_lChildren.Add(child);
		}

		// Token: 0x06003850 RID: 14416 RVA: 0x000BCE7C File Offset: 0x000BBE7C
		internal void AddChildNoDuplicates(ISecurityElementFactory child)
		{
			if (child == null)
			{
				throw new ArgumentNullException("child");
			}
			if (this.m_lChildren == null)
			{
				this.m_lChildren = new ArrayList(1);
				this.m_lChildren.Add(child);
				return;
			}
			for (int i = 0; i < this.m_lChildren.Count; i++)
			{
				if (this.m_lChildren[i] == child)
				{
					return;
				}
			}
			this.m_lChildren.Add(child);
		}

		// Token: 0x06003851 RID: 14417 RVA: 0x000BCEEC File Offset: 0x000BBEEC
		public bool Equal(SecurityElement other)
		{
			if (other == null)
			{
				return false;
			}
			if (!string.Equals(this.m_strTag, other.m_strTag))
			{
				return false;
			}
			if (!string.Equals(this.m_strText, other.m_strText))
			{
				return false;
			}
			if (this.m_lAttributes == null || other.m_lAttributes == null)
			{
				if (this.m_lAttributes != other.m_lAttributes)
				{
					return false;
				}
			}
			else
			{
				int count = this.m_lAttributes.Count;
				if (count != other.m_lAttributes.Count)
				{
					return false;
				}
				for (int i = 0; i < count; i++)
				{
					string a = (string)this.m_lAttributes[i];
					string b = (string)other.m_lAttributes[i];
					if (!string.Equals(a, b))
					{
						return false;
					}
				}
			}
			if (this.m_lChildren == null || other.m_lChildren == null)
			{
				if (this.m_lChildren != other.m_lChildren)
				{
					return false;
				}
			}
			else
			{
				if (this.m_lChildren.Count != other.m_lChildren.Count)
				{
					return false;
				}
				this.ConvertSecurityElementFactories();
				other.ConvertSecurityElementFactories();
				IEnumerator enumerator = this.m_lChildren.GetEnumerator();
				IEnumerator enumerator2 = other.m_lChildren.GetEnumerator();
				while (enumerator.MoveNext())
				{
					enumerator2.MoveNext();
					SecurityElement securityElement = (SecurityElement)enumerator.Current;
					SecurityElement other2 = (SecurityElement)enumerator2.Current;
					if (securityElement == null || !securityElement.Equal(other2))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06003852 RID: 14418 RVA: 0x000BD044 File Offset: 0x000BC044
		[ComVisible(false)]
		public SecurityElement Copy()
		{
			return new SecurityElement(this.m_strTag, this.m_strText)
			{
				m_lChildren = ((this.m_lChildren == null) ? null : new ArrayList(this.m_lChildren)),
				m_lAttributes = ((this.m_lAttributes == null) ? null : new ArrayList(this.m_lAttributes))
			};
		}

		// Token: 0x06003853 RID: 14419 RVA: 0x000BD09C File Offset: 0x000BC09C
		public static bool IsValidTag(string tag)
		{
			return tag != null && tag.IndexOfAny(SecurityElement.s_tagIllegalCharacters) == -1;
		}

		// Token: 0x06003854 RID: 14420 RVA: 0x000BD0B1 File Offset: 0x000BC0B1
		public static bool IsValidText(string text)
		{
			return text != null && text.IndexOfAny(SecurityElement.s_textIllegalCharacters) == -1;
		}

		// Token: 0x06003855 RID: 14421 RVA: 0x000BD0C6 File Offset: 0x000BC0C6
		public static bool IsValidAttributeName(string name)
		{
			return SecurityElement.IsValidTag(name);
		}

		// Token: 0x06003856 RID: 14422 RVA: 0x000BD0CE File Offset: 0x000BC0CE
		public static bool IsValidAttributeValue(string value)
		{
			return value != null && value.IndexOfAny(SecurityElement.s_valueIllegalCharacters) == -1;
		}

		// Token: 0x06003857 RID: 14423 RVA: 0x000BD0E4 File Offset: 0x000BC0E4
		private static string GetEscapeSequence(char c)
		{
			int num = SecurityElement.s_escapeStringPairs.Length;
			for (int i = 0; i < num; i += 2)
			{
				string text = SecurityElement.s_escapeStringPairs[i];
				string result = SecurityElement.s_escapeStringPairs[i + 1];
				if (text[0] == c)
				{
					return result;
				}
			}
			return c.ToString();
		}

		// Token: 0x06003858 RID: 14424 RVA: 0x000BD12C File Offset: 0x000BC12C
		public static string Escape(string str)
		{
			if (str == null)
			{
				return null;
			}
			StringBuilder stringBuilder = null;
			int length = str.Length;
			int num = 0;
			for (;;)
			{
				int num2 = str.IndexOfAny(SecurityElement.s_escapeChars, num);
				if (num2 == -1)
				{
					break;
				}
				if (stringBuilder == null)
				{
					stringBuilder = new StringBuilder();
				}
				stringBuilder.Append(str, num, num2 - num);
				stringBuilder.Append(SecurityElement.GetEscapeSequence(str[num2]));
				num = num2 + 1;
			}
			if (stringBuilder == null)
			{
				return str;
			}
			stringBuilder.Append(str, num, length - num);
			return stringBuilder.ToString();
		}

		// Token: 0x06003859 RID: 14425 RVA: 0x000BD1A0 File Offset: 0x000BC1A0
		private static string GetUnescapeSequence(string str, int index, out int newIndex)
		{
			int num = str.Length - index;
			int num2 = SecurityElement.s_escapeStringPairs.Length;
			for (int i = 0; i < num2; i += 2)
			{
				string result = SecurityElement.s_escapeStringPairs[i];
				string text = SecurityElement.s_escapeStringPairs[i + 1];
				int length = text.Length;
				if (length <= num && string.Compare(text, 0, str, index, length, StringComparison.Ordinal) == 0)
				{
					newIndex = index + text.Length;
					return result;
				}
			}
			newIndex = index + 1;
			return str[index].ToString();
		}

		// Token: 0x0600385A RID: 14426 RVA: 0x000BD21C File Offset: 0x000BC21C
		private static string Unescape(string str)
		{
			if (str == null)
			{
				return null;
			}
			StringBuilder stringBuilder = null;
			int length = str.Length;
			int num = 0;
			for (;;)
			{
				int num2 = str.IndexOf('&', num);
				if (num2 == -1)
				{
					break;
				}
				if (stringBuilder == null)
				{
					stringBuilder = new StringBuilder();
				}
				stringBuilder.Append(str, num, num2 - num);
				stringBuilder.Append(SecurityElement.GetUnescapeSequence(str, num2, out num));
			}
			if (stringBuilder == null)
			{
				return str;
			}
			stringBuilder.Append(str, num, length - num);
			return stringBuilder.ToString();
		}

		// Token: 0x0600385B RID: 14427 RVA: 0x000BD285 File Offset: 0x000BC285
		private static void ToStringHelperStringBuilder(object obj, string str)
		{
			((StringBuilder)obj).Append(str);
		}

		// Token: 0x0600385C RID: 14428 RVA: 0x000BD294 File Offset: 0x000BC294
		private static void ToStringHelperStreamWriter(object obj, string str)
		{
			((StreamWriter)obj).Write(str);
		}

		// Token: 0x0600385D RID: 14429 RVA: 0x000BD2A4 File Offset: 0x000BC2A4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			this.ToString("", stringBuilder, new SecurityElement.ToStringHelperFunc(SecurityElement.ToStringHelperStringBuilder));
			return stringBuilder.ToString();
		}

		// Token: 0x0600385E RID: 14430 RVA: 0x000BD2D5 File Offset: 0x000BC2D5
		internal void ToWriter(StreamWriter writer)
		{
			this.ToString("", writer, new SecurityElement.ToStringHelperFunc(SecurityElement.ToStringHelperStreamWriter));
		}

		// Token: 0x0600385F RID: 14431 RVA: 0x000BD2F0 File Offset: 0x000BC2F0
		private void ToString(string indent, object obj, SecurityElement.ToStringHelperFunc func)
		{
			func(obj, "<");
			switch (this.m_type)
			{
			case SecurityElementType.Format:
				func(obj, "?");
				break;
			case SecurityElementType.Comment:
				func(obj, "!");
				break;
			}
			func(obj, this.m_strTag);
			if (this.m_lAttributes != null && this.m_lAttributes.Count > 0)
			{
				func(obj, " ");
				int count = this.m_lAttributes.Count;
				for (int i = 0; i < count; i += 2)
				{
					string str = (string)this.m_lAttributes[i];
					string str2 = (string)this.m_lAttributes[i + 1];
					func(obj, str);
					func(obj, "=\"");
					func(obj, str2);
					func(obj, "\"");
					if (i != this.m_lAttributes.Count - 2)
					{
						if (this.m_type == SecurityElementType.Regular)
						{
							func(obj, Environment.NewLine);
						}
						else
						{
							func(obj, " ");
						}
					}
				}
			}
			if (this.m_strText == null && (this.m_lChildren == null || this.m_lChildren.Count == 0))
			{
				switch (this.m_type)
				{
				case SecurityElementType.Format:
					func(obj, " ?>");
					break;
				case SecurityElementType.Comment:
					func(obj, ">");
					break;
				default:
					func(obj, "/>");
					break;
				}
				func(obj, Environment.NewLine);
				return;
			}
			func(obj, ">");
			func(obj, this.m_strText);
			if (this.m_lChildren != null)
			{
				this.ConvertSecurityElementFactories();
				func(obj, Environment.NewLine);
				for (int j = 0; j < this.m_lChildren.Count; j++)
				{
					((SecurityElement)this.m_lChildren[j]).ToString("", obj, func);
				}
			}
			func(obj, "</");
			func(obj, this.m_strTag);
			func(obj, ">");
			func(obj, Environment.NewLine);
		}

		// Token: 0x06003860 RID: 14432 RVA: 0x000BD51C File Offset: 0x000BC51C
		public string Attribute(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (this.m_lAttributes == null)
			{
				return null;
			}
			int count = this.m_lAttributes.Count;
			for (int i = 0; i < count; i += 2)
			{
				string a = (string)this.m_lAttributes[i];
				if (string.Equals(a, name))
				{
					string str = (string)this.m_lAttributes[i + 1];
					return SecurityElement.Unescape(str);
				}
			}
			return null;
		}

		// Token: 0x06003861 RID: 14433 RVA: 0x000BD590 File Offset: 0x000BC590
		public SecurityElement SearchForChildByTag(string tag)
		{
			if (tag == null)
			{
				throw new ArgumentNullException("tag");
			}
			if (this.m_lChildren == null)
			{
				return null;
			}
			foreach (object obj in this.m_lChildren)
			{
				SecurityElement securityElement = (SecurityElement)obj;
				if (securityElement != null && string.Equals(securityElement.Tag, tag))
				{
					return securityElement;
				}
			}
			return null;
		}

		// Token: 0x06003862 RID: 14434 RVA: 0x000BD5EC File Offset: 0x000BC5EC
		internal IPermission ToPermission(bool ignoreTypeLoadFailures)
		{
			IPermission permission = XMLUtil.CreatePermission(this, PermissionState.None, ignoreTypeLoadFailures);
			if (permission == null)
			{
				return null;
			}
			permission.FromXml(this);
			PermissionToken.GetToken(permission);
			return permission;
		}

		// Token: 0x06003863 RID: 14435 RVA: 0x000BD618 File Offset: 0x000BC618
		internal object ToSecurityObject()
		{
			string strTag;
			if ((strTag = this.m_strTag) != null && strTag == "PermissionSet")
			{
				PermissionSet permissionSet = new PermissionSet(PermissionState.None);
				permissionSet.FromXml(this);
				return permissionSet;
			}
			return this.ToPermission(false);
		}

		// Token: 0x06003864 RID: 14436 RVA: 0x000BD654 File Offset: 0x000BC654
		internal string SearchForTextOfLocalName(string strLocalName)
		{
			if (strLocalName == null)
			{
				throw new ArgumentNullException("strLocalName");
			}
			if (this.m_strTag == null)
			{
				return null;
			}
			if (this.m_strTag.Equals(strLocalName) || this.m_strTag.EndsWith(":" + strLocalName, StringComparison.Ordinal))
			{
				return SecurityElement.Unescape(this.m_strText);
			}
			if (this.m_lChildren == null)
			{
				return null;
			}
			foreach (object obj in this.m_lChildren)
			{
				string text = ((SecurityElement)obj).SearchForTextOfLocalName(strLocalName);
				if (text != null)
				{
					return text;
				}
			}
			return null;
		}

		// Token: 0x06003865 RID: 14437 RVA: 0x000BD6E4 File Offset: 0x000BC6E4
		public string SearchForTextOfTag(string tag)
		{
			if (tag == null)
			{
				throw new ArgumentNullException("tag");
			}
			if (string.Equals(this.m_strTag, tag))
			{
				return SecurityElement.Unescape(this.m_strText);
			}
			if (this.m_lChildren == null)
			{
				return null;
			}
			IEnumerator enumerator = this.m_lChildren.GetEnumerator();
			this.ConvertSecurityElementFactories();
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				string text = ((SecurityElement)obj).SearchForTextOfTag(tag);
				if (text != null)
				{
					return text;
				}
			}
			return null;
		}

		// Token: 0x04001D23 RID: 7459
		private const string s_strIndent = "   ";

		// Token: 0x04001D24 RID: 7460
		private const int c_AttributesTypical = 8;

		// Token: 0x04001D25 RID: 7461
		private const int c_ChildrenTypical = 1;

		// Token: 0x04001D26 RID: 7462
		internal string m_strTag;

		// Token: 0x04001D27 RID: 7463
		internal string m_strText;

		// Token: 0x04001D28 RID: 7464
		private ArrayList m_lChildren;

		// Token: 0x04001D29 RID: 7465
		internal ArrayList m_lAttributes;

		// Token: 0x04001D2A RID: 7466
		internal SecurityElementType m_type;

		// Token: 0x04001D2B RID: 7467
		private static readonly char[] s_tagIllegalCharacters = new char[]
		{
			' ',
			'<',
			'>'
		};

		// Token: 0x04001D2C RID: 7468
		private static readonly char[] s_textIllegalCharacters = new char[]
		{
			'<',
			'>'
		};

		// Token: 0x04001D2D RID: 7469
		private static readonly char[] s_valueIllegalCharacters = new char[]
		{
			'<',
			'>',
			'"'
		};

		// Token: 0x04001D2E RID: 7470
		private static readonly string[] s_escapeStringPairs = new string[]
		{
			"<",
			"&lt;",
			">",
			"&gt;",
			"\"",
			"&quot;",
			"'",
			"&apos;",
			"&",
			"&amp;"
		};

		// Token: 0x04001D2F RID: 7471
		private static readonly char[] s_escapeChars = new char[]
		{
			'<',
			'>',
			'"',
			'\'',
			'&'
		};

		// Token: 0x02000616 RID: 1558
		// (Invoke) Token: 0x06003868 RID: 14440
		private delegate void ToStringHelperFunc(object obj, string str);
	}
}
