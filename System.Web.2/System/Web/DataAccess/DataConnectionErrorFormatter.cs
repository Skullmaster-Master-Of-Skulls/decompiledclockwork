using System;
using System.Collections.Specialized;
using System.Globalization;
using System.Web.Handlers;
using System.Web.UI;

namespace System.Web.DataAccess
{
	// Token: 0x020001AC RID: 428
	internal class DataConnectionErrorFormatter : ErrorFormatter
	{
		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x06001648 RID: 5704 RVA: 0x0000298D File Offset: 0x00000B8D
		protected override string ErrorTitle
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x06001649 RID: 5705 RVA: 0x0000298D File Offset: 0x00000B8D
		protected override string Description
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x0600164A RID: 5706 RVA: 0x00046738 File Offset: 0x00044938
		protected override string MiscSectionTitle
		{
			get
			{
				return SR.GetString("DataAccessError_MiscSectionTitle");
			}
		}

		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x0600164B RID: 5707 RVA: 0x00046744 File Offset: 0x00044944
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

		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x0600164C RID: 5708 RVA: 0x0000298D File Offset: 0x00000B8D
		protected override string ColoredSquareTitle
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x0600164D RID: 5709 RVA: 0x0000298D File Offset: 0x00000B8D
		protected override string ColoredSquareContent
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x0600164E RID: 5710 RVA: 0x00007722 File Offset: 0x00005922
		protected override bool ShowSourceFileInfo
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600164F RID: 5711 RVA: 0x00046998 File Offset: 0x00044B98
		private string GetResourceStringAndSetAdaptiveNumberedText(ref int currentNumber, string resourceId)
		{
			string @string = SR.GetString(resourceId);
			this.SetAdaptiveNumberedText(ref currentNumber, @string);
			return @string;
		}

		// Token: 0x06001650 RID: 5712 RVA: 0x000469B8 File Offset: 0x00044BB8
		private string GetResourceStringAndSetAdaptiveNumberedText(ref int currentNumber, string resourceId, string parameter1)
		{
			string @string = SR.GetString(resourceId, new object[]
			{
				parameter1
			});
			this.SetAdaptiveNumberedText(ref currentNumber, @string);
			return @string;
		}

		// Token: 0x06001651 RID: 5713 RVA: 0x000469E0 File Offset: 0x00044BE0
		private void SetAdaptiveNumberedText(ref int currentNumber, string resourceString)
		{
			string value = currentNumber.ToString(CultureInfo.InvariantCulture) + " " + resourceString;
			this.AdaptiveMiscContent.Add(value);
			currentNumber++;
		}

		// Token: 0x04001693 RID: 5779
		protected static NameValueCollection s_errMessages = new NameValueCollection();

		// Token: 0x04001694 RID: 5780
		protected static object s_Lock = new object();

		// Token: 0x04001695 RID: 5781
		protected string _UserName;

		// Token: 0x04001696 RID: 5782
		protected DataConnectionErrorEnum _Error;
	}
}
