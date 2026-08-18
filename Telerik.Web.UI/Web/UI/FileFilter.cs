using System;
using System.ComponentModel;
using Telerik.Charting.Styles;

namespace Telerik.Web.UI
{
	// Token: 0x0200006A RID: 106
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class FileFilter : StateManager
	{
		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000468 RID: 1128 RVA: 0x0000B735 File Offset: 0x00009935
		// (set) Token: 0x06000469 RID: 1129 RVA: 0x0000B755 File Offset: 0x00009955
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		public string Description
		{
			get
			{
				return ((string)base.ViewState["Description"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["Description"] = value;
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x0600046A RID: 1130 RVA: 0x0000B768 File Offset: 0x00009968
		// (set) Token: 0x0600046B RID: 1131 RVA: 0x0000B789 File Offset: 0x00009989
		[NotifyParentProperty(true)]
		[TypeConverter(typeof(ListConverter))]
		public string[] Extensions
		{
			get
			{
				return ((string[])base.ViewState["Extensions"]) ?? new string[0];
			}
			set
			{
				base.ViewState["Extensions"] = value;
			}
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x0000B79C File Offset: 0x0000999C
		public static string GetFilter(string[] extensions, bool indentation)
		{
			string[] array = new string[extensions.Length];
			extensions.CopyTo(array, 0);
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = "*." + array[i].Trim(new char[]
				{
					'.'
				});
			}
			return string.Format("{0}", string.Join(";" + (indentation ? " " : ""), array));
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x0000B813 File Offset: 0x00009A13
		public FileFilter()
		{
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x0000B81B File Offset: 0x00009A1B
		public FileFilter(string[] extensions) : this(string.Empty, extensions)
		{
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x0000B829 File Offset: 0x00009A29
		public FileFilter(string description, string[] extensions)
		{
			this.Description = description;
			this.Extensions = extensions;
		}
	}
}
