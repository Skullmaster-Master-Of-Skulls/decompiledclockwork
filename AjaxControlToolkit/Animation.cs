using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Xml;

namespace AjaxControlToolkit
{
	// Token: 0x02000030 RID: 48
	[ParseChildren(true)]
	[DefaultProperty("Name")]
	[PersistChildren(false)]
	public class Animation
	{
		// Token: 0x060001B0 RID: 432 RVA: 0x000064E0 File Offset: 0x000046E0
		static Animation()
		{
			Animation._serializer.RegisterConverters(new JavaScriptConverter[]
			{
				new AnimationJavaScriptConverter()
			});
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00006511 File Offset: 0x00004711
		public Animation()
		{
			this._children = new List<Animation>();
			this._properties = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x00006534 File Offset: 0x00004734
		// (set) Token: 0x060001B3 RID: 435 RVA: 0x0000653C File Offset: 0x0000473C
		[Browsable(false)]
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x00006545 File Offset: 0x00004745
		[Browsable(false)]
		public IList<Animation> Children
		{
			get
			{
				return this._children;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x0000654D File Offset: 0x0000474D
		[Browsable(false)]
		public Dictionary<string, string> Properties
		{
			get
			{
				return this._properties;
			}
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00006555 File Offset: 0x00004755
		public override string ToString()
		{
			return Animation.Serialize(this);
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x0000655D File Offset: 0x0000475D
		public static string Serialize(Animation animation)
		{
			return Animation._serializer.Serialize(animation);
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x0000656A File Offset: 0x0000476A
		public static Animation Deserialize(string json)
		{
			if (string.IsNullOrEmpty(json))
			{
				return null;
			}
			return Animation._serializer.Deserialize<Animation>(json);
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00006584 File Offset: 0x00004784
		public static Animation Deserialize(XmlNode node)
		{
			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
			Animation animation = new Animation
			{
				Name = node.Name
			};
			foreach (object obj in node.Attributes)
			{
				XmlAttribute xmlAttribute = (XmlAttribute)obj;
				animation.Properties.Add(xmlAttribute.Name, xmlAttribute.Value);
			}
			if (node.HasChildNodes)
			{
				foreach (object obj2 in node.ChildNodes)
				{
					XmlNode node2 = (XmlNode)obj2;
					animation.Children.Add(Animation.Deserialize(node2));
				}
			}
			return animation;
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00006678 File Offset: 0x00004878
		public static void Parse(string value, ExtenderControl extenderControl)
		{
			if (extenderControl == null)
			{
				throw new ArgumentNullException("extenderControl");
			}
			if (value == null || string.IsNullOrEmpty(value.Trim()))
			{
				return;
			}
			value = "<Animations>" + value + "</Animations>";
			XmlDocument xmlDocument = new XmlDocument();
			using (XmlTextReader xmlTextReader = new XmlTextReader(new StringReader(value)))
			{
				try
				{
					xmlDocument.Load(xmlTextReader);
				}
				catch (XmlException ex)
				{
					string message = string.Format(CultureInfo.CurrentCulture, "Invalid Animation definition for TargetControlID=\"{0}\": {1}", new object[]
					{
						extenderControl.TargetControlID,
						ex.Message
					});
					throw new HttpParseException(message, new ArgumentException(message, ex), HttpContext.Current.Request.Path, value, ex.LineNumber);
				}
			}
			foreach (object obj in xmlDocument.DocumentElement.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(extenderControl)[xmlNode.Name];
				if (propertyDescriptor == null || propertyDescriptor.IsReadOnly)
				{
					string message2 = string.Format(CultureInfo.CurrentCulture, "Animation on TargetControlID=\"{0}\" uses property {1}.{2} that does not exist or cannot be set", new object[]
					{
						extenderControl.TargetControlID,
						extenderControl.GetType().FullName,
						xmlNode.Name
					});
					throw new HttpParseException(message2, new ArgumentException(message2), HttpContext.Current.Request.Path, value, Animation.GetLineNumber(value, xmlNode.Name));
				}
				if (xmlNode.ChildNodes.Count != 1)
				{
					string message3 = string.Format(CultureInfo.CurrentCulture, "Animation {0} for TargetControlID=\"{1}\" can only have one child node.", new object[]
					{
						xmlNode.Name,
						extenderControl.TargetControlID
					});
					throw new HttpParseException(message3, new ArgumentException(message3), HttpContext.Current.Request.Path, value, Animation.GetLineNumber(value, xmlNode.Name));
				}
				XmlNode node = xmlNode.ChildNodes[0];
				Animation value2 = Animation.Deserialize(node);
				propertyDescriptor.SetValue(extenderControl, value2);
			}
		}

		// Token: 0x060001BB RID: 443 RVA: 0x000068E0 File Offset: 0x00004AE0
		private static int GetLineNumber(string source, string tag)
		{
			using (XmlTextReader xmlTextReader = new XmlTextReader(new StringReader(source)))
			{
				if (xmlTextReader.Read())
				{
					while (xmlTextReader.Read())
					{
						if (string.Compare(xmlTextReader.Name, tag, StringComparison.OrdinalIgnoreCase) == 0)
						{
							return xmlTextReader.LineNumber;
						}
						if (xmlTextReader.NodeType == XmlNodeType.Element && !xmlTextReader.IsEmptyElement)
						{
							xmlTextReader.Skip();
						}
					}
				}
			}
			return 1;
		}

		// Token: 0x04000089 RID: 137
		private static JavaScriptSerializer _serializer = new JavaScriptSerializer();

		// Token: 0x0400008A RID: 138
		private string _name;

		// Token: 0x0400008B RID: 139
		private List<Animation> _children;

		// Token: 0x0400008C RID: 140
		private Dictionary<string, string> _properties;
	}
}
