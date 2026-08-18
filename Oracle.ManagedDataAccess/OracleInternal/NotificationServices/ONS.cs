using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;

namespace OracleInternal.NotificationServices
{
	// Token: 0x02000187 RID: 391
	internal class ONS
	{
		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000F07 RID: 3847 RVA: 0x0009C088 File Offset: 0x0009A288
		private string HostName
		{
			get
			{
				string result = null;
				try
				{
					result = Dns.GetHostName();
				}
				catch (Exception)
				{
					result = "UNKNOWNHOST";
				}
				return result;
			}
		}

		// Token: 0x06000F08 RID: 3848 RVA: 0x0009C0BC File Offset: 0x0009A2BC
		public ONS(string config)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			try
			{
				if (string.IsNullOrEmpty(config))
				{
					throw new ONSException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.ONS_NO_NODE_LISTS, new string[0]));
				}
				this.localConn = false;
				string config2 = this.getConfig("oracle.ons.nodes", false, "nodes=", config);
				string config3 = this.getConfig("oracle.ons.remotetimeout", true, "remotetimeout=", config);
				this.remoteIOtimeout = 30000;
				if (config3 != null)
				{
					try
					{
						int num = int.Parse(config3);
						if (num > 0)
						{
							num = num / 2 + 1;
							if (num < 10)
							{
								num = 10;
							}
							else if (num > 1800)
							{
								num = 1800;
							}
							this.remoteIOtimeout = num * 1000;
						}
						else
						{
							this.remoteIOtimeout = 0;
						}
					}
					catch (Exception ex)
					{
						if (ProviderConfig.m_bTraceLevelPrivate)
						{
							Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Error, new string[]
							{
								"ONS::ONS(string) failed. -" + ex.Message
							});
						}
					}
				}
				this.walletfile = this.getConfig("oracle.ons.walletfile", false, "walletfile=", config);
				this.password = this.getConfig("oracle.ons.walletpassword", false, "walletpassword=", config);
				if (this.walletfile != null)
				{
					string text = this.walletfile;
					if (text.ToLowerInvariant().StartsWith("file:"))
					{
						text = text.Substring("file:".Length);
					}
					FileInfo fileInfo = new FileInfo(text);
					if (!File.Exists(fileInfo.FullName) && !Directory.Exists(fileInfo.FullName))
					{
						throw new ONSException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.ONS_FILE_NOT_EXIST, new string[]
						{
							"Walletfile",
							this.walletfile
						}));
					}
				}
				if (this.oraclehome == null)
				{
					this.oraclehome = "direct-connect";
				}
				string myProperty = this.getMyProperty("oracle.ons.shutdowntimeout");
				if (myProperty != null)
				{
					try
					{
						long num2 = long.Parse(myProperty);
						this.shutdowntimeout = num2 * 1000L;
					}
					catch (Exception ex2)
					{
						OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)270532608, ex2, null);
					}
				}
				string myProperty2 = this.getMyProperty("oracle.ons.ignorescanvip");
				if (myProperty2 != null && bool.Parse(myProperty2))
				{
					this.useSCAN = true;
				}
				this.onsInit();
				if (config2 == null)
				{
					this.scanNodeLists(config);
					if (this.nodeLists.Count == 0)
					{
						throw new ONSException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.ONS_NO_NODE_LISTS, new string[0]));
					}
				}
				else
				{
					if (config2.Length == 0)
					{
						throw new ONSException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.ONS_NO_NODE_LISTS, new string[0]));
					}
					NodeList item = new NodeList("default", config2, this.maxconcurrency, true, this);
					this.nodeLists.Add(item);
					this.defaultList = true;
				}
				lock (this.nodeLock)
				{
					for (int i = 0; i < this.nodeLists.Count; i++)
					{
						NodeList nodeList = this.nodeLists[i];
						nodeList.start(false);
					}
				}
			}
			catch (Exception ex3)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)270532608, ex3, null);
				throw ex3;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097664, new string[0]);
				}
			}
		}

		// Token: 0x06000F09 RID: 3849 RVA: 0x0009C480 File Offset: 0x0009A680
		private void scanNodeLists(string config)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			try
			{
				if (this.nodeLists.Count == 0)
				{
					this.configNodeLists(config);
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)270532608, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097664, new string[0]);
				}
			}
		}

		// Token: 0x06000F0A RID: 3850 RVA: 0x0009C504 File Offset: 0x0009A704
		private void configNodeLists(string config)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			try
			{
				string text = new StringBuilder("nodes=".Substring(0, "nodes=".Length - 1)).ToString();
				int num = 0;
				int num2;
				while ((num2 = config.IndexOf(text, num)) != -1)
				{
					num = num2 + text.Length;
					int num3 = config.IndexOf('\n', num);
					int num4 = config.IndexOf('=', num);
					if (num4 != -1)
					{
						string text2 = config.Substring(num + 1, num4 - num - 1);
						num = num4 + 1;
						string nodes;
						if (num3 != -1)
						{
							nodes = config.Substring(num, num3 - num);
						}
						else
						{
							nodes = config.Substring(num);
						}
						int num5 = this.maxconcurrency;
						string value = this.getValue(config, new StringBuilder("maxconnections." + text2 + "=").ToString());
						if (value != null)
						{
							try
							{
								num5 = int.Parse(value);
							}
							catch (Exception ex)
							{
								if (ProviderConfig.m_bTraceLevelPrivate)
								{
									Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Error, new string[]
									{
										"ONS::configNodeLists() failed. -" + ex.Message
									});
								}
							}
							if (num5 <= 0)
							{
								num5 = this.maxconcurrency;
							}
						}
						bool active = true;
						value = this.getValue(config, new StringBuilder("active." + text2 + "=").ToString());
						if (value != null)
						{
							active = bool.Parse(value);
						}
						this.addNodeList(text2, nodes, num5, active);
					}
					if (num3 == -1)
					{
						break;
					}
					num = num3 + 1;
				}
			}
			catch (Exception ex2)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)270532608, ex2, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097664, new string[0]);
				}
			}
		}

		// Token: 0x06000F0B RID: 3851 RVA: 0x0009C6FC File Offset: 0x0009A8FC
		private void addNodeList(string id, string nodes, int concurrency, bool active)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			try
			{
				for (int i = 0; i < this.nodeLists.Count; i++)
				{
					NodeList nodeList = this.nodeLists[i];
					if (id.Equals(nodeList.Id))
					{
						return;
					}
				}
				NodeList item = new NodeList(id, nodes, concurrency, active, this);
				this.nodeLists.Add(item);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)270532608, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097664, new string[0]);
				}
			}
		}

		// Token: 0x06000F0C RID: 3852 RVA: 0x0009C7B8 File Offset: 0x0009A9B8
		protected internal virtual bool nodeListFailOver(NodeList caller, int cIndex)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			bool result;
			try
			{
				result = false;
				lock (this.nodeLock)
				{
					for (int i = 0; i < this.nodeLists.Count; i++)
					{
						NodeList nodeList = this.nodeLists[i];
						if (!nodeList.active && nodeList.failOverList == null)
						{
							nodeList.FailOver = caller;
							caller.failedOver(nodeList, cIndex);
							nodeList.start(true);
							result = true;
							break;
						}
					}
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)270532608, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097664, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06000F0D RID: 3853 RVA: 0x0009C8A4 File Offset: 0x0009AAA4
		protected internal virtual void nodeListFallBack(NodeList caller, int cIndex)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			try
			{
				NodeList failOverList = caller.failOverList;
				lock (this.nodeLock)
				{
					failOverList.stop();
					caller.fallBack(cIndex);
					failOverList.join();
					failOverList.FailOver = null;
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)270532608, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097664, new string[0]);
				}
			}
		}

		// Token: 0x06000F0E RID: 3854 RVA: 0x0009C95C File Offset: 0x0009AB5C
		private string getMyProperty(string pKey)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			string text;
			try
			{
				text = null;
				if (this.properties != null)
				{
					text = this.properties.Get(pKey);
				}
				if (text == null)
				{
					text = Environment.GetEnvironmentVariable(pKey);
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)270532608, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097664, new string[0]);
				}
			}
			return text;
		}

		// Token: 0x06000F0F RID: 3855 RVA: 0x0009C9EC File Offset: 0x0009ABEC
		private string getConfig(string pKey, bool pSys, string cKey, string config)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			string text;
			try
			{
				text = null;
				if (this.properties != null)
				{
					text = this.properties.Get(cKey);
				}
				if (text == null && pSys)
				{
					text = Environment.GetEnvironmentVariable(pKey);
				}
				if (text == null)
				{
					text = this.getValue(config, cKey);
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)270532608, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097664, new string[0]);
				}
			}
			return text;
		}

		// Token: 0x06000F10 RID: 3856 RVA: 0x0009CA8C File Offset: 0x0009AC8C
		private string getValue(string config, string key)
		{
			bool flag = false;
			string result = null;
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			try
			{
				int num = 0;
				int num2 = 0;
				int i;
				while ((i = config.IndexOf(key, num)) != -1 && !flag)
				{
					num2 = i;
					flag = true;
					num += key.Length;
					while (i > 0)
					{
						char c = config[i - 1];
						if (c == '\n')
						{
							break;
						}
						if (c != ' ')
						{
							flag = false;
							break;
						}
						i--;
					}
				}
				if (flag)
				{
					num2 += key.Length;
					int num3 = config.IndexOf('\n', num2);
					if (num3 == -1)
					{
						result = config.Substring(num2);
					}
					else
					{
						result = config.Substring(num2, num3 - num2);
					}
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)270532608, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097664, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06000F11 RID: 3857 RVA: 0x0009CB84 File Offset: 0x0009AD84
		private void onsInit()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			lock (ONS.lock_Renamed)
			{
				ONS.myoems = this;
			}
			this.myLock = new object();
			this.subscribers = new Hashtable();
			this.pendingSubscriptions = new Hashtable();
			this.subscriberId = 1;
			this.publisherId = 1;
			this.numPublishers = 0;
			this.nodeLock = new object();
			this.startTime = (DateTime.Now.Ticks - 621355968000000000L) / 10000L;
			this.processId_Renamed_Field = this.startTime.ToString();
			this.hostname_Renamed_Field = this.HostName;
			this.clusterid = null;
			this.clustername = null;
			this.instanceid = null;
			this.instancename = null;
			try
			{
				string myProperty = this.getMyProperty("oracle.ons.maxconnections");
				if (myProperty != null)
				{
					this.maxconcurrency = int.Parse(myProperty);
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)270532608, ex, null);
			}
			this.nodeLists = new List<NodeList>();
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097664, new string[0]);
			}
		}

		// Token: 0x06000F12 RID: 3858 RVA: 0x0009CCD8 File Offset: 0x0009AED8
		protected internal virtual void addSubscriber(Subscriber s, long timeout)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			try
			{
				try
				{
					lock (this.myLock)
					{
						if (this.shutdown_Renamed_Field)
						{
							return;
						}
						s.ID = this.subscriberId;
						this.subscriberId++;
						if (this.subscriberId == 99)
						{
							this.subscriberId++;
						}
					}
					SubscriptionNotification subscriptionNotification = new SubscriptionNotification(s.id(), s.subscription(), true);
					lock (this.pendingSubscriptions)
					{
						this.pendingSubscriptions[s.id()] = subscriptionNotification;
					}
					lock (this.nodeLock)
					{
						for (int i = 0; i < this.nodeLists.Count; i++)
						{
							NodeList nodeList = this.nodeLists[i];
							nodeList.send(subscriptionNotification);
						}
					}
					subscriptionNotification.waitForReply(timeout);
					if (subscriptionNotification.success)
					{
						lock (this.subscribers)
						{
							this.subscribers[s.id()] = s;
							goto IL_178;
						}
						goto IL_153;
						IL_178:
						goto IL_18C;
					}
					IL_153:
					if (subscriptionNotification.ex != null)
					{
						throw subscriptionNotification.ex;
					}
					throw new SubscriptionException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.ONS_SUBSCR_FAILED, new string[0]));
				}
				catch (Exception ex)
				{
					OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)270532608, ex, null);
					throw;
				}
				IL_18C:;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097664, new string[0]);
				}
			}
		}

		// Token: 0x06000F13 RID: 3859 RVA: 0x0009CF20 File Offset: 0x0009B120
		protected internal virtual void removeSubscriber(int id)
		{
			bool flag = false;
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			try
			{
				lock (this.subscribers)
				{
					this.subscribers.Remove(id);
					if (this.subscribers.Count == 0)
					{
						flag = true;
					}
				}
				if (flag)
				{
					lock (this.myLock)
					{
						if (this.numPublishers != 0)
						{
							flag = false;
						}
					}
				}
				if (flag)
				{
					this.shutdown();
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)270532608, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097664, new string[0]);
				}
			}
		}

		// Token: 0x06000F14 RID: 3860 RVA: 0x0009D020 File Offset: 0x0009B220
		protected internal virtual Subscriber lookupSubscriber(int id)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			Subscriber result;
			try
			{
				lock (this.subscribers)
				{
					result = (Subscriber)this.subscribers[id];
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)270532608, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097664, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06000F15 RID: 3861 RVA: 0x0009D0D0 File Offset: 0x0009B2D0
		private void closeSubscribers()
		{
			if (!ProviderConfig.m_bTraceLevelPrivate)
			{
				goto IL_18;
			}
			Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			try
			{
				Subscriber subscriber;
				do
				{
					IL_18:
					subscriber = null;
					lock (this.subscribers)
					{
						IEnumerator enumerator = this.subscribers.GetEnumerator();
						if (enumerator.MoveNext())
						{
							subscriber = (Subscriber)enumerator.Current;
							int num = subscriber.id();
							this.subscribers.Remove(num);
						}
					}
					if (subscriber != null)
					{
						subscriber.close();
					}
				}
				while (subscriber != null);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)270532608, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097664, new string[0]);
				}
			}
		}

		// Token: 0x06000F16 RID: 3862 RVA: 0x0009D1B4 File Offset: 0x0009B3B4
		protected internal virtual void addPublisher(Publisher p)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			try
			{
				lock (this.myLock)
				{
					if (!this.shutdown_Renamed_Field)
					{
						p.id(this.publisherId);
						this.publisherId++;
						this.numPublishers++;
					}
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)270532608, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097664, new string[0]);
				}
			}
		}

		// Token: 0x06000F17 RID: 3863 RVA: 0x0009D280 File Offset: 0x0009B480
		protected internal virtual void removePublisher(Publisher p)
		{
			bool flag = false;
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			try
			{
				lock (this.myLock)
				{
					this.numPublishers--;
					if (this.numPublishers == 0)
					{
						flag = true;
					}
				}
				if (flag)
				{
					lock (this.subscribers)
					{
						if (this.subscribers.Count != 0)
						{
							flag = false;
						}
					}
				}
				if (flag)
				{
					this.shutdown();
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)270532608, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097664, new string[0]);
				}
			}
		}

		// Token: 0x06000F18 RID: 3864 RVA: 0x0009D37C File Offset: 0x0009B57C
		public virtual void shutdown(long timeout)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			try
			{
				lock (this.myLock)
				{
					if (this.shutdown_Renamed_Field)
					{
						if (this.startTime != 0L)
						{
							try
							{
								Monitor.Wait(this.myLock, TimeSpan.FromMilliseconds((double)timeout));
							}
							catch (Exception ex)
							{
								OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)270532608, ex, null);
							}
						}
						return;
					}
					this.shutdown_Renamed_Field = true;
				}
				for (int i = 0; i < this.nodeLists.Count; i++)
				{
					NodeList nodeList = this.nodeLists[i];
					nodeList.Shutdown = timeout;
					nodeList.stop();
				}
				for (int j = 0; j < this.nodeLists.Count; j++)
				{
					NodeList nodeList2 = this.nodeLists[j];
					nodeList2.join();
				}
				this.closeSubscribers();
				lock (ONS.lock_Renamed)
				{
					if (ONS.myoems == this)
					{
						ONS.myoems = null;
					}
				}
				lock (this.myLock)
				{
					this.startTime = 0L;
					Monitor.PulseAll(this.myLock);
				}
			}
			catch (Exception ex2)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)270532608, ex2, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097664, new string[0]);
				}
			}
		}

		// Token: 0x06000F19 RID: 3865 RVA: 0x0009D58C File Offset: 0x0009B78C
		public virtual void shutdown()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			try
			{
				this.shutdown(this.shutdowntimeout);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)270532608, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097664, new string[0]);
				}
			}
		}

		// Token: 0x06000F1A RID: 3866 RVA: 0x0009D608 File Offset: 0x0009B808
		protected internal virtual string processId()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097664, new string[0]);
			}
			return this.processId_Renamed_Field;
		}

		// Token: 0x06000F1B RID: 3867 RVA: 0x0009D640 File Offset: 0x0009B840
		protected internal virtual void deliver(Notification e)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			try
			{
				if (e.recipients != null && e.recipients.Length > 0)
				{
					for (int i = 0; i < e.recipients.Length; i++)
					{
						Subscriber subscriber = this.lookupSubscriber(e.recipients[i]);
						if (subscriber != null)
						{
							subscriber.deliver(e);
						}
					}
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)270532608, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097664, new string[0]);
				}
			}
		}

		// Token: 0x06000F1C RID: 3868 RVA: 0x0009D6F0 File Offset: 0x0009B8F0
		protected internal virtual void handleSubscriptionReply(int sid, bool success, string message)
		{
			SubscriptionException sexcept = null;
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			try
			{
				SubscriptionNotification subscriptionNotification;
				lock (this.pendingSubscriptions)
				{
					subscriptionNotification = (SubscriptionNotification)this.pendingSubscriptions[sid];
					if (subscriptionNotification != null)
					{
						this.pendingSubscriptions.Remove(sid);
					}
				}
				if (subscriptionNotification != null)
				{
					if (!success && message != null)
					{
						sexcept = new SubscriptionException(message);
					}
					subscriptionNotification.wakeup(success, sexcept);
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)270532608, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097664, new string[0]);
				}
			}
		}

		// Token: 0x06000F1D RID: 3869 RVA: 0x0009D7D4 File Offset: 0x0009B9D4
		protected internal virtual void resendSubscriptions(SenderThread st)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			try
			{
				lock (this.pendingSubscriptions)
				{
					if (this.pendingSubscriptions.Count > 0)
					{
						foreach (object obj2 in this.pendingSubscriptions)
						{
							SubscriptionNotification subscriptionNotification = (SubscriptionNotification)((DictionaryEntry)obj2).Value;
							st.send(subscriptionNotification, 1);
						}
					}
					lock (this.subscribers)
					{
						if (this.subscribers.Count > 0)
						{
							foreach (object obj4 in this.subscribers)
							{
								Subscriber subscriber = (Subscriber)((DictionaryEntry)obj4).Value;
								int num = subscriber.id();
								if ((SubscriptionNotification)this.pendingSubscriptions[num] == null)
								{
									SubscriptionNotification subscriptionNotification = new SubscriptionNotification(subscriber.id(), subscriber.subscription(), false);
									this.pendingSubscriptions[num] = subscriptionNotification;
									st.send(subscriptionNotification, 1);
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)270532608, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097664, new string[0]);
				}
			}
		}

		// Token: 0x06000F1E RID: 3870 RVA: 0x0009D9A0 File Offset: 0x0009BBA0
		public virtual Subscriber createNewSubscriber(string s, string c, long timeout)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			Subscriber result;
			try
			{
				result = new Subscriber(this, s, c, timeout);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)270532608, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097664, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x04001179 RID: 4473
		protected internal const int ONS_REMOTE_MIN_TIMEOUT = 10;

		// Token: 0x0400117A RID: 4474
		protected internal const int ONS_REMOTE_MAX_TIMEOUT = 1800;

		// Token: 0x0400117B RID: 4475
		protected internal const int ONS_REMOTE_DFLT_TIMEOUT = 30000;

		// Token: 0x0400117C RID: 4476
		protected internal const int ONS_REMOTE_SUBSCRIBER_ID = 99;

		// Token: 0x0400117D RID: 4477
		protected internal const string ONS_REMOTE_SUBSCRIPTION = "(";

		// Token: 0x0400117E RID: 4478
		protected internal const string ONS_SSL_CONTEXT_PROTOCOL = "SSL";

		// Token: 0x0400117F RID: 4479
		protected internal const string ONS_SSL_KEY_MANAGEMENT = "SunX509";

		// Token: 0x04001180 RID: 4480
		protected internal const string ONS_NODES = "nodes=";

		// Token: 0x04001181 RID: 4481
		protected internal const string ONS_WALLET_FILE = "walletfile=";

		// Token: 0x04001182 RID: 4482
		protected internal const string ONS_PASSWORD = "walletpassword=";

		// Token: 0x04001183 RID: 4483
		protected internal const string ONS_FILE_STRING = "file:";

		// Token: 0x04001184 RID: 4484
		protected internal const string ONS_REMOTE_TIMEOUT = "remotetimeout=";

		// Token: 0x04001185 RID: 4485
		protected internal const string ONS_MAXCONNECTIONS = "maxconnections.";

		// Token: 0x04001186 RID: 4486
		protected internal const string ONS_ACTIVELIST = "active.";

		// Token: 0x04001187 RID: 4487
		protected internal const string ONS_LIST_LOCAL_ID = "local";

		// Token: 0x04001188 RID: 4488
		protected internal const string ONS_LIST_DEFAULT_ID = "default";

		// Token: 0x04001189 RID: 4489
		protected internal const char ONS_CONFIG_SEPARATOR = '\n';

		// Token: 0x0400118A RID: 4490
		public const string ONS_PERM_STRING = "ONSUser";

		// Token: 0x0400118B RID: 4491
		public const int ONS_ORACLE_HOME = 1;

		// Token: 0x0400118C RID: 4492
		public const int ONS_CONFIG_FILE = 2;

		// Token: 0x0400118D RID: 4493
		public const int ONS_ORACLE_INSTANCE = 3;

		// Token: 0x0400118E RID: 4494
		public const int ONS_PROTOCOL_VERSION = 4;

		// Token: 0x0400118F RID: 4495
		private const string SYSPROP_ORACLEHOME = "ORACLE_HOME";

		// Token: 0x04001190 RID: 4496
		private const string SYSPROP_ORACLECONFIGHOME = "oracle.ons.oracleconfighome";

		// Token: 0x04001191 RID: 4497
		private const string SYSPROP_ORACLEINSTANCE = "oracle.instance";

		// Token: 0x04001192 RID: 4498
		private const string SYSPROP_MAXCONNECTIONS = "oracle.ons.maxconnections";

		// Token: 0x04001193 RID: 4499
		private const string SYSPROP_ACTIVELIST = "oracle.ons.active";

		// Token: 0x04001194 RID: 4500
		private const string SYSPROP_SHUTDOWNTIMEOUT = "oracle.ons.shutdowntimeout";

		// Token: 0x04001195 RID: 4501
		private const string SYSPROP_IGNORESCANVIP = "oracle.ons.ignorescanvip";

		// Token: 0x04001196 RID: 4502
		private const string SYSPROP_NODES = "oracle.ons.nodes";

		// Token: 0x04001197 RID: 4503
		private const string SYSPROP_REMOTETIMEOUT = "oracle.ons.remotetimeout";

		// Token: 0x04001198 RID: 4504
		private const string SYSPROP_WALLETFILE = "oracle.ons.walletfile";

		// Token: 0x04001199 RID: 4505
		private const string SYSPROP_WALLETPASSWORD = "oracle.ons.walletpassword";

		// Token: 0x0400119A RID: 4506
		private const string SYSPROP_DEBUG = "oracle.ons.debug";

		// Token: 0x0400119B RID: 4507
		private static object lock_Renamed = new object();

		// Token: 0x0400119C RID: 4508
		private static ONS myoems = null;

		// Token: 0x0400119D RID: 4509
		private object myLock;

		// Token: 0x0400119E RID: 4510
		private Hashtable subscribers;

		// Token: 0x0400119F RID: 4511
		private Hashtable pendingSubscriptions;

		// Token: 0x040011A0 RID: 4512
		private int subscriberId;

		// Token: 0x040011A1 RID: 4513
		private int localMode;

		// Token: 0x040011A2 RID: 4514
		private int publisherId;

		// Token: 0x040011A3 RID: 4515
		private int numPublishers;

		// Token: 0x040011A4 RID: 4516
		private long startTime;

		// Token: 0x040011A5 RID: 4517
		private string processId_Renamed_Field;

		// Token: 0x040011A6 RID: 4518
		private string hostname_Renamed_Field;

		// Token: 0x040011A7 RID: 4519
		private NameValueCollection properties;

		// Token: 0x040011A8 RID: 4520
		private object nodeLock;

		// Token: 0x040011A9 RID: 4521
		protected List<NodeList> nodeLists;

		// Token: 0x040011AA RID: 4522
		protected internal string walletfile;

		// Token: 0x040011AB RID: 4523
		protected internal string password;

		// Token: 0x040011AC RID: 4524
		protected internal string clusterid;

		// Token: 0x040011AD RID: 4525
		protected internal string clustername;

		// Token: 0x040011AE RID: 4526
		protected internal string instanceid;

		// Token: 0x040011AF RID: 4527
		protected internal string instancename;

		// Token: 0x040011B0 RID: 4528
		protected internal string oraclehome;

		// Token: 0x040011B1 RID: 4529
		protected internal bool localConn = true;

		// Token: 0x040011B2 RID: 4530
		protected internal bool defaultList;

		// Token: 0x040011B3 RID: 4531
		protected internal bool useSCAN;

		// Token: 0x040011B4 RID: 4532
		protected internal bool shutdown_Renamed_Field;

		// Token: 0x040011B5 RID: 4533
		protected internal long shutdowntimeout = 5000L;

		// Token: 0x040011B6 RID: 4534
		protected internal long notificationtimeout = 30000L;

		// Token: 0x040011B7 RID: 4535
		private int maxconcurrency = 3;

		// Token: 0x040011B8 RID: 4536
		protected internal int remoteIOtimeout;
	}
}
