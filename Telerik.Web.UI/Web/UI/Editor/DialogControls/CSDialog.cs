using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Web;
using System.Web.Hosting;
using Telerik.Web.UI.com.hisoftware.api2;
using Telerik.Web.UI.Dialogs;

namespace Telerik.Web.UI.Editor.DialogControls
{
	// Token: 0x02000F6E RID: 3950
	[ToolboxItem(false)]
	[ClientScriptResource("Telerik.Web.UI.Widgets.CSDialog", "Telerik.Web.UI.Common.Core.js")]
	public class CSDialog : UserControlBase, IClientParameterConsumer
	{
		// Token: 0x17002FD5 RID: 12245
		// (get) Token: 0x06009766 RID: 38758 RVA: 0x0021EF07 File Offset: 0x0021D107
		public override string DialogName
		{
			get
			{
				return "CSDialog";
			}
		}

		// Token: 0x06009767 RID: 38759 RVA: 0x0021EF10 File Offset: 0x0021D110
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			this.EnsureChildControls();
			this.Page.Application["Telerik.Web.UI.Key"] = "HiSoftware Compliance Sheriff";
			if (this.Page.IsPostBack)
			{
				NameValueCollection form = this.Page.Request.Form;
				string text = ContentEncoder.Decode(form["editorContent"]);
				string a = form["editorFullPage"];
				string content = string.Empty;
				if (a != "true")
				{
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.1//EN\" \"http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd\">\n");
					stringBuilder.Append("<html xmlns=\"http://www.w3.org/1999/xhtml\">\n");
					stringBuilder.Append("<head> <title>Validation input</title>\n<meta http-equiv=\"Content-Type\" content=\"text/xhtml; charset=UTF-8\"/>\n");
					stringBuilder.Append("</head>\n<body style=\"margin: 0px;\">");
					stringBuilder.Append(text);
					stringBuilder.Append("</body>\n</html>\n");
					content = stringBuilder.ToString();
				}
				else
				{
					content = text;
				}
				string text2;
				if (HttpContext.Current != null)
				{
					text2 = HttpContext.Current.Request.Url.AbsoluteUri;
				}
				else
				{
					text2 = HostingEnvironment.ApplicationPhysicalPath;
				}
				string title = "Telerik RadEditor - " + text2;
				string text3 = ConfigurationManager.AppSettings["ComplianceSheriff"];
				if (!string.IsNullOrEmpty(text3))
				{
					text3 += "Service.svc";
				}
				string s = CSDialog.CallService(content, title, text2, text3);
				this.Page.Response.Clear();
				this.Page.Response.Write(s);
				this.Page.Response.Flush();
				this.Page.Response.End();
			}
		}

		// Token: 0x06009768 RID: 38760 RVA: 0x0021F0A8 File Offset: 0x0021D2A8
		private static string CallService(string content, string title, string url, string newSvcUrl)
		{
			if (!string.IsNullOrEmpty(newSvcUrl))
			{
				CSDialog.endpointAddress = new EndpointAddress(new Uri(newSvcUrl), new AddressHeader[0]);
				CSDialog.basicHttpBinding = new BasicHttpBinding("basicHttpEndpointBasic");
				CSDialog.proxy = new BasicClient(CSDialog.basicHttpBinding, CSDialog.endpointAddress);
			}
			else
			{
				CSDialog.proxy = new BasicClient("basicHttpEndpointBasic");
			}
			List<Result> list = CSDialog.CallWCF(content, title, url);
			string str = "";
			string text = "<html><head></head><body style=\"margin: 0px;\">";
			bool flag = true;
			foreach (Result result in list)
			{
				if (result.ResultType == ResultType.Fail)
				{
					str = result.CachedUrl;
					flag = false;
					break;
				}
				if (result.ResultType == ResultType.Warning)
				{
					str = result.CachedUrl;
					flag = false;
				}
				else if (result.ResultType == ResultType.Visual)
				{
					str = result.CachedUrl;
					flag = false;
				}
			}
			if (flag)
			{
				if (string.IsNullOrEmpty(CSDialog.error404))
				{
					text += "<h2>Content has passed the compliance verification</h2>";
				}
				else
				{
					text += "<h2>There was an error.</h2>";
					text = text + "<h3>" + CSDialog.error404 + "</h3>";
					if (CSDialog.error404.Equals("ApiKey is empty"))
					{
						text += "<h4>If you do not have an API Key, please register at <a href='http://www.hisoftware.com/trials/TelerikTrialRequest.htm' target='_self'>HiSoftware.com</a></h4>";
					}
					else
					{
						text += "<h4>If necessary, please contact <a href='mailto:professionalservices@hisoftware.com'>professionalservices@hisoftware.com</a> for assistance.</h4>";
					}
				}
			}
			else
			{
				text = text + "<iframe src=\"" + str + "\" frameborder=\"0\" name=\"resultsIframe\" id=\"resultsIframe\"";
				text += "style=\"width: 100%;height: 100%;\" scrolling=\"yes\"></iframe>";
			}
			text += "<script>window.parent.hideLoadingPanel()</script>";
			text += "</body></html>";
			return text;
		}

		// Token: 0x06009769 RID: 38761 RVA: 0x0021F23C File Offset: 0x0021D43C
		public static List<Result> CallWCF(string htmlContent, string display, string urlName)
		{
			List<Result> list = new List<Result>();
			try
			{
				CSDialog.apiKey = ConfigurationManager.AppSettings["CSApiKey"];
				int expiryTime = 600;
				string text = ConfigurationManager.AppSettings["CSExpiry"];
				if (!string.IsNullOrEmpty(text))
				{
					expiryTime = int.Parse(text);
				}
				List<string> list2 = new List<string>();
				list2.Add("AFM_Telerik");
				string text2 = ConfigurationManager.AppSettings["CSCkptGrps"];
				if (!string.IsNullOrEmpty(text2))
				{
					list2.Clear();
					string[] array = text2.Split(new char[]
					{
						','
					});
					for (int i = 0; i < array.Length; i++)
					{
						list2.Add(array[i]);
					}
				}
				ResultInformation resultInformation = CSDialog.proxy.RunOnDemandScanContent(CSDialog.apiKey, display, urlName, list2, Encoding.UTF8.GetBytes(htmlContent), Encoding.UTF8.WebName, expiryTime);
				ResultInformation resultsFull = CSDialog.proxy.GetResultsFull(CSDialog.apiKey, resultInformation.ID);
				foreach (CheckpointGroupResults checkpointGroupResults in resultsFull.CheckpointGroupResults)
				{
					foreach (CheckpointResults checkpointResults in checkpointGroupResults.Results)
					{
						foreach (Result result in checkpointResults.Results)
						{
							if (result.ResultType == ResultType.Fail || result.ResultType == ResultType.Warning || result.ResultType == ResultType.Visual)
							{
								list.Add(result);
							}
						}
					}
				}
			}
			catch (EndpointNotFoundException ex)
			{
				CSDialog.error404 = ex.Message;
			}
			catch (FaultException<InvalidApiKeyException> faultException)
			{
				CSDialog.error404 = faultException.Reason.ToString();
			}
			catch (FaultException<OnDemandScanCouldNotRunException> faultException2)
			{
				CSDialog.error404 = faultException2.Reason.ToString();
			}
			catch (Exception ex2)
			{
				CSDialog.error404 = ex2.Message;
				throw ex2;
			}
			finally
			{
				if (CSDialog.proxy.ChannelFactory.State != CommunicationState.Closed || CSDialog.proxy.ChannelFactory.State != CommunicationState.Closing || CSDialog.proxy.ChannelFactory.State != CommunicationState.Faulted)
				{
					CSDialog.proxy.Close();
				}
			}
			return list;
		}

		// Token: 0x04002B45 RID: 11077
		private static string error404 = "";

		// Token: 0x04002B46 RID: 11078
		private static string apiKey = "";

		// Token: 0x04002B47 RID: 11079
		private static BasicClient proxy;

		// Token: 0x04002B48 RID: 11080
		private static EndpointAddress endpointAddress = null;

		// Token: 0x04002B49 RID: 11081
		private static BasicHttpBinding basicHttpBinding = null;
	}
}
