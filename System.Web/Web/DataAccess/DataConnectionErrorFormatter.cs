using System;
using System.Collections.Specialized;
using System.Globalization;
using System.Web.Handlers;
using System.Web.UI;

namespace System.Web.DataAccess
{
	// Token: 0x02000277 RID: 631
	internal class DataConnectionErrorFormatter : ErrorFormatter
	{
		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x060020C3 RID: 8387 RVA: 0x0008E758 File Offset: 0x0008D758
		protected override string ErrorTitle
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x060020C4 RID: 8388 RVA: 0x0008E75B File Offset: 0x0008D75B
		protected override string Description
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x060020C5 RID: 8389 RVA: 0x0008E75E File Offset: 0x0008D75E
		protected override string MiscSectionTitle
		{
			get
			{
				return SR.GetString("DataAccessError_MiscSectionTitle");
			}
		}

		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x060020C6 RID: 8390 RVA: 0x0008E76C File Offset: 0x0008D76C
		protected override string MiscSectionContent
		{
			get
			{
				int num = 1;
				string resourceStringAndSetAdaptiveNumberedText = this.GetResourceStringAndSetAdaptiveNumberedText(ref num, "DataAccessError_MiscSection_1");
				string str = "<ol>\n<li>" + resourceStringAndSetAdaptiveNumberedText + "</li>\n";
				switch (this._Error)
				{
				case DataConnectionErrorEnum.CanNotCreateDataDir:
					resourceStringAndSetAdaptiveNumberedText = this.GetResourceStringAndSetAdaptiveNumberedText(ref num, "DataAccessError_MiscSection_2_CanNotCreateDataDir");
					str = str + "<li>" + resourceStringAndSetAdaptiveNumberedText + "</li>\n";
					resourceStringAndSetAdaptiveNumberedText = this.GetResourceStringAndSetAdaptiveNumberedText(ref num, "DataAccessError_MiscSection_2");
					str = str + "<li>" + resourceStringAndSetAdaptiveNumberedText + "</li>\n";
					break;
				case DataConnectionErrorEnum.CanNotWriteToDataDir:
					resourceStringAndSetAdaptiveNumberedText = this.GetResourceStringAndSetAdaptiveNumberedText(ref num, "DataAccessError_MiscSection_2");
					str = str + "<li>" + resourceStringAndSetAdaptiveNumberedText + "</li>\n";
					break;
				case DataConnectionErrorEnum.CanNotWriteToDBFile:
					resourceStringAndSetAdaptiveNumberedText = this.GetResourceStringAndSetAdaptiveNumberedText(ref num, "DataAccessError_MiscSection_2_CanNotWriteToDBFile_a");
					str = str + "<li>" + resourceStringAndSetAdaptiveNumberedText + "</li>\n";
					resourceStringAndSetAdaptiveNumberedText = this.GetResourceStringAndSetAdaptiveNumberedText(ref num, "DataAccessError_MiscSection_2_CanNotWriteToDBFile_b");
					str = str + "<li>" + resourceStringAndSetAdaptiveNumberedText + "</li>\n";
					break;
				}
				resourceStringAndSetAdaptiveNumberedText = this.GetResourceStringAndSetAdaptiveNumberedText(ref num, "DataAccessError_MiscSection_3");
				str = str + "<li>" + resourceStringAndSetAdaptiveNumberedText + "<br></li>\n";
				string webResourceUrl = AssemblyResourceLoader.GetWebResourceUrl(typeof(Page), "properties_security_tab.gif", true);
				str = str + "<br><br><IMG SRC=\"" + webResourceUrl + "\"><br><br><br>";
				resourceStringAndSetAdaptiveNumberedText = this.GetResourceStringAndSetAdaptiveNumberedText(ref num, "DataAccessError_MiscSection_ClickAdd");
				str = str + "<li>" + resourceStringAndSetAdaptiveNumberedText + "</li>\n";
				webResourceUrl = AssemblyResourceLoader.GetWebResourceUrl(typeof(Page), "add_permissions_for_users.gif", true);
				str = str + "<br><br><IMG SRC=\"" + webResourceUrl + "\"><br><br>";
				string resourceStringAndSetAdaptiveNumberedText2;
				if (!string.IsNullOrEmpty(this._UserName))
				{
					resourceStringAndSetAdaptiveNumberedText2 = this.GetResourceStringAndSetAdaptiveNumberedText(ref num, "DataAccessError_MiscSection_4", this._UserName);
				}
				else
				{
					resourceStringAndSetAdaptiveNumberedText2 = this.GetResourceStringAndSetAdaptiveNumberedText(ref num, "DataAccessError_MiscSection_4_2");
				}
				str = str + "<li>" + resourceStringAndSetAdaptiveNumberedText2 + "</li>\n";
				resourceStringAndSetAdaptiveNumberedText = this.GetResourceStringAndSetAdaptiveNumberedText(ref num, "DataAccessError_MiscSection_ClickOK");
				str = str + "<li>" + resourceStringAndSetAdaptiveNumberedText + "</li>\n";
				resourceStringAndSetAdaptiveNumberedText = this.GetResourceStringAndSetAdaptiveNumberedText(ref num, "DataAccessError_MiscSection_5");
				str = str + "<li>" + resourceStringAndSetAdaptiveNumberedText + "</li>\n";
				webResourceUrl = AssemblyResourceLoader.GetWebResourceUrl(typeof(Page), "properties_security_tab_w_user.gif", true);
				str = str + "<br><br><IMG SRC=\"" + webResourceUrl + "\"><br><br>";
				resourceStringAndSetAdaptiveNumberedText = this.GetResourceStringAndSetAdaptiveNumberedText(ref num, "DataAccessError_MiscSection_ClickOK");
				return str + "<li>" + resourceStringAndSetAdaptiveNumberedText + "</li>\n";
			}
		}

		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x060020C7 RID: 8391 RVA: 0x0008E9BD File Offset: 0x0008D9BD
		protected override string ColoredSquareTitle
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000719 RID: 1817
		// (get) Token: 0x060020C8 RID: 8392 RVA: 0x0008E9C0 File Offset: 0x0008D9C0
		protected override string ColoredSquareContent
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x060020C9 RID: 8393 RVA: 0x0008E9C3 File Offset: 0x0008D9C3
		protected override bool ShowSourceFileInfo
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060020CA RID: 8394 RVA: 0x0008E9C8 File Offset: 0x0008D9C8
		private string GetResourceStringAndSetAdaptiveNumberedText(ref int currentNumber, string resourceId)
		{
			string @string = SR.GetString(resourceId);
			this.SetAdaptiveNumberedText(ref currentNumber, @string);
			return @string;
		}

		// Token: 0x060020CB RID: 8395 RVA: 0x0008E9E8 File Offset: 0x0008D9E8
		private string GetResourceStringAndSetAdaptiveNumberedText(ref int currentNumber, string resourceId, string parameter1)
		{
			string @string = SR.GetString(resourceId, new object[]
			{
				parameter1
			});
			this.SetAdaptiveNumberedText(ref currentNumber, @string);
			return @string;
		}

		// Token: 0x060020CC RID: 8396 RVA: 0x0008EA14 File Offset: 0x0008DA14
		private void SetAdaptiveNumberedText(ref int currentNumber, string resourceString)
		{
			string value = currentNumber.ToString(CultureInfo.InvariantCulture) + " " + resourceString;
			this.AdaptiveMiscContent.Add(value);
			currentNumber++;
		}

		// Token: 0x04001ACA RID: 6858
		protected static NameValueCollection s_errMessages = new NameValueCollection();

		// Token: 0x04001ACB RID: 6859
		protected static object s_Lock = new object();

		// Token: 0x04001ACC RID: 6860
		protected string _UserName;

		// Token: 0x04001ACD RID: 6861
		protected DataConnectionErrorEnum _Error;
	}
}
