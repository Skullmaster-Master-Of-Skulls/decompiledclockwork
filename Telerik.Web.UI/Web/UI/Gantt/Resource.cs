using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Web.UI;

namespace Telerik.Web.UI.Gantt
{
	// Token: 0x02000345 RID: 837
	[Serializable]
	public class Resource : StateManager, IResource, IResourceBase, IMarkableStateManager, IStateManager
	{
		// Token: 0x170009AE RID: 2478
		// (get) Token: 0x06001C8B RID: 7307 RVA: 0x0005A54F File Offset: 0x0005874F
		// (set) Token: 0x06001C8C RID: 7308 RVA: 0x0005A561 File Offset: 0x00058761
		public object ID
		{
			get
			{
				return base.ViewState["ID"];
			}
			set
			{
				base.ViewState["ID"] = value;
			}
		}

		// Token: 0x170009AF RID: 2479
		// (get) Token: 0x06001C8D RID: 7309 RVA: 0x0005A574 File Offset: 0x00058774
		// (set) Token: 0x06001C8E RID: 7310 RVA: 0x0005A594 File Offset: 0x00058794
		public string Text
		{
			get
			{
				return (string)(base.ViewState["Text"] ?? string.Empty);
			}
			set
			{
				base.ViewState["Text"] = value;
			}
		}

		// Token: 0x170009B0 RID: 2480
		// (get) Token: 0x06001C8F RID: 7311 RVA: 0x0005A5A7 File Offset: 0x000587A7
		// (set) Token: 0x06001C90 RID: 7312 RVA: 0x0005A5CC File Offset: 0x000587CC
		public Color Color
		{
			get
			{
				return (Color)(base.ViewState["Color"] ?? Color.Empty);
			}
			set
			{
				base.ViewState["Color"] = value;
			}
		}

		// Token: 0x170009B1 RID: 2481
		// (get) Token: 0x06001C91 RID: 7313 RVA: 0x0005A5E4 File Offset: 0x000587E4
		// (set) Token: 0x06001C92 RID: 7314 RVA: 0x0005A604 File Offset: 0x00058804
		public string Format
		{
			get
			{
				return (string)(base.ViewState["Format"] ?? string.Empty);
			}
			set
			{
				base.ViewState["Format"] = value;
			}
		}

		// Token: 0x06001C93 RID: 7315 RVA: 0x0005A618 File Offset: 0x00058818
		protected internal virtual IDictionary<string, object> GetSerializationData()
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary["ID"] = this.ID;
			dictionary["Text"] = this.Text;
			if (!this.Color.IsEmpty)
			{
				dictionary["Color"] = ColorTranslator.ToHtml(this.Color);
			}
			if (!string.IsNullOrEmpty(this.Format))
			{
				dictionary["Format"] = this.Format;
			}
			return dictionary;
		}

		// Token: 0x06001C94 RID: 7316 RVA: 0x0005A694 File Offset: 0x00058894
		public IOrderedDictionary GetData()
		{
			return new OrderedDictionary
			{
				{
					"ID",
					this.ID
				},
				{
					"Text",
					this.Text
				},
				{
					"Color",
					this.Color
				},
				{
					"Format",
					this.Format
				}
			};
		}

		// Token: 0x06001C95 RID: 7317 RVA: 0x0005A76C File Offset: 0x0005896C
		public void LoadFromDictionary(IDictionary values)
		{
			Dictionary<string, Action<object>> dictionary = new Dictionary<string, Action<object>>();
			dictionary.Add("ID", delegate(object obj)
			{
				if (obj is string)
				{
					try
					{
						Guid guid = new Guid(obj.ToString());
						obj = guid;
					}
					catch
					{
					}
				}
				this.ID = obj;
			});
			dictionary.Add("Text", delegate(object obj)
			{
				this.Text = (string)obj;
			});
			dictionary.Add("Color", delegate(object obj)
			{
				this.Color = ColorTranslator.FromHtml((string)obj);
			});
			dictionary.Add("Format", delegate(object obj)
			{
				this.Format = (string)obj;
			});
			foreach (string key in Resource.ResourceDataKeys.Keys)
			{
				if (values.Contains(key))
				{
					dictionary[key](values[key]);
				}
			}
		}

		// Token: 0x02000346 RID: 838
		internal static class ResourceDataKeys
		{
			// Token: 0x04000742 RID: 1858
			public const string ID = "ID";

			// Token: 0x04000743 RID: 1859
			public const string Text = "Text";

			// Token: 0x04000744 RID: 1860
			public const string Color = "Color";

			// Token: 0x04000745 RID: 1861
			public const string Format = "Format";

			// Token: 0x04000746 RID: 1862
			public static IList<string> Keys = new List<string>
			{
				"ID",
				"Text",
				"Color",
				"Format"
			};
		}
	}
}
