using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Helpers.Resources;

namespace System.Web.Helpers
{
	// Token: 0x02000013 RID: 19
	internal class HtmlObjectPrinter : ObjectVisitor
	{
		// Token: 0x060000C9 RID: 201 RVA: 0x00004A18 File Offset: 0x00002C18
		public HtmlObjectPrinter(int recursionLimit, int enumerationLimit) : base(recursionLimit, enumerationLimit)
		{
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000CA RID: 202 RVA: 0x00004A2D File Offset: 0x00002C2D
		private HtmlElement Current
		{
			get
			{
				return this._elementStack.Peek();
			}
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00004A3C File Offset: 0x00002C3C
		public void WriteTo(object value, TextWriter writer)
		{
			HtmlElement htmlElement = new HtmlElement("div");
			htmlElement.AddCssClass("objectinfo");
			this.PushElement(htmlElement);
			this.Visit(value, 0);
			this.PopElement();
			writer.Write("<style type=\"text/css\">       \r\n    .objectinfo { font-size: 13px; }\r\n    .objectinfo .type { color: #0000ff; }\r\n    .objectinfo .complexType { color: #2b91af; }\r\n    .objectinfo .name { color: Black; }\r\n    .objectinfo .value { color: Black; }\r\n    .objectinfo .quote { color: Brown; }\r\n    .objectinfo .null { color: Red; }\r\n    .objectinfo .exception { color:Red; }\r\n    .objectinfo .typeContainer { border-left: solid 2px #7C888A; padding-left: 3px; margin-left:3px; }\r\n    .objectinfo h3, h2 { margin:0; padding:0; }\r\n    .objectinfo ul { margin-top:0; margin-bottom:0; list-style-type:none; padding-left:10px; margin-left:10px; }\r\n</style>\r\n");
			htmlElement.WriteTo(writer);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00004A88 File Offset: 0x00002C88
		public override void VisitKeyValues(object value, IEnumerable<object> keys, Func<object, object> valueSelector, int depth)
		{
			string objectId = base.GetObjectId(value);
			HtmlElement htmlElement = new HtmlElement("ul");
			htmlElement.AddCssClass("typeEnumeration");
			htmlElement["id"] = objectId;
			this.PushElement(htmlElement);
			base.VisitKeyValues(value, keys, valueSelector, depth);
			this.PopElement();
			this.Current.AppendChild(htmlElement);
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00004AE8 File Offset: 0x00002CE8
		public override void VisitKeyValue(object key, object value, int depth)
		{
			HtmlElement htmlElement = new HtmlElement("span");
			this.PushElement(htmlElement);
			this.Visit(key, depth);
			this.PopElement();
			HtmlElement htmlElement2 = new HtmlElement("span");
			this.PushElement(htmlElement2);
			this.Visit(value, depth);
			this.PopElement();
			HtmlElement htmlElement3 = new HtmlElement("li");
			htmlElement3.AppendChild(htmlElement);
			htmlElement3.AppendChild(" = ");
			htmlElement3.AppendChild(htmlElement2);
			this.Current.AppendChild(htmlElement3);
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00004B6C File Offset: 0x00002D6C
		public override void VisitEnumerable(IEnumerable enumerable, int depth)
		{
			string objectId = base.GetObjectId(enumerable);
			HtmlElement htmlElement = new HtmlElement("ul");
			htmlElement.AddCssClass("typeEnumeration");
			htmlElement["id"] = objectId;
			this.PushElement(htmlElement);
			base.VisitEnumerable(enumerable, depth);
			this.PopElement();
			this.Current.AppendChild(htmlElement);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00004BC8 File Offset: 0x00002DC8
		public override void VisitIndexedEnumeratedValue(int index, object item, int depth)
		{
			HtmlElement htmlElement = new HtmlElement("li");
			htmlElement.AppendChild(string.Format(CultureInfo.InvariantCulture, "[{0}] = ", new object[]
			{
				index
			}));
			this.PushElement(htmlElement);
			base.VisitIndexedEnumeratedValue(index, item, depth);
			this.PopElement();
			this.Current.AppendChild(htmlElement);
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00004C2C File Offset: 0x00002E2C
		public override void VisitEnumeratedValue(object item, int depth)
		{
			HtmlElement htmlElement = new HtmlElement("li");
			this.PushElement(htmlElement);
			base.VisitEnumeratedValue(item, depth);
			this.PopElement();
			this.Current.AppendChild(htmlElement);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00004C68 File Offset: 0x00002E68
		public override void VisitEnumeratonLimitExceeded()
		{
			HtmlElement htmlElement = new HtmlElement("li");
			htmlElement.AppendChild("...");
			this.Current.AppendChild(htmlElement);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00004C9C File Offset: 0x00002E9C
		public override void VisitMembers(IEnumerable<string> names, Func<string, Type> typeSelector, Func<string, object> valueSelector, int depth)
		{
			HtmlElement htmlElement = new HtmlElement("ul");
			htmlElement.AddCssClass("typeProperties");
			this.PushElement(htmlElement);
			base.VisitMembers(names, typeSelector, valueSelector, depth);
			this.PopElement();
			this.Current.AppendChild(htmlElement);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00004CE8 File Offset: 0x00002EE8
		public override void VisitMember(string name, Type type, object value, int depth)
		{
			HtmlElement htmlElement = new HtmlElement("li");
			if (type != null)
			{
				htmlElement.AppendChild(HtmlObjectPrinter.CreateTypeNameSpan(type));
				htmlElement.AppendChild(" ");
			}
			htmlElement.AppendChild(HtmlObjectPrinter.CreateNameSpan(name));
			htmlElement.AppendChild(" = ");
			this.PushElement(htmlElement);
			this._excludeTypeName = true;
			base.VisitMember(name, type, value, depth);
			this._excludeTypeName = false;
			this.PopElement();
			this.Current.AppendChild(htmlElement);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00004D70 File Offset: 0x00002F70
		public override void VisitComplexObject(object value, int depth)
		{
			string objectId = base.GetObjectId(value);
			HtmlElement htmlElement = new HtmlElement("div");
			htmlElement.AddCssClass("typeContainer");
			htmlElement["id"] = objectId;
			this.PushElement(htmlElement);
			base.VisitComplexObject(value, depth);
			this.PopElement();
			if (htmlElement.Children.Any<HtmlElement>())
			{
				this.Current.AppendChild(htmlElement);
			}
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00004DD8 File Offset: 0x00002FD8
		public override void VisitNull()
		{
			this.Current.AppendChild(HtmlObjectPrinter._nullSpan);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00004DEB File Offset: 0x00002FEB
		public override void VisitStringValue(string stringValue)
		{
			stringValue = "\"" + HtmlObjectPrinter.ConvertEscapseSequences(stringValue) + "\"";
			this.Current.AppendChild(HtmlObjectPrinter.CreateQuotedSpan(stringValue));
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00004E16 File Offset: 0x00003016
		public override void VisitVisitedObject(string id, object value)
		{
			this.Current.AppendChild(HtmlObjectPrinter.CreateVisitedLink(id));
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00004E2C File Offset: 0x0000302C
		public override void Visit(object value, int depth)
		{
			if (value != null)
			{
				if (!this._excludeTypeName)
				{
					this.Current.AppendChild(HtmlObjectPrinter.CreateTypeNameSpan(value.GetType()));
					this.Current.AppendChild(" ");
				}
				this._excludeTypeName = false;
			}
			base.Visit(value, depth);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00004E7B File Offset: 0x0000307B
		public override void VisitObjectVisitorException(ObjectVisitor.ObjectVisitorException exception)
		{
			this.Current.AppendChild(HtmlObjectPrinter.CreateExceptionSpan(exception));
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00004E90 File Offset: 0x00003090
		public override void VisitConvertedValue(object value, string convertedValue)
		{
			Type type = value.GetType();
			if (type.Equals(typeof(bool)))
			{
				convertedValue = convertedValue.ToLowerInvariant();
				this.Current.AppendChild(HtmlObjectPrinter.CreateTypeSpan(convertedValue));
				return;
			}
			if (type.Equals(typeof(char)))
			{
				string charValue = HtmlObjectPrinter.GetCharValue((char)value);
				this.Current.AppendChild(HtmlObjectPrinter.CreateQuotedSpan("'" + charValue + "'"));
				return;
			}
			Type type2 = value as Type;
			if (type2 != null)
			{
				this.Current.AppendChild(HtmlObjectPrinter.CreateParentSpan(new HtmlElement[]
				{
					HtmlObjectPrinter.CreateTypeSpan("typeof"),
					HtmlObjectPrinter.CreateOperatorSpan("("),
					HtmlObjectPrinter.CreateTypeNameSpan(type2),
					HtmlObjectPrinter.CreateOperatorSpan(")")
				}));
				return;
			}
			this.Current.AppendChild(HtmlObjectPrinter.CreateValueSpan(convertedValue));
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00004F80 File Offset: 0x00003180
		private static HtmlElement CreateParentSpan(params HtmlElement[] elements)
		{
			HtmlElement htmlElement = new HtmlElement("span");
			foreach (HtmlElement e in elements)
			{
				htmlElement.AppendChild(e);
			}
			return htmlElement;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00004FB5 File Offset: 0x000031B5
		private static HtmlElement CreateNameSpan(string name)
		{
			return HtmlElement.CreateSpan(name, "name");
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00004FC2 File Offset: 0x000031C2
		private static HtmlElement CreateOperatorSpan(string @operator)
		{
			return HtmlElement.CreateSpan(@operator, "operator");
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00004FCF File Offset: 0x000031CF
		private static HtmlElement CreateValueSpan(string value)
		{
			return HtmlElement.CreateSpan(value, "value");
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00004FDC File Offset: 0x000031DC
		private static HtmlElement CreateExceptionSpan(ObjectVisitor.ObjectVisitorException exception)
		{
			HtmlElement htmlElement = new HtmlElement("span");
			htmlElement.AppendChild(HelpersResources.ObjectInfo_PropertyThrewException);
			htmlElement.AppendChild(HtmlElement.CreateSpan(exception.InnerException.Message, "exception"));
			return htmlElement;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x0000501D File Offset: 0x0000321D
		private static HtmlElement CreateQuotedSpan(string value)
		{
			return HtmlElement.CreateSpan(value, "quote");
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x0000502C File Offset: 0x0000322C
		private static HtmlElement CreateLink(string href, string linkText, string cssClass = null)
		{
			HtmlElement htmlElement = new HtmlElement("a");
			htmlElement.SetInnerText(linkText);
			htmlElement["href"] = href;
			if (!string.IsNullOrEmpty(cssClass))
			{
				htmlElement.AddCssClass(cssClass);
			}
			return htmlElement;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x0000506C File Offset: 0x0000326C
		private static HtmlElement CreateVisitedLink(string id)
		{
			string linkText = string.Format(CultureInfo.InvariantCulture, "[{0}]", new object[]
			{
				HelpersResources.ObjectInfo_PreviousDisplayed
			});
			return HtmlObjectPrinter.CreateLink("#" + id, linkText, null);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x000050AB File Offset: 0x000032AB
		private static HtmlElement CreateTypeSpan(string value)
		{
			return HtmlElement.CreateSpan(value, "type");
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x000050B8 File Offset: 0x000032B8
		private static HtmlElement CreateTypeNameSpan(Type type)
		{
			string typeName = ObjectVisitor.GetTypeName(type);
			HtmlElement htmlElement = new HtmlElement("span");
			StringBuilder stringBuilder = new StringBuilder();
			foreach (char c in typeName)
			{
				if (HtmlObjectPrinter.IsOperator(c))
				{
					if (stringBuilder.Length > 0)
					{
						htmlElement.AppendChild(HtmlObjectPrinter.CreateTypeSpan(stringBuilder.ToString()));
						stringBuilder.Clear();
					}
					htmlElement.AppendChild(HtmlObjectPrinter.CreateOperatorSpan(c.ToString()));
				}
				else
				{
					stringBuilder.Append(c);
				}
			}
			if (stringBuilder.Length > 0)
			{
				htmlElement.AppendChild(HtmlObjectPrinter.CreateTypeSpan(stringBuilder.ToString()));
			}
			return htmlElement;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00005163 File Offset: 0x00003363
		private static bool IsOperator(char ch)
		{
			return ch == '[' || ch == ']' || ch == '<' || ch == '>' || ch == '&' || ch == '*';
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00005185 File Offset: 0x00003385
		internal void PushElement(HtmlElement element)
		{
			this._elementStack.Push(element);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00005193 File Offset: 0x00003393
		internal HtmlElement PopElement()
		{
			return this._elementStack.Pop();
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x000051A0 File Offset: 0x000033A0
		internal static string ConvertEscapseSequences(string value)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (char ch in value)
			{
				stringBuilder.Append(HtmlObjectPrinter.GetCharValue(ch));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x000051E4 File Offset: 0x000033E4
		private static string GetCharValue(char ch)
		{
			string result;
			if (HtmlObjectPrinter._printableEscapeChars.TryGetValue(ch, out result))
			{
				return result;
			}
			return ch.ToString();
		}

		// Token: 0x0400003F RID: 63
		private const string Styles = "<style type=\"text/css\">       \r\n    .objectinfo { font-size: 13px; }\r\n    .objectinfo .type { color: #0000ff; }\r\n    .objectinfo .complexType { color: #2b91af; }\r\n    .objectinfo .name { color: Black; }\r\n    .objectinfo .value { color: Black; }\r\n    .objectinfo .quote { color: Brown; }\r\n    .objectinfo .null { color: Red; }\r\n    .objectinfo .exception { color:Red; }\r\n    .objectinfo .typeContainer { border-left: solid 2px #7C888A; padding-left: 3px; margin-left:3px; }\r\n    .objectinfo h3, h2 { margin:0; padding:0; }\r\n    .objectinfo ul { margin-top:0; margin-bottom:0; list-style-type:none; padding-left:10px; margin-left:10px; }\r\n</style>\r\n";

		// Token: 0x04000040 RID: 64
		private static readonly HtmlElement _nullSpan = HtmlElement.CreateSpan("(null)", "null");

		// Token: 0x04000041 RID: 65
		private static readonly Dictionary<char, string> _printableEscapeChars = new Dictionary<char, string>
		{
			{
				'\0',
				"\\0"
			},
			{
				'\\',
				"\\\\"
			},
			{
				'\'',
				"'"
			},
			{
				'"',
				"\\\""
			},
			{
				'\a',
				"\\a"
			},
			{
				'\b',
				"\\b"
			},
			{
				'\f',
				"\\f"
			},
			{
				'\n',
				"\\n"
			},
			{
				'\r',
				"\\r"
			},
			{
				'\t',
				"\\t"
			},
			{
				'\v',
				"\\v"
			}
		};

		// Token: 0x04000042 RID: 66
		private bool _excludeTypeName;

		// Token: 0x04000043 RID: 67
		private Stack<HtmlElement> _elementStack = new Stack<HtmlElement>();
	}
}
