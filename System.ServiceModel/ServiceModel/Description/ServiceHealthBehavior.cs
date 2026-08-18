using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Resources;
using System.Runtime.Serialization;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Xml;
using System.Xml.XPath;

namespace System.ServiceModel.Description
{
	// Token: 0x02000434 RID: 1076
	public class ServiceHealthBehavior : ServiceHealthBehaviorBase
	{
		// Token: 0x17000A44 RID: 2628
		// (get) Token: 0x060029F6 RID: 10742 RVA: 0x000A1E96 File Offset: 0x000A0096
		protected virtual bool HasXmlSupport { get; } = 1;

		// Token: 0x060029F7 RID: 10743 RVA: 0x000A1EA0 File Offset: 0x000A00A0
		public override void HandleHealthRequest(ServiceHostBase serviceHost, Message httpGetRequest, string[] queries, out Message replyMessage)
		{
			if (serviceHost == null)
			{
				throw new ArgumentNullException("serviceHost");
			}
			if (httpGetRequest == null)
			{
				throw new ArgumentNullException("httpGetRequest");
			}
			if (queries == null)
			{
				throw new ArgumentNullException("queries");
			}
			replyMessage = null;
			bool flag = false;
			bool flag2 = false;
			foreach (string parameter in queries)
			{
				bool flag3;
				if (ServiceHealthBehavior.TryParseBooleanQueryParameter("noContent", parameter, true, out flag3))
				{
					flag2 = flag3;
				}
				else if (ServiceHealthBehavior.TryParseBooleanQueryParameter("xml", parameter, true, out flag3))
				{
					flag = flag3;
				}
			}
			HttpStatusCode httpResponseCode = this.GetHttpResponseCode(serviceHost, queries);
			if (base.HealthDetailsEnabled && !flag2)
			{
				if (flag && this.HasXmlSupport)
				{
					XmlDocument xmlDocument = this.GetXmlDocument(serviceHost);
					if (xmlDocument != null)
					{
						replyMessage = new ServiceHealthBehavior.XmlDocumentMessage(xmlDocument);
					}
				}
				else
				{
					ServiceHealthSectionCollection serviceHealthSections = this.GetServiceHealthSections(serviceHost);
					if (serviceHealthSections != null && serviceHealthSections.Count > 0)
					{
						string serviceName = ServiceHealthBehavior.GetServiceName(serviceHost);
						replyMessage = new ServiceHealthBehavior.ServiceHealthMessage(serviceHealthSections, serviceName, (int)httpResponseCode);
					}
				}
			}
			if (replyMessage == null)
			{
				replyMessage = new ServiceHealthBehavior.EmptyMessage();
			}
			ServiceHealthBehavior.AddHttpProperty(replyMessage, httpResponseCode, flag);
		}

		// Token: 0x060029F8 RID: 10744 RVA: 0x000A1FA0 File Offset: 0x000A01A0
		protected virtual ServiceHealthSectionCollection GetServiceHealthSections(ServiceHostBase serviceHost)
		{
			if (serviceHost == null)
			{
				throw new ArgumentNullException("serviceHost");
			}
			ServiceHealthSectionCollection serviceHealthSectionCollection = new ServiceHealthSectionCollection();
			ServiceHealthModel serviceHealthModel = new ServiceHealthModel(serviceHost, base.ServiceStartTime);
			ServiceHealthSection serviceHealthSection = serviceHealthSectionCollection.CreateSection(SR.GetString("ServiceHealthBehavior_WCFServiceProperties"), "#0C5DA4", "#ffffff");
			ServiceHealthDataCollection serviceHealthDataCollection = serviceHealthSection.CreateElementsCollection();
			serviceHealthDataCollection.Add(SR.GetString("ServiceHealthBehavior_ServiceName"), serviceHealthModel.ServiceProperties.Name);
			serviceHealthDataCollection.Add(SR.GetString("ServiceHealthBehavior_State"), this.FormatCommunicationState(new CommunicationState?(serviceHealthModel.ServiceProperties.State)));
			serviceHealthDataCollection.Add(SR.GetString("ServiceHealthBehavior_ServiceType"), serviceHealthModel.ServiceProperties.ServiceTypeName);
			InstanceContextMode? instanceContextMode;
			serviceHealthDataCollection.Add(SR.GetString("ServiceHealthBehavior_InstanceContextMode"), (serviceHealthModel.ServiceProperties.InstanceContextMode != null) ? instanceContextMode.GetValueOrDefault().ToString() : null);
			ConcurrencyMode? concurrencyMode;
			serviceHealthDataCollection.Add(SR.GetString("ServiceHealthBehavior_ConcurrencyMode"), (serviceHealthModel.ServiceProperties.ConcurrencyMode != null) ? concurrencyMode.GetValueOrDefault().ToString() : null);
			serviceHealthDataCollection.Add(SR.GetString("ServiceHealthBehavior_BaseAddresses"), serviceHealthModel.ServiceProperties.BaseAddresses);
			serviceHealthDataCollection.Add(SR.GetString("ServiceHealthBehavior_ServiceThrottles"), this.FormatServiceThrottle(serviceHealthModel.ServiceProperties.ServiceThrottle));
			serviceHealthDataCollection.Add(SR.GetString("ServiceHealthBehavior_ServiceBehaviors"), serviceHealthModel.ServiceProperties.ServiceBehaviorNames);
			ServiceHealthSection serviceHealthSection2 = serviceHealthSectionCollection.CreateSection(SR.GetString("ServiceHealthBehavior_ProcessInformation"), "#2C4079", "#ffffff");
			ServiceHealthDataCollection serviceHealthDataCollection2 = serviceHealthSection2.CreateElementsCollection();
			serviceHealthDataCollection2.Add(SR.GetString("ServiceHealthBehavior_ProcessName"), serviceHealthModel.ProcessInformation.ProcessName);
			serviceHealthDataCollection2.Add(SR.GetString("ServiceHealthBehavior_ProcessBitness"), serviceHealthModel.ProcessInformation.Bitness.ToString());
			serviceHealthDataCollection2.Add(SR.GetString("ServiceHealthBehavior_ProcessRunningSince"), serviceHealthModel.ProcessInformation.ProcessStartDate.ToString());
			serviceHealthDataCollection2.Add(SR.GetString("ServiceHealthBehavior_ServiceRunningSince"), serviceHealthModel.ProcessInformation.ServiceStartDate.ToString());
			serviceHealthDataCollection2.Add(SR.GetString("ServiceHealthBehavior_Uptime"), serviceHealthModel.ProcessInformation.Uptime.ToString("dd\\.hh\\:mm\\:ss"));
			serviceHealthDataCollection2.Add(SR.GetString("ServiceHealthBehavior_GCMode"), serviceHealthModel.ProcessInformation.GCMode);
			serviceHealthDataCollection2.Add(SR.GetString("ServiceHealthBehavior_Threads"), this.FormatThreads(serviceHealthModel.ProcessInformation.Threads));
			if (serviceHealthModel.ServiceEndpoints != null && serviceHealthModel.ServiceEndpoints.Length != 0)
			{
				ServiceHealthSection serviceHealthSection3 = serviceHealthSectionCollection.CreateSection(SR.GetString("ServiceHealthBehavior_Endpoints"), "#3e7185", "#ffffff");
				foreach (ServiceHealthModel.ServiceEndpointModel serviceEndpointModel in serviceHealthModel.ServiceEndpoints)
				{
					ServiceHealthDataCollection serviceHealthDataCollection3 = serviceHealthSection3.CreateElementsCollection();
					serviceHealthDataCollection3.Add(SR.GetString("ServiceHealthBehavior_Address"), serviceEndpointModel.Address);
					serviceHealthDataCollection3.Add(SR.GetString("ServiceHealthBehavior_Binding"), serviceEndpointModel.BindingName);
					serviceHealthDataCollection3.Add(SR.GetString("ServiceHealthBehavior_Contract"), serviceEndpointModel.ContractName);
					serviceHealthDataCollection3.Add(SR.GetString("ServiceHealthBehavior_EndpointBehaviors"), serviceEndpointModel.BehaviorNames);
				}
			}
			if (serviceHealthModel.ChannelDispatchers != null && serviceHealthModel.ChannelDispatchers.Length != 0)
			{
				ServiceHealthSection serviceHealthSection4 = serviceHealthSectionCollection.CreateSection(SR.GetString("ServiceHealthBehavior_ChannelDispatchers"), "#406BE8", "#ffffff");
				foreach (ServiceHealthModel.ChannelDispatcherModel channelDispatcherModel in serviceHealthModel.ChannelDispatchers)
				{
					ServiceHealthDataCollection serviceHealthDataCollection4 = serviceHealthSection4.CreateElementsCollection();
					serviceHealthDataCollection4.Add(SR.GetString("ServiceHealthBehavior_ListenerUri"), channelDispatcherModel.ListenerUri);
					serviceHealthDataCollection4.Add(SR.GetString("ServiceHealthBehavior_ListenerState"), this.FormatCommunicationState(channelDispatcherModel.ListenerState));
					serviceHealthDataCollection4.Add(SR.GetString("ServiceHealthBehavior_Binding"), channelDispatcherModel.BindingName);
					serviceHealthDataCollection4.Add(SR.GetString("ServiceHealthBehavior_State"), this.FormatCommunicationState(channelDispatcherModel.State));
					serviceHealthDataCollection4.Add(SR.GetString("ServiceHealthBehavior_MessageEncoder"), channelDispatcherModel.MessageEncoder);
					serviceHealthDataCollection4.Add(SR.GetString("ServiceHealthBehavior_Contract"), channelDispatcherModel.ContractName);
					serviceHealthDataCollection4.Add(SR.GetString("ServiceHealthBehavior_IsSystemEndpoint"), channelDispatcherModel.IsSystemEndpoint.ToString());
					serviceHealthDataCollection4.Add(SR.GetString("ServiceHealthBehavior_ChannelTimeouts"), this.FormatCommunicationTimeouts(channelDispatcherModel.CommunicationTimeouts));
					serviceHealthDataCollection4.Add(SR.GetString("ServiceHealthBehavior_MessageInspectors"), channelDispatcherModel.MessageInspectors);
				}
			}
			return serviceHealthSectionCollection;
		}

		// Token: 0x060029F9 RID: 10745 RVA: 0x000A245C File Offset: 0x000A065C
		protected virtual HttpStatusCode GetHttpResponseCode(ServiceHostBase serviceHost, string[] queries)
		{
			if (serviceHost == null)
			{
				throw new ArgumentNullException("serviceHost");
			}
			if (queries == null || queries.Length == 0)
			{
				return HttpStatusCode.OK;
			}
			bool flag = true;
			for (int i = 0; i < queries.Length; i++)
			{
				HttpStatusCode result;
				if (!ServiceHealthBehavior.TryParseHttpStatusCodeQueryParameter("OnServiceFailure", queries[i], HttpStatusCode.ServiceUnavailable, out result))
				{
					if (ServiceHealthBehavior.TryParseHttpStatusCodeQueryParameter("OnDispatcherFailure", queries[i], HttpStatusCode.ServiceUnavailable, out result))
					{
						flag = false;
						if (serviceHost.ChannelDispatchers == null)
						{
							goto IL_34E;
						}
						using (IEnumerator<ChannelDispatcherBase> enumerator = serviceHost.ChannelDispatchers.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								ChannelDispatcherBase channelDispatcherBase = enumerator.Current;
								ChannelDispatcher channelDispatcher = channelDispatcherBase as ChannelDispatcher;
								if (channelDispatcher != null && channelDispatcher.State > CommunicationState.Opened)
								{
									return result;
								}
							}
							goto IL_34E;
						}
					}
					if (ServiceHealthBehavior.TryParseHttpStatusCodeQueryParameter("OnListenerFailure", queries[i], HttpStatusCode.ServiceUnavailable, out result))
					{
						flag = false;
						if (serviceHost.ChannelDispatchers == null)
						{
							goto IL_34E;
						}
						using (IEnumerator<ChannelDispatcherBase> enumerator2 = serviceHost.ChannelDispatchers.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								ChannelDispatcherBase channelDispatcherBase2 = enumerator2.Current;
								if (channelDispatcherBase2.Listener != null && channelDispatcherBase2.Listener.State > CommunicationState.Opened)
								{
									return result;
								}
							}
							goto IL_34E;
						}
					}
					string[] array = queries[i].Split(new char[]
					{
						'='
					});
					if (serviceHost.ServiceThrottle == null || string.Compare(array[0], "OnThrottlePercentExceeded", StringComparison.OrdinalIgnoreCase) != 0)
					{
						goto IL_34E;
					}
					flag = false;
					if (array.Length != 2)
					{
						goto IL_34E;
					}
					string text = array[0];
					string text2 = array[1];
					string[] array2 = text2.Split(new char[]
					{
						',',
						';'
					}, StringSplitOptions.RemoveEmptyEntries);
					SortedDictionary<int, int> sortedDictionary = new SortedDictionary<int, int>(ServiceHealthBehavior.descendingComparer);
					foreach (string text3 in array2)
					{
						string[] array4 = text3.Split(new char[]
						{
							':'
						}, StringSplitOptions.RemoveEmptyEntries);
						if (array4.Length == 1)
						{
							array4 = new string[]
							{
								array4[0],
								503.ToString()
							};
						}
						int num;
						if (array4.Length == 2 && int.TryParse(array4[0], out num) && num >= 0 && num <= 100 && !sortedDictionary.ContainsKey(num))
						{
							int num2;
							if (!int.TryParse(array4[1], out num2) || !ServiceHealthBehavior.EnsureHttpStatusCode(num2))
							{
								num2 = 503;
							}
							sortedDictionary.Add(num, num2);
						}
					}
					if (sortedDictionary.Count > 0)
					{
						ServiceThrottle serviceThrottle = serviceHost.ServiceThrottle;
						int num3 = (serviceThrottle.Calls.Capacity == 0) ? 0 : (serviceThrottle.Calls.Count * 100 / serviceThrottle.Calls.Capacity);
						int num4 = (serviceThrottle.Sessions.Capacity == 0) ? 0 : (serviceThrottle.Sessions.Count * 100 / serviceThrottle.Sessions.Capacity);
						int num5 = (serviceThrottle.InstanceContexts.Capacity == 0) ? 0 : (serviceThrottle.InstanceContexts.Count * 100 / serviceThrottle.InstanceContexts.Capacity);
						foreach (KeyValuePair<int, int> keyValuePair in sortedDictionary)
						{
							int key = keyValuePair.Key;
							int value = keyValuePair.Value;
							if (num3 >= key || num4 >= key || num5 >= key)
							{
								return (HttpStatusCode)value;
							}
						}
					}
					goto IL_34E;
				}
				flag = false;
				if (serviceHost.State > CommunicationState.Opened)
				{
					return result;
				}
				IL_34E:;
			}
			if (flag)
			{
				bool flag2 = flag && serviceHost.State > CommunicationState.Opened;
				if (!flag2 && serviceHost.ChannelDispatchers != null)
				{
					foreach (ChannelDispatcherBase channelDispatcherBase3 in serviceHost.ChannelDispatchers)
					{
						ChannelDispatcher channelDispatcher2 = channelDispatcherBase3 as ChannelDispatcher;
						if ((channelDispatcherBase3.Listener != null && channelDispatcherBase3.Listener.State > CommunicationState.Opened) || (channelDispatcher2 != null && channelDispatcher2.State > CommunicationState.Opened))
						{
							flag2 = true;
							break;
						}
					}
				}
				if (flag2)
				{
					return HttpStatusCode.ServiceUnavailable;
				}
			}
			return HttpStatusCode.OK;
		}

		// Token: 0x060029FA RID: 10746 RVA: 0x000A2890 File Offset: 0x000A0A90
		protected virtual XmlDocument GetXmlDocument(ServiceHostBase serviceHost)
		{
			if (serviceHost == null)
			{
				throw new ArgumentNullException("serviceHost");
			}
			ServiceHealthModel source = new ServiceHealthModel(serviceHost, base.ServiceStartTime);
			return ServiceHealthBehavior.SerializeToXml<ServiceHealthModel>(source);
		}

		// Token: 0x060029FB RID: 10747 RVA: 0x000A28C0 File Offset: 0x000A0AC0
		private static XmlDocument SerializeToXml<T>(T source) where T : class
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			XmlDocument xmlDocument = new XmlDocument();
			XPathNavigator xpathNavigator = xmlDocument.CreateNavigator();
			using (XmlWriter xmlWriter = xpathNavigator.AppendChild())
			{
				DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(T));
				dataContractSerializer.WriteObject(xmlWriter, source);
			}
			return xmlDocument;
		}

		// Token: 0x060029FC RID: 10748 RVA: 0x000A2930 File Offset: 0x000A0B30
		internal static string GetServiceName(ServiceHostBase serviceHost)
		{
			ServiceDescription description = serviceHost.Description;
			string text = (description != null) ? description.Name : null;
			if (string.IsNullOrWhiteSpace(text))
			{
				ServiceDescription description2 = serviceHost.Description;
				text = ((description2 != null) ? description2.ServiceType.Name : null);
			}
			return text;
		}

		// Token: 0x060029FD RID: 10749 RVA: 0x000A2974 File Offset: 0x000A0B74
		protected static bool TryParseHttpStatusCodeQueryParameter(string parameterName, string parameter, HttpStatusCode defaultErrorCode, out HttpStatusCode result)
		{
			if (parameterName == null)
			{
				throw new ArgumentNullException(parameterName);
			}
			if (parameter == null)
			{
				throw new ArgumentNullException(parameter);
			}
			result = defaultErrorCode;
			string[] array = parameter.Split(new char[]
			{
				'='
			});
			if (string.Compare(array[0], parameterName, StringComparison.OrdinalIgnoreCase) == 0)
			{
				if (array.Length == 2 && !string.IsNullOrWhiteSpace(array[1]))
				{
					int num;
					result = (HttpStatusCode)((int.TryParse(array[1], out num) && ServiceHealthBehavior.EnsureHttpStatusCode(num)) ? num : ((int)defaultErrorCode));
				}
				return true;
			}
			return false;
		}

		// Token: 0x060029FE RID: 10750 RVA: 0x000A29E4 File Offset: 0x000A0BE4
		protected static bool TryParseBooleanQueryParameter(string parameterName, string parameter, bool defaultValue, out bool result)
		{
			if (parameterName == null)
			{
				throw new ArgumentNullException(parameterName);
			}
			if (parameter == null)
			{
				throw new ArgumentNullException(parameter);
			}
			result = defaultValue;
			string[] array = parameter.Split(new char[]
			{
				'='
			});
			if (string.Compare(array[0], parameterName, StringComparison.OrdinalIgnoreCase) == 0)
			{
				if (array.Length == 2)
				{
					bool.TryParse(array[1], out result);
				}
				return true;
			}
			return false;
		}

		// Token: 0x060029FF RID: 10751 RVA: 0x000A2A3C File Offset: 0x000A0C3C
		protected static void AddHttpProperty(Message message, HttpStatusCode status, bool isXml)
		{
			if (message == null)
			{
				throw new ArgumentNullException("message");
			}
			string value = isXml ? "text/xml; charset=UTF-8" : "text/html; charset=UTF-8";
			HttpResponseMessageProperty httpResponseMessageProperty = new HttpResponseMessageProperty();
			httpResponseMessageProperty.StatusCode = status;
			httpResponseMessageProperty.Headers.Add(HttpResponseHeader.ContentType, value);
			message.Properties.Add(HttpResponseMessageProperty.Name, httpResponseMessageProperty);
		}

		// Token: 0x06002A00 RID: 10752 RVA: 0x000A2A93 File Offset: 0x000A0C93
		protected static bool EnsureHttpStatusCode(int code)
		{
			return code >= 200 && code <= 599;
		}

		// Token: 0x06002A01 RID: 10753 RVA: 0x000A2AAC File Offset: 0x000A0CAC
		private string[] FormatCommunicationTimeouts(ServiceHealthModel.CommunicationTimeoutsModel timeouts)
		{
			string[] array = new string[4];
			if (timeouts != null && timeouts.HasTimeouts)
			{
				array[0] = SR.GetString("ServiceHealthBehavior_Close") + ": <b>" + timeouts.CloseTimeout.ToString("dd\\.hh\\:mm\\:ss") + "</b>";
				array[1] = SR.GetString("ServiceHealthBehavior_Open") + ": <b>" + timeouts.OpenTimeout.ToString("dd\\.hh\\:mm\\:ss") + "</b>";
				array[2] = SR.GetString("ServiceHealthBehavior_Receive") + ": <b>" + timeouts.ReceiveTimeout.ToString("dd\\.hh\\:mm\\:ss") + "</b>";
				array[3] = SR.GetString("ServiceHealthBehavior_Send") + ": <b>" + timeouts.SendTimeout.ToString("dd\\.hh\\:mm\\:ss") + "</b>";
			}
			return array;
		}

		// Token: 0x06002A02 RID: 10754 RVA: 0x000A2B90 File Offset: 0x000A0D90
		private string[] FormatThreads(ServiceHealthModel.ProcessThreadsModel processThreads)
		{
			return new string[]
			{
				string.Format("{0}: <b>{1}</b>", SR.GetString("ServiceHealthBehavior_NativeThreadCount"), processThreads.NativeThreadCount),
				string.Format("{0}: {1}: <b>{2}</b> {3}: <b>{4}</b> {5}: <b>{6}</b>", new object[]
				{
					SR.GetString("ServiceHealthBehavior_WorkerThreads"),
					SR.GetString("ServiceHealthBehavior_Available"),
					processThreads.AvailableWorkerThreads,
					SR.GetString("ServiceHealthBehavior_MaxLimit"),
					processThreads.MaxWorkerThreads,
					SR.GetString("ServiceHealthBehavior_MinLimit"),
					processThreads.MinWorkerThreads
				}),
				string.Format("{0}: {1}: <b>{2}</b> {3}: <b>{4}</b> {5}: <b>{6}</b>", new object[]
				{
					SR.GetString("ServiceHealthBehavior_CompletionPortThreads"),
					SR.GetString("ServiceHealthBehavior_Available"),
					processThreads.AvailableCompletionPortThreads,
					SR.GetString("ServiceHealthBehavior_MaxLimit"),
					processThreads.MaxCompletionPortThreads,
					SR.GetString("ServiceHealthBehavior_MinLimit"),
					processThreads.MinCompletionPortThreads
				})
			};
		}

		// Token: 0x06002A03 RID: 10755 RVA: 0x000A2CAC File Offset: 0x000A0EAC
		private string[] FormatServiceThrottle(ServiceHealthModel.ServiceThrottleModel serviceThrottle)
		{
			string[] array = new string[3];
			if (serviceThrottle != null && serviceThrottle.HasThrottle)
			{
				array[0] = this.FormatThrottle(SR.GetString("ServiceHealthBehavior_ConcurrentCalls"), serviceThrottle.CallsCount, serviceThrottle.CallsCapacity);
				array[1] = this.FormatThrottle(SR.GetString("ServiceHealthBehavior_Sessions"), serviceThrottle.SessionsCount, serviceThrottle.SessionsCapacity);
				array[2] = this.FormatThrottle(SR.GetString("ServiceHealthBehavior_Instances"), serviceThrottle.InstanceContextsCount, serviceThrottle.InstanceContextsCapacity);
			}
			return array;
		}

		// Token: 0x06002A04 RID: 10756 RVA: 0x000A2D29 File Offset: 0x000A0F29
		private string FormatThrottle(string label, int count, int capacity)
		{
			return string.Format("{0}: <b>{1}</b>/<b>{2}</b>", label, count, capacity);
		}

		// Token: 0x06002A05 RID: 10757 RVA: 0x000A2D44 File Offset: 0x000A0F44
		private string FormatCommunicationState(CommunicationState? state)
		{
			if (state != null)
			{
				return state.Value.ToString();
			}
			return string.Empty;
		}

		// Token: 0x040022AD RID: 8877
		private const string TimeSpanFormat = "dd\\.hh\\:mm\\:ss";

		// Token: 0x040022AE RID: 8878
		private static readonly IComparer<int> descendingComparer = new ServiceHealthBehavior.DescendingComparer<int>();

		// Token: 0x02000C0F RID: 3087
		private class EmptyMessage : ContentOnlyMessage
		{
			// Token: 0x0600763C RID: 30268 RVA: 0x001BBD20 File Offset: 0x001B9F20
			protected override void OnWriteBodyContents(XmlDictionaryWriter writer)
			{
			}
		}

		// Token: 0x02000C10 RID: 3088
		private class XmlDocumentMessage : ContentOnlyMessage
		{
			// Token: 0x0600763D RID: 30269 RVA: 0x001BBD22 File Offset: 0x001B9F22
			public XmlDocumentMessage(XmlDocument document)
			{
				this.document = document;
			}

			// Token: 0x0600763E RID: 30270 RVA: 0x001BBD31 File Offset: 0x001B9F31
			protected override void OnWriteBodyContents(XmlDictionaryWriter writer)
			{
				this.document.WriteTo(writer);
			}

			// Token: 0x040042F7 RID: 17143
			private XmlDocument document;
		}

		// Token: 0x02000C11 RID: 3089
		private class ServiceHealthMessage : ContentOnlyMessage
		{
			// Token: 0x0600763F RID: 30271 RVA: 0x001BBD3F File Offset: 0x001B9F3F
			public ServiceHealthMessage(ServiceHealthSectionCollection healthInfo, string serviceName, int httpStatusCode)
			{
				this.healthInfo = healthInfo;
				this.httpStatusCode = httpStatusCode;
				this.serviceName = serviceName;
			}

			// Token: 0x06007640 RID: 30272 RVA: 0x001BBD5C File Offset: 0x001B9F5C
			protected override void OnWriteBodyContents(XmlDictionaryWriter writer)
			{
				this.writer = writer;
				this.writer.WriteStartElement("html");
				this.writer.WriteAttributeString("lang", this.GetISOLanguageNameFromResourceManager(SR.Resources));
				this.writer.WriteStartElement("head");
				this.WriteStyleSheet();
				this.writer.WriteElementString("title", this.serviceName);
				this.writer.WriteEndElement();
				this.writer.WriteStartElement("body");
				this.writer.WriteStartElement("div");
				this.writer.WriteAttributeString("role", "main");
				this.WriteTitleHeader(this.serviceName);
				this.WriteServiceHealthSectionCollection();
				this.writer.WriteEndElement();
				this.writer.WriteEndElement();
				this.writer.WriteEndElement();
			}

			// Token: 0x06007641 RID: 30273 RVA: 0x001BBE3C File Offset: 0x001BA03C
			private string GetISOLanguageNameFromResourceManager(ResourceManager rm)
			{
				try
				{
					CultureInfo cultureInfo = CultureInfo.CurrentCulture;
					while (cultureInfo.Name.Length > 0)
					{
						if (rm.GetResourceSet(cultureInfo, false, false) != null)
						{
							return cultureInfo.TwoLetterISOLanguageName;
						}
						cultureInfo = cultureInfo.Parent;
					}
				}
				catch (Exception)
				{
				}
				return "en";
			}

			// Token: 0x06007642 RID: 30274 RVA: 0x001BBE98 File Offset: 0x001BA098
			private void WriteServiceHealthSectionCollection()
			{
				if (this.healthInfo == null || this.healthInfo.Count == 0)
				{
					return;
				}
				foreach (ServiceHealthSection serviceHealthSection in this.healthInfo)
				{
					this.WriteSectionTitle(serviceHealthSection);
					bool flag = false;
					foreach (ServiceHealthDataCollection elements in serviceHealthSection)
					{
						this.writer.WriteStartElement("div");
						this.writer.WriteAttributeString("class", "section");
						this.writer.WriteStartElement("div");
						if (flag)
						{
							this.writer.WriteAttributeString("class", "section subsection_even");
						}
						else
						{
							this.writer.WriteAttributeString("class", "section subsection_odd");
						}
						this.writer.WriteStartElement("dl");
						this.writer.WriteAttributeString("class", "formatted_list");
						this.WriteServiceHealthElements(elements);
						this.writer.WriteEndElement();
						this.writer.WriteEndElement();
						this.writer.WriteEndElement();
						flag = !flag;
					}
				}
			}

			// Token: 0x06007643 RID: 30275 RVA: 0x001BC00C File Offset: 0x001BA20C
			private void WriteServiceHealthElements(ServiceHealthDataCollection elements)
			{
				foreach (ServiceHealthData serviceHealthData in elements)
				{
					if (serviceHealthData.Values != null && serviceHealthData.Values.Length != 0)
					{
						if (serviceHealthData.Values.Length == 1)
						{
							this.WriteElement(serviceHealthData.Key, serviceHealthData.Values[0]);
						}
						else
						{
							this.WriteElement(serviceHealthData.Key, serviceHealthData.Values);
						}
					}
				}
			}

			// Token: 0x06007644 RID: 30276 RVA: 0x001BC094 File Offset: 0x001BA294
			private void WriteElement(string label, string value)
			{
				if (!string.IsNullOrWhiteSpace(value))
				{
					this.writer.WriteStartElement("div");
					this.writer.WriteStartElement("dt");
					this.writer.WriteStartElement("span");
					this.writer.WriteAttributeString("class", "label");
					this.writer.WriteString(label + ": ");
					this.writer.WriteEndElement();
					this.writer.WriteEndElement();
					this.writer.WriteStartElement("dd");
					this.writer.WriteRaw(value + "\r\n");
					this.writer.WriteEndElement();
					this.writer.WriteEndElement();
				}
			}

			// Token: 0x06007645 RID: 30277 RVA: 0x001BC15C File Offset: 0x001BA35C
			private void WriteElement(string label, string[] values)
			{
				this.writer.WriteRaw("<br />");
				this.writer.WriteStartElement("div");
				this.writer.WriteStartElement("dt");
				this.writer.WriteStartElement("span");
				this.writer.WriteAttributeString("class", "label");
				this.writer.WriteString(label + ": ");
				this.writer.WriteEndElement();
				this.writer.WriteEndElement();
				this.writer.WriteRaw("<dd>");
				this.writer.WriteRaw("<ul>");
				foreach (string text in values)
				{
					if (!string.IsNullOrWhiteSpace(text))
					{
						this.writer.WriteRaw("<li>");
						this.writer.WriteRaw(text + "<br />\r\n");
						this.writer.WriteRaw("</li>");
					}
				}
				this.writer.WriteRaw("</ul>");
				this.writer.WriteRaw("</dd>");
				this.writer.WriteEndElement();
				this.writer.WriteRaw(Environment.NewLine);
			}

			// Token: 0x06007646 RID: 30278 RVA: 0x001BC298 File Offset: 0x001BA498
			private void WriteStyleSheet()
			{
				this.writer.WriteStartElement("style");
				this.writer.WriteAttributeString("type", "text/css");
				this.writer.WriteString("body { margin: 0px; color: #000000; font-family: Segoe UI; background-color: #ffffff }");
				this.writer.WriteString(".header { width: 100%; margin: 0px; padding: 5px 25px; text-transform: lowercase; background-color: #dceeff; font-size: 20px; }");
				this.writer.WriteString(".header_title { width: 1%; white-space: nowrap; font-weight: bold; font-size: 26px; text-transform: none; }");
				this.writer.WriteString(".header_statuscode { width: 1%; white-space: nowrap; }");
				this.writer.WriteString(".header_datetime { float: right }");
				this.writer.WriteString(".section { width: 100%; margin: 0px; padding: 5px 25px; font-size: 13px; }");
				this.writer.WriteString(".subsection_even { padding: 0px; background: #fafafa; border: 1px solid #666666; }");
				this.writer.WriteString(".subsection_odd { padding: 0px; }");
				this.writer.WriteString(".title { font-weight: bold; font-size: 1.08em; text-transform: uppercase; }");
				this.writer.WriteString(".content { font-size: 12px; padding: 0 30px; }");
				this.writer.WriteString(".label { font-weight: bolder; font-size: 1.05em; }");
				this.writer.WriteString(".bullet { font-size: 1.13em; font-weight: bold; }");
				this.writer.WriteString("ul { list-style-type: none; margin: 0 auto; padding-left: 0px}");
				this.writer.WriteString(".formatted_list dt,");
				this.writer.WriteString(".formatted_list dd {display: inline-block; vertical-align: top;}");
				this.writer.WriteString(".formatted_list dt {width:  150px;}");
				this.writer.WriteEndElement();
			}

			// Token: 0x06007647 RID: 30279 RVA: 0x001BC3D8 File Offset: 0x001BA5D8
			private void WriteTitleHeader(string title)
			{
				this.writer.WriteStartElement("div");
				this.writer.WriteAttributeString("class", "header");
				this.writer.WriteStartElement("span");
				this.writer.WriteAttributeString("class", "header_title");
				this.writer.WriteString(title);
				this.writer.WriteString(" ");
				this.writer.WriteEndElement();
				this.writer.WriteStartElement("span");
				this.writer.WriteAttributeString("class", "header_statuscode");
				this.writer.WriteString((this.httpStatusCode >= 0) ? string.Format("HTTP/{0}", this.httpStatusCode) : " ");
				this.writer.WriteEndElement();
				this.writer.WriteStartElement("span");
				this.writer.WriteAttributeString("class", "header_datetime");
				this.writer.WriteString(DateTime.Now.ToString());
				this.writer.WriteEndElement();
				this.writer.WriteEndElement();
			}

			// Token: 0x06007648 RID: 30280 RVA: 0x001BC50C File Offset: 0x001BA70C
			private void WriteSectionTitle(ServiceHealthSection section)
			{
				this.writer.WriteStartElement("h1");
				this.writer.WriteAttributeString("class", "section title");
				this.writer.WriteAttributeString("style", "background: " + section.BackgroundColor + "; color: " + section.ForegroundColor);
				this.writer.WriteString(section.Title);
				this.writer.WriteEndElement();
			}

			// Token: 0x040042F8 RID: 17144
			private XmlDictionaryWriter writer;

			// Token: 0x040042F9 RID: 17145
			private ServiceHealthSectionCollection healthInfo;

			// Token: 0x040042FA RID: 17146
			private int httpStatusCode;

			// Token: 0x040042FB RID: 17147
			private string serviceName;
		}

		// Token: 0x02000C12 RID: 3090
		private class DescendingComparer<T> : IComparer<T> where T : IComparable<T>
		{
			// Token: 0x06007649 RID: 30281 RVA: 0x001BC585 File Offset: 0x001BA785
			public int Compare(T x, T y)
			{
				return y.CompareTo(x);
			}
		}
	}
}
