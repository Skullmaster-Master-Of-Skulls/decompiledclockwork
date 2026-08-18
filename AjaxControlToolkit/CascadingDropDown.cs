using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using AjaxControlToolkit.Design;
using AjaxControlToolkit.ToolboxIcons;

namespace AjaxControlToolkit
{
	// Token: 0x02000064 RID: 100
	[ToolboxBitmap(typeof(Accessor), "CascadingDropDown.bmp")]
	[Designer(typeof(CascadingDropDownExtenderDesigner))]
	[ClientScriptResource("Sys.Extended.UI.CascadingDropDownBehavior", "CascadingDropDown")]
	[RequiredScript(typeof(CommonToolkitScripts))]
	[TargetControlType(typeof(ListBox))]
	[TargetControlType(typeof(DropDownList))]
	public class CascadingDropDown : ExtenderControlBase
	{
		// Token: 0x0600035E RID: 862 RVA: 0x0000A8BE File Offset: 0x00008ABE
		public CascadingDropDown()
		{
			base.ClientStateValuesLoaded += this.CascadingDropDown_ClientStateValuesLoaded;
			base.EnableClientState = true;
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x0600035F RID: 863 RVA: 0x0000A8DF File Offset: 0x00008ADF
		// (set) Token: 0x06000360 RID: 864 RVA: 0x0000A8F1 File Offset: 0x00008AF1
		[IDReferenceProperty(typeof(DropDownList))]
		[ClientPropertyName("parentControlID")]
		[DefaultValue("")]
		[ExtenderControlProperty]
		public string ParentControlID
		{
			get
			{
				return base.GetPropertyValue<string>("ParentControlID", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("ParentControlID", value);
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000361 RID: 865 RVA: 0x0000A8FF File Offset: 0x00008AFF
		// (set) Token: 0x06000362 RID: 866 RVA: 0x0000A911 File Offset: 0x00008B11
		[RequiredProperty]
		[ClientPropertyName("category")]
		[DefaultValue("")]
		[ExtenderControlProperty]
		public string Category
		{
			get
			{
				return base.GetPropertyValue<string>("Category", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("Category", value);
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000363 RID: 867 RVA: 0x0000A91F File Offset: 0x00008B1F
		// (set) Token: 0x06000364 RID: 868 RVA: 0x0000A931 File Offset: 0x00008B31
		[DefaultValue("")]
		[ClientPropertyName("promptText")]
		[ExtenderControlProperty]
		public string PromptText
		{
			get
			{
				return base.GetPropertyValue<string>("PromptText", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("PromptText", value);
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000365 RID: 869 RVA: 0x0000A93F File Offset: 0x00008B3F
		// (set) Token: 0x06000366 RID: 870 RVA: 0x0000A951 File Offset: 0x00008B51
		[ClientPropertyName("promptValue")]
		[DefaultValue("")]
		[ExtenderControlProperty]
		public string PromptValue
		{
			get
			{
				return base.GetPropertyValue<string>("PromptValue", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("PromptValue", value);
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000367 RID: 871 RVA: 0x0000A95F File Offset: 0x00008B5F
		// (set) Token: 0x06000368 RID: 872 RVA: 0x0000A971 File Offset: 0x00008B71
		[DefaultValue("")]
		[ClientPropertyName("emptyText")]
		[ExtenderControlProperty]
		public string EmptyText
		{
			get
			{
				return base.GetPropertyValue<string>("EmptyText", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("EmptyText", value);
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000369 RID: 873 RVA: 0x0000A97F File Offset: 0x00008B7F
		// (set) Token: 0x0600036A RID: 874 RVA: 0x0000A991 File Offset: 0x00008B91
		[ClientPropertyName("emptyValue")]
		[DefaultValue("")]
		[ExtenderControlProperty]
		public string EmptyValue
		{
			get
			{
				return base.GetPropertyValue<string>("EmptyValue", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("EmptyValue", value);
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x0600036B RID: 875 RVA: 0x0000A99F File Offset: 0x00008B9F
		// (set) Token: 0x0600036C RID: 876 RVA: 0x0000A9B1 File Offset: 0x00008BB1
		[ClientPropertyName("loadingText")]
		[ExtenderControlProperty]
		[DefaultValue("")]
		public string LoadingText
		{
			get
			{
				return base.GetPropertyValue<string>("LoadingText", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("LoadingText", value);
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x0600036D RID: 877 RVA: 0x0000A9BF File Offset: 0x00008BBF
		// (set) Token: 0x0600036E RID: 878 RVA: 0x0000A9D0 File Offset: 0x00008BD0
		[ClientPropertyName("selectedValue")]
		[ExtenderControlProperty]
		[DefaultValue("")]
		public string SelectedValue
		{
			get
			{
				return base.ClientState ?? string.Empty;
			}
			set
			{
				base.ClientState = value;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x0600036F RID: 879 RVA: 0x0000A9D9 File Offset: 0x00008BD9
		// (set) Token: 0x06000370 RID: 880 RVA: 0x0000A9EB File Offset: 0x00008BEB
		[ExtenderControlProperty]
		[TypeConverter(typeof(ServicePathConverter))]
		[ClientPropertyName("servicePath")]
		[UrlProperty]
		public string ServicePath
		{
			get
			{
				return base.GetPropertyValue<string>("ServicePath", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("ServicePath", value);
			}
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0000A9F9 File Offset: 0x00008BF9
		private bool ShouldSerializeServicePath()
		{
			return !string.IsNullOrEmpty(this.ServiceMethod);
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000372 RID: 882 RVA: 0x0000AA09 File Offset: 0x00008C09
		// (set) Token: 0x06000373 RID: 883 RVA: 0x0000AA1B File Offset: 0x00008C1B
		[DefaultValue("")]
		[ClientPropertyName("serviceMethod")]
		[RequiredProperty]
		[ExtenderControlProperty]
		public string ServiceMethod
		{
			get
			{
				return base.GetPropertyValue<string>("ServiceMethod", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("ServiceMethod", value);
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000374 RID: 884 RVA: 0x0000AA29 File Offset: 0x00008C29
		// (set) Token: 0x06000375 RID: 885 RVA: 0x0000AA37 File Offset: 0x00008C37
		[DefaultValue(null)]
		[ExtenderControlProperty]
		[ClientPropertyName("contextKey")]
		public string ContextKey
		{
			get
			{
				return base.GetPropertyValue<string>("ContextKey", null);
			}
			set
			{
				base.SetPropertyValue<string>("ContextKey", value);
				this.UseContextKey = true;
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000376 RID: 886 RVA: 0x0000AA4C File Offset: 0x00008C4C
		// (set) Token: 0x06000377 RID: 887 RVA: 0x0000AA5A File Offset: 0x00008C5A
		[ClientPropertyName("useContextKey")]
		[DefaultValue(false)]
		[ExtenderControlProperty]
		public bool UseContextKey
		{
			get
			{
				return base.GetPropertyValue<bool>("UseContextKey", false);
			}
			set
			{
				base.SetPropertyValue<bool>("UseContextKey", value);
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000378 RID: 888 RVA: 0x0000AA68 File Offset: 0x00008C68
		// (set) Token: 0x06000379 RID: 889 RVA: 0x0000AA76 File Offset: 0x00008C76
		[ExtenderControlProperty]
		[ClientPropertyName("useHttpGet")]
		[DefaultValue(false)]
		public bool UseHttpGet
		{
			get
			{
				return base.GetPropertyValue<bool>("UseHttpGet", false);
			}
			set
			{
				base.SetPropertyValue<bool>("UseHttpGet", value);
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x0600037A RID: 890 RVA: 0x0000AA84 File Offset: 0x00008C84
		// (set) Token: 0x0600037B RID: 891 RVA: 0x0000AA92 File Offset: 0x00008C92
		[DefaultValue(false)]
		[ExtenderControlProperty]
		[ClientPropertyName("enableAtLoading")]
		public bool EnableAtLoading
		{
			get
			{
				return base.GetPropertyValue<bool>("EnableAtLoading", false);
			}
			set
			{
				base.SetPropertyValue<bool>("EnableAtLoading", value);
			}
		}

		// Token: 0x0600037C RID: 892 RVA: 0x0000AAA0 File Offset: 0x00008CA0
		private void CascadingDropDown_ClientStateValuesLoaded(object sender, EventArgs e)
		{
			ListControl listControl = (ListControl)base.TargetControl;
			if (listControl == null)
			{
				throw new ArgumentNullException("No target control is set for the CascadingDropDown extender.");
			}
			listControl.Items.Clear();
			string text = ":::";
			string clientState = base.ClientState;
			int num = (clientState ?? string.Empty).IndexOf(text, StringComparison.Ordinal);
			if (num == -1)
			{
				listControl.Items.Add(clientState);
				return;
			}
			string[] array = Regex.Split(clientState, text);
			string value = array[0];
			string text2 = array[1];
			ListItem listItem = new ListItem(text2, value);
			if (array.Length > 2)
			{
				string value2 = array[2];
				listItem.Attributes.Add("title", value2);
			}
			listControl.Items.Add(listItem);
		}

		// Token: 0x0600037D RID: 893 RVA: 0x0000AB50 File Offset: 0x00008D50
		public static StringDictionary ParseKnownCategoryValuesString(string knownCategoryValues)
		{
			if (knownCategoryValues == null)
			{
				throw new ArgumentNullException("knownCategoryValues");
			}
			StringDictionary stringDictionary = new StringDictionary();
			if (knownCategoryValues != null)
			{
				foreach (string text in knownCategoryValues.Split(new char[]
				{
					';'
				}))
				{
					string[] array2 = text.Split(new char[]
					{
						':'
					});
					if (array2.Length == 2)
					{
						stringDictionary.Add(array2[0].ToLowerInvariant(), array2[1]);
					}
				}
			}
			return stringDictionary;
		}

		// Token: 0x0600037E RID: 894 RVA: 0x0000ABD1 File Offset: 0x00008DD1
		public static CascadingDropDownNameValue[] QuerySimpleCascadingDropDownDocument(XmlDocument document, string[] documentHierarchy, StringDictionary knownCategoryValuesDictionary, string category)
		{
			return CascadingDropDown.QuerySimpleCascadingDropDownDocument(document, documentHierarchy, knownCategoryValuesDictionary, category, new Regex("^[^/'\\*]*$"));
		}

		// Token: 0x0600037F RID: 895 RVA: 0x0000ABE8 File Offset: 0x00008DE8
		public static CascadingDropDownNameValue[] QuerySimpleCascadingDropDownDocument(XmlDocument document, string[] documentHierarchy, StringDictionary knownCategoryValuesDictionary, string category, Regex inputValidationRegex)
		{
			if (document == null)
			{
				throw new ArgumentNullException("document");
			}
			if (documentHierarchy == null)
			{
				throw new ArgumentNullException("documentHierarchy");
			}
			if (knownCategoryValuesDictionary == null)
			{
				throw new ArgumentNullException("knownCategoryValuesDictionary");
			}
			if (category == null)
			{
				throw new ArgumentNullException("category");
			}
			if (inputValidationRegex == null)
			{
				throw new ArgumentNullException("inputValidationRegex");
			}
			foreach (object obj in knownCategoryValuesDictionary.Keys)
			{
				string text = (string)obj;
				if (!inputValidationRegex.IsMatch(text) || !inputValidationRegex.IsMatch(knownCategoryValuesDictionary[text]))
				{
					throw new ArgumentException("Invalid characters present.", "category");
				}
			}
			if (!inputValidationRegex.IsMatch(category))
			{
				throw new ArgumentException("Invalid characters present.", "category");
			}
			string text2 = "/" + document.DocumentElement.Name;
			foreach (string text3 in documentHierarchy)
			{
				if (knownCategoryValuesDictionary.ContainsKey(text3))
				{
					text2 += string.Format(CultureInfo.InvariantCulture, "/{0}[(@name and @value='{1}') or (@name='{1}' and not(@value))]", new object[]
					{
						text3,
						knownCategoryValuesDictionary[text3]
					});
				}
			}
			text2 = text2 + "/" + category.ToLowerInvariant();
			List<CascadingDropDownNameValue> list = new List<CascadingDropDownNameValue>();
			foreach (object obj2 in document.SelectNodes(text2))
			{
				XmlNode xmlNode = (XmlNode)obj2;
				string value = xmlNode.Attributes.GetNamedItem("name").Value;
				XmlNode namedItem = xmlNode.Attributes.GetNamedItem("value");
				string value2 = (namedItem != null) ? namedItem.Value : value;
				XmlNode namedItem2 = xmlNode.Attributes.GetNamedItem("default");
				bool defaultValue = namedItem2 != null && bool.Parse(namedItem2.Value);
				CascadingDropDownNameValue cascadingDropDownNameValue = new CascadingDropDownNameValue(value, value2, defaultValue);
				XmlNode namedItem3 = xmlNode.Attributes.GetNamedItem("optionTitle");
				string optionTitle = (namedItem3 != null) ? namedItem3.Value : string.Empty;
				cascadingDropDownNameValue.optionTitle = optionTitle;
				list.Add(cascadingDropDownNameValue);
			}
			return list.ToArray();
		}
	}
}
