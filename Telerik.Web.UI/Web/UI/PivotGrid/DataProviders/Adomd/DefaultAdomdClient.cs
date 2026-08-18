using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Xml;
using Microsoft.AnalysisServices.AdomdClient;
using Telerik.Web.UI.PivotGrid.Core.Internal;
using Telerik.Web.UI.PivotGrid.Core.Olap;

namespace Telerik.Web.UI.PivotGrid.DataProviders.Adomd
{
	// Token: 0x02000D5C RID: 3420
	internal class DefaultAdomdClient : AdomdBaseClient
	{
		// Token: 0x06007FA0 RID: 32672 RVA: 0x001D2858 File Offset: 0x001D0A58
		[SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities", Justification = "mdxQuery parameter is defined in our code and cannot be modified by user.")]
		[SuppressMessage("Microsoft.Security", "CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Justification = "Design choice.")]
		internal static IDictionary<string, string> GetMeasureGroupsAndCaptions(string connectionString, string mdxQuery)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			if (connectionString != null && mdxQuery != null)
			{
				using (AdomdConnection adomdConnection = new AdomdConnection(connectionString))
				{
					adomdConnection.Open();
					using (AdomdCommand adomdCommand = adomdConnection.CreateCommand())
					{
						adomdCommand.CommandText = mdxQuery;
						using (XmlReader xmlReader = adomdCommand.ExecuteXmlReader())
						{
							while (xmlReader.Read())
							{
								if (xmlReader.Name == "MEASUREGROUP_NAME")
								{
									string key = xmlReader.ReadInnerXml();
									if (xmlReader.Name == "MEASUREGROUP_CAPTION")
									{
										string value = xmlReader.ReadInnerXml();
										dictionary.Add(key, value);
									}
								}
							}
						}
					}
					adomdConnection.Close();
				}
			}
			return dictionary;
		}

		// Token: 0x06007FA1 RID: 32673 RVA: 0x001D2934 File Offset: 0x001D0B34
		[SuppressMessage("Microsoft.Security", "CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Justification = "Design choice.")]
		[SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities", Justification = "Will fix.")]
		private static CellSet ExecuteAdomdRequest(AdomdClientRequestInfo requestInfo)
		{
			CellSet result = null;
			using (AdomdConnection adomdConnection = new AdomdConnection(requestInfo.ConnectionSettings.ConnectionString))
			{
				adomdConnection.Open();
				using (AdomdCommand adomdCommand = adomdConnection.CreateCommand())
				{
					adomdCommand.CommandText = requestInfo.MdxQuery;
					XmlReader xmlReader = adomdCommand.ExecuteXmlReader();
					using (MemoryStream memoryStream = new MemoryStream())
					{
						using (MemoryStream memoryStream2 = DefaultAdomdClient.FixKeysXmlReader(xmlReader, memoryStream))
						{
							XmlReader xmlReader2 = XmlReader.Create(memoryStream2);
							result = CellSet.LoadXml(xmlReader2);
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06007FA2 RID: 32674 RVA: 0x001D2A04 File Offset: 0x001D0C04
		private static MemoryStream FixKeysXmlReader(XmlReader xmlReader, MemoryStream memoryStream)
		{
			int num = 0;
			XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
			xmlWriterSettings.Indent = true;
			xmlWriterSettings.IndentChars = " ";
			try
			{
				using (XmlWriter xmlWriter = XmlWriter.Create(memoryStream))
				{
					xmlWriter.WriteStartElement(xmlReader.Prefix, xmlReader.LocalName, xmlReader.NamespaceURI);
					xmlWriter.WriteAttributes(xmlReader, true);
					while (xmlReader.Read())
					{
						XmlNodeType nodeType = xmlReader.NodeType;
						switch (nodeType)
						{
						case XmlNodeType.Element:
							if (xmlReader.Name == "Key")
							{
								xmlWriter.WriteStartElement(xmlReader.Prefix, xmlReader.Name + num, xmlReader.NamespaceURI);
								xmlWriter.WriteAttributes(xmlReader, true);
								num++;
							}
							else
							{
								num = 0;
								xmlWriter.WriteStartElement(xmlReader.Prefix, xmlReader.LocalName, xmlReader.NamespaceURI);
								xmlWriter.WriteAttributes(xmlReader, true);
							}
							if (xmlReader.IsEmptyElement)
							{
								xmlWriter.WriteEndElement();
							}
							break;
						case XmlNodeType.Attribute:
							break;
						case XmlNodeType.Text:
							xmlWriter.WriteString(xmlReader.Value);
							break;
						default:
							if (nodeType != XmlNodeType.ProcessingInstruction)
							{
								switch (nodeType)
								{
								case XmlNodeType.SignificantWhitespace:
									xmlWriter.WriteWhitespace(xmlReader.Value);
									continue;
								case XmlNodeType.EndElement:
									xmlWriter.WriteFullEndElement();
									continue;
								case XmlNodeType.EndEntity:
									continue;
								case XmlNodeType.XmlDeclaration:
									break;
								default:
									continue;
								}
							}
							xmlWriter.WriteProcessingInstruction(xmlReader.Name, xmlReader.Value);
							break;
						}
					}
				}
			}
			finally
			{
				if (xmlReader != null)
				{
					((IDisposable)xmlReader).Dispose();
				}
			}
			memoryStream.Position = 0L;
			return memoryStream;
		}

		// Token: 0x06007FA3 RID: 32675 RVA: 0x001D2BA8 File Offset: 0x001D0DA8
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Design choice.")]
		private void ExecuteAdomdRequest2()
		{
			AdomdClientRequestInfo currentRequest = base.GetCurrentRequest();
			try
			{
				CellSet result = DefaultAdomdClient.ExecuteAdomdRequest(currentRequest);
				AdomdClientRequestCompletedEventArgs e = new AdomdClientRequestCompletedEventArgs(result, currentRequest, null);
				base.OnSendRequestCompleted(e);
			}
			catch (Exception error)
			{
				this.HandleAdomdError(error, currentRequest);
			}
		}

		// Token: 0x06007FA4 RID: 32676 RVA: 0x001D2BF0 File Offset: 0x001D0DF0
		private void HandleAdomdError(Exception error, AdomdClientRequestInfo requestInfo)
		{
			OlapCommunicationException error2 = new OlapCommunicationException("Problem with ADOMD call", error);
			base.HandleRequestError(requestInfo, error2);
		}

		// Token: 0x06007FA5 RID: 32677 RVA: 0x001D2C14 File Offset: 0x001D0E14
		protected override void BeginNewRequestCore(AdomdClientRequestInfo requestInfo)
		{
			WorkExecutionContext contextForCurrentExecutionStrategy = WorkExecutionContext.GetContextForCurrentExecutionStrategy();
			contextForCurrentExecutionStrategy.ActionToExecute = new Action(this.ExecuteAdomdRequest2);
			contextForCurrentExecutionStrategy.Execute();
		}
	}
}
