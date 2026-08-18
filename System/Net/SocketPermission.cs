using System;
using System.Collections;
using System.Globalization;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Net
{
	// Token: 0x02000441 RID: 1089
	[Serializable]
	public sealed class SocketPermission : CodeAccessPermission, IUnrestrictedPermission
	{
		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x06002234 RID: 8756 RVA: 0x00087148 File Offset: 0x00086148
		public IEnumerator ConnectList
		{
			get
			{
				return this.m_connectList.GetEnumerator();
			}
		}

		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x06002235 RID: 8757 RVA: 0x00087155 File Offset: 0x00086155
		public IEnumerator AcceptList
		{
			get
			{
				return this.m_acceptList.GetEnumerator();
			}
		}

		// Token: 0x06002236 RID: 8758 RVA: 0x00087162 File Offset: 0x00086162
		public SocketPermission(PermissionState state)
		{
			this.initialize();
			this.m_noRestriction = (state == PermissionState.Unrestricted);
		}

		// Token: 0x06002237 RID: 8759 RVA: 0x0008717A File Offset: 0x0008617A
		internal SocketPermission(bool free)
		{
			this.initialize();
			this.m_noRestriction = free;
		}

		// Token: 0x06002238 RID: 8760 RVA: 0x0008718F File Offset: 0x0008618F
		public SocketPermission(NetworkAccess access, TransportType transport, string hostName, int portNumber)
		{
			this.initialize();
			this.m_noRestriction = false;
			this.AddPermission(access, transport, hostName, portNumber);
		}

		// Token: 0x06002239 RID: 8761 RVA: 0x000871B0 File Offset: 0x000861B0
		public void AddPermission(NetworkAccess access, TransportType transport, string hostName, int portNumber)
		{
			if (hostName == null)
			{
				throw new ArgumentNullException("hostName");
			}
			EndpointPermission endPoint = new EndpointPermission(hostName, portNumber, transport);
			this.AddPermission(access, endPoint);
		}

		// Token: 0x0600223A RID: 8762 RVA: 0x000871DD File Offset: 0x000861DD
		internal void AddPermission(NetworkAccess access, EndpointPermission endPoint)
		{
			if (this.m_noRestriction)
			{
				return;
			}
			if ((access & NetworkAccess.Connect) != (NetworkAccess)0)
			{
				this.m_connectList.Add(endPoint);
			}
			if ((access & NetworkAccess.Accept) != (NetworkAccess)0)
			{
				this.m_acceptList.Add(endPoint);
			}
		}

		// Token: 0x0600223B RID: 8763 RVA: 0x00087211 File Offset: 0x00086211
		public bool IsUnrestricted()
		{
			return this.m_noRestriction;
		}

		// Token: 0x0600223C RID: 8764 RVA: 0x0008721C File Offset: 0x0008621C
		public override IPermission Copy()
		{
			return new SocketPermission(this.m_noRestriction)
			{
				m_connectList = (ArrayList)this.m_connectList.Clone(),
				m_acceptList = (ArrayList)this.m_acceptList.Clone()
			};
		}

		// Token: 0x0600223D RID: 8765 RVA: 0x00087264 File Offset: 0x00086264
		private bool FindSubset(ArrayList source, ArrayList target)
		{
			foreach (object obj in source)
			{
				EndpointPermission endpointPermission = (EndpointPermission)obj;
				bool flag = false;
				foreach (object obj2 in target)
				{
					EndpointPermission e = (EndpointPermission)obj2;
					if (endpointPermission.SubsetMatch(e))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600223E RID: 8766 RVA: 0x00087314 File Offset: 0x00086314
		public override IPermission Union(IPermission target)
		{
			if (target == null)
			{
				return this.Copy();
			}
			SocketPermission socketPermission = target as SocketPermission;
			if (socketPermission == null)
			{
				throw new ArgumentException(SR.GetString("net_perm_target"), "target");
			}
			if (this.m_noRestriction || socketPermission.m_noRestriction)
			{
				return new SocketPermission(true);
			}
			SocketPermission socketPermission2 = (SocketPermission)socketPermission.Copy();
			for (int i = 0; i < this.m_connectList.Count; i++)
			{
				socketPermission2.AddPermission(NetworkAccess.Connect, (EndpointPermission)this.m_connectList[i]);
			}
			for (int j = 0; j < this.m_acceptList.Count; j++)
			{
				socketPermission2.AddPermission(NetworkAccess.Accept, (EndpointPermission)this.m_acceptList[j]);
			}
			return socketPermission2;
		}

		// Token: 0x0600223F RID: 8767 RVA: 0x000873D0 File Offset: 0x000863D0
		public override IPermission Intersect(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			SocketPermission socketPermission = target as SocketPermission;
			if (socketPermission == null)
			{
				throw new ArgumentException(SR.GetString("net_perm_target"), "target");
			}
			SocketPermission socketPermission2;
			if (this.m_noRestriction)
			{
				socketPermission2 = (SocketPermission)socketPermission.Copy();
			}
			else if (socketPermission.m_noRestriction)
			{
				socketPermission2 = (SocketPermission)this.Copy();
			}
			else
			{
				socketPermission2 = new SocketPermission(false);
				SocketPermission.intersectLists(this.m_connectList, socketPermission.m_connectList, socketPermission2.m_connectList);
				SocketPermission.intersectLists(this.m_acceptList, socketPermission.m_acceptList, socketPermission2.m_acceptList);
			}
			if (!socketPermission2.m_noRestriction && socketPermission2.m_connectList.Count == 0 && socketPermission2.m_acceptList.Count == 0)
			{
				return null;
			}
			return socketPermission2;
		}

		// Token: 0x06002240 RID: 8768 RVA: 0x00087488 File Offset: 0x00086488
		public override bool IsSubsetOf(IPermission target)
		{
			if (target == null)
			{
				return !this.m_noRestriction && this.m_connectList.Count == 0 && this.m_acceptList.Count == 0;
			}
			SocketPermission socketPermission = target as SocketPermission;
			if (socketPermission == null)
			{
				throw new ArgumentException(SR.GetString("net_perm_target"), "target");
			}
			if (socketPermission.IsUnrestricted())
			{
				return true;
			}
			if (this.IsUnrestricted())
			{
				return false;
			}
			if (this.m_acceptList.Count + this.m_connectList.Count == 0)
			{
				return true;
			}
			if (socketPermission.m_acceptList.Count + socketPermission.m_connectList.Count == 0)
			{
				return false;
			}
			bool result = false;
			try
			{
				if (this.FindSubset(this.m_connectList, socketPermission.m_connectList) && this.FindSubset(this.m_acceptList, socketPermission.m_acceptList))
				{
					result = true;
				}
			}
			finally
			{
				this.CleanupDNS();
			}
			return result;
		}

		// Token: 0x06002241 RID: 8769 RVA: 0x00087570 File Offset: 0x00086570
		private void CleanupDNS()
		{
			foreach (object obj in this.m_connectList)
			{
				EndpointPermission endpointPermission = (EndpointPermission)obj;
				if (!endpointPermission.cached)
				{
					endpointPermission.address = null;
				}
			}
			foreach (object obj2 in this.m_acceptList)
			{
				EndpointPermission endpointPermission2 = (EndpointPermission)obj2;
				if (!endpointPermission2.cached)
				{
					endpointPermission2.address = null;
				}
			}
		}

		// Token: 0x06002242 RID: 8770 RVA: 0x00087628 File Offset: 0x00086628
		public override void FromXml(SecurityElement securityElement)
		{
			if (securityElement == null)
			{
				throw new ArgumentNullException("securityElement");
			}
			if (!securityElement.Tag.Equals("IPermission"))
			{
				throw new ArgumentException(SR.GetString("net_not_ipermission"), "securityElement");
			}
			string text = securityElement.Attribute("class");
			if (text == null)
			{
				throw new ArgumentException(SR.GetString("net_no_classname"), "securityElement");
			}
			if (text.IndexOf(base.GetType().FullName) < 0)
			{
				throw new ArgumentException(SR.GetString("net_no_typename"), "securityElement");
			}
			this.initialize();
			string text2 = securityElement.Attribute("Unrestricted");
			if (text2 != null)
			{
				this.m_noRestriction = (0 == string.Compare(text2, "true", StringComparison.OrdinalIgnoreCase));
				if (this.m_noRestriction)
				{
					return;
				}
			}
			this.m_noRestriction = false;
			this.m_connectList = new ArrayList();
			this.m_acceptList = new ArrayList();
			SecurityElement securityElement2 = securityElement.SearchForChildByTag("ConnectAccess");
			if (securityElement2 != null)
			{
				SocketPermission.ParseAddXmlElement(securityElement2, this.m_connectList, "ConnectAccess, ");
			}
			securityElement2 = securityElement.SearchForChildByTag("AcceptAccess");
			if (securityElement2 != null)
			{
				SocketPermission.ParseAddXmlElement(securityElement2, this.m_acceptList, "AcceptAccess, ");
			}
		}

		// Token: 0x06002243 RID: 8771 RVA: 0x00087748 File Offset: 0x00086748
		private static void ParseAddXmlElement(SecurityElement et, ArrayList listToAdd, string accessStr)
		{
			foreach (object obj in et.Children)
			{
				SecurityElement securityElement = (SecurityElement)obj;
				if (securityElement.Tag.Equals("ENDPOINT"))
				{
					Hashtable attributes = securityElement.Attributes;
					string text;
					try
					{
						text = (attributes["host"] as string);
					}
					catch
					{
						text = null;
					}
					if (text == null)
					{
						throw new ArgumentNullException(accessStr + "host");
					}
					string epname = text;
					try
					{
						text = (attributes["transport"] as string);
					}
					catch
					{
						text = null;
					}
					if (text == null)
					{
						throw new ArgumentNullException(accessStr + "transport");
					}
					TransportType trtype;
					try
					{
						trtype = (TransportType)Enum.Parse(typeof(TransportType), text, true);
					}
					catch (Exception ex)
					{
						if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
						{
							throw;
						}
						throw new ArgumentException(accessStr + "transport", ex);
					}
					catch
					{
						throw new ArgumentException(accessStr + "transport", new Exception(SR.GetString("net_nonClsCompliantException")));
					}
					try
					{
						text = (attributes["port"] as string);
					}
					catch
					{
						text = null;
					}
					if (text == null)
					{
						throw new ArgumentNullException(accessStr + "port");
					}
					if (string.Compare(text, "All", StringComparison.OrdinalIgnoreCase) == 0)
					{
						text = "-1";
					}
					int num;
					try
					{
						num = int.Parse(text, NumberFormatInfo.InvariantInfo);
					}
					catch (Exception ex2)
					{
						if (ex2 is ThreadAbortException || ex2 is StackOverflowException || ex2 is OutOfMemoryException)
						{
							throw;
						}
						throw new ArgumentException(SR.GetString("net_perm_invalid_val", new object[]
						{
							accessStr + "port",
							text
						}), ex2);
					}
					catch
					{
						throw new ArgumentException(SR.GetString("net_perm_invalid_val", new object[]
						{
							accessStr + "port",
							text
						}), new Exception(SR.GetString("net_nonClsCompliantException")));
					}
					if (!ValidationHelper.ValidateTcpPort(num) && num != -1)
					{
						throw new ArgumentOutOfRangeException(SR.GetString("net_perm_invalid_val", new object[]
						{
							accessStr + "port",
							text
						}));
					}
					listToAdd.Add(new EndpointPermission(epname, num, trtype));
				}
			}
		}

		// Token: 0x06002244 RID: 8772 RVA: 0x00087A6C File Offset: 0x00086A6C
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = new SecurityElement("IPermission");
			securityElement.AddAttribute("class", base.GetType().FullName + ", " + base.GetType().Module.Assembly.FullName.Replace('"', '\''));
			securityElement.AddAttribute("version", "1");
			if (!this.IsUnrestricted())
			{
				if (this.m_connectList.Count > 0)
				{
					SecurityElement securityElement2 = new SecurityElement("ConnectAccess");
					foreach (object obj in this.m_connectList)
					{
						EndpointPermission endpointPermission = (EndpointPermission)obj;
						SecurityElement securityElement3 = new SecurityElement("ENDPOINT");
						securityElement3.AddAttribute("host", endpointPermission.Hostname);
						securityElement3.AddAttribute("transport", endpointPermission.Transport.ToString());
						securityElement3.AddAttribute("port", (endpointPermission.Port != -1) ? endpointPermission.Port.ToString(NumberFormatInfo.InvariantInfo) : "All");
						securityElement2.AddChild(securityElement3);
					}
					securityElement.AddChild(securityElement2);
				}
				if (this.m_acceptList.Count > 0)
				{
					SecurityElement securityElement4 = new SecurityElement("AcceptAccess");
					foreach (object obj2 in this.m_acceptList)
					{
						EndpointPermission endpointPermission2 = (EndpointPermission)obj2;
						SecurityElement securityElement5 = new SecurityElement("ENDPOINT");
						securityElement5.AddAttribute("host", endpointPermission2.Hostname);
						securityElement5.AddAttribute("transport", endpointPermission2.Transport.ToString());
						securityElement5.AddAttribute("port", (endpointPermission2.Port != -1) ? endpointPermission2.Port.ToString(NumberFormatInfo.InvariantInfo) : "All");
						securityElement4.AddChild(securityElement5);
					}
					securityElement.AddChild(securityElement4);
				}
			}
			else
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			return securityElement;
		}

		// Token: 0x06002245 RID: 8773 RVA: 0x00087CBC File Offset: 0x00086CBC
		private void initialize()
		{
			this.m_noRestriction = false;
			this.m_connectList = new ArrayList();
			this.m_acceptList = new ArrayList();
		}

		// Token: 0x06002246 RID: 8774 RVA: 0x00087CDC File Offset: 0x00086CDC
		private static void intersectLists(ArrayList A, ArrayList B, ArrayList result)
		{
			bool[] array = new bool[A.Count];
			bool[] array2 = new bool[B.Count];
			int num = 0;
			int num2 = 0;
			foreach (object obj in A)
			{
				EndpointPermission endpointPermission = (EndpointPermission)obj;
				num2 = 0;
				foreach (object obj2 in B)
				{
					EndpointPermission obj3 = (EndpointPermission)obj2;
					if (!array2[num2] && endpointPermission.Equals(obj3))
					{
						result.Add(endpointPermission);
						array[num] = (array2[num2] = true);
						break;
					}
					num2++;
				}
				num++;
			}
			num = 0;
			foreach (object obj4 in A)
			{
				EndpointPermission endpointPermission2 = (EndpointPermission)obj4;
				if (!array[num])
				{
					num2 = 0;
					foreach (object obj5 in B)
					{
						EndpointPermission e = (EndpointPermission)obj5;
						if (!array2[num2])
						{
							EndpointPermission endpointPermission3 = endpointPermission2.Intersect(e);
							if (endpointPermission3 != null)
							{
								bool flag = false;
								foreach (object obj6 in result)
								{
									EndpointPermission endpointPermission4 = (EndpointPermission)obj6;
									if (endpointPermission4.Equals(endpointPermission3))
									{
										flag = true;
										break;
									}
								}
								if (!flag)
								{
									result.Add(endpointPermission3);
								}
							}
						}
						num2++;
					}
				}
				num++;
			}
		}

		// Token: 0x04002221 RID: 8737
		public const int AllPorts = -1;

		// Token: 0x04002222 RID: 8738
		internal const int AnyPort = 0;

		// Token: 0x04002223 RID: 8739
		private ArrayList m_connectList;

		// Token: 0x04002224 RID: 8740
		private ArrayList m_acceptList;

		// Token: 0x04002225 RID: 8741
		private bool m_noRestriction;
	}
}
