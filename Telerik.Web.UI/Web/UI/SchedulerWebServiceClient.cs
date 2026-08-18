using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Permissions;
using System.Text;
using Telerik.Web.UI.Scheduling;

namespace Telerik.Web.UI
{
	// Token: 0x02000E65 RID: 3685
	internal class SchedulerWebServiceClient
	{
		// Token: 0x17002C30 RID: 11312
		// (get) Token: 0x06008BC6 RID: 35782 RVA: 0x001FC5C1 File Offset: 0x001FA7C1
		// (set) Token: 0x06008BC7 RID: 35783 RVA: 0x001FC5C9 File Offset: 0x001FA7C9
		private RadScheduler Owner
		{
			get
			{
				return this._owner;
			}
			set
			{
				this._owner = value;
			}
		}

		// Token: 0x17002C31 RID: 11313
		// (get) Token: 0x06008BC8 RID: 35784 RVA: 0x001FC5D2 File Offset: 0x001FA7D2
		private ResourceDataSerializer Serializer
		{
			get
			{
				if (this._serializer == null)
				{
					this._serializer = new ResourceDataSerializer();
				}
				return this._serializer;
			}
		}

		// Token: 0x17002C32 RID: 11314
		// (get) Token: 0x06008BC9 RID: 35785 RVA: 0x001FC5ED File Offset: 0x001FA7ED
		protected SchedulerWebServiceSettings Settings
		{
			get
			{
				return this.Owner.WebServiceSettings;
			}
		}

		// Token: 0x06008BCA RID: 35786 RVA: 0x001FC5FA File Offset: 0x001FA7FA
		public SchedulerWebServiceClient(RadScheduler owner)
		{
			this.Owner = owner;
		}

		// Token: 0x06008BCB RID: 35787 RVA: 0x001FC60C File Offset: 0x001FA80C
		public IEnumerable<Resource> GetResources()
		{
			List<Resource> list = new List<Resource>();
			IEnumerable<Resource> result;
			using (WebClient webClient = this.GetWebClient())
			{
				ResourcesPopulatingEventArgs resourcesPopulatingEventArgs = new ResourcesPopulatingEventArgs(this.GetSchedulerInfo(), webClient.BaseAddress, webClient.Headers);
				if (!this.Owner.OnResourcesPopulating(resourcesPopulatingEventArgs))
				{
					result = list;
				}
				else if (!SchedulerWebServiceClient.HasPermission(resourcesPopulatingEventArgs.ServicePath))
				{
					if (this.ResourcesRequired())
					{
						throw new NotSupportedException("Not enough permissions to retrieve RadScheduler's resources from the Web Service. Verify that System.Net.WebPermission is granted for the service URL. By default, this permission is not granted in Medium trust. Alternatively, you can populate the resources in the OnInit method of the page that hosts the RadScheduler control and set WebService-ResourcePopulationMode to Manual.");
					}
					result = list;
				}
				else
				{
					this.UpdateClient(webClient, resourcesPopulatingEventArgs);
					result = this.LoadResources(webClient, resourcesPopulatingEventArgs);
				}
			}
			return result;
		}

		// Token: 0x06008BCC RID: 35788 RVA: 0x001FC6A4 File Offset: 0x001FA8A4
		private void UpdateClient(WebClient client, ResourcesPopulatingEventArgs args)
		{
			client.BaseAddress = args.ServicePath;
			client.Credentials = args.Credentials;
			if (args.Proxy != null)
			{
				client.Proxy = args.Proxy;
			}
		}

		// Token: 0x06008BCD RID: 35789 RVA: 0x001FC6D4 File Offset: 0x001FA8D4
		protected virtual List<Resource> LoadResources(WebClient client, ResourcesPopulatingEventArgs args)
		{
			string json = "";
			string text = this.Serializer.Serialize(args.SchedulerInfo);
			try
			{
				if (this.Settings.UseHttpGet)
				{
					string address = string.Format("{0}?schedulerInfo={1}", this.Settings.GetResourcesMethod, Uri.EscapeDataString(text));
					json = client.DownloadString(address);
				}
				else
				{
					string data = string.Format("{{\"schedulerInfo\":{0}}}", text);
					json = client.UploadString(this.Settings.GetResourcesMethod, "POST", data);
				}
			}
			catch (WebException webEx)
			{
				this.HandleWebException(webEx);
			}
			ResourceData[] deserialiedResponse = this.Serializer.Deserialize(json);
			return this.ParseResourceData(deserialiedResponse);
		}

		// Token: 0x06008BCE RID: 35790 RVA: 0x001FC784 File Offset: 0x001FA984
		protected List<Resource> ParseResourceData(ResourceData[] deserialiedResponse)
		{
			List<Resource> list = new List<Resource>();
			foreach (ResourceData resourceData in deserialiedResponse)
			{
				Resource resource = new Resource();
				resourceData.CopyTo(resource);
				list.Add(resource);
			}
			return list;
		}

		// Token: 0x06008BCF RID: 35791 RVA: 0x001FC7C8 File Offset: 0x001FA9C8
		private void HandleWebException(WebException webEx)
		{
			string message = "Unable to retrieve response message";
			try
			{
				Stream responseStream = webEx.Response.GetResponseStream();
				int num = (int)Math.Min(8096L, responseStream.Length);
				byte[] array = new byte[num];
				responseStream.Read(array, 0, num);
				string @string = Encoding.UTF8.GetString(array);
				message = string.Format("An error occurred while requesting resources from the web service. Server responded with: {0}", @string);
			}
			catch (Exception)
			{
			}
			throw new Exception(message, webEx);
		}

		// Token: 0x06008BD0 RID: 35792 RVA: 0x001FC844 File Offset: 0x001FAA44
		private bool ResourcesRequired()
		{
			return !string.IsNullOrEmpty(this.Owner.GroupBy) || (!string.IsNullOrEmpty(this.Owner.DayView.GroupBy) && this.Owner.DayView.UserSelectable) || (!string.IsNullOrEmpty(this.Owner.WeekView.GroupBy) && this.Owner.WeekView.UserSelectable) || (!string.IsNullOrEmpty(this.Owner.MonthView.GroupBy) && this.Owner.MonthView.UserSelectable) || (!string.IsNullOrEmpty(this.Owner.TimelineView.GroupBy) && this.Owner.TimelineView.UserSelectable) || (!string.IsNullOrEmpty(this.Owner.MultiDayView.GroupBy) && this.Owner.MultiDayView.UserSelectable);
		}

		// Token: 0x06008BD1 RID: 35793 RVA: 0x001FC93D File Offset: 0x001FAB3D
		private static bool HasPermission(string servicePath)
		{
			return SecurityHelper.IsPermissionGranted(new ReflectionPermission(PermissionState.Unrestricted));
		}

		// Token: 0x06008BD2 RID: 35794 RVA: 0x001FC94C File Offset: 0x001FAB4C
		private SchedulerInfo GetSchedulerInfo()
		{
			return new SchedulerInfo
			{
				ViewStart = this.Owner.VisibleRangeStart,
				ViewEnd = this.Owner.VisibleRangeEnd,
				MinutesPerRow = this.Owner.MinutesPerRow,
				TimeZoneOffset = (int)this.Owner.TimeZoneOffset.TotalMilliseconds
			};
		}

		// Token: 0x06008BD3 RID: 35795 RVA: 0x001FC9B0 File Offset: 0x001FABB0
		private WebClient GetWebClient()
		{
			return new WebClient
			{
				BaseAddress = this.GetWebServicePath(),
				Headers = 
				{
					{
						HttpRequestHeader.ContentType,
						"application/json; charset=utf-8"
					}
				}
			};
		}

		// Token: 0x06008BD4 RID: 35796 RVA: 0x001FC9E4 File Offset: 0x001FABE4
		private string GetWebServicePath()
		{
			string text = this.Owner.ResolveUrl(this.Settings.Path);
			if (text.Contains("://"))
			{
				if (!text.EndsWith("/"))
				{
					text += "/";
				}
				return text;
			}
			Uri url = this.Owner.Page.Request.Url;
			return string.Format("{0}://{1}:{2}{3}/", new object[]
			{
				url.Scheme,
				url.DnsSafeHost,
				url.Port,
				text
			});
		}

		// Token: 0x04002725 RID: 10021
		private RadScheduler _owner;

		// Token: 0x04002726 RID: 10022
		private ResourceDataSerializer _serializer;
	}
}
